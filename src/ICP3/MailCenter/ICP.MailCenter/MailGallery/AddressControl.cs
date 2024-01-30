using System;
using System.Windows.Forms;
using ICP.MailCenter.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using System.Drawing;
using System.Diagnostics;
using Microsoft.Office.Interop.Outlook;
using outlook = Microsoft.Office.Interop.Outlook;
using Microsoft.Practices.CompositeUI;
using System.Threading;
namespace ICP.MailCenter.UI
{
    /// <summary>
    /// 地址控件
    /// </summary>
    public class AddressControl : Control
    {
        Font font = null;
        /// <summary>
        /// 接收人地址类型
        /// </summary>
        public int RecipientType
        {
            get;
            set;
        }
        private static Microsoft.Practices.CompositeUI.Collections.ManagedObjectCollection<object> _SmartParts;
        public static Microsoft.Practices.CompositeUI.Collections.ManagedObjectCollection<object> SmartParts
        {
            get
            {
                if (_SmartParts == null)
                    _SmartParts = ServiceClient.GetClientService<WorkItem>().SmartParts;
                return _SmartParts;
            }
        }

        public EmailDetailPart DetailPart
        {
            get
            {
                return SmartParts.Get<EmailDetailPart>(MailCenterWorkSpaceConstants.EmailDetailPart);
            }
        }

        public EmailListPart ListPart
        {
            get { return SmartParts.Get<EmailListPart>(MailCenterWorkSpaceConstants.EmailListPart); }

        }

        private string address;
        /// <summary>
        /// 控件对应的Email地址
        /// </summary>
        public String Address
        {
            get
            {
                if (!LocalData.IsDesignMode)
                {

                    if (Process.GetProcessesByName("OUTLOOK").Length == 0)
                    {
                        //MailUtility.StartProcess();
                        ClientUtility.GetOutlookNewNameSpace();
                        try
                        {
                            ListPart.RefershCtl();
                            MailListPresenter.BindViewCtlData(ListPart.axViewCtlEmailList, ClientProperties.folder_FullPath, string.Empty);
                        }
                        catch { }
                        finally
                        {
                            MailListPresenter.BindViewCtlData(ListPart.axViewCtlEmailList, ClientProperties.folder_FullPath, string.Empty);
                            address = MailListPresenter.GetEmailAddress(address, this.RecipientType, MailListPresenter.GetMailRecipients());
                        }
                    }
                    else
                    {
                        Recipients olRecipients = null;
                        if (ClientProperties.CurrentMailItem != null)
                        {

                            try
                            {
                                if (ClientProperties.CurrentMailItem != null && ClientProperties.CurrentMailItem.Recipients.Count != 0)
                                {
                                    olRecipients = ClientProperties.CurrentMailItem.Recipients;
                                }
                            }
                            catch
                            {
                                olRecipients = MailListPresenter.GetMailRecipients();
                            }
                            address = MailListPresenter.GetEmailAddress(this.Text, this.RecipientType, olRecipients);
                        }
                    }
                }
                return address;
            }

        }
        /// <summary>
        /// 控件显示的文本
        /// </summary>
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                Size sz = Size.Empty;
                //Size sz = new Size(160, 500);

