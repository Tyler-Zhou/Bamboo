using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface.DataObjects;

namespace ICP.ReportCenter.UI.Comm.Controls
{  
    /// <summary>
    /// CRM部门下拉框
    /// </summary>
    public class CRMCompanyComboBox : ICP.Framework.ClientComponents.Controls.TreeSelectBox
    {
        private ReportCenterHelper ReportCenterHelper
        {
            get
            {
                return ClientHelper.Get<ReportCenterHelper, ReportCenterHelper>();
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
            {
                AddCompanyItems();
            }
        }
        private void AddCompanyItems()
        {
            List<OrganizationList> companys = ReportCenterHelper.CrmOrganizationList;

            this.SetSource<OrganizationList>(companys, LocalData.IsEnglish ? "EShortName" : "CShortName");
            this.EditValue = Utility.UserDefaultDepartmentID;
        }
    }
}
