using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Server;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
namespace ICP.FAM.UI
{
    [ToolboxItem(false)]
    public partial class InvoiceSearchPart : BaseSearchPart
    {
        
        #region 服务
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }
        #endregion

        #region 初始化

        public InvoiceSearchPart()
        {
            InitializeComponent();
            Disposed += delegate {
                OnSearched = null;
                RemoveKeyDownHandle();
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
                InitControls();
                SetKeyDownToSearch();
            }
        }
        /// <summary>
        /// 总行数
        /// </summary>
        private int totalRowCount
        {
            get;
            set;
        }
        private void SetKeyDownToSearch()
        {
            navbarInvoice.KeyDown +=item_KeyDown;
            navbarBase.KeyDown += item_KeyDown;

            foreach (Control item in navbarBase.Controls)
            {
                if (item is TextEdit
                && (item is SpinEdit) == false)
                {
                    item.KeyDown += item_KeyDown;
                }
            }
            foreach (Control item in navbarInvoice.Controls)
            {
                if (item is TextEdit
                && (item is SpinEdit) == false)
                {
                    item.KeyDown += item_KeyDown;
                }
            }
        }
        private void RemoveKeyDownHandle()
        {
            navbarInvoice.KeyDown -= item_KeyDown;
            navbarBase.KeyDown -= item_KeyDown;

            foreach (Control item in navbarBase.Controls)
            {
                if (item is TextEdit
                && (item is SpinEdit) == false)
                {
                    item.KeyDown -=item_KeyDown;
                }
            }
            foreach (Control item in navbarInvoice.Controls)
            {
                if (item is TextEdit
                && (item is SpinEdit) == false)
                {
                    item.KeyDown -= item_KeyDown;
                }
            }
        }

        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2) btnSearch.PerformClick();
            else if (e.KeyCode == Keys.F3) btnClear.PerformClick();
        }
        #region 快捷查询
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;

            txtInvoiceNo.Text = string.Empty;
            txtBillNo.Text = string.Empty;
            txtBLNo.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtExpressNo.Text = string.Empty;
            txtRemark.Text = string.Empty;

            foreach (var item in values)
            {
                if (item.Key.ToLower() == "InvoiceNo".ToLower())
                {
                    txtInvoiceNo.Text = item.Value.ToString();
                }
                else if (item.Key.ToLower() == "CustomerName".ToLower())
                {
                    txtCustomerName.Text = item.Value.ToString();
                }
                else if (item.Key.ToLower() == "BillNo".ToLower())
                {
                    txtBillNo.Text = item.Value.ToString();
                }
                else if (item.Key.ToLower() == "BLNo".ToLower())
                {
                    txtBLNo.Text = item.Value.ToString();
                }
                else if (item.Key.ToLower() == "ExpressNo".ToLower())
                {
                    txtExpressNo.Text = item.Value.ToString();
                }
                else if (item.Key.ToLower() == "Remark".ToLower())
                {
                    txtRemark.Text = item.Value.ToString();
                }
            }
        }
        #endregion
        private void InitControls()
        {
            dmdInvoiceDate.IsEngish = LocalData.IsEnglish;
            dmdETD.IsEngish = LocalData.IsEnglish;

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

            numAmountMin.Enabled = numAmountMax.Enabled = chkAmount.Checked;
            chkAmount.CheckedChanged += delegate
            {
                numAmountMin.Enabled = numAmountMax.Enabled = chkAmount.Checked;
            };
            dmdInvoiceDate.IsEngish =dmdETD.IsEngish= LocalData.IsEnglish;//初始化控件语言属性
            if (!LocalData.IsEnglish)
            {
                labTitleCN.Text = "中文抬头";
                labTitleEN.Text = "英文抬头";
            }
        }
        #endregion

        #region 
        //最大金额
        public decimal? MaxAmount
        {
            get
            {
                if (!chkAmount.Checked) return null;
                else return numAmountMax.Value;
            }
        }
        //最小金额
        public decimal? MinAmount
        {
            get
            {
                if (!chkAmount.Checked) return null;
                else return numAmountMin.Value;
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

        enum DateType
        {
            Unknown,
            LastMonth,
            ThisMonth,
            Specify
        }
        #endregion

        #region 查询
        InvoiceSearchParameter searchParameter = new InvoiceSearchParameter();
        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CompanyIDs.Count == 0)
                {
                    string message = LocalData.IsEnglish ? "Please select company" : "请选择公司";
                    XtraMessageBox.Show(message);
                    chkcmbCompany.Focus();
                    return;
                }

                totalRowCount = 0;
                searchParameter.companyIds = CompanyIDs.ToArray();
                searchParameter.customerName = txtCustomerName.Text.Trim();
                searchParameter.titleCName = txtTitleCN.Text.Trim();
                searchParameter.titleEName = txtTitleEN.Text.Trim();
                searchParameter.isValid = ckbValid.Checked;
                searchParameter.invoiceNo = txtInvoiceNo.Text.Trim();
                searchParameter.operationNo = txtRefNo.Text.Trim();
                searchParameter.billNo = txtBillNo.Text.Trim();
                searchParameter.blNo = txtBLNo.Text.Trim();
                searchParameter.ctnNo = txtContainerNo.Text.Trim();

                searchParameter.expressNo = txtExpressNo.Text.Trim();
                searchParameter.remark = txtRemark.Text.Trim();
                searchParameter.amountMin = MinAmount;
                searchParameter.amountMax = MaxAmount;
                searchParameter.invoiceBeginTime = dmdInvoiceDate.From;
                searchParameter.invoiceEndTime = dmdInvoiceDate.To;
                searchParameter.etdBeginTime = dmdETD.From;
                searchParameter.etdEndTime = dmdETD.To;
                //分页数据
                searchParameter.DataPageInfo.PageSize = int.Parse(numpageCount.Value.ToString());
                searchParameter.DataPageInfo.CurrentPage = 1;
                if (string.IsNullOrEmpty(searchParameter.DataPageInfo.SortByName))
                {
                    searchParameter.DataPageInfo.SortByName = "InvoiceDate";
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
            foreach (Control item in navbarBase.Controls)
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
                else if (item is MultiSearchCommonBox) (item as MultiSearchCommonBox).EditText = string.Empty;
            }
            foreach (Control item in navbarInvoice.Controls)
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
                else if (item is MultiSearchCommonBox) (item as MultiSearchCommonBox).EditText = string.Empty;
            }
            chkAmount.Checked = false;
            ckbValid.Checked = null;
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

        public override object GetData()
        {
            try
            {
                PageList list = FinanceService.GetInvoiceListByList(searchParameter.companyIds
                                                                          ,searchParameter.customerName
                                                                          ,searchParameter.titleCName
                                                                          ,searchParameter.titleEName
                                                                          ,searchParameter.isValid
                                                                          ,searchParameter.invoiceNo
                                                                          ,searchParameter.operationNo
                                                                          ,searchParameter.billNo
                                                                          ,searchParameter.blNo
                                                                          ,searchParameter.ctnNo
                                                                          ,searchParameter.expressNo
                                                                          ,searchParameter.remark
                                                                          ,searchParameter.amountMin,searchParameter.amountMax
                                                                          ,searchParameter.invoiceBeginTime,searchParameter.invoiceEndTime
                                                                          ,searchParameter.etdBeginTime,searchParameter.etdEndTime
                                                                          ,searchParameter.DataPageInfo);
                return list;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); return null; }
        }

        #endregion
    }

    class InvoiceSearchParameter
    {
        public InvoiceSearchParameter() { DataPageInfo = new DataPageInfo(); }

        public Guid[] companyIds { get; set; }
        public string customerName { get; set; }
        public string titleCName { get; set; }
        public string titleEName { get; set; }
        public bool? isValid { get; set; }
        public string invoiceNo { get; set; }
        public string operationNo { get; set; }
        public string billNo { get; set; }
        public string blNo { get; set; }
        public string ctnNo { get; set; }
        public string expressNo { get; set; }
        public string remark { get; set; }
        public decimal? amountMin { get; set; }
        public decimal? amountMax { get; set; }
        public DateTime? invoiceBeginTime { get; set; }
        public DateTime? invoiceEndTime { get; set; }
        public DateTime? etdBeginTime { get; set; }
        public DateTime? etdEndTime { get; set; }
        public DataPageInfo DataPageInfo { get; set; }
    }

}
