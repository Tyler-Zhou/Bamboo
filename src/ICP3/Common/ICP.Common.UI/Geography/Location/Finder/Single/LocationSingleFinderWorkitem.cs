using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.Common.UI.Geography.Location
{
    public class LocationSingleFinderWorkitem : WorkItem
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.DataChoosed = null;
                this._searchPart = null;
                this._listPart = null;
            }
            base.Dispose(disposing);
        }
        public event EventHandler<DataFindEventArgs> DataChoosed;
        LocationFinderSearchPart _searchPart = null;
        LocationSingleMainListPart _listPart = null;

        public void Show(IWorkspace mainWorkspace, List<LocationList> list, string[] returnFields, Dictionary<string, object> initValues)
        {
            if (mainWorkspace == null)
                mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];

            LocationSingleFinderWorkspace locationMainSpce = this.SmartParts.Get<LocationSingleFinderWorkspace>("LocationSingleFinderWorkspace");
            if (locationMainSpce == null)
            {
                locationMainSpce = this.SmartParts.AddNew<LocationSingleFinderWorkspace>("LocationSingleFinderWorkspace");

                #region AddPart

                LocationSingleFinderToolBar toolBar = this.SmartParts.AddNew<LocationSingleFinderToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[LocationWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                _listPart = this.SmartParts.AddNew<LocationSingleMainListPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[LocationWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(_listPart);

                _searchPart = this.SmartParts.AddNew<LocationFinderSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[LocationWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(_searchPart);

                #endregion

                BulidConnection(toolBar, _searchPart, _listPart, returnFields);
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
            if (list != null) _listPart.DataSource = list;            
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
                    DataChoosed(sender, new DataFindEventArgs(CommonUtility.GetSingleSearchResult<LocationList>(list, returnFields)));
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
    }
}
