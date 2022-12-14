using System;
using System.Linq;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace SeNA80
{
    public partial class Main_Form : Form
    {
        bool bMainArduino = false;
        bool bTritylArduino = false;
        public static string UVCSVfile = globals.CSV_path + "UV_" + DateTime.Now.ToString("MMddHHmm") + ".CSV";
        public static string CondCSVfile = globals.CSV_path + "Cond_" + DateTime.Now.ToString("MMddHHmm") + ".CSV";
        public static int iPresCntr = 0;
        public static DateTime start = DateTime.Now;  //for setting up polling frequency.
        public static DateTime Pstart = DateTime.Now;  //for setting up polling frequency.
        public static DateTime Qstart = DateTime.Now; //for polling with queue
        public static DateTime Bstart = DateTime.Now;  // for deblock monitoring
        public static bool bIamClosing = false;  //for closing all...
        public static bool bLoaded = false;     //just a bool to use while program loads
        public static IntPtr hmainWind = (IntPtr)null;

        public Main_Form()
        {
            //put the splash screen on its own thread instead of writing a separate async program
            //this is easiest...
            bLoaded = false;
            this.Cursor = Cursors.WaitCursor;
            ThreadPool.QueueUserWorkItem(delegate
            {
                using (Splash splash = new Splash())
                {
                    splash.Show();
                    while (!bLoaded)
                        Application.DoEvents();

                }
            }, null);

            hmainWind = this.Handle;

            InitializeComponent();

            //set the titlebar text
            this.Text = "RiboBio8 DNA/RNA Syntheiszer - " + globals.Curr_User;

            // write the logged in user to the log file
            Man_Controlcs.WriteStatus("Main", "System Starting with User - " + globals.Curr_User);
            // first read the logging state from the settings file
            globals.blogging = Properties.Settings.Default.Loggin_YN;

            if (globals.blogging)
            {
                Loggin_On_MenuItem.Checked = true;
                //if we are just reopening the program and already have a log file, open it up and count the lines
                string fName = Properties.Settings.Default.Cur_Log_File;
                globals.log_file = globals.logfile_path + fName;

                try
                {
                    if (File.Exists(globals.log_file))
                    {
                        String[] file = File.ReadAllLines(globals.log_file);
                        Man_Controlcs.lines = file.Count();
                    }
                    else
                        Man_Controlcs.lines = 0;
                }
                catch
                {
                    Man_Controlcs.lines = 0;
                }
            }

            else
                Loggin_On_MenuItem.Checked = false;


            // initialize all serial devices....
            // inclduing the main, trityl and other cotrollers, etc
            InitControllers();


            //open the Trityl Port, Open the Main Port, leave both open.

            if (!globals.bDemoMode)
            {
                if (!Main_Arduino.IsOpen)
                {
                    Main_Arduino.Open();
                    //initialize it
                    Main_Form.Main_Arduino.Write("?\n");
                    string recd = Main_Form.Main_Arduino.ReadLine();
                    while (!recd.Contains("OK"))
                    {
                        Man_Controlcs.SyncWait(20);
                        recd = Main_Form.Main_Arduino.ReadLine();
                    }
                    globals.bIsMain = true;
                }

                if (globals.bUVTrityl)
                {

                    if (!UV_Trityl_Arduino.IsOpen)
                        UV_Trityl_Arduino.Open();
                }
                //turn on the LEDs
                if (globals.bUVTrityl || globals.bMonPressure || globals.bCondTrityl)
                {
                    Task cO = Task.Run(async () => { await Man_Controlcs.TurnCellsOn(); });
                    //cO.Wait();


                }
                else
                    Menu_CellsOnOff.Enabled = false;

                //start the data streams
                if (globals.bUVTrityl)
                    StartSensorData(1);
                if (globals.bCondTrityl)
                    StartSensorData(2);


            }

            //keep the computer from sleeping while the system is running
            SystemSleepManagement.PreventSleep(true);

            //gray the BaseCaller Menu for basic users
            if (globals.Curr_Rights.Contains("2"))
                Menu_BaseTable.Enabled = false;

            if (!globals.Curr_Rights.Contains("Admin"))
                Menu_Config.Enabled = false;

            bLoaded = true;
            this.Cursor = Cursors.Default;

            if (globals.bShowTips)
            {
                Menu_Tips.Checked = true;
                Menu_Tips.Text = "&Disable Tips..";
            }
            else
            {
                Menu_Tips.Checked = false;
                Menu_Tips.Text = "&Enable Tips..";
            }

        } //done with initialization...

        private void btnPress_Click(object sender, EventArgs e)
        {
            if (globals.pressuriz == null)
                globals.pressuriz = new Pressuriz();


            try { globals.pressuriz.ShowDialog(this); }
            catch { }

        }

        private void btnAmConf_Click(object sender, EventArgs e)
        {
            using (AmiditeConfig dlg = new AmiditeConfig())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
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

        private void btnSchedEd_Click(object sender, EventArgs e)
        {
            using (Sequence_Editor dlg = new Sequence_Editor())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    return;
                    // whatever you need to do with result
                }
            }
        }

        private void btnManCtl_Click(object sender, EventArgs e)
        {
            if (globals.manctl == null)
                globals.manctl = new Man_Controlcs();

            try { globals.manctl.ShowDialog(this); }
            catch (Exception x) { MessageBox.Show("Error Opening Manual Control -" + x.ToString()); }

        }

        public static void Trityl_DataProcess(object sender, SerialDataReceivedEventArgs e)
        {
            if (globals.bUVStreaming)
            {

                try { ProcessUVData(); }
                catch { Man_Controlcs.WriteStatus("Data Stream", "Could Not Process a Data Point"); }
            }

        }
        static async Task WaitDelay(int ms)
        {
            await Task.Delay(ms);
        }
        static int iCounter = 0;

        public static async void ProcessUVData()
        {
            Man_Controlcs.SyncWait(800);

            int length = 200;
            string indata = "";
            string[] lines = new string[8];
            byte[] buf = new byte[length];
            string datastring = "";

            //add a P every minute with date and time to show polling workin to the title
            iCounter = iCounter + 1;
            if (iCounter > 10)  //polling at about 1/sec so every 10 seconds update the title bar
            {
                if (globals.runform != null && globals.bUVTrityl)
                {
                    try
                    {
                        string cTitle = globals.runform.Text;
                        if (cTitle.Contains("-Trityl"))
                        {
                            cTitle = cTitle.Substring(0, cTitle.LastIndexOf("-Trityl") - 2);
                        }

                        Man_Controlcs.LabelSafe(globals.runform, cTitle + "  -Trityl Polling at - " + DateTime.Now.ToString("hh:mm:ss"));
                    }
                    catch (Exception x) { MessageBox.Show("Could not update Title\n\n" + x.ToString(), "Polling Text", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
                iCounter = 0;
            }


            //read the data on the port
            if (!globals.bDemoMode)//shouldn't ever be here but check anyway
            {

                if (UV_Trityl_Arduino.IsOpen && !bIamClosing)
                {

                    try
                    {
                        await WaitDelay(50);
                        try { UV_Trityl_Arduino.Read(buf, 0, length); }
                        catch (System.IO.IOException error) { Man_Controlcs.WriteStatus("Run", "Error Reading trityl Controller" + error); return; }
                        catch (System.InvalidOperationException error) { Man_Controlcs.WriteStatus("Run", "Error Reading Trityl Controller" + error); return; }
                        catch { return; }

                        if (buf.Length > 0)
                            indata = System.Text.Encoding.UTF8.GetString(buf);
                        else
                            Man_Controlcs.WriteStatus("Trityl", "Could not get data from trityl monitor");

                        //try { indata = UV_Trityl_Arduino.ReadTo("\n"); Thread.Sleep(30);}
                        //catch (Exception x) { MessageBox.Show("Error - " + x.ToString(), indata); return; }

                        //return if bad data
                        if (indata.Trim().Length < 10 || !indata.Contains("data") || indata.Trim().Length > 200)
                        {
                            //try again next hit...
                            return;
                        }
                        //get the crap off the end of the string
                        indata = indata.Replace("\0", string.Empty);
                        //indata = indata.Replace("\n", string.Empty);
                        //indata = indata.Replace("\r", string.Empty);
                        //count the number of lines
                        int cnt = 0;
                        foreach (char c in indata)
                        {
                            if (c == '\n') cnt++;
                        }


                        if (cnt > 0)
                        {
                            int MaxCnt = 0;
                            if (cnt < 5)
                                MaxCnt = cnt;
                            else
                                MaxCnt = 5;


                            for (int i = 0; i < MaxCnt; i++)
                                lines = indata.Split('\n');

                        }
                        else
                            return;

                    }
                    catch (Exception x) { MessageBox.Show("Error Occured - " + x.ToString(), "Data Read Error"); return; }
                }
                string[] outdata = new string[5];
                //initialize the array
                for (int i = 0; i < 5; i++)
                    outdata[i] = string.Empty;

                int k = 0;
                foreach (string line in lines)
                {
                    outdata[k] = Regex.Replace(indata, @"^\s+$*", "", RegexOptions.Multiline);
                    k++;
                    if (k > 4)
                        break;
                }
                globals.sStream = outdata[0];

                string[] values = new string[13];
                //initialize the array
                for (int i = 0; i < 13; i++)
                    values[i] = "";

                if (!(outdata.Length > 2) || outdata == null)
                    return;

                for (int Ts = 0; Ts < outdata.Length; Ts++)
                {
                    if (outdata.Length > 0 && outdata != null)
                    {
                        if (DataValidata(outdata[Ts], 11))
                        {
                            try
                            {
                                values = outdata[Ts].Split(',');
                                values[11] = values[11].Replace("\0", string.Empty);
                                values[11] = values[11].TrimEnd();
                            }
                            catch { return; }
                        }
                        else  //just wait for more good data
                            return;
                    }

                    //Man_Controlcs.WriteStatus("Trityl", outdata[Ts]);
                    //now validate the data
                    if (values[0].Contains("data"))
                    {
                        if (values.Count() < 11)
                        {
                            //MessageBox.Show(outdata);
                            return;
                        }
                        else
                        {
                            //Debug.WriteLine("out data string" + outdata[0] + "Values 11" + values[11]);
                            if (!globals.bMonPressure)
                            {
                                values[9] = "0";
                                values[10] = "0";
                                values[11] = "0";

                            }
                            else
                            {
                                double elapsed = DateTime.Now.Subtract(Pstart).TotalSeconds;

                                if (elapsed > globals.polFreq)
                                {
                                    double dAmP = 0.0;
                                    double dReagP = 0.0;

                                    dReagP = Convert.ToDouble(values[9]) / 100;
                                    dAmP = Convert.ToDouble(values[10]) / 100;

                                    //store in the globals variable as doubles
                                    globals.dReagPres = dReagP;
                                    globals.dAmidPres = dAmP;

                                    //check to make sure that the pressure is within 20% of the calibrated pressure, if not set the label color RED
                                    //make it 40% since we are at smaller pressures to avoid the red so much...
                                    if ((dAmP < (globals.dAmditePCalib * 1.4)) && (dAmP > (globals.dAmditePCalib * 0.6)))
                                    {
                                        globals.runform.l_AmP.BackColor = System.Drawing.Color.Transparent;
                                        globals.manctl.l_AmPres.BackColor = System.Drawing.Color.Transparent;
                                        Man_Controlcs.LabelSafe(globals.runform.l_AmP, dAmP.ToString("0.0") + " psi");
                                        Man_Controlcs.LabelSafe(globals.manctl.l_AmPres, dAmP.ToString("0.0") + " psi");
                                    }
                                    else
                                    {
                                        globals.runform.l_AmP.BackColor = System.Drawing.Color.Red;
                                        globals.manctl.l_AmPres.BackColor = System.Drawing.Color.Red;
                                        Man_Controlcs.LabelSafe(globals.runform.l_AmP, dAmP.ToString("0.0") + " - ERR");
                                        Man_Controlcs.LabelSafe(globals.manctl.l_AmPres, dAmP.ToString("0.0") + " - ERR");
                                    }


                                    if ((dReagP < (globals.dReagentPCalib * 1.4)) && (dReagP > (globals.dReagentPCalib * 0.6)))
                                    {
                                        globals.runform.l_ReagP.BackColor = System.Drawing.Color.Transparent;
                                        globals.manctl.l_RgtPres.BackColor = System.Drawing.Color.Transparent;
                                        Man_Controlcs.LabelSafe(globals.runform.l_ReagP, dReagP.ToString("0.0") + " psi");
                                        Man_Controlcs.LabelSafe(globals.manctl.l_RgtPres, dReagP.ToString("0.0") + " psi");
                                    }
                                    else
                                    {
                                        globals.runform.l_ReagP.BackColor = System.Drawing.Color.Red;
                                        globals.manctl.l_RgtPres.BackColor = System.Drawing.Color.Red;
                                        Man_Controlcs.LabelSafe(globals.runform.l_ReagP, dReagP.ToString("0.0") + "- ERR");
                                        Man_Controlcs.LabelSafe(globals.manctl.l_RgtPres, dReagP.ToString("0.0") + " - ERR");
                                    }

                                    iPresCntr++;

                                    Pstart = DateTime.Now;


                                    if (iPresCntr > 80) //~70 update status every minute
                                    {
                                        iPresCntr = 0;
                                        Man_Controlcs.WriteStatus("Pressure Monitor", "Amidite Pressure: " + dAmP.ToString("0.0") + " psi   " + "Calib Pressure: " + globals.dAmditePCalib.ToString("0.0") + " psi");
                                        Man_Controlcs.WriteStatus("Pressure Monitor", "Reagent Pressure: " + dReagP.ToString("0.0") + " psi   " + "Calib Pressure: " + globals.dReagentPCalib.ToString("0.0") + " psi");
                                    }
                                }
                            }
                            double debelapsed = DateTime.Now.Subtract(Bstart).TotalSeconds;
                            if (debelapsed > 1) //for deblock monitoring make it one time per second
                            {
                                if (globals.DeblockMonitor)
                                {

                                    for (int tseegii = 1; tseegii < 9; tseegii++)  //only do the trityl data, don't worry about the pressures
                                    { globals.iUVTritylData[tseegii - 1, globals.MonitorCntr] = Int32.Parse(values[tseegii]); globals.MonitorCntr = globals.MonitorCntr + 1; }
                                }

                                datastring = values[1] + "," + values[2] + "," + values[3] + "," + values[4] + "," + values[5] + "," +
                                    values[6] + "," + values[7] + "," + values[8];

                                Bstart = DateTime.Now;
                            }
                        }
                    }
                    // MessageBox.Show(datastring);
                    if (globals.bStripCharting)
                    {

                        double elapsed = DateTime.Now.Subtract(start).TotalSeconds;
                        if (elapsed > globals.polFreq)
                        {
                            start = DateTime.Now;
                            stripchartcs.UpdateStripChart(datastring);
                        }

                    }
                    double qlapsed = DateTime.Now.Subtract(Qstart).TotalSeconds;
                    if (qlapsed > globals.polFreq) //only update according to the polling frequency
                    {
                        if (DataValidata(datastring, 8))
                        {

                            if (globals.UVTrityResponse.Count < globals.maxtritylpts)
                            {
                                try
                                {
                                    //store the date and time
                                    globals.TritylDate.Enqueue(DateTime.Now);

                                    //now store the outstring
                                    globals.UVTrityResponse.Enqueue(datastring);
                                }
                                catch (Exception x) { Man_Controlcs.WriteStatus("Enqueue", "Could Not add points" + x.ToString()); }
                            }
                            else
                            {
                                //remove half the values from the queue and continue
                                for (int i = 0; i < (int)(globals.maxtritylpts / 2); i++)
                                {
                                    try
                                    {
                                        globals.TritylDate.Dequeue();
                                        globals.UVTrityResponse.Dequeue();
                                    }
                                    catch (Exception x) { Man_Controlcs.WriteStatus("Enqueue", "Could Not remove points" + x.ToString()); }
                                }


                                //clear off the null values in the queue
                                globals.TritylDate.TrimToSize();
                                globals.UVTrityResponse.TrimToSize();
                                try
                                {
                                    //store the data
                                    globals.TritylDate.Enqueue(DateTime.Now);

                                    //now store the outstring
                                    globals.UVTrityResponse.Enqueue(datastring);
                                }
                                catch (Exception x) { Man_Controlcs.WriteStatus("Enqueue", "Could Not add points" + x.ToString()); }
                            }
                        }
                        //if collecting data write the data to a file
                        if (globals.bIsRunning)
                        {
                            //later put in the logic for storing only deblock or all
                            //we will have a global bDeblocking variable and if deblock only 
                            //is selected we will save only during deblocking, otherwise all
                            if (globals.bUVTrityl)
                            {
                                //write the CSV for UV data for the current run
                                string CSVString = "";

                                //make sure the string is complete


                                CSVString = DateTime.Now.ToString("MMddyyyy HH:mm:ss") + "," + datastring;


                                if (DataValidata(CSVString, 9))
                                {
                                    //MessageBox.Show("HERE CSV" + UVCSVfile);
                                    FileStream fs = null;
                                    try
                                    {
                                        // for now turn off file saving -- later we will put it back
                                        if (globals.bSaveStrip)
                                        {
                                            if (!File.Exists(UVCSVfile))
                                            {
                                                // Create a file to write to.
                                                var filestr = new FileStream(UVCSVfile, FileMode.Create);
                                                using (StreamWriter sw = new StreamWriter(filestr))
                                                {

                                                    filestr = null;
                                                    sw.WriteLine(CSVString);
                                                }
                                            }
                                            else
                                            {
                                                var filestr = new FileStream(UVCSVfile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                                                using (StreamWriter sw = new StreamWriter(filestr))
                                                //StreamWriter sw = File.AppendText(UVCSVfile, FileShare.ReadWrite))
                                                {
                                                    filestr = null;
                                                    sw.WriteLine(CSVString);
                                                }
                                            }
                                        }
                                    }
                                    finally
                                    {
                                        if (fs != null)
                                            fs.Dispose();
                                    }
                                }

                            }
                            Qstart = DateTime.Now;
                        }
                        //if charting, update the real time chart...
                        if (globals.bStripCharting)
                        {
                            //add point to strip chart
                        }

                        if (globals.iCurLine < globals.maxtritylpts)
                            globals.iCurLine++; //increment the line counter
                    }

                }
            } // demo mode

        }
        private static bool DataValidata(string checkdata, int length)
        {
            string[] parts = checkdata.Split(',');

            //first make sure that there are enough elements
            if (parts.Length < length)
                return false;

            //next make sure every elemeent contains something
            foreach (string part in parts)
            {
                if (part.Length < 1)
                    return false;
            }

            return true;
        }
        private void Loggin_On_MenuItem_Click(object sender, EventArgs e)
        {
            if (Loggin_On_MenuItem.Checked) // on turn off
            {
                //set the logging state on
                Loggin_On_MenuItem.Checked = true;
                globals.blogging = true;

                // AddUpdateAppSettings("Loggin_YN", true);

                ChangBoolGlobal(true);
                //Properties.Settings.Default.Loggin_YN = false;
                //Properties.Settings.Default.Upgrade();
                //Properties.Settings.Default.Save();
                MessageBox.Show(Properties.Settings.Default.Loggin_YN.ToString(), Properties.Settings.Default.Am_10_lbl.ToString());
            }
            else // off turn on
            {
                //set the logging state turn on
                Loggin_On_MenuItem.Checked = false;
                globals.blogging = false;

                ChangBoolGlobal(false);
                //Properties.Settings.Default.Upgrade();
                //Properties.Settings.Default.Save();
                MessageBox.Show(Properties.Settings.Default.Loggin_YN.ToString(), "BOTTOM");
            }
        }

        //this function makes sure that the setting is stored if the program is 
        //closed and reopened, will also have the same funciton for text values 
        public void ChangBoolGlobal(bool value)
        {
            Properties.Settings settings = Properties.Settings.Default;

            settings.Loggin_YN = (bool)value;
            settings.Save();
        }

        private void View_Log_MenuItem_Click(object sender, EventArgs e)
        {
            if (globals.blogging)
                System.Diagnostics.Process.Start("notepad.exe", globals.log_file);
            else
                MessageBox.Show("Must turn logging on first", "Log File View");
        }
        private void Main_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            //set the bool to true;
            bIamClosing = true;

            //close the graphing dialogs
            if (globals.sc != null)
                globals.sc.Close();

            if (globals.sb != null)
                globals.sb.Close();

            globals.bBarCharting = false;

            //reset sleep to default
            SystemSleepManagement.ResotreSleep();

            Properties.Settings.Default.Upgrade();
            Properties.Settings.Default.Save();
            if (Main_Arduino.IsOpen)
                Main_Arduino.Close();

            globals.bUVStreaming = false;
            if (UV_Trityl_Arduino.IsOpen)
            {
                //Stop Stream
                UV_Trityl_Arduino.Write("?\n");
                Man_Controlcs.SyncWait(1200);


                //close Arduino
                UV_Trityl_Arduino.Close();
            }

            this.Dispose();
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }

        }

        private void Exit_Menu_Item_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Upgrade();
            Properties.Settings.Default.Save();
            if (Main_Arduino.IsOpen)
                Main_Arduino.Close();

            globals.bUVStreaming = false;
            if (UV_Trityl_Arduino.IsOpen)
            {
                //Stop Stream
                UV_Trityl_Arduino.Write("?\n");
                Man_Controlcs.SyncWait(1200);


                //close Arduino
                UV_Trityl_Arduino.Close();
            }

            this.Dispose();
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process.Start(globals.Help_Path);
        }

        private void btnMetEd_Click(object sender, EventArgs e)
        {
            //Administrators and User 1 can edit protocols
            if (globals.Curr_Rights.Contains("2"))
            {
                MessageBox.Show("You must be an Administrator or User 1 rights\nto edit protocols", "Insufficient Rights", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            using (Protocol_Editor dlg = new Protocol_Editor())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    return;
                    // whatever you need to do with result
                }
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            SafeNativeMethods.ShowMain((int)hmainWind, 0);
            if (globals.runform == null)
                globals.runform = new SeNARun();

            try { globals.runform.ShowDialog(this); }
            catch (Exception x) { MessageBox.Show("Error Opening Run Program -" + x.ToString()); }

        }

        private void Menu_Config_Click(object sender, EventArgs e)
        {
            if (!globals.Curr_Rights.Contains("Admin"))
            {
                MessageBox.Show("You must be an Administrator to configure system parameters\n\nand add or edit users", "Administrator Only", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            using (Config_Editor dlg = new Config_Editor())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (globals.bUVTrityl || globals.bMonPressure || globals.bCondTrityl)
                        Menu_CellsOnOff.Enabled = true;

                    return;
                    // whatever you need to do with result
                }
            }
        }

        private void View_TritylSC_Click(object sender, EventArgs e)
        {
            //check to make sure a monitor is configured and on
            if (!globals.bUVTrityl && !globals.bMonPressure)
            {
                MessageBox.Show("You Must Configure a Monitor\n\nOr turn on a cell", "No Monitor");
                return;
            }

            if (globals.bUVTrityl)
            {
                if (!globals.bUV1On && !globals.bUV2On && !globals.bUV3On && !globals.bUV4On &&
                !globals.bUV5On && !globals.bUV6On && !globals.bUV7On && !globals.bUV8On)
                {
                    MessageBox.Show("You Must Configure a Monitor\n\nOr turn on a cell", "No Monitor");
                    return;
                }
            }

            if (globals.bMonPressure)
            {
                if (!globals.bPres1On && !globals.bPres2On && !globals.bPres2On)
                {
                    MessageBox.Show("You Must Configure a Monitor\n\nOr turn on a cell", "No Monitor");
                    return;
                }
            }
            if (globals.sc != null)
                globals.sc = new stripchartcs();

            globals.sc.Show();


        }

        private void Menu_CellsOnOff_Click(object sender, EventArgs e)
        {
            Sensor_Config sconf = new Sensor_Config();

            if (sconf.ShowDialog() == DialogResult.OK)
            {
                return;
                // whatever you need to do with result
            }

        }

        private void Menu_BaseTable_Click(object sender, EventArgs e)
        {
            using (BaseTable dlg = new BaseTable())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //dlg.Dispose();
                    // whatever you need to do with result
                }
            }
        }

        private void btnPress_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btnPress, "System Pressurize", "Program to Pressurize all of the Reagents and Amidites");
        }

        private void btnAmConf_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btnAmConf, "Amidite Configuration", "Allows you to define the amidites installed at each bottle position on the instrument\nand assign a coupling protocol to each.");
        }

        private void btnManCtl_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btnManCtl, "Manual Control", "Manually Control the delivery of all Reagents and Amdities through a graphical interface.");
        }

        private void btnRun_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btnRun, "Run Program", "After selecting protocols and sequences,\nthe Run Program will, in a fully automated fashion,\ncontrol the entire synthesis procedure");
        }

        private void btnMetEd_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btnMetEd, "Protocol Editor", "This program allows you to create or edit protocols.  These protocols will\nbe used to control the automated synthesis process.");
        }

        private void btnSchedEd_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btnSchedEd, "Sequence Editor", "The Sequence Editor allows you to type, paste or import \nindividual or Batches of sequence(s)");
        }

        private void Menu_Tips_Click(object sender, EventArgs e)
        {
            if (Menu_Tips.Checked) //disable
            {
                Menu_Tips.Checked = false;
                Menu_Tips.Text = "&Enable Tips..";
                globals.bShowTips = false;
                Properties.Settings.Default.Tips_Enabled = false;
                Properties.Settings.Default.Save();
                Debug.WriteLine("The value of showTips is = " + globals.bShowTips.ToString());
            }
            else //enable
            {
                Menu_Tips.Checked = true;
                Menu_Tips.Text = "&Disable Tips..";
                globals.bShowTips = true;
                Properties.Settings.Default.Tips_Enabled = true;
                Properties.Settings.Default.Save();
                Debug.WriteLine("The value of showTips is = " + globals.bShowTips.ToString());
            }
        }
    }
}
