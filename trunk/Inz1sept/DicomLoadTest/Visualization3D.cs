using System.Collections.Generic;
using System.Threading.Tasks;
using Kitware.VTK;
using XMLReaderTest;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace MainWindow
{
    public class Visualization3D
    {
        private readonly RenderWindowControl _window;
        private readonly vtkRenderWindowInteractor _renderWindowInteractor;

        public vtkImagePlaneWidget PlaneWidgetX { get; set; }
        public vtkImagePlaneWidget PlaneWidgetY { get; set; }
        public vtkImagePlaneWidget PlaneWidgetZ { get; set; }

        public vtkVolumeMapper Mapper { get; private set; }
        private readonly vtkVolume _volume;
        private readonly DicomLoader _dicomLoader;
        private readonly Chart _chart;
        public XMLPresetReader PresetReader { get; set; }
        public PresetInformation PresetInfo { get; set; } 

        private float _windowWidth = 0;
        private float _windowLevel = 40;

        private readonly ClipingModule _clipingModule;
        public delegate void MyDlgt();


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
            this._chart = chart;
            this._window = window;
            this._dicomLoader = dicomLoader;
            this.PresetReader = new XMLPresetReader();

            // Create a mapper and actor
            Mapper = vtkSmartVolumeMapper.New();
            Mapper.SetInput(dicomLoader.GetOutput());
            _volume = vtkVolume.New();
            this.SetOpacityFunction();
            this.SetGradientOpacity();
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
            this.SetupPlane(PlaneWidgetX);
            PlaneWidgetX.SetPlaneOrientationToXAxes();


            PlaneWidgetY = vtkImagePlaneWidget.New();
            this.SetupPlane(PlaneWidgetY);
            PlaneWidgetY.SetPlaneOrientationToYAxes();

            
            PlaneWidgetZ = vtkImagePlaneWidget.New();
            this.SetupPlane(PlaneWidgetZ);
            PlaneWidgetZ.SetPlaneOrientationToZAxes();

            // Render
            window.RenderWindow.Render();

        }

        public void ChangeColorAndOpacityFunction(string presetName)
        {
            vtkColorTransferFunction ctf = vtkColorTransferFunction.New();
            vtkPiecewiseFunction spwf = vtkPiecewiseFunction.New();

            this.PresetInfo = this.PresetReader.ReadXMLFile(presetName);
            _chart.Series["OpacityFunction"].Points.Clear();
            _chart.Series["OpacityFunctionSpline"].Points.Clear();

            foreach (var pair in this.PresetInfo.Series[0].OpacityFunction)
            {
                spwf.AddPoint(pair.Key, pair.Value);
                _chart.Series["OpacityFunction"].Points.AddXY(pair.Key, pair.Value);
                _chart.Series["OpacityFunctionSpline"].Points.AddXY(pair.Key, pair.Value);
            }

            //float lastOpacity = -100;
            foreach (var pair in this.PresetInfo.Series[0].ColorFuction)
            {
                ctf.AddRGBSegment(pair.Key, pair.Value[0].R, pair.Value[0].G, pair.Value[0].B,
                    pair.Key, pair.Value[1].R, pair.Value[1].G, pair.Value[1].B);
                Color colorLeft = Color.FromArgb((int)pair.Value[0].R, (int)pair.Value[0].G, (int)pair.Value[0].B);
                Color colorRight = Color.FromArgb((int)pair.Value[1].R, (int)pair.Value[1].G, (int)pair.Value[1].B);
            }

            _volume.GetProperty().SetColor(ctf);
            _volume.GetProperty().SetScalarOpacity(spwf);
        }

        public void ChangeToSerie(int numberOfSerie)
        {
            vtkPiecewiseFunction spwf = vtkPiecewiseFunction.New();
            _chart.Series["OpacityFunction"].Points.Clear();
            _chart.Series["OpacityFunctionSpline"].Points.Clear();

            foreach (var pair in this.PresetInfo.Series[numberOfSerie].OpacityFunction)
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


        private void SetOpacityFunction()
        {
            vtkPiecewiseFunction spwf = vtkPiecewiseFunction.New();

            //Set the opacity curve for the volume
            spwf.AddPoint(this._windowLevel - (this._windowWidth / 2), 0);
            spwf.AddPoint(this._windowLevel, 1);
            spwf.AddPoint(this._windowLevel + (this._windowWidth / 2), 0);

            _volume.GetProperty().SetScalarOpacity(spwf);
        }

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
            this._windowLevel = windowLevel;
            this._windowWidth = windowWidth;

            this.SetOpacityFunction();

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
            var xyzSize = new List<double>();
            xyzSize.Add(_volume.GetXRange()[1]);
            xyzSize.Add(_volume.GetYRange()[1]);
            xyzSize.Add(_volume.GetZRange()[1]);
            return xyzSize;
        }

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
