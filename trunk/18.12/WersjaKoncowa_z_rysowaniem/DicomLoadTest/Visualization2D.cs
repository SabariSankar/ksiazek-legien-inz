using Kitware.VTK;

namespace MainWindow
{

    /// <summary>
    /// Takes care of 2D visualization window.
    /// </summary>
    public class Visualization2D
    {
        /// <summary>
        /// Render window of current visualization 2D.
        /// </summary>
        private readonly RenderWindowControl _window;
        /// <summary>
        /// Viewer of current visualization 2D. Takes care of proper display slice inside the window.
        /// </summary>
        private readonly vtkImageViewer2 _viewer;
        /// <summary>
        /// Window width of the visualization 2D.
        /// </summary>
        private float _windowWidth = 100;
        /// <summary>
        /// Window level of the visualization 2D.
        /// </summary>
        private float _windowLevel = 100;
 

        /// <summary>
        /// Update the 2D visualization when the plane moved.
        /// </summary>
        /// <param name="plane">PlaneWidget which changed the coordinates.</param>
        public void PlaneMoved(vtkImagePlaneWidget plane)
        {

            _viewer.SetInput(plane.GetResliceOutput());
            UpdateViewer();

        }


        /// <summary>
        /// Refresh viewer and window.
        /// </summary>
        private void UpdateViewer()
        {
            _viewer.SetColorWindow(_windowWidth);
            _viewer.SetColorLevel(_windowLevel);
            _viewer.Render();
            _window.Update();
            _window.RenderWindow.Render();
        }

        /// <summary>
        /// Creating the 2D visualization window.
        /// </summary>
        /// <param name="window">Orginal window component. </param>
        public Visualization2D(RenderWindowControl window)
        {
            _window = window;
            vtkInteractorStyleImage imageStyle = vtkInteractorStyleImage.New();

            _viewer = vtkImageViewer2.New();
            _viewer.OffScreenRenderingOn();
            _window.RenderWindow.AddRenderer(_viewer.GetRenderer());
            _window.RenderWindow.GetInteractor().SetInteractorStyle(imageStyle);
            _viewer.Render();
        }

        public vtkRenderer GetRenderer()
        {
            return _viewer.GetRenderer(); ;
        }

   

        /// <summary>
        /// Changing the window width and level of the current slice.
        /// </summary>
        /// <param name="windowLevel"> Level of the window.</param>
        /// <param name="windowWidth"> Width of the window.</param>
        public void Update2DVisualization(float windowLevel, float windowWidth)
        {
            _windowLevel = windowLevel;
            _windowWidth = windowWidth;
          
            UpdateViewer();

        }

        /// <summary>
        /// Dispose class objects. 
        /// </summary>
        public bool Dispose()
        {
            if (_window != null) _window.Dispose();
            if (_viewer != null) _viewer.Dispose();
            return true;
        }
    }

}
