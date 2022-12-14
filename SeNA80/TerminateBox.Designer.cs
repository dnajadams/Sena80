namespace SeNA80
{
    partial class TerminateBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TerminateBox));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_Immediately = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_EndofCycle = new System.Windows.Forms.Button();
            this.btn_EndofStep = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(96, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(556, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Are You Sure You Wish to Terminate the Run in Progress?";
            // 
            // label2
            // 
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(182, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(413, 50);
            this.label2.TabIndex = 1;
            this.label2.Text = "From the Options Below select when you would like to Terminate the current Run..." +
    "";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(182, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(403, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "You can press Cancel to Abandon Abort...";
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::SeNA80.Properties.Resources.Warning_sign;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(12, 62);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(125, 99);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::SeNA80.Properties.Resources.Warning_sign;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Location = new System.Drawing.Point(624, 62);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(125, 99);
            this.panel2.TabIndex = 4;
            // 
            // btn_Immediately
            // 
            this.btn_Immediately.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.btn_Immediately.ForeColor = System.Drawing.Color.Red;
            this.btn_Immediately.Location = new System.Drawing.Point(46, 19);
            this.btn_Immediately.Name = "btn_Immediately";
            this.btn_Immediately.Size = new System.Drawing.Size(115, 35);
            this.btn_Immediately.TabIndex = 5;
            this.btn_Immediately.Text = "&Immediately";
            this.btn_Immediately.UseVisualStyleBackColor = true;
            this.btn_Immediately.Click += new System.EventHandler(this.btn_Immediately_Click);
            this.btn_Immediately.MouseHover += new System.EventHandler(this.btn_Immediately_MouseHover);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_Cancel);
            this.groupBox1.Controls.Add(this.btn_EndofCycle);
            this.groupBox1.Controls.Add(this.btn_EndofStep);
            this.groupBox1.Controls.Add(this.btn_Immediately);
            this.groupBox1.Location = new System.Drawing.Point(12, 183);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(737, 66);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Cancel.Location = new System.Drawing.Point(565, 19);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(115, 35);
            this.btn_Cancel.TabIndex = 8;
            this.btn_Cancel.Text = "&Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.MouseHover += new System.EventHandler(this.btn_Cancel_MouseHover);
            // 
            // btn_EndofCycle
            // 
            this.btn_EndofCycle.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_EndofCycle.Location = new System.Drawing.Point(392, 19);
            this.btn_EndofCycle.Name = "btn_EndofCycle";
            this.btn_EndofCycle.Size = new System.Drawing.Size(115, 35);
            this.btn_EndofCycle.TabIndex = 7;
            this.btn_EndofCycle.Text = "End of &Cycle";
            this.btn_EndofCycle.UseVisualStyleBackColor = true;
            this.btn_EndofCycle.Click += new System.EventHandler(this.btn_EndofCycle_Click);
            this.btn_EndofCycle.MouseHover += new System.EventHandler(this.btn_EndofCycle_MouseHover);
            // 
            // btn_EndofStep
            // 
            this.btn_EndofStep.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btn_EndofStep.Location = new System.Drawing.Point(219, 19);
            this.btn_EndofStep.Name = "btn_EndofStep";
            this.btn_EndofStep.Size = new System.Drawing.Size(115, 35);
            this.btn_EndofStep.TabIndex = 6;
            this.btn_EndofStep.Text = "End of &Step";
            this.btn_EndofStep.UseVisualStyleBackColor = true;
            this.btn_EndofStep.Click += new System.EventHandler(this.btn_EndofStep_Click);
            this.btn_EndofStep.MouseHover += new System.EventHandler(this.btn_EndofStep_MouseHover);
            // 
            // TerminateBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(775, 255);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TerminateBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Terminate Run...";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Button btn_Immediately;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Button btn_Cancel;
        public System.Windows.Forms.Button btn_EndofCycle;
        public System.Windows.Forms.Button btn_EndofStep;
    }
}