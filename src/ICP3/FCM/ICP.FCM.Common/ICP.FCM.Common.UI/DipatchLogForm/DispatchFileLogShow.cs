using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIManagement;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Common.UI;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
using ICP.DataCache.ServiceInterface;
using DevExpress.XtraEditors;
using ICP.FileSystem.ServiceInterface;

namespace ICP.FCM.Common.UI.DipatchLogForm
{
    public partial class DispatchFileLogShow : BasePart
    {
        public DispatchFileLogShow()
        {
            InitializeComponent();
        }

        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        List<UserList> users = new List<UserList>();


        public IOperationAgentService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IOperationAgentService>();
            }
        }

        /// <summary>
        /// 文件操作服务
        /// </summary>
        [ServiceDependency]
        public IFileService FileService { get; set; }

        private ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }

        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
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

        public string SelState
        {
            get
            {
                return chkDispatchState.Text;
            }
        }

        #endregion

        int type = 1;//查询方式 1 所有 2 条件查询

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
                setControlText();
                users = UserService.GetUserListByList(string.Empty, string.Empty, null, null, null, null, null, null, 0);
                List<DispathLogData> datasours = FCMCommonService.GetDispatchFileLogForUser(null, "1,2,3,5", LocalData.UserInfo.LoginID,null,null);
                gcMain.DataSource = datasours;
                gvDetails.RefreshData();
                DateStart.DateTime = DateTime.Now;
                DateEnd.DateTime = DateTime.Now;
                btnRetry.Left = btnOpen.Left;
                btnRetry.Top = btnOpen.Top;
            }
        }

        private void InitControls()
        {
            FCMUIUtility.SetEnterToExecuteOnec(this.chkDispatchState, delegate
            {
                chkDispatchState.Properties.BeginUpdate();
                chkDispatchState.Properties.Items.Clear();
               
                this.chkDispatchState.Properties.BeginUpdate();
                this.chkDispatchState.Properties.Items.Clear();

                this.chkDispatchState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem("需要分发", 1));
                this.chkDispatchState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem("分发成功", 2));
                this.chkDispatchState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem("分发失败", 3));
                this.chkDispatchState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem("今天分发", 4));

                this.chkDispatchState.Properties.EndUpdate();
            });
        }

        private void setControlText()
        {
            if (LocalData.IsEnglish)
            {
                labState.Text = "DispatchState";
                labDateStrat.Text = "DispatchDate";
                labTo.Text = "To";
                btnSearch.Text = "Search";
                btnRefresh.Text = "Refresh";
                btnOpen.Text = "Open";
                btnRetry.Text = "Retry";
                colCreateBy.Caption = "CreateBy";
                colCreateDate.Caption = "DispatchDate";
                colIsTransTo.Caption = "Dispatched";
                colOperationNo.Caption = "BusinessNo";
                colState.Caption = "DispatchState";
                colAcceptBy.Caption = "AcceptBy";
                colAcceptDate.Caption = "AcceptDate";
                colOperationType.Caption = "OperationType";
            }
            else
            {
                labState.Text = "分发状态";
                labDateStrat.Text = "分发时间";
                labTo.Text = "至";
                btnSearch.Text = "查询";
                btnRefresh.Text = "刷新";
                btnOpen.Text = "打开";
                btnRetry.Text = "重试";
                colCreateBy.Caption = "分发人";
                colCreateDate.Caption = "分发时间";
                colIsTransTo.Caption = "是否分发到港后";
                colOperationNo.Caption = "业务号";
                colState.Caption = "分发状态";
                colAcceptBy.Caption = "签收人";
                colAcceptDate.Caption = "签收时间";
                colOperationType.Caption = "业务类型";

            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<DispathLogData> datasours = new List<DispathLogData>();
            switch (SelState)
            {
                case "需要分发":
                    datasours = FCMCommonService.GetDispatchFileLogForUser(null, "1,2", LocalData.UserInfo.LoginID, DateStart.DateTime, DateEnd.DateTime);
                    gcMain.DataSource = datasours;
                    gvDetails.RefreshData();
                    break;
                case "分发成功":
                    datasours = FCMCommonService.GetDispatchFileLogForUser(null, "4,6", LocalData.UserInfo.LoginID, DateStart.DateTime, DateEnd.DateTime);
                    gcMain.DataSource = datasours;
                    gvDetails.RefreshData();
                    break;
                case "分发失败":
                    datasours = FCMCommonService.GetDispatchFileLogForUser(null, "5", LocalData.UserInfo.LoginID, DateStart.DateTime, DateEnd.DateTime);
                    gcMain.DataSource = datasours;
                    gvDetails.RefreshData();
                    break;
                case "今天分发":
                    datasours = FCMCommonService.GetDispatchFileLogForUser(null, "1,2,3,4,5,6", LocalData.UserInfo.LoginID, Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")+" 00:00:00"),DateTime.Now.AddDays(1));
                    gcMain.DataSource = datasours;
                    gvDetails.RefreshData();
                    break;
                default:
                    break;
            }
            type = 2;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (gvDetails.FocusedRowHandle < 0)
            {
                string message = LocalData.IsEnglish ? "Please select a dispatch record to open！" : "请选择一条分发记录打开！";
                MessageBox.Show(message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DispathLogData currentrow = gvDetails.GetFocusedRow() as DispathLogData;
                if (currentrow.State == 3)
                {
                    string message = LocalData.IsEnglish ? "The file is being distributed, do not operate！" : "该文件正在分发中，请勿操作！";
                    MessageBox.Show(message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                FCM.Common.UI.FCMUIUtility.ShowDispatchDocumentNew(this.Workitem, BuildBussinessInfo(currentrow.OperationID,(int)currentrow.OperationType), AfterDocumentDispatchSaved,2);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
        }

        BusinessOperationContext BuildBussinessInfo(Guid OperationID,int type)
        {
            BusinessOperationContext context = BusinessOperationContext.Current;
            context.OperationID = OperationID;
            context.OperationType = type == 1 ? OperationType.OceanExport : OperationType.OceanImport;
            return context;
        }

        public void AfterDocumentDispatchSaved(object[] prams)
        {
            if (prams.Count() == 0)
            {
                return;
            }
            switch (type)
            {
                case 1:
                    btnRefresh_Click(null, null);
                    break;
                case 2:
                    btnSearch_Click(null, null);
                    break;
            }
        }


        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (type == 1)
            {
                List<DispathLogData> datasours = FCMCommonService.GetDispatchFileLogForUser(null, "1,2,3,5", LocalData.UserInfo.LoginID, null, null);
                gcMain.DataSource = datasours;
                gvDetails.RefreshData();
            }
            else
            {
                btnSearch_Click(null, null);
            }
         
        }

        /// <summary>
        /// 重新分发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRetry_Click(object sender, EventArgs e)
        {
            if (gvDetails.FocusedRowHandle < 0)
            {
                return;
            }

            DispathLogData currentrow = gvDetails.GetFocusedRow() as DispathLogData;
            Stopwatch stopwatch = Stopwatch.StartNew();
            this.Visible = false;
            this.Parent.Visible = false;
            WaitCallback callback = data =>
            {
                strat(currentrow);
            };
            ThreadPool.QueueUserWorkItem(callback);
            MethodBase method = MethodBase.GetCurrentMethod();
            StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, method.DeclaringType.FullName, "DISPATCH", "分发文件");
        }

        /// <summary>
        /// 双击记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvDetails_DoubleClick(object sender, EventArgs e)
        {
            if (btnOpen.Visible)
            {
                simpleButton1_Click(null, null);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvDetails_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "OperationType")
            {
                if (e.Value != null && e.Value != DBNull.Value)
                {
                    switch (e.Value.ToString())
                    {
                        case "1":
                            e.DisplayText = "出口";
                            break;
                        case "2":
                            e.DisplayText = "进口";
                            break;
                    }
                }
            }
            else if (e.Column.FieldName == "State")
            {
                if (e.Value != null && e.Value != DBNull.Value)
                {
                    switch (e.Value.ToString())
                    {
                        case "1":
                            e.DisplayText = LocalData.IsEnglish ? "Pending" : "未分发";
                            break;
                        case "2":
                        case "3":
                            e.DisplayText = LocalData.IsEnglish ? " In Progress" : "正在分发";
                            break;
                        case "4":
                            e.DisplayText = LocalData.IsEnglish ? "Completed" : "分发成功";
                            break;
                        case "5":
                            e.DisplayText = LocalData.IsEnglish ? "Failed" : "分发失败";
                            break;
                        case "6":
                            e.DisplayText = LocalData.IsEnglish ? "Accept" : "签收";
                            break;
                    }
                }
            }
            else if (e.Column.FieldName == "AcceptBy" || e.Column.FieldName == "CreateBy")
            {
                if (e.Value != null && e.Value != DBNull.Value)
                {
                    e.DisplayText = LocalData.IsEnglish ? users.Find(r => r.ID == (Guid)e.Value).EName : users.Find(r => r.ID == (Guid)e.Value).CName;
                }
            }
        }

        /// <summary>
        /// 选择列切换显示转换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvDetails_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvDetails.FocusedRowHandle < 0)
            {
                return;
            }

            DispathLogData currentrow = gvDetails.GetFocusedRow() as DispathLogData;
            if (currentrow.State == 1)
            {
                btnOpen.Visible = true;
                btnRetry.Visible = false;
            }
            else if (currentrow.State == 5)
            {
                btnOpen.Visible = false;
                btnRetry.Visible = true;
            }
            else
            {
                btnOpen.Visible = false;
                btnRetry.Visible = false;
            }
        }


        private void strat(DispathLogData currentrow)
        {
            try
            {
                bool isSuccess = SaveDocumentDispatchInfo(currentrow);

                string tip = LocalData.IsEnglish ? "" : "";

                if (isSuccess)
                {

                }
                else
                {
                    tip = LocalData.IsEnglish ? "Dispatching Docs is Failure!" : "文档分发失败!";
                    XtraMessageBox.Show(tip, "Tips", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception ex)
            {
                string strmessage = "";
                strmessage = ClientHelper.GetErrorMessage(ex);
                XtraMessageBox.Show(strmessage, "Tips", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        /// <summary>
        /// 保存分文档信息
        /// </summary>
        private bool SaveDocumentDispatchInfo(DispathLogData currentrow)
        {
            
            if (currentrow.OperationType == 1)
            {
                List<DocumentInfo> filelist = FileService.GetDispatchFiles(currentrow.ID);
                List<Guid> fileid = new List<Guid>();
                if (filelist.Count > 0)
                {
                    foreach (DocumentInfo info in filelist)
                    {
                        fileid.Add(info.Id);
                    }
                }

                return OperationAgentService.DicpatchFilesForOE(currentrow.OperationID, 1, fileid.ToArray(), LocalData.UserInfo.LoginID);
            }
            else
            {
                return OperationAgentService.DicpatchFilesForOI(currentrow.OperationID, LocalData.UserInfo.LoginID);
            }

        }
    }
}
