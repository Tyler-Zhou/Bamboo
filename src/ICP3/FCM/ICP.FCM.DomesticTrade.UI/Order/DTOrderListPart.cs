using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.FCM.DomesticTrade.ServiceInterface;
using ICP.FCM.DomesticTrade.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.ClientComponents.Controls;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Message.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;

namespace ICP.FCM.DomesticTrade.UI.Order
{
    [ToolboxItem(false)]
    public partial class DTOrderListPart : BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IDomesticTradeService DomesticTradeService
        {
            get
            {
                return ServiceClient.GetService<IDomesticTradeService>();
            }
        }
        public IClientMessageService ClientMessageService
        {
            get
            {
                return ServiceClient.GetClientService<IClientMessageService>();
            }
        }
        public DomesticTradePrintHelper DomesticTradePrintHelper
        {
            get
            {
                return ClientHelper.Get<DomesticTradePrintHelper, DomesticTradePrintHelper>();
            }
        }
        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }
        #endregion

        #region Init

        public DTOrderListPart()
        {
            InitializeComponent();

            Disposed += delegate {
                CurrentChanged = null;
                CurrentChanging = null;
                KeyDown = null;
                gcMain.DataSource = null;
                bsList.DataSource = null;
                bsList.Dispose();
                
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };

            Load += new EventHandler(DTOrderListPart_Load);

            if (!LocalData.IsEnglish)
            {
                SetCnText();
            }
        }

        bool _shown = false;

        void DTOrderListPart_Load(object sender, EventArgs e)
        {
            DataSource = new List<DTOrderList>();

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
            List<EnumHelper.ListItem<DTOrderState>> orderStates = EnumHelper.GetEnumValues<DTOrderState>(LocalData.IsEnglish);
            foreach (var item in orderStates)
            {
                rcmbState.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
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

                SetColumnsWidth();



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

                if (_shown)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), message);
                }
            }
        }

        public override void Refresh(object items)
        {
            List<DTOrderList> list = DataSource as List<DTOrderList>;
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

        public override event CurrentChangedHandler CurrentChanged;
        public override event CancelEventHandler CurrentChanging;

        #endregion

        #region GridView Event

        private void gvMain_RowCellClick(object sender, RowCellClickEventArgs e)
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
            else if (KeyDown != null
                && e.KeyCode == Keys.F5
                && gvMain.FocusedColumn != null
                && gvMain.FocusedValue != null)
            {
                string text = gvMain.GetFocusedDisplayText();//.GetDisplayText(treeMain.FocusedColumn);
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add(gvMain.FocusedColumn.FieldName, text);
                KeyDown(keyValue, null);
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

        private void gvMain_BeforeLeaveRow(object sender, RowAllowEventArgs e)
        {
            if (CurrentChanging != null)
            {
                CancelEventArgs ce = new CancelEventArgs();
                CurrentChanging(this, ce);
                e.Allow = !ce.Cancel;
            }
        }

        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            DTOrderList list = gvMain.GetRow(e.RowHandle) as DTOrderList;
            if (list == null) return;

            if (list.IsValid == false)
            {
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.Disabled);
            }
            else if (list.State == DTOrderState.NewOrder)
            {
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.NewLine);
            }
            else if (list.State == DTOrderState.Rejected)
            {
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.Error);
            }

        }

        #endregion

        #region Workitem Common

        #region 数据操作

        [CommandHandler(DTOrderCommandConstants.Command_AddData)]
        public void Command_AddData(object sender, EventArgs e)
        {
            PartLoader.ShowEditPart<OrderBaseEditPart>(Workitem, null, string.Empty, EditPartSaved);
        }

        void EditPartSaved(object[] prams)
        {
            if (prams == null || prams.Length == 0) return;

            DTOrderInfo data = prams[0] as DTOrderInfo;
            List<DTOrderList> source = DataSource as List<DTOrderList>;
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


            SetColumnsWidth();
        }

        void SetColumnsWidth()
        {
            gvMain.BestFitColumns();
        }

        [CommandHandler(DTOrderCommandConstants.Command_CopyData)]
        public void Command_CopyData(object sender, EventArgs e)
        {
            if (CurrentRow == null || ArgumentHelper.GuidIsNullOrEmpty(CurrentRow.ID)) return;

            CurrentRow.EditMode = EditMode.Copy;

            PartLoader.ShowEditPart<OrderBaseEditPart>(Workitem, CurrentRow, string.Empty, EditPartSaved);
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


                DialogResult dialogResult = XtraMessageBox.Show(message,
                                                    LocalData.IsEnglish ? "Tip" : "提示",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    SingleResult result = DomesticTradeService.CancelDTOrder(CurrentRow.ID,CurrentRow.CompanyID, isCancel, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);

                    DTOrderList currentRow = CurrentRow;
                    currentRow.IsValid = !isCancel;
                    currentRow.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    bsList.ResetCurrentItem();

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Changed Order State Successfully" : "更改订单状态成功");
                    if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), (LocalData.IsEnglish ? "Changed Order State Failed" : "更改订单状态失败") + ex.Message);
            }
        }

        [CommandHandler(DTOrderCommandConstants.Command_Print)]
        public void Command_Print(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;
            using (new CursorHelper())
            {
                DomesticTradePrintHelper.PrintOEOrder(CurrentRow.ID, CurrentRow.CompanyID);
            }
         
        }

        [CommandHandler(DTOrderCommandConstants.Command_EditData)]
        public void Command_EditData(object sender, EventArgs e)
        {
            if (CurrentRow == null || CurrentRow.IsNew) return;

            CurrentRow.EditMode = EditMode.Edit;
            using (new CursorHelper())
            {
                PartLoader.ShowEditPart<OrderBaseEditPart>(Workitem, CurrentRow, string.Empty, EditPartSaved,
                    DTOrderCommandConstants.Command_EditData + CurrentRow.ID.ToString());
            }
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
                List<Guid> companyIDs = new List<Guid>();
                foreach (var item in blList)
                {
                    ids.Add(item.ID);
                    companyIDs.Add(item.CompanyID);
                }

                List<DTOrderList> list = DomesticTradeService.GetDTOrderListByIds(ids.ToArray(), companyIDs.ToArray());
                DataSource = list;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
        }

        [CommandHandler(DTOrderCommandConstants.Command_ViewReason)]
        public void Command_ViewReason(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            XtraMessageBox.Show(CurrentRow.Reason);
        }

        [CommandHandler(DTOrderCommandConstants.Command_SendEmail)]
        public void Command_SendEmail(object sender, EventArgs e)
        {
            if (CurrentRow == null || ArgumentHelper.GuidIsNullOrEmpty(CurrentRow.BookingerID)) return;

            Message.ServiceInterface.Message message = CreateMessageInfo();
            bool isSuccess = ClientMessageService.ShowSendForm(message);
            if (isSuccess)
                ClientMessageService.Save(message);
        }

        Message.ServiceInterface.Message CreateMessageInfo()
        {
            Message.ServiceInterface.Message message = new Message.ServiceInterface.Message();
            message.UserProperties = new MessageUserPropertiesObject();
            message.Subject = string.Empty;
            message.Body = string.Empty;
            message.BodyFormat = BodyFormat.olFormatHTML;
            message.SendFrom = LocalData.UserInfo.EmailAddress;
            
            UserInfo receiveInfo = UserService.GetUserInfo(CurrentRow.BookingerID.Value);
            if (receiveInfo != null && !string.IsNullOrEmpty(receiveInfo.EMail))
                message.SendTo = receiveInfo.EMail;
            MessageUserPropertiesObject userProperties = message.UserProperties;
            userProperties.FormId = CurrentRow.ID;
            userProperties.FormType = FormType.Booking;
            userProperties.OperationType = OperationType.Internal;
            userProperties.OperationId = CurrentRow.ID;


            return message;

        }

        #endregion

        private void gvMain_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }
    }
}
