using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing.Design;
using System.Reflection;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Compiler;
using ICP.WF.Activities.Common;

namespace ICP.WF.Activities
{
    internal class ActivityBindPropertyDescriptor : DynamicPropertyDescriptor
    {
        private object propertyOwner;

        internal ActivityBindPropertyDescriptor(IServiceProvider serviceProvider, PropertyDescriptor realPropertyDescriptor, object propertyOwner)
            : base(serviceProvider, realPropertyDescriptor)
        {
            this.propertyOwner = propertyOwner;
        }

        internal static MemberInfo FindMatchingMember(string name, Type ownerType, bool ignoreCase)
        {
            foreach (MemberInfo info2 in ownerType.GetMembers(BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance))
            {
                if (info2.Name.Equals(name, ignoreCase ? ((StringComparison)5) : ((StringComparison)4)))
                {
                    return info2;
                }
            }
            return null;
        }

        internal static IList<MemberInfo> GetBindableMembers(object obj, ITypeDescriptorContext context)
        {
            List<MemberInfo> list = new List<MemberInfo>();
            IDesignerHost service = context.GetService(typeof(IDesignerHost)) as IDesignerHost;
            Activity activity = (service != null) ? (service.RootComponent as Activity) : null;
            Type type = (obj == activity) ? WFHelpers.GetDataSourceClass(activity, context) : obj.GetType();
            Type toType = WFPropertyDescriptorUtils.GetBaseType(context.PropertyDescriptor, context.Instance, context);
            if ((type != null) && (toType != null))
            {
                DependencyProperty property = DependencyProperty.FromName(context.PropertyDescriptor.Name, context.PropertyDescriptor.ComponentType);
                bool flag = (property != null) && property.IsEvent;
                BindingFlags bindingAttr = BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance;
                if (obj == activity)
                {
                    bindingAttr |= BindingFlags.NonPublic;
                }
                foreach (MemberInfo info in type.GetMembers(bindingAttr))
                {
                    object[] customAttributes = info.GetCustomAttributes(typeof(DebuggerNonUserCodeAttribute), false);
                    if (((customAttributes == null) || (customAttributes.Length <= 0)) || !(customAttributes[0] is DebuggerNonUserCodeAttribute))
                    {
                        object[] objArray2 = info.GetCustomAttributes(typeof(BrowsableAttribute), false);
                        if (objArray2.Length > 0)
                        {
                            bool browsable = false;
                            BrowsableAttribute attribute = objArray2[0] as BrowsableAttribute;
                            if (attribute != null)
                            {
                                browsable = attribute.Browsable;
                            }
                            else
                            {
                                try
                                {
                                    AttributeInfoAttribute attribute2 = objArray2[0] as AttributeInfoAttribute;
                                    if ((attribute2 != null) && (attribute2.AttributeInfo.ArgumentValues.Count > 0))
                                    {
                                        browsable = (bool)attribute2.AttributeInfo.GetArgumentValueAs(context, 0, typeof(bool));
                                    }
                                }
                                catch
                                {
                                }
                            }
                            if (!browsable)
                            {
                                goto Label_035E;
                            }
                        }
                        if ((info.DeclaringType != typeof(object)) || (!string.Equals(info.Name, "Equals", StringComparison.Ordinal) && !string.Equals(info.Name, "ReferenceEquals", StringComparison.Ordinal)))
                        {
                            bool flag3 = false;
                            bool flag4 = false;
                            bool isAssembly = false;
                            if (flag && (info is EventInfo))
                            {
                                EventInfo info2 = info as EventInfo;
                                MethodInfo addMethod = info2.GetAddMethod();
                                MethodInfo removeMethod = info2.GetRemoveMethod();
                                flag4 = ((((addMethod != null) && addMethod.IsFamily) || ((removeMethod != null) && removeMethod.IsFamily)) || ((addMethod != null) && addMethod.IsPublic)) || ((removeMethod != null) && removeMethod.IsPublic);
                                isAssembly = ((addMethod != null) && addMethod.IsAssembly) || ((removeMethod != null) && removeMethod.IsAssembly);
                                flag3 = TypeProvider.IsAssignable(toType, info2.EventHandlerType);
                            }
                            else if (info is FieldInfo)
                            {
                                FieldInfo info5 = info as FieldInfo;
                                flag4 = info5.IsFamily || info5.IsPublic;
                                isAssembly = info5.IsAssembly;
                                flag3 = TypeProvider.IsAssignable(toType, info5.FieldType);
                            }
                            else if (info is PropertyInfo)
                            {
                                PropertyInfo info6 = info as PropertyInfo;
                                MethodInfo getMethod = info6.GetGetMethod();
                                MethodInfo setMethod = info6.GetSetMethod();
                                flag4 = ((((getMethod != null) && getMethod.IsFamily) || ((setMethod != null) && setMethod.IsFamily)) || ((getMethod != null) && getMethod.IsPublic)) || ((setMethod != null) && setMethod.IsPublic);
                                isAssembly = ((getMethod != null) && getMethod.IsAssembly) || ((setMethod != null) && setMethod.IsAssembly);
                                flag3 = (getMethod != null) && TypeProvider.IsAssignable(toType, info6.PropertyType);
                            }
                            if (((info.DeclaringType != type) && !flag4) && ((info.DeclaringType.Assembly != null) || !isAssembly))
                            {
                                flag3 = false;
                            }
                            if (flag3)
                            {
                                list.Add(info);
                            }
                        }
                    Label_035E: ;
                    }
                }
            }
            return list.AsReadOnly();
        }

