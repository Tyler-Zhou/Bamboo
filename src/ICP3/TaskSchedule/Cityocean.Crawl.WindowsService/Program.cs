#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/3/3 15:01:38
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System.ServiceProcess;

namespace Cityocean.Crawl.WindowsService
{
    /// <summary>
    /// 
    /// </summary>
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun ={ 
                new ICPCrawlService() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
