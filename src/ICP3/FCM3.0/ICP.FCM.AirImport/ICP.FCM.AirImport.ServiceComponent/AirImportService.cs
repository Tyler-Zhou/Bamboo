//-----------------------------------------------------------------------
// <copyright file="AirImportService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.AirImport.ServiceComponent
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using ICP.FCM.Common.ServiceInterface.Common;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.FCM.AirImport.ServiceInterface;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Framework.CommonLibrary.Helper;
    using ICP.Framework.CommonLibrary.Server;
    using ICP.Common.ServiceInterface;
    using ICP.Sys.ServiceInterface;
    

    /// <summary>
    /// 空运进口服务
    /// </summary>
    partial class AirImportService : IAirImportService
    {
        #region 本地变量

        private ISessionService _sessionService;
        private ICustomerService _customerService;
        private IOrganizationService _organizationService;

        public AirImportService(
            ISessionService sessionService,
            ICustomerService customerService,
            IOrganizationService organizationService)
         {
             _sessionService = sessionService;
             _customerService = customerService;
             _organizationService = organizationService;

         }

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