using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrawingModule
{
    public class DrawingLine
    {
        private List<DrawingPoint> _pointList = new List<DrawingPoint>();
        public List<DrawingPoint> PointList { get { return _pointList; } }
    }
}
