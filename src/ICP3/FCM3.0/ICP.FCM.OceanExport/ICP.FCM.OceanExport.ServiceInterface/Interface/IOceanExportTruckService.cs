namespace ICP.FCM.OceanExport.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Sys.ServiceInterface.DataObjects;
    using ICP.FCM.OceanExport.ServiceInterface.CompositeObjects;
    using System.ServiceModel;

    /// <summary>
    /// 海运出口拖车服务
    /// </summary>
    [ServiceInfomation("海运出口拖车服务")]
    [ServiceContract]
    public interface IOceanExportTruckService
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
        SingleResult GetTruckRecentData(Guid orderID);

        #region Truck

        /// <summary>
        /// 获取派车信息
        /// </summary>
        /// <param name="oceanBookingID">订单ID</param>
        /// <returns>返回派车信息</returns>
        [FunctionInfomation]
        [OperationContract]
        List<OceanTruckInfo> GetOceanTruckServiceList(Guid oceanBookingID);

        /// <summary>
        /// 保存派车信息
        /// </summary>
        /// <param name="saveRequest">详见数据对象</param>
        /// <returns>返回SingleResult</returns>
        [FunctionInfomation]
        [OperationContract]
        HierarchyManyResult SaveOceanTruckServiceInfo(TruckSaveRequest saveRequest);

        /// <summary>
        /// 删除派车信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveOceanTruckServiceInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate);

        /// <summary>
        /// 删除派车的箱信息
        /// </summary>
        /// <param name="ids">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDates">更新时间-做数据版本用</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveOceanTruckContainerInfo(
            Guid[] ids,
            Guid removeByID,
            DateTime?[] updateDates);

        #endregion
    }
}