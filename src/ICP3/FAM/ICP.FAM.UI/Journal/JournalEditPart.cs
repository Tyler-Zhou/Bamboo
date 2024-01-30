using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects.SaveRequests;
using ICP.Framework.CommonLibrary.Common;
using ICP.Common.ServiceInterface.Client;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.FAM.ServiceInterface.DataObjects.Report;
using ICP.Framework.ClientComponents.Service;
using JournalDetailReportData = ICP.FAM.ServiceInterface.DataObjects.Report.JournalDetailReportData;

namespace ICP.FAM.UI
{
    [ToolboxItem(false)]
    public partial class JournalEditPart : BaseEditPart
    {
        IDataFindClientService dfService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        public JournalEditPart()
        {
            InitializeComponent();
            Disposed += delegate {
                SmartPartClosing -= JournalEdit_SmartPartClosing;
                cmbCompanyID.SelectedIndexChanged -= cmbCompany_SelectedIndexChanged;
                Saved = null;
                gcMain.DataSource = null;
                bsDetails.DataSource = null;
                bsDetails.Dispose();
                bsJournalEdit.DataSource = null;
                bsJournalEdit.Dispose();

                if (customerFinder != null)
                {
                    customerFinder.Dispose();
                    customerFinder = null;
                }
                
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            
            };
            Load += new EventHandler(JournalEdit_Load);
        }

        #region 初始化

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void JournalEdit_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                InitMessage();
                InitControls();
                AttachEvents();
                SmartPartClosing += JournalEdit_SmartPartClosing;
                ActivateSmartPartClosingEvent(Workitem);
                BindCombo();//初始化details的combobox数据   2011-09-06
                SetButtomEnabled();

