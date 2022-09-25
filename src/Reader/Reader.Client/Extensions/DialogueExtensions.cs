using Prism.Events;
using Reader.Client.Events;
using Reader.Client.Models;
using System;

namespace Reader.Client.Extensions
{
    /// <summary>
    /// 对话扩展服务
    /// </summary>
    public static class DialogueExtensions
    {
        #region Loading
        /// <summary>
        /// 注册等待消息
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="action"></param>
        public static void ResgiterLoading(this IEventAggregator aggregator, Action<LoadingModel> action)
        {
            aggregator.GetEvent<LoadingEvent>().Subscribe(action);
        }

        /// <summary>
        /// 显示等待窗体
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="model"></param>
        public static void ShowLoading(this IEventAggregator aggregator, LoadingModel model)
        {
            aggregator.GetEvent<LoadingEvent>().Publish(model);
        }
        #endregion

        #region Message
        /// <summary>
        /// 注册提示消息 
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="action"></param>
        /// <param name="filterName"></param>
        public static void ResgiterMessage(this IEventAggregator aggregator,
            Action<TipsModel> action, string filterName = "Main")
        {
            aggregator.GetEvent<TipsEvent>().Subscribe(action,
                ThreadOption.PublisherThread, true, (m) =>
                {
                    return m.Filter.Equals(filterName);
                });
        }

        /// <summary>
        /// 发送提示消息
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="tipsInfo"></param>
        /// <param name="filterName"></param>
        public static void ShowMessage(this IEventAggregator aggregator, TipsInfo tipsInfo, string filterName = "Main")
        {
            aggregator.GetEvent<TipsEvent>().Publish(new TipsModel()
            {
                Filter = filterName,
                Tips = tipsInfo,
            });
        }
        #endregion

        #region Task
        /// <summary>
        /// 注册任务订阅
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="action"></param>
        public static void ResgiterTask(this IEventAggregator aggregator,Action<TaskModel> action)
        {
            aggregator.GetEvent<TaskEvent>().Subscribe(action);
        }

        /// <summary>
        /// 发送任务
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="taskModel">任务模型</param>
        public static void SendTask(this IEventAggregator aggregator, TaskModel taskModel)
        {
            aggregator.GetEvent<TaskEvent>().Publish(taskModel);
        }
        #endregion
    }
}
