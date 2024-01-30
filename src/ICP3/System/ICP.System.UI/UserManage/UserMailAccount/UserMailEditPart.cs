using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Client;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Sys.UI.UserManage.UserMailAccount
{
    [ToolboxItem(false)]
    public partial class UserMailEditPart : ICP.Framework.ClientComponents.UIFramework.BaseEditPart
    {
        #region 服务注入

        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region init

        public UserMailEditPart()
        {
            InitializeComponent();

            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate {
                this.Saved = null;
                this.txtIncomingLogin.TextChanged -= this.txtIncomingLogin_TextChanged;
                this.txtIncomingPassword.TextChanged -= this.txtIncomingPassword_TextChanged;
                this._parentList = null;
                this.dxErrorProvider1.DataSource = null;
                this.bindingSource1.DataSource = null;
                this.bindingSource1.Dispose();
                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
            
            };
        }

        private void SetCnText()
        {
            labEmail.Text = "邮箱";
            labFriendlyName .Text = "名称";
            labIncoming .Text = "接收";
            labIncoming1.Text = "接收";
            labIncomingHost.Text = "接收服务器";
            labIncomingPassword.Text = "密码";
            labIncomingPort .Text = "端口";
            labIncomingProtocol .Text = "协议";
            labMailIncomingLogin .Text = "登录名";
            labOutgoing.Text = "发送";
            labOutgoing2.Text = "发送";
            labOutgoingHost.Text = "发送服务器";
            labOutgoingLogin.Text = "登录名";
            labOutgoingPassword.Text = "密码";
            labOutgoingPort.Text = "端口";
            labOutgoingProtocol.Text = "协议";

            chkSameAsIncoming.Text = "和接收登录方式相同.";

            navBarBase.Caption = "常规";
            navBarAdvanced.Caption = "高级";
            navBarLoginInfo.Caption = "登录";
            navBarService.Caption = "服务器";

        }
        protected override void OnLoad(EventArgs e)
        {
            InitControls();
            txtIncomingLogin.TextChanged += new EventHandler(txtIncomingLogin_TextChanged);
            txtIncomingPassword.TextChanged += new EventHandler(txtIncomingPassword_TextChanged);
            base.OnLoad(e);
        }

        private void InitControls()
        {
            
            List<EnumHelper.ListItem<MailProtocol>> mailProtocols = EnumHelper.GetEnumValues<MailProtocol>(LocalData.IsEnglish);
            cmbIncomingProtocol.Properties.BeginUpdate();
            this.cmbMailOutgoingProtocol.Properties.BeginUpdate();
            foreach (var item in mailProtocols)
            {
                cmbIncomingProtocol.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
                cmbMailOutgoingProtocol.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbIncomingProtocol.Properties.EndUpdate();
            this.cmbMailOutgoingProtocol.Properties.EndUpdate();

        }

        #endregion

        #region method

        private void chkSameAsIncoming_CheckedChanged(object sender, EventArgs e)
        {
            UserMailAccountList data = this.bindingSource1.DataSource as UserMailAccountList;
            bool isDirty = data == null ? false : data.IsDirty;

            txtOutgoingLogin.Enabled = txtOutgoingPassword.Enabled = !chkSameAsIncoming.Checked;
            if (chkSameAsIncoming.Checked)
            {
                txtOutgoingLogin.Text = txtIncomingLogin.Text;
                txtOutgoingPassword.Text = txtIncomingPassword.Text ;
            }

            if (data != null && isDirty == false) data.BeginEdit();
        }

        void txtIncomingPassword_TextChanged(object sender, EventArgs e)
        {
            if (chkSameAsIncoming.Checked)
            {
                UserMailAccountList data = this.bindingSource1.DataSource as UserMailAccountList;
                txtOutgoingPassword.Text = data.MailOutgoingPassword = data.MailIncomingPassword = txtIncomingPassword.Text;
            }
        }

        void txtIncomingLogin_TextChanged(object sender, EventArgs e)
        {
            if (chkSameAsIncoming.Checked)
            {
                UserMailAccountList data = this.bindingSource1.DataSource as UserMailAccountList;
                txtOutgoingLogin.Text = data.MailOutgoingLogin = data.MailIncomingLogin = txtIncomingLogin.Text;
            }
        }

        bool Save()
        {
             if (_parentList == null) return false;
            this.Validate();
            this.bindingSource1.EndEdit();

            List<UserMailAccountList> currentList = new List<UserMailAccountList> { this.DataSource as UserMailAccountList };
            foreach (var item in currentList)
            {
                if (item.Validate() == false) return false;
            }

            try
            {
                


                for (int i = 0; i < currentList.Count; i++)
                {
                    bool isDirty = currentList[i].IsDirty;
                    if (currentList[i].FriendlyName != null) currentList[i].FriendlyName = currentList[i].FriendlyName.Trim();
                    if (currentList[i].Email != null) currentList[i].Email = currentList[i].Email.Trim();
                    if (currentList[i].MailIncomingHost != null) currentList[i].MailIncomingHost.Trim();
                    if (currentList[i].MailIncomingLogin != null) currentList[i].MailIncomingLogin = currentList[i].MailIncomingLogin.Trim();
                    if (currentList[i].MailOutgoingHost != null) currentList[i].MailOutgoingHost = currentList[i].MailOutgoingHost.Trim();
                    if (currentList[i].MailOutgoingLogin != null) currentList[i].MailOutgoingLogin = currentList[i].MailOutgoingLogin.Trim();
                    if (isDirty == false) currentList[i].BeginEdit();

                    if (currentList[i].Validate() == false)
                    {
                        return false;
                    }
                }

                Guid[] ids = new Guid[currentList.Count];
                string[] emails = new string[currentList.Count];
                MailProtocol[] mailIncomingProtocols = new MailProtocol[currentList.Count];
                string[] mailIncomingHosts = new string[currentList.Count];
                int[] mailIncomingPorts = new int[currentList.Count];
                string[] mailIncomingLogins = new string[currentList.Count];
                string[] mailIncomingPasswords = new string[currentList.Count];
                MailProtocol[] mailOutgoingProtocols = new MailProtocol[currentList.Count];
                string[] mailOutgoingHosts = new string[currentList.Count];
                int[] mailOutgoingPorts = new int[currentList.Count];
                string[] mailOutgoingLogins = new string[currentList.Count];
                string[] mailOutgoingPasswords = new string[currentList.Count];
                string[] friendlyNames = new string[currentList.Count];
                bool[] getMailAtLogins = new bool[currentList.Count];
                List<DateTime?> updates = new List<DateTime?>(currentList.Count);


                for (int i = 0; i < currentList.Count; i++)
                {
                    ids[i] = currentList[i].ID;
                    emails[i] = currentList[i].Email;
                    mailIncomingProtocols[i] = currentList[i].MailIncomingProtocol;
                    mailIncomingHosts[i] = currentList[i].MailIncomingHost ;
                    mailIncomingPorts[i] = currentList[i].MailIncomingPort;
                    mailIncomingLogins[i] = currentList[i].MailIncomingLogin;
                    mailIncomingPasswords[i] = currentList[i].MailIncomingPassword;
                    mailOutgoingProtocols[i] = currentList[i].MailOutgoingProtocol;
                    mailOutgoingHosts[i] = currentList[i].MailOutgoingHost;
                    mailOutgoingPorts[i] = currentList[i].MailOutgoingPort;
                    mailOutgoingLogins[i] = currentList[i].MailOutgoingLogin;
                    mailOutgoingPasswords[i] = currentList[i].MailOutgoingPassword;
                    friendlyNames[i] = currentList[i].FriendlyName;
                    getMailAtLogins[i] = currentList[i].GetMailAtLogin;
                    updates.Add(currentList[i].UpdateDate);
                }

                ManyResultData result = UserService.SaveUserMailAccountInfo(_parentList.ID,
                                                                   ids,
                                                                   emails,
                                                                   mailIncomingProtocols,
                                                                   mailIncomingHosts,
                                                                   mailIncomingPorts,
                                                                   mailIncomingLogins,
                                                                   mailIncomingPasswords,
                                                                   mailOutgoingProtocols,
                                                                   mailOutgoingHosts,
                                                                   mailOutgoingPorts,
                                                                   mailOutgoingLogins,
                                                                   mailOutgoingPasswords,
                                                                   friendlyNames,
                                                                   getMailAtLogins,
                                                                   LocalData.UserInfo.LoginID,
                                                                   updates.ToArray());

                for (int i = 0; i < currentList.Count; i++)
                {
                    currentList[i].ID = result.ChildResults[i].ID;
                    currentList[i].UpdateDate = result.ChildResults[i].UpdateDate;
                    currentList[i].CancelEdit();
                    currentList[i].BeginEdit();
                }

                if (Saved != null) Saved(currentList[0]);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                return false;
            }
        }

        #region Command

        [CommandHandler(UserCommonConstants.Command_MailSaveData)]
        public void Command_MailSaveData(object sender, EventArgs e)
        {
            Save();
        }

        #endregion

        #endregion

        #region IEditPart 成员

        public override void EndEdit()
        {
            bindingSource1.EndEdit();
        }

        public override bool SaveData()
        {
            return Save();
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        public override object DataSource
        {
            get
            {
                return bindingSource1.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }
        void BindingData(object data)
        {
            if (data == null) { this.bindingSource1.DataSource = typeof(UserMailAccountList); this.Enabled = false; }
            else if ((data as UserMailAccountList).IsValid == false) { this.Enabled = false; ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).EndEdit(); }
            else
            {
                UserMailAccountList userMailAccountList = data as UserMailAccountList;

                this.bindingSource1.DataSource = userMailAccountList;
                this.Enabled = true;
                ((ICP.Framework.CommonLibrary.Common.BaseDataObject)userMailAccountList).BeginEdit();

                if (string.IsNullOrEmpty(userMailAccountList.MailOutgoingLogin) && string.IsNullOrEmpty(userMailAccountList.MailOutgoingPassword))
                    chkSameAsIncoming.Checked = true;
                else
                    chkSameAsIncoming.Checked = false;
            }
        }

        #endregion

        #region IPart 成员
        UserList _parentList = null;
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key == "ParentList")
                {
                    _parentList = item.Value as UserList;
                    UserMailAccountList data = this.bindingSource1.DataSource as UserMailAccountList;

                    if (data==null || _parentList == null || _parentList.IsValid == false)
                        this.Enabled = false;
                }
            }
        }
        #endregion
    }
}
