
using Polokus.App.Controls;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panelBottom = new Polokus.App.Controls.PolokusGradientPanel();
            this.labelInfo = new Polokus.App.Controls.PolokusLabel();
            this.splitContainer1 = new Polokus.App.Controls.PolokusSplitContainer();
            this.panelWorkspace = new System.Windows.Forms.Panel();
            this.panelProcessesGraph = new System.Windows.Forms.Panel();
            this.panelProcessesXml = new System.Windows.Forms.Panel();
            this.panelProcessesCsharp = new System.Windows.Forms.Panel();
            this.panelEditor = new System.Windows.Forms.Panel();
            this.panelService = new System.Windows.Forms.Panel();
            this.panelSettings = new System.Windows.Forms.Panel();
            this.panelHome = new System.Windows.Forms.Panel();
            this.panelTaskBar = new Polokus.App.Controls.PolokusGradientPanel();
            this.iconBtnMinimize = new Polokus.App.Controls.IconBtn();
            this.iconBtnSize = new Polokus.App.Controls.IconBtn();
            this.iconBtnExit = new Polokus.App.Controls.IconBtn();
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelWorkspace.SuspendLayout();
            this.panelTaskBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.labelInfo);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.GradientAngle = 0F;
            this.panelBottom.GradientBeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(18)))), ((int)(((byte)(51)))));
            this.panelBottom.GradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(92)))));
            this.panelBottom.Location = new System.Drawing.Point(0, 820);
            this.panelBottom.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1353, 28);
            this.panelBottom.TabIndex = 0;
            this.panelBottom.Visible = false;
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelInfo.Location = new System.Drawing.Point(0, 0);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(55, 21);
            this.labelInfo.TabIndex = 0;
            this.labelInfo.Text = "label2";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelWorkspace);
            this.splitContainer1.Panel2.Controls.Add(this.panelTaskBar);
            this.splitContainer1.Size = new System.Drawing.Size(1353, 820);
            this.splitContainer1.SplitterDistance = 277;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.TabStop = false;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // panelWorkspace
            // 
            this.panelWorkspace.Controls.Add(this.panelProcessesGraph);
            this.panelWorkspace.Controls.Add(this.panelProcessesXml);
            this.panelWorkspace.Controls.Add(this.panelProcessesCsharp);
            this.panelWorkspace.Controls.Add(this.panelEditor);
            this.panelWorkspace.Controls.Add(this.panelService);
            this.panelWorkspace.Controls.Add(this.panelSettings);
            this.panelWorkspace.Controls.Add(this.panelHome);
            this.panelWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWorkspace.Location = new System.Drawing.Point(0, 43);
            this.panelWorkspace.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelWorkspace.Name = "panelWorkspace";
            this.panelWorkspace.Size = new System.Drawing.Size(1073, 775);
            this.panelWorkspace.TabIndex = 6;
            // 
            // panelProcessesGraph
            // 
            this.panelProcessesGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProcessesGraph.Location = new System.Drawing.Point(0, 0);
            this.panelProcessesGraph.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelProcessesGraph.Name = "panelProcessesGraph";
            this.panelProcessesGraph.Size = new System.Drawing.Size(1073, 775);
            this.panelProcessesGraph.TabIndex = 0;
            // 
            // panelProcessesXml
            // 
            this.panelProcessesXml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProcessesXml.Location = new System.Drawing.Point(0, 0);
            this.panelProcessesXml.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelProcessesXml.Name = "panelProcessesXml";
            this.panelProcessesXml.Size = new System.Drawing.Size(1073, 775);
            this.panelProcessesXml.TabIndex = 1;
            // 
            // panelProcessesCsharp
            // 
            this.panelProcessesCsharp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProcessesCsharp.Location = new System.Drawing.Point(0, 0);
            this.panelProcessesCsharp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelProcessesCsharp.Name = "panelProcessesCsharp";
            this.panelProcessesCsharp.Size = new System.Drawing.Size(1073, 775);
            this.panelProcessesCsharp.TabIndex = 2;
            // 
            // panelEditor
            // 
            this.panelEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEditor.Location = new System.Drawing.Point(0, 0);
            this.panelEditor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelEditor.Name = "panelEditor";
            this.panelEditor.Size = new System.Drawing.Size(1073, 775);
            this.panelEditor.TabIndex = 3;
            // 
            // panelService
            // 
            this.panelService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelService.Location = new System.Drawing.Point(0, 0);
            this.panelService.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelService.Name = "panelService";
            this.panelService.Size = new System.Drawing.Size(1073, 775);
            this.panelService.TabIndex = 4;
            // 
            // panelSettings
            // 
            this.panelSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSettings.Location = new System.Drawing.Point(0, 0);
            this.panelSettings.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelSettings.Name = "panelSettings";
            this.panelSettings.Size = new System.Drawing.Size(1073, 775);
            this.panelSettings.TabIndex = 4;
            // 
            // panelHome
            // 
            this.panelHome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHome.Location = new System.Drawing.Point(0, 0);
            this.panelHome.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelHome.Name = "panelHome";
            this.panelHome.Size = new System.Drawing.Size(1073, 775);
            this.panelHome.TabIndex = 5;
            // 
            // panelTaskBar
            // 
            this.panelTaskBar.Controls.Add(this.iconBtnMinimize);
            this.panelTaskBar.Controls.Add(this.iconBtnSize);
            this.panelTaskBar.Controls.Add(this.iconBtnExit);
            this.panelTaskBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTaskBar.GradientAngle = 0F;
            this.panelTaskBar.GradientBeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(18)))), ((int)(((byte)(51)))));
            this.panelTaskBar.GradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(92)))));
            this.panelTaskBar.Location = new System.Drawing.Point(0, 0);
            this.panelTaskBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelTaskBar.Name = "panelTaskBar";
            this.panelTaskBar.Size = new System.Drawing.Size(1073, 43);
            this.panelTaskBar.TabIndex = 2;
            this.panelTaskBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTitleBar_MouseDown);
            // 
            // iconBtnMinimize
            // 
            this.iconBtnMinimize.BackColor = System.Drawing.Color.Transparent;
            this.iconBtnMinimize.Dock = System.Windows.Forms.DockStyle.Right;
            this.iconBtnMinimize.FlatAppearance.BorderSize = 0;
            this.iconBtnMinimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.iconBtnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBtnMinimize.FontChar = Polokus.App.Fonts.FontsManager.SegMDL2.Minimize;
            this.iconBtnMinimize.FontCharCustom = 0;
            this.iconBtnMinimize.FontSize = 18.5F;
            this.iconBtnMinimize.FontStyle = System.Drawing.FontStyle.Regular;
            this.iconBtnMinimize.ForeColor = System.Drawing.Color.White;
            this.iconBtnMinimize.Location = new System.Drawing.Point(953, 0);
            this.iconBtnMinimize.Margin = new System.Windows.Forms.Padding(0);
            this.iconBtnMinimize.Name = "iconBtnMinimize";
            this.iconBtnMinimize.Padding = new System.Windows.Forms.Padding(11, 13, 11, 13);
            this.iconBtnMinimize.Size = new System.Drawing.Size(40, 43);
            this.iconBtnMinimize.TabIndex = 5;
            this.iconBtnMinimize.TabStop = false;
            this.iconBtnMinimize.UseVisualStyleBackColor = false;
            this.iconBtnMinimize.Click += new System.EventHandler(this.iconBtnMinimize_Click);
            // 
            // iconBtnSize
            // 
            this.iconBtnSize.BackColor = System.Drawing.Color.Transparent;
            this.iconBtnSize.Dock = System.Windows.Forms.DockStyle.Right;
            this.iconBtnSize.FlatAppearance.BorderSize = 0;
            this.iconBtnSize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.iconBtnSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBtnSize.FontChar = Polokus.App.Fonts.FontsManager.SegMDL2.Restore;
            this.iconBtnSize.FontCharCustom = 0;
            this.iconBtnSize.FontSize = 18.5F;
            this.iconBtnSize.FontStyle = System.Drawing.FontStyle.Regular;
            this.iconBtnSize.ForeColor = System.Drawing.Color.White;
            this.iconBtnSize.Location = new System.Drawing.Point(993, 0);
            this.iconBtnSize.Margin = new System.Windows.Forms.Padding(11, 13, 11, 13);
            this.iconBtnSize.Name = "iconBtnSize";
            this.iconBtnSize.Padding = new System.Windows.Forms.Padding(11, 13, 11, 13);
            this.iconBtnSize.Size = new System.Drawing.Size(40, 43);
            this.iconBtnSize.TabIndex = 4;
            this.iconBtnSize.TabStop = false;
            this.iconBtnSize.UseVisualStyleBackColor = false;
            this.iconBtnSize.Click += new System.EventHandler(this.iconBtnSize_Click);
            // 
            // iconBtnExit
            // 
            this.iconBtnExit.BackColor = System.Drawing.Color.Transparent;
            this.iconBtnExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.iconBtnExit.FlatAppearance.BorderSize = 0;
            this.iconBtnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.iconBtnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBtnExit.FontChar = Polokus.App.Fonts.FontsManager.SegMDL2.Close;
            this.iconBtnExit.FontCharCustom = 0;
            this.iconBtnExit.FontSize = 18.5F;
            this.iconBtnExit.FontStyle = System.Drawing.FontStyle.Regular;
            this.iconBtnExit.ForeColor = System.Drawing.Color.White;
            this.iconBtnExit.Location = new System.Drawing.Point(1033, 0);
            this.iconBtnExit.Margin = new System.Windows.Forms.Padding(11, 13, 11, 13);
            this.iconBtnExit.Name = "iconBtnExit";
            this.iconBtnExit.Padding = new System.Windows.Forms.Padding(11, 13, 11, 13);
            this.iconBtnExit.Size = new System.Drawing.Size(40, 43);
            this.iconBtnExit.TabIndex = 3;
            this.iconBtnExit.TabStop = false;
            this.iconBtnExit.UseVisualStyleBackColor = false;
            this.iconBtnExit.Click += new System.EventHandler(this.iconBtnExit_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(92)))));
            this.ClientSize = new System.Drawing.Size(1353, 848);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panelBottom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "MainWindow";
            this.Text = "Polokus BPMN Engine";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelWorkspace.ResumeLayout(false);
            this.panelTaskBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private PolokusGradientPanel panelBottom;
        private PolokusSplitContainer splitContainer1;
        private Panel panelProcessesCsharp;
        private Panel panelProcessesXml;
        private Panel panelProcessesGraph;
        private Panel panelEditor;
        private Panel panelSettings;
        private Panel panelService;
        private Panel panelHome;
        private Views.XmlView xmlView1;
        private Views.GraphView graphView1;
        private PolokusLabel labelInfo;
        private PolokusGradientPanel panelTaskBar;
        private Panel panelWorkspace;
        private IconBtn iconBtnExit;
        private IconBtn iconBtnMinimize;
        private IconBtn iconBtnSize;
    }
}

