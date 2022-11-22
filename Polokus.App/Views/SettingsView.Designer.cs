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
            this.polokusLabel2 = new Polokus.App.Controls.PolokusLabel();
            this.numericUpDownMessageListenerPort = new System.Windows.Forms.NumericUpDown();
            this.checkBoxEnableLogs = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonBrowseBpmnPath = new System.Windows.Forms.Button();
            this.textBoxBpmnPath = new System.Windows.Forms.TextBox();
            this.polokusLabel1 = new Polokus.App.Controls.PolokusLabel();
            this.polokusLabel3 = new Polokus.App.Controls.PolokusLabel();
            this.textBoxBpmnServiceNodeHandlers = new System.Windows.Forms.TextBox();
            this.buttonServiceNodeHandlers = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMessageListenerPort)).BeginInit();
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
            // numericUpDownMessageListenerPort
            // 
            this.numericUpDownMessageListenerPort.Location = new System.Drawing.Point(178, 98);
            this.numericUpDownMessageListenerPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownMessageListenerPort.Name = "numericUpDownMessageListenerPort";
            this.numericUpDownMessageListenerPort.Size = new System.Drawing.Size(150, 27);
            this.numericUpDownMessageListenerPort.TabIndex = 1;
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
            this.groupBox1.Controls.Add(this.buttonServiceNodeHandlers);
            this.groupBox1.Controls.Add(this.textBoxBpmnServiceNodeHandlers);
            this.groupBox1.Controls.Add(this.polokusLabel3);
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
            // polokusLabel3
            // 
            this.polokusLabel3.AutoSize = true;
            this.polokusLabel3.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.polokusLabel3.Location = new System.Drawing.Point(7, 66);
            this.polokusLabel3.Name = "polokusLabel3";
            this.polokusLabel3.Size = new System.Drawing.Size(308, 17);
            this.polokusLabel3.TabIndex = 3;
            this.polokusLabel3.Text = "Path with custom Service Task NodeHandlers:";
            // 
            // textBoxBpmnServiceNodeHandlers
            // 
            this.textBoxBpmnServiceNodeHandlers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBpmnServiceNodeHandlers.Location = new System.Drawing.Point(321, 61);
            this.textBoxBpmnServiceNodeHandlers.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxBpmnServiceNodeHandlers.Name = "textBoxBpmnServiceNodeHandlers";
            this.textBoxBpmnServiceNodeHandlers.Size = new System.Drawing.Size(532, 27);
            this.textBoxBpmnServiceNodeHandlers.TabIndex = 4;
            // 
            // buttonServiceNodeHandlers
            // 
            this.buttonServiceNodeHandlers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonServiceNodeHandlers.Location = new System.Drawing.Point(859, 61);
            this.buttonServiceNodeHandlers.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonServiceNodeHandlers.Name = "buttonServiceNodeHandlers";
            this.buttonServiceNodeHandlers.Size = new System.Drawing.Size(86, 31);
            this.buttonServiceNodeHandlers.TabIndex = 5;
            this.buttonServiceNodeHandlers.Text = "Browse";
            this.buttonServiceNodeHandlers.UseVisualStyleBackColor = true;
            this.buttonServiceNodeHandlers.Click += new System.EventHandler(this.buttonServiceNodeHandlers_Click);
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMessageListenerPort)).EndInit();
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
        private Controls.PolokusLabel polokusLabel2;
        private NumericUpDown numericUpDownMessageListenerPort;
        private Button buttonServiceNodeHandlers;
        private TextBox textBoxBpmnServiceNodeHandlers;
        private Controls.PolokusLabel polokusLabel3;
    }
}
