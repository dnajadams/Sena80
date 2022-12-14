using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeNA80
{
    public partial class Sensor_Config : Form
    {
        public Sensor_Config()
        {
            InitializeComponent();

            //can only turn cells on/off if configured
            if (!globals.bUVTrityl)
                gb_UVMonitors.Hide();
            if (!globals.bMonPressure || globals.bMonPressure)
                gb_PresMon.Hide();
            if (!globals.bCondTrityl)
                gb_Cond.Hide(); 
        }

        private void CellsOnOffcs_Load(object sender, EventArgs e)
        {
            //set UV State
            if (globals.bUVTrityl)
            {
                if (globals.bUV1On)
                    cb_UV1.Checked = true;
                if (globals.bUV2On)
                    cb_UV2.Checked = true;
                if (globals.bUV3On)
                    cb_UV3.Checked = true;
                if (globals.bUV4On)
                    cb_UV4.Checked = true;
                if (globals.bUV5On)
                    cb_UV5.Checked = true;
                if (globals.bUV6On)
                    cb_UV6.Checked = true;
                if (globals.bUV7On)
                    cb_UV7.Checked = true;
                if (globals.bUV8On)
                    cb_UV8.Checked = true;
            }
            //next pressure
            if(globals.bMonPressure)
            {
                if (globals.bPres1On)
                    cb_Pres1.Checked = true;
                if (globals.bPres2On)
                    cb_Pres2.Checked = true;
                if (globals.bPres3On)
                    cb_Pres3.Checked = true;
            }
            //and...conductivity
            if(globals.bCondTrityl)
            {
                if (globals.bCond1On)
                    cb_Cond1.Checked = true;
                if (globals.bCond2On)
                    cb_Cond2.Checked = true;
                if (globals.bCond3On)
                    cb_Cond3.Checked = true;
                if (globals.bCond4On)
                    cb_Cond4.Checked = true;
                if (globals.bCond5On)
                    cb_Cond5.Checked = true;
                if (globals.bCond6On)
                    cb_Cond6.Checked = true;
                if (globals.bCond7On)
                    cb_Cond7.Checked = true;
                if (globals.bCond8On)
                    cb_Cond8.Checked = true;


            }
        }
        public static async void LightsOn(int cell)
        {
            switch (cell)
            {
                case 0:
                    if (globals.bUV1On)
                    {
                        Man_Controlcs.SendControllerMsg(2, valves.UV1off);
                        await Task.Delay(5);
                    }
                    globals.bUV1On = false;

                    if (globals.bUV2On)
                    {
                        Man_Controlcs.SendControllerMsg(2, valves.UV2off);
                        await Task.Delay(5);
                    }
                    globals.bUV2On = false;

                    if (globals.bUV3On)
                    {
                        Man_Controlcs.SendControllerMsg(2, valves.UV3off);
                        await Task.Delay(6);
                    }
                    globals.bUV3On = false;

                    if (globals.bUV4On)
                    {
                        Man_Controlcs.SendControllerMsg(2, valves.UV4off);
                        await Task.Delay(5);
                    }
                    globals.bUV4On = false;

                    if (globals.bUV5On)
                    {
                        Man_Controlcs.SendControllerMsg(2, valves.UV5off);
                        await Task.Delay(5);
                    }
                    globals.bUV5On = false;

                    if (globals.bUV6On)
                    {
                        Man_Controlcs.SendControllerMsg(2, valves.UV6off);
                        await Task.Delay(20);
                    }
                    globals.bUV6On = false;

                    if (globals.bUV7On)
                    {
                        Man_Controlcs.SendControllerMsg(2, valves.UV7off);
                        await Task.Delay(5);
                    }
                    globals.bUV7On = false;
                   
                    if (globals.bUV8On)
                    {
                        Man_Controlcs.SendControllerMsg(2, valves.UV8off);
                        await Task.Delay(5);
                    }
                        globals.bUV8On = false;
                    
                    break;


                case 1:
                    Man_Controlcs.SendControllerMsg(2, valves.UV1on);
                    await Task.Delay(15);
                    globals.bUV1On = true;
                    
                    break;
                case 2:
                    Man_Controlcs.SendControllerMsg(2, valves.UV2on);
                    await Task.Delay(15);
                    globals.bUV2On = true;
                    
                    break;
                case 3:
                    Man_Controlcs.SendControllerMsg(2, valves.UV3on);
                    await Task.Delay(15);
                    globals.bUV3On = true;
                    
                    break;
                case 4:
                    Man_Controlcs.SendControllerMsg(2, valves.UV4on);
                    await Task.Delay(15);
                    globals.bUV4On = true;
                    
                    break;
                case 5:
                    Man_Controlcs.SendControllerMsg(2, valves.UV5on);
                    await Task.Delay(15);
                    globals.bUV5On = true;
                    
                    break;
                case 6:
                    Man_Controlcs.SendControllerMsg(2, valves.UV6on);
                    await Task.Delay(15);
                    globals.bUV6On = true;
                    
                    break;
                case 7:
                    Man_Controlcs.SendControllerMsg(2, valves.UV7on);
                    await Task.Delay(15);
                    globals.bUV7On = true;
                    
                    break;
                case 8:
                    Man_Controlcs.SendControllerMsg(2, valves.UV8on);
                    await Task.Delay(15);
                    globals.bUV8On = true;
                    
                    break;
            }//end switch
          return;
        } //end program
        private void btn_OK_Click(object sender, EventArgs e)
        {
            //update values turn cell on/off and save to the settings file if desired
            //first process UV
            if (cb_UV1.Checked)
            {
                globals.bUV1On = true;
                Man_Controlcs.SendControllerMsg(2, "L,1,1\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.UV1On = true;
            }
            else
            {
                globals.bUV1On = false;
                Man_Controlcs.SendControllerMsg(2, "L,1,0\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.UV1On = false;
            }

            if (cb_UV2.Checked)
            {
                globals.bUV2On = true;
                Man_Controlcs.SendControllerMsg(2, "L,2,1\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.UV2On = true;
            }
            else
            {
                globals.bUV2On = false;
                Man_Controlcs.SendControllerMsg(2, "L,2,0\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.UV2On = false;
            }
            if (cb_UV3.Checked)
            {
                globals.bUV3On = true;
                Man_Controlcs.SendControllerMsg(2, "L,3,1\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.UV3On = true;
            }
            else
            {
                globals.bUV3On = false;
                Man_Controlcs.SendControllerMsg(2, "L,3,0\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.UV3On = false;
            }
            if (cb_UV4.Checked)
            {
                globals.bUV4On = true;
                Man_Controlcs.SendControllerMsg(2, "L,4,1\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.UV4On = true;
            }
            else
            {
                globals.bUV4On = false;
                Man_Controlcs.SendControllerMsg(2, "L,4,0\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.UV4On = false;
            }
            if (cb_UV5.Checked)
            {
                globals.bUV5On = true;
                Man_Controlcs.SendControllerMsg(2, "L,5,1\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.UV5On = true;
            }
            else
            {
                globals.bUV5On = false;
                Man_Controlcs.SendControllerMsg(2, "L,5,0\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.UV5On = false;
            }
            if (cb_UV6.Checked)
            {
                globals.bUV6On = true;
                Man_Controlcs.SendControllerMsg(2, "L,6,1\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.UV6On = true;
            }
            else
            {
                globals.bUV6On = false;
                Man_Controlcs.SendControllerMsg(2, "L,6,0\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.UV6On = false;
            }
            if (cb_UV7.Checked)
            {
                globals.bUV7On = true;
                Man_Controlcs.SendControllerMsg(2, "L,7,1\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.UV7On = true;
            }
            else
            {
                globals.bUV7On = false;
                Man_Controlcs.SendControllerMsg(2, "L,7,0\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.UV7On = false;
            }
            if (cb_UV8.Checked)
            {
                globals.bUV8On = true;
                Man_Controlcs.SendControllerMsg(2, "L,8,1\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.UV8On = true;
            }
            else
            {
                globals.bUV8On = false;
                Man_Controlcs.SendControllerMsg(2, "L,8,0\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.UV8On = false;
            }
            Properties.Settings.Default.Save();

            //Next Pressure
            if (cb_Pres1.Checked)
            {
                globals.bPres1On = true;
                //Man_Controlcs.SendControllerMsg(2, "P,1,1");
                if (cb_Default.Checked)
                    Properties.Settings.Default.Pres1On = true;
            }
            else
            {
                globals.bPres1On = false;
                //Man_Controlcs.SendControllerMsg(2, "P,1,0");
                if (cb_Default.Checked)
                    Properties.Settings.Default.Pres1On = false;
            }
            if (cb_Pres2.Checked)
            {
                globals.bPres2On = true;
                //Man_Controlcs.SendControllerMsg(2, "P,2,1");
                if (cb_Default.Checked)
                    Properties.Settings.Default.Pres2On = true;
            }
            else
            {
                globals.bPres2On = false;
                //Man_Controlcs.SendControllerMsg(2, "P,2,0");
                if (cb_Default.Checked)
                    Properties.Settings.Default.Pres2On = false;
            }
            if (cb_Pres3.Checked)
            {
                globals.bPres3On = true;
                //Man_Controlcs.SendControllerMsg(2, "P,3,1");
                if (cb_Default.Checked)
                    Properties.Settings.Default.Pres3On = true;
            }
            else
            {
                globals.bPres3On = false;
                //Man_Controlcs.SendControllerMsg(2, "P,3,0");
                if (cb_Default.Checked)
                    Properties.Settings.Default.Pres3On = false;
            }

            Properties.Settings.Default.Save();

            //last conductivity
           if (cb_Cond1.Checked)
            {
                globals.bCond1On = true;
                Man_Controlcs.SendControllerMsg(3, "C,1,1\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.Cond1On = true;
            }
           else
            {
                globals.bCond1On = false;
                Man_Controlcs.SendControllerMsg(3, "C,1,0\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.Cond1On = false;
            }
            if (cb_Cond2.Checked)
            {
                globals.bCond2On = true;
                Man_Controlcs.SendControllerMsg(3, "C,2,1\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.Cond2On = true;
            }
            else
            {
                globals.bCond2On = false;
                Man_Controlcs.SendControllerMsg(3, "C,2,0\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.Cond2On = false;
            }
            if (cb_Cond3.Checked)
            {
                globals.bCond3On = true;
                Man_Controlcs.SendControllerMsg(3, "C,3,1\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.Cond3On = true;
            }
            else
            {
                globals.bCond3On = false;
                Man_Controlcs.SendControllerMsg(3, "C,3,0\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.Cond3On = false;
            }
            if (cb_Cond4.Checked)
            {
                globals.bCond4On = true;
                Man_Controlcs.SendControllerMsg(3, "C,4,1\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.Cond4On = true;
            }
            else
            {
                globals.bCond4On = false;
                Man_Controlcs.SendControllerMsg(3, "C,4,0\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.Cond4On = false;
            }
            if (cb_Cond5.Checked)
            {
                globals.bCond5On = true;
                Man_Controlcs.SendControllerMsg(3, "C,5,1\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.Cond5On = true;
            }
            else
            {
                globals.bCond5On = false;
                Man_Controlcs.SendControllerMsg(3, "C,5,0\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.Cond5On = false;
            }
            if (cb_Cond6.Checked)
            {
                globals.bCond6On = true;
                Man_Controlcs.SendControllerMsg(3, "C,6,1\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.Cond6On = true;
            }
            else
            {
                globals.bCond6On = false;
                Man_Controlcs.SendControllerMsg(3, "C,6,0\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.Cond6On = false;
            }
            if (cb_Cond7.Checked)
            {
                globals.bCond7On = true;
                Man_Controlcs.SendControllerMsg(3, "C,7,1\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.Cond7On = true;
            }
            else
            {
                globals.bCond7On = false;
                Man_Controlcs.SendControllerMsg(3, "C,7,0\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.Cond7On = false;
            }
            if (cb_Cond8.Checked)
            {
                globals.bCond8On = true;
                Man_Controlcs.SendControllerMsg(3, "C,8,1\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.Cond8On = true;
            }
            else
            {
                globals.bCond8On = false;
                Man_Controlcs.SendControllerMsg(3, "C,8,0\n");
                if (cb_Default.Checked)
                    Properties.Settings.Default.Cond8On = false;
            }

            Properties.Settings.Default.Save();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {

        }
    }
}
