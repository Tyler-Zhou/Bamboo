using System;
using System.Collections.Generic;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System.ServiceModel;

namespace ICP.Common.ServiceInterface
{
    /// <summary>
    /// 账单服务类
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IVerificationSheetService
    {
        #region 通过业务Id获取核销单列表

        /// <summary>
        /// 通过业务Id获取核销单列表
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [FunctionInfomation]  [OperationContract]
        List<VerificationSheet> GetVerificationSheetListByIds(Guid operationId);

        #endregion

        #region 通过查询面板输入条件获取核销单列表

        /// <summary>
        /// 通过查询面板输入条件获取核销单列表
        /// </summary>     
        [FunctionInfomation]  [OperationContract]
        List<VerificationSheet> GetVerificationSheetList(
                         string operationNO,     //业务号
                         string sheetNo,         //核销单号
                         string customer,        //经营单位
                         string expressNo,       //快递单号
                         bool? isFreightArrive,   //运费是否到帐
                         int maxRecords);
        #endregion

        #region 保存核销单

        /// <summary>
        /// 保存核销单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sheetNo">核销单号</param>
        /// <param name="operationId">业务Id</param>
        /// <param name="customerId">经营单位</param>
        /// <param name="receiptDate"></param>
        /// <param name="returnDate"></param>
        /// <param name="expressNo">快递单号</param>
        /// <param name="isFreightArrive"></param>
        /// <param name="remark"></param>
        /// <param name="saveByID"></param>
        /// <param name="updateDate"></param>
        /// <returns></returns>
        [FunctionInfomation]  [OperationContract]
        SingleResult SaveVerificationSheet(
            Guid id,
            string sheetNo,
            Guid operationId,
            Guid? customerId,
            DateTime? receiptDate,
            DateTime? returnDate,
            string expressNo,
            bool isFreightArrive,
            string remark,
            Guid saveByID,
            DateTime? updateDate);

        #endregion

        #region 删除核销单

        /// <summary>
        /// 删除核销单
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        [FunctionInfomation]  [OperationContract]
        void RemoveVerificationSheet(
                     Guid id,
                     Guid removeByID,
                     DateTime? updateDate);
        #endregion
    }
}