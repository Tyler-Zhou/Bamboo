#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/1/4 星期四 14:15:49
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using Cityocean.Crawl.CommonLibrary;

namespace Cityocean.Crawl.LogComponents
{
    /// <summary>
    /// 日志服务
    /// </summary>
    public sealed class LogService
    {
        /// <summary>
        /// 记录消息Queue
        /// </summary>
        private readonly ConcurrentQueue<LogMessage> _que;

        /// <summary>
        /// 信号
        /// </summary>
        private readonly ManualResetEvent _mre;

        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog _log;

        /// <summary>
        /// 日志服务
        /// </summary>
        private static readonly LogService _flashLog = new LogService();


        private LogService()
        {
            var configFile = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config"));
            if (!configFile.Exists)
            {
                throw new Exception("未配置log4net配置文件！");
            }
            // 设置日志配置文件路径
            XmlConfigurator.Configure(configFile);

            _que = new ConcurrentQueue<LogMessage>();
            _mre = new ManualResetEvent(false);
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        /// <summary>
        /// 实现单例
        /// </summary>
        /// <returns></returns>
        public static LogService Instance()
        {
            return _flashLog;
        }

        /// <summary>
        /// 另一个线程记录日志，只在程序初始化时调用一次
        /// </summary>
        public void Register()
        {
            Task.Factory.StartNew(WriteLog);
        }

        /// <summary>
        /// 从队列中写日志至磁盘
        /// </summary>
        [HandleProcessCorruptedStateExceptions]
        private void WriteLog()
        {
            while (GlobalVariable.ServiceIsRuning)
            {
                // 等待信号通知
                _mre.WaitOne();

                LogMessage msg;
                // 判断是否有内容需要写入磁盘 从列队中获取内容，并删除列队中的内容
                while (_que.Count > 0 && _que.TryDequeue(out msg))
                {
                    try
                    {
                        // 判断日志等级，然后写日志
                        switch (msg.Level)
                        {
                            case LogLevel.Debug:
                                _log.Debug(msg.Message, msg.Exception);
                                break;
                            case LogLevel.Info:
                                _log.Info(msg.Message, msg.Exception);
                                break;
                            case LogLevel.Error:
                                _log.Error(msg.Message, msg.Exception);
                                break;
                            case LogLevel.Warn:
                                _log.Warn(msg.Message, msg.Exception);
                                break;
                            case LogLevel.Fatal:
                                _log.Fatal(msg.Message, msg.Exception);
                                break;
                        }
                    }
                    catch
                    {
                    }
                }

                // 重新设置信号
                _mre.Reset();
                Thread.Sleep(1);
            }
        }


        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="module">日志模块</param>
        /// <param name="message">日志文本</param>
        /// <param name="level">等级</param>
        /// <param name="ex">Exception</param>
        public void EnqueueMessage(string module, string message, LogLevel level, Exception ex = null)
        {
            if (string.IsNullOrEmpty(module))
                module = "System";
            if ((level != LogLevel.Debug || !_log.IsDebugEnabled) && (level != LogLevel.Error || !_log.IsErrorEnabled) &&
                (level != LogLevel.Fatal || !_log.IsFatalEnabled) && (level != LogLevel.Info || !_log.IsInfoEnabled) &&
                (level != LogLevel.Warn || !_log.IsWarnEnabled)) return;
            _que.Enqueue(new LogMessage
            {
                Message = string.Format("{0}\t{1}\t{2}\t", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff"), module, message),
                Level = level,
                Exception = ex
            });

            // 通知线程往磁盘中写日志
            _mre.Set();
        }
        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="module"></param>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Debug(string module, string msg, Exception ex = null)
        {
#if DEBUG
            Instance().EnqueueMessage(module, msg, LogLevel.Debug, ex);
#endif
        }
        /// <summary>
        /// Error
        /// </summary>
        /// <param name="module"></param>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Error(string module, string msg, Exception ex = null)
        {
            Instance().EnqueueMessage(module, msg, LogLevel.Error, ex);
        }
        /// <summary>
        /// Fatal
        /// </summary>
        /// <param name="module"></param>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Fatal(string module, string msg, Exception ex = null)
        {
            Instance().EnqueueMessage(module, msg, LogLevel.Fatal, ex);
        }
        /// <summary>
        /// Info
        /// </summary>
        /// <param name="module"></param>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Info(string module, string msg, Exception ex = null)
        {
            Instance().EnqueueMessage(module, msg, LogLevel.Info, ex);
        }
        /// <summary>
        /// Warn
        /// </summary>
        /// <param name="module"></param>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Warn(string module, string msg, Exception ex = null)
        {
            Instance().EnqueueMessage(module, msg, LogLevel.Warn, ex);
        }
        /// <summary>
        /// 写入异常
        /// </summary>
        /// <param name="ex"></param>
        public static void Write(Exception ex)
        {
            try
            {
                string filePath = AppDomain.CurrentDomain.BaseDirectory + DateTime.Now.ToString("yyyy-MM-dd") + ".Log";
                string message = string.Format("Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                File.AppendAllText(filePath, message);
            }
            catch
            {
            }
        }
    }
}
