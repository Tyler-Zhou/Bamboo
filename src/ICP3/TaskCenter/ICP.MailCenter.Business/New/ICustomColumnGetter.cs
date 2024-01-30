using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.Operation.Common.ServiceInterface
{  
    /// <summary>
    /// 用户自定义列获取接口
    /// </summary>
   public interface ICustomColumnGetter
    {
       List<BusinessColumnInfo> Get();
    }
}
