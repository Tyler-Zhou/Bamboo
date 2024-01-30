#region Comment

/*
 * 
 * FileName:    FrmAssociatedBatch.cs
 * CreatedOn:   2014/7/14 15:04:49
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      ->批量关联
 * History：
 *      ->
 * 
 * 
 * 
 * 
 */

#endregion

using System.Globalization;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ICP.MailCenterFramework.UI
{
    /// <summary>
    /// 批量关联
    /// </summary>
    public partial class FrmAssociatedBatch : Form
    {
        #region 成员变量
        /// <summary>
        /// 消息提示标题
        /// </summary>
        private string _TipTitle = string.Empty;
        /// <summary>
        /// 消息提示信息
        /// </summary>
        private string _TipMessage = string.Empty;
        /// <summary>
        /// 选择的文件夹
        /// </summary>
        private ArrayList _SelectFolders;
        /// <summary>
        /// 当前文件夹
        /// </summary>
        private MAPIFolder _CurrentFolder;
        /// <summary>
        /// 选择文件夹串联文本
        /// </summary>
        private Dictionary<string, string> _SelectFolderDictionary;
        /// <summary>
        /// 默认数据源ID
        /// </summary>
        private readonly string _DefaultStoreName = OutlookUtility.OLNameSpace.DefaultStore.DisplayName;
        /// <summary>
        /// 当前文件夹索引
        /// </summary>
        private int _CurrentFolderIndex;
        /// <summary>
        /// 当前循环邮件索引
        /// </summary>
        private int _CurrentItemIndex;
        /// <summary>
        /// 成功操作邮件个数
        /// </summary>
        private int _SccesfullyCount;
        /// <summary>
        /// 操作失败邮件个数
        /// </summary>
        private int _FailedCount;
        /// <summary>
        /// 乱入模式
        /// </summary>
        private bool _IsTrespassing;
        /// <summary>
        /// 语言环境
        /// </summary>
        private bool IsEnglish=false;
        /// <summary>
        /// 重置配置
        /// </summary>
        private bool ResetConfig = false;
        /// <summary>
        /// 日志对象
        /// </summary>
        private System.Text.StringBuilder strLog = null;
        /// <summary>
        /// 邮件最大数量
        /// </summary>
        private int mailMaxCount = 1000;

        #region 属性-配置文件操作类
        /// <summary>
        /// 配置文件操作类
        /// </summary>
        private INIHelper _IniUtility;
        public INIHelper IniUtility
        {
            get
            {
                return _IniUtility ??
                       (_IniUtility = new INIHelper(AppDomain.CurrentDomain.BaseDirectory, "OutLookConfig"));
            }
        }
        #endregion

        #region 属性-选择文件夹目录字符串
        /// <summary>
        /// 选择文件夹目录字符串
        /// </summary>
        public string SelectFolderString
        {
            get
            {
                var tempStr = "";
                if (_SelectFolderDictionary == null || _SelectFolderDictionary.Count <= 0)
                    return tempStr;
                tempStr = _SelectFolderDictionary.Aggregate("", (current, item) => current + (item.Value + ","));
                tempStr = tempStr.Substring(0, tempStr.Length - 1);
                return tempStr;
            }
        }
        #endregion

        #region 属性-是否移动邮件
        /// <summary>
        /// 是否移动关联且已读邮件邮件
        /// </summary>
        public bool IsMoveMail { get; set; }
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
                catch(System.Exception ex)
                {
                    ToolUtility.WriteLog("AssociatedBatch DataCacheOperationService", ex);
                    temp = null;
                }
                return temp;
            }
        }
        #endregion

        #endregion

        #region 构造函数
        public FrmAssociatedBatch()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                //初始化变量
                InitData();
                //初始化控件
                InitControl();
                //注册事件
                OperationEvent(true);

                Disposed += (sender, args) =>
                {
                    //0.移除事件
                    OperationEvent(false);
                    //1.清空数据
                    _CurrentFolder = null;
                    _SelectFolders = null;
                };
            }
        }
        #endregion

        #region 窗体事件

        /// <summary>
        /// 浏览:显示选择OutLook文件夹面板
        /// </summary>
        void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                //0 弹出选择文件夹窗体
                FrmPickFolders frmPickFolders = new FrmPickFolders();
                //设置历史选择文件夹
                frmPickFolders.SelectFolderDictionary = new Dictionary<string, string>(_SelectFolderDictionary);
                frmPickFolders.ShowDialog();
                if (frmPickFolders.IsPick)
                {
                    #region Pick Folder
                    _SelectFolderDictionary = new Dictionary<string, string>(frmPickFolders.SelectFolderDictionary);
                    frmPickFolders.Dispose();
                    string tempSFS = "";
                    if (_SelectFolderDictionary.Count > 0)
                    {
                        tempSFS = _SelectFolderDictionary.Aggregate("", (current, item) => current + (item.Key + "&" + item.Value + ","));
                        tempSFS = tempSFS.Substring(0, tempSFS.Length - 1);
                    }
                    txtLocation.Text = SelectFolderString;//将选择结果显示到txtLocation上
                    //1.记录当前选择文件夹
                    IniUtility.IniWriteValue("PickFolders", "SelectFolders", tempSFS);
                    //是否启用关联按钮
                    btnAssociate.Enabled = _SelectFolderDictionary.Count > 0;
                    //重新开启关联
                    _CurrentFolderIndex = 0;
                    _CurrentItemIndex = 1;
                    _SccesfullyCount = 0;
                    _FailedCount = 0;
                    _IsTrespassing = false;
                    _CurrentFolder = null;
                    _SelectFolders = new ArrayList(); 
                    #endregion
                }
            }
            catch(System.Exception ex)
            {
                ToolUtility.WriteLog("Associated Batch:Browse", ex);
            }
        }

        /// <summary>
        /// 批量关联
        /// </summary>
        void btnAssociate_Click(object sender, EventArgs e)
        {
            try
            {
                BulkMailAssociation();
            }
            catch(System.Exception ex)
            {
                ToolUtility.WriteLog("Associated Batch:Associate", ex);
                if (ResetConfig)
                {
                    _TipMessage = IsEnglish ? "Please close forms and try again!\r\nException Error:The selected folder is removed or renamed!" 
                        : "关联出错，请关闭窗体后重试！\r\n异常原因：选择的文件夹被移除或重命名！";
                    MessageBox.Show(_TipMessage, _TipTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// 超链接点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lblAssociatedResult_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }
        /// <summary>
        /// 窗体即将关闭
        /// </summary>
        void FrmAssociatedBatch_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (ResetConfig) //重置配置
                {
                    IniUtility.IniWriteValue("PickFolders", "SelectFolders", "");
                }
                else
                {
                    //如果正在进行关联，将中断循环
                    _IsTrespassing = true;

                    #region 将已关联并且已读的邮件归档
                    //是否进行归档操作
                    if (checkMoveMailAR.Checked)
                    {
                        IsMoveMail = true;
                    }
                    #endregion
                }
            }
            catch(System.Exception ex)
            {
                ToolUtility.WriteLog("Associated Batch:Associated Batch", ex);
            }
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        void btnClose_Click(object sender, EventArgs e)
        {
            _IsTrespassing = true;
            Close();
        }
        #endregion

        #region 方法定义

        /// <summary>
        /// 初始化数据
        /// </summary>
        void InitData()
        {
            IsEnglish = OutlookUtility.IsEnglish;
            _TipTitle = IsEnglish ? "Tip" : "提示";
            _CurrentFolderIndex = 0;    //文件夹从0开始遍历
            _CurrentItemIndex = 1;      //邮件项从1开始遍历
            _SccesfullyCount = 0;
            _FailedCount = 0;
            _IsTrespassing = false;
            _CurrentFolder = null;
            _SelectFolders = new ArrayList();
            _SelectFolderDictionary = new Dictionary<string, string>();
            string tempSF = IniUtility.IniReadValue("PickFolders", "SelectFolders");
            strLog = new System.Text.StringBuilder();
            #region Load Dictionary
            if (!string.IsNullOrEmpty(tempSF))
            {
                foreach (var item in tempSF.Split(',').Select(items => items.Split('&')))
                {
                    if (item.Length > 2)
                        _SelectFolderDictionary.Add(item[0] + "&" + item[1], item[2]);
                    else
                        _SelectFolderDictionary.Add(_DefaultStoreName + "&" + item[0], item[1]);
                }
            }
            else
            {
                try
                {
                    //获取收件箱
                    MAPIFolder inboxFolder = OutlookUtility.CurrentApplication.Session.GetDefaultFolder(OlDefaultFolders.olFolderInbox);
                    string entryID = inboxFolder.Store.DisplayName + "&" + inboxFolder.EntryID;
                    _SelectFolderDictionary.Add(entryID, inboxFolder.Name);
                    IniUtility.IniWriteValue("PickFolders", "SelectFolders", entryID + "&" + inboxFolder.Name);
                }
                catch (System.Exception ex)
                {
                    ToolUtility.WriteLog("Associated Batch:InitData", ex);
                }
            } 
            #endregion
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        void InitControl()
        {
            if (!IsEnglish)
            {
                Text = "批量关联邮件";
                btnClose.Text = "关闭";
                xtraTabAssociation.Text = "关联";
                lblLocation.Text = "位置：";
                btnBrowse.Text = "浏览";
                checkMoveMailAR.Text = "将已关联并且已读的邮件归档";
                btnAssociate.Text = "开始关联";
                lblSccesfullyTitle.Text = "成功关联邮件：";
                lblFailedTitle.Text = "失败关联邮件：";
                lblHandledState.Text = "正在关联...";
            }
            btnAssociate.Enabled = _SelectFolderDictionary.Count > 0;
            txtLocation.Text = SelectFolderString;
        }

        /// <summary>
        /// 操作事件
        /// </summary>
        /// <param name="isRegister">是否注册</param>
        private void OperationEvent(bool isRegister)
        {
            if (isRegister)
            {
                FormClosing += FrmAssociatedBatch_FormClosing;   //窗体即将关闭
                btnClose.Click += btnClose_Click;                //关闭
                btnAssociate.Click += btnAssociate_Click;        //批量关联
                btnBrowse.Click += btnBrowse_Click;              //浏览
                lblAssociatedResult.LinkClicked += lblAssociatedResult_LinkClicked;
            }
            else
            {
                FormClosing -= FrmAssociatedBatch_FormClosing;   //窗体即将关闭
                btnClose.Click -= btnClose_Click;               //关闭
                btnAssociate.Click -= btnAssociate_Click;       //批量关联
                btnBrowse.Click -= btnBrowse_Click;             //浏览
            }
        }
        /// <summary>
        /// 设置乱入模式
        /// </summary>
        private void SetTrespassing()
        {
            //1.0 设置按钮显示值
            if (btnAssociate.Text.Equals(IsEnglish ? "Stop" : "停止"))
            {
                _IsTrespassing = true;
                btnBrowse.Enabled = true;
                btnAssociate.Text = IsEnglish ? "Continue" : "继续关联";
                lblHandledState.Text = IsEnglish ? "Stopped" : "已停止关联";
            }
            else
            {
                _IsTrespassing = false;
                btnBrowse.Enabled = false;
                btnAssociate.Text = IsEnglish ? "Stop" : "停止";
                lblHandledState.Text = IsEnglish ? "Associating..." : "正在关联...";
            }
        }

        void BulkMailAssociation()
        {
            #region 0 关联前验证

            if (!ValidationMail())
                return;
            #endregion

            #region 1 设置按钮文本，判断是否乱入模式
            //1.0 设置按钮显示值
            SetTrespassing();
            //1.1 是否乱入模式:乱入模式只设置显示值，不进行关联操作
            if (_IsTrespassing)
                return;
            #endregion
            System.Diagnostics.Stopwatch totalWatch =ICP.DataCache.ServiceInterface.StopwatchHelper.StartStopwatch();
            System.Reflection.MethodBase methodother = System.Reflection.MethodBase.GetCurrentMethod();

            #region 2. 循环选择文件夹内邮件,并调用关联

            for (; _CurrentFolderIndex < _SelectFolders.Count; _CurrentFolderIndex++)
            {
                //2.0 乱入模式:暂停循环
                if (_IsTrespassing)
                    break;
                _CurrentFolder = _SelectFolders[_CurrentFolderIndex] as MAPIFolder;
                if (_CurrentFolder == null)
                    continue;
                //2.循环选择文件夹内邮件,并调用关联
                #region 循环文件夹内的邮件

                int currentItemsCount = _CurrentFolder.Items.Count;
                for (; _CurrentItemIndex <= currentItemsCount; _CurrentItemIndex++)
                {
                    //2.0 乱入模式:暂停循环
                    if (_IsTrespassing)
                    {
                        //Break仅停止当前循环，上级for循环会再次进入，需要将目录索引-1
                        _CurrentFolderIndex--;
                        break;
                    }
                    ////2.1 正常关联操作
                    ////2.1.0 禁用邮件上传
                    ////TODO:1.1.0 关联期间，后台计时器自动上传邮件动作已开始则禁用 

                    //2.1.1 设置当前操作邮件索引
                    object item = _CurrentFolder.Items[_CurrentItemIndex];
                    //2.1.2 判断当前邮件是否已关联
                    string associateResult = string.Empty;
                    if (item != null && AssociateEmail(item, ref associateResult))
                        _SccesfullyCount++;
                    else  //已关联邮件和关联失败邮件统一计入关联失败邮件个数中
                        _FailedCount++;
                    //2.2 即时显示邮件个数：可中断或继续关联
                    lblSccesfullyCount.Text = _SccesfullyCount.ToString(CultureInfo.InvariantCulture);
                    lblFailedCount.Text = _FailedCount.ToString(CultureInfo.InvariantCulture);
                    strLog.AppendFormat("Folder:[{0}]Item:[{1}]Result:[{2}]Subject:[{3}]\r\n"
                        , _CurrentFolder.Name, _CurrentItemIndex, associateResult, (item is MailItem)?(item as MailItem).Subject:"");
                    //防止窗体假死
                    System.Windows.Forms.Application.DoEvents();
                    Focus();
                }
                #endregion
                //防止窗体假死
                System.Windows.Forms.Application.DoEvents();
                Focus();
            }
            if (!_IsTrespassing)
            {
                if (DataCacheOperationService != null)
                {
                    totalWatch.Stop();
                    string strOperationLog = string.Format("批量关联邮件：总计{0}项,成功{1}项,失败{2}项", (_SccesfullyCount + _FailedCount), _SccesfullyCount, _FailedCount);
                    DataCacheOperationService.WriteStopwatchLog(methodother.DeclaringType.FullName,"PLUGIN", strOperationLog,totalWatch.ElapsedMilliseconds.ToString());
                    totalWatch = null;
                }
                btnBrowse.Enabled = true;
                lblHandledState.Text = IsEnglish ? "Stopped" : "已停止关联";
                btnAssociate.Text = IsEnglish ? "Associate" : "开始关联";
                btnAssociate.Enabled = false;
            }
            string fullFilePath=WriteLog();
            if (!string.IsNullOrEmpty(fullFilePath))
            {
                lblAssociatedResult.Links[0].LinkData = fullFilePath;
                lblAssociatedResult.Visible = true;
            }

            #endregion
        }

        /// <summary>
        /// 关联邮件
        /// </summary>
        /// <param name="item">邮件对象</param>
        /// <param name="associateResult">关联结果</param>
        /// <returns>是否关联成功</returns>
        private bool AssociateEmail(object item, ref string associateResult)
        {
            bool returnValue = false;
            try
            {
                if (!OutlookUtility.IsRelationOperation(item))
                {
                    OperationSaveController omc = new OperationSaveController();
                    returnValue = omc.AssociateOperationBusiness(item, ref associateResult);
                }
                associateResult = returnValue ? "Success" : associateResult;
            }
            catch (System.Exception ex)
            {
                //异常不处理：默认关联失败
                returnValue = false;
                ToolUtility.WriteLog("Associated Batch:AssociateEmail", ex);
            }
            return returnValue;
        }
        /// <summary>
        /// 验证邮件
        /// </summary>
        /// <returns>验证结果 true：可继续关联；false：不能继续关联</returns>
        private bool ValidationMail()
        {
            //非首次关联验证
            if (_CurrentFolderIndex != 0 || _CurrentItemIndex != 1)
                return true;
            //0.0 是否选择文件夹
            if (_SelectFolderDictionary.Count <= 0)
                return false;
            int _ItemSumCount = 0;
            _SelectFolders.Clear();
            panelAssociatedState.Visible = true;
            //通过选择的文件夹文本构建文件夹集对象
            foreach (var item in _SelectFolderDictionary)
            {
                string[] tempEntryIDs = item.Key.Split('&');
                MAPIFolder findFolder = null;
                try
                {
                    findFolder = OutlookUtility.OLNameSpace.Stores[tempEntryIDs[0]].Session.GetFolderFromID(tempEntryIDs[1]);
                }
                catch(System.Exception ex)
                {
                    ResetConfig = true;
                    throw ex;
                }
                _ItemSumCount += findFolder.Items.Count;
                //  0.1.1 文件夹邮件个数超过300封提示
                if (_ItemSumCount > mailMaxCount)
                {
                    _TipMessage = IsEnglish
                        ? "Too many mail, please tidy the mail folder less than [{0}] items."
                        : "邮件数量太多，请将邮件数量清理至 [{0}] 封以内。";
                    MessageBox.Show(string.Format(_TipMessage,mailMaxCount), _TipTitle,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    _SelectFolders.Clear();
                    break;
                }
                _SelectFolders.Add(findFolder);
            }
            //检索文件夹失败：文件夹内邮件过多
            if (_SelectFolders.Count <= 0)
                return false;
            //  0.1.0 是否包含邮件
            if (_ItemSumCount == 0)
            {
                _TipMessage = IsEnglish ? "No association Mail" : "不存在已关联邮件";
                MessageBox.Show(_TipMessage, _TipTitle,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                return false;
            }
            panelAssociatedState.Visible = true;
            return true;
        }
        #region 写日志
        /// <summary>
        /// 写日志
        /// </summary>
        string WriteLog()
        {
            string FileFullPath = string.Empty;
            try
            {
                string logPath = LocalData.MainPath + "\\LogFiles\\";
                System.IO.Directory.CreateDirectory(logPath);
                string FileName = String.Format("AssociateResult{0:HH-mm-ss}", DateTime.Now) + ".txt";
                FileFullPath = logPath + FileName;
                string WriteText = string.Empty;
                using (System.IO.TextWriter write = System.IO.File.AppendText(FileFullPath))
                {
                    write.WriteLine(strLog);
                    write.Close();
                }
            }
            catch
            {
            }
            return FileFullPath;
        }
        #endregion  
        #endregion
    }
}
