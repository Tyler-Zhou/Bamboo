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
    public partial class JournalSearchPart : BaseSearchPart
    {
        public JournalSearchPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                RemoveKeyDownHandle();
                OnSearched = null;
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

        #region 初始化begin
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                SetKeyDownToSearch();
                InitControls();
            }
        }

        #region 回车事件响应
        private void SetKeyDownToSearch()
        {
            foreach (Control item in navBarTitle.Controls)
            {
                item.KeyDown += item_KeyDown;
            }
        }
        private void RemoveKeyDownHandle()
        {
            foreach (Control item in navBarTitle.Controls)
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
        }
        #endregion

        #region 初始化
        private void InitControls()
        {
            FAMUtility.BindCheckComboBoxByCompany(chcCompany);//调用公用方法
            dmdDate.IsEngish = LocalData.IsEnglish;
            cheAmounts.CheckedChanged += delegate
            {
                numMin.Enabled = numMax.Enabled = cheAmounts.Checked;
            };//启用&禁用部分查询条件
        }
        #endregion

        public override void Init(IDictionary<string, object> values)
        {
            if (values == null)
            {
                return;
            }

            //foreach (var item in values)
            //{
            //    string value = item.Value == null ? string.Empty : item.Value.ToString();
            //    switch (item.Key)
            //    {
            //    }
            //}

        }

        #endregion 初始化end

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
                PageList list = FinanceService.GetJournalList(
                    searchParameter.NO,
                    searchParameter.companyIDs,
                    searchParameter.startDate,
                    searchParameter.endDate,
                    searchParameter.minAmount,
                    searchParameter.maxAmount,
                    searchParameter.isValid,
                    searchParameter.DataPageInfo,
                    LocalData.IsEnglish);
                int maxRow = (int)cheRecord.Value;
                return list;

            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); return null; }

        }

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

        public override void RaiseSearched(object data)
        {
            DataPageInfo dataPageInfo = data as DataPageInfo;
            searchParameter.DataPageInfo = dataPageInfo;
            if (OnSearched != null)
                OnSearched(this, GetData());
        }
        public override void RaiseSearched()
        {
            btnSearch.PerformClick();

        }

        #region 按钮事件
        #region 查询
        JournalSearchParameter searchParameter = new JournalSearchParameter();
        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                totalRowCount = 0;
                searchParameter.companyIDs = CompanyIDs.ToArray();
                searchParameter.startDate = dmdDate.From;
                searchParameter.endDate = dmdDate.To;
                searchParameter.NO = txtNo.Text;
                if (!cheAmounts.Checked)
                {
                    searchParameter.minAmount = searchParameter.maxAmount = null;
                }
                else
                {
                    searchParameter.minAmount = Convert.ToDecimal(numMin.Text.Trim());
                    searchParameter.maxAmount = Convert.ToDecimal(numMax.Text.Trim());
                }
                searchParameter.isValid = ckbValid.Checked;
                searchParameter.DataPageInfo.PageSize = (int)cheRecord.Value;
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
        #endregion

        #region 清空
        private void btnClare_Click(object sender, EventArgs e)
        {
            cheRecord.Value = 50;
            ckbValid.Checked = null;
            numMin.Value = 0;
            numMax.Value = 0;
            chcCompany.Text = string.Empty;
        }
        #endregion
        #endregion

        /// <summary>
        /// 查询实体
        /// </summary>
        public class JournalSearchParameter
        {
            public JournalSearchParameter() { DataPageInfo = new DataPageInfo(); }
            public string NO { get; set; }
            public Guid[] companyIDs { get; set; }
            public DateTime? startDate { get; set; }
            public DateTime? endDate { get; set; }
            public decimal? minAmount { get; set; }
            public decimal? maxAmount { get; set; }
            public bool? isValid { get; set; }
            public int pageSize { get; set; }
            public DataPageInfo DataPageInfo { get; set; }

        }

        private void txtNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2)
            {
                btnSearch.PerformClick();
            }
        }

    }
}
