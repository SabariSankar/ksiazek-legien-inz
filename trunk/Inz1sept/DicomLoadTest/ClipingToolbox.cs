using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DicomLoadTest
{
    public partial class ClipingToolbox : Form
    {
        public ClipingToolbox(IList<double> sizeList)
        {
            InitializeComponent();
            if (sizeList.Count != 3)
                throw new ArgumentException("Invalid size List length. Expected: 3 for all dimensions.");
            XClipingTrackBar1.Maximum = (int)(sizeList[0] / 2);
            XClipingTrackBar2.Maximum = (int)(sizeList[0] / 2);
        }
    }
}
