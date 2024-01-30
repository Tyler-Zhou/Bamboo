using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.WF.Activities
{
    public class WorkflowPersistenceServiceNotFoundException : Exception
    {
        public WorkflowPersistenceServiceNotFoundException()
        {
        }


        public WorkflowPersistenceServiceNotFoundException(string message)
            : base(message)
        {
        }
    }
}
