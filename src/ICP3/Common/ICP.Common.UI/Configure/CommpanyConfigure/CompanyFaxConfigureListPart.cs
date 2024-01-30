using System;
using System.Collections.Generic;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Message.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Common;
using System.Text.RegularExpressions;

namespace ICP.Common.UI.Configure
{
    /// <summary>
    /// 公司传真配置面板
    /// </summary>
    [System.ComponentModel.ToolboxItem(false)]
    public partial class CompanyFaxConfigureListPart : BaseListEditPart
    {
        #region 服务
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public IFaxService FaxService
        {
            get
            {
                return ServiceClient.GetService<IFaxService>();
            }
        }
        #endregion

        #region 常量
        public ConfigureObjects CurrentRow { get; set; }

        #endregion

        #region 初始化
        public CompanyFaxConfigureListPart()
        {
            InitializeComponent();
            if (!LocalData.IsDesignMode)
            {
                InitControls();
            }
            this.Disposed += delegate
            {
                this.faxErrorProvider.DataSource = null;
                this.bsFax.DataSource = null;
                this.bsFax.Dispose();
                this.btnSave.Click -= new EventHandler(btnSave_Click);
                this.btnReset.Click -= new EventHandler(btnReset_Click);
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }
        void RegisterEvents()
        {
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnReset.Click += new EventHandler(btnReset_Click);
        }


        void InitControls()
        {
            if (LocalData.IsEnglish)
            {
                lblEmailAddress.Text = "EmailAddress: ";
                lblEmailHost.Text = "EmailHost: ";
                lblEmail.Text = "UserName: ";
                lblEmailPwd.Text = "PassWord: ";
                lblReEmailPwd.Text = "Confirm PassWord: ";
                btnReset.Text = "Reset(&P)";
                btnSave.Text = "Save(&S)";
            }
            RegisterEvents();
        }
        public override void Init(IDictionary<string, object> values)
        {
            if (values != null && values.ContainsKey("ConfigureObjects"))
            {
                CurrentRow = (ConfigureObjects)values["ConfigureObjects"];
                if (CurrentRow == null)
                {
                    CurrentRow = new ConfigureObjects();
                }
            }
            else
            {
                CurrentRow = new ConfigureObjects();
            }
            this.bsFax.DataSource = CurrentRow;
            bsFax.ResetBindings(false);
        }

        #endregion

        private bool ValidateData()
        {
            bool isPass = false;
            if (ValidateEmailAddress(txtEmailAddress, CurrentRow.EmailAddress) 
                && ValidatePassWord()
                && !ValidateMailHost()
                )
            {
                isPass = true;
            }

            return isPass;
        }

        private bool ValidateMailHost()
        {
            bool isNullEmailHost = false;
            if (!string.IsNullOrEmpty(txtEmailAddress.Text) && !string.IsNullOrEmpty(txtEmailPwd.Text) && !string.IsNullOrEmpty(txtReEmailPwd.Text))
            {
                if (string.IsNullOrEmpty(txtEmailHost.Text))
                {
                    isNullEmailHost = true;
                    faxErrorProvider.SetError(txtEmailHost,LocalData.IsEnglish?"Email Host is nessary.":"邮件服务器地址必须输入.");
                }
            }

            return isNullEmailHost;
        }

        private bool ValidatePassWord()
        {
            bool isSamePwd = false;
            if (string.IsNullOrEmpty(txtReEmailPwd.Text) && string.IsNullOrEmpty(txtEmailPwd.Text))
            {
                return true;
            }
            if (string.IsNullOrEmpty(txtEmailPwd.Text))
            {
                faxErrorProvider.SetError(txtEmailPwd, LocalData.IsEnglish ? "Password is necessary." : "密码不能为空.");
            }
            else if (string.IsNullOrEmpty(txtReEmailPwd.Text))
            {
                faxErrorProvider.SetError(txtReEmailPwd, LocalData.IsEnglish ? "Re-Password is necessary." : "确认密码不能为空.");
            }
            else
            {
                if (txtEmailPwd.Text != txtReEmailPwd.Text)
                {
                    faxErrorProvider.SetError(txtReEmailPwd, LocalData.IsEnglish ? "Re-Password must be same." : "确认密码必须相同.");
                }
                else
                {
                    isSamePwd = true;
                }
            }

            return isSamePwd;
        }

        private bool ValidateEmailAddress(DevExpress.XtraEditors.TextEdit txtCtl, string address)
        {
            bool isEmailAddress = true;
            string EmailPattern = @"^([A-Za-z0-9]{1}[A-Za-z0-9_]*)@([A-Za-z0-9_]+)[.]([A-Za-z0-9_]*)$";//E-Mail地址格式的正则表达式
            if (!string.IsNullOrEmpty(address))
            {
                isEmailAddress = Regex.IsMatch(CurrentRow.EmailAddress, EmailPattern);
                if (!isEmailAddress)
                {
                    faxErrorProvider.SetError(txtCtl, LocalData.IsEnglish ? "Email Address is not correct." : "邮件地址不正确.");
                }
            }

            return isEmailAddress;
        }

        #region 事件
        void btnReset_Click(object sender, EventArgs e)
        {
            if (CurrentRow != null)
            {
                CurrentRow.EmailHost = CurrentRow.EmailAddress = CurrentRow.Email = CurrentRow.EmailHost
                    = CurrentRow.EmailPassWord = CurrentRow.TaxNo= txtReEmailPwd.Text  = string.Empty;

                this.bsFax.DataSource = CurrentRow;
                bsFax.ResetBindings(false);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            faxErrorProvider.Clear();
            if (CurrentRow != null && ValidateData())
            {
                ManyResult result = FaxService.UpdateConfigureInfoByCompanyID(CurrentRow);
                if (result != null && result.Items.Count > 0)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save successfully!" : "保存成功!");
                }
                else
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save failure!" : "保存失败!");
                }
            }
        }

        #endregion
    }
}
