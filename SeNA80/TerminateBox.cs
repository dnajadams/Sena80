using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeNA80
{
    public partial class TerminateBox : Form
    {
        public static bool bTImmediately = false;
        public static bool bTStep = false;
        public static bool bTCycle = false;

        public TerminateBox()
        {
            InitializeComponent();
        }

        private void btn_Immediately_Click(object sender, EventArgs e)
        {
            SeNARun.SafeSetStatus("Terminating Run Immediately...");
            
            SeNARun.bRunProcessing = false;
            globals.bIsRunning = false;
            globals.iCycle = SeNARun.MaxCycles + 2;
            
            this.Close();
            return;
        }
        private void btn_EndofStep_Click(object sender, EventArgs e)
        {
            SeNARun.SafeSetStatus("Terminating At End of Current Step...");
            SeNARun.bTerminateEndofStep = true;

            this.Close();
            return;
        }
        private void btn_EndofCycle_Click(object sender, EventArgs e)
        {
            SeNARun.SafeSetStatus("Terminating at the end of the Current Cycle....");
            SeNARun.bTerminateEndofCycle = true;

            this.Close();
            return;
        }

        private void btn_Immediately_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_Immediately, "Immediately..", "Terminate the existing Run Immediately\nNote: valves may be left open!...");
        }

        private void btn_EndofStep_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_EndofStep, "End of Step..", "Safely Terminate the Run when the existing step is complete...");
        }

        private void btn_EndofCycle_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_EndofCycle, "End Of Cycle..", "Safely Terminate the Run at the end of the current cycle\nthe last step in the looping protocol...");
        }

        private void btn_Cancel_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_Cancel, "Cancel..", "Return to the Run program without terminating...");


        }
    }
}
