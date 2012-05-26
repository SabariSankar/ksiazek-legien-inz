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
        vtkVolume vol;

        private void renderWindowControl1_Load(object sender, EventArgs e)
        {
            vtkRenderer renderer = renderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer();
            vtkXMLImageDataReader reader = vtkXMLImageDataReader.New();
            vtkFixedPointVolumeRayCastMapper texMapper = vtkFixedPointVolumeRayCastMapper.New();
            vol = vtkVolume.New();
            ctf = vtkColorTransferFunction.New();
            vtkPiecewiseFunction spwf = vtkPiecewiseFunction.New();
            vtkPiecewiseFunction gpwf = vtkPiecewiseFunction.New();

            reader.SetFileName("head.vti");
            reader.Update();

            //Go through the visulizatin pipeline
            texMapper.SetInputConnection(reader.GetOutputPort());

            //Set the color curve for the volume
            ctf.AddHSVPoint(1*x, .67, .07, 1);//1
            ctf.AddHSVPoint(94*x, .67, .07, 1);
            ctf.AddHSVPoint(139*x, 0, 0, 0);
            ctf.AddHSVPoint(160*x, .28, .047, 1);
            ctf.AddHSVPoint(254*x, .38, .013, 1);

            //Set the opacity curve for the volume
            spwf.AddPoint(84, 0);
            spwf.AddPoint(151, .1);
            spwf.AddPoint(255, 1);

            //Set the gradient curve for the volume
            gpwf.AddPoint(0, .2);
            gpwf.AddPoint(10, .2);
            gpwf.AddPoint(25, 1);

            vol.GetProperty().SetColor(ctf);
            vol.GetProperty().SetScalarOpacity(spwf);
            vol.GetProperty().SetGradientOpacity(gpwf);

            vol.SetMapper(texMapper);

            //Go through the Graphics Pipeline
            renderer.AddVolume(vol);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            trackBar1.Minimum = 0;
            trackBar1.Maximum = 256;
            x = trackBar1.Value;
            //prowizoryczne zwiększanie współczymmika przenikalności
            ctf = vtkColorTransferFunction.New();
            ctf.AddHSVPoint(0 + x, .67, .07, 1);//1
            ctf.AddHSVPoint(94 + x, .67, .07, 1);
            ctf.AddHSVPoint(139 + x, 0, 0, 0);
            ctf.AddHSVPoint(160 + x, .28, .047, 1);
            ctf.AddHSVPoint(254 + x, .38, .013, 1);

            vol.GetProperty().SetColor(ctf);
            //

            renderWindowControl1.Validate();
            renderWindowControl1.Update();
        }
    }
}
