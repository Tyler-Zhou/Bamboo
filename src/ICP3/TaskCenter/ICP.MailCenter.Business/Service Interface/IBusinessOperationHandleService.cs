using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.DataCache.ServiceInterface;
using System.Runtime.Serialization;
using ICP.Message.ServiceInterface;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 业务回调客户端功能实现接口
    /// </summary>
    public interface IBusinessOperationHandleService
    {
        event EventHandler<CommonEventArgs<BusinessOperationParameter>> BusinessOperationCompleted;
        void HandleBusinessOperation(BusinessOperationParameter paramter);
    }
    /// <summary>
    /// 邮件业务关联参数
    /// </summary>
    [Serializable]
    [KnownType(typeof(BusinessOperationContext))]
    public class BusinessOperationParameter
    {
        public BusinessOperationContext Context { get; set; }
        
        /// <summary>
        /// 操作视图代码
        /// </summary>
        public string TemplateCode { get; set; }
        /// <summary>
        /// 动作类型 
        /// </summary>
        public ICP.Common.ServiceInterface.DataObjects.ActionType ActionType { get; set; }
        /// <summary>
        /// 当前消息
        /// </summary>
        public Message.ServiceInterface.Message Message { get; set; }
        /// <summary>
        /// 消息与业务关联信息
        /// </summary>
        public Message.ServiceInterface.OperationMessageRelation Relation { get; set; }
    
        
    }
}
