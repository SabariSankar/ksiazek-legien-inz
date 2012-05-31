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
            ctf.AddHSVPoint(1*x, .67, .07, 1);//1
            ctf.AddHSVPoint(94*x, .67, .07, 1);
            ctf.AddHSVPoint(139*x, 0, 0, 0);
            ctf.AddHSVPoint(160*x, .28, .047, 1);
            ctf.AddHSVPoint(254*x, .38, .013, 1);

            //Set the opacity curve for the volume
            spwf.AddPoint(84, 0); //84
            spwf.AddPoint(151, .1); //151
            spwf.AddPoint(255, 1);

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

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            trackBar1.Minimum = 0;
            trackBar1.Maximum = 150;
            x = trackBar1.Value;


            spwf = vtkPiecewiseFunction.New();
            /*
            ctf.AddHSVPoint(0 + x, .67, .07, 1);//1
            ctf.AddHSVPoint(94 + x, .67, .07, 1);
            ctf.AddHSVPoint(139 + x, 0, 0, 0);
            ctf.AddHSVPoint(160 + x, .28, .047, 1);
            ctf.AddHSVPoint(254 + x, .38, .013, 1);
            */

            ctf.AddRGBPoint(0 + x , 0, 0, 0);
            ctf.AddRGBPoint(94 + x, .6, .7, 1);
            ctf.AddRGBPoint(139 + x, 0, .9, .2);
            ctf.AddRGBPoint(160 + x, .4, .47, .5);
            ctf.AddRGBPoint(254 + x, .3, .13, .1);

            vol.GetProperty().SetColor(ctf);
 
            spwf.AddPoint(84 + x, 0); //84
            spwf.AddPoint(151 + x, .1); //151
            spwf.AddPoint(255 + x, 1);
            vol.GetProperty().SetScalarOpacity(spwf);


            renderWindowControl1.Validate();
            renderWindowControl1.Update();
        }
    }
}
