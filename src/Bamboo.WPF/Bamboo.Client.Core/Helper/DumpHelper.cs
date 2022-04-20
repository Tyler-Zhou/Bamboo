using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace Bamboo.Client.Core.Helper
{
    /// <summary>
    /// 转储帮助类
    /// </summary>
    public static class DumpHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>From dbghelp.h:</remarks>
        [Flags]
        public enum Option : uint
        {
            /// <summary>
            /// 
            /// </summary>
            Normal = 0x00000000,
            /// <summary>
            /// 
            /// </summary>
            WithDataSegs = 0x00000001,
            /// <summary>
            /// 
            /// </summary>
            WithFullMemory = 0x00000002,
            /// <summary>
            /// 
            /// </summary>
            WithHandleData = 0x00000004,
            /// <summary>
            /// 
            /// </summary>
            FilterMemory = 0x00000008,
            /// <summary>
            /// 
            /// </summary>
            ScanMemory = 0x00000010,
            /// <summary>
            /// 
            /// </summary>
            WithUnloadedModules = 0x00000020,
            /// <summary>
            /// 
            /// </summary>
            WithIndirectlyReferencedMemory = 0x00000040,
            /// <summary>
            /// 
            /// </summary>
            FilterModulePaths = 0x00000080,
            /// <summary>
            /// 
            /// </summary>
            WithProcessThreadData = 0x00000100,
            /// <summary>
            /// 
            /// </summary>
            WithPrivateReadWriteMemory = 0x00000200,
            /// <summary>
            /// 
            /// </summary>
            WithoutOptionalData = 0x00000400,
            /// <summary>
            /// 
            /// </summary>
            WithFullMemoryInfo = 0x00000800,
            /// <summary>
            /// 
            /// </summary>
            WithThreadInfo = 0x00001000,
            /// <summary>
            /// 
            /// </summary>
            WithCodeSegs = 0x00002000,
            /// <summary>
            /// 
            /// </summary>
            WithoutAuxiliaryState = 0x00004000,
            /// <summary>
            /// 
            /// </summary>
            WithFullAuxiliaryState = 0x00008000,
            /// <summary>
            /// 
            /// </summary>
            WithPrivateWriteCopyMemory = 0x00010000,
            /// <summary>
            /// 
            /// </summary>
            IgnoreInaccessibleMemory = 0x00020000,
            /// <summary>
            /// 
            /// </summary>
            ValidTypeFlags = 0x0003ffff,
        }
        /// <summary>
        /// 
        /// </summary>
        enum ExceptionInfo
        {
            /// <summary>
            /// 
            /// </summary>
            None,
            /// <summary>
            /// 
            /// </summary>
            Present
        }

        //typedef struct _MINIDUMP_EXCEPTION_INFORMATION {
        //    DWORD ThreadId;
        //    PEXCEPTION_POINTERS ExceptionPointers;
        //    BOOL ClientPointers;
        //} MINIDUMP_EXCEPTION_INFORMATION, *PMINIDUMP_EXCEPTION_INFORMATION;
        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 4)]  // Pack=4 is important! So it works also for x64!
        struct MiniDumpExceptionInformation
        {
            public uint ThreadId;
            public IntPtr ExceptionPointers;
            [MarshalAs(UnmanagedType.Bool)]
            public bool ClientPointers;
        }

        //BOOL
        //WINAPI
        //MiniDumpWriteDump(
        //    __in HANDLE hProcess,
        //    __in DWORD ProcessId,
        //    __in HANDLE hFile,
        //    __in MINIDUMP_TYPE DumpType,
        //    __in_opt PMINIDUMP_EXCEPTION_INFORMATION ExceptionParam,
        //    __in_opt PMINIDUMP_USER_STREAM_INFORMATION UserStreamParam,
        //    __in_opt PMINIDUMP_CALLBACK_INFORMATION CallbackParam
        //    );
        /// <summary>
        /// Overload requiring MiniDumpExceptionInformation
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="processId"></param>
        /// <param name="hFile"></param>
        /// <param name="dumpType"></param>
        /// <param name="expParam"></param>
        /// <param name="userStreamParam"></param>
        /// <param name="callbackParam"></param>
        /// <returns></returns>
        [DllImport("dbghelp.dll", EntryPoint = "MiniDumpWriteDump", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]

        static extern bool MiniDumpWriteDump(IntPtr hProcess, uint processId, SafeHandle hFile, uint dumpType, ref MiniDumpExceptionInformation expParam, IntPtr userStreamParam, IntPtr callbackParam);

        /// <summary>
        /// Overload supporting MiniDumpExceptionInformation == NULL
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="processId"></param>
        /// <param name="hFile"></param>
        /// <param name="dumpType"></param>
        /// <param name="expParam"></param>
        /// <param name="userStreamParam"></param>
        /// <param name="callbackParam"></param>
        /// <returns></returns>
        [DllImport("dbghelp.dll", EntryPoint = "MiniDumpWriteDump", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        static extern bool MiniDumpWriteDump(IntPtr hProcess, uint processId, SafeHandle hFile, uint dumpType, IntPtr expParam, IntPtr userStreamParam, IntPtr callbackParam);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "GetCurrentThreadId", ExactSpelling = true)]
        static extern uint GetCurrentThreadId();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileHandle"></param>
        /// <param name="options"></param>
        /// <param name="exceptionInfo"></param>
        /// <returns></returns>
        static bool Write(SafeHandle fileHandle, Option options, ExceptionInfo exceptionInfo)
        {
            Process currentProcess = Process.GetCurrentProcess();
            IntPtr currentProcessHandle = currentProcess.Handle;
            uint currentProcessId = (uint)currentProcess.Id;
            MiniDumpExceptionInformation exp;
            exp.ThreadId = GetCurrentThreadId();
            exp.ClientPointers = false;
            exp.ExceptionPointers = IntPtr.Zero;
            if (exceptionInfo == ExceptionInfo.Present)
            {
                exp.ExceptionPointers = Marshal.GetExceptionPointers();
            }
            return exp.ExceptionPointers == IntPtr.Zero ? MiniDumpWriteDump(currentProcessHandle, currentProcessId, fileHandle, (uint)options, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero) : MiniDumpWriteDump(currentProcessHandle, currentProcessId, fileHandle, (uint)options, ref exp, IntPtr.Zero, IntPtr.Zero);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileHandle"></param>
        /// <param name="dumpType"></param>
        /// <returns></returns>
        static bool Write(SafeHandle fileHandle, Option dumpType)
        {
            return Write(fileHandle, dumpType, ExceptionInfo.None);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dmpPath"></param>
        /// <param name="dmpType"></param>
        /// <returns></returns>
        public static bool TryDump(string dmpPath, Option dmpType = Option.Normal)
        {
            var path = Path.Combine(Environment.CurrentDirectory, dmpPath);
            var dir = Path.GetDirectoryName(path);
            if (dir != null && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            using (var fs = new FileStream(path, FileMode.Create))
            {
                return Write(fs.SafeFileHandle, dmpType);
            }
        }
    }
}
