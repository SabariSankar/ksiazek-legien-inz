using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DrwaingComponentTest
{
    public partial class Form1 : Form
    {

        private Bitmap DrawingArea;  // Area to draw on.
        private List<PointF> _pointList = new List<PointF>() { new PointF(10,10), new PointF(40,40) };
        private PointF[] PointList { get { return _pointList.ToArray(); } }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DrawingArea = new Bitmap(
                    this.Width,
                    this.Height,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            using (var canvas = Graphics.FromImage(DrawingArea))
            {
                canvas.Clear(Color.Transparent);
                canvas.DrawLines(new Pen(Brushes.Black, 5), PointList);
            }

            e.Graphics.DrawImage(DrawingArea, 0, 0, Width, Height);
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            _pointList.Add(new PointF(e.X, e.Y));
            this.Invalidate();
        }

    }
}
