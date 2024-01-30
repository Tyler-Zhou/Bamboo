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

    /// <summary>
    /// 空运进口模块初始化
    /// </summary>
    public class AirImportModuleInit : ModuleInit
    {
        
        Microsoft.Practices.CompositeUI.WorkItem _rootWorkItem;
        IBusinessInfoProviderFactory _businessInfoProviderFactory;

        public AirImportModuleInit([ServiceDependency]WorkItem rootWorkItem
             , [ServiceDependency]IBusinessInfoProviderFactory businessInfoProviderFactory
            )
        {
            _rootWorkItem = rootWorkItem;
            _businessInfoProviderFactory = businessInfoProviderFactory;
        }


        public override void AddServices()
        {
            base.AddServices();
            _businessInfoProviderFactory.Register<ICP.FCM.AirImport.UI.BusinessInfoProvider.AIBusinessInfoProvider>
              (OperationType.AirImport );
        }


        /// <summary>
        /// 打开订单列表界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.AirImport_OrderList)]
        public void Open_AirImport_OrderList(object sender, EventArgs e)
        {
            OIOrderWorkitem workItem = this._rootWorkItem.WorkItems.AddNew<OIOrderWorkitem>();
            workItem.Run();
        }
        /// <summary>
        /// 打开业务管理列表界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.AirImport_BusinessManage)]
        public void Open_AirImport_BusinessManage(object sender, EventArgs e)
        {
            OIBusinessWorkitem workItem = this._rootWorkItem.WorkItems.AddNew<OIBusinessWorkitem>();
            workItem.Run();
        }

    }
}
