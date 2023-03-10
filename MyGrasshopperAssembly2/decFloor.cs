using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace MyGrasshopperAssembly2
{
    public class decFloor : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the decFloor class.
        /// </summary>
        public decFloor()
          : base("decFloor", "Nickname",
              "Description",
              "NTNU", "PC2023_building")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("floor", "f","",GH_ParamAccess.item) ;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddBrepParameter("surface", "srf","", GH_ParamAccess.item) ;
            pManager.AddTextParameter("name", "c", "", GH_ParamAccess.item);
            pManager.AddIntegerParameter("id", "i", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("zlevel", "z", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            FloorClass f= new FloorClass();
            DA.GetData(0, ref f);

            Brep b = new Brep();
            if (f.surface != null)
                b = f.surface;

            DA.SetData(0, b);
            DA.SetData(1, f.name);
            DA.SetData(2, f.floorId);
            DA.SetData(3, f.level);

        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return MyGrasshopperAssembly2.Properties.Resources.icon_decfloot;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("5DE7F874-3A71-4372-9273-41E88A23271E"); }
        }
    }
}