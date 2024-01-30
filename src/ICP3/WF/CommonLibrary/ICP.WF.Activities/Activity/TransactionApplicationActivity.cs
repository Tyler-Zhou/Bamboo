using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace ICP.WF.Activities
{
    /// <summary>
    /// 封装了对申请活动事务处理的版本
    /// </summary>
    //[Designer(typeof(TransactionApplicationActivityDesigner), typeof(IDesigner))]
    public partial class TransactionApplicationActivity: SequenceActivity
	{
		public TransactionApplicationActivity()
		{
            
			InitializeComponent();
		}
	}


    /// <summary>
    ///审批活动设计器组件
    /// </summary>
    public class TransactionApplicationActivityDesigner : CompositeActivityDesigner
    {

        public override bool CanExpandCollapse
        {
            get
            {
                return true;
            }
        }

        public override bool CanInsertActivities(HitTestInfo insertLocation, System.Collections.ObjectModel.ReadOnlyCollection<Activity> activitiesToInsert)
        {
            return false;
        }

        public override bool CanMoveActivities(HitTestInfo moveLocation, System.Collections.ObjectModel.ReadOnlyCollection<Activity> activitiesToMove)
        {
            return false;
        }

        public override bool CanRemoveActivities(System.Collections.ObjectModel.ReadOnlyCollection<Activity> activitiesToRemove)
        {
            return false;
        }

        protected override void OnPaint(ActivityDesignerPaintEventArgs e)
        {
            base.OnPaint(e);

            DrawCustomActivity(e);
        }

        private void DrawCustomActivity(ActivityDesignerPaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            ActivityDesignerTheme compositeDesignerTheme = e.DesignerTheme;
            ActivityDesignerPaint.DrawRoundedRectangle(graphics, compositeDesignerTheme.BorderPen, this.Bounds, compositeDesignerTheme.BorderWidth);

            string text = string.Empty;
            TransactionApplicationActivity activity = this.Activity as TransactionApplicationActivity;
            if (activity != null)
            {
                ApplicationActivity child = (ApplicationActivity)activity.GetActivityByName(typeof(ApplicationActivity).FullName);
                if (child != null && this.Expanded == false)
                {
                    text = string.IsNullOrEmpty(child.CName) ? child.EName : child.CName;
                }
            }

            Rectangle textRectangle = this.TextRectangle;
            if (!string.IsNullOrEmpty(text) && !textRectangle.IsEmpty)
            {
                ActivityDesignerPaint.DrawText(graphics, compositeDesignerTheme.Font, text, textRectangle, StringAlignment.Center, e.AmbientTheme.TextQuality, compositeDesignerTheme.ForegroundBrush);
            }

            System.Drawing.Image image = this.Image;
            Rectangle imageRectangle = this.ImageRectangle;
            if (image != null && !imageRectangle.IsEmpty)
            {
                ActivityDesignerPaint.DrawImage(graphics, image, imageRectangle, DesignerContentAlignment.Fill);
            }
        }

    }
}
