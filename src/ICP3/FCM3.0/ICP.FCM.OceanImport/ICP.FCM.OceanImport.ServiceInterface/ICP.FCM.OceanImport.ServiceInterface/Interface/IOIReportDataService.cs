﻿//-----------------------------------------------------------------------
// <copyright file="IOIReportDataService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.OceanImport.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.FCM.OceanImport.ServiceInterface;
    using ICP.Framework;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.FCM.OceanImport.ServiceInterface.DataObjects;
    using System.ServiceModel;
    using ICP.FCM.OceanImport.ServiceInterface.DataObjects.ReportObjects;

    /// <summary>
    /// 海运进口报表数据获取服务
    /// </summary>
    [ServiceInfomation("海运进口报表数据获取服务")]
    [ServiceContract]
    public interface IOIReportDataService
    {      
        /// <summary>
        /// 获取提单报表数据
        /// </summary>
        /// <param name="hblId">提单ID</param>
        /// <returns>返回提单报表数据</returns>
        [FunctionInfomation]
        [OperationContract]
        OIBLReportData GetOIBLReportData(Guid hblId);

        /// <summary>
        /// 获取订单报表数据
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <returns>OIOrderReportData</returns>
        [FunctionInfomation]
        [OperationContract]
        OIOrderReportData GetOIOrderReportData(Guid orderID);

        /// <summary>
        /// 获取到港通知书数据
        /// </summary>
        /// <param name="businessID">业务ID</param>
        /// <returns>ArrivalNoticeReportData</returns>
        [FunctionInfomation]
        [OperationContract]
        ArrivalNoticeReportData GetArrivalNoticeReportData(Guid businessID,Guid? hblID);

        /// <summary>
        /// 获取放货通知书数据
        /// </summary>
        /// <param name="businessID">业务ID</param>
        /// <returns>ReleaseOrderReportData</returns>
        [FunctionInfomation]
        [OperationContract]
        ReleaseOrderReportData GetReleaseOrderInfoByBusinessID(Guid businessID);

        /// <summary>
        /// 此方法的动作是把该业务的IsSentAN = true和记录发送时间
        /// </summary>
        /// <param name="businessID">业务ID</param>
        /// <param name="saveByID">saveByID</param>
        /// <returns>SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ArrivalNoticeSent(Guid businessID, Guid saveByID);

         /// <summary>
        /// 获取巴西到港通知书报表数据
        /// </summary>
        /// <param name="businessID">业务ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ArrivalNoticeReportDataForBrazil GetOIArrivalNoticeReportDataForBrazil(Guid businessID);

        /// <summary>
        /// 获取海进业务提单列表报表数据对象
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <returns>OIBusinessReportData</returns>
        [FunctionInfomation]
        [OperationContract]
        OIBusinessReportData GetOIBusinessReportData(Guid operationID);

        /// <summary>
        /// 获取派车委托单(预约)报表数据
        /// </summary>
        /// <param name="truckID">拖车ID</param>
        /// <returns>PickupCNReportData</returns>
        [FunctionInfomation]
        [OperationContract]
        OIPickupCNReportData GetOIPickupCNReportData(Guid truckID);
    }  
}