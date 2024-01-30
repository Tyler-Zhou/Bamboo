using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Microsoft.Practices.CompositeUI;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary;

namespace ICP.FCM.OceanExport.UI
{    
    /// <summary>
    /// 快速搜索界面基类
    /// </summary>
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class FastSearchPart : ICP.Framework.ClientComponents.UIFramework.BaseSearchPart
    {
        #region service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
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

        #region 属性


        protected NoSearchType PartNoSearchType
        { get { return (NoSearchType)Enum.Parse(typeof(NoSearchType), cmbNoSearchType.EditValue.ToString()); } }
        protected CustomerSearchType PartCustomerSearchType
        { get { return (CustomerSearchType)Enum.Parse(typeof(CustomerSearchType), cmbCustomerSearchType.EditValue.ToString()); } }
        protected PortSearchType PartPortSearchType
        { get { return (PortSearchType)Enum.Parse(typeof(PortSearchType), cmbPortSearchType.EditValue.ToString()); } }
        protected DateSearchType PartDateSearchType
        { get { return (DateSearchType)Enum.Parse(typeof(DateSearchType), cmbDateSearchType.EditValue.ToString()); } }


        #endregion

        #region init

        public FastSearchPart()
        {
            InitializeComponent();
            this.Disposed += (sender, e) => {

                this.OnSearched = null;
                this.txtNo.KeyDown -= TextBox_KeyDown;
                this.stxtCustomer.KeyDown -= TextBox_KeyDown;
                this.stxtPort.KeyDown -= TextBox_KeyDown;
                this.KeyDown -=TextBox_KeyDown;

                //单号
                this.cmbNoSearchType.OnFirstEnter -= this.OncmbNoSearchTypeEnter;

                this.cmbCustomerSearchType.OnFirstEnter -= this.OncmbCustomerSearchTypeEnter;
     
                this.cmbPortSearchType.OnFirstEnter -= this.OncmbPortSearchTypeEnter;

                this.cmbDateSearchType.OnFirstEnter -= this.OncmbDateSearchTypeEnter;

                this.cmbDateType.OnFirstEnter -= this.OncmbDateTypeEnter;
               
                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
            {
                InitControls();
                SetControlsEnterToSearch();
            }
        }

        private void SetControlsEnterToSearch()
        {
            this.txtNo.KeyDown += new KeyEventHandler(TextBox_KeyDown);
            this.stxtCustomer.KeyDown += new KeyEventHandler(TextBox_KeyDown);
            this.stxtPort.KeyDown += new KeyEventHandler(TextBox_KeyDown);
            this.KeyDown += new KeyEventHandler(TextBox_KeyDown);

            llabMore.Click += delegate { OnClickMore(); };
        }
        protected virtual void OnClickMore() { }

        void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2 || e.KeyCode == Keys.F5) this.btnSearch.PerformClick();
        }

