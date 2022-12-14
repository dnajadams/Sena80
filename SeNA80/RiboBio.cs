using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Timers;
using System.Windows.Forms;

namespace SeNA80
{
    public partial class SeNARun : Form
    {
        public DateTime StartAt, EndAt;
        public static int[] sameBase = new int[8];
        public static int MaxCycles = 0;
        public static bool noSequential = false;
        public static bool MixPSPO = false;
        public static bool bOnlyPS = false;
        public static bool bOnlyPO = false;
        public static DateTime dtStart, dtEnd;
        public static bool bIsPaused = false;
        public bool bBeginandEnd = false, bLooping = false;
        public static bool bRunProcessing;
        public bool bUniversalSupport = Properties.Settings.Default.UniversalSupport;
        public int iTimerClicks = 0;
        public static bool bMonitoringUV = false;
        public bool bMixPSPOSequences;
        public static string curBase2Ltr = string.Empty;
        public static bool bGotChar = false;
        private static string sCurPart = string.Empty;
        public static System.Windows.Forms.StatusStrip Run_StatusStrip = null;
        public static System.Windows.Forms.ToolStripLabel Run_StatusLab = null;
        public static bool bAutoShowChart = true;
        //set this timer up off thread so that it doesn't do thread blocking....
        public System.Timers.Timer clearstat = null;  //timer to just clear the status bar every 500 ms or so
        public static int iScrollIndex = 0;
        public static bool bTerminateEndofStep = false;
        public static bool bTerminateEndofCycle = false;
        public static string[] runSequences = new string[9];
        public static string[] protolbls = new string[4];
        public static string[] bases = new string[100];
        public static bool inParallel = true;
        public static int ibases = 0;
        

        public SeNARun()
        {
            InitializeComponent();

        }
        private void SeNARun_Load(object sender, EventArgs e)
        {
            Run_StatusStrip = ss_Run;
            Run_StatusLab = ts_status;

            //initialize run sequences
            for (int i = 0; i < 9; i++)
                runSequences[i] = string.Empty;

            //show - hide scale factors
            if (globals.AutoScaleAmidites)
                gb_Scale.Visible = true;
            else
                gb_Scale.Visible = false;

            //set default to 1
            cb_Col1Scale.SelectedIndex = cb_Col1Scale.FindStringExact("1.0");
            cb_Col2Scale.SelectedIndex = cb_Col2Scale.FindStringExact("1.0");
            cb_Col3Scale.SelectedIndex = cb_Col3Scale.FindStringExact("1.0");
            cb_Col4Scale.SelectedIndex = cb_Col4Scale.FindStringExact("1.0");
            cb_Col5Scale.SelectedIndex = cb_Col5Scale.FindStringExact("1.0");
            cb_Col6Scale.SelectedIndex = cb_Col6Scale.FindStringExact("1.0");
            cb_Col7Scale.SelectedIndex = cb_Col7Scale.FindStringExact("1.0");
            cb_Col8Scale.SelectedIndex = cb_Col8Scale.FindStringExact("1.0");

            //set up status
            clearstat = new System.Timers.Timer(2000); //clear status every two seconds
            try
            {
                SafeSetStatus("Initializing Run");
                clearstat.Elapsed += ClearStatEvent;
                clearstat.AutoReset = true;
                clearstat.Start();
            }
            catch(Exception x) { MessageBox.Show("Can't Set Status Timer - " + x.ToString()); }

            //make sure the run timer is enabled
            //if (RunTimer.Enabled == false)
            //  RunTimer.Enabled = true;

            //check the user level and enable/disable boxes - User level 2 can only run default protocols
            if (globals.Curr_Rights.Contains("2"))
            {
                this.gb_RunProtos.Enabled = false;
                Menu_1Letter.Enabled = false;
                Menu_2Letter.Enabled = false;
            }

            //check the menu for the letter view
            if(globals.i12Ltr == 0)
            { Menu_1Letter.Checked = true; }
            else
            { Menu_2Letter.Checked = true; }

            //check or uncheck tool tips
            if(globals.bShowTips)
            {
                Menu_Tips.Checked = true;
                Menu_Tips.Text = "&Disable Tips..";
            }
            else
            {
                Menu_Tips.Checked = false;
                Menu_Tips.Text = "&Enable Tips..";
            }
            //make sure the status text is blank...
            this.lbl_Status.Text = "Status: ";
            globals.bIsRunning = false;
            this.gb_RunOpts.Enabled = true;
            this.btn_Run.Enabled = false;
            this.btn_Pause.Enabled = false;
            this.btn_Terminate.Enabled = false;

            //set the menu for revier
            Menu_UV_Bar.Text = "&Review UV Bar";

            //set recycle valve to autoon so it closes while pump drawing up and opens when pump purging
            //Man_Controlcs.SendControllerMsg(1, valves.recycleautoon);

            //if Pressures are being monitored show Presssure GB
            if (!globals.bMonPressure)
                gb_PresMon.Visible = false;

            if (globals.bUVTrityl)
            {
                Menu_UV_Bar.Enabled = true;
                Menu_UV_Strip.Enabled = false;  //too much data, do not allow this right now
            }
            else
            {
                Menu_UV_Bar.Enabled = false;
                Menu_UV_Strip.Enabled = false;
            }
            if(globals.bCondTrityl)
            {
                Menu_Cond_Bar.Enabled = true;
                Menu_Cond_Strip.Enabled = true;
            }
            else
            {
                Menu_Cond_Bar.Enabled = false;
                Menu_Cond_Strip.Enabled = false;
            }

            //turn lights off
            if (globals.bUVTrityl)
            {
                Sensor_Config.LightsOn(0);
            }

            //set the amidite letters on the radio buttons
            if(Properties.Settings.Default.Am_1_lbl.Length > 0)
                rb_Am1.Text = Properties.Settings.Default.Am_1_lbl.ToString();
            if (Properties.Settings.Default.Am_2_lbl.Length > 0)
                rb_Am2.Text = Properties.Settings.Default.Am_2_lbl.ToString();
            if (Properties.Settings.Default.Am_3_lbl.Length > 0)
                rb_Am3.Text = Properties.Settings.Default.Am_3_lbl.ToString();
            if (Properties.Settings.Default.Am_4_lbl.Length > 0)
                rb_Am4.Text = Properties.Settings.Default.Am_4_lbl.ToString();
            if (Properties.Settings.Default.Am_5_lbl.Length > 0)
                rb_Am5.Text = Properties.Settings.Default.Am_5_lbl.ToString();
            if (Properties.Settings.Default.Am_6_lbl.Length > 0)
                rb_Am6.Text = Properties.Settings.Default.Am_6_lbl.ToString();
            if (Properties.Settings.Default.Am_7_lbl.Length > 0)
                rb_Am7.Text = Properties.Settings.Default.Am_7_lbl.ToString();
            if (Properties.Settings.Default.Am_8_lbl.Length > 0)
                rb_Am8.Text = Properties.Settings.Default.Am_8_lbl.ToString();
            if (Properties.Settings.Default.Am_9_lbl.Length > 0)
                rb_Am9.Text = Properties.Settings.Default.Am_9_lbl.ToString();
            if (Properties.Settings.Default.Am_10_lbl.Length > 0)
                rb_Am10.Text = Properties.Settings.Default.Am_10_lbl.ToString();
            if (Properties.Settings.Default.Am_11_lbl.Length > 0)
                rb_Am11.Text = Properties.Settings.Default.Am_11_lbl.ToString();
            if (Properties.Settings.Default.Am_12_lbl.Length > 0)
                rb_Am12.Text = Properties.Settings.Default.Am_12_lbl.ToString();
            if (Properties.Settings.Default.Am_13_lbl.Length > 0)
                rb_Am13.Text = Properties.Settings.Default.Am_13_lbl.ToString();
            if (Properties.Settings.Default.Am_14_lbl.Length > 0)
                rb_Am14.Text = Properties.Settings.Default.Am_14_lbl.ToString();

            if (Properties.Settings.Default.View_VlvStat == 0)
            {
                Menu_ViewStatusBox.Checked = true;
                Menu_ValveView.Checked = false;
                Pnl_Valves.Visible = false;
                Status_R.Visible = true;
                
            }
            else
            {
                Menu_ValveView.Checked = true;
                Menu_ViewStatusBox.Checked = false;
                Pnl_Valves.Visible = true;
                Status_R.Visible = false;
            }
            if (globals.defPrepProtocol != string.Empty && globals.defRunProtocol != string.Empty && globals.defPostProtocol != string.Empty)
            {
                //set the default protocols in the text box
                this.lbl_Start.Text = globals.defStartupProtocol;
                Man_Controlcs.WriteStatus("Run", "Startup Protocols Loaded-" + globals.defStartupProtocol);

                this.lbl_PreRun.Text = globals.defPrepProtocol;
                Man_Controlcs.WriteStatus("Run", "Prep Protocol Loaded-" + globals.defPrepProtocol);

                this.lbl_Run.Text = globals.defRunProtocol;
                Man_Controlcs.WriteStatus("Run", "Run Method Loaded-" + globals.defRunProtocol);

                this.lbl_Post.Text = globals.defPostProtocol;
                Man_Controlcs.WriteStatus("Run", "Post Method Loaded-" + globals.defPostProtocol);

                globals.bProtocolsLoaded = true;

                protolbls[0] = globals.defStartupProtocol;
                protolbls[1] = globals.defPrepProtocol;
                protolbls[2] = globals.defRunProtocol;
                protolbls[3] = globals.defPostProtocol;

                //can't start the run until the protocols and sequences selected
                if (globals.bProtocolsLoaded && globals.bSeqeuncesLoaded)
                    this.btn_Run.Enabled = true;

                //you don't have to have a startup protocol...
                if (lbl_Start.Text.Length > 0 && !(lbl_Start.Text.Contains("none")))
                {
                    FillTheList(this.lbl_Start.Text);
                    this.lbl_CurProtocol.Text = this.lbl_Start.Text;
                }
                else if (this.lbl_PreRun.Text.Length > 0)
                {
                    FillTheList(this.lbl_PreRun.Text);
                }
             }
            else // pressed Cancel so clear everything and make the user select again
            {
                this.lbl_PreRun.Text = "";
                this.lbl_Run.Text = "";
                this.lbl_Post.Text = "";
                this.lbl_Start.Text = "";

                protolbls[0] = string.Empty;
                protolbls[1] = string.Empty;
                protolbls[2] = string.Empty;
                protolbls[3] = string.Empty;

                Man_Controlcs.WriteStatus("Run", "No Protocols Loaded");

                globals.bProtocolsLoaded = false;
                this.btn_Run.Enabled = false;
            }

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

            SafeSetStatus("Initialization Complete");
        }
        //safely set the status label text using a delegate
        
       public static void ClearStatEvent(Object source, ElapsedEventArgs e)
        {
            SafeSetStatus("");
        }
        public static void SafeSetStatus(string text)
        {
            if ( Run_StatusStrip == null)
                return;

            if (Run_StatusStrip.InvokeRequired)
                Run_StatusStrip.Invoke(new MethodInvoker(() => Run_StatusLab.Text = text));
            else
                Run_StatusLab.Text = text;
        }
        /*********************************************************
         * Load the protocols and the sequences
         * into the Run Program
         * 
         * Then start and run all of the commands
         * one at a time
         */

        private void btn_LoadProtocols_Click(object sender, EventArgs e)
        {
            SafeSetStatus("Loading Protocols...");
            using (Load_Protocols dlg = new Load_Protocols())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // get the three list box items and set them here
                    this.lbl_PreRun.Text = dlg.cb_PreRun.SelectedItem.ToString();
                    Man_Controlcs.WriteStatus("Run", "Method Loaded-" + dlg.cb_PreRun.SelectedItem.ToString());
                    this.lbl_Run.Text = dlg.cb_Run.SelectedItem.ToString();
                    Man_Controlcs.WriteStatus("Run", "Method Loaded-" + dlg.cb_Run.SelectedItem.ToString());
                    this.lbl_Start.Text = dlg.cb_Start.SelectedItem.ToString();
                    Man_Controlcs.WriteStatus("Run", "Method Loaded-" + dlg.cb_Start.SelectedItem.ToString());
                    this.lbl_Post.Text = dlg.cb_PostRun.SelectedItem.ToString();
                    Man_Controlcs.WriteStatus("Run", "Method Loaded-" + dlg.cb_PostRun.SelectedItem.ToString());

                    //can't start the run until the protocols and sequences selected
                    if (globals.bProtocolsLoaded && globals.bSeqeuncesLoaded)
                        this.btn_Run.Enabled = true;

                    //start protocol is optional
                    if (!(this.lbl_Start.Text.Contains("none")))
                    {
                        FillTheList(this.lbl_Start.Text);
                        this.lbl_CurProtocol.Text = this.lbl_Start.Text;
                    }
                    else  //you must have a prep prtocol
                    {
                        if (lbl_PreRun.Text.Length > 2)
                        {
                            FillTheList(this.lbl_PreRun.Text);
                            this.lbl_CurProtocol.Text = this.lbl_PreRun.Text;
                        }
                        else
                        {
                            MessageBox.Show("You must specify a PreRun, Run and Post Run Protocol", "Illegal Configuration", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            return;
                        }
                    }

                    //if you are an administrator, you can save the selected protocols as defaults.
                    if (globals.Curr_Rights.Contains("Admin"))
                    {
                        if (MessageBox.Show("Would You Like to Save these Protocols as Default?", "Update Default", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            globals.defPrepProtocol = lbl_PreRun.Text;
                            globals.defRunProtocol = lbl_Run.Text;
                            globals.defStartupProtocol = lbl_Start.Text;
                            globals.defPostProtocol = lbl_Post.Text;

                            Properties.Settings.Default.def_Prep_Protocol = globals.defPrepProtocol;
                            Properties.Settings.Default.def_Run_Protocol = globals.defRunProtocol;
                            Properties.Settings.Default.def_Startup_Protocol = globals.defStartupProtocol;
                            Properties.Settings.Default.def_Post_Protocol = globals.defPostProtocol;

                            Properties.Settings.Default.Save();
                            
                        }
                    }
                    protolbls[0] = this.lbl_Start.Text;
                    protolbls[1] = this.lbl_PreRun.Text;
                    protolbls[2] = this.lbl_Run.Text;
                    protolbls[3] = this.lbl_Post.Text;

                    SafeSetStatus("Protocol Loading Complete");
                    return;
                    // whatever you need to do with result
                }
                else // pressed Cancel so clear everything and make the user select again
                {
                    this.lbl_PreRun.Text = "";
                    this.lbl_Run.Text = "";
                    this.lbl_Start.Text = "";
                    this.lbl_Post.Text = "";

                    protolbls[0] = string.Empty;
                    protolbls[1] = string.Empty;
                    protolbls[2] = string.Empty;
                    protolbls[3] = string.Empty;
                    Man_Controlcs.WriteStatus("Run", "Protocols Cleared");

                    globals.bProtocolsLoaded = false;
                    this.btn_Run.Enabled = false;
                }
            }
        }
        public bool bByFile1 = false, bByFile2 = false, bByFile3 = false, bByFile4 = false;
        public bool bByFile5 = false, bByFile6 = false, bByFile7 = false, bByFile8 = false;

        private void btn_col1_Click(object sender, EventArgs e)
        {
            if (tb_col1.Text.Length > 0)
            {
                SafeSetStatus("Clearing Sequence");
                tb_col1.Clear();
                tb_col1.Text = "";
                lbl_Length1.Text = "0";
                bMonitoringUV = false;

                //reset the font
                tb_col1.SelectionFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
                tb_col1.SelectionColor = Color.Black;

                cb_Col1Scale.SelectedIndex = cb_Col1Scale.FindStringExact("1.0");
            }
            OpenSequence(1, 0);
        }

        private void btn_col2_Click(object sender, EventArgs e)
        {
            if (tb_col2.Text.Length > 0)
            {
                SafeSetStatus("Clearing Sequence");
                tb_col2.Clear();
                tb_col2.Text = "";
                lbl_Length2.Text = "0";
                bMonitoringUV = false;

                //reset the font
                tb_col2.SelectionFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
                tb_col2.SelectionColor = Color.Black;

                cb_Col2Scale.SelectedIndex = cb_Col2Scale.FindStringExact("1.0");
            }
            OpenSequence(2, 0);
        }

        private void btn_col3_Click(object sender, EventArgs e)
        {
            if (tb_col3.Text.Length > 0)
            {

                SafeSetStatus("Clearing Sequence");
                tb_col3.Clear();
                tb_col3.Text = "";
                lbl_Length3.Text = "0";
                bMonitoringUV = false;

                //reset the font
                tb_col3.SelectionFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
                tb_col3.SelectionColor = Color.Black;

                cb_Col3Scale.SelectedIndex = cb_Col3Scale.FindStringExact("1.0");
            }
            OpenSequence(3, 0);
        }

        private void btn_col4_Click(object sender, EventArgs e)
        {
            if (tb_col4.Text.Length > 0)
            {

                SafeSetStatus("Clearing Sequence");
                tb_col4.Clear();

                tb_col4.Text = "";
                lbl_Length4.Text = "0";
                bMonitoringUV = false;

                //reset the font
                tb_col4.SelectionFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
                tb_col4.SelectionColor = Color.Black;

                cb_Col4Scale.SelectedIndex = cb_Col4Scale.FindStringExact("1.0");
            }
            OpenSequence(4, 0);
        }

        private void btn_col5_Click(object sender, EventArgs e)
        {
            if (tb_col5.Text.Length > 0)
            {

                SafeSetStatus("Clearing Sequence");
                tb_col5.Clear();
                tb_col5.Text = "";
                lbl_Length5.Text = "0";
                bMonitoringUV = false;

                //reset the font
                tb_col5.SelectionFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
                tb_col5.SelectionColor = Color.Black;

                cb_Col5Scale.SelectedIndex = cb_Col5Scale.FindStringExact("1.0");
            }
            OpenSequence(5, 0);
        }

        private void btn_col6_Click(object sender, EventArgs e)
        {
            if (tb_col6.Text.Length > 0)
            {
                SafeSetStatus("Clearing Sequence");
                tb_col6.Clear();
                tb_col6.Text = "";
                lbl_Length6.Text = "0";
                bMonitoringUV = false;

                //reset the font
                tb_col6.SelectionFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
                tb_col6.SelectionColor = Color.Black;

                cb_Col6Scale.SelectedIndex = cb_Col6Scale.FindStringExact("1.0");
            }
            OpenSequence(6, 0);
        }

        private void btn_col7_Click(object sender, EventArgs e)
        {
            if (tb_col7.Text.Length > 0)
            {
                SafeSetStatus("Clearing Sequence");
                tb_col7.Clear();
                tb_col7.Text = "";
                lbl_Length7.Text = "0";
                bMonitoringUV = false;

                //reset the font
                tb_col7.SelectionFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
                tb_col7.SelectionColor = Color.Black;

                cb_Col7Scale.SelectedIndex = cb_Col7Scale.FindStringExact("1.0");
            }
            OpenSequence(7, 0);
        }

        private void btn_col8_Click(object sender, EventArgs e)
        {
            if (tb_col8.Text.Length > 0)
            {
                SafeSetStatus("Clearing Sequence");
                tb_col8.Clear();
                tb_col8.Text = "";
                lbl_Length8.Text = "0";
                bMonitoringUV = false;

                //reset the font
                tb_col8.SelectionFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
                tb_col8.SelectionColor = Color.Black;

                cb_Col8Scale.SelectedIndex = cb_Col8Scale.FindStringExact("1.0");
            }
            OpenSequence(8, 0);
        }

