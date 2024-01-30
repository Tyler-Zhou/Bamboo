using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.DomesticTrade.ServiceInterface.CompositeObjects;
using ICP.FCM.DomesticTrade.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ICP.FCM.DomesticTrade.ServiceInterface
{
    /// <summary>
    /// 内贸业务PO服务
    /// </summary>
    [ServiceInfomation("内贸业务PO服务")]
    [ServiceContract]
    public interface IDomesticTradePOService
    {
        /// <summary>
        /// 获取PO列表
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <returns>返回PO列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<DTBookingPOList> GetDTOrderPOList(Guid orderID, bool IsEnglish);

        /// <summary>
        /// 保存PO信息
        /// </summary>
        /// <param name="request">详见数据对象</param>
        /// <returns>返回ManyResult</returns>
        [FunctionInfomation]
        [OperationContract]
        HierarchyManyResult SaveDTOrderPOInfo(POSaveRequest request, bool IsEnglish);

        /// <summary>
        /// 改变PO状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="state">状态（0待处理、1已确认、2全部发货、3部分发货、4取消订单）</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult ChangeDTOrderPOState(
            Guid id,
            FCMPOState state,
            Guid changeByID,
            DateTime? updateDate,
            bool IsEnglish);

        /// <summary>
        /// 删除PO
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveDTOrderPOInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate,
            bool IsEnglish);
    }
}
