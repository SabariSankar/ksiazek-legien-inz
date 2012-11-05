using System;
using System.Drawing;
using System.Windows.Forms;
using Kitware.VTK;

namespace MainWindow
{
    public partial class Form2 : Form
    {
        private Visualization2D _visualization2D;
        private readonly DicomLoader _dicomLoader;
        private Bitmap _windowBitmap;

        public Form2(DicomLoader dicomLoader)
        {
            InitializeComponent();
            _dicomLoader = dicomLoader;
        }

        private void renderWindowControl1_Load(object sender, EventArgs e)
        {
            _visualization2D = new Visualization2D(renderWindowControl);
            _visualization2D.SliceToAxes(_dicomLoader, 300, Axis.X);
            trackBar.Maximum = 300;
            trackBar.Value = 300;
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            _visualization2D.SliceToAxes(_dicomLoader, trackBar.Value, Axis.X);
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 1)
            {
                _windowBitmap = new System.Drawing.Bitmap(1000, 1000);
                vtkWindowToImageFilter ImageFilter = vtkWindowToImageFilter.New();
                ImageFilter.SetInput(renderWindowControl.RenderWindow);
                ImageFilter.Update();
                var bmp = ImageFilter.GetOutput().ToBitmap();
                pictureBox.Image = bmp;
                pictureBox.Update();
            }
        }

        private Point? _Previous = null;
        private Pen _Pen = new Pen(Color.Red);
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            _Previous = new Point(e.X, e.Y);
            pictureBox1_MouseMove(sender, e);
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_Previous != null)
            {
                if (pictureBox.Image == null)
                {
                    Bitmap bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.White);
                    }
                    pictureBox.Image = bmp;
                }
                using (Graphics g = Graphics.FromImage(pictureBox.Image))
                {
                    g.DrawLine(_Pen, _Previous.Value.X, _Previous.Value.Y, e.X, e.Y);
                }
                pictureBox.Invalidate();
                _Previous = new Point(e.X, e.Y);
            }
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            _Previous = null;
        }
    }
}
