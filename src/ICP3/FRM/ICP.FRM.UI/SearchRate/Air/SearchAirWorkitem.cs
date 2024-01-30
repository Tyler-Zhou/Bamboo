using Microsoft.Practices.CompositeUI;
using ICP.FRM.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace ICP.FRM.UI.SearchRate
{
    public class SearchAirWorkitem : WorkItem
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region Show

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            SearchAirMainWorkspace srMainWorkspace = SmartParts.Get<SearchAirMainWorkspace>("SearchAirMainWorkspace");
            if (srMainWorkspace == null)
            {
                srMainWorkspace = SmartParts.AddNew<SearchAirMainWorkspace>("SearchAirMainWorkspace");
                #region AddPart

                SearchAirToolPart opToolBar = SmartParts.AddNew<SearchAirToolPart>();
                IWorkspace toolBarWorkspace = (IWorkspace)Workspaces[SearchAirWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(opToolBar);

                SearchAirListPart opMainListPart = SmartParts.AddNew<SearchAirListPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[SearchAirWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(opMainListPart);

                SearchAirRateInfoPart opEditPart = SmartParts.AddNew<SearchAirRateInfoPart>();
                IWorkspace editWorkspace = (IWorkspace)Workspaces[SearchAirWorkSpaceConstants.BaseInfoWorkspace];
                editWorkspace.Show(opEditPart);

                SearchAirSearchPart opSearchPart = SmartParts.AddNew<SearchAirSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)Workspaces[SearchAirWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(opSearchPart);

                #endregion

                #region 定义面板连接 

                opSearchPart.OnSearched += delegate(object sender, object results)
                {
                    opMainListPart.DataSource = results;
                };
                opMainListPart.CurrentChanged += delegate(object sender, object data)
                {
                    SearchAirRateList list = data as SearchAirRateList;
                    opEditPart.DataSource = list;
                };
                opMainListPart.InvokeGetData += delegate(object sender, object data)
                {
                    opSearchPart.RaiseSearched(data);
                };

                #endregion

                IWorkspace mainWorkspace = Workspaces[SearchRateWorkSpaceConstants.AirMainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = "Search Air Rates";
                mainWorkspace.Show(srMainWorkspace, smartPartInfo);
            }
            else
            {
                Workspaces[SearchRateWorkSpaceConstants.AirMainWorkspace].Activate(srMainWorkspace);
            }
        }

        #endregion
    }

    #region Constants

    /// <summary>
    /// SearchAir WorkSpace 常量
    /// </summary>
    public class SearchAirWorkSpaceConstants
    {
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string BaseInfoWorkspace = "BaseInfoWorkspace";
    }

    /// <summary>
    /// 海运运价查询常量
    /// </summary>
    public class SearchAirCommandConstants
    {
        public const string Command_ShowSearch = "Command_ShowSearch";

        public const string Command_RefreshData = "Command_RefreshData";
    }

    #endregion
}
