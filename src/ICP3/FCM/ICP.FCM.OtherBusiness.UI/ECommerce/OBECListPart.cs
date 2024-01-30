using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.FAM.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.ServiceInterface;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
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

namespace ICP.FCM.OtherBusiness.UI.ECommerce
{
    /// <summary>
    /// 电商物流-列表
    /// </summary>
    [ToolboxItem(false)]
    public partial class OBECListPart : BaseListPart
    {
        #region Fields & Property
        /// <summary>
        /// 
        /// </summary>
        bool _Shown = false;
        /// <summary>
        /// 
        /// </summary>
        protected OtherBusinessList CurrentRow
        {
            get { return Current as OtherBusinessList; }
        }
        #endregion

        #region 服务注入
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public OtherBusinessPrintHelper OtherBusinessPrintHelper
        {
            get
            {
                return ClientHelper.Get<OtherBusinessPrintHelper, OtherBusinessPrintHelper>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IOtherBusinessService OtherBusinessService
        {
            get
            {
                return ServiceClient.GetService<IOtherBusinessService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IFinanceClientService FinanceClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFinanceClientService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public OtherUIHelper ExportUIHelper
        {
            get
            {
                return ClientHelper.Get<OtherUIHelper, OtherUIHelper>();
            }
        }
        /// <summary>
        /// 其他业务
        /// </summary>
        public IClientOtherBusinessService ClientOtherBusiness
        {
            get
            {
                return ServiceClient.GetClientService<IClientOtherBusinessService>();
            }
        }
        #endregion

        #region Init
        /// <summary>
        /// 电商物流-列表
        /// </summary>
        public OBECListPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                CurrentChanged = null;
                CurrentChanging = null;
                gcMain.DataSource = null;
                bsOBList.PositionChanged -= bsOBList_PositionChanged;
                bsOBList.DataSource = null;
                bsOBList.Dispose();

                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };

            if (!LocalData.IsEnglish)
            {
                SetCnText();

            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
            DataSource = new List<OtherBusinessList>();
            _Shown = true;
        }

        private void InitControls()
        {
            Utility.ShowGridRowNo(gvMain);
            //订单状态
            List<EnumHelper.ListItem<OBOrderState>> orderStates = EnumHelper.GetEnumValues<OBOrderState>(LocalData.IsEnglish);
            foreach (var item in orderStates)
            {
                rcmState.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
        }
        #endregion

        #region IListPart成员
        /// <summary>
        /// 
        /// </summary>
        public override event CurrentChangedHandler CurrentChanged;
        /// <summary>
        /// 
        /// </summary>
        public override event CancelEventHandler CurrentChanging;

        public override object Current
        {
            get { return bsOBList.Current; }
        }

        public override object DataSource
        {
            get
            {
                return bsOBList.DataSource;
            }
            set
            {
                string message = string.Empty;
                message = LocalData.IsEnglish ? "0 records found" : "查询到0条记录";
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), message);
                bsOBList.DataSource = value;
                List<OtherBusinessList> list = value as List<OtherBusinessList>;
                if (list == null || list.Count == 0)
                {
                    bsOBList.DataSource = typeof(OtherBusinessList);
                }

                else
                {
                    if (CurrentChanged != null)
                    {
                        CurrentChanged(this, Current);
                    }

                    SetColumnsWidth();


                    if (_Shown)
                    {
                        if (LocalData.IsEnglish) { message = string.Format("{0} records found", list.Count); }
                        else { message = string.Format("查询到 {0} 条记录", list.Count); }
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), message);
                        bsOBList.DataSource = list;
                        bsOBList.ResetBindings(false);
                    }
                }
            }
        }
        #endregion

        #region BaseListPart
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="items"></param>
        public override void Refresh(object items)
        {
            List<OtherBusinessList> list = DataSource as List<OtherBusinessList>;
            if (list == null) return;
            List<OtherBusinessList> newLists = items as List<OtherBusinessList>;

            if (newLists != null)
                foreach (var item in newLists)
                {
                    OtherBusinessList tager = list.Find(jItem => item.ID == jItem.ID);
                    if (tager == null) continue;
                    Utility.CopyToValue(item, tager, typeof(OtherBusinessList));
                }
            bsOBList.ResetBindings(false);
        }
        /// <summary>
        /// 移除项
        /// </summary>
        /// <param name="index"></param>
        public override void RemoveItem(int index)
        {
            bsOBList.RemoveAt(index);
        }
        /// <summary>
        /// 移除项
        /// </summary>
        /// <param name="item"></param>
        public override void RemoveItem(object item)
        {
            bsOBList.Remove(item);
        }
        #endregion

