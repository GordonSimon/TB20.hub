namespace letStaff
{
    partial class ucPaycodes
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
            this.dtpDay = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.lblEdit = new System.Windows.Forms.Label();
            this.lblClose = new System.Windows.Forms.Label();
            this.tbxEmpId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvPaycodes = new System.Windows.Forms.DataGridView();
            this.cmdSave = new System.Windows.Forms.Button();
            this.lblSql = new System.Windows.Forms.Label();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.chkHourly = new System.Windows.Forms.CheckBox();
            this.tbxDuty = new System.Windows.Forms.TextBox();
            this.tbxOT1 = new System.Windows.Forms.TextBox();
            this.tbxSalary = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxOT2 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbxHourly = new System.Windows.Forms.TextBox();
            this.cmdDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaycodes)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpDay
            // 
            this.dtpDay.Location = new System.Drawing.Point(87, 14);
            this.dtpDay.Name = "dtpDay";
            this.dtpDay.Size = new System.Drawing.Size(161, 20);
            this.dtpDay.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Date :";
            // 
            // lblEdit
            // 
            this.lblEdit.AutoSize = true;
            this.lblEdit.Location = new System.Drawing.Point(742, 16);
            this.lblEdit.Name = "lblEdit";
            this.lblEdit.Size = new System.Drawing.Size(14, 13);
            this.lblEdit.TabIndex = 25;
            this.lblEdit.Text = "E";
            this.lblEdit.Visible = false;
            this.lblEdit.Click += new System.EventHandler(this.lblEdit_Click);
            // 
            // lblClose
            // 
            this.lblClose.AutoSize = true;
            this.lblClose.Location = new System.Drawing.Point(762, 16);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(14, 13);
            this.lblClose.TabIndex = 26;
            this.lblClose.Text = "X";
            this.lblClose.Click += new System.EventHandler(this.lblClose_Click);
            // 
            // tbxEmpId
            // 
            this.tbxEmpId.Enabled = false;
            this.tbxEmpId.Location = new System.Drawing.Point(87, 39);
            this.tbxEmpId.Name = "tbxEmpId";
            this.tbxEmpId.Size = new System.Drawing.Size(163, 20);
            this.tbxEmpId.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Employee :";
            // 
            // dgvPaycodes
            // 
            this.dgvPaycodes.AllowUserToAddRows = false;
            this.dgvPaycodes.AllowUserToDeleteRows = false;
            this.dgvPaycodes.AllowUserToOrderColumns = true;
            this.dgvPaycodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPaycodes.Location = new System.Drawing.Point(24, 82);
            this.dgvPaycodes.Name = "dgvPaycodes";
            this.dgvPaycodes.ReadOnly = true;
            this.dgvPaycodes.Size = new System.Drawing.Size(752, 136);
            this.dgvPaycodes.TabIndex = 29;
            this.dgvPaycodes.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPaycodes_CellValidated);
            this.dgvPaycodes.SelectionChanged += new System.EventHandler(this.dgvPaycodes_SelectionChanged);
            // 
            // cmdSave
            // 
            this.cmdSave.Enabled = false;
            this.cmdSave.Location = new System.Drawing.Point(701, 289);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 30;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // lblSql
            // 
            this.lblSql.AutoSize = true;
            this.lblSql.Location = new System.Drawing.Point(28, 277);
            this.lblSql.Name = "lblSql";
            this.lblSql.Size = new System.Drawing.Size(20, 13);
            this.lblSql.TabIndex = 31;
            this.lblSql.Text = "sql";
            this.lblSql.DoubleClick += new System.EventHandler(this.lblSql_DoubleClick);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(620, 289);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(75, 23);
            this.cmdAdd.TabIndex = 32;
            this.cmdAdd.Text = "Add";
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // chkHourly
            // 
            this.chkHourly.AutoSize = true;
            this.chkHourly.Location = new System.Drawing.Point(720, 59);
            this.chkHourly.Name = "chkHourly";
            this.chkHourly.Size = new System.Drawing.Size(56, 17);
            this.chkHourly.TabIndex = 43;
            this.chkHourly.Text = "Hourly";
            this.chkHourly.UseVisualStyleBackColor = true;
            this.chkHourly.Visible = false;
            // 
            // tbxDuty
            // 
            this.tbxDuty.Enabled = false;
            this.tbxDuty.Location = new System.Drawing.Point(270, 39);
            this.tbxDuty.Name = "tbxDuty";
            this.tbxDuty.Size = new System.Drawing.Size(71, 20);
            this.tbxDuty.TabIndex = 44;
            // 
            // tbxOT1
            // 
            this.tbxOT1.Location = new System.Drawing.Point(509, 16);
            this.tbxOT1.Name = "tbxOT1";
            this.tbxOT1.Size = new System.Drawing.Size(71, 20);
            this.tbxOT1.TabIndex = 45;
            // 
            // tbxSalary
            // 
            this.tbxSalary.Location = new System.Drawing.Point(229, 16);
            this.tbxSalary.Name = "tbxSalary";
            this.tbxSalary.Size = new System.Drawing.Size(71, 20);
            this.tbxSalary.TabIndex = 46;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(171, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 47;
            this.label3.Text = "SALARY :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(469, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 48;
            this.label4.Text = "OT1 :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(610, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 50;
            this.label5.Text = "OT2 :";
            // 
            // tbxOT2
            // 
            this.tbxOT2.Location = new System.Drawing.Point(650, 16);
            this.tbxOT2.Name = "tbxOT2";
            this.tbxOT2.Size = new System.Drawing.Size(71, 20);
            this.tbxOT2.TabIndex = 49;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tbxHourly);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbxOT2);
            this.groupBox1.Controls.Add(this.tbxSalary);
            this.groupBox1.Controls.Add(this.tbxOT1);
            this.groupBox1.Location = new System.Drawing.Point(25, 224);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(759, 42);
            this.groupBox1.TabIndex = 51;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Totals";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(325, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 52;
            this.label6.Text = "HOURLY :";
            // 
            // tbxHourly
            // 
            this.tbxHourly.Location = new System.Drawing.Point(383, 16);
            this.tbxHourly.Name = "tbxHourly";
            this.tbxHourly.Size = new System.Drawing.Size(71, 20);
            this.tbxHourly.TabIndex = 51;
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(539, 289);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(75, 23);
            this.cmdDelete.TabIndex = 52;
            this.cmdDelete.Text = "Delete";
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // ucPaycodes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmdDelete);
            this.Controls.Add(this.tbxDuty);
            this.Controls.Add(this.chkHourly);
            this.Controls.Add(this.cmdAdd);
            this.Controls.Add(this.lblSql);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.dgvPaycodes);
            this.Controls.Add(this.dtpDay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblEdit);
            this.Controls.Add(this.lblClose);
            this.Controls.Add(this.tbxEmpId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ucPaycodes";
            this.Size = new System.Drawing.Size(800, 330);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaycodes)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpDay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblEdit;
        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.TextBox tbxEmpId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvPaycodes;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Label lblSql;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.CheckBox chkHourly;
        private System.Windows.Forms.TextBox tbxDuty;
        private System.Windows.Forms.TextBox tbxOT1;
        private System.Windows.Forms.TextBox tbxSalary;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxOT2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbxHourly;
        private System.Windows.Forms.Button cmdDelete;
    }
}
