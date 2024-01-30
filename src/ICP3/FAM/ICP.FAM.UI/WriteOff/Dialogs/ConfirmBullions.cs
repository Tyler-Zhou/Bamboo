
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.FAM.UI.WriteOff.Dialogs
{
    public partial class ConfirmBullions : XtraUserControl
    {
        public ConfirmBullions()
        {
            InitializeComponent();

            DevHelper.FormatMoney(spinEdit1);
        }
    }
}
