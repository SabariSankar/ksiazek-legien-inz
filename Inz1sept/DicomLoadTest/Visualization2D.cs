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
        private RenderWindowControl _window;
        private vtkImageViewer2 _viewer;
        private vtkImagePlaneWidget planeWidget;

        private float windowWidth = 100;
        private float windowLevel = 100;

        public void PlaneMoved(vtkImagePlaneWidget plane)
        {
            _viewer.SetInput(plane.GetResliceOutput());
            _viewer.SetColorWindow(this.windowWidth);
            _viewer.SetColorLevel(this.windowLevel);
            _viewer.Render();
            _window.Update();
            _window.RenderWindow.Render();
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
            planeWidget.SetSliceIndex((int)slicePosition);
            planeWidget.SetWindowLevel(this.windowWidth, this.windowLevel, 1);
            _viewer.SetInput(planeWidget.GetResliceOutput());
            _viewer.SetColorWindow(planeWidget.GetWindow());
            _viewer.SetColorLevel(planeWidget.GetLevel());
            
            _viewer.Render();

            _window.Update();
            _window.RenderWindow.Render();
        }


        /// <summary>
        /// Creating the 2D visualization window.
        /// </summary>
        /// <param name="window">Orginal window component. </param>
        public Visualization2D(RenderWindowControl window)
        {
            _window = window;

            vtkRenderer renderer = window.RenderWindow.GetRenderers().GetFirstRenderer();
            vtkRenderWindowInteractor renderWindowInteractor = window.RenderWindow.GetInteractor();
         
            _viewer = vtkImageViewer2.New();
            _viewer.OffScreenRenderingOn();
            _viewer.SetupInteractor(renderWindowInteractor);
            _viewer.SetRenderer(renderer);
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
            _viewer.SetColorWindow(this.windowWidth);
            _viewer.SetColorLevel(this.windowLevel);
            _viewer.Render();
            _window.Update();
            _window.RenderWindow.Render();

        }

        /// <summary>
        /// Dispose class objects. 
        /// </summary>
        public bool Dispose()
        {
            if (_window != null) _window.Dispose();
            if (_viewer != null) _viewer.Dispose();
            if (planeWidget != null) planeWidget.Dispose();
            return true;
        }
    }
}
