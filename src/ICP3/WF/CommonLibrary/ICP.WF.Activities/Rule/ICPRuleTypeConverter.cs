using System;
using System.CodeDom;
using System.ComponentModel;

namespace ICP.WF.Activities
{

    internal class ICPRuleTypeConverter : TypeConverter
    {
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            PropertyDescriptorCollection descriptors = new PropertyDescriptorCollection(null);
            descriptors.Add(new ICPNamePropertyDescriptor(context, TypeDescriptor.CreateProperty(typeof(ICPActivityCondition), "ConditionName", typeof(string), new Attribute[] { new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content), DesignOnlyAttribute.Yes })));
            return descriptors.Sort(new string[] { "ConditionName" });
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
