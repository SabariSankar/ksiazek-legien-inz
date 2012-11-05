using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MainWindow
{
    public partial class ClipingToolbox : Form
    {
        private readonly ClipingModule _module;

        public ClipingToolbox(IList<double> sizeList, ClipingModule module)
        {
            InitializeComponent();

            _module = module;

            if (sizeList.Count != 3)
                throw new ArgumentException("Invalid size List length. Expected: 3 for all dimensions.");
            XClipingTrackBar1.Maximum = (int)(sizeList[0] / 2);
            XClipingTrackBar2.Maximum = (int)(sizeList[0] / 2);

            YClipingTrackBar1.Maximum = (int)(sizeList[1] / 2);
            YClipingTrackBar2.Maximum = (int)(sizeList[1] / 2);

            ZClipingTrackBar1.Maximum = (int)(sizeList[2] / 2);
            ZClipingTrackBar2.Maximum = (int)(sizeList[2] / 2);
        }

        private void XClipingTrackBar1_Scroll(object sender, EventArgs e)
        {
            _module.ExecuteClipingOperation(new ClipingEventArgs()
                                                {
                                                    Type = EClipingModuleOperationType.X1,
                                                    Position = XClipingTrackBar1.Value,
                                                });
        }

        private void XClipingTrackBar2_Scroll(object sender, EventArgs e)
        {
            _module.ExecuteClipingOperation(new ClipingEventArgs()
            {
                Type = EClipingModuleOperationType.X2,
                Position = XClipingTrackBar2.Value,
            });
        }

        private void YClipingTrackBar1_Scroll(object sender, EventArgs e)
        {
            _module.ExecuteClipingOperation(new ClipingEventArgs()
            {
                Type = EClipingModuleOperationType.Y1,
                Position = YClipingTrackBar1.Value,
            });
        }

        private void YClipingTrackBar2_Scroll(object sender, EventArgs e)
        {
            _module.ExecuteClipingOperation(new ClipingEventArgs()
            {
                Type = EClipingModuleOperationType.Y2,
                Position = YClipingTrackBar2.Value,
            });
        }

        private void ZClipingTrackBar1_Scroll(object sender, EventArgs e)
        {
            _module.ExecuteClipingOperation(new ClipingEventArgs()
            {
                Type = EClipingModuleOperationType.Z1,
                Position = ZClipingTrackBar1.Value,
            });
        }

        private void ZClipingTrackBar2_Scroll(object sender, EventArgs e)
        {
            _module.ExecuteClipingOperation(new ClipingEventArgs()
            {
                Type = EClipingModuleOperationType.Z2,
                Position = ZClipingTrackBar2.Value,
            });
        }
    }
}
