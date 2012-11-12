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
        private vtkRenderWindowInteractor _renderWindowInteractor;

        public PlaneWidget PlaneWidgetX { get; set; }
        public PlaneWidget PlaneWidgetY { get; set; }
        public PlaneWidget PlaneWidgetZ { get; set; }

        private vtkVolumeMapper _mapper;
        private vtkVolume _volume;
        private DicomLoader _dicomLoader;
        private readonly Chart _chart;
        public XmlPresetReader PresetReader { get; set; }
        public PresetInformation PresetInfo { get; set; } 

        private float _windowWidth;
        private float _windowLevel = 40;

        private ClipingModule _clipingModule;

        /// <summary>
        /// Set up the new PlaneWidget.
        /// </summary>
        /// <param name="planeWidget">Plane to set up.</param>
        private void SetupPlane(PlaneWidget planeWidget)
        {
            planeWidget.DisplayTextOff();
            planeWidget.SetInput(_dicomLoader.GetOutput());
            planeWidget.SetPlaneOrientationToXAxes();
            planeWidget.SetSliceIndex(250);
            planeWidget.SetInteractor(_renderWindowInteractor);
            planeWidget.SetLeftButtonAction(99);
            planeWidget.SetRightButtonAction(0);
            planeWidget.SetMarginSizeX(0);
            planeWidget.SetMarginSizeY(0);

            planeWidget.GetMarginProperty().SetColor(1, 0, 0);
            planeWidget.GetSelectedPlaneProperty().SetOpacity(0);
            planeWidget.GetCursorProperty().SetOpacity(0);
            planeWidget.GetPlaneProperty().SetOpacity(0);

            vtkColorTransferFunction colors = vtkColorTransferFunction.New();
            
            if (planeWidget.Axis == Axis.X)
            {
                planeWidget.SetPlaneOrientationToXAxes();
                colors.AddRGBPoint(0, 1, 0, 0);         //red
            }
            else if (planeWidget.Axis == Axis.Y)
            {
                planeWidget.SetPlaneOrientationToYAxes();
                colors.AddRGBPoint(0, 0, 1, 0);
            }
            else if (planeWidget.Axis == Axis.Z)
            {
                planeWidget.SetPlaneOrientationToZAxes();
                colors.AddRGBPoint(0, 0, 0, 1);
            }
            colors.SetAlpha(0.4);
            colors.SetColorSpaceToRGB();
            colors.Build();
            planeWidget.GetColorMap().SetLookupTable(colors);
   
        }

        //wizualizacja 3d -----------------------------------------------------------------
        public Visualization3D(RenderWindowControl window, DicomLoader dicomLoader, Chart chart)
        {
            _chart = chart;
            _window = window;
            _dicomLoader = dicomLoader;
            PresetReader = new XmlPresetReader();

            // Create a mapper and actor
            _mapper = vtkSmartVolumeMapper.New();
            _mapper.SetInput(_dicomLoader.GetOutput());
            _volume = vtkVolume.New();
            SetOpacityFunction();
            SetGradientOpacity();
            _volume.SetMapper(_mapper);

            _window.RenderWindow.GetRenderers().GetFirstRenderer().AddVolume(_volume);


            // An interactor
            _renderWindowInteractor = vtkRenderWindowInteractor.New();
            _renderWindowInteractor.SetRenderWindow(_window.RenderWindow);

            //Camera style
            vtkInteractorStyleTrackballCamera style = vtkInteractorStyleTrackballCamera.New();
            style.AutoAdjustCameraClippingRangeOff();
            _renderWindowInteractor.SetInteractorStyle(style);


            //Create and setup planes
            PlaneWidgetX = new PlaneWidget(Axis.X);
            SetupPlane(PlaneWidgetX);
            PlaneWidgetY = new PlaneWidget(Axis.Y);
            SetupPlane(PlaneWidgetY);
            PlaneWidgetZ = new PlaneWidget(Axis.Z);
            SetupPlane(PlaneWidgetZ);

            // Render
            _window.RenderWindow.Render();

            //ClipingModule
            _clipingModule = new ClipingModule(GetObjectSize());          
        }

        public void ChangeDirectory(DicomLoader dicomLoader)
        {
            _dicomLoader = dicomLoader;
            _mapper.Dispose();
            _mapper = vtkSmartVolumeMapper.New();
            _mapper.SetInput(_dicomLoader.GetOutput());
            //_mapper.Update();

            _window.RenderWindow.GetRenderers().GetFirstRenderer().RemoveVolume(_volume);
            _volume.Dispose();
            _volume = vtkVolume.New();
            SetOpacityFunction();
            SetGradientOpacity();
            _volume.SetMapper(_mapper);
            //_volume.Update();

            _window.RenderWindow.GetRenderers().GetFirstRenderer().AddVolume(_volume);
            //_window.RenderWindow.GetRenderers().GetFirstRenderer().UseDepthPeelingOff();
            _window.RenderWindow.GetRenderers().GetFirstRenderer().Render();

            SetupPlane(PlaneWidgetX);
            SetupPlane(PlaneWidgetY);
            SetupPlane(PlaneWidgetZ);

            // Render
            _window.RenderWindow.Render();
            _window.Validate();
            _window.Update();

            

            _clipingModule = new ClipingModule(GetObjectSize());     
            
        }

        public void ExecuteClipingOperation(object sender, ClipingEventArgs args)
        {
            _mapper.SetClippingPlanes(_clipingModule.GenerateNewPlaneCollection(args));
            Update3DVisualization();
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
        public void ChangeSplineFunction(List<DataPoint> splinePoints)
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

        public void ChangeSplineAndPointFunction(List<DataPoint> splinePoints )
        {
            vtkPiecewiseFunction spwf = vtkPiecewiseFunction.New();
            _chart.Series["OpacityFunctionSpline"].Points.Clear();
            _chart.Series["OpacityFunction"].Points.Clear();

            foreach (DataPoint point in splinePoints)
            {
                spwf.AddPoint(point.XValue, point.YValues[0]);
                _chart.Series["OpacityFunctionSpline"].Points.AddXY(point.XValue, point.YValues[0]);
                _chart.Series["OpacityFunction"].Points.AddXY(point.XValue, point.YValues[0]);
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
            if(_mapper != null) _mapper.Dispose();
            if (_renderWindowInteractor != null) _renderWindowInteractor.Dispose();
            if (PlaneWidgetX != null) PlaneWidgetX.Dispose();
            if (PlaneWidgetY != null) PlaneWidgetY.Dispose();
            if (PlaneWidgetZ != null) PlaneWidgetZ.Dispose();
            if(_window != null) _window.Dispose();
        
            return true;
        }

     
   
    }


    public class PlaneWidget : vtkImagePlaneWidget
    {
        public Axis Axis { get; set; }

        public PlaneWidget(Axis axis)
        {
            Axis = axis;
        }


    }


}

