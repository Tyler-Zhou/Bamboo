using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface.DataObjects;

namespace ICP.ReportCenter.UI.Comm.Controls
{  
    /// <summary>
    /// 管理报表部门下拉树选择框
    /// </summary>
   public class BusinessCompanyTreeSelectBox:ICP.Framework.ClientComponents.Controls.TreeSelectBox
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
            List<OrganizationList> companys = ReportCenterHelper.GetUserOrganizationList;
            this.SetSource<OrganizationList>(companys, LocalData.IsEnglish ? "EShortName" : "CShortName");
            this.EditValue = LocalData.UserInfo.DefaultDepartmentID;
        }
    }
}
