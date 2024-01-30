//-----------------------------------------------------------------------
// <copyright file="FCMCommonService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.Common.ServiceComponent
{
    using System;
    using System.Collections.Generic;
    using ICP.FCM.Common.ServiceInterface;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using CommonData = ICP.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Framework.CommonLibrary.Helper;
    using System.Data;
    using System.Linq;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using ICP.Framework.CommonLibrary.Server;

    /// <summary>
    /// FCM公共服务
    /// </summary>
    public partial class FCMCommonService : IFCMCommonService
    {
       private ISessionService _sessionService;


       public FCMCommonService(ISessionService sessionService)
        {
            _sessionService = sessionService;
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
    }
}
