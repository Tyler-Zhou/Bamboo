using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.Service;

namespace ICP.FAM.UI.TelexApply
{
    [ToolboxItem(false)]
    public partial class TelexApplyListPart : BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        #endregion

        #region 初始化

        public TelexApplyListPart()
        {
            InitializeComponent();
           
            Disposed += delegate {
                CurrentChanged = null;
                CurrentChanging = null;
                KeyDown = null;
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
            RegisterMessage("1108190003", LocalData.IsEnglish ? "Are you sure to invalidate the selected time?" : "确认要作废该数据");
            RegisterMessage("1108190004", LocalData.IsEnglish ? "Are you sure to resume the selected time?" : "确认要激活该数据");
        }
        #endregion

        #region event

        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null)
            {
                CurrentChanged(this, Current);
            }
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

        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            TelexApplyList list = gvMain.GetRow(e.RowHandle) as TelexApplyList;
            if (list == null) return;

            if (list.IsValid == false)
            {
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.Disabled);
            }

        }
        #endregion

        #region 命令

        [CommandHandler(TelexApplyCommondConstants.Commond_Add)]
        public void Commond_Add(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                TelexApplyInfo telex = new TelexApplyInfo();
                telex.CreateByID = LocalData.UserInfo.LoginID;
                telex.CreateByName = LocalData.UserInfo.LoginName;
                telex.ApplyTime = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
                telex.ValidDate = new DateTime(DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).Year, 12, 31);
                telex.IsValid = true;
                telex.CompanyId = LocalData.UserInfo.DefaultCompanyID;

                gvMain.ClearSorting();
                bsList.Add(telex);
                gvMain.MoveLast();

                telex.BeginEdit();
            }
        }

        public override void Refresh(object items)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (items == null)
                {
                    return;
                }

                object[] paras = (object[])items;

                TelexApplyList data = paras[0] as TelexApplyList;

                if (data == null)
                {
                    return;
                }

                List<TelexApplyList> source = bsList.DataSource as List<TelexApplyList>;


                if (source == null || source.Count == 0)
                {
                    bsList.DataSource = new List<TelexApplyList>();
                    bsList.Insert(0, data);
                    bsList.ResetBindings(false);
                }
                else
                {
                    TelexApplyList tager = source.Find(delegate(TelexApplyList item) { return item.ID == data.ID; });
                    if (tager == null)
                    {
                        bsList.Insert(0, data);
                        bsList.ResetBindings(false);
                    }
                    else
                    {
                        FAMUtility.CopyToValue(data, tager, typeof(TelexApplyList));

                        bsList.ResetItem(bsList.IndexOf(tager));
                    }
                }
            }
        }

        void AfterBatchAddBill(object[] prams)
        {
            //if (prams == null || prams.Length == 0) return;
            //List<Guid> needUpdateIDs = prams[0] as List<Guid>;
            //List<BusinessList> source = this.DataSource as List<BusinessList>;
            //if (source == null || source.Count == 0)
            //{
            //    source = new List<BusinessList>();
            //    List<BusinessList> updated = finService.GetBusinessList(needUpdateIDs.ToArray());
            //    source.AddRange(updated);
            //    bsList.DataSource = source;
            //    bsList.ResetBindings(false);
            //}
            //else
            //{
            //    List<BusinessList> tagers = source.FindAll(delegate(BusinessList item) { return needUpdateIDs.Contains (item.ID); });
            //    if (tagers != null && tagers.Count > 0)
            //    {
            //        foreach (var item in tagers)
            //        {
            //            source.Remove(item);
            //        }
            //    }

            //    List<BusinessList> updated = finService.GetBusinessList(needUpdateIDs.ToArray());
            //    source.InsertRange(0,updated);
            //    bsList.DataSource = source;
            //    bsList.ResetBindings(false);
            //}
            //if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
        }


        [CommandHandler(TelexApplyCommondConstants.Commond_Cancel)]
        public void Commond_Delete(object sender, EventArgs e)
        {
            if (CurrentRow != null)
            {
                string failureMessage = string.Empty;
                if (CurrentRow.IsValid)
                    failureMessage = LocalData.IsEnglish ? "Cancel Booking State Failed." : "作废总电放失败.";
                else
                    failureMessage = LocalData.IsEnglish ? "Available Booking State Failed." : "激活总电放失败.";

                try
                {

                    if (CurrentRow.IsValid)
                    {
                        if (!FAMUtility.ShowResultMessage(NativeLanguageService.GetText(this, "1108190003")))
                        {
                            return;
                        }

                    }
                    else
                    {
                        if (!FAMUtility.ShowResultMessage(NativeLanguageService.GetText(this, "1108190004")))
                        {
                            return;
                        }
                    }


                    SingleResult result = FinanceService.ChangeTelexRequestValidity(CurrentRow.ID,
                        CurrentRow.IsValid,
                        LocalData.UserInfo.LoginID,
                        CurrentRow.UpdateDate);

                    CurrentRow.IsValid = !CurrentRow.IsValid;
                    CurrentRow.UpdateDate = result.GetValue<DateTime?>("UpdateDate");

                    if (CurrentChanged != null)
                    {
                        CurrentChanged(this,CurrentRow);
                    }

                    if (!CurrentRow.IsValid)
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Cancel Booking Successfully." : "总电放已经成功作废.");
                    else
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Available Booking Successfully." : "总电放已经成功激活.");

                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), failureMessage + ex.Message);
                }
            }
        }
       
        [CommandHandler(TelexApplyCommondConstants.Commond_View )]
        public void Commond_View(object sender, EventArgs e)
        {
            //List<BusinessList> source = bsList.DataSource as List<BusinessList>;
            //if (source == null || source.Count == 0) return;

            //List<BusinessList> selected = new List<BusinessList>();
            //foreach (var item in source)
            //{
            //    if (item.Selected) selected.Add(item);
            //}

            //if (selected.Count == 0) return;

            //string title = LocalData.IsEnglish ? "Batch Bill" : "批量帐单";
            //PartLoader.ShowEditPart<BatchAddBillPart>(Workitem, selected, title, AfterBatchAddBill);
        }

        


        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }
        protected TelexApplyList CurrentRow
        {
            get { return Current as TelexApplyList; }
        }

        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {

                List<TelexApplyList> list = value as List<TelexApplyList>;

                bsList.DataSource = list;

                bsList.ResetBindings(false);
                if (CurrentChanged != null)
                {
                    CurrentChanged(this, Current);
                }
                gvMain.BestFitColumns();


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
            bsList.RemoveAt(index);
        }

        public override void RemoveItem(object item)
        {
            bsList.Remove(item);
        }

        #endregion

        #region 热键

        public new event KeyEventHandler KeyDown;

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
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
                Workitem.Commands[TelexApplyCommondConstants.Command_ShowSearch].Execute();
            }

        }

        #endregion

 

    }
}
