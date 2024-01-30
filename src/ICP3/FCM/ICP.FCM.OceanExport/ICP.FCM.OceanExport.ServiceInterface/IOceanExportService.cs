//-----------------------------------------------------------------------
// <copyright file="IOceanExportService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using ICP.Framework.CommonLibrary.Attributes;
using System.ServiceModel;

namespace ICP.FCM.OceanExport.ServiceInterface
{
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
                                         , IOceanExportPOService
                                         , IOEFreightService
                                         , IOceanExportCustomsService
                                         , IOceanHBL2AmsAciIsfService
                                         , IAcceptBillReviseService
    {

    }
}
