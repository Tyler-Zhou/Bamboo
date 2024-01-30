using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FAM.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Common;
using ICP.FAM.ServiceInterface.CompositeObjects;

namespace ICP.FAM.UI.WriteOff
{
    /// <summary>
    /// 销账查询
    /// </summary>
    [ToolboxItem(false)]
    public partial class SearchPanel : BaseSearchPart
    {
        #region 服务
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public IFinanceService FinanceService { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public IConfigureService ConfigureService { get; set; }

        #endregion

        #region 成员
        /// <summary>
        /// 直连批量支付
        /// </summary>
        private string DirectBankBatchPayment = "FAM_DirectBankBatchPayment";
        /// <summary>
        /// 
        /// </summary>
        private WriteOffSearchParameter searchParameter;
        /// <summary>
        /// 
        /// </summary>
        private DataPageInfo dataPageInfo;
        /// <summary>
        /// 
        /// </summary>
        public override event SearchResultHandler OnSearched;
        /// <summary>
        /// 支付方式
        /// </summary>
        BillSearchPaymentWay PaymentWay
        {
            get
            {
                return (BillSearchPaymentWay)rdoPaymentWay.SelectedIndex;
            }
        }
        /// <summary>
        /// 银行流水
        /// </summary>
        BankTransactionInfo _BankTransactionInfo { get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public SearchPanel()
        {
            InitializeComponent();
            Disposed += delegate
            {
                OnSearched = null;
                dataPageInfo = null;
                RemoveKeyDownHandle();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        } 
        #endregion

        #region 初始化
        /// <summary>
        /// 重写加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
                SetKeyDownToSearch();
                CheckBankTransactionInfo();
            }
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            rdoPaymentWay.SelectedIndex = 0;
            if (LocalCommonServices.PermissionService.HaveActionPermission(DirectBankBatchPayment))
            {
                rdoPaymentWay.Enabled = true;
                rdoPaymentWay.SelectedIndexChanged += rdoPaymentWay_SelectedIndexChanged;
            }
            dateMonthControl1.IsEngish = LocalData.IsEnglish;

            rdoFeeWay.SelectedIndex = 0;

            DevHelper.FormatMoney(numAmountMax);
            DevHelper.FormatMoney(numAmountMin);

            //绑定公司
            FAMUtility.BindComboBoxByCompany(cmbCompanyID);

            //审核状态 
            List<EnumHelper.ListItem<BillSearchAuditorStatue>> auditorStatus = EnumHelper.GetEnumValues<BillSearchAuditorStatue>(LocalData.IsEnglish);
            cmbAuditorStatue.Properties.BeginUpdate();
            foreach (var item in auditorStatus)
            {
                cmbAuditorStatue.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbAuditorStatue.Properties.EndUpdate();

            //日期查询类型
            List<EnumHelper.ListItem<WriteOffSearchDateType>> dateType = EnumHelper.GetEnumValues<WriteOffSearchDateType>(LocalData.IsEnglish);
            cmbDataSearchType.Properties.BeginUpdate();
            foreach (var item in dateType)
            {
                cmbDataSearchType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbDataSearchType.Properties.EndUpdate();

            cmbAuditorStatue.SelectedIndex = cmbDataSearchType.SelectedIndex = 0;

            //其它项目
            FAMUtility.SetEnterToExecuteOnec(ccmbOther,
                delegate
                {
                    BindOther();
                });

            if (dataPageInfo == null)
                dataPageInfo = new DataPageInfo();
            dataPageInfo.PageSize = (int)numMaxCount.Value;
            dataPageInfo.SortByName = "CreateDate";
            dataPageInfo.SortOrderType = SortOrderType.Desc;
            dataPageInfo.CurrentPage = 1;


            
        }

        

        private void BindOther()
        {
            Guid companyID;
            if (cmbCompanyID.EditValue != null && (Guid)cmbCompanyID.EditValue != Guid.Empty)
            {
                companyID = (Guid)cmbCompanyID.EditValue;
            }
            else
            {
                companyID = LocalData.UserInfo.DefaultCompanyID;
            }

            ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(companyID);
            List<SolutionGLCodeList> glCodeList = new List<SolutionGLCodeList>();

            glCodeList = ConfigureService.GetSolutionGLCodeList(configureInfo.SolutionID, true);
            //Add by Sunny 判断空值
            if (glCodeList != null && glCodeList.Count > 0)
            {
                ccmbOther.Properties.BeginUpdate();

                foreach (SolutionGLCodeList gl in glCodeList)
                {
                    string name = LocalData.IsEnglish ? gl.EName : gl.CName;

                    ccmbOther.Properties.Items.Add(gl.ID, name, CheckState.Unchecked, true);
                }

                ccmbOther.Properties.SelectAllItemCaption = LocalData.IsEnglish ? "All" : "全部";

                ccmbOther.Properties.EndUpdate();
            }
        }

        private void SetKeyDownToSearch()
        {
            foreach (Control item in navBarGroupControlContainer1.Controls)
            {
                item.KeyDown += item_KeyDown;
            }
        }
        private void RemoveKeyDownHandle()
        {
            foreach (Control item in navBarGroupControlContainer1.Controls)
            {
                item.KeyDown -= item_KeyDown;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null)
            {
                return;
            }

            txtOperationNo.Text = string.Empty;
            txtCheckNo.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtCertificateNo.Text = string.Empty;

            foreach (var item in values)
            {
                string value = item.Value == null ? string.Empty : item.Value.ToString();
                switch (item.Key)
                {
                    case "No":
                        txtCheckNo.Text = value;
                        break;
                    case "CheckNo":
                        txtCheckNo.Text = value;
                        break;
                    case "CustomerName	":
                        txtCustomerName.Text = value;
                        break;
                    case "VoucherNo":
                        txtCertificateNo.Text = value;
                        break;
                }
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override object GetData()
        {
            try
            {
                PageList list = FinanceService.GetWriteOffListByList(searchParameter);
                return list;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); return null; }
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="data"></param>
        public override void RaiseSearched(object data)
        {
            DataPageInfo dataPageInfo = data as DataPageInfo;
            searchParameter.DataPageInfo = dataPageInfo;
            if (OnSearched != null)
            {
                OnSearched(this, GetData());
            }
        }
        /// <summary>
        /// 热键查询
        /// </summary>
        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }
        /// <summary>
        /// 检查银行流水信息
        /// </summary>
        void CheckBankTransactionInfo()
        {
            _BankTransactionInfo = Workitem.RootWorkItem.State[ModuleConstantsForFAM.FAM_STATEOBJECT_WRITEOFFLIST_BANKTRANSACTION] as BankTransactionInfo;
            if (_BankTransactionInfo != null)
            {
                BankAccountList accountList = FinanceService.GetBankAccountByNO(_BankTransactionInfo.AccountNO);
                if (!accountList.ID.IsNullOrEmpty())
                {
                    cmbCompanyID.EditValue = accountList.CompanyID;
                    cmbBankAccountID.EditValue = accountList.ID;
                    cmbCompanyID.Properties.ReadOnly = cmbBankAccountID.Properties.ReadOnly = true;
                    
                }
                chkIsValid.Checked = true;
                chkCheckAmount.Checked = true;
                numAmountMax.Value = _BankTransactionInfo.TransactionAmount;
                chkIsAccount.Checked = false;
                cmbAuditorStatue.SelectedIndex = 1;

                chkIsValid.ReadOnly = chkIsAccount.ReadOnly
                                    = chkCheckAmount.Properties.ReadOnly
                                    = cmbAuditorStatue.Properties.ReadOnly
                                    = numAmountMax.Properties.ReadOnly = true;

            }
        }

        #endregion

        #region 事件
        /// <summary>
        /// 按键事件
        /// </summary>
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
        void rdoPaymentWay_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbCompanyID.EditValue = LocalData.UserInfo.DefaultCompanyID;
                if(PaymentWay==BillSearchPaymentWay.Direct)
                {
                    rdoFeeWay.SelectedIndex = 3;
                    rdoFeeWay.Enabled = false;
                    chkIsAccount.Checked = false;
                    chkIsAccount.Enabled = false;
                }else
                {
                    rdoFeeWay.Enabled = true;
                    chkIsAccount.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        /// <summary>
        /// 选择的公司发生改变
        /// </summary>
        private void cmbCompanyID_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid companyID;
            if (cmbCompanyID.EditValue == null || (Guid)cmbCompanyID.EditValue == Guid.Empty)
            {
                companyID = LocalData.UserInfo.DefaultCompanyID;
            }
            else
            {
                companyID = (Guid)cmbCompanyID.EditValue;
            }
            //绑定银行列表
            cmbBankAccountID.Properties.Items.Clear();

            cmbBankAccountID.Properties.Items.Add(new ImageComboBoxItem(null, null));
            List<BankAccountList> accountList = FinanceService.GetCompanyBankAccounts(companyID, LocalData.IsEnglish);
            //Add by Sunny 处理公司无关联银行时的空值判断
            if (accountList != null && accountList.Count > 0)
            {
                foreach (var item in accountList)
                {
                    //查询直连；支持银企直连账户
                    if (PaymentWay != BillSearchPaymentWay.All && !item.IsSupportDirectBank)
                        continue;
                    cmbBankAccountID.Properties.Items.Add(new ImageComboBoxItem(item.CurrencyName, item.ID));
                }
            }

            //绑定会计科目
            BindOther();
        }
        /// <summary>
        /// 
        /// </summary>
        private void chkCheckAmount_CheckedChanged(object sender, EventArgs e)
        {
            numAmountMax.Properties.ReadOnly = numAmountMin.Properties.ReadOnly = !chkCheckAmount.Checked;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                searchParameter = new WriteOffSearchParameter();
                searchParameter.CurrentUserID = LocalData.UserInfo.LoginID;
                searchParameter.PaymentWay = PaymentWay;
                if (FAMUtility.GuidIsNullOrEmpty(cmbCompanyID.EditValue))
                {
                    if (searchParameter.PaymentWay == BillSearchPaymentWay.Direct)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), LocalData.IsEnglish ? "Must choose a company." : "必须选择一个操作口岸.");
                        return;
                    }
                    searchParameter.CompanyID = FAMUtility.GetCompanyIDList().ToArray();
                }
                else
                {
                    searchParameter.CompanyID = new Guid[] { (Guid)cmbCompanyID.EditValue };
                }
                
                if (!FAMUtility.GuidIsNullOrEmpty(cmbBankAccountID.EditValue))
                {
                    searchParameter.BankAccountID = (Guid)cmbBankAccountID.EditValue;
                }
                else
                {
                    if (searchParameter.PaymentWay == BillSearchPaymentWay.Direct)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), LocalData.IsEnglish ? "Must choose a bank account." : "必须选择一个银行账号.");
                        return;
                    }
                    searchParameter.BankAccountID = null;
                }

