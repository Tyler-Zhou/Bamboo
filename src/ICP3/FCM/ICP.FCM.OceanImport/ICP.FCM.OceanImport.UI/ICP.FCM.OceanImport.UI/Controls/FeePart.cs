﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.ReportCenter.UI;

namespace ICP.FCM.OceanImport.UI.Controls
{
    [ToolboxItem(false)]
    public partial class FeePart : DevExpress.XtraEditors.XtraUserControl, IChildPart
    {
        #region Fields
        Guid _CompanyID = Guid.Empty;
        #endregion

        #region 服务

        WorkItem Workitem = null;

        IDataFindClientService DataFindClientService { get { return ServiceClient.GetClientService<IDataFindClientService>(); } }
        IOceanImportService OceanImportService { get { return ServiceClient.GetService<IOceanImportService>(); } }

        #endregion

        #region  本地变量

        OceanImportFeeList CurrentRow
        {
            get
            {
                if (bsFee.Current == null)
                    return null;
                else
                    return bsFee.Current as OceanImportFeeList;
            }
        }

        List<OceanImportFeeList> SelectRows
        {
            get
            {
                int[] indexs = gridViewFee.GetSelectedRows();
                if (indexs == null || indexs.Length == 0) return null;

                List<OceanImportFeeList> list = new List<OceanImportFeeList>();
                foreach (var item in indexs)
                {
                    OceanImportFeeList tager = gridViewFee.GetRow(item) as OceanImportFeeList;
                    if (tager != null) list.Add(tager);
                }
                return list;
            }
        }


        /// <summary>
        /// 选择的费用代码集合
        /// </summary>
        public string SelectChargeCodeIds
        {
            get
            {
                string ChargeCodes = "";
                if (txtFreightIncluded.Tag != null)
                {
                    List<Guid> listChargeCode = txtFreightIncluded.Tag as List<Guid>;
                    List<string> strChargeCode = new List<string>();
                    foreach (var item in listChargeCode)
                    {
                        strChargeCode.Add(item.ToString());
                    }

                    ChargeCodes = string.Join(",", strChargeCode.ToArray());
                }
                return ChargeCodes;
            }
        }




        /// <summary>
        /// 解决方案代码
        /// </summary>
        Guid _solutionID;

        #endregion

        #region 初始化

        public FeePart()
        {
            InitializeComponent();
            if (!LocalData.IsDesignMode)
            {
                this.Disposed += delegate
                {
                    this.gridViewFee.CellValueChanged -= this.gridViewFee_CellValueChanged;
                    this.gridControl1.DataSource = null;
                    bsFee.DataSource = null;
                    bsFee.Dispose();
                    this.DataChanged = null;
                    _currencyList = null;
                    _rateList = null;
                    if (this.Workitem != null)
                    {
                        this.Workitem.Items.Remove(this);
                        this.Workitem = null;
                    }
                };
            }
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
            colRemark.Caption = "备注";
            colUnitPrice.Caption = "单价";
            colWay.Caption = "方向";
            barAdd.Caption = "新增(&A)";
            barDelete.Caption = "删除(&D)";
        }


        public void SetChargeCodeDataSource(string tag, string text)
        {
            if (!string.IsNullOrEmpty(tag) && !string.IsNullOrEmpty(text))
            {

                List<Guid> taglist = new List<Guid>();
                List<string> list = new List<string>(tag.Split(','));
                foreach (var item in list)
                {
                    taglist.Add(new Guid(item.ToString()));
                }

                this.txtFreightIncluded.Tag = taglist;
                this.txtFreightIncluded.Text = text.Replace(",", ";");

            }
        }

        List<SolutionCurrencyList> _currencyList = null;

        List<SolutionExchangeRateList> _rateList = null;

        Guid _defaultCurrencyID = Guid.Empty;



        private void InitControls()
        {
            //FeeWay
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<FeeWay>> feeWays = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<FeeWay>(LocalData.IsEnglish);
            cmbWay.Properties.BeginUpdate();
            foreach (var item in feeWays)
            {
                if (item.Value == FeeWay.None) continue;
                cmbWay.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value, (short)item.Value));
            }
            cmbWay.Properties.EndUpdate();

            DataFindClientService.RegisterGridColumnFinder(colChargingCodeName
                                                , ICP.Common.ServiceInterface.CommonFinderConstants.ChargingCodeFinder
                                                , "ChargingCodeID"
                                                , "ChargingCodeName"
                                                , "ChargingCodeID"
                                                , "ChargingCodeName"
                                                , GetSolutionChargingCodeSearchCondition);

            DataFindClientService.RegisterGridColumnFinder(colCustomerName
                                                , ICP.Common.ServiceInterface.CommonFinderConstants.CustoemrFinder
                                                , "CustomerID"
                                                , "CustomerName"
                                                , "ID"
                                                , LocalData.IsEnglish ? "EName" : "CName");

