

//-----------------------------------------------------------------------
// <copyright file="DataBindingConvert.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.Controls
{
    using System.ComponentModel;

    public class DataPropertyBindingConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }


        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            LWBaseForm v = (LWBaseForm)context.Container.Components["WFForm1"];
           // WFForm v = (WFForm)context.GetService(typeof(WFForm));
            if (v == null) return new StandardValuesCollection(new string[] { });

            return new StandardValuesCollection(v.GetFields());
        }


        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }


    }

    public class DataBindingControlPropertyConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }


        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            IBindingService service = context.Instance as IBindingService;
            if (service != null)
            {
                return new StandardValuesCollection(service.GetCanBindingControlProperty());
            }
            else
            {
                return new StandardValuesCollection(new string[] { });
            }
        }


        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }

    }





    public class DataTableNameConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }


        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            LWBaseForm v = (LWBaseForm)context.Container.Components["WFForm1"];
            if (v == null) return new StandardValuesCollection(new string[] { });


            return new StandardValuesCollection(v.GetTableNames());
        }


        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }
    }


    public class RadionGroupSelectedTextConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }


        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            LWRadioGroup v = (LWRadioGroup)context.Instance;
            if (v == null) return new StandardValuesCollection(new string[] { });

            return new StandardValuesCollection(v.GetTitles());
        }


        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }
    }


    public class ControlPropertyNameConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            IEventService service = context.Instance as IEventService;
            if (service != null && service.TargetControl != null)
            {
                return new StandardValuesCollection(service.TargetControl.GetCanBindingControlProperty());
            }
            else
            {
                return new StandardValuesCollection(new string[] { });
            }
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }
    }



    public class ControlNameConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            LWBaseForm v = (LWBaseForm)context.Container.Components[0];
            if (v == null) return new StandardValuesCollection(new string[] { });

            return new StandardValuesCollection(v.ControlNames);
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }
    }
}
