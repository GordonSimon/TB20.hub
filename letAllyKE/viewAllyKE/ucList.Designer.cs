namespace viewAllyKE
{
    partial class ucList
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
            this.clbItems = new System.Windows.Forms.CheckedListBox();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.cmdDel = new System.Windows.Forms.Button();
            this.cmdEdit = new System.Windows.Forms.Button();
            this.tbxGangName = new System.Windows.Forms.TextBox();
            this.lbxItems = new System.Windows.Forms.ListBox();
            this.cmdEmployee = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // clbItems
            // 
            this.clbItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.clbItems.FormattingEnabled = true;
            this.clbItems.Location = new System.Drawing.Point(2, 67);
            this.clbItems.Name = "clbItems";
            this.clbItems.Size = new System.Drawing.Size(123, 19);
            this.clbItems.TabIndex = 1;
            this.clbItems.Visible = false;
            this.clbItems.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbItems_ItemCheck);
            this.clbItems.SelectedIndexChanged += new System.EventHandler(this.clbItems_SelectedIndexChanged);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(55, 38);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(33, 23);
            this.cmdAdd.TabIndex = 2;
            this.cmdAdd.Text = "+";
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdDel
            // 
            this.cmdDel.Location = new System.Drawing.Point(94, 38);
            this.cmdDel.Name = "cmdDel";
            this.cmdDel.Size = new System.Drawing.Size(33, 23);
            this.cmdDel.TabIndex = 3;
            this.cmdDel.Text = "-";
            this.cmdDel.UseVisualStyleBackColor = true;
            this.cmdDel.Click += new System.EventHandler(this.cmdDel_Click);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(4, 38);
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Size = new System.Drawing.Size(45, 23);
            this.cmdEdit.TabIndex = 4;
            this.cmdEdit.Text = "View";
            this.cmdEdit.UseVisualStyleBackColor = true;
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // tbxGangName
            // 
            this.tbxGangName.BackColor = System.Drawing.Color.Black;
            this.tbxGangName.Font = new System.Drawing.Font("Bookman Old Style", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxGangName.ForeColor = System.Drawing.Color.White;
            this.tbxGangName.Location = new System.Drawing.Point(4, 7);
            this.tbxGangName.Name = "tbxGangName";
            this.tbxGangName.ReadOnly = true;
            this.tbxGangName.Size = new System.Drawing.Size(122, 22);
            this.tbxGangName.TabIndex = 5;
            this.tbxGangName.Text = "No Gang";
            this.tbxGangName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbxItems
            // 
            this.lbxItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbxItems.FormattingEnabled = true;
            this.lbxItems.Location = new System.Drawing.Point(4, 66);
            this.lbxItems.Name = "lbxItems";
            this.lbxItems.Size = new System.Drawing.Size(120, 199);
            this.lbxItems.TabIndex = 6;
            // 
            // cmdEmployee
            // 
            this.cmdEmployee.Location = new System.Drawing.Point(4, 271);
            this.cmdEmployee.Name = "cmdEmployee";
            this.cmdEmployee.Size = new System.Drawing.Size(120, 23);
            this.cmdEmployee.TabIndex = 7;
            this.cmdEmployee.Text = "Employees";
            this.cmdEmployee.UseVisualStyleBackColor = true;
            this.cmdEmployee.Click += new System.EventHandler(this.cmdEmployee_Click);
            // 
            // ucList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.cmdEmployee);
            this.Controls.Add(this.lbxItems);
            this.Controls.Add(this.tbxGangName);
            this.Controls.Add(this.cmdEdit);
            this.Controls.Add(this.cmdDel);
            this.Controls.Add(this.cmdAdd);
            this.Controls.Add(this.clbItems);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucList";
            this.Size = new System.Drawing.Size(130, 300);
            this.Load += new System.EventHandler(this.ucList_Load);
            this.SizeChanged += new System.EventHandler(this.ucList_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox clbItems;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.Button cmdDel;
        private System.Windows.Forms.Button cmdEdit;
        private System.Windows.Forms.TextBox tbxGangName;
        private System.Windows.Forms.ListBox lbxItems;
        private System.Windows.Forms.Button cmdEmployee;
    }
}
