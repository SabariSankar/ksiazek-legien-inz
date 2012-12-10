using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kitware.VTK;

namespace DrawingModule
{
    public class DrawingUtils
    {
        private int _lineWidth = 1;
        public int LineWidth { get; set; }

        private int _lineColor = 1;
        public int LineColor { get; set; }

        public static void Draw(vtkImageCanvasSource2D imageCanvas, DrawingInfo drawingInfo)
        {
            imageCanvas.SetDrawColor(234,123,123,234);
            for (int j = 0; j < drawingInfo.Lines.Count; j++)
            {
                var line = drawingInfo.Lines[j];
                if (line.PointList.Count > 1)
                {
                    DrawingPoint prev = line.PointList[0];
                    for (int i = 1; i < line.PointList.Count; i++)
                    {
                        imageCanvas.FillTube(prev.X, prev.Y, line.PointList[i].X, line.PointList[i].Y, 2);
                        prev = line.PointList[i];
                    }
                }
            }
        }

    }
}
