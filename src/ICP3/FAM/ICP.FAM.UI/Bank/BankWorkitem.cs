using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FAM.ServiceInterface.DataObjects;

namespace ICP.FAM.UI
{
    class BankWorkitem : WorkItem
    {

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            BankMainWorkSpace mainSpce = SmartParts.Get<BankMainWorkSpace>("BankMainWorkSpace");
            if (mainSpce == null)
            {
                mainSpce = SmartParts.AddNew<BankMainWorkSpace>("BankMainWorkSpace");

                #region AddPart

                BankTool toolBar = SmartParts.AddNew<BankTool>();
                IWorkspace toolBarWorkspace = (IWorkspace)Workspaces[BankWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                BanksList listPart = SmartParts.AddNew<BanksList>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[BankWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                BankSearch searchPart = SmartParts.AddNew<BankSearch>();
                IWorkspace searchWorkspace = (IWorkspace)Workspaces[BankWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);


                UCBankAccountList detailPart = SmartParts.AddNew<UCBankAccountList>();
                detailPart.FormType = "List";
                IWorkspace detailWorkspace = (IWorkspace)Workspaces[BankWorkSpaceConstants.BankAccountListWorkspace];
                detailWorkspace.Show(detailPart);


                #endregion

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Bank List" : "银行列表";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                BankUIAdapter bookingAdapter = new BankUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(detailPart.GetType().Name,detailPart);

                bookingAdapter.Init(dic);
            }
            else
            {
                Workspaces[ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }
    }

    /// <summary>
    /// 命常量
    /// </summary>
    public class BankCommandConstants
    {

        public const string Command_BankAdd = "Command_BankAdd";

        public const string Command_BankEdit = "Command_BankEdit";

        public const string Command_BankCancel = "Command_BankCancel";

        public const string Command_BankRefreshData = "Command_BankRefreshData";

        public const string Command_BankShowSearch = "Command_BankShowSearch";


    }
    /// <summary>
    /// WorkSpace常量
    /// </summary>
    public class BankWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";

        public const string BankAccountListWorkspace = "BankAccountListWorkspace";
    }

    /// <summary>
    /// UI适配器
    /// </summary>
    public class BankUIAdapter:IDisposable
    {

        #region parts

        IToolBar _toolBar;
        ISearchPart _searchPart;
        BanksList _mainListPart;
        UCBankAccountList _bankAccountList;

        #endregion

        #region interface

        public void Init(Dictionary<string, object> controls)
        {
            _toolBar = (IToolBar)controls[typeof(BankTool).Name];
            _searchPart = (ISearchPart)controls[typeof(BankSearch).Name];
            _mainListPart = (BanksList)controls[typeof(BanksList).Name];

            _bankAccountList = (UCBankAccountList)controls[typeof(UCBankAccountList).Name];

            RefreshBarEnabled(_toolBar, null);
            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                BankList listData = data as BankList;

                RefreshBarEnabled(_toolBar, listData);
                if (listData != null)
                {
                    _bankAccountList.BankID = listData.ID;
                }
                else
                {
                    _bankAccountList.BankID = Guid.Empty;
                }

                _bankAccountList.BindDataList();
            };

            _mainListPart.Selected += delegate(object sender, object data)
            {

            };

            _mainListPart.InvokeGetData += delegate(object sender, object data)
            {
                _searchPart.RaiseSearched(data);
            };

            _mainListPart.KeyDown += new KeyEventHandler(_mainListPart_KeyDown);

            #endregion


            #region _searchPart.OnSearched
            _searchPart.OnSearched += delegate(object sender, object results)
            {
                _mainListPart.DataSource = results;
            };
            #endregion

            #endregion
        }

        void _mainListPart_KeyDown(object sender, KeyEventArgs e)
        {

            if (sender != null)
            {
                Dictionary<string, object> keyValue = sender as Dictionary<string, object>;
                if (keyValue != null)
                {
                    _searchPart.Init(keyValue);
                    _searchPart.RaiseSearched();
                }
            }
        }
        private void RefreshBarEnabled(IToolBar toolBar, BankList listData)
        {
            if (listData == null || listData.IsNew)
            {
                toolBar.SetEnable("barEdit", false);
                toolBar.SetEnable("barCancel", false);
            }
            else
            {
                toolBar.SetEnable("barEdit", true);
                toolBar.SetEnable("barCancel", true);

                if (listData.IsValid)
                {
                    toolBar.SetText("barCancel", LocalData.IsEnglish ? "Invalid" : "作废");
                }
                else
                {
                    toolBar.SetText("barCancel", LocalData.IsEnglish ? "Activation" : "激活");
                }
            }
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            _bankAccountList = null;
            _mainListPart = null;
            _searchPart = null;
            _toolBar = null;
        }

        #endregion
    }
 
}
