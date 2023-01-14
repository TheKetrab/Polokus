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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.labelProcessesCountVal = new System.Windows.Forms.Label();
            this.labelFilenameVal = new System.Windows.Forms.Label();
            this.labelProcessesCount = new System.Windows.Forms.Label();
            this.labelFilename = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panelInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // readOnlyRichTextBox1
            // 
            this.readOnlyRichTextBox1.BackColor = System.Drawing.Color.White;
            this.readOnlyRichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.readOnlyRichTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.readOnlyRichTextBox1.Location = new System.Drawing.Point(0, 60);
            this.readOnlyRichTextBox1.Name = "readOnlyRichTextBox1";
            this.readOnlyRichTextBox1.Size = new System.Drawing.Size(1045, 578);
            this.readOnlyRichTextBox1.TabIndex = 1;
            this.readOnlyRichTextBox1.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.readOnlyRichTextBox1);
            this.panel1.Controls.Add(this.panelInfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1045, 638);
            this.panel1.TabIndex = 2;
            // 
            // panelInfo
            // 
            this.panelInfo.BackColor = System.Drawing.Color.White;
            this.panelInfo.Controls.Add(this.labelProcessesCountVal);
            this.panelInfo.Controls.Add(this.labelFilenameVal);
            this.panelInfo.Controls.Add(this.labelProcessesCount);
            this.panelInfo.Controls.Add(this.labelFilename);
            this.panelInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInfo.Location = new System.Drawing.Point(0, 0);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(1045, 60);
            this.panelInfo.TabIndex = 3;
            // 
            // labelProcessesCountVal
            // 
            this.labelProcessesCountVal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelProcessesCountVal.AutoSize = true;
            this.labelProcessesCountVal.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelProcessesCountVal.Location = new System.Drawing.Point(102, 28);
            this.labelProcessesCountVal.Name = "labelProcessesCountVal";
            this.labelProcessesCountVal.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.labelProcessesCountVal.Size = new System.Drawing.Size(10, 19);
            this.labelProcessesCountVal.TabIndex = 5;
            this.labelProcessesCountVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelFilenameVal
            // 
            this.labelFilenameVal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelFilenameVal.AutoSize = true;
            this.labelFilenameVal.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelFilenameVal.Location = new System.Drawing.Point(102, 9);
            this.labelFilenameVal.Name = "labelFilenameVal";
            this.labelFilenameVal.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.labelFilenameVal.Size = new System.Drawing.Size(10, 19);
            this.labelFilenameVal.TabIndex = 4;
            this.labelFilenameVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelProcessesCount
            // 
            this.labelProcessesCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelProcessesCount.AutoSize = true;
            this.labelProcessesCount.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelProcessesCount.Location = new System.Drawing.Point(3, 28);
            this.labelProcessesCount.Name = "labelProcessesCount";
            this.labelProcessesCount.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.labelProcessesCount.Size = new System.Drawing.Size(83, 19);
            this.labelProcessesCount.TabIndex = 3;
            this.labelProcessesCount.Text = "Processes:";
            this.labelProcessesCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelFilename
            // 
            this.labelFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelFilename.AutoSize = true;
            this.labelFilename.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelFilename.Location = new System.Drawing.Point(3, 9);
            this.labelFilename.Name = "labelFilename";
            this.labelFilename.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.labelFilename.Size = new System.Drawing.Size(83, 19);
            this.labelFilename.TabIndex = 0;
            this.labelFilename.Text = "Workflow:";
            this.labelFilename.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // XmlView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "XmlView";
            this.Size = new System.Drawing.Size(1045, 638);
            this.panel1.ResumeLayout(false);
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ReadOnlyRichTextBox readOnlyRichTextBox1;
        private Panel panel1;
        private Panel panelInfo;
        private Label labelFilename;
        private Label labelProcessesCount;
        private Label labelProcessesCountVal;
        private Label labelFilenameVal;
    }
}
