using System;
using System.Data;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Configuration;
using System.Windows.Forms;

namespace SeNA80
{
    public partial class Config_Editor : Form
    {
        public bool bProtosPresent = true;
        public bool bQuickLogin = false;
        static int noOfUsers = 0;
        public string[] bases = new string[100];
        public int ibases = 0;
        public bool init = false; 


        public Config_Editor()
        {
            InitializeComponent();
            //********************************************************************************************************************************
            //            First Page Information...
            //            These are all the synthesizer constants
            //
            //*********************************************************************************************************************************
            //paths
            this.tb_LogDir.Text = globals.logfile_path;
            this.tb_ProtoDir.Text = globals.protocol_path;
            this.tb_SeqDir.Text = globals.sequence_path;
            this.tb_CSVDir.Text = globals.CSV_path;

            //Support type
            this.cb_UnivSupport.Checked = Properties.Settings.Default.UniversalSupport;

            //Dead Volume
            this.nu_DeadVol.Value = Properties.Settings.Default.DeadVol;

            //Threshold
            this.nu_Threshold.Value = (decimal)Properties.Settings.Default.Threshold;

            //Alarms
            if (Properties.Settings.Default.Am_Pres_Alarm)
            {
                cb_AlAmPres.Checked = true;
                nu_AmPresAl.Enabled = true;
            }
            else
            {
                cb_AlAmPres.Checked = false;
                nu_AmPresAl.Enabled = false;
            }

            if (Properties.Settings.Default.Reg_Pres_Alarm)
            {
                cb_AlRegPres.Checked = true;
                nu_RegPresAlm.Enabled = true;
            }
            else
            {
                cb_AlRegPres.Checked = false;
                nu_RegPresAlm.Enabled = false;
            }

            if (Properties.Settings.Default.Trityl_Alarm)
            {
                cb_AlTrityl.Checked = true;
                nu_AlTrityl.Enabled = true;
            }
            else
            {
                cb_AlTrityl.Checked = false;
                nu_AlTrityl.Enabled = false;
            }

            nu_AlTrityl.Value = (int)Properties.Settings.Default.Trityl_Amount;
            nu_AmPresAl.Value = (int)Properties.Settings.Default.Am_Pres_Amt;
            nu_RegPresAlm.Value = (int)Properties.Settings.Default.Reg_Pres_Amt;

            //logging values
            if (globals.blogging)
                this.cb_LogginYN.Checked = true;
            else
                this.cb_LogginYN.Checked = false;

            this.nUD_LogInterval.Value = (Properties.Settings.Default.Log_Interval / 1000);
            this.nUD_LogMaxLines.Value = globals.iMaxLines;

            this.nUD_MaxTritylPts.Value = Properties.Settings.Default.Max_TritylPts;

            //accessories
            if (globals.bUVTrityl)
                this.cb_UVTrityl.Checked = true;
            else
                this.cb_UVTrityl.Checked = false;

            if (globals.bCondTrityl)
                this.cb_CondTrityl.Checked = true;
            else
                this.cb_CondTrityl.Checked = false;

            if (globals.bMonPressure)
                this.cb_PresMon.Checked = true;
            else
                this.cb_PresMon.Checked = false;

            if (globals.tritylreport == 0)
                this.rb_Deblk.Checked = true;
            else if (globals.tritylreport == 1)
                this.rb_All.Checked = true;
            else if (globals.tritylreport == 2)
                this.rb_Both.Checked = true;

            this.cb_SaveBar.Checked = globals.bSaveBar;
            this.cb_SaveStrip.Checked = globals.bSaveStrip;
            this.cb_SaveHist.Checked = globals.bSaveHist;

            this.nu_PolFreq.Value = globals.polFreq;

            //set the area or height radio button
            if (globals.iAreaHeight == 0)
            {
                rb_Area.Checked = true;
                rb_Height.Checked = false;
            }
            else
            {
                rb_Area.Checked = false;
                rb_Height.Checked = true;
            }

            //flow & calibration constants
            nu_AmiditeCF.Value = (decimal)globals.dAmiditeCF;
            nu_ReagentCF.Value = (decimal)globals.dReagentCF;
            nu_AmiditePCalib.Value = (decimal)globals.dAmditePCalib;
            nu_ReagentPCalib.Value = (decimal)globals.dReagentPCalib;
            nu_AmPumpDwell.Value = (decimal)globals.iAmPumpDwell;
            nu_ReagPumpDwell.Value = (decimal)globals.iReagPumpDwell;

            //trityl bar labels
            int i = Properties.Settings.Default.def_Trityl_Label;
            switch(i)
            {
                case 0:
                    rb_nolabels.Checked = true;
                    break;
                case 1:
                    rb_HALabel.Checked = true;
                    break;
                case 2:
                    rb_StepYield.Checked = true;
                    break;
                case 3:
                    rb_TotYield.Checked = true;
                    break;
            }
            //************************************************************************************************************************************************************
            //
            //                               Second Page Default Run Protocols and Default Amidite Protocols
            //
            //*************************************************************************************************************************************************************
            //default Run Protocols
            lbl_Start.Text = globals.defStartupProtocol;
            lbl_PreRun.Text = globals.defPrepProtocol;
            lbl_Run.Text = globals.defRunProtocol;
            lbl_Post.Text = globals.defPostProtocol;

            //scaling factors
            //scaling factors
            if (globals.AutoScaleAmidites)
            {
                cb_AutoScale.Checked = true;
                gb_ScaleFactors.Enabled = true;
            }
            else
            {
                cb_AutoScale.Checked = false;
                gb_ScaleFactors.Enabled = false;
            }

            if (globals.bScaleLong)
                cb_LongCorrect.Checked = true;
            else
                cb_LongCorrect.Checked = false;

            if (globals.bDoubleFirst)
                cb_DoubleOne.Checked = true;
            else
                cb_DoubleOne.Checked = false;

            nu_02.Value = (decimal)Properties.Settings.Default.SF_02;
            nu_05.Value = (decimal)Properties.Settings.Default.SF_05;
            nu_10.Value = (decimal)Properties.Settings.Default.SF_10;
            nu_20.Value = (decimal)Properties.Settings.Default.SF_20;
            nu_30.Value = (decimal)Properties.Settings.Default.SF_30;
            nu_40.Value = (decimal)Properties.Settings.Default.SF_40;
            nu_50.Value = (decimal)Properties.Settings.Default.SF_50;

            int cindex = 0;

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

            if (globals.i12Ltr == 0)  //1 letter code
            { cindex = 0; rb_1Leter.Checked = true; }
            else
            { cindex = 1; rb_2Letter.Checked = true; }

            string[] codes = new string[ibases];

            for (int t = 0; t < ibases; t++)
            {
                string[] parts = bases[t].Split(',');
                codes[t] = parts[cindex];
            }
            //now load the list boxes
            cb_AMLtr1.Items.AddRange(codes);
            cb_AMLtr2.Items.AddRange(codes);
            cb_AMLtr3.Items.AddRange(codes);
            cb_AMLtr4.Items.AddRange(codes);
            cb_AMLtr5.Items.AddRange(codes);
            cb_AMLtr6.Items.AddRange(codes);
            cb_AMLtr7.Items.AddRange(codes);
            cb_AMLtr8.Items.AddRange(codes);
            cb_AMLtr9.Items.AddRange(codes);
            cb_AMLtr10.Items.AddRange(codes);
            cb_AMLtr11.Items.AddRange(codes);
            cb_AMLtr12.Items.AddRange(codes);
            cb_AMLtr13.Items.AddRange(codes);
            cb_AMLtr14.Items.AddRange(codes);

            //select the current letters
            //select the current letters
            if (Properties.Settings.Default.Am_1_lbl != string.Empty)
            {
                int match = FindExactString(cb_AMLtr1, Properties.Settings.Default.Am_1_lbl.ToString());
                cb_AMLtr1.SelectedIndex = match;
            }
            if (Properties.Settings.Default.Am_2_lbl != string.Empty)
            {
                int match = FindExactString(cb_AMLtr2, Properties.Settings.Default.Am_2_lbl.ToString());
                cb_AMLtr2.SelectedIndex = match;
            }
            if (Properties.Settings.Default.Am_3_lbl != string.Empty)
            {
                int match = FindExactString(cb_AMLtr3, Properties.Settings.Default.Am_3_lbl.ToString());
                cb_AMLtr3.SelectedIndex = match;
            }
            if (Properties.Settings.Default.Am_4_lbl != string.Empty)
            {
                int match = FindExactString(cb_AMLtr4, Properties.Settings.Default.Am_4_lbl.ToString());
                cb_AMLtr4.SelectedIndex = match;
            }
            if (Properties.Settings.Default.Am_5_lbl != string.Empty)
            {
                int match = FindExactString(cb_AMLtr5, Properties.Settings.Default.Am_5_lbl.ToString());
                cb_AMLtr5.SelectedIndex = match;
            }
            if (Properties.Settings.Default.Am_6_lbl != string.Empty)
            {
                int match = FindExactString(cb_AMLtr6, Properties.Settings.Default.Am_6_lbl.ToString());
                cb_AMLtr6.SelectedIndex = match;
            }
            if (Properties.Settings.Default.Am_7_lbl != string.Empty)
            {
                int match = FindExactString(cb_AMLtr7, Properties.Settings.Default.Am_7_lbl.ToString());
                cb_AMLtr7.SelectedIndex = match;
            }
            if (Properties.Settings.Default.Am_8_lbl != string.Empty)
            {
                int match = FindExactString(cb_AMLtr8, Properties.Settings.Default.Am_8_lbl.ToString());
                cb_AMLtr8.SelectedIndex = match;
            }
            if (Properties.Settings.Default.Am_9_lbl != string.Empty)
            {
                int match = FindExactString(cb_AMLtr9, Properties.Settings.Default.Am_9_lbl.ToString());
                cb_AMLtr9.SelectedIndex = match;
            }
            if (Properties.Settings.Default.Am_10_lbl != string.Empty)
            {
                int match = FindExactString(cb_AMLtr10, Properties.Settings.Default.Am_10_lbl.ToString());
                cb_AMLtr10.SelectedIndex = match;
            }
            if (Properties.Settings.Default.Am_11_lbl != string.Empty)
            {
                int match = FindExactString(cb_AMLtr11, Properties.Settings.Default.Am_11_lbl.ToString());
                cb_AMLtr11.SelectedIndex = match;
            }
            if (Properties.Settings.Default.Am_12_lbl != string.Empty)
            {
                int match = FindExactString(cb_AMLtr12, Properties.Settings.Default.Am_12_lbl.ToString());
                cb_AMLtr12.SelectedIndex = match;
            }
            if (Properties.Settings.Default.Am_13_lbl != string.Empty)
            {
                int match = FindExactString(cb_AMLtr13, Properties.Settings.Default.Am_13_lbl.ToString());
                cb_AMLtr13.SelectedIndex = match;
            }
            if (Properties.Settings.Default.Am_14_lbl != string.Empty)
            {
                int match = FindExactString(cb_AMLtr14, Properties.Settings.Default.Am_14_lbl.ToString());
                cb_AMLtr14.SelectedIndex = match;
            }
            //fill the list boxes with the avialable protocols
            string[] amiditeprotos = Directory.EnumerateFiles(globals.protocol_path, "*.*")
                 .Select(p => Path.GetFileName(p))
                 .Where(s => s.EndsWith(".apr", StringComparison.OrdinalIgnoreCase)).ToArray();
            //string[] amiditeprotos = Directory.GetFiles(globals.protocol_path, "*.apr").Select(p =>Path.GetFileName(p).ToArray());

            if (amiditeprotos.Length < 1)
            {
                bProtosPresent = false;
                MessageBox.Show("You must create on or more amidite protocols\nThese are needed to assign to each amidite", "Amidite Protocols");
            }
            
            //Last select the protocol for each list box if it is configured
            if (bProtosPresent)
            {
                FillListBoxes(amiditeprotos);

                if(Properties.Settings.Default.Am_1_prtcl.Length > 3)
                   this.cb_AMPrtcl1.SelectedIndex = this.cb_AMPrtcl1.FindStringExact(Properties.Settings.Default.Am_1_prtcl);
                if (Properties.Settings.Default.Am_2_prtcl.Length > 3)
                    this.cb_AMPrtcl2.SelectedIndex = this.cb_AMPrtcl2.FindStringExact(Properties.Settings.Default.Am_2_prtcl);
                if (Properties.Settings.Default.Am_3_prtcl.Length > 3)
                    this.cb_AMPrtcl3.SelectedIndex = this.cb_AMPrtcl3.FindStringExact(Properties.Settings.Default.Am_3_prtcl);
                if (Properties.Settings.Default.Am_4_prtcl.Length > 3)
                    this.cb_AMPrtcl4.SelectedIndex = this.cb_AMPrtcl4.FindStringExact(Properties.Settings.Default.Am_4_prtcl);
                if (Properties.Settings.Default.Am_5_prtcl.Length > 3)
                    this.cb_AMPrtcl5.SelectedIndex = this.cb_AMPrtcl5.FindStringExact(Properties.Settings.Default.Am_5_prtcl);
                if (Properties.Settings.Default.Am_6_prtcl.Length > 3)
                    this.cb_AMPrtcl6.SelectedIndex = this.cb_AMPrtcl6.FindStringExact(Properties.Settings.Default.Am_6_prtcl);
                if (Properties.Settings.Default.Am_7_prtcl.Length > 3)
                    this.cb_AMPrtcl7.SelectedIndex = this.cb_AMPrtcl7.FindStringExact(Properties.Settings.Default.Am_7_prtcl);
                if (Properties.Settings.Default.Am_8_prtcl.Length > 3)
                    this.cb_AMPrtcl8.SelectedIndex = this.cb_AMPrtcl8.FindStringExact(Properties.Settings.Default.Am_8_prtcl);
                if (Properties.Settings.Default.Am_9_prtcl.Length > 3)
                    this.cb_AMPrtcl9.SelectedIndex = this.cb_AMPrtcl9.FindStringExact(Properties.Settings.Default.Am_9_prtcl);
                if (Properties.Settings.Default.Am_10_prtcl.Length > 3)
                    this.cb_AMPrtcl10.SelectedIndex = this.cb_AMPrtcl10.FindStringExact(Properties.Settings.Default.Am_10_prtcl);
                if (Properties.Settings.Default.Am_11_prtcl.Length > 3)
                    this.cb_AMPrtcl11.SelectedIndex = this.cb_AMPrtcl11.FindStringExact(Properties.Settings.Default.Am_11_prtcl);
                if (Properties.Settings.Default.Am_12_prtcl.Length > 3)
                    this.cb_AMPrtcl12.SelectedIndex = this.cb_AMPrtcl12.FindStringExact(Properties.Settings.Default.Am_12_prtcl);
                if (Properties.Settings.Default.Am_13_prtcl.Length > 3)
                    this.cb_AMPrtcl13.SelectedIndex = this.cb_AMPrtcl13.FindStringExact(Properties.Settings.Default.Am_13_prtcl);
                if (Properties.Settings.Default.Am_14_prtcl.Length > 3)
                   this.cb_AMPrtcl14.SelectedIndex = this.cb_AMPrtcl14.FindStringExact(Properties.Settings.Default.Am_14_prtcl);
            }
            //***************************************************************************************************************************
            //                                   Finally Administrative Settings
            //
            //***************************************************************************************************************************
            //fist get the array of users from the file, the file is stored in the same directory as the program config file
            ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
            string AppDataPath = Application.CommonAppDataPath;
            int index = AppDataPath.IndexOf("Ltd.");
            string newApp = AppDataPath.Substring(0, index + 4);

            string passpath = newApp + "\\RiboBio8\\ribobio8sec.bin";

            if (File.Exists(passpath))
            {
                DataBaseIO.ReadUsers();
            }
            else // create a starter file with 4 users (Admin, Service, User 1, User 2)
            {

                DataBaseIO.WriteInitUsers();

            }//done with else
            //now populate the list boxes and other information.
            combo_LoginUser.Items.Add("[none]");
            int cntr = 0;
            foreach (string user in globals.Users)
            {
                if (string.IsNullOrEmpty(user)) { noOfUsers = cntr; break; }

                //append to the combobox and the list box, select the first user and the rights for the first user
                string[] userparts = user.Split(',');

                //don't add service user to list boxes
                if (!userparts[0].Contains("Service"))
                {
                    combo_LoginUser.Items.Add(userparts[0]);
                    lb_Users.Items.Add(userparts[0]);

                    //set the radiobuttons for the first user in the list
                    if (cntr == 0)
                    {
                        if (userparts[2].Contains("Admin")) { rb_Admin.Checked = true; }
                        if (userparts[2].Contains("1")) { rb_User1.Checked = true; }
                        if (userparts[2].Contains("2")) { rb_User2.Checked = true; }
                    }
                    cntr++;
                }
            }

            //Login Options
            if (globals.bQuickLogin)
            {
                cb_QuickLogin.Checked = true;
                combo_LoginUser.SelectedIndex = combo_LoginUser.FindString(globals.Login_User);
            }
            //select the first user in the user box
            lbl_UserCnt.Text = lb_Users.Items.Count.ToString("0");
            lb_Users.SelectedIndex = 0;
            lb_Users.Focus();
            init = true;
        }


