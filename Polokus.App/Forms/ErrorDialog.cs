namespace Polokus.App.Forms
{
    public partial class ErrorDialog : Form
    {
        public ErrorDialog(Exception e)
        {
            this.StartPosition = FormStartPosition.CenterParent;

            InitializeComponent();

            this.polokusLabelExceptionType.Text = $"Exception type: {e.GetType().Name}";
            this.readOnlyRichTextBoxMsg.Text = e.Message;
            this.readOnlyRichTextBoxCallstack.Text = e.StackTrace;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
