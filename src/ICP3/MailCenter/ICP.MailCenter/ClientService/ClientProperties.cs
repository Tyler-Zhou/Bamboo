using System;
using System.Collections.Generic;
using System.Threading;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Office.Interop.Outlook;
using ICP.MailCenter.ServiceInterface;
using System.Windows.Forms;
using System.IO;
using ICP.Message.ServiceInterface;

namespace ICP.MailCenter.UI
{
    /// <summary>
    /// 邮件中心全局存放客户端属性类
    /// </summary>
    public static class ClientProperties
    {
        /// <summary>
        /// 搜索文件夹，查找到文件夹后，将其展开延迟等待
        /// </summary>
        public static AutoResetEvent ExpandResetEvent { get; set; }

        /// <summary>
        /// 是否展开TreeNode节点
        /// </summary>
        public static bool IsExpandTreeNode = false;
        /// <summary>
        /// 是否折叠TreeNode节点
        /// </summary>
        public static bool IsCollapseTreeNode = false;

        /// <summary>
        /// 是否接收到新邮件
        /// </summary>
        public static bool IsReceiveNewMail = false;
        /// <summary>
        /// 是否选择邮件改变了
        /// </summary>
        public static bool selectedChanged = true;
        /// <summary>
        /// 标识邮件中心邮件列表邮件属性是否被改变
        /// </summary>
        public static bool FlagMailItemPropertyChanged = false;

        /// <summary>
        /// 记录用户进入邮件中心时间
        /// </summary>
        public static DateTime BeginTime;
        /// <summary>
        /// 记录用户结束邮件中心时间
        /// </summary>
        public static DateTime EndTime;
        /// <summary>
        /// 记录选中的邮件EntryID
        /// </summary>
        public static string selected_MailEntryID = string.Empty;
        /// <summary>
        /// 记录一个标识，邮件是否重新发送
        /// </summary>
        public static bool isReSend = false;
        //新邮件ID        
        public static string NewMail_EntryID = string.Empty;
        //新邮件entryID，为了避免Item_Send记录邮件日志double
        public static string SendMail_EntryID = string.Empty;
        //选择右键entryID
        public static string SelectMail_EntryID = string.Empty;
        //标示是否要同步文件夹
        public static bool IsRefershFolders = false;
        /// <summary>
        /// 获取当前接收到未读取的邮件个数
        /// </summary>
        public static int UnReadCount = 0;
        public static string newMail_UnRead = string.Empty;
        public static bool IsEnableToolBar = false;
        //标志是否拖拽邮件
        public static bool isDragDropMail = false;
        //标志是否拖拽文件夹
        public static bool isDragDropFolder = false;
        /// <summary>
        /// 后台同步时记录根目录文件夹集合
        /// </summary>
        public static List<string> RootFolders = new List<string>();

        public static string folder_FullPath = string.Empty;
        ////为了过滤在邮件中心不需要记录邮件日志的条件
        public static bool isSaveMail = false;
        /// <summary>
        /// 标识是否要删除配置文件
        /// </summary>
        public static bool IsNeedDeleteConfigFile = false;

        public static MAPIFolder _NewMailFolder;
        public static MAPIFolder NewMailFolder
        {
            get { return _NewMailFolder; }
            set { _NewMailFolder = value; }
        }

        public static MAPIFolder _CurrentFolder;
        public static MAPIFolder CurrentFolder
        {
            get { return _CurrentFolder; }
            set { _CurrentFolder = value; }
        }
        /// <summary>
        /// 当前TreeView 选中的节点
        /// </summary>
        public static TreeNode CurrentSelectedNode { get; set; }
        /// <summary>
        /// 是否正在同步文件夹
        /// </summary>
        public static bool IsSynchronizingFolders = false;

        public static _MailItem _CurrentMailItem;
        public static _MailItem CurrentMailItem
        {
            get { return _CurrentMailItem; }
            set { _CurrentMailItem = value; }
        }
        /// <summary>
        /// 当前选择的邮件是否属于MailItem
        /// </summary>
        public static object ObjMailItem
        {
            get { return CurrentMailItem; }
        }

        public static string CurrentFolderName
        {
            get;
            set;
        }
        public static string _EmailAddress;
        public static string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }

        public static string MessageID { get; set; }

        public static string SenderName { get; set; }
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

