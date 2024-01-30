//-----------------------------------------------------------------------
// <copyright file="SetCustomerIsDangerous.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace ICP.Common.UI.CustomerManager
{
    using System;
    using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;

    public partial class SetCustomerIsDangerousForm : DevExpress.XtraEditors.XtraForm
    {
        private bool _isDangerous = false;
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        #region 初始化

        public SetCustomerIsDangerousForm()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.btnCancel.Click -= this.btnCancel_Click;
                this.btnOk.Click -= this.btnOk_Click;
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            this.InitControls();
        }

        private void InitControls()
        {
            if (IsDangerous)
            {
                labelMemo.Text = LocalData.IsEnglish ? "Memo for cancel" : "取消危险客户备注";
            }
            else
            {
                labelMemo.Text = LocalData.IsEnglish ? "Memo for Set to Risk Clients" : "设为危险客户备注";
            }

            btnOk.Text = LocalData.IsEnglish ? "Ok(&S)" : "确定(&S)";
            btnCancel.Text = LocalData.IsEnglish ? "Cancel(&C)" : "取消(&C)";
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
            if (string.IsNullOrEmpty(this.memoEdit1.Text.Trim()))
            {
                labelMessege.Text = "请先加备注信息!";
                this.memoEdit1.Focus();
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        /*取消*/
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        #endregion

        #region 外部接口

        /// <summary>
        /// 当前报表配置
        /// </summary>
        public bool IsDangerous
        {
            get
            {
                return _isDangerous;
            }

            set
            {
                _isDangerous = value;
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            get
            {
                return this.memoEdit1.Text.Trim();
            }
        }

        #endregion
    }
}