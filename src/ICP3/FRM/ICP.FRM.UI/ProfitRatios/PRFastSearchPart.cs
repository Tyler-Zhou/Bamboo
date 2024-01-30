using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface;
using ICP.FRM.ServiceInterface;
using ICP.FRM.ServiceInterface.DataObjects;

namespace ICP.FRM.UI.ProfitRatios
{    
    /// <summary>
    /// 快速搜索界面基类
    /// </summary>
    [ToolboxItem(false)]
    [SmartPart]
    public partial class PRFastSearchPart : BaseSearchPart
    {
        #region service
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        /// <summary>
        /// 利润配比服务
        /// </summary>
        private IProfitRatiosService ProfitRatiosService
        {
            get
            {
                return ServiceClient.GetService<IProfitRatiosService>();
            }
        }

        List<Guid> companyIDs = null;
        /// <summary>
        /// 公司IDs,默认为当前用户所在公司(OrganizationType.Company)
        /// </summary>
        private Guid[] CompanyIDs
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
        #endregion

        #region init
        /// <summary>
        /// 
        /// </summary>
        public PRFastSearchPart()
        {
            InitializeComponent();
            Disposed += (sender, e) => {

                OnSearched = null;
                txtNo.KeyDown -= TextBox_KeyDown;
                stxtCustomer.KeyDown -= TextBox_KeyDown;
                stxtPort.KeyDown -= TextBox_KeyDown;
                KeyDown -= TextBox_KeyDown;

                //单号
                cmbNoSearchType.OnFirstEnter -= OncmbNoSearchTypeEnter;

                cmbCustomerSearchType.OnFirstEnter -= OncmbCustomerSearchTypeEnter;

                cmbPortSearchType.OnFirstEnter -= OncmbPortSearchTypeEnter;

                cmbDateSearchType.OnFirstEnter -= OncmbDateSearchTypeEnter;

                cmbDateType.OnFirstEnter -= OncmbDateTypeEnter;
               
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
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
            txtNo.KeyDown += new KeyEventHandler(TextBox_KeyDown);
            stxtCustomer.KeyDown += new KeyEventHandler(TextBox_KeyDown);
            stxtPort.KeyDown += new KeyEventHandler(TextBox_KeyDown);
            KeyDown += new KeyEventHandler(TextBox_KeyDown);

            llabMore.Click += delegate { OnClickMore(); };
        }
        /// <summary>
        /// 
        /// </summary>
        protected void OnClickMore() {
            Workitem.Commands[ProfitRatiosCommandConstants.Command_ShowSearch].Execute();
        }

        void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2 || e.KeyCode == Keys.F5) btnSearch.PerformClick();
        }

        private void OncmbNoSearchTypeEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<NoSearchType>> noSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<NoSearchType>(LocalData.IsEnglish);
            cmbNoSearchType.Properties.BeginUpdate();
            cmbNoSearchType.Properties.Items.Clear();
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
            cmbCustomerSearchType.Properties.Items.Clear();
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
            cmbPortSearchType.Properties.Items.Clear();
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
            cmbDateSearchType.Properties.Items.Clear();
            foreach (var item in dateSearchTypes)
            {
                cmbDateSearchType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbDateSearchType.Properties.EndUpdate();
        }
        private void OncmbDateTypeEnter(object sender, EventArgs e)
        {
            cmbDateType.Properties.BeginUpdate();
            cmbDateType.Properties.Items.Clear();
            cmbDateType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "UnKnow" : "不确定", 0));
            cmbDateType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "Last Week" : "一周内", 1));
            cmbDateType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "This Month" : "一月内", 2));
            cmbDateType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "Last Year" : "一年内", 3));
            cmbDateType.Properties.EndUpdate();
        }
        private void InitControls()
        {
            cmbNoSearchType.ShowSelectedValue(NoSearchType.All, LocalData.IsEnglish ? "All No" : "全部单号");

            //单号
            cmbNoSearchType.OnFirstEnter += OncmbNoSearchTypeEnter;

            cmbCustomerSearchType.OnFirstEnter += OncmbCustomerSearchTypeEnter;
            cmbCustomerSearchType.ShowSelectedValue(CustomerSearchType.All, LocalData.IsEnglish ? "All Customer" : "全部客户");


            cmbPortSearchType.ShowSelectedValue(PortSearchType.All, LocalData.IsEnglish ? "All Location" : "全部地点");
            cmbPortSearchType.OnFirstEnter += OncmbPortSearchTypeEnter;

            cmbDateSearchType.ShowSelectedValue(DateSearchType.All,LocalData.IsEnglish?"All Date":"全部日期");
            cmbDateSearchType.OnFirstEnter += OncmbDateSearchTypeEnter;

            cmbDateType.ShowSelectedValue(0, LocalData.IsEnglish ? "UnKnow" : "不确定");
            cmbDateType.OnFirstEnter += OncmbDateTypeEnter;
            
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override object GetData()
        {
            QueryCriteria4ProfitRatios queryParamater = new QueryCriteria4ProfitRatios()
            {
                CompanyIDs = CompanyIDs,
                BeginTime = DateTime.Now.AddDays(-7),
                EndTime = DateTime.Now,
            };
            return ProfitRatiosService.GetBusinessStatisticsList(queryParamater);

        }
        /// <summary>
        /// 
        /// </summary>
        public override event SearchResultHandler OnSearched;
        /// <summary>
        /// 
        /// </summary>
        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        #endregion

        #region btn

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (OnSearched != null)
                {
                    using (new CursorHelper())
                    {
                        OnSearched(this, GetData());
                    }
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtNo.Text = stxtCustomer.Text = stxtPort.Text = string.Empty;
                cmbCustomerSearchType.SelectedIndex = cmbDateSearchType.SelectedIndex = cmbDateType.SelectedIndex = cmbNoSearchType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }
        #endregion
    }
}
