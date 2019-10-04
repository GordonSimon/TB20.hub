namespace viewAllyKE
{
    partial class ucAlley
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
            this.components = new System.ComponentModel.Container();
            this.flpN = new System.Windows.Forms.FlowLayoutPanel();
            this.tlpAlley = new System.Windows.Forms.TableLayoutPanel();
            this.cmdLeft = new System.Windows.Forms.Button();
            this.cmdRight = new System.Windows.Forms.Button();
            this.txtToday = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cmdSched = new System.Windows.Forms.Button();
            this.cmdLogs = new System.Windows.Forms.Button();
            this.cmdTimeOff = new System.Windows.Forms.Button();
            this.cmdBoats = new System.Windows.Forms.Button();
            this.cmdRpt = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.flpGang = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlOnline = new System.Windows.Forms.Panel();
            this.tlpHead = new System.Windows.Forms.TableLayoutPanel();
            this.flpView = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // flpN
            // 
            this.flpN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.flpN.AutoSize = true;
            this.flpN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpN.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpN.Location = new System.Drawing.Point(4, 395);
            this.flpN.Margin = new System.Windows.Forms.Padding(5, 3, 0, 3);
            this.flpN.Name = "flpN";
            this.flpN.Size = new System.Drawing.Size(140, 230);
            this.flpN.TabIndex = 0;
            // 
            // tlpAlley
            // 
            this.tlpAlley.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpAlley.AutoScroll = true;
            this.tlpAlley.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tlpAlley.ColumnCount = 1;
            this.tlpAlley.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlley.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpAlley.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpAlley.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpAlley.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpAlley.Location = new System.Drawing.Point(152, 117);
            this.tlpAlley.Name = "tlpAlley";
            this.tlpAlley.RowCount = 1;
            this.tlpAlley.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 704F));
            this.tlpAlley.Size = new System.Drawing.Size(1043, 508);
            this.tlpAlley.TabIndex = 5;
            this.tlpAlley.Click += new System.EventHandler(this.tlpAlley_Click);
            // 
            // cmdLeft
            // 
            this.cmdLeft.Location = new System.Drawing.Point(396, 6);
            this.cmdLeft.Name = "cmdLeft";
            this.cmdLeft.Size = new System.Drawing.Size(60, 23);
            this.cmdLeft.TabIndex = 6;
            this.cmdLeft.Text = "<<";
            this.cmdLeft.UseVisualStyleBackColor = true;
            this.cmdLeft.Click += new System.EventHandler(this.cmdLeft_Click);
            // 
            // cmdRight
            // 
            this.cmdRight.Location = new System.Drawing.Point(865, 6);
            this.cmdRight.Name = "cmdRight";
            this.cmdRight.Size = new System.Drawing.Size(60, 23);
            this.cmdRight.TabIndex = 7;
            this.cmdRight.Text = ">>";
            this.cmdRight.UseVisualStyleBackColor = true;
            this.cmdRight.Click += new System.EventHandler(this.cmdRight_Click);
            // 
            // txtToday
            // 
            this.txtToday.BackColor = System.Drawing.Color.PaleTurquoise;
            this.txtToday.Location = new System.Drawing.Point(465, 8);
            this.txtToday.Name = "txtToday";
            this.txtToday.ReadOnly = true;
            this.txtToday.Size = new System.Drawing.Size(393, 20);
            this.txtToday.TabIndex = 8;
            this.txtToday.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtToday.DoubleClick += new System.EventHandler(this.txtToday_DoubleClick);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cmdSched
            // 
            this.cmdSched.Location = new System.Drawing.Point(315, 6);
            this.cmdSched.Name = "cmdSched";
            this.cmdSched.Size = new System.Drawing.Size(75, 23);
            this.cmdSched.TabIndex = 11;
            this.cmdSched.Text = "Sched";
            this.cmdSched.UseVisualStyleBackColor = true;
            this.cmdSched.Click += new System.EventHandler(this.cmdSched_Click);
            // 
            // cmdLogs
            // 
            this.cmdLogs.Location = new System.Drawing.Point(153, 6);
            this.cmdLogs.Name = "cmdLogs";
            this.cmdLogs.Size = new System.Drawing.Size(75, 23);
            this.cmdLogs.TabIndex = 12;
            this.cmdLogs.Text = "Logs";
            this.cmdLogs.UseVisualStyleBackColor = true;
            this.cmdLogs.Click += new System.EventHandler(this.cmdLogs_Click);
            // 
            // cmdTimeOff
            // 
            this.cmdTimeOff.Location = new System.Drawing.Point(940, 6);
            this.cmdTimeOff.Name = "cmdTimeOff";
            this.cmdTimeOff.Size = new System.Drawing.Size(75, 23);
            this.cmdTimeOff.TabIndex = 13;
            this.cmdTimeOff.Text = "Salary";
            this.cmdTimeOff.UseVisualStyleBackColor = true;
            this.cmdTimeOff.Click += new System.EventHandler(this.cmdTimeOff_Click);
            // 
            // cmdBoats
            // 
            this.cmdBoats.Location = new System.Drawing.Point(234, 6);
            this.cmdBoats.Name = "cmdBoats";
            this.cmdBoats.Size = new System.Drawing.Size(75, 23);
            this.cmdBoats.TabIndex = 14;
            this.cmdBoats.Text = "Hours";
            this.cmdBoats.UseVisualStyleBackColor = true;
            this.cmdBoats.Click += new System.EventHandler(this.cmdBoats_Click);
            // 
            // cmdRpt
            // 
            this.cmdRpt.Location = new System.Drawing.Point(1020, 6);
            this.cmdRpt.Name = "cmdRpt";
            this.cmdRpt.Size = new System.Drawing.Size(105, 23);
            this.cmdRpt.TabIndex = 15;
            this.cmdRpt.Text = "Report";
            this.cmdRpt.UseVisualStyleBackColor = true;
            this.cmdRpt.Click += new System.EventHandler(this.cmdRpt_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 16;
            this.label1.Text = "Gang";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(1158, 11);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(49, 15);
            this.lblVersion.TabIndex = 17;
            this.lblVersion.Text = "V1.18.X";
            // 
            // flpGang
            // 
            this.flpGang.AutoSize = true;
            this.flpGang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpGang.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpGang.Location = new System.Drawing.Point(4, 8);
            this.flpGang.Name = "flpGang";
            this.flpGang.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.flpGang.Size = new System.Drawing.Size(140, 134);
            this.flpGang.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 387);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 15);
            this.label3.TabIndex = 19;
            this.label3.Text = "Gang Members";
            // 
            // pnlOnline
            // 
            this.pnlOnline.BackColor = System.Drawing.Color.Red;
            this.pnlOnline.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOnline.Location = new System.Drawing.Point(1136, 8);
            this.pnlOnline.Name = "pnlOnline";
            this.pnlOnline.Size = new System.Drawing.Size(16, 18);
            this.pnlOnline.TabIndex = 20;
            // 
            // tlpHead
            // 
            this.tlpHead.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpHead.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tlpHead.ColumnCount = 1;
            this.tlpHead.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHead.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpHead.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpHead.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpHead.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpHead.Location = new System.Drawing.Point(152, 34);
            this.tlpHead.Name = "tlpHead";
            this.tlpHead.RowCount = 1;
            this.tlpHead.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 704F));
            this.tlpHead.Size = new System.Drawing.Size(1043, 83);
            this.tlpHead.TabIndex = 21;
            // 
            // flpView
            // 
            this.flpView.AutoSize = true;
            this.flpView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpView.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpView.Location = new System.Drawing.Point(4, 157);
            this.flpView.Name = "flpView";
            this.flpView.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.flpView.Size = new System.Drawing.Size(140, 221);
            this.flpView.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 15);
            this.label4.TabIndex = 17;
            this.label4.Text = "Gang View";
            // 
            // ucAlley
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.flpView);
            this.Controls.Add(this.tlpAlley);
            this.Controls.Add(this.tlpHead);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pnlOnline);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flpGang);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.cmdRpt);
            this.Controls.Add(this.cmdBoats);
            this.Controls.Add(this.cmdTimeOff);
            this.Controls.Add(this.cmdLogs);
            this.Controls.Add(this.cmdSched);
            this.Controls.Add(this.txtToday);
            this.Controls.Add(this.cmdRight);
            this.Controls.Add(this.cmdLeft);
            this.Controls.Add(this.flpN);
            this.DoubleBuffered = true;
            this.Name = "ucAlley";
            this.Size = new System.Drawing.Size(1200, 660);
            this.Load += new System.EventHandler(this.ucAlley_Load);
            this.Click += new System.EventHandler(this.ucAlley_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpN;
        private System.Windows.Forms.TableLayoutPanel tlpAlley;
        private System.Windows.Forms.Button cmdLeft;
        private System.Windows.Forms.Button cmdRight;
        private System.Windows.Forms.TextBox txtToday;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button cmdSched;
        private System.Windows.Forms.Button cmdLogs;
        private System.Windows.Forms.Button cmdTimeOff;
        private System.Windows.Forms.Button cmdBoats;
        private System.Windows.Forms.Button cmdRpt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.FlowLayoutPanel flpGang;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlOnline;
        private System.Windows.Forms.TableLayoutPanel tlpHead;
        private System.Windows.Forms.FlowLayoutPanel flpView;
        private System.Windows.Forms.Label label4;
    }
}
