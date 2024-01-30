
using System.Collections.Generic;
using System.Linq;
using ICP.DataCache.ServiceInterface;

namespace ICP.MailCenterFramework.UI
{
    /// <summary>
    /// 联系人控制器
    /// </summary>
    public class OperationContactController
    {
        /// <summary>
        /// 通过邮箱地址获取联系人
        /// </summary>
        /// <param name="mailAddress">邮箱地址</param>
        /// <returns>联系人集合</returns>
        public List<OperationContactInfo> GetOperationContactListByMailAddress(string mailAddress)
        {
            return HelpMailStore.TableOperationContact.Where(ocItem => (string.IsNullOrEmpty(ocItem.Mail) ? "" : ocItem.Mail).ToUpper().Equals(mailAddress.ToUpper())).ToList();
        }

        /// <summary>
        /// 通过邮箱地址获取联系人
        /// </summary>
        /// <param name="mailAddress">邮箱地址</param>
        /// <returns>单个联系人</returns>
        public OperationContactInfo GetOperationContactSingleByMailAddress(string mailAddress)
        {
            return HelpMailStore.TableOperationContact.SingleOrDefault(ocItem => (string.IsNullOrEmpty(ocItem.Mail) ? "" : ocItem.Mail).ToUpper().Equals(mailAddress.ToUpper()));
        }
    }
}
