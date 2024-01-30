using ICP.FileSystem.ServiceInterface;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Server;
using ICP.Message.ServiceInterface;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using MailBee;
using MailBee.Mime;
using MailBee.Outlook;
using MailBee.Pop3Mail;
using MailBee.SmtpMail;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WindowsTimer = System.Windows.Forms.Timer;

namespace ICP.Message.ServiceComponent
{
    public class MailBeeService : IMailBeeService
    {
        #region 字段、属性、服务
        static string emailHost;
        /// <summary>
        /// 
        /// </summary>
        public MailMessage MailMessage = null;
        /// <summary>
        /// 
        /// </summary>
        public MsgConvert MConvert = null;
        /// <summary>
        /// bool：false表示还没有配置公司邮件地址，true表示已配置好公司邮件地址,
        /// 下次隔五分钟接收邮件时，就需要重新获取公司邮件配置信息
        /// </summary>
        SortedList CompleteUserCompanyList = null;
        /// <summary>
        /// 当前用户的公司列表
        /// </summary>        
        public List<ConfigureObjects> _UserCompanyList { get; set; }
        public Guid UserId
        {
            get { return ApplicationContext.Current.UserId; }
        }
        public bool IsEnglish
        {
            get { return ApplicationContext.Current.IsEnglish; }
        }

        public Guid _DefaultCompanyID;
        public Guid DefaultCompanyID
        {
            get
            {

                if (ApplicationContext.Current.DefaultCompanyId == Guid.Empty)
                {
                    return _DefaultCompanyID;
                }
                else
                {
                    return ApplicationContext.Current.DefaultCompanyId;
                }
            }
        }

        public IFaxService _FaxService { get; set; }
        public IFaxMessageService faxMessageService { get; set; }
        public IUserService UserSerivce { get; set; }
        public IFrameworkInitializeService InitializeService { get; set; }
        public IMessageService MessageService { get; set; }
        #endregion

