using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Framework.ClientComponents;
using Microsoft.Practices.CompositeUI;
using System.Linq;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Common.ServiceInterface;
using DevExpress.XtraEditors.Controls;

namespace ICP.FAM.UI.CustomerBill
{
    public partial class AddLocalFee : BaseEditPart
    {
        #region 服务
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        public RateHelper RateHelper
        {
            get
            {
                return ClientHelper.Get<RateHelper, RateHelper>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
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


        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        #endregion

        /// <summary>
        /// 数据源
        /// </summary>
        List<AddLocalFeeList> datasours = null;

        /// <summary>
        /// 当前业务信息
        /// </summary>
        public OperationCommonInfo _operation = null;

        /// <summary>
        /// 公司配置信息
        /// </summary>
        ConfigureInfo configureInfo = null;

        /// <summary>
        /// 汇率列表
        /// </summary>
        List<SolutionExchangeRateList> ratList = null;

        /// <summary>
        /// 币种列表
        /// </summary>
        List<SolutionCurrencyList> currencyList = null;

        private IDisposable customerFinder;

        public AddLocalFee()
        {
            InitializeComponent();
        }

        private void AddLocalFee_Load(object sender, EventArgs e)
        {
            if (_operation != null)
            {
                datasours = FinanceService.GetLocalFeeListForOperationID(_operation.OperationID, LocalData.IsEnglish);
                gcMain.DataSource = datasours;
                gvMain.RefreshData();
                gvMain.ExpandAllGroups();


                configureInfo = ConfigureService.GetCompanyConfigureInfo(_operation.CompanyID);
                ratList = ConfigureService.GetCompanyExchangeRateList(_operation.CompanyID, true);
                currencyList = ConfigureService.GetSolutionCurrencyList(configureInfo.SolutionID, true);
            }

            if (LocalData.IsEnglish)
            {
                colChargeCname.Visible = false;
                colChargeEname.Visible = true;
            }
            else
            {
                colChargeCname.Visible = true;
                colChargeEname.Visible = false;
            }

            _operation.TradeCustomers.ForEach(r =>
            {
                repositoryItemComboBox1.Items.Add(LocalData.IsEnglish ? r.EName : r.CName);
            });

            foreach (var item in currencyList)
            {
                repositoryItemImageComboBox1.Items.Add(new ImageComboBoxItem(item.CurrencyName, item.CurrencyID));
            }

            //customerFinder = DataFindClientService.RegisterGridColumnFinder(colCustomerName
            //      , ICP.Common.ServiceInterface.CommonFinderConstants.CustoemrFinder
            //      , "CustomerID"
            //      , "CustomerName"
            //      , "ID"
            //      , LocalData.IsEnglish ? "EName" : "CName",
            //      GetCustomerStateCondition);

            if (!LocalData.IsEnglish)
            {
                SetCN();
            }
        }

        /// <summary>
        /// 设置业务信息
        /// </summary>
        /// <param name="operation"></param>
        public void SetOperation(OperationCommonInfo operation)
        {
            _operation = operation;
        }

        /// <summary>
        /// 处理中文
        /// </summary>
        private void SetCN()
        {
            colAmount.Caption = "金额";
            colBillAmount.Caption = "账单金额";
            colBillNo.Caption = "账单号";
            colBillWay.Caption = "账单类型";
            colChargeCname.Caption = "费用名称";
            colChargeEname.Caption = "费用名称";
            colCustomerName.Caption = "往来单位";
            colIsSelected.Caption = "选择";
            colPrice.Caption = "单价";
            colqty.Caption = "数量";
            colWay.Caption = "类型";
            colCurrencyID.Caption = "币种";
            barText.Caption = "如果本地费用已经做过账单(红色字体显示)，请自行修改账单";
        }

        SearchConditionCollection GetCustomerStateCondition()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("CodeApplyState", CustomerCodeApplyState.Passed, false);
            return conditions;
        }

        //public override object DataSource
        //{
        //    get { return this._operation; }
        //    set { BindingData(value); }
        //}

        //void BindingData(object data)
        //{
        //    if (data == null)
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        _operation = data as OperationCommonInfo;
        //    }
        //}

        /// <summary>
        /// 保存按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            gvMain.EndUpdate();
            //检索直接新增的账单
            List<AddLocalFeeList> addlist = datasours.FindAll(r => r.IsSelected && r.BillID == null);
            List<AddLocalFeeList> arlist = addlist.FindAll(r => r.Way == 1);//应收
            List<AddLocalFeeList> aplist = addlist.FindAll(r => r.Way == 2);//应付
            //List<AddLocalFeeList> updatelist = datasours.FindAll(r => r.IsSelected && r.BillID != null);

            try
            {
                //if (updatelist != null && updatelist.Count > 0)
                //{
                //    IEnumerable<IGrouping<Guid?, AddLocalFeeList>> query = updatelist.GroupBy(a => a.BillID);
                //    foreach (IGrouping<Guid?, AddLocalFeeList> info in query)
                //    {
                //        List<AddLocalFeeList> uplist = info.ToList<AddLocalFeeList>();
                //        UpdateDate(uplist, (Guid)info.Key);
                //    }
                //}

                IEnumerable<IGrouping<Guid, AddLocalFeeList>> query = arlist.GroupBy(a => a.CustomerID);
                foreach (IGrouping<Guid, AddLocalFeeList> info in query)
                {
                    List<AddLocalFeeList> list = info.ToList<AddLocalFeeList>();
                    SaveData(list);
                }

                query = aplist.GroupBy(a => a.CustomerID);
                foreach (IGrouping<Guid, AddLocalFeeList> info in query)
                {
                    List<AddLocalFeeList> list = info.ToList<AddLocalFeeList>();
                    SaveData(list);
                }

                //SaveData(arlist);
                //SaveData(aplist);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), string.Format("Import LocalFee Error:{0}", ex.Message));
                return;
            }


            FindForm().DialogResult = DialogResult.OK;
            FindForm().Close();
        }

