//-----------------------------------------------------------------------
// <copyright file="IAirExportService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.AirExport.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.FCM.AirExport.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Sys.ServiceInterface.DataObjects;
    using System.ServiceModel;


    /// <summary>
    /// 空运出口服务
    /// </summary>
    [ServiceInfomation("空运出口服务")]
    [ServiceContract]
    public interface IAirExportService : IAirExportBLService
                                         , IAirExportBookingService
                                         , IAirExportContainerService
                                         , IAirExportOrderService
                                         , IAirExportOtherService
                                         , IAirExportTruckService
                                         , IAirExportFeeService
                                         , IAirExportBLReportService
    {

    }
}
