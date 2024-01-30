using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Windows.Forms;
using ICP.FRM.UI.ProfitRatios;
using ICP.FRM.ServiceInterface.DataObjects;

namespace ICP.FRM.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class PRWorkitem : WorkItem
    {
        /// <summary>
        /// 
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            PRMainWorkspace mainSpce = SmartParts.Get<PRMainWorkspace>(ProfitRatiosConstants.MainWorkspace);
            if (mainSpce == null)
            {
                mainSpce = SmartParts.AddNew<PRMainWorkspace>(ProfitRatiosConstants.MainWorkspace);

                #region AddPart

                PRToolBar toolBar = SmartParts.AddNew<PRToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)Workspaces[ProfitRatiosConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                PRListPart listPart = SmartParts.AddNew<PRListPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[ProfitRatiosConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                PRSearchPart searchPart = SmartParts.AddNew<PRSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)Workspaces[ProfitRatiosConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);


                PRFastSearchPart fastSearchPart = SmartParts.AddNew<PRFastSearchPart>();
                IWorkspace fastSearchPartWorkspace = (IWorkspace)Workspaces[ProfitRatiosConstants.FastSearchWorkspace];
                fastSearchPartWorkspace.Show(fastSearchPart);

                PREditPart eventListPart = SmartParts.AddNew<PREditPart>();
                IWorkspace eventListWorkspace = (IWorkspace)Workspaces[ProfitRatiosConstants.ProfitRatiosWorkspace];
                eventListWorkspace.Show(eventListPart);

                #endregion

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Profit Ratios" : "利润配比";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                PRUIAdapter orderAdapter = new PRUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(fastSearchPart.GetType().Name, fastSearchPart);
                dic.Add(eventListPart.GetType().Name, eventListPart);
                orderAdapter.Init(dic);
            }
            else
            {
                Workspaces[ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class PRUIAdapter : IDisposable
    {
        #region parts
        IToolBar _toolBar;
        ISearchPart _searchPart;
        IListPart _mainListPart;
        ISearchPart _fastSearchPart;
        IListPart _detailListPart;

        #endregion

        #region interface
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controls"></param>
        public void Init(Dictionary<string, object> controls)
        {

            _toolBar = (IToolBar)controls[typeof(PRToolBar).Name];
            _searchPart = (ISearchPart)controls[typeof(PRSearchPart).Name];
            _mainListPart = (IListPart)controls[typeof(PRListPart).Name];
            _fastSearchPart = (ISearchPart)controls[typeof(PRFastSearchPart).Name];
            _detailListPart = (IListPart)controls[typeof(PREditPart).Name];

            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                if (data != null)
                {
                    _detailListPart.DataSource = data;
                }
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
        }

        
        #endregion

        #region IDisposable 成员
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _detailListPart = null;
            _fastSearchPart = null;
            _mainListPart = null;
            _searchPart = null;
            _toolBar = null;
        }

        #endregion
    }
}
