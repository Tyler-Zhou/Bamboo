using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.FCM.AirExport.ServiceInterface;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OtherBusiness.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.ClientComponents.Extender;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using ICP.FCM.Common.ServiceInterface.CompositeObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents;

namespace ICP.Business.Common.UI.CSPBooking
{
    /// <summary>
    /// 列表布局面板
    /// </summary>
    [ToolboxItem(false)]
    public partial class PartList : BaseListPart
    {
        #region Filed
        bool _shown = false;
        int record_SelectedTabPageIndex = 0;
        #endregion

        #region Service
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem PartWorkItem { get; set; }

        /// <summary>
        /// FCM通用服务
        /// </summary>
        IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetClientService<IFCMCommonService>();
            }

        }

        /// <summary>
        /// 海出客户端服务
        /// </summary>
        IClientOceanExportService ClientOceanExportService
        {
            get
            {
                return ServiceClient.GetClientService<IClientOceanExportService>();
            }

        }
        /// <summary>
        /// 空运出口客户端服务
        /// </summary>
        IClientAirExportService ClientAirExportService
        {
            get
            {
                return ServiceClient.GetClientService<IClientAirExportService>();
            }

        }

        /// <summary>
        /// 其他业务客户端服务
        /// </summary>
        IClientOtherBusinessService ClientOtherBusinessService
        {
            get
            {
                return ServiceClient.GetClientService<IClientOtherBusinessService>();
            }

        }
        #endregion

        #region Init

        /// <summary>
        /// 
        /// </summary>
        public PartList()
        {
            InitializeComponent();

            Disposed += delegate
            {
                gvMain.BeforeLeaveRow -= gvMain_BeforeLeaveRow;
                gvMain.KeyDown -= gvMain_KeyDown;
                gvMain.RowCellClick -= gvMain_RowCellClick;
                CurrentChanged = null;
                KeyDown = null;
                CurrentChanging = null;
                gcMain.DataSource = null;
                bsList.DataSource = null;
                bsList.PositionChanged -= bsMainList_PositionChanged;

                bsList.Dispose();

                if (PartWorkItem != null)
                {
                    PartWorkItem.Items.Remove(this);
                    PartWorkItem = null;
                }
            };
            Load += new EventHandler(PartListWorkspace_Load);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PartListWorkspace_Load(object sender, EventArgs e)
        {
            DataSource = DataSource == null ? new List<BookingDelegateList>() : DataSource;
            _shown = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        private delegate void SetDataSourceDelegate(List<BookingDelegateList> data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        private void InnerSetDataSource(List<BookingDelegateList> data)
        {
            DataSource = data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
                BindDataAsync();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void BindDataAsync()
        {
            WaitCallback callback = data =>
            {
                //BookingListQueryCriteria bookingListQuery = data as BookingListQueryCriteria;
                //if (bookingListQuery != null)
                //{
                //    //定义弱类型的变量
                //    OEOrderState? OrderStateValue = null;
                //    List<Guid> companyIds = new List<Guid>();
                //    LocalData.UserInfo.UserOrganizationList.FindAll(o => o.Type == LocalOrganizationType.Company).ForEach(c => companyIds.Add(c.ID));
                //    List<BookingDelegateList> oceanBookingList = OceanExportService.GetOceanBookingList
                //         (companyIds.ToArray(),
                //         null, null, null, null, null, null,
                //         bookingListQuery.Customer,
                //         bookingListQuery.Companys,
                //         null, null, null,
                //         bookingListQuery.POL,
                //         bookingListQuery.POD,
                //         null,
                //         null, null, null, null, true,
                //         OrderStateValue, DateSearchType.All,
                //         null, null, 100);
                //    Invoke(new SetDataSourceDelegate(InnerSetDataSource), oceanBookingList);
                //}

            };
            //ThreadPool.QueueUserWorkItem(callback, BookIngListQuery);
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitControls()
        {
            gvMain.ShowGridViewRowNo(50);
        }

        #endregion

        #region IListPart 成员
        /// <summary>
        /// 
        /// </summary>
        public override object Current
        {
            get { return bsList.Current; }
        }
        /// <summary>
        /// 
        /// </summary>
        public BookingDelegateList CurrentRow
        {
            get { return Current as BookingDelegateList; }
        }
        /// <summary>
        /// 
        /// </summary>
        private List<BookingDelegateList> SelectRows
        {
            get
            {
                int[] rowIndexs = gvMain.GetSelectedRows();
                if (rowIndexs.Length == 0) return null;
                List<BookingDelegateList> tagers = new List<BookingDelegateList>();
                foreach (var item in rowIndexs)
                {
                    BookingDelegateList dr = gvMain.GetRow(item) as BookingDelegateList;
                    if (dr != null) tagers.Add(dr);
                }
                return tagers;
            }
        }
        /// <summary>
        /// 
        /// </summary>
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

                List<BookingDelegateList> list = value as List<BookingDelegateList>;

                gvMain.EndUpdate();
                gcMain.EndUpdate();

                #endregion


                if (CurrentChanged != null)
                {
                    CurrentChanged(this, Current);
                }

                string message = string.Empty;
                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        if (LocalData.IsEnglish)
                        {
                            message = string.Format("{0} records found", list.Count);
                        }
                        else
                        {
                            message = string.Format("查询到 {0} 条记录", list.Count);
                        }
                    }
                }

                if (_shown)
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        public override void Refresh(object items)
        {
            List<BookingDelegateList> list = DataSource as List<BookingDelegateList>;
            if (list == null) return;
            List<BookingDelegateList> newLists = items as List<BookingDelegateList>;
            foreach (var item in newLists)
            {
                BookingDelegateList tager = list.Find(delegate(BookingDelegateList jItem) { return item.BookingMapID == jItem.BookingMapID; });
                if (tager == null) continue;
            }
            bsList.ResetBindings(false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public override void RemoveItem(int index)
        {
            bsList.RemoveAt(index);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public override void RemoveItem(object item)
        {
            bsList.Remove(item);
        }
        /// <summary>
        /// 
        /// </summary>
        public override event CurrentChangedHandler CurrentChanged;
        /// <summary>
        /// 
        /// </summary>
        public override event CancelEventHandler CurrentChanging;

        #endregion

        #region GridView Event
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bsMainList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(record_SelectedTabPageIndex, Current);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            if (e.Button != MouseButtons.Left) return;

            if (e.Clicks == 2)
            {
                GvMainDoubleClick();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public new event KeyEventHandler KeyDown;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    //Workitem.Commands[OEBookingCommandConstants.Command_ShowSearch].Execute();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected virtual void GvMainDoubleClick()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow != null)
                {
                    //Workitem.Commands[OEBookingCommandConstants.Command_EditData].Execute();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            BookingDelegateList list = gvMain.GetRow(e.RowHandle) as BookingDelegateList;
            if (list == null) return;
            //if (list.status == OEOrderState.NewOrder)
            //{
            //    GridHelper.SetColorStyle(e.Appearance, PresenceStyle.NewLine);
            //}
            //else if (list.State == OEOrderState.Checked)
            //{
            //    GridHelper.SetColorStyle(e.Appearance, PresenceStyle.Confirmed);
            //}
            //else if (list.State == OEOrderState.Rejected)
            //{
            //    GridHelper.SetColorStyle(e.Appearance, PresenceStyle.Error);
            //}
        }
        #endregion

        #region CommandHandler
        [CommandHandler(CSPBKConstants.COMMAND_DOWNLOAD)]
        public void Command_Download(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null)
                {
                    return;
                }
                if (SelectRows.Count() <= 0)
                {
                    return;
                }
                if (SelectRows.Where(fItem => fItem.OperationType == OperationType.Unknown).Count() > 0)
                {
                    MessageBoxService.ShowWarning("不支持的业务类型");
                    return;
                }

                int currentTypeCount = SelectRows.Where(fItem => fItem.OperationType == CurrentRow.OperationType).Count();
                if (SelectRows.Count() > currentTypeCount)
                {
                    MessageBoxService.ShowWarning("不能同时选择多种业务");
                    return;
                }
                if (SelectRows.Where(fItem => fItem.OperationType == OperationType.Other).Count() > 1)
                {
                    MessageBoxService.ShowWarning("FBA/FBM货不能批量下载");
                    return;
                }
                Dictionary<string, object> values = new Dictionary<string, object>();
                List<BookingDelegate> bookings = FCMCommonService.DownloadBookingDelegate(new SearchParameterDownloadBookingDelegate() { BusinessID = SelectRows.Select(fItem => fItem.BookingMapID).ToArray() });
                if (bookings == null || bookings.Count() <=0 )
                {
                    throw new Exception("下载数据失败！");
                }
                values.Add("BOOKINGINFOFORCSP", bookings);
                switch (CurrentRow.OperationType)
                {
                    case OperationType.OceanExport:
                        ClientOceanExportService.AddBooking(values, null);
                        break;
                    case OperationType.AirExport:
                        ClientAirExportService.AddData(values, null);
                        break;
                    case OperationType.Other:
                        ClientOtherBusinessService.ECommerceAddData(values,null);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}
