using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.ClientComponents.UIManagement;
using ICP.Common.UI.Common;
namespace ICP.Common.UI.Customer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class CustomerEditPart : BaseControl, IDataContentPart
    {
        #region 服务

        public IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
            }
        }

        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region 初始化

        public CustomerEditPart()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.treeGeography.DataSource = null;
                this.treeGeography.DoubleClick -= this.treeGeography_DoubleClick;
                this.popGeography.QueryPopUp -= this.popGeography_QueryPopUp;
                this.cmbType.SelectedValueChanged -= this.cmbType_SelectedValueChanged;
                this.dxErrorProvider1.DataSource = null;
                this.bsGeography.DataSource = null;
                this.bsGeography.Dispose();
                this.bindingSource1.DataSource = null;
                this.bindingSource1.Dispose();
                this.DataChanged = null;
                this.cmbTradeTerm.OnFirstEnter -= this.OncmbTradeTermFirstEnter;
                this.cmbPaymentType.OnFirstEnter -= this.OncmbPaymentTypeFirstEnter;
                this.cmbTaxidType.OnFirstEnter -= this.OncmbTaxidTypeFirstEnter;
                this.cmbType.OnFirstEnter -= this.OncmbTypeFirstEnter;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };

            if (DesignMode == false)
            {
                if (LocalData.IsEnglish == false) SetCnText();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            InitControls();
            base.OnLoad(e);
        }

        private void SetCnText()
        {
            chkIsAgentOfCarrier.Properties.Caption = "承运人";
            navBankInfo.Caption = "工商信息";
            navContactInfo.Caption = "联系信息";
            groupCNInfo.Text = "中文信息";
            groupENInfo.Text = "英文信息";
            navBaseInfo.Caption = "基本信息";
            navOtherInfo.Caption = "备注信息";
            navName.Caption = "名称信息";
            labCAddress.Text = "地址";
            labCBillName.Text = "帐单名称";
            labCity.Text = "城市";
            labCName.Text = "全称";
            labCode.Text = "代码";
            labCreditLimit.Text = "信用限额";
            labCShortName.Text = "简称";
            labEAddress.Text = "地址";
            labEBillName.Text = "帐单名称";
            labEmail.Text = "邮箱";
            labEName.Text = "全称";
            labEShortName.Text = "简称";
            labFax.Text = "传真";
            labGeography.Text = "国家/州";
            labHomepage.Text = "主页";
            labKeyWord.Text = "关键字";
            labPaymentType.Text = "付款方式";
            labPostCode.Text = "邮编";
            labTaxIdNo.Text = "税务号";
            labTaxIdType.Text = "税务类型";
            labTel1.Text = "电话1";
            labTel2.Text = "电话2";
            labTerm.Text = "信用期限";
            labTradeTerm.Text = "贸易条款";
            labType.Text = "类型";

        }

        private void InitData()
        {
            if (string.IsNullOrEmpty(_CustomerInfo.CountryName) == false)
            {
                if (string.IsNullOrEmpty(_CustomerInfo.ProvinceName) == false)
                {
                    _CustomerInfo.CountryName += "." + _CustomerInfo.ProvinceName;
                }
            }
        }
        private void OncmbPaymentTypeFirstEnter(object sender, EventArgs e)
        {
            List<DataDictionaryList> paymentTypes = TransportFoundationService.GetDataDictionaryList(string.Empty, string.Empty, DataDictionaryType.PaymentTerm, true, 0);
            this.cmbPaymentType.Properties.BeginUpdate();
            this.cmbPaymentType.Properties.Items.Clear();
            cmbPaymentType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(string.Empty, Guid.Empty));
            foreach (var item in paymentTypes)
            {
                cmbPaymentType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
            }
            this.cmbPaymentType.Properties.EndUpdate();
        }
        private void OncmbTradeTermFirstEnter(object sender, EventArgs e)
        {
            List<DataDictionaryList> tradeTerms = TransportFoundationService.GetDataDictionaryList(string.Empty, string.Empty, DataDictionaryType.TradeTerm, true, 0);
            this.cmbTradeTerm.Properties.BeginUpdate();
            this.cmbTradeTerm.Properties.Items.Clear();
            cmbTradeTerm.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(string.Empty, Guid.Empty));
            foreach (var item in tradeTerms)
            {
                cmbTradeTerm.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
            }
            this.cmbTradeTerm.Properties.EndUpdate();
        }
        private void OncmbTaxidTypeFirstEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<TaxType>> taxTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<TaxType>(LocalData.IsEnglish);
            this.cmbTaxidType.Properties.BeginUpdate();
            this.cmbTaxidType.Properties.Items.Clear();
            cmbTaxidType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(string.Empty, System.DBNull.Value));
            foreach (var item in taxTypes)
            {
                cmbTaxidType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            this.cmbTaxidType.Properties.EndUpdate();
        }
        private void OncmbTypeFirstEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<CustomerType>> customerTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<CustomerType>(LocalData.IsEnglish);
            this.cmbType.Properties.BeginUpdate();
            this.cmbType.Properties.Items.Clear();
            foreach (var item in customerTypes)
            {
                cmbType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            this.cmbType.Properties.EndUpdate();
        }
        private void InitControls()
        {
            popGeography.QueryPopUp += new CancelEventHandler(popGeography_QueryPopUp);
            if (LocalData.IsEnglish)
                colEName.Visible = true;
            else
                colCName.Visible = true;

            if (_CustomerInfo != null)
            {

                SetCmbCityItemByProvinceId(_CustomerInfo.CountryID, _CustomerInfo.ProvinceID);
            }
            if (_CustomerInfo != null && !CommonUtility.GuidIsNullOrEmpty(_CustomerInfo.PaymentTypeID))
            {
                this.cmbPaymentType.ShowSelectedValue(_CustomerInfo.PaymentTypeID.Value, _CustomerInfo.PaymentTypeName);
            }
            this.cmbPaymentType.OnFirstEnter += this.OncmbPaymentTypeFirstEnter;

            if (_CustomerInfo != null && !CommonUtility.GuidIsNullOrEmpty(_CustomerInfo.TradeTermID))
            {
                this.cmbTradeTerm.ShowSelectedValue(_CustomerInfo.TradeTermID.Value, _CustomerInfo.TradeTermName);
            }
            this.cmbTradeTerm.OnFirstEnter += this.OncmbTradeTermFirstEnter;

            if (_CustomerInfo != null && _CustomerInfo.TaxIdType != null)
            {
                this.cmbTaxidType.ShowSelectedValue(_CustomerInfo.TaxIdType, ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<TaxType>(_CustomerInfo.TaxIdType.Value, LocalData.IsEnglish));
            }
            this.cmbTaxidType.OnFirstEnter += this.OncmbTaxidTypeFirstEnter;

            this.cmbType.ShowSelectedValue(_CustomerInfo.Type, ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<CustomerType>(_CustomerInfo.Type, LocalData.IsEnglish));

            this.cmbType.OnFirstEnter += this.OncmbTypeFirstEnter;


        }

        #endregion

        #region 变量

        CustomerInfo _CustomerInfo = null;

        #endregion

        #region  Geography

        void popGeography_QueryPopUp(object sender, CancelEventArgs e)
        {
            this.popGeography.QueryPopUp -= new CancelEventHandler(popGeography_QueryPopUp);
            List<CountryProvinceList> list = GeographyService.GetCountryProvinceList(string.Empty, string.Empty, null, null, 0);
            bsGeography.DataSource = list;
        }

        CountryProvinceList CurrentGeography { get { return bsGeography.Current as CountryProvinceList; } }


        private void treeGeography_DoubleClick(object sender, EventArgs e)
        {
            if (CurrentGeography == null) return;
            CustomerInfo currentData = bindingSource1.DataSource as CustomerInfo;

            if (CurrentGeography.Type == CountryProvinceType.Country)
            {
                _CustomerInfo.CountryID = CurrentGeography.ID;
                _CustomerInfo.ProvinceID = null;
                popGeography.Text = LocalData.IsEnglish ? CurrentGeography.EName : CurrentGeography.CName;
            }
            else
            {
                _CustomerInfo.CountryID = CurrentGeography.ParentID.Value;
                _CustomerInfo.ProvinceID = CurrentGeography.ID;
                popGeography.Text = _CustomerInfo.CountryName = CurrentGeography.ParentName + "." + (LocalData.IsEnglish ? CurrentGeography.EName : CurrentGeography.CName);

            }

            SetCmbCityItemByProvinceId(_CustomerInfo.CountryID, _CustomerInfo.ProvinceID);

            popGeography.ClosePopup();
        }

        #endregion

        #region 本地方法

        private void SetCmbCityItemByProvinceId(Guid? countryId, Guid? provinceId)
        {
            Guid? tempCountryId = null;
            if (countryId.HasValue && countryId.Value != Guid.Empty)
            {
                tempCountryId = countryId.Value;
            }
            Guid? tempprovinceId = null;
            if (provinceId.HasValue && provinceId.Value != Guid.Empty)
            {
                tempprovinceId = provinceId.Value;
            }


            cmbCity.Properties.Items.Clear();
            cmbCity.EditValue = System.DBNull.Value;

            List<LocationList> locationList = GeographyService.GetLocationList(string.Empty,
                                                                                tempCountryId,
                                                                                tempprovinceId,
                                                                                null,
                                                                                null,
                                                                                null,
                                                                                true,
                                                                                100);

            locationList.Add(new LocationList() { ID = Guid.Empty, CName = string.Empty, EName = string.Empty });
            if (locationList != null && locationList.Count != 0)
            {
                foreach (var item in locationList)
                {
                    cmbCity.Properties.Items.Add(
                        new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
                }
                cmbCity.SelectedIndex = 0;
            }
        }

        #endregion

        #region btnToEnCN

        private void btnToEnInfo_Click(object sender, EventArgs e)
        {
            txtEAddress.Text = txtCAddress.Text.Trim();
            txtEName.Text = txtCName.Text.Trim();
            txtEBillName.Text = txtCBillName.Text.Trim();
            txtEShortName.Text = txtCShortName.Text.Trim();
            bindingSource1.EndEdit();
        }

        private void btnToCnInfo_Click(object sender, EventArgs e)
        {
            txtCAddress.Text = txtEAddress.Text.Trim();
            txtCName.Text = txtEName.Text.Trim();
            txtCBillName.Text = txtEBillName.Text.Trim();
            txtCShortName.Text = txtEShortName.Text.Trim();
            bindingSource1.EndEdit();
        }

        #endregion

        #region IDataContentPart 成员
        public void BindingData(object data)
        {
            if (data == null) this.Enabled = false;
            else
            {
                this.bindingSource1.DataSource = _CustomerInfo = data as CustomerInfo;
                this.Enabled = true; ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).BeginEdit();
                if (string.IsNullOrEmpty(_CustomerInfo.CBillName))
                {
                    _CustomerInfo.CBillName = _CustomerInfo.CName;
                }
                if (string.IsNullOrEmpty(_CustomerInfo.EBillName))
                {
                    _CustomerInfo.EBillName = _CustomerInfo.EName;
                }
                InitData();
                bindingSource1.ResetBindings(false);
            }
        }

        public object Current { get { return this.bindingSource1.Current; } }
        public event EventHandler<ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>> DataChanged;
        public object DataSource
        {
            get { return bindingSource1.DataSource; }
            set { BindingData(value); }
        }

        public void EndEdit()
        {
            this.Validate();
            bindingSource1.EndEdit();
        }
        #endregion

        #region other control logic

        private void cmbType_SelectedValueChanged(object sender, EventArgs e)
        {
            CustomerType type = (CustomerType)cmbType.EditValue;
            if (type != CustomerType.Forwarding)
            {
                this.chkIsAgentOfCarrier.Checked = false;
                this.chkIsAgentOfCarrier.Enabled = false;
            }
            else
            {
                this.chkIsAgentOfCarrier.Enabled = true;
            }
        }
        #endregion


    }
}
