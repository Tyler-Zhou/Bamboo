#region Comment

/*
 * 
 * FileName:    FrmEmailArchiving.cs
 * CreatedOn:   2014/4/30 星期三 18:03:06
 * CreatedBy:   taylor
 * 
 * 
 * Description：
 *      ->邮件归档
 * History：
 *      ->
 * 
 * 
 * 
 */

#endregion

using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using ICP.MailCenterFramework.ServiceInterface;
using ICP.Message.ServiceInterface;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Exception = System.Exception;

namespace ICP.MailCenterFramework.UI
{
    /// <summary>
    /// 邮件归档
    /// </summary>
    public partial class FrmEmailArchiving : Form
    {
        #region 成员变量

        #region 私有变量
        
        /// <summary>
        /// 选择文件夹串联文本
        /// </summary>
        private Dictionary<string, string> _SelectFolder;
        /// <summary>
        /// 过滤设置
        /// UnRead：true,false
        /// IsMarkedAsTask：true,false
        /// </summary>
        private Dictionary<string, string> _FilterSetting;
        /// <summary>
        /// 移动邮件MessageID集合
        /// </summary>
        private Dictionary<string,string> _MoveMailMessageIDs;
        /// <summary>
        /// 默认数据源ID
        /// </summary>
        private readonly string _DefaultStoreName = OutlookUtility.OLNameSpace.DefaultStore.DisplayName;
        /// <summary>
        /// 移动邮件线程
        /// </summary>
        private Thread movieMailThread;
        /// <summary>
        /// 总邮件数量
        /// </summary>
        private int _MailCount;
        /// <summary>
        /// 操作计时器
        /// </summary>
        Stopwatch totalWatch = null;

        /// <summary>
        /// 进程最大值
        /// </summary>
        private const int Process_MaxValue = 100;
        /// <summary>
        /// 进度
        /// </summary>
        private float num_Percent;
        /// <summary>
        /// 乱入模式
        /// </summary>
        private bool _IsTrespassing;
        ///// <summary>
        ///// 当前关联文件夹EntryID
        ///// </summary>
        //private string _CurrentRelationFolderEntryID;
        ///// <summary>
        ///// 当前Store Display Name
        ///// </summary>
        //private string _CurrentStoreName;
        ///// <summary>
        ///// 邮件项目
        ///// </summary>
        //private string _ProjectName;   
        ///// <summary>
        ///// 关联邮件串联文本
        ///// </summary>
        //private Dictionary<string, object> _SelectItem;
        #endregion

        #region 是否默认操作
        /// <summary>
        /// 归档模式
        /// </summary>
        public string ArchivingModel { get; set; }
        #endregion

        #region 属性-
        /// <summary>
        /// 
        /// </summary>
        private OperationMessageController _OMCObj
        {
            get { return new OperationMessageController(); }
        }
        #endregion

        #region 属性-配置文件操作类
        /// <summary>
        /// 配置文件操作类
        /// </summary>
        private INIHelper iniUtility;
        public INIHelper IniUtility
        {
            get
            {
                return iniUtility ??
                       (iniUtility = new INIHelper(AppDomain.CurrentDomain.BaseDirectory, "OutLookConfig"));
            }
        }
        #endregion

        #region 服务-FCM接口
        /// <summary>
        /// 缓存数据库服务接口
        /// </summary>
        public IDataCacheOperationService DataCacheOperationService
        {
            get
            {
                IDataCacheOperationService temp = null;
                try
                {
                    temp = ServiceClient.GetService<IDataCacheOperationService>();
                }
                catch (Exception ex)
                {
                    ToolUtility.WriteLog("AssociatedBatch DataCacheOperationService", ex);
                    temp = null;
                }
                return temp;
            }
        }
        #endregion

