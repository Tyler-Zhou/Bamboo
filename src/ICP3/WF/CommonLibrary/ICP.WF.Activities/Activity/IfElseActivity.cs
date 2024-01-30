using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Design;
using ICP.WF.Activities.Common;
using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.WF.Activities
{
    /// <summary>
    /// 自定义IfElse活动
    /// </summary>
    /// 
    [ToolboxItem(typeof(LWIfElseToolboxItem)), ToolboxBitmap(typeof(LWIfElseActivity), "Resources.Decision.bmp")]
    [Designer(typeof(LWIfElseDesigner), typeof(IDesigner))]
    [SRDescription("DescLWIfElseActivity"), SRCategory("Standard"), SRTitle("TitleLWIFElseActivity")]
    public sealed class LWIfElseActivity : CompositeActivity, IActivityEventListener<ActivityExecutionStatusChangedEventArgs>
    {
        #region 构造函数

        public LWIfElseActivity()
        {
        }

        public LWIfElseActivity(string name)
            : base(name)
        {
        }
        #endregion

        #region 重载属性
        /// <summary>
        /// 名称
        /// </summary>
        [SRDisplayName("DispName"), ICPBrowsable(true), SRCategory("Base")]
        public new string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                base.Name = value;
            }
        }

        /// <summary>
        /// 描述
        /// </summary>
        [SRDisplayName("DispDescription"), ICPBrowsable(true), SRCategory("Base")]
        public new string Description
        {
            get
            {
                return base.Description;
            }
            set
            {
                base.Description = value;
            }
        }
        #endregion

        #region 重载方法
        public LWIfElseBranchActivity AddBranch(ICollection<Activity> activities)
        {
            if (activities == null)
            {
                throw new ArgumentNullException("activities");
            }
            return this.AddBranch(activities, null);
        }

        public LWIfElseBranchActivity AddBranch(ICollection<Activity> activities, ActivityCondition branchCondition)
        {
            if (activities == null)
            {
                throw new ArgumentNullException("activities");
            }
            if (!base.DesignMode)
            {
                throw new InvalidOperationException(SR.GetString("Error_ConditionalBranchUpdateAtRuntime"));
            }
            LWIfElseBranchActivity item = new LWIfElseBranchActivity();
            foreach (Activity activity2 in activities)
            {
                item.Activities.Add(activity2);
            }
            item.Condition = branchCondition;
            base.Activities.Add(item);
            return item;
        }

        protected override ActivityExecutionStatus Cancel(ActivityExecutionContext executionContext)
        {
            bool flag = true;
            for (int i = 0; i < base.EnabledActivities.Count; i++)
            {
                Activity activity = base.EnabledActivities[i];
                if (activity.ExecutionStatus == ActivityExecutionStatus.Executing)
                {
                    flag = false;
                    executionContext.CancelActivity(activity);
                    break;
                }
                if ((activity.ExecutionStatus == ActivityExecutionStatus.Canceling) || (activity.ExecutionStatus == ActivityExecutionStatus.Faulting))
                {
                    flag = false;
                    break;
                }
            }
            if (!flag)
            {
                return ActivityExecutionStatus.Canceling;
            }
            return ActivityExecutionStatus.Closed;
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            if (this.Enabled == false) return  ActivityExecutionStatus.Closed;

            if (executionContext == null)
            {
                throw new ArgumentNullException("executionContext");
            }
            bool flag = true;
            for (int i = 0; i < base.EnabledActivities.Count; i++)
            {
                LWIfElseBranchActivity activity = base.EnabledActivities[i] as LWIfElseBranchActivity;
                if ((activity.Condition == null)||activity.Condition.Evaluate(activity, executionContext))
                {
                    flag = false;
                    activity.RegisterForStatusChange(Activity.ClosedEvent, this);
                    executionContext.ExecuteActivity(activity);
                    break;
                }
            }
            if (!flag)
            {
                return ActivityExecutionStatus.Executing;
            }
            return ActivityExecutionStatus.Closed;
        }

        protected override ActivityExecutionStatus HandleFault(ActivityExecutionContext executionContext, Exception exception)
        {
            return ActivityExecutionStatus.Closed;
        }

        void IActivityEventListener<ActivityExecutionStatusChangedEventArgs>.OnEvent(object sender, ActivityExecutionStatusChangedEventArgs e)
        {
            if (sender == null)
            {
                throw new ArgumentNullException("sender");
            }
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            ActivityExecutionContext context = sender as ActivityExecutionContext;
            if (context == null)
            {
                throw new ArgumentException(SR.Error_SenderMustBeActivityExecutionContext, "sender");
            }
            context.CloseActivity();
        }

        #endregion
    }


    /// <summary>
    /// IfElse活动设计器
    /// </summary>
    [ActivityDesignerTheme(typeof(LWIfElseDesignerTheme))]
    internal sealed class LWIfElseDesigner : ParallelActivityDesigner
    {
        #region 重载

        public override bool CanInsertActivities(HitTestInfo insertLocation, ReadOnlyCollection<Activity> activitiesToInsert)
        {
            using (IEnumerator<Activity> enumerator = activitiesToInsert.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    if (!(enumerator.Current is LWIfElseBranchActivity))
                    {
                        return false;
                    }
                }
            }
            return base.CanInsertActivities(insertLocation, activitiesToInsert);
        }

        public override string Text
        {
            get
            {
                return string.IsNullOrEmpty(Activity.Description) ? SR.GetString("ConditionBranch") : Activity.Description;
            }
            protected set
            {
                base.Text = value;
            }
        }

        public override bool CanMoveActivities(HitTestInfo moveLocation, ReadOnlyCollection<Activity> activitiesToMove)
        {
            if ((((this.ContainedDesigners.Count - activitiesToMove.Count) < 1) && (moveLocation != null)) && (moveLocation.AssociatedDesigner != this))
            {
                return false;
            }
            return true;
        }

        public override bool CanRemoveActivities(ReadOnlyCollection<Activity> activitiesToRemove)
        {
            if ((this.ContainedDesigners.Count - activitiesToRemove.Count) < 1)
            {
                return false;
            }
            return true;
        }

        private GraphicsPath GetDiamondPath(Rectangle rectangle)
        {
            Point[] points = new Point[] { new Point(rectangle.Left + (rectangle.Width / 2), rectangle.Top), new Point(rectangle.Right - 1, rectangle.Top + (rectangle.Height / 2)), new Point(rectangle.Left + (rectangle.Width / 2), rectangle.Bottom - 1), new Point(rectangle.Left, rectangle.Top + (rectangle.Height / 2)), new Point(rectangle.Left + (rectangle.Width / 2), rectangle.Top) };
            GraphicsPath path = new GraphicsPath();
            path.AddLines(points);
            path.CloseFigure();
            return path;
        }

        protected override CompositeActivity OnCreateNewBranch()
        {
            return new LWIfElseBranchActivity();
        }

        protected override void OnPaint(ActivityDesignerPaintEventArgs e)
        {
            base.OnPaint(e);
            if ((this.Expanded && (this.ContainedDesigners.Count != 0)) && (this == base.ActiveView.AssociatedDesigner))
            {
                CompositeDesignerTheme designerTheme = e.DesignerTheme as CompositeDesignerTheme;
                if (designerTheme != null)
                {
                    Rectangle bounds = base.Bounds;
                    Rectangle imageRectangle = this.ImageRectangle;
                    Rectangle empty = Rectangle.Empty;
                    empty.Width = (designerTheme.ConnectorSize.Height - (2 * e.AmbientTheme.Margin.Height)) + 2;
                    empty.Height = empty.Width;
                    empty.X = (bounds.Left + (bounds.Width / 2)) - (empty.Width / 2);
                    empty.Y = ((bounds.Top + this.TitleHeight) + ((((designerTheme.ConnectorSize.Height * 3) / 2) - empty.Height) / 2)) + 1;
                    using (GraphicsPath path = this.GetDiamondPath(empty))
                    {
                        e.Graphics.FillPath(designerTheme.ForegroundBrush, path);
                        e.Graphics.DrawPath(designerTheme.ForegroundPen, path);
                    }
                    empty.Y = ((bounds.Bottom - ((designerTheme.ConnectorSize.Height * 3) / 2)) + ((((designerTheme.ConnectorSize.Height * 3) / 2) - empty.Height) / 2)) + 1;
                    using (GraphicsPath path2 = this.GetDiamondPath(empty))
                    {
                        e.Graphics.FillPath(designerTheme.ForegroundBrush, path2);
                        e.Graphics.DrawPath(designerTheme.ForegroundPen, path2);
                    }
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// IfElse验证辅助类
    /// </summary>
    internal sealed class WFIfElseValidator : CompositeActivityValidator
    {
        #region 重载
        public override ValidationErrorCollection Validate(ValidationManager manager, object obj)
        {
            ValidationErrorCollection errors = base.Validate(manager, obj);
            LWIfElseActivity activity = obj as LWIfElseActivity;
            if (activity == null)
            {
                throw new ArgumentException("obj");
            }
            if (activity.EnabledActivities.Count < 1)
            {
                errors.Add(new ValidationError(SR.GetString("ConditionalLessThanOneChildren", "分支必须有一个以上."), 1292));
            }
            using (IEnumerator<Activity> enumerator = activity.EnabledActivities.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    if (!(enumerator.Current is LWIfElseBranchActivity))
                    {
                        errors.Add(new ValidationError("分支类型必须是:ICPIfElseBranchActivity", 1293));
                        return errors;
                    }
                }
            }
            return errors;
        }

        public override ValidationError ValidateActivityChange(Activity activity, ActivityChangeAction action)
        {
            if (activity == null)
            {
                throw new ArgumentNullException("activity");
            }
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            if ((activity.ExecutionStatus != ActivityExecutionStatus.Initialized) && (activity.ExecutionStatus != ActivityExecutionStatus.Closed))
            {
                return new ValidationError(SR.GetString("Error_DynamicActivity", new object[] { activity.QualifiedName, Enum.GetName(typeof(ActivityExecutionStatus), activity.ExecutionStatus) }), 260);
            }
            return null;
        }
        #endregion
    }


    /// <summary>
    /// IfElse设计主题相关
    /// </summary>
    internal sealed class LWIfElseDesignerTheme : CompositeDesignerTheme
    {
        public LWIfElseDesignerTheme(WorkflowTheme theme)
            : base(theme)
        {
            this.ShowDropShadow = false;
            this.ConnectorStartCap = LineAnchor.None;
            this.ConnectorEndCap = LineAnchor.None;
            this.ForeColor = Color.FromArgb(0xff, 0, 100, 0);
            this.BorderColor = Color.FromArgb(0xff, 0xe0, 0xe0, 0xe0);
            this.BorderStyle = DashStyle.Dash;
            this.BackColorStart = Color.FromArgb(0, 0, 0, 0);
            this.BackColorEnd = Color.FromArgb(0, 0, 0, 0);
        }
    }


    /// <summary>
    /// IfElse工具箱相关类
    /// </summary>
    [Serializable]
    internal sealed class LWIfElseToolboxItem : ActivityToolboxItem
    {
        public LWIfElseToolboxItem(Type type)
            : base(type)
        {
        }

        private LWIfElseToolboxItem(SerializationInfo info, StreamingContext context)
        {
            this.Deserialize(info, context);
        }

        protected override IComponent[] CreateComponentsCore(IDesignerHost designerHost)
        {
            CompositeActivity activity = new LWIfElseActivity();
            activity.Activities.Add(new LWIfElseBranchActivity());
            activity.Activities.Add(new LWIfElseBranchActivity());
            return new IComponent[] { activity };
        }
    }
}
