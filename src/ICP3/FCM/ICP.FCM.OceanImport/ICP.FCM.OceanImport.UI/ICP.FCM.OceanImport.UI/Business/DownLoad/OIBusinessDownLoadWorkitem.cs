using ICP.Business.Common.UI.Document;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.UI.Common.Parts;
using ICP.FCM.Common.UI.DispatchCompare;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.FCM.OceanImport.UI.Business.DownLoad;
using ICP.FileSystem.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;

namespace ICP.FCM.OceanImport.UI
{
    public class OIBusinessDownLoadWorkitem : WorkItem
    {
        #region Service

        public DocumentListPresenter DocumentListPresenter
        {
            get
            {
                return ClientHelper.Get<DocumentListPresenter, DocumentListPresenter>();
            }
        }

        public IFileService FileService
        {
            get
            {
                return ServiceClient.GetService<IFileService>();
            }
        }
        #endregion
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Saved = null;
                dataSource = null;
            }
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
            OIBusinessDownloadMain mainSpce = SmartParts.Get<OIBusinessDownloadMain>("OIBusinessDownloadMain");
            if (mainSpce == null)
            {
                mainSpce = SmartParts.AddNew<OIBusinessDownloadMain>("OIBusinessDownloadMain");

                #region AddPart

                OIBusinessDownloadTool toolBar = SmartParts.AddNew<OIBusinessDownloadTool>();
                IWorkspace toolBarWorkspace = (IWorkspace)Workspaces[OIBusinessWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                OIBusinessDownloadList listPart = SmartParts.AddNew<OIBusinessDownloadList>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[OIBusinessWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                OIBusinessDownLoadSearch searchPart = SmartParts.AddNew<OIBusinessDownLoadSearch>();
                IWorkspace searchWorkspace = (IWorkspace)Workspaces[OIBusinessWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);


                OIOrderFeeEdit feePart = SmartParts.AddNew<OIOrderFeeEdit>();
                feePart.FormType = "BusinessDownload";
                feePart.SetService(this);
                IWorkspace feehWorkspace = (IWorkspace)Workspaces[OIBusinessWorkSpaceConstants.FeeWorkspace];
                feehWorkspace.Show(feePart);


                OIAgentDispatchInfoPart operationPart = Items.AddNew<OIAgentDispatchInfoPart>();
                IWorkspace operationPartWorkSpace = (IWorkspace)Workspaces[OIBusinessWorkSpaceConstants.OperationPartWorkspace];
                operationPartWorkSpace.Show(operationPart);

                UCDocumentDispatchPartNew documentList = Items.AddNew<UCDocumentDispatchPartNew>();
                IWorkspace documentListWorkSpace = (IWorkspace)Workspaces[OIBusinessWorkSpaceConstants.DocumentListWorkspace];
                //SetGridCtl(documentList);
                documentList.IsBindPendingDate = false;
                documentListWorkSpace.Show(documentList);
                documentList.RemarkReadOnly = true;
                documentList.IsShowBusinessControl = false;
                documentList.IsCurrentUpdateHide=true;

                HistoryOceanRecordPart  historyOceanRecordPart=Items.AddNew<HistoryOceanRecordPart>();

                IWorkspace acceptPartWorkspace=(IWorkspace)Workspaces[OIBusinessWorkSpaceConstants.AcceptWorkspace];
                historyOceanRecordPart.Type=1;
                acceptPartWorkspace.Show(historyOceanRecordPart);


                #endregion

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "DownLoadOceanImportBusiness" : "下载海运进口业务单";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                OIBDUIAdapter orderAdapter = new OIBDUIAdapter();
                orderAdapter.Workitem =this;
              

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(feePart.GetType().Name, feePart);
                dic.Add(documentList.GetType().Name, documentList);
                dic.Add(operationPart.GetType().Name, operationPart);
                dic.Add(historyOceanRecordPart.GetType().Name, historyOceanRecordPart);
                dic.Add(GetType().Name, this);
                orderAdapter.Init(dic, DocumentListPresenter, FileService);

            }
            else
            {
                Workspaces[ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }

        void SetGridCtl()
        {

        }

        /// <summary>
        /// 下载数据
        /// </summary>
        public event SavedHandler Saved;

        private object dataSource;
        public object DataSource
        {
            get
            {
                return dataSource;
            }
            set
            {
                if (dataSource != value)
                {
                    dataSource = value;
                    if (Saved != null)
                    {
                        Saved(dataSource);
                    }
                }
            }
        }

    }

    /// <summary>
    /// 命令常量
    /// </summary>
    public class OIBusinessDownLoadCommandConstants
    {
        public const string Command_SearchDate = "Command_SearchDate";
        public const string Command_ShowSearch = "BusinessDownLoadCommand_ShowSearch";
        public const string Command_Print = "BusinessDownLoadCommand_Print";
        public const string Command_DownLoad = "BusinessDownLoadCommand_DownLoad";
        public const string Command_ListCurrentChanged = "Command_ListCurrentChanged";
        public const string Command_BusinessDownLoadCurrentRow = "Command_BusinessDownLoadCurrentRow";
        public const string Command_DownLoadNew = "Command_DownLoadNew";
        public const string Command_AcceptedNew = "Command_AcceptedNew";
        public const string Command_AcceptDocument = "Command_AcceptDocument";
        public const string Command_UnAcceptDocument = "Command_UnAcceptDocument";
        public const string Command_AssignTo = "Command_AssignTo";
        public const string Command_Transition = "Command_Transition";
        public const string Command_PrintAll = "Command_PrintAll";

    }

    public class OIBDUIAdapter:IDisposable
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        public IOceanImportService OceanImportService
        {
            get
            {
                return ServiceClient.GetService<IOceanImportService>();
            }
        }
        #endregion

        #region parts

        OIBusinessDownloadTool _toolBar;
        ISearchPart _searchPart;
        OIBusinessDownloadList _mainListPart;
        ISearchPart _fastSearchPart;
        OIOrderFeeEdit _feePart;
        OIBusinessDownLoadWorkitem downLoadWorkitem;
        UCDocumentDispatchPartNew _DocumentList;
        OIAgentDispatchInfoPart operationPart;
        HistoryOceanRecordPart _HistoryOceanRecordPart;

        #endregion

        #region interface

        public void Init(Dictionary<string, object> controls, DocumentListPresenter documentListPresenter, IFileService fileService)
        {

            _toolBar = (OIBusinessDownloadTool)controls[typeof(OIBusinessDownloadTool).Name];
            _searchPart = (ISearchPart)controls[typeof(OIBusinessDownLoadSearch).Name];
            _mainListPart = (OIBusinessDownloadList)controls[typeof(OIBusinessDownloadList).Name];
            _feePart = (OIOrderFeeEdit)controls[typeof(OIOrderFeeEdit).Name];
            _DocumentList = (UCDocumentDispatchPartNew)controls[typeof(UCDocumentDispatchPartNew).Name];
            downLoadWorkitem = (OIBusinessDownLoadWorkitem)controls[typeof(OIBusinessDownLoadWorkitem).Name];
            operationPart = (OIAgentDispatchInfoPart)controls[typeof(OIAgentDispatchInfoPart).Name];
            _HistoryOceanRecordPart=(HistoryOceanRecordPart)controls[typeof(HistoryOceanRecordPart).Name];
            Init();
            #region Connection

            #region 列表行发生改变
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                OceanBusinessDownLoadList listData = data as OceanBusinessDownLoadList;
                RefreshBarEnabled(_toolBar, listData);
                if (listData != null)
                {
                    BusinessOperationContext context = new BusinessOperationContext
                    {
                        OperationID = listData.OceanBookingID,
                        FormType = FormType.ShippingOrder,
                        OperationType = OperationType.OceanExport
                    };

                    List<OceanImportFeeList> feeList = OceanImportService.GetOceanExportFeeList(listData.ID);
                    _feePart.SetCompanyID(listData.CompanyID.Value);
                    _feePart.SetSource(feeList);
                    context.Add("DocumentState", listData.DocumentState);
                    //设置文档列表
                    _mainListPart.colIsError.Visible = false;
                    _DocumentList.ContextHistory = new BusinessOperationContext() { OperationID = listData.RefID };
                    _DocumentList.ContextCurrent = context;
                    _DocumentList.OceanAgentDispatchID = listData.OceanAgentDispatchID;
                    _DocumentList.SetDataSource();
                    AgentDispatchInfo info = fileService.GetAgentDispatchInfo(listData.OceanAgentDispatchID);
                    operationPart.SetAgentDisPatchInfo(info);

                    _HistoryOceanRecordPart.OperationID = listData.OceanBookingID;

                    _HistoryOceanRecordPart.BindingData();
                }
                else
                {
                    ClearData();
                }
            };
            #endregion

            #region 查询面板查询数据
            _searchPart.OnSearched += delegate(object sender, object results)
            {
                _mainListPart.DataSource = results;
                List<OceanBusinessDownLoadList> list = results as List<OceanBusinessDownLoadList>;
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        item.BeginEdit();
                    }
                }
            };
            #endregion


