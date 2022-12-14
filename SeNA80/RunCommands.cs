using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;


namespace SeNA80
{
    public partial class SeNARun
    {
        // int iTickCntr = 0;
        public string cAmPort = "";
        /* Function to fill the list box
         * After each subsequenct cycle the listbox will be filled with the 
         * currently running protocols or sub protocol
         */
        public void FillTheList(String ProtoFile)
        {
            //detach the event handler
            //this.ProtoListView.SelectedIndexChanged -= this.ProtoListView_SelectedIndexChanged;
            //open the prep protocol and put it in the list box
            string[] lblines = new string[100];

            //
            String fName = globals.protocol_path + ProtoFile;
            try { lblines = File.ReadAllLines(fName); }
            catch { MessageBox.Show("Protocol File Not Found", "Error"); }

            //find the line where volumes start
            int cntr = 0;
            foreach (string line in lblines)
            {
                if (!(line.Contains("[Volumes]")))
                    cntr = cntr + 1;
                else
                    break;
            }


            // first Check to see if there is already info in the listbox (i.e. we came back after a reload)
            // if yes, clear it
            if (lb_CurProtocol.SelectedItems.Count > 0)
                lb_CurProtocol.Items.Clear();

            //try listboxview
            if (ProtoListView.Items.Count > 0)
                ProtoListView.Items.Clear();

            //fill the list box
            //July 13, 2018 try to support repeat so we can then have autoscale factor to support multiple scales in the same run
            for (int j = 1; j < cntr; j++)
            {

                string curline = lblines[j];
                if (curline.Contains("Repeat"))
                {
                    string[] repeatfor = curline.Split(',');  //contains 0 = repeat 1 = times
                    int times = int.Parse(repeatfor[1]);
                    //now get teh block to repeat
                    string templine = string.Empty;
                    string[] repeatline = new string[50];  //max lines that can be repeated is 10
                    j++; //skip the  current line Repeat
                    //increment the counter
                    int k = 0;

                    //now correct for scale factor
                    if (fName.Contains("apr"))
                    {
                        //add more for amidite scaling factor
                        times = (int)Math.Ceiling(times * globals.scalefactor);  //always round up lowest is 1

                        //first cycle add one if double first checked
                        if (globals.bDoubleFirst && globals.iCycle == 0)
                            times = times + 1;

                        //ever 30 cycles add an additional coupling step to drive coupling for long oligos if checked
                        if (globals.bScaleLong)
                        {
                            if (globals.iCycle > 1 && (globals.iCycle % 30) == 0)
                                globals.cyclestoadd = globals.cyclestoadd + 1;

                            times = times + globals.cyclestoadd;

                            Debug.WriteLine("Amidite Cycles Added = " + globals.cyclestoadd.ToString() + " Repeat = " + times.ToString());

                        }
                    }
                    else
                    {
                        if (globals.noOligos > 2)
                            times = (int)Math.Ceiling(globals.noOligos * 0.6);

                        times = (int)Math.Ceiling(times * globals.avgScale);
                    }
                    //Debug.WriteLine("Scale Times - " + times.ToString("0"));

                    while (!(templine.Contains("End Repeat")))
                    {
                        if (!(templine.Contains("End Repeat")))
                        {
                            templine = lblines[j];
                            repeatline[k] = templine;
                            k++;
                            j++; //increment j to the next line both counters
                        }
                        // Debug.WriteLine("line added to repeat" + templine);
                    }
                    //now stick the repeated lines in the listbox
                    for (int g = 0; g < times; g++)
                    {
                        for (int r = 0; r < k - 1; r++)
                        {
                            lb_CurProtocol.Items.Add(repeatline[r]);
                            //Debug.WriteLine("line added to listbox for repeat" + repeatline[r]);
                        }
                    }
                    j--; //clean up the extra j that gets added in the repeat loop;

                }
                else  //just add the line       
                    lb_CurProtocol.Items.Add(curline);
            }

            //Set Focus   
            lb_CurProtocol.SelectedIndex = 0;
            if (globals.sb == null)
                lb_CurProtocol.Focus();
            else
                globals.sb.Focus();

            //set the text
            Man_Controlcs.LabelSafe(lbl_cysteps, lb_CurProtocol.Items.Count.ToString("0"));
            //refill lblines with the current list
            int itemCount = lb_CurProtocol.Items.Count;
            lblines = new string[itemCount];

            for (int i = 0; i < itemCount; i++)
            {
                lblines[i] = (string)lb_CurProtocol.Items[i];
            }


            foreach (string line in lblines)
            {
                string cmd = string.Empty;
                string parameters = String.Empty;

                int startIndex = line.IndexOf(',');

                if (startIndex > 0)
                {
                    cmd = line.Substring(0, startIndex);
                    parameters = line.Substring(startIndex + 1);
                }
                else
                {
                    cmd = line;
                    parameters = "";
                }
                string[] row = { cmd.Trim(), parameters.Trim() };
                //MessageBox.Show("Cmd- "+cmd.Trim()+"  Parameter -"+parameters.Trim(), "Start Index - " + startIndex.ToString());
                var listViewItem = new ListViewItem(row);
                try { ProtoListView.Items.Add(listViewItem); } catch (Exception e) { Man_Controlcs.WriteStatus("List View Add", e.ToString()); }
            }
            ProtoListView.Items[0].Selected = true;
            ProtoListView.FullRowSelect = true;

            //ProtoListView.Focus();
            //attach the event handler
            //this.ProtoListView.SelectedIndexChanged += new System.EventHandler(this.ProtoListView_SelectedIndexChanged);
            globals.bProtocolsLoaded = true;

        }
        /// <summary>
        /// FilTheLoopListView
        ///         
        /// /// </summary> dummy box to show the looping protocol in a list view
        /// <param name="list"></param>
        public void FillTheLoopListView(string[] list)
        {

            for (int i = 1; i < list.Length; i++)
            {
                string line = list[i];
                string cmd = string.Empty;
                string parameters = String.Empty;


                int startIndex = line.IndexOf(',');

                if (startIndex > 0)
                {
                    cmd = line.Substring(0, startIndex);
                    parameters = line.Substring(startIndex + 1);
                }
                else
                {
                    cmd = line;
                    parameters = "";
                }

                string[] row = { cmd.Trim(), parameters.Trim() };
                //MessageBox.Show("Cmd- "+cmd.Trim()+"  Parameter -"+parameters.Trim(), "Start Index - " + startIndex.ToString());

                var listViewItem = new ListViewItem(row);

                try { LoopStepsListView.Items.Add(listViewItem); } catch (Exception e) { Man_Controlcs.WriteStatus("List View Add Error ", e.ToString()); }
            }
            LoopStepsListView.Items[0].Selected = true;
            LoopStepsListView.FullRowSelect = true;
        }
        private string GetAmidite(string inAm)
        {
            for (int i = 0; i < ibases; i++)
            {
                string[] parts = bases[i].Split(',');
                if (parts[0].Equals(inAm))
                    return parts[1];
            }
            return string.Empty;
        }
        //Turn Cells Off
        private void CellsOff(int cell)
        {
            switch (cell)
            {
                case 1:
                    globals.bUV1On = false;
                    return;
                case 2:
                    globals.bUV2On = false;
                    return;
                case 3:
                    globals.bUV3On = false;
                    return;
                case 4:
                    globals.bUV4On = false;
                    return;
                case 5:
                    globals.bUV5On = false;
                    return;
                case 6:
                    globals.bUV6On = false;
                    return;
                case 7:
                    globals.bUV7On = false;
                    return;
                case 8:
                    globals.bUV8On = false;
                    return;
            }
        }
        /// <summary>
        /// Color Button - sets the background and foreground colors of the controls in valve view
        /// </summary>
        /// <param name="control"> the control</param>
        /// <param name="color"> 0 for red, 1 for green, 2 for blue</param>
        public void ColorButton(RadioButton control, int whichcolor)
        {
            switch (whichcolor)
            {
                case 0:  //inactive 
                    control.BackColor = cOff;
                    control.ForeColor = cOffBack;
                    break;
                case 1:  //on
                    control.BackColor = cOn;
                    control.ForeColor = cOnBack;
                    break;
                case 3:  //gas active 
                    control.BackColor = cGasOn;
                    control.ForeColor = cOnBack;
                    break;
            }
        }
        //Asynchronise task delay
        async Task PutTaskDelay(int secn)
        {
            await Task.Delay(secn);
        }
        private string GetProtocol(char amidite)
        {
            string retProt = string.Empty;
            string inAmidite = Char.ToString(amidite);

            if (globals.i12Ltr != 0)
                inAmidite = GetAmidite(inAmidite);

            if (Properties.Settings.Default.Am_1_lbl.ToString().Equals(inAmidite))
            {
                retProt = Properties.Settings.Default.Am_1_prtcl;
                globals.iAmBottle = 1;
                cAmPort = "V,A,1";
            }
            else if (Properties.Settings.Default.Am_2_lbl.ToString().Equals(inAmidite))
            {
                retProt = Properties.Settings.Default.Am_2_prtcl;
                globals.iAmBottle = 2;
                cAmPort = "V,A,2";
            }
            else if (Properties.Settings.Default.Am_3_lbl.ToString().Equals(inAmidite))
            {
                retProt = Properties.Settings.Default.Am_3_prtcl;
                globals.iAmBottle = 3;
                cAmPort = "V,A,3";
            }
            else if (Properties.Settings.Default.Am_4_lbl.ToString().Equals(inAmidite))
            {
                retProt = Properties.Settings.Default.Am_4_prtcl;
                globals.iAmBottle = 4;
                cAmPort = "V,A,4";
            }
            else if (Properties.Settings.Default.Am_5_lbl.ToString().Equals(inAmidite))
            {
                retProt = Properties.Settings.Default.Am_5_prtcl;
                globals.iAmBottle = 5;
                cAmPort = "V,A,5";
            }
            else if (Properties.Settings.Default.Am_6_lbl.ToString().Equals(inAmidite))
            {
                retProt = Properties.Settings.Default.Am_6_prtcl;
                globals.iAmBottle = 6;
                cAmPort = "V,A,6";
            }
            else if (Properties.Settings.Default.Am_7_lbl.ToString().Equals(inAmidite))
            {
                retProt = Properties.Settings.Default.Am_7_prtcl;
                globals.iAmBottle = 7;
                cAmPort = "V,A,7";
            }
            else if (Properties.Settings.Default.Am_8_lbl.ToString().Equals(inAmidite))
            {
                retProt = Properties.Settings.Default.Am_8_prtcl;
                globals.iAmBottle = 8;
                cAmPort = "V,A,8";
            }
            else if (Properties.Settings.Default.Am_9_lbl.ToString().Equals(inAmidite))
            {
                retProt = Properties.Settings.Default.Am_9_prtcl;
                globals.iAmBottle = 9;
                cAmPort = "V,A,9";
            }
            else if (Properties.Settings.Default.Am_10_lbl.ToString().Equals(inAmidite))
            {
                retProt = Properties.Settings.Default.Am_10_prtcl;
                globals.iAmBottle = 10;
                cAmPort = "V,A,10";
            }
            else if (Properties.Settings.Default.Am_11_lbl.ToString().Equals(inAmidite))
            {
                retProt = Properties.Settings.Default.Am_11_prtcl;
                globals.iAmBottle = 11;
                cAmPort = "V,A,11";
            }
            else if (Properties.Settings.Default.Am_12_lbl.ToString().Equals(inAmidite))
            {
                retProt = Properties.Settings.Default.Am_12_prtcl;
                globals.iAmBottle = 12;
                cAmPort = "V,A,12";
            }
            else if (Properties.Settings.Default.Am_13_lbl.ToString().Equals(inAmidite))
            {
                retProt = Properties.Settings.Default.Am_13_prtcl;
                globals.iAmBottle = 13;
                cAmPort = "V,A,13";
            }
            else if (Properties.Settings.Default.Am_14_lbl.ToString().Equals(inAmidite))
            {
                retProt = Properties.Settings.Default.Am_14_prtcl;
                globals.iAmBottle = 14;
                cAmPort = "V,A,14";
            }
            return retProt;


        }
        private string GetSeq(int tb, int cycle)
        {
            string retSeq = "";

            switch (tb)
            {
                case 0:
                    retSeq = runSequences[1];
                    if (cycle > retSeq.Length)           //this will keep the column from opening during other cycles
                        globals.bCol1 = false;            //there is a problem, it will not be greater until n+1, so there will be one 
                    break;                                      //extra deblock, may want to move deblock to the end of the cycle and do th e
                case 1:                                        //initial deblock as part of the prep cycle
                    retSeq = runSequences[2];
                    if (cycle > retSeq.Length)
                        globals.bCol2 = false;
                    break;
                case 2:
                    retSeq = runSequences[3];
                    if (cycle > retSeq.Length)
                        globals.bCol3 = false;
                    break;
                case 3:
                    retSeq = runSequences[4];
                    if (cycle > retSeq.Length)
                        globals.bCol4 = false;
                    break;
                case 4:
                    retSeq = runSequences[5];
                    if (cycle > retSeq.Length)
                        globals.bCol5 = false;
                    break;
                case 5:
                    retSeq = runSequences[6];
                    if (cycle > retSeq.Length)
                        globals.bCol6 = false;
                    break;
                case 6:
                    retSeq = runSequences[7];
                    if (cycle > retSeq.Length)
                        globals.bCol7 = false;
                    break;
                case 7:
                    retSeq = runSequences[8];
                    if (cycle > retSeq.Length)
                        globals.bCol8 = false;
                    break;
            }
            return retSeq;
        }

