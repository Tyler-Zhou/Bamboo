using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;

namespace ICP.FCM.AirExport.UI.HBL
{
    public partial class HBLIsUpdateMBLNOForm : XtraForm
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        public HBLIsUpdateMBLNOForm()
        {
            InitializeComponent();
            Disposed += delegate {
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                } 
            };
            if (LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            Text = "提示";
            labMessage.Text = "输入的MBLNO不存在,您是要更新当前MBL的号码还是新增MBL?";
            btnAddNew.Text = "新增(&N)";
            btnCancel.Text = "取消(&C)";
            btnUpdate.Text = "更新(&U)";

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
