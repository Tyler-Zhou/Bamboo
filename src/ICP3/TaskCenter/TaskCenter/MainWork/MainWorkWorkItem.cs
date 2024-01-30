using DevExpress.XtraEditors;
using ICP.TaskCenter.ServiceInterface;
using ICP.TaskCenter.UI.TaskItem;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace ICP.TaskCenter.UI.MainWork
{
    /// <summary>
    /// 任务中心主WorkItem
    /// </summary>
    public class MainWorkWorkItem : WorkItem
    {
        /// <summary>
        /// 异步锁对象
        /// </summary>
        public static object synObj = new object();

        /// <summary>
        /// 任务中心功能树视图列表用户控件
        /// </summary>
        ViewListSmartPart viewListSmartPart = null;
        /// <summary>
        /// 订单项目工作区SmartPart
        /// </summary>
        BaseTaskItemPart baseTaskItemPart = null;

        /// <summary>
        /// 分隔符面板
        /// </summary>
        public SplitGroupPanel DeckTaskItem;

        /// <summary>
        /// 
        /// </summary>
        private TaskItemWorkItem taskItemWorkItem = null;

        /// <summary>
        /// 操作视图工厂
        /// </summary>
        TaskItemFactory factory = new TaskItemFactory();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                viewListSmartPart = null;
                baseTaskItemPart = null;
                DeckTaskItem = null;
                taskItemWorkItem = null;
                factory = null;
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// 显示任务中心功能树视图列表ViewListSmartPart用户控件
        /// </summary>
        /// <param name="deckWorkspace">显示的地方IWorkspace</param>
        public void ShowViewList(IWorkspace deckWorkspace, SplitGroupPanel deckTaskItem)
        {

            DeckTaskItem = deckTaskItem;
            viewListSmartPart = RootWorkItem.SmartParts.AddNew<ViewListSmartPart>("ViewListSmartPart");
            deckWorkspace.Show(viewListSmartPart);
        }


        /// <summary>
        /// 显示订单项目工作区SmartPart用户控件
        /// </summary>
        /// <param name="nodeInfo">节点实体</param>
        public void ShowTaskItems(NodeInfo nodeInfo)
        {
            string key = "WorkItem" + nodeInfo.Id.ToString();

            TaskItemWorkItem workItem = Items.Get<TaskItemWorkItem>(key);

            if (workItem == null)
            {
                workItem = WorkItems.AddNew<TaskItemWorkItem>(key);
                workItem.ID = key;
                workItem.State[ConstMember.CurrentNodeInfo] = nodeInfo;
            }
            // 处理如果用户直接在原有的查询基础上继续进行查询需要替换查询条件
            else if (nodeInfo.Caption == "Search Result" || nodeInfo.Caption == "查询结果")
            {
                workItem.State[ConstMember.CurrentNodeInfo] = nodeInfo;
            }
            workItem.Show(DeckTaskItem);
        }
    }
}
