using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.FCM.OceanImport.UI.Common;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.Utility;
using ICP.FCM.Common.UI.DispatchCompare;
using ICP.FCM.Common.UI;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.Controls;

namespace ICP.FCM.OceanImport.UI
{
    [ToolboxItem(false)]
    public partial class OIBusinessList : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        public OIBusinessList()
        {
            InitializeComponent();
            if (!LocalData.IsDesignMode)
            {
                this.Disposed += delegate
                {
                    this.CurrentChanged = null;
                    this.KeyDown = null;
                    gcMain.DataSource = null;
                    this.gvMain.CustomDrawRowIndicator -= this.gvMain_CustomDrawRowIndicator;
                    this.gvMain.DoubleClick -= this.gvMain_DoubleClick;
                    this.gvMain.KeyDown -= this.gvMain_KeyDown;
                    this.gvMain.RowCellClick -= this.gvMain_RowCellClick;
                    this.gvMain.RowStyle -= this.gvMain_RowStyle;
                    
                    bsList.DataSource = null;
                    this.bsList.PositionChanged -= this.bsList_PositionChanged;
                    this.bsList.Dispose();
                    this.bsList = null;

                    if (this.RefreshService != null && RefreshService.Refresh != null)
                    {
                        RefreshService.Refresh -= RefershListData;
                    }
                    if (Workitem != null)
                    {
                        Workitem.Items.Remove(this);
                        Workitem = null;
                    }
                };
            }
         
        }

        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IOceanImportService OceanImportService
        {
            get
            {
                return ServiceClient.GetService<IOceanImportService>();
            }
        }

        public OceanImportPrintHelper OceanImportPrintHelper
        {
            get
            {
                return ClientHelper.Get<OceanImportPrintHelper, OceanImportPrintHelper>();
            }
        }

        public IOperationAgentService OperationAgentService
        {
            get { return ServiceClient.GetService<IOperationAgentService>(); }
        }

        public IClientOceanImportService ClientOceanImportService
        {
            get
            {
                return ServiceClient.GetClientService<IClientOceanImportService>();
            }
        }

        public RefreshService RefreshService
        {
            get { return ClientHelper.Get<RefreshService, RefreshService>(); }
        }


        #endregion

        #region 私有字段

        /// <summary>
        /// 当前活动行
        /// </summary>
        private OceanBusinessList CurrentRow
        {
            get
            {
                Workitem.State["OEBookingOperationID"] = ((OceanBusinessList)bsList.Current).ID;
                return bsList.Current as OceanBusinessList;
            }
        }

        /// <summary>
        /// 获取选择的列表集合
        /// </summary>
        public List<OceanBusinessList> CurrentList
        {
            get
            {
                int[] rowIndexs = gvMain.GetSelectedRows();

                if (rowIndexs.Length == 0) return null;

                List<OceanBusinessList> tagers = new List<OceanBusinessList>();
                foreach (var item in rowIndexs)
                {
                    OceanBusinessList dr = gvMain.GetRow(item) as OceanBusinessList;
                    if (dr != null) tagers.Add(dr);

                }
                return tagers;
            }

        }

        public EditPartShowCriteria ShowCriteria
        {
            get
            {
                return new EditPartShowCriteria { OperationNo = this.CurrentRow.No, BillNo = this.CurrentRow.ID };
            }
        }

        #endregion

        public bool IsInternalAgent
        {
            get
            {
                if (OceanImportService.CheckIsInternalAgent(CurrentRow.AgentID))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #region 初始化
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitMessage();
                InitControls();
                RefreshService.Refresh += RefershListData;
            }
        }

