using System;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;

namespace ICP.FCM.OceanImport.UI
{
    /// <summary>
    /// 打回订单
    /// </summary>
    [ToolboxItem(false)]
    public partial class ReturnOrder : DevExpress.XtraEditors.XtraUserControl
    {
        public ReturnOrder()
        {
            InitializeComponent();
            this.Disposed += delegate {
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        #region 属性
        public string ReturnRemark
        {
            set;
            get;
        }
        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }



        private void btnOK_Click(object sender, EventArgs e)
        {

            ReturnRemark = this.txtRemark.Text;
            if (string.IsNullOrEmpty(ReturnRemark))
            {
                string message = LocalData.IsEnglish ? "Plase Inpot Return Remark" : "请输入打回原因";
                DevExpress.XtraEditors.XtraMessageBox.Show(message);
                return;
            }

            this.FindForm().DialogResult = DialogResult.OK;
            this.FindForm().Close();

        }
    }
}
