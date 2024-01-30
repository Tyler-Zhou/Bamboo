using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
namespace ICP.EDI.ServiceComponent
{
    class EMailHelper
    {
        public static void SendMail(string mailServerAddress, string username, string pswd,int port, string sender, string to, string cc, string bcc, string subject, string body, byte[][] attatchments, string[] attachFileNames, string[] attachTypes)
        {
            SmtpClient client = new SmtpClient(mailServerAddress)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(username, pswd),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
                Port = port,
            };
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            MailMessage message = new MailMessage
            {
                Subject = subject, 
                Body = body ,
                SubjectEncoding = Encoding.GetEncoding("UTF-8") ,
                BodyEncoding = Encoding.GetEncoding("UTF-8"),
            };
            //收件人
            string[] toArr = to.Split(new char[] { ';', ',' });
            foreach (string toAddress in toArr)
            {
                message.To.Add(new MailAddress(toAddress));
            }
            //抄送到
            if (string.IsNullOrEmpty(cc) == false)
            {
                string[] ccArr = cc.Split(new char[] { ';', ',' });
                foreach (string ccAddress in ccArr)
                {
                    message.CC.Add(new MailAddress(ccAddress));
                }
            }
            if (string.IsNullOrEmpty(bcc) == false)
            {
                string[] bccArr = bcc.Split(new char[] { ';', ',' });
                foreach (string bccAddress in bccArr)
                {
                    message.Bcc.Add(new MailAddress(bccAddress));
                }
            }

            if (attatchments.Length > 0)
            {
                for (int i = 0; i < attatchments.Length; i++)
                {
                    Attachment attatchment = new Attachment(new MemoryStream(attatchments[i]), attachFileNames[i] + "." + attachTypes[i]);
                    message.Attachments.Add(attatchment);
                }
            }
            message.Priority = MailPriority.Normal;
            message.IsBodyHtml = true;

            message.Sender = new MailAddress(username, username);
            message.From = message.Sender;
            client.Send(message);
            message.Dispose();
        }
    }
}
