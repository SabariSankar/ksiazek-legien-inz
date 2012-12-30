using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DrawingModule
{

    public class DrawingToolbox : Panel
    {
        public Button ExportImageButton;
        public Button ClearButton;
        public CheckBox DrawnigModeEnabled;
        private Label titleLabel;

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

            _panel.Controls.Add(titleLabel);
            _panel.SetCellPosition(titleLabel, new TableLayoutPanelCellPosition(0,0));

            _panel.Controls.Add(DrawnigModeEnabled);
            _panel.SetCellPosition(DrawnigModeEnabled, new TableLayoutPanelCellPosition(0,1));

            _panel.Controls.Add(ClearButton);
            _panel.SetCellPosition(ClearButton, new TableLayoutPanelCellPosition(0, 2));
            _panel.Controls.Add(ExportImageButton);
            _panel.SetCellPosition(ExportImageButton, new TableLayoutPanelCellPosition(1, 2));

            Enabled = false;
            DrawnigModeEnabled.Enabled = false;
        }

        private void InitializeComponent()
        {
            this.ExportImageButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.DrawnigModeEnabled = new System.Windows.Forms.CheckBox();
            this.titleLabel = new System.Windows.Forms.Label();
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
            // DrawnigModeEnabled
            // 
            this.DrawnigModeEnabled.AutoSize = true;
            this.DrawnigModeEnabled.Location = new System.Drawing.Point(0, 0);
            this.DrawnigModeEnabled.Name = "DrawnigModeEnabled";
            this.DrawnigModeEnabled.Size = new System.Drawing.Size(104, 24);
            this.DrawnigModeEnabled.TabIndex = 0;
            this.DrawnigModeEnabled.Text = "Enabled";
            this.DrawnigModeEnabled.UseVisualStyleBackColor = true;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(0, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(100, 23);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Drawing:";
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
                ClearButton.Enabled = _enabled;
                ExportImageButton.Enabled = _enabled;
            }
        }
    }
}
