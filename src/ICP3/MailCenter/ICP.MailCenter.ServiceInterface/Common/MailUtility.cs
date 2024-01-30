using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Xml.Linq;
using System.Threading;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Outlook;
using System.Security.Permissions;
using Microsoft.Practices.CompositeUI;
using Microsoft.Win32;

namespace ICP.MailCenter.ServiceInterface
{
    public static class MailUtility
    {
        private static readonly string arrMinToTray = "MinToTray";

        private static readonly string arrAutoSyncDisable = "AutoSyncDisable";

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        public static object synObj = new object();

        public static void ReleaseWaitHandle()
        {

            ClientHelper.ReleaseWaitHandle(ClientHelper.GetAppSettingValue(ClientConstants.EmailCenterNameKey));
        }


        /// <summary>
        /// 记录打开邮件中心的时间
        /// </summary>
        private static Stopwatch beginStopwatch = null;
        public static Stopwatch StartStopwatch()
        {
            beginStopwatch = StopwatchHelper.StartStopwatch();
            return beginStopwatch;
        }

        public static void EndStopwatch(string assemblyName, string text)
        {
            //if (beginStopwatch != null)
            //    StopwatchHelper.EndStopwatch(beginStopwatch, DateTime.Now, assemblyName,"", text);
        }

        /// <summary>
        /// 邮件中心程序启动参数
        /// </summary>
        public static string[] AppStartArgs
        {
            get;
            set;
        }

        /// <summary>
        /// 文件夹列表配置文件说明： 如果新同事安装ICP，会拷贝老同事ICP目录下所有文件，这时配置文件时别人的，所以通过Mac addresss来命名配置文件，就是读取的是自己电脑配置文件了。
        /// 加上文件名称为了在一台电脑上，不同用户登录该电脑的ICP
        /// </summary>
        public static string _ConfigPath = string.Empty;
        public static string ConfigPath
        {
            get
            {
                if (_ConfigPath == string.Empty)
                {
                    string macAddress = ICP.Framework.CommonLibrary.Common.ApplicationContext.Current.MacAddress;
                    if (string.IsNullOrEmpty(macAddress))
                    {
                        _ConfigPath = string.Format(@"{0}\mail\{1}_{2}_27_TreeView.cfg", AppDomain.CurrentDomain.BaseDirectory, ICP.Framework.CommonLibrary.Common.ApplicationContext.Current.Username, "TreeView");
                    }
                    else
                    {
                        macAddress = macAddress.Replace(":", "_");
                        _ConfigPath = string.Format(@"{0}\mail\{1}_{2}_27_TreeView.cfg", AppDomain.CurrentDomain.BaseDirectory, ICP.Framework.CommonLibrary.Common.ApplicationContext.Current.Username, macAddress);
                    }
                }

                return _ConfigPath;
            }
        }

        private static string _LoadingText;
        public static string LoadingText
        {
            get
            {
                if (string.IsNullOrEmpty(_LoadingText))
                    _LoadingText = LocalData.IsEnglish ? "Loading..." : "正在载入...";

                return _LoadingText;
            }
        }

        public static T TypeGetter<T>()
        {
            T tp;
            if (typeof(T).IsValueType)
            {
                tp = default(T);
            }
            else
            {
                tp = Activator.CreateInstance<T>();
            }
            return tp;
        }

        /// <summary>
        /// 获取拖拽文件集合
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string[] GetDropFiles(System.Windows.Forms.IDataObject data)
        {
            string[] files = data.GetData(DataFormats.FileDrop) as string[];
            if (files == null)
            {
                string file = data.GetData(DataFormats.FileDrop) as string;
                files = new string[] { file };
            }

            return files;
        }


