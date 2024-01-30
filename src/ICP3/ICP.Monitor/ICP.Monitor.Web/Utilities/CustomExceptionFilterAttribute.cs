#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/18 18:03:42
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Web;
using System.Web.Mvc;
using ICP.Monitor.Model.Framework;

namespace ICP.Monitor.Web.Utilities
{
    public class CustomExceptionFilterAttribute : FilterAttribute, IExceptionFilter
    {
        /// <summary> 
        /// 异常 
        /// </summary> 
        /// <param name="filterContext"></param> 
        public void OnException(ExceptionContext filterContext)
        {
            //获取异常信息，入库保存 
            string Url = HttpContext.Current.Request.RawUrl;//错误发生地址
            WebAppContext webAppContext=(WebAppContext)filterContext.HttpContext.Session[WebAppConstants.WEBAPPCONTEXT];
            webAppContext.Exception = string.Format("Url:{0} Error:{1}", Url,filterContext.Exception.Message);
            filterContext.ExceptionHandled = true;
            filterContext.Result = new RedirectResult("../Error/Show/");//跳转至错误提示页面 
        } 
    }
}