using System;
using System.Windows.Forms;
using ICP.Sys.UI.SystemHelp.Comm;
using ICP.Sys.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;

namespace ICP.Sys.UI.SystemHelp.MyFeedback
{
    public partial class MyFeedbackPart : ICP.Framework.ClientComponents.UIFramework.BasePart
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


        public MyFeedbackPart()
        {
            InitializeComponent();
            webBrowser1.Visible = false;
            lblLoading.Visible = true;
            webBrowser1.Navigate(string.Format(HelpConstants.Url_MyFeedbacks, Guid.NewGuid().ToString()));
            this.Disposed += delegate
            {
                if (this.WorkItem != null)
                {
                    this.webBrowser1.DocumentCompleted -= this.webBrowser1_DocumentCompleted;
                    this.webBrowser1 = null;
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }

        bool _IsPostBack = false;

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //redirect to login page.
            if (e.Url.ToString().ToLower().IndexOf(HelpConstants.Url_Login) == 0)
            {
                //auto-fill the user and pwd.
                Html(HelpConstants.Url_Login_UserElementID).InnerText = "icpuser";
                Html(HelpConstants.Url_Login_PwdElementID).InnerText = "icpuser";
                Html(HelpConstants.Url_Login_RememberMeElementID).SetAttribute("Checked", "checked");

                //submit
                Html(HelpConstants.Url_Login_SubmitElementID).InvokeMember("click");
            }
            else if (_IsPostBack == false)
            {
                webBrowser1.Visible = true;
                lblLoading.Visible = false;

                //webBrowser1.Document.InvokeScript(@"_ctl00_c_qe1.addClause");

                Html(HelpConstants.Url_MyFeedbacks_FieldElementID).InnerText = "发现环境";
                Html(HelpConstants.Url_MyFeedbacks_OperatorElementID).InnerText = "包含";
                Html(HelpConstants.Url_MyFeedbacks_CreatebyElementID).InnerText = EMail;
                Html(HelpConstants.Url_MyFeedbacks_SearchElementID).InvokeMember("click");
                _IsPostBack = true;

            }
        }

        HtmlElement Html(string elementID)
        {
            return webBrowser1.Document.GetElementById(elementID);
        }
    }
}
