namespace viewLogKE
{
    partial class ucSheet
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbxShift = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxFinish = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxStart = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxFuel = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cbxLogID = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Log # :";
            // 
            // tbxShift
            // 
            this.tbxShift.Location = new System.Drawing.Point(95, 76);
            this.tbxShift.Name = "tbxShift";
            this.tbxShift.Size = new System.Drawing.Size(100, 20);
            this.tbxShift.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Shift Start :";
            // 
            // tbxFinish
            // 
            this.tbxFinish.Location = new System.Drawing.Point(144, 128);
            this.tbxFinish.Name = "tbxFinish";
            this.tbxFinish.Size = new System.Drawing.Size(100, 20);
            this.tbxFinish.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Engine Hours (Finish) :";
            // 
            // tbxStart
            // 
            this.tbxStart.Location = new System.Drawing.Point(144, 102);
            this.tbxStart.Name = "tbxStart";
            this.tbxStart.Size = new System.Drawing.Size(100, 20);
            this.tbxStart.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Engine Hours (Start) :";
            // 
            // tbxFuel
            // 
            this.tbxFuel.Location = new System.Drawing.Point(95, 154);
            this.tbxFuel.Name = "tbxFuel";
            this.tbxFuel.Size = new System.Drawing.Size(100, 20);
            this.tbxFuel.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Fuel :";
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(225, 187);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 5;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(306, 187);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 6;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cbxLogID
            // 
            this.cbxLogID.FormattingEnabled = true;
            this.cbxLogID.Location = new System.Drawing.Point(95, 38);
            this.cbxLogID.Name = "cbxLogID";
            this.cbxLogID.Size = new System.Drawing.Size(121, 21);
            this.cbxLogID.TabIndex = 0;
            this.cbxLogID.SelectionChangeCommitted += new System.EventHandler(this.cbxLogID_SelectionChangeCommitted);
            this.cbxLogID.TextChanged += new System.EventHandler(this.cbxLogID_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(17, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(191, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Enter or Edit Log Sheet Details :";
            // 
            // ucSheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbxLogID);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.tbxFuel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbxFinish);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxStart);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbxShift);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ucSheet";
            this.Size = new System.Drawing.Size(400, 220);
            this.Load += new System.EventHandler(this.ucSheet_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxShift;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxFinish;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxStart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxFuel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.ComboBox cbxLogID;
        private System.Windows.Forms.Label label6;
    }
}
