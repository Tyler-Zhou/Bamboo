using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils.Drawing;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.FRM.ServiceInterface;
using ICP.FRM.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.Utility;
using DevExpress.XtraTreeList.Columns;

namespace ICP.FRM.UI.InquireRates
{
    [ToolboxItem(false)]
    public partial class InquireAirRatesListPart : BaseListPart
    {
        #region 服务注入

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IInquireRatesService InquireRatesService
        {
            get
            {
                return ServiceClient.GetService<IInquireRatesService>();
            }
        }

        public IDataFindClientService DataFindClientService
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

        public InquireRatesUIDataHelper InquireRatesUIDataHelper
        {
            get
            {
                return ClientHelper.Get<InquireRatesUIDataHelper, InquireRatesUIDataHelper>();
            }
        }

        #endregion

        #region 本地变量

        InquierAirRatesResult _currentServiceSource = null;
        List<ClientInquierAirRate> _sourceClone = null;
        public Guid? DefaultCurrencyID
        {
            get
            {
                ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);

                if (configureInfo != null)
                {
                    return configureInfo.DefaultCurrencyID;
                }

                return null;
            }
        }

        public InquireAirRatesGeneralInfoPart GeneralInfoPart
        {
            get;
            set;
        }

        List<ClientInquierAirRate> CurrentSource
        {
            get { return bsList.DataSource as List<ClientInquierAirRate>; }
        }

        //ClientInquierAirRate CurrentRow
        //{
        //    get { return bsList.Current as ClientInquierAirRate; }

        //    set
        //    {
        //        ClientInquierAirRate currentrow = CurrentRow;
        //        currentrow = value;
        //    }
        //}

        List<ClientInquierAirRate> SelectedAirItem
        {
            get
            {
                if (treeMain.Selection == null || treeMain.Selection.Count == 0) return null;

                List<ClientInquierAirRate> tagers = new List<ClientInquierAirRate>();
                foreach (TreeListNode item in treeMain.Selection)
                {
                    ClientInquierAirRate bl = treeMain.GetDataRecordByNode(item) as ClientInquierAirRate;
                    tagers.Add(bl);
                }

                return tagers;
            }
        }

        public bool IsChanged
        {
            get
            {
                List<ClientInquierAirRate> source = CurrentSource;
                if (source == null || source.Count == 0) return false;

                foreach (var item in source)
                {
                    if (item.IsNew || item.IsDirty)
                        return true;
                }

                return false;
            }
        }

        #endregion

        #region init

