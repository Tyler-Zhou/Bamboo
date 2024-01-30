using System;
using ICP.WF.ServiceInterface.DataObject;

namespace ICP.WF.UI
{
    public interface IWorkListFlowChatService
    {


        /// <summary>
        /// 清空流程图
        /// </summary>
        void Clear();

        /// <summary>
        /// 查看指定的流程
        /// </summary>
        void ViewWorkFolwChart(Guid workID);

        /// <summary>
        /// 是否可以取消
        /// </summary>
        /// <returns></returns>
        bool isCanceled();


        /// <summary>
        /// 在任务双击时触发该事件
        /// </summary>
        event EventHandler<WorkItemEventArgs> WorkItemDoubleClick;
    }
}
