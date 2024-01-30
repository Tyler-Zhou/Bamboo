using System.Collections.Generic;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.AirImport.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Business.Common.UI.EventList;
using ICP.Business.Common.UI.Communication;
using ICP.Business.Common.UI.Document;

namespace ICP.FCM.AirImport.UI
{
    public class OIBusinessWorkitem : WorkItem
    {
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }
        /// <summary>
        /// 显示界面
        /// </summary>
        private void Show()
        {
            OIBusinessMainWorkSpace mainSpce = SmartParts.Get<OIBusinessMainWorkSpace>("OIBusinessrMainWorkspace");
            if (mainSpce == null)
            {
                mainSpce = SmartParts.AddNew<OIBusinessMainWorkSpace>("OIBusinessrMainWorkspace");

                #region AddPart

                OIBusinessTool toolBar = SmartParts.AddNew<OIBusinessTool>();
                IWorkspace toolBarWorkspace = (IWorkspace)Workspaces[OIBusinessWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                OIBusinessList listPart = SmartParts.AddNew<OIBusinessList>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[OIBusinessWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                OIBusinessSearch searchPart = SmartParts.AddNew<OIBusinessSearch>();
                IWorkspace searchWorkspace = (IWorkspace)Workspaces[OIBusinessWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);


                OIBusinessFastSearch fastSearchPart = SmartParts.AddNew<OIBusinessFastSearch>();
                IWorkspace fastSearchWorkspace = (IWorkspace)Workspaces[OIBusinessWorkSpaceConstants.FastSearchWorkspace];
                fastSearchWorkspace.Show(fastSearchPart);

                EventListPart eventListPart = Items.AddNew<EventListPart>();
                IWorkspace EventListWorkspace = (IWorkspace)Workspaces[OIBusinessWorkSpaceConstants.EventListWorkspace];
                EventListWorkspace.Show(eventListPart);

                UCCommunicationHistory faxMailListPart = Items.AddNew<UCCommunicationHistory>();
                IWorkspace faxMailListWorkspace = (IWorkspace)Workspaces[OIBusinessWorkSpaceConstants.FaxMailListWorkspace];
                faxMailListWorkspace.Show(faxMailListPart);

                UCDocumentList documentPart = SmartParts.AddNew<UCDocumentList>();
                DocumentListPresenter documentPresenter = Items.AddNew<DocumentListPresenter>();
                documentPresenter.ucList = documentPart;
                documentPart.Presenter = documentPresenter;
                IWorkspace documentListWorkSpace = (IWorkspace)Workspaces[OIBusinessWorkSpaceConstants.DocumentListWorkspace];
                documentListWorkSpace.Show(documentPart);

                #endregion

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "AirImportBusiness" : "空运进口业务";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                OIBusinessUIAdapter orderAdapter = new OIBusinessUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(fastSearchPart.GetType().Name, fastSearchPart);
                dic.Add(eventListPart.GetType().Name, eventListPart);
                dic.Add(faxMailListPart.GetType().Name, faxMailListPart);
                dic.Add(documentPart.GetType().Name, documentPart);
                orderAdapter.Init(dic);
            }
            else
            {
                Workspaces[ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }
    }
    /// <summary>
    /// 命令常量
    /// </summary>
    public class AIBusinessCommandConstants
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
        public const string Command_Bill = "Command_Bill";
        public const string Command_Memo = "Command_Memo";
        public const string Command_SendEmail = "Command_SendEmail";
        public const string Command_RefreshData = "Command_RefreshData";

        public const string Command_FastSecharData = "Command_FastSecharData";
        /// 下载
        public const string Command_DownLoad = "Command_DownLoad";
        /// 下载后保存
        public const string Command_AIInsertToListAfterDownLoad = "Command_AIInsertToListAfterDownLoad";
        ///转移
        public const string Command_BusinessTransfer = "Command_BusinessTransfer";
        ///提货通知书
        public const string Command_CargoBook = "Command_CargoBook";
        /// 跟踪
        public const string Command_BoxTracking = "Command_BoxTrack";
        /// 放货
        public const string Command_Delivery = "Command_Delivery";
        /// 确认订舱
        public const string Command_ConfirmBooking = "Command_ConfirmBooking";
        /// 确认装船
        public const string Command_ConfirmBookingShip = "Command_ConfirmBookingShip";


        public const string Command_ShowChildWorkspace = "Command_ShowChildWorkspace";

        public const string Command_PrintArrivalNotice = "Command_PrintArrivalNotice";
        public const string Command_PrintReleaseOrder = "Command_PrintReleaseOrder";
        public const string Command_PrintPickUp = "Command_PrintPickUp";
        public const string Command_PrintProfit = "Command_PrintProfit";
        public const string Command_PrintWorkSheet = "Command_PrintWorkSheet";
        public const string Command_PrintForwardingBill = "Command_PrintForwardingBill";
        public const string Command_PrintAuthority = "Command_PrintAuthority";
    }

    /// <summary>
    /// 面板名称常量
    /// </summary>
    public class OIBusinessWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";
        public const string FastSearchWorkspace = "FastSearchWorkspace";
        public const string FeeWorkspace = "FeeWorkspace";
        public const string EventListWorkspace = "EventListWorkspace";
        public const string FaxMailListWorkspace = "FaxMailEDIListWorkspace";
        public const string DocumentListWorkspace = "DocumentListWorkspace";

    }

    public class OIBusinessStateConstants
    {
        public const string CurrentRow = "CurrentRow";
    }

    public class OIBusinessUIAdapter
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region parts

        OIBusinessTool _toolBar;
        ISearchPart _searchPart;
        OIBusinessList _mainListPart;
        ISearchPart _fastSearchPart;
        IListPart _eventlistPart;
        UCDocumentList _ucDocumentList;
        UCCommunicationHistory _ucCommunicationHistory;

        #endregion

        #region interface

        public void Init(Dictionary<string, object> controls)
        {

            _toolBar = (OIBusinessTool)controls[typeof(OIBusinessTool).Name];
            _searchPart = (ISearchPart)controls[typeof(OIBusinessSearch).Name];
            _mainListPart = (OIBusinessList)controls[typeof(OIBusinessList).Name];
            _fastSearchPart = (ISearchPart)controls[typeof(OIBusinessFastSearch).Name];
            _eventlistPart = (IListPart)controls[typeof(EventListPart).Name];
            _ucCommunicationHistory = (UCCommunicationHistory)controls[typeof(UCCommunicationHistory).Name];
            _ucDocumentList = (UCDocumentList)controls[typeof(UCDocumentList).Name];


            AirBusinessList firstRow = _mainListPart.Current as AirBusinessList;
            if (firstRow != null)
            {
                RefreshBarEnabled(_toolBar, firstRow);
            }

            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                AirBusinessList listData = data as AirBusinessList;
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add("ParentList", data);

                if (listData != null)
                {
                    BusinessOperationContext context = new BusinessOperationContext();
                    context.OperationID = listData.ID;
                    context.FormId = listData.ID;
                    context.FormType = FormType.Booking;
                    context.OperationType = OperationType.AirImport;

                    _eventlistPart.DataSource = context;
                    //_faxMailListPart.DataSource = para;
                    FCM.Common.UI.FCMUIUtility.SetDocumentListDataSource(_ucDocumentList,context);
                    _ucCommunicationHistory.BindData(context);
                }

                #region toolBar

                RefreshBarEnabled(_toolBar, listData);

                #endregion
            };
            #endregion

            _mainListPart.KeyDown += new KeyEventHandler(_mainListPart_KeyDown);

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
        private void RefreshBarEnabled(OIBusinessTool toolBar, AirBusinessList listData)
        {
            if (listData == null || listData.IsNew)
            {

                toolBar.SetEnable("barCancel", false);
                toolBar.SetEnable("barCopy", false);
                toolBar.SetEnable("barEdit", false);
                toolBar.SetEnable("barPrint", false);
                toolBar.SetEnable("barBusinessTransfer", false);
                toolBar.SetEnable("barCargoBook", false);
                toolBar.SetEnable("barDelivery", false);
                toolBar.SetEnable("barConfirmBooking", false);
                toolBar.SetEnable("barConfirmBookingShip", false);
                toolBar.SetEnable("barBill", false);

            }
            else
            {
                toolBar.SetEnable("barCancel", true);
                toolBar.SetEnable("barCopy", true);

                if (!listData.IsValid)
                {
                    toolBar.SetEnable("barEdit", false);
                    toolBar.SetEnable("barPrint", false);
                    toolBar.SetEnable("barBusinessTransfer", false);
                    toolBar.SetEnable("barCargoBook", false);
                    toolBar.SetEnable("barDelivery", false);
                    toolBar.SetEnable("barConfirmBooking", false);
                    toolBar.SetEnable("barConfirmBookingShip", false);
                    toolBar.SetEnable("barBill", false);

                    toolBar.SetText("barCancel", LocalData.IsEnglish ? "Available(&D)" : "激活(&D)");
                }
                else
                {
                    toolBar.SetEnable("barEdit", true);
                    toolBar.SetEnable("barPrint", true);
                    toolBar.SetEnable("barBusinessTransfer", true);
                    toolBar.SetEnable("barBoxTracking", true);
                    toolBar.SetEnable("barCargoBook", true);
                    toolBar.SetEnable("barDelivery", true);
                    toolBar.SetEnable("barBill", true);

                    toolBar.SetText("barCancel", LocalData.IsEnglish ? "Cancel(&D)" : "取消(&D)");

                    if (listData.State != AIOrderState.NewOrder)
                    {
                        toolBar.SetEnable("barCancel", false);
                    }
                    else
                    {
                        toolBar.SetEnable("barCancel", true);
                    }

                    #region 符合订舱的
                    if ((listData.State == AIOrderState.NewOrder || listData.State == AIOrderState.Checked) && listData.IsValid)
                    {
                        toolBar.SetEnable("barConfirmBooking", true);
                    }
                    else
                    {
                        toolBar.SetEnable("barConfirmBooking", false);
                    }

                    #endregion

                    #region 符合装船的
                    if ((listData.State == AIOrderState.NewOrder || listData.State == AIOrderState.Checked || listData.State == AIOrderState.ArrivalNoticeSended) && listData.IsValid)
                    {
                        toolBar.SetEnable("barConfirmBookingShip", true);
                    }
                    else
                    {
                        toolBar.SetEnable("barConfirmBookingShip", false);
                    }
                    #endregion

                    #region 已放货的
                    if (listData.State == AIOrderState.Release)
                    {
                        toolBar.SetEnable("barConfirmBookingShip", false);
                        toolBar.SetEnable("barConfirmBooking", false);
                        toolBar.SetEnable("barDelivery", true);

                        toolBar.SetCancelDelivery(true);
                    }
                    else
                    {
                        toolBar.SetEnable("barDelivery", true);
                        toolBar.SetCancelDelivery(false);
                    }

                    #endregion

                    #region 已打回的
                    if (listData.State == AIOrderState.Rejected)
                    {
                        toolBar.SetEnable("barBusinessTransfer", false);
                        toolBar.SetEnable("barCargoBook", false);
                        toolBar.SetEnable("barBill", false);
                        toolBar.SetEnable("barDelivery", false);
                    }

                    #endregion
                }

            }
        }

        #endregion
    }
}