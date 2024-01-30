using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;

using ICP.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.ServiceInterface;
using ICP.Framework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.Controls;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.FCM.OtherBusiness.UI.Common
{
    [ToolboxItem(false)]
    public partial class FeePart : BasePart, IChildPart
    {
        #region 服务

        WorkItem Workitem = null;

        IConfigureService configureService { get { return Workitem.Services.Get<IConfigureService>(); } }
        IDataFindClientService dfService { get { return Workitem.Services.Get<IDataFindClientService>(); } }
        IStatusbar statusbar { get { return Workitem.Services.Get<IStatusbar>(); } }
        IOtherBusinessService oceanExportService { get { return Workitem.Services.Get<IOtherBusinessService>(); } }
        #endregion 

        #region 

         OBFeeList CurrentRow
        {
            get
            {
                if (bsFee.Current == null)
                    return null;
                else
                    return bsFee.Current as OBFeeList;
            }
        }

         List<OBFeeList> SelectRows
         {
             get
             {
                 int[] indexs = gridViewFee.GetSelectedRows();
                 if (indexs == null || indexs.Length == 0) return null;

                 List<OBFeeList> list = new List<OBFeeList>();
                 foreach (var item in indexs)
                 {
                     OBFeeList tager = gridViewFee.GetRow(item) as OBFeeList;
                     if (tager != null) list.Add(tager);
                 }
                 return list;
             }
         }

        #endregion

        #region 初始化

        public FeePart()
        {
            InitializeComponent();
            if (LocalData.IsEnglish == false)
            {
                SetCnText();
            }
        }

        private void SetCnText()
        {
            colAmount.Caption = "金额";
            colChargingCodeName.Caption = "费用名称";
            colCurrency.Caption = "币种";
            colCustomerName.Caption = "客户";
            colQuantity.Caption = "数量";
            colRemark .Caption = "备注";
            colUnitPrice.Caption = "单价";
            colWay.Caption = "方向";
            barAdd.Caption = "新增(&A)";
            barDelete.Caption = "删除(&D)";
        }

        List<SolutionCurrencyList> _currencyList = null;

        List<SolutionExchangeRateList> _rateList = null;

        Guid _SolutionID = Guid.Empty;
        Guid _defaultCurrencyID = Guid.Empty;

        private void InitControls()
        {
            //FeeWay
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<FeeWay>> feeWays = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<FeeWay>(LocalData.IsEnglish);
            foreach (var item in feeWays)
            {
                if (item.Value == FeeWay.None) continue;
                cmbWay.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value,(short)item.Value));
            }

            dfService.RegisterGridColumnFinder(colChargingCodeName
                                                , ICP.Common.ServiceInterface.CommonFinderConstants.SolutionChargingCodeFinder
                                                , "ChargingCodeID"
                                                , "ChargingCodeName"
                                                , "ChargingCodeID"
                                                , "ChargingCodeName"
                                                , this.GetSolutionChargingCodeSearchCondition);

            dfService.RegisterGridColumnFinder(colCustomerName
                                                , ICP.Common.ServiceInterface.CommonFinderConstants.CustoemrFinder
                                                , "CustomerID"
                                                , "CustomerName"
                                                , "ID"
                                                , LocalData.IsEnglish ? "EName" : "CName");
        }

        SearchConditionCollection GetSolutionChargingCodeSearchCondition()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("SolutionID", _SolutionID, false);
            return conditions;
        }

        #endregion

        #region 接口

        /// <summary>
        /// 获取总利润,如果没找到汇率会抛出一个ApplicationException错误
        /// </summary>
        public decimal GetProfit(Guid CurrencyID)
        {
            decimal amount = 0m;

            if (Utility.GuidIsNullOrEmpty(CurrencyID) || _rateList == null || _rateList.Count == 0)
            {
                string message = LocalData.IsEnglish?"Have no rate at current company.":"找不到当前公司下的汇率.";
                throw new ApplicationException(message);
            }
            List<OBFeeList> fee = bsFee.DataSource as List<OBFeeList>;

            if (fee == null)
            {
                return 0m;
            }

            foreach (var item in fee)
            {
                if (Utility.GuidIsNullOrEmpty(item.CurrencyID)) continue;

                if (item.CurrencyID == CurrencyID)
                {
                    if (item.Way == FeeWay.AR) 
                    {
                        amount += item.Amount;
                    }
                    else
                    {
                        amount -= item.Amount;
                    }
                }
                else
                {
                    decimal rate = Utility.GetRate(item.CurrencyID, CurrencyID, DateTime.Now, _rateList);
                    if (rate == 0)
                    {
                        string message = LocalData.IsEnglish ? "Rate not find." + item.Currency + " => tager currency" : "找不到" + item.Currency + " 到目标币种的汇率";
                        throw new ApplicationException(message);
                    }

                    if (item.Way == FeeWay.AR)
                    {
                        amount += item.Amount * rate;
                    }
                    else
                    {
                        amount -= item.Amount * rate;
                    }
                }
            }
            return amount;
        }

        /// <summary>
        /// 获取应收币种费用字符串
        /// </summary>
        /// <returns>返回应收币种费用字符串</returns>
        public string GetDRAmountString()
        {
            string amountString = string.Empty;

            List<OBFeeList> fee = bsFee.DataSource as List<OBFeeList>;
            if (fee == null || _currencyList == null)
            {
                return ZeroMoney;
            }
            fee = fee.FindAll(delegate(OBFeeList item) { return item.Way == FeeWay.AR; });
            if (fee == null || fee.Count == 0)
            {
                return ZeroMoney;
            }

            return GetAmountString(fee);
        }

        const string ZeroMoney = "0";

        /// <summary>
        /// 获取应付币种费用字符串
        /// </summary>
        /// <returns>返回应付币种费用字符串</returns>
        public string GetCRAmountString()
        {
            List<OBFeeList> fee = bsFee.DataSource as List<OBFeeList>;
            if (fee == null || _currencyList == null)
            {
                return ZeroMoney;
            }
            fee = fee.FindAll(delegate(OBFeeList item) { return item.Way == FeeWay.AP; });
            if (fee == null || fee.Count == 0)
            {
                return ZeroMoney;
            }


            return GetAmountString(fee);
        }

        private string GetAmountString( List<OBFeeList> fee)
        {
            string amountString = string.Empty;

            Dictionary<Guid, decimal> dic = new Dictionary<Guid, decimal>();
            foreach (var item in fee)
            {
                if (Utility.GuidIsNullOrEmpty(item.CurrencyID) || item.Amount <= 0) continue;

                if (dic.ContainsKey(item.CurrencyID) == false)
                    dic.Add(item.CurrencyID, 0m);

                dic[item.CurrencyID] += item.Amount;
            }

            foreach (var item in dic)
            {
                SolutionCurrencyList currency = _currencyList.Find(delegate(SolutionCurrencyList citem) { return citem.CurrencyID == item.Key; });
                if (currency == null) continue;

                if (amountString.Length > 0) amountString += ",";

                amountString += currency.CurrencyName + ":" + item.Value.ToString("N");
            }
            return amountString;
        }

        /// <summary>
        /// 用于公司更变时，刷新子面板数据
        /// </summary>
        /// <param name="rateList">汇率</param>
        /// <param name="currencyList">币种</param>
        /// <param name="defaultCurrencyID">默认币种</param>
        /// <param name="CompanyID">公司</param>
        public void SetExchangeRateAndCurrency(List<SolutionExchangeRateList> rateList, List<SolutionCurrencyList> currencyList, Guid defaultCurrencyID, Guid solutionID)
        {
            _rateList = rateList;
            _currencyList = currencyList;
            _defaultCurrencyID = defaultCurrencyID;
            _SolutionID = solutionID;
            cmbCurrency.Items.Clear();
            if (_currencyList.Count == 0)
            {
                throw new ApplicationException("找不到币种.");
            }

            foreach (var item in _currencyList)
            {
                cmbCurrency.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.CurrencyName, item.CurrencyID));
            }

        }

        #endregion

        #region

        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OBFeeList preRow=null;
            if (gridViewFee.RowCount > 0)
                preRow = gridViewFee.GetRow(gridViewFee.RowCount - 1) as OBFeeList;

            OBFeeList newFeeRow;

            if (preRow != null)
                newFeeRow = Utility.Clone<OBFeeList>(preRow);
            else
                newFeeRow = new OBFeeList();

            newFeeRow.ID = Guid.Empty;
            newFeeRow.CreateDate = DateTime.Now;
            newFeeRow.CreateByID = LocalData.UserInfo.LoginID;
            newFeeRow.CreateByName = LocalData.UserInfo.LoginName;
            newFeeRow.Quantity = 1;
            newFeeRow.Amount = newFeeRow.Quantity * newFeeRow.UnitPrice;
            newFeeRow.Way =  FeeWay.AR;
            if(Utility.GuidIsNullOrEmpty(newFeeRow.CurrencyID))
                newFeeRow.CurrencyID = _defaultCurrencyID;

            newFeeRow.ChargingCodeID = Guid.Empty;
            newFeeRow.ChargingCodeName = string.Empty;



            this.gridViewFee.ClearSorting();
            (bsFee.List as List<OBFeeList>).Add(newFeeRow);
            bsFee.ResetBindings(false);

            _isChanged = true;
            if (DataChanged != null)
            {
                DataChanged(this, null);
            }

            this.gridViewFee.MoveLast();
        }

        private void barDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<OBFeeList> list = SelectRows;
            if (list == null || list.Count == 0) return;

            if (!PartLoader.EnquireIsDeleteCurrentData())
            {
                return;
            }

            List<Guid> needRemoveIDs = new List<Guid>();
            List<DateTime?> needRemoveUpdateDate = new List<DateTime?>();

            foreach (var item in list)
            {
                if (Utility.GuidIsNullOrEmpty(item.ID) == false)
                {
                    needRemoveIDs.Add(item.ID);
                    needRemoveUpdateDate.Add(item.UpdateDate);
                }
            }
            try
            {
                if (needRemoveIDs.Count != 0)
                {
                    oceanExportService.RemoveOBOrderFeeList(needRemoveIDs.ToArray(), LocalData.UserInfo.LoginID, needRemoveUpdateDate.ToArray(),LocalData.IsEnglish);
                }

                gridViewFee.DeleteSelectedRows();
                _isChanged = true;
                if (DataChanged != null) DataChanged(this, null);
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm()
                ,( LocalData.IsEnglish ? "Delete Faily" : "删除失败.")+ex.Message); }
        }

        private void gridViewFee_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //Amount
            if (e.Column.Name == colUnitPrice.Name || e.Column.Name == colQuantity.Name)
            {
                object row = gridViewFee.GetRow(e.RowHandle);

                if (row != null && row as OBFeeList != null)
                {
                    OBFeeList fee = row as OBFeeList;
                    fee.Amount = decimal.Parse ((fee.Quantity * fee.UnitPrice).ToString("F2"));
                }
            }

            if (DataChanged != null)
            {
                DataChanged(this, null);
            }
            _isChanged = true;
        }

        #endregion

        #region IChildPart 成员

        public event EventHandler DataChanged;

        bool _isChanged = false;
        public bool IsChanged
        {
            get
            {
                if (_isChanged == false)
                {
                    List<OBFeeList> source = bsFee.DataSource as List<OBFeeList>;
                    if (source != null)
                    {
                        foreach (var item in source)
                        {
                            if (item.IsDirty)
                            {
                                return true;
                            }
                        }
                    }
                }

                return _isChanged;
            }
        }

        public bool ValidateData()
        {
            this.Validate();
            this.bsFee.EndEdit();

            foreach (var item in bsFee.DataSource as List<OBFeeList>)
            {
                if (item.Validate
                    (
                        //delegate(ValidateEventArgs e)
                        //{ 
                        //    if(item.Amount
                        //}
                    ) == false) return false;
            }

            return true;
        }

        public void AfterSaved()
        {
            _isChanged = false;
        }

        public object DataSource
        {
            get { return bsFee.DataSource as List<OBFeeList>; }
        }

        public void SetSource(object value)
        {
            if (value == null) return;
            _isChanged = false;
            bsFee.DataSource = value; bsFee.ResetBindings(false);
        }

        public void SetService(WorkItem workitem)
        {
            Workitem = workitem;
            InitControls();
        }

        #endregion
    }
}
