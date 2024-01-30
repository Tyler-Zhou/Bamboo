using System;
using System.Drawing;
using System.Workflow.ComponentModel.Design;
using ICP.WF.ServiceInterface.DataObject;
using ICP.WF.ServiceInterface;

namespace ICP.WF.Activities
{
    /// <summary>
    /// 流程查看控件
    /// </summary>
    public class ViewWorkflowView : WorkflowView
    {
        #region 构造函数

        public ViewWorkflowView(IServiceProvider provider)
            : base(provider)
        {
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
         
        }

        #endregion

        protected override System.Windows.Forms.Padding DefaultPadding
        {
            get
            {
                return new System.Windows.Forms.Padding(0);
                //return base.DefaultPadding;
            }
        }

        protected override System.Windows.Forms.Padding DefaultMargin
        {
            get
            {
                return new System.Windows.Forms.Padding(0);
            }
        }


        #region 外部接口

        /// <summary>
        /// 在任务双击时触发该事件
        /// </summary>
        public event EventHandler<WorkItemEventArgs> WorkItemDoubleClick;

        /// <summary>
        /// 在任务双击时触发该事件
        /// </summary>
        /// <param name="sender">任务数据对象</param>
        /// <param name="e"></param>
        public void RaiseWorkItemDoubleClick(object sender, WorkItemEventArgs e)
        {
            if (WorkItemDoubleClick != null)
            {
                WorkItemDoubleClick(sender, e);
            }
        }


        public new Size ViewPortSize
        {
            get
            {
                return this.Size;
            }
        }
        #endregion

    }
    
}
