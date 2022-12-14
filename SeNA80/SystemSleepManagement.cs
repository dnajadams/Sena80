using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeNA80
{
    class SystemSleepManagement
    {
        
        /// <summary>
        /// PreventSleep, Unit the Thread finish
        /// </summary>
        /// <param name="includeDisplay">Bool Shutdown Display</param>
        public static void PreventSleep(bool includeDisplay = true)
        {
            if (includeDisplay)
                NativeMethods.SetThreadExecutionState(NativeMethods.ExecutionFlag.System | NativeMethods.ExecutionFlag.Display | NativeMethods.ExecutionFlag.Continus);
            else
                NativeMethods.SetThreadExecutionState(NativeMethods.ExecutionFlag.System | NativeMethods.ExecutionFlag.Continus);
        }

        /// <summary>
        /// ResotreSleep
        /// </summary>
        public static void ResotreSleep()
        {
            NativeMethods.SetThreadExecutionState(NativeMethods.ExecutionFlag.Continus);
        }

        /// <summary>
        ///Reset Sleep Timer
        /// </summary>
        /// <param name="includeDisplay"> Bool Shutdown Display </param>
        public static void ResetSleepTimer(bool includeDisplay = false)
        {
            if (includeDisplay)
                NativeMethods.SetThreadExecutionState(NativeMethods.ExecutionFlag.System | NativeMethods.ExecutionFlag.Display);
            else
                NativeMethods.SetThreadExecutionState(NativeMethods.ExecutionFlag.System);
        }
    }

}
