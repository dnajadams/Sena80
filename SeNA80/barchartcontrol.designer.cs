namespace SeNA80
{
    partial class barchartcontrol
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(barchartcontrol));
            this.zb1 = new ZedGraph.ZedGraphControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Menu_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.graphOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_NoLabels = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Height = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Stepwise = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Total = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // zb1
            // 
            this.zb1.AutoSize = true;
            this.zb1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zb1.IsShowHScrollBar = true;
            this.zb1.Location = new System.Drawing.Point(-151, -168);
            this.zb1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.zb1.Name = "zb1";
            this.zb1.Padding = new System.Windows.Forms.Padding(3);
            this.zb1.ScrollGrace = 0D;
            this.zb1.ScrollMaxX = 0D;
            this.zb1.ScrollMaxY = 0D;
            this.zb1.ScrollMaxY2 = 0D;
            this.zb1.ScrollMinX = 0D;
            this.zb1.ScrollMinY = 0D;
            this.zb1.ScrollMinY2 = 0D;
            this.zb1.Size = new System.Drawing.Size(2524, 1908);
            this.zb1.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.graphOptionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1273, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripSeparator1,
            this.Menu_Exit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(131, 6);
            // 
            // Menu_Exit
            // 
            this.Menu_Exit.Name = "Menu_Exit";
            this.Menu_Exit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.Menu_Exit.Size = new System.Drawing.Size(134, 22);
            this.Menu_Exit.Text = "E&xit";
            this.Menu_Exit.Click += new System.EventHandler(this.Menu_Exit_Click);
            // 
            // graphOptionsToolStripMenuItem
            // 
            this.graphOptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_NoLabels,
            this.Menu_Height,
            this.Menu_Stepwise,
            this.Menu_Total});
            this.graphOptionsToolStripMenuItem.Name = "graphOptionsToolStripMenuItem";
            this.graphOptionsToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.graphOptionsToolStripMenuItem.Text = "Graph &Options";
            // 
            // Menu_NoLabels
            // 
            this.Menu_NoLabels.Name = "Menu_NoLabels";
            this.Menu_NoLabels.Size = new System.Drawing.Size(149, 22);
            this.Menu_NoLabels.Text = "[&none]";
            this.Menu_NoLabels.ToolTipText = "No Labels";
            this.Menu_NoLabels.Click += new System.EventHandler(this.Menu_NoLabels_Click);
            // 
            // Menu_Height
            // 
            this.Menu_Height.Name = "Menu_Height";
            this.Menu_Height.Size = new System.Drawing.Size(149, 22);
            this.Menu_Height.Text = "&Area/Height";
            this.Menu_Height.Click += new System.EventHandler(this.Menu_Height_Click);
            // 
            // Menu_Stepwise
            // 
            this.Menu_Stepwise.Name = "Menu_Stepwise";
            this.Menu_Stepwise.Size = new System.Drawing.Size(149, 22);
            this.Menu_Stepwise.Text = "&Stepwise Yield";
            this.Menu_Stepwise.Click += new System.EventHandler(this.Menu_Stepwise_Click_1);
            // 
            // Menu_Total
            // 
            this.Menu_Total.Name = "Menu_Total";
            this.Menu_Total.Size = new System.Drawing.Size(149, 22);
            this.Menu_Total.Text = "&Total Yield";
            this.Menu_Total.Click += new System.EventHandler(this.Menu_Total_Click);
            // 
            // barchartcontrol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1273, 683);
            this.Controls.Add(this.zb1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "barchartcontrol";
            this.Text = "Trityl Bar Chart Histogram";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.barchartcontrol_FormClosing);
            this.Load += new System.EventHandler(this.barchartcontrol_Load);
            this.Resize += new System.EventHandler(this.barchartcontrol_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public ZedGraph.ZedGraphControl zb1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem graphOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem Menu_Exit;
        private System.Windows.Forms.ToolStripMenuItem Menu_NoLabels;
        private System.Windows.Forms.ToolStripMenuItem Menu_Height;
        private System.Windows.Forms.ToolStripMenuItem Menu_Stepwise;
        private System.Windows.Forms.ToolStripMenuItem Menu_Total;
    }
}