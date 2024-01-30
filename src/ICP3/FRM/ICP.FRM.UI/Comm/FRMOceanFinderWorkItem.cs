using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.UI.Geography.Location;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.FRM.UI.Comm
{
    public class FRMOceanFinderWorkItem : WorkItem
    {
        #region Service

        public IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DataChoosed = null;
                if (_listPart != null)
                {
                    Items.Remove(_listPart);
                    _listPart = null;
                }
                if (_searchPart != null)
                {
                    Items.Remove(_searchPart);
                    _searchPart = null;
                }
                
            }
            base.Dispose(disposing);
        }
        #endregion

        #region 属性
        public List<LocationList> DataList
        {
            get;
            set;
        }
        public string[] ReturnFields
        {
            get;
            set;
        }
        public Dictionary<string, object> InitValues
        {
            get;
            set;
        }
        public string TextValue
        {
            get;
            set;
        }
        public event EventHandler<DataFindEventArgs> DataChoosed;
        LocationFinderSearchPart _searchPart = null;
        LocationSingleMainListPart _listPart = null;
        
        #endregion

        #region Show

        protected override void OnRunStarted()
        {
            base.OnRunStarted();

            List<LocationList> list = GeographyService.GetLocationList(TextValue, null, null, true, null, null, true, 100);

            if (list != null && list.Count == 1)
            {
                if (DataChoosed != null)
                    DataChoosed(this, new DataFindEventArgs(Utility.GetSingleSearchResult<LocationList>(list[0], ReturnFields)));

                return;
            }
            else
            {
                DataList = list;
                Show(DataList, ReturnFields, InitValues);
            }
        }


        public void Show(List<LocationList> list, string[] returnFields, Dictionary<string, object> initValues)
        {

                FRMOceanFinderWorkspace locationMainSpce = SmartParts.AddNew<FRMOceanFinderWorkspace>("FRMOceanFinderWorkspace");

                #region AddPart

                LocationSingleFinderToolBar toolBar = SmartParts.AddNew<LocationSingleFinderToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)Workspaces[LocationWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                _listPart = SmartParts.AddNew<LocationSingleMainListPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[LocationWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(_listPart);

                _searchPart = SmartParts.AddNew<LocationFinderSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)Workspaces[LocationWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(_searchPart);

                #endregion

                BulidConnection(toolBar, _searchPart, _listPart, returnFields);

                _searchPart.Init(initValues);
                if (list != null)
                {
                    _listPart.DataSource = list;
                }
                string title=LocalData.IsEnglish?"Ocean Port Finder":"海运港口查找";
                PartLoader.ShowDialog(locationMainSpce,title);


        
        }

        private void BulidConnection(BaseEditPart toolBar
                             , BaseSearchPart searchPart
                             , BaseListPart listPart
                             , string[] returnFields)
        {
            listPart.Selected += delegate(object sender, object data)
            {
                LocationList list = data as LocationList;
                if (list == null) return;
                if (DataChoosed != null)
                {
                    DataChoosed(sender, new DataFindEventArgs(Utility.GetSingleSearchResult<LocationList>(list, returnFields)));
                }
            };

            listPart.CurrentChanged += delegate(object sender, object data)
            {
                toolBar.DataSource = data;
            };

            searchPart.OnSearched += delegate(object sender, object results)
            {
                listPart.DataSource = results;
            };
        }
        #endregion



    }
}
