using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using System.Reflection;
using ICP.Framework.CommonLibrary.Common;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using ICP.FCM.OceanExport.ServiceInterface.CompositeObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using DevExpress.XtraEditors;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.Common.ServiceInterface.CompositeObjects;
using ICP.FCM.Common.ServiceInterface;

namespace ICP.FCM.OceanExport.UI
{
    /// <summary>
    /// 
    /// </summary>
    public partial class POItemSelectPart : BasePart, IChildPart
    {
        #region Service
        /// <summary>
        /// 
        /// </summary>
        public WorkItem Workitem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }
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
        #endregion

        #region 成员
        PurchaseOrderItem CurrentRow
        {
            get
            {
                return bindingSource1.Current as PurchaseOrderItem;
            }
        }

        List<PurchaseOrderItem> cachePOItems = null;
        List<PurchaseOrderItem> _CachePOItems
        {
            get
            {
                if (cachePOItems == null)
                    cachePOItems = new List<PurchaseOrderItem>();
                return cachePOItems;
            }
            set
            {
                cachePOItems = value;
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
                    datalist = new List<PurchaseOrderItem>();
                return datalist;
            }
        }
        public Guid OperationID { get; set; }
        public int OperationMapID { get; set; }
        public OperationType OperationType { get; set; }

        #endregion

        #region 构造函数

        public POItemSelectPart()
        {
            InitializeComponent();
            Disposed += delegate {
                RemoveItemPropertyChangedHandle();
                gridControl1.DataSource = null;
                bindingSource1.DataSource = null;
                bindingSource1.Dispose();
                DataChanged = null;
                if (Workitem != null)
                {   
                    Workitem.Items.Remove(this); 
                }
            };
        }

        #endregion

