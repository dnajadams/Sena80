namespace SeNA80
{
    partial class Protocol_Editor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Protocol_Editor));
            this.b_mv_up = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.New_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.File_Open_Menuitem = new System.Windows.Forms.ToolStripMenuItem();
            this.Save_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Exit_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbStart = new System.Windows.Forms.RadioButton();
            this.rbThio = new System.Windows.Forms.RadioButton();
            this.rbOx = new System.Windows.Forms.RadioButton();
            this.rbMainLoop = new System.Windows.Forms.RadioButton();
            this.rbCap = new System.Windows.Forms.RadioButton();
            this.rbDeblock = new System.Windows.Forms.RadioButton();
            this.rbAmidite = new System.Windows.Forms.RadioButton();
            this.rbPost = new System.Windows.Forms.RadioButton();
            this.rbPrep = new System.Windows.Forms.RadioButton();
            this.lbl_MetOptns = new System.Windows.Forms.Label();
            this.l_Param = new System.Windows.Forms.Label();
            this.l_Cmd = new System.Windows.Forms.Label();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_New = new System.Windows.Forms.Button();
            this.btn_Open = new System.Windows.Forms.Button();
            this.Met_List_Items = new System.Windows.Forms.Label();
            this.b_param_edit = new System.Windows.Forms.Button();
            this.b_rmv = new System.Windows.Forms.Button();
            this.b_mv_dn = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.Met_List = new System.Windows.Forms.ListView();
            this.Command = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Parameter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Met_Optns = new System.Windows.Forms.ListView();
            this.Cmds = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Params = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // b_mv_up
            // 
            this.b_mv_up.BackgroundImage = global::SeNA80.Properties.Resources.up;
            this.b_mv_up.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.b_mv_up.Location = new System.Drawing.Point(933, 270);
            this.b_mv_up.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.b_mv_up.Name = "b_mv_up";
            this.b_mv_up.Size = new System.Drawing.Size(26, 31);
            this.b_mv_up.TabIndex = 5;
            this.b_mv_up.UseVisualStyleBackColor = true;
            this.b_mv_up.Click += new System.EventHandler(this.b_mv_up_Click);
            this.b_mv_up.MouseHover += new System.EventHandler(this.b_mv_up_MouseHover);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1029, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.New_MenuItem,
            this.File_Open_Menuitem,
            this.Save_MenuItem,
            this.toolStripSeparator1,
            this.Exit_MenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // New_MenuItem
            // 
            this.New_MenuItem.Name = "New_MenuItem";
            this.New_MenuItem.Size = new System.Drawing.Size(103, 22);
            this.New_MenuItem.Text = "&New";
            this.New_MenuItem.Click += new System.EventHandler(this.New_MenuItem_Click);
            // 
            // File_Open_Menuitem
            // 
            this.File_Open_Menuitem.Name = "File_Open_Menuitem";
            this.File_Open_Menuitem.Size = new System.Drawing.Size(103, 22);
            this.File_Open_Menuitem.Text = "&Open";
            this.File_Open_Menuitem.Click += new System.EventHandler(this.File_Open_Menuitem_Click);
            // 
            // Save_MenuItem
            // 
            this.Save_MenuItem.Name = "Save_MenuItem";
            this.Save_MenuItem.Size = new System.Drawing.Size(103, 22);
            this.Save_MenuItem.Text = "&Save";
            this.Save_MenuItem.Click += new System.EventHandler(this.Save_MenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(100, 6);
            // 
            // Exit_MenuItem
            // 
            this.Exit_MenuItem.Name = "Exit_MenuItem";
            this.Exit_MenuItem.Size = new System.Drawing.Size(103, 22);
            this.Exit_MenuItem.Text = "E&xit";
            this.Exit_MenuItem.Click += new System.EventHandler(this.Exit_MenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Help,
            this.toolStripSeparator2,
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(115, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.aboutToolStripMenuItem.Text = "&About ";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbStart);
            this.groupBox1.Controls.Add(this.rbThio);
            this.groupBox1.Controls.Add(this.rbOx);
            this.groupBox1.Controls.Add(this.rbMainLoop);
            this.groupBox1.Controls.Add(this.rbCap);
            this.groupBox1.Controls.Add(this.rbDeblock);
            this.groupBox1.Controls.Add(this.rbAmidite);
            this.groupBox1.Controls.Add(this.rbPost);
            this.groupBox1.Controls.Add(this.rbPrep);
            this.groupBox1.Location = new System.Drawing.Point(29, 71);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(970, 88);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "&Type of Protocol";
            // 
            // rbStart
            // 
            this.rbStart.AutoSize = true;
            this.rbStart.Checked = true;
            this.rbStart.Location = new System.Drawing.Point(20, 42);
            this.rbStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbStart.Name = "rbStart";
            this.rbStart.Size = new System.Drawing.Size(50, 19);
            this.rbStart.TabIndex = 8;
            this.rbStart.TabStop = true;
            this.rbStart.Text = "Sta&rt";
            this.rbStart.UseVisualStyleBackColor = true;
            this.rbStart.CheckedChanged += new System.EventHandler(this.rbStart_CheckedChanged);
            this.rbStart.MouseHover += new System.EventHandler(this.rbStart_MouseHover);
            // 
            // rbThio
            // 
            this.rbThio.AutoSize = true;
            this.rbThio.Location = new System.Drawing.Point(759, 42);
            this.rbThio.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbThio.Name = "rbThio";
            this.rbThio.Size = new System.Drawing.Size(79, 19);
            this.rbThio.TabIndex = 7;
            this.rbThio.Text = "&Thiolation";
            this.rbThio.UseVisualStyleBackColor = true;
            this.rbThio.CheckedChanged += new System.EventHandler(this.rbThio_CheckedChanged);
            this.rbThio.MouseHover += new System.EventHandler(this.rbThio_MouseHover);
            // 
            // rbOx
            // 
            this.rbOx.AutoSize = true;
            this.rbOx.Location = new System.Drawing.Point(646, 42);
            this.rbOx.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbOx.Name = "rbOx";
            this.rbOx.Size = new System.Drawing.Size(77, 19);
            this.rbOx.TabIndex = 5;
            this.rbOx.Text = "&Oxidation";
            this.rbOx.UseVisualStyleBackColor = true;
            this.rbOx.CheckedChanged += new System.EventHandler(this.rbOx_CheckedChanged);
            this.rbOx.MouseHover += new System.EventHandler(this.rbOx_MouseHover);
            // 
            // rbMainLoop
            // 
            this.rbMainLoop.AutoSize = true;
            this.rbMainLoop.Location = new System.Drawing.Point(865, 42);
            this.rbMainLoop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbMainLoop.Name = "rbMainLoop";
            this.rbMainLoop.Size = new System.Drawing.Size(84, 19);
            this.rbMainLoop.TabIndex = 6;
            this.rbMainLoop.Text = "&Main Loop";
            this.rbMainLoop.UseVisualStyleBackColor = true;
            this.rbMainLoop.CheckedChanged += new System.EventHandler(this.rbMainLoop_CheckedChanged);
            this.rbMainLoop.MouseHover += new System.EventHandler(this.rbMainLoop_MouseHover);
            // 
            // rbCap
            // 
            this.rbCap.AutoSize = true;
            this.rbCap.Location = new System.Drawing.Point(533, 42);
            this.rbCap.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbCap.Name = "rbCap";
            this.rbCap.Size = new System.Drawing.Size(74, 19);
            this.rbCap.TabIndex = 4;
            this.rbCap.Text = " &Capping";
            this.rbCap.UseVisualStyleBackColor = true;
            this.rbCap.CheckedChanged += new System.EventHandler(this.rbCap_CheckedChanged);
            this.rbCap.MouseHover += new System.EventHandler(this.rbCap_MouseHover);
            // 
            // rbDeblock
            // 
            this.rbDeblock.AutoSize = true;
            this.rbDeblock.Location = new System.Drawing.Point(408, 42);
            this.rbDeblock.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbDeblock.Name = "rbDeblock";
            this.rbDeblock.Size = new System.Drawing.Size(70, 19);
            this.rbDeblock.TabIndex = 3;
            this.rbDeblock.Text = "&Deblock";
            this.rbDeblock.UseVisualStyleBackColor = true;
            this.rbDeblock.CheckedChanged += new System.EventHandler(this.rbDeblock_CheckedChanged);
            this.rbDeblock.MouseHover += new System.EventHandler(this.rbDeblock_MouseHover);
            // 
            // rbAmidite
            // 
            this.rbAmidite.AutoSize = true;
            this.rbAmidite.Location = new System.Drawing.Point(292, 42);
            this.rbAmidite.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbAmidite.Name = "rbAmidite";
            this.rbAmidite.Size = new System.Drawing.Size(66, 19);
            this.rbAmidite.TabIndex = 2;
            this.rbAmidite.Text = "&Amidite";
            this.rbAmidite.UseVisualStyleBackColor = true;
            this.rbAmidite.CheckedChanged += new System.EventHandler(this.rbAmidite_CheckedChanged);
            this.rbAmidite.MouseHover += new System.EventHandler(this.rbAmidite_MouseHover);
            // 
            // rbPost
            // 
            this.rbPost.AutoSize = true;
            this.rbPost.Location = new System.Drawing.Point(188, 42);
            this.rbPost.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbPost.Name = "rbPost";
            this.rbPost.Size = new System.Drawing.Size(49, 19);
            this.rbPost.TabIndex = 1;
            this.rbPost.Text = "Po&st";
            this.rbPost.UseVisualStyleBackColor = true;
            this.rbPost.CheckedChanged += new System.EventHandler(this.rbPost_CheckedChanged);
            this.rbPost.MouseHover += new System.EventHandler(this.rbPost_MouseHover);
            // 
            // rbPrep
            // 
            this.rbPrep.AutoSize = true;
            this.rbPrep.Location = new System.Drawing.Point(99, 42);
            this.rbPrep.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbPrep.Name = "rbPrep";
            this.rbPrep.Size = new System.Drawing.Size(51, 19);
            this.rbPrep.TabIndex = 0;
            this.rbPrep.Text = "&Prep";
            this.rbPrep.UseVisualStyleBackColor = true;
            this.rbPrep.CheckedChanged += new System.EventHandler(this.rbPrep_CheckedChanged);
            this.rbPrep.MouseHover += new System.EventHandler(this.rbPrep_MouseHover);
            // 
            // lbl_MetOptns
            // 
            this.lbl_MetOptns.AutoSize = true;
            this.lbl_MetOptns.Location = new System.Drawing.Point(137, 205);
            this.lbl_MetOptns.Name = "lbl_MetOptns";
            this.lbl_MetOptns.Size = new System.Drawing.Size(97, 15);
            this.lbl_MetOptns.TabIndex = 3;
            this.lbl_MetOptns.Text = "Protocol Options";
            // 
            // l_Param
            // 
            this.l_Param.AutoSize = true;
            this.l_Param.Location = new System.Drawing.Point(713, 205);
            this.l_Param.Name = "l_Param";
            this.l_Param.Size = new System.Drawing.Size(65, 15);
            this.l_Param.TabIndex = 17;
            this.l_Param.Text = "Parameter";
            // 
            // l_Cmd
            // 
            this.l_Cmd.AutoSize = true;
            this.l_Cmd.Location = new System.Drawing.Point(589, 205);
            this.l_Cmd.Name = "l_Cmd";
            this.l_Cmd.Size = new System.Drawing.Size(71, 15);
            this.l_Cmd.TabIndex = 16;
            this.l_Cmd.Text = "Commands";
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(759, 522);
            this.btn_OK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(180, 44);
            this.btn_OK.TabIndex = 12;
            this.btn_OK.Text = "E&xit";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            this.btn_OK.MouseHover += new System.EventHandler(this.btn_OK_MouseHover);
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(551, 522);
            this.btn_Save.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(180, 44);
            this.btn_Save.TabIndex = 11;
            this.btn_Save.Text = "Sa&ve Protocol";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            this.btn_Save.MouseHover += new System.EventHandler(this.btn_Save_MouseHover);
            // 
            // btn_New
            // 
            this.btn_New.Location = new System.Drawing.Point(133, 522);
            this.btn_New.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_New.Name = "btn_New";
            this.btn_New.Size = new System.Drawing.Size(180, 44);
            this.btn_New.TabIndex = 9;
            this.btn_New.Text = "&New Protocol";
            this.btn_New.UseVisualStyleBackColor = true;
            this.btn_New.Click += new System.EventHandler(this.btn_New_Click);
            this.btn_New.MouseHover += new System.EventHandler(this.btn_New_MouseHover);
            // 
            // btn_Open
            // 
            this.btn_Open.Location = new System.Drawing.Point(342, 522);
            this.btn_Open.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Open.Name = "btn_Open";
            this.btn_Open.Size = new System.Drawing.Size(180, 44);
            this.btn_Open.TabIndex = 10;
            this.btn_Open.Text = "&Open Protocol";
            this.btn_Open.UseVisualStyleBackColor = true;
            this.btn_Open.Click += new System.EventHandler(this.btn_Open_Click);
            this.btn_Open.MouseHover += new System.EventHandler(this.btn_Open_MouseHover);
            // 
            // Met_List_Items
            // 
            this.Met_List_Items.AutoSize = true;
            this.Met_List_Items.Location = new System.Drawing.Point(597, 486);
            this.Met_List_Items.Name = "Met_List_Items";
            this.Met_List_Items.Size = new System.Drawing.Size(10, 15);
            this.Met_List_Items.TabIndex = 18;
            this.Met_List_Items.Text = "l";
            // 
            // b_param_edit
            // 
            this.b_param_edit.BackgroundImage = global::SeNA80.Properties.Resources.P;
            this.b_param_edit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.b_param_edit.Location = new System.Drawing.Point(933, 358);
            this.b_param_edit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.b_param_edit.Name = "b_param_edit";
            this.b_param_edit.Size = new System.Drawing.Size(26, 31);
            this.b_param_edit.TabIndex = 7;
            this.b_param_edit.UseVisualStyleBackColor = true;
            this.b_param_edit.Click += new System.EventHandler(this.b_param_edit_Click);
            this.b_param_edit.MouseHover += new System.EventHandler(this.b_param_edit_MouseHover);
            // 
            // b_rmv
            // 
            this.b_rmv.BackgroundImage = global::SeNA80.Properties.Resources.X;
            this.b_rmv.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.b_rmv.Location = new System.Drawing.Point(933, 314);
            this.b_rmv.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.b_rmv.Name = "b_rmv";
            this.b_rmv.Size = new System.Drawing.Size(26, 31);
            this.b_rmv.TabIndex = 6;
            this.b_rmv.UseVisualStyleBackColor = true;
            this.b_rmv.Click += new System.EventHandler(this.b_rmv_Click);
            this.b_rmv.MouseHover += new System.EventHandler(this.b_rmv_MouseHover);
            // 
            // b_mv_dn
            // 
            this.b_mv_dn.BackgroundImage = global::SeNA80.Properties.Resources.down;
            this.b_mv_dn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.b_mv_dn.Location = new System.Drawing.Point(933, 401);
            this.b_mv_dn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.b_mv_dn.Name = "b_mv_dn";
            this.b_mv_dn.Size = new System.Drawing.Size(26, 31);
            this.b_mv_dn.TabIndex = 8;
            this.b_mv_dn.UseVisualStyleBackColor = true;
            this.b_mv_dn.Click += new System.EventHandler(this.b_mv_dn_Click);
            this.b_mv_dn.MouseHover += new System.EventHandler(this.b_mv_dn_MouseHover);
            // 
            // btn_Add
            // 
            this.btn_Add.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_Add.BackgroundImage = global::SeNA80.Properties.Resources.Move_Right1;
            this.btn_Add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Add.Location = new System.Drawing.Point(420, 314);
            this.btn_Add.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(72, 74);
            this.btn_Add.TabIndex = 3;
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            this.btn_Add.MouseHover += new System.EventHandler(this.btn_Add_MouseHover);
            // 
            // Met_List
            // 
            this.Met_List.AllowDrop = true;
            this.Met_List.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Command,
            this.Parameter});
            this.Met_List.FullRowSelect = true;
            this.Met_List.GridLines = true;
            this.Met_List.Location = new System.Drawing.Point(577, 235);
            this.Met_List.MultiSelect = false;
            this.Met_List.Name = "Met_List";
            this.Met_List.Size = new System.Drawing.Size(333, 230);
            this.Met_List.TabIndex = 4;
            this.Met_List.UseCompatibleStateImageBehavior = false;
            this.Met_List.View = System.Windows.Forms.View.Details;
            this.Met_List.DragDrop += new System.Windows.Forms.DragEventHandler(this.Met_List_DragDrop);
            this.Met_List.DragEnter += new System.Windows.Forms.DragEventHandler(this.Met_List_DragEnter);
            this.Met_List.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.Met_List_QueryContinueDrag);
            this.Met_List.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Met_List_MouseDoubleClick);
            this.Met_List.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Met_List_MouseDown);
            this.Met_List.MouseHover += new System.EventHandler(this.Met_List_MouseHover);
            this.Met_List.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Met_List_MouseMove);
            this.Met_List.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Met_List_MouseUp);
            // 
            // Command
            // 
            this.Command.Text = "Command";
            this.Command.Width = 125;
            // 
            // Parameter
            // 
            this.Parameter.Text = "Parameter";
            this.Parameter.Width = 218;
            // 
            // Met_Optns
            // 
            this.Met_Optns.AllowDrop = true;
            this.Met_Optns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Cmds,
            this.Params});
            this.Met_Optns.FullRowSelect = true;
            this.Met_Optns.GridLines = true;
            this.Met_Optns.Location = new System.Drawing.Point(128, 235);
            this.Met_Optns.Name = "Met_Optns";
            this.Met_Optns.Size = new System.Drawing.Size(232, 230);
            this.Met_Optns.TabIndex = 2;
            this.Met_Optns.UseCompatibleStateImageBehavior = false;
            this.Met_Optns.View = System.Windows.Forms.View.Details;
            this.Met_Optns.DragDrop += new System.Windows.Forms.DragEventHandler(this.Met_Optns_DragDrop);
            this.Met_Optns.DragEnter += new System.Windows.Forms.DragEventHandler(this.Met_Optns_DragEnter);
            this.Met_Optns.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.Met_Optns_QueryContinueDrag);
            this.Met_Optns.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Met_Optns_KeyDown);
            this.Met_Optns.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Met_Optns_MouseDown);
            this.Met_Optns.MouseHover += new System.EventHandler(this.Met_Optns_MouseHover);
            this.Met_Optns.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Met_Optns_MouseMove);
            this.Met_Optns.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Met_Optns_MouseUp);
            // 
            // Cmds
            // 
            this.Cmds.Text = "Commands";
            this.Cmds.Width = 226;
            // 
            // Params
            // 
            this.Params.Text = "Parameters";
            this.Params.Width = 0;
            // 
            // Protocol_Editor
            // 
            this.AcceptButton = this.btn_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 602);
            this.Controls.Add(this.Met_Optns);
            this.Controls.Add(this.Met_List);
            this.Controls.Add(this.Met_List_Items);
            this.Controls.Add(this.b_param_edit);
            this.Controls.Add(this.b_rmv);
            this.Controls.Add(this.b_mv_dn);
            this.Controls.Add(this.b_mv_up);
            this.Controls.Add(this.btn_Open);
            this.Controls.Add(this.btn_New);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.l_Param);
            this.Controls.Add(this.l_Cmd);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.lbl_MetOptns);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Protocol_Editor";
            this.Text = "Synthesis Protocol Editor";
            this.Load += new System.EventHandler(this.Protocol_editor_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Protocol_Editor_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem New_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem File_Open_Menuitem;
        private System.Windows.Forms.ToolStripMenuItem Save_MenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem Exit_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_MetOptns;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Label l_Param;
        private System.Windows.Forms.Label l_Cmd;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_New;
        private System.Windows.Forms.Button btn_Open;
        private System.Windows.Forms.Button b_param_edit;
        private System.Windows.Forms.Button b_rmv;
        private System.Windows.Forms.Button b_mv_dn;
        private System.Windows.Forms.Label Met_List_Items;
        public System.Windows.Forms.RadioButton rbMainLoop;
        public System.Windows.Forms.RadioButton rbCap;
        public System.Windows.Forms.RadioButton rbDeblock;
        public System.Windows.Forms.RadioButton rbAmidite;
        public System.Windows.Forms.RadioButton rbPost;
        public System.Windows.Forms.RadioButton rbPrep;
        public System.Windows.Forms.RadioButton rbOx;
        private System.Windows.Forms.ToolStripMenuItem Menu_Help;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public System.Windows.Forms.ListView Met_Optns;
        public System.Windows.Forms.ColumnHeader Cmds;
        public System.Windows.Forms.ListView Met_List;
        public System.Windows.Forms.ColumnHeader Command;
        public System.Windows.Forms.ColumnHeader Parameter;
        public System.Windows.Forms.ColumnHeader Params;
        public System.Windows.Forms.RadioButton rbThio;
        public System.Windows.Forms.RadioButton rbStart;
        public System.Windows.Forms.Button b_mv_up;
    }
}