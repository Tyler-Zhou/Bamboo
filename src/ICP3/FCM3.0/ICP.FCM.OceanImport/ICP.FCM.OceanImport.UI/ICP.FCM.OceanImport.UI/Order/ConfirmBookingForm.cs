
namespace ICP.FCM.OceanImport.UI
{
    using System;

    public partial class ConfirmBookingForm : ICP.Framework.ClientComponents.UIFramework.BasePart
    {
        private string _labelText = string.Empty;

        #region 初始化

        public ConfirmBookingForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!DesignMode)
            {
                this.InitControls();
            }
        }

        private void InitControls()
        {          
            labelMemo.Text = _labelText;
        }

        #endregion

        #region 本地方法

        public void EndEdit()
        {
            this.Validate();
        }

        /*验证*/
        private bool ValidateData()
        {
            //dtErrorInfo.ClearErrors();

            //if (string.IsNullOrEmpty(txtName.Text.Trim()))
            //{
            //    dtErrorInfo.SetError(txtName, "必须录入名称才能检查.");
            //    txtName.Focus();

            //    return false;
            //}

            return true;
        }

        #endregion

        #region 事件处理

        /*确认*/
        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtRemark.Text.Trim()))
            {
                labelMessege.Text = "请先加备注信息!";
                this.txtRemark.Focus();
                return;
            }

            this.FindForm().DialogResult = System.Windows.Forms.DialogResult.OK;
            this.FindForm().Close();
        }

        /*取消*/
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.FindForm().Close();
        }

        #endregion

        #region 外部接口

        public string LabelText
        {
            get
            {
                return _labelText;
            }

            set
            {
                _labelText = value;
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            get
            {
                return this.txtRemark.Text.Trim();
            }
        }

        #endregion
    }
}