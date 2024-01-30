#region Comment

/*
 * 
 * FileName:    InquireRatesShipmentPart.cs
 * CreatedOn:   2014/7/4 14:56:54
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

using ICP.Framework.CommonLibrary.Client;
using ICP.Operation.Common.UI;
using Microsoft.Practices.CompositeUI;

namespace ICP.FRM.UI.InquireRates
{
    /// <summary>
    /// 
    /// </summary>
    public partial class InquireRatesShipmentPart : ListBaseBusinessPart
    {
        #region 服务注入
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public  WorkItem Workitem { get; set; }

        /// <summary>
        /// Root Work Item
        /// </summary>
        public new WorkItem RootWorkItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }


        #endregion
        /// <summary>
        /// 
        /// </summary>
        public InquireRatesShipmentPart()
        {
            InitializeComponent();
        }
    }
}