        /// <summary>
        /// 修改现有账单
        /// </summary>
        /// <param name="feelist"></param>
        private void UpdateDate(List<AddLocalFeeList> feelist, Guid billid)
        {
            BillInfo bill = FinanceService.GetBillInfo(billid);

            //关帐检查
            DateTime operationDate = Convert.ToDateTime(_operation.OperationDate.ToShortDateString());
            DateTime chargingCloseDate = Convert.ToDateTime(configureInfo.ChargingClosingdate.Value.ToShortDateString());
            bool isAccountClose = operationDate < chargingCloseDate ? true : false;
            if (isAccountClose)
            {
                DialogResult result = MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "Account has been closed.Closing date is" + configureInfo.ChargingClosingdate.ToString() : "业务已经关帐，计费时间不能小于关帐时间"
                             , LocalData.IsEnglish ? "Tip" : "提示"
                             , MessageBoxButtons.OK);

                return;
            }

            foreach (AddLocalFeeList localfee in feelist)
            {
                ChargeList list = bill.Fees.Find(r => r.ID == localfee.FeeID);
                list.UnitPrice = localfee.Price;
                list.Quantity = localfee.qty;
                list.Amount = localfee.Amount;
            }

            List<BillInfo> saves = new List<BillInfo>();
            saves.Add(bill);

            FinanceService.SaveBillInfos(_operation.OperationID, saves, LocalData.UserInfo.LoginID, 2, operationDate);
        }