        private void btn_ResetAll_Click(object sender, EventArgs e)
        {
            //close the trityl window
            if(bMonitoringUV)
                if (WindowAlreadyOpen.WindowOpen("Trityl Bar Chart Histogram"))
                    globals.sb.Close();

            //clear everything out
            ClearAllSequences();
            noSequential = false;
            bMonitoringUV = false;

        }
        private void btn_LoadBatch_Click(object sender, EventArgs e)
        {
            ClearAllSequences();
            
            OpenSequence(9, 1);
        }
        private void ClearAllSequences()
        {
            SafeSetStatus("Clearing Sequence");
            tb_col1.Clear();
            tb_col2.Clear();
            tb_col3.Clear();
            tb_col4.Clear();
            tb_col5.Clear();
            tb_col6.Clear();
            tb_col7.Clear();
            tb_col8.Clear();

            tb_col1.Text = "";
            tb_col2.Text = "";
            tb_col3.Text = "";
            tb_col4.Text = "";
            tb_col5.Text = "";
            tb_col6.Text = "";
            tb_col7.Text = "";
            tb_col8.Text = "";

            lbl_Length1.Text = "0";
            lbl_Length2.Text = "0";
            lbl_Length3.Text = "0";
            lbl_Length4.Text = "0";
            lbl_Length5.Text = "0";
            lbl_Length6.Text = "0";
            lbl_Length7.Text = "0";
            lbl_Length8.Text = "0";

            //reset the font
            tb_col1.SelectionFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
            tb_col2.SelectionFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
            tb_col3.SelectionFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
            tb_col4.SelectionFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
            tb_col5.SelectionFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
            tb_col6.SelectionFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
            tb_col7.SelectionFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
            tb_col8.SelectionFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
            tb_col1.SelectionColor = Color.Black;
            tb_col2.SelectionColor = Color.Black;
            tb_col3.SelectionColor = Color.Black;
            tb_col4.SelectionColor = Color.Black;
            tb_col5.SelectionColor = Color.Black;
            tb_col6.SelectionColor = Color.Black;
            tb_col7.SelectionColor = Color.Black;
            tb_col8.SelectionColor = Color.Black;

            //set default to 1
            cb_Col1Scale.SelectedIndex = cb_Col1Scale.FindStringExact("1.0");
            cb_Col2Scale.SelectedIndex = cb_Col2Scale.FindStringExact("1.0");
            cb_Col3Scale.SelectedIndex = cb_Col3Scale.FindStringExact("1.0");
            cb_Col4Scale.SelectedIndex = cb_Col4Scale.FindStringExact("1.0");
            cb_Col5Scale.SelectedIndex = cb_Col5Scale.FindStringExact("1.0");
            cb_Col6Scale.SelectedIndex = cb_Col6Scale.FindStringExact("1.0");
            cb_Col7Scale.SelectedIndex = cb_Col7Scale.FindStringExact("1.0");
            cb_Col8Scale.SelectedIndex = cb_Col8Scale.FindStringExact("1.0");

            bMonitoringUV = false;
            globals.bSeqeuncesLoaded = false;
        }
        private int RetCycles(string instring, int code)
        {
            if (code == 0)
                return instring.Length;
            else
                return instring.Length / 3; 
       }
        private string ConvertSeq(string instring, int to)
        {
            string outstring = string.Empty;

            int sptr = 0;

            if (to == 0)  //convert 2 letter code to 1 letter
            {
                while (sptr < instring.Length)
                {
                    string code = instring.Substring(sptr, 2);
                    string outcode = RetCode(0, code);
                    Debug.WriteLine("outcode" + outcode);
                    outstring = outstring + outcode;
                    sptr = sptr + 3;
                }
            }
            else
            {
                while (sptr < instring.Length)
                {
                    string code = instring.Substring(sptr, 1);
                    string outcode = RetCode(1, code);
                    Debug.WriteLine("outcode" + outcode);
                    outstring = outstring + outcode + "-";
                    sptr = sptr + 1;
                }
            }
        
            return outstring;
        }
        private string RetLetter(string incode)
        {
            //get in 2 letter code
            //put out 1 letter code
            for (int i = 0; i < bases.Length; i++)
            {
                string[] parts = bases[i].Split(',');
                if (parts[1].Contains(incode))
                    return parts[0];
            }
            return string.Empty;
        }
        private string RetCode(int index, string incode)
        {
            int outdex = 0;

            if (index == 0)
            {
                outdex = 1;
                incode = incode.Substring(0, 2);
                Debug.WriteLine("Incode = " + incode);
            }
            else
            {
                outdex = 0;
                Debug.WriteLine("Incode = " + incode);
            }

            for (int i = 0; i < ibases; i++)
            {
                string[] parts = bases[i].Split(',');
                if (parts[outdex].Equals(incode))
                {
                    Debug.WriteLine("parts index = " + parts[index] + " parts outdex =" + parts[outdex]);
                    return parts[index];
                }
            }
            return string.Empty;
        }

