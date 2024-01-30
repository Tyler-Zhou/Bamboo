#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/3/3 15:30:41
 *
 * Description:2017/3/27 7:45:30
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System.Runtime.ExceptionServices;
using System.Security;
using Cityocean.Crawl.CommonLibrary;
using Cityocean.Crawl.LogComponents;
using Cityocean.Crawl.NoticeComponents;
using Cityocean.Crawl.ServerComponents;
using Cityocean.Crawl.ServiceInterface;
using Quartz;
using System;

namespace Cityocean.Crawl.JobSchedule
{
    /// <summary>
    /// 计划任务-码头箱状态
    /// </summary>
    [DisallowConcurrentExecution]
    public sealed class JobTerminalContainerStatus : IJob
    {
        #region Property
        /// <summary>
        /// 码头箱状态服务
        /// </summary>
        ITerminalContainerStatusService TSService { get; set; }

        private IMailService _MService;
        /// <summary>
        /// 邮件服务
        /// </summary>
        IMailService MService
        {
            get { return _MService ?? (_MService = new MailService()); }
        }
        #endregion

        #region IJob 成员
        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="context"></param>
        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                if (!JobMemoryCache.AddJob(context.JobDetail.Key.Group, context.JobDetail.Description)) return;
                LogService.Info(CommonConstants.MODULENAME_SCHEDULERCENTER, string.Format("{0}-{1}开始:{2}",context.JobDetail.Key.Group, context.JobDetail.Key.Name, context.JobDetail.Description));
                TSService = new TerminalContainerStatusService();
                TSService.CrawlerData();
                JobMemoryCache.RemoveJob(context.JobDetail.Key.Group);
                LogService.Info(CommonConstants.MODULENAME_SCHEDULERCENTER, string.Format("{0}-{1}结束", context.JobDetail.Key.Group,context.JobDetail.Key.Name));
            }
            catch (Exception ex)
            {
                JobMemoryCache.RemoveJob(context.JobDetail.Key.Group);
                MService.SendEMail(CommonConstants.MODULENAME_SCHEDULERCENTER, context.JobDetail.Key.Name, ex);
                LogService.Error(CommonConstants.MODULENAME_SCHEDULERCENTER, context.JobDetail.Description, ex);
            }
        }
        #endregion
    }
}
