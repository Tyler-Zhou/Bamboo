using System;
using System.Windows.Forms;
using ICP.Sys.UI.SystemHelp.Comm;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using System.IO;

namespace ICP.Sys.UI.SystemHelp.NewFeedback
{
    public partial class NewFeedbackPart : ICP.Framework.ClientComponents.UIFramework.BasePart
    {   
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public ISystemService SystemService
        {
            get
            {
                return ServiceClient.GetService<ISystemService>();
            }
        }

        public string EMail
        {
            get
            {
                return LocalData.UserInfo.EmailAddress;
            }
        }

        bool _IsPostBack = false;
        private System.Windows.Forms.WebBrowser webBrowser;


        public NewFeedbackPart()
        {
            InitializeComponent();
            
            cmbLevel.SelectedIndex = 0;
            InitData();
            this.Disposed += delegate
            {
                if (this.webBrowser != null)
                {
                    this.webBrowser.DocumentCompleted -= this.webBrowser1_DocumentCompleted;
                    this.webBrowser = null;
                }
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }

            
            };
        }
        /// <summary>
        /// 如果是用户从错误列表反馈错误，则从全局缓冲中读取异常信息
        /// </summary>
        private void InitData()
        {
            WorkItem workItem = ServiceClient.GetClientService<WorkItem>();
            string attachmentPath = workItem.State["ScreenImagePath"] as string;
            
            string errorInfo = workItem.State["TotalErrorMessage"] as string;
            if (!String.IsNullOrEmpty(errorInfo))
            {
                this.txtContent.Text = errorInfo;
                int length = errorInfo.Length >= 150 ? 150 : errorInfo.Length;
                this.txtSubject.Text = errorInfo.Substring(0, length);
            }
            if (!string.IsNullOrEmpty(attachmentPath) && File.Exists(attachmentPath))
            {
                AddAttachment(attachmentPath);
            }
            workItem.State["TotalErrorMessage"] = string.Empty;
            workItem.State["ScreenImagePath"] = string.Empty;
        }

        void InitWebBrowser()
        {
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            // 
            // webBrowser1
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(553, 438);
            this.webBrowser.TabIndex = 0;
            this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);

            webBrowser.Visible = false;
            lblLoading.Visible = true;
            webBrowser.Navigate(string.Format(HelpConstants.Url_NewFeedback, Guid.NewGuid().ToString()));
        }

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
                webBrowser.Visible = true;
                lblLoading.Visible = false;
                Html(HelpConstants.Url_NewFeedback_AssignToElementID).InnerText = "zly";
                Html(HelpConstants.Url_NewFeedback_CreatebyElementID).InnerText = EMail;

                _IsPostBack = true;
            }
        }

        HtmlElement Html(string elementID)
        {
            return webBrowser.Document.GetElementById(elementID);
        }






        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                AddAttachment(openFileDialog1.FileName);
            }
        }
        private void AddAttachment(string filePath)
        {
            //由于接口复杂原因，只能上传一个附件。
            lvAttachment.Items.Clear();

            var file = new FileInfo(filePath);
            lvAttachment.Items.Add(new ListViewItem() { Text = file.Name, Tag = file });
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSelectFile_Click(null, null);
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvAttachment.SelectedItems.Count == 0) return;

            foreach (ListViewItem sub in lvAttachment.SelectedItems)
            {
                lvAttachment.Items.Remove(sub);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            lblSaving.Visible = true;
            lblSaving.Refresh();

            try
            {
                if (txtSubject.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("[Subject] is required.");
                    return;
                }

                if (txtContent.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("[Content] is required.");
                    return;
                }

                //try
                //{
                //level
                string level;
                switch (cmbLevel.SelectedIndex)
                {
                    case 0:
                        level = "低";
                        break;
                    case 1:
                        level = "中";
                        break;
                    case 2:
                        level = "高";
                        break;
                    case 3:
                        level = "严重";
                        break;
                    default:
                        level = "低";
                        break;
                }

                //desc
                string desc = string.Empty; ;
                if (txtContent.Lines.Length == 1)
                {
                    desc = txtContent.Text;
                }
                else
                {
                    foreach (var sub in txtContent.Lines)
                    {
                        desc += string.Format(SQLCmdMark_DescLine, sub);
                    }
                }

                if (lvAttachment.Items.Count == 0)
                    SystemService.AddFeedBack(txtSubject.Text, desc, null, level, EMail, null, null, null);
                else
                {
                    //attachment
                    FileInfo attachment = (FileInfo)lvAttachment.Items[0].Tag;
                    Guid attachmentGuid = Guid.NewGuid();
                    byte[] content = ICP.Common.ServiceInterface.IOHelper.ReadFileContentFromDisk(attachment.FullName);

                    SystemService.AddFeedBack(txtSubject.Text, desc, null, level, EMail, attachmentGuid, attachment.Name, content);
                }

                MessageBox.Show("Saving is done. Thanks for your feedback.", "Feedback", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.FindForm().Close();
            }
            catch
            {
                //error log
                throw;
            }
            finally
            {
                lblSaving.Visible = false;
            }
        }

        string SQLCmdMark_DescLine = @"<P>{0}</P>";
        /// <summary>
        /// 双击查看附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvAttachment_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = lvAttachment.FocusedItem;
            if (item == null)
                return;
            var fileInfo = item.Tag as FileInfo;

            if (fileInfo == null)
                return;
            System.Diagnostics.Process.Start(fileInfo.FullName);
        }
    }
}
