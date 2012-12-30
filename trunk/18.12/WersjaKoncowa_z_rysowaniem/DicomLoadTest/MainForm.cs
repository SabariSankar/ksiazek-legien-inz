using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Kitware.VTK;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using DrawingModule;
using ClipingModule;

namespace MainWindow
{
    public partial class MainForm : Form
    {
        private Visualization3D _vizualization3D;
        private Visualization2D _firstVizualization2D;
        private Visualization2D _secondVizualization2D;
        private Visualization2D _thirdVizualization2D;

        private readonly DicomLoader _dicomLoader;
        private string _directoryPath = @"D:\Downloads\PANORAMIX"; //"D:\\DICOM\\GOUDURIX\\GOUDURIX\\tmp";
        private const string PresetDir = @"..\..\presety";

        /// <summary>
        /// Selected point data used during opacity function modyfication
        /// </summary>
        private DataPoint _selectedDataPoint;
        /// <summary>
        /// Previous active tab.
        /// </summary>
        private int _prevoiusTab;

        public MainForm()
        {
            InitializeComponent();

            InitImageExport();
            drawingToolbox.DrawnigModeEnabled.CheckedChanged += drawingCheckBox_CheckedChanged;

            _dicomLoader = new DicomLoader(_directoryPath);
        }

        #region Obs³uga zamykania aplikacji

        public void DisposeAll(object sender, FormClosingEventArgs e)
        {
            _vizualization3D.Dispose();
            _firstVizualization2D.Dispose();
            _secondVizualization2D.Dispose();
            _thirdVizualization2D.Dispose();
            _dicomLoader.Dispose();
        }

        #endregion

        #region Obs³uga przesuwania poszczególnych p³aszczyzn
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

        #region £adowanie i update RenderWindowControl z wizualizacj¹ 3D

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

            /*vtkImageExtractComponents extract = vtkImageExtractComponents.New();
            extract.SetInput( _dicomLoader.GetOutput() );
            extract.SetComponents(0);
            extract.Update();
            
            double[] range = extract.GetOutput().GetScalarRange();

            vtkXYPlotActor xyPlotActor = new vtkXYPlotActor();
            vtkActor actor = vtkActor.New();

            vtkImageAccumulate histogram = new vtkImageAccumulate();
            histogram.SetInput(extract.GetOutput());
            histogram.SetComponentOrigin(0, 0, 0);
            histogram.SetComponentSpacing(1, 1, 1);
            histogram.IgnoreZeroOn();
            histogram.Update();


            xyPlotActor.AddInput(histogram.GetOutput());
            fourthWindow.RenderWindow.GetRenderers().GetFirstRenderer().AddActor(xyPlotActor); */
            fourthWindow.RenderWindow.Render();

        }

        //updatatuje okno wziualizacji 3d
        private void update3DVisualization(float windowLevel, float windowWidth)
        {
            _vizualization3D.Update3DVisualization(windowLevel, windowWidth);

        }

        #endregion

        #region £adowanie i update RenderWindowControls z wizualizacj¹ 2D

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

        private void update2DVisualization(float windowLevel, float windowWidth)
        {
            _firstVizualization2D.Update2DVisualization(windowLevel, windowWidth);
            _secondVizualization2D.Update2DVisualization(windowLevel, windowWidth);
            _thirdVizualization2D.Update2DVisualization(windowLevel, windowWidth);

            if (bigFirstWindow.RenderWindow != null)
                bigFirstWindow.RenderWindow.Render();
            if (bigSecondWindow.RenderWindow != null)
                bigSecondWindow.RenderWindow.Render();
            if (bigThirdWindow.RenderWindow != null)
                bigThirdWindow.RenderWindow.Render();
            if (bigFourthWindow.RenderWindow != null)
                bigFourthWindow.RenderWindow.Render();

        }

        #endregion

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
            colorStrip.Invalidate();

