using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kitware.VTK;

namespace DicomLoadTest
{
    /// <summary>
    /// Takes care of 2D visualization window.
    /// </summary>
    public class Visualization2D
    {
        private RenderWindowControl window;
        private vtkImageViewer2 viewer;
        private vtkImagePlaneWidget planeWidget;

        private float windowWidth = 100;
        private float windowLevel = 100;

        public void PlaneMoved(vtkImagePlaneWidget plane)
        {
            viewer.SetInput(plane.GetResliceOutput());
            viewer.SetColorWindow(this.windowWidth);
            viewer.SetColorLevel(this.windowLevel);
            viewer.Render();
            window.Update();
            window.RenderWindow.Render();
        }


        /// <summary>
        /// Update the 2D visualization window with new slice of pass X, Y or Z coordination. 
        /// </summary>
        /// <param name="dicomReader"> Dicom input from we are going to cut the slice.</param>
        /// <param name="slicePosition">Coordinates of the slice. </param>
        /// <param name="axis">Name of axis to set cut orientation (X,Y,Z). </param>
        public void sliceToAxes(vtkDICOMImageReader dicomReader, float slicePosition, string axis)
        {
            planeWidget = vtkImagePlaneWidget.New();
            planeWidget.SetInput(dicomReader.GetOutput());
            switch (axis)
            {
                case "X":
                    planeWidget.SetPlaneOrientationToXAxes();
                    break;
                case "Y":
                    planeWidget.SetPlaneOrientationToYAxes();
                    break;
                case "Z":
                    planeWidget.SetPlaneOrientationToZAxes();
                    break;
                default:
                    throw new FormatException("Invalid axis");
            }
            planeWidget.SetSliceIndex((int) slicePosition);
            planeWidget.SetWindowLevel(this.windowWidth, this.windowLevel, 1);
            viewer.SetInput(planeWidget.GetResliceOutput());
            viewer.SetColorWindow(planeWidget.GetWindow());
            viewer.SetColorLevel(planeWidget.GetLevel());
            viewer.Render();

            window.Update();
            window.RenderWindow.Render();
        }


        /// <summary>
        /// Creating the 2D visualization window.
        /// </summary>
        /// <param name="window">Orginal window component. </param>
        public Visualization2D(RenderWindowControl window)
        {
            this.window = window;

            vtkRenderer renderer = window.RenderWindow.GetRenderers().GetFirstRenderer();
            vtkRenderWindowInteractor renderWindowInteractor = window.RenderWindow.GetInteractor();

            viewer = vtkImageViewer2.New();
            viewer.OffScreenRenderingOn();
            viewer.SetupInteractor(renderWindowInteractor);
            viewer.SetRenderer(renderer);
        }

        /// <summary>
        /// Changing the window width and level of the current slice.
        /// </summary>
        /// <param name="windowLevel"> Level of the window.</param>
        /// <param name="windowWidth"> Width of the window.</param>
        public void update2DVisualization(float windowLevel, float windowWidth)
        {
            this.windowLevel = windowLevel;
            this.windowWidth = windowWidth;
            viewer.SetColorWindow(this.windowWidth);
            viewer.SetColorLevel(this.windowLevel);
            viewer.Render();
            window.Update();
            window.RenderWindow.Render();

        }

        /// <summary>
        /// Dispose class objects. 
        /// </summary>
        public bool Dispose()
        {
            if(window != null) window.Dispose();
            if(viewer != null) viewer.Dispose();
            if(planeWidget != null) planeWidget.Dispose();
            return true;
        }
    }
}
