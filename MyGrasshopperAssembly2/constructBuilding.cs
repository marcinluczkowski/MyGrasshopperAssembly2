using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;
using Rhino.Geometry.Intersect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace MyGrasshopperAssembly2
{
    public class constructBuilding : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public constructBuilding()
          : base("MyGrasshopperAssembly2", "Nickname",
            "Description",
            "NTNU", "PC2023_building")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("id","i","",GH_ParamAccess.item,0) ;
            pManager.AddBrepParameter("brep","b","",GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddNumberParameter("o", "", "", GH_ParamAccess.list);
            pManager.AddBrepParameter("ob", "", "", GH_ParamAccess.list);
            pManager.AddGenericParameter("building", "b", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            int id=0;
            Brep brep = new Brep();

            DA.GetData(0, ref id);
            DA.GetData(1, ref brep);
            //code to sort brep faces 
            Dictionary<BrepFace, double> dictionaryWithFaces = new Dictionary<BrepFace, double>();
            foreach (var bf in brep.Faces)
            {
                Point3d cpt = AreaMassProperties.Compute(bf).Centroid;
                dictionaryWithFaces.Add(bf, cpt.Z);
            }
            var sortedDict = dictionaryWithFaces.OrderBy(bface => bface.Value);

            List<double> zs = new List<double>();
            foreach (var sd in sortedDict)
            {
                zs.Add(sd.Value);
            }
            double minZ = zs[0];
            double maxZ = zs[zs.Count-1];

            Plane minPl = new Plane(new Point3d(0, 0, minZ), new Vector3d(0, 0, 1));
            Plane maxPl = new Plane(new Point3d(0, 0, minZ), new Vector3d(0, 0, 1));
            double floorHeight = 2.5;
            double brepHeight = maxZ - minZ;

            //looop for doing list of doubles 
            List<double> floorLevel= new List<double>();
            List<Plane> floorPlanes = new List<Plane>();
            for (int i = 0; i < 100; i++)
            {
                floorLevel.Add(floorHeight * i);
                if (floorHeight * i > brepHeight)
                    break;
                floorPlanes.Add(new Plane(new Point3d(0, 0, floorHeight * i), new Vector3d(0, 0, 1)));

            }

            //make intersection of brep and plane
            List<Brep> flatFloors = new List<Brep>();
            List<FloorClass> floors = new List<FloorClass>();

            int floorID = 0;
            foreach (var pl in floorPlanes)
            {
                Curve[] iCrvs;
                Point3d[] iPts;
                Intersection.BrepPlane(brep, pl, 0.00001, out iCrvs, out iPts);
                Brep floorBrep = Brep.CreatePlanarBreps(iCrvs)[0];
                flatFloors.Add(floorBrep);
                floors.Add(new FloorClass("flat floor", floorID, floorBrep, pl.OriginZ ));
                floorID++;
            }


            List<double> extremeZ = new List<double>() { minZ, maxZ };
            //end of the code

            BuildingClass building = new BuildingClass(id, floors);
            //Rhino.Geometry.Intersect.Intersection.BrepPlane();

            DA.SetDataList(0, extremeZ);
            DA.SetDataList(1, flatFloors);
            DA.SetData(2, building);

        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// You can add image files to your project resources and access them like this:
        /// return Resources.IconForThisComponent;
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return MyGrasshopperAssembly2.Properties.Resources.icon_building;
            }
        }
        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid => new Guid("1B828561-2E5A-40A1-840F-788B984D35A7");
    }
}