#region Comment

/*
 * 
 * FileName:    OutlookService.cs
 * CreatedOn:   2014/7/8 15:25:31
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      ->IOutLookService借口实现类
 *      ->  1.新邮件、回复、回复全部、回复全部带附件 等方法的具体实现
 *      ->  2.异步通讯录
 *      ->  3.邮件归档
 * History：
 *      ->_CommandBarButtonEvents_ClickEventHandler
 *      ->  CancelDefault值设置:取消默认事件后执行设置CancelDefault值之前的代码，之后的代码只执行一次
 * 
 * 
 * 
 * 
 */

#endregion

using ICP.Framework.CommonLibrary.Client;
using ICP.MailCenterFramework.ServiceInterface;
using ICP.Message.ServiceInterface;
using Microsoft.Office.Interop.Outlook;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace ICP.MailCenterFramework.UI
{
    public class OutlookOperateService : IOutlookOperateService
    {
        #region 公用方法
        /// <summary>
        /// 从ICP推送关联信息到内存变量
        /// </summary>
        /// <param name="dt">关联信息Table</param>
        public void SearchOperationView(DataTable dt)
        {
            try
            {
                if (dt != null)
                {
                    HelpMailStore.TableBusiness.Merge(dt);
                }
            }
            catch (System.Exception ex)
            {
                ToolUtility.WriteLog("SearchOperationView", ex);
            }
        }

        /// <summary>
        /// 获取邮件另存目录
        /// </summary>
        /// <param name="strEntryID">邮件EntryID</param>
        /// <param name="strMessageID">邮件MessageID</param>
        /// <param name="strIMessageID">消息Guid</param>
        /// <returns>另存目录</returns>
        public string GetMailItemSaveAsPath(string strEntryID, string strMessageID, string strIMessageID)
        {
            string saveAsPath = ICPPathUtility.CombineDirectory4FileName(ICPPathUtility.TempPathMailEntity(), (string.IsNullOrEmpty(strEntryID) ? strIMessageID : strEntryID).ToLower() + ".msg");
            if (!File.Exists(saveAsPath))
            {
                SaveAsMailItemByOutlook(saveAsPath, strEntryID, strMessageID);
            }
            return saveAsPath;
        }

        /// <summary>
        /// 是否当前登录用户
        /// </summary>
        /// <param name="loginName">当前登录用户名</param>
        /// <returns></returns>
        public bool IsCurrentLoginUser(string loginName)
        {
            return loginName.Equals(LocalData.UserInfo.LoginName);
        }

        /// <summary>
        /// 通过MessageID查询并转换成Byte[]
        /// </summary>
        /// <param name="strEntryID">Mail EntryID(GUID)</param>
        /// <param name="strMessageID">MessageID</param>
        /// <param name="strIMessageID">IMessageID</param>
        /// <returns>byte[]</returns>
        public byte[] GetByteByMessageID(string strEntryID, string strMessageID, string strIMessageID)
        {
            byte[] mailByte = null;
            MailItem mailItem = null;
            string saveAsPath = ICPPathUtility.CombineDirectory4FileName(ICPPathUtility.TempPathMailEntity(), (string.IsNullOrEmpty(strEntryID) ? strIMessageID : strEntryID).ToLower() + ".msg");
            try
            {
                //if (!File.Exists(saveAsPath))
                //{
                //    //确保Outlook启动
                //    if (!ClientHelper.IsAppExists("OUTLOOK.EXE"))
                //        ClientHelper.EnsureEmailCenterAppStarted();
                //    if (!string.IsNullOrEmpty(strEntryID))
                //        mailItem = GetMailItemByEntryID(strEntryID) as MailItem;
                //    if (mailItem == null && !string.IsNullOrEmpty(strMessageID))
                //        mailItem = GetMailItemByMessageID(strMessageID);
                //    if (mailItem != null)
                //        OutlookUtility.SaveMailItemAs(mailItem, saveAsPath);
                //}
                if (File.Exists(saveAsPath))
                    mailByte = ToolUtility.ReadFileContentFromDisk(saveAsPath);

            }
            catch (System.Exception ex)
            {
                ToolUtility.WriteLog("GetByteByMessageID", ex);
            }
            //finally
            //{
            //    DeleteMsgFile(saveAsPath);
            //}
            return mailByte;
        }

        /// <summary>
        /// 通过EntryID获取MailItem对象
        /// </summary>
        /// <param name="paramEntryID">EntryID</param>
        /// <returns>MailItem对象：未找到邮件返回空</returns>
        public object GetMailItemByEntryID(string paramEntryID)
        {
            object mailItem = null;
            try
            {
                if (!string.IsNullOrEmpty(paramEntryID))
                {
                    mailItem = OutlookUtility.OLNameSpace.GetItemFromID(paramEntryID);
                }
            }
            catch (System.Exception ex)
            {
                mailItem = null;
            }
            return mailItem;
        }

        /// <summary>
        /// 通过MessageID查询邮件
        /// </summary>
        /// <param name="strMessageID">MessageID</param>
        /// <returns>MailItem</returns>
        public MailItem GetMailItemByMessageID(string strMessageID)
        {
            MailItem mailItem = null;
            try
            {
                OutlookAdvancedSearch oas = new OutlookAdvancedSearch(strMessageID);
                oas.RunAdvancedSearch();
                if (oas.SearchResults != null && oas.SearchResults.Count > 0)
                {
                    mailItem = oas.SearchResults[0] as MailItem;
                }
                oas = null;
            }
            catch (System.Exception ex)
            {
                ToolUtility.WriteLog("GetMailItemByMessageID",ex);
            }
            return mailItem;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="strPath"></param>
        private void DeleteMsgFile(string strPath)
        {
            try
            {
                if (File.Exists(strPath))
                {
                    File.Delete(strPath);
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// 获取MailItem对象
        /// </summary>
        /// <param name="saveAsPath">另存路径</param>
        /// <param name="strEntryID">邮件EntryID</param>
        /// <param name="strMessageID">邮件MessageID</param>
        /// <returns></returns>
        private MailItem SaveAsMailItemByOutlook(string saveAsPath,string strEntryID, string strMessageID)
        {
            MailItem mailItem = null;
            try
            {
                //确保Outlook启动
                if (!ClientHelper.IsAppExists("OUTLOOK.EXE"))
                    ClientHelper.EnsureEmailCenterAppStarted();
                if (!string.IsNullOrEmpty(strEntryID))
                    mailItem = GetMailItemByEntryID(strEntryID) as MailItem;
                if (mailItem == null && !string.IsNullOrEmpty(strMessageID))
                    mailItem = GetMailItemByMessageID(strMessageID);
                if (mailItem != null && !string.IsNullOrEmpty(saveAsPath))
                     OutlookUtility.SaveMailItemAs(mailItem, saveAsPath);
            }
            catch (System.Exception ex)
            {
                ToolUtility.WriteLog("SaveAsMailItemByOutlook", ex);
            }
            return mailItem;
        }
        #endregion

        #region 插件方法
        /// <summary>
        /// 回复全部(至所有人包含附件)
        /// </summary>
        /// <param name="item">邮件对象</param>
        public void ReplyEmailToAllContainsAttachment(object item)
        {
            if (item == null) return;
            MailItem _item = item as MailItem;
            if (_item == null) return;
            MailItem relyAllItem = null;
            if (_item.Sent)
            {
                relyAllItem = _item.ReplyAll();
                //当前答复邮件是否包含附件
                if (relyAllItem.Attachments != null)
                {
                    List<AttachmentContent> attachments = OutlookUtility.GetAttachmentContentsByMailItem(_item,false);
                    for (int index = 0; index < attachments.Count; index++)
                    {
                        relyAllItem.Attachments.Add(attachments[index].ClientPath
                            , OlAttachmentType.olByValue, index + 1, attachments[index].DisplayName);
                    }
                    //清空邮件列表集合对象
                    attachments.Clear();
                    attachments = null;
                }
                relyAllItem.Display(false);
                if (string.IsNullOrEmpty(relyAllItem.Body) || relyAllItem.Body == " ")
                {
                    relyAllItem.Close(OlInspectorClose.olDiscard);
                    _item.Display(false);
                }
            }
            else
                _item.Display(false);
            relyAllItem = null;
            _item = null;
        }

        /// <summary>
        /// 异步通讯录
        /// </summary>
        public void SynchronAddressBook()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                //实例化并显示窗体
                FrmSynchronAddressBook frmsab = new FrmSynchronAddressBook();
                frmsab.ShowDialog();
                frmsab.Dispose();
            }
        }

        /// <summary>
        /// 邮件归档
        /// </summary>
        public void EmailArchiving()
        {
            //1.显示归档窗体并自动执行归档操作
            FrmEmailArchiving frmArchiving = new FrmEmailArchiving();
            frmArchiving.ShowDialog();
            //frmArchiving.EmailArchiving();
        }

        /// <summary>
        /// 批量关联
        /// </summary>
        public void AssociatedBatch()
        {
            //批量关联
            FrmAssociatedBatch frmAssociatedBatch = new FrmAssociatedBatch();
            frmAssociatedBatch.ShowDialog();
            bool IsMoveMail = frmAssociatedBatch.IsMoveMail;
            frmAssociatedBatch.Dispose();
            if (IsMoveMail)
            {
                FrmEmailArchiving frmArchiving = new FrmEmailArchiving();
                frmArchiving.ArchivingModel = "AfterAssociated";
                frmArchiving.ShowDialog();
                //frmArchiving.EmailArchiving();
            }
        }

        /// <summary>
        /// 关联统计
        /// </summary>
        public void AssociatedStatistics()
        {
            //批量关联
            FrmAssociatedStatistics frmAssociatedStatistics = new FrmAssociatedStatistics();
            frmAssociatedStatistics.ShowDialog();
        }

        public void ShowMessageBox()
        {
            FrmEmailProperty frmMessageBox = new FrmEmailProperty();
            frmMessageBox.ShowDialog();
        }
        #endregion
    }
}
