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

        public static float x = 1;
        vtkColorTransferFunction ctf;
        vtkPiecewiseFunction spwf;
        vtkVolume vol;
        float windowWidth = 0;
        float windowLevel = 40;

        private void renderWindowControl1_Load(object sender, EventArgs e)
        {
            vtkRenderer renderer = renderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer();
            
            vtkSmartVolumeMapper mapper = vtkSmartVolumeMapper.New();
            //vtkFixedPointVolumeRayCastMapper texMapper = vtkFixedPointVolumeRayCastMapper.New();
            vol = vtkVolume.New();
            ctf = vtkColorTransferFunction.New();
            spwf = vtkPiecewiseFunction.New();
            vtkPiecewiseFunction gpwf = vtkPiecewiseFunction.New();

            //vtkXMLImageDataReader reader = vtkXMLImageDataReader.New();
            //reader.SetFileName("head.vti");
            //reader.Update();

            //Go through the visulizatin pipeline
            vtkDICOMImageReader dicomReader = vtkDICOMImageReader.New();
            dicomReader.SetDirectoryName("D:\\Downloads\\PANORAMIX");
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

            vol.SetMapper(mapper);

            //Go through the Graphics Pipeline
            renderer.AddVolume(vol);
        }


        private void renderWindowControl4_Load(object sender, EventArgs e)
        {

            vtkDICOMImageReader dicomReader = vtkDICOMImageReader.New();
            dicomReader.SetDirectoryName("D:\\Downloads\\PANORAMIX\\");
            dicomReader.Update();

            //create image viewer

            vtkImageViewer2 viewer = vtkImageViewer2.New();
            viewer.SetInputConnection(dicomReader.GetOutputPort());

        
            viewer.OffScreenRenderingOn(); //dont show window of load images
            int VolData_Images = viewer.GetSliceMax();
            viewer.SetSlice(100); //slice showed

            viewer.Render();
            renderWindowControl4.RenderWindow.AddRenderer(viewer.GetRenderer());

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.windowWidth = float.Parse(comboBox1.SelectedItem.ToString());

            spwf = vtkPiecewiseFunction.New();
            spwf.AddPoint(this.windowLevel - (this.windowWidth / 2), 0);
            spwf.AddPoint(this.windowLevel, 1);
            spwf.AddPoint(this.windowLevel + (this.windowWidth / 2), 0);
            vol.GetProperty().SetScalarOpacity(spwf);

            this.renderWindowControl1.Validate();
            this.renderWindowControl1.Update();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.windowLevel = float.Parse(comboBox2.SelectedItem.ToString());

            spwf = vtkPiecewiseFunction.New();
            spwf.AddPoint(this.windowLevel - (this.windowWidth / 2), 0);
            spwf.AddPoint(this.windowLevel, 1);
            spwf.AddPoint(this.windowLevel + (this.windowWidth / 2), 0);
            vol.GetProperty().SetScalarOpacity(spwf);

            this.renderWindowControl1.Validate();
            this.renderWindowControl1.Update();
        }

  


   
    }
}
