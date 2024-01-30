using ICP.MailCenter.ServiceInterface;
using System.Windows.Forms;
using System.Collections.Generic;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
namespace ICPMailCenter
{   
    /// <summary>
    /// 邮件中心管理服务类
    /// 与ICP主程序同步设置
    /// </summary>
    public class MailCenterManageService : IMailCenterManageService
    {  

        public void Exit()
        {
            Application.Exit();
        }
        public void SetSkin(string skinName)
        {
            DevExpress.UserSkins.OfficeSkins.Register();
            if (!DevExpress.Skins.SkinManager.AllowFormSkins)
                DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(skinName);
        }
        /// <summary>
        /// 移除内存缓存的联系人类型信息
        /// </summary>
        /// <param name="emailAddresses"></param>
       public void RemoveMemCacheContacts(List<string> emailAddresses)
       {
           IClientBusinessContactService clientBusinessContactService = ServiceClient.GetClientService<IClientBusinessContactService>();
           clientBusinessContactService.RemoveMemCacheContacts(emailAddresses);
       }
                
    }
}
