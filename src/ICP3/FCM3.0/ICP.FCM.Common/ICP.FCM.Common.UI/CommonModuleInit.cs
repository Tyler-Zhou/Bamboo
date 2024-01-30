

namespace ICP.FCM.Common.UI
{
    
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.Commands;
    using System;
    using ICP.Framework.CommonLibrary.Client;
    using ICP.FCM.Common.ServiceInterface;

    /// <summary>
    /// 公共模块初始化
    /// </summary>
    public class CommonModuleInit :ModuleInit
    {
        //Microsoft.Practices.CompositeUI.WorkItem _rootWorkItem;

        ///// <summary>
        ///// 模块初始化
        ///// </summary>
        ///// <param name="rootWorkItem"></param>
        ///// <param name="uiService"></param>
        //public CommonModuleInit([ServiceDependency]WorkItem rootWorkItem)
        //{
        //    _rootWorkItem = rootWorkItem;
        //}
        Microsoft.Practices.CompositeUI.WorkItem _rootWorkItem;
        IDataFinderFactory _datafinderFactory;
        IBusinessInfoProviderFactory _businessInfoProviderFactory;

        public CommonModuleInit([ServiceDependency]WorkItem rootWorkItem
            , [ServiceDependency]IDataFinderFactory datafinderFactory
            , [ServiceDependency]IBusinessInfoProviderFactory businessInfoProviderFactory
            )
        {
            _rootWorkItem = rootWorkItem;
            _datafinderFactory = datafinderFactory;
            _businessInfoProviderFactory = businessInfoProviderFactory;
        }

        /// <summary>
        /// 注册服务
        /// </summary>
        public override void AddServices()
        {
            base.AddServices();
          
            _rootWorkItem.Services.Add<ICP.FCM.Common.ServiceInterface.IFCMCommonClientService>(new ICP.FCM.Common.UI.Service.FCMCommonClientService());

            //_rootWorkItem.Services.AddNew<ICP.FCM.Common.UI.Service.OpenFCMCommonClientService, ICP.FCM.Common.ServiceInterface.IOpenFCMCommonClientService>();

            _datafinderFactory.Register<ICP.FCM.Common.UI.Finder.BusinessFinder>
                (ICP.FCM.Common.ServiceInterface.Common.FCMFinderConstants.BusinessFinder);

            _datafinderFactory.Register<ICP.FCM.Common.UI.Finder.BusinessFinderForOEAEOB>
               (ICP.FCM.Common.ServiceInterface.Common.FCMFinderConstants.BusinessFinderForOEAEOB);

            _datafinderFactory.Register<ICP.FCM.Common.UI.Finder.BusinessFinderForOI>
               (ICP.FCM.Common.ServiceInterface.Common.FCMFinderConstants.BusinessFinderForOI);

            //_businessInfoProviderFactory.Register<ICP.FCM.OceanExport.UI.BusinessInfoProvider.OEBusinessInfoProvider>
            //    (OperationType.OceanExport);
           
        }     


        //申请代理列表命令名
        [CommandHandler(CommandConstants.OceanExport_AgentRequestList)]
        public void Open_OceanExport_AgentRequestList(object sender, EventArgs e)
        {
            AgentRequest.AgentRequestWorkitem agentRequestWorkitem = _rootWorkItem.WorkItems.AddNew<AgentRequest.AgentRequestWorkitem>();
            agentRequestWorkitem.Run();
        }
    }
}
