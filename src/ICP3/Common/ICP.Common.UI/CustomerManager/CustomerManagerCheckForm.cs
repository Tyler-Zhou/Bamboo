
//-----------------------------------------------------------------------
// <copyright file="CustomerManagerContactListEditPart.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace ICP.Common.UI.CustomerManager
{
    using System;
    using ICP.Framework.ClientComponents.UIFramework;
    using Microsoft.Practices.CompositeUI;
    using ICP.Framework.CommonLibrary.Client;
    using System.Windows.Forms;
    using System.Collections.Generic;

    /// <summary>
    /// 客户检查面版
    /// </summary>
    public partial class CustomerManagerCheckForm : BaseListPart //DevExpress.XtraEditors.XtraForm
    {
        #region 服务

        public CustomerManagerController Controller
        {
            get
            {
                return ClientHelper.Get<CustomerManagerController, CustomerManagerController>();
            }
        }
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        #endregion

        #region 初始化

        public CustomerManagerCheckForm()
        {
            InitializeComponent();
            this.Disposed += delegate {
                CommonUtility.RemoveSearchPartKeyEnterToSearch(new List<Control> { this.txtName }, this.KeyEventHandle);
                this.dtErrorInfo.DataSource = null;
                this.mainGridView.CustomDrawRowIndicator -= this.mainGridView_CustomDrawRowIndicator;
                this.mainGridList.DataSource = null;
                this.bsDataSource.DataSource = null;
                this.bsDataSource.Dispose();
                
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.InitControls();
            CommonUtility.SearchPartKeyEnterToSearch(new List<Control> { this.txtName}, this.btnCheck, this.KeyEventHandle);
            this.txtName.Focus();
        }
 
        private void KeyEventHandle(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnCheck.PerformClick();
        }
        private void InitControls()
        {
            //btnToAddCustomer.Enabled = false;

            pictureTip.Image = ICP.Framework.ClientComponents.Resources.GlobalResource.Tip_16;
        }
        #endregion

        #region 本地方法

        /*验证*/
        private bool ValidateData()
        {
            dtErrorInfo.ClearErrors();

            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                dtErrorInfo.SetError(txtName, LocalData.IsEnglish ? "Name is necessary." : "必须录入名称才能检查.");
                txtName.Focus();

                return false;
            }

            return true;
        }
        #endregion

        #region 事件处理

        /*检查*/
        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (this.ValidateData() == false)
            {
                return;
            }
            using (new CursorHelper())
            {
                this.bsDataSource.DataSource = this.Controller.GetCustomerListForName(txtName.Text.Trim());
            }
            if (this.bsDataSource.Count > 0)
            {
                lblTip.Text = "已经存在相匹配的客户,考虑是否需要继续新增客户.";
            }
            else
            {
                var findForm = this.FindForm();
                if (findForm == null)
                    return;
                findForm.DialogResult = System.Windows.Forms.DialogResult.OK;
                findForm.Close();
            }
        }

        /*确认新增客户*/
        private void btnToAddCustomer_Click(object sender, EventArgs e)
        {
            var findForm = this.FindForm();
            if (findForm == null)
                return;
            findForm.DialogResult = System.Windows.Forms.DialogResult.OK;
            findForm.Close();
        }

        /*取消*/
        private void btnCancel_Click(object sender, EventArgs e)
        {
            var findForm = this.FindForm();
            if (findForm == null)
                return;
            findForm.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            findForm.Close();
        }

        private void mainGridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }

        #endregion

        #region 外部接口

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName
        {
            get
            {
                return this.txtName.Text.Trim();
            }
        }

        #endregion
    }
}