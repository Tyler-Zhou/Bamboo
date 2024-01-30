#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/19 9:18:18
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ICP.Monitor.Web.Utilities
{
    /// <summary>
    /// 验证是否登录特性
    /// </summary>
    public class AuthActionFilterAttribute : FilterAttribute, IActionFilter
    {
        /// <summary>
        /// 是否需要验证
        /// </summary>
        public bool IsNeedAuth { get; set; }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (IsNeedAuth)
            {
                //如果存在身份信息 
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    ContentResult Content = new ContentResult
                    {
                        Content =
                            string.Format(
                                "<script type='text/javascript'>alert('请先登录！');window.location.href='{0}';</script>",
                                FormsAuthentication.LoginUrl)
                    };
                    filterContext.Result = Content;
                }
                //else
                //{
                //    string[] Role = CheckLogin.Instance.GetUser().Roles.Split(',');//获取所有角色 
                //    if (!Role.Contains(Code))//验证权限 
                //    {
                //        //验证不通过 
                //        ContentResult Content = new ContentResult
                //        {
                //            Content = "<script type='text/javascript'>alert('权限验证不通过！');history.go(-1);</script>"
                //        };
                //        filterContext.Result = Content;
                //    }
                //}
            }
        }

        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (IsNeedAuth)
            {
                
            }
        }
    }
}