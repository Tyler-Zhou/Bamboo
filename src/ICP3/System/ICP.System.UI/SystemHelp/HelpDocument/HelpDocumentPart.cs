using System.Windows.Forms;
using ICP.Sys.UI.SystemHelp.Comm;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Sys.UI.SystemHelp.HelpDocument
{
    public partial class HelpDocumentPart : ICP.Framework.ClientComponents.UIFramework.BasePart
    {  
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        public string email;
        public string EMail
        {
            get
            {
                if (string.IsNullOrEmpty(email))
                {
                    email = LocalData.UserInfo.EmailAddress;
                }

                return email;
            }
        }


        public HelpDocumentPart()
        {
            InitializeComponent();
            webBrowser1.Visible = false;
            lblLoading.Visible = true;
            webBrowser1.Navigate(HelpConstants.Url_HelpDocument);
            this.Disposed += delegate {
                this.webBrowser1.DocumentCompleted -= this.webBrowser1_DocumentCompleted;
                this.webBrowser1 = null;
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            };
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser1.Visible = true;
            lblLoading.Visible = false;
        }
    }
}
