using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Common.ServiceInterface.Client;
using ICP.FAM.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.FCM.OceanImport.UI.Common;
using ICP.FCM.OceanImport.UI.Report;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.Utility;
using Microsoft.Practices.ObjectBuilder;
using ICP.OA.ServiceInterface.DataObjects;

namespace ICP.FCM.OceanImport.UI
{
    [ToolboxItem(false)]
    public partial class OIBusinessList : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        public OIBusinessList()
        {
            InitializeComponent();
        }

        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        [ServiceDependency]
        public IOceanImportService oiService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public OceanImportPrintHelper OceanImportPrintHelper { get; set; }

        [ServiceDependency]
        public ICP.FCM.Common.ServiceInterface.IFCMCommonService fcmCommonService { get; set; }

        [ServiceDependency]
        public IFinanceClientService finClientService { get; set; }

        #endregion

        #region 私有字段

        /// <summary>
        /// 当前活动行
        /// </summary>
        private OceanBusinessList CurrentRow
        {
            get
            {
                return bsList.Current as OceanBusinessList;
            }
        }
        #endregion

        #region 初始化
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitMessage();
                InitControls();
            }
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
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Utility.ShowEditPart<OIBusinessEdit>(Workitem, null, "新增海运进口业务", EditPartSaved);
            }
        }

        /// <summary>
        /// 复制 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessCommandConstants.Command_CopyData)]
        public void Command_CopyData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null || Utility.GuidIsNullOrEmpty(CurrentRow.ID)) return;

                OceanBusinessInfo newData = oiService.GetBusinessInfo(CurrentRow.ID);
                newData.ID = Guid.Empty;
                newData.State = OIOrderState.NewOrder;
                newData.No = string.Empty;
                newData.CreateID = LocalData.UserInfo.LoginID;
                newData.CreateByName = LocalData.UserInfo.LoginName;
                newData.CreateDate = DateTime.Now;
                newData.SalesID = LocalData.UserInfo.LoginID;
                newData.SalesName = LocalData.UserInfo.LoginName;

                Utility.ShowEditPart<OIBusinessEdit>(Workitem, newData, "新增海运进口业务", EditPartSaved);
            }
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
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null)
                {
                    return;
                }
                Utility.ShowEditPart<OIBusinessEdit>(Workitem, CurrentRow, "编辑海运进口业务", EditPartSaved);
            }
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
                    Utility.ShowMessage(NativeLanguageService.GetText(this, "11091600001"));
                    return;
                }

                OperationCommonInfo operationCommonInfo = fcmCommonService.GetOperationCommonInfo(CurrentRow.ID, ICP.Framework.CommonLibrary.Common.OperationType.OceanImport);
                if (operationCommonInfo != null)
                {
                    operationCommonInfo.CurrentFormID = CurrentRow.MBLID.ToGuid();
                    finClientService.ShowBillList(operationCommonInfo, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
                }
                else
                {
                    Utility.ShowMessage(LocalData.IsEnglish ? @"No found,Please contact the system administrator" : @"无对应的数据,请联系系统管理员!");
                }
            }
        }

        #endregion

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

            if (CurrentRow.IsSentAN == false && string.IsNullOrEmpty(CurrentRow.SalesName))
            {
                if (DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Sales name is null,and Sales can not receive the Arrival Notice ,Sure to continue?" : "由于未填写业务员,港后通知邮件将无法通知到业务员.是否继续?",
                                    LocalData.IsEnglish ? "Tip" : "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            OceanBusinessInfo currentBusinessInfo = oiService.GetBusinessInfo(CurrentRow.ID);
            if (currentBusinessInfo.CompanyID == new Guid("0501D29D-0EFE-E111-B376-0026551CA87B")) //巴西到港通知
            {
                ICP.Message.ServiceInterface.Message operationInfo = GetOperationInfo();
                IReportViewer viewer = OceanImportPrintHelper.PrintArrivalNoticeReportForBrazil(CurrentRow, operationInfo);
                if (viewer == null)
                {
                    return;
                }
            }
            else
            {
                Dictionary<string, object> stateValues = new Dictionary<string, object>();
                stateValues.Add("OceanBusinessList", CurrentRow);
                string no = CurrentRow.No.Length <= 4 ? CurrentRow.No : CurrentRow.No.Substring(CurrentRow.No.Length - 4, 4);
                string title = (LocalData.IsEnglish ? "Print Arrival Notice" : "到港通知书") + ("-" + no);
                PartLoader.ShowEditPart<OIArrivalNotice2>(Workitem, null, stateValues, title, null, null);
            }
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
            string no = CurrentRow.No.Length <= 4 ? CurrentRow.No : CurrentRow.No.Substring(CurrentRow.No.Length - 4, 4);
            string title = (LocalData.IsEnglish ? "Print Release Order" : "放货通知书") + ("-" + no);
            PartLoader.ShowEditPart<OIReleaseOrder2>(Workitem, null, stateValues, title, null, null);
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
            string no = CurrentRow.No.Length <= 4 ? CurrentRow.No : CurrentRow.No.Substring(CurrentRow.No.Length - 4, 4);
            string title = (LocalData.IsEnglish ? "Print Forwarding Bill" : "货代提单") + ("-" + no);
            PartLoader.ShowEditPart<OIBLPrintPart2>(Workitem, null, stateValues, title, null, null);
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
                IReportViewer viewer = OceanImportPrintHelper.PrintProfit(CurrentRow, operationInfo);
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
                IReportViewer viewer = OceanImportPrintHelper.PrintWorkSheet(CurrentRow, operationInfo);
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
                OceanImportPrintHelper.PrintExportBusinessInfo(CurrentRow);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
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

                string message = string.Empty;
                if (isCancel)
                    message = LocalData.IsEnglish ? "Srue Cancel Current Business?" : "你真的要取消这笔业务吗?";
                else
                    message = LocalData.IsEnglish ? "Srue Available Current Business?" : "你真的要恢复这笔业务吗?";


                bool isOK = Utility.ShowResultMessage(message);

                if (isOK)
                {
                    SingleResult result = oiService.CancelOIOrder(CurrentRow.ID, isCancel, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);

                    OceanBusinessList currentRow = CurrentRow;
                    currentRow.IsValid = !isCancel;
                    currentRow.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    bsList.ResetCurrentItem();

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Changed Business State Successfully" : "更改业务状态成功");
                    if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "Changed Order State Failed" : "更改订单状态失败") + ex.Message);
            }
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

                    List<OceanBusinessList> list = oiService.GetBusinessListByIds(ids.ToArray());
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
                    Utility.ShowMessage(NativeLanguageService.GetText(this, "11091600001"));
                    return;
                }


                string title = LocalData.IsEnglish ? "ReleaseNotify" : "提货通知书";



                Utility.ShowEditPart<OIBusinessTruckEdit>(Workitem, CurrentRow.ID, title, TruckEditPartSaved);
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
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OIBusinessDownLoadWorkitem workItem = this.Workitem.WorkItems.AddNew<OIBusinessDownLoadWorkitem>();
                workItem.Run();
            }
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

            ConfirmBookingForm memoContentForm = Workitem.Items.AddNew<ConfirmBookingForm>();
            string title = LocalData.IsEnglish ? "Risk Clients" : "确认订舱";
            memoContentForm.LabelText = LocalData.IsEnglish ? "Memo for cancel" : "确认订舱备注";

            if (PartLoader.ShowDialog(memoContentForm, title) != DialogResult.OK)
            {
                return;
            }

            try
            {
                SingleResultData result = oiService.ChangeOIOrderState(CurrentRow.ID, OIOrderState.BookingConfirmed, memoContentForm.Memo, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);
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

            ConfirmBookingForm memoContentForm = Workitem.Items.AddNew<ConfirmBookingForm>();
            string title = LocalData.IsEnglish ? "Risk Clients" : "确认装船";
            memoContentForm.LabelText = LocalData.IsEnglish ? "Memo for Set to Risk Clients" : "确认装船备注";

            if (PartLoader.ShowDialog(memoContentForm, title) != DialogResult.OK)
            {
                return;
            }

            try
            {
                SingleResultData result = oiService.ChangeOIOrderState(CurrentRow.ID, OIOrderState.LoadVoyage, memoContentForm.Memo, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);
                CurrentRow.State = OIOrderState.LoadVoyage;
                CurrentRow.UpdateDate = result.UpdateDate;
                bsList.ResetCurrentItem();

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Disuse Successfully" : "确认装船成功!");
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }
        #endregion

        #region 放货
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

            if (CurrentRow.State != OIOrderState.Release)
            {
                OIBusinessRelease bsRelease = Workitem.Items.AddNew<OIBusinessRelease>();
                bsRelease.list = CurrentRow;

                string title = LocalData.IsEnglish ? "Release" : "放货";

                if (Utility.ShowDialog(bsRelease, title) == DialogResult.OK)
                {
                    OceanBusinessList currentRow = CurrentRow;
                    currentRow.State = OIOrderState.Release;
                    currentRow.ReleaseType = bsRelease.releaseType;
                    currentRow.IsTelex = currentRow.ReleaseType == FCMReleaseType.Telex ? true : false;
                    currentRow.UpdateDate = bsRelease.UpdateTime;
                    currentRow.ReleaseDate = bsRelease.ReleaseDate;
                    bsList.ResetCurrentItem();

                    if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
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
                        SingleResultData result = oiService.ChangeOIOrderState(CurrentRow.ID, OIOrderState.Checked, string.Empty, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);
                        CurrentRow.State = OIOrderState.Checked;
                        CurrentRow.UpdateDate = result.UpdateDate;
                        bsList.ResetCurrentItem();

                        if (CurrentChanged != null)
                        {
                            CurrentChanged(this, CurrentRow);
                        }
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Cancel Successfully" : "取消成功!");
                    }
                    catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
                }
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

                OIBusinessTransfer bsTransfer = Workitem.Items.AddNew<OIBusinessTransfer>();
                Dictionary<string, object> stateValues = new Dictionary<string, object>();
                bsTransfer.BusinessID = CurrentRow.ID;

                string title = LocalData.IsEnglish ? "Business Transfer" : "业务转移";

                if (Utility.ShowDialog(bsTransfer, title) == DialogResult.OK)
                {
                    OceanBusinessList currentRow = CurrentRow;
                    currentRow.UpdateDate = bsTransfer.UpdateDate;

                    bsList.ResetCurrentItem();
                }
            }
            else
            {
                string message = LocalData.IsEnglish ? "Not Select Business Data" : "请选择一个业务单";
                DevExpress.XtraEditors.XtraMessageBox.Show(message);
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

                bsList.DataSource = list;

                bsList.ResetBindings(false);
                if (CurrentChanged != null)
                {
                    CurrentChanged(this, Current);
                }
                gvMain.BestFitColumns();

                gvMain.EndUpdate();
                gcMain.EndUpdate();

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
            if (prams == null || prams.Length == 0) return;

            OceanBusinessInfo data = prams[0] as OceanBusinessInfo;

            //特殊数据的处理
            string hblNo = prams[1].ToString();
            string containerNo = prams[2].ToString();

            data.SubNo = hblNo;
            data.ContainerNo = containerNo;

            data.MBLID = data.MBLInfo.ID;
            data.MBLNo = data.MBLInfo.MBLNo;
            data.VesselVoyage = data.MBLInfo.VoyageName;
            data.ITNO = data.MBLInfo.ITNO;
            data.OBLRcved = Convert.ToBoolean(prams[3]);


            List<OceanBusinessList> source = this.DataSource as List<OceanBusinessList>;
            if (source == null || source.Count == 0)
            {
                bsList.Add(data);
                bsList.ResetBindings(false);
            }
            else
            {
                OceanBusinessList tager = source.Find(delegate(OceanBusinessList item) { return item.ID == data.ID; });
                if (tager == null)
                {
                    bsList.Insert(0, data);
                    bsList.ResetBindings(false);
                }
                else
                {
                    Utility.CopyToValue(data, tager, typeof(OceanBusinessList));

                    bsList.ResetItem(bsList.IndexOf(tager));
                }

            }
            if (CurrentChanged != null)
            {
                CurrentChanged(this, CurrentRow);
            }
        }

        /// <summary>
        /// 编辑界面保存数据
        /// </summary>
        /// <param name="prams"></param>
        private void TruckEditPartSaved(object[] prams)
        {
            Guid[] ids = new Guid[1];
            ids[0] = CurrentRow.ID;
            List<OceanBusinessList> datas = oiService.GetBusinessListByIds(ids);
            CurrentRow.ContainerNo = datas[0].ContainerNo;

            //bsList.ResetItem(bsList.IndexOf(tager));

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

                if (Utility.ShowDialog(bsTransfer, title) == DialogResult.OK)
                {
                    OceanBusinessList currentRow = CurrentRow;
                    currentRow.OBLRcved = bsTransfer.OBLRcved;

                    bsList.ResetCurrentItem();
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
            if (e.KeyCode == Keys.F6 && CurrentRow != null)
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
