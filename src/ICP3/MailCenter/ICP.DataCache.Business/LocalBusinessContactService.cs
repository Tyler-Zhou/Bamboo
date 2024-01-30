#region Comment

/*
 * 
 * FileName:    LocalBusinessContactService.cs
 * CreatedOn:   2015/6/2 15:33:49
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      ->
 * History：
 *      ->
 * 
 * 
 * 
 * 
 */

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.DataCache.LocalOperation
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class LocalBusinessContactService : ILocalBusinessContactService, IDisposable
    {
        static Dictionary<string, EmailSourceType> types = new Dictionary<string, EmailSourceType>();
        private object objLock = new object();
        private object objAdd = new object();
        private object objRemove = new object();
        public ILocalBusinessCacheDataOperation LocalCacheDataOperation
        {
            get
            {
                //return new LocalBusinessCacheDataOperation();
                return ServiceClient.GetClientService<ILocalBusinessCacheDataOperation>();
            }
        }

        public IBusinessContactService BusinessContactService
        {
            get
            {
                return ServiceClient.GetService<IBusinessContactService>();
            }
        }

        #region IDisposable 成员

        public void Dispose()
        {
            if (types != null)
            {
                types.Clear();
                types = null;
            }
        }

        #endregion

        /// <summary>
        /// 根据email获取OperationContact业务联系人信息
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        public List<OperationContactInfo> GetOperationContactByEmails(List<string> emails)
        {
            return BusinessContactService.GetOperationContactByEmails(emails);
        }

        /// <summary>
        /// 根据发件人地址获取发件人的类型
        /// </summary>
        /// <param name="senderAddress"></param>
        /// <returns></returns>
        public EmailSourceType GetEmailType(string senderAddress)
        {
            return GetEmailTypes(new List<string>() { senderAddress })[0];
        }

        /// <summary>
        /// 根据邮件所有外部的邮件获取联系人信息
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        public EmailSourceType[] GetEmailTypes(List<string> emails)
        {
            if (emails == null || emails.Count <= 0)
                return null;
            if (emails.Count == 1)
            {
                if (string.IsNullOrEmpty(emails[0]))
                    return new EmailSourceType[1] { EmailSourceType.Unknown };
                if (!emails[0].Contains("@"))
                    return new EmailSourceType[1] { EmailSourceType.Unknown };
            }

            List<EmailSourceType> sourceTypes = new List<EmailSourceType>();
            //将缓存集合中的EmailAddress直接找出来，没有找到的就存本地缓存表中去查找
            List<string> unsoughtEmails = new List<string>();
            emails.ForEach(item =>
            {
                string lowerEmail = item.ToLower();
                if (types.ContainsKey(lowerEmail))
                    sourceTypes.Add(types[lowerEmail]);

                else
                    unsoughtEmails.Add(lowerEmail);
            });

            int count = unsoughtEmails.Count;
            if (count > 0)
            {
                EmailSourceType[] newSourceTypes = GetEmailSourceTypes(unsoughtEmails, count);
                AddItemToDictionary(count, unsoughtEmails, newSourceTypes);

                sourceTypes.AddRange(newSourceTypes);
            }

            return sourceTypes.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public OperationContactInfo GetOperationContactInfo(string email)
        {
            OperationContactInfo contactInfo = BusinessContactService.GetOperationContactInfo(email);

            return contactInfo;
        }
        /// <summary>
        /// 移除内存缓存的联系人类型信息
        /// </summary>
        /// <param name="emailAddresses"></param>
        public void RemoveMemCacheContacts(List<string> emailAddresses)
        {
            lock (objRemove)
            {
                foreach (string emailAddress in emailAddresses)
                {
                    string temp = emailAddress.ToLower();
                    if (types.ContainsKey(temp))
                    {
                        types.Remove(temp);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactList"></param>
        public void LocalSaveOperationContacts(List<OperationContactInfo> contactList)
        {
            if (contactList == null || contactList.Count <= 0)
            {
                return;
            }
            List<string> mails = (from item in contactList select item.Mail).ToList();
            RemoveMemCacheContacts(mails);
            LocalCacheDataOperation.SaveContacts(contactList);
        }

        /// <summary>
        ///  获取所有联系人信息(同步ICP通讯录)
        /// </summary>
        /// <returns>邮件联系人列表</returns>
        public List<EmailContactInfo> GetEmailContactList()
        {
            return BusinessContactService.GetEmailContactList();
        }


        /// <summary>
        /// 将int类型转换成EmailSourceType
        /// </summary>
        /// <param name="emails"></param>
        /// <param name="emailCount"></param>
        /// <returns></returns>
        private EmailSourceType[] GetEmailSourceTypes(List<string> emails, int emailCount)
        {
            var sourceTypes = new EmailSourceType[emailCount];
            lock (objLock)
            {
                try
                {
                    for (int i = 0; i < emailCount; i++)
                    {
                        int? type = LocalCacheDataOperation.GetContactPersonType(emails[i]);
                        if (type.HasValue)
                            sourceTypes[i] = (EmailSourceType)type;
                        else
                        {
                            //当本地缓存表没有查找到该联系人，需要到服务端数据库确认该联系人是否存在
                            OperationContactInfo contactInfo = GetOperationContactInfo(emails[i]);
                            EmailSourceType sourceType = GetEmailSourceType(contactInfo);
                            if (sourceType != EmailSourceType.Unknown)
                            {
                                LocalCacheDataOperation.SaveContacts(new List<OperationContactInfo> { contactInfo });
                            }

                            sourceTypes[i] = sourceType;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                }
            }
            return sourceTypes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactInfo"></param>
        /// <returns></returns>
        private EmailSourceType GetEmailSourceType(OperationContactInfo contactInfo)
        {
            EmailSourceType type = EmailSourceType.Unknown;
            if (contactInfo.Customer)
            {
                type = EmailSourceType.Customer;
            }
            if (contactInfo.Carrier)
            {
                type = type | EmailSourceType.Shipper;
            }
            return type;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailCount"></param>
        /// <param name="emails"></param>
        /// <param name="emailSourceTypes"></param>
        private void AddItemToDictionary(int emailCount, List<string> emails, EmailSourceType[] emailSourceTypes)
        {
            lock (objAdd)
            {
                for (int i = 0; i < emailCount; i++)
                {
                    if (!types.ContainsKey(emails[i]))
                        types[emails[i]] = emailSourceTypes[i];
                }
            }
        }
    }
}
