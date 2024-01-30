#region Comment

/*
 * 
 * FileName:    InquireOceanRatesShipmentPart.cs
 * CreatedOn:   2014/7/4 14:58:51
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

using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.FRM.ServiceInterface;
using System.Data;
using ICP.Operation.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;

namespace ICP.FRM.UI.InquireRates
{
    public partial class InquireOceanRatesShipmentPart : BaseEditPart, IInquierRateDataBind
    {
        #region 服务注入

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        /// <summary>
        /// Root Work Item
        /// </summary>
        public WorkItem RootWorkItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }

        
        #endregion

        /// <summary>
        /// 船公司
        /// </summary>
        public BusinessQueryCriteria Criteria { get; set; }

        public InquireOceanRatesShipmentPart()
        {
            InitializeComponent();
        }
        /// <summary>
        /// DataBind
        /// </summary>
        /// <param name="data"></param>
        public void DataSourceBind(object data)
        {
            shipmentPart.baseBindingSource = null;
            ClientInquierOceanRate currentData = data as ClientInquierOceanRate;
            if (currentData == null)
                return;

            currentData.ViewCode = UIConstants.ShipmentTemplateCode;
            currentData.AdvanceQueryString = "$@InquirePriceOceanID@ =  '" + currentData.ID + "'";
            currentData.OperationType = OperationType.OceanExport;
            currentData.TopCount = 0;

            object result;
            Criteria = shipmentPart.GetQueryCriteria(!string.IsNullOrEmpty(currentData.AdvanceQueryString));
            result = shipmentPart.Getter.Query(Criteria, currentData);
            DataTable dt = shipmentPart.GetInnerTable(result);
            shipmentPart.SetDataSource(dt);
        }
    }
}
