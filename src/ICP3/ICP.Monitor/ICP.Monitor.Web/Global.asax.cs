#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/4 09:32:08
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System.Web.Mvc;
using System.Web.Routing;
using ICP.Monitor.Web.Utilities;

namespace ICP.Monitor.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
