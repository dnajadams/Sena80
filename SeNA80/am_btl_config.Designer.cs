namespace SeNA80
{
    partial class am_btl_config
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(am_btl_config));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.am_name = new System.Windows.Forms.TextBox();
            this.btn_Prot_Sel = new System.Windows.Forms.Button();
            this.lbl_Proto = new System.Windows.Forms.Label();
            this.lb_Am_ltr = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(86, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Amidite &Letter:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(85, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "Amidite &Name:";
            // 
            // btn_OK
            // 
            this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_OK.Location = new System.Drawing.Point(96, 227);
            this.btn_OK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(127, 38);
            this.btn_OK.TabIndex = 3;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            this.btn_OK.MouseHover += new System.EventHandler(this.btn_OK_MouseHover);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(259, 227);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(127, 38);
            this.btn_Cancel.TabIndex = 4;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.MouseHover += new System.EventHandler(this.btn_Cancel_MouseHover);
            // 
            // am_name
            // 
            this.am_name.Location = new System.Drawing.Point(181, 98);
            this.am_name.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.am_name.MaxLength = 60;
            this.am_name.Name = "am_name";
            this.am_name.Size = new System.Drawing.Size(142, 21);
            this.am_name.TabIndex = 1;
            this.am_name.MouseHover += new System.EventHandler(this.am_name_MouseHover);
            // 
            // btn_Prot_Sel
            // 
            this.btn_Prot_Sel.Location = new System.Drawing.Point(64, 140);
            this.btn_Prot_Sel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Prot_Sel.Name = "btn_Prot_Sel";
            this.btn_Prot_Sel.Size = new System.Drawing.Size(108, 31);
            this.btn_Prot_Sel.TabIndex = 2;
            this.btn_Prot_Sel.Text = "Select &Protocol";
            this.btn_Prot_Sel.UseVisualStyleBackColor = true;
            this.btn_Prot_Sel.Click += new System.EventHandler(this.btn_Prot_Sel_Click);
            this.btn_Prot_Sel.MouseHover += new System.EventHandler(this.btn_Prot_Sel_MouseHover);
            // 
            // lbl_Proto
            // 
            this.lbl_Proto.AutoSize = true;
            this.lbl_Proto.Location = new System.Drawing.Point(177, 148);
            this.lbl_Proto.Name = "lbl_Proto";
            this.lbl_Proto.Size = new System.Drawing.Size(52, 15);
            this.lbl_Proto.TabIndex = 7;
            this.lbl_Proto.Text = "Protocol";
            // 
            // lb_Am_ltr
            // 
            this.lb_Am_ltr.DropDownWidth = 2;
            this.lb_Am_ltr.FormattingEnabled = true;
            this.lb_Am_ltr.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.lb_Am_ltr.Location = new System.Drawing.Point(181, 58);
            this.lb_Am_ltr.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lb_Am_ltr.Name = "lb_Am_ltr";
            this.lb_Am_ltr.Size = new System.Drawing.Size(49, 23);
            this.lb_Am_ltr.TabIndex = 0;
            this.lb_Am_ltr.SelectedIndexChanged += new System.EventHandler(this.lb_Am_ltr_SelectedIndexChanged);
            this.lb_Am_ltr.MouseHover += new System.EventHandler(this.lb_Am_ltr_MouseHover);
            // 
            // am_btl_config
            // 
            this.AcceptButton = this.btn_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(488, 276);
            this.Controls.Add(this.lb_Am_ltr);
            this.Controls.Add(this.lbl_Proto);
            this.Controls.Add(this.btn_Prot_Sel);
            this.Controls.Add(this.am_name);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "am_btl_config";
            this.Text = "Configure Amidite";
            this.Load += new System.EventHandler(this.am_btl_config_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.TextBox am_name;
        private System.Windows.Forms.Button btn_Prot_Sel;
        private System.Windows.Forms.Label lbl_Proto;
        private System.Windows.Forms.ComboBox lb_Am_ltr;
    }
}