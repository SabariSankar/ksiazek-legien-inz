using System.Collections.Generic;
using System.Threading.Tasks;
using Kitware.VTK;
using XMLReaderTest;
using System.Windows.Forms.DataVisualization.Charting;

namespace MainWindow
{
    /// <summary>
    /// Class is responisible for 3D visualization of dicom data and all operations which applies to it.
    /// </summary>
    public class Visualization3D
    {
        /// <summary>
        /// Render window of visualization 3D.
        /// </summary>
        private readonly RenderWindowControl _window;
        /// <summary>
        /// Window interation of visualization 3D.
        /// </summary>
        private readonly vtkRenderWindowInteractor _renderWindowInteractor;

        public vtkImagePlaneWidget PlaneWidgetX { get; set; }
        public vtkImagePlaneWidget PlaneWidgetY { get; set; }
        public vtkImagePlaneWidget PlaneWidgetZ { get; set; }

        public vtkVolumeMapper Mapper { get; private set; }
        private readonly vtkVolume _volume;
        private readonly DicomLoader _dicomLoader;
        private readonly Chart _chart;
        public XmlPresetReader PresetReader { get; set; }
        public PresetInformation PresetInfo { get; set; } 

        private float _windowWidth;
        private float _windowLevel = 40;

        private readonly ClipingModule _clipingModule;
        public delegate void MyDlgt();


        /// <summary>
        /// Set up the new plane.
        /// </summary>
        /// <param name="plane">Plane to set up.</param>
        private void SetupPlane(vtkImagePlaneWidget plane)
        {
            plane.DisplayTextOn();
            plane.SetInput(_dicomLoader.GetOutput());
            plane.SetSliceIndex(250);
            plane.SetInteractor(_renderWindowInteractor);
   
        }

        //wizualizacja 3d -----------------------------------------------------------------
        public Visualization3D(RenderWindowControl window, DicomLoader dicomLoader, Chart chart)
        {
            _chart = chart;
            _window = window;
            _dicomLoader = dicomLoader;
            PresetReader = new XmlPresetReader();

            // Create a mapper and actor
            Mapper = vtkSmartVolumeMapper.New();
            Mapper.SetInput(dicomLoader.GetOutput());
            _volume = vtkVolume.New();
            SetOpacityFunction();
            SetGradientOpacity();
            _volume.SetMapper(Mapper);

            vtkRenderer renderer = window.RenderWindow.GetRenderers().GetFirstRenderer();
            renderer.AddVolume(_volume);

            // An interactor
            _renderWindowInteractor = vtkRenderWindowInteractor.New();
            _renderWindowInteractor.SetRenderWindow(window.RenderWindow);

            //Camera style
            vtkInteractorStyleTrackballCamera style = vtkInteractorStyleTrackballCamera.New();
            _renderWindowInteractor.SetInteractorStyle(style);

            //Create and setup planes
            PlaneWidgetX = vtkImagePlaneWidget.New();
            SetupPlane(PlaneWidgetX);
            PlaneWidgetX.SetPlaneOrientationToXAxes();


            PlaneWidgetY = vtkImagePlaneWidget.New();
            SetupPlane(PlaneWidgetY);
            PlaneWidgetY.SetPlaneOrientationToYAxes();

            
            PlaneWidgetZ = vtkImagePlaneWidget.New();
            SetupPlane(PlaneWidgetZ);
            PlaneWidgetZ.SetPlaneOrientationToZAxes();

            // Render
            window.RenderWindow.Render();

        }

        /// <summary>
        /// Reads new preset, updating color and opacity function from it. 
        /// </summary>
        /// <param name="presetName">Name of choosen preset.</param>
        public void ChangeColorAndOpacityFunction(string presetName)
        {
            vtkColorTransferFunction ctf = vtkColorTransferFunction.New();
            vtkPiecewiseFunction spwf = vtkPiecewiseFunction.New();

            PresetInfo = PresetReader.ReadXmlFile(presetName);
            _chart.Series["OpacityFunction"].Points.Clear();
            _chart.Series["OpacityFunctionSpline"].Points.Clear();

            foreach (var pair in PresetInfo.Series[0].OpacityFunction)
            {
                spwf.AddPoint(pair.Key, pair.Value);
                _chart.Series["OpacityFunction"].Points.AddXY(pair.Key, pair.Value);
                _chart.Series["OpacityFunctionSpline"].Points.AddXY(pair.Key, pair.Value);
            }

            foreach (var pair in PresetInfo.Series[0].ColorFuction)
            {
                ctf.AddRGBSegment(pair.Key, pair.Value[0].R, pair.Value[0].G, pair.Value[0].B,
                    pair.Key, pair.Value[1].R, pair.Value[1].G, pair.Value[1].B);
                //Color colorRight = Color.FromArgb((int)pair.Value[1].R, (int)pair.Value[1].G, (int)pair.Value[1].B);
                //Color colorLeft = Color.FromArgb((int)pair.Value[0].R, (int)pair.Value[0].G, (int)pair.Value[0].B);
            }

            ctf.SetScaleToLinear();
            _volume.GetProperty().SetColor(ctf);
            _volume.GetProperty().SetScalarOpacity(spwf);
        }

