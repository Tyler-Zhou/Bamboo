using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Common.ServiceInterface.DataObjects;

namespace ICP.Common.UI
{
        /// <summary>
        /// MovieProjectWorkItem
        /// </summary>
        class MovieProjectWorkItem : WorkItem
        {

            protected override void OnRunStarted()
            {
                base.OnRunStarted();
                Show();
            }

            private void Show()
            {
                MovieProjectMainWorkSpace mainSpce = this.SmartParts.Get<MovieProjectMainWorkSpace>("MovieProjectMainWorkSpace");
                if (mainSpce == null)
                {
                    mainSpce = this.SmartParts.AddNew<MovieProjectMainWorkSpace>("MovieProjectMainWorkSpace");

                    #region AddPart

                    MovieProjectTool toolBar = this.SmartParts.AddNew<MovieProjectTool>();
                    IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[MovieProjectWorkSpaceConstants.ToolBarWorkspace];
                    toolBarWorkspace.Show(toolBar);

                    MovieProjectList listPart = this.SmartParts.AddNew<MovieProjectList>();
                    IWorkspace listWorkspace = (IWorkspace)this.Workspaces[MovieProjectWorkSpaceConstants.ListWorkspace];
                    listWorkspace.Show(listPart);

                    MovieProjectSearch searchPart = this.SmartParts.AddNew<MovieProjectSearch>();
                    IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[MovieProjectWorkSpaceConstants.SearchWorkspace];
                    searchWorkspace.Show(searchPart);


                    MovieProjectEdit editPart = this.SmartParts.AddNew<MovieProjectEdit>();
                    IWorkspace detailWorkspace = (IWorkspace)this.Workspaces[MovieProjectWorkSpaceConstants.EditWorkspace];
                    detailWorkspace.Show(editPart);


                    #endregion

                    IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                    SmartPartInfo smartPartInfo = new SmartPartInfo();
                    smartPartInfo.Title = LocalData.IsEnglish ? "Movie Project" : "项目列表";
                    mainWorkspace.Show(mainSpce, smartPartInfo);


                    MovieProjectUIAdapter bookingAdapter = new MovieProjectUIAdapter();
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
        public class MovieProjectCommandConstants
        {

            public const string Command_MovieProjectAdd = "Command_MovieProjectPartAdd";

            public const string Command_MovieProjectCancel = "Command_MovieProjectCancel";

            public const string Command_MovieProjectShowSearch = "Command_MovieProjectShowSearch";


        }
        /// <summary>
        /// WorkSpace常量
        /// </summary>
        public class MovieProjectWorkSpaceConstants
        {
            public const string ToolBarWorkspace = "ToolbarWorkspace";
            public const string SearchWorkspace = "SearchWorkspace";
            public const string ListWorkspace = "ListWorkspace";
            public const string EditWorkspace = "EditWorkspace";
        }

        /// <summary>
        /// UI适配器
        /// </summary>
        public class MovieProjectUIAdapter : IDisposable
        {

            #region parts

            IToolBar _toolBar;
            ISearchPart _searchPart;
            MovieProjectList _mainListPart;
            MovieProjectEdit _editPart;

            #endregion

            #region interface

            public void Init(Dictionary<string, object> controls)
            {
                _toolBar = (IToolBar)controls[typeof(MovieProjectTool).Name];
                _searchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(MovieProjectSearch).Name];
                _mainListPart = (MovieProjectList)controls[typeof(MovieProjectList).Name];

                _editPart = (MovieProjectEdit)controls[typeof(MovieProjectEdit).Name];

                RefreshBarEnabled(_toolBar, null);
                #region Connection

                _editPart.Saved += delegate(object[] data)
                {
                    _mainListPart.EditPartSaved(data);
                };

                #region _mainListPart.CurrentChanged
                _mainListPart.CurrentChanged += delegate(object sender, object data)
                {
                    DataDictionaryList listData = data as DataDictionaryList;

                    RefreshBarEnabled(_toolBar, listData);

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
