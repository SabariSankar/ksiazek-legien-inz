using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Kitware.VTK;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
         }

        private void renderWindowControl1_Load(object sender, EventArgs e)
        {
            vtkSphereSource sphere = vtkSphereSource.New();
            sphere.SetThetaResolution(8);
            sphere.SetPhiResolution(16);

            vtkShrinkPolyData shrink = vtkShrinkPolyData.New();
            shrink.SetInputConnection(sphere.GetOutputPort());
            shrink.SetShrinkFactor(0.5);

            vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(shrink.GetOutputPort());

            vtkActor actor = vtkActor.New();
            actor.SetMapper(mapper);
            actor.GetProperty().SetColor(0, 0, 1);

            vtkRenderer renderer = renderWindowControl1
                .RenderWindow.GetRenderers().GetFirstRenderer();
            vtkRenderWindow rendererWindow = renderWindowControl1
                .RenderWindow;

            renderer.AddViewProp(actor); //Actor to specjalizacja Prop
            rendererWindow.SetSize(250, 250);
            rendererWindow.Render();

            vtkCamera camera = renderer.GetActiveCamera();
            camera.Zoom(1.5);

            //do debugu
            //renderWindowControl1.AddTestActors = true;
        }
    }
}
