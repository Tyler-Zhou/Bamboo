//-----------------------------------------------------------------------
// <copyright file="OceanExportService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Server;

namespace ICP.FCM.OceanExport.ServiceComponent
{
    /// <summary>
    /// 海运出口服务
    /// </summary>
    partial class OceanExportService : IOceanExportService
    {
        #region Fields
        /// <summary>
        /// 操作日志服务
        /// </summary>
        IOperationLogService _OperationLogService;
        /// <summary>
        /// 初始化服务
        /// </summary>
        IFrameworkInitializeService _FrameworkInitializeService;
        /// <summary>
        /// FCM公共服务
        /// </summary>
        IFCMCommonService _FCMCommonService;
        #endregion

        #region Init Service
        /// <summary>
        /// 海运出口服务
        /// </summary>
        /// <param name="OperationLogService">记录日志服务</param>
        /// <param name="frameworkInitializeService">初始化服务</param>
        /// <param name="fcmCommonService">FCM公共服务</param>
        public OceanExportService(IOperationLogService OperationLogService,
            IFrameworkInitializeService frameworkInitializeService,
            IFCMCommonService fcmCommonService)
        {
            _OperationLogService = OperationLogService;
            _FrameworkInitializeService = frameworkInitializeService;
            _FCMCommonService = fcmCommonService;
        } 
        #endregion

        #region Property
        /// <summary>
        /// 是否英文环境
        /// </summary>
        private bool IsEnglish
        {
            get
            {
                return ApplicationContext.Current.IsEnglish;
            }
        } 
        #endregion
    }
}
