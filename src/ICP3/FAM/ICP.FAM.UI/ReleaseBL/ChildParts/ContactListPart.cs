using System;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;

using System.Text.RegularExpressions;
namespace ICP.FAM.UI.ReleaseBL
{
    public partial class ContactListPart : BaseEditPart
    {  
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        public ContactListPart()
        {
            InitializeComponent(); Enabled = false;
            Disposed += delegate {
                contactlist = null;
                bllist = null;
                if (WorkItem != null)
                {
                    WorkItem.Items.Remove(this);
                    WorkItem = null;
                }
            
            };
            if (LocalData.IsEnglish == false)
                SetCnText();
        }

        public void SetCnText()
        {
            labEmail.Text = "联系人邮箱";
            labtxt1.Text = "联系人列表";

            btnAdd.Text = "新增";
            btnRemove.Text = "移除";
            btnSave.Text = "保存";
        }

        protected override void OnLoad(EventArgs e)
        {
            panelControl1.Click += delegate { panelControl1.Focus(); };
            base.OnLoad(e);
        }
        ReleaseBLList bllist;
        ContactList contactlist;
        public override object DataSource
        {
            get { return bllist; }
            set { BindSource(value); }
        }

        void BindSource(object value)
        {
            listboxEmailAddress.Items.Clear();
            bllist = value as ReleaseBLList;
            if (bllist == null || bllist.AgentID == null)
            {
                Enabled = false;
                return;
            }
            if (bllist.AgentID != Guid.Empty) Enabled = true;

            contactlist = FinanceService.GetContactListOfAgent(bllist.AgentID);
            speTime.Value = contactlist!=null ?contactlist.EmailSendTime:24;
            if (contactlist == null)
                return;
            string[] emailAddresses = contactlist.ContactEmail.Split(';');
            foreach (string address in emailAddresses)
            {
                if (string.IsNullOrEmpty(address)) continue;
                listboxEmailAddress.Items.Add(address);
            }
        }

        #region Email验证
        /// <summary>
        /// 验证邮箱是否有效
        /// </summary>
        /// <param name="str">需要验证的字符串</param>
        /// <returns>True/False</returns>
        private bool RegexMailValid(string str)
        {
            string match = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
            if (Regex.IsMatch(str, match))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Btn事件
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string address = txtEmail.Text;
            string[] addressList = address.Split(';');
            //验证Email正确性
            foreach (string item in addressList)
            {
                if (string.IsNullOrEmpty(item.Trim()))
                    continue;
                if (!RegexMailValid(item))
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "Please verify the correctness of the email！" : "请验证邮箱的正确性！");
                    return;
                }
            }
            //添加到列表
            foreach (string item in addressList)
            {
                if (string.IsNullOrEmpty(item.Trim()))
                    continue;
                if (!listboxEmailAddress.Items.Contains(item) && !string.IsNullOrEmpty(item))
                {
                    listboxEmailAddress.Items.Insert(0, item);
                }
            }
            listboxEmailAddress.SelectedIndex = 0;
            txtEmail.Text = string.Empty;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (listboxEmailAddress.SelectedItem == null)
            {
                return;
            }
            listboxEmailAddress.Items.Remove(listboxEmailAddress.SelectedItem);
            if (listboxEmailAddress.Items.Count > 0)
            {
                listboxEmailAddress.SelectedItem = listboxEmailAddress.Items[0];
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        bool Save()
        {
            if (bllist.AgentID == null)
                return false;
            Guid id = Guid.NewGuid();//新增
            contactlist = FinanceService.GetContactListOfAgent(bllist.AgentID);
            if (contactlist != null)
            {
                //修改 
                id = contactlist.ID;
            }
            string contactEmail = string.Empty;
            foreach (string email in listboxEmailAddress.Items)
            {
                contactEmail += email + ";";
            }
            try
            {
                FinanceService.SaveContactListOfAgent(id,
                    bllist.AgentID,
                    contactEmail,
                    (int)speTime.Value,
                    LocalData.UserInfo.LoginID);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
                return false;
            }
        }
        #endregion

    }
}
