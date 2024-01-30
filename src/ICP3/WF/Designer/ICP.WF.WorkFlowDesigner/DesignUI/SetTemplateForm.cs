using System;
using System.Windows.Forms;

namespace ICP.WF.WorkFlowDesigner
{
    /// <summary>
    /// 流程模板设置窗体
    /// </summary>
    public partial class SetTemplateForm : DevExpress.XtraEditors.XtraForm
    {
        #region 资源初始化


        public SetTemplateForm()
        {
            InitializeComponent();
           
      
        }

        #endregion

        #region 事件处理

        /*确定*/
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidateData() == false) return;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /*取消*/
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion


        #region 本地方法

        /*保存前窗体验证*/
        private bool ValidateData()
        {
            errorTip.Clear();
            bool isSucc = true;

            if (txtTemplateName.Text.Trim().Length == 0)
            {
                errorTip.SetError(txtTemplateName,Utility.GetString("MustInput", "Template Name must input！"));
                isSucc = false;
                txtTemplateName.Focus();
                
            }

            if (this.txtTemplateName.Text.Trim().Contains(" "))
            {
                errorTip.SetError(txtTemplateName, Utility.GetString("NameCannotContainSpaceCharacter", "Template Name cannot contain a space character!"));
                isSucc = false;
                txtTemplateName.Focus();
            }

            return isSucc;
        }

        #endregion

        #region 公共属性


        /// <summary>
        /// 摸版名称
        /// </summary>
        public string TemplateName
        {
            get
            {
                return txtTemplateName.Text.Trim();
            }
        }

        #endregion
    }
}
