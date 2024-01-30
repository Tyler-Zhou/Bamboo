﻿namespace ICP.FCM.DomesticTrade.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.FCM.DomesticTrade.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Sys.ServiceInterface.DataObjects;
    using ICP.FCM.DomesticTrade.ServiceInterface.CompositeObjects;
    using System.ServiceModel;

    /// <summary>
    /// 内贸业务拖车服务
    /// </summary>
    [ServiceInfomation("内贸业务拖车服务")]
    [ServiceContract]
    public interface IDomesticTradeTruckService
    {
        /// <summary>
        /// 根据业务ID获取拖车行最近业务信息
        /// </summary>
        /// <param name="orderID">业务ID</param>
        /// <returns>
        /// 该票业务对应客户的最近服务过的拖车行(TruckerID)
        /// 该票业务对应发货人的最近派车单中装货地(POLID)
        /// </returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult GetTruckRecentData(Guid orderID, bool IsEnglish);

        #region Truck

        /// <summary>
        /// 获取派车信息
        /// </summary>
        /// <param name="oceanBookingID">订单ID</param>
        /// <returns>返回派车信息</returns>
        [FunctionInfomation]
        [OperationContract]
        List<DTTruckInfo> GetDTTruckServiceList(Guid oceanBookingID,bool IsEnglish);

        /// <summary>
        /// 保存派车信息
        /// </summary>
        /// <param name="saveRequest">详见数据对象</param>
        /// <returns>返回SingleResult</returns>
        [FunctionInfomation]
        [OperationContract]
        HierarchyManyResult SaveDTTruckServiceInfo(TruckSaveRequest saveRequest,bool IsEnglish);

        /// <summary>
        /// 删除派车信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveDTTruckServiceInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate,
            bool IsEnglish);

        /// <summary>
        /// 删除派车的箱信息
        /// </summary>
        /// <param name="ids">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDates">更新时间-做数据版本用</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveDTTruckContainerInfo(
            Guid[] ids,
            Guid removeByID,
            DateTime?[] updateDates,
            bool IsEnglish);

        #endregion
    }
}