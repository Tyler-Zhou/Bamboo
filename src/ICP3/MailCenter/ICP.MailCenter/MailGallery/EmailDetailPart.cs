using System;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Common;
using ICP.MailCenter.ServiceInterface;
using ICP.Message.ServiceInterface;
using Microsoft.Practices.CompositeUI.SmartParts;
using System.ComponentModel;
using System.Drawing;
using Microsoft.Office.Interop.Outlook;
using ICP.Framework.CommonLibrary.Client;
using System.IO;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using System.Linq;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.MailCenter.UI
{
    /// <summary>
    /// 邮件阅读窗口类
    /// </summary>    
    [ToolboxItem(false)]
    [SmartPart]
    public partial class EmailDetailPart : System.Windows.Forms.UserControl
    {
        #region 服务
        /// <summary>
        /// 根WorkItem
        /// </summary>
        public WorkItem RootWorkItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }

        #endregion

        #region 常量
        string BusinessPartWidthKey = "BusinessPartWidth";
        /// <summary>
        /// 邮件是否已读键
        /// 当选择一封未读邮件时，当前选择邮件事件会触发两次，
        /// 为了避免处理两次，临时设置邮件的属性
        /// </summary>
        private const string IsMailReadKey = "IsMailRead";
        private const string MailCenterValue = "CityOcean";


        /// <summary>
        /// 附件集合
        /// </summary>
        public List<AttachmentContent> listAttachments
        {
            get { return ClientProperties.Attachments; }
            set { ClientProperties.Attachments = value; }
        }

        /// <summary>
        /// 当前选择的邮件是否以读取过
        /// </summary>
        bool currentMailIsRead = true;
        /// <summary>
        /// 当前邮件对象
        /// </summary>
        MailItem currentMail = null;
        /// <summary>
        /// 当前选择的Message对象
        /// </summary>
        public Message.ServiceInterface.Message message { get; set; }

        #endregion

        #region 属性
        private Microsoft.Practices.CompositeUI.Collections.ManagedObjectCollection<object> _SmartParts;
        public Microsoft.Practices.CompositeUI.Collections.ManagedObjectCollection<object> SmartParts
        {
            get
            {
                if (_SmartParts == null)
                    _SmartParts = this.RootWorkItem.SmartParts;
                return _SmartParts;
            }
        }

        public Microsoft.Practices.CompositeUI.Collections.ManagedObjectCollection<Command> Commands
        {
            get { return RootWorkItem.Commands; }
        }

        public EmailFolderPart FolderPart
        {
            get
            {
                return SmartParts.Get<EmailFolderPart>(MailCenterWorkSpaceConstants.EmailFolderPart);
            }
        }
        public EmailListPart ListPart
        {
            get
            {
                return SmartParts.Get<EmailListPart>(MailCenterWorkSpaceConstants.EmailListPart);
            }
        }

        private EmailToolBarPart _ToolBarPart;
        public EmailToolBarPart ToobarPart
        {
            get
            {
                if (_ToolBarPart == null)
                    _ToolBarPart = SmartParts.Get<EmailToolBarPart>(MailCenterWorkSpaceConstants.EmailToolBarPart);
                return _ToolBarPart;
            }
        }

        public IMainForm mainForm
        {
            get { return ServiceClient.GetClientService<IMainForm>(); }
        }

        #endregion

        #region 初始化
        /// <summary>
        /// 构造函数
        /// </summary>
        public EmailDetailPart()
        {
            InitializeComponent();
            if (!LocalData.IsDesignMode)
            {
                InitControl();
                this.Disposed += delegate { DisposedCompent(); };
            }
        }

        void InitControl()
        {
            if (!LocalData.IsEnglish)
            {
                this.lblSender.Text = "发件人:";
                this.lblCC.Text = "抄送:";
                this.lblRecipients.Text = "收件人:";
                this.lblAttachment.Text = "附件:";
                this.lblSendTime.Text = "接收于:";
                this.txtDescription.Text = "  该邮件尚未发送。";
            }
        }


        private void DisposedCompent()
        {
            if (!LocalData.IsDesignMode)
            {
                if (mainForm != null)
                {
                    mainForm.MainFormActivated -= new EventHandler(mainForm_Activated);
                    mainForm.MainFormDeactivated -= new EventHandler(mainForm_Deactivated);
                    mainForm.ApplicationExit -= new EventHandler(OnApplicationExit);
                }
            }
            ClientUtility.FileIcons.Clear();
            _ToolBarPart = null;
            _SmartParts = null;
            ClientUtility._FileIcons = null;
            listAttachments.Clear();
            webBContent = null;

            if (this.RootWorkItem != null) RootWorkItem.Items.Remove(this);
        }

        private void RegisterEvents()
        {
            ServiceClient.GetClientService<IMainForm>()
                .ApplicationExit += new EventHandler(OnApplicationExit);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
            {
                mainForm.MainFormActivated += new EventHandler(mainForm_Activated);
                mainForm.MainFormDeactivated += new EventHandler(mainForm_Deactivated);
                AddMessageFilter();
                MailListPresenter.SetSplitterPosition(splitContainer, BusinessPartWidthKey, 410);
                BlankNavigate();
                RegisterEvents();
                RootWorkItem.State["BusinessPartPanel"] = this.splitContainer.Panel2;
            }
        }
        private void BlankNavigate()
        {
            this.webBContent.Navigate("about:blank");
        }

        #region 滚动控件
        private ScrollableControlMessageFilter htmlBodyFilter;
        private void AddMessageFilter()
        {
            htmlBodyFilter = new ScrollableControlMessageFilter(this.webBContent);
            System.Windows.Forms.Application.AddMessageFilter(htmlBodyFilter);
        }
        void mainForm_Deactivated(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.RemoveMessageFilter(htmlBodyFilter);
        }

        void mainForm_Activated(object sender, EventArgs e)
        {
            AddMessageFilter();
        }
        #endregion

        #endregion

        #region 方法

        public void OnApplicationExit(object sender, EventArgs e)
        {
            MailListPresenter.SaveSplitterPosition(BusinessPartWidthKey, this.splitContainer.SplitterPosition.ToString());
        }

        #region 设置附件颜色
        private void flpAttachment_LostFocus(object sender, EventArgs e)
        {
            SetAttachmentControlBackColor(Color.White);
            try
            {
                Clipboard.Clear();
            }
            catch (System.Runtime.InteropServices.ExternalException ex)
            {
                Clipboard.Clear();
            }
            catch (System.Threading.ThreadStateException t)
            {
                Clipboard.Clear();
            }
        }

        /// <summary>
        /// 是否选中了所有附件
        /// </summary>
        public bool IsSelectAllAttachments
        {
            get
            {
                bool selectedAll = true;
                int count = this.flpAttachment.Controls.Count;
                if (count < 0)
                {
                    selectedAll = false;
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (this.flpAttachment.Controls[i].BackColor == Color.White)
                        {
                            selectedAll = false;
                            break;
                        }
                    }
                }

                return selectedAll;
            }
        }

        public string[] SelectedAttachments()
        {
            List<string> selectedAttachments = new List<string>();
            int count = this.flpAttachment.Controls.Count;
            for (int i = 0; i < count; i++)
            {
                if (this.flpAttachment.Controls[i].BackColor == Color.LightSteelBlue)
                {
                    AttachmentsControl attachmentsCtl = this.flpAttachment.Controls[i] as AttachmentsControl;
                    selectedAttachments.Add(attachmentsCtl.GetFileFullPath());
                }
            }
            return selectedAttachments.ToArray();
        }


        /// <summary>
        /// 选中所有附件
        /// </summary>
        public void SelectAllAttachmentControls()
        {
            SetAttachmentControlBackColor(Color.LightSteelBlue);
            //get attachment control foucs
            this.flpAttachment.Focus();
        }

        public void RestoreBackColor()
        {
            SetAttachmentControlBackColor(Color.White);
        }

        public void SetAttachmentControlBackColor(Color backColor)
        {
            int count = this.flpAttachment.Controls.Count;
            for (int i = 0; i < count; i++)
            {
                this.flpAttachment.Controls[i].BackColor = backColor;
            }
        }

        #endregion

        #endregion


        #region 设置邮件自定义属性，过滤邮件点击2次
        /// <summary>
        /// 获取邮件UserProperty是否有未读邮件自定义信息
        /// </summary>
        /// <param name="mailItem"></param>
        /// <returns></returns>
        private bool HasUserPropertyArchives(_MailItem mailItem)
        {
            string value = GetUserPropertyValue(mailItem);
            if (value.Equals(MailCenterValue))
            {
                SetUserPropertyValue(mailItem, string.Empty);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 添加邮件自定义信息
        /// </summary>
        /// <param name="mailItem"></param>
        /// <param name="value"></param>
        private void SetUserPropertyValue(_MailItem mailItem, string value)
        {
            UserProperties properties = mailItem.UserProperties;
            UserProperty property = properties.Add(IsMailReadKey, OlUserPropertyType.olText, false, OlUserPropertyType.olText);
            property.Value = value;
            properties = null;
            property = null;
            MailUtility.ReleaseComObject(properties);
        }
        /// <summary>
        /// 获取邮件自定义信息
        /// </summary>
        /// <param name="mailItem"></param>
        /// <returns></returns>
        private string GetUserPropertyValue(_MailItem mailItem)
        {
            string value = string.Empty;
            UserProperties properties = mailItem.UserProperties;
            if (properties != null && properties[IsMailReadKey] != null)
            {
                value = properties[IsMailReadKey].Value.ToString();
            }
            else
            {
                SetUserPropertyValue(mailItem, MailCenterValue);
            }
            MailUtility.ReleaseComObject(properties);
            return value;
        }

        #endregion

        /// <summary>
        /// 根据指定字体计算字符串宽度
        /// </summary>
        /// <param name="str"></param>
        /// <param name="font"></param>
        /// <returns></returns>
        public Int32 GetControlWidth(String arr, Font font)
        {
            SizeF siF = this.CreateGraphics().MeasureString(arr, font);
            return (int)Math.Ceiling(siF.Width);
        }


        #region  动态填充邮件预览信息
        [CommandHandler(MailCenterCommandConstants.Command_CurrentMailItemChanged)]
        public void OnCurrentMailItemChanged(object sender, EventArgs e)
        {
            listAttachments.Clear();
            currentMail = RootWorkItem.State[MailCenterCommandConstants.CurrentSelection] as MailItem;

            bool unRead = false;
            try
            {
                message = GetMessageInfo(ref unRead);//获取邮件信息同时给附件属性赋值并返回邮件的unRead属性
                FillMailInfo();//填充邮件信息
            }

            catch (System.Exception ex)
            {
                if (ex.Message.Contains("已开始传送此邮件") || ex.Message.Contains("找不到您所指定的邮件")
                    || ex.Message.ToLower().Contains("has started sending the mail")
                    || ex.Message.ToLower().Contains("the message you specified cannot be found"))
                {
                }
                else
                {
                    ICP.Framework.CommonLibrary.Logger.Log.Error(CommonHelper.BuildExceptionString(ex));
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                }
            }
            finally
            {
                SetMailReadState(unRead);//设置邮件状态
                //加载业务面板和数据
                if (message != null)
                    this.Commands["ShowBusinessPart"].Execute();

                RootWorkItem.State["CurrentMessage"] = message;
                message = null;
            }
        }
        /// <summary>
        /// 设置邮件的状态（已读，未读）
        /// </summary>
        private void SetMailReadState(bool unRead)
        {
            ClientProperties.IsEnableToolBar = true;
            if (message != null)
            {
                if (unRead && !ClientProperties.IsSynchronizingFolders)
                {
                    if (!CompareEntryID())
                        ToobarPart.ExecuteTimer(false);
                    else
                    {
                        //当用户手动将邮件设置成未读后，需要隔2倍时间去设置邮件成未读
                        //ToobarPart.Stop();
                        ToobarPart.ExecuteTimer(true);
                        ClientProperties.FlagMailItemPropertyChanged = false;
                    }
                }
                else
                {
                    ClientProperties.selected_MailEntryID = message.EntryID;
                    ClientProperties.FlagMailItemPropertyChanged = false;
                    ToobarPart.Stop();
                }
            }
        }
        /// <summary>
        /// 比较EntryID，为了处理邮件菜单设置邮件未读或者已读
        /// </summary>
        /// <returns></returns>
        private bool CompareEntryID()
        {
            if (message == null)
                return true;
            if (ClientProperties.selected_MailEntryID.Equals(message.EntryID))
            {
                //邮件视图列表只有一封未读的邮件，则可以设置成已读
                if (ListPart.axViewCtlEmailList.ItemCount == 1)
                    return false;
                else
                    return true;
            }
            else
            {
                ClientProperties.selected_MailEntryID = message.EntryID;
                return false;
            }
        }
        /// <summary>
        /// 备注:由于所有文件夹都可以将邮件设置成已读，就不需要过滤
        /// </summary>
        /// <returns></returns>
        private bool IsRestartTimer()
        {
            bool start = false;
            try
            {
                MAPIFolder folder = currentMail.Parent as MAPIFolder;
                if (folder != null)
                {
                    if (!MailUtility.DOJFolders.Contains(folder.Name.ToLower()))
                    {
                        start = true;
                    }
                    MailUtility.ReleaseComObject(folder);
                }
            }
            catch
            {
                start = false;
            }

            return start;
        }
        public void FillMailInfo()
        {
            if (this.message == null)
            {
                this.Visible = false;
                return;
            }
            if (!this.Visible)
            {
                this.Visible = true;
            }
            this.DisplayMessageBody(message.Body);//填充邮件信息的主体部份
            this.SetCtlText(message.Subject, message.CreateDate, message.CreatorName, message.SendFrom, message.State);//填充主题、发送人,接收时间等信息
            this.FillRecipentsContainer(message.ToName, message.SendTo);//填充接收人信息
            this.FillCCContainer(message.CCName, message.CC);//填充抄送人等信息
            this.FillAttachmentsContainer();//填充附件
        }

        void SetCtlText(string subject, DateTime sentOn, string senderName, string senderEmailAddress, MessageState state)
        {
            SetTitleHeightAndValue(subject);
            this.lnkSender.Text = GetSenderAddressAsString(senderName, senderEmailAddress);//获取发件人名称和邮件地址的组合
            this.lnkSender.Invalidate(false);
            this.pnlTime.Left = this.lnkSender.Location.X + GetControlWidth(this.lnkSender.Text, this.lnkSender.Font) + 16;
            string receiveTime = sentOn.ToString("yyyy-MM-dd HH:mm");
            if (!receiveTime.Contains("4501-1-1"))
                this.lnkTime.Text = receiveTime;
            else
                this.lnkTime.Text = "None";
            if (state == MessageState.Draft)
                this.txtDescription.Visible = true;
            else
                this.txtDescription.Visible = false;
        }
        /// <summary>
        /// 把需要缓存的属性放入全局变量中
        /// </summary>
        void ApplicationCacheProperties(bool isTranfer)
        {
            //缓存在一个全局的变量下，方便获取
            if (isTranfer)
            {
                ClientProperties.CurrentMailItem = currentMail;
                ClientProperties.Attachments = listAttachments;
                ClientProperties.SelectMail_EntryID = currentMail.EntryID;
            }
            else
            {
                ClientProperties.CurrentMailItem = null;
                ClientProperties.Attachments.Clear();
                ClientProperties.SelectMail_EntryID = null;
            }
            //目的是为了当用户拖拽邮件到文件夹
            RootWorkItem.State["SendFrom"] = ClientProperties.EmailAddress = (message == null ? string.Empty : message.SendFrom);
            RootWorkItem.State["MessageID"] =
                ClientProperties.MessageID = (message == null ? string.Empty : message.MessageId);
            ClientProperties.SenderName = message == null ? string.Empty : message.CreatorName;
        }

        private ICP.Message.ServiceInterface.Message GetMessageInfo(ref bool unRead)
        {
            if (currentMail != null)
            {
                this.Visible = true;
                string htmlBody = MailListPresenter.FixHtmlBody(currentMail.Attachments, currentMail.HTMLBody);
                message = ClientUtility.ConvertMailItemToMessageInfo(currentMail, true, false);
                message.Attachments = listAttachments;
                message.HasAttachment = listAttachments.Count > 0;
                message.Body = htmlBody;
                message.IsMailItem = true;
                ApplicationCacheProperties(true);
                ToobarPart.SetToolsEnable(true);
                unRead = currentMail.UnRead;
            }
            else
            {
                _ReportItem reportItem =
                    RootWorkItem.State[MailCenterCommandConstants.CurrentSelection] as ReportItem;
                if (reportItem != null)
                {
                    this.Visible = true;
                    message = ClientUtility.ConvertReportItemToMessageInfo(reportItem);
                    message.IsMailItem = false;
                    unRead = reportItem.UnRead;
                }
                else
                {
                    message = null;
                    this.Visible = false;
                }
                ToobarPart.SetToolsEnable(false);
                ApplicationCacheProperties(false);
            }
            return message;
        }

        private List<ICP.Message.ServiceInterface.AttachmentContent> GetAttachment(List<string> lstAtts)
        {
            List<ICP.Message.ServiceInterface.AttachmentContent> attachments = new List<ICP.Message.ServiceInterface.AttachmentContent>();
            foreach (string attachmentFullPath in lstAtts)
            {
                ICP.Message.ServiceInterface.AttachmentContent attachment = new ICP.Message.ServiceInterface.AttachmentContent();
                attachment.ClientPath = attachmentFullPath;
                attachment.Name = attachment.DisplayName = Path.GetFileName(attachmentFullPath);
                attachment.Id = Guid.NewGuid();
                attachments.Add(attachment);
                attachment = null;
            }
            return attachments;
        }


        private void DisplayMessageBody(string html)
        {
            webBContent.Navigate("about:blank");

            if (webBContent.Document != null)
            {

                webBContent.Document.Write(string.Empty);

            }

            webBContent.DocumentText = html;

        }
        private void SetTitleHeightAndValue(string subject)
        {
            txtSubject.Text = string.Format(" {0}", subject);
            pnlSubject.Height = txtSubject.FullHeight;
        }

        public string GetSenderAddressAsString(string senderName, string senderEmailAddress)
        {
            if (!string.IsNullOrEmpty(senderName) && !string.IsNullOrEmpty(senderEmailAddress))
            {
                return string.Format("{0}[{1}]", senderName, senderEmailAddress);
            }
            else if (string.IsNullOrEmpty(senderName) && string.IsNullOrEmpty(senderEmailAddress))
            {
                return "[ ]";
            }
            else if (!string.IsNullOrEmpty(senderName) && string.IsNullOrEmpty(senderEmailAddress))
            {
                return senderName;
            }
            else
            {
                return senderEmailAddress;
            }
        }

        /// <summary>
        /// init recipents container
        /// </summary>
        /// <param name="recipients"></param>
        public void FillRecipentsContainer(string nameList, string sendTo)
        {
            //this.pnlRecipients.Controls.Clear();
            this.pnlRecivient.Height = 23; this.pnlRecipients.Height = 23;
            if (string.IsNullOrEmpty(sendTo))
                return;
            int _length = 0;

            int _width = DynamicAddAddressControl(pnlRecipients, nameList, sendTo, (int)OlMailRecipientType.olTo, ref _length);
            MailListPresenter.SetControlHeight(_width + (_length * 14), this.pnlRecipients, this.pnlRecivient);
        }

        private int DynamicAddAddressControl(FlowLayoutPanel pnlAddress, string nameList, string emailAddresses, int recipientType, ref int _length)
        {
            int width = 0;
            string[] arrTo = MailUtility.SplitStringToArray(emailAddresses, new char[1] { ';' });
            string[] arrName = MailUtility.SplitStringToArray(nameList, new char[1] { ';' });
            int length = _length = arrTo.Length;//本封邮件所需要的地址控件数量
            int existsCount = pnlAddress.Controls.Count;//已存在地址控件数量
            List<AddressControl> addressList = new List<AddressControl>();
            pnlAddress.SuspendLayout();
            if (existsCount >= length)//如果已存在的控件数量大于等于要填充的数量，删除多余的控件
            {
                int needDeleteCount = existsCount - length;
                for (int i = 0; i < needDeleteCount; i++)
                {
                    pnlAddress.Controls.RemoveAt(needDeleteCount - i);
                }
            }
            else
            {//添加不足的控件并赋值
                int needAddCount = length - existsCount;
                string[] subArray = arrTo.Skip(existsCount).ToArray();
                string[] subNames = arrName.Skip(existsCount).ToArray();
                String _recipient = string.Empty;
                for (int i = 0; i < needAddCount; i++)
                {
                    _recipient = subArray[i];
                    if (!string.IsNullOrEmpty(subNames[i]))
                    {
                        _recipient = subNames[i];
                    }
                    if (i != length - 1)
                        _recipient = String.Format("{0};", _recipient);

                    AddressControl addressControl = new AddressControl(_recipient, recipientType);
                    width += addressControl.Width;
                    addressList.Add(addressControl);
                }

            }
            //给已存在的控件赋值并显示
            int temp = pnlAddress.Controls.Count;
            string[] replaceArray = arrTo.Take(temp).ToArray();
            string[] replaceNames = arrName.Take(temp).ToArray();
            for (int i = 0; i < temp; i++)
            {
                AddressControl control = pnlAddress.Controls[i] as AddressControl;
                string displayText = replaceArray[i];
                if (!string.IsNullOrEmpty(replaceNames[i]))
                {
                    displayText = replaceNames[i];
                }
                control.ResetInfo(displayText, recipientType);
                width += control.Width;
            }

            pnlAddress.Controls.AddRange(addressList.ToArray());
            pnlAddress.ResumeLayout();
            nameList = null;
            addressList = null;

            return width;
        }

        public void FillCCContainer(string ccNameList, string cc)
        {
            if (string.IsNullOrEmpty(cc))
            {
                this.pnlCC.Visible = false;
                return;
            }
            //clear the Dynamic controls
            //this.flpCC.Controls.Clear();
            this.pnlCC.Visible = true;
            this.pnlCC.Height = 23; this.flpCC.Height = 23;
            int _length = 0;
            int _width = DynamicAddAddressControl(flpCC, ccNameList, cc, (int)OlMailRecipientType.olCC, ref _length);
            MailListPresenter.SetControlHeight(_width + (_length * 14), this.flpCC, this.pnlCC);
        }


        public void FillAttachmentsContainer()
        {
            int attCount = listAttachments.Count;
            if (attCount <= 0)
            {
                this.flpAttachment.Controls.Clear();
                this.pnlAtt.Visible = false;
                return;
            }

            this.pnlAtt.Visible = true;
            //clear the Dynamic controls
            this.flpAttachment.Controls.Clear();//清空FlowLayoutPanel中已存在的控件
            this.pnlAtt.Height = 26; this.flpAttachment.Height = 22;
            int _width = DynamicAddAttachmentControls(flpAttachment, attCount);//动态添加附件控件
            MailListPresenter.SetAttachmentControlHeight(_width + (attCount * 56), this.flpAttachment, this.pnlAtt);
        }
        public int DynamicAddAttachmentControls(FlowLayoutPanel flpAttachment, int attCount)
        {
            int width = 0;

            AttachmentsControl[] attControls = new AttachmentsControl[attCount];
            for (int i = 0; i < attCount; i++)
            {
                AttachmentContent attachmentInfo = listAttachments[i];
                String fileName = attachmentInfo.Name;
                string fileExtension = Path.GetExtension(fileName);
                if (string.IsNullOrEmpty(fileExtension))//附件后缀名为空提示为无法识别的文件
                {
                    MailListPresenter.ShowReminderForm(LocalData.IsEnglish ? "the current select mail attachments failed to identify, choose to right mouse click open the mail please!" : "当前查看的邮件附件内容未能识别,请右键单击打开邮件!", false);
                    continue;
                }
                String fileSizeString = CommonHelper.GetFileSizeString(attachmentInfo.Size);//获取文件大小
                AttachmentsControl attachmentControl = new AttachmentsControl();
                attachmentControl.Init(fileName, fileExtension, fileSizeString, attachmentInfo.DisplayName);//初始化附件控件
                width += attachmentControl.PreferredSize.Width;
                attControls[i] = attachmentControl;//将控件添加到数组
                attachmentControl = null;
            }
            if (attControls.Length > 0)
                this.flpAttachment.Controls.AddRange(attControls);//将控件数组添加到FlowLayoutPanel
            else
                this.pnlAtt.Visible = false;

            attControls = null;

            return width;
        }



        #endregion

        #region 控制界面的SIZE

        private void EmailDetailPart_SizeChanged(object sender, EventArgs e)
        {
            if (this.Width < 81) return;

            pnlRecipients.Width = this.Width - 69;
            flpCC.Width = this.Width - 69;
            flpAttachment.Width = this.Width - 82;
            this.txtSubject.Width = this.Width - 23;
        }

        private bool IsSplitter = false;
        private void OnSplitterPositionChanged(object sender, EventArgs e)
        {
            InnerSplitter();
        }

        [CommandHandler("Command_InternalMailBusinessPartFixedSize")]
        public void Command_InternalMailBusinessPartFixedSize(object sender, EventArgs e)
        {
            InnerSplitter();
        }

        private void InnerSplitter()
        {
            if (IsSplitter)
            {
                IsSplitter = false;
                return;
            }
            //当前查看的是否是内部邮件沟通面板            
            if (MailListPresenter.IsVisitInternalMailBusinessPart())
            {
                int height = splitContainer.SplitterPosition;
                if (height < 250)
                {
                    splitContainer.SplitterPosition = 250;
                }
                //if (this.ParentForm.Height - height < 332)
                //{
                //    IsSplitter = true;
                //    //如果邮件中心调整高度少于432 时， 就不需要去固定
                //    if ((this.ParentForm.Height - 332) > 20)
                //    {
                //        if (splitContainer.Panel1.Width == splitContainer.Panel2.Width)
                //            splitContainer.SplitterPosition = this.ParentForm.Height - 333;
                //        else
                //            splitContainer.SplitterPosition = this.ParentForm.Height - 332;
                //    }
                //}
            }
        }
        /// <summary>
        /// 防止按F5键邮件正文消失
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webBContent_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.F5)
            {
                e.IsInputKey = true;
            }
        }

        #endregion


    }
}
