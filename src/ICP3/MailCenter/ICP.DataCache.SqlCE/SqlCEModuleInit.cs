using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;

namespace ICP.DataOperation.SqlCE
{
    /// <summary>
    /// Sql Ce模块加载
    /// </summary>
    public class SqlCEModuleInit : ModuleInit
    {
        /// <summary>
        /// 根WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem RootWorkItem { get; set; }
        /// <summary>
        /// 计时器操作服务
        /// </summary>
        public TimerOperationService TimerOperationService
        {
            get { return ServiceClient.GetClientService<TimerOperationService>(); }
        }

        /// <summary>
        /// 注入客户端服务
        /// </summary>
        public override void AddServices()
        {
            if (LocalData.ApplicationType == ApplicationType.ICP)
            {
                SynchronizeData();
            }
        }

        private void SynchronizeData()
        {
            IMainForm mainForm = ServiceClient.GetClientService<IMainForm>();
            mainForm.ApplicationExit += SynchronizationHelper.Current.OnApplicationExit;
            SynchronizationHelper.Current.Synchronize();
            mainForm.ApplicationExit += TimerOperationService.Current.OnApplicationExit;
            TimerOperationService.Current.Start();
        }
    }
}
