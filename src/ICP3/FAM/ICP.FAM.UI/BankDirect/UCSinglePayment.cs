using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface.CompositeObjects;
using ICP.FAM.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary;
using ICP.Common.ServiceInterface;
using System.IO;
using ICP.Framework.ClientComponents;

namespace ICP.FAM.UI
{
    /// <summary>
    /// 单笔支付
    /// </summary>
    public partial class UCSinglePayment : BaseEditPart
    {
        #region Service
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
        IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        #endregion

        #region Member
        private IDisposable payCustomerFinder;
        /// <summary>
        /// 当前行
        /// </summary>
        APIPaymentInfo CurrentRow
        {
            get
            {
                if (bsData.DataSource == null || bsData.Current == null) return null;
                return bsData.Current as APIPaymentInfo;
            }
        }

        /// <summary>
        /// 数据源
        /// </summary>
        public override object DataSource
        {
            get
            {
                return bsData.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }
        /// <summary>
        /// 缓存文件
        /// </summary>
        string CacheFilePath { get { return Path.Combine(LocalData.MainPath, "Config\\Cache.ini"); } }
        /// <summary>
        /// 用途
        /// </summary>
        private IList<string> useDescriptionList = new List<string>();
        /// <summary>
        /// 缓存配置文件
        /// </summary>
        INIHelper iniConfig;
        /// <summary>
        /// 缓存配置文件
        /// </summary>
        INIHelper _INIConfig
        {
            get
            {
                return iniConfig ?? (iniConfig = new INIHelper(CacheFilePath));
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// 单笔支付
        /// </summary>
        public UCSinglePayment()
        {
            InitializeComponent();
            Disposed += delegate
            {
                if (payCustomerFinder != null)
                {
                    payCustomerFinder.Dispose();
                    payCustomerFinder = null;
                }
            };
        } 
        #endregion

        #region Event
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
            {
                InitControls();
            }
        }
        /// <summary>
        /// 对方银行账户选择
        /// </summary>
        void txtRelativeAccountName_SelectChanged(object sender, CommonEventArgs<CustomerBankInfo> e)
        {

            if (e.Data == null)
            {
                return;
            }
            CustomerBankInfo currentData = e.Data;
            DialogResult result = MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "是否覆盖当前页面数据?" : "是否覆盖当前页面数据?"
                              , LocalData.IsEnglish ? "Tip" : "提示"
                              , MessageBoxButtons.YesNo
                              );
            if (result == DialogResult.Yes)
            {
                txtRelativeAccountName.CustomerName = currentData.AccountName;
                txtRelativeAccountNo.Text = currentData.AccountNO;
                txtRelativeBankName.Text = currentData.BankName;
                txtRelativeBranchName.Text = currentData.BranchName;
                txtRelativeBankNumber.Text = currentData.BankNumber;
                EndEdit();
                Invalidate();
            }
        }
        /// <summary>
        /// 验证码变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtValidCode_TextChanged(object sender, EventArgs e)
        {
            string validCode = txtValidCode.Text;
            if (validCode.Length == 6 && validCode.IsNumeric())
            {
                btnSave.Enabled = true;
            }else
            {
                btnSave.Enabled = false;
            }
        }
        /// <summary>
        /// 有效码
        /// </summary>
        void btnValidCode_Click(object sender, EventArgs e)
        {
            bsData.EndEdit();

            if (!ValidateData())
                return;
            SinglePaymentSaveRequest saveRequest = new SinglePaymentSaveRequest()
            {
                CompanyID = CurrentRow.CompanyID,
                BankCode = BANKCODE.CMB,
                CurrencyName = CurrentRow.CurrencyName,
                AccountNO = CurrentRow.BankAccountNO,
                BusinessID = CurrentRow.BusinessID,
                BusinessNO = CurrentRow.BusinessNO,
                Amount = CurrentRow.Amount,
                SaveByID = LocalData.UserInfo.LoginID,
                SaveBy = LocalData.UserInfo.LoginName,
                UpdateDate = DateTime.Now,
            };
            FinanceService.GetPaymentValidCode(saveRequest);
        }
        void btnCancel_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                if(!useDescriptionList.Contains(cmbUseDescription.Text))
                {
                    useDescriptionList.Add(cmbUseDescription.Text);
                }
                string useDescriptions = "";
                foreach (var item in useDescriptionList)
                {
                    useDescriptions += item+";";
                }
                _INIConfig.IniWriteValue("BankDirect", "UseDescription", useDescriptions);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Payment Successfully" : "支付成功");
            }
            FindForm().Close();
        } 
        #endregion

