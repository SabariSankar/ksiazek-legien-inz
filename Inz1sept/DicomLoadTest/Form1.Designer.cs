using System;

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
            this.lockZ = new System.Windows.Forms.CheckBox();
            this.lockY = new System.Windows.Forms.CheckBox();
            this.lockX = new System.Windows.Forms.CheckBox();
            this.colorStrip = new System.Windows.Forms.Panel();
            this.buttonForward = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonLoadDicom = new System.Windows.Forms.Button();
            this.PlaneZButton = new System.Windows.Forms.Button();
            this.PlaneYButton = new System.Windows.Forms.Button();
            this.PlaneXButton = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.comboBoxSeries = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBoxLevel1 = new System.Windows.Forms.TextBox();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.labelLevel = new System.Windows.Forms.Label();
            this.labelWidth = new System.Windows.Forms.Label();
            this.trackBarLevel = new System.Windows.Forms.TrackBar();
            this.trackBarWidth = new System.Windows.Forms.TrackBar();
            this.openFileDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.clipingPanel = new MainWindow.ClipingToolbox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWidth)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
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
            this.flowLayoutPanel1.Size = new System.Drawing.Size(707, 619);
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
            this.panel1.Controls.Add(this.lockZ);
            this.panel1.Controls.Add(this.lockY);
            this.panel1.Controls.Add(this.lockX);
            this.panel1.Controls.Add(this.colorStrip);
            this.panel1.Controls.Add(this.buttonForward);
            this.panel1.Controls.Add(this.buttonBack);
            this.panel1.Controls.Add(this.buttonLoadDicom);
            this.panel1.Controls.Add(this.clipingPanel);
            this.panel1.Controls.Add(this.PlaneZButton);
            this.panel1.Controls.Add(this.PlaneYButton);
            this.panel1.Controls.Add(this.PlaneXButton);
            this.panel1.Controls.Add(this.chart1);
            this.panel1.Controls.Add(this.comboBoxSeries);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.textBoxLevel1);
            this.panel1.Controls.Add(this.textBoxWidth);
            this.panel1.Controls.Add(this.labelLevel);
            this.panel1.Controls.Add(this.labelWidth);
            this.panel1.Controls.Add(this.trackBarLevel);
            this.panel1.Controls.Add(this.trackBarWidth);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.MinimumSize = new System.Drawing.Size(267, 619);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(305, 622);
            this.panel1.TabIndex = 3;
            // 
            // lockZ
            // 
            this.lockZ.AutoSize = true;
            this.lockZ.Enabled = false;
            this.lockZ.Location = new System.Drawing.Point(144, 395);
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
            this.lockY.Location = new System.Drawing.Point(144, 366);
            this.lockY.Name = "lockY";
            this.lockY.Size = new System.Drawing.Size(81, 17);
            this.lockY.TabIndex = 69;
            this.lockY.Text = "Lock Y axis";
            this.lockY.UseVisualStyleBackColor = true;
            this.lockY.CheckedChanged += new System.EventHandler(this.lockY_CheckedChanged);
            // 
            // lockX
            // 
            this.lockX.AutoSize = true;
            this.lockX.Enabled = false;
            this.lockX.Location = new System.Drawing.Point(144, 337);
            this.lockX.Name = "lockX";
            this.lockX.Size = new System.Drawing.Size(81, 17);
            this.lockX.TabIndex = 68;
            this.lockX.Text = "Lock X axis";
            this.lockX.UseVisualStyleBackColor = true;
            this.lockX.CheckedChanged += new System.EventHandler(this.lockX_CheckedChanged);
            // 
            // colorStrip
            // 
            this.colorStrip.Enabled = false;
            this.colorStrip.Location = new System.Drawing.Point(51, 199);
            this.colorStrip.Name = "colorStrip";
            this.colorStrip.Size = new System.Drawing.Size(185, 17);
            this.colorStrip.TabIndex = 67;
            this.colorStrip.Paint += new System.Windows.Forms.PaintEventHandler(this.colorStrip_Paint);
            // 
            // buttonForward
            // 
            this.buttonForward.Location = new System.Drawing.Point(222, 274);
            this.buttonForward.Name = "buttonForward";
            this.buttonForward.Size = new System.Drawing.Size(62, 23);
            this.buttonForward.TabIndex = 66;
            this.buttonForward.Text = "Forward";
            this.buttonForward.UseVisualStyleBackColor = true;
            this.buttonForward.Click += new System.EventHandler(this.buttonForward_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(176, 274);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(40, 23);
            this.buttonBack.TabIndex = 65;
            this.buttonBack.Text = "Back";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonLoadDicom
            // 
            this.buttonLoadDicom.Location = new System.Drawing.Point(12, 587);
            this.buttonLoadDicom.Name = "buttonLoadDicom";
            this.buttonLoadDicom.Size = new System.Drawing.Size(75, 23);
            this.buttonLoadDicom.TabIndex = 64;
            this.buttonLoadDicom.Text = "Load dicom";
            this.buttonLoadDicom.UseVisualStyleBackColor = true;
            this.buttonLoadDicom.Click += new System.EventHandler(this.buttonLoadDicom_Click);
            // 
            // PlaneZButton
            // 
            this.PlaneZButton.BackColor = System.Drawing.Color.Blue;
            this.PlaneZButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PlaneZButton.ForeColor = System.Drawing.Color.White;
            this.PlaneZButton.Location = new System.Drawing.Point(13, 391);
            this.PlaneZButton.Name = "PlaneZButton";
            this.PlaneZButton.Size = new System.Drawing.Size(125, 23);
            this.PlaneZButton.TabIndex = 62;
            this.PlaneZButton.Text = "Show PlaneZ";
            this.PlaneZButton.UseVisualStyleBackColor = false;
            this.PlaneZButton.Click += new System.EventHandler(this.PlaneZButton_Click);
            // 
            // PlaneYButton
            // 
            this.PlaneYButton.BackColor = System.Drawing.Color.Green;
            this.PlaneYButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PlaneYButton.ForeColor = System.Drawing.Color.White;
            this.PlaneYButton.Location = new System.Drawing.Point(13, 362);
            this.PlaneYButton.Name = "PlaneYButton";
            this.PlaneYButton.Size = new System.Drawing.Size(125, 23);
            this.PlaneYButton.TabIndex = 61;
            this.PlaneYButton.Text = "Show PlaneY";
            this.PlaneYButton.UseVisualStyleBackColor = false;
            this.PlaneYButton.Click += new System.EventHandler(this.PlaneYButton_Click);
            // 
            // PlaneXButton
            // 
            this.PlaneXButton.BackColor = System.Drawing.Color.Red;
            this.PlaneXButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.PlaneXButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PlaneXButton.ForeColor = System.Drawing.Color.White;
            this.PlaneXButton.Location = new System.Drawing.Point(13, 333);
            this.PlaneXButton.Name = "PlaneXButton";
            this.PlaneXButton.Size = new System.Drawing.Size(125, 23);
            this.PlaneXButton.TabIndex = 60;
            this.PlaneXButton.Text = "Show PlaneX";
            this.PlaneXButton.UseVisualStyleBackColor = false;
            this.PlaneXButton.Click += new System.EventHandler(this.PlaneXButton_Click);
            // 
            // chart1
            // 
            chartArea1.AxisY.Maximum = 1D;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Location = new System.Drawing.Point(13, 72);
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
            series2.BackSecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            series2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            series2.BorderWidth = 5;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series2.Color = System.Drawing.Color.Yellow;
            series2.IsVisibleInLegend = false;
            series2.MarkerSize = 8;
            series2.Name = "OpacityFunction";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(238, 120);
            this.chart1.TabIndex = 58;
            this.chart1.Text = "chart1";
            this.chart1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseDoubleClick);
            this.chart1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseDown);
            this.chart1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseMove);
            this.chart1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseUp);
            // 
            // comboBoxSeries
            // 
            this.comboBoxSeries.FormattingEnabled = true;
            this.comboBoxSeries.Location = new System.Drawing.Point(232, 235);
            this.comboBoxSeries.Name = "comboBoxSeries";
            this.comboBoxSeries.Size = new System.Drawing.Size(52, 21);
            this.comboBoxSeries.TabIndex = 57;
            this.comboBoxSeries.SelectedIndexChanged += new System.EventHandler(this.comboBoxSeries_SelectedIndexChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(13, 235);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(203, 21);
            this.comboBox1.TabIndex = 18;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // textBoxLevel1
            // 
            this.textBoxLevel1.Location = new System.Drawing.Point(51, 46);
            this.textBoxLevel1.Name = "textBoxLevel1";
            this.textBoxLevel1.Size = new System.Drawing.Size(42, 20);
            this.textBoxLevel1.TabIndex = 17;
            this.textBoxLevel1.Text = "0";
            this.textBoxLevel1.TextChanged += new System.EventHandler(this.textBoxLevel_TextChanged);
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Location = new System.Drawing.Point(51, 16);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(42, 20);
            this.textBoxWidth.TabIndex = 16;
            this.textBoxWidth.Text = "0";
            this.textBoxWidth.TextChanged += new System.EventHandler(this.textBoxWidth_TextChanged);
            // 
            // labelLevel
            // 
            this.labelLevel.AutoSize = true;
            this.labelLevel.Location = new System.Drawing.Point(10, 46);
            this.labelLevel.Name = "labelLevel";
            this.labelLevel.Size = new System.Drawing.Size(33, 13);
            this.labelLevel.TabIndex = 12;
            this.labelLevel.Text = "Level";
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(10, 19);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(35, 13);
            this.labelWidth.TabIndex = 11;
            this.labelWidth.Text = "Width";
            // 
            // trackBarLevel
            // 
            this.trackBarLevel.Location = new System.Drawing.Point(99, 34);
            this.trackBarLevel.Maximum = 2000;
            this.trackBarLevel.Minimum = -700;
            this.trackBarLevel.Name = "trackBarLevel";
            this.trackBarLevel.Size = new System.Drawing.Size(203, 45);
            this.trackBarLevel.TabIndex = 10;
            this.trackBarLevel.Scroll += new System.EventHandler(this.trackBarLevel_Scroll);
            // 
            // trackBarWidth
            // 
            this.trackBarWidth.Location = new System.Drawing.Point(99, 3);
            this.trackBarWidth.Maximum = 2500;
            this.trackBarWidth.Name = "trackBarWidth";
            this.trackBarWidth.Size = new System.Drawing.Size(203, 45);
            this.trackBarWidth.TabIndex = 6;
            this.trackBarWidth.Scroll += new System.EventHandler(this.trackBarWidth_Scroll);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(311, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(711, 619);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.fourthWindow);
            this.tabPage1.Controls.Add(this.secondWindow);
            this.tabPage1.Controls.Add(this.firstWindow);
            this.tabPage1.Controls.Add(this.thirdWindow);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(703, 593);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(703, 593);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // clipingPanel
            // 
            this.clipingPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.clipingPanel.Location = new System.Drawing.Point(12, 432);
            this.clipingPanel.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.clipingPanel.Name = "clipingPanel";
            this.clipingPanel.Padding = new System.Windows.Forms.Padding(5, 15, 5, 5);
            this.clipingPanel.Size = new System.Drawing.Size(290, 141);
            this.clipingPanel.TabIndex = 63;
            this.clipingPanel.TabStop = false;
            this.clipingPanel.Text = "ClipingToolbox";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 622);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DisposeAll);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWidth)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Button buttonLoadDicom;
        private System.Windows.Forms.Button buttonForward;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button PlaneXButton;
        private System.Windows.Forms.Panel colorStrip;
        private System.Windows.Forms.CheckBox lockZ;
        private System.Windows.Forms.CheckBox lockY;
        private System.Windows.Forms.CheckBox lockX;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}

