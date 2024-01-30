#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/17 17:33:26
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
using System.Web.Mvc;
using ICP.Monitor.BLL.ComputerManage;
using ICP.Monitor.BLL.Framework;
using ICP.Monitor.BLL.SystemManage;
using ICP.Monitor.Interface.ComputerManage;
using ICP.Monitor.Interface.Framework;
using ICP.Monitor.Interface.SystemManage;
using Ninject;

namespace ICP.Monitor.Web.App_Start
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IOperationLogService>().To<OperationLogService>();
            kernel.Bind<IServerService>().To<ServerService>();
            kernel.Bind<IUserService>().To<UserService>();
        }
    }
}