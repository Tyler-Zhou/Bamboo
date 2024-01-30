using System.Collections.Generic;
using System.Windows.Forms;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.ObjectBuilder;
using ICP.Business.Common.UI.EventList;
using ICP.DataCache.ServiceInterface;
using ICP.Business.Common.UI.Communication;
using ICP.Business.Common.UI.Document;

namespace ICP.FCM.AirExport.UI.Booking
{
    public class BookingWorkitem : WorkItem
    {
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
                IWorkspace toolBarWorkspace = (IWorkspace)Workspaces[AEBookingWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                BookingListPart listPart = SmartParts.AddNew<BookingListPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[AEBookingWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                BookingSearchPart searchPart = SmartParts.AddNew<BookingSearchPart>();
                searchPart.IsLoadFromFinder = false;
                IWorkspace searchWorkspace = (IWorkspace)Workspaces[AEBookingWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);


                BookingFastSearchPart fastSearchPart = SmartParts.AddNew<BookingFastSearchPart>();
                IWorkspace fastSearchPartWorkspace = (IWorkspace)Workspaces[AEBookingWorkSpaceConstants.FastSearchWorkspace];
                fastSearchPartWorkspace.Show(fastSearchPart);

                EventListPart memoListPart = Items.AddNew<EventListPart>();
                IWorkspace EventListWorkspace = (IWorkspace)Workspaces[AEBookingWorkSpaceConstants.EventListWorkspace];
                EventListWorkspace.Show(memoListPart);

                UCCommunicationHistory faxMailEDIListPart = Items.AddNew<UCCommunicationHistory>();
                IWorkspace faxMailListWorkspace = (IWorkspace)Workspaces[AEBookingWorkSpaceConstants.FaxMailEDIListWorkspace];
                faxMailListWorkspace.Show(faxMailEDIListPart);

                UCDocumentList documentPart = SmartParts.AddNew<UCDocumentList>();
                DocumentListPresenter documentPresenter = Items.AddNew<DocumentListPresenter>();
                documentPresenter.ucList = documentPart;
                documentPart.Presenter = documentPresenter;
                IWorkspace documentPartWorkspace = (IWorkspace)Workspaces[AEBookingWorkSpaceConstants.DocumentListWorkspace];
                documentPartWorkspace.Show(documentPart);

                #endregion

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "AirBooking" : "空运出口订舱";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                AEBookingUIAdapter bookingAdapter = new AEBookingUIAdapter();
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

    public class AEBookingCommandConstants
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

    public class AEBookingWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";
        public const string FastSearchWorkspace = "FastSearchWorkspace";
        public const string EventListWorkspace = "EventListWorkspace";
        public const string FaxMailEDIListWorkspace = "FaxMailEDIListWorkspace";
        public const string DocumentListWorkspace = "DocumentListWorkspace";
    }

    public class AEBookingUIAdapter
    {

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
                AirBookingList listData = data as AirBookingList;
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add("ParentList", data);
                if (listData != null)
                {
                    BusinessOperationContext context = new BusinessOperationContext();
                    context.OperationID = listData.ID;
                    context.FormId = listData.ID;
                    context.FormType = FormType.Booking;
                    context.OperationType = OperationType.AirExport;
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

            //_fastSearchPart.RaiseSearched();
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
        private void RefreshBarEnabled(IToolBar toolBar, AirBookingList listData)
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
                //if (string.IsNullOrEmpty(listData.FilightNo))
                //{
                toolBar.SetEnable("barCopy", true);
                toolBar.SetEnable("barCancel", listData.BookingerName == LocalData.UserInfo.UserName);
                toolBar.SetEnable("barPrint", true);
                toolBar.SetEnable("barReplyAgent", true);
                toolBar.SetEnable("barE_Booking", true);
                toolBar.SetEnable("barBL", true);
                toolBar.SetEnable("barBill", true);
                //}
                //else
                //{
                //    toolBar.SetEnable("barCopy", false);
                //    toolBar.SetEnable("barCopyData", false);
                //    toolBar.SetEnable("barCancel", false);
                //    toolBar.SetEnable("barPrint", false);
                //    toolBar.SetEnable("barReplyAgent", false);
                //    toolBar.SetEnable("barE_Booking", false);
                //    toolBar.SetEnable("barBL", false);
                //    toolBar.SetEnable("barBill", false);
                //}

                //toolBar.SetEnable("barPrintBookingConfirm", !string.IsNullOrEmpty(listData.AirShippingOrderNo));
                //toolBar.SetEnable("barTruck", !string.IsNullOrEmpty(listData.AirShippingOrderNo) && listData.OEOperationType != OEOperationType.BULK);
                //toolBar.SetEnable("barLoadContainer", !string.IsNullOrEmpty(listData.AirShippingOrderNo));

                if (listData.BookingerName == LocalData.UserInfo.UserName) toolBar.SetEnable("barCancel", true);

                if (listData.IsValid == false)
                    toolBar.SetText("barCancel", LocalData.IsEnglish ? "Available(&D)" : "恢复(&D)");
                else
                    toolBar.SetText("barCancel", LocalData.IsEnglish ? "Cancel(&D)" : "取消(&D)");


                toolBar.SetEnable("barEdit", true);
                toolBar.SetEnable("barBL", listData.AirHBLs != null
                                    && listData.AirMBLs != null
                                    && listData.AirHBLs.Count + listData.AirMBLs.Count > 0);
            }
        }

        #endregion
    }
}
