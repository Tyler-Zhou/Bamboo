using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Sys.ServiceInterface.DataObjects;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.Sys.UI.UserManage.Finder
{
    [ToolboxItem(false)]
    public class UserSingleFinderWorkitem : WorkItem
    {
        #region Service

        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        #endregion
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.DataChoosed = null;
                this._searchPart = null;
                this._listPart = null;
            }
            base.Dispose(disposing);
        }
        public event EventHandler<DataFindEventArgs> DataChoosed;
        UserFinderSearchPart _searchPart = null;
        UserSingleMainListPart _listPart = null;

        public void Show(IWorkspace mainWorkspace, List<UserList> list, string[] returnFields, Dictionary<string, object> initValues)
        {
            if (mainWorkspace == null)
                mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];

            UserSingleFinderWorkspace userMainSpce = this.SmartParts.Get<UserSingleFinderWorkspace>("UserSingleFinderWorkspace");
            if (userMainSpce == null)
            {
                userMainSpce = this.SmartParts.AddNew<UserSingleFinderWorkspace>("UserSingleFinderWorkspace");

                #region AddPart

                UserSingleFinderToolBar toolBar = this.SmartParts.AddNew<UserSingleFinderToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[UserWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                _listPart = this.SmartParts.AddNew<UserSingleMainListPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[UserWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(_listPart);

                UserEditPart editPart = this.SmartParts.AddNew<UserEditPart>();
                IWorkspace editWorkspace = (IWorkspace)this.Workspaces[UserWorkSpaceConstants.EditWorkspace];
                editWorkspace.Show(editPart);

                _searchPart = this.SmartParts.AddNew<UserFinderSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[UserWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(_searchPart);

                #endregion

                BulidConnection(toolBar, editPart, _searchPart, _listPart, returnFields);
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "User Finder" : "查找用户";
                mainWorkspace.Show(userMainSpce, smartPartInfo);
            }
            else
            {
                mainWorkspace.Activate(userMainSpce);
            }
            if (list != null) _listPart.DataSource = list;
            _searchPart.Init(initValues);
        }

        private void BulidConnection(BaseToolBar toolBar
                                     , BaseEditPart editPart
                                     , BaseSearchPart searchPart
                                     , BaseListPart listPart
                                     , string[] returnFields)
        {
            listPart.CurrentChanging += delegate(object sender, CancelEventArgs e)
            {
                UIConnectionHelper.ParentChangingForEditPart(listPart
                                                                , editPart.SaveData
                                                                , (editPart.DataSource as UserInfo)
                                                                , e
                                                                , LocalData.IsEnglish ? "User Edit" : "编辑用户"); ;
            };

            listPart.Selected += delegate(object sender, object data)
            {
                UserList list = data as UserList;
                if (list == null) return;
                if (DataChoosed != null)
                {
                     DataChoosed(sender, new DataFindEventArgs(Utility.GetSingleSearchResult<UserList>(list, returnFields)));
                }
            };

            listPart.CurrentChanged += delegate(object sender, object data)
            {
                #region CurrentChanged

                UserList listData = data as UserList;

                #region editPart

                UserInfo info = null;
                if (listData != null)
                {
                    if (listData.IsNew)
                    {
                        info = new UserInfo();
                        info.CreateById = LocalData.UserInfo.LoginID;
                        info.CreateBy = LocalData.UserInfo.LoginName;
                        info.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
                        info.IsValid = true;
                        info.IsDirty = false;
                    }
                    else
                    {
                        info = UserService.GetUserInfo(((UserList)data).ID);
                    }
                }
                editPart.DataSource = info;
                #endregion

                #endregion
            };

            editPart.Saved += delegate(object[] prams)
            {
                if (listPart.Current == null || prams == null) return;

                UserList userlist = prams[0] as UserList;
                UserList currentRow = listPart.Current as UserList;

                Utility.CopyToValue(userlist, currentRow, typeof(UserList));
            };

            searchPart.OnSearched += delegate(object sender, object results)
            {
                listPart.DataSource = results;
            };
        }

    }
}
