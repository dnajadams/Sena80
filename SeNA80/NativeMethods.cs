using System;
using System.Runtime.InteropServices;

namespace SeNA80
{
    class NativeMethods
    {
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern bool ShowWindow(IntPtr handle, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow([MarshalAs(UnmanagedType.LPWStr)]  string lpClassName, [MarshalAs(UnmanagedType.LPWStr)] string lpWindowName);

        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern bool IsIconic(IntPtr hWnd);

        [DllImport("kernel32.dll")]
        public static extern uint SetThreadExecutionState(NativeMethods.ExecutionFlag flags);

        [DllImport("kernel32.dll")]
        public static extern uint GetCurrentThreadId();

        [Flags]

        /**Use Continue parameter, Restore System Sleep
        Not Use Continue parameter, Stop System Sleep one time
        Combine Continue parameter, Prevent System Sleep until thread abort
        **/
        public enum ExecutionFlag : uint
        {
            System = 0x00000001,
            Display = 0x00000002,
            Continus = 0x80000000,
        }
    }
}
