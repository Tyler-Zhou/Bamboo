using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.Common.UI.Configure.ChargingCode
{
    public class ChargingCodeSingleFinderWorkitem : WorkItem
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
        ChargingCodeFinderSearchPart _searchPart = null;
        ChargingCodeMainListPart _listPart = null;

        public void Show(IWorkspace mainWorkspace, List<SolutionChargingCodeList> list, string[] returnFields, Dictionary<string, object> initValues)
        {
            if (mainWorkspace == null)
                mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];

            ChargingCodeSingleFinderWorkspace locationMainSpce = this.SmartParts.Get<ChargingCodeSingleFinderWorkspace>("ChargingCodeSingleFinderWorkspace");
            if (locationMainSpce == null)
            {
                locationMainSpce = this.SmartParts.AddNew<ChargingCodeSingleFinderWorkspace>("ChargingCodeSingleFinderWorkspace");

                #region AddPart

                ChargingCodeSingleFinderToolBar toolBar = this.SmartParts.AddNew<ChargingCodeSingleFinderToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[ChargingCodeWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                _listPart = this.SmartParts.AddNew<ChargingCodeSingleMainListPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[ChargingCodeWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(_listPart);

                _searchPart = this.SmartParts.AddNew<ChargingCodeFinderSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[ChargingCodeWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(_searchPart);

                #endregion

                BulidConnection(toolBar, _searchPart, _listPart, returnFields);
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                string titel = LocalData.IsEnglish ? "ChargeCode Finder" : "查找费用";

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
                SolutionChargingCodeList list = data as SolutionChargingCodeList;
                if (list == null) return;
                if (DataChoosed != null)
                {
                    DataChoosed(sender, new DataFindEventArgs(CommonUtility.GetSingleSearchResult<SolutionChargingCodeList>(list, returnFields)));
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
