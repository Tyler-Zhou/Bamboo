﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FCM.DomesticTrade.ServiceInterface;
using ICP.FCM.DomesticTrade.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.ObjectBuilder;
using DevExpress.XtraEditors;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Common.UI;

namespace ICP.FCM.DomesticTrade.UI.Booking
{
    [ToolboxItem(false)]
    public partial class BookingListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IDomesticTradeService oeService { get; set; }

        [ServiceDependency]
        public ICP.FAM.ServiceInterface.IFinanceClientService finClientService { get; set; }

        [ServiceDependency]
        public ICP.FCM.Common.ServiceInterface.IFCMCommonService fcmCommonService { get; set; }

        [ServiceDependency]
        public ICP.FCM.Common.ServiceInterface.IFCMCommonClientService fcmCommonClientService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ICPCommUIHelper ICPCommUIHelperService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public DomesticTradePrintHelper DomesticTradePrintHelper { get; set; }

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
            this.DataSource = new List<DTBookingList>();
            
            _shown = true;
        }

        private void SetCnText()
        {
            colState.Caption = "状态";
            colNo.Caption = "业务号";
            this.colPlaceOfReceiptName.Caption = "收货地";
            colShippingOrderNo.Caption = "订舱号";
            colCustomerName.Caption = "客户";
            colCarrierName.Caption = "船公司";
            colAgentOfCarrierName.Caption = "承运人";
            colVesselVoyage.Caption = "船名航次";
            colPOLName.Caption = "装货港";
            colPODName.Caption = "卸货港";
            colPlaceOfDeliveryName.Caption = "交货地";
            colETD.Caption = "离港日";
            colETA.Caption = "到港日";
            this.colShipperName.Caption = "发货人";
            this.colConsigneeName.Caption = "收货人";
            this.colSODate.Caption = "订舱日";
            colCreateDate.Caption = "创建日"; ;
            colType.Caption = "业务类型";
            colSalesName.Caption = "揽货人";
            colBookingerName.Caption = "订舱";
            colFiler.Caption = "文件";
            colPODContact.Caption = "港后客服";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }

        private void InitControls()
        {
            //OrderState
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<DTOrderState>> orderStates = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<DTOrderState>(LocalData.IsEnglish);
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
        protected DTBookingList CurrentRow
        {
            get { return Current as DTBookingList; }
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
                List<DTBookingList> list = value as List<DTBookingList>;

                if (list != null)
                {
                    foreach (DTBookingList item in list)
                    {
                        item.OEOperationTypeDescription = EnumHelper.GetDescription<FCMOperationType>(item.DTOperationType, LocalData.IsEnglish);
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
                        if(list.Count > 1)
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
            }
        }

        public override void Refresh(object items)
        {
            List<DTBookingList> list = this.DataSource as List<DTBookingList>;
            if (list == null) return;
            List<DTBookingList> newLists = items as List<DTBookingList>;

            foreach (var item in newLists)
            {
                DTBookingList tager = list.Find(delegate(DTBookingList jItem) { return item.ID == jItem.ID; });
                if (tager == null) continue;
                
                Utility.CopyToValue(item, tager, typeof(DTBookingList));
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

                ////if (e.Column.Name == this.colMBLNo.Name)
                ////{
                ////    if (this.CurrentRow.OceanMBLs != null)
                ////    {
                ////        foreach (var item in this.CurrentRow.OceanMBLs)
                ////        {
                ////            LabelControl hlMBL = new LabelControl();
                ////            hlMBL.Text = item.NO + " " + EnumHelper.GetDescription<BLState>(item.State, LocalData.IsEnglish);
                ////            hlMBL.AutoSize = true;
                ////            hlMBL.Tag = item;
                ////            //hlMBL.Click += new EventHandler(hlMBL_Click);
                ////            hlMBL.Location = new System.Drawing.Point(3, 22 * this.pccHyperLinks.Controls.Count + 3);
                ////            hlMBL.Font = new Font(FontFamily.GenericSerif, 9F, FontStyle.Underline);
                ////            hlMBL.ForeColor = Color.Blue;

                ////            this.pccHyperLinks.Controls.Add(hlMBL);
                ////        }
                ////    }
                //}
                //else if (e.Column.Name == this.colHBLNo.Name)
                //{
                //    if (this.CurrentRow.OceanHBLs != null)
                //    {
                //        foreach (var item in this.CurrentRow.OceanHBLs)
                //        {
                //            LabelControl hlHBL = new LabelControl();
                //            hlHBL.Text = item.NO + " " + EnumHelper.GetDescription<BLState>(item.State, LocalData.IsEnglish);
                //            hlHBL.AutoSize = true;
                //            hlHBL.Tag = item;
                //            //hlHBL.Click += new EventHandler(hlHBL_Click);
                //            hlHBL.Location = new System.Drawing.Point(3, 22 * this.pccHyperLinks.Controls.Count + 3);
                //            hlHBL.Font = new Font(FontFamily.GenericSerif, 9F, FontStyle.Underline);
                //            hlHBL.ForeColor = Color.Blue;

                //            this.pccHyperLinks.Controls.Add(hlHBL);
                //        }
                //    }
                //}
                //else if (e.Column.Name == this.colContainerNo.Name)
                //{
                //    if (this.CurrentRow.BookingContainers != null)
                //    {
                //        foreach (var item in this.CurrentRow.BookingContainers)
                //        {
                //            LabelControl hlContainer = new LabelControl();
                //            hlContainer.Text = item.NO + " " + item.TypeName;
                //            hlContainer.AutoSize = true;
                //            hlContainer.Tag = item;
                //            hlContainer.Click += new EventHandler(hlContainer_Click);
                //            hlContainer.Location = new System.Drawing.Point(3, 22 * this.pccHyperLinks.Controls.Count + 3);
                //            hlContainer.Font = new Font(FontFamily.GenericSerif, 9F, FontStyle.Underline);
                //            hlContainer.ForeColor = Color.Blue;

                //            this.pccHyperLinks.Controls.Add(hlContainer);
                //        }
                //    }
                //}

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

        //void hlMBL_Click(object sender, EventArgs e)
        //{
        //    LabelControl hlMBL = (LabelControl)sender;
        //    BookingBLInfo editData = (BookingBLInfo)hlMBL.Tag;
        //    DTMBLInfo mblInfo = this.oeService.GetOceanMBLInfo(editData.ID);
        //    string title = LocalData.IsEnglish ? "Edit MBL " + editData.NO : "编辑MBL " + editData.NO;
        //    PartLoader.ShowEditPart<MBL.MBLEditPart>(Workitem, mblInfo, title, EditPartSaved,
        //        typeof(MBL.MBLEditPart).ToString() + editData.ID.ToString());
        //}

        //void hlHBL_Click(object sender, EventArgs e)
        //{
        //    LabelControl hlHBL = (LabelControl)sender;
        //    BookingBLInfo editData = (BookingBLInfo)hlHBL.Tag;
        //    DTHBLInfo hblInfo = this.oeService.GetOceanHBLInfo(editData.ID);
        //    string title = LocalData.IsEnglish ? "Edit HBL " + editData.NO : "编辑HBL " + editData.NO;
        //    PartLoader.ShowEditPart<HBL.HBLEditPart>(Workitem, hblInfo, title, EditPartSaved,
        //        typeof(HBL.HBLEditPart).ToString() + editData.ID.ToString());
        //}

        void hlContainer_Click(object sender, EventArgs e)
        {
            LabelControl hlContainer = (LabelControl)sender;
            BookingContainerInfo editData = (BookingContainerInfo)hlContainer.Tag;
            string title = (LocalData.IsEnglish ? "Container " : "装箱 ") + GetLineNo(CurrentRow);

            IDictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("ContainerId", editData.ID);
            PartLoader.ShowEditPart<Container.LoadContainerPart>(Workitem, this.CurrentRow, dic, title, EditPartSaved,
                DTBookingCommandConstants.Command_LoadContainer + editData.DTBookingID.ToString());
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
                Workitem.Commands[DTBookingCommandConstants.Command_ShowSearch].Execute();
            }
        }

        protected virtual void GvMainDoubleClick()
        {
            if (CurrentRow != null)
            {
                Workitem.Commands[DTBookingCommandConstants.Command_EditData].Execute();
            }
        }

        private void gvMain_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            DTBookingList list = gvMain.GetRow(e.RowHandle) as DTBookingList;
            if (list == null) return;
            
            if (list.IsValid == false)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Disabled);
            }
            else if (list.State == DTOrderState.NewOrder)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.NewLine);
            }
            else if (list.State == DTOrderState.Checked)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Confirmed);
            }
            else if (list.State == DTOrderState.Rejected)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Error);
            }
        }

        #endregion

        #region Workitem Common

        #region add copy edit Cancel

        [CommandHandler(DTBookingCommandConstants.Command_AddData)]
        public void Command_AddData(object sender, EventArgs e) { AddData(); }
        protected void AddData()
        {
            PartLoader.ShowEditPart<Booking.BookingBaseEditPart>(Workitem, null, string.Empty, EditPartSaved);
        }

        [CommandHandler(DTBookingCommandConstants.Command_CopyData)]
        public void Command_CopyData(object sender, EventArgs e) { CopyData(); }
        protected void CopyData()
        {
            if (CurrentRow == null) return;

            CurrentRow.EditMode = EditMode.Copy;

            PartLoader.ShowEditPart<Booking.BookingBaseEditPart>(Workitem, CurrentRow, string.Empty, EditPartSaved);
        }

        [CommandHandler(DTBookingCommandConstants.Command_EditData)]
        public void Command_EditData(object sender, EventArgs e) { EditData(); }
        protected void EditData()
        {
            if (CurrentRow == null)
            {
                return;
            }

            CurrentRow.EditMode = EditMode.Edit;

            PartLoader.ShowEditPart<Booking.BookingBaseEditPart>(Workitem, CurrentRow, string.Empty, EditPartSaved,
                DTBookingCommandConstants.Command_EditData + CurrentRow.ID.ToString());
        }

        static public  string GetLineNo(DTBookingList current)
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

            DTBookingList data = prams[0] as DTBookingList;

            if (data == null)
            {
                return;
            }

            List<DTBookingList> source = this.bsList.DataSource as List<DTBookingList>;
                
            if (prams.Length == 1)
            {
                if (source == null || source.Count == 0)
                {
                    bsList.Insert(0, data);
                    bsList.ResetBindings(false);
                }
                else
                {
                    DTBookingList tager = source.Find(delegate(DTBookingList item) { return item.ID == data.ID; });
                    if (tager == null)
                    {
                        bsList.Insert(0, data);
                        bsList.ResetBindings(false);
                    }
                    else
                    {
                        //List<BookingBLInfo> _mbls = new List<BookingBLInfo>();
                        //tager.OceanMBLs.ForEach(o=>_mbls.Add(o));
                        //List<BookingBLInfo> _hbls = new List<BookingBLInfo>();
                        //tager.OceanHBLs.ForEach(o => _hbls.Add(o));
                        //List<BookingContainerInfo> _ctns = new List<BookingContainerInfo>();
                        //tager.BookingContainers.ForEach(o => _ctns.Add(o));

                        Utility.CopyToValue(data, tager, typeof(DTBookingList));

                        //tager.OceanMBLs = _mbls.ToList();
                        //tager.OceanHBLs = _hbls.ToList();
                        //tager.BookingContainers = _ctns.ToList();

                        //tager.MBLNo = string.Empty;
                        //tager.HBLNo = string.Empty;
                        //tager.ContainerNo = string.Empty;

                        //_mbls.ForEach(o => tager.MBLNo = tager.MBLNo + ((tager.MBLNo.Length > 0 ? "," : string.Empty) + o.NO));
                        //_hbls.ForEach(o => tager.HBLNo = tager.HBLNo + ((tager.HBLNo.Length > 0 ? "," : string.Empty) + o.NO));
                        //_ctns.ForEach(o => tager.ContainerNo = tager.ContainerNo + ((tager.ContainerNo.Length > 0 ? "," : string.Empty) + o.NO));

                        bsList.ResetItem(bsList.IndexOf(tager));
                    }
                }
            }
            else
            {
                //传来的参数是订舱当和箱列表
                DTBookingList tager = source.Find(delegate(DTBookingList item) { return item.ID == data.ID; });
                if (tager != null)
                {
                    List<DTContainerList> containers = prams[1] as List<DTContainerList>;

                    if (containers != null)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (DTContainerList container in containers)
                        {
                            if (tager.BookingContainers == null)
                            {
                                tager.BookingContainers = new List<BookingContainerInfo>();
                            }
                            if (tager.BookingContainers.Exists(o => o.ID == container.ID))
                            {
                                BookingContainerInfo info = tager.BookingContainers.First(o => o.ID == container.ID);
                                info.NO = container.No;
                                info.TypeId = container.TypeID;
                                info.TypeName = container.TypeName;
                            }
                            else
                            {
                                BookingContainerInfo info = new BookingContainerInfo();
                                info.ID = container.ID;
                                info.NO = container.No;
                                info.TypeId = container.TypeID;
                                info.TypeName = container.TypeName;
                                info.DTBookingID = tager.ID;

                                tager.BookingContainers.Add(info);
                            }

                            sb.Append(container.No + ",");
                        }
                        tager.ContainerNo = sb.ToString().TrimEnd(',');
                        bsList.ResetItem(bsList.IndexOf(tager));
                    }
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

        [CommandHandler(DTBookingCommandConstants.Command_CancelData)]
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
                    List<DTBookingFeeList> info = this.oeService.GetDTOrderFeeList(CurrentRow.ID,LocalData.IsEnglish);
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
                    SingleResult result = oeService.CancelDTOrder(CurrentRow.ID, 
                        isValid,
                        LocalData.UserInfo.LoginID, CurrentRow.UpdateDate, LocalData.IsEnglish);

                    DTBookingList currentRow = CurrentRow;
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

        #region print

        [CommandHandler(DTBookingCommandConstants.Command_PrintOrder)]
        public void Command_PrintOrder(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            try
            {
                DomesticTradePrintHelper.PrintOEOrder(CurrentRow.ID);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm() , ex);
            }
        }

        [CommandHandler(DTBookingCommandConstants.Command_PrintBookingConfirm)]
        public void Command_PrintBookingConfirm(object sender, EventArgs e)
        {
            if (CurrentRow == null || CurrentRow.IsNew) return;

            try
            {
                DomesticTradePrintHelper.PrintOEBookingConfirmation(CurrentRow.ID);
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); }
        }

        [CommandHandler(DTBookingCommandConstants.Command_PrintInWarehouse)]
        public void Command_PrintInWarehouse(object sender, EventArgs e)
        {
            if (CurrentRow == null || CurrentRow.IsNew) return;

            //try
            //{
            //    OceanExportPrintHelper.PrintOEBookingConfirmation(CurrentRow.ID);
            //}
            //catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); }
        }

        #endregion

        #region Truck

        [CommandHandler(DTBookingCommandConstants.Command_Truck)]
        public void Command_Truck(object sender, EventArgs e)
        {
            if (CurrentRow == null || CurrentRow.IsNew)
            {
                return;
            }

            List<DTTruckInfo> truckList = oeService.GetDTTruckServiceList(CurrentRow.ID, LocalData.IsEnglish);
            SingleResult recentData = oeService.GetTruckRecentData(CurrentRow.ID, LocalData.IsEnglish);

            Dictionary<string, object> stateValues = new Dictionary<string, object>();
            stateValues.Add("Booking", CurrentRow);

            if (recentData != null)
            {
                stateValues.Add("RecentTruckerID", recentData.GetValue<Guid?>("TruckerID"));
                stateValues.Add("RecentShipperID", recentData.GetValue<Guid?>("ShipperID"));
                stateValues.Add("ReturnLocationID", recentData.GetValue<Guid?>("ReturnLocationID"));
                stateValues.Add("ContainerDescription", SerializerHelper.DeserializeFromString<ContainerDescription>(typeof(ContainerDescription), recentData.GetValue<string>("ContainerDescription")));
                stateValues.Add("CustomsBrokerID", recentData.GetValue<Guid?>("CustomsBrokerID"));
                stateValues.Add("IsDrivingLicence", recentData.GetValue<bool?>("IsDrivingLicence"));
                stateValues.Add("Remark", recentData.GetValue<string>("Remark"));
            }

            string title = LocalData.IsEnglish ? "Truck Service" : "拖车服务";
            PartLoader.ShowEditPart<Booking.DTTruckEditPart>(Workitem, truckList, stateValues, title + GetLineNo(CurrentRow), null,
                Booking.DTBookingCommandConstants.Command_Truck + CurrentRow.ID.ToString());


        }

        #endregion

        #region RefreshData

        [CommandHandler(DTBookingCommandConstants.Command_RefreshData)]
        public void Command_RefreshData(object sender, EventArgs e)
        {
            try
            {
                List<DTBookingList> blList = DataSource as List<DTBookingList>;
                if (blList == null || blList.Count == 0) return;

                List<Guid> ids = new List<Guid>();
                foreach (var item in blList)
                {
                    ids.Add(item.ID);
                }

                List<DTBookingList> list = oeService.GetDTBookingListByIds(ids.ToArray(),LocalData.IsEnglish);
                this.DataSource = list;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }

        #endregion

        #region 装箱

        /// <summary>
        /// 装箱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(DTBookingCommandConstants.Command_LoadContainer)]
        public void Command_LoadContainer(object sender, EventArgs e)
        {
            if (CurrentRow == null || CurrentRow.IsNew)
            {
                return;
            }

            //List<OceanBLContainerList> ctns = oeService.GetOceanMBLContainerList(CurrentRow.ID);
            //Dictionary<string, object> stateValues = new Dictionary<string, object>();
            //stateValues.Add("BLID", CurrentRow.ID);
            //stateValues.Add("ShippingOrderID", CurrentRow.OceanShippingOrderID);

            string title = LocalData.IsEnglish ? "Load Container" : "装箱";

            PartLoader.ShowEditPart<ICP.FCM.DomesticTrade.UI.Container.LoadContainerPart>(Workitem, CurrentRow, null, title + GetLineNo(CurrentRow), EditPartSaved,
                DTBookingCommandConstants.Command_LoadContainer + CurrentRow.ID.ToString());
        }

        #endregion

        #region other

        [CommandHandler(DTBookingCommandConstants.Command_ReplyAgent)]
        public void Command_ReplyAgent(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            try
            {
                fcmCommonClientService.OpenAgentRequestPart(CurrentRow.ID, ICP.Framework.CommonLibrary.Client.OperationType.Unknown, Workitem);
            }
            catch (Exception ex)
            { 
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); 
            }
        }

        [CommandHandler(DTBookingCommandConstants.Command_E_Booking)]
        public void Command_E_Booking(object sender, EventArgs e)
        {

        }

        [CommandHandler(DTBookingCommandConstants.Command_BL)]
        public void Command_BL(object sender, EventArgs e)
        {
        }
        [CommandHandler(DTBookingCommandConstants.Command_Bill)]
        public void Command_Bill(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;
            OperationCommonInfo operationCommonInfo = fcmCommonService.GetOperationCommonInfo(CurrentRow.ID, OperationType.Internal);
            if (operationCommonInfo != null)
            {
                finClientService.ShowBillList(operationCommonInfo, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
            }
            else
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? @"No found,Please contact the system administrator" : @"无对应的数据,请联系系统管理员!");
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

        #region Warehouse
        //[CommandHandler(DTBookingCommandConstants.Command_Warehouse)]
        //public void Command_Warehouse(object sender, EventArgs e)
        //{
        //    if (CurrentRow == null || CurrentRow.IsNew) return;

        //    //OceanBookingInfo editData = DTService.GetOceanBookingInfo(CurrentRow.ID);
        //    OceanBookingInfo editData = UIModelHelper.GetNormalObject<OceanBookingInfo>();

        //    List<OceanWarehouseInfo> warehouseList = DTService.GetOceanWarehouseList(CurrentRow.ID);
        //    if (warehouseList == null)
        //        warehouseList = new List<OceanWarehouseInfo>();
        //    if (warehouseList.Count == 0)
        //        AddDefaultWarehouse(warehouseList, editData);

        //    Dictionary<string, object> stateValues = new Dictionary<string, object>();
        //    stateValues.Add("Booking", editData);

        //    string title = LocalData.IsEnglish ? "Edit Warehouse" : "编辑仓储";
        //    Utility.ShowEditPart<Booking.OceanWarehouseEditPart>(Workitem, warehouseList, stateValues, title, null);
        //}
        //void AddDefaultWarehouse(List<OceanWarehouseInfo> warehouseList, OceanBookingInfo currentData)
        //{
        //    OceanWarehouseInfo newData = new OceanWarehouseInfo();
        //    newData.CreateByID = LocalData.UserInfo.LoginID;
        //    newData.CreateByName = LocalData.UserInfo.LoginName;
        //    newData.CreateDate = DateTime.Now;
        //    newData.WarehouseTime = DateTime.Now;
        //    newData.OceanBookingID = currentData.ID;
        //    newData.ShippingOrderNo = currentData.No;

        //    newData.ShipperID = currentData.ShipperID;
        //    newData.ShipperName = currentData.ShipperName;
        //    newData.ShipperDescription = currentData.ShipperDescription;


        //    newData.Type = WarehouseServiceType.In;
        //    newData.WarehouseCargos = new List<OceanWarehouseCargoList>();
        //    warehouseList.Add(newData);
        //}
        #endregion

        #region Customs
        //[CommandHandler(DTBookingCommandConstants.Command_Customs)]
        //public void Command_Customs(object sender, EventArgs e)
        //{
        //    if (CurrentRow == null || CurrentRow.IsNew) return;

        //    //OceanBookingInfo editData = DTService.GetOceanBookingInfo(CurrentRow.ID);
        //    OceanBookingInfo editData = UIModelHelper.GetNormalObject<OceanBookingInfo>();


        //    List<OceanCustomsInfo> customsList = DTService.GetOceanCustomsList(CurrentRow.ID);
        //    if (customsList == null)
        //        customsList = new List<OceanCustomsInfo>();
        //    if (customsList.Count == 0)
        //        AddDefaultCustoms(customsList, editData);

        //    Dictionary<string, object> stateValues = new Dictionary<string, object>();
        //    stateValues.Add("Booking", editData);

        //    string title = LocalData.IsEnglish ? "Edit Customs" : "编辑报关";
        //    Utility.ShowEditPart<Booking.OceanCustomsEditPart>(Workitem, customsList, stateValues, title, null);
        //}
        //private void AddDefaultCustoms(List<OceanCustomsInfo> customsList, OceanBookingInfo currentData)
        //{
        //    OceanCustomsInfo newData = new OceanCustomsInfo();
        //    newData.CreateByID = LocalData.UserInfo.LoginID;
        //    newData.CreateByName = LocalData.UserInfo.LoginName;
        //    newData.CreateDate = DateTime.Now;
        //    newData.OceanBookingID = currentData.ID;
        //    newData.ShippingOrderNo = currentData.No;
        //    newData.CustomsType = CustomsType.清关;

        //    newData.ShipperID = currentData.ShipperID;
        //    newData.ShipperName = currentData.ShipperName;
        //    newData.ShipperDescription = currentData.ShipperDescription;

        //    newData.Containers = new List<OceanContainerList>();
        //    customsList.Add(newData);
        //}
        #endregion

        #endregion

        #endregion
    }
}
