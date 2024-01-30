using DevExpress.XtraGrid.Views.Grid;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ICP.FAM.UI.BankReceiptList
{
    [ToolboxItem(false)]
    public partial class BankReceiptListPart : BaseListPart
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

        //public IReportViewService ReportViewService
        //{
        //    get
        //    {
        //        return ServiceClient.GetClientService<IReportViewService>();
        //    }
        //}
        //public IConfigureService ConfigureService
        //{
        //    get
        //    {
        //        return ServiceClient.GetService<IConfigureService>();
        //    }
        //}

        ///// <summary>
        ///// 客户端上传文档服务
        ///// </summary>
        //public IClientFileService ClientFileService
        //{
        //    get
        //    {
        //        return ServiceClient.GetService<IClientFileService>();
        //    }

        //}

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

        public BankReceiptListPart()
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
                gvMain.RowStyle += gvMain_RowStyle;
            }
        }

        private void InitControls()
        {
            FAMUtility.ShowGridRowNo(gvMain);
        }

        private void InitMessage()
        {
            RegisterMessage("1108100001", LocalData.IsEnglish ? "Are you sure to invalidate the selected time?" : "确认要作废该数据");
            RegisterMessage("1108100002", LocalData.IsEnglish ? "Are you sure to resume the selected time?" : "确认要激活该数据");
            RegisterMessage("1109270005", LocalData.IsEnglish ? "The bill is already approved" : "该单据已经审核。");
            RegisterMessage("13080903", LocalData.IsEnglish ? "You sure you want to audit?" : "确定要审核选择单据吗?");
            RegisterMessage("13080904", LocalData.IsEnglish ? "You sure you want to cancel audit?" : "确定要取消审核选择单据吗?");
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

        protected BankReceiptListInfo CurrentRow
        {
            get { return bsList.Current as BankReceiptListInfo; }
        }

        //public List<BankReceiptListInfo> DataSourceList
        //{
        //    get { return bsList.DataSource as List<BankReceiptListInfo>; }
        //}

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
            List<BankReceiptListInfo> source = value as List<BankReceiptListInfo>;

            if (source == null || source.Count == 0)
            {
                bsList.DataSource = typeof(BankReceiptListInfo);
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
            if (CurrentChanged != null)
            {
                CurrentChanged(this, CurrentRow);
            }
        }

        #endregion

        #region 按钮方法
        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BankReceiptListCommandConstants.Command_Add)]
        public void Command_Add(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                PartLoader.ShowEditPart<BankReceiptListEditPart>(workItem, null, LocalData.IsEnglish ? "Add BankReceipt" : "新增水单", EditPartSaved);
            }
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BankReceiptListCommandConstants.Command_Edit)]
        public void Command_Edit(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow != null)
                {
                    string titel = LocalData.IsEnglish ? "Edit BankReceitp" : "编辑水单";
                    if (CurrentRow.No.Length >= 8)
                    {
                        titel = titel + CurrentRow.No.Substring(4, 4);
                    }
                    PartLoader.ShowEditPart<BankReceiptListEditPart>(workItem, CurrentRow, titel, EditPartSaved);
                }
            }
        }
        #endregion

        #region 作废
        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BankReceiptListCommandConstants.Command_Cancel)]
        public void Command_Cancel(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            try
            {
                string message = string.Empty;
                if (CurrentRow.IsValid)
                    message = NativeLanguageService.GetText(this, "1108100001");
                else
                    message = NativeLanguageService.GetText(this, "1108100002");


                if (FAMUtility.ShowResultMessage(message))
                {
                    SingleResult result = FinanceService.CancelBankReceiptList(CurrentRow.ID,
                                    CurrentRow.IsValid,
                                    LocalData.UserInfo.LoginID,
                                    CurrentRow.UpdateDate,
                                    LocalData.IsEnglish);

                    BankReceiptListInfo currentRow = CurrentRow;
                    currentRow.IsValid = !CurrentRow.IsValid;
                    currentRow.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    bsList.ResetCurrentItem();

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Changed BankReceipt State Successfully" : "更改水单列表状态成功");
                    if (CurrentChanged != null)
                    {
                        CurrentChanged(this, CurrentRow);
                    }
                    //gvMain.RefreshData();
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), (LocalData.IsEnglish ? "Changed BankReceipt State Failed" : "更改水单列表状态失败") + ex.Message);
            }
        }
        #endregion

        #region 审核
        [CommandHandler(BankReceiptListCommandConstants.Command_Auditor)]
        public void Command_Auditor(object sender,EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null)
                {
                    return;
                }
                if (CurrentRow.Status == (BankReceiptStatus)2 || CurrentRow.Status == (BankReceiptStatus)3)
                {
                    FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1109270005"));
                    return;
                }
                if (CurrentRow.Status == (BankReceiptStatus)1 && !FAMUtility.ShowResultMessage(NativeLanguageService.GetText(this, "13080903")))
                {
                    return;
                }
                try
                {
                    ManyResult result = FinanceService.AuditorBankReceipt(
                         new Guid[] { CurrentRow.ID },
                         new DateTime?[] { CurrentRow.UpdateDate },
                         LocalData.UserInfo.LoginID,
                         true);

                    CurrentRow.ApprovalName = LocalData.IsEnglish ? LocalData.UserInfo.UserEname : LocalData.UserInfo.UserName;
                    CurrentRow.UpdateDate = result.Items[0].GetValue<DateTime?>("UpdateDate");
                    CurrentRow.ApprovalDate = result.Items[0].GetValue<DateTime?>("ApprovalDate");
                    CurrentRow.Status = (BankReceiptStatus)result.Items[0].GetValue<Byte>("Status");
                    bsList.ResetCurrentItem();
                    if (CurrentChanged != null)
                    {
                        CurrentChanged(this, CurrentRow);
                    }
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Auditor Successfully" : "审核成功");

                    //string message = LocalData.IsEnglish ? "BankReceiptNo:" : "水单号:";
                    //message += CurrentRow.No;
                    //Utility.ShowMessage(message);
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                }
            }
        }
        #endregion

        #region 取消审核
        [CommandHandler(BankReceiptListCommandConstants.Command_UnAuditor)]
        public void Command_UnAuditor(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null)
                {
                    return;
                }
                if (CurrentRow.Status == (BankReceiptStatus)2 && !FAMUtility.ShowResultMessage(NativeLanguageService.GetText(this, "13080904")))
                {
                    return;
                }
                try
                {
                    ManyResult result = FinanceService.AuditorBankReceipt(
                         new Guid[] { CurrentRow.ID },
                         new DateTime?[] { CurrentRow.UpdateDate },
                         LocalData.UserInfo.LoginID,
                         false);

                    CurrentRow.ApprovalName = null;
                    CurrentRow.UpdateDate = result.Items[0].GetValue<DateTime?>("UpdateDate");
                    CurrentRow.ApprovalDate = result.Items[0].GetValue<DateTime?>("ApprovalDate");
                    CurrentRow.Status = (BankReceiptStatus)result.Items[0].GetValue<Byte>("Status");
                    bsList.ResetCurrentItem();
                    if (CurrentChanged != null)
                    {
                        CurrentChanged(this, CurrentRow);
                    }
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "UnAuditor Successfully" : "取消审核成功");
                }
                catch (Exception ex)
                {
                    
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                }
            }
        }
        #endregion

        #endregion

        #region 私有方法

        private void EditPartSaved(object[] prams)
        {
            if (prams == null || prams.Length == 0)
            {
                return;
            }
            BankReceiptListInfo data = prams[0] as BankReceiptListInfo;

            List<BankReceiptListInfo> source = DataSource as List<BankReceiptListInfo>;
            if (source == null || source.Count == 0)
            {
                bsList.Add(data);
                bsList.ResetBindings(false);
            }
            else
            {
                BankReceiptListInfo tager = source.Find(delegate(BankReceiptListInfo item) { return item.ID == data.ID; });
                if (tager == null)
                {
                    bsList.Insert(0, data);
                    bsList.ResetBindings(false);
                }
                else
                {
                    FAMUtility.CopyToValue(data, tager, typeof(BankReceiptListInfo));
                    bsList.ResetItem(bsList.IndexOf(tager));
                }
            }
        }

        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            BankReceiptListInfo list = gvMain.GetRow(e.RowHandle) as BankReceiptListInfo;
            if (list == null) return;

            if (list.IsValid == false)
            {
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.Disabled);
            }
            else if (list.Status == BankReceiptStatus.Created)
            {
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.NewLine);
            }
            else if (list.Status == BankReceiptStatus.Verified)
            {
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.Confirmed);
            }
            else if (list.Status == BankReceiptStatus.WriteOff)
            {
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.Bold);
            }
        }

        #endregion

        #region gridView事件

        private void gvMain_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (CurrentChanged != null)
                    workItem.Commands[BankReceiptListCommandConstants.Command_Edit].Execute();
            }
        }

        public new event KeyEventHandler KeyDown;

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && CurrentRow != null)
                workItem.Commands[BankReceiptListCommandConstants.Command_Edit].Execute();
            if (e.KeyCode == Keys.F6 && CurrentRow != null)
                workItem.Commands[BankReceiptListCommandConstants.CommandShowSearch].Execute();
        }
        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null)
                CurrentChanged(this, Current);
        }

        #endregion

    }
}
