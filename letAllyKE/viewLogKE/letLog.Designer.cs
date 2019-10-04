namespace viewLogKE
{
    partial class letLog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dtpLogDate = new System.Windows.Forms.DateTimePicker();
            this.flpVessel = new System.Windows.Forms.FlowLayoutPanel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdSheet = new System.Windows.Forms.Button();
            this.cbxShips = new System.Windows.Forms.ComboBox();
            this.lblShift = new System.Windows.Forms.Label();
            this.cbxShipShift = new System.Windows.Forms.ComboBox();
            this.lblFuel = new System.Windows.Forms.Label();
            this.lblHourFinish = new System.Windows.Forms.Label();
            this.lblHourStart = new System.Windows.Forms.Label();
            this.lblLog = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.nudShift = new System.Windows.Forms.NumericUpDown();
            this.gbxCrew = new System.Windows.Forms.GroupBox();
            this.cmdRight = new System.Windows.Forms.Button();
            this.cmdLeft = new System.Windows.Forms.Button();
            this.tabShift = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblID = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbxCrewCount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxOverHours = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxPaidHours = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxOnVessel = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxCrewHours = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAudit = new System.Windows.Forms.Label();
            this.cbxVerifyDate = new System.Windows.Forms.ComboBox();
            this.lblLogUser = new System.Windows.Forms.Label();
            this.lblLogDate = new System.Windows.Forms.Label();
            this.lblVerifyUser = new System.Windows.Forms.Label();
            this.lblVerifyDate = new System.Windows.Forms.Label();
            this.cmdLogs = new System.Windows.Forms.Button();
            this.flpLogs = new System.Windows.Forms.FlowLayoutPanel();
            this.cmdNew = new System.Windows.Forms.Button();
            this.dgvTB = new System.Windows.Forms.DataGridView();
            this.cmdGrid = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.tbxV1A = new System.Windows.Forms.TextBox();
            this.cmdDel = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudShift)).BeginInit();
            this.gbxCrew.SuspendLayout();
            this.tabShift.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTB)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpLogDate
            // 
            this.dtpLogDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpLogDate.Location = new System.Drawing.Point(280, 13);
            this.dtpLogDate.Name = "dtpLogDate";
            this.dtpLogDate.Size = new System.Drawing.Size(220, 20);
            this.dtpLogDate.TabIndex = 0;
            this.dtpLogDate.CloseUp += new System.EventHandler(this.dtpLogDate_CloseUp);
            // 
            // flpVessel
            // 
            this.flpVessel.Location = new System.Drawing.Point(9, 8);
            this.flpVessel.Name = "flpVessel";
            this.flpVessel.Size = new System.Drawing.Size(82, 420);
            this.flpVessel.TabIndex = 38;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(545, 449);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 22);
            this.cmdOK.TabIndex = 25;
            this.cmdOK.Text = "Save";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(626, 449);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 22);
            this.cmdCancel.TabIndex = 26;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.groupBox1.Controls.Add(this.cmdSheet);
            this.groupBox1.Controls.Add(this.cbxShips);
            this.groupBox1.Controls.Add(this.lblShift);
            this.groupBox1.Controls.Add(this.cbxShipShift);
            this.groupBox1.Controls.Add(this.lblFuel);
            this.groupBox1.Controls.Add(this.lblHourFinish);
            this.groupBox1.Controls.Add(this.lblHourStart);
            this.groupBox1.Controls.Add(this.lblLog);
            this.groupBox1.Location = new System.Drawing.Point(14, 334);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(729, 63);
            this.groupBox1.TabIndex = 72;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Vessel :";
            // 
            // cmdSheet
            // 
            this.cmdSheet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSheet.Location = new System.Drawing.Point(664, 22);
            this.cmdSheet.Name = "cmdSheet";
            this.cmdSheet.Size = new System.Drawing.Size(56, 22);
            this.cmdSheet.TabIndex = 102;
            this.cmdSheet.Text = "6 Crew";
            this.cmdSheet.UseVisualStyleBackColor = true;
            this.cmdSheet.Click += new System.EventHandler(this.cmdSheet_Click);
            // 
            // cbxShips
            // 
            this.cbxShips.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxShips.FormattingEnabled = true;
            this.cbxShips.Location = new System.Drawing.Point(9, 22);
            this.cbxShips.Name = "cbxShips";
            this.cbxShips.Size = new System.Drawing.Size(184, 22);
            this.cbxShips.TabIndex = 103;
            this.cbxShips.Text = "Vessel";
            this.cbxShips.SelectionChangeCommitted += new System.EventHandler(this.cbxShips_SelectionChangeCommitted);
            // 
            // lblShift
            // 
            this.lblShift.BackColor = System.Drawing.Color.White;
            this.lblShift.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblShift.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShift.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblShift.Location = new System.Drawing.Point(358, 22);
            this.lblShift.Name = "lblShift";
            this.lblShift.Size = new System.Drawing.Size(72, 18);
            this.lblShift.TabIndex = 100;
            this.lblShift.Text = "<log shift>";
            this.lblShift.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblShift.Visible = false;
            // 
            // cbxShipShift
            // 
            this.cbxShipShift.FormattingEnabled = true;
            this.cbxShipShift.Location = new System.Drawing.Point(199, 22);
            this.cbxShipShift.Name = "cbxShipShift";
            this.cbxShipShift.Size = new System.Drawing.Size(68, 21);
            this.cbxShipShift.TabIndex = 104;
            this.cbxShipShift.SelectionChangeCommitted += new System.EventHandler(this.cbxShipShift_SelectionChangeCommitted);
            // 
            // lblFuel
            // 
            this.lblFuel.BackColor = System.Drawing.Color.White;
            this.lblFuel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFuel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFuel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFuel.Location = new System.Drawing.Point(592, 22);
            this.lblFuel.Name = "lblFuel";
            this.lblFuel.Size = new System.Drawing.Size(82, 18);
            this.lblFuel.TabIndex = 99;
            this.lblFuel.Text = "<fuel>";
            this.lblFuel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFuel.Visible = false;
            // 
            // lblHourFinish
            // 
            this.lblHourFinish.BackColor = System.Drawing.Color.White;
            this.lblHourFinish.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHourFinish.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHourFinish.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblHourFinish.Location = new System.Drawing.Point(511, 22);
            this.lblHourFinish.Name = "lblHourFinish";
            this.lblHourFinish.Size = new System.Drawing.Size(82, 18);
            this.lblHourFinish.TabIndex = 98;
            this.lblHourFinish.Text = "<engine hours>";
            this.lblHourFinish.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblHourFinish.Visible = false;
            // 
            // lblHourStart
            // 
            this.lblHourStart.BackColor = System.Drawing.Color.White;
            this.lblHourStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHourStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHourStart.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblHourStart.Location = new System.Drawing.Point(430, 22);
            this.lblHourStart.Name = "lblHourStart";
            this.lblHourStart.Size = new System.Drawing.Size(82, 18);
            this.lblHourStart.TabIndex = 97;
            this.lblHourStart.Text = "<engine hours>";
            this.lblHourStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblHourStart.Visible = false;
            // 
            // lblLog
            // 
            this.lblLog.BackColor = System.Drawing.Color.White;
            this.lblLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLog.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblLog.Location = new System.Drawing.Point(288, 22);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(72, 18);
            this.lblLog.TabIndex = 96;
            this.lblLog.Text = "<log sheet>";
            this.lblLog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLog.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(860, 42);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(28, 13);
            this.label10.TabIndex = 92;
            this.label10.Text = "Shift";
            // 
            // nudShift
            // 
            this.nudShift.Location = new System.Drawing.Point(857, 21);
            this.nudShift.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nudShift.Name = "nudShift";
            this.nudShift.Size = new System.Drawing.Size(31, 20);
            this.nudShift.TabIndex = 90;
            this.nudShift.ValueChanged += new System.EventHandler(this.nudShift_ValueChanged);
            // 
            // gbxCrew
            // 
            this.gbxCrew.Controls.Add(this.cmdRight);
            this.gbxCrew.Controls.Add(this.cmdLeft);
            this.gbxCrew.Controls.Add(this.dtpLogDate);
            this.gbxCrew.Controls.Add(this.tabShift);
            this.gbxCrew.Controls.Add(this.lblID);
            this.gbxCrew.Location = new System.Drawing.Point(12, 8);
            this.gbxCrew.Name = "gbxCrew";
            this.gbxCrew.Size = new System.Drawing.Size(730, 324);
            this.gbxCrew.TabIndex = 76;
            this.gbxCrew.TabStop = false;
            this.gbxCrew.Text = "Crew :";
            // 
            // cmdRight
            // 
            this.cmdRight.Location = new System.Drawing.Point(507, 12);
            this.cmdRight.Name = "cmdRight";
            this.cmdRight.Size = new System.Drawing.Size(75, 22);
            this.cmdRight.TabIndex = 104;
            this.cmdRight.Text = ">>";
            this.cmdRight.UseVisualStyleBackColor = true;
            // 
            // cmdLeft
            // 
            this.cmdLeft.Location = new System.Drawing.Point(196, 12);
            this.cmdLeft.Name = "cmdLeft";
            this.cmdLeft.Size = new System.Drawing.Size(75, 22);
            this.cmdLeft.TabIndex = 103;
            this.cmdLeft.Text = "<<";
            this.cmdLeft.UseVisualStyleBackColor = true;
            // 
            // tabShift
            // 
            this.tabShift.Controls.Add(this.tabPage1);
            this.tabShift.Location = new System.Drawing.Point(6, 18);
            this.tabShift.Name = "tabShift";
            this.tabShift.SelectedIndex = 0;
            this.tabShift.Size = new System.Drawing.Size(718, 300);
            this.tabShift.TabIndex = 102;
            this.tabShift.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabShift_Selected);
            this.tabShift.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabShift_Deselecting);
            this.tabShift.DoubleClick += new System.EventHandler(this.tabShift_DoubleClick);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(710, 274);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "AM";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblID
            // 
            this.lblID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblID.Location = new System.Drawing.Point(680, 0);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(40, 14);
            this.lblID.TabIndex = 76;
            this.lblID.Text = "0";
            this.lblID.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbxCrewCount);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.tbxOverHours);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.tbxPaidHours);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.tbxOnVessel);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.tbxCrewHours);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(14, 402);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(730, 41);
            this.groupBox3.TabIndex = 77;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Totals :";
            // 
            // tbxCrewCount
            // 
            this.tbxCrewCount.BackColor = System.Drawing.Color.SandyBrown;
            this.tbxCrewCount.Location = new System.Drawing.Point(646, 15);
            this.tbxCrewCount.Name = "tbxCrewCount";
            this.tbxCrewCount.ReadOnly = true;
            this.tbxCrewCount.Size = new System.Drawing.Size(34, 20);
            this.tbxCrewCount.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(593, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Crew # :";
            // 
            // tbxOverHours
            // 
            this.tbxOverHours.BackColor = System.Drawing.Color.SandyBrown;
            this.tbxOverHours.Location = new System.Drawing.Point(552, 15);
            this.tbxOverHours.Name = "tbxOverHours";
            this.tbxOverHours.ReadOnly = true;
            this.tbxOverHours.Size = new System.Drawing.Size(34, 20);
            this.tbxOverHours.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(468, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Premium Hours :";
            // 
            // tbxPaidHours
            // 
            this.tbxPaidHours.BackColor = System.Drawing.Color.SandyBrown;
            this.tbxPaidHours.Location = new System.Drawing.Point(423, 15);
            this.tbxPaidHours.Name = "tbxPaidHours";
            this.tbxPaidHours.ReadOnly = true;
            this.tbxPaidHours.Size = new System.Drawing.Size(34, 20);
            this.tbxPaidHours.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(352, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Paid Hours :";
            // 
            // tbxOnVessel
            // 
            this.tbxOnVessel.BackColor = System.Drawing.Color.SandyBrown;
            this.tbxOnVessel.Location = new System.Drawing.Point(206, 15);
            this.tbxOnVessel.Name = "tbxOnVessel";
            this.tbxOnVessel.ReadOnly = true;
            this.tbxOnVessel.Size = new System.Drawing.Size(34, 20);
            this.tbxOnVessel.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(139, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "On Vessel :";
            // 
            // tbxCrewHours
            // 
            this.tbxCrewHours.BackColor = System.Drawing.Color.SandyBrown;
            this.tbxCrewHours.Location = new System.Drawing.Point(84, 15);
            this.tbxCrewHours.Name = "tbxCrewHours";
            this.tbxCrewHours.ReadOnly = true;
            this.tbxCrewHours.Size = new System.Drawing.Size(34, 20);
            this.tbxCrewHours.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Crew Hours :";
            // 
            // lblAudit
            // 
            this.lblAudit.BackColor = System.Drawing.Color.Black;
            this.lblAudit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAudit.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAudit.ForeColor = System.Drawing.Color.White;
            this.lblAudit.Location = new System.Drawing.Point(12, 453);
            this.lblAudit.Name = "lblAudit";
            this.lblAudit.Size = new System.Drawing.Size(50, 17);
            this.lblAudit.TabIndex = 78;
            this.lblAudit.Text = "Audit";
            this.lblAudit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbxVerifyDate
            // 
            this.cbxVerifyDate.DropDownWidth = 200;
            this.cbxVerifyDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxVerifyDate.FormattingEnabled = true;
            this.cbxVerifyDate.Location = new System.Drawing.Point(364, 453);
            this.cbxVerifyDate.Name = "cbxVerifyDate";
            this.cbxVerifyDate.Size = new System.Drawing.Size(36, 17);
            this.cbxVerifyDate.TabIndex = 83;
            // 
            // lblLogUser
            // 
            this.lblLogUser.BackColor = System.Drawing.Color.White;
            this.lblLogUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLogUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogUser.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblLogUser.Location = new System.Drawing.Point(60, 453);
            this.lblLogUser.Name = "lblLogUser";
            this.lblLogUser.Size = new System.Drawing.Size(72, 17);
            this.lblLogUser.TabIndex = 85;
            this.lblLogUser.Text = "<log user>";
            this.lblLogUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLogDate
            // 
            this.lblLogDate.BackColor = System.Drawing.Color.White;
            this.lblLogDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLogDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogDate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblLogDate.Location = new System.Drawing.Point(130, 453);
            this.lblLogDate.Name = "lblLogDate";
            this.lblLogDate.Size = new System.Drawing.Size(82, 17);
            this.lblLogDate.TabIndex = 86;
            this.lblLogDate.Text = "<log date>";
            this.lblLogDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVerifyUser
            // 
            this.lblVerifyUser.BackColor = System.Drawing.Color.White;
            this.lblVerifyUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVerifyUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVerifyUser.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblVerifyUser.Location = new System.Drawing.Point(212, 453);
            this.lblVerifyUser.Name = "lblVerifyUser";
            this.lblVerifyUser.Size = new System.Drawing.Size(72, 17);
            this.lblVerifyUser.TabIndex = 87;
            this.lblVerifyUser.Text = "<verify>";
            this.lblVerifyUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVerifyDate
            // 
            this.lblVerifyDate.BackColor = System.Drawing.Color.White;
            this.lblVerifyDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVerifyDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVerifyDate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblVerifyDate.Location = new System.Drawing.Point(283, 453);
            this.lblVerifyDate.Name = "lblVerifyDate";
            this.lblVerifyDate.Size = new System.Drawing.Size(82, 17);
            this.lblVerifyDate.TabIndex = 88;
            this.lblVerifyDate.Text = "<verify date>";
            this.lblVerifyDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdLogs
            // 
            this.cmdLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLogs.Location = new System.Drawing.Point(708, 449);
            this.cmdLogs.Name = "cmdLogs";
            this.cmdLogs.Size = new System.Drawing.Size(34, 22);
            this.cmdLogs.TabIndex = 92;
            this.cmdLogs.Text = ">";
            this.cmdLogs.UseVisualStyleBackColor = true;
            // 
            // flpLogs
            // 
            this.flpLogs.Location = new System.Drawing.Point(894, 21);
            this.flpLogs.Name = "flpLogs";
            this.flpLogs.Size = new System.Drawing.Size(12, 38);
            this.flpLogs.TabIndex = 93;
            // 
            // cmdNew
            // 
            this.cmdNew.Location = new System.Drawing.Point(464, 449);
            this.cmdNew.Name = "cmdNew";
            this.cmdNew.Size = new System.Drawing.Size(75, 22);
            this.cmdNew.TabIndex = 99;
            this.cmdNew.Text = "New";
            this.cmdNew.UseVisualStyleBackColor = true;
            this.cmdNew.Click += new System.EventHandler(this.cmdNew_Click);
            // 
            // dgvTB
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTB.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvTB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTB.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvTB.Location = new System.Drawing.Point(894, 64);
            this.dgvTB.Name = "dgvTB";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTB.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvTB.Size = new System.Drawing.Size(12, 108);
            this.dgvTB.TabIndex = 100;
            // 
            // cmdGrid
            // 
            this.cmdGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGrid.Location = new System.Drawing.Point(708, 449);
            this.cmdGrid.Name = "cmdGrid";
            this.cmdGrid.Size = new System.Drawing.Size(34, 22);
            this.cmdGrid.TabIndex = 101;
            this.cmdGrid.Text = "V";
            this.cmdGrid.UseVisualStyleBackColor = true;
            this.cmdGrid.Click += new System.EventHandler(this.cmdGrid_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cmdAdd);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.tbxV1A);
            this.panel1.Controls.Add(this.cmdGrid);
            this.panel1.Controls.Add(this.dgvTB);
            this.panel1.Controls.Add(this.cmdNew);
            this.panel1.Controls.Add(this.flpLogs);
            this.panel1.Controls.Add(this.cmdLogs);
            this.panel1.Controls.Add(this.lblVerifyDate);
            this.panel1.Controls.Add(this.lblVerifyUser);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.lblLogDate);
            this.panel1.Controls.Add(this.nudShift);
            this.panel1.Controls.Add(this.lblLogUser);
            this.panel1.Controls.Add(this.cbxVerifyDate);
            this.panel1.Controls.Add(this.lblAudit);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.gbxCrew);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.cmdCancel);
            this.panel1.Controls.Add(this.cmdOK);
            this.panel1.Location = new System.Drawing.Point(103, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(930, 479);
            this.panel1.TabIndex = 37;
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(464, 449);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(75, 22);
            this.cmdAdd.TabIndex = 102;
            this.cmdAdd.Text = "Add";
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(856, 75);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 13);
            this.label15.TabIndex = 106;
            this.label15.Text = "Hours";
            // 
            // tbxV1A
            // 
            this.tbxV1A.BackColor = System.Drawing.Color.PowderBlue;
            this.tbxV1A.Location = new System.Drawing.Point(857, 88);
            this.tbxV1A.Name = "tbxV1A";
            this.tbxV1A.ReadOnly = true;
            this.tbxV1A.Size = new System.Drawing.Size(34, 20);
            this.tbxV1A.TabIndex = 105;
            // 
            // cmdDel
            // 
            this.cmdDel.Location = new System.Drawing.Point(58, 460);
            this.cmdDel.Name = "cmdDel";
            this.cmdDel.Size = new System.Drawing.Size(33, 22);
            this.cmdDel.TabIndex = 40;
            this.cmdDel.Text = "-";
            this.cmdDel.UseVisualStyleBackColor = true;
            this.cmdDel.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 460);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(33, 22);
            this.button1.TabIndex = 39;
            this.button1.Text = "+";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // letLog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(1070, 496);
            this.Controls.Add(this.cmdDel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.flpVessel);
            this.Controls.Add(this.panel1);
            this.Name = "letLog";
            this.Text = "      ";
            this.Load += new System.EventHandler(this.frmLog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudShift)).EndInit();
            this.gbxCrew.ResumeLayout(false);
            this.tabShift.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTB)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpLogDate;
        private System.Windows.Forms.FlowLayoutPanel flpVessel;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdGrid;
        private System.Windows.Forms.DataGridView dgvTB;
        private System.Windows.Forms.Button cmdNew;
        private System.Windows.Forms.FlowLayoutPanel flpLogs;
        private System.Windows.Forms.Button cmdLogs;
        private System.Windows.Forms.Label lblVerifyDate;
        private System.Windows.Forms.Label lblVerifyUser;
        private System.Windows.Forms.Label lblLogDate;
        private System.Windows.Forms.Label lblLogUser;
        private System.Windows.Forms.ComboBox cbxVerifyDate;
        private System.Windows.Forms.Label lblAudit;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbxCrewCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxOverHours;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxPaidHours;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxOnVessel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxCrewHours;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbxCrew;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblShift;
        private System.Windows.Forms.Label lblFuel;
        private System.Windows.Forms.Label lblHourFinish;
        private System.Windows.Forms.Label lblHourStart;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.TabControl tabShift;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button cmdSheet;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.ComboBox cbxShipShift;
        private System.Windows.Forms.ComboBox cbxShips;
        private System.Windows.Forms.NumericUpDown nudShift;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tbxV1A;
        private System.Windows.Forms.Button cmdLeft;
        private System.Windows.Forms.Button cmdRight;
        private System.Windows.Forms.Button cmdDel;
        private System.Windows.Forms.Button button1;
    }
}