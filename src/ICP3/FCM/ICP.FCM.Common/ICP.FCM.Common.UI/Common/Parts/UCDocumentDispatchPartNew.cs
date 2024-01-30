using ICP.Business.Common.UI.Document;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.FileSystem.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Server;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace ICP.FCM.Common.UI.Common.Parts
{
    public partial class UCDocumentDispatchPartNew : BaseListPart
    {
        #region 服务
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        #endregion

        private DocumentListPresenter presenter;
        /// <summary>
        /// 当前文档列表呈现类
        /// </summary>
        public DocumentListPresenter CurrentDocumentListPresenter
        {
            get
            {
                if (presenter == null)
                {
                    presenter = this.Workitem.Items.AddNew<DocumentListPresenter>();
                }
                return presenter;
            }

        }
        private DocumentListPresenter historyPresenter;
        public int workflag = 1;
        /// <summary>
        /// 历史分发文档列表呈现类
        /// </summary>
        public DocumentListPresenter HistoryDocumentListPresenter
        {
            get
            {
                if (historyPresenter == null)
                {
                    historyPresenter = this.Workitem.Items.AddNew<DocumentListPresenter>();
                }
                return historyPresenter;
            }

        }


        /// <summary>
        /// 文件操作服务
        /// </summary>
        [ServiceDependency]
        public IFileService FileService { get; set; }

        /// <summary>
        /// 分发文档服务
        /// </summary>
        [ServiceDependency]
        public IOperationAgentService OperationAgentService { get; set; }

        [ServiceDependency]
        public IClientFileService BusinessFileService { get; set; }

        IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }

        /// <summary>
        /// 分文档历史信息
        /// </summary>
        public UCDocumentList DocumentListHistory { get; set; }

        /// <summary>
        /// 当前分文档信息
        /// </summary>
        public UCDocumentList DocumentListCurrent { get; set; }

        /// <summary>
        /// 分文档控件上一次记录
        /// </summary>
        public List<DocumentInfo> DocumentSourceHistory { get; set; }

        public BusinessOperationContext ContextHistory { get; set; }

        /// <summary>
        /// 当前文档列表上下文对象
        /// </summary>
        public BusinessOperationContext ContextCurrent { get; set; }

        /// <summary>
        /// 分文件信息ID
        /// </summary>
        public Guid OceanAgentDispatchID { get; set; }

        /// <summary>
        /// 分文档控件当前数据源
        /// </summary>
        public List<DocumentInfo> DocumentSourceCurrent
        {
            get { return DocumentListCurrent.DataSource; }
        }

        /// <summary>
        /// 是否隐藏DocumentListCurrent的上传
        /// </summary>
        public bool IsCurrentUpdateHide
        {
            get;
            set;
        }

        private bool isBindPendingDate = true;
        /// <summary>
        /// 是否绑定文件状态为Pending的数据
        /// </summary>
        public bool IsBindPendingDate
        {
            get { return isBindPendingDate; }
            set { isBindPendingDate = value; }
        }


        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return MeRemark.Text.Trim(); }
            set { MeRemark.Text = value; }
        }

        /// <summary>
        /// 显示历史面板
        /// </summary>
        public bool ShowHistoryPart { set { splitContainerControl1.Panel1.Visible = value; } }

        /// <summary>
        /// 设置Remark是否只读
        /// </summary>
        public bool RemarkReadOnly { set { MeRemark.Properties.ReadOnly = value; } }

        /// <summary>
        /// 设置Remark是否只读
        /// </summary>
        public void SetRemarkVisible(int height)
        {
            grpRemark.Height = height;

        }


        /// <summary>
        /// 控制当前文档信息是否显示业务相关控件
        /// </summary>
        public bool IsShowBusinessControl
        {
            get;
            set;
        }

        /// <summary>
        /// 设置文档列表显示隐藏
        /// </summary>
        public bool ShowCheckControl { get; set; }
        /// <summary>
        /// 系统错误日志服务
        /// </summary>
        public ISystemErrorLogService SystemErrorLogService
        {
            get { return ServiceClient.GetService<ISystemErrorLogService>(); }
        }

        public UCDocumentDispatchPartNew()
        {
            InitializeComponent();

            this.Disposed += delegate
            {
                try
                {
                    if (Workitem != null)
                    {
                        Workitem.Items.Remove(this);
                        Workitem.Items.Remove(this.presenter);
                        Workitem = null;
                    }
                    if (this.presenter != null)
                    {
                        this.presenter.ucList = null;
                        this.presenter.Dispose();
                        this.presenter = null;
                    }
                    if (this.historyPresenter != null)
                    {
                        this.historyPresenter.ucList = null;
                        this.historyPresenter.Dispose();
                        this.historyPresenter = null;
                    }
                }
                catch (Exception ex)
                {
                    string exceptionstr = "UCDocumentDispatchPartNew():Disposed\r\n" + ex.Message;
                    SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                        LocalData.SessionId, new byte[0], exceptionstr,
                        DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
                }
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!DesignMode)
            {
                DocumentListHistory = Workitem.Items.AddNew<UCDocumentList>();
                DocumentListHistory.ManageBusinessBarDisplay(false);
                DocumentListHistory.Dock = DockStyle.Fill;
                DocumentListHistory.ShowControlCheckState = false;
                pnlControlHistory.Controls.Add(DocumentListHistory);

                //修改代码，解决同时加载多个UCDocumentList时只显示一个。joe 2013-07-24
                DocumentListCurrent = Workitem.Items.AddNew<UCDocumentList>();
                DocumentListCurrent.ManageBusinessBarDisplay(IsShowBusinessControl);
                DocumentListCurrent.Dock = DockStyle.Fill;
                // DocumentListCurrent.ShowControlCheckState = ShowCheckControl;
                DocumentListCurrent.Visible = true;
                pnlControlCurrent.Controls.Add(DocumentListCurrent);

                if (!LocalData.IsEnglish) SetCnText();
            }
        }

        private void SetCnText()
        {
            grpControlHistory.Text = "上次分发文档列表";
            grpControlCurrent.Text = "分发文档列表";
            grpRemark.Text = "备注";
        }

        #region 数据绑定
        public override object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }

        BusinessOperationContext context = null;
        private void BindingData(object value)
        {
            context = value as BusinessOperationContext;
            if (context == null)
            {
                return;
            }
            DocumentSourceHistory = FileService.GetBusinessDocumentList(context);
            DocumentListHistory.DataSource = DocumentSourceHistory;
            CurrentDocumentListPresenter.ucList = this.DocumentListCurrent;
            this.DocumentListCurrent.Presenter = CurrentDocumentListPresenter;
            CurrentDocumentListPresenter.BindData(context);
            DocumentListCurrent.ShowControlCheckState = ShowCheckControl;
        }


        public void SetDataSource()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            if (ContextHistory == null || ContextCurrent == null)
            {
                return;
            }
            this.HistoryDocumentListPresenter.ucList = this.DocumentListHistory;
            this.DocumentListHistory.Presenter = this.HistoryDocumentListPresenter;
            Guid fileLog = Guid.Empty;
            string result = FCMCommonService.GetDispatchLastLogID(ContextCurrent.OperationID);
            if (result == string.Empty)
            {
                result = FCMCommonService.GetDispatchNewLogID(ContextCurrent.OperationID);
                if (!string.IsNullOrEmpty(result))
                {
                    fileLog = JSONSerializerHelper.DeserializeFromJson<Guid>(result);
                }
            }
            else
            {
                fileLog = JSONSerializerHelper.DeserializeFromJson<Guid>(result);
            }

            if (isBindPendingDate)
            {
                this.HistoryDocumentListPresenter.InnerBindDataNew(ContextCurrent, fileLog);
            }
            else
            {
                this.HistoryDocumentListPresenter.InnerBindData(ContextHistory);
            }


            //if (OceanAgentDispatchID != Guid.Empty)
            //{
            //    DocumentDispatchInfo DispatchInfo = OperationAgentService.GetDocumentDispatchInfo(OceanAgentDispatchID);

            //    if (DispatchInfo != null)
            //    {
            //        MeRemark.Text = DispatchInfo.Remark;
            //    }
            //}
            DocumentState documentState = DocumentState.Pending;
            if (ContextCurrent["DocumentState"] != null)
            {
                documentState = (DocumentState)ContextCurrent["DocumentState"];
            }
            if (DocumentListHistory.DataSource.Count == 0 || workflag == 2)
            {
                splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            }
            else if (documentState == DocumentState.Accepted)
            {
                splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
                return;
            }
            else
            {
                splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
            }
            this.CurrentDocumentListPresenter.ucList = this.DocumentListCurrent;
            this.DocumentListCurrent.Presenter = this.CurrentDocumentListPresenter;

            if (isBindPendingDate)
            {
                if (ContextCurrent == ContextHistory)
                {
                    this.CurrentDocumentListPresenter.InnerBindData(ContextHistory);
                }
                else
                {
                    this.CurrentDocumentListPresenter.InnerBindDataNew(ContextCurrent, fileLog);
                }
            }
            else
            {
                if (OceanAgentDispatchID != Guid.Empty)
                {
                    if (ContextCurrent == ContextHistory)
                    {
                        this.CurrentDocumentListPresenter.InnerBindData(ContextHistory);
                    }
                    else
                    {
                        this.CurrentDocumentListPresenter.InnerBindDataNew(ContextCurrent, fileLog);
                    }
                }
                else
                {
                    this.CurrentDocumentListPresenter.InnerBindDataNew(ContextCurrent, Guid.Empty);
                }
            }

            if (!IsCurrentUpdateHide)
            {
                DocumentListCurrent.ManageBusinessBarDisplay(true);
            }
            else
            {
                DocumentListCurrent.ManageBusinessBarDisplay(false);
            }
            DocumentListCurrent.ShowControlCheckState = ShowCheckControl;
            MethodBase method = MethodBase.GetCurrentMethod();
            StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, method.DeclaringType.FullName, "GET-FILES", string.Format("加载文件列表;OperationID[{0}]", ContextCurrent.OperationID));
        }
        #endregion

        /// <summary>
        /// 刷新当前分发的文件列表
        /// </summary>
        public void CurrentDispatchListResetBindings()
        {

            DocumentListCurrent.ResetBindings = false;
        }

        private void UCDocumentDispatchPartNew_Resize(object sender, EventArgs e)
        {
            if (this.DocumentListHistory != null)
            {
                splitContainerControl1.SplitterPosition = (int)(splitContainerControl1.Width / 2);
            }
        }
    }
}
