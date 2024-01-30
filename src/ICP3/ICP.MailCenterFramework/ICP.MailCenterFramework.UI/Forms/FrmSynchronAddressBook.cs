using System.Windows.Forms;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;

namespace ICP.MailCenterFramework.UI
{
    public partial class FrmSynchronAddressBook : Form
    {
        #region 成员变量
        /// <summary>
        /// 邮件联系人服务
        /// </summary>
        private IBusinessContactService BusinessContactService;

        #region 服务-FCM接口
        /// <summary>
        /// 缓存数据库服务接口
        /// </summary>
        public IDataCacheOperationService DataCacheOperationService
        {
            get
            {
                IDataCacheOperationService temp = null;
                try
                {
                    temp = ServiceClient.GetService<IDataCacheOperationService>();
                }
                catch (System.Exception ex)
                {
                    ToolUtility.WriteLog("AssociatedBatch DataCacheOperationService", ex);
                    temp = null;
                }
                return temp;
            }
        }
        #endregion
        #endregion

        #region 构造函数
        public FrmSynchronAddressBook()
        {
            InitializeComponent();
            Load += FrmSynchronAddressBook_Load;
            btnStart.Click += btnStart_Click;
            btnClose.Click += btnClose_Click;
            Disposed += delegate
            {
                Load -= FrmSynchronAddressBook_Load;
                btnStart.Click -= btnStart_Click;
                btnClose.Click -= btnClose_Click;
            };
        } 
        #endregion

        #region 窗体事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        private void FrmSynchronAddressBook_Load(object sender, EventArgs e)
        {
            if (!OutlookUtility.IsEnglish)
            {
                Text = "同步ICP通讯录";
                btnStart.Text = "开始";
                btnClose.Text = "关闭";
            }
        }
        /// <summary>
        /// 开始同步
        /// </summary>
        private void btnStart_Click(object sender, EventArgs e)
        {
            //0.连接WCF服务
            try
            {
                BusinessContactService = ServiceClient.GetService<IBusinessContactService>();
            }
            catch (System.Exception)
            {
                BusinessContactService = null;
                lblStatus.Text = OutlookUtility.IsEnglish ? "ICP Service Unavailable!" : "ICP服务不可用!";
                btnStart.Enabled = false;
                btnClose.Enabled = true;
                return;
            }
            //1.按钮控制
            ButtonEnable(true);
            SynchronAddressBook();
            ButtonEnable(false);
        }
        /// <summary>
        /// 关闭窗体
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        } 
        #endregion

        #region 方法定义

        /// <summary>
        /// 按钮可用状态
        /// </summary>
        /// <param name="isRun">是否正在同步通讯录</param>
        private void ButtonEnable(bool isRun)
        {
            btnStart.Enabled = !isRun;
            btnClose.Enabled = !isRun;
        }

