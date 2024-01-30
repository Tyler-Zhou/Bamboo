
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Xml;

namespace ICP.WF.WorkFlowDesigner
{

    /// <summary>
    /// 流程设计器一些常用辅助方法
    /// </summary>
    internal static class Helpers
    {
        /// <summary>
        /// 获取指定活动的根活动
        /// </summary>
        /// <param name="activity">子活动</param>
        /// <returns>返回根活动</returns>
        internal static Activity GetRootActivity(Activity activity)
        {
            if (activity == null)
            {
                throw new ArgumentNullException("activity");
            }

            while (activity.Parent != null)
            {
                activity = activity.Parent;
            }

            return activity;
        }

        /// <summary>
        /// 根据XOML获取根活动
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <returns></returns>
        internal static Activity GetRootActivity(string fileName)
        {
            XmlReader reader = new XmlTextReader(fileName);
            Activity rootActivity = null;
            try
            {
                WorkflowMarkupSerializer xomlSerializer = new WorkflowMarkupSerializer();
                rootActivity = xomlSerializer.Deserialize(reader) as Activity;
            }
            finally
            {
                reader.Close();
            }
            return rootActivity;

        }

        /// <summary>
        /// 获取指定活动的Type类型
        /// </summary>
        /// <param name="activity">活动</param>
        /// <param name="serviceProvider">定义一种检索服务对象的机制，服务对象是为其他对象提供自定义支持的对象的</param>
        /// <returns></returns>
        internal static Type GetDataSourceClass(Activity activity, IServiceProvider serviceProvider)
        {
            if (activity == null)
            {
                throw new ArgumentNullException("activity");
            }
            if (serviceProvider == null)
            {
                throw new ArgumentNullException("serviceProvider");
            }

            Type activityType = null;
            string className = null;
            if (activity == GetRootActivity(activity))
            {
                className = activity.GetValue(WorkflowMarkupSerializer.XClassProperty) as String;
            }

            if (!String.IsNullOrEmpty(className))
            {
                ITypeProvider typeProvider = (ITypeProvider)serviceProvider.GetService(typeof(ITypeProvider));
                if (typeProvider == null)
                {
                    throw new InvalidOperationException(typeof(ITypeProvider).Name);
                }

                activityType = typeProvider.GetType(className);
            }
            else
            {
                return activity.GetType();
            }

            return activityType;
        }

       
        /// <summary>
        /// 根据全名,计算出命名空间跟类名
        /// </summary>
        /// <param name="fullQualifiedName">类型全名</param>
        /// <param name="namespaceName">返回的命名空间</param>
        /// <param name="className">返回的类名</param>
        internal static void GetNamespaceAndClassName(string fullQualifiedName, out string namespaceName, out string className)
        {
            namespaceName = String.Empty;
            className = String.Empty;

            if (fullQualifiedName == null)
            {
                return;
            }

            int indexOfDot = fullQualifiedName.LastIndexOf('.');
            if (indexOfDot != -1)
            {
                namespaceName = fullQualifiedName.Substring(0, indexOfDot);
                className = fullQualifiedName.Substring(indexOfDot + 1);
            }
            else
            {
                className = fullQualifiedName;
            }
        }

        /// <summary>
        /// 根据复合活动获取它下面的子活动
        /// </summary>
        /// <param name="compositeActivity">复合活动</param>
        /// <returns>返回所有子级活动列表</returns>
        internal static Activity[] GetNestedActivities(CompositeActivity compositeActivity)
        {

            if (compositeActivity == null)
            {
                throw new ArgumentNullException("compositeActivity");
            }

            IList<Activity> childActivities = null;
            ArrayList nestedActivities = new ArrayList();
            Queue compositeActivities = new Queue();
            compositeActivities.Enqueue(compositeActivity);
            while (compositeActivities.Count > 0)
            {
                CompositeActivity compositeActivity2 = (CompositeActivity)compositeActivities.Dequeue();
                childActivities = compositeActivity2.Activities;

                foreach (Activity activity in childActivities)
                {
                    nestedActivities.Add(activity);
                    if (activity is CompositeActivity)
                    {
                        compositeActivities.Enqueue(activity);
                    }
                }
            }
            return (Activity[])nestedActivities.ToArray(typeof(Activity));
        }

        internal static bool IsActivityLocked(Activity activity)
        {
            if (activity == null)
            {
                throw new ArgumentNullException("activity");
            }

            CompositeActivity parent = activity.Parent;
            while (parent != null)
            {
                // 父活动,不让锁定
                if (parent.Parent == null)
                {
                    return false;
                }

                // 自定义活动锁定,不让修改
                if (IsCustomActivity(parent))
                {
                    return true;
                }

                parent = parent.Parent;
            }

            return false;
        }

        /// <summary>
        /// 判断是否自定义活动
        /// </summary>
        /// <param name="compositeActivity">复合活动</param>
        /// <returns>如果是返回:true,否则返回:false</returns>
        internal static bool IsCustomActivity(CompositeActivity compositeActivity)
        {
            if (compositeActivity == null)
            {
                throw new ArgumentNullException("compositeActivity");
            }

            Guid CustomActivity = new Guid("298CF3E0-E9E0-4d41-A11B-506E9132EB27");
            Guid CustomActivityDefaultName = new Guid("8bcd6c40-7bf6-4e60-8eea-bbf40bed92da");

            if (compositeActivity.UserData.Contains(new Guid("298CF3E0-E9E0-4d41-A11B-506E9132EB27")))
            {
                return (bool)(compositeActivity.UserData[CustomActivity]);
            }
            else
            {
                try
                {
                    CompositeActivity activity = Activator.CreateInstance(compositeActivity.GetType()) as CompositeActivity;
                    if (activity != null && activity.Activities.Count > 0)
                    {
                        compositeActivity.UserData[CustomActivityDefaultName] = activity.Name;
                        compositeActivity.UserData[CustomActivity] = true;
                        return true;
                    }
                }
                catch
                {
                }
            }

            compositeActivity.UserData[CustomActivity] = false;
            return false;
        }

        private class DummySite : ISite
        {
            public IComponent Component { get { return null; } }

            public IContainer Container { get { return null; } }
            
            public bool DesignMode { get { return true; } }
            
            public string Name { get { return string.Empty; } set { } }
            
            public object GetService(Type type) { return null; }
        }
    }


}