        //function to look for upper or lower case letter in a combo box and return the index of the exact case sensitive match.
        public static int FindExactString(ComboBox cb, string s)
        {
            cb.SelectedIndex = cb.FindStringExact(s);
            Debug.WriteLine("Selected text is " + cb.Text + "  s is " + s);
            if (String.CompareOrdinal(cb.Text, s) == 0)  //if it is a case senstive match return the index
            {
                Debug.WriteLine("Found First Try at " + cb.SelectedIndex.ToString());
                return cb.SelectedIndex;
            }
            else
            {
                int curSel = cb.SelectedIndex;
                cb.SelectedIndex = cb.FindStringExact(s, curSel + 1); //find the next occurance, assume can only be small or upper case
                if (String.CompareOrdinal(cb.Text, s) == 0)
                {
                    Debug.WriteLine("Found Second Try at " + cb.SelectedIndex.ToString() + "   Starting from " + curSel.ToString());
                    return cb.SelectedIndex;
                }
                else
                    return 0;
            }
        }
        private void FillListBoxes(String [] files)
        {
            //could add range, I chose to add each file one at a time
            foreach (string file in files)
            {
                this.cb_AMPrtcl1.Items.Add(file);
                this.cb_AMPrtcl2.Items.Add(file);
                this.cb_AMPrtcl3.Items.Add(file);
                this.cb_AMPrtcl4.Items.Add(file);
                this.cb_AMPrtcl5.Items.Add(file);
                this.cb_AMPrtcl6.Items.Add(file);
                this.cb_AMPrtcl7.Items.Add(file);
                this.cb_AMPrtcl8.Items.Add(file);
                this.cb_AMPrtcl9.Items.Add(file);
                this.cb_AMPrtcl10.Items.Add(file);
                this.cb_AMPrtcl11.Items.Add(file);
                this.cb_AMPrtcl12.Items.Add(file);
                this.cb_AMPrtcl13.Items.Add(file);
                this.cb_AMPrtcl14.Items.Add(file);
            }
        }

