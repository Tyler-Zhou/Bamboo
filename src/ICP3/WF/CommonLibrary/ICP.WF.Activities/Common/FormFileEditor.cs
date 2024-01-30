using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using ICP.WF.ServiceInterface;
namespace ICP.WF.Activities.Common
{
    /// <summary>
    /// 表单选择
    /// </summary>
    [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
    internal sealed class FormFileEditor : UITypeEditor
    {
        private IWindowsFormsEditorService editorService;
        public override object EditValue(ITypeDescriptorContext typeDescriptorContext, IServiceProvider serviceProvider, object o)
        {
            if (typeDescriptorContext == null)
            {
                throw new ArgumentNullException("typeDescriptorContext");
            }
            if (serviceProvider == null)
            {
                throw new ArgumentNullException("serviceProvider");
            }

            string selectedName = (string)o;
            this.editorService = (IWindowsFormsEditorService)serviceProvider.GetService(typeof(IWindowsFormsEditorService));
            if (this.editorService != null)
            {
                FormFileSelectEditor dialog = new FormFileSelectEditor(typeDescriptorContext,selectedName);
                if (DialogResult.OK == this.editorService.ShowDialog(dialog))
                {
                    selectedName = dialog.SelectedDataSourceName;
                }
            }
            return selectedName;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext typeDescriptorContext)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
