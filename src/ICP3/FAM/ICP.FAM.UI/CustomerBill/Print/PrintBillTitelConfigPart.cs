using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using ICP.Common.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface;
using System.Drawing;

namespace ICP.FAM.UI.CustomerBill.Print
{
    [ToolboxItem(false)]
    public partial class PrintBillTitelConfigPart : BaseEditPart
    {
        #region 服务

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        public IOrganizationService OrganizationService
        {
            get
            {
                return ServiceClient.GetService<IOrganizationService>();
            }
        }
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        #endregion

        public PrintBillTitelConfigPart()
        {
            InitializeComponent();
            Disposed += delegate {
                _CurrentData = null;
                _Customers = null;
                
                bindingSource1.DataSource = null;
                bindingSource1.Dispose();
                cmbTitelCompany.EditValueChanged -= OnCmbTitleCompanyEditValueChanged;
                if (WorkItem != null)
                {
                    WorkItem.Items.Remove(this);
                    WorkItem = null;
                }
            
            };
        }
        private void OnCmbTitleCompanyEditValueChanged(object sender, EventArgs e)
        {
            CompanyChanged();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            List<OrganizationList> userCompanyList = UserService.GetUserCompanyList(LocalData.UserInfo.LoginID, OrganizationType.Company);
            foreach (var item in userCompanyList)
            {
                cmbTitelCompany.Properties.Items.Add(new ImageComboBoxItem
                    (LocalData.IsEnglish ? item.EShortName : item.CShortName, item.ID));
            }

            cmbTitelCompany.EditValueChanged += OnCmbTitleCompanyEditValueChanged;
         
            cmbTitelCompany.EditValue = _CurrentCompanyID;

            #region 口岸公司
            if (_CurrentCompanyID == _ThailandCompanyId && _billWay == BillReportNameEnum.LocalInvoiceDR)
            {
                labCompany.Visible = true;
                cmbCompany.Visible = true;
                List<OrganizationList> list = OrganizationService.GetOfficeList();
                foreach (OrganizationList item in list)
                {
                    cmbCompany.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? item.EShortName : item.CShortName, item.ID));
                }

                cmbCompany.EditValue = _CurrentCompanyID;
            }
            #endregion
        }


        /// <summary>
        /// 缓存的客户信息,Key(CompanyID
        /// </summary>
        Dictionary<Guid, CustomerInfo> _Customers = new Dictionary<Guid, CustomerInfo>();
        private void CompanyChanged()
        {
            Guid companyID = new Guid(cmbTitelCompany.EditValue.ToString());
            if (companyID.IsNullOrEmpty()) return;
            if (_Customers.ContainsKey(companyID) == false)
            {
                ConfigureInfo configure = ConfigureService.GetCompanyConfigureInfo(companyID);
                if (configure == null)
                {
                    _CurrentData.CompanyDsc = txtTitelCompanyDes.Text = string.Empty;
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = LocalData.IsEnglish ? "The company can not print configuration information is empty!" : "该公司配置信息为空不能打印！";
                    btnOK.Enabled = false;
                    return;
                }
                CustomerInfo customer = CustomerService.GetCustomerInfo(configure.CustomerID);
                _Customers.Add(companyID, customer);
            }
            StringBuilder strDes = new StringBuilder();
            strDes.Append(_Customers[companyID].EAddress + "\r\n");
            strDes.Append("Tel:" + _Customers[companyID].Tel1 + " ");
            strDes.Append("Fax:" + _Customers[companyID].Fax);
            _CurrentData.CompanyDsc = txtTitelCompanyDes.Text = strDes.ToString();
            lblMessage.Text = "";
            btnOK.Enabled = true;

        }


        #region IEditPart成员

        PrintBillTitelConfigData _CurrentData = null;
        Guid _ThailandCompanyId = new Guid("13C26E30-F2AD-4D94-B13D-5E337EA97936");//THAILAND 公司

        public override object DataSource
        {
            get
            {
                return bindingSource1.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }

        private void BindingData(object value)
        {
            _CurrentData = value as PrintBillTitelConfigData;
            bindingSource1.DataSource = _CurrentData;
        }

        #endregion

        #region IPart 成员
        Guid _CurrentCompanyID = Guid.Empty;
        BillReportNameEnum _billWay = BillReportNameEnum.LocalInvoiceDR;
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key == "CompanyID")
                {
                    if (item.Value != null) _CurrentCompanyID = new Guid(item.Value.ToString());
                }

                if (item.Key == "BillWay")
                {
                    _billWay = (BillReportNameEnum)item.Value;
                }
            }
        }
        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            Guid companyID = new Guid(cmbTitelCompany.EditValue.ToString());
            if (companyID.IsNullOrEmpty() == false)
            {
                _CurrentData.CompanyID = companyID;
            }

            _CurrentData.CompanyName = cmbTitelCompany.Text;
            _CurrentData.CompanyDsc = txtTitelCompanyDes.Text.Trim();
            if (cmbCompany.Visible == true)
            {
                _CurrentData.CompanyBank = new Guid(cmbCompany.EditValue.ToString());
            }

            FindForm().DialogResult = DialogResult.OK;
            FindForm().Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }
    }
}
