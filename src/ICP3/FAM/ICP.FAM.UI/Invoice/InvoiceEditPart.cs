using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Common.ServiceInterface.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using DevExpress.XtraEditors.Controls;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FAM.ServiceInterface;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface.DataObjects.Report;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.ClientComponents.Controls;
using System.Runtime.InteropServices;
using ICP.Common.UI;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.FAM.UI.Invoice;
using System.Xml.Linq;
using System.IO;
using System.Threading;
using Microsoft.Win32;
using ICP.FAM.UI.Comm;
using System.Text.RegularExpressions;
using System.ServiceProcess;
using System.Diagnostics;
using System.Xml;
using System.Collections;
using ICP.Framework.CommonLibrary.Common;
using System.Drawing;
using Newtonsoft.Json.Linq;
using Baidu.Aip.Ocr;
using System.Drawing.Imaging;
using Newtonsoft.Json;



namespace ICP.FAM.UI
{
    [ToolboxItem(false)]
    public partial class InvoiceEditPart : BaseEditPart
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        public ICommonService CommonService
        {
            get
            {
                return ServiceClient.GetService<ICommonService>();
            }
        }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }


        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }
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

        public IReportViewService ReportViewService
        {
            get
            {
                return ServiceClient.GetClientService<IReportViewService>();
            }
        }


        public RateHelper RateHelper
        {
            get
            {
                return ClientHelper.Get<RateHelper, RateHelper>();
            }
        }

        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        public InvoiceReportHelper InvoiceReportHelper
        {
            get
            {
                return ClientHelper.Get<InvoiceReportHelper, InvoiceReportHelper>();
            }
        }
        #endregion

        #region init
        private Guid solutionID;
        public Guid SolutionID
        {
            set
            {
                solutionID = value;
            }
            get
            {
                if (FAMUtility.GuidIsNullOrEmpty(solutionID))
                {
                    ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(_CurrentData.CompanyID);
                    if (configureInfo != null)
                    {
                        solutionID = configureInfo.SolutionID;
                    }
                }
                return solutionID;
            }
        }

        public InvoiceEditPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                List<IDisposable> list = new List<IDisposable> { customerFinder, selectBillFinder, invoiceTitleFinder, polFinder, podFinder, placeOfDeliveryFinder, vesselFinder, voyageFinder, chargingCodeFinder };
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] != null)
                    {
                        list[i].Dispose();
                    }
                }
                list.Clear();
                gcBill.DataSource = null;
                gcChargeList.DataSource = null;
                SmartPartClosing -= InvoiceEdit_SmartPartClosing;
                //收款
                cmbReceivables.OnFirstEnter -= OnCmbReceivablesEnter;

                //复核
                cmbReview.OnFirstEnter -= OnCmbReviewEnter;
                bsBill.PositionChanged -= bsBill_PositionChanged;
                bsBill.DataSource = null;
                bsBill.Dispose();
                bsInvoiceFeeDate.PositionChanged -= bsInvoiceFeeDate_PositionChanged;
                bsInvoiceFeeDate.DataSource = null;
                bsInvoiceFeeDate.Dispose();
                bsChargeList.DataSource = null;
                bsChargeList.Dispose();
                Saved = null;
                _CurrentData = null;
                _dicCurrency = null;
                _dicSolutionGL = null;
                _List = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitMessage();
                InitControls();
                SmartPartClosing += InvoiceEdit_SmartPartClosing;
                ActivateSmartPartClosingEvent(Workitem);


                _CurrentData.CancelEdit();
                _CurrentData.BeginEdit();

                LocalCommonServices.ErrorTrace.Clear();
            }

        }

        private void InvoiceEditPart_Load(object sender, EventArgs e)
        {
        }
        private void InitControls()
        {
            InitUserList();
            FAMUtility.ShowGridRowNo(gvChargeList);
            FAMUtility.ShowGridRowNo(gvBill);
            FAMUtility.ShowGridRowNo(gvFee);
            //if (string.IsNullOrEmpty(txtCustomer.Text))
            //{
            //    txtInvoiceNo.Properties.ReadOnly = false;
            //}
            //if (_CurrentData.IsNew) txtInvoiceNo.Properties.ReadOnly = false;
            //else
            //{
            //    txtInvoiceNo.Properties.ReadOnly = true;
            //}
            txtSelectBill.Properties.NullValuePrompt = LocalData.IsEnglish ? @"Please Input BillNo(OperationNo) or /BLNo To Search." : @"请输入帐单号(业务号)或 /+提单号 进行搜索.";
            txtSelectBill.Properties.NullValuePromptShowForEmptyValue = true;

            InitComboboxSource();
            BindCombo();
            SearchRegister();
            TotalCurrenAmountBill();
            TotalCurrenAmount();
            colCurrencyName.Caption = LocalData.IsEnglish ? "CurrencyName" : "币种";
            GetBarEnable();
        }
        private void GetBarEnable()
        {
            if (!_CurrentData.IsValid && !_CurrentData.IsNew)
            {
                btnDeleteBill.Enabled = false;
                barRemoveFee.Enabled = false;
                barClearFee.Enabled = false;
                txtSelectBill.Enabled = false;
                barSave.Enabled = false;
                barPrint.Enabled = false;
            }
        }
        private void InitMessage()
        {
            RegisterMessage("1110170001", LocalData.IsEnglish ? "Are you sure to deleted the selected cost " : "确认删除选中的费用?");
            RegisterMessage("1110170002", LocalData.IsEnglish ? "Are you sure to clear all cost" : "确认清空所有费用?");
            RegisterMessage("1110170003", LocalData.IsEnglish ? "Are you sure to delete this bill? The associated {0} ticket fee will also delete?" : "确认删除此账单吗?关联的 {0} 票费用明细也将一起删除?");
            RegisterMessage("1110170004", LocalData.IsEnglish ? "Invoices exceeding100000RMB, do you want to continue printing" : "发票金额超过了100000RMB,是否继续打印?");
        }
        private void OnCmbReceivablesEnter(object sender, EventArgs e)
        {
            List<UserList> users = UserService.GetUserListByList(string.Empty, string.Empty, null, null, null, null, null, true, 0);

            Dictionary<string, string> col = new Dictionary<string, string>();
            col.Add("CName", "名称");
            col.Add("Code", "代码");
            cmbReceivables.InitSource<UserList>(users, col, "CName", "CName");
        }
        private void OnCmbReviewEnter(object sender, EventArgs e)
        {
            List<UserList> users = UserService.GetUserListByList(string.Empty, string.Empty, null, null, null, null, null, true, 0);

            Dictionary<string, string> col = new Dictionary<string, string>();
            col.Add("CName", "名称");
            col.Add("Code", "代码");
            cmbReview.InitSource<UserList>(users, col, "CName", "CName");
        }
        private void InitUserList()
        {
            //收款
            cmbReceivables.OnFirstEnter += OnCmbReceivablesEnter;

            //复核
            cmbReview.OnFirstEnter += OnCmbReviewEnter;

        }

        public new bool ReadOnly
        {
            get
            {
                return base.ReadOnly;
            }
            set
            {
                base.ReadOnly = value;
                if (value)
                {
                    barSave.Enabled = false;
                    barEditInvoiceNo.Enabled = false;
                    btnDeleteBill.Enabled = false;
                    barRemoveFee.Enabled = false;
                    barClearFee.Enabled = false;
                    txtSelectBill.Enabled = false;
                }
            }
        }

        /// <summary>
        /// 大连公司ID
        /// </summary>
        Guid _daLianCompanyID = new Guid("b1afad8f-55dd-4e29-a250-eb82ab3971fe");

        /// <summary>
        /// 宁波公司ID
        /// </summary>
        Guid _ningBoCompanyID = new Guid("a62a9f8e-e69c-4e6e-ad85-e75aed3c6cf9");

        Dictionary<Guid, string> _dicCurrency = new Dictionary<Guid, string>();
        Dictionary<Guid, string> _dicSolutionGL = new Dictionary<Guid, string>();
        Dictionary<Guid, string> _List = new Dictionary<Guid, string>();
        private void BindCombo()
        {
            _dicCurrency = new Dictionary<Guid, string>();
            _dicSolutionGL = new Dictionary<Guid, string>();
            try
            {
                ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(_CurrentData.CompanyID);
                curCureency_.Items.Clear();

                if (configureInfo != null)
                {
                    List<SolutionCurrencyList> currencyList = ConfigureService.GetSolutionCurrencyList(configureInfo.SolutionID, true);
                    if (currencyList != null)
                    {
                        foreach (var item in currencyList)
                        {
                            curCureency_.Items.Add(new ImageComboBoxItem(item.CurrencyName, item.CurrencyID));

                            _dicCurrency.Add(item.CurrencyID, item.CurrencyName);
                        }

                    }

                }

            }
            catch
            {
                ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);

                curCureency_.Items.Clear();

                if (configureInfo != null)
                {
                    List<SolutionCurrencyList> currencyList = ConfigureService.GetSolutionCurrencyList(configureInfo.SolutionID, true);
                    if (currencyList != null)
                    {
                        foreach (var item in currencyList)
                        {
                            curCureency_.Items.Add(new ImageComboBoxItem(item.CurrencyName, item.CurrencyID));

                            _dicCurrency.Add(item.CurrencyID, item.CurrencyName);
                        }

                    }

                }
            }
        }
        /// <summary>
        /// 明细列表
        /// </summary>
        public List<ChargeList> charge
        {
            get
            {
                bsChargeList.EndEdit();

                List<ChargeList> list = bsChargeList.DataSource as List<ChargeList>;
                if (list == null)
                {
                    list = new List<ChargeList>();
                }

                return list;
            }
        }
        /// <summary>
        /// 账单列表
        /// </summary>
        public List<BillList> bill
        {
            get
            {
                bsBill.EndEdit();

                List<BillList> list = bsBill.DataSource as List<BillList>;
                if (list == null)
                {
                    list = new List<BillList>();
                }

                return list;
            }
        }
        /// <summary>
        /// 费用列表
        /// </summary>
        public List<InvoiceFeeDate> Details
        {
            get
            {
                gvChargeList.CloseEditor();
                List<InvoiceFeeDate> list = bsInvoiceFeeDate.DataSource as List<InvoiceFeeDate>;
                if (list == null)
                {
                    list = new List<InvoiceFeeDate>();
                }

                return list;
            }
        }
        /// <summary>
        /// 账单数据
        /// </summary>
        public CurrencyBillList BillDataSource
        {
            get
            {
                return bsBill.DataSource as CurrencyBillList;
            }
            set
            {
                bsBill.DataSource = value;

                bsBill.ResetBindings(false);

            }
        }
        /// <summary>
        /// 费用数据
        /// </summary>
        public object ChargeDataSource
        {
            get
            {
                return bsChargeList.DataSource;
            }
            set
            {
                List<ChargeList> list = value as List<ChargeList>;

                bsChargeList.DataSource = list;

                bsChargeList.ResetBindings(false);

            }
        }

        void InvoiceEdit_SmartPartClosing(object sender, WorkspaceCancelEventArgs e)
        {
            e.Cancel = !Leaving();
        }

        bool _isDirty;
        private bool IsDirty
        {
            get
            {
                if (_isDirty)
                {
                    return true;
                }

                if (_CurrentData.IsDirty || _CurrentData.ID == Guid.Empty)
                {
                    return true;
                }

                if (!FAMUtility.GuidIsNullOrEmpty(_CurrentData.ID))
                {
                    _CurrentData = FinanceService.GetInvoiceInfo(_CurrentData.ID, LocalData.IsEnglish);
                    if (_CurrentData != null && _CurrentData.CtnTypeName != cmbCtnType.Text)
                    {
                        return true;
                    }
                }

                foreach (InvoiceFeeDate item in Details)
                {
                    if (item.IsDirty)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public bool Leaving()
        {
            Refresh();
            if (!barSave.Enabled)
            {
                return true;
            }
            if (IsDirty)
            {
                if (FAMUtility.EnquireIsSaveCurrentDataByUpdated() == DialogResult.Yes)
                {
                    return SaveData();
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
        private void InitComboboxSource()
        {
            List<EnumHelper.ListItem<CustomerInvoiceType>> customerTypes = EnumHelper.GetEnumValues<CustomerInvoiceType>(LocalData.IsEnglish);
            cmbInvoiceType.Properties.Items.Clear();
            foreach (var item in customerTypes)
            {
                if (item.Value == CustomerInvoiceType.Unknown)
                {
                    continue;
                }
                cmbInvoiceType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }

            FAMUtility.BindComboBoxByCompany(cmbCompany);
            if (_CurrentData.CompanyID.IsNullOrEmpty())
            {
                cmbCompany.EditValue = LocalData.UserInfo.DefaultCompanyID;
                cmbCompany.Text = LocalData.UserInfo.DefaultCompanyName;
            }//默认
            if (_CurrentData.CompanyID == null)
            { SetBankAccountsByCompanyID(LocalData.UserInfo.DefaultCompanyID); }
            { SetBankAccountsByCompanyID(LocalData.UserInfo.DefaultCompanyID); }

            List<ContainerList> ctntypes = TransportFoundationService.GetContainerList(string.Empty, true, 0);
        }

        #region 关于银行帐号

        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            _isDirty = true;
            if (cmbCompany.EditValue == null)
            {
                return;
            }
            Guid companyID = new Guid(cmbCompany.EditValue.ToString());
            SetBankAccountsByCompanyID(companyID);
            BindCombo();

        }

        /// <summary>
        /// 缓存的银行帐号列表
        /// </summary>
        Dictionary<Guid, List<BankAccountList>> companyBankAccount = new Dictionary<Guid, List<BankAccountList>>();
        /// <summary>
        /// 设置银行帐号
        /// </summary>
        /// <param name="companyID"></param>
        private void SetBankAccountsByCompanyID(Guid companyID)
        {
            cmbBank1.Properties.Items.Clear();
            cmbBank2.Properties.Items.Clear();

            if (companyBankAccount.ContainsKey(companyID) == false)
            {
                List<BankAccountList> banks = FinanceService.GetBankAccountsOrderByRecentUsedFirst(companyID, LocalData.UserInfo.LoginID, LocalData.IsEnglish);
                companyBankAccount.Add(companyID, banks);
            }
            foreach (var item in companyBankAccount[companyID])
            {
                string bankAccountCurrency = item.BankName + "(" + item.AccountCurrencyName + ") " + item.AccountNo;
                cmbBank1.Properties.Items.Add(new ImageComboBoxItem(bankAccountCurrency, item.ID));
                cmbBank2.Properties.Items.Add(new ImageComboBoxItem(bankAccountCurrency, item.ID));
            }
            //cmbBank1.SelectedIndex = cmbBank2.SelectedIndex =-1;
        }

        #endregion

        SearchConditionCollection getSearchConditionCollection()
        {
            SearchConditionCollection searchList = new SearchConditionCollection();

            searchList.AddWithValue("IsInvoiceSearch", true, true);
            return searchList;
        }
        private IDisposable customerFinder, selectBillFinder, invoiceTitleFinder, polFinder, podFinder, placeOfDeliveryFinder, vesselFinder, voyageFinder, chargingCodeFinder;
        /// <summary>
        /// 注册搜索器
        /// </summary>
        private void SearchRegister()
        {
            #region Customer
            //注册客户搜索器
            customerFinder = DataFindClientService.Register(txtCustomer, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.CustomerResultValue,
                 delegate(object inputSource, object[] resultData)
                 {
                     Guid id = new Guid(resultData[0].ToString());
                     txtCustomer.Tag = _CurrentData.CustomerID = id;
                     txtCustomer.Text = _CurrentData.CustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();

                     BindCustomerTitleName(id);
                 },
                 delegate()
                 {
                     txtCustomer.Text = _CurrentData.CustomerName = string.Empty;
                     txtCustomer.Tag = _CurrentData.CustomerID = Guid.Empty;
                 },
                 ClientConstants.MainWorkspace);

            #endregion

            #region SelectBill


            selectBillFinder = DataFindClientService.RegisterMultiple(txtSelectBill, FAMFinderConstants.BillFinder
                                          , SearchFieldConstants.BillNoBLNo
                                          , SearchFieldConstants.BillResultValue
                                          , getSearchConditionCollection
             , delegate(object inputSource, object[] resultData)
             {
                 SelectedBill(resultData);
             }
             , null
             , null
             , ClientConstants.MainWorkspace);

            #endregion

            #region 发票抬头
            invoiceTitleFinder = DataFindClientService.Register(cmbInvoiceTitleName, CommonFinderConstants.InvoiceTitleFinder, SearchFieldConstants.CodeName, SearchFieldConstants.InvoiceTitleResultValue, GetConditionsForCustomer,
             delegate(object inputSource, object[] resultData)
             {
                 if (resultData != null && resultData.Length > 0)
                 {
                     if (resultData[0] != null)
                     {
                         taxCustomerID = new Guid(resultData[0].ToString());
                     }
                     #region 抬头
                     if (resultData[2] != null && !string.IsNullOrEmpty(resultData[2].ToString()))
                     {
                         string returnName = resultData[2].ToString();
                         string[] nameList = returnName.Split(Environment.NewLine.ToCharArray());
                         if (nameList != null && nameList.Length > 0)
                         {
                             foreach (string name in nameList)
                             {
                                 if (string.IsNullOrEmpty(name))
                                 {
                                     continue;
                                 }
                                 if (!cmbInvoiceTitleName.Properties.Items.Contains(name))
                                 {
                                     cmbInvoiceTitleName.Properties.Items.Insert(0, name);
                                 }
                             }
                             cmbInvoiceTitleName.EditValue = cmbInvoiceTitleName.Text = _CurrentData.TitleCName = nameList[0];
                         }
                     }
                     #endregion

                     #region 税号
                     if (resultData[3] != null && !string.IsNullOrEmpty(resultData[3].ToString()))
                     {
                         cmbCustomerTaxIDNo.Properties.BeginUpdate();
                         cmbCustomerTaxIDNo.Properties.Items.Clear();

                         string returnTaxNo = resultData[3].ToString();
                         string[] taxNoList = returnTaxNo.Split(Environment.NewLine.ToCharArray());
                         if (taxNoList != null && taxNoList.Length > 0)
                         {
                             foreach (string taxNo in taxNoList)
                             {
                                 if (string.IsNullOrEmpty(taxNo))
                                 {
                                     continue;
                                 }
                                 if (!cmbCustomerTaxIDNo.Properties.Items.Contains(taxNo))
                                 {
                                     cmbCustomerTaxIDNo.Properties.Items.Insert(0, taxNo);
                                 }
                             }
                             cmbCustomerTaxIDNo.EditValue = cmbCustomerTaxIDNo.Text = _CurrentData.CustomerTaxIDNo = taxNoList[0];
                         }
                         cmbCustomerTaxIDNo.Properties.EndUpdate();
                     }
                     #endregion

                     #region 地址
                     if (resultData[4] != null && !string.IsNullOrEmpty(resultData[4].ToString()))
                     {
                         txtCustomerAddressTel.Properties.BeginUpdate();
                         txtCustomerAddressTel.Properties.Items.Clear();

                         string returnAddress = resultData[4].ToString();
                         string[] addressList = returnAddress.Split(Environment.NewLine.ToCharArray());
                         if (addressList != null && addressList.Length > 0)
                         {
                             foreach (string addressTel in addressList)
                             {
                                 if (string.IsNullOrEmpty(addressTel))
                                 {
                                     continue;
                                 }
                                 if (!txtCustomerAddressTel.Properties.Items.Contains(addressTel))
                                 {
                                     txtCustomerAddressTel.Properties.Items.Insert(0, addressTel);
                                 }
                             }
                         }
                         txtCustomerAddressTel.EditValue = txtCustomerAddressTel.Text = _CurrentData.CustomerAddressTel = addressList[0];

                         txtCustomerAddressTel.Properties.EndUpdate();
                     }
                     #endregion

                     #region 银行帐号
                     if (resultData[6] != null && !string.IsNullOrEmpty(resultData[6].ToString()))
                     {
                         cmbCustomerBankAccountNo.Properties.BeginUpdate();
                         cmbCustomerBankAccountNo.Properties.Items.Clear();

                         string returnBankAccount = resultData[6].ToString();
                         string[] bankAccountList = returnBankAccount.Split(Environment.NewLine.ToCharArray());
                         if (bankAccountList != null && bankAccountList.Length > 0)
                         {
                             foreach (string bankAccountNo in bankAccountList)
                             {
                                 if (string.IsNullOrEmpty(bankAccountNo))
                                 {
                                     continue;
                                 }
                                 if (!cmbCustomerBankAccountNo.Properties.Items.Contains(bankAccountNo))
                                 {
                                     cmbCustomerBankAccountNo.Properties.Items.Insert(0, bankAccountNo);
                                 }
                                 cmbCustomerBankAccountNo.EditValue = cmbCustomerBankAccountNo.Text = _CurrentData.CustomerBankAccountNo = bankAccountList[0];
                             }
                         }
                         cmbCustomerBankAccountNo.Properties.EndUpdate();
                     }

                     #endregion

                     if (resultData[7] != null && !string.IsNullOrEmpty(resultData[7].ToString()))
                     {
                         CustomerInvoiceType invoiceType = (CustomerInvoiceType)resultData[7];
                         cmbInvoiceType.EditValue = _CurrentData.InvoiceType = invoiceType;
                     }
                 }
             },
             delegate()
             {

             },
             ClientConstants.MainWorkspace);
            #endregion

            #region Port

            #region POL
            polFinder = DataFindClientService.Register(txtPOL, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                    delegate(object inputSource, object[] resultData)
                    {
                        string portID = resultData[0].ToString();
                        txtPOL.Text = _CurrentData.POLName = resultData[2].ToString();

                    },
                    ClientConstants.MainWorkspace);
            #endregion

            #region POD
            podFinder = DataFindClientService.Register(txtPOD, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                    delegate(object inputSource, object[] resultData)
                    {
                        string portID = resultData[0].ToString();
                        txtPOD.Text = _CurrentData.PODName = resultData[2].ToString();
                    },
                    ClientConstants.MainWorkspace);
            #endregion

            #region PlaceOfDelivery
            placeOfDeliveryFinder = DataFindClientService.Register(txtPlaceOfDelivery, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                    delegate(object inputSource, object[] resultData)
                    {
                        string portID = resultData[0].ToString();
                        txtPlaceOfDelivery.Text = _CurrentData.PlaceOfDeliveryName = resultData[2].ToString();
                    },
                    ClientConstants.MainWorkspace);
            #endregion

            #endregion

            #region Voyage

            vesselFinder = DataFindClientService.Register(txtVessel,
                  CommonFinderConstants.VesselVoyageFinder,
                  SearchFieldConstants.Vessel,
                  SearchFieldConstants.VesselResultValue,
                  delegate(object inputSource, object[] resultData)
                  {
                      txtVoyage.Text = _CurrentData.Voyage = resultData[1].ToString();
                      txtVessel.Text = _CurrentData.Vessel = resultData[2].ToString();
                  },
                  ClientConstants.MainWorkspace);

            voyageFinder = DataFindClientService.Register(txtVoyage,
                CommonFinderConstants.VesselVoyageFinder,
                SearchFieldConstants.Voyage,
                SearchFieldConstants.VoyageResultValue,
                delegate(object inputSource, object[] resultData)
                {
                    txtVoyage.Text = _CurrentData.Voyage = resultData[1].ToString();
                    txtVessel.Text = _CurrentData.Vessel = resultData[2].ToString();
                },
                ClientConstants.MainWorkspace);

            #endregion

            #region chargingCode 费用项目

            chargingCodeFinder = DataFindClientService.RegisterGridColumnFinder(colChargingCode
                                     , CommonFinderConstants.SolutionChargingCodeFinder
                                      , new string[] { "ChargingCodeID", "ChargingCode" }
                                     , new string[] { "ChargingCodeID", "ChargingCodeName" }
                                     , GetSolutionChargingCodeSearchCondition);

            #endregion
        }

        /// <summary>
        /// 获得当前选择的客户
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetConditionsForCustomer()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("CustomerName", cmbInvoiceTitleName.Text, false);
            return conditions;
        }

        /// <summary>
        /// 绑定客户抬头 
        /// </summary>
        List<CustomerInvoiceTitleInfo> titleList = null;
        private void BindCustomerTitleName(Guid? customerID)
        {
            if (FAMUtility.GuidIsNullOrEmpty(customerID))
            {
                return;
            }
            titleList = CustomerService.GetCustomerInvoiceTitleList(customerID.Value, LocalData.UserInfo.DefaultCompanyID);
            //if (titleList == null)
            //{
            //    titleList = new List<CustomerInvoiceTitleInfo>();
            //}
            if (titleList == null)
                return;
            cmbInvoiceTitleName.Properties.Items.Clear();
            foreach (CustomerInvoiceTitleInfo item in titleList)
            {
                cmbInvoiceTitleName.Properties.Items.Add(item.Name);
            }

            CustomerInvoiceTitleInfo last = (from d in titleList orderby d.LastUseDate descending select d).Take(1).SingleOrDefault();
            //if (last != null)
            //{
            //    cmbInvoiceTitleName.Focus();

            //    BindCustomerTaxNo(last.TaxNo);
            //    BindCustomerAddressTel(last.AddressTel);
            //    BindCustomerBankAccountNo(last.BankAccountNo);

            //    //发票类型
            //    BindInvoiceType(last.InvoiceType);
            //    //string typeName = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<CustomerInvoiceType>(last.InvoiceType, false);
            //    //this.cmbInvoiceType.ShowSelectedValue(last.InvoiceType, typeName);
            //    //_CurrentData.InvoiceType = last.InvoiceType;
            //}
            if (last != null)
            {
                cmbInvoiceTitleName.EditValue = last.Name;
                cmbInvoiceTitleName.Text = last.Name;
            }
            else
            {
                if (titleList.Count > 0)
                {
                    cmbInvoiceTitleName.EditValue = cmbInvoiceTitleName.Text = titleList[0].Name;
                }
            }

        }
        /// <summary>
        /// 选择的抬头发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private bool IsLoadData = false;
        private void cmbInvoiceTitleName_SelectedValueChanged(object sender, EventArgs e)
        {
            if (titleList != null && cmbInvoiceTitleName.EditValue != null)
            {
                CustomerInvoiceTitleInfo obj = titleList.Where(o => o.Name == cmbInvoiceTitleName.EditValue.ToString()).Take(1).SingleOrDefault();
                if (obj != null)
                {
                    Thread.Sleep(500);
                    cmbInvoiceTitleName.Focus();

                    BindCustomerTaxNo(obj.TaxNo);
                    BindCustomerAddressTel(obj.AddressTel);
                    BindCustomerBankAccountNo(obj.BankAccountNo);

                    //发票类型
                    BindInvoiceType(obj.InvoiceType);
                }
            }
        }

        private void BindInvoiceType(CustomerInvoiceType invoiceType)
        {
            //KeyInput.SendKey(KeyboardConstaint.VK_TAB);
            //this.txtCustomerAddressTel.ShowPopup();
            switch (cmbInvoiceType.Text)
            {
                case "专用发票":
                    if (invoiceType == CustomerInvoiceType.Ordinary)
                        cmbInvoiceType.SelectedIndex = 0;
                    //KeyInput.SendKey(KeyboardConstaint.VK_DOWN);
                    break;
                case "普通发票":
                    if (invoiceType == CustomerInvoiceType.Dedicated)
                        cmbInvoiceType.SelectedIndex = 1;
                    //KeyInput.SendKey(KeyboardConstaint.VK_UP);
                    break;
                default:
                    if (invoiceType == CustomerInvoiceType.Dedicated)
                        cmbInvoiceType.SelectedIndex = 1;
                    // KeyInput.SendKey(KeyboardConstaint.VK_DOWN);
                    else
                    {
                        cmbInvoiceType.SelectedIndex = 0;
                        //KeyInput.SendKey(KeyboardConstaint.VK_DOWN);
                        //KeyInput.SendKey(KeyboardConstaint.VK_DOWN);
                    }
                    break;
            }
            KeyInput.SendKey(13);
            //this.txtCustomerAddressTel.ClosePopup();
        }

        private void BindCustomerAddressTel(string customerAddressTel)
        {
            string[] customerAddressTelList = customerAddressTel.Split(Environment.NewLine.ToCharArray());

            txtCustomerAddressTel.Properties.BeginUpdate();
            txtCustomerAddressTel.Properties.Items.Clear();
            txtCustomerAddressTel.EditValue = null;
            if (customerAddressTelList != null && customerAddressTelList.Length > 0)
            {
                foreach (string str in customerAddressTelList)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        txtCustomerAddressTel.Properties.Items.Add(str);
                    }
                }
                if (txtCustomerAddressTel.Properties.Items.Count > 0)
                {
                    txtCustomerAddressTel.SelectedIndex = 0;
                }
                //KeyInput.SendKey(KeyboardConstaint.VK_TAB);
                //this.txtCustomerAddressTel.ShowPopup();
                //KeyInput.SendKey(KeyboardConstaint.VK_DOWN);
                //KeyInput.SendKey(13);
                //this.txtCustomerAddressTel.ClosePopup();
            }
            txtCustomerAddressTel.Properties.EndUpdate();
        }

        private void BindCustomerBankAccountNo(string customerBankAccountNos)
        {
            string[] customerBankAccountNoList = customerBankAccountNos.Split(Environment.NewLine.ToCharArray());
            cmbCustomerBankAccountNo.Properties.BeginUpdate();
            cmbCustomerBankAccountNo.Properties.Items.Clear();
            cmbCustomerBankAccountNo.EditValue = null;
            if (customerBankAccountNoList != null && customerBankAccountNoList.Length > 0)
            {
                foreach (string str in customerBankAccountNoList)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        cmbCustomerBankAccountNo.Properties.Items.Add(str);
                    }
                }
                if (cmbCustomerBankAccountNo.Properties.Items.Count > 0)
                {
                    cmbCustomerBankAccountNo.SelectedIndex = 0;
                }
                //KeyInput.SendKey(KeyboardConstaint.VK_TAB);
                //this.cmbCustomerBankAccountNo.ShowPopup();
                //KeyInput.SendKey(KeyboardConstaint.VK_DOWN);
                //KeyInput.SendKey(13);
                //this.cmbCustomerBankAccountNo.ClosePopup();
            }
            cmbCustomerBankAccountNo.Properties.EndUpdate();
        }

        private void BindCustomerTaxNo(string customerTaxNos)
        {
            string[] customerTaxNoList = customerTaxNos.Split(Environment.NewLine.ToCharArray());
            cmbCustomerTaxIDNo.Properties.BeginUpdate();
            cmbCustomerTaxIDNo.Properties.Items.Clear();
            cmbCustomerTaxIDNo.EditValue = null;
            if (customerTaxNoList != null && customerTaxNoList.Length > 0)
            {
                foreach (string str in customerTaxNoList)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        cmbCustomerTaxIDNo.Properties.Items.Add(str);
                    }
                }
                if (cmbCustomerTaxIDNo.Properties.Items.Count > 0)
                {
                    cmbCustomerTaxIDNo.SelectedIndex = 0;
                }
            }
            cmbCustomerTaxIDNo.Properties.EndUpdate();
            //KeyInput.SendKey(KeyboardConstaint.VK_TAB);
            //this.cmbCustomerTaxIDNo.ShowPopup();
            //KeyInput.SendKey(KeyboardConstaint.VK_DOWN);
            //KeyInput.SendKey(13);
            //this.cmbCustomerTaxIDNo.ClosePopup();
        }

        List<CustomerInvoiceTitleInfo> InvoiceTitleList = new List<CustomerInvoiceTitleInfo>();

        #region old code

        /// <summary>
        /// 设置客户税号、银行信息、地址
        /// </summary>
        //private void SetCustomerTaxAndBankInfo(bool isSetValue)
        //{
        //    if (_CurrentData == null || Utility.GuidIsNullOrEmpty(_CurrentData.CustomerID))
        //    {
        //        BindCustomerBankAccountNo(string.Empty, isSetValue);
        //        BindCustomerTaxNo(string.Empty, isSetValue);
        //        BindCustomerAddressTel(string.Empty, isSetValue);
        //    }
        //    else if (!string.IsNullOrEmpty(cmbInvoiceTitleName.Text))
        //    {
        //        InvoiceTitleList = CustomerService.GetCustomerInvoiceTitleList(_CurrentData.CustomerID.ToGuid(), cmbInvoiceTitleName.Text, LocalData.UserInfo.DefaultCompanyID);
        //        if (InvoiceTitleList == null || InvoiceTitleList.Count == 0)
        //        {
        //            BindCustomerBankAccountNo(string.Empty, isSetValue);
        //            BindCustomerTaxNo(string.Empty, isSetValue);
        //            BindCustomerAddressTel(string.Empty, isSetValue);
        //        }
        //        else
        //        {
        //            BindCustomerTaxNo(InvoiceTitleList[0].TaxNo, isSetValue);
        //            BindCustomerAddressTel(InvoiceTitleList[0].AddressTel, isSetValue);
        //            BindCustomerBankAccountNo(InvoiceTitleList[0].BankAccountNo, isSetValue);

        //            if (isSetValue)
        //            {
        //                string typeName = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<CustomerInvoiceType>(InvoiceTitleList[0].InvoiceType, false);
        //                this.cmbInvoiceType.ShowSelectedValue(InvoiceTitleList[0].InvoiceType, typeName);
        //                _CurrentData.InvoiceType = InvoiceTitleList[0].InvoiceType;
        //            }
        //        }
        //    }
        //}
        /// <summary>
        /// 绑定客户地址&电话
        /// </summary>
        //private void BindCustomerAddressTel(string customerAddressTel, bool isSetValue)
        //{
        //    if (customerAddressTel == null)
        //    {
        //        customerAddressTel = string.Empty;
        //    }
        //    string[] customerAddressTelList = customerAddressTel.Split(System.Environment.NewLine.ToCharArray());

        //    this.txtCustomerAddressTel.Properties.Items.Clear();
        //    if (customerAddressTelList != null && customerAddressTelList.Length > 0)
        //    {
        //        foreach (string str in customerAddressTelList)
        //        {
        //            if (!string.IsNullOrEmpty(str))
        //            {
        //                this.txtCustomerAddressTel.Properties.Properties.Items.Add(str);
        //            }
        //        }
        //        if (isSetValue)
        //        {
        //            this.txtCustomerAddressTel.EditValue = this.txtCustomerAddressTel.Text = _CurrentData.CustomerAddressTel = customerAddressTelList[0];
        //        }
        //    }
        //}
        /// <summary>
        /// 绑定客户税号
        /// </summary>
        //private void BindCustomerTaxNo(string customerTaxNos, bool isSetValue)
        //{
        //    if (customerTaxNos == null)
        //    {
        //        customerTaxNos = string.Empty;
        //    }
        //    string[] customerTaxNoList = customerTaxNos.Split(System.Environment.NewLine.ToCharArray());

        //    this.cmbCustomerTaxIDNo.Properties.Items.Clear();
        //    if (customerTaxNoList != null && customerTaxNoList.Length > 0)
        //    {
        //        foreach (string str in customerTaxNoList)
        //        {
        //            if (!string.IsNullOrEmpty(str))
        //            {
        //                this.cmbCustomerTaxIDNo.Properties.Items.Add(str);
        //            }
        //        }
        //        if (isSetValue)
        //        {
        //            this.cmbCustomerTaxIDNo.EditValue = this.cmbCustomerTaxIDNo.Text = _CurrentData.CustomerTaxIDNo = customerTaxNoList[0];
        //        }
        //    }
        //}
        /// <summary>
        /// 绑定客户银行帐号
        /// </summary>
        /// <param name="customerBankAccountNos"></param>
        //private void BindCustomerBankAccountNo(string customerBankAccountNos,bool isSetValue)
        //{
        //    if (customerBankAccountNos == null)
        //    {
        //        customerBankAccountNos = string.Empty;
        //    }

        //    string[] customerBankAccountNoList = customerBankAccountNos.Split(System.Environment.NewLine.ToCharArray());

        //    this.cmbCustomerBankAccountNo.Properties.Items.Clear();
        //    if (customerBankAccountNoList != null && customerBankAccountNoList.Length > 0)
        //    {
        //        foreach (string str in customerBankAccountNoList)
        //        {
        //            if (!string.IsNullOrEmpty(str))
        //            {
        //                this.cmbCustomerBankAccountNo.Properties.Items.Add(str);
        //            }
        //        }
        //        if (isSetValue)
        //        {
        //            this.cmbCustomerBankAccountNo.EditValue = this.cmbCustomerBankAccountNo.Text = _CurrentData.CustomerBankAccountNo = customerBankAccountNoList[0];
        //        }
        //    }

        //}

        #endregion

        SearchConditionCollection GetSolutionChargingCodeSearchCondition()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("SolutionID", SolutionID, false);
            return conditions;
        }
        void billFinder_DataChoosed(object sender, DataFindEventArgs e)
        {
            CurrencyBillList[] bills = e.Data as CurrencyBillList[];
            SelectedBill(bills);


        }
        public void SelectedBill(object[] resultData)
        {
            if (resultData == null || resultData.Length == 0) return;

            #region 绑定账单&费用列表
            List<BillList> billList = new List<BillList>();
            Guid operationID = Guid.Empty;
            Guid billID = Guid.Empty;
            string customerName = string.Empty;
            string customerEName = string.Empty;
            Guid customerID = Guid.Empty;

            Boolean isSelectData = false;
            if (CurrentBillSource == null || CurrentBillSource.Count == 0)
            {
                isSelectData = true;
            }
            //返回的是CurrencyBillList 要转换一下
            foreach (var item in resultData)
            {
                CurrencyBillList listItem = item as CurrencyBillList;
                if (listItem != null)
                {
                    BillList billItem = new BillList();
                    billItem.ID = listItem.ID;
                    billItem.CustomerName = listItem.CustomerName;
                    billItem.No = listItem.BillNO;
                    billItem.CompanyID = listItem.CompanyID;

                    if (customerID == Guid.Empty)
                    {
                        customerID = listItem.CustomerID;
                        customerName = listItem.CustomerName;
                        customerEName = listItem.CustomerEName;
                    }
                    //billItem.IsDirty = false;
                    billList.Add(billItem);

                    #region 把帐单业务号填充到备注
                    if (!string.IsNullOrEmpty(_CurrentData.Remark))
                    {
                        _CurrentData.Remark += " ";
                    }

                    _CurrentData.Remark += listItem.OperationNO;
                    #endregion

                    if (operationID == Guid.Empty)
                    {
                        operationID = listItem.OperationID;
                        billID = listItem.ID;
                    }
                }
            }

            if (billList.Count == 0)
            {
                FAMUtility.ShowMessage("没有找到账单，请输入正确的业务号、账单号或提单号。");
                return;
            }
            List<BillList> currentBills = bsBill.DataSource as List<BillList>;
            if (currentBills == null)
            {
                currentBills = new List<BillList>();
            }


            List<Guid> newBillIds = new List<Guid>();


            foreach (var item in billList)
            {
                if (currentBills.Find(delegate(BillList b) { return b.ID == item.ID; }) == null)//过滤重复
                {
                    currentBills.Add(item);

                    newBillIds.Add(item.ID);
                }
            }

            List<ChargeList> fees = FinanceService.GetChargeList(newBillIds.ToArray());
            foreach (var item in currentBills)
            {
                //只需要将本次新增发数据绑定费用，之前的数据不用管
                if (newBillIds.Contains(item.ID))
                {
                    List<ChargeList> cfs = fees.FindAll(delegate(ChargeList b) { return b.BillID == item.ID; });
                    if (cfs != null) item.Fees = cfs;
                }
            }

            bsBill.DataSource = currentBills;
            bsBill.ResetBindings(false);
            gcBill.RefreshDataSource();

            TotalCurrenAmountBill();
            #endregion

            #region 绑定业务信息界面
            ///之前没有选择账单,才绑定业务信息
            if (operationID != Guid.Empty && isSelectData)
            {
                BusinessByInvoice business = FinanceService.GetBusinessByInvoice(new Guid[1] { operationID }, new Guid[1] { billID }, LocalData.IsEnglish);
                if (business != null)
                {
                    if (customerID == Guid.Empty)
                    {
                        _CurrentData.CustomerID = business.CustomerID;
                        _CurrentData.CustomerName = LocalData.IsEnglish ? business.CustomerEName : business.CustomerCName;
                    }
                    else
                    {
                        _CurrentData.CustomerID = customerID;
                        _CurrentData.CustomerName = customerName;
                    }



                    _CurrentData.PODName = business.PODName;
                    _CurrentData.POLName = business.POLName;
                    _CurrentData.SONo = business.SoNo;
                    _CurrentData.BLNo = business.BLNo;//提单号
                    if (business.ETD != null)
                    {
                        _CurrentData.ETD = business.ETD.Value;
                    }
                    _CurrentData.PlaceOfDeliveryName = business.PlaceOfDeliveryName;
                    _CurrentData.Vessel = business.Vessel;
                    _CurrentData.CtnTypeName = business.CtnTypeName;
                    _CurrentData.ContainerNo = business.CtnNo;
                    _CurrentData.Voyage = business.Voyage;
                    _CurrentData.CtnTypeName = business.CtnTypeName;
                    cmbCtnType.Text = business.CtnTypeName;
                    _CurrentData.InvoiceDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    bindingSource1.ResetBindings(false);

                    //在无账单情况下，取用户的默认公司。有账单时，根据账单的所属公司重新刷新显示。2011-11-02
                    if (business.CompanyID == null) { return; }
                    try
                    {
                        cmbCompany.EditValue = business.CompanyID;
                        cmbCompany.SelectedText = business.CompanyName;
                        _CurrentData.CompanyID = business.CompanyID;
                        SetBankAccountsByCompanyID(business.CompanyID);
                        Refresh();
                        BindCombo();
                    }
                    catch (Exception)
                    {
                        return;
                    }

                }
            }

            #endregion
        }

        #endregion

        #region 本地方法

        InvoiceFeeDate CopyChargeBillToInvoiceFeeDate(ChargeList chargeList)
        {
            InvoiceFeeDate fee = new InvoiceFeeDate();
            fee.ID = Guid.Empty;
            fee.Amount = chargeList.Amount;
            fee.BillFeeId = chargeList.ID;
            fee.ChargingCode = chargeList.ChargingCode;
            fee.ChargingCodeID = chargeList.ChargingCodeID;
            fee.CurrencyID = chargeList.CurrencyID;
            fee.CurrencyName = chargeList.CurrencyName;
            fee.Quantity = chargeList.Quantity;
            //fee.Rate = chargeList.Rate;
            fee.Remark = chargeList.Remark;
            fee.CreateByName = LocalData.UserInfo.LoginName;
            fee.CreateByID = LocalData.UserInfo.LoginID;
            fee.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);


            List<SolutionExchangeRateList> rate;
            DateTime time;
            if (string.IsNullOrEmpty(_CurrentData.InvoiceDate.ToString()))
            {
                time = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            }
            else
            {
                time = _CurrentData.InvoiceDate;
            }
            //获取汇率列表

            rate = FinanceService.GetInvoiceExchangeRateList(true);


            Guid TargetCurrencyId = new Guid("DEB5F402-B6C0-4491-B247-B75C3EDA7976");//RMB为目标币种
            decimal rateValue = decimal.Round(RateHelper.GetRate(fee.CurrencyID, TargetCurrencyId, time, rate), 4);
            fee.Rate = rateValue;


            //财务部需要在费用的备注中显示对应的业务号，以方便进行数据的查询
            if (string.IsNullOrEmpty(fee.Remark))
            {
                fee.Remark = chargeList.BillNo;
            }
            else
            {
                fee.Remark = fee.Remark + Environment.NewLine + "Bill No:" + chargeList.BillNo;
            }


            fee.IsDirty = false;
            return fee;
        }

        #endregion

        #region 本地变量
        private string InvoiceNoTemp = "";

        private static Hashtable processWnd = new Hashtable();
        public Guid? taxCustomerID
        {
            get;
            set;
        }
        List<Guid> _existSelectBill;
        InvoiceInfo _CurrentData;
        BillList CurrentBill
        {
            get { return bsBill.Current as BillList; }
        }
        List<BillList> CurrentBillSource
        {
            get { return bsBill.DataSource as List<BillList>; }
        }
        List<ChargeList> CurrentChargeSource
        {
            get { return bsChargeList.DataSource as List<ChargeList>; }
        }
        ChargeList CurrentChargeRow
        {
            get { return bsChargeList.Current as ChargeList; }
        }
        List<InvoiceFeeDate> CurrentInvoiceFeeSource
        {
            get
            {
                List<InvoiceFeeDate> list = bsInvoiceFeeDate.DataSource as List<InvoiceFeeDate>;
                if (list == null)
                {
                    list = new List<InvoiceFeeDate>();
                }

                return list;
            }
        }

        InvoiceFeeDate CurrentInvoiceFee
        {
            get { return bsInvoiceFeeDate.Current as InvoiceFeeDate; }
        }

        /// <summary>
        /// 明细当前行
        /// </summary>
        private InvoiceFeeDate CurrentRow
        {
            get
            {
                return bsInvoiceFeeDate.Current as InvoiceFeeDate;
            }
        }

        public string GetDetailAmount
        {
            get
            {
                string str = string.Empty;
                foreach (InvoiceFeeDate item in CurrentInvoiceFeeSource)
                {
                    str = str + "  名称:" + item.ChargingCode + "    金额:" + item.Amount.ToString("F2");
                }
                return str;
            }
        }

        int SH = Screen.PrimaryScreen.Bounds.Height;

        int SW = Screen.PrimaryScreen.Bounds.Width;
        #endregion
        [DllImport("user32.dll")]
        private static extern IntPtr GetFocus();
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        #region barItem Click

        #region
        /// <summary>
        /// 保存发票
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                SaveData();
            }
        }

        private void barPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                PrintInvoice();
            }
        }

        private void barEditInvoiceNo_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                EditInvoiceNo();
            }
        }

        private void btnDeleteBill_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                DeleteBill();
            }
        }

        private void barAddFee_ItemClick(object sender, ItemClickEventArgs e)
        {
            InvoiceFeeDate feeNew = new InvoiceFeeDate();
            feeNew.ID = Guid.Empty;
            feeNew.CreateByID = LocalData.UserInfo.LoginID;
            feeNew.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

            if (Details == null || Details.Count == 0)
            {
                bsInvoiceFeeDate.DataSource = new List<InvoiceFeeDate>();
            }

            bsInvoiceFeeDate.Add(feeNew);
            bsInvoiceFeeDate.ResetBindings(false);
            _isDirty = true;
        }

        private void barRemoveFee_ItemClick(object sender, ItemClickEventArgs e)
        {
            RemoveFee();
        }

        private void barClearFee_ItemClick(object sender, ItemClickEventArgs e)
        {
            ClearFee();
        }

        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().Close();
        }
        #endregion

        #region Save

        public override bool SaveData()
        {
            if (!ValidateData()) return false;
            #region 构建列表参数

            List<Guid> feeIds = new List<Guid>();
            List<Guid> billFeeIds = new List<Guid>(), feeCurrencyIds = new List<Guid>(), feeChargingCodeIds = new List<Guid>();
            List<decimal> feeRates = new List<decimal>(), feeQuantities = new List<decimal>(), feeAmounts = new List<decimal>();
            List<string> feeRemarks = new List<string>();
            List<DateTime?> feeUpdateDates = new List<DateTime?>();
            string s = _CurrentData.CustomerBankAccountNo;
            foreach (InvoiceFeeDate fee in CurrentInvoiceFeeSource)
            {
                feeIds.Add(fee.ID);
                billFeeIds.Add(fee.BillFeeId == null ? Guid.Empty : fee.BillFeeId.Value);
                feeCurrencyIds.Add(fee.CurrencyID);
                feeChargingCodeIds.Add(fee.ChargingCodeID);
                if (fee.Rate <= 0)
                {
                    string message = LocalData.IsEnglish ? "The exchange rate must be greater than zero" : "汇率必须大于零！";
                    FAMUtility.ShowMessage(message);
                    return false;
                }
                feeRates.Add(fee.Rate);
                feeQuantities.Add(fee.Quantity);
                if (fee.Amount.ToString().Length > 21)
                {
                    return false;
                }
                feeAmounts.Add(fee.Amount);
                feeRemarks.Add(fee.Remark);
                feeUpdateDates.Add(fee.UpdateDate);
            }
            #endregion

            #region 调用服务
            try
            {
                HierarchyManyResult result = FinanceService.SaveInvoiceInfo(_CurrentData.ID
                                        , _CurrentData.InvoiceNo
                                        , _CurrentData.InvoiceDate
                                        , _CurrentData.ExpressNo
                                        , _CurrentData.ExpressDate
                                        , _CurrentData.CompanyID
                                        , _CurrentData.Bank1ID
                                        , _CurrentData.Bank2ID
                                        , _CurrentData.Tax
                                        , _CurrentData.CustomerID
                                        , _CurrentData.TitleCName
                                        , _CurrentData.TitleEName
                                        , taxCustomerID
                                        , LocalData.UserInfo.DefaultCompanyID
                                        , _CurrentData.CustomerAddressTel
                                        , _CurrentData.CustomerTaxIDNo
                                        , _CurrentData.CustomerBankAccountNo
                                        , _CurrentData.ReceivablesName
                                        , _CurrentData.ReviewName
                                        , _CurrentData.InvoiceType
                                        , _CurrentData.SONo
                                        , _CurrentData.ContainerNo
                                        , cmbCtnType.Text //_CurrentData.CtnTypeID
                                        , _CurrentData.BLNo
                                        , _CurrentData.ETD
                                        , _CurrentData.POLName
                                        , _CurrentData.PODName
                                        , _CurrentData.PlaceOfDeliveryName
                                        , _CurrentData.Vessel
                                        , _CurrentData.Voyage
                                        , _CurrentData.Remark
                                        , _CurrentData.UpdateDate
                                        , billFeeIds.ToArray()
                                        , feeIds.ToArray()
                                        , feeCurrencyIds.ToArray()
                                        , feeChargingCodeIds.ToArray()
                                        , feeRates.ToArray()
                                        , feeQuantities.ToArray()
                                        , feeAmounts.ToArray()
                                        , feeRemarks.ToArray()
                                        , feeUpdateDates.ToArray()
                                        , LocalData.UserInfo.LoginID
                                        , LocalData.IsEnglish);

                #endregion

                //更新主表
                _CurrentData.ID = result.GetValue<Guid>("ID");
                _CurrentData.No = result.GetValue<String>("No");
                _CurrentData.InvoiceNo = result.GetValue<string>("invoiceNo");
                _CurrentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                _CurrentData.BeginEdit();
                if (Saved != null)
                {
                    Saved(_CurrentData, new object[] { result });
                }
                _CurrentData.CancelEdit();

                //记录发票号的变化
                if (OldInvoiceNo != _CurrentData.InvoiceNo)
                {
                    InvoiceLogHelper.SaveLogInfo(_CurrentData.No + "   手工变更发票号:  " + OldInvoiceNo + "-->" + _CurrentData.InvoiceNo);
                }
                Stopwatch watch = new Stopwatch();
                //记录金额信息
                InvoiceLogHelper.SaveAmountLogInfo(_CurrentData.No + "    " + GetDetailAmount);

                OldInvoiceNo = _CurrentData.InvoiceNo;
                //明细更新
                _CurrentData.Fees = bsInvoiceFeeDate.DataSource as List<InvoiceFeeDate>;
                for (int i = 0; i < _CurrentData.Fees.Count; i++)
                {
                    _CurrentData.Fees[i].ID = result.Childs[i].GetValue<Guid>("ID");
                    _CurrentData.Fees[i].UpdateDate = result.Childs[i].GetValue<DateTime?>("UpdateDate");

                }

                bsInvoiceFeeDate.DataSource = _CurrentData.Fees;
                bsInvoiceFeeDate.ResetBindings(false);

                _isDirty = _CurrentData.IsDirty = false;
                foreach (InvoiceFeeDate item in Details)
                {
                    item.IsDirty = false;
                }

                if (string.IsNullOrEmpty(_CurrentData.InvoiceNo))
                {

                }
                else
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                }

                Refresh();
                SetReviewNameInfo();

                Thread.Sleep(1000);
                if (string.IsNullOrEmpty(_CurrentData.InvoiceNo))
                {
                    //保存日志
                    InvoiceLogHelper.SaveLogInfo("传输数据到税控中  " + _CurrentData.No);
                    Process[] pros = Process.GetProcesses();

                    //税控
                    if (_CurrentData.CompanyID == new Guid("B1AFAD8F-55DD-4E29-A250-EB82AB3971FE"))//大连公司
                    {
                        SetSHTaxSystemData();
                    }
                    else if (_CurrentData.CompanyID == new Guid("A62A9F8E-E69C-4E6E-AD85-E75AED3C6CF9") //宁波公司
                        || _CurrentData.CompanyID == new Guid("2AFAC53A-2AF9-46ED-8C4B-7035FEDC0279")   //厦门公司
                        || _CurrentData.CompanyID == new Guid("F289109A-C29E-4B0B-A41A-C22D9E70A72F")   //青岛公司
                        || _CurrentData.CompanyID == new Guid("D8D57403-D663-4A93-A927-144907B7963B")   //天津公司
                        || _CurrentData.CompanyID == new Guid("FD69B51B-E71F-4040-8F4B-28447A003C93")   //广州公司
                        || _CurrentData.CompanyID == new Guid("41D7D3FE-183A-41CD-A725-EB6F728541EC")   //深圳公司
                        || _CurrentData.CompanyID == new Guid("62D46581-B6CC-477E-8A60-7375FACD9813")   //连云港公司
                        || _CurrentData.CompanyID == new Guid("B13FAC2D-8250-4990-A622-5ECA00D3A030")   //上海公司
                        || _CurrentData.CompanyID == new Guid("1EA069FE-2E98-E711-80C1-141877442141")   //武汉市达吾国际船务代理有限公司
                        || _CurrentData.CompanyID == new Guid("BEA46D1D-6CEF-E711-80C2-141877442141")   //达吾船务长沙办事处
                        || _CurrentData.CompanyID == new Guid("9A4725F3-E7CF-4AD3-91A4-988892AF2781")   //湖南公司
                        || _CurrentData.CompanyID == new Guid("7523BB15-33A1-E811-B0BF-C013731E3971"))  //宁波跨境电商
                    {
                        SetNBTaxSystemData();
                    }
                    else
                    {
                        SetTaxSystemData();
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
                return false;
            }
        }
        /// <summary>
        /// 保存收款、复核人信息
        /// </summary>
        private void SetReviewNameInfo()
        {
            ClientConfig.Current.AddValue(InvoiceCommandConstants.InvoiceReceivablesName, _CurrentData.ReceivablesName);
            ClientConfig.Current.AddValue(InvoiceCommandConstants.InvoiceReviewName, _CurrentData.ReviewName);
        }
        private bool ValidateData()
        {
            EndEdit();
            bsInvoiceFeeDate.EndEdit();
            bsChargeList.EndEdit();

            if (!_CurrentData.Validate())
            {
                return false;
            }

            if (_CurrentData.InvoiceType == null || _CurrentData.InvoiceType == CustomerInvoiceType.Unknown)
            {
                string message = LocalData.IsEnglish ? "Please select the InvoiceType" : "请选择发票类型!";
                FAMUtility.ShowMessage(message);
                return false;
            }

            if (Details.Count == 0)
            {
                string message = LocalData.IsEnglish ? "One Fee at least" : "至少有一条费用!";
                FAMUtility.ShowMessage(message);
                return false;
            }

            foreach (InvoiceFeeDate fee in Details)
            {
                if (!fee.Validate())
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region EditInvoiceNo编辑发票号
        private void EditInvoiceNo()
        {
            txtInvoiceNo.Properties.ReadOnly = false;
            txtInvoiceNo.Leave -= new EventHandler(txtInvoiceNo_Leave);
            txtInvoiceNo.Leave += new EventHandler(txtInvoiceNo_Leave);
        }

        void txtInvoiceNo_Leave(object sender, EventArgs e)
        {
            txtInvoiceNo.Properties.ReadOnly = true;
        }

        #endregion

        #region 发票打印
        private void PrintInvoice()
        {
            _CurrentData.EndEdit();
            //数据有改变时先保存,保存成功后再进行打印,无需进行提示  
            if (IsDirty)
            {
                bool result = SaveData();
                if (!result)
                {
                    return;
                }
            }

            try
            {
                Refresh();
                string[] reportPara = new string[7];
                PrintSet expressPart = Workitem.Items.AddNew<PrintSet>();

                if (Details.Count > 0)
                {
                    expressPart.DataSource = Details[0];
                }

                string title = LocalData.IsEnglish ? "Report Set" : "报表设置";
                expressPart.Saved += delegate(object[] prams)
                {
                    if (prams != null && prams.Length > 0)
                    {
                        string[] configs = prams[0] as string[];//模态窗口,返回值
                        if (configs != null)
                        {
                            reportPara[0] = configs[0].ToString();//代理文本
                            if (reportPara[0] == "true") { reportPara[0] = "代理"; } else { reportPara[0] = string.Empty; }
                            reportPara[1] = configs[1].ToString();//本位币
                            if (reportPara[1] == "true") { reportPara[1] = "RMB"; } else { reportPara[1] = string.Empty; }
                            reportPara[2] = configs[2].ToString();//公司
                            reportPara[3] = configs[3].ToString();//币种
                            reportPara[4] = configs[4].ToString();//汇率
                            reportPara[5] = configs[5].ToString();//新旧格式
                            reportPara[6] = configs[6].ToString();//报表种类
                        }
                    }
                };

                bindingSource1.ResetBindings(false);
                bindingSource1.ResetCurrentItem();
                if (PartLoader.ShowDialog(expressPart, title) != DialogResult.OK)
                    return;

                Refresh();
                _CurrentData = bindingSource1.DataSource as InvoiceInfo;
                InvoiceInfo invoice = _CurrentData;

                invoice.EndEdit();

                if (companyBankAccount[LocalData.UserInfo.DefaultCompanyID] != null && string.IsNullOrEmpty(invoice.Bank1Name) && !FAMUtility.GuidIsNullOrEmpty(invoice.Bank1ID))
                {
                    foreach (var item in companyBankAccount[LocalData.UserInfo.DefaultCompanyID])
                    {
                        if (item.ID == invoice.Bank1ID)
                        {
                            invoice.Bank1Name = item.CurrencyName;
                            break;
                        }
                    }
                }

                if (companyBankAccount[LocalData.UserInfo.DefaultCompanyID] != null && string.IsNullOrEmpty(invoice.Bank2Name) && !FAMUtility.GuidIsNullOrEmpty(invoice.Bank2ID))
                {
                    foreach (var item in companyBankAccount[LocalData.UserInfo.DefaultCompanyID])
                    {
                        if (item.ID == invoice.Bank2ID)
                        {
                            invoice.Bank2Name = item.CurrencyName;
                            break;
                        }
                    }
                }

                invoice.IsDirty = false;

                foreach (var fee in Details)
                {
                    if (_dicCurrency.Keys.Contains(fee.CurrencyID))
                    {
                        fee.CurrencyName = _dicCurrency[fee.CurrencyID];
                        fee.IsDirty = false;
                    }
                }


                Dictionary<string, object> reportSource = InvoiceReportHelper.Print(invoice, Details, reportPara);
                string fileNameNew = Application.StartupPath + "\\Reports\\FAM\\Invoice\\transportation\\New\\InvoiceReportSZ.frx";
                string fileNameOld = Application.StartupPath + "\\Reports\\FAM\\Invoice\\transportation\\Old\\InvoiceReportSZ.frx";
                string fileNameNew_TD = Application.StartupPath + "\\Reports\\FAM\\Invoice\\transportation\\New\\InvoiceReportSZ_TD.frx";

                //string fileNameOld_TD = System.Windows.Forms.Application.StartupPath + "\\Reports\\FAM\\Invoice\\transportation\\Old\\InvoiceReportSZ_TD.frx";
                string fileNameOld_TD = string.Empty;
                if (LocalData.UserInfo.DefaultCompanyID == _daLianCompanyID) //大连公司要求的模板
                {
                    fileNameOld_TD = Application.StartupPath + "\\Reports\\FAM\\Invoice\\transportation\\Old\\InvoiceReport_TDForDalian.frx";
                }
                else
                {
                    fileNameOld_TD = Application.StartupPath + "\\Reports\\FAM\\Invoice\\transportation\\Old\\InvoiceReportSZ_TD.frx";
                }

                string fileNameNew_SHIP = Application.StartupPath + "\\Reports\\FAM\\Invoice\\transportation\\New\\InvoiceReportSZ_SHIP.frx";
                string fileNameOld_SHIP = Application.StartupPath + "\\Reports\\FAM\\Invoice\\transportation\\Old\\InvoiceReportSZ_SHIP.frx";
                IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Invoice" : "发票打印", (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);
                if (reportPara[5].ToLower() == "new" && reportPara[6] == "Own") { viewer.BindData(fileNameNew, reportSource, null); }//新格式自有格式
                else if (reportPara[5].ToLower() == "new" && reportPara[6] == "Chromatography") { viewer.BindData(fileNameNew_TD, reportSource, null); }//新格式套打
                else if (reportPara[5].ToLower() == "new" && reportPara[6] == "Ship") { viewer.BindData(fileNameNew_SHIP, reportSource, null); }//新格式船务
                else if (reportPara[5].ToLower() == "old" && reportPara[6] == "Own") { viewer.BindData(fileNameOld, reportSource, null); }//旧格式自有
                else if (reportPara[5].ToLower() == "old" && reportPara[6] == "Chromatography") { viewer.BindData(fileNameOld_TD, reportSource, null); }//旧格式套打
                else if (reportPara[5].ToLower() == "old" && reportPara[6] == "Ship") { viewer.BindData(fileNameOld_SHIP, reportSource, null); }//旧格式船务       
                else { viewer.BindData(fileNameOld, reportSource, null); }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }
        #endregion
        /// <summary>
        /// 总费用(报表用)
        /// </summary>
        /// <summary>
        /// 应收
        /// </summary>
        private string TotalCurrenAmountReport()
        {
            Dictionary<Guid, Decimal> dicAmount = (from d in Details group d by d.CurrencyID into g select new { g.Key, TotalAcmount = g.Sum(p => p.Amount) }).ToDictionary(c => c.Key, c => c.TotalAcmount);
            InvoiceReportOthers other = new InvoiceReportOthers();
            foreach (var item in dicAmount)
            {
                string currencyName = string.Empty;
                if (_dicCurrency.Keys.Contains(item.Key))
                {
                    currencyName = _dicCurrency[item.Key];
                }
                else
                {
                    currencyName = LocalData.IsEnglish ? "Unknown" : "未知";
                }

                other.TotalAmout += item.Value.ToString("0.00");
            }
            return other.TotalAmout;
        }
        #region 删除账单
        private void DeleteBill()
        {
            try
            {
                if (CurrentBill == null) return;
                int count = CurrentBill.Fees.Count;

                string message = NativeLanguageService.GetText(this, "1110170003");
                message = string.Format(message, count);

                if (!FAMUtility.ShowResultMessage(message))
                {
                    return;
                }
                else
                {
                    List<Guid> needDeleteChargeIDs = new List<Guid>();
                    foreach (var item in CurrentBill.Fees)
                    {
                        needDeleteChargeIDs.Add(item.ID);
                    }
                    List<InvoiceFeeDate> chargList = CurrentInvoiceFeeSource;
                    //InvoiceFeeDate needRemove = chargList.Find(delegate(InvoiceFeeDate item) { return item.BillFeeId == item.ID; });
                    //chargList.Remove(needRemove);
                    //List<InvoiceFeeDate> newList = chargList.FindAll(delegate(InvoiceFeeDate item) { return needDeleteChargeIDs.Contains(item.ID) == false; });
                    List<InvoiceFeeDate> newList = chargList.FindAll(delegate(InvoiceFeeDate item) { return needDeleteChargeIDs.Contains(item.BillFeeId.Value) == false; });

                    if (newList == null) newList = new List<InvoiceFeeDate>();
                    bsInvoiceFeeDate.DataSource = newList;
                    bsInvoiceFeeDate.ResumeBinding();
                    bsInvoiceFeeDate.ResetBindings(false);

                    int[] selectRowsHandle = gvBill.GetSelectedRows();

                    if (selectRowsHandle == null || selectRowsHandle.Length == 0) return;

                    for (int i = 0; i < selectRowsHandle.Length; i++)
                    {
                        int row = selectRowsHandle[i] - i;
                        gvBill.DeleteRow(row);
                    }

                    TotalCurrenAmountBill();
                    _CurrentData.IsDirty = true;
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Delete Successfully" : "删除成功");
                }
                if (gvBill.RowCount < 1)//账单无数据时刷新
                {
                    _CurrentData = new InvoiceInfo();
                    _CurrentData.InvoiceDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    _CurrentData.ETD = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    _CurrentData.CreateByID = LocalData.UserInfo.LoginID;
                    _CurrentData.CreateByName = LocalData.UserInfo.LoginName;
                    _CurrentData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    _CurrentData.CompanyID = LocalData.UserInfo.DefaultCompanyID;
                    _CurrentData.CompanyName = LocalData.UserInfo.DefaultCompanyName;
                    bindingSource1.DataSource = _CurrentData;
                    bsBill.DataSource = typeof(List<CurrencyBillList>);
                    bsInvoiceFeeDate.DataSource = typeof(List<InvoiceFeeDate>);
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }



        }
        #endregion

        #region 删除费用
        /// <summary>
        /// 删除选中的费用
        /// </summary>
        private void RemoveFee()
        {
            if (CurrentInvoiceFee == null || gvChargeList.FocusedRowHandle < 0) return;
            if (!FAMUtility.ShowResultMessage(NativeLanguageService.GetText(this, "1110170001")))
            {
                return;
            }

            int[] selectRowsHandle = gvChargeList.GetSelectedRows();

            if (selectRowsHandle == null || selectRowsHandle.Length == 0) return;

            #region 未选中的记录是否包含账单号

            //bool flag = false;
            //for (int i = 0; i < gvChargeList.RowCount; i++)
            //{
            //    if (selectRowsHandle.Concat(i))
            //        continue;
            //    if (gvChargeList.GetRowCellValue(i, "colRemark"))
            //    {
            //        flag = true;
            //        break;
            //    }
            //}
            //if (flag == false)
            //{
            //    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), "至少要保留");
            //    return;
            //}

            #endregion

            for (int i = 0; i < selectRowsHandle.Length; i++)
            {
                int row = selectRowsHandle[i] - i;
                gvChargeList.DeleteRow(row);
                if (gvChargeList.RowCount < 1)
                {
                    bsBill.Clear();
                    return;
                }
                Guid billFeeId = CurrentInvoiceFee.BillFeeId.Value;

                //更新关联的已选择的帐单
                List<BillList> billList = CurrentBillSource;
                foreach (var item in billList)
                {
                    foreach (var feeItem in item.Fees)
                    {
                        if (feeItem.ID == billFeeId) feeItem.Selected = false;
                    }
                }
                bsBill.DataSource = billList;
                bsBill.ResetBindings(false);
            }
            TotalCurrenAmount();
            _CurrentData.IsDirty = true;
        }

        /// <summary>
        /// 清空所有的费用
        /// </summary>
        private void ClearFee()
        {
            if (CurrentInvoiceFee == null) return;
            if (!FAMUtility.ShowResultMessage(NativeLanguageService.GetText(this, "1110170002")))
            {
                return;
            }
            //更新关联的已选择的帐单
            List<BillList> billList = CurrentBillSource;
            foreach (var item in billList)
            {
                foreach (var feeItem in item.Fees)
                {
                    feeItem.Selected = false;
                }
            }
            bsBill.DataSource = billList;
            bsBill.ResetBindings(false);
            TotalCurrenAmount();
            bsInvoiceFeeDate.Clear();
        }
        #endregion

        #endregion

        #region IEditPart 成员
        private string OldInvoiceNo = string.Empty;
        #region 数据绑定
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

        private void BindingData(object data)
        {
            try
            {
                if (data == null)
                {
                    _CurrentData = new InvoiceInfo();
                    _CurrentData.InvoiceDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    _CurrentData.ExpressDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    _CurrentData.ETD = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    _CurrentData.CreateByID = LocalData.UserInfo.LoginID;
                    _CurrentData.CreateByName = LocalData.UserInfo.LoginName;
                    _CurrentData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    _CurrentData.CompanyID = LocalData.UserInfo.DefaultCompanyID;
                    _CurrentData.CompanyName = LocalData.UserInfo.DefaultCompanyName;
                    _CurrentData.IsValid = true;

                    if (TasSystemCommon.GetOrdinaryInvoiceCompanyIDList.Contains(_CurrentData.CompanyID))
                    {
                        _CurrentData.InvoiceType = CustomerInvoiceType.Ordinary;
                    }

                    if (ClientConfig.Current.Contains(InvoiceCommandConstants.InvoiceReceivablesName))
                    {
                        _CurrentData.ReceivablesName = ClientConfig.Current.GetValue(InvoiceCommandConstants.InvoiceReceivablesName);
                    }
                    if (ClientConfig.Current.Contains(InvoiceCommandConstants.InvoiceReviewName))
                    {
                        _CurrentData.ReviewName = ClientConfig.Current.GetValue(InvoiceCommandConstants.InvoiceReviewName);
                    }

                    bindingSource1.DataSource = _CurrentData;
                    bsBill.DataSource = typeof(List<CurrencyBillList>);
                    bsInvoiceFeeDate.DataSource = typeof(List<InvoiceFeeDate>);
                }
                else
                {
                    if (data is InvoiceInfo)
                    {
                        txtCustomer.Enabled = false;
                        chkIsValid.Properties.ReadOnly = true;
                        _CurrentData = data as InvoiceInfo;
                        bindingSource1.DataSource = _CurrentData;
                        cmbCtnType.Text = _CurrentData.CtnTypeName;//单独赋值
                        txtInvoiceNo.Properties.ReadOnly = false;

                        BindCustomerTitleName(_CurrentData.CustomerID.ToGuid());

                        if (_CurrentData.BillList == null) _CurrentData.BillList = new List<BillList>();

                        _existSelectBill = new List<Guid>();
                        foreach (var item in _CurrentData.BillList)
                        {
                            _existSelectBill.Add(item.ID);
                        }

                        if (_CurrentData.Fees == null) _CurrentData.Fees = new List<InvoiceFeeDate>();

                        bsBill.DataSource = _CurrentData.BillList;
                        bsInvoiceFeeDate.DataSource = _CurrentData.Fees;
                        bsBill.ResetBindings(false);
                        bsInvoiceFeeDate.ResetBindings(false);
                        Enabled = true;
                        ((BaseDataObject)data).CancelEdit();
                        ((BaseDataObject)data).BeginEdit();
                    }
                    else if (data is InvoiceList)
                    {
                        cmbCompany.Enabled = false;
                        txtCustomer.Enabled = false;
                        chkIsValid.Enabled = false;
                        _CurrentData = FinanceService.GetInvoiceInfo(((InvoiceList)data).ID, LocalData.IsEnglish);
                        bindingSource1.DataSource = _CurrentData;
                        cmbCtnType.Text = _CurrentData.CtnTypeName;
                        if (_CurrentData.BillList == null) _CurrentData.BillList = new List<BillList>();
                        BindCustomerTitleName(_CurrentData.CustomerID.ToGuid());
                        _existSelectBill = new List<Guid>();
                        foreach (var item in _CurrentData.BillList)
                        {
                            _existSelectBill.Add(item.ID);
                        }

                        if (_CurrentData.Fees == null) _CurrentData.Fees = new List<InvoiceFeeDate>();

                        bsBill.DataSource = _CurrentData.BillList;
                        bsInvoiceFeeDate.DataSource = _CurrentData.Fees;
                        bsBill.ResetBindings(false);
                        bsInvoiceFeeDate.ResetBindings(false);
                        Enabled = true;
                        ((BaseDataObject)data).CancelEdit();
                        ((BaseDataObject)data).BeginEdit();
                    }

                }
                OldInvoiceNo = _CurrentData.InvoiceNo;
                IsLoadData = true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }


        }

        #endregion

        public override void EndEdit()
        {
            Validate();
            bsBill.EndEdit();
            bsInvoiceFeeDate.EndEdit();
            bindingSource1.EndEdit();
        }

        public override event SavedHandler Saved;

        #endregion

        #region Gv Event
        Dictionary<Guid, string> dicCurrency = new Dictionary<Guid, string>();
        const int newRowHandle = int.MinValue + 1;//DEV里的新行常量

        #region 统计币种与金额&费用金额统计
        /// <summary>
        /// 费用项目金额统计
        /// </summary>
        private void TotalCurrenAmount()
        {

            txtFeeTotal.Text = string.Empty;
            Dictionary<Guid, Decimal> dicTotal = (from d in Details group d by d.CurrencyID into g select new { g.Key, TotalAmount = g.Sum(p => p.Amount) }).ToDictionary(c => c.Key, c => c.TotalAmount);

            if (dicTotal == null || dicTotal.Count == 0)
            {
                txtFeeTotal.Text = string.Empty;
                return;
            }
            foreach (var item in dicTotal)
            {
                string currencyName = string.Empty;
                if (_dicCurrency.Keys.Contains(item.Key))
                {
                    currencyName = _dicCurrency[item.Key];
                }
                else
                {
                    currencyName = LocalData.IsEnglish ? "Unknown" : "未知";
                }
                if (currencyName == "RMB")
                    txtFeeTotal.Text += currencyName + ":" + item.Value.ToString("n") + " ";
                else
                    txtFeeTotal.Text += currencyName + ":" + item.Value.ToString("n") + "\t" + "RMB:" + decimal.Round(item.Value * CurrentRow.Rate, 2).ToString("n");
            }

        }
        /// <summary>
        /// 账单金额统计
        /// </summary>
        private void TotalCurrenAmountBill()
        {

            List<BillList> bills = bsBill.DataSource as List<BillList>;
            if (bills == null)
            {
                return;
            }
            List<ChargeList> fees = new List<ChargeList>();
            foreach (var item in bills)
            {
                foreach (var fee in item.Fees)
                {
                    fees.Add(fee);
                }
            }

            Dictionary<string, decimal> dicAmount = (from d in fees group d by d.CurrencyName into g select new { g.Key, TotalAmount = g.Sum(d => d.Amount) }).ToDictionary(c => c.Key, c => c.TotalAmount);
            txtBillTotal.Text = string.Empty;

            if (dicAmount == null || dicAmount.Count == 0)
            {
                txtBillTotal.Text = string.Empty;
                return;
            }

            foreach (KeyValuePair<string, Decimal> item in dicAmount)
            {
                txtBillTotal.Text += item.Key + ": " + item.Value.ToString("n") + "  ";
            }

        }
        #endregion
        private void bsInvoiceFeeDate_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentInvoiceFee == null)
                barClearFee.Enabled = barRemoveFee.Enabled = false;
            else
                barClearFee.Enabled = barRemoveFee.Enabled = true;
        }

        private void bsBill_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentBill == null)
                btnDeleteBill.Enabled = false;
            else
                btnDeleteBill.Enabled = true;
        }

        #endregion

        #region GV Events
        //private void gvFee_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        //{

        //}
        //private void gvFee_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        //{

        //}
        #region 构建合计字符串
        protected void total()
        {

            Dictionary<Guid, decimal> dic = new Dictionary<Guid, decimal>();
            StringBuilder strbulider = new StringBuilder();
            foreach (var item in dic)
            {
                if (strbulider.Length > 0) strbulider.Append(" ");

                strbulider.Append(RateHelper.GetCurrencyNameByCurrencyID(item.Key));
                strbulider.Append(":" + item.Value.ToString("F2"));
            }
            txtFeeTotal.Text = strbulider.ToString();
        }
        #endregion

        private static int index = 0;
        bool[] boolSelect = new bool[index];
        Dictionary<int, bool> select = new Dictionary<int, bool>();
        private void gvFee_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            int indexCount = gcBill.Views[1].RowCount;
            boolSelect = new bool[indexCount];
            if (e.Column.Name == colBillFeeFeeSelected.Name)
            {
                //ChargeList fee = this.gcBill.Views[1].GetRow(e.RowHandle) as ChargeList;
                ChargeList fee = gcBill.Views[1].GridControl.FocusedView.GetRow(e.RowHandle) as ChargeList;
                if (fee == null) return;

                fee.Selected = !fee.Selected;

                gvFee.CloseEditor();

                gcBill.Views[1].CloseEditor();
                bsBill.EndEdit();
                bsBill.ResetCurrentItem();
                gcBill.Views[1].GridControl.FocusedView.RefreshData();
                gvFee.RefreshRow(e.RowHandle);
                gcBill.Views[1].RefreshData();


                List<InvoiceFeeDate> invoiceFeeSource = CurrentInvoiceFeeSource;
                if (fee.Selected)
                {
                    invoiceFeeSource.Add(CopyChargeBillToInvoiceFeeDate(fee));//添加一个副本
                    bsBill.ResetCurrentItem();
                }
                else
                {
                    InvoiceFeeDate needRemove = invoiceFeeSource.Find(delegate(InvoiceFeeDate item) { return item.BillFeeId == fee.ID; });
                    if (needRemove != null) invoiceFeeSource.Remove(needRemove);
                    bsBill.ResetCurrentItem();
                }
                bsInvoiceFeeDate.DataSource = invoiceFeeSource;

                TotalCurrenAmount();
                bsInvoiceFeeDate.ResetBindings(false);
            }
        }


        private void gvChargeList_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            if (e.Value == null) return;

            if (e.Column == colCurrencyName)   // 币种
            {

                try
                {
                    DateTime time;
                    List<SolutionExchangeRateList> rate;
                    if (string.IsNullOrEmpty(_CurrentData.InvoiceDate.ToString()))
                    {
                        time = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    }
                    else
                    {
                        time = _CurrentData.InvoiceDate;
                    }

                    //获取汇率列表
                    rate = FinanceService.GetInvoiceExchangeRateList(true);

                    Guid SourceCurrencyId = new Guid(e.Value.ToString());
                    Guid TargetCurrencyId = new Guid("DEB5F402-B6C0-4491-B247-B75C3EDA7976");//RMB为目标币种
                    decimal rateValue = decimal.Round(RateHelper.GetRate(SourceCurrencyId, TargetCurrencyId, time, rate), 4);
                    if (Details.Count > 1)
                    {
                        foreach (var item in Details)
                        {
                            item.CurrencyID = SourceCurrencyId;
                            item.Rate = rateValue;
                        }

                        bsInvoiceFeeDate.ResetBindings(false);
                    }
                    else
                    {
                        CurrentInvoiceFee.CurrencyID = SourceCurrencyId;
                        CurrentInvoiceFee.Rate = rateValue;
                        bsInvoiceFeeDate.ResetCurrentItem();
                    }

                    _isDirty = true;
                }
                catch (Exception ex)
                {
                    string message = LocalData.IsEnglish ? "Business-owned company does not have to configure the currency exchange rate" : "业务所属公司没有配置该币种汇率";
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), message);
                    return;
                }
                TotalCurrenAmount();
            }
        }

        private void gvChargeList_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Value == null) return;

            if (e.Column == colRate)  //汇率
            {
                if (Details.Count > 1)
                {
                    //decimal rateTemp = CurrentInvoiceFee.Rate;
                    decimal rateTemp = Decimal.Parse(e.Value.ToString());
                    foreach (var item in Details)
                    {
                        if (item.CurrencyID == CurrentInvoiceFee.CurrencyID)
                        {
                            item.Rate = rateTemp;
                        }
                    }

                    bsInvoiceFeeDate.ResetBindings(false);
                }
            }
            //else if (e.Column == this.colAmount)
            //{
            TotalCurrenAmount();
            //}

            _isDirty = true;
        }

        private void cmbBank1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _isDirty = true;
        }

        private void cmbBank2_SelectedIndexChanged(object sender, EventArgs e)
        {
            _isDirty = true;
        }

        private void gcBill_DataSourceChanged(object sender, EventArgs e)
        {
            _isDirty = true;
        }
        #endregion

        #region 税控系统

        #region API
        public delegate bool WNDENUMPROC(IntPtr hwnd, uint lParam);
        [DllImport("user32.dll", EntryPoint = "EnumWindows", SetLastError = true)]
        public static extern bool EnumWindows(WNDENUMPROC lpEnumFunc, uint lParam);

        [DllImport("user32.dll", EntryPoint = "GetParent", SetLastError = true)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "GetWindowThreadProcessId")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, ref uint lpdwProcessId);

        [DllImport("user32.dll", EntryPoint = "IsWindow")]
        public static extern bool IsWindow(IntPtr hWnd);

        [DllImport("kernel32.dll", EntryPoint = "SetLastError")]
        public static extern void SetLastError(uint dwErrCode);

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        [DllImport("user32.dll")]
        static extern byte MapVirtualKey(byte wCode, int wMap);

        [DllImport("user32.dll", SetLastError = true, EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false, EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, string lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false, EntryPoint = "FindWindowEx")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        //[DllImport("user32.dll")]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //static extern bool SetForegroundWindow(IntPtr hWnd);

        ///// <summary>
        ///// 得到窗体的线程
        ///// </summary>
        ///// <param name="hWnd"></param>
        ///// <param name="lpdwProcessId"></param>
        ///// <returns></returns>
        //[DllImport("user32.dll", SetLastError = true)]
        //public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        /// <summary>
        /// 得到窗体的线程ID
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="ProcessId"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);
        /// <summary>
        /// 得到当前线程的ID
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern uint GetCurrentThreadId();

        /// <summary>
        /// 得到窗体的
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        /// <summary>
        /// 附加线程
        /// </summary>
        /// <param name="idAttach"></param>
        /// <param name="idAttachTo"></param>
        /// <param name="fAttach"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

        /// <summary>
        /// 把窗口置顶
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool BringWindowToTop(IntPtr hWnd);

        /// <summary>
        /// 把窗口置顶
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool BringWindowToTop(HandleRef hWnd);

        /// <summary>
        /// 显示界面 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nCmdShow"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);

        [DllImport("user32.dll", EntryPoint = "SetFocus")]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        /// <summary>
        /// 判断窗体是否最大化
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern bool IsZoomed(IntPtr hWnd);

        public const uint SW_SHOW = 5;

        public static void AttachedThreadInputAction(Action action)
        {
            var foreThread = GetWindowThreadProcessId(GetForegroundWindow(),
                IntPtr.Zero);
            var appThread = GetCurrentThreadId();
            bool threadsAttached = false;

            try
            {
                threadsAttached =
                    foreThread == appThread ||
                    AttachThreadInput(foreThread, appThread, true);

                if (threadsAttached) action();
                else throw new ThreadStateException("AttachThreadInput failed.");
            }
            finally
            {
                if (threadsAttached)
                    AttachThreadInput(foreThread, appThread, false);
            }
        }

        /// <summary>
        /// 使窗口前置
        /// </summary>
        /// <param name="hwnd"></param>
        public static void ForceWindowToForeground(IntPtr hwnd)
        {
            AttachedThreadInputAction(
                () =>
                {
                    BringWindowToTop(hwnd);
                    ShowWindow(hwnd, SW_SHOW);
                });
        }
        /// <summary>
        /// 设置焦点放到指定的界面上
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        public static IntPtr SetFocusAttached(IntPtr hWnd)
        {
            var result = new IntPtr();
            AttachedThreadInputAction(
                () =>
                {
                    result = SetFocus(hWnd);
                });
            return result;
        }

        /// <summary>
        /// 使界面前置
        /// </summary>
        /// <param name="hWnd"></param>
        private static bool ForceForegroundWindow(IntPtr hWnd)
        {
            bool flag = false;
            uint foreThread = GetWindowThreadProcessId(GetForegroundWindow(),
                IntPtr.Zero);
            uint appThread = GetCurrentThreadId();
            const uint SW_SHOW = 5;

            if (foreThread != appThread)
            {
                AttachThreadInput(foreThread, appThread, true);
                flag = BringWindowToTop(hWnd);
                ShowWindow(hWnd, SW_SHOW);
                AttachThreadInput(foreThread, appThread, false);
            }
            else
            {
                flag = BringWindowToTop(hWnd);
                ShowWindow(hWnd, SW_SHOW);
            }
            return flag;
        }

        const int WM_LBUTTONDOWN = 0x0201;
        const int WM_LBUTTONUP = 0x0202;
        const int WM_KILLFOCUS = 0x0008;
        const int WM_SETFOCUS = 0x07;
        const int WM_SETTEXT = 0x000C;
        const int WM_KEYDOWN = 0x0100;
        //const int VK_TAB = 0x9;
        const int BM_CLICK = 0x00F5;
        const int CB_SETCURSEL = 0x014E;

        #endregion

        #region 上海公司的税控系统
        IntPtr kpSure = IntPtr.Zero;
        Thread OpenThread = null;
        Thread SetDataThread = null;
        private void SetSHTaxSystemData()
        {
            string formName = string.Empty;

            #region 1、拼装数据
            string customerName = cmbInvoiceTitleName.Text;
            string customerTaxNo = cmbCustomerTaxIDNo.Text;
            string customerTel = txtCustomerAddressTel.Text;
            string customerBank = cmbCustomerBankAccountNo.Text;
            string systemNo = txtNo.Text;
            string operationNos = string.Empty;
            string bankAccount = cmbBank1.Text;
            List<string> operationNoList = new List<string>();

            if (string.IsNullOrEmpty(customerName))
            {
                customerName = txtCustomer.Text;
            }
            #region  金额
            //含税金额
            decimal totalAmount = 0.0m;
            //不含税金额
            decimal totalTaxAmount = 0.0m;
            decimal usdAmount = 0.0m;
            decimal usdRate = 0.0m;
            decimal totalQty = (from d in CurrentInvoiceFeeSource select d.Quantity).Max();
            bool isUSD = false;

            if (CurrentInvoiceFeeSource.Count == 0)
            {
                return;
            }

            foreach (InvoiceFeeDate item in CurrentInvoiceFeeSource)
            {
                decimal rate = item.Rate;
                if (rate <= 0)
                {
                    rate = 1;
                }
                totalAmount = totalAmount + (rate * item.Amount);

                if (item.CurrencyID == new Guid("D67186CE-8B2C-4A75-81F1-A4FE3CC12DE9"))
                {
                    //美金
                    isUSD = true;
                    usdRate = item.Rate;
                    usdAmount += item.Amount;
                }
            }
            if (_CurrentData.InvoiceType == CustomerInvoiceType.Ordinary)
            {
                //普票
                totalTaxAmount = totalAmount;
            }
            else
            {
                totalTaxAmount = totalAmount / 1.06m;//得到纳税前的金额
            }
            #endregion


            foreach (BillList item in bill)
            {
                int i = (from d in item.Fees where d.Selected select d).Count();

                if (i > 0 && !operationNoList.Contains(item.No))
                {
                    operationNoList.Add(item.No);
                }
            }
            foreach (string str in operationNoList)
            {
                operationNos = operationNos + str.Substring(0, str.Length - 1) + ",";
            }
            operationNos = operationNos.Substring(0, operationNos.Length - 1);

            string strMeno = string.Empty;
            string blNo = _CurrentData.BLNo;
            if (string.IsNullOrEmpty(blNo) && !string.IsNullOrEmpty(_CurrentData.SONo))
            {
                blNo = _CurrentData.SONo;
            }
            if (!string.IsNullOrEmpty(operationNos))
            {
                if (operationNos.Length > 34)
                {
                    operationNos = operationNos.Substring(0, 33);
                }
                operationNos = operationNos.Replace(Environment.NewLine, "");
            }
            if (!string.IsNullOrEmpty(blNo))
            {
                if (blNo.Length > 60)
                {
                    blNo = blNo.Substring(0, 59);
                }
                blNo = blNo.Replace(Environment.NewLine, "");
            }

            if (isUSD)
            {
                strMeno = "此发票只接受美金付款" + Environment.NewLine + "USD" + usdAmount.ToString("F2") + Environment.NewLine + "USD:RMB=1:" + usdRate.ToString("F4") + Environment.NewLine + operationNos + Environment.NewLine + blNo;
            }
            else
            {
                strMeno = "RMB" + totalAmount.ToString("F2") + Environment.NewLine + operationNos + Environment.NewLine + blNo;
            }
            int bankAccountNoIndex = GetTaxBankAccountNo(bankAccount);

            SHTaxData data = new SHTaxData();
            data.Amount = totalTaxAmount;
            data.bankAccountNoIndex = bankAccountNoIndex;
            data.customerBank = customerBank;
            data.customerName = customerName;
            data.customerTaxNo = customerTaxNo;
            data.customerTel = customerTel;
            data.Remark = strMeno;
            data.systemNo = systemNo;

            #endregion

            #region 2、打开开发票界面
            /*  上海公司的开票界面，由于是showdialog的模式，只能开一个子线程去操作开票界面的数据
             *  但如果用子线程的话，会出现子线程的FindWindow\FindWindowEx都找不到控件,可能是子线程的原因，后面再继续改进
             *  现在的方案是用户自己打开开票的界面，然后税控系统再填入数据
             */


            //IntPtr mainFormWnd = FindWindow("TMainMultFormfrm", "税控开票系统");
            //IntPtr MTRzPanel0101 = FindWindowEx(mainFormWnd, IntPtr.Zero, "TRzPanel", null);
            //IntPtr MTRzPanel0201 = FindWindowEx(MTRzPanel0101, IntPtr.Zero, "TRzPanel", null);
            //IntPtr MTRzPanel0301 = FindWindowEx(MTRzPanel0201, IntPtr.Zero, "TRzPanel", null);
            //IntPtr MTRzPanel0302 = FindWindowEx(MTRzPanel0201, MTRzPanel0301, "TRzPanel", null);
            //IntPtr MTWizbar0401 = FindWindowEx(MTRzPanel0302, IntPtr.Zero, "TWizbar", null);
            //IntPtr MTRzPane050l = FindWindowEx(MTWizbar0401, IntPtr.Zero, "TRzPanel", null);
            //IntPtr MTrzMenuLabel0601 = FindWindowEx(MTRzPane050l, IntPtr.Zero, "TrzMenuLabel", null);

            if (_CurrentData.InvoiceType == CustomerInvoiceType.Ordinary)
            {
                //普票
                //kpSure = FindWindowEx(MTrzMenuLabel0601, IntPtr.Zero, "TRzPanel", "增值税普通发票填开");
                data.formName = "上海增值税普通发票开具";
                if (_CurrentData.CompanyID == new Guid("B1AFAD8F-55DD-4E29-A250-EB82AB3971FE"))
                {
                    data.formName = "大连增值税普通发票开具";
                }
            }
            else
            {
                //专票
                // IntPtr MTrzMenuLabel0602 = FindWindowEx(MTRzPane050l, MTrzMenuLabel0601, "TrzMenuLabel", null);
                // kpSure = FindWindowEx(MTrzMenuLabel0602, IntPtr.Zero, "TRzPanel", "增值税专用发票填开");
                data.formName = "上海增值税专用发票开具";
                if (_CurrentData.CompanyID == new Guid("B1AFAD8F-55DD-4E29-A250-EB82AB3971FE"))
                {
                    data.formName = "大连增值税专用发票开具";
                }
            }
            SetSHKPData(data);
            //if (kpSure == IntPtr.Zero)
            //{
            //    DevExpress.XtraEditors.XtraMessageBox.Show("没有找到开票的按钮");
            //    return;
            //}
            //OpenThread = new Thread(SureSHKP);
            //OpenThread.Start();
            //SendMessage(kpSure, WM_LBUTTONDOWN, IntPtr.Zero, IntPtr.Zero);
            //SendMessage(kpSure, WM_LBUTTONUP, IntPtr.Zero, IntPtr.Zero);

            //SetDataThread = new Thread(SetSHKPData);
            //SetDataThread.Start(data);

            #endregion
        }
        /// <summary>
        /// 确认开票
        /// </summary>
        private void SureSHKP()
        {
            Thread.Sleep(1000);
            IntPtr openFormWnd = FindWindow("TCustomBaseForm", "发票确认");
            ForceForegroundWindow(openFormWnd);

            KeyInput.SendKey(KeyboardConstaint.VK_NUMPAD1);
            keybd_event(13, MapVirtualKey(13, 0), 0, 0); //按下enter鍵。
        }
        private void SetSHKPData(object data)
        {
            SHTaxData item = data as SHTaxData;
            if (item == null)
            {
                return;
            }
            if (!SHIsOperTaxSystem(item.formName))
            {
                return;
            }
            #region 传输数据

            #region 主表
            IntPtr ParenthWnd = FindWindow("TCustomBaseForm", item.formName);

            SwitchToThisWindow(ParenthWnd, true);
            ForceForegroundWindow(ParenthWnd);

            IntPtr DTRzPanel0101 = FindWindowEx(ParenthWnd, IntPtr.Zero, "TRzPanel", null);
            IntPtr DTRzPanel0201 = FindWindowEx(DTRzPanel0101, IntPtr.Zero, "TRzPanel", null);
            IntPtr DTRzPanel0301 = FindWindowEx(DTRzPanel0201, IntPtr.Zero, "TRzPanel", null);
            IntPtr DTRzPanel0302 = FindWindowEx(DTRzPanel0201, DTRzPanel0301, "TRzPanel", null);
            IntPtr DTRzPanel0303 = FindWindowEx(DTRzPanel0201, DTRzPanel0302, "TRzPanel", null);
            IntPtr DTRzPanel0304 = FindWindowEx(DTRzPanel0201, DTRzPanel0303, "TRzPanel", null);
            IntPtr DTRzPanel0401 = FindWindowEx(DTRzPanel0304, IntPtr.Zero, "TfrmFpkj_Zzsfp", "增值税普通发票填开");
            IntPtr DTRzPanel0501 = FindWindowEx(DTRzPanel0401, IntPtr.Zero, "TPanel", null);
            IntPtr DTRzPanel0502 = FindWindowEx(DTRzPanel0401, DTRzPanel0501, "TPanel", null);
            IntPtr DTRzPanel0601 = FindWindowEx(DTRzPanel0502, IntPtr.Zero, "TPanel", null);
            IntPtr DTRzPanel0602 = FindWindowEx(DTRzPanel0502, DTRzPanel0601, "TPanel", null);
            IntPtr DTRzPanel0603 = FindWindowEx(DTRzPanel0502, IntPtr.Zero, "TGroupBox", null);
            IntPtr DTRzPanel0701 = FindWindowEx(DTRzPanel0603, IntPtr.Zero, "TRzButtonEdit", null);

            //纳税人识别号
            SendMessage(DTRzPanel0701, WM_SETFOCUS, IntPtr.Zero, IntPtr.Zero);
            SendMessage(DTRzPanel0701, WM_SETTEXT, IntPtr.Zero, item.customerTaxNo);

            //客户名称
            IntPtr DTRzPanel0702 = FindWindowEx(DTRzPanel0603, DTRzPanel0701, "TRzButtonEdit", null);
            SendMessage(DTRzPanel0701, WM_KILLFOCUS, IntPtr.Zero, IntPtr.Zero);
            SendMessage(DTRzPanel0702, WM_SETFOCUS, IntPtr.Zero, IntPtr.Zero);
            SendMessage(DTRzPanel0702, WM_SETTEXT, IntPtr.Zero, item.customerName);

            //地址电话
            IntPtr DTRzPanel0703 = FindWindowEx(DTRzPanel0603, IntPtr.Zero, "TComboBox", null);
            SendMessage(DTRzPanel0702, WM_KILLFOCUS, IntPtr.Zero, IntPtr.Zero);
            SendMessage(DTRzPanel0703, WM_SETFOCUS, IntPtr.Zero, IntPtr.Zero);
            SendMessage(DTRzPanel0703, WM_SETTEXT, IntPtr.Zero, item.customerTel);

            //银行账号
            IntPtr DTRzPanel0704 = FindWindowEx(DTRzPanel0603, DTRzPanel0703, "TComboBox", null);
            SendMessage(DTRzPanel0703, WM_KILLFOCUS, IntPtr.Zero, IntPtr.Zero);
            SendMessage(DTRzPanel0704, WM_SETFOCUS, IntPtr.Zero, IntPtr.Zero);
            SendMessage(DTRzPanel0704, WM_SETTEXT, IntPtr.Zero, item.customerBank);

            //备注
            IntPtr DTRzPanel0605 = FindWindowEx(DTRzPanel0601, IntPtr.Zero, "TMemo", null);
            SendMessage(DTRzPanel0704, WM_KILLFOCUS, IntPtr.Zero, IntPtr.Zero);
            SendMessage(DTRzPanel0605, WM_SETFOCUS, IntPtr.Zero, IntPtr.Zero);
            SendMessage(DTRzPanel0605, WM_SETTEXT, IntPtr.Zero, item.Remark);

            //SendKey("{0}", 20);
            //SendKey("{BACKSPACE}", 20);

            //让银行获得焦点
            SendMessage(DTRzPanel0605, WM_KILLFOCUS, IntPtr.Zero, IntPtr.Zero);
            SendMessage(DTRzPanel0704, WM_SETFOCUS, IntPtr.Zero, IntPtr.Zero);


            ////发送Tab
            KeyInput.SendKey(KeyboardConstaint.VK_TAB);
            #endregion
            Thread.Sleep(1000);
            ButtonCike(422, 423);
            #region 明细
            if (_CurrentData.InvoiceType == CustomerInvoiceType.Ordinary)
            {

                if (_CurrentData.CompanyID == new Guid("B1AFAD8F-55DD-4E29-A250-EB82AB3971FE"))
                {
                    if (item.Remark.IndexOf("此发票只接受美金付款") >= 0)
                    {
                        SetKeyCombination("代理海运费");
                    }
                    else
                    {
                        SetKeyCombination("代理港杂费");
                    }
                }
                //else
                //    SetKeyCombination(" 国际代理运费");
            }
            else
            {
                if (_CurrentData.CompanyID == new Guid("B1AFAD8F-55DD-4E29-A250-EB82AB3971FE"))
                {
                    if (item.Remark.IndexOf("此发票只接受美金付款") >= 0)
                    {
                        SetKeyCombination("代垫海运费");
                    }
                    else
                    {
                        SetKeyCombination("代垫港杂费");
                    }
                }
                //else
                //    SetKeyCombination("国际代理运费");
            }
            Thread.Sleep(200);
            SendKeys.SendWait("{TAB}");
            Thread.Sleep(200);
            ////输入货物代码后，按下回车
            //KeyInput.SendKey(KeyboardConstaint.VK_RETURN);

            ////按下车后，光标位置到数量的位置了,向左移动两位
            //KeyInput.SendKey(KeyboardConstaint.VK_LEFT);
            //KeyInput.SendKey(KeyboardConstaint.VK_LEFT);

            ButtonCike(555, 420);
            ////将光标回到规格型号的位置上的，输入规格型号
            #region 规格型号
            Thread.Sleep(200);
            SendKeys.SendWait(item.systemNo);
            Thread.Sleep(100);
            //foreach (short typeNo in Encoding.Default.GetBytes(item.systemNo))
            //{
            //    if (typeNo >= 97 && typeNo <= 122)  //a~z
            //    {
            //        if (isCapital == 0)
            //            KeyInput.SendKey((short)(Convert.ToInt16(typeNo) - 32));
            //        else
            //        {
            //            KeyInput.keybd_event(16, KeyInput.MapVirtualKey(16, 0), 0, 0); //按下Shift鍵。　　 
            //            KeyInput.SendKey((short)(Convert.ToInt16(typeNo) - 32));
            //            KeyInput.keybd_event(16, KeyInput.MapVirtualKey(16, 0), 0x2, 0);//松开Shift鍵。   
            //        }
            //    }
            //    else if (typeNo >= 65 && typeNo <= 90)  //A~Z
            //    {
            //        if (isCapital == 0)
            //        {
            //            KeyInput.keybd_event(16, KeyInput.MapVirtualKey(16, 0), 0, 0); //按下Shift鍵。　　 
            //            KeyInput.SendKey(typeNo);
            //            KeyInput.keybd_event(16, KeyInput.MapVirtualKey(16, 0), 0x2, 0);//松开Shift鍵。   
            //        }
            //        else
            //            KeyInput.SendKey(typeNo);
            //    }
            //    else
            //        KeyInput.SendKey(typeNo);
            //}
            #endregion
            ButtonCike(632, 420);
            Thread.Sleep(100);
            //将光标移动到单位,再移到数据
            //KeyInput.SendKey(KeyboardConstaint.VK_TAB);
            SetKeyCombination("票");
            Thread.Sleep(100);
            //KeyInput.SendKey(KeyboardConstaint.VK_TAB);
            ButtonCike(702, 420);
            //数量
            Thread.Sleep(100);
            SendKeys.SendWait("1");
            Thread.Sleep(100);
            //KeyInput.SendKey(KeyboardConstaint.VK_TAB);
            ButtonCike(798, 420);
            Thread.Sleep(100);
            SendKeys.SendWait(item.Amount.ToString("F7"));
            SendKeys.SendWait("{TAB}");
            //价格
            //foreach (short taxAmount in Encoding.Default.GetBytes(item.Amount.ToString("F7")))
            //{
            //    if (taxAmount == 46)  //小数点
            //        KeyInput.SendKey(KeyboardConstaint.VK_DECIMAL);
            //    else
            //        KeyInput.SendKey(taxAmount);
            //}
            //KeyInput.SendKey(KeyboardConstaint.VK_TAB);
            //KeyInput.SendKey(KeyboardConstaint.VK_TAB);
            ////税率
            //if (_CurrentData.InvoiceType == CustomerInvoiceType.Ordinary)
            //{
            //    KeyInput.SendKey(KeyboardConstaint.VK_NUMPAD0);
            //}
            //else
            //{
            //    foreach (short taxAmount in Encoding.Default.GetBytes("0.06"))
            //    {
            //        if (taxAmount == 46)  //小数点
            //            KeyInput.SendKey(KeyboardConstaint.VK_DECIMAL);
            //        else
            //            KeyInput.SendKey(taxAmount);
            //    }
            //}


            //公司银行的还未进行设置,后面再调整
            #endregion

            #endregion

            //SetDataThread.Abort();

        }

        #endregion

        #region  税控系统(宁波公司衍生)
        private void SetNBTaxSystemData()
        {
            string formName = string.Empty;

            #region 1、拼装数据
            string customerName = cmbInvoiceTitleName.Text;
            string customerTaxNo = cmbCustomerTaxIDNo.Text;
            string customerTel = txtCustomerAddressTel.Text;
            string customerBank = cmbCustomerBankAccountNo.Text;
            string systemNo = txtNo.Text;
            string operationNos = string.Empty;
            string bankAccount = cmbBank1.Text;
            List<string> operationNoList = new List<string>();

            if (string.IsNullOrEmpty(customerName))
            {
                customerName = txtCustomer.Text;
            }
            #region  金额
            //含税金额
            decimal totalAmount = 0.0m;
            //不含税金额
            decimal totalTaxAmount = 0.0m;
            decimal usdAmount = 0.0m;
            decimal usdRate = 0.0m;
            decimal totalQty = (from d in CurrentInvoiceFeeSource select d.Quantity).Max();
            bool isUSD = false;

            if (CurrentInvoiceFeeSource.Count == 0)
            {
                return;
            }

            foreach (InvoiceFeeDate item in CurrentInvoiceFeeSource)
            {
                decimal rate = item.Rate;
                if (rate <= 0)
                {
                    rate = 1;
                }
                totalAmount = totalAmount + (rate * item.Amount);

                if (item.CurrencyID == new Guid("D67186CE-8B2C-4A75-81F1-A4FE3CC12DE9"))
                {
                    //美金
                    isUSD = true;
                    usdRate = item.Rate;
                    usdAmount += item.Amount;
                }
            }
            if (_CurrentData.InvoiceType == CustomerInvoiceType.Dedicated)
            {

                totalTaxAmount = totalAmount / 1.06m;//得到纳税前的金额
            }
            else
            {
                //普票
                totalTaxAmount = totalAmount;
            }
            #endregion


            foreach (BillList item in bill)
            {
                int i = (from d in item.Fees where d.Selected select d).Count();

                if (i > 0 && !operationNoList.Contains(item.No))
                {
                    operationNoList.Add(item.No);
                }
            }

            if (operationNoList.Count > 0)
            {
                foreach (string str in operationNoList)
                {
                    operationNos = operationNos + str.Substring(0, str.Length - 1) + ",";
                }
                operationNos = operationNos.Substring(0, operationNos.Length - 1);
            }

            string strMeno = string.Empty;
            string blNo = _CurrentData.BLNo;
            if (string.IsNullOrEmpty(blNo) && !string.IsNullOrEmpty(_CurrentData.SONo))
            {
                blNo = _CurrentData.SONo;
            }
            if (!string.IsNullOrEmpty(operationNos))
            {
                if (operationNos.Length > 34)
                {
                    operationNos = operationNos.Substring(0, 33);
                }
                operationNos = operationNos.Replace(Environment.NewLine, "");
            }
            if (!string.IsNullOrEmpty(blNo))
            {
                if (blNo.Length > 60)
                {
                    blNo = blNo.Substring(0, 59);
                }
                blNo = blNo.Replace(Environment.NewLine, "");
            }

            if (isUSD)
            {
                strMeno = "此发票只接受美金付款" + Environment.NewLine + "USD" + usdAmount.ToString("F2") + Environment.NewLine + "USD:RMB=1:" + usdRate.ToString("F4") + Environment.NewLine + operationNos + Environment.NewLine + blNo;
            }
            else
            {
                strMeno = "RMB" + totalAmount.ToString("F2") + Environment.NewLine + operationNos + Environment.NewLine + blNo + Environment.NewLine + txtVessel.Text + Environment.NewLine + txtVoyage.Text;
            }
            int bankAccountNoIndex = GetTaxBankAccountNo(bankAccount);

            NBTaxData data = new NBTaxData();
            data.Amount = totalTaxAmount;
            data.bankAccountNoIndex = bankAccountNoIndex;
            data.customerBank = customerBank;
            data.customerName = customerName;
            data.customerTaxNo = customerTaxNo;
            data.customerTel = customerTel;
            data.Remark = strMeno;
            data.systemNo = systemNo;
            data.isUSD = isUSD;
            #endregion

            #region 2、打开开发票界面
            SetNBKPData(data);
            #endregion
        }

        //<summary>
        //进程判断税控系统是否打开
        //</summary>
        //<returns></returns>
        public bool Processwhether()
        {
            bool flg = true;
            Process[] pro = Process.GetProcesses();
            for (int i = 0; i < pro.Length; i++)
            {
                if (pro[i].ProcessName == "explorer")
                {
                    DialogResult result = MessageBox.Show("请先登陆税控系统。", "提示", MessageBoxButtons.RetryCancel);
                    if (result == DialogResult.Retry)
                    {
                        Processwhether();
                    }
                    else
                    {
                        flg = false;
                    }
                }
            }
            return flg;
        }

        //<summary>
        //进程判断税控系统是否打开
        //</summary>
        //<returns></returns>
        public bool ProcesswhetherNB()
        {
            bool flg = true;
            Process[] pro = Process.GetProcesses();
            for (int i = 0; i < pro.Length; i++)
            {
                if (pro[i].ProcessName == "Aisino.Framework.Startup.exe")
                {
                    DialogResult result = MessageBox.Show("请先登陆税控系统。", "提示", MessageBoxButtons.RetryCancel);
                    if (result == DialogResult.Retry)
                    {
                        ProcesswhetherNB();
                    }
                    else
                    {
                        flg = false;
                    }
                }
            }
            return flg;
        }


        public enum MouseEventFlags
        {
            Move = 0x0001,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            Wheel = 0x0800,
            Absolute = 0x8000
        }

        const int MOUSEEVENTF_MOVE = 0x0001;      //移动鼠标 
        const int MOUSEEVENTF_LEFTDOWN = 0x0002; //模拟鼠标左键按下 
        const int MOUSEEVENTF_LEFTUP = 0x0004; //模拟鼠标左键抬起 
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008; //模拟鼠标右键按下 
        const int MOUSEEVENTF_RIGHTUP = 0x0010; //模拟鼠标右键抬起 
        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020; //模拟鼠标中键按下 
        const int MOUSEEVENTF_MIDDLEUP = 0x0040; //模拟鼠标中键抬起 
        const int MOUSEEVENTF_ABSOLUTE = 0x8000; //标示是否采用绝对坐标 

        [DllImport("user32.dll")]
        private static extern int SetCursorPos(int x, int y);
        //[DllImport("User32")]
        //public extern static void mouse_event(int dwFlags, int dx, int dy, int dwData, IntPtr dwExtraInfo);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SwitchToThisWindow(IntPtr hWnd, Boolean fAltTab);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int W, int H, uint uFlags);
        [DllImport("user32")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        /// <summary>
        /// 模拟按钮点击
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void ButtonCike(int x, int y)
        {
            SetCursorPos(x, y);//已屏幕为坐标点
            //SetCursorPos(this.Location.X + x, this.Location.Y + y);//已this窗体为坐标点
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, x, y, 0, 0);
        }
        /// <summary>
        /// 模拟鼠标双击
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void ButtonDoubleCike(int x, int y)
        {
            SetCursorPos(x, y);//已屏幕为坐标点
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, x, y, WM_LBUTTONDOWN, 0);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, x, y, WM_LBUTTONUP, 0);
        }

        public IntPtr GetCurrentWindowHandle(uint proid)
        {
            IntPtr ptrWnd = IntPtr.Zero;
            uint uiPid = proid;
            object objWnd = processWnd[uiPid];

            if (objWnd != null)
            {
                ptrWnd = (IntPtr)objWnd;
                if (ptrWnd != IntPtr.Zero && IsWindow(ptrWnd))  // 从缓存中获取句柄
                {
                    return ptrWnd;
                }
                else
                {
                    ptrWnd = IntPtr.Zero;
                }
            }

            bool bResult = EnumWindows(new WNDENUMPROC(EnumWindowsProc), uiPid);
            // 枚举窗口返回 false 并且没有错误号时表明获取成功
            if (!bResult && Marshal.GetLastWin32Error() == 0)
            {
                objWnd = processWnd[uiPid];
                if (objWnd != null)
                {
                    ptrWnd = (IntPtr)objWnd;
                }
            }

            return ptrWnd;
        }

        private static bool EnumWindowsProc(IntPtr hwnd, uint lParam)
        {
            uint uiPid = 0;

            if (GetParent(hwnd) == IntPtr.Zero)
            {
                GetWindowThreadProcessId(hwnd, ref uiPid);
                if (uiPid == lParam)    // 找到进程对应的主窗口句柄
                {
                    processWnd.Add(uiPid, hwnd);   // 把句柄缓存起来
                    SetLastError(0);    // 设置无错误
                    return false;   // 返回 false 以终止枚举窗口
                }
            }
            return true;
        }

        private void SetNBKPData(object data)
        {
            //Thread.Sleep(2000);
            //屏幕截图 截取发票号区域
            //DiscernScreenInvoiceNo(new Point(613, 597), new Size(74, 17),"ocrtest.bmp");
            //return;

            NBTaxData item = data as NBTaxData;
            if (item == null) { return; }
            #region 构建普通发票XML文件
            XmlDocument xmlDoc = new XmlDocument();
            //创建类型声明节点  
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDoc.AppendChild(node);
            //创建第一级节点
            XmlNode Kp = xmlDoc.CreateElement("Kp");
            xmlDoc.AppendChild(Kp);
            XmlNode Version = xmlDoc.CreateElement("Version");
            Version.InnerText = "2.0";
            Kp.AppendChild(Version);
            //创建Fpxx节点
            XmlNode Fpxx = xmlDoc.CreateNode(XmlNodeType.Element, "Fpxx", null);
            Kp.AppendChild(Fpxx);
            //Zsl节点
            XmlNode Zsl = xmlDoc.CreateNode(XmlNodeType.Element, "Zsl", null);
            Zsl.InnerText = "1";
            Fpxx.AppendChild(Zsl);
            //Fpsj节点
            XmlNode Fpsj = xmlDoc.CreateNode(XmlNodeType.Element, "Fpsj", null);
            Fpxx.AppendChild(Fpsj);
            //Fp节点
            XmlNode Fp = xmlDoc.CreateNode(XmlNodeType.Element, "Fp", null);
            Fpsj.AppendChild(Fp);
            //Djh节点
            XmlNode Djh = xmlDoc.CreateNode(XmlNodeType.Element, "Djh", null);
            Djh.InnerText = txtNo.EditValue.ToString().Trim();
            Fp.AppendChild(Djh);
            //Gfmc节点 (购方名称)
            XmlNode Gfmc = xmlDoc.CreateNode(XmlNodeType.Element, "Gfmc", null);
            Gfmc.InnerText = item.customerName.Trim();
            Fp.AppendChild(Gfmc);
            //Gfsh节点(购方税号)
            XmlNode gfsh = xmlDoc.CreateNode(XmlNodeType.Element, "Gfsh", null);
            gfsh.InnerText = cmbCustomerTaxIDNo.EditValue.ToString().Trim();
            Fp.AppendChild(gfsh);

            //Gfyhzh节点(购方银行账号)
            XmlNode gfyhzh = xmlDoc.CreateNode(XmlNodeType.Element, "Gfyhzh", null);
            gfyhzh.InnerText = cmbCustomerBankAccountNo.EditValue.ToString().Trim();
            Fp.AppendChild(gfyhzh);

            //Gfyhzh节点(销售方银行账号)
            XmlNode Xfyhzh = xmlDoc.CreateNode(XmlNodeType.Element, "Xfyhzh", null);
            Xfyhzh.InnerText = cmbBank1.Text.Trim();
            Fp.AppendChild(Xfyhzh);

            //Gfdzdh节点(购方地址电话)
            XmlNode gfdzdh = xmlDoc.CreateNode(XmlNodeType.Element, "Gfdzdh", null);
            gfdzdh.InnerText = txtCustomerAddressTel.EditValue.ToString().Trim();
            Fp.AppendChild(gfdzdh);

            //Bz节点(备注)
            XmlNode bz = xmlDoc.CreateNode(XmlNodeType.Element, "Bz", null);
            bz.InnerText = item.Remark.Trim();
            Fp.AppendChild(bz);

            //Fhr节点(复核人)
            XmlNode fhr = xmlDoc.CreateNode(XmlNodeType.Element, "Fhr", null);
            if (_CurrentData.CompanyID == new Guid("41D7D3FE-183A-41CD-A725-EB6F728541EC"))//深圳公司
            {
                if (string.IsNullOrEmpty(cmbReview.EditText.Trim()))
                    fhr.InnerText = "高建芳";
            }
            else if (_CurrentData.CompanyID == new Guid("FD69B51B-E71F-4040-8F4B-28447A003C93"))//广州公司
            {
                if (string.IsNullOrEmpty(cmbReview.EditText.Trim()))
                    fhr.InnerText = "张涛";
            }
            else if (_CurrentData.CompanyID == new Guid("D8D57403-D663-4A93-A927-144907B7963B"))//天津公司
            {
                if (string.IsNullOrEmpty(cmbReview.EditText.Trim()))
                    fhr.InnerText = "梁媛";
            }
            if (string.IsNullOrEmpty(fhr.InnerText))
                fhr.InnerText = cmbReview.EditText.Trim();


            Fp.AppendChild(fhr);

            //Skr节点(收款人)
            XmlNode skr = xmlDoc.CreateNode(XmlNodeType.Element, "Skr", null);
            if (_CurrentData.CompanyID == new Guid("41D7D3FE-183A-41CD-A725-EB6F728541EC"))//深圳公司
            {
                if (string.IsNullOrEmpty(cmbReceivables.EditText))
                    skr.InnerText = "付燕";
            }
            else if (_CurrentData.CompanyID == new Guid("FD69B51B-E71F-4040-8F4B-28447A003C93"))//广州公司
            {
                if (string.IsNullOrEmpty(cmbReceivables.EditText))
                    skr.InnerText = "宁旭波";
            }
            else if (_CurrentData.CompanyID == new Guid("D8D57403-D663-4A93-A927-144907B7963B"))//天津公司
            {
                if (string.IsNullOrEmpty(cmbReceivables.EditText))
                    skr.InnerText = "刘芳";
            }
            if (string.IsNullOrEmpty(skr.InnerText))
                skr.InnerText = cmbReceivables.EditText.Trim();
            Fp.AppendChild(skr);
            //商品编码版本号
            XmlNode Spbmbbh = xmlDoc.CreateElement("Spbmbbh");
            Spbmbbh.InnerText = "30.0";
            Fp.AppendChild(Spbmbbh);
            //含税标识
            XmlNode Hsbz = xmlDoc.CreateElement("Hsbz");
            Hsbz.InnerText = "0";
            Fp.AppendChild(Hsbz);
            //详细信息
            XmlNode spxx = xmlDoc.CreateNode(XmlNodeType.Element, "Spxx", null);
            Fp.AppendChild(spxx);
            XmlNode sph = xmlDoc.CreateNode(XmlNodeType.Element, "Sph", null);
            spxx.AppendChild(sph);
            //序号
            XmlNode Xh = xmlDoc.CreateNode(XmlNodeType.Element, "Xh", null);
            Xh.InnerText = "1";
            sph.AppendChild(Xh);
            //商品名称
            XmlNode spmc = xmlDoc.CreateNode(XmlNodeType.Element, "Spmc", null);
            if (item.isUSD)
            {
                if (_CurrentData.CompanyID == new Guid("2AFAC53A-2AF9-46ED-8C4B-7035FEDC0279"))//厦门公司
                {
                    spmc.InnerText = "国际货运代理服务费（出口海运费）";
                }
                else if (_CurrentData.CompanyID == new Guid("FD69B51B-E71F-4040-8F4B-28447A003C93"))//广州公司
                {
                    spmc.InnerText = "代理 运费";
                }
                else if (_CurrentData.CompanyID == new Guid("D8D57403-D663-4A93-A927-144907B7963B"))//天津公司
                {
                    if (_CurrentData.CustomerID == new Guid("3AC6EA40-B5B5-E511-BE13-0026551CA878") &
                        _CurrentData.Fees.Exists(o => o.ChargingCode.Contains("关税")))//秦皇岛中秦渤海轮毂有限公司
                    {
                        spmc.InnerText = "关税";
                    }
                    else
                        spmc.InnerText = "代理海运费";
                }
                else if (_CurrentData.CompanyID == new Guid("B13FAC2D-8250-4990-A622-5ECA00D3A030") ||
                    _CurrentData.CompanyID == new Guid("41D7D3FE-183A-41CD-A725-EB6F728541EC")) //上海公司/深圳公司
                {
                    spmc.InnerText = "国际代理运费";
                }
                else
                {
                    spmc.InnerText = "代理海运费";
                }
            }
            else
            {
                if (_CurrentData.CompanyID == new Guid("2AFAC53A-2AF9-46ED-8C4B-7035FEDC0279"))//厦门公司
                {
                    spmc.InnerText = "国际货运代理服务费";
                }
                else if (_CurrentData.CompanyID == new Guid("D8D57403-D663-4A93-A927-144907B7963B"))//天津公司
                {
                    if (_CurrentData.CustomerID == new Guid("BD8A436F-5823-E711-80BD-141877442141"))  //上海运百国际物流有限公司
                    { spmc.InnerText = "国际货物运输代理服务"; }

                    else
                        spmc.InnerText = "代理港杂费";
                }
                else if (_CurrentData.CompanyID == new Guid("FD69B51B-E71F-4040-8F4B-28447A003C93"))//广州公司
                {
                    spmc.InnerText = "代理 运费";
                }
                else if (_CurrentData.CompanyID == new Guid("B13FAC2D-8250-4990-A622-5ECA00D3A030") ||
                    _CurrentData.CompanyID == new Guid("41D7D3FE-183A-41CD-A725-EB6F728541EC"))//上海公司
                {
                    spmc.InnerText = "国际代理运费";
                }
                else
                {
                    spmc.InnerText = "代理运费";
                }
            }
            sph.AppendChild(spmc);
            //规格型号
            XmlNode ggxh = xmlDoc.CreateNode(XmlNodeType.Element, "Ggxh", null);
            ggxh.InnerText = "";
            sph.AppendChild(ggxh);
            //计量单位
            XmlNode jldw = xmlDoc.CreateNode(XmlNodeType.Element, "Jldw", null);
            jldw.InnerText = "票";
            sph.AppendChild(jldw);
            //商品编码
            XmlNode Spbm = xmlDoc.CreateElement("Spbm");
            Spbm.InnerText = "3040802010200000000";//税收分类编码
            sph.AppendChild(Spbm);
            //企业商品编码
            string strQYSPBM = "";
            if (_CurrentData.CompanyID == new Guid("41D7D3FE-183A-41CD-A725-EB6F728541EC"))//深圳公司
            {
                if (_CurrentData.InvoiceType == CustomerInvoiceType.Ordinary)
                    strQYSPBM = "20";
                else if (_CurrentData.InvoiceType == CustomerInvoiceType.Dedicated)
                    strQYSPBM = "21";
            }
            else if (_CurrentData.CompanyID == new Guid("2AFAC53A-2AF9-46ED-8C4B-7035FEDC0279"))//厦门公司
            {
                if (item.isUSD)
                    strQYSPBM = "009";
                else
                    strQYSPBM = "008";
            }
            XmlNode Qyspbm = xmlDoc.CreateElement("Qyspbm");
            Qyspbm.InnerText = strQYSPBM;
            sph.AppendChild(Qyspbm);
            //优惠政策标识
            string strBZ = "0";
            if (_CurrentData.InvoiceType == CustomerInvoiceType.Ordinary)
                strBZ = "1";
            XmlNode Syyhzcbz = xmlDoc.CreateElement("Syyhzcbz");
            Syyhzcbz.InnerText = strBZ;
            sph.AppendChild(Syyhzcbz);
            //零税率标识
            string strSL = "";
            if (_CurrentData.InvoiceType == CustomerInvoiceType.Ordinary)
                strSL = "1";
            XmlNode Lslbz = xmlDoc.CreateElement("Lslbz");
            Lslbz.InnerText = strSL;
            sph.AppendChild(Lslbz);
            //优惠政策说明
            XmlNode Yhzcsm = xmlDoc.CreateElement("Yhzcsm");
            Yhzcsm.InnerText = "";
            sph.AppendChild(Yhzcsm);
            //单价
            XmlNode dj = xmlDoc.CreateNode(XmlNodeType.Element, "Dj", null);
            dj.InnerText = item.Amount.ToString("N2");
            sph.AppendChild(dj);
            //数量
            XmlNode sl = xmlDoc.CreateNode(XmlNodeType.Element, "Sl", null);
            sl.InnerText = "1";
            sph.AppendChild(sl);
            //金额
            XmlNode je = xmlDoc.CreateNode(XmlNodeType.Element, "Je", null);
            je.InnerText = item.Amount.ToString("N2");
            sph.AppendChild(je);
            //税率
            XmlNode slv = xmlDoc.CreateNode(XmlNodeType.Element, "Slv", null);
            //if (_CurrentData.CompanyID == new Guid("B13FAC2D-8250-4990-A622-5ECA00D3A030") && cmbInvoiceType.Text == "专用发票")
            if (_CurrentData.InvoiceType == CustomerInvoiceType.Dedicated)
            {
                slv.InnerText = "0.06";
            }
            else
            {
                slv.InnerText = "0";
            }
            //扣除额
            XmlNode Kce = xmlDoc.CreateElement("Kce");
            Kce.InnerText = "";
            sph.AppendChild(Kce);
            sph.AppendChild(slv);
            string path = "c:\\data\\";
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);
            #endregion
            try
            {
                #region 打开开票软件
                //移除一天前的文件
                new FileHelper().DeleteOneDayPreFile(path);
                xmlDoc.Save(string.Format(path + "{0}.xml", item.systemNo));
                string kpVersion = "V2.2.34.190919";
                //ConfigureInfo confige = ConfigureService.GetConfigureInfoByCompanyID(_CurrentData.CompanyID);
                //if (confige != null)
                //    kpVersion = confige.TaxControlVersion;
                //string kpTitle = string.Format("增值税发票税控开票软件（金税盘版） {0}", kpVersion);
                //string kpmTitle = string.Format("增值税发票税控开票软件（金税盘版） {0} - [发票管理]", kpVersion);
                string kpTitle = string.Format("增值税发票税控开票软件（金税盘版）");
                string kpmTitle = string.Format("增值税发票税控开票软件（金税盘版） {0} - [发票管理]", kpVersion);
                IntPtr ParenthWnd = FindWindow(null, "开具增值税普通发票");
                var list = Process.GetProcesses().Where(x => x.ProcessName.Contains("Aisino.Framework.Startup")).Where(x => x.MainWindowTitle.Contains("开具增值税普通发票"));
                if (list.Count() == 0)//当前没打开的普通发票页面，则去打开
                {
                    string mainFormName;
                    IntPtr mainFormWnd = new IntPtr(0);
                    mainFormName = kpTitle;
                    mainFormWnd = new IntPtr(0);
                    mainFormWnd = FindWindow(null, mainFormName);
                    if (mainFormWnd == IntPtr.Zero)
                    {
                        //mainFormName = kpmTitle;
                        //mainFormWnd = FindWindow(null, mainFormName);
                        var InvoiceList = Process.GetProcesses().Where(x => x.ProcessName.Contains("Aisino.Framework.Startup"))
                            .Where(x => x.MainWindowTitle.Contains("增值税发票税控开票软件"));
                        mainFormWnd = InvoiceList.Count() > 0 ? InvoiceList.Single().MainWindowHandle : IntPtr.Zero;
                    }
                #region 开票
                if (ProcesswhetherNB())//是否打开税控系统
                {
                        string ocrPath = "OCR.bmp";//string.Empty;
                        float DefaultY = 900;
                        float DefaultX = 1600;
                        float SH = Screen.PrimaryScreen.Bounds.Height;
                        float SW = Screen.PrimaryScreen.Bounds.Width;
                        float a = SW / DefaultX;//X倍数
                        float b = SH / DefaultY;//Y倍数
                        if (ParenthWnd == IntPtr.Zero)//当前没打开的普通发票页面，则去打开
                        {
                            //使主界面置前
                            SwitchToThisWindow(mainFormWnd, true);
                            ForceForegroundWindow(mainFormWnd);
                            //点击开票
                            Thread.Sleep(1000);
                            ButtonCike((int)(827 * a), (int)(470 * b));
                            Thread.Sleep(1000);
                            if (_CurrentData.InvoiceType == CustomerInvoiceType.Dedicated)
                            {
                                SendKeys.SendWait("{DOWN}");
                                Thread.Sleep(500);
                                SendKeys.SendWait("{ENTER}");
                            }
                            else if (_CurrentData.InvoiceType == CustomerInvoiceType.Ordinary)
                            {
                                if (_CurrentData.CompanyID == new Guid("2AFAC53A-2AF9-46ED-8C4B-7035FEDC0279"))
                                {
                                    SendKeys.SendWait("{DOWN}");
                                    Thread.Sleep(500);
                                    SendKeys.SendWait("{ENTER}");
                                }
                                else if (_CurrentData.CompanyID == new Guid("FD69B51B-E71F-4040-8F4B-28447A003C93"))//广州公司
                                {
                                    SendKeys.SendWait("{DOWN}");
                                    Thread.Sleep(500);
                                    SendKeys.SendWait("{DOWN}");
                                    Thread.Sleep(500);
                                    SendKeys.SendWait("{DOWN}");
                                    Thread.Sleep(500);
                                    SendKeys.SendWait("{ENTER}");
                                }
                                else
                                {
                                    SendKeys.SendWait("{DOWN}");
                                    Thread.Sleep(500);
                                    SendKeys.SendWait("{DOWN}");
                                    Thread.Sleep(500);
                                    SendKeys.SendWait("{ENTER}");
                                }
                            }
                            else if (_CurrentData.InvoiceType == CustomerInvoiceType.Electronic)
                            {
                                if (_CurrentData.CompanyID == new Guid("2AFAC53A-2AF9-46ED-8C4B-7035FEDC0279"))
                                {
                                    SendKeys.SendWait("{DOWN}");
                                    Thread.Sleep(500);
                                    SendKeys.SendWait("{DOWN}");
                                    Thread.Sleep(500);
                                    SendKeys.SendWait("{ENTER}");
                                }
                                else if (_CurrentData.CompanyID == new Guid("FD69B51B-E71F-4040-8F4B-28447A003C93"))//广州公司
                                {
                                    SendKeys.SendWait("{DOWN}");
                                    Thread.Sleep(500);
                                    SendKeys.SendWait("{DOWN}");
                                    Thread.Sleep(500);
                                    SendKeys.SendWait("{DOWN}");
                                    Thread.Sleep(500);
                                    SendKeys.SendWait("{DOWN}");
                                    Thread.Sleep(500);
                                    SendKeys.SendWait("{ENTER}");
                                }
                                else
                                {
                                    SendKeys.SendWait("{DOWN}");
                                    Thread.Sleep(500);
                                    SendKeys.SendWait("{DOWN}");
                                    Thread.Sleep(500);
                                    SendKeys.SendWait("{DOWN}");
                                    Thread.Sleep(500);
                                    SendKeys.SendWait("{ENTER}");
                                }
                            }
                            Thread.Sleep(1000);
                            IntPtr InvoiceList = FindWindow(null, "发票卷选择");
                            ShowWindow(InvoiceList, 3);
                            Thread.Sleep(500);
                            //选择发票
                            ButtonCike((int)(1496 * a), (int)(46 * b));
                            //点击确定（查看发票页面）
                            Thread.Sleep(1000);
                            var invoiceConfirmWindow = FindWindowEx(IntPtr.Zero, "发票号码确认", null, true);
                            if (ShowWindow(invoiceConfirmWindow, 3))
                            {
                                ButtonCike((int)(293), (int)(276));
                                if (_CurrentData.InvoiceType == CustomerInvoiceType.Dedicated)
                                {
                                    ParenthWnd = FindWindow(null, "开具增值税专用发票");
                                }
                                else if (_CurrentData.InvoiceType == CustomerInvoiceType.Ordinary)
                                {
                                    Thread.Sleep(4000);
                                    ParenthWnd = FindWindowEx(IntPtr.Zero, "开具增值税普通发票", null, true);
                                }
                                else if (_CurrentData.InvoiceType == CustomerInvoiceType.Electronic)
                                {
                                    Thread.Sleep(4000);
                                    ParenthWnd = FindWindowEx(IntPtr.Zero, "开具增值税电子普通发票", null, true);
                                }
                            }
                        }
                        if (ShowWindow(ParenthWnd, 3))
                        {
                            Thread.Sleep(2000);
                            if (SwitchToThisWindow(ParenthWnd, true))
                            {
                                if (ForceForegroundWindow(ParenthWnd))
                                {
                                    InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
                                    Thread.Sleep(500);
                                    //屏幕截图 截取发票号区域
                                    DiscernScreenInvoiceNo(new Point((((int)SW - 948) / 2) + 670, (int)(83)), new Size((int)(150), (int)(80)));
                                    Thread.Sleep(500);
                                    #region 导入按钮
                                    if (SwitchToThisWindow(ParenthWnd, true))
                                    {
                                        if (ForceForegroundWindow(ParenthWnd))
                                        {
                                            if (_CurrentData.InvoiceType == CustomerInvoiceType.Ordinary || _CurrentData.InvoiceType == CustomerInvoiceType.Dedicated)
                                            {
                                                //点击导入按钮
                                                ButtonCike((int)(SW - 648 - 30), (int)(50));
                                                Thread.Sleep(500);
                                                //点击手工导入
                                                SendKeys.SendWait("{DOWN}");
                                                Thread.Sleep(500);
                                                SendKeys.SendWait("{DOWN}");
                                                Thread.Sleep(500);
                                                SendKeys.SendWait("{ENTER}");
                                                Thread.Sleep(1000);
                                                //设置发票号
                                                SendKeys.SendWait(string.Format(path + "{0}.xml", item.systemNo));
                                                Thread.Sleep(1000);
                                                SendKeys.SendWait("{ENTER}");
                                                //点击选择
                                                ShowWindow(FindWindowEx(IntPtr.Zero, "导入单据信息查询选择", null, true), 3);
                                                Thread.Sleep(1000);
                                                //点击选择
                                                ButtonCike((int)(1498 * a), (int)(49 * b));
                                                Thread.Sleep(1000);
                                            }
                                            else if (_CurrentData.InvoiceType == CustomerInvoiceType.Electronic)
                                            {
                                                var defalX = (((int)SW - 948) / 2);
                                                //发票抬头
                                                ButtonDoubleCike(defalX+313, 231);
                                                //ButtonCike(550, 222);
                                                Thread.Sleep(1000);
                                                if (SetKeyCombination(cmbInvoiceTitleName.Text))
                                                {
                                                    Thread.Sleep(1500);
                                                    SendKeys.SendWait("{DOWN}");
                                                    Thread.Sleep(500);
                                                    SendKeys.SendWait("{ENTER}");
                                                    Thread.Sleep(500);
                                                    //纳税人识别号
                                                    SendKeys.SendWait("{TAB}");
                                                    Thread.Sleep(500);
                                                    SendKeys.SendWait(cmbCustomerTaxIDNo.EditValue.ToString());
                                                    Thread.Sleep(500);
                                                    SendKeys.SendWait("{DOWN}");
                                                    Thread.Sleep(500);
                                                    SendKeys.SendWait("{ENTER}");
                                                    Thread.Sleep(500);
                                                    //电话地址
                                                    SendKeys.SendWait("{TAB}");
                                                    Thread.Sleep(500);
                                                    SendKeys.SendWait(txtCustomerAddressTel.EditValue.ToString().Trim());
                                                    //开户行及账号
                                                    SendKeys.SendWait("{TAB}");
                                                    Thread.Sleep(500);
                                                    SendKeys.SendWait(cmbCustomerBankAccountNo.EditValue.ToString().Trim());
                                                    Thread.Sleep(2000);
                                                    //货物或应税劳务、服务名称
                                                    ButtonCike(defalX + 123, 354);
                                                    Thread.Sleep(1000);
                                                    if (!item.isUSD)
                                                    {
                                                        switch (Convert.ToString(_CurrentData.CompanyID))
                                                        {
                                                            case "FD69B51B-E71F-4040-8F4B-28447A003C93":
                                                                SendKeys.SendWait("代理 运费");
                                                                break;
                                                            case "2AFAC53A-2AF9-46ED-8C4B-7035FEDC0279":
                                                                SendKeys.SendWait("国际货运代理");
                                                                break;
                                                            case "F289109A-C29E-4B0B-A41A-C22D9E70A72F":
                                                                SendKeys.SendWait("代理运费");
                                                                break;
                                                            case "D8D57403-D663-4A93-A927-144907B7963B":
                                                                SendKeys.SendWait("代理港杂费");
                                                                break;
                                                            case "41D7D3FE-183A-41CD-A725-EB6F728541EC":
                                                                SendKeys.SendWait("国际代理运费");
                                                                break;
                                                            case "1EA069FE-2E98-E711-80C1-141877442141":
                                                                SendKeys.SendWait("国际货运代理费");
                                                                break;
                                                            case "9A4725F3-E7CF-4AD3-91A4-988892AF2781":
                                                                SendKeys.SendWait("代理运费");
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        switch (Convert.ToString(_CurrentData.CompanyID))
                                                        {
                                                            case "FD69B51B-E71F-4040-8F4B-28447A003C93":
                                                                SendKeys.SendWait("代理 运费");
                                                                break;
                                                            case "2AFAC53A-2AF9-46ED-8C4B-7035FEDC0279":
                                                                SendKeys.SendWait("国际货运代理服务费");
                                                                break;
                                                            case "F289109A-C29E-4B0B-A41A-C22D9E70A72F":
                                                                SendKeys.SendWait("代理海运费");
                                                                break;
                                                            case "D8D57403-D663-4A93-A927-144907B7963B":
                                                                SendKeys.SendWait("代理海运费");
                                                                break;
                                                            case "41D7D3FE-183A-41CD-A725-EB6F728541EC":
                                                                SendKeys.SendWait("国际代理运费");
                                                                break;
                                                            case "1EA069FE-2E98-E711-80C1-141877442141":
                                                                SendKeys.SendWait("国际货运代理费");
                                                                break;
                                                            case "9A4725F3-E7CF-4AD3-91A4-988892AF2781":
                                                                SendKeys.SendWait("代理运费");
                                                                break;
                                                        }
                                                    }
                                                    Thread.Sleep(1000);
                                                    //规格型号
                                                    SendKeys.SendWait("{TAB}");
                                                    Thread.Sleep(500);
                                                    //单位
                                                    SendKeys.SendWait("{TAB}");
                                                    Thread.Sleep(500);
                                                    //数量
                                                    SendKeys.SendWait("{TAB}");
                                                    Thread.Sleep(500);
                                                    SendKeys.SendWait("1");
                                                    Thread.Sleep(500);
                                                    //单价（含税）
                                                    SendKeys.SendWait("{TAB}");
                                                    Thread.Sleep(500);
                                                    SendKeys.SendWait(item.Amount.ToString("f2"));
                                                    Thread.Sleep(500);
                                                    SendKeys.SendWait("{TAB}");
                                                    Thread.Sleep(500);
                                                    //金额（含税）
                                                    //SendKeys.SendWait("{TAB}");
                                                    //Thread.Sleep(200);
                                                    //税率
                                                    //SendKeys.SendWait("{TAB}");
                                                    //Thread.Sleep(200);
                                                    //税额
                                                    //SendKeys.SendWait("{TAB}");
                                                    //Thread.Sleep(200);
                                                    //备注

                                                    ButtonDoubleCike(defalX + 612, 588);
                                                    Thread.Sleep(1000);
                                                    SendKeys.SendWait(item.Remark.ToUpper().Replace("\n", ""));
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                        //}
                        //#endregion
                        //#region 武汉达吾公司
                        //else if (_CurrentData.CompanyID == new Guid("1EA069FE-2E98-E711-80C1-141877442141"))
                        //{
                        //    //IntPtr ParenthWnd = FindWindow(null, "开具增值税普通发票");
                        //    if (ParenthWnd == IntPtr.Zero)//当前没打开的普通发票页面，则去打开
                        //    {
                        //        string mainFormName;
                        //        IntPtr mainFormWnd = new IntPtr(0);
                        //        mainFormName = kpTitle;
                        //        mainFormWnd = new IntPtr(0);
                        //        mainFormWnd = FindWindow(null, mainFormName);
                        //        if (mainFormWnd == IntPtr.Zero)
                        //        {
                        //            mainFormName = kpmTitle;
                        //            mainFormWnd = FindWindow(null, mainFormName);
                        //        }


                        //        //使主界面置前
                        //        SwitchToThisWindow(mainFormWnd, true);
                        //        ForceForegroundWindow(mainFormWnd);
                        //        //点击开票
                        //        Thread.Sleep(1000);
                        //        //ButtonCike(ChangeScreenWidth(700), ChangeScreenHight(400));
                        //        ButtonCike(700, 400);
                        //        if (_CurrentData.InvoiceType == CustomerInvoiceType.Dedicated)
                        //        {
                        //            Thread.Sleep(1000);
                        //            SendKeys.SendWait("{DOWN}");
                        //        }
                        //        else if (_CurrentData.InvoiceType == CustomerInvoiceType.Ordinary)
                        //        {
                        //            //点击普通发票
                        //            Thread.Sleep(1000);
                        //            //ButtonCike(535, 410);
                        //            SendKeys.SendWait("{DOWN}");
                        //            Thread.Sleep(1000);
                        //            //System.Threading.Thread.Sleep(1000);
                        //            SendKeys.SendWait("{ENTER}");
                        //            Thread.Sleep(1000);
                        //            //点击确定（查看发票页面）
                        //            //SendKeys.SendWait("{TAB}");
                        //            //Thread.Sleep(500);
                        //            ButtonDoubleCike(ChangeScreenWidth(600), ChangeScreenHight(240));
                        //            Thread.Sleep(500);
                        //            SendKeys.SendWait("{ENTER}");
                        //            ParenthWnd = FindWindow(null, "开具增值税普通发票");
                        //        }
                        //        else
                        //        {
                        //            //电子发票
                        //            Thread.Sleep(1000);
                        //            SendKeys.SendWait("{DOWN}");
                        //            Thread.Sleep(1000);
                        //            SendKeys.SendWait("{DOWN}");
                        //            //System.Threading.Thread.Sleep(1000);
                        //            //SendKeys.SendWait("{DOWN}");
                        //            Thread.Sleep(1000);
                        //            SendKeys.SendWait("{ENTER}");
                        //            Thread.Sleep(1000);
                        //            SendKeys.SendWait("{ENTER}");
                        //        }

                        //    }
                        //    //转到普通发票页面
                        //    SwitchToThisWindow(ParenthWnd, true);
                        //    ForceForegroundWindow(ParenthWnd);
                        //    InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
                        //    Thread.Sleep(1500);
                        //    if (_CurrentData.InvoiceType == CustomerInvoiceType.Electronic)
                        //        ButtonCike(500, 240);
                        //    else
                        //        ButtonCike(500, 220);
                        //    Thread.Sleep(1000);
                        //    SetKeyCombination(cmbInvoiceTitleName.Text);
                        //    Thread.Sleep(1500);
                        //    SendKeys.SendWait("{DOWN}");
                        //    Thread.Sleep(500);
                        //    SendKeys.SendWait("{ENTER}");
                        //    Thread.Sleep(500);
                        //    SendKeys.SendWait("{TAB}");
                        //    Thread.Sleep(500);
                        //    SendKeys.SendWait(cmbCustomerTaxIDNo.EditValue.ToString());
                        //    Thread.Sleep(500);
                        //    SendKeys.SendWait("{DOWN}");
                        //    Thread.Sleep(500);
                        //    SendKeys.SendWait("{ENTER}");
                        //    Thread.Sleep(500);
                        //    SendKeys.SendWait("{TAB}");
                        //    Thread.Sleep(500);
                        //    SendKeys.SendWait("{DOWN}");
                        //    Thread.Sleep(500);
                        //    SendKeys.SendWait("{ENTER}");
                        //    Thread.Sleep(500);
                        //    SendKeys.SendWait("{TAB}");
                        //    Thread.Sleep(500);
                        //    SendKeys.SendWait("{DOWN}");
                        //    Thread.Sleep(500);
                        //    SendKeys.SendWait("{ENTER}");
                        //    Thread.Sleep(1000);
                        //    if (_CurrentData.InvoiceType == CustomerInvoiceType.Electronic)
                        //        ButtonCike(400, 350);
                        //    else
                        //        ButtonCike(400, 330);
                        //    //Thread.Sleep(200);
                        //    //ButtonCike(400, 336);
                        //    if (item.isUSD)
                        //    {
                        //        SetKeyCombination("国际货运代理费");
                        //    }
                        //    else
                        //    {
                        //        SetKeyCombination("国际货运代理费");
                        //    }

                        //    Thread.Sleep(500);
                        //    SendKeys.SendWait("{TAB}");
                        //    Thread.Sleep(500);
                        //    SendKeys.SendWait("{TAB}");
                        //    Thread.Sleep(500);
                        //    SendKeys.SendWait("{TAB}");
                        //    Thread.Sleep(500);
                        //    SendKeys.SendWait("1");
                        //    Thread.Sleep(500);
                        //    SendKeys.SendWait("{TAB}");
                        //    Thread.Sleep(500);
                        //    SendKeys.SendWait(item.Amount.ToString("F2"));
                        //    Thread.Sleep(200);
                        //    SendKeys.SendWait("{TAB}");
                        //    Thread.Sleep(200);

                        //    SendKeys.SendWait("{TAB}");
                        //    Thread.Sleep(300);
                        //    ButtonCike(955, 638);
                        //    Thread.Sleep(500);
                        //    ButtonCike(955, 638);
                        //    SendKeys.SendWait(item.Remark.ToUpper());
                        //}
                        //#endregion
                        //#region 宁波公司 /宁波跨境电商
                        //else
                        //{
                        //    //IntPtr ParenthWnd = FindWindow(null, "开具增值税普通发票");
                        //    if (ParenthWnd == IntPtr.Zero)//当前没打开的普通发票页面，则去打开
                        //    {
                        //        string mainFormName;
                        //        IntPtr mainFormWnd = new IntPtr(0);
                        //        mainFormName = kpTitle;
                        //        mainFormWnd = new IntPtr(0);
                        //        mainFormWnd = FindWindow(null, mainFormName);
                        //        if (mainFormWnd == IntPtr.Zero)
                        //        {
                        //            mainFormName = kpmTitle;
                        //            mainFormWnd = FindWindow(null, mainFormName);
                        //        }

                        //        //使主界面置前
                        //        SwitchToThisWindow(mainFormWnd, true);
                        //        ForceForegroundWindow(mainFormWnd);
                        //        //点击开票
                        //        Thread.Sleep(1000);
                        //        ButtonCike(ChangeScreenWidth(830), ChangeScreenHight(470));
                        //        if (_CurrentData.InvoiceType == CustomerInvoiceType.Dedicated)
                        //        {
                        //            Thread.Sleep(1000);
                        //            SendKeys.SendWait("{DOWN}");
                        //        }
                        //        else if (_CurrentData.InvoiceType == CustomerInvoiceType.Ordinary)
                        //        {
                        //            //点击普通发票
                        //            Thread.Sleep(1000);
                        //            SendKeys.SendWait("{DOWN}");
                        //            Thread.Sleep(1000);
                        //            SendKeys.SendWait("{DOWN}");
                        //            Thread.Sleep(1000);
                        //            SendKeys.SendWait("{ENTER}");
                        //            Thread.Sleep(1000);
                        //            ButtonDoubleCike(ChangeScreenWidth(800), ChangeScreenHight(300));
                        //            Thread.Sleep(1000);
                        //            SendKeys.SendWait("{ENTER}");
                        //            Thread.Sleep(1000);
                        //            //ParenthWnd = FindWindow(null, "开具增值税普通发票");
                        //        }
                        //        else
                        //        {
                        //            //电子发票
                        //            Thread.Sleep(1000);
                        //            SendKeys.SendWait("{DOWN}");
                        //            Thread.Sleep(1000);
                        //            SendKeys.SendWait("{DOWN}");
                        //            Thread.Sleep(1000);
                        //            SendKeys.SendWait("{DOWN}");
                        //            Thread.Sleep(1000);
                        //            SendKeys.SendWait("{ENTER}");
                        //            Thread.Sleep(1000);
                        //            SendKeys.SendWait("{ENTER}");
                        //        }

                        //    }

                        //    if (_CurrentData.InvoiceType == CustomerInvoiceType.Electronic)
                        //    {
                        //        //转到电子发票页面
                        //        SwitchToThisWindow(ParenthWnd, true);
                        //        ForceForegroundWindow(ParenthWnd);
                        //        InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
                        //        Thread.Sleep(1500);
                        //        ButtonCike(600, 280);
                        //        //ButtonCike(550, 222);
                        //        Thread.Sleep(1000);
                        //        SetKeyCombination(cmbInvoiceTitleName.Text);
                        //        Thread.Sleep(1500);
                        //        SendKeys.SendWait("{DOWN}");
                        //        Thread.Sleep(500);
                        //        SendKeys.SendWait("{ENTER}");
                        //        Thread.Sleep(500);
                        //        SendKeys.SendWait("{TAB}");
                        //        Thread.Sleep(500);
                        //        SendKeys.SendWait(cmbCustomerTaxIDNo.EditValue.ToString());
                        //        Thread.Sleep(500);
                        //        SendKeys.SendWait("{DOWN}");
                        //        Thread.Sleep(500);
                        //        SendKeys.SendWait("{ENTER}");
                        //        Thread.Sleep(500);
                        //        SendKeys.SendWait("{TAB}");
                        //        Thread.Sleep(500);
                        //        SendKeys.SendWait("{DOWN}");
                        //        Thread.Sleep(500);
                        //        SendKeys.SendWait("{ENTER}");
                        //        Thread.Sleep(500);
                        //        SendKeys.SendWait("{TAB}");
                        //        Thread.Sleep(500);
                        //        SendKeys.SendWait("{DOWN}");
                        //        Thread.Sleep(500);
                        //        SendKeys.SendWait("{ENTER}");
                        //        Thread.Sleep(1000);
                        //        ButtonCike(450, 410);
                        //        if (item.isUSD)
                        //        {
                        //            SetKeyCombination("代理海运费");
                        //        }
                        //        else
                        //        {
                        //            SetKeyCombination("代理运费");
                        //        }

                        //        Thread.Sleep(500);
                        //        SendKeys.SendWait("{TAB}");
                        //        Thread.Sleep(500);
                        //        SendKeys.SendWait("{TAB}");
                        //        Thread.Sleep(500);
                        //        SendKeys.SendWait("{TAB}");
                        //        Thread.Sleep(500);
                        //        SendKeys.SendWait("1");
                        //        Thread.Sleep(500);
                        //        SendKeys.SendWait("{TAB}");
                        //        Thread.Sleep(500);
                        //        SendKeys.SendWait(item.Amount.ToString("F2"));
                        //        Thread.Sleep(200);
                        //        SendKeys.SendWait("{TAB}");
                        //        Thread.Sleep(200);

                        //        SendKeys.SendWait("{TAB}");
                        //        Thread.Sleep(300);
                        //        ButtonCike(955, 638);
                        //        Thread.Sleep(500);
                        //        ButtonCike(955, 638);
                        //        SendKeys.SendWait(item.Remark.ToUpper());
                        //    }
                        //    else
                        //    {
                        //        #region 导入按钮
                        //        //屏幕截图 截取发票号区域
                        //        DiscernScreenInvoiceNo(new Point(997, 184), new Size(122, 32), "ocrtest.bmp");
                        //        //转到普通发票页面
                        //        SwitchToThisWindow(ParenthWnd, true);
                        //        ForceForegroundWindow(ParenthWnd);
                        //        InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
                        //        Thread.Sleep(3000);
                        //        //点击导入按钮
                        //        ButtonCike(600, 115);
                        //        Thread.Sleep(1000);
                        //        //点击手工导入
                        //        SendKeys.SendWait("{DOWN}");
                        //        Thread.Sleep(500);
                        //        SendKeys.SendWait("{DOWN}");
                        //        Thread.Sleep(500);
                        //        SendKeys.SendWait("{ENTER}");
                        //        Thread.Sleep(1000);
                        //        //发送文件路径
                        //        string ppp = string.Format(path + "{0}.xml", item.systemNo);
                        //        //MessageBox.Show(ppp);
                        //        //return;
                        //        SetKeyCombination(ppp);
                        //        Thread.Sleep(1000);
                        //        SendKeys.SendWait("{ENTER}");
                        //        Thread.Sleep(1000);
                        //        //点击选择
                        //        ButtonDoubleCike(1034, 224);
                        //        Thread.Sleep(1000);
                        //        #endregion
                        //    }
                        //}
                        //#endregion
                        }
                        #endregion
                }
                #endregion
            }
            catch (Exception e)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), e);
            }
        #endregion
        }
        [DllImport("USER32.DLL", EntryPoint = "FindWindowEx", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, uint hwndChildAfter, string lpszClass, string lpszWindow);

        public delegate bool CallBack(IntPtr hwnd, int lParam);
        [DllImport("USER32.DLL")]
        public static extern int EnumChildWindows(IntPtr hWndParent, CallBack lpfn, int lParam);
        /// <summary>
        /// 查找窗体上控件句柄
        /// </summary>
        /// <param name="hwnd">父窗体句柄</param>
        /// <param name="lpszWindow">控件标题(Text)</param>
        /// <param name="bChild">设定是否在子窗体中查找</param>
        /// <returns>控件句柄，没找到返回IntPtr.Zero</returns>
        private static IntPtr FindWindowEx(IntPtr hwnd, string lpszWindow, string ClassName, bool bChild)
        {
            IntPtr iResult = IntPtr.Zero;
            // 首先在父窗体上查找控件
            iResult = FindWindowEx(hwnd, 0, ClassName, lpszWindow);
            // 如果找到直接返回控件句柄
            if (iResult != IntPtr.Zero) return iResult;
            // 如果设定了不在子窗体中查找
            if (!bChild) return iResult;
            // 枚举子窗体，查找控件句柄
            int i = EnumChildWindows(
            hwnd,
            (h, l) =>
            {
                IntPtr f1 = FindWindowEx(h, 0, ClassName, lpszWindow);
                if (f1 == IntPtr.Zero)
                    return true;
                else
                {
                    iResult = f1;
                    return false;
            }
            },
            0);
            // 返回查找结果
            return iResult;
        }
        /// <summary>
        /// 识别屏幕区域发票号
        /// </summary>
        /// <param name="xy">识别起始点</param>
        /// <param name="area">识别区域</param>
        /// <param name="savePath">保存路径</savePath>
        /// <returns></returns>
        private void DiscernScreenInvoiceNo(Point xy, Size area, string savePath = "")
        {
            try
            {
                SnapScreenHelper snap = new SnapScreenHelper();
                //屏幕截图 截取发票号区域
                using (Image img = snap.SnapAreaScreen(xy, area, savePath))
                {
                    using (Stream ms = new MemoryStream())
                    {
                        img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                        //Stream stream = snap.ImageProcessing(ms, savePath);
                        BaiDuAIAccessToken token = CommonService.GetAccessToken("mMGWVEH0LINdapOpG7v8kP2R", "dPPaQdUm4rdgBAcKh8Ui4Hy1HSem7FoP");
                        string bsae64 = snap.ImgToBase64String(ms, ImageFormat.Bmp);
                        string reStr = CommonService.GetNumerResult(token.access_token, bsae64);
                        NumberResult result = JSONSerializerHelper.DeserializeFromJson<NumberResult>(reStr);
                        if (result != null && result.words_result_num > 0)
                        {
                            InvoiceNo = result.words_result[0].words;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                InvoiceNo = "";
                OCRError = ex.Message;
            }
        }

        /// <summary>
        /// 按分辨率调整按钮横坐标 额定 1600 * 900
        /// </summary>
        /// <param name="width"></param>
        /// <returns></returns>
        private int ChangeScreenWidth(int width)
        {
            return width * SW / 1600;
        }

        /// <summary>
        /// 按分辨率调整按钮纵坐标 额定 1600 * 900
        /// </summary>
        /// <param name="hight"></param>
        /// <returns></returns>
        private int ChangeScreenHight(int hight)
        {
            return hight * SH / 900;
        }
        #endregion

        #region 向税控系统中添加数据
        /// <summary>
        /// 向税控系统中添加数据
        /// </summary>
        private void SetTaxSystemData()
        {
            #region 1、拼装数据
            string customerName = cmbInvoiceTitleName.Text;
            string customerTaxNo = cmbCustomerTaxIDNo.Text;
            string customerTel = txtCustomerAddressTel.Text;
            string customerBank = cmbCustomerBankAccountNo.Text;
            string systemNo = txtNo.Text;
            string operationNos = string.Empty;
            string bankAccount = cmbBank1.Text;
            List<string> operationNoList = new List<string>();

            if (string.IsNullOrEmpty(customerName))
            {
                customerName = txtCustomer.Text;
            }
            #region  金额
            //含税金额
            decimal totalAmount = 0.0m;
            //不含税金额
            decimal totalTaxAmount = 0.0m;
            decimal usdAmount = 0.0m;
            decimal usdRate = 0.0m;
            decimal totalQty = (from d in CurrentInvoiceFeeSource select d.Quantity).Max();
            bool isUSD = false;

            if (CurrentInvoiceFeeSource.Count == 0)
            {
                return;
            }

            foreach (InvoiceFeeDate item in CurrentInvoiceFeeSource)
            {
                decimal rate = item.Rate;
                if (rate <= 0)
                {
                    rate = 1;
                }
                totalAmount = totalAmount + (rate * item.Amount);

                if (item.CurrencyID == new Guid("D67186CE-8B2C-4A75-81F1-A4FE3CC12DE9"))
                {
                    //美金
                    isUSD = true;
                    usdRate = item.Rate;
                    usdAmount += item.Amount;
                }
            }
            if ((_CurrentData.InvoiceType == CustomerInvoiceType.Ordinary && TasSystemCommon.GetParallelInvoiceCompanyIDList.Contains(_CurrentData.CompanyID))
                 || TasSystemCommon.GetOrdinaryInvoiceCompanyIDList.Contains(_CurrentData.CompanyID))
            {
                //深圳、上海、大连 的普票
                //天津、青岛、连云港公司时,税率是0
                totalTaxAmount = totalAmount;
            }
            else
            {
                totalTaxAmount = totalAmount / 1.06m;//得到纳税前的金额
            }
            #endregion


            foreach (BillList item in bill)
            {
                int i = (from d in item.Fees where d.Selected select d).Count();

                if (i > 0 && !operationNoList.Contains(item.No))
                {
                    operationNoList.Add(item.No);
                }
            }
            foreach (string str in operationNoList)
            {
                operationNos = operationNos + str.Substring(0, str.Length - 1) + ",";
            }
            operationNos = operationNos.Substring(0, operationNos.Length - 1);


            string strMeno = string.Empty;
            string blNo = _CurrentData.BLNo;
            if (string.IsNullOrEmpty(blNo) && !string.IsNullOrEmpty(_CurrentData.SONo))
            {
                blNo = _CurrentData.SONo;
            }
            if (_CurrentData.CompanyID == new Guid("41D7D3FE-183A-41CD-A725-EB6F728541EC"))
            {
                //深圳公司 MBLNo+HBLNo
                if (_CurrentData.BLNo != _CurrentData.SONo && _CurrentData.BLNo != string.Empty && _CurrentData.SONo != string.Empty)
                {
                    blNo = _CurrentData.BLNo + " " + _CurrentData.SONo;
                }
            }
            if (!string.IsNullOrEmpty(operationNos))
            {
                if (operationNos.Length > 34)
                {
                    operationNos = operationNos.Substring(0, 33);
                }
                operationNos = operationNos.Replace(Environment.NewLine, "");
            }
            if (!string.IsNullOrEmpty(blNo))
            {
                if (blNo.Length > 60)
                {
                    blNo = blNo.Substring(0, 59);
                }
                blNo = blNo.Replace(Environment.NewLine, "");
            }

            if (isUSD)
            {
                if (_CurrentData.CompanyID == new Guid("D8D57403-D663-4A93-A927-144907B7963B"))
                {//天津公司
                    strMeno = "此发票只接受美金付款" + " " + "USD" + usdAmount.ToString("F2") + " " + "USD:RMB=1:" + usdRate.ToString("F4")
                        + Environment.NewLine + operationNos + Environment.NewLine + blNo
                        + Environment.NewLine + _CurrentData.Vessel + " " + _CurrentData.Voyage + Environment.NewLine + _CurrentData.ETD;
                }
                else if (_CurrentData.CompanyID == new Guid("41D7D3FE-183A-41CD-A725-EB6F728541EC"))
                { //深圳公司（备注集体右移一个空格位）
                    strMeno = " 此发票只接受美金付款" + Environment.NewLine + " USD" + usdAmount.ToString("F2") + Environment.NewLine + " USD:RMB=1:" + usdRate.ToString("F4") + Environment.NewLine + " " + operationNos + Environment.NewLine + " " + blNo;
                }
                else
                {
                    strMeno = "此发票只接受美金付款" + Environment.NewLine + "USD" + usdAmount.ToString("F2") + Environment.NewLine + "USD:RMB=1:" + usdRate.ToString("F4") + Environment.NewLine + operationNos + Environment.NewLine + blNo;
                }

            }
            else
            {
                if (_CurrentData.CompanyID == new Guid("D8D57403-D663-4A93-A927-144907B7963B"))  //天津公司
                    strMeno = "RMB" + totalAmount.ToString("F2") + Environment.NewLine + operationNos + Environment.NewLine + blNo
                        + Environment.NewLine + _CurrentData.Vessel + " " + _CurrentData.Voyage + Environment.NewLine + _CurrentData.ETD;
                else if (_CurrentData.CompanyID == new Guid("41D7D3FE-183A-41CD-A725-EB6F728541EC"))  //深圳公司（备注集体右移一个空格位）
                    strMeno = " RMB" + totalAmount.ToString("F2") + Environment.NewLine + " " + operationNos + Environment.NewLine + " " + blNo;
                else
                    strMeno = "RMB" + totalAmount.ToString("F2") + Environment.NewLine + operationNos + Environment.NewLine + blNo;
            }
            int bankAccountNoIndex = GetTaxBankAccountNo(bankAccount);
            #endregion

            #region 2、打开开发票界面
            string mainFormClass = "TMainForm";
            string mainFormName = "增值税防伪税控系统防伪开票子系统";
            IntPtr mainFormWnd = new IntPtr(0);
            mainFormWnd = FindWindow(mainFormClass, mainFormName);
            if (mainFormWnd == IntPtr.Zero)
            {
                mainFormName = "增值税防伪税控系统开票子系统";
                mainFormWnd = FindWindow(mainFormClass, mainFormName);
            }
            //使主界面置前
            ForceForegroundWindow(mainFormWnd);

            //SendKey("%(F)", 20);
            //SendKey("Y", 20);
            //SendKey("%(B)", 50);
            //SendKey("1", 50);
            keybd_event(18, MapVirtualKey(18, 0), 0, 0); //按下ALT鍵。　　　 
            keybd_event(70, MapVirtualKey(70, 0), 0, 0); //按下f鍵。　　 
            keybd_event(70, MapVirtualKey(70, 0), 0x2, 0);//松开f鍵。
            keybd_event(18, MapVirtualKey(18, 0), 0x2, 0);//松开ALT鍵。
            keybd_event(89, MapVirtualKey(89, 0), 0, 0); //按下y鍵。　　 
            keybd_event(89, MapVirtualKey(89, 0), 0x2, 0);//松开y鍵。
            keybd_event(18, MapVirtualKey(18, 0), 0, 0); //按下ALT鍵。　　　 
            keybd_event(66, MapVirtualKey(66, 0), 0, 0); //按下b鍵。　　 
            keybd_event(66, MapVirtualKey(66, 0), 0x2, 0);//松开b鍵。
            keybd_event(18, MapVirtualKey(18, 0), 0x2, 0);//松开ALT鍵。
            KeyInput.SendKey(KeyboardConstaint.VK_NUMPAD1);

            if (_CurrentData.InvoiceType == CustomerInvoiceType.Unknown)
            {
                XtraMessageBox.Show("请选择发票类型");
                return;
            }
            if (TasSystemCommon.GetOrdinaryInvoiceCompanyIDList.Contains(_CurrentData.CompanyID))
            {
                //普通发票
                KeyInput.SendKey(KeyboardConstaint.VK_NUMPAD2);
            }
            else if (_CurrentData.InvoiceType == CustomerInvoiceType.Dedicated)
            {
                KeyInput.SendKey(KeyboardConstaint.VK_NUMPAD1);
            }
            else if (_CurrentData.InvoiceType == CustomerInvoiceType.Ordinary)
            {
                KeyInput.SendKey(KeyboardConstaint.VK_NUMPAD2);
            }

            //System.Windows.Forms.SendKeys.SendWait("{ENTER}"); 
            keybd_event(13, MapVirtualKey(13, 0), 0, 0); //按下enter鍵。　　 
            keybd_event(13, MapVirtualKey(13, 0), 0x2, 0);//松开enter鍵。

            Thread.Sleep(1000);
            #endregion
            MessageBox.Show("发票界面");
            return;
            #region 3、向税控系统中添加数据
            if (!IsOperTaxSystem())
            {
                return;
            }
            IntPtr ParenthWnd = new IntPtr(0);
            ParenthWnd = FindWindow(taxClassName, taxFormName);
            if (ParenthWnd == IntPtr.Zero)
            {
                if (!IsOperTaxSystem())
                {
                    return;
                }
            }
            //使开发票界面置前
            ForceForegroundWindow(ParenthWnd);
            SendKeys.SendWait("^ +");

            //复核
            IntPtr clReviewName = FindWindowEx(ParenthWnd, IntPtr.Zero, CustomerControlClassName, null);
            SendMessage(clReviewName, WM_SETFOCUS, IntPtr.Zero, IntPtr.Zero);
            int reviewNameIndex = GetTaxUserIndex(_CurrentData.ReviewName);
            if (reviewNameIndex >= 0)
            {
                for (int i = 0; i <= reviewNameIndex; i++)
                {
                    SendKey("{DOWN}", 20);
                }
            }

            //银行帐号
            IntPtr clBankAccount = FindWindowEx(ParenthWnd, clReviewName, CustomerControlClassName, null);
            SendMessage(clReviewName, WM_KILLFOCUS, IntPtr.Zero, IntPtr.Zero);
            if (bankAccountNoIndex > 0)
            {
                SendMessage(clBankAccount, CB_SETCURSEL, new IntPtr(bankAccountNoIndex), IntPtr.Zero);
            }

            //收款
            IntPtr clReceivablesName = FindWindowEx(ParenthWnd, clBankAccount, CustomerControlClassName, null);
            SendMessage(clBankAccount, WM_KILLFOCUS, IntPtr.Zero, IntPtr.Zero);
            SendMessage(clReceivablesName, WM_SETFOCUS, IntPtr.Zero, IntPtr.Zero);
            int receivablesNameIndex = GetTaxUserIndex(_CurrentData.ReceivablesName);
            if (receivablesNameIndex >= 0)
            {
                for (int i = 0; i <= receivablesNameIndex; i++)
                {
                    SendKey("{DOWN}", 20);
                }
            }

            //客户名称
            IntPtr clCustomerName = FindWindowEx(ParenthWnd, clReceivablesName, CustomerControlClassName, null);
            SendMessage(clReceivablesName, WM_KILLFOCUS, IntPtr.Zero, IntPtr.Zero);
            SendMessage(clCustomerName, WM_SETFOCUS, IntPtr.Zero, IntPtr.Zero);
            SendMessage(clCustomerName, WM_SETTEXT, IntPtr.Zero, customerName);

            //填入税号
            IntPtr clTaxNo = FindWindowEx(ParenthWnd, IntPtr.Zero, CustomerInfoControlClassName, null);
            SendMessage(clCustomerName, WM_KILLFOCUS, IntPtr.Zero, IntPtr.Zero);
            SendMessage(clTaxNo, WM_SETFOCUS, IntPtr.Zero, IntPtr.Zero);
            SendMessage(clTaxNo, WM_SETTEXT, IntPtr.Zero, customerTaxNo);

            //填入客户地址
            IntPtr clcustomerAddress = FindWindowEx(ParenthWnd, clTaxNo, CustomerInfoControlClassName, null);
            SendMessage(clTaxNo, WM_KILLFOCUS, IntPtr.Zero, IntPtr.Zero);
            SendMessage(clcustomerAddress, WM_SETFOCUS, IntPtr.Zero, IntPtr.Zero);
            SendMessage(clcustomerAddress, WM_SETTEXT, IntPtr.Zero, customerTel);

            //填入客户银行
            IntPtr clcustomerBankInfo = FindWindowEx(ParenthWnd, clcustomerAddress, CustomerInfoControlClassName, null);
            SendMessage(clcustomerAddress, WM_KILLFOCUS, IntPtr.Zero, IntPtr.Zero);
            SendMessage(clcustomerBankInfo, WM_SETFOCUS, IntPtr.Zero, IntPtr.Zero);
            SendMessage(clcustomerBankInfo, WM_SETTEXT, IntPtr.Zero, customerBank);


            //设置备注
            IntPtr menoControl = FindWindowEx(ParenthWnd, IntPtr.Zero, MenoControlClassName, null);
            SendMessage(clcustomerBankInfo, WM_KILLFOCUS, IntPtr.Zero, IntPtr.Zero);
            SendMessage(clBankAccount, WM_SETFOCUS, IntPtr.Zero, IntPtr.Zero);
            SendKey("{TAB}", 20);
            SendMessage(menoControl, WM_SETTEXT, IntPtr.Zero, strMeno);
            //SetKeyCombination(strMeno);
            SendKey("{0}", 20);
            SendKey("{BACKSPACE}", 20);
            //System.Threading.Thread.Sleep(500);
            SendMessage(clBankAccount, WM_KILLFOCUS, IntPtr.Zero, IntPtr.Zero);

            //使客户银行信息获得焦点，让明细可以模拟键盘录入
            SendMessage(clcustomerBankInfo, WM_SETFOCUS, IntPtr.Zero, IntPtr.Zero);

            #region KEYBDINPUT

            //3、插入明细数据           
            short isCapital = KeyInput.GetKeyState(KeyboardConstaint.VK_CAPITAL);  //是否大写锁定--0小写，1大写
            //List<short> keyList = new List<short>();
            KeyInput.SendKey(KeyboardConstaint.VK_TAB);
            ////货品代码

            //商品名称
            String goods = "代理海运费";
            if (TasSystemCommon.GetParallelInvoiceCompanyIDList.Contains(_CurrentData.CompanyID))
            {
                //深圳公司&&大连公司&&上海
                if (TasSystemCommon.GetParallelInvoiceCompanyIDList.Contains(_CurrentData.CompanyID))
                {
                    if (_CurrentData.InvoiceType == CustomerInvoiceType.Dedicated)
                    {
                        #region  专用发票
                        if (CurrentRow.CurrencyName == "RMB")
                        {
                            if (ClientConfig.Current.Contains(InvoiceCommandConstants.KPGoods_RMB_Z))
                            {
                                goods = ClientConfig.Current.GetValue(InvoiceCommandConstants.KPGoods_RMB_Z);
                            }
                            else
                            {
                                ClientConfig.Current.AddValue(InvoiceCommandConstants.KPGoods_RMB_Z, "01");
                            }
                        }
                        else if (CurrentRow.CurrencyName == "USD")
                        {
                            if (ClientConfig.Current.Contains(InvoiceCommandConstants.KPGoods_USD_Z))
                            {
                                goods = ClientConfig.Current.GetValue(InvoiceCommandConstants.KPGoods_USD_Z);
                            }
                            else
                            {
                                ClientConfig.Current.AddValue(InvoiceCommandConstants.KPGoods_USD_Z, "01");
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region 普通发票
                        if (CurrentRow.CurrencyName == "RMB")
                        {
                            if (ClientConfig.Current.Contains(InvoiceCommandConstants.KPGoods_RMB_P))
                            {
                                goods = ClientConfig.Current.GetValue(InvoiceCommandConstants.KPGoods_RMB_P);
                            }
                            else
                            {
                                ClientConfig.Current.AddValue(InvoiceCommandConstants.KPGoods_RMB_P, "12");
                            }
                        }
                        else if (CurrentRow.CurrencyName == "USD")
                        {
                            if (ClientConfig.Current.Contains(InvoiceCommandConstants.KPGoods_USD_P))
                            {
                                goods = ClientConfig.Current.GetValue(InvoiceCommandConstants.KPGoods_USD_P);
                            }
                            else
                            {
                                ClientConfig.Current.AddValue(InvoiceCommandConstants.KPGoods_USD_P, "12");
                            }
                        }
                        #endregion
                    }
                }
            }
            else
            {
                switch (CurrentRow.CurrencyName)
                {
                    case "RMB":
                        if (ClientConfig.Current.Contains(InvoiceCommandConstants.KPGoods_RMB))
                            goods = ClientConfig.Current.GetValue(InvoiceCommandConstants.KPGoods_RMB);
                        else
                        {
                            ClientConfig.Current.AddValue(InvoiceCommandConstants.KPGoods_RMB, "代理海运费");
                        }
                        break;
                    case "USD":
                        if (ClientConfig.Current.Contains(InvoiceCommandConstants.KPGoods_USD))
                            goods = ClientConfig.Current.GetValue(InvoiceCommandConstants.KPGoods_USD);
                        else
                        {
                            ClientConfig.Current.AddValue(InvoiceCommandConstants.KPGoods_USD, "代理海运费");
                        }
                        break;
                    default:
                        break;
                }
            }
            SetKeyCombination(goods);

            KeyInput.SendKey(KeyboardConstaint.VK_TAB);

            #region 规格型号
            foreach (short typeNo in Encoding.Default.GetBytes(systemNo))
            {
                if (typeNo >= 97 && typeNo <= 122)  //a~z
                {
                    if (isCapital == 0)
                        KeyInput.SendKey((short)(Convert.ToInt16(typeNo) - 32));
                    else
                    {
                        KeyInput.keybd_event(16, KeyInput.MapVirtualKey(16, 0), 0, 0); //按下Shift鍵。　　 
                        KeyInput.SendKey((short)(Convert.ToInt16(typeNo) - 32));
                        KeyInput.keybd_event(16, KeyInput.MapVirtualKey(16, 0), 0x2, 0);//松开Shift鍵。   
                    }
                }
                else if (typeNo >= 65 && typeNo <= 90)  //A~Z
                {
                    if (isCapital == 0)
                    {
                        KeyInput.keybd_event(16, KeyInput.MapVirtualKey(16, 0), 0, 0); //按下Shift鍵。　　 
                        KeyInput.SendKey(typeNo);
                        KeyInput.keybd_event(16, KeyInput.MapVirtualKey(16, 0), 0x2, 0);//松开Shift鍵。   
                    }
                    else
                        KeyInput.SendKey(typeNo);
                }
                else
                    KeyInput.SendKey(typeNo);
            }
            #endregion

            KeyInput.SendKey(KeyboardConstaint.VK_TAB);
            //计量单位（由货品编码列表生成）
            KeyInput.SendKey(KeyboardConstaint.VK_TAB);
            //数量
            KeyInput.SendKey(KeyboardConstaint.VK_NUMPAD1);
            KeyInput.SendKey(KeyboardConstaint.VK_TAB);

            #region 单价
            foreach (short taxAmount in Encoding.Default.GetBytes(totalTaxAmount.ToString("F7")))
            {
                if (taxAmount == 46)  //小数点
                    KeyInput.SendKey(KeyboardConstaint.VK_DECIMAL);
                else
                    KeyInput.SendKey(taxAmount);
            }
            #endregion

            KeyInput.SendKey(KeyboardConstaint.VK_TAB);

            #endregion


            #region SendKeys

            #endregion

            #endregion
        }

        private void SendKey(string key, int time)
        {
            SendKeys.Send(key);
            Thread.Sleep(time);
            SendKeys.Flush();
        }

        #region Set UAC

        private bool UACisSilent()
        {
            bool flag = true;
            OperatingSystem os = Environment.OSVersion;
            if (os.Platform == PlatformID.Win32NT)
            {
                //MessageBox.Show(os.Version.Major.ToString());
                if (os.Version.Major == 6)  //OS is Win7 or Win Vista
                {
                    //MessageBox.Show("start");
                    object obj = Registry.LocalMachine
                            .OpenSubKey("SOFTWARE")
                            .OpenSubKey("Microsoft")
                            .OpenSubKey("Windows")
                            .OpenSubKey("CurrentVersion")
                            .OpenSubKey("Policies")
                            .OpenSubKey("System")
                            .OpenSubKey("EnableLUA");
                    if (obj != null)
                        if ((int)obj == 0)
                            flag = true;
                        else
                            flag = false;
                    //MessageBox.Show("end");
                }
            }
            return flag;
        }

        static void SetUACSilent(bool isSilent)
        {
            List<RegistryKey> regKeys = new List<RegistryKey>();
            try
            {
                string path = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies";
                RegistryKey reg = Registry.LocalMachine.OpenSubKey(path, true);
                regKeys.Add(reg);
                reg = reg.OpenSubKey("System", true);
                if (reg == null)
                {
                    reg = reg.CreateSubKey("System", RegistryKeyPermissionCheck.ReadWriteSubTree);
                }
                if (reg != null && reg.Name.EndsWith("System"))
                {
                    regKeys.Add(reg);
                    reg.SetValue("ConsentPromptBehaviorAdmin", isSilent ? 0 : 2, RegistryValueKind.DWord);
                }
            }
            finally
            {
                for (int i = regKeys.Count - 1; i >= 0; i--)
                {
                    regKeys[i].Close();
                }
            }
        }

        #endregion

        /// <summary>
        /// 得到税控系统中用户的代码
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private int GetTaxUserIndex(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return -1;
            }
            if (!File.Exists(taxUserInfoXml))
            {
                return -1;
            }
            XElement rootUser = XElement.Load(taxUserInfoXml);
            IEnumerable<XElement> query = (from ele in rootUser.Elements("TaxUserInfo") where ele.Element("Name").Value == userName select ele);

            foreach (XElement item in query)
            {
                return Convert.ToInt16(item.Element("ID").Value);
            }

            return -1;
        }
        /// <summary>
        /// 得到税控系统中代理海运费的代码 
        /// </summary>
        /// <returns></returns>
        private string GetTaxCargoIndex()
        {
            if (!File.Exists(taxCargoName))
            {
                return string.Empty;
            }
            XElement rootUser = XElement.Load(taxCargoName);
            IEnumerable<XElement> query = query = (from ele in rootUser.Elements("TaxCargoInfo") where ele.Element("Name").Value == "代理海运费" select ele);

            ///青岛公司的人民币是开代理运费
            if (_CurrentData.CompanyID == new Guid("F289109A-C29E-4B0B-A41A-C22D9E70A72F")
                && cmbBank1.Text.Contains("RMB"))
            {
                query = query = (from ele in rootUser.Elements("TaxCargoInfo") where ele.Element("Name").Value == "代理运费" select ele);
            }


            foreach (XElement item in query)
            {
                return item.Element("Code").Value;
            }
            return string.Empty;

        }
        /// <summary>
        /// 得到税系统中的银行代码
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private int GetTaxBankAccountNo(string bankAccountNo)
        {
            if (string.IsNullOrEmpty(bankAccountNo))
            {
                return 0;
            }
            string currencyName = string.Empty;
            int currencyIndex = 0;
            string accountNo = string.Empty;
            if (bankAccountNo.Contains("(RMB)"))
            {
                currencyName = "RMB";
                currencyIndex = bankAccountNo.IndexOf("(RMB)");
            }
            else if (bankAccountNo.Contains("(USD)"))
            {
                currencyName = "USD";
                currencyIndex = bankAccountNo.IndexOf("(USD)");
            }
            else if (bankAccountNo.Contains("(HKD)"))
            {
                currencyName = "HKD";
                currencyIndex = bankAccountNo.IndexOf("(HKD)");
            }
            if (currencyIndex > 0)
            {
                currencyIndex = currencyIndex + 5;
            }
            accountNo = bankAccountNo.Substring(currencyIndex).Trim(); ;
            if (!File.Exists(taxBankAccountXml))
            {
                return 0;
            }

            XElement rootUser = XElement.Load(taxBankAccountXml);
            IEnumerable<XElement> query = (from ele in rootUser.Elements("TaxBankAccountInfo")
                                               //where ele.Element("Name").Value.Contains(currencyName) && ele.Element("Name").Value.Contains(accountNo)  //bak
                                           where (ele.Element("Name").Value.Contains(currencyName)
                                           || (ele.Element("Name").Value.Contains("人民币") ? ele.Element("Name").Value.Contains("RMB") :
                                           (ele.Element("Name").Value.Contains("港币") ? ele.Element("Name").Value.Contains("HKD") :
                                           ele.Element("Name").Value.Contains("USD"))))
                                           && ele.Element("Name").Value.Contains(accountNo)
                                           select ele);
            //select new rootUser.Elements("TaxBankAccountInfo")
            //{
            //    ele.Element("ID").Value,
            //    ele.Element("Name").Value=ele.Element("Name").Value.Replace("人民币","RMB")
            //});
            string id = "0";
            foreach (XElement item in query)
            {
                id = item.Element("ID").Value;
                break;
            }

            LogHelper.SaveLog("BankAccountNo:" + bankAccountNo + "   CurrencyName:" + currencyName + "  Account:" + accountNo + "  IndexNo:" + id);
            return Convert.ToInt32(id);
        }

        #region 币种替换——作废
        //public int CurrencyConverter(string name)
        //{
        //    string pattern = @"(?is)(\[)?(.*)(\])";
        //    string replacement = "$2";
        //    System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex(pattern);
        //    switch (rgx.Replace(name, replacement))
        //    {
        //        case "人民币":
        //            return System.Text.RegularExpressions.Regex.Replace(name, @"^\([a-zA-Z0-9]+\)$", "RMB");
        //        case "美金":
        //            return System.Text.RegularExpressions.Regex.Replace(name, @"^\([a-zA-Z0-9]+\)$", "USD");
        //        case "港币":
        //            return System.Text.RegularExpressions.Regex.Replace(name, @"^\([a-zA-Z0-9]+\)$", "HKD");
        //        default:
        //            return 0;
        //    }
        //}
        #endregion


        string taxZYFormName = "增值税专用发票填开";
        string taxPTFormName = "增值税普通发票填开";

        string taxZYFormClassName = "TSpecInvoiceForm";
        string taxPTFormClassName = "TMultiCommInvFrm";

        string taxUserInfoXml = "TaxUserInfo.xml";
        string taxCargoName = "TaxCargoInfo.xml";
        string taxBankAccountXml = "TaxBankAccountInfo.xml";

        /// <summary>
        /// 税控系统名称
        /// </summary>
        string taxFormName = "";
        /// <summary>
        /// 税控系统类名
        /// </summary>
        string taxClassName = "";
        /// <summary>
        /// 控件类名
        /// </summary>
        public string CustomerControlClassName = "TDBComboBox";
        /// <summary>
        /// 客户信息
        /// </summary>
        public string CustomerInfoControlClassName = "TDBComboBoxE";
        /// <summary>
        /// 备注控件类名
        /// </summary>
        public string MenoControlClassName = "TDBMemo";
        /// <summary>
        /// 上海公司开票系统
        /// </summary>
        /// <param name="formName"></param>
        /// <returns></returns>
        private bool SHIsOperTaxSystem(string formName)
        {
            IntPtr ParenthWnd = new IntPtr(0);
            ParenthWnd = FindWindow("TCustomBaseForm", formName);

            if (ParenthWnd == IntPtr.Zero)
            {
                DialogResult result = MessageBox.Show("请先登陆税控系统,并打开增值税专用/(普通)发票填开界面", "提示", MessageBoxButtons.RetryCancel);
                if (result == DialogResult.Cancel)
                {
                    return false;
                }
                else
                {
                    return IsOperTaxSystem();
                }
            }
            return true;
        }

        /// <summary>
        /// 是否打开了税控系统
        /// </summary>
        /// <returns></returns>
        private bool IsOperTaxSystem()
        {
            if (_CurrentData.CompanyID == new Guid("B1AFAD8F-55DD-4E29-A250-EB82AB3971FE"))
            {
                //大连公司的电脑比较垃圾,打开速度太慢,先等0.5秒种后再判断界面是否已经打开
                Thread.Sleep(500);
            }

            IntPtr ParenthWnd = new IntPtr(0);
            ParenthWnd = FindWindow(taxZYFormClassName, taxZYFormName);

            if (ParenthWnd == IntPtr.Zero)
            {
                ParenthWnd = FindWindow(taxPTFormClassName, taxPTFormName);
                if (ParenthWnd == IntPtr.Zero)
                {
                    DialogResult result = MessageBox.Show("请先登陆税控系统,并打开增值税专用/(普通)发票填开界面", "提示", MessageBoxButtons.RetryCancel);
                    if (result == DialogResult.Cancel)
                    {
                        return false;
                    }
                    else
                    {
                        return IsOperTaxSystem();
                    }
                }
                else
                {
                    taxClassName = taxPTFormClassName;
                    taxFormName = taxPTFormName;
                    return true;
                }
            }
            else
            {
                taxClassName = taxZYFormClassName;
                taxFormName = taxZYFormName;
                return true;
            }

        }
        /// <summary>
        /// 设置当前输入法为英文输入法
        /// </summary>
        private void SetEnInputLanguage()
        {
            foreach (InputLanguage inputLanguage in InputLanguage.InstalledInputLanguages)
            {
                if (inputLanguage.Culture.Name.IndexOf("en") >= 0 ||
                    inputLanguage.LayoutName.IndexOf("美式键盘") >= 0)
                {
                    InputLanguage.CurrentInputLanguage = inputLanguage;
                    break;
                }
            }
        }

        #endregion

        #region 重启服务

        ServiceController service = new ServiceController("InvoicesSyncService");
        private void RestartService()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (service.Status == ServiceControllerStatus.Running)
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped);
                }
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running);

            }
            catch (Exception)
            {
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        public string RunDosCommand(string command)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.StandardInput.WriteLine(command);
            process.StandardInput.WriteLine("exit");
            return process.StandardOutput.ReadToEnd();
        }

        #endregion

        /// <summary>
        /// 发票号
        /// </summary>
        string InvoiceNo = "";
        string OCRError = "";
        /// <summary>
        /// 获取发票号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barGetInvoiceNo_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (string.IsNullOrEmpty(InvoiceNo))
            {
                XtraMessageBox.Show("发票号获取失败\r\n" + OCRError);
                return;
            }
            SaveInvoiceNo(InvoiceNo, txtInvoiceNo.Text + "");

            #region The orgial code

            //if (_CurrentData == null || string.IsNullOrEmpty(_CurrentData.No))
            //{
            //    return;
            //}
            //List<string> noList = new List<string>();
            //noList.Add(_CurrentData.No);

            //TasSystemCommon.GetTaxInvoiceNo(this.FindForm(), finService, noList);

            //System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            //timer.Interval = 1000;
            //timer.Enabled = true;
            //int tcount = 0;

            //timer.Tick += delegate(object oj, EventArgs ea)
            //{
            //    tcount++;
            //    if (!TasSystemCommon.isSuccess)
            //    {
            //        return;
            //    }
            //    if (tcount > 70)
            //    {
            //        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), "获取超时,请稍候重试");
            //        timer.Enabled = false;
            //        return;
            //    }
            //    timer.Enabled = false;
            //    bool isDirty = _CurrentData.IsDirty;
            //List<InvoiceInfo> list = TasSystemCommon.List;
            //    if (list != null && list.Count > 0)
            //    {
            //        _CurrentData.No = list[0].No;
            //        _CurrentData.InvoiceNo = list[0].InvoiceNo;
            //        _CurrentData.UpdateDate = list[0].UpdateDate;

            //        if (!isDirty)
            //        {
            //            _CurrentData.IsDirty = false;
            //            _CurrentData.BeginEdit();
            //        }

            //        bindingSource1.ResetBindings(false);

            //    }
            //};

            #endregion

            #region 李旭斌代码

            //if (_CurrentData == null || string.IsNullOrEmpty(_CurrentData.No))
            //{
            //    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "Plealse select or new the invoice bill first." : "请先指定或新增发票。");
            //    return;
            //}

            //if (_CurrentData.CompanyID == new Guid("B1AFAD8F-55DD-4E29-A250-EB82AB3971FE"))
            //{
            //    #region 大连
            //    /*
            //     *  上海的税控系统是新的版本，目前没有找到税控系统的数据文件存放在什么位置 
            //     *  目前的方案是从本地的日志文件中找发票号
            //     *  日志在SignLog目录下的，以发票日期为文件夹，搜索日期文件夹内的log日志文件
            //     *  如果该文件中包含了"<ggxh>单号</ggxh>"的内容，则取出这个文件名的名字，去掉前后多余的部分，就是发票号
            //     */
            //    if (string.IsNullOrEmpty(ClientConfig.Current.GetValue("KPDATAFILE")))
            //    {
            //        ClientConfig.Current.AddValue("KPDATAFILE", "C:\\Program Files\\税控发票开票软件(税控盘版)");
            //    }
            //    string day = _CurrentData.InvoiceDate.ToString("yyyyMMdd");
            //    string path = ClientConfig.Current.GetValue("KPDATAFILE") + "\\SignLog\\" + day;
            //    DirectoryInfo folder = new DirectoryInfo(path);
            //    if (!Directory.Exists(path))
            //    {
            //        XtraMessageBox.Show("没有找到[" + day + "]这天的文件数据");
            //        return;
            //    }
            //    string invoiceNo = string.Empty;

            //    foreach (FileInfo file in folder.GetFiles("*.log"))
            //    {
            //        if (!File.Exists(file.FullName))
            //        {
            //            continue;
            //        }
            //        if (file.Name.Contains("_verifysign.log"))
            //        {
            //            StreamReader sr = new StreamReader(file.FullName, Encoding.Default);
            //            string str;
            //            while ((str = sr.ReadLine()) != null)
            //            {
            //                if (str.Contains("<ggxh>" + _CurrentData.No + "</ggxh>"))
            //                {
            //                    invoiceNo = file.Name.Replace("007_3100144350_", "").Replace("_verifysign", "").Replace(".log", "");
            //                    break;
            //                }
            //            }
            //            sr.Close();
            //        }
            //    }
            //    if (string.IsNullOrEmpty(invoiceNo))
            //    {
            //        XtraMessageBox.Show("没有找到发票号.");
            //        return;
            //    }

            //    SaveInvoiceNo(invoiceNo, string.Empty);

            //    #endregion
            //}
            //else if (_CurrentData.CompanyID == new Guid("B13FAC2D-8250-4990-A622-5ECA00D3A030")
            //    || _CurrentData.CompanyID == new Guid("A62A9F8E-E69C-4E6E-AD85-E75AED3C6CF9")
            //    || _CurrentData.CompanyID == new Guid("2AFAC53A-2AF9-46ED-8C4B-7035FEDC0279")
            //    || _CurrentData.CompanyID == new Guid("F289109A-C29E-4B0B-A41A-C22D9E70A72F")
            //    || _CurrentData.CompanyID == new Guid("D8D57403-D663-4A93-A927-144907B7963B")
            //    || _CurrentData.CompanyID == new Guid("FD69B51B-E71F-4040-8F4B-28447A003C93")
            //    || _CurrentData.CompanyID == new Guid("41D7D3FE-183A-41CD-A725-EB6F728541EC")
            //    || _CurrentData.CompanyID == new Guid("62D46581-B6CC-477E-8A60-7375FACD9813"))
            //{

            //    string taxNo = FinanceService.GetCompanyTaxNo((Guid)cmbCompany.EditValue);

            //    //string taxNo = "440300746620601";

            //    if (string.IsNullOrEmpty(ClientConfig.Current.GetValue("KPDATAFILE")))
            //    {
            //        if (_CurrentData.CompanyID == new Guid("D8D57403-D663-4A93-A927-144907B7963B"))
            //            ClientConfig.Current.AddValue("KPDATAFILE", @"D:\开票软件\");
            //        else
            //            ClientConfig.Current.AddValue("KPDATAFILE", @"C:\Program Files\开票软件\");
            //    }
            //    string kpVersion = string.Empty;
            //    ConfigureInfo confige = ConfigureService.GetConfigureInfoByCompanyID(_CurrentData.CompanyID);
            //    if (confige != null)
            //        kpVersion = confige.TaxControlVersion;
            //    string path = string.Empty;
            //    string strNo = string.Empty;
            //    string indexOfChar = string.Empty;
            //    //if (kpVersion.CompareTo("V2.1.30.180828") == -1)
            //    if (string.Compare(kpVersion, "V2.1.30.180828") < 0)
            //    {
            //        path = ClientConfig.Current.GetValue("KPDATAFILE") + taxNo + @".0\Log\log.log";
            //        strNo = "调用接口取到的发票号码";
            //        indexOfChar = "=";
            //    }
            //    else
            //    {
            //        path = ClientConfig.Current.GetValue("KPDATAFILE") + taxNo + @".0\Log\UpDownLog.log";
            //        strNo = "发票上传：fpdm";
            //        indexOfChar = "码";
            //    }
            //    string message = "没有找到开票软件数据！" + Environment.NewLine + path;

            //    if (!File.Exists(path))
            //    {
            //        XtraMessageBox.Show(message);
            //        return;
            //    }
            //    string newPath = path.Replace(".log", "BAK.log");
            //    if (File.Exists(newPath))
            //        File.Delete(newPath);
            //    File.Copy(path, newPath);
            //    string invoiceNo = string.Empty;
            //    string[] files = File.ReadAllLines(newPath, Encoding.Default).Reverse().ToArray();
            //    for (int i = 0; i < files.Length; i++)
            //    {
            //        if (files[i].Contains(strNo))// files[i].Contains(_CurrentData.InvoiceDate.ToString("yyyy-MM-dd")))
            //        {
            //            if (kpVersion.CompareTo("V2.1.30.180828") == -1)
            //            {
            //                invoiceNo = files[i].Substring(files[i].IndexOf(indexOfChar) + 1, 8);
            //                break;
            //            }
            //            else
            //            {
            //                string taxIDNo = string.Format("<GFSH>{0}</GFSH>", _CurrentData.CustomerTaxIDNo);
            //                string feeTotal = txtFeeTotal.Text.Contains("RMB") ? txtFeeTotal.Text.Substring(txtFeeTotal.Text.IndexOf("RMB:") + 4) : txtFeeTotal.Text.Substring(txtFeeTotal.Text.IndexOf(":") + 1);
            //                string taxAmount = string.Format("<JE>{0}</JE>", feeTotal.ToString().Replace(",", "").Trim());
            //                if (files[i - 1].Contains(taxIDNo) || files[i - 1].Contains(taxAmount))
            //                {
            //                    //if (kpVersion.CompareTo("V2.1.30.180828") == -1)
            //                    //invoiceNo = files[i].Substring(files[i].IndexOf(indexOfChar) + 1, 8);
            //                    //else
            //                    invoiceNo = files[i].Substring(files[i].IndexOf(indexOfChar) + 2, 8);
            //                    break;
            //                }
            //            }
            //        }
            //    }
            //    //FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            //    //StreamReader sr = new StreamReader(fs, Encoding.Default);
            //    //string str, invoiceNo = string.Empty;
            //    //while ((str = sr.ReadLine()) != null)
            //    //{
            //    //    string s = _CurrentData.Amounts;
            //    //    string ss = _CurrentData.CustomerTaxIDNo;
            //    //    if (str.Contains(strNo) && str.Contains(_CurrentData.InvoiceDate.ToString("yyyy-MM-dd HH")))
            //    //    {
            //    //        if (kpVersion.CompareTo("V2.1.30.180828") == -1)
            //    //            invoiceNo = str.Substring(str.IndexOf(indexOfChar) + 1, 8);
            //    //        else
            //    //            invoiceNo = str.Substring(str.IndexOf(indexOfChar) + 2, 8);
            //    //    }
            //    //}

            //    if (string.IsNullOrEmpty(invoiceNo))
            //    {
            //        XtraMessageBox.Show("没有找到发票号.");
            //        return;
            //    }

            //    SaveInvoiceNo(invoiceNo, string.Empty);

            //}
            //else
            //{
            //    #region 其他口岸


            //    string xmlSalesInvoiceDtl = Application.StartupPath + "\\SalesInvoiceDtl.xml";

            //    #region 删除同步XML文件

            //    //if (File.Exists(xmlSalesInvoiceDtl))
            //    //{
            //    //    File.Delete(xmlSalesInvoiceDtl);
            //    //}

            //    #endregion

            //    string preData = txtInvoiceNo.Text;
            //    try
            //    {
            //        Cursor = Cursors.WaitCursor;
            //        string invoiceNo = string.Empty;

            //        #region 启动数据同步服务

            //        bool flag = false;
            //        Process[] allProcess = Process.GetProcesses();
            //        foreach (Process p in allProcess)
            //        {

            //            if (p.ProcessName.ToLower() == "invoicessync")
            //            {
            //                for (int i = 0; i < p.Threads.Count; i++)
            //                    p.Threads[i].Dispose();
            //                p.Kill();
            //                //break;
            //                flag = true;
            //            }
            //        }
            //        if (flag)
            //            RunDosCommand("net stop InvoicesSyncService");
            //        RunDosCommand("net start InvoicesSyncService");
            //        //Thread.Sleep(200);

            //        #endregion

            //        //获取kp销项发票明细列表
            //        if (!File.Exists(xmlSalesInvoiceDtl))
            //        {
            //            //找不到文件时，等待3秒后再找
            //            Thread.Sleep(3000);
            //            if (!File.Exists(xmlSalesInvoiceDtl))
            //            {
            //                //还是找不到，提示
            //                XtraMessageBox.Show("找不到SalesInvoiceDtl.xml文件，请检查文件是否存在或路径是否正确。");
            //                return;
            //            }
            //        }
            //        DataSet ds = new DataSet();
            //        ds.ReadXml(xmlSalesInvoiceDtl);
            //        DataRow[] rows = ds.Tables[0].Select(string.Format("规格型号='{0}'", _CurrentData.No));
            //        if (rows.Length > 0)
            //        {
            //            invoiceNo = rows[0]["发票号码"].ToString().Trim().PadLeft(8, '0');
            //        }
            //        if (string.IsNullOrEmpty(invoiceNo))
            //        {
            //            #region 删除同步XML文件

            //            if (File.Exists(xmlSalesInvoiceDtl))
            //            {
            //                File.Delete(xmlSalesInvoiceDtl);
            //            }

            //            #endregion

            //            #region 启动数据同步服务

            //            Process[] allProcess1 = Process.GetProcesses();
            //            foreach (Process p in allProcess1)
            //            {

            //                if (p.ProcessName.ToLower() == "invoicessync")
            //                {
            //                    for (int i = 0; i < p.Threads.Count; i++)
            //                        p.Threads[i].Dispose();
            //                    p.Kill();
            //                    //break;
            //                }
            //            }
            //            RunDosCommand("net stop InvoicesSyncService");
            //            RunDosCommand("net start InvoicesSyncService");
            //            //Thread.Sleep(200);

            //            #endregion

            //            XtraMessageBox.Show("找不到对应的发票，如果发票已经打印。请手动输入发票号；\r\n如果未打印，请检查发票填开界面【规格型号】里是否已填入系统单号，填好后再打印。");
            //            return;
            //        }
            //        else
            //        {
            //            SaveInvoiceNo(invoiceNo, preData);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        #region 删除同步XML文件

            //        if (File.Exists(xmlSalesInvoiceDtl))
            //        {
            //            File.Delete(xmlSalesInvoiceDtl);
            //        }

            //        #endregion

            //        #region 启动数据同步服务

            //        Process[] allProcess1 = Process.GetProcesses();
            //        foreach (Process p in allProcess1)
            //        {

            //            if (p.ProcessName.ToLower() == "invoicessync")
            //            {
            //                for (int i = 0; i < p.Threads.Count; i++)
            //                    p.Threads[i].Dispose();
            //                p.Kill();
            //                //break;
            //            }
            //        }
            //        RunDosCommand("net stop InvoicesSyncService");
            //        RunDosCommand("net start InvoicesSyncService");
            //        //Thread.Sleep(200);

            //        #endregion

            //        txtInvoiceNo.Text = preData;
            //        LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            //    }
            //    finally
            //    {
            //        #region 停止数据同步服务

            //        //Process[] allProcess2 = Process.GetProcesses();
            //        //foreach (Process p in allProcess2)
            //        //{

            //        //    if (p.ProcessName.ToLower() == "invoicessync")
            //        //    {
            //        //        for (int i = 0; i < p.Threads.Count; i++)
            //        //            p.Threads[i].Dispose();
            //        //        p.Kill();
            //        //        //break;
            //        //    }
            //        //}
            //        //RunDosCommand("net stop InvoicesSyncService");

            //        //Thread.Sleep(200);

            //        #endregion

            //        #region 删除同步XML文件

            //        //if (File.Exists(xmlSalesInvoiceDtl))
            //        //{
            //        //    File.Delete(xmlSalesInvoiceDtl);
            //        //}

            //        #endregion

            //        Cursor = Cursors.Default;
            //    }
            //    #endregion
            //}

            #endregion
        }
        /// <summary>
        /// 保存发票号
        /// </summary>
        /// <param name="invoiceNo">目前获取的发票号</param>
        /// <param name="preData">之前的发票号（编辑）</param>
        private void SaveInvoiceNo(string invoiceNo, string preData)
        {
            txtInvoiceNo.Text = invoiceNo;
            List<string> invoiceNoList = new List<string>();
            List<string> systemNoList = new List<string>();
            invoiceNoList.Add(invoiceNo);
            systemNoList.Add(_CurrentData.No);
            List<InvoiceInfo> list = FinanceService.SaveInvoiceNo(systemNoList.ToArray(), invoiceNoList.ToArray(), LocalData.UserInfo.LoginID, LocalData.IsEnglish);
            bool isDirty = _CurrentData.IsDirty;
            if (list != null && list.Count > 0)
            {
                _CurrentData.No = list[0].No;
                _CurrentData.InvoiceNo = list[0].InvoiceNo;
                _CurrentData.UpdateDate = list[0].UpdateDate;

                if (!isDirty)
                {
                    _CurrentData.IsDirty = false;
                    _CurrentData.BeginEdit();
                }

                bindingSource1.ResetBindings(false);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                Cursor = Cursors.Default;
            }
            else
            {
                txtInvoiceNo.Text = preData;
            }
            InvoiceLogHelper.SaveLogInfo("自动获得发票号后保存:   " + invoiceNo);
        }


        private bool GetFileName(DirectoryInfo folder, string fileName)
        {
            bool flag = false;
            FileInfo[] fileInfo = folder.GetFiles();
            foreach (FileInfo NextFile in fileInfo)
            {
                if (NextFile.Name.ToLower() == fileName.ToLower())
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        private void cmbCustomerBankAccountNo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #region 导入开票系统信息
        private void barImportTaxInfo_ItemClick(object sender, ItemClickEventArgs e)
        {
            ImportTaxInfoPart importTax = new ImportTaxInfoPart();
            PartLoader.ShowDialog(importTax, "导入税控数据");
        }
        #endregion

        #region 设置快递号
        private void barExpressNo_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (FAMUtility.GuidIsNullOrEmpty(_CurrentData.ID))
            {
                return;
            }
            SetExpressPart expressPart = Workitem.Items.AddNew<SetExpressPart>();
            InvoiceList list = new InvoiceList();
            list.ID = _CurrentData.ID;
            list.InvoiceNo = _CurrentData.InvoiceNo;
            list.No = _CurrentData.No;
            list.ExpressDate = _CurrentData.ExpressDate;
            list.ExpressNo = _CurrentData.ExpressNo;
            list.UpdateDate = _CurrentData.UpdateDate;


            expressPart.DataSource = list;
            string title = LocalData.IsEnglish ? "Set Express" : "设置快递信息";
            expressPart.Saved += delegate(object[] prams)
            {
                if (prams != null && prams.Length > 0)
                {
                    InvoiceList data = prams[0] as InvoiceList;
                    if (data != null)
                    {
                        _CurrentData.ExpressDate = data.ExpressDate;
                        _CurrentData.ExpressNo = data.ExpressNo;
                        _CurrentData.UpdateDate = data.UpdateDate;
                        bindingSource1.ResetBindings(false);
                    }
                }
            };

            PartLoader.ShowDialog(expressPart, title);
        }
        #endregion

        private void btnCustomerSync_ItemClick(object sender, ItemClickEventArgs e)
        {
            string xmlFileName = Application.StartupPath + "\\CustomerCode.xml";
            try
            {
                Cursor = Cursors.WaitCursor;
                //禁用鼠标
                SetMouseDeviceState(false);

                #region 删除同步XML文件

                ////string xmlFileName = Application.StartupPath + "\\CustomerCode.xml";
                //if (File.Exists(xmlFileName))
                //{
                //    File.Delete(xmlFileName);
                //}

                #endregion

                #region 启动数据同步服务
                bool flag = false;
                Process[] allProcess = Process.GetProcesses();
                foreach (Process p in allProcess)
                {

                    if (p.ProcessName.ToLower() == "invoicessync")
                    {
                        for (int i = 0; i < p.Threads.Count; i++)
                            p.Threads[i].Dispose();
                        p.Kill();
                        //break;
                        flag = true;
                    }
                }
                if (flag)
                    RunDosCommand("net stop InvoicesSyncService");
                RunDosCommand("net start InvoicesSyncService");
                //Thread.Sleep(200);

                #endregion

                #region 同步客户资料

                DateTime updateTime = Convert.ToDateTime("2012-01-01 00:00:00");//更新时间
                if (ClientConfig.Current.Contains(InvoiceCommandConstants.UpdateTime))
                    updateTime = Convert.ToDateTime(ClientConfig.Current.GetValue(InvoiceCommandConstants.UpdateTime));
                else
                {
                    ClientConfig.Current.AddValue(InvoiceCommandConstants.UpdateTime, "2012-01-01 00:00:00");
                }
                //ExeConfigurationFileMap mapSync = new ExeConfigurationFileMap();
                //mapSync.ExeConfigFilename = Application.StartupPath + "\\InvoicesSync.exe.config";
                //Configuration configSync = ConfigurationManager.OpenMappedExeConfiguration(mapSync, ConfigurationUserLevel.None);
                //int recordCount = int.Parse(configSync.AppSettings.Settings["RecordCount"].Value); //同步数据总数

                #region 打开税控开发票界面

                string mainFormClass = "TMainForm";
                string mainFormName = "增值税防伪税控系统防伪开票子系统";
                IntPtr mainFormWnd = new IntPtr(0);
                mainFormWnd = FindWindow(mainFormClass, mainFormName);
                if (mainFormWnd == IntPtr.Zero)
                {
                    mainFormName = "增值税防伪税控系统开票子系统";
                    mainFormWnd = FindWindow(mainFormClass, mainFormName);
                    if (mainFormWnd == IntPtr.Zero)
                    {

                        XtraMessageBox.Show("请先登陆增值税防伪税控系统防伪开票子系统");
                        return;
                    }
                }
                //使主界面置前
                bool isTop = ForceForegroundWindow(mainFormWnd);
                if (isTop == false)
                    return;

                //切换到系统设置界面
                keybd_event(18, MapVirtualKey(18, 0), 0, 0); //按下ALT鍵。　　　 
                keybd_event(70, MapVirtualKey(70, 0), 0, 0); //按下f鍵。　　 
                keybd_event(70, MapVirtualKey(70, 0), 0x2, 0);//松开f鍵。
                keybd_event(18, MapVirtualKey(18, 0), 0x2, 0);//松开ALT鍵。 　
                keybd_event(90, MapVirtualKey(90, 0), 0, 0); //按下z鍵。　　 
                keybd_event(90, MapVirtualKey(90, 0), 0x2, 0);//松开z鍵。
                //切换到客户编码界面
                keybd_event(18, MapVirtualKey(18, 0), 0, 0); //按下ALT鍵。　　　 
                keybd_event(66, MapVirtualKey(66, 0), 0, 0); //按下b鍵。　　 
                keybd_event(66, MapVirtualKey(66, 0), 0x2, 0);//松开b鍵。
                keybd_event(18, MapVirtualKey(18, 0), 0x2, 0);//松开ALT鍵。 　
                keybd_event(49, MapVirtualKey(49, 0), 0, 0); //按下1鍵。　　 
                keybd_event(49, MapVirtualKey(49, 0), 0x2, 0);//松开1鍵。

                Thread.Sleep(1000);

                #endregion

                #region 获取kp客户编码列表

                if (!File.Exists(xmlFileName))
                {
                    XtraMessageBox.Show("找不到CustomerCode.xml文件，请检查文件是否存在或路径是否正确。");
                    return;
                }
                DataSet ds = new DataSet();
                ds.ReadXml(xmlFileName);
                List<CustomerCode> kpList = IListDataSet.DataSetToIList<CustomerCode>(ds, 0, "RowNumber").ToList();

                #endregion

                // ICP DATABASE
                List<CustomerInvoiceTitleInfo> list = CustomerService.GetUNICustomerInvoiceTitleList(LocalData.UserInfo.DefaultCompanyID, updateTime);
                bool isInput = false;

                int iCount = 0;
                int iAddCount = 0;     //新增记录数
                int iUpdateCount = 0;  //修改记录数
                foreach (CustomerInvoiceTitleInfo item in list)
                {
                    iCount++;
                    if (iCount % 5 == 0)
                    {
                        Thread.Sleep(5000);
                    }

                    #region KEYBDINPUT

                    //判断kp客户编码列表是否已存在记录
                    CustomerCode kpObj = kpList.Where(o => o.名称 == item.Name).SingleOrDefault();
                    if (kpObj == null)
                    {
                        // 新增记录
                        keybd_event(17, MapVirtualKey(17, 0), 0, 0); //按下Ctrl鍵。　　 
                        keybd_event(35, MapVirtualKey(35, 0), 0, 0); //按下end鍵。　　 
                        keybd_event(35, MapVirtualKey(35, 0), 0x2, 0);//松开end鍵。
                        keybd_event(17, MapVirtualKey(17, 0), 0x2, 0);//松开Ctrl鍵。
                        keybd_event(40, MapVirtualKey(40, 0), 0, 0); //按下向下鍵。　　                 
                        keybd_event(40, MapVirtualKey(40, 0), 0x2, 0);//松开向下鍵。
                        iAddCount++;
                    }
                    else
                    {
                        // 修改记录
                        for (int i = 0; i < kpObj.RowNumber; i++)
                        {
                            keybd_event(40, MapVirtualKey(40, 0), 0, 0); //按下向下鍵。　　                 
                            keybd_event(40, MapVirtualKey(40, 0), 0x2, 0);//松开向下鍵。
                        }
                        iUpdateCount++;
                    }

                    // 定位
                    if (kpList == null || kpList.Count == 0)  //客户编码表为空
                    {
                        //编码
                        foreach (short code in Encoding.Default.GetBytes("0000100010001"))
                        {
                            isInput = KeyInput.SendKey(code);
                            if (isInput == false)
                                return;
                        }
                    }

                    isInput = KeyInput.SendKey(KeyboardConstaint.VK_TAB);
                    if (isInput == false)
                        return;

                    //名称
                    //SetInputLanguage();
                    isInput = SetKeyCombination(item.Name);
                    if (isInput == false)
                        return;

                    isInput = KeyInput.SendKey(KeyboardConstaint.VK_TAB);
                    if (isInput == false)
                        return;
                    //简码
                    //isInput = KeyInput.SendKey(KeyboardConstaint.VK_SPACE);
                    isInput = KeyInput.SendKey(item.InvoiceType == CustomerInvoiceType.Dedicated ? Encoding.Default.GetBytes("Z")[0] : Encoding.Default.GetBytes("P")[0]);
                    if (isInput == false)
                        return;
                    isInput = KeyInput.SendKey(KeyboardConstaint.VK_TAB);
                    if (isInput == false)
                        return;
                    //税号
                    //isInput = SetKeyCombination(item.TaxNo);
                    //if (isInput == false)
                    //    return;
                    ////foreach (short taxNo in Encoding.Default.GetBytes(item.TaxNo))
                    ////{
                    ////    if (taxNo == 13 || taxNo == KeyboardConstaint.VK_SPACE)  //判断是否有回车符或空格字符——过滤出现两个税号的异常数据
                    ////        break;
                    ////    KeyInput.SendKey(taxNo);
                    ////}
                    if (item.TaxNo.Length == 15)
                    {
                        foreach (short taxNo in Encoding.Default.GetBytes(item.TaxNo))
                        {
                            KeyInput.SendKey(taxNo);
                        }
                    }
                    isInput = KeyInput.SendKey(KeyboardConstaint.VK_TAB);
                    if (isInput == false)
                        return;
                    //地址电话
                    isInput = KeyInput.SendKey(13);  //ENTER键
                    if (isInput == false)
                        return;
                    isInput = KeyInput.SendKey(13);  //ENTER键
                    if (isInput == false)
                        return;
                    if (kpObj != null && kpObj.地址电话.Length > 0)
                    {
                        for (int i = 0; i < kpObj.地址电话.Length; i++)  //删除原有数据
                        {
                            keybd_event(46, MapVirtualKey(46, 0), 0, 0); //按下del鍵。　　 
                            keybd_event(46, MapVirtualKey(46, 0), 0x2, 0);//松开del鍵。
                        }
                    }
                    //SetInputLanguage();
                    if (string.IsNullOrEmpty(item.AddressTel))
                        KeyInput.SendKey(KeyboardConstaint.VK_SPACE);
                    else
                        isInput = SetKeyCombination(item.AddressTel);
                    //if (isInput == false)
                    //    return;

                    keybd_event(17, MapVirtualKey(17, 0), 0, 0); //按下Ctrl鍵。　　 
                    keybd_event(13, MapVirtualKey(13, 0), 0, 0); //按下Enter鍵。　　                 
                    keybd_event(13, MapVirtualKey(13, 0), 0x2, 0);//松开Enter鍵。
                    keybd_event(17, MapVirtualKey(17, 0), 0x2, 0);//松开Ctrl鍵。
                    isInput = KeyInput.SendKey(KeyboardConstaint.VK_TAB);
                    //if (isInput == false)
                    //    return;
                    //银行账号
                    isInput = KeyInput.SendKey(13);  //ENTER键
                    if (isInput == false)
                        return;
                    isInput = KeyInput.SendKey(13);  //ENTER键
                    if (isInput == false)
                        return;
                    if (kpObj != null && kpObj.银行帐号.Length > 0)
                    {
                        for (int i = 0; i < kpObj.银行帐号.Length; i++)  //删除原有数据
                        {
                            keybd_event(46, MapVirtualKey(46, 0), 0, 0); //按下del鍵。　　 
                            keybd_event(46, MapVirtualKey(46, 0), 0x2, 0);//松开del鍵。
                        }
                    }
                    //SetInputLanguage();
                    if (string.IsNullOrEmpty(item.BankAccountNo))
                        KeyInput.SendKey(KeyboardConstaint.VK_SPACE);
                    else
                        isInput = SetKeyCombination(item.BankAccountNo);
                    //if (isInput == false)
                    //    return;

                    keybd_event(17, MapVirtualKey(17, 0), 0, 0); //按下Ctrl鍵。　　 
                    keybd_event(13, MapVirtualKey(13, 0), 0, 0); //按下Enter鍵。　　                 
                    keybd_event(13, MapVirtualKey(13, 0), 0x2, 0);//松开Enter鍵。
                    keybd_event(17, MapVirtualKey(17, 0), 0x2, 0);//松开Ctrl鍵。
                    isInput = KeyInput.SendKey(KeyboardConstaint.VK_TAB);
                    //if (isInput == false)
                    //    return;
                    //邮件地址
                    isInput = KeyInput.SendKey(KeyboardConstaint.VK_TAB);
                    if (isInput == false)
                        return;
                    //备注
                    //SetInputLanguage();
                    foreach (int gbkType in Chs2GBK(item.InvoiceType == CustomerInvoiceType.Dedicated ? "专用发票" : "普通发票"))
                    {
                        keybd_event(18, MapVirtualKey(18, 0), 0, 0); //按下ALT鍵。　　　 
                        foreach (int key in gbkType.ToString())
                        {
                            int temp = key + 48;
                            keybd_event((byte)temp, MapVirtualKey((byte)temp, 0), 0, 0);//鍵下key鍵。　　　 
                            keybd_event((byte)temp, MapVirtualKey((byte)temp, 0), 0x2, 0);//松开key鍵。
                        }
                        keybd_event(18, MapVirtualKey(18, 0), 0x2, 0);//松开ALT鍵。 　　
                    }
                    isInput = KeyInput.SendKey(KeyboardConstaint.VK_TAB);
                    if (isInput == false)
                        return;
                    //身份证校验
                    //isInput = KeyInput.SendKey(KeyboardConstaint.VK_TAB);
                    //if (isInput == false)
                    //    return;

                    // 焦点复原
                    keybd_event(36, MapVirtualKey(36, 0), 0, 0); //按下Home鍵。　　                 
                    keybd_event(36, MapVirtualKey(36, 0), 0x2, 0);//松开Home鍵。
                    keybd_event(17, MapVirtualKey(17, 0), 0, 0); //按下Ctrl鍵。　　 
                    keybd_event(36, MapVirtualKey(36, 0), 0, 0); //按下Home鍵。　　                 
                    keybd_event(36, MapVirtualKey(36, 0), 0x2, 0);//松开Home鍵。
                    keybd_event(17, MapVirtualKey(17, 0), 0x2, 0);//松开Ctrl鍵。

                    #endregion

                }
                //config.AppSettings.Settings["RecordCount"].Value = list.Count.ToString();
                //config.Save();
                ClientConfig.Current.AddValue(InvoiceCommandConstants.UpdateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                Thread.Sleep(5000);
                //启用鼠标
                SetMouseDeviceState(true);
                if (iAddCount == 0 && iUpdateCount == 0)
                    XtraMessageBox.Show("客户资料同步完成。没有可同步的客户资料。");
                else
                    XtraMessageBox.Show(string.Format("客户资料同步完成。新增{0}条记录，修改{1}条记录。", iAddCount, iUpdateCount));

                #endregion
            }
            catch (Exception ex)
            {
                #region 删除同步XML文件

                ////string xmlFileName = Application.StartupPath + "\\CustomerCode.xml";
                //if (File.Exists(xmlFileName))
                //{
                //    File.Delete(xmlFileName);
                //}

                #endregion

                #region 启动数据同步服务

                Process[] allProcess = Process.GetProcesses();
                foreach (Process p in allProcess)
                {

                    if (p.ProcessName.ToLower() == "invoicessync")
                    {
                        for (int i = 0; i < p.Threads.Count; i++)
                            p.Threads[i].Dispose();
                        p.Kill();
                        //break;
                    }
                }
                RunDosCommand("net stop InvoicesSyncService");
                RunDosCommand("net start InvoicesSyncService");
                //Thread.Sleep(200);

                #endregion

                //启用鼠标
                SetMouseDeviceState(true);
                XtraMessageBox.Show(ex.Message);
            }
            finally
            {
                #region 停止数据同步服务

                //Process[] allProcess = Process.GetProcesses();
                //foreach (Process p in allProcess)
                //{

                //    if (p.ProcessName.ToLower() == "invoicessync")
                //    {
                //        for (int i = 0; i < p.Threads.Count; i++)
                //            p.Threads[i].Dispose();
                //        p.Kill();
                //        //break;
                //    }
                //}
                //RunDosCommand("net stop InvoicesSyncService");

                //Thread.Sleep(200);

                #endregion

                #region 删除同步XML文件

                ////string xmlFileName = Application.StartupPath + "\\CustomerCode.xml";
                //if (File.Exists(xmlFileName))
                //{
                //    File.Delete(xmlFileName);
                //}

                #endregion

                //启用鼠标
                SetMouseDeviceState(true);
                Cursor = Cursors.Default;
            }
        }


        /// <summary>
        /// 设置鼠标驱动的状态（启用True/禁用False）
        /// </summary>
        /// <param name="state">状态</param>
        private void SetMouseDeviceState(bool state)
        {
            IList<HardwareHelperLib.HardwareInfo> list = HardwareHelperLib.GetHardwareTable();
            foreach (HardwareHelperLib.HardwareInfo item in list)
            {
                if (item.DeviceName.IndexOf("mouse") != -1)
                {
                    item.SetEnabled(state);
                    break;
                }
            }
        }

        private void SetInputLanguage()
        {
            for (int i = 0; i < InputLanguage.InstalledInputLanguages.Count; i++)
            {
                if (InputLanguage.InstalledInputLanguages[i].LayoutName.IndexOf("中文") != -1)
                {
                    InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[i];
                    break;
                }
            }
        }

        private int[] Chs2GBK(string chs)
        {
            char[] cArr = chs.ToCharArray();
            byte[] b = Encoding.Default.GetBytes(cArr);
            int len = b.Length / 2;
            int[] iArr = new int[len];
            for (int i = 0; i < len; i++)
            {
                iArr[i] = b[2 * i] * 0x100 + b[2 * i + 1];
            }
            return iArr;
        }

        short isCapital = KeyInput.GetKeyState(KeyboardConstaint.VK_CAPITAL);  //是否大写锁定--0小写，1大写

        /// <summary>
        /// 组合键控制
        /// </summary>
        /// <param name="sValue">包含需要使用组合键的字符串</param>
        private bool SetKeyCombination(string sValue)
        {
            bool flag = true;
            try
            {
                for (int i = 0; i < sValue.Trim().Length; i++)
                {
                    //char c = sValue[i];
                    switch (sValue[i])
                    {
                        case '(':
                        case '（':
                            keybd_event(16, MapVirtualKey(16, 0), 0, 0); //按下Shift鍵。　　 
                            keybd_event(57, MapVirtualKey(57, 0), 0, 0); //按下9鍵。　　 
                            keybd_event(57, MapVirtualKey(57, 0), 0x2, 0);//松开9鍵。
                            keybd_event(16, MapVirtualKey(16, 0), 0x2, 0);//松开Shift鍵。
                            break;
                        case ')':
                        case '）':
                            keybd_event(16, MapVirtualKey(16, 0), 0, 0); //按下Shift鍵。　　 
                            keybd_event(48, MapVirtualKey(48, 0), 0, 0); //按下0鍵。　　 
                            keybd_event(48, MapVirtualKey(48, 0), 0x2, 0);//松开0鍵。
                            keybd_event(16, MapVirtualKey(16, 0), 0x2, 0);//松开Shift鍵。
                            break;
                        case ':':
                            keybd_event(16, MapVirtualKey(16, 0), 0, 0); //按下Shift鍵。　　 
                            keybd_event(186, MapVirtualKey(186, 0), 0, 0); //按下:鍵。　　 
                            keybd_event(186, MapVirtualKey(186, 0), 0x2, 0);//松开:鍵。
                            keybd_event(16, MapVirtualKey(16, 0), 0x2, 0);//松开Shift鍵。
                            break;
                        default:
                            if (Regex.IsMatch(sValue[i].ToString(), @"[\u4e00-\u9fbb]+$"))  //判断是否汉字
                            {
                                foreach (int item in Chs2GBK(sValue[i].ToString()))  //汉字转GBK十进制
                                {
                                    keybd_event(18, MapVirtualKey(18, 0), 0, 0); //按下ALT鍵。　　　 
                                    foreach (int key in item.ToString())
                                    {
                                        int temp = key + 48;
                                        keybd_event((byte)temp, MapVirtualKey((byte)temp, 0), 0, 0);//鍵下key鍵。　　　 
                                        keybd_event((byte)temp, MapVirtualKey((byte)temp, 0), 0x2, 0);//松开key鍵。
                                    }
                                    keybd_event(18, MapVirtualKey(18, 0), 0x2, 0);//松开ALT鍵。 　　
                                }
                            }
                            else
                            {
                                short item = KeyboardConstaint.SetVirtualKeyCode(sValue[i]);
                                if (item == 0)
                                {
                                    item = Encoding.Default.GetBytes(sValue[i].ToString())[0];
                                    if (item >= 97 && item <= 122)  //a~z
                                    {
                                        if (isCapital == 0)
                                            flag = KeyInput.SendKey((short)(Convert.ToInt16(item) - 32));
                                        else
                                        {
                                            keybd_event(16, MapVirtualKey(16, 0), 0, 0); //按下Shift鍵。　　 
                                            flag = KeyInput.SendKey((short)(Convert.ToInt16(item) - 32));
                                            keybd_event(16, MapVirtualKey(16, 0), 0x2, 0);//松开Shift鍵。                                        
                                        }
                                    }
                                    else if (item >= 65 && item <= 90)  //A~Z
                                    {
                                        if (isCapital == 0)
                                        {
                                            keybd_event(16, MapVirtualKey(16, 0), 0, 0); //按下Shift鍵。　　 
                                            flag = KeyInput.SendKey(item);
                                            keybd_event(16, MapVirtualKey(16, 0), 0x2, 0);//松开Shift鍵。                                        
                                        }
                                        else
                                            flag = KeyInput.SendKey(item);
                                    }
                                    else
                                        flag = KeyInput.SendKey(item);
                                }
                                else
                                    flag = KeyInput.SendKey(item);
                            }
                            break;
                    }
                    if (flag == false)
                        return false;
                }
                return flag;
            }
            catch
            {
                return false;
            }
        }

    }

    /// <summary>
    /// 发票信息日志
    /// </summary>
    public class InvoiceLogHelper
    {
        public static void SaveLogInfo(string message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\InvoiceLogs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\" + DateTime.Now.ToString("yyyyMM") + "操作日志.txt";
            string str = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ":  " + message + Environment.NewLine;

            StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8);
            sw.Write(str);
            sw.Close();
        }
        public static void SaveAmountLogInfo(string message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\InvoiceLogs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\" + DateTime.Now.ToString("yyyyMM") + "金额日志.txt";
            string str = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ":  " + message + Environment.NewLine;

            StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8);
            sw.Write(str);
            sw.Close();
        }
    }
    public class SHTaxData
    {
        public string customerName;
        public string customerTaxNo;
        public string customerTel;
        public string customerBank;
        public string systemNo;
        public int bankAccountNoIndex;
        public string Remark;
        public decimal Amount;
        public string formName;

    }

    public class NBTaxData
    {
        public string customerName;
        public string customerTaxNo;
        public string customerTel;
        public string customerBank;
        public string systemNo;
        public int bankAccountNoIndex;
        public string Remark;
        public decimal Amount;
        public string formName;
        public bool isUSD;

    }
}
