namespace SeNA80
{
    partial class Pressuriz
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pressuriz));
            this.pStatusBox = new System.Windows.Forms.TextBox();
            this.lblpStatus = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbReagents2 = new System.Windows.Forms.RadioButton();
            this.rbReagents1 = new System.Windows.Forms.RadioButton();
            this.rbAmidites2 = new System.Windows.Forms.RadioButton();
            this.rbaAmidites1 = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pStatusBox
            // 
            this.pStatusBox.Location = new System.Drawing.Point(12, 249);
            this.pStatusBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pStatusBox.Multiline = true;
            this.pStatusBox.Name = "pStatusBox";
            this.pStatusBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.pStatusBox.Size = new System.Drawing.Size(597, 92);
            this.pStatusBox.TabIndex = 0;
            // 
            // lblpStatus
            // 
            this.lblpStatus.AutoSize = true;
            this.lblpStatus.Location = new System.Drawing.Point(20, 226);
            this.lblpStatus.Name = "lblpStatus";
            this.lblpStatus.Size = new System.Drawing.Size(41, 15);
            this.lblpStatus.TabIndex = 1;
            this.lblpStatus.Text = "Status";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbReagents2);
            this.groupBox1.Controls.Add(this.rbReagents1);
            this.groupBox1.Controls.Add(this.rbAmidites2);
            this.groupBox1.Controls.Add(this.rbaAmidites1);
            this.groupBox1.Controls.Add(this.rbAll);
            this.groupBox1.Location = new System.Drawing.Point(30, 50);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(545, 72);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pressurize";
            this.groupBox1.MouseHover += new System.EventHandler(this.groupBox1_MouseHover);
            // 
            // rbReagents2
            // 
            this.rbReagents2.AutoSize = true;
            this.rbReagents2.Location = new System.Drawing.Point(412, 32);
            this.rbReagents2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbReagents2.Name = "rbReagents2";
            this.rbReagents2.Size = new System.Drawing.Size(88, 19);
            this.rbReagents2.TabIndex = 4;
            this.rbReagents2.TabStop = true;
            this.rbReagents2.Text = "Rea&gents 2";
            this.rbReagents2.UseVisualStyleBackColor = true;
            // 
            // rbReagents1
            // 
            this.rbReagents1.AutoSize = true;
            this.rbReagents1.Location = new System.Drawing.Point(311, 32);
            this.rbReagents1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbReagents1.Name = "rbReagents1";
            this.rbReagents1.Size = new System.Drawing.Size(88, 19);
            this.rbReagents1.TabIndex = 3;
            this.rbReagents1.TabStop = true;
            this.rbReagents1.Text = "&Reagents 1";
            this.rbReagents1.UseVisualStyleBackColor = true;
            // 
            // rbAmidites2
            // 
            this.rbAmidites2.AutoSize = true;
            this.rbAmidites2.Location = new System.Drawing.Point(204, 32);
            this.rbAmidites2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbAmidites2.Name = "rbAmidites2";
            this.rbAmidites2.Size = new System.Drawing.Size(82, 19);
            this.rbAmidites2.TabIndex = 2;
            this.rbAmidites2.TabStop = true;
            this.rbAmidites2.Text = "A&midites 2";
            this.rbAmidites2.UseVisualStyleBackColor = true;
            // 
            // rbaAmidites1
            // 
            this.rbaAmidites1.AutoSize = true;
            this.rbaAmidites1.Location = new System.Drawing.Point(97, 32);
            this.rbaAmidites1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbaAmidites1.Name = "rbaAmidites1";
            this.rbaAmidites1.Size = new System.Drawing.Size(82, 19);
            this.rbaAmidites1.TabIndex = 1;
            this.rbaAmidites1.TabStop = true;
            this.rbaAmidites1.Text = "&Amidites 1";
            this.rbaAmidites1.UseVisualStyleBackColor = true;
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Checked = true;
            this.rbAll.Location = new System.Drawing.Point(29, 32);
            this.rbAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(38, 19);
            this.rbAll.TabIndex = 0;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "A&ll";
            this.rbAll.UseVisualStyleBackColor = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(621, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Help,
            this.toolStripSeparator1,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // Menu_Help
            // 
            this.Menu_Help.Name = "Menu_Help";
            this.Menu_Help.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.Menu_Help.Size = new System.Drawing.Size(118, 22);
            this.Menu_Help.Text = "&Help";
            this.Menu_Help.Click += new System.EventHandler(this.Menu_Help_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(115, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(98, 151);
            this.btnStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(160, 40);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "&Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            this.btnStart.MouseHover += new System.EventHandler(this.btnStart_MouseHover);
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnExit.Location = new System.Drawing.Point(332, 151);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(160, 40);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.MouseHover += new System.EventHandler(this.btnExit_MouseHover);
            // 
            // Pressuriz
            // 
            this.AcceptButton = this.btnStart;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(621, 368);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblpStatus);
            this.Controls.Add(this.pStatusBox);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Pressuriz";
            this.Text = "Pressurize Reagents";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblpStatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbReagents2;
        private System.Windows.Forms.RadioButton rbReagents1;
        private System.Windows.Forms.RadioButton rbAmidites2;
        private System.Windows.Forms.RadioButton rbaAmidites1;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnExit;
        public System.Windows.Forms.TextBox pStatusBox;
        private System.Windows.Forms.ToolStripMenuItem Menu_Help;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}