        #region GRID EVENTS
        private void gvMain_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && CurrentRow != null)
                Workitem.Commands[OBECCommandConstants.Command_EditData].Execute();
        }
        /// <summary>
        /// 按键事件
        /// </summary>
        public new event KeyEventHandler KeyDown;

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && CurrentRow != null)
            {
                Workitem.Commands[OBECCommandConstants.Command_EditData].Execute();
            }
            else if (KeyDown != null
                 && e.KeyCode == Keys.F5
                 && gvMain.FocusedColumn != null
                 && gvMain.FocusedValue != null)
            {
                string text = gvMain.GetFocusedDisplayText();//.GetDisplayText(treeMain.FocusedColumn);
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add(gvMain.FocusedColumn.FieldName, text);
                KeyDown(keyValue, null);
            }
            else if (e.KeyCode == Keys.F6)
            {
                Workitem.Commands[OBECCommandConstants.Command_ShowSearch].Execute();
            }
        }

        private void gvMain_DoubleClick(object sender, EventArgs e)
        {
            if (CurrentRow != null)
            {
                Workitem.Commands[OBECCommandConstants.Command_EditData].Execute();
            }
        }

        private void gvMain_BeforeLeaveRow(object sender, RowAllowEventArgs e)
        {
            if (CurrentChanging != null)
            {
                CancelEventArgs ce = new CancelEventArgs();
                CurrentChanging(this, ce);
                e.Allow = !ce.Cancel;
            }
        }

        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            OtherBusinessList list = gvMain.GetRow(e.RowHandle) as OtherBusinessList;
            if (list == null) return;

            if (list.IsValid == false)
            {
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.Disabled);
            }
            else if (list.State == OBOrderState.NewOrder)
            {
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.NewLine);
            }
            else if (list.State == OBOrderState.MBLConfirmed)
            {
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.Error);
            }
        }

        private void bsOBList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prams"></param>
        void EditPartSaved(object[] prams)
        {
            if (prams == null || prams.Length == 0) return;

            OtherBusinessInfo data = prams[0] as OtherBusinessInfo;
            if (data == null) return;
            List<OtherBusinessList> source = DataSource as List<OtherBusinessList>;
            if (source == null || source.Count == 0)
            {
                bsOBList.Add(data);
                bsOBList.ResetBindings(false);
            }
            else
            {
                OtherBusinessList tager = source.Find(item => item.ID == data.ID);
                if (tager == null)
                {
                    bsOBList.Insert(0, data);
                    bsOBList.ResetBindings(false);
                }
                else
                {
                    Utility.CopyToValue(data, tager, typeof(OtherBusinessList));
                    bsOBList.ResetItem(bsOBList.IndexOf(tager));
                }
            }
            if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
            SetColumnsWidth();
        }


        #endregion

        #region Style
        /// <summary>
        /// 绘制行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }
        #endregion

        #region Command Event

        #region 新增
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [CommandHandler(OBECCommandConstants.Command_AddData)]
        public void Command_AddData(object sender, EventArgs args)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                ClientOtherBusiness.ECommerceAddData(null,EditPartSaved);
            }
        }
        #endregion

        #region 复制
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OBECCommandConstants.Command_CopyData)]
        public void Command_CopyData(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            using (new CursorHelper(Cursors.WaitCursor))
            {

                ClientOtherBusiness.ECommerceCopyData(CurrentRow.ID, CurrentRow.CompanyID, EditPartSaved);
            }
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OBECCommandConstants.Command_EditData)]
        public void Command_EditData(object sender, EventArgs e)
        {

            if (CurrentRow == null || CurrentRow.IsNew) return;
            using (new CursorHelper(Cursors.WaitCursor))
            {
                ClientOtherBusiness.ECommerceEditData(CurrentRow.ID, CurrentRow.CompanyID, EditPartSaved);
            }
        }

        #endregion

        #region 刷新
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OBECCommandConstants.Command_RefreshData)]
        public void Command_RefreshData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    List<OtherBusinessList> blList = DataSource as List<OtherBusinessList>;
                    if (blList == null || blList.Count == 0) return;//无数据则返回

                    List<Guid> ids = new List<Guid>();
                    List<Guid> companyids = new List<Guid>();
                    foreach (var item in blList)
                    {
                        ids.Add(item.ID);
                        companyids.Add(item.CompanyID);
                    }

                    List<OtherBusinessList> list = OtherBusinessService.GetOtherBusinessListById(ids.ToArray(), companyids.ToArray());
                    DataSource = list;
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
            }
        }

        #endregion

        #region 作废
        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OBECCommandConstants.Command_CancelData)]
        public void Command_CancelData(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            try
            {
                bool isCancel = CurrentRow.IsValid;

                string message = string.Empty;
                if (isCancel)
                    message = LocalData.IsEnglish ? "Srue Cancel Current Booking?" : "你真的要取消这笔业务吗?";
                else
                    message = LocalData.IsEnglish ? "Srue Available Current Booking?" : "你真的要恢复这笔业务吗?";

                DialogResult dialogResult = XtraMessageBox.Show(message,
                                                    LocalData.IsEnglish ? "Tip" : "提示",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {

                    SingleResult result = OtherBusinessService.CancelOtherBusiness(CurrentRow.ID, CurrentRow.CompanyID, isCancel, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);

                    OtherBusinessList currentRow = CurrentRow;
                    currentRow.IsValid = !isCancel;
                    currentRow.ID = result.GetValue<Guid>("ID");
                    currentRow.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    bsOBList.ResetCurrentItem();

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Changed  State Successfully" : "更改状态成功");
                    if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), (LocalData.IsEnglish ? "Changed  State Failed" : "更改状态失败") + ex.Message);
            }
        }
        #endregion

        #region 账单
        /// <summary>
        /// 账单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OBECCommandConstants.Command_Bill)]
        public void Command_Bill(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;
            using (new CursorHelper(Cursors.WaitCursor))
            {
                ClientOtherBusiness.Bill(CurrentRow.ID);
            }
        }
        #endregion

        #region 核销单
        /// <summary>
        /// 核销单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        [CommandHandler(OBECCommandConstants.Command_VerifiSheet)]
        public void Command_VerifiSheet(object sender, EventArgs e)
        {

            if (CurrentRow == null || CurrentRow.IsNew) return;
            using (new CursorHelper(Cursors.WaitCursor))
            {
                ClientOtherBusiness.VerifiSheet(CurrentRow.ID, CurrentRow.NO);
            }
        }

        #endregion

        #region 提货通知书
        ///// <summary>
        ///// 提货通知书
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //[CommandHandler(OBECCommandConstants.Command_PickUp)]
        //public void Command_PickUp(object sender, EventArgs e)
        //{

        //    if (CurrentRow == null)
        //    {
        //        return;
        //    }
        //    using (new CursorHelper(Cursors.WaitCursor))
        //    {
        //        string title = LocalData.IsEnglish ? "ReleaseNotify" : "提货通知书";
        //        ExportUIHelper.ShowTruckEdit(CurrentRow.ID, CurrentRow,
        //                Workitem, OtherBusinessService,
        //               GetLineNo(CurrentRow));
        //    }
        //}
        #endregion

        #region 打印
        ///// <summary>
        ///// 打印
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //[CommandHandler(OBECCommandConstants.Command_OpContact)]
        //public void Command_PrintOrder(object sender, EventArgs e)
        //{

        //    if (CurrentRow == null) return;
        //    using (new CursorHelper(Cursors.WaitCursor))
        //    {
        //        OtherBusinessPrintHelper.PrintOBOrder(CurrentRow.ID, CurrentRow.CompanyID);

        //    }
        //}

        //#region 利润表
        ///// <summary>
        ///// 利润表
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //[CommandHandler(OBECCommandConstants.Command_Profit)]
        //public void Command_Profit(object sender, EventArgs e)
        //{

        //    if (CurrentRow == null) return;
        //    using (new CursorHelper(Cursors.WaitCursor))
        //    {
        //        OtherBusinessPrintHelper.PrintOEBookingProfit(CurrentRow);
        //    }
        //}
        //#endregion

        #endregion

        #endregion

        #region Method
        /// <summary>
        /// 设置中文
        /// </summary>
        private void SetCnText()
        {
            colNO.Caption = "业务号";
            colIsValid.Caption = "是否有效";
            colState.Caption = "状态";
            colCustomerName.Caption = "客户";
            colShipperName.Caption = "发货人";
            colConsigneeName.Caption = "收货人";
            colHblno.Caption = "HBL No";
            colMblno.Caption = "MBL No";
            colNotifyPartyName.Caption = "通知人";
            colConsigneeName.Caption = "发货人";
            colShipperName.Caption = "收货人";
            colAgengofCarrierName.Caption = "代理人";
            colPolName.Caption = "起运港";
            colPodName.Caption = "目的港";
        }
        /// <summary>
        /// 设置列宽
        /// </summary>
        void SetColumnsWidth()
        {
            gvMain.BestFitColumns();
        }
        #endregion

    }
}
