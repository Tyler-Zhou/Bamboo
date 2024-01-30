using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface.DataObjects.SaveRequests;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace ICP.FAM.UI
{
    /// <summary>
    /// 银行编辑面板
    /// </summary>
    [ToolboxItem(false)]
    public partial class BankEdit : BaseEditPart
    {
        #region 成员属性
        /// <summary>
        /// 账号/客户查找器
        /// </summary>
        private IDisposable accountFinder, customerFinder;
        /// <summary>
        /// 解决方案ID
        /// </summary>
        Guid SolutionID
        {
            get;
            set;
        }
        /// <summary>
        /// 操作口岸ID
        /// </summary>
        Guid CompanyID
        {
            get
            {
                if (_bankInfo == null || FAMUtility.GuidIsNullOrEmpty(_bankInfo.CompanyId))
                {
                    return LocalData.UserInfo.DefaultCompanyID;
                }

                return _bankInfo.CompanyId;
            }
        }
        /// <summary>
        /// 当前编辑银行信息
        /// </summary>
        BankInfo _bankInfo { get; set; }
        /// <summary>
        /// 银行账号列表
        /// </summary>
        List<BankAccountInfo> bankAccountInfoList { get; set; }
        /// <summary>
        /// 银行账号列表
        /// </summary>
        List<BankAccountInfo> BankAccountInfoList
        {
            get
            {
                bsAccountList.EndEdit();
                List<BankAccountInfo> list = bsAccountList.DataSource as List<BankAccountInfo>;

                if (list == null)
                {
                    list = new List<BankAccountInfo>();
                }

                return list;
            }
        }
        bool isDirty;
        /// <summary>
        /// 数据是否变更(IsDirty)
        /// </summary>
        private bool IsDirty
        {
            get
            {
                if (isDirty)
                {
                    return true;
                }
                if (_bankInfo.IsDirty)
                {
                    return true;
                }
                foreach (BankAccountInfo item in BankAccountInfoList)
                {
                    if (item.IsDirty)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
        /// <summary>
        /// 当前行
        /// </summary>
        private BankAccountInfo CurrentRow
        {
            get
            {
                return bsAccountList.Current as BankAccountInfo;
            }
        }

        /// <summary>
        /// 数据源
        /// </summary>
        public override object DataSource
        {
            get { return bsBankInfo.DataSource; }
            set { BindingData(value); }
        }
        #endregion

        #region 服务
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        /// <summary>
        /// 数据查找服务
        /// </summary>
        IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        /// <summary>
        /// 财务服务
        /// </summary>
        IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        #endregion

        #region 构造函数
        /// <summary>
        /// 银行编辑面板
        /// </summary>
        public BankEdit()
        {
            InitializeComponent();


            Load += new EventHandler(BankEdit_Load);
            Disposed += delegate
            {
                if (accountFinder != null)
                {
                    accountFinder.Dispose();
                    accountFinder = null;
                }
                if (customerFinder != null)
                {
                    customerFinder.Dispose();
                    customerFinder = null;
                }
                _bankInfo = null;
                Saved = null;
                gcMain.DataSource = null;
                bankAccountInfoList = null;
                cmbCompany.SelectedIndexChanged -= cmbCompany_SelectedIndexChanged;
                SmartPartClosing -= BankEdit_SmartPartClosing;
                bsAccountList.PositionChanged -= bsAccountList_PositionChanged;
                bsAccountList.DataSource = null;
                bsAccountList.Dispose();
                bsBankInfo.DataSource = null;
                bsBankInfo.Dispose();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }

            };

        } 
        #endregion

        #region 事件
        /// <summary>
        /// 保存事件
        /// </summary>
        public override event SavedHandler Saved;
        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BankEdit_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                AttachEvents();
                InitMessage();
                InitControls();
                SearchRegister();
                SmartPartClosing += BankEdit_SmartPartClosing;
                ActivateSmartPartClosingEvent(Workitem);
            }
        }
        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().Close();
        }

        void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (FAMUtility.GuidIsNullOrEmpty(_bankInfo.CompanyId))
                {
                    rcbxCurrencies.Items.Clear();
                    return;
                }

                EndEdit();

                ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(_bankInfo.CompanyId);
                SolutionID = configureInfo.SolutionID;
                rcbxCurrencies.Items.Clear();

                if (configureInfo != null)
                {
                    List<SolutionCurrencyList> currency = ConfigureService.GetSolutionCurrencyList(configureInfo.SolutionID, true);

                    if (currency != null)
                    {
                        foreach (var item in currency)
                        {
                            rcbxCurrencies.Items.Add(new ImageComboBoxItem(item.CurrencyName, item.CurrencyID));
                        }
                    }
                }

                //this.rcbxGLs.Items.Clear();

                //List<SolutionGLCodeList> list = Utility.GetGLCodeList(this._bankInfo.CompanyId);

                //if (list != null)
                //{
                //    foreach (var item in list)
                //    {
                //        this.rcbxGLs.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.CName, item.ID));
                //    }
                //}
            }
        }

        private void bbiNewAccount_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                BankAccountInfo account = new BankAccountInfo();
                account.ID = Guid.Empty;
                account.BankID = _bankInfo.ID;
                account.CreateByID = LocalData.UserInfo.LoginID;
                account.CreateByName = LocalData.UserInfo.LoginName;
                account.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                account.IsValid = true;

                bsAccountList.Add(account);

                bsAccountList.ResetBindings(false);

                isDirty = true;
            }
        }


        private void bbiDeleteAccount_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (CurrentRow != null)
            {
                if (FAMUtility.GuidIsNullOrEmpty(CurrentRow.ID))
                {
                    #region 删除

                    bsAccountList.RemoveCurrent();

                    #endregion
                }
                else
                {
                    #region 作废
                    if (CurrentRow.IsValid)
                    {
                        if (!FAMUtility.ShowResultMessage(NativeLanguageService.GetText(this, "1108100001")))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (!FAMUtility.ShowResultMessage(NativeLanguageService.GetText(this, "1108100002")))
                        {
                            return;
                        }
                    }

                    string failureMessage = string.Empty;
                    if (CurrentRow.IsValid)
                        failureMessage = LocalData.IsEnglish ? "Cancel Booking State Failed." : "作废银行信息失败.";
                    else
                        failureMessage = LocalData.IsEnglish ? "Available Booking State Failed." : "激活银行信息失败.";

                    try
                    {
                        SingleResult result = FinanceService.ChangeBankAccountValidity(CurrentRow.ID,
                            CurrentRow.IsValid,
                            LocalData.UserInfo.LoginID,
                            CurrentRow.UpdateDate,
                            LocalData.IsEnglish);

                        CurrentRow.IsValid = !CurrentRow.IsValid;
                        CurrentRow.UpdateDate = result.GetValue<DateTime?>("UpdateDate");

                        if (CurrentRow.IsValid)
                        {
                            bbiDeleteAccount.Caption = LocalData.IsEnglish ? "Invalid" : "作废";
                            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Cancel Booking Successfully." : "账号信息已经成功作废.");
                        }
                        else
                        {
                            bbiDeleteAccount.Caption = LocalData.IsEnglish ? "Activation" : "激活";
                            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Available Booking Successfully." : "账号信息已经成功激活.");
                        }

                    }
                    catch (Exception ex)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), failureMessage + ex.Message);
                    }
                    #endregion
                }
            }
        }

        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Save();
            }
        }

        private void barSetAccount_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetBankAndCompany setBank = Workitem.Items.AddNew<SetBankAndCompany>();
            setBank.BankID = _bankInfo.ID;
            string title = LocalData.IsEnglish ? "Set Company" : "设置默认账号";
            PartLoader.ShowDialog(setBank, title);

        }

        private void bsAccountList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentRow == null)
            {
                return;
            }
            if (FAMUtility.GuidIsNullOrEmpty(CurrentRow.ID))
            {
                string name = LocalData.IsEnglish ? "&Delete" : "删除(&D)";
                bbiDeleteAccount.Caption = name;
            }
            else
            {
                string name = LocalData.IsEnglish ? "Invalid(&D)" : "作废(&D)";
                bbiDeleteAccount.Caption = name;
            }
        }

        private void gvMain_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            isDirty = true;
        }
        /// <summary>
        /// 行单击时
        /// </summary>
        private void gvMain_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column == colIsValid)
            {
                BankAccountInfo account = bsAccountList.Current as BankAccountInfo;
                if (account != null)
                {
                    account.IsValid = !account.IsValid;
                }

                bsAccountList.ResetBindings(false);
            }
        }
        /// <summary>
        /// 关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BankEdit_SmartPartClosing(object sender, WorkspaceCancelEventArgs e)
        {
            e.Cancel = !Leaving();
        } 
        #endregion

        #region 方法
        
        /// <summary>
        /// IEditPart 成员 保存数据
        /// </summary>
        /// <returns></returns>
        public override bool SaveData()
        {
            return Save();
        }
        /// <summary>
        /// IEditPart 成员 结束编辑
        /// </summary>
        public override void EndEdit()
        {
            _bankInfo.EndEdit();
            gvMain.CloseEditor();

            foreach (BankAccountInfo item in BankAccountInfoList)
            {
                item.EndEdit();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        void InitMessage()
        {
            RegisterMessage("1108100001", LocalData.IsEnglish ? "Are you sure to invalidate the selected time?" : "确认要作废该数据？");
            RegisterMessage("1108100002", LocalData.IsEnglish ? "Are you sure to resume the selected time?" : "确认要激活该数据？");
            RegisterMessage("1110250001", LocalData.IsEnglish ? "Account number cannot be longer than 30bits" : "帐号的长度不能超过30位");
            RegisterMessage("1110250002", LocalData.IsEnglish ? "The Remark cannot be longer than 200bits" : "备注的的长度不能超过200位");
        }
        /// <summary>
        /// 
        /// </summary>
        void InitControls()
        {
            FAMUtility.BindComboBoxByCompany(cmbCompany);

            if (_bankInfo.ID == Guid.Empty)
            {
                cmbCompany.ShowSelectedValue(LocalData.UserInfo.DefaultCompanyID, LocalData.UserInfo.DefaultCompanyName);
                _bankInfo.CompanyId = LocalData.UserInfo.DefaultCompanyID;
                _bankInfo.CompanyName = LocalData.UserInfo.DefaultCompanyName;

                _bankInfo.IsDirty = false;

                barSetAccount.Enabled = false;
            }
            else
            {
                if (_bankInfo.IsValid)
                {
                    barSetAccount.Enabled = true;
                    barSave.Enabled = true;
                }
                else
                {
                    barSetAccount.Enabled = false;
                    barSave.Enabled = false;
                }
            }

        }
        /// <summary>
        /// IEditPart 成员 绑定数据
        /// </summary>
        /// <param name="data"></param>
        void BindingData(object data)
        {
            if (data == null)
            {
                _bankInfo = new BankInfo();
                _bankInfo.ID = Guid.Empty;
                _bankInfo.IsValid = true;
                _bankInfo.CreateByID = LocalData.UserInfo.LoginID;
                _bankInfo.CreateByName = LocalData.UserInfo.LoginName;
                _bankInfo.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            }
            else
            {
                _bankInfo = FinanceService.GetBankInfo(((BankList)data).ID, LocalData.IsEnglish);
            }

            bsBankInfo.DataSource = _bankInfo;

            bsBankInfo.ResetBindings(false);
            _bankInfo.CancelEdit();
            _bankInfo.BeginEdit();


            bankAccountInfoList = _bankInfo.BankAccountInfoList;
            bsAccountList.DataSource = bankAccountInfoList;
            bsAccountList.ResetBindings(false);


        }
        /// <summary>
        /// 离开焦点
        /// </summary>
        /// <returns></returns>
        bool Leaving()
        {
            if (IsDirty)
            {
                if (FAMUtility.EnquireIsSaveCurrentDataByUpdated() == DialogResult.Yes)
                {
                    return Save();
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        void SearchRegister()
        {
            //客户搜索器
            customerFinder = DataFindClientService.Register(stxtCustomer, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName,
                  SearchFieldConstants.CustomerResultValue,
                        delegate(object inputSource, object[] resultData)
                        {
                            stxtCustomer.Text = _bankInfo.CustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                            stxtCustomer.Tag = _bankInfo.CustomerId = new Guid(resultData[0].ToString());
                        }, delegate
                        {
                            stxtCustomer.Text = _bankInfo.CustomerName = string.Empty;
                            stxtCustomer.Tag = _bankInfo.CustomerId = Guid.Empty;

                        },
                        ClientConstants.MainWorkspace);

            accountFinder = DataFindClientService.RegisterGridColumnFinder(colAccounts
                                      , FAMFinderConstants.GLCodeFinder
                                      , "GLID"
                                      , "GLFullName"
                                      , "ID"
                                      , "GLCodeName"
                                      , GetSolutionIDSearchCondition);
        }
        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetSolutionIDSearchCondition()
        {
            List<Guid> idList = new List<Guid>();
            idList.Add(CompanyID);

            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("SolutionID", SolutionID, false);
            conditions.AddWithValue("OnlyLeaf", true, false);
            conditions.AddWithValue("CompanyIDs", idList, false);
            return conditions;
        }
        /// <summary>
        /// 
        /// </summary>
        void AttachEvents()
        {
            cmbCompany.SelectedIndexChanged += new EventHandler(cmbCompany_SelectedIndexChanged);
        }
        /// <summary>
        /// 数据验证
        /// </summary>
        /// <returns></returns>
        bool ValidateData()
        {
            EndEdit();
            bsBankInfo.EndEdit();
            bsAccountList.EndEdit();


            if (!_bankInfo.Validate())
            {
                return false;
            }
            foreach (BankAccountInfo bank in BankAccountInfoList)
            {
                if (!bank.Validate())
                {
                    return false;
                }
                if (!string.IsNullOrEmpty(bank.AccountNo) && bank.AccountNo.Length > 30)
                {
                    FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1110250001"));
                    return false;
                }
                if (!string.IsNullOrEmpty(bank.Remark) && bank.Remark.Length > 200)
                {
                    FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1110250002"));
                    return false;
                }

            }
            return true;


        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool Save()
        {
            if (!ValidateData())
            {
                return false;
            }

            if (!IsDirty)
            {
                return true;
            }

            BankSaveRequest saveRequest = new BankSaveRequest();
            saveRequest.Id = _bankInfo.ID;
            saveRequest.CompanyId = _bankInfo.CompanyId;
            saveRequest.CreateById = LocalData.UserInfo.LoginID;
            saveRequest.CustomerId = _bankInfo.CustomerId;
            saveRequest.IsValid = _bankInfo.IsValid;
            saveRequest.Remark = _bankInfo.Remark;
            saveRequest.UpdateDate = _bankInfo.UpdateDate;
            saveRequest.CShortName = _bankInfo.CShortName;
            saveRequest.EShortName = _bankInfo.EShortName;
            saveRequest.CName = _bankInfo.CName;
            saveRequest.EName = _bankInfo.EName;
            saveRequest.CAddress = _bankInfo.CAddress;
            saveRequest.EAddress = _bankInfo.EAddress;
            saveRequest.Contact = _bankInfo.Contact;
            saveRequest.Tel = _bankInfo.Tel1;
            saveRequest.Fax = _bankInfo.Fax;
            saveRequest.PostalCode = _bankInfo.PostCode;
            saveRequest.Remark = _bankInfo.Remark;
            saveRequest.UpdateDate = _bankInfo.UpdateDate;

            List<BankAccountSaveRequest> list = new List<BankAccountSaveRequest>();

            foreach (BankAccountInfo bank in BankAccountInfoList)
            {
                BankAccountSaveRequest accountRequest = new BankAccountSaveRequest
                {
                    AcccountNo = bank.AccountNo,
                    BankId = bank.BankID,
                    CurrencyId = bank.CurrencyID,
                    GlId = bank.GLID,
                    Id = bank.ID,
                    IsValid = bank.IsValid,
                    IsShowInInvoiceBankList = bank.IsShowInInvoiceBankList,
                    IsOpen = bank.IsOpen,
                    IsSupportDirectBank = bank.IsSupportDirectBank,
                    Remark = bank.Remark,
                    SaveById = bank.CreateByID,
                    UpdateDate = bank.UpdateDate
                };

                list.Add(accountRequest);
            }

            try
            {
                SingleResult result = FinanceService.SaveBankInfo(saveRequest, list.ToArray(), LocalData.IsEnglish);

                _bankInfo.ID = result.GetValue<Guid>("ID");
                _bankInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                if (Saved != null) Saved(_bankInfo, new object[] { result });

                _bankInfo.CancelEdit();
                barSetAccount.Enabled = true;

                List<BankAccountInfo> accountList = FinanceService.GetBankInfo(_bankInfo.ID, LocalData.IsEnglish).BankAccountInfoList;
                bsAccountList.DataSource = accountList;
                bsAccountList.ResetBindings(false);

                isDirty = false;

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                return false;
            }
        }
        #endregion
    }
}
