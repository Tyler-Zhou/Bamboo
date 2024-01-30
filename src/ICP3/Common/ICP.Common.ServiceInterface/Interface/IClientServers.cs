using System.ServiceModel;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.Common.ServiceInterface
{
    /// <summary>
    /// 客户端方法接口
    /// </summary>
    [ServiceContract]
    [ICPServiceHost]
    public interface IClientServers
    {
        /// <summary>
        /// 根据条件生成当前跳转页面的参数
        /// </summary>
        /// <param name="go">实体对象</param>
        /// <param name="methods">方法名</param>
        /// <param name="checkBoxName">控件的名称(如果用到账单的方法需要传当前的控件名称)</param>
        /// <returns></returns>
        [OperationContract]
        object[] GetGotoparameters(GoToObject go, string methods, string checkBoxName);
    }
}
