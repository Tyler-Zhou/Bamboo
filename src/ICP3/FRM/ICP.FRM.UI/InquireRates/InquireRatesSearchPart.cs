#region Comment

/*
 * 
 * FileName:    InquireRatesSearchPart.cs
 * CreatedOn:   
 * CreatedBy:   
 * 
 * 
 * Description：
 *      ->询价查询
 * History：
 *      ->
 * 
 * 
 * 
 */

#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface;
using ICP.Framework.ClientComponents.Service;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.Utility;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.FRM.UI.InquireRates
{
    /// <summary>
    /// 询价查询面板
    /// </summary>
    [ToolboxItem(false)]
    public partial class InquireRatesSearchPart : BaseSearchPart
    {
        #region Service
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        /// <summary>
        /// 用户服务
        /// </summary>
        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        #endregion

        #region 委托事件
        /// <summary>
        /// 设置TabPage可用状态
        /// </summary>
        [EventPublication(InquireRatesCommandConstants.Command_SetTabVisible)]
        public event EventHandler<DataEventArgs<InquierType>> SetTabVisibleEvent;

        /// <summary>
        /// 查询数据
        /// </summary>
        [EventPublication(InquireRatesCommandConstants.Command_SearchData)]
        public event EventHandler<DataEventArgs<InquireRatesSearchParameter>> SearchDataEvent;

        public override event SearchResultHandler OnSearched;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public InquireRatesSearchPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                SetTabVisibleEvent = null;
                SearchDataEvent = null;
                OnSearched = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };

            if (!DesignMode) { InitMessage(); }
        } 
        #endregion

        #region Override
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            txtPOD.ToolTip = txtPOL.ToolTip = txtDelivery.ToolTip
            = NativeLanguageService.GetText(this, "SearchBoxToolTip");

            txtInquireOrRespondBy.ToolTip = NativeLanguageService.GetText(this, "InputMatchToolTip");
            //txtZipCode.ToolTip = NativeLanguageService.GetText(this, "ZipCodeToolTip");
            Utility.SearchPartKeyEnterToSearch(new List<Control> 
            {
                txtSNo,txtComm,txtDelivery ,txtInquireOrRespondBy,txtPOD,txtPOL
            }, btnSearch);
            InitControls();
        }

        
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public override object GetData()
        {
            DateTime? from = null, to = null;
            //if (chkInquireDate.Checked)
            //{
            //    //from = dteFrom.DateTime.Date;
            //    //to = Utility.GetEndDate(dteTo.DateTime);
            //}
            InquierType? inquierType = null;
            if (cmbType.EditValue != null && cmbType.EditValue != DBNull.Value)
                inquierType = (InquierType)cmbType.EditValue;
            try
            {
                if (inquierType == InquierType.OceanRates)
                {
                    //InquierOceanRatesResult result = ipService.GetInquireOceanRateList(txtPOL.Text.Trim()
                    //     , txtDelivery.Text.Trim()
                    //                                                       , txtPOD.Text.Trim()

                    //                                                       , txtDiscussing.Text.Trim()
                    //                                                       , txtComm.Text.Trim()
                    //                                                       , txtInquireOrRespondBy.Text.Trim()
                    //                                                       , chkUnReply.Checked
                    //                                                       , from, to
                    //                                                       , LocalData.UserInfo.LoginID);

                }
                return null;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); return null; }
        }

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }
        #endregion

        #region 窗体事件
        /// <summary>
        /// 查询
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (cmbType.EditValue != null && SetTabVisibleEvent != null)     //说明不是选择All
                {
                    SetTabVisibleEvent(this, new DataEventArgs<InquierType>((InquierType)cmbType.EditValue));
                }

                if (SearchDataEvent != null)
                {
                    InquireRatesSearchParameter para = new InquireRatesSearchParameter();
                    //para.type = (InquierType)cmbType.EditValue;
                    para.No = txtSNo.Text.Trim();       //No
                    para.pol = txtPOL.Text.Trim();
                    para.pod = txtPOD.Text.Trim();
                    para.delivery = txtDelivery.Text.Trim();
                    para.commodity = txtComm.Text.Trim();
                    if (txtInquireOrRespondBy.EditValue != null && txtInquireOrRespondBy.EditValue.ToString().Length > 0)
                    {
                        para.inquireOrRespondBy = new Guid(txtInquireOrRespondBy.EditValue.ToString());
                    }

                    para.isUnReply = chkUnReply.Checked;
                    para.durationFrom = fromToDateMonthControl1.From;
                    para.durationTo = fromToDateMonthControl1.To;
                    SearchDataEvent(this, new DataEventArgs<InquireRatesSearchParameter>(para));
                }

                ////if (OnSearched != null)
                ////    OnSearched(this, GetData());
            }
        }

        /// <summary>
        /// 清空查询
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            //chkInquireDate.Checked = false;
            foreach (Control item in navBarGroupBase.Controls)
            {
                if (item is TextEdit
                    && (item is SpinEdit) == false
                    && (item is ComboBoxEdit) == false

                    && item.Enabled == true
                    && (item as TextEdit).Properties.ReadOnly == false)
                    item.Text = string.Empty;
            }

            cmbType.SelectedIndex = 0;


            //dteFrom.DateTime = Utility.GetEndDate(DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).AddDays(-7));
            //dteTo.DateTime = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).AddDays(7);
        }

        /// <summary>
        /// 时间选择
        /// </summary>
        private void chkDate_CheckedChanged(object sender, EventArgs e)
        {
            //this.dteFrom.Enabled = dteTo.Enabled = chkInquireDate.Checked;
        }
        /// <summary>
        /// 查询面板大小改变
        /// </summary>
        private void pnlScroll_SizeChanged(object sender, EventArgs e)
        {
            bcMain.Width = pnlScroll.Width - 15;
        }

        #endregion

        #region  方法定义

        /// <summary>
        /// 注册Message
        /// </summary>
        private void InitMessage()
        {
            RegisterMessage("SearchBoxToolTip", "Please input Code, Chinese name or English name.");
            RegisterMessage("InputMatchToolTip", "Input match.");
            RegisterMessage("ZipCodeToolTip", "Here applies only to Trucking Rates.");
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            chkUnReply.Checked = true;

            #region RateType
            List<EnumHelper.ListItem<InquierType>> inquierTypes = EnumHelper.GetEnumValues<InquierType>(LocalData.IsEnglish);
            cmbType.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "ALL" : "ALL", InquierType.None));
            foreach (var item in inquierTypes)
            {
                if (item.Value == InquierType.None) continue;
                cmbType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }

            cmbType.SelectedIndex = 0;
            #endregion

            #region  Inquire Or Respond By

            txtInquireOrRespondBy.ShowSelectedValue(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName);
            Utility.SetEnterToExecuteOnec(txtInquireOrRespondBy, delegate
            {
                List<User2OrganizationJobList> jobList = UserService.GetUser2OrganizationJobList(LocalData.UserInfo.LoginID);
                bool isManager = false;
                bool isFarEastBusinessMan = false;
                bool isPortBusinessMan = false;

                foreach (User2OrganizationJobList job in jobList)
                {
                    if (job.OrganizationJobName.Contains("经理"))
                    {
                        isManager = true;
                    }
                    else if (job.OrganizationJobName.Contains("远东区") && job.OrganizationJobName.Contains("商务员"))
                    {
                        isFarEastBusinessMan = true;
                    }
                    else if (job.OrganizationJobName.Contains("商务员"))
                    {
                        isPortBusinessMan = true;
                    }
                }

                Dictionary<Guid, UserList> userSource = new Dictionary<Guid, UserList>();
                if (isManager)
                {
                    txtInquireOrRespondBy.ReadOnly = false;
                    List<OrganizationList> userCompanyList = UserService.GetUserCompanyList(LocalData.UserInfo.LoginID, OrganizationType.Department);

                    foreach (var item in userCompanyList)
                    {
                        List<UserList> userList = UserService.GetUserListBySearch(item.ID, string.Empty, string.Empty, true, true, 0);
                        foreach (var user in userList)
                        {
                            if (!userSource.Keys.Contains(user.ID))
                            {
                                userSource.Add(user.ID, user);
                            }
                        }
                    }
                }
                else if (isFarEastBusinessMan)
                {
                    txtInquireOrRespondBy.ReadOnly = false;
                    List<OrganizationList> userCompanyList = UserService.GetUserCompanyList(LocalData.UserInfo.LoginID, OrganizationType.Company);

                    foreach (var item in userCompanyList)
                    {
                        List<UserList> userList = UserService.GetUserListBySearch(item.ID, "商务员", string.Empty, true, true, 0);
                        foreach (var user in userList)
                        {
                            if (!userSource.Keys.Contains(user.ID))
                            {
                                userSource.Add(user.ID, user);
                            }
                        }
                    }
                }
                else if (isPortBusinessMan)
                {
                    txtInquireOrRespondBy.ReadOnly = false;
                    List<OrganizationList> userCompanyList = UserService.GetUserCompanyList(LocalData.UserInfo.LoginID, OrganizationType.Department);

                    foreach (var item in userCompanyList)
                    {
                        List<UserList> userList = UserService.GetUserListBySearch(item.ID, string.Empty, string.Empty, true, true, 0);
                        foreach (var user in userList)
                        {
                            if (!userSource.Keys.Contains(user.ID))
                            {
                                userSource.Add(user.ID, user);
                            }
                        }
                    }
                }
                else
                {
                    txtInquireOrRespondBy.ReadOnly = true;
                }

                if (userSource.Values != null)
                {
                    Dictionary<string, string> col = new Dictionary<string, string>();
                    col.Add("EName", "Name");
                    col.Add("Code", "Code");
                    txtInquireOrRespondBy.InitSource<UserList>(userSource.Values.OrderBy(i => i.EName).ToList(), col, "EName", "ID");
                }
            });
            #endregion

            fromToDateMonthControl1.dteFrom.DateTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).AddMonths(-3);
            fromToDateMonthControl1.dteTo.DateTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
        }

        #region RefreshData

        [CommandHandler(InquireRatesCommandConstants.Command_RefreshData)]
        public void Command_RefreshData(object sender, EventArgs e)
        {
            btnSearch_Click(null, null);
        }
        #endregion

        #endregion
    }

    /// <summary>
    /// 查询参数类
    /// </summary>
    public class InquireRatesSearchParameter
    {
        /// <summary>
        /// No
        /// </summary>
        public string No { get; set; }
        /// <summary>
        /// 询价类型(1.海运,2.空运,3拖车
        /// </summary>
        public InquierType type { get; set; }
        public string pol { get; set; }
        public string delivery { get; set; }
        public string pod { get; set; }
        public string commodity { get; set; }
        //public string zipcode { get; set; }
        public Guid? inquireOrRespondBy { get; set; }
        public bool? isUnReply { get; set; }
        public DateTime? durationFrom { get; set; }
        public DateTime? durationTo { get; set; }
        /// <summary>
        /// 高级查询
        /// </summary>
        public string StrQuery { get; set; }
    }
}
