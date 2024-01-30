
//-----------------------------------------------------------------------
// <copyright file="TemplateForm.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.FormDesigner
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// 表单摸版管理界面
    /// </summary>
    public partial class TemplateForm : DevExpress.XtraEditors.XtraForm
    {
        public TemplateForm()
        {
            InitializeComponent();

            if (DesignMode == false)
            {
                this.Load += new EventHandler(TemplateForm_Load);
            }
        }

        void TemplateForm_Load(object sender, EventArgs e)
        {
            if (DesignMode == false)
            {
               // LongWin.Framework.ClientComponents.IMEControl.SetIme(this);
            }
        }

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


        /*保存前窗体验证*/
        private bool ValidateData()
        {
            errorTip.Clear();
            bool isSucc = true;

            if (txtTemplateName.Text.Trim().Length == 0)
            {
                errorTip.SetError(txtTemplateName, Utility.GetString("MustInput", "必须填写！"));
                isSucc = false;
                txtTemplateName.Focus();
            }

            if (this.txtTemplateName.Text.Trim().Contains(" "))
            {
                errorTip.SetError(txtTemplateName, Utility.GetString("FormNameCannotContainSpaceCharacter", "名称不能包含空格等特殊字符!"));
                isSucc = false;
                txtTemplateName.Focus();
            }

            return isSucc;
        }

        public string TemplateName
        {
            get
            {
                return txtTemplateName.Text.Trim();
            }
        }
        
    }
}
