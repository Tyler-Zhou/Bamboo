using System.ComponentModel;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.FAM.UI.WriteOff.Dialogs
{
    [ToolboxItem(false)]
    public partial class AutoBillsFinderWizard : XtraUserControl
    {
        public AutoBillsFinderWizard()
        {
            InitializeComponent();

            DevHelper.FormatSpinEdit(spinEdit1, 2);
        }
    }
}
