using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.IO;
using ICP.Framework.CommonLibrary;
using ICP.Message.ServiceInterface;
using System.Threading;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using System.Text;

namespace ICP.OA.FaxStateUpdateService
{   
    /// <summary>
    /// 传真日志状态更新服务类
    /// </summary>
    public partial class FaxStateUpdateService : ServiceBase
    {
        string serviceAddress = string.Empty;
        string serviceNamespace=string.Empty;
        /// <summary>
        /// 构造函数
        /// </summary>
        public FaxStateUpdateService()
        {
            InitializeComponent();
            
        }
        /// <summary>
        /// 服务启动时 启动日志文件监控线程
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            try
            {
                ReadConfigs();
                EnsureFileWatcherThreadExists();
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog(ex.Message + ex.StackTrace);
            }
        }

        private void ReadConfigs()
        {
         
                serviceAddress = GetConfigValue("serviceAddress");
                serviceNamespace=GetConfigValue("serviceNameSpace");
                faxLogFileName=GetConfigValue("logFileName");
                faxLogFilePath=GetConfigValue("logFilePath");
        }
        
        /// <summary>
        /// 服务停止时 停止日志监控线程
        /// </summary>
        protected override void OnStop()
        {
            if (thread != null && thread.ThreadState == System.Threading.ThreadState.Running)
            {
             thread.Abort();
            }
            if (fileWatcher != null)
            {
                fileWatcher.Dispose();
            }
        }
        #region 传真日志相关

        FileSystemWatcher fileWatcher;
        object syncObj = new object();
        DateTime dtLastScan;
        /// <summary>
        /// 传真日志文件名称
        /// 'sendlog.txt'
        /// </summary>
         string faxLogFileName = "sendlog.txt";
        /// <summary>
        /// 传真日志文件路径
        /// </summary>
         string faxLogFilePath =string.Empty;
        /// <summary>
        /// 检查传真日志文件，更新传真发送状态工作线程
        /// </summary>
        System.Threading.Thread thread;
        /// <summary>
        /// 本地时间
        /// </summary>
        private void SetScanTime()
        {
            dtLastScan = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
        }
        /// <summary>
        /// 确保更新传真发送状态工作线程存在
        /// </summary>
        private void EnsureFileWatcherThreadExists()
        {
            if (thread == null)
            {
                lock (syncObj)
                {
                    thread = new System.Threading.Thread(ConfigFileWatcher);
                    thread.Start();
                }

            }
        }
        /// <summary>
        /// 配置传真日志文件监视者
        /// </summary>
        private void ConfigFileWatcher()
        {
            try
            {

                if (fileWatcher != null)
                    return;
                fileWatcher = new FileSystemWatcher();
                fileWatcher.Path = faxLogFilePath;
                fileWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.CreationTime;
                fileWatcher.Filter = faxLogFileName;
                fileWatcher.Changed += new FileSystemEventHandler(OnFaxLogFileChanged);
                fileWatcher.Created += new FileSystemEventHandler(OnFaxLogFileChanged);
                fileWatcher.EnableRaisingEvents = true;
                SetScanTime();
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog(ex.Message + ex.StackTrace);
            }
        }

