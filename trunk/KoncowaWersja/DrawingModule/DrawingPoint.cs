using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrawingModule
{
    public class DrawingPoint
    {

        public DrawingPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}
