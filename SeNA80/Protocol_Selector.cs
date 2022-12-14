using System;
using System.Windows.Forms;

namespace SeNA80
{
    public partial class Protocol_Selector : Form
    {
        public Protocol_Selector()
        {
            InitializeComponent();
        }

        private void bnt_OK_Click(object sender, EventArgs e)
        {

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void cb_ProtocolList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
