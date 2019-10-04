namespace letTB_logKF
{
    partial class ucEmp
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
            this.dgvEmp = new System.Windows.Forms.DataGridView();
            this.lblEID = new System.Windows.Forms.Label();
            this.lblEmp = new System.Windows.Forms.Label();
            this.flpBoats = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbxEmp = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmp)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvEmp
            // 
            this.dgvEmp.AllowUserToAddRows = false;
            this.dgvEmp.AllowUserToDeleteRows = false;
            this.dgvEmp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmp.Location = new System.Drawing.Point(521, 3);
            this.dgvEmp.Name = "dgvEmp";
            this.dgvEmp.ReadOnly = true;
            this.dgvEmp.Size = new System.Drawing.Size(89, 68);
            this.dgvEmp.TabIndex = 0;
            // 
            // lblEID
            // 
            this.lblEID.AutoSize = true;
            this.lblEID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEID.Location = new System.Drawing.Point(7, 3);
            this.lblEID.Name = "lblEID";
            this.lblEID.Size = new System.Drawing.Size(28, 13);
            this.lblEID.TabIndex = 210;
            this.lblEID.Text = "EID";
            // 
            // lblEmp
            // 
            this.lblEmp.AutoSize = true;
            this.lblEmp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmp.Location = new System.Drawing.Point(72, 3);
            this.lblEmp.Name = "lblEmp";
            this.lblEmp.Size = new System.Drawing.Size(73, 13);
            this.lblEmp.TabIndex = 211;
            this.lblEmp.Text = "EMPLOYEE";
            // 
            // flpBoats
            // 
            this.flpBoats.Location = new System.Drawing.Point(229, 3);
            this.flpBoats.Name = "flpBoats";
            this.flpBoats.Size = new System.Drawing.Size(390, 14);
            this.flpBoats.TabIndex = 212;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SeaShell;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lbxEmp);
            this.panel1.Controls.Add(this.dgvEmp);
            this.panel1.Location = new System.Drawing.Point(5, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(617, 102);
            this.panel1.TabIndex = 213;
            // 
            // lbxEmp
            // 
            this.lbxEmp.BackColor = System.Drawing.Color.SeaShell;
            this.lbxEmp.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxEmp.FormattingEnabled = true;
            this.lbxEmp.ItemHeight = 16;
            this.lbxEmp.Location = new System.Drawing.Point(0, 0);
            this.lbxEmp.Name = "lbxEmp";
            this.lbxEmp.Size = new System.Drawing.Size(617, 100);
            this.lbxEmp.TabIndex = 1;
            // 
            // ucEmp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flpBoats);
            this.Controls.Add(this.lblEmp);
            this.Controls.Add(this.lblEID);
            this.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
            this.Name = "ucEmp";
            this.Size = new System.Drawing.Size(630, 136);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmp)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEmp;
        private System.Windows.Forms.Label lblEID;
        private System.Windows.Forms.Label lblEmp;
        private System.Windows.Forms.FlowLayoutPanel flpBoats;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox lbxEmp;
    }
}
