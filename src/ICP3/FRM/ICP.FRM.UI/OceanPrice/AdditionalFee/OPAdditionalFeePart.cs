using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FRM.ServiceInterface;
using ICP.FRM.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ICP.FRM.UI.OceanPrice
{
    /// <summary>
    /// 附加费编辑面板
    /// </summary>
    [ToolboxItem(false)]
    public partial class OPAdditionalFeePart : BaseListEditPart
    {
        #region 服务注入

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IOceanPriceService OceanPriceService
        {
            get
            {
                return ServiceClient.GetService<IOceanPriceService>();
            }
        }

        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        public OceanPriceUIDataHelper OceanPriceUIDataHelper
        {
            get
            {
                return ClientHelper.Get<OceanPriceUIDataHelper, OceanPriceUIDataHelper>();
            }
        }

        #endregion

        #region 本地变量

        List<ClientAdditionalFeeList> CurrentSource
        {
            get { return bsList.DataSource as List<ClientAdditionalFeeList>; }
        }

        ClientAdditionalFeeList CurrentRow
        {
            get { return bsList.Current as ClientAdditionalFeeList; }
        }

        List<ClientAdditionalFeeList> SelectedFees
        {
            get
            {

                int[] rowIndexs = gvMain.GetSelectedRows();

                if (rowIndexs.Length == 0) return null;

                List<ClientAdditionalFeeList> tagers = new List<ClientAdditionalFeeList>();
                foreach (var item in rowIndexs)
                {
                    ClientAdditionalFeeList dr = gvMain.GetRow(item) as ClientAdditionalFeeList;
                    if (dr != null) tagers.Add(dr);
                }

                return tagers;
            }
        }

        public bool IsChanged
        {
            get
            {
                if (_LoadedOceanID.IsNullOrEmpty() || _parentList == null || _LoadedOceanID != _parentList.ID) return false;

                List<ClientAdditionalFeeList> source = CurrentSource;
                if (source == null || source.Count == 0) return false;

                foreach (var item in source)
                {
                    if (item.IsNew || item.IsDirty)
                        return true;
                }

                return false;
            }
        }

        internal void RefreshUIData()
        {
        }

        #endregion

        #region init
        /// <summary>
        /// 附加费编辑面板
        /// </summary>
        public OPAdditionalFeePart()
        {
            InitializeComponent();
            Enabled = false;
            Disposed += delegate
            {
                _parentList = null;
                gcMain.DataSource = null;
                bsList.PositionChanged -= bsList_PositionChanged;
                bsList.DataSource = null;
                bsList.Dispose();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            if (!DesignMode) { InitMessage(); }
        }

        private void InitMessage()
        {
            RegisterMessage("SaveSuccessfully", "Save Successfully");
            RegisterMessage("RemoveSuccessfully", "Remove Successfully");
            RegisterMessage("RemoveSelectedItem", "Are you sure you want to remove the selected item?");

            RegisterMessage("ValidateRate_20GP", "Price must  great than zero.");
            RegisterMessage("ValidateFromDate", "Duration(Form) must be less than Duration(To).");
            RegisterMessage("Publish", "&Publish");
            RegisterMessage("Pause", "&Pause");

            RegisterMessage("SelectOneFee", "You should select at least one Additional Fee.");
            RegisterMessage("GeneralInfoChanged", "General Info is changed, you should save it first.");
            RegisterMessage("BaseRatesInfoChanged", "Base Port Rates or Arbitrary Rates are changed, you should save them before the association.");

            RegisterMessage("AssociateBasicRatesTitel", "Associate Basic Rates");

            RegisterMessage("ValidateIsSpecialFee", "Additional Fee中费用代码{0}关联了{1}条运价,但未勾上[IsSpecialFee]");

            RegisterMessage("IsSpecialFeeNotCheck", "Fee Code: {0} 必须先勾上IsSpecial，才能进行Associate Base Rate");
            //NativeLanguageService.GetText(this, "SelectOneFee")

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                //ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm();

                InitControls();

                //ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm();
            }
        }

        private void InitControls()
        {
            Utility.ShowGridRowNo(gvMain);
            Utility.SetGridViewClickIndicatorHeader2SelectAll(gvMain);

            InitComboboxSource();
            SearchRegister();
        }
        Guid pchydID = Guid.Empty;
        string pchydName = string.Empty;
        private void InitComboboxSource()
        {
            #region Currency
            List<CurrencyList> currencyLists = OceanPriceUIDataHelper.Currencys;
            foreach (var item in currencyLists)
                rcmbCurrency.Properties.Items.Add(new ImageComboBoxItem(item.Code, item.ID));

            #endregion
        }


        /// <summary>
        /// 搜索类型为“货代”型的“客户”
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetConditionsForForwarding()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("CustomerType", CustomerType.Forwarding, false);
            conditions.AddWithValue("CodeApplyState", CustomerCodeApplyState.Passed, false);
            return conditions;
        }

        private Guid _solutionID = Guid.Empty;
        Guid SolutionID
        {
            get
            {
                if (_solutionID == Guid.Empty)
                {
                    ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
                    if (configureInfo != null)
                    {
                        _solutionID = configureInfo.SolutionID;
                    }
                }
                return _solutionID;
            }
        }
        SearchConditionCollection GetSolutionChargingCodeSearchCondition()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("SolutionID", SolutionID, false);
          
            return conditions;
        }

        /// <summary>
        /// 注册搜索器
        /// </summary>
        private void SearchRegister()
        {
            #region ChargingCode

            DataFindClientService.RegisterGridColumnFinder(colChargingCode
                                               , CommonFinderConstants.ChargingCodeFinder
                                               , new string[] { "ChargingCodeID", "ChargingCode", "ChargingCodeDescription" }
                                               , new string[] { "ChargingCodeID", "Code", "EName" }
                                               , GetSolutionChargingCodeSearchCondition);
            #endregion

            #region 注册客户搜索器

 
            DataFindClientService.RegisterGridColumnFinder(colCustomerID
              , CommonFinderConstants.CustoemrFinder
              ,new string[]{ "CustomerID","CustomerName"}
              ,new string[]{"ID",LocalData.IsEnglish ? "EName" : "CName"}
              ,GetConditionsForForwarding                
              );
            #endregion

        }


        #endregion

        #region GridViewEvent

        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            RefreshBarItemEnabled();
        }

        private void RefreshBarItemEnabled()
        {
            if (CurrentRow == null)
                barCopy.Enabled = barRemove.Enabled = false;
            else
                barCopy.Enabled = barRemove.Enabled = true;
        }

        private void gvMain_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0 || e.Column != colIsSpecialFee) return;

            ClientAdditionalFeeList dr = gvMain.GetRow(e.RowHandle) as ClientAdditionalFeeList;
            if (dr == null) return;

            if (e.Column == colIsSpecialFee)
            {
                dr.BeginEdit();
                dr.IsSpecialFee = !dr.IsSpecialFee;
            }
            gvMain.RefreshData();
        }

        #endregion

        #region Commond

        Guid _LoadedOceanID = Guid.Empty;
        [CommandHandler(OPCommonConstants.Command_TabChanged)]
        public void Command_TabChanged(object sender, EventArgs e)
        {
            try
            {

                if (Visible == false || _LoadedOceanID.IsNullOrEmpty() == false) return;
                Enabled = _parentList != null;
                if (_parentList != null && _parentList.ID.IsNullOrEmpty() == false)
                {

                    List<AdditionalFeeList> list = OceanPriceService.GetOceanAdditionalFees(_parentList.ID);
                    _LoadedOceanID = _parentList.ID;
                    DataSource = list;
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }

        #endregion

        #region interface

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }

        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }

        void BindingData(object data)
        {
            if (_parentList == null || _parentList.OceanUnits == null || _parentList.OceanUnits.Count == 0)
            {
                Enabled = false;
                bsList.DataSource = typeof(ClientAdditionalFeeList);
                return;
            }

            gvMain.ActiveFilterString = string.Empty;
            List<AdditionalFeeList> datas = data as List<AdditionalFeeList>;
            if (datas == null) datas = new List<AdditionalFeeList>();

            BulidGridViewColumnsByOceanUnits(_parentList.OceanUnits);
            List<ClientAdditionalFeeList> source = OceanPriceTransformHelper.TransformS2C(datas, _parentList.OceanUnits);
            //bool containsSpecialFee = false;
            foreach (var item in source)
            {
                item.Selected = true;
                //if(item.IsSpecialFee) containsSpecialFee=true; 
            }
            // barAssociate.Enabled = containsSpecialFee;


            bsList.DataSource = source;
            bsList.ResetBindings(false);

        }

        #region BulidColumns

        private void BulidGridViewColumnsByOceanUnits(List<OceanUnitList> units)
        {
            #region  SetVisible= false;
            colRate_45FR.Visible = false;
            colRate_40RF.Visible = false;
            colRate_45HT.Visible = false;
            colRate_20RF.Visible = false;
            colRate_20HQ.Visible = false;
            colRate_20TK.Visible = false;
            colRate_20GP.Visible = false;
            colRate_40TK.Visible = false;
            colRate_40OT.Visible = false;
            colRate_20FR.Visible = false;
            colRate_45GP.Visible = false;
            colRate_40GP.Visible = false;
            colRate_45RF.Visible = false;
            colRate_20RH.Visible = false;
            colRate_45OT.Visible = false;
            colRate_40NOR.Visible = false;
            colRate_40FR.Visible = false;
            colRate_20OT.Visible = false;
            colRate_45TK.Visible = false;
            colRate_20NOR.Visible = false;
            colRate_40HT.Visible = false;
            colRate_40RH.Visible = false;
            colRate_45RH.Visible = false;
            colRate_45HQ.Visible = false;
            colRate_20HT.Visible = false;
            colRate_40HQ.Visible = false;
            colRate_53HQ.Visible = false;
            #endregion

            int visibleIndex = 5;

            foreach (var item in units)
            {
                #region  SetVisible= true;
                switch (item.UnitName)
                {
                    case "20GP": colRate_20GP.VisibleIndex = visibleIndex + 1; break;
                    case "40GP": colRate_40GP.VisibleIndex = visibleIndex + 2; break;
                    case "40HQ": colRate_40HQ.VisibleIndex = visibleIndex + 3; break;
                    case "45HQ": colRate_45HQ.VisibleIndex = visibleIndex + 4; break;
                    case "20NOR": colRate_20NOR.VisibleIndex = visibleIndex + 5; break;
                    case "40NOR": colRate_40NOR.VisibleIndex = visibleIndex + 6; break;

                    case "20FR": colRate_20FR.VisibleIndex = visibleIndex + 7; break;
                    case "20RH": colRate_20RH.VisibleIndex = visibleIndex + 8; break;
                    case "20RF": colRate_20RF.VisibleIndex = visibleIndex + 9; break;
                    case "20HQ": colRate_20HQ.VisibleIndex = visibleIndex + 10; break;
                    case "20TK": colRate_20TK.VisibleIndex = visibleIndex + 11; break;
                    case "20OT": colRate_20OT.VisibleIndex = visibleIndex + 12; break;
                    case "20HT": colRate_20HT.VisibleIndex = visibleIndex + 13; break;

                    case "40TK": colRate_40TK.VisibleIndex = visibleIndex + 14; break;
                    case "40OT": colRate_40OT.VisibleIndex = visibleIndex + 15; break;
                    case "40FR": colRate_40FR.VisibleIndex = visibleIndex + 16; break;
                    case "40HT": colRate_40HT.VisibleIndex = visibleIndex + 17; break;
                    case "40RH": colRate_40RH.VisibleIndex = visibleIndex + 18; break;
                    case "40RF": colRate_40RF.VisibleIndex = visibleIndex + 19; break;

                    case "45GP": colRate_45GP.VisibleIndex = visibleIndex + 20; break;
                    case "45RF": colRate_45RF.VisibleIndex = visibleIndex + 21; break;
                    case "45HT": colRate_45HT.VisibleIndex = visibleIndex + 22; break;
                    case "45FR": colRate_45FR.VisibleIndex = visibleIndex + 23; break;
                    case "45OT": colRate_45OT.VisibleIndex = visibleIndex + 24; break;
                    case "45TK": colRate_45TK.VisibleIndex = visibleIndex + 25; break;
                    case "45RH": colRate_45RH.VisibleIndex = visibleIndex + 26; break;

                    case "53HQ": colRate_53HQ.VisibleIndex = visibleIndex + 27; break;
                }

                #endregion
            }

            colPercent.VisibleIndex = visibleIndex + 28;
            colCurrencyID.VisibleIndex = visibleIndex + 29;
            colFromDate.VisibleIndex = visibleIndex + 30;
            colToDate.VisibleIndex = visibleIndex + 31;
            colRemark.VisibleIndex = visibleIndex + 32;


        }

        #endregion

        #endregion

        #region IEditPart 成员

        public override void EndEdit()
        {
            Validate();
            bsList.EndEdit();
        }

        #endregion

        #region IPart 成员
        OceanList _parentList = null;
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key == "ParentList")
                {
                    _parentList = item.Value as OceanList;
                    if (_parentList == null
                        || _parentList.IsNew
                        || _parentList.Permission < OceanPermission.Edit
                        || _parentList.OceanUnits == null
                        || _parentList.OceanUnits.Count == 0)
                        Enabled = false;
                    else
                    {
                        Enabled = true;
                    }

                    if (Visible == true && Enabled == true)
                    {
                        List<AdditionalFeeList> list = OceanPriceService.GetOceanAdditionalFees(_parentList.ID);
                        _LoadedOceanID = _parentList.ID;
                        DataSource = list;

                        
                    }
                    else _LoadedOceanID = Guid.Empty;

                    #region  刷新 Publish按钮状态
                    if (_parentList == null)
                    {
                        barPublish.Enabled = false;
                    }
                    else
                    {
                        if (_parentList.State == OceanState.Expired)
                            barPublish.Enabled = false;
                        else
                            barPublish.Enabled = true;

                        if (_parentList.State == OceanState.Expired || _parentList.State == OceanState.Invalidated || _parentList.State == OceanState.Draft)
                        {
                            barPublish.Caption = NativeLanguageService.GetText(this, "Publish");
                            barSave.Enabled = true;
                            barRemove.Enabled = true;
                        }
                        else
                        {
                            barPublish.Caption = NativeLanguageService.GetText(this, "Pause");
                            barSave.Enabled = false;
                            barRemove.Enabled = false;
                        }
                    }

                    #endregion
                }
            }
        }
        #endregion

        #endregion

        #region 工作流

        #region 增删改
        /// <summary>
        /// 是否为发布状态
        /// </summary>
        /// <returns></returns>
        private bool IsPublish()
        {
            if (_parentList == null)
            {
                return false;
            }
            if (_parentList.State == OceanState.Published)
            {
                XtraMessageBox.Show("Please pause contract");
                return false;
            }
            return true;
        }
        private void barInsert_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsPublish())
            {
                return;
            }
            AddData();
        }

        private void AddData()
        {
            gvMain.ActiveFilterString = string.Empty;

            ClientAdditionalFeeList newData = new ClientAdditionalFeeList();
            OceanPriceTransformHelper.BulidNewAdditionalFeeData(newData, _parentList);
            newData.CurrencyID = OceanPriceUIDataHelper.USDCurrency.ID;
            newData.CurrencyName = OceanPriceUIDataHelper.USDCurrency.Code;

            newData.BulidRateToZeroByOceanUints(_parentList.OceanUnits);
            newData.BeginEdit();

            (bsList.List as List<ClientAdditionalFeeList>).Insert(0, newData);
            bsList.ResetBindings(false);

            gvMain.CancelSelection();
            gvMain.FocusedRowHandle = 0;
            gvMain.SelectCell(0, colChargingCode);
        }

        private void barCopy_ItemClick(object sender, ItemClickEventArgs e)
        {
            CopyData();
        }

        private void CopyData()
        {
            if (!IsPublish())
            {
                return;
            }
            List<ClientAdditionalFeeList> selecteds = SelectedFees;
            if (CurrentRow == null || selecteds == null || selecteds.Count == 0) return;

            gvMain.ActiveFilterString = string.Empty;

            List<ClientAdditionalFeeList> copyTager = new List<ClientAdditionalFeeList>();
            foreach (var item in selecteds)
            {
                ClientAdditionalFeeList newItem = Utility.Clone<ClientAdditionalFeeList>(item);
                OceanPriceTransformHelper.BulidNewAdditionalFeeData(newItem, _parentList);
                //newItem.BulidRateToZeroByOceanUints(_parentList.OceanUnits);
                item.BeginEdit();
                copyTager.Add(newItem);
            }


            List<ClientAdditionalFeeList> source = CurrentSource;
            foreach (var item in copyTager)
            {
                source.Insert(0, item);
            }
            bsList.DataSource = source;
            bsList.ResetBindings(false);
        }

        private void barRemove_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsPublish())
            {
                return;
            }
            DeleteData();
        }

        private void DeleteData()
        {
            gvMain.CloseEditor();
            List<ClientAdditionalFeeList> selecteds = SelectedFees;
            if (selecteds == null || selecteds.Count == 0) return;

            #region 询问
            DialogResult result = XtraMessageBox.Show(
                                             NativeLanguageService.GetText(this, "RemoveSelectedItem"),
                                              LocalData.IsEnglish ? "Tip" : "Tip",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            #endregion

            #region 构建需数据库中删除的数据

            List<Guid> needRemoveIDs = new List<Guid>();
            List<DateTime?> needRemoveUpdates = new List<DateTime?>();

            foreach (var item in selecteds)
            {
                if (item.IsNew) continue;

                needRemoveIDs.Add(item.ID);
                needRemoveUpdates.Add(item.UpdateDate);
            }
            #endregion

            try
            {
                if (needRemoveIDs.Count > 0)
                {
                    OceanPriceService.RemoveAdditionalFeeInfo(needRemoveIDs.ToArray(), LocalData.UserInfo.LoginID, needRemoveUpdates.ToArray());
                }

                List<ClientAdditionalFeeList> source = CurrentSource;

                foreach (var item in selecteds)
                {
                    source.Remove(item);
                }

                bsList.DataSource = source;
                bsList.ResetBindings(false);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "RemoveSuccessfully"));
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
        }

        #endregion

        #region Save

        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[OPCommonConstants.Command_SaveData].Execute();
        }

        internal List<ClientAdditionalFeeList> GetChangedItem()
        {
            List<ClientAdditionalFeeList> source = CurrentSource;
            List<ClientAdditionalFeeList> chengedItem = new List<ClientAdditionalFeeList>();
            foreach (var item in source)
            {
                if (item.IsNew || item.IsDirty) chengedItem.Add(item);
            }
            return chengedItem;
        }

        public bool ValidateData()
        {
            if (IsChanged == false) return true;

            gvMain.Focus();
            gcMain.Focus();

            Validate();
            bsList.EndEdit();

            List<ClientAdditionalFeeList> chengedItem = GetChangedItem();

            if (ValidateData(chengedItem) == false) return false;

            return true;
        }

        private bool ValidateData(List<ClientAdditionalFeeList> chengedItems)
        {
            gvMain.ActiveFilterString = string.Empty;

            if (chengedItems == null || chengedItems.Count == 0) return false;

            bool isSrcc = true;

            foreach (var item in chengedItems)
            {
                if (item.Validate(delegate(ValidateEventArgs e)
                {
                    if (item.ValidateHasRate() == false)
                    {
                        e.SetErrorInfo("Rate_20GP", NativeLanguageService.GetText(this, "ValidateRate_20GP"));
                    }
                    if (item.FromDate.HasValue && item.ToDate.HasValue && item.FromDate >= item.ToDate)
                    {
                        e.SetErrorInfo("FromDate", NativeLanguageService.GetText(this, "ValidateFromDate"));
                    }
                    if (item.AssociatedCount > 0 && !item.IsSpecialFee)
                    { 
                        string message=NativeLanguageService.GetText(this, "ValidateIsSpecialFee");
                        message = string.Format(message, item.ChargingCode, item.AssociatedCount);
                        //关联了N条，却没有勾上特殊费用
                        e.SetErrorInfo("IsSpecialFee", message);
                    }


                }) == false) isSrcc = false;
            }

            return isSrcc;
        }

        #endregion

        #region 发布
        private void barPublish_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[OPCommonConstants.Command_PublishPauseData].Execute();
        }
        #endregion

        #region 全选

        private void barSelectAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            GridHelper.ToggleSelectAllRows(gvMain);
            gvMain.RefreshData();
            
        }

        #endregion

        #region 关联
        private void barAssociate_ItemClick(object sender, ItemClickEventArgs e)
        {
            Associate();
        }
        /// <summary>
        /// 关联
        /// </summary>
        void Associate()
        {
            #region IsChanged

            if (IsChanged)
            {
                DialogResult result = XtraMessageBox.Show(
                                        NativeLanguageService.GetText(this, "GeneralInfoChanged")
                                        , "Tip"
                                        , MessageBoxButtons.YesNo
                                        , MessageBoxIcon.Question);

                if (result == DialogResult.No) return;

                barSave.PerformClick();
                return;
            }
            else if (_GetBastRatesIsChanged() == true)
            {
                DialogResult result = XtraMessageBox.Show(
                                        NativeLanguageService.GetText(this, "BaseRatesInfoChanged")
                                        , "Tip"
                                        , MessageBoxButtons.YesNo
                                        , MessageBoxIcon.Question);
                if (result == DialogResult.No) return;

                barSave.PerformClick();
                return;
            }
            #endregion

            List<ClientAdditionalFeeList> source = CurrentSource;
            if (source == null || source.Count == 0) { return; }

            List<ClientAdditionalFeeList> selecteSpecialFees = SelectedFees;
            if (selecteSpecialFees == null || selecteSpecialFees.Count == 0)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this, NativeLanguageService.GetText(this, "SelectOneFee"));
                return;
            }
            foreach (ClientAdditionalFeeList item in selecteSpecialFees)
            {
                if (!item.IsSpecialFee)
                {
                    string message = NativeLanguageService.GetText(this, "IsSpecialFeeNotCheck");
                    message=string.Format(message,item.ChargingCode);
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this, message);
                    return;
                }
            }


            AssociateBasicRatesPart af = Workitem.Items.AddNew<AssociateBasicRatesPart>();
            af.SetSource(_parentList, selecteSpecialFees);
            af.ChangedAdditional += delegate(Dictionary<Guid, List<Guid>> additionals)
            {
                foreach (var item in additionals)
                {
                    ClientAdditionalFeeList tager = source.Find(f => f.ID == item.Key);
                    if (tager != null) { tager.BaseRateIDs = item.Value; }
                }
                gvMain.RefreshData();
            };
            PartLoader.ShowDialog(af, NativeLanguageService.GetText(this, "AssociateBasicRatesTitel"), FormBorderStyle.Sizable);
        }

        #endregion

        #endregion

        #region 启用发布按钮
        /// <summary>
        /// 设置为可以发布
        /// </summary>
        public void SetPublish()
        {
            barPublish.Enabled = true;
            barPublish.Caption = NativeLanguageService.GetText(this, "Publish");
        }
        #endregion

        #region GetBastRatesIsChanged
        //这里的代码是为了实现如果BaseRates有改变,在关联之前要提示

        /// <summary>
        ///  用委托的方式获取BaseRates是否改变
        /// </summary>
        GetBastRatesIsChanged _GetBastRatesIsChanged;
        /// <summary>
        /// 获取BaseRates是否改变
        /// </summary>
        /// <returns>否改变</returns>
        public delegate bool GetBastRatesIsChanged();
        /// <summary>
        /// 设置委托
        /// </summary>
        /// <param name="getIsChanged"></param>
        public void SetGetIsChangedMothed(GetBastRatesIsChanged getIsChanged)
        {
            _GetBastRatesIsChanged = getIsChanged;
        }

        #endregion

        #region 数据发生改变时
        private void gvMain_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column == colChargingCode)
            {
                if (e.Value != null)
                {
                    if (e.Value.ToString().ToUpper() == "CUF(C)".ToUpper())
                    {
                        if (pchydID == Guid.Empty)
                        {
                            List<CustomerList> customerList = CustomerService.GetCustomerListByList("SZPCHWLYD", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, null, null, null, null, null, null, null, null, null, 0);
                            if (customerList.Count > 0)
                            {
                                pchydID = customerList[0].ID;
                                pchydName = customerList[0].EName;
                            }
                        }
                        CurrentRow.CustomerID = pchydID;
                        CurrentRow.CustomerName = pchydName;
                        bsList.ResetCurrentItem();
                    }
                }
            }
        }
        #endregion




    }
}
