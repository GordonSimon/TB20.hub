namespace viewAllyKE
{
    partial class ucHour
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
            this.label2 = new System.Windows.Forms.Label();
            this.tbxEmpId = new System.Windows.Forms.TextBox();
            this.lblClose = new System.Windows.Forms.Label();
            this.lblEdit = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDay = new System.Windows.Forms.DateTimePicker();
            this.cmdSave = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxHour = new System.Windows.Forms.TextBox();
            this.tbxOver = new System.Windows.Forms.TextBox();
            this.tbxVessel = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Employee :";
            // 
            // tbxEmpId
            // 
            this.tbxEmpId.Location = new System.Drawing.Point(76, 37);
            this.tbxEmpId.Name = "tbxEmpId";
            this.tbxEmpId.Size = new System.Drawing.Size(163, 20);
            this.tbxEmpId.TabIndex = 1;
            this.tbxEmpId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxEmpId_KeyPress);
            // 
            // lblClose
            // 
            this.lblClose.AutoSize = true;
            this.lblClose.Location = new System.Drawing.Point(353, 12);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(14, 13);
            this.lblClose.TabIndex = 6;
            this.lblClose.Text = "X";
            this.lblClose.Click += new System.EventHandler(this.lblClose_Click);
            // 
            // lblEdit
            // 
            this.lblEdit.AutoSize = true;
            this.lblEdit.Location = new System.Drawing.Point(333, 12);
            this.lblEdit.Name = "lblEdit";
            this.lblEdit.Size = new System.Drawing.Size(14, 13);
            this.lblEdit.TabIndex = 5;
            this.lblEdit.Text = "E";
            this.lblEdit.Visible = false;
            this.lblEdit.Click += new System.EventHandler(this.lblEdit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Date :";
            // 
            // dtpDay
            // 
            this.dtpDay.Location = new System.Drawing.Point(76, 12);
            this.dtpDay.Name = "dtpDay";
            this.dtpDay.Size = new System.Drawing.Size(161, 20);
            this.dtpDay.TabIndex = 2;
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(292, 147);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 3;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Hours (Paid) :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "OverTime (Premium) :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Vessel :";
            // 
            // tbxHour
            // 
            this.tbxHour.Location = new System.Drawing.Point(166, 19);
            this.tbxHour.Name = "tbxHour";
            this.tbxHour.Size = new System.Drawing.Size(64, 20);
            this.tbxHour.TabIndex = 12;
            // 
            // tbxOver
            // 
            this.tbxOver.Location = new System.Drawing.Point(166, 45);
            this.tbxOver.Name = "tbxOver";
            this.tbxOver.Size = new System.Drawing.Size(64, 20);
            this.tbxOver.TabIndex = 13;
            // 
            // tbxVessel
            // 
            this.tbxVessel.Location = new System.Drawing.Point(166, 71);
            this.tbxVessel.Name = "tbxVessel";
            this.tbxVessel.Size = new System.Drawing.Size(64, 20);
            this.tbxVessel.TabIndex = 14;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbxVessel);
            this.groupBox1.Controls.Add(this.tbxOver);
            this.groupBox1.Controls.Add(this.tbxHour);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(14, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(253, 100);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log Adjusments";
            // 
            // ucHour
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.dtpDay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblEdit);
            this.Controls.Add(this.lblClose);
            this.Controls.Add(this.tbxEmpId);
            this.Controls.Add(this.label2);
            this.Name = "ucHour";
            this.Size = new System.Drawing.Size(380, 179);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ucHour_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ucHour_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ucHour_MouseUp);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxEmpId;
        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.Label lblEdit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDay;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxHour;
        private System.Windows.Forms.TextBox tbxOver;
        private System.Windows.Forms.TextBox tbxVessel;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
