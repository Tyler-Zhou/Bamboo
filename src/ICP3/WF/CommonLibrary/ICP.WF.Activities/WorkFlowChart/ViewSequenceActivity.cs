using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Workflow.Activities;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;

namespace ICP.WF.Activities
{
    /// <summary>
    /// 用于查看流程图主活动容器
    /// </summary>
    [Designer(typeof(WFSequentialWorkflowRootDesigner), typeof(IRootDesigner))]
    public partial class ViewSequenceActivity : SequentialWorkflowActivity
	{

		public ViewSequenceActivity()
		{
			InitializeComponent();
		}

        string title = SR.GetString("FlowChart", "流程图");
        /// <summary>
        /// 显示标题
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public ICP.WF.ServiceInterface.DataObject.FlowChartInfo WorkInfo { get; set; }


        ICP.WF.ServiceInterface.WorkflowState _workflowState= ICP.WF.ServiceInterface.WorkflowState.Activated;

        /// <summary>
        /// 是否是所有任务都已经完成
        /// </summary>
        public ICP.WF.ServiceInterface.WorkflowState WorkflowState
        {
            get { return _workflowState; }
            set { _workflowState = value; }
        }
	}

    /// <summary>
    /// 流程设计器设计类
    /// </summary>
    [ActivityDesignerTheme(typeof(WFSequentialWorkflowDesignerTheme))]
    public class WFSequentialWorkflowRootDesigner : SequentialWorkflowRootDesigner
    {
        protected override void Initialize(Activity activity)
        {
            base.Initialize(activity);
            this.Expanded = false;
           
            this.HelpText = "";
            this.Header.Text = SR.GetString("FlowChart", "流程图");
            ViewSequenceActivity ac = activity as ViewSequenceActivity;
            if (ac != null)
            {
                this.Header.Text = ac.Title;
            }
        }

        public override bool CanRemoveActivities(ReadOnlyCollection<Activity> activitiesToRemove)
        {
            return false;
        }

        public override bool CanMoveActivities(HitTestInfo moveLocation, ReadOnlyCollection<Activity> activitiesToMove)
        {
            return false;
        }


        protected override void OnMouseDragBegin(Point initialDragPoint, System.Windows.Forms.MouseEventArgs e)
        {
            return;
        }

        protected override void OnDragEnter(ActivityDragEventArgs e)
        {
            return;
        }

        protected override void OnDragLeave()
        {
            return;
        }

        protected override void OnDragOver(ActivityDragEventArgs e)
        {
            return;
        }

        protected override void OnDragDrop(ActivityDragEventArgs e)
        {
            return;
        }
      
        protected override Size OnLayoutSize(ActivityDesignerLayoutEventArgs e)
        {
            Size size = base.OnLayoutSize(e);
            if (this.Header != null)
            {
                this.Header.OnLayout(e);
            }
            if (this.Footer != null)
            {
                ViewSequenceActivity ac = this.Activity as ViewSequenceActivity;
                if (ac != null && ac.WorkflowState != ICP.WF.ServiceInterface.WorkflowState.Finished)
                {
                    this.Footer.Image = null;
                }

                this.Footer.OnLayout(e);
            }
            return size;
        }

        protected override void OnPaint(ActivityDesignerPaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.Header != null)
            {
                this.Header.OnPaint(e);
            }
            if (this.Footer != null)
            {
                ViewSequenceActivity ac = this.Activity as ViewSequenceActivity;
                if (ac != null && ac.WorkflowState != ICP.WF.ServiceInterface.WorkflowState.Finished)
                {
                    this.Footer.Image = null;
                }

                this.Footer.OnPaint(e);
            }
        }



        protected override Rectangle[] GetConnectors()
        {
            Rectangle[] oldconnectors = base.GetConnectors();
            ViewSequenceActivity ac = this.Activity as ViewSequenceActivity;
            if (ac != null && ac.WorkflowState != ICP.WF.ServiceInterface.WorkflowState.Finished)
            {
                if (oldconnectors.Length > 0)
                {
                    Rectangle[] connectors = new Rectangle[oldconnectors.Length - 1];
                    System.Array.Copy(oldconnectors, connectors, oldconnectors.Length - 1);
                    return connectors;
                }
            }
           
            return oldconnectors;
        }
       
        protected override bool ShowSmartTag
        {
            get
            {
                return false;
            }
        }

        protected override ReadOnlyCollection<ActivityDesignerVerb> SmartTagVerbs
        {
            get
            {
                List<ActivityDesignerVerb> list = new List<ActivityDesignerVerb>();
                return list.AsReadOnly();
            }
        }

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            ShowTipInfo();
        }

        protected override void OnMouseHover(System.Windows.Forms.MouseEventArgs e)
        {
            ShowTipInfo();
        }

        protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            ShowTipInfo();
        }


        private void ShowTipInfo()
        {
            ViewSequenceActivity activity = this.Activity as ViewSequenceActivity;
            if (activity != null)
            {
                this.ShowInfoTip( activity.Title);
            }
        }
    }

    /// <summary>
    /// 控制流程图的主题
    /// </summary>
       
    internal  class WFSequentialWorkflowDesignerTheme : CompositeDesignerTheme
    {
        public WFSequentialWorkflowDesignerTheme(WorkflowTheme theme)
            : base(theme)
        {
            this.ContainingTheme.AmbientTheme.ShowConfigErrors = false;
            this.WatermarkAlignment = DesignerContentAlignment.Fill;
            this.ShowDropShadow = true;
            this.ConnectorStartCap = LineAnchor.None;
            this.ConnectorEndCap = LineAnchor.ArrowAnchor;
            this.ForeColor = Color.FromArgb(0xff, 0, 0, 0);
            this.BorderColor = Color.FromArgb(0xff, 0x49, 0x77, 180);
            this.BorderStyle = DashStyle.Solid;
            this.BackColorStart = Color.White;
            this.BackColorEnd = Color.White;
        }

      
    }

    public class WFAmbientTheme : AmbientTheme
    {
        public WFAmbientTheme(WorkflowTheme theme)
            : base(theme)
        {
            this.DesignerSize = DesignerSize.Large;
            this.ShowConfigErrors = false;
            this.ShowDesignerBorder = false;
        }

        public override Size Margin
        {
            get
            {
                return new Size(0,0);
            }
        }
    }
}
