using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.Utility;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.ClientComponents.Controls;
using ICP.FCM.OceanImport.UI.Business.DownLoad;
using Wintellect.Threading.AsyncProgModel;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Drawing;
using DevExpress.XtraBars;
using ICP.FCM.Common.ServiceInterface;
using System.Threading;
using ICP.FCM.Common.UI.DispatchCompare;
using ICP.FCM.Common.UI;
using ICP.Business.Common.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using System.Reflection;
using System.Diagnostics;
using ICP.FileSystem.ServiceInterface;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.FCM.OceanImport.UI
{
    public partial class OIBusinessDownloadList : ICP.Framework.ClientComponents.UIFramework.BaseListPart, IDisposable
    {
        public OIBusinessDownloadList()
        {
            InitializeComponent();
            if (!LocalData.IsDesignMode)
            {
                this.Disposed += delegate
                {
                    this.gcMain.DataSource = null;
                    this.gvMain.MouseMove -= this.gvMain_MouseMove;
                    this.gvMain.RowCellClick -= this.gvMain_RowCellClick;
                    this.gvMain.RowStyle -= this.gvMain_RowStyle;
                    this.bsOEList.DataSource = null;
                    this.bsOEList.PositionChanged -= this.bsOEList_PositionChanged;
                    this.bsOEList.Dispose();
                    this.CurrentChanged = null;
                    this.Saved = null;
                    this.InsertToListAfterDownLoadEvent = null;
                    this.AgentDispatchInfoPart = null;
                    this.BusinessToolPart = null;
                    if (this.DocumentNotifyService != null)
                    {
                        this.DocumentNotifyService.DocumentError -= this.OnDocumentError;
                    }
                    if (this.RefreshService != null && RefreshService.Refresh != null)
                    {
                        RefreshService.Refresh -= RefershDownLoadData;
                    }
                    if (this.RefreshService != null && RefreshService.RefreshAcceptDispatchState != null)
                    {
                        RefreshService.RefreshAcceptDispatchState -= RefreshAcceptDispatchState;
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

        IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        public IClientFileService ClientFileService
        {
            get
            {
                return ServiceClient.GetService<IClientFileService>();
            }
        }


        public DocumentNotifyClientService DocumentNotifyService
        {
            get
            {
                return ClientHelper.Get<DocumentNotifyClientService, DocumentNotifyClientService>();
            }
        }

        public RefreshService RefreshService
        {
            get { return ClientHelper.Get<RefreshService, RefreshService>(); }
        }


        /// <summary>
        /// FCM公共服务
        /// </summary>
        public IOperationAgentService OperationAgentService
        {
            get
            {
                return ServiceClient.GetService<IOperationAgentService>();
            }
        }

        public IICPCommonOperationService ICPCommonOperationService
        {
            get
            {
                return ServiceClient.GetClientService<IICPCommonOperationService>();
            }
        }

        #endregion

        #region 常量
        /// <summary>
        /// 记录点击签收时的时间，刷新界面就直接去这个常量值，不需要数据库返回
        /// </summary>
        string arrAcceptTime = string.Empty;

        static int theradID = 0;

        NotifyType Type;

        public OIAgentDispatchInfoPart AgentDispatchInfoPart
        {
            get;
            set;
        }
        public OIBusinessDownloadTool BusinessToolPart
        {
            get;
            set;
        }
        #endregion

        #region 初始化

        private void gcMain_Click(object sender, EventArgs e)
        {

        }


        private void RefreshAcceptDispatchState()
        {
            this.gvMain.CloseEditor();
            CurrentRow.DocumentState = DocumentState.Accepted;
            //CurrentRow.DownLoadState = DownLoadState.Downloaded;
            //CurrentRow.DownLoadStateDescription = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<DownLoadState>(DownLoadState.Downloaded, LocalData.IsEnglish);
            CurrentRow.DocumentStateDescription = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<DocumentState>(DocumentState.Accepted, LocalData.IsEnglish);

            gvMain.RefreshData();
            Control[] ss = this.FindForm().Controls.Find("toolBar", true);
            if (ss != null && ss.Length > 0)
            {
                OIBusinessDownloadTool toolBar = ss[0] as OIBusinessDownloadTool;
                toolBar.barDownLoad.Enabled = false;
                toolBar.toolAccepted.Enabled = false;
            }
        }
        /// <summary>
        /// 签收后刷新文档状态
        /// </summary>
        private void RefershDownLoadData()
        {
            Control[] ss = this.FindForm().Controls.Find("historyOceanRecordPart", true);
            if (ss != null && ss.Length > 0)
            {
                HistoryOceanRecordPart hrpart = ss[0] as HistoryOceanRecordPart;
                hrpart.BindingData();
            }
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
            //gvMain.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(gvMain_RowCellClick);

            DataFindClientService.RegisterGridColumnFinder(colConsigneeID
                        , ICP.Common.ServiceInterface.CommonFinderConstants.CustoemrFinder
                        , "ConsigneeID"
                        , "ConsigneeName"
                        , "ID"
                        , LocalData.IsEnglish ? "EName" : "CName");

            cmbError.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(string.Empty, false, -1));
            cmbError.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(string.Empty, true, 0));
            //DocumentNotifyService.DocumentDispatched += OnDocumentDispatched;
            //DocumentNotifyService.DocumentAccepted += OnDocumentAccepted;
            //DocumentNotifyService.DocumentUnAccepted += OnDocumentUnAccepted;
            //DocumentNotifyService.DocumentAssignTo += OnDocumentAssignTo;
            DocumentNotifyService.DocumentError += OnDocumentError;
            RefreshService.Refresh += RefershDownLoadData;
            RefreshService.RefreshAcceptDispatchState += RefreshAcceptDispatchState;
            string startDate = OperationAgentService.GetStartDateForReviseAgentBill().ToShortDateString();

            colDownLoadDate.ToolTip = LocalData.IsEnglish ? string.Format("If the shipment's download time is greater than (or equal to) {0}," +
                " it will be operated with the rules of ICP E-Doc V2.0. So that the changed D/C fees will be updated automatically to " +
                "the internal agent's ICP, and only own D/C fees can be changed. Otherwise, please operate the shipment with the original" +
                " way if If the shipment's download time is less than {0}.", startDate)
                : string.Format("下载时间>={0}的业务，将启用ICP分文件V2.0的逻辑，即更改账单后可自动更新到代理（内部），" +
                " 并且不能修改代理创建的费用。下载时间<{0}的业务，请按照旧有模式作业。", startDate);
            colDownLoadDate.Width = LocalData.IsEnglish ? 100 : 70;
        }
        #endregion


        #region 外部接口
        /// <summary>
        /// 数据源
        /// </summary>
        /// 
        List<OceanBusinessDownLoadList> list = null;
        public override object DataSource
        {
            get
            {
                return bsOEList.DataSource;
            }
            set
            {
                list = value as List<OceanBusinessDownLoadList>;
                bsOEList.DataSource = list;
                bsOEList.ResetBindings(false);
                if (CurrentChanged != null)
                {
                    CurrentChanged(this, Current);
                }

                gvMain.BestFitColumns();

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

                //if (list.Count.ToString().Length == 1)
                //{
                //    gvMain.IndicatorWidth = 30;
                //}
                //else
                //{
                //    gvMain.IndicatorWidth = list.Count.ToString().Length * 17;
                //}

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), message);
            }
        }

        /// <summary>
        /// 当前行
        /// </summary>
        public override object Current
        {
            get
            {
                return bsOEList.Current as OceanBusinessDownLoadList;
            }
        }

        /// <summary>
        /// 获取选择的列表集合
        /// </summary>
        public List<OceanBusinessDownLoadList> CurrentList
        {

            get
            {
                int[] rowIndexs = gvMain.GetSelectedRows();

                if (rowIndexs.Length == 0) return null;

                List<OceanBusinessDownLoadList> tagers = new List<OceanBusinessDownLoadList>();
                foreach (var item in rowIndexs)
                {
                    OceanBusinessDownLoadList dr = gvMain.GetRow(item) as OceanBusinessDownLoadList;
                    if (dr != null) tagers.Add(dr);

                }
                return tagers;

            }

        }

        public OceanBusinessDownLoadList CurrentRow
        {
            get
            {
                return bsOEList.Current as OceanBusinessDownLoadList;
            }
        }

        /// <summary>
        /// 选择的行发生改变
        /// </summary>
        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;
        /// <summary>
        /// 下载数据
        /// </summary>
        public event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        // Declares a global scoped event that signals that a customer was added to the global list
        [EventPublication(OIBusinessCommandConstants.Command_InsertToListAfterDownLoad)]
        public event EventHandler<DataEventArgs<List<OceanBusinessList>>> InsertToListAfterDownLoadEvent;

        #endregion

        private void bsOEList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentRow == null)
            {
                return;
            }

            if (CurrentChanged != null)
            {
                CurrentChanged(this, CurrentRow);
            }
        }

        private void gvMain_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column == this.colCheck)
            {
                bsOEList.EndEdit();
                if (!CurrentRow.IsCheck)
                {
                    if (CurrentRow.DownLoadState == DownLoadState.Pending && (CurrentRow.HBLState == OEBLState.Draft || CurrentRow.HBLState == OEBLState.Checking) || (CurrentRow.HBLState == OEBLState.Unknown))
                    {
                        DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Are you sure you want to download it when it's still being held by Off-shore agent?" : "该业务港前客服没有处理完成,是否确定要下载?"
                            , LocalData.IsEnglish ? "Tip" : "提示"
                            , MessageBoxButtons.YesNo
                            , MessageBoxIcon.Question);

                        if (result != DialogResult.Yes)
                        {
                            CurrentRow.IsCheck = false;
                            return;
                        }
                    }

                    if (ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(CurrentRow.ConsigneeID))
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "It could not be downloaded because you have not yet fill in the consignee." : "不能下载,请输入收货人.", LocalData.IsEnglish ? "Tip" : "提示");
                        CurrentRow.IsCheck = false;
                        return;
                    }

                    //if (CurrentRow.PlaceofdeliveryDates == null)
                    //{
                    //    DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "It could not be downloaded because you have not yet fill in the Delivery Date." : "不能下载,请输入到交货地日.", LocalData.IsEnglish ? "Tip" : "提示");
                    //    CurrentRow.IsCheck = false;
                    //    return;
                    //}
                }
                bool isCheck = CurrentRow.IsCheck;
                var lst = list.Where(ss => ss.OceanBookingID == CurrentRow.OceanBookingID);

                foreach (var val in lst)
                {
                    val.IsCheck = !isCheck;
                }

                // CurrentRow.IsCheck = !CurrentRow.IsCheck;
                bsOEList.ResetCurrentItem();
                gvMain.RefreshData();
            }
        }

        #region  下载数据
        [CommandHandler(OIBusinessDownLoadCommandConstants.Command_DownLoad)]
        public void Command_DownLoad(object sender, EventArgs e)
        {
            this.Type = NotifyType.Download;
            bsOEList.EndEdit();

            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                List<OceanBusinessDownLoadList> dataList = DataSource as List<OceanBusinessDownLoadList>;
                if (dataList == null || dataList.Count == 0)
                {
                    return;
                }

                List<OceanBusinessDownLoadList> list = (from d in dataList where d.IsCheck select d).ToList<OceanBusinessDownLoadList>();

                BusinessDownLoad(list);

                BusinessOperationParameter businessOperationParameter = new BusinessOperationParameter();
                BusinessOperationContext operationContext = new BusinessOperationContext();
                businessOperationParameter.Context = operationContext;
                ICPCommonOperationService.AfterContainerInfoSaved(businessOperationParameter);
                MethodBase method = MethodBase.GetCurrentMethod();
                StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, method.DeclaringType.FullName, "DOWNLOAD", "业务下载");
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "DownLoad Failed" : "下载失败") + ex.Message);
            }
        }

        #endregion

        #region  下载数据新
        [CommandHandler(OIBusinessDownLoadCommandConstants.Command_DownLoadNew)]
        public void Command_DownLoadNew(object sender, EventArgs e)
        {
            this.Type = NotifyType.Download;
            bsOEList.EndEdit();

            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                List<OceanBusinessDownLoadList> dataList = DataSource as List<OceanBusinessDownLoadList>;
                if (dataList == null || dataList.Count == 0)
                {
                    return;
                }

                List<OceanBusinessDownLoadList> list = (from d in dataList where d.IsCheck select d).ToList<OceanBusinessDownLoadList>();

                BusinessDownLoadNew(list);

                BusinessOperationParameter businessOperationParameter = new BusinessOperationParameter();
                BusinessOperationContext operationContext = new BusinessOperationContext();
                businessOperationParameter.Context = operationContext;
                ICPCommonOperationService.AfterContainerInfoSaved(businessOperationParameter);
                MethodBase method = MethodBase.GetCurrentMethod();
                StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, method.DeclaringType.FullName, "DOWNDISPATCH","业务下载");
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "DownLoad Failed" : "下载失败") + ex.Message);
            }
        }

        #endregion

        private void OnDocumentError(List<Guid> oceanAgentDispatchIds, string errorMessage)
        {
            AsyncEnumerator async = new AsyncEnumerator();
            async.BeginExecute(ShowErrorMessageInfo(oceanAgentDispatchIds, errorMessage), async.EndExecute);
        }

        /// <summary>
        /// 业务下载
        /// </summary>
        /// <param name="downLoadList">下载列表</param>
        /// <returns>返回是否成功</returns>
        public bool BusinessDownLoad(List<OceanBusinessDownLoadList> downLoadList)
        {
            try
            {
                if (downLoadList == null || downLoadList.Count == 0)
                {
                    return false;
                }

                Guid? podagentid = Guid.Empty;

                //验证数据
                foreach (OceanBusinessDownLoadList oe in downLoadList)
                {
                    if (!oe.Validate())
                    {
                        return false;
                    }

                    if (podagentid == Guid.Empty)
                    {
                        podagentid = oe.AgentID;
                    }

                    //Add by Sunny
                    //下载时收货人不允许为空
                    if (oe.ConsigneeID == null || oe.ConsigneeID == Guid.Empty)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "Consignee couldn't be blank,please select consignee." : "收货人不允许为空,请先选择收货人"));
                        return false;
                    }
                    //船名航次为空 不允许下载
                    if (string.IsNullOrEmpty(oe.VesselVoyage))
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "Vessel and voyage No. couldn't be blank,please select vessel and voyage No.." : "船名航次不允许为空,请先选择船名航次"));
                        return false;
                    }

                }
                List<Guid> mblIDList = new List<Guid>();
                List<Guid> consineeIDList = new List<Guid>();
                List<DateTime?> detas = new List<DateTime?>();
                List<string> hblIDList = new List<string>();
                //转换数据
                foreach (OceanBusinessDownLoadList oe in downLoadList)
                {
                    if (oe.DownLoadState == DownLoadState.Pending)
                    {
                        mblIDList.Add(oe.ID);
                        consineeIDList.Add(oe.ConsigneeID);
                        detas.Add(oe.PlaceofdeliveryDates);
                        hblIDList.Add(oe.HBLIDs);
                    }

                }
                if (mblIDList.Count > 0)
                {
                    //OIAfterDownLoadRerurnData returnData = OceanImportService.DownLoadBusiness(mblIDList.ToArray(), consineeIDList.ToArray(), detas.ToArray(), hblIDList.ToArray(), LocalData.UserInfo.LoginID, podagentid);
                    OIAfterDownLoadRerurnData returnData = OceanImportService.DownLoadBusinessFromDispatchFile(mblIDList.ToArray(), consineeIDList.ToArray(), detas.ToArray(), hblIDList.ToArray(), LocalData.UserInfo.LoginID, podagentid);
                    if (returnData == null || returnData.PODRefNoList == null) return false;

                    #region 刷新列表

                    this.gvMain.CloseEditor();
                    for (int i = 0; i < downLoadList.Count; i++)
                    {
                        downLoadList[0].DownLoadDate = DateTime.Now;
                        downLoadList[i].PODRefNo = returnData.PODRefNoList[i];
                        downLoadList[i].RefID = returnData.PODRefIdList[i];
                        downLoadList[i].DownLoadState = DownLoadState.Downloaded;
                        downLoadList[i].DownLoadStateDescription = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<DownLoadState>(DownLoadState.Downloaded, LocalData.IsEnglish);
                        downLoadList[i].IsCheck = false;
                    }

                    gvMain.RefreshData();

                    #endregion

                    #region 在海进业务列表插入下载成功的业务
                    if (InsertToListAfterDownLoadEvent != null && returnData.BusinessList != null && returnData.BusinessList.Count > 0)
                    {
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? " DownLoad Successfully" : "下载成功");

                        InsertToListAfterDownLoadEvent(this, new DataEventArgs<List<OceanBusinessList>>(returnData.BusinessList));
                    }
                    #endregion
                }
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "DownLoad Failed" : "下载失败") + ex.Message);
                return false;
            }
        }

        #region 业务下载新
        /// <summary>
        /// 业务下载
        /// </summary>
        /// <param name="downLoadList">下载列表</param>
        /// <returns>返回是否成功</returns>
        public bool BusinessDownLoadNew(List<OceanBusinessDownLoadList> downLoadList)
        {
            try
            {
                if (downLoadList == null || downLoadList.Count == 0)
                {
                    return false;
                }

                Guid? podagentid = Guid.Empty;

                //验证数据
                foreach (OceanBusinessDownLoadList oe in downLoadList)
                {
                    if (!oe.Validate())
                    {
                        return false;
                    }

                    if (podagentid == Guid.Empty)
                    {
                        podagentid = oe.AgentID;
                    }

                    //Add by Sunny
                    //下载时收货人不允许为空
                    if (oe.ConsigneeID == null || oe.ConsigneeID == Guid.Empty)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "Consignee couldn't be blank,please select consignee." : "收货人不允许为空,请先选择收货人"));
                        return false;
                    }
                    //船名航次为空 不允许下载
                    if (string.IsNullOrEmpty(oe.VesselVoyage))
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "Vessel and voyage No. couldn't be blank,please select vessel and voyage No.." : "船名航次不允许为空,请先选择船名航次"));
                        return false;
                    }
                }
                List<Guid> mblIDList = new List<Guid>();
                List<Guid> consineeIDList = new List<Guid>();
                List<DateTime?> detas = new List<DateTime?>();
                List<string> hblIDList = new List<string>();
                //转换数据
                foreach (OceanBusinessDownLoadList oe in downLoadList)
                {
                    if (oe.DownLoadState == DownLoadState.Pending)
                    {
                        mblIDList.Add(oe.ID);
                        consineeIDList.Add(oe.ConsigneeID);
                        detas.Add(oe.PlaceofdeliveryDates);
                        hblIDList.Add(oe.HBLIDs);
                    }

                }
                if (mblIDList.Count > 0)
                {
                    OIAfterDownLoadRerurnData returnData = OceanImportService.DownLoadBusinessFromDispatchFile(mblIDList.ToArray(), consineeIDList.ToArray(), detas.ToArray(), hblIDList.ToArray(), LocalData.UserInfo.LoginID, podagentid);
                    if (returnData == null || returnData.PODRefNoList == null) return false;

                    #region 刷新列表

                    this.gvMain.CloseEditor();
                    for (int i = 0; i < downLoadList.Count; i++)
                    {
                        downLoadList[i].DownLoadDate = DateTime.Now;
                        downLoadList[i].PODRefNo = returnData.PODRefNoList[i];
                        downLoadList[i].RefID = returnData.PODRefIdList[i];
                        downLoadList[i].DownLoadState = DownLoadState.Downloaded;
                        downLoadList[i].DownLoadStateDescription = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<DownLoadState>(DownLoadState.Downloaded, LocalData.IsEnglish);
                        downLoadList[i].IsCheck = false;
                    }

                    gvMain.RefreshData();

                    #endregion

                    #region 在海进业务列表插入下载成功的业务
                    if (InsertToListAfterDownLoadEvent != null && returnData.BusinessList != null && returnData.BusinessList.Count > 0)
                    {
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? " DownLoad Successfully" : "下载成功");

                        InsertToListAfterDownLoadEvent(this, new DataEventArgs<List<OceanBusinessList>>(returnData.BusinessList));
                    }
                    #endregion
                }
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "DownLoad Failed" : "下载失败") + ex.Message);
                return false;
            }
        }
        #endregion

        IEnumerator<Int32> ShowErrorMessageInfo(List<Guid> oceanAgentDispatchIDs, string errorMessage)
        {
            if (oceanAgentDispatchIDs == null || oceanAgentDispatchIDs.Count == 0)
                yield return 1;
            List<OceanBusinessDownLoadList> dataList = DataSource as List<OceanBusinessDownLoadList>;
            if (dataList == null || dataList.Count == 0)
                yield return 1;
            foreach (Guid id in oceanAgentDispatchIDs)
            {
                OceanBusinessDownLoadList info = dataList.Find(o => o.OceanAgentDispatchID == id);
                if (info != null)
                {

                    ReturnToolEnabled();
                }
            }

            ResetDataBinding();
            yield return 1;
        }

        void SetControlData(string acceptByName, string arrAcceptTime, string assignToName)
        {
            if (AgentDispatchInfoPart != null)
            {
                AgentDispatchInfoPart.SetCtlData(acceptByName, arrAcceptTime, assignToName);
            }
        }

        void EnabledToolItem(bool? toolAcceptEnabled, bool? toolUnAccpetEnabled, bool? toolAssignToEnabled)
        {
            if (BusinessToolPart != null)
            {
                if (toolAcceptEnabled != null)
                {
                    BusinessToolPart.toolAccepted.Enabled = (bool)toolAcceptEnabled;
                }
                if (toolUnAccpetEnabled != null)
                {
                    BusinessToolPart.toolUnAccepted.Enabled = (bool)toolUnAccpetEnabled;
                }
                if (toolAssignToEnabled != null)
                {
                    BusinessToolPart.toolAssignTo.Enabled = (bool)toolAssignToEnabled;
                }
            }
        }

        void ReturnToolEnabled()
        {
            switch (Type)
            {
                case NotifyType.Accepted:
                    BusinessToolPart.toolAccepted.Enabled = true;
                    break;
                case NotifyType.UnAccepted:
                    BusinessToolPart.toolUnAccepted.Enabled = true;
                    break;
                case NotifyType.AssignTo:
                    BusinessToolPart.toolAssignTo.Enabled = true;
                    break;
                case NotifyType.Download:
                    break;
            }
        }

        void ShowErrorColumn(OceanBusinessDownLoadList info, string errorMessage)
        {
            info.ErrorInfo = errorMessage;
            info.IsError = true;
            colIsError.Visible = true;
            colIsError.VisibleIndex = 0;
        }

        void ResetDataBinding()
        {
            bsOEList.ResetBindings(false);
        }

        AgentDispatchParam CreateDispatchParamInfo(DocumentState state, Guid assignTo, string assignToName)
        {
            return new AgentDispatchParam() { DocumentState = state, AssignTo = assignTo, Name = assignToName, LoginId = LocalData.UserInfo.LoginID };
        }

        void OnActionNew(AgentDispatchParam param, bool isAssignToAction)
        {
            List<OceanBusinessDownLoadList> checkList = CurrentList;


            if (checkList == null || checkList.Count == 0)
            {
                ReturnToolEnabled();

                DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Check at least one row data." : "至少选中一行数据."
                            , LocalData.IsEnglish ? "Tip" : "提示"
                            , MessageBoxButtons.YesNo
                            , MessageBoxIcon.Question);
                return;
            }
            List<string> lstNos = new List<string>();
            List<OceanBusinessDownLoadList> lstRemoved = new List<OceanBusinessDownLoadList>();
            if (checkList.Count > 1)
            {
                foreach (OceanBusinessDownLoadList oe in checkList)
                {
                    if (oe.DownLoadState == DownLoadState.Downloaded && oe.DocumentState == DocumentState.Dispatched)
                    {
                        lstNos.Add(oe.RefNo);
                        lstRemoved.Add(oe);
                    }
                }
            }
            foreach (OceanBusinessDownLoadList oe in lstRemoved)
            {
                checkList.Remove(oe);
            }

            int sucessCount = 0;

            //清空上次显示错误信息
            ClearErrorColumnData(checkList);
            Stopwatch stopwatch = Stopwatch.StartNew();
            //验证数据
            foreach (OceanBusinessDownLoadList oe in checkList)
            {
                param.OceanAgentDispatchId = oe.OceanAgentDispatchID;
                if (!isAssignToAction)
                {
                    if (oe.DownLoadState == DownLoadState.Pending)
                    {
                        if (!BusinessDownLoadNew(new List<OceanBusinessDownLoadList> { oe }))
                        {
                            continue;
                        }
                    }

                    bool isSuccess = false;
                    try
                    {
                        List<SimpleBusinnessInfo> result = FCMCommonService.GetOEIDByOIID(oe.OceanBookingID);
                        string fileLogstr = FCMCommonService.GetDispatchNewLogID(oe.OceanBookingID);
                        Guid fileLog = JSONSerializerHelper.DeserializeFromJson<Guid>(fileLogstr);
                        OceanImportService.AcceptDispatchFiles(result[0].OIBusinessID, fileLog, LocalData.UserInfo.LoginID, LocalData.IsEnglish);
                        isSuccess = true;
                    }
                    catch (System.Exception ex)
                    {
                        isSuccess = false;
                    }
                   
                    if (isSuccess)
                    {
                        sucessCount++;
                        oe.DocumentState = DocumentState.Accepted;
                        oe.DocumentStateDescription = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<DocumentState>(DocumentState.Accepted, LocalData.IsEnglish);
                        SetControlData(param.Name, arrAcceptTime, string.Empty);
                        EnabledToolItem(false, true, true);
    
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Accept Success." : "签收成功.");
                        if (CurrentChanged != null)
                        {
                            CurrentChanged(this, oe);
                        }
                    }
                    //else
                    //{
                    //    BusinessToolPart.toolAccepted.Enabled = true;
                    //    ShowErrorColumn(oe, LocalData.IsEnglish ? "Accept failure." : "签收失败");
                    //}
                
                }
                else
                {
                    bool isSuccess = ClientFileService.AssignTo(param);

                    ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
                    if (isSuccess)
                    {
                        if (!string.IsNullOrEmpty(param.Name))
                            oe.AssignToName = param.Name;
                        AgentDispatchInfoPart.txtAssignTo.Text = param.Name;
                        EnabledToolItem(null, null, true);
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Assign-To Success." : "指派成功.");
                    }
                    //else
                    //{
                    //    ShowErrorColumn(oe, LocalData.IsEnglish ? "Assign-To failure." : "指派失败");
                    //}
                }

                ResetDataBinding();
            }
            MethodBase method = MethodBase.GetCurrentMethod();
            StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, method.DeclaringType.FullName, "DOWNLOAD AND ACCEPT", "下载签收");

            if (lstNos.Count > 0)
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ?
                    string.Format("There are {0} shipments has been accepted, but you need to aceept {1} remained shipments one by one which are re-dispatched.", sucessCount, lstNos.Count)
                    : string.Format("签收成功{0}票业务，但仍有{1}票重新分发文件的业务，需要您逐票地签收。", sucessCount, lstNos.Count));
            }

            ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
        }


        void ClearErrorColumnData(List<OceanBusinessDownLoadList> dataList)
        {
            foreach (OceanBusinessDownLoadList info in dataList)
            {
                if (!string.IsNullOrEmpty(info.ErrorInfo))
                {
                    info.ErrorInfo = string.Empty;
                    info.IsError = false;
                }
            }
            ResetDataBinding();
        }

        #region 签收文档
        [CommandHandler(OIBusinessDownLoadCommandConstants.Command_AcceptDocument)]
        public void Command_AcceptDocument(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            DateTime dDateOIBusinessCreateDate = OperationAgentService.GetCreateDateOIBusiness(CurrentRow.RefID);
            DateTime dDateStartDateForReviseAgentBill = OperationAgentService.GetStartDateForReviseAgentBill();
            if (CurrentRow.DownLoadState == DownLoadState.Downloaded && CurrentRow.DocumentState == DocumentState.Dispatched && dDateOIBusinessCreateDate > dDateStartDateForReviseAgentBill)//> new DateTime(2013,08,20)
            {

                BusinessOperationContext boc = new BusinessOperationContext() { OperationID = CurrentRow.OceanBookingID, OperationType = ICP.Framework.CommonLibrary.Common.OperationType.OceanExport, FormId = CurrentRow.ID, FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking };            
                ICP.FCM.Common.UI.FCMUIUtility.ShowAcceptedDocumentCompareNew(Workitem, CurrentRow.OceanBookingID, CurrentRow.RefID, false);
            }
            else
            {


                //this.Type = NotifyType.Accepted;
                //arrAcceptTime = DateTime.Now.ToString();
                //businessToolPart.toolAccepted.Enabled = false;
                ////将DocumentState状态Dispatched设置为Accepted，后如果此item已下载并产生海进业务，则删除海进旧有产生的文档，重新copy分文件信息.文件列表。
                //AgentDispatchParam param = CreateDispatchParamInfo(DocumentState.Accepted, Guid.Empty, LocalData.UserInfo.LoginName);

                //ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm(LocalData.IsEnglish ? "Accepting..." : "正在签收...");
                //try
                //{
                //    OnAction(param, false);
                //}
                //catch (Exception ex)
                //{
                this.Type = NotifyType.Accepted;
                arrAcceptTime = DateTime.Now.ToString();
                BusinessToolPart.toolAccepted.Enabled = false;
                //将DocumentState状态Dispatched设置为Accepted，后如果此item已下载并产生海进业务，则删除海进旧有产生的文档，重新copy分文件信息.文件列表。
                AgentDispatchParam param1 = CreateDispatchParamInfo(DocumentState.Accepted, Guid.Empty, LocalData.UserInfo.LoginName);

                theradID = ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm(LocalData.IsEnglish ? "Accepting..." : "正在签收...");
                try
                {
                    //OnAction(param1, false);
                    OnActionNew(param1, false);

                    if (dDateOIBusinessCreateDate <= dDateStartDateForReviseAgentBill)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ?
                         "ICP would not update D/C fees automatically for the shipments downloaded before 11/11(not include 11/11).\t\nYou must modify D/C fees manually via PDF files."
                         : "对于11/11(不包含11/11)之前下载的业务，签收后系统不会自动更新账单。\t\n您必须根据pdf文件手动更新账户。");

                    }
                }
                catch (Exception ex1)
                {
                    ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex1.Message);
                    BusinessToolPart.toolAccepted.Enabled = true;
                }
                //}
            }

            BusinessOperationParameter businessOperationParameter = new BusinessOperationParameter();
            BusinessOperationContext operationContext = new BusinessOperationContext();
            businessOperationParameter.Context = operationContext;
            ICPCommonOperationService.AfterContainerInfoSaved(businessOperationParameter);
        }

        private BusinessOperationContext GetHistoryContext(Guid businessID)
        {
            BusinessOperationContext context = new BusinessOperationContext();
            context.OperationID = businessID;
            return context;
        }
        #endregion

        #region 签收文档新
        [CommandHandler(OIBusinessDownLoadCommandConstants.Command_AcceptedNew)]
        public void Command_AcceptedNew(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            DateTime dDateOIBusinessCreateDate = OperationAgentService.GetCreateDateOIBusiness(CurrentRow.RefID);
            DateTime dDateStartDateForReviseAgentBill = OperationAgentService.GetStartDateForReviseAgentBill();
            if (CurrentRow.DownLoadState == DownLoadState.Downloaded && CurrentRow.DocumentState == DocumentState.Dispatched && dDateOIBusinessCreateDate > dDateStartDateForReviseAgentBill)//> new DateTime(2013,08,20)
            {

                BusinessOperationContext boc = new BusinessOperationContext() { OperationID = CurrentRow.OceanBookingID, OperationType = ICP.Framework.CommonLibrary.Common.OperationType.OceanExport, FormId = CurrentRow.ID, FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking };    
                ICP.FCM.Common.UI.FCMUIUtility.ShowAcceptedDocumentCompareNew(Workitem, CurrentRow.OceanBookingID, CurrentRow.RefID, false);
            }
            else
            {
                this.Type = NotifyType.Accepted;
                arrAcceptTime = DateTime.Now.ToString();
                BusinessToolPart.toolAccepted.Enabled = false;
                //将DocumentState状态Dispatched设置为Accepted，后如果此item已下载并产生海进业务，则删除海进旧有产生的文档，重新copy分文件信息.文件列表。
                AgentDispatchParam param1 = CreateDispatchParamInfo(DocumentState.Accepted, Guid.Empty, LocalData.UserInfo.LoginName);

                theradID = ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm(LocalData.IsEnglish ? "Accepting..." : "正在签收...");
                try
                {
                    OnActionNew(param1, false);
                }
                catch (Exception ex1)
                {
                    ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex1.Message);
                    BusinessToolPart.toolAccepted.Enabled = true;
                }
            }

            BusinessOperationParameter businessOperationParameter = new BusinessOperationParameter();
            BusinessOperationContext operationContext = new BusinessOperationContext();
            businessOperationParameter.Context = operationContext;
            ICPCommonOperationService.AfterContainerInfoSaved(businessOperationParameter);
        }

        #endregion

        #region 打印全部签收的文件
        [CommandHandler(OIBusinessDownLoadCommandConstants.Command_PrintAll)]
        public void Command_PrintAll(object sender, EventArgs e)
        {
            theradID = ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm(LocalData.IsEnglish ? "Merging Files..." : "正在合并文件...");

            WaitCallback callback = (data) =>
          {
              try
              {
                  //将选择业务合并成一个PDF
                  Utility.PrintAllDispatchDocument(CurrentList);
              }
              catch (Exception ex)
              {
                  LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
              }
              finally
              {
                  ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
              }
          };
            ThreadPool.QueueUserWorkItem(callback);
        }

        #endregion

        #region 取消签收文档
        [CommandHandler(OIBusinessDownLoadCommandConstants.Command_UnAcceptDocument)]
        public void Command_UnAcceptDocument(object sender, EventArgs e)
        {
            this.Type = NotifyType.UnAccepted;
            //将DocumentState状态为Accepted更改为Dispatched
            BusinessToolPart.toolUnAccepted.Enabled = false;
            AgentDispatchParam param = CreateDispatchParamInfo(DocumentState.Dispatched, Guid.Empty, string.Empty);
            theradID = ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm(LocalData.IsEnglish ? "Un-Accepting..." : "正在取消签收...");
            try
            {
                OnActionNew(param, false);
            }
            catch (Exception ex)
            {
                ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                BusinessToolPart.toolUnAccepted.Enabled = true;
            }
        }
        #endregion

        #region 移交给
        [CommandHandler(OIBusinessDownLoadCommandConstants.Command_Transition)]
        public void Command_Transition(object sender, EventArgs e)
        {
            //暂时不实现此功能
        }
        #endregion

        #region 指派给
        [CommandHandler(OIBusinessDownLoadCommandConstants.Command_AssignTo)]
        public void Command_AssignTo(object sender, EventArgs e)
        {
            this.Type = NotifyType.AssignTo;
            List<OceanBusinessDownLoadList> dataList = CurrentList;
            if (dataList == null || dataList.Count == 0)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), LocalData.IsEnglish ? "Please select the assigned business" : "请选择需要指派的业务.");
                return;
            }
            this.ShowAssignToCSPart(BusinessToolPart.toolAssignTo);
        }

        void ShowAssignToCSPart(BarButtonItem toolAssignTo)
        {
            toolAssignTo.Enabled = false;

            OIBusinessAssignToCS assignToCS = this.Workitem.Items.AddNew<OIBusinessAssignToCS>();
            assignToCS.Selected += (obj, data, assignToName) =>
            {
                if (data != null && data != "")
                {
                    AgentDispatchParam param = CreateDispatchParamInfo(DocumentState.Pending, new Guid(data.ToString()), assignToName);
                    try
                    {
                        OnActionNew(param, true);
                    }
                    catch (Exception ex)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                        toolAssignTo.Enabled = true;
                    }
                }
            };

            assignToCS.Canceled += delegate
            {
                toolAssignTo.Enabled = true;
            };
            PartLoader.ShowDialog(assignToCS, LocalData.IsEnglish ? "Selected custom service" : "选择客服", FormBorderStyle.Sizable);
        }

        #endregion

        /// <summary>
        /// 保存后刷新列表
        /// </summary>
        private void RefreshList()
        {
            this.gvMain.CloseEditor();

            List<OceanBusinessDownLoadList> dataList = DataSource as List<OceanBusinessDownLoadList>;
            foreach (OceanBusinessDownLoadList b in dataList)
            {
                if (b.IsCheck)
                {
                    b.DownLoadState = DownLoadState.Downloaded;
                    b.DownLoadStateDescription = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<DownLoadState>(DownLoadState.Downloaded, LocalData.IsEnglish);
                    b.IsCheck = false;
                }
            }

            //this.bsOEList.ResetBindings(false);

            gvMain.RefreshData();

        }

        private void mainGridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }

        #region Dispose
        void IDisposable.Dispose()
        {
            if (this.DocumentNotifyService != null)
            {
                //DocumentNotifyService.DocumentDispatched -= OnDocumentDispatched;
                //DocumentNotifyService.DocumentUnAccepted -= OnDocumentUnAccepted;
                //DocumentNotifyService.DocumentAccepted -= OnDocumentAccepted;
                //DocumentNotifyService.DocumentAssignTo -= OnDocumentAssignTo;
                DocumentNotifyService.DocumentError -= OnDocumentError;
            }
        }

        #endregion

        #region  通过鼠标移动到错误列，pop出提示信息
        private void gvMain_MouseMove(object sender, MouseEventArgs e)
        {
            GridHitInfo hitInfo = gvMain.CalcHitInfo(e.Location);

            if (hitInfo.InRowCell == false || hitInfo.RowHandle < 0 || hitInfo.Column != colIsError)
            {
                toolTip1.Hide(this);
                return;
            }

            OceanBusinessDownLoadList item = gvMain.GetRow(hitInfo.RowHandle) as OceanBusinessDownLoadList;
            if (item == null || item.IsError == false) return;
            string s = toolTip1.GetToolTip(this);
            if (toolTip1.Active & s == item.ErrorInfo) return;

            Point pt = gcMain.PointToClient(MousePosition);
            pt.X += 20;
            pt.Y += 30;
            this.toolTip1.Show(item.ErrorInfo, this, pt, 5000);
        }

        #endregion

        private void gvMain_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            OceanBusinessDownLoadList list = gvMain.GetRow(e.RowHandle) as OceanBusinessDownLoadList;
            if (list == null)
            {
                return;
            }
            if (list.IsAgainDispatch)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.NewLine);
            }
            else if (list.DocumentState == DocumentState.Accepted)
            {
                e.Appearance.ForeColor = Color.Gray;
                //ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.);
            }
        }
    }
}
