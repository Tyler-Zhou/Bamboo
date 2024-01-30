using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Data;
using ICP.Message.ServiceInterface;
using System.Xml.Serialization;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 业务查询接口
    /// </summary>
    [ServiceContract]
    [XmlSerializerFormat]
    [XmlSerializerAssembly(AssemblyName = "ICP.Operation.Common.ServiceInterface.XmlSerializers.dll")]
    public interface IBusinessQueryService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [OperationContract]
        [XmlSerializerFormat]
        DataTable Get(BusinessQueryCriteria criteria);
        /// <summary>
        /// 数据查询，并获取邮件和现有业务的关联关系
        /// </summary>
        /// <param name="criteria">数据查询条件</param>
        /// <param name="message">当前消息</param>
        /// <returns></returns>
        [OperationContract(Name = "GetDataAndRelationInfo")]
        [XmlSerializerFormat]
        BusinessQueryResult Get(BusinessQueryCriteria criteria, ICP.Message.ServiceInterface.Message message);
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable Save(List<BusinessSaveParameter> parameters);

        /// <summary>
        /// 根据业务ID和业务类型获取缓存表整条业务记录
        /// </summary>
        /// <param name="operationId">业务Id</param>
        /// <param name="operationType">业务类型</param>
        /// <returns></returns>
        [OperationContract(Name = "GetByOperationIdAndOperationType")]
        [XmlSerializerFormat]
        DataTable Get(Guid operationId, OperationType operationType);

    }
    /// <summary>
    /// 业务数据查询结果实体类
    /// </summary>
    [Serializable]
    public class BusinessQueryResult
    {
        /// <summary>
        /// 列表数据
        /// </summary>
        public DataTable Dt { get; set; }
        /// <summary>
        /// 当前查询的邮件和业务关联信息
        /// </summary>
        public OperationMessageRelation Relation { get; set; }
    }
}
