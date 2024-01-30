using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.Common.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.Controls;
using ICP.Common.ServiceInterface.DataObjects;
using System.Text;

namespace ICP.FAM.UI.WriteOff.Parts
{
    /// <summary>
    /// 销账-多选模式
    /// </summary>
    public partial class MultiSelections : BaseListPart
    {
        #region 初始化
        /// <summary>
        /// 销账-多选模式
        /// </summary>
        public MultiSelections()
        {
            InitializeComponent();
            Disposed += delegate
            {
                ListOperating = null;
                Selected = null;
                gcMain.DataSource = null;
                bsList.PositionChanged -= bsList_PositionChanged;
                bsList.DataSource = null;
                bsList.Dispose();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }

            };
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
                InitMessage();
            }
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {

            RefreshControl();

            FAMUtility.ShowGridRowNo(gvWriteOffList);

            cmbWay.Items.Add(new ImageComboBoxItem("", FeeWay.AR, 0));
            cmbWay.Items.Add(new ImageComboBoxItem("", FeeWay.AP, 1));
        }
        /// <summary>
        /// 初始化消息 
        /// </summary>
        private void InitMessage()
        {
            RegisterMessage("11092300001", LocalData.IsEnglish ? "The list contains has collected data, unable to arrive at an account operation" : "列表中包含已到账的数据，无法再进行到账操作");
            RegisterMessage("11092300002", LocalData.IsEnglish ? "The list contains an account data, unable to cancel the account operation" : "列表中包含未到账的数据，无法再进行取消到账操作");
            RegisterMessage("11092300003", LocalData.IsEnglish ? "The list contains the checked data, can no longer be used to audit operation" : "列表中包含已审核的数据，无法再进行审核操作");
            RegisterMessage("11092300004", LocalData.IsEnglish ? "The list contains an audit data, unable to cancel the operational auditing" : "列表中包含未审核的数据，无法再进行取消审核操作");
            RegisterMessage("11092300005", LocalData.IsEnglish ? "The list contains an account data, cannot undertake audit operation" : "列表中包含未到账的数据，无法进行审核操作");

            RegisterMessage("11092600001", LocalData.IsEnglish ? "Input the Actual amount " : "请录入实际金额");
            RegisterMessage("11092600002", LocalData.IsEnglish ? "Input the Account Date" : "请录入到账时间");

            RegisterMessage("1109270009", LocalData.IsEnglish ? "Are you sure to approve" : "确认审核?");
            RegisterMessage("1109270010", LocalData.IsEnglish ? "Are you sure to undo the approval" : "确认取消审核?");
            RegisterMessage("1109270011", LocalData.IsEnglish ? "Are you sure to undo the Paid" : "确认取消到帐?");
        }

        #endregion

        #region 重写
        /// <summary>
        /// 数据源
        /// </summary>
        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                List<WriteOffItemList> list = value as List<WriteOffItemList>;

                foreach (WriteOffItemList item in list)
                {
                    item.FinalAmount = item.Amount;
                }
                StringBuilder sbTotal = new StringBuilder();
                var groutByCurrency = list.GroupBy(gItem => gItem.Currency);
                if (groutByCurrency!=null&&groutByCurrency.Count() > 0)
                {
                    sbTotal.AppendFormat("{0}", (LocalData.IsEnglish ? "Amount" : "金额"));
                    foreach (var item in groutByCurrency)
                    {
                        sbTotal.AppendFormat("{0}:{1}  ", item.Key, item.Sum(sumItem => sumItem.Amount));
                    }
                    sbTotal.AppendFormat("{0}", (LocalData.IsEnglish ? "Final Amount" : "实际金额"));
                    foreach (var item in groutByCurrency)
                    {
                        sbTotal.AppendFormat("{0}:{1}  ", item.Key, item.Sum(sumItem => sumItem.FinalAmount));
                    }
                    txtTotal.Text = "" + sbTotal;
                }
                

                bsList.DataSource = list;
                bsList.ResetBindings(false);

                bsList_PositionChanged(null, null);

                if (list.Count.ToString().Length == 1)
                {
                    gvWriteOffList.IndicatorWidth = 30;
                }
                else
                {
                    gvWriteOffList.IndicatorWidth = list.Count.ToString().Length * 17;
                }

