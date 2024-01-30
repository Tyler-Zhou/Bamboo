using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.WinForms;
using Microsoft.Practices.CompositeUI.SmartParts;
using System.Collections;

namespace ICP.Common.UI.Configure.ChargingCode
{
    public class ChargingCodeInfoFinder : IDataFinder, IDisposable
    {
        /// <summary>
        /// IDataFinder 成员
        /// </summary>
        public bool IsBusy { get; set; }

        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        public IConfigureService configureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        #endregion

        private const string LocationFinderWorkspace = "ChargingCodeFinderWorkspace";


        #region ChildWorkitem

        ChargingCodeFinderWorkitem chargingcodeFinderWorkitem = null;
        ChargingCodeMultiFinderWorkitem multiFinderWorkitem = null;

        #endregion

        #region IDataFinder 成员

        public event EventHandler<DataFindEventArgs> DataChoosed;

        #region One

        public void PickOne(string searchValue, string property
            , SearchConditionCollection conditions, string[] returnFields
            , FinderTriggerType triggerType, System.Windows.Forms.Control container)
        {
            DeckWorkspace workspce = this.Workitem.Workspaces.Get<DeckWorkspace>(LocationFinderWorkspace);
            if (workspce == null || workspce.IsDisposed)
            {
                if (this.Workitem.Workspaces.Contains(LocationFinderWorkspace))
                {
                    this.Workitem.Workspaces.Remove(workspce);
                }

                workspce = this.Workitem.Workspaces.AddNew<DeckWorkspace>(LocationFinderWorkspace);
                workspce.Dock = System.Windows.Forms.DockStyle.Fill;
                workspce.BringToFront();
            }
            container.Controls.Clear();
            container.Controls.Add(workspce);
            PickOne(searchValue, property, conditions, returnFields, triggerType, LocationFinderWorkspace);
        }

        public void PickOne(string searchValue, string property
            , SearchConditionCollection conditions, string[] returnFields
            , FinderTriggerType triggerType, string workspaceName)
        {
            if (string.IsNullOrEmpty(workspaceName))
                workspaceName = ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace;

            IWorkspace workspace = this.Workitem.Workspaces[workspaceName];

            #region

            string name = string.Empty;
            if (triggerType == FinderTriggerType.KeyEnter) name = searchValue;

            Guid solutionID = Guid.Empty;
            if (conditions != null)
            {
                if (conditions.Contain("SolutionID"))
                {
                    solutionID = (Guid)conditions.GetValue("SolutionID").Value;
                }
            }


            List<ChargingCodeList> list = configureService.GetChargingCodeListBySearch(
                "", "", null, true, true, 100);

            if (list != null && list.Count == 1)
            {
                if (DataChoosed != null)
                    DataChoosed(this, new DataFindEventArgs(CommonUtility.GetSingleSearchResult<ChargingCodeList>(list[0], returnFields)));

                return;
            }

            #endregion

            chargingcodeFinderWorkitem = Workitem.WorkItems.Get<ChargingCodeFinderWorkitem>(this.GetHashCode().ToString() + "ChargingCodeSingleFinderWorkitem");
            if (chargingcodeFinderWorkitem == null)
            {
                chargingcodeFinderWorkitem = Workitem.WorkItems.AddNew<ChargingCodeFinderWorkitem>(this.GetHashCode().ToString() + "ChargingCodeSingleFinderWorkitem");
                chargingcodeFinderWorkitem.DataChoosed += delegate(object sender, DataFindEventArgs e)
                {
                    if (this.DataChoosed != null) DataChoosed(sender, e);
                };
            }

            Dictionary<string, object> initValues = new Dictionary<string, object>();
            initValues.Add("Name", name);
            initValues.Add("SolutionID", solutionID);

            chargingcodeFinderWorkitem.Show(workspace, list, returnFields, initValues);
        }

        #endregion

        #region Many

