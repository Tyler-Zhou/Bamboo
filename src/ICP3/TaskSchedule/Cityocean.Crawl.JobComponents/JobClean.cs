#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/12/14 星期四 15:18:48
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using Cityocean.Crawl.CommonLibrary;
using Cityocean.Crawl.LogComponents;
using Quartz;
using System;

namespace Cityocean.Crawl.JobSchedule
{
    /// <summary>
    /// 
    /// </summary>
    [DisallowConcurrentExecution]
    public sealed class JobClean : IJob
    {
        #region IJob 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                if (!JobMemoryCache.AddJob(context.JobDetail.Key.Group, context.JobDetail.Description)) return;
                LogService.Info(CommonConstants.MODULENAME_SCHEDULERCENTER, string.Format("{0}-{1}开始:{2}",context.JobDetail.Key.Group, context.JobDetail.Key.Name, context.JobDetail.Description));
                FileDirectoryEnumerable fdeOldFile = new FileDirectoryEnumerable
                    {
                        SearchPath = GlobalVariable.ProgramDirectory,
                        ReturnStringType = false,
                        SearchForDirectory = true,
                        SearchForFile = true,
                        ThrowIOException = true,
                    };
                foreach (object fdItem in fdeOldFile)
                {
                    
                }
                JobMemoryCache.RemoveJob(context.JobDetail.Key.Group);
                LogService.Info(CommonConstants.MODULENAME_SCHEDULERCENTER, string.Format("{0}-{1}结束", context.JobDetail.Key.Group,context.JobDetail.Key.Name));
            }
            catch (Exception ex)
            {
                LogService.Error(CommonConstants.MODULENAME_SCHEDULERCENTER, context.JobDetail.Description, ex);
            }
        } 
        #endregion
    }
}