            #region 下载数据


            #endregion
            _mainListPart.Saved += delegate(object[] prams)
            {
                downLoadWorkitem.DataSource = prams[0];
            };
            #endregion

        }
        void ClearData()
        {
            _DocumentList.DataSource = new List<DocumentInfo>();
            operationPart.SetAgentDisPatchInfo(null);
            _HistoryOceanRecordPart.DataSource = new List<HistoryOceanRecord>();
        }

        void Init()
        {
            _mainListPart.colIsError.Visible = false;
            _mainListPart.AgentDispatchInfoPart = operationPart;
            _mainListPart.BusinessToolPart = _toolBar;
        }

        public void RefreshBarEnabled(OIBusinessDownloadTool _toolBar, OceanBusinessDownLoadList listData)
        {
            if (listData == null)
            {
                _toolBar.SetEnable("toolAccepted", false);
                _toolBar.SetEnable("toolUnAccepted", false);
                //_toolBar.SetEnable("toolTransition", false);
                _toolBar.SetEnable("toolAssignTo", false);
            }
            else
            {
                _toolBar.SetEnable("toolAssignTo", false);
                //状态为已签收
                if (listData.DocumentState == DocumentState.Accepted)
                {
                    _toolBar.SetEnable("toolAccepted", false);
                    _toolBar.SetEnable("toolUnAccepted", true);
                    // _toolBar.SetEnable("toolTransition", false);
                    _toolBar.SetEnable("toolAssignTo", true);
                }
                //状态为已分发
                else if (listData.DocumentState == DocumentState.Dispatched)
                {
                    _toolBar.SetEnable("toolAccepted", true);
                    _toolBar.SetEnable("toolUnAccepted", false);
                    // _toolBar.SetEnable("toolTransition", true);
                }
                //状态为待定
                else if (listData.DocumentState == DocumentState.Pending)
                {
                    _toolBar.SetEnable("toolUnAccepted", false);
                    _toolBar.SetEnable("toolAssignTo", false);
                    _toolBar.SetEnable("toolAccepted", false);
                    // _toolBar.SetEnable("toolTransition", false);
                }
                else if (listData.DocumentState == DocumentState.Reviseing || listData.DocumentState == DocumentState.Revised)
                {
                    _toolBar.SetEnable("toolAccepted", false);
                    _toolBar.SetEnable("toolUnAccepted", false);
                }
            }
        }
        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            _DocumentList = null;
            _fastSearchPart = null;
            _feePart = null;
            _mainListPart = null;
            _searchPart = null;
            _toolBar = null;
            downLoadWorkitem = null;
            operationPart = null;
            _HistoryOceanRecordPart = null;
        }

        #endregion
    }
}
