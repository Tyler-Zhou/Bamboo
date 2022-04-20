using Bamboo.Server.Interface;
using Bamboo.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;

namespace Bamboo.Server.Service
{
    /// <summary>
    /// 日志服务
    /// </summary>
    public class LogService : ILogService
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// 
        /// </summary>
        private readonly ILogger<LogService> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="logger"></param>
        public LogService(IHttpContextAccessor httpContextAccessor, ILogger<LogService> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="ex"></param>
        public void LogError(Exception ex)
        {
            LogMessage logMessage = new LogMessage();
            logMessage.IpAddress = _httpContextAccessor.HttpContext.Request.Host.Host;
            if (ex.InnerException != null)
                logMessage.LogInfo = ex.InnerException.Message;
            else
                logMessage.LogInfo = ex.Message;
            logMessage.StackTrace = ex.StackTrace;
            logMessage.OperationTime = DateTime.Now;
            logMessage.OperationName = "admin";
            _logger.LogError(logMessage.ToString());
        }
    }
}