        List<Guid> companyIDs = null;
        /// <summary>
        /// 公司IDs,默认为当前用户所在公司( OrganizationType.Company)
        /// </summary>
        public virtual Guid[] CompanyIDs
        {
            get
            {
                if (companyIDs != null)
                {
                    return companyIDs.ToArray();
                }
                else
                {
                    companyIDs = new List<Guid>();

                    List<OrganizationList> userCompanyList = UserService.GetUserCompanyList(LocalData.UserInfo.LoginID, OrganizationType.Company);
                    if (userCompanyList.Count == 0)
                    {
                        throw new Exception(LocalData.IsEnglish ? "You have no rights to query data of any company. Please contat administrator." : "您没有权限查询任何操作口岸的数据，请联系管理员！");
                    }

                    foreach (var item in userCompanyList)
                    {
                        companyIDs.Add(item.ID);
                    }
                    return companyIDs.ToArray();
                }
            }
        }
        private void OncmbNoSearchTypeEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<NoSearchType>> noSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<NoSearchType>(LocalData.IsEnglish);
            cmbNoSearchType.Properties.BeginUpdate();
            this.cmbNoSearchType.Properties.Items.Clear();
            foreach (var item in noSearchTypes)
            {
                cmbNoSearchType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbNoSearchType.Properties.EndUpdate();
        }
        private void OncmbCustomerSearchTypeEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<CustomerSearchType>> customerSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<CustomerSearchType>(LocalData.IsEnglish);
            cmbCustomerSearchType.Properties.BeginUpdate();
            this.cmbCustomerSearchType.Properties.Items.Clear();
            foreach (var item in customerSearchTypes)
            {
                cmbCustomerSearchType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbCustomerSearchType.Properties.EndUpdate();
        }
        private void OncmbPortSearchTypeEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<PortSearchType>> portSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<PortSearchType>(LocalData.IsEnglish);
            cmbPortSearchType.Properties.BeginUpdate();
            this.cmbPortSearchType.Properties.Items.Clear();
            foreach (var item in portSearchTypes)
            {
                cmbPortSearchType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbPortSearchType.Properties.EndUpdate();
        }
        private void OncmbDateSearchTypeEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<DateSearchType>> dateSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<DateSearchType>(LocalData.IsEnglish);
            cmbDateSearchType.Properties.BeginUpdate();
            this.cmbDateSearchType.Properties.Items.Clear();
            foreach (var item in dateSearchTypes)
            {
                cmbDateSearchType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbDateSearchType.Properties.EndUpdate();
        }
        private void OncmbDateTypeEnter(object sender, EventArgs e)
        {
            cmbDateType.Properties.BeginUpdate();
            this.cmbDateType.Properties.Items.Clear();
            cmbDateType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "UnKnow" : "不确定", 0));
            cmbDateType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "Last Week" : "一周内", 1));
            cmbDateType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "This Month" : "一月内", 2));
            cmbDateType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "Last Year" : "一年内", 3));
            cmbDateType.Properties.EndUpdate();
        }
        private void InitControls()
        {
            this.cmbNoSearchType.ShowSelectedValue(NoSearchType.All, LocalData.IsEnglish ? "All No" : "全部单号");

            //单号
            this.cmbNoSearchType.OnFirstEnter += this.OncmbNoSearchTypeEnter;

            this.cmbCustomerSearchType.OnFirstEnter += this.OncmbCustomerSearchTypeEnter;
            this.cmbCustomerSearchType.ShowSelectedValue(CustomerSearchType.All, LocalData.IsEnglish ? "All Customer" : "全部客户");
        

            this.cmbPortSearchType.ShowSelectedValue(PortSearchType.All, LocalData.IsEnglish ? "All Location" : "全部地点");
            this.cmbPortSearchType.OnFirstEnter += this.OncmbPortSearchTypeEnter;
      
           this.cmbDateSearchType.ShowSelectedValue(DateSearchType.All,LocalData.IsEnglish?"All Date":"全部日期");
           this.cmbDateSearchType.OnFirstEnter += this.OncmbDateSearchTypeEnter;
       
           this.cmbDateType.ShowSelectedValue(0, LocalData.IsEnglish ? "UnKnow" : "不确定");
           this.cmbDateType.OnFirstEnter += this.OncmbDateTypeEnter;
            
        }

        public DateTime? From
        {
            get
            {
                switch (cmbDateType.SelectedIndex)
                {
                    case 0:
                        return null;
                    case 1:
                        return DateTime.Now.Date.AddDays(-7);
                    case 2:
                        return DateTime.Now.Date.AddDays(-30);
                    case 3:
                        return DateTime.Now.Date.AddDays(-365);
                };
                return null;
            }
        }

        public DateTime? To
        {
            get
            {
                if (cmbDateType.SelectedIndex == 0)
                    return null;
                else
                    return DateTime.Now.DateAttachEndTime();
            }
        }

        void SetSearchTypes(ref NoSearchType noSearchType, ref CustomerSearchType customerSearchType,
                           ref PortSearchType portSearchType, ref DateSearchType dateSearchType)
        {
            noSearchType = (NoSearchType)Enum.Parse(typeof(NoSearchType), cmbNoSearchType.EditValue.ToString());
            customerSearchType = (CustomerSearchType)Enum.Parse(typeof(CustomerSearchType), cmbCustomerSearchType.EditValue.ToString());
            portSearchType = (PortSearchType)Enum.Parse(typeof(PortSearchType), cmbPortSearchType.EditValue.ToString());
            dateSearchType = (DateSearchType)Enum.Parse(typeof(DateSearchType), cmbDateSearchType.EditValue.ToString());
        }

        #endregion

        #region ISearchPart 成员

        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        #endregion

        #region btn

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
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtNo.Text = stxtCustomer.Text = stxtPort.Text = string.Empty;
            cmbCustomerSearchType.SelectedIndex = cmbDateSearchType.SelectedIndex = cmbDateType.SelectedIndex = cmbNoSearchType.SelectedIndex = 0;
        }
        #endregion
    }
}
