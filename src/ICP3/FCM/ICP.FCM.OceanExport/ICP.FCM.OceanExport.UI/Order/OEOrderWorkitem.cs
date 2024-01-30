using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using System;
using ICP.Business.Common.UI.Communication;
using ICP.Business.Common.UI.Document;
using ICP.FCM.Common.ServiceInterface;

namespace ICP.FCM.OceanExport.UI.Order
{
    public class OEOrderWorkitem : WorkItem
    {

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            OEOrderMainWorkspace mainSpce = this.SmartParts.Get<OEOrderMainWorkspace>("OEOrderMainWorkspace");
            if (mainSpce == null)
            {
                mainSpce = this.SmartParts.AddNew<OEOrderMainWorkspace>("OEOrderMainWorkspace");

                #region AddPart

                OEOrderToolBar toolBar = this.SmartParts.AddNew<OEOrderToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[OEOrderWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                OEOrderListPart listPart = this.SmartParts.AddNew<OEOrderListPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[OEOrderWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                OrderSearchPart searchPart = this.SmartParts.AddNew<OrderSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[OEOrderWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);


                OrderFastSearchPart fastSearchPart = this.SmartParts.AddNew<OrderFastSearchPart>();
                IWorkspace fastSearchPartWorkspace = (IWorkspace)this.Workspaces[OEOrderWorkSpaceConstants.FastSearchWorkspace];
                fastSearchPartWorkspace.Show(fastSearchPart);

                ICP.Business.Common.UI.EventList.EventListPart eventListPart = this.SmartParts.AddNew<ICP.Business.Common.UI.EventList.EventListPart>();
                IWorkspace eventListWorkspace = (IWorkspace)this.Workspaces[OEOrderWorkSpaceConstants.EventListWorkspace];
                eventListWorkspace.Show(eventListPart);


                UCCommunicationHistory communicationHistroy = this.SmartParts.AddNew<UCCommunicationHistory>("FaxMailEDIListPart");
                IWorkspace communicationHistoryWorkSpace = (IWorkspace)this.Workspaces[OEOrderWorkSpaceConstants.FaxMailEDIListWorkspace];
                communicationHistoryWorkSpace.Show(communicationHistroy);


                UCDocumentList documentListPart = this.Items.AddNew<UCDocumentList>();
                DocumentListPresenter documentPresenter = this.Items.AddNew<DocumentListPresenter>();
                documentPresenter.ucList = documentListPart;
                documentListPart.Presenter = documentPresenter;

                IWorkspace documentListWorkspace = (IWorkspace)this.Workspaces[OEOrderWorkSpaceConstants.DocumentListWorkspace];
                documentListWorkspace.Show(documentListPart);

                #endregion

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Ocean Export Order List" : "海运出口订单";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                OEOrderUIAdapter orderAdapter = new OEOrderUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(fastSearchPart.GetType().Name, fastSearchPart);
                dic.Add(eventListPart.GetType().Name, eventListPart);
                dic.Add(communicationHistroy.GetType().Name, communicationHistroy);
                dic.Add(documentListPart.GetType().Name, documentListPart);
                orderAdapter.Init(dic);
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }
    }

    public class OEOrderCommandConstants
    {
        public const string Command_AddData = "Command_OEOAddData";
        public const string Command_CancelData = "Command_OEOCancelData";
        public const string Command_EditData = "Command_OEOEditData";
        public const string Command_ViewReason = "Command_OEOViewReason";
        public const string Command_ShowSearch = "Command_OEOShowSearch";
        public const string Command_CopyData = "Command_OEOCopyData";
        public const string Command_Print = "Command_OEOPrint";
        public const string Command_Document = "Command_OEODocument";
        public const string Command_OEOFaxEmail = "Command_OEOFaxEmail";
        public const string Command_Memo = "Command_OEOMemo";
        public const string Command_SendEmail = "Command_OEOSendEmail";
        public const string Command_RefreshData = "Command_OEORefreshData";
        public const string Command_AgentFilerSearch = "Command_AgentFilerSearch";
        public const string Command_ShowChildWorkspace = "Command_OEOShowChildWorkspace";
    }

    public class OEOrderWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";
        public const string FastSearchWorkspace = "FastSearchWorkspace";
        public const string ChildWorkspace = "ChildWorkspace";
        public const string DocumentListWorkspace = "DocumentListWorkspace";
        public const string FaxMailEDIListWorkspace = "FaxMailEDIListWorkspace";
        public const string EventListWorkspace = "EventListWorkspace";
        public const string PreviewWorkspace = "priviewWorkspace";
    }

    public class OEOrderStateConstants
    {
        public const string CurrentRow = "CurrentRow";
    }

