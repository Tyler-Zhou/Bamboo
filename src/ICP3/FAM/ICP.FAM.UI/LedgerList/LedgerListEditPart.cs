using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Helper;
using ICP.ReportCenter.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface;
using ICP.Sys.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Common.UI;
using ICP.Common.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects.SaveRequests;
using ICP.Framework.CommonLibrary.Common;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Service;
using ICP.Common.ServiceInterface.Client;
using ICP.Framework.CommonLibrary;
using System.Drawing;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Server;
using ICP.FAM.UI.LedgerList;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents;

namespace ICP.FAM.UI
{
    [ToolboxItem(false)]
    public partial class LedgerListEditPart : BaseEditPart
    {
        #region 变量、属性
        /// <summary>
        /// 用于计算列
        /// </summary>
        decimal amount = 0;
        /// <summary>
        /// 是否绑定员工
        /// </summary>
        bool isBindStaff = true;
        /// <summary>
        /// 明细是否绑定完成
        /// </summary>
        bool isDetailBindComplete = true;
        /// <summary>
        /// 是否复制凭证
        /// </summary>
        bool isCopy = false;
        /// <summary>
        /// 备注
        /// </summary>
        string remark = string.Empty;
        /// <summary>
        /// 默认操作口岸
        /// </summary>
        private Guid DefaultCompanyID;
        /// <summary>
        /// 本位币
        /// </summary>
        private Guid standardCurrencyID;
        /// <summary>
        /// 查找器
        /// </summary>
        private IDisposable glNameFinder, customerFinder, organizationFinder;
        /// <summary>
        /// 主表信息
        /// </summary>
        LedgerMasters master;   //主表
        /// <summary>
        /// 操作口岸 
        /// </summary>
        List<OrganizationList> companys;
        /// <summary>
        /// 当前公司下的汇率信息
        /// </summary>
        List<SolutionExchangeRateList> _rateList = new List<SolutionExchangeRateList>();
        /// <summary>
        /// 是否修改
        /// </summary>
        bool isDirty;
        /// <summary>
        /// 是否修改
        /// </summary>
        public bool IsDirty
        {
            get
            {
                if (isDirty)
                    return true;
                if (master.IsDirty)
                    return true;
                foreach (Ledgers item in DataList)
                {
                    if (item.IsDirty)
                        return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 是否有删除过数据
        /// </summary>
        public bool IsDeleteData { get; set; }
        /// <summary>
        /// 数据源
        /// </summary>
        public override object DataSource
        {
            get { return bsMaster.DataSource; }
            set { BindingData(value); }
        }
        /// <summary>
        /// 当前用户默认公司的本位币
        /// </summary>
        public Guid StandardCurrencyID
        {
            get
            {
                if (FAMUtility.GuidIsNullOrEmpty(standardCurrencyID))
                {
                    DefaultCompanyID = cmbCompany.EditValue == null ? LocalData.UserInfo.DefaultCompanyID : (Guid)cmbCompany.EditValue;
                    ConfigureInfo configure = ConfigureService.GetCompanyConfigureInfo(DefaultCompanyID, LocalData.IsEnglish);
                    if (configure != null)
                    {
                        standardCurrencyID = configure.StandardCurrencyID;
                    }
                }
                return standardCurrencyID;
            }
        }
        /// <summary>
        /// 公司
        /// </summary>
        public Guid CompanyID
        {
            get
            {
                if (master == null || FAMUtility.GuidIsNullOrEmpty(master.CompanyID))
                {
                    return LocalData.UserInfo.DefaultCompanyID;
                }

                return master.CompanyID;
            }
        }
        /// <summary>
        /// 主表上的日期
        /// </summary>
        public DateTime? MasterDate
        {
            get;
            set;
        }
        /// <summary>
        /// 明细当前行
        /// </summary>
        private Ledgers CurrentRow
        {
            get
            {
                return bsDetails.Current as Ledgers;
            }
        }
        /// <summary>
        /// 明细列表数据
        /// </summary>
        public List<Ledgers> DataList
        {
            get
            {
                bsDetails.EndEdit();
                List<Ledgers> list = bsDetails.DataSource as List<Ledgers>;
                if (list == null)
                    list = new List<Ledgers>();

                return list;
            }
        }
        /// <summary>
        /// 汇率列表
        /// </summary>
        public List<SolutionExchangeRateList> RateList
        {
            get
            {
                if (_rateList.Count == 0)
                {
                    DefaultCompanyID = cmbCompany.EditValue == null ? LocalData.UserInfo.DefaultCompanyID : (Guid)cmbCompany.EditValue;
                    _rateList = ConfigureService.GetCompanyExchangeRateList(DefaultCompanyID, true);
                }
                return _rateList;
            }
        }
        /// <summary>
        /// 保存事件
        /// </summary>
        public override event SavedHandler Saved;
        #endregion

        #region 服务

        [ServiceDependency]
        public WorkItem WorkItem { get; set; }
        /// <summary>
        /// 报表服务
        /// </summary>
        IReportCenterService ReportCenterService
        {
            get
            {
                return ServiceClient.GetService<IReportCenterService>();
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
        /// 打印服务
        /// </summary>
        IReportViewService ReportViewService
        {
            get
            {
                return ServiceClient.GetClientService<IReportViewService>();
            }
        }
        /// <summary>
        /// 基础数据查询服务
        /// </summary>
        IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        /// <summary>
        /// 用户服务
        /// </summary>
        IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }
        /// <summary>
        /// 组织架构服务
        /// </summary>
        IOrganizationService OrganizationService
        {
            get
            {
                return ServiceClient.GetService<IOrganizationService>();
            }
        }
        /// <summary>
        /// UI通用控件服务
        /// </summary>
        ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
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
        /// <summary>
        /// 汇率服务
        /// </summary>
        RateHelper RateHelperService
        {
            get
            {
                return ClientHelper.Get<RateHelper, RateHelper>();
            }
        }
        ///// <summary>
        ///// 用户服务
        ///// </summary>
        //IUserService sUserService
        //{
        //    get
        //    {
        //        return ServiceClient.GetService<IUserService>();
        //    }
        //}
        #endregion

        #region 构造函数
        public LedgerListEditPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                if (customerFinder != null)
                {
                    customerFinder.Dispose();
                    customerFinder = null;
                }
                if (glNameFinder != null)
                {
                    glNameFinder.Dispose();
                    glNameFinder = null;
                }
                if (organizationFinder != null)
                {
                    organizationFinder.Dispose();
                    organizationFinder = null;
                }
                SmartPartClosing -= LedgerListEditPart_SmartPartClosing;
                gcMain.DataSource = null;
                bsDetails.PositionChanged -= bsDetails_PositionChanged;
                bsDetails.DataSource = null;
                bsDetails.Dispose();
                Saved = null;
                mscStaff.EditValueChanged -= mscStaff_EditValueChanged;
                mscStaff.Enter -= mscStaff_Enter;

                if (WorkItem != null)
                {
                    WorkItem.Items.Remove(this);
                    WorkItem = null;
                }

            };
        }
        #endregion

        #region 事件
        /// <summary>
        /// 
        /// </summary>
        private void LedgerListEditPart_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                InitMessage();
                InitControls();
                SmartPartClosing += LedgerListEditPart_SmartPartClosing;
                ActivateSmartPartClosingEvent(WorkItem);
                master.IsDirty = false;
                barUnLock.Enabled = CheckEnblie();
            }
        }
        /// <summary>
        /// 用户发生改变时
        /// </summary>
        private void mscStaff_EditValueChanged(object sender, EventArgs e)
        {
            if (mscStaff.EditValue != null
                && DataTypeHelper.IsGuid(mscStaff.EditValue)
                && DataTypeHelper.GetGuid(mscStaff.EditValue, Guid.Empty) != Guid.Empty)
            {
                UserDetailInfo userInfo = UserService.GetUserDetailInfo(DataTypeHelper.GetGuid(mscStaff.EditValue));

                if (userInfo != null)
                {

                    treeDepartment.EditValue = userInfo.DepartmentID;
                    treeDepartment.Text = userInfo.DepartmentName;

                    if (CurrentRow != null)
                    {
                        CurrentRow.DepID = userInfo.DepartmentID;
                        CurrentRow.Dept = treeDepartment.Text;
                    }
                }
            }
            treeDepartment_Validated(sender, e);
        }
        /// <summary>
        /// 绑定员工
        /// </summary>
        private void mscStaff_Enter(object sender, EventArgs e)
        {
            BindStaff();
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void barSave_Click(object sender, ItemClickEventArgs e)
        {
            Save();
        }
        /// <summary>
        /// 关闭
        /// </summary>
        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().Close();
        }
        /// <summary>
        /// 打印
        /// </summary>
        private void barPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (isDirty && barSave.Enabled)
            {
                XtraMessageBox.Show(LocalData.IsEnglish ? "Current data has changed, want to print please save." : "当前数据有更改,欲打印请先保存.", LocalData.IsEnglish ? "Tip" : "提示");
                return;
            }
            if (master == null || FAMUtility.GuidIsNullOrEmpty(master.ID))
            {
                return;
            }

