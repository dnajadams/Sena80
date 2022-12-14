using System;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SeNA80
{
    partial class Man_Controlcs
    {
        public static int lines = 0;
        public delegate void SetControlCallback(System.Windows.Forms.TextBox c, string text);
        public delegate void SetLabelCallback(System.Windows.Forms.Control l, string text);
        public static void LabelSafe(System.Windows.Forms.Control l, string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            // SetControlCallback(System.Windows.Forms.TextBox c, string text);
            if (l.InvokeRequired)
            {
                SetLabelCallback d = new SetLabelCallback(LabelSafe);
                try
                {
                    l.Invoke(d, new object[] { l, text });
                }
                catch  //(System.Reflection.TargetInvocationException tie)
                {
                    WriteStatus("Could Not Update Label", "Invoke Error ");
                }
            }
            else
            {
                if (l != null)
                    l.Text = text;
            }
        }

        //asynchronously await a delay
        public static async Task AsyncWait(int wait)
        {
            await Task.Delay(wait);
        }
        private static void SetControlText(System.Windows.Forms.TextBox c, string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            // SetControlCallback(System.Windows.Forms.TextBox c, string text);
            SetControlCallback d = new SetControlCallback(SetControlText);
            if (c != null && c.InvokeRequired)
            {
                try
                {
                    //MessageBox.Show("In Callback" + text, c.ToString());
                    c.BeginInvoke(d, new object[] { c, text });
                }
                catch (Exception e) //(System.Reflection.TargetInvocationException tie)
                {
                    MessageBox.Show("Invoke Error \n" + e.ToString(), "Invoke Issue");
                }
            }
            else
            {
                if (c != null)
                {
                    try { c.AppendText(text); }
                    catch (Exception e) { MessageBox.Show("Could not write the message - " + text + "\nReason - " + e.ToString()); }
                }
            }
        }
        public static void UpdatePropConfigLetterCodes(int ibases, string [] bases)
        {
            int oldcode = 0;

            //first build the codes array for old variable
            if (globals.i12Ltr == 0)
                oldcode = 1;
            else
                oldcode = 0;

            //rebuild the codes array for both old codes and new codes
            string[] oldcodes = new string[ibases];
            string[] newcodes = new string[ibases];
            for (int i = 0; i < ibases; i++)
            {
                string[] parts = bases[i].Split(',');
                oldcodes[i] = parts[oldcode];
                newcodes[i] = parts[globals.i12Ltr];
            }

            //now for each code in the Properties Settings File reset based on the new letter selection
            string curcode = string.Empty;
            curcode = Properties.Settings.Default.Am_1_lbl;
            int inxAt = Array.FindIndex(oldcodes, row => row.Contains(curcode));
            if (inxAt > -1)
                Properties.Settings.Default.Am_1_lbl = newcodes[inxAt];

            //repeat 14 times
            curcode = string.Empty;
            curcode = Properties.Settings.Default.Am_2_lbl;
            inxAt = Array.FindIndex(oldcodes, row => row.Contains(curcode));
            if (inxAt > -1)
                Properties.Settings.Default.Am_2_lbl = newcodes[inxAt];

            curcode = string.Empty;
            curcode = Properties.Settings.Default.Am_3_lbl;
            inxAt = Array.FindIndex(oldcodes, row => row.Contains(curcode));
            if (inxAt > -1)
                Properties.Settings.Default.Am_3_lbl = newcodes[inxAt];

            curcode = string.Empty;
            curcode = Properties.Settings.Default.Am_4_lbl;
            inxAt = Array.FindIndex(oldcodes, row => row.Contains(curcode));
            if (inxAt > -1)
                Properties.Settings.Default.Am_4_lbl = newcodes[inxAt];

            curcode = string.Empty;
            curcode = Properties.Settings.Default.Am_5_lbl;
            inxAt = Array.FindIndex(oldcodes, row => row.Contains(curcode));
            if (inxAt > -1)
                Properties.Settings.Default.Am_5_lbl = newcodes[inxAt];

            curcode = string.Empty;
            curcode = Properties.Settings.Default.Am_6_lbl;
            inxAt = Array.FindIndex(oldcodes, row => row.Contains(curcode));
            if (inxAt > -1)
                Properties.Settings.Default.Am_6_lbl = newcodes[inxAt];

            curcode = string.Empty;
            curcode = Properties.Settings.Default.Am_7_lbl;
            inxAt = Array.FindIndex(oldcodes, row => row.Contains(curcode));
            if (inxAt > -1)
                Properties.Settings.Default.Am_7_lbl = newcodes[inxAt];

            curcode = string.Empty;
            curcode = Properties.Settings.Default.Am_8_lbl;
            inxAt = Array.FindIndex(oldcodes, row => row.Contains(curcode));
            if (inxAt > -1)
                Properties.Settings.Default.Am_8_lbl = newcodes[inxAt];

            curcode = string.Empty;
            curcode = Properties.Settings.Default.Am_9_lbl;
            inxAt = Array.FindIndex(oldcodes, row => row.Contains(curcode));
            if (inxAt > -1)
                Properties.Settings.Default.Am_9_lbl = newcodes[inxAt];

            curcode = string.Empty;
            curcode = Properties.Settings.Default.Am_10_lbl;
            inxAt = Array.FindIndex(oldcodes, row => row.Contains(curcode));
            if (inxAt > -1)
                Properties.Settings.Default.Am_10_lbl = newcodes[inxAt];

            curcode = string.Empty;
            curcode = Properties.Settings.Default.Am_11_lbl;
            inxAt = Array.FindIndex(oldcodes, row => row.Contains(curcode));
            if (inxAt > -1)
                Properties.Settings.Default.Am_11_lbl = newcodes[inxAt];

            curcode = string.Empty;
            curcode = Properties.Settings.Default.Am_12_lbl;
            inxAt = Array.FindIndex(oldcodes, row => row.Contains(curcode));
            if (inxAt > -1)
                Properties.Settings.Default.Am_12_lbl = newcodes[inxAt];

            curcode = string.Empty;
            curcode = Properties.Settings.Default.Am_13_lbl;
            inxAt = Array.FindIndex(oldcodes, row => row.Contains(curcode));
            if (inxAt > -1)
                Properties.Settings.Default.Am_13_lbl = newcodes[inxAt];

            curcode = string.Empty;
            curcode = Properties.Settings.Default.Am_14_lbl;
            inxAt = Array.FindIndex(oldcodes, row => row.Contains(curcode));
            if (inxAt > -1)
                Properties.Settings.Default.Am_14_lbl = newcodes[inxAt];

            /* Comment out until we have a system with 16 amidites
            curcode = string.Empty;
            curcode = Properties.Settings.Default.Am_15_lbl;
            inxAt = Array.FindIndex(oldcodes, row => row.Contains(curcode));
            if(inxAt > -1)
                Properties.Settings.Default.Am_15_lbl = newcodes[inxAt];

            curcode = string.Empty;
            curcode = Properties.Settings.Default.Am_16_lbl;
            inxAt = Array.FindIndex(oldcodes, row => row.Contains(curcode));
            if(inxAt > -1)
                Properties.Settings.Default.Am_16_lbl = newcodes[inxAt];
            */
            Properties.Settings.Default.Save();

        }
        public static void SeqConvertLetterCodesRTB(System.Windows.Forms.RichTextBox tb, int to, int ibases, string[] bases)
        {
            string tbstring = tb.Text;
            string newstring = string.Empty;
            int sptr = 0;

            if (to == 0)  //convert 2 letter code to 1 letter
            {
                while (sptr < tbstring.Length)
                {
                    string code = tbstring.Substring(sptr, 2);
                    string outcode = RetCode(0, code, ibases, bases);
                    Debug.WriteLine("outcode" + outcode);
                    newstring = newstring + outcode;
                    sptr = sptr + 3;
                }
            }
            else
            {
                while (sptr < tbstring.Length)
                {
                    string code = tbstring.Substring(sptr, 1);
                    string outcode = RetCode(1, code, ibases, bases);
                    Debug.WriteLine("outcode" + outcode);
                    newstring = newstring + outcode + "-";
                    sptr = sptr + 1;
                }
            }
            tb.Text = newstring;

        }
        public static void PopToolMsg(System.Windows.Forms.Control c, string title, string msg)
        {
            if (!globals.bShowTips)
                return;

            //note may have to invoke for cross thread operation
            ToolTip tp = new ToolTip();

            tp.ToolTipTitle = title;
            tp.UseFading = true;
            tp.UseAnimation = true;
            tp.IsBalloon = true;
            tp.ShowAlways = true;
            tp.AutoPopDelay = 3500;
            tp.InitialDelay = 250;
            tp.ReshowDelay = 500;
            tp.SetToolTip(c, msg);
        }
        public static void SeqConvertLetterCodes(System.Windows.Forms.TextBox tb, int to, int ibases, string [] bases)
        {
            string tbstring = tb.Text;
            string newstring = string.Empty;
            int sptr = 0;

            if (to == 0)  //convert 2 letter code to 1 letter
            {
                while (sptr < tbstring.Length)
                {
                    string code = tbstring.Substring(sptr, 2);
                    string outcode = RetCode(0, code, ibases, bases);
                    Debug.WriteLine("outcode" + outcode);
                    newstring = newstring + outcode;
                    sptr = sptr + 3;
                }
            }
            else
            {
                while (sptr < tbstring.Length)
                {
                    string code = tbstring.Substring(sptr, 1);
                    string outcode = RetCode(1, code, ibases, bases);
                    Debug.WriteLine("outcode" + outcode);
                    newstring = newstring + outcode + "-";
                    sptr = sptr + 1;
                }
            }
            tb.Text = newstring;

        }
        private static string RetCode(int index, string incode, int ibases, string [] bases)
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

        public static void WriteStatus(string sPort, string toWrite)
        {
            DateTime localDate = DateTime.Now;
            string s = String.Format("{0}         Wrote to {1} {2} \r\n", DateTime.Now.ToString("MM/dd/yyy hh:mm:ss.ff tt"), sPort, toWrite);

            //Make thread safe writes to the two status boxes 
            //Man_Controlcs.Man_Status.AppendText(s);
            if (globals.manctl != null) { SetControlText(globals.manctl.Man_Status, s); }
            Thread.Sleep(50);
            if (globals.runform != null) { SetControlText(globals.runform.Status_R, s); }
            Thread.Sleep(50);
            if (globals.pressuriz != null) { SetControlText(globals.pressuriz.pStatusBox, s); }
            

            //Append the same thing to all status boxes...then when full write to file and empty
            //every 10 lines write the data to a file, every 1000 lines create a new file
            if (globals.blogging)  // if logging turned on, lets keep track of everything
            {
                if (lines < 1)
                {
                    //create a new file
                    string fName = String.Format("{0:ddMMyyyy}", DateTime.Now);
                    globals.log_file = globals.logfile_path + "L_" + fName + ".log";

                    //update the current file for Properties.Settings
                    Properties.Settings.Default.Cur_Log_File = "L_" + fName + ".log";
                    Properties.Settings.Default.Save();

                    try { File.WriteAllText(globals.log_file, s);}
                    catch (Exception x)
                    {
                        if (!globals.bIsRunning)
                        { MessageBox.Show("Error Creating a new Log File - \n\n" + x.ToString(), "Logging Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        else
                        { Man_Controlcs.WriteStatus("LOG FILE ERROR", "Could not create a new log file"); }

                    }
                }
                if (lines >= 1 && lines < globals.iMaxLines)  //append to text file
                {
                     try { File.AppendAllText(globals.log_file, s);  }
                     catch (Exception x)
                     {
                         if (!globals.bIsRunning)
                         { MessageBox.Show("Error Appending to Log File - \n\n" + x.ToString(), "Logging Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        else
                         { Man_Controlcs.WriteStatus("LOG FILE ERROR", "Could not update the log file"); }
                     }
                    
                    if (lines > (globals.iMaxLines - 2))
                        lines = -1;
                }
                lines++;
                //MessageBox.Show("STring "+s+"Lines "+lines.ToString(), globals.log_file);
            }

        }


        public static void ReceiveStatus(string sPort, string toWrite)
        {
            DateTime localDate = DateTime.Now;
            string s = String.Format("{0}         Recieved {1} from {2} \r\n", DateTime.Now.ToString("MM/dd/yyy hh:mm:ss.ff tt"), toWrite, sPort);

            //Man_Controlcs.Man_Status.AppendText(s);
            if (globals.manctl != null) { SetControlText(globals.manctl.Man_Status, s); }
            if (globals.runform != null) { SetControlText(globals.runform.Status_R, s); }
            if (globals.pressuriz != null) { SetControlText(globals.pressuriz.pStatusBox, s); }


            //Update the Log..if we have logging turned on
            if (globals.blogging)
            {
                if (lines < 1)
                {
                    string fName = String.Format("{0:ddMMyyyy}", DateTime.Now);
                    globals.log_file = globals.logfile_path + "L_" + fName + ".log";

                    //MessageBox.Show(globals.log_file);

                    try { File.WriteAllText(globals.log_file, s); }
                    catch { MessageBox.Show("Error Writing to Log File", "Logging Error"); }
                }
                if (lines >= 1 && lines < globals.iMaxLines)  //append to text file
                {
                    try { File.AppendAllText(globals.log_file, s); }
                    catch { MessageBox.Show("Error Appending to Log File", "Logging Error"); }

                    if (lines > (globals.iMaxLines - 2))
                        lines = -1;
                }

                lines++;

                //MessageBox.Show(s, lines.ToString(), MessageBoxButtons.OKCancel);*/
            }
        }
        //synchronous wait to replace Thread.Sleep
        public static void SyncWait(double waitfor)
        {
            if (waitfor < 5) { return; }

            DateTime _desired = DateTime.Now.AddMilliseconds(waitfor);

            while (DateTime.Now < _desired)
            {
                System.Windows.Forms.Application.DoEvents();
            }
        }
        // turn flow cells on or off at the start of the program based on defaults
        static async Task PutCODelay(int ms)
        {
            await Task.Delay(ms);
        }
        public static async Task TurnCellsOn()
        {
            if (globals.bUVTrityl)
            {
                if (globals.bUV1On)
                {
                    Man_Controlcs.SendControllerMsg(2, valves.UV1on);
                    await PutCODelay(5);
                }
                if (globals.bUV2On)
                {
                    Man_Controlcs.SendControllerMsg(2, valves.UV2on);
                    await PutCODelay(5);
                }
                if (globals.bUV3On)
                {
                    Man_Controlcs.SendControllerMsg(2, valves.UV3on);
                    await PutCODelay(5);
                }
                if (globals.bUV4On)
                {
                    Man_Controlcs.SendControllerMsg(2, valves.UV4on);
                    await PutCODelay(5);
                }
                if (globals.bUV5On)
                {
                    Man_Controlcs.SendControllerMsg(2, valves.UV5on);
                    await PutCODelay(5);
                }
                if (globals.bUV6On)
                {
                    Man_Controlcs.SendControllerMsg(2, valves.UV6on);
                    await PutCODelay(5);
                }
                if (globals.bUV7On)
                {
                    Man_Controlcs.SendControllerMsg(2, valves.UV7on);
                    await PutCODelay(5);
                }
                if (globals.bUV8On)
                {
                    Man_Controlcs.SendControllerMsg(2, valves.UV8on);
                    await PutCODelay(5);
                }


            }
            if (globals.bMonPressure)
            {
                if (globals.bPres1On)
                { Man_Controlcs.SendControllerMsg(2, "P,1,1\n"); }
                if (globals.bPres2On)
                { Man_Controlcs.SendControllerMsg(2, "P,2,1\n"); }
                if (globals.bPres3On)
                { Man_Controlcs.SendControllerMsg(2, "P,3,1\n"); }

                Thread.Sleep(50);
            }

            if (globals.bCondTrityl)
            {
                if (globals.bCond1On)
                    Man_Controlcs.SendControllerMsg(3, "C,1,1\n");
                if (globals.bCond2On)
                    Man_Controlcs.SendControllerMsg(3, "C,2,1\n");
                if (globals.bCond3On)
                    Man_Controlcs.SendControllerMsg(3, "C,3,1\n");
                if (globals.bCond4On)
                    Man_Controlcs.SendControllerMsg(3, "C,4,1\n");
                if (globals.bCond5On)
                    Man_Controlcs.SendControllerMsg(3, "C,5,1\n");
                if (globals.bCond6On)
                    Man_Controlcs.SendControllerMsg(3, "C,6,1\n");
                if (globals.bCond7On)
                    Man_Controlcs.SendControllerMsg(3, "C,7,1\n");
                if (globals.bCond8On)
                    Man_Controlcs.SendControllerMsg(3, "C,8,1\n");

                Thread.Sleep(100);
            }

        }
        /* 
         * Check and restart if necessary UV trityl Polling
         * 
         */
        private static async Task ClosePort()
        {
            Main_Form.UV_Trityl_Arduino.Close();
            await Task.Delay(60);
        }
        public static async Task<bool> CheckPolling()
        {
            byte [] UVreceive = new byte[100];

            if (globals.bDemoMode)
                return true;

            //if it is open make sure it is streaming...
            if (globals.bUVStreaming && Main_Form.UV_Trityl_Arduino.IsOpen)
            {
                //if not streaming, this string will remain empty
                string indata = string.Empty;

                try { indata = Main_Form.UV_Trityl_Arduino.ReadExisting(); } catch { }

                Man_Controlcs.SyncWait(250);

                Debug.WriteLine("First read" + indata);
                if (indata.Length > 3)
                {
                    await Man_Controlcs.AsyncWait(60);
                    return true;                                         // we are polling ok.
                }
                //repeat 
                try { indata = Main_Form.UV_Trityl_Arduino.ReadExisting(); } catch { }

                Man_Controlcs.SyncWait(250);

                Debug.WriteLine("Second read read" + indata);
                if (indata.Length > 3)
                {
                    await Man_Controlcs.AsyncWait(100);
                    return true;                                         // we are polling ok.
                }

                //try restarting polling
                //disconnect the call back
                Main_Form.UV_Trityl_Arduino.Handshake = Handshake.None;
                Main_Form.UV_Trityl_Arduino.DiscardInBuffer();
                await Man_Controlcs.AsyncWait(3400);

                //now close the port
                await ClosePort();
                await Man_Controlcs.AsyncWait(200);

                //now reopen it
                Main_Form.UV_Trityl_Arduino.Open();
                await Man_Controlcs.AsyncWait(300);

                globals.bUVStreaming = false;

                //now try to restart
                while (!globals.bUVStreaming)
                {
                    Man_Controlcs.WriteStatus("Monitor Stream", "Starting the UV Stream Monitor");
                    Man_Controlcs.SyncWait(20);
                    //check to make sure the port is open
                    if (!Main_Form.UV_Trityl_Arduino.IsOpen)
                    {
                        try
                        {
                            Main_Form.UV_Trityl_Arduino.Open();
                            Man_Controlcs.SyncWait(100);
                        }
                        catch { MessageBox.Show("Could Not Open Monitor \nPlease make sure it is attached.", "Start Monitoring"); return false; }
                        //stream may already be started, if not then start
                        if (globals.bUVStreaming)
                        {
                            if (Main_Form.UV_Trityl_Arduino.ReadExisting().Length == 0)
                                Main_Form.UV_Trityl_Arduino.Write("?\n");
                        }
                    }
                    else
                    {
                        int iBreak = 0;
                        int iStart = 0;

                        Main_Form.UV_Trityl_Arduino.Write("?\n");
                        Man_Controlcs.SyncWait(100);

                        while (Main_Form.UV_Trityl_Arduino.ReadExisting().Length < 10 && Main_Form.UV_Trityl_Arduino.IsOpen)
                        {
                            Man_Controlcs.SyncWait(800);
                            iBreak = iBreak + 1;
                            iStart = iStart + 1;

                            if (iStart > 10)
                            {
                                Main_Form.UV_Trityl_Arduino.Write("?\n"); iStart = 0;
                            }


                            if (iBreak > 30)
                                return false;
                        }
                        Man_Controlcs.SyncWait(800);

                        if (Main_Form.UV_Trityl_Arduino.ReadExisting().Length > 10)
                        { globals.bUVStreaming = true; return true; }

                    }
                }
            }
            //should be polling but port closed
            else if (globals.bUVStreaming && !Main_Form.UV_Trityl_Arduino.IsOpen)
            {
                //open the port 
                while (!globals.bUVStreaming)
                {
                    Man_Controlcs.WriteStatus("Monitor Stream", "Starting the UV Stream Monitor");
                    Man_Controlcs.SyncWait(20);
                    //check to make sure the port is open
                    if (!Main_Form.UV_Trityl_Arduino.IsOpen)
                    {
                        try
                        {
                            Main_Form.UV_Trityl_Arduino.Open();
                            Man_Controlcs.SyncWait(100);
                        }
                        catch { MessageBox.Show("Could Not Open Monitor \nPlease make sure it is attached.", "Start Monitoring"); return false; }
                        //stream may already be started, if not then start
                        if (globals.bUVStreaming)
                        {
                            if (Main_Form.UV_Trityl_Arduino.ReadExisting().Length == 0)
                                Main_Form.UV_Trityl_Arduino.Write("?\n");
                        }
                    }
                    else
                    {
                        int iBreak = 0;
                        int iStart = 0;

                        Main_Form.UV_Trityl_Arduino.Write("?\n");
                        Man_Controlcs.SyncWait(100);

                        while (Main_Form.UV_Trityl_Arduino.ReadExisting().Length < 10 && Main_Form.UV_Trityl_Arduino.IsOpen)
                        {
                            Man_Controlcs.SyncWait(800);
                            iBreak = iBreak + 1;
                            iStart = iStart + 1;

                            if (iStart > 10)
                            {
                                Main_Form.UV_Trityl_Arduino.Write("?\n"); iStart = 0;
                            }


                            if (iBreak > 30)
                                return false;
                        }
                        Man_Controlcs.SyncWait(800);

                        if (Main_Form.UV_Trityl_Arduino.ReadExisting().Length > 10)
                        { globals.bUVStreaming = true; return true; }

                    }
                }
            }
            else //doesn't matter we aren't polling anyway
                return true;

                //still no data - restart the controller
            return true;
         }

        



        /* Send the messages to control the valves
        
        */

        public static void SendControllerMsg(int Controller, string cntCommand)
        {
            switch (Controller)
            {
                case 1:
                    WriteStatus("Main controller", cntCommand.TrimEnd('\n'));
                    if (!globals.bIsMain)
                    {

                        try
                        {
                            //MessageBox.Show("I am here to Open");
                            if (!Main_Form.Main_Arduino.IsOpen  && !globals.bDemoMode)
                            {
                                Main_Form.Main_Arduino.Open();
                                Main_Form.Main_Arduino.Write("?\n");
                                string recd = Main_Form.Main_Arduino.ReadLine();
                                while (!recd.Contains("OK"))
                                {
                                    Man_Controlcs.SyncWait(40);
                                    recd = Main_Form.Main_Arduino.ReadLine();
                                }
                                    globals.bIsMain = true;
                            }
                        }
                        catch
                        {
                            try //again
                            {
                                //close it
                                Main_Form.Main_Arduino.Close();

                                //reopen it
                                Main_Form.Main_Arduino.Open();
                                globals.bIsMain = true;

                                // try a reset
                                Main_Form.Main_Arduino.Write("!\n");
                                Man_Controlcs.SyncWait(50);
                               
                                ReceiveStatus("Main Controller", Main_Form.Main_Arduino.ReadLine());
                            }
                            catch  //still can't open..going to waste a lot of steps.
                            {
                                //post error and  continue.. 
                                Man_Controlcs.WriteStatus("Error", "Could Not Open Main Controller - no Action Taken");
                            }
                        }
                    }

                    // it is open, write the message
                    try
                    {
                        //MessageBox.Show("I am here 1 - ", cntCommand);
                        if(!globals.bDemoMode && Main_Form.Main_Arduino.IsOpen)
                           Main_Form.Main_Arduino.Write(cntCommand);

                        Man_Controlcs.SyncWait(40);
                        string recd = String.Empty;

                        if (!globals.bDemoMode)
                            recd = Main_Form.Main_Arduino.ReadLine();
                       
                        //give a dive out
                        int j = 0;
                        while (!(recd.Contains("OK") || recd.Contains("Pumping")) && !globals.bDemoMode)
                        {
                            Thread.Sleep(15);
                            recd = Main_Form.Main_Arduino.ReadLine();
                            j++;
                            if (j > 10)
                            {
                               // MessageBox.Show("Here in loop");
                                recd = "OK";
                            }
                        }

                        ReceiveStatus("Main Controller", recd);
                    }
                    catch
                    {
                        try //again
                        {
                            //Close and Reopen the port
                            if(Main_Form.Main_Arduino.IsOpen)
                               Main_Form.Main_Arduino.Close();

                           Man_Controlcs.SyncWait(170);

                           Main_Form.Main_Arduino.Open();
                           Man_Controlcs.SyncWait(150);

                            //SOFT RESET THEN TRY
                            Main_Form.Main_Arduino.Write("!\n");
                            Man_Controlcs.SyncWait(100);
                            ReceiveStatus("Main Controller", Main_Form.Main_Arduino.ReadLine());

                            //try again
                            Main_Form.Main_Arduino.Write(cntCommand);
                            Man_Controlcs.SyncWait(100);

                            ReceiveStatus("Main Controller", Main_Form.Main_Arduino.ReadLine());
                        }
                       catch { Man_Controlcs.WriteStatus("Error", "Could Not Post the Command to Main Controller"); }
                    }
                    
                    //Main_Form.Main_Arduino.Close();  //leave it open always
                    //globals.bIsMain = false;
                    break;

                case 2:
                    WriteStatus("Trityl controller", cntCommand.TrimEnd('\n'));
                    //for UV Trityl Arduino
                    try
                    {
                        if (!Main_Form.UV_Trityl_Arduino.IsOpen || globals.bDemoMode)
                            return ;

                        //now write the command
                        Main_Form.UV_Trityl_Arduino.Write(cntCommand);
                        Man_Controlcs.SyncWait(50);

                        string UVreceive = string.Empty;
                        try { UVreceive = Main_Form.UV_Trityl_Arduino.ReadLine(); } catch { }
                        if (UVreceive.Length > 0)
                        {
                            if (UVreceive.Contains("OK"))
                                UVreceive = "OK";
                            else if (UVreceive.Contains("Sensor"))
                            {
                                Man_Controlcs.SyncWait(20);
                                //read another line
                                Main_Form.UV_Trityl_Arduino.ReadLine();
                                UVreceive = "OK";
                            }
                            else if (UVreceive.Contains("ERR"))
                            {
                                int cntr = 0;
                                while (cntr < 4 || !UVreceive.Contains("OK"))
                                {
                                    //resend the command
                                    Main_Form.UV_Trityl_Arduino.Write(cntCommand);
                                    Man_Controlcs.SyncWait(50);
                                    UVreceive = Main_Form.UV_Trityl_Arduino.ReadLine();
                                    cntr++;
                                }
                            }
                            else
                                UVreceive = "OK";
                        }

                        ReceiveStatus("UV Trityl Controller", UVreceive);
                    }
                    catch
                    {
                        try //again
                        {

                            //Close and Reopen the port
                            Main_Form.UV_Trityl_Arduino.Close();
                            Man_Controlcs.SyncWait(150);
                            Main_Form.UV_Trityl_Arduino.Open();
                            Man_Controlcs.SyncWait(150);

                            //try again
                            Main_Form.UV_Trityl_Arduino.Write(cntCommand);
                            Man_Controlcs.SyncWait(100);

                            ReceiveStatus("Main Controller", Main_Form.UV_Trityl_Arduino.ReadLine());

                            //Restart the Stream
                            Main_Form.UV_Trityl_Arduino.Write("?\n");
                            Man_Controlcs.SyncWait(100);


                        }
                        catch (Exception x) { MessageBox.Show("Error - " + x.ToString(), cntCommand); }
                    }
                    //make sure it is still streaming
                    Thread.Sleep(35);
                    int Joshua = 0;
                    string test = " ";
                    if (Main_Form.UV_Trityl_Arduino.IsOpen)
                    {
                        try { test = Main_Form.UV_Trityl_Arduino.ReadExisting(); } catch (Exception x) { MessageBox.Show("Error - " + x.ToString(), cntCommand); }
                        while (!test.Contains("Sensor") && Joshua < 4)
                        {
                            Main_Form.StartSensorData(1);
                            try { test = Main_Form.UV_Trityl_Arduino.ReadExisting(); } catch { }
                            Man_Controlcs.SyncWait(100);
                            if (Joshua > 3)
                                MessageBox.Show("Could not restart Strem", "UV Controller");
                            else
                                Joshua++;
                        }
                    }
                    break;


            }

        }


    }
}
