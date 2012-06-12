using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Kitware.VTK;

namespace DicomLoadTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        String directoryName = "D:\\Downloads\\PANORAMIX\\";

        vtkColorTransferFunction ctf;
        vtkPiecewiseFunction spwf;
        vtkVolume vol;
        float windowWidth = 0;
        float windowLevel = 40;

        vtkImageViewer2 viewer1;    // viewer for first renderWindowControl - x
        vtkImageViewer2 viewer2;    // viewer for second renderWindowControl - y
        vtkImageViewer2 viewer3;    // viewer for third renderWindowControl - z


        //wizualizacja 3d -----------------------------------------------------------------
        private void fourthWindow_Load(object sender, EventArgs e)
        {
            vtkRenderer renderer = fourthWindow.RenderWindow.GetRenderers().GetFirstRenderer();
            
            vtkSmartVolumeMapper mapper = vtkSmartVolumeMapper.New();
            vol = vtkVolume.New();
            ctf = vtkColorTransferFunction.New();
            spwf = vtkPiecewiseFunction.New();
            vtkPiecewiseFunction gpwf = vtkPiecewiseFunction.New();

            //Go through the visulizatin pipeline
            vtkDICOMImageReader dicomReader = vtkDICOMImageReader.New();
            dicomReader.SetDirectoryName(directoryName);
            dicomReader.Update();

            mapper.SetInputConnection(dicomReader.GetOutputPort());

            //Set the color curve for the volume
            ctf.AddRGBPoint(-100, .9, .9, .5);       //tluszcz 50 - -100
            ctf.AddRGBPoint(0, .6, .45, .5);          //woda ~0
            ctf.AddRGBPoint(40, 1, 0, 0);             //krew ~40
            ctf.AddRGBPoint(50, 1, .1, .1);           //watroba 40-60
            ctf.AddRGBPoint(37, .4, .4, .3);         //istota szara mozgu 37-45
            //istota biala mozgu 20 - 30
            //miazsz nerki ~30
            //plyn mozgowo-rdzenowy 10-15
            ctf.AddRGBPoint(20, 1, 0, 0);         //miesnie 10 - 40
            ctf.AddRGBPoint(1500, 1, 1,1);           //kosc 1000 - 1500

            //Set the opacity curve for the volume
            spwf.AddPoint(this.windowLevel - (this.windowWidth / 2), 0);
            spwf.AddPoint(this.windowLevel, 1);
            spwf.AddPoint(this.windowLevel + (this.windowWidth / 2), 0);


            //Set the gradient curve for the volume
            gpwf.AddPoint(0, .2);
            gpwf.AddPoint(10, .2);
            gpwf.AddPoint(25, 1);

            vol.GetProperty().SetColor(ctf);
            vol.GetProperty().SetScalarOpacity(spwf);
            vol.GetProperty().SetGradientOpacity(gpwf);

                            //http://informatyka.umcs.lublin.pl/files/siczek.pdf

            vol.SetMapper(mapper);

            //Go through the Graphics Pipeline
            renderer.AddVolume(vol);
        }

        //updatatuje okno wziualizacji 3d
        private void update3DVisualization()
        {

            spwf = vtkPiecewiseFunction.New();
            spwf.AddPoint(this.windowLevel - (this.windowWidth / 2), 0);
            spwf.AddPoint(this.windowLevel, 1);
            spwf.AddPoint(this.windowLevel + (this.windowWidth / 2), 0);
            vol.GetProperty().SetScalarOpacity(spwf);

            this.fourthWindow.Validate();
            this.fourthWindow.Update();
        }

        //suwak obsluguje szerokosc ------------------------------------------------------------
        private void trackBarWidth_Scroll(object sender, EventArgs e)
        {

            this.windowWidth = float.Parse(trackBarWidth.Value.ToString());
            this.update3DVisualization();
            this.textBoxWidth.Text = trackBarWidth.Value.ToString();

        }

        //suwak obsluguje level ------------------------------------------------------------
        private void trackBarLevel_Scroll(object sender, EventArgs e)
        {
            this.windowLevel = float.Parse(trackBarLevel.Value.ToString());
            this.update3DVisualization();
            this.textBoxLevel1.Text = trackBarLevel.Value.ToString();
        }


        //wizualizaja 2d
        private void firstWindow_Load(object sender, EventArgs e)
        {

            vtkDICOMImageReader dicomReader = vtkDICOMImageReader.New();
            dicomReader.SetDirectoryName(directoryName);
            dicomReader.Update();

            //create image viewer
            viewer1 = vtkImageViewer2.New();
            viewer1.SetInputConnection(dicomReader.GetOutputPort());

            viewer1.OffScreenRenderingOn();
            int max = viewer1.GetSliceMax();
            viewer1.SetSlice(100);
            trackBarFirst.SetRange(0, max);

            viewer1.Render();
            firstWindow.RenderWindow.AddRenderer(viewer1.GetRenderer());

        }


        private void secondWindow_Load(object sender, EventArgs e)
        {
            vtkDICOMImageReader dicomReader = vtkDICOMImageReader.New();
            dicomReader.SetDirectoryName(directoryName);
            dicomReader.Update();

            //create image viewer
            viewer2 = vtkImageViewer2.New();
            viewer2.SetInputConnection(dicomReader.GetOutputPort());


            viewer2.OffScreenRenderingOn();
            int max = viewer2.GetSliceMax();
            viewer2.SetSlice(50);
            trackBarSecond.SetRange(0, max);

            viewer2.Render();
            secondWindow.RenderWindow.AddRenderer(viewer2.GetRenderer());
        }


        private void thirdWindow_Load(object sender, EventArgs e)
        {
            vtkDICOMImageReader dicomReader = vtkDICOMImageReader.New();
            dicomReader.SetDirectoryName(directoryName);
            dicomReader.Update();

            //create image viewer
            viewer3 = vtkImageViewer2.New();
            viewer3.SetInputConnection(dicomReader.GetOutputPort());


            viewer3.OffScreenRenderingOn();
            int max = viewer3.GetSliceMax();
            viewer3.SetSlice(150);
            trackBarThird.SetRange(0, max);

            viewer3.Render();
            thirdWindow.RenderWindow.AddRenderer(viewer3.GetRenderer());
        }


        //suwaki obslugujace 2d  -----------------------------------------------------
        private void trackBarFirst_Scroll(object sender, EventArgs e)
        {
            int slice = int.Parse(trackBarFirst.Value.ToString());
            viewer1.SetSlice(slice);
            viewer1.Render();

            this.firstWindow.Validate();
            this.firstWindow.Update();
        }

        private void trackBarSecond_Scroll(object sender, EventArgs e)
        {
            int slice = int.Parse(trackBarSecond.Value.ToString());
            viewer2.SetSlice(slice);
            viewer2.Render();

            this.secondWindow.Validate();
            this.secondWindow.Update();
        }

        private void trackBarThird_Scroll(object sender, EventArgs e)
        {
            int slice = int.Parse(trackBarThird.Value.ToString());
            viewer3.SetSlice(slice);
            viewer3.Render();

            this.thirdWindow.Validate();
            this.thirdWindow.Update();
        }

        private void textBoxWidth_TextChanged(object sender, EventArgs e)
        {
            int x = int.Parse(textBoxWidth.Text.ToString());
            this.trackBarWidth.Value = x;
            this.windowWidth = float.Parse(textBoxWidth.Text.ToString());

            this.update3DVisualization();
        }

        private void textBoxLevel_TextChanged(object sender, EventArgs e)
        {
            int x = int.Parse(textBoxLevel1.Text.ToString());
            this.trackBarLevel.Value = x;
            this.windowLevel = float.Parse(textBoxLevel1.Text.ToString());

            this.update3DVisualization();
        }


    }
}
