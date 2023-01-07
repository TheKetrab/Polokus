using Polokus.App.Controls;

namespace Polokus.App.Views
{
    partial class ServiceView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceView));
            this.panel1 = new System.Windows.Forms.Panel();
            this.readOnlyRichTextBox1 = new Polokus.App.Controls.ReadOnlyRichTextBox();
            this.panelBpmnio = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.buttonRaiseSignal = new System.Windows.Forms.Button();
            this.textBoxRaiseSignal = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.buttonPingWaiter = new System.Windows.Forms.Button();
            this.textBoxPingWaiter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.listViewWaiters = new Polokus.App.Controls.PolokusNarrowListView();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.label3 = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.listViewStarters = new Polokus.App.Controls.PolokusNarrowListView();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.label8 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.listViewInstances = new Polokus.App.Controls.PolokusNarrowListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.label6 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.listViewProcesses = new Polokus.App.Controls.PolokusNarrowListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.label7 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonStop = new Polokus.App.Controls.PolokusIconButton();
            this.buttonPause = new Polokus.App.Controls.PolokusIconButton();
            this.buttonStart = new Polokus.App.Controls.PolokusIconButton();
            this.buttonAdd = new Polokus.App.Controls.PolokusIconButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelWorkspace = new System.Windows.Forms.Panel();
            this.splitContainerWorkspace1 = new System.Windows.Forms.SplitContainer();
            this.panelWorkflowSelect = new System.Windows.Forms.Panel();
            this.buttonLoadWorkflow = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxWorkflows = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelWorkspace.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerWorkspace1)).BeginInit();
            this.splitContainerWorkspace1.Panel1.SuspendLayout();
            this.splitContainerWorkspace1.Panel2.SuspendLayout();
            this.splitContainerWorkspace1.SuspendLayout();
            this.panelWorkflowSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.Controls.Add(this.readOnlyRichTextBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(671, 189);
            this.panel1.TabIndex = 0;
            // 
            // readOnlyRichTextBox1
            // 
            this.readOnlyRichTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.readOnlyRichTextBox1.Location = new System.Drawing.Point(0, 0);
            this.readOnlyRichTextBox1.Name = "readOnlyRichTextBox1";
            this.readOnlyRichTextBox1.Size = new System.Drawing.Size(671, 189);
            this.readOnlyRichTextBox1.TabIndex = 1;
            this.readOnlyRichTextBox1.Text = "";
            // 
            // panelBpmnio
            // 
            this.panelBpmnio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBpmnio.Location = new System.Drawing.Point(0, 0);
            this.panelBpmnio.Name = "panelBpmnio";
            this.panelBpmnio.Size = new System.Drawing.Size(671, 384);
            this.panelBpmnio.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Controls.Add(this.panel10);
            this.panel2.Controls.Add(this.panel9);
            this.panel2.Controls.Add(this.panel8);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5, 5, 10, 5);
            this.panel2.Size = new System.Drawing.Size(280, 576);
            this.panel2.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.buttonRaiseSignal);
            this.panel5.Controls.Add(this.textBoxRaiseSignal);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(5, 522);
            this.panel5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(265, 35);
            this.panel5.TabIndex = 3;
            // 
            // buttonRaiseSignal
            // 
            this.buttonRaiseSignal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRaiseSignal.Location = new System.Drawing.Point(190, 5);
            this.buttonRaiseSignal.Name = "buttonRaiseSignal";
            this.buttonRaiseSignal.Size = new System.Drawing.Size(75, 25);
            this.buttonRaiseSignal.TabIndex = 2;
            this.buttonRaiseSignal.Text = "Raise";
            this.buttonRaiseSignal.UseVisualStyleBackColor = true;
            this.buttonRaiseSignal.Click += new System.EventHandler(this.buttonRaiseSignal_Click);
            // 
            // textBoxRaiseSignal
            // 
            this.textBoxRaiseSignal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRaiseSignal.Location = new System.Drawing.Point(83, 7);
            this.textBoxRaiseSignal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxRaiseSignal.Name = "textBoxRaiseSignal";
            this.textBoxRaiseSignal.Size = new System.Drawing.Size(100, 23);
            this.textBoxRaiseSignal.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Raise signal:";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.buttonPingWaiter);
            this.panel4.Controls.Add(this.textBoxPingWaiter);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(5, 487);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(265, 35);
            this.panel4.TabIndex = 2;
            // 
            // buttonPingWaiter
            // 
            this.buttonPingWaiter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPingWaiter.Location = new System.Drawing.Point(189, 5);
            this.buttonPingWaiter.Name = "buttonPingWaiter";
            this.buttonPingWaiter.Size = new System.Drawing.Size(75, 25);
            this.buttonPingWaiter.TabIndex = 2;
            this.buttonPingWaiter.Text = "Ping";
            this.buttonPingWaiter.UseVisualStyleBackColor = true;
            this.buttonPingWaiter.Click += new System.EventHandler(this.buttonPingWaiter_Click);
            // 
            // textBoxPingWaiter
            // 
            this.textBoxPingWaiter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPingWaiter.Location = new System.Drawing.Point(83, 8);
            this.textBoxPingWaiter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxPingWaiter.Name = "textBoxPingWaiter";
            this.textBoxPingWaiter.Size = new System.Drawing.Size(100, 23);
            this.textBoxPingWaiter.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ping msg:";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.listViewWaiters);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(5, 387);
            this.panel6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(265, 100);
            this.panel6.TabIndex = 10;
            // 
            // listViewWaiters
            // 
            this.listViewWaiters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader9});
            this.listViewWaiters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewWaiters.Location = new System.Drawing.Point(0, 15);
            this.listViewWaiters.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listViewWaiters.Name = "listViewWaiters";
            this.listViewWaiters.Size = new System.Drawing.Size(265, 85);
            this.listViewWaiters.TabIndex = 7;
            this.listViewWaiters.UseCompatibleStateImageBehavior = false;
            this.listViewWaiters.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Process instance";
            this.columnHeader6.Width = 120;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Node to call Id";
            this.columnHeader7.Width = 80;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Node handler waiters";
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.listViewStarters);
            this.panel10.Controls.Add(this.label8);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(5, 287);
            this.panel10.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(265, 100);
            this.panel10.TabIndex = 9;
            // 
            // listViewStarters
            // 
            this.listViewStarters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader8});
            this.listViewStarters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewStarters.Location = new System.Drawing.Point(0, 15);
            this.listViewStarters.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listViewStarters.Name = "listViewStarters";
            this.listViewStarters.Size = new System.Drawing.Size(265, 85);
            this.listViewStarters.TabIndex = 7;
            this.listViewStarters.UseCompatibleStateImageBehavior = false;
            this.listViewStarters.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Starter Id";
            this.columnHeader4.Width = 120;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Start node";
            this.columnHeader5.Width = 80;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Type";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 15);
            this.label8.TabIndex = 8;
            this.label8.Text = "Process starters";
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.listViewInstances);
            this.panel9.Controls.Add(this.label6);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(5, 137);
            this.panel9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(265, 150);
            this.panel9.TabIndex = 5;
            // 
            // listViewInstances
            // 
            this.listViewInstances.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader10});
            this.listViewInstances.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewInstances.Location = new System.Drawing.Point(0, 15);
            this.listViewInstances.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listViewInstances.Name = "listViewInstances";
            this.listViewInstances.Size = new System.Drawing.Size(265, 135);
            this.listViewInstances.TabIndex = 7;
            this.listViewInstances.UseCompatibleStateImageBehavior = false;
            this.listViewInstances.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Instances Ids";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Status";
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Tasks";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 15);
            this.label6.TabIndex = 8;
            this.label6.Text = "Running processes";
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.SystemColors.Control;
            this.panel8.Controls.Add(this.listViewProcesses);
            this.panel8.Controls.Add(this.label7);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(5, 37);
            this.panel8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(265, 100);
            this.panel8.TabIndex = 6;
            // 
            // listViewProcesses
            // 
            this.listViewProcesses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewProcesses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewProcesses.Location = new System.Drawing.Point(0, 15);
            this.listViewProcesses.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listViewProcesses.Name = "listViewProcesses";
            this.listViewProcesses.Size = new System.Drawing.Size(265, 85);
            this.listViewProcesses.TabIndex = 1;
            this.listViewProcesses.UseCompatibleStateImageBehavior = false;
            this.listViewProcesses.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Available processes";
            this.columnHeader1.Width = 200;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "Defined processes";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.buttonStop);
            this.panel3.Controls.Add(this.buttonPause);
            this.panel3.Controls.Add(this.buttonStart);
            this.panel3.Controls.Add(this.buttonAdd);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(5, 5);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panel3.Size = new System.Drawing.Size(265, 32);
            this.panel3.TabIndex = 1;
            // 
            // buttonStop
            // 
            this.buttonStop.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStop.Image = ((System.Drawing.Image)(resources.GetObject("buttonStop.Image")));
            this.buttonStop.Location = new System.Drawing.Point(99, 2);
            this.buttonStop.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Padding = new System.Windows.Forms.Padding(4);
            this.buttonStop.Size = new System.Drawing.Size(32, 28);
            this.buttonStop.TabIndex = 2;
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonPause
            // 
            this.buttonPause.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPause.Image = ((System.Drawing.Image)(resources.GetObject("buttonPause.Image")));
            this.buttonPause.Location = new System.Drawing.Point(67, 2);
            this.buttonPause.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Padding = new System.Windows.Forms.Padding(4);
            this.buttonPause.Size = new System.Drawing.Size(32, 28);
            this.buttonPause.TabIndex = 1;
            this.buttonPause.UseVisualStyleBackColor = true;
            this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStart.Image = ((System.Drawing.Image)(resources.GetObject("buttonStart.Image")));
            this.buttonStart.Location = new System.Drawing.Point(35, 2);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Padding = new System.Windows.Forms.Padding(4);
            this.buttonStart.Size = new System.Drawing.Size(32, 28);
            this.buttonStart.TabIndex = 3;
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Image = ((System.Drawing.Image)(resources.GetObject("buttonAdd.Image")));
            this.buttonAdd.Location = new System.Drawing.Point(3, 2);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Padding = new System.Windows.Forms.Padding(4);
            this.buttonAdd.Size = new System.Drawing.Size(32, 28);
            this.buttonAdd.TabIndex = 0;
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panelBpmnio);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(671, 576);
            this.splitContainer1.SplitterDistance = 384;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 0;
            // 
            // panelWorkspace
            // 
            this.panelWorkspace.Controls.Add(this.splitContainerWorkspace1);
            this.panelWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWorkspace.Location = new System.Drawing.Point(0, 40);
            this.panelWorkspace.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelWorkspace.Name = "panelWorkspace";
            this.panelWorkspace.Size = new System.Drawing.Size(955, 576);
            this.panelWorkspace.TabIndex = 0;
            // 
            // splitContainerWorkspace1
            // 
            this.splitContainerWorkspace1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerWorkspace1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerWorkspace1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainerWorkspace1.Name = "splitContainerWorkspace1";
            // 
            // splitContainerWorkspace1.Panel1
            // 
            this.splitContainerWorkspace1.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainerWorkspace1.Panel2
            // 
            this.splitContainerWorkspace1.Panel2.Controls.Add(this.panel2);
            this.splitContainerWorkspace1.Size = new System.Drawing.Size(955, 576);
            this.splitContainerWorkspace1.SplitterDistance = 671;
            this.splitContainerWorkspace1.TabIndex = 0;
            // 
            // panelWorkflowSelect
            // 
            this.panelWorkflowSelect.Controls.Add(this.buttonLoadWorkflow);
            this.panelWorkflowSelect.Controls.Add(this.label5);
            this.panelWorkflowSelect.Controls.Add(this.comboBoxWorkflows);
            this.panelWorkflowSelect.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelWorkflowSelect.Location = new System.Drawing.Point(0, 0);
            this.panelWorkflowSelect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelWorkflowSelect.Name = "panelWorkflowSelect";
            this.panelWorkflowSelect.Size = new System.Drawing.Size(955, 40);
            this.panelWorkflowSelect.TabIndex = 0;
            // 
            // buttonLoadWorkflow
            // 
            this.buttonLoadWorkflow.Location = new System.Drawing.Point(516, 10);
            this.buttonLoadWorkflow.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonLoadWorkflow.Name = "buttonLoadWorkflow";
            this.buttonLoadWorkflow.Size = new System.Drawing.Size(82, 22);
            this.buttonLoadWorkflow.TabIndex = 2;
            this.buttonLoadWorkflow.Text = "Load";
            this.buttonLoadWorkflow.UseVisualStyleBackColor = true;
            this.buttonLoadWorkflow.Click += new System.EventHandler(this.buttonLoadWorkflow_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "Workflow:";
            // 
            // comboBoxWorkflows
            // 
            this.comboBoxWorkflows.FormattingEnabled = true;
            this.comboBoxWorkflows.Location = new System.Drawing.Point(91, 10);
            this.comboBoxWorkflows.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxWorkflows.Name = "comboBoxWorkflows";
            this.comboBoxWorkflows.Size = new System.Drawing.Size(420, 23);
            this.comboBoxWorkflows.TabIndex = 0;
            // 
            // ServiceView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelWorkspace);
            this.Controls.Add(this.panelWorkflowSelect);
            this.Name = "ServiceView";
            this.Size = new System.Drawing.Size(955, 616);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelWorkspace.ResumeLayout(false);
            this.splitContainerWorkspace1.Panel1.ResumeLayout(false);
            this.splitContainerWorkspace1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerWorkspace1)).EndInit();
            this.splitContainerWorkspace1.ResumeLayout(false);
            this.panelWorkflowSelect.ResumeLayout(false);
            this.panelWorkflowSelect.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Controls.ReadOnlyRichTextBox readOnlyRichTextBox1;
        private Panel panelBpmnio;
        private Panel panel2;
        private SplitContainer splitContainer1;
        private Panel panel3;
        private PolokusIconButton buttonStart;
        private PolokusIconButton buttonStop;
        private PolokusIconButton buttonPause;
        private PolokusIconButton buttonAdd;
        private Panel panel4;
        private TextBox textBoxPingWaiter;
        private Label label1;
        private Panel panel8;
        private PolokusNarrowListView listViewProcesses;
        private Panel panelWorkspace;
        private Panel panelWorkflowSelect;
        private Button buttonLoadWorkflow;
        private Label label5;
        private ComboBox comboBoxWorkflows;
        private SplitContainer splitContainerWorkspace1;
        private Panel panel9;
        private PolokusNarrowListView listViewInstances;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private Label label7;
        private Label label6;
        private Panel panel10;
        private PolokusNarrowListView listViewStarters;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private Label label8;
        private Panel panel6;
        private PolokusNarrowListView listViewWaiters;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private Label label3;
        private ColumnHeader columnHeader9;
        private ColumnHeader columnHeader8;
        private Button buttonPingWaiter;
        private ColumnHeader columnHeader10;
        private Panel panel5;
        private Button buttonRaiseSignal;
        private TextBox textBoxRaiseSignal;
        private Label label2;
    }
}
