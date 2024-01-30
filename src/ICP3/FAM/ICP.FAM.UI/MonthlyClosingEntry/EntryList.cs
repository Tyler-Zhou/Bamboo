using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;

namespace ICP.FAM.UI.MonthlyClosingEntry
{
    public partial class EntryList : BaseListPart
    {
        private EntryEdit entryEdit = null;
        public EntryList()
        {
            InitializeComponent();
            Disposed += delegate
            {
                gcMain.DataSource = null;
                bsEntries.CurrentChanged -= bsEntries_CurrentChanged;
                bsEntries.PositionChanged -= bsEntries_PositionChanged;
                bsEntries.DataSource = null;
                bsEntries.Dispose();
                CurrentChanged = null;
                CurrentChanging = null;
                KeyDown = null;
                if (entryEdit != null)
                {
                    entryEdit = null;
                }
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }

            };
        }

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        #region IListPart 成员

        public override object Current
        {
            get { return bsEntries.Current; }
        }

        protected MonthlyClosingEntryList CurrentRow
        {
            get { return Current as MonthlyClosingEntryList; }
        }

        public override object DataSource
        {
            get
            {
                return bsEntries.DataSource;
            }
            set
            {

                List<MonthlyClosingEntryList> list = value as List<MonthlyClosingEntryList>;

                gvMain.BeginUpdate();

                bsEntries.DataSource = list;

                bsEntries.ResetBindings(false);
                if (CurrentChanged != null)
                {
                    CurrentChanged(this, Current);
                }
                gvMain.BestFitColumns();

                gvMain.EndUpdate();

                string message = string.Empty;

                if (list.Count > 0)
                {
                    if (LocalData.IsEnglish)
                    {
                        message = string.Format("{0} record found", list.Count);
                    }
                    else
                    {
                        message = string.Format("查询到 {0} 条记录", list.Count);
                    }
                }
                else
                {
                    message = LocalData.IsEnglish ? "Nothing found!" : "没有查询到任何结果。";
                }

                if (list.Count.ToString().Length == 1)
                {
                    gvMain.IndicatorWidth = 30;
                }
                else
                {
                    gvMain.IndicatorWidth = list.Count.ToString().Length * 17;
                }
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), message);
            }
        }

        public override event CurrentChangedHandler CurrentChanged;

        public override event CancelEventHandler CurrentChanging;
        public override void RemoveItem(int index)
        {
            bsEntries.RemoveAt(index);
        }

        public override void RemoveItem(object item)
        {
            bsEntries.Remove(item);
        }

        #endregion

        #region 初始化

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                InitControls();
                InitMeesage();
            }


        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            FAMUtility.ShowGridRowNo(gvMain);
        }

        /// <summary>
        /// 初始化消息内容
        /// </summary>
        private void InitMeesage()
        {
            RegisterMessage("1108190001", LocalData.IsEnglish ? "Are you sure to invalidate the selected time?" : "确认要作废该数据");
            RegisterMessage("1108190002", LocalData.IsEnglish ? "Are you sure to resume the selected time?" : "确认要激活该数据");
        }
        #endregion

        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            //if (e.RowHandle < 0) return;
            //MonthlyClosingEntryList list = gvMain.GetRow(e.RowHandle) as MonthlyClosingEntryList;
            //if (list == null) return;

            //if (list.IsValid == false)
            //{
            //    ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Disabled);
            //}
        }


        public override void Refresh(object items)
        {
            if (items == null)
            {
                return;
            }

            object[] paras = (object[])items;

            MonthlyClosingEntryList data = paras[0] as MonthlyClosingEntryList;

            if (data == null)
            {
                return;
            }

            List<MonthlyClosingEntryList> source = bsEntries.DataSource as List<MonthlyClosingEntryList>;


            if (source == null || source.Count == 0)
            {
                bsEntries.DataSource = new List<MonthlyClosingEntryList>();
                bsEntries.Insert(0, data);
                bsEntries.ResetBindings(false);
            }
            else
            {
                MonthlyClosingEntryList tager = source.Find(delegate(MonthlyClosingEntryList item) { return item.ID == data.ID; });
                if (tager == null)
                {
                    bsEntries.Insert(0, data);
                    bsEntries.ResetBindings(false);
                }
                else
                {
                    FAMUtility.CopyToValue(data, tager, typeof(MonthlyClosingEntryList));

                    bsEntries.ResetCurrentItem();
                }
            }

            bsEntries.ResetBindings(false);

            if(CurrentChanged!=null)
            {
                CurrentChanged(this, CurrentRow);
            }
        }

        private void bsEntries_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void gvMain_BeforeLeaveRow(object sender, RowAllowEventArgs e)
        {
            if (CurrentChanging != null)
            {
                CancelEventArgs cancel = new CancelEventArgs();
                CurrentChanging(sender, cancel);

                if (cancel.Cancel)
                {
                    e.Allow = false;
                }
            }
        }

        [CommandHandler(EntryCommondConstants.Commond_Add)]
        public void Commond_Add(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                MonthlyClosingAgreement entry = new MonthlyClosingAgreement();
                entry.CustomerTypes = "BillCustomer";
                gvMain.ClearSorting();
                bsEntries.Add(entry);
                gvMain.MoveLast();
            }
        }

        [CommandHandler(EntryCommondConstants.Commond_Delete)]
        public void Command_Delete(object sender, EventArgs e)
        {
            //if (CurrentRow != null)
            //{
            //    string failureMessage = string.Empty;
            //    if (CurrentRow.IsValid)
            //        failureMessage = LocalData.IsEnglish ? "Cancel Booking State Failed." : "作废月结协议失败.";
            //    else
            //        failureMessage = LocalData.IsEnglish ? "Available Booking State Failed." : "激活月结协议失败.";

            //    try
            //    {
            //        if (CurrentRow.IsValid)
            //        {
            //            if (!Utility.ShowResultMessage(NativeLanguageService.GetText(this, "1108190001")))
            //            {
            //                return;
            //            }

            //        }
            //        else
            //        {
            //            if (!Utility.ShowResultMessage(NativeLanguageService.GetText(this, "1108190002")))
            //            {
            //                return;
            //            }
            //        }

            //        SingleResult result = FinanceService.ChangeMonthlyClosingEntryValidity(CurrentRow.ID,
            //            CurrentRow.IsValid,
            //            LocalData.UserInfo.LoginID,
            //            CurrentRow.UpdateDate);

            //        CurrentRow.IsValid = !CurrentRow.IsValid;
            //        CurrentRow.UpdateDate = result.GetValue<DateTime?>("UpdateDate");


            //        if (CurrentChanged != null)
            //        {
            //            CurrentChanged(this, Current);
            //        }

            //        if (!CurrentRow.IsValid)
            //            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Cancel Booking Successfully." : "月结协议已经成功作废.");
            //        else
            //            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Available Booking Successfully." : "月结协议已经成功激活.");

            //    }
            //    catch (Exception ex)
            //    {
            //        LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), failureMessage + ex.Message);
            //    }
            //}
        }

        #region 热键
        public new event KeyEventHandler KeyDown;
        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (KeyDown != null
                && e.KeyCode == Keys.F5
                && gvMain.FocusedColumn != null
                && gvMain.FocusedValue != null)
                {
                    string text = gvMain.GetFocusedDisplayText();
                    Dictionary<string, object> keyValue = new Dictionary<string, object>();
                    keyValue.Add(gvMain.FocusedColumn.FieldName, text);
                    KeyDown(keyValue, null);
                }


                if (e.KeyCode == Keys.F6 && CurrentRow != null)
                {
                    Workitem.Commands[EntryCommondConstants.Command_ShowSearch].Execute();
                }
            }
        }

        #endregion

        private void bsEntries_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null)
            {
                CurrentChanged(this, Current);
            }
        }


    }
}
