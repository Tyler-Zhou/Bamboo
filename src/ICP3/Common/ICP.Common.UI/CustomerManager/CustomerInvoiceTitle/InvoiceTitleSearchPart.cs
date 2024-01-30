using System;
using System.Collections.Generic;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Common.UI.CustomerManager
{
    public partial class InvoiceTitleSearchPart : BaseSearchPart
    {
        public InvoiceTitleSearchPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.OnSearched = null;
                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
            
            };
        }

        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        public ICP.Sys.ServiceInterface.IOrganizationService OrganizationService
        {
            get
            {
                return ServiceClient.GetService<ICP.Sys.ServiceInterface.IOrganizationService>();
            }
        }
        #endregion

        #region 清空
        private void btnClaer_Click(object sender, EventArgs e)
        {
            this.txtTaxNo.Text = string.Empty;
            this.txtName.Text = string.Empty;
            this.mcbCreateBy.EditValue = null;
            this.mcbCreateBy.EditText = string.Empty;
        }
        #endregion

        #region 重写
        public override void RaiseSearched()
        {
            this.btnSearch.PerformClick();
        }
        #endregion

        #region 初始化
        public override void Init(IDictionary<string, object> values)
        {
            if (values != null && values.Keys.Contains("CustomerName"))
            {
                this.txtName.Text = values["CustomerName"].ToString();
            }

            List<OrganizationList> orgList=OrganizationService.GetOfficeList();

            Dictionary<string, string> col = new Dictionary<string, string>();
            col.Add("CShortName", "名称");
            this.mcbCreateBy.InitSource<OrganizationList>(orgList, col, "CShortName", "CShortName");
            //this.mcbCreateBy.InitSource<OrganizationList>(orgList, col, "CShortName", "ID");

            this.mcbCreateBy.ShowSelectedValue(LocalData.UserInfo.DefaultCompanyName, LocalData.UserInfo.DefaultCompanyName);
            //this.mcbCreateBy.ShowSelectedValue(LocalData.UserInfo.DefaultCompanyID, LocalData.UserInfo.DefaultCompanyName);

        }
        #endregion

        #region 查询
        public override event SearchResultHandler OnSearched;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (OnSearched != null)
            {
                using (new CursorHelper())
                {
                    List<CustomerInvoiceTitleInfo> invoiceTitleList = CustomerService.GetCustomerInvoiceTitleListForFinder(this.txtTaxNo.Text, this.txtName.Text, this.mcbCreateBy.EditText);

                    OnSearched(this, invoiceTitleList);
                }
            }
        }

        #endregion
    }
}
