using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;

namespace ICP.WF.Activities
{
    /// <summary>
    /// 提供可用于实现自定义设计器加载程序的基本设计器加载程序
    /// </summary>
    internal class WorkflowChartLoader : WorkflowDesignerLoader
    {
        #region 构造函数

        internal WorkflowChartLoader()
        {
        }


        #endregion

        #region WorkflowDesignerLoader重载

        /// <summary>
        /// 加载流程.
        /// </summary>
        /// <param name="serializationManager"></param>
        protected override void PerformLoad(IDesignerSerializationManager serializationManager)
        {
            base.PerformLoad(serializationManager);

            InitWorkFlowView();
        }


        public override void Flush()
        {
            base.Flush();

        }

        public virtual void InitWorkFlowView()
        {
        }


        public override TextReader GetFileReader(string filePath)
        {
            return null;
        }

        public override TextWriter GetFileWriter(string filePath)
        {
            return null;
        }

        public override string FileName
        {
            get { return string.Empty; }
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 添加对象图在设计器中
        /// </summary>
        /// <param name="designerHost">提供用于管理设计器事务和组件的接口</param>
        /// <param name="activity">活动</param>
        protected void AddObjectGraphToDesignerHost(IDesignerHost designerHost, Activity activity)
        {
            Guid Definitions_Class = new Guid("3FA84B23-B15B-4161-8EB8-37A54EFEEFC7");

            if (designerHost == null)
            {
                throw new ArgumentNullException("designerHost");
            }
            if (activity == null)
            {
                throw new ArgumentNullException("activity");
            }

            string rootSiteName = activity.QualifiedName;
            if (activity.Parent == null)
            {
                string fullClassName = activity.UserData[Definitions_Class] as string;
                if (fullClassName == null)
                {
                    fullClassName = activity.GetType().FullName;
                }
                rootSiteName = (fullClassName.LastIndexOf('.') != -1) ? fullClassName.Substring(fullClassName.LastIndexOf('.') + 1) : fullClassName;
                designerHost.Container.Add(activity, rootSiteName);
            }
            else
            {
                designerHost.Container.Add(activity, activity.QualifiedName);
            }

            if (activity is CompositeActivity)
            {
                foreach (Activity activity2 in GetNestedActivities(activity as CompositeActivity))
                {
                    designerHost.Container.Add(activity2, activity2.QualifiedName);
                }
            }
        }

        /// <summary>
        /// 根据复合活动获取它下面的子活动
        /// </summary>
        /// <param name="compositeActivity">复合活动</param>
        /// <returns>返回所有子级活动列表</returns>
        protected Activity[] GetNestedActivities(CompositeActivity compositeActivity)
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


        /// <summary>
        /// 销毁对象从设计器中
        /// </summary>
        /// <param name="designerHost">提供用于管理设计器事务和组件的接口</param>
        /// <param name="activity">活动</param>
        protected void DestroyObjectGraphFromDesignerHost(IDesignerHost designerHost, Activity activity)
        {
            if (designerHost == null)
            {
                throw new ArgumentNullException("designerHost");
            }
            if (activity == null)
            {
                throw new ArgumentNullException("activity");
            }

            designerHost.DestroyComponent(activity);

            if (activity is CompositeActivity)
            {
                foreach (Activity activity2 in GetNestedActivities(activity as CompositeActivity))
                {
                    designerHost.DestroyComponent(activity2);
                }
            }
        }

        #endregion
    }
}
