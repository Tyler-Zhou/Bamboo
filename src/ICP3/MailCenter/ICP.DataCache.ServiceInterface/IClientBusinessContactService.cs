using System.Collections.Generic;

namespace ICP.DataCache.ServiceInterface
{
    /// <summary>
    /// 业务联系信息服务客户端接口
    /// </summary>

    public interface IClientBusinessContactService : IBusinessContactService
    {
        /// <summary>
        /// 根据发件人地址获取发件人的类型
        /// </summary>
        /// <param name="senderAddress"></param>
        /// <returns></returns>

        EmailSourceType GetEmailType(string senderAddress);

        /// <summary>
        /// 根据邮件所有外部的邮件获取联系人信息
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        EmailSourceType[] GetEmailTypes(List<string> emails);

        /// <summary>
        /// 根据email从服务端获取OperationContact业务联系人信息
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        List<OperationContactInfo> GetOperationContactByEmails(List<string> emails);
        /// <summary>
        /// 从服务端获取业务联系人信息
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        OperationContactInfo GetOperationContactInfo(string email);
        /// <summary>
        /// 保存业务联系人信息到本地缓存
        /// </summary>
        /// <param name="operationType"></param>
        /// <param name="contactList"></param>
        void LocalSaveOperationContacts(List<OperationContactInfo> contactList);
        /// <summary>
        /// 移除内存缓存的联系人类型信息
        /// </summary>
        /// <param name="emailAddresses"></param>
        void RemoveMemCacheContacts(List<string> emailAddresses);
    }
}
