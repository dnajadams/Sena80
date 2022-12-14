namespace SeNA80
{
    partial class Text_Param
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
            this.lblComments = new System.Windows.Forms.Label();
            this.tb_Comments = new System.Windows.Forms.TextBox();
            this.btn_OK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblComments
            // 
            this.lblComments.AutoSize = true;
            this.lblComments.Location = new System.Drawing.Point(44, 39);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new System.Drawing.Size(103, 15);
            this.lblComments.TabIndex = 0;
            this.lblComments.Text = "Input Comments: ";
            // 
            // tb_Comments
            // 
            this.tb_Comments.Location = new System.Drawing.Point(47, 75);
            this.tb_Comments.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tb_Comments.MaxLength = 300;
            this.tb_Comments.Name = "tb_Comments";
            this.tb_Comments.Size = new System.Drawing.Size(394, 21);
            this.tb_Comments.TabIndex = 1;
            // 
            // btn_OK
            // 
            this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_OK.Location = new System.Drawing.Point(146, 134);
            this.btn_OK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(163, 36);
            this.btn_OK.TabIndex = 2;
            this.btn_OK.Text = "O.K.";
            this.btn_OK.UseVisualStyleBackColor = true;
            // 
            // Text_Param
            // 
            this.AcceptButton = this.btn_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 198);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.tb_Comments);
            this.Controls.Add(this.lblComments);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Text_Param";
            this.Text = "Comments";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblComments;
        private System.Windows.Forms.Button btn_OK;
        public System.Windows.Forms.TextBox tb_Comments;
    }
}