    public class OEOrderUIAdapter : IDisposable
    {

        #region parts

        IFCMCommonService FCMCommonService
        {
            get { return ServiceClient.GetService<IFCMCommonService>(); }
        }
        IToolBar _toolBar;
        ISearchPart _searchPart;
        OEOrderListPart _mainListPart;
        ISearchPart _fastSearchPart;
        IListPart _eventlistPart;
        UCCommunicationHistory _ucCommunicationHistory;
        UCDocumentList _ucDocumentList;

        #endregion

        #region interface

        public void Init(Dictionary<string, object> controls)
        {

            _toolBar = (IToolBar)controls[typeof(OEOrderToolBar).Name];
            _searchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(OrderSearchPart).Name];
            _mainListPart = (OEOrderListPart)controls[typeof(OEOrderListPart).Name];
            _fastSearchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(OrderFastSearchPart).Name];
            _eventlistPart = (IListPart)controls[typeof(ICP.Business.Common.UI.EventList.EventListPart).Name];
            _ucDocumentList = (UCDocumentList)controls[typeof(UCDocumentList).Name];
            _ucCommunicationHistory = (UCCommunicationHistory)controls[typeof(UCCommunicationHistory).Name];

            RefreshBarEnabled(_toolBar, null);

            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                ICP.FCM.OceanExport.ServiceInterface.DataObjects.OceanOrderList listData = data as ICP.FCM.OceanExport.ServiceInterface.DataObjects.OceanOrderList;
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add("ParentList", data);

                if (listData != null)
                {
                    BusinessOperationContext context = new BusinessOperationContext();
                    context.OperationID = listData.ID;
                    context.FormId = listData.ID;
                    context.FormType = FormType.Booking;
                    context.OperationType = OperationType.OceanExport;

                    _eventlistPart.DataSource = context;
                    //设置文档中心数据源
                    ICP.FCM.Common.UI.FCMUIUtility.SetDocumentListDataSource(_ucDocumentList, context);
                    _ucCommunicationHistory.BindData(context);

                    if ((_ucDocumentList.DataSource == null || _ucDocumentList.DataSource.Count == 0) && (_ucCommunicationHistory.DataSource == null || _ucCommunicationHistory.DataSource.Count == 0))
                    {
                        BusinessOperationContext oicontext = new BusinessOperationContext();
                        List<SimpleBusinnessInfo> result = FCMCommonService.GetOEIDByOIID(listData.ID);
                        if (result != null && result.Count > 0)
                        {
                            context.OperationID = result[0].OIBusinessID;
                            context.FormId = result[0].OIBusinessID;
                            context.FormType = FormType.Booking;
                            context.OperationType = OperationType.OceanImport;

                            //设置文档中心数据源
                            ICP.FCM.Common.UI.FCMUIUtility.SetDocumentListDataSource(_ucDocumentList, context);
                            _ucCommunicationHistory.BindData(context);
                        }

                    }
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

            _mainListPart.KeyDown += new System.Windows.Forms.KeyEventHandler(_mainListPart_KeyDown);

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
        private void RefreshBarEnabled(IToolBar toolBar, OceanOrderList listData)
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

                toolBar.SetEnable("barCopy", true);
                toolBar.SetEnable("barEdit", true);
                toolBar.SetEnable("barShowSearch", true);
                //toolBar.SetEnable("barCopy", listData.SalesName == LocalData.UserInfo.LoginName);
                toolBar.SetEnable("barPrint", true);
                toolBar.SetEnable("barDocument", true);
                toolBar.SetEnable("barFaxEmail", true);
                toolBar.SetEnable("barMemo", true);
                toolBar.SetEnable("barSendMail", true);

                if (listData.State != OEOrderState.NewOrder)
                    toolBar.SetEnable("barCancel", false);
                else
                    toolBar.SetEnable("barCancel", true);

                if (listData.IsValid == false)
                    toolBar.SetText("barCancel", LocalData.IsEnglish ? "Available(&D)" : "激活(&D)");
                else
                    toolBar.SetText("barCancel", LocalData.IsEnglish ? "Cancel(&D)" : "取消(&D)");

                if (listData.State == OEOrderState.Rejected)
                    toolBar.SetEnable("barViewReason", true);
                else
                    toolBar.SetEnable("barViewReason", false);
            }
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            this._eventlistPart = null;
            this._fastSearchPart = null;
            this._mainListPart.KeyDown -= this._mainListPart_KeyDown;
            this._mainListPart = null;
            this._searchPart = null;
            this._toolBar = null;
            this._ucCommunicationHistory = null;
            this._ucDocumentList = null;
        }

        #endregion
    }
}
