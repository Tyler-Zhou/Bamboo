using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.EDI.ServiceInterface;
using ICP.EDI.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.CompositeObjects;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.UI.Booking.BaseEdit;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Server;
using ICP.Message.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
//using ICP.Test;

namespace ICP.FCM.OceanExport.UI.Booking
{
    /// <summary>
    /// 订舱单列表界面
    /// </summary>
    [ToolboxItem(false)]
    public partial class BookingListPart : BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        public IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }

        public IFCMCommonClientService FCMCommonClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFCMCommonClientService>();
            }
        }

        public OceanExportPrintHelper OceanExportPrintHelper
        {
            get
            {
                return ClientHelper.Get<OceanExportPrintHelper, OceanExportPrintHelper>();
            }
        }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        public IEDIClientService EDIClientService
        {
            get
            {
                return ServiceClient.GetClientService<IEDIClientService>();
            }
        }
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        public IClientOceanExportService ClientOceanExportService
        {
            get
            {
                return ServiceClient.GetClientService<IClientOceanExportService>();
            }
        }

        //public IAutoTestServiceInterface AutoTestService
        //{
        //    get
        //    {
        //        return ServiceClient.GetClientService<IAutoTestServiceInterface>();
        //    }

        //}
        #endregion

        #region Init


        public BookingListPart()
        {
            InitializeComponent();

            Disposed += delegate
            {
                gvMain.BeforeLeaveRow -= gvMain_BeforeLeaveRow;
                gvMain.CustomDrawRowIndicator -= gvMain_CustomDrawRowIndicator;
                gvMain.KeyDown -= gvMain_KeyDown;
                gvMain.MouseEnter -= gvMain_MouseEnter;
                gvMain.RowCellClick -= gvMain_RowCellClick;

                CurrentChanged = null;
                KeyDown = null;
                CurrentChanging = null;

                BookIngListQuery = null;

                gcMain.DataSource = null;
                bsList.DataSource = null;
                bsList.PositionChanged -= bsMainList_PositionChanged;

                bsList.Dispose();

                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            Load += new EventHandler(BookingListPart_Load);

        }

        bool _shown = false;
        int record_SelectedTabPageIndex = 0;

        void BookingListPart_Load(object sender, EventArgs e)
        {
            DataSource = DataSource == null ? new List<OceanBookingList>() : DataSource;

            _shown = true;
        }

        private void SetCnText()
        {
            colState.Caption = "状态";
            colNo.Caption = "业务号";
            colMBLNo.Caption = "主提单号";
            colHBLNo.Caption = "分提单号";
            colContainerNo.Caption = "箱号";
            colPlaceOfReceiptName.Caption = "收货地";
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
            colShipperName.Caption = "发货人";
            colConsigneeName.Caption = "收货人";
            colClosingDate.Caption = "截关日";
            colSODate.Caption = "订舱日";
            colCreateDate.Caption = "创建日"; ;
            colType.Caption = "业务类型";
            colSalesName.Caption = "揽货人";
            colOverSeasFilerName.Caption = "海外部客服";
            colBookingerName.Caption = "订舱";
            colFiler.Caption = "文件";
            colPODContact.Caption = "港后客服";
        }
        private delegate void SetDataSourceDelegate(List<OceanBookingList> data);
        private void InnerSetDataSource(List<OceanBookingList> data)
        {
            DataSource = data;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
                BindDataAsync();
            }
        }
        private void BindDataAsync()
        {
            WaitCallback callback = data =>
            {
                BookingListQueryCriteria bookingListQuery = data as BookingListQueryCriteria;
                if (bookingListQuery != null)
                {
                    //定义弱类型的变量
                    OEOrderState? OrderStateValue = null;
                    List<Guid> companyIds = new List<Guid>();
                    LocalData.UserInfo.UserOrganizationList.FindAll(o => o.Type == LocalOrganizationType.Company).ForEach(c => companyIds.Add(c.ID));
                    List<OceanBookingList> oceanBookingList = OceanExportService.GetOceanBookingList
                         (companyIds.ToArray(),
                         null, null, null, null, null, null,
                         bookingListQuery.Customer,
                         bookingListQuery.Companys,
                         null, null, null,
                         bookingListQuery.POL,
                         bookingListQuery.POD,
                         null,
                         null, null, null, null, true,
                         OrderStateValue, DateSearchType.All,
                         null, null, 100);
                    Invoke(new SetDataSourceDelegate(InnerSetDataSource), oceanBookingList);

                }

            };
            ThreadPool.QueueUserWorkItem(callback, BookIngListQuery);
        }

        private void InitControls()
        {
            //OrderState
            List<EnumHelper.ListItem<OEOrderState>> orderStates = EnumHelper.GetEnumValues<OEOrderState>(LocalData.IsEnglish);
            rcmbState.Properties.BeginUpdate();
            foreach (var item in orderStates)
            {
                rcmbState.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            rcmbState.Properties.EndUpdate();


            //OEOperationType
            List<EnumHelper.ListItem<FCMOperationType>> oeoperationType = EnumHelper.GetEnumValues<FCMOperationType>(LocalData.IsEnglish);
            cmbOEOperationType.Properties.BeginUpdate();
            foreach (var item in oeoperationType)
            {
                cmbOEOperationType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbOEOperationType.Properties.EndUpdate();


        }

        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }
        public OceanBookingList CurrentRow
        {
            get { return Current as OceanBookingList; }
        }

        private List<OceanBookingList> SelectRows
        {
            get
            {
                int[] rowIndexs = gvMain.GetSelectedRows();

                if (rowIndexs.Length == 0) return null;

                List<OceanBookingList> tagers = new List<OceanBookingList>();
                foreach (var item in rowIndexs)
                {
                    OceanBookingList dr = gvMain.GetRow(item) as OceanBookingList;
                    if (dr != null) tagers.Add(dr);
                }

                return tagers;
            }
        }

        public BookingListQueryCriteria BookIngListQuery { get; set; }

        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                gcMain.BeginUpdate();
                gvMain.BeginUpdate();

                bsList.DataSource = value;
                bsList.ResetBindings(false);

                #region 对一些枚举类型，获取对应语言的描述信息

                List<OceanBookingList> list = value as List<OceanBookingList>;

                gvMain.EndUpdate();
                gcMain.EndUpdate();

                #endregion


                if (CurrentChanged != null)
                {
                    CurrentChanged(this, Current);
                }

                SetColumnsWidth();

                string message = string.Empty;
                if (list != null)
                {
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
                                // message = string.Format("{0} record found", list.Count);
                            }
                        }
                        else
                        {
                            //message = string.Format("查询到 {0} 条记录", list.Count);
                        }
                    }
                    else
                    {

                        //message = LocalData.IsEnglish ? "Nothing found!" : "没有查询到任何结果。";
                    }
                }

                if (_shown)
                {
                    // LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), message);
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
            List<OceanBookingList> list = DataSource as List<OceanBookingList>;
            if (list == null) return;
            List<OceanBookingList> newLists = items as List<OceanBookingList>;
            foreach (var item in newLists)
            {
                OceanBookingList tager = list.Find(delegate(OceanBookingList jItem) { return item.ID == jItem.ID; });
                if (tager == null) continue;
                OEUtility.CopyToValue(item, tager, typeof(OceanBookingList));
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

        public override event CurrentChangedHandler CurrentChanged;
        public override event CancelEventHandler CurrentChanging;

        #endregion

        #region GridView Event

        private void bsMainList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(record_SelectedTabPageIndex, Current);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            pccHyperLinks.Visible = false;
            pccHyperLinks.Controls.Clear();

            if (e.Button != MouseButtons.Left) return;

            if (e.Clicks == 2)
            {
                GvMainDoubleClick();
            }
            else if (e.Clicks == 1)
            {
                #region Show Tip

                if (e.Column.Name == colMBLNo.Name)
                {
                    if (CurrentRow.OceanMBLs != null)
                    {
                        foreach (var item in CurrentRow.OceanMBLs)
                        {
                            LabelControl hlMBL = new LabelControl();
                            hlMBL.Text = item.NO + " " + EnumHelper.GetDescription<OEBLState>(item.State, LocalData.IsEnglish);
                            hlMBL.AutoSize = true;
                            hlMBL.Tag = item;
                            hlMBL.Click += new EventHandler(hlMBL_Click);
                            hlMBL.Location = new Point(3, 22 * pccHyperLinks.Controls.Count + 3);
                            hlMBL.Font = new Font(FontFamily.GenericSerif, 9F, FontStyle.Underline);
                            hlMBL.ForeColor = Color.Blue;

                            pccHyperLinks.Controls.Add(hlMBL);
                        }
                    }
                }
                else if (e.Column.Name == colHBLNo.Name)
                {
                    if (CurrentRow.OceanHBLs != null)
                    {
                        foreach (var item in CurrentRow.OceanHBLs)
                        {
                            LabelControl hlHBL = new LabelControl();
                            hlHBL.Text = item.NO + " " + EnumHelper.GetDescription<OEBLState>(item.State, LocalData.IsEnglish);
                            hlHBL.AutoSize = true;
                            hlHBL.Tag = item;
                            hlHBL.Click += new EventHandler(hlHBL_Click);
                            hlHBL.Location = new Point(3, 22 * pccHyperLinks.Controls.Count + 3);
                            hlHBL.Font = new Font(FontFamily.GenericSerif, 9F, FontStyle.Underline);
                            hlHBL.ForeColor = Color.Blue;

                            pccHyperLinks.Controls.Add(hlHBL);
                        }
                    }
                }
                else if (e.Column.Name == colContainerNo.Name)
                {
                    if (CurrentRow.BookingContainers != null)
                    {
                        foreach (var item in CurrentRow.BookingContainers)
                        {
                            LabelControl hlContainer = new LabelControl();
                            hlContainer.Text = item.NO + " " + item.TypeName;
                            hlContainer.AutoSize = true;
                            hlContainer.Tag = item;
                            hlContainer.Click += new EventHandler(hlContainer_Click);
                            hlContainer.Location = new Point(3, 22 * pccHyperLinks.Controls.Count + 3);
                            hlContainer.Font = new Font(FontFamily.GenericSerif, 9F, FontStyle.Underline);
                            hlContainer.ForeColor = Color.Blue;

                            pccHyperLinks.Controls.Add(hlContainer);
                        }
                    }
                }

                if (pccHyperLinks.Controls.Count > 0)
                {
                    pccHyperLinks.Location = e.Location;
                    pccHyperLinks.Height = pccHyperLinks.Controls[pccHyperLinks.Controls.Count - 1].Location.Y
                        + pccHyperLinks.Controls[pccHyperLinks.Controls.Count - 1].Height + 5;

                    pccHyperLinks.Width = pccHyperLinks.Controls.Cast<Control>().Max(o => o.Width) + 10;

                    pccHyperLinks.Show();
                }
                #endregion
            }
        }

        void hlMBL_Click(object sender, EventArgs e)
        {
            EditBL(sender, true);
        }
        private void EditBL(object sender, bool isMBL)
        {
            LabelControl hlMBL = (LabelControl)sender;
            BookingBLInfo blInfo = (BookingBLInfo)hlMBL.Tag;
            if (isMBL)
            {
                ClientOceanExportService.EditMBL(blInfo.OperationNo, blInfo.NO, null, EditPartSaved);
            }
            else
            {
                ClientOceanExportService.EditHBL(blInfo.OperationNo, blInfo.NO, null, EditPartSaved);
            }
        }

        void hlHBL_Click(object sender, EventArgs e)
        {
            EditBL(sender, false);
        }

        void hlContainer_Click(object sender, EventArgs e)
        {
            LabelControl hlContainer = (LabelControl)sender;
            BookingContainerInfo editData = (BookingContainerInfo)hlContainer.Tag;

            IDictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("ContainerId", editData.ID);
            ClientOceanExportService.LoadContainer(CurrentRow.No, CurrentRow.ID, dic, EditPartSaved);

        }

        public new event KeyEventHandler KeyDown;

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (e.KeyCode == Keys.Enter)
                {
                    GvMainDoubleClick();
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
                    Workitem.Commands[OEBookingCommandConstants.Command_ShowSearch].Execute();
                }
            }
        }

        protected virtual void GvMainDoubleClick()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow != null)
                {
                    Workitem.Commands[OEBookingCommandConstants.Command_EditData].Execute();
                }
            }
        }

        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            OceanBookingList list = gvMain.GetRow(e.RowHandle) as OceanBookingList;
            if (list == null) return;

            if (list.IsValid == false)
            {
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.Disabled);
            }
            else if (list.State == OEOrderState.NewOrder)
            {
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.NewLine);
            }
            else if (list.State == OEOrderState.Checked)
            {
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.Confirmed);
            }
            else if (list.State == OEOrderState.Rejected)
            {
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.Error);
            }
        }



        #region Workitem Common



        BusinessOperationContext CreateMemoParamInfo()
        {
            BusinessOperationContext para = new BusinessOperationContext();
            para.OperationID = CurrentRow.ID;
            para.FormId = CurrentRow.OceanShippingOrderID == null ? Guid.Empty : CurrentRow.OceanShippingOrderID.Value;
            para.FormType = FormType.ShippingOrder;
            para.OperationType = OperationType.OceanExport;
            return para;
        }

        #endregion

        #region add copy edit Cancel

        [CommandHandler(OEBookingCommandConstants.Command_AddData)]
        public void Command_AddData(object sender, EventArgs e) { AddData(); }
        protected void AddData()
        {
            ClientOceanExportService.AddBooking(null, EditPartSaved);
        }

        [CommandHandler(OEBookingCommandConstants.Command_CopyData)]
        public void Command_CopyData(object sender, EventArgs e) { CopyData(); }
        protected void CopyData()
        {
            if (CurrentRow == null) return;
            ClientOceanExportService.CopyBooking(ShowCriteria, null, EditPartSaved);
        }

        [CommandHandler(OEBookingCommandConstants.Command_CopyOrderData)]
        public void Command_CopyOrderData(object sender, EventArgs e)
        {
            CopyOrderData();
        }

        protected void CopyOrderData()
        {
            if (CurrentRow == null) return;
            ClientOceanExportService.CopyOrder(ShowCriteria, null, EditPartSaved);
        }

        private EditPartShowCriteria ShowCriteria
        {
            get
            {
                if (CurrentRow == null)
                {
                    return null;
                }
                return new EditPartShowCriteria { OperationNo = CurrentRow.No, BillNo = CurrentRow.ID };

            }
        }
        [CommandHandler(OEBookingCommandConstants.Command_EditData)]
        public void Command_EditData(object sender, EventArgs e) { EditData(); }
        protected void EditData()
        {
            if (CurrentRow == null)
            {
                return;
            }
            ClientOceanExportService.EditBooking(ShowCriteria, null, EditPartSaved);

        }



        public void EditPartSaved(object[] prams)
        {
            //if (InvokeRequired)
            //{
            //    Action<object[]> editPartSaved = EditPartSaved;
            //    Invoke(editPartSaved,new object[]{prams});
            //}
            //else
            //{
            if (prams == null || prams.Length == 0)
            {
                return;
            }

            OceanBookingList data = prams[0] as OceanBookingList;

            if (data == null)
            {
                return;
            }

            List<OceanBookingList> source = bsList.DataSource as List<OceanBookingList>;

            if (prams.Length == 2)
            {
                if (source == null || source.Count == 0)
                {
                    bsList.Insert(0, data);
                    bsList.ResetBindings(false);
                }
                else
                {
                    OceanBookingList tager = source.Find(delegate(OceanBookingList item) { return item.ID == data.ID; });
                    if (tager == null)
                    {
                        bsList.Insert(0, data);
                        bsList.ResetBindings(false);
                    }
                    else
                    {
                        List<BookingBLInfo> _mbls = new List<BookingBLInfo>();
                        if (tager.OceanMBLs == null)
                        {
                            tager.OceanMBLs = new List<BookingBLInfo>();
                        }
                        tager.OceanMBLs.ForEach(o => _mbls.Add(o));

                        List<BookingBLInfo> _hbls = new List<BookingBLInfo>();
                        if (tager.OceanHBLs == null)
                        {
                            tager.OceanHBLs = new List<BookingBLInfo>();
                        }
                        tager.OceanHBLs.ForEach(o => _hbls.Add(o));

                        List<BookingContainerInfo> _ctns = new List<BookingContainerInfo>();
                        if (tager.BookingContainers == null)
                        {
                            tager.BookingContainers = new List<BookingContainerInfo>();
                        }
                        tager.BookingContainers.ForEach(o => _ctns.Add(o));

                        OEUtility.CopyToValue(data, tager, typeof(OceanBookingList));

                        tager.OceanMBLs = _mbls.ToList();
                        tager.OceanHBLs = _hbls.ToList();
                        tager.BookingContainers = _ctns.ToList();

                        tager.MBLNo = string.Empty;
                        tager.HBLNo = string.Empty;
                        tager.ContainerNo = string.Empty;

                        _mbls.ForEach(o => tager.MBLNo = tager.MBLNo + ((tager.MBLNo.Length > 0 ? "," : string.Empty) + o.NO));
                        _hbls.ForEach(o => tager.HBLNo = tager.HBLNo + ((tager.HBLNo.Length > 0 ? "," : string.Empty) + o.NO));
                        _ctns.ForEach(o => tager.ContainerNo = tager.ContainerNo + ((tager.ContainerNo.Length > 0 ? "," : string.Empty) + o.NO));

                        bsList.ResetItem(bsList.IndexOf(tager));
                    }
                }
            }
            else
            {
                //传来的参数是订舱当和箱列表
                OceanBookingList tager = source.Find(delegate(OceanBookingList item) { return item.ID == data.ID; });
                if (tager != null)
                {
                    List<OceanContainerList> containers = prams[1] as List<OceanContainerList>;

                    if (containers != null)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (OceanContainerList container in containers)
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
                                info.OceanBookingID = tager.ID;

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

            SetColumnsWidth();
            //}
        }

        void SetColumnsWidth()
        {
            gvMain.BestFitColumns();
        }

        [CommandHandler(OEBookingCommandConstants.Command_CancelData)]
        public void Command_CancelData(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;
            CurrentRow.CompanyID = Guid.Empty;
            ClientOceanExportService.CancelOrderForFCM(CurrentRow, AfterCancelBooking);
        }
        private void AfterCancelBooking(object[] parameters)
        {
            SingleResult result = parameters[0] as SingleResult;
            OceanBookingList _currentRow = CurrentRow;
            _currentRow.IsValid = !_currentRow.IsValid;
            _currentRow.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
            bsList.ResetCurrentItem();
            if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
        }


        /// <summary>
        /// 保存memo
        /// </summary>
        /// <param name="content"></param>
        void SaveMemo(string content)
        {
            CommonMemoList commonMemoList = new CommonMemoList();
            commonMemoList.Subject = "SOC";
            commonMemoList.Content = content;
            commonMemoList.ID = CurrentRow.ID;

            Guid?[] formIDs = new Guid?[1] { CurrentRow.ID };

            FormType[] formType = new FormType[1] { FormType.Booking };

            //commonDataService.SaveMemo(commonMemoList, formIDs, formType, CurrentRow.ID, ICP.Framework.CommonLibrary.Client.OperationType.AirExport);

        }

        #endregion

        #region print

        [CommandHandler(OEBookingCommandConstants.Command_PrintOrder)]
        public void Command_PrintOrder(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;
            ClientOceanExportService.PrintOrder(CurrentRow.ID, CurrentRow.CompanyID);
        }

        [CommandHandler(OEBookingCommandConstants.Command_PrintBookingConfirm)]
        public void Command_PrintBookingConfirm(object sender, EventArgs e)
        {
            if (CurrentRow == null || CurrentRow.IsNew) return;
            ClientOceanExportService.PrintBookingConfirm(CurrentRow.ID);
        }

        /// <summary>
        /// 通知客户ADJ SO Copy(中文版) 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OEBookingCommandConstants.Command_MailADJSOCopyToCustomerCH)]
        public void Command_MailADJSOCopyToCustomerCH(object sender, EventArgs e)
        {
            if (CurrentRow == null || CurrentRow.IsNew) return;
            ClientOceanExportService.MailSoCopyToCustomer(false, CurrentRow.ID);
        }


        /// <summary>
        /// 通知客户ADJ SO Copy(英文版) 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OEBookingCommandConstants.Command_MailADJSOCopyToCustomerEN)]
        public void Command_MailADJSOCopyToCustomerEN(object sender, EventArgs e)
        {
            if (CurrentRow == null || CurrentRow.IsNew) return;
            ClientOceanExportService.MailSoCopyToCustomer(true, CurrentRow.ID);
        }


        /// <summary>
        /// 通知客户订舱确认书(中文版)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OEBookingCommandConstants.Command_OEMailSoConfirmationToCustomerCH)]
        public void Command_MailSoConfirmationToCustomerCH(object sender, EventArgs e)
        {
            if (CurrentRow == null || CurrentRow.IsNew) return;
            ClientOceanExportService.MailSoConfirmationToCustomer(CurrentRow.ID, false);
        }


        /// <summary>
        /// 通知客户订舱确认书(英文版)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OEBookingCommandConstants.Command_OEMailSoConfirmationToCustomerEH)]
        public void Command_MailSoConfirmationToCustomerEH(object sender, EventArgs e)
        {
            if (CurrentRow == null || CurrentRow.IsNew) return;
            ClientOceanExportService.MailSoConfirmationToCustomer(CurrentRow.ID, true);
        }

        /// <summary>
        /// 通知代理订舱确认书(中文版)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OEBookingCommandConstants.Command_MailSoConfirmationToAgentCH)]
        public void Command_MailSoConfirmationToAgentCH(object sender, EventArgs e)
        {
            if (CurrentRow == null || CurrentRow.IsNew) return;
            ClientOceanExportService.MailSoConfirmationToAgent(CurrentRow.ID);
        }

        /// <summary>
        /// 通知代理订舱确认书(英文版)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OEBookingCommandConstants.Command_MailSoConfirmationToAgentEN)]
        public void Command_MailSoConfirmationToAgentEN(object sender, EventArgs e)
        {
            if (CurrentRow == null || CurrentRow.IsNew) return;
            ClientOceanExportService.MailSoConfirmationToAgent(CurrentRow.ID);
        }


        /// <summary>
        ///Print profit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OEBookingCommandConstants.Command_PrintProfit)]
        public void Command_PrintProfit(object sender, EventArgs e)
        {
            if (CurrentRow == null || CurrentRow.IsNew) return;
            ClientOceanExportService.PrintBookingProfit(CurrentRow.ID);

        }


        [CommandHandler(OEBookingCommandConstants.Command_PrintInWarehouse)]
        public void Command_PrintInWarehouse(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null || CurrentRow.IsNew) return;

                //try
                //{
                //    OceanExportPrintHelper.PrintOEBookingConfirmation(CurrentRow.ID);
                //}
                //catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); }
            }
        }

        private Message.ServiceInterface.Message GetOperationInfo()
        {
            if (CurrentRow == null)
                return null;
            Message.ServiceInterface.Message message = new Message.ServiceInterface.Message();
            message.UserProperties = new MessageUserPropertiesObject();
            message.UserProperties.OperationType = OperationType.AirImport;
            message.UserProperties.OperationId = CurrentRow.ID;
            message.UserProperties.FormType = FormType.Booking;
            message.UserProperties.FormId = CurrentRow.ID;

            return message;
        }

        #endregion

        #region Truck

        [CommandHandler(OEBookingCommandConstants.Command_Truck)]
        public void Command_Truck(object sender, EventArgs e)
        {
            InitTruck(CurrentRow);
        }

        public void InitTruck(OceanBookingList currentRow)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (currentRow == null || currentRow.IsNew)
                {
                    return;
                }
                ClientOceanExportService.OpenTruckOrder(currentRow.ID, currentRow.No, currentRow, null, null);

            }
        }

        #endregion

        #region Customs

        [CommandHandler(OEBookingCommandConstants.Command_Customs)]
        public void Command_Customs(object sender, EventArgs e)
        {
            InitCustoms(CurrentRow);
        }

        public void InitCustoms(OceanBookingList currentRow)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (currentRow == null || currentRow.IsNew)
                {
                    return;
                }
                ClientOceanExportService.OpenTruckOrder(currentRow.ID, currentRow.No, CurrentRow, null, null);
            }
        }

        #endregion

        #region RefreshData

        [CommandHandler(OEBookingCommandConstants.Command_RefreshData)]
        public void Command_RefreshData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    List<OceanBookingList> blList = DataSource as List<OceanBookingList>;
                    if (blList == null || blList.Count == 0) return;

                    List<Guid> ids = new List<Guid>();
                    foreach (var item in blList)
                    {
                        ids.Add(item.ID);
                    }

                    List<OceanBookingList> list = OceanExportService.GetOceanBookingListByIds(ids.ToArray());
                    DataSource = list;
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
            }
        }

        #endregion

        #region 装箱

        /// <summary>
        /// 装箱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OEBookingCommandConstants.Command_LoadContainer)]
        public void Command_LoadContainer(object sender, EventArgs e)
        {
            if (CurrentRow == null || CurrentRow.IsNew)
            {
                return;
            }
            ClientOceanExportService.LoadContainer(CurrentRow.No, CurrentRow.ID, null, EditPartSaved);

        }

        #endregion

        #region other

        [CommandHandler(OEBookingCommandConstants.Command_ReplyAgent)]
        public void Command_ReplyAgent(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null) return;

                try
                {
                    FCMCommonClientService.OpenAgentRequestPart(CurrentRow.ID, OperationType.OceanExport, null, null);
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                }
            }
        }




        [CommandHandler(OEBookingCommandConstants.Command_BL)]
        public void Command_BL(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null) return;

                ClientOceanExportService.OpenBillOfLoadingList(CurrentRow.No);
            }
        }

        [CommandHandler(OEBookingCommandConstants.Command_Bill)]
        public void Command_Bill(object sender, EventArgs e)
        {
            if (CurrentRow == null)
                return;

            ClientOceanExportService.OpenBill(CurrentRow.ID, OperationType.OceanExport);
        }

        #endregion

        #region EDI
        /// <summary>
        /// 电子订舱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OEBookingCommandConstants.Command_E_Booking)]
        public void Command_E_Booking(object sender, EventArgs e)
        {
            if (CurrentRow == null)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Please select current operation." : "请选择当前要发送EDI的业务.");
                return;
            }
            EDISendOption sendItem = new EDISendOption();
            List<Guid> inttraList = new List<Guid>();
            inttraList.Add(new Guid("979B3DA5-2FE2-4895-8F43-DE5610D20599"));//CMA
            inttraList.Add(new Guid("E2A5B70E-9D7A-47B2-9902-082D8A317548"));//UASC
            inttraList.Add(new Guid("FDCA28E3-7673-4803-B3C2-71E7E66B7650"));//APL
            inttraList.Add(new Guid("68797EA6-F0BB-4035-947B-84A731E21245"));//HPL
            inttraList.Add(new Guid("BF072F15-BEE9-4C33-8448-70931FC06FA9"));//MSC
            List<EDIConfigureList> ediList = ConfigureService.GetEDIConfigureList(null, null, true, 0);

            List<EDIConfigureList> findEdiList = (from d in ediList where d.EDIMode == EDIMode.Booking && d.CarrierID == CurrentRow.CarrierID select d).ToList();
            string key = string.Empty;

            if (findEdiList.Count > 0)
            {
                List<ConfigureListForEDI> comlist = ConfigureService.GetEDICompanyConfigureListByConfigure(findEdiList[0].ID);
                if (comlist.Find(r => r.CompanyID == CurrentRow.CompanyID) != null)
                {
                    key = findEdiList[0].Code;
                }
            }

            if (string.IsNullOrEmpty(key))
            {
                OceanBookingInfo ob = OceanExportService.GetOceanBookingInfo(CurrentRow.ID);
                findEdiList = (from d in ediList where d.EDIMode == EDIMode.Booking && d.CarrierID == ob.AgentOfCarrierID && d.ReceiverType == 2 select d).ToList();
                if (findEdiList.Count > 0)
                {
                    key = findEdiList[0].Code;
                    sendItem.CarrierID = ob.AgentOfCarrierID;
                }
                else
                {
                    if (inttraList.Contains((Guid)CurrentRow.CarrierID))
                    {
                        key = "InttraSo";
                    }
                    else
                    {
                        string message = "目前EDI订舱只支持下列船东:";
                        List<String> carrierList = (from d in ediList where d.EDIMode == EDIMode.Booking group d by d.CarrierName into g select g.Key).ToList();
                        carrierList.RemoveAll(r => string.IsNullOrEmpty(r));
                        message = message + Environment.NewLine + carrierList.Aggregate((a, b) => (a + Environment.NewLine + b));

                        MessageBoxService.ShowInfo(message);
                    }
                }

            }

            if (SelectRows.Count > 10)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish == true ? "Selected items be less than or equal to 10." : "每次最多只能发送10条！");
                return;
            }

            try
            {
                List<Guid> ids = new List<Guid>(SelectRows.Count);
                List<string> nos = new List<string>(SelectRows.Count);

                bool check = false;
                foreach (var item in SelectRows)
                {

                    ids.Add(item.ID);
                    nos.Add(item.No);
                }

                int i = (from d in SelectRows group d by d.CarrierID into g select g.Key).Count();
                if (i > 1)
                {
                    MessageBoxService.ShowInfo(LocalData.IsEnglish == true ? "Selected items exist different Carrier." : "选择的项中存在不同船东！");
                    return;
                }
                int n = (from d in SelectRows group d by d.CompanyID into g select g.Key).Count();
                if (n > 1)
                {
                    MessageBoxService.ShowInfo(LocalData.IsEnglish == true ? "Selected items exist different companys." : "选择的项中存在不同的操作口岸！");
                    return;
                }
                
                sendItem.ServiceKey = key;
                sendItem.EdiMode = EDIMode.Booking;
                sendItem.CompanyID = CurrentRow.CompanyID;
                if (sendItem.ServiceKey == "InttraSo")
                {
                    sendItem.Subject = "Inttra电子订舱(";
                    foreach (string s in nos)
                        sendItem.Subject += s + ",";
                    sendItem.Subject = sendItem.Subject.TrimEnd(',') + ")";
                }
                else
                {
                    sendItem.Subject = "电子订舱(";
                    foreach (string s in nos)
                        sendItem.Subject += s + ",";
                    sendItem.Subject = sendItem.Subject.TrimEnd(',') + ")";
                }
                sendItem.IDs = ids.ToArray();
                sendItem.FIDs = ids.ToArray();
                sendItem.NOs = nos.ToArray();
                sendItem.OperationType = OperationType.OceanExport;
                if (sendItem.CarrierID == Guid.Empty)
                {
                    sendItem.CarrierID = CurrentRow.CarrierID == null ? Guid.Empty : (Guid)CurrentRow.CarrierID;
                }

                bool isSucc = EDIClientService.SendEDI(sendItem);
                if (isSucc)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Send Successfully!" : "发送成功!");
                }
            }
            catch (Exception ex)
            {
                MessageBoxService.ShowInfo(ex.Message);
            }
            //ClienBookingService clienEBookingService = new ClienBookingService();
            //clienEBookingService.EBookingCall(CurrentRow, SelectRows);
        }

        #endregion

        #region 电子订舱宁波
        /// <summary>
        /// 电子订舱宁波
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OEBookingCommandConstants.Command_OEBE_BookingNB)]
        public void Command_OEBE_BookingNB(object sender, EventArgs e)
        {
            if (CurrentRow == null)
            {
                NBEDIANLBOOKINGObj obj = new NBEDIANLBOOKINGObj();
                string title = LocalData.IsEnglish ? "BOOKING INFO" : "订舱信息";
                ICP.Framework.ClientComponents.Controls.PartLoader.ShowEditPart<BookingEDIEditForNB>(this.Workitem, obj, EditMode.Edit, null, title, null, DateTime.Now.ToString("yyyyMMdd"));
                //MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Please select current operation." : "请选择当前要发送EDI的业务.");
                return;
            }

            List<EDIConfigureList> ediList = ConfigureService.GetEDIConfigureList(null, null, true, 0);

            List<EDIConfigureList> findEdiList = (from d in ediList where d.EDIMode == EDIMode.Booking && d.CarrierID == CurrentRow.CarrierID select d).ToList();
            string key = string.Empty;

            if (findEdiList.Count > 0)
            {
                key = findEdiList[0].Code;
            }
            else
            {
                string message = "目前EDI订舱只支持下列船东:";
                List<String> carrierList = (from d in ediList where d.EDIMode == EDIMode.Booking group d by d.CarrierName into g select g.Key).ToList();
                foreach (string carrierName in carrierList)
                {
                    message = message + System.Environment.NewLine + carrierName;
                }

                MessageBoxService.ShowInfo(message);
                return;
            }

            if (SelectRows.Count > 10)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish == true ? "Selected items be less than or equal to 10." : "每次最多只能发送10条！");
                return;
            }

            try
            {
                if (CurrentRow.CarrierID.ToString().ToUpper() == "FB9634B0-ECFD-450B-A0B3-D52466D30383")
                {
                    NBEDIANLBOOKINGObj obj;
                    OceanBookingInfo editData = OceanExportService.GetOceanBookingInfo(CurrentRow.ID);
                    obj = ConvertNBEDIObj(editData);
                    string title = LocalData.IsEnglish ? "BOOKING INFO" : "订舱信息";
                    ICP.Framework.ClientComponents.Controls.PartLoader.ShowEditPart<BookingEDIEditForNB>(this.Workitem, obj, EditMode.Edit, null, title, null, CurrentRow.No);
                }
                else
                {
                    List<Guid> ids = new List<Guid>(SelectRows.Count);
                    List<string> nos = new List<string>(SelectRows.Count);

                    bool check = false;
                    foreach (var item in SelectRows)
                    {

                        ids.Add(item.ID);
                        nos.Add(item.No);
                    }

                    int i = (from d in SelectRows group d by d.CarrierID into g select g.Key).Count();
                    if (i > 1)
                    {
                        MessageBoxService.ShowInfo(LocalData.IsEnglish == true ? "Selected items exist different Carrier." : "选择的项中存在不同船东！");
                        return;
                    }
                    int n = (from d in SelectRows group d by d.CompanyID into g select g.Key).Count();
                    if (n > 1)
                    {
                        MessageBoxService.ShowInfo(LocalData.IsEnglish == true ? "Selected items exist different companys." : "选择的项中存在不同的操作口岸！");
                        return;
                    }
                    EDISendOption sendItem = new EDISendOption();
                    sendItem.ServiceKey = key;
                    sendItem.EdiMode = EDIMode.Booking;
                    sendItem.CompanyID = CurrentRow.CompanyID;
                    sendItem.Subject = "电子订舱(";
                    foreach (string s in nos)
                        sendItem.Subject += s + ",";
                    sendItem.Subject = sendItem.Subject.TrimEnd(',') + ")";
                    sendItem.IDs = ids.ToArray();
                    sendItem.FIDs = ids.ToArray();
                    sendItem.NOs = nos.ToArray();
                    sendItem.OperationType = OperationType.OceanExport;

                    bool isSucc = EDIClientService.SendEDI(sendItem);
                    if (isSucc)
                    {
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Send Successfully!" : "发送成功!");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBoxService.ShowInfo(ex.Message);
            }
        }

        private NBEDIANLBOOKINGObj ConvertNBEDIObj(OceanBookingInfo orgObj)
        {
            NBEDIANLBOOKINGObj NBObj = new NBEDIANLBOOKINGObj();

            if (orgObj != null)
            {
                NBObj.ID = orgObj.ID;
                NBObj.No = orgObj.No;
                NBObj.HBLNo = orgObj.HBLNo;
                NBObj.MBLNo = orgObj.MBLNo;
                NBObj.CompanyID = orgObj.CompanyID;
                NBObj.ShipperID = orgObj.BookingShipperID;
                NBObj.ShipperName = orgObj.BookingShipperName;
                NBObj.ShipperDescription = orgObj.BookingShipperdescription;
                NBObj.ConsigneeID = orgObj.BookingConsigneeID;
                NBObj.ConsigneeName = orgObj.BookingConsigneeName;
                NBObj.ConsigneeDescription = orgObj.BookingConsigneedescription;
                NBObj.ContainerDescription = orgObj.ContainerDescription;
                NBObj.NotifyPartyID = orgObj.BookingNotifyPartyID;
                NBObj.NotifyPartyname = orgObj.BookingNotifyPartyname;
                NBObj.NotifyPartydescription = orgObj.BookingNotifyPartydescription;
                NBObj.POLID = orgObj.POLID;
                NBObj.POLName = orgObj.POLName;
                NBObj.PODID = orgObj.PODID;
                NBObj.PODName = orgObj.PODName;
                NBObj.PlaceOfReceiptID = orgObj.PlaceOfReceiptID;
                NBObj.PlaceOfReceiptName = orgObj.PlaceOfReceiptName;
                NBObj.PlaceOfDeliveryID = orgObj.PlaceOfDeliveryID;
                NBObj.PlaceOfDeliveryName = orgObj.PlaceOfDeliveryName;
                NBObj.VoyageID = orgObj.VoyageID;
                NBObj.VoyageName = orgObj.VoyageName;
                NBObj.TransportClauseID = orgObj.TransportClauseID;
                NBObj.TransportClauseName = orgObj.TransportClauseName;
                NBObj.PaymentTermID = orgObj.PaymentTermID;
                NBObj.PaymentTermName = orgObj.PaymentTermName;
                NBObj.Marks = orgObj.Marks;
                NBObj.Commodity = orgObj.Commodity;
                NBObj.SQNO = orgObj.ContractNo;
            }

            return NBObj;
        }

        #endregion

        #region 电子订舱宁波预配
        /// <summary>
        /// 电子订舱宁波预配
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OEBookingCommandConstants.Command_OEBE_BookingConNB)]
        public void Command_OEBE_BookingConNB(object sender, EventArgs e)
        {

            if (CurrentRow == null)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Please select current operation." : "请选择当前要发送EDI的业务.");
                return;
            }

            if (CurrentRow.CarrierID.ToString().ToUpper() != "FB9634B0-ECFD-450B-A0B3-D52466D30383")
            {
                string message = "目前EDI预配只支持ANL";

                MessageBoxService.ShowInfo(message);
                return;
            }

            if (SelectRows.Count > 10)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish == true ? "Selected items be less than or equal to 10." : "每次最多只能发送10条！");
                return;
            }


            List<EDIConfigureList> ediList = ConfigureService.GetEDIConfigureList(null, null, true, 0);

            List<EDIConfigureList> findEdiList = (from d in ediList where d.EDIMode == EDIMode.Booking && d.CarrierID == CurrentRow.CarrierID select d).ToList();
            string key = string.Empty;

            if (findEdiList.Count > 0)
            {
                key = findEdiList[0].Code;
                try
                {

                    List<Guid> ids = new List<Guid>(SelectRows.Count);
                    List<string> nos = new List<string>(SelectRows.Count);

                    bool check = false;
                    foreach (var item in SelectRows)
                    {

                        ids.Add(item.ID);
                        nos.Add(item.No);
                    }

                    int i = (from d in SelectRows group d by d.CarrierID into g select g.Key).Count();
                    if (i > 1)
                    {
                        MessageBoxService.ShowInfo(LocalData.IsEnglish == true ? "Selected items exist different Carrier." : "选择的项中存在不同船东！");
                        return;
                    }
                    int n = (from d in SelectRows group d by d.CompanyID into g select g.Key).Count();
                    if (n > 1)
                    {
                        MessageBoxService.ShowInfo(LocalData.IsEnglish == true ? "Selected items exist different companys." : "选择的项中存在不同的操作口岸！");
                        return;
                    }

                    EDISendOption sendItem = new EDISendOption();
                    sendItem.ServiceKey = key;
                    sendItem.EdiMode = EDIMode.Booking;
                    sendItem.CompanyID = CurrentRow.CompanyID;
                    sendItem.Subject = "电子订舱(";
                    foreach (string s in nos)
                        sendItem.Subject += s + ",";
                    sendItem.Subject = sendItem.Subject.TrimEnd(',') + ")";
                    sendItem.IDs = ids.ToArray();
                    sendItem.FIDs = ids.ToArray();
                    sendItem.NOs = nos.ToArray();
                    sendItem.OperationType = OperationType.OceanExport;

                    bool isSucc = EDIClientService.SendEDI(sendItem);
                    if (isSucc)
                    {
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Send Successfully!" : "发送成功!");
                    }
                }

                catch (Exception ex)
                {
                    MessageBoxService.ShowInfo(ex.Message);
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

        private void gvMain_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }

        #region Warehouse Customs\\暂时不做

        private void gvMain_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            if (e.Column.FieldName == "ETA")
            {
                OceanBookingList list = gvMain.GetRow(e.RowHandle) as OceanBookingList;
                if (list.ETA != null && list.IsAllRBLD == false)
                {
                    DateTime dadt = Convert.ToDateTime(list.ETA);
                    dadt = dadt.AddDays(-10);
                    DateTime dt1 = DateTime.Now;
                    if (dt1.CompareTo(dadt) > 0)
                    {
                        e.Appearance.BackColor = Color.DeepSkyBlue;
                    }
                }
            }
        }

        #endregion
        #endregion
    }
}