        public override object GetEditor(Type editorBaseType)
        {
            object editor = base.GetEditor(editorBaseType);
            if ((editorBaseType != typeof(UITypeEditor)) || this.IsReadOnly)
            {
                return editor;
            }
            object obj3 = (this.PropertyOwner != null) ? this.GetValue(this.PropertyOwner) : null;
            bool propertiesSupported = base.RealPropertyDescriptor.Converter.GetPropertiesSupported((this.PropertyOwner != null) ? new TypeDescriptorContext(base.ServiceProvider, base.RealPropertyDescriptor, this.PropertyOwner) : null);
            if (!(obj3 is ActivityBind) && ((editor != null) || propertiesSupported))
            {
                return editor;
            }
            return new BindUITypeEditor();
        }

        public override object GetValue(object component)
        {
            object binding = null;
            DependencyObject obj3 = component as DependencyObject;
            DependencyProperty dependencyProperty = DependencyProperty.FromName(this.Name, this.ComponentType);
            if (((obj3 != null) && (dependencyProperty != null)) && obj3.IsBindingSet(dependencyProperty))
            {
                binding = obj3.GetBinding(dependencyProperty);
            }
            if (!(binding is ActivityBind))
            {
                binding = base.GetValue(component);
            }
            return binding;
        }

        internal static bool IsBindableProperty(PropertyDescriptor propertyDescriptor)
        {
            if (propertyDescriptor.PropertyType == typeof(ActivityBind))
            {
                return true;
            }
            if (propertyDescriptor.Converter is ActivityBindTypeConverter)
            {
                return true;
            }
            DependencyProperty property = DependencyProperty.FromName(propertyDescriptor.Name, propertyDescriptor.ComponentType);
            return ((typeof(DependencyObject).IsAssignableFrom(propertyDescriptor.ComponentType) && (property != null)) && !property.DefaultMetadata.IsMetaProperty);
        }

        public override void SetValue(object component, object value)
        {
            object obj2 = this.GetValue(component);
            ActivityBind bind = value as ActivityBind;
            DependencyObject obj3 = component as DependencyObject;
            DependencyProperty dependencyProperty = DependencyProperty.FromName(this.Name, this.ComponentType);
            if (((obj3 != null) && (dependencyProperty != null)) && (bind != null))
            {
                using (new ComponentChangeDispatcher(base.ServiceProvider, obj3, this))
                {
                    if (dependencyProperty.IsEvent && (base.ServiceProvider != null))
                    {
                        IEventBindingService service = base.ServiceProvider.GetService(typeof(IEventBindingService)) as IEventBindingService;
                        if ((service != null) && (service.GetEvent(base.RealPropertyDescriptor) != null))
                        {
                            base.RealPropertyDescriptor.SetValue(component, null);
                        }
                    }
                    obj3.SetBinding(dependencyProperty, bind);
                    base.OnValueChanged(obj3, EventArgs.Empty);
                    goto Label_00F8;
                }
            }
            if (((obj3 != null) && (dependencyProperty != null)) && obj3.IsBindingSet(dependencyProperty))
            {
                using (new ComponentChangeDispatcher(base.ServiceProvider, obj3, this))
                {
                    obj3.RemoveProperty(dependencyProperty);
                    base.OnValueChanged(obj3, EventArgs.Empty);
                }
            }
            base.SetValue(component, value);
        Label_00F8:
            if ((obj2 != value) && (((obj2 is ActivityBind) && !(value is ActivityBind)) || (!(obj2 is ActivityBind) && (value is ActivityBind))))
            {
                TypeDescriptor.Refresh(component);
            }
        }

        public override AttributeCollection Attributes
        {
            get
            {
                List<Attribute> list = new List<Attribute>();
                foreach (Attribute attribute in base.Attributes)
                {
                    list.Add(attribute);
                }
                object editor = base.RealPropertyDescriptor.GetEditor(typeof(UITypeEditor));
                object obj3 = (this.PropertyOwner != null) ? this.GetValue(this.PropertyOwner) : null;
                bool propertiesSupported = base.RealPropertyDescriptor.Converter.GetPropertiesSupported((this.PropertyOwner != null) ? new TypeDescriptorContext(base.ServiceProvider, base.RealPropertyDescriptor, this.PropertyOwner) : null);
                if ((((editor == null) && !propertiesSupported) || (obj3 is ActivityBind)) && !this.IsReadOnly)
                {
                    list.Add(new EditorAttribute(typeof(BindUITypeEditor), typeof(UITypeEditor)));
                }
                return new AttributeCollection(list.ToArray());
            }
        }

        public override TypeConverter Converter
        {
            get
            {
                TypeConverter converter = base.Converter;
                if (typeof(ActivityBindTypeConverter).IsAssignableFrom(converter.GetType()))
                {
                    return converter;
                }
                return new ActivityBindTypeConverter();
            }
        }

        public override bool IsReadOnly
        {
            get
            {
                return base.RealPropertyDescriptor.IsReadOnly;
            }
        }

        internal object PropertyOwner
        {
            get
            {
                return this.propertyOwner;
            }
        }
    }
}
