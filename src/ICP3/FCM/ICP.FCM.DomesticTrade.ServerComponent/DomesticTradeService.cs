//-----------------------------------------------------------------------
// <copyright file="OceanExportService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using ICP.FCM.DomesticTrade.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Server;

namespace ICP.FCM.DomesticTrade.ServiceComponent
{
    /// <summary>
    /// 内贸业务服务
    /// </summary>
    partial class DomesticTradeService : IDomesticTradeService
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
        #endregion

        #region Init Service
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationLogService">记录日志服务</param>
        /// <param name="frameworkInitializeService">初始化服务</param>
        public DomesticTradeService(IOperationLogService operationLogService,
            IFrameworkInitializeService frameworkInitializeService)
        {
            OperationLogService = operationLogService;
            FrameworkInitializeService = frameworkInitializeService;
        } 
        #endregion

        #region Property
        /// <summary>
        /// 是否英文环境
        /// </summary>
        public bool IsEnglish
        {
            get
            {
                return ApplicationContext.Current.IsEnglish;
            }
        } 
        #endregion
        
    }
}
