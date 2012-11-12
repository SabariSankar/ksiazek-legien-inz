using Kitware.VTK;

namespace MainWindow
{

    /// <summary>
    /// Name of the axis - X,Y or Z.
    /// </summary>
    public enum Axis
    {
        X,
        Y,
        Z
    };


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
        private readonly vtkImageViewer _viewer;
        /// <summary>
        /// Window width of the visualization 2D.
        /// </summary>
        private float _windowWidth = 100;
        /// <summary>
        /// Window Level of the visualization 2D.
        /// </summary>
        private float _windowLevel = 100;


        /// <summary>
        /// Update the 2D visualization when the plane moved.
        /// </summary>
        /// <param name="plane">PlaneWidget which changed the coordinates.</param>
        public void PlaneMoved(vtkImagePlaneWidget plane)
        {
            vtkImageResample resize = vtkImageResample.New();
            resize.SetInput(plane.GetResliceOutput());

            vtkImageData data = plane.GetResliceOutput();
            int[] dims = data.GetDimensions();
            double width = dims[0];
            double height = dims[1];

            //resize.SetOutputExtent(0, 350 - 1, 0, 300 - 1, 0, 0);
            //resize.SetOutputOrigin(0, 0, 0); 
            //resize.SetOutputDimensionality(2);

            double f1 = 350/width;
            double f2 = 250/height;
            if (f1 < f2)
            {
                resize.SetAxisMagnificationFactor(0, f1); //350/512
                resize.SetAxisMagnificationFactor(1, f1); //250/512
            }
            else
            {
                resize.SetAxisMagnificationFactor(0, f2); //350/512
                resize.SetAxisMagnificationFactor(1, f2); //250/512
            }

            _viewer.SetInput(resize.GetOutput());
            _viewer.SetColorWindow(_windowWidth);
            _viewer.SetColorLevel(_windowLevel);
            _viewer.Render();
            _window.Update();
            _window.RenderWindow.Render();
        }


        /// <summary>
        /// Update the 2D visualization window with new slice of pass X, Y or Z coordination. 
        /// </summary>
        /// <param name="dicomLoader"> Dicom input from we are going to cut the slice.</param>
        /// <param name="slicePosition">Coordinates of the slice. </param>
        /// <param name="axis">Name of axis to set cut orientation (X,Y,Z). </param>
        public void SliceToAxes(DicomLoader dicomLoader, float slicePosition, Axis axis)
        {
            vtkImagePlaneWidget planeWidget = vtkImagePlaneWidget.New();
            planeWidget.SetInput(dicomLoader.GetOutput());
            if (axis == Axis.X)
            {
                planeWidget.SetPlaneOrientationToXAxes();
            }
            else if (axis == Axis.Y)
            {
                planeWidget.SetPlaneOrientationToYAxes();
            }
            else if (axis == Axis.Z)
            {
                planeWidget.SetPlaneOrientationToZAxes();
            }
            planeWidget.SetSliceIndex((int)slicePosition);
            planeWidget.SetWindowLevel(_windowWidth, _windowLevel, 1);
            _viewer.SetInput(planeWidget.GetResliceOutput());
            _viewer.SetColorWindow(planeWidget.GetWindow());
            _viewer.SetColorLevel(planeWidget.GetLevel());
            planeWidget.Dispose();

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

            //vtkRenderer renderer = window.RenderWindow.GetRenderers().GetFirstRenderer();
            vtkRenderWindowInteractor renderWindowInteractor = window.RenderWindow.GetInteractor();
         
            _viewer = vtkImageViewer.New();
            _viewer.OffScreenRenderingOn();
            _viewer.SetupInteractor(renderWindowInteractor);
            //_viewer.SetRenderer(renderer);
            _window.RenderWindow.AddRenderer(_viewer.GetRenderer());
            _viewer.Render();
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
            _viewer.SetColorWindow(_windowWidth);
            _viewer.SetColorLevel(_windowLevel);
            _viewer.Render();
            _window.Update();
            _window.RenderWindow.Render();

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
