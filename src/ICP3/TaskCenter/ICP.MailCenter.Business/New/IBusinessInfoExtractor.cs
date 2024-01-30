using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.DataCache.ServiceInterface;

namespace ICP.Operation.Common.ServiceInterface
{  
    /// <summary>
    /// 业务信息抽取接口
    /// </summary>
   public interface IBusinessInfoExtractor
    {
      BusinessOperationContext Extract(object parameter);
    }
}
