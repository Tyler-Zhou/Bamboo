//-----------------------------------------------------------------------
// <copyright file="OceanImportModuleInit.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using ICP.FCM.OceanImport.UI.BusinessInfoProvider;
using ICP.Framework.ClientComponents.Service;

namespace ICP.FCM.OceanImport.UI
{
    using ServiceInterface;
    using Common;
    using Framework.CommonLibrary.Client;
    using Framework.CommonLibrary.Common;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.Commands;
    using System;
    using ICP.Framework.ClientComponents;

    /// <summary>
    /// 海运进口模块初始化
    /// </summary>
    public class OceanImportModuleInit : ModuleInit
    {
        /// <summary>
        /// WorkItem
        /// </summary>
        WorkItem _rootWorkItem;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="rootWorkItem">WorkItem</param>
        /// <remarks>初始化WorkItem</remarks>
        public OceanImportModuleInit([ServiceDependency]WorkItem rootWorkItem)
        {
            _rootWorkItem = rootWorkItem;
        }

        /// <summary>
        /// 添加服务
        /// </summary>
        public override void AddServices()
        {
            base.AddServices();
            IBusinessInfoProviderFactory businessInfoProviderFactory = ServiceClient.GetClientService<IBusinessInfoProviderFactory>();
            businessInfoProviderFactory.Register<OIBusinessInfoProvider>
              (OperationType.OceanImport);
            _rootWorkItem.Services.AddNew<ClientOceanImportService, IClientOceanImportService>();
        }


        /// <summary>
        /// 打开订单列表界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.OceanImport_OrderList)]
        public void Open_OceanImport_OrderList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID=LoadingServce.ShowLoadingForm("Loading...");

            OIOrderWorkitem workItem = _rootWorkItem.WorkItems.AddNew<OIOrderWorkitem>();
            workItem.Run();

            LoadingServce.CloseLoadingForm(theradID);
        }
        /// <summary>
        /// 打开业务管理列表界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.OceanImport_BusinessManage)]
        public void Open_OceanImport_BusinessManage(object sender, EventArgs e)
        {
            if (IsDisablePart())
                return;

            int theradID = 0;
            theradID=LoadingServce.ShowLoadingForm("Loading...");

            OIBusinessWorkitem workItem = _rootWorkItem.WorkItems.AddNew<OIBusinessWorkitem>();
            workItem.Run();
            LoadingServce.CloseLoadingForm(theradID);

        }

        /// <summary>
        /// 是否禁用面板
        /// </summary>
        /// <returns></returns>
        public bool IsDisablePart()
        {
            string tip = string.Empty;
            if (LocalData.ShieldNo == 1)
            {
                if (LocalData.UserInfo.DefaultDepartmentID.ToString().ToUpper() ==
                    "EBFE6FDD-800F-436B-B2B3-8DE85039B15E")
                {
                    return false;
                }
                if ((LocalData.UserInfo.DefaultCompanyID.ToString().ToUpper() == "41D7D3FE-183A-41CD-A725-EB6F728541EC" ||
                     LocalData.UserInfo.DefaultCompanyID.ToString().ToUpper() == "2B109BA9-D770-419D-9323-34EE1553FC2E"))
                {
                    tip = LocalData.IsEnglish
                        ? "This page is migrated to [OA/Task Center], any question please contact to IT Dept."
                        : "功能已经移到[办公自动化>任务中心]，如有问题请咨询IT部Tom Lai.";

                    MessageBoxService.ShowInfo(tip);
                    return true;
                }
                tip = LocalData.IsEnglish
                    ? "[Ocean Import & Export] will be disable on 2015/12/15, insteading of the functionality is [OA]/[Task Center], please learn it early."
                    : "[海运进出口查询页面]将在2015/12/15 禁用，此功能已由[办公自动化]/[任务中心]代替，请作好提前学习。";

                MessageBoxService.ShowInfo(tip);
                return false;
            }
            return false;
        }

    }
}
