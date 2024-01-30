using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using ICP.Common.ServiceInterface.Client;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using System.IO;
using Microsoft.Practices.ObjectBuilder;
using ICP.Message.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.DataCache.ServiceInterface;
using ICP.FileSystem.ServiceInterface;

namespace ICP.Common.UI.ReportView
{
    /// <summary>
    /// 不带查询设置面板公用报表预览SmartPart
    /// </summary>
    public partial class FastReportViewerPart : UserControl, IReportViewer
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }



        public IClientMessageService ClientMessageService
        {
            get
            {
                return ServiceClient.GetClientService<IClientMessageService>();
            }
        }



        public DocumentNotifyClientService DocumentNotifyService
        {
            get
            {
                return ClientHelper.Get<DocumentNotifyClientService, DocumentNotifyClientService>();
            }

        }

        public Guid? hblID { get; set; }

        private ICP.Message.ServiceInterface.Message message;

        private bool isFaxSent = true;

        #region init

        /// <summary>
        /// 业务ID
        /// </summary>
        private Guid OperationID { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        private string EmailAddress { get; set; }

        /// <summary>
        /// 传真地址
        /// </summary>
        private string FaxAddress { get; set; }

       
        /// <summary>
        /// 客户信息
        /// </summary>
        CustomerInfo _customerInfo = null;
        private void HookEvent()
        {
            this.previewControl1.BeforeEdit += previewControl1_BeforeEdit;
            this.previewControl1.ClickRefresh += previewControl1_ClickRefresh;
            this.previewControl1.BeforePrint += previewControl1_BeforePrint;
            this.previewControl1.BeforeSendEmail += previewControl1_BeforeSendEmail;
            this.previewControl1.BeforeClickEmail += previewControl1_BeforeClickEmail;
            this.previewControl1.AfterPrint += previewControl1_AfterPrint;
            //this.previewControl1.AfterSendEmail += new EventHandler(previewControl1_AfterSendEmail);
            this.previewControl1.AfterExport += previewControl1_AfterExport;
            this.report1.NewMessage += report1_NewMessage;
            this.previewControl1.BeforeSendFax += previewControl1_BeforeSendFax;
            //点送发送传真
            this.previewControl1.SendFax += previewControl1_SendFax;
            this.previewControl1.AfterSendFax += previewControl1_AfterSendFax;
            //点送发送Email
            this.previewControl1.SendEmail += previewControl1_SendEmail;
            this.previewControl1.AfterExportOperationDocment += previewControl1_AfterExportOperationDocment;
            this.previewControl1.AfterCustomExportOperationDocument += new EventHandler<Microsoft.Practices.CompositeUI.Utility.DataEventArgs<FastReport.Preview.FRFileArchiveMenuInfo>>(previewControl1_AfterCustomExportOperationDocument);
            this.Load += (sender, e) =>
            {
                DocumentNotifyService.DocumentUploadSucessed += OnDocumentUploadSucessed;
            };
        }

        void previewControl1_AfterCustomExportOperationDocument(object sender, Microsoft.Practices.CompositeUI.Utility.DataEventArgs<FastReport.Preview.FRFileArchiveMenuInfo> args)
        {
            DocumentType documentType = (DocumentType)Enum.Parse(typeof(DocumentType), args.Data.DocumentType);
            string fileName = Path.GetFileName(args.Data.FileName);
            string exportedPath = args.Data.ExportPath;
            ArchiveDocument(documentType, fileName, exportedPath);
        }
        public FastReportViewerPart()
        {
            InitializeComponent();
            this.report1.Preview = this.previewControl1;
            HookEvent();

            this.Disposed += delegate
            {
                this.message = null;
               
                this._customerInfo = null;
                this._datasources = null;
                this._parms = null;
                this._previewDataSource = null;
                UnHookEvent();
                if (this.report1 != null)
                {

                    this.report1.Preview = null;
                    this.report1.Dispose();
                    this.report1 = null;
                }


                if (this.previewControl1 != null)
                {
                    this.previewControl1.Dispose();
                    this.previewControl1 = null;
                }
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        private void UnHookEvent()
        {
            if (this.previewControl1 != null)
            {
                this.previewControl1.BeforeEdit -= previewControl1_BeforeEdit;
                this.previewControl1.ClickRefresh -= previewControl1_ClickRefresh;
                this.previewControl1.BeforePrint -= previewControl1_BeforePrint;
                this.previewControl1.BeforeSendEmail -= previewControl1_BeforeSendEmail;
                this.previewControl1.BeforeClickEmail -= previewControl1_BeforeClickEmail;
                this.previewControl1.AfterPrint -= previewControl1_AfterPrint;
                //this.previewControl1.AfterSendEmail += new EventHandler(previewControl1_AfterSendEmail);
                this.previewControl1.AfterExport -= previewControl1_AfterExport;
                this.report1.NewMessage -= report1_NewMessage;
                this.previewControl1.BeforeSendFax -= previewControl1_BeforeSendFax;
                //点送发送传真
                this.previewControl1.SendFax -= previewControl1_SendFax;
                this.previewControl1.AfterSendFax -= previewControl1_AfterSendFax;
                //点送发送Email
                this.previewControl1.SendEmail -= previewControl1_SendEmail;
                this.previewControl1.AfterExportOperationDocment -= previewControl1_AfterExportOperationDocment;
                this.previewControl1.AfterCustomExportOperationDocument -= this.previewControl1_AfterCustomExportOperationDocument;
                DocumentNotifyService.DocumentUploadSucessed -= OnDocumentUploadSucessed;

            }
        }

        void previewControl1_AfterExportOperationDocment(object sender, Microsoft.Practices.CompositeUI.Utility.DataEventArgs<string> args)
        {
            if (_previewDataSource != null && _previewDataSource.Count > 0)
            {
                DocumentType documentType = (DocumentType)_previewDataSource[ICP.Common.ServiceInterface.CommonConstants.DocumentTypeKey];
                string fileName = Path.GetFileName(args.Data);
                string exportedPath = args.Data;
                ArchiveDocument(documentType, fileName, exportedPath);
            }
        }
        private void ArchiveDocument(DocumentType documentType, string fileName,string exportedPath)
        {
            if (this.message == null || this.message.UserProperties == null || _previewDataSource == null)
            {
                return;
            }

            DocumentInfo document = new DocumentInfo();
            document.Id = Guid.NewGuid();
            document.FileSources = FileSource.FDocument;
            document.DocumentType = documentType;
            document.OperationID = this.message.UserProperties.OperationId;
            document.CreateBy = LocalData.UserInfo.LoginID;
            document.CreateByName = LocalData.UserInfo.UserName;
            document.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            document.FormType = this.message.UserProperties.FormType;
            document.Name = fileName;
            document.Type = this.message.UserProperties.OperationType;
            document.FormId = hblID;

            try
            {
                OperationID = document.OperationID;
                ServiceClient.GetClientService<IClientFileService>().Upload(new DocumentInfo[] { document }, new string[] { exportedPath });
            }
            catch (Exception ex)
            {
                string strMessage = LocalData.IsEnglish ? "Save Failure " + ex.Message : "保存失败 " + ex.Message;
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), strMessage);
            }
        }


        private static WorkItem GetWorkItem(IBuilderContext context)
        {
            return context.Locator.Get<WorkItem>(new DependencyResolutionLocatorKey(typeof(WorkItem), null));
        }

        void previewControl1_SendEmail(object sender, EventArgs e)
        {
            Send(false);
        }

        private void InitAttachment()
        {
            message.Attachments.Clear();
            string fullPath = this.ExportFilePath;
            //生成Excel文件
            if (IsExportExcelFile())
                fullPath = previewControl1.ExportExcelFile();

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
            if (Workitem.State["ExportExcelFile"] == null || Workitem.State["ExportExcelFile"] == "")
            {
                return exportExcelFile;
            }
            else
            {
                bool.TryParse(Workitem.State["ExportExcelFile"].ToString(), out exportExcelFile);
                Workitem.State["ExportExcelFile"] = null;
            }

            return exportExcelFile;
        }

        private void OnDocumentUploadSucessed(DocumentInfo[] documents)
        {
            if (documents.Count() <= 0 || documents[0].OperationID != OperationID)
            {
                return;
            }
            string File = documents[0].Name;
            //LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(),
            //LocalData.IsEnglish ? "File :" + File + " Save Successfully" : "文件:" + File + "  保存成功");
        }


        /// <summary>
        /// 发送传真或Email
        /// </summary>
        /// <param name="isFax">是否为发送Fax</param>
        void Send(Boolean isFax)
        {
            isFaxSent = true;
            ICP.Message.ServiceInterface.Message message = InitMessage(isFax ? MessageType.Fax : MessageType.Email);
            OpenSendForm(message);
        }


        /// <summary>
        /// 打开传真或Email发送界面
        /// </summary>
        /// <param name="mailInfo"></param>
        private void OpenSendForm(ICP.Message.ServiceInterface.Message message)
        {
            if (!ClientMessageService.ShowSendForm(message))
            {
                isFaxSent = false;//如果发送失败
            }
            else
            {//发送成功
                if (message.Type != MessageType.Email)
                {
                    isFaxSent = true;
                    ClientMessageService.Save(message);
                }
            }
        }

        void previewControl1_AfterSendFax(object sender, EventArgs e)
        {
            if (!isFaxSent)
            {
                return;
            }
            if (AfterSendFax != null) AfterSendFax(this, e);
        }

        void previewControl1_SendFax(object sender, EventArgs e)
        {
            this.previewControl1.AfterSendFax -= this.previewControl1_AfterSendFax;
            this.previewControl1.AfterSendFax += this.previewControl1_AfterSendFax;
            Send(true);
        }

        void previewControl1_BeforeSendFax(object sender, CancelEventArgs e)
        {
            if (BeforeSendFax != null) BeforeSendFax(this, e);

        }


        private Message.ServiceInterface.Message InitMessage(MessageType type)
        {
            if (message == null)
            {
                message = new ICP.Message.ServiceInterface.Message();
            }
            else
            {
                //message.Body = string.Empty;
                //message.Subject = string.Empty;
            }

            if (type == MessageType.Email && _previewDataSource.ContainsKey(ICP.Common.ServiceInterface.CommonConstants.CustomerEmailAddressKey))
            {
                message.SendTo = _previewDataSource[ICP.Common.ServiceInterface.CommonConstants.CustomerEmailAddressKey].ToString();
            }
            if (type == MessageType.Fax && _previewDataSource.ContainsKey(ICP.Common.ServiceInterface.CommonConstants.CustomerFaxAddressKey))
            {
                message.SendTo = _previewDataSource[ICP.Common.ServiceInterface.CommonConstants.CustomerFaxAddressKey].ToString();
            }

            message.Type = type;
            message.CreateBy = LocalData.UserInfo.LoginID;
            message.CreatorName = LocalData.UserInfo.UserName;
            message.HasAttachment = true;
            message.Id = Guid.NewGuid();
            message.SendFrom = LocalData.UserInfo.EmailAddress;
            InitAttachment();
            return message;
        }

        #region 事件转接

        void previewControl1_BeforeClickEmail(object sender, CancelEventArgs e)
        {
            if (BeforeClickEmail != null) BeforeClickEmail(this, e);
        }

        void previewControl1_AfterSendEmail(object sender, EventArgs e)
        {
            // if (AfterSendEmail != null) AfterSendEmail(this, e);
        }

        void previewControl1_AfterPrint(object sender, EventArgs e)
        {
            if (AfterPrint != null) AfterPrint(this, e);
        }

        void previewControl1_BeforeSendEmail(object sender, CancelEventArgs e)
        {
            if (BeforeSendEmail != null) BeforeSendEmail(this, e);
        }

        void previewControl1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (BeforePrint != null) BeforePrint(this, e);
        }

        void previewControl1_AfterExport(object sender, EventArgs e)
        {
            //Workitem.RootWorkItem.State["ExportPath"] = previewControl1.ExportedPath;
            if (AfterExport != null) AfterExport(sender, e);
        }


        void report1_NewMessage(object sender, EventArgs e)
        {
            if (ReportMessage != null) ReportMessage(sender, e);
        }

        void previewControl1_BeforeEdit(object sender, CancelEventArgs e)
        {
            if (BeforeEdit != null) BeforeEdit(this, e);
        }

        void previewControl1_ClickRefresh(object sender, EventArgs e)
        {
            if (this.previewControl1.ShowRefreshButton == false) return;

            if (this.ClickRefresh != null)
            {
                ClickRefresh(this, e);
            }
            else
            {
                RefreshReport();
            }
        }

        #endregion

        #endregion

        #region IReportViewer 成员

        public string ExportFilePath
        {
            get
            {
                return this.previewControl1.ExportedPath;
            }

        }
        Dictionary<string, object> _datasources;
        List<FastReport.Data.Parameter> _parms;
        string _reportParth = string.Empty;

        public void BindData(string reportParth, Dictionary<string, object> datasources, List<FastReport.Data.Parameter> parms)
        {
            this.report1.Preview = this.previewControl1;
            _reportParth = reportParth;
            if (string.IsNullOrEmpty(_reportParth)) return;
            if (_datasources != null) _datasources.Clear();
            if (_parms != null) _parms.Clear();

            ExtractDataSources(datasources);
            _datasources = datasources;
            if (datasources.ContainsKey("FormId"))
            {
                hblID = (Guid?)datasources["FormId"];
            }
            _parms = parms;
            RefreshReport();
        }

        /// <summary>
        /// 需要传递给预览控件的键值数据
        /// </summary>
        private Dictionary<string, object> _previewDataSource = new Dictionary<string, object>();
        /// <summary>
        /// 从业务传递过来的报表键值对数据源中提取数据
        /// </summary>
        /// <param name="datasources"></param>
        private void ExtractDataSources(Dictionary<string, object> dataSources)
        {
            _previewDataSource.Clear();
            if (dataSources == null || dataSources.Count <= 0)
            {
                return;
            }
            InnerExtractDataSources(dataSources, ICP.Common.ServiceInterface.CommonConstants.DocumentNameKey);
            InnerExtractDataSources(dataSources, ICP.Common.ServiceInterface.CommonConstants.DocumentTypeKey);
            InnerExtractDataSources(dataSources, ICP.Common.ServiceInterface.CommonConstants.CustomerEmailAddressKey);
            InnerExtractDataSources(dataSources, ICP.Common.ServiceInterface.CommonConstants.CustomerFaxAddressKey);
            InnerExtractDataSources(dataSources, ICP.Common.ServiceInterface.CommonConstants.FileArchiveMenuInfoKey);
            this.previewControl1.BindData(_previewDataSource);
        }
        private void InnerExtractDataSources(Dictionary<string, object> dataSources, string key)
        {
            if (dataSources.ContainsKey(key))
            {
                _previewDataSource.Add(key, dataSources[key]);
                dataSources.Remove(key);
            }
        }

        public void BindData(string reportPath, Dictionary<string, object> datasources, List<FastReport.Data.Parameter> parms, ICP.Message.ServiceInterface.Message message)
        {
            BindData(reportPath, datasources, parms);
            this.message = message;

        }
        private void RefreshReport()
        {
            report1.Load(_reportParth);


            if (_datasources != null)
            {
                foreach (var item in _datasources)
                {
                    BindingSource bs = new BindingSource();
                    bs.DataSource = item.Value;
                    this.report1.RegisterData(bs, item.Key);
                }
            }
            if (_parms != null)
            {
                foreach (var item in _parms)
                {
                    this.report1.Parameters.Add(item);
                }
            }
            this.report1.Refresh();
            this.report1.Show();
        }

        #region  自定义事件

        /// <summary>
        /// 编辑之前事件,可以设置e.Cancel=true以取消编辑
        /// </summary>
        public event CancelEventHandler BeforeEdit;

        /// <summary>
        /// 点击打印事件前
        /// </summary>
        public event CancelEventHandler BeforePrint;

        /// <summary>
        /// 点击打印事件后
        /// </summary>
        public event EventHandler AfterPrint;

        /// <summary>
        /// 点击输出Email前
        /// </summary>
        public event CancelEventHandler BeforeSendEmail;
        /// <summary>
        /// 发送Email
        /// </summary>
        public event EventHandler SendEmail;


        /// <summary>
        /// 点击输出Email后
        /// </summary>
        //public event EventHandler AfterSendEmail;
        public event CancelEventHandler BeforeSendFax;
        public event EventHandler SendFax;
        public event EventHandler AfterSendFax;
        /// <summary>
        /// 输出为文件后
        /// </summary>

        public event EventHandler AfterExport;

        /// <summary>
        /// 点击刷新事件
        /// </summary>
        public event EventHandler ClickRefresh;


        /// <summary>
        /// 由报表发出的指令,用于和报表交互
        /// </summary>
        public event EventHandler ReportMessage;

        /// <summary>
        /// 点击Email前
        /// </summary>
        public event CancelEventHandler BeforeClickEmail;
        /// <summary>
        /// 报表归档事件
        /// </summary>
        public event EventHandler Archive;

        #endregion

        Microsoft.Practices.CompositeUI.WorkItem IReportViewer.Workitem
        {
            get { return Workitem; }
        }

        #endregion

        internal void ShowSetupWorkspace()
        {
            //this.dockManager1.PaneFromControl(this.setupWorkspace).Show();
        }

        string faxNumbers;
        string mailAddress;
        string key1;
        string key2;
        short? _docType = null;

        internal void BindVaules(string faxNumbers, string mailAddress, string key1, string key2, short? docType)
        {
            this.faxNumbers = faxNumbers;
            this.mailAddress = mailAddress;
            this.key1 = key1;
            this.key2 = key2;
            _docType = docType;
        }
    }
}
