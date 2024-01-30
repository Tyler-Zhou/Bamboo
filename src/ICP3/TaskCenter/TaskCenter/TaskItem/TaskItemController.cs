using Microsoft.Practices.CompositeUI;

namespace ICP.TaskCenter.UI.TaskItem
{
    /// <summary>
    /// 订单项目Controller
    /// </summary>
   public class TaskItemController:Controller
    {
        /// <summary>
        /// 订单项目工作区WorkItem
        /// </summary>
       TaskItemWorkItem taskItemWorkItem = null;

       /// <summary>
       /// 订单项目工作区WorkItem
       /// </summary>
       [ServiceDependency(Type=typeof(WorkItem))]
       public TaskItemWorkItem TaskItemWorkItem
       {
           set { taskItemWorkItem = value; }
           get { return taskItemWorkItem; }
       }

    }
}
