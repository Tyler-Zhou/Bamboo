using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.Controls;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.ReportCenter.UI;
using DevExpress.XtraEditors;

namespace ICP.FCM.OceanExport.UI.Common
{
    /// <summary>
    /// 
    /// </summary>
    [ToolboxItem(false)]
    public partial class FeePart : XtraUserControl, IChildPart
    {

        #region Fields
        Guid _CompanyID = Guid.Empty;
        #endregion

        #region 服务

        WorkItem Workitem = null;

        IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }
        #endregion

        #region

        OceanBookingFeeList CurrentRow
        {
            get
            {
                if (bsFee.Current == null)
                    return null;
                else
                    return bsFee.Current as OceanBookingFeeList;
            }
        }

        List<OceanBookingFeeList> SelectRows
        {
            get
            {
                int[] indexs = gridViewFee.GetSelectedRows();
                if (indexs == null || indexs.Length == 0) return null;

                List<OceanBookingFeeList> list = new List<OceanBookingFeeList>();
                foreach (var item in indexs)
                {
                    OceanBookingFeeList tager = gridViewFee.GetRow(item) as OceanBookingFeeList;
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

        #endregion

        #region 初始化

        public FeePart()
        {
            InitializeComponent();
            if (LocalData.IsEnglish == false)
            {
                SetCnText();
            }
            this.Disposed += delegate
            {
                this._currencyList = null;
                this.gridControl1.DataSource = null;

                this.dxErrorProvider1.DataSource = null;

                this.bsFee.DataSource = null;
                this.bsFee.Dispose();
                this.Workitem = null;
                this._rateList = null;
                this.DataChanged = null;
                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }

            };
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
                cmbWay.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value, (short)item.Value));
            }

            DataFindClientService.RegisterGridColumnFinder(colChargingCodeName
                                                , ICP.Common.ServiceInterface.CommonFinderConstants.SolutionChargingCodeFinder
                                                , "ChargingCodeID"
                                                , "ChargingCodeName"
                                                , "ChargingCodeID"
                                                , "ChargingCodeName"
                                                , this.GetSolutionChargingCodeSearchCondition);

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

            if (ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(CurrencyID) || _rateList == null || _rateList.Count == 0)
            {
                string message = LocalData.IsEnglish ? "Have no rate at current company." : "找不到当前公司下的汇率.";
                throw new ApplicationException(message);
            }
            List<OceanBookingFeeList> fee = bsFee.DataSource as List<OceanBookingFeeList>;

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
                    decimal rate = OEUtility.GetRate(item.CurrencyID, CurrencyID, DateTime.Now, _rateList);
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
        /// 设置数据源
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="text"></param>
        public void SetChargeCodeDataSource (string tag,string text)
        {
            if (!string.IsNullOrEmpty(tag)&&!string.IsNullOrEmpty(text))
            {
          
            List<Guid> taglist = new List<Guid>();
            List<string> list = new List<string>(tag.Split(','));
            foreach (var item in list)
            {
                taglist.Add(new Guid(item.ToString()));
            }

            this.txtFreightIncluded.Tag = taglist;
            this.txtFreightIncluded.Text = text.Replace(",",";");

            }
        }

        /// <summary>
        /// 获取应收币种费用字符串
        /// </summary>
        /// <returns>返回应收币种费用字符串</returns>
        public string GetDRAmountString()
        {
            string amountString = string.Empty;

            List<OceanBookingFeeList> fee = bsFee.DataSource as List<OceanBookingFeeList>;
            if (fee == null || _currencyList == null)
            {
                return ZeroMoney;
            }
            fee = fee.FindAll(delegate(OceanBookingFeeList item) { return item.Way == FeeWay.AR; });
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
            List<OceanBookingFeeList> fee = bsFee.DataSource as List<OceanBookingFeeList>;
            if (fee == null || _currencyList == null)
            {
                return ZeroMoney;
            }
            fee = fee.FindAll(delegate(OceanBookingFeeList item) { return item.Way == FeeWay.AP; });
            if (fee == null || fee.Count == 0)
            {
                return ZeroMoney;
            }


            return GetAmountString(fee);
        }

