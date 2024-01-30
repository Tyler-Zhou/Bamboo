//-----------------------------------------------------------------------
// <copyright file="FCMCommonService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Server;
using System;
using ICP.Message.ServiceInterface;
using ICP.Common.ServiceInterface;

namespace ICP.FCM.Common.ServiceComponent
{
    
    /// <summary>
    /// FCM公共服务
    /// </summary>
    public partial class FCMCommonService : IFCMCommonService
    {
        private ISessionService _sessionService;
        /// <summary>
        /// 操作日志服务
        /// </summary>
        IOperationLogService _OperationLogService;
        /// <summary>
        /// 公用服务
        /// </summary>
        ICommonService _CommonService;
        /// <summary>
        /// FCM公共服务初始化
        /// </summary>
        /// <param name="sessionService"></param>
        /// <param name="operationLogService"></param>
        /// <param name="mailBeeService">邮件服务</param>
        public FCMCommonService(ISessionService sessionService, IOperationLogService operationLogService, ICommonService commonService)
        {
            _sessionService = sessionService;
            _OperationLogService = operationLogService;
            _CommonService = commonService;
        }

        /// <summary>
        /// 是否英文环境
        /// </summary>
        private bool IsEnglish
        {
            get
            {
                try
                {
                    return ApplicationContext.Current.IsEnglish;
                }
                catch
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid UserID
        {
            get { return ApplicationContext.Current.UserId; }
        }

        /// <summary>
        /// CSP用户ID
        /// </summary>
        public int CSPUserID
        {
            get { return 0; }
        }
    }
}
