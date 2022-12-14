using System.Windows.Forms;
using System.Drawing;

namespace SeNA80
{
    partial class passchange
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(passchange));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Accept = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_OldPass = new System.Windows.Forms.TextBox();
            this.tb_NewPass = new System.Windows.Forms.TextBox();
            this.tb_ConfirmPass = new System.Windows.Forms.TextBox();
            this.lbl_EText = new System.Windows.Forms.Label();
            this.lbl_PassError = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(90, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Old Password:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(84, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "New Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Confirm New Password:";
            // 
            // btn_Accept
            // 
            this.btn_Accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Accept.Enabled = false;
            this.btn_Accept.Location = new System.Drawing.Point(134, 151);
            this.btn_Accept.Name = "btn_Accept";
            this.btn_Accept.Size = new System.Drawing.Size(80, 29);
            this.btn_Accept.TabIndex = 4;
            this.btn_Accept.Text = "&Accept";
            this.btn_Accept.UseVisualStyleBackColor = true;
            this.btn_Accept.Click += new System.EventHandler(this.btn_Accept_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(23, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(310, 44);
            this.label4.TabIndex = 4;
            this.label4.Text = "Please input your current password and your new password. Note: they must be diff" +
    "erent with a minimum size of 6 characters!";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tb_OldPass
            // 
            this.tb_OldPass.Location = new System.Drawing.Point(171, 56);
            this.tb_OldPass.MaxLength = 50;
            this.tb_OldPass.Name = "tb_OldPass";
            this.tb_OldPass.PasswordChar = '*';
            this.tb_OldPass.Size = new System.Drawing.Size(128, 20);
            this.tb_OldPass.TabIndex = 1;
            this.tb_OldPass.TextChanged += new System.EventHandler(this.tb_OldPass_TextChanged);
            // 
            // tb_NewPass
            // 
            this.tb_NewPass.Location = new System.Drawing.Point(171, 86);
            this.tb_NewPass.MaxLength = 50;
            this.tb_NewPass.Name = "tb_NewPass";
            this.tb_NewPass.PasswordChar = '*';
            this.tb_NewPass.Size = new System.Drawing.Size(128, 20);
            this.tb_NewPass.TabIndex = 2;
            this.tb_NewPass.TextChanged += new System.EventHandler(this.tb_NewPass_TextChanged);
            // 
            // tb_ConfirmPass
            // 
            this.tb_ConfirmPass.Location = new System.Drawing.Point(172, 116);
            this.tb_ConfirmPass.MaxLength = 50;
            this.tb_ConfirmPass.Name = "tb_ConfirmPass";
            this.tb_ConfirmPass.PasswordChar = '*';
            this.tb_ConfirmPass.Size = new System.Drawing.Size(127, 20);
            this.tb_ConfirmPass.TabIndex = 3;
            this.tb_ConfirmPass.TextChanged += new System.EventHandler(this.tb_ConfirmPass_TextChanged);
            // 
            // lbl_EText
            // 
            this.lbl_EText.AutoSize = true;
            this.lbl_EText.ForeColor = System.Drawing.Color.Red;
            this.lbl_EText.Location = new System.Drawing.Point(82, 193);
            this.lbl_EText.Name = "lbl_EText";
            this.lbl_EText.Size = new System.Drawing.Size(32, 13);
            this.lbl_EText.TabIndex = 8;
            this.lbl_EText.Text = "Error:";
            this.lbl_EText.Visible = false;
            // 
            // lbl_PassError
            // 
            this.lbl_PassError.AutoSize = true;
            this.lbl_PassError.ForeColor = System.Drawing.Color.Red;
            this.lbl_PassError.Location = new System.Drawing.Point(120, 193);
            this.lbl_PassError.Name = "lbl_PassError";
            this.lbl_PassError.Size = new System.Drawing.Size(13, 13);
            this.lbl_PassError.TabIndex = 9;
            this.lbl_PassError.Text = "--";
            this.lbl_PassError.Visible = false;
            // 
            // passchange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(360, 217);
            this.Controls.Add(this.lbl_PassError);
            this.Controls.Add(this.lbl_EText);
            this.Controls.Add(this.tb_ConfirmPass);
            this.Controls.Add(this.tb_NewPass);
            this.Controls.Add(this.tb_OldPass);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_Accept);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "passchange";
            this.Text = "Change Password";
            this.Load += new System.EventHandler(this.passchange_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Accept;
        private System.Windows.Forms.Label label4;
        private TextBox tb_OldPass;
        private TextBox tb_NewPass;
        private TextBox tb_ConfirmPass;
        private Label lbl_EText;
        private Label lbl_PassError;
    }
}