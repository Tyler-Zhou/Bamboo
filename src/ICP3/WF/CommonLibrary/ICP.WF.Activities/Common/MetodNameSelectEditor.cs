using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ICP.WF.Activities.Common
{
    /// <summary>
    /// 提供可用于设计MetodData对象编辑器
    /// </summary>
    [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust"), PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
    public class MethodDataEditor : UITypeEditor
	{
        private IWindowsFormsEditorService editorService;

        public override object EditValue(ITypeDescriptorContext typeDescriptorContext, IServiceProvider serviceProvider, object value)
        {
            if (typeDescriptorContext == null)
            {
                throw new ArgumentNullException("typeDescriptorContext");
            }
            if (serviceProvider == null)
            {
                throw new ArgumentNullException("serviceProvider");
            }

            object obj2 = value;
            this.editorService = (IWindowsFormsEditorService)serviceProvider.GetService(typeof(IWindowsFormsEditorService));
            if (this.editorService != null)
            {
                string selectedTypeName = value as string;
                if (((value != null) && (typeDescriptorContext.PropertyDescriptor.PropertyType != typeof(string))) && ((typeDescriptorContext.PropertyDescriptor.Converter != null) && typeDescriptorContext.PropertyDescriptor.Converter.CanConvertTo(typeof(string))))
                {
                    selectedTypeName = typeDescriptorContext.PropertyDescriptor.Converter.ConvertTo(typeDescriptorContext, CultureInfo.CurrentCulture, value, typeof(string)) as string;
                }

                BusinessMethodSelectForm dialog = new BusinessMethodSelectForm(typeDescriptorContext, selectedTypeName);
                if (DialogResult.OK != this.editorService.ShowDialog(dialog))
                {
                    return obj2;
                }
                if (typeDescriptorContext.PropertyDescriptor.PropertyType == typeof(MethodData))
                {
                    return dialog.SelectMethod;
                }
                if (typeDescriptorContext.PropertyDescriptor.PropertyType == typeof(string))
                {
                    return dialog.SelectMethod.MetodName;
                }
                if ((typeDescriptorContext.PropertyDescriptor.Converter != null) && typeDescriptorContext.PropertyDescriptor.Converter.CanConvertFrom(typeDescriptorContext, typeof(string)))
                {
                    obj2 = typeDescriptorContext.PropertyDescriptor.Converter.ConvertFrom(typeDescriptorContext, CultureInfo.CurrentCulture, dialog.SelectMethod.MetodName);
                }
            }
            return obj2;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext typeDescriptorContext)
        {
            return UITypeEditorEditStyle.Modal;
        }
	}


    /// <summary>
    /// 提供可用于设计MetodData对象编辑器
    /// </summary>
    [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust"), PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
    public class PostFunctionEditor : UITypeEditor
    {
        private IWindowsFormsEditorService editorService;

        public override object EditValue(ITypeDescriptorContext typeDescriptorContext, IServiceProvider serviceProvider, object value)
        {
            if (typeDescriptorContext == null)
            {
                throw new ArgumentNullException("typeDescriptorContext");
            }
            if (serviceProvider == null)
            {
                throw new ArgumentNullException("serviceProvider");
            }

            object obj2 = value;
            this.editorService = (IWindowsFormsEditorService)serviceProvider.GetService(typeof(IWindowsFormsEditorService));
            if (this.editorService != null)
            {
                FunctionData data = null;
                if (obj2 != null)
                {
                    data = obj2 as FunctionData;
                }
                SetMainWorkItemDataForm dialog = new SetMainWorkItemDataForm(typeDescriptorContext, data);
                if (DialogResult.OK != this.editorService.ShowDialog(dialog))
                {
                    return obj2;
                }

                if (typeDescriptorContext.PropertyDescriptor.PropertyType == typeof(FunctionData))
                {
                    return dialog.PostFunction;
                }
            }
            return obj2;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext typeDescriptorContext)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
