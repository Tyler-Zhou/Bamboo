#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/4 09:50:45
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
using System.Web.Routing;
using ICP.Monitor.Interface.SystemManage;

namespace ICP.Monitor.Web.Controllers
{
    /// <summary>
    /// Home 控制器
    /// </summary>
    public class HomeController : BaseController
    {
        /// <summary>
        /// 用户服务
        /// </summary>
        private IUserService _UserService;

        #region Init
        /// <summary>
        /// 重写Initialize，给BLL设置WebWorkContext
        /// </summary>
        /// <param name="requestContext"></param>
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            _UserService.WebAppContext = WebAppContext;
        }

        public HomeController(IUserService userService)
        {
            _UserService = userService;
        }
        #endregion

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.ThemeName = GetThemeName();
            return View();
        }

        public ActionResult SystemInfo()
        {
            return View();
        }

        // GET: Home
        public ActionResult Test()
        {
            if (WebAppContext.IsOnLine)
                return View();
            return View("Login");
        }

        private string GetThemeName()
        {
            HttpCookie themeCookie = Request.Cookies["theme_name"];
            string themeName = "default";
            if (themeCookie != null)
            {
                themeName = themeCookie.Value;
            }
            else
            {
                HttpCookie cookie = new HttpCookie("theme_name") { Value = "default", Domain = "cityocean.com" };
                Response.Cookies.Add(cookie);
            }
            return themeName;
        }
    }
}