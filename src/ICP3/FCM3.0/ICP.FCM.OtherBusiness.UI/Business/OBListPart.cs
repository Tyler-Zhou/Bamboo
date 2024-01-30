using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.Service;
using DevExpress.Data;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI.SmartParts;
using System.Text.RegularExpressions;
using DevExpress.XtraBars;
using Microsoft.Practices.ObjectBuilder;
using ICP.Common.ServiceInterface.Client;
using ICP.FCM.OtherBusiness.ServiceInterface;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.UI.Common.Parts;
using ICP.OA.ServiceInterface.DataObjects;

namespace ICP.FCM.OtherBusiness.UI.Business
{
    /// <summary>
    /// 其他业务新增类型
    /// </summary>
    public enum AddType
    {
        /// <summary>
        /// 其他业务新增
        /// </summary>
        OtherBusiness,
        /// <summary>
        /// 新增订单
        /// </summary>
        OtherBusinessOrder
    }
    [ToolboxItem(false)]
    public partial class OBListPart : BaseListPart
    {
        #region 服务注入

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public OBExportPrintHelper OBExportPrintHelper { get; set; }

        [ServiceDependency]
        public ICP.FCM.OtherBusiness.ServiceInterface.IOtherBusinessService OBService { get; set; }

        [ServiceDependency]
        public ICP.FAM.ServiceInterface.IFinanceClientService finClientService { get; set; }

        [ServiceDependency]
        public ICP.FCM.Common.ServiceInterface.IFCMCommonService fcmCommonService { get; set; }

        [ServiceDependency]
        public ICP.FCM.Common.ServiceInterface.IFCMCommonClientService fcmCommonClientService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ExportUIHelper ExportUIHelper { get; set; }

        #endregion

