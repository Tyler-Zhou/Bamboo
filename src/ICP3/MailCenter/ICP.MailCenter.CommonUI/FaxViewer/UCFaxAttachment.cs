using System.Drawing;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Message.ServiceInterface;

namespace ICP.MailCenter.CommonUI
{
    public partial class UCFaxAttachment : UserControl
    {
        private AttachmentContent _attachmentContent;
        private FileSaveContext context;
        public FileSaveContext Context
        {
            get { return this.context; }
            set { this.context = value; }
        }
        public AttachmentContent AttachmentContent
        {
            get {return this._attachmentContent; }
            set {
                this._attachmentContent = value;
                SetChildProperty();
            }
        }

        private void SetChildProperty()
        {
            if (this._attachmentContent == null)
            {
                return;
            }
            string fileType = System.IO.Path.GetExtension(_attachmentContent.Name);
            Icon icon = IconHelper.GetIconByFileType(fileType, false);
            pictureBox.Image = icon == null ? new Bitmap(16, 16) : icon.ToBitmap();
            attachmentLabel.Context = context;
            attachmentLabel.AttachmentContent = _attachmentContent;
        }
        public UCFaxAttachment()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.context = null;
                this._attachmentContent = null;
            };
        }
    }
}