        private void RunTimer_Tick(object sender, EventArgs e)
        {
            //MessageBox.Show("Here in run timerr");
            String s = String.Format("Waiting for {0} s", Convert.ToInt32((DateTime.Now - globals.start_time).TotalSeconds));
            Man_Controlcs.WriteStatus("Run Program", s);
            SafeSetStatus(s);
            iTimerClicks = iTimerClicks + 1;

        }
        public async void BeginandEnd()
        {
            int lbCount = 0;

            //If running
            if (globals.bIsRunning)
            {
                //then process the currently loaded protocol    
                lbCount = lb_CurProtocol.Items.Count;
                int scrollto = 0;
                for (int i = 0; i < lbCount; i++)
                {
                    try { lb_CurProtocol.SelectedIndex = i; }
                    catch (Exception e) { Man_Controlcs.WriteStatus("Error", e.ToString()); }
                    String curItem = lb_CurProtocol.SelectedItem.ToString();

                    //set the focus in the listView
                    if (i < ProtoListView.Items.Count)
                    {
                        ProtoListView.Items[i].Selected = true;
                        ProtoListView.FullRowSelect = true;

                        ProtoListView.Focus();
                    }
                    /*ListView.SelectedIndexCollection indexes =  this.LoopStepsListView.SelectedIndices;

                    foreach (int index in indexes)
                    {
                       LoopStepsListView.FullRowSelect = true;
                        if (LoopStepsListView.Items[index].Focused == false)
                        {
                            LoopStepsListView.Items[index].BackColor = System.Drawing.Color.Red;
                        }
                    }*/

                    int p = 0;

                    if (ProtoListView.SelectedIndices.Count > 0)
                        p = ProtoListView.SelectedIndices[0];

                    lbl_CurStep.Text = (p + 1).ToString("0");

                    if (i > 9 && i < ProtoListView.Items.Count - 9)
                        scrollto = i + 7;
                    else
                        scrollto = ProtoListView.Items.Count - 1;

                    if (p > 9)
                        ProtoListView.Items[scrollto].EnsureVisible();

                    //Do The Commands
                    DoCommands(curItem);

                    //Now Wait Until Done
                    while (globals.bWaiting)
                        await PutTaskDelay(250);

                    if (!bRunProcessing)
                        i = lbCount + 1;


                } //close the for loop

                lb_CurProtocol.SelectedIndex = 0;
            } // Close the if

            bBeginandEnd = true;
        }

