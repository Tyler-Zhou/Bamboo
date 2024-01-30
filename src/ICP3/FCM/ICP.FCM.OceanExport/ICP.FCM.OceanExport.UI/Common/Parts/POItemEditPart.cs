using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;

using Microsoft.Practices.CompositeUI;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using System.Reflection;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.OceanExport.ServiceInterface.CompositeObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.FCM.Common.ServiceInterface.DataObjects;

namespace ICP.FCM.OceanExport.UI
{
    /// <summary>
    /// PO编辑面板
    /// </summary>
    public partial class POItemEditPart : XtraUserControl, IChildPart
    {

        #region Fields
        /// <summary>
        /// 口岸公司
        /// </summary>
        Guid _CompanyID = Guid.Empty; 
        #endregion

        #region Service

        public WorkItem Workitem
        {
            get;
            set;
        }

        IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }

        #endregion

        #region 属性

        [State("IsOrderPO")]
        public bool IsOrderPO { get; set; }

        public void SetService(WorkItem workItem)
        {
            Workitem = workItem;
        }

        bool _isdDesignMode = true;
        public bool IsDesignMode
        { get { return _isdDesignMode; } set { _isdDesignMode = value; } }

        OceanBookingPOList CurrentRow
        {
            get
            {
                return bindingSource1.Current as OceanBookingPOList;
            }
        }

        #endregion

        #region init

        public POItemEditPart()
        {
            InitializeComponent();


            Disposed += delegate
            {
                gridControl1.DataSource = null;

                bindingSource1.DataSource = null;
                bindingSource1.Dispose();


                DataChanged = null;

                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
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


        void gridViewItem_BeforeLeaveRow(object sender, RowAllowEventArgs e)
        {
            if (!ValidateData())
            {
                e.Allow = false;
            }
        }

        #endregion

        #region IChildPart 成员

        public event EventHandler DataChanged;

        bool _isChanged = false;
        public bool IsChanged
        {
            get
            {
                if (_isChanged == false)
                {
                    List<OceanBookingPOList> source = bindingSource1.DataSource as List<OceanBookingPOList>;
                    if (source != null)
                    {
                        foreach (var item in source)
                        {
                            if (item.IsDirty) return true;
                            foreach (var items in item.Items)
                                if (items.IsDirty) return true;
                        }
                    }
                }

                return _isChanged;
            }
        }

        public bool ValidateData()
        {
            Validate();
            gridViewItem.CloseEditor();
            gridViewPO.CloseEditor();
            bindingSource1.EndEdit();
            bool isScrr = true;
            List<OceanBookingPOList> source = bindingSource1.DataSource as List<OceanBookingPOList>;

            if (source == null)
            {
                //界面尚未加载过
                return true;
            }
            foreach (OceanBookingPOList listItem in source)
            {
                if (listItem.Validate() == false) isScrr = false;

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

            return isScrr;
        }

        public void AfterSaved()
        {
            _isChanged = false;
        }

        public object DataSource { get { return bindingSource1.DataSource as List<OceanBookingPOList>; } }

        public void SetSource(object value)
        {
            if (value == null) return;

            _isChanged = false;
            bindingSource1.DataSource = value;
            bindingSource1.ResetBindings(false);
        }


        #endregion

        #region 新增

        private void barAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddData();
        }

        private void AddData()
        {
            OceanBookingPOList newData = new OceanBookingPOList();
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.State = FCMPOState.Processing;//正在处理
            newData.Items = new List<OceanPOItemList>();

            gridViewPO.ClearSorting();

            bindingSource1.Add(newData);
            _isChanged = true;

            gridViewPO.MoveLast();
        }

        const int newRowHandle = int.MinValue + 1;

        /// <summary>
        /// 新行的时候赋值 要先移动父行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewItem_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (CurrentRow != null && e.RowHandle == newRowHandle)
            {
                OceanBookingPOList current = CurrentRow;
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
                item.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                item.POID = current.ID;
                current.Items.Insert(0, item);
                current.IsDirty = true;

                bindingSource1.ResetBindings(false);
                SendKeys.Send("{Down}");
            }

            changedValue = null;
        }

        GridColumn changedColumn = null;
        object changedValue = null;
        private void gridViewItem_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            if (e.RowHandle == newRowHandle)
            {
                changedValue = e.Value;
                changedColumn = e.Column;
            }
        }

