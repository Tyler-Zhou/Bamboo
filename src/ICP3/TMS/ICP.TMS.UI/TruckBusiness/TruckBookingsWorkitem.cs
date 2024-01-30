using ICP.Business.Common.UI.Communication;
using ICP.Business.Common.UI.Document;
using ICP.Business.Common.UI.EventList;
using ICP.FCM.Common.UI;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.TMS.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;

namespace ICP.TMS.UI
{
    public class TruckBookingsWorkitem : WorkItem
    {
        #region Service

        #endregion

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            TruckBookingsMainWorkSpace mainSpce = this.SmartParts.Get<TruckBookingsMainWorkSpace>("TruckBookingsMainWorkSpace");
            if (mainSpce == null)
            {
                mainSpce = this.SmartParts.AddNew<TruckBookingsMainWorkSpace>("TruckBookingsMainWorkSpace");

                #region AddPart

                TruckBookingsToolBar toolBar = this.SmartParts.AddNew<TruckBookingsToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[TruckBookingsWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                TruckBookingsListPart listPart = this.SmartParts.AddNew<TruckBookingsListPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[TruckBookingsWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                TruckBookingsSearchPart searchPart = this.SmartParts.AddNew<TruckBookingsSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[TruckBookingsWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);

                EventListPart  eventListPart = this.Items.AddNew<EventListPart>();
                IWorkspace memoListWorkspace = (IWorkspace)this.Workspaces[TruckBookingsWorkSpaceConstants.EventListWorkspace];
                memoListWorkspace.Show(eventListPart);

                //ICP.FCM.Common.UI.FaxEMailLog.FaxEMailLogListPart faxMailListPart = this.Items.AddNew<ICP.FCM.Common.UI.FaxEMailLog.FaxEMailLogListPart>();
                //IWorkspace faxMailListWorkspace = (IWorkspace)this.Workspaces[TruckBookingsWorkSpaceConstants.FaxMailListWorkspace];
                //faxMailListWorkspace.Show(faxMailListPart);
                UCCommunicationHistory faxMailEDIListPart = this.Items.AddNew<UCCommunicationHistory>();
                IWorkspace faxMailListWorkspace = (IWorkspace)this.Workspaces[TruckBookingsWorkSpaceConstants.FaxMailListWorkspace];
                faxMailListWorkspace.Show(faxMailEDIListPart);

                UCDocumentList documentPart = this.Items.AddNew<UCDocumentList>();
                DocumentListPresenter presenter = this.Items.AddNew<DocumentListPresenter>();
                presenter.ucList = documentPart;
                documentPart.Presenter = presenter;
                IWorkspace documentPartWorkspace = (IWorkspace)this.Workspaces[TruckBookingsWorkSpaceConstants.DocumentListWorkspace];
                documentPartWorkspace.Show(documentPart);
                #endregion

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = NativeLanguageService.GetText(mainSpce, "TruckBookings");
                mainWorkspace.Show(mainSpce, smartPartInfo);


                TruckBookingsUIAdapter bookingAdapter = new TruckBookingsUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(eventListPart.GetType().Name, eventListPart);
                dic.Add(faxMailEDIListPart.GetType().Name, faxMailEDIListPart);
                dic.Add(documentPart.GetType().Name, documentPart);
                bookingAdapter.Init(dic);
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }
    }

    /// <summary>
    /// 命常量
    /// </summary>
    public class TruckBookingsCommandConstants
    {

        public const string Command_Add = "Command_Add";

        public const string Command_Edit = "Command_Edit";

        public const string Command_Cancel = "Command_Cancel";

        public const string Command_Delete = "Command_Delete";

        public const string Command_ShowSearch = "Command_ShowSearch";

        public const string Command_Truck = "Command_Truck";

        public const string Command_Print = "Command_Print";

        public const string Command_Bill = "Command_Bill";

        public const string Command_Refresh = "Command_Refresh";

        public const string Command_Copy = "Command_Copy";

        public const string Command_DownLoadBusiness = "Command_DownLoadBusiness";


    }
    /// <summary>
    /// WorkSpace常量
    /// </summary>
    public class TruckBookingsWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";
        public const string EventListWorkspace = "EventListWorkspace";
        public const string FaxMailListWorkspace = "FaxMailListWorkspace";
        public const string DocumentListWorkspace = "DocumentListWorkspace";

    }

