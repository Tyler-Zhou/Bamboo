using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using ICP.Common.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects.SaveRequests;

namespace ICP.FAM.UI.MonthlyClosingEntry
{
    public partial class SetCustomerPreferences : BaseEditPart
    {
        /// <summary>
        /// 
        /// </summary>
        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        private Guid? _companyid = null;
        private Guid? _customerid = null;
        private string _companyname = string.Empty;
        private string _customername = string.Empty;

        private List<PaymentMail> _mailList = new List<PaymentMail>();
        private CustomerPreferencesInfo _current = null;


        public SetCustomerPreferences()
        {
            InitializeComponent();
        }

        private void SetCustomerPreferences_Load(object sender, EventArgs e)
        {
            List<CustomerPreferencesInfo> oriList = FinanceService.GetCustomerPreferencesInfo(null, _customerid, _companyid);
            if (oriList != null && oriList.Count > 0)
            {
                _current = oriList[0];
                bindingSource1.DataSource = _current;
                bindingSource1.ResetBindings(false);
                SetGrid();
            }
            else
            {
                _current = new CustomerPreferencesInfo();
                if (_customerid != null)
                    _current.CustomerID = (Guid)_customerid;
                if (_companyid != null)
                    _current.CompanyID = (Guid)_companyid;
                _current.CompanyName = _companyname;
                _current.CustomerName = _customername;

                bindingSource1.DataSource = _current;
                bindingSource1.ResetBindings(false);
            }

            SetCheckBox();
            SetComboBox();
            chkIsHblCopy.CheckedChanged += new EventHandler(chkIsHblCopy_CheckedChanged);
            chkIsTruck.CheckedChanged += new EventHandler(chkIsTruck_CheckedChanged);
        }

        public void SetCompanyAndCustomer(Guid? companyid, string companyname, Guid? customerid, string customername)
        {
            _companyid = companyid;
            _customerid = customerid;
            _companyname = companyname;
            _customername = customername;
        }

        private void SetComboBox()
        {
            cmbCargoType.Properties.Items.Clear();
            cmbCargoType.Properties.Items.BeginUpdate();
            cmbCargoType.Properties.Items.Add(new ImageComboBoxItem("Local Statement and Others", 0, 0));
            cmbCargoType.Properties.Items.Add(new ImageComboBoxItem("Separated by Month", 1, 1));
            cmbCargoType.Properties.Items.Add(new ImageComboBoxItem("Separated by Shipment", 2, 2));
            cmbCargoType.Properties.Items.EndUpdate();

            cmbCargoType.SelectedIndex = _current.PdfAssembled;
        }

        private void SetCheckBox()
        {
            if (_current != null)
            {
                switch (_current.OtherAttachments)
                {
                    case 0:
                        chkIsHblCopy.Checked = false;
                        chkIsTruck.Checked = false;
                        break;
                    case 1:
                        chkIsHblCopy.Checked = true;
                        chkIsTruck.Checked = false;
                        break;
                    case 2:
                        chkIsHblCopy.Checked = false;
                        chkIsTruck.Checked = true;
                        break;
                    case 3:
                        chkIsHblCopy.Checked = true;
                        chkIsTruck.Checked = true;
                        break;
                }
            }
        }

        private void SetGrid()
        {
            if (_current != null && !string.IsNullOrEmpty(_current.NotifyMail))
            {
                XmlDocument xmlmail = new XmlDocument();
                xmlmail.LoadXml(_current.NotifyMail);
                XmlNode root = xmlmail.FirstChild;

                List<PaymentMail> mailist = new List<PaymentMail>();
                foreach (XmlNode mail in root.ChildNodes)
                {
                    PaymentMail pm = new PaymentMail();
                    pm.Type = mail.Name;
                    pm.Mail = mail.InnerText;
                    pm.Name = mail.Attributes["Alias"].Value;
                    mailist.Add(pm);
                }

                gridPre.DataSource = mailist;
                ViewPre.RefreshData();
            }
        }

        private string CreateDocument()
        {
            _mailList = gridPre.DataSource as List<PaymentMail>;
            if (_mailList != null && _mailList.Count > 0)
            {
                XmlDocument mailxml = new XmlDocument();
                XmlNode root = mailxml.CreateElement("MailList");
                mailxml.AppendChild(root);

                foreach (PaymentMail mail in _mailList)
                {
                    XmlElement mailElement = mailxml.CreateElement(mail.Type);
                    mailElement.SetAttribute("Alias", mail.Name);
                    mailElement.InnerText = mail.Mail;
                    root.AppendChild(mailElement);
                }

                return mailxml.InnerXml;
            }

            return null;
        }

        private void btAddTo_Click(object sender, EventArgs e)
        {
            _mailList = gridPre.DataSource as List<PaymentMail>;
            if (_mailList == null)
            {
                _mailList = new List<PaymentMail>();
            }
            PaymentMail to = new PaymentMail();
            to.Type = "To";
            _mailList.Add(to);
            gridPre.DataSource = _mailList;
            ViewPre.RefreshData();
        }

