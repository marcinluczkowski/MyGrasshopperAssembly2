using Grasshopper.Kernel.Undo.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGrasshopperAssembly2
{
    internal class BuildingClass
    {

        public List<FloorClass> floors;
        public int id;
        public BuildingClass()
        { }
        public BuildingClass(int _id, List<FloorClass> _floors)
        {
            id = _id;
            floors = _floors;
        }
    }
}
