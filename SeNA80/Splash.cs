using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Timers;

namespace SeNA80
{
    public partial class Splash : Form
    {
        public bool bClosed = false;
        public System.Timers.Timer t = new System.Timers.Timer();
        public double iCntr = 0.0;
        Stopwatch stopwatch = new Stopwatch();

        public Splash()
        {
            InitializeComponent();
            this.TransparencyKey = Color.Turquoise;
            this.BackColor = Color.Turquoise;
            this.Invalidate();
            this.Update();
            
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            SafeNativeMethods.SetState(pb1, 3);
            SetTimer();
            stopwatch.Start();
        }
        private void SetTimer()
        {

            //set interval
            t.Interval = 100; 
            t.Elapsed +=  OnTimedEvent;
            t.Enabled = true;
            t.Start();

        }

        private void OnTimedEvent(Object source, EventArgs e)
        {
            Application.DoEvents();
            //kill if globals.bDemoMode = true;
            if(globals.bDemoMode)
            {
                iCntr = 100;
                bClosed = true;
                //this.Close();
            }
            //add 0.1 to the counter
            double iAdd = (double)(100)/(double)(globals.SplashSecs * 10) ;
            iCntr = iCntr + iAdd;
            TimeSpan ts = stopwatch.Elapsed;
            //leTime.Text = ts.Seconds.ToString("0")+"s";
            //put in logic based on the actual time it takes to load
            // each controller takes about 10%, the camera takes about 60%, 10% left over
           

            //update the status bar
            //if(iCntr < 100.1)
               //pb1.Value = (int)iCntr;

            //reset the timer
            if (iCntr > 100.1)
            {
                t.Stop();
                t.Dispose();
            }
            else
                t.Start();

        }
        
        private void Splash_FormClosing(object sender, FormClosingEventArgs e)
        {
            t.Stop();
            t.Dispose();
            bClosed = true;

        }

        private void Splash_Shown(object sender, EventArgs e)
        {
         /*  while (!bClosed)
            {
                Application.DoEvents();
            }*/
        }

       
    }
    public static class SafeNativeMethods
    {
        //imports
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
        
        [DllImport("User32")]
        static extern int ShowWindow(int hwnd, int nCmdShow);
        public static void SetState(this ProgressBar pBar, int state)
        {
            SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
        }
        public static int ShowMain(int hmainWind, int state)
        {
            int i = ShowWindow((int)hmainWind, state);

            return i;
        }
    }
    
}
