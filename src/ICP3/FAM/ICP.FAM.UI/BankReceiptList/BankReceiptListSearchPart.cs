using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary;
using ICP.FAM.ServiceInterface.CompositeObjects;

namespace ICP.FAM.UI.BankReceiptList
{
    [ToolboxItem(false)]
    public partial class BankReceiptListSearchPart : BaseSearchPart
    {
        public BankReceiptListSearchPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                RemoveKeyDownHandle();
                OnSearched = null;
                RemoveKeyDownHandle();
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

        #region 初始化

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                SetKeyDownToSearch();
                InitControls();
                dmdDate.radioGroup1.SelectedIndex = 1;
            }
        }

        #region 响应回车事件

        private void SetKeyDownToSearch()
        {
            foreach (Control item in navBarGroupControlContainer2.Controls)
            {
                item.KeyDown += item_KeyDown;
            }
        }
        private void RemoveKeyDownHandle()
        {
            foreach (Control item in navBarGroupControlContainer2.Controls)
            {
                item.KeyDown -= item_KeyDown;
            }
        }
        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2)
                btnSearch.PerformClick();
        }

        #endregion

        #region 初始化控件

        private void InitControls()
        {
            FAMUtility.BindCheckComboBoxByCompany(chcCompany);


            dmdDate.IsEngish = LocalData.IsEnglish;
            dmdDate.radioGroup1.SelectedIndex = 2;

            //状态
            List<EnumHelper.ListItem<BankReceiptStatus>> status = EnumHelper.GetEnumValues<BankReceiptStatus>(LocalData.IsEnglish);
            cmbStatue.Properties.BeginUpdate();
            foreach (var item in status)
            {
                if (item.Value != BankReceiptStatus.Unknown)
                {
                    cmbStatue.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
                }
                else
                {
                    cmbStatue.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "ALL" : "所有", 0));
                }
            }
            cmbStatue.Properties.EndUpdate();
            cmbStatue.SelectedIndex = 0;
        }

        #endregion

        #endregion

        public override event SearchResultHandler OnSearched;

        /// <summary>
        /// 总行数
        /// </summary>
        private int totalRowCount
        {
            get;
            set;
        }


        public override object GetData()
        {
            try
            {
                BankReceiptSearchParameter searchParameter = new BankReceiptSearchParameter()
                {
                    ReceiptNO = txtReceiptNO.Text,
                    CompanyIDs = CompanyIDs.ToArray(),
                    Status = (BankReceiptStatus)cmbStatue.SelectedIndex,
                    IsValid = ckbValid.Checked,
                    FromDate = dmdDate.From,
                    ToDate = dmdDate.To,
                };
                List<BankReceiptListInfo> list = FinanceService.GetBankReceiptList(searchParameter);

                return list;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); return null; }
        }

        #region 数组类型
        public List<Guid> CompanyIDs
        {
            get
            {
                List<Guid> companyIDs = new List<Guid>();
                foreach (CheckedListBoxItem item in chcCompany.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                        companyIDs.Add(new Guid(item.Value.ToString()));
                }
                return companyIDs;
            }
        }
        
        #endregion
        

        #region 按钮事件
        #region 查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                totalRowCount = 0;

                if (OnSearched != null)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Searching......" : "正在查询......");

                    List<BankReceiptListInfo> list = GetData() as List<BankReceiptListInfo>;

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "total search " + list.Count.ToString() + " data." : "总共查询到 "
                                                    + list.Count.ToString() + " 条数据.");

                    OnSearched(this, list);
                }
            }
        }
        #endregion



        #region 清空
        private void btnClare_Click(object sender, EventArgs e)
        {
            ckbValid.Checked = null;
            chcCompany.Text = string.Empty;

            cmbStatue.SelectedIndex = 0;
        }
        #endregion

        private void navBarSearch_Click(object sender, EventArgs e)
        {

        }

        #endregion

    }
}
