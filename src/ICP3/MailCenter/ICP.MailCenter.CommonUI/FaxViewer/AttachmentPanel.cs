using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;

namespace ICP.MailCenter.CommonUI
{
    public partial class AttachmentPanel : FlowLayoutPanel
    {
        private int childIndex = 1;
        private ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
        public ICP.Message.ServiceInterface.Message DataSource
        {
            get { return this.message; }
            set { this.message = value;
            this.SuspendLayout();
            AddChildControls();
            this.ResumeLayout();     
            }
        }

        private void AddChildControls()
        {
            this.Controls.Clear();
           // this.Visible = false;
            if (this.message == null || this.message.Attachments==null || this.message.Attachments.Count <= 0)
            {
                return;
            }
           // this.Visible = true;
            FileSaveContext context = new FileSaveContext { FileType= SaveFileType.CommunicationHistory, Id=this.message.Id };
            //ThreadPool.QueueUserWorkItem(data =>
            //{
            //    Array.ForEach(this.message.Attachments.ToArray(), attachment =>
            //    {

            //        if (this.InvokeRequired)
            //        {
            //            AddDelegate addDelegate = new AddDelegate(InnerAddChildControl);
            //            this.Invoke(addDelegate, this.message.Attachments, context);
            //        }
            //        else

                        InnerAddChildControl(this.message.Attachments, context);
             //  });
       
           // });

        }
       
        delegate void AddDelegate(List<AttachmentContent> contents,FileSaveContext context);
        private void InnerAddChildControl(List<AttachmentContent> contents, FileSaveContext context)
        {
            
                List<UCFaxAttachment> listUC = new List<UCFaxAttachment>();
                foreach (AttachmentContent content in contents)
                {
                    UCFaxAttachment ucAttachment = new UCFaxAttachment();
                    ucAttachment.Name = "ucAttachment" + (childIndex++).ToString();
                    ucAttachment.Context = context;
                    ucAttachment.AttachmentContent = content;
                    listUC.Add(ucAttachment);
                }
                childIndex = 0;
                
                this.SuspendLayout();
                this.Controls.AddRange(listUC.ToArray());
                this.ResumeLayout();
            
          
           

        }

        public AttachmentPanel()
        {
             InitializeComponent();
           
             
        }

        public AttachmentPanel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            this.Disposed += delegate {
                this.message = null;

            };
        }

        private void AttachmentPanel_MouseEnter(object sender, EventArgs e)
        {
            this.Focus();
        }
    }
}
