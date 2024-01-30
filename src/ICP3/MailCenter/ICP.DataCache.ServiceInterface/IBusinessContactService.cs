using System.Collections.Generic;
using System.ServiceModel;
namespace ICP.DataCache.ServiceInterface
{
    /// <summary>
    /// 业务联系人信息服务接口
    /// </summary>
    [ServiceContract]
    public interface IBusinessContactService
    {

        /// <summary>
        /// 根据email获取OperationContact业务联系人信息
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        [OperationContract]
        List<OperationContactInfo> GetOperationContactByEmails(List<string> emails);
        /// <summary>
        /// 获取业务联系人信息
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [OperationContract]
        OperationContactInfo GetOperationContactInfo(string email);

        /// <summary>
        /// 获取所有邮件联系人对象
        /// </summary>
        /// <returns>邮件联系人对象集合</returns>
        [OperationContract]
        List<EmailContactInfo> GetEmailContactList();
    }
}