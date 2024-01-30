using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI.WinForms;
using System.ComponentModel;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.Sys.UI.Role
{
    public class RoleWorkitem: WorkItem
    {
        #region Service

        public IRoleService RoleService
        {
            get
            {
                return ServiceClient.GetService<IRoleService>();
            }
        }

        #endregion

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            RoleMainWorkspace roleMainSpce = this.SmartParts.Get<RoleMainWorkspace>("RoleMainWorkspace");
            if (roleMainSpce == null)
            {
                roleMainSpce = this.SmartParts.AddNew<RoleMainWorkspace>("RoleMainWorkspace");

                #region AddPart
                RoleToolBar roleToolBar = this.SmartParts.AddNew<RoleToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[RoleWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(roleToolBar);

                RoleMainListPart roleMainListPart = this.SmartParts.AddNew<RoleMainListPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[RoleWorkSpaceConstants.ListWorkspace];
                if (listWorkspace == null) listWorkspace = this.Workspaces.AddNew<DeckWorkspace>(RoleWorkSpaceConstants.ListWorkspace);
                listWorkspace.Show(roleMainListPart);

                RoleEditPart roleEditPart = this.SmartParts.AddNew<RoleEditPart>();
                IWorkspace editWorkspace = (IWorkspace)this.Workspaces[RoleWorkSpaceConstants.EditWorkspace];
                if (editWorkspace == null) editWorkspace = this.Workspaces.AddNew<DeckWorkspace>(RoleWorkSpaceConstants.EditWorkspace);
                editWorkspace.Show(roleEditPart);

                RoleSearchPart roleSearchPart = this.SmartParts.AddNew<RoleSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[RoleWorkSpaceConstants.SearchWorkspace];
                if (searchWorkspace == null) searchWorkspace = this.Workspaces.AddNew<DeckWorkspace>(RoleWorkSpaceConstants.SearchWorkspace);
                searchWorkspace.Show(roleSearchPart);

                Role2OrgJobListPart role2OrgJobListPart = this.SmartParts.AddNew<Role2OrgJobListPart>();
                IWorkspace orgJobWorkspace = (IWorkspace)this.Workspaces[RoleWorkSpaceConstants.OrgJobWorkspace];
                if (searchWorkspace == null) orgJobWorkspace = this.Workspaces.AddNew<DeckWorkspace>(RoleWorkSpaceConstants.OrgJobWorkspace);
                orgJobWorkspace.Show(role2OrgJobListPart);

                Role2FunctionListPart role2FunctionListPart = this.SmartParts.AddNew<Role2FunctionListPart>();
                IWorkspace functionWorkspace = (IWorkspace)this.Workspaces[RoleWorkSpaceConstants.FunctionWorkspace];
                if (searchWorkspace == null) orgJobWorkspace = this.Workspaces.AddNew<DeckWorkspace>(RoleWorkSpaceConstants.FunctionWorkspace);
                functionWorkspace.Show(role2FunctionListPart);

                #endregion

                #region Connection

                #region CurrentChanging
                roleMainListPart.CurrentChanging += delegate(object sender, CancelEventArgs e)
                {
                    UIConnectionHelper.ParentChangingForEditPart(roleMainListPart
                                                                 , roleEditPart.SaveData
                                                                 , (roleEditPart.DataSource as RoleInfo)
                                                                 , e
                                                                 , LocalData.IsEnglish ? "Role Edit" : "编辑角色");

                    UIConnectionHelper.ParentChangingForBaseListPart<Role2OrganizationJobList>(role2OrgJobListPart.SaveData
                                                                                        , (role2OrgJobListPart.DataSource as BaseList<Role2OrganizationJobList>)
                                                                                        , e
                                                                                        , LocalData.IsEnglish ? "Role Job" : "角色职位 ");

                    UIConnectionHelper.ParentChangingForBaseListPart<RolePermissionList>(role2FunctionListPart.SaveData
                                                                                    , (role2FunctionListPart.DataSource as BaseList<RolePermissionList>)
                                                                                    , e
                                                                                    , LocalData.IsEnglish ? "Role Permission" : "角色权限 ");

                };
                #endregion

                #region CurrentChanged
                roleMainListPart.CurrentChanged += delegate(object sender, object data)
                {
                    RoleList listData = data as RoleList;
                   
                    Dictionary<string, object> keyValue = new Dictionary<string, object>();
                    keyValue.Add("ParentList", data);

                    #region toolBar
                    RefreshBarEnabled(roleToolBar, listData);
                    #endregion

                    #region editPart

                    RoleInfo info = null;
                    if (listData != null)
                    {
                        if (listData.IsNew)
                        {
                            info = new RoleInfo();
                            Utility.CopyToValue(listData, info, typeof(RoleInfo));
                        }
                        else
                        {
                            info = RoleService.GetRoleInfo(((RoleList)data).ID);
                        }
                    }
                    roleEditPart.DataSource = info;

                    #endregion

                    #region Role2OrganizationJob

                    List<Role2OrganizationJobList> role2OrganizationJobList = null;
                    if (listData != null && listData.IsNew ==false)
                    {
                        role2OrganizationJobList = RoleService.GetRole2OrganizationJobList(((RoleList)data).ID);
                    }
                    role2OrgJobListPart.Init(keyValue);
                    role2OrgJobListPart.DataSource = role2OrganizationJobList;


                    #endregion

                    #region Role2Function

                    List<RolePermissionList> rolePermissionList = null;
                    if (listData != null && listData.IsNew == false)
                    {
                        rolePermissionList = RoleService.GetRolePermissionListByRoleID(listData.ID);
                    }
                    role2FunctionListPart.Init(keyValue);
                    role2FunctionListPart.DataSource = rolePermissionList;


                    #endregion
                };
                #endregion

                #region Saved
                roleEditPart.Saved += delegate(object[] prams)
                {
                    if (roleMainListPart.Current == null || prams == null) return;

                    RoleList rolelist = prams[0] as RoleList;
                    RoleList currentRow = roleMainListPart.Current as RoleList;

                    Utility.CopyToValue(rolelist, currentRow, typeof(RoleList));


                    Dictionary<string, object> keyValue = new Dictionary<string, object>();
                    keyValue.Add("ParentList", currentRow);
                    role2OrgJobListPart.Init(keyValue);
                    role2FunctionListPart.Init(keyValue);
                    if (currentRow.IsNew)
                    {
                        role2OrgJobListPart.DataSource = new  List<Role2OrganizationJobList>();
                        role2FunctionListPart.DataSource = new List<RolePermissionList>();
                    }

                    RefreshBarEnabled(roleToolBar, currentRow);
                };
                #endregion

                #region OnSearched
                roleSearchPart.OnSearched += delegate(object sender, object results)
                {
                    roleMainListPart.DataSource = results;
                };
                #endregion

                #endregion


                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Role Manage" : "角色管理";
                mainWorkspace.Show(roleMainSpce, smartPartInfo);

                roleSearchPart.RaiseSearched();
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(roleMainSpce);
            }
        }

        private void RefreshBarEnabled(IToolBar toolBar, RoleList listData)
        {
            if (listData == null || listData.IsNew)
            {
                toolBar.SetEnable("barDisuse", false);
            }
            else
            {
                toolBar.SetEnable("barDisuse", true);
                if (listData.IsValid)
                    toolBar.SetText("barDisuse", LocalData.IsEnglish ? "Disuse(&D)" : "作废(&D)");
                else
                    toolBar.SetText("barDisuse", LocalData.IsEnglish ? "Available(&D)" : "激活(&D)");

            }
        }
    }

    public class RoleWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolBarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string EditWorkspace = "EditWorkspace";
        public const string ListWorkspace = "ListWorkspace";
        public const string OrgJobWorkspace = "OrgJobWorkspace";
        public const string FunctionWorkspace = "FunctionWorkspace";
    }

    public class RoleCommonConstants
    {
        public const string Common_AddData = "Common_AddData";
        public const string Common_DisuseData = "Common_DisuseData";
        public const string Command_ShowSearch = "Command_ShowSearch";

    }
}
