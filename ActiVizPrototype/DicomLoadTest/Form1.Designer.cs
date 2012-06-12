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
            this.renderWindowControl1 = new Kitware.VTK.RenderWindowControl();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.renderWindowControl4 = new Kitware.VTK.RenderWindowControl();
            this.renderWindowControl2 = new Kitware.VTK.RenderWindowControl();
            this.renderWindowControl3 = new Kitware.VTK.RenderWindowControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // renderWindowControl1
            // 
            this.renderWindowControl1.AddTestActors = false;
            this.renderWindowControl1.Location = new System.Drawing.Point(360, 303);
            this.renderWindowControl1.Name = "renderWindowControl1";
            this.renderWindowControl1.Size = new System.Drawing.Size(351, 294);
            this.renderWindowControl1.TabIndex = 0;
            this.renderWindowControl1.TestText = null;
            this.renderWindowControl1.Load += new System.EventHandler(this.renderWindowControl1_Load);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.renderWindowControl4);
            this.flowLayoutPanel1.Controls.Add(this.renderWindowControl2);
            this.flowLayoutPanel1.Controls.Add(this.renderWindowControl3);
            this.flowLayoutPanel1.Controls.Add(this.renderWindowControl1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(282, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(734, 611);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // renderWindowControl4
            // 
            this.renderWindowControl4.AddTestActors = false;
            this.renderWindowControl4.Location = new System.Drawing.Point(3, 3);
            this.renderWindowControl4.Name = "renderWindowControl4";
            this.renderWindowControl4.Size = new System.Drawing.Size(351, 294);
            this.renderWindowControl4.TabIndex = 4;
            this.renderWindowControl4.TestText = null;
            this.renderWindowControl4.Load += new System.EventHandler(this.renderWindowControl4_Load);
            // 
            // renderWindowControl2
            // 
            this.renderWindowControl2.AddTestActors = false;
            this.renderWindowControl2.Location = new System.Drawing.Point(360, 3);
            this.renderWindowControl2.Name = "renderWindowControl2";
            this.renderWindowControl2.Size = new System.Drawing.Size(351, 294);
            this.renderWindowControl2.TabIndex = 3;
            this.renderWindowControl2.TestText = null;
            // 
            // renderWindowControl3
            // 
            this.renderWindowControl3.AddTestActors = false;
            this.renderWindowControl3.Location = new System.Drawing.Point(3, 303);
            this.renderWindowControl3.Name = "renderWindowControl3";
            this.renderWindowControl3.Size = new System.Drawing.Size(351, 294);
            this.renderWindowControl3.TabIndex = 4;
            this.renderWindowControl3.TestText = null;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBox2);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(254, 608);
            this.panel1.TabIndex = 3;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "-700",
            "350",
            "40",
            "1000",
            "10"});
            this.comboBox2.Location = new System.Drawing.Point(108, 130);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 5;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1000",
            "2000",
            "400",
            "30",
            "10",
            "5"});
            this.comboBox1.Location = new System.Drawing.Point(108, 80);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Window level:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Window width: ";
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
            this.ResumeLayout(false);

        }

        #endregion

        private Kitware.VTK.RenderWindowControl renderWindowControl1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Kitware.VTK.RenderWindowControl renderWindowControl2;
        private Kitware.VTK.RenderWindowControl renderWindowControl3;
        private Kitware.VTK.RenderWindowControl renderWindowControl4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
    }
}

