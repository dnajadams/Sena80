using System;
using System.Linq;
using System.Windows.Forms;

namespace SeNA80
{
    public partial class Reagent_Parameters : Form
    {
        public static Char ColByp = 'B';
        public static char PumpByp = 'B';
        public static string ColStr = "";
        public static string sVol = "";
        public static String  sCol = "000000000";


        public Reagent_Parameters()
        {
            InitializeComponent();

            

        }
        private void Reagent_Parameters_Load(object sender, EventArgs e)
        {
            string[] parts = lbl_Selected.Text.Split(',');
            
            // sRet = parts[0] + "," + Reagent_Parameters.ColByp + "," +
            //      Reagent_Parameters.sCol + "," + Reagent_Parameters.PumpByp + "," + Reagent_Parameters.sVol;

            if (parts[1].Contains("C"))
            {
                rbCol.Checked = true;
                rbBypass.Checked = false;


                char[] iColSel = new char[sCol.Length + 1];
                iColSel = parts[2].ToCharArray();

                if (iColSel[0] == '1')
                    cb_Col1.Checked = true;
                if (iColSel[1] == '1')
                    cb_Col2.Checked = true;
                if (iColSel[2] == '1')
                    cb_Col3.Checked = true;
                if (iColSel[3] == '1')
                    cb_Col4.Checked = true;
                if (iColSel[4] == '1')
                    cb_Col5.Checked = true;
                if (iColSel[5] == '1')
                    cb_Col6.Checked = true;
                if (iColSel[6] == '1')
                    cb_Col7.Checked = true;
                if (iColSel[7] == '1')
                    cb_Col8.Checked = true;
                if (iColSel[8] == '1')
                    cbSequence.Checked = true;

                if (cbSequence.Checked)
                {
                    cb_Col1.Enabled = cb_Col2.Enabled = cb_Col3.Enabled = cb_Col4.Enabled = false;
                    cb_Col5.Enabled = cb_Col6.Enabled = cb_Col7.Enabled = cb_Col8.Enabled = false;
                    cbSequence.Enabled = true;
                }
                else
                {
                    cb_Col1.Enabled = cb_Col2.Enabled = cb_Col3.Enabled = cb_Col4.Enabled = true;
                    cb_Col5.Enabled = cb_Col6.Enabled = cb_Col7.Enabled = cb_Col8.Enabled = true;
                    cbSequence.Enabled = true;
                }
            }

            if (parts[3].Contains('P'))
            {
                rb_ByPump.Checked = false;
                rb_Pump.Checked = true;
                gb_FlowControl.Enabled = false;
                gb_PumpControl.Enabled = true;
                nu_Volume.Value = Convert.ToInt32(parts[4]);
            }
            else
            {
                n_FlowSec.Value = Convert.ToInt32(parts[4]);
            }

        }
        private void rbBypass_CheckedChanged(object sender, EventArgs e)
        {
            gbColumns.Enabled = false;
            ColByp = 'B';
        }

        private void rbCol_CheckedChanged(object sender, EventArgs e)
        {
            gbColumns.Enabled = true;
            ColByp = 'C';
            if(Protocol_Editor.iCurList != 0)
            {
                cb_Col1.Enabled = false;
                cb_Col2.Enabled = false;
                cb_Col3.Enabled = false;
                cb_Col4.Enabled = false;
                cb_Col5.Enabled = false;
                cb_Col6.Enabled = false;
                cb_Col7.Enabled = false;
                cb_Col8.Enabled = false;
                cbSequence.Enabled = true;
                cbSequence.Checked = true;
            }
            else
            {

                cb_Col1.Enabled = true;
                cb_Col2.Enabled = true;
                cb_Col3.Enabled = true;
                cb_Col4.Enabled = true;
                cb_Col5.Enabled = true;
                cb_Col6.Enabled = true;
                cb_Col7.Enabled = true;
                cb_Col8.Enabled = true;
                cbSequence.Enabled = true;
                cbSequence.Checked = false;
            }
        }

        private void rb_ByPump_CheckedChanged(object sender, EventArgs e)
        {
            gb_PumpControl.Enabled = false;
            gb_FlowControl.Enabled = true;
            PumpByp = 'B';
        }

        private void rb_Pump_CheckedChanged(object sender, EventArgs e)
        {
            gb_PumpControl.Enabled = true;
            gb_FlowControl.Enabled = false;
            PumpByp = 'P';
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (PumpByp == 'P')
                sVol = nu_Volume.Value.ToString();
            else
                sVol = n_FlowSec.Value.ToString();

            if (ColByp == 'C')
            {
                char[] iCol = new char[sCol.Length];
                iCol = sCol.ToCharArray();
                sCol = "";

                if (cb_Col1.Checked)
                    iCol[0] = '1';
                if (cb_Col2.Checked)
                    iCol[1] = '1';
                if (cb_Col3.Checked)
                    iCol[2] = '1';
                if (cb_Col4.Checked)
                    iCol[3] = '1';
                if (cb_Col5.Checked)
                    iCol[4] = '1';
                if (cb_Col6.Checked)
                    iCol[5] = '1';
                if (cb_Col7.Checked)
                    iCol[6] = '1';
                if (cb_Col8.Checked)
                    iCol[7] = '1';
                if (cbSequence.Checked)
                    iCol[8] = '1';


                for (int j = 0; j < 9; j++)
                    sCol += iCol[j].ToString();
            }

        }

        private void cbSequence_CheckedChanged(object sender, EventArgs e)
        {
            if(cbSequence.Checked)
            {
                cb_Col1.Enabled = cb_Col2.Enabled = cb_Col3.Enabled = cb_Col4.Enabled = cb_Col5.Enabled = cb_Col6.Enabled = cb_Col7.Enabled = cb_Col8.Enabled = false;
                cb_Col1.Checked = cb_Col2.Checked = cb_Col3.Checked = cb_Col4.Checked = cb_Col5.Checked = cb_Col6.Checked = cb_Col7.Checked = cb_Col8.Checked = false;
            }
            else
            {
                cb_Col1.Enabled = cb_Col2.Enabled = cb_Col3.Enabled = cb_Col4.Enabled = cb_Col5.Enabled = cb_Col6.Enabled = cb_Col7.Enabled = cb_Col8.Enabled = true;

            }
        }
    }
}
