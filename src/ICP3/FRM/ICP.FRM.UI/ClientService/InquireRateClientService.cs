#region Comment

/*
 * 
 * FileName:    InquireRateClientService.cs
 * CreatedOn:   2014/10/21 21:12:23
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      ->
 * History：
 *      ->
 * 
 * 
 * 
 * 
 */

#endregion

using System;
using System.Windows.Forms;
using ICP.FRM.ServiceInterface;
using Microsoft.Practices.CompositeUI;

namespace ICP.FRM.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class InquireRateClientService : IInquireRateClientService
    {
        [ServiceDependency]
        public WorkItem WorkItem { get; set; }
        TaskCenterInquireRateWorkSpace MainWorkSpace { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewCode"></param>
        /// <param name="strQuery"></param>
        /// <returns></returns>
        public Control GetInquireRateWorkSpace(string viewCode, string strQuery)
        {
            MainWorkSpace = WorkItem.Items.AddNew<TaskCenterInquireRateWorkSpace>(Guid.NewGuid().ToString());
            MainWorkSpace.Workitem = WorkItem;
            MainWorkSpace.ViewCode = viewCode;
            MainWorkSpace.StrQuery = strQuery;

            return MainWorkSpace;
        }
    }
}
