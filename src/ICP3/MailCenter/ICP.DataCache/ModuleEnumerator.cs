using System.Collections.Generic;
using System.Linq;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.Configuration;
using Microsoft.Practices.CompositeUI.Services;
namespace ICP.DataCache
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
                //"ICP.DataOperation.SqlCE.dll,ICP.DataCache.LocalOperation.dll"
                string[] moduleNames = ClientHelper.GetAppSettingValue(ClientConstants.FilterModulesKey).Split(','); //应用程序配置文件中读取指定键值
                modules = moduleNames.Select(moduleName => new CABModuleInfo(Roles, moduleName.Trim(), string.Empty)).Cast<IModuleInfo>().ToArray();
            }
            return modules;
        }
    }
}
