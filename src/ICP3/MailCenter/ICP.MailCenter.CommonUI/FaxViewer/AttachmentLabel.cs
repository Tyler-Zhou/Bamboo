using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;


namespace ICP.MailCenter.CommonUI
{
    public partial class AttachmentLabel : HyperLinkEdit
    {
        public FileSaveContext Context { get; set; }
       
        public IClientCommunicationHistoryService CommunicationHistoryService { get; set; }

        private AttachmentContent _attachmentContent;
        public AttachmentContent AttachmentContent
        {
            get { return this._attachmentContent; }
            set {
                this._attachmentContent = value;
                SetAttachmentInfo();
            }
        }

        private void SetAttachmentInfo()
        {
            if (this._attachmentContent == null)
            {
                return;
            }
            this.Text = string.Format("{0}({1})", _attachmentContent.DisplayName,ICP.Framework.CommonLibrary.Common.CommonHelper.GetFileSizeString(_attachmentContent.Size));
        }
   
        public AttachmentLabel()
        {   
            InitializeComponent();
            this.Margin = new Padding(0);
            Init();
            this.Disposed += delegate {

                this.Context = null;
                this.CommunicationHistoryService = null;
                this._attachmentContent = null;
            };
        }

        private void Locale()
        {
            this.toolStripItemOpen.Text = "打开";
            this.toolStripItemPreview.Text = "预览";
            this.toolStripItemSaveas.Text = "另存为";
        }

        public AttachmentLabel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            Init();
        }

        private void Init()
        {
            this.Properties.Appearance.Options.UseFont = true;
            this.Properties.ContextMenuStrip = contextMenuStrip;
            Locale();
        }

        private void toolStripItemOpen_Click(object sender, EventArgs e)
        {
            EnsureServiceExists();
            string filePath = this._attachmentContent.ClientPath;
            if (!string.IsNullOrEmpty(filePath))
            {
                System.Diagnostics.Process.Start(filePath);
            }
            else
            {
               this._attachmentContent.ClientPath= CommunicationHistoryService.Open(Context.Id, _attachmentContent.Id);
            }

        }

        private void toolStripItemPreview_Click(object sender, EventArgs e)
        {
            if (EventUtility.PreviewAction != null)
            {

                EventUtility.PreviewAction(this, this._attachmentContent);
            }
        }

        private void toolStripItemSaveas_Click(object sender, EventArgs e)
        {
            EnsureServiceExists();
            CommunicationHistoryService.SaveAs(Context.Id,_attachmentContent.Id,_attachmentContent.Name);
        }
       private void AttachmentLabel_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
       {   

           e.Handled = true;
           toolStripItemPreview.PerformClick();
       }
       private void EnsureServiceExists()
       {
           if (CommunicationHistoryService == null)
           {
               CommunicationHistoryService = ServiceClient.GetClientService<IClientCommunicationHistoryService>();
           }
       }

      
    }
}
