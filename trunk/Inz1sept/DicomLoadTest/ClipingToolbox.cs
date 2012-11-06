using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MainWindow
{
    public partial class ClipingToolbox : Panel
    {
        public delegate void EventHandler(object sender, ClipingEventArgs e);
        public EventHandler<ClipingEventArgs> ClipingOperationEventHandlerDelegate;

        public ClipingToolbox()
        {
            InitializeComponent();
        }

        public void InitialiseClipingToolbox(IList<double> sizeList)
        {
            if (sizeList.Count != 3)
                throw new ArgumentException("Invalid size List length. Expected: 3 for all dimensions.");
            XClipingTrackBar1.Maximum = (int)(sizeList[0] / 2);
            XClipingTrackBar2.Maximum = (int)(sizeList[0] / 2);

            YClipingTrackBar1.Maximum = (int)(sizeList[1] / 2);
            YClipingTrackBar2.Maximum = (int)(sizeList[1] / 2);

            ZClipingTrackBar1.Maximum = (int)(sizeList[2] / 2);
            ZClipingTrackBar2.Maximum = (int)(sizeList[2] / 2);
        }

        private void InvokeOnHandler(EClipingModuleOperationType type, int value)
        {
            if (this.ClipingOperationEventHandlerDelegate != null)
            {
                EventArgs e2 = new ClipingEventArgs()
                {
                    Type = type,
                    Position = value,
                };
                object[] args = new object[] { null, e2 };
                foreach (Delegate handler in this.ClipingOperationEventHandlerDelegate.GetInvocationList())
                {
                    handler.DynamicInvoke(args);
                }
            }
        }

        private void XClipingTrackBar1_Scroll(object sender, EventArgs e)
        {
            InvokeOnHandler(EClipingModuleOperationType.X1, XClipingTrackBar1.Value);
        }
     
        private void XClipingTrackBar2_Scroll(object sender, EventArgs e)
        {
            InvokeOnHandler(EClipingModuleOperationType.X2, XClipingTrackBar2.Value);
        }

        private void YClipingTrackBar1_Scroll(object sender, EventArgs e)
        {
            InvokeOnHandler(EClipingModuleOperationType.Y1, YClipingTrackBar1.Value);
        }

        private void YClipingTrackBar2_Scroll(object sender, EventArgs e)
        {
            InvokeOnHandler(EClipingModuleOperationType.Y2, YClipingTrackBar2.Value);
        }

        private void ZClipingTrackBar1_Scroll(object sender, EventArgs e)
        {
            InvokeOnHandler(EClipingModuleOperationType.Z1, ZClipingTrackBar1.Value);
        }

        private void ZClipingTrackBar2_Scroll(object sender, EventArgs e)
        {
            InvokeOnHandler(EClipingModuleOperationType.Z2, ZClipingTrackBar2.Value);
        }
  
    }
}