        private string GetFaxLogFilePath()
        {
             return GetConfigValue("logfilePath");
            
        }
        private string GetFaxLogFileName()
        {
            return GetConfigValue("logfileName");
        }
        private string GetConfigValue(string key)
        {
            return System.Configuration.ConfigurationSettings.AppSettings[key];
        }
        /// <summary>
        /// 日志文件改变时，过滤传真发送相关条目，更新数据库中相应传真发送日志状态
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFaxLogFileChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                //LogHelper.SaveLog("监听到日志文件修改");
                fileWatcher.EnableRaisingEvents = false;
                WaitCallback callback = data =>
                    {
                        UpdateFaxLogState();
                    };
                ThreadPool.QueueUserWorkItem(callback);
                
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog(ex.Message + ex.StackTrace);
            }
            finally
            {
                fileWatcher.EnableRaisingEvents = true;
            }
        }
        private void UpdateFaxLogState()
        {
            //LogHelper.SaveLog("开始获取更新日志条目");
            List<string> lines = GetFaxLog();
            SetScanTime();
            //LogHelper.SaveLog(string.Format("筛选后的日志条目:{0}",lines.ToArray().Join()));
            InnerUpdateFaxLogState(lines);
        }

        /// <summary>
        /// 获取Message状态
        /// </summary>
        /// <param name="logEntrys"></param>
        private void InnerUpdateFaxLogState(List<string> logEntrys)
        {
            //LogHelper.SaveLog("开始获取Message状态");
            if (logEntrys == null || logEntrys.Count <= 0) 
            {
                //LogHelper.SaveLog("logEntrys对象为空，return");
                return;
            }
            
            List<string> messageIds = new List<string>();
            List<MessageState> states = new List<MessageState>();
            foreach (string entry in logEntrys)
            {
                string[] info = entry.Split(',');
                string messageId = info[6].Replace("\"", "");
                
                string state = info[11].Replace("\"", "");
                //如果日志ID为空或者状态为空 则不处理
                if (string.IsNullOrEmpty(messageId) ||  string.IsNullOrEmpty(state))
                    continue;

                messageIds.Add(messageId);
                MessageState messageState = (MessageState)Enum.Parse(typeof(MessageState), state,true);
                states.Add(messageState);
            }
            //LogHelper.SaveLog("获取到messageIds:"+messageIds.ToArray().Join());
            //LogHelper.SaveLog("获取到States:" + states.ToArray().Join());
            ChangeMessageState(messageIds, states);

        }
        /// <summary>
        /// 改变邮件标志
        /// </summary>
        /// <param name="ids">传真messageId</param>
        /// <param name="states">状态</param>
        public void ChangeMessageState(List<string> ids,List<MessageState> states)
        {
            //LogHelper.SaveLog("准备调用服务："+serviceAddress);
            try
            {
                IMessageService messageService = ServiceClient.GetService<IMessageService>();
                    messageService.ChangeState(ids.ToArray(), states.ToArray(), MessageType.Fax);
                    //LogHelper.SaveLog("调用服务结束");
                
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                Exception innerException = ex.InnerException;
                while (innerException != null)
                {
                    error += Environment.NewLine + innerException.Message;
                    innerException = innerException.InnerException;
                }
                LogHelper.SaveLog(error);
                
            }
        }
         private string BuildExceptionString(Exception exception)
        {
            string errMessage = string.Empty;

            errMessage +=
                exception.Message + Environment.NewLine + exception.StackTrace;
            while (exception.InnerException != null)
            {
                errMessage += BuildInnerExceptionString(exception.InnerException);

                exception = exception.InnerException;
            }

            return errMessage;
        }
          private string BuildInnerExceptionString(Exception innerException)
         {
             string errMessage = string.Empty;

             errMessage += Environment.NewLine + " InnerException ";
             errMessage += Environment.NewLine + innerException.Message + Environment.NewLine + innerException.StackTrace;

             return errMessage;
         }

        /// <summary>
        /// 对比日志记录找出更新的日志记录
        /// </summary>
        /// <returns></returns>
        private List<string> GetFaxLog()
        {
            string faxLogFileFullPath = Path.Combine(faxLogFilePath, faxLogFileName);
            //LogHelper.SaveLog(string.Format("获取日志的路径为{0}",faxLogFileFullPath));
            // string[] lines;
            var lines = System.IO.File.ReadAllLines(faxLogFileFullPath, Encoding.UTF8).ToList<string>();
                //    using (FileStream fs = new FileStream(faxLogFileFullPath, FileMode.Open, FileAccess.Read))
                //{
                //using (StreamReader sr= new StreamReader(fs))
                //    {
                //    string all = sr.ReadToEnd();
                //    lines = all.Split('\n');
                    
                //    }
                //}
            //LogHelper.SaveLog("获取所有日志条目："+lines.ToArray().Join());         
            return lines.Where(FilterLogInfo).ToList();
        }

        private Boolean FilterLogInfo(string line)
        {
            if (string.IsNullOrEmpty(line))
                return false;
            if (line.StartsWith("Date", StringComparison.InvariantCultureIgnoreCase))
                return false;
            string[] info = line.Split(',');
            //当前格式为15个字段
            if (info == null || info.Length < 15)
                return false;

            //LogHelper.SaveLog(string.Format("info[6]前为{0}",info[6].ToString()));
            string logId = info[6].Replace("\"", "");
            //LogHelper.SaveLog(string.Format("info[6]后为{0}", info[6].ToString()));
            if (string.IsNullOrEmpty(logId))
                return false;
            string date = info[0].Replace("\"","");
            string time = info[1].Replace("\"","");
            //获取发送时间
            DateTime dtRecord = DateTime.Parse(string.Format("{0} {1}", date, time));
            //LogHelper.SaveLog(string.Format("dtRecord{0} - dtLastScan{1}", dtRecord.ToString(), dtLastScan.ToString()));
            //发送时间- 上次扫描时间
            if ((dtRecord - dtLastScan).TotalSeconds <= 0)
            return false;      
            return true;
        }
        #endregion
    }
}
