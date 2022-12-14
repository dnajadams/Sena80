using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeNA80
{
    public partial class Pressuriz : Form
    {
        bool bWait = false;
        async Task PutTaskDelay(int secn)
        {
            await Task.Delay(secn);
        }

        public async void PressFor(string howlong)
        {
            int wSecWait = int.Parse(howlong);
            //MessageBox.Show("Waiting " + howlong);

            //set the interval based on the length of the wait
            if (wSecWait < 30)
                globals.runform.RunTimer.Interval = 1000;    // every 1 second
            else if (wSecWait > 31 && wSecWait < 90)
                globals.runform.RunTimer.Interval = 10000;  //every 10 seconds
            else if (wSecWait > 91 && wSecWait < 600)
                globals.runform.RunTimer.Interval = 30000;  //every 30 seconds
            else
                globals.runform.RunTimer.Interval = 60000;  //every 60 seconds
            bWait = true;                                     // this is the easiest, just sit on a wait
            globals.runform.RunTimer.Start();
            globals.start_time = DateTime.Now;
            int wTime = int.Parse(howlong) * 1000;

            await PutTaskDelay(wTime);

            globals.runform.RunTimer.Stop();
            bWait = false;
        }


        string wTime = "10";
        string bwTime = "15";


        public Pressuriz()
        {
            InitializeComponent();
            //we will put these in later when we get the other valves installed.
            rbaAmidites1.Enabled = true;
            rbAmidites2.Enabled = true;
            rbReagents1.Enabled = true;
            rbReagents2.Enabled = true;
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

        private async void btnStart_Click(object sender, EventArgs e)
        {
            //disable exit
            this.btnExit.Enabled = false;
            this.exitToolStripMenuItem.Enabled = false;

            if (rbAll.Checked)
            {
                this.btnStart.Enabled = false;
                this.exitToolStripMenuItem.Enabled = false;
                this.btnExit.Enabled = false;

                Man_Controlcs.SendControllerMsg(1, valves.washagason);
                PressFor(bwTime);
                while (bWait)
                    await PutTaskDelay(20);
                Man_Controlcs.SendControllerMsg(1, valves.washagasoff);
                Man_Controlcs.WriteStatus("Pressurize", "Finished Pressurizing Wash A");

                Man_Controlcs.SendControllerMsg(1, valves.washbgason);
                PressFor(bwTime);
                while (bWait)
                    await PutTaskDelay(20);
                Man_Controlcs.SendControllerMsg(1, valves.washbgasoff);
                Man_Controlcs.WriteStatus("Pressurize", "Finished Pressurizing Wash B");

                Man_Controlcs.SendControllerMsg(1, valves.xtra2gason);
                PressFor(wTime);
                while (bWait)
                    await PutTaskDelay(10);
                Man_Controlcs.SendControllerMsg(1, valves.xtra2gasoff);
                Man_Controlcs.WriteStatus("Pressurize", "Finished Pressurizing Extra 2");

                Man_Controlcs.SendControllerMsg(1, valves.xtra1gason);
                PressFor(wTime);
                while (bWait)
                    await PutTaskDelay(10);
                Man_Controlcs.SendControllerMsg(1, valves.xtra1gasoff);
                Man_Controlcs.WriteStatus("Pressurize", "Finished Pressurizing DEA");

                Man_Controlcs.SendControllerMsg(1, valves.ox1gason);
                PressFor(wTime);
                while (bWait)
                    await PutTaskDelay(10);
                Man_Controlcs.SendControllerMsg(1, valves.ox1gasoff);
                Man_Controlcs.WriteStatus("Pressurize", "Finished Pressurizing Oxidizer");

                Man_Controlcs.SendControllerMsg(1, valves.ox2gason);
                PressFor(wTime);
                while (bWait)
                    await PutTaskDelay(10);
                Man_Controlcs.SendControllerMsg(1, valves.ox2gasoff);
                Man_Controlcs.WriteStatus("Pressurize", "Finished Pressurizing Thiol");

                Man_Controlcs.SendControllerMsg(1, valves.capagason);
                PressFor(wTime);
                while (bWait)
                    await PutTaskDelay(10);
                Man_Controlcs.SendControllerMsg(1, valves.capagasoff);
                Man_Controlcs.WriteStatus("Pressurize", "Finished Pressurizing Cap A Reagents");

                Man_Controlcs.SendControllerMsg(1, valves.capbgason);
                PressFor(wTime);
                while (bWait)
                    await PutTaskDelay(10);
                Man_Controlcs.SendControllerMsg(1, valves.capbgasoff);
                Man_Controlcs.WriteStatus("Pressurize", "Finished Pressurizing Cap B Reagents");

                Man_Controlcs.SendControllerMsg(1, valves.deblgason);
                PressFor(bwTime);
                while (bWait)
                    await PutTaskDelay(20);
                Man_Controlcs.SendControllerMsg(1, valves.deblgasoff);
                Man_Controlcs.WriteStatus("Pressurize", "Finished Pressurizing Deblock");

                //activators
                Man_Controlcs.SendControllerMsg(1, valves.act1gason);
                PressFor(bwTime);
                while (bWait)
                    await PutTaskDelay(20);
                Man_Controlcs.SendControllerMsg(1, valves.act1gasoff);
                Man_Controlcs.WriteStatus("Pressurize", "Finished Pressurizing Act 1");

                Man_Controlcs.SendControllerMsg(1, valves.act2gason);
                PressFor(bwTime);
                while (bWait)
                    await PutTaskDelay(20);
                Man_Controlcs.SendControllerMsg(1, valves.act2gasoff);
                Man_Controlcs.WriteStatus("Pressurize", "Finished Pressurizing Act 2");

                //now amidites
                Man_Controlcs.SendControllerMsg(1, valves.pressuretop);
                PressFor(wTime);
                while (bWait)
                    await PutTaskDelay(20);
                Man_Controlcs.SendControllerMsg(1, valves.pressurebot);
                Man_Controlcs.WriteStatus("Pressurize", "Finished Pressurizing Bank 1 Amidites");

                Man_Controlcs.SendControllerMsg(1, valves.pressurebot);
                PressFor(wTime);
                while (bWait)
                    await PutTaskDelay(20);
                Man_Controlcs.SendControllerMsg(1, valves.pressuretop);
                Man_Controlcs.WriteStatus("Pressurize", "Finished Pressurizing Bank 2 Amidites");

                this.btnStart.Enabled = true;
                this.exitToolStripMenuItem.Enabled = true;
                this.btnExit.Enabled = true;

            }
            //just the big bottles
            else if (rbReagents1.Checked)
            {
                this.btnStart.Enabled = false;
                this.exitToolStripMenuItem.Enabled = false;
                this.btnExit.Enabled = false;

                Man_Controlcs.SendControllerMsg(1, valves.washagason);
                PressFor(bwTime);
                while (bWait)
                    await PutTaskDelay(20);
                Man_Controlcs.SendControllerMsg(1, valves.washagasoff);
                Man_Controlcs.WriteStatus("Pressurize", "Finished Pressurizing Wash A");

                Man_Controlcs.SendControllerMsg(1, valves.washbgason);
                PressFor(bwTime);
                while (bWait)
                    await PutTaskDelay(20);
                Man_Controlcs.SendControllerMsg(1, valves.washbgasoff);
                Man_Controlcs.WriteStatus("Pressurize", "Finished Pressurizing Wash B");

                Man_Controlcs.SendControllerMsg(1, valves.deblgason);
                PressFor(bwTime);
                while (bWait)
                    await PutTaskDelay(20);
                Man_Controlcs.SendControllerMsg(1, valves.deblgasoff);
                Man_Controlcs.WriteStatus("Pressurize", "Finished Pressurizing Deblock");

                this.btnStart.Enabled = true;
                this.exitToolStripMenuItem.Enabled = true;
                this.btnExit.Enabled = true;

            }
            else if (rbReagents2.Checked)
            //all the rest of the reagents
            {
                this.btnStart.Enabled = false;
                this.exitToolStripMenuItem.Enabled = false;
                this.btnExit.Enabled = false;

                Man_Controlcs.SendControllerMsg(1, valves.xtra2gason);
                PressFor(wTime);
                while (bWait)
                    await PutTaskDelay(10);
                Man_Controlcs.SendControllerMsg(1, valves.xtra2gasoff);
                Man_Controlcs.WriteStatus("Pressurize", "Finished Pressurizing Extra 2");

                Man_Controlcs.SendControllerMsg(1, valves.xtra1gason);
                PressFor(wTime);
                while (bWait)
                    await PutTaskDelay(10);
                Man_Controlcs.SendControllerMsg(1, valves.xtra1gasoff);
                Man_Controlcs.WriteStatus("Pressurize", "Finished Pressurizing DEA");

                Man_Controlcs.SendControllerMsg(1, valves.ox1gason);
                PressFor(wTime);
                while (bWait)
                    await PutTaskDelay(10);
                Man_Controlcs.SendControllerMsg(1, valves.ox1gasoff);
                Man_Controlcs.WriteStatus("Pressurize", "Finished Pressurizing Oxidizer");

                Man_Controlcs.SendControllerMsg(1, valves.ox2gason);
                PressFor(wTime);
                while (bWait)
                    await PutTaskDelay(10);
                Man_Controlcs.SendControllerMsg(1, valves.ox2gasoff);
                Man_Controlcs.WriteStatus("Pressurize", "Finished Pressurizing Thiol");

                Man_Controlcs.SendControllerMsg(1, valves.capagason);
                PressFor(wTime);
                while (bWait)
                    await PutTaskDelay(10);
                Man_Controlcs.SendControllerMsg(1, valves.capagasoff);
                Man_Controlcs.WriteStatus("Pressurize", "Finished Pressurizing Capping Reagents");

                //activators
                Man_Controlcs.SendControllerMsg(1, valves.act1gason);
                PressFor(bwTime);
                while (bWait)
                    await PutTaskDelay(20);
                Man_Controlcs.SendControllerMsg(1, valves.act1gasoff);
                Man_Controlcs.WriteStatus("Pressurize", "Finished Pressurizing Act 1");

                Man_Controlcs.SendControllerMsg(1, valves.act2gason);
                PressFor(bwTime);
                while (bWait)
                    await PutTaskDelay(20);
                Man_Controlcs.SendControllerMsg(1, valves.act2gasoff);
                Man_Controlcs.WriteStatus("Pressurize", "Finished Pressurizing Act 2");

                this.btnStart.Enabled = true;
                this.exitToolStripMenuItem.Enabled = true;
                this.btnExit.Enabled = true;


            }
            else if (rbaAmidites1.Checked)
            {
                this.btnStart.Enabled = false;
                this.exitToolStripMenuItem.Enabled = false;
                this.btnExit.Enabled = false;

                //top bottles
                Man_Controlcs.SendControllerMsg(1, valves.pressuretop);
                //MessageBox.Show("Pressurize " + valves.pressuretop);
                PressFor(wTime);
                while (bWait)
                    await PutTaskDelay(20);
                Man_Controlcs.SendControllerMsg(1, valves.pressurebot);
                Man_Controlcs.WriteStatus("Pressurize", "Finished Pressurizing Bank 2 Amidites"); this.btnExit.Enabled = true;

                this.exitToolStripMenuItem.Enabled = true;
                this.btnExit.Enabled = true;
                this.btnStart.Enabled = true;
            }

            else if (rbAmidites2.Checked)
            {
                this.btnStart.Enabled = false;
                this.exitToolStripMenuItem.Enabled = false;
                this.btnExit.Enabled = false;

                //bottom bottles
                Man_Controlcs.SendControllerMsg(1, valves.pressurebot);
                PressFor(wTime);
                while (bWait)
                    await PutTaskDelay(20);
                Man_Controlcs.SendControllerMsg(1, valves.pressuretop);
                Man_Controlcs.WriteStatus("Pressurize", "Finished Pressurizing Bank 2 Amidites"); this.btnExit.Enabled = true;

                this.exitToolStripMenuItem.Enabled = true;
                this.btnExit.Enabled = true;
                this.btnStart.Enabled = true;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();          
        }

        private void Menu_Help_Click(object sender, EventArgs e)
        {
            Process.Start(globals.Help_Path);
        }

        private void groupBox1_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(groupBox1, "Pressurize..", "Select from the group the reagent or amidites to be pressurized\nselect All to pressurize both amidties and reagents...");

        }

        private void btnStart_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btnStart, "Start Pressurization..", "Select to start the reagent and amidtie pressurization operation...");

        }

        private void btnExit_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btnExit, "Exit..", "Select to close the pressurize program and return to the Main Menu...");

        }
    }
}
