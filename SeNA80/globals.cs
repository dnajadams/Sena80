using System;
using System.Runtime.InteropServices;
using System.Collections;


namespace SeNA80
{
    public static class globals
    {

        //forms with global need
        public static SeNARun runform = new SeNARun();
        public static Man_Controlcs manctl = new Man_Controlcs();
        public static Pressuriz pressuriz = new Pressuriz();
        public static stripchartcs sc = new stripchartcs();
        public static barchartcontrol sb = new barchartcontrol();

        //accessories
        public static bool bUVTrityl = Properties.Settings.Default.UV_Trityl_Inst;
        public static bool bCondTrityl = Properties.Settings.Default.Cond_Trityl_Inst;
        public static bool bMonPressure = Properties.Settings.Default.Pres_Mon_Inst;
        public static int polFreq = Properties.Settings.Default.Pol_Freq;
        public static int iAreaHeight = Properties.Settings.Default.Rpt_AreaHeight;
        public static int i12Ltr = Properties.Settings.Default.Ltr_12;
        public static bool bShowTips = Properties.Settings.Default.Tips_Enabled;

        //Paths
        public static String protocol_path = Properties.Settings.Default.Protocol_Path;
        public static String logfile_path = Properties.Settings.Default.Log_Path;
        public static String sequence_path = Properties.Settings.Default.Sequence_Path;
        public static String CSV_path = Properties.Settings.Default.CSV_Path;
        public static string Help_Path = "C:\\SeNA\\SeNA80 User Manual.pdf";
        public static DateTime dtStart = DateTime.Parse("01/01/2018");                          //our reference date for clock start

        //Alarms
        public static bool bAmPresAlarm = Properties.Settings.Default.Am_Pres_Alarm;
        public static bool bReagPresAlarm = Properties.Settings.Default.Reg_Pres_Alarm;
        public static bool bTritylAlarm = Properties.Settings.Default.Trityl_Alarm;
        public static int iTritylAlarmAmt = Properties.Settings.Default.Trityl_Amount;
        public static int iAmPresAmt = Properties.Settings.Default.Am_Pres_Amt;
        public static int iRgtPresAmt = Properties.Settings.Default.Reg_Pres_Amt;
        public static bool bTritylAlarOn = false;
        public static bool bSkipTritylAlarm = false;
        public static double dReagPres = 0.0;
        public static double dAmidPres = 0.0;


        //for Universal Protocols
        public static bool AutoScaleAmidites = Properties.Settings.Default.Autoscale_Amidites;
        public static double scalefactor = 1.0;

        //splash
        public static int SplashSecs = 12;

        //default protocols
        public static string defPrepProtocol = Properties.Settings.Default.def_Prep_Protocol;
        public static string defRunProtocol = Properties.Settings.Default.def_Run_Protocol;
        public static string defStartupProtocol = Properties.Settings.Default.def_Startup_Protocol;
        public static string defPostProtocol = Properties.Settings.Default.def_Post_Protocol;

        //user
        public static string Curr_User = "";
        public static string Curr_Rights = "";
        public static bool bQuickLogin = Properties.Settings.Default.QuickLogin;
        public static string Login_User = Properties.Settings.Default.LoginUser;
        public static bool UserOK = false;

        //User Administration - array to store all users in the database
        public static string[] Users = new String[500];
        public static bool bDirtyBase = false;

        //controller status
        public static bool bSWRecycleControl = false;
        public static bool bIsMain = false, bIsTrityl = false;
        public static bool blogging = false;
        public static bool bUVStreaming, bCondStreaming;
        public static string sStream = string.Empty;
        public static String log_file = "";
        public static int iMaxLines = Properties.Settings.Default.Max_Lines;
        public static int iLogInterval = Properties.Settings.Default.Log_Interval;
        public static bool bDemoMode = false;

