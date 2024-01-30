#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/16 15:48:16
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System.Web.Mvc;

namespace ICP.Monitor.Web.Utilities
{
    public class CustomActionFilterAttribute : FilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            //执行action前执行这个方法,比如做身份验证
        }

        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
            //执行action后执行这个方法 比如做操作日志
        }
    }
}