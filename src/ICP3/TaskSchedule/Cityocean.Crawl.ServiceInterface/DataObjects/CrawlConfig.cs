#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/6/20 16:47:56
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using Cityocean.Crawl.CommonLibrary;

namespace Cityocean.Crawl.ServiceInterface
{
    /// <summary>
    /// 抓取配置
    /// </summary>
    [Serializable]
    public sealed class CrawlConfig
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 网站编码
        /// </summary>
        public string WebsiteCode { get; set; }
        /// <summary>
        /// 页面返回内容类型
        /// </summary>
        public PageReturnType ReturnType { get; set; }
        /// <summary>
        /// 抓取(数据)类型
        /// </summary>
        public CrawlType CrawlType { get; set; }
        /// <summary>
        /// 排序类型{0:降序; 1:升序;}
        /// </summary>
        public DynamicSortType SortType { get; set; }
        /// <summary>
        /// 是否历史(所有历史动态)数据{0:否;1:是}
        /// </summary>
        public bool IsHistory { get; set; }
        /// <summary>
        /// 是否需要登录
        /// </summary>
        public bool IsNeedLogin { get; set; }

        /// <summary>
        /// 超时秒数(s)
        /// </summary>
        public int Timeout { get; set; }
        /// <summary>
        /// 爬虫浏览器
        /// </summary>
        public Browsers Browsers { get; set; }

        /// <summary>
        /// 提空柜(Empty Pick-up)
        /// </summary>
        public string EmptyPickUp { get; set; }
        /// <summary>
        /// 提重柜(Full Pick-up)
        /// </summary>
        public string FullPickUp { get; set; }
        /// <summary>
        /// 装船(Loaded on board)
        /// </summary>
        public string LOBD { get; set; }
        /// <summary>
        /// 卸船(Un Loaded on board)
        /// </summary>
        public string UNLOBD { get; set; }
        /// <summary>
        /// 还空柜(Return Empty Container)
        /// </summary>
        public string REC { get; set; }

        /// <summary>
        /// 网站参数
        /// </summary>
        List<CrawlConfigParam> _EWebsiteParams;
        /// <summary>
        /// 网站参数
        /// </summary>
        public List<CrawlConfigParam> WebsiteParams
        {
            get { return _EWebsiteParams ?? (_EWebsiteParams = new List<CrawlConfigParam>()); }
            set { _EWebsiteParams = value; }
        }

        /// <summary>
        /// 是否包含键
        /// </summary>
        /// <param name="paramKey">包含键值</param>
        /// <returns></returns>
        public bool ContainsKey(string paramKey)
        {
            return WebsiteParams.Any(fitem => fitem.KeyValue == paramKey);
        }

        /// <summary>
        /// 通过Key获取网站明细配置
        /// </summary>
        /// <param name="paramKeyID"></param>
        /// <returns></returns>
        public CrawlConfigParam GetWebsiteParamByID(Guid paramKeyID)
        {
            return WebsiteParams.SingleOrDefault(fItem => fItem.KeyID.Equals(paramKeyID));
        }

        /// <summary>
        /// 通过Key获取网站明细配置
        /// </summary>
        /// <param name="paramKey"></param>
        /// <returns></returns>
        public CrawlConfigParam GetWebsiteParamByKey(string paramKey)
        {
            return WebsiteParams.SingleOrDefault(fItem => fItem.KeyValue == paramKey);
        }
        /// <summary>
        /// 通过Key获取网站参数值
        /// </summary>
        /// <param name="paramKey"></param>
        /// <returns></returns>
        public string GetParamValueByKey(string paramKey)
        {
            string paramValue = string.Empty;
            CrawlConfigParam webParam = WebsiteParams.SingleOrDefault(fItem => fItem.KeyValue == paramKey);
            if (webParam != null)
                paramValue = webParam.ParamValue;
            return paramValue;
        }
        

        /// <summary>
        /// 通过Key获取网站参数值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="paramKey"></param>
        /// <returns></returns>
        public string GetParamValueByKey<T>(T obj, string paramKey)
        {
            string paramValue = string.Empty;
            CrawlConfigParam webParam = WebsiteParams.SingleOrDefault(fItem => fItem.KeyValue == paramKey);
            if (webParam != null)
                paramValue = webParam.GetParamValueByKey(obj);
            return paramValue;
        }
    }
}
