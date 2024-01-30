using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.WinForms;
using Microsoft.Practices.CompositeUI.SmartParts;
using System.Collections;

namespace ICP.Common.UI.Geography.Location
{
    public class LocationFinder : IDataFinder,IDisposable
    {
        /// <summary>
        /// IDataFinder 成员
        /// </summary>
        public bool IsBusy { get; set; }

        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
            }
        }

        #endregion

        private const string LocationFinderWorkspace = "LocationFinderWorkspace";

        #region ChildWorkitem

        LocationSingleFinderWorkitem singleFinderWorkitem = null;
        LocationMultiFinderWorkitem multiFinderWorkitem = null;

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

            List<LocationList> list = GeographyService.GetLocationList(name, null, null, IsOcean,IsAir,IsOther, true, 100);

            if (list != null && list.Count == 1)
            {
                if (DataChoosed != null)
                    DataChoosed(this, new DataFindEventArgs(CommonUtility.GetSingleSearchResult<LocationList>(list[0], returnFields)));

                return;
            }

            #endregion

            singleFinderWorkitem = Workitem.WorkItems.Get<LocationSingleFinderWorkitem>(this.GetHashCode().ToString() + "LocationSingleFinderWorkitem");
            if (singleFinderWorkitem == null)
            {
                singleFinderWorkitem = Workitem.WorkItems.AddNew<LocationSingleFinderWorkitem>(this.GetHashCode().ToString() + "LocationSingleFinderWorkitem");
                singleFinderWorkitem.DataChoosed += delegate(object sender, DataFindEventArgs e)
                {
                    if (this.DataChoosed != null) DataChoosed(sender, e);
                };
            }

            Dictionary<string, object> initValues = new Dictionary<string, object>();
            initValues.Add("Name", name);
            if (IsOcean != null)
                initValues.Add("IsOcean", IsOcean);

            if (IsAir != null)
                initValues.Add("IsAir", IsAir);

            if (IsOther != null)
                initValues.Add("IsOther", IsOther);

            singleFinderWorkitem.Show(workspace, list, returnFields, initValues);
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

            List<LocationList> list = GeographyService.GetLocationList(name, null, null, IsOcean, IsAir, IsOther, true, 100);


            if (list != null && list.Count == 1)
            {
                if (DataChoosed != null)
                    DataChoosed(this, new DataFindEventArgs(CommonUtility.GetMultiSearchResult<LocationList>(list, returnFields)));

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
            List<LocationList> existList = new List<LocationList>();
            foreach (var item in list)
            {
                if (existValues.Contains(item.ID))
                    existList.Add(item);
            }

            #endregion

            multiFinderWorkitem = Workitem.WorkItems.Get<LocationMultiFinderWorkitem>(this.GetHashCode().ToString() + "LocationMultiFinderWorkitem");
            if (multiFinderWorkitem == null)
            {
                multiFinderWorkitem = Workitem.WorkItems.AddNew<LocationMultiFinderWorkitem>(this.GetHashCode().ToString() + "LocationMultiFinderWorkitem");
                multiFinderWorkitem.DataChoosed += delegate(object sender, DataFindEventArgs e)
                {
                    if (this.DataChoosed != null) DataChoosed(sender, e);
                };
            }

            Dictionary<string, object> initValues = new Dictionary<string, object>();

            initValues.Add("Name", name);

            if (IsOcean != null)
                initValues.Add("IsOcean", IsOcean);

            if (IsAir != null)
                initValues.Add("IsAir", IsAir);

            if (IsOther != null)
                initValues.Add("IsOther", IsOther);

            multiFinderWorkitem.Show(workspace, list, existList, returnFields, initValues);
        }

        #endregion


        #endregion

        #region 属性

        protected virtual bool? IsOcean { get { return null; } }
        protected virtual bool? IsAir { get { return null; } }
        protected virtual bool? IsOther { get { return null; } }

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
                if (Workitem != null)
                {
                    this.multiFinderWorkitem = null;
                    this.singleFinderWorkitem = null;
                    this.DataChoosed = null;
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            }
        }
        #endregion
    }

    public class OceanLocationFinder : LocationFinder
    {
        protected override bool? IsOcean { get { return true; } }
    }

    public class AirLocationFinder : LocationFinder
    {
         protected override bool? IsAir { get { return true; } }
    }
    public class OtherLocationFinder : LocationFinder
    {
         protected override bool? IsOther { get { return true; } }
    }

    #region 

    public class LocationWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolBarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";
       
        public const string SelectedListWorkspace = "SelectedListWorkspace";
        public const string SelectedToolBarWorkspace = "SelectedToolBarWorkspace";
    }

    public class LocationCommonConstants
    {
        public const string Command_ShowSearch = "Command_ShowSearch";


        public const string Common_FinderConfirm = "Common_FinderConfirm";
        public const string Common_FindeSelect = "Common_FindeSelect";
        public const string Common_FinderRemove = "Common_FinderRemove";
        public const string Common_FinderRemoveAll = "Common_FinderRemoveAll";

    }

    #endregion
}
