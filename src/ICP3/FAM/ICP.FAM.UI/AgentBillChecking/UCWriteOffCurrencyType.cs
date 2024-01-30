using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.FAM.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;

namespace ICP.FAM.UI
{
    /// <summary>
    /// 销账模式
    /// </summary>
    public partial class UCWriteOffCurrencyType : XtraUserControl
    {  
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public UCWriteOffCurrencyType()
        {
            InitializeComponent();
            Disposed += delegate
            {
                if (WorkItem != null)
                {
                    WorkItem.Items.Remove(this);
                    WorkItem = null;
                }
            
            };
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            FindForm().DialogResult = DialogResult.Cancel;
            //this.FindForm().Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            FindForm().DialogResult = DialogResult.OK;
        }

        public WriteOffType writeOffType
        {
            get
            {
                return (WriteOffType)radioGroup1.SelectedIndex;
            }
        }


    }
}
