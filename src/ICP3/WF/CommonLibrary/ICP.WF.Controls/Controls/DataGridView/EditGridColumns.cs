using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.Security;
using System.Security.Permissions;
using System.ComponentModel;
using System.Windows.Forms;

namespace ICP.WF.Controls
{
    [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
    public class EditGridColumns : UITypeEditor
    {
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            LWDataGridView gridView = context.Instance as LWDataGridView;
            if (gridView == null)
            {
                return null;
            }

            GridColumnsEditForm setColumns = new GridColumnsEditForm();
            setColumns.GridView = gridView;
            if (setColumns.ShowDialog() == DialogResult.OK)
            {
                
            }

            return base.EditValue(context, provider, value);
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext typeDescriptorContext)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