        #region Methods
        /// <summary>
        /// 初始化
        /// </summary>
        void InitControls()
        {
            txtValidCode.TextChanged += txtValidCode_TextChanged;
            btnValidCode.Click += btnValidCode_Click;
            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;


            if (!File.Exists(CacheFilePath))
            {
                _INIConfig.IniWriteValue("BankDirect","UseDescription","运费;转款;保险费;备用金;报销款");
                
            }
            cmbUseDescription.Properties.Items.Clear();
            string[] useDescriptions=_INIConfig.IniReadValue("BankDirect","UseDescription").Split(';');
            foreach (var item in useDescriptions)
            {
                if (!string.IsNullOrEmpty(item)&&!useDescriptionList.Contains(item))
                {
                    useDescriptionList.Add(item);
                    cmbUseDescription.Properties.Items.Add(item);
                }
            }
        }

        
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="data"></param>
        void BindingData(object data)
        {
            GetData((Guid)data);
        }
        /// <summary>
        /// 获取银行交易查询条件
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetCustomerBankCondition()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("CustomerID", CurrentRow.CustomerID, false);
            return conditions;
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="id"></param>
        void GetData(Guid id)
        {
            var qcParameter = new PaymentSearchParameter
            {
                CheckAmountIDs = new Guid[]{id},
            };
            APIPaymentInfo data = FinanceService.GetSinglePaymentInfo(qcParameter);
            if (data!=null)
            {
                bsData.DataSource = data;
                bsData.ResetBindings(false);
            }
            cmbPermissionMode.SelectedIndex = 0;
            cmbSettlementMethod.SelectedIndex = 0;

            txtRelativeAccountName.CustomerID = data.CustomerID;
            txtRelativeAccountName.CustomerName = data.RelativeAccountName;
            CurrentRow.PermissionMode = "" + cmbPermissionMode.EditValue;
            CurrentRow.SettlementMethod = "" + cmbSettlementMethod.EditValue;
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns>是否保存成功</returns>
        public override bool SaveData()
        {
            bsData.EndEdit();

            if (!ValidateData())
                return false;
            SinglePaymentSaveRequest saveRequest = new SinglePaymentSaveRequest()
            {
                ValidCode = txtValidCode.Text,
                CompanyID = CurrentRow.CompanyID,
                BankCode = BANKCODE.CMB,
                CurrencyName = CurrentRow.CurrencyName,
                AccountNO = CurrentRow.BankAccountNO,
                PermissionMode = ""+cmbPermissionMode.EditValue,
                RelativeAccountNO = txtRelativeAccountNo.Text,
                RelativeAccountName = txtRelativeAccountName.CustomerName,
                RelativeBranchName = txtRelativeBranchName.Text,
                RelativeBankName = txtRelativeBankName.Text,
                RelativeBankNumber = txtRelativeBankNumber.Text,
                Amount = (decimal)spinAmount.EditValue,
                BusinessID = CurrentRow.BusinessID,
                BusinessNO = txtRefNo.Text,
                Remark = txtRemark.Text,
                SettlementMethod = ""+cmbSettlementMethod.EditValue,
                UseDescription =""+ cmbUseDescription.EditValue,
                SaveByID = LocalData.UserInfo.LoginID,
                SaveBy = LocalData.UserInfo.LoginName,
                UpdateDate = DateTime.Now,
            };
            return FinanceService.SaveSinglePaymentInfo(saveRequest);
        }

        private bool ValidateData()
        {
            string message = "";
            if (("" + cmbUseDescription.EditValue).IsNullOrEmpty())
            {
                message += LocalData.IsEnglish ? "Use Description can not be empty!" : "用途不能为空" + Environment.NewLine;
            }
            if (txtRelativeAccountNo.Text.IsNullOrEmpty())
            {
                message += LocalData.IsEnglish ? "Relative Account No can not be empty!" : "收款账号不能为空" + Environment.NewLine;
            }
            if (txtRelativeAccountName.CustomerName.IsNullOrEmpty())
            {
                message += LocalData.IsEnglish ? "Relative Account Name can not be empty!" : "收款账号名称不能为空" + Environment.NewLine;
            }
            if (txtRelativeBankName.Text.IsNullOrEmpty())
            {
                message += LocalData.IsEnglish ? "Relative Bank Name can not be empty!" : "收款账号开户银行不能为空" + Environment.NewLine;
            }
            if (!message.IsNullOrEmpty())
            {
                MessageBoxService.ShowWarning(message);
                return false;
            }
            return true;
        }
        #endregion

    }
}
