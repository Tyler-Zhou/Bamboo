using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.CompositeObjects;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ICP.FAM.UI.BankTransaction
{
    /// <summary>
    /// 查询面板(银行流水)
    /// </summary>
    [ToolboxItem(false)]
    public partial class SearchPart : BaseSearchPart
    {
        #region Service injection
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }
        #endregion

        #region Member
        /// <summary>
        /// 操作口岸ID
        /// </summary>
        Guid _CompanyID
        {
            get
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
                return companyID;
            }
        }

        /// <summary>
        /// 银行账号ID
        /// </summary>
        Guid _BankAccountID
        {
            get
            {
                Guid bankAccountID = Guid.Empty;
                if (cmbBankAccountID.EditValue != null && (Guid)cmbBankAccountID.EditValue != Guid.Empty)
                {
                    bankAccountID=(Guid)cmbBankAccountID.EditValue;
                }
                return bankAccountID;
            }
        }
        /// <summary>
        /// 银行
        /// </summary>
        BANKCODE _BankCode
        {
            get
            {
                int bankCode = cmbBankCode.SelectedIndex;
                if (bankCode < 0)
                    bankCode = 0;
                return (BANKCODE)(bankCode+1);
            }
        }
       
        string _CurrencyCode
        {
            get
            {
                string bankAccount = ""+cmbBankAccountID.Text;
                if(string.IsNullOrEmpty(bankAccount))
                {
                    return "";
                }
                return bankAccount.Substring(bankAccount.IndexOf('-')+1, 3);
            }
        }

        string _AccountNO
        {
            get
            {
                string bankAccount = "" + cmbBankAccountID.Text;
                if (string.IsNullOrEmpty(bankAccount))
                {
                    return "";
                }
                return bankAccount.Substring(bankAccount.LastIndexOf('-') + 1, bankAccount.Length - bankAccount.LastIndexOf('-')-1);
            }
        }
        /// <summary>
        /// 银企直连银行账号列表
        /// </summary>
        List<BankAccountList> _DirectBankAccountList { get; set; }
        /// <summary>
        /// 查询事件
        /// </summary>
        public override event SearchResultHandler OnSearched;
        #endregion

        #region Init
        /// <summary>
        /// 实例化时初始化必要事件
        /// </summary>
        public SearchPart()
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
        /// <summary>
        /// 重写加载事件，初始化控件数据源，设置事件
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                InitControls();
                SetKeyDownToSearch();
            }

        }

        #endregion

        #region Method

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

            foreach (var item in values)
            {
                string value = item.Value == null ? string.Empty : item.Value.ToString();
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitControls()
        {
            cmbCompanyID.SelectedIndexChanged += cmbCompanyID_SelectedIndexChanged;
            FAMUtility.BindComboBoxByCompany(cmbCompanyID, false, true);
            DevHelper.FormatSpinEditForInteger(nudTotalRecords);
            dmdDate.IsEngish = LocalData.IsEnglish;

            //银行 
            List<EnumHelper.ListItem<BANKCODE>> bankCodes = EnumHelper.GetEnumValues<BANKCODE>(LocalData.IsEnglish);
            cmbBankCode.Properties.BeginUpdate();
            foreach (var item in bankCodes)
            {
                cmbBankCode.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbBankCode.Properties.EndUpdate();
            cmbBankCode.SelectedIndex = 0;
            rdoDirection.SelectedIndex = 0;
        }

       

        /// <summary>
        /// 设置热键
        /// </summary>
        private void SetKeyDownToSearch()
        {
            foreach (Control item in navBarGroupBaseInfo.Controls)
            {
                item.KeyDown += item_KeyDown;
            }
        }
        /// <summary>
        /// 移除热键
        /// </summary>
        private void RemoveKeyDownHandle()
        {
            foreach (Control item in navBarGroupBaseInfo.Controls)
            {
                item.KeyDown += item_KeyDown;
            }
        }
        

        /// <summary>
        /// 预先查询方法(在窗体首次打开时调用)
        /// </summary>
        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public override object GetData()
        {
            try
            {
                if (_BankAccountID.IsNullOrEmpty())
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), LocalData.IsEnglish ? "Must choose a bank account." : "必须选择一个银行账号.");
                    return new List<BankTransactionInfo>();
                }
                string dcFlag="";
                switch(rdoDirection.SelectedIndex)
                {
                    case 1:
                        dcFlag="D";
                        break;
                    case 2:
                        dcFlag="C";
                        break;
                    default:
                        dcFlag="";
                        break;
                }
                BankTransactionSearchParameter requestParameter = new BankTransactionSearchParameter()
                {
                    CompanyID = _CompanyID,
                    BankCode = _BankCode,
                    BankAccountNO = _AccountNO,
                    CurrentName = _CurrencyCode,
                    BusinessNO = txtBusinessNO.Text,
                    FlowWaterNO = "",
                    BankAccountID = _BankAccountID,
                    RelativeAccountName = txtRelativeAccountName.Text,
                    TotalRecords = (int)nudTotalRecords.Value,
                    BeginDate = dmdDate.From,
                    EndDate = dmdDate.To,
                    DebitCreditFlag =dcFlag,
                    MinimumAmount = (decimal)numAmountMin.EditValue,
                    MaximumAmount = (decimal)numAmountMax.EditValue,
                    Queryer = LocalData.UserInfo.LoginID,
                };
                List<BankTransactionInfo> list = FinanceService.GetTransList(requestParameter);

                return list;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); return null; }

        }

        #endregion

        #region Event
        /// <summary>
        /// 选择的公司发生改变
        /// </summary>
        private void cmbCompanyID_SelectedIndexChanged(object sender, EventArgs e)
        {
            //绑定银行列表
            cmbBankAccountID.Properties.Items.Clear();
            _DirectBankAccountList = FinanceService.DirectBankAccountList(new DirectBankSearchParameter() { CompanyID = _CompanyID, OnlyDirectBank = false }).ToList();
            if (_DirectBankAccountList != null && _DirectBankAccountList.Count > 0)
            {
                foreach (var item in _DirectBankAccountList)
                {
                    cmbBankAccountID.Properties.Items.Add(new ImageComboBoxItem(item.CurrencyName, item.ID));
                }
            }
            cmbBankAccountID.SelectedIndex = 0;
        }
        /// <summary>
        /// 热键注册
        /// </summary>
        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2)
            {
                btnSearch.PerformClick();
            }
            else if (e.KeyCode == Keys.F3)
            {
                btnClear.PerformClick();
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (OnSearched != null) OnSearched(this, GetData());
            }
        }
        /// <summary>
        /// 清空查询条件
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control item in navBarGroupBaseInfo.Controls)
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
        }
        #endregion
    }
}
