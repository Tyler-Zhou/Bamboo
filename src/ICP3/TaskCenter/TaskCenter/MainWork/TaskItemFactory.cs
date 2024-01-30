using ICP.Framework.CommonLibrary.Common;
using ICP.TaskCenter.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;

namespace ICP.TaskCenter.UI
{
    /// <summary>
    /// 操作视图工厂，主要通过操作视图的类型来加载那个类型视图
    /// </summary>
    public class TaskItemFactory : IDisposable
    {
        /// <summary>
        /// 操作视图基类工作区字典集合
        /// </summary>
        private Dictionary<string, BaseTaskItemPart> dict = new Dictionary<string, BaseTaskItemPart>();

        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem WorkItem { get; set; }

        /// <summary>
        /// 获取节点对应操作视图工作区转换成BaseTaskItemPart(操作视图基类工作区)
        /// </summary>
        /// <param name="nodeInfo">节点信息</param>
        /// <returns>操作视图基类工作区</returns>
        public BaseTaskItemPart GetTaskItemControl(NodeInfo nodeInfo)
        {
            string key = nodeInfo.Id.ToString();
            if (dict.ContainsKey(key))
            {
                BaseTaskItemPart baseTask = dict[key];
                if (baseTask.IsDisposed)
                {
                    dict.Remove(key);
                }
                else
                {
                    return baseTask;
                }
            }
            //操作视图的视图名称规则：项目名称_业务类型_节点名称，我们取得业务类型
            string typeName = nodeInfo.ViewCode.Split(new char[] { '_' })[1];
            typeName += "TaskItemsePart";
            try
            {


                Type baseTaskItemType = Type.GetType(string.Format("{0}.{1},{0}", "ICP.TaskCenter.UI", typeName));//Assembly.GetExecutingAssembly().GetExportedTypes().Where(type => type.Name == businessPartTypeName).First();


                BaseTaskItemPart part = WorkItem.SmartParts.AddNew(baseTaskItemType) as BaseTaskItemPart;
                dict[key] = part;
                // part.Init(mail);
                return part;
            }
            catch (Exception ex)
            {
                // Logger.Log.Error(DateTime.Now.ToString() + Environment.NewLine + CommonHelper.BuildExceptionString(ex));

                throw new ICPException(string.Format("实例化类型:{0}失败!", typeName + ex.Message));
            }

        }


        #region IDisposable 成员

        public void Dispose()
        {
            dict.Clear();
            dict = null;
            if (WorkItem != null)
            {
                WorkItem.Items.Remove(this);
                WorkItem = null;
            }
        }

        #endregion
    }
}