        public void PickMany(string searchValue, string property, SearchConditionCollection conditions
            , string[] returnFields, FinderTriggerType triggerType
            , GetExistValueHandler getExistValueHandler, System.Windows.Forms.Control container)
        {
            DeckWorkspace workspce = this.Workitem.Workspaces.Get<DeckWorkspace>(LocationFinderWorkspace);
            if (workspce == null || workspce.IsDisposed)
            {
                workspce = this.Workitem.Workspaces.AddNew<DeckWorkspace>(LocationFinderWorkspace);
                container.Controls.Add(workspce);
                workspce.Dock = System.Windows.Forms.DockStyle.Fill;
                workspce.BringToFront();
            }
            this.PickMany(searchValue, property, conditions, returnFields, triggerType, getExistValueHandler, LocationFinderWorkspace);
        }

        public void PickMany(string searchValue, string property
            , SearchConditionCollection conditions, string[] returnFields
            , FinderTriggerType triggerType, GetExistValueHandler getExistValueHandler, string workspaceName)
        {
            if (string.IsNullOrEmpty(workspaceName))
                workspaceName = ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace;

            IWorkspace workspace = this.Workitem.Workspaces[workspaceName];

            #region

            string name = string.Empty;
            if (triggerType == FinderTriggerType.KeyEnter) name = searchValue;

            Guid solutionID = Guid.Empty;
            if (conditions != null)
            {
                if (conditions.Contain("SolutionID"))
                {
                    solutionID = (Guid)conditions.GetValue("SolutionID").Value;
                }
            }
            List<SolutionChargingCodeList> list = configureService.GetSolutionChargingCodeListByList(solutionID, name, null, null, true);

            if (list != null && list.Count == 1)
            {
                if (DataChoosed != null)
                    DataChoosed(this, new DataFindEventArgs(CommonUtility.GetMultiSearchResult<SolutionChargingCodeList>(list, returnFields)));

                return;
            }

            List<Guid> existValues = new List<Guid>();
            IList exists = getExistValueHandler();
            if (exists != null && exists.Count > 0)
            {
                foreach (var item in exists)
                {
                    existValues.Add((Guid)item);
                }
            }
            List<SolutionChargingCodeList> existList = new List<SolutionChargingCodeList>();
            foreach (var item in list)
            {
                if (existValues.Contains(item.ID))
                    existList.Add(item);
            }

            #endregion

            multiFinderWorkitem = Workitem.WorkItems.Get<ChargingCodeMultiFinderWorkitem>(this.GetHashCode().ToString() + "ChargingCodeMultiFinderWorkitem");
            if (multiFinderWorkitem == null)
            {
                multiFinderWorkitem = Workitem.WorkItems.AddNew<ChargingCodeMultiFinderWorkitem>(this.GetHashCode().ToString() + "ChargingCodeMultiFinderWorkitem");
                multiFinderWorkitem.DataChoosed += delegate(object sender, DataFindEventArgs e)
                {
                    if (this.DataChoosed != null) DataChoosed(sender, e);
                };
            }

            Dictionary<string, object> initValues = new Dictionary<string, object>();

            initValues.Add("Name", name);
            initValues.Add("SolutionID", solutionID);

            multiFinderWorkitem.Show(workspace, list, existList, returnFields, initValues);
        }

        #endregion


        #endregion



        #region IDisposable 成员

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                this.DataChoosed = null;
                this.multiFinderWorkitem = null;
                this.chargingcodeFinderWorkitem = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            }
        }
        #endregion
    }



    #region

    public class ChargingCodeInfoWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolBarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";

        public const string SelectedListWorkspace = "SelectedListWorkspace";
        public const string SelectedToolBarWorkspace = "SelectedToolBarWorkspace";
    }

    public class ChargingCodeInfoCommonConstants
    {
        public const string Command_ShowSearch = "Command_ShowSearch";


        public const string Common_FinderConfirm = "Common_FinderConfirm";
        public const string Common_FindeSelect = "Common_FindeSelect";
        public const string Common_FinderRemove = "Common_FinderRemove";
        public const string Common_FinderRemoveAll = "Common_FinderRemoveAll";

    }

    #endregion
}
