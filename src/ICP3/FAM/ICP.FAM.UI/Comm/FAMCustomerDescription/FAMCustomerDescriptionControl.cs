using ICP.FAM.ServiceInterface.DataObjects;
using System;
using System.Windows.Forms;

namespace ICP.FAM.UI.Comm
{
    /// <summary>
    /// FAM客户描述信息弹出控件
    /// </summary>
    internal partial class FAMCustomerDescriptionControl : UserControl
    {

        public FAMCustomerDescriptionControl()
        {
            InitializeComponent();
            Disposed += delegate
            {
                if (bindingSource != null)
                {
                    bindingSource.DataSource = null;
                    bindingSource = null;
                }
                OnOk = null;
                OnClear = null;
                
            
            };
        }
        public void SetDataBinding(FAMCustomerDescription customerDescription)
        {
            bindingSource.DataSource = customerDescription;
            bindingSource.ResetBindings(false);
        }

        public event EventHandler OnClear;

        public event EventHandler OnOk;

        public void SetLanguage(bool isEnglish)
        {
            if (isEnglish == false)
            {
                labAddress.Text = "地址";
                labFax.Text = "传真";
                labName.Text = "名称";
                labTel.Text = "电话";

                btnClear.Text = "清除(&L)";
                btnOK.Text = "确定(&O)";
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        public FAMCustomerDescription CustomerDescription
        {
            get {
                bindingSource.EndEdit();
                bindingSource.ResetBindings(false);
                return bindingSource.DataSource as FAMCustomerDescription;
            }
            set { bindingSource.DataSource = value;

                }
        }

        public void Clear()
        {
            txtAddress.EditValue = string.Empty;
            txtFax.EditValue = string.Empty;
            txtName.EditValue = string.Empty;
            txtTel.EditValue = string.Empty;
            bindingSource.EndEdit();
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();

            if (OnClear != null)
            {
                OnClear(this, new EventArgs());
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (OnOk != null)
            {
                OnOk(this, new EventArgs());
            }
        }
    }

}
