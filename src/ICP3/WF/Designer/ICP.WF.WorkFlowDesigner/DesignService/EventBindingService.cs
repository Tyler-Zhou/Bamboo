using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Reflection;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Serialization;


namespace ICP.WF.WorkFlowDesigner
{
    /// <summary>
    ///  为注册组件事件的事件处理程序提供服务。

    /// </summary>
    internal class EventBindingService : IEventBindingService
    {
        #region 变量定义区


        private WorkflowLoader loader;              //
        private IServiceProvider serviceProvider;   // 获取指定类型的服务对象。


        #endregion

        #region 初始化区

        public EventBindingService(IServiceProvider provider, WorkflowLoader loader)
        {
            this.loader = loader;
            this.serviceProvider = provider;
        }
        #endregion

        #region IEventBindingService接口实现区


        /// <summary>
        /// 为指定组件和事件的事件处理程序方法创建唯一的名称。

        /// </summary>
        /// <param name="component">事件连接到的组件实例</param>
        /// <param name="e">要为其创建名称的事件</param>
        /// <returns>为此事件的事件处理程序方法建议的名称</returns>
        public string CreateUniqueMethodName(IComponent component, EventDescriptor e)
        {
            return e.DisplayName;
        }

       
        /// <summary>
        /// 获取其方法签名与指定事件兼容的事件处理程序方法的集合
        /// </summary>
        /// <param name="e">要为其获取兼容事件处理程序方法的事件</param>
        /// <returns>字符串的集合</returns>
        public ICollection GetCompatibleMethods(EventDescriptor e)
        {
            return new ArrayList();
        }

        
        /// <summary>
        /// 为指定属性说明符（如果它表示事件）所表示的事件获取 System.ComponentModel.EventDescriptor。

        /// </summary>
        /// <param name="property">表示事件的属性</param>
        /// <returns>属性所表示的事件的 System.ComponentModel.EventDescriptor，或者在属性不表示事件的情况下为 null。</returns>
        public EventDescriptor GetEvent(PropertyDescriptor property)
        {
            return (property is EventPropertyDescriptor) ? ((EventPropertyDescriptor)property).EventDescriptor : null;
        }


        /// <summary>
        /// 将一组事件说明符转换为一组属性说明符
        /// </summary>
        /// <param name="events">要转换为属性的事件</param>
        /// <returns>对事件集进行描述的 System.ComponentModel.PropertyDescriptor 对象数组。</returns>
        public PropertyDescriptorCollection GetEventProperties(EventDescriptorCollection events)
        {
            return new PropertyDescriptorCollection(new PropertyDescriptor[] { }, true);
        }


        /// <summary>
        /// 将单个事件说明符转换为属性说明符
        /// </summary>
        /// <param name="e">要转换的事件</param>
        /// <returns>对事件进行描述的 System.ComponentModel.PropertyDescriptor。</returns>
        public PropertyDescriptor GetEventProperty(EventDescriptor e)
        {
            return new EventPropertyDescriptor(e, this, this.serviceProvider);
        }

        /// <summary>
        /// 显示设计器的用户代码
        /// </summary>
        /// <returns> 如果显示该代码，则为 true，否则为 false。</returns>
        public bool ShowCode()
        {
            return false;
        }


        /// <summary>
        /// 在指定行显示设计器的用户代码
        /// </summary>
        /// <param name="lineNumber">要放置插入符号的行号</param>
        /// <returns>如果显示该代码，则为 true，否则为 false。</returns>
        public bool ShowCode(int lineNumber)
        {
            return false;
        }

        /// <summary>
        /// 显示指定事件的用户代码

        /// </summary>
        /// <param name="component">事件要连接到组件</param>
        /// <param name="e">要显示的事件</param>
        /// <returns>如果显示该代码，则为 true，否则为 false。</returns>
        public bool ShowCode(IComponent component, EventDescriptor e)
        {
            return false;
        }

        #endregion


        #region 本地方法

        protected void UseMethod(IComponent component, EventDescriptor e, string methodName)
        {
          
        }