        #region 委托事件
        /// <summary>
        /// 设置邮件项目名称
        /// </summary>
        /// <param name="strText">设置文本</param>
        private delegate void OnSetMailProjet(string strText);
        /// <summary>
        /// 设置文件夹名
        /// </summary>
        /// <param name="strText">设置文本</param>
        private delegate void OnSetMailFolderName(string strText);
        /// <summary>
        /// 设置移动到文件名
        /// </summary>
        /// <param name="strText">设置文本</param>
        private delegate void OnSetMailMoveFolderName(string strText);
        /// <summary>
        /// 设置当前状态
        /// </summary>
        /// <param name="strText">设置文本</param>
        private delegate void OnSetState(string strText);
        /// <summary>
        /// 当前进程:当前值设定
        /// </summary>
        /// <param name="maxinum">值</param>
        /// <param name="intValue">值</param>
        private delegate void OnCurrentProgressValue(int maxinum,int intValue);
        /// <summary>
        /// 关闭窗体
        /// </summary>
        private delegate void OnClosed();
        #endregion
        #endregion

        #region 构造方法
        public FrmEmailArchiving()
        {
            InitializeComponent();
            //初始化变量
            if (!DesignMode)
            {
                //初始化变量
                InitData();
                //初始化控件
                InitControl();
                OperationEvent(true);
                Disposed += (sender, args) =>
                {
                    OperationEvent(false);
                };
            }
        }
        #endregion

        #region 窗体事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        void FrmEmailArchiving_Load(object sender, EventArgs e)
        {
            try
            {
                switch (ArchivingModel)
                {
                    case "Default"://默认
                        FillDefaultFolder();
                        break; //关联邮件后 
                    case "AfterAssociated":
                        //关联后归档
                        string tempSF = IniUtility.IniReadValue("PickFolders", "SelectFolders");
                        if (!string.IsNullOrEmpty(tempSF))
                        {
                            foreach (var item in tempSF.Split(',').Select(tempItem => tempItem.Split('&')))
                            {
                                if (item.Length > 2)
                                    _SelectFolder.Add(item[0] + "&" + item[1], item[2]);
                                else
                                    _SelectFolder.Add(_DefaultStoreName + "&" + item[0], item[1]);
                            }
                        }
                        break;
                }
                //过滤邮件：未读、后续标志、未发送
                AddFilter("UnRead", "true");    
                AddFilter("IsMarkedAsTask", "true");
                AddFilter("Sent", "false");

                EmailArchiving();
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("Email Archiving:Load", ex);
            }
        }
        /// <summary>
        /// 取消归档并关闭窗体
        /// </summary>
        void btnCancel_Click(object sender, EventArgs e)
        {
            CancelAndClose();
        }
        /// <summary>
        /// 窗体即将关闭
        /// </summary>
        void FrmEmailArchiving_FormClosing(object sender, FormClosingEventArgs e)
        {
            _IsTrespassing = true;
            e.Cancel = false;
        }
        #endregion

        #region 公用方法
        /// <summary>
        /// 添加过滤条件
        /// </summary>
        /// <param name="key">Propertie Name</param>
        /// <param name="value">Propertie Value</param>
        public void AddFilter(string key, string value)
        {
            if (_FilterSetting.ContainsKey(key))
                return;
            _FilterSetting.Add(key, value);
        }

        /// <summary>
        /// 邮件归档
        /// </summary>
        public void EmailArchiving()
        {
            totalWatch = StopwatchHelper.StartStopwatch();
            

            try
            {
                movieMailThread.Start();
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("Email Archiving:EmailArchiving", ex);
            }
        } 
        #endregion

        #region 方法定义

