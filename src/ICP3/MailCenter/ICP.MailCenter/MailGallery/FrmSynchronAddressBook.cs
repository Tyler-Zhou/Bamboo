
using System;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Office.Interop.Outlook;

namespace ICP.MailCenter.UI
{
    public partial class FrmSynchronAddressBook : XtraForm
    {
        /// <summary>
        /// 邮件联系人服务
        /// </summary>
        public IBusinessContactService BusinessContactService
        {
            get
            {
                return ServiceClient.GetService<IBusinessContactService>();
            }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public FrmSynchronAddressBook()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmSynchronAddressBook_Load);
            this.btnStart.Click += new EventHandler(btnStart_Click);
            this.btnClose.Click+=new EventHandler(btnClose_Click);
            this.Disposed += delegate
            {
                this.Load -= new EventHandler(FrmSynchronAddressBook_Load);
                this.btnStart.Click -= new EventHandler(btnStart_Click);
                this.btnClose.Click -= new EventHandler(btnClose_Click);
            };
        }

        private void FrmSynchronAddressBook_Load(object sender, EventArgs e)
        {
            btnStart.Text = LocalData.IsEnglish ? "Start" : "开始";
            btnClose.Text = LocalData.IsEnglish ? "Close" : "关闭";
            lblStatus.Text = LocalData.IsEnglish ? "Synchron ICP Contacts" : "同步ICP通讯录";
            lblStatus.Refresh();

        }

        /// <summary>
        /// 开始同步
        /// </summary>
        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnClose.Enabled = false;
            SynchronAddressBook();
            btnStart.Enabled = true;
            btnClose.Enabled = true;
        }
        /// <summary>
        /// 关闭窗体
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 同步ICP通讯录
        /// </summary>
        private void SynchronAddressBook()
        {
            try
            {
                //0.变量定义
                //联系人
                ContactItem contact=null;
                //当前循环联系人个数
                int i = 1;
                //当前查询联系人
                string sSearchMailAdd = "";

                //1.获取联系人文件夹目录
                MAPIFolder contactsFolder = ClientUtility.OlApp.Session.GetDefaultFolder(OlDefaultFolders.olFolderContacts);
                //2.获取联系人选项
                Items contactItems = contactsFolder.Items;

                //3.1.显示正在查询联系人
                lblStatus.Text = LocalData.IsEnglish ? "Search contact information from the database..." : "从数据库查询联系人信息...";
                lblStatus.Refresh();

                //3.2从数据库获取联系人列表
                List<EmailContactInfo> emailContacts = BusinessContactService.GetEmailContactList();
                //4.设置进度条最大值为联系人总数
                progressBarSync.Properties.Maximum = emailContacts.Count;

                #region 5.循环获取到的联系人
                //5.循环所有联系人
                foreach (EmailContactInfo item in emailContacts)
                {
                    //5.1联系人在Outlook是否已经存在
                    bool isContains = true;
                    //5.2进度条赋值
                    progressBarSync.EditValue = i;
                    progressBarSync.Refresh();
                    //5.3当前同步联系人显示
                    lblStatus.Text = string.Format((LocalData.IsEnglish ? "Synchronizing Contact" : "正在同步联系人") + " {0}({1})...", item.CnName, item.EnName);
                    lblStatus.Refresh();
                    //5.4查询联系人Email地址
                    sSearchMailAdd = String.Format("[Email1Address]='{0}'", item.Email1Address);
                    //5.5在通讯簿查找联系人
                    contact = (ContactItem)contactItems.Find(sSearchMailAdd);
                    //5.6判断员工离职状态，离职将不执行保存操作或删除联系人信息
                    if (!item.IsInOffice)
                    {
                        if (contact != null)
                            contact.Delete();
                        i += 1;
                        continue;
                    }
                    if (contact == null)    //在通讯簿不存在联系人则创建联系人
                    {
                        contact = (ContactItem)ClientUtility.OlApp.CreateItem(OlItemType.olContactItem);
                        isContains = false;
                    }

                    contact.FirstName = item.EnName;
                    contact.LastName = "";//清空原有名称
                    if (item.CnName.Trim() != item.EnName.Trim()) //判断中文名称和英文名称是否相同：不同则获取中文名称
                        contact.FirstName += item.CnName;
                    //部门
                    string strTemp1 = item.Department.Substring(item.Department.LastIndexOf("->") + 2, item.Department.Length - item.Department.LastIndexOf("->") - 2);
                    string strTemp2 = ""; //公司
                    if ((strTemp1 + "").Length != 0) //部门不为空
                    {
                        //移除部门后字符串
                        strTemp2 = item.Department.Replace("->" + strTemp1, "");
                        //获取公司
                        strTemp2 = strTemp2.Substring(strTemp2.LastIndexOf("->") + 2, strTemp2.Length - strTemp2.LastIndexOf("->") - 2).Replace("公司", "");
                        //长度大于20，且包含括号
                        if ((("" + strTemp2).Length > 20) && (strTemp2.Contains("(")))
                        {
                            strTemp2 = strTemp2.Substring(strTemp2.IndexOf('(') + 1);
                            strTemp2 = strTemp2.Replace(")", "");
                        }
                    }

                    if ((strTemp1 + strTemp2 + "").Length != 0)
                    {
                        //FirstName=公司+部门
                        contact.LastName += "(" + strTemp2 + strTemp1 + ")";
                    }
                    //联系人各字段赋值
                    contact.BusinessTelephoneNumber = item.BusinessTelephoneNumber; //座机
                    contact.MobileTelephoneNumber = item.MobileTelephoneNumber;     //手机号
                    contact.Email1Address = item.Email1Address;                     //邮箱地址
                    contact.Department = item.Department;                           //部门
                    contact.BusinessAddressStreet = item.BusinessAddressStreet;     //街道
                    contact.JobTitle = item.JobTitle;                               //职务
                    //EmailDisplayName在新联系人保存时会自动赋值
                    if (!isContains)
                        contact.Save();
                    contact.Email1DisplayName = contact.FirstName + contact.LastName;
                    contact.Save();
                    i += 1;
                }
                lblStatus.Text = LocalData.IsEnglish ? "Synchronizing is Completed." : "同步完成";
                lblStatus.Refresh(); 
                #endregion
            }
            catch (System.Exception ex)
            {
                ICP.Framework.CommonLibrary.Logger.Log.Error(ex.Message);
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                this.Close();
            }
        }


    }
}
