#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/4/13 11:23:01
 *
 * Description:
 *         ->
 *         1.配置
 *         2.获取数据
 *
 * History:
 *         ->
 */
#endregion

using System.ServiceModel.Activation;
using Cityocean.Crawl.CommonLibrary;
using Cityocean.Crawl.ServiceInterface;
using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;

namespace Cityocean.Crawl.ServerComponents
{
    /// <summary>
    /// 数据抓取公开服务
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [JavascriptCallbackBehavior(UrlParameterName = "jsoncallback")]
    public sealed class WebCrawlerService : IWebCrawlerService
    {
        #region Service
        #region ICommonService
        ICommonService _ICommonService = null;
        /// <summary>
        /// 公用服务
        /// </summary>
        ICommonService CService { get { return _ICommonService ?? (_ICommonService = new CommonService()); } }
        #endregion

        #region ITerminalContainerStatusService
        ITerminalContainerStatusService _ITerminalService = null;
        /// <summary>
        /// 码头服务
        /// </summary>
        ITerminalContainerStatusService TSService { get { return _ITerminalService ?? (_ITerminalService = new TerminalContainerStatusService()); } }
        #endregion

        #region ITerminalVesselScheduleService
        ITerminalVesselScheduleService _ISailingScheduleService = null;
        /// <summary>
        /// 船期服务
        /// </summary>
        ITerminalVesselScheduleService SSService { get { return _ISailingScheduleService ?? (_ISailingScheduleService = new TerminalVesselScheduleService()); } }
        #endregion

        #region ICargoTrackingService
        ICargoTrackingService _ICargoTrackingService = null;
        /// <summary>
        /// 货物动态服务
        /// </summary>
        ICargoTrackingService CTService { get { return _ICargoTrackingService ?? (_ICargoTrackingService = new CargoTrackingService()); } }
        #endregion 
        #endregion

        #region Public Method

