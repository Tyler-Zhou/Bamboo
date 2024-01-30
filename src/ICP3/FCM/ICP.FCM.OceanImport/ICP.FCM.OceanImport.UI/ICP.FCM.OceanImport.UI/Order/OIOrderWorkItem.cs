using ICP.Business.Common.UI.Communication;
using ICP.Business.Common.UI.Contact;
using ICP.Business.Common.UI.Document;
using ICP.Business.Common.UI.EventList;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ICP.FCM.OceanImport.UI
{
    public class OIOrderWorkitem : WorkItem
    {
        #region  服务
        #endregion
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            OIOrderMainWorkSpace mainSpce = SmartParts.Get<OIOrderMainWorkSpace>("OIOrderMainWorkSpace");
            if (mainSpce == null)
            {
                mainSpce = SmartParts.AddNew<OIOrderMainWorkSpace>("OIOrderMainWorkSpace");

                #region AddPart

                OIOrderToolBar toolBar = SmartParts.AddNew<OIOrderToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)Workspaces[OIOrderWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                OIOrderListPart listPart = SmartParts.AddNew<OIOrderListPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[OIOrderWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                OrderSearchPart searchPart = SmartParts.AddNew<OrderSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)Workspaces[OIOrderWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);


                OrderFastSearchPart fastSearchPart = SmartParts.AddNew<OrderFastSearchPart>();
                IWorkspace fastSearchPartWorkspace = (IWorkspace)Workspaces[OIOrderWorkSpaceConstants.FastSearchWorkspace];
                fastSearchPartWorkspace.Show(fastSearchPart);

                EventListPart eventListPart = SmartParts.AddNew<EventListPart>();
                IWorkspace eventListWorkspace = (IWorkspace)Workspaces[OIOrderWorkSpaceConstants.EventListWorkspace];
                eventListWorkspace.Show(eventListPart);

                UCCommunicationHistory communicationHistory = SmartParts.AddNew<UCCommunicationHistory>();
                IWorkspace communicationHistoryWorkspace = (IWorkspace)Workspaces[OIOrderWorkSpaceConstants.FaxMailEDIListWorkspace];
                communicationHistoryWorkspace.Show(communicationHistory);

                UCDocumentList documentPart = Items.AddNew<UCDocumentList>();
                DocumentListPresenter presenter = Items.AddNew<DocumentListPresenter>();
                presenter.ucList = documentPart;
                documentPart.Presenter = presenter;
                IWorkspace documentListWorkspace = (IWorkspace)Workspaces[OIOrderWorkSpaceConstants.DocumentListWorkspace];
                documentListWorkspace.Show(documentPart);

                UCContactListPart ucContactBasePart = SmartParts.AddNew<UCContactListPart>();
                IWorkspace contactListWorkspace = (IWorkspace)Workspaces[OIOrderWorkSpaceConstants.ContactListWorkspace];
                contactListWorkspace.Show(ucContactBasePart);

                #endregion

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Ocean Import Order" : "海运进口订单";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                OIOrderUIAdapter orderAdapter = new OIOrderUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(fastSearchPart.GetType().Name, fastSearchPart);
                dic.Add(eventListPart.GetType().Name, eventListPart);
                dic.Add(communicationHistory.GetType().Name, communicationHistory);
                dic.Add(documentPart.GetType().Name, documentPart);
                dic.Add(ucContactBasePart.GetType().Name, ucContactBasePart);

                orderAdapter.Init(dic);
            }
            else
            {
                Workspaces[ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }
    }

    public class OIOrderCommandConstants
    {
        public const string Command_AddData = "Command_OIOrderAddData";
        public const string Command_AIAddData = "Command_AIAddData";
        public const string Command_CancelData = "Command_OIOrderCancelData";
        public const string Command_EditData = "Command_OIOrderEditOIData";
        public const string Command_ViewReason = "Command_OIOrderViewReason";
        public const string Command_ShowSearch = "Command_OIOrderShowSearch";
        public const string Command_ConfirmBooking = "Command_OIOrderConfirmBooking";
        public const string Command_ConfirmBookingShip = "Command_OIOrderConfirmBookingShip";
        public const string Command_CopyData = "Command_OIOrderCopyData";
        public const string Command_Print = "Command_OIOrderPrint";
        public const string Command_SendEmail = "Command_OIOrderSendEmail";
        public const string Command_RefreshData = "Command_OIOrderRefreshData";

        public const string Command_FastSecharData = "Command_OIOrderFastSecharData";
        public const string Command_FaxEmail = "Command_OIOrderFaxEmail";
        public const string Command_Memo = "Command_OIOrderMemo";
        public const string Command_Document = "Command_OIOrderDocument";
        public const string Command_Contact = "Command_OIOrderContact";
        public const string Command_DetailActiveCharge = "Command_OIOrderDetailActiveCharge";
        public const string Command_ShowChildWorkspace = "Command_OIOrderShowChildWorkspace";
    }

    public class OIOrderWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";
        public const string FastSearchWorkspace = "FastSearchWorkspace";
        public const string MemoWorkspace = "FastSearchWorkspace";
        public const string ChildWorkspace = "ChildWorkspace";
        public const string DocumentListWorkspace = "DocumentListWorkspace";
        public const string ContactListWorkspace = "ContactListWorkspace";
        public const string FaxMailEDIListWorkspace = "FaxMailEDIListWorkspace";
        public const string EventListWorkspace = "EventListWorkspace";
    }

    public class OIOrderStateConstants
    {
        public const string CurrentRow = "CurrentRow";
    }

    public class OIOrderUIAdapter:IDisposable
    {

        #region parts

        IToolBar _toolBar;
        ISearchPart _searchPart;
        OIOrderListPart _mainListPart;
        ISearchPart _fastSearchPart;
        IListPart _EventlistPart;
        UCDocumentList _DocumentListPart;
        UCCommunicationHistory _ucCommunicationHistory;
        UCContactListPart _ucContactListPart;

        #endregion

        #region interface

        public void Init(Dictionary<string, object> controls)
        {

            _toolBar = (IToolBar)controls[typeof(OIOrderToolBar).Name];
            _searchPart = (ISearchPart)controls[typeof(OrderSearchPart).Name];
            _mainListPart = (OIOrderListPart)controls[typeof(OIOrderListPart).Name];
            _fastSearchPart = (ISearchPart)controls[typeof(OrderFastSearchPart).Name];
            _EventlistPart = (IListPart)controls[typeof(EventListPart).Name];
            _DocumentListPart = (UCDocumentList)controls[typeof(UCDocumentList).Name];
            _ucCommunicationHistory = (UCCommunicationHistory)controls[typeof(UCCommunicationHistory).Name];
            _ucContactListPart = (UCContactListPart)controls[typeof(UCContactListPart).Name];

            RefreshBarEnabled(null, _toolBar);
            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                OceanOrderList listData = data as OceanOrderList;
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add("ParentList", data);
                if (listData != null)
                {
                    BusinessOperationContext context = new BusinessOperationContext();
                    context.OperationID = listData.ID;
                    context.FormId = listData.ID;
                    context.FormType = FormType.Booking;
                    context.OperationType = OperationType.OceanImport;
                    context["UpdateDate"] = listData.UpdateDate;
                    _EventlistPart.DataSource = context;
                    _ucContactListPart.DataSource = context;
                    _ucCommunicationHistory.BindData(context);
                    //设置文档中心数据源
                    FCM.Common.UI.FCMUIUtility.SetDocumentListDataSource(_DocumentListPart,context);
                }

                #region toolBar

                RefreshBarEnabled(listData, _toolBar);

                #endregion
            };
            #endregion

            #region
            _mainListPart.KeyDown += new KeyEventHandler(_mainListPart_KeyDown);
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

            #endregion

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
        private void RefreshBarEnabled(OceanOrderList listData, IToolBar toolBar)
        {
            if (listData == null || listData.IsNew)
            {
                toolBar.SetEnable("barCopy", false);
                toolBar.SetEnable("barCancel", false);
                toolBar.SetEnable("barEdit", false);
                toolBar.SetEnable("barPrint", false);
                toolBar.SetEnable("barConfirmBooking", false);
                toolBar.SetEnable("barConfirmBookingShip", false);
            }
            else
            {

                toolBar.SetEnable("barCopy", true);
                toolBar.SetEnable("barCancel", true);
                toolBar.SetEnable("barEdit", true);
                toolBar.SetEnable("barPrint", true);
                toolBar.SetEnable("barConfirmBooking", false);
                toolBar.SetEnable("barConfirmBookingShip", false);

                #region 新订单
                if (listData.State != OIOrderState.NewOrder)
                {
                    toolBar.SetEnable("barCancel", false);
                }
                else
                {
                    toolBar.SetEnable("barCancel", true);
                }
                #endregion

                #region 无效的
                if (listData.IsValid == false)
                {
                    toolBar.SetText("barCancel", LocalData.IsEnglish ? "Available(&D)" : "激活(&D)");
                    toolBar.SetEnable("barConfirmBooking", false);
                    toolBar.SetEnable("barConfirmBookingShip", false);

                }
                else
                {
                    toolBar.SetText("barCancel", LocalData.IsEnglish ? "Cancel(&D)" : "取消(&D)");

                }
                #endregion

                #region 退回的
                if (listData.State == OIOrderState.Rejected)
                {
                    toolBar.SetEnable("barViewReason", true);
                }
                else
                {
                    toolBar.SetEnable("barViewReason", false);
                }
                #endregion

                #region 符合订舱的
                if ((listData.State == OIOrderState.NewOrder || listData.State == OIOrderState.Checked) && listData.IsValid)
                {
                    toolBar.SetEnable("barConfirmBooking", true);
                }
                else
                {
                    toolBar.SetEnable("barConfirmBooking", false);
                }

                #endregion

                #region 符合装船的
                if ((listData.State == OIOrderState.NewOrder || listData.State == OIOrderState.Checked || listData.State == OIOrderState.BookingConfirmed) && listData.IsValid)
                {
                    toolBar.SetEnable("barConfirmBookingShip", true);
                }
                else
                {
                    toolBar.SetEnable("barConfirmBookingShip", false);
                }
                #endregion
            }
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            _DocumentListPart = null;
            _EventlistPart = null;
            _fastSearchPart = null;
            _mainListPart.KeyDown -= _mainListPart_KeyDown;
            _mainListPart = null;
            _searchPart = null;
            _toolBar = null;
            _ucCommunicationHistory = null;
            _ucContactListPart = null;
        }

        #endregion
    }
}
