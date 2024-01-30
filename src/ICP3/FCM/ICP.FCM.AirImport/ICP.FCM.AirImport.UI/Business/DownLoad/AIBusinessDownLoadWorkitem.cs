using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.AirImport.ServiceInterface;

namespace ICP.FCM.AirImport.UI
{
    public class OIBusinessDownLoadWorkitem : WorkItem
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        public IAirImportService AirImportService
        {
            get
            {
                return ServiceClient.GetService<IAirImportService>();
            }
        }


        #endregion
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Saved = null;
                this.Workitem = null;
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
            OIBusinessDownloadMain mainSpce = this.SmartParts.Get<OIBusinessDownloadMain>("OIBusinessDownloadMain");
            if (mainSpce == null)
            {
                mainSpce = this.SmartParts.AddNew<OIBusinessDownloadMain>("OIBusinessDownloadMain");

                #region AddPart

                OIBusinessDownloadTool toolBar = this.SmartParts.AddNew<OIBusinessDownloadTool>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[OIBusinessWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                OIBusinessDownloadList listPart = this.SmartParts.AddNew<OIBusinessDownloadList>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[OIBusinessWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                OIBusinessDownLoadSearch searchPart = this.SmartParts.AddNew<OIBusinessDownLoadSearch>();
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[OIBusinessWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);


                OIOrderFeeEdit feePart = this.SmartParts.AddNew<OIOrderFeeEdit>();
                feePart.FormType = "BusinessDownload";
                feePart.SetService(Workitem);
                IWorkspace feehWorkspace = (IWorkspace)this.Workspaces[OIBusinessWorkSpaceConstants.FeeWorkspace];
                feehWorkspace.Show(feePart);

                #endregion

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "DownLoadAirImportBusiness" : "下载空运进口业务单";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                OIBDUIAdapter orderAdapter = new OIBDUIAdapter();
                orderAdapter.Workitem = Workitem;
                orderAdapter.AirImportService = AirImportService;

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(feePart.GetType().Name, feePart);
                dic.Add(this.GetType().Name,this);
                orderAdapter.Init(dic);

            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }

        /// <summary>
        /// 下载数据
        /// </summary>
        public event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

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

    }

    public class OIBDUIAdapter
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        [ServiceDependency]
        public IAirImportService AirImportService { get; set; }


        #endregion

        #region parts

        IToolBar _toolBar;
        ISearchPart _searchPart;
        OIBusinessDownloadList _mainListPart;
        ISearchPart _fastSearchPart;
        OIOrderFeeEdit _feePart;
        OIBusinessDownLoadWorkitem downLoadWorkitem;



        #endregion

        #region interface

        public void Init(Dictionary<string, object> controls)
        {

            _toolBar = (IToolBar)controls[typeof(OIBusinessDownloadTool).Name];
            _searchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(OIBusinessDownLoadSearch).Name];
            _mainListPart = (OIBusinessDownloadList)controls[typeof(OIBusinessDownloadList).Name];
            _feePart = (OIOrderFeeEdit)controls[typeof(OIOrderFeeEdit).Name];
            downLoadWorkitem=(OIBusinessDownLoadWorkitem)controls[typeof(OIBusinessDownLoadWorkitem).Name];

            #region Connection

            #region 列表行发生改变
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                AirBusinessDownLoadList listData = data as AirBusinessDownLoadList;

                if (listData == null)
                {
                    return;
                }

                List<AirImportFeeList> feeList=AirImportService.GetAirExportFeeList(listData.ID);
                _feePart.SetCompanyID(listData.CompanyID.Value);
                _feePart.SetSource(feeList);

            };
            #endregion

            #region 查询面板查询数据
            _searchPart.OnSearched += delegate(object sender, object results)
            {
                _mainListPart.DataSource = results;
                List<AirBusinessDownLoadList> list = results as List<AirBusinessDownLoadList>;
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
 
        #endregion
    }
}
