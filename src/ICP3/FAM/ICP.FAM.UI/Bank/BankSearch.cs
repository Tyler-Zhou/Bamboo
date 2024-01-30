using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using ICP.FAM.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FAM.UI
{
    [ToolboxItem(false)]
    public partial class BankSearch : BaseSearchPart
    {
        public BankSearch()
        {
            InitializeComponent();
            Disposed += delegate {
                OnSearched = null;
                RemovekKeyDownHandle();
                if (WorkItem != null)
                {
                    WorkItem.Items.Remove(this);
                    WorkItem = null;
                }
            
            };
        }

        #region 服务

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
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
            }
        }

        private void SetKeyDownToSearch()
        {
            foreach (Control item in bgcBase.Controls)
            {
                item.KeyDown += new KeyEventHandler(item_KeyDown);
            }
        }
        private void RemovekKeyDownHandle()
        {
            foreach (Control item in bgcBase.Controls)
            {
                item.KeyDown -= item_KeyDown;
            }
        }

        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2)
            {
                btnSearch.PerformClick();
            }
            else if (e.KeyCode == Keys.F3)
            {
                btnClare.PerformClick();
            }
        }
        private void InitControls()
        {
            FAMUtility.BindCheckComboBoxByCompany(chkcmbCompany);
        }
        #endregion

        #region 重写

        public override event SearchResultHandler OnSearched;

        public override void Init(IDictionary<string, object> values)
        {
            if (values == null)
            {
                return;
            }
            txtBankCName.Text = string.Empty;
            txtShortName.Text = string.Empty;
            txtBankEName.Text = string.Empty;

            foreach (var item in values)
            {
                string value = item.Value == null ? string.Empty : item.Value.ToString();
                switch (item.Key)
                {
                    case "CShortName":
                        txtShortName.Text = value;
                        break;
                    case "CName":
                        txtBankCName.Text = value;
                        break;
                    case "EName":
                        txtBankEName.Text = value;
                        break;
                }
            }



        }
        #endregion

        #region 私有字段
        /// <summary>
        /// 总行数
        /// </summary>
        private int totalRowCount
        {
            get;
            set;
        }

        /// <summary>
        /// 公司ID集合
        /// </summary>
        public List<Guid> CompanyIDs
        {
            get
            {
                List<Guid> companyIDs = new List<Guid>();
                foreach (CheckedListBoxItem item in chkcmbCompany.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                        companyIDs.Add(new Guid(item.Value.ToString()));
                }
                return companyIDs;
            }
        }
        #endregion

        #region 查询

        public override void RaiseSearched(object data)
        {
            DataPageInfo dataPageInfo = data as DataPageInfo;
            searchParameter.DataPageInfo = dataPageInfo;
            if (OnSearched != null)
                OnSearched(this, GetData());
        }

        BankSearchParameter searchParameter = new BankSearchParameter();
        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                totalRowCount = 0;

                searchParameter.companyIds = CompanyIDs.ToArray();
                searchParameter.cnName = txtBankCName.Text.Trim();
                searchParameter.enName = txtBankEName.Text.Trim();
                searchParameter.simpleName = txtShortName.Text.Trim();
                searchParameter.isValid = ckbValid.Checked;
                searchParameter.DataPageInfo.PageSize = (int)numMaxRow.Value;
                searchParameter.DataPageInfo.CurrentPage = 1;
                if (string.IsNullOrEmpty(searchParameter.DataPageInfo.SortByName))
                {
                    searchParameter.DataPageInfo.SortByName = "CreateDate";
                    searchParameter.DataPageInfo.SortOrderType = SortOrderType.Desc;
                }

                if (OnSearched != null)
                {
                    PageList list = GetData() as PageList;
                    if (list != null && list.DataPageInfo != null)
                    {
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "total search " + list.DataPageInfo.TotalCount.ToString()+" data." : "总共查询到 "
                                                    + list.DataPageInfo.TotalCount.ToString() + " 条数据.");
                    }
                    OnSearched(this, list);
                }
            }
        }
        public override object GetData()
        {
            try
            {

                PageList list = FinanceService.GetBankList(
                    searchParameter.companyIds,
                    searchParameter.simpleName,
                    searchParameter.cnName,
                    searchParameter.enName,
                    searchParameter.isValid,
                    searchParameter.DataPageInfo,
                    LocalData.IsEnglish);
                return list;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); return null; }

        }

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        #endregion

        #region 清空
        private void btnClare_Click(object sender, EventArgs e)
        {
            txtBankEName.Text = string.Empty;
            txtShortName.Text = string.Empty;
            txtBankCName.Text = string.Empty;

            ckbValid.Checked = null;
            numMaxRow.Value = 100;
        }
        #endregion

    }

    /// <summary>
    /// 查询实体
    /// </summary>
    public class BankSearchParameter
    {
        public BankSearchParameter() { DataPageInfo = new DataPageInfo(); }
        public Guid[] companyIds { get; set; }
        public string simpleName { get; set; }
        public string cnName { get; set; }
        public string enName { get; set; }
        public bool? isValid { get; set; }
        public DataPageInfo DataPageInfo { get; set; }
    }
}
