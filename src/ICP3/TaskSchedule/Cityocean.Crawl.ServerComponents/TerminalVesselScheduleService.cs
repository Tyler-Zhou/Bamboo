#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/4/7 13:43:16
 *
 * Description:
 *         ->
 *         相关配置
 *         LoginURL:https://www.ship.inttra.com/portal/authenticate
 *         SearchURL:https://www.ship.inttra.com/schedules/searchResults?originPortId={0}&originName={1}&destinationPortId={2}&destinationName={3}&startDate={4}&weeksOut=6&arrivalDepFlag=1&inbooking=n&surroundingPort=1
 *         
 *         查询船期传入参数含义
 *         originPortId:始发地ID
 *         originName:始发地名称
 *         destinationPortId:目的地ID
 *         destinationName:目的地名称
 *         startDate:出发日期
 *         weeksOut:出发周数
 *         arrivalDepFlag:1 按出发日期 5 按到达日期
 *         inbooking:      (n)
 *         ts:
 *         surroundingPort:周边港口([1])
 *         t:
 *         
 *         
 *         获取的JSON结果模板
 *         {"status":"success","content":[
 *              {"carrier":{"osCompanyID":"1"
 *                          ,"bookFlag":true
 *                          ,"rateFlag":true
 *                          ,"name":"MAERSK LINE"
 *                          ,"identifier":[
 *                                          {"value":"800000","type":"INTTRA_COMPANY_ID"},
 *                                          {"value":"MAEU","type":"PARTNER_ALIAS"}]}
 *              ,"vessel":"MAERSK ELBA"
 *              ,"lloydsCode":"9458078"
 *              ,"voyageNumber":"717W"
 *              ,"origin":{"identifier":[
 *                                      {"value":"CNYTN","type":"UNLOC"},{"value":"122971","type":"INTTRA_GEO_ID"}]
 *                        ,"name":"YANTIAN, GUANGDONG, CHINA"
 *                        ,"departs":"2017-05-04T05:00:00"
 *                        ,"departsActual":"0"}
 *             ,"destination":{"identifier":[
 *                                      {"value":"BEANR","type":"UNLOC"},{"value":"100161","type":"INTTRA_GEO_ID"}]
 *                        ,"name":"ANTWERPEN, ANTWERPEN, BELGIUM"
 *                        ,"arrives":"2017-05-28T22:00:00"
 *                        ,"arrivesActual":"0"}
 *             ,"transitTime":"25"
 *             ,"dqIndex":"1"
 *             ,"conveyanceReferenceNumber":""
 *             ,"contactNumber":""},
 *         ]}
 *         
 *         
 *         数据库字段与JSON字段对应
 *         CarrierName:carrier:{name:}
 *         VesselName:vessel
 *         VoyageNumber:voyageNumber
 *         OriginName:origin:{name:}
 *         OriginUnCode:origin:{identifier:{value}}
 *         DestinationName:destination:{name:}
 *         DestinationUnCode:destination:{identifier:{value}}
 *         DepartureDate:origin:{departs:}
 *         ArrivalDate:destination:{arrives:}
 *         TransitTime:transitTime
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
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Cityocean.Crawl.ServerComponents
{
    /// <summary>
    /// 服务-码头船期
    /// </summary>
    public sealed class TerminalVesselScheduleService : CrawlService, ITerminalVesselScheduleService
    {
        #region Fields
        #endregion

        #region Property
        /// <summary>
        /// 仅异常数据
        /// </summary>
        public bool OnlyAbnormalData
        {
            get;
            set;
        }

        #region Config
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate
        {
            get;
            set;
        }
        #endregion

        #region override
        /// <summary>
        /// 模块名称
        /// </summary>
        public override string ModuleName
        {
            get
            {
                return CrawlCommonConstants.MODULENAME_TERMINALVESSELSCHEDULE;
            }
        }
        #endregion

        #region TaskList
        IList<TaskTerminalVesselSchedule> _taskList;
        /// <summary>
        /// 参数集合
        /// </summary>
        IList<TaskTerminalVesselSchedule> _TaskList
        {
            get { return _taskList ?? (_taskList = new List<TaskTerminalVesselSchedule>()); }
            set { if (value == null && _taskList != null) _taskList.Clear(); _taskList = value; }
        }

        IList<TaskTerminalVesselSchedule> _resultList;
        /// <summary>
        /// 处理结果集合
        /// </summary>
        IList<TaskTerminalVesselSchedule> _ResultList
        {
            get { return _resultList ?? (_resultList = new List<TaskTerminalVesselSchedule>()); }
            set { if (value == null && _resultList != null) _resultList.Clear(); _resultList = value; }
        }
        #endregion

        #endregion

        #region Public Method
        /// <summary>
        /// 
        /// </summary>
        public TerminalVesselScheduleService()
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
                        _TaskList = GetTasks();
                        TotalCount = _TaskList.Count;
                        if (_TaskList.Count <= 0)
                        {
                            LogService.Info(ModuleName, "未找到抓取码头船期任务");
                            return;
                        }
                        BatchNo = GenerateBatchNo("CRAWL");
                        LastBatchNo = BatchNo;
                    }
                    InitServiceConfig();

                    if (_WebsiteConfigs.Count <= 0)
                    {
                        LogService.Info(ModuleName, "未找到抓取码头船期服务配置信息");
                        return;
                    }
                    TS_Task = new TimeSpan(0, 30, 0);
                    //控制任务时长
                    List<Task> taskMultiple = new List<Task>();
                    CancellationTokenSource ctsMultiple = new CancellationTokenSource();

                    IEnumerable<IGrouping<Guid, TaskTerminalVesselSchedule>> groupTaskList = _TaskList.GroupBy((fitem) => fitem.WebsiteID).OrderByDescending(fItem => fItem.Count());
                    #region Foreach Group

                    foreach (var taskListItem in groupTaskList)
                    {
                        if (!GlobalVariable.ServiceIsRuning)
                            break;

                        try
                        {
                            IGrouping<Guid, TaskTerminalVesselSchedule> listItem = taskListItem;
                            #region 验证配置
                            CrawlConfig websiteConfig = _WebsiteConfigs.SingleOrDefault(ccsItem => listItem.Key.Equals(ccsItem.ID));
                            if (websiteConfig == null)
                            {
                                LogService.Info(ModuleName,
                                    string.Format("[{0}] 配置方案ID[{1}] 未找到网页配置", BatchNo, "" + listItem.Key));
                                continue;
                            }
                            if (websiteConfig.WebsiteParams.Count <= 0)
                            {
                                LogService.Info(ModuleName,
                                    string.Format("[{0}]未找到网站[{1}]采集数据的网页配置明细", BatchNo, websiteConfig.WebsiteCode));
                                continue;
                            }
                            #endregion

                            #region 抓取数据
                            CancellationTokenSource ctsWebsite = new CancellationTokenSource();
                            Task taskWebsite = Task.Factory.StartNew(
                                    () =>
                                    {
                                        #region Foreach
                                        foreach (var taskItem2 in listItem)
                                        {
                                            if (ctsWebsite.IsCancellationRequested)
                                                return;
                                            if (!GlobalVariable.ServiceIsRuning)
                                                break;
                                            TaskTerminalVesselSchedule item = taskItem2;

                                            LogService.Info(ModuleName,
                                                string.Format("[{0}]开始抓取码头[{1}]船期数据", BatchNo, item.TerminalCode));
                                            try
                                            {
                                                CrawlTaskMemoryCache.Add(websiteConfig.ID,
                                                    string.Format("正在从网站[{0}]抓取码头[{1}]船期数据", websiteConfig.WebsiteCode,
                                                        taskItem2.TerminalCode));

                                                CrawlerDataByConfig(item, websiteConfig, ctsWebsite);
                                            }
                                            finally
                                            {
                                                CrawlTaskMemoryCache.Remove(websiteConfig.ID);
                                            }
                                        }
                                        #endregion
                                    }, ctsWebsite.Token);
                            #endregion

                            #region 结束记录

                            taskWebsite.ContinueWith((paramTask) =>
                            {
                                try
                                {
                                    if (paramTask.Exception != null)
                                    {
                                        paramTask.Exception.Handle((inner) =>
                                        {
                                            LogService.Info(ModuleName,
                                                string.Format("[{0}] 网站[{1}] 船期数据抓取失败:{2}", BatchNo, websiteConfig.WebsiteCode,
                                                    inner.Message));
                                            return true;
                                        });
                                    }
                                    else
                                    {
                                        LogService.Info(ModuleName,
                                            string.Format("[{0}] 网站[{1}] 船期数据抓取完成", BatchNo, websiteConfig.WebsiteCode));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    LogService.Error(ModuleName, "TaskWebsite ContinueWith", ex);
                                }
                            }, CancellationToken.None);

                            #endregion
                            taskMultiple.Add(taskWebsite);

                        }
                        catch (AggregateException aEx)
                        {
                            aEx.Handle((inner) =>
                            {
                                LogService.Info(ModuleName,"多任务多异常:", inner);
                                return true;
                            });
                        }
                        catch (Exception ex)
                        {
                            LogService.Error(ModuleName,"多任务异常", ex);
                        }
                        if (!Task.WaitAll(taskMultiple.ToArray(), (int) TS_Task.TotalMilliseconds, ctsMultiple.Token))
                        {
                            ctsMultiple.Cancel();
                        }
                    }
                    #endregion

                    #endregion

                }
                catch (Exception ex)
                {
                    LogService.Error(ModuleName, string.Format("[{0}]抓取码头船期数据", BatchNo), ex);
                }
                try
                {
                    #region Batch Save

                    DataTable dtConfig = new List<TaskTerminalVesselSchedule>().ToDataTable();

                    if ((JSONDirectory).IsExistsDirectory())
                    {
                        List<TaskTerminalVesselSchedule> configs = BuildCrawlResults<TaskTerminalVesselSchedule>(JSONDirectory, false);
                        dtConfig = configs.Where(fItem => fItem.HandleStatus > HandleStatus.Untreated).ToDataTable();
                        _ResultList = configs;
                    }
                    BatchSaveSailingScheduleHTML(dtConfig);
                    #endregion
                    
                }
                catch (Exception ex)
                {
                    LogService.Error(ModuleName, string.Format("[{0}] 批量保存码头船期", BatchNo), ex);
                }
                try
                {
                    #region Log & Report
                    if (_ResultList.Count <= 0) return;

                    EReportInfo eReport = new EReportInfo(string.Format("CSP.TVSR:码头船期报告-{0}", BatchNo));

                    StringBuilder sbContent = new StringBuilder();
                    string strLog = string.Format("码头总数:{0}\t 执行数量:{1}\t 完成数:{2}\t 失败数量:{3}\t 总耗时:{4}",
                        TotalCount,
                        _ResultList.Count(),
                        _ResultList.Count(fitem => fitem.HandleStatus == HandleStatus.Complete),
                        _ResultList.Count(fitem => fitem.HandleStatus == HandleStatus.Exception),
                        DateTime.Now.Subtract(JobDateTime).ToString(@"hh\:mm\:ss"));
                    sbContent.AppendFormat("<h4>{0}</h4><br />", strLog);

                    var resultGroups = _ResultList.GroupBy(gItem => gItem.TerminalCode).ToArray();
                    if (resultGroups.Any())
                    {
                        sbContent.AppendFormat(
                            "<table  border='1'><tr><th>码头编码</th><th>执行数量</th><th>完成数</th><th>失败数量</th></tr>");
                        foreach (var groupItem in resultGroups)
                        {
                            var tempList = groupItem.ToArray();
                            sbContent.AppendFormat(
                                "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>"
                                , groupItem.Key
                                , tempList.Count()
                                , tempList.Count(fitem => fitem.HandleStatus == HandleStatus.Complete)
                                , tempList.Count(fitem => fitem.HandleStatus == HandleStatus.Exception)
                                );
                        }
                        sbContent.Append("</table><br />");
                    }

                    var errorData = _ResultList.Where(fItem => fItem.HandleStatus == HandleStatus.Exception).ToArray();
                    if (errorData.Any())
                    {
                        sbContent.Append("异常数据:<br />");
                        sbContent.AppendFormat(
                            "<table  border='1'><tr><th>网站编码</th><th>码头编码</th><th>异常</th></tr>");
                        foreach (var errorItem in errorData)
                        {
                            sbContent.AppendFormat(
                                "<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>"
                                , errorItem.WebsiteCode
                                , errorItem.TerminalCode
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
                    LogService.Error(ModuleName, string.Format("[{0}] 记录日志和发送报告时", BatchNo), ex);
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

        /// <summary>
        /// 解析数据
        /// </summary>
        public void AnalysisData()
        {
            try
            {
                _TaskList = GetAnalysisTasks();
                TotalCount = _TaskList.Count;
                if (TotalCount <= 0)
                {
                    LogService.Info(ModuleName, "未找到解析码头船期任务");
                    return;
                }
                BatchNo = GenerateBatchNo("ANALYSIS");
                LogService.Info(ModuleName, string.Format("[{0}] 待解析码头船期总数 [{1}]", BatchNo, TotalCount));
                InitServiceConfig();
                if (_WebsiteConfigs.Count <= 0)
                {
                    LogService.Info(ModuleName, "未找到解析码头船期服务配置信息");
                    return;
                }
                List<TerminalVesselSchedule> vesselScheduleList = new List<TerminalVesselSchedule>();
                IEnumerable<IGrouping<Guid, TaskTerminalVesselSchedule>> groupTaskList = _TaskList.GroupBy((fitem) => fitem.WebsiteID).OrderByDescending(fItem => fItem.Count());

                #region Foreach Group
                foreach (var taskListItem in groupTaskList)
                {
                    if (!GlobalVariable.ServiceIsRuning)
                        break;

                    CrawlConfig websiteConfig = _WebsiteConfigs.SingleOrDefault(fItem => fItem.ID.Equals(taskListItem.Key));
                    if (websiteConfig == null)
                        continue;

                    LogService.Info(ModuleName, string.Format("[{0}] 网页编码[{1}] 合计[{2}]", BatchNo, websiteConfig.WebsiteCode, taskListItem.Count()));

                    foreach (var taskItem in taskListItem.OrderBy(oItem => oItem.CreateDate))
                    {
                        try
                        {
                            if (!GlobalVariable.ServiceIsRuning)
                                break;

                            #region 解析结果集

                            MatchCollection mc =
                                taskItem.HTMLContent.RegexMatchesSingleline(websiteConfig.GetParamValueByKey("EX_Data"));

                            foreach (Match m in mc)
                            {
                                string inVoyageNo = m.Groups["InVoyageNumber"].Value.GetText();
                                string outVoyageNo = m.Groups["InVoyageNumber"].Value.GetText();
                                TerminalVesselSchedule tvs = new TerminalVesselSchedule
                                {
                                    TerminalID = taskItem.TerminalID,
                                    VesselName = m.Groups["VesselName"].Value.GetText(),
                                    InVoyageNo = inVoyageNo,
                                    InVoyageNumber = inVoyageNo.RegexReplace(@"^[\d]*[a-z]*[^\d]*([\d]+)$", "$1"),
                                    InVoyageDirection = inVoyageNo.RegexReplace(@"[^a-z]", ""),
                                    OutVoyageNo = outVoyageNo,
                                    OutVoyageNumber = outVoyageNo.RegexReplace(@"^[\d]*[a-z]*[^\d]*([\d]+)$", "$1"),
                                    OutVoyageDirection = outVoyageNo.RegexReplace(@"[^a-z]", ""),
                                    UpdateDate = taskItem.CreateDate,
                                };

                                #region ArrivalDate

                                if (m.Groups["ArrivalDate"].Value.IsNullOrEmpty())
                                {
                                    DateTime? eta = m.Groups["ETA"].Value.GetText().ToDateTimeNull();
                                    DateTime? ata = m.Groups["ATA"].Value.GetText().ToDateTimeNull();
                                    tvs.ArrivalDate = eta;
                                    if (ata.HasValue)
                                    {
                                        tvs.ArrivalDate = ata;
                                    }
                                }
                                else
                                {
                                    tvs.ArrivalDate = m.Groups["ArrivalDate"].Value.GetText().ToDateTimeNull();
                                }

                                #endregion

                                #region DepartureDate

                                if (m.Groups["DepartureDate"].Value.IsNullOrEmpty())
                                {
                                    DateTime? etd = m.Groups["ETD"].Value.GetText().ToDateTimeNull();
                                    DateTime? atd = m.Groups["ATD"].Value.GetText().ToDateTimeNull();
                                    tvs.DepartureDate = etd;
                                    if (atd.HasValue)
                                    {
                                        tvs.DepartureDate = atd;
                                    }
                                }
                                else
                                {
                                    tvs.DepartureDate = m.Groups["DepartureDate"].Value.GetText().ToDateTimeNull();
                                }

                                #endregion

                                taskItem.ListContent.Add(tvs);
                            }
                            if (taskItem.ListContent.Count > 0)
                            {
                                taskItem.JSONContent = JsonConvert.SerializeObject(taskItem.ListContent);
                                HTMLCacheService.WriteCache(JSONDirectory + @"Detail\", taskItem.TerminalCode,
                                    taskItem.JSONContent, true);
                            }

                            #endregion
                            taskItem.HandleStatus = HandleStatus.Complete;
                            vesselScheduleList.AddRange(taskItem.ListContent);
                        }
                        catch (Exception ex)
                        {
                            taskItem.JSONDescription = ex.Message;
                            taskItem.HandleStatus = HandleStatus.Exception;
                            LogService.Error(ModuleName, "AnalysisData Foreach Detail", ex);
                            break;
                        }
                        finally
                        {
                            taskItem.UpdateDate = DateTime.Now;
                        }
                    }
                }

                #endregion

                BatchSaveSailingSchedule(_TaskList.Where(fItem => fItem.HandleStatus != HandleStatus.Untreated).ToDataTable(),
                    vesselScheduleList.ToDataTable());
            }
            catch (Exception ex)
            {
                LogService.Error(ModuleName, "解析数据", ex);
            }
        }
        #endregion

        #region Private Method
        /// <summary>
        /// 初始化服务配置
        /// </summary>
        void InitServiceConfig()
        {
            try
            {
                ICommonService commonService = new CommonService();
                _WebsiteConfigs = commonService.GetWebsiteConfigs(CrawlType.SailingSchedule).Where(fItem => fItem.CrawlType == CrawlType.SailingSchedule).ToList();
            }
            catch (Exception ex)
            {
                LogService.Error(ModuleName, "初始化配置", ex);
            }

        }

        #region Crawl Task
        /// <summary>
        /// 获取查询船期所需参数
        /// </summary>
        /// <returns>查询船期参数列表</returns>
        List<TaskTerminalVesselSchedule> GetTasks()
        {
            List<TaskTerminalVesselSchedule> results = null;
            Database db = GetDefaultDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("[csp].[uspGetTaskTerminalVesselScheduleList]");
            DataSet ds = db.TryExecuteDataSet(dbCommand);
            if (ds == null || ds.Tables.Count < 1)
            {
                return new List<TaskTerminalVesselSchedule>();
            }
            results = (from b in ds.Tables[0].AsEnumerable()
                       select new TaskTerminalVesselSchedule
                       {
                           TaskID = b.Field<Guid>("TaskID"),
                           TerminalID = b.Field<Guid>("TerminalID"),
                           TerminalCode = b.Field<string>("TerminalCode"),
                           WebsiteID = b.Field<Guid>("WebSiteID"),
                           WebsiteCode = b.Field<string>("WebsiteCode"),
                           HandleStatus = HandleStatus.Untreated,
                       }).ToList();
            return results;
        }
        /// <summary>
        /// 单条路线抓取航期
        /// </summary>
        /// <param name="paramTaskItem">任务实体</param>
        /// <param name="crawlConfig"></param>
        /// <param name="paramTokenSource"></param>
        void CrawlerDataByConfig(TaskTerminalVesselSchedule paramTaskItem, CrawlConfig crawlConfig
            , CancellationTokenSource paramTokenSource)
        {
            try
            {
                if (paramTokenSource.IsCancellationRequested)
                    return;
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
                        , string.Format("{0}\t{1}\t{2}\t{3}", curIndex, paramTaskItem.TerminalCode
                            , DateTime.Now.Subtract(watch).ToStringSeconds(), paramTaskItem.WebsiteCode)
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
            try
            {
                CrawlByWebConfig(paramStart.WebDriver, paramStart.TaskObject, (CrawlConfig)paramStart.TaskConfig);
            }
            catch (Exception ex)
            {
                LogService.Error(ModuleName, "抓取数据", ex);
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 抓取数据出现异常
        /// </summary>
        /// <param name="paramError"></param>
        private void CrawlErrorAction(ParamError paramError)
        {
            try
            {
                TaskTerminalVesselSchedule taskObject = (TaskTerminalVesselSchedule)paramError.TaskObject;
                if (taskObject == null) return;
                taskObject.HandleStatus = HandleStatus.Exception;
                taskObject.HTMLDescription = paramError.Exception.Message;

                LogService.Error(ModuleName
                    , string.Format("码头[{0}]进程ID[{1}]异常类型[{2}]", taskObject.TerminalCode, paramError.ProcessId, paramError.Exception.GetType())
                    ,paramError.Exception);
            }
            catch (Exception ex)
            {
                LogService.Error(ModuleName, "写入抓取异常到任务对象", ex);
            }
        }
        /// <summary>
        /// 抓取数据完成
        /// </summary>
        /// <param name="paramCompleted"></param>
        private void CrawlCompletedAction(ParamCompleted paramCompleted)
        {
            try
            {
                if (paramCompleted.PageSource.IsNullOrEmpty())
                    throw new Exception("BlankPage:CompletedAction");

                TaskTerminalVesselSchedule taskObject = paramCompleted.TaskObject as TaskTerminalVesselSchedule;
                if (taskObject == null)
                {
                    throw new Exception("CompletedAction TaskTerminalVesselSchedule Is Null");
                }
                CrawlConfig taskConfig = paramCompleted.TaskConfig as CrawlConfig;
                if (taskConfig == null)
                {
                    throw new Exception("CompletedAction WebsiteConfig Is Null");
                }
                string noResults = string.Empty;
                if (taskConfig.ContainsKey("EX_NoResults"))
                {
                    noResults = paramCompleted.PageSource.RegexMatchString(taskConfig.GetParamValueByKey("EX_NoResults")).GetText();
                    if (!noResults.IsNullOrEmpty())
                    {
                        throw new Exception("NoData:未找到船期数据");
                    }
                }
                #region 获取页面HTML源代码
                IWebElement nextPageElement = null;
                do
                {
                    bool PageIsCompleted = !taskConfig.ContainsKey("EX_PageCompleted");
                    if (!PageIsCompleted)
                    {
                        CrawlConfigParam pageCompleted = taskConfig.GetWebsiteParamByKey("EX_PageCompleted");
                        PageIsCompleted = paramCompleted.WebDriver.WaitElementsLoad(pageCompleted.ParamValue,
                            pageCompleted.Timeout);
                    }
                    if (PageIsCompleted)
                    {
                        var vsresult =
                           paramCompleted.PageSource.RegexMatchHtmlTag(taskConfig.GetParamValueByKey("EX_Results"));
                        taskObject.HTMLContent += vsresult;



                        CrawlConfigParam nextPage = taskConfig.GetWebsiteParamByKey("EX_NextPage");
                        if (nextPage != null)
                        {
                            nextPageElement = paramCompleted.WebDriver.WaitFindElement(nextPage.ParamValue, false, false,
                                nextPage.Timeout);
                            if (nextPageElement != null)
                            {
                                nextPageElement.Click();
                            }
                        }
                    }
                } while (nextPageElement != null);
                #endregion

                #region 解析结果集
                //MatchCollection mc = taskObject.HTMLContent.RegexMatchesSingleline(taskConfig.GetParamValueByKey("EX_Data"));

                //foreach (Match m in mc)
                //{
                //    TerminalVesselSchedule tvs = new TerminalVesselSchedule
                //    {
                //        TerminalID = taskObject.TerminalID,
                //        VesselName = m.Groups["VesselName"].Value.GetText(),
                //        InVoyageNumber = m.Groups["InVoyageNumber"].Value.GetText(),
                //        OutVoyageNumber = m.Groups["OutVoyageNumber"].Value.GetText(),
                //        UpdateDate = DateTime.Now,
                //    };

                //    #region ArrivalDate

                //    if (m.Groups["ArrivalDate"].Value.IsNullOrEmpty())
                //    {
                //        DateTime? eta = m.Groups["ETA"].Value.GetText().ToDateTimeNull();
                //        DateTime? ata = m.Groups["ATA"].Value.GetText().ToDateTimeNull();
                //        tvs.ArrivalDate = eta;
                //        if (ata.HasValue)
                //        {
                //            tvs.ArrivalDate = ata;
                //        }
                //    }
                //    else
                //    {
                //        tvs.ArrivalDate = m.Groups["ArrivalDate"].Value.GetText().ToDateTimeNull();
                //    }

                //    #endregion

                //    #region DepartureDate

                //    if (m.Groups["DepartureDate"].Value.IsNullOrEmpty())
                //    {
                //        DateTime? etd = m.Groups["ETD"].Value.GetText().ToDateTimeNull();
                //        DateTime? atd = m.Groups["ATD"].Value.GetText().ToDateTimeNull();
                //        tvs.DepartureDate = etd;
                //        if (atd.HasValue)
                //        {
                //            tvs.DepartureDate = atd;
                //        }
                //    }
                //    else
                //    {
                //        tvs.DepartureDate = m.Groups["DepartureDate"].Value.GetText().ToDateTimeNull();
                //    }

                //    #endregion

                //    taskObject.ListContent.Add(tvs);
                //}
                #endregion
                taskObject.HandleStatus = HandleStatus.Complete;
            }
            catch (Exception ex)
            {
                LogService.Error(ModuleName, "提取有效HTML数据", ex);
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 抓取数据完成
        /// </summary>
        /// <param name="paramStop"></param>
        private void StopAction(ParamStop paramStop)
        {
            TaskTerminalVesselSchedule taskObject = paramStop.TaskObject as TaskTerminalVesselSchedule;
            if (taskObject == null)
            {
                throw new Exception("StopAction TaskTerminalVesselSchedule Is Null");
            }
            taskObject.UpdateDate = DateTime.Now;
            if (taskObject.HTMLContent.IsNullOrEmpty() && taskObject.HTMLDescription.IsNullOrEmpty())
                LogService.Warn(ModuleName, string.Format("ID[{0}] TerminalCode[{1}] WebsiteID [{2}]", taskObject.TaskID, taskObject.TerminalCode, taskObject.WebsiteID), new Exception("无HTML及其异常"));
            else
                HTMLCacheService.WriteCache(JSONDirectory, taskObject.TerminalCode, JsonConvert.SerializeObject(taskObject), true);
        }
        /// <summary>
        /// 批量保存码头数据
        /// </summary>
        /// <param name="paramTask">任务配置信息</param>
        void BatchSaveSailingScheduleHTML(DataTable paramTask)
        {
            if (paramTask == null || paramTask.Rows.Count <= 0)
            {
                if (IsExceptionBatch) ExceptionBatchNo = "";
                return;
            }
            try
            {
                Database db = GetDefaultDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("[csp].[uspBulkSaveTerminalVesselScheduleHTML]");
                dbCommand.CommandTimeout = 0;

                SqlParameter parameterTask = new SqlParameter("@CrawlTask", paramTask)
                {
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "[csp].[TerminalVesselScheduleTask_Type]"
                };

                dbCommand.Parameters.Add(parameterTask);
                db.TryExecuteNonQuery(dbCommand);
                ExceptionBatchNo = "";
            }
            catch (Exception ex)
            {
                ExceptionBatchNo = BatchNo;
                LogService.Error(ModuleName, "批量保存码头船期", ex);
            }
        }
        #endregion

        #region Analysis Data Task
        /// <summary>
        /// 获取查询船期所需参数
        /// </summary>
        /// <returns>查询船期参数列表</returns>
        List<TaskTerminalVesselSchedule> GetAnalysisTasks()
        {
            List<TaskTerminalVesselSchedule> results = null;
            Database db = GetDefaultDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("[csp].[uspGetTaskAnalysisTerminalVesselScheduleList]");
            DataSet ds = db.TryExecuteDataSet(dbCommand);
            if (ds == null || ds.Tables.Count < 1)
            {
                return new List<TaskTerminalVesselSchedule>();
            }
            results = (from b in ds.Tables[0].AsEnumerable()
                       select new TaskTerminalVesselSchedule
                       {
                           TaskID = b.Field<Guid>("TaskID"),
                           TerminalID = b.Field<Guid>("TerminalID"),
                           TerminalCode = b.Field<string>("TerminalCode"),
                           HTMLContent = b.Field<string>("HTMLContent"),
                           WebsiteID = b.Field<Guid>("WebSiteID"),
                           WebsiteCode = b.Field<string>("WebsiteCode"),
                           CreateDate = b.Field<DateTime>("CreateDate"),
                           HandleStatus = HandleStatus.Untreated,
                       }).ToList();
            return results;
        }
        /// <summary>
        /// 批量保存码头数据
        /// </summary>
        /// <param name="paramTask">任务配置信息</param>
        /// <param name="paramData">抓取到的数据</param>
        void BatchSaveSailingSchedule(DataTable paramTask, DataTable paramData)
        {
            if (paramTask == null || paramTask.Rows.Count <= 0)
            {
                return;
            }
            if (paramData == null)
            {
                return;
            }
            try
            {
                Database db = GetDefaultDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("[csp].[uspBulkSaveTerminalVesselSchedule]");
                dbCommand.CommandTimeout = 0;

                SqlParameter parameterTask = new SqlParameter("@CrawlTask", paramTask)
                {
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "[csp].[TerminalVesselScheduleTask_Type]"
                };

                SqlParameter parameterData = new SqlParameter("@CrawlData", paramData)
                {
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "[csp].[TerminalVesselScheduleData_Type]"
                };

                dbCommand.Parameters.Add(parameterTask);
                dbCommand.Parameters.Add(parameterData);
                db.TryExecuteNonQuery(dbCommand);
                ExceptionBatchNo = "";
            }
            catch (Exception ex)
            {
                ExceptionBatchNo = BatchNo;
                LogService.Error(ModuleName, "批量保存码头船期", ex);
            }
        }
        #endregion
        #endregion
    }
}
