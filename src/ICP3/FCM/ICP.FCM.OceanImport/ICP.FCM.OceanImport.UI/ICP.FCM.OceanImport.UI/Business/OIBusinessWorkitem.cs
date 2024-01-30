using ICP.Business.Common.UI.Communication;
using ICP.Business.Common.UI.Document;
using ICP.Business.Common.UI.EventList;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.UI.DispatchCompare;
using ICP.FCM.Common.UI.UCBillList;
using ICP.FCM.Common.UI.UCDebtList;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace ICP.FCM.OceanImport.UI
{
    public class OIBusinessWorkitem : WorkItem
    {
      
     
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
                IWorkspace eventListWorkspace = (IWorkspace)Workspaces[OIBusinessWorkSpaceConstants.EventListWorkspace];
                eventListWorkspace.Show(eventListPart);

                UCCommunicationHistory faxMailEDIListPart = Items.AddNew<UCCommunicationHistory>();
                IWorkspace faxMailListWorkspace = (IWorkspace)Workspaces[OIBusinessWorkSpaceConstants.FaxMailEDIListWorkspace];
                faxMailListWorkspace.Show(faxMailEDIListPart);

                UCDocumentList documentPart = SmartParts.AddNew<UCDocumentList>();
                DocumentListPresenter documentPresenter = Items.AddNew<DocumentListPresenter>();
                documentPresenter.ucList = documentPart;
                documentPart.Presenter = documentPresenter;
                IWorkspace documentListPartWorkSapce = (IWorkspace)Workspaces[OIBusinessWorkSpaceConstants.DocumentListWorkspace];
                documentListPartWorkSapce.Show(documentPart);

                UCBillListPart billListPart = SmartParts.AddNew<UCBillListPart>();
                IWorkspace ucbillListPart = (IWorkspace)Workspaces[OIBusinessWorkSpaceConstants.BillListWorkSpace];
                ucbillListPart.Show(billListPart);

                UCDebtListPart debtListPart = SmartParts.AddNew<UCDebtListPart>();
                IWorkspace ucdebtListPart = (IWorkspace)Workspaces[OIBusinessWorkSpaceConstants.DebtWorkspace];
                ucdebtListPart.Show(debtListPart);


                HistoryOceanRecordPart historyOceanRecordPart = Items.AddNew<HistoryOceanRecordPart>();

                IWorkspace acceptPartWorkspace = (IWorkspace)Workspaces[OIBusinessWorkSpaceConstants.AcceptWorkspace];
                historyOceanRecordPart.Type = 2;
                acceptPartWorkspace.Show(historyOceanRecordPart);

                #endregion

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Ocean Import Business" : "海运进口业务";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                OIBusinessUIAdapter orderAdapter = new OIBusinessUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(fastSearchPart.GetType().Name, fastSearchPart);
                dic.Add(eventListPart.GetType().Name, eventListPart);
                dic.Add(faxMailEDIListPart.GetType().Name, faxMailEDIListPart);
                dic.Add(documentPart.GetType().Name, documentPart);
                dic.Add(billListPart.GetType().Name, billListPart);
                dic.Add(debtListPart.GetType().Name, debtListPart);
                dic.Add(historyOceanRecordPart.GetType().Name, historyOceanRecordPart);
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
    public class OIBusinessCommandConstants
    {
        /// <summary>
        /// 签收分发
        /// </summary>
        public const string Command_AcceptDispath = "Command_AcceptDispath";

        public const string Command_AddData = "Command_OIBAddData";
        public const string Command_CancelData = "Command_OIBCancelData";
        public const string Command_EditData = "Command_OIBEditData";
        public const string Command_ViewReason = "Command_OIBViewReason";
        public const string Command_ShowSearch = "Command_OIBShowSearch";
        public const string Command_CopyData = "Command_OIBCopyData";
        public const string Command_Print = "Command_OIBPrint";
        public const string Command_Document = "Command_OIBDocument";
        public const string Command_FaxEmail = "Command_OIBFaxEmail";
        public const string Command_Bill = "Command_OIBBill";
        public const string Command_Memo = "Command_OIBMemo";
        public const string Command_SendEmail = "Command_OIBSendEmail";
        public const string Command_RefreshData = "Command_OIBRefreshData";

        public const string Command_FastSecharData = "Command_OIBFastSecharData";
        /// 下载
        public const string Command_DownLoad = "Command_OIBDownLoad";
        /// 下载后保存
        public const string Command_InsertToListAfterDownLoad = "Command_InsertToListAfterDownLoad";
        ///转移
        public const string Command_BusinessTransfer = "Command_BusinessTransfer";
        ///提货通知书
        public const string Command_CargoBook = "Command_OIBCargoBook";
        /// 跟踪
        public const string Command_BoxTracking = "Command_OIBBoxTrack";
        /// 放货管理
        /// 放货
        public const string Command_Delivery = "Command_OIBDelivery";
        /// 同意放货
        public const string Command_AgreeRC = "Command_AgreeRC";
        /// <summary>
        /// 收到放单指令
        /// </summary>
        public const string Command_ReceiveRN = "Command_OIReceiveRN";
        /// <summary>
        /// 申请放单
        /// </summary>
        public const string Command_ApplyRelease = "Command_OIApplyRelease";

        /// <summary>
        /// 异常放货
        /// </summary>
        public const string Command_ExceptionReleaseRC = "Command_ExceptionReleaseRC";

        /// <summary>
        /// 催港前放单
        /// </summary>
        public const string Command_NoticeRelease = "Command_OINoticeRelease";
        /// <summary>
        /// 第三方代理海进业务放单
        /// </summary>
        public const string Command_ReleaseBL = "Command_OIReleaseBL";
        /// 确认订舱
        public const string Command_ConfirmBooking = "Command_OIBConfirmBooking";
        /// 确认装船
        public const string Command_ConfirmBookingShip = "Command_OIBConfirmBookingShip";


        public const string Command_ShowChildWorkspace = "Command_OIBShowChildWorkspace";

        public const string Command_PrintArrivalNotice = "Command_OIBPrintArrivalNotice";
        public const string Command_PrintReleaseOrder = "Command_OIBPrintReleaseOrder";
        public const string Command_PrintPickUp = "Command_OIBPrintPickUp";
        public const string Command_PrintProfit = "Command_OIBPrintProfit";
        public const string Command_PrintWorkSheet = "Command_OIBPrintWorkSheet";
        public const string Command_PrintForwardingBill = "Command_OIBPrintForwardingBill";
        public const string Command_PrintExportBusinessInfo = "Command_PrintExportBusinessInfo";
        
        /// <summary>
        /// 账单修订
        /// </summary>
        public const string Command_BillRevise = "Command_BillRevise";

        /// <summary>
        ///   发送提货通知书给客户(中文)
        /// </summary>
        public const string Command_MailPickUpToCHS = "Command_MailPickUpToCHS";

        /// <summary>
        /// 发送提货通知书给客户(英文)
        /// </summary>
        public const string Command_MailPickUpToENG = "Command_MailPickUpToENG";

        /// <summary>
        /// 发送到港通知书给客户(中文版)
        /// </summary>
        public const string Command_MailANToCustomerCHS = "Command_MailANToCustomerCHS";

        /// <summary>
        /// 发送到港通知书给客户(英文版)
        /// </summary>
        public const string Command_MailANToCustomerENG = "Command_MailANToCustomerENG";
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
        public const string FaxMailEDIListWorkspace = "FaxMailEDIListWorkspace";
        public const string DocumentListWorkspace = "DocumentListWorkspace";
        public const string OperationPartWorkspace = "OperationPartWorkspace";
        public const string BillListWorkSpace = "BillListWorkSpace";
        public const string DebtWorkspace = "DebtWorkspace";
        public const string AcceptWorkspace = "AcceptWorkspace";
    }

    public class OIBusinessStateConstants
    {
        public const string CurrentRow = "CurrentRow";
    }

    public class OIBusinessUIAdapter:IDisposable
    {

        #region parts

        OIBusinessTool _toolBar;
        ISearchPart _searchPart;
        OIBusinessList _mainListPart;
        ISearchPart _fastSearchPart;
        IListPart _eventlistPart;
        UCCommunicationHistory _faxMailEDIListPart;
        UCDocumentList _DocumentListPart;
        UCBillListPart _ucBillList;
        UCDebtListPart _ucdebtList;
        HistoryOceanRecordPart _HistoryOceanRecordPart;

        #endregion

        #region interface

        public void Init(Dictionary<string, object> controls)
        {

            _toolBar = (OIBusinessTool)controls[typeof(OIBusinessTool).Name];
            _searchPart = (ISearchPart)controls[typeof(OIBusinessSearch).Name];
            _mainListPart = (OIBusinessList)controls[typeof(OIBusinessList).Name];
            _fastSearchPart = (ISearchPart)controls[typeof(OIBusinessFastSearch).Name];
            _eventlistPart = (IListPart)controls[typeof(EventListPart).Name];
            _faxMailEDIListPart = (UCCommunicationHistory)controls[typeof(UCCommunicationHistory).Name];
            _DocumentListPart = (UCDocumentList)controls[typeof(UCDocumentList).Name];
            _ucBillList = (UCBillListPart)controls[typeof(UCBillListPart).Name];
            _ucdebtList = (UCDebtListPart)controls[typeof(UCDebtListPart).Name];
            _HistoryOceanRecordPart = (HistoryOceanRecordPart)controls[typeof(HistoryOceanRecordPart).Name];

            RefreshBarEnabled(_toolBar, null);
            //OceanBusinessList firstRow = _mainListPart.Current as OceanBusinessList;
            //if (firstRow != null)
            //{
            //    RefreshBarEnabled(_toolBar, firstRow);
            //}


            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                OceanBusinessList listData = data as OceanBusinessList;
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add("ParentList", data);
                if (listData != null)
                {
                    BusinessOperationContext context = new BusinessOperationContext();
                    context.OperationID = listData.ID;
                    context.FormId = listData.ID;
                    context.FormType = FormType.Booking;
                    context.OperationType = OperationType.OceanImport;

                    _eventlistPart.DataSource = context;
                    //设置Fax/Email/EDI数据源
                     _faxMailEDIListPart.BindData(context);
                    //设置文档中心数据源
                    FCM.Common.UI.FCMUIUtility.SetDocumentListDataSource(_DocumentListPart,context);
                    
                    #region 账单列表和欠款记录
                    _ucBillList.DataSource = context;
                    _ucdebtList.DataSource = context;
                    #endregion 

                    
                    _HistoryOceanRecordPart.OperationID = listData.ID;
                    _HistoryOceanRecordPart.Type = 2;
                    _HistoryOceanRecordPart.BindingData();
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
        private void RefreshBarEnabled(OIBusinessTool toolBar, OceanBusinessList listData)
        {
            if (listData == null || listData.IsNew)
            {

                toolBar.SetEnable("barCancel", false);
                toolBar.SetEnable("barCopy", false);
                toolBar.SetEnable("barEdit", false);
                toolBar.SetEnable("barPrint", false);
                toolBar.SetEnable("barReleaseCargo", false);
                toolBar.SetEnable("barBusinessTransfer", false);
                toolBar.SetEnable("barCargoBook", false);
                toolBar.SetEnable("barReleaseCargo", false);
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
                    toolBar.SetEnable("barReleaseCargo", false);
                    toolBar.SetEnable("barBusinessTransfer", false);
                    toolBar.SetEnable("barCargoBook", false);
                    toolBar.SetEnable("barReleaseCargo", false);
                    toolBar.SetEnable("barConfirmBooking", false);
                    toolBar.SetEnable("barConfirmBookingShip", false);
                    toolBar.SetEnable("barBill", false);

                    toolBar.SetText("barCancel", LocalData.IsEnglish ? "Available(&D)" : "激活(&D)");
                }
                else
                {
                    toolBar.SetEnable("barEdit", true);
                    toolBar.SetEnable("barPrint", true);
                    toolBar.SetEnable("barReleaseCargo", true);
                    toolBar.SetEnable("barBusinessTransfer", true);
                    toolBar.SetEnable("barBoxTracking", true);
                    toolBar.SetEnable("barCargoBook", true);
                    toolBar.SetEnable("barReleaseCargo", true);
                    toolBar.SetEnable("barBill", true);

                    toolBar.SetText("barCancel", LocalData.IsEnglish ? "Cancel(&D)" : "取消(&D)");

                    if (listData.State != OIOrderState.NewOrder)
                    {
                        toolBar.SetEnable("barCancel", false);
                    }
                    else
                    {
                        toolBar.SetEnable("barCancel", true);
                    }

                    //内部代理海进不能执行放单
                    if (_mainListPart.IsInternalAgent)
                    {
                        toolBar.SetEnable("barReleaseBL", true);
                        toolBar.SetEnable("barNoticeRelease", false);
                    }
                    else
                    {
                        toolBar.SetEnable("barReleaseBL", false);
                    }
                    if (ArgumentHelper.GuidIsNullOrEmpty(listData.AgentID))
                    {
                        toolBar.SetEnable("barReleaseBL", false);
                    }

                    //未放单不能放货和签收放单
                    if (listData.IsRelease)
                    {
                        toolBar.SetText("barReleaseBL", LocalData.IsEnglish ? "Cancel ReleaseBL" : "取消放单");
                        toolBar.SetEnable("barReceiveRN", true);
                        toolBar.SetEnable("barDelivery", true);
                        toolBar.SetEnable("barNoticeRelease", false);
                        toolBar.SetEnable("barApplyReleaseCargo", true);
                    }
                    else
                    {
                        toolBar.SetText("barReleaseBL", LocalData.IsEnglish ? "ReleaseBL" : "放单");
                        toolBar.SetEnable("barReceiveRN", false);
                        //toolBar.SetEnable("barDelivery", false);
                        if (listData.IsNoticeRelease)
                        {
                            toolBar.SetEnable("barNoticeRelease", false);
                        }
                        else
                        {
                            toolBar.SetEnable("barNoticeRelease", true);
                        }
                        toolBar.SetEnable("barApplyReleaseCargo", false);
                    }
                    if (listData.IsReceiveNotice)
                    {
                        //toolBar.SetText("barReceiveRN", LocalData.IsEnglish ? "Cancel ReceiveRBL" : "取消签收放单");
                        toolBar.SetEnable("barReceiveRN", false); 
                    }
                    else
                    {
                        toolBar.SetText("barReceiveRN", LocalData.IsEnglish ? "ReceiveRBL" : "签收放单");
                    }
                    if (listData.IsApplyRC)
                    {
                        toolBar.SetText("barApplyReleaseCargo", LocalData.IsEnglish ? "Cancel ApplyRC" : "取消申请放货");
                    }
                    else
                    {
                        toolBar.SetText("barApplyReleaseCargo", LocalData.IsEnglish ? "ApplyRC" : "申请放货");
                    }

                    if (listData.IsReleaseCargo)
                    {
                        toolBar.SetEnable("barReceiveRN", false);
                        toolBar.SetEnable("barNoticeRelease", false);
                        toolBar.SetEnable("barApplyReleaseCargo", false);
                        toolBar.SetEnable("barReleaseBL", false);
                    }

                    if (listData.IsAgreeRC)
                    {
                        //toolBar.SetEnable("barDelivery", true); 暂时不做限制
                        toolBar.SetText("barAgreeRC", LocalData.IsEnglish ? "Cancel Agree RC" : "取消同意放货");
                    }
                    else
                    {
                       // toolBar.SetEnable("barDelivery", false);
                        toolBar.SetText("barAgreeRC", LocalData.IsEnglish ? "Agree RC" : "同意放货");
                    }


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

                    #region 已放货的
                    if (listData.State == OIOrderState.Release)
                    {
                        toolBar.SetEnable("barConfirmBookingShip", false);
                        toolBar.SetEnable("barConfirmBooking", false);
                        toolBar.SetEnable("barDelivery", true);

                        toolBar.SetCancelDelivery(true);
                    }
                    else
                    {
                        toolBar.SetCancelDelivery(false);
                    }

                    #endregion

                    #region 已打回的
                    if (listData.State == OIOrderState.Rejected)
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

        #region IDisposable 成员

        public void Dispose()
        {
            _DocumentListPart = null;
            _eventlistPart = null;
            _fastSearchPart = null;
            _faxMailEDIListPart = null;
            _mainListPart.KeyDown -= _mainListPart_KeyDown;
            _mainListPart = null;
            _searchPart = null;
            _toolBar = null;
            _ucBillList = null;
            _ucdebtList = null;
        }

        #endregion
    }
}
