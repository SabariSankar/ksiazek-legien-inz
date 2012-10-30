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


        private readonly vtkDICOMImageReader _dicomReader;
        String directoryName = @"D:\Downloads\PANORAMIX";// @"C:\Users\Grzegorz\Downloads\Chest\JW\tmp";//@"D:\Downloads\PANORAMIX";
        String presetDir = @"..\..\presety";

        float windowWidth = 100;
        float windowLevel = 100;
        private DataPoint selectedDataPoint = null;

        event EventHandler<ClipingEventArgs> clipingOperation;

        private ClipingModule _clipingModule;

        public Form1()
        {
            InitializeComponent();

            _dicomReader = vtkDICOMImageReader.New();
            _dicomReader.SetDirectoryName(directoryName);
            _dicomReader.Update();

        }

        public void DisposeAll(object sender, FormClosingEventArgs e)
        {
            vizualization3D.Dispose();
            firstVizualization2D.Dispose();
            secondVizualization2D.Dispose();
            thirdVizualization2D.Dispose();
        }

        public void sth()
        {
        }

        //-------------------------------------------------------------------------
        //callback function for moving plane
        public void planeXMoved(vtkObject sender, vtkObjectEventArgs e)
        {
            firstVizualization2D.PlaneMoved(vtkImagePlaneWidget.SafeDownCast(sender));
        }

        public void planeYMoved(vtkObject sender, vtkObjectEventArgs e)
        {
            secondVizualization2D.PlaneMoved(vtkImagePlaneWidget.SafeDownCast(sender));
        }

        public void planeZMoved(vtkObject sender, vtkObjectEventArgs e)
        {
            thirdVizualization2D.PlaneMoved(vtkImagePlaneWidget.SafeDownCast(sender));
        }



        //wizualizacja 3d -----------------------------------------------------------------
        private void fourthWindow_Load(object sender, EventArgs e)
        {
            vizualization3D = new Visualization3D(fourthWindow, _dicomReader, chart1);
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

            //moving planes
            vizualization3D.PlaneWidgetX.InteractionEvt += new vtkObject.vtkObjectEventHandler(planeXMoved);
            vizualization3D.PlaneWidgetY.InteractionEvt += new vtkObject.vtkObjectEventHandler(planeYMoved);
            vizualization3D.PlaneWidgetZ.InteractionEvt += new vtkObject.vtkObjectEventHandler(planeZMoved);

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
            this.secondVizualization2D.update2DVisualization(this.windowLevel, this.windowWidth);
            this.thirdVizualization2D.update2DVisualization(this.windowLevel, this.windowWidth);
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
            firstVizualization2D.sliceToAxes(_dicomReader, 300, "X");
        }


        private void secondWindow_Load(object sender, EventArgs e)
        {
            secondVizualization2D = new Visualization2D(secondWindow);
            secondVizualization2D.sliceToAxes(_dicomReader, 100, "Y");
        }


        private void thirdWindow_Load(object sender, EventArgs e)
        {
            thirdVizualization2D = new Visualization2D(thirdWindow);
            thirdVizualization2D.sliceToAxes(_dicomReader, 100, "Z");
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

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            if (selectedDataPoint != null)
            {
                // Mouse coordinates should not be outside of the chart 
                int coordinate = e.Y;
                if (coordinate <= 0)
                    coordinate = 0;
                if (coordinate >= chart1.Size.Height - 1)
                    coordinate = chart1.Size.Height - 1;

                // Calculate new Y value from current cursor position
                double yValue = chart1.ChartAreas["ChartArea1"].AxisY.PixelPositionToValue(coordinate);
                yValue = Math.Min(yValue, chart1.ChartAreas["ChartArea1"].AxisY.Maximum);
                yValue = Math.Max(yValue, chart1.ChartAreas["ChartArea1"].AxisY.Minimum);

                // Update selected point Y value
                selectedDataPoint.YValues[0] = yValue;

                chart1.Invalidate();
                chart1.Update();
            }
            else
            {

                chart1.Cursor = Cursors.Hand;

            }
        }

        private void chart1_MouseUp(object sender, MouseEventArgs e)
        {
            if (selectedDataPoint != null)
            {
                List<DataPoint> splinePoints = chart1.Series["OpacityFunctionSpline"].Points.ToList<DataPoint>();
                splinePoints.Find(x => x.XValue == selectedDataPoint.XValue).YValues[0] = selectedDataPoint.YValues[0];

                vizualization3D.ChangeSerie(splinePoints);
                selectedDataPoint = null;

                chart1.Invalidate();
            }
        }

        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {
            HitTestResult hitResult = chart1.HitTest(e.X, e.Y);

            selectedDataPoint = null;
            if (hitResult.ChartElementType == ChartElementType.DataPoint)
            {
                DataPoint selected = (DataPoint)hitResult.Object;
                //if (chart1.Series["OpacityFunction"].Points.Any<DataPoint>(x => x.XValue >= selected.XValue - 2 && x.XValue <= selected.XValue + 2))
                if (chart1.Series["OpacityFunction"].Points.Contains(selected))
                {
                    selectedDataPoint = selected;
            }
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

        //-------------------------------------------------------------------------------------
        //obs³uga pojawiania i znikania poszczególnych p³aszczyzn
        private void PlaneXButton_Click(object sender, EventArgs e)
        {
            if (PlaneXButton.Text.Equals("Show PlaneX"))
            {
                this.vizualization3D.PlaneWidgetX.On();
                PlaneXButton.Text = "Hide PlaneX";
            }
            else
            {
                this.vizualization3D.PlaneWidgetX.Off();
                PlaneXButton.Text = "Show PlaneX";
            }
        }

        private void PlaneYButton_Click(object sender, EventArgs e)
        {
            if (PlaneYButton.Text.Equals("Show PlaneY"))
            {
                this.vizualization3D.PlaneWidgetY.On();
                PlaneYButton.Text = "Hide PlaneY";
            }
            else
            {
                this.vizualization3D.PlaneWidgetY.Off();
                PlaneYButton.Text = "Show PlaneY";
            }
        }

        private void PlaneZButton_Click(object sender, EventArgs e)
        {
            if (PlaneZButton.Text.Equals("Show PlaneZ"))
            {
                this.vizualization3D.PlaneWidgetZ.On();
                PlaneZButton.Text = "Hide PlaneZ";
            }
            else
            {
                this.vizualization3D.PlaneWidgetZ.Off();
                PlaneZButton.Text = "Show PlaneZ";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var bmp = firstVizualization2D.GetImage();
            var form = new Form2(_dicomReader);
            form.Visible = true;
        }
    }
}
