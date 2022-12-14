using System.Data;

namespace SeNA80
{
    partial class BaseTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseTable));
            this.dt_BaseTable = new System.Windows.Forms.DataGridView();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.gb_Use = new System.Windows.Forms.GroupBox();
            this.rb_2ltr = new System.Windows.Forms.RadioButton();
            this.rb_1ltr = new System.Windows.Forms.RadioButton();
            this.btn_Delete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dt_BaseTable)).BeginInit();
            this.gb_Use.SuspendLayout();
            this.SuspendLayout();
            // 
            // dt_BaseTable
            // 
            this.dt_BaseTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dt_BaseTable.Location = new System.Drawing.Point(-1, 78);
            this.dt_BaseTable.Name = "dt_BaseTable";
            this.dt_BaseTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dt_BaseTable.Size = new System.Drawing.Size(681, 318);
            this.dt_BaseTable.TabIndex = 1;
            this.dt_BaseTable.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dt_BaseTable_CellBeginEdit);
            this.dt_BaseTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dt_BaseTable.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dt_BaseTable_RowsAdded);
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(115, 402);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(117, 32);
            this.btn_OK.TabIndex = 2;
            this.btn_OK.Text = "&Save";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(449, 402);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(117, 32);
            this.btn_Cancel.TabIndex = 3;
            this.btn_Cancel.Text = "&Close";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(174, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(320, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Input the Base Information Below, Press OK to Save...";
            // 
            // gb_Use
            // 
            this.gb_Use.Controls.Add(this.rb_2ltr);
            this.gb_Use.Controls.Add(this.rb_1ltr);
            this.gb_Use.Location = new System.Drawing.Point(188, 33);
            this.gb_Use.Name = "gb_Use";
            this.gb_Use.Size = new System.Drawing.Size(255, 38);
            this.gb_Use.TabIndex = 5;
            this.gb_Use.TabStop = false;
            this.gb_Use.Text = "&Use";
            // 
            // rb_2ltr
            // 
            this.rb_2ltr.AutoSize = true;
            this.rb_2ltr.Location = new System.Drawing.Point(144, 11);
            this.rb_2ltr.Name = "rb_2ltr";
            this.rb_2ltr.Size = new System.Drawing.Size(94, 17);
            this.rb_2ltr.TabIndex = 1;
            this.rb_2ltr.Text = "&2 Letter Codes";
            this.rb_2ltr.UseVisualStyleBackColor = true;
            // 
            // rb_1ltr
            // 
            this.rb_1ltr.AutoSize = true;
            this.rb_1ltr.Checked = true;
            this.rb_1ltr.Location = new System.Drawing.Point(43, 11);
            this.rb_1ltr.Name = "rb_1ltr";
            this.rb_1ltr.Size = new System.Drawing.Size(94, 17);
            this.rb_1ltr.TabIndex = 0;
            this.rb_1ltr.TabStop = true;
            this.rb_1ltr.Text = "&1 Letter Codes";
            this.rb_1ltr.UseVisualStyleBackColor = true;
            // 
            // btn_Delete
            // 
            this.btn_Delete.Location = new System.Drawing.Point(282, 402);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(117, 32);
            this.btn_Delete.TabIndex = 6;
            this.btn_Delete.Text = "&Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // BaseTable
            // 
            this.AcceptButton = this.btn_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(686, 450);
            this.Controls.Add(this.btn_Delete);
            this.Controls.Add(this.gb_Use);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.dt_BaseTable);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BaseTable";
            this.Text = "BaseTable";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BaseTable_FormClosing);
            this.Load += new System.EventHandler(this.BaseTable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dt_BaseTable)).EndInit();
            this.gb_Use.ResumeLayout(false);
            this.gb_Use.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gb_Use;
        private System.Windows.Forms.RadioButton rb_2ltr;
        private System.Windows.Forms.RadioButton rb_1ltr;
        public System.Windows.Forms.DataGridView dt_BaseTable;
        private System.Windows.Forms.Button btn_Delete;
    }
}