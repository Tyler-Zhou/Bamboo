using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.Operation.Common.ServiceInterface
{  
    /// <summary>
    /// 默认数据绑定后处理器
    /// <remarks>不做任何处理动作</remarks>
    /// </summary>
   public class DefaultPostDataBindHandler:IPostDataBindHandler
    {
        #region IPostDataBindHandler 成员

        public void PostHandle(IBaseBusinessPart_New businessPart, object result, object parameter)
        {
          
        }

        #endregion
    }
}
