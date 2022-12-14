using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace SeNA80
{
    public partial class Load_Protocols : Form
    {
        public Load_Protocols()
        {
            int index = 0;
            
            InitializeComponent();

            

            //Fill the comboboxes with the files in the director
            DirectoryInfo dinfo = new DirectoryInfo(@globals.protocol_path);
            // fill the list box
            //first the Startup box
            this.cb_Start.Items.Add("[none]");
            FileInfo[] Files = dinfo.GetFiles("*.str");
            foreach (FileInfo file in Files)
                this.cb_Start.Items.Add(file.Name);
            if (!string.IsNullOrEmpty(SeNARun.protolbls[0]))
            {
                index = cb_Start.FindString(SeNARun.protolbls[0]);
                cb_Start.SelectedIndex = index;
            }
            else if(globals.defStartupProtocol != string.Empty)
            {
                index = cb_Start.FindString(globals.defStartupProtocol);
                cb_Start.SelectedIndex = index;
            }

            //first the prep list box
            FileInfo[] FilesPr = dinfo.GetFiles("*.prp");
            foreach (FileInfo file in FilesPr)
                    this.cb_PreRun.Items.Add(file.Name);
            if (!string.IsNullOrEmpty(SeNARun.protolbls[1]))
            {
                index = cb_PreRun.FindString(SeNARun.protolbls[1]);
                cb_PreRun.SelectedIndex = index;
            }
            else if (globals.defPrepProtocol != string.Empty)
            {
                index = cb_PreRun.FindString(globals.defPrepProtocol);
                cb_PreRun.SelectedIndex = index;
            }

            //next the run list box
            FileInfo[] FilesPro = dinfo.GetFiles("*.pro");
            foreach (FileInfo file in FilesPro)
                this.cb_Run.Items.Add(file.Name);
            if (!string.IsNullOrEmpty(SeNARun.protolbls[2]))
            {
                index = cb_Run.FindString(SeNARun.protolbls[2]);
                cb_Run.SelectedIndex = index;
            }
            else if(globals.defRunProtocol != string.Empty)
            {
                index = cb_Run.FindString(globals.defRunProtocol);
                cb_Run.SelectedIndex = index;
            }

            //finally the post run list box
            FileInfo [] FilesPS = dinfo.GetFiles("*.psy");
            foreach (FileInfo file in FilesPS)
                this.cb_PostRun.Items.Add(file.Name);
            if (!string.IsNullOrEmpty(SeNARun.protolbls[3]))
            {
                index = cb_PostRun.FindString(SeNARun.protolbls[3]);
                cb_PostRun.SelectedIndex = index;
            }
            else if (globals.defPostProtocol != string.Empty)
            {
                index = cb_PostRun.FindString(globals.defPostProtocol);
                cb_PostRun.SelectedIndex = index;
            }
        }
        

        private void btn_Check_Click(object sender, EventArgs e)
        {
            //make sure that all are filled in if O.K. then ruturn
            //else pop a message box and stay in window
            try
            {

                if (cb_PostRun.SelectedItem.ToString().Length > 1 && cb_Run.SelectedItem.ToString().Length > 1 && cb_PreRun.SelectedItem.ToString().Length > 1)
                {
                    globals.bProtocolsLoaded = true;
                    this.DialogResult = DialogResult.OK;

                }
                else
                {

                    //  this.DialogResult = DialogResult.Cancel;
                    MessageBox.Show("Must have methods for Pre, Run and Post Selected", "Select Protocols");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Must have methods for Pre, Run and Post Selected", "Select Protocols");
                return;
            }

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
           
            this.Dispose();
            return;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

    
    }
}
