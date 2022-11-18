
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
            this.panelSideMenu = new System.Windows.Forms.Panel();
            this.buttonSettings = new Polokus.App.Controls.PolokusMenuButton();
            this.panelProcesses = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panelProcessesButtons = new System.Windows.Forms.Panel();
            this.buttonScriptView = new Polokus.App.Controls.PolokusIconButton();
            this.buttonXmlView = new Polokus.App.Controls.PolokusIconButton();
            this.buttonGraphView = new Polokus.App.Controls.PolokusIconButton();
            this.buttonProcesses = new Polokus.App.Controls.PolokusMenuButton();
            this.buttonEditor = new Polokus.App.Controls.PolokusMenuButton();
            this.buttonService = new Polokus.App.Controls.PolokusMenuButton();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.panelPolokusHeader = new Polokus.App.Controls.PolokusHeader();
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
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelSideMenu.SuspendLayout();
            this.panelProcesses.SuspendLayout();
            this.panelProcessesButtons.SuspendLayout();
            this.panelLogo.SuspendLayout();
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
            this.panelBottom.Location = new System.Drawing.Point(0, 615);
            this.panelBottom.Margin = new System.Windows.Forms.Padding(2);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1184, 21);
            this.panelBottom.TabIndex = 0;
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelInfo.Location = new System.Drawing.Point(0, 0);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(41, 15);
            this.labelInfo.TabIndex = 0;
            this.labelInfo.Text = "label2";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.splitContainer1.Panel2.Controls.Add(this.panelWorkspace);
            this.splitContainer1.Panel2.Controls.Add(this.panelTaskBar);
            this.splitContainer1.Size = new System.Drawing.Size(1184, 615);
            this.splitContainer1.SplitterDistance = 243;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.TabStop = false;
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
            this.panelSideMenu.Size = new System.Drawing.Size(241, 613);
            this.panelSideMenu.TabIndex = 1;
            // 
            // buttonSettings
            // 
            this.buttonSettings.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonSettings.FlatAppearance.BorderSize = 0;
            this.buttonSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSettings.Font = new System.Drawing.Font("Montserrat", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonSettings.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonSettings.Image = ((System.Drawing.Image)(resources.GetObject("buttonSettings.Image")));
            this.buttonSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSettings.Location = new System.Drawing.Point(0, 568);
            this.buttonSettings.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.buttonSettings.Size = new System.Drawing.Size(241, 45);
            this.buttonSettings.TabIndex = 4;
            this.buttonSettings.Text = "   Settings";
            this.buttonSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // panelProcesses
            // 
            this.panelProcesses.Controls.Add(this.treeView1);
            this.panelProcesses.Controls.Add(this.panelProcessesButtons);
            this.panelProcesses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProcesses.Location = new System.Drawing.Point(0, 235);
            this.panelProcesses.Name = "panelProcesses";
            this.panelProcesses.Size = new System.Drawing.Size(241, 378);
            this.panelProcesses.TabIndex = 3;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 53);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(241, 325);
            this.treeView1.TabIndex = 1;
            // 
            // panelProcessesButtons
            // 
            this.panelProcessesButtons.Controls.Add(this.buttonScriptView);
            this.panelProcessesButtons.Controls.Add(this.buttonXmlView);
            this.panelProcessesButtons.Controls.Add(this.buttonGraphView);
            this.panelProcessesButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelProcessesButtons.Location = new System.Drawing.Point(0, 0);
            this.panelProcessesButtons.Name = "panelProcessesButtons";
            this.panelProcessesButtons.Size = new System.Drawing.Size(241, 53);
            this.panelProcessesButtons.TabIndex = 0;
            // 
            // buttonScriptView
            // 
            this.buttonScriptView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonScriptView.Image = ((System.Drawing.Image)(resources.GetObject("buttonScriptView.Image")));
            this.buttonScriptView.Location = new System.Drawing.Point(121, 9);
            this.buttonScriptView.Name = "buttonScriptView";
            this.buttonScriptView.Size = new System.Drawing.Size(35, 35);
            this.buttonScriptView.TabIndex = 2;
            this.buttonScriptView.UseVisualStyleBackColor = true;
            this.buttonScriptView.Click += new System.EventHandler(this.buttonScriptView_Click);
            // 
            // buttonXmlView
            // 
            this.buttonXmlView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonXmlView.Image = ((System.Drawing.Image)(resources.GetObject("buttonXmlView.Image")));
            this.buttonXmlView.Location = new System.Drawing.Point(80, 9);
            this.buttonXmlView.Name = "buttonXmlView";
            this.buttonXmlView.Size = new System.Drawing.Size(35, 35);
            this.buttonXmlView.TabIndex = 1;
            this.buttonXmlView.UseVisualStyleBackColor = true;
            this.buttonXmlView.Click += new System.EventHandler(this.buttonXmlView_Click);
            // 
            // buttonGraphView
            // 
            this.buttonGraphView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGraphView.Image = ((System.Drawing.Image)(resources.GetObject("buttonGraphView.Image")));
            this.buttonGraphView.Location = new System.Drawing.Point(39, 9);
            this.buttonGraphView.Name = "buttonGraphView";
            this.buttonGraphView.Size = new System.Drawing.Size(35, 35);
            this.buttonGraphView.TabIndex = 0;
            this.buttonGraphView.UseVisualStyleBackColor = true;
            this.buttonGraphView.Click += new System.EventHandler(this.buttonGraphView_Click);
            // 
            // buttonProcesses
            // 
            this.buttonProcesses.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonProcesses.FlatAppearance.BorderSize = 0;
            this.buttonProcesses.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonProcesses.Font = new System.Drawing.Font("Montserrat", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonProcesses.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonProcesses.Image = ((System.Drawing.Image)(resources.GetObject("buttonProcesses.Image")));
            this.buttonProcesses.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonProcesses.Location = new System.Drawing.Point(0, 190);
            this.buttonProcesses.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.buttonProcesses.Name = "buttonProcesses";
            this.buttonProcesses.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.buttonProcesses.Size = new System.Drawing.Size(241, 45);
            this.buttonProcesses.TabIndex = 2;
            this.buttonProcesses.Text = "   Processes";
            this.buttonProcesses.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonProcesses.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonProcesses.UseVisualStyleBackColor = true;
            this.buttonProcesses.Click += new System.EventHandler(this.buttonProcesses_Click);
            // 
            // buttonEditor
            // 
            this.buttonEditor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonEditor.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonEditor.FlatAppearance.BorderSize = 0;
            this.buttonEditor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEditor.Font = new System.Drawing.Font("Montserrat", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonEditor.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonEditor.Image = ((System.Drawing.Image)(resources.GetObject("buttonEditor.Image")));
            this.buttonEditor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEditor.Location = new System.Drawing.Point(0, 145);
            this.buttonEditor.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.buttonEditor.Name = "buttonEditor";
            this.buttonEditor.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.buttonEditor.Size = new System.Drawing.Size(241, 45);
            this.buttonEditor.TabIndex = 5;
            this.buttonEditor.Text = "   Editor";
            this.buttonEditor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEditor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonEditor.UseVisualStyleBackColor = true;
            this.buttonEditor.Click += new System.EventHandler(this.buttonEditor_Click);
            // 
            // buttonService
            // 
            this.buttonService.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonService.FlatAppearance.BorderSize = 0;
            this.buttonService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonService.Font = new System.Drawing.Font("Montserrat", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonService.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonService.Image = ((System.Drawing.Image)(resources.GetObject("buttonService.Image")));
            this.buttonService.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonService.Location = new System.Drawing.Point(0, 100);
            this.buttonService.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.buttonService.Name = "buttonService";
            this.buttonService.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.buttonService.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonService.Size = new System.Drawing.Size(241, 45);
            this.buttonService.TabIndex = 1;
            this.buttonService.Text = "   Service";
            this.buttonService.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonService.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonService.UseVisualStyleBackColor = true;
            this.buttonService.Click += new System.EventHandler(this.buttonService_Click);
            // 
            // panelLogo
            // 
            this.panelLogo.Controls.Add(this.panelPolokusHeader);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Padding = new System.Windows.Forms.Padding(10);
            this.panelLogo.Size = new System.Drawing.Size(241, 100);
            this.panelLogo.TabIndex = 0;
            this.panelLogo.Click += new System.EventHandler(this.panelLogo_Click);
            // 
            // panelPolokusHeader
            // 
            this.panelPolokusHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPolokusHeader.Location = new System.Drawing.Point(10, 10);
            this.panelPolokusHeader.Margin = new System.Windows.Forms.Padding(10);
            this.panelPolokusHeader.Name = "panelPolokusHeader";
            this.panelPolokusHeader.Padding = new System.Windows.Forms.Padding(5);
            this.panelPolokusHeader.Size = new System.Drawing.Size(221, 80);
            this.panelPolokusHeader.TabIndex = 2;
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
            this.panelWorkspace.Location = new System.Drawing.Point(0, 32);
            this.panelWorkspace.Name = "panelWorkspace";
            this.panelWorkspace.Size = new System.Drawing.Size(938, 581);
            this.panelWorkspace.TabIndex = 6;
            // 
            // panelProcessesGraph
            // 
            this.panelProcessesGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProcessesGraph.Location = new System.Drawing.Point(0, 0);
            this.panelProcessesGraph.Name = "panelProcessesGraph";
            this.panelProcessesGraph.Size = new System.Drawing.Size(938, 581);
            this.panelProcessesGraph.TabIndex = 0;
            // 
            // panelProcessesXml
            // 
            this.panelProcessesXml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProcessesXml.Location = new System.Drawing.Point(0, 0);
            this.panelProcessesXml.Name = "panelProcessesXml";
            this.panelProcessesXml.Size = new System.Drawing.Size(938, 581);
            this.panelProcessesXml.TabIndex = 1;
            // 
            // panelProcessesCsharp
            // 
            this.panelProcessesCsharp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProcessesCsharp.Location = new System.Drawing.Point(0, 0);
            this.panelProcessesCsharp.Name = "panelProcessesCsharp";
            this.panelProcessesCsharp.Size = new System.Drawing.Size(938, 581);
            this.panelProcessesCsharp.TabIndex = 2;
            // 
            // panelEditor
            // 
            this.panelEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEditor.Location = new System.Drawing.Point(0, 0);
            this.panelEditor.Name = "panelEditor";
            this.panelEditor.Size = new System.Drawing.Size(938, 581);
            this.panelEditor.TabIndex = 3;
            // 
            // panelService
            // 
            this.panelService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelService.Location = new System.Drawing.Point(0, 0);
            this.panelService.Name = "panelService";
            this.panelService.Size = new System.Drawing.Size(938, 581);
            this.panelService.TabIndex = 4;
            // 
            // panelSettings
            // 
            this.panelSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSettings.Location = new System.Drawing.Point(0, 0);
            this.panelSettings.Name = "panelSettings";
            this.panelSettings.Size = new System.Drawing.Size(938, 581);
            this.panelSettings.TabIndex = 4;
            // 
            // panelHome
            // 
            this.panelHome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHome.Location = new System.Drawing.Point(0, 0);
            this.panelHome.Name = "panelHome";
            this.panelHome.Size = new System.Drawing.Size(938, 581);
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
            this.panelTaskBar.Name = "panelTaskBar";
            this.panelTaskBar.Padding = new System.Windows.Forms.Padding(5);
            this.panelTaskBar.Size = new System.Drawing.Size(938, 32);
            this.panelTaskBar.TabIndex = 2;
            this.panelTaskBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTitleBar_MouseDown);
            // 
            // iconBtnMinimize
            // 
            this.iconBtnMinimize.Dock = System.Windows.Forms.DockStyle.Right;
            this.iconBtnMinimize.FontChar = Polokus.App.Fonts.FontsManager.SegMDL2.Minimize;
            this.iconBtnMinimize.FontCharCustom = 0;
            this.iconBtnMinimize.FontSize = 18.5F;
            this.iconBtnMinimize.FontStyle = System.Drawing.FontStyle.Bold;
            this.iconBtnMinimize.Location = new System.Drawing.Point(828, 5);
            this.iconBtnMinimize.Margin = new System.Windows.Forms.Padding(0);
            this.iconBtnMinimize.Name = "iconBtnMinimize";
            this.iconBtnMinimize.Padding = new System.Windows.Forms.Padding(5);
            this.iconBtnMinimize.Size = new System.Drawing.Size(35, 22);
            this.iconBtnMinimize.TabIndex = 5;
            this.iconBtnMinimize.UseVisualStyleBackColor = true;
            this.iconBtnMinimize.Click += new System.EventHandler(this.iconBtn3_Click);
            // 
            // iconBtnSize
            // 
            this.iconBtnSize.Dock = System.Windows.Forms.DockStyle.Right;
            this.iconBtnSize.FontChar = Polokus.App.Fonts.FontsManager.SegMDL2.Restore;
            this.iconBtnSize.FontCharCustom = 0;
            this.iconBtnSize.FontSize = 18.5F;
            this.iconBtnSize.FontStyle = System.Drawing.FontStyle.Bold;
            this.iconBtnSize.Location = new System.Drawing.Point(863, 5);
            this.iconBtnSize.Margin = new System.Windows.Forms.Padding(10);
            this.iconBtnSize.Name = "iconBtnSize";
            this.iconBtnSize.Padding = new System.Windows.Forms.Padding(5);
            this.iconBtnSize.Size = new System.Drawing.Size(35, 22);
            this.iconBtnSize.TabIndex = 4;
            this.iconBtnSize.UseVisualStyleBackColor = true;
            this.iconBtnSize.Click += new System.EventHandler(this.iconBtn2_Click);
            // 
            // iconBtnExit
            // 
            this.iconBtnExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.iconBtnExit.FontChar = Polokus.App.Fonts.FontsManager.SegMDL2.Close;
            this.iconBtnExit.FontCharCustom = 0;
            this.iconBtnExit.FontSize = 18.5F;
            this.iconBtnExit.FontStyle = System.Drawing.FontStyle.Bold;
            this.iconBtnExit.Location = new System.Drawing.Point(898, 5);
            this.iconBtnExit.Margin = new System.Windows.Forms.Padding(10);
            this.iconBtnExit.Name = "iconBtnExit";
            this.iconBtnExit.Padding = new System.Windows.Forms.Padding(5);
            this.iconBtnExit.Size = new System.Drawing.Size(35, 22);
            this.iconBtnExit.TabIndex = 3;
            this.iconBtnExit.UseVisualStyleBackColor = true;
            this.iconBtnExit.Click += new System.EventHandler(this.iconBtn1_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(92)))));
            this.ClientSize = new System.Drawing.Size(1184, 636);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panelBottom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainWindow";
            this.Text = "Polokus BPMN Engine";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelSideMenu.ResumeLayout(false);
            this.panelProcesses.ResumeLayout(false);
            this.panelProcessesButtons.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panelWorkspace.ResumeLayout(false);
            this.panelTaskBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private PolokusGradientPanel panelBottom;
        private PolokusSplitContainer splitContainer1;
        private Panel panelSideMenu;
        private PolokusMenuButton buttonSettings;
        private Panel panelProcesses;
        private TreeView treeView1;
        private Panel panelProcessesButtons;
        private PolokusIconButton buttonScriptView;
        private PolokusIconButton buttonXmlView;
        private PolokusIconButton buttonGraphView;
        private PolokusMenuButton buttonProcesses;
        private PolokusMenuButton buttonService;
        private Panel panelLogo;
        private Panel panelProcessesCsharp;
        private Panel panelProcessesXml;
        private Panel panelProcessesGraph;
        private PolokusMenuButton buttonEditor;
        private Panel panelEditor;
        private Panel panelSettings;
        private Panel panelService;
        private Panel panelHome;
        private Views.XmlView xmlView1;
        private Views.GraphView graphView1;
        private PolokusLabel labelInfo;
        private PolokusHeader panelPolokusHeader;
        private PolokusGradientPanel panelTaskBar;
        private Panel panelWorkspace;
        private IconBtn iconBtnExit;
        private IconBtn iconBtnMinimize;
        private IconBtn iconBtnSize;
    }
}

