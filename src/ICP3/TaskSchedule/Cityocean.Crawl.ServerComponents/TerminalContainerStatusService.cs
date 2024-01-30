#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/4/10 15:31:37
 *
 * Description:
 *         ->
 *         相关配置
 *         SearchURL:http://www.yundangnet.com/terminal/{0}?companyId=2504&portcd={1}&terminalcd={2}&blno={3}&ctnrno={4}
 *         ieid
 *         查询箱参数定义
 *         获取JSON模板
 *         {"status":1
 *         ,"items":"[]"
 *         ,"baseInfoLst":"[
 *              {
 *                      \"keyid\":\"da60f403-b59f-4509-af8f-98cecabc1f2e\",\"ctnrno\":\"TGHU3983420\",\"blno\":\"051752884\",\"owner\":\"APLN\",\"manager\":null
 *                      ,\"vslname\":\"APL SINGAPURA\",\"voy\":\"047FMW\",\"ieid\":\"E\",\"csize\":\"20\",\"ctype\":\"GP\",\"terminalcd\":\"SZXYTMT\"
 *                      ,\"terminal\":\"深圳盐田\",\"slot\":\"\",\"sealno\":\"F5731719\",\"updatetime\":\"2017/9/14 15:18:26\",\"createtime\":\"2017/9/14 15:14:41\"
 *                      ,\"customstatus\":\"Y\",\"customtime\":\"2017/8/10 11:15:05\",\"terminalstatus\":null,\"terminaltime\":null,\"inspectionstatus\":null
 *                      ,\"inspectiontime\":null,\"citype\":null,\"ifci\":null,\"citime\":null,\"dgid\":null,\"dglevel\":null,\"dgunno\":null,\"rfid\":null
 *                      ,\"rftmp\":null,\"owid\":null,\"olid\":null,\"olf\":0,\"olb\":0,\"owl\":0,\"owr\":0,\"oh\":0,\"poundwgt\":\"\",\"vgm\":null,\"netwgt\":null
 *                      ,\"vslname1\":\"\",\"voy1\":\"\",\"vslname2\":\"APL SINGAPURA\",\"voy2\":\"047FMW\",\"vgmsign\":null,\"vgmwgttype\":null,\"storagedays\":\"7\"
 *                      ,\"remark\":null,\"fwdname\":null,\"pieces\":null,\"grosswgt\":null,\"volume\":null,\"ietrade\":null,\"pol\":null,\"pod\":\"LE HAVRE,France(FRLEH)\"
 *                      ,\"tsp\":null,\"dtp\":null,\"avdm\":null,\"intime\":\"2017/8/9 21:13:27\",\"outtime\":\"2017/8/15 4:48:04\",\"cntrdynamic\":\"出口装船\"
 *               }]"
 *          ,"totalHits":0
 *          ,"totalPages":"100"
 *          ,"terminalcd":""}
 *         
 *          {"status":1
 *          ,"items":"[
 *              {\"keyid\":\"4f574424-ae62-4bb3-9f9b-ebc99a5b1c04\",\"fkeyid\":\"da60f403-b59f-4509-af8f-98cecabc1f2e\",\"ctnrno\":\"TGHU3983420\",\"blno\":\"051752884\"
 *                  ,\"truckno\":\"黄GDBBP877\",\"ietime\":\"2017/8/15 4:48:04\",\"ieid\":\"E\",\"iegate\":\"出闸\",\"owner\":\"APLN\",\"vslname\":\"APL SINGAPURA\"
 *                  ,\"voy\":\"047FMW\",\"trailer\":\"\",\"cntrdynamic\":\"出口装船\",\"csize\":\"20\",\"ctype\":\"GP\",\"efid\":\"F\",\"terminalcd\":\"SZXYTMT\"
 *                  ,\"terminal\":\"深圳盐田\",\"slot\":\"\",\"sealno\":\"F5731719\",\"updatetime\":\"2017/9/14 15:18:26\",\"createtime\":\"2017/9/14 15:18:26\"
 *                  ,\"customstatus\":\"Y\",\"inspectionstatus\":null,\"terminalstatus\":null,\"netwgt\":null,\"poundwgt\":\"\",\"vgm\":null,\"pieces\":null
 *                  ,\"grosswgt\":null,\"volume\":null,\"eirno\":null,\"avdm\":null}
 *             ,{\"keyid\":\"e3090e0e-6d3b-44ea-a7a0-e74513b263a8\",\"fkeyid\":\"da60f403-b59f-4509-af8f-98cecabc1f2e\",\"ctnrno\":\"TGHU3983420\",\"blno\":\"051752884\"
 *                  ,\"truckno\":\"黄GDBBP877\",\"ietime\":\"2017/8/9 21:13:27\",\"ieid\":\"E\",\"iegate\":\"进闸\",\"owner\":\"APLN\",\"vslname\":\"APL SINGAPURA\"
 *                  ,\"voy\":\"047FMW\",\"trailer\":\"\",\"cntrdynamic\":\"出口进场\",\"csize\":\"20\",\"ctype\":\"GP\",\"efid\":\"F\",\"terminalcd\":\"SZXYTMT\"
 *                  ,\"terminal\":\"深圳盐田\",\"slot\":\"\",\"sealno\":\"F5731719\",\"updatetime\":\"2017/9/14 15:18:26\",\"createtime\":\"2017/9/14 15:18:26\"
 *                  ,\"customstatus\":\"Y\",\"inspectionstatus\":null,\"terminalstatus\":null,\"netwgt\":null,\"poundwgt\":\"\",\"vgm\":null,\"pieces\":null
 *                  ,\"grosswgt\":null,\"volume\":null,\"eirno\":null,\"avdm\":null}
 *              ]"
 *           ,"baseInfo":"{\"keyid\":\"da60f403-b59f-4509-af8f-98cecabc1f2e\",\"ctnrno\":\"TGHU3983420\",\"blno\":\"051752884\",\"owner\":\"APLN\",\"manager\":null,\"vslname\":\"APL SINGAPURA\",\"voy\":\"047FMW\",\"ieid\":\"E\",\"csize\":\"20\",\"ctype\":\"GP\",\"terminalcd\":\"SZXYTMT\",\"terminal\":\"深圳盐田\",\"slot\":\"\",\"sealno\":\"F5731719\",\"updatetime\":\"2017/9/14 15:18:26\",\"createtime\":\"2017/9/14 15:14:41\",\"customstatus\":\"Y\",\"customtime\":\"2017/8/10 11:15:05\",\"terminalstatus\":null,\"terminaltime\":null,\"inspectionstatus\":null,\"inspectiontime\":null,\"citype\":null,\"ifci\":null,\"citime\":null,\"dgid\":null,\"dglevel\":null,\"dgunno\":null,\"rfid\":null,\"rftmp\":null,\"owid\":null,\"olid\":null,\"olf\":0,\"olb\":0,\"owl\":0,\"owr\":0,\"oh\":0,\"poundwgt\":\"\",\"vgm\":null,\"netwgt\":null,\"vslname1\":\"\",\"voy1\":\"\",\"vslname2\":\"APL SINGAPURA\",\"voy2\":\"047FMW\",\"vgmsign\":null,\"vgmwgttype\":null,\"storagedays\":\"7\",\"remark\":null,\"fwdname\":null,\"pieces\":null,\"grosswgt\":null,\"volume\":null,\"ietrade\":null,\"pol\":null,\"pod\":\"LE HAVRE,France(FRLEH)\",\"tsp\":null,\"dtp\":null,\"avdm\":null,\"intime\":\"2017/8/9 21:13:27\",\"outtime\":\"2017/8/15 4:48:04\",\"cntrdynamic\":\"出口装船\"}"
 *          ,"totalHits":2}
 * 
 *         
 *         
 *         数据库字段与JSON字段对应
 *         baseInfoLst[i]:
 *
 * History:
 *         ->
 */
#endregion

using System.Runtime.ExceptionServices;
using System.Security;
using Cityocean.Crawl.CommonLibrary;
using Cityocean.Crawl.CommonTool;
using Cityocean.Crawl.LogComponents;
using Cityocean.Crawl.NoticeComponents;
using Cityocean.Crawl.ServiceInterface;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cityocean.Crawl.ServerComponents
{
    /// <summary>
    /// 码头箱状态服务
    /// </summary>
    public sealed class TerminalContainerStatusService : CrawlService, ITerminalContainerStatusService
    {
        #region Fields
        #endregion

        #region Property

        private Guid _TaskID = Guid.Empty;
        /// <summary>
        /// 任务ID
        /// </summary>
        public Guid TaskID
        {
            get { return _TaskID; }
            set { _TaskID = value; }
        }

        #region Task List
        IList<TaskTerminalContainerStatus> _taskList;
        /// <summary>
        /// 任务集合
        /// </summary>
        IList<TaskTerminalContainerStatus> _TaskList
        {
            get { return _taskList ?? (_taskList = new List<TaskTerminalContainerStatus>()); }
            set { if (value == null && _taskList != null) _taskList.Clear(); _taskList = value; }
        }

        IList<TaskTerminalContainerStatus> _resultList;
        /// <summary>
        /// 处理结果集合
        /// </summary>
        IList<TaskTerminalContainerStatus> _ResultList
        {
            get { return _resultList ?? (_resultList = new List<TaskTerminalContainerStatus>()); }
            set { if (value == null && _resultList != null) _resultList.Clear(); _resultList = value; }
        }
        #endregion

        #region Override
        /// <summary>
        /// 模块名称
        /// </summary>
        public override string ModuleName
        {
            get
            {
                return CrawlCommonConstants.MODULENAME_TERMINALCONTAINERSTATUS;
            }
        }
        #endregion

        #endregion

        #region Public Method
        /// <summary>
        /// 
        /// </summary>
        public TerminalContainerStatusService()
        {
            JobDateTime = DateTime.Now;
            CurrentIndex = 0;
        }
        /// <summary>
        /// 抓取数据
        /// </summary>
        public void CrawlerData()
        {
            try
            {
                try
                {
                    #region Crawl Data & Save JSON File
                    BatchNo = ExceptionBatchNo;

                    _TaskList.Clear();
                    _ResultList.Clear();

                    if (IsExceptionBatch)
                    {
                        LogService.Info(ModuleName, string.Format("[{0}] 异常数据处理", BatchNo));
                    }
                    else
                    {
                        _TaskList = TaskID != Guid.Empty ? GetTask() : GetTaskList();
                        TotalCount = _TaskList.Count;
                        if (_TaskList.Count <= 0)
                        {
                            LogService.Info(ModuleName, "未找到抓取码头信息任务");
                            return;
                        }
                        BatchNo = GenerateBatchNo("CRAWL");
                        LastBatchNo = BatchNo;
                    }
                    InitTerminalServiceConfig();

                    if (_WebsiteConfigs.Count <= 0)
                    {
                        LogService.Info(ModuleName, "未找到抓取码头信息服务配置信息");
                        return;
                    }
                    TS_Task = new TimeSpan(2, 0, 0);
                    List<Task> taskMultiple = new List<Task>();
                    CancellationTokenSource ctsMultiple = new CancellationTokenSource();
                    try
                    {
                        #region Foreach Code Group
                        var gCodeList = _TaskList.GroupBy(gItem => gItem.WebsiteCode);
                        foreach (var gCodeListItem in gCodeList)
                        {
                            CancellationTokenSource ctsWebsite = new CancellationTokenSource();
                            IGrouping<string, TaskTerminalContainerStatus> igCodeListItem = gCodeListItem;

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

                                #region 按箱号顺序抓取数据
                                IGrouping<Guid, TaskTerminalContainerStatus> igIDItemList = gIDItemList;
                                var oTaskList = igIDItemList.ToList().OrderBy(oItem => oItem.ContainerNO).ToList();
                                Task taskCrawl = taskBeginLog.ContinueWith((paramTask) =>
                                 {
                                     try
                                     {
                                         int forIndex = 1;
                                         foreach (var taskItem2 in oTaskList.TakeWhile(taskItem2 => !ctsWebsite.IsCancellationRequested && GlobalVariable.ServiceIsRuning))
                                         {
                                             try
                                             {
                                                 CrawlTaskMemoryCache.Add(websiteConfig.ID, string.Format("正在从网站[{0}]抓取箱[{1}]状态信息", websiteConfig.WebsiteCode
                                                     , taskItem2.ContainerNO));
                                                 CrawlerDataByConfig(taskItem2, forIndex, websiteConfig, ctsWebsite);
                                             }
                                             finally
                                             {
                                                 CrawlTaskMemoryCache.Remove(websiteConfig.ID);
                                             }
                                             forIndex++;
                                         }
                                     }
                                     catch (Exception ex)
                                     {
                                         LogService.Error(ModuleName, "TaskWebsite", ex);
                                     }
                                 }, TaskContinuationOptions.ExecuteSynchronously);
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
                                }, TaskContinuationOptions.ExecuteSynchronously);
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
                            LogService.Info(ModuleName, "多任务多个异常", inner);
                            return true;
                        });
                    }
                    catch (Exception ex)
                    {
                        LogService.Error(ModuleName, "多任务异常", ex);
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    LogService.Error(ModuleName, string.Format("[{0}]抓取数据", BatchNo), ex);
                }
                try
                {
                    #region Batch Save

                    DataTable dtConfig = new List<TaskTerminalContainerStatus>().ToDataTable();
                    if ((JSONDirectory).IsExistsDirectory())
                    {

                        List<TaskTerminalContainerStatus> configs = BuildCrawlResults<TaskTerminalContainerStatus>(JSONDirectory, false);
                        _ResultList = configs;
                        dtConfig = configs.Where(fItem => fItem.HandleStatus > HandleStatus.Untreated).ToDataTable();
                    }
                    BatchSaveTerminalContainerDynamicHTML(dtConfig);
                    #endregion
                }
                catch (Exception ex)
                {
                    LogService.Error(ModuleName, string.Format("[{0}] 批量保存", BatchNo), ex);
                }
                try
                {
                    #region Log & Report
                    if (_ResultList.Count <= 0) return;

                    EReportInfo eReport = new EReportInfo(string.Format("CSP.TCSR:码头信息报告-{0}", BatchNo));

                    StringBuilder sbContent = new StringBuilder();
                    string strLog = string.Format("箱总数:{0} 执行数量:{1} 完成数:{2} 失败数:{3} 总耗时:{4}",
                        TotalCount,
                        _ResultList.Count(),
                        _ResultList.Count(fitem => fitem.HandleStatus == HandleStatus.Complete),
                        _ResultList.Count(fitem => fitem.HandleStatus == HandleStatus.Failure),
                        DateTime.Now.Subtract(JobDateTime).ToString(@"hh\:mm\:ss"));
                    sbContent.AppendFormat("<h4>{0}</h4><br />", strLog);

                    var resultGroups = _ResultList.GroupBy(gItem => gItem.TerminalCode).ToArray();
                    if (resultGroups.Any())
                    {
                        sbContent.AppendFormat(
                            "<table  border='1'><tr><th>码头编码</th><th>执行数量</th><th>完成数</th><th>失败数</th></tr>");
                        foreach (var groupItem in resultGroups)
                        {
                            var tempList = groupItem.ToList();
                            sbContent.AppendFormat(
                                "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>"
                                , groupItem.Key
                                , tempList.Count()
                                , tempList.Count(fitem => fitem.HandleStatus == HandleStatus.Complete)
                                , tempList.Count(fitem => fitem.HandleStatus == HandleStatus.Failure)
                                );
                        }
                        sbContent.Append("</table><br />");
                    }

                    var errorData = _ResultList.Where(fItem => fItem.HandleStatus < HandleStatus.Untreated).ToArray();
                    if (errorData.Any())
                    {
                        sbContent.Append("异常数据:<br />");
                        sbContent.AppendFormat(
                            "<table  border='1'><tr><th>网站编码</th><th>码头编码</th><th>箱号</th><th>异常</th></tr>");
                        foreach (var errorItem in errorData)
                        {
                            sbContent.AppendFormat(
                                "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>"
                                , errorItem.WebsiteCode
                                , errorItem.TerminalCode
                                , errorItem.ContainerNO
                                , errorItem.HTMLDescription
                                );
                        }
                        sbContent.Append("</table><br />");
                    }

                    eReport.Context = sbContent.ToString();
                    eReport.AttachmentPaths = new[] { "" };
                    MService.SendEMail(eReport);
                    #endregion
                }
                catch (Exception ex)
                {
                    LogService.Error(ModuleName, string.Format("[{0}]记录日志和发送报告", BatchNo), ex);
                }
            }
            catch (Exception ex)
            {
                LogService.Write(ex);
            }
            finally
            {
                LastBatchNo = "";
                _ResultList = null;
                _TaskList = null;
                _WebsiteConfigs = null;
            }
        }
        #endregion

        #region Private Method
        /// <summary>
        /// 初始化服务配置
        /// </summary>
        void InitTerminalServiceConfig()
        {
            try
            {
                ICommonService commonService = new CommonService();
                _WebsiteConfigs = commonService.GetWebsiteConfigs(CrawlType.Terminal).Where(fItem => fItem.CrawlType == CrawlType.Terminal).ToList();
            }
            catch (Exception ex)
            {
                LogService.Error(ModuleName, "初始化配置", ex);
            }

        }
        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <returns></returns>
        List<TaskTerminalContainerStatus> GetTaskList()
        {
            List<TaskTerminalContainerStatus> results = null;
            Database db = GetDefaultDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("[csp].[uspGetTaskTerminalContainerStatusList]");
            DataSet ds = db.TryExecuteDataSet(dbCommand);
            if (ds == null || ds.Tables.Count < 1)
            {
                return new List<TaskTerminalContainerStatus>();
            }
            results = (from b in ds.Tables[0].AsEnumerable()
                       select new TaskTerminalContainerStatus
                       {
                           TaskID = b.Field<Guid>("TaskID"),
                           ContainerNO = b.Field<string>("ContainerNO").Trim(),
                           TerminalID = b.Field<Guid>("TerminalID"),
                           TerminalCode = b.Field<string>("TerminalCode"),
                           WebsiteID = b.Field<Guid>("WebsiteID"),
                           WebsiteCode = b.Field<string>("WebsiteCode"),
                           HandleStatus = HandleStatus.Untreated,
                       }).ToList();
            return results;
        }
        /// <summary>
        /// 获取单个任务
        /// </summary>
        /// <returns></returns>
        List<TaskTerminalContainerStatus> GetTask()
        {
            List<TaskTerminalContainerStatus> results = null;
            Database db = GetDefaultDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("[csp].[uspGetTaskTerminalContainerStatus]");
            db.AddInParameter(dbCommand, "@TaskID", DbType.Guid, TaskID);
            DataSet ds = db.TryExecuteDataSet(dbCommand);
            if (ds == null || ds.Tables.Count < 1)
            {
                return new List<TaskTerminalContainerStatus>();
            }
            results = (from b in ds.Tables[0].AsEnumerable()
                       select new TaskTerminalContainerStatus
                       {
                           TaskID = b.Field<Guid>("TaskID"),
                           ContainerNO = b.Field<string>("ContainerNO").Trim(),
                           TerminalID = b.Field<Guid>("TerminalID"),
                           TerminalCode = b.Field<string>("TerminalCode"),
                           WebsiteID = b.Field<Guid>("WebsiteID"),
                           WebsiteCode = b.Field<string>("WebsiteCode"),
                           HandleStatus = HandleStatus.Untreated,
                       }).ToList();
            return results;
        }
        /// <summary>
        /// 单个抓取数据
        /// </summary>
        /// <param name="paramTaskItem">码头信息实体</param>
        /// <param name="paramIndex"></param>
        /// <param name="crawlConfig"></param>
        /// <param name="paramTokenSource"></param>
        private void CrawlerDataByConfig(TaskTerminalContainerStatus paramTaskItem, int paramIndex, CrawlConfig crawlConfig
            , CancellationTokenSource paramTokenSource)
        {
            try
            {
                var watch = DateTime.Now;
                int curIndex = 0;
                lock (lockObj)
                {
                    CurrentIndex++;
                    curIndex = CurrentIndex;
                }
                IWCrawler wCrawler = new WCrawler(crawlConfig.Browsers, crawlConfig.ID.ToUpperString());
                wCrawler.StartAction = CrawlStartAction;
                wCrawler.ErrorAction = CrawlErrorAction;
                wCrawler.CompletedAction = CrawlCompletedAction;
                wCrawler.StopAction = StopAction;
                wCrawler.StartCrawl(paramTaskItem, crawlConfig, crawlConfig.Timeout, TS_SingleTask);
                LogService.Debug(ModuleName
                        , string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", curIndex, paramIndex, paramTaskItem.WebsiteCode
                            , paramTaskItem.ContainerNO, DateTime.Now.Subtract(watch).ToStringSeconds(), paramTaskItem.WebsiteCode)
                    );
            }
            catch (Exception ex)
            {
                LogService.Error(ModuleName, "单个抓取数据", ex);
                paramTokenSource.Cancel();
            }
        }
        /// <summary>
        /// 抓取数据
        /// </summary>
        /// <param name="paramStart"></param>
        private void CrawlStartAction(ParamStart paramStart)
        {
            CrawlByWebConfig(paramStart.WebDriver, paramStart.TaskObject, (CrawlConfig)paramStart.TaskConfig);
        }
        /// <summary>
        /// 抓取数据出现异常
        /// </summary>
        /// <param name="paramError"></param>
        private void CrawlErrorAction(ParamError paramError)
        {
            try
            {
                TaskTerminalContainerStatus taskObject = (TaskTerminalContainerStatus)paramError.TaskObject;
                if (taskObject == null) return;
                taskObject.HTMLDescription = paramError.Exception.Message;
                taskObject.HandleStatus = HandleStatus.Exception;
                string crawlIgnoreException = INIHelper.Instance.IniReadValue(CommonConstants.MODULENAME_SERVICECONFIG, CommonConstants.CONFIG_CRAWLIGNOREXCEPTION);
                if (taskObject.HTMLDescription.RegexIsMatch(crawlIgnoreException))
                {
                    taskObject.HandleStatus = HandleStatus.Failure;
                }
            }
            catch (Exception ex)
            {
                LogService.Error(ModuleName, "写入抓取异常到任务对象失败", ex);
            }
        }
        /// <summary>
        /// 抓取数据完成
        /// </summary>
        /// <param name="paramCompleted"></param>
        private void CrawlCompletedAction(ParamCompleted paramCompleted)
        {
            TaskTerminalContainerStatus taskObject = paramCompleted.TaskObject as TaskTerminalContainerStatus;
            if (taskObject == null)
            {
                throw new Exception("CompletedAction TaskTerminalContainerStatus Is Null");
            }
            CrawlConfig taskConfig = paramCompleted.TaskConfig as CrawlConfig;
            if (taskConfig == null)
            {
                throw new Exception("CompletedAction WebsiteConfig Is Null");
            }
            if (paramCompleted.PageSource.IsNullOrEmpty())
                throw new Exception("BlankPage:CompletedAction");
            string noResults = string.Empty;
            if (taskConfig.ContainsKey("EX_NoResults"))
            {
                noResults = paramCompleted.PageSource.RegexMatchString(taskConfig.GetParamValueByKey("EX_NoResults")).GetText();
                if (!noResults.IsNullOrEmpty())
                {
                    throw new Exception("NoData:" + noResults);
                }
            }
            #region 解析HTML
            var vsresult = paramCompleted.PageSource.RegexMatchHtmlTag(taskConfig.GetParamValueByKey("EX_Results"));
            if (vsresult.IsNullOrEmpty())
            {
                throw new Exception("NoData:无数据");
            }
            taskObject.HTMLContent = vsresult;
            HTMLCacheService.WriteCache(HTMLDirectory + taskConfig.WebsiteCode + @"\", "" + taskObject.TaskID, vsresult);
            #endregion
        }
        /// <summary>
        /// 抓取数据完成
        /// </summary>
        /// <param name="paramStop"></param>
        private void StopAction(ParamStop paramStop)
        {
            TaskTerminalContainerStatus taskObject = paramStop.TaskObject as TaskTerminalContainerStatus;
            if (taskObject == null)
            {
                throw new Exception("StopAction TaskTerminalContainerStatus Is Null");
            }
            string cacheFileName = "" + taskObject.WebsiteCode + taskObject.ContainerNO;
            taskObject.UpdateDate = DateTime.Now;
            if (taskObject.HTMLContent.IsNullOrEmpty() && taskObject.HTMLDescription.IsNullOrEmpty())
                LogService.Warn(ModuleName, string.Format("ID[{0}] NO[{1}] WebsiteID [{2}]", taskObject.TaskID, taskObject.ContainerNO, taskObject.WebsiteID), new Exception("无HTML及其异常"));
            else
                HTMLCacheService.WriteCache(JSONDirectory, cacheFileName, JsonConvert.SerializeObject(taskObject), true);
        }
        /// <summary>
        /// 批量保存码头数据
        /// </summary>
        /// <param name="paramTasks">抓取任务</param>
        void BatchSaveTerminalContainerDynamicHTML(DataTable paramTasks)
        {
            try
            {
                if (paramTasks == null || paramTasks.Rows.Count <= 0)
                {
                    if (IsExceptionBatch) ExceptionBatchNo = "";
                    return;
                }
                Database db = GetDefaultDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("[csp].[uspBulkSaveTerminalContainerStatusHTML]");
                dbCommand.CommandTimeout = 0;

                SqlParameter parameterTasks = new SqlParameter("@Tasks", paramTasks)
                {
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "[csp].[TerminalContainerStatusTask_Type]"
                };


                dbCommand.Parameters.Add(parameterTasks);
                db.TryExecuteNonQuery(dbCommand);
                ExceptionBatchNo = "";
            }
            catch (Exception ex)
            {
                ExceptionBatchNo = BatchNo;
                LogService.Error(ModuleName, "批量保存码头信息(HTML)", ex);
            }
        }
        #endregion
    }
}
