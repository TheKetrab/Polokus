namespace Polokus.App.Forms
{
    partial class UserTaskDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserTaskDialog));
            this.buttonClose = new System.Windows.Forms.Button();
            this.polokusLabel1 = new Polokus.App.Controls.PolokusLabel();
            this.polokusLabelTaskName = new Polokus.App.Controls.PolokusLabel();
            this.polokusLabelPossibleAnswers = new Polokus.App.Controls.PolokusLabel();
            this.readOnlyRichTextBoxMsg = new Polokus.App.Controls.ReadOnlyRichTextBox();
            this.panelAnswers = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(398, 300);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(86, 31);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "OK";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // polokusLabel1
            // 
            this.polokusLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.polokusLabel1.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.polokusLabel1.Location = new System.Drawing.Point(14, 12);
            this.polokusLabel1.Name = "polokusLabel1";
            this.polokusLabel1.Size = new System.Drawing.Size(470, 31);
            this.polokusLabel1.TabIndex = 3;
            this.polokusLabel1.Text = "User task";
            this.polokusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // polokusLabelTaskName
            // 
            this.polokusLabelTaskName.AutoSize = true;
            this.polokusLabelTaskName.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.polokusLabelTaskName.Location = new System.Drawing.Point(14, 60);
            this.polokusLabelTaskName.Name = "polokusLabelTaskName";
            this.polokusLabelTaskName.Size = new System.Drawing.Size(83, 17);
            this.polokusLabelTaskName.TabIndex = 4;
            this.polokusLabelTaskName.Text = "Task name:";
            // 
            // polokusLabelPossibleAnswers
            // 
            this.polokusLabelPossibleAnswers.AutoSize = true;
            this.polokusLabelPossibleAnswers.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.polokusLabelPossibleAnswers.Location = new System.Drawing.Point(14, 95);
            this.polokusLabelPossibleAnswers.Name = "polokusLabelPossibleAnswers";
            this.polokusLabelPossibleAnswers.Size = new System.Drawing.Size(134, 17);
            this.polokusLabelPossibleAnswers.TabIndex = 7;
            this.polokusLabelPossibleAnswers.Text = "Choose one option:";
            // 
            // readOnlyRichTextBoxMsg
            // 
            this.readOnlyRichTextBoxMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.readOnlyRichTextBoxMsg.Location = new System.Drawing.Point(103, 55);
            this.readOnlyRichTextBoxMsg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.readOnlyRichTextBoxMsg.Name = "readOnlyRichTextBoxMsg";
            this.readOnlyRichTextBoxMsg.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.readOnlyRichTextBoxMsg.Size = new System.Drawing.Size(380, 29);
            this.readOnlyRichTextBoxMsg.TabIndex = 8;
            this.readOnlyRichTextBoxMsg.Text = "";
            this.readOnlyRichTextBoxMsg.WordWrap = false;
            // 
            // panelAnswers
            // 
            this.panelAnswers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAnswers.AutoScroll = true;
            this.panelAnswers.Location = new System.Drawing.Point(12, 115);
            this.panelAnswers.Name = "panelAnswers";
            this.panelAnswers.Size = new System.Drawing.Size(471, 178);
            this.panelAnswers.TabIndex = 9;
            // 
            // UserTaskDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 347);
            this.Controls.Add(this.panelAnswers);
            this.Controls.Add(this.readOnlyRichTextBoxMsg);
            this.Controls.Add(this.polokusLabelPossibleAnswers);
            this.Controls.Add(this.polokusLabelTaskName);
            this.Controls.Add(this.polokusLabel1);
            this.Controls.Add(this.buttonClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(340, 284);
            this.Name = "UserTaskDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button buttonClose;
        private Controls.PolokusLabel polokusLabel1;
        private Controls.PolokusLabel polokusLabelTaskName;
        private Controls.PolokusLabel polokusLabelPossibleAnswers;
        private Controls.ReadOnlyRichTextBox readOnlyRichTextBoxMsg;
        private Panel panelAnswers;
    }
}