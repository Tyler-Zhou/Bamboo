using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.Common.UI.Geography.Location
{
    public  class LocationMultiFinderWorkitem:WorkItem
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.DataChoosed = null;
                this._searchPart = null;
            }
            base.Dispose(disposing);
        }
        public event EventHandler<DataFindEventArgs> DataChoosed;
        LocationFinderSearchPart _searchPart = null;

        public void Show(IWorkspace mainWorkspace, List<LocationList> list
            , List<LocationList> existList, string[] returnFields
            , Dictionary<string, object> initValues)
        {

            if (mainWorkspace == null)
                mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];

            LocationMultiFinderWorkspace locationMainSpce = this.SmartParts.Get<LocationMultiFinderWorkspace>("LocationMultiFinderWorkspace");
            if (locationMainSpce == null)
            {
                locationMainSpce = this.SmartParts.AddNew<LocationMultiFinderWorkspace>("LocationMultiFinderWorkspace");

                #region AddPart

                LocationMultiFinderToolBar toolBar = this.SmartParts.AddNew<LocationMultiFinderToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[LocationWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                LocationMultiMainListPart listPart = this.SmartParts.AddNew<LocationMultiMainListPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[LocationWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                MultiFinderSelectedToolBar selectedToolBar = this.SmartParts.AddNew<MultiFinderSelectedToolBar>();
                IWorkspace selectedToolBarWorkspace = (IWorkspace)this.Workspaces[LocationWorkSpaceConstants.SelectedToolBarWorkspace];
                selectedToolBarWorkspace.Show(selectedToolBar);

                LocationMultiSelectedListPart selectedListPart = this.SmartParts.AddNew<LocationMultiSelectedListPart>();
                IWorkspace selectedListWorkspace = (IWorkspace)this.Workspaces[LocationWorkSpaceConstants.SelectedListWorkspace];
                selectedListWorkspace.Show(selectedListPart);

                _searchPart = this.SmartParts.AddNew<LocationFinderSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[LocationWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(_searchPart);

                #endregion

                BulidConnection(toolBar, _searchPart, listPart, selectedListPart, selectedToolBar, returnFields);
                listPart.DataSource = list;
                selectedListPart.DataSource = existList;

                SmartPartInfo smartPartInfo = new SmartPartInfo();

                string titel = LocalData.IsEnglish ? "Location Finder" : "查找地点";
                foreach (var item in initValues)
                {
                    if (item.Key.ToUpper() == "ISOCEAN")
                        titel = LocalData.IsEnglish ? "Ocean Port Finder" : "海运港口查找";
                    else if (item.Key.ToUpper() == "ISAIR")
                        titel = LocalData.IsEnglish ? "Air Port Finder" : "空运港口查找";
                    else if (item.Key.ToUpper() == "ISOTHER")
                        titel = LocalData.IsEnglish ? "Other Port Finder" : "其它港口查找";
                }

                smartPartInfo.Title = titel;
                mainWorkspace.Show(locationMainSpce, smartPartInfo);
            }
            else
            {
                mainWorkspace.Activate(locationMainSpce);
            }

            _searchPart.Init(initValues);
        }

        private void BulidConnection(BaseEditPart toolBar
                             , BaseSearchPart searchPart
                             , BaseListPart listPart
                             , BaseListPart selectedListPart
                             , BaseEditPart selectedToolBar
                             , string[] returnFields)
        {

            listPart.Selected += delegate(object sender, object data)
            {
                List<LocationList> newSelectedList = data as List<LocationList>;
                if (newSelectedList == null || newSelectedList.Count == 0) return;

                List<LocationList> selcted = selectedListPart.DataSource as List<LocationList>;
                if (selcted == null) selcted = new List<LocationList>();

                List<LocationList> needAddList = new List<LocationList>();
                foreach (var item in newSelectedList)
                {
                    LocationList tager = selcted.Find(delegate(LocationList uItem) { return uItem.ID == item.ID; });
                    if (tager != null) continue;
                    needAddList.Add(item);
                }

                selcted.AddRange(needAddList);
                selectedListPart.DataSource = selcted;

            };

            listPart.CurrentChanged += delegate(object sender, object data)
            {
                toolBar.DataSource = data;
            };

            selectedListPart.Selected += delegate(object sender, object data)
            {
                List<LocationList> newSelectedList = data as List<LocationList>;
                if (newSelectedList == null) newSelectedList = new List<LocationList>();

                if (this.DataChoosed != null)
                    this.DataChoosed(sender, new DataFindEventArgs(CommonUtility.GetMultiSearchResult<LocationList>(newSelectedList, returnFields)));

            };

            selectedListPart.CurrentChanged += delegate(object sender, object data)
            {
                selectedToolBar.DataSource = data;
            };

            searchPart.OnSearched += delegate(object sender, object results)
            {
                listPart.DataSource = results;
            };
        }
    }
}
