using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.CompositeObjects;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Common.UI;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace ICP.FCM.OceanExport.UI.Container
{
    /// <summary>
    /// 提单箱列表界面
    /// </summary>
    [ToolboxItem(false)]
    [SmartPart]
    public partial class ContainerEditPart : BaseEditPart
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

        private IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }

        #endregion

        #region 成员变量
        IDictionary<string, object> stateValues;
        List<ContainerList> _ctnTypes = null;

        /// <summary>
        /// 提单
        /// </summary>
        OceanMBLInfo _OceanMBLInfo { get; set; }
        /// <summary>
        /// 提单类型
        /// </summary>
        FCMBLType _BLSourceType { get; set; }

        bool _ReadOnly = false;
        /// <summary>
        /// 
        /// </summary>
        public new bool ReadOnly { get { return _ReadOnly; } set { _ReadOnly = value; } }

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
                _BSContainer.DataSource = value;
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
                _BSBL.DataSource = value;
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
                List<OceanContainerCargoList>  cargos = _BSCargo.DataSource as List<OceanContainerCargoList>;
                return cargos ?? new List<OceanContainerCargoList>();
            }
            set
            {
                _BSCargo.DataSource = value; 
                _BSCargo.ResetBindings(false);
            }
        }

        #region 当前行
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
        public ContainerEditPart()
        {
            InitializeComponent();
            InitMessage();
            #region 关联控件事件
            _BSContainer.PositionChanged += bsContainer_PositionChanged;
            _BSContainer.DataSourceChanged += _BSContainer_DataSourceChanged;
            _GVContainer.RowCellClick += _GVContainer_RowCellClick;
            _GVContainer.CellValueChanged += _GVContainer_CellValueChanged;

            _BSBL.DataSourceChanged += _BSBL_DataSourceChanged;
            _BSBL.CurrentChanged += _BSBL_CurrentChanged;

            btnSave.ItemClick += btnSave_ItemClick;
            btnClose.ItemClick += btnClose_ItemClick;

            btnAddBL.ItemClick += btnAddBL_ItemClick;
            btnDeleteBL.ItemClick += btnDeleteBL_ItemClick;
            btnSaveBL.ItemClick += btnSaveBL_ItemClick;

            btnAddContainer.ItemClick += barAddContainer_ItemClick;
            btnDeleteContainer.ItemClick += btnDeleteContainer_ItemClick;
            btnSaveContainer.ItemClick += btnSaveContainer_ItemClick;

            btnAddCargo.ItemClick += btnCargoAdd_ItemClick;
            btnDeleteCargo.ItemClick += btnCargoDelete_ItemClick;
            btnSaveCargo.ItemClick += btnSaveCargo_ItemClick;
            #endregion

            Disposed += delegate
            {
                Saved = null;
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
            //包装单位
            ICPCommUIHelper.SetCmbDataDictionary(rcmbQuantityUnit, DataDictionaryType.QuantityUnit, DataBindType.EName);
            
            //重量单位
            ICPCommUIHelper.SetCmbDataDictionary(rcmbWeightUnit, DataDictionaryType.WeightUnit, DataBindType.EName);
            //体积单位
            ICPCommUIHelper.SetCmbDataDictionary(rcmbMeasurementUnit, DataDictionaryType.MeasurementUnit, DataBindType.EName);
            //箱型
            _ctnTypes = ICPCommUIHelper.SetCmbContainerType(cmbType);

            if (_ReadOnly)
            {
                btnDeleteContainer.Enabled = false;
                colNo.OptionsColumn.AllowEdit = false;
                colSealNo.OptionsColumn.AllowEdit = false;
                colType.OptionsColumn.AllowEdit = false;
                colCargoQuantity.OptionsColumn.AllowEdit = false;
                colCargoMeasurement.OptionsColumn.AllowEdit = false;
                colCargoWeight.OptionsColumn.AllowEdit = false;
                colIsSOC.OptionsColumn.AllowEdit = false;
                colIsSOC.OptionsColumn.AllowFocus = false;
                colIsPartOf.OptionsColumn.AllowEdit = false;
                colIsPartOf.OptionsColumn.AllowFocus = false;
                colShippingOrderNo.OptionsColumn.AllowEdit = false;
            }

            isInit = true;
        }

        #endregion

        #region 事件
        void _BSContainer_DataSourceChanged(object sender, EventArgs e)
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

        private void _GVContainer_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            //if (e.RowHandle < 0) return;

            //if (e.Column == colCargoRelation)
            //{
            //    OceanContainerList currentRow = CurrentRow;
            //    if (currentRow.Relation == false)
            //    {
            //        //currentRow.BLID = Guid.Empty;
            //        currentRow.Quantity = 0;
            //        currentRow.Weight = currentRow.Measurement = 0;
            //        //currentRow.Marks = currentRow.Commodity = string.Empty;
            //    }
            //    else
            //        currentRow.BLID = MBLID == Guid.Empty ? HBLID : MBLID;
            //}
            //else if (e.Column == colType)
            //{
            //    isChargeCtn = true;
            //}
            //else if (e.Column == colCargoMeasurement || e.Column == colCargoQuantity || e.Column == colCargoWeight
            //         || e.Column == colCargoCommodity || e.Column == colCargoMarks)
            //{
            //    OceanContainerList currentRow = CurrentRow;
            //    if (currentRow.Relation == true) return;

            //    if (currentRow.Quantity != 0 || currentRow.Weight != 0 || currentRow.Measurement != 0
            //        || string.IsNullOrEmpty(currentRow.Commodity) == false || string.IsNullOrEmpty(currentRow.Marks) == false)
            //    {
            //        currentRow.Relation = true;
            //    }
            //}
            //bsContainer.ResetCurrentItem();
        }

        private void _GVContainer_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            //switch (e.Column.Name)
            //{
            //    case "colRelation":
            //        if (_ReadOnly && LocalData.UserInfo.DefaultCompanyID != new Guid("a62a9f8e-e69c-4e6e-ad85-e75aed3c6cf9"))
            //        {
            //            e.Handled = false;
            //        }
            //        else
            //        {
            //            CurrentRow.Relation = !CurrentRow.Relation;
            //        }
            //        break;
            //    case "colIsSOC":
            //        if (_ReadOnly && LocalData.UserInfo.DefaultCompanyID != new Guid("a62a9f8e-e69c-4e6e-ad85-e75aed3c6cf9"))
            //        {
            //            e.Handled = false;
            //        }
            //        else
            //        {
            //            CurrentRow.IsSOC = !CurrentRow.IsSOC;
            //        }
            //        break;
            //    case "colIsPartOf":
            //        if (_ReadOnly && LocalData.UserInfo.DefaultCompanyID != new Guid("a62a9f8e-e69c-4e6e-ad85-e75aed3c6cf9"))
            //        {
            //            e.Handled = false;
            //        }
            //        else
            //        {
            //            CurrentRow.IsPartOf = !CurrentRow.IsPartOf;
            //        }
            //        break;
            //    default:
            //        break;
            //}
            //_BSContainer.ResetBindings(false);
        }

        void _BSBL_DataSourceChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        
        void _BSBL_CurrentChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsChange_Cargo)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "IsChangeCargo"));
                    return;
                }
                if (CurrentRow_BL == null) return;
                DataSource_Cargo = OceanExportService.GetOceanContainerCargoList(_OceanMBLInfo.ID,CurrentRow_BL.ID, _BLSourceType);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }

        private void btnSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }

        private void btnClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            var findForm = FindForm();
            if (findForm != null) findForm.Close();
        }

        private void barAddContainer_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                OceanContainerList newData = new OceanContainerList
                {
                    ID = Guid.Empty,
                    OceanBookingID = _OceanMBLInfo.OceanBookingID,
                    No = string.Empty,
                    VGMCrossWeight = 0m,
                    CreateByName = LocalData.UserInfo.LoginName,
                    ShippingOrderNo = _OceanMBLInfo.SONO,
                    UpdateDate =  DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified),
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

        private void btnDeleteContainer_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (_BSContainer.Current == null) return;
               
                if (XtraMessageBox.Show(NativeLanguageService.GetText(this, "ContainerDeleteSure"), LocalData.IsEnglish ? "Tip" : "提示",
                    MessageBoxButtons.YesNo) != DialogResult.Yes) return;
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

        private void btnSaveContainer_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (!IsChange_Container) return;
                if (!Save_Container()) return;
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                DataSource_Container = OceanExportService.GetOceanContainerList(_OceanMBLInfo.OceanBookingID);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }

        private void btnAddBL_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                OceanBLInfo newData = new OceanBLInfo
                {
                    ID = Guid.Empty,
                    OceanBookingID = _OceanMBLInfo.OceanBookingID,
                    MBLID = _OceanMBLInfo.ID,
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
                    newData.QuantityUnitID = _OceanMBLInfo.QuantityUnitID;
                    newData.QuantityUnitName = _OceanMBLInfo.QuantityUnitName;
                    newData.WeightUnitID = _OceanMBLInfo.WeightUnitID;
                    newData.WeightUnitName = _OceanMBLInfo.WeightUnitName;
                    newData.MeasurementUnitID = _OceanMBLInfo.MeasurementUnitID;
                    newData.MeasurementUnitName = _OceanMBLInfo.MeasurementUnitName;
                }
                _BSBL.Add(newData);
                _GVBL.RefreshData();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }

        private void btnDeleteBL_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {

                if (CurrentRow_BL == null) return;

                if (XtraMessageBox.Show(NativeLanguageService.GetText(this, "BLDeleteSure"),
                    LocalData.IsEnglish ? "Tip" : "提示",
                    MessageBoxButtons.YesNo) != DialogResult.Yes) return;
                if (CurrentRow_BL.ID.IsNullOrEmpty())
                {
                    _GVBL.DeleteRow(_GVBL.FocusedRowHandle);
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "201806050002"));
                }
                else
                {
                    try
                    {
                        OceanExportService.RemoveDeclarationOceanBLInfo(new[] { CurrentRow_BL.ID }, LocalData.UserInfo.LoginID, new[] { CurrentRow_BL.UpdateDate });
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "201806050002"));
                        _GVBL.DeleteRow(_GVBL.FocusedRowHandle);
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

        private void btnSaveBL_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (!IsChange_BL) return;
                if (!Save_BL()) return;
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                DataSource_BL = OceanExportService.GetBLInfoListByOperationID(_OceanMBLInfo.OceanBookingID, _OceanMBLInfo.ID, _BLSourceType);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }

        private void btnCargoAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (CurrentRow_BL == null||CurrentRow_BL.ID.IsNullOrEmpty()) return;
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

        private void btnCargoDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (CurrentRow_BL == null || CurrentRow_BL.ID.IsNullOrEmpty()) return;
                if (CurrentRow_Cargo == null) return;

                if (XtraMessageBox.Show(NativeLanguageService.GetText(this, "BLDeleteSure"),
                    LocalData.IsEnglish ? "Tip" : "提示",
                    MessageBoxButtons.YesNo) != DialogResult.Yes) return;
                if (CurrentRow_Cargo.ID.IsNullOrEmpty())
                {
                    _GVCargo.DeleteRow(_GVCargo.FocusedRowHandle);
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "201806050002"));
                }
                else
                {
                    try
                    {
                        OceanExportService.RemoveOceanDeclarationBLContainerInfo(new[] { CurrentRow_Cargo.ID }, LocalData.UserInfo.LoginID, new[] { CurrentRow_Cargo.UpdateDate });
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "201806050002"));
                        _GVCargo.DeleteRow(_GVCargo.FocusedRowHandle);
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

        private void btnSaveCargo_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (!IsChange_Cargo) return;
                if (CurrentRow_BL == null || CurrentRow_BL.ID.IsNullOrEmpty()) return;
                if (CurrentRow_Container == null || CurrentRow_Container.ID.IsNullOrEmpty()) return;
                if (!Save_Cargo()) return;
                DataSource_Cargo = OceanExportService.GetOceanContainerCargoList(_OceanMBLInfo.ID, CurrentRow_BL.ID, _BLSourceType);

                string commoditys = "";
                foreach (var item in DataSource_Cargo.GroupBy(item=>item.Commodity))
                {
                    if (commoditys=="")
                        commoditys +=item.Key;
                    else
                        commoditys += ","+item.Key;
                }
                CurrentRow_BL.GoodsDescription = commoditys;
                Save_BL();
                DataSource_BL = OceanExportService.GetBLInfoListByOperationID(_OceanMBLInfo.OceanBookingID, _OceanMBLInfo.ID, _BLSourceType);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "201806050001"));
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        #endregion

        #region 方法

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

        void BindingData(object data)
        {
            InitControls();
            Guid _OceanMBLID = (Guid)stateValues["MBLID"];
            _BLSourceType = (FCMBLType)stateValues["FCMBLType"];
            _OceanMBLInfo = OceanExportService.GetOceanMBLInfo(_OceanMBLID);
            txtMBLNo.Text = _OceanMBLInfo.No;
            DataSource_Container = OceanExportService.GetOceanContainerList(_OceanMBLInfo.OceanBookingID);
            DataSource_BL = OceanExportService.GetBLInfoListByOperationID(_OceanMBLInfo.OceanBookingID, _OceanMBLInfo.ID, _BLSourceType);
        }

        void EndEdit_Contaienr()
        {
            Validate();
            _BSContainer.EndEdit();
        }

        void EndEdit_BL()
        {
            Validate();
            _BSBL.EndEdit();
        }

        public override void EndEdit()
        {
            Validate();
            _BSCargo.EndEdit();
        }

        private void FillContainerNos()
        {
            ricbxContainers.Items.Clear();
            foreach (OceanContainerList info in DataSource_Container)
            {
                ImageComboBoxItem icbi = new ImageComboBoxItem { Description = info.No, Value = info.ID };
                ricbxContainers.Items.Add(icbi);
            }
        }

        private bool Save_Container()
        {
            EndEdit_Contaienr();
            List<ContainerSaveRequest> saveDatas = BuildContainersData();
            OceanExportService.SaveOceanContainerList(saveDatas);
            return true;
        }

        private bool Save_BL()
        {
            EndEdit_BL();
            List<BLSaveRequest> saveDatas = BuildBLData();
            OceanExportService.SaveDeclarationOceanBLInfoList(saveDatas);
            return true;
        }

        private bool Save_Cargo()
        {
            EndEdit();
            List<CargoSaveRequest> saveDatas = BuildCargoData();
            OceanExportService.SaveOceanDeclarationBLContainerInfoList(saveDatas);
            return true;
        }

        /// <summary>
        /// 构建箱保存数据对象
        /// </summary>
        /// <returns></returns>
        private List<ContainerSaveRequest> BuildContainersData()
        {
            List<OceanContainerList> allList = DataSource_Container;
            if (allList.Count <= 0)
            {
                return null;
            }

            List<ContainerSaveRequest> commands = new List<ContainerSaveRequest>();

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
                oceanBookingID = _OceanMBLInfo.OceanBookingID,
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

            commands.Add(saveRequest);

            return commands;
        }

        /// <summary>
        /// 构建箱保存数据对象
        /// </summary>
        /// <returns></returns>
        private List<BLSaveRequest> BuildBLData()
        {
            List<OceanBLInfo> allList = DataSource_BL;
            if (allList.Count <= 0)
            {
                return null;
            }

            List<BLSaveRequest> commands = new List<BLSaveRequest>();

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
                oceanBookingID = _OceanMBLInfo.OceanBookingID,
                MBLID = _OceanMBLInfo.ID,
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

            commands.Add(saveRequest);

            return commands;
        }

        /// <summary>
        /// 构建箱保存数据对象
        /// </summary>
        /// <returns></returns>
        private List<CargoSaveRequest> BuildCargoData()
        {
            List<OceanContainerCargoList> allList = DataSource_Cargo;

            if (allList.Count <= 0)
            {
                return null;
            }

            List<CargoSaveRequest> commands = new List<CargoSaveRequest>();

            #region 收集数据

            List<Guid> ContainerIDs = new List<Guid>()
                ,BLIDs=new List<Guid>()
                ;
            List<Guid?> IDs = new List<Guid?>();
            List<string> cargoMarks = new List<string>()
                , cargoComodities = new List<string>()
                , cargoHSCodes = new List<string>();
            List<int> cargoQuantities = new List<int>();
            List<decimal> cargoWeights = new List<decimal>()
                , cargoMeasurements = new List<decimal>();

            List<DateTime?> cargoUpdateDates = new List<DateTime?>();
            Guid BLID = Guid.Empty;
            foreach (OceanContainerCargoList item in allList)
            {
                BLIDs.Add(item.BLID);
                ContainerIDs.Add(item.OceanContainerID);
                IDs.Add(item.ID);
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
                oceanBookingID = _OceanMBLInfo.OceanBookingID,
                MBLID = _OceanMBLInfo.ID,
                hblids = BLIDs.ToArray(),
                containerids = ContainerIDs.ToArray(),
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

            commands.Add(saveRequest);

            return commands;
        } 

        #endregion
    }
}