        #region IChildPart 成员
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler DataChanged;
        /// <summary>
        /// 
        /// </summary>
        public bool IsChanged
        {
            get
            {
                this.gridViewPO.CloseEditor();
                List<PurchaseOrderItem> source = bindingSource1.DataSource as List<PurchaseOrderItem>;
                if (source != null)
                {
                    foreach (var item in source)
                    {
                        if (item.IsDirty)
                        {
                            return true;
                        }
                    }

                    return false;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool ValidateData()
        {
            this.gridViewPO.CloseEditor();
            this.bindingSource1.EndEdit();

            this.Validate();

            bool isScrr = true;

            List<PurchaseOrderItem> source = bindingSource1.DataSource as List<PurchaseOrderItem>;

            if (source != null)
            {
                foreach (PurchaseOrderItem listItem in source)
                {
                    if (listItem.Validate(delegate(ValidateEventArgs e)
                    {

                    }) == false) isScrr = false;
                }
            }

            return isScrr;
        }

        public void AfterSaved()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public object DataSource { get { return bindingSource1.DataSource as List<PurchaseOrderItem>; } }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void SetSource(object value)
        {
            if (value == null) return;

            this.bindingSource1.DataSource = value;
            this.bindingSource1.ResetBindings(false);
        }
        #endregion

        #region 公开方法
        public void SetHBLCombox(List<OceanHBLInfo> bls)
        {
            this.ricbxHBLs.Items.Clear();
            foreach (OceanHBLInfo info in bls)
            {
                DevExpress.XtraEditors.Controls.ImageComboBoxItem icbi = new DevExpress.XtraEditors.Controls.ImageComboBoxItem();
                icbi.Description = info.No;
                icbi.Value = info.No;

                this.ricbxHBLs.Items.Add(icbi);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="containers"></param>
        public void SetContainerCombox(List<OceanContainerList> containers)
        {
            this.ricbxContainers.Items.Clear();
            foreach (OceanContainerList info in containers)
            {
                DevExpress.XtraEditors.Controls.ImageComboBoxItem icbi = new DevExpress.XtraEditors.Controls.ImageComboBoxItem();
                icbi.Description = info.No;
                icbi.Value = info.No;

                this.ricbxContainers.Items.Add(icbi);
            }
        }
        /// <summary>
        /// 首次显示的时候从服务器获取所有的PO及PO和箱的关联关系
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="workItem"></param>
        /// <param name="containerId"></param>
        public void SetAllPO(Guid orderId)
        {
            _CachePOItems = FCMCommonService.SearchPurchaseOrderItemByShipmentInfo(new SearchParameterOrderItemByShipment() { ShipmentID = OperationMapID});
            SetSource(_CachePOItems);
            //if (this._orderId == Guid.Empty)
            //{
            //    _orderId = orderId;

            //    _containerId = containerId;
            //    CompositePoAndItems pos = OceanExportService.GetOceanContainerPOList(orderId);

            //    _pos = pos;

            //    foreach (PurchaseOrderItem po in this._pos.PoList)
            //    {
            //        foreach (var item in po.Items)
            //        {
            //            item.PropertyChanged +=item_PropertyChanged;
            //        }
            //    }

            //    this.ShowPoList();

            //    this.RefreshEnabled();
            //}
        }
        private void RemoveItemPropertyChangedHandle()
        {
            //if (this._pos == null || this._pos.PoList == null)
            //    return;
        }

        /// <summary>
        /// 重置过滤PO的条件，并且设置PO列表的关联状态
        /// </summary>
        /// <param name="containerId"></param>
        public void SetRelationShips(Guid containerId)
        {
            //if (this._containerId != containerId)
            //{
            //    _containerId = containerId;

            //    this.rgFilter.SelectedIndexChanged -= new EventHandler(rgFilter_SelectedIndexChanged);
            //    this.txtFind.TextChanged -= new EventHandler(txtFind_TextChanged);
            //    this.rgFilter.SelectedIndex = 0;

            //    this.txtFind.Text = string.Empty;

            //    this.ShowPoList();

            //    this.rgFilter.SelectedIndexChanged += new EventHandler(rgFilter_SelectedIndexChanged);
            //    this.txtFind.TextChanged += new EventHandler(txtFind_TextChanged);
            //}
            //this.RefreshEnabled();
        }

        void RefreshEnabled()
        {
            //this.barNew.Enabled = this._containerId != Guid.Empty;
            //this.barRemove.Enabled = this.gridViewPO.GetFocusedRow() != null;
        }

        #endregion

        #region 保存
        /// <summary>
        /// 构建保存对象
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType">业务类型</param>
        /// <returns></returns>
        public List<SaveRequestPurchaseOrderItem> BuildSaveRequest()
        {
            gridViewPO.CloseEditor();
            if (DataList.Count != 0)
            {
                List<PurchaseOrderItem> changedLists = DataList.FindAll(o => o.IsNew || o.IsDirty);
                if (changedLists.Count > 0)
                {
                    List<SaveRequestPurchaseOrderItem> commands = new List<SaveRequestPurchaseOrderItem>()
                    {
                        new SaveRequestPurchaseOrderItem()
                        {
                            OperationID = OperationID,
                            OperationType = OperationType,
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
        public void SaveTempRelations()
        {

            //List<PurchaseOrderItem> _listHasChanged = this._pos.FindAll(o => o.IsDirty);

            //for (int i = 0; i < _listHasChanged.Count; i++)
            //{
            //    PurchaseOrderItem po = _listHasChanged[i];

            //    po.RelationID = po.Associated ? this._containerId : Guid.Empty;

            //    foreach (OceanPOItemList item in po.Items)
            //    {
            //        item.RelationID = item.Associated ? this._containerId : Guid.Empty;
            //    }
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SaveRequestPurchaseOrderItem GetRelations()
        {
            //if (this._pos == null)
            //{
            //    return null;
            //}
            SaveRequestPurchaseOrderItem request = new SaveRequestPurchaseOrderItem();

            //request.poIds = this._pos.POContainerRelationsList.Select(o => o.PoId).ToList().ToArray();
            //request.poContainerIds = this._pos.POContainerRelationsList.Select(o => o.ContainerId).ToList().ToArray();

            //request.itemIds = this._pos.PoItemContainerRelationsList.Select(o => o.ItemId).ToList().ToArray();
            //request.itemContainerIds = this._pos.PoItemContainerRelationsList.Select(o => o.ContainerId).ToList().ToArray();

            request.SaveByID = LocalData.UserInfo.LoginID;

            return request;
        }

        /// <summary>
        /// 保存有更改的数据
        /// </summary>
        public SaveRequestPurchaseOrderItem CollectPosData()
        {

            if (!this.ValidateData())
            {
                throw new Exception();
            }

            if (this._CachePOItems != null && this._CachePOItems.Count > 0)
            {
                if (this.IsChanged)
                {
                    List<PurchaseOrderItem> changedLists = _CachePOItems.FindAll(o => o.IsNew || o.IsDirty);
                    if (changedLists.Count > 0)
                    {
                        //SaveRequestPurchaseOrderItem saveRequest = new SaveRequestPurchaseOrderItem()
                        //        {
                        //            OperationID = operationID,
                        //            OperationType = operationType,
                        //            PurchaseOrderIDs = changedLists.Select(fItem => fItem.PurchaseOrderID).ToArray(),
                        //            IDs = changedLists.Select(fItem => fItem.ID).ToArray(),
                        //            BillOfLadingIDs = changedLists.Select(fItem => fItem.BillOfLadingID).ToArray(),
                        //            BillOfLadingNOs = changedLists.Select(fItem => fItem.BillOfLadingNO).ToArray(),
                        //            ContainerIDs = changedLists.Select(fItem => fItem.ContainerID).ToArray(),
                        //            ContainerNOs = changedLists.Select(fItem => fItem.ContainerNO).ToArray(),
                        //            PurchaseOrderNOs = changedLists.Select(fItem => fItem.PurchaseOrderNO).ToArray(),
                        //            ProductNames = changedLists.Select(fItem => fItem.ProductName).ToArray(),
                        //            StockKeepingUnits = changedLists.Select(fItem => fItem.StockKeepingUnit).ToArray(),
                        //            ManufacturerPartNumbers = changedLists.Select(fItem => fItem.ManufacturerPartNumber).ToArray(),
                        //            CartonCounts = changedLists.Select(fItem => fItem.CartonCount).ToArray(),
                        //            Quantitys = changedLists.Select(fItem => fItem.Quantity).ToArray(),
                        //            UnitCostPrices = changedLists.Select(fItem => fItem.UnitCostPrice).ToArray(),
                        //            Weights = changedLists.Select(fItem => fItem.Weight).ToArray(),
                        //            Volumes = changedLists.Select(fItem => fItem.Volume).ToArray(),
                        //            SaveByID = LocalData.UserInfo.LoginID,
                        //            UpdateDate = DateTime.Now,
                        //        };
                        //return saveRequest;
                        return null;
                    }
                    return null;
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

        public void RefreshUI()
        {
            //foreach (POSaveRequest saveRequest in list)
            //{
            //    if (saveRequest.HasCommit)
            //    {
            //        PurchaseOrderItem po = saveRequest.UnBoxInvolvedObject<PurchaseOrderItem>()[0];

            //        HierarchyManyResult result = saveRequest.HierarchyManyResult;
            //        po.ID = result.GetValue<Guid>("POID");
            //        po.RelationID = result.GetValue<Guid?>("RelationID");
            //        po.UpdateDate = result.GetValue<DateTime?>("UpdateDate");

            //        #region 更新对应关系中的PO主键

            //        Guid newId = result.GetValue<Guid>("POID");

            //        if (newId != saveRequest.id.Value)
            //        {
            //            foreach (POContainerRelation rel in this._pos.POContainerRelationsList)
            //            {
            //                if (rel.PoId == saveRequest.id.Value)
            //                {
            //                    rel.PoId = newId;
            //                }
            //            }
            //        }

            //        #endregion

            //        for (int j = 0; j < po.Items.Count; j++)
            //        {
            //            #region 更新本地对应关系中的Item的主键

            //            Guid newItemId = po.Items[j].ID;
            //            Guid oldItemId = saveRequest.itemIDs[j].Value;

            //            foreach (PoItemContainerRelation rel in this._pos.PoItemContainerRelationsList)
            //            {
            //                if (rel.PoId == saveRequest.id.Value)
            //                {
            //                    rel.PoId = newId;
            //                }

            //                if (rel.ItemId == po.Items[j].ID)
            //                {
            //                    rel.ItemId = result.Childs[j].GetValue<Guid>("ItemID");
            //                }
            //            }

            //            #endregion

            //            po.Items[j].ID = result.Childs[j].GetValue<Guid>("ItemID");
            //            po.Items[j].RelationID = result.Childs[j].GetValue<Guid?>("RelationID");
            //            po.Items[j].UpdateDate = result.Childs[j].GetValue<DateTime?>("UpdateDate");
            //            po.Items[j].POID = result.Childs[j].GetValue<Guid>("POID");
            //            po.Items[j].IsDirty = false;
            //        }


            //        po.IsDirty = false;//因为子项的属性改动，会影响父项的IsDirty属性    

            //        this.bindingSource1.ResetBindings(false);

            //        if (DataChanged != null)
            //        {
            //            this.DataChanged(_pos, EventArgs.Empty);
            //        }
            //    }
            //}

            this.RefreshEnabled();
        }

        #endregion

        #region 新增

        private void barNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddData();

            this.RefreshEnabled();
        }

        DateTime _nullValue = new DateTime(1900, 1, 1, 1, 1, 1);

        private void AddData()
        {
            PurchaseOrderItem newData = new PurchaseOrderItem()
            {
                ID = 0,
                PurchaseOrderID = 0,
                BillOfLadingID = Guid.Empty,
                BillOfLadingNO = "",
                ContainerID = Guid.Empty,
                ContainerNO = "",
                PurchaseOrderNO = "",
                ProductName = "",
                StockKeepingUnit = "",
                ManufacturerPartNumber = "",
                CartonCount = 0,
                Quantity = 0.000M,
                UnitCostPrice = 0.000M,
                Weight = 0.000M,
                Volume = 0.000M,
            };
            bindingSource1.Add(newData);
            newData.IsDirty = true;
            this.gridViewPO.ClearSorting();
            //this._pos.PoList.Add(newData);
            this.gridViewPO.MoveLast();
        }

        const int newRowHandle = int.MinValue + 1;//DEV里的新行常量

        void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
        }

        private void gridViewPO_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
        }

        DevExpress.XtraGrid.Columns.GridColumn changedColumn = null;
        object changedValue = null;
        private void gridViewItem_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle == newRowHandle)
            {
                changedValue = e.Value;
                changedColumn = e.Column;
            }
        }

        #endregion

        #region 删除

        private void barRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteData();

            this.RefreshEnabled();
        }

        private void DeleteData()
        {
            
        }

        private void gridViewItem_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete) DeletePoItem();
        }

        private void DeletePoItem()
        {
            //DevExpress.XtraGrid.Views.Grid.GridView gv = null;
            //for (int i = 1; i < gridControl1.Views.Count; i++)
            //{
            //    if (gridControl1.Views[i].IsFocusedView)
            //    {
            //        gv = (DevExpress.XtraGrid.Views.Grid.GridView)gridControl1.Views[i];
            //        continue;
            //    }

            //}

            //if (gv == null) return;

            //int[] selectedIndex = gv.GetSelectedRows();
            //if (selectedIndex.Length <= 0) return;

            //this.CurrentRow.IsDirty = true;

            //gv.DeleteSelectedRows();

            ////如果PoItem都删除了，则要删除它所有的关联关系
            //this._pos.PoItemContainerRelationsList.RemoveAll(o => !ItemIsExist(o.ItemId));

            //gv.RefreshData();

            //this.bindingSource1.ResetBindings(false);
        }

        bool ItemIsExist(Guid id)
        {
            //List<Guid> itemIds = new List<Guid>();
            //foreach (var po in this._pos.PoList)
            //{
            //    itemIds.AddRange(po.Items.Select(o => o.ID));
            //}

            //return itemIds.FindAll(o => o == id).Count > 0;
            return false;
        }

        #endregion

        #region 显示PO列表

        public enum Filter
        {
            All =0,
            Selected=1,
            UnSelected =2
        }

        void rgFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowPoList();
        }

        void ShowPoList()
        {
            int selected = this.rgFilter.SelectedIndex;

            Filter filter = (Filter)selected;
            string keyword = this.txtFind.Text.Trim().ToUpper();
            string filterString = "";
            switch (filter)
            {
                case Filter.Selected:
                    if (string.IsNullOrEmpty(keyword))
                        filterString = "[Bill Of Lading] Is Not Null blank AND [Container NO] Is Not Null ";
                    break;
                case Filter.UnSelected:
                    filterString = "[Bill Of Lading] Is Null blank AND [Container NO] Is Null ";
                    break;
            }

            if (string.IsNullOrEmpty(keyword))
            {
                filterString = string.Format("[Bill Of Lading] like '%{0}%' AND [Container NO] like '%{0}%' ", keyword);
            }
            gridViewPO.ActiveFilterString = filterString;
        }


        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            this.ShowPoList();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.gridControl1.SelectNextControl(this.gridControl1, false, true, true, true);
        }

        #endregion

      
    }
}
