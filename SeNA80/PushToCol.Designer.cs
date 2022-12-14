namespace SeNA80
{
    partial class PushToCol
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PushToCol));
            this.lbt_Text = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rb_Pump = new System.Windows.Forms.RadioButton();
            this.rb_Gas = new System.Windows.Forms.RadioButton();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbt_Text
            // 
            this.lbt_Text.AutoSize = true;
            this.lbt_Text.Location = new System.Drawing.Point(42, 32);
            this.lbt_Text.Name = "lbt_Text";
            this.lbt_Text.Size = new System.Drawing.Size(304, 15);
            this.lbt_Text.TabIndex = 0;
            this.lbt_Text.Text = "Push Amidite or Amidite/Activator Mix To Column  With:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rb_Gas);
            this.groupBox1.Controls.Add(this.rb_Pump);
            this.groupBox1.Location = new System.Drawing.Point(46, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 49);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // rb_Pump
            // 
            this.rb_Pump.AutoSize = true;
            this.rb_Pump.Checked = true;
            this.rb_Pump.Location = new System.Drawing.Point(18, 20);
            this.rb_Pump.Name = "rb_Pump";
            this.rb_Pump.Size = new System.Drawing.Size(58, 19);
            this.rb_Pump.TabIndex = 0;
            this.rb_Pump.TabStop = true;
            this.rb_Pump.Text = "&Pump";
            this.rb_Pump.UseVisualStyleBackColor = true;
            // 
            // rb_Gas
            // 
            this.rb_Gas.AutoSize = true;
            this.rb_Gas.Location = new System.Drawing.Point(141, 20);
            this.rb_Gas.Name = "rb_Gas";
            this.rb_Gas.Size = new System.Drawing.Size(76, 19);
            this.rb_Gas.TabIndex = 1;
            this.rb_Gas.Text = "&Gas Flow";
            this.rb_Gas.UseVisualStyleBackColor = true;
            // 
            // btn_OK
            // 
            this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_OK.Location = new System.Drawing.Point(47, 124);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(120, 33);
            this.btn_OK.TabIndex = 2;
            this.btn_OK.Text = "&OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(201, 124);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(120, 33);
            this.btn_Cancel.TabIndex = 3;
            this.btn_Cancel.Text = "&Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // PushToCol
            // 
            this.AcceptButton = this.btn_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(432, 168);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbt_Text);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PushToCol";
            this.Text = "Push To Column";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.RadioButton rb_Gas;
        public System.Windows.Forms.RadioButton rb_Pump;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        public System.Windows.Forms.Label lbt_Text;
    }
}