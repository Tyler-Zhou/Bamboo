#region Comment

/*
 * 
 * FileName:    ILocalBusinessContactService.cs
 * CreatedOn:   2015/6/2 15:26:36
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

using System.Collections.Generic;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.DataCache.ServiceInterface
{
    /// <summary>
    /// 
    /// </summary>
    [ICPServiceHost]
    [ServiceContract]
    public interface ILocalBusinessContactService
    {
        /// <summary>
        /// 根据发件人地址获取发件人的类型
        /// </summary>
        /// <param name="senderAddress"></param>
        /// <returns></returns>
        [OperationContract]
        EmailSourceType GetEmailType(string senderAddress);

        /// <summary>
        /// 根据邮件所有外部的邮件获取联系人信息
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        [OperationContract]
        EmailSourceType[] GetEmailTypes(List<string> emails);

        /// <summary>
        /// 根据email从服务端获取OperationContact业务联系人信息
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        [OperationContract]
        List<OperationContactInfo> GetOperationContactByEmails(List<string> emails);
        /// <summary>
        /// 从服务端获取业务联系人信息
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [OperationContract]
        OperationContactInfo GetOperationContactInfo(string email);
        /// <summary>
        /// 保存业务联系人信息到本地缓存
        /// </summary>
        /// <param name="contactList"></param>
        [OperationContract]
        void LocalSaveOperationContacts(List<OperationContactInfo> contactList);
        /// <summary>
        /// 移除内存缓存的联系人类型信息
        /// </summary>
        /// <param name="emailAddresses"></param>
        [OperationContract]
        void RemoveMemCacheContacts(List<string> emailAddresses);

        /// <summary>
        /// 获取所有邮件联系人对象
        /// </summary>
        /// <returns>邮件联系人对象集合</returns>
        [OperationContract]
        List<EmailContactInfo> GetEmailContactList();
    }
}
