namespace ICP.FCM.Common.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Common;
    using CommonData = ICP.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Client;
    using System.ServiceModel;

    /// <summary>
    /// 业务公共信息接口
    /// </summary>
    [ServiceInfomation("业务公共信息接口")]
    [ServiceContract]
    public interface IOperationCommonService
    {
        /// <summary>
        /// 获取业务公共信息
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType">表单类型</param>
        /// <returns>返回业务公共信息</returns>
        [FunctionInfomation]
        [OperationContract]
        OperationCommonInfo GetOperationCommonInfo(Guid operationID, OperationType operationType);
    }
}
