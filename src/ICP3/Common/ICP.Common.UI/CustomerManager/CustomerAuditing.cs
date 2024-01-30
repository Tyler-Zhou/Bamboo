using System;
using System.Text;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface;
using ICP.Sys.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;

namespace ICP.Common.UI.CustomerManager
{
    public partial class CustomerAuditing : DevExpress.XtraEditors.XtraForm
    {
        #region 服务
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        /// <summary>
        /// 客户管理服务
        /// </summary>
        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        /// <summary>
        /// 客户管理服务
        /// </summary>
        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        /// <summary>
        /// 消息服务
        /// </summary>
        public IMessageService MessageService
        {
            get
            {
                return ServiceClient.GetService<IMessageService>();
            }
        }

        #endregion

        #region 变量

        Guid _customerId = Guid.Empty;

        CustomerConfirmInfoForAuditing _applyCodeData; 

        #endregion

        public CustomerConfirmInfoForAuditing CurrentData
        {
            get
            {
                return _applyCodeData;
            }

            set
            {
                _applyCodeData = value;
            }
        }

        public string Code
        {
            get
            {
                return txtCode.Text.Trim();
            }
        }

        DateTime? _updateDate;
        public DateTime? UpdateDate
        {
            get
            {
                return _updateDate;
            }
            set
            {
                _updateDate = value;
            }
        }

        #region 初始化

        public CustomerAuditing()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.errorProvider1.DataSource = null;
                this.bsDataSource.DataSource = null;
                this.bsDataSource.Dispose();
                this.errorProvider1.DataSource = null;

                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            };
        }

        #endregion

        #region 接口

        public void ShowDialog(CustomerConfirmInfoForAuditing applyData, bool isSccTel, bool isSccEmailAndFax, string code,string exmessage)
        {
            this._customerId = applyData.ID;
            _applyCodeData = applyData;
            if (!string.IsNullOrEmpty(exmessage))
            {
                _applyCodeData.State = CustomerCodeApplyState.Unpassed;
            }

            if (_applyCodeData.State == CustomerCodeApplyState.Passed || _applyCodeData.State == CustomerCodeApplyState.Processing) _applyCodeData.IsAgree = true;
            else if (_applyCodeData.State == CustomerCodeApplyState.Unpassed) _applyCodeData.IsAgree = false;

            this.txtCode.Text = code;
            this.txtDate.Text = _applyCodeData.ApplyDate.ToShortDateString();

            StringBuilder strBuf = new StringBuilder();
            if (!string.IsNullOrEmpty(exmessage))
            {
                strBuf.Append(exmessage);
                linkTip.Visible = true;
            }

            //if (isSccTel == false)
            //{
            //    radioGroup1.Properties.ReadOnly = true;
            //    _applyCodeData.IsAgree = false;
            //    linkTip.Visible = true;
            //    strBuf.Append(LocalData.IsEnglish ? "Tel number uncompletable" : "电话号码信息不完整."+"\r\n");
            //    //linkTip.Click += delegate { this.txtRemark.Text += linkTip.Text; };
            //}

            //if (isSccEmailAndFax == false)
            //{
            //    radioGroup1.Properties.ReadOnly = true;
            //    _applyCodeData.IsAgree = false;
            //    linkTip.Visible = true;
            //    strBuf.Append(LocalData.IsEnglish ? "Email.or fax number uncompletable" : "邮件或传真信息不完整." + "\r\n");

            //    //linkTip.Click += delegate { this.txtRemark.Text += linkTip.Text; };
            //}
            //else
            //{
            linkTip.Visible = string.IsNullOrEmpty(strBuf.ToString()) ? false : true;
            //}

            this.linkTip.Text = strBuf.ToString();

            bsDataSource.DataSource = _applyCodeData;
            SetTextForUI();
            this.ShowDialog();
        }

        #endregion

        #region 事件

        private void btnOk_Click(object sender, EventArgs e)
        { 
            this.labelCodeError.Visible = false;
            if (_applyCodeData.IsAgree && !this.ValidateData()) return;

            _applyCodeData.State = _applyCodeData.IsAgree ? CustomerCodeApplyState.Passed : CustomerCodeApplyState.Unpassed;

            try
            {
                SingleResultData result = CustomerService.ConfirmCustomerInfo(_applyCodeData.ID, txtCode.Text.Trim(), _applyCodeData.State, LocalData.UserInfo.LoginID, _applyCodeData.ApplicantRemark, _applyCodeData.UpdateDate);
                _updateDate = result.UpdateDate;
            }
            catch (Exception ex)
            {
                //LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                this.labelCodeError.Visible = true;
                int start = ex.Message.IndexOf("<Message>");
                int end = ex.Message.IndexOf("</Message>");
                int len = end - start;
                if (len > 0)
                {
                    labelCodeError.Text = ex.Message.Substring(start + 9, len - 9);
                }

                return;
            }

            SendEmail();

            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(
                this.FindForm(),
                "审核成功!");
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void SendEmail()
        {
            if (_applyCodeData.IsAgree)
                return;
            string body = LocalData.IsEnglish ? "Did not agree you found customer" : "不同意你创建的客户" +
                (_applyCodeData.CustomerName) + (LocalData.IsEnglish ? "Thereason" : "的代码申请操作，原因：") + this.txtRemark.Text;

            string emailAddress = UserService.GetUserInfo(_applyCodeData.ApplicantID).EMail;
            if (string.IsNullOrEmpty(emailAddress) || !CommonUtility.IsEmail(emailAddress))
                return;
            string subject = LocalData.IsEnglish ? "[" + _applyCodeData.CustomerName + "] Customer code apply status" : "[" + _applyCodeData.CustomerName + "] 客户代码申请状态..";

            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.SendFrom = LocalData.UserInfo.EmailAddress;
            message.CreateBy = LocalData.UserInfo.LoginID;
            message.Body = body;
            message.Subject = subject;
            //message.CC = "Garrettli@cityocean.com";
            message.SendTo =emailAddress; 
            try
            {
                this.MessageService.Send(message);
            }
            catch(Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        bool ValidateData()
        {
            this.errorProvider1.ClearErrors();
            if (string.IsNullOrEmpty(this.txtCode.Text.Trim()))
            {
                this.errorProvider1.SetError(this.txtCode, "Must Input");
                this.txtCode.Focus();
                return false;
            }
 
            return true;
        }

        private void SetTextForUI()
        {
            if (!LocalData.IsEnglish)
            {
                labelIsPass.Text = "是否同意";
                labelCode.Text = "代码";
                labelUser.Text = "申请";
                labelDate.Text = "申请日期";
                groupBox1.Text = "备注";
                btnOk.Text = "确定";
                btnCancel.Text = "取消";

            }
        }

        private void labelCodeError_Click(object sender, EventArgs e)
        {

        }

        private void linkTip_Click(object sender, EventArgs e)
        {
            this.txtRemark.Text += "\r\n" + linkTip.Text;
        }
    }

    public class CustomerConfirmInfoForAuditing : CustomerConfirmInfo
    {
        public bool IsAgree { get; set; }
    }
}
