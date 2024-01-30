using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.Client;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.UI.Bill;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Extender;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ICP.FAM.UI.WriteOff
{
    [ToolboxItem(false)]
    public partial class WriteOffEditPart : BaseEditPart
    {
        #region 服务
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 
        /// </summary>
        IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        IReportViewService ReportViewService
        {
            get
            {
                return ServiceClient.GetClientService<IReportViewService>();
            }

        }
        /// <summary>
        /// 
        /// </summary>
        IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        RateHelper RateHelper
        {
            get
            {
                return ClientHelper.Get<RateHelper, RateHelper>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        IDataFinderFactory DataFinderFactory
        {
            get
            {
                return ServiceClient.GetClientService<IDataFinderFactory>();
            }
        }

        #endregion

        #region 本地成员
        /// <summary>
        /// 操作口岸名称
        /// </summary>
        public string CompanyName = string.Empty;
        /// <summary>
        /// 费用方向
        /// </summary>
        FeeWay _feeWay = FeeWay.AR;
        /// <summary>
        /// 当前销账
        /// </summary>
        WriteOffItemInfo writeOffItemInfo = new WriteOffItemInfo();
        /// <summary>
        /// 初始化值字典
        /// </summary>
        IDictionary<string, object> _values;
        /// <summary>
        /// 账单币种与总计信息
        /// </summary>
        Dictionary<Guid, Decimal> dicBillList = new Dictionary<Guid, decimal>();
        /// <summary>
        /// 币种ID与币种名称信息
        /// </summary>
        Dictionary<Guid, String> currencyList = new Dictionary<Guid, String>();
        /// <summary>
        /// 当前公司下的汇率信息
        /// </summary>
        List<SolutionExchangeRateList> _rateList = new List<SolutionExchangeRateList>();
        /// <summary>
        /// 
        /// </summary>
        List<SolutionCurrencyList> _currencyList = new List<SolutionCurrencyList>();
        /// <summary>
        /// 是否由账单/对账单 加载该界面
        /// </summary>
        private bool isBillLoad = false;
        /// <summary>
        /// 是否从销帐列表打开
        /// </summary>
        private bool _isFromWriteoffList = false;
        /// <summary>
        /// 账单列表选择的币种集合
        /// </summary>
        private List<Guid> BillCurrencyIDList = new List<Guid>();
        /// <summary>
        /// 支付币种(单币种销账时,验证所有账单是否都按同一币种支付时使用)
        /// </summary>
        private Guid PayCurrencyID = Guid.Empty;
        /// <summary>
        /// 搜索器
        /// </summary>
        private IDisposable customerFinder, bankTransactionFinder, bankReceiptFinder;
        /// <summary>
        /// 
        /// </summary>
        public override event SavedHandler Saved;
        #endregion

        #region 属性

        #region 数据源
        /// <summary>
        /// 数据源
        /// </summary>
        public override object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }
        #endregion

        #region 关闭时保存提示
        private bool isCharge = false;
        /// <summary>
        /// 是否有数据发生改变
        /// </summary>
        public bool IsChanged
        {
            get
            {
                if (isCharge)
                {
                    return true;
                }
                if (writeOffItemInfo.IsDirty)
                {
                    return true;
                }
                if (UCAccountListInfo.IsChanged)
                {
                    return true;
                }
                if (UCcharges.IsChanged)
                {
                    return true;
                }

                return false;
            }
        }

        #endregion

        #region 传过来的销帐类型
        /// <summary>
        /// 传过来的销帐类型
        /// </summary>
        private WriteOffType writeOffType { get; set; }
        #endregion

        #region 从对账单中传过来的公司ID
        private Guid companyID;
        /// <summary>
        /// 从对账单中传过来的公司ID
        /// </summary>
        public Guid CompanyID
        {
            get { return companyID; }
            set
            {
                companyID = value;
                UCcharges.CompanyID = value;
            }
        }
        #endregion

        #region 银行信息中选择的币种ID
        /// <summary>
        /// 银行信息中选择的币种ID
        /// </summary>
        public Guid AccountCurrencyID
        {
            get;
            set;
        }
        #endregion

        #region 本位币
        private Guid standardCurrencyID;
        /// <summary>
        /// 本位币
        /// </summary>
        public Guid StandardCurrencyID
        {
            get
            {
                if (DataTypeHelper.GetGuid(standardCurrencyID, Guid.Empty) == Guid.Empty)
                {
                    ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(CompanyID);
                    standardCurrencyID = configureInfo.StandardCurrencyID;
                }
                return standardCurrencyID;
            }
            set
            {
                standardCurrencyID = value;
            }
        }
        #endregion

        #region 账单/费用当前选择行
        /// <summary>
        /// 账单/费用当前选择行
        /// </summary>
        private WriteOffBill writeOffBill
        {
            get
            {
                return bsBills.Current as WriteOffBill;
            }
        }
        #endregion

        #region 账单原始数据源
        private List<WriteOffBill> baseDataList;
        /// <summary>
        /// 账单原始数据源
        /// </summary>
        private List<WriteOffBill> BaseDataList
        {
            get
            {
                if (baseDataList == null)
                {
                    baseDataList = new List<WriteOffBill>();
                }
                return baseDataList;
            }
            set
            {
                baseDataList = value;
            }
        }
        #endregion

        #region 账单当前数据源
        /// <summary>
        /// 账单当前数据源
        /// </summary>
        private List<WriteOffBill> DataList
        {
            get
            {
                List<WriteOffBill> list = bsBills.DataSource as List<WriteOffBill>;
                if (list == null)
                {
                    list = new List<WriteOffBill>();
                }
                return list;
            }
        }
        #endregion

        #region 账单中定义的的汇率列表
        /// <summary>
        /// 账单中定义的的汇率列表
        /// </summary>
        private List<CurrencyRateData> CurrencyRateList
        {
            get;
            set;
        }
        #endregion
        #endregion

        #region 初始化
        /// <summary>
        /// 销账编辑
        /// </summary>
        public WriteOffEditPart()
        {

            InitializeComponent();

            Closing += delegate
            {
                txtCheckNo.Focus();
            };
            Disposed += delegate
            {
                if (customerFinder != null)
                {
                    customerFinder.Dispose();
                    customerFinder = null;
                }
                if (bankTransactionFinder != null)
                {
                    bankTransactionFinder.Dispose();
                    bankTransactionFinder = null;
                }
                if (bankReceiptFinder != null)
                {
                    bankReceiptFinder.Dispose();
                    bankReceiptFinder = null;
                }
                SmartPartClosing -= WriteOffEditPart_SmartPartClosing;
                txtPayCustomerName.SelectChanged -= txtPayCustomerName_SelectChanged;
                UCAccountListInfo.BankAccountSelected -= UCAccountListInfo_BankAccountSelected;
                UCAccountListInfo.Selected -= UCAccountListInfo_Selected;
                UCAccountListInfo.TotalAmountUnequal -= UCAccountListInfo_TotalAmountUnequal;
                _rateList = null;
                _values = null;
                gvTreeGridMain.CellValueChanged -= gvTreeGridMain_CellValueChanged;
                gvTreeGridMain.DataSource = null;
                bsBills.PositionChanged -= bsBills_PositionChanged;
                bsBills.DataSource = null;
                bsBills.Dispose();
                dicBillList = null;
                currencyList = null;
                Saved = null;
                cmbCompany.EditValueChanged -= cmbCompany_EditValueChanged;
                cmbType.SelectedIndexChanged -= cmbType_SelectedIndexChanged;
                dteWriteDate.EditValueChanged -= CheckDate_Changed;
                UCcharges.Selected -= UCcharges_Selected;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };

            UCcharges.AccountListInfo = UCAccountListInfo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                base.OnLoad(e);

                if (!DesignMode)
                {
                    WriteOffEditPart_Load();
                    SmartPartClosing += WriteOffEditPart_SmartPartClosing;
                    ActivateSmartPartClosingEvent(Workitem);
                }
            }
        }

        private void WriteOffEditPart_Load()
        {
            if (!DesignMode)
            {
                InitMessage();

                gvTreeGridMain.ShowTreeGridRowNo(50);
                #region 加载其他项目面板
                UCcharges.SetService(Workitem);
                UCcharges.Selected += UCcharges_Selected;
                UCcharges.FeeWay = _feeWay;
                UCcharges.CheckDate = writeOffItemInfo.CheckDate;
                #endregion

                #region 加载银行面板
                UCAccountListInfo.SetService(Workitem);
                UCAccountListInfo.InitControls();
                UCAccountListInfo.CheckDate = writeOffItemInfo.CheckDate;
                UCAccountListInfo.BankAccountSelected += UCAccountListInfo_BankAccountSelected;
                UCAccountListInfo.TotalAmountUnequal += UCAccountListInfo_TotalAmountUnequal;
                
                #endregion

                #region 加载其他
                financeChargesTools1.SetService(Workitem);

                InitControls();
                SetFormText();
                UCcharges.InitControls();

                financeChargesTools1.SetService(Workitem);

                ShowChargeNameColumn();
                ConfigureInfo companyConfig = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
                List<SolutionGLConfigList> sList = ConfigureService.GetSolutionGLConfigList(companyConfig.SolutionID, true);

                if (sList.Count(r => r.GLConfigTypeID == 5) > 0)
                {
                    glId = sList.Find(r => r.GLConfigTypeID == 5).CRGLCodeID;
                }

                #endregion

                #region 由账单列表加载该界面时,显示汇率调整界面，以及加入账号信息

                BillLoad();

                #endregion

                #region 统计
                TotalCurrencyAndAmount(isBillLoad, true);
                UCcharges.TotalChargeAmount();
                #endregion

                #region 更新保存状态
                AfterSave();
                UCAccountListInfo.AfterSave();
                UCcharges.AfterSave();
                #endregion

                #region 销账时间发生改变
                dteWriteDate.EditValueChanged += CheckDate_Changed;
                #endregion

                RefreshControl();
            }
        }

        private void WriteOffEditPart_SmartPartClosing(object sender, WorkspaceCancelEventArgs e)
        {
            if (IsChanged && barWriteOff.Enabled)
            {
                DialogResult dr = FAMUtility.EnquireIsSaveCurrentDataByUpdated();

                if (dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (dr == DialogResult.Yes)
                {
                    if (!Save())
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        /// <summary>
        /// 银行帐号发生改变时,更新汇率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        void UCAccountListInfo_BankAccountSelected(object sender, object data)
        {
            if (data == null && string.IsNullOrEmpty(data.ToString()))
            {
                return;
            }
            Guid currencyID = new Guid(data.ToString());
            AccountCurrencyID = currencyID;
            UCcharges.AccountCurrencyID = currencyID;

            //SetBaseDataListRate();
            //this.UCcharges.SetBankAccountRate(writeOffType, currencyID, true);
            TotalCurrencyAndAmount(true, true);
        }

        /// <summary>
        /// 销账日期发生改变时，重新计算本位币的汇率
        /// </summary>
        private void CheckDate_Changed(object sender, EventArgs e)
        {
            UCAccountListInfo.CheckDate = writeOffItemInfo.CheckDate;
            UCcharges.CheckDate = writeOffItemInfo.CheckDate;
        }
        /// <summary>
        /// 单币种销帐，帐单币种和销帐币种有不同时，若金额不平，自动结转成汇兑损益(By Pearl)  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void UCAccountListInfo_TotalAmountUnequal(object sender, object data)
        {
            bool isDone = false;
            if (UCcharges.DataList != null)
            {
                foreach (var chargeItem in UCcharges.DataList)
                {
                    if (chargeItem.CurrencyID != AccountCurrencyID)
                    {
                        ChangeToCharge();
                        isDone = true;
                        break;
                    }
                }
            }

            if (!isDone)
            {
                foreach (var item in DataList)
                {
                    if (item.CurrencyID != AccountCurrencyID)
                    {
                        ChangeToCharge();
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 由账单列表加载时
        /// </summary>
        private void BillLoad()
        {
            if (!isBillLoad)
            {
                return;
            }

            //加入一条账号纪录
            List<OperationCurrencyAmountList> list = new List<OperationCurrencyAmountList>();
            if (currencyList == null)
            {
                BillCurrencyIDList = new List<Guid>();
            }
            foreach (Guid currencyID in BillCurrencyIDList)
            {
                OperationCurrencyAmountList accountInfo = new OperationCurrencyAmountList();
                accountInfo.SourceCurrencyID = currencyID;
                accountInfo.CurrencyID = currencyID;
                list.Add(accountInfo);
            }

            UCAccountListInfo.DataSource = list;

            if (writeOffType == WriteOffType.Single)
            {
                UCAccountListInfo.radCurrencyType.SelectedIndex = 0;

                if (PayCurrencyID != Guid.Empty)
                {
                    Workitem.Commands[WriteOffCommands.Command_SetCurrencyRate].Execute();
                }
            }
            else
            {
                UCAccountListInfo.radCurrencyType.SelectedIndex = 1;
            }
        }



        /// <summary>
        /// 根据收/付类型来设置标签
        /// </summary>
        private void SetFormText()
        {
            if (_feeWay == FeeWay.AR)
            {
                lblPrincipleTitle.Text = "实际付款人";
                labPayBankAccountNo.Text = "实际付款账号";
                labPayBankName.Text = "实际付款银行";
                labPayBankBranchName.Text = "实际付款支行";

            }
            else
            {
                // this.bgBase.Caption = "付款信息";
                lblPrincipleTitle.Text = "实际收款人";
                labPayBankAccountNo.Text = "实际收款账号";
                labPayBankBranchName.Text = "实际收款支行";
            }

        }

        
        /// <summary>
        /// 注册搜索器
        /// </summary>
        private void SearchRegister()
        {
            #region 客户搜索器

            //客户搜索器
            customerFinder = DataFindClientService.Register(txtCustomer, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName,
                  SearchFieldConstants.CustomerResultValue,
                        delegate(object inputSource, object[] resultData)
                        {
                            txtCustomer.Tag = txtPayCustomerName.CustomerID= writeOffItemInfo.CustomerID = new Guid(resultData[0].ToString());
                            txtCustomer.EditValue = writeOffItemInfo.CustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                            SetChargeCustomerInfo();
                        }, delegate
                        {
                            txtCustomer.Text = string.Empty;
                            txtCustomer.Tag = Guid.Empty;
                        },
                        ClientConstants.MainWorkspace);
            //客户银行
            txtPayCustomerName.SelectChanged+=txtPayCustomerName_SelectChanged;
            //银行流水
            //bankTransactionFinder = DataFindClientService.Register(txtBankTransaction, FAMFinderConstants.BankTransactionFinder, SearchFieldConstants.CodeName,
            //      SearchFieldConstants.ResultValueBankTransaction, GetBankTransactionCondition,
            //            delegate (object inputSource, object[] resultData)
            //            {
            //                txtBankTransaction.Tag = writeOffItemInfo.BankTransactionID= new Guid(""+resultData[0]);
            //                txtBankTransaction.Text = writeOffItemInfo.BankTransactionNO= ""+resultData[1];

            //                BankTransaction = new BankTransactionInfo
            //                {
            //                    ID = new Guid("" + resultData[0]),
            //                    TransactionAmount = Convert.ToDecimal("" + resultData[2]),
            //                    OperationDateTime = Convert.ToDateTime(""+ resultData[3]),
            //                };
            //                UCAccountListInfo.BankTransaction = BankTransaction;
            //                TotalCurrencyAndAmount(true, true);
            //            }, delegate
            //            {
            //                BankTransaction = null;
            //                txtBankTransaction.Text = string.Empty;
            //                txtBankTransaction.Tag = Guid.Empty;
            //            },
            //            ClientConstants.MainWorkspace);
            //银行水单
            bankReceiptFinder = DataFindClientService.Register(txtBankReceipt, FAMFinderConstants.BankReceiptFinder, SearchFieldConstants.CodeName,
                  SearchFieldConstants.BusinessResultValue, GetBankReceiptCondition,
                        delegate (object inputSource, object[] resultData)
                        {
                            txtBankReceipt.Tag = writeOffItemInfo.BankReceiptID= new Guid(resultData[0].ToString());
                            txtBankReceipt.Text = writeOffItemInfo.BankReceiptNO= resultData[1].ToString();
                        }, delegate
                        {
                            txtBankReceipt.Text = string.Empty;
                            txtBankReceipt.Tag = Guid.Empty;
                        },
                        ClientConstants.MainWorkspace);
            #endregion


        }
        /// <summary>
        /// 获取银行交易查询条件
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetBankTransactionCondition()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("CompanyID", cmbCompany.EditValue, false);
            if (!UCAccountListInfo.CurrentRow.BankAccountID.IsNullOrEmpty())
            {
                conditions.AddWithValue("BankAccountID", UCAccountListInfo.CurrentRow.BankAccountID, false);
            }
            conditions.AddWithValue("DebitCreditFlag", _feeWay == FeeWay.AP ? "D" : "C", false);
            return conditions;
        }
        /// <summary>
        /// 获取银行水单查询条件
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetBankReceiptCondition()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("CompanyID", cmbCompany.EditValue, false);
            return conditions;
        }
        #endregion

        #region 选取/删除 账单
        /// <summary>
        /// 选取帐单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(WriteOffCommands.Command_AutoBillsFinde)]
        public void bbiAutoBillsFinde(object sender, EventArgs e)
        {
            string customerID = string.Empty;
            if (writeOffItemInfo == null)
                return;
            customerID = writeOffItemInfo.CustomerID.ToString();
            SearchConditionCollection searchList = new SearchConditionCollection();

            searchList.AddWithValue("CustomerID", customerID, true);
            searchList.AddWithValue("CompanyID", writeOffItemInfo.CompanyID, true);
            searchList.AddWithValue("CompanyName", writeOffItemInfo.CompanyName, true);
            searchList.AddWithValue("IsValidateCustomer", true, true);
            searchList.AddWithValue("IsValidateCompany", false, true);
            searchList.AddWithValue("CustomerName", txtCustomer.Text, false);
            searchList.AddWithValue("Way", writeOffItemInfo.Way, true);
            searchList.AddWithValue("CheckStatus", BillSearchWriteOffStatue.UnWriteOff, true);

            IDataFinder finder = DataFinderFactory.GetDataFinder(FAMFinderConstants.BillFinder);
            finder.DataChoosed += billFinder_DataChoosed;
            finder.PickMany(
                CommonFinderConstants.CustoemrFinder,
                SearchFieldConstants.CodeName,
                searchList,
                null,
                FinderTriggerType.ClickButton,
                null,
                ClientConstants.MainWorkspace);
        }
        private void SetChargeCustomerInfo()
        {
            UCcharges.CustomerID = writeOffItemInfo.CustomerID;
            UCcharges.CustomerName = writeOffItemInfo.CustomerName;
        }
        void billFinder_DataChoosed(object sender, DataFindEventArgs e)
        {
            CurrencyBillList[] bills = e.Data as CurrencyBillList[];

            if (bills != null && bills.Length > 0)
            {
                if (txtCustomer.Tag == null || (Guid)txtCustomer.Tag == Guid.Empty)
                {
                    txtCustomer.Tag = txtPayCustomerName.CustomerID=writeOffItemInfo.CustomerID = bills[0].CustomerID;
                    txtCustomer.Text = writeOffItemInfo.CustomerName = bills[0].CustomerName;

                    SetChargeCustomerInfo();
                }

                //刷新发票号
                foreach (var item in bills)
                {
                    if (!string.IsNullOrEmpty(item.InvoiceNo) && (string.IsNullOrEmpty(writeOffItemInfo.InvoiceNo) || !writeOffItemInfo.InvoiceNo.Contains(item.InvoiceNo)))
                    {
                        if (!string.IsNullOrEmpty(writeOffItemInfo.InvoiceNo))
                        {
                            writeOffItemInfo.InvoiceNo += ", ";
                        }

                        writeOffItemInfo.InvoiceNo += item.InvoiceNo;
                    }
                }

                txtInvoiceNo.Text = writeOffItemInfo.InvoiceNo;
            }

            BindSelectBillList(bills, true);
        }
        /// <summary>
        /// 删除账单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(WriteOffCommands.Command_DeleteBills)]
        public void DeleteBillsFinde(object sender, EventArgs e)
        {
            labelControl1.Focus();
            gvTreeGridMain.EndUpdate();
            bsBills.EndEdit();
            
            List<WriteOffBill> oriList = bsBills.DataSource as List<WriteOffBill>;
            if (oriList == null) return;
            TreeListMultiSelection listss = gvTreeGridMain.Selection;
            List<WriteOffBill> selectList = (from TreeListNode bill in listss select oriList[bill.Id]).ToList();

            if (selectList.Count == 0)
            {
                XtraMessageBox.Show(LocalData.IsEnglish ? "Please select the data you want to delete." : "请选择要删除的数据.",
                                                            LocalData.IsEnglish ? "Tip" : "提示",
                                                            MessageBoxButtons.OK,
                                                            MessageBoxIcon.Warning);
                return;
            }


            bool isPrepayment = false;
            List<Guid> chargeIDs = new List<Guid>();
            #region 如果个费用有对应的预收预付数据，询问是否继续删除
            foreach (WriteOffBill wob in selectList)
            {
                if (cmbType.SelectedIndex <= 0)
                {
                    //账单模式
                    chargeIDs = (from d in baseDataList where d.BillID == wob.BillID && d.Way == wob.Way && d.CurrencyID == wob.CurrencyID && d.IsCommission == wob.IsCommission select d.ChargeID).ToList();
                }
                else
                {
                    // 费用模式
                    chargeIDs = (from d in baseDataList where d.BillID == wob.BillID && d.Way == wob.Way && d.CurrencyID == wob.CurrencyID && d.ChargingCodeID == wob.ChargingCodeID select d.ChargeID).ToList();
                }
                int count = (from d in UCcharges.DataList where chargeIDs.Contains(DataTypeHelper.GetGuid(d.RefID, Guid.Empty)) select d).Count();
                if (count > 0)
                {
                    isPrepayment = true;
                }
            }

            if (isPrepayment)
            {
                DialogResult result = XtraMessageBox.Show(NativeLanguageService.GetText(this, "1412180001"),
                                                 LocalData.IsEnglish ? "Tip" : "提示",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    UCcharges.RemovePrepaymentData(chargeIDs);
                }
                else
                {
                    return;
                }
            }
            #endregion

            #region 删除
            if (!isPrepayment)
            {
                //没有预收预付的，才弹出确认删除的提示
                if (!FAMUtility.EnquireIsDeleteCurrentData())
                {
                    return;
                }
            }


            if (cmbType.SelectedIndex <= 0)
            {
                #region 账单模式

                foreach (WriteOffBill wob in selectList)
                {
                    List<WriteOffBill> list = baseDataList.FindAll(delegate(WriteOffBill item) { return item.BillID == wob.BillID && item.Way == wob.Way && item.CurrencyID == wob.CurrencyID && item.IsCommission == wob.IsCommission; });

                    foreach (WriteOffBill item in list)
                    {
                        if (baseDataList.Contains(item))
                        {
                            baseDataList.Remove(item);
                        }
                    }
                }
                #endregion
            }
            else
            {
                #region 费用模式
                foreach (WriteOffBill wob in selectList)
                {
                    List<WriteOffBill> list = baseDataList.FindAll(delegate(WriteOffBill item) { return item.BillID == wob.BillID && item.Way == wob.Way && item.CurrencyID == wob.CurrencyID && item.ChargingCodeID == wob.ChargingCodeID; });

                    foreach (WriteOffBill item in list)
                    {
                        if (baseDataList.Contains(item))
                        {
                            baseDataList.Remove(item);
                        }
                    }
                }
                #endregion
            }

            isCharge = true;
            foreach (WriteOffBill wob in selectList)
            {
                oriList.Remove(wob);
            }

            bsBills.DataSource = oriList;

            bsBills.ResetBindings(true);

            if (UCAccountListInfo.radCurrencyType.Enabled)
            {
                int currencyCount = (from d in BaseDataList group d by d.CurrencyID into g select g.Key).Count();
                UCAccountListInfo.CurrencyType = currencyCount > 1 ? WriteOffType.Muitl : WriteOffType.Single;
            }

            TotalCurrencyAndAmount(true, true);


            #endregion
        }

        #endregion

        #region 统计币种与金额

        /// <summary>
        /// 财务的费用发生改变时，更新币种信息列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void UCcharges_Selected(object sender, object data)
        {
            TotalCurrencyAndAmount(true, DataTypeHelper.GetBool(data, true));
        }
        /// <summary>
        /// 账单的金额发生改变时，更新账号信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvTreeGridMain_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column != colWriteOffAmount) return;
            if (writeOffBill == null)
            {
                return;
            }
            if (writeOffBill.ExchangeRate <= 0)
            {
                writeOffBill.ExchangeRate = 1;
            }
            UpdateListData();
        }
        /// <summary>
        /// 更新Guid数据
        /// </summary>
        private void UpdateListData()
        {
            //更新费用中的销帐信息
            AmountChange(writeOffBill.WriteOffAmount);

            //先找出当前数据的所有费用信息
            if (cmbType.SelectedIndex <= 0)
            {
                //账单
                List<WriteOffBill> list = (from d in BaseDataList
                                           where
                                               d.BillID == writeOffBill.BillID &&
                                               d.CurrencyID == writeOffBill.CurrencyID &&
                                               d.Way == writeOffBill.Way &&
                                               d.IsCommission == writeOffBill.IsCommission &&
                                               d.AvailbeWriteOffAmount != 0m
                                           select d).ToList();

                writeOffBill.WriteOffAmount = (from d in list select d.WriteOffAmount).Sum();
                writeOffBill.StandardCurrencyAmount = (from d in list select Get2Round(d.StandardCurrencyAmount)).Sum();
            }
            else
            {
                //费用
                List<WriteOffBill> feeList = (from d in BaseDataList
                                              where
                                                  d.BillID == writeOffBill.BillID &&
                                                  d.CurrencyID == writeOffBill.CurrencyID &&
                                                  d.Way == writeOffBill.Way &&
                                                  d.IsCommission == writeOffBill.IsCommission &&
                                                  d.ChargingCodeID == writeOffBill.ChargingCodeID &&
                                                  d.AvailbeWriteOffAmount != 0m
                                              select d).ToList();

                if (feeList != null && feeList.Count == 1)
                {
                    writeOffBill.WriteOffAmount = feeList[0].WriteOffAmount;
                    writeOffBill.StandardCurrencyAmount = feeList[0].StandardCurrencyAmount;
                }
            }

            TotalCurrencyAndAmount(true, true);
            bsBills.ResetBindings(false);
            isCharge = true;
        }

        private void bsBills_PositionChanged(object sender, EventArgs e)
        {
            RefreshBillListItemEnabled();
        }

        private void RefreshBillListItemEnabled()
        {
            if (bsBills.Current == null) return;

            //账单模式，可编辑
            if (cmbType.SelectedIndex <= 0)
            {
                gvTreeGridMain.OptionsBehavior.Editable = true;
            }
            else
            {
                if (writeOffBill.AvailbeWriteOffAmount == 0m)
                {
                    //已经完全销帐的费用不能再编辑
                    gvTreeGridMain.OptionsBehavior.Editable = false;
                }
                else
                {
                    gvTreeGridMain.OptionsBehavior.Editable = true;
                }
            }
        }

        /// <summary>
        /// 更换币种
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            TotalBillAmountByCurrency();
        }

        private void cmbCompany_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbCompany.EditValue != null && cmbCompany.EditValue.ToString().Length > 0)
            {
                Guid id = new Guid(cmbCompany.EditValue.ToString());
                UCAccountListInfo.SetBankComBox(id);
                UCAccountListInfo.ResetbAankAccountID();
                UCcharges.CompanyID = id;
            }
        }

        #endregion

        #region 控件事件

        /// <summary>
        /// 销帐并打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barWriteOffAndPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Save())
            {
                PrintCheck();
            }

        }
        /// <summary>
        /// 汇率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(WriteOffCommands.Command_EditCurrencyRate)]
        public void Command_EditCurrencyRate(object sender, EventArgs e)
        {
            if (FAMUtility.GuidIsNullOrEmpty(AccountCurrencyID) || UCAccountListInfo.CurrencyType == WriteOffType.Muitl)
            {
                return;
            }

            string title = LocalData.IsEnglish ? "Charge Rate" : "汇率调整";

            CurrencyRateEditor editor = Workitem.Items.AddNew<CurrencyRateEditor>();
            editor.CurrencyID = AccountCurrencyID;
            if (currencyList.Keys.Contains(AccountCurrencyID))
            {
                editor.CurrencyName = currencyList[AccountCurrencyID];
            }

            List<Guid> currencyIDList = (from d in DataList group d by d.CurrencyID into g select g.Key).ToList();
            if (currencyIDList == null && currencyIDList.Count == 0)
            {
                return;
            }

            //List<SolutionExchangeRateList> rateDataList = (from d in _rateList where currencyIDList.Contains(d.SourceCurrencyID) && d.TargetCurrencyID == AccountCurrencyID select d).ToList();
            List<SolutionExchangeRateList> rateDataList = new List<SolutionExchangeRateList>();
            foreach (var item in currencyIDList)
            {
                SolutionExchangeRateList matchRate = null;
                matchRate = RateHelper.GetMatchSolutionExchangeRateList(item, AccountCurrencyID, DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified), _rateList, false);
                if (matchRate != null)  //默认时间约束是当前时间的汇率 -By pearl
                {
                    rateDataList.Add(matchRate);
                }
                else   //若是没找到，则显示第一条币种匹配的汇率 -By pearl
                {
                    matchRate = RateHelper.GetMatchSolutionExchangeRateList(item, AccountCurrencyID, _rateList, true);
                    if (matchRate != null)
                    {
                        rateDataList.Add(matchRate);
                    }
                }
            }

            editor.DataSourceList = rateDataList;

            if (PartLoader.ShowDialog(editor, title) == DialogResult.OK)
            {
                if (editor.DataSourceList.Count == 0)
                {
                    return;
                }
                foreach (WriteOffBill bill in DataList)
                {
                    if (bill.CurrencyID == editor.CurrencyID)
                    {
                        bill.ExchangeRate = 1;
                        bill.FinalAmount = bill.WriteOffAmount;
                        continue;
                    }
                    var item = (from r in editor.DataSourceList where bill.CurrencyID == r.SourceCurrencyID select r).SingleOrDefault();

                    if (item != null)
                    {
                        bill.ExchangeRate = Convert.ToDecimal(item.Rate.ToString("F7"));
                        bill.FinalAmount = Convert.ToDecimal((bill.WriteOffAmount * bill.ExchangeRate).ToString("F2"));
                    }

                }

                bsBills.ResetBindings(false);
            }
        }

        /// <summary>
        /// 按一种币种进行支付时，调整汇率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(WriteOffCommands.Command_SetCurrencyRate)]
        public void Command_SetCurrencyRate(object sender, EventArgs e)
        {
            if (FAMUtility.GuidIsNullOrEmpty(AccountCurrencyID) || writeOffType == WriteOffType.Muitl)
            {
                return;
            }

            string title = LocalData.IsEnglish ? "Charge Rate" : "汇率调整";

            CurrencyRateEditor editor = Workitem.Items.AddNew<CurrencyRateEditor>();
            editor.CurrencyID = AccountCurrencyID;
            if (currencyList.Keys.Contains(AccountCurrencyID))
            {
                editor.CurrencyName = currencyList[AccountCurrencyID];
            }

            List<Guid> currencyIDList = (from d in DataList group d by d.CurrencyID into g select g.Key).ToList();
            if (currencyIDList == null && currencyIDList.Count == 0)
            {
                return;
            }

            List<SolutionExchangeRateList> rateDataList =
                (from d in CurrencyRateList
                 where currencyIDList.Contains(d.CurrencyID)
                 select new SolutionExchangeRateList
                 {
                     SourceCurrencyID = d.CurrencyID,
                     SourceCurrency = d.CurrencyName,
                     Rate = d.Rate
                 }).ToList();

            if (rateDataList == null || rateDataList.Count == 0)
            {
                return;
            }

            editor.DataSourceList = rateDataList;

            if (PartLoader.ShowDialog(editor, title) == DialogResult.OK)
            {
                if (editor.DataSourceList.Count == 0)
                {
                    return;
                }
                foreach (WriteOffBill bill in DataList)
                {
                    if (bill.CurrencyID == editor.CurrencyID)
                    {
                        bill.ExchangeRate = 1;
                        bill.FinalAmount = bill.WriteOffAmount;
                        continue;
                    }
                    var item = (from r in editor.DataSourceList where bill.CurrencyID == r.SourceCurrencyID select r).SingleOrDefault();

                    if (item != null)
                    {
                        bill.ExchangeRate = Convert.ToDecimal(item.Rate.ToString("F7"));
                        bill.FinalAmount = Convert.ToDecimal((bill.WriteOffAmount * bill.ExchangeRate).ToString("F2"));
                    }

                }

                bsBills.ResetBindings(false);
            }
        }

        private void barPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                PrintCheck();
            }
        }


        /// <summary>
        /// 设置显示列
        /// </summary>
        void ShowChargeNameColumn()
        {
            colChargeName.Visible = cmbType.SelectedIndex > 0;
        }

        /// <summary>
        /// 查看发票
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bbiViewCheck_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                List<Guid> chargeIDList = (from d in BaseDataList group d by d.ChargeID into g select g.Key).ToList();

                List<InvoiceList> invoiceList = FinanceService.GetInvoiceListByFeeID(chargeIDList.ToArray());

                if (invoiceList == null || invoiceList.Count == 0)
                {
                    return;
                }
                else if (invoiceList.Count == 1)
                {
                    Guid invoice = invoiceList[0].ID;
                    InvoiceInfo info = FinanceService.GetInvoiceInfo(invoice, LocalData.IsEnglish);
                    InvoiceEditPart viewInvoice = Workitem.Items.AddNew<InvoiceEditPart>();

                    IWorkspace mainWorkspace = Workitem.Workspaces[ClientConstants.MainWorkspace];

                    viewInvoice.DataSource = info;
                    viewInvoice.ReadOnly = true;

                    SmartPartInfo smartPartInfo = new SmartPartInfo();
                    smartPartInfo.Title = LocalData.IsEnglish ? "View Invoice" : "查看发票";
                    mainWorkspace.Show(viewInvoice, smartPartInfo);
                }
                else if (invoiceList.Count > 1)
                {
                    DataPageInfo dp = new DataPageInfo();
                    dp.TotalCount = invoiceList.Count;
                    dp.CurrentPage = 1;
                    dp.PageSize = int.MaxValue;
                    PageList list = PageList.Create<InvoiceList>(invoiceList, dp);

                    BillViewInvoiceList viewInvoice = Workitem.Items.AddNew<BillViewInvoiceList>();
                    viewInvoice.DataSource = list;
                    string title = LocalData.IsEnglish ? "View Invoice" : "查看发票";
                    PartLoader.ShowDialog(viewInvoice, title);

                }
            }
        }
        /// <summary>
        /// 查看凭证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiListCredentials_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                CredentialsDetailEditor view = Workitem.Items.AddNew<CredentialsDetailEditor>();
                WriteOffItemInfo currentItemInfo = bsWriteOff.DataSource as WriteOffItemInfo;
                view.WriteOffID = currentItemInfo.ID;
                if (UCAccountListInfo.CurrentRow != null)
                {
                    view.VoucherSeqNo = UCAccountListInfo.CurrentRow.VoucherSeqNo;
                }

                view.Text = LocalData.IsEnglish ? "Credentials Detail" : "凭证明细";
                DialogResult dlg = view.ShowDialog();
            }
        }

        /// <summary>
        /// 账单/费用  类型发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbType.SelectedIndex == 0)
            {
                //this.bgFinanceCharges.Caption = "账单";
                labBill.Text = "账单";
            }
            else
            {
                // this.bgFinanceCharges.Caption = "费用";
                labBill.Text = "费用";
            }

            SetBillDataSource();
            RefreshBillListItemEnabled();
        }

        void txtPayCustomerName_SelectChanged(object sender, CommonEventArgs<CustomerBankInfo> e)
        {
            
            if (e.Data == null)
            {
                return;
            }
            CustomerBankInfo currentData = e.Data;
            DialogResult result = MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "是否覆盖当前页面数据?" : "是否覆盖当前页面数据?"
                              , LocalData.IsEnglish ? "Tip" : "提示"
                              , MessageBoxButtons.YesNo
                              );
            if (result == DialogResult.Yes)
            {
                writeOffItemInfo.PayCustomerName = txtPayCustomerName.CustomerName= currentData.AccountName;
                writeOffItemInfo.PayBankAccountNo = currentData.AccountNO;
                writeOffItemInfo.PayBankName = currentData.BankName;
                writeOffItemInfo.PayBankBranchName = currentData.BranchName;
                writeOffItemInfo.PayBankNumber = currentData.BankNumber;
                EndEdit();
                Invalidate();
            }
        }

        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            //2014-04-28 周任平修改:关闭之前于先让No这个文本框获得焦点
            //原因:如果关闭时焦点在账单列表中正处于编辑状态,导致树的结束编辑时报错,所以在关闭时,先让树失去焦点
            txtCheckNo.Focus();

            FindForm().Close();
        }

        #endregion

        #region 币种类型发生改变
        private void UCAccountListInfo_Selected(object sender, object data)
        {

            WriteOffType CurrencyType = (WriteOffType)data;

            writeOffType = CurrencyType;
            UCcharges.CurrencyType = CurrencyType;
            bsBills.ResetBindings(false);

        }
        #endregion

        #region 销帐

        /// <summary>
        /// 
        /// </summary>
        [EventPublication(ActionsConstants.FAM_REFRESHBILLLISTPART)]
        public event EventHandler<DataEventArgs<object>> RefreshBillListPartEvent;

        private void barWriteOff_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (Save())
                {
                    if (Saved != null)
                    {
                        Saved(new object[] { writeOffItemInfo });
                    }
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// 币种与金金额统计信息
    /// </summary>
    public class CurrentAndAmountTotalInfo
    {
        /// <summary>
        /// 币种ID
        /// </summary>
        public Guid CurrentID
        {
            get;
            set;
        }
        /// <summary>
        /// 账单金额合计
        /// </summary>
        public Decimal TotalBillAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 账单金额合计(本位币)
        /// </summary>
        public Decimal TotalBillAmountToStandardCurrency
        {
            get;
            set;
        }
        /// <summary>
        /// 其它费用合计
        /// </summary>
        public Decimal TotalOtherAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 其它费用合计(本位币)
        /// </summary>
        public Decimal TotalOtherAmountToStandardCurrency
        {
            get;
            set;
        }
    }
}
