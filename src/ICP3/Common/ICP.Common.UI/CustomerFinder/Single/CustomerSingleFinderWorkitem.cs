using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.Common.UI.CustomerFinder
{
    public class CustomerSingleFinderWorkitem : WorkItem
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.DataChoosed = null;
                this._listPart = null;
                this._searchPart = null;
            }
            base.Dispose(disposing);
        }

        public event EventHandler<DataFindEventArgs> DataChoosed;
        CustomerFinderSearchPart _searchPart = null;
        CustomerSingleMainListPart _listPart = null;

        public void Show(IWorkspace mainWorkspace, List<CustomerInfo> list, string[] returnFields, Dictionary<string, object> initValues)
        {
            if (mainWorkspace == null)
                mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];

            CustomerSingleFinderWorkspace locationMainSpce = this.SmartParts.Get<CustomerSingleFinderWorkspace>("CustomerSingleFinderWorkspace");
            if (locationMainSpce == null)
            {
                locationMainSpce = this.SmartParts.AddNew<CustomerSingleFinderWorkspace>("CustomerSingleFinderWorkspace");

                #region AddPart

                CustomerSingleFinderToolBar toolBar = this.SmartParts.AddNew<CustomerSingleFinderToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[CustomerWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                _listPart = this.SmartParts.AddNew<CustomerSingleMainListPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[CustomerWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(_listPart);

                _searchPart = this.SmartParts.AddNew<CustomerFinderSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[CustomerWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(_searchPart);

                #endregion

                BulidConnection(toolBar, _searchPart, _listPart, returnFields);
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                string titel = LocalData.IsEnglish ? "Customer Finder" : "查找客户";
                CustomerType? type = null;
                bool? isAgentOfCarrier = null;
                foreach (var item in initValues)
                {
                    if (item.Key == "CustomerType")
                    {
                        List<CustomerType> types = item.Value as List<CustomerType>;
                        if (types.Count == 1)
                        {
                            type = types[0];
                        }
                    }
                    else if (item.Key == "IsAgentOfCarrier")
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
            if (list != null)
            {
                _listPart.DataSource = list;

                //根据CodeOrName查询出来的结果可能会有很多。
                //出于便利性考虑，默定位到与code完全匹配的结果中。
                if (_listPart is CustomerSingleMainListPart)
                {
                    ((CustomerSingleMainListPart)_listPart).LocateToMatchedItem(_searchPart.txtCodeOrName.Text);
                }
            }
        }

        private void BulidConnection(BaseEditPart toolBar
                                     , BaseSearchPart searchPart
                                     , BaseListPart listPart
                                     , string[] returnFields)
        {
            listPart.Selected += delegate(object sender, object data)
            {
                CustomerInfo list = data as CustomerInfo;
                if (list == null) return;
                if (DataChoosed != null)
                {
                    DataChoosed(sender, new DataFindEventArgs(CommonUtility.GetSingleSearchResult<CustomerInfo>(list, returnFields)));
                }
            };

            listPart.CurrentChanged += delegate(object sender, object data)
            {
                toolBar.DataSource = data;
            };

            searchPart.OnSearched += delegate(object sender, object results)
            {
                listPart.DataSource = results;

                //根据CodeOrName查询出来的结果可能会有很多。
                //出于便利性考虑，默定位到与code完全匹配的结果中。
                if (listPart is CustomerSingleMainListPart)
                {
                    ((CustomerSingleMainListPart)listPart).LocateToMatchedItem(_searchPart.txtCodeOrName.Text);
                }
            };
        }
    }
}
