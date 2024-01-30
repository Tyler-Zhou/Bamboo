using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.UI.Common;
using System;
using ICP.MailCenter.CommonUI;
using ICP.MailCenter.ServiceInterface;
using Microsoft.Practices.ObjectBuilder;

namespace ICP.FCM.OceanExport.UI.Booking
{
    public class BookingWorkitem : WorkItem
    {
        #region  服务
        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public DocumentListPresenter documentPresenter { get; set; }
        #endregion

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            BookingMainWorkspace mainSpce = this.SmartParts.Get<BookingMainWorkspace>("BookingMainWorkspace");
            if (mainSpce == null)
            {
                mainSpce = this.SmartParts.AddNew<BookingMainWorkspace>("BookingMainWorkspace");

                #region AddPart

                BookingToolBar toolBar = this.SmartParts.AddNew<BookingToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[OEBookingWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                BookingListPart listPart = this.SmartParts.AddNew<BookingListPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[OEBookingWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                BookingSearchPart searchPart = this.SmartParts.AddNew<BookingSearchPart>();
                searchPart.IsLoadFromFinder = false;
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[OEBookingWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);


                BookingFastSearchPart fastSearchPart = this.SmartParts.AddNew<BookingFastSearchPart>();
                IWorkspace fastSearchPartWorkspace = (IWorkspace)this.Workspaces[OEBookingWorkSpaceConstants.FastSearchWorkspace];
                fastSearchPartWorkspace.Show(fastSearchPart);

                MemoListPart memoListPart = this.Items.AddNew<MemoListPart>();
                IWorkspace memoListWorkspace = (IWorkspace)this.Workspaces[OEBookingWorkSpaceConstants.MemoListWorkspace];
                memoListWorkspace.Show(memoListPart);

                UCCommunicationHistory faxMailEDIListPart = this.Items.AddNew<UCCommunicationHistory>();
                IWorkspace faxMailListWorkspace = (IWorkspace)this.Workspaces[OEBookingWorkSpaceConstants.FaxMailEDIListWorkspace];
                faxMailListWorkspace.Show(faxMailEDIListPart);

                UCDocumentList documentListPart = this.Items.AddNew<UCDocumentList>();
                IWorkspace documentListWorkspace = (IWorkspace)this.Workspaces[OEBookingWorkSpaceConstants.DocumentListWorkspace];
                documentListWorkspace.Show(documentListPart);

                #endregion

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Ocean Export Booking" : "海运出口订舱";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                OEBookingUIAdapter bookingAdapter = new OEBookingUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(fastSearchPart.GetType().Name, fastSearchPart);
                dic.Add(memoListPart.GetType().Name, memoListPart);
                dic.Add(faxMailEDIListPart.GetType().Name, faxMailEDIListPart);
                dic.Add(documentListPart.GetType().Name, documentListPart);
                bookingAdapter.Init(dic, documentPresenter);
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }
    }

    public class OEBookingCommandConstants
    {
        public const string Command_AddData = "Command_OEBAddData";
        public const string Command_CopyData = "Command_OEBCopyData";
        public const string Command_EditData = "Command_OEBEditData";
        public const string Command_CancelData = "Command_OEBCancelData";
        public const string Command_RefreshData = "Command_OEBRefreshData";

        public const string Command_Truck = "Command_OEBTruck";
        public const string Command_ReplyAgent = "Command_OEBReplyAgent";
        public const string Command_E_Booking = "Command_OEBE_Booking";
        public const string Command_E_SI = "Command_E_SI";

        public const string Command_BL = "Command_OEBBL";
        public const string Command_Bill = "Command_OEBBill";
        public const string Command_LoadContainer = "Command_OEBLoadContainer";
        public const string Command_ShowSearch = "Command_OEBShowSearch";

        public const string Command_PrintOrder = "Command_OEBPrintOrder";
        public const string Command_PrintBookingConfirm = "Command_OEBPrintBookingConfirm";
        public const string Command_PrintInWarehouse = "Command_OEBPrintInWarehouse";

        public const string Command_PrintProfit = "Command_OEBPrintProfit";
    }

    public class OEBookingWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";
        public const string FastSearchWorkspace = "FastSearchWorkspace";
        public const string MemoListWorkspace = "MemoListWorkspace";
        public const string FaxMailEDIListWorkspace = "FaxMailEDIListWorkspace";
        public const string DocumentListWorkspace = "DocumentListWorkspace";
    }

