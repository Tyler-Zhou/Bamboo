using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using ICP.FAM.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Server;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.Controls;
using ICP.FAM.UI.Comm.OperationTypeSearchParts;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using DevExpress.XtraEditors;

namespace ICP.FAM.UI.Business
{
    [ToolboxItem(false)]
    public partial class BusinessSearchPart : BaseSearchPart
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        public IUserService UserService
        {
            get
            { 
            return ServiceClient.GetService<IUserService>();
            }
        }

        #endregion

        #region 初始化

        public BusinessSearchPart()
        {
            InitializeComponent();
            Disposed += delegate {
                RemoteKeyDownHandle();
                OnSearched = null;
                _searchPart = null;
                user = null;
                mcmbOperate.OnFirstEnter -= OnmcmbOperateEnter;
                mcmbSales.OnFirstEnter -= OnmcmbSalesEnter;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                FAMUtility.SetCustomerTextEditNullValuePrompt(new List<TextEdit> { txtCustomer });

                SetCmbSource();
                SetKeyDownToSearch();
                #region ChkEvent
                chkProfit.CheckedChanged += delegate { seMax.Enabled = seMin.Enabled = chkProfit.Checked; };
                #endregion
            }
        }
        private void RemoteKeyDownHandle()
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                item.KeyDown -= item_KeyDown;
            }
        }
        private void SetKeyDownToSearch()
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                item.KeyDown += item_KeyDown;
            }
        }
        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2) btnSearch.PerformClick();
            else if (e.KeyCode == Keys.F3) btnClear.PerformClick();
        }
        List<UserList> user = null;
        private void SetCmbSource()
        {
            #region 提单类型
            List<EnumHelper.ListItem<OperationType>> operationTypes = EnumHelper.GetEnumValues<OperationType>(LocalData.IsEnglish);
            cmbOperationType.Properties.BeginUpdate();
            cmbOperationType.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "ALL" : "全部", DBNull.Value));

            foreach (var item in operationTypes)
            {
                if (item.Value == 0) continue;
                cmbOperationType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbOperationType.Properties.EndUpdate();

            cmbOperationType.SelectedIndexChanged += new EventHandler(cmbOperationType_SelectedIndexChanged);
            cmbOperationType.SelectedIndex = 0;
            #endregion

            #region Company

            List<LocalOrganizationInfo> userCompanyList = FAMUtility.GetCompanyList();
            chkcmbCompany.Properties.BeginUpdate();
            chkcmbCompany.Properties.Items.Clear();
            foreach (var item in userCompanyList)
            {
                chkcmbCompany.Properties.Items.Add(item.ID, LocalData.IsEnglish ? item.EShortName : item.CShortName,
                                                   CheckState.Checked, true);
            }
            chkcmbCompany.Properties.EndUpdate();
            #endregion


            

            #region 当前用户所在的操作口岸的所有揽货人
            mcmbSales.OnFirstEnter += OnmcmbSalesEnter;
            #endregion

            #region Filer 	当前用户所在的部门的所有用户
            mcmbOperate.OnFirstEnter += OnmcmbOperateEnter;
            #endregion

        }
        private void OnmcmbSalesEnter(object sender, EventArgs e)
        {
            if (user == null) user = UserService.GetUnderlingUserList(CompanyIDs.ToArray(), null, null, true);

            Dictionary<string, string> col = new Dictionary<string, string>();
            col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
            col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");
            mcmbSales.InitSource<UserList>(user, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
        }
        private void OnmcmbOperateEnter(object sender, EventArgs e)
        {
            if (user == null) user = UserService.GetUnderlingUserList(CompanyIDs.ToArray(), null, null, true);

            Dictionary<string, string> col = new Dictionary<string, string>();
            col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
            col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");
            mcmbOperate.InitSource<UserList>(user, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
        }
        #endregion

        #region 属性

        public OperationType? SearchOperationType
        {
            get
            {
                if (cmbOperationType.EditValue != null && cmbOperationType.EditValue != DBNull.Value) return (OperationType)cmbOperationType.EditValue;
                else return null;
            }
        }
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

        public decimal? MinProfit
        {
            get
            {
                if (chkProfit.Checked == false) return null;
                else return seMin.Value;
            }
        }

        public decimal? MaxProfit
        {
            get
            {
                if (chkProfit.Checked == false) return null;
                else return seMax.Value;
            }
        }


        public OperationParameter OperationParameter
        {
            get
            {
                if (SearchOperationType == null) return null;

                return null;
            }
        }

        #endregion

        #region event

        OperationTypeSearchPart _searchPart;
        void cmbOperationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BulidChildSearchPart(SearchOperationType);
        }
        int _orgNavbarHeight = 380;
        private void BulidChildSearchPart(OperationType? operationType)
        {
            panelType.Controls.Clear();
            if (_searchPart != null) _searchPart.Dispose();
            _searchPart = OperationTypeSearchPartFactory.GetSearchPart(operationType);
            navBarOther.GroupClientHeight = panelOperationType.Height + _searchPart.Height;
            navBarControl1.Height = _orgNavbarHeight + _searchPart.Height;

            panelType.Controls.Add(_searchPart);
            _searchPart.Dock = DockStyle.Fill;

            _searchPart.SetTextBoxLocation(labOperationType.Location.X, cmbOperationType.Location.X, cmbOperationType.Width);
        }

        #endregion

        #region ISearchPart 成员

        public override event SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        public override void RaiseSearched(object data)
        {
            DataPageInfo dataPageInfo = data as DataPageInfo;
            searchParameter.DataPageInfo = dataPageInfo;
            if (OnSearched != null)
                OnSearched(this, GetData());
        }

        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                //if (item.Key == "OperationNo")
                //{
                //    txtOperationNo.Text = item.Value == null ? string.Empty : item.Value.ToString();
                //}
                //if (item.Key == "BLNO")
                //{
                //    txtBLNO.Text = item.Value == null ? string.Empty : item.Value.ToString();
                //}
            }
        }

        public override object GetData()
        {
            try
            {
                PageList list = FinanceService.GetBusinessListByList(searchParameter.companyIDs,
                                                                        searchParameter.operationNo,
                                                                        searchParameter.blNo,
                                                                        searchParameter.ctnNo,
                                                                        searchParameter.customer,
                                                                        searchParameter.sales,
                                                                        searchParameter.filerID,
                                                                        searchParameter.minProfit,
                                                                        searchParameter.maxProfit,
                                                                        null,
                                                                        searchParameter.operationType,
                                                                        searchParameter.parameter,
                                                                        searchParameter.DataPageInfo);
                return list;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); return null; }
        }

        #endregion

        #region btn
        /// <summary>
        /// 缓存的查询参数
        /// </summary>
        BusinessSearchParameter searchParameter = new BusinessSearchParameter();
        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OperationParameter parameter = _searchPart == null ? null : _searchPart.GetOperationParameter();

                searchParameter.companyIDs = CompanyIDs.ToArray();
                searchParameter.operationNo = txtOperationNo.Text.Trim();
                searchParameter.blNo = txtBLNo.Text.Trim();
                searchParameter.ctnNo = txtCtnNo.Text.Trim();
                searchParameter.customer = txtCustomer.Text.Trim();
                searchParameter.sales = mcmbSales.EditText.Trim();
                searchParameter.filerID = mcmbOperate.EditText.Trim();
                searchParameter.minProfit = MinProfit;
                searchParameter.maxProfit = MaxProfit;
                searchParameter.operationType = SearchOperationType;
                searchParameter.parameter = parameter;
                searchParameter.DataPageInfo.PageSize = int.Parse(numpageCount.Value.ToString());
                searchParameter.DataPageInfo.CurrentPage = 1;

                if (string.IsNullOrEmpty(searchParameter.DataPageInfo.SortByName))
                {
                    searchParameter.DataPageInfo.SortByName = "OperationNO";
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                if (item is LWImageComboBoxEdit)
                {
                    (item as LWImageComboBoxEdit).SelectedIndex = 0;
                }
                else if (item is TextEdit
                         && (item is SpinEdit) == false
                         && item.Enabled == true
                         && (item as TextEdit).Properties.ReadOnly == false)
                    item.Text = string.Empty;
            }
            chkProfit.Checked = false;
            mcmbOperate.EditText = mcmbSales.EditText = string.Empty;

            if (_searchPart != null) _searchPart.Clear();
        }

        #endregion
    }

 


}
