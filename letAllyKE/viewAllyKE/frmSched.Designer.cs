namespace viewAllyKE
{
    partial class frmSched
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
            this.dtpLogDate = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblVerifyDate = new System.Windows.Forms.Label();
            this.lblVerifyUser = new System.Windows.Forms.Label();
            this.lblLogDate = new System.Windows.Forms.Label();
            this.lblLogUser = new System.Windows.Forms.Label();
            this.cbxVerifyDate = new System.Windows.Forms.ComboBox();
            this.lblAudit = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cbxVessel = new System.Windows.Forms.ComboBox();
            this.tbxBoatH = new System.Windows.Forms.TextBox();
            this.tbxShiftH = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tlpCrew = new System.Windows.Forms.TableLayoutPanel();
            this.lblLine = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.cbxLegend = new System.Windows.Forms.ComboBox();
            this.tbxSun2 = new System.Windows.Forms.TextBox();
            this.tbxSat2 = new System.Windows.Forms.TextBox();
            this.tbxFri2 = new System.Windows.Forms.TextBox();
            this.tbxThu2 = new System.Windows.Forms.TextBox();
            this.tbxWed2 = new System.Windows.Forms.TextBox();
            this.tbxTue2 = new System.Windows.Forms.TextBox();
            this.tbxMon2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblWeek2 = new System.Windows.Forms.Label();
            this.tbxSun = new System.Windows.Forms.TextBox();
            this.tbxSat = new System.Windows.Forms.TextBox();
            this.tbxFri = new System.Windows.Forms.TextBox();
            this.tbxThu = new System.Windows.Forms.TextBox();
            this.tbxWed = new System.Windows.Forms.TextBox();
            this.tbxTue = new System.Windows.Forms.TextBox();
            this.tbxMon = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblWeek = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.chkShow = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpLogDate
            // 
            this.dtpLogDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpLogDate.Location = new System.Drawing.Point(8, 12);
            this.dtpLogDate.Name = "dtpLogDate";
            this.dtpLogDate.Size = new System.Drawing.Size(220, 20);
            this.dtpLogDate.TabIndex = 0;
            this.dtpLogDate.CloseUp += new System.EventHandler(this.dtpLogDate_CloseUp);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblVerifyDate);
            this.panel1.Controls.Add(this.lblVerifyUser);
            this.panel1.Controls.Add(this.lblLogDate);
            this.panel1.Controls.Add(this.lblLogUser);
            this.panel1.Controls.Add(this.cbxVerifyDate);
            this.panel1.Controls.Add(this.lblAudit);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.cmdCancel);
            this.panel1.Controls.Add(this.cmdSave);
            this.panel1.Location = new System.Drawing.Point(8, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(945, 560);
            this.panel1.TabIndex = 37;
            // 
            // lblVerifyDate
            // 
            this.lblVerifyDate.BackColor = System.Drawing.Color.White;
            this.lblVerifyDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVerifyDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVerifyDate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblVerifyDate.Location = new System.Drawing.Point(287, 526);
            this.lblVerifyDate.Name = "lblVerifyDate";
            this.lblVerifyDate.Size = new System.Drawing.Size(82, 20);
            this.lblVerifyDate.TabIndex = 88;
            this.lblVerifyDate.Text = "<verify date>";
            this.lblVerifyDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblVerifyDate.Visible = false;
            // 
            // lblVerifyUser
            // 
            this.lblVerifyUser.BackColor = System.Drawing.Color.White;
            this.lblVerifyUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVerifyUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVerifyUser.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblVerifyUser.Location = new System.Drawing.Point(216, 526);
            this.lblVerifyUser.Name = "lblVerifyUser";
            this.lblVerifyUser.Size = new System.Drawing.Size(72, 20);
            this.lblVerifyUser.TabIndex = 87;
            this.lblVerifyUser.Text = "<verify>";
            this.lblVerifyUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblVerifyUser.Visible = false;
            // 
            // lblLogDate
            // 
            this.lblLogDate.BackColor = System.Drawing.Color.White;
            this.lblLogDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLogDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogDate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblLogDate.Location = new System.Drawing.Point(135, 526);
            this.lblLogDate.Name = "lblLogDate";
            this.lblLogDate.Size = new System.Drawing.Size(82, 20);
            this.lblLogDate.TabIndex = 86;
            this.lblLogDate.Text = "<log date>";
            this.lblLogDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLogDate.Visible = false;
            // 
            // lblLogUser
            // 
            this.lblLogUser.BackColor = System.Drawing.Color.White;
            this.lblLogUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLogUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogUser.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblLogUser.Location = new System.Drawing.Point(64, 526);
            this.lblLogUser.Name = "lblLogUser";
            this.lblLogUser.Size = new System.Drawing.Size(72, 20);
            this.lblLogUser.TabIndex = 85;
            this.lblLogUser.Text = "<log user>";
            this.lblLogUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLogUser.Visible = false;
            // 
            // cbxVerifyDate
            // 
            this.cbxVerifyDate.DropDownWidth = 200;
            this.cbxVerifyDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxVerifyDate.FormattingEnabled = true;
            this.cbxVerifyDate.Location = new System.Drawing.Point(368, 526);
            this.cbxVerifyDate.Name = "cbxVerifyDate";
            this.cbxVerifyDate.Size = new System.Drawing.Size(36, 20);
            this.cbxVerifyDate.TabIndex = 83;
            this.cbxVerifyDate.Visible = false;
            // 
            // lblAudit
            // 
            this.lblAudit.BackColor = System.Drawing.Color.Black;
            this.lblAudit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAudit.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAudit.ForeColor = System.Drawing.Color.White;
            this.lblAudit.Location = new System.Drawing.Point(16, 526);
            this.lblAudit.Name = "lblAudit";
            this.lblAudit.Size = new System.Drawing.Size(50, 20);
            this.lblAudit.TabIndex = 78;
            this.lblAudit.Text = "Audit";
            this.lblAudit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAudit.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.cbxVessel);
            this.groupBox2.Controls.Add(this.tbxBoatH);
            this.groupBox2.Controls.Add(this.tbxShiftH);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.tlpCrew);
            this.groupBox2.Controls.Add(this.lblLine);
            this.groupBox2.Controls.Add(this.lblID);
            this.groupBox2.Controls.Add(this.cbxLegend);
            this.groupBox2.Controls.Add(this.tbxSun2);
            this.groupBox2.Controls.Add(this.tbxSat2);
            this.groupBox2.Controls.Add(this.tbxFri2);
            this.groupBox2.Controls.Add(this.tbxThu2);
            this.groupBox2.Controls.Add(this.tbxWed2);
            this.groupBox2.Controls.Add(this.tbxTue2);
            this.groupBox2.Controls.Add(this.tbxMon2);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.lblWeek2);
            this.groupBox2.Controls.Add(this.tbxSun);
            this.groupBox2.Controls.Add(this.tbxSat);
            this.groupBox2.Controls.Add(this.tbxFri);
            this.groupBox2.Controls.Add(this.tbxThu);
            this.groupBox2.Controls.Add(this.tbxWed);
            this.groupBox2.Controls.Add(this.tbxTue);
            this.groupBox2.Controls.Add(this.tbxMon);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lblWeek);
            this.groupBox2.Location = new System.Drawing.Point(12, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(920, 506);
            this.groupBox2.TabIndex = 76;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Crew :";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(13, 25);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(44, 13);
            this.label15.TabIndex = 133;
            this.label15.Text = "Vessel :";
            // 
            // cbxVessel
            // 
            this.cbxVessel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxVessel.DropDownWidth = 300;
            this.cbxVessel.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxVessel.FormattingEnabled = true;
            this.cbxVessel.Location = new System.Drawing.Point(63, 22);
            this.cbxVessel.Name = "cbxVessel";
            this.cbxVessel.Size = new System.Drawing.Size(145, 22);
            this.cbxVessel.TabIndex = 132;
            this.cbxVessel.SelectedIndexChanged += new System.EventHandler(this.cbxVessel_SelectedIndexChanged);
            // 
            // tbxBoatH
            // 
            this.tbxBoatH.Location = new System.Drawing.Point(800, 22);
            this.tbxBoatH.Name = "tbxBoatH";
            this.tbxBoatH.Size = new System.Drawing.Size(58, 20);
            this.tbxBoatH.TabIndex = 131;
            this.tbxBoatH.Visible = false;
            this.tbxBoatH.TextChanged += new System.EventHandler(this.tbxBoat_TextChanged);
            // 
            // tbxShiftH
            // 
            this.tbxShiftH.Location = new System.Drawing.Point(800, 56);
            this.tbxShiftH.Name = "tbxShiftH";
            this.tbxShiftH.Size = new System.Drawing.Size(58, 20);
            this.tbxShiftH.TabIndex = 130;
            this.tbxShiftH.Visible = false;
            this.tbxShiftH.TextChanged += new System.EventHandler(this.tbxShift_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(103, 63);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(49, 13);
            this.label17.TabIndex = 129;
            this.label17.Text = "Legend :";
            // 
            // tlpCrew
            // 
            this.tlpCrew.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tlpCrew.AutoScroll = true;
            this.tlpCrew.ColumnCount = 1;
            this.tlpCrew.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 910F));
            this.tlpCrew.Location = new System.Drawing.Point(4, 91);
            this.tlpCrew.Name = "tlpCrew";
            this.tlpCrew.RowCount = 1;
            this.tlpCrew.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpCrew.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 409F));
            this.tlpCrew.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 409F));
            this.tlpCrew.Size = new System.Drawing.Size(910, 409);
            this.tlpCrew.TabIndex = 128;
            // 
            // lblLine
            // 
            this.lblLine.BackColor = System.Drawing.Color.Black;
            this.lblLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLine.Location = new System.Drawing.Point(6, 85);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(898, 2);
            this.lblLine.TabIndex = 125;
            // 
            // lblID
            // 
            this.lblID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblID.Location = new System.Drawing.Point(864, 0);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(40, 15);
            this.lblID.TabIndex = 76;
            this.lblID.Text = "0";
            this.lblID.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbxLegend
            // 
            this.cbxLegend.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLegend.DropDownWidth = 300;
            this.cbxLegend.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxLegend.FormattingEnabled = true;
            this.cbxLegend.Location = new System.Drawing.Point(158, 60);
            this.cbxLegend.Name = "cbxLegend";
            this.cbxLegend.Size = new System.Drawing.Size(50, 22);
            this.cbxLegend.TabIndex = 123;
            // 
            // tbxSun2
            // 
            this.tbxSun2.BackColor = System.Drawing.Color.Black;
            this.tbxSun2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxSun2.ForeColor = System.Drawing.Color.White;
            this.tbxSun2.Location = new System.Drawing.Point(749, 63);
            this.tbxSun2.Name = "tbxSun2";
            this.tbxSun2.Size = new System.Drawing.Size(34, 20);
            this.tbxSun2.TabIndex = 106;
            this.tbxSun2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbxSat2
            // 
            this.tbxSat2.BackColor = System.Drawing.Color.Black;
            this.tbxSat2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxSat2.ForeColor = System.Drawing.Color.White;
            this.tbxSat2.Location = new System.Drawing.Point(713, 63);
            this.tbxSat2.Name = "tbxSat2";
            this.tbxSat2.Size = new System.Drawing.Size(34, 20);
            this.tbxSat2.TabIndex = 105;
            this.tbxSat2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbxFri2
            // 
            this.tbxFri2.BackColor = System.Drawing.Color.Black;
            this.tbxFri2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxFri2.ForeColor = System.Drawing.Color.White;
            this.tbxFri2.Location = new System.Drawing.Point(677, 63);
            this.tbxFri2.Name = "tbxFri2";
            this.tbxFri2.Size = new System.Drawing.Size(34, 20);
            this.tbxFri2.TabIndex = 104;
            this.tbxFri2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbxThu2
            // 
            this.tbxThu2.BackColor = System.Drawing.Color.Black;
            this.tbxThu2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxThu2.ForeColor = System.Drawing.Color.White;
            this.tbxThu2.Location = new System.Drawing.Point(641, 63);
            this.tbxThu2.Name = "tbxThu2";
            this.tbxThu2.Size = new System.Drawing.Size(34, 20);
            this.tbxThu2.TabIndex = 103;
            this.tbxThu2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbxWed2
            // 
            this.tbxWed2.BackColor = System.Drawing.Color.Black;
            this.tbxWed2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxWed2.ForeColor = System.Drawing.Color.White;
            this.tbxWed2.Location = new System.Drawing.Point(605, 63);
            this.tbxWed2.Name = "tbxWed2";
            this.tbxWed2.Size = new System.Drawing.Size(34, 20);
            this.tbxWed2.TabIndex = 102;
            this.tbxWed2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbxTue2
            // 
            this.tbxTue2.BackColor = System.Drawing.Color.Black;
            this.tbxTue2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTue2.ForeColor = System.Drawing.Color.White;
            this.tbxTue2.Location = new System.Drawing.Point(569, 63);
            this.tbxTue2.Name = "tbxTue2";
            this.tbxTue2.Size = new System.Drawing.Size(34, 20);
            this.tbxTue2.TabIndex = 101;
            this.tbxTue2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbxMon2
            // 
            this.tbxMon2.BackColor = System.Drawing.Color.Black;
            this.tbxMon2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxMon2.ForeColor = System.Drawing.Color.White;
            this.tbxMon2.Location = new System.Drawing.Point(533, 63);
            this.tbxMon2.Name = "tbxMon2";
            this.tbxMon2.Size = new System.Drawing.Size(34, 20);
            this.tbxMon2.TabIndex = 100;
            this.tbxMon2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(756, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 13);
            this.label8.TabIndex = 99;
            this.label8.Text = "Su";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(720, 44);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(20, 13);
            this.label9.TabIndex = 98;
            this.label9.Text = "Sa";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(684, 44);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(13, 13);
            this.label10.TabIndex = 97;
            this.label10.Text = "F";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(648, 44);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(20, 13);
            this.label11.TabIndex = 96;
            this.label11.Text = "Th";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(612, 44);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(18, 13);
            this.label12.TabIndex = 95;
            this.label12.Text = "W";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(576, 44);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(20, 13);
            this.label13.TabIndex = 94;
            this.label13.Text = "Tu";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(540, 44);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(16, 13);
            this.label14.TabIndex = 93;
            this.label14.Text = "M";
            // 
            // lblWeek2
            // 
            this.lblWeek2.AutoSize = true;
            this.lblWeek2.Location = new System.Drawing.Point(530, 22);
            this.lblWeek2.Name = "lblWeek2";
            this.lblWeek2.Size = new System.Drawing.Size(71, 13);
            this.lblWeek2.TabIndex = 92;
            this.lblWeek2.Text = "January 2014";
            // 
            // tbxSun
            // 
            this.tbxSun.BackColor = System.Drawing.Color.Black;
            this.tbxSun.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxSun.ForeColor = System.Drawing.Color.White;
            this.tbxSun.Location = new System.Drawing.Point(431, 63);
            this.tbxSun.Name = "tbxSun";
            this.tbxSun.Size = new System.Drawing.Size(34, 20);
            this.tbxSun.TabIndex = 91;
            this.tbxSun.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbxSat
            // 
            this.tbxSat.BackColor = System.Drawing.Color.Black;
            this.tbxSat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxSat.ForeColor = System.Drawing.Color.White;
            this.tbxSat.Location = new System.Drawing.Point(395, 63);
            this.tbxSat.Name = "tbxSat";
            this.tbxSat.Size = new System.Drawing.Size(34, 20);
            this.tbxSat.TabIndex = 90;
            this.tbxSat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbxFri
            // 
            this.tbxFri.BackColor = System.Drawing.Color.Black;
            this.tbxFri.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxFri.ForeColor = System.Drawing.Color.White;
            this.tbxFri.Location = new System.Drawing.Point(359, 63);
            this.tbxFri.Name = "tbxFri";
            this.tbxFri.Size = new System.Drawing.Size(34, 20);
            this.tbxFri.TabIndex = 89;
            this.tbxFri.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbxThu
            // 
            this.tbxThu.BackColor = System.Drawing.Color.Black;
            this.tbxThu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxThu.ForeColor = System.Drawing.Color.White;
            this.tbxThu.Location = new System.Drawing.Point(323, 63);
            this.tbxThu.Name = "tbxThu";
            this.tbxThu.Size = new System.Drawing.Size(34, 20);
            this.tbxThu.TabIndex = 88;
            this.tbxThu.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbxWed
            // 
            this.tbxWed.BackColor = System.Drawing.Color.Black;
            this.tbxWed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxWed.ForeColor = System.Drawing.Color.White;
            this.tbxWed.Location = new System.Drawing.Point(287, 63);
            this.tbxWed.Name = "tbxWed";
            this.tbxWed.Size = new System.Drawing.Size(34, 20);
            this.tbxWed.TabIndex = 87;
            this.tbxWed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbxTue
            // 
            this.tbxTue.BackColor = System.Drawing.Color.Black;
            this.tbxTue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTue.ForeColor = System.Drawing.Color.White;
            this.tbxTue.Location = new System.Drawing.Point(251, 63);
            this.tbxTue.Name = "tbxTue";
            this.tbxTue.Size = new System.Drawing.Size(34, 20);
            this.tbxTue.TabIndex = 86;
            this.tbxTue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbxMon
            // 
            this.tbxMon.BackColor = System.Drawing.Color.Black;
            this.tbxMon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxMon.ForeColor = System.Drawing.Color.White;
            this.tbxMon.Location = new System.Drawing.Point(215, 63);
            this.tbxMon.Name = "tbxMon";
            this.tbxMon.Size = new System.Drawing.Size(34, 20);
            this.tbxMon.TabIndex = 85;
            this.tbxMon.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(438, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 13);
            this.label7.TabIndex = 84;
            this.label7.Text = "Su";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(402, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 83;
            this.label6.Text = "Sa";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(366, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 82;
            this.label5.Text = "F";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(330, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 81;
            this.label4.Text = "Th";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(294, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 80;
            this.label3.Text = "W";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(258, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 79;
            this.label2.Text = "Tu";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(222, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 78;
            this.label1.Text = "M";
            // 
            // lblWeek
            // 
            this.lblWeek.AutoSize = true;
            this.lblWeek.Location = new System.Drawing.Point(212, 22);
            this.lblWeek.Name = "lblWeek";
            this.lblWeek.Size = new System.Drawing.Size(71, 13);
            this.lblWeek.TabIndex = 77;
            this.lblWeek.Text = "January 2014";
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(857, 525);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 26;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Location = new System.Drawing.Point(776, 526);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 25;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // chkShow
            // 
            this.chkShow.AutoSize = true;
            this.chkShow.Location = new System.Drawing.Point(844, 11);
            this.chkShow.Name = "chkShow";
            this.chkShow.Size = new System.Drawing.Size(67, 17);
            this.chkShow.TabIndex = 38;
            this.chkShow.Text = "Show All";
            this.chkShow.UseVisualStyleBackColor = true;
            this.chkShow.CheckedChanged += new System.EventHandler(this.chkShow_CheckedChanged);
            // 
            // frmSched
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(960, 602);
            this.Controls.Add(this.chkShow);
            this.Controls.Add(this.dtpLogDate);
            this.Controls.Add(this.panel1);
            this.Name = "frmSched";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Schedule for Weeks";
            this.Load += new System.EventHandler(this.frmSched_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpLogDate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Label lblAudit;
        private System.Windows.Forms.Label lblVerifyUser;
        private System.Windows.Forms.Label lblLogDate;
        private System.Windows.Forms.Label lblLogUser;
        private System.Windows.Forms.ComboBox cbxVerifyDate;
        private System.Windows.Forms.Label lblVerifyDate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbxSun2;
        private System.Windows.Forms.TextBox tbxSat2;
        private System.Windows.Forms.TextBox tbxFri2;
        private System.Windows.Forms.TextBox tbxThu2;
        private System.Windows.Forms.TextBox tbxWed2;
        private System.Windows.Forms.TextBox tbxTue2;
        private System.Windows.Forms.TextBox tbxMon2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblWeek2;
        private System.Windows.Forms.TextBox tbxSun;
        private System.Windows.Forms.TextBox tbxSat;
        private System.Windows.Forms.TextBox tbxFri;
        private System.Windows.Forms.TextBox tbxThu;
        private System.Windows.Forms.TextBox tbxWed;
        private System.Windows.Forms.TextBox tbxTue;
        private System.Windows.Forms.TextBox tbxMon;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblWeek;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblLine;
        private System.Windows.Forms.ComboBox cbxLegend;
        private System.Windows.Forms.TableLayoutPanel tlpCrew;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tbxShiftH;
        private System.Windows.Forms.TextBox tbxBoatH;
        private System.Windows.Forms.CheckBox chkShow;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cbxVessel;
    }
}