        protected void FreeMethod(IComponent component, EventDescriptor e, string methodName)
        {

        }

        #endregion

        /// <summary>
        ///类上的属性的抽象化的实现。

        /// </summary>
        private class EventPropertyDescriptor : PropertyDescriptor
        {
            private EventDescriptor eventDescriptor;
            private IServiceProvider serviceProvider;
            private EventBindingService eventSvc;
            private TypeConverter converter;
            private bool useMethodCalled = false;

            public EventDescriptor EventDescriptor
            {
                get
                {
                    return this.eventDescriptor;
                }
            }

            public EventPropertyDescriptor(EventDescriptor eventDesc, EventBindingService eventSvc, IServiceProvider serviceProvider)
                : base(eventDesc, null)
            {
                this.eventDescriptor = eventDesc;
                this.eventSvc = eventSvc;
                this.serviceProvider = serviceProvider;
            }

            public override Type ComponentType
            {
                get
                {
                    return this.eventDescriptor.ComponentType;
                }
            }

            /// <summary>
            /// 获取属性的类型。

            /// </summary>
            public override Type PropertyType
            {
                get
                {
                    return this.eventDescriptor.EventType;
                }
            }

            /// <summary>
            /// 包含此属性的 TypeConverter。

            /// </summary>
            public override TypeConverter Converter
            {
                get
                {
                    if (this.converter == null)
                    {
                        this.converter = new XomlEventConverter(this.eventDescriptor);
                    }

                    return this.converter;
                }
            }

            /// <summary>
            /// 指示该属性是否是只读的。

            /// </summary>
            public override bool IsReadOnly
            {
                get
                {
                    return false;
                }
            }

            /// <summary>
            /// 指示重置组件是否会更改该组件的值。

            /// </summary>
            /// <param name="component"></param>
            /// <returns></returns>
            public override bool CanResetValue(object component)
            {
                return false;
            }

            /// <summary>
            /// 返回组件上属性的当前值。

