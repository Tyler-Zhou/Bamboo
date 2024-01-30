using ICP.WF.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ICP.WF.UI
{
    public class TaskWorkClientService : ITaskWorkClientService
    {
        [ServiceDependency]
        public WorkItem WorkItem { get; set; }
        TaskWorkListMainWorkSpace MainWorkSpace { get; set; }

        public Control TaskGetWorkList(string viewCode,string strQuery)
        {
            MainWorkSpace = WorkItem.Items.AddNew<TaskWorkListMainWorkSpace>(Guid.NewGuid().ToString());
            MainWorkSpace.ViewCode = viewCode;
            MainWorkSpace.StrQuery = strQuery;

            return MainWorkSpace;
        }
    }
}
