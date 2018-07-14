using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Freeze
{
    public static class ProcessMgr
    {
        [DllImport("ntdll.dll")]
        private static extern uint NtSuspendProcess([In] IntPtr processHandle);

        [DllImport("ntdll.dll")]
        private static extern uint NtResumeProcess([In] IntPtr processHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(
            uint desiredAccess,
            bool inheritHandle,
            int processId);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle([In] IntPtr handle);

        public static void SuspendProcess(int processId)
        {
            IntPtr hProc = IntPtr.Zero;
            hProc = OpenProcess(0x800, false, processId);
            if (hProc != IntPtr.Zero) { 
                NtSuspendProcess(hProc);
                CloseHandle(hProc);
            }
        }

        public static void ResumeProcess(int processId)
        {
            IntPtr hProc = IntPtr.Zero;
            hProc = OpenProcess(0x800, false, processId);
            if (hProc != IntPtr.Zero){
                NtResumeProcess(hProc);
                CloseHandle(hProc);
            }
        }
    }
}
