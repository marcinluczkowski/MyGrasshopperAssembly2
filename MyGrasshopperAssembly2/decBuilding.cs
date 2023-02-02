using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace MyGrasshopperAssembly2
{
    public class decBuilding : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the decBuilding class.
        /// </summary>
        public decBuilding()
          : base("decBuilding", "Nickname",
              "Description",
              "NTNU", "PC2023_building")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("building", "b", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("floor", "f", "", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            BuildingClass b = new BuildingClass();
            DA.GetData(0, ref b);

            List<FloorClass> floors = new List<FloorClass>();
            if (b.floors != null)
                 floors = b.floors;

            DA.SetDataList(0, floors);

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
                return MyGrasshopperAssembly2.Properties.Resources.icon_decbuilding;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("8CEEFE3F-95D8-49CE-95A7-EAD308AD2FBD"); }
        }
    }
}