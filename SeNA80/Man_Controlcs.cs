using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO.Ports;


namespace SeNA80
    {
    public partial class Man_Controlcs : Form
    {
        public static Stopwatch stopwatch = new Stopwatch();
        public static int PumpCntr = 0;
        public bool bWashA = false, bWashB = false, bXtra2 = false, bXtra1 = false, bOx2 = false, bOx = false;
        public bool bCapB = false, bCapA = false, bDeblock = false, bAct1 = false, bAct2 = false, bAm1 = false;
        public bool bAm2 = false, bAm3 = false, bAm4 = false, bAm5 = false, bAm6 = false, bAm7 = false, bAm8 = false;
        public bool bAm9 = false, bAm10 = false, bAm11 = false, bAm12 = false, bAm13 = false, bAm14 = false;
        public bool bCol1 = false, bCol2 = false, bCol3 = false, bCol4 = false, bCol5 = false, bCol6 = false, bCol7 = false, bCol8 = false;
        public bool bW2_1 = false, bW2_2 = false;
        public bool btrainAGrn = false, btrainB1Grn = false, btrainB2Grn = false;
        public bool bRecycling = false;
        public bool bColBypass = true;
        public bool bPumpBypass = true;
        public bool bRecycle = false;
        public bool bWait = false;
        public bool bReagentsPump = true, bAmiditePump = false;
        public static int iAmPVol;

        public Man_Controlcs()
        {
            
            InitializeComponent();

            MakeBypassGreen();

            //disable columns and pump
            btnCol1.Enabled = false;
            btnCol2.Enabled = false;
            btnCol3.Enabled = false;
            btnCol4.Enabled = false;
            btnCol5.Enabled = false;
            btnCol6.Enabled = false;
            btnCol7.Enabled = false;
            btnCol8.Enabled = false;
            DispVol.Enabled = false;
           
            AmDispVol.Enabled = false;
            label102.Enabled = false;
            label103.Enabled = false;
            label109.Enabled = false;
            btnAmPump.Enabled = false;
            btnPump.Enabled = false;

         
            DispVol.Enabled = false;
            label110.Enabled = false;
            label3.Enabled = false;
            label2.Enabled = false;


            //disable the updown
            DispVol.Enabled = false;


            //diable the lines
            label85.Enabled = false;  //(ForeColor = ControlText)
            label89.Enabled = false;
            label86.Enabled = false;
            label91.Enabled = false;
            label92.Enabled = false;
            label94.Enabled = false;
            label97.Enabled = false;
            label98.Enabled = false;
            label93.Enabled = false;

            //diable pump select
            btnPumpSel.Enabled = false;

            
        }
        private bool AllClosed(int CurCol)
        {
            bool retAC = true;

            if (bAm1 && CurCol != 1)
                retAC = false;
            else if (bAm2 && CurCol != 2)
                retAC = false;
            else if (bAm3 && CurCol != 3)
                retAC = false;
            else if (bAm4 && CurCol != 4)
                retAC = false;
            else if (bAm5 && CurCol != 5)
                retAC = false;
            else if (bAm6 && CurCol != 6)
                retAC = false;
            else if (bAm7 && CurCol != 7)
                retAC = false;
            else if (bAm8 && CurCol != 8)
                retAC = false;
            else if (bAm9 && CurCol != 9)
                retAC = false;
            else if (bAm10 && CurCol != 10)
                retAC = false;
            else if (bAm11 && CurCol != 11)
                retAC = false;
            else if (bAm12 && CurCol != 12)
                retAC = false;
            else if (bAm13 && CurCol != 13)
                retAC = false;
            else if (bAct2 && CurCol != 14)
                retAC = false;
            else if (bAct1 && CurCol != 15)
                retAC = false;
            else if (bWashB && CurCol != 16)
                retAC = false;



            return (retAC);

        }
        private bool TrainBClosed(int CurCol)
        {
            bool retAC = true;

            if (bAm9 && CurCol != 9)
                retAC = false;
            else if (bAm10 && CurCol != 10)
                retAC = false;
            else if (bAm11 && CurCol != 11)
                retAC = false;
            else if (bAm12 && CurCol != 12)
                retAC = false;
            else if (bAm13 && CurCol != 13)
                retAC = false;
            else if (bAm14 && CurCol != 13)
                retAC = false;
            else if (bAct2 && CurCol != 14)
                retAC = false;
            else if (bAct1 && CurCol != 15)
                retAC = false;
            else if (bWashB && CurCol != 16)
                retAC = false;

            return (retAC);

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DialogResult.Equals(DialogResult.OK);
            Close();
        }
        private void Man_Controlcs_FormClosing(object sender, FormClosingEventArgs e)
        {
            //make sure all valves are closed
            //big bottles
            if(bWashA || bWashB || bXtra2 || bXtra1 || bOx2 || bOx || bCapB || bCapA || bDeblock)
            {
                if (bWashA)
                    btn_WashA.PerformClick();
                if (bWashB)
                    btn_WashB.PerformClick();
                if (bXtra1)
                    btn_Xtra1.PerformClick();
                if (bXtra2)
                    btn_GasPurge.PerformClick();
                if (bOx2)
                    btn_Ox2.PerformClick();
                if (bOx)
                    btn_Ox1.PerformClick();
                if (bCapA)
                    btn_CapA.PerformClick();
                if (bCapB)
                    btn_CapB.PerformClick();
                if (bDeblock)
                    btn_Debl.PerformClick();
            } 
            //activators
            if(bAct1 || bAct2)
            {
                if (bAct1)
                    btn_Act1.PerformClick();
                if (bAct2)
                    btn_Act2.PerformClick();
            }
            //amidites
            if( bAm1 || bAm2 || bAm3 || bAm4 || bAm5 || bAm6 || bAm7 || bAm8 ||bAm9 || bAm10 || bAm11 || bAm12 || bAm13 || bAm14 )
            {
                if (bAm1)
                    btnAm1.PerformClick();
                if (bAm2)
                    btnAm2.PerformClick();
                if (bAm3)
                    btnAm3.PerformClick();
                if (bAm4)
                    btnAm4.PerformClick();
                if (bAm5)
                    btnAm5.PerformClick();
                if (bAm6)
                    btnAm6.PerformClick();
                if (bAm7)
                    btnAm7.PerformClick();
                if (bAm8)
                    btnAm8.PerformClick();
                if (bAm9)
                    btnAm9.PerformClick();
                if (bAm10)
                    btnAm10.PerformClick();
                if (bAm11)
                    btnAm11.PerformClick();
                if (bAm12)
                    btnAm12.PerformClick();
                if (bAm13)
                    btnAm13.PerformClick();
                if (bAm14)
                    btnAm14.PerformClick();
                }
            //last the columns
            if (bCol1 || bCol2 || bCol3 || bCol4 || bCol5 || bCol6 || bCol7 || bCol8)
                btnColByP.PerformClick();

            //turn auto reccyle valve off
            //string msg = valves.recycleautoon;  //leave it in control of the pump
            //SendControllerMsg(1, msg);

            this.Hide();
            //valves.AllValvesOff();
        }

        private void MakeBypassGreen()
        {
            //just two lines green
            this.label90.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
            this.label95.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
            this.label99.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
            this.label114.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
            this.label85.Image = SeNA80.Properties.Resources.Horiz_Line;
            this.label86.Image = SeNA80.Properties.Resources.Horiz_Line;
            this.label89.Image = SeNA80.Properties.Resources.Vert_Line;
            this.label91.Image = SeNA80.Properties.Resources.Vert_Line;
            this.label92.Image = SeNA80.Properties.Resources.Horiz_Line;
          


            if (bPumpBypass && !bColBypass)
                this.label98.Image = SeNA80.Properties.Resources.Vert_Line;
            else
            {
                /* this.label98.Image = SeNA80.Properties.Resources.Vert_Line_Green;

                 this.label95.Image = SeNA80.Properties.Resources.Horiz_Line;
                 this.label93.Image = SeNA80.Properties.Resources.Vert_Line;
                 this.label97.Image = SeNA80.Properties.Resources.Vert_Line;
                 this.label94.Image = SeNA80.Properties.Resources.Horiz_Line;*/
            }

        }
        private void MakeBypassBlack()
        {
            //just two lines green
            this.label90.Image = SeNA80.Properties.Resources.Horiz_Line;
            this.label95.Image = SeNA80.Properties.Resources.Horiz_Line;
            this.label99.Image = SeNA80.Properties.Resources.Horiz_Line;
            this.label85.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
            this.label86.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
            this.label89.Image = SeNA80.Properties.Resources.Vert_Line_Green;
            this.label91.Image = SeNA80.Properties.Resources.Vert_Line_Green;
            this.label92.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
       
            if (bPumpBypass && bColBypass)
            {
                this.label95.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                this.label98.Image = SeNA80.Properties.Resources.Vert_Line_Green;
                this.label99.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
            }
            else
            {
                this.label98.Image = SeNA80.Properties.Resources.Vert_Line;

                this.label95.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                this.label93.Image = SeNA80.Properties.Resources.Vert_Line_Green;
                this.label97.Image = SeNA80.Properties.Resources.Vert_Line_Green;
                this.label94.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                this.label99.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
            }
        }
        private void TrainB1Green()
        {   //large up
            this.label66.Image = SeNA80.Properties.Resources.Vert_Line_Green;
            this.label67.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
            this.label68.Image = SeNA80.Properties.Resources.Vert_Line_Green;
            this.label47.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;  //to Wash B

            //now the valve block
            this.label33.Image = SeNA80.Properties.Resources.Vert_Line_Green;
            this.label34.Image = SeNA80.Properties.Resources.Vert_Line_Green;
            this.label35.Image = SeNA80.Properties.Resources.Vert_Line_Green;
            this.label36.Image = SeNA80.Properties.Resources.Vert_Line_Green;
            this.label37.Image = SeNA80.Properties.Resources.Vert_Line_Green;
            this.label38.Image = SeNA80.Properties.Resources.Vert_Line_Green;
            this.label39.Image = SeNA80.Properties.Resources.Vert_Line_Green;
            this.label49.Image = SeNA80.Properties.Resources.Vert_Line_Green;

            if (!btrainAGrn)
                this.label88.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;

            btrainB1Grn = true;
        }

        private void TrainB1Black()
        {   //large up
            this.label66.Image = SeNA80.Properties.Resources.Vert_Line;
            this.label67.Image = SeNA80.Properties.Resources.Horiz_Line;
            this.label68.Image = SeNA80.Properties.Resources.Vert_Line;
            this.label47.Image = SeNA80.Properties.Resources.Horiz_Line;

            //now the valve block
            this.label33.Image = SeNA80.Properties.Resources.Vert_Line;
            this.label34.Image = SeNA80.Properties.Resources.Vert_Line;
            this.label35.Image = SeNA80.Properties.Resources.Vert_Line;
            this.label36.Image = SeNA80.Properties.Resources.Vert_Line;
            this.label37.Image = SeNA80.Properties.Resources.Vert_Line;
            this.label38.Image = SeNA80.Properties.Resources.Vert_Line;
            this.label39.Image = SeNA80.Properties.Resources.Vert_Line;
            this.label49.Image = SeNA80.Properties.Resources.Vert_Line;

            if (!btrainAGrn)
                this.label88.Image = SeNA80.Properties.Resources.Horiz_Line;


            btrainB1Grn = false;
        }

        private void TrainB2Green()
        {   //large up
            //large up
            this.label66.Image = SeNA80.Properties.Resources.Vert_Line_Green;
            this.label67.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
            this.label47.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;

            //now the valve block
            this.label40.Image = SeNA80.Properties.Resources.Vert_Line_Green;
            this.label41.Image = SeNA80.Properties.Resources.Vert_Line_Green;
            this.label42.Image = SeNA80.Properties.Resources.Vert_Line_Green;
            this.label43.Image = SeNA80.Properties.Resources.Vert_Line_Green;
            this.label44.Image = SeNA80.Properties.Resources.Vert_Line_Green;
            this.label45.Image = SeNA80.Properties.Resources.Vert_Line_Green;
            this.label46.Image = SeNA80.Properties.Resources.Vert_Line_Green;
            this.label65.Image = SeNA80.Properties.Resources.Vert_Line_Green;
            this.label48.Image = SeNA80.Properties.Resources.Vert_Line_Green;

            btrainB2Grn = true;
        }
        private void TrainB2Black()
        {   //large up
            this.label66.Image = SeNA80.Properties.Resources.Vert_Line;
            this.label67.Image = SeNA80.Properties.Resources.Horiz_Line;
            this.label47.Image = SeNA80.Properties.Resources.Horiz_Line;

            //now the valve block
            this.label40.Image = SeNA80.Properties.Resources.Vert_Line;
            this.label41.Image = SeNA80.Properties.Resources.Vert_Line;
            this.label42.Image = SeNA80.Properties.Resources.Vert_Line;
            this.label43.Image = SeNA80.Properties.Resources.Vert_Line;
            this.label44.Image = SeNA80.Properties.Resources.Vert_Line;
            this.label45.Image = SeNA80.Properties.Resources.Vert_Line;
            this.label46.Image = SeNA80.Properties.Resources.Vert_Line;
            this.label48.Image = SeNA80.Properties.Resources.Vert_Line;
            this.label65.Image = SeNA80.Properties.Resources.Vert_Line;

            btrainB2Grn = false;
        }

        private void btn_WashB_Click_1(object sender, EventArgs e)
        {
            if (!bWashB)  // if it is off turn it on
            {
                //make the train green
                if (!btrainB1Grn)
                    TrainB1Green();
                if (!btrainB2Grn)
                    TrainB2Green();

                //show the individual valves
                cb_W2_1.Visible = true;
                cb_W2_1.Checked = false;

                cb_W2_2.Visible = true;
                cb_W2_2.Checked = false;

                bW2_1 = true;
                bW2_2 = true;

                //open the flow path
                this.label48.Image = SeNA80.Properties.Resources.Vert_Line_Green;
                this.label50.Image = SeNA80.Properties.Resources.Vert_Line_Green;

                //turn the gas on
                String msg = valves.washbgason;
                SendControllerMsg(1, msg);

                Thread.Sleep(500);

                //finally switch both 2 way valves on valve
                SendControllerMsg(1, valves.washb1on);//amidite wash b1 on
                Thread.Sleep(20);
                SendControllerMsg(1, valves.washb2on);//amidite wash b2 on
                Thread.Sleep(20);
                bWashB = true;
            }
            else
            {
                //hide the individual valves
                cb_W2_1.Visible = false;
                cb_W2_1.Checked = true;

                cb_W2_2.Visible = false;
                cb_W2_2.Checked = true;

                //make the train green
                if (AllClosed(16))
                {
                    if (btrainB1Grn)
                        TrainB1Black();
                    if (btrainB2Grn)
                        TrainB2Black();
                }
                if (TrainBClosed(16))
                {
                    if (btrainB2Grn)
                        TrainB2Black();
                }

                this.label67.Image = SeNA80.Properties.Resources.Horiz_Line;
                this.label66.Image = SeNA80.Properties.Resources.Vert_Line;
                this.label88.Image = SeNA80.Properties.Resources.Horiz_Line;


                bW2_1 = false;
                bW2_2 = false;

                //open the flow path
                this.label48.Image = SeNA80.Properties.Resources.Vert_Line;
                this.label50.Image = SeNA80.Properties.Resources.Vert_Line;

                //finally switch the valve
                SendControllerMsg(1, valves.washb1off);//wash 1 off
                SendControllerMsg(1, valves.washb2off);//wash 2 off

                //turn gas off
                Thread.Sleep(100);
                String msg = valves.washbgasoff;
                SendControllerMsg(1, msg);

                bWashB = false;
            }


        }
        
        

        private void btn_GasPurge_Click(object sender, EventArgs e)
        {
            if (!bXtra2)  // if it is off turn it on
            {
                //make the train green
                if (!btrainB1Grn)
                    TrainB1Green();
           
                //open the flow path
                this.label51.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                this.label52.Image = SeNA80.Properties.Resources.Vert_Line_Green;

                //finally switch the valve
                SendControllerMsg(1, valves.gaspurgeon);//amidite 1 on

                bXtra2 = true;
            }
            else
            {
                //make the train green
                if (AllClosed(15))
                {
                    if (btrainB1Grn)
                        TrainB1Black();
                    if (btrainB2Grn)
                        TrainB2Black();
                }

                //open the flow path
                this.label51.Image = SeNA80.Properties.Resources.Horiz_Line;
                this.label52.Image = SeNA80.Properties.Resources.Vert_Line;

                //finally switch the valve
                SendControllerMsg(1, valves.gaspurgeoff);//amidite 1 off
                
                bXtra2 = false;
            }


        }

        private void btn_Act1_Click(object sender, EventArgs e)
        {
            //check A train
            if (bDeblock || bCapA || bCapB || bOx || bOx2 || bXtra1)
            {
                if (MessageBox.Show("You have another bottle open and run the risk of contamination\n\n Would you like to continue???", "Bottle Open", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
                    return;
            }

            if (!bAct1)  // if it is off turn it on
            {
                //make the train green
                this.label11.Image = SeNA80.Properties.Resources.Vert_Line_Green;
                if (!btrainAGrn)
                {
                    if (!bWashA && !bXtra2 && !bXtra1 && !bOx2 && !bOx && !bCapB && !bCapA)
                        MakeTrainAGreen();
                }

                //turn the gas on
                String msg = valves.act1gason;
                SendControllerMsg(1, msg);

                Thread.Sleep(200);

                //finally switch the valve
                SendControllerMsg(1, valves.act1on);//amidite 1 on

                bAct1 = true;
            }
            else
            {
                this.label11.Image = SeNA80.Properties.Resources.Vert_Line;
                if (btrainAGrn)
                {
                    if (!bWashA && !bXtra2 && !bXtra1 && !bOx2 && !bOx && !bCapB && !bCapA)
                        MakeTrainABlk();
                }

                //open the flow path
                this.label51.Image = SeNA80.Properties.Resources.Horiz_Line;
                this.label52.Image = SeNA80.Properties.Resources.Vert_Line;

                //finally switch the valve
                SendControllerMsg(1, valves.act1off);//amidite 1 off
                if (!bAct2)
                {
                    Thread.Sleep(100); //0.5seconds to pressurize

                    //turn gas off
                    String msg = valves.act1gasoff;
                    SendControllerMsg(1, msg);
                }
                bAct1 = false;
            }


        }

        private void btn_Act2_Click(object sender, EventArgs e)
        {
            //check A train
            if (bDeblock || bCapA || bCapB || bOx || bOx2 || bXtra1)
            {
                if (MessageBox.Show("You have another bottle open and run the risk of contamination\n\n Would you like to continue???", "Bottle Open", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
                    return;
            }

            if (!bAct2)  // if it is off turn it on
            {
                //make the train green
                this.lblXtra2.Image = SeNA80.Properties.Resources.Vert_Line_Green;
                if (!btrainAGrn)
                {
                    if (!bWashA && !bXtra2 && !bXtra1 && !bOx2 && !bOx && !bCapB && !bCapA)
                        MakeTrainAGreen();
                }

                
                //turn the gas on
                String msg = valves.act2gason;
                SendControllerMsg(1, msg);

                Thread.Sleep(200);

                //finally switch the valve
                SendControllerMsg(1, valves.act2on);//act 2 on

                bAct2 = true;
            }
            else
            {
                //make the train black
                this.lblXtra2.Image = SeNA80.Properties.Resources.Vert_Line;
                if (btrainAGrn)
                {
                    if (!bWashA && !bXtra2 && !bXtra1 && !bOx2 && !bOx && !bCapB && !bCapA)
                        MakeTrainABlk();
                }

                //finally switch the valve
                SendControllerMsg(1, valves.act2off);//amidite 1 off
                
                if (!bAct1)
                {
                    Thread.Sleep(100); //0.5seconds to pressurize

                    //turn gas off
                    String msg = valves.act2gasoff;
                    SendControllerMsg(1, msg);
                }
                bAct2 = false;
            }
            

        }
        private void btnAm14_Click(object sender, EventArgs e)
        {
            //check Amidite train
            if (bAm2 || bAm3 || bAm4 || bAm5 || bAm6 || bAm7 || bAm8 || bAm9 || bAm10 || bAm11 || bAm12 || bAm13 || bAm1 || bCapB)
            {
                if (MessageBox.Show("You have another amidite open and run the risk of contamination\n\n Would you like to continue???", "Bottle Open", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
                    return;
            }

            if (!bAm14)  // if it is off turn it on
            {
                //make the train green
                if (!btrainB2Grn)
                    TrainB2Green();

                //open the flow path
                this.label55a.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                this.label56a.Image = SeNA80.Properties.Resources.Vert_Line_Green;

                //finally switch the valve
                SendControllerMsg(1, valves.am14on);//amidite 1 on
                Thread.Sleep(20);
                SendControllerMsg(1, valves.pressuretop);//top for amidites 9 - 14 on

                bAm14 = true;
            }
            else
            {
                //make the train green
                if (AllClosed(13))
                {
                    if (btrainB2Grn)
                        TrainB2Black();
                }

                if (TrainBClosed(14))
                {
                    if (btrainB2Grn)
                        TrainB2Black();
                }
                //open the flow path
                this.label55a.Image = SeNA80.Properties.Resources.Horiz_Line;
                this.label56a.Image = SeNA80.Properties.Resources.Vert_Line;

                //finally switch the valve
                SendControllerMsg(1, valves.am14off);//amidite 1 off
                Thread.Sleep(20);
                SendControllerMsg(1, valves.pressurebot);//top for amidites 9 - 14 on

                bAm14 = false;
            }

        }


        private void btnAm13_Click(object sender, EventArgs e)
        {
            //check Amidite train
            if (bAm2 || bAm3 || bAm4 || bAm5 || bAm6 || bAm7 || bAm8 || bAm9 || bAm10 || bAm11 || bAm12 || bAm1 || bAm14 || bCapB)
            {
                if (MessageBox.Show("You have another amidite open and run the risk of contamination\n\n Would you like to continue???", "Bottle Open", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
                    return;
            }

            if (!bAm13)  // if it is off turn it on
            {
                //make the train green
                if (!btrainB2Grn)
                    TrainB2Green();

                //open the flow path
                this.label55.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                this.label56.Image = SeNA80.Properties.Resources.Vert_Line_Green;

                //finally switch the valve
                SendControllerMsg(1, valves.am13on);//amidite 1 on
                Thread.Sleep(20);
                SendControllerMsg(1, valves.pressuretop);//top for amidites 9 - 14 on

                bAm13 = true;
            }
            else
            {
                //make the train green
                if (AllClosed(13))
                {
                    if (btrainB2Grn)
                        TrainB2Black();
                }

                if (TrainBClosed(13))
                {
                    if (btrainB2Grn)
                        TrainB2Black();
                }
                //open the flow path
                this.label55.Image = SeNA80.Properties.Resources.Horiz_Line;
                this.label56.Image = SeNA80.Properties.Resources.Vert_Line;

                //finally switch the valve
                SendControllerMsg(1, valves.am13off);//amidite 1 off
                Thread.Sleep(20);
                SendControllerMsg(1, valves.pressurebot);//top for amidites 9 - 14 on


                bAm13 = false;
            }


        }

        private void btnAm12_Click(object sender, EventArgs e)
        {
            //check Amidite train
            if (bAm2 || bAm3 || bAm4 || bAm5 || bAm6 || bAm7 || bAm8 || bAm9 || bAm10 || bAm11 || bAm1 || bAm13 || bAm14 || bCapB)
            {
                if (MessageBox.Show("You have another amidite open and run the risk of contamination\n\n Would you like to continue???", "Bottle Open", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
                    return;
            }

            if (!bAm12)  // if it is off turn it on
            {
                //make the train green
                if (!btrainB2Grn)
                    TrainB2Green();

                //open the flow path
                this.label58.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                this.label57.Image = SeNA80.Properties.Resources.Vert_Line_Green;

                //finally switch the valve
                SendControllerMsg(1, valves.am12on);//amidite 1 on
                Thread.Sleep(20);
                SendControllerMsg(1, valves.pressuretop);//top for amidites 9 - 14 on

                bAm12 = true;
            }
            else
            {
                //make the train green
                if (AllClosed(12))
                {
                    if (btrainB2Grn)
                        TrainB2Black();
                }

                if (TrainBClosed(12))
                {
                    if (btrainB2Grn)
                        TrainB2Black();
                }
                //open the flow path
                this.label58.Image = SeNA80.Properties.Resources.Horiz_Line;
                this.label57.Image = SeNA80.Properties.Resources.Vert_Line;

                //finally switch the valve
                SendControllerMsg(1, valves.am12off);//amidite 1 off
                Thread.Sleep(20);
                SendControllerMsg(1, valves.pressurebot);//top for amidites 9 - 14 on

                bAm12 = false;
            }


        }

        private void btnAm11_Click(object sender, EventArgs e)
        {
            //check Amidite train
            if (bAm2 || bAm3 || bAm4 || bAm5 || bAm6 || bAm7 || bAm8 || bAm9 || bAm10 || bAm1 || bAm12 || bAm13 || bAm14 || bCapB)
            {
                if (MessageBox.Show("You have another amidite open and run the risk of contamination\n\n Would you like to continue???", "Bottle Open", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
                    return;
            }

            if (!bAm11)  // if it is off turn it on
            {
                //make the train green
                if (!btrainB2Grn)
                    TrainB2Green();

                //open the flow path
                this.label60.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                this.label59.Image = SeNA80.Properties.Resources.Vert_Line_Green;

                //finally switch the valve
                SendControllerMsg(1, valves.am11on);//amidite 1 on
                Thread.Sleep(20);
                SendControllerMsg(1, valves.pressuretop);//top for amidites 9 - 14 on

                bAm11 = true;
            }
            else
            {
                //make the train green
                if (AllClosed(11))
                {
                    if (btrainB2Grn)
                        TrainB2Black();
                }

                if (TrainBClosed(11))
                {
                    if (btrainB2Grn)
                        TrainB2Black();
                }
                //open the flow path
                this.label60.Image = SeNA80.Properties.Resources.Horiz_Line;
                this.label59.Image = SeNA80.Properties.Resources.Vert_Line;

                //finally switch the valve
                SendControllerMsg(1, valves.am11off);//amidite 1 off
                Thread.Sleep(20);
                SendControllerMsg(1, valves.pressurebot);//top for amidites 9 - 14 on

                bAm11 = false;
            }


        }

        private void btnAm10_Click(object sender, EventArgs e)
        {
            //check Amidite train
            if (bAm2 || bAm3 || bAm4 || bAm5 || bAm6 || bAm7 || bAm8 || bAm9 || bAm1 || bAm11 || bAm12 || bAm13 || bAm14 || bCapB)
            {
                if (MessageBox.Show("You have another amidite open and run the risk of contamination\n\n Would you like to continue???", "Bottle Open", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
                    return;
            }

            if (!bAm10)  // if it is off turn it on
            {
                //make the train green
                if (!btrainB2Grn)
                    TrainB2Green();

                //open the flow path
                this.label62.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                this.label61.Image = SeNA80.Properties.Resources.Vert_Line_Green;

                //finally switch the valve
                SendControllerMsg(1, valves.am10on);//amidite 1 on
                Thread.Sleep(20);
                SendControllerMsg(1, valves.pressuretop);//top for amidites 9 - 14 on

                bAm10 = true;
            }
            else
            {
                //make the train green
                if (AllClosed(10))
                {
                    if (btrainB2Grn)
                        TrainB2Black();
                }

                if (TrainBClosed(10))
                {
                    if (btrainB2Grn)
                        TrainB2Black();
                }
                //open the flow path
                this.label62.Image = SeNA80.Properties.Resources.Horiz_Line;
                this.label61.Image = SeNA80.Properties.Resources.Vert_Line;

                //finally switch the valve
                SendControllerMsg(1, valves.am10off);//amidite 1 off
                Thread.Sleep(20);
                SendControllerMsg(1, valves.pressurebot);//top for amidites 9 - 14 on

                bAm10 = false;
            }


        }

        private void btnAm9_Click(object sender, EventArgs e)
        {
            //check Amidite train
            if (bAm2 || bAm3 || bAm4 || bAm5 || bAm6 || bAm7 || bAm8 || bAm1 || bAm10 || bAm11 || bAm12 || bAm13 || bAm14 || bCapB)
            {
                if (MessageBox.Show("You have another amidite open and run the risk of contamination\n\n Would you like to continue???", "Bottle Open", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
                    return;
            }

            if (!bAm9)  // if it is off turn it on
            {
                //make the train green
                if (!btrainB2Grn)
                    TrainB2Green();

                //open the flow path
                this.label64.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                this.label63.Image = SeNA80.Properties.Resources.Vert_Line_Green;

                //finally switch the valve
                SendControllerMsg(1, valves.am9on);//amidite 1 on
                Thread.Sleep(20);
                SendControllerMsg(1, valves.pressuretop);//top for amidites 9 - 14 on

                bAm9 = true;
            }
            else
            {
                //make the train green
                if (AllClosed(9))
                {
                    if (btrainB2Grn)
                        TrainB2Black();
                }

                if (TrainBClosed(9))
                {
                    if (btrainB2Grn)
                        TrainB2Black();
                }

                //open the flow path
                this.label64.Image = SeNA80.Properties.Resources.Horiz_Line;
                this.label63.Image = SeNA80.Properties.Resources.Vert_Line;

                //finally switch the valve
                SendControllerMsg(1, valves.am9off);//amidite 1 off
                Thread.Sleep(20);
                SendControllerMsg(1, valves.pressurebot);//top for amidites 9 - 14 on

                bAm9 = false;
            }

        }

        private void btnAm8_Click(object sender, EventArgs e)
        {
            //check Amidite train
            if (bAm2 || bAm3 || bAm4 || bAm5 || bAm6 || bAm7 || bAm1 || bAm9 || bAm10 || bAm11 || bAm12 || bAm13 || bAm14 || bCapB)
            {
                if (MessageBox.Show("You have another amidite open and run the risk of contamination\n\n Would you like to continue???", "Bottle Open", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
                    return;
            }

            if (!bAm8)  // if it is off turn it on
            {
                //make the train green
                if (!btrainB2Grn)
                    TrainB2Green();

                //open the flow path
                this.label29.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                this.label30.Image = SeNA80.Properties.Resources.Vert_Line_Green;

                //finally switch the valve
                SendControllerMsg(1, valves.am8on);//amidite 1 on
                Thread.Sleep(20);
                SendControllerMsg(1, valves.pressurebot);//bot for amidites 1 - 8 on

                bAm8 = true;
            }
            else
            {
                //make the train green
                if (AllClosed(8))
                {
                    if (btrainB2Grn)
                        TrainB2Black();
                }

                //open the flow path
                this.label29.Image = SeNA80.Properties.Resources.Horiz_Line;
                this.label30.Image = SeNA80.Properties.Resources.Vert_Line;

                //finally switch the valve
                SendControllerMsg(1, valves.am8off);//amidite 1 off
                Thread.Sleep(20);
                SendControllerMsg(1, valves.pressuretop);//bot for amidites 1 - 8 on

                bAm8 = false;
            }

        }

        private void btnAm7_Click(object sender, EventArgs e)
        {
            //check Amidite train
            if (bAm2 || bAm3 || bAm4 || bAm5 || bAm6 || bAm1 || bAm8 || bAm9 || bAm10 || bAm11 || bAm12 || bAm13 || bAm14)
            {
                if (MessageBox.Show("You have another amidite open and run the risk of contamination\n\n Would you like to continue???", "Bottle Open", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
                    return;
            }

            if (!bAm7)  // if it is off turn it on
            {
                //make the train green
                if (!btrainB1Grn)
                    TrainB1Green();

                //open the flow path
                this.label31.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                this.label32.Image = SeNA80.Properties.Resources.Vert_Line_Green;

                //finally switch the valve
                SendControllerMsg(1, valves.am7on);//amidite 1 on
                Thread.Sleep(20);
                SendControllerMsg(1, valves.pressurebot);//bot for amidites 1 - 8 on

                bAm7 = true;
            }
            else
            {
                //make the train green
                if (AllClosed(7))
                {
                    if (btrainB1Grn)
                        TrainB1Black();
                    if (btrainB2Grn)
                        TrainB2Black();
                }

                //open the flow path
                this.label31.Image = SeNA80.Properties.Resources.Horiz_Line;
                this.label32.Image = SeNA80.Properties.Resources.Vert_Line;

                //finally switch the valve
                SendControllerMsg(1, valves.am7off);//amidite 1 off
                Thread.Sleep(20);
                SendControllerMsg(1, valves.pressuretop);//bot for amidites 1 - 8 on

                bAm7 = false;
            }

        }

        private void btnAm6_Click(object sender, EventArgs e)
        {
            //check Amidite train
            if (bAm2 || bAm3 || bAm4 || bAm5 || bAm1 || bAm7 || bAm8 || bAm9 || bAm10 || bAm11 || bAm12 || bAm13 || bAm14)
            {
                if (MessageBox.Show("You have another amidite open and run the risk of contamination\n\n Would you like to continue???", "Bottle Open", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
                    return;
            }

            //check Amidite train
            if (bAm2 || bAm3 || bAm4 || bAm5 || bAm1 || bAm7 || bAm8 || bAm9 || bAm10 || bAm11 || bAm12 || bAm13 || bAm14)
            {
                if (MessageBox.Show("You have another amidite open and run the risk of contamination\n\n Would you like to continue???", "Bottle Open", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
                    return;
            }

            if (!bAm6)  // if it is off turn it on
            {
                //make the train green
                if (!btrainB1Grn)
                    TrainB1Green();

                //open the flow path
                this.label25.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                this.label26.Image = SeNA80.Properties.Resources.Vert_Line_Green;

                //finally switch the valve
                SendControllerMsg(1, valves.am6on);//amidite 1 on
                Thread.Sleep(20);
                SendControllerMsg(1, valves.pressurebot);//bot for amidites 1 - 8 on

                bAm6 = true;
            }
            else
            {
                //make the train green
                if (AllClosed(6))
                {
                    if (btrainB1Grn)
                        TrainB1Black();
                    if (btrainB2Grn)
                        TrainB2Black();
                }

                //open the flow path
                this.label25.Image = SeNA80.Properties.Resources.Horiz_Line;
                this.label26.Image = SeNA80.Properties.Resources.Vert_Line;

                //finally switch the valve
                SendControllerMsg(1, valves.am6off);//amidite 1 off
                Thread.Sleep(20);
                SendControllerMsg(1, valves.pressuretop);//bot for amidites 1 - 8 on

                bAm6 = false;
            }

        }

        private void btnAm5_Click(object sender, EventArgs e)
        {
            //check Amidite train
            if (bAm2 || bAm3 || bAm4 || bAm1 || bAm6 || bAm7 || bAm8 || bAm9 || bAm10 || bAm11 || bAm12 || bAm13 || bAm14)
            {
                if (MessageBox.Show("You have another amidite open and run the risk of contamination\n\n Would you like to continue???", "Bottle Open", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
                    return;
            }

            if (!bAm5)  // if it is off turn it on
            {
                //make the train green
                if (!btrainB1Grn)
                    TrainB1Green();

                //open the flow path
                this.label27.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                this.label28.Image = SeNA80.Properties.Resources.Vert_Line_Green;

                //finally switch the valve
                SendControllerMsg(1, valves.am5on);//amidite 1 on
                Thread.Sleep(20);
                SendControllerMsg(1, valves.pressurebot);//bot for amidites 1 - 8 on

                bAm5 = true;
            }
            else
            {
                //make the train green
                if (AllClosed(5))
                {
                    if (btrainB1Grn)
                        TrainB1Black();
                    if (btrainB2Grn)
                        TrainB2Black();
                }

                //open the flow path
                this.label27.Image = SeNA80.Properties.Resources.Horiz_Line;
                this.label28.Image = SeNA80.Properties.Resources.Vert_Line;

                //finally switch the valve
                SendControllerMsg(1, valves.am5off);//amidite 1 off
                Thread.Sleep(20);
                SendControllerMsg(1, valves.pressuretop);//bot for amidites 1 - 8 on

                bAm5 = false;
            }

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

        private void btnAm4_Click(object sender, EventArgs e)
        {
            //check Amidite train
            if (bAm2 || bAm3 || bAm1 || bAm5 || bAm6 || bAm7 || bAm8 || bAm9 || bAm10 || bAm11 || bAm12 || bAm13 || bAm14)
            {
                if (MessageBox.Show("You have another amidite open and run the risk of contamination\n\n Would you like to continue???", "Bottle Open", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
                    return;
            }

            if (!bAm4)  // if it is off turn it on
            {
                //make the train green
                if (!btrainB1Grn)
                    TrainB1Green();


                //open the flow path
                this.label21.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                this.label22.Image = SeNA80.Properties.Resources.Vert_Line_Green;

                //finally switch the valve
                SendControllerMsg(1, valves.am4on);//amidite 1 on
                Thread.Sleep(20);
                SendControllerMsg(1, valves.pressurebot);//bot for amidites 1 - 8 on

                bAm4 = true;
            }
            else
            {
                //make the train green
                if (AllClosed(4))
                {
                    if (btrainB1Grn)
                        TrainB1Black();
                    if (btrainB2Grn)
                        TrainB2Black();
                }
                //open the flow path
                this.label21.Image = SeNA80.Properties.Resources.Horiz_Line;
                this.label22.Image = SeNA80.Properties.Resources.Vert_Line;

                //finally switch the valve
                SendControllerMsg(1, valves.am4off);//amidite 1 off
                Thread.Sleep(20);
                SendControllerMsg(1, valves.pressuretop);//bot for amidites 1 - 8 on

                bAm4 = false;
            }
        }

        private void btn_RecWaste_Click(object sender, EventArgs e)
        {
            //if recycle button is pushed first take control from the firmware, but give it back when done
            if (bRecycle)  //recycle path selected
            {
    
                label107.Image = SeNA80.Properties.Resources.Horiz_Line;
                label108.Image = SeNA80.Properties.Resources.Horiz_Line;
                label99.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                label114.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                AmDispVol.Visible = true;
                label102.Visible = true;
                label103.Visible = true;
                label111.Visible = false;
                label112.Visible = false;
                label113.Visible = false;
                label5.Visible = false;
                btn_Recycle.Visible = false;

                label106.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                label105.Image = SeNA80.Properties.Resources.Vert_Line_Green;

                bRecycle = false;

                //Send to recycle
                //SendControllerMsg(1, valves.recycleoff);//recycle off
                Thread.Sleep(30);

                //initialize Amidite Pump - make sure it is at 0mL for next recycle
                SendControllerMsg(1, "Q,0\n");
                iAmPVol = 0;


            }
            else //to waste path selected
            {
                //next make sure a reagent is NOT selected
                if (bWashA || bXtra1 || bXtra2 || bOx || bOx2 || bCapA || bCapB|| bDeblock)
                {
                    MessageBox.Show("Can only recycle amidites", "Delselect Reagent");
                    return;
                }

                    //can only recycle amidtes and must have one selected
                if (bAm1 || bAm2 || bAm3 || bAm4 || bAm5 || bAm6 || bAm7 || bAm8 || bAm9 || bAm10 || bAm11 || bAm12 || bAm13 || bAm14 || bWashB || bAct1 || bAct2)
                {
                  
                    label107.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                    label108.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                    label99.Image = SeNA80.Properties.Resources.Horiz_Line;
                    label114.Image = SeNA80.Properties.Resources.Horiz_Line;
                    AmDispVol.Visible = false;
                    label102.Visible = false;
                    label103.Visible = false;
                    label111.Visible = true;
                    label112.Visible = true;
                    label113.Visible = true;
                    label5.Visible = true;
                    btn_Recycle.Visible = true;

                    label106.Image = SeNA80.Properties.Resources.Horiz_Line;
                    label105.Image = SeNA80.Properties.Resources.Vert_Line;

                    bRecycle = true;

                    //Send to recycle
                    //SendControllerMsg(1, valves.recycleon);//recycle on
                }
                else
                    MessageBox.Show("Can Only Recycle Amidites", "Select Amidite");

            }
        }

        private void btnAm3_Click(object sender, EventArgs e)
        {
            //check Amidite train
            if (bAm2 || bAm1 || bAm4 || bAm5 || bAm6 || bAm7 || bAm8 || bAm9 || bAm10 || bAm11 || bAm12 || bAm13 || bAm14)
            {
                if (MessageBox.Show("You have another amidite open and run the risk of contamination\n\n Would you like to continue???", "Bottle Open", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
                    return;
            }

            if (!bAm3)  // if it is off turn it on
            {
                //make the train green
                if (!btrainB1Grn)
                    TrainB1Green();

                //open the flow path
                this.label23.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                this.label24.Image = SeNA80.Properties.Resources.Vert_Line_Green;

                //finally switch the valve
                SendControllerMsg(1, valves.am3on);//amidite 1 on
                Thread.Sleep(20);
                SendControllerMsg(1, valves.pressurebot);//bot for amidites 1 - 8 on

                bAm3 = true;
            }
            else
            {
                //make the train green
                if (AllClosed(3))
                {
                    if (btrainB1Grn)
                        TrainB1Black();
                    if (btrainB2Grn)
                        TrainB2Black();
                }

                //open the flow path
                this.label23.Image = SeNA80.Properties.Resources.Horiz_Line;
                this.label24.Image = SeNA80.Properties.Resources.Vert_Line;

                //finally switch the valve
                SendControllerMsg(1, valves.am3off);//amidite 1 off
                Thread.Sleep(20);
                SendControllerMsg(1, valves.pressuretop);//bot for amidites 1 - 8 on

                bAm3 = false;
            }


        }

        private void btnAm2_Click(object sender, EventArgs e)
        {
            //check Amidite train
            if (bAm1 || bAm3 || bAm4 || bAm5 || bAm6 || bAm7 || bAm8 || bAm9 || bAm10 || bAm11 || bAm12 || bAm13 || bAm14)
            {
                if (MessageBox.Show("You have another amidite open and run the risk of contamination\n\n Would you like to continue???", "Bottle Open", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
                    return;
            }

            if (!bAm2)  // if it is off turn it on
            {
                //make the train green
                if (!btrainB1Grn)
                    TrainB1Green();

                //open the flow path
                this.label19.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                this.label20.Image = SeNA80.Properties.Resources.Vert_Line_Green;

                //finally switch the valve
                SendControllerMsg(1, valves.am2on);//amidite 1 on
                Thread.Sleep(20);
                SendControllerMsg(1, valves.pressurebot);//bot for amidites 1 - 8 on

                bAm2 = true;
            }
            else
            {
                //make the train green
                if (AllClosed(2))
                {
                    if (btrainB1Grn)
                        TrainB1Black();
                    if (btrainB2Grn)
                        TrainB2Black();
                }

                //open the flow path
                this.label19.Image = SeNA80.Properties.Resources.Horiz_Line;
                this.label20.Image = SeNA80.Properties.Resources.Vert_Line;

                //finally switch the valve
                SendControllerMsg(1, valves.am2off);//amidite 1 off
                Thread.Sleep(20);
                SendControllerMsg(1, valves.pressuretop);//bot for amidites 1 - 8 on

                bAm2 = false;
            }

        }

        private void Menu_Trityl_Click(object sender, EventArgs e)
        {
            //check to make sure a monitor is configured and on
           
            if (globals.bUVTrityl)
            {
                if (!globals.bUV1On && !globals.bUV2On && !globals.bUV3On && !globals.bUV4On &&
                !globals.bUV5On && !globals.bUV6On && !globals.bUV7On && !globals.bUV8On)
                {
                    MessageBox.Show("You Must Configure a Monitor\n\nOr turn on a cell", "No Monitor");
                    return;
                }
            }

            if (WindowAlreadyOpen.WindowOpen("UV Strip Chart"))
                return;

            if (globals.sc != null)
                globals.sc = new stripchartcs();

            globals.sc.Show();


        }

        private void Menu_Cond_Click(object sender, EventArgs e)
        {
            //put the conductivity strip chart in here...
        }

        private void Menu_UVCellsOn_Click(object sender, EventArgs e)
        {
         
            Sensor_Config sconf = new Sensor_Config();

            if (sconf.ShowDialog() == DialogResult.OK)
            {
                return;
                // whatever you need to do with result
            }

        
    }

        private void Menu_Help_Click(object sender, EventArgs e)
        {
            Process.Start(globals.Help_Path);
        }
               


        private void btnAm1_Click(object sender, EventArgs e)
        {
            //check Amidite train
            if (bAm2 || bAm3 || bAm4 || bAm5 || bAm6 || bAm7 || bAm8 || bAm9 || bAm10 || bAm11 || bAm12 || bAm13 || bAm14)
            {
                if (MessageBox.Show("You have another amidite open and run the risk of contamination\n\n Would you like to continue???", "Bottle Open", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
                    return;
            }
            if (!bAm1)  // if it is off turn it on
            {
                //make the train green
                if (!btrainB1Grn)
                    TrainB1Green();

                //open the flow path
                this.label18.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                this.Am1V.Image = SeNA80.Properties.Resources.Vert_Line_Green;

                //finally switch the valve
                SendControllerMsg(1, valves.am1on);//amidite 1 on
                Thread.Sleep(20);
                SendControllerMsg(1, valves.pressurebot);//bot for amidites 1 - 8 on

                bAm1 = true;
            }
            else
            {
                //make the train green
                if (AllClosed(1))
                    TrainB1Black();

                //open the flow path
                this.label18.Image = SeNA80.Properties.Resources.Horiz_Line;
                this.Am1V.Image = SeNA80.Properties.Resources.Vert_Line;

                //finally switch the valve
                SendControllerMsg(1, valves.am1off);//amidite 1 off
                Thread.Sleep(20);
                SendControllerMsg(1, valves.pressuretop);//bot for amidites 1 - 8 on

                bAm1 = false;
            }

        }


        private void btnColByP_Click(object sender, EventArgs e)
        {
            if (!bPumpBypass)
                btnBypPump.PerformClick();

            if (!bColBypass)
            {
                    //check if recycle on..if is turn it off and stop recycle
                if (bRecycle)
                {

                    label107.Image = SeNA80.Properties.Resources.Horiz_Line;
                    label108.Image = SeNA80.Properties.Resources.Horiz_Line;
                    label99.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                    label114.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                    AmDispVol.Visible = true;
                    label102.Visible = true;
                    label103.Visible = true;
                    label111.Visible = false;
                    label112.Visible = false;
                    label113.Visible = false;
                    label5.Visible = false;
                    btn_Recycle.Visible = false;

                    label106.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                    label105.Image = SeNA80.Properties.Resources.Vert_Line_Green;

                    bRecycle = false;

                    //Send to recycle
                    //SendControllerMsg(1, valves.recycleoff);//recycle on
                    Thread.Sleep(30);

                }

                label90.Enabled = true;
                MakeBypassGreen();
                bColBypass = true;
               
                //disable columns and pump
                if (bCol1)
                    btnCol1.PerformClick();
                if (bCol2)
                    btnCol2.PerformClick();
                if (bCol3)
                    btnCol3.PerformClick();
                if (bCol4)
                    btnCol4.PerformClick();
                if (bCol5)
                    btnCol5.PerformClick();
                if (bCol6)
                    btnCol6.PerformClick();
                if (bCol7)
                    btnCol7.PerformClick();
                if (bCol8)
                    btnCol8.PerformClick();
                btnCol1.Enabled = false;
                btnCol2.Enabled = false;
                btnCol3.Enabled = false;
                btnCol4.Enabled = false;
                btnCol5.Enabled = false;
                btnCol6.Enabled = false;
                btnCol7.Enabled = false;
                btnCol8.Enabled = false;
                btnAmPump.Enabled = false;

                //disable the updown
                DispVol.Enabled = false;

                //diable the lines
                label85.ForeColor = System.Drawing.Color.Black;
                label86.ForeColor = System.Drawing.Color.Black;
                label85.Enabled = false;
                label86.Enabled = false;
                label89.Enabled = false;
                label86.Enabled = false;
                label91.Enabled = false;
                label92.Enabled = false;
                label94.Enabled = false;
                label97.Enabled = false;
                label98.Enabled = false;
                label93.Enabled = false;

               

                //finally switch the valve
                SendControllerMsg(1, valves.tocolbypass);
            }
            else
            {
                MakeBypassBlack();
                bColBypass = false;
                label90.Enabled = false;

                //enable columns and pump
                btnCol1.Enabled = true;
                btnCol2.Enabled = true;
                btnCol3.Enabled = true;
                btnCol4.Enabled = true;
                btnCol5.Enabled = true;
                btnCol6.Enabled = true;
                btnCol7.Enabled = true;
                btnCol8.Enabled = true;

                //enable the lines
                label85.Enabled = true;
                label86.Enabled = true;
                label85.ForeColor = System.Drawing.Color.Green;
                label86.ForeColor = System.Drawing.Color.Green;
                label89.Enabled = true;
                label86.Enabled = true;
                label91.Enabled = true;
                label92.Enabled = true;
                label94.Enabled = true;
                label97.Enabled = true;
                label98.Enabled = true;
                label93.Enabled = true;

                //finally switch the valve
                SendControllerMsg(1, valves.tocol);

            }

        }

        private void btnByPum_Click(object sender, EventArgs e)
        {

        }

        private void cb_W2_1_Paint(object sender, PaintEventArgs e)
        {
            Point pt = new Point(e.ClipRectangle.Left + 1);
            Rectangle rect = new Rectangle(pt, new Size(17, 17));
            if (cb_W2_1.Checked)
            {
                using (Font wing = new Font("Arial Black", 11f))
                    e.Graphics.DrawString("X", wing, Brushes.Red, rect);
            }
            else
            {
                SolidBrush brush = new SolidBrush(System.Drawing.Color.Green);
                e.Graphics.FillRectangle(brush, rect);
            }
            e.Graphics.DrawRectangle(Pens.Black, rect);
        }

        private void cb_W2_2_Paint(object sender, PaintEventArgs e)
        {
            Point pt = new Point(e.ClipRectangle.Left + 1);
            Rectangle rect = new Rectangle(pt, new Size(17, 17));
            if (cb_W2_2.Checked)
            {
                using (Font wing = new Font("Arial Black", 11f))
                    e.Graphics.DrawString("X", wing, Brushes.Red, rect);
            }
            else
            {
                SolidBrush brush = new SolidBrush(System.Drawing.Color.Green);
                e.Graphics.FillRectangle(brush, rect);
            }
            e.Graphics.DrawRectangle(Pens.Black, rect);
        }
        private void cb_W2_1_CheckedChanged(object sender, EventArgs e)
        {
           if(cb_W2_1.Checked)  //valve is closed open it up
            {
                //turn the train green
                if (btrainB1Grn)
                    TrainB1Black();

                this.label67.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                this.label66.Image = SeNA80.Properties.Resources.Vert_Line_Green;
                this.label88.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;

                //open the valve
                SendControllerMsg(1, valves.washb1off);//amidite wash b1 off
                Thread.Sleep(20);

                bW2_1 = false;

                if (!bW2_2)  //if they are both off, then close Wash B
                    btn_WashB.PerformClick();
            }
            else
            {
                //turn the train green
                if (!btrainB1Grn)
                    TrainB1Green();


                //close the valve
                SendControllerMsg(1, valves.washb1on);//amidite wash b1 on
                Thread.Sleep(20);
                bW2_1 = true;
              }
        }

        private void cb_W2_2_CheckedChanged(object sender, EventArgs e)
        {
         if (cb_W2_2.Checked)  //valve is closed open it up
            {
                //turn the train green
                if (btrainB2Grn)
                    TrainB2Black();

                this.label67.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                this.label66.Image = SeNA80.Properties.Resources.Vert_Line_Green;
                this.label88.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;

                //open the valve
                SendControllerMsg(1, valves.washb2off);//amidite wash b1 off
                Thread.Sleep(20);

                bW2_2 = false;

                if (!bW2_1)  //if they are both off, then close Wash B
                    btn_WashB.PerformClick();
            }
            else
            {
                //turn the train green
                if (!btrainB2Grn)
                    TrainB2Green();

                //close the valve
                SendControllerMsg(1, valves.washb2on);//amidite wash b1 on
                Thread.Sleep(20);
                bW2_2 = true;
            }
        }

        private void btnCol1_Click(object sender, EventArgs e)
        {
            //
            if (!bCol1) //turn it on
            {
                //turn the lines green
                this.label69.Image = SeNA80.Properties.Resources.Vert_Line_Green;
                this.label84.Image = SeNA80.Properties.Resources.Vert_Line_Green;

                SendControllerMsg(1, valves.col1on);
                bCol1 = true;
            }
            else //turn it off
            {
                //turn the lines blacl
                this.label69.Image = SeNA80.Properties.Resources.Vert_Line;
                this.label84.Image = SeNA80.Properties.Resources.Vert_Line;

                SendControllerMsg(1, valves.col1off);
                bCol1 = false;
            }
        }
        private void btnCol2_Click(object sender, EventArgs e)
        {
            //
            if (!bCol2) //turn it on
            {
                //turn the lines green
                this.label70.Image = SeNA80.Properties.Resources.Vert_Line_Green;
                this.label83.Image = SeNA80.Properties.Resources.Vert_Line_Green;

                SendControllerMsg(1, valves.col2on);
                bCol2 = true;
            }
            else //turn it off
            {
                //turn the lines blacl
                this.label70.Image = SeNA80.Properties.Resources.Vert_Line;
                this.label83.Image = SeNA80.Properties.Resources.Vert_Line;

                SendControllerMsg(1, valves.col2off);
                bCol2 = false;
            }

        }

        private void btnCol3_Click(object sender, EventArgs e)
        {
            if (!bCol3) //turn it on
            {
                //turn the lines green
                this.label71.Image = SeNA80.Properties.Resources.Vert_Line_Green;
                this.label82.Image = SeNA80.Properties.Resources.Vert_Line_Green;

                SendControllerMsg(1, valves.col3on);
                bCol3 = true;
            }
            else //turn it off
            {
                //turn the lines blacl
                this.label71.Image = SeNA80.Properties.Resources.Vert_Line;
                this.label82.Image = SeNA80.Properties.Resources.Vert_Line;

                SendControllerMsg(1, valves.col3off);
                bCol3 = false;
            }
        }

        private void btnCol4_Click(object sender, EventArgs e)
        {
            if (!bCol4) //turn it on
            {
                //turn the lines green
                this.label72.Image = SeNA80.Properties.Resources.Vert_Line_Green;
                this.label81.Image = SeNA80.Properties.Resources.Vert_Line_Green;

                SendControllerMsg(1, valves.col4on);
                bCol4 = true;
            }
            else //turn it off
            {
                //turn the lines blacl
                this.label72.Image = SeNA80.Properties.Resources.Vert_Line;
                this.label81.Image = SeNA80.Properties.Resources.Vert_Line;

                SendControllerMsg(1, valves.col4off);
                bCol4 = false;
            }
        }

        private void btnCol5_Click(object sender, EventArgs e)
        {
            if (!bCol5) //turn it on
            {
                //turn the lines green
                this.label73.Image = SeNA80.Properties.Resources.Vert_Line_Green;
                this.label80.Image = SeNA80.Properties.Resources.Vert_Line_Green;

                SendControllerMsg(1, valves.col5on);
                bCol5 = true;
            }
            else //turn it off
            {
                //turn the lines blacl
                this.label73.Image = SeNA80.Properties.Resources.Vert_Line;
                this.label80.Image = SeNA80.Properties.Resources.Vert_Line;

                SendControllerMsg(1, valves.col5off);
                bCol5 = false;
            }
        }

        private void btnCol6_Click(object sender, EventArgs e)
        {
            if (!bCol6) //turn it on
            {
                //turn the lines green
                this.label74.Image = SeNA80.Properties.Resources.Vert_Line_Green;
                this.label79.Image = SeNA80.Properties.Resources.Vert_Line_Green;

                SendControllerMsg(1, valves.col6on);
                bCol6 = true;
            }
            else //turn it off
            {
                //turn the lines blacl
                this.label74.Image = SeNA80.Properties.Resources.Vert_Line;
                this.label79.Image = SeNA80.Properties.Resources.Vert_Line;

                SendControllerMsg(1, valves.col6off);
                bCol6 = false;
            }
        }

        private void btnCol7_Click(object sender, EventArgs e)
        {
            if (!bCol7) //turn it on
            {
                //turn the lines green
                this.label75.Image = SeNA80.Properties.Resources.Vert_Line_Green;
                this.label78.Image = SeNA80.Properties.Resources.Vert_Line_Green;

                SendControllerMsg(1, valves.col7on);
                bCol7 = true;
            }
            else //turn it off
            {
                //turn the lines blacl
                this.label75.Image = SeNA80.Properties.Resources.Vert_Line;
                this.label78.Image = SeNA80.Properties.Resources.Vert_Line;

                SendControllerMsg(1, valves.col7off);
                bCol7 = false;
            }
        }

        private void btnCol8_Click(object sender, EventArgs e)
        {
            if (!bCol8) //turn it on
            {
                //turn the lines green
                this.label76.Image = SeNA80.Properties.Resources.Vert_Line_Green;
                this.label77.Image = SeNA80.Properties.Resources.Vert_Line_Green;

                SendControllerMsg(1, valves.col8on);
                bCol8 = true;
            }
            else //turn it off
            {
                //turn the lines blacl
                this.label76.Image = SeNA80.Properties.Resources.Vert_Line;
                this.label77.Image = SeNA80.Properties.Resources.Vert_Line;

                SendControllerMsg(1, valves.col8off);
                bCol8 = false;
            }
        }

        private async void btnPump_Click(object sender, EventArgs e)
        {
            //get volume from up down control
            int sVol = Convert.ToInt32(DispVol.Value);  //6 steps per uL
            int sCVol = Convert.ToInt32(sVol * globals.dReagentCF);

            if (sVol > 0 && !globals.bDemoMode)
            {
                String msg = "P," + sCVol.ToString() + "\n";
                //MessageBox.Show(msg);
                //send the pump command
                globals.bPumping = true;
                SendControllerMsg(1, msg);
                while (globals.bPumping)
                {
                    await PumpHangOut();
                }

                int dwelltime = sCVol * globals.iReagPumpDwell;
                bool bdwelling = true;

                while (bdwelling)
                {
                    await PutTaskDelay(500);

                    dwelltime = dwelltime - 500;
                    if (dwelltime < 100)
                        bdwelling = false;
                }

            }
            else
            {
                if (!globals.bDemoMode)
                    MessageBox.Show("Must Set a Volume", "Volume not chosen");
                else
                    WriteStatus("Pump", "Pumped " + sCVol.ToString("0"));
            }
            }
        private async void btn_InitR_Click(object sender, EventArgs e)
        {
            String msg = "P,0\n";
            //MessageBox.Show(msg);
            //send the pump command\
            globals.bPumping = true;
            SendControllerMsg(1, msg);
            while (globals.bPumping)
            {
                await PumpHangOut();
            }
        }

        /// <summary>
        /// Pump Wait event handler and async task...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MC_PumpingHandler(object sender, SerialDataReceivedEventArgs e)
        {

            string indata = Main_Form.Main_Arduino.ReadExisting();
            if (indata.Contains("Pumping"))
            {
                //Man_Controlcs.WriteStatus("Main", indata);
                globals.bPumping = true;
            }

            if (indata.Contains("Done"))
            {
                Man_Controlcs.WriteStatus("Main", "Pumping Stopped");
                Man_Controlcs.SyncWait(60);
                globals.bPumping = false;
            }
        }
        async Task PumpHangOut()
        {
            globals.bPumping = true;
            
            //create a "bailout" stopwatch
            stopwatch.Reset();
            stopwatch.Start();
            PumpCntr = 0;

            Man_Controlcs.WriteStatus("Main", "Pumping Started");
            Man_Controlcs.SyncWait(80);

            //very small amounts done quickly
            string done = Main_Form.Main_Arduino.ReadExisting();

            if (!done.Contains("Done"))
            {
                //now just hang out asynchronously until pumping done
                while (globals.bPumping )
                {

                    await Man_Controlcs.AsyncWait(200);
                    if (Main_Form.Main_Arduino.IsOpen)
                    {
                        done = Main_Form.Main_Arduino.ReadExisting();

                        if (done.Contains("Done"))
                        {
                            Man_Controlcs.WriteStatus("Main", "Pumping Finished..");
                            Debug.WriteLine("Done is - " + done);
                            globals.bPumping = false;
                        }
                    }

                    //bail out if wait more than 3 seconds
                   if (stopwatch.ElapsedMilliseconds > 3000)
                    {
                        stopwatch.Stop();
                        Man_Controlcs.WriteStatus("Main", "Did not get pump Don from controller...");
                        bWait = false;
                        globals.bPumping = false;
                    }
                }
            }
            else
                globals.bPumping = false;

            Debug.WriteLine("Done Pumping");

            Debug.WriteLine("Pumping time is - " + stopwatch.ElapsedMilliseconds.ToString("0"));
            bWait = false;
            globals.bPumping = false;
            stopwatch.Stop();
        
        }

        private async void btnAMPump_Click(object sender, EventArgs e)
        {
            //get volume from up down control
            int sVol = Convert.ToInt32(AmDispVol.Value);  //6 steps per uL
            int sCVol = Convert.ToInt32(sVol * globals.dAmiditeCF);

            if (sVol > 0 && !globals.bDemoMode)
            {
                btnAmPump.Cursor = Cursors.WaitCursor;
                btnAmPump.Enabled = false;

                iAmPVol = iAmPVol + sCVol;
                String msg = "Q," + sCVol.ToString() + "\n";
                //MessageBox.Show(msg);
                //send the pump command
                globals.bPumping = true;
                SendControllerMsg(1, msg);
                while (globals.bPumping)
                {
                    await PumpHangOut();
                }
                

                //now dwell to fill the pump with the valves open
                int dwelltime = sCVol * globals.iAmPumpDwell;
                //MessageBox.Show("I am here about to start dwell..."+dwelltime.ToString("0"));

                bool bdwelling = true;
                WriteStatus("Pump", "Starting Dwell Time, will dwell for "+dwelltime.ToString("0")+"ms for "+sCVol.ToString());
                
                while (bdwelling)
                {
                    await PutTaskDelay(500);
                    
                    dwelltime = dwelltime - 500;
                    if (dwelltime < 100)
                        bdwelling = false;
                }
                btnAmPump.Enabled = true;
                btnAmPump.Cursor = Cursors.Default;
                WriteStatus("Pump", "Ending Dwell Time...");

            }
            else
            {
                if (!globals.bDemoMode)
                    MessageBox.Show("Must Set a Volume", "Volume not chosen");
                else
                    WriteStatus("Pump", "Pumped " + sCVol.ToString("0"));
            }


        }
        private async void btn_InitA_Click(object sender, EventArgs e)
        {
            String msg = "P,0\n";
            //MessageBox.Show(msg);
            //send the pump command
            globals.bPumping = true;
            SendControllerMsg(1, msg);
            while(globals.bPumping)
            {
                await PumpHangOut();
            }
        }

        public async void RecycleFor(int howlong)
        {
            
            //MessageBox.Show("Waiting " + howlong);
           
            //set the interval based on the length of the wait
            if (howlong < 30)
                globals.runform.RunTimer.Interval = 1000;    // every 1 second
            else if (howlong > 31 && howlong < 90)
                globals.runform.RunTimer.Interval = 10000;  //every 10 seconds
            else if (howlong > 91 && howlong < 600)
                globals.runform.RunTimer.Interval = 30000;  //every 30 seconds
            else
                globals.runform.RunTimer.Interval = 60000;  //every 60 seconds
            bWait = true;                                     // this is the easiest, just sit on a wait
            globals.runform.RunTimer.Start();
            globals.start_time = DateTime.Now;
            int wTime = howlong * 1000;

            await PutTaskDelay(wTime);

            globals.runform.RunTimer.Stop();
            bWait = false;
        }
        async Task PutTaskDelay(int secn)
        {
            await Task.Delay(secn);
        }

        private async void btn_Recycle_Click(object sender, EventArgs e)
        {
           
            //start the recycling
            if (!bRecycling)  
            {
                //validate that it is O.K. to start recycling
                if (bAm1 || bAm2 || bAm3 || bAm4 || bAm5 || bAm6 || bAm7 || bAm8 || bAm9 || bAm10 || bAm11 || bAm12 || bAm13 || bAm14 || bWashB || bAct2 || bAct1)
                {
                    if (iAmPVol < 10)
                    {
                        MessageBox.Show("You must have at least 10uL of fluid drawn into the pump for recycling", "Need Fluid");
                        return;
                    }
                    if (bCol1 || bCol2 || bCol3 || bCol4 || bCol5 || bCol6 || bCol7 || bCol8)
                    {
                        //turn on recycle valve control
                        //SendControllerMsg(1,valves.recycleautooff);
                        await Man_Controlcs.AsyncWait(40);

                        //make sure the recycle valve is clsoed
                        //SendControllerMsg(1, valves.recycleon);
                        await Man_Controlcs.AsyncWait(40);

                        //gray the buttons so you can only stop by pressing stop
                        btn_RecWaste.Enabled = false;
                        btnPumpSel.Enabled = false;
                        btnBypPump.Enabled = false;
                        btnColByP.Enabled = false;

                        btn_Recycle.Text = "Sto&p";
                        bRecycling = true;

                        string fillto = "Q," + iAmPVol.ToString("0") + "\n";
                        int wait = 6;

                        while (bRecycling)
                        {
                            //empty the pump
                            SendControllerMsg(1, "Q,0\n");

                            RecycleFor(wait); //wait for 6 seconds
                            globals.bPumping = true;
                            while (globals.bPumping)
                            {
                                await PumpHangOut();
                            }

                            //fill the pump
                            SendControllerMsg(1, fillto);
                            RecycleFor(wait); //wait for 6 seconds
                            globals.bPumping = true;
                            while (globals.bPumping)
                            {
                                await PumpHangOut();
                            }

                            int dwelltime = iAmPVol * globals.iAmPumpDwell; 
                            bool bdwelling = true;
                            btnAmPump.Cursor = Cursors.WaitCursor;
                            WriteStatus("Pump", "Starting Dwell Time, will dwell for " + dwelltime.ToString("0") + "ms");
                            btnAmPump.Enabled = false;
                            while (bdwelling)
                            {
                                await PutTaskDelay(500);

                                dwelltime = dwelltime - 500;
                                if (dwelltime < 100)
                                    bdwelling = false;
                            }
                            btnAmPump.Enabled = true;
                            btnAmPump.Cursor = Cursors.Default;
                            WriteStatus("Pump", "Ending Dwell Time...");



                        }
                    }
                    else
                    {
                        MessageBox.Show("You Must have at lease one column selected to recycle through", "Select Column");
                        return;
                    }
                }
                else
                    MessageBox.Show("You Must Select an Amidite to recyle", "Recyle");

            }
            else //stop it
            {
                //gray the buttons so you can only stop by pressing stop
                //turn on recycle valve control
                //SendControllerMsg(1, valves.recycleautoon);
                await PutTaskDelay(50);

                btn_RecWaste.Enabled = true;
                btnPumpSel.Enabled = true;
                btnBypPump.Enabled = true;
                btnColByP.Enabled = true;

                btn_Recycle.Text = "&Start";
                await PutTaskDelay(100);
                bRecycling = false;
            }
        }

        private void btnBypPump_Click(object sender, EventArgs e)
        {
            
            if (!bColBypass)
            {
                if (bPumpBypass)
                {
                    btnPumpSel.Enabled = true;

                    //enable the pump
                    btnPump.Enabled = true;
                    btn_InitR.Enabled = true;
                    DispVol.Enabled = true;
                    label103.Enabled = true;
                    label109.Enabled = true;
                    label102.Enabled = true;
                    label2.Enabled = true;

                    bPumpBypass = false;

                    //open the flow path
                    this.label97.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                    //this.label99.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                    //this.label93.Image = SeNA80.Properties.Resources.Vert_Line_Green;
                    this.label94.Image = SeNA80.Properties.Resources.Vert_Line_Green;
                    this.label104.Image = SeNA80.Properties.Resources.Vert_Line_Green;
                    this.label99.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;

                    this.label95.Image = SeNA80.Properties.Resources.Horiz_Line;
                    this.label98.Image = SeNA80.Properties.Resources.Vert_Line;
                    this.label114.Image = SeNA80.Properties.Resources.Horiz_Line; 
                    //finally switch the valve
                    SendControllerMsg(1, "V,P,1\n");//n is t pump
                }
                else
                {
                    //disable the pump
                    btnPump.Enabled = false;
                    DispVol.Enabled = false;
                    btn_InitR.Enabled = false;
                    label109.Enabled = false;
                    label102.Enabled = false;
                    label110.Enabled = false;
                    //btn_RecWaste.Enabled = false;
                    
                     //btnPumpSel.Enabled = false;

                    bPumpBypass = true;

                    //close the flow path
                    this.label97.Image = SeNA80.Properties.Resources.Horiz_Line;
                    this.label99.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                    this.label114.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                    this.label93.Image = SeNA80.Properties.Resources.Vert_Line;
                    this.label94.Image = SeNA80.Properties.Resources.Vert_Line;
                    this.label17.Image = SeNA80.Properties.Resources.Vert_Line;
                    this.label101.Image = SeNA80.Properties.Resources.Horiz_Line;
                    this.label104.Image = SeNA80.Properties.Resources.Vert_Line;
                    this.label95.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                    this.label98.Image = SeNA80.Properties.Resources.Vert_Line_Green;
                    
                    //finally switch the valve
                    SendControllerMsg(1, "V,P,0\n");

                }
            }

        }
        private void btnPumpSel_Click(object sender, EventArgs e)
        {
            //the Reagent Pump is selected so switch to Amidite Pump
            if (bReagentsPump)
            {
                //disable the reagents pump
                btnAmPump.Enabled = true;
                btnPump.Enabled = false;
                DispVol.Enabled = false;
                label110.Enabled = false;
                label3.Enabled = false;
                label2.Enabled = false;

                //enable the amidite pump
                btn_RecWaste.Enabled = true;
                AmDispVol.Enabled = true;
                AmDispVol.Enabled = true;
                label102.Enabled = true;
                label103.Enabled = true;
                label109.Enabled = true;

                //make the lines green
                label100.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                label104.Image = SeNA80.Properties.Resources.Vert_Line_Green;
                label105.Image = SeNA80.Properties.Resources.Vert_Line_Green;
                label106.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                label114.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;

                //make the reagent pump lines black
                label93.Image = SeNA80.Properties.Resources.Vert_Line;
                label17.Image = SeNA80.Properties.Resources.Vert_Line;
                label101.Image = SeNA80.Properties.Resources.Horiz_Line;

                bReagentsPump = false;
                

                //SendControllerMsg(1, valves.toamidpump);

            }
            else
            {
                //disable the reagents pump
                btnAmPump.Enabled = false;
                btnPump.Enabled = true;
                DispVol.Enabled = true;
                label110.Enabled = true;
                label3.Enabled = true;
                label2.Enabled = true;

                //enable the amidite pump
                btn_RecWaste.Enabled = false;
                AmDispVol.Enabled = false;
                AmDispVol.Enabled = false;
                label102.Enabled = false;
                label103.Enabled = false;
                label109.Enabled = false;

                //make the lines green
                label100.Image = SeNA80.Properties.Resources.Horiz_Line;
                label104.Image = SeNA80.Properties.Resources.Vert_Line;
                label105.Image = SeNA80.Properties.Resources.Vert_Line;
                label106.Image = SeNA80.Properties.Resources.Horiz_Line;
                label114.Image = SeNA80.Properties.Resources.Horiz_Line;

                //make the reagent pump lines black
                label93.Image = SeNA80.Properties.Resources.Vert_Line_Green;
                label17.Image = SeNA80.Properties.Resources.Vert_Line_Green;
                label101.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;

                bReagentsPump = true;

                //SendControllerMsg(1, valves.toreagpump);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {

        }

        private void btn_WashB_Click(object sender, EventArgs e)
        {

        }


        private void MakeTrainAGreen()
        {

            this.label87.Image = SeNA80.Properties.Resources.Vert_Line_Green;
            this.label1.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
            this.label4.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
            this.label6.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
            this.label8.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
            this.label10.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
            this.label12.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
            this.label14.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
            this.label16.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;


            this.label88.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
            btrainAGrn = true;
        }
        private void MakeTrainABlk()
        {

            this.label87.Image = SeNA80.Properties.Resources.Vert_Line;
            this.label1.Image = SeNA80.Properties.Resources.Horiz_Line1;
            this.label4.Image = SeNA80.Properties.Resources.Horiz_Line1;
            this.label6.Image = SeNA80.Properties.Resources.Horiz_Line1;
            this.label8.Image = SeNA80.Properties.Resources.Horiz_Line1;
            this.label10.Image = SeNA80.Properties.Resources.Horiz_Line1;
            this.label12.Image = SeNA80.Properties.Resources.Horiz_Line1;
            this.label14.Image = SeNA80.Properties.Resources.Horiz_Line1;
            this.label16.Image = SeNA80.Properties.Resources.Horiz_Line1;

            if (!btrainB1Grn)
                this.label88.Image = SeNA80.Properties.Resources.Horiz_Line1;
            btrainAGrn = false;
        }

        private void btn_Debl_Click(object sender, EventArgs e)
        {
            //check Activator
            if(bAct1 || bAct2)
            {
                if (MessageBox.Show("You have another reagent open and run the risk of contamination\n\n Not allowed!!!, please close the other bottle first???", "Bottle Open", MessageBoxButtons.OK, MessageBoxIcon.Hand) == DialogResult.OK)
                    return;
            }
            
            //check A train
            if (bWashA || bCapA || bCapB || bOx || bOx2 || bXtra1 )
            {
                if (MessageBox.Show("You have another bottle open and run the risk of contamination\n\n Would you like to continue???", "Bottle Open", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
                    return;
            }

            if (!bDeblock) //turn it on
            {
                this.label15.Image = SeNA80.Properties.Resources.Vert_Line_Green;
                if (!btrainAGrn)
                {
                    if (!bWashA && !bXtra2 && !bXtra1 && !bOx2 && !bOx && !bCapB && !bCapA)
                        MakeTrainAGreen();
                }
                //First turn the gas on
                String msg = valves.deblgason;
                SendControllerMsg(1, msg);

                Thread.Sleep(300); //0.5seconds to pressurize

                msg = valves.deblon;
                SendControllerMsg(1, msg);
                bDeblock = true;
            }
            else //turn it off
            {
                this.label15.Image = SeNA80.Properties.Resources.Vert_Line;
                if (btrainAGrn)
                {
                    if (!bWashA && !bXtra2 && !bXtra1 && !bOx2 && !bOx && !bCapB && !bCapA)
                        MakeTrainABlk();
                }
                String msg = valves.debloff;
                SendControllerMsg(1, msg);

                Thread.Sleep(100);

                //turn gas off
                msg = valves.deblgasoff;
                SendControllerMsg(1, msg);

                bDeblock = false;
            }

        }
        private void btn_CapA_Click(object sender, EventArgs e)
        {
            //check Activator
            if (bAct1 || bAct2)
            {
                if (MessageBox.Show("You have another reagent open and run the risk of contamination\n\n Not allowed!!!, please close the other bottle first???", "Bottle Open", MessageBoxButtons.OK, MessageBoxIcon.Hand) == DialogResult.OK)
                    return;
            }

            //check A train
            if (bDeblock || bWashA || bOx || bOx2 || bXtra1 )
            {
                if (MessageBox.Show("You have another bottle open and run the risk of contamination\n\n Would you like to continue???", "Bottle Open", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
                    return;
            }

            if (!bCapA) //turn it on
            {
                this.label13.Image = SeNA80.Properties.Resources.Vert_Line_Green;
                if (!btrainAGrn)
                {
                    if (!bWashA && !bXtra2 && !bXtra1 && !bOx2 && !bOx && !bCapB && !bDeblock)
                        MakeTrainAGreen();
                }
                //turn gas on
                String msg = valves.capagason;
                SendControllerMsg(1, msg);

                Thread.Sleep(500); //0.5seconds to pressurize

                msg = valves.capaon;
                SendControllerMsg(1, msg);
                bCapA = true;
            }
            else //turn it off
            {
                this.label13.Image = SeNA80.Properties.Resources.Vert_Line;
                if (btrainAGrn)
                {
                    if (!bWashA && !bXtra2 && !bXtra1 && !bOx2 && !bOx && !bCapB && !bDeblock)
                        MakeTrainABlk();
                }
                String msg = valves.capaoff;
                SendControllerMsg(1, msg);
                bCapA = false;
                if (!bCapB)
                {
                    Thread.Sleep(100);
                    //turn gas off
                    msg = valves.capagasoff;
                    SendControllerMsg(1, msg);
                }

            }

        }
        private void btn_CapB_Click(object sender, EventArgs e)
        {
            //check Activator
            if (bAct1 || bAct2)
            {
                if (MessageBox.Show("You have another reagent open and run the risk of contamination\n\n Not allowed!!!, please close the other bottle first???", "Bottle Open", MessageBoxButtons.OK, MessageBoxIcon.Hand) == DialogResult.OK)
                    return;
            }

            //check A train
            if (bDeblock || bWashA || bOx || bOx2 || bXtra1 )
            {
                if (MessageBox.Show("You have another bottle open and run the risk of contamination\n\n Would you like to continue???", "Bottle Open", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
                    return;
            }

            //check Amidite train
            if (bAm1 || bAm2 || bAm3 || bAm4 || bAm5 || bAm6 || bAm7 || bAm8 || bAm9 || bAm10 || bAm11 || bAm12 || bAm13 || bAm1)
            {
                if (MessageBox.Show("You have an amidite open and run the risk of contamination\n\n Would you like to continue???", "Bottle Open", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
                    return;
            }

            if (!bCapB) //turn it on
            {
                //make the train green
                if (!btrainB2Grn)
                    TrainB2Green();
            
                //open the flow path
                this.label54.Image = SeNA80.Properties.Resources.Horiz_Line_Grn;
                this.label53.Image = SeNA80.Properties.Resources.Vert_Line_Green;

                //turn gas on
                String msg = valves.capbgason;
                SendControllerMsg(1, msg);

                Thread.Sleep(200); //0.2seconds to pressurize

                msg = valves.capbon;
                SendControllerMsg(1, msg);
                bCapB = true;
            }
            else //turn it off
            {
                //make the train green
                if (AllClosed(14))
                {
                    if (btrainB2Grn)
                        TrainB2Black();
                }

                if (TrainBClosed(14))
                {
                    if (btrainB2Grn)
                        TrainB2Black();
                }
                //open the flow path
                this.label54.Image = SeNA80.Properties.Resources.Horiz_Line;
                this.label53.Image = SeNA80.Properties.Resources.Vert_Line;

                String msg = valves.capboff;
                SendControllerMsg(1, msg);
                bCapB = false;

                if (!bCapA)
                {
                    Thread.Sleep(100);
                    //turn gas off
                    msg = valves.capbgasoff;
                    SendControllerMsg(1, msg);
                }
            }
        }

        private void btn_Ox1_Click(object sender, EventArgs e)
        {
            //check Activator
            if (bAct1 || bAct2)
            {
                if (MessageBox.Show("You have another reagent open and run the risk of contamination\n\n Not allowed!!!, please close the other bottle first???", "Bottle Open", MessageBoxButtons.OK, MessageBoxIcon.Hand) == DialogResult.OK)
                    return;
            }

            //check A train
            if (bDeblock || bWashA || bCapA || bCapB || bOx2 || bXtra1 )
            {
                if (MessageBox.Show("You have another bottle open and run the risk of contamination\n\n Would you like to continue???", "Bottle Open", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
                    return;
            }

            if (!bOx) //turn it on
            {
                this.label9.Image = SeNA80.Properties.Resources.Vert_Line_Green;
                if (!btrainAGrn)
                {
                    if (!bWashA && !bXtra2 && !bXtra1 && !bOx2 && !bCapB && !bCapA && !bDeblock)
                        MakeTrainAGreen();
                }
                //turn gas on
                String msg = valves.ox1gason;
                SendControllerMsg(1, msg);

                Thread.Sleep(500); //0.5seconds to pressurize


                msg = valves.ox1on;
                SendControllerMsg(1, msg);
                bOx = true;
            }
            else //turn it off
            {
                this.label9.Image = SeNA80.Properties.Resources.Vert_Line;
                if (btrainAGrn)
                {
                    if (!bWashA && !bXtra2 && !bXtra1 && !bOx2 && !bCapB && !bCapA && !bDeblock)
                        MakeTrainABlk();
                }
                String msg = valves.ox1off;
                SendControllerMsg(1, msg);
                Thread.Sleep(100); //0.5seconds to pressurize

                //turn gas off
                msg = valves.ox1gasoff;
                SendControllerMsg(1, msg);

                bOx = false;
            }
        }

        private void btn_Ox2_Click(object sender, EventArgs e)
        {
            //check Activator
            if (bAct1 || bAct2)
            {
                if (MessageBox.Show("You have another reagent open and run the risk of contamination\n\n Not allowed!!!, please close the other bottle first???", "Bottle Open", MessageBoxButtons.OK, MessageBoxIcon.Hand) == DialogResult.OK)
                    return;
            }

            //check A train
            if (bDeblock || bWashA || bCapA || bCapB || bOx || bXtra1)
            {
                if (MessageBox.Show("You have another bottle open and run the risk of contamination\n\n Would you like to continue???", "Bottle Open", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
                    return;
            }
            if (!bOx2) //turn it on
            {
                this.label7.Image = SeNA80.Properties.Resources.Vert_Line_Green;
                if (!btrainAGrn)
                {
                    if (!bWashA && !bXtra2 && !bXtra1 && !bOx && !bCapB && !bCapA && !bDeblock)
                        MakeTrainAGreen();
                }
                //turn gas on
                String msg = valves.ox2gason;
                SendControllerMsg(1, msg);

                Thread.Sleep(500); //0.5seconds to pressurize


                msg = valves.ox2on;
                SendControllerMsg(1, msg);
                bOx2 = true;
            }
            else //turn it off
            {
                this.label7.Image = SeNA80.Properties.Resources.Vert_Line;
                if (btrainAGrn)
                {
                    if (!bWashA && !bXtra2 && !bXtra1 && !bOx && !bCapB && !bCapA && !bDeblock)
                        MakeTrainABlk();
                }
                String msg = valves.ox2off;
                SendControllerMsg(1, msg);
                Thread.Sleep(100); //0.5seconds to pressurize

                //turn gas off
                msg = valves.ox2gasoff;
                SendControllerMsg(1, msg);

                bOx2 = false;


            }

        }
        //DEA bottle   
        private void btn_Xtra1_Click(object sender, EventArgs e)
        {
            //check Activator
            if (bAct1 || bAct2)
            {
                if (MessageBox.Show("You have another reagent open and run the risk of contamination\n\n Not allowed!!!, please close the other bottle first???", "Bottle Open", MessageBoxButtons.OK, MessageBoxIcon.Hand) == DialogResult.OK)
                    return;
            }

            //check A train
            if (bDeblock || bWashA || bCapA || bCapB || bOx2 || bOx )
            {
                if (MessageBox.Show("You have another bottle open and run the risk of contamination\n\n Would you like to continue???", "Bottle Open", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
                    return;
            }
            if (!bXtra1) //turn it on
            {
                this.lblXtra1.Image = SeNA80.Properties.Resources.Vert_Line_Green;
                if (!btrainAGrn)
                {
                    if (!bWashA && !bXtra2 && !bOx2 && !bOx && !bCapB && !bCapA && !bDeblock)
                        MakeTrainAGreen();
                }
                String msg = valves.xtra1gason;
                SendControllerMsg(1, msg);

                Thread.Sleep(500); //0.5seconds to pressurize

                msg = valves.xtra1on;
                SendControllerMsg(1, msg);
                bXtra1 = true;
            }
            else //turn it off
            {
                this.lblXtra1.Image = SeNA80.Properties.Resources.Vert_Line;
                if (btrainAGrn)
                {
                    if (!bWashA && !bXtra2 && !bOx2 && !bOx && !bCapB && !bCapA && !bDeblock)
                        MakeTrainABlk();
                }
                String msg = valves.xtra1off;
                SendControllerMsg(1, msg);

                if (!bXtra2)
                {
                    Thread.Sleep(100); //0.5seconds to pressurize

                    //turn gas off
                    msg = valves.xtra1gasoff;
                    SendControllerMsg(1, msg);

                }
                bXtra1 = false;
            }

        }


        private void Man_Controlcs_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //if the trityl monitor is not attached gray menu item
            if (globals.bUVTrityl)
                Menu_Trityl.Enabled = true;
            else
                Menu_Trityl.Enabled = false;

            if (globals.bCondTrityl)
                Menu_Cond.Enabled = true;
            else
                Menu_Cond.Enabled = false;

            //set Amidite labels
            this.btnAm1.Text = Properties.Settings.Default.Am_1_lbl.ToString();
            this.btnAm2.Text = Properties.Settings.Default.Am_2_lbl.ToString();
            this.btnAm3.Text = Properties.Settings.Default.Am_3_lbl.ToString();
            this.btnAm4.Text = Properties.Settings.Default.Am_4_lbl.ToString();
            this.btnAm5.Text = Properties.Settings.Default.Am_5_lbl.ToString();
            this.btnAm6.Text = Properties.Settings.Default.Am_6_lbl.ToString();
            this.btnAm7.Text = Properties.Settings.Default.Am_7_lbl.ToString();
            this.btnAm8.Text = Properties.Settings.Default.Am_8_lbl.ToString();
            this.btnAm9.Text = Properties.Settings.Default.Am_9_lbl.ToString();
            this.btnAm10.Text = Properties.Settings.Default.Am_10_lbl.ToString();
            this.btnAm11.Text = Properties.Settings.Default.Am_11_lbl.ToString();
            this.btnAm12.Text = Properties.Settings.Default.Am_12_lbl.ToString();
            this.btnAm13.Text = Properties.Settings.Default.Am_13_lbl.ToString();
            this.btnAm14.Text = Properties.Settings.Default.Am_14_lbl.ToString();

            //initialize Amidite Pump - make sure it is at 0mL
            SendControllerMsg(1, "Q,0\n");
            iAmPVol = 0;

            //turn auto reccyle valve off
            //string msg = valves.recycleautoon;  //let the pump control the valve
            //SendControllerMsg(1, msg);

            //if not monitoring pressure hide the values
            if (!globals.bMonPressure)
            {
                this.l_AmPres.Visible = false;
                this.l_RgtPres.Visible = false;
                this.lb_AmPres.Visible = false;
                this.lb_RgPres.Visible = false;
            }

            this.Cursor = Cursors.Default;
        }

        private void btn_WashA_Click(object sender, EventArgs e)
        {
            //check A train
            if(bDeblock || bCapA || bCapB || bOx || bOx2 || bXtra1 || bXtra2)
            {
                if (MessageBox.Show("You have another bottle open and run the risk of contamination\n\n Would you like to continue???", "Bottle Open", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
                    return;
            }

            if (!bWashA) //turn it on
            {
                this.lblWash.Image = SeNA80.Properties.Resources.Vert_Line_Green;
                if (!btrainAGrn)
                {
                    if (!bXtra2 && !bXtra1 && !bOx2 && !bOx && !bCapB && !bCapA && !bDeblock)
                        MakeTrainAGreen();
                }
                String msg = valves.washagason;
                SendControllerMsg(1, msg);

                Thread.Sleep(500); //0.5seconds to pressurize

                msg = valves.washaon;
                SendControllerMsg(1, msg);
                bWashA = true;
            }
            else //turn it off
            {
                this.lblWash.Image = SeNA80.Properties.Resources.Vert_Line;
                if (btrainAGrn)
                {
                    if (!bXtra2 && !bXtra1 && !bOx2 && !bOx && !bCapB && !bCapA && !bDeblock)
                        MakeTrainABlk();
                }
                String msg = valves.washaoff;
                SendControllerMsg(1, msg);

                Thread.Sleep(100); //0.5seconds to pressurize

                //turn gas off
                msg = valves.washagasoff;
                SendControllerMsg(1, msg);

                bWashA = false;
            }
        }
    }
}

