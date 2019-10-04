namespace viewAllyKE
{
    partial class ucEmpNum
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
            this.lbxEmp = new System.Windows.Forms.ListBox();
            this.tbxFilter = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbxEmp
            // 
            this.lbxEmp.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxEmp.FormattingEnabled = true;
            this.lbxEmp.ItemHeight = 15;
            this.lbxEmp.Location = new System.Drawing.Point(0, 0);
            this.lbxEmp.Name = "lbxEmp";
            this.lbxEmp.Size = new System.Drawing.Size(235, 214);
            this.lbxEmp.TabIndex = 0;
            // 
            // tbxFilter
            // 
            this.tbxFilter.Location = new System.Drawing.Point(0, 231);
            this.tbxFilter.Name = "tbxFilter";
            this.tbxFilter.Size = new System.Drawing.Size(235, 20);
            this.tbxFilter.TabIndex = 1;
            this.tbxFilter.TextChanged += new System.EventHandler(this.tbxFilter_TextChanged);
            // 
            // ucEmpNum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbxFilter);
            this.Controls.Add(this.lbxEmp);
            this.Name = "ucEmpNum";
            this.Size = new System.Drawing.Size(238, 258);
            this.Load += new System.EventHandler(this.ucEmpNum_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbxEmp;
        private System.Windows.Forms.TextBox tbxFilter;
    }
}