        /// <summary>
        /// 同步ICP通讯录
        /// </summary>
        private void SynchronAddressBook()
        {
            System.Diagnostics.Stopwatch totalWatch = null;
            System.Reflection.MethodBase methodother = null;
            int totalItem = 0;
            int itemIndex = 1;
            //0.变量定义
            //联系人
            ContactItem contact = null;
            //当前循环联系人个数
            //当前查询联系人
            string sSearchMailAdd = string.Empty;
            try
            {
                if (BusinessContactService == null)
                    return;
                totalWatch = StopwatchHelper.StartStopwatch();
                methodother = System.Reflection.MethodBase.GetCurrentMethod();

                

                //1.获取联系人文件夹目录
                MAPIFolder contactsFolder =
                    OutlookUtility.CurrentApplication.Session.GetDefaultFolder(OlDefaultFolders.olFolderContacts);
                //2.获取联系人选项
                Items contactItems = contactsFolder.Items;

                //3.1.显示正在查询联系人
                lblStatus.Text = OutlookUtility.IsEnglish
                    ? "Search contact information from the database..."
                    : "从数据库查询联系人信息...";
                lblStatus.Refresh();

                //3.2从数据库获取联系人列表
                List<EmailContactInfo> emailContacts = BusinessContactService.GetEmailContactList();
                //4.设置进度条最大值为联系人总数
                totalItem = progressBarSync.Maximum = emailContacts.Count;

                #region 5.循环获取到的联系人

                //5.循环所有联系人
                foreach (EmailContactInfo item in emailContacts)
                {
                    //5.1联系人在Outlook是否已经存在
                    bool isContains = true;
                    //5.2进度条赋值
                    progressBarSync.Value = itemIndex;
                    progressBarSync.Refresh();
                    //5.3当前同步联系人显示
                    lblStatus.Text =
                        string.Format(
                            (OutlookUtility.IsEnglish ? "Synchronizing Contact" : "正在同步联系人") + " {0}({1})...",
                            item.CnName, item.EnName);
                    lblStatus.Refresh();
                    //5.4查询联系人Email地址
                    sSearchMailAdd = String.Format("[Email1Address]='{0}'", item.Email1Address);
                    //5.5在通讯簿查找联系人
                    try
                    {
                        contact = (ContactItem) contactItems.Find(sSearchMailAdd);
                    }
                    catch
                    {
                        contact = null;
                    }
                    //5.6判断员工离职状态，离职将不执行保存操作或删除联系人信息
                    if (!item.IsInOffice)
                    {
                        if (contact != null)
                            contact.Delete();
                        itemIndex += 1;
                        continue;
                    }
                    if (contact == null) //在通讯簿不存在联系人则创建联系人
                    {
                        contact = (ContactItem) OutlookUtility.CurrentApplication.CreateItem(OlItemType.olContactItem);
                        isContains = false;
                    }
                    if (contact != null)
                    {
                        contact.FirstName = item.EnName;
                        contact.LastName = ""; //清空原有名称
                        if (item.CnName.Trim() != item.EnName.Trim()) //判断中文名称和英文名称是否相同：不同则获取中文名称
                            contact.FirstName += item.CnName;
                        string strTemp1 = "";  //部门
                        if (!string.IsNullOrEmpty(item.Department))
                        {
                            strTemp1 = item.Department.Substring(item.Department.LastIndexOf("->", StringComparison.Ordinal) + 2
                                , item.Department.Length - item.Department.LastIndexOf("->", StringComparison.Ordinal) - 2);
                        }
                        string strTemp2 = ""; //公司
                        if ((strTemp1 + "").Length != 0) //部门不为空
                        {
                            //移除部门后字符串
                            strTemp2 = item.Department.Replace("->" + strTemp1, "");
                            //获取公司
                            strTemp2 = strTemp2.Substring(strTemp2.LastIndexOf("->", StringComparison.Ordinal) + 2
                                , strTemp2.Length - strTemp2.LastIndexOf("->", StringComparison.Ordinal) - 2)
                                .Replace("公司", "");
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
                        contact.MobileTelephoneNumber = item.MobileTelephoneNumber; //手机号
                        contact.Email1Address = item.Email1Address; //邮箱地址
                        contact.Department = item.Department; //部门
                        contact.BusinessAddressStreet = item.BusinessAddressStreet; //街道
                        contact.JobTitle = item.JobTitle; //职务
                        //EmailDisplayName在新联系人保存时会自动赋值
                        if (!isContains)
                            contact.Save();
                        contact.Email1DisplayName = contact.FirstName + contact.LastName;
                        contact.Save();
                    }
                    
                    itemIndex += 1;
                }
                lblStatus.Text = OutlookUtility.IsEnglish ? "Synchronizing is Completed." : "同步完成";
                lblStatus.Refresh();

                #endregion
            }
            catch (System.Exception ex)
            {
                lblStatus.Text = string.Format(OutlookUtility.IsEnglish ? "Item:[{0}] Abnormal synchronization" : "第[{0}]项目同步出现异常", itemIndex);
                ToolUtility.WriteLog("Synchron AddressBook:Synchron", ex);
            }
            finally
            {
                contact = null;
                sSearchMailAdd = string.Empty;
                if (totalWatch != null && DataCacheOperationService != null)
                {
                    totalWatch.Stop();
                    string strOperationLog = string.Format("同步联系人：总计{0}项", totalItem);
                    DataCacheOperationService.WriteStopwatchLog(methodother.DeclaringType.FullName, "PLUGIN", strOperationLog, totalWatch.ElapsedMilliseconds.ToString());
                    totalWatch = null;
                }
            }
        }
        #endregion
    }
}
