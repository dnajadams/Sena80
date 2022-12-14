using System;

using System.Runtime.InteropServices;

namespace SeNA80
{
    class WindowAlreadyOpen
    {
        public static bool WindowOpen(string FocusWindowName)
        {
            /*Process[] processlist = Process.GetProcesses();

            foreach (Process process in processlist)
            {
                if (!String.IsNullOrEmpty(process.MainWindowTitle))
                {
                   // MessageBox.Show("Process:" + process.ProcessName + "Process ID: "+ process.Id+" { 1} Window title: "+ process.MainWindowTitle);
                }
            }*/

            IntPtr hWnd = NativeMethods.FindWindow(null, FocusWindowName); // this gives you the handle of the window you need.

            //MessageBox.Show("Here hWnd = " + hWnd.ToString());
            // then use this handle to bring the window to focus or forground(I guessed you wanted this)
            NativeMethods.ShowWindowAsync(hWnd, 9);

            //The bring the application to focus
            NativeMethods.SetForegroundWindow(hWnd);

            if (hWnd == (IntPtr)0)
                return false;
            else
                return true;
        }
    }
}
