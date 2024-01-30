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
    using ICP.FCM.OceanExport.ServiceInterface.DataObjects.ReportObjects;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Common;
    using System.ServiceModel;
    /// <summary>
    /// 报表数据获取服务
    /// </summary>
    [ServiceInfomation("海运出口报表服务")]
    [ServiceContract]
    public interface IOEReportDataService
    {
        /// <summary>
        /// 获取订单报表数据
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        OEOrderReportData GetOEOrderReportData(Guid orderID);

        /// <summary>
        /// 获取提单报表数据
        /// </summary>
        /// <param name="blID">提单ID</param>
        /// <param name="blType">提单类型</param>
        /// <returns>返回提单报表数据</returns>
        [FunctionInfomation]
        [OperationContract]
        BLReportData GetBLReportData(
            Guid blID,
            FCMBLType blType);

        /// <summary>
        /// 获取当票委托的利润数据        /// </summary>
        /// <param name="bookingID">订舱委托ID</param>
        /// <returns>返回当票委托的利润数据</returns>
        [FunctionInfomation]
        [OperationContract]
        ProfitReportData GetProfitReportData(Guid bookingID);

        /// <summary>
        /// 获取订舱确认报表数据
        /// </summary>
        /// <param name="blID">订舱委托ID</param>
        /// <returns>返回订舱确认报表数据</returns>
        [FunctionInfomation]
        [OperationContract]
        BookingConfirmationReportData GetBookingConfirmationReportData(Guid blID);

        /// <summary>
        /// 获取装货单报表数据        /// </summary>
        /// <param name="mblID">订舱委托ID</param>
        /// <returns>返回装货单报表数据</returns>
        [FunctionInfomation]
        [OperationContract]
        ShippingOrderReportData GetShippingOrderReportData(Guid mblID);

        /// <summary>
        /// 获取装箱报表
        /// </summary>
        /// <param name="containerID">箱ID</param>
        /// <returns>ContainerPackingReportData</returns>
        [FunctionInfomation]
        [OperationContract]
        ContainerPackingReportData GetContainerPackingReportData(Guid containerID);

        /// <summary>
        /// 派车国外报表数据对象(短格式)
        /// </summary>
        /// <param name="truckID">拖车ID</param>
        /// <returns>PickupENShortReportData</returns>
        [FunctionInfomation]
        [OperationContract]
        PickupENShortReportData GetPickupENShortReportData(Guid truckID);

        /// <summary>
        /// 获取派车国外报表数据对象
        /// </summary>
        /// <param name="truckID">拖车ID</param>
        /// <returns>PickupENReportData</returns>
        [FunctionInfomation]
        [OperationContract]
        PickupENReportData GetPickupENReportData(Guid truckID);

        /// <summary>
        /// 获派车国内报表数据对象
        /// </summary>
        /// <param name="truckID">拖车ID</param>
        /// <returns>PickupCNReportData</returns>
        [FunctionInfomation]
        [OperationContract]
        PickupCNReportData GetPickupCNReportData(Guid truckID);


        /// <summary>
        /// 获取订舱提单列表报表数据对象
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <returns>GetBookingReportData</returns>
        [FunctionInfomation]
        [OperationContract]
        OEBusinessReportData GetOEBusinessReportData(Guid operationID);
        //[FunctionInfomation]
        //[OperationContract]
        //ProfitContainerObjects GetOceanProfitReportData(Guid OperationID);
    }
}
