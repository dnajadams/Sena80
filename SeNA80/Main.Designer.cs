namespace SeNA80
{
    partial class Main_Form
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_Form));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblManCtl = new System.Windows.Forms.Label();
            this.btnManCtl = new System.Windows.Forms.Button();
            this.lblAmCon = new System.Windows.Forms.Label();
            this.btnAmConf = new System.Windows.Forms.Button();
            this.lblPress = new System.Windows.Forms.Label();
            this.btnPress = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblRun = new System.Windows.Forms.Label();
            this.btnRun = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.View_Log_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Exit_Menu_Item = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Config = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_BaseTable = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.Loggin_On_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.View_TritylSC = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_CellsOnOff = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.Menu_Tips = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblSeqEd = new System.Windows.Forms.Label();
            this.btnSchedEd = new System.Windows.Forms.Button();
            this.lblMetEd = new System.Windows.Forms.Label();
            this.btnMetEd = new System.Windows.Forms.Button();
            Main_Arduino = new System.IO.Ports.SerialPort(this.components);
            UV_Trityl_Arduino = new System.IO.Ports.SerialPort(this.components);
            this.pnl_Run = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.pnl_Run.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.lblManCtl);
            this.groupBox1.Controls.Add(this.btnManCtl);
            this.groupBox1.Controls.Add(this.lblAmCon);
            this.groupBox1.Controls.Add(this.btnAmConf);
            this.groupBox1.Controls.Add(this.lblPress);
            this.groupBox1.Controls.Add(this.btnPress);
            this.groupBox1.Location = new System.Drawing.Point(10, 9);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(581, 205);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Preparat&ion";
            // 
            // lblManCtl
            // 
            this.lblManCtl.AutoSize = true;
            this.lblManCtl.Location = new System.Drawing.Point(413, 161);
            this.lblManCtl.Name = "lblManCtl";
            this.lblManCtl.Size = new System.Drawing.Size(91, 15);
            this.lblManCtl.TabIndex = 7;
            this.lblManCtl.Text = "Manual &Control";
            // 
            // btnManCtl
            // 
            this.btnManCtl.BackgroundImage = global::SeNA80.Properties.Resources.process;
            this.btnManCtl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnManCtl.ForeColor = System.Drawing.Color.Transparent;
            this.btnManCtl.Location = new System.Drawing.Point(393, 25);
            this.btnManCtl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnManCtl.Name = "btnManCtl";
            this.btnManCtl.Size = new System.Drawing.Size(164, 128);
            this.btnManCtl.TabIndex = 6;
            this.btnManCtl.Text = "&c";
            this.btnManCtl.UseVisualStyleBackColor = true;
            this.btnManCtl.Click += new System.EventHandler(this.btnManCtl_Click);
            this.btnManCtl.MouseHover += new System.EventHandler(this.btnManCtl_MouseHover);
            // 
            // lblAmCon
            // 
            this.lblAmCon.AutoSize = true;
            this.lblAmCon.Location = new System.Drawing.Point(224, 161);
            this.lblAmCon.Name = "lblAmCon";
            this.lblAmCon.Size = new System.Drawing.Size(86, 15);
            this.lblAmCon.TabIndex = 3;
            this.lblAmCon.Text = "&Amidite Config";
            // 
            // btnAmConf
            // 
            this.btnAmConf.BackgroundImage = global::SeNA80.Properties.Resources.DNA_btls;
            this.btnAmConf.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAmConf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnAmConf.Location = new System.Drawing.Point(197, 25);
            this.btnAmConf.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAmConf.Name = "btnAmConf";
            this.btnAmConf.Size = new System.Drawing.Size(164, 128);
            this.btnAmConf.TabIndex = 2;
            this.btnAmConf.UseVisualStyleBackColor = true;
            this.btnAmConf.Click += new System.EventHandler(this.btnAmConf_Click);
            this.btnAmConf.MouseHover += new System.EventHandler(this.btnAmConf_MouseHover);
            // 
            // lblPress
            // 
            this.lblPress.AutoSize = true;
            this.lblPress.Location = new System.Drawing.Point(44, 161);
            this.lblPress.Name = "lblPress";
            this.lblPress.Size = new System.Drawing.Size(65, 15);
            this.lblPress.TabIndex = 1;
            this.lblPress.Text = "&Pressurize";
            // 
            // btnPress
            // 
            this.btnPress.BackgroundImage = global::SeNA80.Properties.Resources.Ballon_90;
            this.btnPress.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPress.ForeColor = System.Drawing.Color.CadetBlue;
            this.btnPress.Location = new System.Drawing.Point(16, 25);
            this.btnPress.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnPress.Name = "btnPress";
            this.btnPress.Size = new System.Drawing.Size(164, 128);
            this.btnPress.TabIndex = 0;
            this.btnPress.Text = "&p";
            this.btnPress.UseVisualStyleBackColor = true;
            this.btnPress.Click += new System.EventHandler(this.btnPress_Click);
            this.btnPress.MouseHover += new System.EventHandler(this.btnPress_MouseHover);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.lblRun);
            this.groupBox2.Controls.Add(this.btnRun);
            this.groupBox2.Location = new System.Drawing.Point(7, 9);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(271, 205);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "&Run";
            // 
            // lblRun
            // 
            this.lblRun.AutoSize = true;
            this.lblRun.Location = new System.Drawing.Point(129, 161);
            this.lblRun.Name = "lblRun";
            this.lblRun.Size = new System.Drawing.Size(30, 15);
            this.lblRun.TabIndex = 9;
            this.lblRun.Text = "Ru&n";
            // 
            // btnRun
            // 
            this.btnRun.BackgroundImage = global::SeNA80.Properties.Resources.RunningMan;
            this.btnRun.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRun.ForeColor = System.Drawing.Color.Black;
            this.btnRun.Location = new System.Drawing.Point(61, 25);
            this.btnRun.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(164, 128);
            this.btnRun.TabIndex = 8;
            this.btnRun.Text = "&r";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            this.btnRun.MouseHover += new System.EventHandler(this.btnRun_MouseHover);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(997, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.View_Log_MenuItem,
            this.toolStripSeparator2,
            this.Exit_Menu_Item});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // View_Log_MenuItem
            // 
            this.View_Log_MenuItem.Name = "View_Log_MenuItem";
            this.View_Log_MenuItem.Size = new System.Drawing.Size(143, 22);
            this.View_Log_MenuItem.Text = "View &Log File";
            this.View_Log_MenuItem.Click += new System.EventHandler(this.View_Log_MenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(140, 6);
            // 
            // Exit_Menu_Item
            // 
            this.Exit_Menu_Item.Name = "Exit_Menu_Item";
            this.Exit_Menu_Item.Size = new System.Drawing.Size(143, 22);
            this.Exit_Menu_Item.Text = "E&xit";
            this.Exit_Menu_Item.Click += new System.EventHandler(this.Exit_Menu_Item_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Config,
            this.Menu_BaseTable,
            this.toolStripSeparator4,
            this.Loggin_On_MenuItem,
            this.toolStripSeparator3,
            this.View_TritylSC,
            this.Menu_CellsOnOff,
            this.toolStripSeparator5,
            this.Menu_Tips});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // Menu_Config
            // 
            this.Menu_Config.Name = "Menu_Config";
            this.Menu_Config.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.Menu_Config.Size = new System.Drawing.Size(190, 22);
            this.Menu_Config.Text = "&Configuration";
            this.Menu_Config.Click += new System.EventHandler(this.Menu_Config_Click);
            // 
            // Menu_BaseTable
            // 
            this.Menu_BaseTable.Name = "Menu_BaseTable";
            this.Menu_BaseTable.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.Menu_BaseTable.Size = new System.Drawing.Size(190, 22);
            this.Menu_BaseTable.Text = "&Base Table";
            this.Menu_BaseTable.ToolTipText = "Edit the System Base Table";
            this.Menu_BaseTable.Click += new System.EventHandler(this.Menu_BaseTable_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(187, 6);
            // 
            // Loggin_On_MenuItem
            // 
            this.Loggin_On_MenuItem.AutoToolTip = true;
            this.Loggin_On_MenuItem.Checked = true;
            this.Loggin_On_MenuItem.CheckOnClick = true;
            this.Loggin_On_MenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Loggin_On_MenuItem.Name = "Loggin_On_MenuItem";
            this.Loggin_On_MenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.Loggin_On_MenuItem.Size = new System.Drawing.Size(190, 22);
            this.Loggin_On_MenuItem.Text = "Logging";
            this.Loggin_On_MenuItem.ToolTipText = "&Logging";
            this.Loggin_On_MenuItem.Click += new System.EventHandler(this.Loggin_On_MenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(187, 6);
            // 
            // View_TritylSC
            // 
            this.View_TritylSC.Enabled = false;
            this.View_TritylSC.Name = "View_TritylSC";
            this.View_TritylSC.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.View_TritylSC.Size = new System.Drawing.Size(190, 22);
            this.View_TritylSC.Text = "View &Trityl";
            this.View_TritylSC.Visible = false;
            this.View_TritylSC.Click += new System.EventHandler(this.View_TritylSC_Click);
            // 
            // Menu_CellsOnOff
            // 
            this.Menu_CellsOnOff.Name = "Menu_CellsOnOff";
            this.Menu_CellsOnOff.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.Menu_CellsOnOff.Size = new System.Drawing.Size(190, 22);
            this.Menu_CellsOnOff.Text = "Turn Cells O&n";
            this.Menu_CellsOnOff.Click += new System.EventHandler(this.Menu_CellsOnOff_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(187, 6);
            // 
            // Menu_Tips
            // 
            this.Menu_Tips.Checked = true;
            this.Menu_Tips.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Menu_Tips.Name = "Menu_Tips";
            this.Menu_Tips.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.Menu_Tips.Size = new System.Drawing.Size(190, 22);
            this.Menu_Tips.Text = "&Disable Tips";
            this.Menu_Tips.Click += new System.EventHandler(this.Menu_Tips_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1,
            this.toolStripSeparator1,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(118, 22);
            this.helpToolStripMenuItem1.Text = "He&lp";
            this.helpToolStripMenuItem1.Click += new System.EventHandler(this.helpToolStripMenuItem1_Click);
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
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add(this.lblSeqEd);
            this.groupBox3.Controls.Add(this.btnSchedEd);
            this.groupBox3.Controls.Add(this.lblMetEd);
            this.groupBox3.Controls.Add(this.btnMetEd);
            this.groupBox3.Location = new System.Drawing.Point(14, 11);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(528, 205);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "&Editors";
            // 
            // lblSeqEd
            // 
            this.lblSeqEd.AutoSize = true;
            this.lblSeqEd.Location = new System.Drawing.Point(333, 164);
            this.lblSeqEd.Name = "lblSeqEd";
            this.lblSeqEd.Size = new System.Drawing.Size(98, 15);
            this.lblSeqEd.TabIndex = 7;
            this.lblSeqEd.Text = "Se&quence Editor";
            // 
            // btnSchedEd
            // 
            this.btnSchedEd.BackgroundImage = global::SeNA80.Properties.Resources.DNA_Sequence;
            this.btnSchedEd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSchedEd.Location = new System.Drawing.Point(301, 27);
            this.btnSchedEd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSchedEd.Name = "btnSchedEd";
            this.btnSchedEd.Size = new System.Drawing.Size(164, 128);
            this.btnSchedEd.TabIndex = 6;
            this.btnSchedEd.Text = "&q";
            this.btnSchedEd.UseVisualStyleBackColor = true;
            this.btnSchedEd.Click += new System.EventHandler(this.btnSchedEd_Click);
            this.btnSchedEd.MouseHover += new System.EventHandler(this.btnSchedEd_MouseHover);
            // 
            // lblMetEd
            // 
            this.lblMetEd.AutoSize = true;
            this.lblMetEd.Location = new System.Drawing.Point(130, 164);
            this.lblMetEd.Name = "lblMetEd";
            this.lblMetEd.Size = new System.Drawing.Size(80, 15);
            this.lblMetEd.TabIndex = 5;
            this.lblMetEd.Text = "Prtocol &Editor";
            // 
            // btnMetEd
            // 
            this.btnMetEd.BackgroundImage = global::SeNA80.Properties.Resources.recipie_book;
            this.btnMetEd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMetEd.ForeColor = System.Drawing.Color.Transparent;
            this.btnMetEd.Location = new System.Drawing.Point(84, 27);
            this.btnMetEd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnMetEd.Name = "btnMetEd";
            this.btnMetEd.Size = new System.Drawing.Size(164, 128);
            this.btnMetEd.TabIndex = 4;
            this.btnMetEd.UseVisualStyleBackColor = true;
            this.btnMetEd.Click += new System.EventHandler(this.btnMetEd_Click);
            this.btnMetEd.MouseHover += new System.EventHandler(this.btnMetEd_MouseHover);
            // 
            // Main_Arduino
            // 
            Main_Arduino.BaudRate = 38400;
            Main_Arduino.PortName = "COM12";
            Main_Arduino.WriteBufferSize = 4096;
            // 
            // UV_Trityl_Arduino
            // 
            UV_Trityl_Arduino.BaudRate = 38400;
            UV_Trityl_Arduino.PortName = "COM13";
            UV_Trityl_Arduino.WriteBufferSize = 4096;
            UV_Trityl_Arduino.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(Trityl_DataProcess);
            // 
            // pnl_Run
            // 
            this.pnl_Run.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.pnl_Run.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnl_Run.Controls.Add(this.groupBox2);
            this.pnl_Run.Location = new System.Drawing.Point(638, 56);
            this.pnl_Run.Name = "pnl_Run";
            this.pnl_Run.Size = new System.Drawing.Size(291, 229);
            this.pnl_Run.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Location = new System.Drawing.Point(213, 297);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(557, 229);
            this.panel1.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Location = new System.Drawing.Point(28, 56);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(601, 228);
            this.panel2.TabIndex = 11;
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 538);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnl_Run);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Main_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SeNA80 Oligo Synthesizer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_Form_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.pnl_Run.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPress;
        private System.Windows.Forms.Label lblAmCon;
        private System.Windows.Forms.Button btnAmConf;
        private System.Windows.Forms.Label lblPress;
        private System.Windows.Forms.Label lblManCtl;
        private System.Windows.Forms.Button btnManCtl;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblRun;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblSeqEd;
        private System.Windows.Forms.Button btnSchedEd;
        private System.Windows.Forms.Label lblMetEd;
        private System.Windows.Forms.Button btnMetEd;
        private System.Windows.Forms.ToolStripMenuItem View_Log_MenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem Exit_Menu_Item;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Loggin_On_MenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem Menu_Config;
        private System.Windows.Forms.ToolStripMenuItem View_TritylSC;
        private System.Windows.Forms.ToolStripMenuItem Menu_CellsOnOff;
        private System.Windows.Forms.ToolStripMenuItem Menu_BaseTable;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Panel pnl_Run;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem Menu_Tips;
        public static System.IO.Ports.SerialPort Main_Arduino;
        public static System.IO.Ports.SerialPort UV_Trityl_Arduino;
    }
}

