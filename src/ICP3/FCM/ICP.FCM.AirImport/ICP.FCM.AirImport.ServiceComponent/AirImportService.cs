//-----------------------------------------------------------------------
// <copyright file="AirImportService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using ICP.Common.ServiceInterface;
using ICP.FCM.AirImport.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Server;
using ICP.Sys.ServiceInterface;

namespace ICP.FCM.AirImport.ServiceComponent
{
    /// <summary>
    /// 空运进口服务
    /// </summary>
    partial class AirImportService : IAirImportService
    {
        #region 服务
        /// <summary>
        /// 初始化服务
        /// </summary>
        IFrameworkInitializeService FrameworkInitializeService;
        /// <summary>
        /// FCM公共服务
        /// </summary>
        IFCMCommonService _FCMCommonService; 
        #endregion

        #region 构造函数
        public AirImportService(
            IFrameworkInitializeService frameworkInitializeService, 
            IFCMCommonService fcmCommonService)
        {
            FrameworkInitializeService = frameworkInitializeService;
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