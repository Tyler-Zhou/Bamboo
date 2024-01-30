using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Business.Common.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Message.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
//using ICP.Test;

namespace ICP.FCM.OceanExport.UI.Order
{
    /// <summary>
    /// 海出订单列表界面
    /// </summary>
    [ToolboxItem(false)]
    public partial class OEOrderListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service


        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }


        public IICPCommonOperationService IcpCommonOperationService
        {
            get
            {
                return ServiceClient.GetClientService<IICPCommonOperationService>();
            }

        }
        public ICP.Sys.ServiceInterface.IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<ICP.Sys.ServiceInterface.IUserService>();
            }
        }

        public IClientOceanExportService ClientOceanExportService
        {
            get
            {
                return ServiceClient.GetClientService<IClientOceanExportService>();
            }
        }
        //public IAutoTestServiceInterface AutoTestService
        //{
        //    get
        //    {
        //        return ServiceClient.GetClientService<IAutoTestServiceInterface>();
        //    }

        //}
        #endregion

        #region Init

        public OEOrderListPart()
        {
            InitializeComponent();

            this.Disposed += delegate
            {
                this.gcMain.DataSource = null;
                this.gvMain.BeforeLeaveRow -= this.gvMain_BeforeLeaveRow;
                this.gvMain.CustomDrawRowIndicator -= this.gvMain_CustomDrawRowIndicator;
                this.gvMain.KeyDown -= this.gvMain_KeyDown;
                this.gvMain.RowCellClick -= this.gvMain_RowCellClick;
                this.gvMain.RowStyle -= this.gvMain_RowStyle;

                this.bsList.DataSource = null;
                this.bsList.PositionChanged -= this.bsMainList_PositionChanged;
                this.bsList.Dispose();
                this.CurrentChanged = null;
                this.CurrentChanging = null;

                this.KeyDown = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };

            this.Load += new EventHandler(OEOrderListPart_Load);

            if (!LocalData.IsEnglish)
            {
                // SetCnText();
            }
        }

        bool _shown = false;

        void OEOrderListPart_Load(object sender, EventArgs e)
        {
            this.DataSource = new List<OceanOrderList>();

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
            colClosingDate.Caption = "截关日";
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
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OEOrderState>> orderStates = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OEOrderState>(LocalData.IsEnglish);
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
                bsList.DataSource = null;
                bsList.DataSource = value;

                #region 对一些枚举类型，获取对应语言的描述信息

                //TODO: 装箱拆箱是消耗性能的
                List<OceanOrderList> list = value as List<OceanOrderList>;

                if (list != null)
                {
                    foreach (OceanOrderList item in list)
                    {
                        item.BookingTypeDescription = EnumHelper.GetDescription<FCMOperationType>(item.OEOperationType, LocalData.IsEnglish);
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
                           // message = string.Format("{0} record found", list.Count);
                        }
                    }
                    else
                    {
                        //message = string.Format("查询到 {0} 条记录", list.Count);
                    }
                }
                else
                {
                   // message = LocalData.IsEnglish ? "Nothing found!" : "没有查询到任何结果。";
                }

                if (this._shown)
                {
                   // LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), message);
                }
            }
        }

        public override void Refresh(object items)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                List<OceanOrderList> list = this.DataSource as List<OceanOrderList>;
                if (list == null) return;
                List<OceanOrderList> newLists = items as List<OceanOrderList>;

                foreach (var item in newLists)
                {
                    OceanOrderList tager = list.Find(delegate(OceanOrderList jItem) { return item.ID == jItem.ID; });
                    if (tager == null) continue;

                    OEUtility.CopyToValue(item, tager, typeof(OceanOrderList));
                }
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

        //public override event ICP.Framework.ClientComponents.UIFramework.SelectedHandler Selected;

        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;
        public override event CancelEventHandler CurrentChanging;

        #endregion

        #region GridView Event

        private void gvMain_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && CurrentRow != null)
                Workitem.Commands[OEOrderCommandConstants.Command_EditData].Execute();
        }

        public new event KeyEventHandler KeyDown;

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && CurrentRow != null)
            {
                Workitem.Commands[OEOrderCommandConstants.Command_EditData].Execute();
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
                Workitem.Commands[OEOrderCommandConstants.Command_ShowSearch].Execute();
            }
        }

        private void bsMainList_PositionChanged(object sender, EventArgs e)
        {
            Workitem.State[OEOrderStateConstants.CurrentRow] = CurrentRow;

            if (CurrentChanged != null)
            {
                CurrentChanged(this, Current);
                //Workitem.Commands[OEOrderCommandConstants.Command_OEOFaxEmail].Execute();
                //Workitem.Commands[OEOrderCommandConstants.Command_Memo].Execute();
                //Workitem.Commands[OEOrderCommandConstants.Command_Document].Execute();
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
            else if (list.State == OEOrderState.NewOrder)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.NewLine);
            }
            else if (list.State == OEOrderState.Rejected)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Error);
            }

        }

        #endregion

        #region Workitem Common

        #region 数据操作

        [CommandHandler(OEOrderCommandConstants.Command_AddData)]
        public void Command_AddData(object sender, EventArgs e)
        {
            ClientOceanExportService.AddOrder(null, this.EditPartSaved);
        }

        public void EditPartSaved(object[] prams)
        {
            if (this.IsDisposed || this.Parent.IsDisposed)
                return;
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
                    OEUtility.CopyToValue(data, tager, typeof(OceanOrderList));
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

        [CommandHandler(OEOrderCommandConstants.Command_CopyData)]
        public void Command_CopyData(object sender, EventArgs e)
        {
            if (CurrentRow == null || ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(CurrentRow.ID)) return;
            ClientOceanExportService.CopyOrder(this.ShowCriteria, null, this.EditPartSaved);
        }

        [CommandHandler(OEOrderCommandConstants.Command_CancelData)]
        public void Command_CancelData(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            ClientOceanExportService.CancelOrderForFCM(GetOceanBookingListInfo(), this.AfterCancelOrder);
        }

        private OceanBookingList GetOceanBookingListInfo()
        {
            OceanOrderInfo dataList = OceanExportService.GetOceanOrderInfo(CurrentRow.ID, CurrentRow.CompanyID);
            if (dataList == null) return null;
            OceanBookingList returnValue = new OceanBookingList
            {
                ID = dataList.ID,
                CompanyID = dataList.CompanyID,
                IsValid = dataList.IsValid,
                No = dataList.RefNo,
                UpdateDate = dataList.UpdateDate
            };
            return returnValue;
        }

        private void AfterCancelOrder(object[] parameters)
        {
            if (this.IsDisposed || this.Parent.IsDisposed)
                return;
            SingleResult result = parameters[0] as SingleResult;
            OceanOrderList currentRow = CurrentRow;
            currentRow.IsValid = !currentRow.IsValid;
            currentRow.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
            bsList.ResetCurrentItem();
            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Changed Order State Successfully" : "更改订单状态成功");
            if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
        }

        [CommandHandler(OEOrderCommandConstants.Command_Print)]
        public void Command_Print(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;
            ClientOceanExportService.PrintOrder(CurrentRow.ID, CurrentRow.CompanyID);


        }

        [CommandHandler(OEOrderCommandConstants.Command_EditData)]
        public void Command_EditData(object sender, EventArgs e)
        {
            if (CurrentRow == null || CurrentRow.IsNew) return;
            ClientOceanExportService.EditOrder(this.ShowCriteria, null, this.EditPartSaved);
        }
        public EditPartShowCriteria ShowCriteria
        {
            get
            {
                return new EditPartShowCriteria 
                { 
                    OperationID = CurrentRow.ID
                    , OperationNo = CurrentRow.RefNo
                    ,CompanyID = CurrentRow.CompanyID
                    ,BillNo = CurrentRow.ID
                };
            }
        }

        #endregion

        [CommandHandler(OEOrderCommandConstants.Command_RefreshData)]
        public void Command_RefreshData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    List<OceanOrderList> blList = DataSource as List<OceanOrderList>;
                    if (blList == null || blList.Count == 0) return;

                    List<Guid> ids = new List<Guid>();
                    List<Guid> companyIDs = new List<Guid>();
                    foreach (var item in blList)
                    {
                        ids.Add(item.ID);
                        companyIDs.Add(item.CompanyID);
                    }

                    List<OceanOrderList> list = OceanExportService.GetOceanOrderListByIds(ids.ToArray(), companyIDs.ToArray());
                    this.DataSource = list;
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
            }
        }
        [CommandHandler(OEOrderCommandConstants.Command_ViewReason)]
        public void Command_ViewReason(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            DevExpress.XtraEditors.XtraMessageBox.Show(CurrentRow.Reason);
        }

        [CommandHandler(OEOrderCommandConstants.Command_SendEmail)]
        public void Command_SendEmail(object sender, EventArgs e)
        {
            if (CurrentRow == null || ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(CurrentRow.BookingerID)) return;

            ICP.Message.ServiceInterface.Message message = CreateMessageInfo();
            IcpCommonOperationService.ShowSendForm(message);

        }

        [CommandHandler(OEOrderCommandConstants.Command_AgentFilerSearch)]
        public void Command_AgentFilerSearch(object sender, EventArgs e)
        {
            ClientOceanExportService.ShowAgentFilerList(CurrentRow == null ? string.Empty : CurrentRow.ConsigneeName);

        }
        ICP.Message.ServiceInterface.Message CreateMessageInfo()
        {
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.UserProperties = new MessageUserPropertiesObject();
            message.Subject = string.Empty;
            message.Body = string.Empty;
            message.BodyFormat = BodyFormat.olFormatHTML;
            message.SendFrom = LocalData.UserInfo.EmailAddress;

            UserInfo receiveInfo = UserService.GetUserInfo(CurrentRow.BookingerID.Value);
            if (receiveInfo != null && !string.IsNullOrEmpty(receiveInfo.EMail))
                message.SendTo = receiveInfo.EMail;

            message.Attachments = new List<AttachmentContent>();
            MessageUserPropertiesObject userProperties = message.UserProperties;
            userProperties.FormId = CurrentRow.ID;
            userProperties.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
            userProperties.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.OceanExport;
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
