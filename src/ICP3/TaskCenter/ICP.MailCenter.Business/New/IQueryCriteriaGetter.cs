using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.MailCenter.Business.ServiceInterface;

namespace ICP.Operation.Common.ServiceInterface
{ 
    /// <summary>
    /// 查询条件获取器接口
    /// </summary>
  public interface IQueryCriteriaGetter
    {
      BusinessQueryCriteria Get(IBaseBusinessPart_New basePart, object parameter, bool mergeAdvanceQueryString);
    }
}
