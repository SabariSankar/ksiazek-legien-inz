using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DrawingModule
{

    public class DrawingToolbox : GroupBox
    {
        public Button ExportImageButton;
        public Button ClearButton;

        private TableLayoutPanel _panel;

        public DrawingToolbox()
            : base()
        {

            _panel = new TableLayoutPanel();
            _panel.AutoSize = true;
            _panel.Dock = DockStyle.Top;
            this.Controls.Add(_panel);

            InitializeComponent();

            _panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            _panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            _panel.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            _panel.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            _panel.Controls.Add(ClearButton);
            _panel.SetCellPosition(ClearButton, new TableLayoutPanelCellPosition(0, 0));
            _panel.Controls.Add(ExportImageButton);
            _panel.SetCellPosition(ExportImageButton, new TableLayoutPanelCellPosition(1, 0));

            Enabled = false;
        }

        private void InitializeComponent()
        {
            this.ExportImageButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ExportImageButton
            // 
            this.ExportImageButton.Location = new System.Drawing.Point(0, 0);
            this.ExportImageButton.Name = "ExportImageButton";
            this.ExportImageButton.Size = new System.Drawing.Size(75, 23);
            this.ExportImageButton.TabIndex = 0;
            this.ExportImageButton.Text = "Export";
            this.ExportImageButton.UseVisualStyleBackColor = true;
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(0, 0);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(75, 23);
            this.ClearButton.TabIndex = 0;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = true;
            // 
            // DrawingToolbox
            // 
            this.Text = "DrawingToolbox";
            this.ResumeLayout(false);

        }

        private bool _enabled;
        public new bool Enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;
                foreach (var component in _panel.Controls)
                {
                    var control = component as Control;
                    if(control != null)
                        control.Enabled = _enabled;
                }
            }
        }
    }
}
