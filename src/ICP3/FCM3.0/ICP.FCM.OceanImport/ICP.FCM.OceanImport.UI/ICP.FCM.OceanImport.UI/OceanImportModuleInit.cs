//-----------------------------------------------------------------------
// <copyright file="OceanImportModuleInit.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.OceanImport.UI
{
    using System;
    using ICP.FCM.OceanImport.UI.Common;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.Commands;
    using ICP.Framework.CommonLibrary.Client;
    using Microsoft.Practices.CompositeUI.SmartParts;

    /// <summary>
    /// 海运进口模块初始化

    /// </summary>
    public class OceanImportModuleInit : ModuleInit
    {

        Microsoft.Practices.CompositeUI.WorkItem _rootWorkItem;
        IDataFinderFactory _datafinderFactory;
        IBusinessInfoProviderFactory _businessInfoProviderFactory;

        public OceanImportModuleInit([ServiceDependency]WorkItem rootWorkItem
            , [ServiceDependency]IDataFinderFactory datafinderFactory
             , [ServiceDependency]IBusinessInfoProviderFactory businessInfoProviderFactory
            )
        {
            _rootWorkItem = rootWorkItem;
            _datafinderFactory = datafinderFactory;
            _businessInfoProviderFactory = businessInfoProviderFactory;
        }


        public override void AddServices()
        {
            base.AddServices();
            _businessInfoProviderFactory.Register<ICP.FCM.OceanImport.UI.BusinessInfoProvider.OIBusinessInfoProvider>
              (OperationType.OceanImport);
        }


        /// <summary>
        /// 打开订单列表界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.OceanImport_OrderList)]
        public void Open_OceanImport_OrderList(object sender, EventArgs e)
        {
            ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm("Loading...");

            OIOrderWorkitem workItem = this._rootWorkItem.WorkItems.AddNew<OIOrderWorkitem>();
            workItem.Run();

            ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm();
        }
        /// <summary>
        /// 打开业务管理列表界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.OceanImport_BusinessManage)]
        public void Open_OceanImport_BusinessManage(object sender, EventArgs e)
        {

            ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm("Loading...");

            OIBusinessWorkitem workItem = this._rootWorkItem.WorkItems.AddNew<OIBusinessWorkitem>();
            workItem.Run();

            ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm();


        }

    }
}
