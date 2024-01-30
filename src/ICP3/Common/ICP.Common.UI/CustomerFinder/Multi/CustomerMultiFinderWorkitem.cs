using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.Common.UI.CustomerFinder
{
    public  class CustomerMultiFinderWorkitem:WorkItem
    {


        public event EventHandler<DataFindEventArgs> DataChoosed;
        CustomerFinderSearchPart _searchPart = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.DataChoosed = null;
                this._searchPart = null;
                
            }
            base.Dispose(disposing);
        }
        public void Show(IWorkspace mainWorkspace, List<CustomerInfo> list
            , List<CustomerInfo> existList, string[] returnFields
            , Dictionary<string, object> initValues)
        {

            if (mainWorkspace == null)
                mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];

            CustomerMultiFinderWorkspace locationMainSpce = this.SmartParts.Get<CustomerMultiFinderWorkspace>("CustomerMultiFinderWorkspace");
            if (locationMainSpce == null)
            {
                locationMainSpce = this.SmartParts.AddNew<CustomerMultiFinderWorkspace>("CustomerMultiFinderWorkspace");

                #region AddPart

                CustomerMultiFinderToolBar toolBar = this.SmartParts.AddNew<CustomerMultiFinderToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[CustomerWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                CustomerMultiMainListPart listPart = this.SmartParts.AddNew<CustomerMultiMainListPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[CustomerWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                MultiFinderSelectedToolBar selectedToolBar = this.SmartParts.AddNew<MultiFinderSelectedToolBar>();
                IWorkspace selectedToolBarWorkspace = (IWorkspace)this.Workspaces[CustomerWorkSpaceConstants.SelectedToolBarWorkspace];
                selectedToolBarWorkspace.Show(selectedToolBar);

                CustomerMultiSelectedListPart selectedListPart = this.SmartParts.AddNew<CustomerMultiSelectedListPart>();
                IWorkspace selectedListWorkspace = (IWorkspace)this.Workspaces[CustomerWorkSpaceConstants.SelectedListWorkspace];
                selectedListWorkspace.Show(selectedListPart);

                _searchPart = this.SmartParts.AddNew<CustomerFinderSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[CustomerWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(_searchPart);

                #endregion

                BulidConnection(toolBar, _searchPart, listPart, selectedListPart, selectedToolBar, returnFields);
                listPart.DataSource = list;
                selectedListPart.DataSource = existList;

                SmartPartInfo smartPartInfo = new SmartPartInfo();             
                string titel = LocalData.IsEnglish ? "Customer Finder" : "查找客户";
                CustomerType? type = null;
                bool? isAgentOfCarrier = null;
                foreach (var item in initValues)
                {
                    if (item.Key.ToUpper() == "CustomerType")
                    {
                        List<CustomerType> types = item.Value as List<CustomerType>;
                        if (types.Count == 1)
                        {
                            type = types[0];
                        }
                    }
                    else if (item.Key.ToUpper() == "IsAgentOfCarrier")
                    {
                        isAgentOfCarrier = (bool)item.Value;
                    }
                }

                if (type != null)
                {
                    switch (type)
                    {
                        case CustomerType.Airline: titel = LocalData.IsEnglish ? "Airline Finder" : "航空公司查找"; break;
                        case CustomerType.Carrier: titel = LocalData.IsEnglish ? "Carrier Finder" : "船东查找"; break;
                        case CustomerType.Forwarding:
                            if (isAgentOfCarrier == true)
                            {
                                titel = LocalData.IsEnglish ? "Agent of Carrier Finder" : "承运人查找";
                            }
                            else
                            {
                                titel = LocalData.IsEnglish ? "Forwarding Customer Finder" : "代理查找";
                            }

                            break;
                        case CustomerType.Broker: titel = LocalData.IsEnglish ? "Customs Broker Finder" : "报关行查找"; break;
                        case CustomerType.Trucker: titel = LocalData.IsEnglish ? "Trucker Finder" : "拖车行查找"; break;
                        case CustomerType.Warehouse: titel = LocalData.IsEnglish ? "Warehouse Finder" : "仓储查找"; break;
                        case CustomerType.Storage: titel = LocalData.IsEnglish ? "Storage Finder" : "堆场查找"; break;
                    }
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
                List<CustomerInfo> newSelectedList = data as List<CustomerInfo>;
                if (newSelectedList == null || newSelectedList.Count == 0) return;

                List<CustomerInfo> selcted = selectedListPart.DataSource as List<CustomerInfo>;
                if (selcted == null) selcted = new List<CustomerInfo>();

                List<CustomerInfo> needAddList = new List<CustomerInfo>();
                foreach (var item in newSelectedList)
                {
                    CustomerInfo tager = selcted.Find(delegate(CustomerInfo uItem) { return uItem.ID == item.ID; });
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
                List<CustomerInfo> newSelectedList = data as List<CustomerInfo>;
                if (newSelectedList == null) newSelectedList = new List<CustomerInfo>();

                if (this.DataChoosed != null)
                    this.DataChoosed(sender, new DataFindEventArgs(CommonUtility.GetMultiSearchResult<CustomerInfo>(newSelectedList, returnFields)));

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