        /// <summary>
        /// 初始化数据
        /// </summary>
        void InitData()
        {
            ArchivingModel = "Default";
            _SelectFolder = new Dictionary<string, string>();
            _FilterSetting = new Dictionary<string, string>();
            _MoveMailMessageIDs = new Dictionary<string,string>();
            movieMailThread = new Thread(MovieMailItems);
            movieMailThread.SetApartmentState(ApartmentState.STA);

            //_SelectItem = new Dictionary<string, object>();
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        void InitControl()
        {
            lblMailProject.Text = "";
            lblMailFolderName.Text = "";
            lblMoveToFolder.Text = "";
            //lblRemainded.Text = "";
            lblCurrentState.Text = "";

            if (!OutlookUtility.IsEnglish)
            {
                Text = "邮件归档";
                lblTotalEmail.Text = "共移动 0 封邮件.";
                lblMailProjectTitle.Text = "邮件项目:";
                lblMailFolderNameTitle.Text = "文件夹:";
                lblMoveToFolderTitle.Text = "移动到:";
                //lblRemaindedTitle.Text = "剩余项:";
                btnCancel.Text = "取消";
            }
        }

        /// <summary>
        /// 填充默认文件夹
        /// </summary>
        void FillDefaultFolder()
        {
            try
            {
                //获取收件箱
                _SelectFolder.Clear();
                MAPIFolder defaultFolder = null;
                Selection selectItem = null;
                Store defaultStore = null;
                try
                {
                    selectItem = OutlookUtility.CurrentApplication.ActiveExplorer().Selection;
                }
                catch
                {
                    selectItem = null;
                }
                if (selectItem == null || selectItem.Count <= 0)
                {
                    defaultStore = OutlookUtility.OLNameSpace.DefaultStore;
                    defaultFolder = defaultStore.Session.GetDefaultFolder(OlDefaultFolders.olFolderInbox);
                    
                }
                else
                {
                    object tempObj = selectItem[1];
                    if (tempObj is MailItem)
                    {
                        defaultFolder = (tempObj as MailItem).Parent as MAPIFolder;
                        defaultStore = defaultFolder == null ? null : defaultFolder.Store;
                    }
                }
                if (defaultStore!=null && defaultFolder != null)
                    _SelectFolder.Add(defaultStore.DisplayName + "&" + defaultFolder.EntryID, defaultFolder.Name);
            }
            catch(Exception ex)
            {
                ToolUtility.WriteLog("Email Archiving:FillDefaultFolder", ex);
            }
        }

        /// <summary>
        /// 操作事件
        /// </summary>
        /// <param name="isAdd">是否添加事件</param>
        void OperationEvent(bool isAdd)
        {
            if (isAdd)
            {
                Load += FrmEmailArchiving_Load;
                FormClosing += FrmEmailArchiving_FormClosing;
                btnCancel.Click += btnCancel_Click;
            }
            else
            {
                Load -= FrmEmailArchiving_Load;
                FormClosing -= FrmEmailArchiving_FormClosing;
                btnCancel.Click -= btnCancel_Click;
            }
        }
        
        /// <summary>
        /// 过滤邮件/报文(Item)对象
        /// 属性值与过滤器中的值相等的返回空字符，否则判断是否关联邮件，是关联邮件返回邮件EntryID
        /// </summary>
        /// <param name="item">邮件/报文(Item)对象</param>
        /// <param name="archivingType">归档类型</param>
        /// <returns>Item MessageID</returns>
        string FilterItem(object item, out ArchivingType archivingType)
        {
            string messageID = string.Empty;
            archivingType = ArchivingType.Filter;
            string itemSubject = string.Empty;
            try
            {
                if (item is MailItem)
                {
                    MailItem olItem = item as MailItem;
                    itemSubject = olItem.Subject;
                    //属性值与过滤器中的值相等的返回空字符
                    if (_FilterSetting.Any(filterItem => filterItem.Value.Equals(olItem.ItemProperties[filterItem.Key].Value.ToString().ToLower())))
                    {
                        return string.Empty;
                    }
                }
                if (item is ReportItem)
                {
                    ReportItem olReportItem = item as ReportItem;
                    if (_FilterSetting.Any(filterItem => filterItem.Value.Equals(olReportItem.ItemProperties[filterItem.Key].Value.ToString().ToLower())))
                    {
                        return string.Empty;
                    }
                }
                if (OutlookUtility.IsRelationOperation(item, out messageID))
                    archivingType = ArchivingType.Relation;
                else{
                    //无法根据主题单号查找的业务信息的邮件归档到无业务数据
                    if (string.IsNullOrEmpty(GetQueryConditions.AppendAdvanceStringToSQL(itemSubject)))
                        archivingType = ArchivingType.NoBusiness;
                    else
                        messageID = string.Empty; //主题包含疑为单号邮件清空MessageID，搁置移动
                }
            }
            catch(Exception ex)
            {
                ToolUtility.WriteLog("Email Archiving:FilterItem", ex);
                return string.Empty;
            }
            return messageID;
        }

        /// <summary>
        /// 移动邮件
        /// </summary>
        void MovieMailItems()
        {
            //0.是否选择文件夹
            if (_SelectFolder.Count <= 0)
            {
                CancelAndClose();
            }
            //1.需归档文件夹
            int currentItemCount = 0; //当前邮件数量
            //当前数据文件源
            Store mailStore=null;
            // 当前文件夹
            MAPIFolder _CurrentFolder = null;
            //收件箱：存储关联的邮件OR回执文件夹对象
            MAPIFolder InboxRelationOLFolder =null;
            //收件箱：存储无业务邮件OR回执文件夹对象
            MAPIFolder InboxNoBusinessOLFolder = null;
            //发件箱:存储关联邮件OR回执文件夹对象
            MAPIFolder SendRelationOLFolder = null;
            //发件箱:存储无业务的邮件OR回执文件夹对象
            MAPIFolder SendNoBusinessOLFolder = null;
            //之前数据源
            string beforeStoreID = string.Empty;
            try
            {
                #region 遍历选择文件夹

                //OrderBy:Store ID包含在Key中，按照Store顺序移动邮件
                foreach (var item in _SelectFolder.OrderBy(i => i.Key))
                {
                    string[] keyStr = item.Key.Split('&');
                    //与上次数据源一致则使用上次构建的归档目录
                    if (beforeStoreID == string.Empty || !beforeStoreID.Equals(keyStr[0]))
                    {
                        mailStore = OutlookUtility.OLNameSpace.Stores[keyStr[0]];
                        beforeStoreID = keyStr[0];
                        SetMailPorject(mailStore.DisplayName);
                        //是否默认数据源
                        bool isDefaultStore = mailStore.DisplayName.Equals(_DefaultStoreName);
                        InboxRelationOLFolder = OutlookUtility.GetArchivingFolder(mailStore, ArchivingType.Relation,
                            false, isDefaultStore);
                        InboxNoBusinessOLFolder = OutlookUtility.GetArchivingFolder(mailStore, ArchivingType.NoBusiness,
                            false, isDefaultStore);
                        SendRelationOLFolder = OutlookUtility.GetArchivingFolder(mailStore, ArchivingType.Relation,
                            true, isDefaultStore);
                        SendNoBusinessOLFolder = OutlookUtility.GetArchivingFolder(mailStore, ArchivingType.NoBusiness,
                            true, isDefaultStore);
                    }
                    if (mailStore == null)
                        continue;
                    _CurrentFolder = mailStore.Session.GetFolderFromID(keyStr[1]);
                    SetMailFolderName(_CurrentFolder.Name);

                    SetMailMoveFolderName(InboxRelationOLFolder.Name);
                    //查找已关联邮件

                    //当前文件夹下Item集合对象
                    currentItemCount = _CurrentFolder.Items.Count;
                    _MailCount += currentItemCount;

                    #region 遍历Items
                    //遍历Items
                    int moveIndex = 1;
                    for (int index1 = 1; index1 <= currentItemCount; index1++)
                    {
                        SetCurrentProgress(currentItemCount, index1);
                        SetCurrentState(
                            string.Format(
                                (OutlookUtility.IsEnglish
                                    ? "Moving mail item:Mail Item Total[{0}],Current[{1}]   "
                                    : "正在移动邮件:邮件合计:[{0}]项,当前:[{1}]项   "), currentItemCount, index1));
                        ArchivingType archivingType = ArchivingType.Filter;
                        string itemMessageID = FilterItem(_CurrentFolder.Items[moveIndex], out archivingType);
                        if (string.IsNullOrEmpty(itemMessageID))
                        {
                            moveIndex++; //不存在关联的邮件搁置
                            continue;
                        }
                        try
                        {
                            if (_CurrentFolder.Items[moveIndex] is MailItem)
                            {
                                switch (archivingType)
                                {
                                    case ArchivingType.Relation:
                                        (_CurrentFolder.Items[moveIndex] as MailItem).Move(
                                            OutlookUtility.IsUseOriginalSenderAddress(
                                                (_CurrentFolder.Items[moveIndex] as MailItem))
                                                ? SendRelationOLFolder
                                                : InboxRelationOLFolder);
                                        break;
                                    case ArchivingType.NoBusiness:
                                        (_CurrentFolder.Items[moveIndex] as MailItem).Move(
                                            OutlookUtility.IsUseOriginalSenderAddress(
                                                (_CurrentFolder.Items[moveIndex] as MailItem))
                                                ? SendNoBusinessOLFolder
                                                : InboxNoBusinessOLFolder);
                                        break;
                                }

                            }
                            else if (_CurrentFolder.Items[moveIndex] is ReportItem)
                            {
                                switch (archivingType)
                                {
                                    case ArchivingType.Relation:
                                        (_CurrentFolder.Items[moveIndex] as ReportItem).Move(InboxRelationOLFolder);
                                        break;
                                    case ArchivingType.NoBusiness:
                                        (_CurrentFolder.Items[moveIndex] as ReportItem).Move(InboxNoBusinessOLFolder);
                                        break;
                                }
                                (_CurrentFolder.Items[moveIndex] as ReportItem).Move(InboxRelationOLFolder);
                            }
                            if (archivingType == ArchivingType.Relation)
                                _MoveMailMessageIDs.Add(itemMessageID, mailStore.GetRootFolder().FolderPath);
                        }
                        catch (Exception ex)
                        {
                            moveIndex++; //移动发生异常的邮件搁置
                        }
                    }

                    #endregion
                }
                Thread.Sleep(1000);
                SetCurrentState(
                    string.Format((OutlookUtility.IsEnglish ? "Total Mobile Mail [{0}]   " : "总共移动邮件[{0}]   "),
                        _MoveMailMessageIDs.Count));

                #endregion

                Thread.Sleep(1000);
                SetCurrentState(OutlookUtility.IsEnglish
                    ? "The update to the cache associated mail item EntryID   "
                    : "将关联邮件EntryID更新到缓存");
                //2.已移动邮件项

                #region 根据记录的已移动邮件MessageID，查找关联并更新EntryID

                if (HelpMailStore.TableMessageRelation.Count > 0)
                {
                    MailItem mailItem = null;

                    OperationSaveController osc = new OperationSaveController();
                    int updateEntryIDCount = _MoveMailMessageIDs.Count;
                    int updateEntryIDIndex = 1;
                    foreach (var itemID in _MoveMailMessageIDs)
                    {
                        SetCurrentProgress(updateEntryIDCount, updateEntryIDIndex);
                        string moveMessageID = itemID.Key;
                        SetCurrentState(
                            string.Format(
                                (OutlookUtility.IsEnglish
                                    ? "Updating the mail item EntryID:Mail Item Total[{0}],Current[{1}]   "
                                    : "正在更新邮件EntryID:邮件合计:[{0}]项,当前:[{1}]项   "), updateEntryIDCount, updateEntryIDIndex));
                        OutlookAdvancedSearch oas = new OutlookAdvancedSearch(moveMessageID);
                        oas.ScopeName = itemID.Value;
                        oas.RunAdvancedSearch();
                        if (oas.SearchResults != null && oas.SearchResults.Count > 0)
                        {
                            mailItem = oas.SearchResults[0] as MailItem;
                        }
                        if (mailItem != null)
                        {
                            List<OperationMessageRelation> omrList =
                                _OMCObj.GetOperationMessageRelationListByMessageID(moveMessageID);
                            if (omrList.Count > 0)
                            {
                                foreach (var messageRelation in omrList)
                                {
                                    messageRelation.UpdateDataType = UpdateDataType.MainForMessageID;
                                    messageRelation.EntryID = mailItem.EntryID;
                                }
                                osc.SaveLocalOperationMessageRelation(omrList.ToArray());
                            }
                        }
                        oas = null;
                    }
                    updateEntryIDIndex++;
                }
                Thread.Sleep(1000);
                SetCurrentProgress(1, 1);
                SetCurrentState(OutlookUtility.IsEnglish ? "Complete   " : "完成   ");

                #endregion
            }
            catch (Exception)
            {
            }
            finally
            {
                _CurrentFolder = null;
                InboxRelationOLFolder = null;
                InboxNoBusinessOLFolder = null;
                SendRelationOLFolder = null;
                SendNoBusinessOLFolder = null;
                //关闭窗体窗体
                CancelAndClose();
            }
            
        }

        #region 设置邮箱项目名称
        /// <summary>
        /// 设置邮箱项目名称
        /// </summary>
        /// <param name="strText"></param>
        void SetMailPorject(string strText)
        {
            if (InvokeRequired)
            {
                OnSetMailProjet setMethod = SetMailProjectText;
                Invoke(setMethod, new object[] { strText });
            }
            else
                SetMailProjectText(strText);
        }

        void SetMailProjectText(string strText)
        {
            lblMailProject.Text = strText;
            lblMailProject.Refresh();
        }
        #endregion

        #region 设置邮箱文件夹名称
        /// <summary>
        /// 设置邮箱文件夹名称
        /// </summary>
        /// <param name="strText"></param>
        void SetMailFolderName(string strText)
        {
            if (InvokeRequired)
            {
                OnSetMailFolderName setMethod = SetMailFolderNameText;
                Invoke(setMethod, new object[] { strText });
            }
            else
                SetMailFolderNameText(strText);
        }

        void SetMailFolderNameText(string strText)
        {
            lblMailFolderName.Text = strText;
            lblMailFolderName.Refresh();
        }
        #endregion

        #region 设置移动至文件夹名称
        /// <summary>
        /// 设置移动至文件夹名称
        /// </summary>
        /// <param name="strText"></param>
        void SetMailMoveFolderName(string strText)
        {
            if (InvokeRequired)
            {
                OnSetMailMoveFolderName setMethod = SetMailMoveFolderNameText;
                Invoke(setMethod, new object[] { strText });
            }
            else
                SetMailMoveFolderNameText(strText);
        }

        void SetMailMoveFolderNameText(string strText)
        {
            lblMoveToFolder.Text = strText;
            lblMoveToFolder.Refresh();
        }
        #endregion

        #region 设置当前移动状态
        /// <summary>
        /// 设置当前移动状态
        /// </summary>
        /// <param name="strText"></param>
        void SetCurrentState(string strText)
        {
            if (InvokeRequired)
            {
                OnSetState setMethod = SetCurrentStateText;
                Invoke(setMethod, new object[] { strText });
            }
            else
                SetCurrentStateText(strText);
        }

        void SetCurrentStateText(string strText)
        {
            lblCurrentState.Text = strText;
        }
        #endregion

        #region 设置当前进度值
        /// <summary>
        /// 设置当前进度值
        /// </summary>
        /// <param name="maxinum">最大值</param>
        /// <param name="intValue">值</param>
        void SetCurrentProgress(int maxinum,int intValue)
        {
            if (InvokeRequired)
            {
                OnCurrentProgressValue setMethod = SetCurrentProgressValue;
                Invoke(setMethod, new object[] { maxinum,intValue });
            }
            else
                SetCurrentProgressValue(maxinum,intValue);
        }

        void SetCurrentProgressValue(int maxinum, int intValue)
        {
            barCurrentProgress.Maximum = maxinum;
            barCurrentProgress.Minimum = 0;
            barCurrentProgress.Step = 1;
            barCurrentProgress.Value = intValue;
        }
        #endregion

        #region 取消并关闭窗体
        /// <summary>
        /// 取消移动并关闭窗体
        /// </summary>
        void CancelAndClose()
        {
            if (InvokeRequired)
            {
                OnClosed close = CancelAndCloseWindows;
                Invoke(close);
            }
            else { CancelAndCloseWindows(); }
        }

        /// <summary>
        /// 取消移动并关闭窗体
        /// </summary>
        void CancelAndCloseWindows()
        {
            if (DataCacheOperationService != null)
            {
                MethodBase methodother = MethodBase.GetCurrentMethod();
                totalWatch.Stop();
                string strOperationLog = string.Format("Mail Plugin-Archiving：Total[{0}]item", _MailCount);
                DataCacheOperationService.WriteStopwatchLog(methodother.DeclaringType.FullName, "PLUGIN", strOperationLog, totalWatch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
            }
            totalWatch = null;
            if (movieMailThread.ThreadState == System.Threading.ThreadState.Running)
                movieMailThread.Abort();
            Thread.Sleep(1000);
            Close();
        } 
        #endregion


        
        #endregion
    }
}
