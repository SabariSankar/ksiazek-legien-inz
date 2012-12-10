using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrawingModule
{
 
    public class DrawingInfo
    {
        private List<DrawingLine> _lineList = new List<DrawingLine>();
        public List<DrawingLine> Lines { get { return _lineList; } }

        public void AddPoint(int x, int y)
        {
            if (_lineList.Count == 0)
                _lineList.Add(new DrawingLine());
            _lineList.Last().PointList.Add(new DrawingPoint(x, y));
        }

        public void AddLine()
        {
            if (_lineList.Count > 0 && _lineList.Last().PointList.Count > 0)
            {
                _lineList.Add(new DrawingLine());
            }
        }

        public void Clear()
        {
            _lineList.Clear();
        }
    }
}
