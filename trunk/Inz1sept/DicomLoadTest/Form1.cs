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
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

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
        String presetDir = @"..\..\presety";

        float windowWidth = 100;
        float windowLevel = 100;
        private DataPoint selectedDataPoint = null;

        event EventHandler<ClipingEventArgs> clipingOperation;

        private ClipingModule _clipingModule;

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
            string[] filePaths = Directory.GetFiles(presetDir, "*.xml");

            foreach (string dir in filePaths)
            {
                comboBox1.Items.Add(new FileInfo(dir).Name);
            }
            comboBox1.SelectedIndex = 0;
            vizualization3D.ChangeColorAndOpacityFunction(String.IsNullOrEmpty(comboBox1.SelectedText) ? comboBox1.Text : comboBox1.SelectedText);

            comboBoxSeries.Items.Clear();
            int numberOfSeries = vizualization3D.PresetInfo.Series.Count;
            for (int i = 0; i < numberOfSeries; i++)
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
            this.firstVizualization2D.sliceToAxes(dicomReader, XtrackBar.Value, "X");
            this.secondVizualization2D.update2DVisualization(this.windowLevel, this.windowWidth);
            this.secondVizualization2D.sliceToAxes(dicomReader, YtrackBar1.Value, "Y");
            this.thirdVizualization2D.update2DVisualization(this.windowLevel, this.windowWidth);
            this.thirdVizualization2D.sliceToAxes(dicomReader, ZtrackBar2.Value, "Z");
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
            firstVizualization2D = new Visualization2D(firstWindow);
            firstVizualization2D.sliceToAxes(dicomReader, 300, "X");
            XtrackBar.Value = 300;
        }


        private void secondWindow_Load(object sender, EventArgs e)
        {
            secondVizualization2D = new Visualization2D(secondWindow);
            secondVizualization2D.sliceToAxes(dicomReader, 100, "Y");
            YtrackBar1.Value = 100;
        }


        private void thirdWindow_Load(object sender, EventArgs e)
        {
            thirdVizualization2D = new Visualization2D(thirdWindow);
            thirdVizualization2D.sliceToAxes(dicomReader, 100, "Z");
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
            vizualization3D.ChangeColorAndOpacityFunction(String.IsNullOrEmpty(comboBox1.SelectedText) ? comboBox1.Text : comboBox1.SelectedText);
            comboBoxSeries.Items.Clear();
            int numberOfSeries = vizualization3D.PresetInfo.Series.Count;
            for (int i = 0; i < numberOfSeries; i++)
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
            firstVizualization2D.sliceToAxes(dicomReader, XtrackBar.Value, "X");
        }

        private void YtrackBar1_Scroll(object sender, EventArgs e)
        {
            secondVizualization2D.sliceToAxes(dicomReader, YtrackBar1.Value, "Y");
        }

        private void ZtrackBar2_Scroll(object sender, EventArgs e)
        {
            thirdVizualization2D.sliceToAxes(this.dicomReader, ZtrackBar2.Value, "Z");
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            /*
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
            clipingOperation(sender, args);
             * */
        }



        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            // Check if data point selected
            if (selectedDataPoint != null)
            {
                // Mouse coordinates should not be outside of the chart 
                int coordinate = e.Y;
                if (coordinate < 0)
                    coordinate = 0;
                if (coordinate > chart1.Size.Height - 1)
                    coordinate = chart1.Size.Height - 1;

                // Calculate new Y value from current cursor position
                double yValue = chart1.ChartAreas["ChartArea1"].AxisY.PixelPositionToValue(coordinate);
                yValue = Math.Min(yValue, chart1.ChartAreas["ChartArea1"].AxisY.Maximum);
                yValue = Math.Max(yValue, chart1.ChartAreas["ChartArea1"].AxisY.Minimum);

                // Update selected point Y value
                selectedDataPoint.YValues[0] = yValue;


                // Invalidate chart
                chart1.Invalidate();

                // Force the chart to redraw
                chart1.Update();
            }
            else
            {

                chart1.Cursor = Cursors.Hand;

            }
        }

        private void chart1_MouseUp(object sender, MouseEventArgs e)
        {
            // Initialize currently selected data point
            if (selectedDataPoint != null)
            {
                // Hide point label
                selectedDataPoint.IsValueShownAsLabel = false;

                // reset selected object
                selectedDataPoint = null;

                // Invalidate chart
                chart1.Invalidate();

            }
        }

        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {
            // Call Hit Test Method
            HitTestResult hitResult = chart1.HitTest(e.X, e.Y);

            // Initialize currently selected data point
            selectedDataPoint = null;
            if (hitResult.ChartElementType == ChartElementType.DataPoint)
            {
                selectedDataPoint = (DataPoint)hitResult.Object;


            }
        }

        private void ClipingToolboxButton_Click(object sender, EventArgs e)
        {
            if (_clipingModule == null)
            {
                _clipingModule = new ClipingModule(vizualization3D);
            }
            ClipingToolboxButton.Text = _clipingModule.ShowToolbox()
                                       ? ButtonText.HideClipingToolbox
                                       : ButtonText.ShowClipingToolbox;
        }




    }
}
