namespace Polokus.App.Forms
{
    partial class ErrorDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorDialog));
            this.buttonClose = new System.Windows.Forms.Button();
            this.polokusLabel1 = new Polokus.App.Controls.PolokusLabel();
            this.polokusLabelExceptionType = new Polokus.App.Controls.PolokusLabel();
            this.polokusLabel3 = new Polokus.App.Controls.PolokusLabel();
            this.readOnlyRichTextBoxCallstack = new Polokus.App.Controls.ReadOnlyRichTextBox();
            this.polokusLabelExceptionMessage = new Polokus.App.Controls.PolokusLabel();
            this.readOnlyRichTextBoxMsg = new Polokus.App.Controls.ReadOnlyRichTextBox();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(348, 225);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
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
            this.polokusLabel1.Location = new System.Drawing.Point(12, 9);
            this.polokusLabel1.Name = "polokusLabel1";
            this.polokusLabel1.Size = new System.Drawing.Size(411, 23);
            this.polokusLabel1.TabIndex = 3;
            this.polokusLabel1.Text = "Exception occured";
            this.polokusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // polokusLabelExceptionType
            // 
            this.polokusLabelExceptionType.AutoSize = true;
            this.polokusLabelExceptionType.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.polokusLabelExceptionType.Location = new System.Drawing.Point(12, 45);
            this.polokusLabelExceptionType.Name = "polokusLabelExceptionType";
            this.polokusLabelExceptionType.Size = new System.Drawing.Size(88, 15);
            this.polokusLabelExceptionType.TabIndex = 4;
            this.polokusLabelExceptionType.Text = "Exception type:";
            // 
            // polokusLabel3
            // 
            this.polokusLabel3.AutoSize = true;
            this.polokusLabel3.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.polokusLabel3.Location = new System.Drawing.Point(12, 97);
            this.polokusLabel3.Name = "polokusLabel3";
            this.polokusLabel3.Size = new System.Drawing.Size(61, 15);
            this.polokusLabel3.TabIndex = 5;
            this.polokusLabel3.Text = "Callstack:";
            // 
            // readOnlyRichTextBoxCallstack
            // 
            this.readOnlyRichTextBoxCallstack.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.readOnlyRichTextBoxCallstack.Location = new System.Drawing.Point(12, 115);
            this.readOnlyRichTextBoxCallstack.Name = "readOnlyRichTextBoxCallstack";
            this.readOnlyRichTextBoxCallstack.Size = new System.Drawing.Size(411, 104);
            this.readOnlyRichTextBoxCallstack.TabIndex = 6;
            this.readOnlyRichTextBoxCallstack.Text = "";
            // 
            // polokusLabelExceptionMessage
            // 
            this.polokusLabelExceptionMessage.AutoSize = true;
            this.polokusLabelExceptionMessage.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.polokusLabelExceptionMessage.Location = new System.Drawing.Point(12, 71);
            this.polokusLabelExceptionMessage.Name = "polokusLabelExceptionMessage";
            this.polokusLabelExceptionMessage.Size = new System.Drawing.Size(119, 15);
            this.polokusLabelExceptionMessage.TabIndex = 7;
            this.polokusLabelExceptionMessage.Text = "Exception message:";
            // 
            // readOnlyRichTextBoxMsg
            // 
            this.readOnlyRichTextBoxMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.readOnlyRichTextBoxMsg.Location = new System.Drawing.Point(137, 68);
            this.readOnlyRichTextBoxMsg.Name = "readOnlyRichTextBoxMsg";
            this.readOnlyRichTextBoxMsg.Size = new System.Drawing.Size(286, 23);
            this.readOnlyRichTextBoxMsg.TabIndex = 8;
            this.readOnlyRichTextBoxMsg.Text = "";
            // 
            // ErrorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 260);
            this.Controls.Add(this.readOnlyRichTextBoxMsg);
            this.Controls.Add(this.polokusLabelExceptionMessage);
            this.Controls.Add(this.readOnlyRichTextBoxCallstack);
            this.Controls.Add(this.polokusLabel3);
            this.Controls.Add(this.polokusLabelExceptionType);
            this.Controls.Add(this.polokusLabel1);
            this.Controls.Add(this.buttonClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 225);
            this.Name = "ErrorDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button buttonClose;
        private Controls.PolokusLabel polokusLabel1;
        private Controls.PolokusLabel polokusLabelExceptionType;
        private Controls.PolokusLabel polokusLabel3;
        private Controls.ReadOnlyRichTextBox readOnlyRichTextBoxCallstack;
        private Controls.PolokusLabel polokusLabelExceptionMessage;
        private Controls.ReadOnlyRichTextBox readOnlyRichTextBoxMsg;
    }
}