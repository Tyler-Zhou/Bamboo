using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Workflow.ComponentModel;

namespace ICP.WF.Activities
{

    [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
    internal sealed class CurrentStepExcutorEditor : UITypeEditor
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
            object selectedName = o;
            this.editorService = (IWindowsFormsEditorService)serviceProvider.GetService(typeof(IWindowsFormsEditorService));
            if (this.editorService != null)
            {
               

                string name = typeDescriptorContext.PropertyDescriptor.GetValue(typeDescriptorContext.Instance) as string;
                string tiltle = string.Empty;
                if (typeDescriptorContext.Instance.GetType().Equals(typeof(ApproveActivity)))
                {
                    tiltle = SR.GetString("TaskHolders", "请选择可以执行当前任务的人");
                }
                else if (typeDescriptorContext.Instance.GetType().Equals(typeof(SendMessageActivity)))
                {
                    tiltle = SR.GetString("Receiver", "接受消息的人");
                }
                else if (typeDescriptorContext.Instance.GetType().Equals(typeof(SendEMailActivity)))
                {
                    tiltle = SR.GetString("Receiver", "接受邮件的人");
                } 
                Activity component = typeDescriptorContext.Instance as Activity;
                ConditionBrowserDialog dialog = new ConditionBrowserDialog(typeDescriptorContext,component, name);
                dialog.Text = tiltle;
                if (DialogResult.OK == this.editorService.ShowDialog(dialog))
                {
                    selectedName = dialog.SelectedName;
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
