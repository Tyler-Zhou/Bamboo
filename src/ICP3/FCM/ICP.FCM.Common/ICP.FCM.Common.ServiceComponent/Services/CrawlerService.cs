using ICP.Crawler.CommonLibrary;
using ICP.Crawler.CommonLibrary.Enum;
using ICP.Crawler.ServiceInterface;
using ICP.Crawler.ServiceInterface.DataObjects;

namespace ICP.FCM.Common.ServiceComponent
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CrawlerService : ICP.FCM.Common.ServiceInterface.ICrawlerService
    {
        static ICrawlerService crawlerService = ServiceProxyFactory.Create<ICrawlerService>("CrawlerService");

        //public void LoginFailedListClear()
        //{
        //    crawlerService.LoginFailedListClear();
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="terminals"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public object StartCrawler(Terminals terminals, CrawlerMode mode = CrawlerMode.Normal)
        {
            return crawlerService.StartCrawler(terminals, mode);
        }
    }
}
