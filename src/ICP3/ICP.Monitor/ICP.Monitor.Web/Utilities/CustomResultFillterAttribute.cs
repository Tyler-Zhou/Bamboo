#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/17 18:02:08
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
    public class CustomResultFillterAttribute : FilterAttribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            //执行完action后跳转后执行
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            //执行完action后跳转前执行
        }
    }
}