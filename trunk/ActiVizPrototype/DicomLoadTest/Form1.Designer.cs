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
            this.textBoxLevel1 = new System.Windows.Forms.TextBox();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.labelThird = new System.Windows.Forms.Label();
            this.labelSecond = new System.Windows.Forms.Label();
            this.labelFirst = new System.Windows.Forms.Label();
            this.labelLevel = new System.Windows.Forms.Label();
            this.labelWidth = new System.Windows.Forms.Label();
            this.trackBarLevel = new System.Windows.Forms.TrackBar();
            this.trackBarThird = new System.Windows.Forms.TrackBar();
            this.trackBarSecond = new System.Windows.Forms.TrackBar();
            this.trackBarFirst = new System.Windows.Forms.TrackBar();
            this.trackBarWidth = new System.Windows.Forms.TrackBar();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThird)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSecond)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFirst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWidth)).BeginInit();
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
            this.panel1.Controls.Add(this.textBoxLevel1);
            this.panel1.Controls.Add(this.textBoxWidth);
            this.panel1.Controls.Add(this.labelThird);
            this.panel1.Controls.Add(this.labelSecond);
            this.panel1.Controls.Add(this.labelFirst);
            this.panel1.Controls.Add(this.labelLevel);
            this.panel1.Controls.Add(this.labelWidth);
            this.panel1.Controls.Add(this.trackBarLevel);
            this.panel1.Controls.Add(this.trackBarThird);
            this.panel1.Controls.Add(this.trackBarSecond);
            this.panel1.Controls.Add(this.trackBarFirst);
            this.panel1.Controls.Add(this.trackBarWidth);
            this.panel1.Location = new System.Drawing.Point(12, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(254, 608);
            this.panel1.TabIndex = 3;
            // 
            // textBoxLevel
            // 
            this.textBoxLevel1.Location = new System.Drawing.Point(80, 293);
            this.textBoxLevel1.Name = "textBoxLevel";
            this.textBoxLevel1.Size = new System.Drawing.Size(57, 20);
            this.textBoxLevel1.TabIndex = 17;
            this.textBoxLevel1.Text = "0";
            this.trackBarLevel.Value = 0;
            this.textBoxLevel1.TextChanged += new System.EventHandler(this.textBoxLevel_TextChanged);
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Location = new System.Drawing.Point(80, 215);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(57, 20);
            this.textBoxWidth.TabIndex = 16;
            this.textBoxWidth.Text = "0";
            this.trackBarWidth.Value = 0;
            this.textBoxWidth.TextChanged += new System.EventHandler(this.textBoxWidth_TextChanged);
            // 
            // labelThird
            // 
            this.labelThird.AutoSize = true;
            this.labelThird.Location = new System.Drawing.Point(41, 558);
            this.labelThird.Name = "labelThird";
            this.labelThird.Size = new System.Drawing.Size(13, 13);
            this.labelThird.TabIndex = 15;
            this.labelThird.Text = "3";
            // 
            // labelSecond
            // 
            this.labelSecond.AutoSize = true;
            this.labelSecond.Location = new System.Drawing.Point(41, 501);
            this.labelSecond.Name = "labelSecond";
            this.labelSecond.Size = new System.Drawing.Size(13, 13);
            this.labelSecond.TabIndex = 14;
            this.labelSecond.Text = "2";
            // 
            // labelFirst
            // 
            this.labelFirst.AutoSize = true;
            this.labelFirst.Location = new System.Drawing.Point(41, 447);
            this.labelFirst.Name = "labelFirst";
            this.labelFirst.Size = new System.Drawing.Size(13, 13);
            this.labelFirst.TabIndex = 13;
            this.labelFirst.Text = "1";
            // 
            // labelLevel
            // 
            this.labelLevel.AutoSize = true;
            this.labelLevel.Location = new System.Drawing.Point(41, 300);
            this.labelLevel.Name = "labelLevel";
            this.labelLevel.Size = new System.Drawing.Size(33, 13);
            this.labelLevel.TabIndex = 12;
            this.labelLevel.Text = "Level";
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(39, 220);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(35, 13);
            this.labelWidth.TabIndex = 11;
            this.labelWidth.Text = "Width";
            // 
            // trackBarLevel
            // 
            this.trackBarLevel.Location = new System.Drawing.Point(26, 325);
            this.trackBarLevel.Maximum = 1500;
            this.trackBarLevel.Minimum = -700;
            this.trackBarLevel.Name = "trackBarLevel";
            this.trackBarLevel.Size = new System.Drawing.Size(203, 45);
            this.trackBarLevel.TabIndex = 10;
            this.trackBarLevel.Scroll += new System.EventHandler(this.trackBarLevel_Scroll);
            // 
            // trackBarThird
            // 
            this.trackBarThird.Location = new System.Drawing.Point(108, 549);
            this.trackBarThird.Name = "trackBarThird";
            this.trackBarThird.Size = new System.Drawing.Size(104, 45);
            this.trackBarThird.TabIndex = 9;
            this.trackBarThird.Scroll += new System.EventHandler(this.trackBarThird_Scroll);
            // 
            // trackBarSecond
            // 
            this.trackBarSecond.Location = new System.Drawing.Point(108, 498);
            this.trackBarSecond.Name = "trackBarSecond";
            this.trackBarSecond.Size = new System.Drawing.Size(104, 45);
            this.trackBarSecond.TabIndex = 8;
            this.trackBarSecond.Scroll += new System.EventHandler(this.trackBarSecond_Scroll);
            // 
            // trackBarFirst
            // 
            this.trackBarFirst.Location = new System.Drawing.Point(108, 447);
            this.trackBarFirst.Name = "trackBarFirst";
            this.trackBarFirst.Size = new System.Drawing.Size(104, 45);
            this.trackBarFirst.TabIndex = 7;
            this.trackBarFirst.Scroll += new System.EventHandler(this.trackBarFirst_Scroll);
            // 
            // trackBarWidth
            // 
            this.trackBarWidth.Location = new System.Drawing.Point(26, 249);
            this.trackBarWidth.Maximum = 1000;
            this.trackBarWidth.Name = "trackBarWidth";
            this.trackBarWidth.Size = new System.Drawing.Size(203, 45);
            this.trackBarWidth.TabIndex = 6;
            this.trackBarWidth.Scroll += new System.EventHandler(this.trackBarWidth_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1021, 619);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThird)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSecond)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFirst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWidth)).EndInit();
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
        private System.Windows.Forms.TrackBar trackBarThird;
        private System.Windows.Forms.TrackBar trackBarSecond;
        private System.Windows.Forms.TrackBar trackBarFirst;
        private System.Windows.Forms.TrackBar trackBarWidth;
        private System.Windows.Forms.Label labelThird;
        private System.Windows.Forms.Label labelSecond;
        private System.Windows.Forms.Label labelFirst;
        private System.Windows.Forms.TextBox textBoxLevel1;
        private System.Windows.Forms.TextBox textBoxWidth;
    }
}

