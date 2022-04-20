using Bamboo.Server.Interface;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Bamboo.Server.Filter
{
    /// <summary>
    /// 异步版本自定义全局异常过滤器
    /// </summary>
    public class CustomerGlobalExceptionFilterAsync : IAsyncExceptionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ILogService _LogService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logService"></param>
        public CustomerGlobalExceptionFilterAsync(ILogService logService)
        {
            _LogService = logService;
        }

        /// <summary>
        /// 重新OnExceptionAsync方法
        /// </summary>
        /// <param name="context">异常信息</param>
        /// <returns></returns>
        public Task OnExceptionAsync(ExceptionContext context)
        {
            // 如果异常没有被处理，则进行处理
            if (context.ExceptionHandled == false)
            {
                // 记录错误信息
                _LogService.LogError(context.Exception);
                // 设置为true，表示异常已经被处理了，其它捕获异常的地方就不会再处理了
                context.ExceptionHandled = true;
            }
            return Task.CompletedTask;
        }
    }
}
