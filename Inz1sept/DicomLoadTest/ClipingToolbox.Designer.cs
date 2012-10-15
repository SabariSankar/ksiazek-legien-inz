namespace DicomLoadTest
{
    partial class ClipingToolbox
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
            this.XClipingTrackBar2 = new System.Windows.Forms.TrackBar();
            this.XClipingTrackBar1 = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.XClipingTrackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XClipingTrackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // XClipingTrackBar2
            // 
            this.XClipingTrackBar2.Location = new System.Drawing.Point(149, 82);
            this.XClipingTrackBar2.Margin = new System.Windows.Forms.Padding(0);
            this.XClipingTrackBar2.Maximum = 200;
            this.XClipingTrackBar2.Name = "XClipingTrackBar2";
            this.XClipingTrackBar2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.XClipingTrackBar2.Size = new System.Drawing.Size(128, 45);
            this.XClipingTrackBar2.TabIndex = 66;
            // 
            // XClipingTrackBar1
            // 
            this.XClipingTrackBar1.Location = new System.Drawing.Point(21, 82);
            this.XClipingTrackBar1.Margin = new System.Windows.Forms.Padding(0);
            this.XClipingTrackBar1.Maximum = 200;
            this.XClipingTrackBar1.Name = "XClipingTrackBar1";
            this.XClipingTrackBar1.Size = new System.Drawing.Size(128, 45);
            this.XClipingTrackBar1.TabIndex = 65;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 168);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 64;
            this.label6.Text = "Z:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 63;
            this.label5.Text = "Y:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 62;
            this.label4.Text = "X:";
            // 
            // ClipingToolbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.XClipingTrackBar2);
            this.Controls.Add(this.XClipingTrackBar1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Name = "ClipingToolbox";
            this.Text = "ClipingToolbox";
            ((System.ComponentModel.ISupportInitialize)(this.XClipingTrackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XClipingTrackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar XClipingTrackBar2;
        private System.Windows.Forms.TrackBar XClipingTrackBar1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}