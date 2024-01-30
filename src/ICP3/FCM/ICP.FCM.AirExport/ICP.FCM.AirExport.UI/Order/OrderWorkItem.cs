﻿using System.Collections.Generic;
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

namespace ICP.FCM.AirExport.UI.Order
{
    public class OrderWorkitem : WorkItem
    {

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void Show()
        {
            OrderMainWorkspace mainSpce = SmartParts.Get<OrderMainWorkspace>("OrderMainWorkspace");
            if (mainSpce == null)
            {
                mainSpce = SmartParts.AddNew<OrderMainWorkspace>("OrderMainWorkspace");

                #region AddPart

                OrderMainToolBar toolBar = SmartParts.AddNew<OrderMainToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)Workspaces[AEOrderWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                OrderListPart listPart = SmartParts.AddNew<OrderListPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[AEOrderWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                OrderSearchPart searchPart = SmartParts.AddNew<OrderSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)Workspaces[AEOrderWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);


                OrderFastSearchPart fastSearchPart = SmartParts.AddNew<OrderFastSearchPart>();
                IWorkspace fastSearchPartWorkspace = (IWorkspace)Workspaces[AEOrderWorkSpaceConstants.FastSearchWorkspace];
                fastSearchPartWorkspace.Show(fastSearchPart);


                EventListPart memoListPart = SmartParts.AddNew<EventListPart>();
                IWorkspace EventListWorkspace = (IWorkspace)Workspaces[AEOrderWorkSpaceConstants.EventListWorkspace];
                EventListWorkspace.Show(memoListPart);


                UCCommunicationHistory communicationHistory = SmartParts.AddNew<UCCommunicationHistory>();
                IWorkspace communicationHistoryWorkSpace = (IWorkspace)Workspaces[AEOrderWorkSpaceConstants.FaxMailEDIListWorkspace];
                communicationHistoryWorkSpace.Show(communicationHistory);


                UCDocumentList documentPart = Items.AddNew<UCDocumentList>();
                DocumentListPresenter presenter = Items.AddNew<DocumentListPresenter>();
                presenter.ucList = documentPart;
                documentPart.Presenter = presenter;
                IWorkspace documentPartWorkSapce = (IWorkspace)Workspaces[AEOrderWorkSpaceConstants.DocumentListWorkspace];
                documentPartWorkSapce.Show(documentPart);

                #endregion

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "AirOrder" : "空运出口订单";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                AEOrderUIAdapter orderAdapter = new AEOrderUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(fastSearchPart.GetType().Name, fastSearchPart);
                dic.Add(memoListPart.GetType().Name, memoListPart);
                dic.Add(documentPart.GetType().Name, documentPart);
                dic.Add(communicationHistory.GetType().Name, communicationHistory);
                orderAdapter.Init(dic);
            }
            else
            {
                Workspaces[ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }
    }

    public class AEOrderCommandConstants
    {
        public const string Command_AddData = "Command_AddData";
        public const string Command_CancelData = "Command_CancelData";
        public const string Command_EditData = "Command_EditData";
        public const string Command_ViewReason = "Command_ViewReason";
        public const string Command_ShowSearch = "Command_ShowSearch";
        public const string Command_CopyData = "Command_CopyData";
        public const string Command_Print = "Command_Print";
        public const string Command_Document = "Command_Document";
        public const string Command_FaxEmail = "Command_FaxEmail";
        public const string Command_Memo = "Command_Memo";
        public const string Command_SendEmail = "Command_SendEmail";
        public const string Command_RefreshData = "Command_RefreshData";

        public const string Command_ShowChildWorkspace = "Command_ShowChildWorkspace";
    }

    public class AEOrderWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";
        public const string FastSearchWorkspace = "FastSearchWorkspace";
        public const string ChildWorkspace = "ChildWorkspace";
        public const string DocumentListWorkspace = "DocumentListWorkspace";
        public const string FaxMailEDIListWorkspace = "FaxMailEDIListWorkspace";
        public const string EventListWorkspace = "EventListWorkspace";
    }

