//-----------------------------------------------------------------------
// <copyright file="IAirExportService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using ICP.Framework.CommonLibrary.Attributes;
using System.ServiceModel;

namespace ICP.FCM.AirExport.ServiceInterface
{
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
                                         , IAirExportPOService
                                         , IAirExportBLReportService
    {

    }
}
