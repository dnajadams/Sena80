namespace SeNA80
{
    partial class UserLoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserLoginForm));
            this.label1 = new System.Windows.Forms.Label();
            this.tb_UserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_Password = new System.Windows.Forms.TextBox();
            this.lbl_Error = new System.Windows.Forms.Label();
            this.lbl_ErrorText = new System.Windows.Forms.Label();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_hidden = new System.Windows.Forms.Button();
            this.btn_show = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(79, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please enter your User Name and Password ..";
            // 
            // tb_UserName
            // 
            this.tb_UserName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tb_UserName.Location = new System.Drawing.Point(152, 53);
            this.tb_UserName.MaxLength = 100;
            this.tb_UserName.Name = "tb_UserName";
            this.tb_UserName.Size = new System.Drawing.Size(195, 20);
            this.tb_UserName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "&User Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(86, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "&Password:";
            // 
            // tb_Password
            // 
            this.tb_Password.Location = new System.Drawing.Point(152, 88);
            this.tb_Password.MaxLength = 50;
            this.tb_Password.Name = "tb_Password";
            this.tb_Password.PasswordChar = '*';
            this.tb_Password.Size = new System.Drawing.Size(195, 20);
            this.tb_Password.TabIndex = 4;
            this.tb_Password.UseSystemPasswordChar = true;
            // 
            // lbl_Error
            // 
            this.lbl_Error.AutoSize = true;
            this.lbl_Error.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Error.ForeColor = System.Drawing.Color.Red;
            this.lbl_Error.Location = new System.Drawing.Point(65, 121);
            this.lbl_Error.Name = "lbl_Error";
            this.lbl_Error.Size = new System.Drawing.Size(37, 15);
            this.lbl_Error.TabIndex = 5;
            this.lbl_Error.Text = "Error:";
            this.lbl_Error.Visible = false;
            // 
            // lbl_ErrorText
            // 
            this.lbl_ErrorText.AutoSize = true;
            this.lbl_ErrorText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ErrorText.ForeColor = System.Drawing.Color.Red;
            this.lbl_ErrorText.Location = new System.Drawing.Point(107, 121);
            this.lbl_ErrorText.Name = "lbl_ErrorText";
            this.lbl_ErrorText.Size = new System.Drawing.Size(28, 15);
            this.lbl_ErrorText.TabIndex = 6;
            this.lbl_ErrorText.Text = "===";
            this.lbl_ErrorText.Visible = false;
            // 
            // btn_OK
            // 
            this.btn_OK.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_OK.Location = new System.Drawing.Point(101, 144);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(80, 29);
            this.btn_OK.TabIndex = 7;
            this.btn_OK.Text = "O&K";
            this.btn_OK.UseVisualStyleBackColor = false;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(210, 144);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(80, 29);
            this.btn_Cancel.TabIndex = 8;
            this.btn_Cancel.Text = "&Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = false;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_hidden
            // 
            this.btn_hidden.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_hidden.Location = new System.Drawing.Point(379, 169);
            this.btn_hidden.Name = "btn_hidden";
            this.btn_hidden.Size = new System.Drawing.Size(14, 10);
            this.btn_hidden.TabIndex = 9;
            this.btn_hidden.UseVisualStyleBackColor = true;
            this.btn_hidden.Visible = false;
            this.btn_hidden.Click += new System.EventHandler(this.btn_hidden_Click);
            // 
            // btn_show
            // 
            this.btn_show.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_show.Location = new System.Drawing.Point(303, 88);
            this.btn_show.Name = "btn_show";
            this.btn_show.Size = new System.Drawing.Size(44, 20);
            this.btn_show.TabIndex = 10;
            this.btn_show.Text = "Show";
            this.btn_show.UseVisualStyleBackColor = true;
            this.btn_show.Click += new System.EventHandler(this.btn_show_Click);
            // 
            // UserLoginForm
            // 
            this.AcceptButton = this.btn_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(395, 179);
            this.Controls.Add(this.btn_show);
            this.Controls.Add(this.btn_hidden);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.lbl_ErrorText);
            this.Controls.Add(this.lbl_Error);
            this.Controls.Add(this.tb_Password);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_UserName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UserLoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SeNA80 Login";
            this.Load += new System.EventHandler(this.UserLoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_Error;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        public System.Windows.Forms.TextBox tb_UserName;
        public System.Windows.Forms.TextBox tb_Password;
        private System.Windows.Forms.Button btn_hidden;
        public System.Windows.Forms.Label lbl_ErrorText;
        private System.Windows.Forms.Button btn_show;
    }
}