using System.Collections.Generic;

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
