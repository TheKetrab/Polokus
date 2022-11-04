
namespace Polokus.App.Forms
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelSideMenu = new System.Windows.Forms.Panel();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.panelProcesses = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonScriptView = new System.Windows.Forms.Button();
            this.buttonXmlView = new System.Windows.Forms.Button();
            this.buttonGraphView = new System.Windows.Forms.Button();
            this.buttonProcesses = new System.Windows.Forms.Button();
            this.buttonEditor = new System.Windows.Forms.Button();
            this.buttonService = new System.Windows.Forms.Button();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelHome = new System.Windows.Forms.Panel();
            this.panelSettings = new System.Windows.Forms.Panel();
            this.panelService = new System.Windows.Forms.Panel();
            this.panelEditor = new System.Windows.Forms.Panel();
            this.panelProcessesCsharp = new System.Windows.Forms.Panel();
            this.panelProcessesXml = new System.Windows.Forms.Panel();
            this.panelProcessesGraph = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelSideMenu.SuspendLayout();
            this.panelProcesses.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 615);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1184, 21);
            this.panel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panelSideMenu);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelHome);
            this.splitContainer1.Panel2.Controls.Add(this.panelSettings);
            this.splitContainer1.Panel2.Controls.Add(this.panelService);
            this.splitContainer1.Panel2.Controls.Add(this.panelEditor);
            this.splitContainer1.Panel2.Controls.Add(this.panelProcessesCsharp);
            this.splitContainer1.Panel2.Controls.Add(this.panelProcessesXml);
            this.splitContainer1.Panel2.Controls.Add(this.panelProcessesGraph);
            this.splitContainer1.Size = new System.Drawing.Size(1184, 615);
            this.splitContainer1.SplitterDistance = 243;
            this.splitContainer1.TabIndex = 1;
            // 
            // panelSideMenu
            // 
            this.panelSideMenu.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panelSideMenu.Controls.Add(this.buttonSettings);
            this.panelSideMenu.Controls.Add(this.panelProcesses);
            this.panelSideMenu.Controls.Add(this.buttonProcesses);
            this.panelSideMenu.Controls.Add(this.buttonEditor);
            this.panelSideMenu.Controls.Add(this.buttonService);
            this.panelSideMenu.Controls.Add(this.panelLogo);
            this.panelSideMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSideMenu.Location = new System.Drawing.Point(0, 0);
            this.panelSideMenu.Name = "panelSideMenu";
            this.panelSideMenu.Size = new System.Drawing.Size(243, 615);
            this.panelSideMenu.TabIndex = 1;
            // 
            // buttonSettings
            // 
            this.buttonSettings.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonSettings.FlatAppearance.BorderSize = 0;
            this.buttonSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSettings.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonSettings.Location = new System.Drawing.Point(0, 570);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.buttonSettings.Size = new System.Drawing.Size(243, 45);
            this.buttonSettings.TabIndex = 4;
            this.buttonSettings.Text = "Settings";
            this.buttonSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // panelProcesses
            // 
            this.panelProcesses.Controls.Add(this.treeView1);
            this.panelProcesses.Controls.Add(this.panel3);
            this.panelProcesses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProcesses.Location = new System.Drawing.Point(0, 235);
            this.panelProcesses.Name = "panelProcesses";
            this.panelProcesses.Size = new System.Drawing.Size(243, 380);
            this.panelProcesses.TabIndex = 3;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 53);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(243, 327);
            this.treeView1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel3.Controls.Add(this.buttonScriptView);
            this.panel3.Controls.Add(this.buttonXmlView);
            this.panel3.Controls.Add(this.buttonGraphView);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(243, 53);
            this.panel3.TabIndex = 0;
            // 
            // buttonScriptView
            // 
            this.buttonScriptView.Location = new System.Drawing.Point(121, 9);
            this.buttonScriptView.Name = "buttonScriptView";
            this.buttonScriptView.Size = new System.Drawing.Size(35, 35);
            this.buttonScriptView.TabIndex = 2;
            this.buttonScriptView.Text = "CS";
            this.buttonScriptView.UseVisualStyleBackColor = true;
            this.buttonScriptView.Click += new System.EventHandler(this.buttonScriptView_Click);
            // 
            // buttonXmlView
            // 
            this.buttonXmlView.Location = new System.Drawing.Point(80, 9);
            this.buttonXmlView.Name = "buttonXmlView";
            this.buttonXmlView.Size = new System.Drawing.Size(35, 35);
            this.buttonXmlView.TabIndex = 1;
            this.buttonXmlView.Text = "XM";
            this.buttonXmlView.UseVisualStyleBackColor = true;
            this.buttonXmlView.Click += new System.EventHandler(this.buttonXmlView_Click);
            // 
            // buttonGraphView
            // 
            this.buttonGraphView.Location = new System.Drawing.Point(39, 9);
            this.buttonGraphView.Name = "buttonGraphView";
            this.buttonGraphView.Size = new System.Drawing.Size(35, 35);
            this.buttonGraphView.TabIndex = 0;
            this.buttonGraphView.Text = "GR";
            this.buttonGraphView.UseVisualStyleBackColor = true;
            this.buttonGraphView.Click += new System.EventHandler(this.buttonGraphView_Click);
            // 
            // buttonProcesses
            // 
            this.buttonProcesses.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonProcesses.FlatAppearance.BorderSize = 0;
            this.buttonProcesses.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonProcesses.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonProcesses.Location = new System.Drawing.Point(0, 190);
            this.buttonProcesses.Name = "buttonProcesses";
            this.buttonProcesses.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.buttonProcesses.Size = new System.Drawing.Size(243, 45);
            this.buttonProcesses.TabIndex = 2;
            this.buttonProcesses.Text = "Processes";
            this.buttonProcesses.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonProcesses.UseVisualStyleBackColor = true;
            this.buttonProcesses.Click += new System.EventHandler(this.buttonProcesses_Click);
            // 
            // buttonEditor
            // 
            this.buttonEditor.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonEditor.FlatAppearance.BorderSize = 0;
            this.buttonEditor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEditor.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonEditor.Location = new System.Drawing.Point(0, 145);
            this.buttonEditor.Name = "buttonEditor";
            this.buttonEditor.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.buttonEditor.Size = new System.Drawing.Size(243, 45);
            this.buttonEditor.TabIndex = 5;
            this.buttonEditor.Text = "Editor";
            this.buttonEditor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEditor.UseVisualStyleBackColor = true;
            this.buttonEditor.Click += new System.EventHandler(this.buttonEditor_Click);
            // 
            // buttonService
            // 
            this.buttonService.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonService.FlatAppearance.BorderSize = 0;
            this.buttonService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonService.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonService.Location = new System.Drawing.Point(0, 100);
            this.buttonService.Name = "buttonService";
            this.buttonService.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.buttonService.Size = new System.Drawing.Size(243, 45);
            this.buttonService.TabIndex = 1;
            this.buttonService.Text = "Service";
            this.buttonService.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonService.UseVisualStyleBackColor = true;
            this.buttonService.Click += new System.EventHandler(this.buttonService_Click);
            // 
            // panelLogo
            // 
            this.panelLogo.Controls.Add(this.label1);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(243, 100);
            this.panelLogo.TabIndex = 0;
            this.panelLogo.Click += new System.EventHandler(this.panelLogo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(63, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "POLOKUS";
            // 
            // panelHome
            // 
            this.panelHome.BackColor = System.Drawing.Color.DarkMagenta;
            this.panelHome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHome.Location = new System.Drawing.Point(0, 0);
            this.panelHome.Name = "panelHome";
            this.panelHome.Size = new System.Drawing.Size(937, 615);
            this.panelHome.TabIndex = 5;
            // 
            // panelSettings
            // 
            this.panelSettings.BackColor = System.Drawing.Color.MediumBlue;
            this.panelSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSettings.Location = new System.Drawing.Point(0, 0);
            this.panelSettings.Name = "panelSettings";
            this.panelSettings.Size = new System.Drawing.Size(937, 615);
            this.panelSettings.TabIndex = 4;
            // 
            // panelService
            // 
            this.panelService.BackColor = System.Drawing.Color.LightCyan;
            this.panelService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelService.Location = new System.Drawing.Point(0, 0);
            this.panelService.Name = "panelService";
            this.panelService.Size = new System.Drawing.Size(937, 615);
            this.panelService.TabIndex = 4;
            // 
            // panelEditor
            // 
            this.panelEditor.BackColor = System.Drawing.Color.SpringGreen;
            this.panelEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEditor.Location = new System.Drawing.Point(0, 0);
            this.panelEditor.Name = "panelEditor";
            this.panelEditor.Size = new System.Drawing.Size(937, 615);
            this.panelEditor.TabIndex = 3;
            // 
            // panelProcessesCsharp
            // 
            this.panelProcessesCsharp.BackColor = System.Drawing.Color.GreenYellow;
            this.panelProcessesCsharp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProcessesCsharp.Location = new System.Drawing.Point(0, 0);
            this.panelProcessesCsharp.Name = "panelProcessesCsharp";
            this.panelProcessesCsharp.Size = new System.Drawing.Size(937, 615);
            this.panelProcessesCsharp.TabIndex = 2;
            // 
            // panelProcessesXml
            // 
            this.panelProcessesXml.BackColor = System.Drawing.Color.AliceBlue;
            this.panelProcessesXml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProcessesXml.Location = new System.Drawing.Point(0, 0);
            this.panelProcessesXml.Name = "panelProcessesXml";
            this.panelProcessesXml.Size = new System.Drawing.Size(937, 615);
            this.panelProcessesXml.TabIndex = 1;
            // 
            // panelProcessesGraph
            // 
            this.panelProcessesGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProcessesGraph.Location = new System.Drawing.Point(0, 0);
            this.panelProcessesGraph.Name = "panelProcessesGraph";
            this.panelProcessesGraph.Size = new System.Drawing.Size(937, 615);
            this.panelProcessesGraph.TabIndex = 0;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 636);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainWindow";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelSideMenu.ResumeLayout(false);
            this.panelProcesses.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panelLogo.PerformLayout();
            this.ResumeLayout(false);

        }


        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel1;
        private SplitContainer splitContainer1;
        private Panel panelSideMenu;
        private Button buttonSettings;
        private Panel panelProcesses;
        private TreeView treeView1;
        private Panel panel3;
        private Button buttonScriptView;
        private Button buttonXmlView;
        private Button buttonGraphView;
        private Button buttonProcesses;
        private Button buttonService;
        private Panel panelLogo;
        private Label label1;
        private Panel panelProcessesCsharp;
        private Panel panelProcessesXml;
        private Panel panelProcessesGraph;
        private Button buttonEditor;
        private Panel panelEditor;
        private Panel panelSettings;
        private Panel panelService;
        private Panel panelHome;
    }
}

