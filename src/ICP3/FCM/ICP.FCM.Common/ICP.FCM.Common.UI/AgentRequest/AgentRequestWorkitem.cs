using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.Common.ServiceInterface.DataObjects;

namespace ICP.FCM.Common.UI.AgentRequest
{
    public class AgentRequestWorkitem:WorkItem
    {  
    
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }


        private void Show()
        {
            AgentRequestWorkspace mainSpce = this.SmartParts.Get<AgentRequestWorkspace>("AgentRequestWorkspace");
            if (mainSpce == null)
            {
                mainSpce = this.SmartParts.AddNew<AgentRequestWorkspace>("AgentRequestWorkspace");

                #region AddPart

                AgentRequestToolBar toolBar = this.SmartParts.AddNew<AgentRequestToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[AgentRequestWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                AgentRequestListPart listPart = this.SmartParts.AddNew<AgentRequestListPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[AgentRequestWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                AgentRequestSearchPart searchPart = this.SmartParts.AddNew<AgentRequestSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[AgentRequestWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);

                #endregion

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Agent Request" : "申请代理列表";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                AgentRequestUIAdapter bookingAdapter = new AgentRequestUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                bookingAdapter.Init(dic);
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }

    }

    public class AgentRequestWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";
    }

    public class OEAgentRequesCommandConstants
    {
        public const string Command_Assign = "Command_Assign";
        public const string Command_ShowSearch = "Command_ShowSearch";
        public const string Command_Reject = "Command_Reject";
    }

    public class AgentRequestUIAdapter
    {

        #region parts

        IToolBar _toolBar;
        ISearchPart _searchPart;
        IListPart _mainListPart;

        #endregion

        #region interface

        public void Init(Dictionary<string, object> controls)
        {

            _toolBar = (IToolBar)controls[typeof(AgentRequestToolBar).Name];
            _searchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(AgentRequestSearchPart).Name];
            _mainListPart = (IListPart)controls[typeof(AgentRequestListPart).Name];

            RefreshBarEnabled(_toolBar, null);

            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                AgentRequestInfo listData = data as AgentRequestInfo;
                RefreshBarEnabled(_toolBar, listData);
            };
            #endregion

            #region _searchPart.OnSearched
            _searchPart.OnSearched += delegate(object sender, object results)
            {
                _mainListPart.DataSource = results;
            };
            #endregion

            #endregion
        }
        private void RefreshBarEnabled(IToolBar toolBar, AgentRequestInfo listData)
        {
            //if (listData == null || listData.IsNew||listData.State != AgentRequestStateEnum.Requesting)
            if (listData == null || listData.IsNew)
            {
                toolBar.SetEnable("barReject", false);
                toolBar.SetEnable("barEdit", false);
            }
            else 
            {
                toolBar.SetEnable("barEdit", true);
                toolBar.SetEnable("barReject", true);
            }
        }

        #endregion
    }
}
