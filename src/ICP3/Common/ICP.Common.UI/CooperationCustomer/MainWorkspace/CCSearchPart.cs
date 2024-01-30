using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface.DataObjects;

namespace ICP.Common.UI.CC
{
    [ToolboxItem(false)]
    public partial class CCSearchPart : ICP.Framework.ClientComponents.UIFramework.BaseSearchPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }

        public ICP.Sys.ServiceInterface.IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<ICP.Sys.ServiceInterface.IUserService>();
            }
        }

        #endregion

        #region  init

        public CCSearchPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.OnSearched = null;
                CommonUtility.RemoveSearchPartKeyEnterToSearch(new List<Control>  {txtName }, this.KeyEventHandle);
                this.mcmbSales.Enter -= this.OnSalesFirstTimeEnter;
                
                this.cmbShipLine.OnFirstEnter -= this.OnShipLineFirstEnter;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };

            if (!DesignMode) { InitMessage(); }
        }
        private void InitMessage()
        {
            this.RegisterMessage("SearchBoxToolTip", "Please input Code, Chinese name or English name.");
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            CommonUtility.SearchPartKeyEnterToSearch(new List<Control> 
            {
                txtName
            }, this.btnSearch,this.KeyEventHandle);
            InitControls();
        }
        private void KeyEventHandle(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnSearch.PerformClick();
        }
        private void InitControls()
        {
            SetCmbSource();
        }
        List<ICP.Framework.CommonLibrary.Server.LocalOrganizationInfo> userCompanyList = LocalData.UserInfo.UserOrganizationList.FindAll(item => item.Type == ICP.Framework.CommonLibrary.Server.LocalOrganizationType.Company);
        private void SetCmbSource()
        {
            #region Company

            this.chkcmbCompany.Properties.BeginUpdate();
            chkcmbCompany.Properties.Items.Clear();
            foreach (var item in userCompanyList)
            {
                chkcmbCompany.Properties.Items.Add(item.ID, LocalData.IsEnglish ? item.EShortName : item.CShortName,
                                                   CheckState.Checked, true);
            }
            this.chkcmbCompany.Properties.EndUpdate();
            #endregion

            #region 当前用户所在的操作口岸的所有揽货人
            mcmbSales.Enter += this.OnSalesFirstTimeEnter;
            #endregion

            #region ShipLine
            this.cmbShipLine.OnFirstEnter += OnShipLineFirstEnter;
            
            #endregion
        }
        private void OnSalesFirstTimeEnter(object sender, EventArgs e)
        {
            List<Guid> companyIDs = new List<Guid>();
            foreach (var item in userCompanyList) { companyIDs.Add(item.ID); }

            List<UserList> user = UserService.GetUnderlingUserList(companyIDs.ToArray(), null, null, true);

            Dictionary<string, string> col = new Dictionary<string, string>();
            col.Add(LocalData.IsEnglish ? "EName" : "CName", "名称");
            col.Add("Code", "代码");
            mcmbSales.InitSource<UserList>(user, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
        }
        private void OnShipLineFirstEnter(object sender, EventArgs e)
        {
            List<ShippingLineList> shippingLines = TransportFoundationService.GetShippingLineList(string.Empty, string.Empty, true, 100);
            ShippingLineList emptySL = new ShippingLineList();
            emptySL.ID = Guid.Empty;
            emptySL.EName = emptySL.CName = emptySL.Code = string.Empty;
            this.cmbShipLine.Properties.BeginUpdate();
            shippingLines.Insert(0, emptySL);

            foreach (ShippingLineList item in shippingLines)
            {
                cmbShipLine.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.EName, item.ID));
            }
            this.cmbShipLine.Properties.EndUpdate();
        }


        #endregion

        #region 属性

        public List<Guid> CompanyIDs
        {
            get
            {
                List<Guid> companyIDs = new List<Guid>();
                foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in chkcmbCompany.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                        companyIDs.Add(new Guid(item.Value.ToString()));
                }
                return companyIDs;
            }
        }

        #endregion

        #region event
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (OnSearched != null)
            {
                using (new CursorHelper())
                {
                    OnSearched(this, GetData());
                }
            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            txtName.Text = string.Empty;
            cmbShipLine.SelectedIndex = 0;
        }

        #endregion

        #region ISearchPart 成员

        public override object GetData()
        {
           
            try
            {
                return null;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); return null; }

        }
        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        #endregion

    }
}
