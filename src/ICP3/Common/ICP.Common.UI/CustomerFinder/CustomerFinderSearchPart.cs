using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface;

namespace ICP.Common.UI.CustomerFinder
{
    [ToolboxItem(false)]
    public partial class CustomerFinderSearchPart : ICP.Framework.ClientComponents.UIFramework.BaseSearchPart
    {
        #region serivce

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

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

        #region init

        List<CustomerType> _customerTypes = null;
        bool _isAgentOfCarrier = false;
        Guid? _agentCustomerSolutionID = null;
        bool _isFromOrder = false;
        Guid? _curruntUserID = null;
        Guid? _curruntSalesID = null;
        CustomerCodeApplyState? _codeApplyState = null;

        public CustomerFinderSearchPart()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this._customerTypes = null;
                popCompany.QueryPopUp -= popCompany_QueryPopUp;
                CommonUtility.RemoveSearchPartKeyEnterToSearch(new List<Control> { this.txtCodeOrName, txtAddress, txtEmail, txtFax, txtTel }, this.KeyEventHandle);
                this.treeCompany.DoubleClick -= this.treeCompany_DoubleClick;
                this.treeGeography.DoubleClick -= this.treeGeography_DoubleClick;
                this.OnSearched = null;
                this.popGeography.Enter -= this.OnPopGeographFirstTimeEnter;

