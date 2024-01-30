#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/6/20 17:31:50
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System.Collections.Concurrent;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Cityocean.Crawl.CommonLibrary;
using Cityocean.Crawl.LogComponents;
using Cityocean.Crawl.CommonTool;
using Cityocean.Crawl.ServiceInterface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Cityocean.Crawl.ServerComponents
{
    /// <summary>
    /// 抓取服务
    /// </summary>
    public class CrawlService : BaseService
    {
        #region Fields
        private string _RootDirectory;
        private string _JSONDirectory;
        private string _HTMLDirectory;
        /// <summary>
        /// Lock Object
        /// </summary>
        public readonly object lockObj = new object();
        #endregion

        #region Property
        /// <summary>
        /// 当前执行项索引
        /// </summary>
        public int CurrentIndex
        {
            get;
            set;
        }
        /// <summary>
        /// 是否异常批次
        /// </summary>
        public bool IsExceptionBatch { get; set; }
        /// <summary>
        /// 抓取异常
        /// </summary>
        public string BatchNo { get; set; }

        /// <summary>
        /// Exception Batch No
        /// </summary>
        public string ExceptionBatchNo
        {
            get
            {
                string lastBatchNo = LastBatchNo;
                if(!lastBatchNo.IsNullOrEmpty())
                    SetConfigValue(CrawlCommonConstants.EXCEPTION_BATCHNO, lastBatchNo);
                string batchNo = GetConfigValue(CrawlCommonConstants.EXCEPTION_BATCHNO);
                IsExceptionBatch = !batchNo.IsNullOrEmpty();
                return batchNo;
            }
            set
            {
                SetConfigValue(CrawlCommonConstants.EXCEPTION_BATCHNO, value);
            }
        }
        /// <summary>
        /// Last Batch No
        /// </summary>
        public string LastBatchNo
        {
            get
            {
                return GetConfigValue(CrawlCommonConstants.LAST_BATCHNO);
            }
            set
            {
                SetConfigValue(CrawlCommonConstants.LAST_BATCHNO, value);
            }
        }
        /// <summary>
        /// Root Directory
        /// </summary>
        public string RootDirectory
        {
            get
            {
                if (_RootDirectory.IsNullOrEmpty())
                    _RootDirectory = string.Format(@"{0}{1}\{2}\", GlobalVariable.ProgramDirectory, ModuleName, JobDateTime.ToString("yyyy-MM-dd"));
                return _RootDirectory;
            }
        }

        /// <summary>
        /// JSON Cache Directory
        /// </summary>
        public string JSONDirectory
        {
            get
            {
                if (_JSONDirectory.IsNullOrEmpty())
                    _JSONDirectory= string.Format(@"{0}{1}\JSON\", RootDirectory, BatchNo);
                return _JSONDirectory;
            }
        }

        /// <summary>
        /// HTML Cache Directory
        /// </summary>
        public string HTMLDirectory
        {
            get
            {
                if (_HTMLDirectory.IsNullOrEmpty())
                    _HTMLDirectory=string.Format(@"{0}\{1}\HTML\", RootDirectory, BatchNo);
                return _HTMLDirectory;
            }
        }
        /// <summary>
        /// 任务时间
        /// </summary>
        public DateTime JobDateTime { get; set; }
        /// <summary>
        /// 单次任务执行数据条数
        /// </summary>
        public virtual int TotalCount { get; set; }

        private TimeSpan _TS_Task = new TimeSpan(8, 0, 0);
        /// <summary>
        /// 任务执行超时时间:(默认8H,23:30 前取消任务)
        /// </summary>
        public virtual TimeSpan TS_Task
        {
            set { _TS_Task = value; }
            get
            {
                TimeSpan tsInterval = ((DateTime.Now.ToString("yyyy-MM-dd") + " 23:30:00").ToDateTime()).Subtract(DateTime.Now);
                return (tsInterval < _TS_Task) ? tsInterval : _TS_Task;
            }
        }

        /// <summary>
        /// 单个任务超时时间(5m0s)
        /// </summary>
        public virtual TimeSpan TS_SingleTask
        {
            get
            {
                return new TimeSpan(0, 6, 0);
            }
        }

        #region 网站配置
        private List<CrawlConfig> _websiteConfigs;
        /// <summary>
        /// 网站配置
        /// </summary>
        public List<CrawlConfig> _WebsiteConfigs
        {
            get
            {
                return _websiteConfigs ??
                       (_websiteConfigs =
                           _websiteConfigs = new List<CrawlConfig>());
            }
            set
            {
                if (value == null && _websiteConfigs != null)
                    _websiteConfigs.Clear(); _websiteConfigs = value;
            }
        } 
        #endregion
        #endregion

        #region Method
        /// <summary>
        /// 通过网站配置执行动作
        /// </summary>
        /// <param name="driver">驱动器</param>
        /// <param name="taskCrawlData">取值或赋值对象</param>
        /// <param name="crawlConfig">网站配置</param>
        /// <returns></returns>
        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        public void CrawlByWebConfig(IWebDriver driver, object taskCrawlData, CrawlConfig crawlConfig)
        {
            try
            {
                List<CrawlConfigParam> formElements = crawlConfig.WebsiteParams.Where(fitem => fitem.KeyType == Website_KeyType.FormElement).ToList();
                if (!formElements.Any()) return;

                #region Foreach
                foreach (CrawlConfigParam configItem in formElements.OrderBy(orderItem => orderItem.SortIndex))
                {
                    if (!CommonConstants.BLANK_PAGE.Equals(driver.Url) && driver.GetPageSource().IsNullOrEmpty())
                        throw new WebDriverException(string.Format("BlankPage:[{0}][{1}]", crawlConfig.WebsiteCode, configItem.KeyValue));

                    if (crawlConfig.IsNeedLogin && configItem.KeyValueType == Website_KeyValueType.LoginForm)
                    {
                        CrawlConfigParam loginConfigItem = formElements.FirstOrDefault(fItem => fItem.KeyValueType == Website_KeyValueType.LoginForm);
                        if (loginConfigItem != null)
                        {
                            var loginForm = driver.WaitFindElement(loginConfigItem.KeyValue, false);
                            if (loginForm != null)
                            {
                                List<CrawlConfigParam> loginElements = new List<CrawlConfigParam>();
                                if (!loginConfigItem.ParamValue.IsNullOrEmpty())
                                {
                                    if (loginForm.GetAttribute(loginConfigItem.ParamValue).IsNullOrEmpty())
                                    {
                                        loginElements = crawlConfig.WebsiteParams.Where(fitem => fitem.KeyType == Website_KeyType.CredentialElement).ToList();
                                    }
                                }
                                else
                                    loginElements = crawlConfig.WebsiteParams.Where(fitem => fitem.KeyType == Website_KeyType.CredentialElement).ToList();
                                foreach (CrawlConfigParam eWebsiteParam in loginElements.OrderBy(orderItem => orderItem.SortIndex))
                                {
                                    CrawlByWebConfigParam(driver, taskCrawlData, crawlConfig, eWebsiteParam);
                                }
                            }
                        }
                    }
                    CrawlByWebConfigParam(driver, taskCrawlData, crawlConfig, configItem);
                }
                #endregion
            }
            catch (Exception ex)
            {
                //LogService.Error(ModuleName, "通过网站配置抓取数据时发生异常", ex);
                throw new WebDriverException(ex.Message);
            }
        }

        /// <summary>
        /// 通过网站配置执行动作
        /// </summary>
        /// <param name="driver">驱动器</param>
        /// <param name="taskCrawlData">取值或赋值对象</param>
        /// <param name="crawlConfig">网站配置</param>
        /// <param name="websiteConfig">网站明细配置</param>
        /// <returns></returns>
        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        public void CrawlByWebConfigParam(IWebDriver driver, object taskCrawlData, CrawlConfig crawlConfig, CrawlConfigParam websiteConfig)
        {
            string keyValue = string.Empty;
            try
            {
                var watch = DateTime.Now;
                keyValue = websiteConfig.KeyValue;
                #region switch
                switch (websiteConfig.KeyValueType)
                {
                    case Website_KeyValueType.Wait:
                        int seconds = keyValue.TryToInt();
                        if (seconds == 0)
                        {
                            var waitResult = driver.WaitElementsLoad(keyValue, websiteConfig.Timeout);
                            if (!waitResult)
                            {
                                throw new WebDriverException(string.Format("{0}:等待[{1}][{2}]超时", websiteConfig.ParamValue, websiteConfig.Timeout, keyValue));
                            }
                        }
                        else
                            Thread.Sleep(TimeSpan.FromSeconds(seconds));
                        break;
                    case Website_KeyValueType.TextBox:
                        var txtBox = driver.WaitFindElement(keyValue, true, true, websiteConfig.Timeout);
                        txtBox.Clear();
                        txtBox.SendKeys(websiteConfig.GetParamValueByKey(taskCrawlData));
                        break;
                    case Website_KeyValueType.SelectBox:
                        var selectBox = driver.WaitFindElement(keyValue, false, false, websiteConfig.Timeout);
                        SelectElement selectControl = new SelectElement(selectBox);
                        selectControl.SelectByValue(websiteConfig.GetParamValueByKey(taskCrawlData));
                        break;
                    case Website_KeyValueType.ClickIgnorable:
                        var btnBoxIgnorable = driver.WaitFindElement(keyValue, false, false, websiteConfig.Timeout);
                        if (btnBoxIgnorable != null)
                        {
                            if (websiteConfig.ParamValue.IsNullOrEmpty())
                                btnBoxIgnorable.Click();
                            else
                                btnBoxIgnorable.Submit();
                        }
                        break;
                    case Website_KeyValueType.Click:
                        var btnBox = driver.WaitFindElement(keyValue, true, true, websiteConfig.Timeout);
                        if (websiteConfig.ParamValue.IsNullOrEmpty())
                            btnBox.Click();
                        else
                            btnBox.Submit();
                        break;
                    case Website_KeyValueType.Frame:
                        driver.GotoFrame(keyValue);
                        break;
                    case Website_KeyValueType.Window:
                        try
                        {
                            driver.GotoWindow(keyValue, websiteConfig.Timeout);
                        }
                        catch (Exception ex)
                        {
                            throw new WebDriverException(string.Format("{0}{1}", websiteConfig.ParamValue, ex.Message));
                        }
                        break;
                    case Website_KeyValueType.JavaScript:
                        if (!driver.PageSource.GetText().IsNullOrEmpty())
                            driver.ExecuteScript(keyValue);
                        break;
                    case Website_KeyValueType.Navigate:
                        List<CrawlConfigParam> wParams = crawlConfig.WebsiteParams.Where(fitem => fitem.KeyType == Website_KeyType.HTTPParam).ToList();
                        string[] paramRetainWebsiteParams = websiteConfig.ParamValue.IsNullOrEmpty() ? new string[] { } : websiteConfig.ParamValue.Split(',');
                        string strParams =
                            wParams.Where(
                                s => paramRetainWebsiteParams.Length == 0 || paramRetainWebsiteParams.Contains(s.KeyValue)
                                        ).Aggregate(
                                            "", (current, s) => current + string.Format("{0}={1}&", s.KeyValue, s.GetParamValueByKey(taskCrawlData))
                                                    );
                        keyValue += (strParams.IsNullOrEmpty() ? "" : ("?" + strParams.Substring(0, strParams.Length - 1)));
                        driver.NavigateUrl(keyValue, websiteConfig.Timeout);
                        break;
                    case Website_KeyValueType.Exception:
                        var exception = driver.WaitFindElement(keyValue, false);
                        if (exception != null && !exception.Text.IsNullOrEmpty())
                        {
                            throw new WebDriverException(string.Format("{0}:{1}", websiteConfig.ParamValue, exception.Text));
                        }
                        break;
                }
                #endregion
                LogService.Debug(ModuleName
                    , string.Format("{0}\t{1}\t{2}\t{3}\t{4}", crawlConfig.WebsiteCode, keyValue, websiteConfig.Timeout
                        , DateTime.Now.Subtract(watch).ToStringSeconds(), crawlConfig.WebsiteCode)
                    );
            }
            catch (Exception ex)
            {
                LogService.Error(ModuleName, string.Format("执行操作[{0}]发生异常", keyValue), ex);
                throw new WebDriverException(ex.Message);
            }
        }
        /// <summary>
        /// 生成批次号
        /// </summary>
        /// <param name="paramCodePrefix">代码前缀</param>
        /// <returns></returns>
        public string GenerateBatchNo(string paramCodePrefix)
        {
            return SerialnumberHelper.GenerateNO(paramCodePrefix);
        }
        #endregion


        #region virtual Method
        /// <summary>
        /// 开始任务
        /// </summary>
        /// <param name="taskList"></param>
        public virtual void StartTask(ConcurrentQueue<TaskCrawlData> taskList)
        {
            List<Task> taskMultiple = new List<Task>();
            CancellationTokenSource ctsMultiple = new CancellationTokenSource();
            try
            {
                #region Foreach Code Group
                var gCodeList = taskList.GroupBy(gItem => gItem.WebsiteCode);
                foreach (var gCodeListItem in gCodeList)
                {
                    CancellationTokenSource ctsWebsite = new CancellationTokenSource();
                    IGrouping<string, TaskCrawlData> igCodeListItem = gCodeListItem;

                    #region 记录开始
                    Task taskBeginLog = Task.Factory.StartNew(() => LogService.Info(ModuleName,
                        string.Format("[{0}] 网页编码[{1}] 合计[{2}]", BatchNo, igCodeListItem.Key, igCodeListItem.ToList().Count()))
                        , ctsWebsite.Token);
                    taskMultiple.Add(taskBeginLog);
                    #endregion

                    #region Foreach ID Group
                    var gIDList = igCodeListItem.ToList().GroupBy(gItem => gItem.WebsiteID);
                    foreach (var gIDItemList in gIDList)
                    {
                        #region 验证配置
                        CrawlConfig websiteConfig = _WebsiteConfigs.SingleOrDefault(fItem => gIDItemList.Key.Equals(fItem.ID));
                        if (websiteConfig == null)
                        {
                            LogService.Info(ModuleName, string.Format("[{0}] 配置方案ID[{1}] 未找到网页配置", BatchNo, gIDItemList.Key));
                            continue;
                        }
                        if (websiteConfig.WebsiteParams.Count <= 0)
                        {
                            LogService.Info(ModuleName, string.Format("[{0}] 网页编码[{1}] 未找到配置明细", BatchNo, websiteConfig.WebsiteCode));
                            continue;
                        }
                        #endregion

                        #region 抓取数据
                        IGrouping<Guid, TaskCrawlData> igIDItemList = gIDItemList;
                        var oTaskList = igIDItemList.ToList();
                        int forIndex = 1;
                        Task taskCrawl = taskBeginLog.ContinueWith((paramTask) =>
                        {
                            try
                            {
                                foreach (var taskItem2 in oTaskList.TakeWhile(taskItem2 => !ctsWebsite.IsCancellationRequested && GlobalVariable.ServiceIsRuning))
                                {
                                    CrawlerDataByConfig(taskItem2, forIndex, websiteConfig, ctsWebsite);
                                    forIndex++;
                                }
                            }
                            catch (Exception ex)
                            {
                                LogService.Error(ModuleName, "TaskWebsite", ex);
                            }
                        }, TaskContinuationOptions.AttachedToParent);
                        taskMultiple.Add(taskCrawl);

                        #endregion

                        #region 结束记录

                        Task taskEndLog = taskCrawl.ContinueWith((paramTask) =>
                        {
                            try
                            {
                                if (paramTask.Exception != null)
                                {
                                    paramTask.Exception.Handle((inner) =>
                                    {
                                        LogService.Info(ModuleName,
                                            string.Format("[{0}] 网页编码[{1}] ID[{2}]抓取失败:{3}", BatchNo, igCodeListItem.Key, igIDItemList.Key,
                                                inner.Message));
                                        return true;
                                    });
                                }
                                else
                                {
                                    LogService.Info(ModuleName, string.Format("[{0}] 网页编码[{1}] ID[{2}]任务完成", BatchNo, igCodeListItem.Key, igIDItemList.Key));
                                }
                            }
                            catch (Exception ex)
                            {
                                LogService.Error(ModuleName, "TaskWebsite ContinueWith", ex);
                            }
                        }, TaskContinuationOptions.AttachedToParent);
                        taskMultiple.Add(taskEndLog);
                        #endregion
                    }
                    #endregion
                }
                #endregion
                if (!Task.WaitAll(taskMultiple.ToArray(), (int)TS_Task.TotalMilliseconds, ctsMultiple.Token))
                {
                    ctsMultiple.Cancel();
                }
            }
            catch (AggregateException aEx)
            {
                aEx.Handle((inner) =>
                {
                    if (inner is OperationCanceledException)
                    {
                        return true;
                    }
                    LogService.Error(ModuleName, "多任务多异常", inner);
                    return true;
                });
            }
            catch (Exception ex)
            {
                LogService.Error(ModuleName, "多任务异常", ex);
            }
        }

        void CrawlerDataByConfig(TaskCrawlData paramTaskItem, int paramIndex , CrawlConfig crawlConfig, CancellationTokenSource paramTokenSource)
        {
        }

        /// <summary>
        /// 创建抓取结果DataTable
        /// </summary>
        /// <param name="paramSearchPath">查询路径</param>
        /// <param name="paramIsList">是否集合</param>
        /// <returns></returns>
        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        public virtual List<T> BuildCrawlResults<T>(string paramSearchPath, bool paramIsList)
        {
            try
            {
                List<T> returnValues = new List<T>();
                FileDirectoryEnumerable fdEnumeratorConfig = new FileDirectoryEnumerable
                {
                    SearchPath = paramSearchPath,
                    ReturnStringType = false,
                    SearchPattern = "*.cache",
                    SearchForDirectory = true,
                    SearchForFile = true,
                    ThrowIOException = true,
                };
                #region Foreach JSON Directory File Name
                foreach (object fdItem in fdEnumeratorConfig)
                {
                    string fileFullName = "" + fdItem;
                    string fileName = fileFullName.ExtractFileName();
                    try
                    {
                        string jsonContent = HTMLCacheService.ReadCache(fileFullName);
                        if (jsonContent.IsNullOrEmpty()) continue;
                        if (paramIsList)
                        {
                            List<T> tempObjs = JsonConvert.DeserializeObject<List<T>>(jsonContent);
                            returnValues.AddRange(tempObjs);
                        }
                        else
                        {

                            T tempObj = JsonConvert.DeserializeObject<T>(jsonContent);
                            returnValues.Add(tempObj);
                        }

                    }
                    catch (Exception ex)
                    {
                        LogService.Info(ModuleName, string.Format("解析JSON目录文件[{0}]", fileName), ex);
                    }
                }
                #endregion
                return returnValues;
            }
            catch (Exception ex)
            {
                LogService.Error(ModuleName, "读取结果集cache文件时发生异常", ex);
                throw new Exception("读取结果集cache文件时发生异常");
            }
        }
        #endregion
    }
}
