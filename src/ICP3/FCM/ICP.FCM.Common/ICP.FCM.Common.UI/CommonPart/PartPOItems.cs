using DevExpress.XtraBars;
using ICP.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.CompositeObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.ComponentModel;

namespace ICP.FCM.Common.UI.CommonPart
{
    /// <summary>
    /// 
    /// </summary>
    [ToolboxItem(false)]
    [SmartPart]
    public partial class PartPOItems : BaseEditPart
    {
        #region 成员
        /// <summary>
        /// 
        /// </summary>
        bool _isChanged = false;
        /// <summary>
        /// 
        /// </summary>
        public bool IsChanged
        {
            get
            {
                if (_isChanged == false)
                {
                    List<PurchaseOrderItem> source = bindingSourceMain.DataSource as List<PurchaseOrderItem>;
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
        /// <summary>
        /// 业务ID
        /// </summary>
        BusinessOperationContext Context { get; set; }
        /// <summary>
        /// 当前行
        /// </summary>
        PurchaseOrderItem CurrentRow
        {
            get
            {
                if (bindingSourceMain.Current == null)
                    return null;
                else
                    return bindingSourceMain.Current as PurchaseOrderItem;
            }
        }
        /// <summary>
        /// 选择行
        /// </summary>
        List<PurchaseOrderItem> SelectRows
        {
            get
            {
                int[] indexs = gridViewMain.GetSelectedRows();
                if (indexs == null || indexs.Length == 0) return null;

                List<PurchaseOrderItem> list = new List<PurchaseOrderItem>();
                foreach (var item in indexs)
                {
                    PurchaseOrderItem tager = gridViewMain.GetRow(item) as PurchaseOrderItem;
                    if (tager != null) list.Add(tager);
                }
                return list;
            }
        }
        /// <summary>
        /// 选择行
        /// </summary>
        List<PurchaseOrderItem> DataList
        {
            get
            {
                List<PurchaseOrderItem> datalist = DataSource as List<PurchaseOrderItem>;
                if (datalist == null)
                    datalist= new List<PurchaseOrderItem>();
                return datalist;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override object DataSource
        {
            get { return bindingSourceMain.DataSource; }
            set { BindingData(value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public override event SavedHandler Saved;
        #endregion
        
        #region 服务
        /// <summary>
        /// FCM公用服务
        /// </summary>
        IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }

        /// <summary>
        /// Excel服务
        /// </summary>
        IExcelService ExcelService
        {
            get
            {
                return ServiceClient.GetClientService<IExcelService>();
            }
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 
        /// </summary>
        public PartPOItems()
        {
            InitializeComponent();
            Disposed += delegate
            {
                gridControlMain.DataSource = null;
                dxErrorProvider1.DataSource = null;
                bindingSourceMain.DataSource = null;
                bindingSourceMain.Dispose();
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            RegisterRelativeEvents();
            InitControls();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="values"></param>
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
        }
        #endregion

        #region 窗体事件
        /// <summary>
        /// 
        /// </summary>
        void barAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            PurchaseOrderItem newRow = new PurchaseOrderItem()
            {
                ID = 0,
            };
            bindingSourceMain.Add(newRow);
            bindingSourceMain.ResetBindings(false);
            _isChanged = true;
            gridViewMain.MoveLast();
        }
        /// <summary>
        /// 
        /// </summary>
        void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<PurchaseOrderItem> list = SelectRows;
            if (list == null || list.Count == 0) return;

            if (!PartLoader.EnquireIsDeleteCurrentData())
            {
                return;
            }

            List<int> needRemoveIDs = new List<int>();

            foreach (var item in list)
            {
                if (item.ID == 0)
                {
                    needRemoveIDs.Add(item.ID);
                }
            }
            try
            {
                if (needRemoveIDs.Count != 0)
                {
                    //OceanExportService.RemoveOceanOrderFeeList(needRemoveIDs.ToArray(),_CompanyID, LocalData.UserInfo.LoginID, needRemoveUpdateDate.ToArray());
                }

                gridViewMain.DeleteSelectedRows();
                _isChanged = true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm()
                    , (LocalData.IsEnglish ? "Delete Faily" : "删除失败.") + ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        void barImport_ItemClick(object sender, ItemClickEventArgs e)
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
                    Filter = "Excel files (*.xls;*.xlsx)|*.xls;*.xlsx",
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
                    List<PurchaseOrderItem> poItems = ResolveDataByDataTable(result);
                    bindingSourceMain.DataSource = poItems;
                    bindingSourceMain.ResetBindings(false);
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                SaveData();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        #endregion

        #region 方法
        private void InitControls()
        {
        }

        /// <summary>
        /// 注册各种联动的事件
        /// </summary>
        void RegisterRelativeEvents()
        {
            barAdd.ItemClick += barAdd_ItemClick;
            barDelete.ItemClick += barDelete_ItemClick;
            barImport.ItemClick += barImport_ItemClick;
            barSave.ItemClick += barSave_ItemClick;
        }

       
        void BindingData(object data)
        {
            Context = data as BusinessOperationContext;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool SaveData()
        {
            return Save();
        }
        /// <summary>
        /// 
        /// </summary>
        public override void EndEdit()
        {
            Validate();
            bindingSourceMain.EndEdit();
        }

        List<PurchaseOrderItem> ResolveDataByDataTable(DataTable result)
        {
            DataView dvFilter = result.DefaultView;
            dvFilter.RowFilter = "Column1 <> 'Container NO'";
            DataTable filterResult = dvFilter.ToTable();
            List<PurchaseOrderItem> POItems = (from item in filterResult.AsEnumerable()
                                               select new PurchaseOrderItem
                                            {
                                                ID = 0,
                                                PurchaseOrderID = 0,
                                                BillOfLadingID = Guid.Empty,
                                                BillOfLadingNO = item.Column<string>("Column2"),
                                                ContainerID = Guid.Empty,
                                                ContainerNO = item.Column<string>("Column1"),
                                                PurchaseOrderNO = item.Column<string>("Column3"),
                                                ProductName = item.Column<string>("Column4"),
                                                StockKeepingUnit = item.Column<string>("Column5"),
                                                ManufacturerPartNumber = item.Column<string>("Column6"),
                                                CartonCount = Convert.ToInt32(item.Column<string>("Column7")),
                                                Quantity = Convert.ToInt32(item.Column<string>("Column8")),
                                                UnitCostPrice = Convert.ToDecimal(item.Column<string>("Column9")),
                                                Weight = Convert.ToDecimal(item.Column<string>("Column10")),
                                                Volume = Convert.ToDecimal(item.Column<string>("Column11")),
                                            }).ToList();
            return POItems;
        }


        private bool Save()
        {
            try
            {
                barSave.Enabled = false;
                if ( ValidateData() == false)
                {
                    return false;
                }
                List<SaveRequestPurchaseOrderItem> originalData = BuildSaveRequest(Context.OperationID, Context.OperationType);
                FCMCommonService.SaveImportPurchaseOrderItem(originalData);
                AfterSave();
               
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                return false;
            }
            finally
            {
                barSave.Enabled = true;
            }
        }
        private void AfterSave()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(AfterSave));
            }
            else
            {
                CurrentRow.CancelEdit();
                CurrentRow.BeginEdit();
                //TriggerSavedEvent();
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
            }
        }
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        public bool ValidateData()
        {
            Validate();
            bindingSourceMain.EndEdit();

            foreach (var item in bindingSourceMain.List)
            {
                if ((item as PurchaseOrderItem).Validate
                    (
                    ) == false) return false;
            }

            return true;
        }

        /// <summary>
        /// 构建保存对象
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType">业务类型</param>
        /// <returns></returns>
        List<SaveRequestPurchaseOrderItem> BuildSaveRequest(Guid operationID, OperationType operationType)
        {
            gridViewMain.CloseEditor();
            if (DataList.Count != 0)
            {
                List<PurchaseOrderItem> changedLists = DataList.FindAll(o => o.IsNew || o.IsDirty);
                if (changedLists.Count > 0)
                {
                    List<SaveRequestPurchaseOrderItem> commands = new List<SaveRequestPurchaseOrderItem>()
                    {
                        new SaveRequestPurchaseOrderItem()
                        {
                            OperationID = operationID,
                            OperationType = operationType,
                            PurchaseOrderIDs =changedLists.Select(fItem => fItem.PurchaseOrderID).ToArray(),
                            IDs = changedLists.Select(fItem => fItem.ID).ToArray(),
                            BillOfLadingIDs = changedLists.Select(fItem => fItem.BillOfLadingID).ToArray(),
                            BillOfLadingNOs = changedLists.Select(fItem => fItem.BillOfLadingNO).ToArray(),
                            ContainerIDs = changedLists.Select(fItem => fItem.ContainerID).ToArray(),
                            ContainerNOs = changedLists.Select(fItem => fItem.ContainerNO).ToArray(),
                            PurchaseOrderNOs = changedLists.Select(fItem => fItem.PurchaseOrderNO).ToArray(),
                            ProductNames = changedLists.Select(fItem => fItem.ProductName).ToArray(),
                            StockKeepingUnits = changedLists.Select(fItem => fItem.StockKeepingUnit).ToArray(),
                            ManufacturerPartNumbers = changedLists.Select(fItem => fItem.ManufacturerPartNumber).ToArray(),
                            CartonCounts = changedLists.Select(fItem => fItem.CartonCount).ToArray(),
                            Quantitys = changedLists.Select(fItem => fItem.Quantity).ToArray(),
                            UnitCostPrices = changedLists.Select(fItem => fItem.UnitCostPrice).ToArray(),
                            Weights = changedLists.Select(fItem => fItem.Weight).ToArray(),
                            Volumes = changedLists.Select(fItem => fItem.Volume).ToArray(),
                            SaveByID = LocalData.UserInfo.LoginID,
                            UpdateDate = DateTime.Now,
                        },
                    };

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

        public void AfterSaved()
        {
            _isChanged = false;
        }
        #endregion
    }
}
