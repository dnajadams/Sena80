namespace SeNA80
{
    partial class Wait_Form
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
            this.lbl_Wait = new System.Windows.Forms.Label();
            this.n_Wait = new System.Windows.Forms.NumericUpDown();
            this.lbl_Sec = new System.Windows.Forms.Label();
            this.btn_OK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.n_Wait)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Wait
            // 
            this.lbl_Wait.AutoSize = true;
            this.lbl_Wait.Location = new System.Drawing.Point(48, 46);
            this.lbl_Wait.Name = "lbl_Wait";
            this.lbl_Wait.Size = new System.Drawing.Size(55, 15);
            this.lbl_Wait.TabIndex = 0;
            this.lbl_Wait.Text = "Wait For:";
            // 
            // n_Wait
            // 
            this.n_Wait.Location = new System.Drawing.Point(122, 44);
            this.n_Wait.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.n_Wait.Maximum = new decimal(new int[] {
            1800,
            0,
            0,
            0});
            this.n_Wait.Name = "n_Wait";
            this.n_Wait.Size = new System.Drawing.Size(89, 21);
            this.n_Wait.TabIndex = 1;
            this.n_Wait.ThousandsSeparator = true;
            this.n_Wait.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lbl_Sec
            // 
            this.lbl_Sec.AutoSize = true;
            this.lbl_Sec.Location = new System.Drawing.Point(221, 46);
            this.lbl_Sec.Name = "lbl_Sec";
            this.lbl_Sec.Size = new System.Drawing.Size(55, 15);
            this.lbl_Sec.TabIndex = 2;
            this.lbl_Sec.Text = "Seconds";
            // 
            // btn_OK
            // 
            this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_OK.Location = new System.Drawing.Point(112, 105);
            this.btn_OK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(133, 39);
            this.btn_OK.TabIndex = 3;
            this.btn_OK.Text = "O.K.";
            this.btn_OK.UseVisualStyleBackColor = true;
            // 
            // Wait_Form
            // 
            this.AcceptButton = this.btn_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 157);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.lbl_Sec);
            this.Controls.Add(this.n_Wait);
            this.Controls.Add(this.lbl_Wait);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Wait_Form";
            this.Text = "Wait Parameter";
            ((System.ComponentModel.ISupportInitialize)(this.n_Wait)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.NumericUpDown n_Wait;
        public System.Windows.Forms.Button btn_OK;
        public System.Windows.Forms.Label lbl_Wait;
        public System.Windows.Forms.Label lbl_Sec;
    }
}