using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using NLog;

namespace DrawingModule
{
    public class DrawingPanel : Panel
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private Bitmap _image;
        private Bitmap _imageBackup; 

        public Bitmap Image { 
            get { return _image; }
            set { _image = value; if (_imageBackup == null && _image != null) _imageBackup = new Bitmap(_image); } 
        }

        private Point? _Previous = null;
        private Pen _Pen = new Pen(Color.Red);

        public DrawingPanel()
        {
            MouseDown += DrawingPanelMouseDown;
            MouseMove += DrawingPanelMouseMove;
            MouseUp += DrawingPanelMouseUp;
        }

        private void DrawingPanelMouseDown(object sender, MouseEventArgs e)
        {
            _Previous = new Point(e.X, e.Y);
            DrawingPanelMouseMove(sender, e);
        }

        private void DrawingPanelMouseMove(object sender, MouseEventArgs e)
        {
            if (_Previous != null)
            {
                using (Graphics g = Graphics.FromImage(Image))
                {
                    this.CreateGraphics().DrawLine(_Pen, _Previous.Value.X, _Previous.Value.Y, e.X, e.Y);
                    g.DrawLine(_Pen, _Previous.Value.X, _Previous.Value.Y, e.X, e.Y);
                }
                _Previous = new Point(e.X, e.Y);
            }
        }
        private void DrawingPanelMouseUp(object sender, MouseEventArgs e)
        {
            _Previous = null;
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            if (Image != null) e.Graphics.DrawImage(Image, 0, 0);
        }

        public void Clear()
        {
            Image = new Bitmap(_imageBackup);
            Invalidate();
        }

        public bool Save(string file)
        {
            try
            {
               Image.Save(file);
            }
            catch (Exception e)
            {
                logger.ErrorException("Saving panel view exception", e);
                return false;
            }
            return true;
        }
         
    }
}
