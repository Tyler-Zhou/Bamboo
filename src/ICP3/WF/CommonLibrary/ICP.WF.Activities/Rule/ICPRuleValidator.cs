using System;
using System.Globalization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Design;

namespace ICP.WF.Activities
{

    internal sealed class ICPRuleValidator : ConditionValidator
    {
        public override ValidationErrorCollection Validate(ValidationManager manager, object obj)
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            if (manager.Context == null)
            {
                throw new InvalidOperationException(SR.ContextStackMissing);
            }
            ValidationErrorCollection errors = new ValidationErrorCollection();
            ICPActivityCondition reference = obj as ICPActivityCondition;
            if (reference == null)
            {
                throw new ArgumentException("obj");
            }
            Activity activity = manager.Context[typeof(Activity)] as Activity;
            if (activity == null)
            {
                throw new InvalidOperationException();
            }
            if (!(manager.Context[typeof(PropertyValidationContext)] is PropertyValidationContext))
            {
                throw new InvalidOperationException();
            }
            if (!string.IsNullOrEmpty(reference.ConditionName))
            {
                return errors;
            }
            ValidationError error6 = new ValidationError(string.Format(CultureInfo.CurrentCulture, SR.InvalidConditionName, new object[] { "ConditionName" }), 0x540);
            error6.PropertyName = base.GetFullPropertyName(manager) + ".ConditionName";
            errors.Add(error6);
            return errors;
        }
    }
}
