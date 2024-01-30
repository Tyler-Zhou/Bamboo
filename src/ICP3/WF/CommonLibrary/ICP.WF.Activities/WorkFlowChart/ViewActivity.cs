using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using ICP.WF.ServiceInterface;
using ICP.WF.ServiceInterface.DataObject;

namespace ICP.WF.Activities
{
    /// <summary>
    /// 任务显示活动
    /// 只用于流程图的查看中使用
    /// </summary>
    [ToolboxItem(typeof(ActivityToolboxItem)), ToolboxBitmap(typeof(ViewActivity), "Resources.ApproveActivity.bmp")]
    [Designer(typeof(ViewActivityDesigner), typeof(IDesigner))]
	public partial class ViewActivity: Activity
    {
        #region 构造函数

        public ViewActivity()
		{
			InitializeComponent();
        }

        
        #endregion


        #region 公共属性

        string title;
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        string tipMessage;
        /// <summary>
        /// 提示信息
        /// </summary>
        public string TipMessage
        {
            get { return tipMessage; }
            set { tipMessage = value; }
        }

        FlowChartNode workItem;

        public FlowChartNode WorkItem
        {
            get { return workItem; }
            set { workItem = value; }
        }


        public void RaiseActivityEvent(DependencyProperty dependencyEvent, object sender, System.EventArgs e)
        {
            this.RaiseEvent(dependencyEvent, sender, e);
        }

        #endregion
    }


    /// <summary>
    /// ViewActivity活动的设计相关实现
    /// </summary>
    [ActivityDesignerTheme(typeof(ViewActivityDesignerTheme))]
    public class ViewActivityDesigner : ActivityDesigner
    {
        
        protected override Rectangle ImageRectangle
        {
            get
            {
                Rectangle bounds = this.Bounds;
                Size sz = new Size(24, 24);
                Rectangle imageRect = new Rectangle();
                imageRect.X = bounds.Left + ((bounds.Width - sz.Width) / 2);
                imageRect.Y = bounds.Top + 4;
                imageRect.Size = sz;
                return imageRect;
            }
        }

        protected override Rectangle TextRectangle
        {
            get
            {
                return new Rectangle(
                    this.Bounds.Left + 2,
                    this.ImageRectangle.Bottom,
                    this.Bounds.Width - 4,
                    this.Bounds.Height - this.ImageRectangle.Height - 1);
            }
        }

        protected override void Initialize(Activity activity)
        {
            Bitmap bmp = Properties.Resources.ApproveActivity;
            bmp.MakeTransparent();
            this.Image = bmp;

            this.Verbs.Clear();

        }


        readonly static Size BaseSize = new Size(64, 64);
        protected override Size OnLayoutSize(ActivityDesignerLayoutEventArgs e)
        {
            return BaseSize;
        }

        protected override void OnPaint(ActivityDesignerPaintEventArgs e)
        {
            DrawCustomActivity(e);
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
            ViewActivity activity = this.Activity as ViewActivity;
            if (activity != null)
            {

                this.ShowInfoTip(activity.Title, activity.TipMessage);
            }
        }

        protected override void OnMouseDoubleClick(System.Windows.Forms.MouseEventArgs e)
        {
            ViewActivity activity = this.Activity as ViewActivity;
            if (activity != null)
            {
                ViewSequenceActivity parent = (ViewSequenceActivity)activity.Parent;
                ViewWorkflowView workflowView = this.ParentView as ViewWorkflowView;
                if (workflowView != null 
                    && parent!=null)
                {
                    WorkItemEventArgs we = new WorkItemEventArgs(parent.WorkInfo);
                    workflowView.RaiseWorkItemDoubleClick(activity.WorkItem, we);
                }
              //  activity.RaiseActivityEvent(ViewSequenceActivity.WorkItemDoubleClickEvent, activity.WorkItem, System.EventArgs.Empty);
            }
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

        private void DrawCustomActivity(ActivityDesignerPaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            ViewActivityDesignerTheme compositeDesignerTheme = (ViewActivityDesignerTheme)e.DesignerTheme;
            ActivityDesignerPaint.DrawRoundedRectangle(graphics, compositeDesignerTheme.BorderPen, this.Bounds, compositeDesignerTheme.BorderWidth);
            ViewActivity activity = this.Activity as ViewActivity;
        
            if (this.IsSelected)
            {
                graphics.FillRectangle(SystemBrushes.GradientActiveCaption, this.Bounds);
            }
            else if (activity.WorkItem == null || activity.WorkItem.Id==System.Guid.Empty)
            {
                graphics.FillRectangle(SystemBrushes.ControlDark, this.Bounds);
            }
            else if (activity.WorkItem != null)
            {
                if (activity.WorkItem.State == WorkItemState.Waiting)
                {
                    graphics.FillRectangle(SystemBrushes.GradientActiveCaption, this.Bounds);
                }
                else if(activity.WorkItem.State == WorkItemState.Processing)
                {
                    graphics.FillRectangle(SystemBrushes.GradientActiveCaption, this.Bounds);
                }
                //else if (activity.WorkItem.State == WorkItemState.NotNextExcutor)
                //{
                //    graphics.FillRectangle(SystemBrushes.Control, this.Bounds);
                //}
                else if (activity.WorkItem.IsMainWorkItem)
                {
                    graphics.FillEllipse(Brushes.AliceBlue, this.Bounds);
                }
                else
                {
                    graphics.FillRectangle(SystemBrushes.Info, this.Bounds);
                }
            }
            else
            {
                graphics.FillRectangle(SystemBrushes.Info, this.Bounds);
            }

            if (activity != null)
            {
                string text = (activity.Title.Length > 20) ? (activity.Title.Substring(0, 20) + "...") : activity.Title;
                Rectangle textRectangle = this.TextRectangle;
                if (!string.IsNullOrEmpty(text) && !textRectangle.IsEmpty)
                {
                    ActivityDesignerPaint.DrawText(graphics, compositeDesignerTheme.Font, text, textRectangle, StringAlignment.Center, e.AmbientTheme.TextQuality, compositeDesignerTheme.ForegroundBrush);
                }

                System.Drawing.Image image = this.Image;
                Rectangle imageRectangle = this.ImageRectangle;
                if (image != null && !imageRectangle.IsEmpty)
                {
                    ActivityDesignerPaint.DrawImage(graphics, image, imageRectangle, DesignerContentAlignment.Left);
                }
            }
        }
    }

    /// <summary>
    /// ViewActivity界面主题定义
    /// </summary>
    public class ViewActivityDesignerTheme : ActivityDesignerTheme
    {
        public ViewActivityDesignerTheme(WorkflowTheme theme)
            : base(theme)
        {
           // WorkflowTheme.CurrentTheme.AmbientTheme.ShowConfigErrors = false;
            //this.ContainingTheme.AmbientTheme.ShowConfigErrors = false;
            this.ForeColor = Color.Red;
            this.ForeColor = Color.FromArgb(0xff, 0, 0, 0);
            this.BorderColor = Color.FromArgb(0xff, 0x80, 0x40, 0x40);
            this.BorderStyle = DashStyle.Solid;
            this.BackColorStart = Color.AliceBlue;
            this.BackColorEnd = Color.AliceBlue;
            this.BackgroundStyle = LinearGradientMode.Horizontal;
        }
    }
}
