using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;

namespace ICP.WF.Activities
{
    internal class ICPNamePropertyDescriptor : DynamicPropertyDescriptor
    {
        public ICPNamePropertyDescriptor(IServiceProvider serviceProvider, PropertyDescriptor descriptor)
            : base(serviceProvider, descriptor)
        {
        }

        public override object GetEditor(Type editorBaseType)
        {
            new SecurityPermission(PermissionState.Unrestricted).Demand();
            return new WFConditionNameEditor();
        }

        public override object GetValue(object component)
        {
            if (component == null)
            {
                throw new ArgumentNullException("component");
            }
            ICPActivityCondition reference = component as ICPActivityCondition;
            if (reference == null)
            {
                throw new ArgumentException("component");
            }
            if (reference.ConditionName != null)
            {
                return reference.ConditionName;
            }
            return null;
        }

        public override void SetValue(object component, object value)
        {
            if (component == null)
            {
                throw new ArgumentNullException("component");
            }
            if (!(component is ICPActivityCondition))
            {
                throw new ArgumentException("component");
            }
            string str = value as string;
            if ((str == null) || (str.TrimEnd(new char[0]).Length == 0))
            {
                str = string.Empty;
            }
            ISite serviceProvider = WFPropertyDescriptorUtils.GetSite(base.ServiceProvider, component);
            if (serviceProvider == null)
            {
                throw new InvalidOperationException();
            }
            //WFRuleConditionCollection conditions = null;
            //RuleDefinitions definitions = WFConditionHelper.Load_Rules_DT(serviceProvider, WFHelpers.GetRootActivity(serviceProvider.Component as Activity));
            //if (definitions != null)
            //{
            //    conditions = definitions.Conditions;
            //}
            //if (((conditions != null) && (str.Length != 0)) && !conditions.Contains(str))
            //{
            //    RuleExpressionCondition item = new RuleExpressionCondition();
            //    item.Name = str;
            //    conditions.Add(item);
            //    WFConditionHelper.Flush_Rules_DT(serviceProvider, WFHelpers.GetRootActivity(serviceProvider.Component as Activity));
            //}
            PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)["ConditionName"];
            if (propertyDescriptor != null)
            {
                WFPropertyDescriptorUtils.SetPropertyValue(serviceProvider, propertyDescriptor, component, str);
            }
        }

        public override string Description
        {
            get
            {
                return SR.NamePropertyDescription;
            }
        }

        public override bool IsReadOnly
        {
            get
            {
                return false;
            }
        }
    }
}
