using System.Collections.Generic;

namespace ICP.Operation.Common.ServiceInterface
{  
    /// <summary>
    /// 默认列获取器,返回空集合
    /// </summary>
   public class DefaultCustomColumnGetter:ICustomColumnGetter
    {
        #region ICustomColumnGetter 成员

        public List<BusinessColumnInfo> Get()
        {
            return new List<BusinessColumnInfo>();
        }

        #endregion
    }
}