    public class OEBookingUIAdapter
    {
        #region parts

        IToolBar _toolBar;
        ISearchPart _searchPart;
        BookingListPart _mainListPart;
        ISearchPart _fastSearchPart;
        IListPart _memolistPart;
        UCCommunicationHistory _faxMailEDIListPart;
        UCDocumentList _DocumentListPart;

        #endregion

        #region interface

        public void Init(Dictionary<string, object> controls, DocumentListPresenter documentPresenter)
        {
            _toolBar = (IToolBar)controls[typeof(BookingToolBar).Name];
            _searchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(BookingSearchPart).Name];
            _fastSearchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(BookingFastSearchPart).Name];
            _mainListPart = (BookingListPart)controls[typeof(BookingListPart).Name];
            _memolistPart = (IListPart)controls[typeof(MemoListPart).Name];
            _faxMailEDIListPart = (UCCommunicationHistory)controls[typeof(UCCommunicationHistory).Name];
            _DocumentListPart = (UCDocumentList)controls[typeof(UCDocumentList).Name];

            RefreshBarEnabled(_toolBar, null);

            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                OceanBookingList listData = data as OceanBookingList;
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add("ParentList", data);

                if (listData != null)
                {
                    MemoParam para = new MemoParam();
                    para.OperationId = listData.ID;
                    para.FormID = listData.OceanShippingOrderID == null ? Guid.Empty : listData.OceanShippingOrderID.Value;
                    para.FormType = ICP.Framework.CommonLibrary.Common.FormType.ShippingOrder;
                    para.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.OceanExport;
                    _memolistPart.DataSource = para;
                    //设置文档中心数据源
                    Utility.SetDocumentListDataSource(_DocumentListPart, documentPresenter, para);

                    //设置Fax/Email/EDI数据源
                    Utility.SetCommunicationDataSource(_faxMailEDIListPart, para);
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

            _mainListPart.KeyDown += new System.Windows.Forms.KeyEventHandler(_mainListPart_KeyDown);

            #endregion

            //_fastSearchPart.RaiseSearched();
        }

        void _mainListPart_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
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
        private void RefreshBarEnabled(IToolBar toolBar, OceanBookingList listData)
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
                toolBar.SetEnable("barE_SI", false);
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
                toolBar.SetEnable("barCopy", true);

                if (Utility.GuidIsNullOrEmpty(listData.OceanShippingOrderID) == false)
                {

                    toolBar.SetEnable("barCancel", listData.BookingerName == LocalData.UserInfo.UserName || listData.BookingerName == LocalData.UserInfo.LoginName);
                    toolBar.SetEnable("barPrint", true);
                    toolBar.SetEnable("barE_Booking", true);
                    toolBar.SetEnable("barE_SI", true);
                    toolBar.SetEnable("barBL", true);
                    toolBar.SetEnable("barBill", true);
                    toolBar.SetEnable("barPrintProfit", true);

                    if (listData.State == OEOrderState.LoadPreVoyage ||
                        listData.State == OEOrderState.LoadVoyage ||
                        listData.State == OEOrderState.Closed ||
                        listData.State == OEOrderState.Rejected)
                    {
                        toolBar.SetEnable("barReplyAgent", false);
                    }
                    else
                    {
                        toolBar.SetEnable("barReplyAgent", true);
                    }
                }
                else
                {
                    toolBar.SetEnable("barCopy", false);
                    toolBar.SetEnable("barCopyData", false);
                    toolBar.SetEnable("barCancel", false);
                    toolBar.SetEnable("barPrint", false);
                    toolBar.SetEnable("barReplyAgent", false);
                    toolBar.SetEnable("barE_Booking", false);
                    toolBar.SetEnable("barE_SI", false);
                    toolBar.SetEnable("barBL", false);
                    toolBar.SetEnable("barBill", false);
                    toolBar.SetEnable("barPrintProfit", false);

                }

                toolBar.SetEnable("barPrintBookingConfirm", !string.IsNullOrEmpty(listData.OceanShippingOrderNo));
                toolBar.SetEnable("barTruck", listData.OEOperationType != FCMOperationType.BULK);
                toolBar.SetEnable("barLoadContainer", !string.IsNullOrEmpty(listData.OceanShippingOrderNo));

                if (listData.BookingerName == LocalData.UserInfo.UserName) toolBar.SetEnable("barCancel", true);

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
