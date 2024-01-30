#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/11 15:33:45
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
using System.Web.Security;
using ICP.Monitor.Interface.SystemManage;
using ICP.Monitor.Model.Framework;
using ICP.Monitor.Web.Models;
using ICP.Monitor.Web.Utilities;

namespace ICP.Monitor.Web.Controllers
{
    /// <summary>
    /// 帐号控制器
    /// </summary>
    public class AccountController : BaseController
    {
        #region Fields
        /// <summary>
        /// 用户服务
        /// </summary>
        private IUserService _UserService; 
        #endregion

        #region Init

        public AccountController(IUserService userService)
        {
            _UserService = userService;
        }
        #endregion

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [AuthActionFilter(IsNeedAuth = false)]
        public ActionResult Login()
        {
            ViewBag.UserName = "TaylorZhou";
            return View();
        }

        /// <summary>
        /// 处理登录的信息
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        [AuthActionFilter(IsNeedAuth = false)]
        public JsonResult CheckUserLogin(AuthDataInfo loginInfo)
        {
            try
            {
                ELoginInfo userInfo = _UserService.AuthUser(loginInfo.UserName, loginInfo.UserPassword, "", DateTime.Now);

                if (!string.IsNullOrEmpty(userInfo.UserName))
                {
                    string UserData = SerializeHelper.Instance.JsonSerialize(userInfo);//序列化用户实体 
                    //保存身份信息，参数说明可以看提示 
                    FormsAuthenticationTicket Ticket = new FormsAuthenticationTicket(1, userInfo.UserName, DateTime.Now, DateTime.Now.AddHours(12), false, UserData);
                    HttpCookie Cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(Ticket));//加密身份信息，保存至Cookie 
                    Response.Cookies.Add(Cookie);
                    WebAppContext webAppContext = new WebAppContext
                    {
                        UserID = userInfo.UserID,
                        UserName = userInfo.UserName,
                        UserEmail = userInfo.EmailAddress,
                        SessionID = "" + Guid.NewGuid()
                    };
                    HttpContext.Session[WebAppConstants.WEBAPPCONTEXT] = webAppContext;
                    return Json(new { result = "success", content = "" });
                }
                else
                {
                    return Json(new { result = "error", content = "用户名密码错误，请您检查" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { result = "error", content = ex.Message+"\r\n出现错误，请稍后再试，带来不便，敬请谅解!" });
            }
        }
    }
}