        private void tb_ProtoDir_TextChanged(object sender, EventArgs e)
        {
        }

        private void tb_SeqDir_TextChanged(object sender, EventArgs e)
        {
         
        }

        private void tb_LogDir_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void tb_CSVDir_TextChanged(object sender, EventArgs e)
        {

        }


        private void tb_ProtoDir_MouseClick(object sender, MouseEventArgs e)
        {
            folderBrowserDialog1.SelectedPath = Properties.Settings.Default.Program_Path;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.tb_ProtoDir.Text = folderBrowserDialog1.SelectedPath + '\\';
            }

        }

        private void tb_SeqDir_MouseClick(object sender, MouseEventArgs e)
        {
            folderBrowserDialog1.SelectedPath = Properties.Settings.Default.Program_Path;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.tb_SeqDir.Text = folderBrowserDialog1.SelectedPath + '\\';
            }

        }

        private void tb_LogDir_MouseClick(object sender, MouseEventArgs e)
        {
            folderBrowserDialog1.SelectedPath = Properties.Settings.Default.Program_Path;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.tb_LogDir.Text = folderBrowserDialog1.SelectedPath + '\\';
            }

        }
        private void tb_CSVDir_MouseClick(object sender, MouseEventArgs e)
        {

            folderBrowserDialog1.SelectedPath = Properties.Settings.Default.Program_Path;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.tb_CSVDir.Text = folderBrowserDialog1.SelectedPath + '\\';
            }
        }

        private void cb_LogginYN_CheckedChanged(object sender, EventArgs e)
        {
            if(cb_LogginYN.Checked)
            {
                //enable the lines and frequency
                this.nUD_LogInterval.Enabled = true;
                this.nUD_LogMaxLines.Enabled = true;
            }
            else
            {
                //enable the lines and frequency
                this.nUD_LogInterval.Enabled = false;
                this.nUD_LogMaxLines.Enabled = false;
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            //now update the variables and save to the Setting File
            //paths
            globals.logfile_path = this.tb_LogDir.Text;
            Properties.Settings.Default.Log_Path = this.tb_LogDir.Text;

            globals.protocol_path = this.tb_ProtoDir.Text;
            Properties.Settings.Default.Protocol_Path = this.tb_ProtoDir.Text;

            globals.sequence_path = this.tb_SeqDir.Text;
            Properties.Settings.Default.Sequence_Path = this.tb_SeqDir.Text;

            globals.CSV_path = this.tb_CSVDir.Text;
            Properties.Settings.Default.CSV_Path = this.tb_CSVDir.Text;

            Properties.Settings.Default.Save();

            //Universal SUpport
            if (cb_UnivSupport.Checked)
                Properties.Settings.Default.UniversalSupport = true;
            else
                Properties.Settings.Default.UniversalSupport = false;

            //dead volume
            Properties.Settings.Default.DeadVol = (int)nu_DeadVol.Value;

            //threshold
            Properties.Settings.Default.Threshold = (double)nu_Threshold.Value;

            //now save all
            Properties.Settings.Default.Save();

            if (cb_AlAmPres.Checked)
            {
                Properties.Settings.Default.Am_Pres_Alarm = true;
                globals.bAmPresAlarm = true;
            }
            else
            {
                Properties.Settings.Default.Am_Pres_Alarm = false;
                globals.bAmPresAlarm = false;
            }

            if (cb_AlRegPres.Checked)
            {
                Properties.Settings.Default.Reg_Pres_Alarm = true;
                globals.bReagPresAlarm = true;
            }
            else
            {
                Properties.Settings.Default.Reg_Pres_Alarm = false;
                globals.bReagPresAlarm = false;
            }

            if (cb_AlTrityl.Checked)
            {
                Properties.Settings.Default.Trityl_Alarm = true;
                globals.bTritylAlarm = true;
            }
            else
            {
                Properties.Settings.Default.Trityl_Alarm = false;
                globals.bTritylAlarm = false;
            }

            Properties.Settings.Default.Trityl_Amount = (int)nu_AlTrityl.Value;
            globals.iTritylAlarmAmt = (int)nu_AlTrityl.Value;
            Properties.Settings.Default.Am_Pres_Amt = (int)nu_AmPresAl.Value;
            globals.iAmPresAmt = (int)nu_AmPresAl.Value;
            Properties.Settings.Default.Reg_Pres_Amt = (int)nu_RegPresAlm.Value;
            globals.iRgtPresAmt = (int)nu_RegPresAlm.Value;

            //save checkboxes
            globals.bSaveBar = cb_SaveBar.Checked;
            globals.bSaveStrip = cb_SaveStrip.Checked;
            globals.bSaveHist = cb_SaveHist.Checked;

            Properties.Settings.Default.SaveBar = globals.bSaveBar;
            Properties.Settings.Default.SaveStrip = globals.bSaveStrip;
            Properties.Settings.Default.SaveHist = globals.bSaveHist;
            
            //now save all
            Properties.Settings.Default.Save();

            
            //logging values
            if (this.cb_LogginYN.Checked)
            {
                globals.blogging = true;
                Properties.Settings.Default.Loggin_YN = true;
            }
            else
            {
                globals.blogging = false;
                Properties.Settings.Default.Loggin_YN = false;
            }

            Properties.Settings.Default.Log_Interval = (int)this.nUD_LogInterval.Value * 1000;
            globals.iLogInterval = (int)this.nUD_LogInterval.Value * 1000;

            Properties.Settings.Default.Max_Lines = (int)this.nUD_LogMaxLines.Value;
            globals.iMaxLines = (int)this.nUD_LogMaxLines.Value;

            Properties.Settings.Default.Max_TritylPts = (int)this.nUD_MaxTritylPts.Value;
            globals.maxtritylpts = (int)this.nUD_MaxTritylPts.Value;

            Properties.Settings.Default.Save();

            //accessories
            if (this.cb_UVTrityl.Checked)
            {
                globals.bUVTrityl = true;
                Properties.Settings.Default.UV_Trityl_Inst = true;
            }
            else
            {
                globals.bUVTrityl = false;
                Properties.Settings.Default.UV_Trityl_Inst = false;
            }

            if (this.cb_CondTrityl.Checked)
            {
                globals.bCondTrityl = true;
                Properties.Settings.Default.Cond_Trityl_Inst = true;
            }
            else
            {
                globals.bCondTrityl = false;
                Properties.Settings.Default.Cond_Trityl_Inst = false;
            }

            if (this.cb_PresMon.Checked)
            {
                globals.bMonPressure = true;
                Properties.Settings.Default.Pres_Mon_Inst = true;
            }
            else
            {
                globals.bMonPressure = false;
                Properties.Settings.Default.Pres_Mon_Inst = false;
            }
            int value = 0;
            if (rb_Deblk.Checked)
                value = 0;
            else if (rb_All.Checked)
                value = 1;
            else if (rb_Both.Checked)
                value = 2;

            globals.tritylreport = value;
            Properties.Settings.Default.Report_Trity = value;

            globals.polFreq = (int)nu_PolFreq.Value;
            Properties.Settings.Default.Pol_Freq = globals.polFreq;

            if (rb_Area.Checked)
            {
                Properties.Settings.Default.Rpt_AreaHeight = 0;
                globals.iAreaHeight = 0;
            }
            else
            {
                Properties.Settings.Default.Rpt_AreaHeight = 1;
                globals.iAreaHeight = 1;
            }

            Properties.Settings.Default.Save();

            //calibration numbers
            globals.dAmiditeCF = Convert.ToDouble(nu_AmiditeCF.Value);
            Properties.Settings.Default.AmiditeFC = globals.dAmiditeCF;

            globals.dReagentCF = Convert.ToDouble(nu_ReagentCF.Value);
            Properties.Settings.Default.ReagentFC = globals.dReagentCF;

            globals.iAmPumpDwell = Convert.ToInt32(nu_AmPumpDwell.Value);
            Properties.Settings.Default.AmPumpDwell = globals.iAmPumpDwell;

            globals.iReagPumpDwell = Convert.ToInt32(nu_ReagPumpDwell.Value);
            Properties.Settings.Default.ReagPumpDwell = globals.iReagPumpDwell;

            globals.dAmditePCalib = Convert.ToDouble(nu_AmiditePCalib.Value);
            Properties.Settings.Default.AmiditePCalib = globals.dAmditePCalib;

            globals.dReagentPCalib = Convert.ToDouble(nu_ReagentPCalib.Value);
            Properties.Settings.Default.ReagentPCalib = globals.dReagentPCalib;

            Properties.Settings.Default.Save();
            //trityl bar labels

            if (rb_nolabels.Checked)
                Properties.Settings.Default.def_Trityl_Label = 0;
             else if(rb_HALabel.Checked)
                Properties.Settings.Default.def_Trityl_Label = 1;
            else if(rb_StepYield.Checked)
                Properties.Settings.Default.def_Trityl_Label = 2;
            else if(rb_TotYield.Checked)
                Properties.Settings.Default.def_Trityl_Label = 3;
            Properties.Settings.Default.Save();

            //default Run Protocols
            globals.defStartupProtocol = lbl_Start.Text;
            globals.defPrepProtocol = lbl_PreRun.Text;
            globals.defRunProtocol = lbl_Run.Text;
            globals.defPostProtocol = lbl_Post.Text;
            Properties.Settings.Default.def_Startup_Protocol = globals.defStartupProtocol;
            Properties.Settings.Default.def_Prep_Protocol = globals.defPrepProtocol;
            Properties.Settings.Default.def_Run_Protocol = globals.defRunProtocol;
            Properties.Settings.Default.def_Post_Protocol = globals.defPostProtocol;

            Properties.Settings.Default.Save();

            //scaling factors
            if (cb_AutoScale.Checked)
            { Properties.Settings.Default.Autoscale_Amidites = true; globals.AutoScaleAmidites = true; }
            else
            { Properties.Settings.Default.Autoscale_Amidites = false; globals.AutoScaleAmidites = false; }

            if (cb_LongCorrect.Checked) { Properties.Settings.Default.Scale_Long = true; globals.bScaleLong = true; }
            else { Properties.Settings.Default.Scale_Long = false; globals.bScaleLong = false; }

            if (cb_DoubleOne.Checked) { Properties.Settings.Default.Double_First = true; globals.bDoubleFirst = true; }
            else { Properties.Settings.Default.Double_First = false; globals.bDoubleFirst = false; }

            Properties.Settings.Default.SF_02 = (double)nu_02.Value;
            Properties.Settings.Default.SF_05 = (double)nu_05.Value;
            Properties.Settings.Default.SF_10 = (double)nu_10.Value;
            Properties.Settings.Default.SF_20 = (double)nu_20.Value;
            Properties.Settings.Default.SF_30 = (double)nu_30.Value;
            Properties.Settings.Default.SF_40 = (double)nu_40.Value;
            Properties.Settings.Default.SF_50 = (double)nu_50.Value;
            Properties.Settings.Default.Save();

            //save the preferred format
            if (rb_1Leter.Checked)
                Properties.Settings.Default.Ltr_12 = 0;
            else
                Properties.Settings.Default.Ltr_12 = 1;
            Properties.Settings.Default.Save();

            //finally the amidite configurations
            if (cb_AMLtr1.SelectedItem != null)
                Properties.Settings.Default.Am_1_lbl = cb_AMLtr1.SelectedItem.ToString(); 
           // MessageBox.Show(cb_AMLtr1.SelectedItem.ToString());
            if (cb_AMLtr2.SelectedItem != null)
                Properties.Settings.Default.Am_2_lbl = cb_AMLtr2.SelectedItem.ToString();
            if (cb_AMLtr3.SelectedItem != null)
                Properties.Settings.Default.Am_3_lbl = cb_AMLtr3.SelectedItem.ToString();
            if (cb_AMLtr4.SelectedItem != null)
                Properties.Settings.Default.Am_4_lbl = cb_AMLtr4.SelectedItem.ToString();
            if (cb_AMLtr5.SelectedItem != null)
                Properties.Settings.Default.Am_5_lbl = cb_AMLtr5.SelectedItem.ToString();
            if (cb_AMLtr6.SelectedItem != null)
                Properties.Settings.Default.Am_6_lbl = cb_AMLtr6.SelectedItem.ToString();
            if (cb_AMLtr7.SelectedItem != null)
                Properties.Settings.Default.Am_7_lbl = cb_AMLtr7.SelectedItem.ToString();
            if (cb_AMLtr8.SelectedItem != null)
                Properties.Settings.Default.Am_8_lbl = cb_AMLtr8.SelectedItem.ToString();
            if (cb_AMLtr9.SelectedItem != null)
                Properties.Settings.Default.Am_9_lbl = cb_AMLtr9.SelectedItem.ToString();
            if (cb_AMLtr10.SelectedItem != null)
                Properties.Settings.Default.Am_10_lbl = cb_AMLtr10.SelectedItem.ToString();
            if (cb_AMLtr11.SelectedItem != null)
                Properties.Settings.Default.Am_11_lbl = cb_AMLtr11.SelectedItem.ToString();
            if (cb_AMLtr12.SelectedItem != null)
                Properties.Settings.Default.Am_12_lbl = cb_AMLtr12.SelectedItem.ToString();
            if (cb_AMLtr13.SelectedItem != null)
                Properties.Settings.Default.Am_13_lbl = cb_AMLtr13.SelectedItem.ToString();
            if (cb_AMLtr14.SelectedItem != null)
                Properties.Settings.Default.Am_14_lbl = cb_AMLtr14.SelectedItem.ToString();
            Properties.Settings.Default.Save();

            if(this.cb_AMPrtcl1.SelectedItem != null && this.cb_AMPrtcl1.SelectedItem.ToString().Length > 3)
               Properties.Settings.Default.Am_1_prtcl = this.cb_AMPrtcl1.SelectedItem.ToString();
            if (this.cb_AMPrtcl2.SelectedItem != null && this.cb_AMPrtcl2.SelectedItem.ToString().Length > 3)
                Properties.Settings.Default.Am_2_prtcl = this.cb_AMPrtcl2.SelectedItem.ToString();
            if (this.cb_AMPrtcl3.SelectedItem != null && this.cb_AMPrtcl3.SelectedItem.ToString().Length > 3)
                Properties.Settings.Default.Am_3_prtcl = this.cb_AMPrtcl3.SelectedItem.ToString();
            if (this.cb_AMPrtcl4.SelectedItem != null && this.cb_AMPrtcl4.SelectedItem.ToString().Length > 3)
                Properties.Settings.Default.Am_4_prtcl = this.cb_AMPrtcl4.SelectedItem.ToString();
            if (this.cb_AMPrtcl5.SelectedItem != null && this.cb_AMPrtcl5.SelectedItem.ToString().Length > 3)
                Properties.Settings.Default.Am_5_prtcl = this.cb_AMPrtcl5.SelectedItem.ToString();
            if (this.cb_AMPrtcl6.SelectedItem != null && this.cb_AMPrtcl6.SelectedItem.ToString().Length > 3)
                Properties.Settings.Default.Am_6_prtcl = this.cb_AMPrtcl6.SelectedItem.ToString();
            if (this.cb_AMPrtcl7.SelectedItem != null && this.cb_AMPrtcl7.SelectedItem.ToString().Length > 3)
                Properties.Settings.Default.Am_7_prtcl = this.cb_AMPrtcl7.SelectedItem.ToString();
            if (this.cb_AMPrtcl8.SelectedItem != null && this.cb_AMPrtcl8.SelectedItem.ToString().Length > 3)
                Properties.Settings.Default.Am_8_prtcl = this.cb_AMPrtcl8.SelectedItem.ToString();
            if (this.cb_AMPrtcl9.SelectedItem != null && this.cb_AMPrtcl9.SelectedItem.ToString().Length > 3)
                Properties.Settings.Default.Am_9_prtcl = this.cb_AMPrtcl9.SelectedItem.ToString();
            if (this.cb_AMPrtcl10.SelectedItem != null && this.cb_AMPrtcl10.SelectedItem.ToString().Length > 3)
                Properties.Settings.Default.Am_10_prtcl = this.cb_AMPrtcl10.SelectedItem.ToString();
            if (this.cb_AMPrtcl11.SelectedItem != null && this.cb_AMPrtcl11.SelectedItem.ToString().Length > 3)
                Properties.Settings.Default.Am_11_prtcl = this.cb_AMPrtcl11.SelectedItem.ToString();
            if (this.cb_AMPrtcl12.SelectedItem != null && this.cb_AMPrtcl12.SelectedItem.ToString().Length > 3)
                Properties.Settings.Default.Am_12_prtcl = this.cb_AMPrtcl12.SelectedItem.ToString();
            if (this.cb_AMPrtcl13.SelectedItem != null && this.cb_AMPrtcl13.SelectedItem.ToString().Length > 3)
                Properties.Settings.Default.Am_13_prtcl = this.cb_AMPrtcl13.SelectedItem.ToString();
            if (this.cb_AMPrtcl14.SelectedItem != null && this.cb_AMPrtcl14.SelectedItem.ToString().Length > 3)
                Properties.Settings.Default.Am_14_prtcl = this.cb_AMPrtcl14.SelectedItem.ToString();
                Properties.Settings.Default.Save();

            //login information *************finish later ************************
            if (cb_QuickLogin.Checked)
            {
                globals.bQuickLogin = true;
                Properties.Settings.Default.LoginUser = combo_LoginUser.Text;
                Properties.Settings.Default.QuickLogin = true;
                Properties.Settings.Default.LoginUser =  combo_LoginUser.Text;
            }
            else
            {
                Properties.Settings.Default.QuickLogin = false;
                Properties.Settings.Default.LoginUser =  "[none]";
            }
            //write the User Binary File
            if (DataBaseIO.WriteFinalUsers(globals.Users))
               MessageBox.Show("Databases Updated\n\nAfter Changing System Configuration\nYou should close the  application and restart it", "Restart Recommended", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {

        }

        private void cb_AlTrityl_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_AlTrityl.Checked)
                nu_AlTrityl.Enabled = true;
            else
                nu_AlTrityl.Enabled = false;
        }

        private void cb_AlAmPres_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_AlAmPres.Checked)
                nu_AmPresAl.Enabled = true;
            else
                nu_AmPresAl.Enabled = false;
        }

        private void cb_AlRegPres_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_AlRegPres.Checked)
                nu_RegPresAlm.Enabled = true;
            else
                nu_RegPresAlm.Enabled = false;
        }

        private void Config_Editor_Load(object sender, EventArgs e)
        {

        }

        private void btn_LoadProtocols_Click(object sender, EventArgs e)
        {
            using (Load_Protocols dlg = new Load_Protocols())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // get the three list box items and set them here
                    this.lbl_Start.Text = dlg.cb_Start.SelectedItem.ToString();
                    this.lbl_PreRun.Text = dlg.cb_PreRun.SelectedItem.ToString();
                    this.lbl_Run.Text = dlg.cb_Run.SelectedItem.ToString();
                    this.lbl_Post.Text = dlg.cb_PostRun.SelectedItem.ToString();

                    return;
                    // whatever you need to do with result
                }
                else // pressed Cancel so clear everything and make the user select again
                {
                    this.lbl_Start.Text = "";
                    this.lbl_PreRun.Text = "";
                    this.lbl_Run.Text = "";
                    this.lbl_Post.Text = "";


                }
            }
        }

        /// <summary>
        /// User Administration Functions
        /// Add User - Add a new User
        /// Edit User - Change Existing Rights for a user
        /// Delete User - Delete a User from the Sysem
        /// </summary>
        /// Also includes commands for updating quick user login... 
        private void cb_QuickLogin_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_QuickLogin.Checked)
            {
                bQuickLogin = true;
                combo_LoginUser.Enabled = true;
            }
            else
            {
                bQuickLogin = false;
                combo_LoginUser.Enabled = false;
            }

        }


        private void lb_Users_SelectedIndexChanged(object sender, EventArgs e)
        {
            //clear the list boxes
            tb_CurPass.Text = String.Empty;
            tb_NewPass1.Text = String.Empty;
            tb_NewPass2.Text = String.Empty;

            //get the index of the currently selected usere
            string user = lb_Users.Text;

            int idx = 0;
            for (int i = 0; i <= noOfUsers; i++)
                if (globals.Users[i].Contains(user)) { idx = i; break; }
            
            //now get the user from users
            user = globals.Users[idx];

            string[] userparts = user.Split(',');

            if (userparts[2].Contains("Admin")) { rb_Admin.Checked = true; }
            if (userparts[2].Contains("1")) { rb_User1.Checked = true; }
            if (userparts[2].Contains("2")) { rb_User2.Checked = true; }

        }

        private void btn_NewUser_Click(object sender, EventArgs e)
        {
            //show an add dialog - get the information, then update the screens
            PS_UserManagemet nu = new PS_UserManagemet();

            DialogResult dr = nu.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                nu.Close();
            }
            else if (dr == DialogResult.OK)
            {  //must be a unique name

                //get the data
                string un = nu.tb_UserName.Text;
                for (int j = 0; j <= noOfUsers; j++)
                {
                    if (globals.Users[j].Contains(un))
                    {
                        MessageBox.Show("User Already Exists\n\nPlease Try Again", "Validation Failed",
                                        MessageBoxButtons.OK, MessageBoxIcon.Stop); return;
                    }
                }
                string pass = nu.tb_NewPWD.Text;
                string level = String.Empty;
                if (nu.rb_NewAdmin.Checked)
                {
                    level = "Administrator";
                    rb_Admin.Checked = true;
                }
                if (nu.rb_NewUser1.Checked)
                {
                    level = "User 1";
                    rb_User1.Checked = true;
                }
                if (nu.rb_NewUser2.Checked)
                {
                    level = "User 2";
                    rb_User2.Checked = true;
                }

                string expires = String.Empty;
                if (nu.chk_passexp.Checked)
                    expires = "false";
                else
                {
                    int daystoexp = 0;
                    daystoexp = Convert.ToInt32((DateTime.Now - globals.dtStart).TotalDays) + 365;
                    expires = "true," + daystoexp.ToString("0");
                }
                string combined = un + "," + pass + "," + level + "," + expires;

                //now add the string to the list
                noOfUsers = noOfUsers + 1;
                globals.Users[noOfUsers] = combined;

                int i = lb_Users.Items.Add(un);
                lb_Users.SelectedIndex = i;
                lbl_UserCnt.Text = lb_Users.Items.Count.ToString("0");
                lb_Users.Focus();
                nu.Close();
            }
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            //take the information and update the User array == store to disk only if the 
            //user presses Apply
            //find the user in the User array
            string user = lb_Users.Text;

            int idx = 0;
            for (int i = 0; i <= noOfUsers; i++)
                if (globals.Users[i].Contains(user)) { idx = i; break; }

            string[] userparts = globals.Users[idx].Split(',');
            //now gather the changes...first check to see if there is passwords in the text boxes
            if (tb_CurPass.Text.Length > 0)  //they have put in their current password
            {
                if (tb_CurPass.Text.Equals(userparts[1]))
                {
                    if (tb_NewPass1.Text.Equals(tb_NewPass2.Text))
                        userparts[1] = tb_NewPass2.Text;
                    else
                    { MessageBox.Show("New Passwords Do Not Match", "Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Hand); return; }
                }
                else
                { MessageBox.Show("Current Password Does Not Match Password in the Database", "Password Missmatch", MessageBoxButtons.OK, MessageBoxIcon.Hand); return; }
            }

            //get radio button information
            if (rb_Admin.Checked)
                userparts[2] = "Administrator";
            if (rb_User1.Checked)
                userparts[2] = "User 1";
            if (rb_User2.Checked)
                userparts[2] = "User 2";

            //put the string together
            user = userparts[0] + "," + userparts[1] + "," + userparts[2] + "," + userparts[3];

            //now stuff it into the array
            globals.Users[idx] = user;

            MessageBox.Show("Changes Made to USer - " + user[0] + "\n\nRemember to press Apply to save changes to the database", "Changes Complete", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return;
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (lb_Users.Items.Count < 3)
            {
                MessageBox.Show("You must keep at least two users\n\nPlease add one then you can delete one",
                  "Minimum Reached", MessageBoxButtons.OK, MessageBoxIcon.Hand); return;
            }

            string todel = lb_Users.Text;
            int idel = 0;

            for (int i = 0; i <= noOfUsers; i++)
                if (globals.Users[i].Contains(todel)) { idel = i; break; } //store the index to be deleted

            //create a temp array
            string[] temp = new string[noOfUsers];
            //now copy all but the
            int cntr = 0;
            for (int i = 0; i <= noOfUsers; i++)
            {
                if (i == idel)
                {
                    //don't do anything
                }
                else
                {
                    temp[cntr] = globals.Users[i];
                    cntr++;
                }
            }
            noOfUsers = noOfUsers - 1;
            //now reinitialize the Users array
            globals.Users = new string[500];
            for (int i = 0; i <= noOfUsers; i++)
                globals.Users[i] = temp[i];
            //finally reload the listbox
            combo_LoginUser.Items.Clear();
            lb_Users.Items.Clear();
            for (int j = 0; j <= noOfUsers; j++)
            {
                string[] parts = globals.Users[j].Split(',');

                //don't add service user to list boxes
                if (!parts[0].Contains("Service"))
                {
                    combo_LoginUser.Items.Add(parts[0]);
                    lb_Users.Items.Add(parts[0]);

                    //set the radiobuttons for the first user in the list
                    if (cntr == 0)
                    {
                        if (parts[2].Contains("Admin")) { rb_Admin.Checked = true; }
                        if (parts[2].Contains("1")) { rb_User1.Checked = true; }
                        if (parts[2].Contains("2")) { rb_User2.Checked = true; }
                    }
                    cntr++;
                }
            }
            //reselect the focused items
            combo_LoginUser.SelectedIndex = combo_LoginUser.FindString(globals.Login_User);
            lb_Users.SelectedIndex = 0;
            lb_Users.Focus();
            lbl_UserCnt.Text = lb_Users.Items.Count.ToString("0");
            return;
        }
        
        private void combo_LoginUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            globals.Login_User = combo_LoginUser.Text;
        }

        private void btn_Apply_Click(object sender, EventArgs e)
        {
            //write the User Binary File
            if (DataBaseIO.WriteFinalUsers(globals.Users))
                MessageBox.Show("Database Updated Successfully", "Saved", MessageBoxButtons.OK);
        }

        private void cb_AutoScale_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_AutoScale.Checked)
                gb_ScaleFactors.Enabled = true;
            else
                gb_ScaleFactors.Enabled = false;
        }
        //base table functions
        private void btn_BaseTable_Click(object sender, EventArgs e)
        {
            using (BaseTable dlg = new BaseTable())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    
                    // whatever you need to do with result
                }
            }
            if(globals.bDirtyBase)
            {
                ReloadCECBs();
                globals.bDirtyBase = false;
            }
        }
        private void ReloadCECBs()
        {
            int[] selected = new int[14];

            //clear the list boxes
            selected[0] = cb_AMLtr1.SelectedIndex;
            cb_AMLtr1.Items.Clear();
            cb_AMLtr1.Text = "";
            selected[1] = cb_AMLtr2.SelectedIndex;
            cb_AMLtr2.Items.Clear();
            cb_AMLtr2.Text = "";
            selected[2] = cb_AMLtr3.SelectedIndex;
            cb_AMLtr3.Items.Clear();
            cb_AMLtr3.Text = "";
            selected[3] = cb_AMLtr4.SelectedIndex;
            cb_AMLtr4.Items.Clear();
            cb_AMLtr4.Text = "";
            selected[4] = cb_AMLtr5.SelectedIndex;
            cb_AMLtr5.Items.Clear();
            cb_AMLtr5.Text = "";
            selected[5] = cb_AMLtr6.SelectedIndex;
            cb_AMLtr6.Items.Clear();
            cb_AMLtr6.Text = "";
            selected[6] = cb_AMLtr7.SelectedIndex;
            cb_AMLtr7.Items.Clear();
            cb_AMLtr7.Text = "";
            selected[7] = cb_AMLtr8.SelectedIndex;
            cb_AMLtr8.Items.Clear();
            cb_AMLtr8.Text = "";
            selected[8] = cb_AMLtr9.SelectedIndex;
            cb_AMLtr9.Items.Clear();
            cb_AMLtr9.Text = "";
            selected[9] = cb_AMLtr10.SelectedIndex;
            cb_AMLtr10.Items.Clear();
            cb_AMLtr10.Text = "";
            selected[10] = cb_AMLtr11.SelectedIndex;
            cb_AMLtr11.Items.Clear();
            cb_AMLtr11.Text = "";
            selected[11] = cb_AMLtr12.SelectedIndex;
            cb_AMLtr12.Items.Clear();
            cb_AMLtr12.Text = "";
            selected[12] = cb_AMLtr13.SelectedIndex;
            cb_AMLtr13.Items.Clear();
            cb_AMLtr13.Text = "";
            selected[13] = cb_AMLtr14.SelectedIndex;
            cb_AMLtr14.Items.Clear();
            cb_AMLtr14.Text = "";

            int index = 0;

            //update the global and properties settings
            if (globals.i12Ltr == 0)  //1 letter code
                index = 0;
            else
                index = 1;

            //rebuild the codes array
            string[] codes = new string[ibases];
            for (int i = 0; i < ibases; i++)
            {
                string[] parts = bases[i].Split(',');
                codes[i] = parts[index];
            }

            //reload the list boxes & select the previous item...if still there
            //now load the list boxes
            cb_AMLtr1.Items.AddRange(codes);
            cb_AMLtr1.SelectedIndex = selected[0];
            cb_AMLtr2.Items.AddRange(codes);
            cb_AMLtr2.SelectedIndex = selected[1];
            cb_AMLtr3.Items.AddRange(codes);
            cb_AMLtr3.SelectedIndex = selected[2];
            cb_AMLtr4.Items.AddRange(codes);
            cb_AMLtr4.SelectedIndex = selected[3];
            cb_AMLtr5.Items.AddRange(codes);
            cb_AMLtr5.SelectedIndex = selected[4];
            cb_AMLtr6.Items.AddRange(codes);
            cb_AMLtr6.SelectedIndex = selected[5];
            cb_AMLtr7.Items.AddRange(codes);
            cb_AMLtr7.SelectedIndex = selected[6];
            cb_AMLtr8.Items.AddRange(codes);
            cb_AMLtr8.SelectedIndex = selected[7];
            cb_AMLtr9.Items.AddRange(codes);
            cb_AMLtr9.SelectedIndex = selected[8];
            cb_AMLtr10.Items.AddRange(codes);
            cb_AMLtr10.SelectedIndex = selected[9];
            cb_AMLtr11.Items.AddRange(codes);
            cb_AMLtr11.SelectedIndex = selected[10];
            cb_AMLtr12.Items.AddRange(codes);
            cb_AMLtr12.SelectedIndex = selected[11];
            cb_AMLtr13.Items.AddRange(codes);
            cb_AMLtr13.SelectedIndex = selected[12];
            cb_AMLtr14.Items.AddRange(codes);
            cb_AMLtr14.SelectedIndex = selected[13];

        }

        private void rb_1Leter_CheckedChanged(object sender, EventArgs e)
        {
            if (!init)
                return;

            if (rb_1Leter.Checked)
            {
                int[] selected = new int[14];
                //clear the list boxes
                selected[0] = cb_AMLtr1.SelectedIndex;
                cb_AMLtr1.Items.Clear();
                cb_AMLtr1.Text = "";
                selected[1] = cb_AMLtr2.SelectedIndex;
                cb_AMLtr2.Items.Clear();
                cb_AMLtr2.Text = "";
                selected[2] = cb_AMLtr3.SelectedIndex;
                cb_AMLtr3.Items.Clear();
                cb_AMLtr3.Text = "";
                selected[3] = cb_AMLtr4.SelectedIndex;
                cb_AMLtr4.Items.Clear();
                cb_AMLtr4.Text = "";
                selected[4] = cb_AMLtr5.SelectedIndex;
                cb_AMLtr5.Items.Clear();
                cb_AMLtr5.Text = "";
                selected[5] = cb_AMLtr6.SelectedIndex;
                cb_AMLtr6.Items.Clear();
                cb_AMLtr6.Text = "";
                selected[6] = cb_AMLtr7.SelectedIndex;
                cb_AMLtr7.Items.Clear();
                cb_AMLtr7.Text = "";
                selected[7] = cb_AMLtr8.SelectedIndex;
                cb_AMLtr8.Items.Clear();
                cb_AMLtr8.Text = "";
                selected[8] = cb_AMLtr9.SelectedIndex;
                cb_AMLtr9.Items.Clear();
                cb_AMLtr9.Text = "";
                selected[9] = cb_AMLtr10.SelectedIndex;
                cb_AMLtr10.Items.Clear();
                cb_AMLtr10.Text = "";
                selected[10] = cb_AMLtr11.SelectedIndex;
                cb_AMLtr11.Items.Clear();
                cb_AMLtr11.Text = "";
                selected[11] = cb_AMLtr12.SelectedIndex;
                cb_AMLtr12.Items.Clear();
                cb_AMLtr12.Text = "";
                selected[12] = cb_AMLtr13.SelectedIndex;
                cb_AMLtr13.Items.Clear();
                cb_AMLtr13.Text = "";
                selected[13] = cb_AMLtr14.SelectedIndex;
                cb_AMLtr14.Items.Clear();
                cb_AMLtr14.Text = "";

                int index = 0;

                //update the global and properties settings
                globals.i12Ltr = 0;  //1 letter code
                index = 0;

                Properties.Settings.Default.Ltr_12 = 0;
                Properties.Settings.Default.Save();

                //rebuild the codes array
                string[] codes = new string[ibases];
                for (int i = 0; i < ibases; i++)
                {
                    string[] parts = bases[i].Split(',');
                    codes[i] = parts[index];
                }

                //reload the list boxes
                //now load the list boxes
                cb_AMLtr1.Items.AddRange(codes);
                cb_AMLtr1.SelectedIndex = selected[0];
                cb_AMLtr2.Items.AddRange(codes);
                cb_AMLtr2.SelectedIndex = selected[1];
                cb_AMLtr3.Items.AddRange(codes);
                cb_AMLtr3.SelectedIndex = selected[2];
                cb_AMLtr4.Items.AddRange(codes);
                cb_AMLtr4.SelectedIndex = selected[3];
                cb_AMLtr5.Items.AddRange(codes);
                cb_AMLtr5.SelectedIndex = selected[4];
                cb_AMLtr6.Items.AddRange(codes);
                cb_AMLtr6.SelectedIndex = selected[5];
                cb_AMLtr7.Items.AddRange(codes);
                cb_AMLtr7.SelectedIndex = selected[6];
                cb_AMLtr8.Items.AddRange(codes);
                cb_AMLtr8.SelectedIndex = selected[7];
                cb_AMLtr9.Items.AddRange(codes);
                cb_AMLtr9.SelectedIndex = selected[8];
                cb_AMLtr10.Items.AddRange(codes);
                cb_AMLtr10.SelectedIndex = selected[9];
                cb_AMLtr11.Items.AddRange(codes);
                cb_AMLtr11.SelectedIndex = selected[10];
                cb_AMLtr12.Items.AddRange(codes);
                cb_AMLtr12.SelectedIndex = selected[11];
                cb_AMLtr13.Items.AddRange(codes);
                cb_AMLtr13.SelectedIndex = selected[12];
                cb_AMLtr14.Items.AddRange(codes);
                cb_AMLtr14.SelectedIndex = selected[13];

            }

        }

        private void rb_2Letter_CheckedChanged(object sender, EventArgs e)
        {
            if (!init)
                return;

            if (rb_2Letter.Checked)
            {
                int[] selected = new int[14];
                //clear the list boxes
                selected[0] = cb_AMLtr1.SelectedIndex;
                cb_AMLtr1.Items.Clear();
                cb_AMLtr1.Text = "";
                selected[1] = cb_AMLtr2.SelectedIndex;
                cb_AMLtr2.Items.Clear();
                cb_AMLtr2.Text = "";
                selected[2] = cb_AMLtr3.SelectedIndex;
                cb_AMLtr3.Items.Clear();
                cb_AMLtr3.Text = "";
                selected[3] = cb_AMLtr4.SelectedIndex;
                cb_AMLtr4.Items.Clear();
                cb_AMLtr4.Text = "";
                selected[4] = cb_AMLtr5.SelectedIndex;
                cb_AMLtr5.Items.Clear();
                cb_AMLtr5.Text = "";
                selected[5] = cb_AMLtr6.SelectedIndex;
                cb_AMLtr6.Items.Clear();
                cb_AMLtr6.Text = "";
                selected[6] = cb_AMLtr7.SelectedIndex;
                cb_AMLtr7.Items.Clear();
                cb_AMLtr7.Text = "";
                selected[7] = cb_AMLtr8.SelectedIndex;
                cb_AMLtr8.Items.Clear();
                cb_AMLtr8.Text = "";
                selected[8] = cb_AMLtr9.SelectedIndex;
                cb_AMLtr9.Items.Clear();
                cb_AMLtr9.Text = "";
                selected[9] = cb_AMLtr10.SelectedIndex;
                cb_AMLtr10.Items.Clear();
                cb_AMLtr10.Text = "";
                selected[10] = cb_AMLtr11.SelectedIndex;
                cb_AMLtr11.Items.Clear();
                cb_AMLtr11.Text = "";
                selected[11] = cb_AMLtr12.SelectedIndex;
                cb_AMLtr12.Items.Clear();
                cb_AMLtr12.Text = "";
                selected[12] = cb_AMLtr13.SelectedIndex;
                cb_AMLtr13.Items.Clear();
                cb_AMLtr13.Text = "";
                selected[13] = cb_AMLtr14.SelectedIndex;
                cb_AMLtr14.Items.Clear();
                cb_AMLtr14.Text = "";

                int index = 0;

                //update the global and properties settings
                globals.i12Ltr = 1;  //2 letter code
                index = 1;

                Properties.Settings.Default.Ltr_12 = 1;
                Properties.Settings.Default.Save();

                //rebuild the codes array
                string[] codes = new string[ibases];
                for (int i = 0; i < ibases; i++)
                {
                    string[] parts = bases[i].Split(',');
                    codes[i] = parts[index];
                }

                //reload the list boxes
                //now load the list boxes
                //reload the list boxes
                //now load the list boxes
                cb_AMLtr1.Items.AddRange(codes);
                cb_AMLtr1.SelectedIndex = selected[0];
                cb_AMLtr2.Items.AddRange(codes);
                cb_AMLtr2.SelectedIndex = selected[1];
                cb_AMLtr3.Items.AddRange(codes);
                cb_AMLtr3.SelectedIndex = selected[2];
                cb_AMLtr4.Items.AddRange(codes);
                cb_AMLtr4.SelectedIndex = selected[3];
                cb_AMLtr5.Items.AddRange(codes);
                cb_AMLtr5.SelectedIndex = selected[4];
                cb_AMLtr6.Items.AddRange(codes);
                cb_AMLtr6.SelectedIndex = selected[5];
                cb_AMLtr7.Items.AddRange(codes);
                cb_AMLtr7.SelectedIndex = selected[6];
                cb_AMLtr8.Items.AddRange(codes);
                cb_AMLtr8.SelectedIndex = selected[7];
                cb_AMLtr9.Items.AddRange(codes);
                cb_AMLtr9.SelectedIndex = selected[8];
                cb_AMLtr10.Items.AddRange(codes);
                cb_AMLtr10.SelectedIndex = selected[9];
                cb_AMLtr11.Items.AddRange(codes);
                cb_AMLtr11.SelectedIndex = selected[10];
                cb_AMLtr12.Items.AddRange(codes);
                cb_AMLtr12.SelectedIndex = selected[11];
                cb_AMLtr13.Items.AddRange(codes);
                cb_AMLtr13.SelectedIndex = selected[12];
                cb_AMLtr14.Items.AddRange(codes);
                cb_AMLtr14.SelectedIndex = selected[13];
            }

        }

        private void nu_AmiditeCF_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
