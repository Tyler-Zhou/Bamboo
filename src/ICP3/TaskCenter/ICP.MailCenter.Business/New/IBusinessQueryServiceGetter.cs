﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.Operation.Common.ServiceInterface
{
   public interface IBusinessQueryServiceGetter
    {
       object Query(BusinessQueryCriteria criteria,object parameter);
    }
}