            if (master.Type == LedgerMasterType.Commision)  //报销
            {
                List<Guid> ids = new List<Guid>();
                ids.Add(master.ID);
                //凭证数据源
                List<PrintLedgerMasterReports> hdList = FinanceService.GetBulkPrintLedgerReportDate(ids.ToArray(), LocalData.IsEnglish);
                if (hdList == null && hdList.Count == 0)
                    return;

                LedgerPrint.Print(WorkItem, ReportViewService, hdList);
            }
            else
            {
                LedgerPrint.Print(WorkItem, FinanceService, ReportViewService, master.ID);
            }

        }
        /// <summary>
        /// 新增
        /// </summary>
        private void barAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddData();
        }
        /// <summary>
        /// 删除
        /// </summary>
        private void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (CurrentRow == null || gvDetails.FocusedRowHandle < 0) return;

            int[] selectRowsHandle = gvDetails.GetSelectedRows();

            if (!FAMUtility.ShowResultMessage(NativeLanguageService.GetText(this, "1308080001")))
            {
                return;
            }

            List<Guid> idList = new List<Guid>();
            List<DateTime?> updateDateList = new List<DateTime?>();

            foreach (var row in selectRowsHandle)
            {
                Ledgers item = gvDetails.GetRow(row) as Ledgers;
                if (!FAMUtility.GuidIsNullOrEmpty(item.Id))
                {
                    idList.Add(item.Id);
                    updateDateList.Add(item.UpdateDate);
                }
            }
            if (!idList.Contains(CurrentRow.Id) && !FAMUtility.GuidIsNullOrEmpty(CurrentRow.Id))
            {
                idList.Add(CurrentRow.Id);
                updateDateList.Add(CurrentRow.UpdateDate);
            }
            try
            {
                if (idList.Count > 0)
                {
                    FinanceService.RemoveLedgerInfo(idList.ToArray(), updateDateList.ToArray(), LocalData.UserInfo.LoginID, LocalData.IsEnglish);
                }
                if (selectRowsHandle.Length == 0 && CurrentRow != null)
                {
                    bsDetails.RemoveCurrent();
                }
                else
                {
                    gvDetails.DeleteSelectedRows();
                }
                BindDetailInfo();
                isDirty = true;
                IsDeleteData = true;

                Total();

                if (DataList.Count > 0)
                {
                    gvDetails.SelectCell(0, colRemark);
                }

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Remove Success" : "删除成功!");
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }

        }
        /// <summary>
        /// 解锁
        /// </summary>
        private void barUnLock_ItemClick(object sender, ItemClickEventArgs e)
        {
            ConfigureInfo configure = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID, LocalData.IsEnglish);
            if (IsUntieLock(configure, master.No, master.DATE))
            {
                //解锁服务
                FinanceService.UntieLockLedger(master.ID, LocalData.UserInfo.LoginID);

                //改变控件状态
                barSave.Enabled = true;
                btnSaveAndNew.Enabled = true;
                barDelete.Enabled = true;
                barAdd.Enabled = true;
                barSave.Caption = "保存(&S)";
            }
        }
        /// <summary>
        /// 首张
        /// </summary>
        private void barHome_ItemClick(object sender, ItemClickEventArgs e)
        {
            SearchVouch(VoucherSearchType.Home);
        }
        /// <summary>
        /// 上张
        /// </summary>
        private void barUp_ItemClick(object sender, ItemClickEventArgs e)
        {
            SearchVouch(VoucherSearchType.Up);
        }
        /// <summary>
        /// 下张
        /// </summary>
        private void barNext_ItemClick(object sender, ItemClickEventArgs e)
        {
            SearchVouch(VoucherSearchType.Next);
        }
        /// <summary>
        /// 末张
        /// </summary>
        private void barEnd_ItemClick(object sender, ItemClickEventArgs e)
        {
            SearchVouch(VoucherSearchType.End);
        }
        /// <summary>
        /// 新增凭证
        /// </summary>
        private void barNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            WorkItem.Commands[LedgerListCommandConstants.Command_Add].Execute();
        }
        /// <summary>
        /// 保存并新增
        /// </summary>
        private void btnSaveAndNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!Save())
            {
                return;
            }

            DataSource = null;
            isDirty = false;
        }
        /// <summary>
        /// 查询余额
        /// </summary>
        private void barQueryBalance_ItemClick(object sender, ItemClickEventArgs e)
        {
            //厦门公司陈奕强提出，可以在做凭证时，查询这个科目的余额(如果有辅助核算，则需要带上辅助核算)
            if (CurrentRow == null || CurrentRow.GLId == null)
            {
                MessageBox.Show(LocalData.IsEnglish ? "Please Choose subject to query" : "请选择要查询的科目！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbCompany.EditValue == null)
            {
                MessageBox.Show(LocalData.IsEnglish ? "Please choose the company to query the balance" : "请选择要查询余额的公司！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Guid? CusId, Depid, Userid;
            if (txtCustomer.Tag != null && txtCustomer.Tag != DBNull.Value)
            {
                CusId = (Guid)txtCustomer.Tag;
            }
            else
                CusId = null;

            if (treeDepartment.EditValue != new Guid())
            {
                Depid = (Guid)treeDepartment.EditValue;
            }
            else
                Depid = null;

            if (mscStaff.EditValue != null && (Guid)mscStaff.EditValue != new Guid())
            {
                Userid = (Guid)mscStaff.EditValue;
            }
            else
                Userid = null;

            string fuZhuXiang = txtCustomer.Text + (Depid == null ? "" : ("/" + treeDepartment.Text)) + (Userid == null ? "" : ("/" + mscStaff.EditText));

            List<GLBlance> mylist = FinanceService.GetBalance((Guid)cmbCompany.EditValue, CurrentRow.GLId, CusId, Depid, Userid, deDate.DateTime);

            if (mylist.Count > 0)
            {
                ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(CompanyID);
                List<SolutionCurrencyList> currency = ConfigureService.GetSolutionCurrencyList(configureInfo.SolutionID, true);
                if (mylist.Count == 5)
                {
                    GLBlance newobj = new GLBlance();
                    newobj.Item = "期末余额";
                    if (mylist[0].Direction == "借" || mylist[0].Direction == "平")
                    {
                        newobj.Amount = mylist[0].Amount + mylist[1].Amount - mylist[2].Amount;
                        if (newobj.Amount > 0)
                        {
                            newobj.Direction = "借";
                        }
                        else if (newobj.Amount < 0)
                        {
                            newobj.Direction = "贷";
                            newobj.Amount = -newobj.Amount;
                        }
                        else
                        {
                            newobj.Direction = "平";
                        }
                    }
                    else
                    {
                        newobj.Amount = mylist[0].Amount + mylist[2].Amount - mylist[1].Amount;
                        if (newobj.Amount > 0)
                        {
                            newobj.Direction = "贷";
                        }
                        else if (newobj.Amount < 0)
                        {
                            newobj.Direction = "借";
                            newobj.Amount = -newobj.Amount;
                        }
                        else
                        {
                            newobj.Direction = "平";
                        }
                    }

                    newobj.FCurrency = newobj.Amount;
                    newobj.Count = 1;
                    mylist.Add(newobj);
                }

                LedgerShowBalance showBalance = new LedgerShowBalance();
                showBalance.myGLid = CurrentRow.GLId;
                showBalance.myGL = gvDetails.GetRowCellDisplayText(gvDetails.FocusedRowHandle, "GLFullName");
                showBalance.myCompanyID = CurrentRow.CompanyId;
                showBalance.myCompany = fuZhuXiang;
                showBalance.myRate = CurrentRow.Rate;
                showBalance.myDataList = mylist;
                showBalance.endDate = deDate.DateTime;
                if (currency.Find(r => r.CurrencyID == CurrentRow.ForeignCurrencyID) != null)
                {
                    showBalance.myCurrency = currency.Find(r => r.CurrencyID == CurrentRow.ForeignCurrencyID).CurrencyName;
                }

                string title = LocalData.IsEnglish ? "Show the balance" : "显示余额";
                PartLoader.ShowDialog(showBalance, title);
            }
            else
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "No relevant subjects of exchange of information!" : "没有相关科目的往来信息！");
            }
        }
        /// <summary>
        /// 输入回车
        /// </summary>
        private void gvDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (gvDetails.FocusedColumn == colCRAmt)
                {
                    if (gvDetails.FocusedRowHandle == DataList.Count - 1)
                    {
                        AddData();
                    }
                    else
                    {
                        SendKeys.SendWait("{TAB}");
                    }
                }
                else if (gvDetails.FocusedColumn == colRemark
                      || gvDetails.FocusedColumn == colOrgAmt
                      || gvDetails.FocusedColumn == colRate
                      || gvDetails.FocusedColumn == colDRAmt)
                {
                    SendKeys.SendWait("{TAB}");
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void gvDetails_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            if ((e.Column == colCRAmt
               || e.Column == colDRAmt
               || e.Column == colRate)
               && CurrentRow != null
               && e.Value != null)
            {
                if (e.Value.ToString().Contains("="))
                {
                    #region 输入的字符中包含了=号时
                    decimal totalDRAmt = 0.0m, totalCRAmt = 0.0m;
                    for (int i = 0; i < gvDetails.RowCount; i++)
                    {
                        //不包含当前行的数据
                        if (i != e.RowHandle)
                        {
                            Ledgers item = gvDetails.GetRow(i) as Ledgers;
                            if (item != null)
                            {
                                totalDRAmt += item.DRAmt;
                                totalCRAmt += item.CRAmt;
                            }
                        }
                    }

                    if (e.Column == colDRAmt)
                    {
                        //借方
                        CurrentRow.DRAmt = totalCRAmt - totalDRAmt;
                        gvDetails.SetFocusedValue(CurrentRow.DRAmt);
                    }
                    else if (e.Column == colCRAmt)
                    {
                        //贷方
                        CurrentRow.CRAmt = totalDRAmt - totalCRAmt;
                        gvDetails.SetFocusedValue(CurrentRow.CRAmt);
                    }
                    else if (e.Column == colRate)
                    {
                        //原币
                        if (CurrentRow.Rate == 0)
                        {
                            CurrentRow.Rate = 1;
                        }
                        if (CurrentRow.DRAmt > 0)
                        {
                            CurrentRow.Rate = CurrentRow.DRAmt / CurrentRow.Rate;
                        }
                        else if (CurrentRow.CRAmt > 0)
                        {
                            CurrentRow.Rate = CurrentRow.CRAmt / CurrentRow.Rate;
                        }
                    }
                    bsDetails.ResetCurrentItem();
                    Total();
                    #endregion
                }
                if (e.Value.ToString().Contains(" "))
                {
                    #region 输入的字符是空格时
                    if (e.Column == colDRAmt || e.Column == colCRAmt)
                    {
                        decimal dramount = CurrentRow.DRAmt, cramtount = CurrentRow.CRAmt;

                        CurrentRow.DRAmt = cramtount;
                        CurrentRow.CRAmt = dramount;

                        gvDetails.SetRowCellValue(e.RowHandle, colCRAmt, CurrentRow.CRAmt);
                        gvDetails.SetRowCellValue(e.RowHandle, colDRAmt, CurrentRow.DRAmt);
                    }

                    bsDetails.ResetBindings(false);
                    Total();

                    #endregion
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void gvDetails_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column == colOrgAmt || e.Column == colRate)
            {
                //2015-3-13 周任平修改：输入金额时，当差异与原来的金额大于0.1时，才去更新借贷方的金额

                //贷方大于0时，输入原币、汇率直接贷方,否则进去借方，用户可以在贷方中按下空格，将金额放到贷方中去
                if (CurrentRow.CRAmt > 0)
                {
                    amount = FAMUtility.Get2Round(CurrentRow.OrgAmt * CurrentRow.Rate);
                    if (Math.Abs(amount - CurrentRow.CRAmt) > 0.1m)
                    {
                        CurrentRow.CRAmt = amount;
                    }
                }
                else
                {
                    amount = FAMUtility.Get2Round(CurrentRow.OrgAmt * CurrentRow.Rate);
                    if (Math.Abs(amount - CurrentRow.DRAmt) > 0.1m)
                    {
                        CurrentRow.DRAmt = amount;
                    }
                }
                bsDetails.ResetBindings(false);
                Total();
            }
            else if (e.Column == colCRAmt)
            {
                if (CurrentRow.CRAmt > 0)
                {
                    if (CurrentRow.Rate == 0)
                    {
                        CurrentRow.Rate = 1;
                    }
                    amount = FAMUtility.Get3Round(CurrentRow.CRAmt / CurrentRow.Rate);
                    if (Math.Abs(amount - CurrentRow.OrgAmt) > 0.1m)
                    {
                        CurrentRow.OrgAmt = amount;
                    }
                }
                Total();
            }
            else if (e.Column == colDRAmt)
            {
                if (CurrentRow.DRAmt > 0)
                {
                    if (CurrentRow.Rate == 0)
                    {
                        CurrentRow.Rate = 1;
                    }
                    amount = FAMUtility.Get3Round(CurrentRow.DRAmt / CurrentRow.Rate);
                    if (Math.Abs(amount - CurrentRow.OrgAmt) > 0.1m)
                    {
                        CurrentRow.OrgAmt = amount;
                    }

                }
                Total();
            }

            if (e.Column == colRemark)
            {
                remark = e.Value.ToString();
            }

            isDirty = true;
        }
        /// <summary>
        /// 选择的明细发生改变时
        /// </summary>
        private void bsDetails_PositionChanged(object sender, EventArgs e)
        {
            BindDetailInfo();
        }
        /// <summary>
        /// 复制信息
        /// </summary>
        private void barCopyLedger_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                PartLoader.ShowEditPart<LedgerListEditPart>(WorkItem, bsDetails.DataSource as List<Ledgers>, LocalData.IsEnglish ? "Add Voucher" : "新增凭证", null);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void Bt_ClearDep_Click(object sender, EventArgs e)
        {
            if (CurrentRow != null)
            {
                CurrentRow.DepID = null;
                CurrentRow.Dept = null;
                treeDepartment.EditValue = new Guid();
                treeDepartment.Text = "";
            }
        }
        private void treeDepartment_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(treeDepartment.Text) && treeDepartment.EditValue != null)
            {
                OrganizationList list = companys.Find(r => r.ID == treeDepartment.EditValue);
                if (list != null)
                {
                    OrganizationList plist = companys.Find(r => r.ID == list.ParentID);
                    if (plist != null)
                    {
                        treeDepartment.Text = plist.CShortName + "-" + list.CShortName;
                    }
                }
            }
        }
        /// <summary>
        /// Master时间更改同时更改明细时间
        /// </summary>
        private void DeDate_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                List<Ledgers> ledgers = bsDetails.DataSource as List<Ledgers>;
                foreach (var item in ledgers)
                {
                    item.Date = deDate.DateTime;
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        /// <summary>
        /// 窗体关闭
        /// </summary>
        void LedgerListEditPart_SmartPartClosing(object sender, WorkspaceCancelEventArgs e)
        {
            if (barSave.Enabled)
                e.Cancel = !Leaving();
        }

        #endregion

        #region 方法
        /// <summary>
        /// 初始化提示信息
        /// </summary>
        private void InitMessage()
        {
            RegisterMessage("1308080001", LocalData.IsEnglish ? "Are you sure to deleted the selected Data " : "确认删除选中的数据?");
            RegisterMessage("1411068888888", LocalData.IsEnglish ? "This Ledger no details, whether to delete" : "该凭证没有明细，是否删除?");
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            //显示行号
            FAMUtility.ShowGridRowNo(gvDetails);

            //公司
            FAMUtility.SetEnterToExecuteOnec(cmbCompany, delegate
            {
                ICPCommUIHelper.BindCompanyByUser(cmbCompany, false);
            });

            rlueCompany.ValueMember = "ID";
            rlueCompany.DisplayMember = LocalData.IsEnglish ? "EShortName" : "CShortName";
            rlueCompany.DataSource = LocalData.UserInfo.UserOrganizationList.FindAll(r => r.Type == LocalOrganizationType.Company);
            rlueCompany.Columns[1].FieldName = LocalData.IsEnglish ? "EShortName" : "CShortName";
            rlueCompany.Columns[1].Width = 120;

            //类型
            List<EnumHelper.ListItem<LedgerMasterType>> masterType = EnumHelper.GetEnumValues<LedgerMasterType>(LocalData.IsEnglish);
            cmbType.Properties.BeginUpdate();
            foreach (var item in masterType)
            {
                if (item.Value != LedgerMasterType.Unknown && item.Value != LedgerMasterType.CarryForward)
                {
                    cmbType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
                }
            }
            cmbType.Properties.EndUpdate();
            #region 会计科目搜索器
            FAMUtility.SetEnterToExecuteOnec(gcMain,
                delegate
                {
                    glNameFinder = DataFindClientService.RegisterGridColumnFinder(colGLName
                   , FAMFinderConstants.GLCodeFinder
                   , new string[] { "GLID", "GLFullName", "ForeignCurrencyID", "GLCodeProperty", "IsCustomerCheck", "IsDepartmentCheck", "IsPersonalCheck" }
                   , new string[] { "ID", "GLCodeName", "ForeignCurrencyID", "GLCodeProperty", "IsCustomerCheck", "IsDepartmentCheck", "IsPersonalCheck" }
                   , GetSolutionIDSearchCondition,
                    delegate (object inputSource, object resultData)
                    {
                        Ledgers returnItem = resultData as Ledgers;

                        if (returnItem != null && returnItem.ForeignCurrencyID != null && returnItem.ForeignCurrencyID != StandardCurrencyID)
                        {
                            DateTime date = DateTime.Now;
                            if (master != null && master.DATE != null)
                            {
                                date = master.DATE.Value;
                            }
                            CurrentRow.Rate = RateHelperService.GetRate(returnItem.ForeignCurrencyID.Value, StandardCurrencyID, DateTime.SpecifyKind(date, DateTimeKind.Unspecified), RateList);
                            bsDetails.ResetBindings(false);
                        }else
                        {
                            CurrentRow.Rate = 1;
                            bsDetails.ResetBindings(false);
                        }
                        CurrentRow.ForeignCurrencyID = returnItem.ForeignCurrencyID;
                        CurrentRow.GLCodeProperty = returnItem.GLCodeProperty;
                        txtCustomer.BackColor = returnItem.IsCustomerCheck ? Color.LightYellow : Color.White;
                        treeDepartment.BackColor = returnItem.IsDepartmentCheck ? Color.LightYellow : Color.White;
                        mscStaff.BackColor = returnItem.IsPersonalCheck ? Color.LightYellow : Color.White;
                    });
                });

            #endregion

            #region 客户
            FAMUtility.SetEnterToExecuteOnec(txtCustomer,
                delegate
                {
                    customerFinder = DataFindClientService.Register(
                             txtCustomer,
                             CommonFinderConstants.CustoemrFinder,
                             Common.UI.SearchFieldConstants.CodeName,
                             Common.UI.SearchFieldConstants.ResultValue,
                             GetCustomerStateCondition,
                     delegate (object inputSource, object[] resultData)
                     {
                         if (CurrentRow != null)
                         {
                             txtCustomer.Tag = CurrentRow.CustomerId = new Guid(resultData[0].ToString());
                             string customerCode = DataTypeHelper.GetString(resultData[1]);
                             string customerName = LocalData.IsEnglish ? DataTypeHelper.GetString(resultData[2]) : DataTypeHelper.GetString(resultData[3]);
                             txtCustomer.EditValue = CurrentRow.Customer = "(" + customerCode + ")" + customerName;
                             CurrentRow.IsDirty = true;
                         }
                     },
                     delegate
                     {
                         txtCustomer.Tag = CurrentRow.CustomerId = null;
                         txtCustomer.EditValue = CurrentRow.Customer = string.Empty;
                     },
                     ClientConstants.MainWorkspace);
                });
            #endregion

            #region 部门
            companys = OrganizationService.GetOrganizationList("", "", true, 1000);
            List<OrganizationList> com = ReportCenterService.GetOrganizationListForCRMReport(LocalData.UserInfo.LoginID, LocalData.IsEnglish);
            treeDepartment.SetSource<OrganizationList>(com, LocalData.IsEnglish ? "EShortName" : "CShortName");
            #endregion

            //控件初始化
            cmbCompany.ShowSelectedValue(master.CompanyID, master.CompanyName);

            // SetButtomEnabled();

        }
        /// <summary>
        /// 设置分业务按钮
        /// </summary>
        private void SetPageButton()
        {
            if (master == null)
            {
                return;
            }
            barHome.Enabled = true;
            barUp.Enabled = true;
            barNext.Enabled = true;
            barEnd.Enabled = true;

            //第一张
            if (master.No.EndsWith("0001"))
            {
                barHome.Enabled = false;
                barUp.Enabled = false;
            }
            //最后一张
            if (master.No == master.MaxNo || string.IsNullOrEmpty(master.No))
            {
                barNext.Enabled = false;
                barEnd.Enabled = false;
            }
        }
        /// <summary>
        /// 设置工作栏按钮的可用性
        /// </summary>
        private void SetButtomEnabled()
        {
            #region 已关帐
            //凭证的日期如果小于或等于这个公司的财务关帐日的话，不能再修改凭证。
            barSave.Enabled = false;
            btnSaveAndNew.Enabled = false;
            barDelete.Enabled = false;
            barAdd.Enabled = false;
            if (master.IsValid == true && master.Status == LedgerMasterStatus.CreateBy)
            {

                ConfigureInfo configure = ConfigureService.GetCompanyConfigureInfo(master.CompanyID, LocalData.IsEnglish);
                if (configure != null)
                {
                    if (master != null)
                    {
                        if (master.DATE.Value.Date > configure.AccountingClosingdate.Value.Date)
                        {
                            barSave.Enabled = true;
                            btnSaveAndNew.Enabled = true;
                            barDelete.Enabled = true;
                            barAdd.Enabled = true;
                            barSave.Caption = "保存(&S)";
                        }
                        else
                            barSave.Caption = (LocalData.IsEnglish ? "Accounting Closing" : "财务已关帐");
                    }
                }
            }
            //只有管理成本才允许更改
            cmbType.Properties.ReadOnly = true;
            if (master.Type != LedgerMasterType.Commision)
            {
                barSave.Enabled = false;
                btnSaveAndNew.Enabled = false;
                barAdd.Enabled = false;
                barDelete.Enabled = false;
            }
            #endregion
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="data">传入数据</param>
        void BindingData(object data)
        {
            if (data == null)
            {
                master = new LedgerMasters();
                master.ID = Guid.NewGuid();
                master.IsValid = true;
                master.Status = LedgerMasterStatus.CreateBy;
                master.Type = LedgerMasterType.Commision;
                master.No = string.Empty;
                if (MasterDate == null)
                {
                    master.DATE = DateTime.Now;
                }
                else
                {
                    //如果是保存并新增的话，日期跟上一条的日期一样
                    master.DATE = MasterDate;
                }
                master.CompanyID = LocalData.UserInfo.DefaultCompanyID;
                master.CompanyName = LocalData.UserInfo.DefaultCompanyName;
                master.CreateBy = LocalData.UserInfo.LoginID;
                master.Creator = LocalData.UserInfo.UserName;
                master.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                master.IsDirty = false;
                master.DetailList = new List<Ledgers>();
                bsDetails.DataSource = master.DetailList;
            }
            else
            {
                if (data is LedgerListInfo)
                {
                    master = FinanceService.GetLedgerMastersInfo(((LedgerListInfo)data).ID, LocalData.IsEnglish);
                }
                else if (data is LedgerMasters)
                {
                    master = data as LedgerMasters;
                }
                else if (data is List<Ledgers>)
                {
                    isCopy = true;
                    master = new LedgerMasters();
                    master.ID = Guid.NewGuid();
                    master.IsValid = true;
                    master.Status = LedgerMasterStatus.CreateBy;
                    master.Type = LedgerMasterType.Commision;
                    master.No = string.Empty;
                    if (MasterDate == null)
                    {
                        master.DATE = DateTime.Now;
                    }
                    else
                    {
                        //如果是保存并新增的话，日期跟上一条的日期一样
                        master.DATE = MasterDate;
                    }
                    master.CompanyID = LocalData.UserInfo.DefaultCompanyID;
                    master.CompanyName = LocalData.UserInfo.DefaultCompanyName;
                    master.CreateBy = LocalData.UserInfo.LoginID;
                    master.Creator = LocalData.UserInfo.UserName;
                    master.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    master.IsDirty = true;
                    foreach (Ledgers ledger in (List<Ledgers>)data)
                    {
                        ledger.Id = Guid.NewGuid();
                        ledger.Date = (DateTime)master.DATE;
                        ledger.MasterID = master.ID;
                        ledger.RefId = null;
                        ledger.RefNo = null;
                        ledger.OperationNo = null;
                    }
                    master.DetailList = (List<Ledgers>)data;
                    bsDetails.DataSource = master.DetailList;
                }
            }

            IsDeleteData = false;
            bsMaster.DataSource = master;
            bsMaster.ResetBindings(false);
            master.CancelEdit();
            master.BeginEdit();

            bsDetails.DataSource = master.DetailList;
            bsDetails.ResetBindings(false);

            SetPageButton();
            Total();
            SetButtomEnabled();
            remark = string.Empty;

            if (isCopy)
            {
                deDate.EditValueChanged += DeDate_EditValueChanged;
            }

            if (FindForm() != null)
            {
                if (string.IsNullOrEmpty(master.No))
                {
                    FindForm().Text = LocalData.IsEnglish ? "New Voucher" : "新增凭证";
                }
                else
                {
                    string titel = LocalData.IsEnglish ? "Edit Voucher" : "编辑凭证";
                    if (master.No.Length >= 8)
                    {
                        titel = titel + master.No.Substring(4, 4);
                    }
                    FindForm().Text = titel;
                }
            }
        }
        /// <summary>
        /// 客户查询筛选：已审核客户
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetCustomerStateCondition()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("CodeApplyState", CustomerCodeApplyState.Passed, false);
            return conditions;
        }
        /// <summary>
        /// 会计科目筛选：当前口岸所属解决方案下科目
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetSolutionIDSearchCondition()
        {
            Guid solutionID = Guid.Empty;
            ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(CompanyID);
            if (configureInfo != null)
            {
                solutionID = configureInfo.SolutionID;
            }
            List<Guid> idList = new List<Guid>();
            idList.Add(CompanyID);

            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("SolutionID", solutionID, false);
            conditions.AddWithValue("OnlyLeaf", true, false);
            conditions.AddWithValue("CompanyIds", idList, false);
            return conditions;
        }
        /// <summary>
        /// 绑定员工
        /// </summary>
        private void BindStaff()
        {
            if (!isBindStaff && !isDetailBindComplete)
            {
                return;
            }
            Guid? DepID = null;
            if (treeDepartment.EditValue != null && treeDepartment.EditValue != Guid.Empty)
            {
                DepID = treeDepartment.EditValue;
            }
            ICPCommUIHelper.SetMcmbUsers(mscStaff, DepID == null ? Guid.Empty : DepID.Value, string.Empty, string.Empty,
                true, null);
            isBindStaff = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyConfig"></param>
        /// <param name="no"></param>
        /// <param name="bankDate"></param>
        /// <returns></returns>
        private bool IsUntieLock(ConfigureInfo companyConfig, string no, DateTime? bankDate)
        {
            string message = string.Empty;
            if (bankDate == null)
            {
                if (LocalData.IsEnglish)
                {
                    message = string.Format("LedgerNo[{0}] not create Date", no);
                }
                else
                {
                    message = string.Format("凭证号[{0}]没有生成日期", no);
                }
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), message);
                return false;
            }

            //计费还没有关帐的，不需要解锁
            if (DataTypeHelper.GetDateTime(bankDate).Date > DataTypeHelper.GetDateTime(companyConfig.AccountingClosingdate).Date)
            {
                if (((DateTime)bankDate).ToString("yyyyMMdd") != ((DateTime)companyConfig.AccountingClosingdate).ToString("yyyyMMdd"))
                {
                    if (LocalData.IsEnglish)
                    {
                        message = string.Format("LedgerNo[{0}] Create Date [{1}] is not Accounting Closing [{2}] period, no need to unlock", no, bankDate.Value.ToShortDateString(), companyConfig.ChargingClosingdate.Value.ToShortDateString());
                    }
                    else
                    {
                        message = string.Format("凭证单号[{0}]的生成日期[{1}]在不在会计关帐[{2}]期内,不需要解锁", no, bankDate.Value.ToShortDateString(), companyConfig.ChargingClosingdate.Value.ToShortDateString());
                    }
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), message);
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool Leaving()
        {
            if (IsDeleteData && !ValidateAmount(true))
            {
                //如果有删除过数据，先验证删除后是否平衡
                MessageBoxService.ShowInfo("借方金额与贷方金额不相等,请补充完整.");
                return false;
            }

            if (IsDirty)
            {
                DialogResult result = FAMUtility.EnquireIsSaveCurrentDataByUpdated();
                if (result == DialogResult.Yes)
                {
                    return Save();
                }
                else if (result == DialogResult.No)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public override bool SaveData()
        {
            return Save();
        }

        public override void EndEdit()
        {
            bsMaster.EndEdit();
            bsDetails.EndEdit();
            master.EndEdit();
            foreach (Ledgers item in DataList)
            {
                item.EndEdit();
            }
        }

        private bool Save()
        {
            gcMain.Focus();
            txtNo.Focus();

            if (!ValidateData())
                return false;

            LedgerSaveRequest saveRequest = new LedgerSaveRequest();

            saveRequest.ID = master.ID;
            saveRequest.CompanyID = master.CompanyID;
            saveRequest.ReceiptQty = master.ReceiptQty;
            saveRequest.Type = master.Type;
            saveRequest.DATE = master.DATE;
            saveRequest.AccountingID = master.AccountingID;
            saveRequest.CashierID = master.CashierID;
            saveRequest.UpdateDate = master.UpdateDate;
            saveRequest.IsCarryOver = master.IsCarryOver;

            MasterDate = master.DATE;

            List<Guid?> idList = new List<Guid?>();
            List<Guid> glList = new List<Guid>();
            List<Guid?> companyList = new List<Guid?>();
            List<string> remarkList = new List<string>();
            List<decimal> drAmtList = new List<decimal>();
            List<decimal> crAmtList = new List<decimal>();
            List<decimal> orgAmtList = new List<decimal>();
            List<decimal?> rateList = new List<decimal?>();
            List<Guid?> customerList = new List<Guid?>();
            List<Guid?> depList = new List<Guid?>();
            List<Guid?> userList = new List<Guid?>();
            List<DateTime> datelist = new List<DateTime>();
            List<DateTime?> updatelist = new List<DateTime?>();

            List<Guid?> refIDList = new List<Guid?>();
            List<string> refNoList = new List<string>();
            List<string> operationNoList = new List<string>();
            List<LedgerDetailType?> typeList = new List<LedgerDetailType?>();

            foreach (Ledgers item in DataList)
            {
                idList.Add(item.Id);
                glList.Add(item.GLId);
                companyList.Add(item.CompanyId);
                remarkList.Add(item.Remark);
                drAmtList.Add(item.DRAmt);
                crAmtList.Add(item.CRAmt);
                orgAmtList.Add(item.OrgAmt);
                rateList.Add(item.Rate);

                customerList.Add(item.CustomerId);
                depList.Add(item.DepID);
                userList.Add(item.UserID);
                if (MasterDate != null)
                {
                    datelist.Add((DateTime)MasterDate);
                }
                else
                    datelist.Add(item.Date);

                updatelist.Add(item.UpdateDate);

                refIDList.Add(item.RefId);
                refNoList.Add(item.RefNo);
                operationNoList.Add(item.OperationNo);
                typeList.Add(item.Type);
            }

            saveRequest.DetailIDS = idList.ToArray();
            saveRequest.GLIDs = glList.ToArray();
            saveRequest.CompanyIds = companyList.ToArray();
            saveRequest.Remarks = remarkList.ToArray();
            saveRequest.DRAmts = drAmtList.ToArray();
            saveRequest.CRAmts = crAmtList.ToArray();
            saveRequest.OrgAmts = orgAmtList.ToArray();
            saveRequest.Rates = rateList.ToArray();

            saveRequest.CustomerIDs = customerList.ToArray();
            saveRequest.DepIDs = depList.ToArray();
            saveRequest.UserIDs = userList.ToArray();
            saveRequest.Dates = datelist.ToArray();
            saveRequest.UpdateDates = updatelist.ToArray();

            saveRequest.RefIDs = refIDList.ToArray();
            saveRequest.RefNos = refNoList.ToArray();
            saveRequest.OperationNos = operationNoList.ToArray();
            saveRequest.DetailTypes = typeList.ToArray();

            saveRequest.SaveBy = LocalData.UserInfo.LoginID;

            try
            {
                HierarchyManyResult result = FinanceService.SaveLedgerInfo(saveRequest);

                master.ID = result.GetValue<Guid>("ID");
                master.No = result.GetValue<string>("No");
                master.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                master.IsDirty = false;
                for (int i = 0; i < DataList.Count; i++)
                {
                    DataList[i].Id = result.Childs[i].GetValue<Guid>("ID");
                    DataList[i].UpdateDate = result.Childs[i].GetValue<DateTime?>("UpdateDate");
                    DataList[i].IsDirty = false;
                }
                isDirty = false;
                IsDeleteData = true;

                if (Saved != null)
                {
                    Saved(new object[] { master.ID });
                }

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                return false;
            }
            return true;
        }

        private bool ValidateData()
        {
            gvDetails.Focus();
            bsMaster.EndEdit();
            bsDetails.EndEdit();

            if (!master.Validate())
                return false;

            if (DataList == null || DataList.Count == 0)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "Please log in ledger detail" : "请录入凭证明细数据");
                return false;
            }
            int n = 1;
            foreach (Ledgers item in DataList)
            {
                bool isSuccess = true;
                decimal currentMoney = FAMUtility.Get2Round(DataTypeHelper.GetDecimal(item.OrgAmt, 0) * DataTypeHelper.GetDecimal(item.Rate, 0));
                if (item.Date.ToString("YYYYMM") != master.DATE.Value.ToString("YYYYMM"))
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), "第{" + n.ToString() + "}行中的月份与主表的中的月份不一致", deDate);
                    isSuccess = false;
                }
                if (item.IsCustomerCheck && FAMUtility.GuidIsNullOrEmpty(item.CustomerId))
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), "第{" + n.ToString() + "}行中的科目辅助核算为{客户往来},请选择客户", txtCustomer);
                    isSuccess = false;
                }
                if (item.IsDepartmentCheck && FAMUtility.GuidIsNullOrEmpty(item.DepID))
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), "第{" + n.ToString() + "}行中的科目辅助核算为{部门核算},请选择部门", treeDepartment);
                    isSuccess = false;
                }
                if (item.IsPersonalCheck && FAMUtility.GuidIsNullOrEmpty(item.UserID))
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), "第{" + n.ToString() + "}行中的科目辅助核算为{个人往来},请选择个人", mscStaff);
                    isSuccess = false;
                }
                //同时录入了借贷方
                if (DataTypeHelper.GetDecimal(item.DRAmt, 0) != 0 && DataTypeHelper.GetDecimal(item.CRAmt, 0) != 0)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), "第{" + n.ToString() + "}行中的不能同时录入了借/贷方金额", FindForm());
                    isSuccess = false;
                }
                //借贷方的金额都为0
                if (DataTypeHelper.GetDecimal(item.DRAmt, 0) == 0 && DataTypeHelper.GetDecimal(item.CRAmt, 0) == 0)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), "第{" + n.ToString() + "}行中的借/贷方金额都为0", FindForm());
                    isSuccess = false;
                }
                //汇率不正确
                if (item.ForeignCurrencyID != null)
                {
                    decimal rate = RateHelperService.GetRate(item.ForeignCurrencyID.Value, StandardCurrencyID, DateTime.SpecifyKind(item.Date, DateTimeKind.Unspecified), RateList);
                    if (rate != item.Rate)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), "第{" + n.ToString() + "}行中的汇率应该是" + rate.ToString("F2"), FindForm());
                        isSuccess = false;
                    }
                }
                if (currentMoney != DataTypeHelper.GetDecimal(item.DRAmt, 0) && currentMoney != DataTypeHelper.GetDecimal(item.CRAmt, 0) && Math.Abs(Math.Abs(item.DRAmt - item.CRAmt) - currentMoney) > 0.01M)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), "第{" + n.ToString() + "}行中的本位币和外币的金额不相等", FindForm());
                    isSuccess = false;
                }
                if (!isSuccess || !item.Validate())
                {
                    gcMain.Refresh();
                    return false;
                }
                n++;
            }
            if (!ValidateAmount(false))
            {
                return false;
            }

            return true;
        }

        private bool ValidateAmount(bool isCloseForm)
        {
            if (isCloseForm && string.IsNullOrEmpty(txtNo.Text))
            {
                //关闭时，如果是新增的，则不处理这个事件
                return true;
            }
            decimal totalCRAmount = (from d in DataList select d.CRAmt).Sum();
            decimal totalDRAmount = (from d in DataList select d.DRAmt).Sum();

            //借方金额须等于贷方金额
            if (totalCRAmount != totalDRAmount)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "The amount CRAmt and DRAmt amount of inconsistency" : "借方金额与贷方金额不平衡");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 新增凭证明细
        /// </summary>
        private void AddData()
        {
            gvDetails.CloseEditor();
            Ledgers item = new Ledgers();
            item.CompanyId = master.CompanyID;
            item.CRAmt = 0.0m;
            item.DRAmt = 0.0m;
            item.Date = DateTime.Now;
            item.OrgAmt = 0.0m;
            item.Rate = 1;
            item.Remark = remark;
            if (master != null && master.DATE != null)
            {
                item.Date = master.DATE.Value;
            }
            else
            {
                item.Date = DateTime.Now;
            }

            if (bsDetails.DataSource == null)
            {
                bsDetails.DataSource = new List<Ledgers>();
            }
            bsDetails.Add(item);

            gvDetails.FocusedRowHandle = bsDetails.Count - 1;

            isDirty = true;
        }
        /// <summary>
        /// 绑定明细信息
        /// </summary>
        private void BindDetailInfo()
        {
            isDetailBindComplete = false;

            if (CurrentRow == null)
            {
                return;
            }

            bsDetails2.DataSource = CurrentRow;
            bsDetails2.ResetBindings(false);

            //辅助核算
            txtCustomer.BackColor = CurrentRow.IsCustomerCheck ? Color.LightYellow : Color.White;
            treeDepartment.BackColor = CurrentRow.IsDepartmentCheck ? Color.LightYellow : Color.White;
            mscStaff.BackColor = CurrentRow.IsPersonalCheck ? Color.LightYellow : Color.White;

            bool currentIsDirty = CurrentRow.IsDirty;
            if (CurrentRow.DepID != null)
            {
                treeDepartment.EditValue = (Guid)CurrentRow.DepID;
            }
            else
            {
                treeDepartment.EditValue = new Guid();
            }

            treeDepartment.Text = CurrentRow.Dept;
            mscStaff.ShowSelectedValue(DataTypeHelper.GetGuid(CurrentRow.UserID, Guid.Empty), DataTypeHelper.GetString(CurrentRow.UserName, string.Empty));

            if (!currentIsDirty)
            {
                CurrentRow.IsDirty = false;
            }


            isDetailBindComplete = true;
        }
        /// <summary>
        /// 更新Total数据
        /// </summary>
        private void Total()
        {
            decimal crAmt = 0.0m;
            decimal drAmt = 0.0m;

            if (DataList != null)
            {
                crAmt = (from d in DataList select d.CRAmt).Sum();
                drAmt = (from d in DataList select d.DRAmt).Sum();
            }

            txtTotalCR.Text = crAmt.ToString("n");
            txtTotalDR.Text = drAmt.ToString("n");
        }

        private void SearchVouch(VoucherSearchType type)
        {
            if (barSave.Enabled)
            {
                //有改变，并且不保存时
                if (!Leaving())
                {
                    return;
                }
            }
            if (master == null)
            {
                return;
            }
            //凭证号是空时，不能点下一张与末页
            if (string.IsNullOrEmpty(master.No) && (type == VoucherSearchType.Next || type == VoucherSearchType.End))
            {
                return;
            }
            if (master.DATE == null)
            {
                master.DATE = DateTime.Now;
            }
            LedgerMasters data = FinanceService.GetLedgerMastersByNo(master.CompanyID, master.No, master.DATE.Value, type);
            if (data != null)
            {
                DataSource = data;

            }
        }
        private bool CheckEnblie()
        {
            List<User2OrganizationJobList> jobList = UserService.GetUser2OrganizationJobList(LocalData.UserInfo.LoginID);
            foreach (User2OrganizationJobList job in jobList)
            {
                if (job.OrganizationJobName.Contains("财务部经理") || job.OrganizationJobName.Contains("财务经理") || job.OrganizationJobName.Contains("财务行政部->经理") || job.OrganizationJobName.Contains("管理员") || job.OrganizationJobName.Contains("软件工程师"))
                {
                    return true;
                }
            }

            return false;
        }
        #endregion
    }
}
