using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.SmartParts;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Common;
using System.Windows.Forms;

namespace ICP.Sys.UI.UserManage
{
    public class UserWorkitem:WorkItem
    {
        #region Service

        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }
        public IPermissionService PermissionService
        {
            get
            {
                return ServiceClient.GetService<IPermissionService>();
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
            bool editUser=ICP.Framework.CommonLibrary.Client.LocalCommonServices.PermissionService.HaveActionPermission("EDITUSERINFO");

            UserMainWorkspace userMainSpce = this.SmartParts.Get<UserMainWorkspace>("UserMainWorkspace");
            if (userMainSpce == null)
            {
                userMainSpce = this.SmartParts.AddNew<UserMainWorkspace>("UserMainWorkspace");

                #region AddPart

                UserToolBar userToolBar = this.SmartParts.AddNew<UserToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[UserWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(userToolBar);

                UserMainListPart userMainListPart = this.SmartParts.AddNew<UserMainListPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[UserWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(userMainListPart);

                UserEditPart userEditPart = this.SmartParts.AddNew<UserEditPart>();
                userEditPart.Init(editUser);
                IWorkspace editWorkspace = (IWorkspace)this.Workspaces[UserWorkSpaceConstants.EditWorkspace];
                editWorkspace.Show(userEditPart);

                UserSearchPart userSearchPart = this.SmartParts.AddNew<UserSearchPart>();
                if (!editUser)
                {
                    userSearchPart.Init(LocalData.UserInfo.LoginName);
                }
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[UserWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(userSearchPart);

                User2OrgJobListPart user2OrgJobListPart = this.SmartParts.AddNew<User2OrgJobListPart>();
                user2OrgJobListPart.Enabled = editUser;
                IWorkspace orgJobWorkspace = (IWorkspace)this.Workspaces[UserWorkSpaceConstants.OrgJobWorkspace];
                orgJobWorkspace.Show(user2OrgJobListPart);

                User2FunctionListPart user2FunctionListPart = this.SmartParts.AddNew<User2FunctionListPart>();
                userMainSpce.SetOrgJobEnabled(editUser);
                IWorkspace functionWorkspace = (IWorkspace)this.Workspaces[UserWorkSpaceConstants.FunctionWorkspace];
                functionWorkspace.Show(user2FunctionListPart);

                #region Mail

                UserMailAccount.UserMailToolBar mailToolBar = this.SmartParts.AddNew<UserMailAccount.UserMailToolBar>();
                IWorkspace mailToolBarWorkspace = (IWorkspace)this.Workspaces[UserWorkSpaceConstants.MailToolBarWorkspace];
                mailToolBarWorkspace.Show(mailToolBar);


                UserMailAccount.UserMailListPart mailListPart = this.SmartParts.AddNew<UserMailAccount.UserMailListPart>();
                IWorkspace mailListWorkspace = (IWorkspace)this.Workspaces[UserWorkSpaceConstants.MailListWorkspace];
                mailListWorkspace.Show(mailListPart);

                UserMailAccount.UserMailEditPart mailEditPart = this.SmartParts.AddNew<UserMailAccount.UserMailEditPart>();
                IWorkspace mailEditWorkspace = (IWorkspace)this.Workspaces[UserWorkSpaceConstants.MailEditWorkspace];
                mailEditWorkspace.Show(mailEditPart);
                #endregion

                #endregion

                #region Connection

                #region CurrentChanging
                userMainListPart.CurrentChanging += delegate(object sender, CancelEventArgs e)
                {
                    UIConnectionHelper.ParentChangingForEditPart(userMainListPart
                                                                  , userEditPart.SaveData
                                                                  , (userEditPart.DataSource as UserInfo)
                                                                  , e
                                                                  , LocalData.IsEnglish ? "User Edit" : "编辑用户");

                    UIConnectionHelper.ParentChangingForEditPart(userMainListPart
                                                              , mailEditPart.SaveData
                                                              , (mailEditPart.DataSource as UserMailAccountList)
                                                              , e
                                                              , LocalData.IsEnglish ? "User MailAccount" : "用户邮件"
                                                              ,false);

                    #region 用户职位
                    BaseList<User2OrganizationJobList> org2JobSource = user2OrgJobListPart.DataSource as BaseList<User2OrganizationJobList>;

                    if (org2JobSource != null && org2JobSource.Count>0)
                    {
                        if (org2JobSource.IsDirty)
                        {
                            DialogResult dlg = Utility.EnquireIsSaveCurrentDataByUpdated(LocalData.IsEnglish ? "User Job" : "用户职位 ");
                            if (dlg == DialogResult.Yes) e.Cancel = !user2OrgJobListPart.SaveData();
                            else if (dlg == DialogResult.Cancel) e.Cancel = true;
                            else if (dlg == DialogResult.No) { return; }
                        }
                        else
                        {
                            bool hasDefault =false;
                            foreach (var item in org2JobSource)
                            {
                                if (item.IsDefault) { hasDefault = true; break; }
                            }
                            if (hasDefault == false)
                            {
                                e.Cancel = true;

                                DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Plase set a Default Job." : "请设置一个默认职位."
                                , LocalData.IsEnglish ? "User Job" : "用户职位 "
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Warning);

                            }
                        }

                    }
                    #endregion
                };
                #endregion

                #region CurrentChanged
                userMainListPart.CurrentChanged += delegate(object sender, object data)
                {
                    UserList listData = data as UserList;
                    Dictionary<string, object> keyValue = new Dictionary<string, object>();
                    keyValue.Add("ParentList", data);

                    #region toolBar

                    RefreshBarEnabled(userToolBar, listData);

                    #endregion

                    #region editPart

                    UserInfo info = null;
                    if (listData != null)
                    {
                        if (listData.IsNew)
                        {
                            info = new UserInfo();
                            Utility.CopyToValue(listData, info, typeof(UserInfo));
                        }
                        else
                        {
                            info = UserService.GetUserInfo(((UserList)data).ID);
                        }
                    }
                    userEditPart.DataSource = info;
                    #endregion

                    #region User2OrganizationJob

                    List<User2OrganizationJobList> user2OrganizationJobList = null;
                    if (listData!=null && listData.IsNew ==false)
                    {
                        user2OrganizationJobList = UserService.GetUser2OrganizationJobList(((UserList)data).ID);
                    }
                    user2OrgJobListPart.Init(keyValue);
                    user2OrgJobListPart.DataSource = user2OrganizationJobList;

                    #endregion

                    #region Function

                    List<FunctionList> user2FunctionList = null;
                    if (listData != null && listData.IsNew == false)
                    {
                        user2FunctionList = PermissionService.GetUserFunctionList(((UserList)data).ID,true);
                    }
                    user2FunctionListPart.Init(keyValue);
                    user2FunctionListPart.DataSource = user2FunctionList;

                    #endregion

                    #region Mail

                    List<UserMailAccountList> userMailAccountList = null;
                    if (listData != null && listData.IsNew == false)
                    {
                        userMailAccountList = UserService.GetUserMailAccountList(new Guid[] { ((UserList)data).ID });
                    }

                    if (info == null)
                    {
                        mailToolBar.SetEnable("barNew", false);
                        mailToolBar.SetEnable("barSave", false);
                        mailToolBar.SetEnable("barRemove", false);
                        mailToolBar.SetEnable("barDefault", false);
                    }
                    else
                    {
                        mailToolBar.SetEnable("barNew", true);
                    }

                    mailListPart.Init(keyValue);
                    mailEditPart.Init(keyValue);
                    mailListPart.DataSource = userMailAccountList;

                    #endregion
                };
                #endregion

                #region Saved
                userEditPart.Saved += delegate(object[] prams)
                {
                    if (userMainListPart.Current == null || prams == null) return;

                    UserList userlist = prams[0] as UserList;
                    UserList currentRow = userMainListPart.Current as UserList;

                    Utility.CopyToValue(userlist, currentRow, typeof(UserList));
                    Dictionary<string, object> keyValue = new Dictionary<string, object>();
                    keyValue.Add("ParentList", currentRow);
                    user2OrgJobListPart.Init(keyValue);
                    if (currentRow.IsNew)
                    {
                        user2OrgJobListPart.DataSource = new List<User2OrganizationJobList>();
                    }
                    RefreshBarEnabled(userToolBar, currentRow);
                };
                #endregion

                #region OnSearched
                userSearchPart.OnSearched += delegate(object sender, object results)
                {
                    userMainListPart.DataSource = results;
                };
                #endregion

                #endregion

                #region MailConnection

                mailListPart.CurrentChanged += delegate(object sender, object data)
                {
                    UserMailAccountList info = (UserMailAccountList)data;
                    mailEditPart.DataSource = info;

                    if (info == null)
                    {
                        mailToolBar.SetEnable("barSave", false);
                        mailToolBar.SetEnable("barRemove", false);
                        mailToolBar.SetEnable("barDefault", false);
                    }
                    else
                    {
                        mailToolBar.SetEnable("barSave", true);
                        if (info.IsNew == false && info.IsValid)
                        {
                            mailToolBar.SetEnable("barRemove", true);
                            mailToolBar.SetEnable("barDefault", true);
                        }
                    }
                };

                mailEditPart.Saved += delegate(object[] prams)
                {
                    if (mailListPart.Current == null || prams == null) return;

                    UserMailAccountList list = prams[0] as UserMailAccountList;
                    UserMailAccountList currentRow = mailListPart.Current as UserMailAccountList;

                    Utility.CopyToValue(list, currentRow, typeof(UserMailAccountList));

                    if (list == null)
                    {
                        mailToolBar.SetEnable("barSave", false);
                        mailToolBar.SetEnable("barRemove", false);
                        mailToolBar.SetEnable("barDefault", false);
                    }
                    else
                    {
                        mailToolBar.SetEnable("barSave", true);
                        if (list.IsNew == false && list.IsValid)
                        {
                            mailToolBar.SetEnable("barRemove", true);
                            mailToolBar.SetEnable("barDefault", true);
                        }
                    }

                };

                #endregion

             
                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "User Manage" : "用户管理";
                mainWorkspace.Show(userMainSpce, smartPartInfo);

                userSearchPart.RaiseSearched();
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(userMainSpce);
            }
        }

        private void RefreshBarEnabled(IToolBar toolBar, UserList listData)
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

    public class UserWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolBarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string EditWorkspace = "EditWorkspace";
        public const string ListWorkspace = "ListWorkspace";
        public const string FunctionWorkspace = "FunctionWorkspace";
        

        public const string OrgJobWorkspace = "OrgJobWorkspace";
        public const string MailAccountWorkspace = "MailAccountWorkspace";
        public const string SelectedListWorkspace = "SelectedListWorkspace";
        public const string SelectedToolBarWorkspace = "SelectedToolBarWorkspace";

        public const string MailToolBarWorkspace = "MailToolBarWorkspace";
        public const string MailEditWorkspace = "MailEditWorkspace";
        public const string MailListWorkspace = "MailListWorkspace";

        public const string OrganizationListWorkspace = "OrganizationListWorkspace";
       

    }

    public class UserCommonConstants
    {
        public const string Common_AddData = "Common_AddData";
        public const string Common_DisuseData = "Common_DisuseData";
        public const string Command_ShowSearch = "Command_ShowSearch";

        public const string Common_MailAddData = "Common_MailAddData";
        public const string Common_MailDeleteData = "Common_MailDeleteData";
        public const string Command_MailSetDefault = "Command_MailSetDefault";
        public const string Command_MailSaveData = "Command_MailSaveData";

        public const string Common_FinderConfirm = "Common_FinderConfirm";
        public const string Common_FindeSelect = "Common_FindeSelect";
        public const string Common_FinderRemove = "Common_FinderRemove";
        public const string Common_FinderRemoveAll = "Common_FinderRemoveAll";

    }
}
