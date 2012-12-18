using System;
using DrawingModule;

namespace MainWindow
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.fourthWindow = new Kitware.VTK.RenderWindowControl();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.firstWindow = new Kitware.VTK.RenderWindowControl();
            this.secondWindow = new Kitware.VTK.RenderWindowControl();
            this.thirdWindow = new Kitware.VTK.RenderWindowControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.labelWidth = new System.Windows.Forms.Label();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.labelLevel = new System.Windows.Forms.Label();
            this.textBoxLevel1 = new System.Windows.Forms.TextBox();
            this.trackBarLevel = new System.Windows.Forms.TrackBar();
            this.trackBarWidth = new System.Windows.Forms.TrackBar();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.colorStrip = new System.Windows.Forms.Panel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBoxSeries = new System.Windows.Forms.ComboBox();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.PlaneXButton = new System.Windows.Forms.Button();
            this.PlaneYButton = new System.Windows.Forms.Button();
            this.PlaneZButton = new System.Windows.Forms.Button();
            this.lockX = new System.Windows.Forms.CheckBox();
            this.lockZ = new System.Windows.Forms.CheckBox();
            this.lockY = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadDicomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.drawingPanelX = new DrawingModule.DrawingPanel();
            this.bigFirstWindow = new Kitware.VTK.RenderWindowControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.drawingPanelY = new DrawingModule.DrawingPanel();
            this.bigSecondWindow = new Kitware.VTK.RenderWindowControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.drawingPanelZ = new DrawingModule.DrawingPanel();
            this.bigThirdWindow = new Kitware.VTK.RenderWindowControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.panel6 = new System.Windows.Forms.Panel();
            this.bigFourthWindow = new Kitware.VTK.RenderWindowControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.drawingCheckBox = new System.Windows.Forms.CheckBox();
            this.clipingPanel = new MainWindow.ClipingToolbox();
            this.drawingToolbox = new DrawingModule.DrawingToolbox();
            this.saveImageFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.panel1.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWidth)).BeginInit();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel8.SuspendLayout();
            this.panel10.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // fourthWindow
            // 
            this.fourthWindow.AddTestActors = false;
            this.fourthWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.fourthWindow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fourthWindow.Location = new System.Drawing.Point(352, 296);
            this.fourthWindow.Name = "fourthWindow";
            this.fourthWindow.Size = new System.Drawing.Size(351, 294);
            this.fourthWindow.TabIndex = 0;
            this.fourthWindow.TestText = null;
            this.fourthWindow.Load += new System.EventHandler(this.fourthWindow_Load);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(332, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(690, 619);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // firstWindow
            // 
            this.firstWindow.AddTestActors = false;
            this.firstWindow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.firstWindow.Location = new System.Drawing.Point(0, 0);
            this.firstWindow.Name = "firstWindow";
            this.firstWindow.Size = new System.Drawing.Size(351, 294);
            this.firstWindow.TabIndex = 4;
            this.firstWindow.TestText = null;
            this.firstWindow.Load += new System.EventHandler(this.firstWindow_Load);
            // 
            // secondWindow
            // 
            this.secondWindow.AddTestActors = false;
            this.secondWindow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.secondWindow.Location = new System.Drawing.Point(352, 0);
            this.secondWindow.Name = "secondWindow";
            this.secondWindow.Size = new System.Drawing.Size(351, 294);
            this.secondWindow.TabIndex = 3;
            this.secondWindow.TestText = null;
            this.secondWindow.Load += new System.EventHandler(this.secondWindow_Load);
            // 
            // thirdWindow
            // 
            this.thirdWindow.AddTestActors = false;
            this.thirdWindow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.thirdWindow.Location = new System.Drawing.Point(0, 296);
            this.thirdWindow.Name = "thirdWindow";
            this.thirdWindow.Size = new System.Drawing.Size(351, 294);
            this.thirdWindow.TabIndex = 4;
            this.thirdWindow.TestText = null;
            this.thirdWindow.Load += new System.EventHandler(this.thirdWindow_Load);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.panel9);
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.panel10);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.MinimumSize = new System.Drawing.Size(267, 619);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(305, 622);
            this.panel1.TabIndex = 3;
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel7.Controls.Add(this.label4);
            this.panel7.Controls.Add(this.labelWidth);
            this.panel7.Controls.Add(this.textBoxWidth);
            this.panel7.Controls.Add(this.labelLevel);
            this.panel7.Controls.Add(this.textBoxLevel1);
            this.panel7.Controls.Add(this.trackBarLevel);
            this.panel7.Controls.Add(this.trackBarWidth);
            this.panel7.Location = new System.Drawing.Point(0, 27);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(305, 112);
            this.panel7.TabIndex = 71;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Window width and level:";
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(11, 40);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(35, 13);
            this.labelWidth.TabIndex = 11;
            this.labelWidth.Text = "Width";
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Location = new System.Drawing.Point(57, 37);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(42, 20);
            this.textBoxWidth.TabIndex = 16;
            this.textBoxWidth.Text = "0";
            this.textBoxWidth.TextChanged += new System.EventHandler(this.textBoxWidth_TextChanged);
            // 
            // labelLevel
            // 
            this.labelLevel.AutoSize = true;
            this.labelLevel.Location = new System.Drawing.Point(11, 69);
            this.labelLevel.Name = "labelLevel";
            this.labelLevel.Size = new System.Drawing.Size(33, 13);
            this.labelLevel.TabIndex = 12;
            this.labelLevel.Text = "Level";
            // 
            // textBoxLevel1
            // 
            this.textBoxLevel1.Location = new System.Drawing.Point(57, 66);
            this.textBoxLevel1.Name = "textBoxLevel1";
            this.textBoxLevel1.Size = new System.Drawing.Size(42, 20);
            this.textBoxLevel1.TabIndex = 17;
            this.textBoxLevel1.Text = "0";
            this.textBoxLevel1.TextChanged += new System.EventHandler(this.textBoxLevel_TextChanged);
            // 
            // trackBarLevel
            // 
            this.trackBarLevel.Location = new System.Drawing.Point(108, 53);
            this.trackBarLevel.Maximum = 2000;
            this.trackBarLevel.Minimum = -700;
            this.trackBarLevel.Name = "trackBarLevel";
            this.trackBarLevel.Size = new System.Drawing.Size(184, 45);
            this.trackBarLevel.TabIndex = 10;
            this.trackBarLevel.Scroll += new System.EventHandler(this.trackBarLevel_Scroll);
            // 
            // trackBarWidth
            // 
            this.trackBarWidth.Location = new System.Drawing.Point(105, 25);
            this.trackBarWidth.Maximum = 2500;
            this.trackBarWidth.Name = "trackBarWidth";
            this.trackBarWidth.Size = new System.Drawing.Size(187, 45);
            this.trackBarWidth.TabIndex = 6;
            this.trackBarWidth.Scroll += new System.EventHandler(this.trackBarWidth_Scroll);
            // 
            // panel9
            // 
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel9.Controls.Add(this.label2);
            this.panel9.Controls.Add(this.colorStrip);
            this.panel9.Controls.Add(this.chart1);
            this.panel9.Location = new System.Drawing.Point(0, 136);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(305, 214);
            this.panel9.TabIndex = 73;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 13);
            this.label2.TabIndex = 68;
            this.label2.Text = "Opacity and color functions:";
            // 
            // colorStrip
            // 
            this.colorStrip.Enabled = false;
            this.colorStrip.Location = new System.Drawing.Point(57, 178);
            this.colorStrip.Name = "colorStrip";
            this.colorStrip.Size = new System.Drawing.Size(197, 17);
            this.colorStrip.TabIndex = 67;
            this.colorStrip.Paint += new System.Windows.Forms.PaintEventHandler(this.colorStrip_Paint);
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.Linen;
            this.chart1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            this.chart1.BorderlineColor = System.Drawing.Color.Silver;
            this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chart1.BorderlineWidth = 2;
            this.chart1.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Emboss;
            chartArea1.Area3DStyle.Inclination = 40;
            chartArea1.Area3DStyle.IsClustered = true;
            chartArea1.Area3DStyle.IsRightAngleAxes = false;
            chartArea1.Area3DStyle.LightStyle = System.Windows.Forms.DataVisualization.Charting.LightStyle.Realistic;
            chartArea1.Area3DStyle.Perspective = 9;
            chartArea1.Area3DStyle.Rotation = 25;
            chartArea1.Area3DStyle.WallWidth = 3;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            chartArea1.AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            chartArea1.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisY.Maximum = 1D;
            chartArea1.BackColor = System.Drawing.Color.OldLace;
            chartArea1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea1.BackSecondaryColor = System.Drawing.Color.White;
            chartArea1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.Name = "ChartArea1";
            chartArea1.ShadowColor = System.Drawing.Color.Transparent;
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Location = new System.Drawing.Point(7, 27);
            this.chart1.Name = "chart1";
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.RoyalBlue;
            series1.IsVisibleInLegend = false;
            series1.LabelBorderWidth = 3;
            series1.MarkerColor = System.Drawing.Color.White;
            series1.MarkerSize = 1;
            series1.Name = "OpacityFunctionSpline";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series2.BackSecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            series2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            series2.BorderWidth = 5;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series2.Color = System.Drawing.Color.Yellow;
            series2.IsVisibleInLegend = false;
            series2.MarkerSize = 8;
            series2.Name = "OpacityFunction";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(270, 145);
            this.chart1.TabIndex = 58;
            this.chart1.Text = "chart1";
            this.chart1.Paint += new System.Windows.Forms.PaintEventHandler(this.chart1_Paint);
            this.chart1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseDoubleClick);
            this.chart1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseDown);
            this.chart1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseMove);
            this.chart1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseUp);
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel8.Controls.Add(this.label1);
            this.panel8.Controls.Add(this.comboBox1);
            this.panel8.Controls.Add(this.comboBoxSeries);
            this.panel8.Location = new System.Drawing.Point(0, 348);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(305, 76);
            this.panel8.TabIndex = 72;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 58;
            this.label1.Text = "Preset options:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(7, 32);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(203, 21);
            this.comboBox1.TabIndex = 18;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBoxSeries
            // 
            this.comboBoxSeries.FormattingEnabled = true;
            this.comboBoxSeries.Location = new System.Drawing.Point(216, 32);
            this.comboBoxSeries.Name = "comboBoxSeries";
            this.comboBoxSeries.Size = new System.Drawing.Size(52, 21);
            this.comboBoxSeries.TabIndex = 57;
            this.comboBoxSeries.SelectedIndexChanged += new System.EventHandler(this.comboBoxSeries_SelectedIndexChanged);
            // 
            // panel10
            // 
            this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel10.Controls.Add(this.label3);
            this.panel10.Controls.Add(this.PlaneXButton);
            this.panel10.Controls.Add(this.PlaneYButton);
            this.panel10.Controls.Add(this.PlaneZButton);
            this.panel10.Controls.Add(this.lockX);
            this.panel10.Controls.Add(this.lockZ);
            this.panel10.Controls.Add(this.lockY);
            this.panel10.Location = new System.Drawing.Point(0, 422);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(304, 197);
            this.panel10.TabIndex = 74;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 71;
            this.label3.Text = "Planes options:";
            // 
            // PlaneXButton
            // 
            this.PlaneXButton.BackColor = System.Drawing.Color.Red;
            this.PlaneXButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.PlaneXButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PlaneXButton.ForeColor = System.Drawing.Color.White;
            this.PlaneXButton.Location = new System.Drawing.Point(11, 40);
            this.PlaneXButton.Name = "PlaneXButton";
            this.PlaneXButton.Size = new System.Drawing.Size(125, 23);
            this.PlaneXButton.TabIndex = 60;
            this.PlaneXButton.Text = "Show PlaneX";
            this.PlaneXButton.UseVisualStyleBackColor = false;
            this.PlaneXButton.Click += new System.EventHandler(this.PlaneXButton_Click);
            // 
            // PlaneYButton
            // 
            this.PlaneYButton.BackColor = System.Drawing.Color.Green;
            this.PlaneYButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PlaneYButton.ForeColor = System.Drawing.Color.White;
            this.PlaneYButton.Location = new System.Drawing.Point(12, 69);
            this.PlaneYButton.Name = "PlaneYButton";
            this.PlaneYButton.Size = new System.Drawing.Size(125, 23);
            this.PlaneYButton.TabIndex = 61;
            this.PlaneYButton.Text = "Show PlaneY";
            this.PlaneYButton.UseVisualStyleBackColor = false;
            this.PlaneYButton.Click += new System.EventHandler(this.PlaneYButton_Click);
            // 
            // PlaneZButton
            // 
            this.PlaneZButton.BackColor = System.Drawing.Color.Blue;
            this.PlaneZButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PlaneZButton.ForeColor = System.Drawing.Color.White;
            this.PlaneZButton.Location = new System.Drawing.Point(12, 98);
            this.PlaneZButton.Name = "PlaneZButton";
            this.PlaneZButton.Size = new System.Drawing.Size(125, 23);
            this.PlaneZButton.TabIndex = 62;
            this.PlaneZButton.Text = "Show PlaneZ";
            this.PlaneZButton.UseVisualStyleBackColor = false;
            this.PlaneZButton.Click += new System.EventHandler(this.PlaneZButton_Click);
            // 
            // lockX
            // 
            this.lockX.AutoSize = true;
            this.lockX.Enabled = false;
            this.lockX.Location = new System.Drawing.Point(165, 46);
            this.lockX.Name = "lockX";
            this.lockX.Size = new System.Drawing.Size(81, 17);
            this.lockX.TabIndex = 68;
            this.lockX.Text = "Lock X axis";
            this.lockX.UseVisualStyleBackColor = true;
            this.lockX.CheckedChanged += new System.EventHandler(this.lockX_CheckedChanged);
            // 
            // lockZ
            // 
            this.lockZ.AutoSize = true;
            this.lockZ.Enabled = false;
            this.lockZ.Location = new System.Drawing.Point(165, 104);
            this.lockZ.Name = "lockZ";
            this.lockZ.Size = new System.Drawing.Size(81, 17);
            this.lockZ.TabIndex = 70;
            this.lockZ.Text = "Lock Z axis";
            this.lockZ.UseVisualStyleBackColor = true;
            this.lockZ.CheckedChanged += new System.EventHandler(this.lockZ_CheckedChanged);
            // 
            // lockY
            // 
            this.lockY.AutoSize = true;
            this.lockY.Enabled = false;
            this.lockY.Location = new System.Drawing.Point(165, 75);
            this.lockY.Name = "lockY";
            this.lockY.Size = new System.Drawing.Size(81, 17);
            this.lockY.TabIndex = 69;
            this.lockY.Text = "Lock Y axis";
            this.lockY.UseVisualStyleBackColor = true;
            this.lockY.CheckedChanged += new System.EventHandler(this.lockY_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(305, 24);
            this.menuStrip1.TabIndex = 75;
            this.menuStrip1.Text = "File";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadDicomToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadDicomToolStripMenuItem
            // 
            this.loadDicomToolStripMenuItem.Name = "loadDicomToolStripMenuItem";
            this.loadDicomToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.loadDicomToolStripMenuItem.Text = "Load dicom";
            this.loadDicomToolStripMenuItem.Click += new System.EventHandler(this.loadDicomToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Controls.Add(this.tabPage5);
            this.tabControl.Location = new System.Drawing.Point(311, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.Padding = new System.Drawing.Point(39, 6);
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(711, 619);
            this.tabControl.TabIndex = 4;
            this.tabControl.Tag = "";
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.fourthWindow);
            this.tabPage1.Controls.Add(this.secondWindow);
            this.tabPage1.Controls.Add(this.firstWindow);
            this.tabPage1.Controls.Add(this.thirdWindow);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(703, 587);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Four windows view";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.drawingPanelX);
            this.tabPage2.Controls.Add(this.bigFirstWindow);
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(703, 587);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "X plane view";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // drawingPanelX
            // 
            this.drawingPanelX.Image = null;
            this.drawingPanelX.Location = new System.Drawing.Point(6, 574);
            this.drawingPanelX.Name = "drawingPanelX";
            this.drawingPanelX.Size = new System.Drawing.Size(691, 10);
            this.drawingPanelX.TabIndex = 6;
            // 
            // bigFirstWindow
            // 
            this.bigFirstWindow.AddTestActors = false;
            this.bigFirstWindow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bigFirstWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bigFirstWindow.Location = new System.Drawing.Point(3, 3);
            this.bigFirstWindow.Name = "bigFirstWindow";
            this.bigFirstWindow.Size = new System.Drawing.Size(697, 581);
            this.bigFirstWindow.TabIndex = 5;
            this.bigFirstWindow.TestText = null;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.drawingPanelY);
            this.tabPage3.Controls.Add(this.bigSecondWindow);
            this.tabPage3.Location = new System.Drawing.Point(4, 28);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(703, 587);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Y plane view";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // drawingPanelY
            // 
            this.drawingPanelY.Image = null;
            this.drawingPanelY.Location = new System.Drawing.Point(6, 573);
            this.drawingPanelY.Name = "drawingPanelY";
            this.drawingPanelY.Size = new System.Drawing.Size(691, 10);
            this.drawingPanelY.TabIndex = 7;
            // 
            // bigSecondWindow
            // 
            this.bigSecondWindow.AddTestActors = false;
            this.bigSecondWindow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bigSecondWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bigSecondWindow.Location = new System.Drawing.Point(3, 3);
            this.bigSecondWindow.Name = "bigSecondWindow";
            this.bigSecondWindow.Size = new System.Drawing.Size(697, 581);
            this.bigSecondWindow.TabIndex = 6;
            this.bigSecondWindow.TestText = null;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.drawingPanelZ);
            this.tabPage4.Controls.Add(this.bigThirdWindow);
            this.tabPage4.Location = new System.Drawing.Point(4, 28);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(703, 587);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Z plane view";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // drawingPanelZ
            // 
            this.drawingPanelZ.Image = null;
            this.drawingPanelZ.Location = new System.Drawing.Point(7, 573);
            this.drawingPanelZ.Name = "drawingPanelZ";
            this.drawingPanelZ.Size = new System.Drawing.Size(691, 10);
            this.drawingPanelZ.TabIndex = 8;
            // 
            // bigThirdWindow
            // 
            this.bigThirdWindow.AddTestActors = false;
            this.bigThirdWindow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bigThirdWindow.Location = new System.Drawing.Point(7, 6);
            this.bigThirdWindow.Name = "bigThirdWindow";
            this.bigThirdWindow.Size = new System.Drawing.Size(691, 561);
            this.bigThirdWindow.TabIndex = 7;
            this.bigThirdWindow.TestText = null;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.panel6);
            this.tabPage5.Controls.Add(this.bigFourthWindow);
            this.tabPage5.Location = new System.Drawing.Point(4, 28);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(703, 587);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "3D view";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            this.panel6.Location = new System.Drawing.Point(6, 559);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(691, 26);
            this.panel6.TabIndex = 8;
            // 
            // bigFourthWindow
            // 
            this.bigFourthWindow.AddTestActors = false;
            this.bigFourthWindow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bigFourthWindow.Location = new System.Drawing.Point(6, 6);
            this.bigFourthWindow.Name = "bigFourthWindow";
            this.bigFourthWindow.Size = new System.Drawing.Size(691, 545);
            this.bigFourthWindow.TabIndex = 7;
            this.bigFourthWindow.TestText = null;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(141, 111);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 100);
            this.panel2.TabIndex = 0;
            // 
            // drawingCheckBox
            // 
            this.drawingCheckBox.AutoSize = true;
            this.drawingCheckBox.Enabled = false;
            this.drawingCheckBox.Location = new System.Drawing.Point(1029, 241);
            this.drawingCheckBox.Name = "drawingCheckBox";
            this.drawingCheckBox.Size = new System.Drawing.Size(137, 17);
            this.drawingCheckBox.TabIndex = 64;
            this.drawingCheckBox.Text = "Drawing Mode Enabled";
            this.drawingCheckBox.UseVisualStyleBackColor = true;
            this.drawingCheckBox.CheckedChanged += new System.EventHandler(this.drawingCheckBox_CheckedChanged);
            // 
            // clipingPanel
            // 
            this.clipingPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.clipingPanel.Location = new System.Drawing.Point(1028, 20);
            this.clipingPanel.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.clipingPanel.Name = "clipingPanel";
            this.clipingPanel.Padding = new System.Windows.Forms.Padding(5, 15, 5, 5);
            this.clipingPanel.Size = new System.Drawing.Size(165, 214);
            this.clipingPanel.TabIndex = 63;
            this.clipingPanel.TabStop = false;
            this.clipingPanel.Text = "ClipingToolbox";
            // 
            // drawingToolbox
            // 
            this.drawingToolbox.Location = new System.Drawing.Point(1029, 265);
            this.drawingToolbox.Name = "drawingToolbox";
            this.drawingToolbox.Size = new System.Drawing.Size(164, 100);
            this.drawingToolbox.TabIndex = 65;
            this.drawingToolbox.TabStop = false;
            this.drawingToolbox.Text = "Drawing Toolbox";
            // 
            // saveImageFileDialog
            // 
            this.saveImageFileDialog.DefaultExt = "bmp";
            this.saveImageFileDialog.Filter = "Bitmap files | *.bmp | All files | *.*";
            this.saveImageFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveImageFileDialog_FileOk);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1204, 622);
            this.Controls.Add(this.drawingToolbox);
            this.Controls.Add(this.drawingCheckBox);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.clipingPanel);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "DICOM 3D: aplikacja do wizualizacji wyników badañ medycznych.";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DisposeAll);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWidth)).EndInit();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Kitware.VTK.RenderWindowControl fourthWindow;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Kitware.VTK.RenderWindowControl secondWindow;
        private Kitware.VTK.RenderWindowControl thirdWindow;
        private Kitware.VTK.RenderWindowControl firstWindow;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelLevel;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.TrackBar trackBarLevel;
        private System.Windows.Forms.TrackBar trackBarWidth;
        private System.Windows.Forms.TextBox textBoxLevel1;
        private System.Windows.Forms.TextBox textBoxWidth;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBoxSeries;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button PlaneZButton;
        private System.Windows.Forms.Button PlaneYButton;
        private ClipingToolbox clipingPanel;
        private System.Windows.Forms.FolderBrowserDialog openFileDialog1;
        private System.Windows.Forms.Button PlaneXButton;
        private System.Windows.Forms.Panel colorStrip;
        private System.Windows.Forms.CheckBox lockZ;
        private System.Windows.Forms.CheckBox lockY;
        private System.Windows.Forms.CheckBox lockX;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Panel panel2;
        private Kitware.VTK.RenderWindowControl bigFirstWindow;
        private DrawingPanel drawingPanelX;
        private DrawingPanel drawingPanelY;
        private Kitware.VTK.RenderWindowControl bigSecondWindow;
        private DrawingPanel drawingPanelZ;
        private Kitware.VTK.RenderWindowControl bigThirdWindow;
        private System.Windows.Forms.Panel panel6;
        private Kitware.VTK.RenderWindowControl bigFourthWindow;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadDicomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.CheckBox drawingCheckBox;
        private DrawingToolbox drawingToolbox;
        private System.Windows.Forms.SaveFileDialog saveImageFileDialog;
    }
}

