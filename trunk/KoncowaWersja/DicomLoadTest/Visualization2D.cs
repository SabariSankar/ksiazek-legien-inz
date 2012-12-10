using Kitware.VTK;
using DrawingModule;

namespace MainWindow
{

    public enum RotationOperation
    {
        Back,
        Forward
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
        /// Orientation of the current visualization 2D (in degrees).
        /// </summary>
        private int _orientation;

        /// <summary>
        /// Last image data displayed by window.
        /// </summary>
        private vtkImageData _lastData;
        /// <summary>
        /// Drawing mode enaled/disabled.
        /// </summary>
        public bool DrawingEnabled { get; set; }

        public DrawingInfo DrawingInfo = new DrawingInfo();

        public void RotateImageForward()
        {
            _orientation = _orientation + 90;
            if (_orientation == 360) _orientation = 0;
            RotateImage(RotationOperation.Forward);
        }

        public void RotateImageBack()
        {
            _orientation = _orientation - 90;
            if (_orientation == -90) _orientation = 270;
            RotateImage(RotationOperation.Back);
        }

        private void RotateImage(RotationOperation operation)
        {
            vtkImageReslice reslice = vtkImageReslice.New();
            vtkTransform transform = vtkTransform.New();
            transform.PostMultiply();

            double[] center = { 75, 100, 0 };
            transform.Translate(-center[0], -center[1], -center[2]);
            if (operation == RotationOperation.Forward)
            {
                transform.RotateZ(90);
            }
            else if (operation == RotationOperation.Back)
            {
                transform.RotateZ(-90);
            }
            transform.Translate(+center[0], +center[1], +center[2]);

            transform.Update();
            reslice.SetInput(_viewer.GetInput());
            reslice.SetResliceTransform(transform);
            reslice.Update();

            _viewer.SetInput(reslice.GetOutput());

            UpdateViewer();
        }


        /// <summary>
        /// Update the 2D visualization when the plane moved.
        /// </summary>
        /// <param name="plane">PlaneWidget which changed the coordinates.</param>
        public void PlaneMoved(vtkImagePlaneWidget plane)
        {

            vtkImageReslice reslice = vtkImageReslice.New();
            vtkTransform transform = vtkTransform.New();
            transform.PostMultiply();

            //TODO wyznaczenie centrum okna
            double[] center = {75, 100, 0};
			transform.Translate( -center[0], -center[1], -center[2] );
            transform.RotateZ(_orientation);
			transform.Translate( +center[0], +center[1], +center[2] );

            transform.Update();
            reslice.SetInput(plane.GetResliceOutput());
            reslice.SetResliceTransform(transform);
            reslice.Update();
            //
            vtkImageData viewerInputData;
            viewerInputData = reslice.GetOutput();

            _lastData = viewerInputData;

            _viewer.SetInput(viewerInputData);
            //
            //_viewer.SetInput(reslice.GetOutput());
            UpdateViewer();

        }

        //TODO

        public void DrawingModeRepaint()
        {
            if (_lastData == null)
                return;

            vtkImageReslice reslice = vtkImageReslice.New();
            vtkTransform transform = vtkTransform.New();
            transform.PostMultiply();

            //TODO wyznaczenie centrum okna
            double[] center = { 75, 100, 0 };
            transform.Translate(-center[0], -center[1], -center[2]);
            transform.RotateZ(_orientation);
            transform.Translate(+center[0], +center[1], +center[2]);

            transform.Update();

            reslice.SetInput(_lastData);
            reslice.SetResliceTransform(transform);
            reslice.Update();

            vtkImageData viewerInputData;
            if (!DrawingEnabled)
            {
               viewerInputData = reslice.GetOutput();
            }
            else
            {
                var resliceOutput = reslice.GetOutput();
                var sizeArray = resliceOutput.GetDimensions();

                vtkImageCanvasSource2D imageCanvas = vtkImageCanvasSource2D.New();
                imageCanvas.SetScalarTypeToUnsignedChar();
                imageCanvas.SetExtent(0, sizeArray[0], 0, sizeArray[1], 0, 0);
                imageCanvas.DrawImage(0, 0, resliceOutput);

                DrawingUtils.Draw(imageCanvas, DrawingInfo);
               
                imageCanvas.Update();
                viewerInputData = imageCanvas.GetOutput();
            }
            _viewer.SetInput(viewerInputData);

            UpdateViewer();
        }

        //TODO

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
            _viewer.SetInput(planeWidget.GetResliceOutput());
            planeWidget.Dispose();

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
