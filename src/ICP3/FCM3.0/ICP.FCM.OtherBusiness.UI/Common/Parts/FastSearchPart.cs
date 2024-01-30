using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.FCM.OtherBusiness.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;

namespace ICP.FCM.OtherBusiness.UI
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class FastSearchPart : ICP.Framework.ClientComponents.UIFramework.BaseSearchPart
    {
        #region service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public ICP.FCM.OtherBusiness.ServiceInterface.IOtherBusinessService OBService { get; set; }

        [ServiceDependency]
        public IDataFindClientService dfService { get; set; }

        [ServiceDependency]
        public ICP.Sys.ServiceInterface.IOrganizationService organizationService { get; set; }

        [ServiceDependency]
        public ICP.Sys.ServiceInterface.IUserService userService { get; set; }

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
                this.Load += new EventHandler(BookingFastSearchPart_Load);
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
            this.btnSearch.Text = "查询(&S)";
            this.llabMore.Text = "更多";
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
            if (e.KeyCode == Keys.Enter|| e.KeyCode == Keys.F2) this.btnSearch.PerformClick();
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
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<NoSearchType>> noSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<NoSearchType>(LocalData.IsEnglish);
            foreach (var item in noSearchTypes)
            {
                cmbNoSearchType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }

            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<CustomerSearchType>> customerSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<CustomerSearchType>(LocalData.IsEnglish);
            foreach (var item in customerSearchTypes)
            {
                cmbCustomerSearchType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }

            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<PortSearchType>> portSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<PortSearchType>(LocalData.IsEnglish);
            foreach (var item in portSearchTypes)
            {
                cmbPortSearchType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }

            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<DateSearchType>> dateSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<DateSearchType>(LocalData.IsEnglish);
            foreach (var item in dateSearchTypes)
            {
                cmbDateSearchType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }

            cmbNoSearchType.SelectedIndex = cmbCustomerSearchType.SelectedIndex = cmbPortSearchType.SelectedIndex = cmbDateSearchType.SelectedIndex = 0;

            cmbDateType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "UnKnow" : "不确定", 0));
            cmbDateType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "Last Week" : "一周内", 1));
            cmbDateType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "This Month" : "一月内", 2));
            cmbDateType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "Last Year" : "一年内", 3));
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
                    return Utility.GetEndDate(DateTime.Now);
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
