//-----------------------------------------------------------------------
// <copyright file="IOceanExportService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.OceanExport.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Sys.ServiceInterface.DataObjects;
    using System.ServiceModel;


    /// <summary>
    /// 海运出口服务
    /// </summary>
    [ServiceInfomation("海运出口服务")]
    [ServiceContract]
    public interface IOceanExportService : IOceanExportBLService
                                         , IOceanExportBookingService
                                         , IOceanExportContainerService
                                         , IOceanExportOrderService
                                         , IOceanExportOtherService
                                         , IOceanExportTruckService
                                         , IOceanExportFeeService
                                         ,IOEFreightService
    {

    }
}
