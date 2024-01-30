
//-----------------------------------------------------------------------
// <copyright file="CustomerManagerEditPart.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.Common.UI.CustomerManager
{
    using System;
    using System.Collections.Generic;
    using ICP.Framework.ClientComponents.UIFramework;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.ObjectBuilder;
    using ICP.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Client;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Framework.CommonLibrary.Helper;
    using DevExpress.XtraEditors.Controls;
    using System.Windows.Forms;
    using ICP.Sys.ServiceInterface.DataObjects;
    using ICP.Sys.ServiceInterface;
    using ICP.Framework.ClientComponents;
 

    /// <summary>
    /// 客户管理-编辑面版
    /// </summary>
    [System.ComponentModel.ToolboxItem(false)]
    [SmartPart]
    public class CustomerManagerEditPart : BaseEditPart
    {
        #region 服务

        [ServiceDependency]
        public WorkItem WorkItem { get; set; }


        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }


        #endregion

        CustomerInfo _currentCustomerInfo;
        CustomerConfirmInfo _applyCodeData;
        bool _isEntercmbCountryProvince = false;
        private DevExpress.XtraEditors.LabelControl labFIRMCODE;
        private DevExpress.XtraEditors.TextEdit txtFIRMCODE;
        private DevExpress.XtraEditors.TextEdit txtBankAccountNo;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        List<CountryProvinceList> _countryProvinceList = new List<CountryProvinceList>();

        #region 初始化

        public CustomerManagerEditPart()
        {
            this.InitializeComponent();
            this.Enabled = false;
            this.Disposed += delegate {
                this._applyCodeData = null;
                this.dxErrorProvider1.DataSource = null;
                this._countryProvinceList = null;
                this._currentCustomerInfo = null;
                this.bsDataSource.DataSource = null;
                this.bsDataSource.Dispose();
                this.Saved = null;
                this.cmbCountryProvince.EditValueChanged -= this.cmbCountryProvince_EditValueChanged;
                this.cmbCountryProvince.OnFirstTimeEnter -= this.OncmbCountryProvinceFirstEnter;
                this.SmartPartClosing -= CustomerManagerEditPart_SmartPartClosing;
                this.cmbCity.GotFocus -= new System.EventHandler(this.cmbCity_GotFocus);
                this.cmbType.SelectedIndexChanged -= this.cmbType_SelectedIndexChanged;
                this.cmbType.OnFirstEnter -= this.OncmbTypeFirstEnter;
                //this.cmbTaxidType.OnFirstEnter -= this.OncmbTaxidTypeFirstEnter;
                this.cmbPaymentType.OnFirstEnter -= this.OncmbPaymentTypeFirstEnter;
                this.cmbTradeTerm.OnFirstEnter -= this.OncmbTradeTermFirstEnter;
                
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                this.InitControls();
                InitCarrierPermission();
                this.SmartPartClosing += CustomerManagerEditPart_SmartPartClosing;
                this.ActivateSmartPartClosingEvent(this.WorkItem);
            }
        }

        /// <summary>
        /// 初始化船权限(只有远东区商务员才有权限修改船东资料)
        /// </summary>
        private void InitCarrierPermission()
        {
            if (_currentCustomerInfo != null && _currentCustomerInfo.Type != CustomerType.Carrier)
            {
                return;
            }

            List<UserList> userList = UserService.GetUnderlingUserList(null, null, new string[] { "远东区商务员" }, true);
            if (userList == null)
            {
                this.navBarGroupControlContainer1.Enabled = false;
                this.navBarGroupControlContainer3.Enabled = false;
                this.navBarGroupControlContainer4.Enabled = false;
                this.navBarGroupControlContainer5.Enabled = false;
                this.barSave.Enabled = false;
            }

            UserList userInfo = userList.Find(delegate(UserList jItem) { return jItem.ID == LocalData.UserInfo.LoginID; });

            if (userInfo == null)
            {
                this.navBarGroupControlContainer1.Enabled = false;
                this.navBarGroupControlContainer3.Enabled = false;
                this.navBarGroupControlContainer4.Enabled = false;
                this.navBarGroupControlContainer5.Enabled = false;
                this.barSave.Enabled = false;
            }

        }
        void CustomerManagerEditPart_SmartPartClosing(object sender, WorkspaceCancelEventArgs e)
        {
            if (_currentCustomerInfo.IsDirty && this.barSave.Enabled)
            {
                DialogResult dlg = CommonUtility.EnquireIsSaveCurrentDataByUpdated();
                if (dlg == DialogResult.Yes)
                {
                    if (_currentCustomerInfo.Validate() == false)
                    {
                        //para[0] = false;
                        //return para;

                        e.Cancel = true;
                        return;
                    }

                    RaiseSaved();
                }
                else if (dlg == DialogResult.Cancel)
                {
                    //para[0] = false;
                    //return para;
                    e.Cancel = true;
                }
                else if (dlg == DialogResult.No)
                {
                    //para[0] = true;
                }
            }
            else if (_currentCustomerInfo.IsNew)
            {
                DialogResult dlg = CommonUtility.EnquireIsSaveCurrentDataByNew();
                if (dlg == DialogResult.Yes)
                {
                    if (_currentCustomerInfo.Validate() == false)
                    {
                        //para[0] = false;
                        //return para;
                        e.Cancel = true;
                        return;
                    }

                    RaiseSaved();
                }
                else if (dlg == DialogResult.Cancel)
                {
                    //para[0] = false;
                    e.Cancel = true;
                }
                else if (dlg == DialogResult.No)
                {
                    //para[0] = true;
                    //para[1] = true;
                }
            }
        }
        private void OncmbCountryProvinceFirstEnter(object sender, EventArgs e)
        {
            _isEntercmbCountryProvince = true;
            //国家，省份控件初始化
            _countryProvinceList = this.Controller.GetCountryProvinceList(
              string.Empty,
              string.Empty,
              null,
              true,
              0);

            cmbCountryProvince.AllowMultSelect = false;
            cmbCountryProvince.RootValue = Guid.Empty;
            cmbCountryProvince.ParentMember = "ParentID";
            cmbCountryProvince.ValueMember = "ID";
            cmbCountryProvince.DisplayMember = LocalData.IsEnglish ? "EName" : "CName";
            cmbCountryProvince.DataSource = _countryProvinceList;
            cmbCountryProvince.EditValueChanged += new System.EventHandler(this.cmbCountryProvince_EditValueChanged);

            if (_currentCustomerInfo.ProvinceID != null && _currentCustomerInfo.ProvinceID != Guid.Empty)
            {
                cmbCountryProvince.InitSelectedNode(_currentCustomerInfo.ProvinceID);
            }
            else if (_currentCustomerInfo.CountryID != null && _currentCustomerInfo.CountryID != Guid.Empty)
            {
                cmbCountryProvince.InitSelectedNode(_currentCustomerInfo.CountryID);
            }
            else
            {
                cmbCountryProvince.InitSelectedNode(null);
            }
        }
        private void OncmbTypeFirstEnter(object sender, EventArgs e)
        {
            List<EnumHelper.ListItem<CustomerType>> customerTypes = EnumHelper.GetEnumValues<CustomerType>(LocalData.IsEnglish);
            customerTypes.RemoveAll(item => item.Value == CustomerType.Unknown);
            this.cmbType.Properties.BeginUpdate();
            cmbType.Properties.Items.Clear();
            //this.cmbType.Properties.Items.Add(new ImageComboBoxItem(string.Empty,DBNull.Value));
            foreach (var item in customerTypes)
            {
                cmbType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            this.cmbType.Properties.EndUpdate();
            this.cmbType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsDataSource, "Type", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
        }
        private void OncmbTaxidTypeFirstEnter(object sender, EventArgs e)
        {
            List<EnumHelper.ListItem<TaxType>> taxTypes = EnumHelper.GetEnumValues<TaxType>(LocalData.IsEnglish);
            this.cmbTaxidType.Properties.BeginUpdate();
            cmbTaxidType.Properties.Items.Clear();
            cmbTaxidType.Properties.Items.Add(new ImageComboBoxItem(string.Empty, System.DBNull.Value));
            foreach (var item in taxTypes)
            {
                cmbTaxidType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            this.cmbTaxidType.Properties.EndUpdate();
            this.cmbTaxidType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsDataSource, "TaxidType", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
        }
        private void OncmbPaymentTypeFirstEnter(object sender, EventArgs e)
        {
            List<DataDictionaryList> paymentTypes = this.Controller.GetDataDictionaryList(
                        DataDictionaryType.PaymentTerm,
                        true);

            cmbPaymentType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(string.Empty, Guid.Empty));
            this.cmbPaymentType.Properties.BeginUpdate();
            cmbPaymentType.Properties.Items.Clear();
            foreach (var item in paymentTypes)
            {
                cmbPaymentType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
            }
            this.cmbPaymentType.Properties.EndUpdate();
            this.cmbPaymentType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsDataSource, "PaymentTypeID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
           
        }
        /// <summary>
        /// 初始化下拉式控件的item以及其它一些控件的默认值
        /// </summary>
        private void InitControls()
        {
            //初始化可操作性       

            txtTel1.ToolTip = LocalData.IsEnglish ? "Country-Area-Tel-Ext" : "国际区号-区号-电话号码-分机号";
            txtTel2.ToolTip = LocalData.IsEnglish ? "Country-Area-Tel-Ext" : "国际区号-区号-电话号码-分机号";
            txtFax.ToolTip = LocalData.IsEnglish ? "Country-Area-Fax-Ext" : "国际区号-区号-传真号码-分机号";

            //从全局资源中取工具栏图标
            barSave.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Save_16;
            barCheck.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Check_16;
            barClose.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Close_16;

            this.cmbCountryProvince.OnFirstTimeEnter += this.OncmbCountryProvinceFirstEnter;
        

            if (!CommonUtility.GuidIsNullOrEmpty(_currentCustomerInfo.ProvinceID))
            {
                cmbCountryProvince.Text = _currentCustomerInfo.CountryName + " " + _currentCustomerInfo.ProvinceName;
                SetDataSourceForcmbCity(null, _currentCustomerInfo.ProvinceID, true);
            }
            else if (!CommonUtility.GuidIsNullOrEmpty(_currentCustomerInfo.CountryID))
            {
                cmbCountryProvince.Text = _currentCustomerInfo.CountryName;
                //cmbCountryProvince.SelectedValue = _currentCustomerInfo.CountryID;
                SetDataSourceForcmbCity(_currentCustomerInfo.CountryID, null, true);
            }

            //类型控件初始化
            if (this._currentCustomerInfo.Type != CustomerType.Unknown)
            {
                this.cmbType.ShowSelectedValue(_currentCustomerInfo.Type, ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<CustomerType>(_currentCustomerInfo.Type, LocalData.IsEnglish));
            }
            this.cmbType.OnFirstEnter += this.OncmbTypeFirstEnter;

            //税务类型控件初始化
            List<EnumHelper.ListItem<TaxType>> taxTypes = EnumHelper.GetEnumValues<TaxType>(LocalData.IsEnglish);
            this.cmbTaxidType.Properties.BeginUpdate();
            cmbTaxidType.Properties.Items.Clear();
            cmbTaxidType.Properties.Items.Add(new ImageComboBoxItem(string.Empty, System.DBNull.Value));
            foreach (var item in taxTypes)
            {
                cmbTaxidType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            this.cmbTaxidType.Properties.EndUpdate();
            this.cmbTaxidType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsDataSource, "TaxidType", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));

            if (_currentCustomerInfo.TaxIdType == null)
            {
                _currentCustomerInfo.TaxIdType = TaxType.ATIN;
               //this.cmbTaxidType.ShowSelectedValue(_currentCustomerInfo.TaxIdType.Value,ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<TaxType>(_currentCustomerInfo.TaxIdType.Value,LocalData.IsEnglish));
            }
                this.cmbTaxidType.ShowSelectedValue(_currentCustomerInfo.TaxIdType.Value, ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<TaxType>(_currentCustomerInfo.TaxIdType.Value, LocalData.IsEnglish));

            //this.cmbTaxidType.OnFirstEnter += this.OncmbTaxidTypeFirstEnter;

            //付款方式控件初始化
            if (!CommonUtility.GuidIsNullOrEmpty(_currentCustomerInfo.PaymentTypeID))
            {
                this.cmbPaymentType.ShowSelectedValue(_currentCustomerInfo.PaymentTypeID.Value, _currentCustomerInfo.PaymentTypeName);
            }
            this.cmbPaymentType.OnFirstEnter += this.OncmbPaymentTypeFirstEnter;


            this.cmbTradeTerm.OnFirstEnter += this.OncmbTradeTermFirstEnter;
            //贸易条款控件初始化

            var data = this.bsDataSource.DataSource as CustomerInfo;
            if (data != null)
            {
                data.BeginEdit();
            }
            if (string.IsNullOrEmpty(data.CBillName))
            {
                data.CBillName = data.CName;
            }
            if (string.IsNullOrEmpty(data.EBillName))
            {
                data.EBillName = data.EName;
            }
            this.txtCShortName.Focus();
        }
        private void OncmbTradeTermFirstEnter(object sender, EventArgs e)
        {
            List<DataDictionaryList> tradeTerms = this.Controller.GetDataDictionaryList(
                         DataDictionaryType.TradeTerm,
                         true);
            this.cmbTradeTerm.Properties.BeginUpdate();
            cmbTradeTerm.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(string.Empty, Guid.Empty));
            foreach (var item in tradeTerms)
            {
                cmbTradeTerm.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
            }
            this.cmbTradeTerm.Properties.EndUpdate();
        }
        #endregion

        #region 控制器

        /// <summary>
        /// 客户管理控制器
        /// </summary>
        [CreateNew]
        public CustomerManagerController Controller { get; set; }

        #endregion

        #region 本地控制逻辑

        private void txtCShortName_Leave(object sender, EventArgs e)
        {
            var tb = sender as DevExpress.XtraEditors.TextEdit;

            if (LocalData.IsEnglish)
            {
                if (tb.Name == txtCShortName.Name) return;

                if (txtKeyWord.Text.Trim() == string.Empty)
                {
                    if (txtEShortName.Text.Trim().Length > 50)
                        _currentCustomerInfo.KeyWord = txtKeyWord.Text = txtEShortName.Text.Trim().Substring(0, 50);
                    else
                        _currentCustomerInfo.KeyWord = txtKeyWord.Text = txtEShortName.Text.Trim();
                }
            }
            else
            {
                if (tb.Name == txtEShortName.Name) return;

                if (txtKeyWord.Text.Trim() == string.Empty)
                {
                    if (txtCShortName.Text.Trim().Length > 50)
                        _currentCustomerInfo.KeyWord = txtKeyWord.Text = txtCShortName.Text.Trim().Substring(0, 50);
                    else
                        _currentCustomerInfo.KeyWord = txtKeyWord.Text = txtCShortName.Text.Trim();
                }
            }
        }

        public string ValidateData()
        {
            string exmessage = "";
            #region 验证格式 
            if (string.IsNullOrEmpty(_currentCustomerInfo.EBillName) || string.IsNullOrEmpty(_currentCustomerInfo.CBillName))
            {
                exmessage += LocalData.IsEnglish ? "BillName must input." : "账单名称必须输入." + "\r\n";
            }
            if (string.IsNullOrEmpty(_currentCustomerInfo.EMail) && string.IsNullOrEmpty(_currentCustomerInfo.Fax)&&string.IsNullOrEmpty(_currentCustomerInfo.Tel1)&&string.IsNullOrEmpty(_currentCustomerInfo.Tel2))
            {
                exmessage += LocalData.IsEnglish ? "Tel,Fax and EMail one input at least." : "电话，传真和邮件至少有一个输入." + "\r\n";
            }
            
            return exmessage;

            #endregion
        }

        /*保存数据*/
        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EndEdit();

            //检测关键字是否填写正确(关键字必须是全称里连续的字符串)
            if (!string.IsNullOrEmpty(txtKeyWord.Text.Trim()) && txtEName.Text.Trim().Contains(txtKeyWord.Text.Trim()) == false && txtCName.Text.Trim().Contains(txtKeyWord.Text.Trim()) == false)
            {
                //this.errorProvider1.SetError(this.txtKeyWord, Utility.GetValueFromResource("KeyWordFormatError", "Format error"));
                DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "KeyWord format error!" : "关键字格式错误,关键字必须是全称里连续的字符串!", LocalData.IsEnglish ? "Tip" : "提示");
                this.txtKeyWord.Focus();
                return;
            }
            if (_currentCustomerInfo.Type!= CustomerType.Unknown)
            {
                if (((ComboBoxItem)cmbType.SelectedItem).Value.ToString() == CustomerType.Forwarding.ToString())
                {
                    if (chkIsAgentOfCarrier.Checked)
                    {
                        if (!LocalCommonServices.PermissionService.HaveActionPermission(CustomerManagerConstants.Common_changeCustomerType_SWY))
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "AgentOfCarrier only allows users to type  business 、the general manager and development to operate!!!" : "AgentOfCarrier only allows users to type  business 、the general manager and development to operate!!!", LocalData.IsEnglish ? "Tip" : "提示");
                            return;
                        }
                    }
                }
                else if (((ComboBoxItem)cmbType.SelectedItem).Value.ToString() == CustomerType.Carrier.ToString())
                {
                    //Carrier类型只允许用户为“远东区商务员”才能修改和新增；其他任何角色不能修改；
                    if (!LocalCommonServices.PermissionService.HaveActionPermission(CustomerManagerConstants.Common_changeCustomerType_YDQSWY))
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Carrier only allows users to type <BussinesManager> to operate!" : "船东类型只允许用户为<远东区商务员>进行操作", LocalData.IsEnglish ? "Tip" : "提示");
                        return;
                    }
                }
            }
            else
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), LocalData.IsEnglish ? "Customer Type must select": "客户类型必须选择.");
                return;
            }

            #region 验证格式

            Boolean flag = false;

            if (string.IsNullOrEmpty(_currentCustomerInfo.EMail) && string.IsNullOrEmpty(_currentCustomerInfo.Fax) && string.IsNullOrEmpty(_currentCustomerInfo.Tel1) && string.IsNullOrEmpty(_currentCustomerInfo.Tel2))
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), LocalData.IsEnglish ? "Tel,Fax and EMail one input at least." : "电话，传真和邮件至少有一个输入.");
                flag = true;
            }
            else
            {


                if (!string.IsNullOrEmpty(_currentCustomerInfo.Tel1))
                {
                    if (CheckLetters(_currentCustomerInfo.Tel1))
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), LocalData.IsEnglish ? "Tel1 format is not correct." : "电话1格式不正确.");
                        flag = true;
                    }
                    else
                    {
                        if (!CheckNumerText2(_currentCustomerInfo.Tel1))
                        {
                            LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), LocalData.IsEnglish ? "Tel1 format is not correct." : "电话1格式不正确.");
                            flag = true;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(_currentCustomerInfo.Tel2))
                {
                    if (CheckLetters(_currentCustomerInfo.Tel2))
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), LocalData.IsEnglish ? "Tel2 format is not correct." : "电话2格式不正确.");
                        flag = true;
                    }
                    else
                    {
                        if (!CheckNumerText2(_currentCustomerInfo.Tel2))
                        {
                            LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), LocalData.IsEnglish ? "Tel2 format is not correct." : "电话2格式不正确.");
                            flag = true;
                        }
                    }
                }

                //if (string.IsNullOrEmpty(_currentCustomerInfo.EMail) && string.IsNullOrEmpty(_currentCustomerInfo.Fax))
                //{
                //    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), LocalData.IsEnglish ? "Fax and EMail one input at least." : "传真和邮件至少有一个输入.");
                //    flag = true;
                //}
                //else
                //{
                if (!string.IsNullOrEmpty(_currentCustomerInfo.Fax))
                {
                    if (CheckLetters(_currentCustomerInfo.Fax))
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), LocalData.IsEnglish ? "Fax format is not correct." : "传真格式不正确.");
                        flag = true;
                    }
                    else
                    {
                        if (!CheckNumerText2(_currentCustomerInfo.Fax))
                        {
                            LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), LocalData.IsEnglish ? "Fax format is not correct." : "传真格式不正确.");
                            flag = true;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(_currentCustomerInfo.EMail))
                {
                    if (!CheckEmail(_currentCustomerInfo.EMail))
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), LocalData.IsEnglish ? "EMail format is not correct." : "邮件格式不正确.");
                        flag = true;
                    }
                }
                //}
            }

            if (flag) return;

            #endregion

            if(string.IsNullOrEmpty(_currentCustomerInfo.TaxIdNo))
            {
               MessageBoxService.ShowWarning(LocalData.IsEnglish ? "TaxID NO. can not be empty!" : "税务登记号必填", LocalData.IsEnglish ? "Tip" : "提示");
               txtTaxidNo.Focus();
                return;
            }
            try
            {
                //保存数据
                if (this.SaveData())
                {
                    //触发保存成功事件
                    if (this.Saved != null)
                    {
                        this.Saved(this.bsDataSource.DataSource);
                    }

                    _currentCustomerInfo.IsDirty = false;

                    //提示保存成功
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(
                        this.FindForm(),
                        "保存成功!");
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(
                  this.FindForm(),
                  "保存失败!");

                //设置错误信息
                LocalCommonServices.ErrorTrace.SetErrorInfo(
                    this.FindForm(),
                    ex);
            }
        }

        /*把中文信息直接拷贝到英文信息栏位*/
        private void btnToEnInfo_Click(object sender, EventArgs e)
        {
            txtEAddress.Text = txtCAddress.Text;
            txtEName.Text = txtCName.Text;
            txtEBillName.Text = txtCBillName.Text;
            txtEShortName.Text = txtCShortName.Text;
        }

        /*把英文信息直接拷贝到中文信息栏位*/
        private void btnToCnInfo_Click(object sender, EventArgs e)
        {
            txtCAddress.Text = txtEAddress.Text;
            txtCName.Text = txtEName.Text;
            txtCBillName.Text = txtEBillName.Text;
            txtCShortName.Text = txtEShortName.Text;
        }

        /*根据选择的国家或则省份，列出对应的城市*/
        private void cmbCountryProvince_EditValueChanged(object sender, EventArgs e)
        {
            cmbCity.Properties.Items.Clear();
        }

        private void cmbCity_GotFocus(object sender, EventArgs e)
        {
            if (_isEntercmbCountryProvince)
            {
                Guid? countryId = (Guid?)cmbCountryProvince.GetSelectedValues("ParentID");
                Guid? provinceId = null;
                if (countryId != null)   //countryId != null 说明当前选择的是省份，否则是国家
                {
                    provinceId = (Guid)cmbCountryProvince.SelectedValue;
                }
                else
                {
                    countryId = (Guid)cmbCountryProvince.SelectedValue;
                }

                SetDataSourceForcmbCity(countryId, provinceId, false);
            }
        }

        private void SetDataSourceForcmbCity(Guid? countryId, Guid? provinceId, bool isInit)
        {
            cmbCity.Properties.Items.Clear();
            if (CommonUtility.GuidIsNullOrEmpty(provinceId))  //如果只选择了国家，则不显示城市列表
            {
                return;
            }

            List<LocationList> cities = this.Controller.GetLocationList(
                countryId,
                provinceId,
                true);

            if (cities != null)
            {
                foreach (var item in cities)
                {
                    cmbCity.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
                }

                if (!isInit)
                {
                    cmbCity.EditValue = null;
                }
            }
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbType.EditValue != null && cmbType.EditValue.ToString() == CustomerType.Forwarding.ToString())
            {
                chkIsAgentOfCarrier.Visible = true;
            }
            else
            {
                chkIsAgentOfCarrier.Visible = false;
                _currentCustomerInfo.IsAgentOfCarrier = false;
            }

            if (cmbType.EditValue != null && (cmbType.EditValue.ToString() == CustomerType.Storage.ToString() || cmbType.EditValue.ToString() == CustomerType.Terminal.ToString()))
            {
                labFIRMCODE.Visible = true;
                txtFIRMCODE.Visible = true;
            }
            else
            {
                labFIRMCODE.Visible = false;
                txtFIRMCODE.Visible = false;
                txtFIRMCODE.Text = _currentCustomerInfo.FIRMCODE = string.Empty;
            }
        }

        private void barCheck_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CustomerInfo itemSelectd = bsDataSource.Current as CustomerInfo;
            if (itemSelectd != null)
            {
                bool itemHasChanged = _currentCustomerInfo.IsDirty;
                CustomerAuditing auditingForm = WorkItem.Items.AddNew<CustomerAuditing>();
                auditingForm.Text = LocalData.IsEnglish ? "Customer Auditing" : "客户审核";
                CustomerConfirmInfoForAuditing applyCodeData = new CustomerConfirmInfoForAuditing();
                ICP.Framework.CommonLibrary.Helper.ObjectHelper.CopyData(_applyCodeData, applyCodeData);
                auditingForm.ShowDialog(applyCodeData, ValidateTel(), this.ValidateEmailAndFax2(), itemSelectd.Code, ValidateData());

                if (auditingForm.DialogResult == DialogResult.OK)
                {
                    _currentCustomerInfo.UpdateDate = auditingForm.UpdateDate;
                    if (auditingForm.CurrentData.State == CustomerCodeApplyState.Passed)
                    {
                        _currentCustomerInfo.CheckedState = CustomerCodeApplyState.Passed;
                        _currentCustomerInfo.Code = auditingForm.Code;
                    }
                    else if (auditingForm.CurrentData.State == CustomerCodeApplyState.Unpassed)
                    {
                        _currentCustomerInfo.CheckedState = CustomerCodeApplyState.Unpassed;
                    }
                }

                //刷新主列表，即更改审核状态
                if (this.Saved != null)
                {
                    this.Saved(this.bsDataSource.DataSource);
                }

                if (!itemHasChanged)
                {
                    _currentCustomerInfo.IsDirty = false;
                }
            }
        }

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var findForm = this.FindForm();
            if (findForm != null) findForm.Close();
        }

        private bool ValidateTel()
        {
            bool flag = true;
            CustomerInfo current = bsDataSource.Current as CustomerInfo;
            if (CheckLetters(current.Tel1) || CheckLetters(current.Tel2))
            {
                flag = false;
            }
            else
            {
                flag = CheckNumerText2(current.Tel1) || CheckNumerText2(current.Tel2);
            }

            return flag;
        }

        private bool ValidateEmailAndFax2()
        {
            bool flag = true;
            CustomerInfo current = bsDataSource.Current as CustomerInfo;
            if (string.IsNullOrEmpty(current.EMail) && string.IsNullOrEmpty(current.Fax))
            {
                flag = false;
            }
            else
            {
                if (!string.IsNullOrEmpty(current.Fax) && string.IsNullOrEmpty(current.EMail))
                {
                    if (CheckLetters(current.Fax))
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = CheckNumerText2(current.Fax);
                    }
                }

                if (!string.IsNullOrEmpty(current.EMail) && string.IsNullOrEmpty(current.Fax))
                {
                    flag = CheckEmail(current.EMail);
                }
                else
                {
                    //check 是否包含字母
                    if (CheckLetters(current.Fax))
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = CheckNumerText2(current.Fax) || CheckEmail(current.EMail);
                    }
                }
            }

            return flag;
        }

        private bool CheckEmail(string text)
        {
            if (string.IsNullOrEmpty(text) == false)
            {
                if (CommonUtility.IsEmail(text.Trim()) == false)
                {
                    return false;
                }
            }
            else
                return false;

            return true;
        }

        private bool CheckLetters(string text)
        {
            if (string.IsNullOrEmpty(text) == false)
            {
                if (CommonUtility.IsLetter(text.Trim()) == false)
                {
                    return false;
                }
            }
            else
                return false;

            return true;
        }

        private bool CheckNumerText2(string txt)
        {
            if (string.IsNullOrEmpty(txt) == false)
            {
                if (CommonUtility.IsTEL(txt.Trim()) == false)
                {
                    return false;
                }
            }
            else
                return false;

            return true;
        }

        #endregion

        #region IEditPart接口

        /// <summary>
        /// 数据源
        /// </summary>
        public override object DataSource
        {
            get
            {
                return this.bsDataSource.DataSource;
            }
            set
            {
                this.SuspendLayout();
                this.bsDataSource.DataSource = value;
                this.ResumeLayout(false);
            }
        }

        /// <summary>
        /// 保存完成触发该事件
        /// </summary>
        public override event SavedHandler Saved;

        /// <summary>
        /// 结束编辑
        /// </summary>
        public override void EndEdit()
        {
            this.bsDataSource.EndEdit();

            if (_isEntercmbCountryProvince)
            {
                //处理界面上不能直接绑定的值
                Guid? countryId = (Guid?)cmbCountryProvince.GetSelectedValues("ParentID");
                Guid? provinceId = null;
                if (countryId != null)
                {
                    if (cmbCountryProvince.SelectedValue != null)
                    {
                        provinceId = (Guid)cmbCountryProvince.SelectedValue;
                    }
                }
                else
                {
                    if (cmbCountryProvince.SelectedValue != null)
                    {
                        countryId = (Guid)cmbCountryProvince.SelectedValue;
                    }
                }

                CustomerInfo customer = (CustomerInfo)this.bsDataSource.DataSource;
                customer.CountryID = countryId.HasValue ? countryId.Value : Guid.Empty;
                customer.ProvinceID = provinceId;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="values"></param>
        public override void Init(IDictionary<string, object> values)
        {
            if (values != null
                && values.ContainsKey("CustomerList"))
            {
                CustomerList customerlist = (CustomerList)values["CustomerList"];
                if (customerlist == null)
                {
                    this.Enabled = false;
                    return;
                }
                else
                {
                    this.Enabled = true;
                }

                barCheck.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                if (customerlist.ID == Guid.Empty)
                {
                    _currentCustomerInfo = new CustomerInfo();
                    _currentCustomerInfo.CName = customerlist.CName;
                    _currentCustomerInfo.EName = customerlist.EName;
                    _currentCustomerInfo.CreateByID = LocalData.UserInfo.LoginID;
                    _currentCustomerInfo.CreateByName = LocalData.UserInfo.LoginName;
                    _currentCustomerInfo.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    txtCShortName.Focus();
                }
                else
                {
                    _currentCustomerInfo = this.Controller.GetCustomerInfo(customerlist.ID);
                }

                if (!string.IsNullOrEmpty(_currentCustomerInfo.TaxIdNo))
                {
                    _currentCustomerInfo.TaxIdNo = _currentCustomerInfo.TaxIdNo.Replace(ICP.Framework.CommonLibrary.Common.GlobalConstants.DividedSymbol, ICP.Framework.CommonLibrary.Common.GlobalConstants.ShowDividedSymbol);
                }
                if (!string.IsNullOrEmpty(_currentCustomerInfo.BankAccountNo))
                {
                    _currentCustomerInfo.BankAccountNo = _currentCustomerInfo.BankAccountNo.Replace(ICP.Framework.CommonLibrary.Common.GlobalConstants.DividedSymbol, ICP.Framework.CommonLibrary.Common.GlobalConstants.ShowDividedSymbol);
                }


                this.bsDataSource.DataSource = _currentCustomerInfo;
                _currentCustomerInfo.IsDirty = false;

                if (_currentCustomerInfo.ID != null && _currentCustomerInfo.ID != Guid.Empty)
                {
                    _applyCodeData = this.Controller.GetLatelyCustomerConfirmInfo(_currentCustomerInfo.ID);
                    if (_applyCodeData != null)
                    {
                        if (LocalCommonServices.PermissionService.HaveActionPermission(CustomerManagerConstants.Common_checkCustomer))
                        {
                            barCheck.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        }
                    }
                }

                if (_currentCustomerInfo.Type == CustomerType.Forwarding)
                {
                    chkIsAgentOfCarrier.Visible = true;
                }

                if (_currentCustomerInfo.Type == CustomerType.Storage || _currentCustomerInfo.Type == CustomerType.Terminal)
                {
                    labFIRMCODE.Visible = true;
                    txtFIRMCODE.Visible = true;
                }

                if (LocalCommonServices.PermissionService.HaveActionPermission(CustomerManagerConstants.Common_changeCustomerType))
                {
                    chkIsAgentOfCarrier.Enabled = true;
                }
                else
                {
                    chkIsAgentOfCarrier.Enabled = false;
                }

                if (LocalCommonServices.PermissionService.HaveActionPermission(CustomerManagerConstants.Common_checkCustomer))
                {
                    this.txtCode.Enabled = true;
                    this.txtCode.Properties.ReadOnly = false;
                }
                else
                {
                    this.txtCode.Properties.ReadOnly = true;
                }

                if (cmbType.Text == CustomerType.Carrier.ToString()
                && !LocalCommonServices.PermissionService.HaveActionPermission(CustomerManagerConstants.Common_changeCustomerType_YDQSWY))
                {
                    this.cmbType.Enabled = false;
                }
                else
                {
                    this.cmbType.Enabled = true;
                }
            }

            _currentCustomerInfo.BeginEdit();
        }

        public bool BeforeParentChanged()
        {
            //EndEdit();
            CustomerInfo currentData = bsDataSource.Current as CustomerInfo;
            if (currentData == null) return false;

            if (currentData.IsDirty)
            {
                DialogResult dlg = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "The data was changed,Sure save?" : "数据有更改是否保存?", LocalData.IsEnglish ? "Confirm" : "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dlg != DialogResult.OK)
                {
                    return false;
                }

                if (currentData.Validate() == false)
                {
                    return false;
                }

                SaveData();
            }
            else if (currentData.IsNew)
            {
                DialogResult dlg = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "The new data is UnSave,Sure save?" : "新增的数据未保存,是否保存?", LocalData.IsEnglish ? "Confirm" : "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dlg != DialogResult.OK)
                {
                    return false;
                }

                if (currentData.Validate() == false)
                {
                    return false;
                }

                SaveData();
            }

            return true;
        }

        /// <summary>
        /// 触发保存事件
        /// </summary>
        public override void RaiseSaved()
        {
            this.SaveData();

            if (this.Saved != null)
            {
                this.Saved(this.DataSource);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public override bool SaveData()
        {
            //获取当前对象
            CustomerInfo customer = (CustomerInfo)this.bsDataSource.DataSource;
            //通过Controller提交数据
            SingleResultData result = this.Controller.SaveCustomerInfo(customer);
            if (result == null)
            {
                return false;
            }
            else
            {
                //更改当前对象版本号
                customer.ID = result.ID;
                customer.UpdateDate = result.UpdateDate;

                string strCountryProvince = string.Empty;
                if (!string.IsNullOrEmpty(cmbCountryProvince.EditValue.ToString()))
                {
                    strCountryProvince += cmbCountryProvince.EditValue.ToString();
                }

                if (cmbCity.SelectedItem != null && !string.IsNullOrEmpty(cmbCity.SelectedItem.ToString()))
                {
                    if (!string.IsNullOrEmpty(strCountryProvince))
                    {
                        strCountryProvince += " ";
                    }

                    strCountryProvince += cmbCity.SelectedItem.ToString();
                }

                customer.CountryProvinceName = strCountryProvince;
                return true;
            }
        }

        #endregion

        #region 设计器生成代码

        private System.Windows.Forms.BindingSource bsDataSource;
        private System.ComponentModel.IContainer components;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navName;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private System.Windows.Forms.GroupBox groupCNInfo;
        private DevExpress.XtraEditors.TextEdit txtCShortName;
        private DevExpress.XtraEditors.TextEdit txtCBillName;
        private DevExpress.XtraEditors.TextEdit txtCName;
        private DevExpress.XtraEditors.LabelControl labCShortName;
        private DevExpress.XtraEditors.LabelControl labCBillName;
        private DevExpress.XtraEditors.LabelControl labCName;
        private DevExpress.XtraEditors.LabelControl labCAddress;
        private DevExpress.XtraEditors.MemoEdit txtCAddress;
        private System.Windows.Forms.GroupBox groupENInfo;
        private DevExpress.XtraEditors.TextEdit txtEShortName;
        private DevExpress.XtraEditors.TextEdit txtEBillName;
        private DevExpress.XtraEditors.TextEdit txtEName;
        private DevExpress.XtraEditors.LabelControl labEShortName;
        private DevExpress.XtraEditors.LabelControl labEBillName;
        private DevExpress.XtraEditors.LabelControl labEName;
        private DevExpress.XtraEditors.LabelControl labEAddress;
        private DevExpress.XtraEditors.MemoEdit txtEAddress;
        private DevExpress.XtraEditors.SimpleButton btnToEnInfo;
        private DevExpress.XtraEditors.SimpleButton btnToCnInfo;
        private DevExpress.XtraEditors.LabelControl labKeyWord;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.LabelControl labCode;
        private DevExpress.XtraEditors.CheckEdit chkIsAgentOfCarrier;
        private DevExpress.XtraEditors.LabelControl labType;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbType;
        private DevExpress.XtraEditors.TextEdit txtKeyWord;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer3;
        private DevExpress.XtraEditors.LabelControl labCity;
        private DevExpress.XtraEditors.LabelControl labPostCode;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbCity;
        private DevExpress.XtraEditors.LabelControl labHomepage;
        private DevExpress.XtraEditors.LabelControl labTel1;
        private DevExpress.XtraEditors.LabelControl labEmail;
        private DevExpress.XtraEditors.TextEdit txtTel1;
        private DevExpress.XtraEditors.LabelControl labFax;
        private DevExpress.XtraEditors.LabelControl labGeography;
        private DevExpress.XtraEditors.LabelControl labEnterpriseCodeType;
        private DevExpress.XtraEditors.LabelControl labEnterpriseCode;
        private DevExpress.XtraEditors.LabelControl labTel2;
        private DevExpress.XtraEditors.TextEdit txtTel2;
        private DevExpress.XtraEditors.TextEdit txtPostCode;
        private DevExpress.XtraEditors.TextEdit txtFax;
        private DevExpress.XtraEditors.TextEdit txtEnterpriseCodeType;
        private DevExpress.XtraEditors.TextEdit txtEnterpriseCode;
        private DevExpress.XtraEditors.TextEdit txtHomepage;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer4;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbTaxidType;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbTradeTerm;
        private DevExpress.XtraEditors.LabelControl labTaxIdType;
        private DevExpress.XtraEditors.LabelControl labGoodsType;
        private DevExpress.XtraEditors.LabelControl labPaymentType;
        private DevExpress.XtraEditors.TextEdit txtTaxidNo;
        private DevExpress.XtraEditors.LabelControl labTradeTerm;
        private DevExpress.XtraEditors.LabelControl labTaxIdNo;
        private DevExpress.XtraEditors.LabelControl labTerm;
        private DevExpress.XtraEditors.SpinEdit numCreditLimit;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbPaymentType;
        private DevExpress.XtraEditors.LabelControl labCreditLimit;
        private DevExpress.XtraEditors.SpinEdit numTerm;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer5;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private DevExpress.XtraNavBar.NavBarGroup navContactInfo;
        private DevExpress.XtraNavBar.NavBarGroup navFinanceInfo;
        private DevExpress.XtraNavBar.NavBarGroup navOtherInfo;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManagerCustomer;
        private DevExpress.XtraBars.Bar bar1;
        private ICP.Common.UI.Controls.LWComboBoxTree cmbCountryProvince;
        private DevExpress.XtraEditors.CheckEdit checkIsCompany;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarButtonItem barCheck;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerManagerEditPart));
            this.bsDataSource = new System.Windows.Forms.BindingSource(this.components);
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.txtCShortName = new DevExpress.XtraEditors.TextEdit();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.cmbType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.txtKeyWord = new DevExpress.XtraEditors.TextEdit();
            this.cmbCity = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.txtTel1 = new DevExpress.XtraEditors.TextEdit();
            this.txtTel2 = new DevExpress.XtraEditors.TextEdit();
            this.txtPostCode = new DevExpress.XtraEditors.TextEdit();
            this.txtFax = new DevExpress.XtraEditors.TextEdit();
            this.txtEnterpriseCodeType = new DevExpress.XtraEditors.TextEdit();
            this.txtEnterpriseCode = new DevExpress.XtraEditors.TextEdit();
            this.txtHomepage = new DevExpress.XtraEditors.TextEdit();
            this.txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.cmbTaxidType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbTradeTerm = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.txtTaxidNo = new DevExpress.XtraEditors.TextEdit();
            this.numCreditLimit = new DevExpress.XtraEditors.SpinEdit();
            this.cmbPaymentType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.numTerm = new DevExpress.XtraEditors.SpinEdit();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.txtCBillName = new DevExpress.XtraEditors.TextEdit();
            this.txtCName = new DevExpress.XtraEditors.TextEdit();
            this.txtCAddress = new DevExpress.XtraEditors.MemoEdit();
            this.txtEShortName = new DevExpress.XtraEditors.TextEdit();
            this.txtEBillName = new DevExpress.XtraEditors.TextEdit();
            this.txtEName = new DevExpress.XtraEditors.TextEdit();
            this.txtEAddress = new DevExpress.XtraEditors.MemoEdit();
            this.txtBankAccountNo = new DevExpress.XtraEditors.TextEdit();
            this.txtFIRMCODE = new DevExpress.XtraEditors.TextEdit();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navName = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.checkIsCompany = new DevExpress.XtraEditors.CheckEdit();
            this.labGoodsType = new DevExpress.XtraEditors.LabelControl();
            this.chkIsAgentOfCarrier = new DevExpress.XtraEditors.CheckEdit();
            this.labFIRMCODE = new DevExpress.XtraEditors.LabelControl();
            this.labType = new DevExpress.XtraEditors.LabelControl();
            this.labKeyWord = new DevExpress.XtraEditors.LabelControl();
            this.labTradeTerm = new DevExpress.XtraEditors.LabelControl();
            this.groupCNInfo = new System.Windows.Forms.GroupBox();
            this.labCShortName = new DevExpress.XtraEditors.LabelControl();
            this.labCBillName = new DevExpress.XtraEditors.LabelControl();
            this.labCName = new DevExpress.XtraEditors.LabelControl();
            this.labCAddress = new DevExpress.XtraEditors.LabelControl();
            this.labCode = new DevExpress.XtraEditors.LabelControl();
            this.groupENInfo = new System.Windows.Forms.GroupBox();
            this.labEShortName = new DevExpress.XtraEditors.LabelControl();
            this.labEBillName = new DevExpress.XtraEditors.LabelControl();
            this.labEName = new DevExpress.XtraEditors.LabelControl();
            this.labEAddress = new DevExpress.XtraEditors.LabelControl();
            this.btnToEnInfo = new DevExpress.XtraEditors.SimpleButton();
            this.btnToCnInfo = new DevExpress.XtraEditors.SimpleButton();
            this.navBarGroupControlContainer3 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.cmbCountryProvince = new ICP.Common.UI.Controls.LWComboBoxTree();
            this.labCity = new DevExpress.XtraEditors.LabelControl();
            this.labPostCode = new DevExpress.XtraEditors.LabelControl();
            this.labHomepage = new DevExpress.XtraEditors.LabelControl();
            this.labTel1 = new DevExpress.XtraEditors.LabelControl();
            this.labEmail = new DevExpress.XtraEditors.LabelControl();
            this.labFax = new DevExpress.XtraEditors.LabelControl();
            this.labGeography = new DevExpress.XtraEditors.LabelControl();
            this.labEnterpriseCodeType = new DevExpress.XtraEditors.LabelControl();
            this.labEnterpriseCode = new DevExpress.XtraEditors.LabelControl();
            this.labTel2 = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer4 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.labPaymentType = new DevExpress.XtraEditors.LabelControl();
            this.labTaxIdType = new DevExpress.XtraEditors.LabelControl();
            this.labTaxIdNo = new DevExpress.XtraEditors.LabelControl();
            this.labTerm = new DevExpress.XtraEditors.LabelControl();
            this.labCreditLimit = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer5 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.navContactInfo = new DevExpress.XtraNavBar.NavBarGroup();
            this.navFinanceInfo = new DevExpress.XtraNavBar.NavBarGroup();
            this.navOtherInfo = new DevExpress.XtraNavBar.NavBarGroup();
            this.barManagerCustomer = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barCheck = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.bsDataSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCShortName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKeyWord.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTel1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTel2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPostCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEnterpriseCodeType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEnterpriseCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHomepage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTaxidType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTradeTerm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaxidNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCreditLimit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaymentType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTerm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCBillName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEShortName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEBillName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBankAccountNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFIRMCODE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsAgentOfCarrier.Properties)).BeginInit();
            this.groupCNInfo.SuspendLayout();
            this.groupENInfo.SuspendLayout();
            this.navBarGroupControlContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCountryProvince.Properties)).BeginInit();
            this.navBarGroupControlContainer4.SuspendLayout();
            this.navBarGroupControlContainer5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManagerCustomer)).BeginInit();
            this.SuspendLayout();
            // 
            // bsDataSource
            // 
            this.bsDataSource.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.CustomerInfo);
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bsDataSource;
            // 
            // txtCShortName
            // 
            this.txtCShortName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDataSource, "CShortName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtCShortName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtCShortName.Location = new System.Drawing.Point(83, 14);
            this.txtCShortName.Name = "txtCShortName";
            this.txtCShortName.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtCShortName.Properties.Appearance.Options.UseBackColor = true;
            this.txtCShortName.Properties.MaxLength = 100;
            this.txtCShortName.Size = new System.Drawing.Size(246, 21);
            this.txtCShortName.TabIndex = 0;
            this.txtCShortName.Leave += new System.EventHandler(this.txtCShortName_Leave);
            // 
            // txtCode
            // 
            this.txtCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDataSource, "Code", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCode.Enabled = false;
            this.dxErrorProvider1.SetIconAlignment(this.txtCode, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtCode.Location = new System.Drawing.Point(86, 143);
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.MaxLength = 20;
            this.txtCode.Size = new System.Drawing.Size(246, 21);
            this.txtCode.TabIndex = 2;
            // 
            // cmbType
            // 
            this.dxErrorProvider1.SetIconAlignment(this.cmbType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbType.Location = new System.Drawing.Point(485, 143);
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Size = new System.Drawing.Size(155, 21);
            this.cmbType.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbType.TabIndex = 5;
            // 
            // txtKeyWord
            // 
            this.txtKeyWord.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDataSource, "KeyWord", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtKeyWord, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtKeyWord.Location = new System.Drawing.Point(86, 167);
            this.txtKeyWord.Name = "txtKeyWord";
            this.txtKeyWord.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtKeyWord.Properties.Appearance.Options.UseBackColor = true;
            this.txtKeyWord.Properties.MaxLength = 50;
            this.txtKeyWord.Size = new System.Drawing.Size(246, 21);
            this.txtKeyWord.TabIndex = 3;
            // 
            // cmbCity
            // 
            this.cmbCity.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsDataSource, "CityID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbCity, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbCity.Location = new System.Drawing.Point(86, 54);
            this.cmbCity.Name = "cmbCity";
            this.cmbCity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCity.Size = new System.Drawing.Size(246, 21);
            this.cmbCity.TabIndex = 1;
            this.cmbCity.GotFocus += new System.EventHandler(this.cmbCity_GotFocus);
            // 
            // txtTel1
            // 
            this.txtTel1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsDataSource, "Tel1", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtTel1, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtTel1.Location = new System.Drawing.Point(86, 77);
            this.txtTel1.Name = "txtTel1";
            this.txtTel1.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtTel1.Properties.Appearance.Options.UseBackColor = true;
            this.txtTel1.Properties.MaxLength = 30;
            this.txtTel1.Size = new System.Drawing.Size(246, 21);
            this.txtTel1.TabIndex = 2;
            // 
            // txtTel2
            // 
            this.txtTel2.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsDataSource, "Tel2", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtTel2, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtTel2.Location = new System.Drawing.Point(86, 100);
            this.txtTel2.Name = "txtTel2";
            this.txtTel2.Properties.MaxLength = 30;
            this.txtTel2.Size = new System.Drawing.Size(246, 21);
            this.txtTel2.TabIndex = 3;
            // 
            // txtPostCode
            // 
            this.txtPostCode.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsDataSource, "PostCode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtPostCode, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtPostCode.Location = new System.Drawing.Point(485, 100);
            this.txtPostCode.Name = "txtPostCode";
            this.txtPostCode.Properties.MaxLength = 10;
            this.txtPostCode.Size = new System.Drawing.Size(246, 21);
            this.txtPostCode.TabIndex = 7;
            // 
            // txtFax
            // 
            this.txtFax.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsDataSource, "Fax", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtFax, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtFax.Location = new System.Drawing.Point(485, 3);
            this.txtFax.Name = "txtFax";
            this.txtFax.Properties.MaxLength = 30;
            this.txtFax.Size = new System.Drawing.Size(246, 21);
            this.txtFax.TabIndex = 4;
            // 
            // txtEnterpriseCodeType
            // 
            this.txtEnterpriseCodeType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsDataSource, "EnterpriseCodeType", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtEnterpriseCodeType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtEnterpriseCodeType.Location = new System.Drawing.Point(86, 29);
            this.txtEnterpriseCodeType.Name = "txtEnterpriseCodeType";
            this.txtEnterpriseCodeType.Properties.MaxLength = 30;
            this.txtEnterpriseCodeType.Size = new System.Drawing.Size(246, 21);
            this.txtEnterpriseCodeType.TabIndex = 4;
            // 
            // txtEnterpriseCode
            // 
            this.txtEnterpriseCode.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsDataSource, "EnterpriseCode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtEnterpriseCode, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtEnterpriseCode.Location = new System.Drawing.Point(485, 29);
            this.txtEnterpriseCode.Name = "txtEnterpriseCode";
            this.txtEnterpriseCode.Properties.MaxLength = 30;
            this.txtEnterpriseCode.Size = new System.Drawing.Size(246, 21);
            this.txtEnterpriseCode.TabIndex = 4;
            // 
            // txtHomepage
            // 
            this.txtHomepage.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsDataSource, "Homepage", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtHomepage, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtHomepage.Location = new System.Drawing.Point(485, 54);
            this.txtHomepage.Name = "txtHomepage";
            this.txtHomepage.Properties.MaxLength = 200;
            this.txtHomepage.Size = new System.Drawing.Size(246, 21);
            this.txtHomepage.TabIndex = 5;
            // 
            // txtEmail
            // 
            this.txtEmail.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsDataSource, "EMail", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtEmail, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtEmail.Location = new System.Drawing.Point(485, 77);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Properties.MaxLength = 100;
            this.txtEmail.Size = new System.Drawing.Size(246, 21);
            this.txtEmail.TabIndex = 6;
            // 
            // cmbTaxidType
            // 
            this.dxErrorProvider1.SetIconAlignment(this.cmbTaxidType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbTaxidType.Location = new System.Drawing.Point(86, 3);
            this.cmbTaxidType.Name = "cmbTaxidType";
            this.cmbTaxidType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbTaxidType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbTaxidType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTaxidType.Size = new System.Drawing.Size(155, 21);
            this.cmbTaxidType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbTaxidType.TabIndex = 0;
            // 
            // cmbTradeTerm
            // 
            this.cmbTradeTerm.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsDataSource, "TradeTermID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbTradeTerm, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbTradeTerm.Location = new System.Drawing.Point(485, 167);
            this.cmbTradeTerm.Name = "cmbTradeTerm";
            this.cmbTradeTerm.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbTradeTerm.Properties.Appearance.Options.UseBackColor = true;
            this.cmbTradeTerm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTradeTerm.Size = new System.Drawing.Size(155, 21);
            this.cmbTradeTerm.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbTradeTerm.TabIndex = 7;
            // 
            // txtTaxidNo
            // 
            this.txtTaxidNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDataSource, "TaxIdNo", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtTaxidNo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtTaxidNo.Location = new System.Drawing.Point(86, 27);
            this.txtTaxidNo.Name = "txtTaxidNo";
            this.txtTaxidNo.Properties.MaxLength = 40;
            this.txtTaxidNo.Size = new System.Drawing.Size(155, 21);
            this.txtTaxidNo.TabIndex = 1;
            // 
            // numCreditLimit
            // 
            this.numCreditLimit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsDataSource, "CreditLimit", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numCreditLimit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numCreditLimit.Enabled = false;
            this.dxErrorProvider1.SetIconAlignment(this.numCreditLimit, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.numCreditLimit.Location = new System.Drawing.Point(324, 3);
            this.numCreditLimit.Name = "numCreditLimit";
            this.numCreditLimit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numCreditLimit.Properties.IsFloatValue = false;
            this.numCreditLimit.Properties.Mask.EditMask = "N00";
            this.numCreditLimit.Size = new System.Drawing.Size(155, 21);
            this.numCreditLimit.TabIndex = 2;
            // 
            // cmbPaymentType
            // 
            this.dxErrorProvider1.SetIconAlignment(this.cmbPaymentType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbPaymentType.Location = new System.Drawing.Point(576, 3);
            this.cmbPaymentType.Name = "cmbPaymentType";
            this.cmbPaymentType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbPaymentType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbPaymentType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPaymentType.Size = new System.Drawing.Size(155, 21);
            this.cmbPaymentType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbPaymentType.TabIndex = 4;
            // 
            // numTerm
            // 
            this.numTerm.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsDataSource, "Term", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numTerm.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numTerm.Enabled = false;
            this.dxErrorProvider1.SetIconAlignment(this.numTerm, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.numTerm.Location = new System.Drawing.Point(324, 26);
            this.numTerm.Name = "numTerm";
            this.numTerm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numTerm.Properties.IsFloatValue = false;
            this.numTerm.Properties.Mask.EditMask = "N00";
            this.numTerm.Size = new System.Drawing.Size(155, 21);
            this.numTerm.TabIndex = 3;
            // 
            // txtRemark
            // 
            this.txtRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDataSource, "Remark", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtRemark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dxErrorProvider1.SetIconAlignment(this.txtRemark, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtRemark.Location = new System.Drawing.Point(0, 0);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(752, 78);
            this.txtRemark.TabIndex = 0;
            // 
            // txtCBillName
            // 
            this.txtCBillName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDataSource, "CBillName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtCBillName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtCBillName.Location = new System.Drawing.Point(83, 62);
            this.txtCBillName.Name = "txtCBillName";
            this.txtCBillName.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtCBillName.Properties.Appearance.Options.UseBackColor = true;
            this.txtCBillName.Properties.MaxLength = 200;
            this.txtCBillName.Size = new System.Drawing.Size(246, 21);
            this.txtCBillName.TabIndex = 2;
            // 
            // txtCName
            // 
            this.txtCName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDataSource, "CName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtCName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtCName.Location = new System.Drawing.Point(83, 38);
            this.txtCName.Name = "txtCName";
            this.txtCName.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtCName.Properties.Appearance.Options.UseBackColor = true;
            this.txtCName.Properties.MaxLength = 200;
            this.txtCName.Size = new System.Drawing.Size(246, 21);
            this.txtCName.TabIndex = 1;
            // 
            // txtCAddress
            // 
            this.txtCAddress.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDataSource, "CAddress", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtCAddress, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtCAddress.Location = new System.Drawing.Point(83, 87);
            this.txtCAddress.Name = "txtCAddress";
            this.txtCAddress.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtCAddress.Properties.Appearance.Options.UseBackColor = true;
            this.txtCAddress.Properties.MaxLength = 200;
            this.txtCAddress.Size = new System.Drawing.Size(246, 44);
            this.txtCAddress.TabIndex = 3;
            // 
            // txtEShortName
            // 
            this.txtEShortName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDataSource, "EShortName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtEShortName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtEShortName.Location = new System.Drawing.Point(86, 14);
            this.txtEShortName.Name = "txtEShortName";
            this.txtEShortName.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtEShortName.Properties.Appearance.Options.UseBackColor = true;
            this.txtEShortName.Properties.MaxLength = 100;
            this.txtEShortName.Size = new System.Drawing.Size(246, 21);
            this.txtEShortName.TabIndex = 0;
            this.txtEShortName.Leave += new System.EventHandler(this.txtCShortName_Leave);
            // 
            // txtEBillName
            // 
            this.txtEBillName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDataSource, "EBillName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtEBillName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtEBillName.Location = new System.Drawing.Point(86, 62);
            this.txtEBillName.Name = "txtEBillName";
            this.txtEBillName.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtEBillName.Properties.Appearance.Options.UseBackColor = true;
            this.txtEBillName.Properties.MaxLength = 200;
            this.txtEBillName.Size = new System.Drawing.Size(246, 21);
            this.txtEBillName.TabIndex = 2;
            // 
            // txtEName
            // 
            this.txtEName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDataSource, "EName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtEName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtEName.Location = new System.Drawing.Point(86, 38);
            this.txtEName.Name = "txtEName";
            this.txtEName.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtEName.Properties.Appearance.Options.UseBackColor = true;
            this.txtEName.Properties.MaxLength = 200;
            this.txtEName.Size = new System.Drawing.Size(246, 21);
            this.txtEName.TabIndex = 1;
            // 
            // txtEAddress
            // 
            this.txtEAddress.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDataSource, "EAddress", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtEAddress, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtEAddress.Location = new System.Drawing.Point(86, 87);
            this.txtEAddress.Name = "txtEAddress";
            this.txtEAddress.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtEAddress.Properties.Appearance.Options.UseBackColor = true;
            this.txtEAddress.Properties.MaxLength = 200;
            this.txtEAddress.Size = new System.Drawing.Size(246, 44);
            this.txtEAddress.TabIndex = 3;
            // 
            // txtBankAccountNo
            // 
            this.txtBankAccountNo.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsDataSource, "BankAccountNo", true));
            this.dxErrorProvider1.SetIconAlignment(this.txtBankAccountNo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtBankAccountNo.Location = new System.Drawing.Point(576, 26);
            this.txtBankAccountNo.Name = "txtBankAccountNo";
            this.txtBankAccountNo.Properties.MaxLength = 100;
            this.txtBankAccountNo.Size = new System.Drawing.Size(155, 21);
            this.txtBankAccountNo.TabIndex = 5;
            // 
            // txtFIRMCODE
            // 
            this.txtFIRMCODE.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDataSource, "FIRMCODE", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtFIRMCODE.Location = new System.Drawing.Point(485, 191);
            this.txtFIRMCODE.Name = "txtFIRMCODE";
            this.txtFIRMCODE.Properties.MaxLength = 20;
            this.txtFIRMCODE.Size = new System.Drawing.Size(155, 21);
            this.txtFIRMCODE.TabIndex = 8;
            this.txtFIRMCODE.Visible = false;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navName;
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer3);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer4);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer5);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navName,
            this.navContactInfo,
            this.navFinanceInfo,
            this.navOtherInfo});
            this.navBarControl1.Location = new System.Drawing.Point(0, 26);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 755;
            this.navBarControl1.Size = new System.Drawing.Size(756, 556);
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navName
            // 
            this.navName.Caption = "基本信息";
            this.navName.ControlContainer = this.navBarGroupControlContainer1;
            this.navName.DragDropFlags = DevExpress.XtraNavBar.NavBarDragDrop.AllowDrag;
            this.navName.Expanded = true;
            this.navName.GroupClientHeight = 216;
            this.navName.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navName.Name = "navName";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.checkIsCompany);
            this.navBarGroupControlContainer1.Controls.Add(this.labGoodsType);
            this.navBarGroupControlContainer1.Controls.Add(this.chkIsAgentOfCarrier);
            this.navBarGroupControlContainer1.Controls.Add(this.labFIRMCODE);
            this.navBarGroupControlContainer1.Controls.Add(this.txtFIRMCODE);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbTradeTerm);
            this.navBarGroupControlContainer1.Controls.Add(this.labType);
            this.navBarGroupControlContainer1.Controls.Add(this.labKeyWord);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbType);
            this.navBarGroupControlContainer1.Controls.Add(this.labTradeTerm);
            this.navBarGroupControlContainer1.Controls.Add(this.groupCNInfo);
            this.navBarGroupControlContainer1.Controls.Add(this.txtCode);
            this.navBarGroupControlContainer1.Controls.Add(this.labCode);
            this.navBarGroupControlContainer1.Controls.Add(this.groupENInfo);
            this.navBarGroupControlContainer1.Controls.Add(this.txtKeyWord);
            this.navBarGroupControlContainer1.Controls.Add(this.btnToEnInfo);
            this.navBarGroupControlContainer1.Controls.Add(this.btnToCnInfo);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(752, 214);
            this.navBarGroupControlContainer1.TabIndex = 0;
            // 
            // checkIsCompany
            // 
            this.checkIsCompany.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsDataSource, "IsCompany", true));
            this.checkIsCompany.Location = new System.Drawing.Point(84, 194);
            this.checkIsCompany.Name = "checkIsCompany";
            this.checkIsCompany.Properties.Caption = "公司货";
            this.checkIsCompany.Size = new System.Drawing.Size(64, 19);
            this.checkIsCompany.TabIndex = 4;
            // 
            // labGoodsType
            // 
            this.labGoodsType.Location = new System.Drawing.Point(10, 195);
            this.labGoodsType.Name = "labGoodsType";
            this.labGoodsType.Size = new System.Drawing.Size(48, 14);
            this.labGoodsType.TabIndex = 62;
            this.labGoodsType.Text = "揽货方式";
            // 
            // chkIsAgentOfCarrier
            // 
            this.chkIsAgentOfCarrier.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsDataSource, "IsAgentOfCarrier", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkIsAgentOfCarrier.Location = new System.Drawing.Point(651, 144);
            this.chkIsAgentOfCarrier.Name = "chkIsAgentOfCarrier";
            this.chkIsAgentOfCarrier.Properties.Caption = "承运人";
            this.chkIsAgentOfCarrier.Size = new System.Drawing.Size(64, 19);
            this.chkIsAgentOfCarrier.TabIndex = 6;
            this.chkIsAgentOfCarrier.Visible = false;
            // 
            // labFIRMCODE
            // 
            this.labFIRMCODE.Location = new System.Drawing.Point(413, 195);
            this.labFIRMCODE.Name = "labFIRMCODE";
            this.labFIRMCODE.Size = new System.Drawing.Size(61, 14);
            this.labFIRMCODE.TabIndex = 86;
            this.labFIRMCODE.Text = "FIRM CODE";
            this.labFIRMCODE.Visible = false;
            // 
            // labType
            // 
            this.labType.Location = new System.Drawing.Point(413, 146);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(24, 14);
            this.labType.TabIndex = 61;
            this.labType.Text = "类型";
            // 
            // labKeyWord
            // 
            this.labKeyWord.Location = new System.Drawing.Point(10, 170);
            this.labKeyWord.Name = "labKeyWord";
            this.labKeyWord.Size = new System.Drawing.Size(36, 14);
            this.labKeyWord.TabIndex = 2;
            this.labKeyWord.Text = "关键字";
            // 
            // labTradeTerm
            // 
            this.labTradeTerm.Location = new System.Drawing.Point(413, 170);
            this.labTradeTerm.Name = "labTradeTerm";
            this.labTradeTerm.Size = new System.Drawing.Size(48, 14);
            this.labTradeTerm.TabIndex = 61;
            this.labTradeTerm.Text = "贸易条款";
            // 
            // groupCNInfo
            // 
            this.groupCNInfo.BackColor = System.Drawing.Color.Transparent;
            this.groupCNInfo.Controls.Add(this.txtCShortName);
            this.groupCNInfo.Controls.Add(this.txtCBillName);
            this.groupCNInfo.Controls.Add(this.txtCName);
            this.groupCNInfo.Controls.Add(this.labCShortName);
            this.groupCNInfo.Controls.Add(this.labCBillName);
            this.groupCNInfo.Controls.Add(this.labCName);
            this.groupCNInfo.Controls.Add(this.labCAddress);
            this.groupCNInfo.Controls.Add(this.txtCAddress);
            this.groupCNInfo.Location = new System.Drawing.Point(3, 1);
            this.groupCNInfo.Name = "groupCNInfo";
            this.groupCNInfo.Size = new System.Drawing.Size(340, 138);
            this.groupCNInfo.TabIndex = 0;
            this.groupCNInfo.TabStop = false;
            this.groupCNInfo.Text = "中文信息";
            // 
            // labCShortName
            // 
            this.labCShortName.Location = new System.Drawing.Point(9, 17);
            this.labCShortName.Name = "labCShortName";
            this.labCShortName.Size = new System.Drawing.Size(24, 14);
            this.labCShortName.TabIndex = 0;
            this.labCShortName.Text = "简称";
            // 
            // labCBillName
            // 
            this.labCBillName.Location = new System.Drawing.Point(9, 65);
            this.labCBillName.Name = "labCBillName";
            this.labCBillName.Size = new System.Drawing.Size(48, 14);
            this.labCBillName.TabIndex = 38;
            this.labCBillName.Text = "帐单名称";
            // 
            // labCName
            // 
            this.labCName.Location = new System.Drawing.Point(9, 41);
            this.labCName.Name = "labCName";
            this.labCName.Size = new System.Drawing.Size(24, 14);
            this.labCName.TabIndex = 38;
            this.labCName.Text = "全称";
            // 
            // labCAddress
            // 
            this.labCAddress.Location = new System.Drawing.Point(9, 90);
            this.labCAddress.Name = "labCAddress";
            this.labCAddress.Size = new System.Drawing.Size(28, 14);
            this.labCAddress.TabIndex = 56;
            this.labCAddress.Text = "地址 ";
            // 
            // labCode
            // 
            this.labCode.Location = new System.Drawing.Point(10, 146);
            this.labCode.Name = "labCode";
            this.labCode.Size = new System.Drawing.Size(24, 14);
            this.labCode.TabIndex = 38;
            this.labCode.Text = "代码";
            // 
            // groupENInfo
            // 
            this.groupENInfo.Controls.Add(this.txtEShortName);
            this.groupENInfo.Controls.Add(this.txtEBillName);
            this.groupENInfo.Controls.Add(this.txtEName);
            this.groupENInfo.Controls.Add(this.labEShortName);
            this.groupENInfo.Controls.Add(this.labEBillName);
            this.groupENInfo.Controls.Add(this.labEName);
            this.groupENInfo.Controls.Add(this.labEAddress);
            this.groupENInfo.Controls.Add(this.txtEAddress);
            this.groupENInfo.Location = new System.Drawing.Point(400, 0);
            this.groupENInfo.Name = "groupENInfo";
            this.groupENInfo.Size = new System.Drawing.Size(340, 138);
            this.groupENInfo.TabIndex = 3;
            this.groupENInfo.TabStop = false;
            this.groupENInfo.Text = "英文信息";
            // 
            // labEShortName
            // 
            this.labEShortName.Location = new System.Drawing.Point(10, 17);
            this.labEShortName.Name = "labEShortName";
            this.labEShortName.Size = new System.Drawing.Size(24, 14);
            this.labEShortName.TabIndex = 38;
            this.labEShortName.Text = "简称";
            // 
            // labEBillName
            // 
            this.labEBillName.Location = new System.Drawing.Point(10, 65);
            this.labEBillName.Name = "labEBillName";
            this.labEBillName.Size = new System.Drawing.Size(48, 14);
            this.labEBillName.TabIndex = 38;
            this.labEBillName.Text = "帐单名称";
            // 
            // labEName
            // 
            this.labEName.Location = new System.Drawing.Point(10, 41);
            this.labEName.Name = "labEName";
            this.labEName.Size = new System.Drawing.Size(24, 14);
            this.labEName.TabIndex = 38;
            this.labEName.Text = "全称";
            // 
            // labEAddress
            // 
            this.labEAddress.Location = new System.Drawing.Point(10, 90);
            this.labEAddress.Name = "labEAddress";
            this.labEAddress.Size = new System.Drawing.Size(24, 14);
            this.labEAddress.TabIndex = 56;
            this.labEAddress.Text = "地址";
            // 
            // btnToEnInfo
            // 
            this.btnToEnInfo.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnToEnInfo.Appearance.Options.UseFont = true;
            this.btnToEnInfo.Image = ((System.Drawing.Image)(resources.GetObject("btnToEnInfo.Image")));
            this.btnToEnInfo.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnToEnInfo.Location = new System.Drawing.Point(352, 33);
            this.btnToEnInfo.Name = "btnToEnInfo";
            this.btnToEnInfo.Size = new System.Drawing.Size(38, 28);
            this.btnToEnInfo.TabIndex = 0;
            this.btnToEnInfo.Click += new System.EventHandler(this.btnToEnInfo_Click);
            // 
            // btnToCnInfo
            // 
            this.btnToCnInfo.Image = ((System.Drawing.Image)(resources.GetObject("btnToCnInfo.Image")));
            this.btnToCnInfo.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnToCnInfo.Location = new System.Drawing.Point(352, 71);
            this.btnToCnInfo.Name = "btnToCnInfo";
            this.btnToCnInfo.Size = new System.Drawing.Size(38, 28);
            this.btnToCnInfo.TabIndex = 1;
            this.btnToCnInfo.Click += new System.EventHandler(this.btnToCnInfo_Click);
            // 
            // navBarGroupControlContainer3
            // 
            this.navBarGroupControlContainer3.Controls.Add(this.cmbCountryProvince);
            this.navBarGroupControlContainer3.Controls.Add(this.labCity);
            this.navBarGroupControlContainer3.Controls.Add(this.labPostCode);
            this.navBarGroupControlContainer3.Controls.Add(this.cmbCity);
            this.navBarGroupControlContainer3.Controls.Add(this.labHomepage);
            this.navBarGroupControlContainer3.Controls.Add(this.labTel1);
            this.navBarGroupControlContainer3.Controls.Add(this.labEmail);
            this.navBarGroupControlContainer3.Controls.Add(this.txtTel1);
            this.navBarGroupControlContainer3.Controls.Add(this.labFax);
            this.navBarGroupControlContainer3.Controls.Add(this.labGeography);
            this.navBarGroupControlContainer3.Controls.Add(this.labEnterpriseCodeType);
            this.navBarGroupControlContainer3.Controls.Add(this.labEnterpriseCode);
            this.navBarGroupControlContainer3.Controls.Add(this.labTel2);
            this.navBarGroupControlContainer3.Controls.Add(this.txtTel2);
            this.navBarGroupControlContainer3.Controls.Add(this.txtPostCode);
            this.navBarGroupControlContainer3.Controls.Add(this.txtFax);
            this.navBarGroupControlContainer3.Controls.Add(this.txtEnterpriseCodeType);
            this.navBarGroupControlContainer3.Controls.Add(this.txtEnterpriseCode);
            this.navBarGroupControlContainer3.Controls.Add(this.txtHomepage);
            this.navBarGroupControlContainer3.Controls.Add(this.txtEmail);
            this.navBarGroupControlContainer3.Name = "navBarGroupControlContainer3";
            this.navBarGroupControlContainer3.Size = new System.Drawing.Size(752, 123);
            this.navBarGroupControlContainer3.TabIndex = 1;
            // 
            // cmbCountryProvince
            // 
            this.cmbCountryProvince.AllowMultSelect = false;
            this.cmbCountryProvince.DataSource = null;
            this.cmbCountryProvince.DisplayMember = "CName";
            this.cmbCountryProvince.Location = new System.Drawing.Point(86, 3);
            this.cmbCountryProvince.Name = "cmbCountryProvince";
            this.cmbCountryProvince.ParentMember = "ParentID";
            this.cmbCountryProvince.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbCountryProvince.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCountryProvince.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCountryProvince.Properties.PopupSizeable = false;
            this.cmbCountryProvince.Properties.ShowPopupCloseButton = false;
            this.cmbCountryProvince.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cmbCountryProvince.RootValue = 0;
            this.cmbCountryProvince.SelectedValue = null;
            this.cmbCountryProvince.Separator = ",";
            this.cmbCountryProvince.Size = new System.Drawing.Size(246, 21);
            this.cmbCountryProvince.TabIndex = 0;
            this.cmbCountryProvince.ValueMember = "ID";
            // 
            // labCity
            // 
            this.labCity.Location = new System.Drawing.Point(12, 57);
            this.labCity.Name = "labCity";
            this.labCity.Size = new System.Drawing.Size(24, 14);
            this.labCity.TabIndex = 1;
            this.labCity.Text = "城市";
            // 
            // labPostCode
            // 
            this.labPostCode.Location = new System.Drawing.Point(410, 103);
            this.labPostCode.Name = "labPostCode";
            this.labPostCode.Size = new System.Drawing.Size(24, 14);
            this.labPostCode.TabIndex = 38;
            this.labPostCode.Text = "邮编";
            // 
            // labHomepage
            // 
            this.labHomepage.Location = new System.Drawing.Point(410, 57);
            this.labHomepage.Name = "labHomepage";
            this.labHomepage.Size = new System.Drawing.Size(24, 14);
            this.labHomepage.TabIndex = 38;
            this.labHomepage.Text = "主页";
            // 
            // labTel1
            // 
            this.labTel1.Location = new System.Drawing.Point(12, 80);
            this.labTel1.Name = "labTel1";
            this.labTel1.Size = new System.Drawing.Size(31, 14);
            this.labTel1.TabIndex = 38;
            this.labTel1.Text = "电话1";
            // 
            // labEmail
            // 
            this.labEmail.Location = new System.Drawing.Point(410, 80);
            this.labEmail.Name = "labEmail";
            this.labEmail.Size = new System.Drawing.Size(24, 14);
            this.labEmail.TabIndex = 38;
            this.labEmail.Text = "邮箱";
            // 
            // labFax
            // 
            this.labFax.Location = new System.Drawing.Point(410, 6);
            this.labFax.Name = "labFax";
            this.labFax.Size = new System.Drawing.Size(24, 14);
            this.labFax.TabIndex = 38;
            this.labFax.Text = "传真";
            // 
            // labGeography
            // 
            this.labGeography.Location = new System.Drawing.Point(12, 6);
            this.labGeography.Name = "labGeography";
            this.labGeography.Size = new System.Drawing.Size(53, 14);
            this.labGeography.TabIndex = 73;
            this.labGeography.Text = "国家/省份";
            // 
            // labEnterpriseCodeType
            // 
            this.labEnterpriseCodeType.Location = new System.Drawing.Point(12, 32);
            this.labEnterpriseCodeType.Name = "labEnterpriseCodeType";
            this.labEnterpriseCodeType.Size = new System.Drawing.Size(72, 14);
            this.labEnterpriseCodeType.TabIndex = 73;
            this.labEnterpriseCodeType.Text = "企业代码类型";
            // 
            // labEnterpriseCode
            // 
            this.labEnterpriseCode.Location = new System.Drawing.Point(410, 32);
            this.labEnterpriseCode.Name = "labEnterpriseCode";
            this.labEnterpriseCode.Size = new System.Drawing.Size(48, 14);
            this.labEnterpriseCode.TabIndex = 73;
            this.labEnterpriseCode.Text = "企业代码";
            // 
            // labTel2
            // 
            this.labTel2.Location = new System.Drawing.Point(12, 103);
            this.labTel2.Name = "labTel2";
            this.labTel2.Size = new System.Drawing.Size(31, 14);
            this.labTel2.TabIndex = 38;
            this.labTel2.Text = "电话2";
            // 
            // navBarGroupControlContainer4
            // 
            this.navBarGroupControlContainer4.Controls.Add(this.cmbTaxidType);
            this.navBarGroupControlContainer4.Controls.Add(this.labPaymentType);
            this.navBarGroupControlContainer4.Controls.Add(this.labTaxIdType);
            this.navBarGroupControlContainer4.Controls.Add(this.txtTaxidNo);
            this.navBarGroupControlContainer4.Controls.Add(this.labTaxIdNo);
            this.navBarGroupControlContainer4.Controls.Add(this.labTerm);
            this.navBarGroupControlContainer4.Controls.Add(this.numCreditLimit);
            this.navBarGroupControlContainer4.Controls.Add(this.cmbPaymentType);
            this.navBarGroupControlContainer4.Controls.Add(this.labCreditLimit);
            this.navBarGroupControlContainer4.Controls.Add(this.numTerm);
            this.navBarGroupControlContainer4.Controls.Add(this.labelControl1);
            this.navBarGroupControlContainer4.Controls.Add(this.txtBankAccountNo);
            this.navBarGroupControlContainer4.Name = "navBarGroupControlContainer4";
            this.navBarGroupControlContainer4.Size = new System.Drawing.Size(752, 52);
            this.navBarGroupControlContainer4.TabIndex = 2;
            // 
            // labPaymentType
            // 
            this.labPaymentType.Location = new System.Drawing.Point(504, 8);
            this.labPaymentType.Name = "labPaymentType";
            this.labPaymentType.Size = new System.Drawing.Size(48, 14);
            this.labPaymentType.TabIndex = 61;
            this.labPaymentType.Text = "付款方式";
            // 
            // labTaxIdType
            // 
            this.labTaxIdType.Location = new System.Drawing.Point(3, 6);
            this.labTaxIdType.Name = "labTaxIdType";
            this.labTaxIdType.Size = new System.Drawing.Size(72, 14);
            this.labTaxIdType.TabIndex = 61;
            this.labTaxIdType.Text = "税务登记类型";
            // 
            // labTaxIdNo
            // 
            this.labTaxIdNo.Location = new System.Drawing.Point(3, 30);
            this.labTaxIdNo.Name = "labTaxIdNo";
            this.labTaxIdNo.Size = new System.Drawing.Size(60, 14);
            this.labTaxIdNo.TabIndex = 38;
            this.labTaxIdNo.Text = "税务登记号";
            // 
            // labTerm
            // 
            this.labTerm.Location = new System.Drawing.Point(261, 30);
            this.labTerm.Name = "labTerm";
            this.labTerm.Size = new System.Drawing.Size(48, 14);
            this.labTerm.TabIndex = 64;
            this.labTerm.Text = "信用期限";
            // 
            // labCreditLimit
            // 
            this.labCreditLimit.Location = new System.Drawing.Point(262, 7);
            this.labCreditLimit.Name = "labCreditLimit";
            this.labCreditLimit.Size = new System.Drawing.Size(48, 14);
            this.labCreditLimit.TabIndex = 64;
            this.labCreditLimit.Text = "信用限额";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl1.Appearance.Options.UseBackColor = true;
            this.labelControl1.Location = new System.Drawing.Point(504, 29);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 86;
            this.labelControl1.Text = "银行帐号";
            // 
            // navBarGroupControlContainer5
            // 
            this.navBarGroupControlContainer5.Controls.Add(this.txtRemark);
            this.navBarGroupControlContainer5.Name = "navBarGroupControlContainer5";
            this.navBarGroupControlContainer5.Size = new System.Drawing.Size(752, 78);
            this.navBarGroupControlContainer5.TabIndex = 4;
            // 
            // navContactInfo
            // 
            this.navContactInfo.Caption = "联系信息";
            this.navContactInfo.ControlContainer = this.navBarGroupControlContainer3;
            this.navContactInfo.Expanded = true;
            this.navContactInfo.GroupClientHeight = 125;
            this.navContactInfo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navContactInfo.Name = "navContactInfo";
            // 
            // navFinanceInfo
            // 
            this.navFinanceInfo.Caption = "财务信息";
            this.navFinanceInfo.ControlContainer = this.navBarGroupControlContainer4;
            this.navFinanceInfo.Expanded = true;
            this.navFinanceInfo.GroupClientHeight = 54;
            this.navFinanceInfo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navFinanceInfo.Name = "navFinanceInfo";
            // 
            // navOtherInfo
            // 
            this.navOtherInfo.Caption = "备注";
            this.navOtherInfo.ControlContainer = this.navBarGroupControlContainer5;
            this.navOtherInfo.Expanded = true;
            this.navOtherInfo.GroupClientHeight = 80;
            this.navOtherInfo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navOtherInfo.Name = "navOtherInfo";
            // 
            // barManagerCustomer
            // 
            this.barManagerCustomer.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManagerCustomer.DockControls.Add(this.barDockControlTop);
            this.barManagerCustomer.DockControls.Add(this.barDockControlBottom);
            this.barManagerCustomer.DockControls.Add(this.barDockControlLeft);
            this.barManagerCustomer.DockControls.Add(this.barDockControlRight);
            this.barManagerCustomer.Form = this;
            this.barManagerCustomer.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSave,
            this.barCheck,
            this.barClose});
            this.barManagerCustomer.MaxItemId = 8;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSave),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheck),
            new DevExpress.XtraBars.LinkPersistInfo(this.barClose)});
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // barSave
            // 
            this.barSave.Caption = "保存(&S)";
            this.barSave.Id = 3;
            this.barSave.Name = "barSave";
            this.barSave.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barCheck
            // 
            this.barCheck.Caption = "审核(&H)";
            this.barCheck.Id = 6;
            this.barCheck.Name = "barCheck";
            this.barCheck.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barCheck.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barCheck.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheck_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "关闭(&C)";
            this.barClose.Id = 7;
            this.barClose.Name = "barClose";
            this.barClose.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(756, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 582);
            this.barDockControlBottom.Size = new System.Drawing.Size(756, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 556);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(756, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 556);
            // 
            // CustomerManagerEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.Controls.Add(this.navBarControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "CustomerManagerEditPart";
            this.Size = new System.Drawing.Size(756, 582);
            ((System.ComponentModel.ISupportInitialize)(this.bsDataSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCShortName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKeyWord.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTel1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTel2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPostCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEnterpriseCodeType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEnterpriseCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHomepage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTaxidType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTradeTerm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaxidNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCreditLimit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaymentType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTerm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCBillName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEShortName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEBillName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBankAccountNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFIRMCODE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsAgentOfCarrier.Properties)).EndInit();
            this.groupCNInfo.ResumeLayout(false);
            this.groupCNInfo.PerformLayout();
            this.groupENInfo.ResumeLayout(false);
            this.groupENInfo.PerformLayout();
            this.navBarGroupControlContainer3.ResumeLayout(false);
            this.navBarGroupControlContainer3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCountryProvince.Properties)).EndInit();
            this.navBarGroupControlContainer4.ResumeLayout(false);
            this.navBarGroupControlContainer4.PerformLayout();
            this.navBarGroupControlContainer5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManagerCustomer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

    
    }
}
