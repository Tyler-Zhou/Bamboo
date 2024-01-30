using DevExpress.XtraEditors;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.CompositeObjects;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FAM.UI.Controls
{
    /// <summary>
    /// 最近客户银行网格显示
    /// </summary>
    public partial class CustomerBankPopupContainerControl : PopupContainerControl
    {
        #region Services
        /// <summary>
        /// 财务服务
        /// </summary>
        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }
        #endregion

        #region Property
        #region 是否英文
        /// <summary>
        /// 是否英文
        /// </summary>
        public bool IsEnglish { get; set; } 
        #endregion

        #region 是否变更
        /// <summary>
        /// 是否变更
        /// </summary>
        public bool IsChanged { get; set; }
        #endregion

        #region 客户ID
        private Guid _CustomerID;
        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid CustomerID
        {
            get
            {
                return _CustomerID;
            }
            set
            {
                _CustomerID = value;
                RefreshDataSource();
            }
        }
        #endregion

        #region 银行账户名称
        private string _accountname;
        /// <summary>
        /// 银行账户名称
        /// </summary>
        public string AccountName
        {
            get
            {
                return _accountname;
            }
            set
            {
                if (_accountname != value)
                {
                    _accountname = value;
                    RefreshDataSource();
                }
            }
        }
        #endregion

        /// <summary>
        /// 当前选择行
        /// </summary>
        public CustomerBankInfo CurrentSelect
        {
            get
            {
                if (bsData.List == null || bsData.Current == null)
                {
                    return null;
                }
                return bsData.Current as CustomerBankInfo;
            }
        }
        #endregion

        #region Delegate
        /// <summary>
        /// 双击列表数据行改变事件
        /// </summary>
        public event EventHandler<CommonEventArgs<CustomerBankInfo>> SelectChanged;
        #endregion

        #region Constructor
        /// <summary>
        /// 最近客户银行网格显示
        /// </summary>
        public CustomerBankPopupContainerControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void OnLoaded()
        {
            base.OnLoaded();
            SetLanguage();
            gvMain.DoubleClick+=gvMain_DoubleClick;
            Disposed += (sender, e) =>
            {
                gvMain.DoubleClick -= gvMain_DoubleClick;
                gcMain.DataSource = null;
                bsData.DataSource = null;
                bsData.Dispose();
            };
        }
        #endregion

        #region Control Event
        /// <summary>
        /// 网格双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_DoubleClick(object sender, EventArgs e)
        {
            if (SelectChanged != null)
            {
                SelectChanged(sender, new CommonEventArgs<CustomerBankInfo>(CurrentSelect));
            }
        }
        #endregion

        #region Method
        private void SetLanguage()
        {
            if (!IsEnglish)
            {
                colAccountName.Caption = "账户名称";
                colAccountNO.Caption = "账户账号";
                colBankName.Caption = "开户行";
                colBankNumber.Caption = "行联号";
                colBranchName.Caption = "银行支行";
            }
        }
        /// <summary>
        /// 获取最近业务报价记录
        /// </summary>
        public void RefreshDataSource()
        {
            if (CustomerID.IsNullOrEmpty())
            {
                bsData.Clear();
                bsData.DataSource = new List<CustomerBankInfo>();
                bsData.ResetBindings(false);
            }
            CustomerBankInfoSearchParameter searchParameter = new CustomerBankInfoSearchParameter()
            {
                CustomerID = CustomerID,
                CustomerName = AccountName,
            };
            List<CustomerBankInfo> dataList = FinanceService.AllCustomerBanks(searchParameter);
            bsData.DataSource = dataList;
            bsData.ResetBindings(false);
        }
        #endregion
    }
}
