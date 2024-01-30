//-----------------------------------------------------------------------
// <copyright file="IOIReportDataService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.AirImport.ServiceInterface
{
    using System;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.FCM.AirImport.ServiceInterface.DataObjects;
    using System.ServiceModel;

    /// <summary>
    /// 空运进口报表数据获取服务
    /// </summary>
    [ServiceInfomation("空运进口报表数据获取服务")]
    [ServiceContract]
    public interface IAIReportDataService
    { 
        /// <summary>
        /// 获取订单报表数据
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <returns>OIOrderReportData</returns>
        [FunctionInfomation]
        [OperationContract]
        OIOrderReportData GetAIOrderReportData(Guid orderID,Guid companyID);

        /// <summary>
        /// 获取到港通知书数据
        /// </summary>
        /// <param name="businessID">业务ID</param>
        /// <returns>ArrivalNoticeReportData</returns>
        [FunctionInfomation]
        [OperationContract]
        ArrivalNoticeReportData GetArrivalNoticeReportData(Guid businessID);

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
        /// 获取海进业务提单列表报表数据对象
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <returns>OIBusinessReportData</returns>
        [FunctionInfomation]
        [OperationContract]
        OIBusinessReportData GetAIBusinessReportData(Guid operationID);

        /// <summary>
        /// 获取利润表数据
        /// </summary>
        /// <param name="businessID">业务ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ProfitReportData GetProfitReportData(Guid businessID);

        /// <summary>
        /// 获取Authority To Make Entry数据
        /// </summary>
        /// <param name="hblId">HBL ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        AIArrivalNoticeReportData GetAuthorityToMakeEntry(Guid hblId);

        /// <summary>
        /// 获取派车国内报表数据对象
        /// </summary>
        /// <param name="truckID">拖车ID</param>
        /// <returns>PickupCNReportData</returns>
        [FunctionInfomation]
        [OperationContract]
        PickupCNReportData GetPickupCNReportData(Guid truckID);
    }  
}