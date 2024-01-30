using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Common;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.DataCache.ServiceInterface;
using System.Runtime.Serialization;

namespace ICP.MailCenter.Business.ServiceInterface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBusinessOperationHandleService
    {
        event EventHandler<CommonEventArgs<BusinessOperationParameter>> BusinessOperationCompleted;
        void HandleBusinessOperation(BusinessOperationParameter paramter);
    }
    /// <summary>
    /// 邮件业务关联参数
    /// </summary>
    [DataContract]
    [KnownType(typeof(BusinessOperationParameter))]
    public class BusinessOperationParameter
    {
        [DataMember]
        public BusinessOperationContext Context { get; set; }
        [DataMember]
        public ListFormType BusinessPartType { get; set; }
        /// <summary>
        ///文档列表文件名集合
        /// </summary>
        [DataMember]
        public List<string> FilesName { get; set; }
        /// <summary>
        /// 动作类型 
        /// </summary>
        [DataMember]
        public ActionType ActionType { get; set; }
        [DataMember]
        public string SenderEmailAddress { get; set; }
        [DataMember]
        public Message.ServiceInterface.Message Message { get; set; }
        [DataMember]
        public object[] Data { get; set; }
    }
}
