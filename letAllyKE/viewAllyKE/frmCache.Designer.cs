namespace viewAllyKE
{
    partial class frmCache
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCache));
            this.bwkCache = new System.ComponentModel.BackgroundWorker();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdLogin1 = new System.Windows.Forms.Button();
            this.txtUser1 = new System.Windows.Forms.TextBox();
            this.txtUser2 = new System.Windows.Forms.TextBox();
            this.cmdLogin2 = new System.Windows.Forms.Button();
            this.cbxUsers = new System.Windows.Forms.ComboBox();
            this.cmdLogin3 = new System.Windows.Forms.Button();
            this.pbxApp = new System.Windows.Forms.PictureBox();
            this.lblWait = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblDB = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbxApp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // bwkCache
            // 
            this.bwkCache.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwkCache_DoWork);
            this.bwkCache.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwkCache_ProgressChanged);
            this.bwkCache.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwkCache_RunWorkerCompleted);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(390, 177);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 0;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(292, 177);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 24);
            this.cmdOK.TabIndex = 1;
            this.cmdOK.Text = "Guest";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdLogin1
            // 
            this.cmdLogin1.Location = new System.Drawing.Point(390, 37);
            this.cmdLogin1.Name = "cmdLogin1";
            this.cmdLogin1.Size = new System.Drawing.Size(75, 23);
            this.cmdLogin1.TabIndex = 2;
            this.cmdLogin1.Text = "Login";
            this.cmdLogin1.UseVisualStyleBackColor = true;
            this.cmdLogin1.Visible = false;
            this.cmdLogin1.Click += new System.EventHandler(this.cmdLogin1_Click);
            // 
            // txtUser1
            // 
            this.txtUser1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser1.Location = new System.Drawing.Point(162, 35);
            this.txtUser1.Name = "txtUser1";
            this.txtUser1.Size = new System.Drawing.Size(205, 25);
            this.txtUser1.TabIndex = 3;
            this.txtUser1.Visible = false;
            // 
            // txtUser2
            // 
            this.txtUser2.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser2.Location = new System.Drawing.Point(162, 75);
            this.txtUser2.Name = "txtUser2";
            this.txtUser2.Size = new System.Drawing.Size(205, 25);
            this.txtUser2.TabIndex = 5;
            this.txtUser2.Visible = false;
            // 
            // cmdLogin2
            // 
            this.cmdLogin2.Location = new System.Drawing.Point(390, 77);
            this.cmdLogin2.Name = "cmdLogin2";
            this.cmdLogin2.Size = new System.Drawing.Size(75, 23);
            this.cmdLogin2.TabIndex = 4;
            this.cmdLogin2.Text = "Login";
            this.cmdLogin2.UseVisualStyleBackColor = true;
            this.cmdLogin2.Visible = false;
            this.cmdLogin2.Click += new System.EventHandler(this.cmdLogin2_Click);
            // 
            // cbxUsers
            // 
            this.cbxUsers.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxUsers.FormattingEnabled = true;
            this.cbxUsers.Location = new System.Drawing.Point(162, 118);
            this.cbxUsers.Name = "cbxUsers";
            this.cbxUsers.Size = new System.Drawing.Size(205, 27);
            this.cbxUsers.TabIndex = 6;
            this.cbxUsers.Visible = false;
            // 
            // cmdLogin3
            // 
            this.cmdLogin3.Location = new System.Drawing.Point(390, 120);
            this.cmdLogin3.Name = "cmdLogin3";
            this.cmdLogin3.Size = new System.Drawing.Size(75, 23);
            this.cmdLogin3.TabIndex = 7;
            this.cmdLogin3.Text = "Login";
            this.cmdLogin3.UseVisualStyleBackColor = true;
            this.cmdLogin3.Visible = false;
            this.cmdLogin3.Click += new System.EventHandler(this.cmdLogin3_Click);
            // 
            // pbxApp
            // 
            this.pbxApp.Image = global::viewAllyKE.Properties.Resources.player_time;
            this.pbxApp.Location = new System.Drawing.Point(12, 17);
            this.pbxApp.Name = "pbxApp";
            this.pbxApp.Size = new System.Drawing.Size(128, 128);
            this.pbxApp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbxApp.TabIndex = 8;
            this.pbxApp.TabStop = false;
            // 
            // lblWait
            // 
            this.lblWait.AutoSize = true;
            this.lblWait.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWait.Location = new System.Drawing.Point(160, 51);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(265, 21);
            this.lblWait.TabIndex = 9;
            this.lblWait.Text = "Loading data, please wait ..";
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(160, 94);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(223, 21);
            this.lblError.TabIndex = 10;
            this.lblError.Text = "Error : Netwok failure !";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // lblDB
            // 
            this.lblDB.AutoSize = true;
            this.lblDB.Location = new System.Drawing.Point(57, 166);
            this.lblDB.Name = "lblDB";
            this.lblDB.Size = new System.Drawing.Size(53, 13);
            this.lblDB.TabIndex = 11;
            this.lblDB.Text = "Database";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 166);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "DB Info :";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(10, 180);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(63, 13);
            this.lblInfo.TabIndex = 14;
            this.lblInfo.Text = "w x h @ dpi";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(10, 194);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(34, 13);
            this.lblVersion.TabIndex = 15;
            this.lblVersion.Text = "v3.2a";
            // 
            // frmCache
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 212);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDB);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.lblWait);
            this.Controls.Add(this.pbxApp);
            this.Controls.Add(this.cmdLogin3);
            this.Controls.Add(this.cbxUsers);
            this.Controls.Add(this.txtUser2);
            this.Controls.Add(this.cmdLogin2);
            this.Controls.Add(this.txtUser1);
            this.Controls.Add(this.cmdLogin1);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCache";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "Copyright (C) GarNet RC 2016 V11.25";
            this.Load += new System.EventHandler(this.frmCache_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxApp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bwkCache;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdLogin1;
        private System.Windows.Forms.TextBox txtUser1;
        private System.Windows.Forms.TextBox txtUser2;
        private System.Windows.Forms.Button cmdLogin2;
        private System.Windows.Forms.ComboBox cbxUsers;
        private System.Windows.Forms.Button cmdLogin3;
        private System.Windows.Forms.PictureBox pbxApp;
        private System.Windows.Forms.Label lblWait;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label lblDB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblVersion;
    }
}