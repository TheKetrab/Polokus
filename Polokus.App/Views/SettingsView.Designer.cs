namespace Polokus.App.Views
{
    partial class SettingsView
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonSaveSettings = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxEnableLogs = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonBrowseBpmnPath = new System.Windows.Forms.Button();
            this.textBoxBpmnPath = new System.Windows.Forms.TextBox();
            this.polokusLabel1 = new Polokus.App.Controls.PolokusLabel();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonSaveSettings);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(840, 589);
            this.panel1.TabIndex = 0;
            // 
            // buttonSaveSettings
            // 
            this.buttonSaveSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveSettings.Location = new System.Drawing.Point(762, 563);
            this.buttonSaveSettings.Name = "buttonSaveSettings";
            this.buttonSaveSettings.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveSettings.TabIndex = 2;
            this.buttonSaveSettings.Text = "Save";
            this.buttonSaveSettings.UseVisualStyleBackColor = true;
            this.buttonSaveSettings.Click += new System.EventHandler(this.buttonSaveSettings_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.checkBoxEnableLogs);
            this.groupBox2.Location = new System.Drawing.Point(3, 130);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(834, 121);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Application options";
            // 
            // checkBoxEnableLogs
            // 
            this.checkBoxEnableLogs.AutoSize = true;
            this.checkBoxEnableLogs.Location = new System.Drawing.Point(6, 22);
            this.checkBoxEnableLogs.Name = "checkBoxEnableLogs";
            this.checkBoxEnableLogs.Size = new System.Drawing.Size(86, 19);
            this.checkBoxEnableLogs.TabIndex = 0;
            this.checkBoxEnableLogs.Text = "Enable logs";
            this.checkBoxEnableLogs.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonBrowseBpmnPath);
            this.groupBox1.Controls.Add(this.textBoxBpmnPath);
            this.groupBox1.Controls.Add(this.polokusLabel1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(834, 121);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bpmn options";
            // 
            // buttonBrowseBpmnPath
            // 
            this.buttonBrowseBpmnPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseBpmnPath.Location = new System.Drawing.Point(753, 16);
            this.buttonBrowseBpmnPath.Name = "buttonBrowseBpmnPath";
            this.buttonBrowseBpmnPath.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseBpmnPath.TabIndex = 2;
            this.buttonBrowseBpmnPath.Text = "Browse";
            this.buttonBrowseBpmnPath.UseVisualStyleBackColor = true;
            this.buttonBrowseBpmnPath.Click += new System.EventHandler(this.buttonBrowseBpmnPath_Click);
            // 
            // textBoxBpmnPath
            // 
            this.textBoxBpmnPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBpmnPath.Location = new System.Drawing.Point(133, 16);
            this.textBoxBpmnPath.Name = "textBoxBpmnPath";
            this.textBoxBpmnPath.Size = new System.Drawing.Size(614, 23);
            this.textBoxBpmnPath.TabIndex = 1;
            // 
            // polokusLabel1
            // 
            this.polokusLabel1.AutoSize = true;
            this.polokusLabel1.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.polokusLabel1.Location = new System.Drawing.Point(6, 19);
            this.polokusLabel1.Name = "polokusLabel1";
            this.polokusLabel1.Size = new System.Drawing.Size(121, 15);
            this.polokusLabel1.TabIndex = 0;
            this.polokusLabel1.Text = "Path with bpmn files:";
            // 
            // SettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "SettingsView";
            this.Size = new System.Drawing.Size(840, 589);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private GroupBox groupBox2;
        private CheckBox checkBoxEnableLogs;
        private GroupBox groupBox1;
        private Button buttonBrowseBpmnPath;
        private TextBox textBoxBpmnPath;
        private Controls.PolokusLabel polokusLabel1;
        private Button buttonSaveSettings;
    }
}