        static bool? isCurrentOutlookSupported = null;
        /// <summary>
        /// 判断当前outlook是否为当前邮件中心支持的版本
        /// 当前邮件中心支持的Outlook版本为2007(12),2010(14)
        /// </summary>
        public static bool IsCurrentOutlookSupported
        {
            get
            {
                if (isCurrentOutlookSupported.HasValue)
                    return isCurrentOutlookSupported.Value;
                else
                {
                    if (ClientUtility.olVersion > 11 && ClientUtility.olVersion < 15)
                    {
                        isCurrentOutlookSupported = true;
                    }
                    else
                    {
                        isCurrentOutlookSupported = false;
                    }
                    return isCurrentOutlookSupported.Value;
                }
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

        /// <summary>
        /// 选择文件夹文件夹ID
        /// </summary>
        public static String _Copy_Folder_EntryID;
        public static string Copy_Folder_EntryID
        {
            get
            {
                return _Copy_Folder_EntryID;
            }
            set
            {
                _Copy_Folder_EntryID = value;
            }
        }
        //文件夹列表文件夹ID
        public static String _Folder_EntryID;
        public static String Folder_EntryID
        {
            get
            {
                //设置一个默认 收件箱
                if (string.IsNullOrEmpty(_Folder_EntryID))
                {
                    MAPIFolder olFolder = ClientUtility.OlNS.GetDefaultFolder(OlDefaultFolders.olFolderInbox);
                    _Folder_EntryID = olFolder.EntryID;
                    MailUtility.ReleaseComObject(olFolder);
                }

                return _Folder_EntryID;
            }
            set
            {
                _Folder_EntryID = value;
            }
        }



        public static string SelectedNodeName { get; set; }

        /// <summary>
        /// 新增文件夹
        /// </summary>
        public static MAPIFolder NewFolder { get; set; }

        /// <summary>
        /// 邮件动作
        /// </summary>
        public static MailAction Action = MailAction.Unknown;
        /// <summary>
        /// 是否把邮件发送出去标识
        /// </summary>
        public static bool isSendMail = false;

        //refersh folder
        public static bool _flagRefershFolders = false;
        public static bool FlagRefershFolder
        {
            get { return _flagRefershFolders; }
            set { _flagRefershFolders = value; }
        }

        /// <summary>
        /// 缓存默认排序的view xml
        /// </summary>
        public static string ViewXmlTemp { get; set; }
        /// <summary>
        /// 验证视图控件是否有效，如果无效，就不需要则不需要查看一封邮件，otherwise,可以查看，默认是选中第一封邮件的。
        /// </summary>
        public static bool isOutlookViewCtlValidate = true;

        private static string _ViewCtlDefaultFolderPath;
        public static string ViewCtlDefaultFolderPath
        {
            get
            {
                if (string.IsNullOrEmpty(_ViewCtlDefaultFolderPath))
                    _ViewCtlDefaultFolderPath = ClientUtility.olCultrue == "zh-cn" ? @"\\收件箱" : @"\\Inbox";

                return _ViewCtlDefaultFolderPath;
            }
        }
        /// <summary>
        /// 拖拽邮件到最后一封邮件时，才需要去显示最后一封邮件的detail
        /// </summary>
        public static bool isDragDropLastOne = true;

        /// <summary>
        /// 缓存设置邮件已读的时间
        /// </summary>
        public static int MailReadTimeInterval { get; set; }


        public static List<bool> _dragDropUnreadMail;
        /// <summary>
        /// 记录拖拽邮件至目标文件夹有没有存在未读的邮件
        /// </summary>
        public static List<bool> DragDropUnreadMail
        {
            get
            {
                return _dragDropUnreadMail ?? (_dragDropUnreadMail = new List<bool>());
            }
            set
            {
                _dragDropUnreadMail = value;
            }
        }

        /// <summary>
        /// outlook被意外关闭后，错误消息
        /// </summary>
        private static string _ErrorMessage;
        public static string ErrorMessage
        {
            get
            {
                if (string.IsNullOrEmpty(_ErrorMessage))
                    _ErrorMessage = LocalData.IsEnglish ? "Microsoft Outlook was closed unexpectedly, is awakening... " : "Microsoft Outlook被意外关闭，正在唤醒...";
                return _ErrorMessage;
            }
        }

    }
}
