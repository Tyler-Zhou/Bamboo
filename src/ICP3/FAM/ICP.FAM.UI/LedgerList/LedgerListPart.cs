using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Common.ServiceInterface.Client;
using ICP.Framework.ClientComponents.Controls;
using System.Threading;
using ICP.Common.ServiceInterface;

namespace ICP.FAM.UI
{
    [ToolboxItem(false)]
    public partial class LedgerListPart : BaseListPart
    {
        #region 服务
        [ServiceDependency]
        public WorkItem workItem { get; set; }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        public IReportViewService ReportViewService
        {
            get
            {
                return ServiceClient.GetClientService<IReportViewService>();
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

        #region 属性、变量
        /// <summary>
        /// 选择的数据
        /// </summary>
        List<LedgerListInfo> SelectedDataList
        {
            get
            {
                int[] rowIndexs = gvMain.GetSelectedRows();

                if (rowIndexs.Length == 0) return null;

                List<LedgerListInfo> tagers = new List<LedgerListInfo>();
                foreach (var item in rowIndexs)
                {
                    if (item < 0)
                        continue;
                    LedgerListInfo dr = gvMain.GetRow(item) as LedgerListInfo;
                    if (dr != null) tagers.Add(dr);
                }

                return tagers;
            }
        }
        #endregion

        #region 初始化

        public LedgerListPart()
        {
            InitializeComponent();
            Disposed += delegate {
                KeyDown = null;
                CurrentChanged = null;
                gcMain.DataSource = null;
                bsList.PositionChanged -= bsList_PositionChanged;
                bsList.DataSource = null;
                bsList.Dispose();
                
                if (workItem != null)
                {
                    workItem.Items.Remove(this);
                    workItem = null;
                }
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitMessage();
                InitControls();
            }
        }

        private void InitControls()
        {
            FAMUtility.ShowGridRowNo(gvMain);


            List<EnumHelper.ListItem<LedgerMasterType>> masterType = EnumHelper.GetEnumValues<LedgerMasterType>(LocalData.IsEnglish);
            cmbType.Properties.Items.Clear();
            foreach (var item in masterType)
            {
                if (item.Value != LedgerMasterType.Unknown && item.Value != LedgerMasterType.CarryForward)
                {
                    cmbType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
                }
            }
            List<EnumHelper.ListItem<LedgerMasterStatus>> masterStatus = EnumHelper.GetEnumValues<LedgerMasterStatus>(LocalData.IsEnglish);
            cmbStatus.Properties.Items.Clear();
            foreach (var item in masterStatus)
            {
                if (item.Value != LedgerMasterStatus.Unknown)
                {
                    cmbStatus.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
                }
            }           

        }

        private void InitMessage()
        {
            RegisterMessage("1108100001", LocalData.IsEnglish ? "Are you sure to invalidate the selected time?" : "确认要作废该数据");
            RegisterMessage("1108100002", LocalData.IsEnglish ? "Are you sure to resume the selected time?" : "确认要激活该数据");

            RegisterMessage("13080901", LocalData.IsEnglish ? "Selected data in an inconsistent state" : "选择的数据状态不一致");
            RegisterMessage("13080902", LocalData.IsEnglish ? "The auditor and the creator cannot be the same person." : "审核人不能跟制单人相同");

            RegisterMessage("13080903", LocalData.IsEnglish ? "You sure you want to audit?" : "确定要审核选择单据吗?");
            RegisterMessage("13080904", LocalData.IsEnglish ? "You sure you want to cancel audit?" : "确定要取消审核选择单据吗?");

            RegisterMessage("13080905", LocalData.IsEnglish ? "You sure you want to keep accounts?" : "确定要对选择单据记账吗?");
            RegisterMessage("13080906", LocalData.IsEnglish ? "You sure you want to cancel accounts?" : "确定要取消选择单据的记账吗?");

            RegisterMessage("13081201", LocalData.IsEnglish ? "Voucher Total {0}" : "凭证共 {0} 张");
            RegisterMessage("13081202", LocalData.IsEnglish ? "Audit {0}" : "已审核 {0} 张");
            RegisterMessage("13081203", LocalData.IsEnglish ? "Un Aduit {0}" : "未审核 {0} 张");
            RegisterMessage("1411068888888", LocalData.IsEnglish ? "Are you sure to untie lock" : "确认解锁?");
        }

        #endregion

        #region 重写

        public override event CurrentChangedHandler CurrentChanged;

        /// <summary>
        /// 当前行
        /// </summary>
        public override object Current
        {
            get
            {
                return bsList.Current;
            }
        }

        protected LedgerListInfo CurrentRow
        {
            get { return bsList.Current as LedgerListInfo; }
        }

        public List<LedgerListInfo> DataSourceList
        {
            get { return bsList.DataSource as List<LedgerListInfo>; }
        }

        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }

        DataPageInfo dataPageInfo = new DataPageInfo();
        private void BindingData(object value)
        {
            List<LedgerListInfo> source = value as List<LedgerListInfo>;

            if (source == null || source.Count == 0)
            {
                bsList.DataSource = typeof(LedgerListInfo);
            }
            else
            {
                if (source.Count > 1000)
                {
                    gvMain.IndicatorWidth = 42;
                }
                bsList.DataSource = source;
                bsList.ResetBindings(false);               
            }
            TotalInfo();
        }

        #endregion

        #region 按钮方法

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(LedgerListCommandConstants.Command_Cancel)]
        public void Command_Cancel(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            try
            {
                string message = string.Empty;
                if (CurrentRow.Status != LedgerMasterStatus.CreateBy)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "Ledger have been audited,delete is not allowed." : "单据已审核，不允许删除。");
                    return;
                }
                if (CurrentRow.Type != LedgerMasterType.Commision)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), "不允许删除非管理成本的凭证");
                    return;
                }
                else
                {
                    if (CurrentRow.RefNo.Contains("WF"))
                    {
                        if (!FAMUtility.ShowResultMessage(LocalData.IsEnglish ? "This Voucher is the process generated, whether to confirm delete" : "此凭证是流程生成，是否确认删除！"))
                        {
                            return;
                        }
                        //LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), "不允许删除由流程生成的凭证");
                    }
                }
                if (CurrentRow.IsValid)
                    message = NativeLanguageService.GetText(this, "1108100001");


                if (FAMUtility.ShowResultMessage(message))
                {
                     FinanceService.CancelLedgerList(CurrentRow.ID,
                                    CurrentRow.IsValid,
                                    LocalData.UserInfo.LoginID,
                                    CurrentRow.UpdateDate,
                                    LocalData.IsEnglish);

                    bsList.RemoveCurrent();

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), "删除成功!");
                    if (CurrentChanged != null)
                    {
                        CurrentChanged(this, CurrentRow);
                    }
                    gvMain.RefreshData();
                    TotalInfo();
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), (LocalData.IsEnglish ? "Changed LedgerList State Failed" : "更改凭证列表状态失败") + ex.Message);
            }
        }

        /// <summary>
        /// 出纳签字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(LedgerListCommandConstants.Command_Cashier)]
        public void Command_Cashier(object sender, EventArgs e)
        {
            if (SelectedDataList.Count == 0 || CurrentRow == null) return;

            int count = 0;
            LedgerMasterStatus status = LedgerMasterStatus.CreateBy;
            string message = string.Empty;
            List<Guid> idList = new List<Guid>();
            List<DateTime?> dateList = new List<DateTime?>();

            count = (from d in SelectedDataList where d.Status != CurrentRow.Status select d).Count();
            if (count > 0)
            {
                //选择的状态不一致
                message = NativeLanguageService.GetText(this, "13080901");
                FAMUtility.ShowMessage(message);
                return;
            }
            if (CurrentRow.Status == LedgerMasterStatus.CreateBy)
            {
                status = LedgerMasterStatus.CashierChecked;
                message = NativeLanguageService.GetText(this, "13080903");
            }
            else
            {

                status = LedgerMasterStatus.CreateBy;
                message = NativeLanguageService.GetText(this, "13080904");
            }

            foreach (LedgerListInfo item in SelectedDataList)
            {
                if (!idList.Contains(item.ID))
                {
                    idList.Add(item.ID);
                    dateList.Add(item.UpdateDate);
                }
            }

            try
            {
                if (FAMUtility.ShowResultMessage(message))
                {
                    ManyResult result = FinanceService.CashierCheckedLedgerList(
                        idList.ToArray(),
                        status,
                        LocalData.UserInfo.LoginID,
                        dateList.ToArray(),
                        LocalData.IsEnglish);

                    for (int i = 0; i < result.Items.Count; i++)
                    {
                        if (i >= result.Items.Count())
                        {
                            break;
                        }
                        SelectedDataList[i].ID = result.Items[i].GetValue<Guid>("ID");
                        SelectedDataList[i].UpdateDate = result.Items[i].GetValue<DateTime?>("UpdateDate");
                        SelectedDataList[i].Status = status;
                        SelectedDataList[i].Cashier = (status == LedgerMasterStatus.CashierChecked ? LocalData.UserInfo.UserName : "");
                    }


                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Audit Successfully" : "审核成功");
                    if (CurrentChanged != null)
                    {
                        CurrentChanged(this, CurrentRow);
                    }
                    gvMain.RefreshData();
                    TotalInfo();
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), (LocalData.IsEnglish ? "Audit Failed" : "审核失败") + ex.Message);
            }

        }

        /// <summary>
        /// 财务主管签字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(LedgerListCommandConstants.Command_FM)]
        public void Command_FM(object sender, EventArgs e)
        {
            if (SelectedDataList.Count == 0 || CurrentRow == null) return;

            int count = 0;
            LedgerMasterStatus status = LedgerMasterStatus.CashierChecked;
            string message = string.Empty;
            List<Guid> idList = new List<Guid>();
            List<DateTime?> dateList = new List<DateTime?>();

            count = (from d in SelectedDataList where d.Status != CurrentRow.Status select d).Count();
            if (count > 0)
            {
                //选择的状态不一致
                message = NativeLanguageService.GetText(this, "13080901");
                FAMUtility.ShowMessage(message);
                return;
            }
            if (CurrentRow.Status == LedgerMasterStatus.CashierChecked)
            {
                status = LedgerMasterStatus.FinanceManagerChecked;
                message = NativeLanguageService.GetText(this, "13080903");
            }
            else
            {

                status = LedgerMasterStatus.CashierChecked;
                message = NativeLanguageService.GetText(this, "13080904");
            }

            foreach (LedgerListInfo item in SelectedDataList)
            {
                if (!idList.Contains(item.ID))
                {
                    idList.Add(item.ID);
                    dateList.Add(item.UpdateDate);
                }
            }

            try
            {
                if (FAMUtility.ShowResultMessage(message))
                {
                    ManyResult result = FinanceService.FinanceManagerCheckedLedgerList(
                        idList.ToArray(),
                        status,
                        LocalData.UserInfo.LoginID,
                        dateList.ToArray(),
                        LocalData.IsEnglish);

                    for (int i = 0; i < result.Items.Count; i++)
                    {
                        if (i >= result.Items.Count())
                        {
                            break;
                        }
                        SelectedDataList[i].ID = result.Items[i].GetValue<Guid>("ID");
                        SelectedDataList[i].UpdateDate = result.Items[i].GetValue<DateTime?>("UpdateDate");
                        SelectedDataList[i].Status = status;                        
                    }


                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Audit Successfully" : "审核成功");
                    if (CurrentChanged != null)
                    {
                        CurrentChanged(this, CurrentRow);
                    }
                    gvMain.RefreshData();
                    TotalInfo();
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), (LocalData.IsEnglish ? "Audit Failed" : "审核失败") + ex.Message);
            }

        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(LedgerListCommandConstants.Command_Aduitor)]
        public void Command_Aduitor(object sender, EventArgs e)
        {
            if (SelectedDataList.Count == 0||CurrentRow==null) return;

            int count=0;
            LedgerMasterStatus status = LedgerMasterStatus.CashierChecked;
            string message = string.Empty;
            List<Guid> idList = new List<Guid>();
            List<DateTime?> dateList = new List<DateTime?>();

            count = (from d in SelectedDataList where d.Status != CurrentRow.Status select d).Count();
            if (count > 0)
            {
                //选择的状态不一致
                message = NativeLanguageService.GetText(this, "13080901");
                FAMUtility.ShowMessage(message);
                return;
            }
            if (CurrentRow.Status == LedgerMasterStatus.CashierChecked || CurrentRow.Status== LedgerMasterStatus.FinanceManagerChecked)
            {
                count = (from d in SelectedDataList where d.CreateBy == LocalData.UserInfo.LoginID select d).Count();
                if (count > 0)
                {
                    //审核人跟制单人不能是同一个
                    message = NativeLanguageService.GetText(this, "13080902");
                    FAMUtility.ShowMessage(message);
                    return;
                }
                status = LedgerMasterStatus.Auditor;
                message = NativeLanguageService.GetText(this, "13080903");
            }
            else
            {
                if (CurrentRow.FinanceManagerDate != null)
                    status = LedgerMasterStatus.FinanceManagerChecked;
                else
                    status = LedgerMasterStatus.CashierChecked;
                message = NativeLanguageService.GetText(this, "13080904");
            }

            foreach (LedgerListInfo item in SelectedDataList)
            {
                if (!idList.Contains(item.ID))
                {
                    idList.Add(item.ID);
                    dateList.Add(item.UpdateDate);
                }
            }

            try
            {
                if (FAMUtility.ShowResultMessage(message))
                {
                    ManyResult result = FinanceService.AduitLedgerList(
                        idList.ToArray(),
                        status,
                        LocalData.UserInfo.LoginID,
                        dateList.ToArray(),
                        LocalData.IsEnglish);

                    for (int i = 0; i < result.Items.Count; i++)
                    {
                        if (i >= result.Items.Count())
                        {
                            break;
                        }
                        SelectedDataList[i].ID = result.Items[i].GetValue<Guid>("ID");
                        SelectedDataList[i].UpdateDate = result.Items[i].GetValue<DateTime?>("UpdateDate");
                        SelectedDataList[i].Status = status;
                        SelectedDataList[i].Auditor = (status==LedgerMasterStatus.Auditor?LocalData.UserInfo.UserName:"");
                    }


                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Audit Successfully" : "审核成功");
                    if (CurrentChanged != null)
                    {
                        CurrentChanged(this, CurrentRow);
                    }
                    gvMain.RefreshData();
                    TotalInfo();
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), (LocalData.IsEnglish ? "Audit Failed" : "审核失败") + ex.Message);
            }

        }

        /// <summary>
        /// 记账
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(LedgerListCommandConstants.Command_KeepAccounts)]
        public void Command_KeepAccounts(object sender, EventArgs e)
        {
            if (SelectedDataList.Count == 0 || CurrentRow == null) return;

            int count = 0;
            LedgerMasterStatus status = LedgerMasterStatus.Auditor;
            string message = string.Empty;
            List<Guid> idList = new List<Guid>();
            List<DateTime?> dateList = new List<DateTime?>();

            count = (from d in SelectedDataList where d.Status != CurrentRow.Status select d).Count();
            if (count > 0)
            {
                //选择的状态不一致
                message = NativeLanguageService.GetText(this, "13080901");
                FAMUtility.ShowMessage(message);
                return;
            }
            if (CurrentRow.Status == LedgerMasterStatus.Auditor)
            {
                status = LedgerMasterStatus.KeepAccounts;
                message = NativeLanguageService.GetText(this, "13080905");
            }
            else
            {
                status = LedgerMasterStatus.Auditor;
                message = NativeLanguageService.GetText(this, "13080906");
            }

            foreach (LedgerListInfo item in SelectedDataList)
            {
                if (!idList.Contains(item.ID))
                {
                    idList.Add(item.ID);
                    dateList.Add(item.UpdateDate);
                }
            }

            try
            {
                if (FAMUtility.ShowResultMessage(message))
                {
                    ManyResult result = FinanceService.KeepAccountsLedgerList(
                        idList.ToArray(),
                        status,
                        LocalData.UserInfo.LoginID,
                        dateList.ToArray(),
                        LocalData.IsEnglish);

                    for (int i = 0; i < result.Items.Count; i++)
                    {
                        if (i >= result.Items.Count())
                        {
                            break;
                        }
                        SelectedDataList[i].ID = result.Items[i].GetValue<Guid>("ID");
                        SelectedDataList[i].UpdateDate = result.Items[i].GetValue<DateTime?>("UpdateDate");
                        SelectedDataList[i].Status = status;
                    }


                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Accounts Successfully" : "记账成功");
                    if (CurrentChanged != null)
                    {
                        CurrentChanged(this, CurrentRow);
                    }
                    gvMain.RefreshData();
                    TotalInfo();
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), (LocalData.IsEnglish ? "Accounts Failed" : "记账失败") + ex.Message);
            }

        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(LedgerListCommandConstants.Command_Edit)]
        public void Command_Edit(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow != null)
                {
                    string titel=LocalData.IsEnglish ? "Edit Voucher" : "编辑凭证";
                    if (CurrentRow.No.Length>=8)
                    { 
                        titel=titel+CurrentRow.No.Substring(4,4);
                    }
                    PartLoader.ShowEditPart<LedgerListEditPart>(workItem, CurrentRow, titel, EditPartSaved);
                }
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(LedgerListCommandConstants.Command_Add)]
        public void Command_Add(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                PartLoader.ShowEditPart<LedgerListEditPart>(workItem, null, LocalData.IsEnglish ? "Add Voucher" : "新增凭证", EditPartSaved);
            }
        }

        [CommandHandler(LedgerListCommandConstants.Command_Print)]
        public void Command_Print(object sender, EventArgs e)
        {
            if (CurrentRow == null || FAMUtility.GuidIsNullOrEmpty(CurrentRow.ID))
            {
                return;
            }
            LedgerPrint.Print(workItem, FinanceService, ReportViewService, CurrentRow.ID);
        }

        [CommandHandler(LedgerListCommandConstants.Command_BulkPrint)]
        public void Command_BulkPrint(object sender, EventArgs e)
        {
            if (SelectedDataList == null || SelectedDataList.Count == 0)
            {
                return;
            }
            List<Guid> idList = (from d in SelectedDataList select d.ID).ToList();
            string s = idList.ToArray().Join();
            //凭证数据源
            List<PrintLedgerMasterReports> hdList = new List<PrintLedgerMasterReports>();
            hdList = FinanceService.GetBulkPrintLedgerReportDate(idList.ToArray(), LocalData.IsEnglish);
            LedgerPrint.Print(workItem, ReportViewService, hdList);
        }

        /// <summary>
        /// 生成计账凭证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(LedgerListCommandConstants.Command_BillVoucher)]
        public void Command_BillVoucher(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                BillVoucherPart voucherPart = workItem.Items.AddNew<BillVoucherPart>();
                string title = LocalData.IsEnglish ? " Voucher number should be generated after accounting closing!" : "生成记账凭证";
                voucherPart.workType = 1;
               DialogResult result=PartLoader.ShowDialog(voucherPart, title);
               if (result == DialogResult.OK)
               {
                   LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Generate Success." : "生成凭证成功.");
               }
            }
        }

        /// <summary>
        /// 整理凭证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(LedgerListCommandConstants.Command_Reorganize)]
        public void Command_Reorganize(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    BillVoucherPart voucherPart = workItem.Items.AddNew<BillVoucherPart>();
                    string title = LocalData.IsEnglish ? "Arrange Voucher" : "整理凭证";
                    voucherPart.workType = 2;
                    DialogResult result = PartLoader.ShowDialog(voucherPart, title);
                    if (result == DialogResult.OK)
                    {
                        if (!string.IsNullOrEmpty(BillVoucherPart.ReturnMess))
                        {
                            MessageBox.Show(BillVoucherPart.ReturnMess.Replace("\r\n", Environment.NewLine)); 
                        }
                    
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Arrange Success." : "整理凭证成功.");
                        gvMain.RefreshData();
                        TotalInfo();
                    }
                }
            }
        }

        /// <summary>
        /// 汇兑损益
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(LedgerListCommandConstants.Command_AdjustRate)]
        public void Command_AdjustRate(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    BillVoucherPart voucherPart = workItem.Items.AddNew<BillVoucherPart>();
                    string title = LocalData.IsEnglish ? "Adjust Rate" : "汇兑损益";
                    voucherPart.workType = 3;
                    DialogResult result = PartLoader.ShowDialog(voucherPart, title);
                    if (result == DialogResult.OK)
                    {
                        if (!string.IsNullOrEmpty(BillVoucherPart.ReturnMess))
                        {
                            MessageBox.Show(BillVoucherPart.ReturnMess.Replace("\r\n", Environment.NewLine));
                        }

                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "AdjustRate Success." : "汇兑损益生成成功.");
                        gvMain.RefreshData();
                        TotalInfo();
                    }
                }
            }
        }

        #endregion

        #region 私有方法

        private void EditPartSaved(object[] prams)
        {
            if (prams == null || prams.Length == 0)
            {
                return;
            }
            Guid id =new Guid(prams[0].ToString());
            Guid[] idlist = new Guid[1] { id };

            /* 线程暂停一秒钟
             * 原因:流程生成出的凭证，在凭证列表中修改后，不会直接更新凭证表中的数据，
             *      而是通过修改wf.CostDetails中的数据，再通过数据库的高度，重新生成出凭证
             *      可能会导致数据库这边的生成不迅速，导致凭证列表中的借贷方金额不正确
             */

            Thread.Sleep(1000);


            LedgerListInfo data = null;
            List<LedgerListInfo> list = FinanceService.GetLedgerListByID(idlist);
            if (list != null && list.Count > 0)
            {
                data = list[0];
            }

            List<LedgerListInfo> source = DataSource as List<LedgerListInfo>;
            if (source == null || source.Count == 0)
            {
                source = new List<LedgerListInfo>();
                source.Add(data);

                bsList.DataSource = source;
                bsList.ResetBindings(false);
            }
            else
            {
                LedgerListInfo tager = source.Find(delegate(LedgerListInfo item) { return item.ID == data.ID; });
                if (tager == null)
                {
                    bsList.Insert(0, data);
                    bsList.ResetBindings(false);
                }
                else
                {
                    FAMUtility.CopyToValue(data, tager, typeof(LedgerListInfo));
                    bsList.ResetItem(bsList.IndexOf(tager));
                }
            }
        }

        /// <summary>
        /// 统计信息
        /// </summary>
        private void TotalInfo()
        {
            int total = 0;
            int auditorTotal = 0;
            int unAuditorTotal = 0;
            if (DataSourceList != null)
            {

                total = (from d in DataSourceList select d.ID).Count();
                auditorTotal = (from d in DataSourceList where d.Status == LedgerMasterStatus.Auditor select d.ID).Count();
                unAuditorTotal = (from d in DataSourceList where d.Status == LedgerMasterStatus.CreateBy select d.ID).Count();
            }
            string totalMessage = NativeLanguageService.GetText(this, "13081201");
            string auditorTotalMessage = NativeLanguageService.GetText(this, "13081202");
            string unAuditorTotalMessage = NativeLanguageService.GetText(this, "13081203");

            totalMessage = string.Format(totalMessage, total);
            auditorTotalMessage = string.Format(auditorTotalMessage, auditorTotal);
            unAuditorTotalMessage = string.Format(unAuditorTotalMessage, unAuditorTotal);


            labLedgerTotoal.Text = totalMessage;
            labAuditorTotal.Text = auditorTotalMessage;
            labUnAuditor.Text = unAuditorTotalMessage;

        }

        #endregion

        #region gridView事件

        private void gvMain_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (CurrentChanged != null)
                    workItem.Commands[LedgerListCommandConstants.Command_Edit].Execute();
            }
        }

        public new event KeyEventHandler KeyDown;

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && CurrentRow != null)
                workItem.Commands[LedgerListCommandConstants.Command_Edit].Execute();
            if (e.KeyCode == Keys.F6 && CurrentRow != null)
                workItem.Commands[LedgerListCommandConstants.CommandShowSearch].Execute();
        }


        /// <summary>
        /// 作废数据禁用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            LedgerListInfo list = gvMain.GetRow(e.RowHandle) as LedgerListInfo;
            if (list == null) return;

            if (list.IsValid == false)
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.Disabled);
            if (list.Status == LedgerMasterStatus.Auditor)
            {
                e.Appearance.BackColor = Color.LightYellow;
            }
        }
        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null)
                CurrentChanged(this, Current);
        }

        #endregion

    }
}