        public InquireAirRatesListPart()
        {
            InitializeComponent();
            Disposed += delegate {
                _currentServiceSource = null;
                _para = null;
                _sourceClone = null;
                _visibleColumnsNameList = null;
                treeMain.DataSource = null;
                bsList.PositionChanged -= bsList_PositionChanged;
                bsList.DataSource = null;
                bsList.Dispose();
                CurrentChanged = null;
                CurrentChanging = null;
                //this.UnReadCountEvent = null;
                //this.RefreshAirDiscussingPartEvent = null;

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
            RegisterMessage("ValidatePOLSamePOD", "POD can not same as POL.");
            RegisterMessage("ValidateComm", "Comm must input.");
            RegisterMessage("ValidateSurCharge", "SurCharge must input.");
            RegisterMessage("ValidateDataExist", "BasePort - Data has exist.");
            RegisterMessage("ItemCodeDifferent", " Some Itemcode are conflicted because it has two or more different Commodities.");
            RegisterMessage("MaxScreen", "&MaxScreen");
            RegisterMessage("BrackScreen", "Brack(&M)");
            RegisterMessage("FilterPartTitel", "Base Port Rates Filter");
            RegisterMessage("SearchCommPartTitel", "Comm");
            RegisterMessage("AssociatedRatesPartTitel", "Associated Rates");
            RegisterMessage("BatchItemFaily", "Batch Item Faily.");
            RegisterMessage("RemoveSuccessfully", "Remove Successfully");
            RegisterMessage("SelectOneRate", "You should select at least one BasePort Rate.");
            RegisterMessage("GeneralInfoChanged", "General Info is changed, you should save it first.");
            RegisterMessage("ImportFailed", "Importing Base Port Rates is failed.\r\n");
            RegisterMessage("ImportingSuccessfully", "Importing Base Port Rates is successful with {0} records.");
            RegisterMessage("ValidateItemExist", "Some items are existing in the Base Port Rates {0};");
            RegisterMessage("ValidateItemCodeDifferent", "The Itemcode –{0}-- are conflicted because it has two or more different Commodities.");
            RegisterMessage("CurrentChanging", "The current record is changed and has not yet been saved. \r\nClicks Yes to save the changes and go to the next record. \r\nClicks No to desert all of changing. \r\nClicks Cancel to return.");
          
            RegisterMessage("Publish", "&Publish");
            RegisterMessage("Pause", "&Pause");
            RegisterMessage("ValidateItemCode", "ItemCode must input.");

            RegisterMessage("ChangeSameName", "Would you change all data of the same name?");

        }

        protected override void OnLoad(EventArgs e)
        {
            //ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm();
            base.OnLoad(e);
            InitControls();
            //ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm();
        }

        //[EventPublication(InquireRatesCommandConstants.Command_UnReadCount)]
        //public event EventHandler<DataEventArgs<List<UnReadDiscussingCount>>> UnReadCountEvent;


        //[EventPublication(InquireRatesCommandConstants.Command_RefreshAirDiscussingPart)]
        //public event EventHandler<DataEventArgs<object>> RefreshAirDiscussingPartEvent;

        private void InitControls()
        {
            InitComboboxSource();
            SearchRegister();     
        }

        void InitComboboxSource()
        {
            #region 币种

            foreach (var curr in InquireRatesUIDataHelper.Currencys)
            {
                cmbCurrency.Properties.Items.Add(new ImageComboBoxItem(curr.Code, curr.ID));
            }

            cmbCurrency.SelectedIndexChanged += delegate
            {
                treeMain.CloseEditor();
                ClientInquierAirRate currentrow = CurrentRow;
                if (currentrow != null)
                {
                    currentrow.CurrencyName = InquireRatesUIDataHelper.Currencys.Find(t => t.ID == currentrow.CurrencyID).Code;
                }
            };

            #endregion

            #region 航线

            foreach (var line in InquireRatesUIDataHelper.ShippingLines)
            {
                cmbShipline.Properties.Items.Add(new ImageComboBoxItem(line.EName, line.ID));
            }

            cmbShipline.SelectedIndexChanged += delegate
            {
                treeMain.CloseEditor();
                ClientInquierAirRate currentrow = CurrentRow;
                if (currentrow != null)
                {
                    currentrow.ShippingLineName = InquireRatesUIDataHelper.ShippingLines.Find(t => t.ID == currentrow.ShippingLineID).Code;
                }
            };

            #endregion
        }

        private void SearchRegister()
        {
            // #region pol

            // dfService.RegisterGridColumnFinder(colPOL, CommonFinderConstants.AirLocationFinder
            //  , "POLID", "POLName"
            //  , "ID", LocalData.IsEnglish ? "EName" : "EName"
            //  , delegate(object befocePickedData, object afterPickedData)
            //  {
            //      ClientInquierAirRate befoceChangedRow = befocePickedData as ClientInquierAirRate;
            //      ClientInquierAirRate afterChangedRow = afterPickedData as ClientInquierAirRate;

            //      if (befoceChangedRow != null && afterChangedRow != null)
            //      {
            //          List<ClientInquierAirRate> source = CurrentSource;
            //          List<ClientInquierAirRate> sameData = source.FindAll(s => s.POLID == befoceChangedRow.POLID);
            //          if (sameData != null && sameData.Count > 0)
            //          {
            //              DialogResult result = XtraMessageBox.Show(NativeLanguageService.GetText(this, "ChangeSameName"),
            //                                      LocalData.IsEnglish ? "Tip" : "提示",
            //                                      MessageBoxButtons.YesNo,
            //                                      MessageBoxIcon.Question);
            //              if (result == DialogResult.Yes)
            //              {
            //                  foreach (var item in sameData)
            //                  {
            //                      item.POLID = afterChangedRow.POLID;
            //                      item.POLName = afterChangedRow.POLName;
            //                      item.IsDirty = true;
            //                  }

            //                  treeMain.RefreshDataSource();
            //              }
            //          }
            //      }
            //  });

            // #endregion

            // #region pod

            // dfService.RegisterGridColumnFinder(colPOD
            //, CommonFinderConstants.AirLocationFinder
            //, "PODID"
            //, "PODName"
            //, "ID"
            //, LocalData.IsEnglish ? "EName" : "EName"
            //, delegate(object befocePickedData, object afterPickedData)
            //{
            //    ClientInquierAirRate befoceChangedRow = befocePickedData as ClientInquierAirRate;
            //    ClientInquierAirRate afterChangedRow = afterPickedData as ClientInquierAirRate;

            //    if (befoceChangedRow != null && afterChangedRow != null)
            //    {
            //        List<ClientInquierAirRate> source = CurrentSource;
            //        List<ClientInquierAirRate> sameData = source.FindAll(s => s.PODID == befoceChangedRow.PODID);
            //        if (sameData != null && sameData.Count > 0)
            //        {
            //            DialogResult result = XtraMessageBox.Show(NativeLanguageService.GetText(this, "ChangeSameName"),
            //                                    LocalData.IsEnglish ? "Tip" : "提示",
            //                                    MessageBoxButtons.YesNo,
            //                                    MessageBoxIcon.Question);
            //            if (result == DialogResult.Yes)
            //            {
            //                foreach (var item in sameData)
            //                {
            //                    item.PODID = afterChangedRow.PODID;
            //                    item.PODName = afterChangedRow.PODName;
            //                    item.IsDirty = true;
            //                }

            //                treeMain.RefreshDataSource();
            //            }
            //        }
            //    }
            //});
            // #endregion

            // #region PlaceOfDelivery
            // dfService.RegisterGridColumnFinder(colPlaceOfDelivery
            //, CommonFinderConstants.AirLocationFinder
            //, "PlaceOfDeliveryID"
            //, "PlaceOfDeliveryName"
            //, "ID"
            //, LocalData.IsEnglish ? "EName" : "EName"
            //, delegate(object befocePickedData, object afterPickedData)
            //{
            //    ClientInquierAirRate befoceChangedRow = befocePickedData as ClientInquierAirRate;
            //    ClientInquierAirRate afterChangedRow = afterPickedData as ClientInquierAirRate;

            //    if (befoceChangedRow != null && afterChangedRow != null)
            //    {
            //        List<ClientInquierAirRate> source = CurrentSource;
            //        List<ClientInquierAirRate> sameData = source.FindAll(s => s.PlaceOfDeliveryID == befoceChangedRow.PlaceOfDeliveryID);
            //        if (sameData != null && sameData.Count > 0)
            //        {
            //            DialogResult result = XtraMessageBox.Show(NativeLanguageService.GetText(this, "ChangeSameName"),
            //                                    LocalData.IsEnglish ? "Tip" : "提示",
            //                                    MessageBoxButtons.YesNo,
            //                                    MessageBoxIcon.Question);
            //            if (result == DialogResult.Yes)
            //            {
            //                foreach (var item in sameData)
            //                {
            //                    item.PlaceOfDeliveryID = afterChangedRow.PlaceOfDeliveryID;
            //                    item.PlaceOfDeliveryName = afterChangedRow.PlaceOfDeliveryName;
            //                    item.IsDirty = true;
            //                }

            //                treeMain.RefreshDataSource();
            //            }
            //        }
            //    }
            //});
            // #endregion

            #region Carrier
            DataFindClientService.RegisterTreeListColumnFinder(
                treeMain
                , colCarrier
           , CommonFinderConstants.CustomerAirlineFinder
           , new string[] { "CarrierID", "CarrierName" }
           , new string[] { "ID", LocalData.IsEnglish ? "EShortName" : "CShortName" }
           , null
           , delegate(object befocePickedData, object afterPickedData)
           {
               //ClientInquierAirRate befoceChangedRow = befocePickedData as ClientInquierAirRate;
               //ClientInquierAirRate afterChangedRow = afterPickedData as ClientInquierAirRate;

               //if (befoceChangedRow != null && afterChangedRow != null)
               //{
               //    List<ClientInquierAirRate> source = CurrentSource;
               //    List<ClientInquierAirRate> sameData = source.FindAll(s => s.CarrierID == befoceChangedRow.CarrierID);
               //    if (sameData != null && sameData.Count > 0)
               //    {
               //        DialogResult result = XtraMessageBox.Show(NativeLanguageService.GetText(this, "ChangeSameName"),
               //                                LocalData.IsEnglish ? "Tip" : "提示",
               //                                MessageBoxButtons.YesNo,
               //                                MessageBoxIcon.Question);
               //        if (result == DialogResult.Yes)
               //        {
               //            foreach (var item in sameData)
               //            {
               //                item.CarrierID = afterChangedRow.CarrierID;
               //                item.CarrierName = afterChangedRow.CarrierName;
               //                item.IsDirty = true;
               //            }

               //            treeMain.RefreshDataSource();
               //        }
               //    }
               //}
           });

            #endregion
        }

        #endregion

        #region 工作流

        #region 增删改

        private void barInsert_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddData();
        }

