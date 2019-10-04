namespace letTimebook
{
    partial class frmMain
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvTimebook = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cmdLoad = new System.Windows.Forms.Button();
            this.cmdMemory = new System.Windows.Forms.Button();
            this.lblCountTimebook = new System.Windows.Forms.Label();
            this.lblCountEmployee = new System.Windows.Forms.Label();
            this.dgvEmployee = new System.Windows.Forms.DataGridView();
            this.lblCountToff = new System.Windows.Forms.Label();
            this.dgvToff = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimebook)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvToff)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblCountToff);
            this.panel1.Controls.Add(this.dgvToff);
            this.panel1.Controls.Add(this.lblCountEmployee);
            this.panel1.Controls.Add(this.dgvEmployee);
            this.panel1.Controls.Add(this.lblCountTimebook);
            this.panel1.Controls.Add(this.dgvTimebook);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(717, 615);
            this.panel1.TabIndex = 0;
            // 
            // dgvTimebook
            // 
            this.dgvTimebook.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTimebook.Location = new System.Drawing.Point(28, 61);
            this.dgvTimebook.Name = "dgvTimebook";
            this.dgvTimebook.Size = new System.Drawing.Size(657, 130);
            this.dgvTimebook.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.cmdLoad);
            this.flowLayoutPanel1.Controls.Add(this.cmdMemory);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(835, 16);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(119, 161);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // cmdLoad
            // 
            this.cmdLoad.Location = new System.Drawing.Point(3, 3);
            this.cmdLoad.Name = "cmdLoad";
            this.cmdLoad.Size = new System.Drawing.Size(75, 23);
            this.cmdLoad.TabIndex = 0;
            this.cmdLoad.Text = "Load";
            this.cmdLoad.UseVisualStyleBackColor = true;
            this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
            // 
            // cmdMemory
            // 
            this.cmdMemory.Location = new System.Drawing.Point(3, 32);
            this.cmdMemory.Name = "cmdMemory";
            this.cmdMemory.Size = new System.Drawing.Size(75, 23);
            this.cmdMemory.TabIndex = 1;
            this.cmdMemory.Text = "Memory";
            this.cmdMemory.UseVisualStyleBackColor = true;
            this.cmdMemory.Click += new System.EventHandler(this.cmdMemory_Click);
            // 
            // lblCountTimebook
            // 
            this.lblCountTimebook.AutoSize = true;
            this.lblCountTimebook.Location = new System.Drawing.Point(25, 22);
            this.lblCountTimebook.Name = "lblCountTimebook";
            this.lblCountTimebook.Size = new System.Drawing.Size(28, 15);
            this.lblCountTimebook.TabIndex = 1;
            this.lblCountTimebook.Text = "<0>";
            // 
            // lblCountEmployee
            // 
            this.lblCountEmployee.AutoSize = true;
            this.lblCountEmployee.Location = new System.Drawing.Point(25, 213);
            this.lblCountEmployee.Name = "lblCountEmployee";
            this.lblCountEmployee.Size = new System.Drawing.Size(28, 15);
            this.lblCountEmployee.TabIndex = 3;
            this.lblCountEmployee.Text = "<0>";
            // 
            // dgvEmployee
            // 
            this.dgvEmployee.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmployee.Location = new System.Drawing.Point(28, 252);
            this.dgvEmployee.Name = "dgvEmployee";
            this.dgvEmployee.Size = new System.Drawing.Size(657, 130);
            this.dgvEmployee.TabIndex = 2;
            // 
            // lblCountToff
            // 
            this.lblCountToff.AutoSize = true;
            this.lblCountToff.Location = new System.Drawing.Point(25, 396);
            this.lblCountToff.Name = "lblCountToff";
            this.lblCountToff.Size = new System.Drawing.Size(28, 15);
            this.lblCountToff.TabIndex = 5;
            this.lblCountToff.Text = "<0>";
            // 
            // dgvToff
            // 
            this.dgvToff.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvToff.Location = new System.Drawing.Point(28, 435);
            this.dgvToff.Name = "dgvToff";
            this.dgvToff.Size = new System.Drawing.Size(657, 130);
            this.dgvToff.TabIndex = 4;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 656);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Name = "frmMain";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimebook)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvToff)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvTimebook;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button cmdLoad;
        private System.Windows.Forms.Button cmdMemory;
        private System.Windows.Forms.Label lblCountEmployee;
        private System.Windows.Forms.DataGridView dgvEmployee;
        private System.Windows.Forms.Label lblCountTimebook;
        private System.Windows.Forms.Label lblCountToff;
        private System.Windows.Forms.DataGridView dgvToff;
    }
}