        /// <summary>i
        /// 保存账单
        /// </summary>
        /// <param name="feelist"></param>
        private void SaveData(List<AddLocalFeeList> feelist)
        {
            BillInfo newData = new BillInfo();
            List<Guid?> feeIDs = new List<Guid?>();
            List<FeeType> feeTypes = new List<FeeType>();
            List<FeeWay> feeWays = new List<FeeWay>();
            List<Guid> feeChargingCodeIDs = new List<Guid>(), feeCurrencyIDs = new List<Guid>(), feeUnitIDs = new List<Guid>();
            List<string> feeChargingDescriptions = new List<string>();
            List<decimal> feeRates = new List<decimal>(), feeQuantities = new List<decimal>(), feeUnitPrices = new List<decimal>(), feeAmounts = new List<decimal>();
            List<string> feeRemarks = new List<string>();
            List<DateTime?> feeUpdateDates = new List<DateTime?>();
            List<FormType> formTypes = new List<FormType>();
            List<bool> feeIsAgents = new List<bool>();
            List<bool> feeIsSecondSales = new List<bool>();
            List<bool> feeIsVATInvoiceds = new List<bool>();
            List<bool> feeIsGSTs = new List<bool>();
            List<Guid?> feeContainerIDs = new List<Guid?>();
            List<int?> feeFromType = new List<int?>();
            List<bool> feeIsRevises = new List<bool>();
            List<Guid> rateCurrencyIDs = new List<Guid>();
            List<decimal> rateValues = new List<decimal>();
            List<DataDictionaryList> _packTypes = null;


            //关帐检查
            DateTime operationDate = Convert.ToDateTime(_operation.OperationDate.ToShortDateString());
            DateTime chargingCloseDate = Convert.ToDateTime(configureInfo.ChargingClosingdate.Value.ToShortDateString());
            bool isAccountClose = operationDate < chargingCloseDate ? true : false;
            if (isAccountClose)
            {
                DialogResult result = MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "Account has been closed.Closing date is" + configureInfo.ChargingClosingdate.ToString() : "业务已经关帐，计费时间不能小于关帐时间"
                             , LocalData.IsEnglish ? "Tip" : "提示"
                             , MessageBoxButtons.OK);

                return;
            }
            _packTypes = TransportFoundationService.GetDataDictionaryList(string.Empty, string.Empty, DataDictionaryType.ValuationUnit, true, 0);

