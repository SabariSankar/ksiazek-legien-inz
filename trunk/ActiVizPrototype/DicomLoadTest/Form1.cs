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
        Visualization3D vizualization3D;
        Visualization2D firstVizualization2D;
        Visualization2D secondVizualization2D;
        Visualization2D thirdVizualization2D;

        String directoryName = "D:\\Downloads\\PANORAMIX\\";

        float windowWidth = 0;
        float windowLevel = 40;


        public Form1()
        {
            InitializeComponent();
        }

        //wizualizacja 3d -----------------------------------------------------------------
        private void fourthWindow_Load(object sender, EventArgs e)
        {
            vtkDICOMImageReader dicomReader = vtkDICOMImageReader.New();
            dicomReader.SetDirectoryName(directoryName);
            dicomReader.Update();

            vizualization3D = new Visualization3D(fourthWindow,dicomReader);

            PresetMapper presets = new PresetMapper();
            foreach (string nameOfPreset in presets.Presets)
            {
                comboBox1.Items.Add(nameOfPreset);
            }
            comboBox1.SelectedText = presets.Presets[0];
        }

        //updatatuje okno wziualizacji 3d
        private void update3DVisualization()
        {
            vizualization3D.update3DVisualization(this.windowLevel,this.windowWidth);
        }


        private void update2DVisualization()
        {
            this.firstVizualization2D.update2DVisualization(this.windowLevel,this.windowWidth);
            this.secondVizualization2D.update2DVisualization(this.windowLevel,this.windowWidth);
            this.thirdVizualization2D.update2DVisualization(this.windowLevel,this.windowWidth);
        }

        //suwak obsluguje szerokosc ------------------------------------------------------------
        private void trackBarWidth_Scroll(object sender, EventArgs e)
        {

            this.windowWidth = float.Parse(trackBarWidth.Value.ToString());
            this.update3DVisualization();
            this.update2DVisualization();
            this.textBoxWidth.Text = trackBarWidth.Value.ToString();

        }

        //suwak obsluguje level ------------------------------------------------------------
        private void trackBarLevel_Scroll(object sender, EventArgs e)
        {
            this.windowLevel = float.Parse(trackBarLevel.Value.ToString());
            this.update3DVisualization();
            this.update2DVisualization();
            this.textBoxLevel1.Text = trackBarLevel.Value.ToString();
        }


        //wizualizaja 2d
        private void firstWindow_Load(object sender, EventArgs e)
        {
            firstVizualization2D = new Visualization2D(firstWindow,directoryName);
        }


        private void secondWindow_Load(object sender, EventArgs e)
        {
            secondVizualization2D = new Visualization2D(firstWindow, directoryName);
        }


        private void thirdWindow_Load(object sender, EventArgs e)
        {
            thirdVizualization2D = new Visualization2D(secondWindow, directoryName);
        }

        //zmiany textboxow
        private void textBoxWidth_TextChanged(object sender, EventArgs e)
        {
            int x = int.Parse(textBoxWidth.Text);
            this.trackBarWidth.Value = x;
            this.windowWidth = float.Parse(textBoxWidth.Text);

            this.update3DVisualization();
        }

        private void textBoxLevel_TextChanged(object sender, EventArgs e)
        {
            int x = int.Parse(textBoxLevel1.Text);
            this.trackBarLevel.Value = x;
            this.windowLevel = float.Parse(textBoxLevel1.Text);

            this.update3DVisualization();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            vizualization3D.changeColorFunction(comboBox1.Text);
            vizualization3D.changeOpacityFunction(comboBox1.Text);

            this.update3DVisualization();

        }


    }
}
