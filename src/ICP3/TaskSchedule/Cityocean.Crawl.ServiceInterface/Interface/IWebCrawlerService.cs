#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/4/13 11:13:29
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

using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;

namespace Cityocean.Crawl.ServiceInterface
{
    /// <summary>
    /// 数据抓取公开服务
    /// </summary>
    [ServiceContract]
    public interface IWebCrawlerService
    {
        /// <summary>
        /// 查询船东信息
        /// </summary>
        /// <param name="paramCarrierInfo">船东信息 (船东编码、名称、描述)</param>
        /// <param name="paramCOwner">船东所属</param>
        /// <returns>船东数据(JSON)</returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "GetCarriers?Desc={paramCarrierInfo}&Owner={paramCOwner}")]
        Message SearchCarriers(string paramCarrierInfo, string paramCOwner);

        /// <summary>
        /// 获取船东箱动态
        /// </summary>
        /// <param name="paramCOCarrierID">鹏城海船东ID</param>
        /// <param name="paramBLNo">提单号</param>
        /// <param name="paramCTNRNo">箱号</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetContainerDynamic?COCarrierID={paramCOCarrierID}&BLNo={paramBLNo}&CTNRNo={paramCTNRNo}")]
        Message SearchContainerDynamic(string paramCOCarrierID, string paramBLNo, string paramCTNRNo);


        /// <summary>
        /// 抓取码头数据
        /// </summary>
        /// <param name="paramSearchType">查询类型:All、Single</param>
        /// <param name="paramTaskID">任务ID</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "Terminal?SearchType={paramSearchType}&ContainerID={paramTaskID}")]
        string CrawlTerminalData(string paramSearchType, string paramTaskID);

        /// <summary>
        /// 抓取货物跟踪
        /// </summary>
        /// <param name="paramSearchType">查询类型:All、Single</param>
        /// <param name="paramTaskID">任务ID</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "ContainerDynamic?SearchType={paramSearchType}&ContainerID={paramTaskID}")]
        string CrawlCargoTrackingData(string paramSearchType, string paramTaskID);


        /// <summary>
        /// 解析货物跟踪
        /// </summary>
        /// <param name="paramTaskID">任务ID</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "AnalysisData?LogID={paramTaskID}")]
        string AnalysisCargoTrackingData(string paramTaskID);

        /// <summary>
        /// 抓取码头船期
        /// </summary>
        /// <param name="paramSearchType">查询类型:All、Single</param>
        /// <param name="paramTaskID">任务ID</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "TerminalVesselSchedule?SearchType={paramSearchType}&ContainerID={paramTaskID}")]
        string CrawlTerminalVesselScheduleData(string paramSearchType, string paramTaskID);
    }
}
