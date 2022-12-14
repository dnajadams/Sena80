using System;
using System.Windows.Forms;

namespace SeNA80
{
    public partial class SeNARun
    {
        private void btn_LoadProtocols_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_LoadProtocols, "Load Protocols", "Click to select the protocols for the current synthesis...");
        }

        private void rb_Parallel_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(rb_Parallel, "Parallel Run Mode", "Run Multiple Sequences in parallel,\nall at the same time, will automatically add\nprotocols for PS and PO oxidation...");
        }

        private void rb_Sequenntial_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(rb_Sequenntial, "Sequential Run Mode", "Run Multiple Sequences in sequentially,\none sequence at a time, in order...");
        }

        private void rb_StdDelivery_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(rb_StdDelivery, "Standard Delivery Mode", "Each column is coupled one at a time starting with the first, then second, etc...");
        }

        private void rb_Smart_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(rb_Smart, "Smart Delivery Option", "When 2 or more columns need the same base coupled, during the same cycle\nthey will be coupled at the same simultaneously...");
        }

        private void btn_Run_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_Run, "Start Run", "Select to beging the cyclic synthesis processing of the sequences input\nwith the protocols selected..");
        }

        private void btn_Pause_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_Pause, "Pause Run", "Pausing will allow you to pause the current synthesis for a fixed amount of time...");
        }

        private void btn_Terminate_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_Terminate, "Terminate the Run", "Run termination provides several options to either immediately\nor gracefully terminate the existing synthesis...");
        }

        private void btn_LoadBatch_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_LoadBatch, "Load Batch", "Allows you to select a batch file to load multiple sequences from a single file...");
        }

        private void btn_col1_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_col1, "Select Column 1 Sequence", "Select the sequence file to load for the column 1 position....");
        }

        private void btn_ClrCol1_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_ClrCol1, "Clear Column 1 Sequence", "Clear and Reset the sequence in the column 1 position....");
        }

        private void btn_col2_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_col2, "Select Column 2 Sequence", "Select the sequence file to load for the column 2 position....");
        }

        private void btn_ClrCol2_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_ClrCol2, "Clear Column 2 Sequence", "Clear and Reset the sequence in the column 2 position....");
        }

        private void btn_col3_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_col3, "Select Column 3 Sequence", "Select the sequence file to load for the column 3 position....");
        }

        private void btn_ClrCol3_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_ClrCol3, "Clear Column 3 Sequence", "Clear and Reset the sequence in the column 3 position....");
        }

        private void btn_col4_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_col4, "Select Column 4 Sequence", "Select the sequence file to load for the column 4 position....");
        }

        private void btn_ClrCol4_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_ClrCol4, "Clear Column 4 Sequence", "Clear and Reset the sequence in the column 4 position....");
        }

        private void btn_col5_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_col5, "Select Column 5 Sequence", "Select the sequence file to load for the column 5 position....");
        }

        private void btn_ClrCol5_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_ClrCol5, "Clear Column 5 Sequence", "Clear and Reset the sequence in the column 5 position....");
        }

        private void btn_col6_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_col6, "Select Column 6 Sequence", "Select the sequence file to load for the column 6 position....");
        }

        private void btn_ClrCol6_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_ClrCol6, "Clear Column 6 Sequence", "Clear and Reset the sequence in the column 6 position....");
        }

        private void btn_col7_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_col7, "Select Column 7 Sequence", "Select the sequence file to load for the column 7 position....");
        }

        private void btn_ClrCol7_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_ClrCol7, "Clear Column 7 Sequence", "Clear and Reset the sequence in the column 7 position....");
        }

        private void btn_col8_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_col8, "Select Column 8 Sequence", "Select the sequence file to load for the column 8 position....");
        }

        private void btn_ClrCol8_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_ClrCol8, "Clear Column 8 Sequence", "Clear and Reset the sequence in the column 8 position....");
        }

        private void btn_ResetAll_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_ResetAll, "Clear All Columns", "Clear and Reset the sequences for all columns....");
        }

        private void btn_Consumption_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_Consumption, "Consumption Calculator", "Displays the approximate volumes of each reagent that will be used\nduring the synthesis of the sequences loaded....");
        }

        private void btn_OK_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_OK, "Return", "Leave the Run Program and Return to the Main Menu....");
        }

        private void Menu_Tips_Click(object sender, EventArgs e)
        {
            if (Menu_Tips.Checked)
            {
                Menu_Tips.Checked = false;
                Menu_Tips.Text = "&Enable Tips..";
                globals.bShowTips = false;
                Properties.Settings.Default.Tips_Enabled = false;
            }
            else
            {
                Menu_Tips.Checked = true;
                Menu_Tips.Text = "&Disable Tips..";
                globals.bShowTips = true;
                Properties.Settings.Default.Tips_Enabled = true;
            }
        }

    }
}
