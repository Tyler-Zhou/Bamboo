using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Extender;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ICP.Business.Common.UI.QuotedPrice
{
    /// <summary>
    /// 报价价格编辑网格
    /// </summary>
    [ToolboxItem(false)]
    [SmartPart]
    public partial class QuotedPriceRatesPart : BaseEditPart
    {
        #region Fields & Property & Service
        #region Fields
        ConfigureInfo _ConfigureInfo;
        /// <summary>
        /// 币种集合
        /// </summary>
        List<SolutionCurrencyList> _CurrencyList = null;
        /// <summary>
        /// 箱型集合
        /// </summary>
        List<ContainerList> _ctnTypes = null;
        #endregion

        #region Service
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem RootWorkItem { get; set; }
        /// <summary>
        /// 报价服务
        /// </summary>
        public IFCMCommonService IFCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
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
        /// 公共客户管理服务
        /// </summary>
        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }
        /// <summary>
        /// 配置文件
        /// </summary>
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }
        /// <summary>
        /// 基础数据服务类
        /// </summary>
        ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }
        #endregion

        #region Property
        /// <summary>
        /// DataSource
        /// </summary>
        public override object DataSource
        {
            get
            {
                List<QuotedPriceRatesList> list = new List<QuotedPriceRatesList>();
                if (bsQPRatesList.DataSource is List<QuotedPriceRatesList>)
                    list.AddRange(bsQPRatesList.DataSource as List<QuotedPriceRatesList>);
                return list;
            }
        }

        /// <summary>
        /// 当前数据源
        /// </summary>
        List<QuotedPriceRatesList> CurrentSource
        {
            get { return bsQPRatesList.DataSource as List<QuotedPriceRatesList>; }
        }

        /// <summary>
        /// 当前行
        /// </summary>
        QuotedPriceRatesList Current
        {
            get { return bsQPRatesList.Current as QuotedPriceRatesList; }
        }

        List<QuotedPriceRatesList> SelectRows
        {
            get
            {
                int[] indexs = gvRatesList.GetSelectedRows();
                if (indexs == null || indexs.Length == 0) return null;

                return indexs.Select(item => gvRatesList.GetRow(item)).OfType<QuotedPriceRatesList>().ToList();
            }
        }

        List<QPSurcharge> SelectSurchargRows
        {
            get
            {
                int[] indexs = gvSurcharges.GetSelectedRows();
                if (indexs == null || indexs.Length == 0) return null;

                return indexs.Select(item => gvSurcharges.GetRow(item)).OfType<QPSurcharge>().ToList();
            }
        }

        bool _isChanged = false;
        /// <summary>
        /// 是否改变
        /// </summary>
        public bool IsChanged
        {
            get
            {
                if (_isChanged == false)
                {
                    List<QuotedPriceRatesList> source = bsQPRatesList.DataSource as List<QuotedPriceRatesList>;
                    if (source != null)
                    {
                        if (source.Any(item => item.IsDirty))
                        {
                            return true;
                        }
                    }
                }

                return _isChanged;
            }
        }
        /// <summary>
        /// 是否预览
        /// </summary>
        public bool IsView
        {
            get;
            set;
        }
        #endregion

        #region Delegate
        IDisposable chargingCodeFinder;
        #endregion
        #endregion

        #region Init
        /// <summary>
        /// 报价价格编辑网格
        /// </summary>
        public QuotedPriceRatesPart()
        {
            InitializeComponent();
            RegisterEvent();
            Disposed += delegate
            {
                UnRegisterEvent();
                if (RootWorkItem != null)
                {
                    RootWorkItem.Items.Remove(this);
                    RootWorkItem = null;
                }

            };
        }
        /// <summary>
        /// 加载
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                InitControls();
            }
        }

        #endregion

        #region Control Event
        /// <summary>
        /// 列值改变
        /// </summary>
        void gvRatesList_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            _isChanged = true;
        }
        /// <summary>
        /// 新增报价
        /// </summary>
        void barRatesAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {

                    QuotedPriceRatesList newData = new QuotedPriceRatesList
                    {
                        ID = Guid.Empty,
                        CreateByID = LocalData.UserInfo.LoginID,
                        CreateByName = LocalData.UserInfo.LoginName,
                        CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified),
                        UpdateDate = null
                    };
                    gvRatesList.ClearSorting();
                    bsQPRatesList.Add(newData);
                    bsQPRatesList.ResetBindings(false);
                    _isChanged = true;
                    gvRatesList.MoveLast();
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        /// <summary>
        /// 删除报价
        /// </summary>
        void barRatesRemove_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                List<QuotedPriceRatesList> list = SelectRows;
                if (list == null || list.Count == 0) return;

                if (!PartLoader.EnquireIsDeleteCurrentData())
                {
                    return;
                }

                List<Guid> needRemoveIDs = new List<Guid>();
                List<DateTime?> needRemoveUpdateDate = new List<DateTime?>();

                foreach (var item in list)
                {
                    if (ArgumentHelper.GuidIsNullOrEmpty(item.ID) == false)
                    {
                        needRemoveIDs.Add(item.ID);
                        needRemoveUpdateDate.Add(item.UpdateDate);
                    }
                }

                if (needRemoveIDs.Count != 0)
                {
                    IFCMCommonService.RemoveQuotedPriceRatesList(needRemoveIDs.ToArray(), LocalData.UserInfo.LoginID, needRemoveUpdateDate.ToArray());
                }
                _isChanged = true;
                gvRatesList.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), (LocalData.IsEnglish ? "Delete Faily" : "删除失败.") + ex.Message);
            }
        }
        /// <summary>
        /// 报价行改变事件
        /// </summary>
        void BsQPRatesList_PositionChanged(object sender, EventArgs e)
        {
            try
            {
                SetSurchargeDataSource();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }

        private void BsQPRatesList_CurrentChanged(object sender, EventArgs e)
        {
            try
            {
                SetSurchargeDataSource();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }

        void GvRatesList_BeforeLeaveRow(object sender, RowAllowEventArgs e)
        {
            try
            {
                SetSurcharge();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        void GvSurcharges_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            _isChanged = true;
            Current.IsDirty = true;
        }
        /// <summary>
        /// 
        /// </summary>
        void BarSurchargesAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (Current == null) return;
                using (new CursorHelper(Cursors.WaitCursor))
                {

                    QPSurcharge newData = new QPSurcharge()
                    {
                        CurrencyID = new Guid("D67186CE-8B2C-4A75-81F1-A4FE3CC12DE9"),
                    };
                    gvSurcharges.ClearSorting();
                    bsSurcharge.Add(newData);
                    bsSurcharge.ResetBindings(false);
                    _isChanged = true;
                    gvSurcharges.MoveLast();
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BarSurchargesDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (Current == null) return;
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    List<QPSurcharge> list = SelectSurchargRows;
                    if (list == null || list.Count == 0) return;

                    if (!PartLoader.EnquireIsDeleteCurrentData())
                    {
                        return;
                    }
                    _isChanged = true;
                    gvSurcharges.DeleteSelectedRows();
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), (LocalData.IsEnglish ? "Delete Faily" : "删除失败.") + ex.Message);
            }
        }
        #endregion

        #region Custom Method
        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="contex">绑定数据</param>
        public void DataBind(BusinessOperationContext contex)
        {
            if (contex == null)
            {
                Enabled = false;
                return;
            }
            Enabled = true;
            List<QuotedPriceRatesList> ratesList = IFCMCommonService.GetQuotedPriceRatesList(contex.OperationID);
            if (ratesList != null && ratesList.Count > 0)
            {
                bsQPRatesList.DataSource = ratesList;
                bsQPRatesList.ResetBindings(false);
            }
        }
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        public bool ValidateData()
        {
            if (IsChanged == false) return true;
            Validate();
            bsQPRatesList.EndEdit();

            if ((from object item in bsQPRatesList.List select item as QuotedPriceRatesList).Any(quotedPriceRatesList => quotedPriceRatesList != null && quotedPriceRatesList.Validate
                () == false))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 设置数据源
        /// </summary>
        /// <param name="value"></param>
        public void SetSource(object value)
        {
            if (value == null) return;
            if ((value as List<QuotedPriceRatesList>).Count == 0) return;
            _isChanged = false;
            bsQPRatesList.DataSource = value;
            bsQPRatesList.ResetBindings(false);
        }
        /// <summary>
        /// 保存费用
        /// </summary>
        /// <param name="qpOrderID">报价单ID</param>
        /// <returns></returns>
        public List<QPRatesSaveRequest> BuildRatesList(Guid qpOrderID)
        {
            SetSurcharge();
            EndEditByRates();
            List<QuotedPriceRatesList> rates = DataSource as List<QuotedPriceRatesList>;
            if (rates != null && (rates.Count != 0 || bsQPRatesList.List.Count != 0))
            {
                //把默认的数据值追加到集合中
                if (rates.Any() == false)
                {
                    rates.AddRange(from object fee in bsQPRatesList.List select fee as QuotedPriceRatesList);
                }
                List<QuotedPriceRatesList> changedRates = rates.FindAll(o => o.IsDirty);
                if (qpOrderID == Guid.Empty)
                {
                    changedRates = rates;
                }

                if (changedRates.Count > 0)
                {
                    List<QPRatesSaveRequest> commands = new List<QPRatesSaveRequest>();

                    List<Guid?> ids = new List<Guid?>();
                    List<Guid?> placeOfReceiptIDs = new List<Guid?>();
                    List<Guid> polIDs = new List<Guid>(), podIDs = new List<Guid>(),placeOfDeliveryIDs=new List<Guid>();
                    List<string> carriers = new List<string>(),
                        surcharges=new List<string>();
                    List<short> tts = new List<short>();
                    List<int> unit20 = new List<int>(), unit40 = new List<int>(), unit40HQ = new List<int>(), unit45 = new List<int>();
                    List<DateTime?> updateDates = new List<DateTime?>();
                    foreach (var item in changedRates)
                    {
                        ids.Add(item.ID);
                        placeOfReceiptIDs.Add(item.PlaceOfReceiptID);
                        polIDs.Add(item.POLID);
                        podIDs.Add(item.PODID);
                        placeOfDeliveryIDs.Add(item.PlaceOfDeliveryID);
                        carriers.Add(item.Carrier);
                        tts.Add(item.TT);
                        unit20.Add(item.Unit20);
                        unit40.Add(item.Unit40);
                        unit40HQ.Add(item.Unit40HQ);
                        unit45.Add(item.Unit45);
                        surcharges.Add(item.SurchargeDescription);
                        updateDates.Add(item.UpdateDate);
                    }
                    if (ids.Any())
                    {
                        QPRatesSaveRequest ratesInfo = new QPRatesSaveRequest
                        {
                            qpOrderID = qpOrderID,
                            ids = ids.ToArray(),
                            placeOfReceiptIDs = placeOfReceiptIDs.ToArray(),
                            polIDs = polIDs.ToArray(),
                            podIDs = podIDs.ToArray(),
                            placeOfDeliveryIDs = placeOfDeliveryIDs.ToArray(),
                            carriers=carriers.ToArray(),
                            tts = tts.ToArray(),
                            unit20 = unit20.ToArray(),
                            unit40 = unit40.ToArray(),
                            unit40HQ = unit40HQ.ToArray(),
                            unit45 = unit45.ToArray(),
                            surcharges = surcharges.ToArray(),
                            saveByID = LocalData.UserInfo.LoginID,
                            updateDates = updateDates.ToArray()
                        };

                        changedRates.ForEach(ratesInfo.AddInvolvedObject);

                        commands.Add(ratesInfo);
                    }
                    return commands;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 刷新UI界面
        /// </summary>
        /// <param name="list"></param>
        public void RefreshUI(List<QPRatesSaveRequest> list)
        {
            foreach (QPRatesSaveRequest feeInfo in list)
            {
                List<QuotedPriceRatesList> changedRates = feeInfo.UnBoxInvolvedObject<QuotedPriceRatesList>();
                ManyResult result = feeInfo.ManyResult;

                int Count = 0;
                Count = list.Count == changedRates.Count ? changedRates.Count : list.Count;
                for (int i = 0; i < Count; i++)
                {
                    changedRates[i].ID = result.Items[i].GetValue<Guid>("ID");
                    changedRates[i].UpdateDate = result.Items[i].GetValue<DateTime?>("UpdateDate");
                    changedRates[i].IsDirty = false;
                }
            }
            _isChanged = false;
        }

        private void SetLanguage()
        {
            if (!LocalData.IsEnglish)
            {
                barRatesAdd.Caption = "新增";
                barRatesRemove.Caption = "删除";
                colPlaceOfReceiptName.Caption = "收货地";
                colPOLName.Caption = "装货港";
                colPODName.Caption = "卸货港";
                colPlaceOfDeliveryName.Caption = "交货地";
                colCarrier.Caption = "船公司";
            }
        }
        
        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvent()
        {
            barRatesAdd.ItemClick += barRatesAdd_ItemClick;
            barRatesRemove.ItemClick += barRatesRemove_ItemClick;
            gvRatesList.CellValueChanged += gvRatesList_CellValueChanged;
            bsQPRatesList.PositionChanged += BsQPRatesList_PositionChanged;
            bsQPRatesList.CurrentChanged += BsQPRatesList_CurrentChanged;
            gvRatesList.BeforeLeaveRow += GvRatesList_BeforeLeaveRow;

            barSurchargesAdd.ItemClick += BarSurchargesAdd_ItemClick;
            barSurchargesDelete.ItemClick += BarSurchargesDelete_ItemClick;
            gvSurcharges.CellValueChanged += GvSurcharges_CellValueChanged;
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        private void UnRegisterEvent()
        {
            barRatesAdd.ItemClick -= barRatesAdd_ItemClick;
            barRatesRemove.ItemClick -= barRatesRemove_ItemClick;
            gvRatesList.CellValueChanged -= gvRatesList_CellValueChanged;
            bsQPRatesList.PositionChanged -= BsQPRatesList_PositionChanged;
            bsQPRatesList.CurrentChanged -= BsQPRatesList_CurrentChanged;
            gvRatesList.BeforeLeaveRow -= GvRatesList_BeforeLeaveRow;

            barSurchargesAdd.ItemClick -= BarSurchargesAdd_ItemClick;
            barSurchargesDelete.ItemClick -= BarSurchargesDelete_ItemClick;
            gvSurcharges.CellValueChanged -= GvSurcharges_CellValueChanged;
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        void InitControls()
        {
            gvRatesList.ShowGridViewRowNo();
            gvRatesList.ClickIndicatorHeader2SelectAll();
            SetLanguage();
            InitComboboxSource();
            if (IsView)
            {
                gvRatesList.OptionsBehavior.Editable = false;
                barTools.Visible = false;

                gvSurcharges.OptionsBehavior.Editable = false;
                barSurchargeTools.Visible = false;
            }
            else
            {
                SearchRegister();
            }
        }
        /// <summary>
        /// 初始化复选框
        /// </summary>
        void InitComboboxSource()
        {
            _ConfigureInfo = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
            _CurrencyList = ConfigureService.GetSolutionCurrencyList(_ConfigureInfo.SolutionID, true);

            #region Currency
            rilueCurrency.BestFitMode = BestFitMode.BestFit;

            rilueCurrency.DataSource = _CurrencyList;
            rilueCurrency.DisplayMember = "CurrencyName";
            rilueCurrency.ValueMember = "CurrencyID";
            rilueCurrency.TextEditStyle = TextEditStyles.DisableTextEditor;
            rilueCurrency.UseCtrlScroll = false;
            rilueCurrency.Columns.Add(new LookUpColumnInfo("CurrencyName", LocalData.IsEnglish ? "Currency" : "币种"));
            rilueCurrency.BestFit();
            colCurrencyName.BestFit();
            #endregion

            #region 每柜/每票
            List<ContainerList> ctntypes = TransportFoundationService.GetContainerList(string.Empty, true, 0);
            cmbType.BeginUpdate();
            cmbType.Items.Add(new ImageComboBoxItem("Bill", "Bill"));
            foreach (var item in ctntypes)
            {
                cmbType.Items.Add(new ImageComboBoxItem(item.Code, item.Code));
            }
            cmbType.EndUpdate(); 
            #endregion
        }
        /// <summary>
        /// 查询注册
        /// </summary>
        void SearchRegister()
        {
            #region Place Of Receipt
            DataFindClientService.RegisterGridColumnFinder(colPlaceOfReceiptName
                                             , CommonFinderConstants.LocationFinder
                                             , "PlaceOfReceiptID"
                                             , "PlaceOfReceiptName"
                                             , "ID"
                                             , "EName");
            #endregion

            #region pol

            DataFindClientService.RegisterGridColumnFinder(colPOLName
                                             , CommonFinderConstants.LocationFinder
                                             , "POLID"
                                             , "POLName"
                                             , "ID"
                                             , "EName");
            #endregion

            #region pod

            DataFindClientService.RegisterGridColumnFinder(colPODName
                                           , CommonFinderConstants.LocationFinder
                                           , "PODID"
                                           , "PODName"
                                           , "ID"
                                           , "EName");
            #endregion

            #region Place Of Delivery
            DataFindClientService.RegisterGridColumnFinder(colPlaceOfDeliveryName
                                           , CommonFinderConstants.LocationFinder
                                           , "PlaceOfDeliveryID"
                                           , "PlaceOfDeliveryName"
                                           , "ID"
                                           , "EName");
            #endregion

            #region Carrier
            List<CustomerList> carriers = CustomerService.GetCustomerListByList(string.Empty, string.Empty, string.Empty, string.Empty,
                                                             string.Empty, string.Empty, null, null, CustomerStateType.Valid,
                                                             CustomerType.Carrier, null, null, null, null, null, 0);

            foreach (var itemCarrier in carriers.Where(itemCarrier => !string.IsNullOrEmpty(itemCarrier.Code)).OrderBy(descItem=>descItem.Code))
            {
                cmbCarrier.Items.Add(itemCarrier.Code, CheckState.Unchecked, true);
            }
            #endregion

            #region ChargingCode
            chargingCodeFinder = DataFindClientService.RegisterGridColumnFinder(colChargeName
                                                 , CommonFinderConstants.SolutionChargingCodeFinder
                                                 , new string[] { "ChargeID", "ChargeName"}
                                                 , new string[] { "ChargingCodeID", "ChargingCodeName" }
                                                   , GetSolutionChargingCodeSearchCondition);

            #endregion
        }
        SearchConditionCollection GetSolutionChargingCodeSearchCondition()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("SolutionID", _ConfigureInfo.SolutionID, false);
            return conditions;
        }
        /// <summary>
        /// 设置附加费用数据源
        /// </summary>
        void SetSurchargeDataSource()
        {
            bsSurcharge.DataSource = Current.Surcharges;
        }
        /// <summary>
        /// 
        /// </summary>
        void EndEditByRates()
        {
            gcRatesList.EndUpdate();
            gvRatesList.CloseEditor();
            bsQPRatesList.EndEdit();
        }
        void EndEditBySurcharge()
        {
            gcSurcharges.EndUpdate();
            gvSurcharges.CloseEditor();
            bsSurcharge.EndEdit();
        }

        void SetSurcharge()
        {
            EndEditBySurcharge();
            if (bsSurcharge.DataSource != null)
            {
                Current.Surcharges = bsSurcharge.DataSource as List<QPSurcharge>;
                Current.IsDirty = true;
            }
        }
        #endregion
        
    }
}