        private void AddData()
        {
            //ClientBasePortList newData = new ClientBasePortList();
            //AirPriceTransformHelper.BulidNewBasePortData(newData, _parentList);
            //newData.BulidRateToZeroByAirUints(_parentList.AirUnits);
            //newData.BeginEdit();

            //(bsList.List as List<ClientBasePortList>).Insert(0, newData);
            //bsList.ResetBindings(false);

            //gvMain.CancelSelection();
            //gvMain.FocusedRowHandle = 0;
            //gvMain.SelectCell(0, colAccount);
        }

        private void barCopy_ItemClick(object sender, ItemClickEventArgs e)
        {
            CopyData();
        }

        private void CopyData()
        {
            List<ClientInquierAirRate> selecteds = SelectedAirItem;
            if (CurrentRow == null || selecteds == null || selecteds.Count == 0) return;


            List<ClientInquierAirRate> copyTager = new List<ClientInquierAirRate>();
            foreach (var item in selecteds)
            {
                ClientInquierAirRate newItem = Utility.Clone<ClientInquierAirRate>(item);
                newItem.ID = Guid.Empty;
                newItem.InquireByID = LocalData.UserInfo.LoginID;
                newItem.InquireByName = LocalData.UserInfo.LoginName;
                newItem.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
                //newItem.DurationFrom = _parentList.FromDate;
                //newItem.DurationTo = _parentList.ToDate;
                newItem.UpdateDate = null;
                
                item.BeginEdit();
                copyTager.Add(newItem);
            }


            List<ClientInquierAirRate> source = CurrentSource;
            foreach (var item in copyTager)
            {
                source.Insert(0, item);
            }

            bsList.DataSource = source;
            bsList.ResetBindings(false);
            treeMain.ExpandAll();
        }

        #region Save

        //private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    //Workitem.Commands[OPCommonConstants.Command_SaveData].Execute();
        //}

        internal void RefreshUIData()
        {
            List<ClientInquierAirRate> source = CurrentSource;
            //source = source.OrderByDescending(b => b.No).ToList();
            bsList.DataSource = source;
            treeMain.RefreshDataSource();
        }

        internal List<ClientInquierAirRate> GetChangedItem()
        {
            List<ClientInquierAirRate> source = CurrentSource;
            List<ClientInquierAirRate> chengedItem = new List<ClientInquierAirRate>();
            foreach (var item in source)
            {
                if (item.IsNew || item.IsDirty) chengedItem.Add(item);
            }
            return chengedItem;
        }

        public bool ValidateData()
        {
            if (IsChanged == false) return true;

            Validate();
            bsList.EndEdit();

            List<ClientInquierAirRate> chengedItem = GetChangedItem();

            //if (this.ValidateData(chengedItem) == false) return false;  要的啊

            return true;
        }
        ////pearl
        //private bool ValidateData(List<ClientInquierAirRate> chengedItems)
        //{
        //    gvMain.ActiveFilterString = string.Empty;

        //    if (chengedItems == null || chengedItems.Count == 0) return false;

        //    bool isSrcc = true;

        //    foreach (var item in chengedItems)
        //    {
        //        string errorMessage = string.Empty;
        //        item.ErrorInfo = string.Empty;

        //        if (item.Validate(ref errorMessage, delegate(ValidateEventArgs e)
        //        {
        //            if (item.ValidateHasRate() == false)
        //                e.SetErrorInfo("Rate_20GP", NativeLanguageService.GetText(this, "ValidateRate_20GP"));

        //            if (item.FromDate.HasValue && item.ToDate.HasValue && item.FromDate >= item.ToDate)
        //                e.SetErrorInfo("FromDate", NativeLanguageService.GetText(this, "ValidateFromDate"));

        //            if (item.POLID.IsNullOrEmpty() == false && item.PODID.IsNullOrEmpty() == false && item.PODID == item.POLID)
        //                e.SetErrorInfo("PODName", NativeLanguageService.GetText(this, "ValidatePOLSamePOD"));

        //            if (item.Comm.IsNullOrEmpty())
        //                e.SetErrorInfo("Comm", NativeLanguageService.GetText(this, "ValidateComm"));

        //        }) == false) isSrcc = false;

        //        item.ErrorInfo = errorMessage;
        //    }

        //    #region  //验证唯一键定义 Account+ POL+ VIA+ POD+ Delivery +Origin Arb+ Dest Arb+item code + Term

        //    List<ClientBasePortList> oldSouce = CurrentSource.FindAll(s => s.IsNew == false && s.IsDirty == false);
        //    List<ClientBasePortList> newItems = CurrentSource.FindAll(s => s.IsNew || s.IsDirty);

        //    bool itemCodeCommDifferent = false;
        //    bool isExist = ValidateHasExistItem(oldSouce, newItems, ref itemCodeCommDifferent);

        //    if (isExist || itemCodeCommDifferent) isSrcc = false;

        //    if (isExist) LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), NativeLanguageService.GetText(this, "ValidateDataExist"));

        //    if (itemCodeCommDifferent) LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), NativeLanguageService.GetText(this, "ItemCodeDifferent"));

        //    #endregion

        //    if (isSrcc == false) gvMain.RefreshData();
        //    return isSrcc;
        //}

