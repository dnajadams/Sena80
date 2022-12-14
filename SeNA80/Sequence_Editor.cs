using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace SeNA80
{

    public partial class Sequence_Editor : Form
    {
        string seq_path = Properties.Settings.Default.Sequence_Path;
        bool bDirty = false;
        public string[] bases = new string[100];
        public int ibases = 0;
        public bool init = false;

        public Sequence_Editor()
        {
            InitializeComponent();
        }
        private void Sequence_Editor_Load(object sender, EventArgs e)
        {
            //if user rights not admin, no base code selection or edit base table
            if (!(globals.Curr_Rights.Contains("Admin")))
            {
                gb_Use.Enabled = false;
                Menu_BaseTable.Enabled = false;
            }
            //select the radio button
            if (globals.i12Ltr == 0)
                rb_1Leter.Checked = true;
            else
                rb_2Letter.Checked = true;

            //fill bases spreadsheet
            string path = globals.sequence_path;

            using (System.IO.StreamReader file = new System.IO.StreamReader(path + "\\BaseTable.csv"))
            {

                string newline = string.Empty;

                while ((newline = file.ReadLine()) != null)
                {
                    if (newline == null || newline == string.Empty)
                        break;

                    bases[ibases] = newline;
                    ibases = ibases + 1;
                }
            }


            int index = 0;

            if (globals.i12Ltr == 0)  //1 letter code
                index = 0;
            else
                index = 1;

            string[] codes = new string[ibases];
            for (int i = 0; i < ibases; i++)
            {
                string[] parts = bases[i].Split(',');
                codes[i] = parts[index];
            }
            //now load the list boxes
            cb_1.Items.AddRange(codes);
            cb_2.Items.AddRange(codes);
            cb_3.Items.AddRange(codes);
            cb_4.Items.AddRange(codes);
            cb_5.Items.AddRange(codes);
            cb_6.Items.AddRange(codes);
            cb_7.Items.AddRange(codes);
            cb_8.Items.AddRange(codes);

            init = true;

        }
        private void OpenSeqFile()
        {
            //first clear anything off the form
            ClearAll();

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = @globals.sequence_path;
            openFileDialog1.Title = "Open Seq File";

            openFileDialog1.DefaultExt = "seq";
            openFileDialog1.Filter = "Sequence files (*.seq)|*.seq|Batch files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 3;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fname = openFileDialog1.FileName;

                using (System.IO.StreamReader file = new System.IO.StreamReader(fname))
                {

                    if (Path.GetExtension(fname) == ".seq")
                    {
                        //check the radio button
                        this.rb_SSeq.Checked = true;
                        string[] seq = { "", "", "", "", "", "", "" };
                        int line = 0;

                        while ((seq[line] = file.ReadLine()) != null)
                            line++;

                        //must reset the list boxes first
                        int seqltr = Convert.ToInt32(seq[1]);
                        if (seqltr == 0)
                        {
                            rb_1Leter.Checked = true;
                            globals.i12Ltr = 0;
                        }
                        else
                        {
                            globals.i12Ltr = 1;
                            rb_2Letter.Checked = true;
                        }

                        //we will assume the first line is the sequence
                        // the comments may or may not be there...
                        this.tb_Seq1.Text = seq[0];
                        if (globals.i12Ltr == 0)
                            this.lbl_lc1.Text = seq[0].Length.ToString();
                        else
                            this.lbl_lc1.Text = (Convert.ToInt32((double)seq[0].Length / (double)3)).ToString("0");



                        if (seq[2].Length > 0)
                            this.tb_notes.Text = seq[2];

                        //file.Close();
                    }
                    if (Path.GetExtension(fname) == ".csv")
                    {
                        //check the radio button
                        this.rb_BatchSeq.Checked = true;
                        string[] seq = { " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "", "", "" };

                        int line = 0;
                        int seqltr = 0;
                        int totlines = 0;

                        string[] lines = new string[15];
                        while ((lines[totlines] = file.ReadLine()) != null)
                        {
                            if (lines[totlines] != null && lines[totlines].Length > 0)
                                totlines++;
                        }

                        //now put the lines in 
                        for (int i = 0; i < totlines; i++)
                        {
                            short wc = 0;
                            bool bC = false;

                            string thisline = lines[i];
                            string[] SeqParts = lines[i].Split(',');

                            int divby = 1;

                            if (lines[i].Length > 0)
                            {
                                //read the first line, it will be the base letters
                                if (i == 0)
                                {
                                    seqltr = Convert.ToInt16(lines[i]);
                                    if (seqltr == 0)
                                    {
                                        globals.i12Ltr = 0;
                                        rb_1Leter.Checked = true;
                                    }
                                    else
                                    {
                                        globals.i12Ltr = 1;
                                        rb_2Letter.Checked = true;
                                    }
                                }
                                else
                                {

                                    if (globals.i12Ltr == 0)
                                        divby = 1;
                                    else
                                        divby = 3;

                                    bC = Int16.TryParse(SeqParts[0], out wc);

                                    if (!bC) { wc = 10; }

                                    // MessageBox.Show(thisline, "wc" + wc.ToString());
                                }
                                switch (wc)
                                {
                                    case 1:
                                        this.tb_Seq1.Text = SeqParts[1];
                                        this.lbl_lc1.Text = ((double)SeqParts[1].Length / (double)divby).ToString();
                                        break;

                                    case 2:
                                        this.tb_Seq2.Text = SeqParts[1];
                                        this.lbl_lc2.Text = ((double)SeqParts[1].Length / (double)divby).ToString();
                                        break;

                                    case 3:
                                        this.tb_Seq3.Text = SeqParts[1];
                                        this.lbl_lc3.Text = ((double)SeqParts[1].Length / (double)divby).ToString();
                                        break;

                                    case 4:
                                        this.tb_Seq4.Text = SeqParts[1];
                                        this.lbl_lc4.Text = ((double)SeqParts[1].Length / (double)divby).ToString();
                                        break;

                                    case 5:
                                        this.tb_Seq5.Text = SeqParts[1];
                                        this.lbl_lc5.Text = ((double)SeqParts[1].Length / (double)divby).ToString();
                                        break;

                                    case 6:
                                        this.tb_Seq6.Text = SeqParts[1];
                                        this.lbl_lc6.Text = ((double)SeqParts[1].Length / (double)divby).ToString();
                                        break;

                                    case 7:
                                        this.tb_Seq7.Text = SeqParts[1];
                                        this.lbl_lc7.Text = ((double)SeqParts[1].Length / (double)divby).ToString();
                                        break;

                                    case 8:
                                        this.tb_Seq8.Text = SeqParts[1];
                                        this.lbl_lc8.Text = ((double)SeqParts[1].Length / (double)divby).ToString();
                                        break;

                                    default:
                                        this.tb_notes.Text = lines[i];
                                        break;


                                }
                            }

                            line++;
                        }

                        if (seqltr == 0)
                        {
                            rb_1Leter.Checked = true;
                            globals.i12Ltr = 0;
                        }
                        else
                        {
                            globals.i12Ltr = 1;
                            rb_2Letter.Checked = true;
                        }
                    }

                }
            }
        }
        private void SaveSSeq()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = @globals.sequence_path;
            saveFileDialog1.Title = "Save Sequence File";
            saveFileDialog1.DefaultExt = "seq";
            saveFileDialog1.Filter = "Sequence files (*.seq)|*.seq|All files (*.*)|*.*";


            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fname = saveFileDialog1.FileName;

                //write the file
                string[] lines = { this.tb_Seq1.Text, globals.i12Ltr.ToString("0"), this.tb_notes.Text };

                //fie overwrite trap
                if (File.Exists(fname))
                    File.Delete(fname);

                System.IO.File.WriteAllLines(fname, lines);
            }
            bDirty = false;
        }
        private void SaveMultSeq()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = @seq_path;
            saveFileDialog1.Title = "Save Sequence File";
            saveFileDialog1.DefaultExt = "csv";
            saveFileDialog1.Filter = "Batch files (*.csv)|*.csv|All files (*.*)|*.*";


            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fname = saveFileDialog1.FileName;

                //write the file
                string[] lines = { "", "", "", "", "", "", "", "", "", "", "", "", "" };
                int i = 0;
                //first write the format
                lines[i] = String.Format("{0}", globals.i12Ltr);
                i = i + 1;
                //now save the lines
                if (this.tb_Seq1.Text.Trim().Length > 0)
                {
                    lines[i] = String.Format("1,{0}", this.tb_Seq1.Text);
                    i = i + 1;
                }
                if (this.tb_Seq2.Text.Trim().Length > 0)
                {
                    lines[i] = String.Format("2,{0}", this.tb_Seq2.Text);
                    i = i + 1;
                }
                if (this.tb_Seq3.Text.Trim().Length > 0)
                {
                    lines[i] = String.Format("3,{0}", this.tb_Seq3.Text);
                    i = i + 1;
                }
                if (this.tb_Seq4.Text.Trim().Length > 0)
                {
                    lines[i] = String.Format("4,{0}", this.tb_Seq4.Text);
                    i = i + 1;
                }
                if (this.tb_Seq5.Text.Trim().Length > 0)
                {
                    lines[i] = String.Format("5,{0}", this.tb_Seq5.Text);
                    i = i + 1;
                }
                if (this.tb_Seq6.Text.Trim().Length > 0)
                {
                    lines[i] = String.Format("6,{0}", this.tb_Seq6.Text);
                    i = i + 1;
                }
                if (this.tb_Seq7.Text.Trim().Length > 0)
                {
                    lines[i] = String.Format("7,{0}", this.tb_Seq7.Text);
                    i = i + 1;
                }
                if (this.tb_Seq8.Text.Trim().Length > 0)
                {
                    lines[i] = String.Format("8,{0}", this.tb_Seq8.Text);
                    i = i + 1;
                }
                //finally the notes...
                i = i + 1;
                if (this.tb_notes.Text.Length > 0)
                {
                    lines[i] = String.Format("{0}", this.tb_notes.Text);
                }

                if (File.Exists(fname))
                    File.Delete(fname);

                System.IO.File.WriteAllLines(fname, lines);
            }

            bDirty = false;
        }
        private void ClearAll()
        {
            if (this.tb_Seq1.Text.Length > 0
                   || this.tb_Seq2.Text.Length > 0
                   || this.tb_Seq3.Text.Length > 0
                   || this.tb_Seq4.Text.Length > 0
                   || this.tb_Seq5.Text.Length > 0
                   || this.tb_Seq6.Text.Length > 0
                   || this.tb_Seq7.Text.Length > 0
                   || this.tb_Seq8.Text.Length > 0)
            {
                //save single sequence
                tb_Seq1.Clear();
                tb_Seq2.Clear();
                tb_Seq3.Clear();
                tb_Seq4.Clear();
                tb_Seq5.Clear();
                tb_Seq6.Clear();
                tb_Seq7.Clear();
                tb_Seq8.Clear();
                tb_notes.Clear();

                //clear the lenght labels
                lbl_lc1.Text = "0";
                lbl_lc2.Text = "0";
                lbl_lc3.Text = "0";
                lbl_lc4.Text = "0";
                lbl_lc5.Text = "0";
                lbl_lc6.Text = "0";
                lbl_lc7.Text = "0";
                lbl_lc8.Text = "0";

                //clear the masses
                l_m1.Text = "0";
                l_m2.Text = "0";
                l_m3.Text = "0";
                l_m4.Text = "0";
                l_m5.Text = "0";
                l_m6.Text = "0";
                l_m7.Text = "0";
                l_m8.Text = "0";

                cb_1.Text = "";
                cb_2.Text = "";
                cb_3.Text = "";
                cb_4.Text = "";
                cb_5.Text = "";
                cb_6.Text = "";
                cb_7.Text = "";
                cb_8.Text = "";

            }
            bDirty = false;
        }

        private void rb_SSeq_CheckedChanged(object sender, EventArgs e)
        {   //disable 2 through 8 only edit line one
            this.lbl_Col2.Enabled = false;
            this.lbl_Col3.Enabled = false;
            this.lbl_Col4.Enabled = false;
            this.lbl_Col5.Enabled = false;
            this.lbl_Col6.Enabled = false;
            this.lbl_Col7.Enabled = false;
            this.lbl_Col8.Enabled = false;

            this.tb_Seq2.Enabled = false;
            this.tb_Seq3.Enabled = false;
            this.tb_Seq4.Enabled = false;
            this.tb_Seq5.Enabled = false;
            this.tb_Seq6.Enabled = false;
            this.tb_Seq7.Enabled = false;
            this.tb_Seq8.Enabled = false;

            this.lbl_lc2.Enabled = false;
            this.lbl_lc3.Enabled = false;
            this.lbl_lc4.Enabled = false;
            this.lbl_lc5.Enabled = false;
            this.lbl_lc6.Enabled = false;
            this.lbl_lc7.Enabled = false;
            this.lbl_lc8.Enabled = false;

            this.l_m2.Enabled = false;
            this.l_m3.Enabled = false;
            this.l_m4.Enabled = false;
            this.l_m5.Enabled = false;
            this.l_m6.Enabled = false;
            this.l_m7.Enabled = false;
            this.l_m8.Enabled = false;

            this.cb_2.Enabled = false;
            this.cb_3.Enabled = false;
            this.cb_4.Enabled = false;
            this.cb_5.Enabled = false;
            this.cb_6.Enabled = false;
            this.cb_7.Enabled = false;
            this.cb_8.Enabled = false;

            ClearAll();

            //change the text for Col 1 to read Sequence
            this.lbl_Col1.Text = "Seq";
        }

        private void rb_BatchSeq_CheckedChanged(object sender, EventArgs e)
        {
            //disable 2 through 8 only edit line one
            this.lbl_Col2.Enabled = true;
            this.lbl_Col3.Enabled = true;
            this.lbl_Col4.Enabled = true;
            this.lbl_Col5.Enabled = true;
            this.lbl_Col6.Enabled = true;
            this.lbl_Col7.Enabled = true;
            this.lbl_Col8.Enabled = true;

            this.tb_Seq2.Enabled = true;
            this.tb_Seq3.Enabled = true;
            this.tb_Seq4.Enabled = true;
            this.tb_Seq5.Enabled = true;
            this.tb_Seq6.Enabled = true;
            this.tb_Seq7.Enabled = true;
            this.tb_Seq8.Enabled = true;

            this.lbl_lc2.Enabled = true;
            this.lbl_lc3.Enabled = true;
            this.lbl_lc4.Enabled = true;
            this.lbl_lc5.Enabled = true;
            this.lbl_lc6.Enabled = true;
            this.lbl_lc7.Enabled = true;
            this.lbl_lc8.Enabled = true;

            this.l_m2.Enabled = true;
            this.l_m3.Enabled = true;
            this.l_m4.Enabled = true;
            this.l_m5.Enabled = true;
            this.l_m6.Enabled = true;
            this.l_m7.Enabled = true;
            this.l_m8.Enabled = true;

            this.cb_2.Enabled = true;
            this.cb_3.Enabled = true;
            this.cb_4.Enabled = true;
            this.cb_5.Enabled = true;
            this.cb_6.Enabled = true;
            this.cb_7.Enabled = true;
            this.cb_8.Enabled = true;

            ClearAll();

            //change the text for Col 1 to read Sequence
            this.lbl_Col1.Text = "1";

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
            if (globals.bDirtyBase)
            {
                ReloadCBs();
                globals.bDirtyBase = false;
            }


        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bDirty)
            {
                if (MessageBox.Show("You have unsaved Sequences\n\nAre you sure you want to quit?", "Caution", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    return;
            }
            else
            {
                this.Close();
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            //decide if single sequence or batch
            //then save single sequence as structured tex with sequence then notes
            //save multiple sequence as CSV with notes at end...
            if (rb_SSeq.Checked)
            {
                if (this.tb_Seq1.Text.Length > 0)
                {
                    //save single sequence
                    SaveSSeq();
                    tb_Seq1.Clear();
                    l_m1.Text = "0";
                    lbl_lc1.Text = "0";
                    cb_1.Text = "";
                    tb_notes.Clear();
                }
                else
                    MessageBox.Show("Nothing to Save", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                //save multiple sequence
                if (this.tb_Seq1.Text.Length > 0
                       || this.tb_Seq2.Text.Length > 0
                       || this.tb_Seq3.Text.Length > 0
                       || this.tb_Seq4.Text.Length > 0
                       || this.tb_Seq5.Text.Length > 0
                       || this.tb_Seq6.Text.Length > 0
                       || this.tb_Seq7.Text.Length > 0
                       || this.tb_Seq8.Text.Length > 0)
                {
                    //save single sequence
                    SaveMultSeq();
                    tb_Seq1.Clear();
                    tb_Seq2.Clear();
                    tb_Seq3.Clear();
                    tb_Seq4.Clear();
                    tb_Seq5.Clear();
                    tb_Seq6.Clear();
                    tb_Seq7.Clear();
                    tb_Seq8.Clear();
                    tb_notes.Clear();

                    //clear the lengths
                    lbl_lc1.Text = "0";
                    lbl_lc2.Text = "0";
                    lbl_lc3.Text = "0";
                    lbl_lc4.Text = "0";
                    lbl_lc5.Text = "0";
                    lbl_lc6.Text = "0";
                    lbl_lc7.Text = "0";
                    lbl_lc8.Text = "0";

                    //clear the masses
                    l_m1.Text = "0";
                    l_m2.Text = "0";
                    l_m3.Text = "0";
                    l_m4.Text = "0";
                    l_m5.Text = "0";
                    l_m6.Text = "0";
                    l_m7.Text = "0";
                    l_m8.Text = "0";
                }
                else
                    MessageBox.Show("Nothing to Save", "Error");

                bDirty = false;
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void tb_Seq1_TextChanged(object sender, EventArgs e)
        {
            bDirty = true;
            int divby = 1;
            if (globals.i12Ltr == 0) { divby = 1; } else { divby = 3; }
            this.lbl_lc1.Text = Math.Floor(((double)this.tb_Seq1.Text.Length / (double)divby)).ToString("0");
            if (globals.i12Ltr == 1 && tb_Seq1.Text.Length % 3 == 0)
                this.l_m1.Text = CalcMass(tb_Seq1.Text);
            else if (globals.i12Ltr == 0)
                this.l_m1.Text = CalcMass(tb_Seq1.Text);
        }

        private void tb_Seq2_TextChanged(object sender, EventArgs e)
        {
            bDirty = true;
            int divby = 1;
            if (globals.i12Ltr == 0) { divby = 1; } else { divby = 3; }
            this.lbl_lc2.Text = Math.Floor(((double)this.tb_Seq2.Text.Length / (double)divby)).ToString("0");
            if (globals.i12Ltr == 1 && tb_Seq2.Text.Length % 3 == 0)
                this.l_m2.Text = CalcMass(tb_Seq2.Text);
            else if (globals.i12Ltr == 0)
                this.l_m2.Text = CalcMass(tb_Seq2.Text);
        }

        private void tb_Seq3_TextChanged(object sender, EventArgs e)
        {
            bDirty = true;
            int divby = 1;
            if (globals.i12Ltr == 0) { divby = 1; } else { divby = 3; }
            this.lbl_lc3.Text = Math.Floor(((double)this.tb_Seq3.Text.Length / (double)divby)).ToString("0");
            if (globals.i12Ltr == 1 && tb_Seq3.Text.Length % 3 == 0)
                this.l_m3.Text = CalcMass(tb_Seq3.Text);
            else if (globals.i12Ltr == 0)
                this.l_m3.Text = CalcMass(tb_Seq3.Text);
        }

        private void tb_Seq4_TextChanged(object sender, EventArgs e)
        {
            bDirty = true;
            int divby = 1;
            if (globals.i12Ltr == 0) { divby = 1; } else { divby = 3; }
            this.lbl_lc4.Text = Math.Floor(((double)this.tb_Seq4.Text.Length / (double)divby)).ToString("0");
            if (globals.i12Ltr == 1 && tb_Seq4.Text.Length % 3 == 0)
                this.l_m4.Text = CalcMass(tb_Seq4.Text);
            else if (globals.i12Ltr == 0)
                this.l_m4.Text = CalcMass(tb_Seq4.Text);
        }

        private void tb_Seq5_TextChanged(object sender, EventArgs e)
        {
            bDirty = true;
            int divby = 1;
            if (globals.i12Ltr == 0) { divby = 1; } else { divby = 3; }
            this.lbl_lc5.Text = Math.Floor(((double)this.tb_Seq5.Text.Length / (double)divby)).ToString("0");
            if (globals.i12Ltr == 1 && tb_Seq5.Text.Length % 3 == 0)
                this.l_m5.Text = CalcMass(tb_Seq5.Text);
            else if (globals.i12Ltr == 0)
                this.l_m5.Text = CalcMass(tb_Seq5.Text);
        }

        private void tb_Seq6_TextChanged(object sender, EventArgs e)
        {
            bDirty = true;
            int divby = 1;
            if (globals.i12Ltr == 0) { divby = 1; } else { divby = 3; }
            this.lbl_lc6.Text = Math.Floor(((double)this.tb_Seq6.Text.Length / (double)divby)).ToString("0");
            if (globals.i12Ltr == 1 && tb_Seq6.Text.Length % 3 == 0)
                this.l_m6.Text = CalcMass(tb_Seq6.Text);
            else if (globals.i12Ltr == 0)
                this.l_m6.Text = CalcMass(tb_Seq6.Text);
        }

        private void tb_Seq7_TextChanged(object sender, EventArgs e)
        {
            bDirty = true;
            int divby = 1;
            if (globals.i12Ltr == 0) { divby = 1; } else { divby = 3; }
            this.lbl_lc7.Text = Math.Floor(((double)this.tb_Seq7.Text.Length / (double)divby)).ToString("0");
            if (globals.i12Ltr == 1 && tb_Seq7.Text.Length % 3 == 0)
                this.l_m7.Text = CalcMass(tb_Seq7.Text);
            else if (globals.i12Ltr == 0)
                this.l_m7.Text = CalcMass(tb_Seq7.Text);
        }

        private void tb_Seq8_TextChanged(object sender, EventArgs e)
        {
            bDirty = true;
            int divby = 1;
            if (globals.i12Ltr == 0) { divby = 1; } else { divby = 3; }
            this.lbl_lc8.Text = Math.Floor(((double)this.tb_Seq8.Text.Length / (double)divby)).ToString("0");
            if (globals.i12Ltr == 1 && tb_Seq8.Text.Length % 3 == 0)
                this.l_m8.Text = CalcMass(tb_Seq8.Text);
            else if (globals.i12Ltr == 0)
                this.l_m8.Text = CalcMass(tb_Seq8.Text);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSeqFile();
        }

        private void Menu_Help_Click(object sender, EventArgs e)
        {
            Process.Start(globals.Help_Path);
        }
        private string CalcMass(string massstring)
        {

            char[] masschar = massstring.Trim().ToCharArray();
            double mass = 0.0;

            if (globals.i12Ltr == 0)
            {
                foreach (char c in masschar)
                {
                    mass = mass + RetMass(c.ToString());
                }
            }
            else
            {
                if (massstring.Length > 2)
                {
                    string[] ltrs2 = new string[Convert.ToInt16(Math.Ceiling((double)masschar.Length / (double)3))];
                    int cntr = 0;

                    for (int i = 0; i < masschar.Length; i = i + 3)
                    {
                        if (masschar.Length > ((3 * (cntr + 1)) - 1))
                        {
                            ltrs2[cntr] = masschar[i].ToString() + masschar[i + 1].ToString();
                            Debug.WriteLine(ltrs2[cntr]);
                            cntr = cntr + 1;
                        }
                    }
                    foreach (string s in ltrs2)
                    {
                        mass = mass + RetMass(s);
                    }
                }
            }
            if (mass > 0)
                mass = mass - 62;
            else
                mass = 0;

            return mass.ToString("0.0"); ;

        }
        private double RetMass(string c)
        {
            int index = 0;

            if (c == null || c == string.Empty)
                return 0.0;

            if (globals.i12Ltr == 0)  //1 letter code
                index = 0;
            else
                index = 1;

            if (index == 1 && c.Length % 2 != 0)
                return 0.0;

            for (int i = 0; i < ibases; i++)
            {
                string[] parts = bases[i].Split(',');
                if (parts[index].Equals(c))
                    return (Double.Parse(parts[3]));
            }
            MessageBox.Show("Base not in Base Table", "Error", MessageBoxButtons.OK);
            return 0.0;
        }

        private void Sequence_Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bDirty)
            {
                if (MessageBox.Show("You have unsaved Sequences\n\nAre you sure you want to quit?", "Caution", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    return;
            }
        }

        private void cb_1_MouseUp(object sender, MouseEventArgs e)
        {
            // MessageBox.Show("Mouse Up " + cb_1.Text);
        }

        private void cb_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //check for no click
            if (cb_1.Text == string.Empty)
                return;

            //MessageBox.Show("Selected Item Changed " + cb_1.Text);
            if (globals.i12Ltr == 0)
                tb_Seq1.AppendText(cb_1.Text);
            else
                tb_Seq1.AppendText(cb_1.Text + "-");

            return;
        }

        private void rb_1Leter_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_1Leter.Checked && init)
            {
                //clear the list boxes
                cb_1.Items.Clear();
                cb_1.Text = "";
                cb_2.Items.Clear();
                cb_2.Text = "";
                cb_3.Items.Clear();
                cb_3.Text = "";
                cb_4.Items.Clear();
                cb_4.Text = "";
                cb_5.Items.Clear();
                cb_5.Text = "";
                cb_6.Items.Clear();
                cb_6.Text = "";
                cb_7.Items.Clear();
                cb_7.Text = "";
                cb_8.Items.Clear();
                cb_8.Text = "";

                int index = 0;

                //update the global and properties settings
                globals.i12Ltr = 0;  //1 letter code
                index = 0;

                Properties.Settings.Default.Ltr_12 = 0;
                Properties.Settings.Default.Save();

                //check the text boxes, you can't have a mixed 1 letter 2 letter sequence
                NoMixedSequence(0);

                //rebuild the codes array
                string[] codes = new string[ibases];
                for (int i = 0; i < ibases; i++)
                {
                    string[] parts = bases[i].Split(',');
                    codes[i] = parts[index];
                }

                //reload the list boxes
                cb_1.Items.AddRange(codes);
                cb_2.Items.AddRange(codes);
                cb_3.Items.AddRange(codes);
                cb_4.Items.AddRange(codes);
                cb_5.Items.AddRange(codes);
                cb_6.Items.AddRange(codes);
                cb_7.Items.AddRange(codes);
                cb_8.Items.AddRange(codes);

                Man_Controlcs.UpdatePropConfigLetterCodes(ibases, bases);
            }
        }

        private void rb_2Letter_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_2Letter.Checked && init)
            {
                int index = 0;

                //clear the list boxes
                cb_1.Items.Clear();
                cb_1.Text = "";
                cb_2.Items.Clear();
                cb_2.Text = "";
                cb_3.Items.Clear();
                cb_3.Text = "";
                cb_4.Items.Clear();
                cb_4.Text = "";
                cb_5.Items.Clear();
                cb_5.Text = "";
                cb_6.Items.Clear();
                cb_6.Text = "";
                cb_7.Items.Clear();
                cb_7.Text = "";
                cb_8.Items.Clear();
                cb_8.Text = "";

                bDirty = false;

                //update the global and properties settings
                globals.i12Ltr = 1;  //2 letter code
                index = 1;

                Properties.Settings.Default.Ltr_12 = 1;
                Properties.Settings.Default.Save();

                //check the text boxes, you can't have a mixed 1 letter 2 letter sequence
                NoMixedSequence(1);

                //rebuild the codes array
                string[] codes = new string[ibases];
                for (int i = 0; i < ibases; i++)
                {
                    string[] parts = bases[i].Split(',');
                    codes[i] = parts[index];
                }

                //reload the list boxes
                cb_1.Items.AddRange(codes);
                cb_2.Items.AddRange(codes);
                cb_3.Items.AddRange(codes);
                cb_4.Items.AddRange(codes);
                cb_5.Items.AddRange(codes);
                cb_6.Items.AddRange(codes);
                cb_7.Items.AddRange(codes);
                cb_8.Items.AddRange(codes);

                Man_Controlcs.UpdatePropConfigLetterCodes(ibases, bases);
            }
        }
        private void NoMixedSequence(int to)
        {
            if (tb_Seq1.Text.Length > 0)
            {
                l_m1.Text = "0";
                Man_Controlcs.SeqConvertLetterCodes(tb_Seq1, to, ibases, bases);
            }
            if (tb_Seq2.Text.Length > 0)
            {
                l_m2.Text = "0";
                Man_Controlcs.SeqConvertLetterCodes(tb_Seq2, to, ibases, bases);
            }
            if (tb_Seq3.Text.Length > 0)
            {
                l_m3.Text = "0";
                Man_Controlcs.SeqConvertLetterCodes(tb_Seq3, to, ibases, bases);
            }
            if (tb_Seq4.Text.Length > 0)
            {
                l_m4.Text = "0";
                Man_Controlcs.SeqConvertLetterCodes(tb_Seq4, to, ibases, bases);
            }
            if (tb_Seq5.Text.Length > 0)
            {
                l_m5.Text = "0";
                Man_Controlcs.SeqConvertLetterCodes(tb_Seq5, to, ibases, bases);
            }
            if (tb_Seq6.Text.Length > 0)
            {
                l_m6.Text = "0";
                Man_Controlcs.SeqConvertLetterCodes(tb_Seq6, to, ibases, bases);
            }
            if (tb_Seq7.Text.Length > 0)
            {
                l_m7.Text = "0";
                Man_Controlcs.SeqConvertLetterCodes(tb_Seq7, to, ibases, bases);
            }
            if (tb_Seq8.Text.Length > 0)
            {
                l_m8.Text = "0";
                Man_Controlcs.SeqConvertLetterCodes(tb_Seq8, to, ibases, bases);
            }
        }

        private void ReloadCBs()
        {
            //clear the list boxes
            cb_1.Items.Clear();
            cb_1.Text = "";
            cb_2.Items.Clear();
            cb_2.Text = "";
            cb_3.Items.Clear();
            cb_3.Text = "";
            cb_4.Items.Clear();
            cb_4.Text = "";
            cb_5.Items.Clear();
            cb_5.Text = "";
            cb_6.Items.Clear();
            cb_6.Text = "";
            cb_7.Items.Clear();
            cb_7.Text = "";
            cb_8.Items.Clear();
            cb_8.Text = "";

            int index = 0;

            //update the global and properties settings
            //use the currently selected code as our index
            index = globals.i12Ltr;  //note this may have actually changed when editing the base table...

            //check the text boxes, you can't have a mixed 1 letter 2 letter sequence
            NoMixedSequence(globals.i12Ltr);

            //rebuild the codes array
            string[] codes = new string[ibases];
            for (int i = 0; i < ibases; i++)
            {
                string[] parts = bases[i].Split(',');
                codes[i] = parts[index];
            }

            //reload the list boxes
            cb_1.Items.AddRange(codes);
            cb_2.Items.AddRange(codes);
            cb_3.Items.AddRange(codes);
            cb_4.Items.AddRange(codes);
            cb_5.Items.AddRange(codes);
            cb_6.Items.AddRange(codes);
            cb_7.Items.AddRange(codes);
            cb_8.Items.AddRange(codes);
        }

        private void btn_Open_Click(object sender, EventArgs e)
        {
            Menu_Open.PerformClick();

        }

        private void Menu_Save_Click(object sender, EventArgs e)
        {
            btn_Save.PerformClick();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            //exitToolStripMenuItem.PerformClick();
        }

        private void groupBox1_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(groupBox1, "Sequence Type..", "You can either enter a single sequence or a batch of sequences\nto be synthesized in a Run....");
        }

        private void SeqTable_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(SeqTable, "Sequence Table..", "Enter either a single sequence or batch of seuquences, you may\neither type directly or select the bases from the drop down list...");
        }

        private void tb_notes_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(tb_notes, "Notes..", "Enter notes to describe the sequence or batch of sequences being edited...");
        }

        private void btn_Open_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_Open, "Open..", "Open an existing sequence or batch file for editing...");
        }

        private void btn_Save_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_Save, "Save..", "Save the existing sequence or batch to a disk file...");
        }

        private void btn_Clear_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_Clear, "Clear..", "Clear all sequences from the sequence table...");
        }

        private void btn_Close_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_Close, "Close..", "Close the sequence editor and return to the Main Menu...");
        }

        private void gb_Use_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(gb_Use, "Codes..", "You can select either single letter or two letter codes for entering sequences in the sequence table...");

        }
    }


}