            if (feelist.Count > 0)
            {
                newData.CreateByID = LocalData.UserInfo.LoginID;
                newData.CreateByName = LocalData.UserInfo.LoginName;
                newData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

                if (operationDate <= chargingCloseDate)
                {
                    newData.DueDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    newData.AccountDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                }
                else
                {
                    newData.DueDate = DateTime.SpecifyKind(operationDate, DateTimeKind.Unspecified);
                    newData.AccountDate = DateTime.SpecifyKind(operationDate, DateTimeKind.Unspecified);
                }
                if (_operation.Forms != null && _operation.Forms.Count > 0)
                {
                    FormData form = _operation.Forms.Find(r => r.Type == FormType.HBL);
                    if (form == null)
                    {
                        form = _operation.Forms[0];
                    }
                    newData.FormID = form.ID;
                }
                newData.CompanyID = _operation.CompanyID;
                newData.State = BillState.Created;
                newData.Type = (BillType)feelist[0].Way;
                newData.FromType = (int)_operation.OperationType;
                newData.AgentID = _operation.AgentID;
                newData.Remark = LocalData.IsEnglish ? "Add LocalFee auto created" : "添加本地费用自动生成";
                newData.CustomerID = feelist[0].CustomerID;
                CustomerInfo customer = CustomerService.GetCustomerInfo(newData.CustomerID);
                newData.CustomerDescription = new FAMCustomerDescription();
                newData.CustomerDescription.Name = LocalData.IsEnglish ? customer.EName : customer.CName;
                newData.CustomerDescription.Address = LocalData.IsEnglish ? customer.EAddress : customer.CAddress;
                newData.CustomerDescription.Tel = customer.Tel1;
                newData.CustomerDescription.Fax = customer.Fax;
                newData.PayCurrencyId = configureInfo.StandardCurrencyID;
                newData.Fees = new List<ChargeList>();
                foreach (AddLocalFeeList fee in feelist)
                {
                    ChargeList charge = new ChargeList();
                    charge.Type = FeeType.Normal;
                    charge.Amount = fee.Amount;
                    charge.CurrencyID = fee.CurrencyID;
                    SolutionCurrencyList curr = currencyList.Find(r => r.CurrencyID == fee.CurrencyID);
                    charge.CurrencyName = curr == null ? fee.CurrencyCode : curr.CurrencyName;
                    charge.Quantity = fee.qty;
                    charge.UnitPrice = fee.Price;
                    charge.UnitID = _packTypes[0].ID;
                    charge.UnitName = LocalData.IsEnglish ? _packTypes[0].EName : _packTypes[0].CName;
                    charge.Way = (FeeWay)(feelist[0].Way == 3 ? 1 : feelist[0].Way);
                    charge.Rate = RateHelper.GetRate(configureInfo.DefaultCurrencyID, configureInfo.StandardCurrencyID, DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified), ratList);
                    charge.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    charge.CreateByID = LocalData.UserInfo.LoginID;
                    charge.CreateByName = LocalData.UserInfo.LoginName;
                    charge.FromType = (int)_operation.OperationType;
                    charge.ChargingCodeID = fee.ChargeID;
                    charge.ChargingCode = fee.Code;
                    charge.ChargeName = LocalData.IsEnglish ? fee.ChargeEname : fee.ChargeCname;
                    feeIDs.Add(null);
                    feeWays.Add(charge.Way);
                    feeTypes.Add(charge.Type);
                    feeIsAgents.Add(false);
                    feeIsSecondSales.Add(false);
                    feeIsVATInvoiceds.Add(false);
                    feeIsGSTs.Add(false);
                    feeChargingCodeIDs.Add(charge.ChargingCodeID);
                    feeChargingDescriptions.Add(null);
                    feeCurrencyIDs.Add(charge.CurrencyID);
                    feeRates.Add(charge.Rate);
                    feeContainerIDs.Add(null);
                    feeUnitIDs.Add(charge.UnitID);
                    feeUnitPrices.Add(charge.UnitPrice);
                    feeQuantities.Add(charge.Quantity);
                    feeAmounts.Add(charge.Amount);
                    feeRemarks.Add(null);
                    feeUpdateDates.Add(null);
                    feeFromType.Add(charge.FromType);
                    feeIsRevises.Add(false);
                }

