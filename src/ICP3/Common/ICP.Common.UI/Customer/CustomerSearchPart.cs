
//-----------------------------------------------------------------------
// <copyright file="CustomerManagerSearchPart.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.Common.UI.Customer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI;
    using ICP.Common.ServiceInterface;
    using ICP.Common.ServiceInterface.DataObjects;
    using ICP.Sys.ServiceInterface;
    using ICP.Sys.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Client;

    /// <summary>
    /// 客户管理-搜索器面版
    /// <remarks>
    /// 在该面板只实现本界面的控制逻辑。
    /// 如果要与服务交互。需通过Controller交互.(最少知识原则)
    /// </remarks>
    /// </summary>
    [System.ComponentModel.ToolboxItem(false)]
    public partial class CustomerSearchPart : DevExpress.XtraEditors.XtraUserControl, ICP.Framework.ClientComponents.UIManagement.ISearchPart
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        public IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
            }
        }

        public IOrganizationService OrganizationService
        {
            get
            {
                return ServiceClient.GetService<IOrganizationService>();
            }
        }

        #endregion

        #region 初始化



        protected virtual CustomerType? finderCustomerType { get { return null; } }
        protected virtual bool? IsAgentOfCarrier { get { return null; } }
        protected virtual CustomerStateType? CustomerState { get { return null; } }
        protected virtual CustomerCodeApplyState? CodeApplyState { get { return null; } }

        protected virtual bool? isValid { get { return null; } }

        public CustomerSearchPart()
        {
            InitializeComponent();
            if (LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate
            {

                this.DataReturned = null;
                this._customerTypes = null;
                this.treeCompany.DoubleClick -= this.treeCompany_DoubleClick;
                this.treeGeography.DoubleClick -= this.treeGeography_DoubleClick;
                this.chkEnabled.CheckedChanged -= this.chkEnabled_CheckedChanged;
                this.cmbType.OnFirstEnter -= this.OncmbTypeFirstEnter;
                this.popCompany.QueryPopUp -= this.popCompany_QueryPopUp;
                this.popGeography.QueryPopUp -= this.popGeography_QueryPopUp;
                this.popGeography.Enter -= this.OnPopGeographFirstTimeEnter;
                CommonUtility.RemoveSearchPartKeyEnterToSearch(new List<Control> { this.txtCodeOrName, txtAddress, txtEmail, txtFax, txtTel }, this.KeyEventHandle);
                this.cmbApplyState.OnFirstEnter -= this.OnApplyStateFirstTimeEnter;
                this._customerTypes = null;
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }

            };
        }
        private void OnPopGeographFirstTimeEnter(object sender, EventArgs e)
        {
            if (isFirstTimeEnterGeography)
            {
                this.popGeography.QueryPopUp += this.popGeography_QueryPopUp;
                isFirstTimeEnterGeography = false;
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.popGeography.Enter += OnPopGeographFirstTimeEnter;
            popCompany.QueryPopUp += popCompany_QueryPopUp;

            if (LocalData.IsEnglish)
            {
                colEName.Visible = true;
                colEShortName.Visible = true;
            }
            else
            {
                colCName.Visible = true;
                colCShortName.Visible = true;
            }

            InitControls();

            //if (DataReturned != null)
            //    DataReturned(this, new ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>(GetData()));

            CommonUtility.SearchPartKeyEnterToSearch(new List<Control> { this.txtCodeOrName, txtAddress, txtEmail, txtFax, txtTel }, this.btnSearch, this.KeyEventHandle);
        }
        private void KeyEventHandle(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnSearch.PerformClick();
        }
        private void SetCnText()
        {
            chkEnabled.Text = "审核日期";
            labFrom.Text = "从";
            labTo.Text = "到";
            labAddress.Text = "地址";
            labApplyState.Text = "审核状态";
            labCompany.Text = "申请人公司";
            //labCustomerState.Text = "状态";
            labEmail.Text = "邮箱";
            labFax.Text = "传真";
            labGeography.Text = "国家/省份";
            labMax.Text = "最大行数";
            labCodeOrName.Text = "代码/名称";
            labTel.Text = "电话";
            labType.Text = "类型";

            btnClearGeographyPop.Text = "清空";
            this.btnClear.Text = "清空(&L)";
            this.btnSearch.Text = "查询(&R)";
            nbarBase.Caption = "基本信息";
            nbarApply.Caption = "审核信息";
        }
        private bool isFirstTimeEnterGeography = true;
        private void OncmbTypeFirstEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<CustomerType>> customerTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<CustomerType>(LocalData.IsEnglish);
            customerTypes.RemoveAll(item => item.Value == CustomerType.Unknown);
            cmbType.Properties.BeginUpdate();
            cmbType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(string.Empty, System.DBNull.Value));
            foreach (var item in customerTypes)
            {
                cmbType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbType.Properties.EndUpdate();
        }
        private void InitControls()
        {

            #region CustomerType
            this.cmbType.OnFirstEnter += this.OncmbTypeFirstEnter;

            if (IsAgentOfCarrier.HasValue && IsAgentOfCarrier.Value)
            {
                cmbType.EditValue = (short)100;
                cmbType.Enabled = false;
            }
            else if (finderCustomerType != null)
            {
                cmbType.EditValue = finderCustomerType.Value;
                cmbType.Enabled = false;
            }
            #endregion


            dteFrom.DateTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).Date.AddMonths(-1);
            dteTo.DateTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).Date;
            this.cmbApplyState.OnFirstEnter += this.OnApplyStateFirstTimeEnter;

        }
        private void OnApplyStateFirstTimeEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<CustomerCodeApplyState>> customerCodeApplyState = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<CustomerCodeApplyState>(LocalData.IsEnglish);
            customerCodeApplyState.RemoveAll(item => item.Value == CustomerCodeApplyState.All);
            cmbApplyState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(string.Empty, System.DBNull.Value));
            cmbApplyState.Properties.BeginUpdate();
            foreach (var item in customerCodeApplyState)
            {
                cmbApplyState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbApplyState.Properties.EndUpdate();
            if (CodeApplyState != null)
            {
                cmbApplyState.EditValue = CodeApplyState.Value;
                cmbApplyState.Enabled = false;
            }
        }
        #endregion

        #region POP

        Guid? _organizationID = null;
        Guid? _countryID = null;
        Guid? _provinceID = null;

        #region  Geography

        void popGeography_QueryPopUp(object sender, CancelEventArgs e)
        {
            this.popGeography.QueryPopUp -= new CancelEventHandler(popGeography_QueryPopUp);
            List<CountryProvinceList> list = GeographyService.GetCountryProvinceList(string.Empty, string.Empty, null, true, 0);
            bsGeography.DataSource = list;
        }

        CountryProvinceList CurrentGeography { get { return bsGeography.Current as CountryProvinceList; } }


        private void treeGeography_DoubleClick(object sender, EventArgs e)
        {
            if (CurrentGeography == null) return;

            if (CurrentGeography.Type == CountryProvinceType.Country)
            {
                _countryID = CurrentGeography.ID;
                _provinceID = null;
                popGeography.Text = LocalData.IsEnglish ? CurrentGeography.EName : CurrentGeography.CName;
            }
            else
            {
                _countryID = CurrentGeography.ParentID.Value;
                _provinceID = CurrentGeography.ID;
                popGeography.Text = CurrentGeography.ParentName + "." + (LocalData.IsEnglish ? CurrentGeography.EName : CurrentGeography.CName);
            }
            popGeography.ClosePopup();
        }

        private void btnClearPopGeography_Click(object sender, EventArgs e)
        {
            _countryID = _provinceID = null;
            popGeography.Text = string.Empty;
            popGeography.ClosePopup();
        }
        #endregion

        #region  Company

        void popCompany_QueryPopUp(object sender, CancelEventArgs e)
        {
            this.popCompany.QueryPopUp -= new CancelEventHandler(popCompany_QueryPopUp);
            List<OrganizationList> organizationList = OrganizationService.GetOrganizationList(string.Empty, string.Empty, null, 0);
            bsCompany.DataSource = organizationList;
        }

        OrganizationList CurrentOrganization { get { return bsCompany.Current as OrganizationList; } }

        private void treeCompany_DoubleClick(object sender, EventArgs e)
        {
            if (CurrentOrganization == null) return;

            _organizationID = CurrentOrganization.ID;
            popCompany.Text = LocalData.IsEnglish ? CurrentOrganization.EShortName : CurrentOrganization.CShortName;
            popCompany.ClosePopup();
        }

        private void btnClearPopCompany_Click(object sender, EventArgs e)
        {
            _organizationID = null;
            popCompany.Text = string.Empty;
            popCompany.ClosePopup();
        }

        #endregion

        #endregion

        #region ISearchPart 成员

        public object GetData()
        {
            //CustomerStateType? customerState = null;
            //if (cmbCustomerState.EditValue != null && cmbCustomerState.EditValue.ToString().Length > 0)
            //    customerState = (CustomerStateType)cmbCustomerState.EditValue;

            CustomerType? customerType = null;
            bool? isAgentOfCarrier = null;

            if (finderCustomerType != null)
                customerType = finderCustomerType;
            else if (cmbType.EditValue != null && cmbType.EditValue.ToString().Length > 0)
            {
                if (cmbType.EditValue.GetType() == typeof(short))
                {
                    isAgentOfCarrier = true;
                }
                else
                {
                    customerType = (CustomerType)cmbType.EditValue;
                }
            }

            DateTime? from = null, to = null;
            if (chkEnabled.Checked)
            {
                from = dteFrom.DateTime.Date;
                to = CommonUtility.GetEndDate(dteTo.DateTime);
            }

            CustomerCodeApplyState? codeApplyState = null;
            if (cmbApplyState.EditValue != null && cmbApplyState.EditValue.ToString().Length > 0)
                codeApplyState = (CustomerCodeApplyState)cmbApplyState.EditValue;

            List<CustomerInfo> list = CustomerService.GetCustomerListBySearch(txtCodeOrName.Text.Trim(),
                                                                      txtAddress.Text.Trim(),
                                                                      txtTel.Text.Trim(),
                                                                      txtFax.Text.Trim(),
                                                                      txtEmail.Text.Trim(),
                                                                      _countryID,
                                                                      _provinceID,
                                                                      CustomerStateType.Valid,
                                                                      null,
                                                                      isAgentOfCarrier,
                                                                      codeApplyState,
                                                                      _organizationID,
                                                                      _agentCustomerSolutionID,
                                                                      from,
                                                                      to,
                                                                      false,
                                                                      null,
                                                                      int.Parse(numMax.Value.ToString()));
            return list;
        }

        public event EventHandler<ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>> DataReturned;
        public virtual void InitialValues(string property, object value)
        {
            this.ClearControl();

            if (IsAgentOfCarrier.HasValue && IsAgentOfCarrier.Value)
            {
                cmbType.EditValue = (short)100;
                cmbType.Enabled = false;
            }
            else if (finderCustomerType != null)
            {
                cmbType.EditValue = finderCustomerType.Value;
                cmbType.Enabled = false;
            }

            //if (property.Contains(SearchFieldConstants.Code))
            //    txtCode.Text = value == null ? string.Empty : value.ToString();
            //else
            txtCodeOrName.Text = value == null ? string.Empty : value.ToString();
        }

        #endregion

        #region btn
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (DataReturned != null)
                DataReturned(this, new ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>(GetData()));
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.ClearControl();
        }

        private void ClearControl()
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                if (item is DevExpress.XtraEditors.TextEdit
                    && (item is DevExpress.XtraEditors.SpinEdit) == false
                    && (item is DevExpress.XtraEditors.ComboBoxEdit) == false

                    && item.Enabled == true
                    && (item as DevExpress.XtraEditors.TextEdit).Properties.ReadOnly == false)
                    item.Text = string.Empty;
            }

            _countryID = _provinceID = null;
            popGeography.Text = string.Empty;
            cmbApplyState.EditValue = System.DBNull.Value;
            cmbApplyState.Text = string.Empty;
            //cmbCustomerState.EditValue = DBNull.Value;
            //cmbCustomerState.Text = string.Empty;

            if (this.cmbType.Enabled)
            {
                cmbType.EditValue = System.DBNull.Value;
                cmbType.Text = string.Empty;
            }


            _organizationID = null;
            popCompany.Text = string.Empty;

            txtCodeOrName.Focus();
        }

        private void chkEnabled_CheckedChanged(object sender, EventArgs e)
        {
            this.dteFrom.Enabled = this.dteTo.Enabled = chkEnabled.Checked;
            if (chkEnabled.Checked)
            {
                this.dteFrom.DateTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).AddMonths(-1);
                this.dteTo.DateTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            }
            else
            {
                this.dteFrom.Text = string.Empty;
                this.dteTo.Text = string.Empty;
            }
        }

        #endregion


        #region ISearchPart 成员

        List<CustomerType> _customerTypes;
        Guid? _agentCustomerSolutionID = null;

        public virtual void InitialValues(string searchValue, string property, SearchConditionCollection conditions, FinderTriggerType triggerType)
        {
            this.ClearControl();
            _customerTypes = new List<CustomerType>();
            if (conditions != null)
            {
                if (conditions.Contain("CustomerType"))
                {
                    List<SearchCondition> values = conditions.GetValues("CustomerType");

                    foreach (SearchCondition condition in values)
                    {
                        CustomerType type = (CustomerType)condition.Value;
                        _customerTypes.Add(type);
                    }

                    this.cmbType.Properties.Items.Clear();//Added by Royal at 2011-07-07 18:12
                    foreach (var item in _customerTypes)
                    {
                        string description = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<CustomerType>(item, LocalData.IsEnglish);

                        if (item == CustomerType.Unknown)
                        {
                            description = LocalData.IsEnglish ? "All" : "全部";
                        }

                        cmbType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(description, item));
                    }
                    cmbType.SelectedIndex = 0;
                }


                SearchCondition solutionIDCondition = conditions.GetValue("SolutionID");
                if (solutionIDCondition != null && solutionIDCondition.Value != null)
                    _agentCustomerSolutionID = new Guid(solutionIDCondition.Value.ToString());


            }

            if (triggerType == FinderTriggerType.KeyEnter) this.txtCodeOrName.Text = searchValue;
        }

        #endregion
    }

    public class CustomerFinderSearchPart : CustomerSearchPart
    {
    }

    public class CustomerAirlineFinderSearchPart : CustomerSearchPart
    {
        protected override CustomerType? finderCustomerType { get { return CustomerType.Airline; } }

    }

    public class CustomerCarrierFinderSearchPart : CustomerSearchPart
    {
        protected override CustomerType? finderCustomerType { get { return CustomerType.Carrier; } }
    }

    public class CustomerForwardingFinderSearchPart : CustomerSearchPart
    {
        protected override CustomerType? finderCustomerType { get { return CustomerType.Forwarding; } }
    }
    public class CustomerAgentOfCarrierFinderSearchPart : CustomerSearchPart
    {
        protected override bool? IsAgentOfCarrier { get { return true; } }
    }

    public class CustomerAgentFinderSearchPart : CustomerSearchPart
    {
        protected override CustomerType? finderCustomerType { get { return CustomerType.Forwarding; } }
    }

    public class CustomerCustomsBrokerFinderSearchPart : CustomerSearchPart
    {
        protected override CustomerType? finderCustomerType { get { return CustomerType.Broker; } }
    }

    public class CustomerTruckerFinderSearchPart : CustomerSearchPart
    {
        protected override CustomerType? finderCustomerType { get { return CustomerType.Trucker; } }
    }

    public class CustomerWarehouseFinderSearchPart : CustomerSearchPart
    {
        protected override CustomerType? finderCustomerType { get { return CustomerType.Warehouse; } }
    }
}