                this.cmbApplyState.OnFirstEnter -= this.OnApplyStateFirstTimeEnter;
                this.treeCompany.DataSource = null;
                this.bsGeography.DataSource = null;
                this.bsGeography.Dispose();

                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;

                }
            };
            if (LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            chkEnabled.Text = "审核日期";
            labFrom.Text = "从";
            labTo.Text = "到";
            labAddress.Text = "地址";
            labApplyState.Text = "审核状态";
            labCompany.Text = "申请人公司";
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

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
            CommonUtility.SearchPartKeyEnterToSearch(new List<Control> { this.txtCodeOrName, txtAddress, txtEmail, txtFax, txtTel }, this.btnSearch, this.KeyEventHandle);
        }
        private void KeyEventHandle(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnSearch.PerformClick();
        }
        private void InitControls()
        {
            popGeography.Enter += this.OnPopGeographFirstTimeEnter;


            dteFrom.DateTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).Date.AddMonths(-1);
            dteTo.DateTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).Date;

            this.cmbApplyState.OnFirstEnter += this.OnApplyStateFirstTimeEnter;
        }
        private void OnApplyStateFirstTimeEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<CustomerCodeApplyState>> customerCodeApplyState = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<CustomerCodeApplyState>(LocalData.IsEnglish);
            cmbApplyState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(string.Empty, System.DBNull.Value));
            cmbApplyState.Properties.BeginUpdate();
            foreach (var item in customerCodeApplyState)
            {
                if (item.Value == CustomerCodeApplyState.All)
                {
                    continue;
                }

                cmbApplyState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbApplyState.Properties.EndUpdate();
        }
        private bool isFirstTimeEnterGeography = true;
        private void OnPopGeographFirstTimeEnter(object sender, EventArgs e)
        {
            if (isFirstTimeEnterGeography)
            {
                this.popGeography.QueryPopUp += this.popGeography_QueryPopUp;
                isFirstTimeEnterGeography = false;
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

        public override object GetData()
        {
            List<CustomerType> types = new List<CustomerType>();
            bool? isAgentOfCarrier = null;

            if (_customerTypes != null)
            {
                types = _customerTypes;
                isAgentOfCarrier = _isAgentOfCarrier;
            }
            else
            {
                bool hasForwarding = false;
                foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in cmbType.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                    {
                        if (item.Value is CustomerType &&
                            (CustomerType)item.Value == CustomerType.Forwarding)
                        {
                            hasForwarding = true;
                        }

                        if (item.Value.GetType() == typeof(short))
                        {
                            isAgentOfCarrier = true;
                        }
                        else
                        {
                            types.Add((CustomerType)item.Value);
                        }
                    }
                }

                if (hasForwarding)
                {
                    isAgentOfCarrier = null;
                }
                else if (hasForwarding == false && isAgentOfCarrier == true)
                {
                    types.Add(CustomerType.Forwarding);
                }

                if (types.Count == 11)
                {
                    types = null;
                }
            }

            DateTime? from = null, to = null;
            if (chkEnabled.Checked)
            {
                from = dteFrom.DateTime.Date;
                to = CommonUtility.GetEndDate(dteTo.DateTime);
            }

            //CustomerCodeApplyState? codeApplyState = null;
            if (_codeApplyState == null && cmbApplyState.EditValue != null && cmbApplyState.EditValue.ToString().Length > 0)
                _codeApplyState = (CustomerCodeApplyState)cmbApplyState.EditValue;

            List<CustomerInfo> list = CustomerService.GetCustomerListBySearch(txtCodeOrName.Text.Trim(),
                                                                      txtAddress.Text.Trim(),
                                                                      txtTel.Text.Trim(),
                                                                      txtFax.Text.Trim(),
                                                                      txtEmail.Text.Trim(),
                                                                      _countryID,
                                                                      _provinceID,
                                                                      CustomerStateType.Valid,
                                                                      types == null ? null : types.ToArray(),
                                                                      isAgentOfCarrier,
                                                                      _codeApplyState,
                                                                      _organizationID,
                                                                      _agentCustomerSolutionID,
                                                                      from,
                                                                      to,
                                                                      _isFromOrder,
                                                                      _curruntUserID,
                                                                      int.Parse(numMax.Value.ToString()));

            if (list != null)
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "total search " + list.Count.ToString() + " data." : "总共查询到 "
                                               + list.Count.ToString() + " 条数据.");

            return list;
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            if (OnSearched != null)
                OnSearched(this, GetData());
        }

        public override void Init(IDictionary<string, object> values)
        {
            this.btnClear.PerformClick();
            foreach (var item in values)
            {
                if (item.Key.ToUpper() == "NAME")
                {
                    txtCodeOrName.Text = item.Value.ToString();
                }
                else if (item.Key.ToUpper() == "CUSTOMERTYPE")
                {
                    _customerTypes = item.Value as List<CustomerType>;
                }
                else if (item.Key.ToUpper() == "ISAGENTOFCARRIER")
                {
                    _isAgentOfCarrier = true;
                }
                else if (item.Key == "SolutionID")
                {
                    _agentCustomerSolutionID = new Guid(item.Value.ToString());
                }
                else if (item.Key == "IsFromOrder")
                {
                    _isFromOrder = (bool)item.Value;
                }
                else if (item.Key == "CurruntUserID")
                {
                    _curruntUserID = new Guid(item.Value.ToString());
                }
                else if (item.Key == "CurruntSalesID")
                {
                    _curruntSalesID = new Guid(item.Value.ToString());
                }
                else if (item.Key == "CodeApplyState")
                {
                    if (item.Value != null)
                    {
                        _codeApplyState = (CustomerCodeApplyState)item.Value;
                    }
                }
            }

            if (_codeApplyState != null)
            {
                cmbApplyState.ShowSelectedValue(_codeApplyState, ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<CustomerCodeApplyState>((CustomerCodeApplyState)_codeApplyState, LocalData.IsEnglish));
                cmbApplyState.Enabled = false;
            }
            cmbType.Properties.BeginUpdate();
            cmbType.Properties.Items.Clear();
            if (_customerTypes != null)
            {
                foreach (var item in _customerTypes)
                {
                    if (item == CustomerType.Forwarding && _isAgentOfCarrier)
                    {
                        cmbType.Properties.Items.Add((short)100, LocalData.IsEnglish ? "AgentOfCarrier" : "承运人", CheckState.Checked, true);
                    }
                    else
                    {
                        string description = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<CustomerType>(item, LocalData.IsEnglish);
                        cmbType.Properties.Items.Add(item, description, CheckState.Checked, true);
                    }
                }

                cmbType.Enabled = false;
            }
            else
            {
                List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<CustomerType>> customerTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<CustomerType>(LocalData.IsEnglish);
                foreach (var item in customerTypes)
                {
                    if (item.Value == CustomerType.Unknown)
                    {
                        continue;
                    }

                    cmbType.Properties.Items.Add(item.Value, item.Name, CheckState.Checked, true);
                }

                cmbType.Properties.Items.Add((short)100, LocalData.IsEnglish ? "AgentOfCarrier" : "承运人", CheckState.Checked, true);
            }

            cmbType.Properties.EndUpdate();
            //this.btnSearch.PerformClick();
        }

        #endregion

        #region btn
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (_curruntSalesID != null && _curruntUserID != null)
            {

                if ((_curruntUserID != _curruntSalesID) && System.Text.Encoding.GetEncoding("GB2312").GetBytes(txtCodeOrName.Text.Trim()).Length < 4)
                {
                    ICP.Framework.ClientComponents.Controls.Utility.ShowMessage(LocalData.IsEnglish ? "CodeorName,You must enter 4 or more characters to the query" : "当前用户和揽货人不一致，代码/名称 必须输入4个或以上的字符才能查询！");
                    return;
                }
            }
            if (OnSearched != null)
                OnSearched(this, GetData());
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.ClearControl();
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

        #endregion

        //private void cmbCountryProvince_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Delete)
        //    {
        //        popGeography.Text = string.Empty;
        //        popGeography.EditValue = null;
        //        //popGeography.SelectedValue = null;  ?

        //    }
        //    else if (e.KeyCode == Keys.Back && this.popGeography.Text.Trim().Length == 1)
        //    {
        //        //popGeography.SelectedValue = null;  ?
        //    }
        //}
    }
}
