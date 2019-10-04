namespace viewAllyKE
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
            this.components = new System.ComponentModel.Container();
            this.lblName = new System.Windows.Forms.Label();
            this.lblHome = new System.Windows.Forms.Label();
            this.lblCell = new System.Windows.Forms.Label();
            this.lblMaster = new System.Windows.Forms.Label();
            this.lblSkipper = new System.Windows.Forms.Label();
            this.lblEid = new System.Windows.Forms.Label();
            this.cmsEmployee = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.archiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblDays = new System.Windows.Forms.Label();
            this.pnlBox = new System.Windows.Forms.Panel();
            this.cmsEmployee.SuspendLayout();
            this.pnlBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(3, 5);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(84, 13);
            this.lblName.TabIndex = 19;
            this.lblName.Text = "Employee Name";
            this.lblName.Click += new System.EventHandler(this.lblName_Click);
            // 
            // lblHome
            // 
            this.lblHome.AutoSize = true;
            this.lblHome.Location = new System.Drawing.Point(8, 25);
            this.lblHome.Name = "lblHome";
            this.lblHome.Size = new System.Drawing.Size(90, 13);
            this.lblHome.TabIndex = 20;
            this.lblHome.Text = "H : 604-111-2222";
            this.lblHome.Click += new System.EventHandler(this.lblHome_Click);
            // 
            // lblCell
            // 
            this.lblCell.AutoSize = true;
            this.lblCell.Location = new System.Drawing.Point(8, 42);
            this.lblCell.Name = "lblCell";
            this.lblCell.Size = new System.Drawing.Size(89, 13);
            this.lblCell.TabIndex = 21;
            this.lblCell.Text = "C : 778-123-4567";
            this.lblCell.Click += new System.EventHandler(this.lblCell_Click);
            // 
            // lblMaster
            // 
            this.lblMaster.AutoSize = true;
            this.lblMaster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMaster.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaster.Location = new System.Drawing.Point(80, 60);
            this.lblMaster.Name = "lblMaster";
            this.lblMaster.Size = new System.Drawing.Size(16, 14);
            this.lblMaster.TabIndex = 0;
            this.lblMaster.Text = "M";
            // 
            // lblSkipper
            // 
            this.lblSkipper.AutoSize = true;
            this.lblSkipper.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSkipper.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSkipper.ForeColor = System.Drawing.Color.Red;
            this.lblSkipper.Location = new System.Drawing.Point(80, 60);
            this.lblSkipper.Name = "lblSkipper";
            this.lblSkipper.Size = new System.Drawing.Size(14, 14);
            this.lblSkipper.TabIndex = 22;
            this.lblSkipper.Text = "S";
            // 
            // lblEid
            // 
            this.lblEid.AutoSize = true;
            this.lblEid.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblEid.Location = new System.Drawing.Point(3, 60);
            this.lblEid.Name = "lblEid";
            this.lblEid.Size = new System.Drawing.Size(25, 13);
            this.lblEid.TabIndex = 23;
            this.lblEid.Text = "EID";
            // 
            // cmsEmployee
            // 
            this.cmsEmployee.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.toolStripSeparator1,
            this.archiveToolStripMenuItem});
            this.cmsEmployee.Name = "cmsEmployee";
            this.cmsEmployee.Size = new System.Drawing.Size(115, 54);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(111, 6);
            // 
            // archiveToolStripMenuItem
            // 
            this.archiveToolStripMenuItem.Enabled = false;
            this.archiveToolStripMenuItem.Name = "archiveToolStripMenuItem";
            this.archiveToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.archiveToolStripMenuItem.Text = "Archive";
            this.archiveToolStripMenuItem.Click += new System.EventHandler(this.archiveToolStripMenuItem_Click);
            // 
            // lblDays
            // 
            this.lblDays.AutoSize = true;
            this.lblDays.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblDays.Location = new System.Drawing.Point(2, 2);
            this.lblDays.Name = "lblDays";
            this.lblDays.Size = new System.Drawing.Size(26, 13);
            this.lblDays.TabIndex = 24;
            this.lblDays.Text = "Day";
            this.lblDays.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDays.Click += new System.EventHandler(this.lblDays_Click);
            // 
            // pnlBox
            // 
            this.pnlBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBox.Controls.Add(this.lblDays);
            this.pnlBox.Location = new System.Drawing.Point(117, 1);
            this.pnlBox.Name = "pnlBox";
            this.pnlBox.Size = new System.Drawing.Size(32, 76);
            this.pnlBox.TabIndex = 25;
            this.pnlBox.MouseHover += new System.EventHandler(this.pnlBox_MouseHover);
            // 
            // ucEmp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.cmsEmployee;
            this.Controls.Add(this.pnlBox);
            this.Controls.Add(this.lblEid);
            this.Controls.Add(this.lblSkipper);
            this.Controls.Add(this.lblMaster);
            this.Controls.Add(this.lblCell);
            this.Controls.Add(this.lblHome);
            this.Controls.Add(this.lblName);
            this.Name = "ucEmp";
            this.Size = new System.Drawing.Size(150, 77);
            this.Click += new System.EventHandler(this.ucEmp_Click);
            this.cmsEmployee.ResumeLayout(false);
            this.pnlBox.ResumeLayout(false);
            this.pnlBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblHome;
        private System.Windows.Forms.Label lblCell;
        private System.Windows.Forms.Label lblMaster;
        private System.Windows.Forms.Label lblSkipper;
        private System.Windows.Forms.Label lblEid;
        private System.Windows.Forms.ContextMenuStrip cmsEmployee;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem archiveToolStripMenuItem;
        private System.Windows.Forms.Label lblDays;
        private System.Windows.Forms.Panel pnlBox;
    }
}
