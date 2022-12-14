using System;
using System.Linq;
using System.Windows.Forms;

namespace SeNA80
{
    public partial class am_btl_config : Form
    {
        public string[] bases = new string[100];
        public int ibases = 0;

        public am_btl_config()
        {
            InitializeComponent();
            //open the base table
            //fill bases spreadsheet
            string path = globals.sequence_path;
            System.IO.StreamReader file = new System.IO.StreamReader(path + "\\BaseTable.csv");

            string newline = string.Empty;

            while ((newline = file.ReadLine()) != null)
            {
                if (newline == null || newline == string.Empty)
                    break;

                bases[ibases] = newline;
                ibases = ibases + 1;
            }
            file.Close();

            //fill the list boxe
            string[] codes = new string[ibases];
            for (int i = 0; i < ibases; i++)
            {
                string[] parts = bases[i].Split(',');
                codes[i] = parts[globals.i12Ltr];
            }

            lb_Am_ltr.Items.AddRange(codes);

            ShowCurrent();

            am_name.Enabled = false;
        }

        private void ShowCurrent()
        {
            switch(AmiditeConfig.iAmidite)
            {
                case 1:
                    lb_Am_ltr.SelectedIndex = Config_Editor.FindExactString(lb_Am_ltr, Properties.Settings.Default.Am_1_lbl.ToString());
                    am_name.Text = Properties.Settings.Default.Am_1_name;
                    lbl_Proto.Text = Properties.Settings.Default.Am_1_prtcl;
                    break;
                case 2:
                    lb_Am_ltr.SelectedIndex = Config_Editor.FindExactString(lb_Am_ltr, Properties.Settings.Default.Am_2_lbl.ToString());
                    am_name.Text = Properties.Settings.Default.Am_2_name;
                    lbl_Proto.Text = Properties.Settings.Default.Am_2_prtcl;
                    break;
                case 3:
                    lb_Am_ltr.SelectedIndex = Config_Editor.FindExactString(lb_Am_ltr, Properties.Settings.Default.Am_3_lbl.ToString()); 
                    am_name.Text = Properties.Settings.Default.Am_3_name;
                    lbl_Proto.Text = Properties.Settings.Default.Am_3_prtcl;
                    break;
                case 4:
                    lb_Am_ltr.SelectedIndex = Config_Editor.FindExactString(lb_Am_ltr, Properties.Settings.Default.Am_4_lbl.ToString());
                    am_name.Text = Properties.Settings.Default.Am_4_name;
                    lbl_Proto.Text = Properties.Settings.Default.Am_4_prtcl;
                    break;
                case 5:
                    lb_Am_ltr.SelectedIndex = Config_Editor.FindExactString(lb_Am_ltr, Properties.Settings.Default.Am_5_lbl.ToString());
                    am_name.Text = Properties.Settings.Default.Am_5_name;
                    lbl_Proto.Text = Properties.Settings.Default.Am_5_prtcl;
                    break;
                case 6:
                    lb_Am_ltr.SelectedIndex = Config_Editor.FindExactString(lb_Am_ltr, Properties.Settings.Default.Am_6_lbl.ToString());
                    am_name.Text = Properties.Settings.Default.Am_6_name;
                    lbl_Proto.Text = Properties.Settings.Default.Am_6_prtcl;
                    break;
                case 7:
                    lb_Am_ltr.SelectedIndex = Config_Editor.FindExactString(lb_Am_ltr, Properties.Settings.Default.Am_7_lbl.ToString());
                    am_name.Text = Properties.Settings.Default.Am_7_name;
                    lbl_Proto.Text = Properties.Settings.Default.Am_7_prtcl;
                    break;
                case 8:
                    lb_Am_ltr.SelectedIndex = Config_Editor.FindExactString(lb_Am_ltr, Properties.Settings.Default.Am_8_lbl.ToString());
                    am_name.Text = Properties.Settings.Default.Am_8_name;
                    lbl_Proto.Text = Properties.Settings.Default.Am_8_prtcl;
                    break;
                case 9:
                    lb_Am_ltr.SelectedIndex = Config_Editor.FindExactString(lb_Am_ltr, Properties.Settings.Default.Am_9_lbl.ToString());
                    am_name.Text = Properties.Settings.Default.Am_9_name;
                    lbl_Proto.Text = Properties.Settings.Default.Am_9_prtcl;
                    break;
                case 10:
                    lb_Am_ltr.SelectedIndex = Config_Editor.FindExactString(lb_Am_ltr, Properties.Settings.Default.Am_10_lbl.ToString());
                    am_name.Text = Properties.Settings.Default.Am_10_name;
                    lbl_Proto.Text = Properties.Settings.Default.Am_10_prtcl;
                    break;
                case 11:
                    lb_Am_ltr.SelectedIndex = Config_Editor.FindExactString(lb_Am_ltr, Properties.Settings.Default.Am_11_lbl.ToString());
                    am_name.Text = Properties.Settings.Default.Am_11_name;
                    lbl_Proto.Text = Properties.Settings.Default.Am_11_prtcl;
                    break;
                case 12:
                    lb_Am_ltr.SelectedIndex = Config_Editor.FindExactString(lb_Am_ltr, Properties.Settings.Default.Am_12_lbl.ToString());
                    am_name.Text = Properties.Settings.Default.Am_12_name;
                    lbl_Proto.Text = Properties.Settings.Default.Am_12_prtcl;
                    break;
                case 13:
                    lb_Am_ltr.SelectedIndex = Config_Editor.FindExactString(lb_Am_ltr, Properties.Settings.Default.Am_13_lbl.ToString());
                    am_name.Text = Properties.Settings.Default.Am_13_name;
                    lbl_Proto.Text = Properties.Settings.Default.Am_13_prtcl;
                    break;
                case 14:
                    lb_Am_ltr.SelectedIndex = Config_Editor.FindExactString(lb_Am_ltr, Properties.Settings.Default.Am_14_lbl.ToString());
                    am_name.Text = Properties.Settings.Default.Am_14_name;
                    lbl_Proto.Text = Properties.Settings.Default.Am_14_prtcl;
                    break;
                case 15:
                    lb_Am_ltr.SelectedIndex = Config_Editor.FindExactString(lb_Am_ltr, Properties.Settings.Default.Am_15_lbl.ToString());
                    am_name.Text = Properties.Settings.Default.Am_15_name;
                    lbl_Proto.Text = Properties.Settings.Default.Am_15_prtcl;
                    break;
                case 16:
                    lb_Am_ltr.SelectedIndex = Config_Editor.FindExactString(lb_Am_ltr, Properties.Settings.Default.Am_16_lbl.ToString());
                    am_name.Text = Properties.Settings.Default.Am_16_name;
                    lbl_Proto.Text = Properties.Settings.Default.Am_16_prtcl;
                    break;
            }
        }
        private void btn_Prot_Sel_Click(object sender, EventArgs e)
        {
            
            try
            {
                //Clearing previously selected image from picture box
                string szPath = Properties.Settings.Default.Protocol_Path;
             

                //Showing the File Chooser Dialog Box for Image File selection
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.InitialDirectory = szPath;
                openFileDialog1.Filter = "amidite files (*.apr)|*.apr|All files (*.*)|*.*"; ;
                openFileDialog1.Title = "Open Amidite Protocol File";

                DialogResult IsFileChosen = openFileDialog1.ShowDialog();

                if (IsFileChosen == System.Windows.Forms.DialogResult.OK)
                {
                    //Get the File name
                    string path = openFileDialog1.FileName;
                    string[] pathArr = path.Split('\\');
                    string fileArr = pathArr.Last();

                    lbl_Proto.Text = fileArr;

                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            switch (AmiditeConfig.iAmidite)
            {
                case 1:
                    Properties.Settings.Default.Am_1_lbl = lb_Am_ltr.Text;
                    Properties.Settings.Default.Am_1_name = am_name.Text;
                    Properties.Settings.Default.Am_1_prtcl  = lbl_Proto.Text;
                    Properties.Settings.Default.Save();
                    break;

                case 2:
                    Properties.Settings.Default.Am_2_lbl = lb_Am_ltr.Text;
                    Properties.Settings.Default.Am_2_name = am_name.Text;
                    Properties.Settings.Default.Am_2_prtcl = lbl_Proto.Text;
                    Properties.Settings.Default.Save();
                    break;

                case 3:
                    Properties.Settings.Default.Am_3_lbl = lb_Am_ltr.Text;
                    Properties.Settings.Default.Am_3_name = am_name.Text;
                    Properties.Settings.Default.Am_3_prtcl = lbl_Proto.Text;
                    Properties.Settings.Default.Save();
                    break;

                case 4:
                    Properties.Settings.Default.Am_4_lbl = lb_Am_ltr.Text;
                    Properties.Settings.Default.Am_4_name = am_name.Text;
                    Properties.Settings.Default.Am_4_prtcl = lbl_Proto.Text;
                    Properties.Settings.Default.Save();
                    break;

                case 5:
                    Properties.Settings.Default.Am_5_lbl = lb_Am_ltr.Text;
                    Properties.Settings.Default.Am_5_name = am_name.Text;
                    Properties.Settings.Default.Am_5_prtcl = lbl_Proto.Text;
                    Properties.Settings.Default.Save();
                    break;

                case 6:
                    Properties.Settings.Default.Am_6_lbl = lb_Am_ltr.Text;
                    Properties.Settings.Default.Am_6_name = am_name.Text;
                    Properties.Settings.Default.Am_6_prtcl = lbl_Proto.Text;
                    Properties.Settings.Default.Save();
                    break;

                case 7:
                    Properties.Settings.Default.Am_7_lbl = lb_Am_ltr.Text;
                    Properties.Settings.Default.Am_7_name = am_name.Text;
                    Properties.Settings.Default.Am_7_prtcl = lbl_Proto.Text;
                    Properties.Settings.Default.Save();
                    break;

                case 8:
                    Properties.Settings.Default.Am_8_lbl = lb_Am_ltr.Text;
                    Properties.Settings.Default.Am_8_name = am_name.Text;
                    Properties.Settings.Default.Am_8_prtcl = lbl_Proto.Text;
                    Properties.Settings.Default.Save();
                    break;

                case 9:
                    Properties.Settings.Default.Am_9_lbl = lb_Am_ltr.Text;
                    Properties.Settings.Default.Am_9_name = am_name.Text;
                    Properties.Settings.Default.Am_9_prtcl = lbl_Proto.Text;
                    Properties.Settings.Default.Save();
                    break;

                case 10:
                    Properties.Settings.Default.Am_10_lbl = lb_Am_ltr.Text;
                    Properties.Settings.Default.Am_10_name = am_name.Text;
                    Properties.Settings.Default.Am_10_prtcl = lbl_Proto.Text;
                    Properties.Settings.Default.Save();
                    break;

                case 11:
                    Properties.Settings.Default.Am_11_lbl = lb_Am_ltr.Text;
                    Properties.Settings.Default.Am_11_name = am_name.Text;
                    Properties.Settings.Default.Am_11_prtcl = lbl_Proto.Text;
                    Properties.Settings.Default.Save();
                    break;

                case 12:
                    Properties.Settings.Default.Am_12_lbl = lb_Am_ltr.Text;
                    Properties.Settings.Default.Am_12_name = am_name.Text;
                    Properties.Settings.Default.Am_12_prtcl = lbl_Proto.Text;
                    Properties.Settings.Default.Save();
                    break;

                case 13:
                    Properties.Settings.Default.Am_13_lbl = lb_Am_ltr.Text;
                    Properties.Settings.Default.Am_13_name = am_name.Text;
                    Properties.Settings.Default.Am_13_prtcl = lbl_Proto.Text;
                    Properties.Settings.Default.Save();
                    break;

                case 14:
                    Properties.Settings.Default.Am_14_lbl = lb_Am_ltr.Text;
                    Properties.Settings.Default.Am_14_name = am_name.Text;
                    Properties.Settings.Default.Am_14_prtcl = lbl_Proto.Text;
                    Properties.Settings.Default.Save();
                    break;

                case 15:
                    Properties.Settings.Default.Am_15_lbl = lb_Am_ltr.Text;
                    Properties.Settings.Default.Am_15_name = am_name.Text;
                    Properties.Settings.Default.Am_15_prtcl = lbl_Proto.Text;
                    Properties.Settings.Default.Save();
                    break;

                case 16:
                    Properties.Settings.Default.Am_16_lbl = lb_Am_ltr.Text;
                    Properties.Settings.Default.Am_16_name = am_name.Text;
                    Properties.Settings.Default.Am_16_prtcl = lbl_Proto.Text;
                    Properties.Settings.Default.Save();
                    break;

            }
        }

        private void lb_Am_ltr_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get the name from bases
            int index = lb_Am_ltr.SelectedIndex;
            string[] ampart = bases[index].Split(',');
            am_name.Text = ampart[2];

            //clear the protocol
            lbl_Proto.Text = string.Empty;
        }

        private void am_btl_config_Load(object sender, EventArgs e)
        {

        }

        private void lb_Am_ltr_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(lb_Am_ltr, "Amidite Letter(s)..", "Select the letter(s) of the amidite installed on\nthe bottle postion selected...");
        }

        private void am_name_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(am_name, "Amidite Name..", "Enter the chemical name for the amditei...");
        }

        private void btn_Prot_Sel_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_Prot_Sel, "Select Protocol..", "Select the amidite protocol to be run when the amidite\nconfigured is found in a sequence...");
        }

        private void btn_Cancel_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_Cancel, "Cancel..", "Return to the Amidite Confiugreation Editor without saving changes...");
        }

        private void btn_OK_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_OK, "Return and Save..", "Return to the Amidite Confiugreation Editor after updating\nthe Program configuration with the changes...");

        }
    }
}
