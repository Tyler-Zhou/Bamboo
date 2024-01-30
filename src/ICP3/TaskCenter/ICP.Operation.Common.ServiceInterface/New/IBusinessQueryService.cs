using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Data;
using ICP.Message.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 业务查询接口
    /// </summary>
    [ServiceContract]
    public interface IBusinessQueryService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable Get(BusinessQueryCriteria criteria);
        /// <summary>
        /// 数据查询，并获取邮件和现有业务的关联关系
        /// </summary>
        /// <param name="criteria">数据查询条件</param>
        /// <param name="messageID">邮件的MessageID特性值</param>
        /// <param name="messageReference">邮件的Reference特性值</param>
        /// <returns></returns>
        [OperationContract(Name = "GetDataAndRelationInfo")]
        BusinessQueryResult Get(BusinessQueryCriteria criteria, string messageID, string messageReference);
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable Save(List<BusinessSaveParameter> parameters);

        /// <summary>
        ///根据业务ID和业务类型批量获取业务信息
        /// </summary>
        /// <param name="operationIDs"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetOperationInfo(List<Guid> operationIDs, OperationType operationType);

        /// <summary>
        /// 返回联系人的邮箱地址信息
        /// </summary>
        /// <param name="operationId">业务号</param>
        /// <param name="values">搜索范围</param>
        /// <returns></returns>
        [OperationContract]
        string GetCustomerMailList(Guid operationId, string values);
    }
    /// <summary>
    /// 业务数据查询结果实体类
    /// </summary>
    [Serializable]
    public class BusinessQueryResult
    {
        /// <summary>
        /// 单件模式
        /// </summary>
        private static bool instance = false;
        private static BusinessQueryResult info = null;
        public static BusinessQueryResult CreateInstance()
        {
            if (!instance || info == null)
            {
                info = new BusinessQueryResult();
                instance = true;
                return info;
            }
            else
            {
                //邮件中心查询业务数据可能得到NULL的返回结果
                info.Dt = null;
                return info;
            }
        }

        /// <summary>
        /// 列表数据
        /// </summary>
        public DataTable Dt { get; set; }
        /// <summary>
        /// 当前查询的邮件和业务关联信息
        /// </summary>
        public List<OperationMessageRelation> Relations { get; set; }
    }
}
