using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Office.Interop.Outlook;
using ICP.MailCenter.ServiceInterface;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using ICP.Message.ServiceInterface;
namespace ICP.MailCenterFramework.UI
{
    /// <summary>
    /// 邮件中心全局存放客户端属性类
    /// </summary>
   public static class ClientProperties
    {
       public static string _TempPath;
       public static string TempPath
       {
           get
           {
               if (string.IsNullOrEmpty(_TempPath))
               {
                   _TempPath = Path.GetTempPath();
               }
               return _TempPath;
           }
       }
       //Attachment
       private static List<AttachmentContent> _Attachments;
       public static List<AttachmentContent> Attachments
       {
           get
           {
               return _Attachments ?? (_Attachments = new List<AttachmentContent>());
           }
           set
           {
               _Attachments = value;
           }
       }
       public static _MailItem _CurrentMailItem;
       public static _MailItem CurrentMailItem
       {
           get { return _CurrentMailItem; }
           set { _CurrentMailItem = value; }
       }
       //选择右键entryID
       public static string SelectMail_EntryID = string.Empty;
       public static string _EmailAddress;
       public static string EmailAddress
       {
           get { return _EmailAddress; }
           set { _EmailAddress = value; }
       }
       public static string MessageID { get; set; }
       public static string SenderName { get; set; }
    }
}
