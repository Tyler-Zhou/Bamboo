using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Common.ServiceInterface.DataObjects;
using System;

namespace ICP.FAM.UI
{
    class GLCodeWorkitem : WorkItem
    {
        
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            GLCodeMainWorkSpace mainSpce = SmartParts.Get<GLCodeMainWorkSpace>("GLCodeMainWorkSpace");
            if (mainSpce == null)
            {
                mainSpce = SmartParts.AddNew<GLCodeMainWorkSpace>("GLCodeMainWorkSpace");

                #region AddPart

                GLCodeToolPart toolBar = SmartParts.AddNew<GLCodeToolPart>();
                IWorkspace toolBarWorkspace = (IWorkspace)Workspaces[GLCodeWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                GLCodeListPart listPart = SmartParts.AddNew<GLCodeListPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[GLCodeWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                GLCodeSearhcPart searchPart = SmartParts.AddNew<GLCodeSearhcPart>();
                IWorkspace searchWorkspace = (IWorkspace)Workspaces[GLCodeWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);

                #endregion

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "GLCode" : "会计科目";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                GLCodeUIAdapter glCodeAdapter = new GLCodeUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);

                glCodeAdapter.Init(dic);

                searchPart.RaiseSearched();
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
    public class GLCodeCommandConstants
    {
        public const string Command_GLCodeAdd = "Command_GLCodeAdd";

        public const string Command_GLCodeEdit = "Command_GLCodeEdit";

        public const string Command_GLCodeCancel = "Command_GLCodeCancel";

        public const string Command_GLCodeRefreshData = "Command_GLCodeRefreshData";

        public const string Command_GLCodeShowSearch = "Command_GLCodeShowSearch";

        public const string Command_GLCodeTo = "Command_GLCodeTo";

        public const string Command_GLCompany = "Command_GLCompany";

        public const string Command_FinderConfirm = "Command_FinderConfirm";
        public const string Command_FinderSelect = "Command_FinderSelect";
        public const string Command_FinderRemove = "Commond_FinderRemove";
        public const string Command_FinderRemoveAll = "Commond_FinderRemoveAll";
    }
    /// <summary>
    /// WorkSpace常量
    /// </summary>
    public class GLCodeWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolBarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";
        public const string SelectedListWorkspace = "SelectedListWorkspace";
        public const string SelectedToolBarWorkspace = "SelectedToolBarWorkspace";
        public const string EditWorkspace = "EditWorkspace";

    }

    /// <summary>
    /// UI适配器
    /// </summary>
    public class GLCodeUIAdapter : IDisposable
    {

        #region parts

        IToolBar _toolBar;
        ISearchPart _searchPart;
        GLCodeListPart _mainListPart;

        #endregion

        /// <summary>
        /// 是否有权限编辑会计科目
        /// </summary>
        private string COMEDITGLCode = "COMMON_EDITGLCode";

        #region interface

        public void Init(Dictionary<string, object> controls)
        {
            _toolBar = (IToolBar)controls[typeof(GLCodeToolPart).Name];
            _searchPart = (ISearchPart)controls[typeof(GLCodeSearhcPart).Name];
            _mainListPart = (GLCodeListPart)controls[typeof(GLCodeListPart).Name];
            RefreshBarEnabled(_toolBar, null);
            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                SolutionGLCodeList listData = data as SolutionGLCodeList;

                RefreshBarEnabled(_toolBar, listData);

            };

            #endregion

            #region _searchPart.OnSearched
            _searchPart.OnSearched += delegate(object sender, object results)
            {
                _mainListPart.DataSource = results;
                _mainListPart.CompanyIDs = (sender as GLCodeSearhcPart).CompanyIDs;
            };
            #endregion

            #endregion
        }
        private void RefreshBarEnabled(IToolBar toolBar, SolutionGLCodeList listData)
        {
            if (listData == null)
            {
                toolBar.SetEnable("barEdit", false);
                toolBar.SetEnable("barInvalid", false);
            }
            else
            {
                if (LocalCommonServices.PermissionService.HaveActionPermission(COMEDITGLCode))
                {
                    toolBar.SetEnable("barEdit", true);
                    toolBar.SetEnable("barInvalid", true);

                    if (listData.IsValid)
                    {
                        toolBar.SetText("barInvalid", LocalData.IsEnglish ? "Invalid" : "作废");
                    }
                    else
                    {
                        toolBar.SetText("barInvalid", LocalData.IsEnglish ? "Activation" : "激活");
                    }
                }
                else 
                {
                    toolBar.SetEnable("barEdit", false);
                    toolBar.SetEnable("barInvalid", false);
                    toolBar.SetEnable("barAdd", false);
                    
                }
                        
            }     
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            _mainListPart = null;
            _searchPart = null;
            _toolBar = null;
        }

        #endregion
    }

}