        public async void DoCommands(string command)
        {
            string[] CurItemSplit = command.Split(',');


            //Need to put all Commands here
            //First Deblock 
            if (CurItemSplit[0].Contains("Deblock"))
            {
                globals.bWaiting = true;

                DoDeblock(command);
                //await PutTaskDelay(1000);

                int iTime = 0;

                while (globals.bFluidicsBusy)
                {
                    await PutTaskDelay(10);
                    iTime++;

                    if (iTime == 3000)
                        globals.bFluidicsBusy = false;
                }

                globals.bWaiting = false;
            }
            //Comments
            if (CurItemSplit[0].Contains("Comment"))
            {
                globals.bWaiting = true;

                DoComment(CurItemSplit[1]);
                await PutTaskDelay(100);

                globals.bWaiting = false;
            }
            //Pressurize
            if (CurItemSplit[0].Contains("Pressurize"))
            {
                globals.bWaiting = true;

                DoPressurize();
                await PutTaskDelay(100);

                int iTime = 0;

                while (globals.bFluidicsBusy)
                {
                    await PutTaskDelay(10);
                    iTime++;

                    if (iTime == 3500)
                        globals.bFluidicsBusy = false;
                }
                globals.bWaiting = false;
            }
            //Wash A
            if (CurItemSplit[0].Contains("Wash A"))
            {
                globals.bWaiting = true;

                DoWashA(command);
                await PutTaskDelay(100);

                int iTime = 0;

                while (globals.bFluidicsBusy)
                {
                    await PutTaskDelay(10);
                    iTime++;

                    if (iTime == 3500)
                        globals.bFluidicsBusy = false;
                }
                globals.bWaiting = false;
            }
            //Wash B
            if (CurItemSplit[0].Contains("Wash B"))
            {
                globals.bWaiting = true;

                DoWashB(command);
                await PutTaskDelay(1000);

                int iTime = 0;

                while (globals.bFluidicsBusy)
                {
                    await PutTaskDelay(100);
                    iTime++;

                    if (iTime == 250)
                        globals.bFluidicsBusy = false;
                }
                globals.bWaiting = false;
            }
            //Both  AB
            if (CurItemSplit[0].Contains("Both AB"))
            {
                globals.bWaiting = true;

                DoBothAB(command);
                await PutTaskDelay(1000);

                int iTime = 0;

                while (globals.bFluidicsBusy)
                {
                    await PutTaskDelay(100);
                    iTime++;

                    if (iTime == 250)
                        globals.bFluidicsBusy = false;
                }
                globals.bWaiting = false;
            }
            //Both  Act + B
            if (CurItemSplit[0].Contains("Act+B"))
            {
                globals.bWaiting = true;

                DoBothActB(command);
                await PutTaskDelay(1000);

                int iTime = 0;

                while (globals.bFluidicsBusy)
                {
                    await PutTaskDelay(100);
                    iTime++;

                    if (iTime == 250)
                        globals.bFluidicsBusy = false;
                }
                globals.bWaiting = false;
            }
            //Wash Current Amidite Block 
            if (CurItemSplit[0].Contains("Amidite Block"))
            {
                globals.bWaiting = true;

                DoWashAmidite(command);
                await PutTaskDelay(1000);

                int iTime = 0;

                while (globals.bFluidicsBusy)
                {
                    await PutTaskDelay(100);
                    iTime++;

                    if (iTime == 250)
                        globals.bFluidicsBusy = false;
                }
                globals.bWaiting = false;
            }
            //Oxidizer 1
            if (CurItemSplit[0].Contains("Oxidizer"))
            {
                globals.bWaiting = true;

                DoOxidizer(command);
                await PutTaskDelay(500);

                int iTime = 0;

                while (globals.bFluidicsBusy)
                {
                    await PutTaskDelay(100);
                    iTime++;

                    if (iTime == 250)
                        globals.bFluidicsBusy = false;
                }

                globals.bWaiting = false;
            }
            //Oxidizer 2
            if (CurItemSplit[0].Contains("Ox 2"))
            {
                globals.bWaiting = true;

                DoOx2(command);
                await PutTaskDelay(500);

                int iTime = 0;

                while (globals.bFluidicsBusy)
                {
                    await PutTaskDelay(100);
                    iTime++;

                    if (iTime == 250)
                        globals.bFluidicsBusy = false;
                }

                globals.bWaiting = false;
            }
            //Cap A and Cap B
            if (CurItemSplit[0].Contains("CapA + CapB"))
            {
                globals.bWaiting = true;

                DoCaps(command);
                await PutTaskDelay(500);

                int iTime = 0;

                while (globals.bFluidicsBusy)
                {
                    await PutTaskDelay(10);
                    iTime++;

                    if (iTime == 2500)
                        globals.bFluidicsBusy = false;
                }

                globals.bWaiting = false;
            }
            //Cap A
            if (CurItemSplit[0].Contains("Cap A"))
            {
                globals.bWaiting = true;

                DoCapA(command);
                await PutTaskDelay(500);

                int iTime = 0;

                while (globals.bFluidicsBusy)
                {
                    await PutTaskDelay(100);
                    iTime++;

                    if (iTime == 250)
                        globals.bFluidicsBusy = false;
                }

                globals.bWaiting = false;
            }
            //Cap B
            if (CurItemSplit[0].Contains("Cap B"))
            {
                globals.bWaiting = true;

                DoCapB(command);
                await PutTaskDelay(500);

                int iTime = 0;

                while (globals.bFluidicsBusy)
                {
                    await PutTaskDelay(100);
                    iTime++;

                    if (iTime == 250)
                        globals.bFluidicsBusy = false;
                }

                globals.bWaiting = false;
            }
            //Extra 1
            if (CurItemSplit[0].Contains("DEA"))
            {
                globals.bWaiting = true;

                DoDEA(command);
                await PutTaskDelay(500);

                int iTime = 0;

                while (globals.bFluidicsBusy)
                {
                    await PutTaskDelay(100);
                    iTime++;

                    if (iTime == 250)
                        globals.bFluidicsBusy = false;
                }

                globals.bWaiting = false;
            }
            //Extra 2
            if (CurItemSplit[0].Contains("Purge"))
            {
                globals.bWaiting = true;

                DoGasPurge(command);
                await PutTaskDelay(500);

                int iTime = 0;

                while (globals.bFluidicsBusy)
                {
                    await PutTaskDelay(100);
                    iTime++;

                    if (iTime == 250)
                        globals.bFluidicsBusy = false;
                }

                globals.bWaiting = false;
            }
            //Act 1
            if (CurItemSplit[0].Contains("Activator 1"))
            {
                globals.bWaiting = true;

                DoAct1(command);
                await PutTaskDelay(500);

                int iTime = 0;

                while (globals.bFluidicsBusy)
                {
                    await PutTaskDelay(100);
                    iTime++;

                    if (iTime == 250)
                        globals.bFluidicsBusy = false;
                }

                globals.bWaiting = false;
            }
            //Act1 + base
            if (CurItemSplit[0].Contains("Act1 + Base"))
            {
                globals.bWaiting = true;

                DoAct1Base(command);
                await PutTaskDelay(500);

                int iTime = 0;

                while (globals.bFluidicsBusy)
                {
                    await PutTaskDelay(50);
                    iTime++;

                    if (iTime == 600)
                        globals.bFluidicsBusy = false;
                }

                globals.bWaiting = false;
            }
            //Act 2
            if (CurItemSplit[0].Contains("Activator 2"))
            {
                globals.bWaiting = true;

                DoAct2(command);
                await PutTaskDelay(500);

                int iTime = 0;

                while (globals.bFluidicsBusy)
                {
                    await PutTaskDelay(100);
                    iTime++;

                    if (iTime == 300)
                        globals.bFluidicsBusy = false;
                }


                globals.bWaiting = false;
            }
            //Act2 + base
            if (CurItemSplit[0].Contains("Act2 + Base"))
            {
                globals.bWaiting = true;

                DoAct2Base(command);
                await PutTaskDelay(500);

                int iTime = 0;

                while (globals.bFluidicsBusy)
                {
                    await PutTaskDelay(100);
                    iTime++;

                    if (iTime == 300)
                        globals.bFluidicsBusy = false;
                }

                globals.bWaiting = false;
            }
            //Base - the currently being coupled base
            if (CurItemSplit[0].Contains("Base Reagent") && !CurItemSplit[0].Contains("Act"))
            {
                globals.bWaiting = true;

                DoBase(command);
                await PutTaskDelay(500);

                int iTime = 0;

                while (globals.bFluidicsBusy)
                {
                    await PutTaskDelay(100);
                    iTime++;

                    if (iTime == 300)
                        globals.bFluidicsBusy = false;
                }

                globals.bWaiting = false;
            }

            // Do all amidites with the same procedure
            //Act 2
            if (CurItemSplit[0].Contains("Amidite"))
            {

                //                MessageBox.Show("Amidite Selected");
                globals.bWaiting = true;

                if (CurItemSplit[0].Equals("Amidite 1 Reagent"))
                    DoAmidite(command, "V,A,1");
                else if (CurItemSplit[0].Contains("Amidite 2"))
                    DoAmidite(command, "V,A,2");
                else if (CurItemSplit[0].Contains("Amidite 3"))
                    DoAmidite(command, "V,A,3");
                else if (CurItemSplit[0].Contains("Amidite 4"))
                    DoAmidite(command, "V,A,4");
                else if (CurItemSplit[0].Contains("Amidite 5"))
                    DoAmidite(command, "V,A,5");
                else if (CurItemSplit[0].Contains("Amidite 6"))
                    DoAmidite(command, "V,A,6");
                else if (CurItemSplit[0].Contains("Amidite 7"))
                    DoAmidite(command, "V,A,7");
                else if (CurItemSplit[0].Contains("Amidite 8"))
                    DoAmidite(command, "V,A,8");
                else if (CurItemSplit[0].Contains("Amidite 9"))
                    DoAmidite(command, "V,A,9");
                else if (CurItemSplit[0].Equals("Amidite 10 Reagent\t\t"))
                    DoAmidite(command, "V,A,10");
                else if (CurItemSplit[0].Equals("Amidite 11 Reagent\t\t"))
                    DoAmidite(command, "V,A,11");
                else if (CurItemSplit[0].Equals("Amidite 12 Reagent\t\t"))
                    DoAmidite(command, "V,A,12");
                else if (CurItemSplit[0].Equals("Amidite 13 Reagent\t\t"))
                    DoAmidite(command, "V,A,13");
                else if (CurItemSplit[0].Equals("Amidite 14 Reagent\t\t"))
                    DoAmidite(command, "V,A,14");

                await PutTaskDelay(500);

                int iTime = 0;

                while (globals.bFluidicsBusy)
                {
                    await PutTaskDelay(100);
                    iTime++;

                    if (iTime == 300)
                        globals.bFluidicsBusy = false;
                }

                globals.bWaiting = false;
            }
            //Waste (Recycle) Valve
            if (CurItemSplit[0].Contains("Waste"))
            {
                globals.bWaiting = true;

                DoCOWaste(CurItemSplit[1]);  //1 for close 0 for open
                await PutTaskDelay(100);

                int iTime = 0;

                while (globals.bFluidicsBusy)
                {
                    await PutTaskDelay(50);
                    iTime++;

                    if (iTime == 500)  //timeout afer 30 seconds
                        globals.bFluidicsBusy = false;
                }
                globals.bWaiting = false;
            }
            //Push to Column
            if (CurItemSplit[0].Contains("Push"))
            {
                globals.bWaiting = true;

                DoPushToCol(command);
                await PutTaskDelay(100);

                int iTime = 0;

                while (globals.bFluidicsBusy)
                {
                    await PutTaskDelay(100);
                    iTime++;

                    if (iTime == 250)
                        globals.bFluidicsBusy = false;
                }
                globals.bWaiting = false;
            }
            //Recycle
            if (CurItemSplit[0].Contains("Recycle"))
            {
                globals.bWaiting = true;
                globals.bFluidicsBusy = true;
                int MaxWait = Convert.ToInt16(CurItemSplit[1]);
                DoRecycle(command);
                await PutTaskDelay(1000);

                int iTime = 0;

                while (globals.bFluidicsBusy)
                {
                    await PutTaskDelay(1000);
                    iTime++;

                    if (iTime > (MaxWait + 20))
                        globals.bFluidicsBusy = false;
                }

                globals.bWaiting = false;
            }
            if (CurItemSplit[0].Contains("Pump"))
            {
                globals.bWaiting = true;

                DoPumpInit(command);
                await PutTaskDelay(800);

                int iTime = 0;

                while (globals.bFluidicsBusy)
                {
                    await PutTaskDelay(500);
                    iTime++;

                    if (iTime == 6)
                        globals.bFluidicsBusy = false;
                }


                globals.bWaiting = false;
            }
            if (CurItemSplit[0].Contains("Wait"))
            {
                double wSecWait = int.Parse(CurItemSplit[1]);

                //keep everything coordinated...it will move on to next command while 
                //finishing previous commands...
                while (globals.bWaiting || globals.bFluidicsBusy)
                    await Task.Delay(100);

                globals.bWaiting = true;
                bWait = true;


                //set the interval based on the length of the wait
                if (wSecWait < 17)
                    globals.runform.RunTimer.Interval = 1000;    // every 1 second
                else if (wSecWait >= 17 && wSecWait < 90)
                    globals.runform.RunTimer.Interval = 10000;  //every 10 seconds
                else if (wSecWait >= 90 && wSecWait < 600)
                    globals.runform.RunTimer.Interval = 30000;  //every 30 seconds
                else
                    globals.runform.RunTimer.Interval = 60000;  //every 60 seconds
                // this is the easiest, just sit on a wait
                globals.runform.RunTimer.Start();

                globals.start_time = DateTime.Now;
                int segundos = int.Parse(CurItemSplit[1]);
                int wTime = segundos * 1000;

                if (segundos < 1) { globals.runform.RunTimer.Stop(); globals.bWaiting = false; return; }
                //to see which works better, either the timer inside of a timer or 
                //this while loop with applicaiton do events()
                DateTime _desired = DateTime.Now.AddSeconds(segundos);
                while (DateTime.Now < _desired)
                {
                    System.Windows.Forms.Application.DoEvents();
                }


                /*while (cntr < wTime)
                {
                    await PutTaskDelay(100);
                    cntr = cntr + 100;
                }*/

                globals.runform.RunTimer.Stop();
                bWait = false;
                globals.bWaiting = false;
            }
        }
        public void CloaseAllValves()
        {
            string cmd = string.Empty;

            //3 way valves
            Man_Controlcs.SendControllerMsg(1, valves.tocolbypass);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_3WBC, 0);
            rb_3WBC.Text = "To Bypass";

