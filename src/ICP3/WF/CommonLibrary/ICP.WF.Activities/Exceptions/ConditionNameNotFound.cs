using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.WF.Activities
{
    public	class ConditionNameNotFoundException:Exception
	{
        public ConditionNameNotFoundException()
        {
        }


        public ConditionNameNotFoundException(string message)
            : base(message)
        {
        }
	}
}
