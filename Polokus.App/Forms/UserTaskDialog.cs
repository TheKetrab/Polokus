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
    public partial class UserTaskDialog : Form
    {
        public string? Answer { get; private set; } = null;

        public UserTaskDialog(string userTaskDefinition)

        {
            this.StartPosition = FormStartPosition.CenterParent;

            InitializeComponent();

            InitializeOptions(userTaskDefinition);

        }

        private void InitializeOptions(string userTaskDefinition)
        {
            if (userTaskDefinition.Count(x => x == '(') != 1
                || userTaskDefinition.Count(x => x == ')') != 1)
            {
                return;
            }

            int a = userTaskDefinition.IndexOf('(');
            int b = userTaskDefinition.IndexOf(')');

            if (b <= a)
            {
                return;
            }

            string taskName = userTaskDefinition.Substring(0, a).Trim();
            this.readOnlyRichTextBoxMsg.Text = taskName;

            string taskOptions = userTaskDefinition.Substring(a+1, b - a - 1);
            var options = taskOptions.Split(';').Select(x => x.Trim());

            InitializeRadioButtons(options);
        }

        private void InitializeRadioButtons(IEnumerable<string> options)
        {
            var location = new Point(0, 0);
            foreach (var option in options)
            {
                RadioButton radiobutton = new RadioButton();
                radiobutton.Name = option;
                radiobutton.Text = option;
                radiobutton.Location = location;
                radiobutton.CheckedChanged += (s, e) =>
                {
                    if (s is RadioButton rb && rb.Checked)
                    {
                        Answer = rb.Text;
                    }
                };
                this.panelAnswers.Controls.Add(radiobutton);

                location.Y = location.Y + radiobutton.Height;
            }

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (Answer != null)
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