            if (bigFourthWindow.RenderWindow != null)
            {
                bigFourthWindow.RenderWindow.GetRenderers().GetFirstRenderer().GetVolumes().RemoveAllItems();
                bigFourthWindow.RenderWindow.GetRenderers().GetFirstRenderer().AddVolume(_vizualization3D.GetVolume());
                bigFourthWindow.Update();
                bigFourthWindow.RenderWindow.Render();
            }

        }


        private void comboBoxSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            _vizualization3D.ChangeToSerie(int.Parse(comboBoxSeries.Text));
            colorStrip.Invalidate();

            if (bigFourthWindow.RenderWindow != null)
                bigFourthWindow.RenderWindow.Render();
        }

        #endregion

        #region Obsluga wykresu
        //-------------------------------------------------------------------------------------
        //Obs³uga wykresu

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
                if (bigFourthWindow.RenderWindow != null)
                    bigFourthWindow.RenderWindow.Render();
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

                        if (bigFourthWindow.RenderWindow != null)
                            bigFourthWindow.RenderWindow.Render();
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
                    DataPoint selected = (DataPoint)hitResult.Object;
                    if (chart1.Series["OpacityFunction"].Points.Contains(selected))
                    {
                        chart1.Series["OpacityFunction"].Points.Remove(selected);
                        //------
                        //chart1.Series["OpacityFunction"].Points.AddXY(selected.XValue, selected.YValues[0]);
                        List<DataPoint> points = chart1.Series["OpacityFunction"].Points.ToList<DataPoint>();
                        //points.Remove(points.Find(x => x.XValue == pX & x.YValues[0] == pY));
                        points.Sort(new Comparison<DataPoint>(Compare));
                        _vizualization3D.ChangeSplineAndPointFunction(points);

                        if (bigFourthWindow.RenderWindow != null)
                            bigFourthWindow.RenderWindow.Render();
                    }
                }
            }

        }

        #endregion

        #region Obs³uga pojawiania i znikania poszczególnych p³aszczyzn
        //-------------------------------------------------------------------------------------
        //obs³uga pojawiania i znikania poszczególnych p³aszczyzn
        private void PlaneXButton_Click(object sender, EventArgs e)
        {
            if (PlaneXButton.Text.Equals(ButtonText.ShowPlaneX))
            {
                _vizualization3D.PlaneWidgetX.On();
                PlaneXButton.Text = ButtonText.HidePlaneX;
                lockX.Enabled = true;
            }
            else
            {
                _vizualization3D.PlaneWidgetX.Off();
                PlaneXButton.Text = ButtonText.ShowPlaneX;
                lockX.Enabled = false;
            }
        }

        private void PlaneYButton_Click(object sender, EventArgs e)
        {
            if (PlaneYButton.Text.Equals(ButtonText.ShowPlaneY))
            {
                _vizualization3D.PlaneWidgetY.On();
                PlaneYButton.Text = ButtonText.HidePlaneY;
                lockY.Enabled = true;
            }
            else
            {
                _vizualization3D.PlaneWidgetY.Off();
                PlaneYButton.Text = ButtonText.ShowPlaneY;
                lockY.Enabled = false;
            }
        }

        private void PlaneZButton_Click(object sender, EventArgs e)
        {
            if (PlaneZButton.Text.Equals(ButtonText.ShowPlaneZ))
            {
                _vizualization3D.PlaneWidgetZ.On();
                PlaneZButton.Text = ButtonText.HidePlaneZ;
                lockZ.Enabled = true;
            }
            else
            {
                _vizualization3D.PlaneWidgetZ.Off();
                PlaneZButton.Text = ButtonText.ShowPlaneZ;
                lockZ.Enabled = false;
            }
        }

        #endregion

        #region Obs³uga locków

        private void lockX_CheckedChanged(object sender, EventArgs e)
        {
            int state = ((CheckBox)sender).Checked ? 0 : 1;
            _vizualization3D.ChangePlaneGadetActivity(Axis.X, state);
        }

        private void lockY_CheckedChanged(object sender, EventArgs e)
        {
            int state = ((CheckBox)sender).Checked ? 0 : 1;
            _vizualization3D.ChangePlaneGadetActivity(Axis.Y, state);
        }

        private void lockZ_CheckedChanged(object sender, EventArgs e)
        {
            int state = ((CheckBox)sender).Checked ? 0 : 1;
            _vizualization3D.ChangePlaneGadetActivity(Axis.Z, state);
            colorStrip.Invalidate();
        }

        #endregion

        #region Rysowanie paska z funkcj¹ koloru

        private void colorStrip_Paint(object sender, PaintEventArgs e)
        {
            if (_vizualization3D != null)
            {
                var width = chart1.ChartAreas["ChartArea1"].Position.Size.Width > 0 ? (int)chart1.ChartAreas["ChartArea1"].Position.Size.Width * chart1.Size.Width / 100 : 1;
                _vizualization3D.GenerateStrip(e.Graphics, 10, width);
            }
        }

        #endregion

        # region Zmiana zak³adki

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            int current = (sender as TabControl).SelectedTab.TabIndex;
            if (current == 0)
            {
                firstWindow.RenderWindow.GetRenderers().RemoveAllItems();
                firstWindow.RenderWindow.AddRenderer(_firstVizualization2D.GetRenderer());
                secondWindow.RenderWindow.GetRenderers().RemoveAllItems();
                secondWindow.RenderWindow.AddRenderer(_secondVizualization2D.GetRenderer());
                thirdWindow.RenderWindow.GetRenderers().RemoveAllItems();
                thirdWindow.RenderWindow.AddRenderer(_thirdVizualization2D.GetRenderer());

                drawingToolbox.DrawnigModeEnabled.Enabled = false;
                drawingToolbox.Enabled = false;
            }
            if (current == 1)
            {
                bigFirstWindow.RenderWindow.GetRenderers().RemoveAllItems();
                bigFirstWindow.RenderWindow.AddRenderer(_firstVizualization2D.GetRenderer());
                bigFirstWindow.RenderWindow.GetInteractor().SetInteractorStyle(vtkInteractorStyleImage.New());

                drawingToolbox.DrawnigModeEnabled.Enabled = true;
                drawingToolbox.DrawnigModeEnabled.Checked = false;
            }
            if (current == 2)
            {
                bigSecondWindow.RenderWindow.GetRenderers().RemoveAllItems();
                bigSecondWindow.RenderWindow.AddRenderer(_secondVizualization2D.GetRenderer());
                bigSecondWindow.RenderWindow.GetInteractor().SetInteractorStyle(vtkInteractorStyleImage.New());

                drawingToolbox.DrawnigModeEnabled.Enabled = true;
                drawingToolbox.DrawnigModeEnabled.Checked = false;
            }
            if (current == 3)
            {
                bigThirdWindow.RenderWindow.GetRenderers().RemoveAllItems();
                bigThirdWindow.RenderWindow.AddRenderer(_thirdVizualization2D.GetRenderer());
                bigThirdWindow.RenderWindow.GetInteractor().SetInteractorStyle(vtkInteractorStyleImage.New());

                drawingToolbox.DrawnigModeEnabled.Enabled = true;
                drawingToolbox.DrawnigModeEnabled.Checked = false;
            }
            if (current == 4)
            {
                //bigFourthWindow.RenderWindow.GetRenderers().RemoveAllItems();
                //bigFourthWindow.RenderWindow.AddRenderer(_vizualization3D.GetRenderer());

                bigFourthWindow.RenderWindow.GetRenderers().GetFirstRenderer().GetVolumes().RemoveAllItems();
                bigFourthWindow.RenderWindow.GetRenderers().GetFirstRenderer().AddVolume(_vizualization3D.GetVolume());
                bigFourthWindow.Update();
                bigFourthWindow.RenderWindow.Render();

                drawingToolbox.DrawnigModeEnabled.Enabled = false;
                drawingToolbox.DrawnigModeEnabled.Checked = false;
            }

        }

        #endregion

        [Obsolete]//Wygl¹da na nieu¿ywane?
        private void buttonBack_Click(object sender, EventArgs e)
        {
            _firstVizualization2D.RotateImageBack();
        }

        [Obsolete]//Wygl¹da na nieu¿ywane?
        private void buttonForward_Click(object sender, EventArgs e)
        {
            _firstVizualization2D.RotateImageForward();
        }

        #region Otwieranie nowych obrazów

        private void loadDicomToolStripMenuItem_Click(object sender, EventArgs e)
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

        #endregion

        #region Wymuszenie odœwierzania paska funkcji koloru

        private void chart1_Paint(object sender, PaintEventArgs e)
        {
            colorStrip.Invalidate();
        }

        #endregion

        #region Rysowanie

        private void drawingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null)
            {
                if (checkBox.Checked)
                {
                    _prevoiusTab = tabControl.SelectedIndex;
                    DrawingModeOn();
                }
                else
                {
                    DrawingModeOff();
                }
            }
        }

        private void DrawingModeOn()
        {
            RenderWindowControl windowControl = null;
            DrawingPanel drawingPanel = null;
            switch (tabControl.SelectedIndex)
            {
                case 1:
                    windowControl = bigFirstWindow; drawingPanel = drawingPanelX;
                    break;
                case 2:
                    windowControl = bigSecondWindow; drawingPanel = drawingPanelY;
                    break;
                case 3:
                    windowControl = bigThirdWindow; drawingPanel = drawingPanelZ;
                    break;
            }
            drawingPanel.Visible = true;
            var height = windowControl.Height;

            vtkWindowToImageFilter ImageFilter = vtkWindowToImageFilter.New();
            ImageFilter.SetInput(windowControl.RenderWindow);
            ImageFilter.Update();
            drawingPanel.Image = ImageFilter.GetOutput().ToBitmap();
            drawingPanel.Dock = DockStyle.Fill;
            windowControl.Height = 0;
            drawingPanel.Invalidate();

            drawingToolbox.Enabled = true;
        }

        private void DrawingModeOff() {
            RenderWindowControl windowControl = null;
            DrawingPanel drawingPanel = null;
            switch (_prevoiusTab)
            {
                case 1:
                    windowControl = bigFirstWindow; drawingPanel = drawingPanelX;
                    break;
                case 2:
                    windowControl = bigSecondWindow; drawingPanel = drawingPanelY;
                    break;
                case 3:
                    windowControl = bigThirdWindow; drawingPanel = drawingPanelZ;
                    break;
            }

            drawingPanel.Dock = DockStyle.Bottom;
            drawingPanel.Height = 0;
            drawingPanel.Visible = false;

            drawingPanel.ImageBackupClear();

            windowControl.Dock = DockStyle.Fill;

            drawingToolbox.Enabled = false;
        }

        #endregion

        #region Zapisywanie obrazu do pliku

        private void InitImageExport()
        {
            drawingToolbox.ExportImageButton.Click += (obj, args) =>
            {
                var dialog = saveImageFileDialog.ShowDialog();
            };

            drawingToolbox.ClearButton.Click += (obj, args) =>
                {
                    DrawingPanel drawingPanel = null;
                    switch (tabControl.SelectedIndex)
                    {
                        case 1:
                            drawingPanel = drawingPanelX;
                            break;
                        case 2:
                            drawingPanel = drawingPanelY;
                            break;
                        case 3:
                            drawingPanel = drawingPanelZ;
                            break;
                    }
                    if(drawingPanel != null)
                        drawingPanel.Clear();
                };
        }

        private void saveImageFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DrawingPanel drawingPanel = null;
            switch (tabControl.SelectedIndex)
            {
                case 1:
                    drawingPanel = drawingPanelX;
                    break;
                case 2:
                    drawingPanel = drawingPanelY;
                    break;
                case 3:
                    drawingPanel = drawingPanelZ;
                    break;
            }
            if(drawingPanel != null)
                drawingPanel.Save(saveImageFileDialog.FileName);
        }

        #endregion      

    }
}
