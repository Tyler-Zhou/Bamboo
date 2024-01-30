using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.WF.Activities
{
    public class WorkFlowServiceNotFoundException : Exception
    {
        public WorkFlowServiceNotFoundException()
        {
        }


        public WorkFlowServiceNotFoundException(string message)
            : base(message)
        {
        }
    }
}
