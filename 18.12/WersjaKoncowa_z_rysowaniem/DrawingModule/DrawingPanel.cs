using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using NLog;

namespace DrawingModule
{
    /// <summary>
    /// Panel with drawing support. Some event handlers are changed/overriden 
    /// to enable drawing on the surface of the panel. 
    /// </summary>
    public class DrawingPanel : Panel
    {
        /// <summary>
        /// Logger.
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private Bitmap _image;
        private Bitmap _imageBackup; 

        /// <summary>
        /// Property with panel content as Bitmap object.
        /// </summary>
        public Bitmap Image { 
            get { return _image; }
            set { _image = value; if (_imageBackup == null && _image != null) _imageBackup = new Bitmap(_image); } 
        }

        private Point? _previous = null;
        private Pen _pen = new Pen(Color.Red);

        /// <summary>
        /// Constructor.
        /// </summary>
        public DrawingPanel()
        {
            MouseDown += DrawingPanelMouseDown;
            MouseMove += DrawingPanelMouseMove;
            MouseUp += DrawingPanelMouseUp;
        }

        private void DrawingPanelMouseDown(object sender, MouseEventArgs e)
        {
            _previous = new Point(e.X, e.Y);
            DrawingPanelMouseMove(sender, e);
        }

        private void DrawingPanelMouseMove(object sender, MouseEventArgs e)
        {
            if (_previous != null && Image != null)
            {
                using (Graphics g = Graphics.FromImage(Image))
                {
                    this.CreateGraphics().DrawLine(_pen, _previous.Value.X, _previous.Value.Y, e.X, e.Y);
                    g.DrawLine(_pen, _previous.Value.X, _previous.Value.Y, e.X, e.Y);
                }
                _previous = new Point(e.X, e.Y);
            }
        }
        private void DrawingPanelMouseUp(object sender, MouseEventArgs e)
        {
            _previous = null;
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            if (Image != null) e.Graphics.DrawImage(Image, 0, 0);
        }

        /// <summary>
        /// Restores orginal panel content.
        /// </summary>
        public void Clear()
        {
            Image = new Bitmap(_imageBackup);
            Invalidate();
        }

        /// <summary>
        /// Save content of panel as bitmap file.
        /// </summary>
        /// <param name="file">Path with file name.</param>
        /// <returns>Success/failure</returns>
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

        /// <summary>
        /// Removes original panel content.
        /// </summary>
        public void ImageBackupClear()
        {
            _imageBackup.Dispose();
            _imageBackup = null;
        }
    }
}