            Man_Controlcs.SendControllerMsg(1, valves.topumpbypass);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_3WBP, 0);
            rb_3WBP.Text = "To Bypass";
            //ColorButton(rb_3WRA, 0);
            //ColorButton(rb_3WWR, 0);

            Man_Controlcs.SendControllerMsg(1, valves.pressuretop);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_AGas, 0);
            rb_AGas.Text = "Top";

            //columns
            Man_Controlcs.SendControllerMsg(1, valves.col1off);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_C1, 0);

            Man_Controlcs.SendControllerMsg(1, valves.col2off);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_C2, 0);

            Man_Controlcs.SendControllerMsg(1, valves.col3off);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_C3, 0);

            Man_Controlcs.SendControllerMsg(1, valves.col4off);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_C4, 0);

            Man_Controlcs.SendControllerMsg(1, valves.col5off);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_C5, 0);

            Man_Controlcs.SendControllerMsg(1, valves.col6off);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_C6, 0);

            Man_Controlcs.SendControllerMsg(1, valves.col7off);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_C7, 0);

            Man_Controlcs.SendControllerMsg(1, valves.col8off);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_C8, 0);

            //amidites
            Man_Controlcs.SendControllerMsg(1, valves.am1off);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_Am1, 0);

            Man_Controlcs.SendControllerMsg(1, valves.am2off);
            Man_Controlcs.SyncWait(50); rb_Am1.ForeColor = System.Drawing.Color.Black;
            ColorButton(rb_Am2, 0);

            Man_Controlcs.SendControllerMsg(1, valves.am3off);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_Am3, 0);

            Man_Controlcs.SendControllerMsg(1, valves.am4off);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_Am4, 0);

            Man_Controlcs.SendControllerMsg(1, valves.am5off);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_Am5, 0);

            Man_Controlcs.SendControllerMsg(1, valves.am6off);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_Am6, 0);

            Man_Controlcs.SendControllerMsg(1, valves.am7off);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_Am7, 0); ;

            Man_Controlcs.SendControllerMsg(1, valves.am8off);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_Am8, 0);

            Man_Controlcs.SendControllerMsg(1, valves.am9off);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_Am9, 0);

            Man_Controlcs.SendControllerMsg(1, valves.am10off);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_Am10, 0);

            Man_Controlcs.SendControllerMsg(1, valves.am11off);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_Am11, 0);

            Man_Controlcs.SendControllerMsg(1, valves.am12off);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_Am12, 0);

            Man_Controlcs.SendControllerMsg(1, valves.am13off);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_Am13, 0);

            Man_Controlcs.SendControllerMsg(1, valves.am14off);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_Am14, 0);


            //train B reagents
            Man_Controlcs.SendControllerMsg(1, valves.act1off);
            Man_Controlcs.SyncWait(50);
            Man_Controlcs.SendControllerMsg(1, valves.act1gasoff);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_Act1, 0);

            Man_Controlcs.SendControllerMsg(1, valves.act2off);
            Man_Controlcs.SyncWait(50);
            Man_Controlcs.SendControllerMsg(1, valves.act2gasoff);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_Act2, 0);

            Man_Controlcs.SendControllerMsg(1, valves.washb1off);
            Man_Controlcs.SyncWait(50);
            Man_Controlcs.SendControllerMsg(1, valves.washb2off);
            Man_Controlcs.SyncWait(50);

            Man_Controlcs.SendControllerMsg(1, valves.washbgasoff);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_WashB, 0);


            //last the big bottle reagents
            Man_Controlcs.SendControllerMsg(1, valves.debloff);
            Man_Controlcs.SyncWait(50);
            Man_Controlcs.SendControllerMsg(1, valves.deblgasoff);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_Dbl, 0);

            Man_Controlcs.SendControllerMsg(1, valves.ox1off);
            Man_Controlcs.SyncWait(50);
            Man_Controlcs.SendControllerMsg(1, valves.ox1gasoff);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_Ox1, 0);

            Man_Controlcs.SendControllerMsg(1, valves.ox2off);
            Man_Controlcs.SyncWait(50);
            Man_Controlcs.SendControllerMsg(1, valves.ox2gasoff);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_Ox2, 0);

            Man_Controlcs.SendControllerMsg(1, valves.capaoff);
            Man_Controlcs.SyncWait(50);
            Man_Controlcs.SendControllerMsg(1, valves.capagasoff);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_CapA, 0);

            Man_Controlcs.SendControllerMsg(1, valves.capboff);
            Man_Controlcs.SyncWait(50);
            Man_Controlcs.SendControllerMsg(1, valves.capbgasoff);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_CapB, 0);

            Man_Controlcs.SendControllerMsg(1, valves.xtra1off);
            Man_Controlcs.SyncWait(50);
            Man_Controlcs.SendControllerMsg(1, valves.xtra1gasoff);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_Xtra1, 0);

            Man_Controlcs.SendControllerMsg(1, valves.gaspurgeoff);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_Xtra2, 0);

            Man_Controlcs.SendControllerMsg(1, valves.washaoff);
            Man_Controlcs.SyncWait(50);
            Man_Controlcs.SendControllerMsg(1, valves.washagasoff);
            Man_Controlcs.SyncWait(50);
            ColorButton(rb_WashA, 0);

        }
    }
}
