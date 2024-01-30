using System;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace ICP.WF.Activities
{
    /// <summary>
    /// 查看流程文件的流程图
    /// </summary>
    public class ViewXomlWorkflowView : UserControl
    {
        #region 本地变量

        private ViewWorkflowView workflowView;

        #endregion

        #region 构造函数

        public ViewXomlWorkflowView()
        {
            if (DesignMode == false)
            {
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            }
        }

        protected override System.Windows.Forms.Padding DefaultPadding
        {
            get
            {
                return new System.Windows.Forms.Padding(0);
            }
        }

        protected override System.Windows.Forms.Padding DefaultMargin
        {
            get
            {
                return new System.Windows.Forms.Padding(0);
            }
        }

        #endregion


        #region 本地方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void Init(string xoml)
        {
            DesignSurface designSurface = new DesignSurface();
            ViewXomlWorkFlowChartLoader loader = new ViewXomlWorkFlowChartLoader(xoml);
            designSurface.BeginLoad(loader);
            IDesignerHost designerHost = designSurface.GetService(typeof(IDesignerHost)) as IDesignerHost;
            if (designerHost != null && designerHost.RootComponent != null)
            {
                IRootDesigner rootDesigner = designerHost.GetDesigner(designerHost.RootComponent) as IRootDesigner;
                if (rootDesigner != null)
                {
                    ClearWorkFlowChart();

                    this.workflowView = new ViewWorkflowView(designSurface); //rootDesigner.GetView(ViewTechnology.Default) as WorkflowView;
                    this.workflowView.WorkItemDoubleClick += delegate(object sender, ICP.WF.ServiceInterface.DataObject.WorkItemEventArgs e)
                    {
                        RaiseWorkItemDoubleClick(sender, e);
                    };
                    this.Controls.Add(this.workflowView);
                    this.workflowView.Margin = new Padding(1, 1, 1, 1);
                    this.workflowView.Dock = DockStyle.Fill;
                    this.workflowView.TabIndex = 1;
                    this.workflowView.TabStop = true;
                    this.workflowView.HScrollBar.TabStop = false;
                    this.workflowView.VScrollBar.TabStop = false;
                    //this.workflowView.PrintPreviewMode = true;
                    this.workflowView.Focus();
                }
                designerHost.Activate();
            }
            ResumeLayout(true);
        }


        /// <summary>
        /// 在任务双击时触发该事件
        /// </summary>
        /// <param name="sender">任务数据对象</param>
        /// <param name="e"></param>
        private void RaiseWorkItemDoubleClick(object sender, EventArgs e)
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
        public void ShowWorkFlowChart(string xoml)
        {
            Init(xoml);
        }

        /// <summary>
        /// 清除流程
        /// </summary>
        public void ClearWorkFlowChart()
        {
            if (this.workflowView != null)
            {
                Controls.Remove(this.workflowView);
                this.workflowView.Dispose();
                this.workflowView = null;
            }
        }

        /// <summary>
        /// 在任务双击时触发该事件
        /// </summary>
        public event EventHandler WorkItemDoubleClick;

        #endregion

    }
}
