using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ICP.FCM.OceanImport.ServiceInterface
{
    /// <summary>
    /// 海运进口PO服务类
    /// </summary>
    [ServiceInfomation("海运PO服务")]
    [ServiceContract]
    public interface IOIPOItemService
    {
        #region 获取PO列表
        /// <summary>
        /// 获取PO列表
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <returns>返回PO列表</returns>
        [FunctionInfomation]
        [OperationContract(Name = "GetOIOrderPOList1")]
        List<OceanImportPOList> GetOIOrderPOList(Guid orderID);

        /// <summary>
        /// 获取PO列表
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="companyID">订单口岸ID</param>
        /// <returns>返回PO列表</returns>
        [FunctionInfomation]
        [OperationContract(Name = "GetOIOrderPOList2")]
        List<OceanImportPOList> GetOIOrderPOList(Guid orderID,Guid companyID); 
        #endregion

        #region 保存PO信息
        /// <summary>
        /// 保存PO信息
        /// </summary>
        /// <param name="poSaveRequest">保存PO的时候传递的实体</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        HierarchyManyResult SaveOIOrderPOInfo(POSaveRequest poSaveRequest);
        #endregion

        #region 删除PO
        /// <summary>
        /// 删除PO
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        [FunctionInfomation]
        [OperationContract(Name = "RemoveOIOrderPOInfo1")]
        void RemoveOIOrderPOInfo(Guid id, Guid removeByID, DateTime? updateDate);

        /// <summary>
        /// 删除PO
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="companyID">订单口岸ID</param>
        /// <param name="removeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        [FunctionInfomation]
        [OperationContract(Name = "RemoveOIOrderPOInfo2")]
        void RemoveOIOrderPOInfo(Guid id, Guid companyID, Guid removeByID, DateTime? updateDate); 
        #endregion
    }
}
