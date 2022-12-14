using System;
using System.Windows.Forms;

namespace SeNA80
{
    public partial class Pause_Time : Form
    {
        public Pause_Time()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(383, 249);
            pnl_timer.Visible = false;
            this.cbHowLong.SelectedIndex = 2;
            this.cbHowLong.Focus();
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            int toadd = int.Parse(cbHowLong.Text) * 60;
            DateTime dtstart = DateTime.Now;
            DateTime dtfinish = dtstart.AddSeconds(toadd);

            Man_Controlcs.SyncWait(500);

            //set the global variables
            globals.bWaiting = true;
            globals.bIsPaused = true;

            //expand timer to show numbers;
            this.Size = new System.Drawing.Size(383, 249);
            pnl_timer.Visible = true;

            while (DateTime.Now < dtfinish)
            {
                TimeSpan togo = dtfinish.Subtract(DateTime.Now);

                string time = new DateTime(togo.Ticks).ToString("HH:mm:ss");

                lbl_CountDown.Text = time;
                
                SeNARun.SafeSetStatus("Pased for " + time + "sec");
                Man_Controlcs.SyncWait(500);
            }
        }
    }
}