            SearchBoxAdapter.RegisterChargingCodeMultipleSearchBox(DataFindClientService, this.txtFreightIncluded);
        }

        SearchConditionCollection GetSolutionChargingCodeSearchCondition()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("SolutionID", _solutionID, false);
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

            if (ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(CurrencyID) || _rateList == null || _rateList.Count == 0)
            {
                string message = LocalData.IsEnglish ? "Have no rate at current company." : "找不到当前公司下的汇率.";
                throw new ApplicationException(message);
            }
            List<OceanImportFeeList> fee = bsFee.DataSource as List<OceanImportFeeList>;

            if (fee == null)
            {
                return 0m;
            }

            foreach (var item in fee)
            {
                if (ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(item.CurrencyID)) continue;

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

            List<OceanImportFeeList> fee = bsFee.DataSource as List<OceanImportFeeList>;
            if (fee == null || _currencyList == null)
            {
                return ZeroMoney;
            }
            fee = fee.FindAll(delegate(OceanImportFeeList item) { return item.Way == FeeWay.AR; });
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
            List<OceanImportFeeList> fee = bsFee.DataSource as List<OceanImportFeeList>;
            if (fee == null || _currencyList == null)
            {
                return ZeroMoney;
            }
            fee = fee.FindAll(delegate(OceanImportFeeList item) { return item.Way == FeeWay.AP; });
            if (fee == null || fee.Count == 0)
            {
                return ZeroMoney;
            }


            return GetAmountString(fee);
        }

        private string GetAmountString(List<OceanImportFeeList> fee)
        {
            string amountString = string.Empty;

            Dictionary<Guid, decimal> dic = new Dictionary<Guid, decimal>();
            foreach (var item in fee)
            {
                if (ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(item.CurrencyID) || item.Amount <= 0) continue;

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

        public void SetExchangeRateAndCurrency(List<SolutionExchangeRateList> rateList, List<SolutionCurrencyList> currencyList, Guid defaultCurrencyID, Guid solutionID)
        {
            _rateList = rateList;
            _currencyList = currencyList;
            _defaultCurrencyID = defaultCurrencyID;
            _solutionID = solutionID;


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

        #region 新增&删除

        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OceanImportFeeList preRow = null;
            if (gridViewFee.RowCount > 0)
                preRow = gridViewFee.GetRow(gridViewFee.RowCount - 1) as OceanImportFeeList;

            OceanImportFeeList newFeeRow;

            if (preRow != null)
                newFeeRow = Utility.Clone<OceanImportFeeList>(preRow);
            else
                newFeeRow = new OceanImportFeeList();

            newFeeRow.ID = Guid.Empty;
            newFeeRow.CreateDate = DateTime.Now;
            newFeeRow.CreateByID = LocalData.UserInfo.LoginID;
            newFeeRow.CreateByName = LocalData.UserInfo.LoginName;
            newFeeRow.Quantity = 1;
            newFeeRow.Amount = newFeeRow.Quantity * newFeeRow.UnitPrice;
            newFeeRow.Way = FeeWay.AR;
            if (ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(newFeeRow.CurrencyID))
                newFeeRow.CurrencyID = _defaultCurrencyID;

            newFeeRow.ChargingCodeID = Guid.Empty;
            newFeeRow.ChargingCodeName = string.Empty;


            (bsFee.List as List<OceanImportFeeList>).Add(newFeeRow);
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
            List<OceanImportFeeList> list = SelectRows;
            if (list == null || list.Count == 0) return;

            if (!Utility.EnquireIsDeleteCurrentData())
            {
                return;
            }

            List<Guid> needRemoveIDs = new List<Guid>();
            List<DateTime?> needRemoveUpdateDate = new List<DateTime?>();

            foreach (var item in list)
            {
                if (ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(item.ID) == false)
                {
                    needRemoveIDs.Add(item.ID);
                    needRemoveUpdateDate.Add(item.UpdateDate);
                }
            }
            try
            {
                if (needRemoveIDs.Count != 0)
                {
                    if (_CompanyID==Guid.Empty)
                        OceanImportService.RemoveOIOrderFeeList(needRemoveIDs.ToArray(), LocalData.UserInfo.LoginID, needRemoveUpdateDate.ToArray());
                    else
                        OceanImportService.RemoveOIOrderFeeList(needRemoveIDs.ToArray(),_CompanyID, LocalData.UserInfo.LoginID, needRemoveUpdateDate.ToArray());
                }

                gridViewFee.DeleteSelectedRows();
                _isChanged = true;
                if (DataChanged != null) DataChanged(this, null);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm()
                    , (LocalData.IsEnglish ? "Delete Faily" : "删除失败.") + ex.Message);
            }
        }

        private void gridViewFee_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //Amount
            if (e.Column.Name == colUnitPrice.Name || e.Column.Name == colQuantity.Name)
            {
                object row = gridViewFee.GetRow(e.RowHandle);

                if (row != null && row as OceanImportFeeList != null)
                {
                    OceanImportFeeList fee = row as OceanImportFeeList;
                    fee.Amount = decimal.Parse((fee.Quantity * fee.UnitPrice).ToString("F2"));
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
                if (_isChanged)
                {
                    return true;
                }
                foreach (var item in bsFee.DataSource as List<OceanImportFeeList>)
                {
                    if (item.IsDirty)
                    {
                        return true;
                    }
                }

                return _isChanged;
            }
        }

        public bool ValidateData()
        {
            this.Validate();
            this.bsFee.EndEdit();

            foreach (var item in bsFee.DataSource as List<OceanImportFeeList>)
            {
                if (!item.Validate())
                {
                    return false;
                }
            }

            return true;
        }

        public void AfterSaved()
        {
            _isChanged = false;
        }

        public object DataSource
        {
            get { return bsFee.DataSource as List<OceanImportFeeList>; }
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

        /// <summary>
        /// 设置口岸ID
        /// </summary>
        /// <param name="companyID"></param>
        public void SetCompanyID(Guid companyID)
        {
            if (companyID != Guid.Empty && _CompanyID != companyID)
            {
                _CompanyID = companyID;
            }
        }
    }
}