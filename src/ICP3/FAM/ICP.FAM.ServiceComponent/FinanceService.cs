using ICP.Framework.CommonLibrary;
using ICP.Message.ServiceInterface;

namespace ICP.FAM.ServiceComponent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ServiceInterface;
    using ServiceInterface.DataObjects;
    using Framework.CommonLibrary.Common;
    using Framework.CommonLibrary.Server;
    using ICP.Common.ServiceInterface;
    using FCM.Common.ServiceInterface;
    using Microsoft.Practices.CompositeUI;
    using Sys.ServiceInterface;
    using FCM.OceanExport.ServiceInterface;
    using System.Threading;
    using System.Text;
    using System.IO;
    using ICP.Framework.CommonLibrary.Helper;
    /// <summary>
    /// 财务服务
    /// </summary>
    public partial class FinanceService : IFinanceService
    {
        private ISessionService _sessionService;
        private IMessageService _emailService;
        /// <summary>
        /// 操作日志服务
        /// </summary>
        IOperationLogService OperationLogService;

        [ServiceDependency]
        public IUserService userService { get; set; }
        private IConfigureService _configureService;
        private ISystemService _systemService { get; set; }
        private IFCMCommonService _fcmCommonService { get; set; }
        private IUserService _userService { get; set; }
        private IOceanExportService _oceanexportservice { get; set; }

        static List<ReleaseAndArList> waitSendReleaseList = new List<ReleaseAndArList>();
        static List<ReleaseAndArList> SendReleaseList = new List<ReleaseAndArList>();

        static List<ReleaseAndArList> waitSendARList = new List<ReleaseAndArList>();
        static List<ReleaseAndArList> SendARList = new List<ReleaseAndArList>();

        public Timer timerEmail;

        INIHelper iniConfig;
        /// <summary>
        /// INI 配置文件
        /// </summary>
        INIHelper INIConfig
        {
            get
            {
                return iniConfig ?? (iniConfig = new INIHelper(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,FrameworkCommandConstants.CONFIG_ICPSERVICE)));
            }
        }

        public FinanceService(ISessionService sessionService,
                            IMessageService emailService,
                            IConfigureService configureService,
                            ISystemService systemService,
                            IOceanExportService oceanexportservice,
                            IOperationLogService operationLogService,
                            IFCMCommonService fcmCommonService,
                            IUserService UserService)
        {
            _sessionService = sessionService;
            _emailService = emailService;
            _configureService = configureService;
            _systemService = systemService;
            _oceanexportservice = oceanexportservice;
            OperationLogService = operationLogService;
            _fcmCommonService = fcmCommonService;
            _userService = UserService;
            bool isSendNoticeAR = false;
            try
            {
                isSendNoticeAR = Convert.ToBoolean(INIConfig.IniReadValue(FrameworkCommandConstants.SECTION_TASKSCHEDULE, FAMCommonConstants.TASKSCHEDULE_NOTICEAR));
            }
            catch
            {
                isSendNoticeAR = false;
            }
            if (isSendNoticeAR)
            {
                //发送催款通知:启动服务1分钟后开始计时器;每次间隔24小时
                TimeSpan tsDelayed = INIConfig.IniReadValue(FrameworkCommandConstants.SECTION_TASKSCHEDULE, FAMCommonConstants.TASKSCHEDULE_NOTICEAR_DELAYED).ToTimeSpan();
                TimeSpan tsInterval = INIConfig.IniReadValue(FrameworkCommandConstants.SECTION_TASKSCHEDULE, FAMCommonConstants.TASKSCHEDULE_NOTICEAR_INTERVAL).ToTimeSpan();
                timerEmail = new Timer(OnTimedEventCheckAR, this, tsDelayed, tsInterval);
            }
        }

        private void OnTimedEventCheckAR(object source)
        {
            try
            {
                waitSendARList.Clear();
                SendARList.Clear();
                List<ReleaseAndArList> alllist = GetReleaseAndArList(null);
                //自动发送暂时深圳公司使用
                List<ReleaseAndArList> currentArlist = null;
                if (alllist != null && alllist.Count > 0)
                {
                    currentArlist = GetReleaseAndArList(null).FindAll(r => r.CompanyID.ToString().ToUpper() == "41D7D3FE-183A-41CD-A725-EB6F728541EC");
                }

                if (currentArlist != null)
                {
                    currentArlist.ForEach(delegate(ReleaseAndArList ral)
                    {
                        if (waitSendARList.Count(r => r.ID == ral.ID) == 0)
                        {
                            waitSendARList.Add(ral);
                        }
                        else
                        {
                            ReleaseAndArList find = waitSendARList.Find(r => r.ID == ral.ID);
                            find = ral;
                        }
                    });
                }


                try
                {
                    waitSendARList.ForEach(delegate(ReleaseAndArList waitsend)
                    {
                        if (SendReleaseList.Count(r => waitsend.ID == r.ID && r.Days == waitsend.Days) == 0)
                        {
                            int interval = 60*1000;
                            List<EventObjects> events = _fcmCommonService.GetMemoList(waitsend.OperationID, DataSearchType.Local, null);
                            if (events.Count(r => r.Subject == "Send a reminder message" && ((DateTime)r.OccurrenceTime).ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd")) == 0)
                            {
                                if (waitsend.Days == 7)
                                {
                                    SendAREmail(waitsend, false, true);
                                    Thread.Sleep(interval);
                                }
                                else if (waitsend.Days == 3 || waitsend.Days == 0)
                                {
                                    SendAREmail(waitsend, true, true);
                                    Thread.Sleep(interval);
                                }
                                else if (waitsend.Days < 0)
                                {
                                    if (Math.Abs(waitsend.Days) % 2 == 0)
                                    {
                                        SendAREmail(waitsend, true, true);
                                        Thread.Sleep(interval);
                                    }
                                }
                            }
                        }
                    });

                    waitSendARList.RemoveAll(delegate(ReleaseAndArList sended)
                    {
                        if (SendReleaseList.Count(r => sended.ID == r.ID && r.Days == sended.Days) == 0)
                        {
                            return true;
                        }
                        else
                            return false;
                    });
                }
                catch (Exception ex)
                {
                    SaveArErrorLogInfo(ex.Data.ToString() + ":" + ex.Message);
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                SaveArErrorLogInfo(ex.Message);
            }
        }

        /// <summary>
        /// 保存当次催款日志
        /// </summary>
        /// <param name="message"></param>
        public void SaveArLogInfo(string message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\EmailLog\\ARLogs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":  " + message + Environment.NewLine;

            StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8);
            sw.Write(str);
            sw.Close();
        }

        /// <summary>
        /// 保存当次催款日志
        /// </summary>
        /// <param name="message"></param>
        public void SaveArErrorLogInfo(string message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\EmailLog\\ErrorLogs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":  " + message + Environment.NewLine;

            StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8);
            sw.Write(str);
            sw.Close();
        }

        /// <summary>
        /// 是否英文环境
        /// </summary>
        private bool IsEnglish
        {
            get
            {
                try
                {
                    return ApplicationContext.Current.IsEnglish;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// CSP用户ID
        /// </summary>
        public int CSPUserID
        {
            get { return 0; }
        }
    }
}
