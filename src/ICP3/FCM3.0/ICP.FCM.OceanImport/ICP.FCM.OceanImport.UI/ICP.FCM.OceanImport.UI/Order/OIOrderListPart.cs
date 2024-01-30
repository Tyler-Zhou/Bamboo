using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Common.ServiceInterface.Client;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.ObjectBuilder;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.FCM.OceanImport.UI.Common;
using ICP.Framework.ClientComponents.Controls;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.OA.ServiceInterface.DataObjects;

namespace ICP.FCM.OceanImport.UI
{
    [ToolboxItem(false)]
    public partial class OIOrderListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IOceanImportService oiService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public OceanImportPrintHelper OceanImportPrintHelper { get; set; }

        #endregion

        #region Init

        public OIOrderListPart()
        {
            InitializeComponent();
            this.Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {


        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                InitControls();
            }
        }

        private void InitControls()
        {

            ////OIOrderState
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OIOrderState>> orderStates = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OIOrderState>(LocalData.IsEnglish);
            rcmbState.Properties.BeginUpdate();
            foreach (var item in orderStates)
            {
                rcmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            rcmbState.Properties.EndUpdate();
        }

        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }

        protected OceanOrderList CurrentRow
        {
            get { return Current as OceanOrderList; }
        }

        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                this.gcMain.BeginUpdate();
                this.gvMain.BeginUpdate();
                this.gvMain.BeginDataUpdate();

                bsList.DataSource = value;

                #region 对一些枚举类型，获取对应语言的描述信息

                List<OceanOrderList> list = value as List<OceanOrderList>;

                #endregion

                bsList.ResetBindings(false);
                if (CurrentChanged != null)
                {
                    CurrentChanged(this, Current);
                }
                gvMain.BestFitColumns();

                this.gvMain.EndDataUpdate();
                this.gvMain.EndUpdate();
                this.gcMain.EndUpdate();

                string message = string.Empty;

                if (list.Count > 0)
                {
                    if (LocalData.IsEnglish)
                    {
                        message = string.Format("{0} record found", list.Count);
                    }
                    else
                    {
                        message = string.Format("查询到 {0} 条记录", list.Count);
                    }
                }
                else
                {
                    message = LocalData.IsEnglish ? "Nothing found!" : "没有查询到任何结果。";
                }

