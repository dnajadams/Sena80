namespace SeNA80
{
    partial class ConsumptionCalc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsumptionCalc));
            this.rTB_Volumes = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_OK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dt_ConsumptionTable = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_ConsumptionTable)).BeginInit();
            this.SuspendLayout();
            // 
            // rTB_Volumes
            // 
            this.rTB_Volumes.Location = new System.Drawing.Point(0, 91);
            this.rTB_Volumes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rTB_Volumes.Name = "rTB_Volumes";
            this.rTB_Volumes.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rTB_Volumes.Size = new System.Drawing.Size(586, 311);
            this.rTB_Volumes.TabIndex = 0;
            this.rTB_Volumes.Text = "";
            this.rTB_Volumes.WordWrap = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btn_OK);
            this.panel1.Location = new System.Drawing.Point(-2, 506);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(588, 69);
            this.panel1.TabIndex = 1;
            // 
            // btn_OK
            // 
            this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_OK.Location = new System.Drawing.Point(229, 15);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(120, 40);
            this.btn_OK.TabIndex = 0;
            this.btn_OK.Text = "E&xit";
            this.btn_OK.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(10, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(563, 42);
            this.label1.TabIndex = 2;
            this.label1.Text = "The volumes shown below are approximates...It is recommended that you add at leas" +
    "t 10% additional to each quantity.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dt_ConsumptionTable
            // 
            this.dt_ConsumptionTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dt_ConsumptionTable.Location = new System.Drawing.Point(0, 91);
            this.dt_ConsumptionTable.Name = "dt_ConsumptionTable";
            this.dt_ConsumptionTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dt_ConsumptionTable.Size = new System.Drawing.Size(586, 413);
            this.dt_ConsumptionTable.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(54, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(470, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Note: The volumes displayed DO NOT include quanties from amidite primes during Sy" +
    "stem Startup.";
            // 
            // ConsumptionCalc
            // 
            this.AcceptButton = this.btn_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_OK;
            this.ClientSize = new System.Drawing.Size(587, 575);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dt_ConsumptionTable);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.rTB_Volumes);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "ConsumptionCalc";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Reagent Consumption Viewer";
            this.Load += new System.EventHandler(this.ConsumptionCalc_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dt_ConsumptionTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_OK;
        public System.Windows.Forms.RichTextBox rTB_Volumes;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DataGridView dt_ConsumptionTable;
        private System.Windows.Forms.Label label2;
    }
}