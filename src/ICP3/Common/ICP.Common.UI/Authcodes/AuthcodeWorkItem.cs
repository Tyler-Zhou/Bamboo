using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.UI.Authcodes;

namespace ICP.Common.UI
{
    /// <summary>
    ///用户MAC地址管理
    /// </summary>
    class AuthcodeWorkItem : WorkItem
    {

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            AuthcodeMainWorkSpace mainSpce = this.SmartParts.Get<AuthcodeMainWorkSpace>("AuthcodeMainWorkSpace");
            if (mainSpce == null)
            {
                mainSpce = this.SmartParts.AddNew<AuthcodeMainWorkSpace>("AuthcodeMainWorkSpace");

                #region AddPart

                AuthcodeTool toolBar = this.SmartParts.AddNew<AuthcodeTool>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[AuthcodeWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                AuthcodeList listPart = this.SmartParts.AddNew<AuthcodeList>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[AuthcodeWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                AuthcodeSearch searchPart = this.SmartParts.AddNew<AuthcodeSearch>();
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[AuthcodeWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);


                AuthcodeEdit editPart = this.SmartParts.AddNew<AuthcodeEdit>();
                IWorkspace detailWorkspace = (IWorkspace)this.Workspaces[AuthcodeWorkSpaceConstants.EditWorkspace];
                detailWorkspace.Show(editPart);


                #endregion

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "MAC Manager" : "MAC管理";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                AuthcodeUIAdapter bookingAdapter = new AuthcodeUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(editPart.GetType().Name, editPart);

                bookingAdapter.Init(dic);
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }
    }
    /// <summary>
    /// 命常量
    /// </summary>
    public class AuthcodeCommandConstants
    {

        public const string Command_AuthcodeAdd = "Command_AuthcodeAdd";

        public const string Command_AuthcodeCancel = "Command_AuthcodeCancel";

        public const string Command_AuthcodeShowSearch = "Command_AuthcodeShowSearch";

        public const string Command_AuthcodeDelete = "Command_AuthcodeDelete";
    }
    /// <summary>
    /// WorkSpace常量
    /// </summary>
    public class AuthcodeWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";
        public const string EditWorkspace = "EditWorkspace";
    }

    /// <summary>
    /// UI适配器
    /// </summary>
    public class AuthcodeUIAdapter : IDisposable
    {

        #region parts

        IToolBar _toolBar;
        ISearchPart _searchPart;
        AuthcodeList _mainListPart;
        AuthcodeEdit _editPart;

        #endregion

        #region interface

        public void Init(Dictionary<string, object> controls)
        {
            _toolBar = (IToolBar)controls[typeof(AuthcodeTool).Name];
            _searchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(AuthcodeSearch).Name];
            _mainListPart = (AuthcodeList)controls[typeof(AuthcodeList).Name];

            _editPart = (AuthcodeEdit)controls[typeof(AuthcodeEdit).Name];

            //RefreshBarEnabled(_toolBar, null);
            #region Connection

            _editPart.Saved += delegate(object[] data)
            {
                _mainListPart.EditPartSaved(data);
            };

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                AuthcodeInfo listData = data as AuthcodeInfo;

                //RefreshBarEnabled(_toolBar, listData);

                _editPart.BindDataList(listData);

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

        private void RefreshBarEnabled(IToolBar toolBar, DataDictionaryList listData)
        {
            if (listData == null || listData.IsNew)
            {
                toolBar.SetEnable("barCancel", false);
            }
            else
            {
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
            this._editPart = null;
            this._mainListPart = null;
            this._searchPart = null;
            this._toolBar = null;
        }

        #endregion
    }
}
