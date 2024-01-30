using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ICP.Monitor.Web.Controllers
{
    public class ErrorController : BaseController
    {
        /// <summary>
        /// 显示错误信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Show()
        {
            ViewBag.ErrorMessage = WebAppContext.Exception;
            return View();
        }
    }
}