using System;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.MailCenter.ServiceInterface;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Practices.CompositeUI;

namespace ICP.MailCenter.UI
{
    /// <summary>
    /// 邮件文件夹包装类
    /// </summary>
    public partial class FolderWrapper
    {
        private FolderWrapper()
        {
        }

        #region 常量
        private Microsoft.Practices.CompositeUI.Collections.ManagedObjectCollection<object> _SmartParts;
        public Microsoft.Practices.CompositeUI.Collections.ManagedObjectCollection<object> SmartParts
        {
            get
            {
                if (_SmartParts == null)
                    _SmartParts = ServiceClient.GetClientService<WorkItem>().SmartParts;
                return _SmartParts;
            }
        }

        public EmailFolderPart FolderPart
        {
            get
            {
                return SmartParts.Get<EmailFolderPart>(MailCenterWorkSpaceConstants.EmailFolderPart);
            }
        }

        #endregion

        #region 属性

        private MAPIFolder _folder { get; set; }
        private Folders _folders { get; set; }
        private Items _items { get; set; }

        #endregion

        #region 方法

        public static void Wrapper(MAPIFolder folder, bool isFolderChanged)
        {
            if (isFolderChanged)
            {
                var wrapper = new FolderWrapper();
                wrapper._folder = folder;
                if (wrapper._folder != null)
                {
                    try
                    {
                        wrapper._folders = wrapper._folder.Folders;
                        //文件夹发生改动（包括邮件的增删改）
                        wrapper._folders.FolderChange += wrapper.OnFolderChange;
                        wrapper._items = wrapper._folder.Items;
                        //只是为了保存与业务相关的邮件
                        wrapper._items.ItemAdd += wrapper.OnItemAdd;
                    }
                    catch (System.Exception ex)
                    {
                        ICP.Framework.CommonLibrary.Logger.Log.Error(CommonHelper.BuildExceptionString(ex));
                    }
                }
                //if (folder != null)
                //{
                //    _Items = folder.Items;
                //    _Items.ItemAdd += new ItemsEvents_ItemAddEventHandler(FolderItem_Add);
                //    if (isItemChanged)
                //    {
                //        _Items.ItemChange += new ItemsEvents_ItemChangeEventHandler(FolderItem_ItemChange);
                //    }
                //    _Items.ItemRemove += new ItemsEvents_ItemRemoveEventHandler(FolderItem_ItemRemove);
                //}

            }
        }
        private void OnFolderChange(MAPIFolder Folder)
        {
            OnRefershed(Folder);
        }

        private void OnItemAdd(object Item)
        {
            InnerAdd(Item);
        }

        ///// <summary>
        ///// 文件夹删除邮件
        ///// </summary>
        //void FolderItem_ItemRemove()
        //{
        //    //如果是拖拽文件夹，然后文件夹下邮件会执行Item_Remove事件，但是需要过滤
        //    if (ClientProperties.isDragDropFolder == false)
        //    {
        //        ClientProperties.FlagMailItemPropertyChanged = false;
        //        MAPIFolder olFolder = null;
        //        try
        //        {

        //            if (!string.IsNullOrEmpty(folder_EntryID))
        //                olFolder = MailListPresenter.GetFolderByEntryID(folder_EntryID);
        //            else
        //                olFolder = MailListPresenter.GetFolderByEntryID(_folder.EntryID);

        //            if (olFolder == null)
        //                return;

        //            Refershed(olFolder, false);
        //        }
        //        catch (System.Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //        }
        //        finally
        //        {
        //            MailUtility.ReleaseComObject(olFolder);
        //        }
        //    }
        //}


        ///// <summary>
        ///// 指该表邮件未读状态
        ///// </summary>
        ///// <param name="Item"></param>
        //void FolderItem_ItemChange(object Item)
        //{
        //    ChangeRefershFolder(Item);
        //}

        //void ChangeRefershFolder(object Item)
        //{
        //    MailItem olItem = null;
        //    MAPIFolder olFolder = null;
        //    try
        //    {
        //        olItem = Item as MailItem;

        //        if (olItem == null)
        //        {
        //            ReportItem olReportItem = Item as ReportItem;
        //            if (olReportItem != null)
        //            {
        //                olFolder = olReportItem.Parent as MAPIFolder;
        //                Refershed(olFolder, false);
        //            }
        //        }
        //        else
        //        {
        //            olFolder = olItem.Parent as MAPIFolder;
        //            Refershed(olFolder, false);
        //        }
        //        ClientProperties.FlagMailItemPropertyChanged = true;
        //    }
        //    catch (System.Exception ex) { Console.WriteLine(ex.Message); }
        //    finally
        //    {
        //        MailUtility.ReleaseComObject(olItem);
        //        MailUtility.ReleaseComObject(olFolder);
        //    }
        //}

        /// <summary>
        /// 获取文件夹数量，刷新树节点
        /// </summary>
        /// <param name="olFolder"></param>
        private void OnRefershed(MAPIFolder olFolder)
        {
            if (olFolder != null)
            {
                int count = 0;
                bool isContains = MailUtility.IsInDOJFolders(olFolder.Name);
                if (isContains)
                {
                    count = olFolder.Items.Count; //将草稿,发件箱,垃圾邮件总数量刷新
                }
                else
                {
                    //if (isFolderAdd)
                    //{
                    //    //如果发现是属于已发送文件夹，则不需要显示标题“收到1封邮件”
                    //    bool isContainsSentFolder = false;
                    //    MailUtility.IsContainsFolders(MailUtility.NotDisplayTitleFolders, olFolder.Name.ToLower(), ref isContainsSentFolder);
                    //    if (!isContainsSentFolder)
                    //    {
                    //        ClientProperties.UnReadCount++;
                    //    }
                    //}

                    count = olFolder.UnReadItemCount;
                }
                InnerRefershed(count, olFolder.Name, olFolder.FullFolderPath, olFolder.EntryID);//刷新节点
            }
        }


        private void InnerAdd(object obj)
        {
            //如果不是拖拽的邮件，而是新增邮件
            if (FolderPart.IsHandleCreated && ClientProperties.isDragDropMail == false)
            {
                ClientProperties.FlagMailItemPropertyChanged = false;
                if (ClientProperties.isSaveMail)
                    SaveShipmentRelationMail(obj);
            }
        }

        /// <summary>
        /// 保存与业务相关的邮件
        /// </summary>
        /// <param name="objItem"></param>
        void SaveShipmentRelationMail(object objItem)
        {
            MAPIFolder olFolder = null;
            MailItem olItem = null;
            int count = 0;
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
                if (folderName.Equals("发件箱") || folderName.Equals("outbox"))
                {
                    if (olItem.Sent == false)
                    {
                        ClientProperties.isReSend = true;
                        olItem.Send();
                    }
                    return;
                }
            }
            catch (System.Exception ex) { Console.WriteLine(ex.Message); }
            finally
            {
                MailUtility.ReleaseComObject(olFolder);
            }
        }
        /// <summary>
        /// 刷新节点
        /// </summary>
        /// <param name="count"></param>
        /// <param name="folderName"></param>
        /// <param name="fullPath"></param>
        /// <param name="entityID"></param>
        private void InnerRefershed(int count, string folderName, string fullPath, string entityID)
        {
            FolderPart.UpdateTreeNode(count, folderName, fullPath, entityID);
        }

        #endregion
    }
}