        private void gridViewPO_CellValueChanged(object sender, CellValueChangedEventArgs e)
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

        #region 删除

        private void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteData();
        }

        private void DeleteData()
        {
            if (CurrentRow == null || gridViewPO.FocusedRowHandle < 0) return;

            if (PartLoader.EnquireIsDeleteCurrentData() == false)
            {
                return;
            }

            if (CurrentRow.IsNew == false)
            {
                if (_CompanyID==Guid.Empty)
                    OceanExportService.RemoveOceanOrderPOInfo(CurrentRow.ID, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);
                else
                    OceanExportService.RemoveOceanOrderPOInfo(CurrentRow.ID, _CompanyID, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);
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
            GridView gv = null;
            for (int i = 1; i < gridControl1.Views.Count; i++)
            {
                if (gridControl1.Views[i].IsFocusedView)
                {
                    gv = (GridView)gridControl1.Views[i];
                    continue;
                }

            }

            if (gv == null) return;

            int[] selectedIndex = gv.GetSelectedRows();
            if (selectedIndex.Length <= 0) return;

            //if (Utility.EnquireIsDeleteCurrentData())
            //{
            gv.DeleteSelectedRows();
            gv.RefreshData();
            //}
            _isChanged = true;
        }

        #endregion

        #region 初始化数据

        public Guid OrderId { get; set; }

        public void InitData(Guid orderId)
        {
            if (orderId != Guid.Empty)
            {
                OrderId = orderId;
                List<OceanBookingPOList> _oceanBookingPOInfo = DataSource as List<OceanBookingPOList>;
                if (_oceanBookingPOInfo == null)
                {
                    if (_CompanyID==Guid.Empty)
                        _oceanBookingPOInfo = OceanExportService.GetOceanOrderPOList(OrderId);
                    else
                        _oceanBookingPOInfo = OceanExportService.GetOceanOrderPOList(OrderId,_CompanyID);

                    foreach (OceanBookingPOList po in _oceanBookingPOInfo)
                    {
                        foreach (var item in po.Items)
                        {
                            item.PropertyChanged +=item_PropertyChanged;
                        }
                    }

                    SetSource(_oceanBookingPOInfo);
                }
            }
            else
            {
                SetSource(new List<OceanBookingPOList>());
            }
        }
   
        void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            List<OceanBookingPOList> pos = DataSource as List<OceanBookingPOList>;
            if (pos == null)
            {
                return;
            }

            OceanPOItemList item = (OceanPOItemList)sender;

            if (item != null)
            {
                OceanBookingPOList po = pos.FirstOrDefault(o => o.ID == item.POID);
                if (po != null)
                {
                    po.IsDirty = true;
                }
            }
        }

        #endregion

        #region 保存

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="companyID">订单口岸ID</param>
        /// <returns></returns>
        public void BuildPOList(Guid orderId, Guid companyID)
        {
            //if (!ValidateData())
            //{
            //    //throw new Exception("验证数据不通过");
            //}
            //List<OceanBookingPOList> pos = DataSource as List<OceanBookingPOList>;
            //if (pos == null)
            //{
            //    return null;
            //}

            //List<OceanBookingPOList> changedItems = pos.FindAll(o => o.IsDirty);

            //if (orderId == Guid.Empty)
            //{
            //    changedItems = pos;
            //}

            //if (changedItems.Count == 0)
            //{
            //    return null;
            //}

            //List<POSaveRequest> commands = new List<POSaveRequest>();


            //foreach (OceanBookingPOList poItem in changedItems)
            //{
            //    //poItem
            //    List<Guid?> itemIDs = new List<Guid?>();
            //    List<string> itemNos = new List<string>(), itemDescriptions = new List<string>(), itemColors = new List<string>(), itemSizes = new List<string>(), itemHTSCodes = new List<string>();
            //    List<decimal> itemVolumes = new List<decimal>(), itemWeights = new List<decimal>();
            //    List<int> itemCartons = new List<int>(), itemUnits = new List<int>();
            //    List<DateTime?> itemUpdateDates = new List<DateTime?>();

            //    foreach (OceanPOItemList item in poItem.Items)
            //    {
            //        itemIDs.Add(item.ID);
            //        itemNos.Add(item.No);
            //        itemDescriptions.Add(item.Description);
            //        itemColors.Add(item.Color);
            //        itemSizes.Add(item.Size);
            //        itemHTSCodes.Add(item.HTSCode);

            //        itemVolumes.Add(item.Volume);
            //        itemWeights.Add(item.Weight);
            //        itemCartons.Add(item.Cartons);
            //        itemUnits.Add(item.Units);
            //        itemUpdateDates.Add(item.UpdateDate);
            //    }

            //    POSaveRequest request = new POSaveRequest();

            //    request.id = poItem.ID;
            //    request.companyID = companyID;
            //    request.relationID = poItem.RelationID;
            //    request.orderID = orderId;
            //    request.no = poItem.No;
            //    request.podc = poItem.PODescription;
            //    request.vendorID = poItem.VendorID;
            //    request.vendor = poItem.VendorName;
            //    request.buyerID = poItem.BuyerID;
            //    request.buyer = poItem.BuyerName;
            //    request.finalDestination = poItem.FinalDestination;
            //    request.inWarehouseDate = poItem.InWarehouseDate;
            //    request.orderDate = poItem.OrderDate;
            //    request.updateDate = poItem.UpdateDate;
            //    request.itemIDs = itemIDs.ToArray();
            //    request.itemNos = itemNos.ToArray();
            //    request.itemDescriptions = itemDescriptions.ToArray();
            //    request.itemColors = itemColors.ToArray();
            //    request.itemSizes = itemSizes.ToArray();
            //    request.itemVolumes = itemVolumes.ToArray();
            //    request.itemWeights = itemWeights.ToArray();
            //    request.itemCartons = itemCartons.ToArray();
            //    request.itemUnits = itemUnits.ToArray();
            //    request.itemHTSCodes = itemHTSCodes.ToArray();
            //    request.saveByID = LocalData.UserInfo.LoginID;
            //    request.itemUpdateDates = itemUpdateDates.ToArray();

            //    //HierarchyManyResult result = this.OeService.SaveOceanOrderPOInfo(request);
            //    //results.Add(result);

            //    request.AddInvolvedObject(poItem);
            //    commands.Add(request);
            //}



            //return commands;
        }

        public void RefreshUI()
        {
            //foreach (POSaveRequest request in list)
            //{
            //    HierarchyManyResult result = request.HierarchyManyResult;
            //    List<OceanBookingPOList> pos = request.UnBoxInvolvedObject<OceanBookingPOList>();
            //    for (int i = 0; i < pos.Count; i++)
            //    {
            //        pos[i].ID = result.GetValue<Guid>("POID");
            //        pos[i].RelationID = result.GetValue<Guid>("RelationID");
            //        pos[i].UpdateDate = result.GetValue<DateTime?>("UpdateDate");

            //        for (int j = 0; j < pos[i].Items.Count; j++)
            //        {
            //            pos[i].Items[j].ID = result.Childs[j].GetValue<Guid>("ItemID");
            //            pos[i].Items[j].RelationID = result.Childs[j].GetValue<Guid>("RelationID");
            //            pos[i].Items[j].POID = result.Childs[j].GetValue<Guid>("POID");
            //            pos[i].Items[j].UpdateDate = result.Childs[j].GetValue<DateTime?>("UpdateDate");
            //            pos[i].Items[j].IsDirty = false;
            //        }

            //        pos[i].IsDirty = false;
            //    }

            //    //this.SetSource(pos);
            //}
            //AfterSaved();
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
