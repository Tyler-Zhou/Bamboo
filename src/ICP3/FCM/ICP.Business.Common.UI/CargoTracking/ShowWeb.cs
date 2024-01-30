using System;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.Business.Common.UI
{
    /// <summary>
    ///
    /// </summary>
    [ToolboxItem(false)]
    public partial class ShowWeb : BaseEditPart
    {
        public ShowWeb()
        {
            InitializeComponent();
            webBrowser1.ScriptErrorsSuppressed = true;
        }

        public override object DataSource
        {
            set
            {
                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    string str = value.ToString();
                    if (str.Substring(0, 3).ToLower() == "url")
                        webBrowser1.Navigate(str.Substring(3));
                    else
                        webBrowser1.DocumentText = value.ToString();
                }
            }
        }
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //将所有的链接的目标，指向本窗体
            foreach (HtmlElement archor in this.webBrowser1.Document.Links)
            {
                archor.SetAttribute("target", "_self");
            }

            //将所有的FORM的提交目标，指向本窗体
            foreach (HtmlElement form in this.webBrowser1.Document.Forms)
            {
                form.SetAttribute("target", "_self");
            }
        }
        protected override void OnLoad(EventArgs e)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            webBrowser1.Navigate(webBrowser1.StatusText);
        }

    }
}
