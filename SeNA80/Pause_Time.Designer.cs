namespace SeNA80
{
    partial class Pause_Time
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbHowLong = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bOK = new System.Windows.Forms.Button();
            this.bCANCEL = new System.Windows.Forms.Button();
            this.lbl_CountDown = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnl_timer = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.pnl_timer.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(54, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Wait For:";
            // 
            // cbHowLong
            // 
            this.cbHowLong.FormattingEnabled = true;
            this.cbHowLong.Items.AddRange(new object[] {
            "1",
            "2",
            "5",
            "10",
            "30",
            "60",
            "180",
            "360"});
            this.cbHowLong.Location = new System.Drawing.Point(132, 105);
            this.cbHowLong.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbHowLong.Name = "cbHowLong";
            this.cbHowLong.Size = new System.Drawing.Size(56, 23);
            this.cbHowLong.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(206, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Minutes";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(36, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(309, 65);
            this.label2.TabIndex = 7;
            this.label2.Text = "How long would you like to pause for?  Once you prese the OK button, I will pause" +
    " for the desired number of minutes.";
            // 
            // bOK
            // 
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.Location = new System.Drawing.Point(40, 155);
            this.bOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(112, 34);
            this.bOK.TabIndex = 8;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bCANCEL
            // 
            this.bCANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCANCEL.Location = new System.Drawing.Point(185, 155);
            this.bCANCEL.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bCANCEL.Name = "bCANCEL";
            this.bCANCEL.Size = new System.Drawing.Size(112, 34);
            this.bCANCEL.TabIndex = 9;
            this.bCANCEL.Text = "Cancel";
            this.bCANCEL.UseVisualStyleBackColor = true;
            // 
            // lbl_CountDown
            // 
            this.lbl_CountDown.AutoSize = true;
            this.lbl_CountDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_CountDown.ForeColor = System.Drawing.Color.Maroon;
            this.lbl_CountDown.Location = new System.Drawing.Point(72, 98);
            this.lbl_CountDown.Name = "lbl_CountDown";
            this.lbl_CountDown.Size = new System.Drawing.Size(154, 42);
            this.lbl_CountDown.TabIndex = 10;
            this.lbl_CountDown.Text = "-- --: -- --";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(17, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Pausing For:";
            // 
            // pnl_timer
            // 
            this.pnl_timer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnl_timer.Controls.Add(this.label4);
            this.pnl_timer.Controls.Add(this.lbl_CountDown);
            this.pnl_timer.Controls.Add(this.label5);
            this.pnl_timer.Location = new System.Drawing.Point(16, 12);
            this.pnl_timer.Name = "pnl_timer";
            this.pnl_timer.Size = new System.Drawing.Size(329, 184);
            this.pnl_timer.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(249, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "minutes";
            // 
            // Pause_Time
            // 
            this.AcceptButton = this.bOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCANCEL;
            this.ClientSize = new System.Drawing.Size(367, 209);
            this.Controls.Add(this.pnl_timer);
            this.Controls.Add(this.bCANCEL);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbHowLong);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Pause_Time";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pause Time";
            this.pnl_timer.ResumeLayout(false);
            this.pnl_timer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox cbHowLong;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCANCEL;
        private System.Windows.Forms.Label lbl_CountDown;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnl_timer;
        private System.Windows.Forms.Label label4;
    }
}