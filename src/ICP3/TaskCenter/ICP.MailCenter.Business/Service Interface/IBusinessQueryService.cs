using System;
using System.ServiceModel;
using System.Data;
using ICP.Message.ServiceInterface;
using System.Xml.Serialization;

namespace ICP.Common.Business.ServiceInterface
{   
    /// <summary>
    /// 业务查询接口
    /// </summary>
    [ServiceContract]
    [XmlSerializerFormat]
    [XmlSerializerAssembly(AssemblyName = "ICP.Common.Business.ServiceInterface.XmlSerializers.dll")]
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
      public DataTable Dt {get;set;} 
        /// <summary>
        /// 当前查询的邮件和业务关联信息
        /// </summary>
      public OperationMessageRelation Relation{get;set;}
    }
}
