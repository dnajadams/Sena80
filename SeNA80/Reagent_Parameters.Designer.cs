namespace SeNA80
{
    partial class Reagent_Parameters
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
            this.lbl_Rgnt = new System.Windows.Forms.Label();
            this.lbl_Selected = new System.Windows.Forms.Label();
            this.GrColByPass = new System.Windows.Forms.GroupBox();
            this.rbCol = new System.Windows.Forms.RadioButton();
            this.rbBypass = new System.Windows.Forms.RadioButton();
            this.gbColumns = new System.Windows.Forms.GroupBox();
            this.cbSequence = new System.Windows.Forms.CheckBox();
            this.cb_Col8 = new System.Windows.Forms.CheckBox();
            this.cb_Col7 = new System.Windows.Forms.CheckBox();
            this.cb_Col6 = new System.Windows.Forms.CheckBox();
            this.cb_Col5 = new System.Windows.Forms.CheckBox();
            this.cb_Col4 = new System.Windows.Forms.CheckBox();
            this.cb_Col3 = new System.Windows.Forms.CheckBox();
            this.cb_Col2 = new System.Windows.Forms.CheckBox();
            this.cb_Col1 = new System.Windows.Forms.CheckBox();
            this.gbPumpByPass = new System.Windows.Forms.GroupBox();
            this.rb_Pump = new System.Windows.Forms.RadioButton();
            this.rb_ByPump = new System.Windows.Forms.RadioButton();
            this.gb_PumpControl = new System.Windows.Forms.GroupBox();
            this.lbl_vol = new System.Windows.Forms.Label();
            this.nu_Volume = new System.Windows.Forms.NumericUpDown();
            this.btn_OK = new System.Windows.Forms.Button();
            this.gb_FlowControl = new System.Windows.Forms.GroupBox();
            this.lbl_sec = new System.Windows.Forms.Label();
            this.n_FlowSec = new System.Windows.Forms.NumericUpDown();
            this.GrColByPass.SuspendLayout();
            this.gbColumns.SuspendLayout();
            this.gbPumpByPass.SuspendLayout();
            this.gb_PumpControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nu_Volume)).BeginInit();
            this.gb_FlowControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.n_FlowSec)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Rgnt
            // 
            this.lbl_Rgnt.AutoSize = true;
            this.lbl_Rgnt.Location = new System.Drawing.Point(72, 30);
            this.lbl_Rgnt.Name = "lbl_Rgnt";
            this.lbl_Rgnt.Size = new System.Drawing.Size(57, 15);
            this.lbl_Rgnt.TabIndex = 0;
            this.lbl_Rgnt.Text = "Reagent:";
            // 
            // lbl_Selected
            // 
            this.lbl_Selected.AutoSize = true;
            this.lbl_Selected.Location = new System.Drawing.Point(153, 30);
            this.lbl_Selected.Name = "lbl_Selected";
            this.lbl_Selected.Size = new System.Drawing.Size(24, 15);
            this.lbl_Selected.TabIndex = 1;
            this.lbl_Selected.Text = "lab";
            // 
            // GrColByPass
            // 
            this.GrColByPass.Controls.Add(this.rbCol);
            this.GrColByPass.Controls.Add(this.rbBypass);
            this.GrColByPass.Location = new System.Drawing.Point(76, 72);
            this.GrColByPass.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GrColByPass.Name = "GrColByPass";
            this.GrColByPass.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GrColByPass.Size = new System.Drawing.Size(346, 62);
            this.GrColByPass.TabIndex = 2;
            this.GrColByPass.TabStop = false;
            this.GrColByPass.Text = "To ByPass or Co&lumn";
            // 
            // rbCol
            // 
            this.rbCol.AutoSize = true;
            this.rbCol.CausesValidation = false;
            this.rbCol.Location = new System.Drawing.Point(177, 29);
            this.rbCol.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbCol.Name = "rbCol";
            this.rbCol.Size = new System.Drawing.Size(68, 19);
            this.rbCol.TabIndex = 1;
            this.rbCol.Tag = "";
            this.rbCol.Text = "&Column";
            this.rbCol.UseVisualStyleBackColor = true;
            this.rbCol.CheckedChanged += new System.EventHandler(this.rbCol_CheckedChanged);
            // 
            // rbBypass
            // 
            this.rbBypass.AutoSize = true;
            this.rbBypass.Checked = true;
            this.rbBypass.Location = new System.Drawing.Point(50, 29);
            this.rbBypass.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbBypass.Name = "rbBypass";
            this.rbBypass.Size = new System.Drawing.Size(68, 19);
            this.rbBypass.TabIndex = 0;
            this.rbBypass.TabStop = true;
            this.rbBypass.Text = " &ByPass";
            this.rbBypass.UseVisualStyleBackColor = true;
            this.rbBypass.CheckedChanged += new System.EventHandler(this.rbBypass_CheckedChanged);
            // 
            // gbColumns
            // 
            this.gbColumns.Controls.Add(this.cbSequence);
            this.gbColumns.Controls.Add(this.cb_Col8);
            this.gbColumns.Controls.Add(this.cb_Col7);
            this.gbColumns.Controls.Add(this.cb_Col6);
            this.gbColumns.Controls.Add(this.cb_Col5);
            this.gbColumns.Controls.Add(this.cb_Col4);
            this.gbColumns.Controls.Add(this.cb_Col3);
            this.gbColumns.Controls.Add(this.cb_Col2);
            this.gbColumns.Controls.Add(this.cb_Col1);
            this.gbColumns.Enabled = false;
            this.gbColumns.Location = new System.Drawing.Point(76, 155);
            this.gbColumns.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbColumns.Name = "gbColumns";
            this.gbColumns.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbColumns.Size = new System.Drawing.Size(537, 65);
            this.gbColumns.TabIndex = 3;
            this.gbColumns.TabStop = false;
            this.gbColumns.Text = "&Column Selection";
            // 
            // cbSequence
            // 
            this.cbSequence.AutoSize = true;
            this.cbSequence.Location = new System.Drawing.Point(437, 28);
            this.cbSequence.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbSequence.Name = "cbSequence";
            this.cbSequence.Size = new System.Drawing.Size(82, 19);
            this.cbSequence.TabIndex = 9;
            this.cbSequence.Text = "Se&quence";
            this.cbSequence.UseVisualStyleBackColor = true;
            this.cbSequence.CheckedChanged += new System.EventHandler(this.cbSequence_CheckedChanged);
            // 
            // cb_Col8
            // 
            this.cb_Col8.AutoSize = true;
            this.cb_Col8.Location = new System.Drawing.Point(393, 28);
            this.cb_Col8.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cb_Col8.Name = "cb_Col8";
            this.cb_Col8.Size = new System.Drawing.Size(33, 19);
            this.cb_Col8.TabIndex = 8;
            this.cb_Col8.Text = "&8";
            this.cb_Col8.UseVisualStyleBackColor = true;
            // 
            // cb_Col7
            // 
            this.cb_Col7.AutoSize = true;
            this.cb_Col7.Location = new System.Drawing.Point(341, 28);
            this.cb_Col7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cb_Col7.Name = "cb_Col7";
            this.cb_Col7.Size = new System.Drawing.Size(33, 19);
            this.cb_Col7.TabIndex = 7;
            this.cb_Col7.Text = "&7";
            this.cb_Col7.UseVisualStyleBackColor = true;
            // 
            // cb_Col6
            // 
            this.cb_Col6.AutoSize = true;
            this.cb_Col6.Location = new System.Drawing.Point(288, 28);
            this.cb_Col6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cb_Col6.Name = "cb_Col6";
            this.cb_Col6.Size = new System.Drawing.Size(33, 19);
            this.cb_Col6.TabIndex = 6;
            this.cb_Col6.Text = "&6";
            this.cb_Col6.UseVisualStyleBackColor = true;
            // 
            // cb_Col5
            // 
            this.cb_Col5.AutoSize = true;
            this.cb_Col5.Location = new System.Drawing.Point(236, 28);
            this.cb_Col5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cb_Col5.Name = "cb_Col5";
            this.cb_Col5.Size = new System.Drawing.Size(33, 19);
            this.cb_Col5.TabIndex = 5;
            this.cb_Col5.Text = "&5";
            this.cb_Col5.UseVisualStyleBackColor = true;
            // 
            // cb_Col4
            // 
            this.cb_Col4.AutoSize = true;
            this.cb_Col4.Location = new System.Drawing.Point(183, 28);
            this.cb_Col4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cb_Col4.Name = "cb_Col4";
            this.cb_Col4.Size = new System.Drawing.Size(33, 19);
            this.cb_Col4.TabIndex = 4;
            this.cb_Col4.Text = "&4";
            this.cb_Col4.UseVisualStyleBackColor = true;
            // 
            // cb_Col3
            // 
            this.cb_Col3.AutoSize = true;
            this.cb_Col3.Location = new System.Drawing.Point(131, 28);
            this.cb_Col3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cb_Col3.Name = "cb_Col3";
            this.cb_Col3.Size = new System.Drawing.Size(33, 19);
            this.cb_Col3.TabIndex = 2;
            this.cb_Col3.Text = "&3";
            this.cb_Col3.UseVisualStyleBackColor = true;
            // 
            // cb_Col2
            // 
            this.cb_Col2.AutoSize = true;
            this.cb_Col2.Location = new System.Drawing.Point(78, 28);
            this.cb_Col2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cb_Col2.Name = "cb_Col2";
            this.cb_Col2.Size = new System.Drawing.Size(33, 19);
            this.cb_Col2.TabIndex = 1;
            this.cb_Col2.Text = "&2";
            this.cb_Col2.UseVisualStyleBackColor = true;
            // 
            // cb_Col1
            // 
            this.cb_Col1.AutoSize = true;
            this.cb_Col1.Location = new System.Drawing.Point(26, 28);
            this.cb_Col1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cb_Col1.Name = "cb_Col1";
            this.cb_Col1.Size = new System.Drawing.Size(33, 19);
            this.cb_Col1.TabIndex = 0;
            this.cb_Col1.Text = "&1";
            this.cb_Col1.UseVisualStyleBackColor = true;
            // 
            // gbPumpByPass
            // 
            this.gbPumpByPass.Controls.Add(this.rb_Pump);
            this.gbPumpByPass.Controls.Add(this.rb_ByPump);
            this.gbPumpByPass.Location = new System.Drawing.Point(76, 230);
            this.gbPumpByPass.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbPumpByPass.Name = "gbPumpByPass";
            this.gbPumpByPass.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbPumpByPass.Size = new System.Drawing.Size(346, 62);
            this.gbPumpByPass.TabIndex = 4;
            this.gbPumpByPass.TabStop = false;
            this.gbPumpByPass.Text = "To ByPass or &Pump";
            // 
            // rb_Pump
            // 
            this.rb_Pump.AutoSize = true;
            this.rb_Pump.Location = new System.Drawing.Point(177, 29);
            this.rb_Pump.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rb_Pump.Name = "rb_Pump";
            this.rb_Pump.Size = new System.Drawing.Size(58, 19);
            this.rb_Pump.TabIndex = 1;
            this.rb_Pump.Tag = "";
            this.rb_Pump.Text = "P&ump";
            this.rb_Pump.UseVisualStyleBackColor = true;
            this.rb_Pump.CheckedChanged += new System.EventHandler(this.rb_Pump_CheckedChanged);
            // 
            // rb_ByPump
            // 
            this.rb_ByPump.AutoSize = true;
            this.rb_ByPump.Checked = true;
            this.rb_ByPump.Location = new System.Drawing.Point(50, 29);
            this.rb_ByPump.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rb_ByPump.Name = "rb_ByPump";
            this.rb_ByPump.Size = new System.Drawing.Size(68, 19);
            this.rb_ByPump.TabIndex = 0;
            this.rb_ByPump.TabStop = true;
            this.rb_ByPump.Text = " B&yPass";
            this.rb_ByPump.UseVisualStyleBackColor = true;
            this.rb_ByPump.CheckedChanged += new System.EventHandler(this.rb_ByPump_CheckedChanged);
            // 
            // gb_PumpControl
            // 
            this.gb_PumpControl.Controls.Add(this.lbl_vol);
            this.gb_PumpControl.Controls.Add(this.nu_Volume);
            this.gb_PumpControl.Enabled = false;
            this.gb_PumpControl.Location = new System.Drawing.Point(364, 319);
            this.gb_PumpControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gb_PumpControl.Name = "gb_PumpControl";
            this.gb_PumpControl.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gb_PumpControl.Size = new System.Drawing.Size(248, 64);
            this.gb_PumpControl.TabIndex = 5;
            this.gb_PumpControl.TabStop = false;
            this.gb_PumpControl.Text = "Pump Con&trol";
            // 
            // lbl_vol
            // 
            this.lbl_vol.AutoSize = true;
            this.lbl_vol.Location = new System.Drawing.Point(194, 30);
            this.lbl_vol.Name = "lbl_vol";
            this.lbl_vol.Size = new System.Drawing.Size(21, 15);
            this.lbl_vol.TabIndex = 1;
            this.lbl_vol.Text = "µL";
            // 
            // nu_Volume
            // 
            this.nu_Volume.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nu_Volume.Location = new System.Drawing.Point(68, 25);
            this.nu_Volume.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nu_Volume.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nu_Volume.Name = "nu_Volume";
            this.nu_Volume.Size = new System.Drawing.Size(100, 21);
            this.nu_Volume.TabIndex = 0;
            this.nu_Volume.ThousandsSeparator = true;
            // 
            // btn_OK
            // 
            this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_OK.Location = new System.Drawing.Point(218, 406);
            this.btn_OK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(145, 41);
            this.btn_OK.TabIndex = 6;
            this.btn_OK.Text = "O.&K.";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // gb_FlowControl
            // 
            this.gb_FlowControl.Controls.Add(this.lbl_sec);
            this.gb_FlowControl.Controls.Add(this.n_FlowSec);
            this.gb_FlowControl.Location = new System.Drawing.Point(75, 319);
            this.gb_FlowControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gb_FlowControl.Name = "gb_FlowControl";
            this.gb_FlowControl.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gb_FlowControl.Size = new System.Drawing.Size(248, 64);
            this.gb_FlowControl.TabIndex = 7;
            this.gb_FlowControl.TabStop = false;
            this.gb_FlowControl.Text = "&Flow Control";
            // 
            // lbl_sec
            // 
            this.lbl_sec.AutoSize = true;
            this.lbl_sec.Location = new System.Drawing.Point(194, 30);
            this.lbl_sec.Name = "lbl_sec";
            this.lbl_sec.Size = new System.Drawing.Size(31, 15);
            this.lbl_sec.TabIndex = 1;
            this.lbl_sec.Text = "Sec.";
            // 
            // n_FlowSec
            // 
            this.n_FlowSec.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.n_FlowSec.Location = new System.Drawing.Point(68, 25);
            this.n_FlowSec.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.n_FlowSec.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.n_FlowSec.Name = "n_FlowSec";
            this.n_FlowSec.Size = new System.Drawing.Size(100, 21);
            this.n_FlowSec.TabIndex = 0;
            this.n_FlowSec.ThousandsSeparator = true;
            // 
            // Reagent_Parameters
            // 
            this.AcceptButton = this.btn_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 468);
            this.Controls.Add(this.gb_FlowControl);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.gb_PumpControl);
            this.Controls.Add(this.gbPumpByPass);
            this.Controls.Add(this.gbColumns);
            this.Controls.Add(this.GrColByPass);
            this.Controls.Add(this.lbl_Selected);
            this.Controls.Add(this.lbl_Rgnt);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Reagent_Parameters";
            this.Text = "Reagent Parameters";
            this.Load += new System.EventHandler(this.Reagent_Parameters_Load);
            this.GrColByPass.ResumeLayout(false);
            this.GrColByPass.PerformLayout();
            this.gbColumns.ResumeLayout(false);
            this.gbColumns.PerformLayout();
            this.gbPumpByPass.ResumeLayout(false);
            this.gbPumpByPass.PerformLayout();
            this.gb_PumpControl.ResumeLayout(false);
            this.gb_PumpControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nu_Volume)).EndInit();
            this.gb_FlowControl.ResumeLayout(false);
            this.gb_FlowControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.n_FlowSec)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Rgnt;
        private System.Windows.Forms.GroupBox GrColByPass;
        public System.Windows.Forms.RadioButton rbCol;
        public System.Windows.Forms.RadioButton rbBypass;
        private System.Windows.Forms.GroupBox gbColumns;
        public System.Windows.Forms.CheckBox cbSequence;
        public System.Windows.Forms.CheckBox cb_Col8;
        public System.Windows.Forms.CheckBox cb_Col7;
        public System.Windows.Forms.CheckBox cb_Col6;
        public System.Windows.Forms.CheckBox cb_Col5;
        public System.Windows.Forms.CheckBox cb_Col4;
        public System.Windows.Forms.CheckBox cb_Col3;
        public System.Windows.Forms.CheckBox cb_Col2;
        public System.Windows.Forms.CheckBox cb_Col1;
        private System.Windows.Forms.GroupBox gbPumpByPass;
        private System.Windows.Forms.Label lbl_vol;
        public System.Windows.Forms.NumericUpDown nu_Volume;
        private System.Windows.Forms.Button btn_OK;
        public System.Windows.Forms.GroupBox gb_FlowControl;
        private System.Windows.Forms.Label lbl_sec;
        public System.Windows.Forms.NumericUpDown n_FlowSec;
        public System.Windows.Forms.Label lbl_Selected;
        public System.Windows.Forms.GroupBox gb_PumpControl;
        public System.Windows.Forms.RadioButton rb_Pump;
        public System.Windows.Forms.RadioButton rb_ByPump;
    }
}