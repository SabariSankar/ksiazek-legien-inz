using System;
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
        private readonly vtkVolume vol;
        private vtkDICOMImageReader dicomReader;
        private Chart chart1;
        public XMLPresetReader PresetReader { get; set; }
        public PresetInformation PresetInfo { get; set; } 

        private float windowWidth = 0;
        private float windowLevel = 40;

        private readonly vtkVolumeMapper _mapper;
        private readonly ClipingModule _clipingModule;

        delegate void MyDlgt();

        public void ChangeColorAndOpacityFunction(string presetName)
        {
            vtkColorTransferFunction ctf = vtkColorTransferFunction.New();
            vtkPiecewiseFunction spwf = vtkPiecewiseFunction.New();

            this.PresetInfo = this.PresetReader.ReadXMLFile(presetName);
            chart1.Series["OpacityFunction"].Points.Clear();
            foreach (var pair in this.PresetInfo.Series[0].OpacityFunction)
            {
                spwf.AddPoint(pair.Key, pair.Value);
                chart1.Series["OpacityFunction"].Points.AddXY(pair.Key, pair.Value);
            }

            float lastOpacity = -100;
            foreach (var pair in this.PresetInfo.Series[0].ColorFuction)
            {
                ctf.AddRGBSegment(pair.Key, pair.Value[0].R, pair.Value[0].G, pair.Value[0].B,
                    pair.Key, pair.Value[1].R, pair.Value[1].G, pair.Value[1].B);
                Color colorLeft = Color.FromArgb((int)pair.Value[0].R, (int)pair.Value[0].G, (int)pair.Value[0].B);
                Color colorRight= Color.FromArgb((int)pair.Value[1].R, (int)pair.Value[1].G, (int)pair.Value[1].B);

                Series series = new Series();
                series.ChartType = SeriesChartType.Line;
                series.BorderColor = colorLeft;
                series.Points.AddXY(lastOpacity, 0.2);
                series.BorderWidth = 10;
                series.BorderDashStyle = ChartDashStyle.Solid;
                series.Points.AddXY(pair.Key, 0.2);
                lastOpacity = pair.Key;
                chart1.Series.Add(series);
               
            }

            vol.GetProperty().SetColor(ctf);
            vol.GetProperty().SetScalarOpacity(spwf);
        }

        public void ChangeToSerie(int numberOfSerie)
        {
            vtkPiecewiseFunction spwf = vtkPiecewiseFunction.New();
            chart1.Series["OpacityFunction"].Points.Clear();
            
            foreach (var pair in this.PresetInfo.Series[numberOfSerie].OpacityFunction)
            {
                spwf.AddPoint(pair.Key, pair.Value);
                chart1.Series["OpacityFunction"].Points.AddXY(pair.Key, pair.Value);
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


        //wizualizacja 3d -----------------------------------------------------------------
        public Visualization3D(RenderWindowControl window, vtkDICOMImageReader dicomReader , Chart chart1)
        {
            this.chart1 = chart1;
            this.window = window;
            this.dicomReader = dicomReader;
            this.PresetReader = new XMLPresetReader();

            vtkRenderer renderer = window.RenderWindow.GetRenderers().GetFirstRenderer();

            _mapper = vtkSmartVolumeMapper.New();
            vol = vtkVolume.New();

            _clipingModule = new ClipingModule(_mapper);

            vtkLookupTable bwLut =vtkLookupTable.New();
            bwLut.SetTableRange (0, 2000);
            bwLut.SetSaturationRange (0, 0);
            bwLut.SetHueRange (0, 0);
            bwLut.SetValueRange (0, 1);
            bwLut.Build(); //effective built 

            vtkImageMapToColors sagittalColors =vtkImageMapToColors.New();
            sagittalColors.SetInputConnection(dicomReader.GetOutputPort());
            sagittalColors.SetLookupTable(bwLut);
            sagittalColors.Update();
            vtkImageActor sagittal = vtkImageActor.New();
            sagittal.SetInput(sagittalColors.GetOutput());
            sagittal.SetDisplayExtent(117,117,0,173,1,180);
            
            vtkImageReslice reslicer = vtkImageReslice.New();
            reslicer.SetResliceAxesDirectionCosines(1,0,0,2,0,0,0,0,0);
 
            _mapper.SetInputConnection(dicomReader.GetOutputPort());
           
            this.SetOpacityFunction();
            this.SetGradientOpacity();

            vol.SetMapper(_mapper);

            renderer.AddActor(sagittal);
            renderer.AddVolume(vol);
        }


        //updatatuje okno wziualizacji 3d
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



   
    }
}
