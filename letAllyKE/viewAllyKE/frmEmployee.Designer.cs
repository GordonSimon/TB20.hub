namespace viewAllyKE
{
    partial class frmEmployee
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
            this.clbItems = new System.Windows.Forms.CheckedListBox();
            this.cmdSave = new System.Windows.Forms.Button();
            this.pnlEmp = new System.Windows.Forms.Panel();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxCount = new System.Windows.Forms.TextBox();
            this.cmdGrid = new System.Windows.Forms.Button();
            this.dgvEmployee = new System.Windows.Forms.DataGridView();
            this.cmdNew = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployee)).BeginInit();
            this.SuspendLayout();
            // 
            // clbItems
            // 
            this.clbItems.FormattingEnabled = true;
            this.clbItems.Location = new System.Drawing.Point(12, 12);
            this.clbItems.Name = "clbItems";
            this.clbItems.Size = new System.Drawing.Size(137, 469);
            this.clbItems.TabIndex = 0;
            this.clbItems.SelectedIndexChanged += new System.EventHandler(this.clbItems_SelectedIndexChanged);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(632, 501);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 1;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // pnlEmp
            // 
            this.pnlEmp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEmp.Location = new System.Drawing.Point(155, 13);
            this.pnlEmp.Name = "pnlEmp";
            this.pnlEmp.Size = new System.Drawing.Size(642, 482);
            this.pnlEmp.TabIndex = 2;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(722, 501);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 506);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Count :";
            // 
            // tbxCount
            // 
            this.tbxCount.Location = new System.Drawing.Point(70, 503);
            this.tbxCount.Name = "tbxCount";
            this.tbxCount.Size = new System.Drawing.Size(79, 20);
            this.tbxCount.TabIndex = 5;
            this.tbxCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmdGrid
            // 
            this.cmdGrid.Location = new System.Drawing.Point(156, 501);
            this.cmdGrid.Name = "cmdGrid";
            this.cmdGrid.Size = new System.Drawing.Size(75, 23);
            this.cmdGrid.TabIndex = 6;
            this.cmdGrid.Text = "Grid";
            this.cmdGrid.UseVisualStyleBackColor = true;
            this.cmdGrid.Click += new System.EventHandler(this.cmdGrid_Click);
            // 
            // dgvEmployee
            // 
            this.dgvEmployee.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmployee.Location = new System.Drawing.Point(-1, 0);
            this.dgvEmployee.Margin = new System.Windows.Forms.Padding(2);
            this.dgvEmployee.Name = "dgvEmployee";
            this.dgvEmployee.RowTemplate.Height = 28;
            this.dgvEmployee.Size = new System.Drawing.Size(81, 81);
            this.dgvEmployee.TabIndex = 8;
            this.dgvEmployee.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmployee_CellDoubleClick);
            // 
            // cmdNew
            // 
            this.cmdNew.Location = new System.Drawing.Point(632, 501);
            this.cmdNew.Name = "cmdNew";
            this.cmdNew.Size = new System.Drawing.Size(75, 23);
            this.cmdNew.TabIndex = 9;
            this.cmdNew.Text = "New";
            this.cmdNew.UseVisualStyleBackColor = true;
            this.cmdNew.Click += new System.EventHandler(this.cmdNew_Click);
            // 
            // frmEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 539);
            this.Controls.Add(this.dgvEmployee);
            this.Controls.Add(this.cmdGrid);
            this.Controls.Add(this.tbxCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.pnlEmp);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.clbItems);
            this.Controls.Add(this.cmdNew);
            this.Name = "frmEmployee";
            this.Text = "frmEmployee (Albali) : May 10/2015 (V1.0b)";
            this.Load += new System.EventHandler(this.frmEmployee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployee)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox clbItems;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Panel pnlEmp;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxCount;
        private System.Windows.Forms.Button cmdGrid;
        private System.Windows.Forms.DataGridView dgvEmployee;
        private System.Windows.Forms.Button cmdNew;
    }
}