        /// <summary>
        /// 释放组件对象
        /// </summary>
        /// <param name="comObject"></param>
        public static void ReleaseComObject(object comObject)
        {
            if (comObject != null)
            {
                Marshal.ReleaseComObject(comObject);
                comObject = null;
            }
        }
        /// <summary>
        /// 打印文件
        /// </summary>
        /// <param name="fullPath"></param>
        public static void PrintFile(string fullPath)
        {
            using (System.Diagnostics.Process shellProcess = new System.Diagnostics.Process())
            {
                shellProcess.StartInfo.FileName = fullPath;
                shellProcess.StartInfo.CreateNoWindow = true;
                shellProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                shellProcess.StartInfo.Verb = "print";
                shellProcess.Start();
            }
        }

        /// <summary>
        /// 打开Outlook时是否自动接收和发送邮件
        /// </summary>
        /// <param name="version"></param>
        public static void AutoSyncDisable(bool disabled)
        {
            int value = 1;
            if (disabled == true)
                value = 0;

            Microsoft.Win32.RegistryKey currentUser = Microsoft.Win32.Registry.CurrentUser;
            Microsoft.Win32.RegistryKey autoSyncDisable = null;
            try
            {
                autoSyncDisable = currentUser.OpenSubKey(
                         @"Software\Microsoft\Office\12.0\Outlook\Options\General\", RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl);
                if (autoSyncDisable != null)
                {
                    autoSyncDisable.SetValue(arrAutoSyncDisable, value, RegistryValueKind.DWord);

                    autoSyncDisable.Close();
                }
                else
                {
                    autoSyncDisable = currentUser.OpenSubKey(@"Software\Microsoft\Office\14.0\Outlook\Options\General\", RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl);

                    if (autoSyncDisable != null)
                    {
                        autoSyncDisable.SetValue(arrAutoSyncDisable, value, RegistryValueKind.DWord);

                        autoSyncDisable.Close();
                    }
                }
            }
            catch (System.Exception ex)
            {
                ICP.Framework.CommonLibrary.Logger.Log.Error(ICP.Framework.CommonLibrary.Common.CommonHelper.BuildExceptionString(ex));
            }
        }

        /// <summary>
        /// 将注册表MinToTray值设置为1,表示outlook最小化时隐藏
        /// </summary>
        public static void RegisterMinToTrayKey(string version)
        {
            Microsoft.Win32.RegistryKey 最小化至托盘右键菜单;
            Microsoft.Win32.RegistryKey currentUser = Microsoft.Win32.Registry.CurrentUser;
            try
            {
                最小化至托盘右键菜单 =
                    currentUser.OpenSubKey(
                        string.Format(@"Software\Microsoft\Office\{0}\Outlook\Preferences\", version), RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl);
                if (最小化至托盘右键菜单 != null)
                {
                    最小化至托盘右键菜单.SetValue(arrMinToTray, 1, RegistryValueKind.DWord);
                    最小化至托盘右键菜单.Close();
                }


                Microsoft.Win32.RegistryKey 安全性 = currentUser.OpenSubKey(string.Format(@"Software\Microsoft\Office\{0}\Outlook\Security", version), RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl);
                if (安全性 != null)
                {
                    安全性.SetValue("Level", 1, RegistryValueKind.DWord);

                    安全性.SetValue("ObjectModelGuard", 2, RegistryValueKind.DWord);

                    if (!安全性.GetValueNames().Any(item => item.Equals("PromptOOMSend")))
                    {
                        安全性.SetValue("PromptOOMSend", 2, RegistryValueKind.DWord);
                    }
                    if (!安全性.GetValueNames().Any(item => item.Equals("AdminSecurityMode")))
                    {
                        安全性.SetValue("AdminSecurityMode", 3, RegistryValueKind.DWord);
                    }

                    安全性.Close();
                }

                Microsoft.Win32.RegistryKey 邮件内容 = currentUser.OpenSubKey(string.Format(@"Software\Microsoft\Office\{0}\Outlook\Options\Mail", version), RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl);
                if (邮件内容 != null)
                {
                    邮件内容.SetValue("BlockExtContent", 0, RegistryValueKind.DWord);

                    邮件内容.Close();
                }
            }
            catch (System.Exception ex)
            {
                ICP.Framework.CommonLibrary.Logger.Log.Error(ICP.Framework.CommonLibrary.Common.CommonHelper.BuildExceptionString(ex));
            }
        }

        /// <summary>
        /// 只能有一个Outlook进程
        /// </summary>
        public static void SingleProcess(bool isRaisingEvents)
        {
            Process[] arrProcess = GetProcessByName("OUTLOOK");
            if (arrProcess == null || arrProcess.Length <= 0)
                StartProcess(false, string.Empty);

            if (isRaisingEvents)
            {
                if (!arrProcess[0].HasExited)
                {
                    arrProcess[0].EnableRaisingEvents = true;
                    arrProcess[0].Exited += OnProcessExited;
                }

                if (arrProcess.Length > 1)
                    Killed(arrProcess);
            }
            else
            {
                if (arrProcess.Length > 1)
                    Killed(arrProcess);
            }

            arrProcess = null;
        }
        /// <summary>
        /// 如果有两个进程，需要关闭一个进程
        /// </summary>
        /// <param name="arrProcess"></param>
        /// <returns></returns>
        private static Process Killed(Process[] arrProcess)
        {
            for (int i = 1; i < arrProcess.Length; i++)
            {
                arrProcess[i].Exited -= OnProcessExited;
                arrProcess[i].Kill();
            }

            return arrProcess[0];
        }

        private static void Killed()
        {
            Process[] arrProcess = GetProcessByName("Outlook");
            for (int i = 0; i < arrProcess.Length; i++)
            {
                arrProcess[i].Exited -= OnProcessExited;
                arrProcess[i].Kill();
            }
        }

        public static void KillProcess()
        {
            Process[] arrProcess = GetProcessByName("OUTLOOK");
            if (arrProcess != null)
            {
                if (arrProcess.Length > 1)
                {
                    Killed(arrProcess);
                }
                arrProcess = null;
            }
        }

        public static void ReStartOutLook()
        {
            Killed();
            StartProcess(false, string.Empty);
        }

        public static void StartProcess(bool isShowLoadingFrom, string message)
        {

            using (Process proc = new Process())
            {
                proc.StartInfo.FileName = "OUTLOOK.exe";
                proc.StartInfo.Arguments = "/safe:3";
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                proc.Start();
                if (!proc.HasExited)
                {
                    do
                    {
                        Thread.Sleep(460);

                    } while (!proc.WaitForInputIdle());
                }
            }

            //HideOutLookApp(isShowLoadingFrom, message);
        }
        /// <summary>
        /// 根WorkItem
        /// </summary>
        public static WorkItem RootWorkItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }
        /// <summary>
        /// 监听Outlook进程是否被关闭，如果被用户关闭后，就会执行该事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnProcessExited(object sender, EventArgs e)
        {
            lock (synObj)
            {
                try
                {
                    Process[] proc = GetProcessByName("OUTLOOK");
                    if (proc == null || proc.Length == 0)
                        StartProcess(false, string.Empty);
                    else
                        KillProcess();

                    RootWorkItem.Commands["Command_ValidateViewCtl"].Execute();
                    proc = null;
                }
                catch { }
            }
        }

        public static Process[] GetProcessByName(string procName)
        {
            return Process.GetProcessesByName(procName);
        }



        #region 显示和隐藏Outlook应用程序
        public static void HideOutLookApp(bool isShowLoadingFrom, string message)
        {
            //SetWindowsState(6, isShowLoadingFrom, message);
        }
        public static void ShowOutLookApp(bool isShowLoadingFrom, string message)
        {
            //SetWindowsState(1, false, string.Empty);
        }

        //如果outlook被打开多个窗口，或者子窗口，则需要将其全部隐藏
        public static void SetWindowsState(int flag, bool isShowLoadingFrom, string message)
        {
            int tokenID = -1;
            if (isShowLoadingFrom)
            {
                tokenID = ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm(message);
            }

            //多个outlook窗口
            Process[] arrProcess = GetProcessByName("OUTLOOK");
            if (arrProcess.Length == 1)
                Hiden(arrProcess[0], flag);
            else if (arrProcess.Length > 1)
                Hiden(Killed(arrProcess), flag);

            if (isShowLoadingFrom)
            {
                ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(tokenID);
            }
            arrProcess = null;
        }

        private static void Hiden(Process proc, int flag)
        {
            IntPtr mainWindowsHandle;

            if (!proc.HasExited)
            {
                proc.EnableRaisingEvents = true;
                proc.Exited -= OnProcessExited;
                proc.Exited += OnProcessExited;
                mainWindowsHandle = proc.MainWindowHandle;

                if (mainWindowsHandle == IntPtr.Zero)
                {
                    proc.Refresh();
                    mainWindowsHandle = FindWindow("rctrl_renwnd32", proc.MainWindowTitle);
                }
                bool isClosed = WindowsExtension.ShowWindowAsync(mainWindowsHandle, flag);
                if (!isClosed)
                {
                    proc.Refresh();
                    Thread.Sleep(proc.TotalProcessorTime.Milliseconds);
                    isClosed = WindowsExtension.ShowWindowAsync(FindWindow("rctrl_renwnd32", proc.MainWindowTitle), flag);
                    if (!isClosed)
                    {
                        proc.Refresh();
                        Thread.Sleep(500);
                        WindowsExtension.ShowWindowAsync(proc.MainWindowHandle, flag);
                    }
                }
            }
        }

        #endregion
        public const string treeViewFont = "宋体";
        /// <summary>
        /// 未读数量文件夹字体
        /// </summary>
        public static Font SpecialFont = new Font(treeViewFont, 10F, FontStyle.Bold);


        /// <summary>
        /// 普通文件夹字体
        /// </summary>
        public static Font NormalFont = new Font(treeViewFont, 10F);

        /// <summary>
        /// 正在加载节点颜色
        /// </summary>
        public static Color ShamNodeColor = Color.Gray;


        /// <summary>
        /// 文件夹普通颜色
        /// </summary>
        public static Color NormalColor = Color.Black;


        /// <summary>
        /// 显示所有邮件数量颜色
        /// </summary>
        public static Color TotalCountColor = Color.MediumSeaGreen;

        /// <summary>
        /// 未读文件夹颜色
        /// </summary>
        public static Color UnreadCountColor = Color.RoyalBlue;


        public static string[] OrderFolders = { "已发送邮件", "sent items", "草稿", "drafts", "发件箱", "outbox" };

        public static string[] InboxFolders = { "收件箱", "inbox" };
        public static string[] ClearFolders = { "已删除邮件", "deleted items", "垃圾邮件", "junk e-mail" };

        public static string[] DeleteItemsFolders = { "已删除邮件", "deleted items" };

        /// <summary>
        /// 垃圾文件夹
        /// </summary>
        public static string[] CN_JunFolders = { "所有邮件", "对话操作设置", "新闻源", "快速步骤设置", "rss 源" };
        public static string[] EN_JunFolders = { "rss feeds", "all mails", "dialogue operation settings", "news source", "quick steps set" };

        public static string[] RootPersonalFolders = { "个人文件夹", "personal folders" };
        public static string[] RootArchiveFolders = { "存档文件夹", "archive folders" };
        /// <summary>
        /// 草稿,发件箱,垃圾邮件文件夹名称数组
        /// </summary>
        public static string[] DOJFolders = { "草稿", "发件箱", "垃圾邮件", "drafts", "outbox", "junk e-mail" };
        public static string[] JunE_MailFolder = { "垃圾邮件", "junk e-mail" };
        public static string[] InBoxFolder = { "收件箱", "inbox" };
        public static string[] OutBoxFolder = { "发件箱", "outbox" };
        public static string[] DraftsFolders = { "草稿", "drafts" };
        public static string[] SentFolders = { "已发送邮件", "sent items" };
        public static string[] Calendars = { "日历", "calendar" };
        public static string[] Journals = { "日记", "journal" };
        public static string[] Tasks = { "任务", "task" };
        public static string[] Contacts = { "联系人", "contacts" };
        public static string[] SearchFolders = { "搜索文件夹", "search folders" };
        public static string[] Notes = { "便笺", "note" };

        public static string[] MonitorFolders = { "收件箱", "已删除邮件", "inbox", "deleted items" };


        public static string[] NotDisplayTitleFolders = { "已删除邮件", "已发送邮件", "deleted items", "sent items" };


        public static string[] UnreadFolders = { "收件箱", "已删除邮件", "已发送邮件", "inbox", "deleted items", "sent items" };

        public static string[] RecipientFolders = { "收件人", "Recipients" };

        public static string[] AllFolders =  { "个人文件夹", "草稿", "发件箱", "垃圾邮件", "收件箱", "已发送邮件", "已删除邮件", 
                        "personal folders", "drafts", "outbox", "junk e-mail", "inbox", "sent items", "deleted items" };

        public static string[] FixedFolders =
            {
               "搜索文件夹", "草稿", "发件箱", "垃圾邮件", "收件箱", "已发送邮件", "已删除邮件", 
                        "search folders", "drafts", "outbox", "junk e-mail", "inbox", "sent items", "deleted items"  
            };
        public static string[] EditFolders =  { "rss 源", "草稿", "发件箱", "垃圾邮件", "收件箱", "已发送邮件", "已删除邮件", "日历", "联系人", "日记","任务","便笺",
                        "note","task", "journal", "contacts", "calendar", "rss feeds", "drafts", "outbox", "junk e-mail", "inbox", "sent items", "deleted items" };



        public static String[] CN_OlFolders = { "个人文件夹", "草稿", "发件箱", "垃圾邮件", "收件箱", "已发送邮件", "已删除邮件" };

        public static String[] EN_OlFolders = { "personal folders", "drafts", "outbox", "junk e-mail", "inbox", "sent items", "deleted items" };

        public static String[] CN_OlJunkFolders = { "日历", "便笺", "联系人", "rss 源", "任务", "日记", "对话操作设置", "新闻源", "快速步骤设置" };


        public static String[] EN_OlJunkFolders = { "calendar", "note", "contacts", "rss feeds", "tasks", "journal", "dialogue operation settings", "news source", "quick steps set" };

        public static string[] JunkFolders = { "对话操作设置", "新闻源", "快速步骤设置",
                                                  "dialogue operation settings", "news source", "quick steps set" };
        /// <summary>
        /// 监听文件夹
        /// </summary>
        public static string[] CN_OlMonitorFolders = { "收件箱", "已删除邮件" };

        public static string[] EN_OlMonitorFolders = { "inbox", "deleted items" };

        /// <summary>
        /// 文件夹需要计算出所有邮件数量
        /// </summary>
        public static string[] CN_OlTotalCountFolders = { "草稿", "发件箱", "垃圾邮件" };
        public static string[] EN_OlTotalCountFolders = { "drafts", "outbox", "junk e-mail" };

        public static string[] arrCopy = { "SOCopy", "MBLCopy", "APCopy" };

        /// <summary>
        /// 根据条件创建邮件文件夹上下文菜单项
        /// </summary>
        /// <param name="image"></param>
        /// <param name="text"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ToolStripMenuItem CreateMenuItem(Image image, String text, object tag)
        {
            return new ToolStripMenuItem() { Text = text, Tag = tag, Image = image };
        }
        /// <summary>
        /// 寻找当前文件夹是否是收件箱，如果没有找到，直往上面节点找
        /// </summary>
        /// <param name="arrInbox"></param>
        /// <param name="folder"></param>
        /// <param name="isInboxFolder"></param>
        public static void IsContainsFolders(string[] arrFolders, string folderFullPath, ref bool isContainsKeyWord)
        {
            if (!string.IsNullOrEmpty(folderFullPath))
            {
                string lower_FolderFullPath = folderFullPath.ToLower();

                isContainsKeyWord = arrFolders.Any(lower_FolderFullPath.Contains);
                //foreach (string f in arrFolders)
                //{
                //    //查找树文件夹或邮件文件夹相同的文件夹名称
                //    if (lower_FolderFullPath.Contains(f))
                //    {
                //        isContainsKeyWord = true;
                //        break;
                //    }
                //}
            }
        }
        /// <summary>
        /// 判断文件夹是否属于草稿,发件箱,垃圾邮件
        /// </summary>
        /// <param name="folderFullPath"></param>
        /// <returns></returns>
        public static bool IsInDOJFolders(string folderFullPath)
        {
            string lowerFolderFullPath = folderFullPath.ToLower();
            return DOJFolders.Any(lowerFolderFullPath.Contains);
            //foreach (string f in DOJFolders)
            //{
            //    //查找树文件夹或邮件文件夹相同的文件夹名称
            //    if (lowerFolderFullPath.Contains(f))
            //    {
            //        return true;
            //    }
            //    continue;
            //}
            //return false;
        }

        public static void IsEqualsFolders(string[] arrFolders, string folderFullPath, ref bool isContainsKeyWord)
        {
            if (!string.IsNullOrEmpty(folderFullPath))
            {
                string lower_FolderFullPath = folderFullPath.ToLower();

                isContainsKeyWord = arrFolders.Any(item => lower_FolderFullPath.Equals(item));
                //foreach (string f in arrFolders)
                //{
                //    //查找树文件夹或邮件文件夹相同的文件夹名称
                //    if (lower_FolderFullPath.Equals(f))
                //    {
                //        isContainsKeyWord = true;
                //        break;
                //    }
                //}
            }
        }
        /// <summary>
        /// 找到当前文件夹下的所有文件夹
        /// </summary>
        /// <param name="olFolders"></param>
        /// <param name="folderName"></param>
        /// <param name="isContains"></param>
        public static void IsContainsFolders(Folders olFolders, string folderName, ref bool isContains)
        {
            foreach (MAPIFolder folder in olFolders)
            {
                if (folderName.Equals(folder.Name))
                {
                    isContains = true;
                    MailUtility.ReleaseComObject(folder);
                    return;
                }
                else
                {
                    IsContainsFolders(folder.Folders, folderName, ref isContains);
                }
            }
        }

        public static void IsContainsSubFolders(Folders olFolders, string folderName, ref bool isContains)
        {
            isContains = olFolders.OfType<MAPIFolder>().Any(item => item.Name.Equals(folderName));
            //foreach (MAPIFolder folder in olFolders)
            //{
            //    if (folderName.Equals(folder.Name))
            //    {
            //        isContains = true;
            //        return;
            //    }
            //    MailUtility.ReleaseComObject(folder);
            //}
        }

        public static string ValidateNewNodeName(string folderName)
        {
            string newName = string.Empty;
            if (string.IsNullOrEmpty(folderName)) { newName = "NewFolder"; }
            else
            {
                newName = folderName;
            }

            return newName;
        }
        /// <summary>
        /// 只找到当前文件夹下的文件夹
        /// </summary>
        /// <param name="olFolder"></param>
        /// <param name="folderName"></param>
        /// <param name="isContains"></param>
        public static void IsContainsFolders(MAPIFolder olFolder, string folderName, ref bool isContains)
        {

            MAPIFolder folder = FindMAPIFolderbyName(olFolder, folderName);
            if (folder == null)
                isContains = false;
            else
                isContains = true;
        }
        /// <summary>
        ///根据文件夹名称查找Outlook文件夹
        /// </summary>
        /// <param name="olFolder"></param>
        /// <param name="folderName"></param>
        /// <param name="isContains"></param>
        /// <returns></returns>
        public static MAPIFolder FindMAPIFolderbyName(MAPIFolder olFolder, string folderNamw)
        {

            return olFolder.Folders.OfType<MAPIFolder>().SingleOrDefault(item => item.Name.Equals(folderNamw));
            //foreach (MAPIFolder folder in olFolder.Folders)
            //{
            //    if (folderNamw.Equals(folder.Name))
            //    {
            //        return folder;
            //    }
            //    MailUtility.ReleaseComObject(folder);
            //}

            // return null;
        }
        /// <summary>
        /// 空白图片
        /// </summary>
        public static Image _NullImage = null;
        public static Image NullImage
        {
            get
            {
                return _NullImage ?? (_NullImage = (new Bitmap(1, 1)));
            }
        }

        /// <summary>
        /// 启动文件应用程序
        /// </summary>
        /// <param name="file"></param>
        public static void StartFileApplication(String file)
        {
            if (File.Exists(file))
            {
                Process proc = null;
                try
                {
                    proc = System.Diagnostics.Process.Start(file);
                }
                catch
                {
                    proc = System.Diagnostics.Process.Start("rundll32.exe", @"shell32,OpenAs_RunDLL " + file + "");
                }
                proc.Dispose();
            }
            else
            {
                //throw new ICPException(isEnglish ? "the file does not exsit." : "该文件不存在.");
            }
        }

        /// <summary>
        /// 检查字符串是不是包含中文字符
        /// </summary>
        /// <param name="srcString"></param>
        /// <returns></returns>
        public static bool CheckEncode(string srcString)
        {
            int strLen = srcString.Length;
            //字符串的长度，一个字母和汉字都算一个 
            int bytLeng = System.Text.Encoding.UTF8.GetBytes(srcString).Length;
            //字符串的字节数，字母占1位，汉字占2位,注意，一定要UTF8 
            bool chkResult = false;
            if (strLen < bytLeng)  //如果字符串的长度比字符串的字节数小，当然就是其中有汉字啦^-^ 
            {
                chkResult = true;
            }
            return chkResult;
        }

        public static void KillProcess(string processName)
        {
            Killed(GetProcessByName(processName));
        }

        /// <summary>
        /// 将两个数组合并成 一个数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static T[] Merge<T>(T[] arr, T[] other)
        {
            T[] buffer = new T[arr.Length + other.Length];
            arr.CopyTo(buffer, 0);
            other.CopyTo(buffer, arr.Length);
            return buffer;
        }
        public static T DeepClone<T>(T obj)
        {
            T objResult;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                ms.Position = 0;
                objResult = (T)bf.Deserialize(ms);
            }
            return objResult;
        }
        /// <summary>
        /// 将字符串分割成数组(过滤空值)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="charMark"></param>
        /// <returns></returns>
        public static List<String> SplitStringToList(String text, char[] charMark)
        {
            List<String> list = new List<String>();

            if (!String.IsNullOrEmpty(text))
            {
                String[] arrText = text.Split(charMark);
                list = arrText.Where(o => !string.IsNullOrEmpty(o)).ToList();
            }

            return list;
        }

        public static string[] SplitStringToArray(string text, char[] charMark)
        {
            string[] arrText = null;
            if (!String.IsNullOrEmpty(text))
            {
                arrText = text.Split(charMark);
            }

            return arrText;
        }

        /// <summary>
        /// 将数组转换成字符串
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static string ConvertStringArrayToString(string separator, string[] array)
        {
            if (array == null)
                return string.Empty;
            return string.Join(separator, array);
        }
        /// <summary>
        /// 将guid字符串分割成泛型集合(过滤空值)
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="charMark"></param>
        /// <returns></returns>
        public static List<Guid> SplitGuidToList(String guid, char[] charMark)
        {
            List<Guid> list = new List<Guid>();

            if (!String.IsNullOrEmpty(guid))
            {
                String[] arrText = guid.Split(charMark);
                foreach (var arr in arrText)
                {
                    if (!String.IsNullOrEmpty(arr))
                        list.Add(new Guid(arr));
                }
            }

            return list;
        }

        /// <summary>
        /// 终止线程
        /// </summary>
        /// <param name="thread"></param>        
        [SecurityPermissionAttribute(SecurityAction.Demand, ControlThread = true)]
        public static void AbortThread(Thread thread)
        {
            if (thread != null)
            {
                try
                {
                    if (thread != null && thread.IsAlive)
                    {
                        thread.Abort();
                    }
                }
                catch (ThreadInterruptedException ex) { }
            }
        }

        public static Byte[] ReadFileContentFromDisk(String filePath)
        {
            if (!File.Exists(filePath)) { return new byte[0]; }
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                byte[] content = new byte[(int)fs.Length];
                fs.Read(content, 0, content.Length);
                return content;
            }

        }

        /// <summary>
        /// 获取拖拽文件路径
        /// </summary>
        /// <param name="objFile"></param>
        /// <returns></returns>
        public static String GetFilePath(object objFile)
        {
            String fullFilePath = String.Empty;
            if (objFile != null)
            {
                if (objFile.GetType() == typeof(String))
                {
                    fullFilePath = Path.Combine(Path.GetTempPath(), objFile.ToString());
                }
                if (objFile.GetType() == typeof(String[]))
                {
                    String[] files = objFile as String[];
                    if (files != null && files.Length > 0)
                    {
                        fullFilePath = files[0];
                    }
                }
            }
            return fullFilePath;
        }

        /// <summary>
        /// 加载节点值
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="nodeName"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        public static String LoadTemplateFromXml(String xmlPath, String parentNodeName, String nodeName)
        {
            String value = String.Empty;
            if (File.Exists(xmlPath))
            {
                XDocument doc = XDocument.Load(xmlPath);
                if (!String.IsNullOrEmpty(parentNodeName))
                {
                    IEnumerable<String> query;
                    if (!String.IsNullOrEmpty(nodeName) && nodeName != "Unknown")
                    {
                        query = from node in doc.Descendants(parentNodeName)
                                select node.Element(nodeName) != null ? node.Element(nodeName).Value : String.Empty;
                    }
                    else
                    {
                        query = from node in doc.Descendants(parentNodeName)
                                select node.Value;
                    }
                    value = query.SingleOrDefault().ToString();
                }
            }

            return value;
        }

        /// <summary>
        /// 保存或者更改邮件模板
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="nodeName"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool UpdateAndSaveXmlTemplate(String xmlPath, String nodeName, String text)
        {
            bool SaveState = false;
            if (File.Exists(xmlPath))
            {
                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlPath);
                    XmlNodeList NodeList = xmlDoc.GetElementsByTagName(nodeName);
                    if (NodeList != null && NodeList.Count > 0)  //存在节点就赋值
                    {
                        NodeList[0].InnerText = text;
                        SaveState = true;
                    }
                    else //不存在就新增，后赋值
                    {
                        XmlElement element = xmlDoc.CreateElement(nodeName);
                        element.InnerText = text;
                        xmlDoc.DocumentElement.AppendChild(element);
                        SaveState = true;
                    }
                    xmlDoc.Save(xmlPath);
                }
                catch (System.Exception ex)
                {
                    SaveState = false;
                }
            }
            else { SaveState = false; }

