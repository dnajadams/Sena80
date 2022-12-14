using System;
using System.Data;
using System.IO.Ports;
using System.Management;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Text;

namespace SeNA80
{
    public partial class Main_Form

    {
        public static void StartSensorData(int Sensor)  //1 for UV 2 for Conductivity
        {
            string test = "";
            switch (Sensor)
            {
                case 1:
                    //Start the Sensor data stream
                    if (globals.bUVTrityl)
                    {
                        while (!globals.bUVStreaming)
                        {
                            Man_Controlcs.WriteStatus("Monitor Stream", "Starting the UV Stream Monitor");
                            Man_Controlcs.SyncWait(20);
                            //check to make sure the port is open
                            if (!UV_Trityl_Arduino.IsOpen)
                            {
                                try
                                {
                                    UV_Trityl_Arduino.Open();
                                    Man_Controlcs.SyncWait(100);
                                }
                                catch { MessageBox.Show("Could Not Open Monitor \nPlease make sure it is attached.", "Start Monitoring"); return; }
                                //stream may already be started, if not then start
                                if (globals.bUVStreaming)
                                {
                                    if (UV_Trityl_Arduino.ReadExisting().Length == 0)
                                        UV_Trityl_Arduino.Write("?\n");
                                }
                            }
                            else
                            {
                                int iBreak = 0;
                                int iStart = 0;

                                UV_Trityl_Arduino.Write("?\n");
                                Man_Controlcs.SyncWait(100);

                                while (UV_Trityl_Arduino.ReadExisting().Length < 10 && UV_Trityl_Arduino.IsOpen)
                                {
                                    Man_Controlcs.SyncWait(800);
                                    iBreak = iBreak + 1;
                                    iStart = iStart + 1;

                                    if (iStart > 10)
                                    {
                                        UV_Trityl_Arduino.Write("?\n"); iStart = 0;
                                    }


                                    if (iBreak > 30)
                                        break;
                                }
                                Man_Controlcs.SyncWait(800);

                                if (UV_Trityl_Arduino.ReadExisting().Length > 10)
                                    globals.bUVStreaming = true;

                            }
                        }


                        //prime the pump
                        if (UV_Trityl_Arduino.BytesToRead > 0)
                            test = UV_Trityl_Arduino.ReadLine();

                        UV_Trityl_Arduino.DiscardOutBuffer();
                    }
                    break;
                case 2:
                    //put in the Conductivity Start stream later
                    break;
            }

        }
        public void InitControllers()
        {
            String[] PortName = { "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "COM10", "COM11",
                "COM12", "COM13", "COM14", "COM15", "COM16", "COM17", "COM18", "COM19", "COM20", "COM21", "COM22", "COM23",
                "COM24", "COM25","COM26","COM27","COM28","COM29","COM30", };

            int i = 0;
            String k;
            bool portexists = false;
            String MainPort = "";
            String TritylPort = "";


            string[] ports = new string[] { "none" };

            // GetPortNames fails with no ports it throws a big exception, 
            // so we first check to see if any ports exists
            for (i = 0; i < 28; i++)
            {
                portexists = SerialPort.GetPortNames().Any(x => x == PortName[i]);
                k = String.Format("Port exists is {0}", portexists);
                //MessageBox.Show(k, PortName[i], MessageBoxButtons.OK);
                //we have valid serial ports, let the GetPortNames fine them all
                if (portexists)
                    break;
            }

            //Now check the names against the controllers we have
            SerialPort testPort;
            int iJoshua = 0;
            testPort = new SerialPort();

            //if they exist are they the ones we want (i.e. stage, arduino or autofocus ports)
            //ManagementObjectSearcher("SELECT * FROM WIN32_SerialPort")

            using (var searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_SerialPort"))
            {
                string[] portnames = SerialPort.GetPortNames();

                var ports_2 = searcher.Get().Cast<ManagementBaseObject>().ToList();
                var tList = (from n in portnames
                             join p in ports_2 on n equals p["DeviceID"].ToString()
                             select n + " - " + p["Caption"]).ToList();

                if (portnames.Length > 0)
                { 
                   
                    //string s in tList
                    foreach (String p in portnames)
                    {
                        //MessageBox.Show(s);
                        string sTP = "";
                        using (testPort = new SerialPort())
                        {
                            // so figure out which Arduino controller it is
                            testPort.PortName = portnames[iJoshua];
                            try
                            {

                                testPort.BaudRate = 38400;
                                try { testPort.Open(); } catch (Exception e) { MessageBox.Show("Serial Port Open Failed " + e.ToString()); }
                                if (testPort.IsOpen)
                                {
                                    Thread.Sleep(200);
                                    testPort.Write("?\n");
                                    Thread.Sleep(100);
                                    sTP = testPort.ReadExisting();
                                    int iQuit = 0;
                                    while (sTP.Length < 5 || sTP.Contains("ERR"))
                                    {

                                        testPort.Write("?\n");
                                        Thread.Sleep(100);
                                        sTP = testPort.ReadExisting();
                                        iQuit++;
                                        if (iQuit > 100)
                                            break;
                                    }
                                    //MessageBox.Show(testPort.PortName + " sTP - " + sTP, "Count" + iQuit.ToString());
                                    //testPort.Close();
                                }
                            }
                            catch
                            {
                                return;
                            }
                        }
           
                        if (sTP.Contains("Main"))
                        {
                            Main_Arduino.PortName = testPort.PortName;
                            globals.bIsMain = true;
                            bMainArduino = true;
                            MainPort = testPort.PortName;
                            Man_Controlcs.WriteStatus("Initialized Main Controller on", portnames[iJoshua].ToString());
                        }

                        /*later we will have trityl*/
                        else if (globals.bUVTrityl)
                        {

                            if (sTP.Contains("UV"))
                            {
                                UV_Trityl_Arduino.PortName = testPort.PortName;
                                globals.bIsTrityl = true;
                                Man_Controlcs.WriteStatus("Initialized Trityl Controller on", portnames[iJoshua].ToString());
                                bTritylArduino = true;
                                TritylPort = testPort.PortName;
                            }
                        }
                        else
                            bTritylArduino = true;

                            iJoshua++;
                    }
                }
            }
            if (!globals.bUVTrityl)
                bTritylArduino = true;

                //If can't initialize the controller give option to continue in demo mode or die
            if (!bMainArduino || !bTritylArduino)
            {
                    
           

                Man_Controlcs.WriteStatus("Did not find one or more of the Controllers", "Error");
                if (MessageBox.Show("Could Not Find  One or More of The Controllers on USB Ports. I found....\n\n " +
                                        "\nThe Main Controller on: " + MainPort +
                                        "\nThe Trityl Controller on: " + TritylPort +
                                        "\n \n Would you like to continue in Demo Mode"
                                       , "ERROR",
                                       MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                {
                    this.Dispose();
                    if (System.Windows.Forms.Application.MessageLoop)
                    {
                        // WinForms app
                        System.Windows.Forms.Application.Exit();
                    }
                    else
                    {
                        // Console app
                        System.Environment.Exit(1);
                    }
                }
                else
                    globals.bDemoMode = true;
            }
        }
    } //partial class

}