                _journalInfo.IsDirty = false;

            }
        }

        /// <summary>
        /// 初始化消息
        /// </summary>
        private void InitMessage()
        {
            RegisterMessage("1110100001", LocalData.IsEnglish ? "Input the detail data" : "请输入明细数据");
            RegisterMessage("1110100002", LocalData.IsEnglish ? "Payment amount is not consistent" : "收付金额不一致");
        }

        private IDisposable customerFinder;
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            FAMUtility.BindComboBoxByCompany(cmbCompanyID);
            if (_journalInfo.ID == Guid.Empty)
            {
                cmbCompanyID.EditValue = LocalData.UserInfo.DefaultCompanyID;
                cmbCompanyID.Text = LocalData.UserInfo.DefaultCompanyName;
            }//默认
            else
            {
                if (_journalInfo.IsValid)
                {
                    barSave.Enabled = true;
                }
                else
                {
                    barSave.Enabled = false;
                }
            }

            customerFinder = dfService.RegisterGridColumnFinder(colCustomer
                   , CommonFinderConstants.CustoemrFinder
                   , "CustomerID"
                   , "CustomerName"
                   , "ID"
                   , LocalData.IsEnglish ? "EName" : "CName",
                   GetCustomerStateCondition);
        }

        SearchConditionCollection GetCustomerStateCondition()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("CodeApplyState", CustomerCodeApplyState.Passed, false);
            return conditions;
        }

        /// <summary>
        /// 设置工作栏按钮的可用性
        /// </summary>
        private void SetButtomEnabled()
        {
            ConfigureInfo configure = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID, LocalData.IsEnglish);
            if (configure != null)
            {
                if (_journalInfo != null)
                {
                    if (_journalInfo.PostDate > configure.AccountingClosingdate.Value.Date)
                    {
                        barSave.Enabled = true;
                        barDelete.Enabled = true;
                        barAdd.Enabled = true;
                        barSave.Caption = "保存(&S)";
                    }
                    else
                    {
                        barSave.Caption = (LocalData.IsEnglish ? "Accounting Closing" : "财务已关帐");
                        barSave.Enabled = false;
                        barDelete.Enabled = false;
                        barAdd.Enabled = false;
                    }
                      
                }
            }
        }

        #endregion

        #region 关闭

        void JournalEdit_SmartPartClosing(object sender, WorkspaceCancelEventArgs e)
        {
            if (barSave.Enabled)
            {
                e.Cancel = !Leaving();
            }
        }

        public bool Leaving()
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
        #endregion

        #region 绑定
        private void AttachEvents()
        {
            cmbCompanyID.SelectedIndexChanged += new EventHandler(cmbCompany_SelectedIndexChanged);
        }
        void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCombo();
        }

        /// <summary>
        ///  for test method  2011-09-06
        /// </summary>
        private void BindCombo()
        {
            _dicCurrency = new Dictionary<Guid, string>();
            _dicSolutionGL = new Dictionary<Guid, string>();
            ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo((Guid)cmbCompanyID.Value);
            cur.Items.Clear();

            if (configureInfo != null)
            {
                List<SolutionCurrencyList> currencyList = ConfigureService.GetSolutionCurrencyList(configureInfo.SolutionID, true);

                if (currencyList != null)
                {
                    foreach (var item in currencyList)
                    {
                        cur.Items.Add(new ImageComboBoxItem(item.CurrencyName, item.CurrencyID));

                        _dicCurrency.Add(item.CurrencyID, item.CurrencyName);
                        //JournalDetail jour = new JournalDetail();
                    }
                }

            }

            GLID.Items.Clear();

            List<SolutionGLCodeList> solutionGLCodeList = FAMUtility.GetGLCodeList((Guid)cmbCompanyID.Value);

            if (solutionGLCodeList != null)
            {
                foreach (var item in solutionGLCodeList)
                {
                    GLID.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
                    _dicSolutionGL.Add(item.ID, LocalData.IsEnglish ? item.EName : item.CName);
                }
            }
        }
        #endregion

        #region 属性及变量

        /// <summary>
        /// 主表
        /// </summary>
        JournalInfo _journalInfo;
        List<JournalDetail> details;
        /// <summary>
        /// 明细列表
        /// </summary>
        public List<JournalDetail> Details
        {
            get
            {
                bsDetails.EndEdit();
                List<JournalDetail> list = bsDetails.DataSource as List<JournalDetail>;
                if (list == null)
                {
                    list = new List<JournalDetail>();
                }
                return list;
            }
        }
        /// <summary>
        /// 当前对象
        /// </summary>
        JournalDetail _CurrentData
        {
            get
            {
                if (bsDetails.DataSource == null) return null;
                else return bsDetails.DataSource as JournalDetail;
            }
            set
            {
                if (_CurrentData != null)
                {
                    JournalDetail currentData = _CurrentData;
                    currentData = value;
                }
            }
        }

        /// <summary>
        /// 明细当前行
        /// </summary>
        private JournalDetail CurrentRow
        {
            get
            {
                return bsDetails.Current as JournalDetail;
            }
        }

        Dictionary<Guid, string> _dicCurrency = new Dictionary<Guid, string>();
        Dictionary<Guid, string> _dicSolutionGL = new Dictionary<Guid, string>();

        #endregion

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

        public IReportViewService ReportViewService
        {
            get
            {
                return ServiceClient.GetClientService<IReportViewService>();
            }
        }

        #endregion

        #region IEditPart 成员

        bool isDirty;
        private bool IsDirty
        {
            get
            {
                if (isDirty)
                {
                    return true;
                }
                if (_journalInfo.IsDirty)
                {
                    return true;
                }
                foreach (JournalDetail item in Details)
                {
                    if (item.IsDirty)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public override object DataSource
        {
            get { return bsJournalEdit.DataSource; }
            set { BindingData(value); }
        }
        void BindingData(object data)
        {
            if (data == null)
            {
                _journalInfo = new JournalInfo();
                _journalInfo.ID = Guid.Empty;
                _journalInfo.IsValid = true;
                _journalInfo.CreateName = LocalData.UserInfo.LoginName;
                _journalInfo.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                _journalInfo.IsDirty = false;

                _journalInfo.DetailList = new List<JournalDetail>();

                bsDetails.DataSource = _journalInfo.DetailList;
            }
            else
            {
                _journalInfo = FinanceService.GetJournalInfo(((JournalList)data).ID, LocalData.IsEnglish);
            }

            bsJournalEdit.DataSource = _journalInfo;
            bsJournalEdit.ResetBindings(false);
            _journalInfo.CancelEdit();
            _journalInfo.BeginEdit();

            details = _journalInfo.DetailList;
            bsDetails.DataSource = details;
            bsDetails.ResetBindings(false);

        }

        public override bool SaveData()
        {
            return Save();
        }

        public override void EndEdit()
        {
            _journalInfo.EndEdit();

            foreach (JournalDetail item in Details)
            {
                item.EndEdit();
            }
        }

        public override event SavedHandler Saved;

        #endregion

        #region Save
        private bool Save()
        {
            try
            {
                gcMain.Focus();
                txtNo.Focus();

                //if (!this.ValidateData())
                //{
                //    return false;
                //}

                if (!IsDirty)
                {
                    return true;
                }

                JournalSaveRequest saveRequest = new JournalSaveRequest();
                saveRequest.CompanyID = _journalInfo.CompanyID;
                saveRequest.ID = _journalInfo.ID;
                saveRequest.No = _journalInfo.No;
                saveRequest.PostDate = _journalInfo.PostDate;
                saveRequest.Remark = _journalInfo.Remark;
                saveRequest.SaveByID = LocalData.UserInfo.LoginID;
                saveRequest.UpdateDate = _journalInfo.UpdateDate;
                saveRequest.IsEnglish = LocalData.IsEnglish;

                List<JournalDetailSaveRequest> list = new List<JournalDetailSaveRequest>();

                List<Decimal> CRAmountList = new List<decimal>();
                List<Decimal> DRAmountList = new List<decimal>();
                List<Guid> CurrencyIDList = new List<Guid>();
                List<Guid> GLIDList = new List<Guid>();
                List<Guid?> CustomerIDList = new List<Guid?>();
                List<Guid?> IDList = new List<Guid?>();
                List<String> RemarkList = new List<string>();
                List<Guid> saveBy = new List<Guid>();
                List<DateTime?> updatedatesList = new List<DateTime?>();
                JournalDetailSaveRequest accountRequest = new JournalDetailSaveRequest();

                List<JournalDetail> changeList = Details;//判定数据是否修改,明细无修改则不执行保存明细之过程
                List<JournalDetailSaveRequest> detaiList = new List<JournalDetailSaveRequest>();

                if (isDirty)
                {
                    foreach (JournalDetail details in changeList)
                    {
                        CRAmountList.Add(details.CRAmount);
                        CurrencyIDList.Add(details.CurrencyID);
                        DRAmountList.Add(details.DRAmount);
                        GLIDList.Add(details.GLID);
                        CustomerIDList.Add(details.CustomerID);
                        IDList.Add(details.ID);
                        RemarkList.Add(details.Remark);
                        updatedatesList.Add(details.UpdateDates);
                        list.Add(accountRequest);
                    }
                    accountRequest.CRAmounts = CRAmountList.ToArray();
                    accountRequest.DRAmounts = DRAmountList.ToArray();
                    accountRequest.CurrencyIDs = CurrencyIDList.ToArray();
                    accountRequest.GLIDs = GLIDList.ToArray();
                    accountRequest.Customers = CustomerIDList.ToArray();
                    accountRequest.Remarks = RemarkList.ToArray();
                    accountRequest.IDs = IDList.ToArray();
                    accountRequest.UpdateDates = updatedatesList.ToArray();
                    accountRequest.SaveByID = LocalData.UserInfo.LoginID;

                    changeList.ForEach(o => accountRequest.AddInvolvedObject(o));

                    detaiList.Add(accountRequest);
                }

                try
                {
                    Dictionary<Guid, SaveResponse> saves = FinanceService.SaveJournalWithTrans(saveRequest, detaiList);

                    SaveResponse.Analyze(detaiList.Cast<SaveRequest>().ToList(), saves, true);

                    #region 更新主表内容
                    SaveResponse.Analyze(new List<SaveRequest> { saveRequest }, saves, true);
                    SingleResult result = saveRequest.SingleResult;

                    _journalInfo.CompanyName = cmbCompanyID.Text;
                    _journalInfo.ID = result.GetValue<Guid>("ID");
                    _journalInfo.No = result.GetValue<String>("No");
                    _journalInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    _journalInfo.IsDirty = false;

                    bsJournalEdit.ResetBindings(false);

                    #endregion

                    #region 更新明细内容
                    foreach (JournalDetailSaveRequest detailInfo in list)
                    {
                        List<JournalDetail> changedDetail = detailInfo.UnBoxInvolvedObject<JournalDetail>();
                        ManyResult detailResult = detailInfo.ManyResult;

                        for (int i = 0; i < changedDetail.Count; i++)
                        {
                            changedDetail[i].ID = detailResult.Items[i].GetValue<Guid>("ID");
                            changedDetail[i].UpdateDates = detailResult.Items[i].GetValue<DateTime?>("UpdateDate");
                            changedDetail[i].IsDirty = false;
                        }
                    }
                    #endregion

                    if (Saved != null) Saved(_journalInfo, new object[] { result });

                    _journalInfo.CancelEdit();

                    isDirty = false;

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                    return true;

                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
                    return false;
                }
            }
            catch (Exception ee)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ee);
                return false;
            }
        }
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            gvMain.Focus();
            bsJournalEdit.EndEdit();
            bsDetails.EndEdit();

            if (Details == null || Details.Count == 0)
            {
                FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1110100001"));
                return false;
            }

            if (!CompareAmout())
            {
                FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1110100002"));
                return false;
            }

            if (!_journalInfo.Validate())
            {
                return false;
            }
            foreach (JournalDetail Journal in Details)
            {
                if (!Journal.Validate())
                {
                    return false;
                }
            }
            return true;


        }

        #endregion


        #region 工具栏按钮

        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().Close();
        }

        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                bsJournalEdit.EndEdit();
                bsDetails.EndEdit();
                try
                {
                    Save();
                }
                catch (Exception)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "Information is not completed" : "信息未填写完整");
                }
            }
        }

        private void barPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (_journalInfo == null) return;
                _journalInfo.EndEdit();
                if (_journalInfo.IsDirty)
                {
                    XtraMessageBox.Show(LocalData.IsEnglish ? "Current data has changed, want to print please save." : "当前数据有更改,欲打印请先保存.", LocalData.IsEnglish ? "Tip" : "提示");
                    return;
                }

                try
                {
                    JournalReportData journalReportData = new JournalReportData();
                    journalReportData.JournalBaseReportData = new JournalBaseReportData();
                    journalReportData.JournalBaseReportData.CompanyName = _journalInfo.CompanyName;
                    journalReportData.JournalBaseReportData.No = _journalInfo.No;
                    journalReportData.JournalBaseReportData.PostDate = _journalInfo.PostDate.ToString("MMM,dd.yyyy", DateTimeFormatInfo.InvariantInfo);
                    journalReportData.JournalBaseReportData.Remark = _journalInfo.Remark;

                    journalReportData.DetailList = new List<JournalDetailReportData>();
                    foreach (var detailItem in Details)
                    {
                        JournalDetailReportData detailReportItem = new JournalDetailReportData();
                        if (_dicSolutionGL.Keys.Contains(detailItem.GLID))
                        {
                            detailReportItem.GLDescription = _dicSolutionGL[detailItem.GLID];
                        }

                        if (_dicCurrency.Keys.Contains(detailItem.CurrencyID))
                        {
                            detailReportItem.Currency = _dicCurrency[detailItem.CurrencyID];
                        }

                        detailReportItem.CustomerName = detailItem.CustomerName;
                        detailReportItem.DRAmount = detailItem.DRAmount;
                        detailReportItem.CRAmount = detailItem.CRAmount;
                        detailReportItem.Remark = detailItem.Remark;
                        journalReportData.DetailList.Add(detailReportItem);
                    }
                    journalReportData.TotalFeeList = GetReportTotalFee();
                    IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Journal" : "打印日记帐", (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);
                    string fileName = Application.StartupPath + "\\Reports\\FAM\\";
                    fileName += "JournalReport.frx";
                    Dictionary<string, object> reportSource = new Dictionary<string, object>();
                    reportSource.Add("JournalBaseReportData", journalReportData.JournalBaseReportData);
                    reportSource.Add("JournalDetailReportData", journalReportData.DetailList);
                    reportSource.Add("TotalFeeReportData", journalReportData.TotalFeeList);

                    viewer.BindData(fileName, reportSource, null);
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
                }
            }
        }

        #endregion

        #region 明细工具栏事件

        #region 添加明细
        private void barNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                JournalDetail jour = new JournalDetail();
                jour.ID = Guid.Empty;
                jour.JournalID = _journalInfo.ID;
                jour.Undoable = true;
                jour.CRAmount = 0;
                jour.DRAmount = 0;
                bsDetails.Add(jour);
                bsDetails.ResetBindings(false);
                isDirty = true;
            }
        }
        #endregion
        #region 明细删除
        private void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (CurrentRow == null) return;
            bsDetails.RemoveCurrent();
            //RemoveFee();
            TotalCurrenCRAmount();
            TotalCurrenDRAmount();
            isDirty = true;
        }
        private void RemoveFee()
        {
            if (CurrentRow == null) return;

            if (FAMUtility.EnquireIsDeleteCurrentData())
            {
                JournalDetail currentData = CurrentRow;

                if (currentData.IsNew)
                {
                    bsDetails.RemoveCurrent();
                }
                else
                {
                    try
                    {
                        FinanceService.RemoveJournalDetailList(CurrentRow.ID, LocalData.UserInfo.LoginID, LocalData.IsEnglish);
                        bsDetails.RemoveCurrent();
                    }
                    catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
                }
            }
        }

        #endregion

        #endregion

        #region 明细数据发生改变时
        private void gvMain_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {

            if (e.Column == colCurrencyID)
            {
                TotalCurrenCRAmount();
                TotalCurrenDRAmount();
            }
            if (e.Column == colCRAmount)
            {
                TotalCurrenCRAmount();
            }
            if (e.Column == colDRAmount)
            {
                TotalCurrenDRAmount();
            }
            isDirty = true;
        }

        #endregion

        #region 统计币种与金额
        //应付
        private void TotalCurrenCRAmount()
        {
            Dictionary<Guid, Decimal> dicAmount = (from d in Details group d by d.CurrencyID into g select new { g.Key, TotalAcmount = g.Sum(p => p.CRAmount) }).ToDictionary(c => c.Key, c => c.TotalAcmount);
            txtCRAmt.Text = string.Empty;

            foreach (var item in dicAmount)
            {
                string currencyName = string.Empty;
                if (_dicCurrency.Keys.Contains(item.Key))
                {
                    currencyName = _dicCurrency[item.Key];
                }
                else
                {
                    currencyName = LocalData.IsEnglish ? "Unknown" : "未知";
                }
                txtCRAmt.Text += currencyName + ":" + item.Value.ToString("n") + " ";

            }

        }

        /// <summary>
        /// 应收
        /// </summary>
        private void TotalCurrenDRAmount()
        {
            Dictionary<Guid, Decimal> dicAmount = (from d in Details group d by d.CurrencyID into g select new { g.Key, TotalAcmount = g.Sum(p => p.DRAmount) }).ToDictionary(c => c.Key, c => c.TotalAcmount);
            txtDRAmt.Text = string.Empty;

            foreach (var item in dicAmount)
            {
                string currencyName = string.Empty;
                if (_dicCurrency.Keys.Contains(item.Key))
                {
                    currencyName = _dicCurrency[item.Key];
                }
                else
                {
                    currencyName = LocalData.IsEnglish ? "Unknown" : "未知";
                }
                txtDRAmt.Text += currencyName + ":" + item.Value.ToString("n") + " ";

            }
        }
        #region 判断应收、应付是否相等
        private bool CompareAmout()
        {
            Decimal DR = 0;//应收
            Decimal CR = 0;//应付
            Dictionary<Guid, Decimal> dicAmount = (from d in Details group d by d.CurrencyID into g select new { g.Key, TotalAcmount = g.Sum(p => p.DRAmount) }).ToDictionary(c => c.Key, c => c.TotalAcmount);

            foreach (var item in dicAmount)
            {
                DR = Convert.ToDecimal(item.Value.ToString("n"));
            }
            Dictionary<Guid, Decimal> dicAmount1 = (from d in Details group d by d.CurrencyID into g select new { g.Key, TotalAcmount = g.Sum(p => p.CRAmount) }).ToDictionary(c => c.Key, c => c.TotalAcmount);

            foreach (var item in dicAmount1)
            {
                CR = Convert.ToDecimal(item.Value.ToString("n"));
            }
            if (DR != CR)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "The amount due and payable amount of inconsistency" : "应收金额与应付金额不一致");
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
        /// <summary>
        /// 总应收/应付(报表用)
        /// </summary>
        private List<TotalFee> GetReportTotalFee()
        {
            List<TotalFee> totalFeeList = new List<TotalFee>();

            IEnumerable<IGrouping<Guid, JournalDetail>> arr = Details.GroupBy(i => i.CurrencyID);
            foreach (IGrouping<Guid, JournalDetail> item in arr)
            {
                List<JournalDetail> listByGroup = item.ToList<JournalDetail>();
                TotalFee totalFeeItem = new TotalFee();
                totalFeeItem.TotalDRAmount = listByGroup.Sum(p => p.DRAmount).ToString();
                totalFeeItem.TotalCRAmount = listByGroup.Sum(p => p.CRAmount).ToString();

                //foreach (var cur in _currency)
                //{
                //    if (cur.CurrencyID == item.Key)
                //    {
                //        totalFeeItem.Currency = cur.CurrencyName;
                //        break;
                //    }
                //}

                if (_dicCurrency.Keys.Contains(item.Key))
                {
                    totalFeeItem.Currency = _dicCurrency[item.Key];
                }

                totalFeeList.Add(totalFeeItem);
            }

            return totalFeeList;
        }

        #endregion

        private void barUnLock_ItemClick(object sender, ItemClickEventArgs e)
        {
            ConfigureInfo configure = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID, LocalData.IsEnglish);
            if (IsUntieLock(configure, _journalInfo.No, _journalInfo.PostDate))
            {
                //解锁服务
                FinanceService.UntieLockLedgerForJournal(_journalInfo.ID, LocalData.UserInfo.LoginID);

                //改变控件状态
                barSave.Enabled = true;
                barDelete.Enabled = true;
                barAdd.Enabled = true;
                barSave.Caption = "保存(&S)";
            }
        }
        private bool IsUntieLock(ConfigureInfo companyConfig, string no, DateTime? bankDate)
        {
            string message = string.Empty;
            if (bankDate == null)
            {
                if (LocalData.IsEnglish)
                {
                    message = string.Format("Journal[{0}] not create Date", no);
                }
                else
                {
                    message = string.Format("日记账[{0}]没有生成日期", no);
                }
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), message);
                return false;
            }

            //计费还没有关帐的，不需要解锁
            //if (DataTypeHelper.GetDateTime(bankDate).Date > DataTypeHelper.GetDateTime(companyConfig.AccountingClosingdate).Date)
            //{
            //    if (((DateTime)bankDate).ToString("yyyyMMdd") != ((DateTime)companyConfig.AccountingClosingdate).ToString("yyyyMMdd"))
            //    {
            //        if (LocalData.IsEnglish)
            //        {
            //            message = string.Format("LedgerNo[{0}] Create Date [{1}] is not Accounting Closing [{2}] period, no need to unlock", no, bankDate.Value.ToShortDateString(), companyConfig.ChargingClosingdate.Value.ToShortDateString());
            //        }
            //        else
            //        {
            //            message = string.Format("日记账[{0}]的生成日期[{1}]在不在会计关帐[{2}]期内,不需要解锁", no, bankDate.Value.ToShortDateString(), companyConfig.ChargingClosingdate.Value.ToShortDateString());
            //        }
            //        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), message);
            //        return false;
            //    }
            //}
            return true;
        }
    }

}