                sz = TextRenderer.MeasureText(value, this.Font, sz);
                this.Height = sz.Height + 2;
                this.Margin = new Padding(0);
                this.Padding = new Padding(0);
                this.Width = sz.Width + 10;
            }
        }
        public Size MeasureText(string Text, Font Font)
        {
            TextFormatFlags flags
              = TextFormatFlags.Left
              | TextFormatFlags.Top
              | TextFormatFlags.NoPadding
              | TextFormatFlags.NoPrefix
              ;
            Size szProposed = new Size(int.MaxValue, int.MaxValue);
            // Size sz1 = TextRenderer.MeasureText("m", Font, szProposed, flags);
            Size sz2 = TextRenderer.MeasureText(Text, Font, szProposed, flags);
            return new Size(sz2.Width + 5, sz2.Height + 5);
        }


        public IOutLookService OutLookService
        {
            get
            {
                return ServiceClient.GetClientService<IOutLookService>();
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            if (!LocalData.IsDesignMode)
            {
                e.Graphics.DrawString(this.Text, this.Font, SystemBrushes.ControlText, 0, 0);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="text"></param>
        /// <param name="recipientType"></param>
        public AddressControl(string text, int recipientType)
            : this()
        {
            font = new Font(this.Font, FontStyle.Bold);
            this.Text = text;
            this.RecipientType = recipientType;
        }
        public void ResetInfo(string text, int recipientType)
        {
            this.SuspendLayout();
            this.Text = text;
            this.RecipientType = recipientType;
            this.ResumeLayout(true);

        }
        public AddressControl()
        {
            this.BackColor = SystemColors.Window;
            this.ContextMenuStrip = MenuService.CreateContextMenu(OnMenuItemClick);
            this.Cursor = Cursors.IBeam;
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.MouseDoubleClick += new MouseEventHandler(AddressControl_MouseDoubleClick);
            this.MouseClick += new MouseEventHandler(AddressControl_MouseClick);
        }

        /// <summary>
        /// 用户点击联系人地址，系统默认使用了Ctrl+V 实现复制功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AddressControl_MouseClick(object sender, MouseEventArgs e)
        {
            //鼠标左键单击
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                this.BackColor = Color.LightSteelBlue;
                MailListPresenter.CopyText(this.Address);
                Thread.Sleep(600);
                this.BackColor = SystemColors.Window;
            }
        }
        /// <summary>
        /// 双击打开outlook联系人界面
        /// </summary>
        void AddressControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            WaitCallback callback = (obj) => InnerOpenContactForm(sender, e);
            ThreadPool.QueueUserWorkItem(callback);
        }
        /// <summary>
        /// 打开Outlook联系人界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InnerOpenContactForm(object sender, MouseEventArgs e)
        {
            bool isCloseOL = false;
            MAPIFolder olFolder = null;
            SelectNamesDialog snd = null;
            Recipients olRecipients = null;
            MailItem olItem = null;
            outlook.Application app = null;
            NameSpace olNS = null;
            try
            {
                //打开联系人信息
                if (e.Button == MouseButtons.Left && e.Clicks == 2)
                {
                    AddressControl contextLink = sender as AddressControl;
                    if (string.IsNullOrEmpty(contextLink.Text) || contextLink.Text == "[ ]") return;
                    string address = string.Empty;
                    if (MailUtility.GetProcessByName("OUTLOOK").Length == 0)
                    {
                        isCloseOL = true;
                        address = MailListPresenter.ReGetEmailAddress(contextLink.Text, contextLink.RecipientType);
                    }
                    else
                    {
                        if (ClientProperties.CurrentMailItem == null)
                            address = contextLink.Text;
                        else
                            address = MailListPresenter.GetEmailAddress(contextLink.Text, contextLink.RecipientType, ClientProperties.CurrentMailItem.Recipients);
                    }
                    olFolder = MailListPresenter.DefaultContactsFolder;
                    bool isContainsContactItem = MailListPresenter.RefileContacts(olFolder, address);
                    if (!isContainsContactItem)
                    {
                        string nickName = MailListPresenter.GetNickName(contextLink.Text);
                        snd = MailListPresenter.ResettingRecipients(ClientUtility.OlNS.GetSelectNamesDialog(), address);
                        string type = MailListPresenter.GetAccountType(snd);
                        using (frmOutlookProperties form = new frmOutlookProperties(nickName, address, type) { TopMost = true })
                        {
                            form.ShowDialog();
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                if (ex.Message.Contains("RPC"))
                {
                    ClientUtility.GetOutlookNewNameSpace();
                }
                else
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            }
            finally
            {
                if (isCloseOL)
                {
                    ClientProperties.selectedChanged = false;
                    ListPart.axViewCtlEmailList.Folder = ClientProperties.folder_FullPath;
                }
                MailUtility.ReleaseComObject(olFolder);
                MailUtility.ReleaseComObject(olRecipients);
                MailUtility.ReleaseComObject(olItem);
                MailUtility.ReleaseComObject(snd);
                olNS = null;
                MailUtility.ReleaseComObject(app);
            }
        }

        /// <summary>
        ///  new mail
        /// </summary>
        public void NewMail()
        {
            try
            {
                this.OutLookService.AddNewEmail(this.Address);
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "add failure: " : "添加失败: ") + ex.Message);
            }
        }

        private void OnMenuItemClick(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;

            ContactType type = (ContactType)(Enum.Parse(typeof(ContactType), item.Tag.ToString()));
            switch (type)
            {
                case ContactType.NewMail:
                    NewMail();
                    break;
                case ContactType.CopyContact:
                    MailListPresenter.CopyText(this.Address);
                    break;
                case ContactType.AddToContact:
                    MailListPresenter.AddToContact(this.Text, this.Address);
                    break;
                default:
                    try
                    {
                        MailListPresenter.OpenAdvancedSearchPart(type, this.Address);
                    }
                    catch (System.Exception ex)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                    }
                    break;

            }

        }

    }
}
