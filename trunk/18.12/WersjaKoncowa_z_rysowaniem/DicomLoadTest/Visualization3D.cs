using System.Collections.Generic;
using System.Threading.Tasks;
using Kitware.VTK;
using XMLReaderModule;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System.Drawing.Drawing2D;
using NLog;
using System.Linq;
using ClipingModule;


namespace MainWindow
{
    /// <summary>
    /// Class is responisible for 3D visualization of dicom data and all operations which applies to it.
    /// </summary>
    public class Visualization3D
    {
        /// <summary>
        /// Logger
        /// </summary>
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Render window of visualization 3D.
        /// </summary>
        private readonly RenderWindowControl _window;

        /// <summary>
        /// Render for the 3d view.
        /// </summary>
        private vtkRenderer renderer;

        /// <summary>
        /// Plane with the Axis.X orientation.
        /// </summary>
        public PlaneWidget PlaneWidgetX { get; set; }
        /// <summary>
        /// Plane with the Axis.Y orientation.
        /// </summary>
        public PlaneWidget PlaneWidgetY { get; set; }
        /// <summary>
        /// Plane with the Axis.Z orientation.
        /// </summary>
        public PlaneWidget PlaneWidgetZ { get; set; }

        /// <summary>
        /// Mapper for the 3d view.
        /// </summary>
        private vtkVolumeMapper _mapper;
        /// <summary>
        /// Volume for the 3d view.
        /// </summary>
        private vtkVolume _volume;
        /// <summary>
        /// Dicom input
        /// </summary>
        private DicomLoader _dicomLoader;

        /// <summary>
        /// Module for cutting 3d volume.
        /// </summary>
        private ClipingObject _clipingModule;

        /// <summary>
        /// Number of current serie of opacity function.
        /// </summary>
        private int _currentSerieNumber;

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
                colors.AddRGBPoint(0, 1, 0, 0); //red
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


        /// <summary>
        /// Create vizualization 3D
        /// </summary>
        /// <param name="window">Orginal window component. </param>
        /// <param name="dicomLoader">Dicom input</param>
        public Visualization3D(RenderWindowControl window, DicomLoader dicomLoader)
        {
            _window = window;
            _dicomLoader = dicomLoader;

            // Create a mapper and actor
            _mapper = vtkSmartVolumeMapper.New();
            _mapper.SetInput(_dicomLoader.GetOutput());
            _volume = vtkVolume.New();
            SetOpacityFunction();
            SetGradientOpacity();
            _volume.SetMapper(_mapper);

            renderer = vtkRenderer.New();
            renderer.AddVolume(_volume);
            _window.RenderWindow.GetRenderers().RemoveAllItems();
            _window.RenderWindow.AddRenderer(renderer);
            _window.RenderWindow.GetRenderers().GetFirstRenderer().AddVolume(_volume);


            //Create and setup planes
            PlaneWidgetX = new PlaneWidget(Axis.X);
            SetupPlane(PlaneWidgetX);
            PlaneWidgetY = new PlaneWidget(Axis.Y);
            SetupPlane(PlaneWidgetY);
            PlaneWidgetZ = new PlaneWidget(Axis.Z);
            SetupPlane(PlaneWidgetZ);
          

            //Camera style
            vtkInteractorStyleTrackballCamera style = vtkInteractorStyleTrackballCamera.New();
            style.AutoAdjustCameraClippingRangeOff();
           
            _window.RenderWindow.GetInteractor().SetInteractorStyle(style);
            PlaneWidgetX.SetInteractor(_window.RenderWindow.GetInteractor());
            PlaneWidgetY.SetInteractor(_window.RenderWindow.GetInteractor());
            PlaneWidgetZ.SetInteractor(_window.RenderWindow.GetInteractor());


            // Render
            _window.RenderWindow.Render();

            //ClipingObject
            _clipingModule = new ClipingObject(GetObjectSize());
        }

