using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ICP.FCM.AirExport.ServiceInterface;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using DevExpress.XtraEditors;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.FCM.AirExport.UI.Common;
using Microsoft.Practices.ObjectBuilder;

namespace ICP.FCM.AirExport.UI.Booking
{
    [ToolboxItem(false)]
    public partial class BookingListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IAirExportService aeService { get; set; } 

        [ServiceDependency]
        public ICP.FAM.ServiceInterface.IFinanceClientService finClientService { get; set; }

        [ServiceDependency]
        public ICP.FCM.Common.ServiceInterface.IFCMCommonService fcmCommonService { get; set; }

        [ServiceDependency]
        public ICP.FCM.Common.ServiceInterface.IFCMCommonClientService fcmCommonClientService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public AirExportPrintHelper AirExportPrintHelper { get; set; }

        #endregion

        #region Init

        public BookingListPart()
        {
            InitializeComponent();

            this.Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };
            this.Load += new EventHandler(BookingListPart_Load);

            if (!LocalData.IsEnglish)
            {
                SetCnText();
            }
        }

        bool _shown = false;
        void BookingListPart_Load(object sender, EventArgs e)
        {
            this.DataSource = new List<AirBookingList>();
            
            _shown = true;
        }

        private void SetCnText()
        {
            colState.Caption = "状态";
            colNo.Caption = "业务号";
            colMBLNo.Caption = "主提单号";
            colHBLNo.Caption = "分提单号";
            colFilightNo.Caption = "航班号";
            colCustomerName.Caption = "客户";
            colAirCompany.Caption = "航空公司";
            colAgentOfCarrierName.Caption = "承运人";
            colDeparture.Caption = "起运港";
            colDetination.Caption = "目的港";
            colPlaceOfDelivery.Caption = "交货地";
            colETD.Caption = "起航日";
            colETA.Caption = "到达日";
            this.colShipperName.Caption = "发货人";
            this.colConsigneeName.Caption = "收货人";
            colAgent.Caption = "代理人";
            this.colClosingDate.Caption = "截关日";
            this.colSODate.Caption = "订舱日";
            colCreateDate.Caption = "创建日"; ;
            colSalesName.Caption = "揽货人";
            colBookingerName.Caption = "订舱";
            colFiler.Caption = "文件";
            colBookingCustomerName.Caption= "订舱客户";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }

        private void InitControls()
        {
            //OrderState
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<AEOrderState>> orderStates = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<AEOrderState>(LocalData.IsEnglish);
            foreach (var item in orderStates)
            {
                rcmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
        }

        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }
        protected AirBookingList CurrentRow
        {
            get { return Current as AirBookingList; }
        }

        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                bsList.DataSource = value;
                bsList.ResetBindings(false);



                #region 对一些枚举类型，获取对应语言的描述信息

                //TODO: 装箱拆箱是消耗性能的
                List<AirBookingList> list = value as List<AirBookingList>;

                if (list != null)
                {
                    foreach (AirBookingList item in list)
                    {
                        //item.OEOperationTypeDescription = EnumHelper.GetDescription<OEOperationType>(item.OEOperationType, LocalData.IsEnglish);
                    }
                }

                #endregion


                if (CurrentChanged != null)
                {
                    CurrentChanged(this, Current);
                }

                this.SetColumnsWidth();

                string message = string.Empty;

                if (list.Count > 0)
                {
                    if (LocalData.IsEnglish)
                    {
                        if (list.Count > 1)
                        {
                            message = string.Format("{0} records found", list.Count);
                        }
                        else
                        {
                            message = string.Format("{0} record found", list.Count);
                        }
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

                if (this._shown)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), message);
                }

                if (bsList.List.Count > 0)
                {
                    gvMain.Focus();
                    gvMain.SelectRow(0);
                }
            }
        }

        public override void Refresh(object items)
        {
            List<AirBookingList> list = this.DataSource as List<AirBookingList>;
            if (list == null) return;
            List<AirBookingList> newLists = items as List<AirBookingList>;

            foreach (var item in newLists)
            {
                AirBookingList tager = list.Find(delegate(AirBookingList jItem) { return item.ID == jItem.ID; });
                if (tager == null) continue;
                
                Utility.CopyToValue(item, tager, typeof(AirBookingList));
            }
            bsList.ResetBindings(false);
        }

        public override void RemoveItem(int index)
        {
            bsList.RemoveAt(index);
        }

        public override void RemoveItem(object item)
        {
            bsList.Remove(item);
        }

        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;
        public override event CancelEventHandler CurrentChanging;

        #endregion

        #region GridView Event

        private void bsMainList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        private void gvMain_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            if (CurrentChanging != null)
            {
                CancelEventArgs ce = new CancelEventArgs();
                CurrentChanging(this, ce);
                e.Allow = !ce.Cancel;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            this.pccHyperLinks.Visible = false;
            this.pccHyperLinks.Controls.Clear();

            if (e.Button != MouseButtons.Left) return;

            if ( e.Clicks == 2)
            {
                GvMainDoubleClick();
            }
            else if (e.Clicks == 1)
            {
                #region Show Tip

                if (e.Column.Name == this.colMBLNo.Name)
                {
                    if (this.CurrentRow.AirMBLs != null)
                    {
                        foreach (var item in this.CurrentRow.AirMBLs)
                        {
                            LabelControl hlMBL = new LabelControl();
                            hlMBL.Text = item.NO + " " + EnumHelper.GetDescription<AEBLState>(item.State, LocalData.IsEnglish);
                            hlMBL.AutoSize = true;
                            hlMBL.Tag = item;
                            hlMBL.Click += new EventHandler(hlMBL_Click);
                            hlMBL.Location = new System.Drawing.Point(3, 22 * this.pccHyperLinks.Controls.Count + 3);
                            hlMBL.Font = new Font(FontFamily.GenericSerif, 9F, FontStyle.Underline);
                            hlMBL.ForeColor = Color.Blue;

                            this.pccHyperLinks.Controls.Add(hlMBL);
                        }
                    }
                }
                else if (e.Column.Name == this.colHBLNo.Name)
                {
                    if (this.CurrentRow.AirHBLs != null)
                    {
                        foreach (var item in this.CurrentRow.AirHBLs)
                        {
                            LabelControl hlHBL = new LabelControl();
                            hlHBL.Text = item.NO + " " + EnumHelper.GetDescription<AEBLState>(item.State, LocalData.IsEnglish);
                            hlHBL.AutoSize = true;
                            hlHBL.Tag = item;
                            hlHBL.Click += new EventHandler(hlHBL_Click);
                            hlHBL.Location = new System.Drawing.Point(3, 22 * this.pccHyperLinks.Controls.Count + 3);
                            hlHBL.Font = new Font(FontFamily.GenericSerif, 9F, FontStyle.Underline);
                            hlHBL.ForeColor = Color.Blue;

                            this.pccHyperLinks.Controls.Add(hlHBL);
                        }
                    }
                }

                if (this.pccHyperLinks.Controls.Count > 0)
                {
                    pccHyperLinks.Location = e.Location;
                    pccHyperLinks.Height = pccHyperLinks.Controls[this.pccHyperLinks.Controls.Count - 1].Location.Y
                        + pccHyperLinks.Controls[this.pccHyperLinks.Controls.Count - 1].Height + 5;

                    this.pccHyperLinks.Width = this.pccHyperLinks.Controls.Cast<Control>().Max(o => o.Width) + 10;

                    pccHyperLinks.Show();
                }
                #endregion
            }
        }

        void hlMBL_Click(object sender, EventArgs e)
        {
            LabelControl hlMBL = (LabelControl)sender;
            BookingBLInfo editData = (BookingBLInfo)hlMBL.Tag;
            AirMBLInfo mblInfo = this.aeService.GetAirMBLInfo(editData.ID);
            string title = LocalData.IsEnglish ? "Edit MBL " + editData.NO : "编辑MBL " + editData.NO;
            PartLoader.ShowEditPart<MBL.MAWBEditPart>(Workitem, mblInfo, title, EditPartSaved,
                typeof(MBL.MAWBEditPart).ToString() + editData.ID.ToString());
        }

        void hlHBL_Click(object sender, EventArgs e)
        {
            LabelControl hlHBL = (LabelControl)sender;
            BookingBLInfo editData = (BookingBLInfo)hlHBL.Tag;
            AirHBLInfo hblInfo = this.aeService.GetAirHBLInfo(editData.ID);
            string title = LocalData.IsEnglish ? "Edit HBL " + editData.NO : "编辑HBL " + editData.NO;
            PartLoader.ShowEditPart<HBL.HAWBEditPart>(Workitem, hblInfo, title, EditPartSaved,
                typeof(HBL.HAWBEditPart).ToString() + editData.ID.ToString());
        }

        void hlContainer_Click(object sender, EventArgs e)
        {
            LabelControl hlContainer = (LabelControl)sender;
            BookingContainerInfo editData = (BookingContainerInfo)hlContainer.Tag;
            string title = (LocalData.IsEnglish ? "Container " : "装箱 ") + GetLineNo(CurrentRow);

            IDictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("ContainerId", editData.ID);
            PartLoader.ShowEditPart<Container.LoadContainerPart>(Workitem, this.CurrentRow, dic, title, EditPartSaved,
                AEBookingCommandConstants.Command_LoadContainer + editData.AirBookingID.ToString());
        }

        public new event KeyEventHandler KeyDown;

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GvMainDoubleClick();
            }
            else if (this.KeyDown != null
                && e.KeyCode == Keys.F5
                && this.gvMain.FocusedColumn != null
                && this.gvMain.FocusedValue != null)
            {
                string text = gvMain.GetFocusedDisplayText();//.GetDisplayText(treeMain.FocusedColumn);
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add(gvMain.FocusedColumn.FieldName, text);
                this.KeyDown(keyValue, null);
            }
            else if (e.KeyCode == Keys.F6)
            {
                Workitem.Commands[AEBookingCommandConstants.Command_ShowSearch].Execute();
            }
        }

        protected virtual void GvMainDoubleClick()
        {
            if (CurrentRow != null)
            {
                Workitem.Commands[AEBookingCommandConstants.Command_EditData].Execute();
            }
        }

        private void gvMain_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            AirBookingList list = gvMain.GetRow(e.RowHandle) as AirBookingList;
            if (list == null) return;
            
            if (list.IsValid == false)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Disabled);
            }
            else if (list.State == AEOrderState.NewOrder)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.NewLine);
            }
            else if (list.State == AEOrderState.Checked)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Confirmed);
            }
            else if (list.State == AEOrderState.Rejected)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Error);
            }
        }

        #endregion

        #region Workitem Common

        #region add copy edit Cancel

        [CommandHandler(AEBookingCommandConstants.Command_AddData)]
        public void Command_AddData(object sender, EventArgs e) {
            using (new CursorHelper(Cursors.WaitCursor))
            { 
                AddData(); 
            }
        }
        protected void AddData()
        {
            PartLoader.ShowEditPart<Booking.BookingBaseEditPart>(Workitem, null, string.Empty, EditPartSaved);
        }

        [CommandHandler(AEBookingCommandConstants.Command_CopyData)]
        public void Command_CopyData(object sender, EventArgs e) {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                CopyData();
            }
        }
        protected void CopyData()
        {
            if (CurrentRow == null) return;

            CurrentRow.EditMode = EditMode.Copy;

            PartLoader.ShowEditPart<Booking.BookingBaseEditPart>(Workitem, CurrentRow, string.Empty, EditPartSaved);
        }

        [CommandHandler(AEBookingCommandConstants.Command_EditData)]
        public void Command_EditData(object sender, EventArgs e) {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                EditData();
            }
        }
        protected void EditData()
        {
            if (CurrentRow == null)
            {
                return;
            }

            CurrentRow.EditMode = EditMode.Edit;

            PartLoader.ShowEditPart<Booking.BookingBaseEditPart>(Workitem, CurrentRow, string.Empty, EditPartSaved,
                AEBookingCommandConstants.Command_EditData + CurrentRow.ID.ToString());
        }

        static public  string GetLineNo(AirBookingList current)
        {
            if (current == null || string.IsNullOrEmpty(current.No))
            {
                return string.Empty;
            }
            else if (current.No.Length <= 4)
            {
                return (LocalData.IsEnglish ? ":" : "：") + current.No;
            }
            else
            {
                return (LocalData.IsEnglish ? ":" : "：") + current.No.Substring(current.No.Length - 4, 4);
            }
        }

        void EditPartSaved(object[] prams)
        {
            if (prams == null || prams.Length == 0)
            {
                return;
            }

            AirBookingList data = prams[0] as AirBookingList;

            if (data == null)
            {
                return;
            }

            List<AirBookingList> source = this.bsList.DataSource as List<AirBookingList>;

            if (prams.Length == 1)
            {
                if (source == null || source.Count == 0)
                {
                    bsList.Insert(0, data);
                    bsList.ResetBindings(false);
                }
                else
                {
                    AirBookingList tager = source.Find(delegate(AirBookingList item) { return item.ID == data.ID; });
                    if (tager == null)
                    {
                        bsList.Insert(0, data);
                        bsList.ResetBindings(false);
                    }
                    else
                    {
                        List<BookingBLInfo> _mbls = new List<BookingBLInfo>();
                        tager.AirMBLs.ForEach(o => _mbls.Add(o));
                        List<BookingBLInfo> _hbls = new List<BookingBLInfo>();
                        tager.AirHBLs.ForEach(o => _hbls.Add(o));

                        Utility.CopyToValue(data, tager, typeof(AirBookingList));

                        tager.AirMBLs = _mbls.ToList();
                        tager.AirHBLs = _hbls.ToList();

                        tager.MBLNo = string.Empty;
                        tager.HBLNo = string.Empty;

                        _mbls.ForEach(o => tager.MBLNo = tager.MBLNo + ((tager.MBLNo.Length > 0 ? "," : string.Empty) + o.NO));
                        _hbls.ForEach(o => tager.HBLNo = tager.HBLNo + ((tager.HBLNo.Length > 0 ? "," : string.Empty) + o.NO));

                        bsList.ResetItem(bsList.IndexOf(tager));
                    }
                }
            }
            else
            {
                //传来的参数是订舱当和箱列表
                AirBookingList tager = source.Find(delegate(AirBookingList item) { return item.ID == data.ID; });
                if (tager != null)
                {

                }
            }

            if (CurrentChanged != null)
            {
                CurrentChanged(this, CurrentRow);
            }

            this.SetColumnsWidth();
        }

        void SetColumnsWidth()
        {
            gvMain.BestFitColumns();
        }
        #region 作废/恢复
        [CommandHandler(AEBookingCommandConstants.Command_CancelData)]
        public void Command_CancelData(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            bool isValid = CurrentRow.IsValid;
            string message = string.Empty;
            if (isValid)
                message = LocalData.IsEnglish ? "Srue Cancel Current Booking?" : "你真的要取消这笔订舱单吗?";
            else
                message = LocalData.IsEnglish ? "Srue Available Current Booking?" : "你真的要恢复这笔订舱单吗?";

            string failureMessage = string.Empty;

            if (isValid)
                failureMessage = LocalData.IsEnglish ? "Cancel Booking State Failed." : "取消订舱单失败.";
            else
                failureMessage = LocalData.IsEnglish ? "Available Booking State Failed." : "恢复订舱单失败.";


            DialogResult dialogResult = XtraMessageBox.Show(message,
                                                LocalData.IsEnglish ? "Tip" : "提示",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    List<AirBookingFeeList> info = this.aeService.GetAirOrderFeeList(CurrentRow.ID);
                    if (info.Count > 0)
                    {
                        message = LocalData.IsEnglish ? 
                            "The order has bring fees. It can not be cancelled." 
                            : 
                            "这笔订舱单不能取消，因为操作部门已经开始做单并产生了费用！";

                        XtraMessageBox.Show(message,
                            LocalData.IsEnglish ? "Warning" : "警告",
                             MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);

                        return;
                    }
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), failureMessage + ex.Message);                
                }

                try
                {
                    SingleResult result = aeService.CancelAirOrder(CurrentRow.ID, 
                        isValid, 
                        LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);

                    AirBookingList currentRow = CurrentRow;
                    currentRow.IsValid = !isValid;
                    currentRow.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    bsList.ResetCurrentItem();

                    if (isValid)
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Cancel Booking Successfully." : "这笔订舱单已经成功取消.");
                    else
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Available Booking Successfully." : "这笔订舱单已经成功恢复.");

                    if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), failureMessage + ex.Message);
                }
            }
        }
        #endregion

        #endregion

        #region print

        [CommandHandler(AEBookingCommandConstants.Command_PrintOrder)]
        public void Command_PrintOrder(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null) return;

                try
                {
                    AirExportPrintHelper.PrintAEOrder(CurrentRow.ID);
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                }
            }
        }

        //[CommandHandler(AEBookingCommandConstants.Command_PrintBookingConfirm)]
        //public void Command_PrintBookingConfirm(object sender, EventArgs e)
        //{
        //    if (CurrentRow == null || CurrentRow.IsNew) return;

        //    try
        //    {
        //        AirExportPrintHelper.PrintOEBookingConfirmation(CurrentRow.ID);
        //    }
        //    catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); }
        //}

        //[CommandHandler(AEBookingCommandConstants.Command_PrintInWarehouse)]
        //public void Command_PrintInWarehouse(object sender, EventArgs e)
        //{
        //    if (CurrentRow == null || CurrentRow.IsNew) return;

            //try
            //{
            //    AirExportPrintHelper.PrintOEBookingConfirmation(CurrentRow.ID);
            //}
            //catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); }
        //}

        #endregion

        #region Truck

        [CommandHandler(AEBookingCommandConstants.Command_Truck)]
        public void Command_Truck(object sender, EventArgs e)
        {
            if (CurrentRow == null || CurrentRow.IsNew)
            {
                return;
            }




       

        }

        #endregion

        #region RefreshData

        [CommandHandler(AEBookingCommandConstants.Command_RefreshData)]
        public void Command_RefreshData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    List<AirBookingList> blList = DataSource as List<AirBookingList>;
                    if (blList == null || blList.Count == 0) return;

                    List<Guid> ids = new List<Guid>();
                    foreach (var item in blList)
                    {
                        ids.Add(item.ID);
                    }

                    List<AirBookingList> list = aeService.GetAirBookingListByIds(ids.ToArray());
                    this.DataSource = list;
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
            }
        }
        #endregion

        #region other

        [CommandHandler(AEBookingCommandConstants.Command_ReplyAgent)]
        public void Command_ReplyAgent(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null) return;

                try
                {
                    fcmCommonClientService.OpenAgentRequestPart(CurrentRow.ID, ICP.Framework.CommonLibrary.Common.OperationType.AirExport, Workitem);
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                }
            }
        }

        [CommandHandler(AEBookingCommandConstants.Command_E_Booking)]
        public void Command_E_Booking(object sender, EventArgs e)
        {

        }
        //提单
        [CommandHandler(AEBookingCommandConstants.Command_BL)]
        public void Command_BL(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null) return;
                BL.AEBLWorkitem blWorkitem = Workitem.WorkItems.AddNew<BL.AEBLWorkitem>();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("RefNo", CurrentRow.No);
                blWorkitem.Init(dic);
                blWorkitem.Run();
            }
        }

        //账单
        [CommandHandler(AEBookingCommandConstants.Command_Bill)]
        public void Command_Bill(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null) return;
                OperationCommonInfo operationCommonInfo = fcmCommonService.GetOperationCommonInfo(CurrentRow.ID, OperationType.AirExport);
                if (operationCommonInfo != null)
                {
                    finClientService.ShowBillList(operationCommonInfo, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
                }
                else
                {
                    Utility.ShowMessage(LocalData.IsEnglish ? @"No found,Please contact the system administrator" : @"无对应的数据,请联系系统管理员!");
                }
            }

        }

        #endregion

        /// <summary>
        /// 弹出MBL/HBL/箱号的超链接面板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_MouseEnter(object sender, EventArgs e)
        {
        }

        private void gvMain_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }

        #region Warehouse Customs\\暂时不做


        #endregion

        #endregion
    }
}
