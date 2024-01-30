using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.FAM.ServiceInterface;
using DevExpress.XtraTreeList.Nodes;
using ICP.Framework.ClientComponents.Controls;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;
using ICP.EDI.ServiceInterface;
using ICP.FCM.Common.UI;
using ICP.Business.Common.UI.EventList;
using ICP.FCM.Common.UI.DispatchCompare;
using ICP.FCM.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using ICP.FileSystem.ServiceInterface;
using ICP.Framework.ClientComponents;

namespace ICP.FCM.OceanExport.UI.BL
{
    [ToolboxItem(false)]
    public partial class OEBLListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
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
        public IClientOceanExportService ClientOceanExportService
        {
            get
            {
                return ServiceClient.GetClientService<IClientOceanExportService>();
            }
        }

        public ICP.FCM.Common.ServiceInterface.IFCMCommonClientService FCMCommonClientService
        {
            get
            {
                return ServiceClient.GetClientService<ICP.FCM.Common.ServiceInterface.IFCMCommonClientService>();
            }
        }

        public ICP.FCM.Common.ServiceInterface.IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<ICP.FCM.Common.ServiceInterface.IFCMCommonService>();
            }
        }

        /// <summary>
        /// 配置服务
        /// </summary>
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }


        public IFinanceClientService FinanceClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFinanceClientService>();
            }
        }

        public OceanExportPrintHelper OceanExportPrintHelper
        {
            get
            {
                return ClientHelper.Get<OceanExportPrintHelper, OceanExportPrintHelper>();
            }
        }

        public IOperationAgentService OperationAgentService
        {
            get { return ServiceClient.GetService<IOperationAgentService>(); }
        }

        /// <summary>
        /// 当前选中的提单
        /// </summary>
        public OceanBLList OceanBLList { get; set; }

        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        /// <summary>
        /// EDI客户端服务
        /// </summary>
        public IEDIClientService ediClientService
        {
            get
            {
                return ServiceClient.GetClientService<IEDIClientService>();
            }
        }

        RefreshService RefreshService
        {
            get { return ClientHelper.Get<RefreshService, RefreshService>(); }
        }
        #endregion

        #region 常量
        /// <summary>
        /// 代理分发文档界面句柄
        /// </summary>
        public IntPtr agentDispatchMailForm;

        #endregion

        #region Init

        public OEBLListPart()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.CurrentChanged = null;
                this.KeyDown = null;
                this.treeMain.CustomDrawNodeIndicator -= this.treeMain_CustomDrawNodeIndicator;
                //this.treeMain.CustomDrawNodeCell
                this.treeMain.KeyDown -= this.treeMain_KeyDown;
                this.treeMain.MouseDoubleClick -= this.treeMain_MouseDoubleClick;
                this.treeMain.NodeCellStyle -= this.treeMain_NodeCellStyle;
                this.treeMain.DataSource = null;
                this.bsList.DataSource = null;
                this.bsList.PositionChanged -= this.bsMainList_PositionChanged;
                this.bsList.Dispose();

                if (RefreshService != null && RefreshService.Refresh != null)
                {
                    RefreshService.Refresh -= RefreshReviseState;
                }

                if (RefreshService != null && RefreshService.RefreshAcceptReviseState != null)
                {
                    RefreshService.RefreshAcceptReviseState -= RefreshAcceptReviseState;
                }

                if (orgSource != null) orgSource.Clear(); orgSource = null;
                if (Workitem != null) Workitem.Items.Remove(this);
            };

            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();

            if (RefreshService != null)
            {
                RefreshService.Refresh += RefreshReviseState;
                RefreshService.RefreshAcceptReviseState += RefreshAcceptReviseState;
            }
        }

        private void RefreshAcceptReviseState()
        {
            this.treeMain.CloseEditor();
            CurrentRow.DispatchState = "Revised";

            treeMain.Refresh();

            Control[] ss = this.FindForm().Controls.Find("toolBar", true);
            if (ss != null && ss.Length > 0)
            {
                OEBLToolBar toolBar = ss[0] as OEBLToolBar;
                toolBar.barDocumentDispatch.Enabled = true;
                toolBar.barAcceptBillRevise.Enabled = false;
            }
        }

        private void RefreshReviseState()
        {
            Control[] ss = this.FindForm().Controls.Find("historyOceanRecordPart", true);
            if (ss != null && ss.Length > 0)
            {
                HistoryOceanRecordPart hrpart = ss[0] as HistoryOceanRecordPart;
                hrpart.BindingData();
            }
            ss = this.FindForm().Controls.Find("eventListPart", true);
            if (ss != null && ss.Length > 0)
            {
                EventListPart eventListPart = ss[0] as EventListPart;
                eventListPart.DataBind(new BusinessOperationContext() { OperationID = CurrentRow.OceanBookingID });
            }
        }

        private void SetCnText()
        {
            colState.Caption = "状态";
            colRefNo.Caption = "业务号";
            colNo.Caption = "提单号";
            colSoNo.Caption = "订舱号";
            colCustomerName.Caption = "客户";
            colVesselVoyage.Caption = "船名航次";
            colPOLName.Caption = "装货港";
            colPODName.Caption = "卸货港";
            colPlaceOfDeliveryName.Caption = "交货地";
            colETD.Caption = "离港日";
            colIssueType.Caption = "类型";
            colSales.Caption = "揽货人";
            colCreateDate.Caption = "创建时间";
            colState.Caption = "状态";
            colContainerNos.Caption = "箱号";
            colIssueType.Caption = "签单类型";
            colETA.Caption = "到港日";
            colBookingerName.Caption = "订舱";
            colFiler.Caption = "文件";
            colReleaseType.Caption = "放单类型";
            colConsignee.Caption = "收货人";
            colNotifyParty.Caption = "通知人";
            colShipper.Caption = "发货人";
            colAgentOfCarrierName.Caption = "承运人";
            colDispatchDate.Caption = "分发时间";
            colDispatchState.Caption = "分发状态";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            InitControls();
        }

        private void InitControls()
        {
            treeMain.ExpandAll();
        }

        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }
        public OceanBLList CurrentRow
        {
            get { return Current as OceanBLList; }
        }
        List<OceanBLList> SelectedItems
        {
            get
            {
                if (treeMain.Selection == null || treeMain.Selection.Count == 0) return null;

                List<OceanBLList> tagers = new List<OceanBLList>();
                foreach (TreeListNode item in treeMain.Selection)
                {
                    OceanBLList bl = treeMain.GetDataRecordByNode(item) as OceanBLList;
                    tagers.Add(bl);
                }
                return tagers;

            }
        }


        List<OceanBLList> orgSource = null;
        /// <summary>
        /// List OceanBLList,缓存一个原始数据源,在更变(ALL,MBL,HBL)视图时切换bsList的DataSource
        /// </summary>
        public override object DataSource
        {
            get
            {
                return orgSource;
            }
            set
            {
                treeMain.BeginUpdate();

                orgSource = value as List<OceanBLList>;
                if (orgSource == null || orgSource.Count == 0)
                {
                    bsList.DataSource = orgSource;
                    bsList.ResetBindings(false);
                    if (CurrentChanged != null) CurrentChanged(this, Current);
                }
                else
                {
                    SetListSourceByVisibleMode();
                }

                if (orgSource == null || orgSource.Count == 0) treeMain.IndicatorWidth = 15;
                else
                {
                    int count = orgSource.Count.ToString().Length - 1;
                    int width = 10 + count * 10;
                    treeMain.IndicatorWidth = 15 + width;
                }

                //treeMain.BestFitColumns();
                treeMain.ExpandAll();

                treeMain.EndUpdate();
            }
        }

        private void SetListSourceByVisibleMode()
        {
            if (_VisibleMode == VisibleMode.ALL)
                bsList.DataSource = orgSource;
            else if (orgSource != null)
            {
                if (_VisibleMode == VisibleMode.MBL)
                    bsList.DataSource = orgSource.FindAll(delegate(OceanBLList item) { return item.BLType == FCMBLType.MBL; });
                else
                    bsList.DataSource = orgSource.FindAll(delegate(OceanBLList item) { return item.BLType == FCMBLType.HBL; });
            }
            bsList.ResetBindings(false);

            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        public override void Refresh(object items)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                List<OceanBLList> list = this.DataSource as List<OceanBLList>;
                if (list == null) return;
                List<OceanBLList> newLists = items as List<OceanBLList>;

                foreach (var item in newLists)
                {
                    OceanBLList tager = list.Find(delegate(OceanBLList jItem) { return item.ID == jItem.ID; });
                    if (tager == null) continue;

                    OEUtility.CopyToValue(item, tager, typeof(OceanBLList));
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

        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;

        public new event KeyEventHandler KeyDown;
        #endregion

        #region TreeView Event

        private void bsMainList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        protected virtual void GvMainDoubleClick()
        {
            if (CurrentRow != null) Workitem.Commands[OEBLCommandConstants.Command_EditData].Execute();
        }

        private void treeMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) GvMainDoubleClick();
        }
        private void treeMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) GvMainDoubleClick();
            else if (e.KeyCode == Keys.F5)
            {
                if (this.KeyDown != null && treeMain.FocusedColumn != null && treeMain.FocusedNode != null)
                {
                    string text = treeMain.FocusedNode.GetDisplayText(treeMain.FocusedColumn);
                    Dictionary<string, object> keyValue = new Dictionary<string, object>();
                    keyValue.Add(treeMain.FocusedColumn.FieldName, text);
                    this.KeyDown(keyValue, null);
                }
            }
            else if (e.KeyCode == Keys.F6)
            {
                Workitem.Commands[OEBLCommandConstants.Command_ShowSearch].Execute();
            }
        }

        private void treeMain_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node == null) return;
            OceanBLList listData = treeMain.GetDataRecordByNode(e.Node) as OceanBLList;
            if (listData == null) return;

            if (listData.IsValid == false)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Disabled);
            }

            //如果没有放单  ETA显示蓝色：没有放单并且ETA-10<TODAY的业务；
            if (e.Column.FieldName == "ETA" && listData.ETA != null && listData.RBLD == false)
            {
                DateTime dtETA = Convert.ToDateTime(listData.ETA);
                dtETA.AddDays(-10);

                if (DateTime.Now.CompareTo(dtETA) > 0)
                {
                    e.Appearance.BackColor = System.Drawing.Color.DeepSkyBlue;
                }
            }

            //else if (listData.State == OEBLState.Checking)
            //{
            //    e.Appearance.ForeColor = OEBLColorConstant.CheckingColor;
            //    e.Appearance.Options.UseForeColor = true;
            //}
            //else if (listData.State == OEBLState.Checked)
            //{
            //    e.Appearance.ForeColor = OEBLColorConstant.CheckedColor;
            //    e.Appearance.Options.UseForeColor = true;
            //}
            //else if (listData.State == OEBLState.Release)
            //{
            //    e.Appearance.ForeColor = OEBLColorConstant.ReleaseColor;
            //    e.Appearance.Options.UseForeColor = true;
            //}

        }

        /// <summary>
        /// 绘制行号
        /// </summary>
        private void treeMain_CustomDrawNodeIndicator(object sender, DevExpress.XtraTreeList.CustomDrawNodeIndicatorEventArgs e)
        {
            if (e.IsNodeIndicator == false || e.ObjectArgs == null) return;

            DevExpress.Utils.Drawing.IndicatorObjectInfoArgs args = e.ObjectArgs as DevExpress.Utils.Drawing.IndicatorObjectInfoArgs;
            if (args != null)
            {
                int rowNum = treeMain.GetVisibleIndexByNode(e.Node) + 1;
                args.DisplayText = rowNum.ToString();
            }
        }

        #endregion

        #region Workitem Common

        #region 增删

        [CommandHandler(OEBLCommandConstants.Command_AddMBL)]
        public void Command_AddMBL(object sender, EventArgs e)
        {
            ClientOceanExportService.AddMBL(null, AfterMBLEditPartSaved);

        }


        [CommandHandler(OEBLCommandConstants.Command_AddHBL)]
        public void Command_AddHBL(object sender, EventArgs e)
        {
            ClientOceanExportService.AddHBL(null, AfterHBLEditPartSaved);
        }

        [CommandHandler(OEBLCommandConstants.Command_CopyData)]
        public void Command_CopyData(object sender, EventArgs e)
        {
            Copy(CurrentRow);
        }

        public void Copy(OceanBLList listData)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (listData == null) return;

                if (listData.BLType == FCMBLType.MBL)
                {
                    OECommonUtility.InnerCopyMBLData(Workitem, listData, ICPCommUIHelper, AfterMBLEditPartSaved);
                }
                else
                {
                    OECommonUtility.InnerCopyHBLData(Workitem, listData, ICPCommUIHelper, AfterHBLEditPartSaved);
                }
            }
        }


        [CommandHandler(OEBLCommandConstants.Command_EditData)]
        public void Command_EditData(object sender, EventArgs e)
        {
            Edit(CurrentRow);
        }

        public void Edit(OceanBLList listData)
        {
            if (listData == null) return;
            if (listData.BLType == FCMBLType.MBL)
            {
                ClientOceanExportService.EditMBL(listData.RefNo, listData.No, null, AfterMBLEditPartSaved);
            }
            else
            {
                ClientOceanExportService.EditHBL(listData.RefNo, listData.No, null, AfterHBLEditPartSaved);
            }
        }

        public void AfterMBLEditPartSaved(object[] prams)
        {
            //if (InvokeRequired)
            //{
            //    Action<object[]> afterMBLEditPartSaved = AfterMBLEditPartSaved;
            //    Invoke(afterMBLEditPartSaved, new object[] { prams });
            //}
            //else
            //{
                if (this.IsDisposed || this.Parent.IsDisposed)
                    return;
                OECommonUtility.InnerRefershMBLDataSource(prams, DataSource, bsList);
                if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
            //}
        }

        public void AfterDocumentDispatchSaved(object[] prams)
        {
            if (prams.Count() == 0)
            {
                return;
            }
            string type = prams[0] as string;
            CurrentRow.DispatchState = type;
            bsList.ResetBindings(false);
            //if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
        }

        public void AfterHBLEditPartSaved(object[] prams)
        {
            //if (InvokeRequired)
            //{
            //    Action<object[]> afterHBLEditPartSaved = AfterHBLEditPartSaved;
            //    Invoke(afterHBLEditPartSaved, new object[] { prams });
            //}
            //else
            //{
                if (this.IsDisposed || this.Parent.IsDisposed)
                    return;
                OECommonUtility.InnerRefershHBLDataSource(prams, DataSource, bsList);
                if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
            //}
        }

        [CommandHandler(OEBLCommandConstants.Command_DeleteData)]
        public void Command_DeleteData(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;
            if (CurrentRow.RBLD)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "The bill of lading has been released, unable to delete this bill of lading!" : "该分提单已放单，不能删除该提单！");
                return;
            }
            try
            {
                FCMBLType type = CurrentRow.BLType;
                if (OECommonUtility.InnerDelete(CurrentRow, DataSource, bsList))
                {
                    if (type == FCMBLType.HBL)
                    {
                        SetListSourceByVisibleMode();
                    }

                    bsMainList_PositionChanged(null, null);
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(),
                       CurrentRow.No + " " + (LocalData.IsEnglish ? "Delete Successfully" : "删除成功"));
                }
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }

        #endregion


        #region 分发文档
        [CommandHandler(OEBLCommandConstants.Command_DocumentDispatch)]
        public void Command_DocumentDispatch(object sender, EventArgs e)
        {
            if (Current == null) return;

            OceanBLList = Current as OceanBLList;

            if (string.IsNullOrEmpty(OceanBLList.AgentName))
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Agent couldn't be blank, please select [Agent].." : "代理不允许为空,请先选择代理..");
                return;
            }

            //箱号为空, 不允许下载
            if (OceanBLList.OEOperationType != FCMOperationType.LCL && string.IsNullOrEmpty(OceanBLList.ContainerNos))
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Ctn NO. couldn't be blank,please select Ctn NO.." : "箱号不允许为空,请先选择箱号..");
                return;
            }

            //船名航次为空 不允许下载
            if (string.IsNullOrEmpty(OceanBLList.VesselVoyage))
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Vessel and voyage No. couldn't be blank,please select vessel and voyage No.." : "船名航次不允许为空,请先选择船名航次..");
                return;
            }

            //if (OceanBLList.DocumentState == DocumentState.Reviseing)
            //{
            //    MessageBoxService.ShowInfo(LocalData.IsEnglish ? "D/C Fees have been revised by the agent, you must accept the revised fees first.." : "代理已经修订了代理账单费用，您必须先签收此次修订..");
            //    return;
            //}

            try
            {
                //FCMCommonClientService.DispatchDocument(BuildBussinessInfo(), AfterDocumentDispatchSaved);
                FCM.Common.UI.FCMUIUtility.ShowDispatchDocumentNew(this.Workitem, BuildBussinessInfo(), AfterDocumentDispatchSaved, 1);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
        }

        [CommandHandler(OEBLCommandConstants.Command_Dispatch)]
        public void Command_Dispatch(object sender, EventArgs e)
        {
            if (Current == null) return;

            OceanBLList = Current as OceanBLList;

            if (string.IsNullOrEmpty(OceanBLList.AgentName))
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Agent couldn't be blank, please select [Agent].." : "代理不允许为空,请先选择代理..");
                return;
            }

            //箱号为空, 不允许下载
            if (OceanBLList.OEOperationType != FCMOperationType.LCL && string.IsNullOrEmpty(OceanBLList.ContainerNos))
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Ctn NO. couldn't be blank,please select Ctn NO.." : "箱号不允许为空,请先选择箱号..");
                return;
            }

            //船名航次为空 不允许下载
            if (string.IsNullOrEmpty(OceanBLList.VesselVoyage))
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Vessel and voyage No. couldn't be blank,please select vessel and voyage No.." : "船名航次不允许为空,请先选择船名航次..");
                return;
            }

            //if (OceanBLList.DocumentState == DocumentState.Reviseing)
            //{
            //    MessageBoxService.ShowInfo(LocalData.IsEnglish ? "D/C Fees have been revised by the agent, you must accept the revised fees first.." : "代理已经修订了代理账单费用，您必须先签收此次修订..");
            //    return;
            //}

            try
            {
                FCM.Common.UI.FCMUIUtility.ShowDispatchDocumentNew(this.Workitem, BuildBussinessInfo(), AfterDocumentDispatchSaved, 1);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
        }


        void form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ICP.Framework.ClientComponents.Forms.PopupWindow mainForm = sender as ICP.Framework.ClientComponents.Forms.PopupWindow;
                mainForm.Close();
            }
        }

        BusinessOperationContext BuildBussinessInfo()
        {
            BusinessOperationContext context = BusinessOperationContext.Current;
            context.OperationID = OceanBLList.OceanBookingID;
            context.OperationType = OperationType.OceanExport;
            context.FormType = FormType.Booking;
            context.FormId = CurrentRow.ID;
            return context;
        }

        void AgentDispatchClosed(object sender, FormClosedEventArgs e)
        {
            agentDispatchMailForm = new IntPtr(0);
        }

        #endregion

        #region 对单

        [CommandHandler(OEBLCommandConstants.Command_Check)]
        public void Command_Check(object sender, EventArgs e)
        {
            if (CurrentRow == null || CurrentRow.State == OEBLState.Checking) return;

            try
            {
                ICP.Framework.CommonLibrary.Common.SingleResult result;
                if (CurrentRow.BLType == FCMBLType.MBL)
                    result = OceanExportService.ChangeOceanMBLState(CurrentRow.ID, OEBLState.Checking, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);
                else
                    result = OceanExportService.ChangeOceanHBLState(CurrentRow.ID, OEBLState.Checking, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);

                OceanBLList currentData = CurrentRow;
                currentData.State = OEBLState.Checking;
                currentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");

                bsList.ResetCurrentItem();
                bsMainList_PositionChanged(null, null);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(),
                    CurrentRow.No + " " + (LocalData.IsEnglish ? "Begin Check." : "开始对单."));
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }

        [CommandHandler(OEBLCommandConstants.Command_CompleteCheck)]
        public void Command_CompleteCheck(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            try
            {
                ICP.Framework.CommonLibrary.Common.SingleResult result;
                if (CurrentRow.BLType == FCMBLType.MBL)
                    result = OceanExportService.ChangeOceanMBLState(CurrentRow.ID, OEBLState.Checked, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);
                else
                    result = OceanExportService.ChangeOceanHBLState(CurrentRow.ID, OEBLState.Checked, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);

                OceanBLList currentData = CurrentRow;
                currentData.State = OEBLState.Checked;
                currentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");

                bsList.ResetCurrentItem();
                bsMainList_PositionChanged(null, null);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(),
                    CurrentRow.No + " " + (LocalData.IsEnglish ? "Check Done." : "完成对单."));
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }

        #endregion

        #region Print

        [CommandHandler(OEBLCommandConstants.Command_PrintBL)]
        public void Command_Print(object sender, EventArgs e)
        {
            if (CurrentRow == null)
                return;
            ClientOceanExportService.PrintBillOfLoading(CurrentRow.ID);
        }

        [CommandHandler(OEBLCommandConstants.Command_PrintProfit)]
        public void Command_PrintProfit(object sender, EventArgs e)
        {
            if (CurrentRow == null)
                return;
            ClientOceanExportService.PrintBookingProfit(CurrentRow.OceanBookingID);
        }

        [CommandHandler(OEBLCommandConstants.Command_PrintLoadCtn)]
        public void Command_PrintLoadCtn(object sender, EventArgs e)
        {
            //if (CurrentRow == null || CurrentRow.MBLID ==Guid.Empty) return;

            // OceanExportPrintHelper.PrintOELoadContainer(CurrentRow);
        }
        [CommandHandler(OEBLCommandConstants.Command_PrintLoadGoods)]
        public void Command_PrintLoadGoods(object sender, EventArgs e)
        {
            if (CurrentRow == null || CurrentRow.MBLID == Guid.Empty)
                return;
            ClientOceanExportService.PrintLoadGoods(CurrentRow);
        }

        /// <summary>
        /// 客户确认补料(中文)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OEBLCommandConstants.Command_MailBlCopyToCustomerCHS)]
        public void Command_MailBlCopyToCustomerCHS(object sender, EventArgs e)
        {
            if (CurrentRow == null || CurrentRow.MBLID == Guid.Empty)
                return;
            OceanMBLInfo oceanMblInfo = null;
            OceanHBLInfo oceanHbl = null;
            if (CurrentRow.BLType == FCMBLType.MBL)
            {
                //MBL
                oceanMblInfo = new OceanMBLInfo
                {
                    No = CurrentRow.No,
                    ReleaseType = CurrentRow.ReleaseType,
                    ID = CurrentRow.MBLID
                };
            }
            else
            {
                //HBL;
                oceanHbl = new OceanHBLInfo
                {
                    No = CurrentRow.No,
                    ReleaseType = CurrentRow.ReleaseType,
                    ID = CurrentRow.ID
                };
            }

            ClientOceanExportService.MailCustomerAskForConfirmSI(false, CurrentRow.OceanBookingID,
                                                                              oceanHbl, oceanMblInfo);
        }

        /// <summary>
        /// 客户确认补料(英文)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OEBLCommandConstants.Command_MailBlCopyToCustomerENG)]
        public void Command_MailBlCopyToCustomerENG(object sender, EventArgs e)
        {
            if (CurrentRow == null || CurrentRow.MBLID == Guid.Empty)
                return;

            OceanMBLInfo oceanMblInfo = null;
            OceanHBLInfo oceanHbl = null;
            if (CurrentRow.BLType == FCMBLType.MBL)
            {
                //MBL
                oceanMblInfo = new OceanMBLInfo
                {
                    No = CurrentRow.No,
                    ReleaseType = CurrentRow.ReleaseType,
                    ID = CurrentRow.MBLID
                };
            }
            else
            {
                //HBL;
                oceanHbl = new OceanHBLInfo
                {
                    No = CurrentRow.No,
                    ReleaseType = CurrentRow.ReleaseType,
                    ID = CurrentRow.ID
                };
            }

            ClientOceanExportService.MailCustomerAskForConfirmSI(true, CurrentRow.OceanBookingID,
                                                                              oceanHbl, oceanMblInfo);

        }

        private ICP.Message.ServiceInterface.Message GetOperationInfo()
        {
            if (CurrentRow == null)
                return null;
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.UserProperties = new ICP.Message.ServiceInterface.MessageUserPropertiesObject();
            message.UserProperties.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.AirImport;
            message.UserProperties.OperationId = CurrentRow.ID;
            message.UserProperties.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
            message.UserProperties.FormId = CurrentRow.ID;

            return message;
        }

        #endregion

        #region LoadShip RefreshData ReplyAgent

        [CommandHandler(OEBLCommandConstants.Command_LoadShip)]
        public void Command_LoadShip(object sender, EventArgs e)
        {
            if (CurrentRow == null || ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(CurrentRow.MBLID)) return;
            ClientOceanExportService.LoadShip(CurrentRow.MBLID, CurrentRow.No, null, AfterLoadShip);
        }

        /// <summary>
        /// 装船完成后
        /// </summary>
        void AfterLoadShip(object[] prams)
        {
            if (prams == null || prams.Length == 0) return;

            Guid shippingOrderId = new Guid(prams[0].ToString());
            List<OceanBLList> source = this.DataSource as List<OceanBLList>;
            if (source == null || source.Count == 0) return;

            List<OceanBLList> needUpdateBLs = source.FindAll(delegate(OceanBLList item) { return item.ShippingOrderID == shippingOrderId; });
            if (needUpdateBLs == null || needUpdateBLs.Count == 0) return;

            List<Guid> needUpdateIds = new List<Guid>();
            foreach (var bl in needUpdateBLs) { needUpdateIds.Add(bl.ID); }
            List<OceanBLList> bls = OceanExportService.GetOceanBLListByIds(needUpdateIds.ToArray());

            foreach (var bl in needUpdateBLs)
            {
                OceanBLList temp = bls.Find(delegate(OceanBLList item) { return item.ID == bl.ID; });
                if (temp != null)
                {
                    bl.State = temp.State;
                    bl.UpdateDate = temp.UpdateDate;
                    bl.ShippingOrderUpdateDate = temp.ShippingOrderUpdateDate;
                    bl.VesselVoyage = temp.VesselVoyage;
                }
            }
            SetListSourceByVisibleMode();
        }

        [CommandHandler(OEBLCommandConstants.Command_RefreshData)]
        public void Command_RefreshData(object sender, EventArgs e)
        {
            try
            {
                OECommonUtility.InnerRefersh(bsList);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
        }

        [CommandHandler(OEBLCommandConstants.Command_ReplyAgent)]
        public void Command_ReplyAgent(object sender, EventArgs e)
        {
            if (CurrentRow == null)
                return;
            ClientOceanExportService.ReplyAgent(CurrentRow.OceanBookingID, null, null);
        }

        #endregion

        #region 未完成

        [CommandHandler(OEBLCommandConstants.Command_ConfirmReleaseBL)]
        public void Command_ConfirmReleaseBL(object sender, EventArgs e)
        {
            if (CurrentRow == null || CurrentRow.State != OEBLState.Checked) return;

            try
            {

                if (DevExpress.XtraEditors.XtraMessageBox.Show((LocalData.IsEnglish ? "Sure Release BL" : "确认放单吗") + "?"
                             , LocalData.IsEnglish ? "Tip" : "提示"
                             , MessageBoxButtons.YesNo
                             , MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                ICP.Framework.CommonLibrary.Common.SingleResult result;
                if (CurrentRow.BLType == FCMBLType.MBL)
                    result = OceanExportService.ChangeOceanMBLState(CurrentRow.ID, OEBLState.Release, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);
                else
                    result = OceanExportService.ChangeOceanHBLState(CurrentRow.ID, OEBLState.Release, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);

                OceanBLList currentData = CurrentRow;
                currentData.State = OEBLState.Release;
                currentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");

                bsList.ResetCurrentItem();
                bsMainList_PositionChanged(null, null);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Release." : "已放单.");
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }
        [CommandHandler(OEBLCommandConstants.Command_E_MBL)]
        public void Command_E_MBL(object sender, EventArgs e)
        {
            bool isSucc = false;
            try
            {
                OceanMBLInfo mbl = OceanExportService.GetOceanMBLInfo(CurrentRow.MBLID);
                if (mbl.BookingPartyID == null || mbl.BookingPartyID == Guid.Empty)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "MBL has no booking party." : "MBL提单没有订舱人.", LocalData.IsEnglish ? "Tip" : "提示",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Warning);
                    return;
                }

                ConfigureInfo companyConfig = ConfigureService.GetCompanyConfigureInfoByCustomer((Guid)mbl.BookingPartyID);
                if (companyConfig == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "MBL's booking party is wrong, cannot send EDI." : "MBL提单订舱人无效，不能发送EDI.", LocalData.IsEnglish ? "Tip" : "提示",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Warning);
                    return;
                }
                if (companyConfig.CompanyID != CurrentRow.CompanyID)
                {
                    string message = LocalData.IsEnglish ? "MBL's booking party and MBL's company are different" + Environment.NewLine + "Are you sure send EDI by [" + companyConfig.CompanyName + "]" : "MBL提单订舱人所属公司和提单所属公司不同" + Environment.NewLine + "是否确定以订舱人[" + companyConfig.CompanyName + "]发送EDI？";
                    DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(message,
                                  LocalData.IsEnglish ? "Tip" : "提示",
                                  MessageBoxButtons.YesNo,
                                  MessageBoxIcon.Question);
                    if (result == DialogResult.No)
                        return;
                }

                OECommonUtility.InnerEMBL(ediClientService, SelectedItems, CurrentRow.CompanyID, AMSEntryType.Unknown, ACIEntryType.Unknown, ref isSucc);
                if (isSucc)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Send Successfully!" : "发送成功!");
                }

            }
            catch (Exception ex)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Send failed" : "发送失败!" + System.Environment.NewLine + ex.Message);
            }
        }

        [CommandHandler(OEBLCommandConstants.Command_NBEDIANL)]
        public void Command_NBEDIANL(object sender, EventArgs e)
        {
            bool isSucc = false;
            try
            {
                OECommonUtility.InnerEMBLANL(ediClientService, SelectedItems, CurrentRow.CompanyID, ref isSucc);
                if (isSucc)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Send Successfully!" : "发送成功!");
                }

            }
            catch (Exception ex)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Send failed" : "发送失败!" + System.Environment.NewLine + ex.Message);
            }
        }

        [CommandHandler(OEBLCommandConstants.Command_Pre)]
        public void Command_Pre(object sender, EventArgs e)
        {
            bool isSucc = false;
            try
            {
                OECommonUtility.InnerEMBLPro(ediClientService, SelectedItems, CurrentRow.CompanyID, ref isSucc);
                if (isSucc)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Send Successfully!" : "发送成功!");
                }

            }
            catch (Exception ex)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Send failed" : "发送失败!" + System.Environment.NewLine + ex.Message);
            }
        }

        /// <summary>
        /// 宁波edi中心
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OEBLCommandConstants.Command_NBEDI)]
        public void Command_NBEDI(object sender, EventArgs e)
        {
            bool isSucc = false;
            try
            {
                OECommonUtility.NBEDI(ediClientService, SelectedItems, CurrentRow.CompanyID, ref isSucc);
                if (isSucc)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Send Successfully!" : "发送成功!");
                }

            }
            catch (Exception ex)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Send failed" : "发送失败!" + System.Environment.NewLine + ex.Message);
            }
        }


        [CommandHandler(OEBLCommandConstants.Command_CopyAMS)]
        public void Command_CopyAMS(object sender, EventArgs e)
        {
            try
            {
                List<OceanBLList> hbllist = SelectedItems.FindAll(m => m.BLType == FCMBLType.HBL);
                int i = hbllist.Count;
                if (i != 1)
                {
                    MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Please select only one HBL." : "请选择一个HBL提单.");
                    return;
                }
                PartLoader.ShowEditPartInDialog<ICP.FCM.OceanExport.UI.MBL.CopyAMSToHBL>(Workitem, hbllist[0], "Copy AMS To Other HBL", null);
            }
            catch (Exception ex)
            {
                MessageBoxService.ShowInfo(ex.Message);
            }
        }

        [CommandHandler(OEBLCommandConstants.Command_ISF)]
        public void Command_ISF(object sender, EventArgs e)
        {

        }
        [CommandHandler(OEBLCommandConstants.Command_Bill)]
        public void Command_Bill(object sender, EventArgs e)
        {
            if (CurrentRow == null)
                return;
            ClientOceanExportService.OpenBill(this.CurrentRow.OceanBookingID, OperationType.OceanExport);
        }

        #endregion

        #region Visible Mode

        VisibleMode _VisibleMode = VisibleMode.ALL;

        [CommandHandler(OEBLCommandConstants.Command_VisibleALL)]
        public void Command_VisibleALL(object sender, EventArgs e)
        {
            if (_VisibleMode == VisibleMode.ALL) return;
            _VisibleMode = VisibleMode.ALL;
            SetListSourceByVisibleMode();

        }
        [CommandHandler(OEBLCommandConstants.Command_VisibleMBL)]
        public void Command_VisibleMBL(object sender, EventArgs e)
        {
            if (_VisibleMode == VisibleMode.MBL) return;
            _VisibleMode = VisibleMode.MBL;
            SetListSourceByVisibleMode();

        }
        [CommandHandler(OEBLCommandConstants.Command_VisibleHBL)]
        public void Command_VisibleHBL(object sender, EventArgs e)
        {
            if (_VisibleMode == VisibleMode.HBL) return;
            _VisibleMode = VisibleMode.HBL;
            SetListSourceByVisibleMode();
        }

        #endregion

        #region Split Merge

        [CommandHandler(OEBLCommandConstants.Command_SplitBL)]
        public void Command_SplitBL(object sender, EventArgs e)
        {
            if (CurrentRow == null)
                return;
            ClientOceanExportService.SplitBillOfLoading(CurrentRow, null, AfterSplitBL);
        }
        private void AfterSplitBL(object[] parameters)
        {

            List<Guid> ids = parameters[0] as List<Guid>;
            if (ids == null || ids.Count == 0) return;
            List<OceanBLList> newList = OceanExportService.GetOceanBLListByIds(ids.ToArray());
            List<OceanBLList> sources = DataSource as List<OceanBLList>;
            List<OceanBLList> needRemove = sources.FindAll(delegate(OceanBLList item) { return ids.Contains(item.ID); });
            foreach (var item in needRemove) { sources.Remove(item); }
            sources.InsertRange(0, newList);
            bsList.DataSource = null;
            bsList.DataSource = sources;
            bsList.ResetBindings(false);

        }

        [CommandHandler(OEBLCommandConstants.Command_Merge)]
        public void Command_Merge(object sender, EventArgs e)
        {
            ClientOceanExportService.MergeBillOfLoading(this.SelectedItems, AfterMergeBillOfLoading);
        }

        private void AfterMergeBillOfLoading(object[] parameters)
        {
            SingleResult result = parameters[0] as SingleResult;
            List<Guid> ids = parameters[1] as List<Guid>;

            List<OceanBLList> blLists = bsList.DataSource as List<OceanBLList>;
            List<OceanBLList> needRemove = blLists.FindAll(delegate(OceanBLList item) { return ids.Contains(item.ID); });
            if (needRemove != null && needRemove.Count > 0)
            {
                foreach (var item in needRemove) { blLists.Remove(item); }
            }

            OceanBLList needRefreshData = blLists.Find(delegate(OceanBLList item) { return item.ID == result.GetValue<Guid>("ID"); });
            needRefreshData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
            needRefreshData.ContainerNos = result.GetValue<string>("ContainerNos");

            bsList.DataSource = blLists;
            bsList.ResetBindings(false);
        }

        #endregion

        #region 核销单

        [CommandHandler(OEBLCommandConstants.Command_VerifiSheet)]
        public void Command_VerifiSheet(object sender, EventArgs e)
        {
            if (CurrentRow == null || CurrentRow.IsNew)
            {
                return;
            }
            ClientOceanExportService.OpenVerifiSheet(this.CurrentRow.OceanBookingID, this.CurrentRow.RefNo);
        }
        #endregion
        #region 装箱
        /// <summary>
        /// 装箱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OEBLCommandConstants.Command_LoadContainer)]
        public void Command_LoadContainer(object sender, EventArgs e)
        {
            if (CurrentRow == null || CurrentRow.IsNew)
            {
                return;
            }
            ClientOceanExportService.LoadContainer(this.CurrentRow.RefNo, this.CurrentRow.OceanBookingID, null, null);

        }

        #endregion

        #region 装箱
        /// <summary>
        /// 签收修订
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OEBLCommandConstants.Command_ReviseAccept)]
        public void Command_ReviseAccept(object sender, EventArgs e)
        {
            if (CurrentRow == null || CurrentRow.IsNew)
            {
                return;
            }
            if (CurrentRow.DocumentState == DocumentState.Reviseing)
            {
                //ICP.FCM.Common.UI.Utility.ShowReviseAccepte(Workitem, CurrentRow.OceanBookingID);
                ICP.FCM.Common.UI.FCMUIUtility.ShowReviseAccepteNew(Workitem, CurrentRow.OceanBookingID);
            }
        }

        #endregion


        #region 获取一个用户的分文件记录
        /// <summary>
        /// 获取一个用户的分文件记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OEBLCommandConstants.Command_DispatchLog)]
        public void Command_DispatchLog(object sender, EventArgs e)
        {
            ICP.FCM.Common.UI.DipatchLogForm.DispatchFileLogShow countPart = Workitem.Items.AddNew<ICP.FCM.Common.UI.DipatchLogForm.DispatchFileLogShow>();
            string title = LocalData.IsEnglish ? "分文档记录" : "分文档记录";
            PartLoader.ShowDialog(countPart, title);
        }

        #endregion

        #region 签收分文档
        /// <summary>
        /// 签收分文档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OEBLCommandConstants.Command_AcceptDispatch)]
        public void Command_AcceptDispatch(object sender, EventArgs e)
        {
            if (CurrentRow == null || CurrentRow.IsNew)
            {
                return;
            }

            int state = OperationAgentService.GetDispatchState(CurrentRow.OceanBookingID);
            if (state > 1 && state < 6)
            {
                ICP.FCM.Common.UI.FCMUIUtility.ShowReviseAccepteNew(this.Workitem, CurrentRow.OceanBookingID);
            }
            else
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "The bill does not need to be signed!" : "该业务不需要签收！");
            }
        }

        #endregion

        #endregion
    }

    enum VisibleMode
    {
        ALL, MBL, HBL
    }
}
