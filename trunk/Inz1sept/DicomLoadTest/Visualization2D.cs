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
        private vtkImagePlaneWidget planeWidget;

        private float windowWidth = 100;
        private float windowLevel = 100;


        public void sliceY(float slicePosition)
        {
            planeWidget = vtkImagePlaneWidget.New();
            planeWidget.SetInput(dicomReader.GetOutput());
            planeWidget.SetPlaneOrientationToYAxes();
            planeWidget.SetSliceIndex((int) slicePosition);
            planeWidget.SetWindowLevel(this.windowWidth, this.windowLevel, 1);
            viewer.SetInput(planeWidget.GetResliceOutput());
            viewer.SetColorWindow(planeWidget.GetWindow());
            viewer.SetColorLevel(planeWidget.GetLevel());
            viewer.Render();

            window.Update();
            window.RenderWindow.Render();
        }


        public void sliceZ(float slicePosition)
        {
            planeWidget = vtkImagePlaneWidget.New();
            planeWidget.SetInput(dicomReader.GetOutput());
            planeWidget.SetPlaneOrientationToZAxes();
            planeWidget.SetSliceIndex((int)slicePosition);
            planeWidget.SetWindowLevel(this.windowWidth, this.windowLevel, 1);
            viewer.SetInput(planeWidget.GetResliceOutput());
            viewer.SetColorWindow(planeWidget.GetWindow());
            viewer.SetColorLevel(planeWidget.GetLevel());
            viewer.Render();

            window.Update();
            window.RenderWindow.Render();
        }


        public void sliceX(float slicePosition)
        {
            planeWidget = vtkImagePlaneWidget.New();
            planeWidget.SetInput(dicomReader.GetOutput());
            planeWidget.SetPlaneOrientationToXAxes();
            planeWidget.SetSliceIndex((int)slicePosition);
            planeWidget.SetWindowLevel(this.windowWidth, this.windowLevel, 1);
            viewer.SetInput(planeWidget.GetResliceOutput());
            viewer.SetColorWindow(planeWidget.GetWindow());
            viewer.SetColorLevel(planeWidget.GetLevel());
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