        #endregion

        #endregion     

        #endregion

        #region Comm


        /// <summary>
        /// 进入网格editer时记录text,如果在离开网格时有改变,就执行搜索
        /// </summary>
        string _enterText = string.Empty;
        private void rbtnEditComm_Enter(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                _enterText = ((TextEdit)(sender)).Text;
            }
        }

        int _BeforeLeaveRowHandle = -1;
        private void rbtnEditComm_Leave(object sender, EventArgs e)
        {
          //_BeforeLeaveRowHandle = treeMain.FocusedRowHandle;
           _BeforeLeaveRowHandle = treeMain.FocusedColumn.SortIndex;
             string leaveText = ((TextEdit)(sender)).Text;
            if (leaveText != _enterText)
            {
                IRSearchCommPart scf = Workitem.Items.AddNew<IRSearchCommPart>();
                scf.SetSource(InquireRatesUIDataHelper.Commoditys, leaveText);
                DialogResult dr = PartLoader.ShowDialog(scf, NativeLanguageService.GetText(this, "SearchCommPartTitel"), FormBorderStyle.Sizable);
                if (dr == DialogResult.OK && _BeforeLeaveRowHandle >= 0)
                {
                    ClientInquierAirRate row = treeMain.GetDataRecordByNode(treeMain.FindNodeByID(_BeforeLeaveRowHandle)) as ClientInquierAirRate;
                    if (row != null)
                    {
                        row.Commodity = scf.CommString;
                        row.IsDirty = true;
                    }

                    bsList.EndEdit();
                    treeMain.RefreshDataSource();
                }
            }
        }

        private void rbtnEditComm_KeyDown(object sender, KeyEventArgs e)
        {
            ((TextEdit)(sender)).Leave -= new EventHandler(rbtnEditComm_Leave);



            if (e.KeyCode == Keys.Enter)
            {
                string leaveText = ((TextEdit)(sender)).Text;
                if (leaveText != _enterText)
                {
                    SearchComm();
                }
                _enterText = ((TextEdit)(sender)).Text;
            }

            ((TextEdit)(sender)).Leave += new EventHandler(rbtnEditComm_Leave);
        }

        private void rbtnEditComm_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                SearchComm();
            }
        }

        private void SearchComm()
        {
            if (CurrentRow == null) return;

            bsList.EndEdit();
            bsList.ResetCurrentItem();
            treeMain.CloseEditor();
            treeMain.RefreshDataSource();
            Validate();

            IRSearchCommPart scf = Workitem.Items.AddNew<IRSearchCommPart>();
            scf.SetSource(InquireRatesUIDataHelper.Commoditys, CurrentRow.Commodity);
            DialogResult dr = PartLoader.ShowDialog(scf, NativeLanguageService.GetText(this, "SearchCommPartTitel"), FormBorderStyle.Sizable);
            if (dr == DialogResult.OK)
            {
                CurrentRow.Commodity = scf.CommString;
                bsList.EndEdit();
                bsList.ResetCurrentItem();
                treeMain.CloseEditor();
                treeMain.RefreshDataSource();
                Validate();
            }
        }

        #endregion

        #region GridView Event

        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            SetEditState();

            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        void SetEditState()
        {
            ClientInquierAirRate data = (ClientInquierAirRate)bsList.Current;
            if (data == null) return;
            if (LocalData.UserInfo.LoginID == data.RespondByID)
            {
                foreach (var item in _visibleColumnsNameList)
                {

                    var findItem = (from d in data.RateUnitList where d.UnitName == item select d).SingleOrDefault();

                    if (findItem == null)
                    {
                        treeMain.Columns.ColumnByName("colRate_" + item.Replace("+", "")).OptionsColumn.AllowEdit = false;
                    }
                    else
                    {
                        treeMain.Columns.ColumnByName("colRate_" + item.Replace("+", "")).OptionsColumn.AllowEdit = true;
                    }
                }
                foreach (TreeListColumn sub in treeMain.Columns)
                {
                    sub.OptionsColumn.ReadOnly = false;
                }
            }
            else
            {
                foreach (TreeListColumn sub in treeMain.Columns)
                {
                    sub.OptionsColumn.ReadOnly = true;
                }
            }
        }
       
        private void treeMain_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node == null) return;
            ClientInquierAirRate listData = (ClientInquierAirRate)treeMain.GetDataRecordByNode(e.Node);
            if (listData == null) return;

            //IsNoPriceAll = true:都没回复价格的询价
            if (listData.IsNoPriceAll)
            {
                if (e.Node.Selected)
                    e.Appearance.BackColor = Color.LightBlue;
                else
                    e.Appearance.BackColor = Color.LightYellow;
            }
            else
                e.Appearance.BackColor = Color.White;

            e.Appearance.ForeColor = Color.Black;
        }

        /// <summary>
        /// 绘制行号
        /// </summary>
        private void treeMain_CustomDrawNodeIndicator(object sender, CustomDrawNodeIndicatorEventArgs e)
        {
            if (e.IsNodeIndicator == false || e.ObjectArgs == null) return;

            IndicatorObjectInfoArgs args = e.ObjectArgs as IndicatorObjectInfoArgs;
            if (args != null)
            {
                int rowNum = treeMain.GetVisibleIndexByNode(e.Node) + 1;
                args.DisplayText = rowNum.ToString();
            }
        }

        private void treeMain_BeforeFocusNode(object sender, BeforeFocusNodeEventArgs e)
        {
            if (e.OldNode == null || e.Node == null) return;
            ClientInquierAirRate dataOld = treeMain.GetDataRecordByNode(e.OldNode) as ClientInquierAirRate;
            ClientInquierAirRate dataNew = treeMain.GetDataRecordByNode(e.Node) as ClientInquierAirRate;
            if (dataOld.ID == dataNew.MainRecordID ||
               (dataOld.MainRecordID != null && dataOld.MainRecordID == dataNew.MainRecordID) ||
                dataOld.MainRecordID == dataNew.ID)
            {
            }
            else          //不是父子询价，切换行前数据有更新，提示保存
            {
                if (CurrentChanging != null)
                {
                    CancelEventArgs ce = new CancelEventArgs();
                    CurrentChanging(this, ce);
                    e.CanFocus = !ce.Cancel;
                }
            }
        }

        /// <summary>
        /// 当切换到某一行时，如果当前用户不等于该inquire rate的Respond  By，则该行只读，否则可编辑（栏位定义中为只读的，仍然为只读）。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeMain_ShowingEditor(object sender, CancelEventArgs e)
        {
            //DataRow row = this.gridView1.GetDataRow(this.gridView1.FocusedRowHandle);
            //if (row != null)
            //{
            //    if (Convert.ToInt32(row["Section"]) % 2 == 0)
            //    {
            //        e.Cancel = true;
            //    }
            //}

            //ClientInquierAirRate data = treeMain.GetDataRecordByNode(((LWTreeGridControl)sender).FocusedNode) as ClientInquierAirRate;
            //if (data != null && LocalData.UserInfo.LoginID != data.RespondByID)
            //{
            //    e.Cancel = true;   
            //}
        }

        #endregion

        #region Commond

        bool _hadLoadCurrentPage = false;
        [CommandHandler(InquireRatesCommandConstants.Command_TabChanged)]
        public void Command_TabChanged(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (Visible == false || _hadLoadCurrentPage) return;

                if (_para != null)
                {
                    InquierAirRatesResult data = InquireRatesService.GetInquireAirRateList(_para.pol,
                        _para.delivery,
                        _para.pod,
                        _para.commodity,
                        _para.inquireOrRespondBy,
                        _para.isUnReply,
                        _para.durationFrom,
                        _para.durationTo,_para.StrQuery,
                        LocalData.UserInfo.LoginID);

                    DataSource = data;
                    _hadLoadCurrentPage = true;
                }
            }
        }

        [EventSubscription(InquireRatesCommandConstants.Command_Mail)]
        public void Command_Mail(object sender, DataEventArgs<object> e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (Visible == false) return;
                if (SelectedAirItem == null) return;

                string mailContent = string.Empty;
                foreach (var item in SelectedAirItem)
                {
                    mailContent += "AIRPORT OF DEPARTURE: " + item.POLName + Environment.NewLine;
                    mailContent += "AIRPORT OF DESTINATION: " + item.PODName + Environment.NewLine;
                    if (!string.IsNullOrEmpty(item.Commodity))
                        mailContent += "COMM: " + item.Commodity + Environment.NewLine;
                    else if (!string.IsNullOrEmpty(item.ExpCommodity))
                        mailContent += "COMM: " + item.ExpCommodity + Environment.NewLine;
                    mailContent += "VOLUME: " + Environment.NewLine;
                    mailContent += "OTHER: " + Environment.NewLine;
                }

                Clipboard.SetDataObject(mailContent);
            }
        }

        [EventSubscription(InquireRatesCommandConstants.Command_Copy)]
        public void Command_Copy(object sender, DataEventArgs<object> e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (Visible == false) return;
                if (LocalData.UserInfo.LoginID != CurrentRow.RespondByID) return;

                treeMain.BeforeFocusNode -= new BeforeFocusNodeEventHandler(treeMain_BeforeFocusNode);
                ClientInquierAirRate copyData = Utility.Clone<ClientInquierAirRate>(CurrentRow);
                copyData.ID = Guid.NewGuid();
                copyData.IsNewRecord = true;
                foreach (var item in copyData.RateUnitList)
                {
                    item.ID = Guid.Empty;
                }

                if (CurrentRow.MainRecordID != null)
                {
                    copyData.MainRecordID = CurrentRow.MainRecordID;
                }
                else
                {
                    copyData.MainRecordID = CurrentRow.ID;
                }

                copyData.HasUnRead = false;
                copyData.IsNoPriceAll = true;
                copyData.Shared = true;
                copyData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
                copyData.UpdateDate = null;
                copyData.IsDirty = true;
                bsList.PositionChanged -= new EventHandler(bsList_PositionChanged);
                bsList.Insert(0, copyData);
                bsList.ResetBindings(false);
                treeMain.ExpandAll();
                bsList.PositionChanged += new EventHandler(bsList_PositionChanged);
                TreeListNode node = treeMain.FindNodeByFieldValue("ID", copyData.ID);
                treeMain.Selection.Clear();
                node.Selected = true;
                treeMain.SetFocusedNode(node);
                treeMain.BeforeFocusNode += new BeforeFocusNodeEventHandler(treeMain_BeforeFocusNode);
            }
        }
        [EventSubscription(InquireRatesCommandConstants.Command_Save)]
        public void Command_Save(object sender, DataEventArgs<object> e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (Visible == false) return;

                SaveRateList(true);
            }
        }

        [EventSubscription(InquireRatesCommandConstants.Command_NewAirRate)]
        public void Command_NewAirRate(object sender, DataEventArgs<object> e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string titleNo = LocalData.IsEnglish ? "New Inquire Air" : "New Inquire Air";
                PartLoader.ShowEditPart<NewInquireAirRatePart>(Workitem, null, titleNo, AfterNewInquireAirPartSaved);
            }
        }

        [EventSubscription(InquireRatesCommandConstants.Command_ReInquire)]
        public void Command_ReInquire(object sender, DataEventArgs<object> e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (Visible == false) return;
                if (Current == null) return;
                InquierAirRate newdata = InquireRatesHelper.TransformC2S(Utility.Clone<ClientInquierAirRate>(CurrentRow),true);

                var inquireInfo = InquireRatesService.GetInquierAirRateInfoForInquireBy(CurrentRow.ID, LocalData.UserInfo.LoginID);
                newdata.ExpTransportClauseID = inquireInfo.ExpTransportClauseID;
                newdata.ExpTransportClauseName = inquireInfo.ExpTransportClauseName;
                newdata.ExpCarrierName = inquireInfo.ExpCarrierName;
                newdata.CustomerID = inquireInfo.CustomerID;
                newdata.CustomerName = inquireInfo.CustomerName;
                newdata.ExpPrice = inquireInfo.ExpPrice;
                newdata.MAWB = inquireInfo.MAWB;
                newdata.HAWB = inquireInfo.HAWB;
                newdata.CartonsOrPallets = inquireInfo.CartonsOrPallets;
                newdata.EstimateTimeOfDelivery = inquireInfo.EstimateTimeOfDelivery;
                newdata.ID = Guid.Empty;
                newdata.InquireByID = LocalData.UserInfo.LoginID;
                newdata.InquireByName = LocalData.UserInfo.LoginName;
                foreach (var sub in newdata.UnitRates)
                {
                    sub.ID = Guid.Empty;
                    sub.Rate = 0;
                }

                string titleNo = LocalData.IsEnglish ? "New Inquire Air" : "New Inquire Air";
                PartLoader.ShowEditPart<NewInquireAirRatePart>(Workitem, newdata, titleNo, AfterNewInquireAirPartSaved);
            }
        }

        [EventSubscription(InquireRatesCommandConstants.Command_Delete)]
        public void Command_Delete(object sender, DataEventArgs<object> e)
        {
            if (Visible == false) return;
            treeMain.CloseEditor();
            List<ClientInquierAirRate> selecteds = SelectedAirItem;
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
                if (item.IsNewRecord) continue;

                needRemoveIDs.Add(item.ID);
                needRemoveUpdates.Add(item.UpdateDate);
            }
            #endregion

            try
            {
                if (needRemoveIDs.Count > 0)
                {
                    InquireRatesService.RemoveInquireRate(needRemoveIDs.ToArray(), needRemoveUpdates.ToArray(), LocalData.UserInfo.LoginID);
                }

                List<ClientInquierAirRate> source = CurrentSource;

                foreach (var item in selecteds)
                {
                    source.Remove(item);
                }

                bsList.DataSource = source;
                bsList.ResetBindings(false);
                treeMain.BestFitColumns();
                treeMain.ExpandAll();
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "RemoveSuccessfully"));
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
        }

        private void AfterNewInquireAirPartSaved(object[] prams)
        {
            if (prams == null || prams.Length == 0) return;

            InquierAirRate newData = prams[0] as InquierAirRate;

            #region  需要重新统计最大箱型，多的加，少的不变

            if (_currentServiceSource == null)
            {
                _currentServiceSource = new InquierAirRatesResult();
                _currentServiceSource.MaxUnits = newData.UnitRates;
            }
            else
            {
                foreach (var sub in newData.UnitRates)
                {
                    var found = _currentServiceSource.MaxUnits.Find(item => item.UnitID == sub.UnitID);
                    if (found == null)
                        _currentServiceSource.MaxUnits.Add(sub);
                }
            }
            BulidGridViewColumnsByAirUnits(_currentServiceSource.MaxUnits);

            //List<InquireUnit> maxUnits = null;
            //if (_currentServiceSource == null)
            //{
            //    _currentServiceSource = new InquierAirRatesResult();
            //}

            //if (_currentServiceSource.MaxUnits == null || _currentServiceSource.MaxUnits.Count == 0)
            //{
            //    maxUnits = newData.UnitRates;
            //}
            //else
            //{
            //    maxUnits = Utility.Clone<List<InquireUnit>>(_currentServiceSource.MaxUnits);

            //    foreach (InquireUnit unit in newData.UnitRates)
            //    {
            //        var findItem = (from d in _currentServiceSource.MaxUnits where d.UnitID == unit.UnitID select d).SingleOrDefault();
            //        if (findItem == null)
            //        {
            //            maxUnits.Add(unit);
            //        }
            //    }                
            //}

            //_currentServiceSource.MaxUnits = maxUnits;

            #endregion

            //这里需要控制tab面板的隐藏，还没实现
            if (_currentServiceSource.InquierAirRateList == null || _currentServiceSource.InquierAirRateList.Count == 0)
            {
               _currentServiceSource.InquierAirRateList = new List<InquierAirRate>();
            }

            _currentServiceSource.InquierAirRateList.Insert(0, newData);
            BulidGridViewColumnsByAirUnits(_currentServiceSource.MaxUnits);
            List<ClientInquierAirRate> dataSource = InquireRatesHelper.TransformS2C(_currentServiceSource.InquierAirRateList, _currentServiceSource.MaxUnits, DefaultCurrencyID);
            _sourceClone = Utility.Clone<List<ClientInquierAirRate>>(dataSource);       
            bsList.DataSource = dataSource;
            bsList.ResetBindings(false);
            treeMain.BestFitColumns();
            treeMain.ExpandAll();
        }

        #endregion

        #region EventSubscription

        InquireRatesSearchParameter _para = null;

        /// <summary>
        /// This is the subscription for the CustomerAdded event
        /// We're using the default scope, which is Global
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [EventSubscription(InquireRatesCommandConstants.Command_SearchData)]
        public void Command_SearchData(object sender, DataEventArgs<InquireRatesSearchParameter> e)
        {
            if (Visible == false) return;
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    _para = e.Data as InquireRatesSearchParameter;
                    if (_para != null)
                    {
                        InquierAirRatesResult data = InquireRatesService.GetInquireAirRateList(_para.pol,
                            _para.delivery,
                            _para.pod,
                            _para.commodity,
                            _para.inquireOrRespondBy,
                            _para.isUnReply,
                            _para.durationFrom,
                            _para.durationTo,_para.StrQuery,
                            LocalData.UserInfo.LoginID);

                        //if (data != null &&
                        //  data.UnReadCountList != null &&
                        //  UnReadCountEvent != null)
                        //{
                        //    UnReadCountEvent(this, new DataEventArgs<List<UnReadDiscussingCount>>(data.UnReadCountList));
                        //}

                        DataSource = data;
                        _hadLoadCurrentPage = true;

                        string message = string.Empty;
                        if (data == null || data.InquierAirRateList == null)
                        {
                            message = "Nothing found!";
                        }
                        else
                        {
                            message = string.Format("{0} records found", data.InquierAirRateList.Count);
                        }

                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), message);
                    }
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }

        #endregion

        #region interface     

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }

        protected ClientInquierAirRate CurrentRow
        {
            get { return Current as ClientInquierAirRate; }
            set
            {
                ClientInquierAirRate currentRow = CurrentRow;
                currentRow = value;
            }
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
                //if (CurrentChanged != null) CurrentChanged(this, Current);
            }
        }

        private void BindingData(object data)
        {
            _currentServiceSource = data as InquierAirRatesResult;

            if (_currentServiceSource == null || _currentServiceSource.InquierAirRateList == null || _currentServiceSource.InquierAirRateList.Count == 0)
            {
                //ratesResult.InquierAirRateList = new List<InquierAirRate>();
                bsList.DataSource = null;
                bsList.ResetBindings(false);
            }
            else
            {
                //备份ID.
                //用于重新加载后定位到之前行记录。
                Guid? bakID = null;
                if (bsList.DataSource != null && bsList.Current != null)
                    bakID = ((ClientInquierAirRate)bsList.Current).ID;

                bsList.DataSource = null;
                bsList.ResetBindings(false);
                BulidGridViewColumnsByAirUnits(_currentServiceSource.MaxUnits);
                //InitControls();

                List<ClientInquierAirRate> source = InquireRatesHelper.TransformS2C(_currentServiceSource.InquierAirRateList, _currentServiceSource.MaxUnits, DefaultCurrencyID);
                _sourceClone = Utility.Clone<List<ClientInquierAirRate>>(source);
                bsList.DataSource = source;
                bsList.ResetBindings(false);

                treeMain.BestFitColumns();
                treeMain.ExpandAll();
                //if (CurrentChanged != null) CurrentChanged(this, Current);

                //定位到之前行记录
                if (bakID != null)
                {
                    for (int i = 0; i < bsList.Count; i++)
                    {
                        var item = ((ClientInquierAirRate)bsList[i]);
                        if (item.ID == bakID)
                        {
                            bsList.Position = i;
                            break;
                        }
                    }
                }
            }
        }

        public override void Refresh(object items)
        {
            //List<AirList> list = this.DataSource as List<AirList>;
            //if (list == null) return;
            //List<AirList> newLists = items as List<AirList>;

            //foreach (var item in newLists)
            //{
            //    AirList tager = list.Find(delegate(AirList jItem) { return item.ID == jItem.ID; });
            //    if (tager == null) continue;

            //    Utility.CopyToValue(item, tager, typeof(AirList));
            //}
            //bsList.ResetBindings(false);
        }

        public override void RemoveItem(int index)
        {
            bsList.RemoveAt(index);
        }
            
        public override void RemoveItem(object item)
        {
            bsList.Remove(item);
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        public override event CurrentChangedHandler CurrentChanged;
        public override event CancelEventHandler CurrentChanging;

        #region BulidColumns

        private List<string> _visibleColumnsNameList = null;
        private void BulidGridViewColumnsByAirUnits(List<InquireUnit> units)
        {
            _visibleColumnsNameList = new List<string>();

            #region  SetVisible= false;
            colRate_MIN.Visible = false;
            colRate_45.Visible = false;
            colRate_100.Visible = false;
            colRate_300.Visible = false;
            colRate_500.Visible = false;
            colRate_800.Visible = false;
            colRate_1000.Visible = false;
            colRate_1300.Visible = false;
           
            #endregion

            int visibleIndex = 5;

            foreach (var item in units)
            {
                #region  SetVisible= true;
                switch (item.UnitName)
                {
                    case "MIN": colRate_MIN.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("MIN"); break;
                    case "+45": colRate_45.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("+45"); break;
                    case "+100": colRate_100.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("+100"); break;
                    case "+300": colRate_300.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("+300"); break;
                    case "+500": colRate_500.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("+500"); break;
                    case "+800": colRate_800.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("+800"); break;

                    case "+1000": colRate_1000.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("+1000"); break;
                    case "+1300": colRate_1300.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("+1300"); break;
                }

                visibleIndex++;
                #endregion
            }
        }

        #endregion

        #endregion
        
        #endregion

        public void ResetCurrent()
        {
            ClientInquierAirRate tager = _sourceClone.Find(delegate(ClientInquierAirRate item) { return item.ID == CurrentRow.ID; });
            if (tager != null)
            {
                Utility.CopyToValue(tager, CurrentRow, typeof(ClientInquierAirRate));
                bsList.ResetCurrentItem();
            }
        }

        bool ValidateData(List<ClientInquierAirRate> needSaveList)
        {
            bool value = true;
            string eError = @"[{0}] is required! You must fill-in it.";
            string cError = @"请您输入必填项：[{0}]。";

            foreach (var item in needSaveList)
            {
                if (Utility.GuidIsNullOrEmpty(item.CarrierID))
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ?
                        string.Format(eError, colCarrier.Caption)
                        : string.Format(cError, colCarrier.Caption));

                    value = false;
                }

                if (Utility.GuidIsNullOrEmpty(item.POLID))
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ?
                        string.Format(eError, colPOL.Caption)
                        : string.Format(cError, colPOL.Caption));
                    value = false;
                }

                if (Utility.GuidIsNullOrEmpty(item.PlaceOfDeliveryID))
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ?
                        string.Format(eError, colPlaceOfDelivery.Caption)
                        : string.Format(cError, colPlaceOfDelivery.Caption));
                    value = false;
                }

                if (Utility.GuidIsNullOrEmpty(item.CurrencyID))
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ?
                        string.Format(eError, colCurrency.Caption)
                        : string.Format(cError, colCurrency.Caption));
                    value = false;
                }

                if (Utility.GuidIsNullOrEmpty(item.ShippingLineID))
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ?
                        string.Format(eError, colShipline.Caption)
                        : string.Format(cError, colShipline.Caption));
                    value = false;
                }

                if (item.Rate_MIN == null
                    && item.Rate_45 == null
                    && item.Rate_100 == null
                    && item.Rate_300 == null
                    && item.Rate_500 == null
                    && item.Rate_800 == null
                    && item.Rate_1000 == null
                    && item.Rate_1300 == null)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ?
                        string.Format(eError, colCurrency.Caption)
                        : string.Format(cError, colCurrency.Caption));
                    value = false;
                }
            }

            return value;
        }

        public void GeneralInfoPart_ChangedUnitEvent(object sender, Guid inquirePriceID, List<InquireUnit> list)
        {
            List<ClientInquierAirRate> inquireList = (List<ClientInquierAirRate>)bsList.DataSource;
            foreach (var inquire in inquireList)
            {
                if (inquire.ID == inquirePriceID || inquire.MainRecordID == inquirePriceID)
                {
                    ChangeUnit(inquire, list);
                }
            }

            SetEditState();

            foreach (var sub in list)
            {
                var found = _currentServiceSource.MaxUnits.Find(item => item.UnitID == sub.UnitID);
                if (found == null)
                    _currentServiceSource.MaxUnits.Add(sub);
            }
            BulidGridViewColumnsByAirUnits(_currentServiceSource.MaxUnits);
        }

        void ChangeUnit(ClientInquierAirRate current, List<InquireUnit> list)
        {
            foreach (var sub in list)
            {
                InquireUnit found;

                //删除单位
                for (int i = current.RateUnitList.Count - 1; i >= 0; i--)
                {
                    var subCurrent = current.RateUnitList[i];
                    found = list.Find(item => item.UnitID == subCurrent.UnitID);

                    if (found == null)
                    {
                        switch (subCurrent.UnitName)
                        {
                            case "MIN": current.Rate_MIN = 0; break;
                            case "+45": current.Rate_45 = 0; break;
                            case "+100": current.Rate_100 = 0; break;
                            case "+300": current.Rate_300 = 0; break;
                            case "+500": current.Rate_500 = 0; break;
                            case "+800": current.Rate_800 = 0; break;
                            case "+1000": current.Rate_1000 = 0; break;
                            case "+1300": current.Rate_1300 = 0; break;
                        }

                        current.RateUnitList.Remove(subCurrent);
                    }
                }

                //新增单位
                found = current.RateUnitList.Find(item => item.UnitID == sub.UnitID);
                if (found == null)
                {
                    current.RateUnitList.Add(new InquireUnit()
                    {
                        Rate = 0,
                        UnitID = sub.UnitID,
                        UnitName = sub.UnitName
                    });
                }
                current.IsDirty = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public void GeneralInfoPart_ChangedUnitEvent(object sender, List<InquireUnit> list)
        {
            foreach (var sub in list)
            {
                var found = _currentServiceSource.MaxUnits.Find(item => item.UnitID == sub.UnitID);
                if (found == null)
                    _currentServiceSource.MaxUnits.Add(sub);

                var current = ((ClientInquierAirRate)bsList.Current);

                //var currentList = (List<ClientInquierOceanRate>)bsList.DataSource;

                //删除单位
                for (int i = current.RateUnitList.Count - 1; i >= 0; i--)
                {
                    var subCurrent = current.RateUnitList[i];
                    found = list.Find(item => item.UnitID == subCurrent.UnitID);

                    if (found == null)
                    {
                        switch (subCurrent.UnitName)
                        {
                            case "MIN": current.Rate_MIN = 0; break;
                            case "+45": current.Rate_45 = 0; break;
                            case "+100": current.Rate_100 = 0; break;
                            case "+300": current.Rate_300 = 0; break;
                            case "+500": current.Rate_500 = 0; break;
                            case "+800": current.Rate_800 = 0; break;
                            case "+1000": current.Rate_1000 = 0; break;
                            case "+1300": current.Rate_1300 = 0; break;
                        }

                        current.RateUnitList.Remove(subCurrent);
                    }
                }

                //新增单位
                found = current.RateUnitList.Find(item => item.UnitID == sub.UnitID);
                if (found == null)
                {
                    current.RateUnitList.Add(new InquireUnit()
                    {
                        Rate = 0,
                        UnitID = sub.UnitID,
                        UnitName = sub.UnitName
                    });
                }
                current.IsDirty = true;
            }

            SetEditState();

            BulidGridViewColumnsByAirUnits(_currentServiceSource.MaxUnits);
        }


        public bool SaveRateList(bool isNeedRefreshDiscussingPart)
        {
            //保存General Info面板
            if (!GeneralInfoPart.IsReadOnly && GeneralInfoPart.IsChanged)
            {
                if (!GeneralInfoPart.ValidateData())
                {
                    return false;
                }

                GeneralInfoPart.SaveData();
            }

            //保存Inquire List
            if (LocalData.UserInfo.LoginID != CurrentRow.RespondByID) return false;
            treeMain.CloseEditor();
            List<ClientInquierAirRate> needSaveList = new List<ClientInquierAirRate>();
            List<ClientInquierAirRate> datas = bsList.DataSource as List<ClientInquierAirRate>;
            if (CurrentRow.IsDirty)
            {
                needSaveList.Add(CurrentRow);
            }

            foreach (var item in datas)
            {
                if (item.IsDirty && (item.MainRecordID == CurrentRow.ID || item.ID == CurrentRow.MainRecordID ||
                   (item.ID != CurrentRow.ID && CurrentRow.MainRecordID != null && CurrentRow.MainRecordID == item.MainRecordID)))
                {
                    needSaveList.Add(item);
                }
            }

            if (needSaveList.Count == 0)
            {
                return true;
            }

            bool isContainMain = false;
            foreach (var item in needSaveList)
            {
                if (item.MainRecordID == null)
                {
                    isContainMain = true;
                    break;
                }
            }

            if (!isContainMain)
            {
                foreach (var item in datas)
                {
                    if (item.ID == CurrentRow.MainRecordID)
                    {
                        needSaveList.Add(item);
                        break;
                    }
                }
            }

            needSaveList = needSaveList.OrderBy(i => i.MainRecordID).ToList();

            if (!ValidateData(needSaveList))
            {
                return false;
            }

            try
            {
                ManyResultData result = InquireRatesService.SaveAirInquireRateWithTrans(InquireRatesHelper.TransformC2S(needSaveList), DateTime.Now, LocalData.UserInfo.DefaultCompanyID);
                for (int i = 0; i < result.ChildResults.Count; i++)
                {
                    needSaveList[i].ID = result.ChildResults[i].ID;
                    needSaveList[i].UpdateDate = result.ChildResults[i].UpdateDate;
                    needSaveList[i].IsDirty = false;
                    needSaveList[i].IsNewRecord = false;
                }

                bsList.ResetBindings(false);
                _sourceClone = bsList.DataSource as List<ClientInquierAirRate>;
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), "Save Successfully");
                //if (isNeedRefreshDiscussingPart && RefreshAirDiscussingPartEvent != null)
                //{
                //    RefreshAirDiscussingPartEvent(this, new DataEventArgs<object>(new object()));
                //}

                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
                return false;
            }
        }
    }
}
