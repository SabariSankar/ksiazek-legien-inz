﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kitware.VTK;

namespace DicomLoadTest
{
    public class Visualization3D
    {
        private readonly RenderWindowControl window;
        private vtkColorTransferFunction ctf;
        private vtkPiecewiseFunction spwf;
        private vtkPiecewiseFunction gpwf; 
        private readonly vtkVolume vol;
        private readonly PresetMapper presetMapper;
        private vtkDICOMImageReader dicomReader;

        private float windowWidth = 0;
        private float windowLevel = 40;

        private readonly vtkVolumeMapper _mapper;
        private readonly ClipingModule _clipingModule;

        delegate void MyDlgt();

        public void ChangeColorFunction(String presetName)
        {
            ctf = vtkColorTransferFunction.New();
            Dictionary<int,float[]> values = presetMapper.changeColorFunction(presetName);

            foreach (var pair in values)
            {
                ctf.AddRGBPoint(pair.Key, pair.Value[0], pair.Value[1], pair.Value[2]);
            }

            vol.GetProperty().SetColor(ctf);
        }

        public void ChangeOpacityFunction(String presetName)
        {
            spwf = vtkPiecewiseFunction.New();

            Dictionary<float, float> values = presetMapper.changeOpacityFunction(presetName);

            foreach (var pair in values)
            {
                spwf.AddPoint(pair.Key, pair.Value);
            }

            vol.GetProperty().SetScalarOpacity(spwf);
        }

        private void SetColorFunction()
        {
            ctf = vtkColorTransferFunction.New();

            //Set the color curve for the volume
            ctf.AddRGBPoint(-700, 0, 0, 0);             //powietrze -700
            ctf.AddRGBPoint(-100, .9, .9, .5);       //tluszcz 50 - -100
            ctf.AddRGBPoint(0, .6, .45, .5);          //woda ~0
            ctf.AddRGBPoint(40, 1, 0, 0);             //krew ~40
            ctf.AddRGBPoint(50, 1, .1, .1);           //watroba 40-60
            ctf.AddRGBPoint(37, .4, .4, .3);         //istota szara mozgu 37-45
            ctf.AddRGBPoint(20, 1, 0, 0);               //miesnie 10 - 40
            ctf.AddRGBPoint(1500, 1, 1, 1);           //kosc 1000 - 1500

            vol.GetProperty().SetColor(ctf);
        }

        private void SetOpacityFunction()
        {
            spwf = vtkPiecewiseFunction.New();

            //Set the opacity curve for the volume
            spwf.AddPoint(this.windowLevel - (this.windowWidth / 2), 0);
            spwf.AddPoint(this.windowLevel, 1);
            spwf.AddPoint(this.windowLevel + (this.windowWidth / 2), 0);

            vol.GetProperty().SetScalarOpacity(spwf);
        }

        private void SetGradientOpacity()
        {
            gpwf = vtkPiecewiseFunction.New();

            //Set the gradient curve for the volume
            gpwf.AddPoint(0, .2);
            gpwf.AddPoint(10, .2);
            gpwf.AddPoint(25, 1);


            vol.GetProperty().SetGradientOpacity(gpwf);
        }


        //wizualizacja 3d -----------------------------------------------------------------
        public Visualization3D(RenderWindowControl window, vtkDICOMImageReader dicomReader)
        {
            this.window = window;
            this.dicomReader = dicomReader;
            this.presetMapper = new PresetMapper();

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
           
            this.SetColorFunction();
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

            spwf = vtkPiecewiseFunction.New();
            spwf.AddPoint(this.windowLevel - (this.windowWidth / 2), 0);
            spwf.AddPoint(this.windowLevel, 1);
            spwf.AddPoint(this.windowLevel + (this.windowWidth / 2), 0);
            vol.GetProperty().SetScalarOpacity(spwf);

            window.Validate();
            window.Update();
            window.RenderWindow.Render();
        }

        public void PlaneOperation(object sender, ClipingEventArgs args)
        {
            Console.WriteLine("A");
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
