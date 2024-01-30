using DevExpress.XtraEditors;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace ICP.Business.Common.UI.QuotedPrice
{
    /// <summary>
    /// 报价查询面板
    /// </summary>
    [ToolboxItem(false)]
    public partial class QuotedPriceSearchPart : BaseSearchPart
    {
        #region Services & Property & Delegate
        #region Services
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        /// <summary>
        /// 报价服务
        /// </summary>
        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }

        /// <summary>
        /// 菜单配置管理服务
        /// </summary>
        public IPermissionService PermissionService
        {
            get
            {
                return ServiceClient.GetService<IPermissionService>();
            }
        }

        /// <summary>
        /// 报价UI数据服务
        /// </summary>
        public QuotedPriceUIDataHelper QuotedPriceUIDataServices
        {
            get
            {
                return ClientHelper.Get<QuotedPriceUIDataHelper, QuotedPriceUIDataHelper>();
            }
        } 
        #endregion

        #region Delegate
        /// <summary>
        /// 查询之前
        /// </summary>
        public event CancelEventHandler BeForeSearchData;
        /// <summary>
        /// 查询后
        /// </summary>
        public override event SearchResultHandler OnSearched;
        #endregion
        #endregion

        #region Init
        /// <summary>
        /// 报价查询面板
        /// </summary>
        public QuotedPriceSearchPart()
        {
            InitializeComponent();
            InitControls();
            RegisterEvent();
            Disposed += (sender, arg) =>
            {

                UnRegisterEvent();
            };
        } 
        #endregion

        #region Control Event
        /// <summary>
        /// 查询
        /// </summary>
        void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                bool isCancel = false;
                if (BeForeSearchData != null)
                {
                    CancelEventArgs ce = new CancelEventArgs();
                    BeForeSearchData(this, ce);
                    isCancel = ce.Cancel;
                }

                if (isCancel) return;

                if (OnSearched != null)
                {
                    using (new CursorHelper())
                    {
                        OnSearched(this, GetData());
                    }
                }
            }
        }
        /// <summary>
        /// 清空
        /// </summary>
        void btnClean_Click(object sender, EventArgs e)
        {
            chkDate.Checked = false;
            foreach (Control item in navBarControlBase.Controls)
            {
                if (item is TextEdit
                    && (item is SpinEdit) == false
                    && (item is ComboBoxEdit) == false

                    && item.Enabled == true
                    && (item as TextEdit).Properties.ReadOnly == false)
                    item.Text = string.Empty;
            }

            dteFrom.DateTime = DateTime.Now;
            dteTo.DateTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).AddDays(14);
        }

        /// <summary>
        /// 回车时调用查询按钮点击事件
        /// </summary>
        private void KeyEventHandle(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnSearch.PerformClick();
        }
        /// <summary>
        /// 查询条件更改
        /// </summary>
        void chkDate_CheckedChanged(object sender, EventArgs e)
        {
            dteFrom.Enabled = dteTo.Enabled = chkDate.Checked;
        }
        #endregion

        #region Custom Method
        /// <summary>
        /// 初始化控件数据
        /// </summary>
        private void InitControls()
        {
            SetLanguage();

            #region Init Setting
            new List<Control> { stxtNO, stxtCustomerName, txtQuoteBy }.BatchAddKeyEvent(KeyEventHandle);
            #endregion

            #region Init Data

            #region Quote By
            txtQuoteBy.EditText = LocalData.UserInfo.LoginName;
            txtQuoteBy.EditValue = LocalData.UserInfo.LoginID;

            //报价人
            txtQuoteBy.SetEnterToExecuteOnec(delegate
            {
                List<ModuleUserList> users = PermissionService.GetModuleUserList(BusinessCommandConstants.QuotedPrice_List, null, 0);
                ModuleUserList currentUser = users.Find(item => item.ID == LocalData.UserInfo.LoginID);
                Dictionary<string, string> col = new Dictionary<string, string>
                    {
                        {"EName", "Name"},
                        {"Code", "Code"}
                    };
                txtQuoteBy.InitSource(users, col, "EName", "ID");
            }); 
            #endregion

            #region Duration
            dteFrom.DateTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            dteTo.DateTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).DateAttachEndTime().AddDays(14);
            chkDate.Checked = dteFrom.Enabled = dteTo.Enabled = false;
            #endregion 
            #endregion
        }

        private void SetLanguage()
        {
            if (LocalData.IsEnglish) return;
            lwchkIsValid.CheckedText = "有效";
            lwchkIsValid.NULLText = "全部";
            lwchkIsValid.UnCheckedText = "无效";

            lwchkIsSure.CheckedText = "已确认";
            lwchkIsSure.NULLText = "全部";
            lwchkIsSure.UnCheckedText = "未确认";

            
        }
        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvent()
        {
            btnSearch.Click += btnSearch_Click;
            btnClean.Click += btnClean_Click;
            chkDate.CheckedChanged += chkDate_CheckedChanged;
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        private void UnRegisterEvent()
        {
            btnSearch.Click -= btnSearch_Click;
            btnClean.Click -= btnClean_Click;
            chkDate.CheckedChanged -= chkDate_CheckedChanged;
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public override object GetData()
        {
            try
            {
                DateTime? from = null, to = null;
                if (chkDate.Checked)
                {
                    from = dteFrom.DateTime.Date;
                    to = dteTo.DateTime.DateAttachEndTime();
                }
                Guid? quoteBy = null;
                if (txtQuoteBy.EditValue != null && txtQuoteBy.EditValue != DBNull.Value)
                    quoteBy = new Guid(txtQuoteBy.EditValue.ToString());

                List<QuotedPriceOrderList> list = FCMCommonService.GetQuotedPriceOrderList(stxtNO.Text.Trim()
                    , stxtCustomerName.Text.Trim()
                    , lwchkIsSure.Checked
                    , lwchkIsValid.Checked
                    , quoteBy
                    , from
                    , to
                    ,int.Parse(snumMax.Value.ToString(CultureInfo.InvariantCulture)));

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "total search " + list.Count.ToString(CultureInfo.InvariantCulture) + " data." : "总共查询到 " + list.Count.ToString(CultureInfo.InvariantCulture) + " 条数据.");
                return list;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); return null; }

        }
        #endregion
    }
}
