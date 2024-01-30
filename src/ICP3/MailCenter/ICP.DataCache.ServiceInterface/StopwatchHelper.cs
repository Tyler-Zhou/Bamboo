using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.DataCache.ServiceInterface
{
    /// <summary>
    /// 操作使用时间帮助类
    /// </summary>
    public class StopwatchHelper
    {
        private static ILocalBusinessCacheDataOperation localBusinessCacheDataOperation;
        private static ILocalBusinessCacheDataOperation LocalBusinessCacheDataOperation
        {
            get
            {
                if (localBusinessCacheDataOperation == null)
                {
                    localBusinessCacheDataOperation = ServiceClient.GetClientService<ILocalBusinessCacheDataOperation>();
                }
                return localBusinessCacheDataOperation;
            }
        }

        /// <summary>
        /// 开始监控
        /// </summary>
        /// <returns></returns>
        public static Stopwatch StartStopwatch()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            return stopwatch;
        }

        /// <summary>
        /// 保存操作用时(General)
        /// </summary>
        /// <param name="stopwatch">时间监控对象</param>
        /// <param name="startDatetime">开始时间</param>
        /// <param name="assamblyName">程序集</param>
        /// <param name="executeType">操作类型</param>
        /// <param name="OperationContent">操作描述</param>
        public static void EndStopwatch(Stopwatch stopwatch, DateTime startDatetime,
            string assamblyName, string executeType, string OperationContent)
        {
            if (stopwatch != null)
                LocalBusinessCacheDataOperation.AddOperationLog(Guid.Empty,startDatetime, assamblyName, executeType, OperationContent
                    , stopwatch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// 保存操作用时(General - No StopWatch)
        /// </summary>
        /// <param name="startDatetime">开始时间</param>
        /// <param name="assamblyName">程序集</param>
        /// <param name="executeType">操作类型</param>
        /// <param name="OperationContent">操作描述</param>
        /// <param name="OperationDuration">操作时长(ms)</param>
        public static void EndStopwatch(DateTime startDatetime,
            string assamblyName, string executeType, string OperationContent, string OperationDuration)
        {
            WaitCallback callback = data =>
            {
                try
                {
                    if (LocalBusinessCacheDataOperation != null)
                        LocalBusinessCacheDataOperation.AddOperationLog(Guid.Empty, startDatetime, assamblyName, executeType, OperationContent, OperationDuration);
                }
                catch
                {
                }
            };
            ThreadPool.QueueUserWorkItem(callback);
        }

        /// <summary>
        /// 保存操作用时(Custom LogID)
        /// </summary>
        /// <param name="stopwatch">时间监控对象</param>
        /// <param name="logID">日志ID</param>
        /// <param name="startDatetime">开始时间</param>
        /// <param name="assamblyName">程序集</param>
        /// <param name="executeType">操作类型</param>
        /// <param name="OperationContent">操作描述</param>
        public static void CustomRecordStopwatch(Stopwatch stopwatch,Guid logID, DateTime startDatetime,
            string assamblyName, string executeType, string OperationContent)
        {
            WaitCallback callback = data =>
            {
                try
                {
                    if (LocalBusinessCacheDataOperation != null)
                        LocalBusinessCacheDataOperation.AddOperationLog(logID, startDatetime, assamblyName, executeType, OperationContent
                        , stopwatch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
                }
                catch
                {
                }
            };
            ThreadPool.QueueUserWorkItem(callback);
            
        }
        
        /// <summary>
        /// 记录操作日志(Custom LogID - Update)
        /// </summary>
        /// <param name="stopwatch">时间监控对象</param>
        /// <param name="logID">日志ID</param>
        /// <param name="ExecuteDescription">操作内容(F1)</param>
        /// <param name="ExecuteDescription2">操作内容2(F2)</param>
        /// <param name="ExecuteDescription3">操作内容3(F3)</param>
        /// <param name="IsPending">是否待定(F4)</param>
        /// <param name="F5">F5</param>
        /// <param name="ExecuteDescription7">操作内容7(F7)</param>
        /// <param name="ExecuteDescription8">操作内容8(F8)</param>
        /// <param name="ExecuteDescription9">操作内容9(F9)</param>
        /// <param name="ExecuteDescription10">操作内容10(F10)</param>
        public static void CustomUpdateStopwatchLog(Stopwatch stopwatch, Guid logID, bool IsPending, string ExecuteDescription = "", string ExecuteDescription2 = ""
            , string ExecuteDescription3 = "", bool F5 = false, string ExecuteDescription7 = "", string ExecuteDescription8 = "", string ExecuteDescription9 = "", string ExecuteDescription10 = "")
        {
            WaitCallback callback = data =>
            {
                try
                {
                    if (LocalBusinessCacheDataOperation != null)
                        LocalBusinessCacheDataOperation.UpdateOperationLog(logID, ExecuteDescription, ExecuteDescription2, ExecuteDescription3, IsPending, F5, stopwatch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture)
                        , ExecuteDescription7, ExecuteDescription8, ExecuteDescription9, ExecuteDescription10);
                }
                catch
                {
                }
            };
            ThreadPool.QueueUserWorkItem(callback);
        }

        

        #region Comment Code
        ///// <summary>
        ///// 保存操作用时
        ///// </summary>
        ///// <param name="stopwatch">时间监控对象</param>
        ///// <param name="startDatetime">开始时间</param>
        ///// <param name="assamblyName">程序集</param>
        ///// <param name="executeDescription">操作描述</param>
        //public static void EndStopwatch(Stopwatch stopwatch, DateTime startDatetime,
        //    string assamblyName, string executeDescription)
        //{
        //    if (stopwatch != null)
        //        LocalBusinessCacheDataOperation.Add(startDatetime, assamblyName, executeDescription, stopwatch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));

        //}


        /// <summary>
        /// 邮件中心预览邮件超过3秒就记录日志
        /// </summary>
        /// <param name="stopwatch"></param>
        /// <param name="startDatetime"></param>
        /// <param name="assamblyName"></param>
        /// <param name="functionName"></param>
        public static void MailCenterPreviewMailEndStopwatch(Stopwatch stopwatch, DateTime startDatetime, string assamblyName, string functionName)
        {
            //if (stopwatch != null)
            //{
            //    if (stopwatch.ElapsedMilliseconds > 3000)
            //    {
            //        EndStopwatch(stopwatch, startDatetime, assamblyName, functionName);
            //    }
            //}
        } 
        #endregion

    }
}
