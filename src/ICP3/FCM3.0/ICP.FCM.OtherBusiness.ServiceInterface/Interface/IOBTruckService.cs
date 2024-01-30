using System;
using System.Collections.Generic;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.OtherBusiness.ServiceInterface.CompositeObjects;
using System.ServiceModel;

namespace ICP.FCM.OtherBusiness.ServiceInterface
{
    /// </summary>
    [ServiceInfomation("其他业务拖车服务")]
    [ServiceContract]
    public interface IOBTruckService
    {
         /// <summary>
        /// 根据业务ID获取拖车行最近业务信息
        /// </summary>
        /// <param name="orderID">业务ID</param>
        /// <returns>
        /// 该票业务对应客户的最近服务过的拖车行(TruckerID)
        /// 该票业务对应发货人的最近派车单中装货地(POLID)
        /// </returns>
        [OperationContract]
        SingleResult GetTruckRecentData(Guid orderID, bool IsEnglish);

         /// <summary>
        /// 获取派车信息
        /// </summary>
        /// <param name="oceanBookingID">订单ID</param>
        /// <returns>返回派车信息</returns>
        [OperationContract]
        List<TruckInfo> GetOBTruckServiceList(Guid BookingID,bool IsEnglish);

          /// <summary>
        /// 保存拖车信息
        /// </summary>
         /// <returns>返回SingleResult</returns>
        [OperationContract]
         HierarchyManyResult SaveOBTruckServiceInfo(TruckSaveRequest saveRequest, bool IsEnglish);
         /// <summary>
        /// 删除派车信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        [OperationContract]
        void RemoveOBTruckServiceInfo(
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
        [OperationContract]
        void RemoveOBTruckContainerInfo(
           Guid[] ids,
           Guid removeByID,
            DateTime?[] updateDates,
            bool IsEnglish);
    }
}