        private void OpenSequence(int SeqNo, int SeqFilter)
        {
            SafeSetStatus("Loading Sequence");
            string seq = "";
            int ltrcode = 0;
            int cycles = 0;

            //first set all UV Cells Off
            OpenFileDialog openSeq = new OpenFileDialog();

            openSeq.InitialDirectory = globals.sequence_path;
            if (SeqFilter == 0)
                openSeq.Filter = "Sequence files (*.seq)|*.seq|All files (*.*)|*.*";
            else
                openSeq.Filter = "Batch files (*.csv)|*.csv|All files (*.*)|*.*";


            openSeq.Title = "Select a Sequence File";
            openSeq.RestoreDirectory = true;
            Debug.WriteLine("Here in Open Sequence File");
            if (openSeq.ShowDialog() == DialogResult.OK)
            {
                seq = openSeq.FileName;

                if (seq.Length < 1)
                {
                    MessageBox.Show("Must Select a FileName", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            else
                return;
           
            string[] lines = File.ReadAllLines(seq);
            //1 letter or two letter file?? for csv files the type is stored in line 0, for seq files it is stored in line 1
            if (SeqNo == 9)
                ltrcode = int.Parse(lines[0]);
            else
                ltrcode = int.Parse(lines[1]);

            //if ltrcode does not match globals.ltr12 then conver the sequence
            if(ltrcode != globals.i12Ltr)
            {
                if (SeqNo == 9)
                {
                    for(int l = 1; l < lines.Length - 2; l++)
                    {
                        string[] parts = lines[l].Split(',');
                        string sOut = ConvertSeq(parts[1], globals.i12Ltr);
                        lines[l] = parts[0] + "," + sOut;
                    }
                }
                else
                    lines[0] = ConvertSeq(lines[0], globals.i12Ltr);

                ltrcode = globals.i12Ltr;
            }

            

            //make sure the file has a string in it
            if (lines[0].Length > 1 && SeqFilter == 0)
            {
                
                //now update the text box on the screen
                switch (SeqNo)
                {
                    case 1:
                        globals.bCol1 = true;
                        bByFile1 = true;
                        tb_col1.Text = lines[0];
                        cycles = RetCycles(lines[0], ltrcode);
                        lbl_Length1.Text = cycles.ToString("0");
                        if (cycles > MaxCycles)
                            MaxCycles = cycles;

                        if (globals.i12Ltr > 0)
                        {
                            if (CheckForPlus(tb_col1.Text) && !noSequential)
                            {
                                MixPSPO = true;
                                if (rb_Parallel.Checked && globals.bSeqeuncesLoaded)
                                {
                                    if (MessageBox.Show("You have Mixed Phosphotioate and Phosphate backbone Oligos in your Sequences\n\n" +
                                        "Would you like to continue in Parallel?\n\nPress No to Switch to Sequential", 
                                        "In Parallel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                    {
                                        rb_Sequenntial.Checked = true;
                                        rb_Parallel.Checked = false;
                                    }
                                }
                            }
                        }
                        if (globals.bUVTrityl)
                            Sensor_Config.LightsOn(1);
                        Man_Controlcs.WriteStatus("Run", "Col 1 Loaded -" + lines[0]);
                        break;

                    case 2:
                        globals.bCol2 = true;
                        bByFile2 = true;
                        tb_col2.Text = lines[0];
                        cycles = RetCycles(lines[0], ltrcode);
                        lbl_Length2.Text = cycles.ToString("0");
                        if (cycles > MaxCycles)
                            MaxCycles = cycles;

                        if (globals.i12Ltr > 0)
                        {
                            if (CheckForPlus(tb_col2.Text) && !noSequential)
                            {
                                MixPSPO = true;
                            
                                if (rb_Parallel.Checked && globals.bSeqeuncesLoaded)
                                {
                                    if (MessageBox.Show("You have Mixed Phosphotioate and Phosphate backbone Oligos in your Sequences\n\n" +
                                        "Would you like to continue in Parallel?\n\nPress No to Switch to Sequential",
                                        "In Parallel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                    {
                                        rb_Sequenntial.Checked = true;
                                        rb_Parallel.Checked = false;
                                    }
                                }
                            }
                        }
                        if (globals.bUVTrityl)
                            Sensor_Config.LightsOn(2);
                        Man_Controlcs.WriteStatus("Run", "Col 2 Loaded -" + lines[0]);
                        break;

                    case 3:
                        globals.bCol3 = true;
                        bByFile3 = true;
                        tb_col3.Text = lines[0];
                        cycles = RetCycles(lines[0], ltrcode);
                        lbl_Length3.Text = cycles.ToString("0");
                        if (cycles > MaxCycles)
                            MaxCycles = cycles;

                        if (globals.i12Ltr > 0)
                        {
                            if (CheckForPlus(tb_col3.Text) && !noSequential)
                            {
                                MixPSPO = true;

                                if (rb_Parallel.Checked && globals.bSeqeuncesLoaded)
                                {
                                    if (MessageBox.Show("You have Mixed Phosphotioate and Phosphate backbone Oligos in your Sequences\n\n" +
                                        "Would you like to continue in Parallel?\n\nPress No to Switch to Sequential",
                                        "In Parallel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                    {
                                        rb_Sequenntial.Checked = true;
                                        rb_Parallel.Checked = false;
                                    }
                                }
                            }
                        }

                        if (globals.bUVTrityl)
                            Sensor_Config.LightsOn(3);
                        Man_Controlcs.WriteStatus("Run", "Col 3 Loaded -" + lines[0]);
                        break;

                    case 4:
                        globals.bCol4 = true;
                        bByFile4 = true;
                        tb_col4.Text = lines[0];
                        cycles = RetCycles(lines[0], ltrcode);
                        lbl_Length4.Text = cycles.ToString("0");
                        if (cycles > MaxCycles)
                            MaxCycles = cycles;

                        if (globals.i12Ltr > 0)
                        {
                            if (CheckForPlus(tb_col4.Text) && !noSequential)
                            {
                                MixPSPO = true;
                                if (rb_Parallel.Checked && globals.bSeqeuncesLoaded)
                                {
                                    if (MessageBox.Show("You have Mixed Phosphotioate and Phosphate backbone Oligos in your Sequences\n\n" +
                                        "Would you like to continue in Parallel?\n\nPress No to Switch to Sequential",
                                        "In Parallel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                    {
                                        rb_Sequenntial.Checked = true;
                                        rb_Parallel.Checked = false;
                                    }
                                }
                            }
                        }

                        if (globals.bUVTrityl)
                            Sensor_Config.LightsOn(4);
                        Man_Controlcs.WriteStatus("Run", "Col 4 Loaded -" + lines[0]);
                        break;

                    case 5:
                        globals.bCol5 = true;
                        bByFile5 = true;
                        tb_col5.Text = lines[0];
                        cycles = RetCycles(lines[0], ltrcode);
                        lbl_Length5.Text = cycles.ToString("0");
                        if (cycles > MaxCycles)
                            MaxCycles = cycles;

                        if (globals.i12Ltr > 0)
                        {
                            if (CheckForPlus(tb_col5.Text) && !noSequential)
                            {
                                MixPSPO = true;
                                if (rb_Parallel.Checked && globals.bSeqeuncesLoaded)
                                {
                                    if (MessageBox.Show("You have Mixed Phosphotioate and Phosphate backbone Oligos in your Sequences\n\n" +
                                        "Would you like to continue in Parallel?\n\nPress No to Switch to Sequential",
                                        "In Parallel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                    {
                                        rb_Sequenntial.Checked = true;
                                        rb_Parallel.Checked = false;
                                    }
                                }
                            }
                        }

                        if (globals.bUVTrityl)
                            Sensor_Config.LightsOn(5);
                        Man_Controlcs.WriteStatus("Run", "Col 5 Loaded -" + lines[0]);
                        break;

                    case 6:
                        globals.bCol6 = true;
                        bByFile6 = true;
                        tb_col6.Text = lines[0];
                        cycles = RetCycles(lines[0], ltrcode);
                        lbl_Length6.Text = cycles.ToString("0");
                        if (cycles > MaxCycles)
                            MaxCycles = cycles;

                        if (globals.i12Ltr > 0)
                        {
                            if (CheckForPlus(tb_col6.Text) && !noSequential)
                            {
                                MixPSPO = true;
                                if (rb_Parallel.Checked && globals.bSeqeuncesLoaded)
                                {
                                    if (MessageBox.Show("You have Mixed Phosphotioate and Phosphate backbone Oligos in your Sequences\n\n" +
                                        "Would you like to continue in Parallel?\n\nPress No to Switch to Sequential",
                                        "In Parallel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                    {
                                        rb_Sequenntial.Checked = true;
                                        rb_Parallel.Checked = false;
                                    }
                                }
                            }
                        }

                        if (globals.bUVTrityl)
                            Sensor_Config.LightsOn(6);
                        Man_Controlcs.WriteStatus("Run", "Col 6 Loaded -" + lines[0]);
                        break;

                    case 7:
                        globals.bCol7 = true;
                        bByFile7 = true;
                        tb_col7.Text = lines[0];
                        cycles = RetCycles(lines[0], ltrcode);
                        lbl_Length7.Text = cycles.ToString("0");
                        if (cycles > MaxCycles)
                            MaxCycles = cycles;

                        if (globals.i12Ltr > 0)
                        {
                            if (CheckForPlus(tb_col7.Text) && !noSequential)
                            {
                                MixPSPO = true;
                                if (rb_Parallel.Checked && globals.bSeqeuncesLoaded)
                                {
                                    if (MessageBox.Show("You have Mixed Phosphotioate and Phosphate backbone Oligos in your Sequences\n\n" +
                                        "Would you like to continue in Parallel?\n\nPress No to Switch to Sequential",
                                        "In Parallel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                    {
                                        rb_Sequenntial.Checked = true;
                                        rb_Parallel.Checked = false;
                                    }
                                }
                            }
                        }
                        if (globals.bUVTrityl)
                            Sensor_Config.LightsOn(7);
                        Man_Controlcs.WriteStatus("Run", "Col 7 Loaded -" + lines[0]);
                        break;

                    case 8:
                        globals.bCol8 = true;
                        bByFile8 = true;
                        tb_col8.Text = lines[0];
                        cycles = RetCycles(lines[0], ltrcode);
                        lbl_Length8.Text = cycles.ToString("0");
                        if (cycles > MaxCycles)
                            MaxCycles = cycles;

                        if (globals.i12Ltr > 0)
                        {
                            if (CheckForPlus(tb_col8.Text) && !noSequential)
                            {
                                MixPSPO = true;
                                if (rb_Parallel.Checked && globals.bSeqeuncesLoaded)
                                {
                                    if (MessageBox.Show("You have Mixed Phosphotioate and Phosphate backbone Oligos in your Sequences\n\n" +
                                        "Would you like to continue in Parallel?\n\nPress No to Switch to Sequential",
                                        "In Parallel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                    {
                                        rb_Sequenntial.Checked = true;
                                        rb_Parallel.Checked = false;
                                    }
                                }
                            }
                        }
                        if (globals.bUVTrityl)
                            Sensor_Config.LightsOn(8);
                        Man_Controlcs.WriteStatus("Run", "Col 8 Loaded -" + lines[0]);
                        break;
                }
                
            }
            else if (SeqFilter == 1)
            {
                int lineNo = 0;
                foreach (string line in lines)
                {
                  
                    string[] parts = line.Split(',');
                    int Joshua = 0;

                    if (parts[0].Length > 0)
                    {

                        if (parts[0].Length < 2 && lineNo > 0)  
                            Joshua = Int32.Parse(parts[0]);
                        

                        lineNo = lineNo + 1;
                
                        switch (Joshua)
                        {
                            case 1:
                                globals.bCol1 = true;
                                bByFile1 = true;
                                tb_col1.Text = parts[1];
                                cycles = RetCycles(parts[1], ltrcode);
                                lbl_Length1.Text = cycles.ToString("0");
                                if (cycles > MaxCycles)
                                    MaxCycles = cycles;

                                if (globals.i12Ltr > 0)
                                {
                                    if (CheckForPlus(tb_col1.Text) && !noSequential)
                                    {
                                        MixPSPO = true;
                                        if (rb_Parallel.Checked && globals.bSeqeuncesLoaded)
                                        {
                                            if (MessageBox.Show("You have Mixed Phosphotioate and Phosphate backbone Oligos in your Sequences\n\n" +
                                                "Would you like to continue in Parallel?\n\nPress No to Switch to Sequential",
                                                "In Parallel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                            {
                                                rb_Sequenntial.Checked = true;
                                                rb_Parallel.Checked = false;
                                            }
                                        }
                                    }
                                }
                                if (globals.bUVTrityl)
                                    Sensor_Config.LightsOn(1);
                                Man_Controlcs.WriteStatus("Run", "Col 1 Loaded -" + parts[1]);
                                break;

                            case 2:
                                globals.bCol2 = true;
                                bByFile2 = true;
                                tb_col2.Text = parts[1];
                                cycles = RetCycles(parts[1], ltrcode);
                                lbl_Length2.Text = cycles.ToString("0");
                                if (cycles > MaxCycles)
                                    MaxCycles = cycles;

                                if (globals.i12Ltr > 0)
                                {
                                    if (CheckForPlus(tb_col2.Text) && !noSequential)
                                    {
                                        MixPSPO = true;
                                        if (rb_Parallel.Checked && globals.bSeqeuncesLoaded)
                                        {
                                            if (MessageBox.Show("You have Mixed Phosphotioate and Phosphate backbone Oligos in your Sequences\n\n" +
                                                "Would you like to continue in Parallel?\n\nPress No to Switch to Sequential",
                                                "In Parallel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                            {
                                                rb_Sequenntial.Checked = true;
                                                rb_Parallel.Checked = false;
                                            }
                                        }
                                    }
                                }

                                if (globals.bUVTrityl)
                                    Sensor_Config.LightsOn(2);
                                Man_Controlcs.WriteStatus("Run", "Col 2 Loaded -" + parts[1]);
                                break;

                            case 3:
                                globals.bCol3 = true;
                                bByFile3 = true;
                                tb_col3.Text = parts[1];
                                cycles = RetCycles(parts[1], ltrcode);
                                lbl_Length3.Text = cycles.ToString("0");
                                if (cycles > MaxCycles)
                                    MaxCycles = cycles;

                                if (globals.i12Ltr > 0)
                                {
                                    if (CheckForPlus(tb_col3.Text) && !noSequential)
                                    {
                                        MixPSPO = true;
                                        if (rb_Parallel.Checked && globals.bSeqeuncesLoaded)
                                        {
                                            if (MessageBox.Show("You have Mixed Phosphotioate and Phosphate backbone Oligos in your Sequences\n\n" +
                                                "Would you like to continue in Parallel?\n\nPress No to Switch to Sequential",
                                                "In Parallel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                            {
                                                rb_Sequenntial.Checked = true;
                                                rb_Parallel.Checked = false;
                                            }
                                        }
                                    }
                                }
                                if (globals.bUVTrityl)
                                    Sensor_Config.LightsOn(3);
                                Man_Controlcs.WriteStatus("Run", "Col 3 Loaded -" + parts[1]);
                                break;

                            case 4:
                                Debug.WriteLine("Here in Case 4b");
                                globals.bCol4 = true;
                                bByFile4 = true;
                                tb_col4.Text = parts[1];
                                cycles = RetCycles(parts[1], ltrcode);
                                lbl_Length4.Text = cycles.ToString("0");
                                if (cycles > MaxCycles)
                                    MaxCycles = cycles;

                                if (globals.i12Ltr > 0)
                                {
                                    if (CheckForPlus(tb_col4.Text) && !noSequential)
                                    {
                                        MixPSPO = true;
                                        if (rb_Parallel.Checked && globals.bSeqeuncesLoaded)
                                        {
                                            if (MessageBox.Show("You have Mixed Phosphotioate and Phosphate backbone Oligos in your Sequences\n\n" +
                                                "Would you like to continue in Parallel?\n\nPress No to Switch to Sequential",
                                                "In Parallel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                            {
                                                rb_Sequenntial.Checked = true;
                                                rb_Parallel.Checked = false;
                                            }
                                        }
                                    }
                                }
                                if (globals.bUVTrityl)
                                    Sensor_Config.LightsOn(4);
                                Man_Controlcs.WriteStatus("Run", "Col 4 Loaded -" + parts[1]);
                                break;

                            case 5:
                                globals.bCol5 = true;
                                bByFile5 = true;
                                tb_col5.Text = parts[1];
                                cycles = RetCycles(parts[1], ltrcode);
                                lbl_Length5.Text = cycles.ToString("0");
                                if (cycles > MaxCycles)
                                    MaxCycles = cycles;

                                if (globals.i12Ltr > 0)
                                {
                                    if (CheckForPlus(tb_col5.Text) && !noSequential)
                                    {
                                        MixPSPO = true;
                                        if (rb_Parallel.Checked && globals.bSeqeuncesLoaded)
                                        {
                                            if (MessageBox.Show("You have Mixed Phosphotioate and Phosphate backbone Oligos in your Sequences\n\n" +
                                                "Would you like to continue in Parallel?\n\nPress No to Switch to Sequential",
                                                "In Parallel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                            {
                                                rb_Sequenntial.Checked = true;
                                                rb_Parallel.Checked = false;
                                            }
                                        }
                                    }
                                }
                                if (globals.bUVTrityl)
                                    Sensor_Config.LightsOn(5);
                                Man_Controlcs.WriteStatus("Run", "Col 5 Loaded -" + parts[1]);
                                break;

                            case 6:
                                globals.bCol6 = true;
                                bByFile6 = true;
                                tb_col6.Text = parts[1];
                                cycles = RetCycles(parts[1], ltrcode);
                                lbl_Length6.Text = cycles.ToString("0");
                                if (cycles > MaxCycles)
                                    MaxCycles = cycles;
                                if (globals.i12Ltr > 0)
                                {
                                    if (CheckForPlus(tb_col6.Text) && !noSequential)
                                    {
                                        MixPSPO = true;
                                        if (rb_Parallel.Checked && globals.bSeqeuncesLoaded)
                                        {
                                            if (MessageBox.Show("You have Mixed Phosphotioate and Phosphate backbone Oligos in your Sequences\n\n" +
                                                "Would you like to continue in Parallel?\n\nPress No to Switch to Sequential",
                                                "In Parallel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                            {
                                                rb_Sequenntial.Checked = true;
                                                rb_Parallel.Checked = false;
                                            }
                                        }
                                    }
                                }
                                if (globals.bUVTrityl)
                                    Sensor_Config.LightsOn(6);
                                Man_Controlcs.WriteStatus("Run", "Col 6 Loaded -" + parts[1]);
                                break;

                            case 7:
                                globals.bCol7 = true;
                                bByFile7 = true;
                                tb_col7.Text = parts[1];
                                cycles = RetCycles(parts[1], ltrcode);
                                lbl_Length7.Text = cycles.ToString("0");
                                if (cycles > MaxCycles)
                                    MaxCycles = cycles;
                                if (globals.i12Ltr > 0)
                                {
                                    if (CheckForPlus(tb_col7.Text) && !noSequential)
                                    {
                                        MixPSPO = true;
                                        if (rb_Parallel.Checked && globals.bSeqeuncesLoaded)
                                        {
                                            if (MessageBox.Show("You have Mixed Phosphotioate and Phosphate backbone Oligos in your Sequences\n\n" +
                                                "Would you like to continue in Parallel?\n\nPress No to Switch to Sequential",
                                                "In Parallel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                            {
                                                rb_Sequenntial.Checked = true;
                                                rb_Parallel.Checked = false;
                                            }
                                        }
                                    }
                                }
                                if (globals.bUVTrityl)
                                    Sensor_Config.LightsOn(7);
                                Man_Controlcs.WriteStatus("Run", "Col 7 Loaded -" + parts[1]);
                                break;

                            case 8:
                                globals.bCol8 = true;
                                bByFile8 = true;
                                tb_col8.Text = parts[1];
                                cycles = RetCycles(parts[1], ltrcode);
                                lbl_Length8.Text = cycles.ToString("0");
                                if (cycles > MaxCycles)
                                    MaxCycles = cycles;
                                if (globals.i12Ltr > 0)
                                {
                                    if (CheckForPlus(tb_col8.Text) && !noSequential)
                                    {
                                        MixPSPO = true;
                                        if (rb_Parallel.Checked && globals.bSeqeuncesLoaded)
                                        {
                                            if (MessageBox.Show("You have Mixed Phosphotioate and Phosphate backbone Oligos in your Sequences\n\n" +
                                                "Would you like to continue in Parallel?\n\nPress No to Switch to Sequential",
                                                "In Parallel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                            {
                                                rb_Sequenntial.Checked = true;
                                                rb_Parallel.Checked = false;
                                            }
                                        }
                                    }
                                }
                                if (globals.bUVTrityl)
                                    Sensor_Config.LightsOn(8);
                                Man_Controlcs.WriteStatus("Run", "Col 8 Loaded -" + parts[1]);
                                break;
                        }
                    }
                }
            }  // close the else if
            //fill the run-time sequence array with 1 letter codes
            FillSeqArray();

            //now validate the lines one by one
            for (int v = 0; v < 9; v++)
            {
                Debug.WriteLine("Run Seq = " + v.ToString() + "  is " + runSequences[v]);
                if (runSequences[v].Length > 0)
                {
                    if (!ValidSeq(runSequences[v], 0))
                        return;
                }
            }

            //now set globals
            globals.bSeqeuncesLoaded = true;
            SafeSetStatus("Sequence Load Complete");
            if (globals.bSeqeuncesLoaded && globals.bProtocolsLoaded)
                btn_Run.Enabled = true;
        }
        private void FillSeqArray()
        {
            //last store them all in a 1 letter array for running....
            runSequences[0] = string.Empty;
            //1 column at a time...
            if (globals.bCol1)
            {
                if (globals.i12Ltr == 0)
                    runSequences[1] = tb_col1.Text;
                else
                    runSequences[1] = ConvertSeq(tb_col1.Text, 0);

            }
            else
                runSequences[1] = string.Empty;
            //now column 2
            if (globals.bCol2)
            {
                if (globals.i12Ltr == 0)
                    runSequences[2] = tb_col2.Text;
                else
                    runSequences[2] = ConvertSeq(tb_col2.Text, 0);

            }
            else
                runSequences[2] = string.Empty;
            //now column 3
            if (globals.bCol3)
            {
                if (globals.i12Ltr == 0)
                    runSequences[3] = tb_col3.Text;
                else
                    runSequences[3] = ConvertSeq(tb_col3.Text, 0);

            }
            else
                runSequences[3] = string.Empty;
            //now column 4
            if (globals.bCol4)
            {
                if (globals.i12Ltr == 0)
                    runSequences[4] = tb_col4.Text;
                else
                    runSequences[4] = ConvertSeq(tb_col4.Text, 0);

            }
            else
                runSequences[4] = string.Empty;
            //now column 5
            if (globals.bCol5)
            {
                if (globals.i12Ltr == 0)
                    runSequences[5] = tb_col5.Text;
                else
                    runSequences[5] = ConvertSeq(tb_col5.Text, 0);

            }
            else
                runSequences[5] = string.Empty;
            //now column 6
            if (globals.bCol6)
            {
                if (globals.i12Ltr == 0)
                    runSequences[6] = tb_col6.Text;
                else
                    runSequences[6] = ConvertSeq(tb_col6.Text, 0);

            }
            else
                runSequences[6] = string.Empty;
            //now column 7
            if (globals.bCol7)
            {
                if (globals.i12Ltr == 0)
                    runSequences[7] = tb_col7.Text;
                else
                    runSequences[7] = ConvertSeq(tb_col7.Text, 0);

            }
            else
                runSequences[7] = string.Empty;
            //now column 8
            if (globals.bCol8)
            {
                if (globals.i12Ltr == 0)
                    runSequences[8] = tb_col8.Text;
                else
                    runSequences[8] = ConvertSeq(tb_col8.Text, 0);

            }
            else
                runSequences[8] = string.Empty;
        }
        private bool ValidSeq(string seq, int type)
        {
            SafeSetStatus("Validating Sequence");
            //get the letters from the setting file
            string[] validltrs = new string[14];
            if (globals.i12Ltr < 1)
            {
                validltrs[0] = Properties.Settings.Default.Am_1_lbl;
                validltrs[1] = Properties.Settings.Default.Am_2_lbl;
                validltrs[2] = Properties.Settings.Default.Am_3_lbl;
                validltrs[3] = Properties.Settings.Default.Am_4_lbl;
                validltrs[4] = Properties.Settings.Default.Am_5_lbl;
                validltrs[5] = Properties.Settings.Default.Am_6_lbl;
                validltrs[6] = Properties.Settings.Default.Am_7_lbl;
                validltrs[7] = Properties.Settings.Default.Am_8_lbl;
                validltrs[8] = Properties.Settings.Default.Am_9_lbl;
                validltrs[9] = Properties.Settings.Default.Am_10_lbl;
                validltrs[10] = Properties.Settings.Default.Am_11_lbl;
                validltrs[11] = Properties.Settings.Default.Am_12_lbl;
                validltrs[12] = Properties.Settings.Default.Am_13_lbl;
                validltrs[13] = Properties.Settings.Default.Am_14_lbl;
            }
            else
            {
                validltrs[0] = RetLetter(Properties.Settings.Default.Am_1_lbl);
                validltrs[1] = RetLetter(Properties.Settings.Default.Am_2_lbl);
                validltrs[2] = RetLetter(Properties.Settings.Default.Am_3_lbl);
                validltrs[3] = RetLetter(Properties.Settings.Default.Am_4_lbl);
                validltrs[4] = RetLetter(Properties.Settings.Default.Am_5_lbl);
                validltrs[5] = RetLetter(Properties.Settings.Default.Am_6_lbl);
                validltrs[6] = RetLetter(Properties.Settings.Default.Am_7_lbl);
                validltrs[7] = RetLetter(Properties.Settings.Default.Am_8_lbl);
                validltrs[8] = RetLetter(Properties.Settings.Default.Am_9_lbl);
                validltrs[9] = RetLetter(Properties.Settings.Default.Am_10_lbl);
                validltrs[10] = RetLetter(Properties.Settings.Default.Am_11_lbl);
                validltrs[11] = RetLetter(Properties.Settings.Default.Am_12_lbl);
                validltrs[12] = RetLetter(Properties.Settings.Default.Am_13_lbl);
                validltrs[13] = RetLetter(Properties.Settings.Default.Am_14_lbl);

            }

            //have to rewrite this...after open evaluate 1letter or two letter codes, strip off the -
            char[] seqchar = seq.ToCharArray();
            int ilen = seqchar.Length;
            int start = 0;
            if (type > 0)
                start = 2;

            for (int i=start; i< ilen;i++ )
            {
                if (!validltrs.Any( x=>x.Contains(seqchar[i].ToString())))
                {
                    MessageBox.Show(seq + "\n\nFailed Validation at -"+seqchar[i].ToString()+"!\n      Please review", "Failed Validation", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }
            }
            SafeSetStatus("Validation Complete");
            return true;
        }

        private bool SetAutoScaleValue(int Seq)
        {
            int index = -1;
           

            switch(Seq)
            {
                case 0:
                    index = cb_Col1Scale.SelectedIndex;
                    globals.scalefactor = GetScaleFactor(index);
                    return true;
                case 1:
                    index = cb_Col2Scale.SelectedIndex;
                    globals.scalefactor = GetScaleFactor(index);
                    return true;
                case 2:
                    index = cb_Col3Scale.SelectedIndex;
                    globals.scalefactor = GetScaleFactor(index);
                    return true;
                case 3:
                    index = cb_Col4Scale.SelectedIndex;
                    globals.scalefactor = GetScaleFactor(index);
                    return true;
                case 4:
                    index = cb_Col5Scale.SelectedIndex;
                    globals.scalefactor = GetScaleFactor(index);
                    return true;
                case 5:
                    index = cb_Col6Scale.SelectedIndex;
                    globals.scalefactor = GetScaleFactor(index);
                    return true;
                case 6:
                    index = cb_Col7Scale.SelectedIndex;
                    globals.scalefactor = GetScaleFactor(index);
                    return true;
                case 7:
                    index = cb_Col8Scale.SelectedIndex;
                    globals.scalefactor = GetScaleFactor(index);
                    return true;
            }
            return false;
        }
        private double GetScaleFactor(int index)
        {
            double scalefactor = 1.0;

            //index is from 0.2, 0.5, 1, 2,3,4, 5 list for each combobox
            switch (index)
            {
                case 0:
                    scalefactor = Properties.Settings.Default.SF_02;
                    return scalefactor;
                case 1:
                    scalefactor = Properties.Settings.Default.SF_05;
                    return scalefactor;
                case 2:
                    scalefactor = Properties.Settings.Default.SF_10;
                    return scalefactor;
                case 3:
                    scalefactor = Properties.Settings.Default.SF_20;
                    return scalefactor;
                case 4:
                    scalefactor = Properties.Settings.Default.SF_30;
                    return scalefactor;
                case 5:
                    scalefactor = Properties.Settings.Default.SF_40;
                    return scalefactor;
                case 6:
                    scalefactor = Properties.Settings.Default.SF_50;
                    return scalefactor;
            }

            return 1.0;
        }
        private void CheckColGlobal()
        {
            if (globals.bCol1 || globals.bCol2 || globals.bCol3 || globals.bCol4
               || globals.bCol5 || globals.bCol6 || globals.bCol7 || globals.bCol8)
            {
                //if any are true set the global for col true and if both cols loaded 
                // and protocols loaded set run button enabled
                globals.bSeqeuncesLoaded = true;
                if (globals.bSeqeuncesLoaded && globals.bProtocolsLoaded)
                    btn_Run.Enabled = true;
            }
            else
            {
                globals.bSeqeuncesLoaded = false;
                btn_Run.Enabled = false;
            }
        }
        private void btn_ClrCol1_Click(object sender, EventArgs e)
        {
            SafeSetStatus("Clearing Sequence");
            tb_col1.Clear();
            tb_col1.Text = "";

            //reset the font
            tb_col1.SelectionFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
            tb_col1.SelectionColor = Color.Black;

            lbl_Length1.Text = "0";
            globals.bCol1 = false;
            CheckColGlobal();
            bByFile1 = false;
            runSequences[1] = string.Empty;
            Man_Controlcs.WriteStatus("Run", "Col 1 Sequence Cleared");

            //turn monitor off if needed
            if (globals.bUV1On)
            {
                Man_Controlcs.SendControllerMsg(2, valves.UV1off);
                Man_Controlcs.SyncWait(50);
            }
            globals.bUV1On = false;
            SafeSetStatus("Clearing Sequence Complete");
        }

        private void btn_ClrCol2_Click(object sender, EventArgs e)
        {
            SafeSetStatus("Clearing Sequence");
            tb_col2.Text = "";
            lbl_Length2.Text = "0";
            globals.bCol2 = false;
            bByFile2 = false;
            CheckColGlobal();
            runSequences[2] = string.Empty;
            Man_Controlcs.WriteStatus("Run", "Col 2 Sequence Cleared");

            //turn monitor off if needed
            if (globals.bUV2On)
            {
                Man_Controlcs.SendControllerMsg(2, valves.UV2off);
                Man_Controlcs.SyncWait(50);
            }
            globals.bUV2On = false;
            SafeSetStatus("Clearing Sequence Complete");
        }

        private void btn_ClrCol3_Click(object sender, EventArgs e)
        {
            SafeSetStatus("Clearing Sequence");
            tb_col3.Text = "";
            lbl_Length3.Text = "0";
            globals.bCol3 = false;
            bByFile3 = false;
            CheckColGlobal();
            runSequences[3] = string.Empty;
            Man_Controlcs.WriteStatus("Run", "Col 3 Sequence Cleared");

            //turn monitor off if needed
            if (globals.bUV3On)
            {
                Man_Controlcs.SendControllerMsg(2, valves.UV3off);
                Man_Controlcs.SyncWait(50);
            }
            globals.bUV3On = false;
            SafeSetStatus("Clearing SequenceComplete");
        }

        private void btn_ClrCol4_Click(object sender, EventArgs e)
        {
            SafeSetStatus("Clearing Sequence");
            tb_col4.Text = "";
            lbl_Length4.Text = "0";
            globals.bCol4 = false;
            bByFile4 = false;
            CheckColGlobal();
            runSequences[4] = string.Empty;
            Man_Controlcs.WriteStatus("Run", "Col 4 Sequence Cleared");

            //turn monitor off if needed
            if (globals.bUV4On)
            {
                Man_Controlcs.SendControllerMsg(2, valves.UV4off);
                Man_Controlcs.SyncWait(50);
            }
            globals.bUV4On = false;
            SafeSetStatus("Clearing Sequence Complete");
        }

        private void btn_ClrCol5_Click(object sender, EventArgs e)
        {
            SafeSetStatus("Clearing Sequence");
            tb_col5.Text = "";
            lbl_Length5.Text = "0";
            globals.bCol5 = false;
            bByFile5 = false;
            CheckColGlobal();
            runSequences[5] = string.Empty;
            Man_Controlcs.WriteStatus("Run", "Col 5 Sequence Cleared");

            //turn monitor off if needed
            if (globals.bUV5On)
            {
                Man_Controlcs.SendControllerMsg(2, valves.UV5off);
                Man_Controlcs.SyncWait(50);
            }
            globals.bUV5On = false;
            SafeSetStatus("Clearing Sequence Complete");
        }

        private void btn_ClrCol6_Click(object sender, EventArgs e)
        {
            SafeSetStatus("Clearing Sequence");
            tb_col6.Text = "";
            lbl_Length6.Text = "0";
            globals.bCol6 = false;
            bByFile6 = false;
            CheckColGlobal();
            runSequences[6] = string.Empty;
            Man_Controlcs.WriteStatus("Run", "Col 6 Sequence Cleared");

            //turn monitor off if needed
            if (globals.bUV6On)
            {
                Man_Controlcs.SendControllerMsg(2, valves.UV6off);
                Man_Controlcs.SyncWait(50);
            }
            globals.bUV6On = false;
            SafeSetStatus("Clearing Sequence Complete");
        }

        private void btn_ClrCol7_Click(object sender, EventArgs e)
        {
            SafeSetStatus("Clearing Sequence");
            tb_col7.Text = "";
            lbl_Length7.Text = "0";
            globals.bCol7 = false;
            bByFile7 = false;
            CheckColGlobal();
            runSequences[7] = string.Empty;
            Man_Controlcs.WriteStatus("Run", "Col 7 Sequence Cleared");

            //turn monitor off if needed
            if (globals.bUV7On)
            {
                Man_Controlcs.SendControllerMsg(2, valves.UV7off);
                Man_Controlcs.SyncWait(50);
            }
            globals.bUV7On = false;
            SafeSetStatus("Clearing Sequence Complete");
        }

        private void btn_ClrCol8_Click(object sender, EventArgs e)
        {
            SafeSetStatus("Clearing Sequence");
            tb_col8.Text = "";
            lbl_Length8.Text = "0";
            globals.bCol8 = false;
            bByFile8 = false;
            CheckColGlobal();
            runSequences[8] = string.Empty;
            Man_Controlcs.WriteStatus("Run", "Col 8 Sequence Cleared");

            //turn monitor off if needed
            if (globals.bUV8On)
            {
                Man_Controlcs.SendControllerMsg(2, valves.UV8off);
                Man_Controlcs.SyncWait(50);
            }
            globals.bUV8On = false;
            SafeSetStatus("Clearing Sequence Complete");
        }

        private void MI_About_Click(object sender, EventArgs e)
        {
            using (RiboBioAboutBox dlg = new RiboBioAboutBox())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    return;
                    // whatever you need to do with result
                }
            }
        }

        private void MI_ViewLog_Click(object sender, EventArgs e)
        {
            if (globals.blogging)
                System.Diagnostics.Process.Start("notepad.exe", globals.log_file);
            else
                MessageBox.Show("Must turn logging on first", "Log File View", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Menu_UniversalSup_Click(object sender, EventArgs e)
        {
            if (Menu_UniversalSup.Checked)
            {
                bUniversalSupport = false;
                Menu_UniversalSup.Checked = false;
                Properties.Settings.Default.UniversalSupport = false;
                Properties.Settings.Default.Save();
            }
            else
            {
                bUniversalSupport = true;
                Menu_UniversalSup.Checked = true;
                Properties.Settings.Default.UniversalSupport = true;
                Properties.Settings.Default.Save();
                //  MessageBox.Show("here univ Support = true");
            }

        }

        private void MI_Exit_Click(object sender, EventArgs e)
        {
            SafeSetStatus("Exiting Run...");
            if (globals.bIsRunning)
            {
                MessageBox.Show("You must first stop the run before exiting",
                                      "Stop Run", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                if (globals.bUVTrityl)
                    ResetCells();

                this.DialogResult.Equals(DialogResult.OK);
                this.lbl_Status.Text = "Status: ";
                Close();
           
            }

        }

        private void Menu_UV_Strip_Click(object sender, EventArgs e)
        {
            //check to make sure a monitor is configured and on
            if (globals.bUVTrityl && globals.bIsRunning)
            {
                
                    if (!globals.bUV1On && !globals.bUV2On && !globals.bUV3On && !globals.bUV4On &&
                    !globals.bUV5On && !globals.bUV6On && !globals.bUV7On && !globals.bUV8On)
                    {
                        MessageBox.Show("You Must Configure a Monitor\n\nOr turn on a cell", "No Monitor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                
            }

            if (globals.sc != null)
                globals.sc = new stripchartcs();

            globals.sc.Show();

        }

        private void Menu_UV_Bar_Click(object sender, EventArgs e)
        {
            if (WindowAlreadyOpen.WindowOpen("Trityl Bar Chart Histogram"))
                return;

            if (globals.bUVTrityl && globals.bIsRunning)
            {
                if (!globals.bUV1On && !globals.bUV2On && !globals.bUV3On && !globals.bUV4On &&
                !globals.bUV5On && !globals.bUV6On && !globals.bUV7On && !globals.bUV8On)
                {
                    MessageBox.Show("You Must Configure a Monitor\n\nOr turn on a cell", "No Monitor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            /*if (!File.Exists(globals.UVBarCSVfile))
            {
                MessageBox.Show("The file does not exit yet, try later", "No File");
                return;
            }*/
            if (globals.sb != null)
                globals.sb = new barchartcontrol();
            
           if(!bMonitoringUV)
                globals.sb.Show();

            bMonitoringUV = true;
        }

       

       
        private void ResetCells()
        {
            //set back to default
            globals.bUV1On = Properties.Settings.Default.UV1On;
            globals.bUV2On = Properties.Settings.Default.UV2On;
            globals.bUV3On = Properties.Settings.Default.UV3On;
            globals.bUV4On = Properties.Settings.Default.UV4On;
            globals.bUV5On = Properties.Settings.Default.UV5On;
            globals.bUV6On = Properties.Settings.Default.UV6On;
            globals.bUV7On = Properties.Settings.Default.UV7On;
            globals.bUV8On = Properties.Settings.Default.UV8On;

            //now reopen the default cells
            if (globals.bUV1On)
                Man_Controlcs.SendControllerMsg(1, valves.UV1on);
            if (globals.bUV2On)
                Man_Controlcs.SendControllerMsg(1, valves.UV2on);
            if (globals.bUV3On)
                Man_Controlcs.SendControllerMsg(1, valves.UV3on);
            if (globals.bUV4On)
                Man_Controlcs.SendControllerMsg(1, valves.UV4on);
            if (globals.bUV5On)
                Man_Controlcs.SendControllerMsg(1, valves.UV5on);
            if (globals.bUV6On)
                Man_Controlcs.SendControllerMsg(1, valves.UV6on);
            if (globals.bUV7On)
                Man_Controlcs.SendControllerMsg(1, valves.UV7on);
            if (globals.bUV8On)
                Man_Controlcs.SendControllerMsg(1, valves.UV8on);

        }

        /*will completely get rid of the X and the menu for closing
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_NOCLOSE = 0x200;

                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_NOCLOSE;
                return cp;
            }
        }*/
        private void SeNARun_FormClosing(object sender, FormClosingEventArgs e)
        {
            //for some reason when we close barchart this gets a close message too...

            SafeNativeMethods.ShowMain((int)Main_Form.hmainWind,1);
            SafeSetStatus("Program Exiting...");

            

            if (globals.bIsRunning)  //don't allow closing
            {
                MessageBox.Show("Closing Not Allowed while running. \nYou must first stop the run", 
                    "Exit Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    this.lbl_Status.Text = "Status: ";
                    e.Cancel = true;

                }
            }
            else
            {
                //kill timers
                if (clearstat != null)
                {
                    clearstat.Stop();
                    clearstat.Elapsed -= ClearStatEvent; //disconnect the event handler
                    Man_Controlcs.SyncWait(1000); //wait 1 second to clear all events
                    clearstat.Dispose();
                }

                //reset the recycle automatic state to off
                //set recycle valve to autooff so it can be manually controlled
                //Man_Controlcs.SendControllerMsg(1, valves.recycleautooff);

                if (globals.bUVTrityl  && !globals.bDemoMode)
                    ResetCells();

                //clear all list and text boxes so they refill on coming back
                lbl_PreRun.Text = String.Empty;
                lbl_Run.Text = String.Empty;
                lbl_Post.Text = String.Empty;
                tb_col1.Clear();
                tb_col2.Clear();
                tb_col3.Clear();
                tb_col4.Clear();
                tb_col5.Clear();
                tb_col6.Clear();
                tb_col7.Clear();
                tb_col8.Clear();

                globals.bProtocolsLoaded = false;
                globals.bSeqeuncesLoaded = false;
                bMonitoringUV = false;

                this.DialogResult.Equals(DialogResult.OK);

                if(RunTimer.Enabled)
                {
                    RunTimer.Stop();
                    RunTimer.Enabled = false;
                }
                
            }
            //if the bar chart is open then close it
            if (globals.sb != null)
            {
                //close the UV monitor
                bMonitoringUV = false;
                globals.sb.CloseMe();
                //globals.sb.BeginInvoke(new MethodInvoker(CloseMe));
               
                globals.sb = null;
            }
        }
        private void CheckAlarms()
        {
            if (!globals.bMonPressure) //not monitoring pressure just return
                return;

            SafeSetStatus("Checking Alarms");
            //Amidite Pressure Alarm
            if (globals.bAmPresAlarm)
            {
                //get the value from the screen
                double dPres = Double.Parse(l_AmP.Text.Substring(0, l_AmP.Text.Length - 4));
                //we will show a Message Box, this will "freeze" the tread until user responds
                // if the user responds O.K. the run will continue, if Cancel, the run will terminate
                if (dPres < globals.iAmPresAmt)
                {
                    if (MessageBox.Show("Amidite Pressure Alarm Reached\nPlease fix the problem, then press O.K. to Continue\n\nOr...Press Cancel to Terminate the Run Now", 
                        "ALARM", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    {
                        bRunProcessing = false;
                        globals.bIsRunning = false;
                    }
                } 

            }
            //Reagent Pressure Alarm
            if (globals.bAmPresAlarm)
            {
                //get the value from the screen
                double dPres = Double.Parse(l_ReagP.Text.Substring(0, l_ReagP.Text.Length - 4)); ;
                //we will show a Message Box, this will "freeze" the tread until user responds
                // if the user responds O.K. the run will continue, if Cancel, the run will terminate
                if (dPres < globals.iRgtPresAmt)
                {
                    if (MessageBox.Show("Reagent Pressure Alarm Reached\n\nPlease fix the problem, then press O.K. to Continue\n\nOr...Press Cancel to Terminate the Run Now",
                        "ALARM", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    {
                        bRunProcessing = false;
                        globals.bIsRunning = false;
                    }
                }

            }
            //last check the Trityl Alarm Flag
            //Once again, freeze the current running synthesis until OK is pressed
            if(globals.bTritylAlarOn && !globals.bSkipTritylAlarm)
            {
                if (MessageBox.Show("Trityl Yield Dropped below the Setpoint\n\nPlease fix the problem, then press O.K. to Continue\n\nOr...Press Cancel to Terminate the Run Now",
                    "ALARM", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                {
                    bRunProcessing = false;
                    globals.bIsRunning = false;
                }
                else
                {   //reset the alarm status
                    globals.bTritylAlarOn = false;
                    if (MessageBox.Show("Ignore future trityl alarms during this synthesis?", "Ignore", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        globals.bSkipTritylAlarm = true;
                }
            }
            SafeSetStatus("Check Alarms Complete");
        }
        private bool checkSameScale()
        {
            double scale = 0.0;
            int firstempty = 0;
            string cbval = string.Empty;

            //make sure there are more than two sequences
            if (runSequences.Length < 2)
                return true;

            //get the first scale
            for(int s=0; s<9; s++)
            {
                if(runSequences[s].Length > 0)
                {
                    firstempty = s;
                    break; //leave the for loop
                }
            }
            //get the scale of the first sequence
            switch(firstempty)
            {
                case 1:
                    cbval = cb_Col1Scale.Text;
                    break;
                case 2:
                    cbval = cb_Col2Scale.Text;
                    break;
                case 3:
                    cbval = cb_Col3Scale.Text;
                    break;
                case 4:
                    cbval = cb_Col4Scale.Text;
                    break;
                case 5:
                    cbval = cb_Col5Scale.Text;
                    break;
                case 6:
                    cbval = cb_Col6Scale.Text;
                    break;
                case 7:
                    cbval = cb_Col7Scale.Text;
                    break;
            }
            scale = double.Parse(cbval);
            Debug.WriteLine("The Scale Value for column " + firstempty.ToString() + " is " + scale.ToString("0.0"));

            //now compare
            for(int c=(firstempty+1); c<8; c++)
            {
                switch(c)
                {
                    case 2:
                        if (!cbval.Equals(cb_Col2Scale.Text))
                            return false;
                        break;
                    case 3:
                        if (!cbval.Equals(cb_Col3Scale.Text))
                            return false;
                        break;
                    case 4:
                        if (!cbval.Equals(cb_Col4Scale.Text))
                            return false;
                        break;
                    case 5:
                        if (!cbval.Equals(cb_Col5Scale.Text))
                            return false;
                        break;
                    case 6:
                        if (!cbval.Equals(cb_Col6Scale.Text))
                            return false;
                        break;
                    case 7:
                        if (!cbval.Equals(cb_Col7Scale.Text))
                            return false;
                        break;
                    case 8:
                        if (!cbval.Equals(cb_Col8Scale.Text))
                            return false;
                        break;
                }
            }
            return true;
        }
        private async void btn_Run_Click(object sender, EventArgs e)
        {
            String time = "";
            string[] runningSeq = new string[9];
            int saveseq = 0;

            int TotalOligos = 0;
            int curOligo = 0;
            //disable all buttons
            if (MessageBox.Show("You are About to Start Synthesis....\n\nAre you sure you:\n\n* Filled All Reagents and Checked Consumption?\n\n" +
                "* Added enough amdite?\n\n* Securely installed columns?\n\n* Checked the Gas Supply?\n\n* Checked the Waste Container? \n", "Start", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            //if Trityl Monitor Open from previous run, close it...
            if (bMonitoringUV)
                if (WindowAlreadyOpen.WindowOpen("Trityl Bar Chart Histogram"))
                    globals.sb.Close();

            //check scales if using Smart coupling mode...they must all be the same
            if (rb_Smart.Checked)  //make sure all scales are the same
            {
                if(!checkSameScale())
                {
                    MessageBox.Show("All Oligos MUST be the same scale for Smart Coupling\n\nDifferent Scales are Not Allowed", "No Mixed Scales", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
            }

            //check to make sure they cleared all previous sequences
            if (tb_col1.Text.Contains("***") ||
               tb_col2.Text.Contains("***") ||
               tb_col3.Text.Contains("***") ||
               tb_col4.Text.Contains("***") ||
               tb_col5.Text.Contains("***") ||
               tb_col6.Text.Contains("***") ||
               tb_col7.Text.Contains("***") ||
               tb_col8.Text.Contains("***"))
            {
                MessageBox.Show("You must clear all of the previous sequences and reload them prior to the next Run\n*** not Allowed", "Improper Sequence", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            SafeSetStatus("Starting Run....");

            //disable the 1 2 letter menus
            Menu_1Letter.Enabled = false;
            Menu_2Letter.Enabled = false;
            //setup UI
            gb_RunProtos.Enabled = false;
            DisableTBs();
            btn_Run.Enabled = false;
            btn_OK.Enabled = false;
            btn_Pause.Enabled = true;
            btn_Terminate.Enabled = true;
            gb_RunOpts.Enabled = false;

            //running flags
            globals.bIsRunning = true;
            bRunProcessing = true;

            // now do system startup if there is one...
            if (lbl_Start.Text.Length > 1 && !(lbl_Start.Text.Contains("none")))  //you don't have to have a startup protocol
            {
                if (globals.bProtocolsLoaded && globals.bSeqeuncesLoaded)
                {
                    //Make Sure the Listbox is full if not fill it...
                    if (!(ProtoListView.Items.Count > 0) && bRunProcessing && !bTerminateEndofCycle && !bTerminateEndofStep)
                        FillTheList(lbl_Start.Text);

                    //Set Program Text to Running...
                    this.Text = "SeNA80 Run - Running the Start Protocol ...";
                    SafeSetStatus("Starting System Preparation Protocol");

                    //Update the Label
                    this.lbl_CurProtocol.Text = "Running the Start Protocol.... ";
                    this.lbl_curBase.Text = "";
                    SafeSetStatus("Running Start Protocol...");

                    //now do Begin and End
                    BeginandEnd();
                    while (!bBeginandEnd && bRunProcessing)
                        await PutTaskDelay(350);

                    bBeginandEnd = false;

                    if (bTerminateEndofStep)
                    {
                        // first kill any timers
                        if (RunTimer.Enabled)
                        {
                            RunTimer.Stop();
                            RunTimer.Enabled = false;
                        }
                        bRunProcessing = false;
                        globals.bIsRunning = false;

                        SafeSetStatus("Run Terminated at end of Start Cycle...");
                        Man_Controlcs.WriteStatus("Run", "Terminated Run at End of Start Cycle...");
                    }

                }
            }

            //if not in parallel we will copy the sequences to an array then feed it to RunningSequences one at a time and set MaxCycles 
            //every oligo to the current seuqence length; otherwise, we will just feed it the whole group and say do it once.
            if (!inParallel)
            {
                bool gotFirst = false;

                //first turn the UV cells off
                Sensor_Config.LightsOn(0);
                Debug.WriteLine("Sesor Off ");

                for (int u = 0; u < 9; u++)
                {
                    if (runSequences[u].Length > 0)
                    {
                        //so we copy the runSequence array to a temporary to storage array
                        //then make the runSequences array a single oligo...
                        runningSeq[u] = runSequences[u];
                        if (TotalOligos > 0)
                            runSequences[u] = string.Empty;

                        if (!gotFirst)
                        {
                            Debug.WriteLine("The first runningSeq is " + runSequences[u]);
                            curOligo = u;
                            MaxCycles = runSequences[u].Length;

                            //turn lights on
                            Sensor_Config.LightsOn(u);
                            Man_Controlcs.SyncWait(60);

                            Debug.WriteLine("Sesor On = " + (u + 1).ToString());
                            gotFirst = true;
                        }
                        else
                            runSequences[u] = string.Empty;


                        TotalOligos++;
                    }
                    else
                        runningSeq[u] = string.Empty;

                }
            }
            else
            {
                TotalOligos = 1;

                //make sure lights are on;
                for (int TJ = 0; TJ < 9; TJ++)
                {
                    if (runSequences[TJ].Length > 1)
                        Sensor_Config.LightsOn(TJ);
                }
            }



            for (int OligosToMake = 0; OligosToMake < TotalOligos; OligosToMake++)
            {
                Debug.WriteLine("Oligos to Make is " + OligosToMake.ToString() + "  Total Oligos = " + TotalOligos.ToString());
                SafeSetStatus("Status: Starting the Synthesis of Your Oligo Sequences...");
                lbl_Status.Text = "Status: Starting the Synthesis of Your Oligo Sequences...";
                //check for bRunProcessing
                if (!bRunProcessing)
                    break;

                //skip for first step 
                if (OligosToMake > 0 && !inParallel)
                {
                    //empty the array
                    for (int y = 0; y < 9; y++)
                        runSequences[y] = string.Empty;

                    //now put the second oligo in the runSequences array and start again
                    for (int w = curOligo + 1; w < 9; w++)
                    {
                        bool bfound = false;

                        //turn all lights off
                        Sensor_Config.LightsOn(0);
                        Debug.WriteLine("Sensors Off");

                        //now put one seuqence back in
                        if (runningSeq[w].Length > 0)
                        {
                            if (!bfound)
                            {
                                runSequences[w] = runningSeq[w];
                                MaxCycles = runSequences[w].Length;
                                Debug.WriteLine("The next runningSeq is " + runSequences[w] + "   for " + MaxCycles.ToString() + " Cycles");
                                lbl_Status.Text = "Status: The next Oligo to make is " + runSequences[w] + "   for " + MaxCycles.ToString() + " Cycles";
                                curOligo = w;
                                bfound = true;
                                Sensor_Config.LightsOn(w);
                                Man_Controlcs.SyncWait(60);

                                //break out of loop
                                Debug.WriteLine("Sesor On = " + (w + 1).ToString());
                                w = 10;
                            }
                            else
                                runningSeq[w] = string.Empty;

                        }

                    }

                }
                //Record the time
                dtStart = DateTime.Now;
                globals.avgScale = 0;
                globals.noOligos = 0;

                //Determine scale for reagent additions
                for (int a = 0; a < 8; a++)
                {
                    string curSeq = GetSeq(a, 1);
                    if (curSeq.Length > 0)
                    {
                        string selected = string.Empty;
                        globals.noOligos = globals.noOligos + 1;

                        switch (a)
                        {
                            case 0:
                                selected = cb_Col1Scale.Text;
                                globals.avgScale += double.Parse(selected);
                                break;
                            case 1:
                                selected = cb_Col2Scale.Text;
                                globals.avgScale += double.Parse(selected);
                                break;
                            case 2:
                                selected = cb_Col3Scale.Text;
                                globals.avgScale += double.Parse(selected);
                                break;
                            case 3:
                                selected = cb_Col4Scale.Text;
                                globals.avgScale += double.Parse(selected);
                                break;
                            case 4:
                                selected = cb_Col5Scale.Text;
                                globals.avgScale += double.Parse(selected);
                                break;
                            case 5:
                                selected = cb_Col6Scale.Text;
                                globals.avgScale += double.Parse(selected);
                                break;
                            case 6:
                                selected = cb_Col7Scale.Text;
                                globals.avgScale += double.Parse(selected);
                                break;
                            case 7:
                                selected = cb_Col8Scale.Text;
                                globals.avgScale += double.Parse(selected);
                                break;

                        }
                    }
                }
                globals.avgScale = globals.avgScale / globals.noOligos;
                Man_Controlcs.WriteStatus("Scal Reagents - ", "Using " + globals.avgScale.ToString("0.0") + " as the scaling factor ");
                SafeSetStatus("Calculated Reagent Scaling Factor - " + globals.avgScale.ToString("0.0"));

                //set up the bar chart file
                string CSVName = "UV_BAR_" + DateTime.Now.ToString("MMddHHmm") + ".CSV";
                globals.UVBarCSVfile = globals.CSV_path + CSVName;
                Properties.Settings.Default.UV_Bar_File = CSVName;
                Properties.Settings.Default.Save();
                //set the menu for revier
                Menu_UV_Bar.Text = "&View Running UV Bar";


                //Perform Each command in the protocol list box one at a time
                //jMessageBox.Show("Cycles to Run -"+ MaxCycles.ToString());
                //Once the Prerun is done then Loop for with the main protocol
                //have a nested loop for each of the amidites


                time = String.Format("{0:F}", dtStart);
                Man_Controlcs.WriteStatus("Run Program", "PreRun Started at:  " + time);
                SafeSetStatus("PreRun Started at: " + time);
                lbl_Status.Text = "Status: PreRun Started at: " + time;
                //Make sure the list boxk is filled with the Prep Protocol
                if (bRunProcessing && !bTerminateEndofCycle && !bTerminateEndofStep)
                    FillTheList(this.lbl_PreRun.Text);
                Debug.WriteLine("Filled the PreRun List box");

                if (globals.bProtocolsLoaded && globals.bSeqeuncesLoaded)
                {
                    //Set Program Text to Running...
                    this.Text = "SeNA80 Run - Running the Prep Protocol ...";
                    SafeSetStatus("Starting Prep Protocol");

                    //Later we will put in LEDs to show the system is running
                    //Chaning the LED to run would go here

                    //Update the Label
                    this.lbl_CurProtocol.Text = "Running the Prep Protocol.... ";
                    this.lbl_curBase.Text = "";
                    SafeSetStatus("Running Prep Protocol...");

                    //now do Begin and End
                    BeginandEnd();
                    while (!bBeginandEnd && bRunProcessing)
                        await PutTaskDelay(350);

                    bBeginandEnd = false;

                    if (bTerminateEndofStep)
                    {
                        // first kill any timers
                        if (RunTimer.Enabled)
                        {
                            RunTimer.Stop();
                            RunTimer.Enabled = false;
                        }
                        bRunProcessing = false;
                        globals.bIsRunning = false;

                        SafeSetStatus("Run Terminated at end of Prep Cycle...");
                        Man_Controlcs.WriteStatus("Run", "Terminated Run at End of Prep Cycle...");
                    }


                    // Now empty the listbox and then fill it with the loop protocol
                    if (bRunProcessing)
                    {

                        //put the looping protocol in the 2nd list box
                        String fName = globals.protocol_path + this.lbl_Run.Text;

                        String[] lblines = File.ReadAllLines(fName);

                        // first Check to see if there is already info in the listbox (i.e. we came back after a reload)
                        // if yes, clear it
                        if (lb_loop.SelectedItems.Count > 0)
                            lb_loop.Items.Clear();

                        //if more than one oligo and PSPO both there then and in parallel check then rebuild the looping protocol
                        MixPSPO = finalPSPOCheck();

                        if (MixPSPO && rb_Parallel.Checked && globals.noOligos > 1)
                        {
                            for (int t = 0; t < lblines.Length; t++)
                            {
                                if (lblines[t].Contains("Mix"))
                                {
                                    lblines = SplitOxThiol(lblines);
                                    t = lblines.Length + 5;
                                }
                            }
                        }

                        //find the end of loop
                        int max = 0;
                        for (int l = 0; l < lblines.Length; l++)
                        {
                            if (lblines[l].Contains("[Volumes]"))
                            {
                                max = l;
                                break;
                            }
                        }
                        string[] slblines = new string[max];
                        for (int b = 0; b < max; b++)
                            slblines[b] = lblines[b];

                        //now create three lists Ox only Thio only and Both
                        string[] onlyOx = slblines;
                        string[] onlyThiol = slblines;
                        string[] bothOxThio = slblines;
                        if (MixPSPO)
                        {
                            onlyOx = OnlyOxThiol(0, onlyOx);
                            onlyThiol = OnlyOxThiol(1, onlyThiol);
                        }
                        /*fill the list box
                        for (int i = 1; i < max; i++)
                            lb_loop.Items.Add(slblines[i]);

                        //Set Focus   
                        lb_loop.SelectedIndex = 0;
                        lb_loop.Focus();*/
                        bLooping = true;
                        DateTime[] dtCycle = new DateTime[MaxCycles];
                        dtEnd = DateTime.Now;

                        int StartCycle = 0;

                        globals.iCycle = 0;

                        // Need a lot more logic here to 1) Open the deblock protocol
                        // run it 2) decide what amidtie to do for each column 
                        // 3) do the other protocols.. this will be complex logic
                        if (!bUniversalSupport)
                            StartCycle = 1;
                        SafeSetStatus("Starting Run Protocol");

                        time = String.Format("{0:F}", dtStart);
                        Man_Controlcs.WriteStatus("Run Program", "Run Started at:  " + time);
                        SafeSetStatus("PreRun Started at: " + time);
                        lbl_Status.Text = "Status: Stated Run Cycling Protocol on: " + time;
                        for (globals.iCycle = StartCycle; globals.iCycle < MaxCycles; globals.iCycle++)
                        {
                            //first determine which loop protocol to run
                            int cp = OxThioBoth(globals.iCycle);  //returns 0 for both 1 for Ox only 2 for Thio only.... 
                            Debug.WriteLine("Current Cycle involves Both Ox Thio = " + cp.ToString());

                            string[] whichlines = new string[0];
                            if (cp == 0 && rb_Parallel.Checked)
                                whichlines = bothOxThio;
                            if (globals.noOligos < 2)
                                whichlines = bothOxThio;
                            if (rb_Sequenntial.Checked)
                                whichlines = bothOxThio;
                            else if (cp == 1)
                                whichlines = onlyOx;
                            else if (cp == 2)
                                whichlines = onlyThiol;
                            else
                                whichlines = bothOxThio;


                            //fill the list box
                            lb_loop.Items.Clear();
                            for (int i = 1; i < whichlines.Length; i++)
                                lb_loop.Items.Add(whichlines[i]);

                            //Set Focus   
                            lb_loop.SelectedIndex = 0;
                            lb_loop.Focus();


                            time = String.Format("{0:F}", DateTime.Now);
                            Man_Controlcs.WriteStatus("Run Program", "Synthesis Cycle " + (globals.iCycle + 1).ToString() + " Started at:  " + time);
                            SafeSetStatus("Synthesis Cycle " + (globals.iCycle + 1).ToString() + " Started at:  " + time);

                            //Check Alarms
                            CheckAlarms();
                            this.lbl_curBase.Text = " Cycle  - " + (globals.iCycle + 1).ToString("0") + " Deblocking...";


                            /*if (globals.bUVTrityl || globals.bCondTrityl)
                            {
                                await Man_Controlcs.CheckPolling();
                            }*/

                            //determine the loop time and report the estimated end time
                            dtCycle[globals.iCycle] = DateTime.Now;


                            //after the second cycle we can determine the loop time and estimate the total time
                            if (globals.iCycle > 0)
                            {
                                TimeSpan tsCycle = dtCycle[globals.iCycle] - dtCycle[globals.iCycle - 1];

                                //MessageBox.Show("Cycle Time " + tsCycle.ToString(@"hh\:mm\:ss"));
                                TimeSpan duration = new TimeSpan();
                                duration = TimeSpan.FromTicks(tsCycle.Ticks * MaxCycles);

                                //MessageBox.Show("expected duration - " + duration.ToString(@"hh\:mm\:ss"));
                                TimeSpan tsEnd = TimeSpan.FromTicks(dtStart.Ticks + duration.Ticks);

                                String status = "Status: Current Cycle time -  " + tsCycle.ToString(@"hh\:mm\:ss") + "  Total Syntheis Time -  " + duration.ToString(@"hh\:mm\:ss") + "  Estimated End Time -  " + tsEnd.ToString(@"hh\:mm\:ss");

                                SafeSetStatus("Current Cycle time -  " + tsCycle.ToString(@"hh\:mm\:ss") + "  Total Syntheis Time -  " + duration.ToString(@"hh\:mm\:ss") + "  Estimated End Time -  " + tsEnd.ToString(@"hh\:mm\:ss"));

                                //MessageBox.Show(status);
                                this.lbl_Status.Text = status;


                            }
                            //drop out if terminate is pushed
                            if (!globals.bIsRunning)
                                globals.iCycle = MaxCycles + 1;

                            // Reset the text string
                            this.Text = "SeNA80 Run - Performing Synthesis Cycle " + (globals.iCycle + 1).ToString() + " of " + MaxCycles.ToString() + ".....";
                            SafeSetStatus("Performing Synthesis Cycle " + (globals.iCycle + 1).ToString() + " of " + MaxCycles.ToString() + ".....");


                            this.lbl_CurProtocol.Text = "Synthesizing Cycle " + (globals.iCycle + 1).ToString() + " of " + MaxCycles.ToString() + ".....";
                            Man_Controlcs.WriteStatus("Run Program", this.lbl_CurProtocol.Text);


                            //Do the Synthesis Cycles for For Every Base
                            //Get the first line from the loop list box
                            //then process the currently loaded protocol    
                            int LoopSteps = lb_loop.Items.Count;
                            for (int i = 0; i < LoopSteps; i++)
                            {

                                lb_loop.SelectedIndex = i;
                                lb_loop.TopIndex = lb_loop.SelectedIndex;

                                iScrollIndex = i;
                                String curItem = lb_loop.SelectedItem.ToString();
                                String[] parts = curItem.Split(',');

                                SafeSetStatus("Doing Current Step - " + parts[0] + "...");

                                if (curItem.Contains("Comment") || curItem.Contains("Pump"))
                                {

                                    //Do The Commands
                                    DoCommands(curItem);

                                    //Now Wait Until Done
                                    while (globals.bWaiting)
                                        await PutTaskDelay(250);
                                }
                                else if (!parts[0].Contains("Coupling"))
                                {
                                    globals.bOxidizing = false;
                                    globals.bThiolating = false;
                                    //turn deblock monitor on
                                    if (parts[0].Contains("Deblock"))
                                    {
                                        //make sure the array is empty
                                        Array.Clear(globals.iUVTritylData, 0, globals.iUVTritylData.Length);
                                        globals.DeblockMonitor = true;

                                    }
                                    if (parts[0].Contains("Oxi"))
                                    {
                                        this.lbl_curBase.Text = "Post Coupling --  Oxidation";
                                        globals.bOxidizing = true;
                                    }
                                    if (parts[0].Contains("Thio"))
                                    {
                                        this.lbl_curBase.Text = "Post Coupling --  Thiolation";
                                        globals.bThiolating = true;
                                    }

                                    if (parts[0].Contains("Mix"))
                                    {
                                        this.lbl_curBase.Text = "Post Coupling --  Oxidation/Thiolation";
                                        globals.bOxidizing = true;
                                        //we have to be using 2 letter and running sequentially
                                        if (globals.i12Ltr == 1 && (rb_Sequenntial.Checked || globals.noOligos < 2))
                                        {
                                            sCurPart = "Thiolation...";
                                            char[] cB = new char[4];
                                            cB = curBase2Ltr.ToCharArray();
                                            if (cB.Length > 1)
                                            {
                                                if (cB[2] == '+' || cB[2] == 's')  //can be either + or s to signify do sulfurization
                                                {
                                                    parts[1] = parts[2];
                                                    globals.bOxidizing = false;
                                                    globals.bThiolating = true;
                                                }
                                            }
                                            if (parts.Length > 1)
                                                try { Debug.WriteLine("Should have loaded - " + parts[1] + "  for base -" + curBase2Ltr + " with test Char = " + char.ToString(cB[2])); } catch { }
                                        }
                                        curBase2Ltr = string.Empty;
                                    }

                                    if (parts[0].Contains("Cap"))
                                        this.lbl_curBase.Text = "Post Coupling --  Capping";

                                    //fill the top list box
                                    if (bRunProcessing && !bTerminateEndofCycle && !bTerminateEndofStep)
                                        FillTheList(parts[1]);

                                    //now do Begin and End
                                    BeginandEnd();
                                    while (!bBeginandEnd && bRunProcessing)
                                        await PutTaskDelay(1000);

                                    bBeginandEnd = false;

                                    //if in the amidite coupling step
                                    if (bTerminateEndofStep)
                                    {
                                        SafeSetStatus("Terminating at end of " + parts[0] + " Step...");
                                        Man_Controlcs.WriteStatus("Run Program", "Synthesis Cycle Terminated at end of " + parts[0] + " Step, Cycle" + globals.iCycle.ToString("0"));
                                        // first kill any timers
                                        if (RunTimer.Enabled)
                                        {
                                            RunTimer.Stop();
                                            RunTimer.Enabled = false;
                                        }
                                        bRunProcessing = false;
                                        globals.bIsRunning = false;
                                        globals.iCycle = MaxCycles + 2;
                                    }
                                }
                                else  // Coupling Protocol
                                {
                                    SafeSetStatus("Starting Coupling Step...");
                                    ProcessUVTrityl();
                                    globals.DeblockMonitor = false;
                                    globals.MonitorCntr = 0;

                                    //initialize the done array
                                    int[] done = new int[9];

                                    //now reopen it if we are monitoring
                                    if (bMonitoringUV && rb_Sequenntial.Checked)    // !WindowAlreadyOpen.WindowOpen("Trityl Bar Chart Histogram"))
                                        Menu_UV_Bar.PerformClick();
                                    else if (!bMonitoringUV && bAutoShowChart)
                                        Menu_UV_Bar.PerformClick();

                                    //get all the same bases for the current cycle into an array
                                    if (rb_Smart.Checked && rb_Parallel.Checked && globals.noOligos > 1)
                                        GetSameBases(globals.iCycle, 0);

                                    for (int sb = 0; sb < sameBase.Length; sb++)
                                        Debug.WriteLine("Same Bases Found = " + sameBase[sb]);

                                    if (globals.bUVTrityl)
                                    {
                                        //when reach sequence end turn the detector off...
                                        for (int seq = 0; seq < 7; seq++)
                                        {
                                            //turn off if sequence is finished
                                            string curSeq = GetSeq(seq, globals.iCycle);

                                            if (curSeq.Length > 0 && (curSeq.Length - 1) == globals.iCycle)
                                            {

                                                //turn the detector off
                                                switch (seq)
                                                {
                                                    case 0:
                                                        if (globals.bUV1On) { Man_Controlcs.SendControllerMsg(2, valves.UV1off); globals.bUV1On = false; }
                                                        break;
                                                    case 1:
                                                        if (globals.bUV2On) { Man_Controlcs.SendControllerMsg(2, valves.UV2off); globals.bUV2On = false; }
                                                        break;
                                                    case 2:
                                                        if (globals.bUV3On) { Man_Controlcs.SendControllerMsg(2, valves.UV3off); globals.bUV3On = false; }
                                                        break;
                                                    case 3:
                                                        if (globals.bUV4On) { Man_Controlcs.SendControllerMsg(2, valves.UV4off); globals.bUV4On = false; }
                                                        break;
                                                    case 4:
                                                        if (globals.bUV5On) { Man_Controlcs.SendControllerMsg(2, valves.UV5off); globals.bUV5On = false; }
                                                        break;
                                                    case 5:
                                                        if (globals.bUV6On) { Man_Controlcs.SendControllerMsg(2, valves.UV6off); globals.bUV6On = false; }
                                                        break;
                                                    case 6:
                                                        if (globals.bUV7On) { Man_Controlcs.SendControllerMsg(2, valves.UV7off); globals.bUV7On = false; }
                                                        break;
                                                    case 7:
                                                        if (globals.bUV8On) { Man_Controlcs.SendControllerMsg(2, valves.UV8off); globals.bUV8On = false; }
                                                        break;
                                                }
                                            }//put in else turn the cells off
                                        }

                                    }
                                    
                                    for (int d = 0; d < 9; d++)
                                    {
                                        if (runSequences[d].Length < 1 || runSequences[d].Length == globals.iCycle)
                                        { done[d] = 1; Debug.WriteLine("Empty or sequences Complete At  " + d.ToString()); }
                                    }

                                    for (int seq = 0; seq <= 7; seq++)//one at a time get the sequence
                                    {
                                        if (rb_Smart.Checked && bRunProcessing && !bTerminateEndofCycle && !bTerminateEndofStep)
                                            seq = sameBase[0] - 1;

                                        globals.bCoupling = true;

                                        //initialize with current cycle and completed cycles and empty cycles
                                        if (seq > 0)
                                        {
                                            done[seq] = 1;
                                        }
                                        for(int s=0; s < runningSeq.Length; s++)
                                        {
                                            if (runSequences[s].Length < 0)
                                                done[s] = 1;
                                            if (runSequences[s].Length < globals.iCycle)
                                                done[s] = 1;
                                        }
                                        //set the global scale factor
                                        globals.dAmiditeCF = Properties.Settings.Default.AmiditeFC;
                                        if (rb_Smart.Checked && bRunProcessing && !bTerminateEndofCycle && !bTerminateEndofStep)
                                        {
                                            if (sameBase.Length > 1) 
                                            {
                                                SetAutoScaleValue(sameBase[0]);
                                               globals.dAmiditeCF = globals.dAmiditeCF * (sameBase.Length * 0.9) * globals.scalefactor;
                                            }
                                        }

                                        else if (globals.AutoScaleAmidites)
                                        {
                                            SetAutoScaleValue(seq);
                                            globals.dAmiditeCF = globals.scalefactor * globals.dAmiditeCF;
                                        }

                                        string curSeq = GetSeq(seq, globals.iCycle);

                                        if (curSeq.Length > 0 && curSeq.Length > globals.iCycle)
                                        {
                                            // record the column
                                            globals.iCoupCol = seq + 1;
                                            string CouplingBase = string.Empty;
                                            if (rb_Smart.Checked)
                                            {
                                                CouplingBase = sameBase[0].ToString("0");
                                                if (sameBase.Length > 1)
                                                {
                                                    for (int j = 1; j < sameBase.Length; j++)
                                                    {
                                                        
                                                        CouplingBase = CouplingBase + ", " + sameBase[j].ToString("0");
                                                    }
                                                }

                                            }
                                            else
                                                CouplingBase = (seq + 1).ToString("0"); 



                                            char[] chBase = curSeq.ToCharArray();
                                            this.lbl_curBase.Text = " Coupling: Base - " + chBase[globals.iCycle] + " Column(s) - " + CouplingBase;
                                            SafeSetStatus(" Coupling: Base - " + chBase[globals.iCycle] + " Column - " + (seq + 1).ToString());

                                            //if rb_Smart not checked make only one red, else make all the same Red
                                            if (rb_Smart.Checked) 
                                            {
                                                if (sameBase.Length > 1)
                                                {
                                                    //make the color in the text box Red for the current cycle
                                                    foreach (int b in sameBase)
                                                    {
                                                        FormatTextBox(b - 1, globals.iCycle, Color.Black, Color.Red, Color.Green, false);
                                                        GetSeq(b, globals.iCycle); //just to make sure the column done flag gets set
                                                    }
                                                }
                                                else
                                                    FormatTextBox(seq, globals.iCycle, Color.Black, Color.Red, Color.Green, false);
                                            }
                                            else
                                            {
                                                //make the color in the text box Red for the current cycle
                                                FormatTextBox(seq, globals.iCycle, Color.Black, Color.Red, Color.Green, false);
                                            }

                                            //get the protocol base for the amidite
                                            string sProto = GetProtocol(chBase[globals.iCycle]);
                                            lbl_AmProtoTxt.Visible = true;
                                            lbl_AmProto.Text = sProto;

                                            //load it in the list box
                                            if(bRunProcessing && !bTerminateEndofCycle && !bTerminateEndofStep)
                                               FillTheList(sProto);

                                            //Do Begin and End 
                                            BeginandEnd();
                                            while (!bBeginandEnd && bRunProcessing)
                                                await PutTaskDelay(1000);

                                            SafeSetStatus("Coupling Complete...");
                                            bBeginandEnd = false;

                                            //if in the amidite coupling step
                                            if (bTerminateEndofStep)
                                            {
                                                SafeSetStatus("Terminating at End of Coupling Step....");
                                                Man_Controlcs.WriteStatus("Run", "Terminated at the end of the coupling step on cycle - " + globals.iCycle.ToString());
                                                // first kill any timers
                                                if (RunTimer.Enabled)
                                                {
                                                    RunTimer.Stop();
                                                    RunTimer.Enabled = false;
                                                }
                                                globals.bIsRunning = false;
                                                bRunProcessing = false;
                                                globals.iCycle = MaxCycles + 2;
                                            }

                                            //give it a second to make sure all is well
                                            Man_Controlcs.SyncWait(800);
                                            //MessageBox.Show("Protocol is :"+sProto +"\n Sequence is:"+curSeq, "Amidite is" +  chBase[globals.iCycle].ToString());

                                        }

                                        globals.bCoupling = false;
                                        SafeSetStatus("Coupling Cycle Complete...");
                                        lbl_AmProtoTxt.Visible = false;
                                        lbl_AmProto.Text = string.Empty;
                                        SafeSetStatus(lbl_curBase.Text);

                                        int iCurSeqCycle = seq;
                                        bool lastcycle = false;
                                        //build the done array
                                        if (rb_Smart.Checked && bRunProcessing && !bTerminateEndofCycle && !bTerminateEndofStep)
                                        {   
                                            Debug.WriteLine("I am writing the first element to done...the Current seq is " + seq.ToString());
                                            //store the bases done in a done array
                                            foreach (int g in sameBase)
                                            {
                                                if (g > 0)
                                                {
                                                    done[g] = 1;
                                                    Debug.WriteLine("Done - g = " + done[g].ToString() + " for column " + g.ToString());
                                                }
                                            }
                                            int tot_u = 0;

                                            //do we need to bail...are we done???
                                            for (int u = 1; u < 9; u++)
                                            {
                                                
                                                if (done[u] == 1) { Debug.WriteLine("Column Done = " + u.ToString()); tot_u++; }
                                                if (done[u] == 1 && runSequences[u].Length-1 == globals.iCycle)
                                                    //turn the LED off
                                                    CellsOff(u);
                                            }
                                            //check to see if all cycles are full 1 through 7
                                            if (done[1] == 1 && done[2] == 1 && done[3] == 1 && done[4] == 1 && done[5] == 1 && done[6] == 1 && done[7] == 1)
                                            { lastcycle = true; }

                                            if (!lastcycle) //if is is the last cycle don't worry about this...
                                            {

                                                //find first not done
                                                int first = 0;
                                                for (int d = 1; d < 7; d++)
                                                {
                                                    if (done[d] == 0)
                                                    { first = d; d = 10; }
                                                }

                                                //second check for the end
                                                if (sameBase[0] == -1 || lastcycle) { seq = 7; }

                                                //reset the array
                                                if (seq < 6)
                                                    sameBase = new int[7 - seq];

                                                //now determine the next oligo to do if sequence is 8 then just use seq in the for loop
                                                for (int b = first; b < 8; b++)
                                                {
                                                    Debug.WriteLine("evaluating " + b.ToString());
                                                    if (!lastcycle && runSequences[b].Length > 0)
                                                    {
                                                        GetSameBases(globals.iCycle, b);
                                                        Debug.WriteLine("I found " + sameBase.Length.ToString() + " of the same bases for  cycle " + seq.ToString() + " the first base is " + sameBase[0].ToString());
                                                        if (sameBase[0] > 0)
                                                            seq = sameBase[0];  //0 through 7 versus 1 through 8
                                                        else
                                                            seq = 7; //it is the last cycle

                                                        Debug.WriteLine("the first element in same bases is " + sameBase[0].ToString());
                                                        b = 9;
                                                    }

                                                }
                                            }
                                            else
                                            {
                                                if (done[8] == 1)
                                                { seq = 7; sameBase[0] = -1; }
                                                else
                                                { seq = 6; sameBase[0] = 8; }
                                            }
                                        }
                                       
                                    }
                                    for (int seq = 0; seq <= 7; seq++)//one at a time get the sequence
                                    {
                                        string curSeq = GetSeq(seq, globals.iCycle);
                                        if (curSeq.Length > 0 && curSeq.Length > globals.iCycle)
                                        {
                                            FormatTextBox(seq, globals.iCycle, Color.Black, Color.Red, Color.Green, true);
                                            saveseq = seq;
                                        }
                                    }
                                }

                            }//close the second for loop
                            bBeginandEnd = false;
                            SafeSetStatus("Cycle Complete...");
                            //           if (!bRunProcessing)
                            //             await PutTaskDelay(2000);
                            iScrollIndex = 0;
                            // i = LoopSteps + 1;

                            //check if terminated
                            if (bTerminateEndofCycle)
                            {
                                globals.iCycle = MaxCycles + 10;
                                SafeSetStatus("Terminated at end of Current Cycle");
                                string endtext = "Run Terminated at End of Cycle..." + globals.iCycle.ToString() + "  at:  " + time;
                                this.lbl_Status.Text = "Status:" + endtext;

                                Man_Controlcs.WriteStatus("Run", endtext);
                                SafeSetStatus(endtext);
                                // first kill any timers
                                if (RunTimer.Enabled)
                                {
                                    RunTimer.Stop();
                                    RunTimer.Enabled = false;
                                }
                                globals.bIsRunning = false;
                                bRunProcessing = false;
                                break;
                            }


                        } //close the cycles for loop

                        /*  if (bRunProcessing)
                          {
                              BeginandEnd();
                              while (!bBeginandEnd && bRunProcessing)
                                  await PutTaskDelay(500);
                          }*/

                        bBeginandEnd = false;
                        bLooping = false;
                        SafeSetStatus("Done with Synthesis Cycle");
                        //if in the amidite coupling step

                    }


                    // Clear the Loop list box
                    lb_loop.Items.Clear();
                    this.lbl_curBase.Text = "";
                    //Final Protocol
                    time = String.Format("{0:F}", DateTime.Now);
                    Man_Controlcs.WriteStatus("Run Program", "PostRun Started at:  " + time);
                    SafeSetStatus("Starting Post Run...");
                    //Make sure the list boxk is filled with the Post Run Protocol
                    if (bRunProcessing && !bTerminateEndofCycle && !bTerminateEndofStep)
                    {
                        FillTheList(this.lbl_Post.Text);

                        //Set Program Text to Running...
                        this.Text = "SeNA80 Run - Running the Post Run Protocol ...";
                        SafeSetStatus("Running Post Run Protocol...");
                        //Later we will put in LEDs to show the system is running
                        //Chaning the LED to run would go here

                        //Update the Label
                        this.lbl_CurProtocol.Text = "Running the Post Run Protocol.... ";
                        if (bRunProcessing)
                        {
                            //now do Begin and End
                            BeginandEnd();
                            while (!bBeginandEnd && bRunProcessing)
                                await PutTaskDelay(500);
                        }


                        bBeginandEnd = false;
                        SafeSetStatus("Post Run Complete...");
                    }
                    if (rb_Sequenntial.Checked)
                    {
                        //make the color in the text box Red
                        FormatTextBox(saveseq, -1, Color.Black, Color.Red, Color.Green, false);
                        if(bMonitoringUV)
                        {
                            if (globals.sb != null)
                            {

                                globals.sb.CloseMe();
                                Debug.WriteLine("Monitor Should've Closed");
                            }

                        }
                    }
                    else
                    {
                        for (int seq = 0; seq <= 7; seq++)//one at a time get the sequence
                            FormatTextBox(seq, -1, Color.Black, Color.Red, Color.Green, false);
                    }
                }

                sameBase = new int[8];
                dtEnd = DateTime.Now;
                TimeSpan elapsed = dtEnd.Subtract(dtStart);
                //in all cases make sure all valves are left in the off position
                CloaseAllValves();

                //leave the list box ready for the next run...
                //Make Sure the Listbox is full if not fill it...
                if (!(lbl_Start.Text.Contains("[none]")) && lbl_Start.Text.Length > 0)
                    FillTheList(lbl_Start.Text);
                else
                    FillTheList(lbl_PreRun.Text);

                //log the end time and set the status...
                String tottime = String.Format("{0:F}", elapsed.ToString());
                time = String.Format("{0:F}", DateTime.Now);
                //make sure all valves are closed by turning all valves allf
                valves.AllValvesOff();

                this.lbl_CurProtocol.Text = "Syntheis Complete.... ";
                this.lbl_Status.Text = "Status: Synthesis Completed - Oligos Finished at " + time;
            Man_Controlcs.WriteStatus("Run Program", "Run Ended at:  " + time + "  Total Run Time:   " + tottime);
        }  //end of in parallel

            globals.bIsRunning = false;
            SafeSetStatus("Synthesis Complete...");

            //reenable the menus if allowed
            if (!globals.Curr_Rights.Contains("2"))
            {
                //disable the 1 2 letter menus
                Menu_1Letter.Enabled = true;
                Menu_2Letter.Enabled = true;
            }
            iScrollIndex = 0;
            gb_RunProtos.Enabled = true;
            gb_RunSeqs.Enabled = true;
            gb_RunOpts.Enabled = true;
            EnableTBs();
            btn_Run.Enabled = true;
            btn_OK.Enabled = true;
            btn_Pause.Enabled = false;
            btn_Terminate.Enabled = false;
            bTerminateEndofCycle = false;
            bTerminateEndofStep = false;

            if (!bTerminateEndofCycle)
              this.lbl_Status.Text = "Status: Synthesis Completed -Oligos Finished at " + time;
        }
        private bool finalPSPOCheck()
        {
            string seq = string.Empty;
            bool PSfound = false;
            bool POfound = false;

            if (tb_col1.Text.Length > 0)
            {
                seq = tb_col1.Text;
                char[] chSeq = seq.ToCharArray();
                for(int i =2; i < chSeq.Length; i=i+3)
                {
                    if(chSeq[i] == '-' ){ POfound = true; }
                    else if (chSeq[i] == 's' || chSeq[i] == '+') { PSfound = true; }

                if(PSfound && POfound) { return true; }
                }
            }
            if (tb_col2.Text.Length > 0)
            {
                seq = tb_col2.Text;
                char[] chSeq = seq.ToCharArray();
                for (int i = 2; i < chSeq.Length; i = i + 3)
                {
                    if (chSeq[i] == '-') { POfound = true; }
                    else if (chSeq[i] == 's' || chSeq[i] == '+') { PSfound = true; }

                    if (PSfound && POfound) { return true; }
                }
            }
            if (tb_col3.Text.Length > 0)
            {
                seq = tb_col3.Text;
                char[] chSeq = seq.ToCharArray();
                for (int i = 2; i < chSeq.Length; i = i + 3)
                {
                    if (chSeq[i] == '-') { POfound = true; }
                    else if (chSeq[i] == 's' || chSeq[i] == '+') { PSfound = true; }

                    if (PSfound && POfound) { return true; }
                }
            }
            if (tb_col4.Text.Length > 0)
            {
                seq = tb_col4.Text;
                char[] chSeq = seq.ToCharArray();
                for (int i = 2; i < chSeq.Length; i = i + 3)
                {
                    if (chSeq[i] == '-') { POfound = true; }
                    else if (chSeq[i] == 's' || chSeq[i] == '+') { PSfound = true; }

                    if (PSfound && POfound) { return true; }
                }
            }
            if (tb_col5.Text.Length > 0)
            {
                seq = tb_col5.Text;
                char[] chSeq = seq.ToCharArray();
                for (int i = 2; i < chSeq.Length; i = i + 3)
                {
                    if (chSeq[i] == '-') { POfound = true; }
                    else if (chSeq[i] == 's' || chSeq[i] == '+') { PSfound = true; }

                    if (PSfound && POfound) { return true; }
                }
            }
            if (tb_col6.Text.Length > 0)
            {
                seq = tb_col6.Text;
                char[] chSeq = seq.ToCharArray();
                for (int i = 2; i < chSeq.Length; i = i + 3)
                {
                    if (chSeq[i] == '-') { POfound = true; }
                    else if (chSeq[i] == 's' || chSeq[i] == '+') { PSfound = true; }

                    if (PSfound && POfound) { return true; }
                }
            }
            if (tb_col7.Text.Length > 0)
            {
                seq = tb_col7.Text;
                char[] chSeq = seq.ToCharArray();
                for (int i = 2; i < chSeq.Length; i = i + 3)
                {
                    if (chSeq[i] == '-') { POfound = true; }
                    else if (chSeq[i] == 's' || chSeq[i] == '+') { PSfound = true; }

                    if (PSfound && POfound) { return true; }
                }
            }
            if (tb_col8.Text.Length > 0)
            {
                seq = tb_col8.Text;
                char[] chSeq = seq.ToCharArray();
                for (int i = 2; i < chSeq.Length; i = i + 3)
                {
                    if (chSeq[i] == '-') { POfound = true; }
                    else if (chSeq[i] == 's' || chSeq[i] == '+') { PSfound = true; }

                    if (PSfound && POfound) { return true; }
                }
            }
            if (PSfound){ bOnlyPS = true; }
            if (POfound) { bOnlyPO = true; }

            return false; //not mixed
        }
        private void Menu_Help_Click(object sender, EventArgs e)
        {
            Process.Start(globals.Help_Path);
        }

        private void Menu_ViewStatusBox_Click(object sender, EventArgs e)
        {
            if (!Menu_ViewStatusBox.Checked)
            {
                Properties.Settings.Default.View_VlvStat = 0;
                Menu_ViewStatusBox.Checked = true;
                this.Status_R.Visible = true;
                this.Pnl_Valves.Visible = false;
                Menu_ValveView.Checked = false;
                Properties.Settings.Default.Save();
            }

        }
        private int OxThioBoth(int curcycle)
        {
            bool thio = false;
            bool ox = false;
           
            //we will assume 2 letter codes since you can only have mixed protocols with 2 letter coldes
            if(tb_col1.Text.Length > 1 && (curcycle * 3) < tb_col1.Text.Length )
            {
                char[] curSeq = tb_col1.Text.ToCharArray();
                if (curSeq[(curcycle * 3) + 2] == '-') { ox = true; }
                else if (curSeq[(curcycle * 3) + 2] == '+' || curSeq[(curcycle * 3) + 2] == 's') { thio = true; }
                
                if(thio && ox) { return 0; }
            }
            if (tb_col2.Text.Length > 1 && (curcycle * 3) < tb_col2.Text.Length)
            {
                char[] curSeq = tb_col2.Text.ToCharArray();
                if (curSeq[(curcycle * 3) + 2] == '-') { ox = true; }
                else if (curSeq[(curcycle * 3) + 2] == '+' || curSeq[(curcycle * 3) + 2] == 's') { thio = true; }

                if (thio && ox) { return 0; }
            }
            if (tb_col3.Text.Length > 1 && (curcycle * 3) < tb_col3.Text.Length)
            {
                char[] curSeq = tb_col3.Text.ToCharArray();
                if (curSeq[(curcycle * 3) + 2] == '-') { ox = true; }
                else if (curSeq[(curcycle * 3) + 2] == '+' || curSeq[(curcycle * 3) + 2] == 's') { thio = true; }

                if (thio && ox) { return 0; }
            }
            if (tb_col4.Text.Length > 1 && (curcycle * 3) < tb_col4.Text.Length)
            {
                char[] curSeq = tb_col4.Text.ToCharArray();
                if (curSeq[(curcycle * 3) + 2] == '-') { ox = true; }
                else if (curSeq[(curcycle * 3) + 2] == '+' || curSeq[(curcycle * 3) + 2] == 's') { thio = true; }

                if (thio && ox) { return 0; }
            }
            if (tb_col5.Text.Length > 1 && (curcycle * 3) < tb_col5.Text.Length)
            {
                char[] curSeq = tb_col5.Text.ToCharArray();
                if (curSeq[(curcycle * 3) + 2] == '-') { ox = true; }
                else if (curSeq[(curcycle * 3) + 2] == '+' || curSeq[(curcycle * 3) + 2] == 's') { thio = true; }

                if (thio && ox) { return 0; }
            }
            if (tb_col6.Text.Length > 1 && (curcycle * 3) < tb_col6.Text.Length)
            {
                char[] curSeq = tb_col6.Text.ToCharArray();
                if (curSeq[(curcycle * 3) + 2] == '-') { ox = true; }
                else if (curSeq[(curcycle * 3) + 2] == '+' || curSeq[(curcycle * 3) + 2] == 's') { thio = true; }

                if (thio && ox) { return 0; }
            }
            if (tb_col7.Text.Length > 1 && (curcycle * 3) < tb_col7.Text.Length)
            {
                char[] curSeq = tb_col7.Text.ToCharArray();
                if (curSeq[(curcycle * 3) + 2] == '-') { ox = true; }
                else if (curSeq[(curcycle * 3) + 2] == '+' || curSeq[(curcycle * 3) + 2] == 's') { thio = true; }

                if (thio && ox) { return 0; }
            }
            if (tb_col8.Text.Length > 1 && (curcycle * 3) < tb_col8.Text.Length)
            {
                char[] curSeq = tb_col8.Text.ToCharArray();
                if (curSeq[(curcycle * 3) + 2] == '-') { ox = true; }
                else if (curSeq[(curcycle * 3) + 2] == '+' || curSeq[(curcycle * 3) + 2] == 's') { thio = true; }

                if (thio && ox) { return 0; }
            }
            if(ox && !thio) { return 1; }
            if(thio && !ox) { return 2; }

            return 0;
        }
        private string[] OnlyOxThiol(int w, string[] inList)
        {
            List<string> Protocol = inList.ToList();

            for (int i = 0; i < Protocol.Count; i++)
            {
                if (w == 1)
                {
                    if (Protocol[i].Contains("Oxidation"))
                        Protocol.RemoveAt(i);
                }
                else if (w == 0)
                {
                    if (Protocol[i].Contains("Thio"))
                        Protocol.RemoveAt(i);
                }
            }
            string[] retArray = Protocol.ToArray();

            return retArray;
        }
        private void GetSameBases(int curCycle, int startat)
        {
            int[] sb = new int[8];
            bool foundone = false;
            int curcol = 0;
            int cntr = 0;
            char curBase = ' ';

            //get the first one
            for(int ja = startat; ja < runSequences.Length; ja++)
            {
                //find the first non empty sequence and store the base 
                if(runSequences[ja].Length > 0 && curCycle < runSequences[ja].Length)
                {
                    char[] rs = new char[runSequences[ja].Length];
                    rs = runSequences[ja].ToCharArray();
                    sb[cntr] = ja;
                    curBase = rs[globals.iCycle];
                    curcol = ja;
                    ja = runSequences.Length + 10;
                    foundone = true;
                    break;
                }
            }
            //if we are at the end, just put the first element in the array and return
            if (curcol > 7)
            {
                sameBase = new int[1];
                sameBase[0] = globals.iCycle;
                return;
            }
            //didn't find a match because there were no non-empty sequences from the start 
            //for example they have done 4 and the next one is 5 and 5,6,7,8 are empty...just return with 
            //the rest empty (i.e. -1)
            if (!foundone)
            {
                sameBase = new int[1];
                sameBase[0] = -1;
                return;
            }
            //now find all the other ones that are the same 
            for(int tb = curcol+1; tb < runSequences.Length; tb++ )
            {
                //find the first non empty sequence and store the base 
                if (runSequences[tb].Length > 0 && curCycle < runSequences[tb].Length) 
                {
          
                    char[] rs = new char[runSequences[tb].Length];
                    rs = runSequences[tb].ToCharArray();
                    if (rs[globals.iCycle] == curBase)
                    {
                        cntr++;
                        sb[cntr] = tb;
                    }
                }
             }
            //last store the sb array in the sameBase array
            sameBase = new int[cntr+1];
            for (int i = 0; i < cntr+1; i++)
                sameBase[i] = sb[i];

            return;

        }
        private string[] SplitOxThiol(string [] inList)
        {
            List<string> Protocol = inList.ToList();

            for (int i= 0; i<inList.Length; i++)
            {
                if(inList[i].Contains("Mix"))
                {
                    string[] parts = inList[i].Split(',');
                    Protocol[i] =             "Oxidation Protocol           ," + parts[1];
                    Protocol.Insert(i+1, "Thiolation Protocol          ," + parts[2]);
                }
            }

            //make sure the list is now correct
            string [] retArray = Protocol.ToArray();
            for (int j = 0; j < retArray.Length; j++)
                Debug.WriteLine("Replaced and Added Ox Thiol"+retArray[j]);


            return retArray;
        }
        private void Menu_ValveView_Click(object sender, EventArgs e)
        {
            if (!Menu_ValveView.Checked)
            {
                Properties.Settings.Default.View_VlvStat = 1;
                Menu_ViewStatusBox.Checked = false;
                this.Status_R.Visible = false;
                this.Pnl_Valves.Visible = true;
                Menu_ValveView.Checked = true;
                Properties.Settings.Default.Save();
            }
        }

        private async void btn_Pause_Click(object sender, EventArgs e)
        {
            SafeSetStatus("Pausing Run ...");
            // this is the easiest, just sit on a wait

            //make sure all the valves are closed before pausing....
            while (globals.bFluidicsBusy)
            {
                await PutTaskDelay(2);
                SafeSetStatus("One Moment while I finish the current step...");
            }
            //now show the dialog, lock the thread and pause
            using (Pause_Time dbpause = new Pause_Time())
            {
                if (dbpause.ShowDialog(this) == DialogResult.OK)
                {
                   
                    //get the time
                    String sTime = dbpause.cbHowLong.SelectedItem.ToString();

                    //write status
                    Man_Controlcs.WriteStatus("Run Program", "Paused for : " + sTime + "sec");

                    SafeSetStatus("Run Program Paused for : " + sTime + "sec");
                    globals.bIsPaused = false;
                    globals.bWaiting = false;


                }
                else
                    return;
            }

        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            SafeNativeMethods.ShowMain((int)Main_Form.hmainWind, 1);
        }
        //scroll the selected item to the top...
        private void ProtoListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int p = 0;

           /*if (ProtoListView.FocusedItem == null) return;
            { if (ProtoListView.SelectedIndices.Count > 0)
                        p = ProtoListView.SelectedIndices[0];
             }
            Debug.WriteLine("Current Selected Item is " + p.ToString());

            if(p > 9)
               ProtoListView.Items[ProtoListView.Items.Count - 1].EnsureVisible();
               if (ProtoListView.Items.Count > 1 && ProtoListView != null)
               {
                   var index = ProtoListView.FocusedItem.Index;
                   if (index > 1)
                       ProtoListView.EnsureVisible(index);
               }*/
        }
        public string tempstr = string.Empty;
        public int tempcycles = 0;
        private void tb_col1_Enter(object sender, EventArgs e)
        {
            //wait for text box to fill
            Man_Controlcs.SyncWait(30);
            tempstr = tb_col1.Text;
            if (tempstr.Length > 0)
                tempcycles = int.Parse(lbl_Length1.Text);
        }
        private void tb_col1_TextChanged(object sender, EventArgs e)
        {
            if (globals.i12Ltr == 0)
                lbl_Length1.Text = tb_col1.Text.Length.ToString();
            else
                lbl_Length1.Text = (tb_col1.Text.Length / 3).ToString();
            tb_col1.ForeColor = Color.Black;
        } 
        private void tb_col1_Leave(object sender, EventArgs e)
        {
            //you changed or added text 
            if(string.Compare(tb_col1.Text, tempstr) != 0)
            {
                globals.bCol1 = true;
                globals.bSeqeuncesLoaded = true;

                if (globals.i12Ltr > 0)
                {
                    if (CheckForPlus(tb_col1.Text) && !noSequential)
                    {
                        MixPSPO = true;
                        if (rb_Parallel.Checked && globals.bSeqeuncesLoaded)
                        {
                            if (MessageBox.Show("You have Mixed Phosphotioate and Phosphate backbone Oligos in your Sequences\n\n" +
                                "Would you like to continue in Parallel?\n\nPress No to Switch to Sequential",
                                "In Parallel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                rb_Sequenntial.Checked = true;
                                rb_Parallel.Checked = false;
                            }
                        }
                    }
                }
                            
                
                if (tb_col1.Text.Length < MaxCycles)
                    CheckCycles();
                else
                    MaxCycles = tb_col1.Text.Length;

                if (globals.bUVTrityl)
                    Sensor_Config.LightsOn(1);

                //store current cycle in array
                if (globals.bCol1)
                {
                    if (globals.i12Ltr == 0)
                        runSequences[1] = tb_col1.Text;
                    else
                        runSequences[1] = ConvertSeq(tb_col1.Text, 0);

                }
                else
                    runSequences[1] = string.Empty;

                //if protocols are loaded too
                if (globals.bSeqeuncesLoaded && globals.bProtocolsLoaded)
                    btn_Run.Enabled = true;

                Man_Controlcs.WriteStatus("Run", "Col 1 **MANUALLY** Loaded/Changed -" + tb_col1.Text);
            }
            tempstr = string.Empty;
            tempcycles = 0;
        }
        private void tb_col2_Enter(object sender, EventArgs e)
        {
            //wait for text box to fill
            Man_Controlcs.SyncWait(30);
            tempstr = tb_col2.Text;
            if (tempstr.Length > 0)
                tempcycles = int.Parse(lbl_Length2.Text);
        }
        private void tb_col2_TextChanged(object sender, EventArgs e)
        {
            if (globals.i12Ltr == 0)
                lbl_Length2.Text = tb_col2.Text.Length.ToString();
            else
                lbl_Length2.Text = (tb_col2.Text.Length / 3).ToString();
            tb_col2.ForeColor = Color.Black;

        }
        private void tb_col2_Leave(object sender, EventArgs e)
        {
            //you changed or added text 
            if (string.Compare(tb_col2.Text, tempstr) != 0)
            {
                globals.bCol2 = true;
                globals.bSeqeuncesLoaded = true;

                if (globals.i12Ltr > 0)
                {
                    if (CheckForPlus(tb_col2.Text) && !noSequential)
                    {
                        MixPSPO = true;
                        if (rb_Parallel.Checked && globals.bSeqeuncesLoaded)
                        {
                            if (MessageBox.Show("You have Mixed Phosphotioate and Phosphate backbone Oligos in your Sequences\n\n" +
                                "Would you like to continue in Parallel?\n\nPress No to Switch to Sequential",
                                "In Parallel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                rb_Sequenntial.Checked = true;
                                rb_Parallel.Checked = false;
                            }
                        }
                    }
                }

                if (tb_col2.Text.Length < MaxCycles)
                    CheckCycles();
                else
                    MaxCycles = tb_col2.Text.Length;
                
                if (globals.bUVTrityl)
                    Sensor_Config.LightsOn(2);

                //store current cycle in array
                if (globals.bCol2)
                {
                    if (globals.i12Ltr == 0)
                        runSequences[2] = tb_col2.Text;
                    else
                        runSequences[2] = ConvertSeq(tb_col2.Text, 0);

                }
                else
                    runSequences[2] = string.Empty;

                //if protocols are loaded too
                if (globals.bSeqeuncesLoaded && globals.bProtocolsLoaded)
                    btn_Run.Enabled = true;

                Man_Controlcs.WriteStatus("Run", "Col 2 **MANUALLY** Loaded/Changed -" + tb_col2.Text);
            }
            tempstr = string.Empty;
            tempcycles = 0;
        }
        private void tb_col3_Enter(object sender, EventArgs e)
        {
            //wait for text box to fill
            Man_Controlcs.SyncWait(30);
            tempstr = tb_col3.Text;
            if (tempstr.Length > 0)
                tempcycles = int.Parse(lbl_Length3.Text);
        }
        private void tb_col3_TextChanged(object sender, EventArgs e)
        {
            if (globals.i12Ltr == 0)
                lbl_Length3.Text = tb_col3.Text.Length.ToString();
            else
                lbl_Length3.Text = (tb_col3.Text.Length / 3).ToString();
            tb_col3.ForeColor = Color.Black;
        }
        private void tb_col3_Leave(object sender, EventArgs e)
        {
            //you changed or added text 
            if (string.Compare(tb_col3.Text, tempstr) != 0)
            {
                globals.bCol3 = true;
                globals.bSeqeuncesLoaded = true;

                if (globals.i12Ltr > 0)
                {
                    if (CheckForPlus(tb_col3.Text) && !noSequential)
                    {
                        MixPSPO = true;
                        if (rb_Parallel.Checked && globals.bSeqeuncesLoaded)
                        {
                            if (MessageBox.Show("You have Mixed Phosphotioate and Phosphate backbone Oligos in your Sequences\n\n" +
                                "Would you like to continue in Parallel?\n\nPress No to Switch to Sequential",
                                "In Parallel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                rb_Sequenntial.Checked = true;
                                rb_Parallel.Checked = false;
                            }
                        }
                    }
                }

                if (globals.bUVTrityl)
                    Sensor_Config.LightsOn(3);

                if (tb_col3.Text.Length < MaxCycles)
                    CheckCycles();
                else
                    MaxCycles = tb_col3.Text.Length;

                //store current cycle in array
                if (globals.bCol3)
                {
                    if (globals.i12Ltr == 0)
                        runSequences[3] = tb_col3.Text;
                    else
                        runSequences[3] = ConvertSeq(tb_col3.Text, 0);

                }
                else
                    runSequences[3] = string.Empty;

                //if protocols are loaded too
                if (globals.bSeqeuncesLoaded && globals.bProtocolsLoaded)
                    btn_Run.Enabled = true;

                Man_Controlcs.WriteStatus("Run", "Col 3 **MANUALLY** Loaded/Changed -" + tb_col3.Text);
            }
            tempstr = string.Empty;
            tempcycles = 0;
        }
        private void tb_col4_Enter(object sender, EventArgs e)
        {
            //wait for text box to fill
            Man_Controlcs.SyncWait(30);
            tempstr = tb_col4.Text;
            if (tempstr.Length > 0)
                tempcycles = int.Parse(lbl_Length4.Text);
        }
        private void tb_col4_TextChanged(object sender, EventArgs e)
        {
            if (globals.i12Ltr == 0)
                lbl_Length4.Text = tb_col4.Text.Length.ToString();
            else
                lbl_Length4.Text = (tb_col4.Text.Length / 3).ToString();
            tb_col4.ForeColor = Color.Black;
        }
        private void tb_col4_Leave(object sender, EventArgs e)
        {
            //you changed or added text 
            if (string.Compare(tb_col4.Text, tempstr) != 0)
            {
                globals.bCol4 = true;
                globals.bSeqeuncesLoaded = true;

                if (globals.i12Ltr > 0)
                {
                    if (CheckForPlus(tb_col4.Text) && !noSequential)
                    {
                        MixPSPO = true;
                        if (rb_Parallel.Checked && globals.bSeqeuncesLoaded)
                        {
                            if (MessageBox.Show("You have Mixed Phosphotioate and Phosphate backbone Oligos in your Sequences\n\n" +
                                "Would you like to continue in Parallel?\n\nPress No to Switch to Sequential",
                                "In Parallel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                rb_Sequenntial.Checked = true;
                                rb_Parallel.Checked = false;
                            }
                        }
                    }
                }

                if (tb_col4.Text.Length < MaxCycles)
                    CheckCycles();
                else
                    MaxCycles = tb_col4.Text.Length;

                //store current cycle in array
                if (globals.bCol4)
                {
                    if (globals.i12Ltr == 0)
                        runSequences[4] = tb_col4.Text;
                    else
                        runSequences[4] = ConvertSeq(tb_col4.Text, 0);

                }
                else
                    runSequences[4] = string.Empty;


                if (globals.bUVTrityl)
                    Sensor_Config.LightsOn(4);

                //if protocols are loaded too
                if (globals.bSeqeuncesLoaded && globals.bProtocolsLoaded)
                    btn_Run.Enabled = true;

                Man_Controlcs.WriteStatus("Run", "Col 4 **MANUALLY** Loaded/Changed -" + tb_col4.Text);
            }
            tempstr = string.Empty;
            tempcycles = 0;
        }
        private void tb_col5_Enter(object sender, EventArgs e)
        {
            //wait for text box to fill
            Man_Controlcs.SyncWait(30);
            tempstr = tb_col5.Text;
            if (tempstr.Length > 0)
                tempcycles = int.Parse(lbl_Length5.Text);
        }
        private void tb_col5_TextChanged(object sender, EventArgs e)
        {
            if (globals.i12Ltr == 0)
                lbl_Length5.Text = tb_col5.Text.Length.ToString();
            else
                lbl_Length5.Text = (tb_col5.Text.Length / 3).ToString();
            tb_col5.ForeColor = Color.Black;
        }
        private void tb_col5_Leave(object sender, EventArgs e)
        {
            //you changed or added text 
            if (string.Compare(tb_col5.Text, tempstr) != 0)
            {

                globals.bCol5 = true;
                globals.bSeqeuncesLoaded = true;

                if (globals.i12Ltr > 0)
                {
                    if (CheckForPlus(tb_col5.Text) && !noSequential)
                    {
                        MixPSPO = true;
                        if (rb_Parallel.Checked && globals.bSeqeuncesLoaded)
                        {
                            if (MessageBox.Show("You have Mixed Phosphotioate and Phosphate backbone Oligos in your Sequences\n\n" +
                                "Would you like to continue in Parallel?\n\nPress No to Switch to Sequential",
                                "In Parallel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                rb_Sequenntial.Checked = true;
                                rb_Parallel.Checked = false;
                            }
                        }
                    }
                }

                if (tb_col5.Text.Length < MaxCycles)
                    CheckCycles();
                else
                    MaxCycles = tb_col5.Text.Length;

                if (globals.bUVTrityl)
                    Sensor_Config.LightsOn(5);

                //store current cycle in array
                if (globals.bCol5)
                {
                    if (globals.i12Ltr == 0)
                        runSequences[5] = tb_col5.Text;
                    else
                        runSequences[5] = ConvertSeq(tb_col5.Text, 0);

                }
                else
                    runSequences[5] = string.Empty;

                //if protocols are loaded too
                if (globals.bSeqeuncesLoaded && globals.bProtocolsLoaded)
                    btn_Run.Enabled = true;

                Man_Controlcs.WriteStatus("Run", "Col 5 **MANUALLY** Loaded/Changed -" + tb_col5.Text);
            }
            tempstr = string.Empty;
            tempcycles = 0;
        }
        private void tb_col6_Enter(object sender, EventArgs e)
        {
            //wait for text box to fill
            Man_Controlcs.SyncWait(30);
            tempstr = tb_col6.Text;
            if (tempstr.Length > 0)
                tempcycles = int.Parse(lbl_Length6.Text);
        }
        private void tb_col6_TextChanged(object sender, EventArgs e)
        {
            if (globals.i12Ltr == 0)
                lbl_Length6.Text = tb_col6.Text.Length.ToString();
            else
                lbl_Length6.Text = (tb_col6.Text.Length / 3).ToString();
            tb_col6.ForeColor = Color.Black;
        }
        private void tb_col6_Leave(object sender, EventArgs e)
        {
            //you changed or added text 
            if (string.Compare(tb_col6.Text, tempstr) != 0)
            {
                globals.bCol6 = true;
                globals.bSeqeuncesLoaded = true;

                if (globals.i12Ltr > 0)
                {
                    if (CheckForPlus(tb_col6.Text) && !noSequential)
                    {
                        MixPSPO = true;
                        if (rb_Parallel.Checked && globals.bSeqeuncesLoaded)
                        {
                            if (MessageBox.Show("You have Mixed Phosphotioate and Phosphate backbone Oligos in your Sequences\n\n" +
                                "Would you like to continue in Parallel?\n\nPress No to Switch to Sequential",
                                "In Parallel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                rb_Sequenntial.Checked = true;
                                rb_Parallel.Checked = false;
                            }
                        }
                    }
                }

                if (tb_col6.Text.Length < MaxCycles)
                    CheckCycles();
                else
                    MaxCycles = tb_col6.Text.Length;

                //store current cycle in array
                if (globals.bCol6)
                {
                    if (globals.i12Ltr == 0)
                        runSequences[6] = tb_col6.Text;
                    else
                        runSequences[6] = ConvertSeq(tb_col6.Text, 0);

                }
                else
                    runSequences[6] = string.Empty;

                if (globals.bUVTrityl)
                    Sensor_Config.LightsOn(6);

                //if protocols are loaded too
                if (globals.bSeqeuncesLoaded && globals.bProtocolsLoaded)
                    btn_Run.Enabled = true;

                Man_Controlcs.WriteStatus("Run", "Col 6 **MANUALLY** Loaded/Changed -" + tb_col6.Text);
            }
            tempstr = string.Empty;
            tempcycles = 0;
        }
        private void tb_col7_Enter(object sender, EventArgs e)
        {
            //wait for text box to fill
            Man_Controlcs.SyncWait(30);
            tempstr = tb_col7.Text;
            if(tempstr.Length > 0)
               tempcycles = int.Parse(lbl_Length7.Text);
        }
        private void SwitchCodes(int to)
        {
            //first flip the global variable
            globals.i12Ltr = to;

            //update the Program Config
            Properties.Settings.Default.Ltr_12 = to;
            Properties.Settings.Default.Save();

            //make sure the Config File is current for all codes
            Man_Controlcs.UpdatePropConfigLetterCodes(ibases, bases);

            //now convert the loaded sequences
            if (globals.bCol1)
                Man_Controlcs.SeqConvertLetterCodesRTB(tb_col1, to, ibases, bases);
            if (globals.bCol2)
                Man_Controlcs.SeqConvertLetterCodesRTB(tb_col2, to, ibases, bases);
            if (globals.bCol3)
                Man_Controlcs.SeqConvertLetterCodesRTB(tb_col3, to, ibases, bases);
            if (globals.bCol4)
                Man_Controlcs.SeqConvertLetterCodesRTB(tb_col4, to, ibases, bases);
            if (globals.bCol5)
                Man_Controlcs.SeqConvertLetterCodesRTB(tb_col5, to, ibases, bases);
            if (globals.bCol6)
                Man_Controlcs.SeqConvertLetterCodesRTB(tb_col6, to, ibases, bases);
            if (globals.bCol7)
                Man_Controlcs.SeqConvertLetterCodesRTB(tb_col7, to, ibases, bases);
            if (globals.bCol8)
                Man_Controlcs.SeqConvertLetterCodesRTB(tb_col8, to, ibases, bases);
        }
        private void UpdateButtonLabels()
        {
            if (Properties.Settings.Default.Am_1_lbl.Length > 0)
                rb_Am1.Text = Properties.Settings.Default.Am_1_lbl;
            else
                rb_Am1.Text = "1";
            if (Properties.Settings.Default.Am_2_lbl.Length > 0)
                rb_Am2.Text = Properties.Settings.Default.Am_2_lbl;
            else
                rb_Am2.Text = "2";
            if (Properties.Settings.Default.Am_3_lbl.Length > 0)
                rb_Am3.Text = Properties.Settings.Default.Am_3_lbl;
            else
                rb_Am3.Text = "3";
            if (Properties.Settings.Default.Am_4_lbl.Length > 0)
                rb_Am4.Text = Properties.Settings.Default.Am_4_lbl;
            else
                rb_Am4.Text = "4";
            if (Properties.Settings.Default.Am_5_lbl.Length > 0)
                rb_Am5.Text = Properties.Settings.Default.Am_5_lbl;
            else
                rb_Am5.Text = "5";
            if (Properties.Settings.Default.Am_6_lbl.Length > 0)
                rb_Am6.Text = Properties.Settings.Default.Am_6_lbl;
            else
                rb_Am6.Text = "6";
            if (Properties.Settings.Default.Am_7_lbl.Length > 0)
                rb_Am7.Text = Properties.Settings.Default.Am_7_lbl;
            else
                rb_Am7.Text = "7";
            if (Properties.Settings.Default.Am_8_lbl.Length > 0)
                rb_Am8.Text = Properties.Settings.Default.Am_8_lbl;
            else
                rb_Am8.Text = "8";
            if (Properties.Settings.Default.Am_9_lbl.Length > 0)
                rb_Am9.Text = Properties.Settings.Default.Am_9_lbl;
            else
                rb_Am9.Text = "9";
            if (Properties.Settings.Default.Am_10_lbl.Length > 0)
                rb_Am10.Text = Properties.Settings.Default.Am_10_lbl;
            else
                rb_Am10.Text = "10";
            if (Properties.Settings.Default.Am_11_lbl.Length > 0)
                rb_Am11.Text = Properties.Settings.Default.Am_11_lbl;
            else
                rb_Am11.Text = "11";
            if (Properties.Settings.Default.Am_12_lbl.Length > 0)
                rb_Am12.Text = Properties.Settings.Default.Am_12_lbl;
            else
                rb_Am12.Text = "12";
            if (Properties.Settings.Default.Am_13_lbl.Length > 0)
                rb_Am13.Text = Properties.Settings.Default.Am_13_lbl;
            else
                rb_Am13.Text = "13";
            if (Properties.Settings.Default.Am_14_lbl.Length > 0)
                rb_Am14.Text = Properties.Settings.Default.Am_14_lbl;
            else
                rb_Am14.Text = "14";
        }

        private void Menu_1Letter_Click(object sender, EventArgs e)
        {
            Menu_1Letter.Checked = true;
            Menu_2Letter.Checked = false;
            
            SwitchCodes(0);
            UpdateButtonLabels();
        }
        private bool CheckForPlus(string inSequence)
        {
            //first check string length, make sure that we have the right number of characters
            if (inSequence.Length % 3 == 0)
            {
                if (inSequence.Contains("+") || CheckForS(inSequence))
                    return true;
                else
                    return false;
            }
            else
                MessageBox.Show("Sequence not in proper format\nPlease check to make sure every base has 3 letters\nespecially the last base", "Incorrect Format", MessageBoxButtons.OK, MessageBoxIcon.Hand);

            return false;
        }
        private bool CheckForS(string inSequence)
        {
            char[] cArray = inSequence.ToCharArray();
            for (int i = 2; i < cArray.Length; i  = i+3)
            {
                if (cArray[i] == 's')
                    return true;
            }
            return false;
        }

        private void Menu_2Letter_Click(object sender, EventArgs e)
        {
            Menu_1Letter.Checked = false;
            Menu_2Letter.Checked = true;
            SwitchCodes(1);
            UpdateButtonLabels();
        }

        private void tb_col7_TextChanged(object sender, EventArgs e)
        {
            if (globals.i12Ltr == 0)
                lbl_Length7.Text = tb_col7.Text.Length.ToString();
            else
                lbl_Length7.Text = (tb_col7.Text.Length / 3).ToString();
            tb_col7.ForeColor = Color.Black;
        }

        private void tb_col7_Leave(object sender, EventArgs e)
        {
            //you changed or added text 
            if (string.Compare(tb_col7.Text, tempstr) != 0)
            {
                globals.bCol7 = true;
                globals.bSeqeuncesLoaded = true;

                if (globals.i12Ltr > 0)
                {
                    if (CheckForPlus(tb_col7.Text) && !noSequential)
                    {
                        MixPSPO = true;
                        if (rb_Parallel.Checked && globals.bSeqeuncesLoaded)
                        {
                            if (MessageBox.Show("You have Mixed Phosphotioate and Phosphate backbone Oligos in your Sequences\n\n" +
                                "Would you like to continue in Parallel?\n\nPress No to Switch to Sequential",
                                "In Parallel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                rb_Sequenntial.Checked = true;
                                rb_Parallel.Checked = false;
                            }
                        }
                    }
                }
                if (tb_col7.Text.Length < MaxCycles)
                    CheckCycles();
                else
                    MaxCycles = tb_col7.Text.Length;

                //store current cycle in array
                if (globals.bCol7)
                {
                    if (globals.i12Ltr == 0)
                        runSequences[7] = tb_col7.Text;
                    else
                        runSequences[7] = ConvertSeq(tb_col7.Text, 0);

                }
                else
                    runSequences[7] = string.Empty;


                if (globals.bUVTrityl)
                    Sensor_Config.LightsOn(7);

                //if protocols are loaded too
                if (globals.bSeqeuncesLoaded && globals.bProtocolsLoaded)
                    btn_Run.Enabled = true;

                Man_Controlcs.WriteStatus("Run", "Col 7 **MANUALLY** Loaded/Changed -" + tb_col7.Text);
            }
            tempstr = string.Empty;
            tempcycles = 0;
        }

        private void gb_Len_Enter(object sender, EventArgs e)
        {

        }
        
      
        private void btn_Consumption_Click(object sender, EventArgs e)
        {

            //determine average scale
            int avgCntr = 0;
            string selected = string.Empty;
            globals.avgScale = 0;
            globals.noOligos = 0;
            for (int a = 0; a < 8; a++)
            {
                  switch (a)
                  {
                        case 0:
                        if (globals.bCol1)
                        {
                            selected = cb_Col1Scale.Text;
                            globals.avgScale += double.Parse(selected);
                            avgCntr++;
                        }
                         break;
                        case 1:
                        if (globals.bCol2)
                        {
                            selected = cb_Col2Scale.Text;
                            globals.avgScale += double.Parse(selected);
                            avgCntr++;
                        }
                        break;
                        case 2:
                        if (globals.bCol3)
                        {
                            selected = cb_Col3Scale.Text;
                            globals.avgScale += double.Parse(selected);
                            avgCntr++;
                        }
                        break;
                        case 3:
                        if (globals.bCol4)
                        {
                            selected = cb_Col4Scale.Text;
                            globals.avgScale += double.Parse(selected);
                            avgCntr++;
                        }
                        break;
                        case 4:
                        if (globals.bCol5)
                        {
                            selected = cb_Col5Scale.Text;
                            globals.avgScale += double.Parse(selected);
                            avgCntr++;
                        }
                        break;
                        case 5:
                        if (globals.bCol6)
                        {
                            selected = cb_Col6Scale.Text;
                            globals.avgScale += double.Parse(selected);
                            avgCntr++;
                        }
                        break;
                        case 6:
                        if (globals.bCol7)
                        {
                            selected = cb_Col7Scale.Text;
                            globals.avgScale += double.Parse(selected);
                            avgCntr++;
                        }
                        break;
                        case 7:
                        if (globals.bCol8)
                        {
                            selected = cb_Col8Scale.Text;
                            globals.avgScale += double.Parse(selected);
                            avgCntr++;
                        }
                        break;

                    }
                }
            
            globals.avgScale = globals.avgScale / avgCntr;
            globals.noOligos = avgCntr;
            Debug.WriteLine("Average Scale - " + globals.avgScale + " Number of Oligos - " + globals.noOligos);

            if (globals.bSeqeuncesLoaded && globals.bProtocolsLoaded)
            {
                ConsumptionCalc cc = new ConsumptionCalc();
                

                cc.ShowDialog();


                // Show testDialog as a modal dialog and determine if DialogResult = OK.
                if (cc.DialogResult == DialogResult.OK)
                {
                    // just fall through and dispose the box.
                    //relView.Dispose();

                }
            }
            else
            {
                MessageBox.Show("Need both Protocols and Sequences\nTo Calculate Reagent Consumption", "Not Loaded", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

        }
        
        private void rb_Parallel_CheckedChanged(object sender, EventArgs e)
        {
            if(rb_Parallel.Checked)
            {
                inParallel = true;
                lbl_Start.Enabled = false;
                lbl_4.Enabled = false;
                rb_Parallel.Checked = true;
                rb_Sequenntial.Checked = false;
                rb_Smart.Enabled = true;
                gb_AmidDelivery.Enabled = true;
            }
        }

        private void rb_Sequenntial_CheckedChanged(object sender, EventArgs e)
        {
            if(rb_Sequenntial.Checked)
            {
                inParallel = false;
                lbl_Start.Enabled = true;
                lbl_4.Enabled = true;
                rb_Parallel.Checked = false;
                rb_Sequenntial.Checked = true;
                rb_Smart.Enabled = false;
                gb_AmidDelivery.Enabled = false;
            }
        }

        private void Menu_Autostart_Click(object sender, EventArgs e)
        {
            if(Menu_Autostart.Checked)
            {
                Menu_Autostart.Checked = false;
                bAutoShowChart = false;
            }
            else
            {
                Menu_Autostart.Checked = false;
                bAutoShowChart = true ;
            }
        }

        
        private void tb_col8_Enter(object sender, EventArgs e)
        {
            //wait for text box to fill
            Man_Controlcs.SyncWait(30);
            tempstr = tb_col8.Text;
            if (tempstr.Length > 0)
                tempcycles = int.Parse(lbl_Length8.Text);
        }
        private void tb_col8_TextChanged(object sender, EventArgs e)
        {
            if (globals.i12Ltr == 0)
                lbl_Length8.Text = tb_col8.Text.Length.ToString();
            else
                lbl_Length8.Text = (tb_col8.Text.Length / 3).ToString();
            tb_col8.ForeColor = Color.Black;
        }
        private void tb_col8_Leave(object sender, EventArgs e)
        {
            //you changed or added text 
            if (string.Compare(tb_col8.Text, tempstr) != 0)
            {
                globals.bCol8 = true;
                globals.bSeqeuncesLoaded = true;

                if (tb_col8.Text.Length < MaxCycles)
                    CheckCycles();
                else
                    MaxCycles = tb_col8.Text.Length;

                if (globals.i12Ltr > 0)
                {
                    if (CheckForPlus(tb_col8.Text) && !noSequential)
                    {
                        MixPSPO = true;
                        if (rb_Parallel.Checked && globals.bSeqeuncesLoaded)
                        {
                            if (MessageBox.Show("You have Mixed Phosphotioate and Phosphate backbone Oligos in your Sequences\n\n" +
                                "Would you like to continue in Parallel?\n\nPress No to Switch to Sequential",
                                "In Parallel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                rb_Sequenntial.Checked = true;
                                rb_Parallel.Checked = false;
                            }
                        }
                    }
                }

                //store current cycle in array
                if (globals.bCol8)
                {
                    if (globals.i12Ltr == 0)
                        runSequences[8] = tb_col8.Text;
                    else
                        runSequences[8] = ConvertSeq(tb_col8.Text, 0);

                }
                else
                    runSequences[8] = string.Empty;

                if (globals.bUVTrityl)
                    Sensor_Config.LightsOn(8);

                //if protocols are loaded too
                if (globals.bSeqeuncesLoaded && globals.bProtocolsLoaded)
                    btn_Run.Enabled = true;

                Man_Controlcs.WriteStatus("Run", "Col 8 **MANUALLY** Loaded/Changed -" + tb_col8.Text);
            }
            tempstr = string.Empty;
            tempcycles = 0;
        }
        private void CheckCycles()
        {
            MaxCycles = 0;

            if (globals.bCol1)
            {
                if (globals.i12Ltr == 0)
                {
                    if (tb_col1.Text.Length > MaxCycles)
                        MaxCycles = tb_col1.Text.Length;
                }
                else
                {
                    if (((double)tb_col1.Text.Length/3) > MaxCycles)
                        MaxCycles = Convert.ToInt16((double)tb_col1.Text.Length/3);
                }
            }
            else if (globals.bCol2)
            {
                if (globals.i12Ltr == 0)
                {
                    if (tb_col2.Text.Length > MaxCycles)
                        MaxCycles = tb_col2.Text.Length;
                }
                else
                {
                    if (((double)tb_col2.Text.Length / 3) > MaxCycles)
                        MaxCycles = Convert.ToInt16((double)tb_col2.Text.Length/3);
                }
            }
            else if (globals.bCol3)
            {
                if (globals.i12Ltr == 0)
                {
                    if (tb_col3.Text.Length > MaxCycles)
                        MaxCycles = tb_col3.Text.Length;
                }
                else
                {
                    if (((double)tb_col3.Text.Length / 3) > MaxCycles)
                        MaxCycles = Convert.ToInt16((double)tb_col3.Text.Length/3);
                }
            }
            else if (globals.bCol4)
            {
                if (globals.i12Ltr == 0)
                {
                    if (tb_col4.Text.Length > MaxCycles)
                        MaxCycles = tb_col4.Text.Length;
                }
                else
                {
                    if (((double)tb_col4.Text.Length / 3) > MaxCycles)
                        MaxCycles = Convert.ToInt16((double)tb_col4.Text.Length / 3);
                }
            }
            else if (globals.bCol5)
            {
                if (globals.i12Ltr == 0)
                {
                    if (tb_col5.Text.Length > MaxCycles)
                        MaxCycles = tb_col5.Text.Length;
                }
                else
                {
                    if (((double)tb_col5.Text.Length / 3) > MaxCycles)
                        MaxCycles = Convert.ToInt16((double)tb_col5.Text.Length / 3);
                }
            }
            else if (globals.bCol6)
            {
                if (globals.i12Ltr == 0)
                {
                    if (tb_col6.Text.Length > MaxCycles)
                        MaxCycles = tb_col6.Text.Length;
                }
                else
                {
                    if (((double)tb_col6.Text.Length / 3) > MaxCycles)
                        MaxCycles = Convert.ToInt16((double)tb_col6.Text.Length / 3);
                }
            }
            else if (globals.bCol7)
            {
                if (globals.i12Ltr == 0)
                {
                    if (tb_col7.Text.Length > MaxCycles)
                        MaxCycles = tb_col7.Text.Length;
                }
                else
                {
                    if (((double)tb_col7.Text.Length / 3) > MaxCycles)
                        MaxCycles = Convert.ToInt16((double)tb_col7.Text.Length / 3);
                }
            }
            else if (globals.bCol8)
            {
                if (globals.i12Ltr == 0)
                {
                    if (tb_col8.Text.Length > MaxCycles)
                        MaxCycles = tb_col8.Text.Length;
                }
                else
                {
                    if (((double)tb_col8.Text.Length / 3) > MaxCycles)
                        MaxCycles = Convert.ToInt16((double)tb_col8.Text.Length / 3);
                }
            }
            Debug.WriteLine("MaxCycles is - " + MaxCycles.ToString("0"));
        }

        private void ss_Run_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        //change the color of the letter for the base being synthesized
        private void FormatTextBox(int seq, int cycle,
            Color todoColor, Color curColor, Color doneColor, bool done)
        {
            System.Windows.Forms.RichTextBox rb = tb_col1;

            //determine which text box
            switch(seq)
            {
                case 0:
                    rb = tb_col1;
                    break;
                case 1:
                    rb = tb_col2;
                    break;
                case 2:
                    rb = tb_col3;
                    break;
                case 3:
                    rb = tb_col4;
                    break;
                case 4:
                    rb = tb_col5;
                    break;
                case 5:
                    rb = tb_col6;
                    break;
                case 6:
                    rb = tb_col7;
                    break;
                case 7:
                    rb = tb_col8;
                    break;
               
            }

            if(cycle == -1) //means the sequence is complete
            {
                string doneSeq = rb.Text;
                rb.Clear();
                rb.SelectionFont = new Font("Arial", 11, FontStyle.Bold | FontStyle.Italic);
                rb.SelectionColor = Color.FromArgb(102,0,102);
                doneSeq = "*****" + doneSeq + "*****";
                rb.AppendText(doneSeq);
                rb.SelectionStart = rb.Text.Length;
                rb.ScrollToCaret();
                Debug.WriteLine("Done = " + doneSeq);
                return;

            }

            string Sequence = rb.Text;
            rb.Text = "";
            char[] chBase = Sequence.ToCharArray();
            string chX = new string(chBase);
            Debug.WriteLine("Sequence is - " + Sequence + "         Array of Characters is - " + chX);

            char[] chFront = new char[400];
            char[] chBack = new char[400];
            try
            {
                if (globals.i12Ltr == 0)
                {
                    if (cycle == 0)
                        for (int i = 1; i < Sequence.Length; i++)
                            chBack[i - 1] = chBase[i];
                    else
                    {
                        for (int i = 0; i < cycle; i++)
                            chFront[i] = chBase[i];
                        for (int i = (cycle + 1); i < Sequence.Length; i++)
                            chBack[i - cycle - 1] = chBase[i];
                    }
                }
                else
                {
                    if (cycle == 0)
                        for (int i = 3; i < Sequence.Length; i++)
                            chBack[i - 3] = chBase[i];
                    else
                    {
                        for (int i = 0; i < (cycle * 3); i++)
                            chFront[i] = chBase[i];
                        for (int i = ((cycle + 1) * 3); i < Sequence.Length; i++)
                        {
                            chBack[i - ((cycle + 1) * 3)] = chBase[i];
                        }
                    }
                }
            }
            catch (Exception e) { Man_Controlcs.WriteStatus("Run", "Could not Format Sequence String "+e.ToString()); return; }
            

            chFront[cycle] += '\0';
            string cF = new string(chFront);
            chBack[Sequence.Length] = '\0';
            string cB = new string(chBack);
            string cS = new string(chBase);
            //MessageBox.Show("Sequence - " + cS.ToString() + "\n Front = " + cF.ToString() + "\n Back =  " + cB.ToString() + "\nSequence Length = " + Sequence.Length.ToString(), "Here -  Base"+cB.ToString());
            Debug.WriteLine("Sequence" + cS + "   Front = " + cF + "   Back = " + cB + "   Length = " + Sequence.Length.ToString());
;         
            char[] b = new char[5];
            if (globals.i12Ltr == 0)
            {
                b[0] = chBase[cycle];
                b[1] += '\0';
            }
            else
            {
                b[0] = chBase[(cycle * 3)];
                b[1] = chBase[(cycle * 3) + 1];
                b[2] = chBase[(cycle * 3) + 2];
                b[3] += '\0';
            }
            Debug.WriteLine("Current Base = " + b.ToString());
            
;
            string bs = new string(b);
            //MessageBox.Show("Base" + bs);

            string front = new string(chFront);
            //MessageBox.Show("Front - " + front);
            string back = new string(chBack);
            

            //MessageBox.Show("\nFront - " + front + "\nBase - " + chBase[cycle].ToString() + "\nBack - " + back);

            rb.SelectionFont = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            rb.SelectionColor = doneColor;
            rb.AppendText(front);
            Debug.WriteLine("Front = " + front);

            if (!done)
            {
                rb.SelectionFont = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                rb.SelectionColor = curColor;
                rb.AppendText(bs);
                Debug.WriteLine("Current Base(1) = " + bs);
                curBase2Ltr = bs;
            }
            else
            {
                rb.SelectionFont = new Font("Microsoft Sans Serif", 11, FontStyle.Regular);
                rb.SelectionColor = doneColor;
                rb.AppendText(bs);
                Debug.WriteLine("Current Base (2) = " + bs);
            }
            rb.SelectionStart = rb.Text.Length;
            rb.ScrollToCaret();

            rb.SelectionFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
            rb.SelectionColor = todoColor;
            rb.AppendText(back);
            Debug.WriteLine("Back = " + back);


        }

        private void btn_Terminate_Click(object sender, EventArgs e)
        {
            using (TerminateBox terminate = new TerminateBox())
            {
                if (terminate.ShowDialog(this) == DialogResult.Cancel)
                {
                    //terminate.Dispose();
                    bTerminateEndofCycle = false;
                    bTerminateEndofStep = false;
                    return;
                }
                else
                {
                    SafeSetStatus("Terminating Run --  Please be Patient......");
                    if (RunTimer.Enabled)
                    {
                        RunTimer.Stop();
                        RunTimer.Enabled = false;
                    }
                    //terminate.Dispose();
                }
            }
            Debug.WriteLine("bTermateEndofStep = " + bTerminateEndofStep + "  bTerminateEndofCycle = " + bTerminateEndofCycle);
        }
     private void DisableTBs()
        {
            tb_col1.ReadOnly = true;
            tb_col2.ReadOnly = true;
            tb_col3.ReadOnly = true;
            tb_col4.ReadOnly = true;
            tb_col5.ReadOnly = true;
            tb_col6.ReadOnly = true;
            tb_col7.ReadOnly = true;
            tb_col8.ReadOnly = true;

            gb_Scale.Enabled = false;
            btn_ClrCol1.Enabled = false;
            btn_ClrCol2.Enabled = false;
            btn_ClrCol3.Enabled = false;
            btn_ClrCol4.Enabled = false;
            btn_ClrCol5.Enabled = false;
            btn_ClrCol6.Enabled = false;
            btn_ClrCol7.Enabled = false;
            btn_ClrCol8.Enabled = false;
            btn_ResetAll.Enabled = false;

            btn_col1.Enabled = false;
            btn_col2.Enabled = false;
            btn_col3.Enabled = false;
            btn_col4.Enabled = false;
            btn_col5.Enabled = false;
            btn_col6.Enabled = false;
            btn_col7.Enabled = false;
            btn_col8.Enabled = false;
            

            btn_Consumption.Enabled = false;
            btn_LoadBatch.Enabled = false;
        }
        private void EnableTBs()
        {
            tb_col1.ReadOnly = false;
            tb_col2.ReadOnly = false;
            tb_col3.ReadOnly = false;
            tb_col4.ReadOnly = false;
            tb_col5.ReadOnly = false;
            tb_col6.ReadOnly = false;
            tb_col7.ReadOnly = false;
            tb_col8.ReadOnly = false;

            gb_Scale.Enabled = true;
            btn_ClrCol1.Enabled = true;
            btn_ClrCol2.Enabled = true;
            btn_ClrCol3.Enabled = true;
            btn_ClrCol4.Enabled = true;
            btn_ClrCol5.Enabled = true;
            btn_ClrCol6.Enabled = true;
            btn_ClrCol7.Enabled = true;
            btn_ClrCol8.Enabled = true;
            btn_ResetAll.Enabled = true;

            btn_col1.Enabled = true;
            btn_col2.Enabled = true;
            btn_col3.Enabled = true;
            btn_col4.Enabled = true;
            btn_col5.Enabled = true;
            btn_col6.Enabled = true;
            btn_col7.Enabled = true;
            btn_col8.Enabled = true;

            btn_Consumption.Enabled = true;
            btn_LoadBatch.Enabled = true;
        }
    }
}

