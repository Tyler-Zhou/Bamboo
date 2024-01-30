using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Workflow.Activities;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using ICP.WF.Activities.Common;
using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.WF.Activities
{

    /// <summary>
    /// 自定义条件分支
    /// </summary>
    [Designer(typeof(LWIfElseBranchDesigner), typeof(IDesigner)), ToolboxBitmap(typeof(LWIfElseBranchActivity), "Resources.DecisionBranch.bmp"), SRCategory("Standard"), ToolboxItem(false)]
    public sealed class LWIfElseBranchActivity : SequenceActivity
    {

        #region 构造函数


        public LWIfElseBranchActivity()
        {
        }

        public LWIfElseBranchActivity(string name)
            : base(name)
        {
        }

        #endregion

        #region 重载属性

        public static readonly DependencyProperty ConditionProperty = DependencyProperty.Register("Condition", typeof(ActivityCondition), typeof(LWIfElseBranchActivity), new PropertyMetadata(DependencyPropertyOptions.Metadata));
        [ICPBrowsable(true), SRDisplayName("Condition")]
        [RefreshProperties(RefreshProperties.Repaint), DefaultValue((string)null), SRCategory("Custom"), SRDescription("Condition")]
        public ActivityCondition Condition
        {
            get
            {
                return (base.GetValue(ConditionProperty) as ActivityCondition);
            }
            set
            {
                base.SetValue(ConditionProperty, value);
            }
        }


        protected override ActivityExecutionStatus HandleFault(ActivityExecutionContext executionContext, Exception exception)
        {
            return ActivityExecutionStatus.Closed;
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
    }


    /// <summary>
    /// 定义条件分支设计器
    /// </summary>
    [ActivityDesignerTheme(typeof(ConditionedDesignerTheme))]
    internal sealed class LWIfElseBranchDesigner : SequentialActivityDesigner
    {
        public override bool CanBeParentedTo(CompositeActivityDesigner parentActivityDesigner)
        {
            if (parentActivityDesigner == null)
            {
                throw new ArgumentNullException("parentActivity");
            }
            return ((parentActivityDesigner.Activity is LWIfElseActivity) && base.CanBeParentedTo(parentActivityDesigner));
        }

        public override string Text
        {
            get
            {
                return string.IsNullOrEmpty(Activity.Description) ? SR.GetString("Subbranch","Child Branch") : Activity.Description;
            }
            protected set
            {
                base.Text = value;
            }
        }
    }

    /// <summary>
    /// 定义设计器主题
    /// </summary>
    internal sealed class ConditionedDesignerTheme : CompositeDesignerTheme
    {
        public ConditionedDesignerTheme(WorkflowTheme theme)
            : base(theme)
        {
            this.ShowDropShadow = false;
            this.ConnectorStartCap = LineAnchor.None;
            this.ConnectorEndCap = LineAnchor.ArrowAnchor;
            this.ForeColor = Color.FromArgb(0xff, 0, 100, 0);
            this.BorderColor = Color.FromArgb(0xff, 0xe0, 0xe0, 0xe0);
            this.BorderStyle = DashStyle.Dash;
            this.BackColorStart = Color.FromArgb(0, 0, 0, 0);
            this.BackColorEnd = Color.FromArgb(0, 0, 0, 0);
        }
    }
}
