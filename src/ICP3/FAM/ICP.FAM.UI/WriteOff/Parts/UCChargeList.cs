using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using ICP.FAM.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.CompositeObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Extender;
using ICP.Framework.CommonLibrary;
using System.Windows.Forms;
using ICP.FAM.ServiceInterface;

namespace ICP.FAM.UI.WriteOff
{
    public partial class UCChargeList : BaseListPart
    {
        public UCChargeList()
        {
            InitializeComponent();
            Disposed += delegate
            {
                if (customerFinder != null)
                {
                    customerFinder.Dispose();
                    customerFinder = null;
                }
                if (chargeIdFinder != null)
                {
                    chargeIdFinder.Dispose();
                    chargeIdFinder = null;
                }
                gcMain.DataSource = null;
                bsCharges.DataSource = null;
                bsCharges.Dispose();
                Selected = null;
                _gLList = null;
                RateList = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }

            };
        }

        #region 服务
        [ServiceDependency]
        private WorkItem Workitem
        {
            get;
            set;
        }

        IDataFinderFactory dataFinderFactory
        {
            get
            {
                return ServiceClient.GetClientService<IDataFinderFactory>();
            }
        }

        public RateHelper RateHelper
        {
            get
            {
                return ClientHelper.Get<RateHelper, RateHelper>();
            }
        }
        IDataFindClientService dfService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }
        private Guid companyID;
        /// <summary>
        /// 公司ID
        /// </summary>
        public Guid CompanyID
        {
            get
            {
                if (companyID == null || companyID == Guid.Empty)
                {
                    return LocalData.UserInfo.DefaultCompanyID;
                }    
                
                return companyID;
            }
            set
            {
                companyID = value;
            }
        }
        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid CustomerID
        {
            get;
            set;
        }
        public string CustomerName
        {
            get;
            set;
        }

        public void SetService(WorkItem workitem)
        {
            Workitem = workitem;
        }
        public DateTime CheckDate 
        { 
            get; 
            set; 
        }

        #endregion

        #region 属性

        /// <summary>
        /// 数据源
        /// </summary>
        public override object DataSource
        {
            get
            {
                return bsCharges.DataSource;
            }
            set
            {
                bsCharges.DataSource = value;

            }
        }

        /// <summary>
        /// 当前行数据
        /// </summary>
        public WriteOffCharge CurrentRow
        {
            get
            {
                return bsCharges.Current as WriteOffCharge;
            }
        }

        public Guid StandardCurrencyID { get; set; }

        public AccountListInfo AccountListInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 数据源
        /// </summary>
        public List<WriteOffCharge> DataList
        {
            get
            {
                List<WriteOffCharge> list = bsCharges.DataSource as List<WriteOffCharge>;
                if (list == null)
                {
                    list = new List<WriteOffCharge>();
                }
                return list;
            }
        }
        /// <summary>
        /// 汇率信息
        /// </summary>
        public List<SolutionExchangeRateList> RateList = new List<SolutionExchangeRateList>();
        /// <summary>
        /// 币种ID与币种名称信息
        /// </summary>
        public Dictionary<Guid, String> CurrencyList = new Dictionary<Guid, String>();
        /// <summary>
        /// 获得财务费用的统计信息
        /// </summary>
        public Dictionary<Guid, Decimal> GetTotalInfo()
        {
            Dictionary<Guid, Decimal> dicFinanceList = new Dictionary<Guid, decimal>();
            if (DataList == null)
            {
                return dicFinanceList;
            }

            dicFinanceList = (from d in DataList group d by d.CurrencyID into g select new { g.Key, TotalAcmount = g.Sum(p => p.Way == FeeWay.AR ? p.Amount : -p.Amount) }).ToDictionary(c => c.Key, c => c.TotalAcmount);

            if (dicFinanceList == null)
            {
                dicFinanceList = new Dictionary<Guid, decimal>();
            }

            return dicFinanceList;

        }

        /// <summary>
        /// 账单统计信息
        /// </summary>
        public Dictionary<Guid, Decimal> BillTotalInfo = new Dictionary<Guid, decimal>();
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
                foreach (WriteOffCharge item in DataList)
                {
                    if (item.IsDirty)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// 银行选择的
        /// </summary>
        public Guid AccountCurrencyID
        {
            get;
            set;
        }

        WriteOffType _CurrencyType;
        /// <summary>
        /// 核销类型
        /// </summary>
        public WriteOffType CurrencyType
        {
            get
            {
                return _CurrencyType;
            }
            set
            {
                _CurrencyType = value;
            }
        }

        public FeeWay FeeWay { get; set; }

        /// <summary>
        /// 除汇兑损益外的总金额
        /// </summary>
        public decimal TotalAmount(FeeWay feeWay)
        {
            decimal amount = 0;
            foreach (WriteOffCharge item in DataList)
            {
                if (item.Way == feeWay)
                {
                    amount += (item.Amount * item.ExchangeRate);
                }
                else
                {
                    amount -= (item.Amount * item.ExchangeRate);
                }
            }
            return amount;

        }

        /// <summary>
        /// 其他项目中的总金额
        /// </summary>
        public decimal TotalNoGLAmount
        {
            get
            {
                decimal amount =FAMUtility.Get2Round((from d in DataList where d.GLID != new Guid("4D652752-A5BE-4159-BDDB-DFE263AAFF32") select d.StandardCurrencyAmount).Sum());
                return amount;
            }
        }


        #endregion

        #region 统计信息
        /// <summary>
        /// 统计所有的币种信息
        /// </summary>
        public void TotalChargeAmount()
        {
            txtAmount.Text = string.Empty;
            foreach (KeyValuePair<Guid, Decimal> item in GetTotalInfo())
            {
                string currencyName = string.Empty;
                if (CurrencyList.Keys.Contains(item.Key))
                {
                    currencyName = CurrencyList[item.Key];
                }
                else
                {
                    currencyName = LocalData.IsEnglish ? "Unknown" : "未知";
                }
                txtAmount.Text += currencyName + ":" + item.Value.ToString("n") + " ";

            }
            TotalPaymentsAmount();
            TotalStandardCurrencyAmount();
        }
        /// <summary>
        /// 合计为本位币金额
        /// </summary>
        private void TotalStandardCurrencyAmount()
        {
            decimal totalAmount = (from d in DataList where DataTypeHelper.GetGuid(d.RefID, Guid.Empty) == Guid.Empty select d.Way==FeeWay?d.StandardCurrencyAmount:-d.StandardCurrencyAmount).Sum();
            txtTotalByCurrency.Text = totalAmount.ToString("n");
        }
        /// <summary>
        /// 统计预收预付信息
        /// </summary>
        private void TotalPaymentsAmount()
        {
            Dictionary<Guid, Decimal> dicFinanceList = new Dictionary<Guid, decimal>();

            dicFinanceList = (from d in DataList where DataTypeHelper.GetGuid(d.RefID,Guid.Empty)!=Guid.Empty group d by d.CurrencyID into g select new { g.Key, TotalAcmount = g.Sum(p => p.Way == FeeWay.AR ? p.Amount : -p.Amount) }).ToDictionary(c => c.Key, c => c.TotalAcmount);

            if (dicFinanceList == null)
            {
                dicFinanceList = new Dictionary<Guid, decimal>();
            }

            txtPaymentAmount.Text = string.Empty;
            foreach (KeyValuePair<Guid, Decimal> item in dicFinanceList)
            {
                string currencyName = string.Empty;
                if (CurrencyList.Keys.Contains(item.Key))
                {
                    currencyName = CurrencyList[item.Key];
                }
                else
                {
                    currencyName = LocalData.IsEnglish ? "Unknown" : "未知";
                }
                txtPaymentAmount.Text += currencyName + ":" + item.Value.ToString("n") + " ";

            }
        }
        
        #endregion

        #region 重写
        /// <summary>
        /// 财务费用的金额发生改变
        /// </summary>
        public override event SelectedHandler Selected;
        #endregion

        #region 初始化
        private void ChargeList_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                cmbWay.Items.Add(new ImageComboBoxItem("", FeeWay.AR, 0));
                cmbWay.Items.Add(new ImageComboBoxItem("", FeeWay.AP, 1));

                _gLList = ConfigureService.GetSolutionGLCodeListNew(GetSolutionID(), new Guid[1] { LocalData.UserInfo.DefaultCompanyID }, string.Empty, string.Empty, GLCodeType.Unknown, true, LocalData.IsEnglish);
                gvMain.ShowGridViewRowNo(50);
            }

        }

        private Guid GetSolutionID()
        {
            Guid solutionID = Guid.Empty;
            ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(CompanyID);
            if (configureInfo != null)
            {
                solutionID = configureInfo.SolutionID;
            }
            return solutionID;
        }

        public void InitControls()
        {

            SearchRegister();

            if (PrepaymentList.Count > 0)
            {
                bsCharges.DataSource = PrepaymentList;
            }

        }

        #region 设置币种下拉框内容
        /// <summary>
        /// 设置币种
        /// </summary>
        /// <param name="list"></param>
        public void SetCurrencyComBox(List<SolutionCurrencyList> list, Guid defaultCurrencyID)
        {
            cmbCurrencyID.Properties.Items.Clear();
            cmbCurrencyID.Items.Clear();
            foreach (SolutionCurrencyList item in list)
            {
                cmbCurrencyID.Properties.Items.Add(new ImageComboBoxItem(item.CurrencyName, item.CurrencyID));

                //this.cmbCurrency.Properties.Items.Add(new ImageComboBoxItem(item.CurrencyName, item.CurrencyID));
            }

            //this.cmbCurrency.EditValue = defaultCurrencyID;

        }

        #endregion

        #endregion

        #region 搜索器
        private IDisposable customerFinder, chargeIdFinder;
        /// <summary>
        /// 注册客户搜索器
        /// </summary>
        private void SearchRegister()
        {
            customerFinder = dfService.RegisterGridColumnFinder(colCustomerName
                          , CommonFinderConstants.CustoemrFinder
                          , "CustomerID"
                          , "CustomerName"
                          , "ID"
                          , LocalData.IsEnglish ? "EName" : "CName",
                          GetCustomerStateCondition);

            //chargeIdFinder = dfService.RegisterGridColumnFinder(colChargeID, 
            //              ICP.FAM.ServiceInterface.FAMFinderConstants.GLCodeFinder,
            //              "GLID", 
            //              "GLFullName", 
            //              "ID", 
            //              "GLCodeName",
            //              GetSolutionIDSearchCondition);
            chargeIdFinder = dfService.RegisterGridColumnFinder(colChargeID
                 , FAMFinderConstants.GLCodeFinder
                 , new string[] { "GLID", "GLFullName", "ForeignCurrencyID" }
                 , new string[] { "ID", "GLCodeName", "ForeignCurrencyID" }
                 , GetSolutionIDSearchCondition,
                  delegate(object inputSource, object resultData)
                  {
                      WriteOffCharge returnItem = resultData as WriteOffCharge;

                      if (returnItem != null)
                      {
                          if (returnItem.ForeignCurrencyID == null || returnItem.ForeignCurrencyID == StandardCurrencyID)
                          {
                              // 本币位
                              CurrentRow.CurrencyID = StandardCurrencyID;
                              CurrentRow.StandardCurrencyRate = 1;
                          }
                          else
                          { 
                             //其他币种
                              CurrentRow.CurrencyID = DataTypeHelper.GetGuid(returnItem.ForeignCurrencyID, StandardCurrencyID);
                              CurrentRow.StandardCurrencyRate = RateHelper.GetRate(returnItem.ForeignCurrencyID.Value, StandardCurrencyID, DateTime.SpecifyKind(CheckDate, DateTimeKind.Unspecified), RateList);
                          }

                          //DateTime date = DateTime.Now;
                          //if (CheckDate != null)
                          //{
                          //    date = CheckDate;
                          //}
                          //CurrentRow.ra = RateHelperService.GetRate(returnItem.ForeignCurrencyID.Value, StandardCurrencyID, DateTime.SpecifyKind(date, DateTimeKind.Unspecified), RateList);
                          //bsDetails.ResetBindings(false);
                      }
                      //CurrentRow.ForeignCurrencyID = returnItem.ForeignCurrencyID;

                  });

        }

        SearchConditionCollection GetCustomerStateCondition()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("CodeApplyState", CustomerCodeApplyState.Passed, false);
            return conditions;
        }
        SearchConditionCollection GetSolutionIDSearchCondition()
        {
            List<Guid> idList = new List<Guid>();
            idList.Add(CompanyID);

            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("SolutionID", GetSolutionID(), false);
            conditions.AddWithValue("OnlyLeaf", true, false);
            conditions.AddWithValue("CompanyIDs", idList, false);
            return conditions;
        }

        public ConfigureInfo UCConfigureInfo { get; set; }

        /// <summary>
        /// 填充会计科目下拉框
        /// </summary>
        //public void SetChargeList(ConfigureInfo configureInfo, IConfigureService configureService)
        //{
        //    _gLList = configureService.GetSolutionGLCodeList(configureInfo.SolutionID, true);

        //    foreach (SolutionGLCodeList item in _gLList)
        //    {
        //        string name = LocalData.IsEnglish ? item.EName : item.CName;
        //        this.cmbGL.Properties.Items.Add(new ImageComboBoxItem(name, item.ID));

        //    }

        //}
        #endregion

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            WriteOffCharge charge = new WriteOffCharge();
            charge.ID = Guid.NewGuid();
            charge.Way = FeeWay;
            charge.CurrencyID = StandardCurrencyID;
            charge.ExchangeRate = 1;
            charge.StandardCurrencyRate = 1;

            bsCharges.Add(charge);
            isCharge = true;

            bsCharges.ResetBindings(false);

            if (Selected != null)
            {
                Selected(this, null);
            }

        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (bsCharges.Current == null)
            {
                return;
            }
            if (FAMUtility.EnquireIsDeleteCurrentData())
            {
                bsCharges.RemoveCurrent();
                isCharge = true;

                if (Selected != null)
                {
                    Selected(this, null);
                    TotalChargeAmount();
                }
            }
        }

        #endregion

        #region 列表事件
        /// <summary>
        /// 币种发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            if (e.Value == null || CurrentRow == null)
            {
                return;
            }

            if (e.Column == colCurrencyID)
            {
                Guid curID = new Guid(e.Value.ToString());
                try
                {
                    CurrentRow.StandardCurrencyRate = RateHelper.GetRate(curID, StandardCurrencyID, DateTime.SpecifyKind(CheckDate, DateTimeKind.Unspecified), RateList);
                }
                catch (Exception ex)
                {
                    CurrentRow.StandardCurrencyRate = 1;
                }
                if (Selected != null)
                {
                    Selected(this, null);
                    TotalChargeAmount();
                }
            }

        }

        private void gvMain_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column == colAmount ||
                e.Column == colWay ||
                e.Column == colStandardCurrencyRate ||
                e.Column == colCurrencyID)
            {
                decimal rate = RateHelper.GetRate(CurrentRow.CurrencyID, StandardCurrencyID, DateTime.SpecifyKind(CheckDate, DateTimeKind.Unspecified), RateList);
                CurrentRow.StandardCurrencyRate = rate;
                CurrentRow.StandardCurrencyAmount = decimal.Round(CurrentRow.Amount * rate, 2, MidpointRounding.AwayFromZero);

                TotalChargeAmount();
                if (Selected != null)
                {
                    Selected(this, null);
                }

            }

        }

        #endregion

        #region 外部调用

        private List<SolutionGLCodeList> _gLList;
        /// <summary>
        /// 会计科目列表
        /// </summary>
        public List<SolutionGLCodeList> GLList
        {
            get
            {
                return _gLList;
            }
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        public void ValidateData()
        {
            bsCharges.EndEdit();
        }
        /// <summary>
        /// 获得数据
        /// </summary>
        /// <returns></returns>
        public List<Expense> GetDataList(bool isMultCurrency,Guid currencyID)
        {
            List<Expense> list = new List<Expense>();
            foreach (WriteOffCharge item in DataList)
            {
                Expense saveRequest = new Expense();
                saveRequest.ID = item.ID;
                saveRequest.CustomerID = item.CustomerID;
                saveRequest.GLID = item.GLID;
                saveRequest.GLDescription = item.GLDescription;
                saveRequest.BillNo = item.BillNo;
                saveRequest.CurrencyID = item.CurrencyID;
                saveRequest.Amount = item.Amount;
                //根据银行币种，计算出汇率
                if (isMultCurrency)
                {
                    saveRequest.Rate = 1;
                }
                else
                {
                    decimal rate = RateHelper.GetRate(item.CurrencyID, currencyID, DateTime.SpecifyKind(CheckDate, DateTimeKind.Unspecified), RateList);
                    saveRequest.Rate = item.ExchangeRate = rate;
                }

                saveRequest.Remark = item.Remark;
                saveRequest.UpdateDate = item.UpdateDate == null ? string.Empty : item.UpdateDate.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");
                saveRequest.Way = item.Way.GetHashCode();
                saveRequest.RefID = item.RefID;
                saveRequest.StandardCurrencyAmount = item.StandardCurrencyAmount;
                saveRequest.AddInvolvedObject(item);

                list.Add(saveRequest);

            }

            return list;
        }
        /// <summary>
        /// 刷新数据
        /// </summary>
        public void RefreshUI(List<Expense> saveRequestList, ManyResult expenseMany)
        {
            if (expenseMany == null || expenseMany.Items.Count == 0)
            {
                return;
            }
            for (int n = 0; n < saveRequestList.Count; n++)
            {
                Expense expenseRequest = saveRequestList[n];
                List<WriteOffCharge> expenses = expenseRequest.UnBoxInvolvedObject<WriteOffCharge>();
                for (int i = 0; i < expenses.Count; i++)
                {
                    expenses[i].ID = expenseMany.Items[n].GetValue<Guid>("ID");
                    expenses[i].UpdateDate = expenseMany.Items[n].GetValue<DateTime?>("UpdateDate");
                    //expenses[i].RefID = DataTypeHelper.GetGuid(expenseMany.Items[n].GetValue<DateTime?>("RefID"), Guid.Empty);
                    expenses[i].IsDirty = false;
                }
            }
        }

        public void RefreshUI()
        {
            bsCharges.ResetBindings(false);
        }

        /// <summary>
        /// 刷新保存
        /// </summary>
        public void AfterSave()
        {
            isCharge = false;

            foreach (WriteOffCharge item in DataList)
            {
                item.IsDirty = false;
            }
        }
        /// <summary>
        /// 设置银行汇率--已无效
        /// </summary>
        /// <param name="currencyID"></param>
        public void SetBankAccountRate(WriteOffType currencyType, Guid currencyID, bool isAlter)
        {
            //CurrencyID = currencyID;
            //CurrencyType = currencyType;

            //if (CurrencyID == Guid.Empty)
            //{
            //    return;
            //}


            //if (currencyType == WriteOffType.Single)
            //{
            //    #region 单币种:折算汇率

            //    if (isAlter)
            //    {
            //        foreach (WriteOffCharge bill in DataList)
            //        {
            //            if (bill.CurrencyID == currencyID)
            //            {
            //                bill.ExchangeRate = 1;
            //                continue;
            //            }
            //            else
            //            {
            //                try
            //                {
            //                    bill.ExchangeRate = RateHelper.GetRate(bill.CurrencyID, currencyID, DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified), RateList);
            //                }
            //                catch (Exception ex)
            //                {
            //                    bill.ExchangeRate = 1;
            //                }

            //            }
            //        }
            //    }
            //    #endregion
            //}
            //else
            //{
            //    #region 多币种:汇率为1
            //    DataList.ForEach(o => o.ExchangeRate = 1);
            //    #endregion
            //}

            //bsCharges.ResetBindings(false);

        }
        #endregion

        #region 预收预付
        private void barSelectData_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindPrepaymentPart part = Workitem.Items.AddNew<FindPrepaymentPart>();
            part.CompanyID = CompanyID;
            part.CustomerID = CustomerID;
            part.CurrencyList = CurrencyList;
            string title = LocalData.IsEnglish ? "Select Prepayment" : "选择多收多付";
            DialogResult result = PartLoader.ShowDialog(part, title);
            if (result == DialogResult.OK)
            {
                //确认 -- 绑定数据
                List<PrepaymentList> list = (from d in part.SelectDataList
                                             select new PrepaymentList
                                                {
                                                    Way=d.Way,
                                                    RefID=d.RefID,
                                                    CurrencyID=d.CurrencyID,
                                                    CheckAmount=d.CheckAmount,
                                                    BillNo=d.BillNo,
                                                }).ToList();

                BindPrepaymentData(list);

            }
        }

        List<SolutionGLConfigList> glConfigList = new  List<SolutionGLConfigList>();

        public List<SolutionGLConfigList> GLConfigList
        {
            get
            {
                if (glConfigList.Count == 0)
                {
                   List<SolutionGLConfigList> list= ConfigureService.GetSolutionGLConfigList(GetSolutionID(),true); 
                   glConfigList=(from d in list where d.GLConfigTypeID==3 select d).ToList();
                }
                return glConfigList;
            }
        }
        List<WriteOffCharge> PrepaymentList = new List<WriteOffCharge>();
        /// <summary>
        /// 绑定选择的多收多付数据(冲销情况)
        /// </summary>
        public void BindPrepaymentData(List<PrepaymentList> list)
        {
            foreach (PrepaymentList item in list)
            {
                WriteOffCharge findItem = DataList.Find(delegate(WriteOffCharge data) { return data.RefID == item.RefID; });
                if (findItem == null)
                {
                    //找到预收预付科目
                    WriteOffCharge addItem = new WriteOffCharge();
                    addItem.ID = Guid.NewGuid();
                    addItem.Amount = item.CheckAmount;
                    addItem.RefID = item.RefID;
                    addItem.CustomerID = CustomerID;
                    addItem.CustomerName = CustomerName;
                    addItem.IsWriteOff = true;
                    addItem.CurrencyID = item.CurrencyID;
                    addItem.BillNo = item.BillNo;
                    addItem.Way = (item.Way == FeeWay.AR ? FeeWay.AP : FeeWay.AR);
                    #region 会计科目
                    SolutionGLConfigList glconfig = GLConfigList.Find(delegate(SolutionGLConfigList data) { return data.CurrencyID == item.CurrencyID && data.CompanyID == CompanyID; });
                    if (glconfig == null)
                    {
                        //如果没有找到公司下的，就找公司ID为空的(适用于所有)
                        glconfig = GLConfigList.Find(delegate(SolutionGLConfigList data) { return data.CurrencyID == item.CurrencyID && DataTypeHelper.GetGuid(data.CompanyID, Guid.Empty) == Guid.Empty; });
                    }
                    if (glconfig == null)
                    {
                        XtraMessageBox.Show(LocalData.IsEnglish ? "Please contact the system administrator to configure Receipts / prepaid account information" : "请联系系统管理员配置预收/预付科目信息");
                        return;
                    }

                    if (item.Way == FeeWay.AR)
                    {
                        addItem.GLID = glconfig.DRGLCodeID;
                        addItem.GLFullName = glconfig.DRGLFullName;
                        addItem.GLDescription = glconfig.DRGLFullName;
                    }
                    else
                    {
                        addItem.GLID = glconfig.CRGLCodeID;
                        addItem.GLFullName = glconfig.CRGLFullName;
                        addItem.GLDescription = glconfig.CRGLFullName;
                    }  
                    #endregion
                    addItem.StandardCurrencyRate = RateHelper.GetRate(item.CurrencyID, StandardCurrencyID, DateTime.SpecifyKind(CheckDate, DateTimeKind.Unspecified), RateList);
                    addItem.StandardCurrencyAmount = decimal.Round(addItem.Amount * addItem.StandardCurrencyRate, 2, MidpointRounding.AwayFromZero);

                    PrepaymentList.Add(addItem);
                    bsCharges.Add(addItem);
                }
                else
                {
                    findItem.Amount = item.CheckAmount;
                    findItem.StandardCurrencyRate = RateHelper.GetRate(findItem.CurrencyID, StandardCurrencyID, DateTime.SpecifyKind(CheckDate, DateTimeKind.Unspecified), RateList);
                    findItem.StandardCurrencyAmount = decimal.Round(findItem.Amount * findItem.StandardCurrencyRate, 2, MidpointRounding.AwayFromZero);

                }
            }
            TotalInfo(true);
        }

        /// <summary>
        /// 修改账单费用信息添加多收多付数据(预收/预付情况)
        /// </summary>
        /// <param name="bill"></param>
        public void AddPrepayment(WriteOffBill bill,decimal amount)
        {
            bool isAddItem = false;
            WriteOffCharge findItem = DataList.Find(delegate(WriteOffCharge data) { return data.RefID == bill.ChargeID; });
            if (!FAMUtility.GuidIsNullOrEmpty(AccountCurrencyID)
                 && CurrencyType == WriteOffType.Single
                 && findItem != null
                 && findItem.CurrencyID != AccountCurrencyID)
            { 
                //已经存在，但由于币种不一样，先删除，再新增
                DataList.Remove(findItem);
                isAddItem = true;
            }

            if (findItem == null || isAddItem)
             {
                 WriteOffCharge addItem = new WriteOffCharge();
                 addItem.ID = Guid.NewGuid();
                 addItem.RefID = bill.ChargeID;
                 addItem.CustomerID = CustomerID;
                 addItem.CustomerName = CustomerName;
                 addItem.BillNo = bill.BillNo;
                 addItem.IsWriteOff = false;
                 addItem.Way = bill.Way;

                 //币种与金额
                 UpdateAmount(addItem, bill, amount);

                 bsCharges.Add(addItem);
             }
             else
             {
                 UpdateAmount(findItem, bill, amount);
             }
             bsCharges.ResetBindings(false);
             
            TotalInfo(true);
        }
        /// <summary>
        /// 计算预收预付上的金额
        /// </summary>
        /// <param name="item"></param>
        /// <param name="billCurrencyID"></param>
        /// <param name="amount"></param>
        private void UpdateAmount(WriteOffCharge item, WriteOffBill bill, decimal amount)
        {
            #region 会计科目
            //先找该公司下的
            SolutionGLConfigList glconfig = GLConfigList.Find(delegate(SolutionGLConfigList data) { return data.CurrencyID == bill.CurrencyID && data.CompanyID == CompanyID; });
            if (glconfig == null)
            {
                //如果没有找到公司下的，就找公司ID为空的(适用于所有)
                glconfig = GLConfigList.Find(delegate(SolutionGLConfigList data) { return data.CurrencyID == bill.CurrencyID && DataTypeHelper.GetGuid(data.CompanyID, Guid.Empty) == Guid.Empty; });
            }
            if (glconfig == null)
            {
                XtraMessageBox.Show(LocalData.IsEnglish ? "Please contact the system administrator to configure Receipts / prepaid account information" : "请联系系统管理员配置预收/预付科目信息");
                return;
            }
            if (bill.Way == FeeWay.AR)
            {
                item.GLID = glconfig.DRGLCodeID;
                item.GLFullName = glconfig.DRGLFullName;
                item.GLDescription = glconfig.DRGLFullName;
            }
            else
            {
                item.GLID = glconfig.CRGLCodeID;
                item.GLFullName = glconfig.CRGLFullName;
                item.GLDescription = glconfig.CRGLFullName;
            }
            #endregion

            #region 币种与金额
            Guid currencyID = Guid.Empty;
            if (CurrencyType == WriteOffType.Muitl)
            {
                currencyID = bill.CurrencyID;
            }
            else
            {
                //单币种 如果有选择银行,则按银行的币种来算，如果没有选银行，则按账单中的银行来算
                if (FAMUtility.GuidIsNullOrEmpty(AccountCurrencyID))
                {
                    currencyID = bill.CurrencyID;
                }
                else
                {
                    currencyID = AccountCurrencyID;
                }
            }
            item.CurrencyID = currencyID;
            if (!FAMUtility.GuidIsNullOrEmpty(AccountCurrencyID)
                && AccountCurrencyID != bill.CurrencyID
                && CurrencyType == WriteOffType.Single)
            {
                //银行的币种跟账单上的不一样
                decimal rate = RateHelper.GetRate(bill.CurrencyID, AccountCurrencyID, DateTime.SpecifyKind(CheckDate, DateTimeKind.Unspecified), RateList);
                item.Amount = decimal.Round(amount * rate, 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                item.Amount = amount;
            }
            item.StandardCurrencyRate = RateHelper.GetRate(item.CurrencyID, StandardCurrencyID, DateTime.SpecifyKind(CheckDate, DateTimeKind.Unspecified), RateList);
            item.StandardCurrencyAmount = decimal.Round(item.Amount * item.StandardCurrencyRate, 2, MidpointRounding.AwayFromZero);
            #endregion

        }
        public void TotalInfo(bool updateTotalAmount)
        {
            TotalChargeAmount();
            TotalPaymentsAmount();
            if (Selected != null)
            {
                Selected(this, updateTotalAmount);
            }
        }
        #endregion

        #region 删除对应的预收预付信息
        /// <summary>
        /// 删除对应的预收预付信息
        /// </summary>
        /// <param name="refid"></param>
        public void RemovePrepaymentData(List<Guid> refIDList)
        {
            if (refIDList == null || refIDList.Count == 0 || DataList == null || DataList.Count==0)
            {
                return;
            }
            List<WriteOffCharge> list=(from d in DataList where !refIDList.Contains(DataTypeHelper.GetGuid(d.RefID, Guid.Empty)) select d).ToList();
            bsCharges.DataSource = list;
        }
        #endregion
    }
}