    /// <summary>
    /// UI适配器
    /// </summary>
    public class TruckBookingsUIAdapter:IDisposable
    {


        #region parts
        IToolBar _toolBar;
        ISearchPart _searchPart;
        TruckBookingsListPart _mainListPart;
        IListPart _eventlistPart;
        UCCommunicationHistory _uCCommunicationHistory;
        UCDocumentList _DocumentListPart;
        #endregion

        #region interface

        public void Init(Dictionary<string, object> controls)
        {
            _toolBar = (IToolBar)controls[typeof(TruckBookingsToolBar).Name];
            _searchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(TruckBookingsSearchPart).Name];
            _mainListPart = (TruckBookingsListPart)controls[typeof(TruckBookingsListPart).Name];
            _eventlistPart = (IListPart)controls[typeof(EventListPart).Name];
            _uCCommunicationHistory = (UCCommunicationHistory)controls[typeof(UCCommunicationHistory).Name];
            _DocumentListPart = (UCDocumentList)controls[typeof(UCDocumentList).Name];

            RefreshBarEnabled(_toolBar, null);
            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                TruckBookingsList listData = data as TruckBookingsList;
                if (listData != null)
                {
                    BusinessOperationContext context = new BusinessOperationContext();
                    context.OperationID = listData.ID;
                    context.FormId = listData.ID;
                    context.FormType = FormType.Truck;
                    context.OperationType = OperationType.Truck;
                    _eventlistPart.DataSource = context;
                    //_faxMailListPart.DataSource = para;
                    // 设置文档中心数据源
                    FCMUIUtility.SetDocumentListDataSource(_DocumentListPart,context);
                    _uCCommunicationHistory.BindData(context);
                }
                RefreshBarEnabled(_toolBar, listData);

            };
            #endregion

            #region
            _mainListPart.KeyDown += new System.Windows.Forms.KeyEventHandler(_mainListPart_KeyDown);
            #endregion
            #region _searchPart.OnSearched
            _searchPart.OnSearched += delegate(object sender, object results)
            {
                _mainListPart.DataSource = results;
            };

            #endregion


            #endregion
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
        private void RefreshBarEnabled(IToolBar toolBar, TruckBookingsList listData)
        {
            if (listData == null || listData.IsNew)
            {
                toolBar.SetEnable("barEdit", false);
                toolBar.SetEnable("barVoid", false);
                toolBar.SetEnable("barDelete", false);
                toolBar.SetEnable("barPrint", false);
                toolBar.SetEnable("barDispatch", false);
                toolBar.SetEnable("barBill", false);
            }
            else
            {
                toolBar.SetEnable("barEdit", true);
                toolBar.SetEnable("barVoid", true);
                toolBar.SetEnable("barDelete", true);
                toolBar.SetEnable("barPrint", true);
                toolBar.SetEnable("barDispatch", true);
                toolBar.SetEnable("barBill", true);

                if (listData.ContainerID == null || listData.ContainerID == Guid.Empty)
                {
                    toolBar.SetEnable("barDelete", false);
                    toolBar.SetEnable("barPrint", false);
                }

                if (listData.IsValid)
                {
                    toolBar.SetText("barVoid", LocalData.IsEnglish ? "Invalid" : "作废");
                }
                else
                {
                    toolBar.SetText("barVoid", LocalData.IsEnglish ? "Activation" : "激活");

                    toolBar.SetEnable("barEdit", false);
                    toolBar.SetEnable("barDelete", false);
                    toolBar.SetEnable("barPrint", false);
                    toolBar.SetEnable("barDispatch", false);

                    if (listData.State != TruckBusinessState.NoTruck)
                    {
                        toolBar.SetEnable("barDelete", false);
                    }
                    else
                    {
                        toolBar.SetEnable("barDelete", true);
                    }
                }



            }

        }
        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
     
            this._DocumentListPart = null;
            this._eventlistPart = null;
            this._uCCommunicationHistory = null;
            this._mainListPart = null;
            this._searchPart = null;
            this._toolBar = null;
        }

        #endregion
    }

}
