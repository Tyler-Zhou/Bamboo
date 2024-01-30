using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.DomesticTrade.ServiceInterface.DataObjects;
using ICP.FCM.DomesticTrade.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary;

namespace ICP.FCM.DomesticTrade.UI
{
    [ToolboxItem(false)]
    [SmartPart]
    public partial class FastSearchPart : BaseSearchPart
    {
        #region service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IDomesticTradeService oeService { get; set; }

        [ServiceDependency]
        public IDataFindClientService DataFindClientService { get; set; }

        [ServiceDependency]
        public IOrganizationService organizationService { get; set; }

        [ServiceDependency]
        public IUserService userService { get; set; }

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

            if (DesignMode == false)
            {
                Load += new EventHandler(BookingFastSearchPart_Load);
                if (LocalData.IsEnglish == false)
                {
                    SetCnText();
                }
                InitControls();
            }
        }

        void BookingFastSearchPart_Load(object sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            //if (LocalData.IsEnglish)
            //{
            //    SetCnText();
            //}

            SetControlsEnterToSearch();
        }

        private void SetCnText()
        {
            btnSearch.Text = "查询(&S)";
            llabMore.Text = "更多";
        }

        private void SetControlsEnterToSearch()
        {
            txtNo.KeyDown += new KeyEventHandler(TextBox_KeyDown);
            stxtCustomer.KeyDown += new KeyEventHandler(TextBox_KeyDown);
            stxtPort.KeyDown += new KeyEventHandler(TextBox_KeyDown);
            KeyDown += new KeyEventHandler(TextBox_KeyDown);

            llabMore.Click += delegate { OnClickMore(); };
        }
        protected virtual void OnClickMore() { }

        void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter|| e.KeyCode == Keys.F2) btnSearch.PerformClick();
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

                    List<OrganizationList> userCompanyList = userService.GetUserCompanyList(LocalData.UserInfo.LoginID, OrganizationType.Company);
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

        private void InitControls()
        {
            List<EnumHelper.ListItem<NoSearchType>> noSearchTypes = EnumHelper.GetEnumValues<NoSearchType>(LocalData.IsEnglish);
            foreach (var item in noSearchTypes)
            {
                cmbNoSearchType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }

            List<EnumHelper.ListItem<CustomerSearchType>> customerSearchTypes = EnumHelper.GetEnumValues<CustomerSearchType>(LocalData.IsEnglish);
            foreach (var item in customerSearchTypes)
            {
                cmbCustomerSearchType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }

            List<EnumHelper.ListItem<PortSearchType>> portSearchTypes = EnumHelper.GetEnumValues<PortSearchType>(LocalData.IsEnglish);
            foreach (var item in portSearchTypes)
            {
                cmbPortSearchType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }

            List<EnumHelper.ListItem<DateSearchType>> dateSearchTypes = EnumHelper.GetEnumValues<DateSearchType>(LocalData.IsEnglish);
            foreach (var item in dateSearchTypes)
            {
                cmbDateSearchType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }

            cmbNoSearchType.SelectedIndex = cmbCustomerSearchType.SelectedIndex = cmbPortSearchType.SelectedIndex = cmbDateSearchType.SelectedIndex = 0;

            cmbDateType.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "UnKnow" : "不确定", 0));
            cmbDateType.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "Last Week" : "一周内", 1));
            cmbDateType.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "This Month" : "一月内", 2));
            cmbDateType.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "Last Year" : "一年内", 3));
            cmbDateType.SelectedIndex = 0;
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

        void SetSearchTypes(ref NoSearchType noSearchType,ref CustomerSearchType customerSearchType,
                           ref PortSearchType portSearchType,ref DateSearchType dateSearchType)
        {
            noSearchType = (NoSearchType)Enum.Parse(typeof(NoSearchType), cmbNoSearchType.EditValue.ToString());
            customerSearchType = (CustomerSearchType)Enum.Parse(typeof(CustomerSearchType), cmbCustomerSearchType.EditValue.ToString());
            portSearchType = (PortSearchType)Enum.Parse(typeof(PortSearchType), cmbPortSearchType.EditValue.ToString());
            dateSearchType = (DateSearchType)Enum.Parse(typeof(DateSearchType), cmbDateSearchType.EditValue.ToString());
        }

        #endregion

        #region ISearchPart 成员

        public override event SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        #endregion

        #region btn

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (OnSearched != null)
                OnSearched(this, GetData());
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtNo.Text = stxtCustomer.Text = stxtPort.Text = string.Empty;
            cmbCustomerSearchType.SelectedIndex = cmbDateSearchType.SelectedIndex = cmbDateType.SelectedIndex = cmbNoSearchType.SelectedIndex = 0;
        }
        #endregion
    }
}
