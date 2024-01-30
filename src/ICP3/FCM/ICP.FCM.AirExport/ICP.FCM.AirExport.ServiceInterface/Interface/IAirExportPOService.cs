using ICP.FCM.AirExport.ServiceInterface.CompositeObjects;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ICP.FCM.AirExport.ServiceInterface
{
    /// <summary>
    /// 空运出口PO服务
    /// </summary>
    [ServiceInfomation("空运出口PO服务")]
    [ServiceContract]
    public interface IAirExportPOService
    {
        #region 获取PO列表
        /// <summary>
        /// 获取PO列表
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="companyID">订单口岸ID</param>
        /// <returns>返回PO列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<AirBookingPOList> GetAirOrderPOList(Guid orderID, Guid companyID); 
        #endregion

        #region 保存PO信息
        /// <summary>
        /// 保存PO信息
        /// </summary>
        /// <param name="request">详见数据对象</param>
        /// <returns>返回ManyResult</returns>
        [FunctionInfomation]
        [OperationContract]
        HierarchyManyResult SaveAirOrderPOInfo(POSaveRequest request); 
        #endregion

        #region 改变PO状态
        /// <summary>
        /// 改变PO状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="companyID">订单口岸ID</param>
        /// <param name="state">状态（0待处理、1已确认、2全部发货、3部分发货、4取消订单）</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult ChangeAirOrderPOState(Guid id, Guid companyID, FCMPOState state,
            Guid changeByID,
            DateTime? updateDate); 
        #endregion

        #region 删除PO
        /// <summary>
        /// 删除PO
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="companyID">订单口岸ID</param>
        /// <param name="removeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveAirOrderPOInfo(Guid id, Guid companyID, Guid removeByID, DateTime? updateDate); 
        #endregion
    }
}
