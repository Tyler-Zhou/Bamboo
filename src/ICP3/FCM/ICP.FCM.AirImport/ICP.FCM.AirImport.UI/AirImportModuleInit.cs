//-----------------------------------------------------------------------
// <copyright file="AirImportModuleInit.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------



namespace ICP.FCM.AirImport.UI
{
    using System;
    using ICP.FCM.AirImport.UI.Common;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.Commands;
    using ICP.Framework.CommonLibrary.Client;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.FCM.AirImport.ServiceInterface;
    /// <summary>
    /// 空运进口模块初始化
    /// </summary>
    public class AirImportModuleInit : ModuleInit
    {
        
        Microsoft.Practices.CompositeUI.WorkItem _rootWorkItem;
       

        public AirImportModuleInit([ServiceDependency]WorkItem rootWorkItem)
        {
            _rootWorkItem = rootWorkItem;
        }


        public override void AddServices()
        {
            base.AddServices();
            IBusinessInfoProviderFactory businessInfoProviderFactory = ServiceClient.GetClientService<IBusinessInfoProviderFactory>();
            businessInfoProviderFactory.Register<ICP.FCM.AirImport.UI.BusinessInfoProvider.AIBusinessInfoProvider>
              (OperationType.AirImport );

            _rootWorkItem.Services.AddNew<ClientAirImportService, IClientAirImportService>();
        }


        /// <summary>
        /// 打开订单列表界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.AirImport_OrderList)]
        public void Open_AirImport_OrderList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID=ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm("Loading...");
           
            OIOrderWorkitem workItem = this._rootWorkItem.WorkItems.AddNew<OIOrderWorkitem>();
            workItem.Run();
            ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
        }
        /// <summary>
        /// 打开业务管理列表界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.AirImport_BusinessManage)]
        public void Open_AirImport_BusinessManage(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID=ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm("Loading...");
            OIBusinessWorkitem workItem = this._rootWorkItem.WorkItems.AddNew<OIBusinessWorkitem>();
            workItem.Run();
            ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
        }

    }
}