        private string GetAmountString(List<OceanBookingFeeList> fee)
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
            cmbCurrency.BeginUpdate();
            foreach (var item in _currencyList)
            {
                cmbCurrency.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.CurrencyName, item.CurrencyID));
            }
            cmbCurrency.EndUpdate();

        }

        #endregion

        #region

        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OceanBookingFeeList preRow = null;
            if (gridViewFee.RowCount > 0)
                preRow = gridViewFee.GetRow(gridViewFee.RowCount - 1) as OceanBookingFeeList;

            OceanBookingFeeList newFeeRow;

            if (preRow != null)
                newFeeRow = OEUtility.Clone<OceanBookingFeeList>(preRow);
            else
                newFeeRow = new OceanBookingFeeList();

            newFeeRow.ID = Guid.Empty;
            newFeeRow.CreateDate = DateTime.Now;
            newFeeRow.CreateByID = LocalData.UserInfo.LoginID;
            newFeeRow.CreateByName = LocalData.UserInfo.LoginName;
            newFeeRow.Quantity = 1;
            newFeeRow.UnitPrice = 0;
            newFeeRow.Amount = newFeeRow.Quantity * newFeeRow.UnitPrice;
            newFeeRow.Way = FeeWay.AR;
            if (ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(newFeeRow.CurrencyID))
                newFeeRow.CurrencyID = _defaultCurrencyID;

            newFeeRow.ChargingCodeID = Guid.Empty;
            newFeeRow.ChargingCodeName = string.Empty;



            this.gridViewFee.ClearSorting();
            //if (bsFee.List.Count == 0)
            //{
            bsFee.Add(newFeeRow);
            //}
            //else
            //{
            //    (bsFee.List as List<OceanBookingFeeList>).Add(newFeeRow);
            //}
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
            List<OceanBookingFeeList> list = SelectRows;
            if (list == null || list.Count == 0) return;

            if (!PartLoader.EnquireIsDeleteCurrentData())
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
                    OceanExportService.RemoveOceanOrderFeeList(needRemoveIDs.ToArray(),_CompanyID, LocalData.UserInfo.LoginID, needRemoveUpdateDate.ToArray());
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

                if (row != null && row as OceanBookingFeeList != null)
                {
                    OceanBookingFeeList fee = row as OceanBookingFeeList;
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
                if (_isChanged == false)
                {
                    List<OceanBookingFeeList> source = bsFee.DataSource as List<OceanBookingFeeList>;
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

            foreach (var item in bsFee.List)
            {
                if ((item as OceanBookingFeeList).Validate
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
            get { return bsFee.DataSource as List<OceanBookingFeeList>; }
        }

        public void SetSource(object value)
        {
            if (value == null) return;
            if ((value as List<OceanBookingFeeList>).Count == 0) return;
            _isChanged = false;
            bsFee.DataSource = value;
            bsFee.ResetBindings(false);
        }

        public void SetService(WorkItem workitem)
        {
            Workitem = workitem;
            InitControls();
        }

        #endregion

        #region 默认填充费用信息 (英文版 币种美元，中文版币种人民币)

        /// <summary>
        /// 判断是否存在相同的费用名称
        /// </summary>
        /// <param name="ocean"></param>
        /// <returns></returns>
        public bool WhetherInsert(OceanBookingFeeList ocean)
        {
            bool flg = true;
            foreach (var item in bsFee.List)
            {
                if ((item as OceanBookingFeeList).ChargingCodeID == ocean.ChargingCodeID)
                {
                    flg = false;
                }

            }
            return flg;
        }


        /// <summary>
        /// 插入默认费用信息
        /// </summary>
        public void InsertDefault()
        {
            _isChanged = true;
            var hyf = new OceanBookingFeeList();
            hyf.ID = Guid.Empty;
            hyf.CreateDate = DateTime.Now;
            hyf.CreateByID = LocalData.UserInfo.LoginID;
            hyf.CreateByName = LocalData.UserInfo.LoginName;
            hyf.Quantity = 1;
            hyf.Amount = hyf.Quantity * hyf.UnitPrice;
            hyf.Way = FeeWay.AR;
            hyf.CurrencyID =
                LocalData.IsEnglish
                    ? new Guid("D67186CE-8B2C-4A75-81F1-A4FE3CC12DE9")
                    : new Guid("DEB5F402-B6C0-4491-B247-B75C3EDA7976");
            hyf.ChargingCodeName = LocalData.IsEnglish ? "OCEAN FREIGHT" : "海运费";
            hyf.ChargingCodeID = new Guid("E746D7BA-8077-4297-ABF4-C5AD28833405");
            hyf.IsDirty = true;
            bsFee.Add(hyf);


            var wj = new OceanBookingFeeList();
            wj.ID = Guid.Empty;
            wj.CreateDate = DateTime.Now;
            wj.CreateByID = LocalData.UserInfo.LoginID;
            wj.CreateByName = LocalData.UserInfo.LoginName;
            wj.Quantity = 1;
            wj.Amount = hyf.Quantity * hyf.UnitPrice;
            wj.Way = FeeWay.AR;
            wj.CurrencyID =
               LocalData.IsEnglish ? new Guid("D67186CE-8B2C-4A75-81F1-A4FE3CC12DE9") : new Guid("DEB5F402-B6C0-4491-B247-B75C3EDA7976");
            wj.ChargingCodeName = LocalData.IsEnglish ? "DOCUMENTATION FEE" : "文件费";
            wj.ChargingCodeID = new Guid("6F1F78A2-0ED8-4514-B7A8-F5994B043145");
            wj.IsDirty = true;
            bsFee.Add(wj);


            var orc = new OceanBookingFeeList();
            orc.ID = Guid.Empty;
            orc.CreateDate = DateTime.Now;
            orc.CreateByID = LocalData.UserInfo.LoginID;
            orc.CreateByName = LocalData.UserInfo.LoginName;
            orc.Quantity = 1;
            orc.Amount = hyf.Quantity * hyf.UnitPrice;
            orc.Way = FeeWay.AR;
            orc.CurrencyID =
               LocalData.IsEnglish ? new Guid("D67186CE-8B2C-4A75-81F1-A4FE3CC12DE9") : new Guid("DEB5F402-B6C0-4491-B247-B75C3EDA7976");
            orc.ChargingCodeName = LocalData.IsEnglish ? "ORIGINAL RECEIVING CHARGE" : "启运港接货费";
            orc.ChargingCodeID = new Guid("AACB9389-FAEE-43EF-8BE3-76F645DEE8A8");
            orc.IsDirty = true;
            bsFee.Add(orc);


            OceanBookingFeeList seal = new OceanBookingFeeList();
            seal.ID = Guid.Empty;
            seal.CreateDate = DateTime.Now;
            seal.CreateByID = LocalData.UserInfo.LoginID;
            seal.CreateByName = LocalData.UserInfo.LoginName;
            seal.Quantity = 1;
            seal.Amount = hyf.Quantity * hyf.UnitPrice;
            seal.Way = FeeWay.AR;
            seal.CurrencyID =
               LocalData.IsEnglish ? new Guid("D67186CE-8B2C-4A75-81F1-A4FE3CC12DE9") : new Guid("DEB5F402-B6C0-4491-B247-B75C3EDA7976");
            seal.ChargingCodeName = "SEAL FEE";
            seal.ChargingCodeID = new Guid("719AB43F-CAED-4B42-90BA-8AC15F53223D");
            seal.IsDirty = true;
            bsFee.Add(seal);



            //OceanBookingFeeList hac = new OceanBookingFeeList
            //{
            //    Way = FeeWay.AR,
            //    UnitPrice = 0,
            //    Quantity = 1,
            //    CurrencyID = LocalData.IsEnglish ? new Guid("D67186CE-8B2C-4A75-81F1-A4FE3CC12DE9") : new Guid("DEB5F402-B6C0-4491-B247-B75C3EDA7976"),
            //    Amount = (decimal)0.00,
            //    Remark = string.Empty,
            //    ChargingCodeName = LocalData.IsEnglish ? "HARBOR CHARGE" : "港杂费",
            //    ChargingCodeID = new Guid("7F6183C5-207D-494D-A93C-B8042A7BD8E8")
            //};
            //bsFee.Insert(10, hac);

            //OceanBookingFeeList yzf = new OceanBookingFeeList
            //{
            //    Way = FeeWay.AR,
            //    UnitPrice = 0,
            //    Quantity = 1,
            //    CurrencyID = LocalData.IsEnglish ? new Guid("D67186CE-8B2C-4A75-81F1-A4FE3CC12DE9") : new Guid("DEB5F402-B6C0-4491-B247-B75C3EDA7976"),
            //    Amount = (decimal)0.00,
            //    Remark = string.Empty,
            //    ChargingCodeName = LocalData.IsEnglish ? "YZF" : "运杂费",
            //    ChargingCodeID = new Guid("7895DA54-E520-42D9-AA82-AF3AE845C89B")
            //};
            //bsFee.Insert(11, yzf);
        }


        /// <summary>
        /// 追加拖车费用
        /// </summary>
        public void TrailerFee()
        {
            OceanBookingFeeList tcf = new OceanBookingFeeList();
            tcf.ID = Guid.Empty;
            tcf.CreateDate = DateTime.Now;
            tcf.CreateByID = LocalData.UserInfo.LoginID;
            tcf.CreateByName = LocalData.UserInfo.LoginName;
            tcf.Quantity = 1;
            tcf.Amount = tcf.Quantity * tcf.UnitPrice;
            tcf.Way = FeeWay.AR;
            tcf.CurrencyID =
               LocalData.IsEnglish ? new Guid("D67186CE-8B2C-4A75-81F1-A4FE3CC12DE9") : new Guid("DEB5F402-B6C0-4491-B247-B75C3EDA7976");
            tcf.ChargingCodeName = LocalData.IsEnglish ? "TRUCKING CHARGE" : "拖车费";
            tcf.ChargingCodeID = new Guid("16B8F7B5-21E5-4C07-8BA5-0CE9154B05F9");
            tcf.IsDirty = true;
            if (WhetherInsert(tcf))
            {
                bsFee.Add(tcf);
            }

        }

        /// <summary>
        /// 目的港清关
        /// </summary>
        public void CustomsClearance()
        {
            OceanBookingFeeList dccc = new OceanBookingFeeList();
            dccc.ID = Guid.Empty;
            dccc.CreateDate = DateTime.Now;
            dccc.CreateByID = LocalData.UserInfo.LoginID;
            dccc.CreateByName = LocalData.UserInfo.LoginName;
            dccc.Quantity = 1;
            dccc.Amount = dccc.Quantity * dccc.UnitPrice;
            dccc.Way = FeeWay.AR;
            dccc.CurrencyID =
               LocalData.IsEnglish ? new Guid("D67186CE-8B2C-4A75-81F1-A4FE3CC12DE9") : new Guid("DEB5F402-B6C0-4491-B247-B75C3EDA7976");
            dccc.ChargingCodeName = LocalData.IsEnglish ? "DEST. Customs Clearance Charge" : "目的港清关费";
            dccc.ChargingCodeID = new Guid("F7B1EBB0-C266-4937-8D5D-171B2FF8D81A");
            dccc.IsDirty = true;
            if (WhetherInsert(dccc))
            {
                bsFee.Add(dccc);
            }
        }

        /// <summary>
        /// 美线添加费用
        /// </summary>
        public void Ams()
        {
            OceanBookingFeeList Ams = new OceanBookingFeeList();
            Ams.ID = Guid.Empty;
            Ams.CreateDate = DateTime.Now;
            Ams.CreateByID = LocalData.UserInfo.LoginID;
            Ams.CreateByName = LocalData.UserInfo.LoginName;
            Ams.Quantity = 1;
            Ams.Amount = Ams.Quantity * Ams.UnitPrice;
            Ams.Way = FeeWay.AR;
            Ams.CurrencyID =
               LocalData.IsEnglish ? new Guid("D67186CE-8B2C-4A75-81F1-A4FE3CC12DE9") : new Guid("DEB5F402-B6C0-4491-B247-B75C3EDA7976");
            Ams.ChargingCodeName = LocalData.IsEnglish ? "AMS FILING CHARGES" : "美国自动报关费";
            Ams.ChargingCodeID = new Guid("824D2382-EB05-4EF2-93CF-D81F0F7F0CEA");
            Ams.IsDirty = true;
            if (WhetherInsert(Ams))
            {
                bsFee.Add(Ams);
            }

        }


        /// <summary>
        /// 欧线添加费用
        /// </summary>
        public void Ens()
        {
            OceanBookingFeeList Ens = new OceanBookingFeeList();
            Ens.ID = Guid.Empty;
            Ens.CreateDate = DateTime.Now;
            Ens.CreateByID = LocalData.UserInfo.LoginID;
            Ens.CreateByName = LocalData.UserInfo.LoginName;
            Ens.Quantity = 1;
            Ens.Amount = Ens.Quantity * Ens.UnitPrice;
            Ens.Way = FeeWay.AR;
            Ens.CurrencyID =
               LocalData.IsEnglish ? new Guid("D67186CE-8B2C-4A75-81F1-A4FE3CC12DE9") : new Guid("DEB5F402-B6C0-4491-B247-B75C3EDA7976");
            Ens.ChargingCodeName = LocalData.IsEnglish ? "Entry Summary Declaration" : "欧盟反恐单证费";
            Ens.ChargingCodeID = new Guid("8E8E2B10-78E1-403C-BE52-2DEA001F574A");
            Ens.IsDirty = true;
            if (WhetherInsert(Ens))
            {
                bsFee.Add(Ens);
            }
        }
        #endregion

        /// <summary>
        /// 公司发生改变时
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
