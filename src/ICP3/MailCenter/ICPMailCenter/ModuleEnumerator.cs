using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.Configuration;
using Microsoft.Practices.CompositeUI.Services;
namespace ICPMailCenter
{
    /// <summary>
    /// 邮件中心模块加载器
    /// </summary>
    public class ModuleEnumerator : IModuleEnumerator
    {
        private List<string> _Roles;
        public List<string> Roles
        {
            get { return _Roles ?? (_Roles = new List<string>()); }
        }
        IModuleInfo[] modules;
        public IModuleInfo[] EnumerateModules()
        {
            if (modules == null)
            {
                List<IModuleInfo> list = new List<IModuleInfo>();
                //"ICP.MailCenter.UI.dll,ICP.MailCenter.Business.UI.dll"
                string[] moduleNames = ClientHelper.GetAppSettingValue(ClientConstants.FilterModulesKey).Split(','); //应用程序配置文件中读取指定键值
                foreach (string moduleName in moduleNames)
                {                    
                    list.Add(new CABModuleInfo(Roles, moduleName.Trim(), string.Empty));
                }
                modules = list.ToArray();
            }
            return modules;
        }
    }
}
