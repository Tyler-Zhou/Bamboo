using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace ICP.WF.Activities
{
    /// <summary>
    /// 提供有关组件的上下文信息，如其容器和属性描述符(用于类型转换中)
    /// </summary>
    internal sealed class TypeDescriptorContext : ITypeDescriptorContext, IServiceProvider
    {
        private object instance;
        private PropertyDescriptor propDesc;
        private IServiceProvider serviceProvider;

        public TypeDescriptorContext(IServiceProvider serviceProvider, PropertyDescriptor propDesc, object instance)
        {
            this.serviceProvider = serviceProvider;
            this.propDesc = propDesc;
            this.instance = instance;
        }

        public object GetService(Type serviceType)
        {
            return this.serviceProvider.GetService(serviceType);
        }

        public void OnComponentChanged()
        {
            IComponentChangeService service = (IComponentChangeService)this.serviceProvider.GetService(typeof(IComponentChangeService));
            if (service != null)
            {
                service.OnComponentChanged(this.instance, this.propDesc, null, null);
            }
        }

        public bool OnComponentChanging()
        {
            IComponentChangeService service = (IComponentChangeService)this.serviceProvider.GetService(typeof(IComponentChangeService));
            if (service != null)
            {
                try
                {
                    service.OnComponentChanging(this.instance, this.propDesc);
                }
                catch (CheckoutException exception)
                {
                    if (exception != CheckoutException.Canceled)
                    {
                        throw exception;
                    }
                    return false;
                }
            }
            return true;
        }

        public IContainer Container
        {
            get
            {
                return (IContainer)this.serviceProvider.GetService(typeof(IContainer));
            }
        }

        public object Instance
        {
            get
            {
                return this.instance;
            }
        }

        public PropertyDescriptor PropertyDescriptor
        {
            get
            {
                return this.propDesc;
            }
        }
    }
}
