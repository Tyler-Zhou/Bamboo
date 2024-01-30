


using ICP.Framework.CommonLibrary;

namespace ICP.FCM.Common.UI
{
    using ServiceInterface;
    using ServiceInterface.Common;
    using AgentRequest;
    using Finder;
    using Service;
    using Framework.ClientComponents.Service;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.Commands;
    using System;
    using Framework.CommonLibrary.Client;

    /// <summary>
    /// 公共模块初始化
    /// </summary>
    public class CommonModuleInit : ModuleInit
    {
        WorkItem _rootWorkItem;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootWorkItem"></param>
        public CommonModuleInit([ServiceDependency]WorkItem rootWorkItem
            )
        {
            _rootWorkItem = rootWorkItem;
        }

        /// <summary>
        /// 注册服务
        /// </summary>
        public override void AddServices()
        {
            base.AddServices();

            _rootWorkItem.Services.Add<IFCMCommonClientService>(new FCMCommonClientService());
            _rootWorkItem.Services.Add<IClientContactNotifyService>(new ClientContactNotifyService());
            IDataFinderFactory datafinderFactory = ServiceClient.GetClientService<IDataFinderFactory>();
            datafinderFactory.Register<BusinessFinder>(FCMFinderConstants.BusinessFinder);
            datafinderFactory.Register<BusinessFinderForOEAE>(FCMFinderConstants.BusinessFinderForOEAE);
            datafinderFactory.Register<BusinessFinderForOEAEOB>(FCMFinderConstants.BusinessFinderForOEAEOB);
            datafinderFactory.Register<BusinessFinderForOI>(FCMFinderConstants.BusinessFinderForOI);

        }


        /// <summary>
        /// 申请代理列表命令名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.OceanExport_AgentRequestList)]
        public void Open_OceanExport_AgentRequestList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID=LoadingServce.ShowLoadingForm("Loading...");
            AgentRequestWorkitem agentRequestWorkitem = _rootWorkItem.WorkItems.AddNew<AgentRequestWorkitem>();
            agentRequestWorkitem.Run();
            LoadingServce.CloseLoadingForm(theradID);
        }


        /// <summary>
        /// 方法测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(FrameworkCommandConstants.COMMAND_SYNCLOCALDATA)]
        public void SyncLocalData(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.XtraMessageBox.Show("test");
        }
    }
}
