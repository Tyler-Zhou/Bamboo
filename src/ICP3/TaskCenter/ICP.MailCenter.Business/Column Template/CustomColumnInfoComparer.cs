using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.DataCache.ServiceInterface;

namespace ICP.Common.Business.ServiceInterface
{
    /// <summary>
    /// 列比较器 
    /// </summary>   
    public class CustomColumnInfoComparer : IComparer<CustomColumnInfo>
    {
        #region IComparer<CustomColumnInfo> 成员

        public int Compare(CustomColumnInfo x, CustomColumnInfo y)
        {
            if (x == y) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            return x.VisibleIndex.CompareTo(y.VisibleIndex);
        }

        #endregion
    }
}
