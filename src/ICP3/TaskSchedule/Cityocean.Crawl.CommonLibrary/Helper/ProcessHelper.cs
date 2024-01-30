#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/6/23 9:50:23
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Diagnostics;
using System.Threading;

namespace Cityocean.Crawl.CommonLibrary
{
    /// <summary>
    /// 进程帮助类
    /// </summary>
    public sealed class ProcessHelper
    {
        public static void KillProcess4ID(int threadID)
        {
            Process[] pro = Process.GetProcesses();//获取已开启的所有进程

            //遍历所有查找到的进程
            for (int i = 0; i < pro.Length; i++)
            {

                //判断此进程是否是要查找的进程
                if (pro[i].Id == threadID)
                {
                    pro[i].Kill();//结束进程
                }
            }
        }

        /// <summary>
        /// 通过进程名称结束进程
        /// </summary>
        /// <param name="processId">进程名称</param>
        public static void KillProcess(int processId)
        {
            try
            {
                if (processId == 0) return;
                Process processe = GetProcessById(processId);
                if (!processe.IsNull())
                    processe.Kill();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 通过进程名称结束进程
        /// </summary>
        /// <param name="appName">进程名称</param>
        public static void KillProcess(string appName)
        {
            Process[] processes = Process.GetProcessesByName(appName);
            if (processes.Length <= 0) return;
            foreach (Process process in processes)
            {
                KillProcess(process.Id);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="processId"></param>
        public static Process GetProcessById(int processId)
        {
            try
            {
                return Process.GetProcessById(processId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 判断进程是否存
        /// </summary>
        /// <param name="appName">进程名称</param>
        public static bool ExistsProcess(string appName)
        {
            try
            {
                Process[] processes = Process.GetProcessesByName(appName);
                return processes.Length > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
