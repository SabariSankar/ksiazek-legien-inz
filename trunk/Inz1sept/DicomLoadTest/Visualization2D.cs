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
        private vtkDICOMImageReader dicomReader;
        private vtkImageViewer2 viewer;
        private vtkImagePlaneWidget widget;

        private float windowWidth = 100;
        private float windowLevel = 100;


        public void sliceY(float slicePosition)
        {
            widget = vtkImagePlaneWidget.New();
            widget.SetInput(dicomReader.GetOutput());
            widget.SetPlaneOrientationToYAxes();
            widget.SetSliceIndex((int) slicePosition);
            widget.SetWindowLevel(this.windowWidth, this.windowLevel, 1);
            viewer.SetInput(widget.GetResliceOutput());
            viewer.Render();

            window.Update();
            window.RenderWindow.Render();
        }


        public void sliceZ(float slicePosition)
        {
            widget = vtkImagePlaneWidget.New();
            widget.SetInput(dicomReader.GetOutput());
            widget.SetPlaneOrientationToZAxes();
            widget.SetSliceIndex((int)slicePosition);
            widget.SetWindowLevel(this.windowWidth, this.windowLevel, 1);
            viewer.SetInput(widget.GetResliceOutput());
            viewer.Render();

            window.Update();
            window.RenderWindow.Render();
        }


        public void sliceX(float slicePosition)
        {
            widget = vtkImagePlaneWidget.New();
            widget.SetInput(dicomReader.GetOutput());
            widget.SetPlaneOrientationToXAxes();
            widget.SetSliceIndex((int)slicePosition);
            widget.SetWindowLevel(this.windowWidth, this.windowLevel, 1);
            viewer.SetInput(widget.GetResliceOutput());
            viewer.Render();

            window.Update();
            window.RenderWindow.Render();

        }


        public Visualization2D(RenderWindowControl window, vtkDICOMImageReader dicomReader)
        {
            this.window = window;
            this.dicomReader = dicomReader;

            vtkRenderer renderer = window.RenderWindow.GetRenderers().GetFirstRenderer();
            vtkRenderWindowInteractor renderWindowInteractor = window.RenderWindow.GetInteractor();

            viewer = vtkImageViewer2.New();
            viewer.OffScreenRenderingOn();
            viewer.SetupInteractor(renderWindowInteractor);
            viewer.SetRenderer(renderer);

        }

        public void update2DVisualization(float windowLevel, float windowWidth)
        {
            this.windowLevel = windowLevel;
            this.windowWidth = windowWidth;

        }

    }
}
