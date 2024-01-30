//-----------------------------------------------------------------------
// <copyright file="OceanImportService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using ICP.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Server;
using ICP.Message.ServiceInterface;
using ICP.Sys.ServiceInterface;

namespace ICP.FCM.OceanImport.ServiceComponent
{
    /// <summary>
    /// 海运进口服务
    /// </summary>
    partial class OceanImportService : IOceanImportService
    {
        #region 服务
        /// <summary>
        /// 
        /// </summary>
        ISessionService _SessionService;
        /// <summary>
        /// 
        /// </summary>
        ICustomerService _CustomerService;
        /// <summary>
        /// 
        /// </summary>
        IOrganizationService _OrganizationService;
        /// <summary>
        /// 
        /// </summary>
        IMailBeeService _MailBeeService;
        /// <summary>
        /// 
        /// </summary>
        IUserService _UserService;
        /// <summary>
        /// 
        /// </summary>
        IMessageService _MessageService;
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

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionService"></param>
        /// <param name="customerService"></param>
        /// <param name="organizationService"></param>
        /// <param name="mailBeeService"></param>
        /// <param name="userService"></param>
        /// <param name="messageService"></param>
        /// <param name="frameworkInitializeService"></param>
        /// <param name="operationLogService"></param>
        /// <param name="fcmCommonService">FCM公共服务</param>
        public OceanImportService(
            ISessionService sessionService,
            ICustomerService customerService,
            IOrganizationService organizationService,
            IMailBeeService mailBeeService,
            IUserService userService,
            IMessageService messageService,
            IFrameworkInitializeService frameworkInitializeService,
            IOperationLogService operationLogService,
            IFCMCommonService fcmCommonService
            )
        {
            _SessionService = sessionService;
            _CustomerService = customerService;
            _OrganizationService = organizationService;
            _MailBeeService = mailBeeService;
            _UserService = userService;
            _MessageService = messageService;
            _FrameworkInitializeService = frameworkInitializeService;
            _OperationLogService = operationLogService;
            _FCMCommonService = fcmCommonService;
        } 
        #endregion

        #region 本地变量

        /// <summary>
        /// 判断是否英文环境
        /// </summary>
        bool isEnglish
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

        #endregion
    }
}