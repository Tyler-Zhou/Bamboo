using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ICP.MailCenter.ServiceInterface;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ICP.MailCenter.UI;
using Microsoft.Office.Interop.Outlook;
using ICP.Framework.CommonLibrary.Common;
using System.IO;
using System.Text.RegularExpressions;
using ICP.Framework.CommonLibrary.Client;
using DevExpress.XtraEditors;
using System.Diagnostics;
using Microsoft.Practices.CompositeUI;
using AxMicrosoft.Office.Interop.OutlookViewCtl;
using ICP.Message.ServiceInterface;
using System.Threading;
using System.Text;
namespace ICP.MailCenterFramework.UI
{
  public class MailListPresenter
    {
        /// <summary>
        /// 将邮件接收人转换成string字符串，以 ‘;’分割
        /// </summary>
        /// <param name="olRecipients"></param>
        /// <param name="type">类型:发件人为0，接收人为1，CC为2</param>
        /// <param name="nameList">发件人或接收人的名称</param>
        /// <returns></returns>
        public static string ConvertRecipientsToString(Recipients olRecipients, int type, out string nameList)
        {
            StringBuilder strAddress = new StringBuilder();
            StringBuilder strNames = new StringBuilder();
            try
            {
                int count = olRecipients.Count;
                for (int i = 1; i <= count; i++)
                {
                    if (olRecipients[i].Type == type)
                    {
                        string address = !string.IsNullOrEmpty(olRecipients[i].Address) ? olRecipients[i].Address : olRecipients[i].Name;
                        strAddress.Append(string.Format("{0};", address));
                        strNames.Append(string.Format("{0};", olRecipients[i].Name));
                    }
                }
            }
            finally
            {
                MailUtility.ReleaseComObject(olRecipients);
            }


            if (strAddress.Length > 0)
            {
                nameList = strNames.ToString(0, strNames.Length - 1);
                //nameList = names.Aggregate((a, b) => string.Format("{0};{1}", a, b));
                //return addresses.Aggregate((a, b) => string.Format("{0};{1}", a, b));
                return strAddress.ToString(0, strAddress.Length - 1);
            }
            else
            {
                nameList = string.Empty;
                return string.Empty;
            }
        }
        /// <summary>
        /// 解析邮件的主体
        /// 1.将邮件中的图片保存到本地，并将邮件中的图片路径替换为本地路径
        /// 2.抽取邮件中真正的附件，剔除邮件背景图片附件
        /// </summary>
        /// <param name="attachments"></param>
        /// <param name="htmlBody"></param>
        /// <returns></returns>
        public static string FixHtmlBody(Attachments attachments, string htmlBody)
        {
            string body = htmlBody;
            Attachments _attachments = attachments;
            foreach (Attachment att in _attachments)
            {
                string fileName = GetAttachmentName(att);

                //http://schemas.microsoft.com/mapi/proptag/0x3712001F
                //http://schemas.microsoft.com/mapi/proptag/0x3704001F
                //http://schemas.microsoft.com/mapi/proptag/0x3707001F                
                //查询图片contentID
                string emCID = att.PropertyAccessor.GetProperty("http://schemas.microsoft.com/mapi/proptag/0x3712001F").ToString();

                if (!string.IsNullOrEmpty(emCID))
                {
                    //当可以查询到图片contentID，却没有要替换htmlBody中的字符，则该文件就是附件
                    bool isReplaceSign = false;
                    string fullFilePath = GetFileFullPath(fileName);
                    SaveAsLocalFile(att, fullFilePath);
                    //将平铺的背景图片替换
                    body = ReplaceHtmlBodyBackgroundImage(body, emCID, ref isReplaceSign);
                    //将图片路径图换掉
                    body = ReplaceHtmlBodyImagePathWithLocalPath(emCID, body, fullFilePath, ref isReplaceSign);

                    if (!isReplaceSign)
                    {
                        AttachmentContent attachmentInfo = AddAttachmentToList(att, body);
                        if (attachmentInfo != null)
                        {
                            ClientProperties.Attachments.Add(attachmentInfo);
                        }
                    }
                }
                else
                {
                    //特殊处理： 把Attachment为Image的文件类型去判断HtmlBody中是否包含该图片，如果不包含，则添加到集合;否则表示是真正的附件
                    AttachmentContent attachmentInfo = AddAttachmentToList(att, body);
                    if (attachmentInfo != null)
                    {
                        ClientProperties.Attachments.Add(attachmentInfo);
                    }
                }
            }
            MailUtility.ReleaseComObject(_attachments);

            return body;
        }
        /// <summary>
        /// 获取附件名称
        /// </summary>
        /// <param name="attachment"></param>
        /// <returns></returns>
        private static string GetAttachmentName(Attachment attachment)
        {
            string fileName = string.Empty;
            if (attachment.Type.Equals(OlAttachmentType.olOLE))
                fileName = attachment.DisplayName;
            else
                fileName = attachment.FileName;
            return fileName;
        }
        /// <summary>
        /// 得到文件完整路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFileFullPath(string fileName)
        {
            string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempPath);
            return Path.Combine(tempPath, fileName);
        }
        private static string ReplaceHtmlBodyBackgroundImage(string htmlBody, string contentID, ref bool isReplaceSign)
        {
            string body = htmlBody;
            // “background=” 代表背景图片
            if (body.Contains("background="))
            {
                string pattern = string.Format("background=(\")?(cid:)?{0}(\")?", contentID);
                body = Regex.Replace(htmlBody, pattern, "", RegexOptions.IgnoreCase);
                isReplaceSign = true;
            }
            return body;
        }

