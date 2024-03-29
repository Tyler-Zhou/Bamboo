﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Common.ServiceInterface.Client;
using ICP.FCM.DomesticTrade.ServiceInterface;
using ICP.FCM.DomesticTrade.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.OA.ServiceInterface.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.ObjectBuilder;
using ICP.Framework.ClientComponents.Controls;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Message.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;

namespace ICP.FCM.DomesticTrade.UI.Order
{
    [ToolboxItem(false)]
    public partial class DTOrderListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IDomesticTradeService oeService { get; set; }

        [ServiceDependency]
        public ICP.Message.ServiceInterface.IClientMessageService eMailClientService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public DomesticTradePrintHelper DomesticTradePrintHelper { get; set; }

        [ServiceDependency]
        public ICP.Sys.ServiceInterface.IUserService userService { get; set; }
        #endregion

        #region Init

        public DTOrderListPart()
        {
            InitializeComponent();

            this.Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };

            this.Load += new EventHandler(DTOrderListPart_Load);

            if (!LocalData.IsEnglish)
            {
                SetCnText();
            }
        }

        bool _shown = false;

        void DTOrderListPart_Load(object sender, EventArgs e)
        {
            this.DataSource = new List<DTOrderList>();

            _shown = true;
        }

        private void SetCnText()
        {
            colState.Caption = "状态";
            colRefNo.Caption = "业务号";
            colCustomerName.Caption = "客户";
            colShipperName.Caption = "发货人";
            colConsigneeName.Caption = "收货人";
            colPlaceOfReceiptName.Caption = "收货地";
            colPOLName.Caption = "装货港";
            colPODName.Caption = "卸货港";
            colBookingTypeDescription.Caption = "业务类型";
            colSODate.Caption = "订舱日";
            colSalesName.Caption = "揽货人";
            colCreateDate.Caption = "创建日";
            colETD.Caption = "离港日";
            colETA.Caption = "到港日";

            colPlaceOfDeliveryName.Caption = "交货地";
            colPODContact.Caption = "港后客服";
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

            //OrderState
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<DTOrderState>> orderStates = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<DTOrderState>(LocalData.IsEnglish);
            foreach (var item in orderStates)
            {
                rcmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
        }

        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }
        protected DTOrderList CurrentRow
        {
            get { return Current as DTOrderList; }
        }

        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                bsList.DataSource = value;

                #region 对一些枚举类型，获取对应语言的描述信息

                //TODO: 装箱拆箱是消耗性能的
                List<DTOrderList> list = value as List<DTOrderList>;

                if (list != null)
                {
                    foreach (DTOrderList item in list)
                    {
                        item.BookingTypeDescription = EnumHelper.GetDescription<FCMOperationType>(item.DTOperationType, LocalData.IsEnglish);
                    }
                }

                #endregion

                bsList.ResetBindings(false);
                if (CurrentChanged != null)
                {
                    CurrentChanged(this, Current);
                }

                this.SetColumnsWidth();



                string message = string.Empty;

                if (list.Count > 0)
                {
                    if (LocalData.IsEnglish)
                    {
                        if (list.Count > 1)
                        {
                            message = string.Format("{0} records found", list.Count);
                        }
                        else
                        {
                            message = string.Format("{0} record found", list.Count);
                        }
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

                if (this._shown)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), message);
                }
            }
        }

        public override void Refresh(object items)
        {
            List<DTOrderList> list = this.DataSource as List<DTOrderList>;
            if (list == null) return;
            List<DTOrderList> newLists = items as List<DTOrderList>;

            foreach (var item in newLists)
            {
                DTOrderList tager = list.Find(delegate(DTOrderList jItem) { return item.ID == jItem.ID; });
                if (tager == null) continue;

                Utility.CopyToValue(item, tager, typeof(DTOrderList));
            }
            bsList.ResetBindings(false);
        }

        public override void RemoveItem(int index)
        {
            bsList.RemoveAt(index);
        }

        public override void RemoveItem(object item)
        {
            bsList.Remove(item);
        }

        //public override event ICP.Framework.ClientComponents.UIFramework.SelectedHandler Selected;

        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;
        public override event CancelEventHandler CurrentChanging;

        #endregion

        #region GridView Event

        private void gvMain_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && CurrentRow != null)
                Workitem.Commands[DTOrderCommandConstants.Command_EditData].Execute();
        }

        public new event KeyEventHandler KeyDown;

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && CurrentRow != null)
            {
                Workitem.Commands[DTOrderCommandConstants.Command_EditData].Execute();
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
                Workitem.Commands[DTOrderCommandConstants.Command_ShowSearch].Execute();
            }
        }

        private void bsMainList_PositionChanged(object sender, EventArgs e)
        {
            Workitem.State[DTOrderStateConstants.CurrentRow] = CurrentRow;

            if (CurrentChanged != null) CurrentChanged(this, Current);
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
            DTOrderList list = gvMain.GetRow(e.RowHandle) as DTOrderList;
            if (list == null) return;

            if (list.IsValid == false)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Disabled);
            }
            else if (list.State == DTOrderState.NewOrder)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.NewLine);
            }
            else if (list.State == DTOrderState.Rejected)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Error);
            }

        }

        #endregion

        #region Workitem Common

        #region 数据操作

        [CommandHandler(DTOrderCommandConstants.Command_AddData)]
        public void Command_AddData(object sender, EventArgs e)
        {
            PartLoader.ShowEditPart<Order.OrderBaseEditPart>(Workitem, null, string.Empty, EditPartSaved);
        }

        void EditPartSaved(object[] prams)
        {
            if (prams == null || prams.Length == 0) return;

            DTOrderInfo data = prams[0] as DTOrderInfo;
            List<DTOrderList> source = this.DataSource as List<DTOrderList>;
            if (source == null || source.Count == 0)
            {
                bsList.Add(data);
                bsList.ResetBindings(false);
            }
            else
            {
                DTOrderList tager = source.Find(delegate(DTOrderList item) { return item.ID == data.ID; });
                if (tager == null)
                {
                    bsList.Insert(0, data);
                    bsList.ResetBindings(false);
                }
                else
                {
                    Utility.CopyToValue(data, tager, typeof(DTOrderList));
                    bsList.ResetItem(bsList.IndexOf(tager));
                }

            }
            if (CurrentChanged != null) CurrentChanged(this, CurrentRow);


            this.SetColumnsWidth();
        }

        void SetColumnsWidth()
        {
            gvMain.BestFitColumns();
        }

        [CommandHandler(DTOrderCommandConstants.Command_CopyData)]
        public void Command_CopyData(object sender, EventArgs e)
        {
            if (CurrentRow == null || Utility.GuidIsNullOrEmpty(CurrentRow.ID)) return;

            CurrentRow.EditMode = EditMode.Copy;

            PartLoader.ShowEditPart<Order.OrderBaseEditPart>(Workitem, CurrentRow, string.Empty, EditPartSaved);
        }

        [CommandHandler(DTOrderCommandConstants.Command_CancelData)]
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
                    SingleResult result = oeService.CancelDTOrder(CurrentRow.ID, isCancel, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate, LocalData.IsEnglish);

                    DTOrderList currentRow = CurrentRow;
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

        [CommandHandler(DTOrderCommandConstants.Command_Print)]
        public void Command_Print(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            try
            {
                IReportViewer viewer = DomesticTradePrintHelper.PrintOEOrder(CurrentRow.ID);
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

        [CommandHandler(DTOrderCommandConstants.Command_EditData)]
        public void Command_EditData(object sender, EventArgs e)
        {
            if (CurrentRow == null || CurrentRow.IsNew) return;

            CurrentRow.EditMode = EditMode.Edit;

            PartLoader.ShowEditPart<Order.OrderBaseEditPart>(Workitem, CurrentRow, string.Empty, EditPartSaved,
                DTOrderCommandConstants.Command_EditData + CurrentRow.ID.ToString());
        }

        #endregion

        [CommandHandler(DTOrderCommandConstants.Command_RefreshData)]
        public void Command_RefreshData(object sender, EventArgs e)
        {
            try
            {
                List<DTOrderList> blList = DataSource as List<DTOrderList>;
                if (blList == null || blList.Count == 0) return;

                List<Guid> ids = new List<Guid>();
                foreach (var item in blList)
                {
                    ids.Add(item.ID);
                }

                List<DTOrderList> list = oeService.GetDTOrderListByIds(ids.ToArray(), LocalData.IsEnglish);
                this.DataSource = list;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }

        [CommandHandler(DTOrderCommandConstants.Command_ViewReason)]
        public void Command_ViewReason(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            DevExpress.XtraEditors.XtraMessageBox.Show(CurrentRow.Reason);
        }

        [CommandHandler(DTOrderCommandConstants.Command_SendEmail)]
        public void Command_SendEmail(object sender, EventArgs e)
        {
            if (CurrentRow == null || Utility.GuidIsNullOrEmpty(CurrentRow.BookingerID)) return;

            ICP.Message.ServiceInterface.Message message = CreateMessageInfo();
            bool isSuccess = eMailClientService.ShowSendForm(message);
            if (isSuccess)
                eMailClientService.Save(message);
            //eMailClientService.ShowEmailForm(LocalData.UserInfo.LoginID
            //                                , new Guid[] { CurrentRow.BookingerID.Value }
            //                                , new Guid[] { }
            //                                , string.Empty
            //                                , string.Empty
            //                                , new List<ICP.OA.ServiceInterface.DataObjects.MailAttachmentInfo>());

            //eMailClientService.EMailSent += delegate(object data, EventArgs ea)
            //{

            //};
        }

        ICP.Message.ServiceInterface.Message CreateMessageInfo()
        {
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.Subject = string.Empty;
            message.Body = string.Empty;
            message.BodyFormat = BodyFormat.olFormatHTML;
            message.SendFrom = LocalData.UserInfo.EmailAddress;
            
            UserInfo receiveInfo = userService.GetUserInfo(CurrentRow.BookingerID.Value);
            if (receiveInfo != null && !string.IsNullOrEmpty(receiveInfo.EMail))
                message.SendTo = receiveInfo.EMail;

            message.Attachments = new List<AttachmentContent>();
            MessageUserPropertiesObject userProperties = message.UserProperties;
            userProperties.FormId = CurrentRow.ID;
            userProperties.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
            userProperties.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.Internal;
            userProperties.OperationId = CurrentRow.ID;


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
    }
}
