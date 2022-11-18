namespace Polokus.App.Forms
{
    partial class DownloadedDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownloadedDialog));
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonOpenLocation = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.polokusLabel1 = new Polokus.App.Controls.PolokusLabel();
            this.polokusLabel2 = new Polokus.App.Controls.PolokusLabel();
            this.polokusLabel3 = new Polokus.App.Controls.PolokusLabel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(67, 141);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(75, 23);
            this.buttonOpen.TabIndex = 0;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            // 
            // buttonOpenLocation
            // 
            this.buttonOpenLocation.Location = new System.Drawing.Point(148, 141);
            this.buttonOpenLocation.Name = "buttonOpenLocation";
            this.buttonOpenLocation.Size = new System.Drawing.Size(98, 23);
            this.buttonOpenLocation.TabIndex = 1;
            this.buttonOpenLocation.Text = "Show in folder";
            this.buttonOpenLocation.UseVisualStyleBackColor = true;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(252, 141);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "OK";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // polokusLabel1
            // 
            this.polokusLabel1.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.polokusLabel1.Location = new System.Drawing.Point(12, 9);
            this.polokusLabel1.Name = "polokusLabel1";
            this.polokusLabel1.Size = new System.Drawing.Size(315, 23);
            this.polokusLabel1.TabIndex = 3;
            this.polokusLabel1.Text = "Download complete";
            this.polokusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // polokusLabel2
            // 
            this.polokusLabel2.AutoSize = true;
            this.polokusLabel2.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.polokusLabel2.Location = new System.Drawing.Point(12, 45);
            this.polokusLabel2.Name = "polokusLabel2";
            this.polokusLabel2.Size = new System.Drawing.Size(55, 15);
            this.polokusLabel2.TabIndex = 4;
            this.polokusLabel2.Text = "File type:";
            // 
            // polokusLabel3
            // 
            this.polokusLabel3.AutoSize = true;
            this.polokusLabel3.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.polokusLabel3.Location = new System.Drawing.Point(12, 70);
            this.polokusLabel3.Name = "polokusLabel3";
            this.polokusLabel3.Size = new System.Drawing.Size(76, 15);
            this.polokusLabel3.TabIndex = 5;
            this.polokusLabel3.Text = "File location:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 88);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(315, 23);
            this.textBox1.TabIndex = 6;
            // 
            // DownloadedDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 176);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.polokusLabel3);
            this.Controls.Add(this.polokusLabel2);
            this.Controls.Add(this.polokusLabel1);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonOpenLocation);
            this.Controls.Add(this.buttonOpen);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DownloadedDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button buttonOpen;
        private Button buttonOpenLocation;
        private Button buttonClose;
        private Controls.PolokusLabel polokusLabel1;
        private Controls.PolokusLabel polokusLabel2;
        private Controls.PolokusLabel polokusLabel3;
        private TextBox textBox1;
    }
}