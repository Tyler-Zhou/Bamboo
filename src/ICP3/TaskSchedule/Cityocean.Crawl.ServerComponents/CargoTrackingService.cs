#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/4/10 15:33:27
 *
 * Description:
 *         ->
 *         标记为完成: 0. 最后一个动态描述有还空柜
 *                    1. 上一次获取的最后一个动态描述是提重柜，这次的第一动态描述是提空柜或装船
 *                    2. 最后一个动态描述是提重柜或卸船的时间超过卸船超期值(天:默认30)
 *
 * History:
 *         ->
 */
#endregion

using System.Collections.Concurrent;
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
    /// 货物动态服务
    /// </summary>
    public sealed class CargoTrackingService : CrawlService, ICargoTrackingService
    {
        #region Fields
        #endregion

        #region Property

        /// <summary>
        /// 动态有效天数
        /// </summary>
        public int DynamicValidDays
        {
            get { return 180; }
        }

        private Guid _SingleID = Guid.Empty;
        /// <summary>
        /// 箱号
        /// </summary>
        public Guid SingleID
        {
            get { return _SingleID; }
            set { _SingleID = value; }
        }

        #region Config
        /// <summary>
        /// 预计时间之前天数(Estimated Time Before Days)
        /// </summary>
        public int ESTBeforeDays
        {
            get
            {
                return Convert.ToInt32(GetConfigValue(CargoTrackingConstants.CONFIG_ESTBEFOREDAYS, "3"));
            }
        }
        /// <summary>
        /// 预计交货时间范围
        /// </summary>
        public int ETDRange
        {
            get
            {
                return Convert.ToInt32(GetConfigValue(CargoTrackingConstants.CONFIG_ETDRANGE, "7"));
            }
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
                return CrawlCommonConstants.MODULENAME_CARGOTRACKING;
            }
        }
        #endregion

        #region Task List
        IList<TaskCargoTracking> _taskList;
        /// <summary>
        /// 参数集合
        /// </summary>
        IList<TaskCargoTracking> _TaskList
        {
            get { return _taskList ?? (_taskList = new List<TaskCargoTracking>()); }
            set { if (value == null && _taskList != null) _taskList.Clear(); _taskList = value; }
        }

        IList<TaskCargoTracking> _resultList;
        /// <summary>
        /// 处理结果集合
        /// </summary>
        IList<TaskCargoTracking> _ResultList
        {
            get { return _resultList ?? (_resultList = new List<TaskCargoTracking>()); }
            set { if (value == null && _resultList != null) _resultList.Clear(); _resultList = value; }
        }

        IList<TaskAnalysisCargoTracking> _analysistaskList;
        /// <summary>
        /// 参数集合
        /// </summary>
        IList<TaskAnalysisCargoTracking> _AnalysisTaskList
        {
            get { return _analysistaskList ?? (_analysistaskList = new List<TaskAnalysisCargoTracking>()); }
            set { if (value == null && _analysistaskList != null) _analysistaskList.Clear(); _analysistaskList = value; }
        }
        #endregion
        #endregion

        #region Public Method
        /// <summary>
        /// 构造函数
        /// </summary>
        public CargoTrackingService()
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
                    if (IsExceptionBatch)
                    {
                        LogService.Info(ModuleName, string.Format("[{0}] 异常数据处理", BatchNo));
                        _TaskList.Clear();
                        _ResultList.Clear();
                    }
                    else
                    {
                        _TaskList.Clear();
                        _TaskList = SingleID != Guid.Empty ? GetTask() : GetTasks();
                        TotalCount = _TaskList.Count;
                        if (TotalCount <= 0)
                        {
                            LogService.Info(ModuleName, "未找到抓取箱动态任务");
                            return;
                        }
                        _ResultList.Clear();
                        BatchNo = GenerateBatchNo("CRAWL");
                        LastBatchNo = BatchNo;
                        LogService.Info(ModuleName, string.Format("[{0}] 待抓取箱总数 [{1}]", BatchNo, TotalCount));
                    }
                    InitCargoTrackingServiceConfig();
                    if (_WebsiteConfigs.Count <= 0)
                    {
                        LogService.Info(ModuleName, "未找到抓取箱动态服务配置信息");
                        return;
                    }

                    #region Group by WebsiteID

                    TS_Task = new TimeSpan(3, 0, 0);

                    List<Task> taskMultiple = new List<Task>();
                    CancellationTokenSource ctsMultiple = new CancellationTokenSource();
                    try
                    {
                        #region Foreach Code Group
                        var gCodeList = _TaskList.GroupBy(gItem => gItem.WebsiteCode);
                        foreach (var gCodeListItem in gCodeList)
                        {
                            CancellationTokenSource ctsWebsite = new CancellationTokenSource();
                            IGrouping<string, TaskCargoTracking> igCodeListItem = gCodeListItem;

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

                                #region 按ETD顺序抓取数据
                                IGrouping<Guid, TaskCargoTracking> igIDItemList = gIDItemList;
                                var oTaskList = igIDItemList.ToList().OrderBy(oItem => oItem.ETD).ToList();
                                int forIndex = 1;
                                Task taskCrawl = taskBeginLog.ContinueWith((paramTask) =>
                                {
                                    try
                                    {
                                        foreach (var taskItem2 in oTaskList.TakeWhile(taskItem2 => !ctsWebsite.IsCancellationRequested && GlobalVariable.ServiceIsRuning))
                                        {
                                            try
                                            {
                                                CrawlTaskMemoryCache.Add(websiteConfig.ID, string.Format("正在从网站[{0}]抓取箱[{1}]动态", websiteConfig.WebsiteCode
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

                    #endregion

                    #endregion
                }
                catch (Exception ex)
                {
                    LogService.Error(ModuleName, string.Format("[{0}]抓取数据", BatchNo), ex);
                }

                BatchSaveContainerDynamicHTML();

                CrawlReport();
                LastBatchNo = "";
            }
            catch (Exception ex)
            {
                LogService.Write(ex);
            }
            finally
            {
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
                _AnalysisTaskList = SingleID != Guid.Empty ? GetAnalysisTask() : GetAnalysisTasks();
                TotalCount = _AnalysisTaskList.Count;
                if (TotalCount <= 0)
                {
                    LogService.Info(ModuleName, "未找到解析箱动态任务");
                    return;
                }
                BatchNo = GenerateBatchNo("ANALYSIS");
                LogService.Info(ModuleName, string.Format("[{0}] 待解析箱总数 [{1}]", BatchNo, TotalCount));
                InitCargoTrackingServiceConfig();

                if (_WebsiteConfigs.Count <= 0)
                {
                    LogService.Info(ModuleName, "未找到解析箱动态服务配置信息");
                    return;
                }
                LogService.Info(ModuleName, string.Format("[{0}] 开始解析", BatchNo));
                try
                {
                    IEnumerable<IGrouping<Guid, TaskAnalysisCargoTracking>> groupTaskList = _AnalysisTaskList.GroupBy((fitem) => fitem.WebsiteID).OrderByDescending(fItem => fItem.Count());
                    TS_Task = new TimeSpan(2, 0, 0);
                    List<Task> taskMultiple = new List<Task>();
                    CancellationTokenSource ctsMultiple = new CancellationTokenSource();

                    #region Foreach Group
                    foreach (var groupListItem in groupTaskList)
                    {
                        if (!GlobalVariable.ServiceIsRuning)
                            break;
                        CrawlConfig websiteConfig = _WebsiteConfigs.SingleOrDefault(fItem => fItem.ID.Equals(groupListItem.Key));
                        if (websiteConfig == null)
                            continue;
                        LogService.Info(ModuleName, string.Format("[{0}] 网页编码[{1}] 合计[{2}]", BatchNo, websiteConfig.WebsiteCode, groupListItem.Count()));

                        IGrouping<Guid, TaskAnalysisCargoTracking> vargroupListItem = groupListItem;
                        Task analysisTask = Task.Factory.StartNew(() =>
                        {
                            #region Analysis Data
                            foreach (TaskAnalysisCargoTracking taskItem in vargroupListItem)
                            {
                                if (!GlobalVariable.ServiceIsRuning)
                                    break;
                                try
                                {
                                    List<ContainerDynamic> resultList = new List<ContainerDynamic>();
                                    CrawlConfigParam analysisConfigID = websiteConfig.GetWebsiteParamByKey("EX_Data");
                                    //EMC MBL单号非EGLV开头
                                    if (websiteConfig.ID.EqualsString("8A84D219-8692-450A-B120-933B4D14D4A5") &&
                                        taskItem.MBLNO.Contains("EGLV"))
                                    {
                                        analysisConfigID = websiteConfig.GetWebsiteParamByKey("EX_Data2");
                                    }

                                    if (websiteConfig.ID.EqualsString("7461697D-1F07-4FDF-9268-DB6C8E4E91A5"))
                                    {
                                        #region MSK Analysis

                                        MatchCollection mcMsk =
                                            taskItem.HTMLContent.RegexMatchesSingleline(analysisConfigID.ParamValue);
                                        string strLocal = "";
                                        int eventIndexMSK = 0;
                                        foreach (Match m in mcMsk)
                                        {
                                            #region Foreach Regex Match

                                            #region Event DateTime

                                            DateTime eventDateTime = (m.Groups[CargoTrackingConstants.CELL_EVENTDATE].Value.GetText()
                                                .ToDateTime()
                                                .ToString("yyyy-MM-dd")
                                                                      + " " +
                                                                      m.Groups[CargoTrackingConstants.CELL_EVENTTIME].Value.GetText()
                                                                          .ToDateTime()
                                                                          .ToString("HH:mm:ss")
                                                ).ToDateTime();

                                            #endregion

                                            eventIndexMSK++;
                                            string strStation =
                                                m.Groups[CargoTrackingConstants.CELL_STATION].Value.GetText();
                                            if (!strStation.IsNullOrEmpty())
                                                strLocal = strStation;

                                            ContainerDynamic ecd = new ContainerDynamic
                                            {
                                                ParentID = taskItem.ContainerID,
                                                EventIndex = eventIndexMSK,
                                                EventTime = eventDateTime,
                                                StateDescription = m.Groups[CargoTrackingConstants.CELL_STATEDESCRIPTION].Value.GetText(),
                                                Station = strLocal,
                                                VesselName = m.Groups[CargoTrackingConstants.CELL_VESSELNAME].Value.GetText(),
                                                VoyageNumber = m.Groups[CargoTrackingConstants.CELL_VOYAGENUMBER].Value.GetText(),
                                                IsEST =
                                                    websiteConfig.ContainsKey("EX_Est") &&
                                                    Regex.IsMatch(m.Groups[CargoTrackingConstants.CELL_EST].Value.GetText(),
                                                        websiteConfig.GetParamValueByKey("EX_Est")),
                                            };
                                            ecd.State = GetContainerStateByDesc(ecd.StateDescription, websiteConfig);
                                            resultList.Add(ecd);

                                            #endregion
                                        }

                                        #endregion
                                    }
                                    else
                                    {
                                        #region Common Analysis

                                        MatchCollection mc =
                                            taskItem.HTMLContent.RegexMatchesSingleline(analysisConfigID.ParamValue);

                                        int eventIndex = 0;
                                        ContainerState beforeCState = ContainerState.UnKnown;
                                        foreach (Match m in mc)
                                        {
                                            #region Foreach Regex Match

                                            #region Event DateTime

                                            string strEventDateTime = m.Groups[CargoTrackingConstants.CELL_EVENTDATETIME].Value.GetText();
                                            if (strEventDateTime.IsNullOrEmpty())
                                            {
                                                string strDate = m.Groups[CargoTrackingConstants.CELL_EVENTDATE].Value.GetText();
                                                string strTime = m.Groups[CargoTrackingConstants.CELL_EVENTTIME].Value.GetText();
                                                strEventDateTime = strDate.ToDateTime().ToString("yyyy-MM-dd")
                                                                   + ""
                                                                   + strTime.ToDateTime().ToString("HH:mm:ss");
                                            }
                                            DateTime eventDateTime;
                                            if (websiteConfig.ContainsKey("EX_DateFormat"))
                                            {
                                                eventDateTime =
                                                    strEventDateTime.ToDateTime(
                                                        websiteConfig.GetParamValueByKey("EX_DateFormat"),
                                                        websiteConfig.GetParamValueByKey("EX_CultureName"));
                                            }
                                            else
                                            {
                                                eventDateTime = strEventDateTime.ToDateTime();
                                            }

                                            #endregion

                                            eventIndex++;
                                            ContainerDynamic ecd = new ContainerDynamic
                                            {
                                                ParentID = taskItem.ContainerID,
                                                EventIndex = eventIndex,
                                                EventTime = eventDateTime,
                                                StateDescription = m.Groups[CargoTrackingConstants.CELL_STATEDESCRIPTION].Value.GetText(),
                                                Station = m.Groups[CargoTrackingConstants.CELL_STATION].Value.GetText(),
                                                VesselName = m.Groups[CargoTrackingConstants.CELL_VESSELNAME].Value.GetText(),
                                                VoyageNumber = m.Groups[CargoTrackingConstants.CELL_VOYAGENUMBER].Value.GetText(),
                                            };

                                            if (websiteConfig.ContainsKey("EX_Est"))
                                            {
                                                string est = m.Groups[CargoTrackingConstants.CELL_EST].Value.GetText();
                                                string ex_EST = websiteConfig.GetParamValueByKey("EX_Est");
                                                if (ex_EST.IsNullOrEmpty())
                                                {
                                                    ecd.IsEST = est.IsNullOrEmpty() && ex_EST.IsNullOrEmpty();
                                                }
                                                else
                                                    ecd.IsEST = Regex.IsMatch(est,
                                                        websiteConfig.GetParamValueByKey("EX_Est"));
                                            }
                                            else
                                                ecd.IsEST = false;

                                            ecd.State = GetContainerStateByDesc(ecd.StateDescription, websiteConfig);
                                            //HPL状态验证:运输方式为Truck时提重柜验证表达式有效
                                            if (websiteConfig.ID.EqualsString("798B7C3C-CD7B-4AA1-ACAC-8ADFDD64D38B"))
                                            {
                                                var model = m.Groups[CargoTrackingConstants.CELL_MODEL].Value.GetText();
                                                if (ecd.State == ContainerState.FullPickUp &&
                                                    !"TRUCK".Equals(model.ToUpper()))
                                                {
                                                    ecd.State = ContainerState.UnKnown;
                                                }
                                            }
                                            //TSL 状态验证:之前状态为提重柜时还空验证表达式有效
                                            if (websiteConfig.ID.EqualsString("CAFF54C4-7211-45C3-B97C-F6ADA75FCE9B"))
                                            {
                                                if (ecd.State == ContainerState.REC &&
                                                    beforeCState != ContainerState.FullPickUp)
                                                    ecd.State = ContainerState.UnKnown;
                                            }
                                            beforeCState = ecd.State;
                                            resultList.Add(ecd);

                                            #endregion
                                        }

                                        #endregion
                                    }

                                    if (resultList.Count <= 0)
                                    {
                                        throw new Exception("NoData:未解析到动态数据");
                                    }
                                    taskItem.JSONContent = JsonConvert.SerializeObject(resultList);
                                    taskItem.HandleStatus = HandleStatus.Complete;
                                }
                                catch (Exception ex)
                                {
                                    taskItem.HandleStatus = HandleStatus.Exception;
                                    taskItem.JSONDescription = ex.Message;
                                    if (taskItem.JSONDescription.RegexIsMatch("NoData:"))
                                        taskItem.HandleStatus = HandleStatus.Failure;

                                    LogService.Error(ModuleName, string.Format("[{0}]解析数据", taskItem.ID), ex);
                                }
                                finally
                                {
                                    taskItem.UpdateDate = DateTime.Now;
                                }
                            }
                            LogService.Info(ModuleName, string.Format("[{0}] 网页编码[{1}] 任务完成", BatchNo, websiteConfig.WebsiteCode));
                            #endregion

                        }, CancellationToken.None);
                        taskMultiple.Add(analysisTask);

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
                        LogService.Info(ModuleName, "解析数据 AggregateException", inner);
                        return true;
                    });
                }
                catch (Exception ex)
                {
                    LogService.Error(ModuleName, "解析数据 Exception:", ex);
                }

                BatchSaveContainerDynamicJSON(_AnalysisTaskList.Where(fItem => fItem.HandleStatus != HandleStatus.Untreated).ToDataTable());

                LogService.Info(ModuleName, string.Format("[{0}]解析完成", BatchNo));
            }
            catch (Exception ex)
            {
                LogService.Write(ex);
            }

            try
            {
                BatchSaveContainerDynamic();
            }
            catch (Exception ex)
            {
                LogService.Write(ex);
            }
        }
        /// <summary>
        /// 获取船东箱动态
        /// </summary>
        /// <param name="carrierID">鹏城海船东ID</param>
        /// <param name="blno">提单号</param>
        /// <param name="ctnrno">箱号</param>
        /// <returns></returns>
        public List<ContainerDynamic> GetCargoTracking(Guid carrierID, string blno, string ctnrno)
        {
            List<ContainerDynamic> cdList = GetCargoTrackingList(carrierID, blno, ctnrno);
            return cdList;
        }
        #endregion

        #region Private Method
        /// <summary>
        /// 初始化服务配置
        /// </summary>
        private void InitCargoTrackingServiceConfig()
        {
            try
            {
                ICommonService commonService = new CommonService();
                _WebsiteConfigs = commonService.GetWebsiteConfigs(CrawlType.CargoTracking).Where(fItem => fItem.CrawlType == CrawlType.CargoTracking).ToList();
            }
            catch (Exception ex)
            {
                LogService.Error(ModuleName, "初始化服务配置", ex);
            }
        }
        /// <summary>
        /// 通过状态描述获取箱状态
        /// </summary>
        /// <param name="stateDescription">箱状态</param>
        /// <param name="crawlConfig">箱状态配置</param>
        /// <returns></returns>
        private ContainerState GetContainerStateByDesc(string stateDescription, CrawlConfig crawlConfig)
        {
            if (stateDescription.RegexIsMatch(crawlConfig.EmptyPickUp))
                return ContainerState.EmptyPickUp;
            if (stateDescription.RegexIsMatch(crawlConfig.FullPickUp))
                return ContainerState.FullPickUp;
            if (stateDescription.RegexIsMatch(crawlConfig.LOBD))
                return ContainerState.LOBD;
            if (stateDescription.RegexIsMatch(crawlConfig.UNLOBD))
                return ContainerState.UNLOBD;
            if (stateDescription.RegexIsMatch(crawlConfig.REC))
                return ContainerState.REC;
            return ContainerState.UnKnown;
        }

        /// <summary>
        /// 抓取报告
        /// </summary>
        private void CrawlReport()
        {
            try
            {
                #region Log & Report

                var resultList = _ResultList.ToArray();
                if (!resultList.Any()) return;


                EReportInfo eReport = new EReportInfo(string.Format("CSP.CTR:货物动态跟踪报告-{0}", BatchNo));
                StringBuilder sbContent = new StringBuilder();
                string strLog = string.Format("箱总数:{0} 执行数量:{1} 完成数:{2} 失败数:{3}\t 异常数量:{4} 总耗时:{5}",
                    TotalCount,
                    resultList.Count(),
                    resultList.Count(fitem => fitem.HandleStatus == HandleStatus.Complete),
                    resultList.Count(fitem => fitem.HandleStatus == HandleStatus.Failure),
                    resultList.Count(fitem => fitem.HandleStatus == HandleStatus.Exception),
                    DateTime.Now.Subtract(JobDateTime).ToString(@"hh\:mm\:ss"));
                sbContent.AppendFormat("<h4>{0}</h4><br />", strLog);
                //var paramGroups = _ParamList.GroupBy(gItem => gItem.CarrierCode)K;
                var resultGroups = resultList.GroupBy(gItem => gItem.COCarrierCode);
                var groupItems = resultGroups as IGrouping<string, TaskCargoTracking>[] ?? resultGroups.ToArray();
                if (groupItems.Any())
                {
                    sbContent.AppendFormat(
                        "<table  border='1'><tr><th>船东</th><th>执行数量</th><th>完成数</th><th>失败数</th><th>异常数量</th></tr>");
                    foreach (var groupItem in groupItems)
                    {
                        var tempList = groupItem.ToArray();
                        sbContent.AppendFormat(
                            "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>"
                            , groupItem.Key
                            , tempList.Count()
                            , tempList.Count(fitem => fitem.HandleStatus == HandleStatus.Complete)
                            , tempList.Count(fitem => fitem.HandleStatus == HandleStatus.Failure)
                            , tempList.Count(fitem => fitem.HandleStatus == HandleStatus.Exception)
                            );
                    }
                    sbContent.Append("</table><br />");
                }

                var errorData = resultList.Where(fItem => fItem.HandleStatus == HandleStatus.Exception).ToArray();
                if (errorData.Any())
                {
                    sbContent.Append("<b>异常数据:</b><br />");
                    sbContent.AppendFormat(
                        "<table  border='1'><tr><th>网站编码</th><th>箱号</th><th>异常</th></tr>");
                    foreach (var errorItem in errorData)
                    {
                        sbContent.AppendFormat(
                            "<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>"
                            , errorItem.WebsiteCode
                            , errorItem.ContainerNO
                            , errorItem.HTMLDescription
                            );
                    }
                    sbContent.Append("</table><br />");
                }

                eReport.Context = sbContent.ToString();
                eReport.AttachmentPaths = new[] { "" };
                MService.SendEMail(eReport);
                LogService.Info(ModuleName, strLog);
                #endregion
            }
            catch (Exception ex)
            {
                LogService.Error(ModuleName, string.Format("[{0}]记录日志和发送报告", BatchNo), ex);
            }
        }

        /// <summary>
        /// 获取货物动态查询参数
        /// </summary>
        /// <returns></returns>
        private List<TaskCargoTracking> GetTask()
        {
            if (SingleID == Guid.Empty)
                throw new Exception("箱ID不能为空");
            Database db = GetDefaultDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("[csp].[uspGetTaskCargoTracking]");
            db.AddInParameter(dbCommand, "@ContainerID", DbType.Guid, SingleID);
            DataSet ds = db.TryExecuteDataSet(dbCommand);
            List<TaskCargoTracking> returnValue = (from b in ds.Tables[0].AsEnumerable()
                                                   select new TaskCargoTracking
                                                   {
                                                       TaskID = b.Field<Guid>("ContainerID"),
                                                       BLNO = b.Field<string>("BLNO"),
                                                       ContainerNO = b.Field<string>("ContainerNO"),
                                                       COCarrierID = b.Field<Guid>("COCarrierID"),
                                                       COCarrierCode = b.Field<string>("COCarrierCode"),
                                                       ETD = b.Field<DateTime>("ETD"),
                                                       WebsiteID = b.Field<Guid>("WebsiteID"),
                                                       WebsiteCode = b.Field<string>("WebsiteCode"),
                                                       UnConfirmedCarrier = b.Field<bool>("UnConfirmedCarrier"),
                                                       HandleStatus = HandleStatus.Untreated,
                                                   }).ToList();
            return returnValue;
        }
        /// <summary>
        /// 获取货物动态查询参数
        /// </summary>
        /// <returns></returns>
        private List<TaskCargoTracking> GetTasks()
        {
            Database db = GetDefaultDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("[csp].[uspGetTaskCargoTrackingList]");
            db.AddInParameter(dbCommand, "@ESTBeforeDays", DbType.Int32, ESTBeforeDays);
            db.AddInParameter(dbCommand, "@ETDRange", DbType.Int32, ETDRange);
            DataSet ds = db.TryExecuteDataSet(dbCommand);
            //ConcurrentQueue<TaskCargoTracking> taskList = new ConcurrentQueue<TaskCargoTracking>();
            //foreach (TaskCargoTracking taskItem in Enumerable.Select(ds.Tables[0].AsEnumerable(), b => new TaskCargoTracking
            //{
            //    TaskID = b.Field<Guid>("ContainerID"),
            //    BLNO = b.Field<string>("BLNO"),
            //    ContainerNO = b.Field<string>("ContainerNO"),
            //    COCarrierID = b.Field<Guid>("COCarrierID"),
            //    COCarrierCode = b.Field<string>("COCarrierCode"),
            //    ETD = b.Field<DateTime>("ETD"),
            //    WebsiteID = b.Field<Guid>("WebsiteID"),
            //    WebsiteCode = b.Field<string>("WebsiteCode"),
            //    UnConfirmedCarrier = b.Field<bool>("UnConfirmedCarrier"),
            //    HandleStatus = HandleStatus.Untreated,
            //}))
            //{
            //    taskList.Enqueue(taskItem);
            //}


            List<TaskCargoTracking> returnValue = (from b in ds.Tables[0].AsEnumerable()
                                                   select new TaskCargoTracking
                                                   {
                                                       TaskID = b.Field<Guid>("ContainerID"),
                                                       BLNO = b.Field<string>("BLNO"),
                                                       ContainerNO = b.Field<string>("ContainerNO"),
                                                       COCarrierID = b.Field<Guid>("COCarrierID"),
                                                       COCarrierCode = b.Field<string>("COCarrierCode"),
                                                       ETD = b.Field<DateTime>("ETD"),
                                                       WebsiteID = b.Field<Guid>("WebsiteID"),
                                                       WebsiteCode = b.Field<string>("WebsiteCode"),
                                                       UnConfirmedCarrier = b.Field<bool>("UnConfirmedCarrier"),
                                                       HandleStatus = HandleStatus.Untreated,
                                                   }).ToList();
            return returnValue;
        }
        /// <summary>
        /// 单个箱动态抓取
        /// </summary>
        /// <param name="paramTaskItem"></param>
        /// <param name="paramIndex"></param>
        /// <param name="crawlConfig"></param>
        /// <param name="paramTokenSource"></param>
        void CrawlerDataByConfig(TaskCargoTracking paramTaskItem, int paramIndex
            , CrawlConfig crawlConfig, CancellationTokenSource paramTokenSource)
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

                switch (crawlConfig.ID.ToUpperString())
                {
                    case "8A84D219-8692-450A-B120-933B4D14D4A5": //EMC
                        wCrawler.StartAction = EMCCrawlStartAction;
                        break;
                    case "2E0A6D8A-15C4-437D-A8A2-DBBCF5F7CAD1": //ANSHENG
                        wCrawler.StartAction = ANSHENGCrawlStartAction;
                        break;
                    default:
                        wCrawler.StartAction = CrawlStartAction;
                        break;
                }
                wCrawler.ErrorAction = CrawlErrorAction;
                switch (crawlConfig.ID.ToUpperString())
                {
                    case "8A84D219-8692-450A-B120-933B4D14D4A5": //EMC
                        wCrawler.CompletedAction = EMCCrawlCompletedAction;
                        break;
                    case "7461697D-1F07-4FDF-9268-DB6C8E4E91A5":
                        wCrawler.CompletedAction = MSKCrawlCompletedAction;
                        break;
                    default:
                        wCrawler.CompletedAction = CrawlCompletedAction;
                        break;
                }
                wCrawler.StopAction = StopAction;
                wCrawler.StartCrawl(paramTaskItem, crawlConfig, crawlConfig.Timeout, TS_SingleTask);

                LogService.Debug(ModuleName, string.Format("总[{0}]项,网站[{1}]项,船东[{2}]箱[{3}]状态[{4}]耗时[{5}]秒,PID[{6}]网站[{7}]", curIndex, paramIndex,
                    paramTaskItem.COCarrierCode, paramTaskItem.ContainerNO, paramTaskItem.HandleStatus,
                    DateTime.Now.Subtract(watch).ToStringSeconds(), wCrawler.ProcessId, crawlConfig.WebsiteCode));
            }
            catch (Exception ex)
            {
                LogService.Fatal(ModuleName, "CrawlerDataByConfig", ex);
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
        /// EMC抓取数据
        /// </summary>
        /// <param name="paramStart"></param>
        private void EMCCrawlStartAction(ParamStart paramStart)
        {
            TaskCargoTracking taskObject = (TaskCargoTracking)paramStart.TaskObject;
            CrawlConfig taskConfig = (CrawlConfig)paramStart.TaskConfig;
            //Navigate To Url
            CrawlConfigParam setup00 = taskConfig.GetWebsiteParamByID("27e709da-b6cd-4762-8346-629b27a8c75d".NewGuid());
            CrawlByWebConfigParam(paramStart.WebDriver, taskObject, taskConfig, setup00);
            if (taskObject.BLNO.RegexIsMatch("EGLV"))
            {
                //Input
                CrawlConfigParam setup01 = taskConfig.GetWebsiteParamByID("3b648a4e-2a1d-46e8-a93b-0602725774aa".NewGuid());
                setup01.ParamValue = taskObject.BLNO.RegexExtractNumber();
                CrawlByWebConfigParam(paramStart.WebDriver, taskObject, taskConfig, setup01);
                //Button
                CrawlConfigParam setup02 = taskConfig.GetWebsiteParamByID("16386371-13f4-4043-91a4-fa5eaf5e8a95".NewGuid());
                CrawlByWebConfigParam(paramStart.WebDriver, taskObject, taskConfig, setup02);

                //NoData
                CrawlConfigParam setup03 = taskConfig.GetWebsiteParamByID("4b1bc950-9eed-491a-9aab-e131b1814042".NewGuid());
                string noResults = paramStart.WebDriver.GetPageSource().RegexMatchString(setup03.KeyValue, "NoResults").GetText();
                if (!noResults.IsNullOrEmpty())
                {
                    throw new WebDriverException("NoData:" + noResults);
                }

                //A 数据更改后变回
                CrawlConfigParam setup04 = taskConfig.GetWebsiteParamByID("0c742e69-efde-480b-85ec-2a3fcd471460".NewGuid());
                string keyValue = setup04.KeyValue;
                setup04.KeyValue = string.Format(setup04.KeyValue, taskObject.ContainerNO);
                try
                {
                    CrawlByWebConfigParam(paramStart.WebDriver, taskObject, taskConfig, setup04);
                }
                finally
                {
                    setup04.KeyValue = keyValue;
                }
                //Window
                CrawlConfigParam setup05 = taskConfig.GetWebsiteParamByID("c528df26-eef0-4cf1-ab9d-26ea6bc3b7e5".NewGuid());
                CrawlByWebConfigParam(paramStart.WebDriver, taskObject, taskConfig, setup05);
            }
            else
            {
                //Radio
                CrawlConfigParam setup01 = taskConfig.GetWebsiteParamByID("92f1e825-23a1-43cd-a38d-78278997b3df".NewGuid());
                CrawlByWebConfigParam(paramStart.WebDriver, taskObject, taskConfig, setup01);
                //Input
                CrawlConfigParam setup02 = taskConfig.GetWebsiteParamByID("3b648a4e-2a1d-46e8-a93b-0602725774aa".NewGuid());
                setup02.ParamValue = taskObject.ContainerNO;
                CrawlByWebConfigParam(paramStart.WebDriver, taskObject, taskConfig, setup02);
                //Button
                CrawlConfigParam setup03 = taskConfig.GetWebsiteParamByID("16386371-13f4-4043-91a4-fa5eaf5e8a95".NewGuid());
                CrawlByWebConfigParam(paramStart.WebDriver, taskObject, taskConfig, setup03);

                //NoData
                CrawlConfigParam setup04 = taskConfig.GetWebsiteParamByID("1d4fa6ed-7c25-4607-a700-4dac6a3bcfa1".NewGuid());
                string noResults = paramStart.WebDriver.GetPageSource().RegexMatchString(setup04.KeyValue, "NoResults").GetText();
                if (!noResults.IsNullOrEmpty())
                {
                    throw new WebDriverException("NoData:" + noResults);
                }
            }
        }
        /// <summary>
        /// EMC抓取数据
        /// </summary>
        /// <param name="paramStart"></param>
        private void ANSHENGCrawlStartAction(ParamStart paramStart)
        {
            TaskCargoTracking taskObject = (TaskCargoTracking)paramStart.TaskObject;
            CrawlConfig taskConfig = (CrawlConfig)paramStart.TaskConfig;
            //Navigate To Url
            CrawlConfigParam setup00 = taskConfig.GetWebsiteParamByID("022B8D56-F733-44D2-9694-ABDDC600AA09".NewGuid());
            CrawlByWebConfigParam(paramStart.WebDriver, taskObject, taskConfig, setup00);
            //Input BL NO
            CrawlConfigParam setup01 = taskConfig.GetWebsiteParamByID("0AE820ED-2A4A-47F6-B4E3-011BB5718D03".NewGuid());
            CrawlByWebConfigParam(paramStart.WebDriver, taskObject, taskConfig, setup01);
            //Input Container NO
            CrawlConfigParam setup02 = taskConfig.GetWebsiteParamByID("530B6ABF-47C5-4661-B40A-C8EB22299486".NewGuid());
            CrawlByWebConfigParam(paramStart.WebDriver, taskObject, taskConfig, setup02);
            //Button Search
            CrawlConfigParam setup03 = taskConfig.GetWebsiteParamByID("4CBA8CE6-1613-4A3B-AC17-1F1535259606".NewGuid());
            CrawlByWebConfigParam(paramStart.WebDriver, taskObject, taskConfig, setup03);
            //Wait HTML Content
            CrawlConfigParam setup04 = taskConfig.GetWebsiteParamByID("9F9247B7-518F-41CA-AD45-27068B8E5086".NewGuid());
            CrawlByWebConfigParam(paramStart.WebDriver, taskObject, taskConfig, setup04);
            //Exception No Data
            CrawlConfigParam setup05 = taskConfig.GetWebsiteParamByID("22B94961-3023-4C15-A4CA-5538362B94F2".NewGuid());
            CrawlByWebConfigParam(paramStart.WebDriver, taskObject, taskConfig, setup05);
            //Wait Container NO
            CrawlConfigParam setup06 = taskConfig.GetWebsiteParamByID("1642BF5B-19D0-425D-81A3-D1AB88EC6218".NewGuid());
            string keyValue6 = setup06.KeyValue;
            setup06.KeyValue = string.Format(setup06.KeyValue, taskObject.ContainerNO);
            try
            {
                CrawlByWebConfigParam(paramStart.WebDriver, taskObject, taskConfig, setup06);
            }
            finally
            {
                setup06.KeyValue = keyValue6;
            }
            //A 数据更改后变回
            CrawlConfigParam setup07 = taskConfig.GetWebsiteParamByID("E77855D4-7A91-48D5-B23A-B1CED3A0AE75".NewGuid());
            string keyValue7 = setup07.KeyValue;
            setup07.KeyValue = string.Format(setup07.KeyValue, taskObject.ContainerNO);
            try
            {
                CrawlByWebConfigParam(paramStart.WebDriver, taskObject, taskConfig, setup07);
            }
            finally
            {
                setup07.KeyValue = keyValue7;
            }
        }
        /// <summary>
        /// 错误处理动作
        /// </summary>
        /// <param name="paramError"></param>
        private void CrawlErrorAction(ParamError paramError)
        {
            try
            {
                TaskCargoTracking taskObject = (TaskCargoTracking)paramError.TaskObject;
                if (taskObject.IsNull()) return;
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
        /// 完成抓取后处理动作
        /// </summary>
        /// <param name="paramCompleted"></param>
        private void CrawlCompletedAction(ParamCompleted paramCompleted)
        {
            TaskCargoTracking taskObject = (TaskCargoTracking)paramCompleted.TaskObject;
            CrawlConfig taskConfig = (CrawlConfig)paramCompleted.TaskConfig;
            string pageSource = paramCompleted.PageSource;
            if (pageSource.IsNullOrEmpty())
                throw new WebDriverException("BlankPage:CompletedAction");
            string noResults = string.Empty;
            if (taskConfig.ContainsKey("EX_NoResults"))
            {
                noResults = pageSource.RegexMatchString(taskConfig.GetParamValueByKey("EX_NoResults"), "NoResults").GetText();
                if (!noResults.IsNullOrEmpty())
                {
                    throw new WebDriverException("NoData:" + noResults);
                }
            }
            #region HTMLText


            taskObject.HTMLContent = "";
            int maxPageSize = 3;
            int curPageSize = 1;
            IWebElement nextPageElement = null;
            do
            {
                bool PageIsCompleted = !taskConfig.ContainsKey("EX_PageCompleted");
                if (!PageIsCompleted)
                {
                    CrawlConfigParam pageCompleted = taskConfig.GetWebsiteParamByKey("EX_PageCompleted");
                    PageIsCompleted = paramCompleted.WebDriver.WaitElementsLoad(pageCompleted.ParamValue, pageCompleted.Timeout);
                }
                if (PageIsCompleted)
                {
                    pageSource = paramCompleted.WebDriver.GetPageSource();
                    var resultDiv = pageSource;
                    if (taskConfig.ContainsKey("EX_Results"))
                    {
                        resultDiv = pageSource.RegexMatchHtmlTag(taskConfig.GetParamValueByKey("EX_Results"));
                        if (resultDiv.GetText().IsNullOrEmpty())
                            return;
                    }
                    taskObject.HTMLContent += resultDiv;

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
                    curPageSize++;
                }
            } while (nextPageElement != null && curPageSize < maxPageSize);

            #endregion
            taskObject.HandleStatus = HandleStatus.Complete;
        }
        /// <summary>
        /// 完成抓取后处理动作
        /// </summary>
        /// <param name="paramCompleted"></param>
        private void EMCCrawlCompletedAction(ParamCompleted paramCompleted)
        {
            TaskCargoTracking taskObject = paramCompleted.TaskObject as TaskCargoTracking;
            if (taskObject == null)
            {
                throw new WebDriverException("EMCCrawlCompletedAction TaskCargoTracking Is Null");
            }
            CrawlConfig taskConfig = paramCompleted.TaskConfig as CrawlConfig;
            if (taskConfig == null)
            {
                throw new WebDriverException("EMCCrawlCompletedAction ServiceConfig Is Null");
            }
            if (!taskObject.BLNO.Contains("EGLV"))
            {
                CrawlCompletedAction(paramCompleted);
                return;
            }
            string pageSource = paramCompleted.PageSource.FormatHTML();
            if (pageSource.IsNullOrEmpty())
                throw new WebDriverException("BlankPage:CompletedAction");
            string noResults = string.Empty;
            if (taskConfig.ContainsKey("EX_NoResults"))
            {
                noResults = paramCompleted.PageSource.RegexMatchString(taskConfig.GetParamValueByKey("EX_NoResults", "NoResults")).GetText();
                if (!noResults.IsNullOrEmpty())
                {
                    throw new WebDriverException("NoData:" + noResults);
                }
            }
            #region HTMLText
            var resultDiv = pageSource;
            if (taskConfig.ContainsKey("EX_Results"))
            {
                resultDiv = pageSource.RegexMatchHtmlTag(taskConfig.GetParamValueByKey("EX_Results"));
                if (resultDiv.GetText().IsNullOrEmpty())
                    return;
            }
            taskObject.HTMLContent = resultDiv;
            #endregion
            taskObject.HandleStatus = HandleStatus.Complete;
        }
        /// <summary>
        /// 完成抓取后处理动作
        /// </summary>
        /// <param name="paramCompleted"></param>
        private void MSKCrawlCompletedAction(ParamCompleted paramCompleted)
        {
            TaskCargoTracking taskObject = paramCompleted.TaskObject as TaskCargoTracking;
            if (taskObject == null)
            {
                throw new WebDriverException("CrawlCompletedAction TaskCargoTracking Is Null");
            }
            CrawlConfig taskConfig = paramCompleted.TaskConfig as CrawlConfig;
            if (taskConfig == null)
            {
                throw new WebDriverException("CrawlCompletedAction ServiceConfig Is Null");
            }
            string pageSource = paramCompleted.PageSource;
            if (pageSource.IsNullOrEmpty())
                throw new WebDriverException("BlankPage:CompletedAction");
            string noResults = string.Empty;
            if (taskConfig.ContainsKey("EX_NoResults"))
            {
                noResults = paramCompleted.PageSource.RegexMatchString(taskConfig.GetParamValueByKey("EX_NoResults"), "NoResults").GetText();
                if (!noResults.IsNullOrEmpty())
                {
                    throw new WebDriverException("NoData:" + noResults);
                }
            }
            #region HTMLText
            var resultDiv = pageSource;
            if (taskConfig.ContainsKey("EX_Results"))
            {
                resultDiv = pageSource.RegexMatchHtmlTag(taskConfig.GetParamValueByKey("EX_Results"));
                if (resultDiv.GetText().IsNullOrEmpty())
                    return;
            }
            taskObject.HTMLContent = resultDiv;
            #endregion
            taskObject.HandleStatus = HandleStatus.Complete;
        }
        /// <summary>
        /// 抓取数据完成
        /// </summary>
        /// <param name="paramStop"></param>
        private void StopAction(ParamStop paramStop)
        {
            TaskCargoTracking taskObject = paramStop.TaskObject as TaskCargoTracking;
            if (taskObject == null)
            {
                throw new Exception("StopAction TaskCargoTracking Is Null");
            }
            string cacheFileName = "" + taskObject.COCarrierCode + taskObject.ContainerNO;
            taskObject.UpdateDate = DateTime.Now;
            if (taskObject.HTMLContent.IsNullOrEmpty() && taskObject.HTMLDescription.IsNullOrEmpty())
                LogService.Warn(ModuleName, string.Format("ID[{0}] NO[{1}] WebsiteID [{2}]", taskObject.TaskID, taskObject.ContainerNO, taskObject.WebsiteID), new Exception("无HTML及其异常"));
            else
                HTMLCacheService.WriteCache(JSONDirectory, cacheFileName, JsonConvert.SerializeObject(taskObject), true);
        }
        /// <summary>
        /// 批量保存箱动态
        /// </summary>
        private void BatchSaveContainerDynamicHTML()
        {
            try
            {
                LogService.Info(ModuleName, "开始读取结果集合");
                var watch = DateTime.Now;
                DataTable dtConfig = new List<TaskCargoTracking>().ToDataTable();
                if (JSONDirectory.IsExistsDirectory())
                {
                    List<TaskCargoTracking> configs = BuildCrawlResults<TaskCargoTracking>(JSONDirectory, false);
                    _ResultList = configs;
                    dtConfig = configs.Where(fItem => fItem.HandleStatus > HandleStatus.Untreated).ToDataTable();
                }

                if (dtConfig == null || dtConfig.Rows.Count <= 0)
                {
                    if (IsExceptionBatch) ExceptionBatchNo = "";
                    return;
                }
                LogService.Info(ModuleName, string.Format("读取到[{0}]项动态结果,耗时[{1}]秒", _ResultList.Count(), DateTime.Now.Subtract(watch).ToStringSeconds()));
                Database db = GetDefaultDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("[csp].[uspBulkSaveContainerDynamicHTML]");
                dbCommand.CommandTimeout = 0;
                SqlParameter parameterConfig = new SqlParameter("@CrawlTask", dtConfig)
                {
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "[csp].[ContainerDynamicTask_Type]"
                };

                dbCommand.Parameters.Add(parameterConfig);
                db.TryExecuteNonQuery(dbCommand);
                ExceptionBatchNo = "";
            }
            catch (Exception ex)
            {
                ExceptionBatchNo = BatchNo;
                LogService.Error(ModuleName, "批量保存动态HTML内容", ex);
            }
        }

        /// <summary>
        /// 获取解析任务
        /// </summary>
        /// <returns></returns>
        private List<TaskAnalysisCargoTracking> GetAnalysisTask()
        {
            if (SingleID == Guid.Empty)
                throw new Exception("箱ID不能为空");
            Database db = GetDefaultDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("[csp].[uspGetTaskAnalysisCargoTracking]");
            db.AddInParameter(dbCommand, "@LogID", DbType.Guid, SingleID);
            DataSet ds = db.TryExecuteDataSet(dbCommand);
            List<TaskAnalysisCargoTracking> returnValue = (from b in ds.Tables[0].AsEnumerable()
                                                           select new TaskAnalysisCargoTracking
                                                           {
                                                               ID = b.Field<Guid>("ID"),
                                                               ContainerID = b.Field<Guid>("ContainerID"),
                                                               ContainerNO = b.Field<string>("ContainerNO"),
                                                               MBLNO = b.Field<string>("MBLNO"),
                                                               HTMLContent = b.Field<string>("HTMLContent"),
                                                               WebsiteID = b.Field<Guid>("WebsiteID"),
                                                               WebsiteCode = b.Field<string>("WebsiteCode"),
                                                               HandleStatus = HandleStatus.Untreated,
                                                           }).ToList();
            return returnValue;
        }
        /// <summary>
        /// 获取解析任务
        /// </summary>
        /// <returns></returns>
        private List<TaskAnalysisCargoTracking> GetAnalysisTasks()
        {
            Database db = GetDefaultDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("[csp].[uspGetTaskAnalysisCargoTrackingList]");
            DataSet ds = db.TryExecuteDataSet(dbCommand);
            List<TaskAnalysisCargoTracking> returnValue = (from b in ds.Tables[0].AsEnumerable()
                                                           select new TaskAnalysisCargoTracking
                                                           {
                                                               ID = b.Field<Guid>("ID"),
                                                               ContainerID = b.Field<Guid>("ContainerID"),
                                                               ContainerNO = b.Field<string>("ContainerNO"),
                                                               MBLNO = b.Field<string>("MBLNO"),
                                                               HTMLContent = b.Field<string>("HTMLContent"),
                                                               WebsiteID = b.Field<Guid>("WebsiteID"),
                                                               WebsiteCode = b.Field<string>("WebsiteCode"),
                                                               HandleStatus = HandleStatus.Untreated,
                                                           }).ToList();
            return returnValue;
        }
        /// <summary>
        /// 批量保存箱动态JSON
        /// </summary>
        /// <param name="paramTasks">抓取任务</param>
        private void BatchSaveContainerDynamicJSON(DataTable paramTasks)
        {
            try
            {
                if (paramTasks == null || paramTasks.Rows.Count <= 0)
                {
                    return;
                }
                Database db = GetDefaultDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("[csp].[uspBulkSaveContainerDynamicJSON]");
                dbCommand.CommandTimeout = 0;
                SqlParameter tasks = new SqlParameter("@Tasks", paramTasks)
                {
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "[csp].[ContainerDynamicAnalysisTask_Type]"
                };
                dbCommand.Parameters.Add(tasks);
                db.TryExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                LogService.Error(ModuleName, "批量保存动态JSON", ex);
            }
        }

        /// <summary>
        /// 批量保存箱动态
        /// </summary>
        private void BatchSaveContainerDynamic()
        {
            try
            {
                Database db = GetDefaultDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("[csp].[uspBulkSaveContainerDynamic]");
                dbCommand.CommandTimeout = 0;
                db.TryExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                LogService.Error(ModuleName, "批量保存动态", ex);
            }
        }

        /// <summary>
        /// 获取船东箱动态
        /// </summary>
        /// <param name="paramCOCarrierID">鹏城海船东ID</param>
        /// <param name="paramBLNo">提单号</param>
        /// <param name="paramCTNRNo">箱号</param>
        /// <returns>箱动态(List列表)</returns>
        private List<ContainerDynamic> GetCargoTrackingList(Guid paramCOCarrierID, string paramBLNo, string paramCTNRNo)
        {
            List<ContainerDynamic> results = null;
            Database db = GetDefaultDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("[csp].[uspGetCargoTrackingList]");
            //前3次执行任务作为初始化查询参数使用,后续只查询有航线的港口关系数据
            db.AddInParameter(dbCommand, "@COCarrierID", DbType.Guid, paramCOCarrierID);
            db.AddInParameter(dbCommand, "@BLNo", DbType.String, paramBLNo);
            db.AddInParameter(dbCommand, "@CTNRNo", DbType.String, paramCTNRNo);
            DataSet ds = db.TryExecuteDataSet(dbCommand);
            if (ds == null || ds.Tables.Count < 1)
            {
                return new List<ContainerDynamic>();
            }
            results = (from b in ds.Tables[0].AsEnumerable()
                       select new ContainerDynamic
                       {
                           ParentID = b.Field<Guid>("ID"),
                           EventTime = b.Field<DateTime>("EventTime"),
                           Station = b.Field<string>("Station"),
                           StateDescription = b.Field<string>("Statedescription"),
                           VesselName = b.Field<string>("VesselName"),
                           VoyageNumber = b.Field<string>("VoyageNumber"),
                           IsEST = b.Field<bool>("IsEST"),
                       }).ToList();
            return results;
        }
        #endregion
    }
}
