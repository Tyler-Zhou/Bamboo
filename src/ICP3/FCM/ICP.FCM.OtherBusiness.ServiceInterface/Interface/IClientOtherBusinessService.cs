using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;

namespace ICP.FCM.OtherBusiness.ServiceInterface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IClientOtherBusinessService
    {
        #region Order / Business
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="addType"></param>
        /// <param name="editPartSaved"></param>
        void AddData(AddType addType, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="addType">新增类型(其他业务新增、新增订单)</param>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">业务口岸ID</param>
        /// <param name="editPartSaved">数据保存后执行事件</param>
        void CopyData(AddType addType, Guid operationId, Guid companyID, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="addType">新增类型(其他业务新增、新增订单)</param>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">业务口岸ID</param>
        /// <param name="editPartSaved">数据保存后执行事件</param>
        void EditData(AddType addType, Guid operationId, Guid companyID, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 取消业务/恢复业务
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">业务口岸ID</param>
        /// <param name="editPartSaved">数据保存后执行事件</param>
        void CancelData(Guid operationId, Guid companyID, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 打开账单
        /// </summary>
        /// <param name="operationId"></param>
        void Bill(Guid operationId);

        /// <summary>
        /// 核销单
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">业务口岸ID</param>
        void VerifiSheet(Guid operationId, Guid companyID);

        /// <summary>
        /// 提货通知书
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">业务口岸ID</param>
        void PickUp(Guid operationId, Guid companyID);

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">业务口岸ID</param>
        void PrintOrder(Guid operationId, Guid companyID);

        /// <summary>
        /// 利润表
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">业务口岸ID</param>
        void Profit(Guid operationId, Guid companyID); 
        #endregion

        #region 电商物流(E-Commerce Logistics)

        /// <summary>
        /// 电商物流-新增
        /// </summary>
        /// <param name="editPartSaved">新增类型</param>
        void ECommerceAddData(IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 电商物流-复制
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">业务口岸ID</param>
        /// <param name="editPartSaved">数据保存后执行事件</param>
        void ECommerceCopyData(Guid operationId, Guid companyID, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 电商物流-编辑
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">业务口岸ID</param>
        /// <param name="editPartSaved">数据保存后执行事件</param>
        void ECommerceEditData(Guid operationId, Guid companyID, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 电商物流-取消业务/恢复业务
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">业务口岸ID</param>
        /// <param name="editPartSaved">数据保存后执行事件</param>
        void ECommerceCancelData(Guid operationId, Guid companyID, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 其他业务-核销单
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="NO">业务口岸ID</param>
        void VerifiSheet(Guid operationId, string NO);

        #endregion
    }
}
