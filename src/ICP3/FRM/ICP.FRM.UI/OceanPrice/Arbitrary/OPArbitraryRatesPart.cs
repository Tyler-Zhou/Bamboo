using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using Microsoft.Practices.CompositeUI;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.FRM.ServiceInterface;

using ICP.Common.ServiceInterface;
using System.IO;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI.Commands;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Service;
using DevExpress.XtraEditors;

namespace ICP.FRM.UI.OceanPrice
{
    /// <summary>
    /// 支线价格编辑界面
    /// </summary>
    [ToolboxItem(false)]
    public partial class OPArbitraryRatesPart : BaseListEditPart
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

        public IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
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
        /// <summary>
        /// 线程ID:进度面板
        /// </summary>
        static int theradID = 0;
        /// <summary>
        /// 当前数据源
        /// </summary>
        List<ClientArbitraryList> CurrentSource
        {
            get { return bsList.DataSource as List<ClientArbitraryList>; }
        }

        /// <summary>
        /// 当前行
        /// </summary>
        ClientArbitraryList CurrentRow
        {
            get { return bsList.Current as ClientArbitraryList; }
        }
        /// <summary>
        /// 选择行
        /// </summary>
        List<ClientArbitraryList> SelectedOceanItem
        {
            get
            {

                int[] rowIndexs = gvMain.GetSelectedRows();

                if (rowIndexs.Length == 0) return null;

                List<ClientArbitraryList> tagers = rowIndexs.Select(item => gvMain.GetRow(item)).OfType<ClientArbitraryList>().ToList();
                return tagers;
            }
        }
        /// <summary>
        /// 是否更改
        /// </summary>
        public bool IsChanged
        {
            get
            {
                if (_LoadedOceanID.IsNullOrEmpty() || _parentList == null || _LoadedOceanID != _parentList.ID) return false;

                List<ClientArbitraryList> source = CurrentSource;
                if (source == null || source.Count == 0) return false;

                return source.Any(item => item.IsNew || item.IsDirty || string.IsNullOrEmpty(item.ErrorInfo) == false);
            }
        }
        
        #endregion

        #region Init
        /// <summary>
        /// 支线价格编辑界面
        /// </summary>
        public OPArbitraryRatesPart()
        {
            InitializeComponent();
            Enabled = false;
            Disposed += delegate
            {
                _ArbitraryFilterObject = null;
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
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                InitMessage();
                InitControls();
            }
        }
        private void InitMessage()
        {
            RegisterMessage("SaveSuccessfully", "Save Successfully");
            RegisterMessage("RemoveSuccessfully", "Remove Successfully");
            RegisterMessage("RemoveSelectedItem", "Are you sure you want to remove the selected item?");
            RegisterMessage("ValidateRate_20GP", "Price must  great than zero.");
            RegisterMessage("ValidatePOLSamePOD", "From can not same as to.");
            RegisterMessage("ValidateDataExist", "Arbitrary - Data has exist.");
            RegisterMessage("MaxScreen", "&MaxScreen");
            RegisterMessage("BrackScreen", "Brack(&M)");

            RegisterMessage("FilterPartTitel", "Search Arbitrary Rates");
            RegisterMessage("AssociatedRatesPartTitel", "Associated Rates");
            RegisterMessage("BatchItemFaily", "Batch Item Faily.");
            RegisterMessage("RemoveSuccessfully", "Remove Successfully");
            RegisterMessage("SelectOneRate", "You should select at least one Arbitrary Rate.");
            RegisterMessage("GeneralInfoChanged", "General Info is changed, you should save it first.");
            RegisterMessage("ImportFailed", "Importing Arbitrary Rates is failed.\r\n");
            RegisterMessage("ImportingSuccessfully", "Importing Arbitrary Rates is successful with {0} records.");
            RegisterMessage("ValidateItemExist", "Some items are existing in the Arbitrary Rates {0};");
            //NativeLanguageService.GetText(this, "ValidateItemCodeDifferent")
            RegisterMessage("Publish", "&Publish");
            RegisterMessage("Pause", "&Pause");

            RegisterMessage("ChangeSameName", "Would you change all data of the same name?");

        }

        private void InitControls()
        {
            Utility.ShowGridRowNo(gvMain);
            Utility.SetGridViewClickIndicatorHeader2SelectAll(gvMain);

            InitComboboxSource();

            SearchRegister();
        }

