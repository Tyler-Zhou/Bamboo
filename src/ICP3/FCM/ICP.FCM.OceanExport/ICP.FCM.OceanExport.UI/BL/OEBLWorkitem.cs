using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.SmartParts;
using System.Drawing;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using System;
using ICP.Framework.CommonLibrary.Common;
using ICP.Business.Common.UI.EventList;
using ICP.FCM.OceanImport.UI;
using ICP.FCM.Common.UI.DispatchCompare;
using DevExpress.XtraTab;
using ICP.Business.Common.UI.Communication;
using ICP.Business.Common.UI.Document;
using ICP.Business.Common.UI;
using ICP.FileSystem.ServiceInterface;

namespace ICP.FCM.OceanExport.UI.BL
{
    public class OEBLWorkitem : WorkItem
    {

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _values = null;

            }
            base.Dispose(disposing);
        }
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            OEBLMainWorkspace mainSpce = SmartParts.Get<OEBLMainWorkspace>("OEBLMainWorkspace");
            if (mainSpce == null)
            {
                mainSpce = SmartParts.AddNew<OEBLMainWorkspace>("OEBLMainWorkspace");

                #region AddPart

                OEBLToolBar toolBar = SmartParts.AddNew<OEBLToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)Workspaces[OEBLWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                OEBLListPart listPart = SmartParts.AddNew<OEBLListPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[OEBLWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                BLSearchPart searchPart = SmartParts.AddNew<BLSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)Workspaces[OEBLWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);

                BLFastSearchPart fastSearchPart = SmartParts.AddNew<BLFastSearchPart>();
                IWorkspace fastSearchPartWorkspace = (IWorkspace)Workspaces[OEBLWorkSpaceConstants.FastSearchWorkspace];
                fastSearchPartWorkspace.Show(fastSearchPart);

                EventListPart eventListPart = Items.AddNew<EventListPart>();
                IWorkspace eventListWorkspace = (IWorkspace)Workspaces[OEBLWorkSpaceConstants.EventListWorkspace];
                eventListWorkspace.Show(eventListPart);

                UCCommunicationHistory faxMailEDIListPart = Items.AddNew<UCCommunicationHistory>();
                //FaxEMailLogListPart faxMailListPart = this.Items.AddNew<FaxEMailLogListPart>();
                IWorkspace faxMailListWorkspace = (IWorkspace)Workspaces[OEBLWorkSpaceConstants.FaxMailEDIListWorkspace];
                faxMailListWorkspace.Show(faxMailEDIListPart);

                UCDocumentList documentListPart = Items.AddNew<UCDocumentList>();
                DocumentListPresenter documentPresenter = Items.AddNew<DocumentListPresenter>();
                documentPresenter.ucList = documentListPart;
                documentListPart.Presenter = documentPresenter;
                IWorkspace documentListWorkspace = (IWorkspace)Workspaces[OEBLWorkSpaceConstants.DocumentListWorkspace];
                documentListWorkspace.Show(documentListPart);

                UCOECargoTracking cargoTracking = Items.AddNew<UCOECargoTracking>();
                IWorkspace cargoTrackingWorkspace = (IWorkspace)Workspaces[OEBLWorkSpaceConstants.CargoTracking];
                cargoTrackingWorkspace.Show(cargoTracking);

                HistoryOceanRecordPart historyOceanRecordPart = Items.AddNew<HistoryOceanRecordPart>();

                IWorkspace acceptPartWorkspace = (IWorkspace)Workspaces[OIBusinessWorkSpaceConstants.AcceptWorkspace];
                historyOceanRecordPart.Type = 1;
                acceptPartWorkspace.Show(historyOceanRecordPart);

                #endregion

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Ocean Export BL List" : "海出提单";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                OEBLUIAdapter blAdapter = new OEBLUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(fastSearchPart.GetType().Name, fastSearchPart);
                dic.Add(eventListPart.GetType().Name, eventListPart);
                dic.Add(cargoTracking.GetType().Name, cargoTracking);
                dic.Add(faxMailEDIListPart.GetType().Name, faxMailEDIListPart);
                dic.Add(documentListPart.GetType().Name, documentListPart);
                dic.Add(historyOceanRecordPart.GetType().Name, historyOceanRecordPart);
                dic.Add(mainSpce.GetType().Name, mainSpce);

                blAdapter.Init(dic);

                if (_values != null)
                {
                    fastSearchPart.Init(_values);
                    fastSearchPart.RaiseSearched();
                    _values = null;
                }
            }
            else
            {
                Workspaces[ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }


        IDictionary<string, object> _values;
        /// <summary>
        /// 简易搜索器
        /// </summary>
        /// <param name="values"></param>
        public void Init(IDictionary<string, object> values)
        {
            _values = values;
        }
    }


    public class OEBLColorConstant
    {
        /// <summary>
        /// 对单中
        /// </summary>
        public static Color CheckingColor = Color.FromArgb(67, 156, 50); //Color.Lime;
        /// <summary>
        /// 对单完成
        /// </summary>
        public static Color CheckedColor = Color.FromArgb(237, 75, 11); //Color.Orange;

        /// <summary>
        /// 放单
        /// </summary>
        public static Color ReleaseColor = Color.FromArgb(233, 160, 0); //Color.Blue;
    }

    /// <summary>
    /// 命令常量
    /// </summary>
    public class OEBLCommandConstants
    {
        /// <summary>
        /// 新增MBL
        /// </summary>
        public const string Command_AddMBL = "Command_AddMBL";
        /// <summary>
        /// 新增HBL
        /// </summary>
        public const string Command_AddHBL = "Command_AddHBL";
        /// <summary>
        /// 复制提单
        /// </summary>
        public const string Command_CopyData = "Command_OELCopyData";
        /// <summary>
        /// 编辑提单
        /// </summary>
        public const string Command_EditData = "Command_OELEditData";
        /// <summary>
        /// 删除提单
        /// </summary>
        public const string Command_DeleteData = "Command_OELDeleteData";
        /// <summary>
        /// 开始对单
        /// </summary>
        public const string Command_Check = "Command_OELCheck";
        /// <summary>
        /// 完成对单
        /// </summary>
        public const string Command_CompleteCheck = "Command_OELCompleteCheck";
        /// <summary>
        /// 打印提单
        /// </summary>
        public const string Command_PrintBL = "Command_OELPrintBL";
        /// <summary>
        /// 打印利润表
        /// </summary>
        public const string Command_PrintProfit = "Command_OEPrintProfit";
        /// <summary>
        /// 打印装箱单
        /// </summary>
        public const string Command_PrintLoadCtn = "Command_OELPrintLoadCtn";
        /// <summary>
        /// 打印装货单
        /// </summary>
        public const string Command_PrintLoadGoods = "Command_OELPrintLoadGoods";
        /// <summary>
        /// 确认装船
        /// </summary>
        public const string Command_LoadShip = "Command_OELLoadShip";
        /// <summary>
        /// 申请代理
        /// </summary>
        public const string Command_ReplyAgent = "Command_OELReplyAgent";
        /// <summary>
        /// E-MBL
        /// </summary>
        public const string Command_E_MBL = "Command_E_OELMBL";
        /// <summary>
        /// NBEDIANL
        /// </summary>
        public const string Command_NBEDIANL = "Command_NBEDIANL";
        /// <summary>
        /// Pre
        /// </summary>
        public const string Command_Pre = "Command_Pre";
        /// <summary>
        /// 宁波edi中心
        /// </summary>
        public const string Command_NBEDI = "Command_NBEDI";
        /// <summary>
        /// 复制AMS信息到新提单
        /// </summary>
        public const string Command_CopyAMS = "Command_CopyAMS";
        /// <summary>
        /// ISF
        /// </summary>
        public const string Command_ISF = "Command_OELISF";
        /// <summary>
        /// 帐单
        /// </summary>
        public const string Command_Bill = "Command_OELBill";

        /// <summary>
        /// 显示搜索面板
        /// </summary>
        public const string Command_ShowSearch = "Command_OELShowSearch";

        /// <summary>
        /// 刷新
        /// </summary>
        public const string Command_RefreshData = "Command_OELRefreshData";
        /// <summary>
        /// 分单
        /// </summary>
        public const string Command_SplitBL = "Command_OELSplitBL";
        /// <summary>
        /// 合单
        /// </summary>
        public const string Command_Merge = "Command_OELMerge";
        /// <summary>
        /// 确认放单
        /// </summary>
        public const string Command_ConfirmReleaseBL = "Command_OELConfirmReleaseBL";
        /// <summary>
        /// 查看模式-MBL
        /// </summary>
        public const string Command_VisibleMBL = "Command_OELVisibleMBL";
        /// <summary>
        /// 查看模式-HBL
        /// </summary>
        public const string Command_VisibleHBL = "Command_OELVisibleHBL";
        /// <summary>
        /// 查看模式-全部
        /// </summary>
        public const string Command_VisibleALL = "Command_OELVisibleALL";

        /// <summary>
        /// 核销单
        /// </summary>
        public const string Command_VerifiSheet = "Command_OELVerifiSheet";

        /// <summary>
        /// 分发文档事件
        /// </summary>
        public const string Command_DocumentDispatch = "Command_OELDocumentDispatch";

        /// <summary>
        /// 分发文档日志
        /// </summary>
        public const string Command_DispatchLog = "Command_DispatchLog";

        /// <summary>
        /// 分发文档
        /// </summary>
        public const string Command_Dispatch = "Command_Dispatch";

        /// <summary>
        /// 签收分发
        /// </summary>
        public const string Command_AcceptDispatch = "Command_AcceptDispatch";

        /// <summary>
        ///  装箱
        /// </summary>
        public const string Command_LoadContainer = "Command_LoadContainer_New";

        /// <summary>
        ///修订签收
        /// </summary>
        public const string Command_ReviseAccept = "Command_ReviseAccept";

        /// <summary>
        /// 客户确认补料(中文)
        /// </summary>
        public const string Command_MailBlCopyToCustomerCHS = "Command_MailBlCopyToCustomerCHS";

        /// <summary>
        /// 客户确认补料(英文)
        /// </summary>
        public const string Command_MailBlCopyToCustomerENG = "Command_MailBlCopyToCustomerENG";

    }

    /// <summary>
    /// WorkSpace常量
    /// </summary>
    public class OEBLWorkSpaceConstants
    {
        /// <summary>
        /// 工具栏
        /// </summary>
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        /// <summary>
        /// 搜索
        /// </summary>
        public const string SearchWorkspace = "SearchWorkspace";
        /// <summary>
        /// 列表
        /// </summary>
        public const string ListWorkspace = "ListWorkspace";
        /// <summary>
        /// 快捷搜索
        /// </summary>
        public const string FastSearchWorkspace = "FastSearchWorkspace";

        public const string EventListWorkspace = "EventListWorkspace";
        public const string BLListWorkSpace = "BLListWorkSpace";
        public const string FaxMailEDIListWorkspace = "FaxMailEDIListWorkspace";
        public const string DocumentListWorkspace = "DocumentListWorkspace";

        public const string CargoTracking = "CargoTrackingWorkspace";
    }

    public class OEBLUIAdapter : IDisposable
    {
        #region parts

        IToolBar _toolBar;
        ISearchPart _searchPart;
        ISearchPart _fastSearchPart;
        OEBLListPart _mainListPart;
        IListPart _eventlistPart;
        UCCommunicationHistory _faxMailEDIListPart;
        UCOECargoTracking _cargoTracking;
        UCDocumentList _DocumentListPart;
        HistoryOceanRecordPart _HistoryOceanRecordPart;
        OEBLMainWorkspace _mainSpace;
        #endregion

        #region interface and RefreshBarEnabled

        public void Init(Dictionary<string, object> controls)
        {

            _toolBar = (IToolBar)controls[typeof(OEBLToolBar).Name];
            _searchPart = (ISearchPart)controls[typeof(BLSearchPart).Name];
            _mainListPart = (OEBLListPart)controls[typeof(OEBLListPart).Name];
            _fastSearchPart = (ISearchPart)controls[typeof(BLFastSearchPart).Name];
            _eventlistPart = (IListPart)controls[typeof(EventListPart).Name];
            _faxMailEDIListPart = (UCCommunicationHistory)controls[typeof(UCCommunicationHistory).Name];
            _cargoTracking = (UCOECargoTracking)controls[typeof(UCOECargoTracking).Name];
            _DocumentListPart = (UCDocumentList)controls[typeof(UCDocumentList).Name];
            _HistoryOceanRecordPart = (HistoryOceanRecordPart)controls[typeof(HistoryOceanRecordPart).Name];
            _mainSpace = (OEBLMainWorkspace)controls[typeof(OEBLMainWorkspace).Name];

            RefreshBarEnabled(_toolBar, null, _mainListPart);

            #region Connection

            _mainSpace.TabSelectedPageChanged += delegate(object sender, object data)
            {
                if (_mainListPart.Current != null)
                {
                    OceanBLList listData = _mainListPart.Current as OceanBLList;

                    BusinessOperationContext context = new BusinessOperationContext();
                    context.OperationID = listData.OceanBookingID;
                    context.FormId = listData.ID;
                    if (listData.BLType == FCMBLType.MBL)
                    {
                        context.FormType = FormType.MBL;
                    }
                    else
                    {
                        context.FormType = FormType.HBL;
                    }
                    context.OperationType = OperationType.OceanExport;


                    TabPageChangedEventArgs e = data as TabPageChangedEventArgs;

                    if (e.Page.Name == "tabCargoTracking")
                    {
                        if (_mainListPart.CurrentRow != null)
                        {
                            context.Clear();
                            context.OperationID = _mainListPart.CurrentRow.OceanBookingID;
                            context.Add("CompanyID", _mainListPart.CurrentRow.CompanyID);
                            _cargoTracking.DataBind(context);
                        }
                    }

                    if (e.Page.Name == "tabDocumentList")
                    {                    
                            FCM.Common.UI.FCMUIUtility.SetDocumentListDataSource(_DocumentListPart, context);
                    }

                    if (e.Page.Name=="FaxMailEDIListWorkspace")
                    {
                        _faxMailEDIListPart.BindData(context);
                    }

                    if (e.Page.Name=="EventListWorkspace")
                    {
                        _eventlistPart.DataSource = context;
                    }

                    if (e.Page.Name=="tabAcceptList")
                    {
                        _HistoryOceanRecordPart.OperationID = listData.OceanBookingID;
                        _HistoryOceanRecordPart.Type = 1;
                        _HistoryOceanRecordPart.BindingData();   
                    }


                }
            };

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                OceanBLList listData = data as OceanBLList;
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add("ParentList", data);

                if (listData != null)
                {
                    BusinessOperationContext context = new BusinessOperationContext();
                    context.OperationID = listData.OceanBookingID;
                    context.FormId = listData.ID;
                    if (listData.BLType == FCMBLType.MBL)
                    {
                        context.FormType = FormType.MBL;
                    }
                    else
                    {
                        context.FormType = FormType.HBL;
                    }
                    context.OperationType = OperationType.OceanExport;

                    if (_mainSpace.IsSelectedEventList)
                    {
                        _eventlistPart.DataSource = context;
                    }

                    if (_mainSpace.IsSelectedFaxMailList)
                    {
                        //设置Fax/Email/EDI数据源
                        _faxMailEDIListPart.BindData(context);
                    }

                    if (_mainSpace.IsSelectedAcceptList)
                    {
                        _HistoryOceanRecordPart.OperationID = listData.OceanBookingID;
                        _HistoryOceanRecordPart.Type = 1;
                        _HistoryOceanRecordPart.BindingData();
                    }

                    if (_mainSpace.IsSelectedDocumentList)
                    {
                        //设置文档列表数据源
                        FCM.Common.UI.FCMUIUtility.SetDocumentListDataSource(_DocumentListPart, context);
                    }

                    //CargoTracking货物跟踪数据
                    if (_mainSpace.IsSelectedCargoTracking == true)
                        _cargoTracking.DataBind(context);

                }

                #region toolBar

                RefreshBarEnabled(_toolBar, listData, _mainListPart);

                #endregion
            };

            _mainListPart.KeyDown += delegate(object sender, KeyEventArgs e)
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
            };

            #endregion

            #region _searchPart.OnSearched
            _searchPart.OnSearched += delegate(object sender, object results)
            {
                _mainListPart.DataSource = results;
            };
            #endregion


            #region _searchPart.OnSearched
            _fastSearchPart.OnSearched += delegate(object sender, object results)
            {
                _mainListPart.DataSource = results;
            };
            #endregion

            #endregion

            // _fastSearchPart.RaiseSearched();Sophial
        }


        private void RefreshBarEnabled(IToolBar toolBar, OceanBLList listData, OEBLListPart _mainPart)
        {
            if (listData == null || listData.IsNew)
            {
                toolBar.SetEnable("barCopy", false);
                toolBar.SetEnable("barEdit", false);
                toolBar.SetEnable("barDelete", false);
                toolBar.SetEnable("barReleaseBL", false);

                toolBar.SetEnable("barCheck", false);
                toolBar.SetEnable("barCompleteCheck", false);

                toolBar.SetEnable("barPrint", false);
                toolBar.SetEnable("barPrintBL", false);
                toolBar.SetEnable("barPrintLoadCtn", false);
                toolBar.SetEnable("barPrintLoadGoods", false);
                toolBar.SetEnable("barLoadShip", false);
                toolBar.SetEnable("barReplyAgent", false);
                toolBar.SetEnable("barE_MBL", false);
                toolBar.SetEnable("barNBEDI", false);
                toolBar.SetEnable("barISF", false);
                toolBar.SetEnable("barBill", false);
                toolBar.SetEnable("barVerifiSheet", false);
                toolBar.SetEnable("barDocument", false);
                toolBar.SetEnable("barFaxEmail", false);
                toolBar.SetEnable("barMemo", false);

                toolBar.SetEnable("barSplitAndMerge", false);
                toolBar.SetEnable("barSplitBL", false);
                toolBar.SetEnable("barMergeBL", false);
            }
            else
            {

                toolBar.SetEnable("barEdit", true);

                toolBar.SetEnable("barDocument", true);
                toolBar.SetEnable("barFaxEmail", true);
                toolBar.SetEnable("barMemo", true);
                toolBar.SetEnable("barSplitAndMerge", true);

                if (listData.IsValid == false)
                {
                    toolBar.SetEnable("barCopy", false);
                    toolBar.SetEnable("barPrintBL", false);
                    toolBar.SetEnable("barPrintLoadCtn", false);
                    toolBar.SetEnable("barPrintLoadGoods", false);
                    toolBar.SetEnable("barE_MBL", false);
                    toolBar.SetEnable("barNBEDI", false);
                    toolBar.SetEnable("barISF", false);
                    toolBar.SetEnable("barBill", false);
                    toolBar.SetEnable("barVerifiSheet", false);
                    toolBar.SetEnable("barDelete", false);
                    toolBar.SetEnable("barPrint", false);

                    toolBar.SetEnable("barLoadShip", false);
                    toolBar.SetEnable("barCheck", false);
                    toolBar.SetEnable("barCompleteCheck", false);
                    toolBar.SetEnable("barReplyAgent", false);
                    toolBar.SetEnable("barDocumentDispatch", false);

                }
                else
                {
                    toolBar.SetEnable("barCopy", true);
                    toolBar.SetEnable("barPrintBL", true);
                    toolBar.SetEnable("barPrintLoadCtn", true);
                    toolBar.SetEnable("barPrintLoadGoods", true);
                    toolBar.SetEnable("barE_MBL", true);
                    //宁波edi中心
                    if (LocalData.UserInfo.DefaultCompanyID == new Guid("a62a9f8e-e69c-4e6e-ad85-e75aed3c6cf9"))
                        toolBar.SetEnable("barNBEDI", true);
                    else
                        toolBar.SetEnable("barNBEDI", false);
                    toolBar.SetEnable("barISF", true);
                    toolBar.SetEnable("barBill", true);
                    toolBar.SetEnable("barVerifiSheet", true);
                    toolBar.SetEnable("barPrint", true);
                    toolBar.SetEnable("barDocumentDispatch", true);
                    toolBar.SetEnable("barDelete", false);
                    toolBar.SetEnable("barLoadShip", false);
                    toolBar.SetEnable("barCheck", false);
                    toolBar.SetEnable("barCompleteCheck", false);
                    toolBar.SetEnable("barReplyAgent", false);
                    toolBar.SetEnable("barSplitBL", false);
                    toolBar.SetEnable("barMergeBL", false);
                    toolBar.SetEnable("barReleaseBL", false);

                    #region
                    //删除条件：
                    //MBL:没有HBL，并且没有费用 
                    //HBL:没有费用
                    if (!listData.ExistFees)
                    {
                        if (listData.HBLCount == 0 && listData.BLType == FCMBLType.MBL)
                        {
                            toolBar.SetEnable("barDelete", true);
                        }
                        if (listData.BLType == FCMBLType.HBL)
                        {
                            toolBar.SetEnable("barDelete", true);
                        }
                    }

                    ////有箱可以打印 
                    //if (string.IsNullOrEmpty(listData.ContainerNos)) toolBar.SetEnable("barPrint", false);
                    if (listData.BLType == FCMBLType.HBL) toolBar.SetEnable("barPrintLoadGoods", false);
                    // 如果有箱 或顺签提单，确认装船
                    if (string.IsNullOrEmpty(listData.ContainerNos) == false || listData.IssueType == IssueType.Post_date)
                    {
                        toolBar.SetEnable("barLoadShip", true);
                    }
                    if ((listData.OEOperationType == FCMOperationType.LCL || listData.OEOperationType == FCMOperationType.BULK)
                        && string.IsNullOrEmpty(listData.ContainerNos))
                    {
                        //装箱或散货，在没有箱号的情况下，不能进行装船确认
                        toolBar.SetEnable("barLoadShip", false);
                    }
                    if (listData.State == OEBLState.Draft
                        && listData.HBLCount <= 0 && string.IsNullOrEmpty(listData.ContainerNos) == false)
                    {
                        toolBar.SetEnable("barCheck", true);
                    }

                    if (listData.State != OEBLState.Release && listData.State != OEBLState.Checked
                       && listData.HBLCount <= 0 && string.IsNullOrEmpty(listData.ContainerNos) == false)
                    {
                        toolBar.SetEnable("barCompleteCheck", true);
                    }

                    if (listData.State != OEBLState.Checked && !listData.RBLD)
                    {
                        toolBar.SetEnable("barMergeBL", true);
                        toolBar.SetEnable("barReplyAgent", true);
                    }
                    //if (string.IsNullOrEmpty(listData.AgentName))
                    //{
                    //    toolBar.SetEnable("barDocumentDispatch", false);
                    //    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(_mainPart.FindForm(), LocalData.IsEnglish ? "No agent information." : "没有代理信息.");
                    //}

                    if (listData.State != OEBLState.Checked && !listData.RBLD)
                    {
                        toolBar.SetEnable("barSplitBL", true);
                    }
                    if (listData.State == OEBLState.Checked)
                    {
                        toolBar.SetEnable("barReleaseBL", true);
                    }

                    #endregion


                    #region  分文件菜单
                    if (listData.DocumentState == DocumentState.Reviseing)
                    {
                        toolBar.SetEnable("barAcceptBillRevise", true);
                    }
                    else
                    {
                        toolBar.SetEnable("barAcceptBillRevise", false);
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
            _cargoTracking = null;
            _mainListPart = null;
            _searchPart = null;
            _toolBar = null;
            _cargoTracking = null;
            _mainSpace = null;
        }

        #endregion
    }
}
