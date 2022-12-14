using System;
using System.Windows.Forms;

namespace SeNA80
{
    public partial class PS_UserManagemet : Form
    {
        public PS_UserManagemet()
        {
            InitializeComponent();

            rb_NewAdmin.Checked = true;
        }

        private void PS_UserManagemet_Load(object sender, EventArgs e)
        {
          
        }

  
        private void btn_Submit_Click(object sender, EventArgs e)
        {

            if (tb_UserName.Text.Trim().Length < 3)
            {
                if (MessageBox.Show("You Must Have a Valid User Name\n\nPlease enter one and try again...", "User Name", MessageBoxButtons.OK, MessageBoxIcon.Hand) ==
                    DialogResult.OK) { return; }
               
            }

            if (tb_NewPWD.Text.Trim().Length < 6)
            {
                if (MessageBox.Show("You Must Have a Password\nMinimum Length is 6 characters\nPlease enter one...", "No Password", MessageBoxButtons.OK, MessageBoxIcon.Hand) ==
                        DialogResult.OK) { return; }
                }


            if (tb_ComfirmPWD.Text.Trim().Length <6)
            {
              if (MessageBox.Show("You are missing a password confirmation\n\nPlease Try Again....", "Password Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Hand) ==
                          DialogResult.OK) { return; }
            }

            if (!tb_ComfirmPWD.Text.Trim().Equals(tb_NewPWD.Text.Trim()))
            {
                if (MessageBox.Show("Passwords Do Not Match\n\nTry Again....", "Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Hand) ==
                             DialogResult.OK) { return; }
            }


            MessageBox.Show("User Added!\n\nRemember to press Apply to save to the database", "Add Complete", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbox_NewPWD_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbox_ComfirmPWD_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
