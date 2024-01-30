using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary;
using DevExpress.XtraBars;

namespace ICP.FAM.UI.WriteOff
{
    /// <summary>
    /// 销账绑定数据
    /// </summary>
    partial class WriteOffEditPart
    {
        /// <summary>
        /// 注册消息
        /// </summary>
        private void InitMessage()
        {
            RegisterMessage("1109210001", LocalData.IsEnglish ? "The AccountDate must input" : "到帐时间不能为空");

            RegisterMessage("1109290001", LocalData.IsEnglish ? "input the Currency info" : "请录入币种信息");
            RegisterMessage("1109290002", LocalData.IsEnglish ? "input the Bill/Fee info" : "请录入账单/费用信息");

            RegisterMessage("1109290003", LocalData.IsEnglish ? "Single mode does not allow the existence of multiple currency amount information" : "单种模式下不允许存在多个币种金额信息");
            RegisterMessage("1109290004", LocalData.IsEnglish ? "Multi currency box under the currency amount, information must be more" : "多币种框下,币种金额信息必须为多个");

            RegisterMessage("1109300001", LocalData.IsEnglish ? "Currency amounts of {0} and aggregated amount not equal" : "币种{0}的金额与汇总金额不相等");

            RegisterMessage("1110210001", LocalData.IsEnglish ? "Amount couldnot equal zero" : "金额不能为0");
            RegisterMessage("1110210002", LocalData.IsEnglish ? "Selected banks repeat" : "选择的银行重复");
            RegisterMessage("1110210003", LocalData.IsEnglish ? "Can not choose more of the same currency as the bank account" : "不能同时选择多个相同币种的银行账号");

            RegisterMessage("1412180001", LocalData.IsEnglish ? "Costs associated with the current advance / prepaid, and delete deletes advance / prepaid, continue to delete?" : "当前费用关联了预收/预付,删除后同时会删除预收/预付,是否继续删除?");
        }
        bool isLoad = false;
        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitControls()
        {
            if (isLoad)
            {
                return;
            }
            SearchRegister();

            FAMUtility.SetCustomerTextEditNullValuePrompt(new List<TextEdit>
            {
                txtCustomer
            });

            FAMUtility.BindComboBoxByCompany(cmbCompany);
            if (writeOffItemInfo.CompanyID.IsNullOrEmpty())
            {
                cmbCompany.EditValue = LocalData.UserInfo.DefaultCompanyID;
            }
            else if (writeOffItemInfo != null && !writeOffItemInfo.CompanyID.IsNullOrEmpty())
            {
                cmbCompany.EditValue = writeOffItemInfo.CompanyID;
            }

            cmbCompany.EditValueChanged += cmbCompany_EditValueChanged;
            string chargeItem = LocalData.IsEnglish ? "Charge" : "费用";
            string billItem = LocalData.IsEnglish ? "Bill" : "帐单";

            cmbType.Properties.Items.Add(new ImageComboBoxItem(billItem, CheckMode.Bill));
            cmbType.Properties.Items.Add(new ImageComboBoxItem(chargeItem, CheckMode.Charge));

            cmbWay.Items.Add(new ImageComboBoxItem("", FeeWay.AR, 0));
            cmbWay.Items.Add(new ImageComboBoxItem("", FeeWay.AP, 1));

            cmbType.EditValue = writeOffItemInfo.CheckMode;


            Guid companyID = (Guid)cmbCompany.EditValue;

            List<SolutionCurrencyList> _currencyList = new List<SolutionCurrencyList>();

            if (_rateList == null || _rateList.Count == 0)
            {
                _rateList = ConfigureService.GetCompanyExchangeRateList(companyID, true);
            }

            ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(companyID);
            //找到解决方案
            if (configureInfo != null)
            {
                _currencyList = ConfigureService.GetSolutionCurrencyList(configureInfo.SolutionID, true);
            }
            else
            {
                return;
            }

            //填充下拉框与币种信息
            foreach (SolutionCurrencyList currency in _currencyList)
            {
                currencyList.Add(currency.CurrencyID, currency.CurrencyName);
            }
            UCcharges.CurrencyList = currencyList;

            StandardCurrencyID = configureInfo.StandardCurrencyID;

            UCAccountListInfo.SetCurrencyComBox(_currencyList);
            UCAccountListInfo.RateList = _rateList;
            UCAccountListInfo.StandardCurrencyID = configureInfo.StandardCurrencyID;

            UCcharges.RateList = _rateList;
            UCcharges.StandardCurrencyID = configureInfo.StandardCurrencyID;
            UCcharges.SetCurrencyComBox(_currencyList, configureInfo.DefaultCurrencyID);
            UCcharges.UCConfigureInfo = configureInfo;

            isLoad = true;
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="values"></param>
        public override void Init(IDictionary<string, object> values)
        {
            _values = values;
            if (values == null)
            {
                return;
            }
            dteWriteDate.DateTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            foreach (var item in values)
            {
                if (item.Key.ToUpper() == "WriteOffType".ToUpper())
                {
                    writeOffType = (WriteOffType)item.Value;
                    UCAccountListInfo.CurrencyType = writeOffType;
                    UCAccountListInfo.SetradCurrencyTypeEnable();
                }
                if (item.Key.ToUpper() == "PayCurrencyID".ToUpper())
                {
                    PayCurrencyID = new Guid(item.Value.ToString());
                    AccountCurrencyID = PayCurrencyID;
                }
                if (item.Key.ToUpper() == "CompanyID".ToUpper())
                {
                    CompanyID = new Guid(item.Value.ToString());
                    if(values.ContainsKey("CompanyName"))
                    {
                        CompanyName = values["CompanyName"].ToString();
                    }
                    cmbCompany.ShowSelectedValue(CompanyID, CompanyName);
                    writeOffItemInfo.CompanyID = CompanyID;

                    UCcharges.CompanyID = CompanyID;
                    UCAccountListInfo.CompanyID = CompanyID;
                }
                if (item.Key.ToUpper() == "CustomerID".ToUpper() && item.Value != null)
                {
                    txtCustomer.Tag =txtPayCustomerName.CustomerID =  writeOffItemInfo.CustomerID = new Guid(item.Value.ToString());
                    UCcharges.CustomerID = writeOffItemInfo.CustomerID;
                }
                if (item.Key.ToUpper() == "CustomerName".ToUpper() && item.Value != null)
                {
                    txtCustomer.EditValue = writeOffItemInfo.CustomerName = item.Value.ToString();
                    UCcharges.CustomerName = writeOffItemInfo.CustomerName;
                }
                if (item.Key.ToUpper() == "FeeWay".ToUpper())
                {
                    _feeWay = (FeeWay)item.Value;
                }
                if (item.Key.ToUpper() == "BillList".ToUpper())
                {
                    bsBills.DataSource = new List<WriteOffBill>();
                    //绑定账单列表
                    List<CurrencyBillList> billList = item.Value as List<CurrencyBillList>;
                    if (billList != null)
                    {
                        writeOffItemInfo.CheckDate = DateTime.Now;
                        UCcharges.CheckDate = writeOffItemInfo.CheckDate;
                        UCAccountListInfo.CheckDate = writeOffItemInfo.CheckDate;
                        BindSelectBillList(billList.ToArray(), false);
                    }
                }
                if (item.Key.ToUpper() == "CurrencyIDList".ToUpper())
                {
                    isBillLoad = true;
                    BillCurrencyIDList = item.Value as List<Guid>;
                    if (BillCurrencyIDList != null && BillCurrencyIDList.Count == 1)
                    {
                        AccountCurrencyID = BillCurrencyIDList[0];
                    }
                }
                if (item.Key.ToUpper() == "DataList".ToUpper())
                {
                    BaseDataList = item.Value as List<WriteOffBill>;
                    bsBills.DataSource = BaseDataList;
                }
                if (item.Key.ToUpper() == "CurrencyRateList".ToUpper())
                {
                    CurrencyRateList = item.Value as List<CurrencyRateData>;
                    if (CurrencyRateList == null)
                    {
                        CurrencyRateList = new List<CurrencyRateData>();
                    }
                }

                //if (item.Key.ToUpper() == "BankTransactionInfo".ToUpper())
                //{
                //    BankTransaction = item.Value as BankTransactionInfo;
                //    UCAccountListInfo.BankTransaction = BankTransaction;
                //}
            }
        }

        /// <summary>
        /// 绑定选择的账单列表
        /// </summary>
        public void BindSelectBillList(CurrencyBillList[] billList, bool isFinder)
        {
            if (billList == null)
            {
                return;
            }
            if (!isLoad)
            {
                InitControls();
            }

            List<CurrencyBillList> currencyBillList = (from d in billList
                                                       select
                                                           new CurrencyBillList
                                                           {
                                                               BillNO = d.BillNO,
                                                               CurrencyID = d.CurrencyID,
                                                               ID = d.ID,
                                                               IsCommission = d.IsCommission,
                                                               Way = d.Way,
                                                               CompanyID=d.CompanyID
                                                           }).ToList();

            List<WriteOffBill> list = FinanceService.GetWriteOffBills(currencyBillList, LocalData.IsEnglish);

            if ((BaseDataList == null || BaseDataList.Count == 0)&&currencyBillList.Count>0)
            {
                cmbCompany.EditValue=writeOffItemInfo.CompanyID = currencyBillList[0].CompanyID;
                //this.cmbCompany.ShowSelectedValue(writeOffItemInfo.CompanyID, writeOffItemInfo.CompanyName);
            }


            List<PrepaymentList> preList = new List<PrepaymentList>();

            foreach (WriteOffBill item in list)
            {
                WriteOffBill tager = BaseDataList.Find(delegate(WriteOffBill data) { return item.ChargeID == data.ChargeID; });
                if (tager == null)
                {
                    #region 账单列表中没有的，则新增
                    item.Amount = Math.Abs(item.Amount);
                    item.AvailbeWriteOffAmount = item.AvailbeWriteOffAmount;
                    item.FinalAmount = item.AvailbeWriteOffAmount;
                    item.BillAmount = item.BillAmount;

                    decimal rate = RateHelper.GetRate(item.CurrencyID, StandardCurrencyID, DateTime.SpecifyKind(writeOffItemInfo.CheckDate, DateTimeKind.Unspecified), _rateList);
                    if (rate == 0)
                    {
                        rate = 1;
                    }
                    item.StandardCurrencyRate = rate;
                    item.StandardCurrencyAmount = Get2Round(item.AvailbeWriteOffAmount * rate);

                    BaseDataList.Insert(0, item);

                    //预收预付信息
                    if (DataTypeHelper.GetDecimal(item.PreAmount, 0) > 0)
                    {
                        PrepaymentList preitem = new PrepaymentList();
                        preitem.CheckAmount = DataTypeHelper.GetDecimal(item.PreAmount);
                        preitem.Way = item.Way;
                        preitem.RefID = DataTypeHelper.GetGuid(item.PrepaymentID, Guid.Empty);
                        preitem.CurrencyID = DataTypeHelper.GetGuid(item.PreCurrencyID, item.CurrencyID);
                        preitem.BillNo = item.BillNo;
                        preList.Add(preitem);
                    }
                    #endregion
                }
                else
                {
                    #region 账单列表中已存在,更新一下本次销账金额
                    if (tager.RemainedWriteOffAmount == 0)
                    {
                        //可销账金额为0的，不再处理
                        continue;
                    };
                    if (tager.RemainedWriteOffAmount < item.WriteOffAmount)
                    {
                        //选进入的金额大于可销帐的金额时,本次销账金额=本次销账金额+可销账金额
                        tager.WriteOffAmount = tager.WriteOffAmount + tager.RemainedWriteOffAmount;
                    }
                    else
                    {
                        tager.WriteOffAmount = tager.WriteOffAmount + item.WriteOffAmount;
                    }
                    decimal rate = RateHelper.GetRate(item.CurrencyID, StandardCurrencyID, DateTime.SpecifyKind(writeOffItemInfo.CheckDate, DateTimeKind.Unspecified), _rateList);
                    if (rate == 0)
                    {
                        rate = 1;
                    }
                    tager.StandardCurrencyRate = rate;
                    tager.StandardCurrencyAmount = Get2Round(tager.WriteOffAmount * rate);

                    #endregion
                } 


            }

            isCharge = true;

            SetBillDataSource();

            if (list != null && list.Count > 0 && isFinder)
            {
                TotalCurrencyAndAmount(true,true);
            }
            
            if (preList.Count > 0)
            {
                UCcharges.BindPrepaymentData(preList);
            }

            SwitchState();
        }

        /// <summary>
        /// 编辑时，绑定列表数据
        /// </summary>
        /// <param name="data"></param>
        private void BindingData(object data)
        {
            WriteOffItemList list = data as WriteOffItemList;
            if (list == null)
            {
                writeOffItemInfo.IsPublic = true;
                writeOffItemInfo.CheckDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                writeOffItemInfo.No = string.Empty;
                writeOffItemInfo.Way = _feeWay;
                writeOffItemInfo.IsValid = true;
                writeOffItemInfo.CheckMode = CheckMode.Bill;
                writeOffItemInfo.CreateByName = LocalData.UserInfo.UserName;
                writeOffItemInfo.CreateID = LocalData.UserInfo.LoginID;

                //绑定公司
                if (!FAMUtility.GuidIsNullOrEmpty(CompanyID))
                {
                    writeOffItemInfo.CompanyID = CompanyID;
                    writeOffItemInfo.CompanyName = CompanyName;
                }
                else
                {
                    writeOffItemInfo.CompanyID = LocalData.UserInfo.DefaultCompanyID;
                    writeOffItemInfo.CompanyName = LocalData.UserInfo.DefaultCompanyName;
                }

                bsWriteOff.DataSource = writeOffItemInfo;
                bsWriteOff.ResetBindings(false);

                //绑定账单数据
                if (DataList != null)
                {
                    bsBills.DataSource = DataList;
                    bsBills.ResetBindings(false);
                }
                else
                {
                    bsBills.DataSource = new List<WriteOffBill>();
                    bsBills.ResetBindings(false);
                }
                //绑定其他项目
                UCcharges.DataSource = new List<WriteOffCharge>();
            }
            else
            {
                _isFromWriteoffList = true;
                writeOffItemInfo = FinanceService.GetWriteOffItemInfo(list.CheckID);
                if (writeOffItemInfo == null)
                {
                    return;
                }
                txtPayCustomerName.CustomerID= writeOffItemInfo.CustomerID;
                txtPayCustomerName.CustomerName = writeOffItemInfo.PayCustomerName;
                //cmbCompany.Properties.ReadOnly = true;
                _feeWay = writeOffItemInfo.Way;
                bsWriteOff.DataSource = writeOffItemInfo;
                bsWriteOff.ResetBindings(false);

                //绑定账单数据
                List<WriteOffBill> billList = FinanceService.GetWriteOffBillsByIds(writeOffItemInfo.ID);

                foreach (WriteOffBill item in billList)
                {
                    if (baseDataList == null)
                    {
                        baseDataList = new List<WriteOffBill>();
                    }
                    if (!baseDataList.Contains(item))
                    {
                        baseDataList.Add(item);
                    }
                }

                SetBillDataSource();

                //绑定币种金额信息
                List<OperationCurrencyAmountList> currencyAmountList = FinanceService.GetOperationCurrencyAmountList(writeOffItemInfo.ID);
                foreach (var item in currencyAmountList)
                {
                    item.SourceCurrencyID = item.CurrencyID;

                    if (item.BankDate != null && item.BankByID != null)
                    {
                        //已到帐的，不允许再更改
                        barWriteOff.Enabled = false;
                        barWriteOffAndPrint.Enabled = false;
                    }
                }

                UCAccountListInfo.DataSource = currencyAmountList;
                UCAccountListInfo.radCurrencyType.SelectedIndex = writeOffItemInfo.IsMultCurrency.GetHashCode();
                ////UCAccountListInfo.CompanyID = writeOffItemInfo.CompanyID;
                //绑定其它费用数据
                UCcharges.DataSource = FinanceService.GetWriteOffCharges(writeOffItemInfo.ID);

                if (!writeOffItemInfo.IsMultCurrency)
                {
                    AccountCurrencyID = currencyAmountList[0].CurrencyID;
                }

            }

            UCAccountListInfo.CompanyID = writeOffItemInfo.CompanyID;
            UCcharges.CompanyID = writeOffItemInfo.CompanyID;
            UCcharges.CheckDate = writeOffItemInfo.CheckDate;
            UCAccountListInfo.CheckDate = writeOffItemInfo.CheckDate;


            SetChargeCustomerInfo();

            SwitchState();
        }
        /// <summary>
        /// 获取银行交易查询条件
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetCustomerBankCondition()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("CustomerID", writeOffItemInfo.CustomerID, false);
            return conditions;
        }
        /// <summary>
        /// 设置账单/费用数据源
        /// </summary>
        private void SetBillDataSource()
        {
            ShowChargeNameColumn();
            if (cmbType.SelectedIndex <= 0)
            {
                #region 账单
                List<WriteOffBill> list = (from d in BaseDataList
                                           group d by new { d.BillID, BillRefNo = d.BillNo, d.CurrencyID, d.CurrencyName, d.Way, d.IsCommission, d.BillAmount } into g
                                           select new WriteOffBill
                                           {
                                               Selected = false,
                                               BillID = g.Key.BillID,
                                               BillNo = g.Key.BillRefNo,
                                               CurrencyID = g.Key.CurrencyID,
                                               CurrencyName = g.Key.CurrencyName,
                                               Way = g.Key.Way,
                                               IsCommission = g.Key.IsCommission,
                                               //Amount=g.Sum(p=>p.Amount),
                                               Amount = g.Key.BillAmount,
                                               AvailbeWriteOffAmount = g.Sum(p => p.AvailbeWriteOffAmount),
                                               ExchangeRate = g.Max(p => p.ExchangeRate),
                                               FinalAmount = g.Sum(p => p.FinalAmount),
                                               WriteOffAmount = g.Sum(p => p.WriteOffAmount),
                                               StandardCurrencyRate=g.Max(p=>p.StandardCurrencyRate),
                                               StandardCurrencyAmount = g.Sum(p => p.StandardCurrencyAmount),
                                           }).ToList();

                gvTreeGridMain.KeyFieldName = null;
                bsBills.DataSource = list;
                bsBills.ResetBindings(false);

                #endregion
            }
            else
            {
                #region 费用

                List<WriteOffBill> list = (from d in BaseDataList
                                           group d by new { d.BillID, BillNo = d.BillNo, d.CurrencyID, d.CurrencyName, d.Way, d.IsCommission, d.ChargingCodeID, d.ChargeName } into g
                                           select new WriteOffBill
                                           {
                                               Selected = false,
                                               BillID = g.Key.BillID,
                                               BillNo = g.Key.BillNo,
                                               CurrencyID = g.Key.CurrencyID,
                                               CurrencyName = g.Key.CurrencyName,
                                               Way = g.Key.Way,
                                               IsCommission = g.Key.IsCommission,
                                               ChargingCodeID = g.Key.ChargingCodeID,
                                               ChargeName = g.Key.ChargeName,
                                               Amount = g.Sum(p => p.Amount),
                                               AvailbeWriteOffAmount = g.Sum(p => p.AvailbeWriteOffAmount),
                                               ExchangeRate = g.Max(p => p.ExchangeRate),
                                               FinalAmount = g.Sum(p => p.FinalAmount),
                                               WriteOffAmount = g.Sum(p => p.WriteOffAmount),
                                               StandardCurrencyRate = g.Max(p => p.StandardCurrencyRate),
                                               StandardCurrencyAmount = g.Sum(p => p.StandardCurrencyAmount)
                                           }).ToList();

                bsBills.DataSource = list;
                bsBills.ResetBindings(false);

                gvTreeGridMain.BestFitColumns();
                gvTreeGridMain.ExpandAll();

                #endregion
            }
        }

