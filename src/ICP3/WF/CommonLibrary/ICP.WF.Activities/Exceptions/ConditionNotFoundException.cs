using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.WF.Activities
{
	public class ConditionNotFoundException:Exception
	{
         public ConditionNotFoundException()
        {
        }


         public ConditionNotFoundException(string message)
            : base(message)
        {
        }
	}
}
