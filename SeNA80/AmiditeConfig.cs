using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SeNA80
{
    public partial class AmiditeConfig : Form
    {
        public static int iAmidite  = 0;
        public string[] amname = new string[18];

        public AmiditeConfig()
        {
            InitializeComponent();
            //read the defaults from the setting file
            //set from default

            if (globals.AutoScaleAmidites)
            {
                cb_AutoScale.Checked = true;
                lbl_Note.Visible = true;
            }
            else
            {
                cb_AutoScale.Checked = false;
                lbl_Note.Visible = false;
            }
            //initialize annames
            for (int i = 0; i < 18; i++)
                amname[i] = string.Empty;

            this.amidite1.Text = Properties.Settings.Default.Am_1_lbl.ToString();
            this.tb_amproto_1.Text = Properties.Settings.Default.Am_1_prtcl;
            amname[1] = Properties.Settings.Default.Am_1_name;
            if (amname[1].Length > 17)
                this.Am_1_Chem.Text = amname[1].Substring(0, 17);
            else
                this.Am_1_Chem.Text = amname[1];

            this.amidite2.Text = Properties.Settings.Default.Am_2_lbl.ToString();
            this.tb_amproto_2.Text = Properties.Settings.Default.Am_2_prtcl;
            amname[2] = Properties.Settings.Default.Am_2_name;
            if (amname[2].Length > 17)
                this.Am_2_Chem.Text = amname[2].Substring(0, 17);
            else
                this.Am_2_Chem.Text = amname[2];

            this.amidite3.Text = Properties.Settings.Default.Am_3_lbl.ToString();
            this.tb_amproto_3.Text = Properties.Settings.Default.Am_3_prtcl;
            amname[3] = Properties.Settings.Default.Am_3_name;
            if (amname[3].Length > 17)
                this.Am_3_Chem.Text = amname[3].Substring(0, 17);
            else
                this.Am_3_Chem.Text = amname[3];
            this.amidite4.Text = Properties.Settings.Default.Am_4_lbl.ToString();
            this.tb_amproto_4.Text = Properties.Settings.Default.Am_4_prtcl;
            amname[4] = Properties.Settings.Default.Am_4_name;
            if (amname[4].Length > 17)
                this.Am_4_Chem.Text = amname[4].Substring(0, 17);
            else
                this.Am_4_Chem.Text = amname[4];
            this.amidite5.Text = Properties.Settings.Default.Am_5_lbl.ToString();
            this.tb_amproto_5.Text = Properties.Settings.Default.Am_5_prtcl;
            amname[5] = Properties.Settings.Default.Am_5_name;
            if (amname[5].Length > 17)
                this.Am_5_Chem.Text = amname[5].Substring(0, 17);
            else
                this.Am_5_Chem.Text = amname[5];
            this.amidite6.Text = Properties.Settings.Default.Am_6_lbl.ToString();
            this.tb_amproto_6.Text = Properties.Settings.Default.Am_6_prtcl;
            amname[6] = Properties.Settings.Default.Am_6_name;
            if (amname[6].Length > 17)
                this.Am_6_Chem.Text = amname[6].Substring(0, 17);
            else
                this.Am_6_Chem.Text = amname[6];
            this.amidite7.Text = Properties.Settings.Default.Am_7_lbl.ToString();
            this.tb_amproto_7.Text = Properties.Settings.Default.Am_7_prtcl;
            amname[7] = Properties.Settings.Default.Am_7_name;
            if (amname[7].Length > 17)
                this.Am_7_Chem.Text = amname[7].Substring(0, 17);
            else
                this.Am_7_Chem.Text = amname[7];
            this.amidite8.Text = Properties.Settings.Default.Am_8_lbl.ToString();
            this.tb_amproto_8.Text = Properties.Settings.Default.Am_8_prtcl;
            amname[8] = Properties.Settings.Default.Am_8_name;
            if (amname[8].Length > 17)
                this.Am_8_Chem.Text = amname[8].Substring(0, 17);
            else
                this.Am_8_Chem.Text = amname[8];
            this.amidite9.Text = Properties.Settings.Default.Am_9_lbl.ToString();
            this.tb_amproto_9.Text = Properties.Settings.Default.Am_9_prtcl;
            amname[9] = Properties.Settings.Default.Am_9_name;
            if (amname[9].Length > 17)
                this.Am_9_Chem.Text = amname[9].Substring(0, 17);
            else
                this.Am_9_Chem.Text = amname[9];
            this.amidite10.Text = Properties.Settings.Default.Am_10_lbl.ToString();
            this.tb_amproto_10.Text = Properties.Settings.Default.Am_10_prtcl;
            amname[10] = Properties.Settings.Default.Am_10_name;
            if (amname[10].Length > 17)
                this.Am_10_Chem.Text = amname[10].Substring(0, 17);
            else
                this.Am_10_Chem.Text = amname[10];
            this.amidite11.Text = Properties.Settings.Default.Am_11_lbl.ToString();
            this.tb_amproto_11.Text = Properties.Settings.Default.Am_11_prtcl;
            amname[11] = Properties.Settings.Default.Am_11_name;
            if (amname[11].Length > 17)
                this.Am_11_Chem.Text = amname[11].Substring(0, 17);
            else
                this.Am_11_Chem.Text = amname[11];
            this.amidite12.Text = Properties.Settings.Default.Am_12_lbl.ToString();
            this.tb_amproto_12.Text = Properties.Settings.Default.Am_12_prtcl;
            amname[12] = Properties.Settings.Default.Am_12_name;
            if (amname[12].Length > 17)
                this.Am_12_Chem.Text = amname[12].Substring(0, 17);
            else
                this.Am_12_Chem.Text = amname[12];
            this.amidite13.Text = Properties.Settings.Default.Am_13_lbl.ToString();
            this.tb_amproto_13.Text = Properties.Settings.Default.Am_13_prtcl;
            amname[13] = Properties.Settings.Default.Am_13_name;
            if (amname[13].Length > 17)
                this.Am_13_Chem.Text = amname[13].Substring(0, 17);
            else
                this.Am_13_Chem.Text = amname[13];
            this.amidite14.Text = Properties.Settings.Default.Am_14_lbl.ToString();
            this.tb_amproto_14.Text = Properties.Settings.Default.Am_14_prtcl;
            amname[14] = Properties.Settings.Default.Am_14_name;
            if (amname[14].Length > 17)
                this.Am_14_Chem.Text = amname[14].Substring(0, 17);
            else
                this.Am_14_Chem.Text = amname[14];

            if (globals.i12Ltr == 0)
                lbl_Using.Text = "Using One Letter Base Codes";
            else
                lbl_Using.Text = "Using Two Letter Base Codes";

        }

        public void ChangStringGlobal(bool value)
        {
            Properties.Settings settings = Properties.Settings.Default;

        
            settings.Loggin_YN = (bool)value;
            settings.Save();
        }

       
        private void amidite1_Click(object sender, EventArgs e)
        {
            iAmidite =  1;

            using (am_btl_config dlg = new am_btl_config())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    //update the settings file
                    this.amidite1.Text = Properties.Settings.Default.Am_1_lbl.ToString();
                    this.tb_amproto_1.Text = Properties.Settings.Default.Am_1_prtcl;
                    amname[1] = Properties.Settings.Default.Am_1_name;
                    if (amname[iAmidite].Length > 17)
                        this.Am_1_Chem.Text = amname[1].Substring(0, 17);
                    else
                        this.Am_1_Chem.Text = amname[1];


                    iAmidite = 0;
                    Properties.Settings.Default.Save();
                    return;
                    // whatever you need to do with result
                }
            }
        }

        private void amidite2_Click(object sender, EventArgs e)
        {
            iAmidite = 2;

            using (am_btl_config dlg = new am_btl_config())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    //update the settings file
                    this.amidite2.Text = Properties.Settings.Default.Am_2_lbl.ToString();
                    this.tb_amproto_2.Text = Properties.Settings.Default.Am_2_prtcl;
                    amname[2] = Properties.Settings.Default.Am_2_name;
                    if (amname[iAmidite].Length > 17)
                        this.Am_2_Chem.Text = amname[1].Substring(0, 17);
                    else
                        this.Am_2_Chem.Text = amname[2];

                    iAmidite = 0;
                    Properties.Settings.Default.Save();
                    return;
                    // whatever you need to do with result
                }
            }
        }

        private void amidite3_Click(object sender, EventArgs e)
        {
            iAmidite = 3;

            using (am_btl_config dlg = new am_btl_config())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    //update the settings file
                    this.amidite3.Text = Properties.Settings.Default.Am_3_lbl.ToString();
                    this.tb_amproto_3.Text = Properties.Settings.Default.Am_3_prtcl;
                    amname[3] = Properties.Settings.Default.Am_3_name;
                    if (amname[iAmidite].Length > 17)
                        this.Am_3_Chem.Text = amname[3].Substring(0, 17);
                    else
                        this.Am_3_Chem.Text = amname[3];

                    iAmidite = 0;
                    Properties.Settings.Default.Save();
                    return;
                    // whatever you need to do with result
                }
            }

        }

        private void amidite4_Click(object sender, EventArgs e)
        {
            iAmidite = 4;

            using (am_btl_config dlg = new am_btl_config())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    //update the settings file
                    this.amidite4.Text = Properties.Settings.Default.Am_4_lbl.ToString();
                    this.tb_amproto_4.Text = Properties.Settings.Default.Am_4_prtcl;
                    amname[iAmidite] = Properties.Settings.Default.Am_4_name;
                    if (amname[iAmidite].Length > 17)
                        this.Am_4_Chem.Text = amname[iAmidite].Substring(0, 17);
                    else
                        this.Am_4_Chem.Text = amname[4];

                    iAmidite = 0;
                    Properties.Settings.Default.Save();
                    return;
                    // whatever you need to do with result
                }
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            iAmidite = 5;

            using (am_btl_config dlg = new am_btl_config())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    //update the settings file
                    this.amidite5.Text = Properties.Settings.Default.Am_5_lbl.ToString();
                    this.tb_amproto_5.Text = Properties.Settings.Default.Am_5_prtcl;
                    amname[iAmidite] = Properties.Settings.Default.Am_5_name;
                    if (amname[iAmidite].Length > 17)
                        this.Am_5_Chem.Text = amname[iAmidite].Substring(0, 17);
                    else
                        this.Am_5_Chem.Text = amname[5];

                    iAmidite = 0;
                    Properties.Settings.Default.Save();
                    return;
                    // whatever you need to do with result
                }
            }
        }

        private void amidite6_Click(object sender, EventArgs e)
        {
            iAmidite = 6;

            using (am_btl_config dlg = new am_btl_config())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    //update the settings file
                    this.amidite6.Text = Properties.Settings.Default.Am_6_lbl.ToString();
                    this.tb_amproto_6.Text = Properties.Settings.Default.Am_6_prtcl;
                    amname[iAmidite] = Properties.Settings.Default.Am_6_name;
                    if (amname[iAmidite].Length > 17)
                        this.Am_6_Chem.Text = amname[iAmidite].Substring(0, 17);
                    else
                        this.Am_6_Chem.Text = amname[6];

                    iAmidite = 0;
                    Properties.Settings.Default.Save();
                    return;
                    // whatever you need to do with result
                }
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            iAmidite = 7;

            using (am_btl_config dlg = new am_btl_config())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    //update the settings file
                    this.amidite7.Text = Properties.Settings.Default.Am_7_lbl.ToString();
                    this.tb_amproto_7.Text = Properties.Settings.Default.Am_7_prtcl;
                    amname[iAmidite] = Properties.Settings.Default.Am_7_name;
                    if (amname[iAmidite].Length > 17)
                        this.Am_7_Chem.Text = amname[iAmidite].Substring(0, 17);
                    else
                        this.Am_7_Chem.Text = amname[7];

                    Properties.Settings.Default.Save();
                    iAmidite = 0;
                    return;
                    // whatever you need to do with result
                }
            }
        }


        private void amidite8_Click(object sender, EventArgs e)
        {
            iAmidite = 8;

            using (am_btl_config dlg = new am_btl_config())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    //update the settings file
                    this.amidite8.Text = Properties.Settings.Default.Am_8_lbl.ToString();
                    this.tb_amproto_8.Text = Properties.Settings.Default.Am_8_prtcl;
                    amname[iAmidite] = Properties.Settings.Default.Am_8_name;
                    if (amname[iAmidite].Length > 17)
                        this.Am_8_Chem.Text = amname[iAmidite].Substring(0, 17);
                    else
                        this.Am_8_Chem.Text = amname[8];

                    Properties.Settings.Default.Save();
                    iAmidite = 0;
                    return;
                    // whatever you need to do with result
                }
            }

        }

        private void amidite9_Click(object sender, EventArgs e)
        {
            iAmidite = 9;

            using (am_btl_config dlg = new am_btl_config())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    //update the settings file
                    this.amidite9.Text = Properties.Settings.Default.Am_9_lbl.ToString();
                    this.tb_amproto_9.Text = Properties.Settings.Default.Am_9_prtcl;
                    amname[iAmidite] = Properties.Settings.Default.Am_9_name;
                    if (amname[iAmidite].Length > 17)
                        this.Am_9_Chem.Text = amname[iAmidite].Substring(0, 17);
                    else
                        this.Am_9_Chem.Text = amname[9];

                    Properties.Settings.Default.Save();
                    iAmidite = 0;
                    return;
                    // whatever you need to do with result
                }
            }

        }

        private void amidite10_Click(object sender, EventArgs e)
        {
            iAmidite = 10;

            using (am_btl_config dlg = new am_btl_config())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    //update the settings file
                    this.amidite10.Text = Properties.Settings.Default.Am_10_lbl.ToString();
                    this.tb_amproto_10.Text = Properties.Settings.Default.Am_10_prtcl;
                    amname[iAmidite] = Properties.Settings.Default.Am_10_name;
                    if (amname[iAmidite].Length > 17)
                        this.Am_10_Chem.Text = amname[iAmidite].Substring(0, 17);
                    else
                        this.Am_10_Chem.Text = amname[10];

                    Properties.Settings.Default.Save();
                    iAmidite = 0;
                    return;
                    // whatever you need to do with result
                }
            }

        }

        private void amidite11_Click(object sender, EventArgs e)
        {
            iAmidite = 11;

            using (am_btl_config dlg = new am_btl_config())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    //update the settings file
                    this.amidite11.Text = Properties.Settings.Default.Am_11_lbl.ToString();
                    this.tb_amproto_11.Text = Properties.Settings.Default.Am_11_prtcl;
                    amname[iAmidite] = Properties.Settings.Default.Am_11_name;
                    if (amname[iAmidite].Length > 17)
                        this.Am_11_Chem.Text = amname[iAmidite].Substring(0, 17);
                    else
                        this.Am_11_Chem.Text = amname[11];

                    Properties.Settings.Default.Save();
                    iAmidite = 0;
                    return;
                    // whatever you need to do with result
                }
            }
        }

        private void amidite12_Click(object sender, EventArgs e)
        {
            iAmidite = 12;

            using (am_btl_config dlg = new am_btl_config())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    //update the settings file
                    this.amidite12.Text = Properties.Settings.Default.Am_12_lbl.ToString();
                    this.tb_amproto_12.Text = Properties.Settings.Default.Am_12_prtcl;
                    amname[iAmidite] = Properties.Settings.Default.Am_12_name;
                    if (amname[iAmidite].Length > 17)
                        this.Am_12_Chem.Text = amname[iAmidite].Substring(0, 17);
                    else
                        this.Am_12_Chem.Text = amname[12];

                    Properties.Settings.Default.Save();
                    iAmidite = 0;
                    return;
                    // whatever you need to do with result
                }
            }
        }

        private void amidite13_Click(object sender, EventArgs e)
        {
            iAmidite = 13;

            using (am_btl_config dlg = new am_btl_config())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    //update the settings file
                    this.amidite13.Text = Properties.Settings.Default.Am_13_lbl.ToString();
                    this.tb_amproto_13.Text = Properties.Settings.Default.Am_13_prtcl;
                    amname[iAmidite] = Properties.Settings.Default.Am_13_name;
                    if (amname[iAmidite].Length > 17)
                        this.Am_13_Chem.Text = amname[iAmidite].Substring(0, 17);
                    else
                        this.Am_13_Chem.Text = amname[13];
                    Properties.Settings.Default.Save();
                    iAmidite = 0;
                    return;
                    // whatever you need to do with result
                }
            }
        }
        private void amidite14_Click(object sender, EventArgs e)
        {
            iAmidite = 14;

            using (am_btl_config dlg = new am_btl_config())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    //update the settings file
                    this.amidite14.Text = Properties.Settings.Default.Am_14_lbl.ToString();
                    this.tb_amproto_14.Text = Properties.Settings.Default.Am_14_prtcl;
                    amname[iAmidite] = Properties.Settings.Default.Am_14_name;
                    if (amname[iAmidite].Length > 17)
                        this.Am_14_Chem.Text = amname[iAmidite].Substring(0, 17);
                    else
                        this.Am_14_Chem.Text = amname[14];

                    Properties.Settings.Default.Save();
                    iAmidite = 0;
                    return;
                    // whatever you need to do with result
                }
            }
        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SeNAAboutBox dlg = new SeNAAboutBox())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    return;
                    // whatever you need to do with result
                }
            }
         }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();          
        }

        private void Menu_Help_Click(object sender, EventArgs e)
        {
            Process.Start(globals.Help_Path);
        }

        private void cb_AutoScale_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_AutoScale.Checked)
                lbl_Note.Visible = true;
            else
                lbl_Note.Visible = false;
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
          
            if(cb_AutoScale.Checked)
            {
                Properties.Settings.Default.Autoscale_Amidites = true;
                globals.AutoScaleAmidites = true;
            }
            else
            {
                Properties.Settings.Default.Autoscale_Amidites = false;
                globals.AutoScaleAmidites = false;
            }
            Properties.Settings.Default.Save();
        }
        private bool CheckDups()
        {
            string[] amlabels = new string[15];

            //init array
            for (int i = 0; i < 15; i++)
                amlabels[i] = string.Empty;

            amlabels[1] = amidite1.Text;
            amlabels[2] = amidite2.Text;
            amlabels[3] = amidite3.Text;
            amlabels[4] = amidite4.Text;
            amlabels[5] = amidite5.Text;
            amlabels[6] = amidite6.Text;
            amlabels[7] = amidite7.Text;
            amlabels[8] = amidite8.Text;
            amlabels[9] = amidite9.Text;
            amlabels[10] = amidite10.Text;
            amlabels[10] = amidite11.Text;
            amlabels[12] = amidite12.Text;
            amlabels[13] = amidite13.Text;
            amlabels[14] = amidite14.Text;

            //now check each
            for (int j = 1; j < 15; j++ )
            {
                if(amlabels[j].Length > 0)
                {
                    for (int k = j + 1; k < 15; k++)
                        if (amlabels[k].Equals(amlabels[j]))
                        {
                            Debug.WriteLine("Duplicates Found at " + j.ToString() + " am = " + amlabels[j] + " With- " + k.ToString() + " am = " + amlabels[k]);
                            return (true);
                        }
                }
            }
        
            return false;
        }

        private void AmiditeConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            //check and make sure there are not duplicates
            if (CheckDups())
            {
                MessageBox.Show("Duplicates are Not Allowed\n\nPlease remove duplicates", "Duplicates Found", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.DialogResult = DialogResult.Ignore;
                e.Cancel = true;
            }
        }

        private void Menu_BaseTable_Click(object sender, EventArgs e)
        {
            using (BaseTable dlg = new BaseTable())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // whatever you need to do with result
                }
            }
        }

        private void groupBox2_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(groupBox2, "Bank 2 Amidite Configuration..", "Configure each of the bank two amidties by assigning\n a letter code, name and amidite protocol...");


        }

        private void groupBox1_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(groupBox1, "Bank 1 Amidite Configuration..", "Configure each of the bank one amidties by assigning\n a letter code, name and amidite protocol...");

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
        }

        private void btnReturn_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btnReturn, "Return..", "Return to the Main Menu saving changes made to the editor...");

        }

        private void btn_Cancel_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_Cancel, "Cancel..", "Return to the Run program without terminating...");
        }
    }
}
