using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;

namespace ICP.FAM.UI.BankTransaction.Finder
{
    public class SingleFinderWorkitem : WorkItem
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DataChoosed = null;
                _listPart = null;
                _searchPart = null;
            }
            base.Dispose(disposing);
        }

        public event EventHandler<DataFindEventArgs> DataChoosed;
        FinderSearchPart _searchPart = null;
        SingleFinderListPart _listPart = null;

        public void Show(IWorkspace mainWorkspace, List<BankTransactionInfo> list, string[] returnFields, Dictionary<string, object> initValues)
        {
            if (mainWorkspace == null)
                mainWorkspace = Workspaces[ClientConstants.MainWorkspace];

            SingleFinderWorkspace locationMainSpce = SmartParts.Get<SingleFinderWorkspace>(BankTransactionFinderConstants.SINGLEFINDERMAINWORKSPACE);
            if (locationMainSpce == null)
            {
                locationMainSpce = SmartParts.AddNew<SingleFinderWorkspace>(BankTransactionFinderConstants.SINGLEFINDERMAINWORKSPACE);

                #region AddPart

                SingleFinderToolBar toolBar = SmartParts.AddNew<SingleFinderToolBar>();
                IWorkspace toolBarWorkspace = Workspaces[BankTransactionFinderConstants.SINGLEFINDERTOOLBARWORKSPACE];
                toolBarWorkspace.Show(toolBar);

                _listPart = SmartParts.AddNew<SingleFinderListPart>();
                IWorkspace listWorkspace = Workspaces[BankTransactionFinderConstants.SINGLEFINDERLISTWORKSPACE];
                listWorkspace.Show(_listPart);

                _searchPart = SmartParts.AddNew<FinderSearchPart>();
                IWorkspace searchWorkspace = Workspaces[BankTransactionFinderConstants.SINGLEFINDERSEARCHWORKSPACE];
                searchWorkspace.Show(_searchPart);

                #endregion

                BulidConnection(toolBar, _searchPart, _listPart, returnFields);
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                string titel = LocalData.IsEnglish ? "Bank Transaction Finder" : "查找银行流水记录";
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
                if (_listPart is SingleFinderListPart)
                {
                    ((SingleFinderListPart)_listPart).LocateToMatchedItem(_searchPart.UniqueNO);
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
                BankTransactionInfo list = data as BankTransactionInfo;
                if (list == null) return;
                if (DataChoosed != null)
                {
                    DataChoosed(sender, new DataFindEventArgs(Framework.ClientComponents.Controls.Utility.GetSingleFinderResult(list, returnFields)));
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
                if (listPart is SingleFinderListPart)
                {
                    ((SingleFinderListPart)listPart).LocateToMatchedItem(_searchPart.UniqueNO);
                }
            };
        }
    }
}