        #region 构造函数
        public MailBeeService(IFrameworkInitializeService initializeService, IUserService userService,
            IFaxMessageService _faxMessageService)
        {
            InitializeService = initializeService;
            UserSerivce = userService;
            faxMessageService = _faxMessageService;
        } 
        #endregion

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public SingleResult Send(ServiceInterface.Message message)
        {

            string mailMessage = "Send:{0}<br />Host:{1}<br />Login:{2}<br />Subject:{3}<br />";
            string sender = string.Empty;
            string bodyHTML = string.Empty;
            try
            {
                if (message.Type == MessageType.Fax)
                {
                    //干掉PDF进程，会先判断进程是否存在。
                    ClientHelper.KillProcess("AcroRd32");
                }
                //邮箱地址
                IAddressProvider provider = AddressProviderFactory.GetProvider(message.Type);
                string messageId = "systemmail" + Guid.NewGuid();
                provider.MessageId = !string.IsNullOrEmpty(message.MessageId) ? message.MessageId : messageId;
                provider.EmailHost = GetServer();
                //统一使用系统管理员(ICPSystem@cityocean.com)发送
                UserMailAccountList account = GetUserDefaultMailAccount(new Guid("4047CFAD-ECC8-E111-9D0D-0026551CA87B"));
                sender = message.SendFrom;
                #region Mail Message
                MailMessage mail = new MailMessage
                {
                    From = new EmailAddress(account.Email),
                    To = { AsString = provider.GetAddress(message.SendTo) },
                    Subject = message.Subject,
                    Builder = { SetMessageIDOnSend = false }
                };
                //非空、非系统邮箱将同时发送到发件人
                if (!string.IsNullOrEmpty(sender) && !sender.ToLower().Contains("icpsystem"))
                {
                    mail.To.AddFromString(sender);
                }
                if (message.Type != MessageType.Email)
                {
                    messageId = messageId.Replace("mail", "notmail");
                    mail.MessageID = messageId;
                }
                else
                {
                    if (!string.IsNullOrEmpty(messageId) && messageId.Contains("@"))
                    {
                        if (!messageId.StartsWith("<") && !messageId.EndsWith(">"))
                        {
                            messageId = "<" + messageId + ">";

                        }
                        mail.MessageID = messageId;
                    }
                    else
                    {
                        string hostName = "@cityocean.com";
                        int index = message.SendFrom.IndexOf("@", StringComparison.Ordinal);
                        if (index >= 0)
                        {
                            hostName = message.SendFrom.Substring(index);
                        }
                        messageId = "<" + messageId + hostName + ">";
                        mail.MessageID = messageId;
                    }
                }
                if (!string.IsNullOrEmpty(message.CC))
                {
                    mail.Cc.AsString = provider.GetAddress(message.CC);
                }
                if (message.BodyFormat == BodyFormat.olFormatPlain)
                {
                    mail.BodyPlainText = bodyHTML = message.Body;
                }
                else
                {
                    mail.BodyHtmlText = bodyHTML = message.Body;
                }

                mail.Charset = "UTF-8";
                foreach (AttachmentContent content in message.Attachments)
                {
                    mail.Attachments.Add(content.Content, content.Name, null, null, null, NewAttachmentOptions.None, MailTransferEncoding.Base64);
                }
                #endregion
                Smtp smtp = new Smtp();
                mailMessage = string.Format(mailMessage, account.Email, account.MailOutgoingHost, account.MailOutgoingLogin, mail.Subject);

                SmtpServer server = new SmtpServer(account.MailOutgoingHost, account.MailOutgoingLogin, account.MailOutgoingPassword)
                {
                    AuthMethods = AuthenticationMethods.SaslLogin |
                                  AuthenticationMethods.SaslPlain | AuthenticationMethods.SaslNtlm,
                    AuthOptions = AuthenticationOptions.PreferSimpleMethods,
                    Port = account.MailOutgoingPort
                };

                smtp.SmtpServers.Add(server);
                smtp.Message = mail;
                smtp.ThrowExceptions = true;
                smtp.Send();

                SingleResult result = new SingleResult();
                result.Add("MessageID", messageId);
                result.Add("SendFrom", message.SendFrom);
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog("MailBeeSend", mailMessage+ex.Message + ex.StackTrace);
                mailMessage += "Body:" + bodyHTML;
                NoticeMailSendFaild(ex.Message, mailMessage, sender);
                throw new ApplicationException(ex.Message);
            }

            #region  传真大厅发送传真代码，未实施起来，先暂时屏蔽。（适合发送邮件和传真）
            //MailMessage mail = new MailMessage();
            //IAddressProvider provider = AddressProviderFactory.GetProvider(message.Type);
            //Guid messageId = Guid.NewGuid();
            //provider.MessageId = !string.IsNullOrEmpty(message.MessageId) ? message.MessageId : messageId.ToString();
            //mail.To.AsString = MailToAddress(provider, message.Type, message.SendTo);
            //mail.Subject = message.Subject;
            //mail.MessageID = messageId.ToString();
            //mail.Priority = MessageUtility.ConvertMessagePriorityToMailPriority(message.Priority);
            //if (!string.IsNullOrEmpty(message.CC))
            //{
            //    mail.Cc.AsString = provider.GetAddress(message.CC);
            //}
            //if (message.BodyFormat == BodyFormat.olFormatPlain)
            //{
            //    mail.BodyPlainText = message.Body;
            //}
            //else
            //    mail.BodyHtmlText = message.Body;

            //mail.Charset = "utf-8";

            //// mail.MailTransferEncodingPlain = MailTransferEncoding.QuotedPrintable;

            //// Set the Quoted-Printable encoding for HTML-formatted body.
            //// mail.MailTransferEncodingHtml = MailTransferEncoding.QuotedPrintable;


            //foreach (AttachmentContent content in message.Attachments)
            //{
            //    mail.Attachments.Add(content.Content, content.Name, null, null, null, NewAttachmentOptions.None, MailTransferEncoding.Base64);
            //}
            //try
            //{
            //    Smtp smtp = new Smtp();

            //    Guid userID = this.UserId;
            //    if (userID == Guid.Empty && message.CreateBy != Guid.Empty)
            //    {
            //        userID = message.CreateBy;
            //    }

            //    string sendFrom = string.Empty;
            //    if (message.Type == MessageType.Fax)
            //    {
            //        ConfigureObjects info = _FaxService.GetConfigureInfoByCompanyID(this.DefaultCompanyID);
            //        if (info != null && !string.IsNullOrEmpty(info.Email) && !string.IsNullOrEmpty(info.EmailAddress))
            //        {
            //            if (string.IsNullOrEmpty(info.TaxNo))
            //            {
            //                throw new Exception(IsEnglish ? "You should setup company TaxNO first." : "请先配置公司的传真号.");
            //            }
            //            else
            //            {
            //                smtp.SmtpServers.Add(info.EmailHost, info.Email, info.EmailPassWord);
            //                mail.From = new EmailAddress(info.EmailAddress);
            //                sendFrom = info.TaxNo;
            //            }
            //        }
            //        else
            //        {
            //            throw new Exception(IsEnglish ? "You should setup company email first." : "请先配置公司的邮件帐号.");
            //        }
            //    }
            //    else
            //    {
            //        UserMailAccountList account = GetUserDefaultMailAccount(userID);
            //        smtp.SmtpServers.Add(account.MailOutgoingHost);
            //        mail.From = new EmailAddress(message.SendFrom);
            //        sendFrom = message.SendFrom;
            //    }

            //    smtp.Message = mail;

            //    smtp.ThrowExceptions = true;
            //    smtp.Send();
            //    smtp.Dispose();

            //    result.Add("MessageID", messageId);
            //    result.Add("SendFrom", sendFrom);
            //    return result;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

            #endregion
        }

        

