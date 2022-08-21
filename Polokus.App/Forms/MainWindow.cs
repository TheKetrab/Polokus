using Polokus.App.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Polokus.App.Forms
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            //this.toolTip1.SetToolTip(this.iconBtn1, "Tooltip text goes here");

            ChromiumWindow chw = new ChromiumWindow();
            chw.Parent = this.splitContainer1.Panel1;
        }

    }
}
