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
    public partial class MainForm : Form
    {
        private readonly string appName = "MADCA";
        public MainForm()
        {
            InitializeComponent();
            Text = appName;
        }
    }
}
