using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FAM.ServiceInterface.DataObjects;
using System;

namespace ICP.FAM.UI
{
    class AgentBillCheckingWorkitem : WorkItem
    {

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            AgentBillCheckingMainWorkSpace mainSpce = SmartParts.Get<AgentBillCheckingMainWorkSpace>("AgentBillCheckingMainWorkSpace");
            if (mainSpce == null)
            {
                mainSpce = SmartParts.AddNew<AgentBillCheckingMainWorkSpace>("AgentBillCheckingMainWorkSpace");

                #region AddPart


                AgentBillCheckingList listPart = SmartParts.AddNew<AgentBillCheckingList>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[AgentBillCheckingWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);


                AgentBillCheckingSearch searchPart = SmartParts.AddNew<AgentBillCheckingSearch>();
                IWorkspace searchWorkspace = (IWorkspace)Workspaces[AgentBillCheckingWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);


                AgentBillCheckingTool toolPart = SmartParts.AddNew<AgentBillCheckingTool>();
                IWorkspace toolWorkspace = (IWorkspace)Workspaces[AgentBillCheckingWorkSpaceConstants.ToolBarWorkspace];
                toolWorkspace.Show(toolPart);

                #endregion

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "AgentBillChecking" : "代理对账";
                mainWorkspace.Show(mainSpce, smartPartInfo);

                AgentBillCheckingUIAdapter ABCUIAdapter = new AgentBillCheckingUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(toolPart.GetType().Name, toolPart);

                ABCUIAdapter.Init(dic);
            }
            else
            {
                Workspaces[ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }
    }

    /// <summary>
    /// 命常量
    /// </summary>
    public class ABCCommandConstants
    {
        public const string Command_ABCAdd = "COMMAND_ABCADD";

        public const string Command_ABCOpen = "COMMAND_ABCOPEN";

        public const string Command_ABCDelete = "COMMAND_ABCDELETE";

        public const string Command_ABCShowSearch = "COMMAND_ABCSHOWSEARCH";
    }
    /// <summary>
    /// WorkSpace常量
    /// </summary>
    public class AgentBillCheckingWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolbarWorkspace";

        public const string SearchWorkspace = "SearchWorkspace";

        public const string ListWorkspace = "ListWorkspace";

    }

    /// <summary>
    /// UI适配器
    /// </summary>
    public class AgentBillCheckingUIAdapter:IDisposable
    {
        #region parts
        AgentBillCheckingTool agentBillCheckTool;
        AgentBillCheckingSearch agentBillCheckSearch;
        AgentBillCheckingList agentBillCheckList;
        #endregion

        #region interface

        public void Init(Dictionary<string, object> controls)
        {
            agentBillCheckTool = (AgentBillCheckingTool)controls[typeof(AgentBillCheckingTool).Name];
            agentBillCheckSearch = (AgentBillCheckingSearch)controls[typeof(AgentBillCheckingSearch).Name];
            agentBillCheckList = (AgentBillCheckingList)controls[typeof(AgentBillCheckingList).Name];

            RefreshBarEnabled(agentBillCheckTool, null);
                  
            #region Connection

            #region 分页
            agentBillCheckList.InvokeGetData += delegate(object sender, object data)
            {
                agentBillCheckSearch.RaiseSearched(data);
            };
            #endregion

            #region _mainListPart.CurrentChanged
            agentBillCheckList.CurrentChanged += delegate(object sender, object data)
            {
                AgnetBillCheckList list = data as AgnetBillCheckList;
                RefreshBarEnabled(agentBillCheckTool, list);
            };

            #endregion

            #region _searchPart.OnSearched
            agentBillCheckSearch.OnSearched += delegate(object sender, object results)
            {
                agentBillCheckList.DataSource = results;
            };
            #endregion

            #endregion
        }

        private void RefreshBarEnabled(IToolBar toolBar, AgnetBillCheckList listData)
        {
            if (listData == null || listData.IsNew)
            {
                toolBar.SetEnable("barOpen", false);
                toolBar.SetEnable("barDelete", false);
            }
            else
            {
                toolBar.SetEnable("barOpen", true);
                if (listData.Status == AgentBillCheckStatusEnum.Completed)
                {
                    toolBar.SetEnable("barDelete", false);
                }
                else
                {
                    toolBar.SetEnable("barDelete", true);
                }
            }
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            agentBillCheckList = null;
            agentBillCheckSearch = null;
            agentBillCheckTool = null;
        }

        #endregion
    }
 
}
