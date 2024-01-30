using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Microsoft.Reporting.WinForms;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraEditors;
using System.IO;
using ICP.ReportCenter.UI.Comm.Parts;
using ICP.Message.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;

namespace ICP.ReportCenter.UI
{
    /// <summary>
    /// 报表显示，需设置reportName和ParamList的值，然后调用Display即可，也可重写该控件
    /// </summary>
    [ToolboxItem(false)]
    public partial class ReportViewBase : DevExpress.XtraEditors.XtraUserControl
    {
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        public ICP.Message.ServiceInterface.IClientMessageService ClientMessageService
        {
            get
            {
                return ServiceClient.GetClientService<ICP.Message.ServiceInterface.IClientMessageService>();
            }
        }

        public ICP.Common.ServiceInterface.ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICP.Common.ServiceInterface.ICustomerService>();
            }
        }


        public ReportCenterHelper ReportCenterHelper
        {
            get
            {
                return ClientHelper.Get<ReportCenterHelper, ReportCenterHelper>();
            }
        }

        #region 初始化
        private const string WELCOME = "请输入你要检索的有效查询条件，并点击 ＂查询＂ 开始显示报表";
        private const string READING_DATA = "数据读取中，请稍等..........";

        private ICP.Message.ServiceInterface.Message message;

        private bool IsLocalReport = false;

        private string _reportName;
        /// <summary>
        /// 报表的名称，不加后缀名

        /// </summary>
        public string ReportName
        {
            set { this._reportName = value; }
        }

        /// <summary>
        /// 报表输出路径
        /// </summary>
        public string ExportedPath = "";

        private Guid _customerid;
        /// <summary>
        /// 发邮件时使用客户ID
        /// </summary>
        public Guid CustomerID
        {
            get
            {
                return _customerid;
            }
            set
            {
                _customerid = value;
            }
        }

        private List<ReportParameter> _paramList;
        /// <summary>
        /// 报表的参数列表
        /// </summary>
        public List<ReportParameter> ParamList
        {
            set { this._paramList = value; }
        }

        public ReportViewBase()
        {
            InitializeComponent();

            ToolStrip toolStrip = (ToolStrip)reportViewer.Controls.Find("toolStrip1", true)[0];

            ToolStripButton item = new ToolStripButton();
            item.Text = "Email";
            item.Click += new EventHandler(SendMail_Click);
            toolStrip.Items.Insert(15, item);

            this.reportViewer.Drillthrough += this.OnDrillThrough;
            this.Disposed += delegate
            {
                this.SubDataSource = null;
                this._paramList = null;
                this.Drillthrough = null;
                if (this.reportViewer != null)
                {
                    this.reportViewer.Drillthrough -= this.OnDrillThrough;

                    this.reportViewer.Dispose();
                    this.reportViewer = null;
                }

                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }

            };

        }
        private void OnDrillThrough(object sender, DrillthroughEventArgs e)
        {
            if (Drillthrough != null) Drillthrough(this, e);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetReportServerInfo();
        }


        private void SendMail_Click(object sender, EventArgs e)
        {

            if (IsLocalReport)
            {
                pdfExportLocal("");
            }
            else
            {
                pdfExportServer();
            }


            ICP.Message.ServiceInterface.Message message = InitMessage();
            ClientMessageService.ShowSendForm(message);
        }

        /// <summary>
        /// 本地报表导出为PDF
        /// </summary>
        public string pdfExportLocal(string filename)
        {
            if (string.IsNullOrEmpty(ExportedPath))
            {
                GetExportedPath(filename);
            }

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            byte[] bytes = this.reportViewer.LocalReport.Render("pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);
            FileStream fs = new FileStream(ExportedPath, FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            return ExportedPath;
        }

        
        /// <summary>
        /// 本地报表导出为二进制流
        /// </summary>
        public byte[] GetpdfExport(string filename)
        {
            if (string.IsNullOrEmpty(ExportedPath))
            {
                GetExportedPath(filename);
            }

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            byte[] bytes = this.reportViewer.LocalReport.Render("pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);
            return bytes;
        }

        /// <summary>
        /// 远程报表
        /// </summary>
        private void pdfExportServer()
        {
            if (string.IsNullOrEmpty(ExportedPath))
            {
                GetExportedPath("");
            }

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            byte[] bytes = this.reportViewer.ServerReport.Render("pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);
            FileStream fs = new FileStream(ExportedPath, FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
        }


        private void GetExportedPath(string filename)
        {
            string dirName = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            if (!Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }
            if (string.IsNullOrEmpty(filename))
                ExportedPath = Path.Combine(dirName, "attachment.pdf");
            else
                ExportedPath = Path.Combine(dirName, filename);
        }


        private Message.ServiceInterface.Message InitMessage()
        {
            if (message == null)
            {
                message = new ICP.Message.ServiceInterface.Message();
            }

            if (CustomerID != null && CustomerID != Guid.Empty)
            {
                CustomerInfo cusInfo = CustomerService.GetCustomerInfo(CustomerID);

                message.SendTo = cusInfo.EMail;
            }

            message.Type = Message.ServiceInterface.MessageType.Email;
            message.CreateBy = LocalData.UserInfo.LoginID;
            message.CreatorName = LocalData.UserInfo.UserName;
            message.HasAttachment = true;
            message.Id = Guid.NewGuid();
            message.SendFrom = LocalData.UserInfo.EmailAddress;
            InitAttachment();
            return message;
        }

        private void InitAttachment()
        {
            message.Attachments.Clear();
            string fullPath = this.ExportedPath;
            //生成Excel文件
            if (IsExportExcelFile())
                fullPath = ExportedPath;

            AttachmentContent attachment = new AttachmentContent();
            FileInfo fi = new FileInfo(fullPath);
            attachment.Name = attachment.DisplayName = Path.GetFileName(fullPath);
            attachment.ClientPath = fullPath;
            attachment.Size = fi.Length;
            message.Attachments.Add(attachment);
        }


        private bool IsExportExcelFile()
        {
            bool exportExcelFile = false;
            if (WorkItem.State["ExportExcelFile"] == null || WorkItem.State["ExportExcelFile"] == "")
            {
                return exportExcelFile;
            }
            else
            {
                bool.TryParse(WorkItem.State["ExportExcelFile"].ToString(), out exportExcelFile);
                WorkItem.State["ExportExcelFile"] = null;
            }

            return exportExcelFile;
        }

        /// <summary>
        /// 设置初始化服务路径
        /// </summary>
        private void SetReportServerInfo()
        {
            // this.reportViewer.ServerReport.ReportServerUrl = new Uri("http://rpt.cityocean.com/ReportServer");
            //this.reportViewer.ServerReport.ReportServerUrl = new Uri("http://192.168.99.32/ReportServer");

            this.reportViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            this.reportViewer.ServerReport.ReportServerUrl = new Uri(ReportCenterHelper.ReportServerInfo.ReportUrl);
            System.Net.NetworkCredential myCred = new System.Net.NetworkCredential(ReportCenterHelper.ReportServerInfo.ReportUser, ReportCenterHelper.ReportServerInfo.ReportUserPSW, null);
            //MessageBoxService.ShowInfo(Utility.ReportServerInfos.ReportUser + '/' + Utility.ReportServerInfos.ReportUserPSW);
            this.reportViewer.ServerReport.ReportServerCredentials.NetworkCredentials = myCred;


        }

        #endregion

        #region 接口

        #region 本地
        /// <summary>
        /// 显示本地报表
        /// </summary>
        /// <param name="reportName">reportName</param>
        /// <param name="dataSouce">dataSouce</param>
        /// <param name="reportParam">reportParam</param>
        public void DisplayLocalReport(string reportName, List<ReportDataSource> dataSouce, IEnumerable<ReportParameter> reportParam)
        {
            DisplayLocalReport(reportName, dataSouce, reportParam, null);
        }

        /// <summary>
        /// 显示本地报表
        /// </summary>
        /// <param name="reportName">reportName</param>
        /// <param name="dataSouce">dataSouce</param>
        /// <param name="reportParam">reportParam</param>
        /// <param name="subDataSource">子报表的数据源</param>
        public void DisplayLocalReport(string reportName, List<ReportDataSource> dataSouce, IEnumerable<ReportParameter> reportParam, List<ReportDataSource> subDataSource)
        {
            IsLocalReport = true;

            this.reportViewer.Reset();
            this.reportViewer.ProcessingMode = ProcessingMode.Local;
            this.reportViewer.LocalReport.ReportEmbeddedResource = reportName;
            this.reportViewer.LocalReport.DataSources.Clear();

            this.reportViewer.LocalReport.DataSources.Clear();

            foreach (ReportDataSource data in dataSouce)
            {
                this.reportViewer.LocalReport.DataSources.Add(data);
            }

            if (reportParam != null)
            {
                this.reportViewer.LocalReport.SetParameters(reportParam);
            }

            this.reportViewer.LocalReport.SubreportProcessing -= LocalReport_SubreportProcessing;
            this.SubDataSource = null;
            if (subDataSource != null)
            {
                this.SubDataSource = subDataSource;
                this.reportViewer.LocalReport.SubreportProcessing += LocalReport_SubreportProcessing;
            }
            this.reportViewer.LocalReport.ExecuteReportInSandboxAppDomain();
            this.reportViewer.RefreshReport();
        }

        private List<ReportDataSource> SubDataSource = null;
        void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            if (SubDataSource != null)
            {
                foreach (ReportDataSource ds in SubDataSource)
                {
                    e.DataSources.Add(ds);
                }
            }
        }

        #endregion

        #region 远程

        /// <summary>
        /// 显示报表，必须首先设置reportName和ParamList的值
        /// </summary>
        public void DisplayData()
        {
            IsLocalReport = false;

            //如果没有设置参数的值就返回
            if (this._reportName == "" || this._paramList == null) return;
            while (this.reportViewer.ServerReport.IsDrillthroughReport)
            {
                this.reportViewer.PerformBack();
            }

            SetReportServerInfo();
            //this.reportViewer.ProcessingMode = ProcessingMode.Local;

            //this.reportViewer.ServerReport.ReportServerCredentials.SetFormsCredentials(null, "ReportUser", "longwin", null);
            this.reportViewer.ServerReport.ReportPath = "/LongWin.DataWarehouseReport.Report/" + this._reportName;
            this.reportViewer.ServerReport.SetParameters(this._paramList);
            this.reportViewer.RefreshReport();
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// 显示报表，必须首先设置reportName和ParamList的值
        /// </summary>
        public void DisplayData(string reportFolder)
        {
            IsLocalReport = false;
            if (this.reportViewer.ServerReport.IsDrillthroughReport)
            {
                this.reportViewer.PerformBack();
            }
            SetReportServerInfo();

            //如果没有设置参数的值就返回
            if (this._reportName == "" || this._paramList == null) return;
            while (this.reportViewer.ServerReport.IsDrillthroughReport)
            {
                this.reportViewer.PerformBack();
            }

            //this.reportViewer.ServerReport.ReportServerCredentials.SetFormsCredentials(null, "ReportUser", "longwin", null);
            this.reportViewer.ServerReport.ReportPath = reportFolder + this._reportName;
            this.reportViewer.ServerReport.SetParameters(this._paramList);

            this.reportViewer.RefreshReport();
            this.Cursor = Cursors.Default;
        }

        #endregion

        #endregion

        #region Events

        /// <summary>
        /// 报表下钻事件
        /// </summary>
        public event DrillthroughEventHandler Drillthrough;

        #endregion
    }
}
