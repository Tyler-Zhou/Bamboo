//-----------------------------------------------------------------------
// <copyright file="AirExportService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.AirExport.ServiceComponent
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using ICP.Common.ServiceInterface.DataObjects;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.FCM.AirExport.ServiceInterface;
    using ICP.FCM.AirExport.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Framework.CommonLibrary.Helper;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using ICP.Sys.ServiceInterface.DataObjects;

    /// <summary>
    /// 空运出口服务
    /// </summary>
    partial class AirExportService : IAirExportService
    {
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
        
    }
}
