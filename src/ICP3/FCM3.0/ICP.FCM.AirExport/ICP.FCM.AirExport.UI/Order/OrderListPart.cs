using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.FCM.AirExport.ServiceInterface;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.AirExport.UI.Common;
using Microsoft.Practices.ObjectBuilder;
namespace ICP.FCM.AirExport.UI.Order
{
    [ToolboxItem(false)]
    public partial class OrderListPart : BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IAirExportService oeService { get; set; }

        //[ServiceDependency]
        //public IEMailClientService eMailClientService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public AirExportPrintHelper AirExportPrintHelper { get; set; }

        #endregion

        #region Init

        public OrderListPart()
        {
            InitializeComponent();

            this.Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };

            this.Load += new EventHandler(OrderListPart_Load);

            if (!LocalData.IsEnglish)
            {
                SetCnText();
            }
        }

        bool _shown = false;

        void OrderListPart_Load(object sender, EventArgs e)
        {
            this.DataSource = new List<AirOrderList>();

            _shown = true;
        }

        private void SetCnText()
        {
            colIsValid.Caption = "是否有效";
            colState.Caption = "状态";
            colRefNo.Caption = "业务号";
            colCustomerName.Caption = "客户";
            colShipperName.Caption = "发货人";
            colConsigneeName.Caption = "收货人";
            colPOLName.Caption = "起运港";
            colPODName.Caption = "目的港";
            //colBookingTypeDescription.Caption ="业务类型";
            //colClosingDate.Caption = "截关日";
            colSODate.Caption = "订舱日";
            colSalesName.Caption = "揽货人";
            colCreateDate.Caption = "创建日";
            colETD.Caption = "起航日";
            colETA.Caption = "到达日";

            colPlaceOfDeliveryName.Caption = "交货地";
            //colPODContact.Caption = "港后客服";
            colOperator.Caption = "订舱";
            colFiler.Caption = "文件";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }

        private void InitControls()
        {
            Utility.ShowGridRowNo(this.gvMain);
            //OrderState
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<AEOrderState>> orderStates = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<AEOrderState>(LocalData.IsEnglish);
            foreach (var item in orderStates)
            {
                rcmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
        }

        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return this.bsOrderList.Current; }
        }
        protected AirOrderList CurrentRow
        {
            get { return Current as AirOrderList; }
        }

        public override object DataSource
        {
            get
            {
                return bsOrderList.DataSource;
            }
            set
            {
                string message = string.Empty;
                message = LocalData.IsEnglish ? "0 records found" : "查询到0条记录";
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), message);
                bsOrderList.DataSource = value;
                List<AirOrderList> list = value as List<AirOrderList>;
                if (list == null || list.Count == 0)
                {
                    bsOrderList.DataSource = typeof(AirOrderList);
                }
                //else if (list != null)
                //{
                //    foreach (AirOrderList item in list)
                //    {
                //        item.BookingTypeDescription = EnumHelper.GetDescription<OEOperationType>(item.OEOperationType, LocalData.IsEnglish);
                //    }
                //}

                else
                {
                    if (CurrentChanged != null)
                    {
                        CurrentChanged(this, CurrentRow);
                    }

                    this.SetColumnsWidth();


                    if (this._shown)
                    {
                        if (LocalData.IsEnglish) { message = string.Format("{0} records found", list.Count); }
                        else { message = string.Format("查询到 {0} 条记录", list.Count); }
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), message);
                        bsOrderList.DataSource = list;
                        bsOrderList.ResetBindings(false);
                    }
                }
            }
        }

        public override void Refresh(object items)
        {
            List<AirOrderList> list = this.DataSource as List<AirOrderList>;
            if (list == null) return;
            List<AirOrderList> newLists = items as List<AirOrderList>;

            foreach (var item in newLists)
            {
                AirOrderList tager = list.Find(delegate(AirOrderList jItem) { return item.ID == jItem.ID; });
                if (tager == null) continue;

                Utility.CopyToValue(item, tager, typeof(AirOrderList));
            }
            bsOrderList.ResetBindings(false);
        }

        public override void RemoveItem(int index)
        {
            bsOrderList.RemoveAt(index);
        }

        public override void RemoveItem(object item)
        {
            bsOrderList.Remove(item);
        }

        //public override event ICP.Framework.ClientComponents.UIFramework.SelectedHandler Selected;

        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;
        public override event CancelEventHandler CurrentChanging;

        #endregion

        #region GridView Event

        private void gvMain_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && CurrentRow != null)
                Workitem.Commands[AEOrderCommandConstants.Command_EditData].Execute();
        }

        public new event KeyEventHandler KeyDown;

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && CurrentRow != null)
            {
                Workitem.Commands[AEOrderCommandConstants.Command_EditData].Execute();
            }
            else if (this.KeyDown != null
                && e.KeyCode == Keys.F5
                && this.gvMain.FocusedColumn != null
                && this.gvMain.FocusedValue != null)
            {
                string text = gvMain.GetFocusedDisplayText();//.GetDisplayText(treeMain.FocusedColumn);
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add(gvMain.FocusedColumn.FieldName, text);
                this.KeyDown(keyValue, null);
            }
            else if (e.KeyCode == Keys.F6)
            {
                Workitem.Commands[AEOrderCommandConstants.Command_ShowSearch].Execute();
            }


        }

        private void bsMainList_PositionChanged(object sender, EventArgs e)
        {
            Workitem.State[AEOrderStateConstants.CurrentRow] = CurrentRow;

            if (CurrentChanged != null)
            {
                CurrentChanged(this, CurrentRow);
                Workitem.Commands[AEOrderCommandConstants.Command_FaxEmail].Execute();
                //Workitem.Commands[AEOrderCommandConstants.Command_Memo].Execute();
                Workitem.Commands[AEOrderCommandConstants.Command_Document].Execute();
            }
        }

        private void gvMain_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            if (CurrentChanging != null)
            {
                CancelEventArgs ce = new CancelEventArgs();
                CurrentChanging(this, ce);
                e.Allow = !ce.Cancel;
            }
        }

        private void gvMain_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            AirOrderList list = gvMain.GetRow(e.RowHandle) as AirOrderList;
            if (list == null) return;

            if (list.IsValid == false)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Disabled);
            }
            else if (list.State == AEOrderState.NewOrder)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.NewLine);
            }
            else if (list.State == AEOrderState.Rejected)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Error);
            }

        }

        #endregion

        #region Workitem Common

        #region 数据操作

        [CommandHandler(AEOrderCommandConstants.Command_AddData)]
        public void Command_AddData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                PartLoader.ShowEditPart<Order.OrderBaseEditPart>(Workitem, null, string.Empty, EditPartSaved);
            }
        }

        void EditPartSaved(object[] prams)
        {
            if (prams == null || prams.Length == 0) return;

            AirOrderInfo data = prams[0] as AirOrderInfo;
            List<AirOrderList> source = this.DataSource as List<AirOrderList>;
            if (source == null || source.Count == 0)
            {
                bsOrderList.Add(data);
                bsOrderList.ResetBindings(false);
            }
            else
            {
                AirOrderList tager = source.Find(delegate(AirOrderList item) { return item.ID == data.ID; });
                if (tager == null)
                {
                    bsOrderList.Insert(0, data);
                    bsOrderList.ResetBindings(false);
                }
                else
                {
                    Utility.CopyToValue(data, tager, typeof(AirOrderList));
                    bsOrderList.ResetItem(bsOrderList.IndexOf(tager));
                }

            }
            if (CurrentChanged != null) CurrentChanged(this, CurrentRow);


            this.SetColumnsWidth();
        }

        void SetColumnsWidth()
        {
            gvMain.BestFitColumns();
        }

        [CommandHandler(AEOrderCommandConstants.Command_CopyData)]
        public void Command_CopyData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null || Utility.GuidIsNullOrEmpty(CurrentRow.ID)) return;

                CurrentRow.EditMode = EditMode.Copy;

                PartLoader.ShowEditPart<Order.OrderBaseEditPart>(Workitem, CurrentRow, string.Empty, EditPartSaved);
            }
        }

        [CommandHandler(AEOrderCommandConstants.Command_CancelData)]
        public void Command_CancelData(object sender, EventArgs e)
        {

            if (CurrentRow == null) return;

            try
            {
                bool isCancel = CurrentRow.IsValid;

                string message = string.Empty;
                if (isCancel)
                    message = LocalData.IsEnglish ? "Srue Cancel Current Booking?" : "你真的要取消这笔订单吗?";
                else
                    message = LocalData.IsEnglish ? "Srue Available Current Booking?" : "你真的要恢复这笔订单吗?";


                DialogResult dialogResult = DevExpress.XtraEditors.XtraMessageBox.Show(message,
                                                    LocalData.IsEnglish ? "Tip" : "提示",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    SingleResult result = oeService.CancelAirOrder(CurrentRow.ID, isCancel, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);

                    AirOrderList currentRow = CurrentRow;
                    currentRow.IsValid = !isCancel;
                    currentRow.ID = result.GetValue<Guid>("ID");
                    currentRow.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    bsOrderList.ResetCurrentItem();

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Changed Order State Successfully" : "更改订单状态成功");
                    if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "Changed Order State Failed" : "更改订单状态失败") + ex.Message);
            }
        }

        [CommandHandler(AEOrderCommandConstants.Command_Print)]
        public void Command_Print(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null) return;


                AirExportPrintHelper.PrintAEOrder(CurrentRow.ID);
            }
        }
        #region 编辑
        [CommandHandler(AEOrderCommandConstants.Command_EditData)]
        public void Command_EditData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null || CurrentRow.IsNew) return;

                CurrentRow.EditMode = EditMode.Edit;

                PartLoader.ShowEditPart<Order.OrderBaseEditPart>(Workitem, CurrentRow, string.Empty, EditPartSaved,
                    AEOrderCommandConstants.Command_EditData + CurrentRow.ID.ToString());


            }
        }
        #endregion
        #endregion
        #region 刷新
        [CommandHandler(AEOrderCommandConstants.Command_RefreshData)]
        public void Command_RefreshData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {

                try
                {
                    List<AirOrderList> blList = DataSource as List<AirOrderList>;
                    if (blList == null || blList.Count == 0) return;//无数据则返回

                    List<Guid> ids = new List<Guid>();
                    foreach (var item in blList)
                    {
                        ids.Add(item.ID);
                    }

                    List<AirOrderList> list = oeService.GetAirOrderListById(ids.ToArray());
                    this.DataSource = list;
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
            }
        }
        #endregion
        [CommandHandler(AEOrderCommandConstants.Command_ViewReason)]
        public void Command_ViewReason(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            DevExpress.XtraEditors.XtraMessageBox.Show(CurrentRow.Reason);
        }

        [CommandHandler(AEOrderCommandConstants.Command_SendEmail)]
        public void Command_SendEmail(object sender, EventArgs e)
        {

        }

        #endregion

        private void gvMain_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }
    }
}
