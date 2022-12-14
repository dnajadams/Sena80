using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SeNA80
{
    public static class ApplicationRunningHelper
    {
      
        /// -------------------------------------------------------------------------------------------------     
        /// /// <summary> check if current process already running. if running, set focus to existing process and      
        /// ///           returns <see langword="true"/> otherwise returns <see langword="false"/>. </summary>     
        /// /// <returns> <see langword="true"/> if it succeeds, <see langword="false"/> if it fails. </returns>     
        /// /// -------------------------------------------------------------------------------------------------     
        public static bool AlreadyRunning()
        {
            /*         
            const int SW_HIDE = 0;         
            const int SW_SHOWNORMAL = 1;         
            const int SW_SHOWMINIMIZED = 2;         
            const int SW_SHOWMAXIMIZED = 3;         
            const int SW_SHOWNOACTIVATE = 4;         
            const int SW_RESTORE = 9;         
            const int SW_SHOWDEFAULT = 10;         
            */

            const int swRestore = 9;
            var me = Process.GetCurrentProcess();
            var arrProcesses = Process.GetProcessesByName(me.ProcessName);

            if (arrProcesses.Length > 1)
            {
                for (var i = 0; i < arrProcesses.Length; i++)
                {
                    if (arrProcesses[i].Id != me.Id)
                    {
                        // get the window handle                     
                        IntPtr hWnd = arrProcesses[i].MainWindowHandle;

                        // if iconic, we need to restore the window                     
                        if (NativeMethods.IsIconic(hWnd))
                        {
                            NativeMethods.ShowWindowAsync(hWnd, swRestore);
                            //MessageBox.Show("I am here");
                        }                     // bring it to the foreground                     

                        NativeMethods.SetForegroundWindow(hWnd);
                        break;
                    }
                }

                return true;
            }

            return false;
        }
    }

}