                if (list.Count.ToString().Length == 1)
                {
                    gvMain.IndicatorWidth = 30;
                }
                else
                {
                    gvMain.IndicatorWidth = list.Count.ToString().Length * 17;
                }
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), message);
            }
        }

        public override void Refresh(object items)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                //List<OceanOrderList> list = this.DataSource as List<OceanOrderList>;
                //if (list == null) return;
                //List<OceanOrderList> newLists = items as List<OceanOrderList>;

                //foreach (var item in newLists)
                //{
                //    OceanOrderList tager = list.Find(delegate(OceanOrderList jItem) { return item.ID == jItem.ID; });
                //    if (tager == null) continue;

                //    Utility.CopyToValue(item, tager, typeof(OceanOrderList));
                //}
                bsList.ResetBindings(false);
            }
        }

        public override void RemoveItem(int index)
        {
            bsList.RemoveAt(index);
        }

        public override void RemoveItem(object item)
        {
            bsList.Remove(item);
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SelectedHandler Selected;

        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;
        public override event CancelEventHandler CurrentChanging;

        #endregion

        #region GridView Event

        private void gvMain_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Clicks == 2 && CurrentRow != null)
                Workitem.Commands[OIOrderCommandConstants.Command_EditData].Execute();
        }
        public new event KeyEventHandler KeyDown;
        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && CurrentRow != null)
            {
                Workitem.Commands[OIOrderCommandConstants.Command_EditData].Execute();
            }
            else if (this.KeyDown != null
                && e.KeyCode == Keys.F5
                && this.gvMain.FocusedColumn != null
                && this.gvMain.FocusedValue != null)
            {
                string text = gvMain.GetFocusedDisplayText();
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add(gvMain.FocusedColumn.FieldName, text);
                this.KeyDown(keyValue, null);
            }
            else if (e.KeyCode == Keys.F6)
            {
                Workitem.Commands[OIOrderCommandConstants.Command_ShowSearch].Execute();
            }
        }

        private void bsMainList_PositionChanged(object sender, EventArgs e)
        {
            Workitem.State[OIOrderStateConstants.CurrentRow] = CurrentRow;

            if (CurrentChanged != null)
            {
                CurrentChanged(this, Current);
                Workitem.Commands[OIOrderCommandConstants.Command_FaxEmail].Execute();
                //Workitem.Commands[OIOrderCommandConstants.Command_Memo].Execute();
                Workitem.Commands[OIOrderCommandConstants.Command_Document].Execute();
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
            OceanOrderList list = gvMain.GetRow(e.RowHandle) as OceanOrderList;
            if (list == null) return;

            if (list.IsValid == false)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Disabled);
            }
            else if (list.State == OIOrderState.NewOrder)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.NewLine);
            }
            else if (list.State == OIOrderState.Rejected)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Error);
            }
            else if (list.State == OIOrderState.Release)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Confirmed);
            }
        }

        #endregion

        #region Workitem Common

        #region 数据操作
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIOrderCommandConstants.Command_AddData)]
        public void Command_AddData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                
                Utility.ShowEditPart<OIOrderBaseEdit>(Workitem, null, string.Empty, EditPartSaved);
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="prams"></param>
        void EditPartSaved(object[] prams)
        {

            if (prams == null || prams.Length == 0) return;

            OceanOrderInfo data = prams[0] as OceanOrderInfo;
            List<OceanOrderList> source = this.DataSource as List<OceanOrderList>;
            if (source == null || source.Count == 0)
            {
                bsList.Add(data);
                bsList.ResetBindings(false);
            }
            else
            {
                OceanOrderList tager = source.Find(delegate(OceanOrderList item) { return item.ID == data.ID; });
                if (tager == null)
                {
                    bsList.Insert(0, data);
                    bsList.ResetBindings(false);
                }
                else
                {
                    Utility.CopyToValue(data, tager, typeof(OceanOrderList));
                    bsList.ResetItem(bsList.IndexOf(tager));
                }

            }
            if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
        }
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIOrderCommandConstants.Command_CopyData)]
        public void Command_CopyData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null || Utility.GuidIsNullOrEmpty(CurrentRow.ID)) return;
                CurrentRow.EditMode = EditMode.Copy;

                //OceanOrderInfo newData = oiService.GetOIOrderInfo(CurrentRow.ID);               
                //newData.ID = Guid.Empty;
                //newData.State = OIOrderState.NewOrder;
                //newData.RefNo = string.Empty;
                //newData.CreateByID = LocalData.UserInfo.LoginID;
                //newData.CreateByName = LocalData.UserInfo.LoginName;
                //newData.CreateDate = DateTime.Now;
                //newData.SalesID = LocalData.UserInfo.LoginID;
                //newData.SalesName = LocalData.UserInfo.LoginName;

                Utility.ShowEditPart<OIOrderBaseEdit>(Workitem, CurrentRow, string.Empty, EditPartSaved);
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIOrderCommandConstants.Command_CancelData)]
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

                bool isOK = Utility.ShowResultMessage(message); ;

                if (isOK)
                {
                    SingleResult result = oiService.CancelOIOrder(CurrentRow.ID, isCancel, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);

                    OceanOrderList currentRow = CurrentRow;
                    currentRow.IsValid = !isCancel;
                    currentRow.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    bsList.ResetCurrentItem();

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Changed Order State Successfully" : "更改订单状态成功");
                    if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "Changed Order State Failed" : "更改订单状态失败") + ex.Message);
            }
        }

        [CommandHandler(OIOrderCommandConstants.Command_Print)]
        public void Command_Print(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null) return;
                ICP.Message.ServiceInterface.Message operationInfo = GetOperationInfo();
                try
                {
                    IReportViewer viewer = OceanImportPrintHelper.PrintOIOrder(CurrentRow.ID, operationInfo);
                    if (viewer == null)
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                }
            }
        }

        private ICP.Message.ServiceInterface.Message GetOperationInfo()
        {
            if (CurrentRow == null)
                return null;
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.UserProperties.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.AirImport;
            message.UserProperties.OperationId = CurrentRow.ID;
            message.UserProperties.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
            message.UserProperties.FormId = CurrentRow.ID;

            return message;
        }

        [CommandHandler(OIOrderCommandConstants.Command_EditData)]
        public void Command_EditData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null)
                {
                    return;
                }

                CurrentRow.EditMode = EditMode.Edit;
                Utility.ShowEditPart<OIOrderBaseEdit>(Workitem, CurrentRow, string.Empty, EditPartSaved);
            }
        }

        #endregion

        [CommandHandler(OIOrderCommandConstants.Command_RefreshData)]
        public void Command_RefreshData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    List<OceanOrderList> blList = DataSource as List<OceanOrderList>;
                    if (blList == null || blList.Count == 0) return;

                    List<Guid> ids = new List<Guid>();
                    foreach (var item in blList)
                    {
                        ids.Add(item.ID);
                    }

                    List<OceanOrderList> list = oiService.GetOIOrderListByIds(ids.ToArray());
                    this.DataSource = list;
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
            }
        }


        [CommandHandler(OIOrderCommandConstants.Command_SendEmail)]
        public void Command_SendEmail(object sender, EventArgs e)
        {
        }

        [CommandHandler(OIOrderCommandConstants.Command_ConfirmBooking)]
        public void Command_ConfirmBooking(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanOrderList currentRow = Current as OceanOrderList;
                if (currentRow == null) return;

                ConfirmBookingForm memoContentForm = Workitem.Items.AddNew<ConfirmBookingForm>();
                string title = LocalData.IsEnglish ? "Risk Clients" : "确认订舱";

                memoContentForm.LabelText = LocalData.IsEnglish ? "Memo for cancel" : "确认订舱备注";


                if (PartLoader.ShowDialog(memoContentForm, title) != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    SingleResultData result = oiService.ChangeOIOrderState(currentRow.ID, OIOrderState.BookingConfirmed, memoContentForm.Memo, LocalData.UserInfo.LoginID, currentRow.UpdateDate);
                    currentRow.State = OIOrderState.BookingConfirmed;
                    currentRow.UpdateDate = result.UpdateDate;
                    bsList.ResetCurrentItem();
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Disuse Successfully" : "确认订舱成功!");
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
            }
        }
        [CommandHandler(OIOrderCommandConstants.Command_ConfirmBookingShip)]
        public void Command_ConfirmBookingShip(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanOrderList currentRow = Current as OceanOrderList;
                if (currentRow == null) return;

                ConfirmBookingForm memoContentForm = Workitem.Items.AddNew<ConfirmBookingForm>();
                string title = LocalData.IsEnglish ? "Risk Clients" : "确认装船";
                memoContentForm.LabelText = LocalData.IsEnglish ? "Memo for Set to Risk Clients" : "确认装船备注";

                if (PartLoader.ShowDialog(memoContentForm, title) != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    SingleResultData result = oiService.ChangeOIOrderState(currentRow.ID, OIOrderState.LoadVoyage, memoContentForm.Memo, LocalData.UserInfo.LoginID, currentRow.UpdateDate);
                    currentRow.State = OIOrderState.LoadVoyage;
                    currentRow.UpdateDate = result.UpdateDate;
                    bsList.ResetCurrentItem();

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Disuse Successfully" : "确认装船成功!");
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
            }
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
