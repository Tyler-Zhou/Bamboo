using ICP.Framework.CommonLibrary.Client;
using ICP.TaskCenter.UI.MainWork;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace ICP.TaskCenter.UI
{
    /// <summary>
    /// 任务中心WorkItem
    /// </summary>
    public partial class TaskCenterWorkitem : WorkItem
    {
        #region Service
        /// <summary>
        /// 任务中心总面板定位SmartPart
        /// </summary>
        MainWorkSpace mainWorkSpace = null;

        /// <summary>
        /// 重写Dispose，处理类变量对象
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (mainWorkSpace != null)
                {
                    mainWorkSpace.Dispose();
                    mainWorkSpace = null;

                }

            }
            base.Dispose(disposing);
        }
        #endregion
        /// <summary>
        /// 重载OnRunStarted
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            IWorkspace workspace = RootWorkItem.Workspaces[ClientConstants.MainWorkspace];

            SmartPartInfo smartPartInfo = new SmartPartInfo();
            smartPartInfo.Title = LocalData.IsEnglish ? "Task Center" : "任务中心";


            mainWorkSpace = RootWorkItem.SmartParts.Get<MainWorkSpace>(TaskCenterCommandConstants.MainWorkSpacePartName);
            if (mainWorkSpace == null)
            {
                mainWorkSpace = RootWorkItem.SmartParts.AddNew<MainWorkSpace>(TaskCenterCommandConstants.MainWorkSpacePartName);
                workspace.Show(mainWorkSpace, smartPartInfo);
                ShowMainWorkSpace();
            }
            else
            {
                workspace.Activate(mainWorkSpace);
            }
        }

        /// <summary>
        ///  显示主页面
        /// </summary>
        public void ShowMainWorkSpace()
        {
            MainWorkWorkItem mainWorkItem = RootWorkItem.WorkItems.Get<MainWorkWorkItem>(TaskCenterCommandConstants.MainWorkWorkItemName);
            if (mainWorkItem == null)
            {
                mainWorkItem = RootWorkItem.WorkItems.AddNew<MainWorkWorkItem>(TaskCenterCommandConstants.MainWorkWorkItemName);
            }

            mainWorkItem.ShowViewList(mainWorkSpace.ViewListWorkSpace, mainWorkSpace.TaskItemsWorkSpace);
        }
    }


    /// <summary>
    /// 事件命令常量
    /// </summary>
    public partial class TaskCenterCommandConstants
    {
        /// <summary>
        /// 任务中心
        /// </summary>
        public const string TaskCenter = "Command_OpenTaskCenter";
        /// <summary>
        /// 设置TaskItemsWorkSpace控件只读
        /// </summary>
        public const string SetReadOnly = "Command_SetReadOnly";
        /// <summary>
        /// 取消TaskItemsWorkSpace控件只读
        /// </summary>
        public const string CancelReadOnly = "Command_CancelReadOnly";
        /// <summary>
        /// 页面置灰
        /// </summary>
        public const string CommandDisableTaskCenter = "Command_DisableTaskCenter";
        /// <summary>
        /// 取消页面置灰
        /// </summary>
        public const string CommandEnableTaskCenter = "Command_EnableTaskCenter";
        /// <summary>
        /// 任务中心WorkItem名
        /// </summary>
        public const string TaskCenterWorkitemName = "TaskCenterWorkitem";
        /// <summary>
        /// 显示视图面板
        /// </summary>
        public const string ViewListSmartPartName = "ViewListSmartPart";
        /// <summary>
        /// 任务中心主界面WorkItem名
        /// </summary>
        public const string MainWorkWorkItemName = "MainWorkWorkItem";
        /// <summary>
        /// 任务中心主界面面板名
        /// </summary>
        public const string MainWorkSpacePartName = "MainWorkSpacePart";
    }

    /// <summary>
    /// 工作区域常量
    /// </summary>
    public partial class TaskCenterWorkSapceConsts
    {
        /// <summary>
        /// 视图列表WorkSpace名称
        /// </summary>
        public const string ViewList = "ViewListWorkSpace";
    }
}
