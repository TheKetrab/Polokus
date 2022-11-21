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
            this.numericUpDownMessageListenerPort = new System.Windows.Forms.NumericUpDown();
            this.polokusLabel2 = new Polokus.App.Controls.PolokusLabel();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMessageListenerPort)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonSaveSettings);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(960, 785);
            this.panel1.TabIndex = 0;
            // 
            // buttonSaveSettings
            // 
            this.buttonSaveSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveSettings.Location = new System.Drawing.Point(871, 751);
            this.buttonSaveSettings.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonSaveSettings.Name = "buttonSaveSettings";
            this.buttonSaveSettings.Size = new System.Drawing.Size(86, 31);
            this.buttonSaveSettings.TabIndex = 2;
            this.buttonSaveSettings.Text = "Save";
            this.buttonSaveSettings.UseVisualStyleBackColor = true;
            this.buttonSaveSettings.Click += new System.EventHandler(this.buttonSaveSettings_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.polokusLabel2);
            this.groupBox2.Controls.Add(this.numericUpDownMessageListenerPort);
            this.groupBox2.Controls.Add(this.checkBoxEnableLogs);
            this.groupBox2.Location = new System.Drawing.Point(3, 173);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(953, 161);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Application options";
            // 
            // checkBoxEnableLogs
            // 
            this.checkBoxEnableLogs.AutoSize = true;
            this.checkBoxEnableLogs.Location = new System.Drawing.Point(7, 29);
            this.checkBoxEnableLogs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBoxEnableLogs.Name = "checkBoxEnableLogs";
            this.checkBoxEnableLogs.Size = new System.Drawing.Size(108, 24);
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
            this.groupBox1.Location = new System.Drawing.Point(3, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(953, 161);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bpmn options";
            // 
            // buttonBrowseBpmnPath
            // 
            this.buttonBrowseBpmnPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseBpmnPath.Location = new System.Drawing.Point(861, 21);
            this.buttonBrowseBpmnPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonBrowseBpmnPath.Name = "buttonBrowseBpmnPath";
            this.buttonBrowseBpmnPath.Size = new System.Drawing.Size(86, 31);
            this.buttonBrowseBpmnPath.TabIndex = 2;
            this.buttonBrowseBpmnPath.Text = "Browse";
            this.buttonBrowseBpmnPath.UseVisualStyleBackColor = true;
            this.buttonBrowseBpmnPath.Click += new System.EventHandler(this.buttonBrowseBpmnPath_Click);
            // 
            // textBoxBpmnPath
            // 
            this.textBoxBpmnPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBpmnPath.Location = new System.Drawing.Point(152, 21);
            this.textBoxBpmnPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxBpmnPath.Name = "textBoxBpmnPath";
            this.textBoxBpmnPath.Size = new System.Drawing.Size(701, 27);
            this.textBoxBpmnPath.TabIndex = 1;
            // 
            // polokusLabel1
            // 
            this.polokusLabel1.AutoSize = true;
            this.polokusLabel1.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.polokusLabel1.Location = new System.Drawing.Point(7, 25);
            this.polokusLabel1.Name = "polokusLabel1";
            this.polokusLabel1.Size = new System.Drawing.Size(143, 17);
            this.polokusLabel1.TabIndex = 0;
            this.polokusLabel1.Text = "Path with bpmn files:";
            // 
            // numericUpDownMessageListenerPort
            // 
            this.numericUpDownMessageListenerPort.Location = new System.Drawing.Point(178, 98);
            this.numericUpDownMessageListenerPort.Name = "numericUpDownMessageListenerPort";
            this.numericUpDownMessageListenerPort.Size = new System.Drawing.Size(150, 27);
            this.numericUpDownMessageListenerPort.Minimum = 0;
            this.numericUpDownMessageListenerPort.Maximum = 65535;
            this.numericUpDownMessageListenerPort.TabIndex = 1;
            // 
            // polokusLabel2
            // 
            this.polokusLabel2.AutoSize = true;
            this.polokusLabel2.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.polokusLabel2.Location = new System.Drawing.Point(14, 102);
            this.polokusLabel2.Name = "polokusLabel2";
            this.polokusLabel2.Size = new System.Drawing.Size(158, 17);
            this.polokusLabel2.TabIndex = 2;
            this.polokusLabel2.Text = "Message Listener Port:";
            // 
            // SettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SettingsView";
            this.Size = new System.Drawing.Size(960, 785);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMessageListenerPort)).EndInit();
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
        private Controls.PolokusLabel polokusLabel2;
        private NumericUpDown numericUpDownMessageListenerPort;
    }
}
