namespace Polokus.App.Views
{
    partial class XmlView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.readOnlyRichTextBox1 = new Polokus.App.Controls.ReadOnlyRichTextBox();
            this.SuspendLayout();
            // 
            // readOnlyRichTextBox1
            // 
            this.readOnlyRichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.readOnlyRichTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.readOnlyRichTextBox1.Location = new System.Drawing.Point(0, 0);
            this.readOnlyRichTextBox1.Name = "readOnlyRichTextBox1";
            this.readOnlyRichTextBox1.Size = new System.Drawing.Size(1045, 638);
            this.readOnlyRichTextBox1.TabIndex = 1;
            this.readOnlyRichTextBox1.Text = "";
            // 
            // XmlView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.readOnlyRichTextBox1);
            this.Name = "XmlView";
            this.Size = new System.Drawing.Size(1045, 638);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ReadOnlyRichTextBox readOnlyRichTextBox1;
    }
}
