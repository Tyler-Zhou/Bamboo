using System;
using System.ComponentModel;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using ICP.WF.Activities.Common;
namespace ICP.WF.Activities
{
    internal sealed class IDPropertyDescriptor : DynamicPropertyDescriptor
    {
        internal IDPropertyDescriptor(IServiceProvider serviceProvider, PropertyDescriptor actualPropDesc)
            : base(serviceProvider, actualPropDesc)
        {
        }

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override void SetValue(object component, object value)
        {
            Activity activity = component as Activity;
            if (activity != null)
            {
                ISite site = WFPropertyDescriptorUtils.GetSite(base.ServiceProvider, component);
                if (site == null)
                {
                    throw new Exception(SR.GetString("General_MissingService", new object[] { typeof(ISite).FullName }));
                }
                IIdentifierCreationService service = site.GetService(typeof(IIdentifierCreationService)) as IIdentifierCreationService;
                if (service == null)
                {
                    throw new Exception(SR.GetString("General_MissingService", new object[] { typeof(IIdentifierCreationService).FullName }));
                }
                string identifier = value as string;
                service.ValidateIdentifier(activity, identifier);
                WFHelpers.UpdateSiteName(activity, identifier);
                base.SetValue(component, value);
            }
        }
    }
}
