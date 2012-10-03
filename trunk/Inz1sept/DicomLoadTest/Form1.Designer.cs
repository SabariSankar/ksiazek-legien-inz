namespace DicomLoadTest
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
            this.fourthWindow = new Kitware.VTK.RenderWindowControl();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.firstWindow = new Kitware.VTK.RenderWindowControl();
            this.secondWindow = new Kitware.VTK.RenderWindowControl();
            this.thirdWindow = new Kitware.VTK.RenderWindowControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBoxLevel1 = new System.Windows.Forms.TextBox();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.labelLevel = new System.Windows.Forms.Label();
            this.labelWidth = new System.Windows.Forms.Label();
            this.trackBarLevel = new System.Windows.Forms.TrackBar();
            this.trackBarWidth = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.XtrackBar = new System.Windows.Forms.TrackBar();
            this.YtrackBar1 = new System.Windows.Forms.TrackBar();
            this.ZtrackBar2 = new System.Windows.Forms.TrackBar();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XtrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YtrackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ZtrackBar2)).BeginInit();
            this.SuspendLayout();
            // 
            // fourthWindow
            // 
            this.fourthWindow.AddTestActors = false;
            this.fourthWindow.Location = new System.Drawing.Point(360, 303);
            this.fourthWindow.Name = "fourthWindow";
            this.fourthWindow.Size = new System.Drawing.Size(351, 294);
            this.fourthWindow.TabIndex = 0;
            this.fourthWindow.TestText = null;
            this.fourthWindow.Load += new System.EventHandler(this.fourthWindow_Load);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.firstWindow);
            this.flowLayoutPanel1.Controls.Add(this.secondWindow);
            this.flowLayoutPanel1.Controls.Add(this.thirdWindow);
            this.flowLayoutPanel1.Controls.Add(this.fourthWindow);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(282, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(734, 611);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // firstWindow
            // 
            this.firstWindow.AddTestActors = false;
            this.firstWindow.Location = new System.Drawing.Point(3, 3);
            this.firstWindow.Name = "firstWindow";
            this.firstWindow.Size = new System.Drawing.Size(351, 294);
            this.firstWindow.TabIndex = 4;
            this.firstWindow.TestText = null;
            this.firstWindow.Load += new System.EventHandler(this.firstWindow_Load);
            // 
            // secondWindow
            // 
            this.secondWindow.AddTestActors = false;
            this.secondWindow.Location = new System.Drawing.Point(360, 3);
            this.secondWindow.Name = "secondWindow";
            this.secondWindow.Size = new System.Drawing.Size(351, 294);
            this.secondWindow.TabIndex = 3;
            this.secondWindow.TestText = null;
            this.secondWindow.Load += new System.EventHandler(this.secondWindow_Load);
            // 
            // thirdWindow
            // 
            this.thirdWindow.AddTestActors = false;
            this.thirdWindow.Location = new System.Drawing.Point(3, 303);
            this.thirdWindow.Name = "thirdWindow";
            this.thirdWindow.Size = new System.Drawing.Size(351, 294);
            this.thirdWindow.TabIndex = 4;
            this.thirdWindow.TestText = null;
            this.thirdWindow.Load += new System.EventHandler(this.thirdWindow_Load);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ZtrackBar2);
            this.panel1.Controls.Add(this.YtrackBar1);
            this.panel1.Controls.Add(this.XtrackBar);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.textBoxLevel1);
            this.panel1.Controls.Add(this.textBoxWidth);
            this.panel1.Controls.Add(this.labelLevel);
            this.panel1.Controls.Add(this.labelWidth);
            this.panel1.Controls.Add(this.trackBarLevel);
            this.panel1.Controls.Add(this.trackBarWidth);
            this.panel1.Location = new System.Drawing.Point(12, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(267, 608);
            this.panel1.TabIndex = 3;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(13, 195);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(203, 21);
            this.comboBox1.TabIndex = 18;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // textBoxLevel1
            // 
            this.textBoxLevel1.Location = new System.Drawing.Point(67, 94);
            this.textBoxLevel1.Name = "textBoxLevel1";
            this.textBoxLevel1.Size = new System.Drawing.Size(57, 20);
            this.textBoxLevel1.TabIndex = 17;
            this.textBoxLevel1.Text = "0";
            this.textBoxLevel1.TextChanged += new System.EventHandler(this.textBoxLevel_TextChanged);
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Location = new System.Drawing.Point(67, 16);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(57, 20);
            this.textBoxWidth.TabIndex = 16;
            this.textBoxWidth.Text = "0";
            this.textBoxWidth.TextChanged += new System.EventHandler(this.textBoxWidth_TextChanged);
            // 
            // labelLevel
            // 
            this.labelLevel.AutoSize = true;
            this.labelLevel.Location = new System.Drawing.Point(28, 101);
            this.labelLevel.Name = "labelLevel";
            this.labelLevel.Size = new System.Drawing.Size(33, 13);
            this.labelLevel.TabIndex = 12;
            this.labelLevel.Text = "Level";
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(26, 21);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(35, 13);
            this.labelWidth.TabIndex = 11;
            this.labelWidth.Text = "Width";
            // 
            // trackBarLevel
            // 
            this.trackBarLevel.Location = new System.Drawing.Point(13, 120);
            this.trackBarLevel.Maximum = 2000;
            this.trackBarLevel.Minimum = -700;
            this.trackBarLevel.Name = "trackBarLevel";
            this.trackBarLevel.Size = new System.Drawing.Size(203, 45);
            this.trackBarLevel.TabIndex = 10;
            this.trackBarLevel.Scroll += new System.EventHandler(this.trackBarLevel_Scroll);
            // 
            // trackBarWidth
            // 
            this.trackBarWidth.Location = new System.Drawing.Point(13, 50);
            this.trackBarWidth.Maximum = 2500;
            this.trackBarWidth.Name = "trackBarWidth";
            this.trackBarWidth.Size = new System.Drawing.Size(203, 45);
            this.trackBarWidth.TabIndex = 6;
            this.trackBarWidth.Scroll += new System.EventHandler(this.trackBarWidth_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 263);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "x:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 300);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "y:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 344);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "z:";
            // 
            // XtrackBar
            // 
            this.XtrackBar.Location = new System.Drawing.Point(88, 263);
            this.XtrackBar.Name = "XtrackBar";
            this.XtrackBar.Size = new System.Drawing.Size(163, 45);
            this.XtrackBar.TabIndex = 50;
            this.XtrackBar.Maximum = 200;
            this.XtrackBar.Minimum = 0;
            this.XtrackBar.Scroll += new System.EventHandler(this.XtrackBar_Scroll);
            // 
            // YtrackBar1
            // 
            this.YtrackBar1.Location = new System.Drawing.Point(88, 300);
            this.YtrackBar1.Name = "YtrackBar1";
            this.YtrackBar1.Size = new System.Drawing.Size(163, 45);
            this.YtrackBar1.TabIndex = 50;
            this.YtrackBar1.Maximum = 200;
            this.YtrackBar1.Minimum = 0;
            this.YtrackBar1.Scroll += new System.EventHandler(this.YtrackBar1_Scroll);
            // 
            // ZtrackBar2
            // 
            this.ZtrackBar2.Location = new System.Drawing.Point(88, 344);
            this.ZtrackBar2.Name = "ZtrackBar2";
            this.ZtrackBar2.Size = new System.Drawing.Size(163, 45);
            this.ZtrackBar2.TabIndex = 50;
            this.ZtrackBar2.Maximum = 200;
            this.ZtrackBar2.Minimum = 0;
            this.ZtrackBar2.Scroll += new System.EventHandler(this.ZtrackBar2_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1021, 619);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "Form1";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XtrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YtrackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ZtrackBar2)).EndInit();
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
        private System.Windows.Forms.TrackBar ZtrackBar2;
        private System.Windows.Forms.TrackBar YtrackBar1;
        private System.Windows.Forms.TrackBar XtrackBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

