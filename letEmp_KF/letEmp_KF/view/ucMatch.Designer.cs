namespace letEmp_KF
{
    partial class ucMatch
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
            this.pnlUI = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkArchive = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tbxEmpId = new System.Windows.Forms.TextBox();
            this.tbxLast = new System.Windows.Forms.TextBox();
            this.tbxFirst = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxTab = new System.Windows.Forms.TextBox();
            this.lblID = new System.Windows.Forms.Label();
            this.pnlUI.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlUI
            // 
            this.pnlUI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlUI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlUI.Controls.Add(this.groupBox1);
            this.pnlUI.Location = new System.Drawing.Point(1, 27);
            this.pnlUI.Name = "pnlUI";
            this.pnlUI.Size = new System.Drawing.Size(297, 135);
            this.pnlUI.TabIndex = 47;
            this.pnlUI.Click += new System.EventHandler(this.tbxTab_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.lblID);
            this.groupBox1.Controls.Add(this.chkArchive);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.tbxEmpId);
            this.groupBox1.Controls.Add(this.tbxLast);
            this.groupBox1.Controls.Add(this.tbxFirst);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(8, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(276, 117);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Timebook";
            this.groupBox1.Click += new System.EventHandler(this.groupBox1_Click);
            // 
            // chkArchive
            // 
            this.chkArchive.AutoCheck = false;
            this.chkArchive.AutoSize = true;
            this.chkArchive.Location = new System.Drawing.Point(200, 81);
            this.chkArchive.Name = "chkArchive";
            this.chkArchive.Size = new System.Drawing.Size(62, 17);
            this.chkArchive.TabIndex = 65;
            this.chkArchive.Text = "Archive";
            this.chkArchive.UseVisualStyleBackColor = true;
            this.chkArchive.Click += new System.EventHandler(this.tbxTab_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 60;
            this.label3.Text = "Last Name :";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(39, 81);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 13);
            this.label14.TabIndex = 59;
            this.label14.Text = "Old ID :";
            // 
            // tbxEmpId
            // 
            this.tbxEmpId.BackColor = System.Drawing.Color.White;
            this.tbxEmpId.Location = new System.Drawing.Point(83, 78);
            this.tbxEmpId.Name = "tbxEmpId";
            this.tbxEmpId.ReadOnly = true;
            this.tbxEmpId.Size = new System.Drawing.Size(76, 20);
            this.tbxEmpId.TabIndex = 2;
            this.tbxEmpId.Click += new System.EventHandler(this.tbxTab_Click);
            // 
            // tbxLast
            // 
            this.tbxLast.BackColor = System.Drawing.Color.White;
            this.tbxLast.Location = new System.Drawing.Point(83, 26);
            this.tbxLast.Name = "tbxLast";
            this.tbxLast.ReadOnly = true;
            this.tbxLast.Size = new System.Drawing.Size(179, 20);
            this.tbxLast.TabIndex = 1;
            this.tbxLast.Click += new System.EventHandler(this.tbxTab_Click);
            // 
            // tbxFirst
            // 
            this.tbxFirst.BackColor = System.Drawing.Color.White;
            this.tbxFirst.Location = new System.Drawing.Point(83, 52);
            this.tbxFirst.Name = "tbxFirst";
            this.tbxFirst.ReadOnly = true;
            this.tbxFirst.Size = new System.Drawing.Size(179, 20);
            this.tbxFirst.TabIndex = 0;
            this.tbxFirst.Click += new System.EventHandler(this.tbxTab_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "First Name :";
            // 
            // tbxTab
            // 
            this.tbxTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tbxTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTab.Location = new System.Drawing.Point(1, 2);
            this.tbxTab.Name = "tbxTab";
            this.tbxTab.ReadOnly = true;
            this.tbxTab.Size = new System.Drawing.Size(112, 26);
            this.tbxTab.TabIndex = 50;
            this.tbxTab.TabStop = false;
            this.tbxTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbxTab.Click += new System.EventHandler(this.tbxTab_Click);
            // 
            // lblID
            // 
            this.lblID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblID.Location = new System.Drawing.Point(210, 0);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(60, 15);
            this.lblID.TabIndex = 56;
            this.lblID.Text = "0";
            this.lblID.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ucMatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbxTab);
            this.Controls.Add(this.pnlUI);
            this.Margin = new System.Windows.Forms.Padding(10);
            this.Name = "ucMatch";
            this.Size = new System.Drawing.Size(301, 167);
            this.pnlUI.ResumeLayout(false);
            this.pnlUI.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlUI;
        private System.Windows.Forms.CheckBox chkArchive;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbxEmpId;
        private System.Windows.Forms.TextBox tbxLast;
        private System.Windows.Forms.TextBox tbxFirst;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxTab;
        public System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblID;


    }
}
