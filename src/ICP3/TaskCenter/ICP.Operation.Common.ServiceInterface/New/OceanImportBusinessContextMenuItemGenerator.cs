using System.Collections.Generic;
using System.Data;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 海进业务上下文菜单项产生器
    /// </summary>
    public class OceanImportBusinessContextMenuItemGenerator : IBusinessContextMenuItemGenerator
    {
        #region IBusinessContextMenuItemGenerator 成员

        public List<ContextMenuItemInfo> Get(DataRow row, string registerUISiteName)
        {
            return null;
        }

        #endregion
    }
}