        /// <summary>
        /// 将附件保存到本地磁盘
        /// </summary>
        /// <param name="att"></param>
        /// <param name="fullFilePath"></param>
        /// <returns></returns>
        public static string SaveAsLocalFile(Attachment att, string fullFilePath)
        {
            try
            {
                //将附件保存到本地
                att.SaveAsFile(fullFilePath);
            }
            //如果出现 “尝试读取或写入受保护的内存。这通常指示其他内存已损坏”异常，则文件名称重命名为 FileName(1).extension
            catch (System.AccessViolationException ave)
            {
                try
                {
                    string extension = Path.GetExtension(fullFilePath);
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fullFilePath);

                    string fileName = string.Format("{0}(1){1}", fileNameWithoutExtension, extension);
                    fullFilePath = Path.Combine(Path.GetDirectoryName(fullFilePath), fileName);
                    att.SaveAsFile(fullFilePath);
                }
                catch (System.Exception ex)
                {
                    throw;
                }
            }

            return fullFilePath;
        }
        /// <summary>
        /// 将htmlbody中图片路径替换
        /// </summary>
        /// <param name="htmlBody"></param>
        /// <returns></returns>
        private static string ReplaceHtmlBodyImagePathWithLocalPath(string contentID, string htmlBody, string fullFilePath, ref bool isReplaceSign)
        {
            if (htmlBody.Contains(contentID))
            {
                string pattern = string.Format("cid:(\")?{0}(\")?", contentID);
                htmlBody = Regex.Replace(htmlBody, pattern, fullFilePath + "\"", RegexOptions.IgnoreCase);
                isReplaceSign = true;
            }
            return htmlBody;
        }
        private static AttachmentContent AddAttachmentToList(Attachment att, string body)
        {
            object objMime = att.PropertyAccessor.GetProperty("http://schemas.microsoft.com/mapi/proptag/0x370E001E");

            string emMime = objMime == null ? "" : objMime.ToString();
            if (emMime.ToLower().Contains("image"))
            {
                if (!body.Contains(att.FileName))
                {
                    return CreateAttachmentContentInfo(att);
                }
            }
            else
            {
                //将真正的附件添加到集合中
                return CreateAttachmentContentInfo(att);
            }

            return null;
        }
        /// <summary>
        /// 将附件转换成实体类
        /// </summary>
        /// <param name="att"></param>
        /// <returns></returns>
        public static AttachmentContent CreateAttachmentContentInfo(Attachment att)
        {
            return new AttachmentContent()
            {
                OLAttachment = att,
                Name = GetFileName(att),
                DisplayName = att.DisplayName ?? string.Empty,
                Size = att.Size,
                UploadTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified)
            };
        }
        private static string GetFileName(Attachment att)
        {
            string fileName = string.Empty;
            try
            {
                fileName = att.FileName;
            }
            catch (System.Exception ex)
            {
                fileName = att.DisplayName;
            }

            return fileName;
        }
    }
}
