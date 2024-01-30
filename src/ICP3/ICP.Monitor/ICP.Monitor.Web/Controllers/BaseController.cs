#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/4 09:43:45
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using ICP.Monitor.Model.Framework;
using System.Web.Mvc;
using System.Web.Routing;
using ICP.Monitor.Web.Utilities;

namespace ICP.Monitor.Web.Controllers
{
    /// <summary>
    /// 基础控制器
    /// </summary>
    [CustomActionFilter]
    [CustomExceptionFilter]
    [AuthActionFilter(IsNeedAuth=true)]
    public class BaseController : Controller
    {
        private WebAppContext _UserRequestContext;

        /// <summary>
        /// 用户请求上下文
        /// </summary>
        public virtual WebAppContext WebAppContext
        {
            get { return _UserRequestContext ?? (_UserRequestContext = new WebAppContext()); }
            set { _UserRequestContext = value; }
        }

        /// <summary>
        /// 重写Controller中的Initialize方法。在此方法中获取用户和其他的信息
        /// </summary>
        /// <param name="requestContext"></param>
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            WebAppContext = (WebAppContext)Session[WebAppConstants.WEBAPPCONTEXT];
            // 判断是否是Get请求
            WebAppContext.IsGet = requestContext.HttpContext.Request.HttpMethod == "GET";
            // 判断是否是Ajax请求
            WebAppContext.IsAjax = requestContext.HttpContext.Request.IsAjaxRequest();

        }

        /// <summary>
        /// 重写异常处理
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            if (_UserRequestContext != null)
            {
                //TODO:异常处理
            }
        }
    }
}