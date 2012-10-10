using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XMLReaderTest;

using Kitware.VTK;

namespace DicomLoadTest
{
    public partial class Form1 : Form
    {
        Visualization3D vizualization3D;
        Visualization2D firstVizualization2D;
        Visualization2D secondVizualization2D;
        Visualization2D thirdVizualization2D;

        vtkDICOMImageReader dicomReader;
        String directoryName = @"D:\Downloads\PANORAMIX";

        float windowWidth = 100;
        float windowLevel = 100;

        event EventHandler<ClipingEventArgs> clipingOperation;

        public Form1()
        {
            InitializeComponent();

            dicomReader = vtkDICOMImageReader.New();
            dicomReader.SetDirectoryName(directoryName);
            dicomReader.Update();

        }

        //wizualizacja 3d -----------------------------------------------------------------
        private void fourthWindow_Load(object sender, EventArgs e)
        {
            vizualization3D = new Visualization3D(fourthWindow, dicomReader, chart1);
            List<String> presetsNames = vizualization3D.PresetReader.Presets;
            foreach (string nameOfPreset in presetsNames)
            {
                comboBox1.Items.Add(nameOfPreset);
            }
            comboBox1.SelectedIndex = 0;
            vizualization3D.ChangeColorAndOpacityFunction(presetsNames[0]);

            comboBoxSeries.Items.Clear();
            int numberOfSeries = vizualization3D.PresetInfo.Series.Count;
            for (int i = 1; i < numberOfSeries; i++)
            {
                comboBoxSeries.Items.Add(i.ToString());
            }
            comboBoxSeries.SelectedIndex = 0;

            //TODO: troche s³abe miejsce
            clipingOperation += vizualization3D.PlaneOperation;
        }

        //updatatuje okno wziualizacji 3d
        private void update3DVisualization()
        {
            vizualization3D.Update3DVisualization(this.windowLevel, this.windowWidth);
        }


        private void update2DVisualization()
        {
            this.firstVizualization2D.update2DVisualization(this.windowLevel, this.windowWidth);
            this.firstVizualization2D.sliceX(XtrackBar.Value);
            this.secondVizualization2D.update2DVisualization(this.windowLevel, this.windowWidth);
            this.secondVizualization2D.sliceY(YtrackBar1.Value);
            this.thirdVizualization2D.update2DVisualization(this.windowLevel, this.windowWidth);
            this.thirdVizualization2D.sliceZ(ZtrackBar2.Value);
        }

        //suwak obsluguje szerokosc ------------------------------------------------------------
        private void trackBarWidth_Scroll(object sender, EventArgs e)
        {

            this.windowWidth = float.Parse(trackBarWidth.Value.ToString());
            this.update2DVisualization();
            this.textBoxWidth.Text = trackBarWidth.Value.ToString();

        }

        //suwak obsluguje level ------------------------------------------------------------
        private void trackBarLevel_Scroll(object sender, EventArgs e)
        {
            this.windowLevel = float.Parse(trackBarLevel.Value.ToString());
            this.update2DVisualization();
            this.textBoxLevel1.Text = trackBarLevel.Value.ToString();
        }


        //wizualizaja 2d
        private void firstWindow_Load(object sender, EventArgs e)
        {
            firstVizualization2D = new Visualization2D(firstWindow, dicomReader);
            firstVizualization2D.sliceX(300);
            XtrackBar.Value = 300;
        }


        private void secondWindow_Load(object sender, EventArgs e)
        {
            secondVizualization2D = new Visualization2D(secondWindow, dicomReader);
            secondVizualization2D.sliceY(100);
            YtrackBar1.Value = 100;
        }


        private void thirdWindow_Load(object sender, EventArgs e)
        {
            thirdVizualization2D = new Visualization2D(thirdWindow, dicomReader);
            thirdVizualization2D.sliceZ(100);
            ZtrackBar2.Value = 100;
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
            vizualization3D.ChangeColorAndOpacityFunction(comboBox1.Text);
            comboBoxSeries.Items.Clear();
            int numberOfSeries = vizualization3D.PresetInfo.Series.Count;
            for (int i = 1; i < numberOfSeries; i++)
            {
                comboBoxSeries.Items.Add(i.ToString());
            }
            comboBoxSeries.SelectedIndex = 0;
            this.update3DVisualization();

        }


        private void comboBoxSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            vizualization3D.ChangeToSerie(int.Parse(comboBoxSeries.Text));
        }

        private void XtrackBar_Scroll(object sender, EventArgs e)
        {
            firstVizualization2D.sliceX(XtrackBar.Value);
        }

        private void YtrackBar1_Scroll(object sender, EventArgs e)
        {
            secondVizualization2D.sliceY(YtrackBar1.Value);
        }

        private void ZtrackBar2_Scroll(object sender, EventArgs e)
        {
            thirdVizualization2D.sliceZ(ZtrackBar2.Value);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            var args = new ClipingEventArgs()
                           {
                               Type = EClipingModuleOperationType.NewPlane,

                               A = numericUpDown1.Value,
                               B = numericUpDown2.Value,
                               C = numericUpDown3.Value,

                               XNormal = numericUpDown4.Value,
                               YNormal = numericUpDown5.Value,
                               ZNormal = numericUpDown6.Value,
                           };
            clipingOperation(sender,args);
        }




    }
}
