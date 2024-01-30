using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using System.Reflection;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;

namespace ICP.FCM.OceanImport.UI
{

    public partial class POItemEditPart : DevExpress.XtraEditors.XtraUserControl, IChildPart
    {
        #region Fields
        Guid _CompanyID = Guid.Empty;
        #endregion

        #region Service
        [ServiceDependency]
       public WorkItem Workitem
        {
            get;
            set;
        }

        IOceanImportService OceanImportService { get { return ServiceClient.GetService<IOceanImportService>(); } }

        #endregion

        [State("IsOrderPO")]
        public bool IsOrderPO { get; set; }

        #region 属性

        bool _isdDesignMode = true;
        public bool IsDesignMode
        { get { return _isdDesignMode; } set { _isdDesignMode = value; } }

        OceanImportPOList CurrentRow
        {
            get
            {
                return bindingSource1.Current as OceanImportPOList;
            }
        }

        #endregion

        #region init

        public POItemEditPart()
        {
            InitializeComponent();
            
            this.Disposed += delegate {
                this.gridControl1.DataSource = null;
                bindingSource1.DataSource = null;
                this.bindingSource1.Dispose();
                this.gridViewItem.BeforeLeaveRow -= this.gridViewItem_BeforeLeaveRow;
                this.gridViewItem.CellValueChanged -= this.gridViewItem_CellValueChanged;
                this.gridViewItem.CellValueChanging -= this.gridViewItem_CellValueChanging;
                this.gridViewItem.Click -= this.gridViewItem_Click;
                this.gridViewItem.KeyDown -= this.gridViewItem_KeyDown;
                this.gridViewItem.ShowingEditor -= this.gridViewItem_ShowingEditor;
                this.gridViewPO.CellValueChanged -= this.gridViewPO_CellValueChanged;

                
                DataChanged = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            this.Load += new EventHandler(POItemEditPart_Load);
        }

        public void SetCnText()
        {
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
            colUnits.Caption = "数量";
            colVendorName.Caption = "卖主";
            colVolume.Caption = "体积";
            colWeight.Caption = "重量";
            gridViewItem.NewItemRowText = "点击这里以新增一行.选中行首按下Delete键可以删除一行.";
            colHTSCode.Caption = "海关编码";
            colNo.Caption = "订单号";
            colItemNo.Caption = "产品编号";

            barAdd.Caption = "新增(&N)";
            barDelete.Caption = "删除(&R)";
        }

        void POItemEditPart_Load(object sender, EventArgs e)
        {
            if (IsDesignMode) return;
            InitControls();

            this.gridViewItem.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(gridViewItem_BeforeLeaveRow);
        }

        void gridViewItem_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            if (!this.ValidateData())
            {
                e.Allow = false;
            }
        }

        private void InitControls()
        {

        }

        #endregion

        #region IChildPart 成员

        public event EventHandler DataChanged;

        bool _isChanged = false;
        public bool IsChanged
        {
            get
            {
                if (_isChanged)
                {
                    return true;
                }

                List<OceanImportPOList> source = bindingSource1.DataSource as List<OceanImportPOList>;
                if (source != null)
                {
                    foreach (var item in source)
                    {
                        if (item.IsDirty) return true;
                        foreach (var items in item.Items)
                            if (items.IsDirty) return true;
                    }
                }

                return _isChanged; 
            }
        }

        public bool ValidateData()
        {
            this.Validate();
            this.bindingSource1.EndEdit();
            bool isScrr = true;
            List<OceanImportPOList> source = bindingSource1.DataSource as List<OceanImportPOList>;
            foreach (OceanImportPOList listItem in source)
            {
                if (!listItem.Validate())
                {
                    isScrr = false;
                }      
            }

            return isScrr;
        }

        public void AfterSaved()
        {
            _isChanged = false;
        }

        public object DataSource { get { return bindingSource1.DataSource as List<OceanImportPOList>; } }

        public void SetSource(object value)
        {
            if (value == null) return;

            _isChanged = false;
            this.bindingSource1.DataSource = value;
            this.bindingSource1.ResetBindings(false);
        }

        public void SetService(WorkItem workitem)
        {
            Workitem = workitem;
        }

        #endregion

        #region 事件

        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddData();
        }

        private void AddData()
        {
            OceanImportPOList newData = new OceanImportPOList();
            newData.CreateDate = DateTime.Now;
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.State = FCMPOState.Processing;//正在处理
            newData.Items = new List<OceanPOItemList>();
            bindingSource1.Add(newData);
            _isChanged = true;

            this.gridViewPO.MoveLast();
        }

