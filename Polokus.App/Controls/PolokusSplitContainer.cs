using Polokus.App.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Polokus.App.Controls
{
    public class PolokusSplitContainer : System.Windows.Forms.SplitContainer
    {
        public PolokusSplitContainer()
        {
            this.BorderStyle = BorderStyle.FixedSingle;
            this.SplitterWidth = 4;
        }

    }
}
