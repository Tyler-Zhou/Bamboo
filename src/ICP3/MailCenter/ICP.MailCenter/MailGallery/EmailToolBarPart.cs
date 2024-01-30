using System;
using System.ComponentModel;
using System.Timers;
using System.Windows.Forms;
using ICP.MailCenter.ServiceInterface;
using Microsoft.Practices.CompositeUI.Commands;
using outlook = Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Core;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.MailCenter.UI.MailGallery;
using Microsoft.Practices.ObjectBuilder;
using ICP.MailCenter.UI.UC;
using ICP.Framework.CommonLibrary.Common;
using MailContactType = ICP.MailCenter.ServiceInterface.ContactType;


namespace ICP.MailCenter.UI
{
    /// <summary>
    /// 工具栏类
    /// </summary>
    /// 
    [ToolboxItem(false)]
    [SmartPart]
    public partial class EmailToolBarPart : System.Windows.Forms.UserControl
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        #region Service
        /// <summary>
        /// 根WorkItem
        /// </summary>
        public WorkItem RootWorkItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public CommonDataService CommonDataService { get; set; }

        IOutLookService _outlookService;
        public IOutLookService outLookService
        {
            get
            {
                if (_outlookService == null)
                    _outlookService = ServiceClient.GetClientService<IOutLookService>();

                return _outlookService;
            }
        }
        private Microsoft.Practices.CompositeUI.Collections.ManagedObjectCollection<object> _SmartParts;
        public Microsoft.Practices.CompositeUI.Collections.ManagedObjectCollection<object> SmartParts
        {
            get
            {
                if (_SmartParts == null)
                    _SmartParts = this.RootWorkItem.SmartParts;
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
        public EmailListPart ListPart
        {
            get
            {
                return SmartParts.Get<EmailListPart>(MailCenterWorkSpaceConstants.EmailListPart);
            }
        }

        #endregion

        #region const or target

        public System.Timers.Timer timeReminder = null;
        private System.Timers.Timer mailReadTimer = null;
        IntPtr parenthWnd = IntPtr.Zero;
        #endregion

        #region public Click

        #endregion

        #region 初始化
        public EmailToolBarPart()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Disposed += delegate { DisposedCompent(); };
            InitControl();
        }

        private void DisposedCompent()
        {
            _SmartParts = null;
            timeReminder.Stop();
            timeReminder.Dispose();
            timeReminder = null;
            mailReadTimer.Stop();
            mailReadTimer.Dispose();
            mailReadTimer = null;
            if (RootWorkItem != null) this.RootWorkItem.Items.Remove(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
            {
                RegisterEvents();
                timeReminder = new System.Timers.Timer(TimerManager.ReceivedMailTimerInterval);
                timeReminder.Elapsed += new System.Timers.ElapsedEventHandler(timeReminder_Elapsed);
                timeReminder.Enabled = false; //是否现在执行Elapsed事件
                timeReminder.AutoReset = true; //是否定时执行

                //timeReminder.Start();

                ClientProperties.MailReadTimeInterval = TimerManager.MailReadTimerInterval;
                mailReadTimer = new System.Timers.Timer(ClientProperties.MailReadTimeInterval);
                mailReadTimer.Elapsed += new ElapsedEventHandler(mailReadTimer_Elapsed);//更新树文件夹数量
                mailReadTimer.Enabled = false;    //是否现在执行Elapsed事件             
                mailReadTimer.AutoReset = false; //是否定时执行
            }
        }

        void RegisterEvents()
        {
            IMainForm form = ServiceClient.GetClientService<IMainForm>();
            form.ApplicationExit += new EventHandler(MailFormClosed);
        }

        void InitControl()
        {
            if (!LocalData.IsEnglish)
            {
                toolNewMail.Text = "新建";
                toolNewMail.ToolTipText = "新建(Ctrl+N)";
                toolReply.Text = "答复";
                toolReply.ToolTipText = "答复(Ctrl+R)";
                toolReplyAll.Text = "全部答复";
                toolReplyAll.ToolTipText = "全部答复(Ctrl+Shift+R)";
                toolReplyAllContainsAttachment.Text = "全部答复(包含附件)";
                toolReplyAllContainsAttachment.ToolTipText = "全部答复(包含附件)(Ctrl+Shift+A)";
                toolForward.Text = "转发";
                toolForward.ToolTipText = "转发(Ctrl+F)";
                toolSearch.Text = "查找";
                toolSearch.ToolTipText = "查找(Ctrl+Shift+F)";
                toolRefersh.Text = "刷新文件夹列表";
                toolRefershBar.ToolTipText = "刷新";
                toolEmailArchiving.Text = "邮件归档";
                toolSBSearch.ToolTipText = "通讯簿";
                syncAddressToolStripMenuItem.Text = "同步ICP通讯簿";
                toolSendAndReceive.ToolTipText = toolSendAndReceive.Text = "发送/接收";
                toolRules.ToolTipText = toolRules.ToolTipText = "规则和通知";
                toolReminder.ToolTipText = "接收/发送时间";
                toolClose.Text = toolClose.ToolTipText = "关闭";
                toolPrintPreview.Text = toolPrintPreview.ToolTipText = "打印预览";
                toolDeleteMail.ToolTipText = "删除";
                toolMailReadTime.Text = "邮件已读时间";
                toolMailReadTime.ToolTipText = "将邮件设置已读时间段";
                toolSetting.Text = "设置";
                toolSetting.ToolTipText = "";
                toolReminder.Text = "发送/接收";
                toolReminder.ToolTipText = "设置发送和接收时间";
                toolRules.Text = "规则/通知";
                toolRules.ToolTipText = "规则和通知";
            }
        }

        #endregion

        public void MailFormClosed(object sender, EventArgs e)
        {
            do
            {
                WindowsExtension.PostMessage(parenthWnd, SWP.WM_CLOSE, 0, 0);
            }
            while (GetSearchFormHandle() != IntPtr.Zero);
        }

        /// <summary>
        /// 获取搜索窗口句柄
        /// </summary>
        /// <returns></returns>
        private IntPtr GetSearchFormHandle()
        {
            string title = ClientUtility.olCultrue == "zh-cn" ? "高级查找" : "Advanced Find";
            return parenthWnd = FindWindow("rctrl_renwnd32", title);
        }

        #region Click

        private void toolSynchronFolders_Click(object sender, EventArgs e)
        {
            RootWorkItem.Commands[MailCenterWorkSpaceConstants.Command_SynchronFolders].Execute();
        }

        private void toolRefersh_Click(object sender, EventArgs e)
        {
            ClientProperties.IsRefershFolders = false;
            RootWorkItem.Commands[MailCenterWorkSpaceConstants.Command_RefershFolderList].Execute();
        }

        /// <summary>
        /// 邮件归档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolEmailArchiving_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("功能未实现!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            RootWorkItem.Commands["Command_EmailArchiving"].Execute();
        }

        private void toolPrintPreview_Click(object sender, EventArgs e)
        {
            OpenPrintForm();
        }

        void OpenPrintForm()
        {
            System.Threading.Timer timer = null;
            string title = ClientUtility.olCultrue == "zh-cn" ? "打印" : "Print";
            IntPtr parenthWnd = FindWindow("#32770", title);
            if (parenthWnd == IntPtr.Zero)
            {
                if (Process.GetProcessesByName("outlook").Length == 0)
                {
                    try
                    {
                        MailUtility.StartProcess(false, string.Empty);
                        ClientUtility.GetOutlookNewNameSpace();
                        MailListPresenter.BindViewCtlData(ListPart.axViewCtlEmailList, ClientProperties.folder_FullPath,
                                                          string.Empty);
                    }
                    catch
                    {
                        ClientUtility.GetOutlookNewNameSpace();
                        MailListPresenter.BindViewCtlData(ListPart.axViewCtlEmailList, ClientProperties.folder_FullPath, string.Empty);
                    }
                    finally
                    {
                        try
                        {
                            timer = new System.Threading.Timer(CrossCheckOpenedPrintItem, title, (long)220, 0);
                            ListPart.axViewCtlEmailList.PrintItem();
                        }
                        catch { }
                        finally
                        {
                            TreeViewPresenter.RegisterAllExpandedNodes(FolderPart.trvFolder, true);
                        }
                    }
                }
                else
                {
                    try
                    {
                        ClientUtility.GetOutlookNewNameSpace();
                        timer = new System.Threading.Timer(CrossCheckOpenedPrintItem, title, (long)220, 0);
                        ListPart.axViewCtlEmailList.PrintItem();
                    }
                    catch (System.Exception ex)
                    {
                        if (ex.Message.Contains("发生意外") || ex.Message.Contains("accident occurred"))
                        {
                            string message = LocalData.IsEnglish ? "The printer is not available. The printer is not installed. You can select and configure a printer in Windows control panel." : "打印机不可用。未安装打印机。您可以在Windows 控制面板中选择和配置打印机。";
                            MessageBox.Show(message, "Microsoft Ofifce Outlook", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                        }
                    }
                }
            }
        }

        private void CrossCheckOpenedPrintItem(object title)
        {
            IntPtr parenthWnd = FindWindow("#32770", title.ToString());
            if (parenthWnd == IntPtr.Zero)
            {
                try
                {
                    _MailItem olItem = RootWorkItem.State[MailCenterCommandConstants.CurrentSelection] as MailItem;
                    if (olItem != null)
                    {
                        olItem.PrintOut();
                    }
                    else
                    {
                        _ReportItem olReportItem =
                            RootWorkItem.State[MailCenterCommandConstants.CurrentSelection] as ReportItem;
                        if (olReportItem != null)
                        {
                            olReportItem.PrintOut();
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                }
            }
        }

        private void toolNewMail_Click(object sender, EventArgs e)
        {
            RootWorkItem.Commands[MailCenterCommandConstants.Command_AddNewMail].Execute();
        }

        private void toolReply_Click(object sender, EventArgs e)
        {
            RootWorkItem.Commands[MailCenterCommandConstants.Command_ReplyMail].Execute();
        }

        private void toolReplyAll_Click(object sender, EventArgs e)
        {
            RootWorkItem.Commands[MailCenterCommandConstants.Command_ReplyALL].Execute();
        }
        /// <summary>
        /// 答复所有(包含附件)
        /// </summary>
        private void toolReplyAllContainsAttachment_Click(object sender, EventArgs e)
        {
            RootWorkItem.Commands["Command_ReplyALLContainsAttachment"].Execute();
        }

        private void toolForward_Click(object sender, EventArgs e)
        {
            RootWorkItem.Commands[MailCenterCommandConstants.Command_Forward].Execute();
        }
        /// <summary>
        /// 查询
        /// </summary>
        private void toolSearch_Click(object sender, EventArgs e)
        {
            //如果没有找到Advanced find 窗体，就需要打开该窗体
            using (new CursorHelper(Cursors.WaitCursor))
            {
                MailListPresenter.OpenAdvancedSearchPart(MailContactType.SendingReceivingByContact, string.Empty);
            }
        }
        /// <summary>
        /// 发送和接收邮件
        /// </summary>
        private void toolSendAndReceiveSendAndReceive_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                ClientProperties.UnReadCount = 0;
                ClientProperties.isDragDropMail = false;
                timeReminder.Stop();
                SendAndReceive(true);//发送和接收邮件
                timeReminder.Start();
                SetWindowText();
            }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        private void toolClose_Click(object sender, EventArgs e)
        {
            ClientProperties.IsNeedDeleteConfigFile = false;
            try
            {
                RootWorkItem.Commands[MailCenterCommandConstants.Command_CloseMainForm].Execute();
            }
            catch { }
        }

        void SendAndReceive(bool isShowProcessDailog)
        {
            SendAndRecive(isShowProcessDailog);
            Invalidate();
            //ValidateUseTime();
        }

        //public void ValidateUseTime()
        //{
        //    string useTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).ToShortDateString();
        //    DateTime.TryParse(useTime, out ClientProperties.EndTime);
        //    if (ClientProperties.EndTime.Day - ClientProperties.BeginTime.Day > 0)
        //    {
        //        try
        //        {
        //            ServiceClient.GetService<ICP.MailCenter.Business.ServiceInterface.IMailCenterOperationService>()
        //                         .SaveOperationMemo(LocalData.UserInfo.UserName, ClientProperties.EndTime,
        //                                            ClientProperties.EndTime.Day - ClientProperties.BeginTime.Day);
        //            ClientProperties.BeginTime = ClientProperties.EndTime;
        //        }
        //        catch (System.Exception ex) { }
        //    }
        //}


        public void Invalidate()
        {
            if (ListPart.InvokeRequired)
            {
                ListPart.Invoke((System.Action)(() => ListPart.Refresh()));
            }
            else
                ListPart.Refresh();
        }


        public void SetWindowText()
        {
            if (ClientProperties.UnReadCount == 0)
            {
                if (string.IsNullOrEmpty(ClientProperties.CurrentFolderName))
                {
                    MailListPresenter.SetMailCenterText(this, (LocalData.IsEnglish ? "Mail Center" : "邮件中心"), true);
                }
                else
                {
                    TreeNode node = FolderPart.trvFolder.SelectedNode;
                    MailListPresenter.SetMailCenterText(this, (string.Format("{0}{1}", node == null ? string.Empty : node.Text, LocalData.IsEnglish ? "  -  Mail Center" : "  -  邮件中心")), true);
                }
            }
        }

        private void toolStripSplitButton1_Click(object sender, EventArgs e)
        {
            ////outlook2003
            //if (Convert.ToInt32(ClientUtility.OlApp.Version.Substring(0, 2)) == 11)
            //{
            //    //chinese version
            //    if (olCulture == "zh-cn")
            //        AddressBook("通讯簿(&B)...");
            //    else
            //        //english version
            //        AddressBook("Address &Book...");
            //}
            ListPart.axViewCtlEmailList.Refresh();
            ListPart.axViewCtlEmailList.AddressBook();
        }

        /// <summary>
        /// 同步IPC通讯录
        /// </summary>
        private void syncAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                //MessageBox.Show("功能未实现!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FrmSynchronAddressBook frmsab = new FrmSynchronAddressBook();
                frmsab.ShowDialog();
                frmsab.Dispose();
            }
        }

        private void toolMailReadTime_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OpenSetTimeForm(TimeType.MailRead, TimerManager.MailReadTime);
            }
        }

        void timeReminder_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            AutoSync();
        }

        public void ActiveAutoSync()
        {
            Thread.Sleep(1000);
            AutoSync();
        }

        /// <summary>
        /// 自动接收和发送邮件
        /// </summary>
        private void AutoSync()
        {
            ClientProperties.UnReadCount = 0;
            ClientProperties.isDragDropMail = false;
            //asynchronous refersh send/recive function
            SendAndReceive(false);
            SetWindowText();
        }

        private void toolRules_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                //outlook2003
                if (ClientUtility.olVersion > 10 && ClientUtility.olVersion < 14)
                {
                    //chinese version
                    if (ClientUtility.olCultrue == "zh-cn")
                        RulesAndAlertsToolBar("工具(&T)", "规则和通知(&L)...");
                    else
                        //english version
                        RulesAndAlertsToolBar("Tools", "Rules and Alerts...");
                }
                //outlook2007/2010
                else
                {
                    //chinese version
                    if (ClientUtility.olCultrue == "zh-cn")
                        RulesAndAlertsToolBar("工具(&T)", "规则和通知​​(&L)...");
                    else
                        //english version
                        RulesAndAlertsToolBar("Tools", "Ru&les and Alerts...");
                    //ExecuteManagerRule();
                }
            }
        }

        /// <summary>
        /// 设置接收/发送时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolReminder_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OpenSetTimeForm(TimeType.ReceiveMail, TimerManager.ReveiveTimer);
            }
        }

        private void toolDeleteMail_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Delete(true);
            }
        }
        [CommandHandler("Command_DeleteSelectedMail")]
        public void Command_DeleteSelectedMail(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Delete(false);
            }
        }

        private void Delete(bool showDailog)
        {
            Selection olSelection = ListPart.axViewCtlEmailList.Selection as Selection;
            if (olSelection.Count > 0)
            {
                if (!ConfirmDeteleSelectedMail(showDailog))
                    return;


                WaitCallback callback = (obj) =>
                {

                    ClientProperties.DragDropUnreadMail.Clear();
                    ListPart.axViewCtlEmailList.Invoke((System.Action)delegate
                    {
                        Selection tempSelection = null;
                        try
                        {
                            tempSelection = obj as Selection;
                            int totalCount = tempSelection.Count;
                            for (int i = 1; i <= totalCount; i++)
                            {
                                Deleted(tempSelection[i]);
                                if (i == totalCount)
                                {
                                    //object objEntryID =
                                    //    this.RootWorkItem.State[MailCenterCommandConstants.CurrentEntryID];
                                    //MAPIFolder originalFolder = null;
                                    //if (objEntryID == null || objEntryID == "")
                                    //    originalFolder = MailListPresenter.GetDefaultInboxFolder();
                                    //else
                                    //    originalFolder = MailListPresenter.GetFolderByEntryID(objEntryID.ToString());

                                    FolderPart.RefershFolderFlagCount(MailListPresenter.GetDeleteFolder(), true);
                                    ClientProperties.DragDropUnreadMail.Clear();
                                }
                            }
                        }
                        catch (System.Exception ex)
                        {
                            ClientProperties.DragDropUnreadMail.Clear();
                            LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                        }
                        finally
                        {
                            MailUtility.ReleaseComObject(tempSelection);
                        }
                    });
                };
                ThreadPool.QueueUserWorkItem(callback, olSelection);
            }

        }

        private bool ConfirmDeteleSelectedMail(bool showDialog)
        {
            if (showDialog)
            {
                DialogResult result =
                MessageBox.Show(LocalData.IsEnglish ? "Sure to delete the selected item?" : "确定要删除所选的项目?", "Microsoft Office Outlook",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                return (result == DialogResult.Yes);
            }
            return true;
        }

        private void OpenSetTimeForm(TimeType type, string key)
        {
            string time = TimerManager.GetConfigNodeValue(key);
            if (string.IsNullOrEmpty(time)) { time = "5"; }
            frmSetTime form = new frmSetTime(type, time);
            form.QueryReminder += OnQueryReminder;
            form.ShowDialog();
            form.Dispose();
        }

        public void OnQueryReminder(object sender, CommonEventArgs<TimeInfo> result)
        {
            SetTimer(result.Data);
        }

        public void SetTimer(TimeInfo info)
        {
            if (info != null)
            {
                if (info.TimeType == TimeType.ReceiveMail)
                {
                    timeReminder.Stop();
                    timeReminder.Interval = TimerManager.ReceivedMailTimerInterval;
                    timeReminder.Start();
                }
                else
                {
                    mailReadTimer.Stop();
                    mailReadTimer.Interval = TimerManager.MailReadTimerInterval;
                }
            }
        }
        #endregion

        #region Method

        void ClearIECache()
        {
            //IECache.ClearBrowserCache();
        }

        /// <summary>
        /// send and recive mail
        /// </summary>
        public void SendAndRecive(bool isShowProcessDialog)
        {
            // outlook2003 version
            if (ClientUtility.olVersion > 10 && ClientUtility.olVersion < 12)
            {
                // outlook Chinese version
                if (ClientUtility.olCultrue == "zh-cn")
                    SendAndReciveBar("工具(&T)", "发送和接收(&E)", "全部发送/接收(&A)");
                // outlook English version
                else
                    SendAndReciveBar("Tools", "Send/Receive", "Send/Receive All");
            }
            // outlook2007/2010 version
            else
            {
                try
                {
                    ClientUtility.OlNS.SendAndReceive(isShowProcessDialog);
                }
                catch (System.Exception ex)
                {
                    try
                    {
                        ClientUtility.CreateOutlookNameSpaceInstance().SendAndReceive(isShowProcessDialog);
                    }
                    catch (System.Exception e)
                    {
                        try
                        {
                            MailUtility.StartProcess(false, string.Empty);
                            ClientUtility.CreateOutlookNameSpaceInstance().SendAndReceive(isShowProcessDialog);
                        }
                        catch
                        {
                            ICP.Framework.CommonLibrary.Logger.Log.Error(
                                ICP.Framework.CommonLibrary.Common.CommonHelper.BuildExceptionString(ex));
                        }
                        finally
                        {
                            TreeViewPresenter.RegisterAllExpandedNodes(FolderPart.trvFolder, true);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Sync send/recive mail 
        /// </summary>
        private void AlternativeWay()
        {
            outlook.NameSpace ns = null;
            outlook.SyncObjects syncObjs = null;
            outlook.SyncObject syncObj = null;
            try
            {
                ns = ClientUtility.OlApp.GetNamespace("MAPI");
                ns.Logon(null, null, Type.Missing, Type.Missing);
                syncObjs = ns.SyncObjects;

                for (int i = 1; i <= syncObjs.Count; i++)
                {
                    syncObj = syncObjs[i];
                    if (syncObj != null)
                    {
                        syncObj.Start();
                        MailUtility.ReleaseComObject(syncObj);
                    }
                }
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "Send and Recive failure: " : "发送/接收失败: ") + ex.Message);
                return;
            }
            finally
            {
                MailUtility.ReleaseComObject(syncObjs);
                MailUtility.ReleaseComObject(ns);
                //ns.Logoff();
            }
        }

        /// <summary>
        /// send/recive mail
        /// </summary>
        /// <param name="cbpTools"></param>
        /// <param name="cbpSendAndRecive"></param>
        /// <param name="cbcSendAndRecive"></param>
        public void SendAndReciveBar(String cbpTools, String cbpSendAndRecive, String cbcSendAndRecive)
        {
            // Get Explorer for the folder.
            outlook._Explorer oExp = null;
            CommandBars oCmdBars = null;
            NameSpace OutlookNameSpace = null;
            MAPIFolder OutlookFolder = null;
            Explorer OutlookExplorer = null;

            try
            {
                oExp = ClientUtility.OlApp.ActiveExplorer();
                if (oExp == null)
                {
                    OutlookNameSpace = ClientUtility.OlApp.GetNamespace("MAPI");
                    OutlookFolder = OutlookNameSpace.GetDefaultFolder(OlDefaultFolders.olFolderInbox);
                    OutlookExplorer = OutlookFolder.GetExplorer(false);
                    // Get the Menu bar.
                    oCmdBars = OutlookExplorer.CommandBars;
                }
                else
                    // Get the Menu bar.
                    oCmdBars = oExp.CommandBars;
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "Send and Recive failure: " : "发送/接收失败: ") + ex.Message);
                return;
            }

            // Get the Menu bar.

            CommandBar oCmdBar = oCmdBars["Menu Bar"];

            CommandBarControls oBarCrls = oCmdBar.Controls;

            // Get the Tools menu.
            CommandBarPopup oBPop = (CommandBarPopup)oBarCrls[cbpTools];

            oBarCrls = oBPop.Controls;

            // Get the Send/Receive menu.
            CommandBarPopup oSendReceive = (CommandBarPopup)oBarCrls[cbpSendAndRecive];

            // Get the Send and Receive All menu.
            oBarCrls = oSendReceive.Controls;

            CommandBarControl oSendReceiveAll = null;
            try
            {
                //TO DO: If you use the Microsoft Outlook 11.0 Object Library, uncomment the following line.
                oSendReceiveAll = (CommandBarControl)oBarCrls[cbcSendAndRecive];
                Console.WriteLine(oSendReceiveAll.Caption);
                // Do the action.
                oSendReceiveAll.Execute();
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "Send and Recive failure: " : "发送/接收失败: ") + ex.Message);
            }
            finally
            {
                if (OutlookNameSpace != null) Marshal.ReleaseComObject(OutlookNameSpace);
                if (OutlookFolder != null) Marshal.ReleaseComObject(OutlookFolder);
                if (OutlookExplorer != null) Marshal.ReleaseComObject(OutlookExplorer);
                if (oExp != null) Marshal.ReleaseComObject(oExp);
                if (oCmdBars != null) Marshal.ReleaseComObject(oCmdBars);
                if (oCmdBar != null) Marshal.ReleaseComObject(oCmdBar);
                if (oBarCrls != null) Marshal.ReleaseComObject(oBarCrls);
                if (oBPop != null) Marshal.ReleaseComObject(oBPop);
                if (oSendReceive != null) Marshal.ReleaseComObject(oSendReceive);
                if (oSendReceiveAll != null) Marshal.ReleaseComObject(oSendReceiveAll);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        /// <summary>
        /// Rules and Alerts 
        /// </summary>
        /// <param name="cbpTool"></param>
        /// <param name="cbcRulesAndNotice"></param>
        public void RulesAndAlertsToolBar(String cbpTool, String cbcRulesAndNotice)
        {
            // Get Explorer for the folder.
            outlook._Explorer oExp = null;
            _CommandBars oCmdBars = null;
            NameSpace OutlookNameSpace = null;
            MAPIFolder OutlookFolder = null;
            Explorer OutlookExplorer = null;

            try
            {
                oExp = ClientUtility.OlApp.ActiveExplorer();
                if (oExp == null)
                {
                    OutlookNameSpace = ClientUtility.OlApp.GetNamespace("MAPI");
                    OutlookFolder = OutlookNameSpace.GetDefaultFolder(OlDefaultFolders.olFolderInbox);
                    OutlookExplorer = OutlookFolder.GetExplorer(false);
                    // Get the Menu bar.
                    oCmdBars = OutlookExplorer.CommandBars;
                }
                else
                    // Get the Menu bar.
                    oCmdBars = oExp.CommandBars;
            }
            catch (System.Exception ex)
            {

                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "open Rules and Alerts failure: " : "打开规则/通知失败: ") + ex.Message);
                return;
            }

            CommandBar oCmdBar = oCmdBars["Menu Bar"];

            CommandBarControls oBarCrls = oCmdBar.Controls;

            // Get the Tools menu.
            CommandBarPopup oBPop = (CommandBarPopup)oBarCrls[cbpTool];

            oBarCrls = oBPop.Controls;


            try
            {
                //TO DO: If you use the Microsoft Outlook 11.0 Object Library, uncomment the following line.
                CommandBarControl oRulesAndNotices = (CommandBarControl)oBarCrls[cbcRulesAndNotice];
                Console.WriteLine(oRulesAndNotices.Caption);

                // Do the action.
                oRulesAndNotices.Execute();

                if (oRulesAndNotices != null) Marshal.ReleaseComObject(oRulesAndNotices);
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "open Rules and Alerts failure: " : "打开规则/通知失败: ") + ex.Message);
            }
            finally
            {
                if (OutlookNameSpace != null) Marshal.ReleaseComObject(OutlookNameSpace);
                if (OutlookFolder != null) Marshal.ReleaseComObject(OutlookFolder);
                if (OutlookExplorer != null) Marshal.ReleaseComObject(OutlookExplorer);
                if (oExp != null) Marshal.ReleaseComObject(oExp);
                if (oCmdBars != null) Marshal.ReleaseComObject(oCmdBars);
                if (oCmdBar != null) Marshal.ReleaseComObject(oCmdBar);
                if (oBarCrls != null) Marshal.ReleaseComObject(oBarCrls);
                if (oBPop != null) Marshal.ReleaseComObject(oBPop);
            }
        }

        /// <summary>
        /// Address book
        /// </summary>
        /// <param name="addressBook"></param>
        public void AddressBook(String addressBook)
        {
            // Get Explorer for the folder.
            outlook._Explorer oExp = null;
            _CommandBars oCmdBars = null;
            NameSpace OutlookNameSpace = null;
            MAPIFolder OutlookFolder = null;
            Explorer OutlookExplorer = null;

            try
            {
                oExp = ClientUtility.OlApp.ActiveExplorer();
                if (oExp == null)
                {
                    OutlookNameSpace = ClientUtility.OlApp.GetNamespace("MAPI");
                    OutlookFolder = OutlookNameSpace.GetDefaultFolder(OlDefaultFolders.olFolderInbox);
                    OutlookExplorer = OutlookFolder.GetExplorer(false);
                    // Get the Menu bar.
                    oCmdBars = OutlookExplorer.CommandBars;
                }
                else
                    // Get the Menu bar.
                    oCmdBars = oExp.CommandBars;
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "open Address Book failure: " : "打开通讯簿失败: ") + ex.Message);
                return;
            }

            CommandBar oCmdBar = oCmdBars["Standard"];

            CommandBarControls oBarCrls = oCmdBar.Controls;

            CommandBarControl oAddressBook = null;
            try
            {
                oAddressBook = (CommandBarControl)oBarCrls[addressBook];
                Console.WriteLine(oAddressBook.Caption);

                oAddressBook.Execute();
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "open Address Book failure: " : "打开通讯簿失败: ") + ex.Message);
            }
            finally
            {
                if (OutlookNameSpace != null) Marshal.ReleaseComObject(OutlookNameSpace);
                if (OutlookFolder != null) Marshal.ReleaseComObject(OutlookFolder);
                if (OutlookExplorer != null) Marshal.ReleaseComObject(OutlookExplorer);
                if (oExp != null) Marshal.ReleaseComObject(oExp);
                if (oCmdBars != null) Marshal.ReleaseComObject(oCmdBars);
                if (oCmdBar != null) Marshal.ReleaseComObject(oCmdBar);
                if (oBarCrls != null) Marshal.ReleaseComObject(oBarCrls);
                if (oAddressBook != null) Marshal.ReleaseComObject(oAddressBook);
            }
        }
        /// <summary>
        ///  Tool bars enable
        /// </summary>       
        public void SetToolsEnable(bool enable)
        {
            toolDeleteMail.Enabled = toolReply.Enabled = toolForward.Enabled =
           toolReplyAll.Enabled = toolReplyAllContainsAttachment.Enabled = enable;
        }

        /// <summary>
        /// 设置同步和刷新按钮是否可用
        /// </summary>
        /// <param name="enable"></param>
        public void SetSynchrousAndRefreshButtonEnable(bool enabled)
        {
            this.toolRefersh.Enabled = enabled;
        }
        /// <summary>
        /// 转换成相对应的对象后，删除
        /// </summary>
        /// <param name="objSelection"></param>
        private void Deleted(object objSelection)
        {
            MailItem olItem = objSelection as MailItem;
            if (olItem != null)  //邮件
            {
                ClientProperties.DragDropUnreadMail.Add(olItem.UnRead);
                olItem.Delete();
            }
            else
            {
                ReportItem reportItem = objSelection as ReportItem; //回执
                if (reportItem != null)
                {
                    ClientProperties.DragDropUnreadMail.Add(reportItem.UnRead);
                    reportItem.Delete();
                }
                else
                {
                    ContactItem contactItem = objSelection as ContactItem; //联系人
                    if (contactItem != null)
                    {
                        ClientProperties.DragDropUnreadMail.Add(contactItem.UnRead);
                        contactItem.Delete();
                    }
                    else
                    {
                        PostItem postItem = objSelection as PostItem;  //报告
                        if (postItem != null)
                        {
                            ClientProperties.DragDropUnreadMail.Add(postItem.UnRead);
                            postItem.Delete();
                        }
                        else
                        {
                            NoteItem noteItem = objSelection as NoteItem; //便签
                            if (noteItem != null)
                            {
                                ClientProperties.DragDropUnreadMail.Add(false);
                                noteItem.Delete();
                            }
                            else
                            {
                                TaskItem taskItem = objSelection as TaskItem; //任务
                                if (taskItem != null)
                                {
                                    ClientProperties.DragDropUnreadMail.Add(taskItem.UnRead);
                                    taskItem.Delete();
                                }
                                else
                                {
                                    AppointmentItem appointmentItem = objSelection as AppointmentItem; //约会
                                    if (appointmentItem != null)
                                    {
                                        ClientProperties.DragDropUnreadMail.Add(appointmentItem.UnRead);
                                        appointmentItem.Delete();
                                    }
                                    else
                                    {
                                        JournalItem journalItem = objSelection as JournalItem;  //日记
                                        if (journalItem != null)
                                        {
                                            ClientProperties.DragDropUnreadMail.Add(journalItem.UnRead);
                                            journalItem.Delete();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void Stop()
        {
            this.mailReadTimer.Stop();
        }

        public void ExecuteTimer(bool doubleInterval)
        {
            this.mailReadTimer.Stop();
            //手动将邮件设置成已读后，需要间隔两倍时间去将邮件设置成已读
            if (doubleInterval)
                this.mailReadTimer.Interval = ClientProperties.MailReadTimeInterval * 2;
            else
                this.mailReadTimer.Interval = ClientProperties.MailReadTimeInterval;
            this.mailReadTimer.Start();
        }

        private void mailReadTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                ListPart.Command_SetMailRead(null, EventArgs.Empty);
                this.mailReadTimer.Stop();
            }
            catch (System.Exception ex)
            {
                this.mailReadTimer.Stop();
            }
        }
        #endregion

        private void toolReplyAll_MouseHover(object sender, EventArgs e)
        {
            toolReplyAll.ShowDropDown();
        }

        private void toolReplyAllContainsAttachment_MouseLeave(object sender, EventArgs e)
        {
            toolReplyAll.HideDropDown();
        }
    }
}