            return SaveState;
        }

        /// <summary>
        /// 将字段以"|"分割成一个字符串
        /// </summary>
        /// <param name="arrFields"></param>
        /// <param name="separator">分隔符,如果为空则取'|'字符</param>
        /// <returns></returns>
        public static String ToString(String[] arrFields, char separator)
        {
            if (arrFields == null || arrFields.Length <= 0)
                return string.Empty;
            //设置默认‘|’分割
            if (separator == null)
            {
                separator = '|';
            }
            return arrFields.Aggregate((a, b) => a + separator + b);
        }






        public static bool IsChineseLetter(string letter)
        {
            bool flag = false;
            for (int i = 0; i < letter.Length; i++)
            {
                String temp = letter.Substring(i, 1);
                byte[] sarr = System.Text.Encoding.GetEncoding("gb2312").GetBytes(temp);
                if (sarr.Length == 2)
                    flag = true;
                else
                    flag = false;
            }
            return flag;
        }
        /// <summary>
        /// 以指定路径保存MailItem对象到磁盘MSG文件
        /// </summary>
        /// <param name="mailItem"></param>
        /// <param name="savePath"></param>
        public static void SaveMailItemAs(object mailItem, string savePath)
        {
            MailItem _mailItem = mailItem as MailItem;
            if (_mailItem != null)
            {
                _mailItem.SaveAs(savePath, OlSaveAsType.olMSG);
                //_mailItem.Close(OlInspectorClose.olDiscard);
            }
        }
    }
}