        void InitComboboxSource()
        {
            #region 运输条款
            foreach (var item in OceanPriceUIDataHelper.TransportClauses)
            {
                rcmbTransportClause.Properties.Items.Add(new ImageComboBoxItem(item.Code, item.ID));
            }
           
            rcmbTransportClause.SelectedIndexChanged += delegate
            {
                gvMain.CloseEditor();
                ClientArbitraryList currentrow = CurrentRow;
                if (currentrow != null)
                {
                    currentrow.TransportClauseName = OceanPriceUIDataHelper.TransportClauses.Find(t => t.ID == currentrow.TransportClauseID).Code;
                }
            };
            #endregion

            #region GeographyType
            List<EnumHelper.ListItem<GeographyType>> accountTypes = EnumHelper.GetEnumValues<GeographyType>(LocalData.IsEnglish);
            foreach (var item in accountTypes)
            {
                if (item.Value == GeographyType.None) continue;

                cmbGeographyType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));

            }
            #endregion

            #region GeographyType
            List<EnumHelper.ListItem<ModeOfTransport>> transportType = EnumHelper.GetEnumValues<ModeOfTransport>(LocalData.IsEnglish);
            foreach (var item in transportType)
            {
                if (item.Value == ModeOfTransport.None) continue;

                cmbTransportType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));

            }
            #endregion

            #region ChangeState

            rcmbChangeState.Properties.Items.Add(new ImageComboBoxItem(string.Empty, ChangeState.None, -1));
            rcmbChangeState.Properties.Items.Add(new ImageComboBoxItem(string.Empty, ChangeState.New, 0));
            rcmbChangeState.Properties.Items.Add(new ImageComboBoxItem(string.Empty, ChangeState.Changed, 1));
            #endregion

            #region ErrorState

            rcmbError.Properties.Items.Add(new ImageComboBoxItem(string.Empty, false, -1));
            rcmbError.Properties.Items.Add(new ImageComboBoxItem(string.Empty, true, 0));

            #endregion
        }


        private void SearchRegister()
        {
            #region pol

            DataFindClientService.RegisterGridColumnFinder(colPOL
                                             , CommonFinderConstants.OceanLocationFinder
                                             , "POLID"
                                             , "POLName"
                                             , "ID"
                                             , LocalData.IsEnglish ? "EName" : "EName", delegate(object befocePickedData, object afterPickedData)
                                             {
                                                 ClientArbitraryList befoceChangedRow = befocePickedData as ClientArbitraryList;
                                                 ClientArbitraryList afterChangedRow = afterPickedData as ClientArbitraryList;

                                                 if (befoceChangedRow != null && afterChangedRow != null)
                                                 {
                                                     List<ClientArbitraryList> source = CurrentSource;
                                                     List<ClientArbitraryList> sameData = source.FindAll(s => s.POLID == befoceChangedRow.POLID);
                                                     if (sameData != null && sameData.Count > 0)
                                                     {
                                                         DialogResult result = XtraMessageBox.Show(NativeLanguageService.GetText(this, "ChangeSameName"),
                                                                                 LocalData.IsEnglish ? "Tip" : "提示",
                                                                                 MessageBoxButtons.YesNo,
                                                                                 MessageBoxIcon.Question);
                                                         if (result == DialogResult.Yes)
                                                         {
                                                             foreach (var item in sameData)
                                                             {
                                                                 item.POLID = afterChangedRow.POLID;
                                                                 item.POLName = afterChangedRow.POLName;
                                                                 item.IsDirty = true;
                                                             }

                                                             gvMain.RefreshData();

                                                             SendKeys.Send("{TAB}");
                                                         }
                                                     }
                                                 }
                                             });
            #endregion

            #region pod

            DataFindClientService.RegisterGridColumnFinder(colPOD
                                           , CommonFinderConstants.OceanLocationFinder
                                           , "PODID"
                                           , "PODName"
                                           , "ID"
                                           , LocalData.IsEnglish ? "EName" : "EName", delegate(object befocePickedData, object afterPickedData)
                                           {
                                               ClientArbitraryList befoceChangedRow = befocePickedData as ClientArbitraryList;
                                               ClientArbitraryList afterChangedRow = afterPickedData as ClientArbitraryList;

                                               if (befoceChangedRow != null && afterChangedRow != null)
                                               {
                                                   List<ClientArbitraryList> source = CurrentSource;
                                                   List<ClientArbitraryList> sameData = source.FindAll(s => s.PODID == befoceChangedRow.PODID);
                                                   if (sameData != null && sameData.Count > 0)
                                                   {
                                                       DialogResult result = XtraMessageBox.Show(NativeLanguageService.GetText(this, "ChangeSameName"),
                                                                               LocalData.IsEnglish ? "Tip" : "提示",
                                                                               MessageBoxButtons.YesNo,
                                                                               MessageBoxIcon.Question);
                                                       if (result == DialogResult.Yes)
                                                       {
                                                           foreach (var item in sameData)
                                                           {
                                                               item.PODID = afterChangedRow.PODID;
                                                               item.PODName = afterChangedRow.PODName;
                                                               item.IsDirty = true;
                                                           }

                                                           gvMain.RefreshData();
                                                           SendKeys.Send("{TAB}");
                                                       }
                                                   }
                                               }
                                           });
            #endregion
        }

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

            ClientArbitraryList newData = new ClientArbitraryList();
            OceanPriceTransformHelper.BulidNewArbitraryData(newData, _parentList);
            newData.BulidRateToZeroByOceanUints(_parentList.OceanUnits);
            newData.BeginEdit();

            (bsList.List as List<ClientArbitraryList>).Insert(0, newData);
            bsList.ResetBindings(false);

            gvMain.CancelSelection();
            gvMain.FocusedRowHandle = 0;
            gvMain.SelectCell(0, colGeographyType);
        }

        private void barCopy_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsPublish())
            {
                return;
            }
            using (new CursorHelper(Cursors.WaitCursor))
            {
                CopyData();
            }
        }
        private void CopyData()
        {
            List<ClientArbitraryList> selecteds = SelectedOceanItem;
            if (CurrentRow == null || selecteds == null || selecteds.Count == 0) return;

            gvMain.ActiveFilterString = string.Empty;

            List<ClientArbitraryList> copyTager = new List<ClientArbitraryList>();
            foreach (var item in selecteds)
            {
                ClientArbitraryList newItem = Utility.Clone<ClientArbitraryList>(item);
                OceanPriceTransformHelper.BulidNewArbitraryData(newItem, _parentList);
                item.BeginEdit();
                copyTager.Add(newItem);
            }


            List<ClientArbitraryList> source = CurrentSource;
            foreach (var item in copyTager)
            {
                source.Insert(0, item);
            }

            gvMain.SortInfo.Clear();

            bsList.DataSource = source;
            bsList.ResetBindings(false);

            gvMain.MoveFirst();
            gvMain.ClearSelection();
            gvMain.SelectRows(0, copyTager.Count - 1);
        }

        private void barDelete_ItemClick(object sender, ItemClickEventArgs e)
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
            List<ClientArbitraryList> selecteds = SelectedOceanItem;
            if (CurrentRow == null || selecteds == null || selecteds.Count == 0) return;

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
                ManyResultData resultData = new ManyResultData();
                if (needRemoveIDs.Count > 0)
                {
                    resultData = OceanPriceService.RemoveArbitrarys(needRemoveIDs.ToArray(), LocalData.UserInfo.LoginID, needRemoveUpdates.ToArray());
                }

                List<Guid> noRemoveIDList = (from d in resultData.ChildResults select d.ID).ToList();

                List<ClientArbitraryList> source = CurrentSource;

                foreach (var item in selecteds)
                {
                    if (!noRemoveIDList.Contains(item.ID))
                    {
                        source.Remove(item);
                    }
                }

                bsList.DataSource = source;
                bsList.ResetBindings(false);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "RemoveSuccessfully"));
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
        }

        #region Save

        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Workitem.Commands[OPCommonConstants.Command_SaveData].Execute();
            }
        }

        internal List<ClientArbitraryList> GetChangedItem()
        {
            List<ClientArbitraryList> source = CurrentSource;
            List<ClientArbitraryList> chengedItem = new List<ClientArbitraryList>();
            foreach (var item in source)
            {
                if (item.IsNew || item.IsDirty) chengedItem.Add(item);
            }
            return chengedItem;
        }

        internal void RefreshUIData()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                List<ClientArbitraryList> source = CurrentSource;
                source = source.OrderByDescending(b => b.No).ToList();
                bsList.DataSource = source;
                gvMain.RefreshData();
            }
        }

        public bool ValidateData()
        {
            if (IsChanged == false) return true;

            Validate();
            bsList.EndEdit();

            List<ClientArbitraryList> source = CurrentSource;
            List<ClientArbitraryList> chengedItem = new List<ClientArbitraryList>();
            foreach (var item in source)
            {
                if (item.IsNew || item.IsDirty) chengedItem.Add(item);
            }

            if (ValidateData(chengedItem) == false) return false;

            return true;
        }

        private bool ValidateData(List<ClientArbitraryList> chengedItems)
        {
            gvMain.ActiveFilterString = string.Empty;

            if (chengedItems == null || chengedItems.Count == 0) return false;

            bool isSrcc = true;

            foreach (var item in chengedItems)
            {
                string errorMessage = string.Empty;
                item.ErrorInfo = string.Empty;

                if (item.ModeOfTransport == ModeOfTransport.None)
                {
                    errorMessage = GetErrorInfo(errorMessage, "Transport must input");
                }
                if (item.GeographyType == GeographyType.None)
                {
                    errorMessage = GetErrorInfo(errorMessage, "GeographyType must input");
                }
                if (item.TransportClauseID.IsNullOrEmpty())
                {
                    errorMessage = GetErrorInfo(errorMessage, "Term must input");
                }
                if (item.POLID.IsNullOrEmpty())
                {
                    errorMessage=GetErrorInfo(errorMessage, "Form must input");
                    isSrcc = false;
                }
                if (item.PODID.IsNullOrEmpty())
                {
                    errorMessage = GetErrorInfo(errorMessage, "To must input");
                    isSrcc = false;
                }
                if (item.ValidateHasRate() == false)
                {
                    errorMessage = GetErrorInfo(errorMessage, NativeLanguageService.GetText(this, "ValidateRate_20GP"));
                    isSrcc = false;
                }
              

                item.ErrorInfo = errorMessage;
            }

            #region  //验证唯一键定义 Account+ POL+ VIA+ POD+ Delivery +Origin Arb+ Dest Arb+item code + Term

            List<ClientArbitraryList> oldSouce = CurrentSource.FindAll(s => s.IsNew == false && s.IsDirty == false);

            bool itemCodeCommDifferent = false;
            bool isExist = ValidateHasExistItem(oldSouce, chengedItems);

            if (isExist || itemCodeCommDifferent) isSrcc = false;

            //if (isExist) LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), NativeLanguageService.GetText(this, "ValidateDataExist"));

            #endregion

            if (isSrcc == false) gvMain.RefreshData();
            return isSrcc;
        }
        private string GetErrorInfo(string errorList,string errorInfo)
        {
            if (errorList.IsNullOrEmpty())
            {
                errorList = errorInfo;
            }
            else
            {
                errorList = errorList + Environment.NewLine + errorInfo;
            }

            return errorList;
        }
        #endregion

        #endregion

        #region 相对简单操作

        #region MaxScreen

        bool isMax = false;
        private void barMaxScreen_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[OPCommonConstants.Command_MaxOceanItem].Execute();
        }
        [CommandHandler(OPCommonConstants.Command_MaxOceanItem)]
        public void Command_MaxOceanItem(object sender, EventArgs e)
        {
            isMax = !isMax;
            barMaxScreen.Caption = NativeLanguageService.GetText(this, isMax ? "BrackScreen" : "MaxScreen");
        }
        #endregion

        #region Finder

        ArbitraryFilterObject _ArbitraryFilterObject = null;
        private void barShowFinder_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_parentList == null) return;
            OPArbitraryFilterForm of = new OPArbitraryFilterForm();
            of.SetSouce(_ArbitraryFilterObject);
            DialogResult dr = PartLoader.ShowDialog(of, NativeLanguageService.GetText(this, "FilterPartTitel"), FormBorderStyle.Sizable);

            //直接点X关闭是不会更变查找条件的
            if (dr == DialogResult.OK)
            {
                _ArbitraryFilterObject = of.FilterObject;
                gvMain.ActiveFilterString = _ArbitraryFilterObject.BulidFilterString();
            }
        }

        #endregion

        #region 全选

        private void barSelectAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            GridHelper.ToggleSelectAllRows(gvMain);
            gvMain.RefreshData();
        }
        #endregion

        #region  打开模板
        private void barTemplate_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (_parentList == null) return;
                string path = OceanPriceUIDataHelper.GetOceanArbitraryTemplateFileName();
                Process.Start(path);
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); }
        }

        #endregion

        #region 批量修改
        private void barBatchItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsPublish())
            {
                return;
            }
            BatchChangeOceanItem();
        }

        void BatchChangeOceanItem()
        {
            List<ClientArbitraryList> source = SelectedOceanItem;

            List<ClientArbitraryList> selected = SelectedOceanItem;
            if (selected == null || selected.Count == 0)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this, NativeLanguageService.GetText(this, "SelectOneRate"));
                return;
            }

            OPArbitraryBatchItemForm opbForm = Workitem.Items.AddNew<OPArbitraryBatchItemForm>();
            opbForm.SetSource(_parentList);

            DialogResult dr = PartLoader.ShowDialog(opbForm, NativeLanguageService.GetText(this, "BatchEditPartTitel"), FormBorderStyle.Sizable);

            if (dr != DialogResult.OK) return;

            ArbitraryBatchItem batchItem = opbForm.BatchItem;
            if (batchItem == null) return;

            try
            {
                Enabled = false;
                foreach (var item in selected)
                {
                    item.BeginEdit();
                    #region Rate
                    foreach (var clientUnitItem in opbForm.RateList)
                    {
                        if (string.IsNullOrEmpty(clientUnitItem.Rate)) continue;
                       
                        decimal rate = Convert.ToDecimal(clientUnitItem.Rate);

                        if (!batchItem.RateAppend)
                        {
                            switch (clientUnitItem.UnitName)
                            {
                                case "45FR": item.Rate_45FR = rate; break;
                                case "40RF": item.Rate_40RF = rate; break;
                                case "45HT": item.Rate_45HT = rate; break;
                                case "20RF": item.Rate_20RF = rate; break;
                                case "20HQ": item.Rate_20HQ = rate; break;
                                case "20TK": item.Rate_20TK = rate; break;
                                case "20GP": item.Rate_20GP = rate; break;
                                case "40TK": item.Rate_40TK = rate; break;
                                case "40OT": item.Rate_40OT = rate; break;
                                case "20FR": item.Rate_20FR = rate; break;
                                case "45GP": item.Rate_45GP = rate; break;
                                case "40GP": item.Rate_40GP = rate; break;
                                case "45RF": item.Rate_45RF = rate; break;
                                case "20RH": item.Rate_20RH = rate; break;
                                case "45OT": item.Rate_45OT = rate; break;
                                case "40NOR": item.Rate_40NOR = rate; break;
                                case "40FR": item.Rate_40FR = rate; break;
                                case "20OT": item.Rate_20OT = rate; break;
                                case "45TK": item.Rate_45TK = rate; break;
                                case "20NOR": item.Rate_20NOR = rate; break;
                                case "40HT": item.Rate_40HT = rate; break;
                                case "40RH": item.Rate_40RH = rate; break;
                                case "45RH": item.Rate_45RH = rate; break;
                                case "45HQ": item.Rate_45HQ = rate; break;
                                case "20HT": item.Rate_20HT = rate; break;
                                case "40HQ": item.Rate_40HQ = rate; break;
                                case "53HQ": item.Rate_53HQ = rate; break;
                            }
                        }
                        else
                        {
                            switch (clientUnitItem.UnitName)
                            {
                                case "45FR": item.Rate_45FR += rate; break;
                                case "40RF": item.Rate_40RF += rate; break;
                                case "45HT": item.Rate_45HT += rate; break;
                                case "20RF": item.Rate_20RF += rate; break;
                                case "20HQ": item.Rate_20HQ += rate; break;
                                case "20TK": item.Rate_20TK += rate; break;
                                case "20GP": item.Rate_20GP += rate; break;
                                case "40TK": item.Rate_40TK += rate; break;
                                case "40OT": item.Rate_40OT += rate; break;
                                case "20FR": item.Rate_20FR += rate; break;
                                case "45GP": item.Rate_45GP += rate; break;
                                case "40GP": item.Rate_40GP += rate; break;
                                case "45RF": item.Rate_45RF += rate; break;
                                case "20RH": item.Rate_20RH += rate; break;
                                case "45OT": item.Rate_45OT += rate; break;
                                case "40NOR": item.Rate_40NOR += rate; break;
                                case "40FR": item.Rate_40FR += rate; break;
                                case "20OT": item.Rate_20OT += rate; break;
                                case "45TK": item.Rate_45TK += rate; break;
                                case "20NOR": item.Rate_20NOR += rate; break;
                                case "40HT": item.Rate_40HT += rate; break;
                                case "40RH": item.Rate_40RH += rate; break;
                                case "45RH": item.Rate_45RH += rate; break;
                                case "45HQ": item.Rate_45HQ += rate; break;
                                case "20HT": item.Rate_20HT += rate; break;
                                case "40HQ": item.Rate_40HQ += rate; break;
                                case "53HQ": item.Rate_53HQ += rate; break;
                            }
                        }
                    }

                    #endregion

                    #region Ports
                    #region POL

                    if (batchItem.POLID.IsNullOrEmpty() == false)
                    {
                        item.POLID = batchItem.POLID.Value;
                        item.POLName = batchItem.POLName;
                    }

                    #endregion

                    #region PODID

                    if (batchItem.PODID.IsNullOrEmpty() == false)
                    {
                        item.PODID = batchItem.PODID.Value;
                        item.PODName = batchItem.PODName;
                    }

                    #endregion

                    #endregion

                    #region ItemCode GeographyType TransportClauseList FromDate ToDate

                    if (batchItem.ItemCode.IsNullOrEmpty() == false) item.ItemCode = batchItem.ItemCode;

                    if (batchItem.TransportClauseListID.HasValue) item.TransportClauseID = batchItem.TransportClauseListID.Value;

                    if (batchItem.GeographyType != GeographyType.None) item.GeographyType = batchItem.GeographyType;

                    #endregion
                }

                Validate();
                bsList.EndEdit();
                gvMain.ActiveFilterString = string.Empty;
                gvMain.RefreshData();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), NativeLanguageService.GetText(this, "BatchItemFaily") + ex.Message);
            }
            finally
            {
                Enabled = true;
            }
        }

        #endregion

        #endregion

        #region Import
        private void barImport_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsPublish())
            {
                return;
            }
            if (IsChanged)
            {
                DialogResult result = XtraMessageBox.Show("General Info is changed, you should save it first.",
                                                 "Tip",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);

                if (result == DialogResult.Yes) { barSave.PerformClick(); }

                return;
            }

            Import();
        }

        delegate List<ClientArbitraryList> ImportOceanItemHandler(string fileName);
        private void Import()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Multiselect = false,
                CheckFileExists = true,
                Title = "Choose File",
                Filter = "Excel files (*.xls)|*.xls"
            };
            DialogResult dlt = openFileDialog.ShowDialog();

            if (dlt != DialogResult.OK) return;
            string fileName = openFileDialog.FileName;

            if (string.IsNullOrEmpty(fileName) || File.Exists(fileName) == false) return;

            //导入
            theradID=LoadingServce.ShowLoadingForm("Import......");            
            List<ClientArbitraryList> arbitrarys = BingInputOceanItem(fileName);
            AfterImport(arbitrarys);
            LoadingServce.CloseLoadingForm(theradID);

            //生成
            theradID = LoadingServce.ShowLoadingForm("Builer......");
            try
            {
                OceanPriceService.BuilerBaseItemForOceanID(_parentList.ID);
            }
            finally
            {
                LoadingServce.CloseLoadingForm(theradID);
            }
        }

        List<ClientArbitraryList> BingInputOceanItem(string fileName)
        {
            DataSet dsExcel;
            try
            {
                if (IntPtr.Size == 8)
                {
                    dsExcel = ArbitraryImportHelper.X64ReadExcelToDataSet(fileName);
                }
                else
                {
                    dsExcel = ArbitraryImportHelper.ReadExcelToDataSet(fileName);
                }
               
                ArbitraryImportHelper.ValidateArbitraryExcelColumn(dsExcel, _parentList.OceanUnits);
                DataTable dt = dsExcel.Tables[0];

                //最终需传到数据库的对象
                List<ClientArbitraryList> arbitrarys = new List<ClientArbitraryList>();

                //先把基本信息导入到客户端对象
                ArbitraryImportHelper.InputBaseInfoIntoArbitrary(dt, arbitrarys, _parentList);

                if (arbitrarys.Count == 0) throw new ApplicationException("Data Not Find.");

                //	可以忽略逗号间空格的port名称，如：Los Angels, CA -> Los Angels,CA
                //	港口支持“/”号分隔。如：Los Angels,CA/Long Beach,CA，即解释为两条Arbitrary Rates

                //替换逗号
                ArbitraryImportHelper.ReplacePortComma(arbitrarys);

                //港口一转多
                ArbitraryImportHelper.TransformPortToMutil(arbitrarys);

                //ValidatePortNameCarrierTransportClause
                ArbitraryImportHelper.ValidatePortNameTransportClause(arbitrarys, GeographyService, OceanPriceUIDataHelper.TransportClauses);


                #region 类真实数据验证
                foreach (var item in arbitrarys)
                {
                    string errorMessage = string.Empty;
                    if (item.ModeOfTransport==ModeOfTransport.None)
                    {
                        errorMessage = GetErrorInfo(errorMessage, "Transport must input");
                    }
                    if (item.GeographyType == GeographyType.None)
                    {
                        errorMessage = GetErrorInfo(errorMessage, "GeographyType must input");
                    }
                    if (item.TransportClauseID.IsNullOrEmpty())
                    {
                        errorMessage = GetErrorInfo(errorMessage, "Term must input");
                    }
                    if (item.ValidateHasRate() == false)
                    {
                        errorMessage = GetErrorInfo(errorMessage, NativeLanguageService.GetText(this, "ValidateRate_20GP"));
                    }
                 

                    item.ErrorInfo += errorMessage;
                }
                #endregion

                arbitrarys = arbitrarys.OrderByDescending(o => o.HasError).ToList();

                #region  //验证唯一键定义 Account+ POL+ VIA+ POD+ Delivery +Origin Arb+ Dest Arb+item code + Term
                ValidateHasExistItem(CurrentSource, arbitrarys);

                #endregion

                List<ClientArbitraryList> canSaveData = arbitrarys.FindAll(b => b.ErrorInfo.IsNullOrEmpty());
                #region Save


                if (canSaveData != null && canSaveData.Count > 0)
                {

                    ArbitraryCollect oceanItemCollect = new ArbitraryCollect();
                    oceanItemCollect.ArbitraryItem = OceanPriceTransformHelper.TransformC2S(canSaveData, _parentList.OceanUnits);
                    ManyResult result = OceanPriceService.SaveOceanArbitrarys(_parentList.ID, oceanItemCollect, LocalData.UserInfo.LoginID);

                    ////调用异步执行生成运价
                    //try
                    //{
                    //    AsyncBuilerBaseItem async = new AsyncBuilerBaseItem();
                    //    async.AsyncBuilerBaseItemForOceanID(_parentList.ID);
                    //}
                    //catch { }

                    for (int i = 0; i < result.Items.Count; i++)
                    {
                        canSaveData[i].ID = result.Items[i].GetValue<Guid>("ID");
                        canSaveData[i].UpdateDate = result.Items[i].GetValue<DateTime?>("UpdateDate");
                        canSaveData[i].No = result.Items[i].GetValue<int>("No");
                        canSaveData[i].IsDirty = false;
                    }
                }
                #endregion

                return arbitrarys;
            }
            catch (Exception ex)
            {
                LoadingServce.CloseLoadingForm(theradID);
                MessageBox.Show(NativeLanguageService.GetText(this, "ImportFailed") + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// 验证唯一键定义 Account+ POL+ VIA+ POD+ Delivery +Origin Arb+ Dest Arb+item code + Term
        /// </summary>
        /// <param name="oldSource">oldSource</param>
        /// <param name="newItems">newItems</param>
        private bool ValidateHasExistItem(List<ClientArbitraryList> oldSource, List<ClientArbitraryList> newItems)
        {
            bool isExist = false;
            Dictionary<string, int?> existItem = new Dictionary<string, int?>();
            List<ClientArbitraryList> oldSouce = CurrentSource.FindAll(s => s.IsNew == false && s.IsDirty == false);

            #region 先Forech旧的数据,为了防止重复时错误先显示到旧数据中
            foreach (var item in oldSouce)
            {
                StringBuilder bulider = new StringBuilder();
                bulider.Append(item.POLID);
                bulider.Append(item.PODID);
                bulider.Append(item.ItemCode);
                bulider.Append(item.TransportClauseID);
                existItem.Add(bulider.ToString(), item.No);
            }
            #endregion

            #region 验证新的数据
            foreach (var item in newItems)
            {
                StringBuilder bulider = new StringBuilder();
                bulider.Append(item.POLID);
                bulider.Append(item.PODID);
                bulider.Append(item.ItemCode);
                bulider.Append(item.TransportClauseID);

                if (bulider.ToString().IsNullOrEmpty()) continue;

                if (existItem.ContainsKey(bulider.ToString()))
                {
                    isExist = true;
                    item.ErrorInfo += string.Format(NativeLanguageService.GetText(this, "ValidateItemExist"), existItem[bulider.ToString()].HasValue ? existItem[bulider.ToString()].ToString() : string.Empty);
                }
                else
                    existItem.Add(bulider.ToString(), item.No);
            }
            #endregion
            return isExist;
        }

        #region after Import

        void AfterImport(List<ClientArbitraryList> arbitrarys)
        {
            try
            {
                LoadingServce.CloseLoadingForm(theradID);

                if (arbitrarys == null) return;

                int records = arbitrarys.Count(b => b.ErrorInfo.IsNullOrEmpty());
                //arbitrarys = arbitrarys.OrderByDescending(o => o.ErrorInfo).ToList();

                List<ClientArbitraryList> source = CurrentSource;
                source.InsertRange(0, arbitrarys);
                bsList.DataSource = source;
                bsList.ResetBindings(false);
                gvMain.RefreshData();
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(),
                    string.Format(string.Format(NativeLanguageService.GetText(this, "ImportingSuccessfully"), records.ToString())));
            }
            catch { LocalCommonServices.ErrorTrace.SetErrorInfo(this, "Refresh Data failed."); }
        }

        #endregion

        #endregion

        #endregion

        #region GridViewEvent

        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                RefreshBarItemEnabled();
            }
        }
        private void RefreshBarItemEnabled()
        {
            if (CurrentRow == null)
                barCopy.Enabled = barRemove.Enabled = false;
            else
                barCopy.Enabled = barRemove.Enabled = true;
        }

        private void gvMain_MouseMove(object sender, MouseEventArgs e)
        {
            GridHitInfo hitInfo = gvMain.CalcHitInfo(e.Location);

            if (hitInfo.InRowCell == false || hitInfo.RowHandle < 0 || hitInfo.Column != colErrorInfo)
            {
                toolTip1.Hide(this);
                return;
            }

            ClientArbitraryList item = gvMain.GetRow(hitInfo.RowHandle) as ClientArbitraryList;
            if (item == null || item.HasError == false) return;
            string s = toolTip1.GetToolTip(this);
            if (toolTip1.Active && s == item.ErrorInfo) return;

            Point pt = gcMain.PointToClient(MousePosition);
            pt.X += 20;
            pt.Y += 30;
            toolTip1.Show(item.ErrorInfo, this, pt, 5000);
        }

        #endregion

        #region Commond

        #region 发布
        private void barPublish_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Workitem.Commands[OPCommonConstants.Command_PublishPauseData].Execute();
            }
        }
        #endregion

        Guid _LoadedOceanID = Guid.Empty;
        [CommandHandler(OPCommonConstants.Command_TabChanged)]
        public void Command_TabChanged(object sender, EventArgs e)
        {
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    if (Visible == false || _LoadedOceanID.IsNullOrEmpty() == false) return;
                    Enabled = _parentList != null;

                    if (_parentList != null && _parentList.ID.IsNullOrEmpty() == false)
                    {

                        List<ArbitraryList> list = OceanPriceService.GetOceanArbitrarys(_parentList.ID);
                        _LoadedOceanID = _parentList.ID;
                        DataSource = list;

                    }
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
                bsList.DataSource = typeof(ClientArbitraryList);
                return;
            }

            gvMain.ActiveFilterString = string.Empty;
            List<ArbitraryList> datas = data as List<ArbitraryList>;
            if (datas == null) datas = new List<ArbitraryList>();

            BulidGridViewColumnsByOceanUnits(_parentList.OceanUnits);
            List<ClientArbitraryList> source = OceanPriceTransformHelper.TransformOceanItemsToClientObjects(datas, _parentList.OceanUnits);

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

            int visibleIndex = 8;

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

                    case "53HQ": colRate_53HQ.VisibleIndex = visibleIndex + 26; break;
                }

                #endregion
            }

            colRemark.VisibleIndex = visibleIndex + 27;
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
                        List<ArbitraryList> list = OceanPriceService.GetOceanArbitrarys(_parentList.ID);
                        _LoadedOceanID = _parentList.ID;
                        DataSource = list;

                    }
                    else _LoadedOceanID = Guid.Empty;


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

                }
            }
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
    }


}
