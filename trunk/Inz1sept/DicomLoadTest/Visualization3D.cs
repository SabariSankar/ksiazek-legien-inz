using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kitware.VTK;
using XMLReaderTest;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace DicomLoadTest
{
    public class Visualization3D
    {
        private readonly RenderWindowControl window;
        private vtkRenderWindow renderWindow;
        private vtkRenderWindowInteractor renderWindowInteractor;
        private vtkImagePlaneWidget planeWidgetX;
        private vtkImagePlaneWidget planeWidgetY;
        private vtkImagePlaneWidget planeWidgetZ;

        private readonly vtkVolume vol;
        private vtkDICOMImageReader dicomReader;
        private Chart chart1;
        public XMLPresetReader PresetReader { get; set; }
        public PresetInformation PresetInfo { get; set; } 

        private float windowWidth = 0;
        private float windowLevel = 40;

        public vtkVolumeMapper Mapper { get; private set; }
        private readonly ClipingModule _clipingModule;

        delegate void MyDlgt();

        //wizualizacja 3d -----------------------------------------------------------------
        public Visualization3D(RenderWindowControl window, vtkDICOMImageReader dicomReader, Chart chart1)
        {
            this.chart1 = chart1;
            this.window = window;
            this.dicomReader = dicomReader;
            this.PresetReader = new XMLPresetReader();

            // Create a mapper and actor
            Mapper = vtkSmartVolumeMapper.New();
            Mapper.SetInputConnection(dicomReader.GetOutputPort());
            vol = vtkVolume.New();
            this.SetOpacityFunction();
            this.SetGradientOpacity();
            vol.SetMapper(Mapper);

            // A renderer and render window
            //vtkRenderer renderer = vtkRenderer.New();
            //renderWindow = vtkRenderWindow.New();
            //vtkRenderer renderer = window.RenderWindow.GetRenderers().GetFirstRenderer();

            vtkRenderer renderer = window.RenderWindow.GetRenderers().GetFirstRenderer();
            renderer.AddVolume(vol);

            // An interactor
            renderWindowInteractor = vtkRenderWindowInteractor.New();
            renderWindowInteractor.SetRenderWindow(window.RenderWindow);

            vtkInteractorStyleTrackballCamera style = vtkInteractorStyleTrackballCamera.New();
            renderWindowInteractor.SetInteractorStyle(style);

            vtkCellPicker picker = vtkCellPicker.New();
            picker.SetTolerance(0.005);

            planeWidgetX = vtkImagePlaneWidget.New();
            planeWidgetX.DisplayTextOn();
            planeWidgetX.SetInput(dicomReader.GetOutput());
            planeWidgetX.SetPlaneOrientationToXAxes();
            planeWidgetX.SetSliceIndex(250);
            planeWidgetX.SetInteractor(renderWindowInteractor);

            planeWidgetY = vtkImagePlaneWidget.New();
            planeWidgetY.DisplayTextOn();
            planeWidgetY.SetInput(dicomReader.GetOutput());
            planeWidgetY.SetPlaneOrientationToYAxes();
            planeWidgetY.SetSliceIndex(100);
            planeWidgetY.SetInteractor(renderWindowInteractor);


            planeWidgetZ = vtkImagePlaneWidget.New();
            planeWidgetZ.DisplayTextOn();
            planeWidgetZ.SetInput(dicomReader.GetOutput());
            planeWidgetZ.SetPlaneOrientationToZAxes();
            planeWidgetZ.SetSliceIndex(250);
            planeWidgetZ.SetInteractor(renderWindowInteractor);

            // Render
            window.RenderWindow.Render();

            //renderWindowInteractor.Initialize();
            window.RenderWindow.Render();
            // Begin mouse interaction
            //renderWindowInteractor.Start();
        }

        public void ChangeColorAndOpacityFunction(string presetName)
        {
            vtkColorTransferFunction ctf = vtkColorTransferFunction.New();
            vtkPiecewiseFunction spwf = vtkPiecewiseFunction.New();

            this.PresetInfo = this.PresetReader.ReadXMLFile(presetName);
            chart1.Series["OpacityFunction"].Points.Clear();
            chart1.Series["OpacityFunctionSpline"].Points.Clear();
            foreach (var pair in this.PresetInfo.Series[0].OpacityFunction)
            {
                spwf.AddPoint(pair.Key, pair.Value);
                chart1.Series["OpacityFunction"].Points.AddXY(pair.Key, pair.Value);
                chart1.Series["OpacityFunctionSpline"].Points.AddXY(pair.Key, pair.Value);
            }

            //float lastOpacity = -100;
            foreach (var pair in this.PresetInfo.Series[0].ColorFuction)
            {
                ctf.AddRGBSegment(pair.Key, pair.Value[0].R, pair.Value[0].G, pair.Value[0].B,
                    pair.Key, pair.Value[1].R, pair.Value[1].G, pair.Value[1].B);
                Color colorLeft = Color.FromArgb((int)pair.Value[0].R, (int)pair.Value[0].G, (int)pair.Value[0].B);
                Color colorRight = Color.FromArgb((int)pair.Value[1].R, (int)pair.Value[1].G, (int)pair.Value[1].B);

                //Series series = new Series();
                //series.ChartType = SeriesChartType.Line;
                //series.BorderColor = colorLeft;
                //series.Points.AddXY(lastOpacity, 0.2);
                //series.BorderWidth = 10;
                //series.BorderDashStyle = ChartDashStyle.Solid;
                //series.Points.AddXY(pair.Key, 0.2);
                //lastOpacity = pair.Key;
                //chart1.Series.Add(series);

            }

            vol.GetProperty().SetColor(ctf);
            vol.GetProperty().SetScalarOpacity(spwf);
        }

        public void ChangeToSerie(int numberOfSerie)
        {
            vtkPiecewiseFunction spwf = vtkPiecewiseFunction.New();
            chart1.Series["OpacityFunction"].Points.Clear();
            chart1.Series["OpacityFunctionSpline"].Points.Clear();

            foreach (var pair in this.PresetInfo.Series[numberOfSerie].OpacityFunction)
            {
                spwf.AddPoint(pair.Key, pair.Value);
                chart1.Series["OpacityFunction"].Points.AddXY(pair.Key, pair.Value);
                chart1.Series["OpacityFunctionSpline"].Points.AddXY(pair.Key, pair.Value);
            }
            vol.GetProperty().SetScalarOpacity(spwf);

            window.Validate();
            window.Update();
            window.RenderWindow.Render();
        }

        public void ChangeSerie(List<DataPoint> splinePoints)
        {
            vtkPiecewiseFunction spwf = vtkPiecewiseFunction.New();
            chart1.Series["OpacityFunctionSpline"].Points.Clear();

            foreach (DataPoint point in splinePoints)
            {
                spwf.AddPoint(point.XValue, point.YValues[0]);
                chart1.Series["OpacityFunctionSpline"].Points.AddXY(point.XValue, point.YValues[0]);
            }
            vol.GetProperty().SetScalarOpacity(spwf);

            window.Validate();
            window.Update();
            window.RenderWindow.Render();
        }


        private void SetOpacityFunction()
        {
            vtkPiecewiseFunction spwf = vtkPiecewiseFunction.New();

            //Set the opacity curve for the volume
            spwf.AddPoint(this.windowLevel - (this.windowWidth / 2), 0);
            spwf.AddPoint(this.windowLevel, 1);
            spwf.AddPoint(this.windowLevel + (this.windowWidth / 2), 0);

            vol.GetProperty().SetScalarOpacity(spwf);
        }

        private void SetGradientOpacity()
        {
            vtkPiecewiseFunction gpwf = vtkPiecewiseFunction.New();

            //Set the gradient curve for the volume
            gpwf.AddPoint(0, .2);
            gpwf.AddPoint(10, .2);
            gpwf.AddPoint(25, 1);

            vol.GetProperty().SetGradientOpacity(gpwf);
        }

        //updatatuje okno wziualizacji 3d
        public void Update3DVisualization()
        {
            Update3DVisualization(windowLevel, windowWidth);
        }

        public void Update3DVisualization(float windowLevel, float windowWidth)
        {
            this.windowLevel = windowLevel;
            this.windowWidth = windowWidth;

            //this.SetOpacityFunction();

            window.Validate();
            window.Update();
            window.RenderWindow.Render();
        }

        public void PlaneOperation(object sender, ClipingEventArgs args)
        {
            var task = new Task(() => _clipingModule.ExecuteClipingOperation(args));
            task.ContinueWith(x => window.Invoke(
                new MyDlgt(() =>
                               {
                                   window.Validate();
                                   window.Update();
                                   window.RenderWindow.Render();
                               }
                    )));
            task.Start();
        }

        public IList<double> GetObjectSize()
        {
            var xyzSize = new List<double>();
            xyzSize.Add(vol.GetXRange()[1]);
            xyzSize.Add(vol.GetYRange()[1]);
            xyzSize.Add(vol.GetZRange()[1]);
            return xyzSize;
        }

        public bool Dispose()
        {
            if(vol != null) vol.Dispose();
            if(dicomReader != null) dicomReader.Dispose();
            if(Mapper != null) Mapper.Dispose();
            if (renderWindowInteractor != null) renderWindowInteractor.Dispose();
            if (planeWidgetX != null) planeWidgetX.Dispose();
            if (planeWidgetY != null) planeWidgetY.Dispose();
            if (planeWidgetZ != null) planeWidgetZ.Dispose();
            if (renderWindow != null) renderWindow.Dispose();
            if(window != null) window.Dispose();
        
            return true;
        }

        //metody obsługujące pojawianie się i chowanie poszczególnych płaszczyzn
        public void ShowPlaneX()
        {
            planeWidgetX.On();
        }

        public void ShowPlaneY()
        {
            planeWidgetY.On();
        }

        public void ShowPlaneZ()
        {
            planeWidgetZ.On();
        }

        public void HidePlaneX()
        {
            planeWidgetX.Off();
        }
        public void HidePlaneY()
        {
            planeWidgetY.Off();
        }
        public void HidePlaneZ()
        {
            planeWidgetZ.Off();
        }
   
    }
}
