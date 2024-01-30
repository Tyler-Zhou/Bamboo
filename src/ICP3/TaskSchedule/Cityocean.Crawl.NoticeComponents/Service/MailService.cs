#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/4/6 18:02:03
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
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Cityocean.Crawl.CommonLibrary;
using Cityocean.Crawl.LogComponents;

namespace Cityocean.Crawl.NoticeComponents
{
    /// <summary>
    /// 邮件服务
    /// </summary>
    public sealed class MailService : IMailService
    {
        #region Property
        /// <summary>
        /// Mail UserName
        /// </summary>
        string _MailUserName
        {
            get
            {
                string strTemp = INIHelper.Instance.IniReadValue(CommonConstants.MODULENAME_SERVICECONFIG, CommonConstants.CONFIG_MAILUSERNAME);
                if (strTemp.IsNullOrEmpty())
                {
                    throw new Exception(string.Format("未找到邮件帐号配置"));
                }
                return strTemp;
            }
        }
        /// <summary>
        /// Mail Password
        /// </summary>
        string _MailPassword
        {
            get
            {
                string strTemp = INIHelper.Instance.IniReadValue(CommonConstants.MODULENAME_SERVICECONFIG, CommonConstants.CONFIG_MAILPASSWORD);
                if (strTemp.IsNullOrEmpty())
                {
                    throw new Exception(string.Format("未找到邮件密码配置"));
                }
                return strTemp;
            }
        }
        /// <summary>
        /// Smtp Server
        /// </summary>
        string _SmtpServer
        {
            get
            {
                string strTemp = INIHelper.Instance.IniReadValue(CommonConstants.MODULENAME_SERVICECONFIG, CommonConstants.CONFIG_SMTPSERVER);
                if (strTemp.IsNullOrEmpty())
                {
                    throw new Exception(string.Format("未找到邮件Smtp配置"));
                }
                return strTemp;
            }
        }
        /// <summary>
        /// Mail From
        /// </summary>
        string _MailFrom
        {
            get
            {
                string strTemp = INIHelper.Instance.IniReadValue(CommonConstants.MODULENAME_SERVICECONFIG, CommonConstants.CONFIG_MAILFROM);
                if (strTemp.IsNullOrEmpty())
                {
                    throw new Exception(string.Format("未找到系统发件人配置"));
                }
                return strTemp;
            }
        }
        /// <summary>
        /// Mail To
        /// </summary>
        string _MailTo
        {
            get
            {
                string strTemp = INIHelper.Instance.IniReadValue(CommonConstants.MODULENAME_SERVICECONFIG, CommonConstants.CONFIG_MAILTO);
                if (strTemp.IsNullOrEmpty())
                {
                    throw new Exception(string.Format("未找到接收异常的邮件地址配置"));
                }
                return strTemp;
            }
        }
        /// <summary>
        /// Mail CC
        /// </summary>
        string _MailCC
        {
            get
            {
                return INIHelper.Instance.IniReadValue(CommonConstants.MODULENAME_SERVICECONFIG, CommonConstants.CONFIG_MAILCC);
            }
        }
        #endregion

        #region Method

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="reportInfo">报告信息</param>
        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        public void SendEMail(EReportInfo reportInfo)
        {
            try
            {
                EMessageInfo messageInfo = new EMessageInfo { MOwerJob = reportInfo.Name, MContent = reportInfo.Context, AttachmentPath = reportInfo.AttachmentPaths };
                Task.Factory.StartNew(() => SendEMail(messageInfo));
            }
            catch (Exception ex)
            {
                LogService.Error("MailCenter", "发送邮件", ex);
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="paramOwerJob">所属任务</param>
        /// <param name="paramMessageContent">消息内容</param>
        /// <param name="paramException">异常信息</param>
        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        public void SendEMail(string paramOwerJob, string paramMessageContent, Exception paramException)
        {
            try
            {
                EMessageInfo messageInfo = new EMessageInfo { MOwerJob = paramOwerJob, MContent = paramMessageContent, AttachmentPath = new[] { "" }, };
                if (paramException!=null)
                    messageInfo.MContent += string.Format("<br />Message:{0}<br />StackTrace:{1}", paramException.Message, paramException.StackTrace);
                Task.Factory.StartNew(() => SendEMail(messageInfo));
            }
            catch (Exception ex)
            {
                LogService.Write(ex);
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="messageBody"></param>
        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        void SendEMail(EMessageInfo messageBody)
        {
            try
            {
                if (messageBody == null) return;
                SendMessage(messageBody.MOwerJob, messageBody.MContent, messageBody.AttachmentPath);
            }
            catch (Exception ex)
            {
                LogService.Error("MailCenter","Send Mail",ex);
            }
        }

        /// <summary> 
        /// 发送邮件功能 
        /// </summary> 
        /// <param name="paramMailTitle">邮件标题</param> 
        /// <param name="paramMailContent">邮件内容</param> 
        /// <param name="paramAttachmentPath">附件路径</param>
        void SendMessage(string paramMailTitle, string paramMailContent, ICollection<string> paramAttachmentPath)
        {
            SmtpClient smtp = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = false,
                Host = _SmtpServer,
                Credentials = new NetworkCredential(_MailUserName, _MailPassword)
            }; //实例化一个SmtpClient 
            MailMessage mm = new MailMessage
            {
                Priority = MailPriority.Normal,
                From = new MailAddress(_MailFrom, "System", Encoding.GetEncoding(936)),
            }; //实例化一个邮件类 
            foreach (string mailTo in _MailTo.Split(',').Where(mailTo => !mailTo.IsNullOrEmpty()))
            {
                mm.To.Add(mailTo);
            }
            foreach (string mailCC in _MailCC.Split(',').Where(mailCC => !mailCC.IsNullOrEmpty()))
            {
                mm.CC.Add(mailCC);
            }
            mm.Subject = paramMailTitle; //邮件标题 
            mm.SubjectEncoding = Encoding.GetEncoding(936);
            mm.IsBodyHtml = true; //邮件正文是否是HTML格式mm.BodyEncoding = Encoding.GetEncoding(936); 
            mm.Body = paramMailContent;
            if (paramAttachmentPath != null && paramAttachmentPath.Count > 0)
                foreach (string filePath in paramAttachmentPath.Where(attachmentName => !attachmentName.IsNullOrEmpty()))
                {
                    mm.Attachments.Add(new Attachment(filePath));
                }
            smtp.Send(mm);
        }
        #endregion
    }
}