        private void RefershListData()
        {
            Control[] ss = this.FindForm().Controls.Find("historyOceanRecordPart", true);
            if (ss != null && ss.Length > 0)
            {
                HistoryOceanRecordPart hrpart = ss[0] as HistoryOceanRecordPart;
                hrpart.BindingData();
            }
                     //  ss = this.FindForm().Controls.Find("eventListPart", true);
            //if (ss != null && ss.Length > 0)
            //{
            //    ICP.Business.Common.UI.EventList.EventListPart eventListPart = ss[0] as ICP.Business.Common.UI.EventList.EventListPart;
            //    eventListPart.DataBind()
            //}

        }
        private void InitControls()
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OIOrderState>> orderStates = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OIOrderState>(LocalData.IsEnglish);
            cmbState.Properties.BeginUpdate();
            foreach (var item in orderStates)
            {
                cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbState.Properties.EndUpdate();
        }

        private void InitMessage()
        {
            this.RegisterMessage("11091600001", LocalData.IsEnglish ? "Please save MBL info" : "请先保存MBL信息");
        }

        #endregion

        #region 按钮事件

        #region 新增 & 复制
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessCommandConstants.Command_AddData)]
        public void Command_AddData(object sender, EventArgs e)
        {
            ClientOceanImportService.AddBusiness(null, EditPartSaved);
        }

        /// <summary>
        /// 复制 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessCommandConstants.Command_CopyData)]
        public void Command_CopyData(object sender, EventArgs e)
        {
            ClientOceanImportService.CopyBusiness(ShowCriteria, null, EditPartSaved);
        }
        #endregion

        #region 编辑

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessCommandConstants.Command_EditData)]
        public void Command_EditData(object sender, EventArgs e)
        {
            ClientOceanImportService.EditBusiness(ShowCriteria, null, EditPartSaved);
        }

        #endregion

        #region 账单

        [CommandHandler(OIBusinessCommandConstants.Command_Bill)]
        public void Command_Bill(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null)
                {
                    return;
                }
                if (CurrentRow.MBLID == null || CurrentRow.MBLID.ToGuid() == Guid.Empty)
                {
                    MessageBoxService.ShowInfo(NativeLanguageService.GetText(this, "11091600001"));
                    return;
                }

                ClientOceanImportService.OpenBill(CurrentRow.ID, CurrentRow.MBLID.ToGuid());
            }
        }

        #endregion

        [CommandHandler(OIBusinessCommandConstants.Command_BillRevise)]
        public void Command_BillRevise(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null) return;
                if (CurrentRow.MBLID == null || CurrentRow.MBLID.ToGuid() == Guid.Empty)
                {
                    MessageBoxService.ShowInfo(NativeLanguageService.GetText(this, "11091600001"));
                    return;
                }

               ICP.FCM.Common.UI.FCMUIUtility.ShowBillRevise(Workitem,CurrentRow.ID,CurrentRow.AgentName);
                
            }
        }

        #region 打印报表

        /// <summary>
        /// 打印到港通知书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessCommandConstants.Command_PrintArrivalNotice)]
        public void Command_PrintArrivalNotice(object sender, EventArgs e)
        {
            if (this.CurrentRow == null) return;

            ClientOceanImportService.PrintArrivalNotice(CurrentRow);

        }

        /// <summary>
        /// 打印放货通知书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessCommandConstants.Command_PrintReleaseOrder)]
        public void Command_PrintReleaseOrder(object sender, EventArgs e)
        {
            if (this.CurrentRow == null) return;
            Dictionary<string, object> stateValues = new Dictionary<string, object>();
            stateValues.Add("OceanBusinessList", CurrentRow);
            ClientOceanImportService.PrintReleaseOrder(stateValues, CurrentRow.No);
        }

        /// <summary>
        /// 打印货代提单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessCommandConstants.Command_PrintForwardingBill)]
        public void Command_PrintForwardingBill(object sender, EventArgs e)
        {
            if (this.CurrentRow == null) return;
            Dictionary<string, object> stateValues = new Dictionary<string, object>();
            stateValues.Add("OceanBusinessList", CurrentRow);
            ClientOceanImportService.PrintForwardingBill(stateValues, CurrentRow.No);
        }

        /// <summary>
        /// 打印利润表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessCommandConstants.Command_PrintProfit)]
        public void Command_PrintProfit(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;
            ICP.Message.ServiceInterface.Message operationInfo = GetOperationInfo();
            try
            {
                OceanImportPrintHelper.PrintProfit(CurrentRow, operationInfo);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
        }

        /// <summary>
        /// 打印工作表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessCommandConstants.Command_PrintWorkSheet)]
        public void Command_PrintWorkSheet(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;
            ICP.Message.ServiceInterface.Message operationInfo = GetOperationInfo();
            try
            {
                OceanImportPrintHelper.PrintWorkSheet(CurrentRow, operationInfo);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
        }

        
        /// <summary>
        /// 打印出口业务信息报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessCommandConstants.Command_PrintExportBusinessInfo)]
        public void Command_PrintExportBusinessInfo(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            try
            {
                OceanImportPrintHelper.PrintExportBusinessInfo(CurrentRow.CustomerName, CurrentRow.CustomerID);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
        }

        /// <summary>
        /// 发送提货通知书给客户(中文)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessCommandConstants.Command_MailPickUpToCHS)]
        public void Command_MailPickUpToCHS(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;
            ClientOceanImportService.MailPickUpToCustomer(CurrentRow.ID, false);
        }

        /// <summary>
        /// 发送提货通知书给客户(英文)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessCommandConstants.Command_MailPickUpToENG)]
        public void Command_MailPickUpToENG(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;
            ClientOceanImportService.MailPickUpToCustomer(CurrentRow.ID,true);
        }

        /// <summary>
        /// 发送到港通知书给客户(中文版)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessCommandConstants.Command_MailANToCustomerCHS)]
        public void Command_MailANToCustomerCHS(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;
            ClientOceanImportService.MailAnToCustomer(CurrentRow.ID,false);
        }

        /// <summary>
        /// 发送到港通知书给客户(英文版)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessCommandConstants.Command_MailANToCustomerENG)]
        public void Command_MailANToCustomerENG(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;
            ClientOceanImportService.MailAnToCustomer(CurrentRow.ID, true);
        }
        
        private ICP.Message.ServiceInterface.Message GetOperationInfo()
        {
            if (CurrentRow == null)
                return null;
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.UserProperties = new ICP.Message.ServiceInterface.MessageUserPropertiesObject();
            message.UserProperties.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.OceanImport;
            message.UserProperties.OperationId = CurrentRow.ID;
            message.UserProperties.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
            message.UserProperties.FormId = CurrentRow.ID;

            return message;
        }
        #endregion

        #region 取消
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessCommandConstants.Command_CancelData)]
        public void Command_CancelData(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            try
            {
                bool isCancel = CurrentRow.IsValid;
                ClientOceanImportService.CancelOIBusiness(CurrentRow.ID, isCancel, CurrentRow.UpdateDate, AfterCancelData);

            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "Changed Order State Failed" : "更改订单状态失败") + ex.Message);
            }
        }

        private void AfterCancelData(object[] prams)
        {
            if (prams == null || prams.Length < 2)
            {
                return;
            }
            DateTime? datetime = prams[0] as DateTime?;
            CurrentRow.IsValid = !CurrentRow.IsValid;
            CurrentRow.UpdateDate = datetime;
            bsList.ResetCurrentItem();
            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Changed Business State Successfully" : "更改业务状态成功");
            if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
        }

        #endregion

        #region 刷新
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessCommandConstants.Command_RefreshData)]
        public void Command_RefreshData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    List<OceanBusinessList> blList = DataSource as List<OceanBusinessList>;
                    if (blList == null || blList.Count == 0) return;

                    List<Guid> ids = new List<Guid>();
                    foreach (var item in blList)
                    {
                        ids.Add(item.ID);
                    }

                    List<OceanBusinessList> list = OceanImportService.GetBusinessListByIds(ids.ToArray());
                    this.DataSource = list;
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
            }
        }

        #endregion

        #region 提货通知书
        /// <summary>
        /// 提货通知书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessCommandConstants.Command_CargoBook)]
        public void Command_CargoBook(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null)
                {
                    return;
                }
                if (CurrentRow.MBLID == null || CurrentRow.MBLID.ToGuid() == Guid.Empty)
                {
                    MessageBoxService.ShowInfo(NativeLanguageService.GetText(this, "11091600001"));
                    return;
                }
                string title = LocalData.IsEnglish ? "ReleaseNotify" : "提货通知书";

                ClientOceanImportService.OpenDeliveryNotice(CurrentRow.ID, null, title, TruckEditPartSaved);
            }
        }
        #endregion

        #region 集装箱跟踪
        /// <summary>
        /// 集装箱跟踪
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessCommandConstants.Command_BoxTracking)]
        public void Command_BoxTracking(object sender, EventArgs e)
        {
            OIBusinessTracking oibusinessTrack = Workitem.Items.AddNew<OIBusinessTracking>();
            IWorkspace workSpace = Workitem.RootWorkItem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];

            SmartPartInfo sm = new SmartPartInfo();
            sm.Description = "集装箱跟踪";
            sm.Title = "集装箱跟踪";
            workSpace.Show(oibusinessTrack, sm);

        }
        #endregion

        #region 业务下载
        /// <summary>
        /// 下载 
        /// </summary>
        [CommandHandler(OIBusinessCommandConstants.Command_DownLoad)]
        public void Command_DownLoad(object sender, EventArgs e)
        {
            ClientOceanImportService.OIDownLoad();
        }

        /// <summary>
        /// This is the subscription for the CustomerAdded event
        /// We're using the default scope, which is Global
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [EventSubscription(OIBusinessCommandConstants.Command_InsertToListAfterDownLoad)]
        public void OnInsertToListAfterDownLoad(object sender, DataEventArgs<List<OceanBusinessList>> e)
        {
            List<OceanBusinessList> list = e.Data as List<OceanBusinessList>;
            if (list != null && list.Count > 0)
            {
                List<OceanBusinessList> dataList = bsList.DataSource as List<OceanBusinessList>;
                if (dataList == null || dataList.Count == 0)
                {
                    //数据源等于Null的时候，直接赋值数据源
                    bsList.DataSource = list;
                }
                else
                {
                    //数据源不等于Null的时候，加入数据源
                    foreach (OceanBusinessList b in list)
                    {
                        //    if ((from OceanBusinessList o in dataList where o.ID == b.ID select o).ToList().Count == 0)                     
                        bsList.Insert(0, b);
                        if (CurrentChanged != null)
                        {
                            CurrentChanged(this, b);
                        }
                    }
                }
                //刷新显示
                bsList.ResetBindings(false);

            }
        }

        #endregion

        #region 确认订舱
        /// <summary>
        /// 确认订舱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessCommandConstants.Command_ConfirmBooking)]
        public void Command_ConfirmBooking(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;
            string title = LocalData.IsEnglish ? "Risk Clients" : "确认订舱";
            string lableText = LocalData.IsEnglish ? "Memo for cancel" : "确认订舱备注";
            ClientOceanImportService.CommonConfirmForm(lableText, null, title, AfterConfirmBooking);
        }

        private void AfterConfirmBooking(object[] prams)
        {
            if (prams == null || prams.Length == 0)
            {
                return;
            }
            string memo = prams[0] as string;
            if (string.IsNullOrEmpty(memo))
            {
                return;
            }

            try
            {
                SingleResultData result = OceanImportService.ChangeOIOrderState(CurrentRow.ID, OIOrderState.BookingConfirmed, memo, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);
                CurrentRow.State = OIOrderState.BookingConfirmed;
                CurrentRow.UpdateDate = result.UpdateDate;
                bsList.ResetCurrentItem();
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Disuse Successfully" : "确认订舱成功!");
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }
        #endregion

        #region 确认装船
        /// <summary>
        /// 确认装船
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessCommandConstants.Command_ConfirmBookingShip)]
        public void Command_ConfirmBookingShip(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;
            string title = LocalData.IsEnglish ? "Risk Clients" : "确认装船";
            string labelText = LocalData.IsEnglish ? "Memo for Set to Risk Clients" : "确认装船备注";
            ClientOceanImportService.CommonConfirmForm(labelText, null, title, AfterConfirmBookingShip);
        }

        void AfterConfirmBookingShip(object[] prams)
        {
            if (prams == null || prams.Length == 0)
            {
                return;
            }
            string memo = prams[0] as string;
            if (string.IsNullOrEmpty(memo))
            {
                return;
            }
            try
            {
                SingleResultData result = OceanImportService.ChangeOIOrderState(CurrentRow.ID, OIOrderState.LoadVoyage, memo, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);
                CurrentRow.State = OIOrderState.LoadVoyage;
                CurrentRow.UpdateDate = result.UpdateDate;
                bsList.ResetCurrentItem();

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Disuse Successfully" : "确认装船成功!");
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }

        }
        #endregion

        #region 放货管理
        /// <summary>
        /// 放货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessCommandConstants.Command_Delivery)]
        public void Command_Delivery(object sender, EventArgs e)
        {
            if (CurrentRow == null)
            {
                return;
            }
            bool isreleasecargo = !CurrentRow.IsReleaseCargo;
            if (CurrentRow.State != OIOrderState.Release)
            {
                try
                {
                    ClientOceanImportService.ConfirmDelivery(CurrentRow, AfterDelivery);
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Release Successfully" : "成功!");
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                }
            }
            else
            {
                string message = LocalData.IsEnglish ? "Srue Cancel Release ?" : "确认要取消放货?";
                bool isOK = Utility.ShowResultMessage(message);
                if (isOK)
                {
                    try
                    {
                        ClientOceanImportService.CancelDelivery(CurrentRow, AfterDelivery);
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Cancel Successfully" : "取消成功!");
                    }
                    catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
                }
            }
        }

        private void AfterDelivery(object[] prams)
        {
            if (prams == null || prams.Length == 0)
            {
                return;
            }
            BusinessReleaseInfo releaseinfo = prams[0] as BusinessReleaseInfo;
            string message = string.Empty;
            bool isreleasecargo = !CurrentRow.IsReleaseCargo;

            OceanBusinessList currentRow = CurrentRow;
            currentRow.UpdateDate = releaseinfo.UpdateDate;

            if (CurrentRow.State != OIOrderState.Release)
            {
                currentRow.State = OIOrderState.Release;
                currentRow.IsTelex = currentRow.ReleaseType == FCMReleaseType.Telex ? true : false;
                currentRow.ReleaseType = releaseinfo.ReleaseType;
                currentRow.ReleaseDate = releaseinfo.Releasedate;
            }
            else
            {
                CurrentRow.State = OIOrderState.Checked;
                CurrentRow.ReleaseDate = null;
            }

            OceanImportService.ChangeOITrackingInfo(CurrentRow.ID, CurrentRow.IsReceiveNotice, CurrentRow.IsNoticeRelease, CurrentRow.IsApplyRC, !CurrentRow.IsReleaseCargo, currentRow.IsNoticePay, currentRow.IsAgreeRC, LocalData.UserInfo.LoginID);
            currentRow.IsReleaseCargo = isreleasecargo;
            bsList.ResetCurrentItem();

            if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
        }


        /// <summary>
        /// 同意放货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessCommandConstants.Command_AgreeRC)]
        public void Command_AgreeRC(object sender, EventArgs e)
        {
            if (CurrentRow == null)
            {
                return;
            }

            bool agreeRc = !CurrentRow.IsAgreeRC;
            
            string message = LocalData.IsEnglish ? "Mark 'AgreeRC'?" : "标识'同意放货'?";
            string cancelmessage = LocalData.IsEnglish ? "Un-Mark 'AgreeRC'?" : "取消标识'同意放货'?";
            bool isOK = Utility.ShowResultMessage(!CurrentRow.IsAgreeRC?message:cancelmessage);
            if (isOK)
            {
                try
                {
                    OceanImportService.ChangeOITrackingInfo(CurrentRow.ID, CurrentRow.IsReceiveNotice, CurrentRow.IsNoticeRelease, CurrentRow.IsApplyRC, CurrentRow.IsReleaseCargo, CurrentRow.IsNoticePay, !CurrentRow.IsAgreeRC, LocalData.UserInfo.LoginID);
                    CurrentRow.IsAgreeRC = agreeRc;
                    if (CurrentChanged != null)
                    {
                        CurrentChanged(this, CurrentRow);
                    }
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "change 'AgreeRC' Successfully" : "'同意放货'更新成功!");
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
            }
        }
        /// <summary>
        /// 申请放货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessCommandConstants.Command_ApplyRelease)]
        public void Command_ApplyRelease(object sender, EventArgs e)
        {
            if (CurrentRow == null)
            {
                return;
            }

            bool applyrelease = !CurrentRow.IsApplyRC;
            try
            {
                OceanImportService.ChangeOITrackingInfo(CurrentRow.ID, CurrentRow.IsReceiveNotice, CurrentRow.IsNoticeRelease, !CurrentRow.IsApplyRC, CurrentRow.IsReleaseCargo, CurrentRow.IsNoticePay, CurrentRow.IsAgreeRC, LocalData.UserInfo.LoginID);
                CurrentRow.IsApplyRC = applyrelease;
                if (CurrentChanged != null)
                {
                    CurrentChanged(this, CurrentRow);
                }
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Apply ReleaseRC Successfully" : "成功申请放货!");
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }

        /// <summary>
        /// 第三方代理海进业务放单(放单时，自动接收放单指令)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessCommandConstants.Command_ReleaseBL)]
        public void Command_ReleaseBL(object sender, EventArgs e)
        {
            if (CurrentRow == null)
            {
                return;
            }

            bool isrelease = !CurrentRow.IsRelease;
            bool isnotice = !CurrentRow.IsReceiveNotice;
            try
            {
                OceanImportService.ReleaseOIBL(CurrentRow.ID, !CurrentRow.IsRelease, LocalData.UserInfo.LoginID);
                CurrentRow.IsRelease = isrelease;
                CurrentRow.IsReceiveNotice = isnotice;
                if (CurrentChanged != null)
                {
                    CurrentChanged(this, CurrentRow);
                }
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "ReleaseBL Successfully" : "放单成功!");
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }

        /// <summary>
        /// 催港前放单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessCommandConstants.Command_NoticeRelease)]
        public void Command_NoticeRelease(object sender, EventArgs e)
        {
            if (CurrentRow == null)
            {
                return;
            }

            bool noticerelease = !CurrentRow.IsNoticeRelease;
            try
            {
                OceanImportService.ChangeOITrackingInfo(CurrentRow.ID, CurrentRow.IsReceiveNotice, !CurrentRow.IsNoticeRelease, CurrentRow.IsApplyRC, CurrentRow.IsReleaseCargo, CurrentRow.IsNoticePay, CurrentRow.IsAgreeRC, LocalData.UserInfo.LoginID);
                CurrentRow.IsNoticeRelease = noticerelease;
                if (CurrentChanged != null)
                {
                    CurrentChanged(this, CurrentRow);
                }
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Notice ReleaseBL Successfully" : "成功通知港前放单!");
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }

        /// <summary>
        /// 接受放单通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessCommandConstants.Command_ReceiveRN)]
        public void Command_ReceiveRN(object sender, EventArgs e)
        {
            List<OceanBusinessList> checkList = CurrentList;
            if (checkList == null || checkList.Count == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Check at least one row data." : "至少选中一行数据."
                            , LocalData.IsEnglish ? "Tip" : "提示"
                            , MessageBoxButtons.YesNo
                            , MessageBoxIcon.Question);
                return;
            }
            int theradID = 0;
            theradID=ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm(LocalData.IsEnglish ? "Receive ReleaseNotice..." : "正在接收放单通知...");
            try
            {
                foreach (OceanBusinessList item in checkList)
                {
                    bool receiveRN = !item.IsReceiveNotice;
                    if (!receiveRN)
                    {
                        continue;
                    }
                    OceanImportService.ChangeOITrackingInfo(item.ID, receiveRN, item.IsNoticeRelease, item.IsApplyRC, item.IsReleaseCargo, item.IsNoticePay, CurrentRow.IsAgreeRC, LocalData.UserInfo.LoginID);
                    item.IsReceiveNotice = receiveRN;
                    if (CurrentChanged != null)
                    {
                        CurrentChanged(this, item);
                    }
                }
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Receive ReleaseNotice Successfully" : "成功接收放单通知!");
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
            finally
            {
                bsList.ResetBindings(false);
                ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
            }
        }

        #endregion

        #region 业务转移
        /// <summary>
        /// 业务转换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessCommandConstants.Command_BusinessTransfer)]
        public void Command_BusinessTransfer(object sender, EventArgs e)
        {
            BusinessTransfer();
        }

        /// <summary>
        /// 业务转移
        /// </summary>
        public void BusinessTransfer()
        {
            if (CurrentRow != null)
            {
                ClientOceanImportService.BusinessTransfer(CurrentRow.ID, null,AfterBusinessTransfer);
            }
            else
            {
                string message = LocalData.IsEnglish ? "Not Select Business Data" : "请选择一个业务单";
                DevExpress.XtraEditors.XtraMessageBox.Show(message);
            }
        }

        private void AfterBusinessTransfer(object[] prams)
        {
            if (prams == null || prams.Length == 0)
            {
                return;
            }
            DateTime? datetime = prams[0] as DateTime?;
            try
            {
                CurrentRow.UpdateDate = datetime;
                bsList.ResetCurrentItem();
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Business Transfer Successfully" : "业务转移成功!");
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }

        }
        #endregion

        #region 异常放货申请流程
        /// <summary>
        /// 异常放货流程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessCommandConstants.Command_ExceptionReleaseRC)]
        public void Command_ExceptionReleaseRC(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow.IsNew) return;

                try
                {
                    ClientOceanImportService.ExceptionReleaseRC(CurrentRow.ID, CurrentRow.No);
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                }
            }
        }
        #endregion

        #region 签收分发
        /// <summary>
        /// 异常放货流程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessCommandConstants.Command_AcceptDispath)]
        public void Command_AcceptDispath(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            int state = OperationAgentService.GetDispatchState(CurrentRow.ID);
            if (state == 1)
            {
                ICP.FCM.Common.UI.FCMUIUtility.ShowAcceptedDocumentCompareNew(this.Workitem, Guid.Empty, CurrentRow.ID, false);
            }
            else
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "The bill does not need to be signed!" : "该业务不需要签收！");
            }
        }
        #endregion

        #endregion

        #region 重写
        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                gcMain.BeginUpdate();
                gvMain.BeginUpdate();

                List<OceanBusinessList> list = value as List<OceanBusinessList>;
                this.bsList.PositionChanged -= this.bsList_PositionChanged;
                bsList.DataSource = list;
                
                bsList.ResetBindings(false);
                this.bsList.PositionChanged += this.bsList_PositionChanged;
                this.bsList_PositionChanged(this.bsList, EventArgs.Empty);
                gvMain.BestFitColumns();

                gvMain.EndUpdate();
                gcMain.EndUpdate();

                string message = string.Empty;

                if (list!=null && list.Count > 0)
                {
                    if (LocalData.IsEnglish)
                    {
                        message = string.Format("{0} record found", list.Count);
                    }
                    else
                    {
                       // message = string.Format("查询到 {0} 条记录", list.Count);
                    }
                }
                else
                {
                   // message = LocalData.IsEnglish ? "Nothing found!" : "没有查询到任何结果。";
                }

                if (list!=null)
                {
                    if (list.Count.ToString().Length == 1)
                    {
                        gvMain.IndicatorWidth = 30;
                    }
                    else
                    {
                        gvMain.IndicatorWidth = list.Count.ToString().Length * 17;
                    }
                }
              
               // LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), message);
            }
        }
        public override object Current
        {
            get
            {
                return bsList.Current as OceanBusinessList;
            }
        }
        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;
        #endregion

        #region 私有方法
        /// <summary>
        /// 编辑界面保存数据
        /// </summary>
        /// <param name="prams"></param>
        private void EditPartSaved(object[] prams)
        {
            
        }

        /// <summary>
        /// 编辑界面保存数据
        /// </summary>
        /// <param name="prams"></param>
        private void TruckEditPartSaved(object[] prams)
        {
            if (prams == null || prams.Length < 1)
            {
                return;
            }
            Guid[] ids = new Guid[1];
            ids[0] = CurrentRow.ID;
            List<OceanBusinessList> datas = OceanImportService.GetBusinessListByIds(ids);
            CurrentRow.ContainerNo = datas[0].ContainerNo;
            bsList.ResetCurrentItem();
        }

        /// <summary>
        /// 当前行改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null)
            {
                CurrentChanged(this, Current);
            }
        }
        /// <summary>
        /// 单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column == this.colOBLRcved)
            {
                if (CurrentRow == null || CurrentRow.State == OIOrderState.Rejected)
                {
                    return;
                }

                OIBusinessReceived bsTransfer = Workitem.Items.AddNew<OIBusinessReceived>();
                bsTransfer.BusinessID = CurrentRow.ID;

                string title = LocalData.IsEnglish ? "Set OBLRcved Date" : "设置收到正本时间";

                if (PartLoader.ShowDialog(bsTransfer, title) == DialogResult.OK)
                {
                    OceanBusinessList currentRow = CurrentRow;
                    currentRow.OBLRcved = bsTransfer.OBLRcved;

                    bsList.ResetCurrentItem();
                }
            }
            if (e.Column == this.colIsNoticePay)
            {
                if (CurrentRow == null || CurrentRow.IsReleaseCargo)
                { return; }
                try
                {
                    string cancelmessage = LocalData.IsEnglish ? "Do you want to un-mark as 'Had push customer payment'?" : "确定取消标识：已催客户付款?";
                    string message = LocalData.IsEnglish ? "Do you want to mark as 'Had push customer payment'?" : "确定标识：已催客户付款?";

                    if (DevExpress.XtraEditors.XtraMessageBox.Show(CurrentRow.IsNoticePay ? cancelmessage : message
                        , LocalData.IsEnglish ? "Tip" : "提示"
                        , MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }

                    bool noticepay = !CurrentRow.IsNoticePay;
                    OceanImportService.ChangeOITrackingInfo(CurrentRow.ID, CurrentRow.IsReceiveNotice, CurrentRow.IsNoticeRelease, CurrentRow.IsApplyRC, CurrentRow.IsReleaseCargo, !CurrentRow.IsNoticePay, CurrentRow.IsAgreeRC, LocalData.UserInfo.LoginID);
                    CurrentRow.IsNoticePay = noticepay;
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "Failed to un-mark as 'Had push customer payment'" : "取消标识失败：已催客户付款。") + ex.Message);
                }
            }

        }
        public new event KeyEventHandler KeyDown;
        /// <summary>
        /// 回车编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && CurrentRow != null)
            {
                Workitem.Commands[OIBusinessCommandConstants.Command_EditData].Execute();
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
            if (e.KeyCode == Keys.F6)
            {
                Workitem.Commands[OIBusinessCommandConstants.Command_ShowSearch].Execute();
            }
        }
        /// <summary>
        /// 双击编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_DoubleClick(object sender, EventArgs e)
        {
            if (CurrentRow != null)
            {
                Workitem.Commands[OIBusinessCommandConstants.Command_EditData].Execute();
            }
        }

        private void gvMain_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            OceanBusinessList list = gvMain.GetRow(e.RowHandle) as OceanBusinessList;
            if (list == null) return;

            if (list.IsValid == false)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Disabled);
            }
            else if (list.State == OIOrderState.NewOrder)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.NewLine);
            }
            else if (list.State == OIOrderState.Release)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Confirmed);
            }
            else if (list.State == OIOrderState.Rejected)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Error);
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
