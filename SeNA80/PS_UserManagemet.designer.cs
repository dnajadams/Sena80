namespace SeNA80
{
    partial class PS_UserManagemet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PS_UserManagemet));
            this.label1 = new System.Windows.Forms.Label();
            this.tb_UserName = new System.Windows.Forms.TextBox();
            this.chk_passexp = new System.Windows.Forms.CheckBox();
            this.tb_ComfirmPWD = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_NewPWD = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Submit = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.gb_rights = new System.Windows.Forms.GroupBox();
            this.rb_NewUser2 = new System.Windows.Forms.RadioButton();
            this.rb_NewUser1 = new System.Windows.Forms.RadioButton();
            this.rb_NewAdmin = new System.Windows.Forms.RadioButton();
            this.gb_rights.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "&User Name：";
            // 
            // tb_UserName
            // 
            this.tb_UserName.HideSelection = false;
            this.tb_UserName.Location = new System.Drawing.Point(128, 12);
            this.tb_UserName.MaxLength = 100;
            this.tb_UserName.Name = "tb_UserName";
            this.tb_UserName.Size = new System.Drawing.Size(150, 20);
            this.tb_UserName.TabIndex = 2;
            // 
            // chk_passexp
            // 
            this.chk_passexp.AutoSize = true;
            this.chk_passexp.Location = new System.Drawing.Point(95, 166);
            this.chk_passexp.Name = "chk_passexp";
            this.chk_passexp.Size = new System.Drawing.Size(141, 17);
            this.chk_passexp.TabIndex = 8;
            this.chk_passexp.Text = "Password Ne&ver Expires";
            this.chk_passexp.UseVisualStyleBackColor = true;
            // 
            // tb_ComfirmPWD
            // 
            this.tb_ComfirmPWD.Location = new System.Drawing.Point(128, 75);
            this.tb_ComfirmPWD.MaxLength = 20;
            this.tb_ComfirmPWD.Name = "tb_ComfirmPWD";
            this.tb_ComfirmPWD.PasswordChar = '*';
            this.tb_ComfirmPWD.Size = new System.Drawing.Size(136, 20);
            this.tb_ComfirmPWD.TabIndex = 6;
            this.tb_ComfirmPWD.TextChanged += new System.EventHandler(this.tbox_ComfirmPWD_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "&Comfirm Password：";
            // 
            // tb_NewPWD
            // 
            this.tb_NewPWD.Location = new System.Drawing.Point(128, 46);
            this.tb_NewPWD.MaxLength = 20;
            this.tb_NewPWD.Name = "tb_NewPWD";
            this.tb_NewPWD.PasswordChar = '*';
            this.tb_NewPWD.Size = new System.Drawing.Size(136, 20);
            this.tb_NewPWD.TabIndex = 4;
            this.tb_NewPWD.TextChanged += new System.EventHandler(this.tbox_NewPWD_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Ne&w Password：";
            // 
            // btn_Submit
            // 
            this.btn_Submit.Location = new System.Drawing.Point(44, 200);
            this.btn_Submit.Name = "btn_Submit";
            this.btn_Submit.Size = new System.Drawing.Size(111, 25);
            this.btn_Submit.TabIndex = 9;
            this.btn_Submit.Text = "&Add";
            this.btn_Submit.UseVisualStyleBackColor = true;
            this.btn_Submit.Click += new System.EventHandler(this.btn_Submit_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(187, 200);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(111, 25);
            this.btn_Cancel.TabIndex = 10;
            this.btn_Cancel.Text = "&Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // gb_rights
            // 
            this.gb_rights.Controls.Add(this.rb_NewUser2);
            this.gb_rights.Controls.Add(this.rb_NewUser1);
            this.gb_rights.Controls.Add(this.rb_NewAdmin);
            this.gb_rights.Location = new System.Drawing.Point(29, 106);
            this.gb_rights.Margin = new System.Windows.Forms.Padding(2);
            this.gb_rights.Name = "gb_rights";
            this.gb_rights.Padding = new System.Windows.Forms.Padding(2);
            this.gb_rights.Size = new System.Drawing.Size(290, 45);
            this.gb_rights.TabIndex = 7;
            this.gb_rights.TabStop = false;
            this.gb_rights.Text = "A&ccess";
            // 
            // rb_NewUser2
            // 
            this.rb_NewUser2.AutoSize = true;
            this.rb_NewUser2.Location = new System.Drawing.Point(164, 17);
            this.rb_NewUser2.Margin = new System.Windows.Forms.Padding(2);
            this.rb_NewUser2.Name = "rb_NewUser2";
            this.rb_NewUser2.Size = new System.Drawing.Size(56, 17);
            this.rb_NewUser2.TabIndex = 8;
            this.rb_NewUser2.TabStop = true;
            this.rb_NewUser2.Text = "User &2";
            this.rb_NewUser2.UseVisualStyleBackColor = true;
            // 
            // rb_NewUser1
            // 
            this.rb_NewUser1.AutoSize = true;
            this.rb_NewUser1.Location = new System.Drawing.Point(93, 17);
            this.rb_NewUser1.Margin = new System.Windows.Forms.Padding(2);
            this.rb_NewUser1.Name = "rb_NewUser1";
            this.rb_NewUser1.Size = new System.Drawing.Size(56, 17);
            this.rb_NewUser1.TabIndex = 7;
            this.rb_NewUser1.TabStop = true;
            this.rb_NewUser1.Text = "User &1";
            this.rb_NewUser1.UseVisualStyleBackColor = true;
            // 
            // rb_NewAdmin
            // 
            this.rb_NewAdmin.AutoSize = true;
            this.rb_NewAdmin.Location = new System.Drawing.Point(4, 17);
            this.rb_NewAdmin.Margin = new System.Windows.Forms.Padding(2);
            this.rb_NewAdmin.Name = "rb_NewAdmin";
            this.rb_NewAdmin.Size = new System.Drawing.Size(85, 17);
            this.rb_NewAdmin.TabIndex = 6;
            this.rb_NewAdmin.TabStop = true;
            this.rb_NewAdmin.Text = "A&dministrator";
            this.rb_NewAdmin.UseVisualStyleBackColor = true;
            // 
            // PS_UserManagemet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(394, 246);
            this.Controls.Add(this.gb_rights);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Submit);
            this.Controls.Add(this.tb_NewPWD);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_ComfirmPWD);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chk_passexp);
            this.Controls.Add(this.tb_UserName);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PS_UserManagemet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New User";
            this.Load += new System.EventHandler(this.PS_UserManagemet_Load);
            this.gb_rights.ResumeLayout(false);
            this.gb_rights.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Submit;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.GroupBox gb_rights;
        public System.Windows.Forms.TextBox tb_UserName;
        public System.Windows.Forms.TextBox tb_ComfirmPWD;
        public System.Windows.Forms.TextBox tb_NewPWD;
        public System.Windows.Forms.CheckBox chk_passexp;
        public System.Windows.Forms.RadioButton rb_NewUser2;
        public System.Windows.Forms.RadioButton rb_NewUser1;
        public System.Windows.Forms.RadioButton rb_NewAdmin;
    }
}