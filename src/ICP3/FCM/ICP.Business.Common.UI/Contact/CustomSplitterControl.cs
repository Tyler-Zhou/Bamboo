using System.Windows.Forms;
namespace ICP.Business.Common.UI.Contact
{
    public class CustomSplitterControl : DevExpress.XtraEditors.SplitContainerControl
    {
        static int i = 1;
        protected void ChangeSize()
        {
            if (this.PanelVisibility == DevExpress.XtraEditors.SplitPanelVisibility.Both)
            {
                this.Panel2.Width = this.Panel2.Width + i;
                i = -i;
            }
        }

        protected override void OnSizeChanged(System.EventArgs ea)
        {
            if (this.Handle != null)
            {
                BeginInvoke(new MethodInvoker(ChangeSize));
            }

            base.OnResize(ea);
        }

    }
}
