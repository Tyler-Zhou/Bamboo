﻿#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/11/12 16:22:15
 *
 * Description:
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
    /// 计划任务-货物跟踪
    /// </summary>
    [DisallowConcurrentExecution]
    public sealed class JobAnalysisCargoTracking : IJob
    {
        #region Fields

        #endregion

        #region Property
        /// <summary>
        /// 货物动态服务
        /// </summary>
        ICargoTrackingService CTService { get; set; }

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
        /// 
        /// </summary>
        /// <param name="context"></param>
        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                LogService.Info(CommonConstants.MODULENAME_SCHEDULERCENTER,
                    string.Format("{0}-{1}开始:{2}", context.JobDetail.Key.Group, context.JobDetail.Key.Name, context.JobDetail.Description));
                CTService = new CargoTrackingService();
                CTService.AnalysisData();
                LogService.Info(CommonConstants.MODULENAME_SCHEDULERCENTER, string.Format("{0}-{1}结束", context.JobDetail.Key.Group, context.JobDetail.Key.Name));
            }
            catch (Exception ex)
            {
                MService.SendEMail(CommonConstants.MODULENAME_SCHEDULERCENTER, context.JobDetail.Key.Name, ex);
                LogService.Error(CommonConstants.MODULENAME_SCHEDULERCENTER, context.JobDetail.Description, ex);
            }
        }
        #endregion
    }
}
