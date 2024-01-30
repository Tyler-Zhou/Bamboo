#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/6/15 星期五 13:56:24
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion


namespace ICP.Common.UI.Controls
{
    using System;
    using System.Windows.Forms;
    using DevExpress.XtraEditors.Controls;
    using ServiceInterface.DataObjects;

    /// <summary>
    /// 客户描述信息弹出控件
    /// </summary>
    internal partial class CustomerForNewPopupContainer : UserControl
    {
        /// <summary>
        /// 清除事件
        /// </summary>
        public event EventHandler OnClear;

        /// <summary>
        /// 确定事件
        /// </summary>
        public event EventHandler OnOk;

        /// <summary>
        /// 国家数据列表
        /// </summary>
        public ImageComboBoxItemCollection CountryItems
        {
            get
            {
                return cmbCountry.Properties.Items;
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        public CustomerDescriptionForNew DataSource
        {
            get
            {
                _BSCustomerInfo.EndEdit();
                _BSCustomerInfo.ResetBindings(false);
                return _BSCustomerInfo.DataSource as CustomerDescriptionForNew;
            }
            set
            {
                _BSCustomerInfo.DataSource = value;

            }
        }

        /// <summary>
        /// 客户描述信息弹出控件
        /// </summary>
        public CustomerForNewPopupContainer()
        {
            InitializeComponent();
            Disposed += delegate
            {
                if (_BSCustomerInfo != null)
                {
                    _BSCustomerInfo.DataSource = null;
                    _BSCustomerInfo = null;
                }
                OnOk = null;
                OnClear = null;
            };
            cmbCountry.ButtonClick += cmbCountry_ButtonClick;
        }

        /// <summary>
        /// 设置客户描述信息控件绑定数据源
        /// </summary>
        /// <param name="datasource"></param>
        public void SetDataBinding(CustomerDescriptionForNew datasource)
        {
            _BSCustomerInfo.DataSource = datasource;
            _BSCustomerInfo.ResetBindings(false);
        }

        /// <summary>
        /// 国家信息
        /// </summary>
        void cmbCountry_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                cmbCountry.Text = string.Empty;
                txtCityZip.Text = string.Empty;
                txtCityZip.EditValue = null;
                _BSCustomerInfo.EndEdit();
                _BSCustomerInfo.ResetBindings(false);
            }
        }

        /// <summary>
        /// 清空
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();

            if (OnClear != null)
            {
                OnClear(this, new EventArgs());
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (OnOk != null)
            {
                OnOk(this, new EventArgs());
            }
        }

        /// <summary>
        /// 设置语言
        /// </summary>
        /// <param name="isEnglish"></param>
        public void SetLanguage(bool isEnglish)
        {
            if (isEnglish == false)
            {
                labAddress.Text = "地址";
                labCityZip.Text = "城市";
                labCountry.Text = "国家";
                labEnterpriseCodeType.Text = "企业代码类型";
                labEnterpriseCode.Text = "企业代码";
                labFax.Text = "传真";
                labName.Text = "名称";
                labRemark.Text = "备注";
                labContact.Text = "联系人";
                labTel.Text = "电话";
                btnClear.Text = "清除(&L)";
                btnOK.Text = "确定(&O)";

                foreach (EditorButton item in cmbCountry.Properties.Buttons)
	            {
                    if (item.Kind == ButtonPredefines.Delete)
                    {
                        item.ToolTip = "点击按钮以清空国家和城市.";
                    }
	            }
            }
        }

        /// <summary>
        /// 清空内容
        /// </summary>
        public void Clear()
        {
            txtAddress.EditValue = string.Empty;
            txtCityZip.EditValue = string.Empty;
            txtFax.EditValue = string.Empty;
            txtName.EditValue = string.Empty;
            txtRemark.EditValue = string.Empty;
            txtTel.EditValue = string.Empty;
            cmbCountry.EditValue = null;
            txtEnterpriseCodeType.EditValue = string.Empty;
            txtEnterpriseCode.EditValue = string.Empty;
            _BSCustomerInfo.EndEdit();
        }
    }

}
