namespace viewAllyKE
{
    partial class ucAM
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
            this.lblAM = new System.Windows.Forms.Label();
            this.lblPM = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblAM
            // 
            this.lblAM.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAM.ForeColor = System.Drawing.Color.Blue;
            this.lblAM.Location = new System.Drawing.Point(0, 0);
            this.lblAM.Margin = new System.Windows.Forms.Padding(0);
            this.lblAM.Name = "lblAM";
            this.lblAM.Size = new System.Drawing.Size(17, 9);
            this.lblAM.TabIndex = 0;
            this.lblAM.Text = "AM";
            // 
            // lblPM
            // 
            this.lblPM.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPM.ForeColor = System.Drawing.Color.Red;
            this.lblPM.Location = new System.Drawing.Point(0, 9);
            this.lblPM.Margin = new System.Windows.Forms.Padding(0);
            this.lblPM.Name = "lblPM";
            this.lblPM.Size = new System.Drawing.Size(17, 9);
            this.lblPM.TabIndex = 1;
            this.lblPM.Text = "PM";
            // 
            // ucAM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblPM);
            this.Controls.Add(this.lblAM);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucAM";
            this.Size = new System.Drawing.Size(16, 18);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblAM;
        private System.Windows.Forms.Label lblPM;
    }
}
