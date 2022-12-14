namespace SeNA80
{
    partial class Protocol_Selector
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
            this.cb_ProtocolList = new System.Windows.Forms.ComboBox();
            this.lbl_FirstProtoText = new System.Windows.Forms.Label();
            this.bnt_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.lbl_2ndProtocol = new System.Windows.Forms.Label();
            this.cb_ProtocolList2 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cb_ProtocolList
            // 
            this.cb_ProtocolList.FormattingEnabled = true;
            this.cb_ProtocolList.Location = new System.Drawing.Point(82, 63);
            this.cb_ProtocolList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cb_ProtocolList.Name = "cb_ProtocolList";
            this.cb_ProtocolList.Size = new System.Drawing.Size(233, 23);
            this.cb_ProtocolList.Sorted = true;
            this.cb_ProtocolList.TabIndex = 0;
            this.cb_ProtocolList.SelectedIndexChanged += new System.EventHandler(this.cb_ProtocolList_SelectedIndexChanged);
            // 
            // lbl_FirstProtoText
            // 
            this.lbl_FirstProtoText.AutoSize = true;
            this.lbl_FirstProtoText.Location = new System.Drawing.Point(58, 36);
            this.lbl_FirstProtoText.Name = "lbl_FirstProtoText";
            this.lbl_FirstProtoText.Size = new System.Drawing.Size(285, 15);
            this.lbl_FirstProtoText.TabIndex = 1;
            this.lbl_FirstProtoText.Text = "Select Protocol from List then press the O.K. button:";
            // 
            // bnt_OK
            // 
            this.bnt_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bnt_OK.Location = new System.Drawing.Point(62, 160);
            this.bnt_OK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bnt_OK.Name = "bnt_OK";
            this.bnt_OK.Size = new System.Drawing.Size(128, 36);
            this.bnt_OK.TabIndex = 2;
            this.bnt_OK.Text = "O.K.";
            this.bnt_OK.UseVisualStyleBackColor = true;
            this.bnt_OK.Click += new System.EventHandler(this.bnt_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(239, 160);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(128, 36);
            this.btn_Cancel.TabIndex = 3;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // lbl_2ndProtocol
            // 
            this.lbl_2ndProtocol.AutoSize = true;
            this.lbl_2ndProtocol.Location = new System.Drawing.Point(58, 100);
            this.lbl_2ndProtocol.Name = "lbl_2ndProtocol";
            this.lbl_2ndProtocol.Size = new System.Drawing.Size(285, 15);
            this.lbl_2ndProtocol.TabIndex = 5;
            this.lbl_2ndProtocol.Text = "Select Protocol from List then press the O.K. button:";
            this.lbl_2ndProtocol.Visible = false;
            // 
            // cb_ProtocolList2
            // 
            this.cb_ProtocolList2.FormattingEnabled = true;
            this.cb_ProtocolList2.Location = new System.Drawing.Point(82, 125);
            this.cb_ProtocolList2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cb_ProtocolList2.Name = "cb_ProtocolList2";
            this.cb_ProtocolList2.Size = new System.Drawing.Size(233, 23);
            this.cb_ProtocolList2.Sorted = true;
            this.cb_ProtocolList2.TabIndex = 4;
            this.cb_ProtocolList2.Visible = false;
            // 
            // Protocol_Selector
            // 
            this.AcceptButton = this.bnt_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(444, 211);
            this.Controls.Add(this.lbl_2ndProtocol);
            this.Controls.Add(this.cb_ProtocolList2);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.bnt_OK);
            this.Controls.Add(this.lbl_FirstProtoText);
            this.Controls.Add(this.cb_ProtocolList);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Protocol_Selector";
            this.Text = "Select Protocol";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button bnt_OK;
        private System.Windows.Forms.Button btn_Cancel;
        public System.Windows.Forms.ComboBox cb_ProtocolList;
        public System.Windows.Forms.ComboBox cb_ProtocolList2;
        public System.Windows.Forms.Label lbl_FirstProtoText;
        public System.Windows.Forms.Label lbl_2ndProtocol;
    }
}