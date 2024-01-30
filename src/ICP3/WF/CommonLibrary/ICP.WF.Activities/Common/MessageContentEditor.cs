using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ICP.WF.Activities
{
    [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
    internal sealed class MessageContentEditor : UITypeEditor
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
            string message = (string)o;
            this.editorService = (IWindowsFormsEditorService)serviceProvider.GetService(typeof(IWindowsFormsEditorService));
            if (this.editorService != null)
            {
                string tiltle = string.Empty;
                if (typeDescriptorContext.PropertyDescriptor.Name == "Subject")
                {
                    tiltle = SR.GetString("SubjectDesc", "邮件主题");
                }
                else if (typeDescriptorContext.PropertyDescriptor.Name == "Message")
                {
                    tiltle = SR.GetString("MailContent", "邮件内容");
                } 

                SetMessageContentForm dialog = new SetMessageContentForm(typeDescriptorContext, message);
                dialog.Text = tiltle;
                if (DialogResult.OK == this.editorService.ShowDialog(dialog))
                {
                    message = dialog.Message;
                }
            }
            return message;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext typeDescriptorContext)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
