using System.Collections.Generic;
using System.Windows.Forms;
using ICP.FAM.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using System;

namespace ICP.FAM.UI
{
    class JournalWorkitem : WorkItem
    {

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            JournalMainWorkSpace mainSpce = SmartParts.Get<JournalMainWorkSpace>("JournalMainWorkSpace");
            if (mainSpce == null)
            {
                mainSpce = SmartParts.AddNew<JournalMainWorkSpace>("JournalMainWorkSpace");

                #region AddPart

                JournalToolPart toolBar = SmartParts.AddNew<JournalToolPart>();
                IWorkspace toolBarWorkspace = (IWorkspace)Workspaces[JournalWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                JournalListPart listPart = SmartParts.AddNew<JournalListPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[JournalWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                JournalSearchPart searchPart = SmartParts.AddNew<JournalSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)Workspaces[JournalWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);

                #endregion

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Journal List" : "日记帐";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                JournalUIAdapter bookingAdapter = new JournalUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);

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
    public class JournalCommandConstants
    {

        public const string Command_JournalAdd = "Command_JournalAdd";

        public const string Command_JournalEdit = "Command_JournalEdit";

        public const string Command_JournalCancel = "Command_JournalCancel";

        public const string Command_JournalRefreshData = "Command_JournalRefreshData";

        public const string Command_JournalShowSearch = "Command_JournalShowSearch";


    }
    /// <summary>
    /// WorkSpace常量
    /// </summary>
    public class JournalWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";

    }

    /// <summary>
    /// UI适配器
    /// </summary>
    public class JournalUIAdapter : IDisposable
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region parts

        IToolBar _toolBar;
        ISearchPart _searchPart;
        JournalListPart _mainListPart;

        #endregion

        #region interface

        public void Init(Dictionary<string, object> controls)
        {
            _toolBar = (IToolBar)controls[typeof(JournalToolPart).Name];
            _searchPart = (ISearchPart)controls[typeof(JournalSearchPart).Name];
            _mainListPart = (JournalListPart)controls[typeof(JournalListPart).Name];
            RefreshBarEnabled(_toolBar, null);
            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                JournalList listData = data as JournalList;

                RefreshBarEnabled(_toolBar, listData);

            };

            _mainListPart.Selected += delegate(object sender, object data)
         {

         };
            _mainListPart.KeyDown += new KeyEventHandler(_mainListPart_KeyDown);
            #endregion

            #region _searchPart.OnSearched
            _searchPart.OnSearched += delegate(object sender, object results)
            {
                _mainListPart.DataSource = results;
            };
            #region _mainListPart.InvokeGetData
            _mainListPart.InvokeGetData += delegate(object sender, object data)
            {
                _searchPart.RaiseSearched(data);
            };
            #endregion
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
        private void RefreshBarEnabled(IToolBar toolBar,JournalList listData)
        {
            if (listData == null || listData.IsNew)
            {
                toolBar.SetEnable("barEdit", false);
                toolBar.SetEnable("barDelete", false);
            }
            else
            {
                toolBar.SetEnable("barEdit", true);
                toolBar.SetEnable("barDelete", true);
                if (listData.IsValid)
                {
                    toolBar.SetText("barDelete", LocalData.IsEnglish ? "Invalid" : "作废");
                }
                else
                {
                    toolBar.SetText("barDelete", LocalData.IsEnglish ? "Activation" : "激活");
                }
            }
        }

        #endregion

        #region IDisposable 成员

        public void Dispose() 
        {
            if (_mainListPart != null)
            {
                _mainListPart.KeyDown -= _mainListPart_KeyDown;
                _mainListPart = null;
            }
            _searchPart = null;
            _toolBar = null;
        }

        #endregion
    }

}
