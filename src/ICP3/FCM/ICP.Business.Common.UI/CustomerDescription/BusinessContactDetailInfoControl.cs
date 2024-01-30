//-----------------------------------------------------------------------
// <copyright file="CustomerDescriptionControl.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.Business.Common.UI
{
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using FCM.Common.ServiceInterface;
    using FCM.Common.ServiceInterface.DataObjects;
    using Framework.CommonLibrary.Client;
    using Framework.CommonLibrary.Common;
    using Framework.CommonLibrary.Helper;
    using ICP.Common.ServiceInterface;
    using ICP.Common.ServiceInterface.DataObjects;
    using ICP.Common.UI;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Forms;

    /// <summary>
    /// 客户描述信息弹出控件
    /// </summary>
    public partial class BusinessContactDetailInfoControl : XtraUserControl
    {
        #region Property
        /// <summary>
        /// 客户ID
        /// </summary>
        private Guid customerId;
        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid CustomerID
        {
            get
            {
                return customerId;
            }
            set
            {
                if (LocalData.IsDesignMode)
                {
                    return;
                }
                customerId = value;
                ucNewCustomerList.CustomerID = customerId;

            }
        }

        /// <summary>
        /// 业务操作上下文
        /// </summary>
        private BusinessOperationContext operationContext;
        /// <summary>
        /// 业务操作上下文
        /// </summary>
        public BusinessOperationContext OperationContext
        {
            get
            {
                return operationContext;
            }
            set
            {
                operationContext = value;
                ucNewCustomerList.OperationContext = operationContext;
            }
        }

        /// <summary>
        /// 联系人类型
        /// </summary>
        private ContactType contactType = ContactType.Customer;
        /// <summary>
        /// 联系人类型
        /// </summary>
        public ContactType ContactType
        {
            get
            {
                return contactType;
            }
            set
            {
                contactType = value;
                ucNewCustomerList.ContactType = contactType;
            }
        }

        /// <summary>
        /// 业务联系人所属沟通阶段
        /// </summary>
        private ContactStage contactStage = ContactStage.Unknown;
        /// <summary>
        /// 业务联系人所属沟通阶段
        /// </summary>
        public ContactStage ContactStage
        {
            get
            {
                return contactStage;
            }
            set
            {
                contactStage = value;
                if (ucNewCustomerList != null)
                {
                    ucNewCustomerList.ContactStage = contactStage;
                }
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        public CustomerDescription CustomerDescription
        {
            get
            {
                bindingSource.EndEdit();
                return bindingSource.DataSource as CustomerDescription;
            }
            set
            {
                bindingSource.DataSource = value;
                bindingSource.ResetBindings(false);
                CustomerDescription.IsDirty = false;
            }
        }

        /// <summary>
        /// 联系人列表
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<CustomerCarrierObjects> ContactList
        {
            get
            {
                return GetContactList();
            }
            set
            {
                if (!LocalData.IsDesignMode)
                {
                    FillContactInfo(value);
                }
            }
        }

        /// <summary>
        /// 业务联系人信息是否更改
        /// </summary>
        public bool IsContactDataChanged
        {
            get
            {
                return ucNewCustomerList.IsChanged;
            }
        }
        #endregion

        #region Service
        /// <summary>
        /// FCM公共服务
        /// </summary>
        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        /// <summary>
        /// ICP Common UI helper
        /// </summary>
        private ICPCommUIHelper _ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        /// <summary>
        /// 公共客户管理服务
        /// </summary>
        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        #endregion

        #region Custom Event
        /// <summary>
        /// 清除联系人信息
        /// </summary>
        public event EventHandler OnClear;
        /// <summary>
        /// 确认联系人信息
        /// </summary>
        public event EventHandler OnOk;
        /// <summary>
        /// 刷新联系人信息
        /// </summary>
        public event EventHandler OnRefresh; 
        #endregion

        #region Constructor
        /// <summary>
        /// 构造函数
        /// </summary>
        public BusinessContactDetailInfoControl()
        {
            InitializeComponent();
            ucNewCustomerList.IsShowCustomerNameColumn = false;
            ucNewCustomerList.ValidateReturnErrorString = true;

            Disposed += delegate
            {
                if (bindingSource != null)
                {
                    bindingSource.DataSource = null;
                    bindingSource = null;
                }
                OnOk = null;
                OnClear = null;
            };
            cmbCountry.ButtonClick += cmbCountry_ButtonClick;
        }

        #endregion

        #region Control Event
        /// <summary>
        /// 清空联系人信息
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (CustomerID == Guid.Empty)
            {
                return;
            }
            Clear();

            if (OnClear != null)
            {
                OnClear(this, new EventArgs());
            }
        }

        /// <summary>
        /// 确认联系人信息
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (CustomerID == Guid.Empty)
            {
                return;
            }

            if (!SaveChange())
            {
                return;
            }

            if (OnOk != null)
            {
                OnOk(this, new EventArgs());
            }
        }

        /// <summary>
        /// 刷新联系人信息，包括公司信息和业务联系人信息
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (CustomerID == Guid.Empty)
            {
                return;
            }
            CustomerDescription copy = new CustomerDescription();
            CustomerDescription temp = CustomerDescription ?? new CustomerDescription();
            ObjectHelper.CopyData<CustomerDescription>(temp, copy);
            _ICPCommUIHelper.SetCustomerDesByID(CustomerID, copy);
            copy.IsDirty = false;
            CustomerDescription = copy;
            if (OnRefresh != null)
            {
                OnRefresh(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// 国家选定
        /// </summary>
        void cmbCountry_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                cmbCountry.Text = string.Empty;
                txtCityZip.Text = string.Empty;
                txtCityZip.EditValue = null;
                bindingSource.EndEdit();
                bindingSource.ResetBindings(false);
            }
        } 
        #endregion

        #region Custom Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="contactStage"></param>
        /// <param name="contactType"></param>
        public void SetConfigInfo(Guid customerID, ContactStage contactStage, ContactType contactType)
        {
            CustomerID = customerId;
            ContactStage = contactStage;
            ContactType = contactType;
        }

        /// <summary>
        /// 根据选择的客户更新业务联系人信息
        /// </summary>
        private void FillContactInfo(List<CustomerCarrierObjects> contactList)
        {
            EnsureCustomerDescriptionExists();

            for (int i = 0; i < contactList.Count; i++)
            {
                if (contactList[i].CustomerID != CustomerID)
                {
                    contactList[i].Id = Guid.NewGuid();
                }
            }
            ucNewCustomerList.DataSource = contactList;
        }
        /// <summary>
        /// 确保客户描述存在
        /// </summary>
        private void EnsureCustomerDescriptionExists()
        {
            if (CustomerDescription == null)
            {
                if (CustomerID == Guid.Empty)
                {
                    CustomerDescription = new CustomerDescription();
                }
                else
                {
                    CustomerDescription temp = new CustomerDescription();
                    _ICPCommUIHelper.SetCustomerDesByID(CustomerID, temp);
                    CustomerDescription = temp;
                }
            }
        }

        /// <summary>
        /// 设置数据源
        /// </summary>
        /// <param name="customerDescription"></param>
        public void SetDataBinding(CustomerDescription customerDescription)
        {
            if (customerDescription != null)
            {
                customerDescription.IsDirty = false;
            }
            bindingSource.DataSource = customerDescription;
            bindingSource.ResetBindings(false);


        }

        /// <summary>
        /// 国家数据列表
        /// </summary>
        public void SetCountryList(List<CountryList> countryList)
        {
            cmbCountry.Properties.BeginUpdate();
            foreach (CountryList countryInfo in countryList)
            {
                cmbCountry.Properties.Items.Add(new ImageComboBoxItem(countryInfo.EName));
            }
            cmbCountry.Properties.EndUpdate();
        }
        /// <summary>
        /// 语言设置
        /// </summary>
        /// <param name="isEnglish"></param>
        public void SetLanguage(bool isEnglish)
        {
            if (isEnglish == false)
            {
                labAddress.Text = "地址";
                labCityZip.Text = "城市";
                labCountry.Text = "国家";

                labName.Text = "名称";
                labRemark.Text = "备注";

                btnClear.Text = "清除(&L)";
                btnOK.Text = "确定(&O)";
                btnRefresh.Text = "刷新(&R)";

                foreach (EditorButton item in cmbCountry.Properties.Buttons)
                {
                    if (item.Kind == ButtonPredefines.Delete)
                    {
                        item.ToolTip = "点击按钮以清空国家和城市.";
                    }
                }

            }
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        public void Clear()
        {
            txtAddress.EditValue = string.Empty;
            txtCityZip.EditValue = string.Empty;
            txtCompanyName.EditValue = string.Empty;
            txtRemark.EditValue = string.Empty;
            cmbCountry.EditValue = null;
            ucNewCustomerList.DataSource = null;
            bindingSource.EndEdit();
        }

        /// <summary>
        /// 保存更改的联系人信息
        /// </summary>
        /// <returns></returns>
        private bool SaveChange()
        {
            if (!ValidateData())
            {
                return false;
            }
            //客户信息是否有更改,有更改保存
            if (CustomerDescription.IsDirty)
            {
                try
                {
                    DialogResult result = XtraMessageBox.Show(
                    LocalData.IsEnglish ? "Click Ok, changed information will be synchronized to customer data." : "点击Ok，更改的信息将会同步到客户资料中。"
                           , LocalData.IsEnglish ? "Tip" : "提示"
                           , MessageBoxButtons.OKCancel
                           , MessageBoxIcon.Question);
                    if (result == DialogResult.OK)
                    {
                        CustomerInfo customerInfo = CustomerService.GetCustomerInfo(CustomerID);
                        customerInfo.EName = CustomerDescription.Name;
                        customerInfo.EAddress = CustomerDescription.Address;
                        customerInfo.Fax = CustomerDescription.Fax;
                        customerInfo.Tel1 = CustomerDescription.Tel;
                        CustomerService.SaveCustomerInfo(customerInfo.ID, customerInfo.Code, customerInfo.KeyWord, customerInfo.CShortName, customerInfo.EShortName, customerInfo.CName, customerInfo.EName, customerInfo.CBillName, customerInfo.EBillName, customerInfo.CAddress, customerInfo.EAddress, customerInfo.CountryID, customerInfo.ProvinceID, customerInfo.CityID, customerInfo.EnterpriseCodeType, customerInfo.EnterpriseCode, customerInfo.PostCode, customerInfo.Tel1, customerInfo.Tel2, customerInfo.Fax, customerInfo.EMail, customerInfo.Homepage, customerInfo.TaxIdType, customerInfo.TaxIdNo, customerInfo.BankAccountNo, customerInfo.CreditLimit, customerInfo.Term, customerInfo.TradeTermID, customerInfo.PaymentTypeID, customerInfo.Type, customerInfo.IsAgentOfCarrier, customerInfo.FIRMCODE, customerInfo.Remark, LocalData.UserInfo.LoginID, customerInfo.IsCompany, customerInfo.UpdateDate);

                    }
                    isDirty = false;
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ClientHelper.GetErrorMessage(ex), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            //联系信息有修改
            if (IsContactDataChanged)
            {
                if (ucNewCustomerList.DataSourceList.Count > 0)
                {
                    List<Guid?> IDs = new List<Guid?>();
                    List<string> cNames = new List<string>();
                    List<string> eNames = new List<string>();
                    List<string> depart = new List<string>();
                    List<string> position = new List<string>();
                    List<string> tels = new List<string>();
                    List<string> faxs = new List<string>();
                    List<string> mobiles = new List<string>();
                    List<string> emails = new List<string>();
                    List<string> remarks = new List<string>();
                    List<CustomerContactType> types = new List<CustomerContactType>();
                    List<DateTime?> updatedate = new List<DateTime?>();

                    foreach (CustomerCarrierObjects cusCar in ucNewCustomerList.DataSourceList)
                    {
                        if (cusCar.IsDirty)
                        {
                            CustomerInfo ccustomerInfo = CustomerService.GetCustomerInfo(CustomerID);
                            IDs.Add(cusCar.Id);
                            cNames.Add(ccustomerInfo.CName);
                            eNames.Add(ccustomerInfo.EName);
                            depart.Add("");
                            position.Add("");
                            tels.Add(string.IsNullOrEmpty(cusCar.Tel) ? ccustomerInfo.Tel1 : cusCar.Tel);
                            faxs.Add(string.IsNullOrEmpty(cusCar.Fax) ? ccustomerInfo.Fax : cusCar.Fax);
                            mobiles.Add(cusCar.Mobile);
                            emails.Add(string.IsNullOrEmpty(cusCar.Mail) ? ccustomerInfo.EMail : cusCar.Mail);
                            remarks.Add(ccustomerInfo.Remark);
                            types.Add(CustomerContactType.Normal);
                            updatedate.Add(DateTime.Now);
                        }
                    }
                    if (IDs.Count > 0)
                    {
                        CustomerService.SaveCustomerContactInfo(
                            CustomerID,
                            IDs.ToArray(),
                            cNames.ToArray(),
                            eNames.ToArray(),
                            depart.ToArray(),
                            position.ToArray(),
                            tels.ToArray(),
                            faxs.ToArray(),
                            mobiles.ToArray(),
                            emails.ToArray(),
                            remarks.ToArray(),
                            types.ToArray(),
                            LocalData.UserInfo.LoginID,
                            updatedate.ToArray());
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 获取联系人列表
        /// </summary>
        /// <returns></returns>
        public List<CustomerCarrierObjects> GetContactList()
        {
            if (ucNewCustomerList.DataSourceList == null)
            {
                return new List<CustomerCarrierObjects>();
            }
            return ucNewCustomerList.DataSourceList;
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            bindingSource.EndEdit();
            dxErrorProvider.ClearErrors();
            bool isSrcc = true;
            if (string.IsNullOrEmpty(CustomerDescription.Name))
            {

                dxErrorProvider.SetError(txtCompanyName, LocalData.IsEnglish ? "Customer Name Must Input." : "必须输入客户名称.");
                isSrcc = false;

            }
            if (string.IsNullOrEmpty(CustomerDescription.Address))
            {

                dxErrorProvider.SetError(txtAddress, LocalData.IsEnglish ? "Customer Address Must Input." : "必须输入客户地址.");
                isSrcc = false;

            }
            string errorString = ucNewCustomerList.ValidateData2();
            if (!string.IsNullOrEmpty(errorString))
            {

                isSrcc = false;
                XtraMessageBox.Show(errorString, "Tip", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return isSrcc;
        } 
        #endregion

        #region Comment Code

        private bool isDirty = false;
        private CustomerCarrierObjects CreateNewStaff()
        {
            CustomerCarrierObjects staffObject = new CustomerCarrierObjects() { CustomerID = CustomerID, CreateByID = LocalData.UserInfo.LoginID, CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified), Type = ContactType, IsDirty = true };
            if (ContactStage != ContactStage.Unknown)
            {
                FCM.Common.ServiceInterface.FCMInterfaceUtility.SetStage(staffObject, ContactStage);
            }

            staffObject.OceanBookingID = OperationContext.OperationID;


            return staffObject;
        }

        void txtCompanyName_TextChanged(object sender, EventArgs e)
        {
            isDirty = true;
        } 
        #endregion
    }

}
