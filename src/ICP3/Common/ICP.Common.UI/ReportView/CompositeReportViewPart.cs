using System;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Common.ServiceInterface.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.WinForms;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Common.UI.ReportView
{
    /// <summary>
    /// 带查询设置面板公用报表预览SmartPart
    /// </summary>
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class CompositeReportViewPart : BaseEditPart
    {
        #region 服务注入
        /// <summary>
        /// 报表预览服务
        /// </summary>
        public IReportViewService ReportViewService
        {
            get
            {
                return ServiceClient.GetClientService<IReportViewService>();
            }
        }
        /// <summary>
        /// 父WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
     

        #endregion
        #region 辅助字段
        private DeckWorkspace previewWorkspace;
        protected IReportViewer reportViewer;
        /// <summary>
        /// 附件导出路径
        /// 注意：必须点击发送邮件或传真按钮后，此字段才有实际值，否则返回空
        /// </summary>
        public string ExportFilePath
        {
            get
            {

                if (this.reportViewer == null || string.IsNullOrEmpty(this.reportViewer.ExportFilePath))
                {
                    return string.Empty;
                }
                return this.reportViewer.ExportFilePath;

            }

        }
        #endregion
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public CompositeReportViewPart()
        {
            IsShowLanguageMenu = false;
            InitializeComponent();
            this.Disposed += delegate
            {
                if (Workitem != null)
                {
                    RemoveReportViewerEventHandler();
                    if (this.previewWorkspace != null)
                    {
                        Workitem.Workspaces.Remove(this.previewWorkspace);
                        this.previewWorkspace.PerformLayout(null, null);
                    }
                    if (this.reportViewer != null)
                    {
                        this.reportViewer = null;
                    }
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            
            this.Load += new EventHandler(CompositeReportViewPart_Load);
        }


        #endregion

        #region 辅助方法
        private void SetPreviewInfo()
        {
            if (!DesignMode)
            {
                AddWorkSpace();
                ConfigReportViewer();
            }
        }

        private void ConfigReportViewer()
        {
            reportViewer = ReportViewService.ShowReportViewer(this.Title, previewWorkspace);
            reportViewer.AfterPrint += new EventHandler(reportViewer_AfterPrint);
            reportViewer.BeforeSendEmail += new CancelEventHandler(reportViewer_BeforeSendEmail);
            reportViewer.BeforeSendFax += new CancelEventHandler(reportViewer_BeforeSendFax);
           // reportViewer.AfterSendEmail += new EventHandler(reportViewer_AfterSendEmail);
            reportViewer.AfterSendFax += new EventHandler(reportViewer_AfterSendFax);
        }

        private void AddWorkSpace()
        {
            previewWorkspace = new DeckWorkspace();
            previewWorkspace.Dock = DockStyle.Fill;
            this.splitContainerControl.Panel2.Controls.Add(this.previewWorkspace);
        }
        private void RemoveReportViewerEventHandler()
        {
            if (reportViewer != null)
            {
                reportViewer.AfterPrint -= new EventHandler(reportViewer_AfterPrint);
                reportViewer.BeforeSendEmail -= new CancelEventHandler(reportViewer_BeforeSendEmail);
                reportViewer.BeforeSendFax -= new CancelEventHandler(reportViewer_BeforeSendFax);
               // reportViewer.AfterSendEmail -= new EventHandler(reportViewer_AfterSendEmail);
                reportViewer.AfterSendFax -= new EventHandler(reportViewer_AfterSendFax);
            }
        }
        #endregion
        #region 事件处理
        void CompositeReportViewPart_Load(object sender, EventArgs e)
        {
            Locale();
            LoadData();
            SetPreviewInfo();
        }
        private void reportViewer_AfterPrint(object sender, EventArgs e)
        {
            AfterPrint();
        }

        private void reportViewer_AfterSendFax(object sender, EventArgs e)
        {
            AfterSendFax();
        }

       // private void reportViewer_AfterSendEmail(object sender, EventArgs e)
       // {
           // AferSendEmail();
       // }

        private void reportViewer_BeforeSendFax(object sender, CancelEventArgs e)
        {
            e.Cancel = BeforeSendFax();
        }

        private void reportViewer_BeforeSendEmail(object sender, CancelEventArgs e)
        {
            e.Cancel = BeforeSendEmail();
        }
        
         private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnQuery.Enabled = false;
                Query();
            }
            finally
            {
                this.btnQuery.Enabled = true;
            }
        }
        #endregion
        #region 可覆盖方法
        /// <summary>
        ///根据当前语言进行界面本地化处理
        /// </summary>
        protected virtual void Locale()
        {
            if (!LocalData.IsEnglish)
            {
                this.btnQuery.Text = "查询";
            }
        }
        /// <summary>
        /// 发送邮件后
        /// </summary>
        protected virtual void AferSendEmail()
        {

        }
        /// <summary>
        /// 发送传真后
        /// </summary>
        protected virtual void AfterSendFax()
        {

        }
        /// <summary>
        /// 界面加载中的界面自定义处理
        ///例如给派生窗体上添加的控件填充数据
        /// </summary>
        protected virtual void LoadData()
        {

        }
        /// <summary>
        /// 传真发送前处理
        /// 方法返回值默认为true，则取消随后事件处理,默认为false
        /// </summary>
        /// <returns></returns>
        protected virtual Boolean BeforeSendFax()
        {
            return false;
        }
        /// <summary>
        /// 查询
        /// </summary>
        protected virtual void Query()
        {

        }
        /// <summary>
        /// Email发送前处理
        ///  方法返回值true，则取消随后事件处理,默认为false
        /// </summary>
        /// <returns></returns>
        protected virtual Boolean BeforeSendEmail()
        {
            return false;
        }
        /// <summary>
        /// 打印后处理方法
        /// </summary>
        protected virtual void AfterPrint()
        {

        }
        #endregion
    }

}
