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

using Cityocean.Crawl.CommonLibrary;
using Cityocean.Crawl.LogComponents;
using Cityocean.Crawl.ServerComponents;
using Cityocean.Crawl.ServiceInterface;
using Quartz;
using Quartz.Impl;
using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceProcess;

namespace Cityocean.Crawl.WindowsService
{
    /// <summary>
    /// 抓取服务
    /// </summary>
    public partial class ICPCrawlService : ServiceBase
    {
        #region Fields
        /// <summary>
        /// 
        /// </summary>
        private IScheduler _Scheduler;
        /// <summary>
        /// 服务Host
        /// </summary>
        private WebServiceHost serviceHost;
        #endregion

        #region Property
        #endregion

        #region Init
        /// <summary>
        /// 抓取服务
        /// </summary>
        public ICPCrawlService()
        {
            InitializeComponent();
            try
            {
                AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;
                KillCrawlProcess();
                
            }
            catch (Exception ex)
            {
                LogService.Write(ex);
                throw;
            }
        } 
        #endregion

        #region Override
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            try
            {
                GlobalVariable.ServiceIsRuning = true;
                GlobalVariable.SessionID = Guid.NewGuid();
                LogService.Instance().Register();
                InitConfig();
                InitGlobalData();
                StartSchedulerJob();
                StartWCFService();
            }
            catch (Exception ex)
            {
                LogService.Write(ex);
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void OnStop()
        {
            try
            {
                GlobalVariable.ServiceIsRuning = false;
                StopWCFService();
                StopSchedulerJob();
                KillCrawlProcess();
                DisposeResource();
            }
            catch (Exception ex)
            {
                LogService.Error("CrawlerService", "停止服务", ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void OnPause()
        {
            try
            {
                _Scheduler.PauseAll();
            }
            catch (Exception ex)
            {
                LogService.Error("CrawlerService", "暂停服务",ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void OnContinue()
        {
            try
            {
                _Scheduler.ResumeAll();
            }
            catch (Exception ex)
            {
                LogService.Error("CrawlerService", "继续服务", ex);
            }
        } 
        #endregion

        #region Custom Method
        /// <summary>
        /// 
        /// </summary>
        private void KillCrawlProcess()
        {
            try
            {
                string crawlProcesss = INIHelper.Instance.IniReadValue(CommonConstants.MODULENAME_SERVICECONFIG,
                        CommonConstants.CONFIG_CRAWLPROCESSS);
                foreach (var crawlProcess in crawlProcesss.Split(','))
                {
                    ProcessHelper.KillProcess(crawlProcess);
                }
            }
            catch (Exception ex)
            {
                LogService.Error("CrawlerService","结束抓取进程",ex);
            }
        }
        /// <summary>
        /// 初始化配置文件
        /// </summary>
        private void InitConfig()
        {
            try
            {
                if (!File.Exists(GlobalVariable.ConfigPath))
                {
                    #region ServiceConfig
                    INIHelper.Instance.IniWriteValue(CommonConstants.MODULENAME_SERVICECONFIG, CommonConstants.CONFIG_CRAWLPROCESSS, "PhantomJS,ChromeDriver,IEDriverServer");
                    INIHelper.Instance.IniWriteValue(CommonConstants.MODULENAME_SERVICECONFIG, CommonConstants.CONFIG_MAILUSERNAME, "ICPSystem");
                    INIHelper.Instance.IniWriteValue(CommonConstants.MODULENAME_SERVICECONFIG, CommonConstants.CONFIG_MAILUSERNAME, "ICPSystem");
                    INIHelper.Instance.IniWriteValue(CommonConstants.MODULENAME_SERVICECONFIG, CommonConstants.CONFIG_MAILPASSWORD, "ICPOceanCity");
                    INIHelper.Instance.IniWriteValue(CommonConstants.MODULENAME_SERVICECONFIG, CommonConstants.CONFIG_SMTPSERVER, "mail.cityocean.com");
                    INIHelper.Instance.IniWriteValue(CommonConstants.MODULENAME_SERVICECONFIG, CommonConstants.CONFIG_MAILFROM, "ICPSystem@cityocean.com");
                    INIHelper.Instance.IniWriteValue(CommonConstants.MODULENAME_SERVICECONFIG, CommonConstants.CONFIG_MAILTO, "TaylorZhou@cityocean.com");
                    INIHelper.Instance.IniWriteValue(CommonConstants.MODULENAME_SERVICECONFIG, CommonConstants.CONFIG_MAILCC, "");
                    INIHelper.Instance.IniWriteValue(CommonConstants.MODULENAME_SERVICECONFIG, CommonConstants.CONFIG_CRAWLEXCEPTION, "(NoResponse)|(BlankPage)|(NoData)");
                    INIHelper.Instance.IniWriteValue(CommonConstants.MODULENAME_SERVICECONFIG, CommonConstants.CONFIG_CRAWLIGNOREXCEPTION, "NoData");
                    #endregion

                    #region CargoTracking
                    INIHelper.Instance.IniWriteValue(CrawlCommonConstants.MODULENAME_CARGOTRACKING, CargoTrackingConstants.CONFIG_ESTBEFOREDAYS, "2");
                    INIHelper.Instance.IniWriteValue(CrawlCommonConstants.MODULENAME_CARGOTRACKING, CargoTrackingConstants.CONFIG_ETDRANGE, "7");
                    INIHelper.Instance.IniWriteValue(CrawlCommonConstants.MODULENAME_CARGOTRACKING, CrawlCommonConstants.EXCEPTION_BATCHNO, "");
                    #endregion

                    #region Terminal Vessel Schedule
                    INIHelper.Instance.IniWriteValue(CrawlCommonConstants.MODULENAME_TERMINALVESSELSCHEDULE, CrawlCommonConstants.EXCEPTION_BATCHNO, "");
                    #endregion

                    #region Terminal Container Dynamic
                    INIHelper.Instance.IniWriteValue(CrawlCommonConstants.MODULENAME_TERMINALCONTAINERSTATUS, CrawlCommonConstants.EXCEPTION_BATCHNO, "");
                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw new Exception("初始化配置文件发生异常:\r\n"+ex);
            }
        }
        /// <summary>
        /// 初始化全局数据
        /// </summary>
        private void InitGlobalData()
        {
            try
            {
                LogService.Info("CrawlerService", "[全局资源]已初始化");
            }
            catch (Exception ex)
            {
                throw new Exception("获取[全局资源]发生异常:\r\n" ,ex);
            }
        }
        /// <summary>
        /// 开始抓取任务
        /// </summary>
        private void StartSchedulerJob()
        {
            try
            {
                _Scheduler = StdSchedulerFactory.GetDefaultScheduler();
                _Scheduler.Start();
                LogService.Info("CrawlerService", "调度任务启动成功");
            }
            catch (Exception ex)
            {
                throw new Exception("调度任务启动异常", ex);
            }
        }
        /// <summary>
        /// WCF 服务:
        /// 1. 挂载查询航期服务
        /// </summary>
        private void StartWCFService()
        {
            try
            {
                serviceHost = new WebServiceHost(typeof(WebCrawlerService));
                serviceHost.Open();
                LogService.Info("CrawlerService", "WCF服务启动成功");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("WCF服务启动异常:{0}",ex.Message));
            }
        }
        /// <summary>
        /// 停止WCF服务
        /// </summary>
        private void StopWCFService()
        {
            try
            {
                if (serviceHost != null && serviceHost.State == CommunicationState.Opened)
                {
                    serviceHost.Close();
                }
                serviceHost = null;
                LogService.Info("CrawlerService", "WCF服务成功终止");
            }
            catch (Exception ex)
            {
                LogService.Error("CrawlerService", "WCF服务停止", ex);
            }
        }
        /// <summary>
        /// 停止抓取任务
        /// </summary>
        private void StopSchedulerJob()
        {
            try
            {
                if (_Scheduler != null)
                    _Scheduler.Shutdown(false);
                LogService.Info("CrawlerService","调度任务成功终止");
            }
            catch (Exception ex)
            {
                LogService.Error("CrawlerService", "调度任务停止", ex);
            }
        }
        /// <summary>
        /// 清理资源
        /// </summary>
        private void DisposeResource()
        {
            try
            {
                LogService.Info("CrawlerService", "已清理全局变量[网站配置]资源");
                LogService.Info("CrawlerService", "已清理全局变量[船东状态配置]资源");
            }
            catch (Exception ex)
            {
                LogService.Error("CrawlerService", "清理全局变量资源", ex);
            }
        }

        void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            // Get Exception
            Exception exObj = args.ExceptionObject as Exception;
            if (exObj != null)
            {
                LogService.Write(exObj);
            }
            StopSchedulerJob();
            StartSchedulerJob();
        }
        #endregion
    }
}
