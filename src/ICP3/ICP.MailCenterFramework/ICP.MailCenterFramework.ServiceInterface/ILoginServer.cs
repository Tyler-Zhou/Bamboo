/**
 *  创建时间:2014-07-09
 *  创建人:Joabwang    
 *  描  述:数据库通信实现接口
 **/
using System.ServiceModel;

namespace ICP.MailCenterFramework.ServiceInterface
{
    /// <summary>
    /// 数据库访问接口
    /// </summary>
    public interface ILoginServer
    {
        /// <summary>
        /// 判断当前是否需要进行登录操作
        /// </summary>
        /// <returns>是否需要登录</returns>
        [OperationContract]
        bool IsLogin();
    }
}