        private string FixReceivent(string sendTo)
        {
            string _sendTo = string.Empty;
            List<ConfigureObjects> list = _FaxService.GetConfigureInfoByEmail(sendTo, true);
            if (list.Count > 0)
            {
                if (list.Count == 1)
                {
                    _sendTo = list[0].EmailAddress;
                }
                else
                {
                    throw new Exception(LocalData.IsEnglish ? string.Format("Exsits the {0} same tax no.", list.Count)
                        : string.Format("存在{0}相同的传真号.", list.Count));
                }
            }
            else
            {
                throw new Exception(IsEnglish ? "not exsit the fax no." : "不存在该传真号.");
            }

            return _sendTo;
        }


        #region Receive
        
        

        public void Transfer(List<ConfigureObjects> userCompanyList, Guid defaultCompanyID)
        {
            _DefaultCompanyID = defaultCompanyID;
            _UserCompanyList = userCompanyList;
            CompleteUserCompanyList = new SortedList(new SortComparer());
            MessageUtility.faxService = _FaxService;

            Received();
            //在点击接收时执行一次，然后使用timer控件定时去下载
            InitComponent();
        }

        private void Received()
        {
            if (CompleteUserCompanyList.Count == 0)
            {
                foreach (ConfigureObjects info in _UserCompanyList)
                {
                    if (info != null && info.CompanyID != Guid.Empty && info.Type == LocalOrganizationType.Company)
                    {
                        ConfigureObjects completeConfigureObjects = _FaxService.GetConfigureInfoByCompanyID(info.CompanyID);
                        if (completeConfigureObjects == null)
                            continue;
                        if (string.IsNullOrEmpty(completeConfigureObjects.Email) || string.IsNullOrEmpty(completeConfigureObjects.EmailHost))
                        {
                            CompleteUserCompanyList.Add(false, completeConfigureObjects);
                            continue;
                        }
                        CompleteUserCompanyList.Add(true, completeConfigureObjects);
                        ReceiveMessages(completeConfigureObjects);
                    }
                }
            }
            else
            {
                for (int i = 0; i < CompleteUserCompanyList.Count; i++)
                {
                    ConfigureObjects value = CompleteUserCompanyList.GetByIndex(i) as ConfigureObjects;
                    if (bool.Parse((CompleteUserCompanyList.GetKey(i).ToString())) == false)
                    {
                        ConfigureObjects completeConfigureObjects = _FaxService.GetConfigureInfoByCompanyID(value.CompanyID);
                        CompleteUserCompanyList.RemoveAt(i);
                        if (string.IsNullOrEmpty(completeConfigureObjects.Email) || string.IsNullOrEmpty(completeConfigureObjects.EmailHost))
                        {
                            CompleteUserCompanyList.Add(false, completeConfigureObjects);
                            continue;
                        }
                        else
                        {
                            CompleteUserCompanyList.Add(true, completeConfigureObjects);
                        }

                        ReceiveMessages(completeConfigureObjects);
                    }
                    else
                    {
                        ReceiveMessages(value);
                    }
                }
            }
        }
        private void OnReceived(object sender, EventArgs e)
        {
            Received();
        }