        /// <summary>
        /// Update 3d view after directory has changed.
        /// </summary>
        /// <param name="dicomLoader">Updated dicom loader.</param>
        public void ChangeDirectory(DicomLoader dicomLoader)
        {
            _dicomLoader = dicomLoader;
            _mapper.Dispose();
            _mapper = vtkSmartVolumeMapper.New();
            _mapper.SetInput(_dicomLoader.GetOutput());
            _mapper.Update();

            _window.RenderWindow.GetRenderers().GetFirstRenderer().RemoveVolume(_volume);
            _volume.Dispose();
            _volume = vtkVolume.New();
            SetOpacityFunction();
            SetGradientOpacity();
            _volume.SetMapper(_mapper);
            _volume.Update();

            _window.RenderWindow.GetRenderers().GetFirstRenderer().AddVolume(_volume);
            _window.RenderWindow.GetRenderers().GetFirstRenderer().Render();

            SetupPlane(PlaneWidgetX);
            SetupPlane(PlaneWidgetY);
            SetupPlane(PlaneWidgetZ);

            // Render
            _window.RenderWindow.Render();
            _window.Validate();
            _window.Update();

            _clipingModule = new ClipingObject(GetObjectSize());

        }

        /// <summary>
        /// Handler for ClipingEvent - reacts for cliping volume and update 3D view
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="args">Event arguments</param>
        public void ExecuteClipingOperation(object sender, ClipingEventArgs args)
        {
            _mapper.SetClippingPlanes(_clipingModule.Clip(args));
            Update3DVisualization();
        }

        /// <summary>
        /// Reads new preset, updating color and opacity function from it. 
        /// </summary>
        /// <param name="presetName">Name of choosen preset.</param>
        public void ChangeColorAndOpacityFunction(PresetInformation PresetInfo)
        {
            vtkColorTransferFunction ctf = vtkColorTransferFunction.New();
            vtkPiecewiseFunction spwf = vtkPiecewiseFunction.New();

            if (PresetInfo != null)
            {
                foreach (var pair in PresetInfo.Series[0].OpacityFunction)
                {
                    spwf.AddPoint(pair.Key, pair.Value);
                }

                foreach (var pair in PresetInfo.Series[0].ColorFuction)
                {
                    ctf.AddRGBSegment(pair.Key, pair.Value[0].R, pair.Value[0].G, pair.Value[0].B,
                        pair.Key, pair.Value[1].R, pair.Value[1].G, pair.Value[1].B);
                }

                ctf.SetScaleToLinear();
                _volume.GetProperty().SetColor(ctf);
                _volume.GetProperty().SetScalarOpacity(spwf);

                _currentSerieNumber = 0;
                Update3DVisualization();
            }
        }

        /// <summary>
        /// Changes the opacity function to another serie from preset.
        /// </summary>
        /// <param name="numberOfSerie">Number of serie to update.</param>
        public void ChangeToSerie(PresetInformation PresetInfo, int numberOfSerie)
        {
            vtkPiecewiseFunction spwf = vtkPiecewiseFunction.New();
            foreach (var pair in PresetInfo.Series[numberOfSerie].OpacityFunction)
            {
                spwf.AddPoint(pair.Key, pair.Value);
            }
            _volume.GetProperty().SetScalarOpacity(spwf);

            _window.Validate();
            _window.Update();
            _window.RenderWindow.Render();

            _currentSerieNumber = numberOfSerie;
        }

