using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using System.Collections.Generic;

namespace ICP.DataCache.BusinessOperation
{
    public class ClientBusinessContactService : IClientBusinessContactService
    {
        public ILocalBusinessContactService LocalBusinessContactService
        {
            get
            {
                return ServiceClient.GetClientService<ILocalBusinessContactService>();
            }
        }

        #region IClientBusinessContactService 成员
        /// <summary>
        /// 根据email获取OperationContact业务联系人信息
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        public List<OperationContactInfo> GetOperationContactByEmails(List<string> emails)
        {
            return LocalBusinessContactService.GetOperationContactByEmails(emails);
        }
        public EmailSourceType GetEmailType(string senderAddress)
        {
            return LocalBusinessContactService.GetEmailTypes(new List<string>() { senderAddress })[0];
        }

        public EmailSourceType[] GetEmailTypes(List<string> emails)
        {
            return LocalBusinessContactService.GetEmailTypes(emails);

        }

        public OperationContactInfo GetOperationContactInfo(string email)
        {
            return LocalBusinessContactService.GetOperationContactInfo(email);
        }
        /// <summary>
        /// 移除内存缓存的联系人类型信息
        /// </summary>
        /// <param name="emailAddresses"></param>
        public void RemoveMemCacheContacts(List<string> emailAddresses)
        {
            LocalBusinessContactService.RemoveMemCacheContacts(emailAddresses);
        }

        #endregion

        public void LocalSaveOperationContacts(List<OperationContactInfo> contactList)
        {
            LocalBusinessContactService.LocalSaveOperationContacts(contactList);
        }

        #region IBusinessContactService 成员

        /// <summary>
        ///  获取所有联系人信息(同步ICP通讯录)
        /// </summary>
        /// <returns>邮件联系人列表</returns>
        public List<EmailContactInfo> GetEmailContactList()
        {
            return LocalBusinessContactService.GetEmailContactList();
        }

        #endregion
    }
}
