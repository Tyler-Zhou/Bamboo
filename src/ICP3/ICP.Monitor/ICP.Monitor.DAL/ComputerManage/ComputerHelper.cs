#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/23 10:24:44
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ICP.Monitor.Model.ComputerManage;

namespace ICP.Monitor.DAL.ComputerManage
{
    /// <summary>
    /// 服务器信息帮助类
    /// </summary>
    public class ComputerHelper
    {
        #region Fields & Service & Struct
        //定义内存的信息结构
        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_INFO
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public uint dwTotalPhys;
            public uint dwAvailPhys;
            public uint dwTotalPageFile;
            public uint dwAvailPageFile;
            public uint dwTotalVirtual;
            public uint dwAvailVirtual;
        } 
        #endregion

        #region DllImport
        /// <summary>
        /// kernel32.dll是Windows9x/Me中非常重要的32位动态链接库文件，属于内核级文件。
        ///它控制着系统的内存管理、数据的输入输出操作和中断处理，当Windows启动时，kernel32.dll就驻留在内存中特定的写保护区域，使别的程序无法占用这个内存区域。
        /// </summary>
        [DllImport("kernel32", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern void GetWindowsDirectory(StringBuilder WinDir, int count);

        [DllImport("kernel32", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern void GetSystemDirectory(StringBuilder SysDir, int count);

        [DllImport("kernel32", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern void GlobalMemoryStatus(ref MEMORY_INFO meminfo); 
        #endregion


        /// <summary>
        /// 获取计算机名
        /// </summary>
        /// <returns></returns>
        public static string GetMachineName()
        {
            try
            {
                return Environment.MachineName;
            }
            catch (Exception e)
            {
                return "uMnNk";
            }
        }

        #region CPU
        /// <summary>
        /// 输出CPU信息
        /// </summary>
        /// <returns></returns>
        public static string GetCPUInfo()
        {
            StringBuilder sb = new StringBuilder();
            int cpuPercent = Convert.ToInt32(GetCPUCounter());
            sb.Append(cpuPercent);
            return sb.ToString();
        }

        /// <summary>
        /// 获取CPU信息
        /// </summary>
        /// <returns></returns>
        private static object GetCPUCounter()
        {
            PerformanceCounter pc = new PerformanceCounter
            {
                CategoryName = "Processor",
                CounterName = "% Processor Time",
                InstanceName = "_Total"
            };
            dynamic Value_1 = pc.NextValue();
            System.Threading.Thread.Sleep(1000);
            dynamic Value_2 = pc.NextValue();
            return Value_2;
        } 
        #endregion

        #region Memory
        /// <summary>
        /// 获取内存信息
        /// </summary>
        /// <returns></returns>
        public static string GetMemoryInfo()
        {
            //调用GlobalMemoryStatus函数获取内存的相关信息
            MEMORY_INFO MemInfo = new MEMORY_INFO();
            GlobalMemoryStatus(ref MemInfo);
            //拼接字符串
            StringBuilder sb = new StringBuilder();
            return "" + MemInfo.dwMemoryLoad;
        } 
        #endregion

        #region Disk Hard

        public static List<EDriveInfo> GetHardDisks()
        {
            List<EDriveInfo> driveList = new List<EDriveInfo>();
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            foreach (System.IO.DriveInfo drive in drives)
            {
                EDriveInfo eDriveInfo = new EDriveInfo();
                eDriveInfo.DriveName = drive.Name;
                eDriveInfo.DriveType = drive.DriveType.ToString();
                if (drive.DriveType == System.IO.DriveType.Fixed || drive.DriveType == System.IO.DriveType.Removable)
                {
                    eDriveInfo.Capacity = drive.TotalSize / (1024 * 1024 * 1024);
                    eDriveInfo.FeeSpace = drive.TotalFreeSpace / (1024 * 1024 * 1024);
                }
                driveList.Add(eDriveInfo);
            }
            return driveList;
        }

        /// <summary>
        /// 获取指定驱动器的空间总大小(单位为B) 
        /// 只需输入代表驱动器的字母即可 （大写） 
        /// </summary>
        /// <param name="str_HardDiskName"></param>
        /// <returns></returns>
        public static float GetHardDiskSpace(string str_HardDiskName)
        {
            float totalSize = new float();
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            foreach (System.IO.DriveInfo drive in drives)
            {
                if (drive.Name == str_HardDiskName)
                {
                    totalSize = drive.TotalSize / (1024 * 1024 * 1024);
                    break;
                }
            }
            return totalSize;
        }

        /// <summary>
        /// 获取指定驱动器的剩余空间总大小(单位为B) 
        /// 只需输入代表驱动器的字母即可  
        /// </summary>
        /// <param name="str_HardDiskName"></param>
        /// <returns></returns>
        public static long GetHardDiskFreeSpace(string str_HardDiskName)
        {
            long freeSpace = new long();
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            foreach (System.IO.DriveInfo drive in drives)
            {
                if (drive.Name == str_HardDiskName)
                {
                    freeSpace = drive.TotalFreeSpace / (1024 * 1024 * 1024);
                    break;
                }
            }
            return freeSpace;
        }
        #endregion
    }
}