                gvWriteOffList.BestFitColumns();
            }
        }
        /// <summary>
        /// 当前行
        /// </summary>
        protected WriteOffItemList CurrentRow
        {
            get { return bsList.Current as WriteOffItemList; }
        }
        /// <summary>
        /// 数据列表
        /// </summary>
        public List<WriteOffItemList> DataList
        {
            get
            {
                List<WriteOffItemList> list = DataSource as List<WriteOffItemList>;
                if (list == null)
                {
                    list = new List<WriteOffItemList>();
                }
                return list;
            }
        }

        public override event SelectedHandler Selected;
        /// <summary>
        /// 列表操作事件
        /// </summary>
        public event SelectedHandler ListOperating;
        #endregion

        #region 服务
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IFinanceService financeService { get; set; }

        [ServiceDependency]
        public IConfigureService configureService { get; set; }

        #endregion

        #region 按钮方法
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barRemove_ItemClick(object sender, ItemClickEventArgs e)
        {
            WriteOffItemList item = CurrentRow;

            if (DataList.Contains(item))
            {
                if (Selected != null)
                {
                    Selected("Remove", item.ID);
                }

                DataList.Remove(item);
                DataSource = DataList;

                bsList.ResetBindings(false);
            }
        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barClear_ItemClick(object sender, ItemClickEventArgs e)
        {
            string message = LocalData.IsEnglish ? "Sure Clare Data" : "确认清空数据";
            if (!FAMUtility.ShowResultMessage(message))
            {
                return;
            }
            if (Selected != null)
            {
                Selected("Clear", null);
            }

            DataList.Clear();
            DataSource = DataList;
            bsList.ResetBindings(false);
        }
        #endregion

        #region 到账
        /// <summary>
        /// 到账
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barArrive_ItemClick(object sender, ItemClickEventArgs e)
        {

            #region 数据验证

            if (DataList == null || DataList.Count == 0)
            {
                return;
            }

            int i = (from d in DataList where !string.IsNullOrEmpty(d.BankByName) select d).Count();
            if (i > 0)
            {
                //列表中包含已到账的数据
                FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "11092300001"));
                return;
            }

            bsList.EndEdit();

            //实际金额为0
            int f = (from d in DataList where d.FinalAmount == 0 select d).Count();
            if (f > 0)
            {
                FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "11092600001"));
                return;
            }
            //到账时间为空的
            List<WriteOffItemList> list = (from d in DataList where d.ReachedDate == null select d).ToList();
            if (list.Count > 0)
            {
                SetBankDate setBankDate = new SetBankDate();
                string title = LocalData.IsEnglish ? "Set Bank Date" : "设置到帐时间";
                if (DialogResult.OK == PartLoader.ShowDialog(setBankDate, title))
                {
                    list.ForEach(o => o.ReachedDate = setBankDate.BankDate);
                }
                else
                {
                    return;
                }
            }
            #endregion

            #region 获得数据

            //到账数据
            List<Guid> idList = new List<Guid>();
            List<DateTime?> updateDateList = new List<DateTime?>();
            List<DateTime?> reachedDateList = new List<DateTime?>();
            List<Decimal> amountList = new List<decimal>();
            List<Guid?> accountIDList = new List<Guid?>();

            //差异数据
            List<WriteOffCharge> dataList = new List<WriteOffCharge>();

            foreach (WriteOffItemList item in DataList)
            {
                idList.Add(item.ID);
                updateDateList.Add(item.UpdateDate);
                reachedDateList.Add(item.ReachedDate.HasValue ? DateTime.SpecifyKind(item.ReachedDate.Value, DateTimeKind.Unspecified) : item.ReachedDate);
                amountList.Add(item.FinalAmount);
                accountIDList.Add(item.BankAccountID);

                #region 存在差异
                if (item.Amount != item.FinalAmount)
                {
                    WriteOffCharge charge = new WriteOffCharge();
                    charge.CurrencyID = item.CurrencyID;
                    charge.ExchangeRate = 1;
                    charge.Amount = Math.Abs(item.Amount - item.FinalAmount);
                    charge.CheckID = item.CheckID;

                    if (item.Amount > 0)
                    {
                        if (Math.Abs(item.Amount) - Math.Abs(item.FinalAmount) > 0)
                        {
                            charge.Way = FeeWay.AP;
                        }
                        else
                        {
                            charge.Way = FeeWay.AR;
                        }
                    }
                    else
                    {
                        if (Math.Abs(item.Amount) - Math.Abs(item.FinalAmount) > 0)
                        {
                            charge.Way = FeeWay.AR;
                        }
                        else
                        {
                            charge.Way = FeeWay.AP;
                        }
                    }

                    dataList.Add(charge);

                }
                #endregion
            }

            #endregion

            try
            {
                ConfigureInfo configureInfo = configureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
                if (configureInfo.SolutionID == new Guid("b6e4dded-4359-456a-b835-e8401c910fd0")) //是否远东区解决方案的销帐单
                {
                    string message = financeService.CheckExistBankReceived(idList.ToArray(), reachedDateList.ToArray(), amountList.ToArray(), accountIDList.ToArray());
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

                if (dataList.Count > 0)
                {

                    #region 获得公司数据
                    ConfigureInfo companyInfo = configureService.GetCompanyConfigureInfo(DataList[0].CompanyID);
                    if (companyInfo != null)
                    {
                        dataList.ForEach(o => o.CustomerID = companyInfo.CustomerID);
                        dataList.ForEach(o => o.CustomerName = companyInfo.CustomerName);
                    }
                    #endregion

                    #region 弹出差异界面

                    PaidSetExpenses expense = Workitem.Items.AddNew<PaidSetExpenses>();
                    expense.CompnayID = DataList[0].CompanyID;
                    expense.DataSourceList = dataList;

                    string title = LocalData.IsEnglish ? "Set Difference" : "设置差异";
                    if (PartLoader.ShowDialog(expense, title) == DialogResult.OK)
                    {
                        #region 保存差异与到账数据
                        ManyResult result = financeService.WriteOffReached(
                                                           idList.ToArray(),
                                                           reachedDateList.ToArray(),
                                                           amountList.ToArray(),
                                                           accountIDList.ToArray(),
                                                           updateDateList.ToArray(),
                                                           LocalData.UserInfo.LoginID,
                                                           expense.ExpenseList);


                        RefreshDate(result, "Reached");

                        RefreshControl();

                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Set Successfully" : "设置成功");


                        #endregion
                    }


                    #endregion


                }
                else
                {
                    #region 只保存到账数据
                    ManyResult result = financeService.WriteOffReachedByCheck(
                                        idList.ToArray(),
                                        reachedDateList.ToArray(),
                                        amountList.ToArray(),
                                        accountIDList.ToArray(),
                                        updateDateList.ToArray(),
                                        LocalData.UserInfo.LoginID);

                    RefreshDate(result, "Reached");

                    RefreshControl();

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Set Successfully" : "设置成功");

                    #endregion
                }

            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }


        }
        #endregion

        #region 取消到账
        /// <summary>
        /// 取消到账
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barUnArrive_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!FAMUtility.ShowResultMessage(NativeLanguageService.GetText(this, "1109270011")))
            {
                return;
            }

            int i = (from d in DataList where string.IsNullOrEmpty(d.BankByName) select d).Count();
            if (i > 0)
            {
                //列表中包含未到账的数据
                FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "11092300002"));
                return;
            }

            bsList.EndEdit();

            List<Guid> idList = new List<Guid>();
            List<DateTime?> updateDateList = new List<DateTime?>();

            foreach (WriteOffItemList item in DataList)
            {
                idList.Add(item.ID);
                updateDateList.Add(item.UpdateDate);
            }

            try
            {

                string remark = LocalData.UserInfo.LoginName + DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).ToString() + "Cancel Reached";
                ManyResult result = financeService.CancelReached(
                                    idList.ToArray(),
                                    remark,
                                    LocalData.UserInfo.LoginID,
                                    updateDateList.ToArray());

                RefreshDate(result, "CancelReached");

                RefreshControl();

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Cancel Successfully" : "取消成功");

            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }

        }
        #endregion

        #region 审核
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAuditor_ItemClick(object sender, ItemClickEventArgs e)
        {
            int i = (from d in DataList where !string.IsNullOrEmpty(d.ApprovalByName) select d).Count();
            if (i > 0)
            {
                //列表中包含已审核的数据
                FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "11092300003"));
                return;
            }

            int n = (from d in DataList where string.IsNullOrEmpty(d.BankByName) select d).Count();
            if (n > 0)
            {
                //列表中包含未到账的数据
                FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "11092300005"));
                return;
            }

            if (!FAMUtility.ShowResultMessage(NativeLanguageService.GetText(this, "1109270009")))
            {
                return;
            }

            List<Guid> idList = new List<Guid>();
            List<DateTime?> updateDateList = new List<DateTime?>();

            foreach (WriteOffItemList item in DataList)
            {
                if (!idList.Contains(item.CheckID))
                {
                    idList.Add(item.CheckID);
                    updateDateList.Add(item.CheckUpdateDate);
                }
            }

            try
            {
                ManyResult result = financeService.AuditorWriterOff(
                     idList.ToArray(),
                     updateDateList.ToArray(),
                     LocalData.UserInfo.LoginID,
                     true);

                RefreshDate(result, "Auditor");

                RefreshControl();

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Auditor Successfully" : "审核成功");

                string message = LocalData.IsEnglish ? "VoucherSeqNo:" : "凭证号:";
                message += CurrentRow.VoucherSeqNo;
                FAMUtility.ShowMessage(message);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }

        }
        #endregion

        #region 取消审核
        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barUnAuditor_ItemClick(object sender, ItemClickEventArgs e)
        {
            int i = (from d in DataList where string.IsNullOrEmpty(d.ApprovalByName) select d).Count();
            if (i > 0)
            {
                //列表中包含未审核的数据
                FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "11092300004"));
                return;
            }
            if (!FAMUtility.ShowResultMessage(NativeLanguageService.GetText(this, "1109270010")))
            {
                return;
            }

            List<Guid> idList = new List<Guid>();
            List<DateTime?> updateDateList = new List<DateTime?>();

            foreach (WriteOffItemList item in DataList)
            {
                if (!idList.Contains(item.CheckID))
                {
                    idList.Add(item.CheckID);
                    updateDateList.Add(item.CheckUpdateDate);
                }
            }

            try
            {
                ManyResult result = financeService.AuditorWriterOff(
                     idList.ToArray(),
                     updateDateList.ToArray(),
                     LocalData.UserInfo.LoginID,
                     false);

                RefreshDate(result, "UnAuditor");

                RefreshControl();

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "UnAuditor Successfully" : "取消审核成功");

            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        #endregion

        #region 刷新数据/控件
        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="result"></param>
        /// <param name="type"></param>
        private void RefreshDate(ManyResult result, string type)
        {
            foreach (var item in result.Items)
            {
                Guid id = item.GetValue<Guid>("ID");
                DateTime? updateDate = item.GetValue<DateTime?>("UpdateDate");

                List<WriteOffItemList> list = (from d in DataList where d.ID == id select d).ToList();

                foreach (WriteOffItemList writeOffItem in list)
                {
                    switch (type.ToString())
                    {
                        case "Auditor":
                            writeOffItem.UpdateDate = updateDate;
                            writeOffItem.CheckUpdateDate = item.GetValue<DateTime?>("CheckUpdateDate");
                            writeOffItem.VoucherSeqNo = item.GetValue<String>("VoucherSeqNo");
                            writeOffItem.ApprovalByName = LocalData.UserInfo.LoginName;
                            break;
                        case "UnAuditor":
                            writeOffItem.UpdateDate = updateDate;
                            writeOffItem.CheckUpdateDate = item.GetValue<DateTime?>("CheckUpdateDate");
                            writeOffItem.VoucherSeqNo = string.Empty;
                            writeOffItem.ApprovalByName = string.Empty;
                            break;
                        case "Reached":
                            writeOffItem.UpdateDate = updateDate;
                            writeOffItem.BankByName = LocalData.UserInfo.LoginName;
                            writeOffItem.ReachedDate = item.GetValue<DateTime?>("BankDate");
                            writeOffItem.Amount = writeOffItem.FinalAmount;
                            break;
                        case "CancelReached":
                            writeOffItem.UpdateDate = updateDate;
                            writeOffItem.BankByName = string.Empty;
                            writeOffItem.ReachedDate = null;
                            break;
                    }
                }
            }

            bsList.ResetBindings(false);

            if (ListOperating != null)
            {
                ListOperating(type, DataList);
            }

        }

        /// <summary>
        /// 刷新控件
        /// </summary>
        public void RefreshControl()
        {
            if (CurrentRow == null)
            {
                barArrive.Enabled = false;
                barUnArrive.Enabled = false;
                barAuditor.Enabled = false;
                barUnAuditor.Enabled = false;
                barRemove.Enabled = false;
                barClear.Enabled = false;
            }
            else
            {
                barRemove.Enabled = true;
                barClear.Enabled = true;

                ///到账
                if (string.IsNullOrEmpty(CurrentRow.BankByName))
                {
                    barArrive.Enabled = true;
                    barUnArrive.Enabled = false;
                    barAuditor.Enabled = false;
                    barUnAuditor.Enabled = false;

                    colFinalAmount.OptionsColumn.AllowEdit = true;
                    colReachedDate.OptionsColumn.AllowEdit = true;

                }
                else
                {
                    if (string.IsNullOrEmpty(CurrentRow.ApprovalByName))
                    {
                        //未审核
                        barArrive.Enabled = false;
                        barUnArrive.Enabled = true;

                        barAuditor.Enabled = true;
                        barUnAuditor.Enabled = false;
                    }
                    else
                    {
                        //已审核
                        barAuditor.Enabled = false;
                        barUnAuditor.Enabled = true;

                        barArrive.Enabled = false;
                        barUnArrive.Enabled = false;

                    }
                    colFinalAmount.OptionsColumn.AllowEdit = false;
                    colReachedDate.OptionsColumn.AllowEdit = false;
                }


            }
        }
        #endregion

        #region 当前行改变时，刷新工具栏控件的使用
        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            RefreshControl();
        }


        #endregion
    }
}
