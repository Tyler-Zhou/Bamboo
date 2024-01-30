using ICP.MailCenter.ServiceInterface;
using ICP.MailCenter.UI;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Threading;
using outlook = Microsoft.Office.Interop.Outlook;

namespace ICP.MailCenterFramework.UI
{
   public class FolderWrapper
    {
        #region 属性

        private MAPIFolder _folder { get; set; }
        private Folders _folders { get; set; }
        private Items _items { get; set; }

        #endregion
       public static void RegisterEvent()
       {
           //Folders rootFolders = OutlookUtility.CreateOutlookNameSpaceInstance().Folders;
           //int count = rootFolders.Count;

           outlook.Application olApp = new outlook.Application();
           NameSpace olNS = olApp.Session;
           Folders rootFolders = olNS.Folders;
           int count = rootFolders.Count;
           for (int i = 1; i <= count; i++)
           {
               MAPIFolder folder = rootFolders[i];
               RegisterFolderEvents(folder);
              
           }

       }
       public static void RegisterFolderEvents(MAPIFolder folder)
       {
             var wrapper = new FolderWrapper();
                wrapper._folder = folder;
                if (wrapper._folder != null)
                {
                    try
                    {
                        //wrapper._folders = wrapper._folder.Folders;
                        ////文件夹发生改动（包括邮件的增删改）
                        //wrapper._folders.FolderChange += wrapper.OnFolderChange;
                        wrapper._items = wrapper._folder.Items;
                        //只是为了保存与业务相关的邮件
                        wrapper._items.ItemAdd += wrapper.OnItemAdd;
                    }
                    catch (System.Exception ex)
                    {
                        //ICP.Framework.CommonLibrary.Logger.Log.Error(CommonHelper.BuildExceptionString(ex));
                    }
                }
       }
       private void OnItemAdd(object objItem)
       {
           MAPIFolder olFolder = null;
           MailItem olItem = null;
           //int count = 0;
           try
           {
               olItem = objItem as MailItem;
               if (olItem == null)
                   return;
               olFolder = olItem.Parent as MAPIFolder;
               if (olFolder == null)
                   return;

               ClientProperties._NewMailFolder = olFolder;
               string folderName = olFolder.Name.ToLower();
               ClientProperties.isReSend = false;
               //if (folderName.Equals("发件箱") || folderName.Equals("outbox"))
               //{
               //    if (olItem.Sent == false)
               //    {
               //        ClientProperties.isReSend = true;
               //        olItem.Send();
               //    }
               //    return;
               //}
               //这里是沟通历史记录回调，通知发送已发送成功
               if (folderName.Equals("已发送邮件") || folderName.Equals("sent items"))
               {
                   if (ClientProperties.SendMail_EntryID.Equals(olItem.EntryID))
                       return;
                   else
                   {
                       ClientProperties.SendMail_EntryID = olItem.EntryID;
                       WaitCallback callback = (obj) =>
                       {
                           try
                           {
                               Message.ServiceInterface.Message message =
                                  OutlookUtility.ConvertMailItemToMessageInfo(olItem, false);
                               message = OutlookUtility.GetMessageInfo(message);

                               OutlookUtility.SaveMessage(message);
                               message = null;
                           }
                           catch (System.Exception ex)
                           {
                               ICP.Framework.CommonLibrary.LogHelper.SaveLog(
                                   ICP.Framework.CommonLibrary.Common.CommonHelper.BuildExceptionString(ex));
                               //LocalCommonServices.ErrorTrace.SetErrorInfo(FolderPart.FindForm(), ex.Message);
                           }
                           finally
                           {
                               MailUtility.ReleaseComObject(olItem);
                           }
                       };
                       ThreadPool.QueueUserWorkItem(callback);
                   }
               }
           }
           catch (System.Exception ex) { Console.WriteLine(ex.Message); }
           finally
           {
               MailUtility.ReleaseComObject(olFolder);
           }
       }
    }
}
