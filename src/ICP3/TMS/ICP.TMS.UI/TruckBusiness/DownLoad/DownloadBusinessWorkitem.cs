using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.TMS.UI
{
    public class DownloadBusinessWorkitem : WorkItem
    {


        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Saved = null;
            }
            base.Dispose(disposing);
        }
        /// <summary>
        /// 显示界面
        /// </summary>
        private void Show()
        {
            DownloadBusinessMain mainSpce = this.SmartParts.Get<DownloadBusinessMain>("TMSDownloadBusinessMain");
            if (mainSpce == null)
            {
                mainSpce = this.SmartParts.AddNew<DownloadBusinessMain>("TMSDownloadBusinessMain");

                #region AddPart

                DownloadBusinessTool toolBar = this.SmartParts.AddNew<DownloadBusinessTool>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[TruckBookingsWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                DownloadBusinessList listPart = this.SmartParts.AddNew<DownloadBusinessList>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[TruckBookingsWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                DownLoadBusinessSearch searchPart = this.SmartParts.AddNew<DownLoadBusinessSearch>();
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[TruckBookingsWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);


                #endregion

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "DownLoadBusiness" : "下载业务数据";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                OIBDUIAdapter orderAdapter = new OIBDUIAdapter();
              

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(this.GetType().Name,this);
                orderAdapter.Init(dic);


                listPart.Saved += new SavedHandler(listPart_Saved);

            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }

        void listPart_Saved(params object[] prams)
        {
            if (Saved != null)
            {
                Saved(prams);
            }
        }

        /// <summary>
        /// 下载数据
        /// </summary>
        public event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

    }

    /// <summary>
    /// 命令常量
    /// </summary>
    public class TMSDownLoadCommandConstants
    {
        public const string Command_SearchDate = "Command_SearchDate";
        public const string Command_ShowSearch = "BusinessDownLoadCommand_ShowSearch";
        public const string Command_Print = "BusinessDownLoadCommand_Print";
        public const string Command_DownLoad = "BusinessDownLoadCommand_DownLoad";
        public const string Command_ListCurrentChanged = "Command_ListCurrentChanged";
        public const string Command_BusinessDownLoadCurrentRow = "Command_BusinessDownLoadCurrentRow";

    }

    public class OIBDUIAdapter:IDisposable
    {

        #region parts

        DownloadBusinessTool _toolBar;
        DownLoadBusinessSearch _searchPart;
        DownloadBusinessList _mainListPart;



        #endregion

        #region interface

        public void Init(Dictionary<string, object> controls)
        {

            _toolBar = (DownloadBusinessTool)controls[typeof(DownloadBusinessTool).Name];
            _searchPart = (DownLoadBusinessSearch)controls[typeof(DownLoadBusinessSearch).Name];
            _mainListPart = (DownloadBusinessList)controls[typeof(DownloadBusinessList).Name];
           
            #region Connection



            #region 查询面板查询数据
            _searchPart.OnSearched += delegate(object sender, object results)
            {
                _mainListPart.DataSource = results;              
            };
            #endregion


            #region 下载数据


            #endregion
       
            #endregion

        }
 
        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            this._mainListPart = null;
            this._searchPart = null;
            this._toolBar = null;
      
        }

        #endregion
    }
}
