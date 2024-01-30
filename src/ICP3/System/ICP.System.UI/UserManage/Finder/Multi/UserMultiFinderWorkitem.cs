using System;
using System.Collections.Generic;
using System.Linq;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Sys.ServiceInterface.DataObjects;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.Sys.UI.UserManage.Finder
{

    public class UserMultiFinderWorkitem : WorkItem
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

            }
            base.Dispose(disposing);
        }
        public event EventHandler<DataFindEventArgs> DataChoosed;
        UserFinderSearchPart _searchPart = null;

        public void Show(IWorkspace mainWorkspace, List<UserList> list, List<UserList> existList, string[] returnFields, Dictionary<string, object> initValues)
        {

            if (mainWorkspace == null)
                mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];

            UserMultiFinderWorkspace userMainSpce = this.SmartParts.Get<UserMultiFinderWorkspace>("UserMultiFinderWorkspace");
            if (userMainSpce == null)
            {
                userMainSpce = this.SmartParts.AddNew<UserMultiFinderWorkspace>("UserMultiFinderWorkspace");

                #region AddPart

                UserMultiFinderToolBar toolBar = this.SmartParts.AddNew<UserMultiFinderToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[UserWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                UserMultiMainListPart listPart = this.SmartParts.AddNew<UserMultiMainListPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[UserWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                UserEditPart editPart = this.SmartParts.AddNew<UserEditPart>();
                IWorkspace editWorkspace = (IWorkspace)this.Workspaces[UserWorkSpaceConstants.EditWorkspace];
                editWorkspace.Show(editPart);

                MultiFinderSelectedToolBar selectedToolBar = this.SmartParts.AddNew<MultiFinderSelectedToolBar>();
                IWorkspace selectedToolBarWorkspace = (IWorkspace)this.Workspaces[UserWorkSpaceConstants.SelectedToolBarWorkspace];
                selectedToolBarWorkspace.Show(selectedToolBar);

                UserMultiSelectedListPart selectedListPart = this.SmartParts.AddNew<UserMultiSelectedListPart>();
                IWorkspace selectedListWorkspace = (IWorkspace)this.Workspaces[UserWorkSpaceConstants.SelectedListWorkspace];
                selectedListWorkspace.Show(selectedListPart);

                _searchPart = this.SmartParts.AddNew<UserFinderSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[UserWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(_searchPart);

                #endregion

                BulidConnection(toolBar, editPart, _searchPart, listPart, selectedListPart, selectedToolBar, returnFields);
                listPart.DataSource = list;
                selectedListPart.DataSource = existList;

                string title=LocalData.IsEnglish ? "User Finder" : "查找用户";

                if (initValues != null && initValues.Keys.Contains("FormTitle"))
                {
                    if (initValues["FormTitle"] != null)
                    {
                        title = initValues["FormTitle"].ToString();
                    }
                }

                SmartPartInfo smartPartInfo = new SmartPartInfo();
                
                smartPartInfo.Title = title;
                mainWorkspace.Show(userMainSpce, smartPartInfo);
            }
            else
            {
                mainWorkspace.Activate(userMainSpce);
            }

            _searchPart.Init(initValues);

        }

        private void BulidConnection(BaseToolBar toolBar
                             , BaseEditPart editPart
                             , BaseSearchPart searchPart
                             , BaseListPart listPart
                             , BaseListPart selectedListPart
                             , BaseToolBar selectedToolBar
                             , string[] returnFields)
        {
            listPart.CurrentChanging += delegate(object sender, CancelEventArgs e)
            {
                UIConnectionHelper.ParentChangingForEditPart(listPart
                                                                 , editPart.SaveData
                                                                 , (editPart.DataSource as UserInfo)
                                                                 , e
                                                                 , LocalData.IsEnglish ? "User Edit" : "编辑用户");
            };

            listPart.Selected += delegate(object sender, object data)
            {
                List<UserList> newSelectedList = data as List<UserList>;
                if (newSelectedList == null || newSelectedList.Count ==0) return;

                List<UserList> selcted = selectedListPart.DataSource as List<UserList>;
                if (selcted == null) selcted = new List<UserList>();

                List<UserList> needAddList = new List<UserList>();
                foreach (var item in newSelectedList)
                {
                    UserList tager = selcted.Find(delegate(UserList uItem) { return uItem.ID == item.ID; });
                    if (tager != null) continue;
                    needAddList.Add(item);
                }

                selcted.AddRange(needAddList);
                selectedListPart.DataSource = selcted;

            };

            listPart.CurrentChanged += delegate(object sender, object data)
            {
                #region CurrentChanged

                UserList listData = data as UserList;
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add("ParentList", data);

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

            selectedListPart.Selected += delegate(object sender, object data)
            {
                List<UserList> newSelectedList = data as List<UserList>;
                if (newSelectedList == null) newSelectedList = new List<UserList>(); 

                if(this.DataChoosed !=null)
                    this.DataChoosed(sender, new DataFindEventArgs(Utility.GetMultiSearchResult<UserList>(newSelectedList, returnFields)));

            };

            selectedListPart.CurrentChanged += delegate(object sender, object data)
            {
                #region CurrentChanged

                UserList listData = data as UserList;

                if (listData == null)
                {
                    selectedToolBar.SetEnable("barRemove", false);
                    selectedToolBar.SetEnable("barRemoveAll", false);
                }
                else
                {
                    selectedToolBar.SetEnable("barRemove", true);
                    selectedToolBar.SetEnable("barRemoveAll", true);
                }

                #endregion
            };
            
            editPart.Saved += delegate(object[] prams)
            {
                #region editPart Saved
                if (listPart.Current == null || prams == null) return;

                UserList userlist = prams[0] as UserList;
                UserList currentRow = listPart.Current as UserList;

                Utility.CopyToValue(userlist, currentRow, typeof(UserList));

                #endregion
            };

            searchPart.OnSearched += delegate(object sender, object results)
            {
                listPart.DataSource = results;
            };
        }

    }
}