        WindowsTimer timer;
        private void InitComponent()
        {
            if (timer != null)
            {
                timer.Tick -= new EventHandler(OnReceived);
                timer.Stop();
                timer.Dispose();
                timer = null;
            }

            timer = new WindowsTimer();
            timer.Interval = 10000;//300000;
            timer.Tick += new EventHandler(OnReceived);
            timer.Start();
        }

        private void ReceiveMessages(ConfigureObjects info)
        {
            try
            {
                Pop3 pop = new Pop3();
                pop.Connect(info.EmailHost);
                pop.Login(info.Email, info.EmailPassWord);
                MailMessageCollection msgs = pop.DownloadEntireMessages();
                if (msgs.Count > 0)
                {
                    foreach (MailMessage msg in msgs)
                    {
                        if (msg.Subject.Equals("Returned mail: response error") && msg.RawHeader.Contains("response error"))
                            continue;
                        //过滤登录邮件服务器间，收到发送过来的邮件
                        //if (Regex.IsMatch(msg.To.AsString, @"^[0-9]]+$"))
                        //    continue;
                        FaxMessageObjects message = Utility.ConvertMailMessageToFaxMessage(msg, info.CompanyID, info.CompanyName);
                        faxMessageService.SaveFaxMessage(message);
                    }
                    pop.DeleteMessages();
                }
                pop.Disconnect();
                pop.Dispose();
                #region Invalidate
                //Imap imp = new Imap();
                //imp.Connect(account.MailIncomingHost);
                //imp.Login("hylunhe", account.MailIncomingPassword);
                //imp.SelectFolder("Inbox");
                //UidCollection uids = (UidCollection)imp.Search(true, "NEW", null);
                //if (uids.Count > 0)
                //{
                //    MailMessageCollection msgs = imp.DownloadEntireMessages(uids.ToString(), true);
                //    foreach (MailMessage msg in msgs)
                //    {
                //        Message.ServiceInterface.Message message = MessageUtility.ConvertMailMessageToMessage(msg);
                //        message.Type = MessageType.Fax;
                //        message.Flag = MessageFlag.UnRead;
                //        message.Way = MessageWay.Receive;
                //        mailList.Add(message);
                //    }
                //    imp.DeleteMessages(uids.ToString(), true);
                //}
                //imp.Disconnect();
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        private UserMailAccountList GetUserDefaultMailAccount(Guid fromUserID)
        {
            List<UserMailAccountList> accounts = UserSerivce.GetUserMailAccountList(new Guid[] { fromUserID });
            if (accounts.Count == 0 || string.IsNullOrEmpty(accounts[0].Email))
            {
                throw new Exception(IsEnglish ? "You should setup email account first." : "请先配置自己的邮件帐号.");
            }

            ////获取当前用户默认邮件帐号,发送邮件服务器
            UserMailAccountList defaultMailAcc = accounts.FirstOrDefault(a => a.IsDefault) ?? accounts[0];
            return defaultMailAcc;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetServer()
        {
            if (!string.IsNullOrEmpty(emailHost))
                return emailHost;
            emailHost = InitializeService.GetSystemConfigurationInfo("EmailHost").Value;
            return emailHost;
        }
        /// <summary>
        /// 传入messageID得到附件流另存msg文件后打开
        /// </summary>
        /// <param name="messageId">消息ID</param>
        public string SaveAstMsgFile(string messageId)
        {
            string targetPath = string.Empty;
            List<ContentInfo> listContentInfo = MessageService.GetMailInfo(messageId);
            if (listContentInfo.Any())
            {
                foreach (var c in listContentInfo)
                {
                    //判断文件类型,msg文件直接写入磁盘
                    //另存路径
                    targetPath = Path.GetTempPath() + messageId + ".msg";
                    if (c.FileType.Equals("msg"))
                    {
                        MemoryStream m = new MemoryStream(c.Content);
                        FileStream fs = new FileStream(targetPath, FileMode.OpenOrCreate);
                        m.WriteTo(fs);
                        m.Close();
                        fs.Close();
                    }
                    else //非MSG文件则通过转换
                    {
                        if (MailMessage == null)
                        {
                            MailMessage = new MailMessage();
                        }
                        MailMessage.LoadMessage(c.Content);
                        if (MConvert == null)
                        {
                            MConvert = new MsgConvert();
                        }
                        MConvert.MailMessageToMsg(MailMessage, targetPath);
                    }
                }
            }
            return targetPath;
        }
        /// <summary>
        /// 在联单邮件发送失败后通过icpsystem@cityocean.com发送通知邮件给发送人和IT
        /// </summary>
        /// <param name="errorInfo"></param>
        /// <param name="originalMail"></param>
        /// <param name="noticeEmail"></param>
        void NoticeMailSendFaild(string errorInfo,string originalMail,string noticeEmail)
        {
            
            try
            {
                if (noticeEmail.IsNullOrEmpty()) return;
                //System Mail
                UserMailAccountList account = GetUserDefaultMailAccount(new Guid("4047CFAD-ECC8-E111-9D0D-0026551CA87B"));
                if (noticeEmail.Equals(account.Email)) return;

                SmtpServer server = new SmtpServer(account.MailOutgoingHost, account.MailOutgoingLogin, account.MailOutgoingPassword)
                {
                    AuthMethods = AuthenticationMethods.SaslLogin |
                                  AuthenticationMethods.SaslPlain | AuthenticationMethods.SaslNtlm,
                    AuthOptions = AuthenticationOptions.PreferSimpleMethods,
                    Port = account.MailOutgoingPort
                };

                Smtp oMailer = new Smtp();
                oMailer.SmtpServers.Add(server);
                oMailer.From.AsString = account.MailOutgoingLogin;
                oMailer.To.AsString = noticeEmail;
                oMailer.Cc.AsString = "taylorzhou@cityocean.com";
                oMailer.Charset = "UTF-8";
                oMailer.Subject = "Failed to send mail";
                StringBuilder bodyHtml = new StringBuilder();
                bodyHtml.Append("Dear ICP User:<br />");
                if (ApplicationContext.Current.IsEnglish)
                {
                    bodyHtml.Append("<p>Failed to send mail;Mailbox configuration error;Please configure your email as follows:</p><br />");
                    bodyHtml.Append( "<p>Click the login account name in the upper right corner of ICP, and switch to [Mailbox Password] in the pop-up [Personal Data]. Enter your email password and save it.</p><br />");
                    bodyHtml.Append("<p>After saving the password, if you continue to receive this email; please contact Peter at duan@cityocean.net to retrieve your password</p><br />");
                }
                else
                {
                    bodyHtml.Append("<p>邮件发送失败;邮箱配置错误;请按以下方式配置邮箱：</p><br />");
                    bodyHtml.Append("<p>点击ICP右上角登录帐号名，在弹出的[个人资料]中切换到[邮箱密码]，录入邮箱密码后保存</p><br />");
                    bodyHtml.Append("<p>保存密码后，如果继续收到此邮件；请联系胡凯 hk@cityocean.com 找回密码</p><br />");
                }
                bodyHtml.Append(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>><br />");
                bodyHtml.AppendFormat("<p>Exception:{0}</p><br />", errorInfo);
                bodyHtml.AppendFormat("<p>MainInfo:<br />{0}</p>", originalMail);

                oMailer.Message.BodyHtmlText = bodyHtml.ToString();
                oMailer.Send();
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog("NoticeMailSendFaild",ex.Message);
            }
        }

        private string MailToAddress(IAddressProvider provider, MessageType type, string sendTo)
        {
            string _SendTo = string.Empty;
            if (type == MessageType.Fax)
            {
                if (sendTo.Contains(";"))
                {
                    string[] arrTaxNos = sendTo.Split(new char[1] { ';' });
                    if (arrTaxNos != null && arrTaxNos.Length > 0)
                    {
                        StringBuilder strEmailHost = new StringBuilder();
                        StringBuilder strEmailTo = new StringBuilder();
                        foreach (string str in arrTaxNos)
                        {
                            if (!string.IsNullOrEmpty(str))
                            {
                                string emailHost = FixReceivent(str);
                                provider.EmailHost = emailHost;
                                strEmailTo.Append(provider.GetAddress(str) + ";");
                                strEmailHost.Append(emailHost + ";");
                            }
                        }

                        provider.EmailHost = strEmailHost.ToString().TrimEnd(new char[1] { ';' });
                        _SendTo = strEmailTo.ToString().TrimEnd(new char[1] { ';' });
                    }
                }
                else
                {
                    provider.EmailHost = FixReceivent(sendTo);
                    _SendTo = provider.GetAddress(sendTo);
                }
            }
            else
            {
                provider.EmailHost = GetServer();
                _SendTo = provider.GetAddress(sendTo);
            }

            return _SendTo;
        }

    }
    public class AddressProviderFactory
    {
        static Dictionary<MessageType, IAddressProvider> providers = new Dictionary<MessageType, IAddressProvider>();
        public static IAddressProvider GetProvider(MessageType type)
        {
            if (providers.ContainsKey(type))
            {
                return providers[type];
            }
            else
            {
                IAddressProvider provider = null;
                if (type == MessageType.Email)
                {
                    provider = new EmailAddressProvider();
                }
                else if (type == MessageType.Fax)
                {
                    provider = new FaxAddressProvider();
                }
                providers.Add(type, provider);
                return providers[type];
            }

        }
    }
    public interface IAddressProvider
    {
        string GetAddress(string originalAddress);
        string MessageId { get; set; }
        string EmailHost { get; set; }
    }

    public class FaxAddressProvider : IAddressProvider
    {
        private string emailHost;
        private string messageId;

        #region IAddressProvider 成员

        public string GetAddress(string originalAddress)
        {
            string[] addresses = originalAddress.Split(';');
            List<string> listAddress = new List<string>();
            foreach (string address in addresses)
            {
                listAddress.Add(FormatReceiverAddress(address));
            }
            return listAddress.Aggregate((a, b) => a + ";" + b);
        }
        private string FormatReceiverAddress(string emailAddress)
        {
            return string.Format("{0}.{1}<{2}>", messageId, emailAddress, emailHost);
        }

        public string MessageId
        {
            get
            {

                return messageId;
            }
            set
            {
                messageId = value;
            }
        }

        public string EmailHost
        {
            get
            {
                return emailHost;
            }
            set
            {
                emailHost = value;
            }
        }

        #endregion
    }
    public class EmailAddressProvider : IAddressProvider
    {

        #region IAddressProvider 成员

        public string GetAddress(string originalAddress)
        {
            return originalAddress;
        }

        public string MessageId
        {
            get;
            set;
        }

        public string EmailHost
        {
            get;
            set;
        }

        #endregion
    }

}