        /// <summary>
        /// Updates opacity function, when new points are added. 
        /// </summary>
        /// <param name="splinePoints">List of new points</param>
        public void ChangeSplineFunction(List<DataPoint> splinePoints)
        {
            vtkPiecewiseFunction spwf = vtkPiecewiseFunction.New();

            foreach (DataPoint point in splinePoints)
            {
                spwf.AddPoint(point.XValue, point.YValues[0]);
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

        /// <summary>
        /// Update 3D visualization window
        /// </summary>
        private void Update3DVisualization()
        {
            _window.Validate();
            _window.Update();
            _window.RenderWindow.Render();
        }

        /// <summary>
        /// Returns size of volume in every dimension.
        /// </summary>
        /// <returns>List containing 3 values(for x,y,z axis).</returns>
        public IList<double> GetObjectSize()
        {
            var xyzSize = new List<double> { _volume.GetXRange()[1], _volume.GetYRange()[1], _volume.GetZRange()[1] };
            return xyzSize;
        }

        /// <summary>
        /// Generate strip representing function of color. 
        /// </summary>
        /// <param name="sourceGraphics">Graphics to paint strip on.</param>
        /// <param name="height">Strip height.</param>
        /// <param name="width">Strip width.</param>
        public void GenerateStrip(PresetInformation PresetInfo, Graphics sourceGraphics, int height, int width)
        {
            var opacityFunction = _volume.GetProperty().GetScalarOpacity();

            var colorFunction = _volume.GetProperty().GetRGBTransferFunction();

            double[] rangeArray = opacityFunction.GetRange();
            var min = (float)rangeArray[0];
            var max = (float)rangeArray[1];
            int len = (int)(max - min);
            logger.Warn(rangeArray[1].ToString() + " " + rangeArray[0].ToString());

            int unit = len / width;
            int counter;
            int? prevCounter = null;
            float counterf = 0;

            var colorList = PresetInfo.Series[_currentSerieNumber].ColorFuction.Keys
                .Where(key => key > min & key < max)
                .Select(key => PresetInfo.Series[_currentSerieNumber].ColorFuction[key]).ToList(); 

            var firstColor = Color.FromArgb(255, (int)colorFunction.GetRedValue(min), 
                (int)colorFunction.GetGreenValue(min), (int)colorFunction.GetBlueValue(min));

            var lastColor = Color.FromArgb(255, (int)colorFunction.GetRedValue(max),
               (int)colorFunction.GetGreenValue(max), (int)colorFunction.GetBlueValue(max));

            colorList.Add(new Color[] { lastColor, lastColor });
            colorList.Insert(0, new Color[] { firstColor, firstColor });

            var keyList = PresetInfo.Series[_currentSerieNumber].ColorFuction.Keys.Select(key => key).Where(key => key > min & key < max).ToList();
            keyList.Add(max);
            keyList.Insert(0, min);

            int i = -1;
            foreach(var key in keyList)
            {
                i++;
                if (prevCounter == null)
                {
                    prevCounter = (int)0;
                    counterf = key;
                }
                else
                {
                    counter = (int)((key - counterf) / unit);
                    if (counter > 0)
                    {
                        var colorInfo1 = colorList[i-1][1];
                        var colorInfo2 = colorList[i][0];

                        logger.Warn(counterf.ToString() + " " + key.ToString() + " " + colorInfo1.ToString() + " " + colorInfo2.ToString()); 

                        var rect = new Rectangle(prevCounter.Value, 0, prevCounter.Value + counter, height);

                        using (Brush aGradientBrush = new LinearGradientBrush(rect, colorInfo1, colorInfo2, LinearGradientMode.Horizontal))
                        {
                            sourceGraphics.FillRectangle(aGradientBrush, prevCounter.Value, 0, prevCounter.Value + counter, height);
                        }
                    }
                    prevCounter = prevCounter.Value + counter;
                    counterf = key;
                }
            }
        }

        /// <summary>
        /// Lock/unlock PlaneWidget on specified axis.
        /// </summary>
        /// <param name="axis">axis to lock/unlock</param>
        /// <param name="state">0 to turn off, 1 to turn on</param>
        public void ChangePlaneGadetActivity(Axis axis, int state)
        {
            PlaneWidget widget = null;
            switch (axis)
            {
                case (Axis.X): widget = PlaneWidgetX; break;
                case (Axis.Y): widget = PlaneWidgetY; break;
                case (Axis.Z): widget = PlaneWidgetZ; break;
            }
            if (state == 0)
                widget.InteractionOff();
            if (state == 1)
                widget.InteractionOn();
        }

        /// <summary>
        /// Disposing all elements in visualization 3D.
        /// </summary>
        public bool Dispose()
        {
            if (_volume != null) _volume.Dispose();
            if (_dicomLoader != null) _dicomLoader.Dispose();
            if (_mapper != null) _mapper.Dispose();
            if (PlaneWidgetX != null) PlaneWidgetX.Dispose();
            if (PlaneWidgetY != null) PlaneWidgetY.Dispose();
            if (PlaneWidgetZ != null) PlaneWidgetZ.Dispose();
            if (_window != null) _window.Dispose();

            return true;
        }

    }


    /// <summary>
    /// Class is a wrapper for vtkImagePlaneWidget with axis orientation.
    /// </summary>
    public class PlaneWidget : vtkImagePlaneWidget
    {
        /// <summary>
        /// Axis
        /// </summary>
        public Axis Axis { get; set; }

        /// <summary>
        /// Create plane for the particular axis.
        /// </summary>
        /// <param name="axis">Axis.</param>
        public PlaneWidget(Axis axis)
        {
            Axis = axis;
        }

    }

}

