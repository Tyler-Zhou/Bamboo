using System.ServiceModel;
using ICP.Framework.CommonLibrary.Attributes;
using System.Collections.Generic;

namespace ICP.MailCenter.ServiceInterface
{    
    /// <summary>
    /// 邮件中心管理服务接口
    /// </summary>
    [EmailCenterServiceHost]
   [ServiceContract]
   public interface IMailCenterManageService
    {  
        /// <summary>
        /// 关闭邮件中心
        /// </summary>
       [OperationContract]
       void Exit();
        [OperationContract]
       void SetSkin(string skinName);

        /// <summary>
        /// 移除内存缓存的联系人类型信息
        /// </summary>
        /// <param name="emailAddresses"></param>
       [OperationContract]
        void RemoveMemCacheContacts(List<string> emailAddresses);
    }
}
