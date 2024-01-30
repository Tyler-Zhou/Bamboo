
namespace ICP.FCM.OceanImport.UI
{
    using System;
    using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;

    public partial class ConfirmBookingForm : BaseEditPart
    {
        private string _labelText = string.Empty;

        #region 初始化
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public ConfirmBookingForm()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                this.Disposed += delegate
                {

                    Saved = null;
                    if (this.WorkItem != null)
                    {
                        this.WorkItem.Items.Remove(this);
                        this.WorkItem = null;
                    }
                };
            }
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

        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;
        /*确认*/
        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtRemark.Text.Trim()))
            {
                labelMessege.Text = "请先加备注信息!";
                this.txtRemark.Focus();
                return;
            }

            if (Saved != null) Saved(new object[] { this.txtRemark.Text.Trim() });

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


        public override object DataSource
        {
            set { BindingData(value); }
        }

        private void BindingData(object value)
        {
            _labelText = value as string;

            if (!string.IsNullOrEmpty(_labelText))
            {
                InitControls();
            }
        }


        #endregion
    }
}