                searchParameter.FeeWay = (BillSearchFeeWay)rdoFeeWay.SelectedIndex;
                searchParameter.CheckNo = txtCheckNo.Text;
                searchParameter.CustomerName = txtCustomerName.Text;
                searchParameter.RealName = txtActualName.Text;
                searchParameter.OperationNo = txtOperationNo.Text;
                searchParameter.CustomerRefNo = txtCustomerRefNo.Text;
                searchParameter.CertificateNo = txtCertificateNo.Text;
                searchParameter.AuditorState = (BillSearchAuditorStatue)cmbAuditorStatue.SelectedIndex;
                searchParameter.OtherIDs = ccmbOther.EditValue == null ? string.Empty : ccmbOther.EditValue.ToString().Replace(", ", GlobalConstants.DividedSymbol);
                searchParameter.IsValid = chkIsValid.Checked;
                searchParameter.IsReached = chkIsAccount.Checked;
                searchParameter.DataPageInfo = dataPageInfo;
                if (chkCheckAmount.Checked)
                {
                    searchParameter.AmountMax = numAmountMax.Value;
                    searchParameter.AmountMin = numAmountMin.Value;
                }
                else
                {
                    searchParameter.AmountMax = null;
                    searchParameter.AmountMin = null;
                }
                searchParameter.DateType = (WriteOffSearchDateType)cmbDataSearchType.EditValue;
                searchParameter.StartDate = dateMonthControl1.From;
                searchParameter.EndDate = dateMonthControl1.To;
                searchParameter.DataPageInfo.PageSize = (int)numMaxCount.Value;
                searchParameter.DataPageInfo.CurrentPage = 1;
                searchParameter.Remark = txtRemark.Text;
                searchParameter.IsDateLess = chkD.Checked;

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
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "total search " + list.DataPageInfo.TotalCount.ToString() + " data." : "总共查询到 "
                                                    + list.DataPageInfo.TotalCount.ToString() + " 条数据.");
                    }
                    OnSearched(this, list);
                }
            }
        }
        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClare_Click(object sender, EventArgs e)
        {
            rdoFeeWay.SelectedIndex = 0;
            cmbCompanyID.SelectedIndex = 0;
            cmbBankAccountID.SelectedIndex = 0;
            txtCheckNo.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtActualName.Text = string.Empty;
            txtOperationNo.Text = string.Empty;
            txtCustomerRefNo.Text = string.Empty;
            txtCertificateNo.Text = string.Empty;
            cmbAuditorStatue.SelectedIndex = 0;
            ccmbOther.SelectAll();
            chkIsValid.Checked = null;
            numMaxCount.Value = 50;
            chkCheckAmount.Checked = false;
            numAmountMin.Value = 0;
            numAmountMax.Value = 0;
            cmbDataSearchType.SelectedIndex = 0;
        }

        #endregion

    }
}
