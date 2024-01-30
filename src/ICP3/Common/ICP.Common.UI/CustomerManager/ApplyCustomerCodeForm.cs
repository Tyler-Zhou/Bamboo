using System;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.Common.UI.CustomerManager
{
    public partial class ApplyCustomerCodeForm : BaseEditPart //DevExpress.XtraEditors.XtraForm
    {
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        #region 变量

        CustomerList _currentCustomer;

        bool _isApplyDone = false;

        #endregion

        public CustomerList CurrentCustomer
        {
            get
            {
                return _currentCustomer;
            }

            set
            {
                _currentCustomer = value;
            }
        }

        public bool IsApplyDone
        {
            get
            {
                return _isApplyDone;
            }

            set
            {
                _isApplyDone = value;
            }
        }

        #region 初始化

        public ApplyCustomerCodeForm()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.bsDataSource.DataSource = null;
                this.bsDataSource.Dispose();
                this._currentCustomer = null;
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
        }

        private void InitControls()
        {
            this.txtTel.Text += _currentCustomer.Tel1;
            if (_currentCustomer.Tel2 != null && _currentCustomer.Tel2.Length > 0)
            {
                if (txtTel.Text.Length > 0)
                    this.txtTel.Text += "," + _currentCustomer.Tel2;
                else
                    this.txtTel.Text += _currentCustomer.Tel2;
            }

            bsDataSource.DataSource = _currentCustomer;
        }

        #endregion

        #region 控制器

        /// <summary>
        /// 客户管理控制器
        /// </summary>
        public CustomerManagerController Controller
        {
            get
            {
                return ClientHelper.Get<CustomerManagerController, CustomerManagerController>();
            }
        }

        #endregion

        #region 事件

        void btnExit_Click(object sender, System.EventArgs e)
        {
            var findForm = this.FindForm();
            if (findForm == null)
                return;
            findForm.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            findForm.Close();
        }

        void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_currentCustomer.Fax == "" && _currentCustomer.EMail == "")
                {
                    ICP.Framework.ClientComponents.Controls.Utility.ShowMessage((LocalData.IsEnglish ? "Email.or fax number uncompletable" : "邮箱或传真信息不完整..."));
                    return;
                }
                SingleResultData returndata = Controller.ApplyCustomerCode(_currentCustomer.ID, LocalData.UserInfo.LoginID, this.txtRemark.Text.Trim(), _currentCustomer.UpdateDate);
                if (returndata != null)
                {
                    _currentCustomer.CheckedState = CustomerCodeApplyState.Processing;
                    _currentCustomer.UpdateDate = returndata.UpdateDate;

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(
                        this,
                        "保存成功!");
                    IsApplyDone = true;
                }

                var findForm = this.FindForm();
                if (findForm == null)
                    return;
                findForm.DialogResult = System.Windows.Forms.DialogResult.OK;
                findForm.Close();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(
                    this,
                    ex);
            }
        }

        #endregion
    }
}