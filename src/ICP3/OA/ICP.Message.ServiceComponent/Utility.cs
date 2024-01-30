#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/7/19 星期四 15:34:53
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using ICP.Framework.CommonLibrary.Client;
using System.IO;
using ICP.Message.ServiceInterface;
using MailBee.Mime;

namespace ICP.Message.ServiceComponent
{
    public class Utility
    {
        public static IFaxService faxService { get; set; }

        public static FaxMessageObjects ConvertMailMessageToFaxMessage(MailMessage mailMessage, Guid companyID, string companyName)
        {
            FaxMessageObjects message = new FaxMessageObjects();
            message.MessageId = mailMessage.MessageID;
            message.Priority = ConvertMailPriorityToMessagePriority(mailMessage.Priority);
            message.CompanyID = companyID;
            message.State = MessageState.Success;
            if (!string.IsNullOrEmpty(mailMessage.From.Email))
            {
                List<ConfigureObjects> list = faxService.GetConfigureInfoByEmail(mailMessage.From.Email, false);
                if (list.Count > 0)
                {
                    if (list.Count == 1 && !string.IsNullOrEmpty(list[0].TaxNo))
                    {
                        message.SendFrom = list[0].TaxNo;
                    }
                    else
                    {
                        throw new Exception(LocalData.IsEnglish ? string.Format("{0} email address same as {1}.", list.Count, companyName)
                            : string.Format("{0}个邮件地址与{1}相同.", list.Count, companyName));
                    }
                }
                else
                {
                    ShowErrorMessage();
                }
            }
            else
            {
                ShowErrorMessage();
            }

            if (mailMessage.To != null)
            {
                string[] arrSendTo = mailMessage.To.AsString.Split(new char[1] { '\"' });
                if (arrSendTo != null && arrSendTo.Length > 1)
                {
                    StringBuilder strBuf = new StringBuilder();
                    foreach (string str in arrSendTo)
                    {
                        if (str.Contains("<"))
                        { continue; }

                        string[] arrText = str.Split('.');
                        if (arrText != null && arrText.Length > 1)
                            strBuf.Append(arrText[1] + ";");
                    }
                    message.SendTo = strBuf.ToString().TrimEnd(new char[1] { ';' });
                }
                else
                {
                    message.SendTo = mailMessage.To.AsString;
                }
            }
            message.Subject = mailMessage.Subject;
            message.Size = mailMessage.Size;
            message.Body = mailMessage.BodyHtmlText;
            if (mailMessage.Cc != null)
            {
                message.CC = mailMessage.Cc.AsString;
            }
            message.HasAttachment = mailMessage.Attachments.Count > 0;
            if (mailMessage.Attachments.Count > 0)
            {
                string tempPath = Path.GetTempPath();
                message.Attachments = new List<AttachmentContent>(mailMessage.Attachments.Count);
                foreach (Attachment att in mailMessage.Attachments)
                {
                    string clientPath = Path.Combine(tempPath, att.Filename);
                    att.Save(clientPath, true);
                    message.Attachments.Add(new AttachmentContent() { ClientPath = clientPath, DisplayName = att.Filename, Name = att.Name, Size = att.Size, Content = att.GetData() });
                }
            }

            message.Type = MessageType.Fax;
            message.Way = MessageWay.Receive;
            message.BodyFormat = BodyFormat.olFormatHTML;
            message.ReceiveFaxID = Guid.NewGuid();
            message.Flag = MessageFlag.UnRead;

            return message;
        }

        public static MessagePriority ConvertMailPriorityToMessagePriority(MailPriority mailPriority)
        {
            MessagePriority msgPriority = MessagePriority.Normal;
            switch (mailPriority)
            {
                case MailPriority.None:
                case MailPriority.Normal:
                    msgPriority = MessagePriority.Normal;
                    break;
                case MailPriority.Lowest:
                case MailPriority.Low:
                    msgPriority = MessagePriority.Low;
                    break;
                case MailPriority.Highest:
                case MailPriority.High:
                    msgPriority = MessagePriority.High;
                    break;
            }
            return msgPriority;
        }

        public static MailPriority ConvertMessagePriorityToMailPriority(MessagePriority msgPriority)
        {
            MailPriority mailPriority = MailPriority.Normal;
            if (msgPriority == MessagePriority.High)
            {
                mailPriority = MailPriority.High;
            }
            else if (msgPriority == MessagePriority.Low)
            {
                mailPriority = MailPriority.Low;
            }
            else
            {
                mailPriority = MailPriority.Normal;
            }

            return mailPriority;
        }

        static void ShowErrorMessage()
        {
            throw new Exception(LocalData.IsEnglish ? "The Sender is unknown." : "发件人不明确!");
        }
    }
}
