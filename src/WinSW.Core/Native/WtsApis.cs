using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WinSW.Native
{
    internal class WtsApis
    {
        [DllImport(Libraries.Kernel32)]
        internal static extern uint WTSGetActiveConsoleSessionId();

        [DllImport(Libraries.Wtsapi32, SetLastError = true)]
        internal static extern bool WTSQueryUserToken(uint sessionId, out IntPtr hToken);
    }
}
