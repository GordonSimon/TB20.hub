namespace letEmp_KF
{
    partial class frmImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImport));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cmdNew = new System.Windows.Forms.Button();
            this.cmdUpdate = new System.Windows.Forms.Button();
            this.flpTimebook = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.pnlPaydirt = new System.Windows.Forms.Panel();
            this.dgvPaydirt = new System.Windows.Forms.DataGridView();
            this.bnvPaydirt = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.pnlMatch = new System.Windows.Forms.Panel();
            this.lbMatch = new System.Windows.Forms.Label();
            this.tbxPayroll = new System.Windows.Forms.TextBox();
            this.tbxTimebook = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.pnlPaydirt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaydirt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bnvPaydirt)).BeginInit();
            this.bnvPaydirt.SuspendLayout();
            this.pnlMatch.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Controls.Add(this.cmdNew);
            this.flowLayoutPanel1.Controls.Add(this.cmdUpdate);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(722, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(90, 463);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // cmdNew
            // 
            this.cmdNew.Location = new System.Drawing.Point(3, 3);
            this.cmdNew.Name = "cmdNew";
            this.cmdNew.Size = new System.Drawing.Size(82, 29);
            this.cmdNew.TabIndex = 0;
            this.cmdNew.Text = "New";
            this.cmdNew.UseVisualStyleBackColor = true;
            this.cmdNew.Click += new System.EventHandler(this.cmdNew_Click);
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Location = new System.Drawing.Point(3, 38);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(82, 29);
            this.cmdUpdate.TabIndex = 1;
            this.cmdUpdate.Text = "Update";
            this.cmdUpdate.UseVisualStyleBackColor = true;
            this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // flpTimebook
            // 
            this.flpTimebook.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpTimebook.AutoScroll = true;
            this.flpTimebook.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpTimebook.Location = new System.Drawing.Point(331, 29);
            this.flpTimebook.Name = "flpTimebook";
            this.flpTimebook.Size = new System.Drawing.Size(368, 543);
            this.flpTimebook.TabIndex = 3;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel3.Controls.Add(this.cmdCancel);
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(722, 493);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(90, 79);
            this.flowLayoutPanel3.TabIndex = 5;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(3, 45);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(82, 29);
            this.cmdCancel.TabIndex = 0;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // pnlPaydirt
            // 
            this.pnlPaydirt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlPaydirt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPaydirt.Controls.Add(this.bnvPaydirt);
            this.pnlPaydirt.Location = new System.Drawing.Point(12, 133);
            this.pnlPaydirt.Name = "pnlPaydirt";
            this.pnlPaydirt.Size = new System.Drawing.Size(313, 439);
            this.pnlPaydirt.TabIndex = 6;
            // 
            // dgvPaydirt
            // 
            this.dgvPaydirt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPaydirt.Location = new System.Drawing.Point(645, 120);
            this.dgvPaydirt.Name = "dgvPaydirt";
            this.dgvPaydirt.Size = new System.Drawing.Size(62, 167);
            this.dgvPaydirt.TabIndex = 7;
            this.dgvPaydirt.Visible = false;
            this.dgvPaydirt.SelectionChanged += new System.EventHandler(this.dgvPaydirt_SelectionChanged);
            // 
            // bnvPaydirt
            // 
            this.bnvPaydirt.AddNewItem = null;
            this.bnvPaydirt.CountItem = this.bindingNavigatorCountItem;
            this.bnvPaydirt.DeleteItem = null;
            this.bnvPaydirt.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bnvPaydirt.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2});
            this.bnvPaydirt.Location = new System.Drawing.Point(0, 412);
            this.bnvPaydirt.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bnvPaydirt.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bnvPaydirt.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bnvPaydirt.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bnvPaydirt.Name = "bnvPaydirt";
            this.bnvPaydirt.PositionItem = this.bindingNavigatorPositionItem;
            this.bnvPaydirt.Size = new System.Drawing.Size(311, 25);
            this.bnvPaydirt.TabIndex = 0;
            this.bnvPaydirt.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
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
            // pnlMatch
            // 
            this.pnlMatch.BackColor = System.Drawing.Color.PaleGreen;
            this.pnlMatch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMatch.Controls.Add(this.lbMatch);
            this.pnlMatch.Controls.Add(this.tbxPayroll);
            this.pnlMatch.Controls.Add(this.tbxTimebook);
            this.pnlMatch.Location = new System.Drawing.Point(12, 29);
            this.pnlMatch.Name = "pnlMatch";
            this.pnlMatch.Size = new System.Drawing.Size(311, 94);
            this.pnlMatch.TabIndex = 7;
            // 
            // lbMatch
            // 
            this.lbMatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMatch.Location = new System.Drawing.Point(1, 11);
            this.lbMatch.Name = "lbMatch";
            this.lbMatch.Size = new System.Drawing.Size(310, 32);
            this.lbMatch.TabIndex = 53;
            this.lbMatch.Text = "MATCH";
            this.lbMatch.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tbxPayroll
            // 
            this.tbxPayroll.BackColor = System.Drawing.Color.MistyRose;
            this.tbxPayroll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxPayroll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPayroll.Location = new System.Drawing.Point(20, 51);
            this.tbxPayroll.Name = "tbxPayroll";
            this.tbxPayroll.ReadOnly = true;
            this.tbxPayroll.Size = new System.Drawing.Size(112, 26);
            this.tbxPayroll.TabIndex = 52;
            this.tbxPayroll.TabStop = false;
            this.tbxPayroll.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbxTimebook
            // 
            this.tbxTimebook.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tbxTimebook.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxTimebook.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTimebook.Location = new System.Drawing.Point(179, 51);
            this.tbxTimebook.Name = "tbxTimebook";
            this.tbxTimebook.ReadOnly = true;
            this.tbxTimebook.Size = new System.Drawing.Size(112, 26);
            this.tbxTimebook.TabIndex = 51;
            this.tbxTimebook.TabStop = false;
            this.tbxTimebook.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "From Payroll";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(328, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "To Timebook";
            // 
            // frmImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 586);
            this.Controls.Add(this.dgvPaydirt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlMatch);
            this.Controls.Add(this.pnlPaydirt);
            this.Controls.Add(this.flowLayoutPanel3);
            this.Controls.Add(this.flpTimebook);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "frmImport";
            this.Text = "Import from Payroll";
            this.Load += new System.EventHandler(this.frmImport_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.pnlPaydirt.ResumeLayout(false);
            this.pnlPaydirt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaydirt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bnvPaydirt)).EndInit();
            this.bnvPaydirt.ResumeLayout(false);
            this.bnvPaydirt.PerformLayout();
            this.pnlMatch.ResumeLayout(false);
            this.pnlMatch.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button cmdNew;
        private System.Windows.Forms.FlowLayoutPanel flpTimebook;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Panel pnlPaydirt;
        private System.Windows.Forms.BindingNavigator bnvPaydirt;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.DataGridView dgvPaydirt;
        private System.Windows.Forms.Panel pnlMatch;
        private System.Windows.Forms.TextBox tbxTimebook;
        private System.Windows.Forms.Label lbMatch;
        private System.Windows.Forms.TextBox tbxPayroll;
        private System.Windows.Forms.Button cmdUpdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

    }
}