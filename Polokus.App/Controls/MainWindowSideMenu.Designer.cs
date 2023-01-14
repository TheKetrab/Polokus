namespace Polokus.App.Controls
{
    partial class MainWindowSideMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindowSideMenu));
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
            this.panelSideMenu.SuspendLayout();
            this.panelProcesses.SuspendLayout();
            this.panelProcessesButtons.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.SuspendLayout();
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
            this.panelSideMenu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelSideMenu.Name = "panelSideMenu";
            this.panelSideMenu.Size = new System.Drawing.Size(318, 420);
            this.panelSideMenu.TabIndex = 2;
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
            this.buttonSettings.Location = new System.Drawing.Point(0, 375);
            this.buttonSettings.Margin = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.buttonSettings.Size = new System.Drawing.Size(318, 45);
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
            this.panelProcesses.Location = new System.Drawing.Point(0, 210);
            this.panelProcesses.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelProcesses.Name = "panelProcesses";
            this.panelProcesses.Size = new System.Drawing.Size(318, 210);
            this.panelProcesses.TabIndex = 3;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 45);
            this.treeView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(318, 165);
            this.treeView1.TabIndex = 1;
            // 
            // panelProcessesButtons
            // 
            this.panelProcessesButtons.Controls.Add(this.buttonScriptView);
            this.panelProcessesButtons.Controls.Add(this.buttonXmlView);
            this.panelProcessesButtons.Controls.Add(this.buttonGraphView);
            this.panelProcessesButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelProcessesButtons.Location = new System.Drawing.Point(0, 0);
            this.panelProcessesButtons.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelProcessesButtons.Name = "panelProcessesButtons";
            this.panelProcessesButtons.Size = new System.Drawing.Size(318, 45);
            this.panelProcessesButtons.TabIndex = 0;
            // 
            // buttonScriptView
            // 
            this.buttonScriptView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonScriptView.Image = ((System.Drawing.Image)(resources.GetObject("buttonScriptView.Image")));
            this.buttonScriptView.Location = new System.Drawing.Point(93, 2);
            this.buttonScriptView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonScriptView.Name = "buttonScriptView";
            this.buttonScriptView.Size = new System.Drawing.Size(39, 39);
            this.buttonScriptView.TabIndex = 2;
            this.buttonScriptView.UseVisualStyleBackColor = true;
            this.buttonScriptView.Click += new System.EventHandler(this.buttonScriptView_Click);
            // 
            // buttonXmlView
            // 
            this.buttonXmlView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonXmlView.Image = ((System.Drawing.Image)(resources.GetObject("buttonXmlView.Image")));
            this.buttonXmlView.Location = new System.Drawing.Point(48, 3);
            this.buttonXmlView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonXmlView.Name = "buttonXmlView";
            this.buttonXmlView.Size = new System.Drawing.Size(39, 39);
            this.buttonXmlView.TabIndex = 1;
            this.buttonXmlView.UseVisualStyleBackColor = true;
            this.buttonXmlView.Click += new System.EventHandler(this.buttonXmlView_Click);
            // 
            // buttonGraphView
            // 
            this.buttonGraphView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGraphView.Image = ((System.Drawing.Image)(resources.GetObject("buttonGraphView.Image")));
            this.buttonGraphView.Location = new System.Drawing.Point(3, 3);
            this.buttonGraphView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonGraphView.Name = "buttonGraphView";
            this.buttonGraphView.Size = new System.Drawing.Size(39, 39);
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
            this.buttonProcesses.Location = new System.Drawing.Point(0, 165);
            this.buttonProcesses.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.buttonProcesses.Name = "buttonProcesses";
            this.buttonProcesses.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.buttonProcesses.Size = new System.Drawing.Size(318, 45);
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
            this.buttonEditor.Location = new System.Drawing.Point(0, 120);
            this.buttonEditor.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.buttonEditor.Name = "buttonEditor";
            this.buttonEditor.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.buttonEditor.Size = new System.Drawing.Size(318, 45);
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
            this.buttonService.Location = new System.Drawing.Point(0, 75);
            this.buttonService.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.buttonService.Name = "buttonService";
            this.buttonService.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.buttonService.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonService.Size = new System.Drawing.Size(318, 45);
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
            this.panelLogo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Padding = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this.panelLogo.Size = new System.Drawing.Size(318, 75);
            this.panelLogo.TabIndex = 0;
            this.panelLogo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelLogo_MouseDown);
            // 
            // panelPolokusHeader
            // 
            this.panelPolokusHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPolokusHeader.Frozen = false;
            this.panelPolokusHeader.Location = new System.Drawing.Point(9, 8);
            this.panelPolokusHeader.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this.panelPolokusHeader.Name = "panelPolokusHeader";
            this.panelPolokusHeader.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelPolokusHeader.Size = new System.Drawing.Size(300, 59);
            this.panelPolokusHeader.TabIndex = 2;
            this.panelPolokusHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelLogo_MouseDown);
            // 
            // MainWindowSideMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelSideMenu);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainWindowSideMenu";
            this.Size = new System.Drawing.Size(318, 420);
            this.panelSideMenu.ResumeLayout(false);
            this.panelProcesses.ResumeLayout(false);
            this.panelProcessesButtons.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panelSideMenu;
        private PolokusMenuButton buttonSettings;
        private Panel panelProcesses;
        private TreeView treeView1;
        private Panel panelProcessesButtons;
        private PolokusIconButton buttonScriptView;
        private PolokusIconButton buttonXmlView;
        private PolokusIconButton buttonGraphView;
        private PolokusMenuButton buttonProcesses;
        private PolokusMenuButton buttonEditor;
        private PolokusMenuButton buttonService;
        private Panel panelLogo;
        private PolokusHeader panelPolokusHeader;
    }
}
