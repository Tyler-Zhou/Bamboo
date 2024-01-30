using System;
using System.Collections.Generic;
using System.ServiceModel;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface.CompositeObjects;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.OceanExport.ServiceInterface
{
    /// <summary>
    /// 海运出口PO服务
    /// </summary>
    [ServiceInfomation("海运出口PO服务")]
    [ServiceContract]
    public interface IOceanExportPOService
    {
        #region 获取PO列表
        /// <summary>
        /// 获取PO列表
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <returns>返回PO列表</returns>
        [FunctionInfomation]
        [OperationContract(Name = "GetOceanOrderPOList1")]
        List<OceanBookingPOList> GetOceanOrderPOList(Guid orderID);

        /// <summary>
        /// 获取PO列表
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="companyID">口岸ID</param>
        /// <returns>返回PO列表</returns>
        [FunctionInfomation]
        [OperationContract(Name = "GetOceanOrderPOList2")]
        List<OceanBookingPOList> GetOceanOrderPOList(Guid orderID,Guid companyID);
        #endregion

        #region 保存PO信息

        ///// <summary>
        ///// 保存PO信息
        ///// </summary>
        ///// <param name="request">详见数据对象</param>
        ///// <returns>返回ManyResult</returns>
        //[FunctionInfomation]
        //[OperationContract]
        //HierarchyManyResult SaveOceanOrderPOInfo(POSaveRequest request);
        #endregion

        #region 删除PO
        /// <summary>
        /// 删除PO
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        [FunctionInfomation]
        [OperationContract(Name = "RemoveOceanOrderPOInfo1")]
        void RemoveOceanOrderPOInfo(Guid id, Guid removeByID, DateTime? updateDate);

        /// <summary>
        /// 删除PO
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="companyID">口岸ID</param>
        /// <param name="removeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        [FunctionInfomation]
        [OperationContract(Name = "RemoveOceanOrderPOInfo2")]
        void RemoveOceanOrderPOInfo(Guid id, Guid companyID, Guid removeByID, DateTime? updateDate);
        #endregion

        #region Comment Code

        #region 改变PO状态
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
        SingleResult ChangeOceanOrderPOState(Guid id, FCMPOState state, Guid changeByID, DateTime? updateDate);
        #endregion
        #endregion
    }
}