            /// </summary>
            /// <param name="component"></param>
            /// <returns></returns>
            public override object GetValue(object component)
            {
                string value = null;
                DependencyObject dependencyObject = component as DependencyObject;
                if (dependencyObject != null)
                {
                    Hashtable dynamicEvents = dependencyObject.GetValue(WorkflowMarkupSerializer.EventsProperty) as Hashtable;
                    if (dynamicEvents != null)
                    {
                        if (dynamicEvents.ContainsKey(this.eventDescriptor.Name))
                        {
                            value = dynamicEvents[this.eventDescriptor.Name] as string;
                        }
                    }
                    else
                    {
                        DependencyProperty dependencyEvent = DependencyProperty.FromName(this.eventDescriptor.Name, dependencyObject.GetType());
                        MethodInfo getInvocationListMethod = dependencyObject.GetType().GetMethod("System.Workflow.ComponentModel.IDependencyObjectAccessor.GetInvocationList", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                        if (getInvocationListMethod != null && dependencyEvent != null && dependencyEvent.IsEvent)
                        {
                            MethodInfo boundGetInvocationListMethod = getInvocationListMethod.MakeGenericMethod(new Type[] { dependencyEvent.PropertyType });
                            if (boundGetInvocationListMethod != null)
                            {
                                Delegate[] delegates = boundGetInvocationListMethod.Invoke(dependencyObject, new object[] { dependencyEvent }) as Delegate[];
                                if (delegates != null && delegates.Length > 0 && delegates[0].Method != null)
                                {
                                    value = delegates[0].Method.Name;
                                }
                            }
                        }
                    }
                }

                return value;
            }

            /// <summary>
            /// 重置组件属性的值。

            /// </summary>
            /// <param name="component"></param>
            public override void ResetValue(object component)
            {
                SetValue(component, null);
            }

            /// <summary>
            /// 将组件的值设置为一个不同的值。

            /// </summary>
            /// <param name="component"></param>
            /// <param name="value"></param>
            public override void SetValue(object component, object value)
            {
                if (IsReadOnly)
                {
                    throw new ArgumentException(this.eventDescriptor.Name);
                }

                if (value != null && !(value is string))
                {
                    throw new ArgumentException(this.eventDescriptor.Name);
                }

                if (component is DependencyObject)
                {
                    string name = value as string;
                    DependencyObject dependencyObject = component as DependencyObject;
                    string oldName = null;
                    if (dependencyObject.GetValue(WorkflowMarkupSerializer.EventsProperty) == null)
                    {
                        dependencyObject.SetValue(WorkflowMarkupSerializer.EventsProperty, new Hashtable());
                    }

                    Hashtable dynamicEvents = dependencyObject.GetValue(WorkflowMarkupSerializer.EventsProperty) as Hashtable;
                    if (dynamicEvents.ContainsKey(this.eventDescriptor.Name))
                    {
                        oldName = dynamicEvents[this.eventDescriptor.Name] as string;
                    }

                    if (oldName != null && name != null && oldName.Equals(name, StringComparison.Ordinal))
                    {
                        foreach (string methodName in this.eventSvc.GetCompatibleMethods(this.eventDescriptor))
                        {
                            if (methodName.Equals(name, StringComparison.CurrentCulture))
                            {
                                return;
                            }
                        }
                    }
                    else if (oldName == name)
                    {
                        return;
                    }

                    IDesignerHost host = this.serviceProvider.GetService(typeof(IDesignerHost)) as IDesignerHost;
                    if (host == null)
                    {
                        throw new InvalidOperationException(typeof(IDesignerHost).FullName);
                    }

                    if (!String.IsNullOrEmpty(name))
                    {
                        if (name.StartsWith("@"))
                        {
                            throw new InvalidOperationException(name);
                        }

                        Activity rootActivity = host.RootComponent as Activity;
                        if (rootActivity != null)
                        {
                            MemberInfo matchingMember = null;
                            Type designedType = Helpers.GetDataSourceClass(rootActivity, this.serviceProvider);
                            if (designedType != null)
                            {
                                WorkflowLoader loader = this.serviceProvider.GetService(typeof(WorkflowLoader)) as WorkflowLoader;
                                foreach (MemberInfo memberInfo in designedType.GetMembers(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                                {
                                    if (memberInfo.Name.Equals(name, StringComparison.Ordinal))
                                    {
                                        matchingMember = memberInfo;
                                        break;
                                    }
                                }
                            }

                            if (matchingMember != null)
                            {
                                if (!(matchingMember is MethodInfo))
                                {
                                    throw new InvalidOperationException(designedType.FullName);
                                }
                            }
                            else
                            {
                                IIdentifierCreationService idService = this.serviceProvider.GetService(typeof(IIdentifierCreationService)) as IIdentifierCreationService;
                                if (idService != null)
                                {
                                    idService.ValidateIdentifier(rootActivity, name);
                                }
                            }
                        }
                    }

                    DesignerTransaction trans = null;
                    if (host != null)
                    {
                        trans = host.CreateTransaction(name);
                    }

                    try
                    {
                        IComponentChangeService change = this.serviceProvider.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                        if (change != null)
                        {
                            try
                            {
                                change.OnComponentChanging(component, this);
                                change.OnComponentChanging(component, this.eventDescriptor);
                            }
                            catch (CheckoutException coEx)
                            {
                                if (coEx == CheckoutException.Canceled)
                                {
                                    return;
                                }
                                throw;
                            }
                        }

                        if (name != null)
                        {
                            if (host.RootComponent != null)
                            {
                                if (!this.useMethodCalled && !string.IsNullOrEmpty(oldName))
                                {
                                    eventSvc.UseMethod((IComponent)component, eventDescriptor, oldName);
                                }

                                eventSvc.UseMethod((IComponent)component, eventDescriptor, name);
                                this.useMethodCalled = true;
                            }
                        }

                        if (oldName != null && host.RootComponent != null)
                        {
                            eventSvc.FreeMethod((IComponent)component, eventDescriptor, oldName);
                        }

                        dynamicEvents[this.eventDescriptor.Name] = name;

                        if (change != null)
                        {
                            change.OnComponentChanged(component, this.eventDescriptor, null, null);
                            change.OnComponentChanged(component, this, oldName, name);
                        }

                        OnValueChanged(component, EventArgs.Empty);

                        if (trans != null)
                        {
                            trans.Commit();
                        }
                    }
                    finally
                    {
                        if (trans != null)
                        {
                            ((IDisposable)trans).Dispose();
                        }
                    }
                }
            }

            /// <summary>
            /// 指示是否需要持久保存该属性的值。

            /// </summary>
            /// <param name="component"></param>
            /// <returns></returns>
            public override bool ShouldSerializeValue(object component)
            {
                return true;
            }

            /// <summary>
            /// 提供一种将值的类型转换为其他类型以及访问标准值和子属性的统一方法。

            /// </summary>
            private class XomlEventConverter : TypeConverter
            {

                private EventDescriptor evt;

                internal XomlEventConverter(EventDescriptor evt)
                {
                    this.evt = evt;
                }

                /// <summary>
                /// 返回该转换器是否可以将一种类型的对象转换为此转换器的类型。

                /// </summary>
                /// <param name="context"></param>
                /// <param name="sourceType"></param>
                /// <returns></returns>
                public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
                {
                    if (sourceType == typeof(string))
                    {
                        return true;
                    }

                    return base.CanConvertFrom(context, sourceType);
                }

                /// <summary>
                /// 返回此转换器是否可将该对象转换为指定的类型。

                /// </summary>
                /// <param name="context"></param>
                /// <param name="destinationType"></param>
                /// <returns></returns>
                public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
                {
                    if (destinationType == typeof(string))
                    {
                        return true;
                    }

                    return base.CanConvertTo(context, destinationType);
                }

                /// <summary>
                /// 将给定值转换为此转换器的类型。

                /// </summary>
                /// <param name="context"></param>
                /// <param name="culture"></param>
                /// <param name="value"></param>
                /// <returns></returns>
                public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
                {
                    if (value == null)
                    {
                        return value;
                    }

                    if (value is string)
                    {
                        if (((string)value).Length == 0)
                        {
                            return null;
                        }

                        return value;
                    }
                    return base.ConvertFrom(context, culture, value);
                }

                /// <summary>
                /// 将给定值对象转换为指定的类型。

                /// </summary>
                /// <param name="context"></param>
                /// <param name="culture"></param>
                /// <param name="value"></param>
                /// <param name="destinationType"></param>
                /// <returns></returns>
                public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
                {
                    if (destinationType == typeof(string))
                    {
                        return (value == null || !(value is string)) ? string.Empty : value;
                    }

                    return base.ConvertTo(context, culture, value, destinationType);
                }

                /// <summary>
                /// 返回此类型转换器设计用于的数据类型的标准值集合。

                /// </summary>
                /// <param name="context"></param>
                /// <returns></returns>
                public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
                {
                    string[] eventMethods = null;

                    if (context != null)
                    {
                        IEventBindingService ebs = (IEventBindingService)context.GetService(typeof(IEventBindingService));
                        if (ebs != null)
                        {
                            try
                            {
                                ICollection methods = ebs.GetCompatibleMethods(evt);
                                eventMethods = new string[methods.Count];
                                int i = 0;
                                foreach (string s in methods)
                                {
                                    eventMethods[i++] = s;
                                }
                            }
                            catch (Exception)
                            {
                                
                            }
                        }
                    }

                    return new StandardValuesCollection(eventMethods);
                }

                /// <summary>
                /// 返回从 GetStandardValues 返回的标准值的集合是否为独占列表。

                /// </summary>
                /// <param name="context"></param>
                /// <returns></returns>
                public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
                {
                    return false;
                }

                /// <summary>
                /// 返回此对象是否支持可以从列表中选取的标准值集。

                /// </summary>
                /// <param name="context"></param>
                /// <returns></returns>
                public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
                {
                    return true;
                }
            }
        }
    }
}
