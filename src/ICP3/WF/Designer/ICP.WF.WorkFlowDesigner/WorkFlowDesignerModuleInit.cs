using System;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.WF.WorkFlowDesigner
{
    /// <summary>
    /// 设计器入口
    /// </summary>
    public class WorkFlowDesignerModuleInit : ModuleInit
    {

        #region 常量定义

        #endregion


        #region 服务

        private WorkItem _rootWorkItem;
        public WorkFlowDesignerModuleInit(
            [ServiceDependency]WorkItem rootWorkItem)
        {
            _rootWorkItem = rootWorkItem;
        }
        #endregion


        #region 菜单事件

        /// <summary>
        /// 打开流程设计界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_OpenFlowDesigner)]
        public void OpenWorkFlowDesigner(object sender, EventArgs e)
        {
            WorkFlowDesignerWorkItem workItem = _rootWorkItem.WorkItems.AddNew<WorkFlowDesignerWorkItem>();

            IWorkspace mainWorkspace = _rootWorkItem.Workspaces[ClientConstants.MainWorkspace];
            string titel = LocalData.IsEnglish ? "Workflow Designer" : "流程设计器";
            workItem.Show(mainWorkspace, titel);
        }


        #endregion

    }
}