        private void barDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteData();
        }

        private void DeleteData()
        {
            if (CurrentRow == null || gridViewPO.FocusedRowHandle < 0) return;

            if (Utility.EnquireIsDeleteCurrentData() == false)
            {
                return;
            }

            if (CurrentRow.IsNew == false)
            {
                if (_CompanyID==Guid.Empty)
                    OceanImportService.RemoveOIOrderPOInfo(CurrentRow.ID, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);
                else
                    OceanImportService.RemoveOIOrderPOInfo(CurrentRow.ID,_CompanyID, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);
            }

            bindingSource1.RemoveCurrent();
            gridViewPO.RefreshData();
        }

        private void gridViewItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeletePoItem();
            }
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


            gv.DeleteSelectedRows();
            gv.RefreshData();

            _isChanged = true;
        }

        private void gridViewItem_Click(object sender, EventArgs e)
        {

        }

        const int newRowHandle = int.MinValue + 1;

        /// <summary>
        /// 新行的时候赋值
        /// TODO: 要先移动父行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewItem_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (CurrentRow != null && e.RowHandle == newRowHandle)
            {
                OceanImportPOList current = CurrentRow;
                if (current.Items == null)
                {
                    current.Items = new List<OceanPOItemList>();
                }

                OceanPOItemList item = new OceanPOItemList();

                PropertyInfo tp = item.GetType().GetProperty(e.Column.FieldName);
                if (tp != null)
                {
                    changedValue = Convert.ChangeType(changedValue, tp.PropertyType);
                    tp.SetValue(item, changedValue, null);
                }

                item.CreateByID = LocalData.UserInfo.LoginID;
                item.CreateByName = LocalData.UserInfo.LoginName;
                item.CreateDate = DateTime.Now;
                item.POID = current.ID;
                current.Items.Insert(0, item);
                current.IsDirty = true;

                bindingSource1.ResetBindings(false);
                SendKeys.Send("{Down}");
            }

            changedValue = null;
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

        private void gridViewPO_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (DataChanged != null)
            {
                DataChanged(this, null);
            }
            _isChanged = true;
        }

        /// <summary>
        /// 用于控制点选子GridView时，更新父GridView的当前行
        /// </summary>
        private void gridViewItem_ShowingEditor(object sender, CancelEventArgs e)
        {
            Point pt = gridControl1.PointToClient(MousePosition);
            GridHitInfo hitInfo = gridViewPO.CalcHitInfo(pt);
            if (hitInfo != null && hitInfo.RowHandle >= 0)
            {
                gridViewPO.FocusedRowHandle = hitInfo.RowHandle;
            }
        }

        #endregion

        #region 保存PO

        public Guid OrderId { get; set; }

        public void InitData(Guid orderId)
        {
            if (orderId != Guid.Empty)
            {
                this.OrderId = orderId;
                List<OceanImportPOList> _oceanBookingPOInfo = this.DataSource as List<OceanImportPOList>;
                if (_oceanBookingPOInfo == null)
                {
                    _oceanBookingPOInfo = this.OceanImportService.GetOIOrderPOList(this.OrderId);
                }

                this.SetSource(_oceanBookingPOInfo);
            }
        }

        /// <summary>
        /// 另存为的时候需要
        /// </summary>
        public void ResetPrimaryKeys()
        {
            List<OceanImportPOList> list = this.DataSource as List<OceanImportPOList>;
            if (list != null)
            {
                list.ForEach(o => o.ID = Guid.NewGuid());
            }
        }

        /// <summary>
        /// 构建需保存PO列表
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="companyID"></param>
        /// <returns></returns>
        public List<POSaveRequest> BuildPoList(Guid orderId, Guid companyID)
        {
            this.gridViewItem.CloseEditor();
            this.gridViewPO.CloseEditor();
            List<OceanImportPOList> pos = this.DataSource as List<OceanImportPOList>;
            if (pos == null)
            {
                return null;
            }

            List<OceanImportPOList> changedItems = pos.FindAll(o => o.IsDirty);

            if (orderId == Guid.Empty)
            {
                changedItems = pos;
            }

            if (changedItems.Count == 0)
            {
                return null;
            }

            List<POSaveRequest> commands = new List<POSaveRequest>();


            foreach (OceanImportPOList poItem in changedItems)
            {
                //poItem
                List<Guid?> itemIDs = new List<Guid?>();
                List<string> itemNos = new List<string>(), itemDescriptions = new List<string>(), itemColors = new List<string>(), itemSizes = new List<string>(), itemHTSCodes = new List<string>();
                List<decimal> itemVolumes = new List<decimal>(), itemWeights = new List<decimal>();
                List<int> itemCartons = new List<int>(), itemUnits = new List<int>();
                List<DateTime?> itemUpdateDates = new List<DateTime?>();

                foreach (OceanPOItemList item in poItem.Items)
                {
                    itemIDs.Add(item.ID);
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
                request.companyID = companyID;
                request.relationID = poItem.RelationID;
                request.orderID = orderId;
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

                changedItems.ForEach(o => request.AddInvolvedObject(o));
                commands.Add(request);
            }



            return commands;
        }

        public void RefreshUI(List<POSaveRequest> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                HierarchyManyResult result = list[i].HierarchyManyResult;
                List<OceanImportPOList> pos = list[i].UnBoxInvolvedObject<OceanImportPOList>();

                pos[i].ID = result.GetValue<Guid>("POID");
                pos[i].RelationID = result.GetValue<Guid>("RelationID");
                pos[i].UpdateDate = result.GetValue<DateTime?>("UpdateDate");

                for (int j = 0; j < pos[i].Items.Count; j++)
                {
                    pos[i].Items[j].ID = result.Childs[j].GetValue<Guid>("ItemID");
                    pos[i].Items[j].RelationID = result.Childs[j].GetValue<Guid>("RelationID");
                    pos[i].Items[j].POID = result.Childs[j].GetValue<Guid>("POID");
                    pos[i].Items[j].UpdateDate = result.Childs[j].GetValue<DateTime?>("UpdateDate");
                    pos[i].Items[j].IsDirty = false;
                }

                pos[i].IsDirty = false;
                
            }
           
            this.AfterSaved();
        }

        #endregion

        /// <summary>
        /// 公司发生改变时
        /// </summary>
        /// <param name="companyID"></param>
        public void SetCompanyID(Guid companyID)
        {
            if (companyID != Guid.Empty && _CompanyID != companyID)
            {
                _CompanyID = companyID;
            }
        }
    }
}
