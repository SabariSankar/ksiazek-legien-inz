using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kitware.VTK;

namespace DicomLoadTest
{
    public class Visualization2D
    {
        private RenderWindowControl window;
        private vtkColorTransferFunction ctf;
        private vtkPiecewiseFunction spwf;
        private vtkVolume vol;

        private float windowWidth = 0;
        private float windowLevel = 40;


        public Visualization2D(RenderWindowControl window, String directoryName)
        {
            this.window = window;
         
            vtkDICOMImageReader dicomReader = vtkDICOMImageReader.New();
            dicomReader.SetFileName(directoryName + "IM-0001-0050.dcm");
            dicomReader.Update();

            vtkRenderer renderer = window.RenderWindow.GetRenderers().GetFirstRenderer();

            vtkSmartVolumeMapper mapper = vtkSmartVolumeMapper.New();
            vol = vtkVolume.New();
            spwf = vtkPiecewiseFunction.New();
            vtkPiecewiseFunction gpwf = vtkPiecewiseFunction.New();

            mapper.SetInputConnection(dicomReader.GetOutputPort());

            //Set the opacity curve for the volume
            spwf.AddPoint(this.windowLevel - (this.windowWidth / 2), 1);
            spwf.AddPoint(this.windowLevel, 0);
            spwf.AddPoint(this.windowLevel + (this.windowWidth / 2), 1);


            //Set the gradient curve for the volume
            gpwf.AddPoint(0, .2);
            gpwf.AddPoint(10, .2);
            gpwf.AddPoint(25, 1);

            vol.GetProperty().SetScalarOpacity(spwf);
            vol.GetProperty().SetGradientOpacity(gpwf);

            vol.SetMapper(mapper);

            //Go through the Graphics Pipeline
            renderer.AddVolume(vol);
        }

        public void update2DVisualization(float windowLevel, float windowWidth)
        {
            this.windowLevel = windowLevel;
            this.windowWidth = windowWidth;

            spwf = vtkPiecewiseFunction.New();
            spwf.AddPoint(this.windowLevel - (this.windowWidth / 2), 1);
            spwf.AddPoint(this.windowLevel, 0);
            spwf.AddPoint(this.windowLevel + (this.windowWidth / 2), 1);
            vol.GetProperty().SetScalarOpacity(spwf);

            this.window.Validate();
            this.window.Update();
            this.window.RenderWindow.Render();

        }

    }
}
