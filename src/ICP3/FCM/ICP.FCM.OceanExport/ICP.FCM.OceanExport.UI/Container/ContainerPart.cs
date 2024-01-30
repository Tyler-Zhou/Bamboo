using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.CompositeObjects;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.CompositeObjects;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Common.UI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.Common.ServiceInterface;
using ICP.Sys.ServiceInterface;

namespace ICP.FCM.OceanExport.UI.Container
{
    /// <summary>
    /// 提单箱列表界面
    /// </summary>
    [ToolboxItem(false)]
    [SmartPart]
    public partial class ContainerPart : BaseEditPart
    {
        #region 服务
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        /// <summary>
        /// 海出服务
        /// </summary>
        private IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }
        /// <summary>
        /// 用户服务
        /// </summary>
        private IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }
        /// <summary>
        /// 客户服务
        /// </summary>
        private ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }
        /// <summary>
        /// 地理服务
        /// </summary>
        private IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
            }
        }
        /// <summary>
        /// 客户端搜索器
        /// </summary>
        private IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        /// <summary>
        /// Excel服务
        /// </summary>
        public IExcelService ExcelService
        {
            get
            {
                return ServiceClient.GetClientService<IExcelService>();
            }
        }
        /// <summary>
        /// FCM通用服务
        /// </summary>
        private IFCMCommonService FCMCommonService
        {
            get
            {

                return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        /// <summary>
        /// 海出客户端服务
        /// </summary>
        private IClientOceanExportService ClientOceanExportService
        {
            get
            {
                return ServiceClient.GetClientService<IClientOceanExportService>();
            }
        }
        #endregion

        #region 成员变量
        /// <summary>
        /// 是否需要申请代理
        /// </summary>
        bool _isNeedRequestAgent = false;
        /// <summary>
        /// 是否需要保存前自动申请代理
        /// </summary>
        bool _isNeedAutoRequestAgent = false;
        private IDisposable placeOfDeliveryFinder;
        IDictionary<string, object> stateValues;
        List<ContainerList> _ctnTypes = null;
        private List<DataDictionaryList> measurementUnits = null;
        private List<DataDictionaryList> weightUnits = null;
        private List<DataDictionaryList> quantityUnits = null;

        /// <summary>
        /// 业务ID
        /// </summary>
        Guid OperationID { get; set; }

        /// <summary>
        /// 提单类型
        /// </summary>
        FCMBLType _BLSourceType { get; set; }

        OceanMBLInfo _CurrentMBL = null;

        /// <summary>
        /// 数据源 - 主提单
        /// </summary>
        public OceanMBLInfo DataSource_MBL
        {
            get
            {
                OceanMBLInfo mbl = _BSMBL.DataSource as OceanMBLInfo;
                return mbl ?? new OceanMBLInfo();
            }
            set
            {
                ChangeMBLChainEvent(true);
                _BSMBL.DataSource = value;
                _BSMBL.ResetBindings(false);
                ChangeMBLChainEvent(false);
            }
        }

        /// <summary>
        /// 数据源 - 箱
        /// </summary>
        public List<OceanContainerList> DataSource_Container
        {
            get
            {
                List<OceanContainerList> containers = _BSContainer.DataSource as List<OceanContainerList>;
                return containers ?? new List<OceanContainerList>();
            }
            set
            {
                List<OceanContainerList> OldSource = (_BSContainer.DataSource as List<OceanContainerList>) ?? new List<OceanContainerList>();
                //从旧数据源检索不在新数据源的数据
                List<OceanContainerList> newSource = OldSource.Where(oItem => !value.Any(fItem => fItem.No.Equals(oItem.No))).ToList();
                //从新数据源检索不在旧数据源的数据存入临时数据源
                newSource.AddRange(value.Where(oItem => !OldSource.Any(fItem => fItem.No.Equals(oItem.No))));
                //如果新数据源中的项在旧数据源存在，则使用旧数据源的ID
                foreach (OceanContainerList cItem in OldSource)
                {
                    OceanContainerList nItem = value.SingleOrDefault(fItem => fItem.No.Equals(cItem.No));
                    if (nItem == null) continue;
                    nItem.UpdateDate = nItem.IsNew ? cItem.UpdateDate : nItem.UpdateDate;
                    nItem.ID = cItem.ID;
                    newSource.Add(nItem);
                }
                _BSContainer.DataSource = newSource;
                _BSContainer.ResetBindings(false);
            }
        }

        /// <summary>
        /// 数据源 - 提单
        /// </summary>
        public List<OceanBLInfo> DataSource_BL
        {
            get
            {
                List<OceanBLInfo> bls = _BSBL.DataSource as List<OceanBLInfo>;
                return bls ?? new List<OceanBLInfo>();
            }
            set
            {
                List<OceanBLInfo> OldSource = (_BSBL.DataSource as List<OceanBLInfo>) ?? new List<OceanBLInfo>();
                //从旧数据源检索不在新数据源的数据
                List<OceanBLInfo> newSource = OldSource.Where(oItem => !value.Any(fItem => fItem.No.Equals(oItem.No))).ToList();
                //从新数据源检索不在旧数据源的数据存入临时数据源
                newSource.AddRange(value.Where(oItem => !OldSource.Any(fItem => fItem.No.Equals(oItem.No))));
                //如果新数据源中的项在旧数据源存在，则使用旧数据源的ID
                foreach (OceanBLInfo cItem in OldSource)
                {
                    OceanBLInfo nItem = value.SingleOrDefault(fItem => fItem.No.Equals(cItem.No));
                    if (nItem == null) continue;
                    nItem.UpdateDate = nItem.IsNew ? cItem.UpdateDate : nItem.UpdateDate;
                    nItem.ID = cItem.ID;
                    newSource.Add(nItem);
                }
                _BSBL.DataSource = newSource;
                _BSBL.ResetBindings(false);
            }
        }

        /// <summary>
        /// 数据源 - 箱货物
        /// </summary>
        public List<OceanContainerCargoList> DataSource_Cargo
        {
            get
            {
                List<OceanContainerCargoList> cargos = _BSCargo.DataSource as List<OceanContainerCargoList>;
                return cargos ?? new List<OceanContainerCargoList>();
            }
            set
            {
                List<OceanContainerCargoList> OldSource = (_BSCargo.DataSource as List<OceanContainerCargoList>) ?? new List<OceanContainerCargoList>();
                //从旧数据源检索不在新数据源的数据
                List<OceanContainerCargoList> newSource = OldSource.Where(oItem => !value.Any(fItem => 
                    fItem.BLNo.Equals(oItem.BLNo) 
                    && fItem.OceanContainerNo.Equals(oItem.OceanContainerNo)
                    && fItem.Commodity.Trim().Equals(oItem.Commodity.Trim())
                    && fItem.HSCode.Equals(oItem.HSCode)
                    )).ToList();
                //从新数据源检索不在旧数据源的数据存入临时数据源
                newSource.AddRange(value.Where(oItem => !OldSource.Any(fItem => 
                    fItem.BLNo.Equals(oItem.BLNo) 
                    && fItem.OceanContainerNo.Equals(oItem.OceanContainerNo)
                    && fItem.Commodity.Equals(oItem.Commodity)
                    && fItem.HSCode.Equals(oItem.HSCode)
                    )));
                //如果新数据源中的项在旧数据源存在，则使用旧数据源的ID
                foreach (OceanContainerCargoList cItem in OldSource)
                {
                    OceanContainerCargoList nItem = value.SingleOrDefault(fItem => 
                        fItem.BLNo.Equals(cItem.BLNo) 
                        && fItem.OceanContainerNo.Equals(cItem.OceanContainerNo)
                        && fItem.Commodity.Equals(cItem.Commodity)
                        && fItem.HSCode.Equals(cItem.HSCode)
                        );
                    if (nItem == null) continue;
                    nItem.UpdateDate = nItem.IsNew ? cItem.UpdateDate : nItem.UpdateDate;
                    nItem.ID = cItem.ID;
                    newSource.Add(nItem);
                }
                _BSCargo.DataSource = newSource;
                _BSCargo.ResetBindings(false);
            }
        }

        #region 当前行
        /// <summary>
        /// 当前行 - 主提单
        /// </summary>
        public OceanMBLInfo CurrentRow_MBL
        {
            get
            {
                if (_BSMBL.DataSource == null || _BSMBL.Current == null) return null;
                return _BSMBL.Current as OceanMBLInfo;
            }
        }

        /// <summary>
        /// 当前行 - 箱
        /// </summary>
        OceanContainerList CurrentRow_Container
        {
            get
            {
                if (_BSContainer.DataSource == null || _BSContainer.Current == null) return null;
                return _BSContainer.Current as OceanContainerList;
            }
        }

        /// <summary>
        /// 当前行 - 提单
        /// </summary>
        OceanBLInfo CurrentRow_BL
        {
            get
            {
                if (_BSBL.DataSource == null || _BSBL.Current == null) return null;
                return _BSBL.Current as OceanBLInfo;
            }
        }

        /// <summary>
        /// 当前行 - 箱货物
        /// </summary>
        OceanContainerCargoList CurrentRow_Cargo
        {
            get
            {
                if (_BSCargo.DataSource == null || _BSCargo.Current == null) return null;
                return _BSCargo.Current as OceanContainerCargoList;
            }
        }
        #endregion

        #region 选中行
        /// <summary>
        /// 选中行 - 箱
        /// </summary>
        List<OceanContainerList> SelectedItem_Container
        {
            get
            {
                int[] rowIndexs = _GVContainer.GetSelectedRows();
                if (rowIndexs.Length == 0) return null;
                return rowIndexs.Select(item => _GVContainer.GetRow(item)).OfType<OceanContainerList>().ToList();
            }
        }

        /// <summary>
        /// 选中行 - 提单
        /// </summary>
        List<OceanBLInfo> SelectedItem_BL
        {
            get
            {
                int[] rowIndexs = _GVBL.GetSelectedRows();
                if (rowIndexs.Length == 0) return null;
                return rowIndexs.Select(item => _GVBL.GetRow(item)).OfType<OceanBLInfo>().ToList();
            }
        }

        /// <summary>
        /// 选中行 - 箱货物
        /// </summary>
        List<OceanContainerCargoList> SelectedItem_Cargo
        {
            get
            {
                int[] rowIndexs = _GVCargo.GetSelectedRows();
                if (rowIndexs.Length == 0) return null;
                return rowIndexs.Select(item => _GVCargo.GetRow(item)).OfType<OceanContainerCargoList>().ToList();
            }
        }
        #endregion

        #region 数据是否变更
        /// <summary>
        /// 数据是否变更 - 箱
        /// </summary>
        bool IsChange_Container
        {
            get
            {
                _GVContainer.CloseEditor();
                return DataSource_Container.Any(item => item.IsDirty);
            }
        }
        /// <summary>
        /// 数据是否变更 - 提单
        /// </summary>
        bool IsChange_BL
        {
            get
            {
                _GVBL.CloseEditor();
                return DataSource_BL.Any(item => item.IsDirty);
            }
        }
        /// <summary>
        /// 数据是否变更 - 箱货物
        /// </summary>
        bool IsChange_Cargo
        {
            get
            {
                _GVCargo.CloseEditor();
                return DataSource_Cargo.Any(item => item.IsDirty);
            }
        }
        #endregion

        /// <summary>
        /// OceanContainerList
        /// </summary>
        public override object DataSource
        {
            get { return _BSCargo.DataSource; }
            set { BindingData(value); }
        }

        /// <summary>
        /// 返回List Guid
        /// </summary>
        public override event SavedHandler Saved;
        #endregion

        #region 初始化
        /// <summary>
        /// 
        /// </summary>
        public ContainerPart()
        {
            InitializeComponent();
            InitMessage();
            #region 关联控件事件
            Load += ContainerPart_Load;
            stxtShipper.OnFirstEnter += OnstxtShipperFirstTimeEnter;
            stxtShipper.OnOk += stxtShipper_OnOk;
            stxtConsignee.OnFirstEnter += OnstxtConsigneeFirstTimeEnter;
            stxtConsignee.OnOk += stxtConsignee_OnOk;
            stxtNotifyParty.OnFirstEnter += OnstxtNotifyPartyFirstTimeEnter;
            stxtNotifyParty.OnOk += stxtNotifyParty_OnOk;
            cmbPaymentTerm.SelectedIndexChanged += cmbPaymentTerm_SelectedIndexChanged;
            _BSContainer.PositionChanged += bsContainer_PositionChanged;
            _BSContainer.DataSourceChanged += _BSContainer_DataSourceChanged;
            _GVContainer.RowCellClick += _GVContainer_RowCellClick;
            _GVContainer.CellValueChanged += _GVContainer_CellValueChanged;
            _GVContainer.CustomDrawRowIndicator += GridView_CustomDrawRowIndicator;
            _GVBL.CustomDrawRowIndicator += GridView_CustomDrawRowIndicator;
            _GVCargo.CustomDrawRowIndicator += GridView_CustomDrawRowIndicator;

            _BSBL.DataSourceChanged += _BSBL_DataSourceChanged;
            _BSBL.CurrentChanged += _BSBL_CurrentChanged;

            btnImport.ItemClick += btnImport_ItemClick;
            btnSave.ItemClick += btnSave_ItemClick;
            btnClose.ItemClick += btnClose_ItemClick;

            btnAddBL.ItemClick += btnAddBL_ItemClick;
            btnDeleteBL.ItemClick += btnDeleteBL_ItemClick;

            btnAddContainer.ItemClick += barAddContainer_ItemClick;
            btnDeleteContainer.ItemClick += btnDeleteContainer_ItemClick;

            btnAddCargo.ItemClick += btnCargoAdd_ItemClick;
            btnDeleteCargo.ItemClick += btnCargoDelete_ItemClick;
            #endregion

            Disposed += delegate
            {
                Saved = null;
                Load -= ContainerPart_Load;
                _LWGCContainer.DataSource = null;

                _BSContainer.DataSource = null;
                _BSContainer.PositionChanged -= bsContainer_PositionChanged;
                _BSContainer.Dispose();
                _ctnTypes = null;

                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="values"></param>
        public override void Init(IDictionary<string, object> values)
        {
            stateValues = values;
            if (values == null) return;
        }

        bool isInit = false;
        private void InitControls()
        {
            if (isInit) return;
            //付费条款
            ICPCommUIHelper.SetCmbDataDictionary(cmbPaymentTerm, DataDictionaryType.PaymentTerm, DataBindType.EName, true);
            //签单类型
            ICPCommUIHelper.SetComboxByEnum<IssueType>(cmbIssueType, false, false, true);
            //签单人
            FillIssueBy(LocalData.UserInfo.DefaultCompanyID);
            //放单类型
            ICPCommUIHelper.SetComboxByEnum<FCMReleaseType>(cmbReleaseType, false, false, true);
            //国家
            if (_countryList == null) _countryList = GeographyService.GetCountryListByFCM(string.Empty, string.Empty, true, true, 0);
            //包装单位
            ICPCommUIHelper.SetCmbDataDictionary(cmbQuantityUnit, DataDictionaryType.QuantityUnit, DataBindType.EName);
            quantityUnits = ICPCommUIHelper.SetCmbDataDictionary(rcmbQuantityUnit, DataDictionaryType.QuantityUnit, DataBindType.EName);
            //重量单位
            ICPCommUIHelper.SetCmbDataDictionary(cmbWeightUnit, DataDictionaryType.WeightUnit, DataBindType.EName);
            weightUnits = ICPCommUIHelper.SetCmbDataDictionary(rcmbWeightUnit, DataDictionaryType.WeightUnit, DataBindType.EName);
            //体积单位
            ICPCommUIHelper.SetCmbDataDictionary(cmbMeasurementUnit, DataDictionaryType.MeasurementUnit, DataBindType.EName);
            measurementUnits = ICPCommUIHelper.SetCmbDataDictionary(rcmbMeasurementUnit, DataDictionaryType.MeasurementUnit, DataBindType.EName);
            //箱型
            _ctnTypes = ICPCommUIHelper.SetCmbContainerType(cmbType);
            SearchRegister();
            isInit = true;

        }

        #endregion

        #region 事件
        /// <summary>
        /// 缓存国家列表,只获取一次.现只用于客户弹出式描述框
        /// </summary>
        List<CountryList> _countryList = null;
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ContainerPart_Load(object sender, EventArgs e)
        {
            SetNullValuePrompt();
        }
        /// <summary>
        /// 
        /// </summary>
        private void OnstxtShipperFirstTimeEnter(object sender, EventArgs e)
        {
            if (_countryList == null) _countryList = GeographyService.GetCountryListByFCM(string.Empty, string.Empty, true, true, 0);
            //shipper
            CustomerForNewFinderBridge shipperBridge = new CustomerForNewFinderBridge(
            stxtShipper,
            _countryList,
            DataFindClientService,
            DataSource_MBL.ShipperDescription,
            ICPCommUIHelper,
            LocalData.IsEnglish);
            shipperBridge.Init();
        }
        /// <summary>
        /// 
        /// </summary>
        private void OnstxtConsigneeFirstTimeEnter(object sender, EventArgs e)
        {
            if (_countryList == null) _countryList = GeographyService.GetCountryListByFCM(string.Empty, string.Empty, true, true, 0);
            CustomerForNewFinderBridge consigneeBridge = new CustomerForNewFinderBridge(
            stxtConsignee,
            _countryList,
            DataFindClientService,
            DataSource_MBL.ConsigneeDescription,
            ICPCommUIHelper,
            LocalData.IsEnglish);
            consigneeBridge.Init();
        }
        /// <summary>
        /// 
        /// </summary>
        private void OnstxtNotifyPartyFirstTimeEnter(object sender, EventArgs e)
        {
            if (_countryList == null) _countryList = GeographyService.GetCountryListByFCM(string.Empty, string.Empty, true, true, 0);

            CustomerForNewFinderBridge notifyPartyBridge = new CustomerForNewFinderBridge(
             stxtNotifyParty,
             _countryList,
             DataFindClientService,
             DataSource_MBL.NotifyPartyDescription,
             ICPCommUIHelper,
             LocalData.IsEnglish);
            notifyPartyBridge.Init();
        }
        /// <summary>
        /// 
        /// </summary>
        private void stxtNotifyParty_OnOk(object sender, EventArgs e)
        {
            CustomerDescriptionForNew des = stxtNotifyParty.DataSource ?? new CustomerDescriptionForNew();
            DataSource_MBL.NotifyPartyDescription = des;
        }
        /// <summary>
        /// 
        /// </summary>
        private void stxtConsignee_OnOk(object sender, EventArgs e)
        {
            CustomerDescriptionForNew des = stxtConsignee.DataSource ?? new CustomerDescriptionForNew();
            DataSource_MBL.ConsigneeDescription = des;
        }
        /// <summary>
        /// 
        /// </summary>
        private void stxtShipper_OnOk(object sender, EventArgs e)
        {
            CustomerDescriptionForNew des = stxtShipper.DataSource ?? new CustomerDescriptionForNew();
            DataSource_MBL.ShipperDescription = des;
        }
        /// <summary>
        /// 付款方式
        /// </summary>
        void cmbPaymentTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbPaymentTerm.SelectedIndexChanged -= cmbPaymentTerm_SelectedIndexChanged;
                if (ArgumentHelper.GuidIsNullOrEmpty(CurrentRow_MBL.BookingPaymentTermID) == false &&
                    CurrentRow_MBL.BookingPaymentTermID != CurrentRow_MBL.PaymentTermID)
                {
                    if (MessageBoxService.ShowQuestion(
                        LocalData.IsEnglish ? "Un Done" : "现选择的MBL付款方式与订舱单中MBL付款方式不同,是否继续?",
                        LocalData.IsEnglish ? "Tip" : "提示",
                        MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        cmbPaymentTerm.EditValue = CurrentRow_MBL.PaymentTermID = CurrentRow_MBL.BookingPaymentTermID;
                    }
                }
                if (cmbPaymentTerm.Text == "CC" || cmbPaymentTerm.Text == "到付")
                    _CurrentMBL.FreightDescription = "FREIGHT COLLECT";
                else
                    _CurrentMBL.FreightDescription = "FREIGHT PREPAID";
                cmbPaymentTerm.SelectedIndexChanged += cmbPaymentTerm_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void _BSContainer_DataSourceChanged(object sender, EventArgs e)
        {
            try
            {
                FillContainerNos();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void bsContainer_PositionChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void _GVContainer_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        private void _GVContainer_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            switch (e.Column.Name)
            {
                case "colIsSOC":
                    CurrentRow_Container.IsSOC = !CurrentRow_Container.IsSOC;
                    break;
                case "colIsPartOf":
                    CurrentRow_Container.IsPartOf = !CurrentRow_Container.IsPartOf;
                    break;
                default:
                    break;
            }
            _BSContainer.ResetBindings(false);
        }
        /// <summary>
        /// 绘制行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void GridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString(CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// 
        /// </summary>
        private void _BSBL_DataSourceChanged(object sender, EventArgs e)
        {
            try
            {
                FillBLNos();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void _BSBL_CurrentChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        /// <summary>
        /// 导入
        /// </summary>
        private void btnImport_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    RestoreDirectory = true,
                    Multiselect = false,
                    CheckFileExists = true,
                    Title = "Choose File",
                    Filter = "Excel files (*.xls)|*.xls",
                    FilterIndex = 1
                };
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string filepath = ofd.FileName;
                    DataTable result = null;
                    string error = string.Empty;
                    ExcelService.ExcelFileToDataTable(filepath, out result, out error);
                    if (!string.IsNullOrEmpty(error))
                        throw new Exception(error);
                    if (result == null) return;
                    OceanMBLInfo mbl = new OceanMBLInfo { ID = Guid.Empty };
                    List<OceanBLInfo> dbls = new List<OceanBLInfo>();
                    List<OceanContainerList> containers = new List<OceanContainerList>();
                    List<OceanContainerCargoList> cargos = new List<OceanContainerCargoList>();
                    ResolveDataByDataTable(result, out mbl, out dbls, out containers, out cargos);
                    _CurrentMBL = DataSource_MBL;
                    if (mbl.No.IsNullOrEmpty())
                    {

                        MessageBoxService.ShowInfo("未解析到预配信息,文件可能已损坏或不支持" , LocalData.IsEnglish ? "Tip" : "提示" , MessageBoxButtons.YesNo);
                        return;
                    }
                    if (_CurrentMBL.No.IsNullOrEmpty())
                    {
                        _CurrentMBL.No = mbl.No;
                    }
                    else
                    {
                        if (!_CurrentMBL.No.Equals(mbl.No))
                        {
                            if(MessageBoxService.ShowInfo(
                                string.Format(LocalData.IsEnglish ? "Import MBL[{0}] is inconsistent with the current edit MBL[{1}], whether to continue" : "导入MBL[{0}]与当前编辑MBL[{1}]不一致,是否继续?", mbl.No,
                                    _CurrentMBL.No)
                                , LocalData.IsEnglish ? "Tip" : "提示"
                                , MessageBoxButtons.YesNo) != DialogResult.Yes) return;
                        }
                    }
                    FillMBLByImportContent(mbl, _CurrentMBL);
                    FillBLByMBL(_CurrentMBL, dbls);
                    FillCargoByMBL(_CurrentMBL, cargos);

                    DataSource_MBL = _CurrentMBL;
                    DataSource_Container = containers;
                    DataSource_BL = dbls;
                    DataSource_Cargo = cargos;
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void btnSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (Save())
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "201806050001"));
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
        private void barAddContainer_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                OceanContainerList newData = new OceanContainerList
                {
                    ID = Guid.Empty,
                    OceanBookingID = DataSource_MBL.OceanBookingID,
                    No = string.Empty,
                    VGMCrossWeight = 0m,
                    CreateByName = LocalData.UserInfo.LoginName,
                    ShippingOrderNo = DataSource_MBL.SONO,
                    UpdateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified),
                };

                if (CurrentRow_Container != null)
                {
                    newData.TypeID = CurrentRow_Container.TypeID;
                    newData.TypeName = CurrentRow_Container.TypeName;
                }
                else
                {
                    newData.TypeID = _ctnTypes[0].ID;
                    newData.TypeName = _ctnTypes[0].Code;
                }
                _BSContainer.Add(newData);
                _GVContainer.RefreshData();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void btnDeleteContainer_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (CurrentRow_Container == null) return;
                
                if (MessageBoxService.ShowInfo(NativeLanguageService.GetText(this, "ContainerDeleteSure"), LocalData.IsEnglish ? "Tip" : "提示",
                    MessageBoxButtons.YesNo) != DialogResult.Yes) return;
                List<OceanContainerCargoList> cargos = DataSource_Cargo;
                for (int cIndex = cargos.Count - 1; cIndex >= 0; cIndex--)
                {
                    if (cargos[cIndex].OceanContainerNo.Equals(CurrentRow_Container.No))
                        cargos.RemoveAt(cIndex);
                }
                DataSource_Cargo = cargos;
                Save();
                foreach (OceanContainerCargoList ocCargo in DataSource_Cargo)
                {
                    if(ocCargo.OceanContainerNo.Equals(CurrentRow_Container.No))
                    DataSource_Cargo.Remove(ocCargo);
                }

                if (CurrentRow_Container.ID.IsNullOrEmpty())
                {
                    _GVContainer.DeleteRow(_GVContainer.FocusedRowHandle);
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "201806050002"));
                }
                else
                {
                    try
                    {
                        OceanExportService.RemoveOceanContaierInfo(new[] { CurrentRow_Container.ID }, LocalData.UserInfo.LoginID, new[] { CurrentRow_Container.UpdateDate });
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "201806050002"));
                        _GVContainer.DeleteRow(_GVContainer.FocusedRowHandle);
                    }
                    catch (Exception)
                    {
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "201806050003"));
                        throw;
                    }
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
        private void btnAddBL_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                OceanBLInfo newData = new OceanBLInfo
                {
                    ID = Guid.Empty,
                    OceanBookingID = DataSource_MBL.OceanBookingID,
                    MBLID = DataSource_MBL.ID,
                    No = string.Empty,
                    Marks = string.Empty,
                    GoodsDescription = string.Empty,
                    CreateByName = LocalData.UserInfo.LoginName,
                    UpdateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified),
                };

                if (CurrentRow_BL != null)
                {
                    newData.QuantityUnitID = CurrentRow_BL.QuantityUnitID;
                    newData.QuantityUnitName = CurrentRow_BL.QuantityUnitName;
                    newData.WeightUnitID = CurrentRow_BL.WeightUnitID;
                    newData.WeightUnitName = CurrentRow_BL.WeightUnitName;
                    newData.MeasurementUnitID = CurrentRow_BL.MeasurementUnitID;
                    newData.MeasurementUnitName = CurrentRow_BL.MeasurementUnitName;
                }
                else
                {
                    newData.QuantityUnitID = DataSource_MBL.QuantityUnitID;
                    newData.QuantityUnitName = DataSource_MBL.QuantityUnitName;
                    newData.WeightUnitID = DataSource_MBL.WeightUnitID;
                    newData.WeightUnitName = DataSource_MBL.WeightUnitName;
                    newData.MeasurementUnitID = DataSource_MBL.MeasurementUnitID;
                    newData.MeasurementUnitName = DataSource_MBL.MeasurementUnitName;
                }
                _BSBL.Add(newData);
                _GVBL.RefreshData();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void btnDeleteBL_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {

                if (CurrentRow_BL == null) return;
                
                if (MessageBoxService.ShowInfo(NativeLanguageService.GetText(this, "BLDeleteSure"),
                    LocalData.IsEnglish ? "Tip" : "提示",
                    MessageBoxButtons.YesNo) != DialogResult.Yes) return;
                List<OceanContainerCargoList> cargos = DataSource_Cargo;
                //移除当前提单下的货物
                for (int blIndex = cargos.Count - 1; blIndex >= 0; blIndex--)
                {
                    if (cargos[blIndex].BLNo.Equals(CurrentRow_BL.No))
                        cargos.RemoveAt(blIndex);
                }
                DataSource_Cargo = cargos;
                _GVBL.DeleteRow(_GVBL.FocusedRowHandle);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "201806050002"));
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void btnCargoAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (CurrentRow_BL == null || CurrentRow_BL.ID.IsNullOrEmpty()) return;
                if (CurrentRow_Container == null || CurrentRow_Container.ID.IsNullOrEmpty()) return;

                OceanContainerCargoList newData = new OceanContainerCargoList
                {
                    BLID = CurrentRow_BL.ID,
                    OceanContainerID = CurrentRow_Container.ID,
                    Quantity = 0,
                    QuantityUnitID = CurrentRow_BL.QuantityUnitID,
                    Measurement = 0m,
                    MeasurementUnitID = CurrentRow_BL.MeasurementUnitID.Value,
                    Weight = 0m,
                    WeightUnitID = CurrentRow_BL.WeightUnitID.Value,
                };
                newData.UpdateDate = newData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                newData.CreateByID = newData.CreateByID = LocalData.UserInfo.LoginID;
                newData.CreateByName = LocalData.UserInfo.LoginName;

                _BSCargo.Add(newData);

                _GVCargo.RefreshData();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void btnCargoDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (CurrentRow_BL == null || CurrentRow_BL.ID.IsNullOrEmpty()) return;
                if (CurrentRow_Cargo == null) return;

                if (MessageBoxService.ShowInfo(NativeLanguageService.GetText(this, "BLDeleteSure"),
                    LocalData.IsEnglish ? "Tip" : "提示",
                    MessageBoxButtons.YesNo) != DialogResult.Yes) return;
                _GVCargo.DeleteRow(_GVCargo.FocusedRowHandle);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "201806050002"));
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void btnClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            var findForm = FindForm();
            if (findForm != null) findForm.Close();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 
        /// </summary>
        private void InitMessage()
        {
            RegisterMessage("201806050001", LocalData.IsEnglish ? "Save Successfully" : "保存成功");
            RegisterMessage("201806050002", LocalData.IsEnglish ? "Delete Successfully" : "删除成功");
            RegisterMessage("201806050003", LocalData.IsEnglish ? "Delete failed" : "删除失败");
            RegisterMessage("ContainerDeleteSure", LocalData.IsEnglish ? "Srue Delete Selected?" : "是否删除选中箱?");
            RegisterMessage("BLDeleteSure", LocalData.IsEnglish ? "Srue Delete Selected?" : "是否删除选中提单?");
            RegisterMessage("CargoDeleteSure", LocalData.IsEnglish ? "Srue Delete Selected?" : "是否删除选中货物信息?");
            RegisterMessage("IsChangeCargo", LocalData.IsEnglish ? "Please save container cargo data first?" : "请先保存箱货物数据!");
        }
        /// <summary>
        /// 填充签单人
        /// </summary>
        /// <param name="companyID"></param>
        private void FillIssueBy(Guid companyID)
        {
            if (mcmbIssueBy.DataSource != null) mcmbIssueBy.ClearItems();
            List<UserList> saless = UserService.GetUnderlingUserList(new [] { companyID }, null, new [] { "操作员" }, true);
            Dictionary<string, string> col = new Dictionary<string, string>
            {
                {LocalData.IsEnglish ? "EName" : "CName", "名称"},
                {"Code", "代码"}
            };
            mcmbIssueBy.InitSource(saless, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
        }
        /// <summary>
        /// 搜索器注册
        /// </summary>
        private void SearchRegister()
        {
            #region PlaceOfDelivery

            placeOfDeliveryFinder = DataFindClientService.Register(txtPlaceOfDeliveryCode, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                    delegate(object inputSource, object[] resultData)
                    {
                        txtPlaceOfDeliveryName.Text = DataSource_MBL.PlaceOfDeliveryName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                        txtPlaceOfDeliveryCode.Text = DataSource_MBL.PlaceOfDeliveryCode = resultData[1].ToString();
                        txtPlaceOfDeliveryCode.Tag = DataSource_MBL.PlaceOfDeliveryID = new Guid(resultData[0].ToString());

                        PlaceOfDeliveryChanged();

                    },
                    delegate
                    {
                        txtPlaceOfDeliveryCode.Tag = DataSource_MBL.PlaceOfDeliveryID = null;
                        txtPlaceOfDeliveryCode.Text = DataSource_MBL.PlaceOfDeliveryCode = string.Empty;
                        txtPlaceOfDeliveryName.Text = DataSource_MBL.PlaceOfDeliveryName = string.Empty;
                    },
                    ClientConstants.MainWorkspace);
            #endregion
        }
        /// <summary>
        /// 改变MBL联动事件：影响数据源复制
        /// </summary>
        /// <param name="isRemove"></param>
        private void ChangeMBLChainEvent(bool isRemove)
        {
            if (isRemove)
            {
                cmbPaymentTerm.SelectedIndexChanged -= cmbPaymentTerm_SelectedIndexChanged;
            }
            else
            {
                cmbPaymentTerm.SelectedIndexChanged += cmbPaymentTerm_SelectedIndexChanged;
                
            }
        }
        /// <summary>
        /// 如果交货地所在的国家不存在于公司配置客户对应的国家并且代理已存在，需要提示用户是否清空代理并重新申请代理
        /// </summary>
        void PlaceOfDeliveryChanged()
        {
            _isNeedRequestAgent = false;
            _isNeedAutoRequestAgent = false;

            //如果交货地所在的国家不存在于公司配置客户对应的国家并且代理已存在，需要提示用户是否清空代理并重新申请代理
            if (DataSource_MBL.IsRequestAgent == false
                && ArgumentHelper.GuidIsNullOrEmpty(DataSource_MBL.AgentID) == false)
            {
                try
                {
                    bool isExist = OceanExportService.IsPortCountryExistCompanyConfig(DataSource_MBL.PlaceOfDeliveryID.Value, DataSource_MBL.CompanyID);
                    if (isExist == false)
                    {
                        _isNeedRequestAgent = true;
                        if (MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "Current BL need request agent.is clear current BL\'s agent?"
                                                                  : "当前提单需要申请代理,是否清空代理?"
                           , LocalData.IsEnglish ? "Tip" : "提示"
                           , MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            //_isNeedRequestAgent = true;
                            _isNeedAutoRequestAgent = true;
                            DataSource_MBL.AgentDescription = new CustomerDescription();
                            DataSource_MBL.AgentID = Guid.Empty;
                            DataSource_MBL.AgentName = string.Empty;
                        }
                    }
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); }
            }
        }
        /// <summary>
        /// 获取单位
        /// </summary>
        /// <param name="units"></param>
        /// <param name="unitAmount"></param>
        /// <param name="pUnitName"></param>
        /// <returns></returns>
        DataDictionaryList GetUnitName(List<DataDictionaryList> units,decimal unitAmount, string pUnitName)
        {
            DataDictionaryList unit = null;
            try
            {
                if (unitAmount > 0 && (!pUnitName.IsNullOrEmpty()))
                {
                    unit = units.SingleOrDefault(fItem => fItem.EName.Equals(pUnitName));
                    if (unit == null)
                    {
                        //单位数量大于0且为复数(包含s)
                        if (unitAmount > 0 && pUnitName.EndsWith("S"))
                        {
                            pUnitName = pUnitName.Substring(0, pUnitName.Length - 1);
                            unit = units.SingleOrDefault(fItem => fItem.EName.Equals(pUnitName));
                        }
                    }
                }
            }
            catch
            {
                unit = null;
            }
            return unit;
        }
        /// <summary>
        /// 从DataTable解析数据
        /// </summary>
        /// <param name="result"></param>
        /// <param name="mbl"></param>
        /// <param name="dbls"></param>
        /// <param name="containers"></param>
        /// <param name="cargos"></param>
        void ResolveDataByDataTable(DataTable result, out OceanMBLInfo mbl, out List<OceanBLInfo> dbls, out List<OceanContainerList> containers, out List<OceanContainerCargoList> cargos)
        {
            mbl = new OceanMBLInfo { ID = Guid.Empty };
            dbls = new List<OceanBLInfo>();
            containers = new List<OceanContainerList>();
            cargos = new List<OceanContainerCargoList>();

            string configPath = "Config/SINO56.ini";
            if (result.Rows.Count > 0 && result.Columns.Count > 0 && ("" + result.Rows[0][0]).Contains("预配舱单3.0"))
            {
                configPath = "Config/XGEDI.ini";
            }
            #region 通过配置文件解析 Excel 中导出的 DataTable
            INIHelper iniConfig = new INIHelper(Path.Combine(LocalData.MainPath, configPath));
            DataView dvFilter = result.DefaultView;
            dvFilter.RowFilter = iniConfig.IniReadValue("DATAVIEW", "ROWFILTER");
            DataTable filterResult = dvFilter.ToTable();
            string beforeRowColumnValue = string.Empty;
            foreach (DataRow dataRow in filterResult.AsEnumerable())
            {
                string rowColumnValue = dataRow.Field<string>(0);
                if (rowColumnValue.IsNullOrEmpty() && !beforeRowColumnValue.IsNullOrEmpty())
                {
                    dataRow[0] = beforeRowColumnValue + dataRow.Field<string>(1);
                }
                if (!rowColumnValue.IsNullOrEmpty())
                    beforeRowColumnValue = rowColumnValue;
            }
            string[] sections;
            iniConfig.GetAllSectionNames(out sections);
            if (sections == null || sections.Length <= 0) return;
            string currentSection = string.Empty;
            

            foreach (DataRow item in filterResult.AsEnumerable())
            {
                #region 逐行解析
                string section = item.Field<string>(0);
                bool isDetail = false;
                if (sections.Contains(section))
                {
                    currentSection = section;
                    isDetail = Convert.ToBoolean(iniConfig.IniReadValue(currentSection, "ISDETAIL"));
                }
                if (isDetail) continue;
                if (currentSection.IsNullOrEmpty()) continue;

                string dataType = iniConfig.IniReadValue(currentSection, "DATATYPE");
                string[] keys;
                string[] values;
                iniConfig.GetAllKeyValues(currentSection, out keys, out values);
                switch (dataType)
                {
                    case "SHIPPER":
                        if (mbl.ShipperDescription == null)
                            mbl.ShipperDescription = new CustomerDescriptionForNew();
                        SetObjectPropertyValue(mbl.ShipperDescription, item, keys, values);
                        break;
                    case "CONSIGNEE":
                        if (mbl.ConsigneeDescription == null)
                            mbl.ConsigneeDescription = new CustomerDescriptionForNew();
                        SetObjectPropertyValue(mbl.ConsigneeDescription, item, keys, values);
                        break;
                    case "NOTIFYPARTY":
                        if (mbl.NotifyPartyDescription == null)
                            mbl.NotifyPartyDescription = new CustomerDescriptionForNew();
                        SetObjectPropertyValue(mbl.NotifyPartyDescription, item, keys, values);
                        break;
                    case "MBL":
                        #region MBL
                        SetObjectPropertyValue(mbl, item, keys, values);
                        #endregion
                        break;
                    case "DBL":
                        OceanBLInfo dbl = new OceanBLInfo();
                        SetObjectPropertyValue(dbl, item, keys, values);
                        dbls.Add(dbl);
                        break;
                    case "CONTAINER":
                        OceanContainerList container = new OceanContainerList();
                        SetObjectPropertyValue(container, item, keys, values);
                        containers.Add(container);
                        break;
                    case "VGM":
                        break;
                    case "CARGO":
                        OceanContainerCargoList cargo = new OceanContainerCargoList();
                        SetObjectPropertyValue(cargo, item, keys, values);
                        cargos.Add(cargo);
                        break;
                }

                if (!Convert.ToBoolean(iniConfig.IniReadValue(currentSection, "ISDETAIL")))
                {
                    currentSection = string.Empty;
                }
                #endregion
            }
            #endregion

            if (mbl.No.IsNullOrEmpty())
                throw new Exception("未解析到预配信息,文件可能已损坏或不支持");
            if (cargos == null || cargos.Count <= 0)
                throw new Exception("未解析到预配品名信息");
            var groupUnit = cargos.GroupBy(gItem => gItem.QuantityUnitName);
            if (groupUnit != null && groupUnit.Count() > 1)
            {
                throw new Exception("预配存在多个包装单位");
            }

            #region Fill MBL
            //船名航次
            mbl.VesselVoyage = string.Format("{0}/{1}", mbl.PreVesselVoyage, mbl.VesselVoyage);
            //交货地
            mbl.PlaceOfDeliveryName = mbl.PlaceOfDeliveryName;
            //名称字段临时存储企业代码类型和企业代码
            //发货人
            string[] stempcodes = mbl.ShipperName.Split('+');
            mbl.ShipperDescription.EnterpriseCodeType = stempcodes[0];
            mbl.ShipperDescription.EnterpriseCode = stempcodes.Length > 1 ? stempcodes[1] : "";
            string scc = mbl.ShipperDescription.CountryCode;
            CountryList sccountry = _countryList.SingleOrDefault(fItem => fItem.Code.Equals(scc));
            if (sccountry == null)
                throw new Exception(string.Format("发货人[{0}]国家代码[{1}]无效!", mbl.ShipperDescription.Name, mbl.ShipperDescription.CountryCode));
            mbl.ShipperDescription.CountryID = sccountry.ID;

            //收货人
            if (!mbl.ConsigneeName.IsNullOrEmpty())
            {
                string[] ctempcodes = mbl.ConsigneeName.Split('+');
                mbl.ConsigneeDescription.EnterpriseCodeType = ctempcodes[0];
                mbl.ConsigneeDescription.EnterpriseCode = ctempcodes.Length > 1 ? ctempcodes[1] : "";
                string ccc = mbl.ConsigneeDescription.CountryCode;
                CountryList ccountry = _countryList.SingleOrDefault(fItem => fItem.Code.Equals(ccc));
                if (ccountry == null)
                    throw new Exception(string.Format("收货人[{0}]国家代码[{1}]无效!", mbl.ConsigneeDescription.Name,
                        mbl.ConsigneeDescription.CountryCode));
                mbl.ConsigneeDescription.CountryID = ccountry.ID;
            }
            else
            {
                mbl.ConsigneeDescription.CountryID = Guid.Empty;
            }
            

            //通知人
            if (!mbl.NotifyPartyName.IsNullOrEmpty())
            {
                string[] ntempcodes = mbl.NotifyPartyName.Split('+');
                mbl.NotifyPartyDescription.EnterpriseCodeType = ntempcodes[0];
                mbl.NotifyPartyDescription.EnterpriseCode = ntempcodes.Length > 1 ? ntempcodes[1] : "";
                string ncc = mbl.NotifyPartyDescription.CountryCode;
                CountryList ncountry = _countryList.SingleOrDefault(fItem => fItem.Code.Equals(ncc));
                if (ncountry == null)
                    throw new Exception(string.Format("通知人[{0}]国家代码[{1}]无效!", mbl.NotifyPartyDescription.Name,
                        mbl.NotifyPartyDescription.CountryCode));
                mbl.NotifyPartyDescription.CountryID = ncountry.ID;
            }
            else
            {
                mbl.NotifyPartyDescription.CountryID = Guid.Empty;
            }
            

            List<CustomerInfo> shippers = CustomerService.GetCustomerInfoBySearch(mbl.ShipperDescription.Name, mbl.ShipperDescription.EnterpriseCode);
            if (shippers != null && shippers.Count > 0)
            {
                mbl.ShipperID = shippers[0].ID;
                mbl.ShipperName = shippers[0].EName;
                mbl.ShipperDescription.IsNew = false;
            }
            else
            {
                mbl.ShipperID = Guid.Empty;
                mbl.ShipperName = mbl.ShipperDescription.Name;
                mbl.ShipperDescription.IsNew = true;
            }
            List<CustomerInfo> consinees = CustomerService.GetCustomerInfoBySearch(mbl.ConsigneeDescription.Name, mbl.ConsigneeDescription.EnterpriseCode);
            if (consinees != null && consinees.Count > 0)
            {
                mbl.ConsigneeID = consinees[0].ID;
                mbl.ConsigneeName = consinees[0].EName;
                FillDescriptByDB(consinees[0], mbl.ConsigneeDescription);
                mbl.ConsigneeDescription.IsNew = false;
            }
            else
            {
                mbl.ConsigneeID = Guid.Empty;
                mbl.ConsigneeName = mbl.ConsigneeDescription.Name;
                mbl.ConsigneeDescription.IsNew = true;
            }
            List<CustomerInfo> notifyPartys = CustomerService.GetCustomerInfoBySearch(mbl.NotifyPartyDescription.Name, mbl.NotifyPartyDescription.EnterpriseCode);
            if (notifyPartys != null && notifyPartys.Count > 0)
            {
                mbl.NotifyPartyID = notifyPartys[0].ID;
                mbl.NotifyPartyName = notifyPartys[0].EName;
                FillDescriptByDB(notifyPartys[0], mbl.NotifyPartyDescription);
                mbl.NotifyPartyDescription.IsNew = false;
            }
            else
            {
                mbl.NotifyPartyID = Guid.Empty;
                mbl.NotifyPartyName = mbl.NotifyPartyDescription.Name;
                mbl.NotifyPartyDescription.IsNew = true;
            }
            DataDictionaryList ddq = null;
            string quName = mbl.QuantityUnitName;
            if (quName.IsNullOrEmpty())
            {
                quName = cargos[0].QuantityUnitName;
            }
            ddq = GetUnitName(quantityUnits, mbl.Quantity, quName);
            if (ddq == null)
                throw new Exception(string.Format("包装单位[{0}]无效!", quName));
            mbl.QuantityUnitID = ddq.ID;
            mbl.QuantityUnitName = ddq.EName;


            DataDictionaryList ddw = GetUnitName(weightUnits, mbl.Weight, "KGS");
            if (ddw == null)
                throw new Exception(string.Format("毛重单位[{0}]无效!", mbl.WeightUnitName));
            mbl.WeightUnitID = ddw.ID;
            mbl.WeightUnitName = ddw.EName;

            DataDictionaryList ddm = GetUnitName(measurementUnits, mbl.Measurement, "CBM");
            if (ddm == null)
                throw new Exception(string.Format("体积单位[{0}]无效!", mbl.WeightUnitName));
            mbl.MeasurementUnitID = ddm.ID;
            mbl.MeasurementUnitName = ddm.EName;
            #endregion

            #region Fill Container
            foreach (OceanContainerList cItem in containers)
            {
                cItem.OceanBookingID = OperationID;
                if (cItem.TypeName.Contains("*"))
                {
                    int charIndex = cItem.TypeName.IndexOf("*", StringComparison.Ordinal);
                    cItem.TypeName = cItem.TypeName.Substring(charIndex + 1, cItem.TypeName.Length - charIndex-1);
                }
                ContainerList c = _ctnTypes.SingleOrDefault(fItem => fItem.Code.Equals(cItem.TypeName));
                if (c == null)
                {
                    throw new Exception(string.Format("箱型[{0}]无效!", cItem.TypeName));
                }
                cItem.TypeID = c.ID;
            }
            #endregion

            #region Fill DBL
            //无预配提单信息从品名中汇总
            if (dbls.Count <= 0)
            {
                var groupDBL = cargos.GroupBy(gItem => new { gItem.BLNo, gItem.QuantityUnitID, gItem.QuantityUnitName });
                dbls.AddRange(groupDBL.Select(gItem => new OceanBLInfo
                {
                    No = gItem.Key.BLNo,
                    QuantityUnitID = gItem.Key.QuantityUnitID,
                    QuantityUnitName = gItem.Key.QuantityUnitName
                }));
            }
            foreach (OceanBLInfo bItem in dbls)
            {
                bItem.OceanBookingID = OperationID;
                bItem.MBLNo = mbl.No;
                bItem.QuantityUnitID = mbl.QuantityUnitID;
                bItem.QuantityUnitName = mbl.QuantityUnitName;
                bItem.WeightUnitID = mbl.WeightUnitID;
                bItem.WeightUnitName = mbl.WeightUnitName;
                bItem.MeasurementUnitID = mbl.MeasurementUnitID;
                bItem.MeasurementUnitName = mbl.MeasurementUnitName;
            }
            
            #endregion

            #region Fill Cargo
            foreach (OceanContainerCargoList caItem in cargos)
            {
                caItem.MBLNo = mbl.No;
                caItem.QuantityUnitID = mbl.QuantityUnitID;
                caItem.QuantityUnitName = mbl.QuantityUnitName;
                caItem.WeightUnitID = mbl.WeightUnitID;
                caItem.WeightUnitName = mbl.WeightUnitName;
                caItem.MeasurementUnitID = mbl.MeasurementUnitID;
                caItem.MeasurementUnitName = mbl.MeasurementUnitName;
            }
            #endregion
        }
        /// <summary>
        /// 将Excel导入内容填充到当前MBL
        /// </summary>
        /// <param name="sourceMBL"></param>
        /// <param name="targetMBL"></param>
        void FillMBLByImportContent(OceanMBLInfo sourceMBL, OceanMBLInfo targetMBL)
        {
            #region MBL 赋值

            targetMBL.No = sourceMBL.No;

            targetMBL.VesselVoyage = sourceMBL.VesselVoyage;
            targetMBL.PlaceOfDeliveryName = sourceMBL.PlaceOfDeliveryName;

            targetMBL.ShipperID = sourceMBL.ShipperID;
            targetMBL.ShipperName = sourceMBL.ShipperName;
            targetMBL.ShipperDescription = sourceMBL.ShipperDescription;

            targetMBL.ConsigneeID = sourceMBL.ConsigneeID;
            targetMBL.ConsigneeName = sourceMBL.ConsigneeName;
            targetMBL.ConsigneeDescription = sourceMBL.ConsigneeDescription;

            targetMBL.NotifyPartyID = sourceMBL.NotifyPartyID;
            targetMBL.NotifyPartyName = sourceMBL.NotifyPartyName;
            targetMBL.NotifyPartyDescription = sourceMBL.NotifyPartyDescription;

            targetMBL.QuantityUnitID = sourceMBL.QuantityUnitID;
            targetMBL.Quantity = sourceMBL.Quantity;
            targetMBL.QuantityUnitName = sourceMBL.QuantityUnitName;

            targetMBL.WeightUnitID = sourceMBL.WeightUnitID;
            targetMBL.Weight = sourceMBL.Weight;
            targetMBL.WeightUnitName = sourceMBL.WeightUnitName;

            targetMBL.MeasurementUnitID = sourceMBL.MeasurementUnitID;
            targetMBL.Measurement = sourceMBL.Measurement;
            targetMBL.MeasurementUnitName = sourceMBL.MeasurementUnitName;
            #endregion
        }
        /// <summary>
        /// 填充MBL信息到预配提单
        /// </summary>
        /// <param name="sourceMBL"></param>
        /// <param name="targetBLs"></param>
        void FillBLByMBL(OceanMBLInfo sourceMBL, IEnumerable<OceanBLInfo> targetBLs)
        {
            foreach (OceanBLInfo targetBL in targetBLs)
            {
                targetBL.MBLID = sourceMBL.ID;
            }
        }
        /// <summary>
        /// 填充MBL信息到预配货物信息
        /// </summary>
        /// <param name="sourceMBL"></param>
        /// <param name="targetCargos"></param>
        void FillCargoByMBL(OceanMBLInfo sourceMBL, IEnumerable<OceanContainerCargoList> targetCargos)
        {
            foreach (OceanContainerCargoList targetCargo in targetCargos)
            {
                targetCargo.MBLID = sourceMBL.ID;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        void FillDescriptByDB(CustomerInfo source, CustomerDescriptionForNew target)
        {
            target.Name = target.Name.IsNullOrEmpty() ? source.EName : target.Name;
            target.Address = target.Address.IsNullOrEmpty() ? source.EAddress : target.Address;
            target.CountryID = target.CountryID.Equals(Guid.Empty) ? source.CountryID : target.CountryID;
            target.CountryCode = target.CountryCode.IsNullOrEmpty() ? source.CountryEName : target.CountryCode;
            target.Tel = target.Tel.IsNullOrEmpty() ? source.Tel1 : target.Tel;
            target.Fax = target.Fax.IsNullOrEmpty() ? source.Fax : target.Fax;
        }
        /// <summary>
        /// 设置一些控件的提示信息
        /// </summary>
        void SetNullValuePrompt()
        {
            OEUtility.SetCustomerTextEditNullValuePrompt(new List<TextEdit>
            {
                stxtShipper,
                stxtConsignee,
                stxtNotifyParty ,
            });
            OEUtility.SetPortTextEditNullValuePrompt(new List<TextEdit>
            {
                txtPlaceOfDeliveryCode,
            });

            string tip = LocalData.IsEnglish ? "Un Done" :
                        "此栏目链接于订舱单,保存时修改的内容将会更新到订舱单中.";
            txtPlaceOfDeliveryCode.ToolTip = stxtVoyage.ToolTip = tip;
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="data"></param>
        void BindingData(object data)
        {
            InitControls();
            OperationID = (Guid)stateValues["OperationID"];
            _BLSourceType = (FCMBLType)stateValues["FCMBLType"];
            DataSource_Container = OceanExportService.GetOceanContainerList(OperationID);
            if (!stateValues.ContainsKey("MBLID"))
            {
                _CurrentMBL = ClientOceanExportService.BuildMBLByBookingInfo(OperationID);
                DataSource_MBL = _CurrentMBL;
            }
            else
            {
                Guid _OceanMBLID = (Guid)stateValues["MBLID"];
                _CurrentMBL = OceanExportService.GetOceanMBLInfo(_OceanMBLID);
                DataSource_MBL = _CurrentMBL;
                DataSource_BL = OceanExportService.GetBLInfoListByOperationID(OperationID, _OceanMBLID, _BLSourceType);
                DataSource_Cargo = OceanExportService.GetOceanContainerCargoList(_OceanMBLID, null, _BLSourceType);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        void EndEdit_MBL()
        {
            Validate();
            _BSMBL.EndEdit();
        }
        /// <summary>
        /// 
        /// </summary>
        void EndEdit_Contaienr()
        {
            Validate();
            _BSContainer.EndEdit();
        }
        /// <summary>
        /// 
        /// </summary>
        void EndEdit_BL()
        {
            Validate();
            _BSBL.EndEdit();
        }
        /// <summary>
        /// 
        /// </summary>
        public override void EndEdit()
        {
            Validate();
            _BSCargo.EndEdit();
        }
        /// <summary>
        /// 设置对象属性值
        /// </summary>
        /// <param name="tObj">待设置对象</param>
        /// <param name="dataRow">数据行</param>
        /// <param name="keys">键集合-属性</param>
        /// <param name="values">键值集合-列名</param>
        private void SetObjectPropertyValue<T>(T tObj, DataRow dataRow, string[] keys, string[] values)
        {
            for (int keyIndex = 0; keyIndex < keys.Length; keyIndex++)
            {
                if (dataRow.Table.Columns.Contains(values[keyIndex]))
                    tObj.SetPropertyValue(keys[keyIndex], dataRow.Field<string>(values[keyIndex]));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void FillBLNos()
        {
            ricbxBLs.Items.Clear();
            foreach (OceanBLInfo info in DataSource_BL)
            {
                ImageComboBoxItem icbi = new ImageComboBoxItem { Description = info.No, Value = info.No };
                ricbxBLs.Items.Add(icbi);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void FillContainerNos()
        {
            ricbxContainers.Items.Clear();
            foreach (OceanContainerList info in DataSource_Container)
            {
                ImageComboBoxItem icbi = new ImageComboBoxItem { Description = info.No, Value = info.No };
                ricbxContainers.Items.Add(icbi);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool Save()
        {
            if (!AutoRequestAgent())
            {
                return false;
            }
            _CurrentMBL = DataSource_MBL;
            if (_CurrentMBL.ShipperDescription.IsNew)
            {
                CustomerInfoSaveRequest ciSaveRequest = _CurrentMBL.ShipperDescription.ConvertToSaveRequest();
                ciSaveRequest.savebyid = LocalData.UserInfo.LoginID;
                SingleResultData ciResultData = CustomerService.SaveCustomerInfo(ciSaveRequest);
                if (ciResultData == null)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), "发货人保存失败");
                    return false;
                }
                _CurrentMBL.ShipperID = ciResultData.ID;
                _CurrentMBL.ShipperDescription.IsNew = false;
            }
            if (_CurrentMBL.ConsigneeDescription.IsNew)
            {
                CustomerInfoSaveRequest ciSaveRequest = _CurrentMBL.ConsigneeDescription.ConvertToSaveRequest();
                ciSaveRequest.savebyid = LocalData.UserInfo.LoginID;
                SingleResultData ciResultData = CustomerService.SaveCustomerInfo(ciSaveRequest);
                if (ciResultData == null)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), "收货人保存失败");
                    return false;
                }
                _CurrentMBL.ConsigneeID = ciResultData.ID;
                _CurrentMBL.ConsigneeDescription.IsNew = false;
            }
            if (_CurrentMBL.NotifyPartyDescription.IsNew)
            {
                if (_CurrentMBL.NotifyPartyDescription.Name.Equals(_CurrentMBL.ConsigneeDescription.Name))
                {
                    _CurrentMBL.NotifyPartyDescription = _CurrentMBL.ConsigneeDescription;
                    _CurrentMBL.NotifyPartyID = _CurrentMBL.ConsigneeID;
                    _CurrentMBL.NotifyPartyName = _CurrentMBL.ConsigneeName;
                }
                else
                {
                    CustomerInfoSaveRequest ciSaveRequest = _CurrentMBL.NotifyPartyDescription.ConvertToSaveRequest();
                    ciSaveRequest.savebyid = LocalData.UserInfo.LoginID;
                    SingleResultData ciResultData = CustomerService.SaveCustomerInfo(ciSaveRequest);
                    if (ciResultData == null)
                    {
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), "收货人保存失败");
                        return false;
                    }
                    _CurrentMBL.NotifyPartyID = ciResultData.ID;
                }
                _CurrentMBL.NotifyPartyDescription.IsNew = false;
            }
            DataSource_MBL = _CurrentMBL;

            EndEdit_MBL();
            SaveMBLInfoParameter MBLParameter = DataSource_MBL.ConvertToParameter(false);
            EndEdit_Contaienr();
            ContainerSaveRequest saveData_containers = BuildContainersData();
             EndEdit_BL();
            BLSaveRequest saveData_bls = BuildBLData();
             EndEdit();
            CargoSaveRequest saveData_cargos = BuildCargoData();
            SingleResult result =OceanExportService.SaveOceanDeclarationContainerWithTrans(MBLParameter, saveData_containers, saveData_bls,
                saveData_cargos);
            SingleResult blResult = result.GetValue<SingleResult>("BLResult");
            Guid MBLID = blResult.GetValue<Guid>("ID");
            _CurrentMBL = OceanExportService.GetOceanMBLInfo(MBLID);
            DataSource_MBL = _CurrentMBL;
            DataSource_Container = OceanExportService.GetOceanContainerList(OperationID);
            DataSource_BL = OceanExportService.GetBLInfoListByOperationID(OperationID, MBLID, _BLSourceType);
            DataSource_Cargo = OceanExportService.GetOceanContainerCargoList(MBLID, null, _BLSourceType);
            return true;
        }

        /// <summary>
        /// 自动申请代理
        /// </summary>
        private bool AutoRequestAgent()
        {
            if (_isNeedRequestAgent)
            {
                if (_isNeedAutoRequestAgent)
                {
                    SingleResult result = FCMCommonService.RequestOceanAgent(DataSource_MBL.OceanBookingID
                                                                            , OperationType.OceanExport
                                                                            , LocalData.UserInfo.LoginID
                                                                            , DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified)
                                                                            , AgentType.Normal
                                                                            , string.Empty, null);
                    _isNeedRequestAgent = false;
                    DataSource_MBL.IsRequestAgent = true;
                    return true;
                }
                else
                {
                    bool srcc = ClientOceanExportService.ReplyAgent(DataSource_MBL.OceanBookingID, null, null);
                    if (srcc)
                    {
                        bool isDirty = DataSource_MBL.IsDirty;
                        _isNeedRequestAgent = false;
                        DataSource_MBL.IsRequestAgent = true;
                        DataSource_MBL.IsDirty = isDirty;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        /// <summary>
        /// 构建箱保存数据对象
        /// </summary>
        /// <returns></returns>
        private ContainerSaveRequest BuildContainersData()
        {
            List<OceanContainerList> allList = DataSource_Container;
            if (allList.Count <= 0)
            {
                return null;
            }

            #region 收集箱列表数据

            List<Guid?> containerIDs = new List<Guid?>();
            List<Guid> containerTypeIDs = new List<Guid>();
            List<string> containerNos = new List<string>()
                , containerCTNOpers = new List<string>()
                , containerShippingOrderNos = new List<string>()
                , containerSealNos = new List<string>()
                , containerVGMMethod = new List<string>()
                ;
            List<decimal> containerVGMCrossWeight = new List<decimal>();
            List<bool> containerIsSOCs = new List<bool>(), containerIsPartOfs = new List<bool>();
            List<DateTime?> containerUpdateDates = new List<DateTime?>();

            foreach (var item in allList)
            {
                containerIDs.Add(item.ID);
                containerNos.Add(item.No);
                containerTypeIDs.Add(item.TypeID);
                containerSealNos.Add(item.SealNo);
                containerCTNOpers.Add(item.CTNOper);
                containerShippingOrderNos.Add(item.ShippingOrderNo);
                containerVGMCrossWeight.Add(item.VGMCrossWeight);
                containerVGMMethod.Add(item.VGMMethod);
                containerIsSOCs.Add(item.IsSOC);
                containerIsPartOfs.Add(item.IsPartOf);
                containerUpdateDates.Add(item.UpdateDate);
            }

            #endregion

            ContainerSaveRequest saveRequest = new ContainerSaveRequest
            {
                oceanBookingID = DataSource_MBL.OceanBookingID,
                containerIDs = containerIDs.ToArray(),
                containerNos = containerNos.ToArray(),
                containerTypeIDs = containerTypeIDs.ToArray(),
                containerSealNos = containerSealNos.ToArray(),
                containerCTNOpers = containerCTNOpers.ToArray(),
                containerShippingOrderNos = containerShippingOrderNos.ToArray(),
                containerVGMCrossWeights = containerVGMCrossWeight.ToArray(),
                containerVGMMethods = containerVGMMethod.ToArray(),
                containerIsSOCs = containerIsSOCs.ToArray(),
                containerIsPartOfs = containerIsPartOfs.ToArray(),
                saveByID = LocalData.UserInfo.LoginID,
                containerUpdateDates = containerUpdateDates.ToArray(),
                isEnglish = LocalData.IsEnglish
            };

            return saveRequest;
        }
        /// <summary>
        /// 构建箱保存数据对象
        /// </summary>
        /// <returns></returns>
        private BLSaveRequest BuildBLData()
        {
            List<OceanBLInfo> allList = DataSource_BL;
            if (allList.Count <= 0)
            {
                return null;
            }

            #region 收集箱列表数据

            List<Guid?> BLIDs = new List<Guid?>()
                , QuantityUnitIDs = new List<Guid?>()
                , WeightUnitIDs = new List<Guid?>()
                , MeasurementUnitIDs = new List<Guid?>()
                ;
            List<string> BLNos = new List<string>()
                , Markss = new List<string>()
                , GoodsDescriptions = new List<string>()
                ;
            List<DateTime?> containerUpdateDates = new List<DateTime?>();

            foreach (var item in allList)
            {
                BLIDs.Add(item.ID);
                BLNos.Add(item.No);
                Markss.Add(item.Marks);
                GoodsDescriptions.Add(item.GoodsDescription);
                QuantityUnitIDs.Add(item.QuantityUnitID);
                WeightUnitIDs.Add(item.WeightUnitID);
                MeasurementUnitIDs.Add(item.MeasurementUnitID);
                containerUpdateDates.Add(item.UpdateDate);
            }

            #endregion

            BLSaveRequest saveRequest = new BLSaveRequest
            {
                oceanBookingID = DataSource_MBL.OceanBookingID,
                MBLID = DataSource_MBL.ID,
                BLIDs = BLIDs.ToArray(),
                BLNos = BLNos.ToArray(),
                Markss = Markss.ToArray(),
                GoodsDescriptions = GoodsDescriptions.ToArray(),
                QuantityUnitIDs = QuantityUnitIDs.ToArray(),
                WeightUnitIDs = WeightUnitIDs.ToArray(),
                MeasurementUnitIDs = MeasurementUnitIDs.ToArray(),
                saveByID = LocalData.UserInfo.LoginID,
                containerUpdateDates = containerUpdateDates.ToArray(),
            };
            return saveRequest;
        }
        /// <summary>
        /// 构建箱保存数据对象
        /// </summary>
        /// <returns></returns>
        private CargoSaveRequest BuildCargoData()
        {
            List<OceanContainerCargoList> allList = DataSource_Cargo;

            if (allList.Count <= 0)
            {
                return null;
            }
            #region 收集数据

            List<Guid> ContainerIDs = new List<Guid>()
                , BLIDs = new List<Guid>()
                ;
            List<Guid?> IDs = new List<Guid?>();
            List<string> BLNos=new List<string>()
                , ContainerNos=new List<string>()
                , cargoMarks = new List<string>()
                , cargoComodities = new List<string>()
                , cargoHSCodes = new List<string>();
            List<int> cargoQuantities = new List<int>();
            List<decimal> cargoWeights = new List<decimal>()
                , cargoMeasurements = new List<decimal>();

            List<DateTime?> cargoUpdateDates = new List<DateTime?>();
            foreach (OceanContainerCargoList item in allList)
            {
                IDs.Add(item.ID);
                BLIDs.Add(item.BLID);
                BLNos.Add(item.BLNo);
                ContainerIDs.Add(item.OceanContainerID);
                ContainerNos.Add(item.OceanContainerNo);
                cargoMarks.Add(item.Marks);
                cargoComodities.Add(item.Commodity);
                cargoHSCodes.Add(item.HSCode);
                cargoQuantities.Add(item.Quantity);
                cargoWeights.Add(item.Weight);
                cargoMeasurements.Add(item.Measurement);
                cargoUpdateDates.Add(item.UpdateDate);
            }

            #endregion

            CargoSaveRequest saveRequest = new CargoSaveRequest
            {
                oceanBookingID = DataSource_MBL.OceanBookingID,
                MBLID = DataSource_MBL.ID,
                hblids = BLIDs.ToArray(),
                hblnos=BLNos.ToArray(),
                containerids = ContainerIDs.ToArray(),
                containernos=ContainerNos.ToArray(),
                ids = IDs.ToArray(),
                marks = cargoMarks.ToArray(),
                commodities = cargoComodities.ToArray(),
                hscodes = cargoHSCodes.ToArray(),
                quantities = cargoQuantities.ToArray(),
                weights = cargoWeights.ToArray(),
                measurements = cargoMeasurements.ToArray(),
                saveByID = LocalData.UserInfo.LoginID,
                updateDates = cargoUpdateDates.ToArray(),
            };

            return saveRequest;
        }

        #endregion
    }
}
