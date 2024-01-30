using Microsoft.Practices.CompositeUI;
using ICP.FRM.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace ICP.FRM.UI.SearchRate
{
    public class SearchTruckWorkitem : WorkItem
    {

        #region Show

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            SearchTruckMainWorkspace srMainWorkspace = SmartParts.Get<SearchTruckMainWorkspace>("SearchTruckMainWorkspace");
            if (srMainWorkspace == null)
            {
                srMainWorkspace = SmartParts.AddNew<SearchTruckMainWorkspace>("SearchTruckMainWorkspace");
                #region AddPart

                SearchTruckToolPart opToolBar = SmartParts.AddNew<SearchTruckToolPart>();
                IWorkspace toolBarWorkspace = (IWorkspace)Workspaces[SearchTruckWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(opToolBar);

                SearchTruckListPart opMainListPart = SmartParts.AddNew<SearchTruckListPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[SearchTruckWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(opMainListPart);

                SearchTruckRateInfoPart opEditPart = SmartParts.AddNew<SearchTruckRateInfoPart>();
                IWorkspace editWorkspace = (IWorkspace)Workspaces[SearchTruckWorkSpaceConstants.BaseInfoWorkspace];
                editWorkspace.Show(opEditPart);

                SearchTruckSearchPart opSearchPart = SmartParts.AddNew<SearchTruckSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)Workspaces[SearchTruckWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(opSearchPart);

                #endregion

                #region 定义面板连接 

                opSearchPart.OnSearched += delegate(object sender, object results)
                {
                    opMainListPart.DataSource = results;
                };
                opMainListPart.CurrentChanged += delegate(object sender, object data)
                {
                    SearchTruckRateList list = data as SearchTruckRateList;
                    opEditPart.DataSource = list;
                };
                opMainListPart.InvokeGetData += delegate(object sender, object data)
                {
                    opSearchPart.RaiseSearched(data);
                };

                #endregion

                IWorkspace mainWorkspace = Workspaces[SearchRateWorkSpaceConstants.TruckMainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = "Search Truck Rates";
                mainWorkspace.Show(srMainWorkspace, smartPartInfo);
            }
            else
            {
                Workspaces[SearchRateWorkSpaceConstants.TruckMainWorkspace].Activate(srMainWorkspace);
            }
        }

        #endregion
    }

    #region Constants

    /// <summary>
    /// SearchTruck WorkSpace 常量
    /// </summary>
    public class SearchTruckWorkSpaceConstants
    {
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string BaseInfoWorkspace = "BaseInfoWorkspace";
    }

    /// <summary>
    /// 海运运价查询常量
    /// </summary>
    public class SearchTruckCommandConstants
    {
        public const string Command_ShowSearch = "Command_ShowSearch";

        public const string Command_RefreshData = "Command_RefreshData";
    }

    #endregion
}