    public class AEOrderStateConstants
    {
        public const string CurrentRow = "CurrentRow";
    }

    public class AEOrderUIAdapter
    {

        #region parts

        IToolBar _toolBar;
        ISearchPart _searchPart;
        OrderListPart _mainListPart;
        ISearchPart _fastSearchPart;
        IListPart _eventlistPart;
        UCCommunicationHistory _ucCommunicationHistory;

        UCDocumentList _DocumentListPart;

        #endregion

        #region interface

        public void Init(Dictionary<string, object> controls)
        {

            _toolBar = (IToolBar)controls[typeof(OrderMainToolBar).Name];
            _searchPart = (ISearchPart)controls[typeof(OrderSearchPart).Name];
            _mainListPart = (OrderListPart)controls[typeof(OrderListPart).Name];
            _fastSearchPart = (ISearchPart)controls[typeof(OrderFastSearchPart).Name];
            _eventlistPart = (IListPart)controls[typeof(EventListPart).Name];
            _ucCommunicationHistory = (UCCommunicationHistory)controls[typeof(UCCommunicationHistory).Name];
            _DocumentListPart = (UCDocumentList)controls[typeof(UCDocumentList).Name];

            AirOrderList firstRow = _mainListPart.Current as AirOrderList;
            if (firstRow != null)
            {
                RefreshBarEnabled(_toolBar, firstRow);
            }
            //RefreshBarEnabled(_toolBar, null);

            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                AirOrderList listData = data as AirOrderList;
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
                    //设置文档中心数据源
                    FCM.Common.UI.FCMUIUtility.SetDocumentListDataSource(_DocumentListPart, context);
                    _ucCommunicationHistory.BindData(context);
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

            #endregion

            //_searchPart.RaiseSearched();

            _mainListPart.KeyDown += new KeyEventHandler(_mainListPart_KeyDown);

            //_fastSearchPart.RaiseSearched();
            //_searchPart.RaiseSearched();
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
        private void RefreshBarEnabled(IToolBar toolBar, AirOrderList listData)
        {
            if (listData == null || listData.IsNew)
            {
                toolBar.SetEnable("barCancel", false);
                toolBar.SetEnable("barEdit", false);
                toolBar.SetEnable("barShowSearch", false);
                toolBar.SetEnable("barCopy", false);
                toolBar.SetEnable("barPrint", false);
                toolBar.SetEnable("barDocument", false);
                toolBar.SetEnable("barFaxEmail", false);
                toolBar.SetEnable("barMemo", false);
                toolBar.SetEnable("barSendMail", false);
                toolBar.SetEnable("barCopy", false);
                toolBar.SetEnable("barViewReason", false);
            }
            else
            {

                toolBar.SetEnable("barEdit", true);
                toolBar.SetEnable("barShowSearch", true);
                toolBar.SetEnable("barCopy", true);
                toolBar.SetEnable("barPrint", true);
                toolBar.SetEnable("barDocument", true);
                toolBar.SetEnable("barFaxEmail", true);
                toolBar.SetEnable("barMemo", true);
                toolBar.SetEnable("barSendMail", true);

                if (listData.State != AEOrderState.NewOrder)
                    //toolBar.SetEnable("barCancel", false);
                    toolBar.SetEnable("barCancel", true);
                else
                    toolBar.SetEnable("barCancel", true);

                if (listData.IsValid == false)
                    toolBar.SetText("barCancel", LocalData.IsEnglish ? "Available(&D)" : "激活(&D)");
                else
                    toolBar.SetText("barCancel", LocalData.IsEnglish ? "Cancel(&D)" : "取消(&D)");

                if (listData.State == AEOrderState.Rejected)
                    toolBar.SetEnable("barViewReason", true);
                else
                    toolBar.SetEnable("barViewReason", false);
            }
        }

        #endregion
    }
}