        public OBListPart()
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
            this.DataSource = new List<OtherBusinessList>();
            _shown = true;

        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }
        private void InitControls()
        {
            Utility.ShowGridRowNo(this.gvMain);
            //订单状态
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OBOrderState>> orderStates = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OBOrderState>(LocalData.IsEnglish);
            foreach (var item in orderStates)
            {
                rcmState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
        }
        private void SetCnText()
        {
            colNO.Caption = "业务号";
            colIsValid.Caption = "是否有效";
            colState.Caption = "状态";
            colEta.Caption = "ETA";
            colEtd.Caption = "ETD";
            colCustomerName.Caption = "客户";
            colShipperName.Caption = "发货人";
            colConsigneeName.Caption = "收货人";
            colFeta.Caption = "FETA";
            colHblno.Caption = "HBL No";
            colMblno.Caption = "MBL No";
            colNotifyPartyName.Caption = "通知人";
            colFinalDestinationName.Caption = "交货地";
            colConsigneeName.Caption = "发货人";
            colShipperName.Caption = "收货人";
            colCarrierName.Caption = "船东";
            colAgengofCarrierName.Caption = "代理人";
            colPolName.Caption = "起运港";
            colPodName.Caption = "目的港";
            colVesselVoyage.Caption = "船名航次";
        }
        #region IListPart成员

        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;
        public override event CancelEventHandler CurrentChanging;

        public override object Current
        {
            get { return this.bsOBList.Current; }
        }
        protected OtherBusinessList CurrentRow
        {
            get { return Current as OtherBusinessList; }
        }
        public override object DataSource
        {
            get
            {
                return bsOBList.DataSource;
            }
            set
            {
                string message = string.Empty;
                message = LocalData.IsEnglish ? "0 records found" : "查询到0条记录";
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), message);
                bsOBList.DataSource = value;
                List<OtherBusinessList> list = value as List<OtherBusinessList>;
                if (list == null || list.Count == 0)
                {
                    bsOBList.DataSource = typeof(OtherBusinessList);
                }

                else
                {
                    if (CurrentChanged != null)
                    {
                        CurrentChanged(this, Current);
                    }

                    this.SetColumnsWidth();


                    if (this._shown)
                    {
                        if (LocalData.IsEnglish) { message = string.Format("{0} records found", list.Count); }
                        else { message = string.Format("查询到 {0} 条记录", list.Count); }
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), message);
                        bsOBList.DataSource = list;
                        bsOBList.ResetBindings(false);
                    }
                }
            }
        }

        public AddType addType
        {
            get;
            set;
        }


        #endregion



        void SetColumnsWidth()
        {
            gvMain.BestFitColumns();
        }
        static public string GetLineNo(OtherBusinessList current)
        {
            if (current == null || string.IsNullOrEmpty(current.NO))
            {
                return string.Empty;
            }
            else if (current.NO.Length <= 4)
            {
                return (LocalData.IsEnglish ? ":" : "：") + current.NO;
            }
            else
            {
                return (LocalData.IsEnglish ? ":" : "：") + current.NO.Substring(current.NO.Length - 4, 4);
            }
        }
        public override void Refresh(object items)
        {
            List<OtherBusinessList> list = this.DataSource as List<OtherBusinessList>;
            if (list == null) return;
            List<OtherBusinessList> newLists = items as List<OtherBusinessList>;

            foreach (var item in newLists)
            {
                OtherBusinessList tager = list.Find(delegate(OtherBusinessList jItem) { return item.ID == jItem.ID; });
                if (tager == null) continue;

                Utility.CopyToValue(item, tager, typeof(OtherBusinessList));
            }
            bsOBList.ResetBindings(false);
        }

        public override void RemoveItem(int index)
        {
            bsOBList.RemoveAt(index);
        }

        public override void RemoveItem(object item)
        {
            bsOBList.Remove(item);
        }
        #region GRID EVENTS
        private void gvMain_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && CurrentRow != null)
                Workitem.Commands[OBCommandConstants.Command_EditData].Execute();
        }
        public new event KeyEventHandler KeyDown;

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && CurrentRow != null)
            {
                Workitem.Commands[OBCommandConstants.Command_EditData].Execute();
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
                Workitem.Commands[OBCommandConstants.Command_ShowSearch].Execute();
            }
        }

        private void gvMain_DoubleClick(object sender, EventArgs e)
        {
            if (CurrentRow != null)
            {
                Workitem.Commands[OBCommandConstants.Command_EditData].Execute();
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
            OtherBusinessList list = gvMain.GetRow(e.RowHandle) as OtherBusinessList;
            if (list == null) return;

            if (list.IsValid == false)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Disabled);
            }
            else if (list.State == OBOrderState.NewOrder)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.NewLine);
            }
            else if (list.State == OBOrderState.MBLConfirmed)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Error);
            }
        }

        private void bsOBList_PositionChanged(object sender, EventArgs e)
        {
            Workitem.State[OrderStateConstants.CurrentRow] = CurrentRow;

            if (CurrentChanged != null) CurrentChanged(this, Current);
        }
        [CommandHandler(OrderCommandConstants.Command_AddOtherData)]
        public void Command_AddData(object sender, EventArgs args)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Workitem.State["AddType"] = this.addType;
                if (this.addType == AddType.OtherBusiness)
                {
                    PartLoader.ShowEditPart<Business.OBBaseEditPart>(Workitem, null, LocalData.IsEnglish ? "Add Business" : "新增业务信息", EditPartSaved);
                }
                else
                {
                    PartLoader.ShowEditPart<Business.OBOrderBaseEditPart>(Workitem, null, LocalData.IsEnglish ? "Add Order" : "新增订单信息", EditPartSaved);
                }
            }
        }

        void EditPartSaved(object[] prams)
        {
            if (prams == null || prams.Length == 0) return;

            OtherBusinessInfo data = prams[0] as OtherBusinessInfo;
            List<OtherBusinessList> source = this.DataSource as List<OtherBusinessList>;
            if (source == null || source.Count == 0)
            {
                bsOBList.Add(data);
                bsOBList.ResetBindings(false);
            }
            else
            {
                OtherBusinessList tager = source.Find(delegate(OtherBusinessList item) { return item.ID == data.ID; });
                if (tager == null)
                {
                    bsOBList.Insert(0, data);
                    bsOBList.ResetBindings(false);
                }
                else
                {
                    Utility.CopyToValue(data, tager, typeof(OtherBusinessList));
                    bsOBList.ResetItem(bsOBList.IndexOf(tager));
                }

            }
            if (CurrentChanged != null) CurrentChanged(this, CurrentRow);


            this.SetColumnsWidth();
        }
        #endregion


        #region button events
        //[CommandHandler(OrderCommandConstants.Command_AddData)]

        #region 复制
        [CommandHandler(OrderCommandConstants.Command_CopyData)]
        public void Command_CopyData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {

                CurrentRow.EditMode = EditMode.Copy;
                Workitem.State["AddType"] = this.addType;
                if (this.addType == AddType.OtherBusiness)
                {
                    PartLoader.ShowEditPart<Business.OBBaseEditPart>(Workitem, CurrentRow, LocalData.IsEnglish ? "Edit Business" : "编辑业务信息", EditPartSaved,
                        OrderCommandConstants.Command_EditData + CurrentRow.ID.ToString());
                }
                else
                {
                    PartLoader.ShowEditPart<Business.OBOrderBaseEditPart>(Workitem, CurrentRow, LocalData.IsEnglish ? "Edit Business" : "编辑业务信息", EditPartSaved,
                        OrderCommandConstants.Command_EditData + CurrentRow.ID.ToString());
                }
            }
        }

        #endregion

        #region 编辑
        [CommandHandler(OrderCommandConstants.Command_EditData)]
        public void Command_EditData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null || CurrentRow.IsNew) return;

                CurrentRow.EditMode = EditMode.Edit;
                Workitem.State["AddType"] = this.addType;
                if (this.addType == AddType.OtherBusiness)
                {
                    PartLoader.ShowEditPart<Business.OBBaseEditPart>(Workitem, CurrentRow, LocalData.IsEnglish ? "Edit Business" : "编辑业务信息", EditPartSaved,
                        OrderCommandConstants.Command_EditData + CurrentRow.ID.ToString());
                }
                else
                {
                    PartLoader.ShowEditPart<Business.OBOrderBaseEditPart>(Workitem, CurrentRow, LocalData.IsEnglish ? "Edit Business" : "编辑业务信息", EditPartSaved,
                       OrderCommandConstants.Command_EditData + CurrentRow.ID.ToString());
                }
            }
        }

        #endregion

        #region 刷新
        [CommandHandler(OrderCommandConstants.Command_RefreshData)]
        public void Command_RefreshData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    List<OtherBusinessList> blList = DataSource as List<OtherBusinessList>;
                    if (blList == null || blList.Count == 0) return;//无数据则返回

                    List<Guid> ids = new List<Guid>();
                    foreach (var item in blList)
                    {
                        ids.Add(item.ID);
                    }

                    List<OtherBusinessList> list = OBService.GetOtherBusinessListById(ids.ToArray(), LocalData.IsEnglish);
                    this.DataSource = list;
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
            }
        }

        #endregion

        #region 作废
        [CommandHandler(OrderCommandConstants.Command_CancelData)]
        public void Command_CancelData(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            try
            {

                bool isCancel = CurrentRow.IsValid;

                string message = string.Empty;
                if (isCancel)
                    message = LocalData.IsEnglish ? "Srue Cancel Current Booking?" : "你真的要取消这笔业务吗?";
                else
                    message = LocalData.IsEnglish ? "Srue Available Current Booking?" : "你真的要恢复这笔业务吗?";


                DialogResult dialogResult = DevExpress.XtraEditors.XtraMessageBox.Show(message,
                                                    LocalData.IsEnglish ? "Tip" : "提示",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {

                    //List<OBFeeList> info = this.OBFService.GetOrderFeeList(CurrentRow.ID,LocalData.IsEnglish);
                    //if (info.Count > 0)
                    //{
                    //    message = LocalData.IsEnglish ?
                    //        "The order has bring fees. It can not be cancelled."
                    //        :
                    //        "这笔业务不能取消，因为操作部门已经开始做单并产生了费用！";

                    //    XtraMessageBox.Show(message,
                    //        LocalData.IsEnglish ? "Warning" : "警告",
                    //         MessageBoxButtons.OK,
                    //          MessageBoxIcon.Warning);

                    //    return;
                    //}

                    SingleResult result = OBService.CancelOtherBusiness(CurrentRow.ID, isCancel, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate, LocalData.IsEnglish);

                    OtherBusinessList currentRow = CurrentRow;
                    currentRow.IsValid = !isCancel;
                    currentRow.ID = result.GetValue<Guid>("ID");
                    currentRow.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    this.bsOBList.ResetCurrentItem();

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Changed  State Successfully" : "更改状态成功");
                    if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "Changed  State Failed" : "更改状态失败") + ex.Message);
            }
        }
        #endregion

        #region 账单
        [CommandHandler(OrderCommandConstants.Command_Bill)]
        public void Command_Bill(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                //OperationCommonInfo operationCommonInfo = new OperationCommonInfo();
                //if (CurrentRow == null) return;
                //operationCommonInfo = fcmCommonService.GetOperationCommonInfo(CurrentRow.ID, OperationType.Other);
                //if (operationCommonInfo.Forms == null || operationCommonInfo.Forms.Count == 0 || operationCommonInfo == null)
                //{
                //    DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "TO DO" : "此业务没有参考单号.无法维护帐单.");
                //    return;
                //}
                //finClientService.ShowBillList(operationCommonInfo, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
                if (CurrentRow == null)
                {
                    return;
                }

                OperationCommonInfo operationCommonInfo = fcmCommonService.GetOperationCommonInfo(CurrentRow.ID, ICP.Framework.CommonLibrary.Common.OperationType.Other);
                if (operationCommonInfo != null)
                {
                    finClientService.ShowBillList(operationCommonInfo, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
                }
                else
                {
                    Utility.ShowMessage(LocalData.IsEnglish ? @"No found" : @"无对应的账单");
                }

            }
        }
        #endregion

        #region 核销单

        [CommandHandler(OBCommandConstants.Command_VerifiSheet)]
        public void Command_VerifiSheet(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null || CurrentRow.IsNew)
                {
                    return;
                }

                string no = CurrentRow.NO.Length <= 4 ? CurrentRow.NO : CurrentRow.NO.Substring(CurrentRow.NO.Length - 4, 4);
                string title = LocalData.IsEnglish ? "Verifi.Sheet" : "核销单" + ("-" + no);
                object[] data = new object[2];
                data[0] = CurrentRow.ID;
                data[1] = CurrentRow.NO;
                PartLoader.ShowEditPart<VerifiSheetEditPart>(Workitem,
                    data,
                    null,
                    title,
                    null,
                    OBCommandConstants.Command_VerifiSheet + CurrentRow.ID.ToString());
            }
        }

        #endregion

        #region 提货通知书
        /// <summary>
        /// 提货通知书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OrderCommandConstants.Command_PickUp)]
        public void Command_PickUp(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null)
                {
                    return;
                }

                //if (CurrentRow.MBLID == null || CurrentRow.MBLID.ToGuid() == Guid.Empty)
                //{
                //    Utility.ShowMessage(NativeLanguageService.GetText(this, "11091600001"));
                //    return;
                //}

                string title = LocalData.IsEnglish ? "ReleaseNotify" : "提货通知书";
                this.ExportUIHelper.ShowTruckEdit(this.CurrentRow.ID, this.CurrentRow,
                        this.Workitem, this.OBService,
                       Business.OBListPart.GetLineNo(this.CurrentRow));
            }
        }
        #endregion

        #region 打印
        [CommandHandler(OrderCommandConstants.Command_OpContact)]
        public void Command_PrintOrder(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null) return;

                try
                {
                    OBExportPrintHelper.PrintOBOrder(CurrentRow.ID);
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                }
            }
        }

        #region 利润表
        [CommandHandler(OrderCommandConstants.Command_Profit)]
        public void Command_Profit(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null) return;

                try
                {
                     OBExportPrintHelper.PrintOEBookingProfit(CurrentRow);

                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                }
            }
        }
        #endregion



        private ICP.Message.ServiceInterface.Message GetOperationInfo()
        {
            if (CurrentRow == null)
                return null;
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.UserProperties.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.Other;
            message.UserProperties.OperationId = CurrentRow.ID;
            message.UserProperties.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
            message.UserProperties.FormId = CurrentRow.ID;

            return message;
        }

        #endregion

        private void gvMain_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }
        #endregion

    }
}
