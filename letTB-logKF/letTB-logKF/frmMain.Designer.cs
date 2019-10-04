namespace letTB_logKF
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.cmdLogs = new System.Windows.Forms.Button();
            this.dgvCRUD = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cmdCrew = new System.Windows.Forms.Button();
            this.cmdStaff = new System.Windows.Forms.Button();
            this.cmdExport = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bnvCRUD = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbxEmp = new System.Windows.Forms.ListBox();
            this.flpBoats = new System.Windows.Forms.FlowLayoutPanel();
            this.calLog = new System.Windows.Forms.MonthCalendar();
            this.flpLogs = new System.Windows.Forms.FlowLayoutPanel();
            this.flpEmps = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.cmdExit = new System.Windows.Forms.Button();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.cmdNew = new System.Windows.Forms.Button();
            this.cmdView = new System.Windows.Forms.Button();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.lblLogs = new System.Windows.Forms.Label();
            this.lblFilter = new System.Windows.Forms.Label();
            this.lblPayroll = new System.Windows.Forms.Label();
            this.lblMenu1 = new System.Windows.Forms.Label();
            this.fbdCSV = new System.Windows.Forms.FolderBrowserDialog();
            this.cmdGetAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCRUD)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bnvCRUD)).BeginInit();
            this.bnvCRUD.SuspendLayout();
            this.panel2.SuspendLayout();
            this.flpEmps.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdLogs
            // 
            this.cmdLogs.Location = new System.Drawing.Point(3, 13);
            this.cmdLogs.Name = "cmdLogs";
            this.cmdLogs.Size = new System.Drawing.Size(82, 29);
            this.cmdLogs.TabIndex = 0;
            this.cmdLogs.Text = "Logs";
            this.cmdLogs.UseVisualStyleBackColor = true;
            this.cmdLogs.Click += new System.EventHandler(this.cmdLogs_Click);
            // 
            // dgvCRUD
            // 
            this.dgvCRUD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCRUD.Location = new System.Drawing.Point(14, 14);
            this.dgvCRUD.Name = "dgvCRUD";
            this.dgvCRUD.Size = new System.Drawing.Size(614, 70);
            this.dgvCRUD.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Controls.Add(this.cmdLogs);
            this.flowLayoutPanel1.Controls.Add(this.cmdCrew);
            this.flowLayoutPanel1.Controls.Add(this.cmdStaff);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(925, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(90, 230);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // cmdCrew
            // 
            this.cmdCrew.Location = new System.Drawing.Point(3, 48);
            this.cmdCrew.Name = "cmdCrew";
            this.cmdCrew.Size = new System.Drawing.Size(82, 29);
            this.cmdCrew.TabIndex = 1;
            this.cmdCrew.Text = "Crew";
            this.cmdCrew.UseVisualStyleBackColor = true;
            this.cmdCrew.Click += new System.EventHandler(this.cmdCrew_Click);
            // 
            // cmdStaff
            // 
            this.cmdStaff.Location = new System.Drawing.Point(3, 83);
            this.cmdStaff.Name = "cmdStaff";
            this.cmdStaff.Size = new System.Drawing.Size(82, 29);
            this.cmdStaff.TabIndex = 2;
            this.cmdStaff.Text = "Staff";
            this.cmdStaff.UseVisualStyleBackColor = true;
            this.cmdStaff.Click += new System.EventHandler(this.cmdStaff_Click);
            // 
            // cmdExport
            // 
            this.cmdExport.Location = new System.Drawing.Point(3, 104);
            this.cmdExport.Name = "cmdExport";
            this.cmdExport.Size = new System.Drawing.Size(82, 29);
            this.cmdExport.TabIndex = 2;
            this.cmdExport.Text = "Export";
            this.cmdExport.UseVisualStyleBackColor = true;
            this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Moccasin;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.bnvCRUD);
            this.panel1.Controls.Add(this.dgvCRUD);
            this.panel1.Location = new System.Drawing.Point(264, 382);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(650, 123);
            this.panel1.TabIndex = 3;
            this.panel1.Visible = false;
            // 
            // bnvCRUD
            // 
            this.bnvCRUD.AddNewItem = this.bindingNavigatorAddNewItem;
            this.bnvCRUD.CountItem = this.bindingNavigatorCountItem;
            this.bnvCRUD.DeleteItem = this.bindingNavigatorDeleteItem;
            this.bnvCRUD.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bnvCRUD.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem});
            this.bnvCRUD.Location = new System.Drawing.Point(0, 96);
            this.bnvCRUD.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bnvCRUD.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bnvCRUD.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bnvCRUD.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bnvCRUD.Name = "bnvCRUD";
            this.bnvCRUD.PositionItem = this.bindingNavigatorPositionItem;
            this.bnvCRUD.Size = new System.Drawing.Size(648, 25);
            this.bnvCRUD.TabIndex = 2;
            this.bnvCRUD.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.Color.Moccasin;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.cmdGetAll);
            this.panel2.Controls.Add(this.chkAll);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lbxEmp);
            this.panel2.Controls.Add(this.flpBoats);
            this.panel2.Controls.Add(this.calLog);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(246, 537);
            this.panel2.TabIndex = 4;
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(179, 488);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(57, 17);
            this.chkAll.TabIndex = 7;
            this.chkAll.Text = "Get All";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.Visible = false;            
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 491);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(205, 39);
            this.label3.TabIndex = 6;
            this.label3.Text = "Date Range :\r\nIn month calendar, click on Start Date\r\nand then drag mouse pointer" +
    " to End Date.";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Moccasin;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(94, 197);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Worked :";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Moccasin;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 196);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Logged :";
            // 
            // lbxEmp
            // 
            this.lbxEmp.FormattingEnabled = true;
            this.lbxEmp.Location = new System.Drawing.Point(97, 218);
            this.lbxEmp.Name = "lbxEmp";
            this.lbxEmp.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbxEmp.Size = new System.Drawing.Size(138, 264);
            this.lbxEmp.TabIndex = 3;
            this.lbxEmp.SelectedIndexChanged += new System.EventHandler(this.lbxEmp_SelectedIndexChanged);
            // 
            // flpBoats
            // 
            this.flpBoats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpBoats.Location = new System.Drawing.Point(9, 219);
            this.flpBoats.Name = "flpBoats";
            this.flpBoats.Size = new System.Drawing.Size(82, 262);
            this.flpBoats.TabIndex = 2;
            // 
            // calLog
            // 
            this.calLog.Location = new System.Drawing.Point(9, 11);
            this.calLog.MaxSelectionCount = 31;
            this.calLog.Name = "calLog";
            this.calLog.TabIndex = 1;
            this.calLog.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.calLog_DateSelected);
            // 
            // flpLogs
            // 
            this.flpLogs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.flpLogs.AutoScroll = true;
            this.flpLogs.BackColor = System.Drawing.SystemColors.Control;
            this.flpLogs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpLogs.Location = new System.Drawing.Point(264, 12);
            this.flpLogs.Name = "flpLogs";
            this.flpLogs.Size = new System.Drawing.Size(654, 537);
            this.flpLogs.TabIndex = 5;
            // 
            // flpEmps
            // 
            this.flpEmps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.flpEmps.AutoScroll = true;
            this.flpEmps.BackColor = System.Drawing.SystemColors.Control;
            this.flpEmps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpEmps.Controls.Add(this.flowLayoutPanel3);
            this.flpEmps.Location = new System.Drawing.Point(925, 12);
            this.flpEmps.Name = "flpEmps";
            this.flpEmps.Size = new System.Drawing.Size(654, 536);
            this.flpEmps.TabIndex = 6;
            this.flpEmps.Visible = false;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(90, 0);
            this.flowLayoutPanel3.TabIndex = 49;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel2.Controls.Add(this.cmdExit);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(925, 448);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(90, 100);
            this.flowLayoutPanel2.TabIndex = 34;
            // 
            // cmdExit
            // 
            this.cmdExit.Location = new System.Drawing.Point(3, 66);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(82, 29);
            this.cmdExit.TabIndex = 0;
            this.cmdExit.Text = "Close";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel4.Controls.Add(this.cmdNew);
            this.flowLayoutPanel4.Controls.Add(this.cmdView);
            this.flowLayoutPanel4.Controls.Add(this.cmdExport);
            this.flowLayoutPanel4.Controls.Add(this.cmdPrint);
            this.flowLayoutPanel4.Location = new System.Drawing.Point(925, 248);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(90, 194);
            this.flowLayoutPanel4.TabIndex = 49;
            // 
            // cmdNew
            // 
            this.cmdNew.Location = new System.Drawing.Point(3, 3);
            this.cmdNew.Name = "cmdNew";
            this.cmdNew.Size = new System.Drawing.Size(82, 29);
            this.cmdNew.TabIndex = 1;
            this.cmdNew.Text = "New";
            this.cmdNew.UseVisualStyleBackColor = true;
            this.cmdNew.Visible = false;
            this.cmdNew.Click += new System.EventHandler(this.cmdNew_Click);
            // 
            // cmdView
            // 
            this.cmdView.Location = new System.Drawing.Point(3, 38);
            this.cmdView.Name = "cmdView";
            this.cmdView.Size = new System.Drawing.Size(82, 60);
            this.cmdView.TabIndex = 0;
            this.cmdView.Text = "View Multi-Select";
            this.cmdView.UseVisualStyleBackColor = true;
            this.cmdView.Visible = false;
            this.cmdView.Click += new System.EventHandler(this.cmdView_Click);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Location = new System.Drawing.Point(3, 139);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(82, 29);
            this.cmdPrint.TabIndex = 3;
            this.cmdPrint.Text = "Print";
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // lblLogs
            // 
            this.lblLogs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogs.Location = new System.Drawing.Point(273, 3);
            this.lblLogs.Name = "lblLogs";
            this.lblLogs.Size = new System.Drawing.Size(60, 19);
            this.lblLogs.TabIndex = 50;
            this.lblLogs.Text = "Logs";
            this.lblLogs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFilter
            // 
            this.lblFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilter.Location = new System.Drawing.Point(21, 2);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(60, 19);
            this.lblFilter.TabIndex = 52;
            this.lblFilter.Text = "Filters";
            this.lblFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPayroll
            // 
            this.lblPayroll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPayroll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayroll.Location = new System.Drawing.Point(933, 3);
            this.lblPayroll.Name = "lblPayroll";
            this.lblPayroll.Size = new System.Drawing.Size(60, 19);
            this.lblPayroll.TabIndex = 53;
            this.lblPayroll.Text = "Crew";
            this.lblPayroll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPayroll.Visible = false;
            // 
            // lblMenu1
            // 
            this.lblMenu1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMenu1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMenu1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenu1.Location = new System.Drawing.Point(933, 3);
            this.lblMenu1.Name = "lblMenu1";
            this.lblMenu1.Size = new System.Drawing.Size(60, 19);
            this.lblMenu1.TabIndex = 54;
            this.lblMenu1.Text = "Views";
            this.lblMenu1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmdGetAll
            // 
            this.cmdGetAll.Location = new System.Drawing.Point(179, 185);
            this.cmdGetAll.Name = "cmdGetAll";
            this.cmdGetAll.Size = new System.Drawing.Size(57, 23);
            this.cmdGetAll.TabIndex = 8;
            this.cmdGetAll.Text = "Get All";
            this.cmdGetAll.UseVisualStyleBackColor = true;
            this.cmdGetAll.Click += new System.EventHandler(this.cmdGetAll_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 561);
            this.Controls.Add(this.lblMenu1);
            this.Controls.Add(this.lblPayroll);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.lblLogs);
            this.Controls.Add(this.flowLayoutPanel4);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flpLogs);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flpEmps);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "Timebook Log";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.SizeChanged += new System.EventHandler(this.frmMain_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCRUD)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bnvCRUD)).EndInit();
            this.bnvCRUD.ResumeLayout(false);
            this.bnvCRUD.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.flpEmps.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdLogs;
        private System.Windows.Forms.DataGridView dgvCRUD;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.BindingNavigator bnvCRUD;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.MonthCalendar calLog;
        private System.Windows.Forms.FlowLayoutPanel flpLogs;
        private System.Windows.Forms.Button cmdCrew;
        private System.Windows.Forms.FlowLayoutPanel flpBoats;
        private System.Windows.Forms.ListBox lbxEmp;
        private System.Windows.Forms.FlowLayoutPanel flpEmps;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdExport;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Button cmdNew;
        private System.Windows.Forms.Button cmdView;
        private System.Windows.Forms.Label lblLogs;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.Label lblPayroll;
        private System.Windows.Forms.Label lblMenu1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FolderBrowserDialog fbdCSV;
        private System.Windows.Forms.Button cmdPrint;
        private System.Windows.Forms.Button cmdStaff;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.Button cmdGetAll;
    }
}

