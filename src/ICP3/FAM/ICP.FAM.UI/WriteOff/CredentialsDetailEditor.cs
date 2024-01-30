using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;

namespace ICP.FAM.UI.WriteOff
{
    [ToolboxItem(false)]
    public partial class CredentialsDetailEditor : XtraForm
    {
        public CredentialsDetailEditor()
        {
            InitializeComponent();
            Disposed += delegate {
                gcMain.DataSource = null;
                bsList.DataSource = null;
                bsList.Dispose();
                if (WorkItem != null)
                {
                    WorkItem.Items.Remove(this);
                    WorkItem = null;
                }
            };
        }

        #region 服务
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

        #endregion

        #region 属性

        /// <summary>
        /// 核销ID
        /// </summary>
        public Guid WriteOffID
        {
            get;
            set;
        }

        /// <summary>
        /// 凭证号
        /// </summary>
        public string VoucherSeqNo
        {
            get;
            set;
        }

        private List<CredentialsDetailList> DataList
        {
            get
            {
                return bsList.DataSource as List<CredentialsDetailList>;
            }
        }

        private CredentialsDetailList CurrentRow
        {
            get
            {
                return bsList.Current as CredentialsDetailList;
            }
        }

        #endregion

        #region 关闭

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().Close();
        }

        #endregion

        #region 初始化

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                InitControls();
                BindDataList();
            }
        }

        private void InitControls()
        {
            labTotal.Text = LocalData.IsEnglish ? "Total:" : "合计:";
            if (string.IsNullOrEmpty(VoucherSeqNo) == false)  //当凭证号不为空，即有凭证号
            {
                bAutoGen.Enabled = false;
            }

            FAMUtility.ShowGridRowNo(gvMain);
        }

        private void BindDataList()
        {
            labTip.Text = string.Empty;

            List<CredentialsDetailList> list = FinanceService.GetCredentialsDetailList(WriteOffID);
            bsList.DataSource = list;
            bsList.ResetBindings(false);

            //txtDebit.Text = list.Sum(p => p.Debit).ToString("n");
            //txtCredit.Text = list.Sum(p => p.Credit).ToString("n");
            decimal totalDebit = list.Sum(p => p.Debit);
            decimal totalCredit = list.Sum(p => p.Credit);
            txtDebit.Text = totalDebit.ToString("n");
            txtCredit.Text = totalCredit.ToString("n");

            if (totalDebit != totalCredit)
            {
                labTip.Text = LocalData.IsEnglish ? "Credit must be equal to Debit!" : "借方和贷方不平衡!";
            }

            foreach (var item in list)
            {
                if (string.IsNullOrEmpty(item.GL))
                {
                    labTip.Text += " " + (LocalData.IsEnglish ? "GL can not be empty!" : "会计科目不能为空!");
                    break;
                }
            }
        }

        #endregion

        #region 自动生成

        private void bAutoGen_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    FinanceService.BuildRPLedgers(WriteOffID, LocalData.UserInfo.LoginID);
                    XtraMessageBox.Show(LocalData.IsEnglish ? "Successfully generated." : "生成成功!",
                                                  LocalData.IsEnglish ? "Tip" : "提示");

                    BindDataList();
                    bAutoGen.Enabled = false;
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
                }
            }
        }

        #endregion

        #region 锁定账户

        private void ckbLock_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string msg = ckbLock.Checked == true ? "Lock current check ?" : "Unlock current check ?";
                //Guid userid = new Guid(initialzeService.GetUserInfo().UserId);   
                if (XtraMessageBox.Show(msg, "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //financeService.LockCheckAccounts(this._check.Id, chkLockAccounts.Checked, userid);
                    //_check.AccountsLocked = chkLockAccounts.Checked;
                }
                //chkLockAccounts.Checked = _check.AccountsLocked;
            }
        }
        #endregion
    }
}
