using System.Collections.Generic;

using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.ObjectBuilder;
using ICP.MailCenter.CommonUI;
using ICP.FCM.Common.UI.Document;

namespace ICP.FCM.OceanImport.UI
{
    public class OIOrderWorkitem : WorkItem
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
            OIOrderMainWorkSpace mainSpce = this.SmartParts.Get<OIOrderMainWorkSpace>("OIOrderMainWorkSpace");
            if (mainSpce == null)
            {
                mainSpce = this.SmartParts.AddNew<OIOrderMainWorkSpace>("OIOrderMainWorkSpace");

                #region AddPart

                OIOrderToolBar toolBar = this.SmartParts.AddNew<OIOrderToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[OIOrderWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                OIOrderListPart listPart = this.SmartParts.AddNew<OIOrderListPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[OIOrderWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                OrderSearchPart searchPart = this.SmartParts.AddNew<OrderSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[OIOrderWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);


                OrderFastSearchPart fastSearchPart = this.SmartParts.AddNew<OrderFastSearchPart>();
                IWorkspace fastSearchPartWorkspace = (IWorkspace)this.Workspaces[OIOrderWorkSpaceConstants.FastSearchWorkspace];
                fastSearchPartWorkspace.Show(fastSearchPart);

                ICP.FCM.Common.UI.Memolist.MemoListPart memoListPart = this.SmartParts.AddNew<ICP.FCM.Common.UI.Memolist.MemoListPart>();
                IWorkspace memoListWorkspace = (IWorkspace)this.Workspaces[OIOrderWorkSpaceConstants.MemoListWorkspace];
                memoListWorkspace.Show(memoListPart);

                UCCommunicationHistory communicationHistory = this.SmartParts.AddNew<UCCommunicationHistory>();
                IWorkspace communicationHistoryWorkspace = (IWorkspace)this.Workspaces[OIOrderWorkSpaceConstants.FaxMailEDIListWorkspace];
                communicationHistoryWorkspace.Show(communicationHistory);


                UCDocumentList documentPart = this.SmartParts.AddNew<UCDocumentList>();
                IWorkspace documentListWorkspace = (IWorkspace)this.Workspaces[OIOrderWorkSpaceConstants.DocumentListWorkspace];
                documentListWorkspace.Show(documentPart);


                #endregion

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Ocean Import Order" : "海运进口订单";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                OIOrderUIAdapter orderAdapter = new OIOrderUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(fastSearchPart.GetType().Name, fastSearchPart);
                dic.Add(memoListPart.GetType().Name, memoListPart);
                dic.Add(communicationHistory.GetType().Name, communicationHistory);
                dic.Add(documentPart.GetType().Name, documentPart);
                orderAdapter.Init(dic, documentPresenter);
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(mainSpce);
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
        public const string FaxMailEDIListWorkspace = "FaxMailEDIListWorkspace";
        public const string MemoListWorkspace = "MemoListWorkspace";
    }

    public class OIOrderStateConstants
    {
        public const string CurrentRow = "CurrentRow";
    }

    public class OIOrderUIAdapter
    {

        #region parts

        IToolBar _toolBar;
        ISearchPart _searchPart;
        OIOrderListPart _mainListPart;
        ISearchPart _fastSearchPart;
        IListPart _memolistPart;
        UCDocumentList _DocumentListPart;
        UCCommunicationHistory _ucCommunicationHistory;


        #endregion

        #region interface

        public void Init(Dictionary<string, object> controls, DocumentListPresenter documentPresenter)
        {

            _toolBar = (IToolBar)controls[typeof(OIOrderToolBar).Name];
            _searchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(OrderSearchPart).Name];
            _mainListPart = (OIOrderListPart)controls[typeof(OIOrderListPart).Name];
            _fastSearchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(OrderFastSearchPart).Name];
            _memolistPart = (IListPart)controls[typeof(MemoListPart).Name];
            _DocumentListPart = (UCDocumentList)controls[typeof(UCDocumentList).Name];
            _ucCommunicationHistory = (UCCommunicationHistory)controls[typeof(UCCommunicationHistory).Name];

            RefreshBarEnabled(null, _toolBar);
            //OceanOrderList firstRow = _mainListPart.Current as OceanOrderList;
            //if (firstRow != null)
            //{
            //    RefreshBarEnabled(firstRow, _toolBar);
            //}

            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                OceanOrderList listData = data as OceanOrderList;
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add("ParentList", data);
                if (listData != null)
                {
                    MemoParam para = new MemoParam();
                    para.OperationId = listData.ID;
                    para.FormID = listData.ID;
                    para.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
                    para.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.OceanImport;
                    _memolistPart.DataSource = para;
                    //设置文档中心数据源
                    ICP.FCM.Common.UI.Utility.SetDocumentListDataSource(_DocumentListPart, documentPresenter, para);
                    ICP.FCM.Common.UI.Utility.SetCommunicationDataSource(_ucCommunicationHistory, para);

                }

                #region toolBar

                RefreshBarEnabled(listData, _toolBar);

                #endregion
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

            #region _fastSearchPart.OnSearched
            _fastSearchPart.OnSearched += delegate(object sender, object results)
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
    }
}
