using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SeNA80
{
    public partial class SeNARun
    {
        static bool bWait = false;
        public bool bColsOpen = false;
        static bool bIsPush = false;
        public static bool bdwelling = false;

        /* Must parse the commands and then open the valves
         * and perform the procedures described in each line
         * of the protocols....
         * The structure is Reagent, Bypass/Col, Which Col, Bypass/Pump, Sec/uL
         */
        private int PumpWait(string sVol)
        {
            int iVol = int.Parse(sVol);

            double pulses = (double)iVol / 2.5;

            double wTime = pulses * 10.5;

            return ((int)Math.Round(wTime));
        }
        /// <summary>
        /// Main_PumpingHandler
        /// Dynamic Event handler that will be created when pumping starts and be killed when pumping stops to monitor pumping actions </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Main_PumpingHandler(object sender, SerialDataReceivedEventArgs e)
        {

            Man_Controlcs.SyncWait(20);
            string indata = Main_Form.Main_Arduino.ReadExisting();
            if (string.IsNullOrEmpty(indata))
                return;

            if (indata.Contains("Pumping"))
            {
                bWait = true;
                globals.bPumping = true;
            }

            if (indata.Contains("Done"))
            {
                Man_Controlcs.WriteStatus("Main", "Pumping Stopped");
                Debug.WriteLine("Attached Found Done....");
                Man_Controlcs.SyncWait(60);
                bWait = false;
                globals.bPumping = false;
            }

        }
        private async Task PumpHangOut()
        {
            Stopwatch stopwatch = new Stopwatch();

            if (globals.bDemoMode)
            {
                stopwatch.Start();

                //check the stopwatch
                Debug.WriteLine("Stopwatch Started at  = " + DateTime.Now.ToString());

                while (globals.bPumping)
                {
                    //bail out if wait more than 3 seconds
                    if (stopwatch.ElapsedMilliseconds > 3000)
                    {
                        //check the stopwatch
                        Debug.WriteLine("Stopwatch Elapsed ms = " + stopwatch.ElapsedMilliseconds.ToString());

                        stopwatch.Stop();
                        Man_Controlcs.WriteStatus("Main", "Did not get pump Done from controller...");
                        bWait = false;
                        globals.bPumping = false;
                        return;
                    }
                }

            }

            SafeSetStatus("Waiting for pumping to complete....");
            bWait = true;

            //create a "bailout" stopwatch
            stopwatch.Start();

            //add an event handler to wait for input from main (note: we have a permanent event handler for Trityl)
            //Main_Form.Main_Arduino.DataReceived += new SerialDataReceivedEventHandler(Main_PumpingHandler);

            Man_Controlcs.WriteStatus("Main", "Pumping Started");
            Man_Controlcs.SyncWait(100);

            //very small amounts done quickly
            string done = string.Empty;

            if (!globals.bDemoMode)
                done = Main_Form.Main_Arduino.ReadExisting();

            Debug.WriteLine("Done return " + done);

            if (!done.Contains("Done"))
            {
                //now just hang out asynchronously until pumping done
                while (globals.bPumping && !bTerminateEndofStep)
                {

                    await Man_Controlcs.AsyncWait(100);

                    if (Main_Form.Main_Arduino.IsOpen)
                    {
                        done = Main_Form.Main_Arduino.ReadExisting();

                        if (done.Contains("Done"))
                        {
                            SafeSetStatus("Pump Waiting Complete....");
                            Man_Controlcs.WriteStatus("Main", "Pumping Complete in  " + stopwatch.ElapsedMilliseconds.ToString() + "ms");
                            Debug.WriteLine("Done is - " + done);
                            await Man_Controlcs.AsyncWait(500);   //give it some extra time.
                            globals.bPumping = false;
                            break;
                        }
                    }
                    else
                    {
                        Man_Controlcs.WriteStatus("Main", "Controller Not Open, Did not get pump Done response...");
                        bWait = false;
                        globals.bPumping = false;
                        stopwatch.Stop();
                        break;
                    }

                    //check the stopwatch
                    Debug.WriteLine("Stopwatch Elapsed ms = " + stopwatch.ElapsedMilliseconds.ToString());

                    //bail out if wait more than 5 seconds
                    if (stopwatch.ElapsedMilliseconds > 8000) //pumping out takes 16 seconds
                    {
                        stopwatch.Stop();
                        Man_Controlcs.WriteStatus("Main", "Did not get pump Done from controller...");
                        bWait = false;
                        await Man_Controlcs.AsyncWait(500);
                        globals.bPumping = false;
                        break;
                    }
                }
            }
            else
            {
                Man_Controlcs.WriteStatus("Main", "Did not get pump Done from controller, Port Closed...");
                SafeSetStatus("Did not get response from Pump, port closed....");
                stopwatch.Stop();
                globals.bPumping = false;
            }

            //release the event handler to wait for input from main (note: we have a permanent event handler for Trityl)
            //Main_Form.Main_Arduino.DataReceived -= new SerialDataReceivedEventHandler(Main_PumpingHandler);
            Debug.WriteLine("Done Pumping");

            Debug.WriteLine("Pumping time is - " + stopwatch.ElapsedMilliseconds.ToString("0"));
            bWait = false;
            Man_Controlcs.SyncWait(500);
            globals.bPumping = false;
            stopwatch.Stop();
            Man_Controlcs.WriteStatus("Pump", "Pumping Completed in - " + stopwatch.ElapsedMilliseconds.ToString("0"));
            SafeSetStatus("Pump Waiting Complete....");
        }
        public void SelectCols(string colString)
        {
            SafeSetStatus("Opening Columns....");
            bColsOpen = true;
            //MessageBox.Show(colString, "Here");

            //if coupling
            if (globals.bCoupling)
            {
                if (!(rb_Smart.Checked))
                {
                    switch (globals.iCoupCol)
                    {
                        case 1:
                            SafeSetStatus("Opened Column 1...");
                            ColorButton(rb_C1, 1);

                            Man_Controlcs.SendControllerMsg(1, valves.col1on);
                            break;
                        case 2:
                            SafeSetStatus("Opened Column 2...");
                            ColorButton(rb_C2, 1);

                            Man_Controlcs.SendControllerMsg(1, valves.col2on);
                            break;
                        case 3:
                            SafeSetStatus("Opened Column 3...");
                            ColorButton(rb_C3, 1);

                            Man_Controlcs.SendControllerMsg(1, valves.col3on);
                            break;
                        case 4:
                            SafeSetStatus("Opened Column 4...");
                            ColorButton(rb_C4, 1);

                            Man_Controlcs.SendControllerMsg(1, valves.col4on);
                            break;
                        case 5:
                            SafeSetStatus("Opened Column 5...");
                            ColorButton(rb_C5, 1);

                            Man_Controlcs.SendControllerMsg(1, valves.col5on);
                            break;
                        case 6:
                            SafeSetStatus("Opened Column 6...");
                            ColorButton(rb_C6, 1);

                            Man_Controlcs.SendControllerMsg(1, valves.col6on);
                            break;
                        case 7:
                            SafeSetStatus("Opened Column 7...");
                            ColorButton(rb_C7, 1);

                            Man_Controlcs.SendControllerMsg(1, valves.col7on);
                            break;
                        case 8:
                            SafeSetStatus("Opened Column 8...");
                            ColorButton(rb_C8, 1);

                            Man_Controlcs.SendControllerMsg(1, valves.col8on);
                            break;
                    }
                    SyncWait(60);
                }
                else
                {
                    foreach (int c in SeNARun.sameBase)
                    {
                        switch (c)
                        {
                            case 1:
                                SafeSetStatus("Opened Column 1...");
                                ColorButton(rb_C1, 1);

                                Man_Controlcs.SendControllerMsg(1, valves.col1on);
                                break;
                            case 2:
                                SafeSetStatus("Opened Column 2...");
                                ColorButton(rb_C2, 1);

                                Man_Controlcs.SendControllerMsg(1, valves.col2on);
                                break;
                            case 3:
                                SafeSetStatus("Opened Column 3...");
                                ColorButton(rb_C3, 1);

                                Man_Controlcs.SendControllerMsg(1, valves.col3on);
                                break;
                            case 4:
                                SafeSetStatus("Opened Column 4...");
                                ColorButton(rb_C4, 1);

                                Man_Controlcs.SendControllerMsg(1, valves.col4on);
                                break;
                            case 5:
                                SafeSetStatus("Opened Column 5...");
                                ColorButton(rb_C5, 1);

                                Man_Controlcs.SendControllerMsg(1, valves.col5on);
                                break;
                            case 6:
                                SafeSetStatus("Opened Column 6...");
                                ColorButton(rb_C6, 1);

                                Man_Controlcs.SendControllerMsg(1, valves.col6on);
                                break;
                            case 7:
                                SafeSetStatus("Opened Column 7...");
                                ColorButton(rb_C7, 1);

                                Man_Controlcs.SendControllerMsg(1, valves.col7on);
                                break;
                            case 8:
                                SafeSetStatus("Opened Column 8...");
                                ColorButton(rb_C8, 1);

                                Man_Controlcs.SendControllerMsg(1, valves.col8on);
                                break;
                        }
                        SyncWait(60);

                    }
                }


                SafeSetStatus("Open Column Complete...");
            }

            //open the column we are coupling
            else if (bLooping)
            {
                if (globals.bCol1 && ShouldOpen(1))
                {
                    SafeSetStatus("Opened Column 1...");
                    ColorButton(rb_C1, 1);

                    Man_Controlcs.SendControllerMsg(1, valves.col1on);
                    SyncWait(40);
                }

                if (globals.bCol2 && ShouldOpen(2))
                {
                    SafeSetStatus("Opened Column 2...");
                    ColorButton(rb_C2, 1);

                    Man_Controlcs.SendControllerMsg(1, valves.col2on);
                    SyncWait(40);
                }

                if (globals.bCol3 && ShouldOpen(3))
                {
                    SafeSetStatus("Opened Column 3...");
                    ColorButton(rb_C3, 1);

                    Man_Controlcs.SendControllerMsg(1, valves.col3on);
                    SyncWait(40);
                }

                if (globals.bCol4 && ShouldOpen(4))
                {
                    SafeSetStatus("Opened Column 4...");
                    ColorButton(rb_C4, 1);

                    Man_Controlcs.SendControllerMsg(1, valves.col4on);
                    SyncWait(40);
                }

                if (globals.bCol5 && ShouldOpen(5))
                {
                    SafeSetStatus("Opened Column 5...");
                    ColorButton(rb_C5, 1);

                    Man_Controlcs.SendControllerMsg(1, valves.col5on);
                    SyncWait(40);
                }

                if (globals.bCol6 && ShouldOpen(6))
                {
                    SafeSetStatus("Opened Column 6...");
                    ColorButton(rb_C6, 1);

                    Man_Controlcs.SendControllerMsg(1, valves.col6on);
                    SyncWait(40);
                }
                if (globals.bCol7 && ShouldOpen(7))
                {
                    SafeSetStatus("Opened Column 7...");
                    ColorButton(rb_C7, 1);

                    Man_Controlcs.SendControllerMsg(1, valves.col7on);
                    SyncWait(40);
                }

                if (globals.bCol8 && ShouldOpen(8))
                {
                    SafeSetStatus("Opened Column 8...");
                    ColorButton(rb_C8, 1);

                    Man_Controlcs.SendControllerMsg(1, valves.col8on);
                    SyncWait(40);
                }
                SafeSetStatus("Open Column Complete...");
            }
            else if (!globals.bCoupling && !bLooping)
            {
                char[] cols = colString.ToCharArray();

                if (globals.bCol1 || cols[0] == '1')
                {
                    SafeSetStatus("Opened Column 1...");
                    ColorButton(rb_C1, 1);

                    Man_Controlcs.SendControllerMsg(1, valves.col1on);
                    SyncWait(40);
                }

                if (globals.bCol2 || cols[1] == '1')
                {
                    SafeSetStatus("Opened Column 2...");
                    ColorButton(rb_C2, 1);

                    Man_Controlcs.SendControllerMsg(1, valves.col2on);
                    SyncWait(40);
                }

                if (globals.bCol3 || cols[2] == '1')
                {
                    SafeSetStatus("Opened Column 3...");
                    ColorButton(rb_C3, 1);

                    Man_Controlcs.SendControllerMsg(1, valves.col3on);
                    SyncWait(40);
                }

                if (globals.bCol4 || cols[3] == '1')
                {
                    SafeSetStatus("Opened Column 4...");
                    ColorButton(rb_C4, 1);

                    Man_Controlcs.SendControllerMsg(1, valves.col4on);
                    SyncWait(40);
                }

                if (globals.bCol5 || cols[4] == '1')
                {
                    SafeSetStatus("Opened Column 5...");
                    ColorButton(rb_C5, 1);

                    Man_Controlcs.SendControllerMsg(1, valves.col5on);
                    SyncWait(40);
                }

                if (globals.bCol6 || cols[5] == '1')
                {
                    SafeSetStatus("Opened Column 6...");
                    ColorButton(rb_C6, 1);

                    Man_Controlcs.SendControllerMsg(1, valves.col6on);
                    SyncWait(40);
                }
                if (globals.bCol7 || cols[6] == '1')
                {
                    SafeSetStatus("Opened Column 7...");
                    ColorButton(rb_C7, 1);

                    Man_Controlcs.SendControllerMsg(1, valves.col7on);
                    SyncWait(40);
                }

                if (globals.bCol8 || cols[7] == '1')
                {
                    SafeSetStatus("Opened Column 8...");
                    ColorButton(rb_C8, 1);

                    Man_Controlcs.SendControllerMsg(1, valves.col8on);
                    SyncWait(40);
                }
                SafeSetStatus("Open Column Complete...");

            }

            return;
        }
        private bool ShouldOpen(int col)
        {
            string seq = string.Empty;

            switch (col)
            {
                case 1:
                    seq = tb_col1.Text;
                    break;
                case 2:
                    seq = tb_col2.Text;
                    break;
                case 3:
                    seq = tb_col3.Text;
                    break;
                case 4:
                    seq = tb_col4.Text;
                    break;
                case 5:
                    seq = tb_col5.Text;
                    break;
                case 6:
                    seq = tb_col6.Text;
                    break;
                case 7:
                    seq = tb_col7.Text;
                    break;
                case 8:
                    seq = tb_col8.Text;
                    break;
            }

            if (globals.i12Ltr > 0 && (!globals.bOxidizing && !globals.bThiolating))
            {
                //test for oxidation versus thiolation
                if (!(seq.Length > (globals.iCycle * 3)))
                    return false;
            }
            else
            {
                if (!(seq.Length > globals.iCycle))
                    return false;
            }

            //test for oxidation versus thiolation
            if (globals.i12Ltr > 0)
            {
                //if done leave
                if (!(seq.Length > (globals.iCycle * 3)))
                    return false;

                char[] test = seq.ToCharArray();
                if (globals.bOxidizing)
                {
                    if (test[(globals.iCycle * 3) + 2] == '-')
                        return true;
                    else
                        return false;
                }
                else if (globals.bThiolating)
                {
                    if (test[(globals.iCycle * 3) + 2] == '+' || test[(globals.iCycle * 3) + 2] == 's')
                        return true;
                    else
                        return false;
                }
            }
            else
            {
                //can only oxidize
                if (!(seq.Length > globals.iCycle))
                    return false;
            }

            return true;
        }
        //Close the columns when done

        public void CloseCols(string colString)
        {
            //if coupling
            SafeSetStatus("Closing Columns...");
            if (globals.bCoupling)
            {
                if (!(rb_Smart.Checked))
                {
                    switch (globals.iCoupCol)
                    {
                        case 1:
                            SafeSetStatus("Closing Column 1...");
                            ColorButton(rb_C1, 0);

                            Man_Controlcs.SendControllerMsg(1, valves.col1off);
                            break;
                        case 2:
                            SafeSetStatus("Closing Column 2...");
                            ColorButton(rb_C2, 0);

                            Man_Controlcs.SendControllerMsg(1, valves.col2off);
                            break;
                        case 3:
                            SafeSetStatus("Closing Column 3...");
                            ColorButton(rb_C3, 0);

                            Man_Controlcs.SendControllerMsg(1, valves.col3off);
                            break;
                        case 4:
                            SafeSetStatus("Closing Column 4...");
                            ColorButton(rb_C4, 0);

                            Man_Controlcs.SendControllerMsg(1, valves.col4off);
                            break;
                        case 5:
                            SafeSetStatus("Closing Column 5...");
                            ColorButton(rb_C5, 0);

                            Man_Controlcs.SendControllerMsg(1, valves.col5off);
                            break;
                        case 6:
                            SafeSetStatus("Closing Column 6...");
                            ColorButton(rb_C6, 0);

                            Man_Controlcs.SendControllerMsg(1, valves.col6off);
                            break;
                        case 7:
                            SafeSetStatus("Closing Column 7...");
                            ColorButton(rb_C7, 0);

                            Man_Controlcs.SendControllerMsg(1, valves.col7off);
                            break;
                        case 8:
                            SafeSetStatus("Closing Column 8...");
                            ColorButton(rb_C8, 0);

                            Man_Controlcs.SendControllerMsg(1, valves.col8off);
                            break;
                    }
                    SyncWait(60);
                }
                else
                {
                    foreach (int c in SeNARun.sameBase)
                    {
                        switch (c)
                        {
                            case 1:
                                SafeSetStatus("Closing Column 1...");
                                ColorButton(rb_C1, 0);

                                Man_Controlcs.SendControllerMsg(1, valves.col1off);
                                break;
                            case 2:
                                SafeSetStatus("Closing Column 2...");
                                ColorButton(rb_C2, 0);

                                Man_Controlcs.SendControllerMsg(1, valves.col2off);
                                break;
                            case 3:
                                SafeSetStatus("Closing Column 3...");
                                ColorButton(rb_C3, 0);

                                Man_Controlcs.SendControllerMsg(1, valves.col3off);
                                break;
                            case 4:
                                SafeSetStatus("Closing Column 4...");
                                ColorButton(rb_C4, 0);

                                Man_Controlcs.SendControllerMsg(1, valves.col4off);
                                break;
                            case 5:
                                SafeSetStatus("Closing Column 5...");
                                ColorButton(rb_C5, 0);

                                Man_Controlcs.SendControllerMsg(1, valves.col5off);
                                break;
                            case 6:
                                SafeSetStatus("Closing Column 6...");
                                ColorButton(rb_C6, 0);

                                Man_Controlcs.SendControllerMsg(1, valves.col6off);
                                break;
                            case 7:
                                SafeSetStatus("Closing Column 7...");
                                ColorButton(rb_C7, 0);

                                Man_Controlcs.SendControllerMsg(1, valves.col7off);
                                break;
                            case 8:
                                SafeSetStatus("Closing Column 8...");
                                ColorButton(rb_C8, 0);

                                Man_Controlcs.SendControllerMsg(1, valves.col8off);
                                break;
                        }
                        SyncWait(60);
                    }
                }
                SafeSetStatus("Closing Columns Complete...");
            }

            //open the column we are coupling
            else if (bLooping)
            {
                if (globals.bCol1)
                {
                    SafeSetStatus("Closing Column 1...");
                    ColorButton(rb_C1, 0);

                    Man_Controlcs.SendControllerMsg(1, valves.col1off);
                    SyncWait(40);
                }

                if (globals.bCol2)
                {
                    SafeSetStatus("Closing Column 2...");
                    ColorButton(rb_C2, 0);

                    Man_Controlcs.SendControllerMsg(1, valves.col2off);
                    SyncWait(40);
                }
                if (globals.bCol3)
                {
                    SafeSetStatus("Closing Column 3...");
                    ColorButton(rb_C3, 0);

                    Man_Controlcs.SendControllerMsg(1, valves.col3off);
                    SyncWait(40);
                }
                if (globals.bCol4)
                {
                    SafeSetStatus("Closing Column 4...");
                    ColorButton(rb_C4, 0);

                    Man_Controlcs.SendControllerMsg(1, valves.col4off);
                    SyncWait(40);
                }
                if (globals.bCol5)
                {
                    SafeSetStatus("Closing Column 5...");
                    ColorButton(rb_C5, 0);

                    Man_Controlcs.SendControllerMsg(1, valves.col5off);
                    SyncWait(40);
                }
                if (globals.bCol6)
                {
                    SafeSetStatus("Closing Column 6...");
                    ColorButton(rb_C6, 0);

                    Man_Controlcs.SendControllerMsg(1, valves.col6off);
                    SyncWait(40);
                }
                if (globals.bCol7)
                {
                    SafeSetStatus("Closing Column 7...");
                    ColorButton(rb_C7, 0);

                    Man_Controlcs.SendControllerMsg(1, valves.col7off);
                    SyncWait(40);
                }
                if (globals.bCol8)
                {
                    SafeSetStatus("Closing Column 8...");
                    ColorButton(rb_C8, 0);

                    Man_Controlcs.SendControllerMsg(1, valves.col8off);
                    SyncWait(40);
                }
                SafeSetStatus("Closing Columns Complete...");
            }
            else
            {
                ColorButton(rb_C1, 0);
                ColorButton(rb_C2, 0);
                ColorButton(rb_C3, 0);
                ColorButton(rb_C4, 0);
                ColorButton(rb_C5, 0);
                ColorButton(rb_C6, 0);
                ColorButton(rb_C7, 0);
                ColorButton(rb_C8, 0);

                Man_Controlcs.SendControllerMsg(1, valves.col1off);
                SyncWait(40);
                Man_Controlcs.SendControllerMsg(1, valves.col2off);
                SyncWait(40);
                Man_Controlcs.SendControllerMsg(1, valves.col3off);
                SyncWait(40);
                Man_Controlcs.SendControllerMsg(1, valves.col4off);
                SyncWait(40);
                Man_Controlcs.SendControllerMsg(1, valves.col5off);
                SyncWait(40);
                Man_Controlcs.SendControllerMsg(1, valves.col6off);
                SyncWait(40);
                Man_Controlcs.SendControllerMsg(1, valves.col7off);
                SyncWait(40);
                Man_Controlcs.SendControllerMsg(1, valves.col8off);
                SyncWait(40);
            }

            return;
        }
        public void DoCOWaste(string command)
        {
            //if pumping, the firmware will retake control of the recyle(Waste) valve when finished
            SafeSetStatus("Opening Column to Waste...");
            globals.bFluidicsBusy = true;

            int iOnOff = int.Parse(command);

            if (iOnOff != 1)  //close = 0 (turn on the recycle valve)
            {
                //ColorButton(rb_3WWR, 1);

                Man_Controlcs.WriteStatus("Recycle Vavle", "Recycle Valve Closed");
                SafeSetStatus("Recycle Valve Closed....");
                //Man_Controlcs.SendControllerMsg(1, valves.recycleon);
                SyncWait(180);
                globals.bRecycleOn = true;
            }
            else   // open = 1 (turn off the recycle valve)
            {
                //ColorButton(rb_3WWR, 0);

                Man_Controlcs.WriteStatus("Recycle Valve", "Recycle Valve Open");
                SafeSetStatus("Recycle Valve Open...");
                // Man_Controlcs.SendControllerMsg(1, valves.recycleoff);
                SyncWait(180);   //may be pressure backed up
                globals.bRecycleOn = false;
            }
            globals.bFluidicsBusy = false;
            SafeSetStatus("Column to Waste Done... ");
        }
        public async void DoRecycle(string command)
        {
            SafeSetStatus("Starting Recycling...");
            bool bAlreadyOn = false;

            //set auto recycle valve control to off
            ColorButton(rb_recycle, 1);

            globals.bFluidicsBusy = true;

            string[] Cmdsplit = command.Split(',');

            //check to make sure we have at least 200uL in the pump
            if (globals.iAmPumpVol < 100)
            {
                Man_Controlcs.WriteStatus("Pump", "Can not recycle with less than 100uL");
                return;
            }

            //select columns
            if (!globals.bColBypass)
            {
                SafeSetStatus("Amidite Recycle to Columns...");
                Man_Controlcs.WriteStatus("Amidite Recycle", "To Columns Selected");
                ColorButton(rb_3WBC, 1);
                rb_3WBC.Text = "To Column";

                Man_Controlcs.SendControllerMsg(1, valves.tocol);   //bypass selected
                SyncWait(100);

                globals.bColBypass = true;
                SafeSetStatus("Column Selected...");
            }
            SyncWait(60);

            Man_Controlcs.WriteStatus("Amidite Recycle", "Active Column(s) Selected");
            SafeSetStatus("Amidite Recycle Active Column Selected.....");
            SelectCols("000000001"); //select which open
            SyncWait(200);

            //now open to pumps
            if (globals.bPumpBypass)
            {
                SafeSetStatus("Amidite Recycle to Pump Selected...");
                ColorButton(rb_3WBP, 1);
                rb_3WBP.Text = "To Pump";

                Man_Controlcs.WriteStatus("Amidite Recycle", "Pump/Bypass - To Pump Selected");
                Man_Controlcs.SendControllerMsg(1, valves.topump); //pump is off and bypass is on
                SyncWait(60);

                globals.bPumpBypass = false;
                SafeSetStatus("To Pump Selected...");
            }

            //make sure flow is to pump
            await Man_Controlcs.AsyncWait(100);

            //now select the amidite pump
            if (!globals.bAmidReagPump)
            {
                SafeSetStatus("Amdite Recycle Amidite Pump Selected..");
                Man_Controlcs.WriteStatus("Amidite Recycle", "Pump Selection - Amidite Pump Selected");
                //ColorButton(rb_3WRA, 1);

                //     Man_Controlcs.SendControllerMsg(1, valves.toamidpump);
                SyncWait(60);
                globals.bAmidReagPump = true;
                SafeSetStatus("Amidite Pump Selected...");
            }

            SyncWait(60);
            //make sure the recycle valve is closed
            SafeSetStatus("Amidite Recycle Selecting Recycle Valve On..");

            Man_Controlcs.WriteStatus("Amidite Recycle", "Recycle Valve Turned On");
            //ColorButton(rb_3WWR, 1);

            // Man_Controlcs.SendControllerMsg(1, valves.recycleon);
            SyncWait(60);
            globals.bRecycleOn = true;

            SyncWait(100);

            //*********NOW RECYCLE ***********************//
            //initialize the pump
            SafeSetStatus("Starting Recycle..");
            double dwell = globals.iAmPumpVol * 1.2;
            int iPW = PumpWait(dwell.ToString("0"));
            string cmd = "P," + globals.iAmPumpVol.ToString("0") + "\n";

            ColorButton(rb_RgPump, 1);

            globals.bPumping = true;
            Man_Controlcs.WriteStatus("Amidite Recycle", "Pump Empty");
            SafeSetStatus("Amidite Recycle Pushing to Column.....");
            Man_Controlcs.SendControllerMsg(1, "P,0\n");  //initialize the amidite pump


            //wait for the pump to finish pumping
            Task tUp = Task.Run(async () => { await PumpHangOut(); });

            while (globals.bPumping)
            {
                await PutTaskDelay(50);
                if (tUp.IsCompleted) globals.bPumping = false;
            }
            ColorButton(rb_RgPump, 1);

            int iTime = Convert.ToInt32(Cmdsplit[1]);
            int iCycles = 0;

            Stopwatch watch = new Stopwatch();
            watch.Start();

            bool bRunning = true;

            while (bRunning)
            {
                SafeSetStatus("Recycling Pulling back to Pump..");
                string t = globals.iAmPumpVol.ToString("0").Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty).Replace("\0", String.Empty);
                Man_Controlcs.WriteStatus("Amidite Recycle", "Pump Fill with " + int.Parse(t).ToString("0") + "uL");
                ColorButton(rb_RgPump, 1);

                globals.bPumping = true;
                Man_Controlcs.SendControllerMsg(1, cmd);

                //wait for the pump to finish pumping
                Task tDown = Task.Run(async () => { await PumpHangOut(); });

                while (globals.bPumping)
                {
                    await PutTaskDelay(50);
                    if (tDown.IsCompleted) globals.bPumping = false;
                }

                ColorButton(rb_RgPump, 0);

                //now dwell to fill the pump with the valves open
                if (globals.iReagPumpDwell > 0)
                {
                    bdwelling = true;

                    Task tDwell = Task.Run(async () => { await DwellPump(globals.iAmPumpVol); });

                    while (bdwelling)
                    {
                        await PutTaskDelay(50);
                        if (tDwell.IsCompleted) { bdwelling = false; }
                    }
                    SyncWait(50);
                }

                SyncWait(50);  //extra time for pressure to stabilize

                //don't need this much time...
                //await PutTaskDelay(iPW);

                Man_Controlcs.WriteStatus("Amidite Recycle", "Pump Empty");
                SafeSetStatus("Amidite Recycle Pushing to Column.....");
                ColorButton(rb_RgPump, 1);

                globals.bPumping = true;
                Man_Controlcs.SendControllerMsg(1, "Q,0\n");  //initialize the amidite pump

                //wait for the pump to finish pumping
                Task tUp2 = Task.Run(async () => { await PumpHangOut(); });

                while (globals.bPumping)
                {
                    Man_Controlcs.SyncWait(100);
                    if (tUp2.IsCompleted) globals.bPumping = false;
                }
                ColorButton(rb_RgPump, 0);

                SyncWait(100);  //extra time for pressure to stabilize

                iCycles++;
                //check for current tick count
                if (watch.Elapsed.TotalSeconds > iTime)
                    bRunning = false;

            }
            watch.Stop();
            SafeSetStatus("Recycling Complete..");
            //*************END RECYCLE***************//
            //draw one more syring full...release pressure from the lines
            string s = globals.iAmPumpVol.ToString("0").Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty).Replace("\0", String.Empty);
            Man_Controlcs.WriteStatus("Amidite Recycle", "Pump Fill with " + int.Parse(s).ToString("0") + "uL");

            SafeSetStatus("Amidite Recycle Pulling to Pump.....");
            ColorButton(rb_RgPump, 1);

            globals.bPumping = true;
            Man_Controlcs.SendControllerMsg(1, cmd);

            //wait for the pump to finish pumping
            Task tDown2 = Task.Run(async () => { await PumpHangOut(); });

            while (globals.bPumping)
            {
                Man_Controlcs.SyncWait(100);
                if (tDown2.IsCompleted) globals.bPumping = false;
            }
            ColorButton(rb_RgPump, 0);

            //Send Message Done
            Man_Controlcs.WriteStatus("Recycle", "Recycled for " + iCycles.ToString("0") + " Cycles");
            SafeSetStatus("Amidite Recycled for " + iCycles.ToString("0") + " Cycles");
            //flip the recycle valve
            if (globals.bRecycleOn && !bAlreadyOn)  //if it was already closed, leave it closed
            {
                //ColorButton(rb_3WWR, 0);

                Man_Controlcs.WriteStatus("Amidite Recycle", "Recycle Valve Opened");
                SafeSetStatus("Recycle Valve Open");
                //dont open it let the pump control it
                //Man_Controlcs.SendControllerMsg(1, valves.recycleoff);
                globals.bRecycleOn = false;
            }
            SyncWait(100);

            //reselect reagent pump
            //now select the amidite pump
            if (!globals.bAmidReagPump && !bAlreadyOn)
            {
                Man_Controlcs.WriteStatus("Amidite Recycle", "Pump Selection - To Reagent Pump");
                SafeSetStatus("Reagent Pump Selected");
                //ColorButton(rb_3WRA, 0);

                //     Man_Controlcs.SendControllerMsg(1, valves.toreagpump);
                globals.bAmidReagPump = false;
            }
            SyncWait(100);

            //leave in off position -- to pumps
            if (globals.bPumpBypass)
            {
                ColorButton(rb_3WBP, 1);
                rb_3WBP.Text = "To Pump";

                Man_Controlcs.WriteStatus("Amidite Recycle", "Pump/Bypass - To Pump Selected");
                SafeSetStatus("Pump Bypass to Pump Selected");
                Man_Controlcs.SendControllerMsg(1, valves.topump); //pump is off and bypass is on
                globals.bPumpBypass = false;
            }
            SyncWait(100);

            if (globals.bColBypass)
            {
                ColorButton(rb_3WBC, 1);
                rb_3WBC.Text = "To Column";

                Man_Controlcs.WriteStatus("Amidite Recycle", "Cols/Bypass - To columns Selected");
                SafeSetStatus("Column Bypass to Columns Selected");
                Man_Controlcs.SendControllerMsg(1, valves.tocolbypass);   //columns selected
                globals.bColBypass = false;
            }
            SafeSetStatus("Closing Columns..");
            Man_Controlcs.WriteStatus("Amidite Recycle", "All Columns Closed");
            SafeSetStatus("All Columns Closed");
            CloseCols("111111111"); //select which open


            rb_recycle.BackColor = System.Drawing.Color.Red;
            this.rb_recycle.ForeColor = System.Drawing.Color.Black;

            globals.bFluidicsBusy = false;
            SafeSetStatus("Recycle Complete..");

        }

        public async void DoPumpInit(string Init)
        {
            SafeSetStatus("Initializing Pump..");

            //the pumps go to waste so no other valve switching is required.
            Man_Controlcs.WriteStatus("Pump Init", "Reagent Pump Initialized");
            SafeSetStatus("Reagent Pump Initialized");
            ColorButton(rb_RgPump, 1);

            //Init reagent pump
            globals.bPumping = true;
            Man_Controlcs.SendControllerMsg(1, valves.reagPumpInit);

            Debug.WriteLine("About to Start Hangout....");

            //wait for the pump to finish pumping
            Task tUp = Task.Run(async () => { await PumpHangOut(); });

            while (globals.bPumping)
            {
                await Man_Controlcs.AsyncWait(100);
                if (tUp.IsCompleted) globals.bPumping = false;
            }

            Debug.WriteLine("Finished Hangout....");
            //additional second for initialization to complete
            Man_Controlcs.SyncWait(1200);
            ColorButton(rb_RgPump, 0);

            globals.iAmPumpVol = 0;

            SafeSetStatus("Pump Initialization Complete..");
            globals.bFluidicsBusy = false;
        }
        public void ByPassTime(string howlong)
        {
            SafeSetStatus("Waiting.....");
            int wSecWait = int.Parse(howlong);

            globals.bWaiting = true;
            bWait = true;

            //set the interval based on the length of the wait
            //this is just used to update the status boxes...the other timer is used
            //to keep time
            if (wSecWait < 17)
                globals.runform.RunTimer.Interval = 1000;    // every 1 second
            else if (wSecWait >= 17 && wSecWait < 90)
                globals.runform.RunTimer.Interval = 10000;  //every 10 seconds
            else if (wSecWait >= 90 && wSecWait < 600)
                globals.runform.RunTimer.Interval = 30000;  //every 30 seconds
            else
                globals.runform.RunTimer.Interval = 60000;  //every 60 seconds

            globals.runform.RunTimer.Start();

            globals.start_time = DateTime.Now;
            int segundos = int.Parse(howlong);

            //create a separate time for the wait
            //we sit on this one until the time desired...
            int wTime = segundos * 1000;


            globals.start_time = DateTime.Now;
            if (segundos < 1) { globals.runform.RunTimer.Stop(); globals.bWaiting = false; bWait = false; return; }
            //to see which works better, either the timer inside of a timer or 
            //this while loop with applicaiton do events()
            DateTime _desired = DateTime.Now.AddSeconds(segundos);
            while (DateTime.Now < _desired)
            {
                System.Windows.Forms.Application.DoEvents();
            }

            globals.runform.RunTimer.Stop();
            bWait = false;
            globals.bWaiting = false;
            SafeSetStatus("Waiting Complete.....");

        }
        /// <summary>
        /// SyncWait
        /// </summary>
        /// Non blocking wait routine...It is a countdown timer that is on the main thread
        /// but uses AppDoEvents() to refresh the main thread every millisecond
        /// <param name="waitfor">milliseconds to wait</param>
        public void SyncWait(double waitfor)
        {
            if (waitfor < 5) { return; }

            DateTime _desired = DateTime.Now.AddMilliseconds(waitfor);


            while (DateTime.Now < _desired)
            {
                System.Windows.Forms.Application.DoEvents();
            }
        }
        /// <summary>
        /// Do Comment - For comments just put the text into the Status Box so that it is written
        ///              ToolBar the log file for full documentation of the run
        /// </summary>
        /// <param name="Comment">string of comment</param>
        public void DoComment(string Comment)
        {
            Man_Controlcs.WriteStatus(" Comment", Comment);
            SyncWait(50);

            SafeSetStatus(Comment);
            globals.bWaiting = false;
        }
        /// <summary>
        /// Do Wash - Do command to deliver wash A during a protocol
        /// controls the valves and delivers via pump or pressure the desired amount
        /// </summary>
        /// <param name="command">the command string</param>
        public void DoWashA(string command)
        {
            SafeSetStatus("Doing Wash A.....");
            //Check if we are still waiting
            if (bWait || globals.bFluidicsBusy)
                SyncWait(100);

            string[] Cmdsplit = command.Split(',');

            globals.bFluidicsBusy = true;

            // First Process ByPass or Cols
            ByPassCol(Cmdsplit[1], Cmdsplit[2]);

            //Next Open the wash valve and the gas
            Man_Controlcs.WriteStatus("Wash A", "Wash A On");
            SafeSetStatus("Wash A being delivered...");
            ColorButton(rb_WashA, 1);

            Man_Controlcs.SendControllerMsg(1, valves.washaon);  //Wash A reagent valve #8
            SyncWait(60);
            ColorButton(rb_RGas, 1);


            SyncWait(10);
            this.rb_RGas.Text = "WshA";
            SyncWait(10);
            Man_Controlcs.WriteStatus("Wash A", "Wash A Gas On");
            Man_Controlcs.SendControllerMsg(1, valves.washagason); //Gas for Was A on gas valve port 6
            SyncWait(60);

            //Now Open ByPass or Pump and Execute
            TrainAPumpByPass(Cmdsplit[3], Cmdsplit[4]);

            //Reset Everything and Done
            Man_Controlcs.WriteStatus("Wash A", "Wash A Off");
            SafeSetStatus("Wash A Off...");
            ColorButton(rb_WashA, 0);

            Man_Controlcs.SendControllerMsg(1, valves.washaoff);  //close the Wash A  Valve
            SyncWait(100);

            Man_Controlcs.WriteStatus("none", "Wash A Gas Off");
            ColorButton(rb_RGas, 0);

            this.rb_RGas.Text = "none";

            Man_Controlcs.SendControllerMsg(1, valves.washagasoff);  // gas off
            SyncWait(100);

            TrainAEnd();
            SafeSetStatus("Wash A Complete...");
            globals.bFluidicsBusy = false;
        }
        /// <summary>
        /// Do WashB - Do command to deliver wash B during a protocol
        /// controls the valves and delivers via pump or pressure the desired amount
        /// </summary>
        /// <param name="command">the command string</param>
        public void DoWashB(string cmd)
        {
            //Check if we are still waiting
            if (bWait || globals.bFluidicsBusy)
                SyncWait(100);

            //int Vol = int.Parse(amt);

            globals.bFluidicsBusy = true;

            string[] Cmdsplit = cmd.Split(',');

            // First secelct ByPass or Cols, bypass will allow reagent to flow fast
            ByPassCol(Cmdsplit[1], Cmdsplit[2]);

            //Next Open the wash valve and the gas
            Man_Controlcs.WriteStatus("Wash B", "Wash B On");
            SafeSetStatus("Wash B On...");
            ColorButton(rb_WashB, 1);

            SyncWait(10);
            Man_Controlcs.SendControllerMsg(1, valves.washb1on);  //Wash B reagent valve Amidite Block 1
            SyncWait(30);
            Man_Controlcs.SendControllerMsg(1, valves.washb2on);  //Wash B reagent valve Amidite Block 2
            SyncWait(30);

            ColorButton(rb_RGas, 1);
            this.rb_RGas.Text = "WshB";

            Man_Controlcs.WriteStatus("Wash B", "Wash B Gas On");
            Man_Controlcs.SendControllerMsg(1, valves.washbgason); //Gas for Was A on gas valve port 6
            SyncWait(40);

            //Now Open ByPass or Pump and Execute
            TrainBPumpByPass(Cmdsplit[3], Cmdsplit[4]);

            //Reset Everything and Done
            Man_Controlcs.WriteStatus("Wash B", "Wash B Off");
            ColorButton(rb_WashB, 0);

            SyncWait(10);
            Man_Controlcs.SendControllerMsg(1, valves.washb1off);  //close the Wash B block A  Valve
            SyncWait(30);
            Man_Controlcs.SendControllerMsg(1, valves.washb2off);  //close the Wash B block B  Valve
            SyncWait(30);

            Man_Controlcs.WriteStatus("Wash B", "Wash B Gas Off");
            SafeSetStatus("Wash B Off...");
            ColorButton(rb_RGas, 0);

            this.rb_RGas.Text = "none";

            Man_Controlcs.SendControllerMsg(1, valves.washbgasoff);  // gas off
            SyncWait(40);

            //add extra time for long Washes
            string s = Cmdsplit[4].Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty).Replace("\0", String.Empty);
            int xt = int.Parse(s);
            SyncWait(xt);   //wait 5000ms for 5mL pump in

            //close down
            TrainBEnd();
            SafeSetStatus("Wash B Complete.....");
            globals.bFluidicsBusy = false;

        }
        /// <summary>
        /// Do WashB - Do command to deliver wash B during a protocol
        /// controls the valves and delivers via pump or pressure the desired amount
        /// </summary>
        /// <param name="command">the command string</param>
        public void DoWashAmidite(string cmd)
        {
            //Check if we are still waiting
            if (bWait || globals.bFluidicsBusy)
                SyncWait(100);

            //int Vol = int.Parse(amt);

            globals.bFluidicsBusy = true;

            string[] Cmdsplit = cmd.Split(',');

            // First secelct ByPass or Cols, bypass will allow reagent to flow fast
            ByPassCol(Cmdsplit[1], Cmdsplit[2]);

            //Next Open the wash valve and the gas
            Man_Controlcs.WriteStatus("Wash B", "Wash B On");
            SafeSetStatus("Wash B On...");
            ColorButton(rb_WashB, 1);

            SyncWait(10);
            if (globals.iAmBottle < 8) //amidites 1 to 7 on washb1 amdites 8 to 14 on washb2
            {
                Man_Controlcs.SendControllerMsg(1, valves.washb1on);  //Wash B reagent valve Amidite Block 1
                SyncWait(30);
                Man_Controlcs.SendControllerMsg(1, valves.washb2off); //just to make sure
            }
            else
            {
                Man_Controlcs.SendControllerMsg(1, valves.washb2on);  //Wash B reagent valve Amidite Block 2
                SyncWait(30);
                Man_Controlcs.SendControllerMsg(1, valves.washb1off);  //just to make sure
            }
            ColorButton(rb_RGas, 1);

            this.rb_RGas.Text = "WshB";

            Man_Controlcs.WriteStatus("Wash B", "Wash B Gas On");
            Man_Controlcs.SendControllerMsg(1, valves.washbgason); //Gas for Was A on gas valve port 6
            SyncWait(40);

            bIsPush = true;
            //Now Open ByPass or Pump and Execute
            TrainBPumpByPass(Cmdsplit[3], Cmdsplit[4]);


            //Reset Everything and Done
            Man_Controlcs.WriteStatus("Wash B", "Wash B Off");
            ColorButton(rb_WashB, 0);

            SyncWait(10);
            //in all cases make sure they are both closed!!!
            Man_Controlcs.SendControllerMsg(1, valves.washb1off);  //close the Wash B block A  Valve
            SyncWait(30);
            Man_Controlcs.SendControllerMsg(1, valves.washb2off);  //close the Wash B block B  Valve
            SyncWait(30);

            Man_Controlcs.WriteStatus("Wash B", "Wash B Gas Off");
            SafeSetStatus("Wash B Off...");
            ColorButton(rb_RGas, 0);

            this.rb_RGas.Text = "none";

            Man_Controlcs.SendControllerMsg(1, valves.washbgasoff);  // gas off
            SyncWait(40);

            //add extra time for long Washes
            string s = Cmdsplit[4].Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty).Replace("\0", String.Empty);
            int xt = int.Parse(s) * 3;
            SyncWait(xt);   //wait 15000ms for 5mL pump in

            //close down
            TrainBEnd();
            SafeSetStatus("Wash B Complete.....");
            globals.bFluidicsBusy = false;

        }
        /// <summary>
        /// Do WashAB - Do command to deliver wash B during a protocol
        /// controls the valves and delivers via pump or pressure the desired amount
        /// </summary>
        /// <param name="command">the command string</param>
        public void DoBothAB(string cmd)
        {
            //Check if we are still waiting
            if (bWait || globals.bFluidicsBusy)
                SyncWait(300);

            //int Vol = int.Parse(amt);

            globals.bFluidicsBusy = true;

            string[] Cmdsplit = cmd.Split(',');

            // First secelct ByPass or Cols, bypass will allow reagent to flow fast
            ByPassCol(Cmdsplit[1], Cmdsplit[2]);

            //Next Open the both wash valves and the gas
            //first B
            Man_Controlcs.WriteStatus("Wash B", "Wash B On");
            SafeSetStatus("Wash B On...");
            ColorButton(rb_WashB, 1);

            SyncWait(10);
            Man_Controlcs.SendControllerMsg(1, valves.washb1on);  //Wash B reagent valve Amidite Block 1
            SyncWait(30);
            Man_Controlcs.SendControllerMsg(1, valves.washb2on);  //Wash B reagent valve Amidite Block 2
            SyncWait(30);


            //next A
            Man_Controlcs.WriteStatus("Wash A", "Wash A On");
            SafeSetStatus("Wash A On...");
            ColorButton(rb_WashA, 1);

            SyncWait(10);
            Man_Controlcs.SendControllerMsg(1, valves.washaon);  //Wash A reagent valve Amidite Block 2
            SyncWait(30);

            ColorButton(rb_RGas, 1);
            this.rb_RGas.Text = "WshAB";

            Man_Controlcs.WriteStatus("Wash B", "Wash B Gas On");
            Man_Controlcs.SendControllerMsg(1, valves.washbgason); //Gas for Was B on gas valve port 6
            SyncWait(30);

            Man_Controlcs.WriteStatus("Wash B", "Wash A Gas On");
            Man_Controlcs.SendControllerMsg(1, valves.washagason); //Gas for Was A on gas valve port 6
            SyncWait(30);

            //Now Open ByPass or Pump and Execute
            TrainBPumpByPass(Cmdsplit[3], Cmdsplit[4]);

            //Reset Everything and Done
            //first A
            Man_Controlcs.WriteStatus("Wash A", "Wash A Off");
            ColorButton(rb_WashA, 0);

            SyncWait(10);
            Man_Controlcs.SendControllerMsg(1, valves.washaoff);  //close the Wash A  Valve
            SyncWait(40);

            //next B
            Man_Controlcs.WriteStatus("Wash B", "Wash B Off");
            ColorButton(rb_WashB, 0);

            SyncWait(10);
            Man_Controlcs.SendControllerMsg(1, valves.washb1off);  //close the Wash B  Block 1 Valve
            SyncWait(40);
            Man_Controlcs.SendControllerMsg(1, valves.washb2off);  //close the Wash B  Block 2 Valve
            SyncWait(40);

            Man_Controlcs.WriteStatus("Wash A", "Wash A Gas Off");
            SafeSetStatus("Wash A Off...");
            ColorButton(rb_RGas, 0);
            SyncWait(10);

            Man_Controlcs.SendControllerMsg(1, valves.washagasoff);  // gas off
            SyncWait(30);

            Man_Controlcs.WriteStatus("Wash B", "Wash B Gas Off");
            SafeSetStatus("Wash B Off...");
            ColorButton(rb_RGas, 0);
            SyncWait(10);

            this.rb_RGas.Text = "none";

            Man_Controlcs.SendControllerMsg(1, valves.washbgasoff);  // gas off
            SyncWait(30);

            //add extra time for long Washes
            string s = Cmdsplit[4].Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty).Replace("\0", String.Empty);
            int xt = int.Parse(s) * 3;

            SyncWait(xt);   //wait 15000ms for 5mL pump in

            //close down
            TrainBEnd();
            SafeSetStatus("Wash AB Complete.....");
            globals.bFluidicsBusy = false;

        }
        /// <summary>
        /// Do BothActB - Do command to deliver wash B during a protocol
        /// controls the valves and delivers via pump or pressure the desired amount
        /// with Activator and Wash instead of wash and wash as above
        /// </summary>
        /// <param name="command">the command string</param>
        public void DoBothActB(string cmd)
        {
            //Check if we are still waiting
            if (bWait || globals.bFluidicsBusy)
                SyncWait(300);

            //int Vol = int.Parse(amt);

            globals.bFluidicsBusy = true;

            string[] Cmdsplit = cmd.Split(',');

            // First secelct ByPass or Cols, bypass will allow reagent to flow fast
            ByPassCol(Cmdsplit[1], Cmdsplit[2]);

            //Next Open the both wash valves and the gas
            //first B
            Man_Controlcs.WriteStatus("Wash B", "Wash B On");
            SafeSetStatus("Wash B On...");
            ColorButton(rb_WashB, 1);

            SyncWait(10);
            Man_Controlcs.SendControllerMsg(1, valves.washb1on);  //Wash B reagent valve Amidite Block 1
            SyncWait(30);
            Man_Controlcs.SendControllerMsg(1, valves.washb2on);  //Wash B reagent valve Amidite Block 2
            SyncWait(30);


            //next Activator
            Man_Controlcs.WriteStatus("Activator", "Activator On");
            SafeSetStatus("Activator On...");
            ColorButton(rb_Act1, 1);

            SyncWait(10);
            Man_Controlcs.SendControllerMsg(1, valves.act1on);  //Activator 1 reagent valve
            SyncWait(30);

            ColorButton(rb_RGas, 1);
            this.rb_RGas.Text = "Act+B";

            Man_Controlcs.WriteStatus("Wash B", "Wash B Gas On");
            Man_Controlcs.SendControllerMsg(1, valves.washbgason); //Gas for Was B on gas valve port 6
            SyncWait(30);

            Man_Controlcs.WriteStatus("Act", "Activator 1 Gas On");
            Man_Controlcs.SendControllerMsg(1, valves.act1gason); //Gas for Was A on gas valve port 6
            SyncWait(30);

            //Now Open ByPass or Pump and Execute
            TrainBPumpByPass(Cmdsplit[3], Cmdsplit[4]);

            //Reset Everything and Done
            //first A
            Man_Controlcs.WriteStatus("Act 1", "Act1 Off");
            ColorButton(rb_Act1, 0);

            SyncWait(10);
            Man_Controlcs.SendControllerMsg(1, valves.act1off);  //close the Wash A  Valve
            SyncWait(40);

            //next B
            Man_Controlcs.WriteStatus("Wash B", "Wash B Off");
            ColorButton(rb_WashB, 0);

            SyncWait(10);
            Man_Controlcs.SendControllerMsg(1, valves.washb1off);  //close the Wash B  Block 1 Valve
            SyncWait(40);
            Man_Controlcs.SendControllerMsg(1, valves.washb2off);  //close the Wash B  Block 2 Valve
            SyncWait(40);

            Man_Controlcs.WriteStatus("Act 1 Gas", "Act 1 Gas Off");
            SafeSetStatus("Act 1 Off...");
            ColorButton(rb_RGas, 0);
            SyncWait(10);

            Man_Controlcs.SendControllerMsg(1, valves.act1gasoff);  // gas off
            SyncWait(30);

            Man_Controlcs.WriteStatus("Wash B", "Wash B Gas Off");
            SafeSetStatus("Wash B Off...");
            ColorButton(rb_RGas, 0);
            SyncWait(10);

            this.rb_RGas.Text = "none";

            Man_Controlcs.SendControllerMsg(1, valves.washbgasoff);  // gas off
            SyncWait(30);

            //add extra time for long Washes
            string s = Cmdsplit[4].Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty).Replace("\0", String.Empty);
            int xt = int.Parse(s) * 3;

            SyncWait(xt);   //wait 15000ms for 5mL pump in

            //close down
            TrainBEnd();
            SafeSetStatus("Wash Act + B Complete.....");
            globals.bFluidicsBusy = false;

        }
        /// <summary>
        /// Do Deblock - Do command to deliver Deblock Reagent during a protocol
        /// controls the valves and delivers via pump or pressure the desired amount
        /// </summary>
        /// <param name="command">the command string</param>
        public void DoDeblock(string cmd)
        {
            SafeSetStatus("Doing Deblock.....");
            //Check if we are still waiting
            if (bWait || globals.bFluidicsBusy)
                SyncWait(100);

            string[] Cmdsplit = cmd.Split(',');

            globals.bFluidicsBusy = true;
            globals.bDeblocking = true;

            // First Process ByPass or Cols
            ByPassCol(Cmdsplit[1], Cmdsplit[2]);

            //Now open the deblock valve and the gas
            Man_Controlcs.WriteStatus("Deblock", "Deblock On");
            SafeSetStatus("Deblock On...");
            ColorButton(rb_Dbl, 1);

            Man_Controlcs.SendControllerMsg(1, valves.deblon);  //Deblocks reagent valve #8
            SyncWait(60);
            Man_Controlcs.WriteStatus("Deblock", "Deblock Gas On");
            ColorButton(rb_RGas, 1);

            this.rb_RGas.Text = "Dblk";

            Man_Controlcs.SendControllerMsg(1, valves.deblgason); //Gas for Was A on gas valve port 6
            SyncWait(60);

            //Now Open ByPass or Pump and Execute
            TrainAPumpByPass(Cmdsplit[3], Cmdsplit[4]);

            //Reset Everything and Done
            Man_Controlcs.WriteStatus("Deblock", "Deblock Off");
            ColorButton(rb_Dbl, 0);

            Man_Controlcs.SendControllerMsg(1, valves.debloff);  //close the Wash A  Valve
            SyncWait(100);
            Man_Controlcs.WriteStatus("Deblock", "Deblock Gas Off");
            SafeSetStatus("Deblock Off...");
            ColorButton(rb_RGas, 0);

            this.rb_RGas.Text = "none";

            Man_Controlcs.SendControllerMsg(1, valves.deblgasoff);  // gas off
            SyncWait(100);

            //now close everything
            TrainAEnd();

            globals.bFluidicsBusy = false;
            globals.bDeblocking = false;
            SafeSetStatus("Deblock Complete.....");
        }
        /// <summary>
        /// Process Trityl UV - Gets the UV data from the serial port call back and then processes it
        /// it calculates area/height the sends it both to the bar chart control as well as an array for
        /// later storage as CSV
        /// </summary>
        /// <param name="">no commands</param>

        private void ProcessUVTrityl()
        {
            SafeSetStatus("Processing Trity UV.....");
            double[] cycleArray = new double[8];
            if (globals.iCycle <= MaxCycles + 1)  //added +1 to cover the final deprotection
            {

                //if the flow cell is not running, stuff the array with a 0
                //it wont be displayed and we won't add it to the CSV file
                //first stuff the array and update the trityl bar chart
                int[] baseline = new int[6];
                int iArrayLength = 0;
                //globals.iUVTritylData.GetLength(1);

                for (int i = 0; i < 3600; i++)
                {
                    if (Convert.ToDouble(globals.iUVTritylData[0, i]) > 0 ||
                        Convert.ToDouble(globals.iUVTritylData[1, i]) > 0 ||
                        Convert.ToDouble(globals.iUVTritylData[2, i]) > 0 ||
                        Convert.ToDouble(globals.iUVTritylData[3, i]) > 0 ||
                        Convert.ToDouble(globals.iUVTritylData[4, i]) > 0 ||
                        Convert.ToDouble(globals.iUVTritylData[5, i]) > 0 ||
                        Convert.ToDouble(globals.iUVTritylData[6, i]) > 0 ||
                        Convert.ToDouble(globals.iUVTritylData[7, i]) > 0)
                        iArrayLength = iArrayLength + 1;
                }
                double[] onedetector = new double[iArrayLength + 1];

                //MessageBox.Show(iArrayLength.ToString(), "ArrayLength");

                if (globals.bUV1On)
                {
                    for (int i = 0; i < iArrayLength; i++)
                        onedetector[i] = Convert.ToDouble(globals.iUVTritylData[0, i]);

                    cycleArray[0] = GetOutData(onedetector, iArrayLength);
                }
                else
                    cycleArray[0] = 0.0;
                //repeat for each detector
                //detector 2
                if (globals.bUV2On)
                {
                    for (int i = 0; i < iArrayLength; i++)
                        onedetector[i] = Convert.ToDouble(globals.iUVTritylData[1, i]);

                    cycleArray[1] = GetOutData(onedetector, iArrayLength);
                }
                else
                    cycleArray[1] = 0.0;
                //detector 3
                if (globals.bUV3On)
                {
                    for (int i = 0; i < iArrayLength; i++)
                        onedetector[i] = Convert.ToDouble(globals.iUVTritylData[2, i]);

                    cycleArray[2] = GetOutData(onedetector, iArrayLength);
                }
                else
                    cycleArray[2] = 0.0;
                //detector 4
                if (globals.bUV4On)
                {
                    for (int i = 0; i < iArrayLength; i++)
                        onedetector[i] = Convert.ToDouble(globals.iUVTritylData[3, i]);

                    cycleArray[3] = GetOutData(onedetector, iArrayLength);
                }
                else
                    cycleArray[3] = 0.0;
                //detector 5
                if (globals.bUV5On)
                {
                    for (int i = 0; i < iArrayLength; i++)
                        onedetector[i] = Convert.ToDouble(globals.iUVTritylData[4, i]);

                    cycleArray[4] = GetOutData(onedetector, iArrayLength);
                }
                else
                    cycleArray[4] = 0.0;
                //detector 6
                if (globals.bUV6On)
                {
                    for (int i = 0; i < iArrayLength; i++)
                        onedetector[i] = Convert.ToDouble(globals.iUVTritylData[5, i]);

                    cycleArray[5] = GetOutData(onedetector, iArrayLength);
                }
                else
                    cycleArray[5] = 0.0;
                //detector 7
                if (globals.bUV7On)
                {
                    for (int i = 0; i < iArrayLength; i++)
                        onedetector[i] = Convert.ToDouble(globals.iUVTritylData[6, i]);

                    cycleArray[6] = GetOutData(onedetector, iArrayLength);
                }
                else
                    cycleArray[6] = 0.0;
                //detector 8
                if (globals.bUV8On)
                {
                    for (int i = 0; i < iArrayLength; i++)
                        onedetector[i] = Convert.ToDouble(globals.iUVTritylData[7, i]);

                    cycleArray[7] = GetOutData(onedetector, iArrayLength);
                }
                else
                    cycleArray[7] = 0.0;

                //in all cases write to the CSV file
                //write the CSV for UV data for the current run
                string CSVString = "";

                CSVString = (globals.iCycle + 1).ToString("0") + "," + cycleArray[0].ToString("0.0") + "," + cycleArray[1].ToString("0.0") + ","
                    + cycleArray[2].ToString("0.0") + "," + cycleArray[3].ToString("0.0") + ","
                    + cycleArray[4].ToString("0.0") + "," + cycleArray[5].ToString("0.0") + ","
                    + cycleArray[6].ToString("0.0") + "," + cycleArray[7].ToString("0.0");

                //MessageBox.Show("CSV STRING - " + CSVString, iArrayLength.ToString("0") );
                FileStream fs = null;

                try
                {
                    if (globals.bSaveBar)
                    {
                        if (!File.Exists(globals.UVBarCSVfile))
                        {
                            // Create a file to write to.
                            fs = new FileStream(globals.UVBarCSVfile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                            using (StreamWriter sw = new StreamWriter(fs))
                            {
                                fs = null;
                                sw.WriteLine(CSVString);
                            }
                        }
                        else
                        {
                            fs = new FileStream(globals.UVBarCSVfile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                            using (StreamWriter sw = new StreamWriter(fs))
                            {
                                fs = null;
                                sw.WriteLine(CSVString);

                            }
                        }
                    }
                    //do this last so if needed we can simply update from the file
                    //later repeat the same for conductivity
                    //if the chart is open, update it
                    //if (globals.bBarCharting)
                    barchartcontrol.UpdateUVTrityBar(cycleArray);


                }
                catch (Exception x) { MessageBox.Show("Trityl File Error - " + x.ToString()); return; }
                finally
                {
                    if (fs != null)
                        fs.Dispose();
                }


            }
            SafeSetStatus("Processing Trity UV Complete.....");
        }
        private double GetOutData(double[] detector, int Count)
        {

            double outdata = 0.0;
            double baseline = 0.0;
            double highest = 0.0;
            double threshold = Properties.Settings.Default.Threshold;

            if (Count < 15 || detector.Length < 15)
                return 1.0;

            //assume it takes at least 10 seconds for the trityl to start eluting, we will 
            //the first 10 seconds of data to be baseline
            for (int i = 0; i < 10; i++)
                baseline = baseline + detector[i];

            baseline = baseline / 10;
            baseline = baseline * (1 + threshold);

            //now add every point, if above baseline * threshold
            int j = 0;

            //sum all the points
            for (int i = 0; i < Count; i++)
            {
                if (detector[i] > baseline)
                {
                    if (detector[i] > highest)  //will give the highest peak
                        highest = detector[i];

                    //subtract baseline from each point...
                    outdata = outdata + (detector[i] - baseline);
                    j++;
                }

            }


            if (globals.iAreaHeight == 0)
                return outdata;
            else
                return highest;
        }
        /// <summary>
        /// Do Oxidizer - Do command to deliver Oxidizer Reagent during a protocol
        /// controls the valves and delivers via pump or pressure the desired amount
        /// </summary>
        /// <param name="command">the command string</param>

        public void DoOxidizer(string cmd)
        {
            SafeSetStatus("Doing Oxidation.....");
            //Check if we are still waiting
            if (bWait || globals.bFluidicsBusy)
                SyncWait(100);

            string[] Cmdsplit = cmd.Split(',');

            globals.bFluidicsBusy = true;

            // First Process ByPass or Cols
            ByPassCol(Cmdsplit[1], Cmdsplit[2]);

            //Next Open the Oxidizer valve and the gas
            Man_Controlcs.WriteStatus("Oxidizer", "Oxidizer 1 On");
            SafeSetStatus("Oxidizer On...");
            ColorButton(rb_Ox1, 1);

            Man_Controlcs.SendControllerMsg(1, valves.ox1on);
            SyncWait(60);
            Man_Controlcs.WriteStatus("Oxidizer", "Oxidizer 1 Gas On");
            ColorButton(rb_RGas, 1);

            this.rb_RGas.Text = "Ox-1";

            Man_Controlcs.SendControllerMsg(1, valves.ox1gason);
            SyncWait(60);

            //Now Open ByPass or Pump and Execute
            TrainAPumpByPass(Cmdsplit[3], Cmdsplit[4]);

            //Reset Everything and Done
            Man_Controlcs.WriteStatus("Oxidizer", "Oxidizer 1 Off");
            ColorButton(rb_Ox1, 0);

            Man_Controlcs.SendControllerMsg(1, valves.ox1off);  //close the Wash A  Valve
            SyncWait(100);
            Man_Controlcs.WriteStatus("Oxidizer", "Oxidizer 1 Gas Off");
            SafeSetStatus("Oxidizer Off...");
            ColorButton(rb_RGas, 0);

            this.rb_RGas.Text = "none";

            Man_Controlcs.SendControllerMsg(1, valves.ox1gasoff);  // gas off
            SyncWait(100);

            //now close everything
            TrainAEnd();

            globals.bFluidicsBusy = false;
            SafeSetStatus("Oxidation Complete.....");

        }
        /// <summary>
        /// Do Oxidizer 2 - Do command to deliver Oxidizer 2 Reagent during a protocol
        /// controls the valves and delivers via pump or pressure the desired amount
        /// </summary>
        /// <param name="command">the command string</param>
        public void DoOx2(string cmd)
        {
            SafeSetStatus("Doing Oxidation 2.....");
            //Check if we are still waiting
            if (bWait || globals.bFluidicsBusy)
                SyncWait(100);

            string[] Cmdsplit = cmd.Split(',');

            globals.bFluidicsBusy = true;

            // First Process ByPass or Cols
            ByPassCol(Cmdsplit[1], Cmdsplit[2]);

            //Now Open the wash valve and the gas
            Man_Controlcs.WriteStatus("Oxidizer", "Thiol Reagent On");
            SafeSetStatus("Thiol Reagent On.....");
            ColorButton(rb_Ox2, 1);

            Man_Controlcs.SendControllerMsg(1, valves.ox2on);
            SyncWait(100);
            Man_Controlcs.WriteStatus("Oxidizer", "Thiol Reagent Gas On");
            ColorButton(rb_RGas, 1);

            this.rb_RGas.Text = "Thiol";

            Man_Controlcs.SendControllerMsg(1, valves.ox2gason);
            SyncWait(60);

            //Now Open ByPass or Pump and Execute
            TrainAPumpByPass(Cmdsplit[3], Cmdsplit[4]);

            //Reset Everything and Done
            Man_Controlcs.WriteStatus("Oxidizer", "Thiol Reagent Off");
            ColorButton(rb_Ox2, 0);

            Man_Controlcs.SendControllerMsg(1, valves.ox2off);  //close the Wash A  Valve
            SyncWait(100);
            Man_Controlcs.WriteStatus("Oxidizer", "Thiol Reagent Gas Off");
            SafeSetStatus("Thiol Reagent Off.....");
            ColorButton(rb_RGas, 0);

            this.rb_RGas.Text = "none";

            Man_Controlcs.SendControllerMsg(1, valves.ox2gasoff);  // gas off
            SyncWait(100);

            //close everthing else
            TrainAEnd();

            globals.bFluidicsBusy = false;
            SafeSetStatus("Thiolation Complete.....");

        }
        /// <summary>
        /// Do Caps - Do command to deliver a mixture of CapA and CapB Reagents during a protocol
        /// the procedure controls the valves and delivers via pump or pressure the desired amount
        /// </summary>
        /// <param name="command">the command string</param>

        public void DoCaps(string cmd)
        {
            SafeSetStatus("Doing Cappling.....");
            //Check if we are still waiting
            if (bWait || globals.bFluidicsBusy)
                SyncWait(100);

            string[] Cmdsplit = cmd.Split(',');

            globals.bFluidicsBusy = true;

            // First Process ByPass or Cols
            ByPassCol(Cmdsplit[1], Cmdsplit[2]);

            //Next Open BOTH Cap A and Cap B  and the gas
            Man_Controlcs.WriteStatus("Capping Reagents", "Cap A and Cap B On");
            SafeSetStatus("Cap A and Cap B On.....");
            //first gas on...
            Man_Controlcs.SendControllerMsg(1, valves.capagason);
            SyncWait(60);

            ColorButton(rb_CapA, 1);

            Man_Controlcs.SendControllerMsg(1, valves.capaon);
            SyncWait(60);

            ColorButton(rb_CapB, 1);

            Man_Controlcs.SendControllerMsg(1, valves.capbon);
            SyncWait(60);

            Man_Controlcs.WriteStatus("Capping Reagents", "Cap A and Cap B Gas On");
            ColorButton(rb_RGas, 1);

            this.rb_RGas.Text = "Caps";

            Man_Controlcs.SendControllerMsg(1, valves.capagason);

            //Now Open ByPass or Pump and Execute
            TrainAPumpByPass(Cmdsplit[3], Cmdsplit[4]);

            //Reset Everything and Done
            Man_Controlcs.WriteStatus("Capping Reagents", "Cap A and Cap B Off");
            ColorButton(rb_CapA, 0);

            //turn the gas off
            Man_Controlcs.SendControllerMsg(1, valves.capagasoff);  // gas off
            SyncWait(60);

            //then close B first
            Man_Controlcs.SendControllerMsg(1, valves.capboff);  //close the Wash A  Valve
            SyncWait(60);

            Man_Controlcs.SendControllerMsg(1, valves.capaoff);  //close the Wash A  Valve
            SyncWait(60);

            ColorButton(rb_CapB, 0);

            Man_Controlcs.WriteStatus("Capping Reagents", "Cap A and Cap B Gas Off");
            SafeSetStatus("Cap A and Cap B Off.....");
            ColorButton(rb_RGas, 0);

            this.rb_RGas.Text = "none";

            //close the train
            TrainAEnd();

            globals.bFluidicsBusy = false;
            SafeSetStatus("Capping Complete.....");

        }
        /// <summary>
        /// Do Cap A - Do command to deliver Cap A Reagent during a protocol
        /// controls the valves and delivers via pump or pressure the desired amount
        /// </summary>
        /// <param name="command">the command string</param>

        public void DoCapA(string cmd)
        {
            SafeSetStatus("Doing Cap A.....");
            //Check if we are still waiting
            if (bWait || globals.bFluidicsBusy)
                SyncWait(100);

            string[] Cmdsplit = cmd.Split(',');

            globals.bFluidicsBusy = true;

            // First Process ByPass or Cols
            ByPassCol(Cmdsplit[1], Cmdsplit[2]);

            //First Open the wash valve and the gas
            Man_Controlcs.WriteStatus("Capping Reagents", "Cap A On");
            SafeSetStatus("Cap A On.....");
            ColorButton(rb_CapA, 1);

            Man_Controlcs.SendControllerMsg(1, valves.capaon);
            SyncWait(100);
            Man_Controlcs.WriteStatus("Capping Reagents", "Cap A Gas On");
            ColorButton(rb_RGas, 1);

            this.rb_RGas.Text = "CapA";

            Man_Controlcs.SendControllerMsg(1, valves.capagason);
            SyncWait(100);

            //Now Open ByPass or Pump and Execute
            TrainAPumpByPass(Cmdsplit[3], Cmdsplit[4]);

            //Reset Everything and Done
            Man_Controlcs.WriteStatus("Capping Reagents", "Cap A Off");
            ColorButton(rb_CapA, 0);

            Man_Controlcs.SendControllerMsg(1, valves.capaoff);  //close the Wash A  Valve
            SyncWait(100);
            Man_Controlcs.WriteStatus("Capping Reagents", "Cap A Gas Off");
            SafeSetStatus("Cap A Off.....");
            ColorButton(rb_RGas, 0);

            this.rb_RGas.Text = "none";

            Man_Controlcs.SendControllerMsg(1, valves.capagasoff);  // gas off
            SyncWait(100);

            //now close everything
            TrainAEnd();
            globals.bFluidicsBusy = false;
            SafeSetStatus("Cap A Complete.....");
        }
        /// <summary>
        /// Do Cap B - Do command to deliver Cap B Reagent during a protocol
        /// controls the valves and delivers via pump or pressure the desired amount
        /// </summary>
        /// <param name="command">the command string</param>
        public void DoCapB(string cmd)
        {
            SafeSetStatus("Doing Cap B.....");
            //Check if we are still waiting
            if (bWait || globals.bFluidicsBusy)
                SyncWait(100);

            string[] Cmdsplit = cmd.Split(',');

            globals.bFluidicsBusy = true;

            // First Process ByPass or Cols
            ByPassCol(Cmdsplit[1], Cmdsplit[2]);

            //next open the reagent valves
            Man_Controlcs.WriteStatus("Capping Reagents", "Cap B On");
            SafeSetStatus("Cap B On.....");
            ColorButton(rb_CapB, 1);

            Man_Controlcs.SendControllerMsg(1, valves.capbon);  //Open the Cap B  Valve
            SyncWait(100);
            Man_Controlcs.WriteStatus("Capping Reagents", "Cap B Gas On");
            ColorButton(rb_RGas, 1);

            this.rb_RGas.Text = "CapB";

            Man_Controlcs.SendControllerMsg(1, valves.capbgason);  // gas on
            SyncWait(100);


            //Now Open ByPass or Pump and Execute
            TrainAPumpByPass(Cmdsplit[3], Cmdsplit[4]);

            //Reset Everything and Done
            Man_Controlcs.WriteStatus("Capping Reagents", "Cap B Off");
            ColorButton(rb_CapB, 0);

            Man_Controlcs.SendControllerMsg(1, valves.capboff);  //close the Cap B  Valve
            SyncWait(100);
            Man_Controlcs.WriteStatus("Capping Reagents", "Cap B Gas Off");
            SafeSetStatus("Cap B Off.....");
            ColorButton(rb_RGas, 1);

            this.rb_RGas.Text = "none";

            Man_Controlcs.SendControllerMsg(1, valves.capbgasoff);  // gas off
            SyncWait(100);

            //now close everything
            TrainAEnd();

            globals.bFluidicsBusy = false;
            SafeSetStatus("Cap B Complete.....");
        }
        /// <summary>
        /// Do DEA - Do command to deliver Reagent from the DEA bottle during a protocol
        /// controls the valves and delivers via pump or pressure the desired amount
        /// </summary>
        /// <param name="command">the command string</param>

        public void DoDEA(string cmd)
        {
            SafeSetStatus("Doing DEA.....");
            //Check if we are still waiting
            if (bWait || globals.bFluidicsBusy)
                SyncWait(100);

            string[] Cmdsplit = cmd.Split(',');

            globals.bFluidicsBusy = true;

            // First Process ByPass or Cols
            ByPassCol(Cmdsplit[1], Cmdsplit[2]);

            //First Open the wash valve and the gas
            Man_Controlcs.WriteStatus("DEA Reagent", "DEA On");
            SafeSetStatus("DEA On.....");
            ColorButton(rb_Xtra1, 1);

            Man_Controlcs.SendControllerMsg(1, valves.xtra1on);
            SyncWait(100);
            Man_Controlcs.WriteStatus("DEA Reagent", "DEA Gas On");
            ColorButton(rb_RGas, 1);

            this.rb_RGas.Text = "DEA";

            Man_Controlcs.SendControllerMsg(1, valves.xtra1gason);

            //Now Open ByPass or Pump and Execute
            TrainAPumpByPass(Cmdsplit[3], Cmdsplit[4]);

            //Reset Everything and Done
            Man_Controlcs.WriteStatus("DEA Reagent", "DEA Off");
            ColorButton(rb_Xtra1, 0);

            Man_Controlcs.SendControllerMsg(1, valves.xtra1off);  //close the Wash A  Valve
            SyncWait(100);
            Man_Controlcs.WriteStatus("DEA Reagent", "DEA Gas Off");
            SafeSetStatus("DEA Off.....");
            ColorButton(rb_RGas, 0);

            this.rb_RGas.Text = "none";

            Man_Controlcs.SendControllerMsg(1, valves.xtra1gasoff);  // gas off
            SyncWait(100);

            //now close everything
            TrainAEnd();

            globals.bFluidicsBusy = false;
            SafeSetStatus("DEA Complete.....");
        }
        /// <summary>
        /// Do XTra2 - Do command to deliver Reagent from the Xtra 2 bottle during a protocol
        /// controls the valves and delivers via pump or pressure the desired amount
        /// </summary>
        /// <param name="command">the command string</param>
        public void DoGasPurge(string cmd)
        {
            SafeSetStatus("Doing Gas Purge.....");
            //Check if we are still waiting
            if (bWait || globals.bFluidicsBusy)
                SyncWait(100);

            string[] Cmdsplit = cmd.Split(',');

            globals.bFluidicsBusy = true;

            // First Process ByPass or Cols
            ByPassCol(Cmdsplit[1], Cmdsplit[2]);

            //Next Open the reagent valve and the gas
            Man_Controlcs.WriteStatus("Gas PUrge", "Gas Purge On");
            SafeSetStatus("Gas Purge On.....");
            ColorButton(rb_Xtra2, 1);

            Man_Controlcs.SendControllerMsg(1, valves.gaspurgeon);
            SyncWait(100);
            Man_Controlcs.WriteStatus("Gas Purge", "Gas Purge Gas On");


            SyncWait(100);

            //Now Open ByPass or Pump and Execute
            TrainAPumpByPass(Cmdsplit[3], Cmdsplit[4]);


            //Reset Everything and Done
            Man_Controlcs.WriteStatus("Gas Purge", "Gas Purge Off");
            ColorButton(rb_Xtra2, 0);

            Man_Controlcs.SendControllerMsg(1, valves.gaspurgeoff);  //close the Wash A  Valve
            SyncWait(100);
            Man_Controlcs.WriteStatus("Gas Purge", "Gas Purge Gas Off");
            SafeSetStatus("Gas Purge Off.....");

            SyncWait(100);

            //now close everything
            TrainAEnd();

            globals.bFluidicsBusy = false;
            SafeSetStatus("Gas Purge Complete.....");

        }
        /// <summary>
        /// Do Pressurize - Do command to deliver gas to each reagent for three to four seconds
        /// pressurizes each and every bottle prior to use
        /// </summary>
        /// <param name="command">none</param>
        public void DoPressurize()
        {
            SafeSetStatus("Doing System Pressurization.....");
            Man_Controlcs.WriteStatus("Pressurizing", "Pressurizing all Reagents");
            //Check if we are still waiting
            if (bWait || globals.bFluidicsBusy)
                SyncWait(100);

            globals.bFluidicsBusy = true;

            //first pressurize the amdites
            SafeSetStatus("Pressurizing the Amidites.....");
            Man_Controlcs.WriteStatus("Pressurize", "Pressurizing the Amidites");
            ColorButton(rb_AGas, 1);

            this.rb_AGas.Text = "Bottom";
            Man_Controlcs.SendControllerMsg(1, valves.pressurebot);  // amidite to top
            Man_Controlcs.SyncWait(2000);

            ColorButton(rb_Am1, 3);
            ColorButton(rb_Am2, 3);
            ColorButton(rb_Am3, 3);
            ColorButton(rb_Am4, 3);
            ColorButton(rb_Am5, 3);
            ColorButton(rb_Am6, 3);
            ColorButton(rb_Am7, 3);
            ColorButton(rb_Am8, 3);
            this.rb_AGas.Text = "Top";
            Man_Controlcs.SendControllerMsg(1, valves.pressuretop);  // amidite to top
            Man_Controlcs.SyncWait(2000);
            ColorButton(rb_Am1, 0);
            ColorButton(rb_Am2, 0);
            ColorButton(rb_Am3, 0);
            ColorButton(rb_Am4, 0);
            ColorButton(rb_Am5, 0);
            ColorButton(rb_Am6, 0);
            ColorButton(rb_Am7, 0);
            ColorButton(rb_Am8, 0);

            ColorButton(rb_Am9, 3);
            ColorButton(rb_Am10, 3);
            ColorButton(rb_Am11, 3);
            ColorButton(rb_Am12, 3);
            ColorButton(rb_Am13, 3);
            ColorButton(rb_Am14, 3);
            Man_Controlcs.SyncWait(2000);

            Man_Controlcs.SendControllerMsg(1, valves.pressuretop);  // amidite to top
            Man_Controlcs.SyncWait(30);
            ColorButton(rb_Am9, 0);
            ColorButton(rb_Am10, 0);
            ColorButton(rb_Am11, 0);
            ColorButton(rb_Am12, 0);
            ColorButton(rb_Am13, 0);
            ColorButton(rb_Am14, 0);
            ColorButton(rb_AGas, 0);
            this.rb_AGas.Text = "none";

            SafeSetStatus("Pressuriziation of Amidites Complete.....");

            //next the train B Reagents
            SafeSetStatus("Pressurizing the Reagents.....");
            Man_Controlcs.WriteStatus("Pressurize", "Pressurizing the Reagents");
            ColorButton(rb_WashB, 3);
            ColorButton(rb_RGas, 1);

            this.rb_RGas.Text = "Wash B";
            Man_Controlcs.SendControllerMsg(1, valves.washbgason);  // gas on
            Man_Controlcs.SyncWait(3000);                                             // wait
            Man_Controlcs.SendControllerMsg(1, valves.washbgasoff);  // gas off
            Man_Controlcs.SyncWait(30);
            ColorButton(rb_WashB, 0);

            this.rb_RGas.Text = "Act 1";
            ColorButton(rb_Act1, 3);
            Man_Controlcs.SendControllerMsg(1, valves.act1gason);  // gas on
            Man_Controlcs.SyncWait(2500);                                             // wait
            Man_Controlcs.SendControllerMsg(1, valves.act1gasoff);  // gas off
            Man_Controlcs.SyncWait(30);
            ColorButton(rb_Act1, 0);

            ColorButton(rb_Act2, 3);
            this.rb_RGas.Text = "Act 2";
            Man_Controlcs.SendControllerMsg(1, valves.act2gason);  // gas on
            Man_Controlcs.SyncWait(2500);                                             // wait
            Man_Controlcs.SendControllerMsg(1, valves.act2gasoff);  // gas off
            Man_Controlcs.SyncWait(30);
            ColorButton(rb_Act2, 0);

            //now the big bottles
            ColorButton(rb_WashA, 3);
            this.rb_RGas.Text = "Wash A";
            Man_Controlcs.SendControllerMsg(1, valves.washagason);  // gas on
            Man_Controlcs.SyncWait(3000);                                             // wait
            Man_Controlcs.SendControllerMsg(1, valves.washagasoff);  // gas off
            Man_Controlcs.SyncWait(30);
            ColorButton(rb_WashA, 0);

            ColorButton(rb_Dbl, 3);
            this.rb_RGas.Text = "Debl";
            Man_Controlcs.SendControllerMsg(1, valves.deblgason);  // gas on
            Man_Controlcs.SyncWait(3000);                                             // wait
            Man_Controlcs.SendControllerMsg(1, valves.deblgasoff);  // gas off
            Man_Controlcs.SyncWait(30);
            ColorButton(rb_Dbl, 0);

            ColorButton(rb_CapA, 3);
            this.rb_RGas.Text = "Cap A";
            Man_Controlcs.SendControllerMsg(1, valves.capagason);  // gas on
            Man_Controlcs.SyncWait(3000);                                             // wait
            Man_Controlcs.SendControllerMsg(1, valves.capagasoff);  // gas off
            Man_Controlcs.SyncWait(30);
            ColorButton(rb_CapA, 0);

            ColorButton(rb_CapB, 3);
            this.rb_RGas.Text = "Cap B";
            Man_Controlcs.SendControllerMsg(1, valves.capbgason);  // gas on
            Man_Controlcs.SyncWait(3000);                                             // wait
            Man_Controlcs.SendControllerMsg(1, valves.capbgasoff);  // gas off
            Man_Controlcs.SyncWait(30);
            ColorButton(rb_CapB, 0);

            ColorButton(rb_Ox1, 3);
            this.rb_RGas.Text = "Ox 1";
            Man_Controlcs.SendControllerMsg(1, valves.ox1gason);  // gas on
            Man_Controlcs.SyncWait(3000);                                             // wait
            Man_Controlcs.SendControllerMsg(1, valves.ox1gasoff);  // gas off
            Man_Controlcs.SyncWait(30);
            ColorButton(rb_Ox1, 0);

            ColorButton(rb_Ox2, 3);
            this.rb_RGas.Text = "Thiol";
            Man_Controlcs.SendControllerMsg(1, valves.ox2gason);  // gas on
            Man_Controlcs.SyncWait(3000);                                             // wait
            Man_Controlcs.SendControllerMsg(1, valves.ox2gasoff);  // gas off
            Man_Controlcs.SyncWait(30);
            ColorButton(rb_Ox2, 0);

            ColorButton(rb_Xtra1, 3);
            this.rb_RGas.Text = "DEA";
            Man_Controlcs.SendControllerMsg(1, valves.xtra1gason);  // gas on
            Man_Controlcs.SyncWait(3000);                                             // wait
            Man_Controlcs.SendControllerMsg(1, valves.xtra1gasoff);  // gas off
            Man_Controlcs.SyncWait(30);
            ColorButton(rb_Xtra1, 0);

            ColorButton(rb_Xtra2, 3);
            this.rb_RGas.Text = "Gas";
            ColorButton(rb_Xtra2, 0);

            this.rb_RGas.Text = "none";
            ColorButton(rb_RGas, 0);
            //all  done
            SafeSetStatus("System Pressurization Complete.....");
            Man_Controlcs.WriteStatus("Pressurize", "Pressurization Complete...");
            globals.bFluidicsBusy = false;
        }
        // Next Activator 1
        public void DoAct1(string cmd)
        {
            SafeSetStatus("Doing Activator 1.....");
            //Check if we are still waiting
            if (bWait || globals.bFluidicsBusy)
                SyncWait(100);

            string[] Cmdsplit = cmd.Split(',');

            globals.bFluidicsBusy = true;

            // First secelct ByPass or Cols, bypass will allow reagent to flow fast
            ByPassCol(Cmdsplit[1], Cmdsplit[2]);

            //Next Open the act1 valve and the gas
            Man_Controlcs.WriteStatus("Activator", "Activator 1 On");
            SafeSetStatus("Activator 1 On.....");
            ColorButton(rb_Act1, 1);

            Man_Controlcs.SendControllerMsg(1, valves.act1on);
            SyncWait(80);
            Man_Controlcs.WriteStatus("Activator", "Activator 1 Gas On");
            ColorButton(rb_RGas, 1);

            this.rb_RGas.Text = "Act1";

            Man_Controlcs.SendControllerMsg(1, valves.act1gason);
            SyncWait(80);


            //Now Open ByPass or Pump and Execute
            TrainBPumpByPass(Cmdsplit[3], Cmdsplit[4]);

            //Reset Everything and Done
            Man_Controlcs.WriteStatus("Activator", "Activator 1 Off");
            ColorButton(rb_Act1, 0);

            Man_Controlcs.SendControllerMsg(1, valves.act1off);  //close the Wash A  Valve
            SyncWait(100);
            Man_Controlcs.WriteStatus("Activator", "Activator 1 Gas Off");
            SafeSetStatus("Activator 1 Off.....");
            ColorButton(rb_RGas, 0);

            this.rb_RGas.Text = "none";

            Man_Controlcs.SendControllerMsg(1, valves.act1gasoff);  // gas off
            SyncWait(100);

            TrainBEnd();

            globals.bFluidicsBusy = false;
            SafeSetStatus("Activator 1 Complete.....");
        }
        private void AmGreenRed(String aP, int onoff)
        {
            string[] parts = aP.Split(',');
            int port = int.Parse(parts[2]);

            switch (port)
            {
                case 1:
                    if (onoff == 1)
                        ColorButton(rb_Am1, 1);
                    else
                        ColorButton(rb_Am1, 0);
                    break;
                case 2:
                    if (onoff == 1)
                        ColorButton(rb_Am2, 1);
                    else
                        ColorButton(rb_Am2, 0);
                    break;
                case 3:
                    if (onoff == 1)
                        ColorButton(rb_Am3, 1);
                    else
                        ColorButton(rb_Am3, 0);
                    break;
                case 4:
                    if (onoff == 1)
                        ColorButton(rb_Am4, 1);
                    else
                        ColorButton(rb_Am4, 0);
                    break;
                case 5:
                    if (onoff == 1)
                        ColorButton(rb_Am5, 1);
                    else
                        ColorButton(rb_Am5, 0);
                    break;
                case 6:
                    if (onoff == 1)
                        ColorButton(rb_Am6, 1);
                    else
                        ColorButton(rb_Am6, 0);
                    break;
                case 7:
                    if (onoff == 1)
                        ColorButton(rb_Am7, 1);
                    else
                        ColorButton(rb_Am7, 0);
                    break;
                case 8:
                    if (onoff == 1)
                        ColorButton(rb_Am8, 1);
                    else
                        ColorButton(rb_Am8, 0);
                    break;
                case 9:
                    if (onoff == 1)
                        ColorButton(rb_Am9, 1);
                    else
                        ColorButton(rb_Am9, 0);
                    break;
                case 10:
                    if (onoff == 1)
                        ColorButton(rb_Am10, 1);
                    else
                        ColorButton(rb_Am10, 0);
                    break;
                case 11:
                    if (onoff == 1)
                        ColorButton(rb_Am11, 1);
                    else
                        ColorButton(rb_Am11, 0);
                    break;
                case 12:
                    if (onoff == 1)
                        ColorButton(rb_Am12, 1);
                    else
                        ColorButton(rb_Am12, 0);
                    break;
                case 13:
                    if (onoff == 1)
                        ColorButton(rb_Am13, 1);
                    else
                        ColorButton(rb_Am13, 0);
                    break;
                case 14:
                    if (onoff == 1)
                        ColorButton(rb_Am14, 1);
                    else
                        ColorButton(rb_Am14, 0);
                    break;
            }
        }
        // Next Activator 1 + Base
        public void DoAct1Base(string cmd)
        {
            SafeSetStatus("Doing Activator 1 plus Base.....");
            //Check if we are still waiting
            if (bWait || globals.bFluidicsBusy)
                SyncWait(100);

            string[] Cmdsplit = cmd.Split(',');

            globals.bFluidicsBusy = true;

            //First Open the activator, base and gas
            Man_Controlcs.WriteStatus("Activator + Base", "Activator 1 On");
            SafeSetStatus("Activator +  Base On.....");
            ColorButton(rb_Act1, 1);

            Man_Controlcs.SendControllerMsg(1, valves.act1on);
            SyncWait(70);

            Man_Controlcs.WriteStatus("Activator + Base", "Amidite -" + cAmPort + " On");
            string amOpen = cAmPort + ",1\n";
            TurnAmGasOn(amOpen);  //open the amidite gas
            SyncWait(70);
            AmGreenRed(cAmPort, 1);
            Man_Controlcs.SendControllerMsg(1, amOpen);
            SyncWait(70);
            //turn gas on
            TurnAmGasOn(amOpen);
            SyncWait(60);
            Man_Controlcs.WriteStatus("Activator + Base", "Activator 1 Gas On");
            ColorButton(rb_RGas, 1);

            this.rb_RGas.Text = "Act1";

            Man_Controlcs.SendControllerMsg(1, valves.act1gason);
            SyncWait(100);

            // Next Process ByPass or Cols
            // First secelct ByPass or Cols, bypass will allow reagent to flow fast
            ByPassCol(Cmdsplit[1], Cmdsplit[2]);

            //Now Open ByPass or Pump and Execute
            TrainBPumpByPass(Cmdsplit[3], Cmdsplit[4], true);

            //Reset Everything and Done
            Man_Controlcs.WriteStatus("Activator + Base", "Activator 1 Off");
            ColorButton(rb_Act1, 0);

            Man_Controlcs.SendControllerMsg(1, valves.act1off);  //close the Activator  Valve
            SyncWait(100);
            Man_Controlcs.WriteStatus("Activator + Base", "Amidite " + cAmPort + " Off");
            SafeSetStatus("Activator + Base Off.....");
            AmGreenRed(cAmPort, 0);
            string amClose = cAmPort + ",0\n";
            Man_Controlcs.SendControllerMsg(1, amClose);  //close the amidtie
            SyncWait(100);
            Man_Controlcs.WriteStatus("Activator + Base", "Activator 1 Gas Off");
            ColorButton(rb_RGas, 0);

            this.rb_RGas.Text = "none";

            Man_Controlcs.SendControllerMsg(1, valves.act1gasoff);  // gas off
            SyncWait(100);
            TurnAmGasOff(amOpen);
            SyncWait(60);


            TrainBEnd();

            globals.bFluidicsBusy = false;
            SafeSetStatus("Activator 1 Plus Base Complete.....");
        }
        // Next Activator 2
        public void DoAct2(string cmd)
        {
            SafeSetStatus("Doing Activator 2.....");
            //Check if we are still waiting
            if (bWait || globals.bFluidicsBusy)
                SyncWait(100);

            string[] Cmdsplit = cmd.Split(',');

            globals.bFluidicsBusy = true;

            //First Open the wash valve and the gas
            Man_Controlcs.WriteStatus("Activator", "Activator 2 On");
            SafeSetStatus("Activator 2 On.....");
            ColorButton(rb_Act2, 1);

            Man_Controlcs.SendControllerMsg(1, valves.act2on);
            SyncWait(200);
            Man_Controlcs.WriteStatus("Activator", "Activator 2 Gas On");
            ColorButton(rb_RGas, 1);

            this.rb_RGas.Text = "Act2";

            Man_Controlcs.SendControllerMsg(1, valves.act2gason);
            SyncWait(60);

            // Next Process ByPass or Cols
            // First secelct ByPass or Cols, bypass will allow reagent to flow fast
            ByPassCol(Cmdsplit[1], Cmdsplit[2]);

            //Now Open ByPass or Pump and Execute
            TrainBPumpByPass(Cmdsplit[3], Cmdsplit[4]);

            //Reset Everything and Done
            Man_Controlcs.WriteStatus("Activator", "Activator 2 Off");
            ColorButton(rb_Act2, 0);

            Man_Controlcs.SendControllerMsg(1, valves.act2off);  //close the Wash A  Valve
            SyncWait(100);
            Man_Controlcs.WriteStatus("Activator", "Activator 2 Gas Off");
            SafeSetStatus("Activator 2 Off.....");

            ColorButton(rb_RGas, 0);

            this.rb_RGas.Text = "none";

            Man_Controlcs.SendControllerMsg(1, valves.act2gasoff);  // gas off
            SyncWait(100);

            TrainBEnd();

            globals.bFluidicsBusy = false;
            SafeSetStatus("Activator 2 Complete.....");

        }
        // Next Activator 2 + Base
        public void DoAct2Base(string cmd)
        {
            SafeSetStatus("Doing Activator 2 Plus Base.....");
            //Check if we are still waiting
            if (bWait || globals.bFluidicsBusy)
                SyncWait(100);

            string[] Cmdsplit = cmd.Split(',');

            globals.bFluidicsBusy = true;

            //First Open the activator, base and gas
            Man_Controlcs.WriteStatus("Activator + Base", "Activator 2 On");
            SafeSetStatus("Activator 2 + Base On.....");

            ColorButton(rb_Act2, 1);

            Man_Controlcs.SendControllerMsg(1, valves.act2on);
            SyncWait(80);
            Man_Controlcs.WriteStatus("Activator + Base", "Amidite " + cAmPort + " On");
            string amOpen = cAmPort + ",1\n";
            AmGreenRed(amOpen, 1);
            Man_Controlcs.SendControllerMsg(1, amOpen);
            SyncWait(100);
            TurnAmGasOn(amOpen);
            SyncWait(100);
            Man_Controlcs.WriteStatus("Activator + Base", "Activator 2 Gas On");
            ColorButton(rb_RGas, 1);

            this.rb_RGas.Text = "Act2";

            Man_Controlcs.SendControllerMsg(1, valves.act2gason);
            SyncWait(100);

            // Next Process ByPass or Cols
            // First secelct ByPass or Cols, bypass will allow reagent to flow fast
            ByPassCol(Cmdsplit[1], Cmdsplit[2]);

            //Now Open ByPass or Pump and Execute
            TrainBPumpByPass(Cmdsplit[3], Cmdsplit[4], true);

            //Reset Everything and Done
            Man_Controlcs.WriteStatus("Activator + Base", "Activator 2 Off");
            ColorButton(rb_Act2, 0);

            Man_Controlcs.SendControllerMsg(1, valves.act2off);  //close the Activator  Valve
            SyncWait(100);
            Man_Controlcs.WriteStatus("Activator + Base", "Amidite " + cAmPort + " Off");
            SafeSetStatus("Activator 2 + Base Off.....");

            string amClose = cAmPort + ",0\n";
            AmGreenRed(amClose, 0);
            Man_Controlcs.SendControllerMsg(1, amClose);  //close the amidtie
            SyncWait(100);
            Man_Controlcs.WriteStatus("Activator + Base", "Activator 2 Gas Off");
            ColorButton(rb_RGas, 0);

            this.rb_RGas.Text = "none";

            Man_Controlcs.SendControllerMsg(1, valves.act2gasoff);  // gas off
            SyncWait(100);
            TurnAmGasOff(amOpen);  //amidite gas off

            //now close the train
            TrainBEnd();

            globals.bFluidicsBusy = false;
            SafeSetStatus("Activator 2 Plus Base Complete.....");

        }
        //  Base
        // Note to self - if we SyncWait the procedure stops until complete, but if we use await PutTaskDelay then the thread
        // continues and runs the tasks in parallel, in each their own thread...so both are non thread blocking...
        public void DoBase(string cmd)
        {
            SafeSetStatus("Doing Base.....");
            //Check if we are still waiting
            if (bWait || globals.bFluidicsBusy)
                SyncWait(100);

            string[] Cmdsplit = cmd.Split(',');

            globals.bFluidicsBusy = true;

            // First secelct ByPass or Cols, bypass will allow reagent to flow fast
            ByPassCol(Cmdsplit[1], Cmdsplit[2]);

            //open the amdite
            Man_Controlcs.WriteStatus("Amidite", "Amidite " + cAmPort + " On");
            SafeSetStatus("Amidite - " + cAmPort + " On.....");

            string amOpen = cAmPort + ",1\n";
            AmGreenRed(amOpen, 1);
            Man_Controlcs.SendControllerMsg(1, amOpen);  //open the amidtie
            SyncWait(100);

            //turn the gas on
            TurnAmGasOn(amOpen);
            SyncWait(60);
            Man_Controlcs.WriteStatus("Amidite", "Starting Pumping ");

            TrainBPumpByPass(Cmdsplit[3], Cmdsplit[4], true);

            //Reset Everything and Done
            Man_Controlcs.WriteStatus("Amidite", "Amidite " + cAmPort + " Off");
            SafeSetStatus("Amidite - " + cAmPort + " Off.....");

            string amClose = cAmPort + ",0\n";
            AmGreenRed(amClose, 0);
            Man_Controlcs.SendControllerMsg(1, amClose);  //close the amidtie
            SyncWait(100);
            TurnAmGasOff(amOpen);
            SyncWait(60);

            Man_Controlcs.WriteStatus("Amidite", "Closed Valves...");
            TrainBEnd();

            SafeSetStatus("Doing Base Complete.....");
            Man_Controlcs.WriteStatus("Amidite", "Amidite Delivery Done...");
            globals.bFluidicsBusy = false;
        }
        // Amidites
        public void DoAmidite(string cmd, string valve)
        {
            SafeSetStatus("Doing Amidites.....");
            //Check if we are still waiting
            if (bWait || globals.bFluidicsBusy)
                SyncWait(100);


            string[] Cmdsplit = cmd.Split(',');

            globals.bFluidicsBusy = true;

            // First secelct ByPass or Cols, bypass will allow reagent to flow fast
            ByPassCol(Cmdsplit[1], Cmdsplit[2]);

            //Next Open the amidite valve and the gas
            string Am_Valve_Cmd = valve + ",1\n";
            string Am_Valve_Cls = valve + ",0\n";

            //gas on amidite open
            TurnAmGasOn(Am_Valve_Cmd);
            Man_Controlcs.WriteStatus("Amidite", "Amidite " + valve + " On");
            SafeSetStatus("Amidite - " + valve + " On.....");
            AmGreenRed(Am_Valve_Cmd, 1);
            Man_Controlcs.SendControllerMsg(1, Am_Valve_Cmd);
            SyncWait(70);

            //Now Open ByPass or Pump and Execute
            //we will also have to select the Amidite Pump
            TrainBPumpByPass(Cmdsplit[3], Cmdsplit[4], true);

            //Reset Everything and Done
            //close the amidite valve
            Man_Controlcs.WriteStatus("Amidite", "Amidite " + cAmPort + " Off");
            SafeSetStatus("Amidite - " + valve + " Off.....");
            AmGreenRed(Am_Valve_Cls, 0);
            Man_Controlcs.SendControllerMsg(1, Am_Valve_Cls);  //close the amidite  Valve

            SyncWait(100);
            TurnAmGasOff(Am_Valve_Cmd);
            SyncWait(60);

            //process end
            TrainBEnd();

            globals.bFluidicsBusy = false;

            SafeSetStatus("Amidite Delivery Complete.....");
        }
        /// <summary>
        /// Do PushToCol - Do command to deliver wash B during a protocol
        /// controls an amount dictated by the DeadVolume Spec in the Configuration File
        /// Can be delivered by Pump or Time
        /// </summary>
        /// <param name="command">the command string</param>
        public async void DoPushToCol(string cmd)
        {
            SafeSetStatus("Doing Push To Column.....");
            //Check if we are still waiting
            if (bWait || globals.bFluidicsBusy)
                SyncWait(100);

            int DeadVol = Properties.Settings.Default.DeadVol;
            int DeadTime = (int)Math.Round((decimal)((DeadVol * 60) / 3000), 0);

            Man_Controlcs.WriteStatus("DeadVol Is", DeadVol.ToString());
            Man_Controlcs.WriteStatus("Dead TimeIs ", DeadTime.ToString());

            globals.bFluidicsBusy = true;

            string[] Cmdsplit = cmd.Split(',');

            globals.bFluidicsBusy = true;

            //First Open the wash valve and the gas
            Man_Controlcs.WriteStatus("Push To Column", "Wash B On");
            ColorButton(rb_WashB, 1);

            SyncWait(10);
            Man_Controlcs.SendControllerMsg(1, valves.washb1on);  //Wash B reagent valve Amidite Block 2
            SyncWait(60);
            Man_Controlcs.SendControllerMsg(1, valves.washb2on);  //Wash B reagent valve Amidite Block 2
            SyncWait(60);

            Man_Controlcs.WriteStatus("Push To Column", "Wash B Gas On");

            ColorButton(rb_RGas, 1);
            this.rb_RGas.Text = "WshB";

            Man_Controlcs.SendControllerMsg(1, valves.washbgason); //Gas for Was A on gas valve port 6
            SyncWait(60);

            // Next Process ByPass or Cols -- always to Column
            Man_Controlcs.WriteStatus("Push To Column", "Select To Columns");
            ColorButton(rb_3WBC, 1);
            rb_3WBC.Text = "To Column";

            Man_Controlcs.SendControllerMsg(1, valves.tocol);   //columns selected
            SyncWait(60);
            globals.bColBypass = true;

            Man_Controlcs.WriteStatus("Push To Column", "To Active Columns");
            SelectCols("000000001"); //select which open
            SyncWait(100);


            //Now Open ByPass or Pump and Execute
            if (Cmdsplit[1].Contains("G"))
            {
                Man_Controlcs.WriteStatus("Push To Column", "Pump/Bypass - To ByPass Sected");
                ColorButton(rb_3WBP, 0);
                rb_3WBP.Text = "To Bypass";

                Man_Controlcs.SendControllerMsg(1, valves.topumpbypass); //pump is off and bypass is on
                SyncWait(60);
                globals.bPumpBypass = true;

                string s = DeadTime.ToString().Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty).Replace("\0", String.Empty);
                Man_Controlcs.WriteStatus("Push To Column", "Flowing for" + int.Parse(s).ToString("0") + " seconds");
                ByPassTime(DeadTime.ToString());

            }
            else
            {
                if (globals.bPumpBypass)
                {
                    Man_Controlcs.WriteStatus("Push To Column", "Pump/Bypass - To Pump Selected");

                    ColorButton(rb_3WBP, 1);
                    rb_3WBP.Text = "To Pump";

                    Man_Controlcs.SendControllerMsg(1, valves.topump); //pump is off and bypass is on
                    SyncWait(60);

                    globals.bPumpBypass = false;

                    if (!globals.bAmidReagPump)
                    {
                        Man_Controlcs.WriteStatus("Push To Column", "To Amidite Pump");
                        //ColorButton(rb_3WRA, 1);



                        //   Man_Controlcs.SendControllerMsg(1, valves.toamidpump);
                        SyncWait(60);
                        globals.bAmidReagPump = true;
                    }
                }
                bWait = true;
                ColorButton(rb_RgPump, 1);

                globals.bPumping = true;
                string s = DeadVol.ToString().Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty).Replace("\0", String.Empty);
                Man_Controlcs.WriteStatus("Push To Column", "Pumping " + int.Parse(s).ToString("0") + "uL");
                String pCmd = "P," + DeadVol.ToString() + "\n";
                globals.iAmPumpVol = globals.iAmPumpVol + DeadVol;

                //if the volume is less than total pump volume then close the recycle valve
                Man_Controlcs.SendControllerMsg(1, pCmd);


                //wait for the pump to finish pumping
                Task tUp = Task.Run(async () => { await PumpHangOut(); });

                while (globals.bPumping)
                {
                    SyncWait(100);
                    if (tUp.IsCompleted) { globals.bPumping = false; }
                }

                SyncWait(100);

                //now dwell to fill the pump with the valves open
                if (globals.iReagPumpDwell > 0)
                {
                    bdwelling = true;

                    Task tDwell = Task.Run(async () => { await DwellPump(globals.iAmPumpVol); });

                    while (bdwelling)
                    {
                        await PutTaskDelay(50);
                        if (tDwell.IsCompleted) { bdwelling = false; }
                    }
                    SyncWait(50);
                }


                ColorButton(rb_RgPump, 0);

                bWait = false;
            }

            while (bWait)
                await PutTaskDelay(50);

            //Reset Everything and Done
            Man_Controlcs.WriteStatus("Push To Column", "Wash B Off");
            ColorButton(rb_WashB, 0);

            SyncWait(10);
            Man_Controlcs.SendControllerMsg(1, valves.washb1off);  //close the Wash B  Valve
            SyncWait(60);
            Man_Controlcs.SendControllerMsg(1, valves.washb2off);  //close the Wash B  Valve
            SyncWait(60);

            Man_Controlcs.WriteStatus("Push To Column", "Wash B Gas Off");
            ColorButton(rb_RGas, 0);

            this.rb_RGas.Text = "none";
            Man_Controlcs.SendControllerMsg(1, valves.washbgasoff);  // gas off
            SyncWait(80);

            TrainBEnd();

            globals.bFluidicsBusy = false;
            SafeSetStatus("Push to Column Complete.....");
        }
        private async Task DwellPump(int Vol)
        {
            int dwelltime = Vol * globals.iReagPumpDwell;
            bool bdwelling = true;
            int tm = 0;

            while (bdwelling)
            {
                await PutTaskDelay(500);


                if (tm == 2)
                {
                    SafeSetStatus("Pump Dwell Wait...." + dwelltime.ToString("0") + "ms to go");
                    tm = 0;
                }
                tm++;

                dwelltime = dwelltime - 500;
                Debug.WriteLine("Dwell Time Left = " + dwelltime.ToString() + "ms");
                if (dwelltime < 100)
                    bdwelling = false;
            }
        }
        private void TurnAmGasOn(string Am_Valve_Cmd)
        {
            SafeSetStatus("Turning A Train Gas On.....");
            string[] parts = Am_Valve_Cmd.Split(',');

            if (parts[2].Contains("14") || parts[2].Contains("13") || parts[2].Contains("12") || parts[2].Contains("11") || parts[2].Contains("10") || parts[2].Contains("9"))
            {
                Man_Controlcs.WriteStatus("Amidite Gas", "Towards Top Amidites");
                SafeSetStatus("Amidite - Gas to Top.....");
                ColorButton(rb_AGas, 0);

                this.rb_AGas.Text = "top";

                Man_Controlcs.SendControllerMsg(1, valves.pressuretop);
            }
            else
            {
                Man_Controlcs.WriteStatus("Amidite Gas", "Towards Bottom Amidites");
                SafeSetStatus("Amidite - Gas to Bottom.....");

                ColorButton(rb_AGas, 1);
                this.rb_AGas.Text = "Botm";

                Man_Controlcs.SendControllerMsg(1, valves.pressurebot);
            }

            SyncWait(80);
            SafeSetStatus("Amidite Gas Complete.....");
            return;

        }
        private void TurnAmGasOff(string Am_Valve_Cmd)
        {
            SafeSetStatus("Turning Train B Gas On.....");
            //will switch the gas to the opposite one to relieve some pressure
            string[] parts = Am_Valve_Cmd.Split(',');

            if (parts[2].Contains("14") || parts[2].Contains("13") || parts[2].Contains("12") || parts[2].Contains("11") || parts[2].Contains("10") || parts[2].Contains("9"))
            {
                Man_Controlcs.WriteStatus("Amidite Gas", "Towards Bottom Amidites");
                SafeSetStatus("Amidite - Gas to Bottom.....");

                ColorButton(rb_AGas, 1);
                this.rb_AGas.Text = "Botm";

                Man_Controlcs.SendControllerMsg(1, valves.pressurebot);
            }
            else
            {
                Man_Controlcs.WriteStatus("Amidite Gas", "Towards Top Amidites");
                SafeSetStatus("Amidite - Gas to Top.....");
                ColorButton(rb_AGas, 0);

                this.rb_AGas.Text = "Top";

                Man_Controlcs.SendControllerMsg(1, valves.pressuretop);
            }

            SyncWait(80);
            SafeSetStatus("Amidite Gas Complete.....");
            return;

        }
        private void TrainAPumpByPass(string cmd3, string cmd4)
        {
            SafeSetStatus("Switching Pump Bypass Valve.....");
            //Now Open ByPass or Pump and Execute
            if (cmd3.Contains("B"))
            {
                Man_Controlcs.WriteStatus("Pump/Bypass", "Bypass Selected");
                SafeSetStatus("Pump Bypass selecting to Bypass.....");
                ColorButton(rb_3WBP, 0);
                rb_3WBP.Text = "To Bypass";

                Man_Controlcs.SendControllerMsg(1, valves.topumpbypass); //pump is off and bypass is on
                SyncWait(60);
                globals.bPumpBypass = true;

                string s = cmd4.Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty).Replace("\0", String.Empty);
                Man_Controlcs.WriteStatus("Bypass", "Flowing to Bypass for " + int.Parse(s).ToString("0") + "seconds");
                SafeSetStatus("Flowing to Bypass for " + int.Parse(s).ToString("0") + "seconds");
                ByPassTime(cmd4);

                bWait = false;
            }
            else
            {
                Man_Controlcs.WriteStatus("Pump/ByPass", "To Pump");
                SafeSetStatus("Pump ByPass to Pump Selected");
                ColorButton(rb_3WBP, 1);
                rb_3WBP.Text = "To Pump";

                Man_Controlcs.SendControllerMsg(1, valves.topump); //pump is on and bypass is on
                SyncWait(60);
                globals.bPumpBypass = false;

                Man_Controlcs.WriteStatus("Pump Select", "Pump");
                SafeSetStatus("Pump Slection -- Pump Selected");
                //this.rb_3WRA.BackColor = cOn;
                //Man_Controlcs.SendControllerMsg(1, valves.toamidpump); //only one pump no switching switch to amidite pump
                //SyncWait(70);
                globals.bAmidReagPump = true;

                //seal off the back end to avoid wasting amidite
                bWait = true;
                string s = cmd4.Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty).Replace("\0", String.Empty);
                globals.bPumping = true;
                Man_Controlcs.WriteStatus("Pump", "Pumping " + int.Parse(s).ToString("0") + "uL");
                SafeSetStatus("Pumping " + int.Parse(s).ToString("0") + "uL");
                ColorButton(rb_RgPump, 1);

                int corVol = 0;
                string toPump = String.Empty;

                if (Int32.TryParse(cmd4, out corVol))
                {
                    int corAVol = Convert.ToInt32(corVol * globals.dReagentCF);
                    toPump = corAVol.ToString("0");
                }
                else
                {
                    Man_Controlcs.WriteStatus("Pump", "Could not correct Pump Volume " + cmd4);
                    toPump = cmd4;
                }


                globals.bPumping = true;
                String pCmd = "P," + toPump + "\n";  //pump with the amidite pump
                Debug.WriteLine("Pumped -   " + toPump + "uL");
                //globals.iAmPumpVol = globals.iAmPumpVol + corVol;  //needed for recycling keep track of the amount pumped to the amidite pump
                Man_Controlcs.SendControllerMsg(1, pCmd);

                //wait for the pump to finish pumping
                Task tUp = Task.Run(async () => { await PumpHangOut(); });

                while (globals.bPumping)
                {
                    SyncWait(100);
                    if (tUp.IsCompleted) globals.bPumping = false;
                }
                SyncWait(100);

                //now dwell to fill the pump with the valves open
                if (globals.iReagPumpDwell > 0)
                {
                    int dwelltime = Int32.Parse(toPump) * globals.iReagPumpDwell;
                    Man_Controlcs.WriteStatus("Pump", "Starting Pump Dwell for " + dwelltime.ToString("0") + " ms");
                    SafeSetStatus("Starting Pump Dwell for " + dwelltime.ToString("0") + " ms");

                    bdwelling = true;

                    Task tDwell = Task.Run(async () => { await DwellPump(Int32.Parse(toPump)); });

                    while (bdwelling)
                    {
                        SyncWait(50);
                        if (tDwell.IsCompleted) { bdwelling = false; }
                    }
                    SyncWait(50);
                }


                ColorButton(rb_RgPump, 0);

                globals.bAmidReagPump = false;
                Man_Controlcs.WriteStatus("Pump", "Pump Dwell Complete");
                SafeSetStatus("Pump Dwell Complete...");
                bWait = false;
            }


            while (bWait)
                SyncWait(50);

            SafeSetStatus("Pumping or Bypass Complete.....");

        }
        private void TrainBPumpByPass(string cmd3, string cmd4, bool isBase = false)
        {
            SafeSetStatus("Switching Pump Bypass Valve.....");
            //Now Open ByPass or Pump and Execute
            if (cmd3.Contains("B"))
            {
                Man_Controlcs.WriteStatus("Pump/Bypass", "Bypass Selected");
                SafeSetStatus("Pump Bypass selecting to Bypass.....");
                ColorButton(rb_3WBP, 0);
                rb_3WBP.Text = "To Bypass";

                Man_Controlcs.SendControllerMsg(1, valves.topumpbypass); //pump is off and bypass is on
                SyncWait(60);
                globals.bPumpBypass = true;

                string s = cmd4.Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty).Replace("\0", String.Empty);
                Man_Controlcs.WriteStatus("Bypass", "Flowing to Bypass for " + int.Parse(s).ToString("0") + "seconds");

                SafeSetStatus("Flowing to Bypass for " + int.Parse(s).ToString("0") + "seconds");
                ByPassTime(cmd4);

            }
            else
            {
                Man_Controlcs.WriteStatus("Pump/ByPass", "To Pump");
                SafeSetStatus("Pump ByPass to Pump Selected");
                ColorButton(rb_3WBP, 1);
                rb_3WBP.Text = "To Pump";

                Man_Controlcs.SendControllerMsg(1, valves.topump); //pump is on and bypass is off
                SyncWait(60);
                globals.bPumpBypass = false;

                Man_Controlcs.WriteStatus("Pump Select", "Pump");
                SafeSetStatus("Pump Slection -- Pump Selected");

                //this.rb_3WRA.BackColor = cOn;
                //Man_Controlcs.SendControllerMsg(1, valves.toamidpump); //only one pump no switching switch to amidite pump
                globals.bAmidReagPump = true;

                //seal off the back end to avoid wasting amidite
                bWait = true;

                string s = cmd4.Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty).Replace("\0", String.Empty);

                globals.bPumping = true;

                Man_Controlcs.WriteStatus("Pump", "Pumping " + int.Parse(s).ToString("0") + "uL");
                SafeSetStatus("Pumping " + int.Parse(s).ToString("0") + "uL");
                ColorButton(rb_RgPump, 1);

                int corVol = 0;
                double iCycleCorr = 1;
                string toPump = String.Empty;

                if (Int32.TryParse(cmd4, out corVol))
                {
                    double corforbase = 1.0;

                    if (isBase)
                    {
                        if (globals.bScaleLong)
                        {
                            //cycle correction - add 0.5% more amidite per cycle
                            iCycleCorr = 1.0 + ((double)globals.iCycle / (double)200);

                            Man_Controlcs.ReceiveStatus("Pump", "Long Oligo Scale Factor is " + iCycleCorr.ToString("0.00"));
                        }

                        //add factor for cycle
                        if (rb_Smart.Checked)
                            corforbase = globals.dAmiditeCF * (sameBase.Length * 0.8) * globals.scalefactor * iCycleCorr;
                        else
                            corforbase = globals.dAmiditeCF * globals.scalefactor * iCycleCorr;

                        Man_Controlcs.ReceiveStatus("Pump", "Base Correction Factor is " + corforbase.ToString("0.0"));
                        Debug.WriteLine("Base Correction factor is " + corforbase.ToString("0.00") + "   Cycle Correction -" + iCycleCorr.ToString("0.00"));
                    }
                    else if (bIsPush)
                    {
                        //decrease the amount of push from the block by the extra amidite added for each cycle
                        //down to no push...the amidite is filled clear to the column
                        if (globals.bScaleLong)
                            iCycleCorr = 1.0 - ((double)globals.iCycle / (double)200);

                        if (iCycleCorr > 0)
                            corforbase = globals.dAmiditeCF * iCycleCorr;
                        else
                            corforbase = globals.dAmiditeCF * 0.001;  //amost nothing!!!

                        Man_Controlcs.ReceiveStatus("Pump", "Push Correction Factor is " + corforbase.ToString("0.00"));
                        Debug.WriteLine("Push Correction factor is " + corforbase.ToString("0.00"));
                    }
                    else
                        corforbase = globals.dAmiditeCF;

                    int corAVol = Convert.ToInt32(corVol * corforbase);
                    if (isBase)  //check to make sure it does not exceed 1200uL
                    {
                        if (((double)corAVol / (double)globals.dAmiditeCF) > 1200)
                            corAVol = Convert.ToInt32(1200 * globals.dAmiditeCF);
                    }
                    toPump = corAVol.ToString("0");
                    Man_Controlcs.WriteStatus("Pump", "Corrected Volume is " + toPump + "uL");
                }
                else
                {
                    Man_Controlcs.WriteStatus("Pump", "Could not correct Pump Volume " + cmd4);
                    toPump = cmd4;
                }

                globals.bPumping = true;

                String pCmd = "P," + toPump + "\n";  //pump with the amidite pump
                Debug.WriteLine("Pumped -   " + toPump + "uL");
                //globals.iAmPumpVol = globals.iAmPumpVol + corVol;  //needed for recycling keep track of the amount pumped to the amidite pump


                if (bIsPush) { bIsPush = false; }

                globals.bPumping = true;
                Man_Controlcs.SendControllerMsg(1, pCmd);
                SyncWait(40);
                Man_Controlcs.WriteStatus("Pump", "Pumping " + toPump + "uL");


                //wait for the pump to finish pumping
                Task tUp = Task.Run(async () => { await PumpHangOut(); });

                while (globals.bPumping)
                {
                    SyncWait(100);
                    if (tUp.IsCompleted) globals.bPumping = false;
                }

                Man_Controlcs.SyncWait(200);

                Man_Controlcs.WriteStatus("Pump", "Recieved Done from Pump");

                //now dwell to fill the pump with the valves open
                if (globals.iReagPumpDwell > 0)
                {
                    bool bdwelling = true;

                    int dwelltime = Int32.Parse(toPump) * globals.iReagPumpDwell;
                    Man_Controlcs.WriteStatus("Pump", "Starting Pump Dwell for " + dwelltime.ToString("0") + " ms");
                    SafeSetStatus("Starting Pump Dwell for " + dwelltime.ToString("0") + " ms");

                    Task tDwell = Task.Run(async () => { await DwellPump(Int32.Parse(toPump)); });

                    while (bdwelling)
                    {
                        SyncWait(50);
                        if (tDwell.IsCompleted) { bdwelling = false; }
                    }
                    SyncWait(50);
                    Man_Controlcs.WriteStatus("Pump", "Finished Dwell " + dwelltime.ToString("0") + " ms");
                    SafeSetStatus("Pump Dwell Complete...");
                }

                ColorButton(rb_RgPump, 0);



                globals.bAmidReagPump = false;


            }


            SyncWait(1000);  //extra time to finish everything 
            while (bWait)
                SyncWait(50);

            SafeSetStatus("Pumping or Bypass Complete.....");

        }
        private void TrainAEnd()
        {
            SafeSetStatus("Closing Train A.....");
            //close the columns
            if (bColsOpen)
            {
                Man_Controlcs.WriteStatus("Columns", "Columns Closed");
                SafeSetStatus("Columns Closed");
                CloseCols("000000001");
                bColsOpen = false;
                SyncWait(230);
            }
            //leave towards columns
            if (!globals.bColBypass)
            {
                Man_Controlcs.WriteStatus("Column ByPass", "To Columns");
                SafeSetStatus("Columns Bypass to Columns Selected");
                ColorButton(rb_3WBC, 1);
                rb_3WBC.Text = "To Column";

                Man_Controlcs.SendControllerMsg(1, valves.tocol);   //leave flow to column....bypass closed
                SyncWait(60);
                globals.bColBypass = true;
            }
            SyncWait(100);

            if (globals.bPumpBypass)
            {
                Man_Controlcs.WriteStatus("Pump ByPass", "To Pump");
                SafeSetStatus("Pump Bypass to Pump Selected....");
                ColorButton(rb_3WBP, 1);
                rb_3WBP.Text = "To Pump";

                Man_Controlcs.SendControllerMsg(1, valves.topump);  //towards pump to keep things from leaking
                SyncWait(60);

                globals.bPumpBypass = false;

            }

            Debug.WriteLine(" Waiting = " + globals.bWaiting.ToString() + "       Fluidics Status " + globals.bFluidicsBusy.ToString());

            //add 0.5second to let everything get caught up...
            SyncWait(500);
            SafeSetStatus("Train A Closed.....");
        }
        private void TrainBEnd()
        {
            SafeSetStatus("Closing Train B.....");
            //close the columns
            if (bColsOpen)
            {
                Man_Controlcs.WriteStatus("Columns", "Columns Closed");
                SafeSetStatus("Columns Closed.....");
                CloseCols("000000001");
                bColsOpen = false;
                SyncWait(230);
            }

            //leave towards the columns
            if (!globals.bColBypass) //if towards the bypass is on leave towards the columns avoid leaking
            {
                Man_Controlcs.WriteStatus("Column Bypass", "To Columns");
                SafeSetStatus("Columns Bypass to Columns Selected....");
                ColorButton(rb_3WBC, 1);
                rb_3WBC.Text = "To Column";
                SyncWait(10);

                Man_Controlcs.SendControllerMsg(1, valves.tocol);   //columns closed
                SyncWait(30);
                globals.bColBypass = true;
            }
            SyncWait(100);

            //always leave towards pump to deadhead at the pump, with recycle closed or waste valve closed, everything closed
            if (globals.bPumpBypass)
            {
                Man_Controlcs.WriteStatus("Pump ByPass", "To Pump");
                SafeSetStatus("Pump Bypass Valve to Pump Selected.....");
                ColorButton(rb_3WBP, 1);
                rb_3WBP.Text = "To Pump";
                SyncWait(10);

                Man_Controlcs.SendControllerMsg(1, valves.topump);  //towards pump
                SyncWait(60);

                globals.bPumpBypass = false;
            }

            Debug.WriteLine(" Waiting = " + globals.bWaiting.ToString() + "     Fluidics Status " + globals.bFluidicsBusy.ToString());

            //add 0.5second to let everything get caught up...
            SyncWait(500);
            SafeSetStatus("Train B Closed.....");
        }
        private void ByPassCol(string cmd1, string cmd2)
        {
            SafeSetStatus("Selecting ByPass Columns.....");
            if (cmd1.Contains("B"))
            {
                Man_Controlcs.WriteStatus("Col/Bypass Valve", "To Bypass");
                SafeSetStatus("Column Bypass to Bypass.....");
                ColorButton(rb_3WBC, 0);
                rb_3WBC.Text = "To Bypass";

                Man_Controlcs.SendControllerMsg(1, valves.tocolbypass);   //columns closed
                SyncWait(60);
                globals.bColBypass = false;

                //make sure columns are closed
                Man_Controlcs.WriteStatus("Columns", "Closed Columns");
                SafeSetStatus("Closed Columns.....");
                CloseCols("111111111");
                bColsOpen = false;  //all closed
                SafeSetStatus("Column ByPass Selected - To ByPass.....");
            }
            else
            {
                Man_Controlcs.WriteStatus("Col/Bypass Valve", "To Column");
                SafeSetStatus("Column Bypass Valve to Columns.....");
                ColorButton(rb_3WBC, 1);
                rb_3WBC.Text = "To Column";

                Man_Controlcs.SendControllerMsg(1, valves.tocol);   //columns selected
                SyncWait(60);
                globals.bColBypass = true;

                Man_Controlcs.WriteStatus("Columns", "Columns Open");
                SafeSetStatus("Opening Columns.....");
                SelectCols(cmd2); //select which open
                bColsOpen = true;  // make sure we set the flag
                SyncWait(140);
                SafeSetStatus("Column ByPass Selected - To Columns.....");
            }



        }
        private void SetAll()
        {

        }
    }

}
