using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;

using Microsoft.Practices.CompositeUI;
using ICP.FCM.AirExport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using System.Reflection;
using ICP.Framework.CommonLibrary.Common;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using ICP.FCM.AirExport.ServiceInterface.CompositeObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FCM.Common.ServiceInterface.DataObjects;

namespace ICP.FCM.AirExport.UI
{
    public partial class POItemSelectPart : DevExpress.XtraEditors.XtraUserControl, IChildPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IAirExportService OeService { get { return Workitem.Services.Get<IAirExportService>(); } }
      
        #endregion

        #region 私有成员

        /// <summary>
        /// 订舱单GUID
        /// </summary>
        Guid _orderId;

        /// <summary>
        /// 当前箱GUID
        /// </summary>
        Guid _containerId;

        /// <summary>
        /// 当前订舱单所有的PO的列表
        /// </summary>
        CompositePoAndItems _pos;

        #endregion

        #region 公开成员

        AirBookingPOList CurrentRow
        {
            get
            {
                return bindingSource1.Current as AirBookingPOList;
            }
        }

        #endregion

        #region 构造函数

        public POItemSelectPart()
        {
            InitializeComponent();

            if (LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };

            this.gridViewItem.ShowingEditor += new CancelEventHandler(gridViewItem_ShowingEditor);

            this.gridViewItem.ShownEditor += new EventHandler(gridViewItem_ShownEditor);

            this.gridViewItem.CalcRowHeight += new DevExpress.XtraGrid.Views.Grid.RowHeightEventHandler(gridViewItem_CalcRowHeight);

            this.gridViewItem.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gridViewItem_CellValueChanging);
            this.gridViewItem.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewItem_FocusedRowChanged);

        }

        int _lastItemRowHandler = int.MinValue +5;

        void gridViewItem_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            _lastItemRowHandler = e.FocusedRowHandle;
        }

        void gridViewItem_CalcRowHeight(object sender, DevExpress.XtraGrid.Views.Grid.RowHeightEventArgs e)
        {
        }

        void gridViewItem_ShownEditor(object sender, EventArgs e)
        {
        }

        void gridViewItem_ShowingEditor(object sender, CancelEventArgs e)
        {
            Point pt = gridControl1.PointToClient(MousePosition);
            GridHitInfo hitInfo = gridViewPO.CalcHitInfo(pt);
            if (hitInfo != null && hitInfo.RowHandle >= 0)
            {
                gridViewPO.FocusedRowHandle = hitInfo.RowHandle;
            }
        }

        private void SetCnText()
        {
            this.cbxExpandAll.Text = "全部展开";
            colBuyerName.Caption = "买家";
            colCartons.Caption = "箱";
            colColor.Caption = "颜色";
            colCreateByName.Caption = "创建人";
            colCreateDate.Caption = "创建日期";
            colDescription.Caption = "描述";
            colFinalDestination.Caption = "目的地";
            colHTSCode.Caption = "HTS Code";
            colInWarehouseDate.Caption = "入仓日";
            colNo.Caption = "NO";
            colItemNo.Caption = "NO";
            colOrderDate.Caption = "订单日";
            colPODescription.Caption = "描述";
            colSize.Caption = "尺寸";
            colUnits.Caption = "件数";
            colVendorName.Caption = "卖主";
            colVolume.Caption = "体积";
            colWeight.Caption = "重量";
            gridViewItem.NewItemRowText = "点击这里以新增一行.选中行首按下Delete键可以删除一行.";
            colHTSCode.Caption = "海关编码";
            colNo.Caption = "订单号";
            colItemNo.Caption = "产品编号";
            colAssociated.Caption = "关联";
            this.colItemAssociated.Caption = "关联";

            barNew.Caption = "新增(&N)";
            barRemove.Caption = "删除(&R)";
        }

        #endregion

        #region IChildPart 成员

        public event EventHandler DataChanged;

        public bool IsChanged
        {
            get
            {
                this.gridViewPO.CloseEditor();
                this.gridViewItem.CloseEditor();
                List<AirBookingPOList> source = bindingSource1.DataSource as List<AirBookingPOList>;
                if (source != null)
                {
                    foreach (var item in source)
                    {
                        if (item.IsDirty)
                        {
                            return true;
                        }
                        foreach (var items in item.Items)
                        {
                            if (items.IsDirty)
                            {
                                return true;
                            }
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
            this.gridViewItem.CloseEditor();
            this.gridViewPO.CloseEditor();
            this.bindingSource1.EndEdit();

            this.Validate();

            bool isScrr = true;

            List<AirBookingPOList> source = bindingSource1.DataSource as List<AirBookingPOList>;

            if (source != null)
            {
                foreach (AirBookingPOList listItem in source)
                {
                    if (listItem.Validate(delegate(ValidateEventArgs e)
                    {

                    }) == false) isScrr = false;

                    foreach (var item in listItem.Items)
                    {
                        if (item.Validate(delegate(ValidateEventArgs e)
                        {
                            if (string.IsNullOrEmpty(item.No))
                            {
                                e.SetErrorInfo("NO", LocalData.IsEnglish ? "You must input Product NO." : "必须输入产品编号");
                            }
                            else
                            {
                                if (listItem.Items.FindAll(o => o.No == item.No).Count > 1)
                                {
                                    e.SetErrorInfo("NO", LocalData.IsEnglish ?
                                        "The item no must not be repeated below the same PO. PO is" + listItem.No + ",product NO. is" + item.No
                                        :
                                        "同一PO下产品编号不能重复。PO：" + listItem.No + ",产品编号：" + item.No);
                                }
                            }
                            //if (item.Units <= 0)
                            //{
                            //    e.SetErrorInfo("Quantity", LocalData.IsEnglish ? "Quantity can't be 0." : "件数不能为0.");
                            //}
                            //if (item.Volume <= 0)
                            //{
                            //    e.SetErrorInfo("Measurement", LocalData.IsEnglish ? "Measurement can't be 0." : "体积不能为0.");
                            //}
                            //if (item.Weight <= 0)
                            //{
                            //    e.SetErrorInfo("Weight", LocalData.IsEnglish ? "Weight can't be 0." : "重量不能为0.");
                            //}
                        }) == false)
                        {
                            isScrr = false;
                            break;
                        }
                    }
                }
            }

            return isScrr;
        }

        public void AfterSaved()
        {
        }

        public object DataSource { get { return bindingSource1.DataSource as List<AirBookingPOList>; } }

        public void SetSource(object value)
        {
            if (value == null) return;

            this.bindingSource1.DataSource = value;
            this.bindingSource1.ResetBindings(false);
        }

        public void SetService(WorkItem workitem)
        {
            Workitem = workitem;
        }

        #endregion

        #region 公开方法

        /// <summary>
        /// 首次显示的时候从服务器获取所有的PO及PO和箱的关联关系
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="workItem"></param>
        /// <param name="containerId"></param>
        public void SetAllPO(Guid orderId, WorkItem workItem, Guid containerId)
        {
            if (this._orderId == Guid.Empty)
            {
                this.Workitem = workItem;
                _orderId = orderId;

                _containerId = containerId;
                CompositePoAndItems pos = OeService.GetAirContainerPOList(orderId);

                _pos = pos;

                foreach (AirBookingPOList po in this._pos.PoList)
                {
                    foreach (var item in po.Items)
                    {
                        item.PropertyChanged +=new PropertyChangedEventHandler(item_PropertyChanged);
                    }
                }

                this.ShowPoList();

                this.RefreshEnabled();
            }
        }

        /// <summary>
        /// 重置过滤PO的条件，并且设置PO列表的关联状态
        /// </summary>
        /// <param name="containerId"></param>
        public void SetRelationShips(Guid containerId)
        {
            if (this._containerId != containerId)
            {
                _containerId = containerId;

                this.rgFilter.SelectedIndexChanged -= new EventHandler(rgFilter_SelectedIndexChanged);
                this.txtFind.TextChanged -= new EventHandler(txtFind_TextChanged);
                this.rgFilter.SelectedIndex = 0;

                this.txtFind.Text = string.Empty;

                this.ShowPoList();

                this.rgFilter.SelectedIndexChanged += new EventHandler(rgFilter_SelectedIndexChanged);
                this.txtFind.TextChanged += new EventHandler(txtFind_TextChanged);
            }
            //_containerId = containerId;

            this.RefreshEnabled();
        }

        void RefreshEnabled()
        {
            this.barNew.Enabled = this._containerId != Guid.Empty;
            this.barRemove.Enabled = this.gridViewPO.GetFocusedRow() != null;
        }

        #endregion

        #region 保存

        public void SaveTempRelations()
        {
            //List<AirBookingPOList> _listHasChanged = this._pos.FindAll(o => o.IsDirty);

            //for (int i = 0; i < _listHasChanged.Count; i++)
            //{
            //    AirBookingPOList po = _listHasChanged[i];

            //    po.RelationID = po.Associated ? this._containerId : Guid.Empty;

            //    foreach (AirPOItemList item in po.Items)
            //    {
            //        item.RelationID = item.Associated ? this._containerId : Guid.Empty;
            //    }
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public PoItemContainersRelationSaveRequest GetRelations()
        {
            if (this._pos == null)
            {
                return null;
            }
            PoItemContainersRelationSaveRequest request = new PoItemContainersRelationSaveRequest();

            request.poIds = this._pos.POContainerRelationsList.Select(o => o.PoId).ToList().ToArray();
            request.poContainerIds = this._pos.POContainerRelationsList.Select(o => o.ContainerId).ToList().ToArray();

            request.itemIds = this._pos.PoItemContainerRelationsList.Select(o => o.ItemId).ToList().ToArray();
            request.itemContainerIds = this._pos.PoItemContainerRelationsList.Select(o => o.ContainerId).ToList().ToArray();

            request.saveByID = LocalData.UserInfo.LoginID;

            return request;
        }

        /// <summary>
        /// 保存有更改的数据
        /// </summary>
        public List<POSaveRequest> CollectPosData()
        {

            if (!this.ValidateData())
            {
                throw new Exception();
            }

            if (this._pos != null && this._pos.PoList.Count > 0)
            {
                if (this.IsChanged)
                {
                    List<POSaveRequest> commands = new List<POSaveRequest>();

                    List<HierarchyManyResult> results = new List<HierarchyManyResult>();

                    List<AirBookingPOList> _listHasChanged = this._pos.PoList.FindAll(o => o.IsDirty);

                    #region Save

                    for (int i = 0; i < _listHasChanged.Count; i++)
                    {
                        AirBookingPOList poItem = _listHasChanged[i];
                        List<Guid?> itemIDs = new List<Guid?>();
                        List<string> itemNos = new List<string>(), itemDescriptions = new List<string>(), itemColors = new List<string>(), itemSizes = new List<string>(), itemHTSCodes = new List<string>();
                        List<decimal> itemVolumes = new List<decimal>(), itemWeights = new List<decimal>();
                        List<int> itemCartons = new List<int>(), itemUnits = new List<int>();
                        List<DateTime?> itemUpdateDates = new List<DateTime?>();
                        List<Guid?> itemRelationIDs = new List<Guid?>();

                        foreach (AirPOItemList item in poItem.Items)
                        {
                            itemIDs.Add(item.ID);
                            itemRelationIDs.Add(item.RelationID.HasValue ? item.RelationID.Value:Guid.Empty);
                            itemNos.Add(item.No);
                            itemDescriptions.Add(item.Description);
                            itemColors.Add(item.Color);
                            itemSizes.Add(item.Size);
                            itemHTSCodes.Add(item.HTSCode);

                            itemVolumes.Add(item.Volume);
                            itemWeights.Add(item.Weight);
                            itemCartons.Add(item.Cartons);
                            itemUnits.Add(item.Units);
                            itemUpdateDates.Add(item.UpdateDate);
                        }

                        POSaveRequest request = new POSaveRequest();

                        request.id = poItem.ID;
                        request.relationID = poItem.RelationID;
                        request.orderID = this._orderId;
                        request.no = poItem.No;
                        request.podc = poItem.PODescription;
                        request.vendorID = poItem.VendorID;
                        request.vendor = poItem.VendorName;
                        request.buyerID = poItem.BuyerID;
                        request.buyer = poItem.BuyerName;
                        request.finalDestination = poItem.FinalDestination;
                        request.inWarehouseDate = poItem.InWarehouseDate;
                        request.orderDate = poItem.OrderDate;
                        request.updateDate = poItem.UpdateDate;
                        
                        request.itemIDs = itemIDs.ToArray();
                        request.itemNos = itemNos.ToArray();
                        request.itemDescriptions = itemDescriptions.ToArray();
                        request.itemColors = itemColors.ToArray();
                        request.itemSizes = itemSizes.ToArray();
                        request.itemVolumes = itemVolumes.ToArray();
                        request.itemWeights = itemWeights.ToArray();
                        request.itemCartons = itemCartons.ToArray();
                        request.itemUnits = itemUnits.ToArray();
                        request.itemHTSCodes = itemHTSCodes.ToArray();
                        request.saveByID = LocalData.UserInfo.LoginID;
                        request.itemUpdateDates = itemUpdateDates.ToArray();

                        //HierarchyManyResult result = this.OeService.SaveAirContainerPOInfo(saveRequest);
                        //results.Add(result);

                        request.AddInvolvedObject(poItem);
                        commands.Add(request);
                    }

                    #endregion

                    
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

        public void RefreshUI(List<POSaveRequest> list)
        {
            foreach (POSaveRequest saveRequest in list)
            {
                if (saveRequest.HasCommit)
                {
                    AirBookingPOList po = saveRequest.UnBoxInvolvedObject<AirBookingPOList>()[0];

                    HierarchyManyResult result = saveRequest.HierarchyManyResult;
                    po.ID = result.GetValue<Guid>("POID");
                    po.RelationID = result.GetValue<Guid?>("RelationID");
                    po.UpdateDate = result.GetValue<DateTime?>("UpdateDate");

                    #region 更新对应关系中的PO主键

                    Guid newId = result.GetValue<Guid>("POID");

                    if (newId != saveRequest.id.Value)
                    {
                        foreach (POContainerRelation rel in this._pos.POContainerRelationsList)
                        {
                            if (rel.PoId == saveRequest.id.Value)
                            {
                                rel.PoId = newId;
                            }
                        }
                    }

                    #endregion

                    for (int j = 0; j < po.Items.Count; j++)
                    {
                        #region 更新本地对应关系中的Item的主键

                        Guid newItemId = po.Items[j].ID;
                        Guid oldItemId = saveRequest.itemIDs[j].Value;

                        foreach (PoItemContainerRelation rel in this._pos.PoItemContainerRelationsList)
                        {
                            if (rel.PoId == saveRequest.id.Value)
                            {
                                rel.PoId = newId;
                            }

                            if (rel.ItemId == po.Items[j].ID)
                            {
                                rel.ItemId = result.Childs[j].GetValue<Guid>("ItemID");
                            }
                        }

                        #endregion

                        po.Items[j].ID = result.Childs[j].GetValue<Guid>("ItemID");
                        po.Items[j].RelationID = result.Childs[j].GetValue<Guid?>("RelationID");
                        po.Items[j].UpdateDate = result.Childs[j].GetValue<DateTime?>("UpdateDate");
                        po.Items[j].POID = result.Childs[j].GetValue<Guid>("POID");
                        po.Items[j].IsDirty = false;
                    }


                    po.IsDirty = false;//因为子项的属性改动，会影响父项的IsDirty属性    

                    this.bindingSource1.ResetBindings(false);

                    if (DataChanged != null)
                    {
                        this.DataChanged(_pos, EventArgs.Empty);
                    }
                }
            }

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
            AirBookingPOList newData = new AirBookingPOList();
            newData.ID = Guid.NewGuid();
            newData.CreateDate = _nullValue;
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.State = FCMPOState.Processing;//正在处理
            newData.Items = new List<AirPOItemList>();
            bindingSource1.Add(newData);
            newData.IsDirty = true;

            this.gridViewPO.ClearSorting();
            this._pos.PoList.Add(newData);

            this.gridViewPO.MoveLast();
        }

        const int newRowHandle = int.MinValue + 1;//DEV里的新行常量
        private void gridViewItem_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            if (CurrentRow != null && e.RowHandle == newRowHandle)
            {
                AirBookingPOList current = CurrentRow;
                if (current.Items == null)
                {
                    current.Items = new List<AirPOItemList>();
                }

                AirPOItemList item = new AirPOItemList();

                PropertyInfo tp = item.GetType().GetProperty(e.Column.FieldName);
                if (tp != null)
                {
                    changedValue = Convert.ChangeType(changedValue, tp.PropertyType);
                    tp.SetValue(item, changedValue, null);
                }

                item.ID = Guid.NewGuid();
                item.CreateByID = LocalData.UserInfo.LoginID;
                item.CreateByName = LocalData.UserInfo.LoginName;
                item.CreateDate = DateTime.Now;
                item.POID = current.ID;
                item.PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);

                if (e.Column.Name == this.colItemAssociated.Name)
                {
                    this.item_PropertyChanged(item, new PropertyChangedEventArgs("Associated"));
                }

                current.IsDirty = true;

                current.Items.Insert(0, item);

                bindingSource1.ResetBindings(false);
                SendKeys.Send("{Down}");
            }


            changedValue = null;

            if (e.Column.Name == this.colItemAssociated.Name)
            {
                AirBookingPOList current = CurrentRow;
                if (current != null)
                {
                    current.IsDirty = true;
                }
            }
        }

        void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            AirPOItemList item = (AirPOItemList)sender;
            //if (!Utility.GuidIsNullOrEmpty(item.POID))
            //{
                AirBookingPOList po = this._pos.PoList.First(o => o.ID == item.POID);
                if (po != null)
                {
                    po.IsDirty = true;
                }
            //}

                if (e.PropertyName == "Associated")
                {
                    item.PropertyChanged -= new PropertyChangedEventHandler(item_PropertyChanged);
                    //item.RelationID = item.Associated ? this._containerId : Guid.Empty;

                    if (item.Associated)
                    {
                        int count = this._pos.PoItemContainerRelationsList.FindAll(o => o.ContainerId == this._containerId
                            && o.PoId == CurrentRow.ID && o.ItemId == item.ID).Count;
                        if (count < 1)
                        {
                            this._pos.PoItemContainerRelationsList.Add(new PoItemContainerRelation
                            {
                                ContainerId = this._containerId,
                                PoId = CurrentRow.ID,
                                ItemId = item.ID
                            });
                        }
                    }
                    else
                    {
                        this._pos.PoItemContainerRelationsList.RemoveAll(o => o.ContainerId == this._containerId
                            && o.PoId == CurrentRow.ID && o.ItemId == item.ID);
                    }

                    item.PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);
                }
        }

        private void gridViewPO_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == this.colAssociated.Name)
            {
                AirBookingPOList current = CurrentRow;
                //current.RelationID = current.Associated ? this._containerId : Guid.Empty;

                if (current.Associated)
                {
                    int count =this._pos.POContainerRelationsList.FindAll(o => o.ContainerId == this._containerId
                           && o.PoId == current.ID).Count;
                    if ( count < 1)
                    {
                        this._pos.POContainerRelationsList.Add(new POContainerRelation
                        {
                            ContainerId = this._containerId,
                            PoId = current.ID
                        });
                    }
                }
                else
                {
                    this._pos.POContainerRelationsList.RemoveAll(o => o.ContainerId == this._containerId
                        && o.PoId == current.ID);
                }
            }
        }

        DevExpress.XtraGrid.Columns.GridColumn changedColumn = null;
        object changedValue = null;
        private void gridViewItem_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle == newRowHandle)
            {
                //if (this.ValidateData())
                //{

                changedValue = e.Value;
                changedColumn = e.Column;
                //}
                //else
                //{
                //    this.gridViewItem.DeleteRow(e.RowHandle);
                //}
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
            if (this.gridControl1.FocusedView == this.gridViewPO && (CurrentRow == null || gridViewPO.FocusedRowHandle < 0))
            {
                return;
            }

            if (this.gridControl1.FocusedView == this.gridViewItem && this.gridViewItem.FocusedRowHandle < int.MinValue)
            {
                return;
            }

            if (PartLoader.EnquireIsDeleteCurrentData() == false)
            {
                return;
            }

            if (this.gridControl1.FocusedView == this.gridViewPO)
            {
                if (CurrentRow.CreateDate != _nullValue)
                {
                    OeService.RemoveAirContainerPOInfo(CurrentRow.ID, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);
                }

                this._pos.PoItemContainerRelationsList.RemoveAll(o => o.PoId == CurrentRow.ID);
                this._pos.POContainerRelationsList.RemoveAll(o => o.PoId == CurrentRow.ID);
                this._pos.PoList.RemoveAll(o => o.ID == CurrentRow.ID);

                bindingSource1.RemoveCurrent();
                gridViewPO.RefreshData();
            }
            else
            {
                DeletePoItem(); return;
                //int[] rowHandlers = this.gridViewItem.GetSelectedRows();
                //foreach (int rowHandler in rowHandlers)
                //{
                //    this.gridViewItem.DeleteRow(rowHandler);
                //}

                //this.gridViewItem.DeleteRow(this._lastItemRowHandler);

                //bindingSource1.ResetBindings(false);
            }
        }

        private void gridViewItem_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete) DeletePoItem();
        }

        private void DeletePoItem()
        {
            DevExpress.XtraGrid.Views.Grid.GridView gv = null;
            for (int i = 1; i < gridControl1.Views.Count; i++)
            {
                if (gridControl1.Views[i].IsFocusedView)
                {
                    gv = (DevExpress.XtraGrid.Views.Grid.GridView)gridControl1.Views[i];
                    continue;
                }

            }

            if (gv == null) return;

            int[] selectedIndex = gv.GetSelectedRows();
            if (selectedIndex.Length <= 0) return;

            this.CurrentRow.IsDirty = true;

            gv.DeleteSelectedRows();

            //如果PoItem都删除了，则要删除它所有的关联关系
            this._pos.PoItemContainerRelationsList.RemoveAll(o => !ItemIsExist(o.ItemId));

            gv.RefreshData();

            this.bindingSource1.ResetBindings(false);
        }

        bool ItemIsExist(Guid id)
        {
            List<Guid> itemIds = new List<Guid>();
            foreach (var po in this._pos.PoList)
            {
                itemIds.AddRange(po.Items.Select(o => o.ID));
            }

            return itemIds.FindAll(o => o == id).Count > 0;
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

            List<AirBookingPOList> results = new List<AirBookingPOList>();

            Filter filter = (Filter)selected;

            string keyword = this.txtFind.Text.Trim().ToUpper();

            foreach (AirBookingPOList po in this._pos.PoList)
            {
                switch (filter)
                {
                    case Filter.Selected:
                        if (this._pos.POContainerRelationsList.FindAll(z=>z.PoId == po.ID && z.ContainerId == this._containerId).Count > 0)
                        {
                            //results.Add(po);
                        }
                        else
                        {
                            continue;
                        }
                        break;
                    case Filter.UnSelected:
                        if (this._pos.POContainerRelationsList.FindAll(z => z.PoId == po.ID && z.ContainerId == this._containerId).Count == 0)
                        {
                            //results.Add(po);
                        }
                        else
                        {
                            continue;
                        }
                        break;
                    default:
                        //results.Add(po);
                        break;
                }

                if (string.IsNullOrEmpty(keyword))
                {
                    results.Add(po);
                }
                else
                {
                    if (po.No.ToUpper().Contains(keyword)
                        || po.PODescription.ToUpper().Contains(keyword)
                        || po.BuyerName.ToUpper().Contains(keyword)
                        || po.VendorName.ToUpper().Contains(keyword)
                        || po.FinalDestination.ToUpper().Contains(keyword)
                        || po.CreateByName.ToUpper().Contains(keyword)
                        || po.CreateDate.ToString().Contains(keyword)
                        || po.InWarehouseDate.ToString().Contains(keyword)
                        || po.OrderDate.ToString().Contains(keyword))
                    {
                        results.Add(po);
                    }
                    else
                    {
                        bool foundInItems = false;
                        foreach (var item in po.Items)
                        {
                            if (item.Color.ToUpper().Contains(keyword)
                                || item.CreateByName.ToUpper().Contains(keyword)
                                || item.CreateDate.ToString().Contains(keyword)
                                || item.Description.ToUpper().Contains(keyword)
                                || item.HTSCode.ToUpper().Contains(keyword)
                                || item.No.ToUpper().Contains(keyword)
                                || item.Size.ToUpper().Contains(keyword)
                                || item.Units.ToString().Contains(keyword)
                                || item.Volume.ToString().Contains(keyword)
                                || item.Weight.ToString().Contains(keyword))
                            {
                                foundInItems = true;
                                break;
                            }
                        }

                        if (foundInItems)
                        {
                            results.Add(po);
                        }
                    }
                }
            }

            foreach (AirBookingPOList po in results)
            {
                bool originalPoDirty = po.IsDirty;
                po.Associated = this._pos.POContainerRelationsList.FindAll(o => o.PoId == po.ID
                    && o.ContainerId == this._containerId).Count > 0;
                //po.RelationID !=Guid.Empty && po.RelationID == this._containerId;

                foreach (AirPOItemList item in po.Items)
                {
                    bool originalDirty = item.IsDirty;

                    item.PropertyChanged -=new PropertyChangedEventHandler(item_PropertyChanged);
                    item.Associated = this._pos.PoItemContainerRelationsList.FindAll(o => o.ItemId == item.ID
                        && o.ContainerId == this._containerId).Count > 0;
                    //item.Associated = item.RelationID != Guid.Empty && item.RelationID == this._containerId;
                    item.PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);

                    item.IsDirty = originalDirty;
                }

                po.IsDirty = originalPoDirty;
            }

            this.bindingSource1.DataSource = results;
            this.bindingSource1.ResetBindings(false);

            this.ShowAllDetails();
        }

        void ShowAllDetails()
        {
            if (this.cbxExpandAll.Checked)
            {
                GridViewHelper.ExpandAllRows(this.gridViewPO,true);
            }
        }

        private void cbxExpandAll_CheckedChanged(object sender, EventArgs e)
        {
            this.ShowAllDetails();

            if (!this.cbxExpandAll.Checked)
            {
                GridViewHelper.ExpandAllRows(this.gridViewPO, false);
            }
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

        private void gridViewPO_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }
    }
}
