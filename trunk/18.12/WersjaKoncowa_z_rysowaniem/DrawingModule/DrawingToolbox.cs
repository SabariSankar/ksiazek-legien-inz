using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DrawingModule
{
    /// <summary>
    /// Panel with check box for turning drawing mode on/off and Export/Clear buttons.
    /// Contains definition of panel layout, but not the buttons or check box actions.
    /// </summary>
    public class DrawingToolbox : Panel
    {
        /// <summary>
        /// If checked drawing is enabled.
        /// </summary>
        public CheckBox DrawnigModeEnabled;

        /// <summary>
        /// On click save the drawing area content into the file. Need user on-click action definition.
        /// </summary>
        public Button ExportImageButton;

        /// <summary>
        /// On click clears drawing area. Need user on-click action definition.
        /// </summary>
        public Button ClearButton;
        
        private Label _titleLabel;
        private TableLayoutPanel _panel;

        /// <summary>
        /// Constructor.
        /// </summary>
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

            _panel.Controls.Add(_titleLabel);
            _panel.SetCellPosition(_titleLabel, new TableLayoutPanelCellPosition(0,0));

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
            this._titleLabel = new System.Windows.Forms.Label();
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
            // _titleLabel
            // 
            this._titleLabel.AutoSize = true;
            this._titleLabel.Location = new System.Drawing.Point(0, 0);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(100, 23);
            this._titleLabel.TabIndex = 0;
            this._titleLabel.Text = "Drawing:";
            // 
            // DrawingToolbox
            // 
            this.Text = "DrawingToolbox";
            this.ResumeLayout(false);

        }

        private bool _enabled;

        /// <summary>
        /// If Clear button and ExportImage button are enabled/disabled; 
        /// </summary>
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
