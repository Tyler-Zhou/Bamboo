using DevExpress.XtraTab;
using ICP.Business.Common.UI;
using ICP.Business.Common.UI.Communication;
using ICP.Business.Common.UI.Document;
using ICP.Business.Common.UI.EventList;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface.CompositeObjects;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;

namespace ICP.FCM.OceanExport.UI.Booking
{
    public class WorkItemBooking : WorkItem
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string TitleName { get; set; }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                faxMailEDIListPart = null;
            }
            base.Dispose(disposing);
        }

        public static UCCommunicationHistory faxMailEDIListPart = null;
        protected override void OnRunStarted()
        {
            base.OnRunStarted();

            Show(null);
        }
        /// <summary>
        /// 面板加载
        /// </summary>
        /// <param name="bookIngListQuery">列表的查询参数</param>
        public void Show(BookingListQueryCriteria bookIngListQuery)
        {
            string workspaceId = bookIngListQuery == null ? "DefaultBookingMainWorkspace" : bookIngListQuery.Companys.ToString() + bookIngListQuery.Customer.ToString() + bookIngListQuery.POD + bookIngListQuery.POL;
            BookingMainWorkspace mainSpce = this.SmartParts.Get<BookingMainWorkspace>(workspaceId);
            if (mainSpce == null)
            {
                mainSpce = this.SmartParts.AddNew<BookingMainWorkspace>(workspaceId);

                #region AddPart

                BookingToolBar toolBar = this.SmartParts.AddNew<BookingToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[OEBookingWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                BookingListPart listPart = GetBookingListPart(bookIngListQuery);

                BookingSearchPart searchPart = this.SmartParts.AddNew<BookingSearchPart>();
                searchPart.IsLoadFromFinder = false;
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[OEBookingWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);

                BookingFastSearchPart fastSearchPart = this.SmartParts.AddNew<BookingFastSearchPart>();
                IWorkspace fastSearchPartWorkspace = (IWorkspace)this.Workspaces[OEBookingWorkSpaceConstants.FastSearchWorkspace];
                fastSearchPartWorkspace.Show(fastSearchPart);

                EventListPart eventListPart = this.Items.AddNew<EventListPart>();
                IWorkspace eventListWorkspace = (IWorkspace)this.Workspaces[OEBookingWorkSpaceConstants.EventListWorkspace];
                eventListWorkspace.Show(eventListPart);

                UCOECargoTracking cargoTracking = this.Items.AddNew<UCOECargoTracking>();
                IWorkspace cargoTrackingWorkspace = (IWorkspace)this.Workspaces[OEBookingWorkSpaceConstants.CargoTracking];
                cargoTrackingWorkspace.Show(cargoTracking);
                
                faxMailEDIListPart = this.Items.AddNew<UCCommunicationHistory>("FaxMailEDIListPart");
                IWorkspace faxMailListWorkspace = (IWorkspace)this.Workspaces[OEBookingWorkSpaceConstants.FaxMailEDIListWorkspace];
                faxMailListWorkspace.Show(faxMailEDIListPart);

                UCDocumentList documentListPart = this.Items.AddNew<UCDocumentList>();
                DocumentListPresenter presenter = this.Items.AddNew<DocumentListPresenter>();
                presenter.ucList = documentListPart;
                documentListPart.Presenter = presenter;
                IWorkspace documentListWorkspace = (IWorkspace)this.Workspaces[OEBookingWorkSpaceConstants.DocumentListWorkspace];
                documentListWorkspace.Show(documentListPart);

                #endregion

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                if (bookIngListQuery != null)
                {
                    smartPartInfo.Title = LocalData.IsEnglish ? bookIngListQuery.Customer + "  " + "History of the business" : bookIngListQuery.Customer + "  " + "历史业务";
                }
                else
                {
                    smartPartInfo.Title = LocalData.IsEnglish ? "Ocean Export Booking" : "海运出口订舱";
                }
                mainWorkspace.Show(mainSpce, smartPartInfo);

                OEBookingUIAdapter bookingAdapter = new OEBookingUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(fastSearchPart.GetType().Name, fastSearchPart);
                dic.Add(eventListPart.GetType().Name, eventListPart);
                dic.Add(cargoTracking.GetType().Name, cargoTracking);
                dic.Add(faxMailEDIListPart.GetType().Name, faxMailEDIListPart);
                dic.Add(documentListPart.GetType().Name, documentListPart);
                dic.Add(mainSpce.GetType().Name, mainSpce);
                bookingAdapter.Init(dic);
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }

        /// <summary>
        /// 根据传进的参数信息对订舱表的数据进行过滤
        /// </summary>
        /// <param name="bookIngListQuery">查询参数</param>
        /// <returns></returns>
        public BookingListPart GetBookingListPart(ICP.FCM.OceanExport.ServiceInterface.CompositeObjects.BookingListQueryCriteria bookIngListQuery)
        {
            ////定义弱类型的变量
            //OEOrderState? OrderStateValue = null;
            //实例面板
            BookingListPart listPart = this.SmartParts.AddNew<BookingListPart>();
            //面板的唯一名称
            IWorkspace listWorkspace = (IWorkspace)this.Workspaces[OEBookingWorkSpaceConstants.ListWorkspace];
            listPart.BookIngListQuery = bookIngListQuery;
            listWorkspace.Show(listPart);
            return listPart;
        }

    }

    public class OEBookingCommandConstants
    {
        public const string Command_AddData = "Command_OEBAddData";
        public const string Command_CopyData = "Command_OEBCopyData";
        public const string Command_CopyOrderData = "Command_OEBCopyOrderData";
        public const string Command_EditData = "Command_OEBEditData";
        public const string Command_CancelData = "Command_OEBCancelData";
        public const string Command_RefreshData = "Command_OEBRefreshData";
        public const string Command_Customs = "Command_OEBCustoms";
        public const string Command_Truck = "Command_OEBTruck";
        public const string Command_ReplyAgent = "Command_OEBReplyAgent";
        public const string Command_E_Booking = "Command_OEBE_Booking";
        public const string Command_E_SI = "Command_E_SI";
        public const string Command_BL = "Command_OEBBL";
        public const string Command_Bill = "Command_OEBBill";
        /// <summary>
        /// 装箱
        /// </summary>
        public const string Command_LoadContainer = "Command_OEBLoadContainer";
        /// <summary>
        /// 预配箱
        /// </summary>
        public const string Command_DeclarationContainer = "Command_OEDeclarationContainer";
        public const string Command_ShowSearch = "Command_OEBShowSearch";
        public const string Command_PrintOrder = "Command_OEBPrintOrder";
        public const string Command_PrintBookingConfirm = "Command_OEBPrintBookingConfirm";
        public const string Command_PrintInWarehouse = "Command_OEBPrintInWarehouse";
        public const string Command_PrintProfit = "Command_OEBPrintProfit";
        public const string Command_TabChanged = "Command_TabChanged";
        public const string SelectedTabPageIndex = "SelectedTabPageIndex";
        public const string Command_OEBE_BookingNB = "Command_OEBE_BookingNB";
        public const string Command_OEBE_BookingConNB = "Command_OEBE_BookingConNB";
        /// <summary>
        /// 通知客户ADJ SO Copy(中文版) 
        /// </summary>
        public const string Command_MailADJSOCopyToCustomerCH = "Command_MailADJSOCopyToCustomerCH";
        /// <summary>
        /// 通知客户ADJ SO Copy(英文版) 
        /// </summary>
        public const string Command_MailADJSOCopyToCustomerEN = "Command_MailADJSOCopyToCustomerEN";
        /// <summary>
        ///  通知客户订舱确认书(中文版)
        /// </summary>
        public const string Command_OEMailSoConfirmationToCustomerCH = "Command_OEMailSoConfirmationToCustomerCH";
        /// <summary>
        /// 通知客户订舱确认书(英文版)
        /// </summary>
        public const string Command_OEMailSoConfirmationToCustomerEH = "Command_OEMailSoConfirmationToCustomerEH";
        /// <summary>
        /// 通知代理订舱确认书(中文版)
        /// </summary>
        public const string Command_MailSoConfirmationToAgentCH = "Command_MailSoConfirmationToAgentCH";
        /// <summary>
        /// 通知代理订舱确认书(英文版)
        /// </summary>
        public const string Command_MailSoConfirmationToAgentEN = "Command_MailSoConfirmationToAgentEN";
    }

    public class OEBookingWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";
        public const string FastSearchWorkspace = "FastSearchWorkspace";
        public const string EventListWorkspace = "EventListWorkspace";
        public const string BLListWorkSpace = "BLListWorkSpace";
        public const string FaxMailEDIListWorkspace = "FaxMailEDIListWorkspace";
        public const string DocumentListWorkspace = "DocumentListWorkspace";
        public const string CargoTracking = "CargoTrackingWorkspace";
    }

    public class OEBookingUIAdapter : IDisposable
    {
        #region parts

        IToolBar _toolBar;
        ISearchPart _searchPart;
        BookingListPart _mainListPart;
        ISearchPart _fastSearchPart;
        IListPart _eventlistPart;
        UCOECargoTracking _cargoTracking;
        UCCommunicationHistory _faxMailEDIListPart;
        UCDocumentList _DocumentListPart;
        BookingMainWorkspace _mainSpace;

        #endregion

        #region interface

        public void Init(Dictionary<string, object> controls)
        {
            _toolBar = (IToolBar)controls[typeof(BookingToolBar).Name];
            _searchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(BookingSearchPart).Name];
            _fastSearchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(BookingFastSearchPart).Name];
            _mainListPart = (BookingListPart)controls[typeof(BookingListPart).Name];
            _eventlistPart = (IListPart)controls[typeof(EventListPart).Name];
            _cargoTracking = (UCOECargoTracking)controls[typeof(UCOECargoTracking).Name];
            _faxMailEDIListPart = (UCCommunicationHistory)controls[typeof(UCCommunicationHistory).Name];
            _DocumentListPart = (UCDocumentList)controls[typeof(UCDocumentList).Name];
            _mainSpace = (BookingMainWorkspace)controls[typeof(BookingMainWorkspace).Name];

            RefreshBarEnabled(_toolBar, null);

            #region Connection

            _mainSpace.TabSelectedPageChanged += delegate(object sender, object data)
            {
                TabPageChangedEventArgs e = data as TabPageChangedEventArgs;
                if (e.Page.Name == "tabCargoTracking")
                {
                    if (_mainListPart.CurrentRow != null)
                    {
                        BusinessOperationContext context = new BusinessOperationContext();
                        context.Clear();
                        context.OperationID = _mainListPart.CurrentRow.ID;
                        context.Add("CompanyID", _mainListPart.CurrentRow.CompanyID);
                        _cargoTracking.DataBind(context);
                    }
                }
            };

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                OceanBookingList listData = data as OceanBookingList;
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add("ParentList", data);

                if (listData != null)
                {
                    BusinessOperationContext context = new BusinessOperationContext();
                    context.OperationID = listData.ID;
                    context.FormId = listData.OceanShippingOrderID == null ? Guid.Empty : listData.OceanShippingOrderID.Value;
                    context.FormType = ICP.Framework.CommonLibrary.Common.FormType.ShippingOrder;
                    context.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.OceanExport;

                    _eventlistPart.DataSource = context;
                    //设置文档中心数据源
                    ICP.FCM.Common.UI.FCMUIUtility.SetDocumentListDataSource(_DocumentListPart, context);
                    //设置Fax/Email/EDI数据源
                    _faxMailEDIListPart.BindData(context);
                    //CargoTracking货物跟踪数据
                    if (_mainSpace.IsSelectedCargoTracking == true)
                        _cargoTracking.DataBind(context);
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


        MemoParam CreateMemoParamInfo(OceanBookingList listData)
        {
            MemoParam para = new MemoParam();
            para.OperationId = listData.ID;
            para.FormID = listData.OceanShippingOrderID == null ? Guid.Empty : listData.OceanShippingOrderID.Value;
            para.FormType = ICP.Framework.CommonLibrary.Common.FormType.ShippingOrder;
            para.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.OceanExport;

            return para;
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
                toolBar.SetEnable("barCustoms", false);
            }
            else
            {
                toolBar.SetEnable("barSubPrint", true);
                toolBar.SetEnable("barPrintOrder", true);
                toolBar.SetEnable("barPrintInWarehouse", true);
                toolBar.SetEnable("barCopy", true);
                if (ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(listData.OceanShippingOrderID) == false)
                {
                    toolBar.SetEnable("barCancel", true);
                    //toolBar.SetEnable("barCancel", listData.BookingerName == LocalData.UserInfo.UserName || listData.BookingerName == LocalData.UserInfo.LoginName);
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
                toolBar.SetEnable("barCustoms", !string.IsNullOrEmpty(listData.ContainerNo));

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

        #region IDisposable 成员

        public void Dispose()
        {
            this._DocumentListPart = null;
            this._eventlistPart = null;
            this._fastSearchPart = null;
            this._faxMailEDIListPart = null;
            this._mainListPart.KeyDown -= this._mainListPart_KeyDown;
            this._mainListPart = null;
            this._searchPart = null;
            this._toolBar = null;
            this._cargoTracking = null;
            this._mainSpace = null;
        }

        #endregion


    }
}
