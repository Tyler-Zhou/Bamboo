using System;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Attributes;


namespace ICP.DataCache.ServiceInterface
{    
    /// <summary>
    /// 客户端用户列表显示自定义服务接口
    /// </summary>
    [ServiceContract]
    [ICPServiceHost]
   public interface IClientCustomDataGridService
    {  
        /// <summary>
        /// 保存用户列表自定义显示信息
        /// </summary>
        /// <param name="customInfo"></param>
        [OperationContract]
        void Save(UserCustomGridInfo customInfo);
        /// <summary>
        /// 获取用户列表自定义显示信息
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="listType">列表类型</param>
        /// <returns></returns>
        [OperationContract]
        UserCustomGridInfo Get(Guid userId, string templateCode);
        /// <summary>
        /// 获取用户列表自定义显示信息
        /// </summary>
        /// <param name="listType">列表类型</param>
        /// <returns></returns>
        [OperationContract(Name="GetByCurrentLoginUserId")]
        UserCustomGridInfo Get(string templateCode);
    }
}
