using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.Common.UI.Configure.ChargingCode
{
    public class ChargingCodeMultiFinderWorkitem : WorkItem
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.DataChoosed = null;
                 
                if (_searchPart != null)
                {
                    this.Items.Remove(_searchPart);
                    _searchPart = null;
                }

            }
            base.Dispose(disposing);
        }

        public event EventHandler<DataFindEventArgs> DataChoosed;
        ChargingCodeFinderSearchPart _searchPart = null;

        public void Show(IWorkspace mainWorkspace, List<SolutionChargingCodeList> list
            , List<SolutionChargingCodeList> existList, string[] returnFields
            , Dictionary<string, object> initValues)
        {

            if (mainWorkspace == null)
                mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];

            ChargingCodeMultiFinderWorkspace chargeCodeMainSpce = this.SmartParts.Get<ChargingCodeMultiFinderWorkspace>("ChargingCodeMultiFinderWorkspace");
            if (chargeCodeMainSpce == null)
            {
                chargeCodeMainSpce = this.SmartParts.AddNew<ChargingCodeMultiFinderWorkspace>("ChargingCodeMultiFinderWorkspace");

                #region AddPart

                ChargingCodeMultiFinderToolBar toolBar = this.SmartParts.AddNew<ChargingCodeMultiFinderToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[ChargingCodeWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                ChargingCodeMultiMainListPart listPart = this.SmartParts.AddNew<ChargingCodeMultiMainListPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[ChargingCodeWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                ChargingCodeMultiFinderSelectedToolBar selectedToolBar = this.SmartParts.AddNew<ChargingCodeMultiFinderSelectedToolBar>();
                IWorkspace selectedToolBarWorkspace = (IWorkspace)this.Workspaces[ChargingCodeWorkSpaceConstants.SelectedToolBarWorkspace];
                selectedToolBarWorkspace.Show(selectedToolBar);

                ChargingCodeMultiSelectedListPart selectedListPart = this.SmartParts.AddNew<ChargingCodeMultiSelectedListPart>();
                IWorkspace selectedListWorkspace = (IWorkspace)this.Workspaces[ChargingCodeWorkSpaceConstants.SelectedListWorkspace];
                selectedListWorkspace.Show(selectedListPart);

                _searchPart = this.SmartParts.AddNew<ChargingCodeFinderSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[ChargingCodeWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(_searchPart);

                #endregion

                BulidConnection(toolBar, _searchPart, listPart, selectedListPart, selectedToolBar, returnFields);
                listPart.DataSource = list;
                selectedListPart.DataSource = existList;

                SmartPartInfo smartPartInfo = new SmartPartInfo();

                string titel = LocalData.IsEnglish ? "ChargeCode Finder" : "查找费用";


                smartPartInfo.Title = titel;
                mainWorkspace.Show(chargeCodeMainSpce, smartPartInfo);
            }
            else
            {
                mainWorkspace.Activate(chargeCodeMainSpce);
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
                List<SolutionChargingCodeList> newSelectedList = data as List<SolutionChargingCodeList>;
                if (newSelectedList == null || newSelectedList.Count == 0) return;

                List<SolutionChargingCodeList> selcted = selectedListPart.DataSource as List<SolutionChargingCodeList>;
                if (selcted == null) selcted = new List<SolutionChargingCodeList>();

                List<SolutionChargingCodeList> needAddList = new List<SolutionChargingCodeList>();
                foreach (var item in newSelectedList)
                {
                    SolutionChargingCodeList tager = selcted.Find(delegate(SolutionChargingCodeList uItem) { return uItem.ID == item.ID; });
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
                List<SolutionChargingCodeList> newSelectedList = data as List<SolutionChargingCodeList>;
                if (newSelectedList == null) newSelectedList = new List<SolutionChargingCodeList>();

                if (this.DataChoosed != null)
                    this.DataChoosed(sender, new DataFindEventArgs(CommonUtility.GetMultiSearchResult<SolutionChargingCodeList>(newSelectedList, returnFields)));

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
