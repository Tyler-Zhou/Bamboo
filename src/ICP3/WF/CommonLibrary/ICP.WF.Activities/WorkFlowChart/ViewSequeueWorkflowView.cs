

namespace ICP.WF.Activities
{
    using System;
    using System.ComponentModel.Design;
    using System.Windows.Forms;
    using ICP.WF.ServiceInterface;
    using ICP.WF.ServiceInterface.DataObject;

    /// <summary>
    /// 查看指定流程的完成情况的流程图控件 
    /// </summary>
    public class ViewSequeueWorkflowView : UserControl
    {
        #region 本地变量

        private ViewWorkflowView workflowView;

        #endregion

        #region 构造函数

        public ViewSequeueWorkflowView()
        {
            if (DesignMode == false)
            {

            }
        }

        #endregion


        #region 本地方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void Init(IWorkflowService workflowService, Guid workFlowId)
        {
            SuspendLayout();

            DesignSurface designSurface = new DesignSurface();
            ViewSequeueWorkflowChartLoader loader = new ViewSequeueWorkflowChartLoader(workflowService, workFlowId);
            designSurface.BeginLoad(loader);
            IDesignerHost designerHost = designSurface.GetService(typeof(IDesignerHost)) as IDesignerHost;
            if (designerHost != null && designerHost.RootComponent != null)
            {
                IRootDesigner rootDesigner = designerHost.GetDesigner(designerHost.RootComponent) as IRootDesigner;
                if (rootDesigner != null)
                {
                    ClearWorkFlowChart();
                    this.workflowView = new ViewWorkflowView(designSurface);

                    isCanceled = loader.isCanceled;
                    
                    this.workflowView.WorkItemDoubleClick += delegate(object sender, WorkItemEventArgs e)
                    {
                        RaiseWorkItemDoubleClick(sender, e);
                    };
                    this.workflowView.Dock = DockStyle.Fill;
                    this.Controls.Add(this.workflowView);
                    this.workflowView.Margin = new Padding(0);
                    this.workflowView.TabIndex = 1;
                    this.workflowView.TabStop = true;
                    this.workflowView.HScrollBar.TabStop = true;
                    this.workflowView.HScrollBar.Value = this.workflowView.HScrollBar.Width/2;
                    this.workflowView.VScrollBar.TabStop = true;
                    this.workflowView.PrintPreviewMode = false;
                    this.workflowView.Focus();
                }
                designerHost.Activate();
            }

            ResumeLayout(true);
        }

        /// <summary>
        /// 是否可以进行取消操作
        /// </summary>
        public bool isCanceled
        {
            get;
            set;
        }

        /// <summary>
        /// 在任务双击时触发该事件
        /// </summary>
        /// <param name="sender">任务数据对象</param>
        /// <param name="e"></param>
        private void RaiseWorkItemDoubleClick(object sender, WorkItemEventArgs e)
        {
            if (WorkItemDoubleClick != null)
            {
                WorkItemDoubleClick(sender, e);
            }
        }

        #endregion

        #region 外部接口

        /// <summary>
        /// 显示指定流程的流程图
        /// </summary>
        /// <param name="workflowService"></param>
        /// <param name="workFlowId"></param>
        public void ShowWorkFlowChart(IWorkflowService workflowService, Guid workFlowId)
        {
            Init(workflowService, workFlowId);
        }

        /// <summary>
        /// 清空流程图
        /// </summary>
        public void ClearWorkFlowChart()
        {
            if (this.workflowView != null)
            {
                this.Controls.Remove(this.workflowView);
                this.workflowView.Dispose();
                this.workflowView = null;
            }
        }

        /// <summary>
        /// 在任务双击时触发该事件
        /// </summary>
        public event EventHandler<WorkItemEventArgs> WorkItemDoubleClick;

        #endregion

    }

}