        //run program globals
        public static double avgScale;
        public static double noOligos;
        public static bool bScaleLong = Properties.Settings.Default.Scale_Long;
        public static bool bDoubleFirst = Properties.Settings.Default.Double_First;
        public static bool bProtocolsLoaded = false, bSeqeuncesLoaded = false, bIsRunning = true;  //change back to false
        public static bool bIsPaused = false;
        public static bool bCol1 = false, bCol2 = false, bCol3 = false, bCol4 = false;
        public static bool bCol5 = false, bCol6 = false, bCol7 = false, bCol8 = false;
        public static bool bFluidicsBusy = false;
        public static bool bWaiting = false;
        public static bool bPumping = false;
        public static bool bCoupling = false;
        public static bool bOxidizing = false;
        public static bool bThiolating = false;
        public static int iAmBottle = 0;
        public static int iCoupCol = 0;
        public static bool bPumpBypass = false, bColBypass = false, bAmidReagPump = false, bRecycleOn = false;
        public static DateTime start_time;
        public static int cyclestoadd = 0;

        //trityl and pressure monitor arrays 
        //store a maximum of 1 days worth of data
        public static bool bStripCharting = false, bBarCharting = false;
        public static int tritylreport = Properties.Settings.Default.Report_Trity;
        public static int maxtritylpts = Properties.Settings.Default.Max_TritylPts;
        public static Queue UVTrityResponse = new Queue(maxtritylpts + 100);  //add buffer so that it never fills up
        public static Queue TritylDate = new Queue(maxtritylpts + 100);  //add buffer so it never fills up
        public static int[,] iUVTritylData = new int[11, 3600];
        public static DateTime[] xvalues = new DateTime[3600];
        public static string UVBarCSVfile = globals.CSV_path + Properties.Settings.Default.UV_Bar_File;
        public static int iAmPumpVol = 0;
        public static bool DeblockMonitor = false;
        public static int MonitorCntr = 0;
        public static bool bSaveBar = Properties.Settings.Default.SaveBar;
        public static bool bSaveStrip = Properties.Settings.Default.SaveStrip;
        public static bool bSaveHist = Properties.Settings.Default.SaveHist;

        //public static string UVBarCSVfile = globals.CSV_path + "UV_Bar" + DateTime.Now.ToString("MMddHHmm") + ".CSV";
        public static int iCurLine = 0;
        public static bool bZoomed = false;
        public static int iCycle = 31;  //CHANGE BACK TO 0
        public static bool bDeblocking = false;

        //flow pressure calibration
        public static double dAmiditeCF = Properties.Settings.Default.AmiditeFC;
        public static double dReagentCF = Properties.Settings.Default.ReagentFC;
        public static double dAmditePCalib = Properties.Settings.Default.AmiditePCalib;
        public static double dReagentPCalib = Properties.Settings.Default.ReagentPCalib;
        public static int iAmPumpDwell = Properties.Settings.Default.AmPumpDwell;
        public static int iReagPumpDwell = Properties.Settings.Default.ReagPumpDwell;

        //trityl histogram
        public static bool bTritylBar = false;
        public static bool bTrityLabels = true;

        //UVmonitor OnOff
        public static bool bUV1On = Properties.Settings.Default.UV1On;
        public static bool bUV2On = Properties.Settings.Default.UV2On;
        public static bool bUV3On = Properties.Settings.Default.UV3On;
        public static bool bUV4On = Properties.Settings.Default.UV4On;
        public static bool bUV5On = Properties.Settings.Default.UV5On;
        public static bool bUV6On = Properties.Settings.Default.UV6On;
        public static bool bUV7On = Properties.Settings.Default.UV7On;
        public static bool bUV8On = Properties.Settings.Default.UV8On;

        //Pressure Monitor OnOff
        public static bool bPres1On = Properties.Settings.Default.Pres1On;
        public static bool bPres2On = Properties.Settings.Default.Pres2On;
        public static bool bPres3On = Properties.Settings.Default.Pres3On;

        //Conductivity Monitor OnOff
        public static bool bCond1On = Properties.Settings.Default.Cond1On;
        public static bool bCond2On = Properties.Settings.Default.Cond2On;
        public static bool bCond3On = Properties.Settings.Default.Cond3On;
        public static bool bCond4On = Properties.Settings.Default.Cond4On;
        public static bool bCond5On = Properties.Settings.Default.Cond5On;
        public static bool bCond6On = Properties.Settings.Default.Cond6On;
        public static bool bCond7On = Properties.Settings.Default.Cond7On;
        public static bool bCond8On = Properties.Settings.Default.Cond8On;

    }
}