using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ICP.Operation.Common.ServiceInterface
{  
    /// <summary>
    /// 业务上下文菜单项产生器接口
    /// </summary>
   public interface IBusinessContextMenuItemGenerator
    {
       List<ContextMenuItemInfo> Get(DataRow row, string registerUISiteName);
    }
}
