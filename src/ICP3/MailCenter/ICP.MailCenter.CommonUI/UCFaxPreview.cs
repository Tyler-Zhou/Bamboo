using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Message.ServiceInterface;
using Microsoft.Practices.CompositeUI;


namespace ICP.MailCenter.CommonUI
{
    /// <summary>
    /// 传真预览控件
    /// </summary>
    public partial class UCFaxPreview : XtraUserControl, IReadOnlyControl
    {

        [ServiceDependency]
        public IClientCommunicationHistoryService ClientHistoryService { get; set; }
        private ICP.Message.ServiceInterface.Message dataSource;
        
        public UCFaxPreview()
        {
            InitializeComponent();
            EventUtility.PreviewAction += OpenAttachment;
            EventUtility.ShowBodyAction += ShowBody;
            this.SizeChanged += new EventHandler(UCFaxPreview_SizeChanged);
            this.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.FindForm().Close();
                }
            };
            this.Disposed += delegate {
                this.dataSource = null;
                this.ClientHistoryService = null;
                if (this.ucFaxDetail != null)
                {
                    this.ucFaxDetail.Dispose();
                    this.ucFaxDetail = null;
                }
                EventUtility.PreviewAction -= OpenAttachment;
                EventUtility.ShowBodyAction -= ShowBody;
            };
        }

        void UCFaxPreview_SizeChanged(object sender, EventArgs e)
        {
            this.pnlContent.Dock = DockStyle.Fill;
            this.richEditBody.Dock = DockStyle.Fill;
          
        }


        private void ShowBody(SimpleButton btnSource)
        {
            if (btnSource.FindForm() != this.FindForm())
                return;
            
            SwitchControl(false);

        }
        private bool readOnly = false;
        public bool ReadOnly
        {
            get { return this.readOnly; }
            set
            {
                this.readOnly = value;
                SetChildControlReadOnly();
            }
        }
        public void SetChildControlReadOnly()
        {
            if (this.ucFaxDetail != null)
            {
                this.ucFaxDetail.ReadOnly = this.readOnly;
                this.richEditBody.ReadOnly = this.readOnly;
            }

        }
        public string AttachmentClientPath
        {
            get;
            set;
        }

        private void OpenAttachment(AttachmentLabel lblSource, AttachmentContent attachment)
        {
            if (lblSource.FindForm() != this.FindForm())
                return;
            //EnsurePreviewControlExists();
           // SwitchControl(true);
            LoadAttachment(attachment);
        }

        private void LoadAttachment(AttachmentContent attachment)
        {
            AttachmentClientPath = attachment.ClientPath;
            if (string.IsNullOrEmpty(AttachmentClientPath))
            {

                if (ClientHistoryService == null)
                {
                    ClientHistoryService = ServiceClient.GetClientService<IClientCommunicationHistoryService>();
                }
                AttachmentContent content = ClientHistoryService.GetAttachment(dataSource.Id, new List<Guid> { attachment.Id }).First();
                AttachmentClientPath = content.ClientPath;
            }
            //this.ucPreviewControl.Load(AttachmentClientPath);
          
            //this.ucPreviewControl.Focus();
            ServiceClient.FilePreviewService.Preview(AttachmentClientPath, PointToScreen(this.pnlContent.Location), this.pnlContent.Size, true);
        }

        public void PrintPDF()
        {
            ServiceClient.FilePreviewService.Print();
        }

        private void SwitchControl(bool isShowPreviewControl)
        {
         
            this.richEditBody.Visible = !isShowPreviewControl;
        }

    
        public void BindData(ICP.Message.ServiceInterface.Message entry)
        {
            SetDataSource(entry);
            if (!this.richEditBody.Visible)
            {
                SwitchControl(false);
            }
            SetChildControlData();


        }

        private void SetChildControlData()
        {
            this.ucFaxDetail.DataSource = dataSource;
            ReadBodyContent();
        }

        private void ReadBodyContent()
        {
            if (string.IsNullOrEmpty(dataSource.Body))
            {
                this.richEditBody.Text = string.Empty;
            }
            else
            {
                byte[] body = Encoding.UTF8.GetBytes(dataSource.Body);
                using (MemoryStream ms = new MemoryStream(body))
                {
                    DevExpress.XtraRichEdit.DocumentFormat format = (dataSource.BodyFormat == BodyFormat.olFormatHTML ? DevExpress.XtraRichEdit.DocumentFormat.Html : DevExpress.XtraRichEdit.DocumentFormat.PlainText);
                    this.richEditBody.LoadDocument(ms, format);
                }

            }

        }
        private void SetDataSource(ICP.Message.ServiceInterface.Message entry)
        {
            if (entry == null)
            {
                entry = new ICP.Message.ServiceInterface.Message();
            }
            dataSource = entry;
        }

    }
}
