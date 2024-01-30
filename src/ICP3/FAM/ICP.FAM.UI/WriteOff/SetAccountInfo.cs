using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.Common.ServiceInterface;
using ICP.FAM.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.Controls;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary;

namespace ICP.FAM.UI.WriteOff
{
    public partial class SetAccountInfo : BaseEditPart
    {
        public SetAccountInfo()
        {
            InitializeComponent();
            Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };

        }

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

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        #endregion

        #region 属性
        /// <summary>
        /// 销账信息
        /// </summary>
        public WriteOffItemList ItemList
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
                InitMessage();
                InitControls();
            }
        }
        public List<BankAccountList> AccountList
        {
            get;
            set;
        }
        /// <summary>
        /// 原来的银行
        /// </summary>
        public Guid OldBankAccountID
        {
            get;
            set;
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            //初始化银行账号
            AccountList = FinanceService.GetCompanyBankAccounts(ItemList.CompanyID, LocalData.IsEnglish).OrderBy(i => i.CurrencyName).ToList();
            foreach (BankAccountList account in AccountList)
            {
                cmbBankAccountID.Properties.Items.Add(new ImageComboBoxItem(account.CurrencyName, account.ID));
            }
            OldBankAccountID =DataTypeHelper.GetGuid(ItemList.BankAccountID);

            cmbBankAccountID.EditValue = ItemList.BankAccountID;
            dtpReachedDate.EditValue = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            numFinalAmount.Value = ItemList.Amount;
            if (ItemList.Type == FeeWay.AR)
            {
                labelControl2.Text = LocalData.IsEnglish? "Amount of paid-up":"实收金额";
            }
            else
            {
                labelControl2.Text = LocalData.IsEnglish? "Amount actually paid":"实付金额";
            }
        }

        private void InitMessage()
        {
            RegisterMessage("11092600001", LocalData.IsEnglish?"Input the Account Date": "请录入到账日期");
            RegisterMessage("11092600002", LocalData.IsEnglish?"Input the bankAccount":"请选择银行账号");
            RegisterMessage("1409160001", LocalData.IsEnglish ? "The choice of bank currency {0} with the previously selected bank currency {1} is inconsistentt,Whether to continue to check" : "本次选择的银行币种{ 0 }跟之前选择的银行币种{ 1 }不一致,是否继续销账?");
            RegisterMessage("1409160002", LocalData.IsEnglish ? "Please confirm the write off bill, bank information is not found" : "未找到销账银行请在销账界面中重新选择银行");
        }

        #endregion

        #region 关闭
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            FindForm().DialogResult = DialogResult.Cancel;
            FindForm().Close();
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {           
            if (dtpReachedDate.EditValue == null)
            {
                FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "11092600001"));
                return;
            }

            if (cmbBankAccountID.EditValue == null || (Guid)cmbBankAccountID.EditValue == Guid.Empty)
            {
                FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "11092600002"));
                return;
            }

            //if (ItemList.WriteOffDate != null && dtpReachedDate.DateTime < ItemList.WriteOffDate)
            //{
            //    Utility.ShowMessage(LocalData.IsEnglish ? "BANKDATE can not be less than CHECKDATE " : "到帐日期不能小于核销日期！");
            //    return;
            //}

            //判断币种是否发生改变了
            string oldCurrency;
            try
            {
                oldCurrency = (from d in AccountList where d.ID == OldBankAccountID select d.CurrencyEName).SingleOrDefault();
            }
            catch
            {
                FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1409160002"));
                return;
            }
           
            string newCurrency = (from d in AccountList where d.ID == DataTypeHelper.GetGuid(cmbBankAccountID.EditValue) select d.CurrencyEName).SingleOrDefault();
            if (string.IsNullOrEmpty(oldCurrency))
            {
                FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1409160002"));
                return;
            }

            if (oldCurrency != newCurrency)
            {
                bool isSave = FAMUtility.ShowResultMessage(string.Format(NativeLanguageService.GetText(this, "1409160001"),newCurrency,oldCurrency));
                if (!isSave)
                {
                    return;
                }
            }


            ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(ItemList.CompanyID);
            if (configureInfo.SolutionID == new Guid("b6e4dded-4359-456a-b835-e8401c910fd0")) //是否远东区解决方案的销帐单
            {
                string message = FinanceService.CheckExistBankReceived(new Guid[] { ItemList.ID },
                                                                       new DateTime?[] { dtpReachedDate.DateTime },
                                                                       new decimal[] { numFinalAmount.Value },
                                                                       new Guid?[] { (Guid)cmbBankAccountID.EditValue });
                if (!string.IsNullOrEmpty(message))
                {
                    DialogResult result = XtraMessageBox.Show(message
                 , LocalData.IsEnglish ? "Tip" : "提示"
                 , MessageBoxButtons.YesNo
                 , MessageBoxIcon.Question);

                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }
            }

            if (ItemList.Amount != numFinalAmount.Value)
            {
                #region 金额有差异时，需要手动录入手续费

                PaidSetExpenses expense = Workitem.Items.AddNew<PaidSetExpenses>();

                #region 创建列表数据
                List<WriteOffCharge> dataList = new List<WriteOffCharge>();

                WriteOffCharge charge = new WriteOffCharge();
                charge.CurrencyID = ItemList.CurrencyID;
                charge.ExchangeRate = 1;
                charge.Amount = Math.Abs(ItemList.Amount - numFinalAmount.Value);
                charge.CheckID = ItemList.CheckID;
                charge.GLID = new Guid("20FD2442-56C8-4EC0-A0BE-8D8ED71A0175");
                charge.GLFullName = "(660301) 财务费用->手续费";

                if (numFinalAmount.Value - ItemList.Amount > 0)
                {
                    charge.Way = ItemList.Type;
                }
                else
                {
                    charge.Way = ItemList.Type == FeeWay.AR ? FeeWay.AP : FeeWay.AR;
                }
               
                ConfigureInfo companyInfo = ConfigureService.GetCompanyConfigureInfo(ItemList.CompanyID);
                if (companyInfo != null)
                {
                    charge.CustomerID = companyInfo.CustomerID;
                    charge.CustomerName = companyInfo.CustomerName;
                }

                dataList.Add(charge);

                #endregion

                expense.CompnayID = ItemList.CompanyID;
                expense.DataSourceList = dataList;

                string title = LocalData.IsEnglish ? "Set Difference" : "设置差异";
                if (PartLoader.ShowDialog(expense, title) == DialogResult.OK)
                {
                    #region 保存差异与到账信息

                    ItemList.ReachedDate = dtpReachedDate.DateTime;
                    ItemList.FinalAmount = numFinalAmount.Value;
                    ItemList.BankAccountID = (Guid)cmbBankAccountID.EditValue;

                    try
                    {
                        ManyResult result = FinanceService.WriteOffReached(
                                new Guid[] { ItemList.ID },
                                new DateTime?[] { ItemList.ReachedDate },
                                new decimal[] { ItemList.FinalAmount },
                                new Guid?[] { ItemList.BankAccountID },
                                new DateTime?[] { ItemList.UpdateDate },
                                LocalData.UserInfo.LoginID,
                                expense.ExpenseList);

                        ItemList.BankAccount = cmbBankAccountID.Text;
                        ItemList.UpdateDate = result.Items[0].GetValue<DateTime>("UpdateDate");
                        ItemList.BankByName = LocalData.UserInfo.LoginName;
                        FindForm().DialogResult = DialogResult.OK;
                        FindForm().Close();
                    }
                    catch (Exception ex)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                    }

                    #endregion
                }

                #endregion
            }
            else
            {
                #region 没有差异时，直接到账

                ItemList.ReachedDate = dtpReachedDate.DateTime;
                ItemList.FinalAmount = numFinalAmount.Value;
                ItemList.BankAccountID = (Guid)cmbBankAccountID.EditValue;

                try
                {
                    ManyResult result = FinanceService.WriteOffReachedByCheck(
                            new Guid[] { ItemList.ID },
                            new DateTime?[] { ItemList.ReachedDate },
                            new decimal[] { ItemList.FinalAmount },
                            new Guid?[] { ItemList.BankAccountID },
                            new DateTime?[] { ItemList.UpdateDate },
                            LocalData.UserInfo.LoginID);


                    ItemList.BankAccount = cmbBankAccountID.Text;
                    ItemList.UpdateDate = result.Items[0].GetValue<DateTime>("UpdateDate");
                    ItemList.BankByName = LocalData.UserInfo.LoginName;


                    FindForm().DialogResult = DialogResult.OK;

                    FindForm().Close();


                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                }
                #endregion
            }
        }
        #endregion
    }

}