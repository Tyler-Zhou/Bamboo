using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;
using System;
using System.Runtime.Serialization;
using ActionType = ICP.Common.ServiceInterface.DataObjects.ActionType;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 业务回调客户端功能实现接口
    /// </summary>
    public interface IBusinessOperationHandleService
    {
        /// <summary>
        /// 业务操作完成
        /// </summary>
        event EventHandler<CommonEventArgs<BusinessOperationParameter>> BusinessOperationCompleted;
        /// <summary>
        /// 转接给回调客户端实现类处理
        /// </summary>
        /// <param name="paramter"></param>
        void HandleBusinessOperation(BusinessOperationParameter paramter);
    }
    /// <summary>
    /// 邮件业务关联参数
    /// </summary>
    [Serializable]
    [KnownType(typeof(BusinessOperationContext))]
    [KnownType(typeof(Message.ServiceInterface.Message))]
    [KnownType(typeof(OperationMessageRelation))]
    public class BusinessOperationParameter
    {
        /// <summary>
        /// 
        /// </summary>
        public BusinessOperationContext Context { get; set; }

        /// <summary>
        /// 操作视图代码
        /// </summary>
        public string TemplateCode { get; set; }
        /// <summary>
        /// 动作类型 
        /// </summary>
        public ActionType ActionType { get; set; }
        /// <summary>
        /// 当前消息
        /// </summary>
        public Message.ServiceInterface.Message Message { get; set; }
        /// <summary>
        /// 消息与业务关联信息
        /// </summary>
        public OperationMessageRelation Relation { get; set; }
        /// <summary>
        /// 邮件阶段
        /// </summary>
        public ContactStage? ContactStage { get; set; }
    }
}
