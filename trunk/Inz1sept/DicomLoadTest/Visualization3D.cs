using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kitware.VTK;
using XMLReaderTest;
using System.Windows.Forms.DataVisualization.Charting;

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
             
            foreach (var pair in this.PresetInfo.Series[0].ColorFuction)
            {
                ctf.AddRGBSegment(pair.Key, pair.Value[0].Red, pair.Value[0].Green, pair.Value[0].Blue,
                    pair.Key, pair.Value[1].Red, pair.Value[1].Green, pair.Value[1].Blue);
                System.Drawing.Color colorLeft = System.Drawing.Color.FromArgb((int)pair.Value[0].Red, (int)pair.Value[0].Green, (int)pair.Value[0].Blue);
                System.Drawing.Color colorRight= System.Drawing.Color.FromArgb((int)pair.Value[1].Red, (int)pair.Value[1].Green, (int)pair.Value[1].Blue);
                
               // chart1.Series["ColorFunction"].Points.AddXY(pair.Key, 0);
               // chart1.Series["ColorFunction"].MarkerColor = colorLeft;
               // chart1.Series["ColorFunction"].Points.AddXY(pair.Key, 0);
               // chart1.Series["ColorFunction"].MarkerColor = colorRight;
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
