using System;

namespace Bamboo.Server.Interface
{
    /// <summary>
    /// 日志服务
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// 错误日志记录
        /// </summary>
        /// <param name="ex"></param>
        void LogError(Exception ex);
    }
}
