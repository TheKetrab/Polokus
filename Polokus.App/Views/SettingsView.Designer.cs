using Polokus.App.Controls;

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
            this.buttonResetSettings = new Polokus.App.Controls.PolokusButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.polokusLabel2 = new Polokus.App.Controls.PolokusLabel();
            this.numericUpDownMessageListenerPort = new System.Windows.Forms.NumericUpDown();
            this.buttonExternalsPath = new Polokus.App.Controls.PolokusButton();
            this.textBoxExternalsPath = new System.Windows.Forms.TextBox();
            this.polokusLabel3 = new Polokus.App.Controls.PolokusLabel();
            this.polokusLabel7 = new Polokus.App.Controls.PolokusLabel();
            this.numericUpDownTimeoutForManualProcesses = new System.Windows.Forms.NumericUpDown();
            this.trackBarTimeOutForManualProcesses = new Polokus.App.Controls.PolokusTrackBar();
            this.polokusLabel5 = new Polokus.App.Controls.PolokusLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxRemotePolokusUri = new System.Windows.Forms.TextBox();
            this.labelPolokusUri = new Polokus.App.Controls.PolokusLabel();
            this.checkBoxUseRemotePolokus = new System.Windows.Forms.CheckBox();
            this.polokusLabel6 = new Polokus.App.Controls.PolokusLabel();
            this.numericUpDownDelayPerNodeHandler = new System.Windows.Forms.NumericUpDown();
            this.polokusLabel4 = new Polokus.App.Controls.PolokusLabel();
            this.trackBarDelayPerNodeHandler = new Polokus.App.Controls.PolokusTrackBar();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonBrowseBpmnPath = new Polokus.App.Controls.PolokusButton();
            this.textBoxBpmnPath = new System.Windows.Forms.TextBox();
            this.checkBoxEnableLogs = new System.Windows.Forms.CheckBox();
            this.polokusLabel1 = new Polokus.App.Controls.PolokusLabel();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMessageListenerPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeoutForManualProcesses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTimeOutForManualProcesses)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelayPerNodeHandler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDelayPerNodeHandler)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.buttonResetSettings);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(1200, 825);
            this.panel1.TabIndex = 0;
            // 
            // buttonResetSettings
            // 
            this.buttonResetSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonResetSettings.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonResetSettings.Location = new System.Drawing.Point(1079, 779);
            this.buttonResetSettings.Margin = new System.Windows.Forms.Padding(4);
            this.buttonResetSettings.Name = "buttonResetSettings";
            this.buttonResetSettings.Size = new System.Drawing.Size(107, 32);
            this.buttonResetSettings.TabIndex = 2;
            this.buttonResetSettings.Text = "Reset Settings";
            this.buttonResetSettings.UseVisualStyleBackColor = false;
            this.buttonResetSettings.Click += new System.EventHandler(this.buttonResetSettings_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.polokusLabel2);
            this.groupBox3.Controls.Add(this.numericUpDownMessageListenerPort);
            this.groupBox3.Controls.Add(this.buttonExternalsPath);
            this.groupBox3.Controls.Add(this.textBoxExternalsPath);
            this.groupBox3.Controls.Add(this.polokusLabel3);
            this.groupBox3.Controls.Add(this.polokusLabel7);
            this.groupBox3.Controls.Add(this.numericUpDownTimeoutForManualProcesses);
            this.groupBox3.Controls.Add(this.trackBarTimeOutForManualProcesses);
            this.groupBox3.Controls.Add(this.polokusLabel5);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(10, 340);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(10, 20, 10, 20);
            this.groupBox3.Size = new System.Drawing.Size(1180, 233);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Service options";
            // 
            // polokusLabel2
            // 
            this.polokusLabel2.AutoSize = true;
            this.polokusLabel2.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.polokusLabel2.Location = new System.Drawing.Point(15, 116);
            this.polokusLabel2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.polokusLabel2.Name = "polokusLabel2";
            this.polokusLabel2.Size = new System.Drawing.Size(143, 16);
            this.polokusLabel2.TabIndex = 11;
            this.polokusLabel2.Text = "Message Listener Port:";
            // 
            // numericUpDownMessageListenerPort
            // 
            this.numericUpDownMessageListenerPort.Location = new System.Drawing.Point(312, 110);
            this.numericUpDownMessageListenerPort.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.numericUpDownMessageListenerPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownMessageListenerPort.Name = "numericUpDownMessageListenerPort";
            this.numericUpDownMessageListenerPort.Size = new System.Drawing.Size(98, 25);
            this.numericUpDownMessageListenerPort.TabIndex = 10;
            // 
            // buttonExternalsPath
            // 
            this.buttonExternalsPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExternalsPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExternalsPath.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonExternalsPath.Location = new System.Drawing.Point(1058, 31);
            this.buttonExternalsPath.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.buttonExternalsPath.Name = "buttonExternalsPath";
            this.buttonExternalsPath.Size = new System.Drawing.Size(107, 32);
            this.buttonExternalsPath.TabIndex = 9;
            this.buttonExternalsPath.Text = "Browse";
            this.buttonExternalsPath.UseVisualStyleBackColor = false;
            // 
            // textBoxExternalsPath
            // 
            this.textBoxExternalsPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxExternalsPath.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxExternalsPath.Location = new System.Drawing.Point(312, 34);
            this.textBoxExternalsPath.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.textBoxExternalsPath.Name = "textBoxExternalsPath";
            this.textBoxExternalsPath.Size = new System.Drawing.Size(736, 22);
            this.textBoxExternalsPath.TabIndex = 8;
            // 
            // polokusLabel3
            // 
            this.polokusLabel3.AutoSize = true;
            this.polokusLabel3.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.polokusLabel3.Location = new System.Drawing.Point(15, 38);
            this.polokusLabel3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.polokusLabel3.Name = "polokusLabel3";
            this.polokusLabel3.Size = new System.Drawing.Size(126, 16);
            this.polokusLabel3.TabIndex = 7;
            this.polokusLabel3.Text = "Path with externals:";
            // 
            // polokusLabel7
            // 
            this.polokusLabel7.AutoSize = true;
            this.polokusLabel7.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.polokusLabel7.Location = new System.Drawing.Point(552, 73);
            this.polokusLabel7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.polokusLabel7.Name = "polokusLabel7";
            this.polokusLabel7.Size = new System.Drawing.Size(13, 16);
            this.polokusLabel7.TabIndex = 6;
            this.polokusLabel7.Text = "s";
            // 
            // numericUpDownTimeoutForManualProcesses
            // 
            this.numericUpDownTimeoutForManualProcesses.Location = new System.Drawing.Point(471, 70);
            this.numericUpDownTimeoutForManualProcesses.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.numericUpDownTimeoutForManualProcesses.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownTimeoutForManualProcesses.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDownTimeoutForManualProcesses.Name = "numericUpDownTimeoutForManualProcesses";
            this.numericUpDownTimeoutForManualProcesses.Size = new System.Drawing.Size(71, 25);
            this.numericUpDownTimeoutForManualProcesses.TabIndex = 5;
            // 
            // trackBarTimeOutForManualProcesses
            // 
            this.trackBarTimeOutForManualProcesses.Location = new System.Drawing.Point(312, 72);
            this.trackBarTimeOutForManualProcesses.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.trackBarTimeOutForManualProcesses.Maximum = 300;
            this.trackBarTimeOutForManualProcesses.Minimum = -1;
            this.trackBarTimeOutForManualProcesses.Name = "trackBarTimeOutForManualProcesses";
            this.trackBarTimeOutForManualProcesses.Size = new System.Drawing.Size(149, 45);
            this.trackBarTimeOutForManualProcesses.TabIndex = 3;
            this.trackBarTimeOutForManualProcesses.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarTimeOutForManualProcesses.TrackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(18)))), ((int)(((byte)(51)))));
            // 
            // polokusLabel5
            // 
            this.polokusLabel5.AutoSize = true;
            this.polokusLabel5.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.polokusLabel5.Location = new System.Drawing.Point(15, 73);
            this.polokusLabel5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.polokusLabel5.Name = "polokusLabel5";
            this.polokusLabel5.Size = new System.Drawing.Size(180, 16);
            this.polokusLabel5.TabIndex = 2;
            this.polokusLabel5.Text = "Timeout for manual process:";
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(10, 320);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1180, 20);
            this.panel2.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxRemotePolokusUri);
            this.groupBox2.Controls.Add(this.labelPolokusUri);
            this.groupBox2.Controls.Add(this.checkBoxUseRemotePolokus);
            this.groupBox2.Controls.Add(this.polokusLabel6);
            this.groupBox2.Controls.Add(this.numericUpDownDelayPerNodeHandler);
            this.groupBox2.Controls.Add(this.polokusLabel4);
            this.groupBox2.Controls.Add(this.trackBarDelayPerNodeHandler);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(10, 139);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(10, 20, 10, 20);
            this.groupBox2.Size = new System.Drawing.Size(1180, 181);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Application options";
            // 
            // textBoxRemotePolokusUri
            // 
            this.textBoxRemotePolokusUri.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxRemotePolokusUri.Location = new System.Drawing.Point(312, 112);
            this.textBoxRemotePolokusUri.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.textBoxRemotePolokusUri.Name = "textBoxRemotePolokusUri";
            this.textBoxRemotePolokusUri.Size = new System.Drawing.Size(266, 22);
            this.textBoxRemotePolokusUri.TabIndex = 11;
            // 
            // labelPolokusUri
            // 
            this.labelPolokusUri.AutoSize = true;
            this.labelPolokusUri.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelPolokusUri.Location = new System.Drawing.Point(15, 116);
            this.labelPolokusUri.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelPolokusUri.Name = "labelPolokusUri";
            this.labelPolokusUri.Size = new System.Drawing.Size(136, 16);
            this.labelPolokusUri.TabIndex = 10;
            this.labelPolokusUri.Text = "Remote Polokus URI:";
            // 
            // checkBoxUseRemotePolokus
            // 
            this.checkBoxUseRemotePolokus.AutoSize = true;
            this.checkBoxUseRemotePolokus.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBoxUseRemotePolokus.Location = new System.Drawing.Point(15, 73);
            this.checkBoxUseRemotePolokus.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.checkBoxUseRemotePolokus.Name = "checkBoxUseRemotePolokus";
            this.checkBoxUseRemotePolokus.Size = new System.Drawing.Size(148, 20);
            this.checkBoxUseRemotePolokus.TabIndex = 9;
            this.checkBoxUseRemotePolokus.Text = "Use remote Polokus";
            this.checkBoxUseRemotePolokus.UseVisualStyleBackColor = true;
            // 
            // polokusLabel6
            // 
            this.polokusLabel6.AutoSize = true;
            this.polokusLabel6.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.polokusLabel6.Location = new System.Drawing.Point(552, 38);
            this.polokusLabel6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.polokusLabel6.Name = "polokusLabel6";
            this.polokusLabel6.Size = new System.Drawing.Size(26, 16);
            this.polokusLabel6.TabIndex = 8;
            this.polokusLabel6.Text = "ms";
            // 
            // numericUpDownDelayPerNodeHandler
            // 
            this.numericUpDownDelayPerNodeHandler.Location = new System.Drawing.Point(471, 35);
            this.numericUpDownDelayPerNodeHandler.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.numericUpDownDelayPerNodeHandler.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownDelayPerNodeHandler.Name = "numericUpDownDelayPerNodeHandler";
            this.numericUpDownDelayPerNodeHandler.Size = new System.Drawing.Size(71, 25);
            this.numericUpDownDelayPerNodeHandler.TabIndex = 7;
            // 
            // polokusLabel4
            // 
            this.polokusLabel4.AutoSize = true;
            this.polokusLabel4.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.polokusLabel4.Location = new System.Drawing.Point(15, 38);
            this.polokusLabel4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.polokusLabel4.Name = "polokusLabel4";
            this.polokusLabel4.Size = new System.Drawing.Size(151, 16);
            this.polokusLabel4.TabIndex = 6;
            this.polokusLabel4.Text = "Delay per NodeHandler:";
            // 
            // trackBarDelayPerNodeHandler
            // 
            this.trackBarDelayPerNodeHandler.Location = new System.Drawing.Point(312, 37);
            this.trackBarDelayPerNodeHandler.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.trackBarDelayPerNodeHandler.Maximum = 10000;
            this.trackBarDelayPerNodeHandler.Name = "trackBarDelayPerNodeHandler";
            this.trackBarDelayPerNodeHandler.Size = new System.Drawing.Size(149, 45);
            this.trackBarDelayPerNodeHandler.TabIndex = 5;
            this.trackBarDelayPerNodeHandler.TickFrequency = 10;
            this.trackBarDelayPerNodeHandler.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarDelayPerNodeHandler.TrackColor = System.Drawing.Color.Empty;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(10, 119);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1180, 20);
            this.panel3.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonBrowseBpmnPath);
            this.groupBox1.Controls.Add(this.textBoxBpmnPath);
            this.groupBox1.Controls.Add(this.checkBoxEnableLogs);
            this.groupBox1.Controls.Add(this.polokusLabel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(10, 20, 10, 20);
            this.groupBox1.Size = new System.Drawing.Size(1180, 109);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Main options";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // buttonBrowseBpmnPath
            // 
            this.buttonBrowseBpmnPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseBpmnPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBrowseBpmnPath.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonBrowseBpmnPath.Location = new System.Drawing.Point(1058, 31);
            this.buttonBrowseBpmnPath.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.buttonBrowseBpmnPath.Name = "buttonBrowseBpmnPath";
            this.buttonBrowseBpmnPath.Size = new System.Drawing.Size(107, 32);
            this.buttonBrowseBpmnPath.TabIndex = 2;
            this.buttonBrowseBpmnPath.Text = "Browse";
            this.buttonBrowseBpmnPath.UseVisualStyleBackColor = false;
            this.buttonBrowseBpmnPath.Click += new System.EventHandler(this.buttonBrowseBpmnPath_Click);
            // 
            // textBoxBpmnPath
            // 
            this.textBoxBpmnPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBpmnPath.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxBpmnPath.Location = new System.Drawing.Point(312, 34);
            this.textBoxBpmnPath.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.textBoxBpmnPath.Name = "textBoxBpmnPath";
            this.textBoxBpmnPath.Size = new System.Drawing.Size(736, 22);
            this.textBoxBpmnPath.TabIndex = 1;
            // 
            // checkBoxEnableLogs
            // 
            this.checkBoxEnableLogs.AutoSize = true;
            this.checkBoxEnableLogs.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBoxEnableLogs.Location = new System.Drawing.Point(15, 69);
            this.checkBoxEnableLogs.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.checkBoxEnableLogs.Name = "checkBoxEnableLogs";
            this.checkBoxEnableLogs.Size = new System.Drawing.Size(95, 20);
            this.checkBoxEnableLogs.TabIndex = 0;
            this.checkBoxEnableLogs.Text = "Enable logs";
            this.checkBoxEnableLogs.UseVisualStyleBackColor = true;
            // 
            // polokusLabel1
            // 
            this.polokusLabel1.AutoSize = true;
            this.polokusLabel1.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.polokusLabel1.Location = new System.Drawing.Point(15, 38);
            this.polokusLabel1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.polokusLabel1.Name = "polokusLabel1";
            this.polokusLabel1.Size = new System.Drawing.Size(135, 16);
            this.polokusLabel1.TabIndex = 0;
            this.polokusLabel1.Text = "Path with bpmn files:";
            // 
            // SettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Montserrat", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SettingsView";
            this.Size = new System.Drawing.Size(1200, 825);
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMessageListenerPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeoutForManualProcesses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTimeOutForManualProcesses)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelayPerNodeHandler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDelayPerNodeHandler)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private GroupBox groupBox2;
        private CheckBox checkBoxEnableLogs;
        private GroupBox groupBox1;
        private PolokusButton buttonBrowseBpmnPath;
        private TextBox textBoxBpmnPath;
        private Controls.PolokusLabel polokusLabel1;
        private PolokusButton buttonResetSettings;
        private GroupBox groupBox3;
        private Controls.PolokusLabel polokusLabel5;
        private Controls.PolokusLabel polokusLabel7;
        private NumericUpDown numericUpDownTimeoutForManualProcesses;
        private PolokusTrackBar trackBarTimeOutForManualProcesses;
        private Panel panel2;
        private Panel panel3;
        private PolokusLabel polokusLabel2;
        private NumericUpDown numericUpDownMessageListenerPort;
        private PolokusButton buttonExternalsPath;
        private TextBox textBoxExternalsPath;
        private PolokusLabel polokusLabel3;
        private TextBox textBoxRemotePolokusUri;
        private PolokusLabel labelPolokusUri;
        private CheckBox checkBoxUseRemotePolokus;
        private PolokusLabel polokusLabel6;
        private NumericUpDown numericUpDownDelayPerNodeHandler;
        private PolokusLabel polokusLabel4;
        private PolokusTrackBar trackBarDelayPerNodeHandler;
    }
}
