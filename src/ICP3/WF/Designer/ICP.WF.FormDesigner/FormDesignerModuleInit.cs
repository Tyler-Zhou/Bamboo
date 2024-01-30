//-----------------------------------------------------------------------
// <copyright file="FormDesignerModuleInit.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.FormDesigner
{
    using System;
    using ICP.Framework.CommonLibrary.Client;
    using ICP.WF.FormDesigner.Common;
    using ICP.WF.ServiceInterface;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.Commands;
    using Microsoft.Practices.CompositeUI.SmartParts;

    /// <summary>
    /// 表单设计器入口
    /// </summary>
    public class FormDesignerModuleInit : ModuleInit
    {

        #region 服务
        WorkItem _rootWorkItem;
        public FormDesignerModuleInit([ServiceDependency]WorkItem rootWorkItem)
        {
            _rootWorkItem = rootWorkItem;
            _rootWorkItem.Services.Add<IFormDesignClientService>(new WorkFlowClientService());

        }

        #endregion


        #region 菜单事件

        /// <summary>
        /// 打开流表单设计器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_OpenFormDesigner)]
        public void OpenFormDesigner(object sender, EventArgs e)
        {
            ShellWorkItem workItem = _rootWorkItem.WorkItems.AddNew<ShellWorkItem>();

            IWorkspace mainWorkspace = _rootWorkItem.Workspaces[ClientConstants.MainWorkspace];
            workItem.Show(
                   mainWorkspace,
                   LocalData.IsEnglish?"Form Designer":"表单设计器");
        }

        #endregion

    }
}