                HierarchyManyResult mresult = FinanceService.SaveBillInfo(
                        _operation.OperationID
                        , newData.FormID
                        , newData.FormType
                        , newData.ID
                        , newData.CompanyID
                        , newData.CustomerID
                        , newData.ShipToID
                        , newData.CustomerDescription
                        , newData.CustomerRefNo
                        , newData.Type
                        , newData.AccountDate
                        , newData.DueDate
                        , newData.PayCurrencyId
                        , newData.AuditorID
                        , newData.AuditorEmail
                        , newData.State
                        , newData.No
                        , _operation.OperationDate
                        , newData.FromType
                        , rateCurrencyIDs.ToArray()
                        , rateValues.ToArray()
                        , newData.Remark
                        , newData.IsVATInvoiced
                        , newData.Taxrate
                        , newData.UpdateDate
                        , feeIDs.ToArray()
                        , feeWays.ToArray()
                        , feeTypes.ToArray()
                        , feeIsAgents.ToArray()
                        , feeIsSecondSales.ToArray()
                        , feeIsVATInvoiceds.ToArray()
                        , feeIsGSTs.ToArray()
                        , feeChargingCodeIDs.ToArray()
                        , feeChargingDescriptions.ToArray()
                        , feeCurrencyIDs.ToArray()
                        , feeRates.ToArray()
                        , feeContainerIDs.ToArray()
                        , feeUnitIDs.ToArray()
                        , feeUnitPrices.ToArray()
                        , feeQuantities.ToArray()
                        , feeAmounts.ToArray()
                        , feeRemarks.ToArray()
                        , feeUpdateDates.ToArray()
                        , feeFromType.ToArray()
                        , feeIsRevises.ToArray()
                        , false
                        , false
                        , LocalData.UserInfo.LoginID
                        );
            }
        }

        private void gvMain_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.ToUpper().Contains("WAY"))
            {
                switch (Convert.ToByte(e.Value))
                {
                    case 1:
                        e.DisplayText = "应收";
                        break;
                    case 2:
                        e.DisplayText = "应付";
                        break;
                    case 3:
                        e.DisplayText = "代理";
                        break;
                    default:
                        e.DisplayText = "";
                        break;
                }
            }
        }

        private void gvMain_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            gvMain.ShowEditor();
            if (e.Column.FieldName == "IsSelected")
            {
                AddLocalFeeList list = gvMain.GetRow(e.RowHandle) as AddLocalFeeList;
                list.IsSelected = !list.IsSelected;
                gvMain.RefreshRow(e.RowHandle);
            }
        }

        private void gvMain_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Price" || e.Column.FieldName == "qty")
            {
                AddLocalFeeList list = gvMain.GetRow(e.RowHandle) as AddLocalFeeList;
                list.Amount = list.Price * list.qty;
                gvMain.RefreshRow(e.RowHandle);
            }
        }

        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle > -1)
            {
                AddLocalFeeList current = gvMain.GetRow(e.RowHandle) as AddLocalFeeList;
                if (current.BillID != null)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
        }

        private void barOK_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (barCustomer.EditValue == null || string.IsNullOrEmpty(barCustomer.EditValue.ToString()))
            {
                return;
            }
            OperationCustomer selCustomer = _operation.TradeCustomers.Find(r => r.CName == barCustomer.EditValue.ToString());
            int[] selectindexs = gvMain.GetSelectedRows();
            foreach (int index in selectindexs)
            {
                AddLocalFeeList current = gvMain.GetRow(index) as AddLocalFeeList;
                current.CustomerID = selCustomer.ID;
                current.CustomerName = LocalData.IsEnglish ? selCustomer.EName : selCustomer.CName;
            }
            bool isAllAgent = true;
            List<AddLocalFeeList> arlist = datasours.FindAll(r => r.Way == 1 || r.Way == 3);
            if (_operation.TradeCustomers.Find(j => j.IsAgent) != null)
            {
                foreach (AddLocalFeeList ar in arlist)
                {
                    if (ar.CustomerID != _operation.TradeCustomers.Find(j => j.IsAgent).ID)
                    {
                        isAllAgent = false;
                        break;
                    }
                }
            }
            else
            {
                isAllAgent = false;
            }


            if (isAllAgent)
            {
                arlist.ForEach(r =>
                {
                    r.Way = 3;
                });
            }
            else
            {
                arlist.ForEach(r =>
                {
                    r.Way = 1;
                });
            }
            gvMain.RefreshData();
        }

        private void barFreightCollect_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<AddLocalFeeList> arlist = datasours.FindAll(r => r.Way == 1);//应收
            arlist.ForEach(r =>
            {
                r.CustomerID = _operation.TradeCustomers.Find(j => j.IsAgent).ID;
                r.CustomerName = LocalData.IsEnglish ? _operation.TradeCustomers.Find(j => j.IsAgent).EName : _operation.TradeCustomers.Find(j => j.IsAgent).CName;
                r.Way = 3;
            });
            gvMain.RefreshData();
        }

        private void barRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            datasours = FinanceService.GetLocalFeeListForOperationID(_operation.OperationID, LocalData.IsEnglish);
            gcMain.DataSource = datasours;
            gvMain.RefreshData();
            gvMain.ExpandAllGroups();
        }
    }
}
