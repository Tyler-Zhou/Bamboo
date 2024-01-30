#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/6 17:11:45
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Routing;
using System.Web.Mvc;
using ICP.Monitor.Interface.ComputerManage;
using ICP.Monitor.Interface.Framework;
using ICP.Monitor.Model;
using ICP.Monitor.Model.ComputerManage;
using ICP.Monitor.Model.Framework;
using ICP.Monitor.Web.Common;

namespace ICP.Monitor.Web.Controllers
{
    /// <summary>
    /// 性能监视控制器
    /// </summary>
    public class PerformanceController : BaseController
    {
        #region Fields & Service
        /// <summary>
        /// 操作日志服务
        /// </summary>
        private IOperationLogService _OperationLogService;
        /// <summary>
        /// 用户服务
        /// </summary>
        private IServerService _IServerService; 
        #endregion

        #region Init
        /// <summary>
        /// 重写Initialize，给BLL设置WebWorkContext
        /// </summary>
        /// <param name="requestContext"></param>
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            _OperationLogService.WebAppContext = WebAppContext;
        }
        /// <summary>
        /// 初始化服务
        /// </summary>
        /// <param name="operationLogService">操作日志服务</param>
        /// <param name="serverService">服务器服务</param>
        public PerformanceController(IOperationLogService operationLogService, IServerService serverService)
        {
            _OperationLogService = operationLogService;
            _IServerService = serverService;
        }
        #endregion

        #region Server

        public ActionResult ServerInfo()
        {
            EResultInfo resultInfo = _IServerService.GetServerInfo();
            if (resultInfo.Exception != null)
            {
                return Content("<script >alert('"+resultInfo.Exception.Message+"');</script >", "text/html");
            }
            EServerInfo serverInfo= resultInfo.Data;
            return View(serverInfo);
        }
        #endregion

        #region Operation Log
        // GET: Performance
        public ActionResult OperationLog()
        {
            return View();
        }

        /// <summary>
        /// 获取操作日志
        /// </summary>
        /// <returns></returns>
        public JsonResult GetOperationLogs()
        {
            OperationLogSearchParam olSearchParam = new OperationLogSearchParam
            {
                BeginDate = DateTime.Now.AddHours(-8),
                EndDate = DateTime.Now
            };
            EResultInfo resultInfo = _OperationLogService.GetOperationLogs(olSearchParam);
            List<EOperationLogInfo> operationLogList = resultInfo.Data;

            return Json(operationLogList, JsonRequestBehavior.AllowGet);
        } 
        #endregion

        #region Windows Service
        /// <summary>
        /// ICP Services
        /// </summary>
        /// <returns></returns>
        public ActionResult ICPServices()
        {
            EResultInfo resultInfo = _IServerService.GetServices();
            if (resultInfo.Exception != null)
            {
                return Content("<script >alert('" + resultInfo.Exception.Message + "');</script >", "text/html");
            }
            List<EWindowsServiceInfo> services = resultInfo.Data as List<EWindowsServiceInfo>;
            if (services != null) return View(services.ToArray());
            return View();
        }

        [HttpPost]
        [MultiButton(Name = "Start")]
        public ActionResult StartService(FormCollection form)
        {
            var winnars = from x in form.AllKeys where form[x] != "false" select x;//找到你在视图中选定的要删除的数据
            foreach (var serviceName in winnars)
            {
                if (serviceName != "selectAll")
                {
                    _IServerService.StartService(serviceName);
                }
            }
            return RedirectToAction("ICPServices");
        }

        [HttpPost]
        [MultiButton(Name = "Stop")]
        public ActionResult StopService(FormCollection form)
        {
            var winnars = from x in form.AllKeys where form[x] != "false" select x;//找到你在视图中选定的要删除的数据
            foreach (var serviceName in winnars)
            {
                if (serviceName != "selectAll")
                {
                    _IServerService.StopService(serviceName);
                }
            }
            return RedirectToAction("ICPServices");
        }

        [HttpPost]
        [MultiButton(Name = "ReStart")]
        public ActionResult ReStartService(FormCollection form)
        {
            var winnars = from x in form.AllKeys where form[x] != "false" select x;//找到你在视图中选定的要删除的数据
            foreach (var serviceName in winnars)
            {
                if (serviceName != "selectAll")
                {
                    _IServerService.ReStartService(serviceName);
                }
            }
            return RedirectToAction("ICPServices");
        } 
        #endregion

        #region Test

        public async Task<ActionResult> IndexAsync()
        {
            return View();
        }
        #endregion
    }
}