        /// <summary>
        /// Changes the opacity function to another serie from preset.
        /// </summary>
        /// <param name="numberOfSerie">Number of serie to update.</param>
        public void ChangeToSerie(int numberOfSerie)
        {
            vtkPiecewiseFunction spwf = vtkPiecewiseFunction.New();
            _chart.Series["OpacityFunction"].Points.Clear();
            _chart.Series["OpacityFunctionSpline"].Points.Clear();

            foreach (var pair in PresetInfo.Series[numberOfSerie].OpacityFunction)
            {
                spwf.AddPoint(pair.Key, pair.Value);
                _chart.Series["OpacityFunction"].Points.AddXY(pair.Key, pair.Value);
                _chart.Series["OpacityFunctionSpline"].Points.AddXY(pair.Key, pair.Value);
            }
            _volume.GetProperty().SetScalarOpacity(spwf);

            _window.Validate();
            _window.Update();
            _window.RenderWindow.Render();
        }

        /// <summary>
        /// Updates opacity function, when new points are added. 
        /// </summary>
        /// <param name="splinePoints">List of new points</param>
        public void ChangeSerie(List<DataPoint> splinePoints)
        {
            vtkPiecewiseFunction spwf = vtkPiecewiseFunction.New();
            _chart.Series["OpacityFunctionSpline"].Points.Clear();

            foreach (DataPoint point in splinePoints)
            {
                spwf.AddPoint(point.XValue, point.YValues[0]);
                _chart.Series["OpacityFunctionSpline"].Points.AddXY(point.XValue, point.YValues[0]);
            }
            _volume.GetProperty().SetScalarOpacity(spwf);

            _window.Validate();
            _window.Update();
            _window.RenderWindow.Render();
        }

        /// <summary>
        /// Set the opacity function based on current window level and width.
        /// </summary>
        private void SetOpacityFunction()
        {
            vtkPiecewiseFunction spwf = vtkPiecewiseFunction.New();

            //Set the opacity curve for the volume
            spwf.AddPoint(_windowLevel - (_windowWidth / 2), 0);
            spwf.AddPoint(_windowLevel, 1);
            spwf.AddPoint(_windowLevel + (_windowWidth / 2), 0);

            _volume.GetProperty().SetScalarOpacity(spwf);
        }

        /// <summary>
        /// Set the gradient function for the volume.
        /// </summary>
        private void SetGradientOpacity()
        {
            vtkPiecewiseFunction gpwf = vtkPiecewiseFunction.New();

            //Set the gradient curve for the volume
            gpwf.AddPoint(0, .2);
            gpwf.AddPoint(10, .2);
            gpwf.AddPoint(25, 1);

            _volume.GetProperty().SetGradientOpacity(gpwf);
        }

        //updatatuje okno wziualizacji 3d
        public void Update3DVisualization()
        {
            _window.Validate();
            _window.Update();
            _window.RenderWindow.Render();
        }

        public void Update3DVisualization(float windowLevel, float windowWidth)
        {
            _windowLevel = windowLevel;
            _windowWidth = windowWidth;

            SetOpacityFunction();

            _window.Validate();
            _window.Update();
            _window.RenderWindow.Render();
        }

        public void PlaneOperation(object sender, ClipingEventArgs args)
        {
            var task = new Task(() => _clipingModule.ExecuteClipingOperation(args));
            task.ContinueWith(x => _window.Invoke(
                new MyDlgt(() =>
                               {
                                   _window.Validate();
                                   _window.Update();
                                   _window.RenderWindow.Render();
                               }
                    )));
            task.Start();
        }

        public IList<double> GetObjectSize()
        {
            var xyzSize = new List<double> {_volume.GetXRange()[1], _volume.GetYRange()[1], _volume.GetZRange()[1]};
            return xyzSize;
        }

        /// <summary>
        /// Disposing all elements in visualization 3D.
        /// </summary>
        public bool Dispose()
        {
            if(_volume != null) _volume.Dispose();
            if(_dicomLoader != null) _dicomLoader.Dispose();
            if(Mapper != null) Mapper.Dispose();
            if (_renderWindowInteractor != null) _renderWindowInteractor.Dispose();
            if (PlaneWidgetX != null) PlaneWidgetX.Dispose();
            if (PlaneWidgetY != null) PlaneWidgetY.Dispose();
            if (PlaneWidgetZ != null) PlaneWidgetZ.Dispose();
            if(_window != null) _window.Dispose();
        
            return true;
        }

     
   
    }
}
