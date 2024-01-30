using ICP.Crawler.CommonLibrary.Enum;
using ICP.Crawler.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ICP.FCM.Common.ServiceInterface
{
    /// <summary>
    /// 爬虫服务接口
    /// </summary>
    [ServiceInfomation("爬虫接口")]
    [ServiceContract]
    public interface ICrawlerService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="terminal"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        object StartCrawler(Terminals terminal, CrawlerMode mode = CrawlerMode.Normal);
    }
}
