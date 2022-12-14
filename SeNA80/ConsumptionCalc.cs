using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Windows.Forms;

namespace SeNA80
{
    public partial class ConsumptionCalc : Form
    {
        public static string[] reagentvols = new string[50];
        public static string[] amiditevols = new string[35];
        public static int rvcntr = 0;
        public static int amcntr = 0;
        public static int[] basecnt = new int[20];
        public ConsumptionCalc()
        {
            InitializeComponent();
        }

        private void ConsumptionCalc_Load(object sender, EventArgs e)
        {
            //initialize the arrays
            for (int i = 0; i < reagentvols.Length; i++)
                reagentvols[i] = string.Empty;

            for (int i = 0; i < amiditevols.Length; i++)
                amiditevols[i] = string.Empty;

            rvcntr = 0;
            amcntr = 0;

            //fill with reagent amounts
            string[] protolines = new string[100];
            String fName = string.Empty;
            int cntr = 0;

            //first the start protocol
            if (!(SeNARun.protolbls[0].Contains("[none]")))
            {
                fName = globals.protocol_path + SeNARun.protolbls[0];

                try { protolines = File.ReadAllLines(fName); }
                catch { MessageBox.Show("Startup Protocol File Not Found", "Error"); }

                foreach (string line in protolines)
                {
                    if (line.Contains("[Volumes]"))
                    { cntr = cntr + 1; break; }
                    else
                        cntr = cntr + 1;
                }
                //now store the volumes in a string array
                for (int i = cntr; i < protolines.Length; i++)
                {
                    if (!(string.IsNullOrEmpty(protolines[i])))
                    {
                        reagentvols[rvcntr] = protolines[i];
                        string[] parts = reagentvols[rvcntr].Split(',');
                        reagentvols[rvcntr] = parts[0] + "," + parts[1] + "," + "STR";
                        rvcntr = rvcntr + 1;
                        Debug.WriteLine("Startup Protocol Vol Line " + rvcntr.ToString() + " is -" + reagentvols[rvcntr - 1]);
                    }
                }
            }
            //next do the prep protocol
            //fill with reagent amounts
            protolines = new string[100];

            //first the prepprotocol
            fName = globals.protocol_path + SeNARun.protolbls[1];

            try { protolines = File.ReadAllLines(fName); }
            catch { MessageBox.Show("Prep Protocol File Not Found", "Error"); }

            cntr = 0;
            foreach (string line in protolines)
            {
                if (line.Contains("[Volumes]"))
                { cntr = cntr + 1; break; }
                else
                    cntr = cntr + 1;
            }

            //now store the volumes in a string array
            for (int i = cntr; i < protolines.Length; i++)
            {
                if (!(string.IsNullOrEmpty(protolines[i])))
                {
                    string[] steps = protolines[i].Split(',');

                    //check to see if the reagent is already in the reagvols arra
                    for (int j = 0; j < rvcntr; j++)
                    {
                        if (!String.IsNullOrEmpty(reagentvols[j]))
                        {
                            string[] temp = reagentvols[j].Split(',');

                            //if it is there replace it
                            if (temp[0].Equals(steps[0]))
                            {
                                temp[1] = (Convert.ToDouble(temp[1]) + Convert.ToDouble(steps[1])).ToString("0.00");
                                temp[2] = "PRP";
                                reagentvols[j] = temp[0] + "," + temp[1] + "," + temp[2];
                                Debug.WriteLine("Prep Protocol Line Updated" + reagentvols[j]);
                                j = rvcntr;
                            }
                        }
                    }


                    //not there add it
                    reagentvols[rvcntr] = protolines[i];
                    string[] parts = reagentvols[rvcntr].Split(',');
                    reagentvols[rvcntr] = parts[0] + "," + parts[1] + "," + "PRP";
                    rvcntr = rvcntr + 1;
                    Debug.WriteLine("Prep Protocol Vol Line " + rvcntr.ToString() + " is -" + reagentvols[rvcntr - 1]);
                }
            }

            //next do the post protocol
            //fill with reagent amounts
            protolines = new string[100];

            //first the prepprotocol
            fName = globals.protocol_path + SeNARun.protolbls[3];

            try { protolines = File.ReadAllLines(fName); }
            catch { MessageBox.Show("Post Protocol File Not Found", "Error"); }

            cntr = 0;
            foreach (string line in protolines)
            {
                if (line.Contains("[Volumes]"))
                { cntr = cntr + 1; break; }
                else
                    cntr = cntr + 1;
            }

            //now store the volumes in a string array
            for (int i = cntr; i < protolines.Length; i++)
            {
                if (!(string.IsNullOrEmpty(protolines[i])))
                {
                    string[] steps = protolines[i].Split(',');

                    //check to see if the reagent is already in the reagvols arra
                    for (int j = 0; j < rvcntr; j++)
                    {
                        if (!String.IsNullOrEmpty(reagentvols[j]))
                        {
                            string[] temp = reagentvols[j].Split(',');

                            //if it is there replace it
                            if (temp[0].Equals(steps[0]))
                            {
                                temp[1] = (Convert.ToDouble(temp[1]) + Convert.ToDouble(steps[1])).ToString("0.00");
                                temp[2] = "PST";
                                reagentvols[j] = temp[0] + "," + temp[1] + "," + temp[2];
                                Debug.WriteLine("Post Line Updated" + reagentvols[j]);
                                j = rvcntr;
                            }
                        }
                    }


                    //not there add it
                    reagentvols[rvcntr] = protolines[i];
                    string[] parts = reagentvols[rvcntr].Split(',');
                    reagentvols[rvcntr] = parts[0] + "," + parts[1] + "," + "PST";
                    rvcntr = rvcntr + 1;
                    Debug.WriteLine("Prep Protocol Vol Line " + rvcntr.ToString() + " is -" + reagentvols[rvcntr - 1]);
                }
            }

            //finally open the run protcol and get each step, then open each individual file (i.e. debl, cap, ox, cap), not the amidite
            //and one line at a time, if the reagent is already in the table sum the volumes (volume * maxcycles)
            //open the protocol file
            fName = globals.protocol_path + SeNARun.protolbls[2];
            string[] mainprotolines = new string[10];

            try { mainprotolines = File.ReadAllLines(fName); }
            catch { MessageBox.Show("Protocol File - " + fName + " Not Found", "Error"); }

            int pc = 0;
            foreach (string step in mainprotolines)
            {
                if (step.Contains("[Volumes]"))
                { break; }
                else
                    pc = pc + 1;
            }

            for (int m = 1; m < pc; m++)
            {
                string line = mainprotolines[m];
                string[] proto = line.Split(',');

                if (proto[0].Contains("Debl") || proto[0].Contains("Cap") || proto[0].Contains("Thio") || proto[0].Contains("Oxi"))
                {
                    fName = globals.protocol_path + proto[1];

                    try { protolines = File.ReadAllLines(fName); }
                    catch { MessageBox.Show("Protocol File - " + fName + " Not Found", "Error"); }

                    //find the [Volumes]
                    cntr = 0;
                    foreach (string step in protolines) //find the start line
                    {
                        if (step.Contains("[Volumes]"))
                        { cntr = cntr + 1; break; }
                        else
                            cntr = cntr + 1;
                    }
                    //split the Cap A and Cap B protocol lines
                    int sLength = protolines.Length;
                    string sSame = string.Empty;

                    if (proto[0].Contains("Cap"))
                    {
                        for (int cA = cntr; cA < sLength; cA++)
                        {
                            if (protolines[cA].Contains("CapA") && protolines[cA].Contains("CapB"))
                            {
                                string[] temp = protolines[cA].Split(',');
                                double amount = Double.Parse(temp[1]) / (double)2;

                                Debug.WriteLine("Amount is " + amount.ToString("0.0"));
                                int Replaced2 = 0;

                                //serch for CapA or Cap B
                                for (int fA = cntr; fA < sLength; fA++)
                                {

                                    //find Cap A Reagent
                                    if (protolines[fA].Contains("Cap A Rea"))
                                    {
                                        string[] temp2 = protolines[fA].Split(',');
                                        double afA = Double.Parse(temp2[1]);
                                        afA = afA + amount;

                                        protolines[fA] = temp2[0] + "," + afA.ToString("0.0") + "," + "mLs";
                                        if (Replaced2 == 0)
                                            Replaced2 = 1;
                                        else
                                            Replaced2 = 3;
                                    }
                                    //find Cap B Reagent
                                    if (protolines[fA].Contains("Cap B Rea"))
                                    {
                                        string[] temp2 = protolines[fA].Split(',');
                                        double afA = Double.Parse(temp2[1]);
                                        afA = afA + amount;

                                        protolines[fA] = temp2[0] + "," + afA.ToString("0") + "," + "mLs";

                                        if (Replaced2 == 0)
                                            Replaced2 = 2;
                                        else
                                            Replaced2 = 3;
                                    }
                                }
                                if (Replaced2 == 1) //so we found and added to Cap A Only
                                    protolines[cA] = "Cap B Reagent              " + "," + amount.ToString("0.00") + "," + "mLs";
                                else if (Replaced2 == 2)//found and updated Cap B Only
                                    protolines[cA] = "Cap A Reagent              " + "," + amount.ToString("0.00") + "," + "mLs";
                                else if (Replaced2 == 0)//Didn't find either
                                {
                                    protolines[cA] = "Cap B Reagent              " + "," + amount.ToString("0") + "," + "mLs";
                                    //later append a line for Cap A
                                    Debug.WriteLine("Cap B Reagent " + protolines[cA] + " at line " + cA.ToString());
                                }
                                else //found both and added the amount to each
                                    protolines[cA] = string.Empty;

                                Debug.WriteLine("What is in the current line " + protolines[cA] + " at line " + cA.ToString());
                            }
                            for (int j = cntr; j < protolines.Length; j++)
                                Debug.WriteLine("Protocol Volumes is " + protolines[j]);
                        }
                    } //end of look at Capping protocols

                    //now check either add or append into the reagvols array
                    for (int i = cntr; i < protolines.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(protolines[i]))
                        {

                            bool bIn = false;
                            string[] steps = protolines[i].Split(',');

                            Debug.WriteLine("Current Protocol Line = " + protolines[i] + "RVCNTR = " + rvcntr.ToString());

                            //check to see if the reagent is already in the reagvols arra
                            for (int j = 0; j < rvcntr; j++)
                            {
                                if (!String.IsNullOrEmpty(reagentvols[j]))
                                {
                                    string[] temp = reagentvols[j].Split(',');

                                    //if it is there replace it
                                    if (temp[0].Equals(steps[0]))
                                    {
                                        temp[1] = (Convert.ToDouble(temp[1]) + Convert.ToDouble(steps[1])).ToString("0.00");
                                        temp[2] = "MAIN";
                                        reagentvols[j] = temp[0] + "," + temp[1] + "," + temp[2];
                                        Debug.WriteLine("Main Proto Line Updated  with " + reagentvols[j]);
                                        bIn = true;
                                        j = rvcntr;
                                    }
                                }

                            }//end of for
                             //if not there then append
                            if (!bIn)
                            {
                                reagentvols[rvcntr] = protolines[i];
                                rvcntr = rvcntr + 1;
                                Debug.WriteLine("Proto Line Added" + reagentvols[rvcntr]);
                            }
                        }
                    }//end of for protolines

                } // end of if
                else if (proto[0].Contains("Mix"))
                {
                    for (int c = 1; c < 3; c++)
                    {
                        fName = globals.protocol_path + proto[c];

                        try { protolines = File.ReadAllLines(fName); }
                        catch { MessageBox.Show("Protocol File - " + fName + " Not Found", "Error"); }

                        //find the [Volumes]
                        cntr = 0;
                        foreach (string step in protolines)
                        {
                            if (step.Contains("[Volumes]"))
                            { cntr = cntr + 1; break; }
                            else
                                cntr = cntr + 1;
                        }

                        //now check either add or append into the reagvols array
                        for (int i = cntr; i < protolines.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(protolines[i]))
                            {

                                bool bIn = false;
                                string[] steps = protolines[i].Split(',');

                                Debug.WriteLine("Current Protocol Line = " + protolines[i] + "RVCNTR = " + rvcntr.ToString());

                                //check to see if the reagent is already in the reagvols arra
                                for (int j = 0; j < rvcntr; j++)
                                {
                                    if (!String.IsNullOrEmpty(reagentvols[j]))
                                    {
                                        string[] temp = reagentvols[j].Split(',');

                                        //if it is there replace it
                                        if (temp[0].Equals(steps[0]))
                                        {
                                            temp[1] = (Convert.ToDouble(temp[1]) + Convert.ToDouble(steps[1])).ToString("0.00");
                                            temp[2] = "MAIN";
                                            reagentvols[j] = temp[0] + "," + temp[1] + "," + temp[2];
                                            Debug.WriteLine("Main Proto Line Updated" + reagentvols[j]);
                                            bIn = true;
                                            j = rvcntr;
                                        }
                                    }

                                }//end of for
                                 //if not there then append
                                if (!bIn)
                                {
                                    reagentvols[rvcntr] = protolines[i];
                                    Debug.WriteLine("Proto Line Added" + reagentvols[rvcntr]);
                                    rvcntr = rvcntr + 1;
                                }
                            }
                        }//end of for protolines

                    }//end of for open ox and thio protocols
                }
                else if (proto[0].Contains("Coupling"))
                {
                    string[] bases = BuildBasesString();

                    foreach (string b in bases)
                    {
                        string code = string.Empty;
                        string baseprotcol = String.Empty;
                        //first determine if we are using 1 or 2 letter codes
                        if (globals.i12Ltr != 0)
                            code = Make2Ltr(b);
                        else
                            code = b;

                        //now get the protocol
                        baseprotcol = GetProtocol(code);

                        fName = globals.protocol_path + baseprotcol;

                        string[] baselines = new string[35];

                        try { baselines = File.ReadAllLines(fName); }
                        catch { MessageBox.Show("Protocol File -" + fName + " File Not Found", "Error"); }

                        //find the start of the volumes
                        cntr = 0;
                        foreach (string step in baselines)
                        {
                            if (step.Contains("[Volumes]"))
                            { cntr = cntr + 1; break; }
                            else
                                cntr = cntr + 1;
                        }

                        //now check either add or append into the reagvols array
                        for (int i = cntr; i < baselines.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(baselines[i]))
                            {
                                //get rid of the word Base and substitute the current amdite
                                if (baselines[i].Contains("Base"))
                                {
                                    string[] p = baselines[i].Split(',');
                                    baselines[i] = "Amidite - " + code + "," + p[1] + "," + p[2];
                                }

                                bool bIn = false;
                                string[] steps = baselines[i].Split(',');

                                Debug.WriteLine("Current Base Line = " + baselines[i] + "AMCNTR = " + amcntr.ToString());

                                //check to see if the reagent is already in the amiditevols array
                                for (int j = 0; j < amcntr; j++)
                                {
                                    string[] temp = amiditevols[j].Split(',');

                                    //if it is there replace it
                                    if (temp[0].Equals(steps[0]))
                                    {
                                        temp[1] = (Convert.ToDouble(temp[1]) + Convert.ToDouble(steps[1])).ToString("0.00");
                                        baselines[j] = temp[0] + "," + temp[1] + "," + temp[2];
                                        Debug.WriteLine("Amidite Line Updated" + amiditevols[j]);
                                        bIn = true;
                                        j = amcntr;
                                    }

                                }//end of for
                                 //if not there then append
                                if (!bIn)
                                {
                                    //check if contain base
                                    amiditevols[amcntr] = baselines[i];
                                    Debug.WriteLine("Amidite Proto Line Added" + amiditevols[rvcntr]);
                                    amcntr = amcntr + 1;
                                }
                            }
                        }//end of for baselines


                    }

                }//add post here
            } //end of for each line in main protocol
            //then do the same for the post protocol

            //after each are done report and calculate including average scale and max cycles
            for (int c = 0; c < rvcntr; c++)
            {
                string[] parts = reagentvols[c].Split(',');
                if (parts[2].Contains("PRE"))
                    parts[1] = (Convert.ToDouble(parts[1]) * globals.avgScale).ToString("0.00");
                else
                    parts[1] = (Convert.ToDouble(parts[1]) * SeNARun.MaxCycles * globals.avgScale).ToString("0.00");
                reagentvols[c] = parts[0] + "," + parts[1] + "," + parts[2];
            }
            int firstbase = 0;

            int totalcouplings = 0;
            for (int i = 0; i < basecnt.Length; i++)
                totalcouplings = totalcouplings + basecnt[i];

            for (int c = 0; c < amcntr; c++)
            {
                string[] parts = amiditevols[c].Split(',');
                if (parts[0].Contains("Amidite"))
                {
                    parts[1] = (Convert.ToDouble(parts[1]) * basecnt[firstbase] * globals.avgScale).ToString("0.00");
                    amiditevols[c] = parts[0] + "," + parts[1] + "," + parts[2];
                    firstbase = firstbase + 1;
                }
                else
                {
                    parts[1] = (Convert.ToDouble(parts[1]) * totalcouplings * globals.avgScale).ToString("0.00");
                    amiditevols[c] = parts[0] + "," + parts[1] + "," + parts[2];

                }

            }
            //clean up duplicates 
            reagentvols = CleanDups(reagentvols);
            amiditevols = CleanDups(amiditevols);

            //format and add reagents to the text box
            //print the reagents first
            DataTable dt = new DataTable();

            DataColumn dtColumn = new DataColumn();
            dtColumn.ColumnName = "Reagent.";
            DataColumn colString = new DataColumn("StringCol");
            colString.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.ColumnName = "Volume(mLs)";
            colString = new DataColumn("StringCol");
            colString.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(dtColumn);
            string newline = string.Empty;
            rvcntr = reagentvols.Length;
            amcntr = amiditevols.Length;
            for (int i = 0; i < rvcntr; i++)
            {
                newline = reagentvols[i];

                if (newline == null || newline == string.Empty)
                    break;

                DataRow rr = dt.NewRow();
                string[] values = newline.Split(',');

                rr[0] = values[0];
                rr[1] = Convert.ToDouble(values[1]).ToString("0.00");

                dt.Rows.Add(rr);
            }
            //add a blank line
            DataRow br = dt.NewRow();
            dt.Rows.Add(br);
            //do the same for amidites
            for (int i = 0; i < amcntr; i++)
            {
                newline = amiditevols[i];

                if (newline == null || newline == string.Empty)
                    break;

                DataRow ar = dt.NewRow();
                string[] values = newline.Split(',');

                ar[0] = values[0];
                ar[1] = Convert.ToDouble(values[1]).ToString("0.00");

                dt.Rows.Add(ar);
            }

            //rTB_Volumes.Text = dt;

            dt_ConsumptionTable.DataSource = dt;
            dt_ConsumptionTable.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            dt_ConsumptionTable.ColumnHeadersHeight = 25;
            dt_ConsumptionTable.Columns[0].Width = 312;
            dt_ConsumptionTable.Columns[1].Width = 210;
            dt_ConsumptionTable.Columns[0].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, FontStyle.Regular);
            dt_ConsumptionTable.Columns[1].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, FontStyle.Regular);


            //finally do the amidites...this is going to be tricky, have to count each base (A, G, C, T, rA, rG, etc), 
            //then look at the protocol for each base and do the same as above
            //multiply the each of the volumes times the bases the average scale, 
            //then add to report....
        }
        private string[] CleanDups(string[] cleanme)
        {
            string[] outstring = new string[1];
            string temp = string.Empty;
            int add = 1;
            bool bfound = false;

            outstring[0] = cleanme[0];
            for (int i = 1; i < cleanme.Length; i++)
            {
                if (!string.IsNullOrEmpty(cleanme[i]))
                {

                    string[] parts = cleanme[i].Split(',');
                    //now search -- if found update it
                    for (int j = 0; j < outstring.Length; j++)
                    {
                        if (outstring[j].Contains(parts[0]))
                        {
                            string[] temp2 = outstring[j].Split(',');
                            double sum = double.Parse(temp2[1]) + double.Parse(parts[1]);
                            temp = temp2[0] + "," + sum.ToString("0.00") + "," + temp2[2];
                            outstring[j] = temp;
                            bfound = true;
                        }
                    }
                    if (!bfound) //if now found append it
                    {
                        Array.Resize(ref outstring, outstring.Length + 1);
                        outstring[add] = cleanme[i];
                        add = add + 1;
                    }
                    //reset found and start again
                    bfound = false;
                }
            }


            return outstring;

        }
        private string GetProtocol(string code)
        {
            if (Properties.Settings.Default.Am_1_lbl.Equals(code))
                return Properties.Settings.Default.Am_1_prtcl;
            if (Properties.Settings.Default.Am_2_lbl.Equals(code))
                return Properties.Settings.Default.Am_2_prtcl;
            if (Properties.Settings.Default.Am_3_lbl.Equals(code))
                return Properties.Settings.Default.Am_3_prtcl;
            if (Properties.Settings.Default.Am_4_lbl.Equals(code))
                return Properties.Settings.Default.Am_4_prtcl;
            if (Properties.Settings.Default.Am_5_lbl.Equals(code))
                return Properties.Settings.Default.Am_5_prtcl;
            if (Properties.Settings.Default.Am_6_lbl.Equals(code))
                return Properties.Settings.Default.Am_6_prtcl;
            if (Properties.Settings.Default.Am_7_lbl.Equals(code))
                return Properties.Settings.Default.Am_7_prtcl;
            if (Properties.Settings.Default.Am_8_lbl.Equals(code))
                return Properties.Settings.Default.Am_8_prtcl;
            if (Properties.Settings.Default.Am_9_lbl.Equals(code))
                return Properties.Settings.Default.Am_9_prtcl;
            if (Properties.Settings.Default.Am_10_lbl.Equals(code))
                return Properties.Settings.Default.Am_10_prtcl;
            if (Properties.Settings.Default.Am_11_lbl.Equals(code))
                return Properties.Settings.Default.Am_11_prtcl;
            if (Properties.Settings.Default.Am_12_lbl.Equals(code))
                return Properties.Settings.Default.Am_12_prtcl;
            if (Properties.Settings.Default.Am_13_lbl.Equals(code))
                return Properties.Settings.Default.Am_13_prtcl;
            if (Properties.Settings.Default.Am_14_lbl.Equals(code))
                return Properties.Settings.Default.Am_14_prtcl;

            return string.Empty;
        }
        private string Make2Ltr(string sI)
        {
            foreach (string b in SeNARun.bases)
            {
                string[] pts = b.Split(',');
                if (pts[0].Equals(sI))
                {
                    return pts[1];
                }
            }
            return string.Empty;
        }
        private string[] BuildBasesString()
        {
            string[] basesout = new string[20];
            string totalseqs = String.Empty;

            //initialize basesout
            for (int i = 0; i < 20; i++)
                basesout[i] = string.Empty;

            //first get the different bases
            for (int i = 1; i < 8; i++)
            {
                if (SeNARun.runSequences[i].Length > 0)
                {
                    totalseqs = totalseqs + SeNARun.runSequences[i].Trim();
                }
            }
            string answer = new String(totalseqs.Distinct().ToArray());
            Debug.WriteLine("Distinct Bases " + answer);
            char[] chbases = answer.ToCharArray();
            int counter = 0;

            basesout = new string[chbases.Length];

            foreach (char c in chbases)
            {
                basesout[counter] = Char.ToString(c);
                counter = counter + 1;
            }

            //next count the number of each in totalseqs
            char[] letters = new char[totalseqs.Length + 1];

            letters = totalseqs.ToCharArray();
            int ltrCntr = 0;
            //store the first letter in the basesout array and start the count at 1
            foreach (char c in chbases)
            {
                foreach (char l in letters)
                {
                    if (c == l)
                        basecnt[ltrCntr] = basecnt[ltrCntr] + 1;
                }
                ltrCntr = ltrCntr + 1;
            }
            return basesout;
        }


    }
}
