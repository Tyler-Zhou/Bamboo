//-----------------------------------------------------------------------
// <copyright file="AirExportService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using ICP.FCM.AirExport.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Server;

namespace ICP.FCM.AirExport.ServiceComponent
{
    /// <summary>
    /// 空运出口服务
    /// </summary>
    partial class AirExportService : IAirExportService
    {
        #region Fields
        /// <summary>
        /// 操作日志服务
        /// </summary>
        IOperationLogService OperationLogService;
        /// <summary>
        /// 初始化服务
        /// </summary>
        IFrameworkInitializeService FrameworkInitializeService;
        /// <summary>
        /// FCM公共服务
        /// </summary>
        IFCMCommonService _FCMCommonService;
        #endregion

        #region Init Service
        /// <summary>
        /// 空运出口服务
        /// </summary>
        /// <param name="operationLogService">记录日志服务</param>
        /// <param name="frameworkInitializeService">初始化服务</param>
        /// <param name="FCMCommonService">FCM公共服务</param>
        public AirExportService(IOperationLogService operationLogService,
            IFrameworkInitializeService frameworkInitializeService, IFCMCommonService FCMCommonService)
        {
            OperationLogService = operationLogService;
            FrameworkInitializeService = frameworkInitializeService;
            _FCMCommonService = FCMCommonService;
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
