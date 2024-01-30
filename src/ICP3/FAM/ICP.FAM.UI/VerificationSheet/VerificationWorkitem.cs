using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;

using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;

namespace ICP.FAM.UI.VerificationSheet
{
    public class VerificationSheetWorkItem : WorkItem
    {

        protected override void OnRunStarted()
        {
            base.OnRunStarted();

            Show();
        }

        private void Show()
        {
            VerificationMainWorkSpace mainSpace = SmartParts.Get<VerificationMainWorkSpace>("VerificationMainWorkSpace");

            if (mainSpace == null)
            {
                mainSpace = Items.AddNew<VerificationMainWorkSpace>("VerificationMainWorkSpace");

                #region AddPart

                VerificationSheetToolBar toolBar = Items.AddNew<VerificationSheetToolBar>();
                IWorkspace toolBarSpace = Workspaces[VerificationWorkSpaceConstants.ToolBarWorkspace];
                toolBarSpace.Show(toolBar);

                VerificationSheetSearchPart searchPart = Items.AddNew<VerificationSheetSearchPart>();
                IWorkspace searchSpace = Workspaces[VerificationWorkSpaceConstants.SearchWorkspace];
                searchSpace.Show(searchPart);

                VerificationSheetListPart listPart = SmartParts.AddNew<VerificationSheetListPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[VerificationWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                VerificationSheetEditPart editor = SmartParts.AddNew<VerificationSheetEditPart>();
                IWorkspace editworkspace = (IWorkspace)Workspaces[VerificationWorkSpaceConstants.EditWorkspace];
                editor.Enabled = false;
                editworkspace.Show(editor);

                #endregion

                VerificationUIAdapter adapter = new VerificationUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(editor.GetType().Name, editor);
                adapter.Init(dic);

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Verifi.Sheet List" : "核销单列表";
                mainWorkspace.Show(mainSpace, smartPartInfo);
            }
            else
            {
                Workspaces[ClientConstants.MainWorkspace].Activate(mainSpace);
            }
        }
    }

    #region 常量
    public class VerificationCommandConstants
    {
        public const string Command_Add = "Command_Add";
        public const string Command_Delete = "Command_Delete";
        public const string Command_ShowSearch = "Command_ShowSearch";
    }

    public class VerificationWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";
        public const string EditWorkspace = "EditWorkspace";
    }

    #endregion

    public class VerificationUIAdapter:IDisposable
    {
        IToolBar _toolBar;
        ISearchPart _searchPart;
        VerificationSheetListPart _mainListPart;
        VerificationSheetEditPart _editPart;

        public void Init(Dictionary<string, object> controls)
        {
            _toolBar = (IToolBar)controls[typeof(VerificationSheetToolBar).Name];
            _searchPart = (ISearchPart)controls[typeof(VerificationSheetSearchPart).Name];
            _mainListPart = (VerificationSheetListPart)controls[typeof(VerificationSheetListPart).Name];
            _editPart = (VerificationSheetEditPart)controls[typeof(VerificationSheetEditPart).Name];

            RefreshBarEnabled(_toolBar, null);

            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                Common.ServiceInterface.DataObjects.VerificationSheet listData = data as Common.ServiceInterface.DataObjects.VerificationSheet;
                //Dictionary<string, object> keyValue = new Dictionary<string, object>();
                //keyValue.Add("ParentList", data);

                #region toolBar

                RefreshBarEnabled(_toolBar, listData);

                #endregion

                _editPart.DataSource = listData;
            };

            _mainListPart.CurrentChanging += delegate(object sender, CancelEventArgs e)
            {
                UIConnectionHelper.ParentChangingForEditPart(_mainListPart
                                                            , _editPart.SaveData
                                                            , (_editPart.DataSource as Common.ServiceInterface.DataObjects.VerificationSheet)
                                                            , e
                                                            , LocalData.IsEnglish ? "Verification Sheet" : "核销单");
            };
            _mainListPart.KeyDown += new KeyEventHandler(_mainListPart_KeyDown);

            _editPart.Saved += delegate(object[] data)
            {
                _mainListPart.Refresh(data);
            };
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
        private void RefreshBarEnabled(IToolBar toolBar, Common.ServiceInterface.DataObjects.VerificationSheet listData)
        {
            if (listData == null)
            {
                toolBar.SetEnable("barDelete", false);
            }
            else
            {
                toolBar.SetEnable("barDelete", true);             
            }
        }

        #region IDisposable 成员

        public void Dispose()
        {
            _editPart = null;
            _mainListPart.KeyDown -= _mainListPart_KeyDown;
            _mainListPart = null;
            _searchPart = null;
            _toolBar = null;
        }

        #endregion
    }
}