        private void btAddCC_Click(object sender, EventArgs e)
        {
            _mailList = gridPre.DataSource as List<PaymentMail>;
            if (_mailList == null)
            {
                _mailList = new List<PaymentMail>();
            }
            PaymentMail to = new PaymentMail();
            to.Type = "CC";
            _mailList.Add(to);
            gridPre.DataSource = _mailList;
            ViewPre.RefreshData();
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            if (ViewPre.FocusedRowHandle < 0)
            {
                return;
            }

            ViewPre.DeleteRow(ViewPre.FocusedRowHandle);
            _mailList = gridPre.DataSource as List<PaymentMail>;
            //_mailList.Remove(_mailList[ViewPre.FocusedRowHandle]);

            //gridPre.DataSource = _mailList;
            //ViewPre.RefreshData();
        }

        private void chkIsHblCopy_CheckedChanged(object sender, EventArgs e)
        {
            if (_current != null)
            {
                _current.OtherAttachments = Convert.ToByte((chkIsHblCopy.Checked ? 1 : 0) + (chkIsTruck.Checked ? 2 : 0));
            }
        }

        private void chkIsTruck_CheckedChanged(object sender, EventArgs e)
        {
            _current.OtherAttachments = Convert.ToByte((chkIsHblCopy.Checked ? 1 : 0) + (chkIsTruck.Checked ? 2 : 0));
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            bindingSource1.EndEdit();
            labCompany.Focus();

            if (!CheckSetting())
            {
                return;
            }

            try
            {
                _current.NotifyMail = CreateDocument();
                if (cmbCargoType.SelectedIndex > -1)
                {
                    _current.PdfAssembled = Convert.ToByte(cmbCargoType.SelectedIndex);
                }

                CustomerPreferencesSaveRequest saveRequest = createRequest();
                FinanceService.SaveCustomerPreferencesInfo(saveRequest);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }

        }

        private CustomerPreferencesSaveRequest createRequest()
        {
            CustomerPreferencesSaveRequest myRequest = new CustomerPreferencesSaveRequest();

            myRequest.CompanyId = _current.CompanyID;
            myRequest.CustomerId = _current.CustomerID;
            myRequest.Id = _current.ID;
            myRequest.InvoiceTitle = _current.InvoiceTitle;
            myRequest.IsNeedPO = _current.IsNeedPO;
            myRequest.NotifyContact = _current.NotifyContact;
            myRequest.ShipTo = _current.ShipTo;
            myRequest.NotifyMail = _current.NotifyMail;
            myRequest.NotifyPaymentDay = _current.NotifyPaymentDay;
            myRequest.OtherAttachments = _current.OtherAttachments;
            myRequest.PdfAssembled = _current.PdfAssembled;
            myRequest.SaveByID = LocalData.UserInfo.LoginID;
            myRequest.UpdateDate = _current.UpdateDate;
            myRequest.Tue = _current.Tue;

            return myRequest;
        }

        private bool CheckSetting()
        {
            if (cmbCargoType.SelectedIndex < 0)
            {
                XtraMessageBox.Show(LocalData.IsEnglish ? "Please selete PDF Assembled！" : "请选择PDF组成！",
                                                             LocalData.IsEnglish ? "Tips" : "提示",
                                                             MessageBoxButtons.OK,
                                                             MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(txtContact.Text))
            {
                //DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Please Set Contact！" : "请输入账单电话！",
                //                                             LocalData.IsEnglish ? "Tips" : "提示",
                //                                             MessageBoxButtons.OK,
                //                                             MessageBoxIcon.Warning);
                //return false;
                _current.NotifyContact = "";
            }

            _mailList = gridPre.DataSource as List<PaymentMail>;
            if (_mailList == null || _mailList.Count == 0)
            {
                XtraMessageBox.Show(LocalData.IsEnglish ? "Please enter your contacts and Email！" : "请输入联系人和邮箱！",
                                                                               LocalData.IsEnglish ? "Tips" : "提示",
                                                                               MessageBoxButtons.OK,
                                                                               MessageBoxIcon.Warning);
                return false;
            }

            foreach (PaymentMail mail in _mailList)
            {
                if (string.IsNullOrEmpty(mail.Mail) || string.IsNullOrEmpty(mail.Name))
                {
                    XtraMessageBox.Show(LocalData.IsEnglish ? "Please enter your contacts and Email！" : "请输入联系人和邮箱！",
                                                                               LocalData.IsEnglish ? "Tips" : "提示",
                                                                               MessageBoxButtons.OK,
                                                                               MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            _current = new CustomerPreferencesInfo();
            _current.CustomerID = (Guid)_customerid;
            _current.CustomerName = _customername;
            _current.CompanyID = (Guid)_companyid;
            _current.CompanyName = _companyname;

            bindingSource1.DataSource = _current;
            bindingSource1.ResetBindings(false);
            cmbCargoType.SelectedIndex = -1;
            chkIsTruck.Checked = false;
            chkIsHblCopy.Checked = false;

            gridPre.DataSource = null;
            ViewPre.RefreshData();
        }
    }
}
