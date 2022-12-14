namespace SeNA80
{
    partial class Load_Protocols
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
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Check = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_PreRun = new System.Windows.Forms.ComboBox();
            this.cb_Run = new System.Windows.Forms.ComboBox();
            this.cb_PostRun = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_Start = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(109, 202);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(146, 41);
            this.btn_Cancel.TabIndex = 0;
            this.btn_Cancel.Text = "&Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Check
            // 
            this.btn_Check.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Check.Location = new System.Drawing.Point(313, 202);
            this.btn_Check.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Check.Name = "btn_Check";
            this.btn_Check.Size = new System.Drawing.Size(146, 41);
            this.btn_Check.TabIndex = 1;
            this.btn_Check.Text = "O.&K.";
            this.btn_Check.UseVisualStyleBackColor = true;
            this.btn_Check.Click += new System.EventHandler(this.btn_Check_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(106, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(341, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Please select the protocols you will use for this synthesis";
            // 
            // cb_PreRun
            // 
            this.cb_PreRun.FormattingEnabled = true;
            this.cb_PreRun.Location = new System.Drawing.Point(198, 93);
            this.cb_PreRun.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cb_PreRun.Name = "cb_PreRun";
            this.cb_PreRun.Size = new System.Drawing.Size(234, 23);
            this.cb_PreRun.TabIndex = 3;
            // 
            // cb_Run
            // 
            this.cb_Run.FormattingEnabled = true;
            this.cb_Run.Location = new System.Drawing.Point(198, 131);
            this.cb_Run.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cb_Run.Name = "cb_Run";
            this.cb_Run.Size = new System.Drawing.Size(234, 23);
            this.cb_Run.TabIndex = 4;
            // 
            // cb_PostRun
            // 
            this.cb_PostRun.FormattingEnabled = true;
            this.cb_PostRun.Location = new System.Drawing.Point(198, 169);
            this.cb_PostRun.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cb_PostRun.Name = "cb_PostRun";
            this.cb_PostRun.Size = new System.Drawing.Size(234, 23);
            this.cb_PostRun.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "&Start-Up Protocol:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(85, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "&Pre-Run Protocl:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(74, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Po&st Run Protocol:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(101, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "&Run Protocol:";
            // 
            // cb_Start
            // 
            this.cb_Start.FormattingEnabled = true;
            this.cb_Start.Location = new System.Drawing.Point(198, 55);
            this.cb_Start.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cb_Start.Name = "cb_Start";
            this.cb_Start.Size = new System.Drawing.Size(234, 23);
            this.cb_Start.TabIndex = 9;
            // 
            // Load_Protocols
            // 
            this.AcceptButton = this.btn_Check;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(615, 272);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cb_Start);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cb_PostRun);
            this.Controls.Add(this.cb_Run);
            this.Controls.Add(this.cb_PreRun);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Check);
            this.Controls.Add(this.btn_Cancel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Load_Protocols";
            this.Text = "Select Protocols to Load";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Check;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox cb_PreRun;
        public System.Windows.Forms.ComboBox cb_Run;
        public System.Windows.Forms.ComboBox cb_PostRun;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.ComboBox cb_Start;
    }
}