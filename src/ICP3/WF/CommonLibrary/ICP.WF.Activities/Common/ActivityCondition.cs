using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Drawing.Design;

namespace ICP.WF.Activities
{
    [TypeConverter(typeof(ICPConditionTypeConverter)), ActivityValidator(typeof(ConditionValidator)), DesignerSerializer(typeof(WorkflowMarkupSerializer), typeof(WorkflowMarkupSerializer)), DesignerSerializer(typeof(DependencyObjectCodeDomSerializer), typeof(CodeDomSerializer)), Browsable(true), MergableProperty(false)]
    public abstract class ActivityCondition : DependencyObject
    {

        protected ActivityCondition()
        {
        }

        public abstract bool Evaluate(Activity activity, IServiceProvider provider);
    }
}
