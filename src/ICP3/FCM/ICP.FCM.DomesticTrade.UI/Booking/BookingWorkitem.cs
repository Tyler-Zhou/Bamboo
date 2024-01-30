using System.Collections.Generic;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.DomesticTrade.ServiceInterface;
using ICP.FCM.DomesticTrade.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Business.Common.UI.EventList;
using ICP.Business.Common.UI.Communication;
using ICP.Business.Common.UI.Document;

namespace ICP.FCM.DomesticTrade.UI.Booking
{
    public class BookingWorkitem : WorkItem
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        #endregion
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Workitem = null;
            }
            base.Dispose(disposing);
        }
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            BookingMainWorkspace mainSpce = SmartParts.Get<BookingMainWorkspace>("BookingMainWorkspace");
            if (mainSpce == null)
            {
                mainSpce = SmartParts.AddNew<BookingMainWorkspace>("BookingMainWorkspace");

                #region AddPart

                BookingToolBar toolBar = SmartParts.AddNew<BookingToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)Workspaces[DTBookingWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                BookingListPart listPart = SmartParts.AddNew<BookingListPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[DTBookingWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                BookingSearchPart searchPart = SmartParts.AddNew<BookingSearchPart>();
                searchPart.IsLoadFromFinder = false;
                IWorkspace searchWorkspace = (IWorkspace)Workspaces[DTBookingWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);


                BookingFastSearchPart fastSearchPart = SmartParts.AddNew<BookingFastSearchPart>();
                IWorkspace fastSearchPartWorkspace = (IWorkspace)Workspaces[DTBookingWorkSpaceConstants.FastSearchWorkspace];
                fastSearchPartWorkspace.Show(fastSearchPart);


                EventListPart memoListPart = Items.AddNew<EventListPart>();
                IWorkspace EventListWorkspace = (IWorkspace)Workspaces[DTBookingWorkSpaceConstants.EventListWorkspace];
                EventListWorkspace.Show(memoListPart);

                UCCommunicationHistory faxMailEDIListPart = SmartParts.AddNew<UCCommunicationHistory>("faxMailEDIListPart");
                IWorkspace faxMailListWorkspace = (IWorkspace)Workspaces[DTBookingWorkSpaceConstants.FaxMailEDIListWorkspace];
                faxMailListWorkspace.Show(faxMailEDIListPart);

                UCDocumentList documentPart = SmartParts.AddNew<UCDocumentList>();
                DocumentListPresenter documentPresenter = Items.AddNew<DocumentListPresenter>();
                documentPresenter.ucList = documentPart;
                documentPart.Presenter = documentPresenter;
                IWorkspace documentPartWorkspace = (IWorkspace)Workspaces[DTBookingWorkSpaceConstants.DocumentListWorkspace];
                documentPartWorkspace.Show(documentPart);


                #endregion

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "DT_BOOKING" : "业务订舱";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                DTBookingUIAdapter bookingAdapter = new DTBookingUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(fastSearchPart.GetType().Name, fastSearchPart);
                dic.Add(memoListPart.GetType().Name, memoListPart);
                dic.Add(faxMailEDIListPart.GetType().Name, faxMailEDIListPart);
                dic.Add(documentPart.GetType().Name, documentPart);
                bookingAdapter.Init(dic);
            }
            else
            {
                Workspaces[ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }
    }

    public class DTBookingCommandConstants
    {
        public const string Command_AddData = "Command_AddData";
        public const string Command_CopyData = "Command_CopyData";
        public const string Command_EditData = "Command_EditData";
        public const string Command_CancelData = "Command_CancelData";
        public const string Command_RefreshData = "Command_RefreshData";

        public const string Command_Truck = "Command_Truck";
        public const string Command_ReplyAgent = "Command_ReplyAgent";
        public const string Command_E_Booking = "Command_E_Booking";
        public const string Command_BL = "Command_BL";
        public const string Command_Bill = "Command_Bill";
        public const string Command_LoadContainer = "Command_LoadContainer";
        public const string Command_ShowSearch = "Command_ShowSearch";

        public const string Command_PrintOrder = "Command_PrintOrder";
        public const string Command_PrintBookingConfirm = "Command_PrintBookingConfirm";
        public const string Command_PrintInWarehouse = "Command_PrintInWarehouse";

    }

    public class DTBookingWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";
        public const string FastSearchWorkspace = "FastSearchWorkspace";
        public const string EventListWorkspace = "EventListWorkspace";
        public const string FaxMailEDIListWorkspace = "FaxMailEDIListWorkspace";
        public const string DocumentListWorkspace = "DocumentListWorkspace";
    }

    public class DTBookingUIAdapter
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IDomesticTradeService oeService { get; set; }

        #endregion

        #region parts

        IToolBar _toolBar;
        ISearchPart _searchPart;
        BookingListPart _mainListPart;
        ISearchPart _fastSearchPart;
        IListPart _eventlistPart;
        UCCommunicationHistory _faxMailEDIListPart;
        UCDocumentList _DocumentListPart;

        #endregion

        #region interface

        public void Init(Dictionary<string, object> controls)
        {
            _toolBar = (IToolBar)controls[typeof(BookingToolBar).Name];
            _searchPart = (ISearchPart)controls[typeof(BookingSearchPart).Name];
            _fastSearchPart = (ISearchPart)controls[typeof(BookingFastSearchPart).Name];
            _mainListPart = (BookingListPart)controls[typeof(BookingListPart).Name];
            _eventlistPart = (IListPart)controls[typeof(EventListPart).Name];
            _faxMailEDIListPart = (UCCommunicationHistory)controls[typeof(UCCommunicationHistory).Name];
            _DocumentListPart = (UCDocumentList)controls[typeof(UCDocumentList).Name];

            RefreshBarEnabled(_toolBar, null);

            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                DTBookingList listData = data as DTBookingList;
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add("ParentList", data);

                if (listData != null)
                {
                    BusinessOperationContext context = new BusinessOperationContext();
                    context.OperationID = listData.ID;
                    context.FormId = listData.ID;
                    context.FormType = FormType.Booking;
                    context.OperationType = OperationType.Internal;
                    _eventlistPart.DataSource = context;
                    //设置沟通历史记录数据源
                    _faxMailEDIListPart.BindData(context);
                    //设置文档中心数据源
                    FCM.Common.UI.FCMUIUtility.SetDocumentListDataSource(_DocumentListPart,context);
                }

                #region toolBar

                RefreshBarEnabled(_toolBar, listData);

                #endregion
            };
            #endregion

            #region _searchPart.OnSearched
            _searchPart.OnSearched += delegate(object sender, object results)
            {
                _mainListPart.DataSource = results;
            };
            #endregion

            #region _fastSearchPart.OnSearched
            _fastSearchPart.OnSearched += delegate(object sender, object results)
            {
                _mainListPart.DataSource = results;
            };
            #endregion

            _mainListPart.KeyDown += new KeyEventHandler(_mainListPart_KeyDown);

            #endregion

            _fastSearchPart.RaiseSearched();
        }

        void _mainListPart_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender != null)
            {
                Dictionary<string, object> keyValue = sender as Dictionary<string, object>;
                if (keyValue != null)
                {
                    _searchPart.Init(keyValue);
                    _searchPart.RaiseSearched();
                }
            }
        }
        private void RefreshBarEnabled(IToolBar toolBar, DTBookingList listData)
        {
            if (listData == null || listData.IsNew)
            {
                toolBar.SetEnable("barCopy", false);
                toolBar.SetEnable("barEdit", false);
                toolBar.SetEnable("barCancel", false);
                toolBar.SetEnable("barPrint", false);
                toolBar.SetEnable("barTruck", false);
                toolBar.SetEnable("barReplyAgent", false);
                toolBar.SetEnable("barE_Booking", false);
                toolBar.SetEnable("barBL", false);
                toolBar.SetEnable("barBill", false);
                toolBar.SetEnable("barLoadContainer", false);
                toolBar.SetEnable("barSubPrint", false);
                toolBar.SetEnable("barBL", false);
            }
            else
            {
                toolBar.SetEnable("barSubPrint", true);
                toolBar.SetEnable("barPrintOrder", true);
                toolBar.SetEnable("barPrintInWarehouse", true);
                if (ArgumentHelper.GuidIsNullOrEmpty(listData.OceanShippingOrderID) == false)
                {
                    toolBar.SetEnable("barCopy", listData.BookingerName == LocalData.UserInfo.LoginName);
                    toolBar.SetEnable("barCancel", listData.BookingerName == LocalData.UserInfo.LoginName);
                    toolBar.SetEnable("barPrint", true);
                    toolBar.SetEnable("barReplyAgent", true);
                    toolBar.SetEnable("barE_Booking", true);
                    toolBar.SetEnable("barBL", true);
                    toolBar.SetEnable("barBill", true);
                }
                else
                {
                    toolBar.SetEnable("barCopy", false);
                    toolBar.SetEnable("barCopyData", false);
                    toolBar.SetEnable("barCancel", false);
                    toolBar.SetEnable("barPrint", false);
                    toolBar.SetEnable("barReplyAgent", false);
                    toolBar.SetEnable("barE_Booking", false);
                    toolBar.SetEnable("barBL", false);
                    toolBar.SetEnable("barBill", false);
                }

                toolBar.SetEnable("barPrintBookingConfirm", !string.IsNullOrEmpty(listData.OceanShippingOrderNo));
                toolBar.SetEnable("barTruck", !string.IsNullOrEmpty(listData.OceanShippingOrderNo) && listData.DTOperationType != FCMOperationType.BULK);
                toolBar.SetEnable("barLoadContainer", !string.IsNullOrEmpty(listData.OceanShippingOrderNo));

                if (listData.BookingerName == LocalData.UserInfo.LoginName) toolBar.SetEnable("barCancel", true);

                if (listData.IsValid == false)
                    toolBar.SetText("barCancel", LocalData.IsEnglish ? "Available(&D)" : "恢复(&D)");
                else
                    toolBar.SetText("barCancel", LocalData.IsEnglish ? "Cancel(&D)" : "取消(&D)");


                toolBar.SetEnable("barEdit", true);
                toolBar.SetEnable("barBL", listData.OceanHBLs != null
                                    && listData.OceanMBLs != null
                                    && listData.OceanHBLs.Count + listData.OceanMBLs.Count > 0);
            }
        }

        #endregion
    }
}
