#region Comment

/*
 * 
 * FileName:    FrmAssociatedStatistics.cs
 * CreatedOn:   2015/4/8 15:55:01
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      ->
 * History：
 *      ->
 * 
 * 
 * 
 * 
 */

#endregion

using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Windows.Forms;
using Exception = System.Exception;

namespace ICP.MailCenterFramework.UI
{
    public partial class FrmAssociatedStatistics : Form
    {
        /// <summary>
        /// 已关联
        /// </summary>
        private int _associatedCount = 0;
        /// <summary>
        /// 未关联
        /// </summary>
        private int _unAssociatedCount = 0;
        /// <summary>
        /// 无业务数据
        /// </summary>
        private int _noBusinessData = 0;
        /// <summary>
        /// 合计项
        /// </summary>
        private int _totalItemCount = 0;
        /// <summary>
        /// 当前文件夹索引
        /// </summary>
        private int _CurrentFolderIndex;
        /// <summary>
        /// 当前循环邮件索引
        /// </summary>
        private int _CurrentItemIndex;
        /// <summary>
        /// 消息提示标题
        /// </summary>
        private string _TipTitle = string.Empty;
        /// <summary>
        /// 消息提示信息
        /// </summary>
        private string _TipMessage = string.Empty;
        /// <summary>
        /// 当前文件夹
        /// </summary>
        private MAPIFolder _CurrentFolder;
        /// <summary>
        /// 选择的文件夹
        /// </summary>
        private ArrayList _SelectFolders;
        /// <summary>
        /// 语言环境
        /// </summary>
        private bool IsEnglish = false;
        /// <summary>
        /// 重置配置
        /// </summary>
        private bool ResetConfig = false;
        /// <summary>
        /// 选择文件夹串联文本
        /// </summary>
        private Dictionary<string, string> _SelectFolderDictionary;
        /// <summary>
        /// 默认数据源ID
        /// </summary>
        private readonly string _DefaultStoreName = OutlookUtility.OLNameSpace.DefaultStore.DisplayName;
        /// <summary>
        /// 
        /// </summary>
        private DataTable dtStatisticsResult = null;

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
                catch (System.Exception ex)
                {
                    ToolUtility.WriteLog("AssociatedStatistics DataCacheOperationService", ex);
                    temp = null;
                }
                return temp;
            }
        }
        #endregion

        public FrmAssociatedStatistics()
        {
            InitializeComponent();
            OperationEvent(true);
            Disposed += (sender, args) =>
            {
                //0.移除事件
                OperationEvent(false);
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmAssociatedStatistics_Load(object sender, EventArgs e)
        {
            try
            {
                InitData();
                InitControl();
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("AssociatedStatistics Load", ex);
            }
        }

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
                    btnStatistics.Enabled = _SelectFolderDictionary.Count > 0;
                    //重新开启关联
                    _CurrentFolderIndex = 0;
                    _CurrentItemIndex = 1;
                    _unAssociatedCount = 0;
                    _associatedCount = 0;
                    _noBusinessData = 0;
                    _totalItemCount = 0;
                    _CurrentFolder = null;
                    _SelectFolders = new ArrayList();
                    #endregion
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("AssociatedStatistics Browser", ex);
            }
        }

        void btnStatistics_Click(object sender, EventArgs e)
        {
            try
            {
                StatisticsAssociatedData();
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("AssociatedStatistics Statistics", ex);
            }
        }

        private void InitData()
        {
            try
            {
                IsEnglish = OutlookUtility.IsEnglish;
                _CurrentFolder = null;
                _SelectFolders = new ArrayList();
                _CurrentFolderIndex = 0;    //文件夹从0开始遍历
                _CurrentItemIndex = 1;      //邮件项从1开始遍历
                _SelectFolderDictionary = new Dictionary<string, string>();
                string tempSF = IniUtility.IniReadValue("PickFolders", "SelectFolders");
                dtStatisticsResult = new DataTable("StatisticsTable");
                dtStatisticsResult.Columns.Add("FolderID");
                dtStatisticsResult.Columns.Add("FolderName");
                dtStatisticsResult.Columns.Add("AssociatedCount");
                dtStatisticsResult.Columns.Add("UnAssociatedCount");

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
            catch (Exception ex)
            {
                ToolUtility.WriteLog("AssociatedStatistics InitData", ex);
            }
        }

        void InitControl()
        {
            if (!IsEnglish)
            {
                Text = "批量关联邮件";
                lblLocal.Text = "位置：";
                btnBrowse.Text = "浏览";
                btnStatistics.Text = "统计";
                groupBoxPanel.Text = "统计结果";
                ColumnFolder.HeaderText = "文件夹";
                ColumnAssiciated.HeaderText = "已关联";
                ColumnUnAssiciated.HeaderText = "未关联";
            }
            btnStatistics.Enabled = _SelectFolderDictionary.Count > 0;
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
                Load += FrmAssociatedStatistics_Load;
                btnBrowse.Click += btnBrowse_Click;
                btnStatistics.Click += btnStatistics_Click;
            }
            else
            {
                Load -= FrmAssociatedStatistics_Load;
                btnBrowse.Click -= btnBrowse_Click;
                btnStatistics.Click -= btnStatistics_Click;
            }
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
            //通过选择的文件夹文本构建文件夹集对象
            foreach (var item in _SelectFolderDictionary)
            {
                string[] tempEntryIDs = item.Key.Split('&');
                MAPIFolder findFolder = null;
                try
                {
                    findFolder = OutlookUtility.OLNameSpace.Stores[tempEntryIDs[0]].Session.GetFolderFromID(tempEntryIDs[1]);
                }
                catch (System.Exception ex)
                {
                    ResetConfig = true;
                    throw ex;
                }
                _ItemSumCount += findFolder.Items.Count;
                _SelectFolders.Add(findFolder);
            }
            //检索文件夹失败：文件夹内邮件过多
            if (_SelectFolders.Count <= 0)
                return false;
            //  0.1.0 是否包含邮件
            if (_ItemSumCount == 0)
            {
                _TipMessage = IsEnglish ? "No Mail" : "不存在邮件";
                MessageBox.Show(_TipMessage, _TipTitle,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        #region 统计关联数据
        /// <summary>
        /// 统计关联数据
        /// </summary>
        private void StatisticsAssociatedData()
        {
            try
            {
                ssState.Visible = true;
                if (!ValidationMail())
                    return;
                System.Diagnostics.Stopwatch totalWatch = ICP.DataCache.ServiceInterface.StopwatchHelper.StartStopwatch();
                System.Reflection.MethodBase methodother = System.Reflection.MethodBase.GetCurrentMethod();

                #region 2. 循环选择文件夹内邮件,并调用关联

                for (; _CurrentFolderIndex < _SelectFolders.Count; _CurrentFolderIndex++)
                {
                    _CurrentFolder = _SelectFolders[_CurrentFolderIndex] as MAPIFolder;
                    if (_CurrentFolder == null)
                        continue;
                    //2.循环选择文件夹内邮件,并调用关联
                    #region 循环文件夹内的邮件

                    int currentItemsCount = _CurrentFolder.Items.Count;
                    tsBarStatistics.Value = 0;
                    tsBarStatistics.Maximum = currentItemsCount;

                    for (; _CurrentItemIndex <= currentItemsCount; _CurrentItemIndex++)
                    {
                        tsBarStatistics.Value = _CurrentItemIndex;
                        ////2.1 正常关联操作
                        ////2.1.0 禁用邮件上传
                        ////TODO:1.1.0 关联期间，后台计时器自动上传邮件动作已开始则禁用 

                        //2.1.1 设置当前操作邮件索引
                        object item = _CurrentFolder.Items[_CurrentItemIndex];
                        //2.1.2 判断当前邮件是否已关联
                        string associateResult = string.Empty;
                        if (item != null && !OutlookUtility.IsRelationOperation(item))
                            _associatedCount++;
                        else  //已关联邮件和关联失败邮件统一计入关联失败邮件个数中
                            _unAssociatedCount++;
                        //2.2 即时显示邮件个数：可中断或继续关联
                        //防止窗体假死
                        System.Windows.Forms.Application.DoEvents();
                        Focus();
                    }
                    FillRecordText(_CurrentFolder.EntryID,_CurrentFolder.Name, _associatedCount, _unAssociatedCount);
                    _associatedCount = 0;
                    _unAssociatedCount = 0;
                    #endregion
                    //防止窗体假死
                    System.Windows.Forms.Application.DoEvents();
                    Focus();
                }
                if (DataCacheOperationService != null)
                {
                    totalWatch.Stop();
                    string strOperationLog = string.Format("关联邮件统计：总计{0}项,已关联{1}项,未{2}项", (_associatedCount + _unAssociatedCount), _associatedCount, _unAssociatedCount);
                    DataCacheOperationService.WriteStopwatchLog(methodother.DeclaringType.FullName,"PLUGIN", strOperationLog, totalWatch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
                    totalWatch = null;
                }
                btnBrowse.Enabled = true;
                btnStatistics.Enabled = true;
                ssState.Visible = false;

                #endregion
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("StatisticsAssociatedData", ex);
            }
        }
        #endregion

        /// <summary>
        /// 显示记录信息
        /// </summary>
        /// <param name="folderID"></param>
        /// <param name="folderName"></param>
        /// <param name="assiciatedCount"></param>
        /// <param name="unAssiciatedCount"></param>
        private void FillRecordText(string folderID,string folderName, int assiciatedCount, int unAssiciatedCount)
        {
            try
            {
                bool isAdd = true;
                foreach (DataRow item in dtStatisticsResult.Rows.Cast<DataRow>().Where(item => folderID.Equals(item["FolderID"])))
                {
                    item["AssociatedCount"] = assiciatedCount;
                    item["UnAssociatedCount"] = unAssiciatedCount;
                    isAdd = false;
                    break;
                }
                if (isAdd)
                {
                    DataRow row = dtStatisticsResult.NewRow();
                    row["FolderID"] = folderID;
                    row["FolderName"] = folderName;
                    row["AssociatedCount"] = assiciatedCount;
                    row["UnAssociatedCount"] = unAssiciatedCount;
                    dtStatisticsResult.Rows.Add(row);
                }
                dgvStatistics.DataSource = dtStatisticsResult;
            }
            catch
            {

            }
        }
    }
}
