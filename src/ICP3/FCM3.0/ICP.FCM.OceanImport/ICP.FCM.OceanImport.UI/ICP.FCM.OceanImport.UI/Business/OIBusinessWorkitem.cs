using System.Collections.Generic;

using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.UI.FaxEMailLog;
using ICP.MailCenter.CommonUI;
using Microsoft.Practices.ObjectBuilder;


namespace ICP.FCM.OceanImport.UI
{
    public class OIBusinessWorkitem : WorkItem
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
        /// <summary>
        /// 显示界面
        /// </summary>
        private void Show()
        {
            OIBusinessMainWorkSpace mainSpce = this.SmartParts.Get<OIBusinessMainWorkSpace>("OIBusinessrMainWorkspace");
            if (mainSpce == null)
            {
                mainSpce = this.SmartParts.AddNew<OIBusinessMainWorkSpace>("OIBusinessrMainWorkspace");

                #region AddPart

                OIBusinessTool toolBar = this.SmartParts.AddNew<OIBusinessTool>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[OIBusinessWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                OIBusinessList listPart = this.SmartParts.AddNew<OIBusinessList>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[OIBusinessWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                OIBusinessSearch searchPart = this.SmartParts.AddNew<OIBusinessSearch>();
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[OIBusinessWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);


                OIBusinessFastSearch fastSearchPart = this.SmartParts.AddNew<OIBusinessFastSearch>();
                IWorkspace fastSearchWorkspace = (IWorkspace)this.Workspaces[OIBusinessWorkSpaceConstants.FastSearchWorkspace];
                fastSearchWorkspace.Show(fastSearchPart);

                ICP.FCM.Common.UI.Memolist.MemoListPart memoListPart = this.Items.AddNew<ICP.FCM.Common.UI.Memolist.MemoListPart>();
                IWorkspace memoListWorkspace = (IWorkspace)this.Workspaces[OIBusinessWorkSpaceConstants.MemoListWorkspace];
                memoListWorkspace.Show(memoListPart);

                UCCommunicationHistory faxMailEDIListPart = this.Items.AddNew<UCCommunicationHistory>();
                IWorkspace faxMailListWorkspace = (IWorkspace)this.Workspaces[OIBusinessWorkSpaceConstants.FaxMailEDIListWorkspace];
                faxMailListWorkspace.Show(faxMailEDIListPart);


                UCDocumentList documentPart = this.SmartParts.AddNew<UCDocumentList>();
                IWorkspace documentListPartWorkSapce = (IWorkspace)this.Workspaces[OIBusinessWorkSpaceConstants.DocumentListWorkspace];
                documentListPartWorkSapce.Show(documentPart);

                #endregion

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Ocean Import Business" : "海运进口业务";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                OIBusinessUIAdapter orderAdapter = new OIBusinessUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(fastSearchPart.GetType().Name, fastSearchPart);
                dic.Add(memoListPart.GetType().Name, memoListPart);
                dic.Add(faxMailEDIListPart.GetType().Name, faxMailEDIListPart);
                dic.Add(documentPart.GetType().Name, documentPart);
                orderAdapter.Init(dic, documentPresenter);
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }
    }
    /// <summary>
    /// 命令常量
    /// </summary>
    public class OIBusinessCommandConstants
    {
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
        /// 放货
        public const string Command_Delivery = "Command_OIBDelivery";
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
        public const string MemoListWorkspace = "MemoListWorkspace";
        public const string FaxMailEDIListWorkspace = "FaxMailEDIListWorkspace";
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
        IListPart _memolistPart;
        UCCommunicationHistory _faxMailEDIListPart;
        UCDocumentList _DocumentListPart;


        #endregion

        #region interface

        public void Init(Dictionary<string, object> controls, DocumentListPresenter documentPresenter)
        {

            _toolBar = (OIBusinessTool)controls[typeof(OIBusinessTool).Name];
            _searchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(OIBusinessSearch).Name];
            _mainListPart = (OIBusinessList)controls[typeof(OIBusinessList).Name];
            _fastSearchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(OIBusinessFastSearch).Name];
            _memolistPart = (IListPart)controls[typeof(MemoListPart).Name];
            _faxMailEDIListPart = (UCCommunicationHistory)controls[typeof(UCCommunicationHistory).Name];
            _DocumentListPart = (UCDocumentList)controls[typeof(UCDocumentList).Name];

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
                    MemoParam para = new MemoParam();
                    para.OperationId = listData.ID;
                    para.FormID = listData.ID;
                    para.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
                    para.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.OceanImport;
                    _memolistPart.DataSource = para;
                    //设置Fax/Email/EDI数据源
                     ICP.FCM.Common.UI.Utility.SetCommunicationDataSource(_faxMailEDIListPart, para);
                    //设置文档中心数据源
                    ICP.FCM.Common.UI.Utility.SetDocumentListDataSource(_DocumentListPart, documentPresenter, para);

                }
                #region toolBar

                RefreshBarEnabled(_toolBar, listData);

                #endregion
            };
            #endregion

            _mainListPart.KeyDown += new System.Windows.Forms.KeyEventHandler(_mainListPart_KeyDown);

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
        private void RefreshBarEnabled(OIBusinessTool toolBar, OceanBusinessList listData)
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

                    if (listData.State != OIOrderState.NewOrder)
                    {
                        toolBar.SetEnable("barCancel", false);
                    }
                    else
                    {
                        toolBar.SetEnable("barCancel", true);
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
    }
}
