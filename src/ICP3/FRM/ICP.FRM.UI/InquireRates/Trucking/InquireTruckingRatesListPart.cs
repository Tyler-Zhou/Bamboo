using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using ICP.Common.ServiceInterface;
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
    public partial class InquireTruckingRatesListPart : BaseListPart
    {
        #region Services

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        /// <summary>
        /// 询价操作服务
        /// </summary>
        public IInquireRatesService InquireRatesService
        {
            get
            {
                return ServiceClient.GetService<IInquireRatesService>();
            }
        }

        /// <summary>
        /// 搜索器客户端服务
        /// </summary>
        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        /// <summary>
        /// 询价UI数据帮助类
        /// </summary>
        public InquireRatesUIDataHelper InquireRatesUIDataHelper
        {
            get
            {
                return ClientHelper.Get<InquireRatesUIDataHelper, InquireRatesUIDataHelper>();
            }
        }

        #endregion

        #region Local variables
        /// <summary>
        /// 是否加载当前页
        /// </summary>
        bool _hadLoadCurrentPage = false;

        /// <summary>
        /// 当前查询结果
        /// </summary>
        InquierTruckingRatesResult _currentServiceSource = null;

        /// <summary>
        /// 当前数据源(克隆体)
        /// </summary>
        List<ClientInquierTruckingRate> _sourceClone = null;

        /// <summary>
        /// 查询参数
        /// </summary>
        InquireRatesSearchParameter _para = null;

        ///// <summary>
        ///// 可用列(询价价格)
        ///// </summary>
        //private List<string> _visibleColumnsNameList = null;

        #region 属性-当前数据源
        /// <summary>
        /// 当前数据源
        /// </summary>
        List<ClientInquierTruckingRate> CurrentSource
        {
            get { return bsList.DataSource as List<ClientInquierTruckingRate>; }
        }
        #endregion

        #region 属性-当前选择拖车询价
        /// <summary>
        /// 当前选择拖车询价
        /// </summary>
        List<ClientInquierTruckingRate> SelectedTruckingItem
        {
            get
            {
                if (treeMain.Selection == null || treeMain.Selection.Count == 0) return null;
                return (from TreeListNode item in treeMain.Selection select treeMain.GetDataRecordByNode(item) as ClientInquierTruckingRate).ToList();
            }
        }
        #endregion

        #region 属性-默认币种
        /// <summary>
        /// 默认币种
        /// </summary>
        public Guid? DefaultCurrencyID
        {
            get
            {
                if (InquireRatesUIDataHelper.ConfigureInfo != null)
                    return InquireRatesUIDataHelper.ConfigureInfo.DefaultCurrencyID;
                return null;
                //ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);

                //if (configureInfo != null)
                //{
                //    return configureInfo.DefaultCurrencyID;
                //}

                //return null;
            }
        }
        #endregion

        #region 属性-General Info(通用)信息面板
        /// <summary>
        /// General Info(通用)信息面板
        /// </summary>
        public InquireTruckingRatesGeneralInfoPart GeneralInfoPart
        {
            get;
            set;
        }
        #endregion

        #region 属性-是否存在数据更改
        /// <summary>
        /// 是否更改
        /// </summary>
        public bool IsChanged
        {
            get
            {
                List<ClientInquierTruckingRate> source = CurrentSource;
                if (source == null || source.Count == 0) return false;

                return source.Any(item => item.IsNew || item.IsDirty);
            }
        }
        #endregion

        #endregion

        #region  Init

        /// <summary>
        /// 构造函数
        /// </summary>
        public InquireTruckingRatesListPart()
        {
            InitializeComponent();
            EventRegister(false);
            Disposed += delegate {
                CurrentChanged = null;
                CurrentChanging = null;
                _currentServiceSource = null;
                _para = null;
                _sourceClone = null;
                //_visibleColumnsNameList = null;
                treeMain.DataSource = null;
                bsList.DataSource = null;
                EventRegister(true);
                bsList.Dispose();
                
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            if (!DesignMode) { InitMessage(); }
        }

        /// <summary>
        /// 注册Message提示字符串
        /// </summary>
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

        /// <summary>
        /// 重写加载方法
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            InitComboboxSource();
            SearchRegister();     
        }

        /// <summary>
        /// 初始化下拉框的值
        /// </summary>
        void InitComboboxSource()
        {
            #region 运输条款
            foreach (var item in InquireRatesUIDataHelper.TransportClauses)
            {
                rcmbTransportClause.Properties.Items.Add(new ImageComboBoxItem(item.Code, item.ID));
            }
           
            rcmbTransportClause.SelectedIndexChanged += delegate
            {
                treeMain.CloseEditor();
                ClientInquierTruckingRate currentrow = CurrentRow;
                if (currentrow != null)
                {
                    currentrow.TransportClauseName = InquireRatesUIDataHelper.TransportClauses.Find(t => t.ID == currentrow.TransportClauseID).Code;
                }
            };

            #endregion

            #region 币种

            foreach (var curr in InquireRatesUIDataHelper.Currencys)
            {
                cmbCurrency.Properties.Items.Add(new ImageComboBoxItem(curr.Code, curr.ID));
            }

            cmbCurrency.SelectedIndexChanged += delegate
            {
                treeMain.CloseEditor();
                ClientInquierTruckingRate currentrow = CurrentRow;
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
                ClientInquierTruckingRate currentrow = CurrentRow;
                if (currentrow != null)
                {
                    currentrow.ShippingLineName = InquireRatesUIDataHelper.ShippingLines.Find(t => t.ID == currentrow.ShippingLineID).Code;
                }
            };

            #endregion
        }

        private void SearchRegister()
        {
            #region Carrier
            DataFindClientService.RegisterTreeListColumnFinder(
                treeMain
                , colCarrier
           , CommonFinderConstants.CustomerTruckerFinder
           , new string[] { "CarrierID", "CarrierName" }
           , new string[] { "ID", LocalData.IsEnglish ? "EShortName" : "CShortName" }
           , null
           , delegate {});

            #endregion
        }

        /// <summary>
        /// 事件注册
        /// </summary>
        /// <param name="isDisposed">是否注册添加事件</param>
        private void EventRegister(bool isDisposed)
        {
            if (!isDisposed)
            {
                //TreeList
                bsList.PositionChanged += bsList_PositionChanged;       //焦点行改变时间
                treeMain.NodeCellStyle += treeMain_NodeCellStyle;       //节点样式改变
                treeMain.BeforeFocusNode += treeMain_BeforeFocusNode;   //焦点行改变之前
                treeMain.CellValueChanged += treeMain_CellValueChanged; //列值改变
                treeMain.CustomDrawNodeIndicator += treeMain_CustomDrawNodeIndicator;       //绘制行号

                //Commodity
                rbtnEditComm.ButtonClick += rbtnEditComm_ButtonClick;
                rbtnEditComm.Enter += rbtnEditComm_Enter;
                rbtnEditComm.KeyDown += rbtnEditComm_KeyDown;
                rbtnEditComm.Leave += rbtnEditComm_Leave;
            }
            else
            {
                //TreeList
                bsList.PositionChanged -= bsList_PositionChanged;       //焦点行改变时间
                treeMain.NodeCellStyle -= treeMain_NodeCellStyle;       //节点样式改变
                treeMain.BeforeFocusNode -= treeMain_BeforeFocusNode;   //焦点行改变之前
                treeMain.CellValueChanged -= treeMain_CellValueChanged; //列值改变
                treeMain.CustomDrawNodeIndicator -= treeMain_CustomDrawNodeIndicator;       //绘制行号

                //Commodity
                rbtnEditComm.ButtonClick -= rbtnEditComm_ButtonClick;
                rbtnEditComm.Enter -= rbtnEditComm_Enter;
                rbtnEditComm.KeyDown -= rbtnEditComm_KeyDown;
                rbtnEditComm.Leave -= rbtnEditComm_Leave;
            }
        }
        #endregion

        #region Base & Interface

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }

        protected ClientInquierTruckingRate CurrentRow
        {
            get { return Current as ClientInquierTruckingRate; }
            set
            {
                ClientInquierTruckingRate currentRow = CurrentRow;
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
            _currentServiceSource = data as InquierTruckingRatesResult;

            if (_currentServiceSource == null || _currentServiceSource.InquierTruckingRateList == null || _currentServiceSource.InquierTruckingRateList.Count == 0)
            {
                //ratesResult.InquierTruckingRateList = new List<InquierTruckingRate>();
                bsList.DataSource = null;
                bsList.ResetBindings(false);
            }
            else
            {
                //备份ID.
                //用于重新加载后定位到之前行记录。
                Guid? bakID = null;
                if (bsList.DataSource != null && bsList.Current != null)
                    bakID = ((ClientInquierTruckingRate)bsList.Current).ID;

                bsList.DataSource = null;
                bsList.ResetBindings(false);

                List<ClientInquierTruckingRate> source = InquireRatesHelper.TransformS2C(_currentServiceSource.InquierTruckingRateList, _currentServiceSource.MaxUnits, DefaultCurrencyID);
                _sourceClone = Utility.Clone<List<ClientInquierTruckingRate>>(source);
                bsList.DataSource = source;
                bsList.ResetBindings(false);
                treeMain.BestFitColumns();
                treeMain.ExpandAll();

                //定位到之前行记录
                if (bakID != null)
                {
                    for (int i = 0; i < bsList.Count; i++)
                    {
                        var item = ((ClientInquierTruckingRate)bsList[i]);
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
            //List<TruckingList> list = this.DataSource as List<TruckingList>;
            //if (list == null) return;
            //List<TruckingList> newLists = items as List<TruckingList>;

            //foreach (var item in newLists)
            //{
            //    TruckingList tager = list.Find(delegate(TruckingList jItem) { return item.ID == jItem.ID; });
            //    if (tager == null) continue;

            //    Utility.CopyToValue(item, tager, typeof(TruckingList));
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



        #endregion

        #endregion

        #region Commond & Event

        #region Commond

        
        [CommandHandler(InquireRatesCommandConstants.Command_TabChanged)]
        public void Command_TabChanged(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (Visible == false || _hadLoadCurrentPage) return;

                if (_para != null)
                {
                    InquierTruckingRatesResult data = InquireRatesService.GetInquireTruckingRateList(_para.No, _para.pol,
                        _para.delivery,
                        _para.pod,
                        _para.commodity,
                        _para.inquireOrRespondBy,
                        _para.isUnReply,
                        _para.durationFrom,
                        _para.durationTo, _para.StrQuery,
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
                if (SelectedTruckingItem == null) return;

                string mailContent = string.Empty;
                foreach (var item in SelectedTruckingItem)
                {
                    mailContent += "COMM: " + item.Commodity + Environment.NewLine;
                    mailContent += "FROM: " + item.POLName + Environment.NewLine;
                    mailContent += "TO: " + item.PlaceOfDeliveryName + Environment.NewLine;
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
                //移除焦点行事件，防止Copy行数据时子面板数据刷新
                treeMain.BeforeFocusNode -= treeMain_BeforeFocusNode;
                ClientInquierTruckingRate copyData = Utility.Clone<ClientInquierTruckingRate>(CurrentRow);
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
                copyData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                copyData.UpdateDate = null;
                copyData.IsDirty = true;
                bsList.PositionChanged -= bsList_PositionChanged;
                bsList.Insert(0, copyData);
                bsList.ResetBindings(false);
                treeMain.ExpandAll();
                bsList.PositionChanged += bsList_PositionChanged;
                TreeListNode node = treeMain.FindNodeByFieldValue("ID", copyData.ID);
                treeMain.Selection.Clear();
                node.Selected = true;
                treeMain.SetFocusedNode(node);
                treeMain.BeforeFocusNode += treeMain_BeforeFocusNode;
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

        [EventSubscription(InquireRatesCommandConstants.Command_NewTruckingRate)]
        public void Command_NewTruckingRate(object sender, DataEventArgs<object> e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                const string titleNo = "New Inquire Trucking";
                PartLoader.ShowEditPart<NewInquireTruckingRatePart>(Workitem, null, titleNo, AfterNewInquireTruckingPartSaved);
            }
        }

        [EventSubscription(InquireRatesCommandConstants.Command_ReInquire)]
        public void Command_ReInquire(object sender, DataEventArgs<object> e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (Visible == false) return;
                if (Current == null) return;
                InquierTruckingRate newdata = InquireRatesHelper.TransformC2S(Utility.Clone<ClientInquierTruckingRate>(CurrentRow));
                var inquireInfo = InquireRatesService.GetInquierTruckingRateInfoForInquireBy(CurrentRow.ID, LocalData.UserInfo.LoginID);
                newdata.ExpTransportClauseID = inquireInfo.ExpTransportClauseID;
                newdata.ExpTransportClauseName = inquireInfo.ExpTransportClauseName;
                newdata.ExpCarrierName = inquireInfo.ExpCarrierName;
                newdata.CustomerID = inquireInfo.CustomerID;
                newdata.CustomerName = inquireInfo.CustomerName;
                newdata.ExpPrice = inquireInfo.ExpPrice;
                newdata.CartonsOrPallets = inquireInfo.CartonsOrPallets;
                newdata.EstimateTimeOfDelivery = inquireInfo.EstimateTimeOfDelivery;
                newdata.ID = Guid.Empty;
                newdata.InquireByID = LocalData.UserInfo.LoginID;
                newdata.InquireByName = LocalData.UserInfo.LoginName;
                if (newdata.UnitRates != null)
                {
                    foreach (var sub in newdata.UnitRates)
                    {
                        sub.ID = Guid.Empty;
                        sub.Rate = 0;
                    }
                }
                newdata.FUEL = 0;
                newdata.Rate = 0;


                string titleNo = LocalData.IsEnglish ? "New Inquire Trucking" : "New Inquire Trucking";
                PartLoader.ShowEditPart<NewInquireTruckingRatePart>(Workitem, newdata, titleNo, AfterNewInquireTruckingPartSaved);
            }
        }

        [EventSubscription(InquireRatesCommandConstants.Command_Delete)]
        public void Command_Delete(object sender, DataEventArgs<object> e)
        {
            if (Visible == false) return;
            treeMain.CloseEditor();
            List<ClientInquierTruckingRate> selecteds = SelectedTruckingItem;
            if (selecteds == null || selecteds.Count == 0) return;

            #region 询问
            DialogResult result = XtraMessageBox.Show(
                                             NativeLanguageService.GetText(this, "RemoveSelectedItem"),
                                              "Tip",
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

                List<ClientInquierTruckingRate> source = CurrentSource;

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

        /// <summary>
        /// 新询价保存后回调方法
        /// </summary>
        /// <param name="prams"></param>
        private void AfterNewInquireTruckingPartSaved(object[] prams)
        {
            if (prams == null || prams.Length == 0) return;

            InquierTruckingRate newData = prams[0] as InquierTruckingRate;

            #region  需要重新统计最大箱型，多的加，少的不变

            List<InquireUnit> maxUnits = null;
            if (_currentServiceSource == null)
            {
                _currentServiceSource = new InquierTruckingRatesResult();
            }

            if (_currentServiceSource.MaxUnits == null || _currentServiceSource.MaxUnits.Count == 0)
            {
                maxUnits = newData.UnitRates;
            }
            else
            {
                maxUnits = Utility.Clone<List<InquireUnit>>(_currentServiceSource.MaxUnits);

                foreach (InquireUnit unit in newData.UnitRates)
                {
                    var findItem = (from d in _currentServiceSource.MaxUnits where d.UnitID == unit.UnitID select d).SingleOrDefault();
                    if (findItem == null)
                    {
                        maxUnits.Add(unit);
                    }
                }
            }

            _currentServiceSource.MaxUnits = maxUnits;

            #endregion

            //这里需要控制tab面板的隐藏，还没实现-- 可能不要

            if (_currentServiceSource.InquierTruckingRateList == null || _currentServiceSource.InquierTruckingRateList.Count == 0)
            {
                _currentServiceSource.InquierTruckingRateList = new List<InquierTruckingRate>();
            }

            InquierTruckingRate rateFind = _currentServiceSource.InquierTruckingRateList.Find(delegate(InquierTruckingRate item) { return item.ID == newData.ID; });
            if (rateFind == null)
            {
                _currentServiceSource.InquierTruckingRateList.Insert(0, newData);
            }
            else
            {
                Utility.CopyToValue(rateFind, newData, typeof(InquierTruckingRate));
            }

            //BulidGridViewColumnsByTruckingUnits(_currentServiceSource.MaxUnits);
            List<ClientInquierTruckingRate> dataSource = InquireRatesHelper.TransformS2C(_currentServiceSource.InquierTruckingRateList, _currentServiceSource.MaxUnits, DefaultCurrencyID);
            _sourceClone = Utility.Clone<List<ClientInquierTruckingRate>>(dataSource);
            bsList.DataSource = dataSource;
            bsList.ResetBindings(false);
            treeMain.BestFitColumns();
            treeMain.ExpandAll();
            //if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
        }

        #endregion

        #region EventSubscription

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
                        InquierTruckingRatesResult data = InquireRatesService.GetInquireTruckingRateList(_para.No, _para.pol,
                            _para.delivery,
                            _para.pod,
                            _para.commodity,
                            _para.inquireOrRespondBy,
                            _para.isUnReply,
                            _para.durationFrom,
                            _para.durationTo, _para.StrQuery,
                            LocalData.UserInfo.LoginID);

                        DataSource = data;
                        _hadLoadCurrentPage = true;

                        string message = string.Empty;
                        if (data == null || data.InquierTruckingRateList == null || data.InquierTruckingRateList.Count <= 0)
                        {
                            message = "Nothing found!";
                            bsList_PositionChanged(null, null);
                        }
                        else
                        {
                            message = string.Format("{0} records found", data.InquierTruckingRateList.Count);
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

        /// <summary>
        /// 询价历史替换
        ///     将历史记录与新询价合并,删除新询价,保存历史询价
        /// </summary>
        [EventSubscription(InquireRatesCommandConstants.Command_HistoryReplace)]
        public void Command_HistoryReplace(object sender, DataEventArgs<object> e)
        {
            try
            {
                ClientInquierTruckingRate historyData = e.Data as ClientInquierTruckingRate;
                if (historyData == null) return;
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    if (CurrentRow == null || historyData.ID == null || historyData.ID == Guid.Empty)
                    {
                        XtraMessageBox.Show("No record is selected.", "Tip");
                        return;
                    }
                    if (CurrentRow.RateUnitList.Any(item => item.Rate > 0))
                    {
                        XtraMessageBox.Show("The new Inquire Price could not be merged because it has rates already.", "Tip");
                        return;
                    }

                    #region 询问

                    var inquireBys = string.Empty;
                    CurrentRow.InquirePriceInquireBysList = InquireRatesService.GetInquirePriceInquireBys(CurrentRow.ID, CurrentRow.MainRecordID);
                    foreach (var item in CurrentRow.InquirePriceInquireBysList)
                    {
                        if (string.IsNullOrEmpty(inquireBys))
                            inquireBys += item.InquireByEName;
                        else
                            inquireBys += ',' + item.InquireByEName;
                    }

                    var result = XtraMessageBox.Show(
                                                     "Are you sure to use the selected rates instead of the new Inquire Price? \r\nNote: "
                                                     + inquireBys + " will be noticed with the Rates.",
                                                      LocalData.IsEnglish ? "Tip" : "Tip",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question);

                    if (result == DialogResult.No) return;

                    #endregion

                    Guid? oldID = Guid.Empty;
                    Guid? newID = Guid.Empty;
                    //只取主线ID（ParentID）
                    oldID = CurrentRow.MainRecordID ?? CurrentRow.ID;
                    newID = historyData.MainRecordID ?? historyData.ID;
                    var resultData = InquireRatesService.ReplaceInquirePrice(oldID.Value, newID.Value);
                    if (resultData != null)
                    {
                        //刷新列表
                        Workitem.Commands[InquireRatesCommandConstants.Command_RefreshData].Execute();
                        //选中替换行
                        foreach (TreeListNode node in treeMain.Nodes)
                        {
                            if (node.GetValue(colID).Equals(resultData.ID))
                            {
                                treeMain.Selection.Clear();
                                //node.Selected = true;
                                treeMain.SetFocusedNode(node);
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }

        /// <summary>
        /// 询价历史复制
        ///     将历史记录的询价箱型、备注等复制到新询价
        /// </summary>
        [EventSubscription(InquireRatesCommandConstants.Command_HistoryCopy)]
        public void Command_HistoryCopy(object sender, DataEventArgs<object> e)
        {
            try
            {
                ClientInquierTruckingRate historyData = e.Data as ClientInquierTruckingRate;
                if (historyData == null) return;
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    if (CurrentRow == null || historyData.ID == null || historyData.ID == Guid.Empty)
                    {
                        XtraMessageBox.Show("No record is selected.", "Tip");
                        return;
                    }
                    if (CurrentRow.RateUnitList.Any(item => item.Rate > 0))
                    {
                        XtraMessageBox.Show("The new Inquire Price could not be merged because it has rates already.", "Tip");
                        return;
                    }

                    #region 询问

                    var inquireBys = string.Empty;
                    CurrentRow.InquirePriceInquireBysList = InquireRatesService.GetInquirePriceInquireBys(CurrentRow.ID, CurrentRow.MainRecordID);
                    foreach (var item in CurrentRow.InquirePriceInquireBysList)
                    {
                        if (string.IsNullOrEmpty(inquireBys))
                            inquireBys += item.InquireByEName;
                        else
                            inquireBys += ',' + item.InquireByEName;
                    }

                    var result = XtraMessageBox.Show(
                                                     "Are you sure to copy the selected rates and remark into the new Inquire Price? \r\nNote: " + inquireBys + " will be noticed with the Rates.",
                                                      "Tip",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question);

                    if (result == DialogResult.No) return;

                    #endregion
                    CurrentRow.Remark = historyData.Remark;
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }
        #endregion

        #region TreeList Event

        /// <summary>
        /// 焦点行改变事件:设置列的编辑状态、验证数据是否更改
        /// </summary>
        private void bsList_PositionChanged(object sender, EventArgs e)
        {

            SetEditState();

            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        /// <summary>
        /// 节点样式改变：区分价格回复
        /// </summary>
        private void treeMain_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node == null) return;
            ClientInquierTruckingRate listData = (ClientInquierTruckingRate)treeMain.GetDataRecordByNode(e.Node);
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
        /// 节点列值改变：计算询价价格合计
        /// </summary>
        private void treeMain_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Node == null) return;

            if (e.Column.Name == colFUEL.Name)  //FUEL列改变
            {
                #region FUEL列改变
                ClientInquierTruckingRate listData = treeMain.GetDataRecordByNode(e.Node) as ClientInquierTruckingRate;
                if (listData == null || listData.FUEL == null) return;

                List<ClientInquierTruckingRate> rates = bsList.DataSource as List<ClientInquierTruckingRate>;
                //1.不存在与当前行POL向同行时直接计算合计到合计列
                int count = rates.Where(i => i.POLID == listData.POLID).ToList().Count;
                if (count == 1)
                {
                    if (listData.Rate != null)
                    {
                        listData.Total = decimal.Parse((listData.Rate.Value * listData.FUEL.Value).ToString("n"));
                    }

                    listData.IsDirty = true;
                    return;
                }
                //2.存在多条相同POL提示是否批量更新
                string message = string.Format("Do you want to batch update the 'FUEL' of the current list to meet the conditions that 'From' is '{0}'?", listData.POLName);
                DialogResult result = XtraMessageBox.Show(message
                 , "Tip"
                 , MessageBoxButtons.YesNo
                 , MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    #region 确认批量更新所有相同POL记录FUEL
                    decimal currentFUEL = listData.FUEL.Value;
                    List<ClientInquierTruckingRate> needSaveList = new List<ClientInquierTruckingRate>();
                    List<Guid> ids = new List<Guid>();
                    List<DateTime?> updateDates = new List<DateTime?>();
                    //构建需更新List
                    foreach (var item in rates)
                    {
                        if (item.POLID == listData.POLID)
                        {
                            item.FUEL = currentFUEL;
                            if (item.Rate != null)
                            {
                                item.Total = decimal.Parse((item.Rate.Value * currentFUEL).ToString("n"));
                            }
                            ids.Add(item.ID);
                            updateDates.Add(item.UpdateDate);
                            needSaveList.Add(item);
                        }
                    }

                    try
                    {
                        //开始批量更新
                        //更新服务端(DataBase)
                        ManyResultData resultData = InquireRatesService.BatchUpdateChargeFuelForInquirePrices(ids.ToArray(), updateDates.ToArray(), currentFUEL, LocalData.UserInfo.LoginID);
                        for (int i = 0; i < resultData.ChildResults.Count; i++)
                        {
                            needSaveList[i].ID = resultData.ChildResults[i].ID;
                            needSaveList[i].UpdateDate = resultData.ChildResults[i].UpdateDate;
                            if (needSaveList[i].ID != listData.ID)
                            {
                                needSaveList[i].IsDirty = false;
                            }

                            needSaveList[i].IsNewRecord = false;
                        }

                        bsList.ResetBindings(false);
                        //定位到当前编辑行并更新当前网格数据
                        ClientInquierTruckingRate tager = _sourceClone.Find(delegate(ClientInquierTruckingRate item) { return item.ID == CurrentRow.ID; });
                        if (tager != null)
                        {
                            tager.FUEL = listData.FUEL;
                            if (tager.Rate != null)
                            {
                                tager.Total = tager.FUEL * tager.Rate;
                            }

                            tager.UpdateDate = listData.UpdateDate;
                        }

                        _sourceClone = Utility.Clone(bsList.DataSource as List<ClientInquierTruckingRate>);
                        ClientInquierTruckingRate current = _sourceClone.Find(delegate(ClientInquierTruckingRate item) { return item.ID == CurrentRow.ID; });
                        Utility.CopyToValue(tager, current, typeof(ClientInquierTruckingRate));
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), "Batch update 'FUEL' successfully!");
                    }
                    catch (Exception ex)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
                    }  
                    #endregion
                }
                else   //只更新当前行
                {
                    if (listData.Rate != null)
                    {
                        listData.Total = decimal.Parse((listData.Rate.Value * listData.FUEL.Value).ToString("n"));
                    }

                    listData.IsDirty = true;
                }
                #endregion
            }
            else if (e.Column.Name == colRate.Name)  //Rate列改变:计算合计Total
            {
                ClientInquierTruckingRate listData = treeMain.GetDataRecordByNode(e.Node) as ClientInquierTruckingRate;
                if (listData == null || listData.Rate == null || listData.FUEL == null) return;
                listData.Total = decimal.Parse((listData.Rate.Value * listData.FUEL.Value).ToString("n"));
                listData.IsDirty = true;
            }
            else if (e.Column.Name == colDurationFrom.Name)  //DurationFrom列改变：DurationTo设置为DurationFrom3个月后
            {
                ClientInquierTruckingRate listData = treeMain.GetDataRecordByNode(e.Node) as ClientInquierTruckingRate;
                if (listData == null || listData.DurationFrom == null) return;
                listData.DurationTo = listData.DurationFrom.Value.AddMonths(3);
                //bsList.ResetBindings(false);
                listData.IsDirty = true;
            }
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

        /// <summary>
        /// 焦点行数据改变前验证是否父节点改变，父节点改变则调用行改变事件以刷新子面板
        /// </summary>
        private void treeMain_BeforeFocusNode(object sender, BeforeFocusNodeEventArgs e)
        {
            if (e.OldNode == null || e.Node == null) return;
            ClientInquierTruckingRate dataOld = treeMain.GetDataRecordByNode(e.OldNode) as ClientInquierTruckingRate;
            ClientInquierTruckingRate dataNew = treeMain.GetDataRecordByNode(e.Node) as ClientInquierTruckingRate;
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
        /// 设置编辑框是否启用编辑：当前登录用户为答复人时编辑可用
        /// </summary>
        void SetEditState()
        {
            ClientInquierTruckingRate data = (ClientInquierTruckingRate)bsList.Current;
            if (data == null) return;
            foreach (TreeListColumn sub in treeMain.Columns)
            {
                sub.OptionsColumn.ReadOnly = false;
            }
            //if (LocalData.UserInfo.LoginID == data.RespondByID)
            //{
            //    foreach (TreeListColumn sub in treeMain.Columns)
            //    {
            //        sub.OptionsColumn.ReadOnly = false;
            //    }
            //}
            //else
            //{
            //    foreach (TreeListColumn sub in treeMain.Columns)
            //    {
            //        sub.OptionsColumn.ReadOnly = true;
            //    }
            //}
        }

        #endregion

        #region Commodity

        /// <summary>
        /// 进入网格editer时记录text,如果在离开网格时有改变,就执行搜索
        /// </summary>
        string _enterText = string.Empty;
        private void rbtnEditComm_Enter(object sender, EventArgs e)
        {
            _enterText = ((TextEdit)(sender)).Text;
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
                    ClientInquierTruckingRate row = treeMain.GetDataRecordByNode(treeMain.FindNodeByID(_BeforeLeaveRowHandle)) as ClientInquierTruckingRate;
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
            ((TextEdit)(sender)).Leave -= rbtnEditComm_Leave;



            if (e.KeyCode == Keys.Enter)
            {
                string leaveText = ((TextEdit)(sender)).Text;
                if (leaveText != _enterText)
                {
                    SearchComm();
                }
                _enterText = ((TextEdit)(sender)).Text;
            }

            ((TextEdit)(sender)).Leave += rbtnEditComm_Leave;
        }

        private void rbtnEditComm_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            SearchComm();
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

        #endregion

        #region Method
        /// <summary>
        /// 重置当前行(本地变量CurrentRow)
        /// </summary>
        public void ResetCurrent()
        {
            ClientInquierTruckingRate tager = _sourceClone.Find(item => item.ID == CurrentRow.ID);
            if (tager == null) return;
            Utility.CopyToValue(tager, CurrentRow, typeof(ClientInquierTruckingRate));
            bsList.ResetCurrentItem();
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        /// <param name="needSaveList">需要保存的询价List</param>
        /// <returns></returns>
        bool ValidateData(IEnumerable<ClientInquierTruckingRate> needSaveList)
        {
            bool value = true;
            string eError = @"[{0}] is required! You must fill-in it.";
            string cError = @"请您输入必填项：[{0}]。";

            foreach (var item in needSaveList)
            {
                //POL
                if (Utility.GuidIsNullOrEmpty(item.POLID))
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ?
                        string.Format(eError, colPOL.Caption)
                        : string.Format(cError, colPOL.Caption));
                    value = false;
                }
                //Delivery
                if (Utility.GuidIsNullOrEmpty(item.PlaceOfDeliveryID))
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ?
                        string.Format(eError, colPlaceOfDelivery.Caption)
                        : string.Format(cError, colPlaceOfDelivery.Caption));
                    value = false;
                }
                //ShippingLine
                if (Utility.GuidIsNullOrEmpty(item.ShippingLineID))
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ?
                        string.Format(eError, colShipline.Caption)
                        : string.Format(cError, colShipline.Caption));
                    value = false;
                }
                //ZipCode
                if (String.IsNullOrEmpty(item.ZipCode))
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ?
                        string.Format(eError, colZipCode.Caption)
                        : string.Format(cError, colZipCode.Caption));
                    value = false;
                }
                //Rate
                if (item.Rate == null)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ?
                        string.Format(eError, colRate.Caption)
                        : string.Format(cError, colRate.Caption));
                    value = false;
                }
                //FUEL
                if (item.FUEL == null)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ?
                        string.Format(eError, colFUEL.Caption)
                        : string.Format(cError, colFUEL.Caption));
                    value = false;
                }
            }

            return value;
        }
        /// <summary>
        /// 保存询价列表
        /// </summary>
        /// <param name="isNeedRefreshEmailPart">是否需要刷新邮件面板</param>
        /// <returns></returns>
        public bool SaveRateList(bool isNeedRefreshEmailPart)
        {
            try
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
                //if (LocalData.UserInfo.LoginID != CurrentRow.RespondByID) return false;
                CurrentRow.RespondByID = LocalData.UserInfo.LoginID;
                treeMain.CloseEditor();
                List<ClientInquierTruckingRate> needSaveList = new List<ClientInquierTruckingRate>();
                List<ClientInquierTruckingRate> datas = bsList.DataSource as List<ClientInquierTruckingRate>;
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

                if (ValidateData(needSaveList) == false) return false;
                ManyResultData result = InquireRatesService.SaveTruckingInquireRateWithTrans(
                    InquireRatesHelper.TransformC2S(needSaveList), DateTime.Now, LocalData.UserInfo.DefaultCompanyID
                    , LocalData.UserInfo.LoginID);
                for (int i = 0; i < result.ChildResults.Count; i++)
                {
                    needSaveList[i].ID = result.ChildResults[i].ID;
                    needSaveList[i].UpdateDate = result.ChildResults[i].UpdateDate;
                    needSaveList[i].IsDirty = false;
                    needSaveList[i].IsNewRecord = false;
                }

                bsList.ResetBindings(false);
                //_sourceClone = bsList.DataSource as List<ClientInquierTruckingRate>;
                _sourceClone = Utility.Clone<List<ClientInquierTruckingRate>>(bsList.DataSource as List<ClientInquierTruckingRate>);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), "Save Successfully");
                if (isNeedRefreshEmailPart)
                {
                    //TODO:是否需要刷新邮件面板
                }
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
                return false;
            }
        }
        #endregion

        #region Comment Code

        #region BulidColumns

        //private void BulidGridViewColumnsByTruckingUnits(List<InquireUnit> units)
        //{

        //    _visibleColumnsNameList = new List<string>();

        //    #region  SetVisible= false;
        //    colRate_45FR.Visible = false;
        //    colRate_40RF.Visible = false;
        //    colRate_45HT.Visible = false;
        //    colRate_20RF.Visible = false;
        //    colRate_20HQ.Visible = false;
        //    colRate_20TK.Visible = false;
        //    colRate_20GP.Visible = false;
        //    colRate_40TK.Visible = false;
        //    colRate_40OT.Visible = false;
        //    colRate_20FR.Visible = false;
        //    colRate_45GP.Visible = false;
        //    colRate_40GP.Visible = false;
        //    colRate_45RF.Visible = false;
        //    colRate_20RH.Visible = false;
        //    colRate_45OT.Visible = false;
        //    colRate_40NOR.Visible = false;
        //    colRate_40FR.Visible = false;
        //    colRate_20OT.Visible = false;
        //    colRate_45TK.Visible = false;
        //    colRate_20NOR.Visible = false;
        //    colRate_40HT.Visible = false;
        //    colRate_40RH.Visible = false;
        //    colRate_45RH.Visible = false;
        //    colRate_45HQ.Visible = false;
        //    colRate_20HT.Visible = false;
        //    colRate_40HQ.Visible = false;
        //    #endregion

        //    int visibleIndex = 6;

        //    foreach (var item in units)
        //    {
        //        #region  SetVisible= true;
        //        switch (item.UnitName)
        //        {
        //            case "20GP": colRate_20GP.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("20GP"); break;
        //            case "40GP": colRate_40GP.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("40GP"); break;
        //            case "40HQ": colRate_40HQ.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("40HQ"); break;
        //            case "45HQ": colRate_45HQ.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("45HQ"); break;
        //            case "20NOR": colRate_20NOR.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("20NOR"); break;
        //            case "40NOR": colRate_40NOR.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("40NOR"); break;

        //            case "20FR": colRate_20FR.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("20FR"); break;
        //            case "20RH": colRate_20RH.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("20RH"); break;
        //            case "20RF": colRate_20RF.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("20RF"); break;
        //            case "20HQ": colRate_20HQ.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("20HQ"); break;
        //            case "20TK": colRate_20TK.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("20TK"); break;
        //            case "20OT": colRate_20OT.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("20OT"); break;
        //            case "20HT": colRate_20HT.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("20HT"); break;

        //            case "40TK": colRate_40TK.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("40TK"); break;
        //            case "40OT": colRate_40OT.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("40OT"); break;
        //            case "40FR": colRate_40FR.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("40FR"); break;
        //            case "40HT": colRate_40HT.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("40HT"); break;
        //            case "40RH": colRate_40RH.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("40RH"); break;
        //            case "40RF": colRate_40RF.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("40RF"); break;

        //            case "45GP": colRate_45GP.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("45GP"); break;
        //            case "45RF": colRate_45RF.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("45RF"); break;
        //            case "45HT": colRate_45HT.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("45HT"); break;
        //            case "45FR": colRate_45FR.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("45FR"); break;
        //            case "45OT": colRate_45OT.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("45OT"); break;
        //            case "45TK": colRate_45TK.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("45TK"); break;
        //            case "45RH": colRate_45RH.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("45RH"); break;
        //        }

        //        visibleIndex++;
        //        #endregion
        //    }
        //}

        #endregion

        #region 工作流

        //#region 增删改

        //private void barInsert_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    AddData();
        //}

        //private void AddData()
        //{
        //    //ClientBasePortList newData = new ClientBasePortList();
        //    //TruckingPriceTransformHelper.BulidNewBasePortData(newData, _parentList);
        //    //newData.BulidRateToZeroByTruckingUints(_parentList.TruckingUnits);
        //    //newData.BeginEdit();

        //    //(bsList.List as List<ClientBasePortList>).Insert(0, newData);
        //    //bsList.ResetBindings(false);

        //    //gvMain.CancelSelection();
        //    //gvMain.FocusedRowHandle = 0;
        //    //gvMain.SelectCell(0, colAccount);
        //}

        //private void barCopy_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    CopyData();
        //}

        //private void CopyData()
        //{
        //    List<ClientInquierTruckingRate> selecteds = SelectedTruckingItem;
        //    if (CurrentRow == null || selecteds == null || selecteds.Count == 0) return;


        //    List<ClientInquierTruckingRate> copyTager = new List<ClientInquierTruckingRate>();
        //    foreach (var item in selecteds)
        //    {
        //        ClientInquierTruckingRate newItem = Utility.Clone<ClientInquierTruckingRate>(item);
        //        newItem.ID = Guid.Empty;
        //        newItem.InquireByID = LocalData.UserInfo.LoginID;
        //        newItem.InquireByName = LocalData.UserInfo.LoginName;
        //        newItem.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
        //        //newItem.DurationFrom = _parentList.FromDate;
        //        //newItem.DurationTo = _parentList.ToDate;
        //        newItem.UpdateDate = null;

        //        item.BeginEdit();
        //        copyTager.Add(newItem);
        //    }


        //    List<ClientInquierTruckingRate> source = CurrentSource;
        //    foreach (var item in copyTager)
        //    {
        //        source.Insert(0, item);
        //    }

        //    bsList.DataSource = source;
        //    bsList.ResetBindings(false);
        //}

        //#region Save

        //internal void RefreshUIData()
        //{
        //    List<ClientInquierTruckingRate> source = CurrentSource;
        //    //source = source.OrderByDescending(b => b.No).ToList();
        //    bsList.DataSource = source;
        //    treeMain.RefreshDataSource();
        //}

        //internal List<ClientInquierTruckingRate> GetChangedItem()
        //{
        //    List<ClientInquierTruckingRate> source = CurrentSource;
        //    List<ClientInquierTruckingRate> chengedItem = new List<ClientInquierTruckingRate>();
        //    foreach (var item in source)
        //    {
        //        if (item.IsNew || item.IsDirty) chengedItem.Add(item);
        //    }
        //    return chengedItem;
        //}

        //public bool ValidateData()
        //{
        //    if (IsChanged == false) return true;

        //    Validate();
        //    bsList.EndEdit();

        //    List<ClientInquierTruckingRate> chengedItem = GetChangedItem();

        //    //if (this.ValidateData(chengedItem) == false) return false;  要的啊

        //    return true;
        //}


        //#endregion

        //#endregion     

        #endregion

        ////pearl
        //private bool ValidateData(List<ClientInquierTruckingRate> chengedItems)
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
        //界面输入验证
        ////bool ValidateData(List<ClientInquierTruckingRate> rateList)
        ////{
        ////    bool isScrrs = true;
        ////    foreach (var item in rateList)
        ////    {
        ////        if (string.IsNullOrEmpty(item.ZipCode))
        ////        {
        ////            LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), "'Zip Code' must input");
        ////            isScrrs = false;
        ////        }

        ////        if (item.Rate == null)
        ////        {
        ////            LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), "'Rate must' input");
        ////            isScrrs = false;
        ////        }

        ////        if (item.FUEL == null)
        ////        {
        ////            LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), "'FUEL' must input");
        ////            isScrrs = false;
        ////        }
        ////    }

        ////    return isScrrs;
        ////} 
        #endregion

    }
}
