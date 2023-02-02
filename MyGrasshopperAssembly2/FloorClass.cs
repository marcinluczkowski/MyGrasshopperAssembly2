using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGrasshopperAssembly2
{
    internal class FloorClass
    {

        public Brep surface;
        public int floorId;
        public string name;
        public double level;

        public FloorClass()
        { }
        public FloorClass(string _name, int _floorID, Brep _surface, double _level)
        { 
            name = _name;
            floorId = _floorID;
            level = _level;
            surface = _surface;
           
        
        }
    }
}
