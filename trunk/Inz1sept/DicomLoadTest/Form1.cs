using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Kitware.VTK;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace MainWindow
{
    public partial class Form1 : Form
    {
        private Visualization3D _vizualization3D;
        private Visualization2D _firstVizualization2D;
        private Visualization2D _secondVizualization2D;
        private Visualization2D _thirdVizualization2D;

        private readonly DicomLoader _dicomLoader;
        private String _directoryPath = @"D:\Downloads\PANORAMIX"; //"D:\\DICOM\\GOUDURIX\\GOUDURIX\\tmp";
        private const String PresetDir = @"..\..\presety";

        private DataPoint _selectedDataPoint;

        public Form1()
        {
            InitializeComponent();
            _dicomLoader = new DicomLoader(_directoryPath);
        }

        public void DisposeAll(object sender, FormClosingEventArgs e)
        {
            _vizualization3D.Dispose();
            _firstVizualization2D.Dispose();
            _secondVizualization2D.Dispose();
            _thirdVizualization2D.Dispose();
            _dicomLoader.Dispose();
        }


        #region Obsluga przesuwania poszczegolnych plaszczyzn
        //-------------------------------------------------------------------------
        //callback function for moving PlaneWidget
        public void PlaneXMoved(vtkObject sender, vtkObjectEventArgs e)
        {
            _firstVizualization2D.PlaneMoved(vtkImagePlaneWidget.SafeDownCast(sender));
        }

        public void PlaneYMoved(vtkObject sender, vtkObjectEventArgs e)
        {
            _secondVizualization2D.PlaneMoved(vtkImagePlaneWidget.SafeDownCast(sender));
        }

        public void PlaneZMoved(vtkObject sender, vtkObjectEventArgs e)
        {
            _thirdVizualization2D.PlaneMoved(vtkImagePlaneWidget.SafeDownCast(sender));
        }
        #endregion


        //wizualizacja 3d -----------------------------------------------------------------
        private void fourthWindow_Load(object sender, EventArgs e)
        {
            _vizualization3D = new Visualization3D(fourthWindow, _dicomLoader, chart1);
            string[] filePaths = Directory.GetFiles(PresetDir, "*.xml");

            foreach (string dir in filePaths)
            {
                comboBox1.Items.Add(new FileInfo(dir).Name);
            }
            comboBox1.SelectedIndex = 1;
            _vizualization3D.ChangeColorAndOpacityFunction(String.IsNullOrEmpty(comboBox1.SelectedText) ? comboBox1.Text : comboBox1.SelectedText);

            comboBoxSeries.Items.Clear();
            int numberOfSeries = _vizualization3D.PresetInfo.Series.Count;
            for (int i = 0; i < numberOfSeries; i++)
            {
                comboBoxSeries.Items.Add(i.ToString(CultureInfo.InvariantCulture));
            }
            comboBoxSeries.SelectedIndex = 0;

            //moving planes
            _vizualization3D.PlaneWidgetX.InteractionEvt += PlaneXMoved;
            _vizualization3D.PlaneWidgetY.InteractionEvt += PlaneYMoved;
            _vizualization3D.PlaneWidgetZ.InteractionEvt += PlaneZMoved;

            //handling events from ClipingToolbox
            clipingPanel.ClipingOperationEventHandlerDelegate += new EventHandler<ClipingEventArgs>(_vizualization3D.ExecuteClipingOperation);
            clipingPanel.InitialiseClipingToolbox(_vizualization3D.GetObjectSize());
        }

        //updatatuje okno wziualizacji 3d
        private void update3DVisualization(float windowLevel, float windowWidth)
        {
            _vizualization3D.Update3DVisualization(windowLevel, windowWidth);
        }


        private void update2DVisualization(float windowLevel, float windowWidth)
        {
            _firstVizualization2D.Update2DVisualization(windowLevel, windowWidth);
            _secondVizualization2D.Update2DVisualization(windowLevel, windowWidth);
            _thirdVizualization2D.Update2DVisualization(windowLevel, windowWidth);
        }

  

        //wizualizaja 2d
        private void firstWindow_Load(object sender, EventArgs e)
        {
            _firstVizualization2D = new Visualization2D(firstWindow);
            //_firstVizualization2D.SliceToAxes(_dicomLoader, 300, Axis.X);
        }


        private void secondWindow_Load(object sender, EventArgs e)
        {
            _secondVizualization2D = new Visualization2D(secondWindow);
            // _secondVizualization2D.SliceToAxes(_dicomLoader, 100, Axis.Y);
        }


        private void thirdWindow_Load(object sender, EventArgs e)
        {
            _thirdVizualization2D = new Visualization2D(thirdWindow);
            //_thirdVizualization2D.SliceToAxes(_dicomLoader, 100, Axis.Z);
        }

        #region Obsluga levelu okna i jego szerokosci
        //suwak obsluguje szerokosc ------------------------------------------------------------
        private void trackBarWidth_Scroll(object sender, EventArgs e)
        {
            float windowLevel = float.Parse(trackBarLevel.Value.ToString(CultureInfo.InvariantCulture));
            float windowWidth = float.Parse(trackBarWidth.Value.ToString(CultureInfo.InvariantCulture));
            update2DVisualization(windowLevel, windowWidth);
            textBoxWidth.Text = trackBarWidth.Value.ToString(CultureInfo.InvariantCulture);

        }

        //suwak obsluguje level ------------------------------------------------------------
        private void trackBarLevel_Scroll(object sender, EventArgs e)
        {
            float windowLevel = float.Parse(trackBarLevel.Value.ToString(CultureInfo.InvariantCulture));
            float windowWidth = float.Parse(trackBarWidth.Value.ToString(CultureInfo.InvariantCulture));
            update2DVisualization(windowLevel, windowWidth);
            textBoxLevel1.Text = trackBarLevel.Value.ToString();
        }

        //zmiany textboxow
        private void textBoxWidth_TextChanged(object sender, EventArgs e)
        {
            int x = int.Parse(textBoxWidth.Text);
            trackBarWidth.Value = x;
        }

        private void textBoxLevel_TextChanged(object sender, EventArgs e)
        {
            int x = int.Parse(textBoxLevel1.Text);
            trackBarLevel.Value = x;
        }
        #endregion

        #region Obsluga presetow

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _vizualization3D.ChangeColorAndOpacityFunction(String.IsNullOrEmpty(comboBox1.SelectedText) ? comboBox1.Text : comboBox1.SelectedText);
            comboBoxSeries.Items.Clear();
            int numberOfSeries = _vizualization3D.PresetInfo.Series.Count;
            for (var i = 0; i < numberOfSeries; i++)
            {
                comboBoxSeries.Items.Add(i.ToString(CultureInfo.InvariantCulture));
            }
            comboBoxSeries.SelectedIndex = 0;
            _vizualization3D.Update3DVisualization();

        }


        private void comboBoxSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            _vizualization3D.ChangeToSerie(int.Parse(comboBoxSeries.Text));
        }

        #endregion

        #region Obsluga wykresu
        //-------------------------------------------------------------------------------------
        //Obsługa wykresu

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_selectedDataPoint != null)
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
                _selectedDataPoint.YValues[0] = yValue;

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
            if (_selectedDataPoint != null)
            {
                List<DataPoint> splinePoints = chart1.Series["OpacityFunctionSpline"].Points.ToList<DataPoint>();
                if (splinePoints.Find(x => x.XValue == _selectedDataPoint.XValue) != null)
                {
                    splinePoints.Find(x => x.XValue == _selectedDataPoint.XValue).YValues[0] =
                        _selectedDataPoint.YValues[0];
                }
                _vizualization3D.ChangeSplineFunction(splinePoints);
                _selectedDataPoint = null;

                chart1.Invalidate();
            }
        }

        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {
            HitTestResult hitResult = chart1.HitTest(e.X, e.Y);

            if (e.Button == MouseButtons.Left)
            {
                _selectedDataPoint = null;
                if (hitResult.ChartElementType == ChartElementType.DataPoint)
                {
                    DataPoint selected = (DataPoint)hitResult.Object;
                    if (chart1.Series["OpacityFunction"].Points.Contains(selected))
                    {
                        _selectedDataPoint = selected;
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (hitResult.ChartElementType == ChartElementType.DataPoint)
                {
                    DataPoint selected = (DataPoint)hitResult.Object;
                    if (chart1.Series["OpacityFunctionSpline"].Points.Contains(selected))
                    {
                        //var pos = chart1.PointToClient(new Point(e.X, e.Y));
                        //--------
                        chart1.ChartAreas["ChartArea1"].CursorX.SetCursorPixelPosition(new Point(e.X, e.Y), false);
                        chart1.ChartAreas["ChartArea1"].CursorY.SetCursorPixelPosition(new Point(e.X, e.Y), false);

                        double pX = chart1.ChartAreas["ChartArea1"].CursorX.Position; //X Axis Coordinate of your mouse cursor
                        double pY = chart1.ChartAreas["ChartArea1"].CursorY.Position; //Y Axis Coordinate of your mouse cursor

                        chart1.Series["OpacityFunction"].Points.AddXY(pX, pY);

                        //------
                        //chart1.Series["OpacityFunction"].Points.AddXY(selected.XValue, selected.YValues[0]);
                        List<DataPoint> points = chart1.Series["OpacityFunction"].Points.ToList<DataPoint>();
                        points.Sort(new Comparison<DataPoint>(Compare));
                        _vizualization3D.ChangeSplineFunction(points);
                    }
                }
            }
        }

        private int Compare(DataPoint point1, DataPoint point2)
        {
            return point1.XValue.CompareTo(point2.XValue);
        }

        private void chart1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            HitTestResult hitResult = chart1.HitTest(e.X, e.Y);
            if (e.Button == MouseButtons.Right)
            {
                if (hitResult.ChartElementType == ChartElementType.DataPoint)
                {
                    DataPoint selected = (DataPoint) hitResult.Object;
                    if (chart1.Series["OpacityFunction"].Points.Contains(selected))
                    {
                        chart1.Series["OpacityFunction"].Points.Remove(selected);
                        //------
                        //chart1.Series["OpacityFunction"].Points.AddXY(selected.XValue, selected.YValues[0]);
                        List<DataPoint> points = chart1.Series["OpacityFunction"].Points.ToList<DataPoint>();
                        //points.Remove(points.Find(x => x.XValue == pX & x.YValues[0] == pY));
                        points.Sort(new Comparison<DataPoint>(Compare));
                        _vizualization3D.ChangeSplineAndPointFunction(points);
                    }
                }
            }

        }

        #endregion

        #region Obsługa pojawiania i znikania poszczególnych płaszczyzn
        //-------------------------------------------------------------------------------------
        //obsługa pojawiania i znikania poszczególnych płaszczyzn
        private void PlaneXButton_Click(object sender, EventArgs e)
        {
            if (PlaneXButton.Text.Equals(ButtonText.ShowPlaneX))
            {
                _vizualization3D.PlaneWidgetX.On();
                PlaneXButton.Text = ButtonText.HidePlaneX;
            }
            else
            {
                _vizualization3D.PlaneWidgetX.Off();
                PlaneXButton.Text = ButtonText.ShowPlaneX;
            }
        }

        private void PlaneYButton_Click(object sender, EventArgs e)
        {
            if (PlaneYButton.Text.Equals(ButtonText.ShowPlaneY))
            {
                _vizualization3D.PlaneWidgetY.On();
                PlaneYButton.Text = ButtonText.HidePlaneY;
            }
            else
            {
                _vizualization3D.PlaneWidgetY.Off();
                PlaneYButton.Text = ButtonText.ShowPlaneY;
            }
        }

        private void PlaneZButton_Click(object sender, EventArgs e)
        {
            if (PlaneZButton.Text.Equals(ButtonText.ShowPlaneZ))
            {
                _vizualization3D.PlaneWidgetZ.On();
                PlaneZButton.Text = ButtonText.HidePlaneZ;
            }
            else
            {
                _vizualization3D.PlaneWidgetZ.Off();
                PlaneZButton.Text = ButtonText.ShowPlaneZ;
            }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            //var bmp = firstVizualization2D.GetImage();
            var form = new Form2(_dicomLoader) { Visible = true };
        }


        private void buttonLoadDicom_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _directoryPath = openFileDialog1.SelectedPath;
                _dicomLoader.ChangeDirectory(_directoryPath);

                _vizualization3D.ChangeDirectory(_dicomLoader);
                _vizualization3D.ChangeColorAndOpacityFunction(comboBox1.Text);
                _vizualization3D.ChangeToSerie(int.Parse(comboBoxSeries.Text));
            }

        }


    }
}