        /// <summary>
        /// 查询船东信息
        /// </summary>
        /// <param name="paramCarrierInfo">船东信息</param>
        /// <param name="paramCOwner">船东所属</param>
        /// <returns>船东数据(JSON)</returns>
        public Message SearchCarriers(string paramCarrierInfo, string paramCOwner)
        {
            WebOperationContext context = WebOperationContext.Current;
            try
            {
                paramCarrierInfo = paramCarrierInfo.FilterSql();
                CarrierOwner owner = paramCOwner.IsNullOrEmpty() ? CarrierOwner.INTTRA : (CarrierOwner)Enum.Parse(typeof(CarrierOwner), paramCOwner);
                List<ECarrierInfo> resultList = CService.GetCarrierInfos(paramCarrierInfo, owner);
                if (context != null)
                {
                    Message msgreturn = context.CreateJsonResponse(resultList);
                    return msgreturn;
                }
            }
            catch(Exception ex)
            {
                if (context != null)
                    context.CreateTextResponse("获取船东时发生异常" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// 获取船东箱动态
        /// </summary>
        /// <param name="paramCOCarrierID">鹏城海船东ID</param>
        /// <param name="paramBLNo">提单号</param>
        /// <param name="paramCTNRNo">箱号</param>
        /// <returns></returns>
        public Message SearchContainerDynamic(string paramCOCarrierID, string paramBLNo, string paramCTNRNo)
        {
            WebOperationContext context = WebOperationContext.Current;
            try
            {
                paramCOCarrierID = paramCOCarrierID.FilterSql();
                paramBLNo = paramBLNo.FilterSql();
                paramCTNRNo = paramCTNRNo.FilterSql();

                if (paramCOCarrierID.IsNullOrEmpty() || paramCTNRNo.IsNullOrEmpty())
                {
                    throw new Exception("[Ocean Carrier ID]或[CTNR No]不能为空");
                }
                Guid coCarrierID = paramCOCarrierID.NewGuid();
                if (coCarrierID.IsNullOrEmpty())
                {
                    throw new Exception("[Ocean Carrier ID]不是有效的 Guid");
                }
                List<ContainerDynamic> resultList = CTService.GetCargoTracking(new Guid(paramCOCarrierID), paramBLNo, paramCTNRNo);
                if (context != null)
                {
                    Message msgreturn = context.CreateJsonResponse(resultList);
                    return msgreturn;
                }
            }
            catch (Exception ex)
            {
                if (context != null)
                    context.CreateTextResponse("获取动态时发生异常" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// 抓取码头数据
        /// </summary>
        /// <param name="paramSearchType">查询类型</param>
        /// <param name="paramTaskID">任务ID</param>
        /// <returns></returns>
        public string CrawlTerminalData(string paramSearchType, string paramTaskID)
        {
            try
            {
                if (paramSearchType.IsNullOrEmpty())
                    return "查询类型不能为空";
                if ("Single".Equals(paramSearchType))
                {
                    if (paramTaskID.IsNullOrEmpty())
                        return "箱ID不能为空";
                    TSService.TaskID = paramTaskID.NewGuid();
                }
                if (!JobMemoryCache.AddJob("WebTerminal", "测试抓取码头箱状态数据")) return "抓取码头箱状态数据任务正在运行";
                TSService.CrawlerData();
                JobMemoryCache.RemoveJob("WebTerminal");
                return paramTaskID;
            }
            catch (Exception ex)
            {
                JobMemoryCache.RemoveJob("WebTerminal");
                return "抓取数据时出现异常:" + ex.Message;
            }
        }

        /// <summary>
        /// 抓取货物跟踪
        /// </summary>
        /// <param name="paramSearchType">查询类型</param>
        /// <param name="paramTaskID">任务ID</param>
        /// <returns></returns>
        public string CrawlCargoTrackingData(string paramSearchType, string paramTaskID)
        {
            try
            {
                if (paramSearchType.IsNullOrEmpty())
                    return "查询类型不能为空";
                if ("Single".Equals(paramSearchType))
                {
                    if (paramTaskID.IsNullOrEmpty())
                        return "箱ID不能为空";
                    CTService.SingleID = paramTaskID.NewGuid();
                }
                if (!JobMemoryCache.AddJob("WebCargoTracking", "测试抓取货物动态数据")) return "抓取货物动态数据任务正在运行";
                CTService.CrawlerData();
                JobMemoryCache.RemoveJob("WebCargoTracking");
                return paramTaskID;
            }
            catch (Exception ex)
            {
                JobMemoryCache.RemoveJob("WebCargoTracking");
                return "抓取数据时出现异常:" + ex.Message;
            }
        }

        /// <summary>
        /// 解析货物跟踪
        /// </summary>
        /// <param name="paramTaskID">任务ID</param>
        /// <returns></returns>
        public string AnalysisCargoTrackingData(string paramTaskID)
        {
            try
            {
                if (paramTaskID.IsNullOrEmpty())
                    return "日志ID不能为空";
                CTService.SingleID = paramTaskID.NewGuid();
                if (!JobMemoryCache.AddJob("AnalysisCargoTracking", "测试解析货物动态数据")) return "解析货物动态数据任务正在运行";
                CTService.AnalysisData();
                JobMemoryCache.RemoveJob("AnalysisCargoTracking");
                return paramTaskID;
            }
            catch (Exception ex)
            {
                JobMemoryCache.RemoveJob("AnalysisCargoTracking");
                return "解析数据时出现异常:" + ex.Message;
            }
        }

        /// <summary>
        /// 抓取船期
        /// </summary>
        /// <param name="paramSearchType">查询类型</param>
        /// <param name="paramTaskID">任务ID</param>
        /// <returns></returns>
        public string CrawlTerminalVesselScheduleData(string paramSearchType, string paramTaskID)
        {
            try
            {
                if (paramSearchType.IsNullOrEmpty())
                {
                    return "查询类型不能为空";
                }
                if ("Single".Equals(paramSearchType))
                {
                    if (paramTaskID.IsNullOrEmpty())
                        return "任务ID不能为空";
                    //SSService.TaskID = paramTaskID.NewGuid();
                }
                if (!JobMemoryCache.AddJob("WebTerminal", "测试抓取码头箱状态数据")) return "抓取码头箱状态数据任务正在运行";
                SSService.CrawlerData();
                JobMemoryCache.RemoveJob("WebTerminal");
                return paramTaskID;
            }
            catch (Exception ex)
            {
                JobMemoryCache.RemoveJob("WebTerminal");
                return "抓取数据时出现异常:" + ex.Message;
            }
        }
        #endregion
    }
}
