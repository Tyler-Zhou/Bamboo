using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Server;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects.SaveRequests;
using ICP.Framework.CommonLibrary.Common;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary;

namespace ICP.FAM.UI.MonthlyClosingEntry
{
    public partial class EntryEdit : BaseEditPart
    {
        MonthlyClosingAgreement _monthlyClosingEntry2Option = new MonthlyClosingAgreement();
        /// <summary>
        /// 获取口岸下人员信息
        /// </summary>
        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        /// <summary>
        /// 获取当前选择项的数据
        /// </summary>
        MonthlyClosingAgreement2Company CurrentOption2Company
        {
            get { return bsMonthlyClosingEntry2Company.Current as MonthlyClosingAgreement2Company; }
        }


        public EntryEdit()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                Load += new EventHandler(EntryEdit_Load);
            }
            Disposed += delegate
            {
                if (customerFinder != null)
                {
                    customerFinder.Dispose();
                    customerFinder = null;
                }

                if (userNameFinder != null)
                {
                    userNameFinder.Dispose();
                    userNameFinder = null;
                }

                _monthlyClosingEntry2Option = null;
                bsMonthlyClosingEntry2Option.DataSource = null;
                bsMonthlyClosingEntry2Option.Dispose();
                Saved = null;
                list = null;
                if (WorkItem != null)
                {
                    WorkItem.Items.Remove(this);
                    WorkItem = null;
                }

            };
        }

        void EntryEdit_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                SearchRegister();
                SetDataLazyLoaders();
                RicomboxCompanydataBind();
                RicomboxUserDataBind();
                bsMonthlyClosingEntry2Company.DataSource = _monthlyClosingEntry2Option;
                bsMonthlyClosingEntry2Company.ResetBindings(false);
                Closing += new EventHandler<FormClosingEventArgs>(EntryEdit_Closing);
                if (LocalData.IsEnglish)
                {
                    btnAdd.Caption = "New";
                    btnRemove.Caption = "Delete";
                    gcCompanyName.Caption = "Company";
                    gcCreditDate.Caption = "Term";
                    gcUserName.Caption = "Owner";
                }
            }
        }

        void EntryEdit_Closing(object sender, FormClosingEventArgs e)
        {
            if (_monthlyClosingEntry2Option.IsDirty)
            {
                DialogResult dr = FAMUtility.EnquireIsSaveCurrentDataByUpdated();

                if (dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (dr == DialogResult.Yes)
                {
                    if (!Save())
                    {
                        e.Cancel = true;
                    }
                }
            }
        }


        #region 服务
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        #endregion



        #region IEditPart 成员

        public override object DataSource
        {
            get { return bsMonthlyClosingEntry2Option.DataSource; }
            set { BindingData(value); }
        }
        void BindingData(object data)
        {
            MonthlyClosingEntryList dataList = data as MonthlyClosingEntryList;
            if (dataList == null)
            {
                bsMonthlyClosingEntry2Option.DataSource = typeof(MonthlyClosingAgreement);
                Enabled = false;
                return;
            }
            Enabled = true;

            if (dataList.ID == Guid.Empty)
            {
                // 新增行记录的时候 会触发行选择事件
                bsMonthlyClosingEntry2Option.DataSource = _monthlyClosingEntry2Option;
                bsMonthlyClosingEntry2Option.ResetBindings(false);
                bsMonthlyClosingEntry2Company.DataSource = _monthlyClosingEntry2Option.Option2Company;
                bsMonthlyClosingEntry2Company.ResetBindings(false);
            }
            else
            {
                _monthlyClosingEntry2Option = FinanceService.GetMonthlyClosingEntryInfo(((MonthlyClosingEntryList)data).ID);

                _monthlyClosingEntry2Option.IsValid = dataList.IsValid;
                _monthlyClosingEntry2Option.ValidDate = dataList.ValidDate;
                _monthlyClosingEntry2Option.ApplyByName = dataList.ApplyByName;
                _monthlyClosingEntry2Option.Profit = dataList.Profit;
            }


            #region 公司 & 客户类型处理
            if (!_monthlyClosingEntry2Option.CustomerTypes.IsNullOrEmpty())
            {
                foreach (CheckedListBoxItem item in chkcmbCustomerType.Properties.Items)
                {
                    item.CheckState = CheckState.Unchecked;

                    string[] customertypes = _monthlyClosingEntry2Option.CustomerTypes.Split(';');
                    foreach (string customertype in customertypes)
                    {
                        if (item.Value.ToString() == customertype)
                        {
                            item.CheckState = CheckState.Checked;
                        }

                    }
                }
            }
            #endregion

            bsMonthlyClosingEntry2Company.DataSource = _monthlyClosingEntry2Option.Option2Company;
            bsMonthlyClosingEntry2Company.ResetBindings(false);

            bsMonthlyClosingEntry2Option.DataSource = _monthlyClosingEntry2Option;
            bsMonthlyClosingEntry2Option.ResetBindings(false);

            _monthlyClosingEntry2Option.BeginEdit();
        }

        public override bool SaveData()
        {
            return Save();
        }

        public override void EndEdit()
        {
            bsMonthlyClosingEntry2Option.EndEdit();
            bsMonthlyClosingEntry2Company.EndEdit();
        }

        public override event SavedHandler Saved;

        #endregion

        public bool Leaving()
        {
            if (_monthlyClosingEntry2Option != null && _monthlyClosingEntry2Option.IsDirty)
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

        


        public string CustomerTypes
        {
            get
            {
                string customerTypes = "";
                foreach (CheckedListBoxItem item in chkcmbCustomerType.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                    {
                        if (customerTypes == "")
                        {
                            customerTypes = item.Value.ToString();
                        }
                        else
                        {
                            customerTypes += ";" + item.Value.ToString();
                        }
                    }
                }
                return customerTypes;
            }
        }

        #region 搜索器注册
        private IDisposable customerFinder, userNameFinder;
        void SearchRegister()
        {
            //客户搜索器
            customerFinder = DataFindClientService.Register(stxtCustomer, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName,
                  SearchFieldConstants.CustomerResultValue,
                        delegate(object inputSource, object[] resultData)
                        {
                            Guid customerid = new Guid(resultData[0].ToString());
                            if (FinanceService.IsExistMonthlyClosingEntry(customerid))
                            {
                                XtraMessageBox.Show(LocalData.IsEnglish ? "Please check the already exists！" : "此客户已存在月结协议，请查询！",
                                                LocalData.IsEnglish ? "Tips" : "提示",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Question);
                                return;
                            }
                            stxtCustomer.Text = _monthlyClosingEntry2Option.CustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                            stxtCustomer.Tag = _monthlyClosingEntry2Option.CustomerId = new Guid(resultData[0].ToString());

                        }, delegate
                        {
                            stxtCustomer.Text = _monthlyClosingEntry2Option.CustomerName = string.Empty;
                            stxtCustomer.Tag = _monthlyClosingEntry2Option.CustomerId = Guid.Empty;

                        },
                        ClientConstants.MainWorkspace);


            userNameFinder = DataFindClientService.RegisterGridColumnFinder(gcApplyByName
                                               , SystemFinderConstants.UserFinder
                                               , "ApplyByID"
                                               , "ApplyByName"
                                               , "ID"
                                               , LocalData.IsEnglish ? "EName" : "CName");

        }

        #endregion
        List<LocalOrganizationInfo> list;

        #region 下拉列表数据延迟加载
        void SetDataLazyLoaders()
        {
            list = FAMUtility.GetCompanyList();

            List<EnumHelper.ListItem<MonthlyCustomerType>> customerTypes = EnumHelper.GetEnumValues<MonthlyCustomerType>(LocalData.IsEnglish);
            chkcmbCustomerType.Properties.BeginUpdate();
            chkcmbCustomerType.Properties.Items.Clear();
            foreach (var item in customerTypes)
            {
                chkcmbCustomerType.Properties.Items.Add(item.Value, item.Name);
            }
            chkcmbCustomerType.Properties.SelectAllItemCaption = LocalData.IsEnglish ? "All" : "全部";
            chkcmbCustomerType.Properties.EndUpdate();

            cmbRiskRating.SetDataSource<RiskRatingLevel>();
            cmbRiskRating.EditValue = RiskRatingLevel.Level1;

            List<EnumHelper.ListItem<CalculateTermType>> creditTermType = EnumHelper.GetEnumValues<CalculateTermType>(LocalData.IsEnglish);
            foreach (var item in creditTermType)
            {
                if(Convert.ToInt32(item.Value) == 0)
                    continue;
                riicbCalculateTermType.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
        }

        #endregion

        private bool Save()
        {
            txtInsuredAmount.Focus();
            bsMonthlyClosingEntry2Option.EndEdit();
            bsMonthlyClosingEntry2Company.EndEdit();

            if (ValidateData() == false)
            {
                return false;
            }
            MonthlyClosingEntrySaveRequest saveRequest = new MonthlyClosingEntrySaveRequest();
            saveRequest.Id = _monthlyClosingEntry2Option.ID;
            saveRequest.CreateById = LocalData.UserInfo.LoginID;
            saveRequest.CustomerId = _monthlyClosingEntry2Option.CustomerId;

            saveRequest.UpdateDate = _monthlyClosingEntry2Option.UpdateDate;
            saveRequest.CustomerTypes = CustomerTypes;
            saveRequest.IsInsured = _monthlyClosingEntry2Option.IsInsured;
            saveRequest.RiskRating = _monthlyClosingEntry2Option.RiskRating;
            saveRequest.InsuredAmount = _monthlyClosingEntry2Option.InsuredAmount;
            saveRequest.InsuredDate = _monthlyClosingEntry2Option.InsuredDate;


            #region  读取当前月结客户的关联信息
            List<Guid> _CompanyIDs = new List<Guid>();
            List<Guid?> _Userids = new List<Guid?>(), _ApplyByIDs = new List<Guid?>();
            List<CalculateTermType> _PaymentDateType = new List<CalculateTermType>();
            List<int> _PaymentDates = new List<int>();
            List<int> _CreditDates = new List<int>();
            List<Decimal?> _CreditAmount = new List<Decimal?>(),_Estimatedvalue = new List<Decimal?>();
            List<DateTime?> _ApplyTimes = new List<DateTime?>(), _ValidDates = new List<DateTime?>();
            List<bool?> _IsValids = new List<bool?>();
            List<String> _Remarks = new List<String>();

            var mon = bsMonthlyClosingEntry2Company.DataSource as List<MonthlyClosingAgreement2Company>;
            if (mon != null)
            {
                foreach (var it in mon)
                {
                    _CompanyIDs.Add((Guid)it.CompanyId);
                    _Userids.Add(it.UserID);
                    _PaymentDateType.Add(it.CalculateTermType);
                    _PaymentDates.Add(it.PaymentDate);
                    _CreditDates.Add(it.CreditDate);
                    _CreditAmount.Add(it.CreditAmount);
                    _ApplyByIDs.Add(it.ApplyByID);
                    _ApplyTimes.Add(it.ApplyTime);
                    _IsValids.Add(it.IsValid);
                    _Remarks.Add(it.Remark);
                    _ValidDates.Add(it.ValidDate);
                    _Estimatedvalue.Add(it.Estimatedvalue);
                }
            }
            #endregion
            saveRequest.monthlyCompanyIDs = _CompanyIDs;
            saveRequest.UserIDs = _Userids;
            saveRequest.CalculateTermTypes = _PaymentDateType;
            saveRequest.PaymentDates = _PaymentDates;
            saveRequest.CreditDate = _CreditDates;
            saveRequest.CreditAmount = _CreditAmount;
            saveRequest.Estimatedvalue = _Estimatedvalue;

            saveRequest.ApplyTimes = _ApplyTimes;
            saveRequest.IsValids = _IsValids;
            saveRequest.Remarks = _Remarks;
            saveRequest.ValidDates = _ValidDates;
            saveRequest.ApplyByIDs = _ApplyByIDs;

            try
            {
                if (_Userids.Any() == false)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(null, LocalData.IsEnglish ? "Maintenance personnel can't for empty...." : "维护人员不能够为空....");
                    return false;
                }
                SingleResult result = FinanceService.SaveMonthlyClosingEntryInfo(saveRequest);

                _monthlyClosingEntry2Option.ID = result.GetValue<Guid>("ID");
                _monthlyClosingEntry2Option.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                _monthlyClosingEntry2Option.CustomerId = result.GetValue<Guid>("CustomerID");
                _monthlyClosingEntry2Option.CustomerName = result.GetValue<string>("CustomerName");

                _monthlyClosingEntry2Option.IsValid = CurrentOption2Company.IsValid;
                _monthlyClosingEntry2Option.ValidDate = CurrentOption2Company.ValidDate;
                _monthlyClosingEntry2Option.ApplyByName = CurrentOption2Company.ApplyByName;


                if (Saved != null) Saved(_monthlyClosingEntry2Option, new object[] { result });

                _monthlyClosingEntry2Option.EndEdit();
               
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                return false;
            }
        }

        private bool ValidateData()
        {
            EndEdit();

            List<Guid> companyids = new List<Guid>();
            var mon = bsMonthlyClosingEntry2Company.DataSource as List<MonthlyClosingAgreement2Company>;
            foreach (var item in mon)
            {
                if (DataTypeHelper.GetGuid(item.CompanyId, Guid.Empty) == Guid.Empty)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "must input company" : "公司必须输入");
                    return false;
                }
                if (DataTypeHelper.GetGuid(item.ApplyByID, Guid.Empty) == Guid.Empty)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "must input ApplyBy" : "申请人必须输入");
                    return false;
                }
                if (DataTypeHelper.GetGuid(item.UserID, Guid.Empty) == Guid.Empty)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "must input user" : "维护人员");
                    return false;
                }
    
                companyids.Add(item.CompanyId);
            }
            List<Guid> companyidsnew = companyids.Distinct().ToList();
            if (companyids.Count != companyidsnew.Count)
            {
                DialogResult dialogResult = XtraMessageBox.Show(LocalData.IsEnglish ? "公司不能重复?" : "公司不能重复？",
                                                LocalData.IsEnglish ? "Tips" : "提示",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Question);
                return false;
            }

            return _monthlyClosingEntry2Option.Validate();
        }

        private void bbiSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Save();
            }
        }

        /// <summary>
        ///  公司下拉框绑定值
        /// </summary>
        private void RicomboxCompanydataBind()
        {
            list = FAMUtility.GetCompanyList();
            rICompany.BeginUpdate();
            rICompany.Items.Clear();
            foreach (var item in list)
            {
                string name = LocalData.IsEnglish ? item.EShortName : item.CShortName;
                rICompany.Items.Add(new ImageComboBoxItem(name, item.ID));
            }
            rICompany.EndUpdate();
        }

        /// <summary>
        /// 绑定维护人员
        /// </summary>
        /// <param name="CompanyId"></param>
        private void RicomboxUserDataBind()
        {

            List<UserList> userList = UserService.GetUnderlingUserList(null, new string[] { "计费" }, null, true);
            if (userList.Any())
            {
                rIUser.BeginUpdate();
                rIUser.Items.Clear();
                foreach (var item in userList)
                {
                    string name = LocalData.IsEnglish ? item.EName : item.CName;
                    rIUser.Items.Add(new ImageComboBoxItem(name, item.ID));
                }
                rIUser.EndUpdate();
            }

        }

        private void btnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                InsertEntryInfo();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }

        private void btnRemove_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                int rowhandle = gvmain.FocusedRowHandle;
                DialogResult dialogResult = XtraMessageBox.Show(LocalData.IsEnglish ? "Sure to delete records?" : "确定删除记录吗？",
                                                  LocalData.IsEnglish ? "Delete" : "删除",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    _monthlyClosingEntry2Option.IsDirty = true;
                    gvmain.DeleteRow(gvmain.FocusedRowHandle);
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }

        /// <summary>
        /// 新增加子表信息
        /// </summary>
        public void InsertEntryInfo()
        {
            var monthlyClosingEntryInfo = new MonthlyClosingAgreement2Company
            {
                CompanyId = LocalData.UserInfo.DefaultCompanyID,
                CreditDate = 0,
                UserID = LocalData.UserInfo.LoginID,
                IsValid=true,
                ApplyTime=DateTime.Now
            };
            _monthlyClosingEntry2Option.IsDirty = true;
            RicomboxCompanydataBind();
            RicomboxUserDataBind();
            bsMonthlyClosingEntry2Company.Add(monthlyClosingEntryInfo);
        }

        private void gvmain_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e == null || e.Column == null || CurrentOption2Company == null)
            {
                return;
            }

            if (e.Column.UnboundType == UnboundColumnType.Boolean)
            {
                if (e.Column == gcIsValid)
                {
                    CurrentOption2Company.IsValid = !CurrentOption2Company.IsValid;
                }
                bsMonthlyClosingEntry2Company.ResetCurrentItem();
            }
        }

        private void gvmain_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            MonthlyClosingAgreement2Company list = gvmain.GetRow(e.RowHandle) as MonthlyClosingAgreement2Company;
            if (list == null) return;
            if (list.IsValid == false)
            {
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.Disabled);
            }
            else
            {
                e.Appearance.BackColor = Color.White;
            }
        }

        private void chkIsInsured_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckEdit checkControl = sender as CheckEdit;
                if(checkControl!=null)
                {
                    cmbRiskRating.Properties.ReadOnly = txtInsuredAmount.Properties.ReadOnly = txtInsuredDate.Properties.ReadOnly = !checkControl.Checked;
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }

        private void barCustomerPreferences_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (CurrentOption2Company == null) return;
            SetCustomerPreferences voucherPart = new SetCustomerPreferences();
            voucherPart.SetCompanyAndCustomer(CurrentOption2Company.CompanyId, CurrentOption2Company.CompanyName, _monthlyClosingEntry2Option.CustomerId, _monthlyClosingEntry2Option.CustomerName);
            string title = LocalData.IsEnglish ? "Customer Preferences Set" : "客户账单偏好设置";
            PartLoader.ShowDialog(voucherPart,title);
        }

    }
}