        /// <summary>
        /// 设置销账单内容的禁用或启用
        /// </summary>
        void SwitchState()
        {
            //已经作废
            if (!writeOffItemInfo.IsNew && !writeOffItemInfo.IsValid)
            {
                barWriteOff.Enabled = false;
                barWriteOffAndPrint.Enabled = false;
                bbiViewCheck.Enabled = false;
                bbiListCredentials.Enabled = false;
                barPrint.Enabled = false;

                financeChargesTools1.Enabled = false;
                UCAccountListInfo.Enabled = false;
            }
            //已经审核
            else if (!writeOffItemInfo.IsNew && !FAMUtility.GuidIsNullOrEmpty(writeOffItemInfo.AuditorID))
            {
                barWriteOff.Enabled = false;
                barWriteOffAndPrint.Enabled = false;

                financeChargesTools1.barAdjustment.Enabled = false;
                financeChargesTools1.barDelete.Enabled = false;
                financeChargesTools1.barSelect.Enabled = false;

                UCAccountListInfo.radCurrencyType.Enabled = false;

                UCcharges.barAdd.Enabled = false;
                UCcharges.btnDelete.Enabled = false;
            }

            //如果是新增空白的销账单，则启用多币种单币种选项。
            if (bsBills.Count == 0)
                UCAccountListInfo.radCurrencyType.Enabled = true;
            else
                UCAccountListInfo.radCurrencyType.Enabled = false;
        }
    }
}
