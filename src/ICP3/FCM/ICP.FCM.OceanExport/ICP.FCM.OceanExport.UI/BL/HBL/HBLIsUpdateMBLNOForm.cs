using System;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;

namespace ICP.FCM.OceanExport.UI.BL.HBL
{
    public partial class HBLIsUpdateMBLNOForm : DevExpress.XtraEditors.XtraForm
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        public HBLIsUpdateMBLNOForm()
        {
            InitializeComponent();
            this.Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            this.Text = "提示";
            labMessage.Text = "输入的MBLNO不存在,是否新增或更新MBL?";
            btnAddNew.Text = "新增(&N)";
            btnCancel.Text = "取消(&C)";
            btnUpdate.Text = "更新(&U)";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
