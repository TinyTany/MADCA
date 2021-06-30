using MADCA.Core.Data;
using MADCA.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MADCA.UI
{
    public partial class BeatStrideDialog : Form
    {
        public event Action<object, TimingPosition> BeatStrideConfirmed;
        public BeatStrideDialog()
        {
            InitializeComponent();
            bOK.Click += (s, e) =>
            {
                BeatStrideConfirmed.Invoke(this, new TimingPosition(nudBeatStride.Value.ToUInt(), 1));
                Close();
            };
            bCancel.Click += (s, e) =>
            {
                Close();
            };
        }
    }
}
