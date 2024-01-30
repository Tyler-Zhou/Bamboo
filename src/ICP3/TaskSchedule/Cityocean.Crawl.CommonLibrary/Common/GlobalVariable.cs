#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/3/28 16:51:49
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;

namespace Cityocean.Crawl.CommonLibrary
{
    /// <summary>
    /// 全局变量
    /// </summary>
    public static class GlobalVariable
    {
        #region Init
        static GlobalVariable()
        {
            LogicalProcessorCount = Environment.ProcessorCount;
            ProgramDirectory = AppDomain.CurrentDomain.BaseDirectory;
        } 
        #endregion

        #region Program Directory
        /// <summary>
        /// 程序路径
        /// </summary>
        public static string ProgramDirectory { get; set; }  
        #endregion

        #region SessionID
        /// <summary>
        /// 会话ID
        /// </summary>
        public static Guid SessionID { get; set; } 
        #endregion

        #region Service Runing
        /// <summary>
        /// 服务是否正在运行
        /// </summary>
        public static bool ServiceIsRuning { get; set; } 
        #endregion

        #region Job Runing
        /// <summary>
        /// 码头任务是否运行
        /// </summary>
        public static bool TaskIsRunningTerminal { get; set; }
        /// <summary>
        /// 码头船期任务运行
        /// </summary>
        public static bool JobTerminalVesselScheduleRuning { get; set; }
        /// <summary>
        /// 码头船期解析任务
        /// </summary>
        public static bool JobAnalysisTerminalVesselScheduleRuning { get; set; }
        /// <summary>
        /// 货物动态运行中
        /// </summary>
        public static bool JobCargoTrackingRuning { get; set; }

        /// <summary>
        /// 货物动态运行中
        /// </summary>
        public static bool JobAnalysisCargoTrackingRuning { get; set; } 
        #endregion

        #region Time Span

        #endregion

        #region Cache Data
        #endregion

        #region INIHelper
        /// <summary>
        /// 配置文件路径
        /// </summary>
        public static string ConfigPath
        {
            get
            {
                return ProgramDirectory + CommonConstants.INI_CONFIG_NAME;
            }
        }
        /// <summary>
        /// 逻辑处理器数量
        /// </summary>
        public static int LogicalProcessorCount { get; set; }
        #endregion
    }
}
