using System;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.DataCache.ServiceInterface
{   
    /// <summary>
    /// 用户列表自定义服务接口
    /// <remarks>用户自定义列表的显示。可以通过全局变量LocalData.EnableCustomDataGrid禁用此功能</remarks>
    /// </summary>
    [ServiceContract]
   public interface ICustomDataGridService
    {    
        /// <summary>
        /// 保存用户自定义列表显示信息
        /// </summary>
        /// <param name="customInfo"></param>
        /// <returns></returns>
        [OperationContract]
         SingleResult Save(UserCustomGridInfo customInfo);

        /// <summary>
        /// 得到用户定义列(如果获取失败，则获取默认模板)
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="templateCode">视图代码</param>
        /// <returns></returns>
        [OperationContract]
        UserCustomGridInfo Get(Guid? userId, string templateCode);
       
    }
}
