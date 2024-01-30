using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Common.ServiceInterface.DataObjects;

namespace ICP.ReportCenter.UI.Comm.Controls
{  
    /// <summary>
    /// 业务报表费用项目下拉树选择框
    /// </summary>
   public class CostItemTreeSelectBox:ICP.Framework.ClientComponents.Controls.TreeSelectBox
    {
        private ReportCenterHelper ReportCenterHelper
        {
            get
            {
                return ICP.Framework.CommonLibrary.Client.ClientHelper.Get<ReportCenterHelper, ReportCenterHelper>();
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!ICP.Framework.CommonLibrary.Client.LocalData.IsDesignMode)
            {
                AddCompanyItems();
            }
        }
        private void AddCompanyItems()
        {
            List<CostItemData> costItems = ReportCenterHelper.CostItemDatas;
            this.SetSource<CostItemData>(costItems, ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish ? "EName" : "CName");
            this.EditValue = ReportCenterHelper.TopCostItem.ID;
        }
    }
}
