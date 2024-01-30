using ICP.Framework.CommonLibrary.Client;
using ICP.TaskCenter.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using System;

namespace ICP.TaskCenter.UI.MainWork
{
    /// <summary>
    /// 订单工作区控制器
    /// </summary>
    public class MainWorkController : Controller, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem RootWorkItem { get; set; }
        /// <summary>
        /// 订单工作区的WorkItem项
        /// </summary>
        private MainWorkWorkItem mainWorkWorkItem = null;

        /// <summary>
        /// 订单工作区的WorkItem项
        /// </summary>
        public MainWorkWorkItem MainWorkWorkItem
        {

            get
            {
                if (mainWorkWorkItem != null)
                {
                    return mainWorkWorkItem;
                }
                else
                {
                    mainWorkWorkItem = RootWorkItem.WorkItems.Get<MainWorkWorkItem>(TaskCenterCommandConstants.MainWorkWorkItemName);
                    if (mainWorkWorkItem == null)
                    {
                        mainWorkWorkItem = RootWorkItem.WorkItems.AddNew<MainWorkWorkItem>(TaskCenterCommandConstants.MainWorkWorkItemName);
                    }
                    return mainWorkWorkItem;
                }
            }
        }
        /// <summary>
        /// 操作视图服务
        /// </summary>
        public IOperationViewService OperationViewService
        {
            get
            {
                return ServiceClient.GetService<IOperationViewService>();
            }
        }

        /// <summary>
        /// 根据操作视图来时显示订单项目
        /// </summary>
        /// <param name="nodeInfo">当前操作视图信息</param>
        public void ShowTaskItems(NodeInfo nodeInfo)
        {
            MainWorkWorkItem.ShowTaskItems(nodeInfo);
        }

        #region IDisposable 成员

        public void Dispose()
        {

            if (RootWorkItem != null)
            {
                RootWorkItem.Items.Remove(this);
                RootWorkItem = null;
            }
            mainWorkWorkItem = null;
        }

        #endregion
    }
}
