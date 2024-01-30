using System;
using System.Collections.Generic;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Common;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FileSystem.ServiceInterface;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    ///业务操作回调接口
    /// </summary>
    [ServiceKnownType(typeof(DocumentInfo[]))]
    [ServiceKnownType(typeof(ManyResult))]
    [ServiceKnownType(typeof(SingleResult))]
    [ServiceKnownType(typeof(List<SingleResult>))]
    [ServiceKnownType(typeof(Guid[]))]
    [ServiceKnownType(typeof(SingleResult))]
    [ServiceKnownType(typeof(BusinessOperationParameter))]
    [ServiceKnownType(typeof(OceanBookingInfo))]
    public interface IBusinessOperationCallbackService
    {   
        /// <summary>
        /// 业务操作完成后，调用邮件中心和任务中心相关联处理动作
        /// </summary>
        ///<param name="parameter"></param>
        [OperationContract(IsOneWay = true)]
        void HandleBusinessOperation(BusinessOperationParameter parameter);
        /// <summary>
        /// 文档上传后回调处理动作
        /// </summary>
        /// <param name="type"></param>
        /// <param name="data"></param>
        /// <param name="generateIds"></param>
        [OperationContract(IsOneWay=true)]
        void Upload(NotifyType type, object data, object generateIds);
    }
}
