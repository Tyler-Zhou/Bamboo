using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.Sys.UI.UserManage.MiniFinder
{
    /// <summary>
    /// UMFS= UserMiniFinderSingle
    /// </summary>
    public class UMFS_Workitem :WorkItem
    {

        #region Service

        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }
        public IOrganizationService OrganizationService
        {
            get
            {
                return ServiceClient.GetService<IOrganizationService>();
            }
        }

        #endregion
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.DataChoosed = null;
                this._searchPart = null;
                this._UMFS_OrganizationListPart = null;
            }
            base.Dispose(disposing);
        }
        UMFS_MainListPart _listPart = null;

        public event EventHandler<DataFindEventArgs> DataChoosed;
        UserFinderSearchPart _searchPart = null;
        UMFS_OrganizationListPart _UMFS_OrganizationListPart = null;
        public void Show(IWorkspace mainWorkspace, string[] returnFields)
        {
            if (mainWorkspace == null)
                mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];

            UMFS_MainWorkspace userMainSpce = this.Items.Get<UMFS_MainWorkspace>("UMFS_MainWorkspace");
            if (userMainSpce == null)
            {
                userMainSpce = this.Items.AddNew<UMFS_MainWorkspace>("UMFS_MainWorkspace");

                #region AddPart

                _listPart = this.Items.AddNew<UMFS_MainListPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[UserWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(_listPart);

                _UMFS_OrganizationListPart = this.Items.AddNew<UMFS_OrganizationListPart>();
                IWorkspace organizationListWorkspace = (IWorkspace)this.Workspaces[UserWorkSpaceConstants.OrganizationListWorkspace];
                organizationListWorkspace.Show(_UMFS_OrganizationListPart);

                #endregion
                _UMFS_OrganizationListPart.DataSource = OrganizationService.GetOrganizationList(string.Empty, string.Empty, true, 0);

                List<UserList> list = UserService.GetUserListByList(string.Empty, string.Empty, null, null, null, null,true, null, 0);

                Dictionary<string, object> initValues = new Dictionary<string, object>();
                initValues.Add("InitSource", list);
                _listPart.Init(initValues);
                _listPart.DataSource = list;

                BulidConnection(_listPart,_UMFS_OrganizationListPart,list ,returnFields);

                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "User Finder" : "查找用户";
                mainWorkspace.Show(userMainSpce, smartPartInfo);
            }
            else
            {
                mainWorkspace.Activate(userMainSpce);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        public void ResetCondition(IDictionary<string, object> values)
        {
            if (_listPart != null) _listPart.Clear();
        }

        private void BulidConnection(BaseListPart listPart
                                     , BaseListPart orgListPart
                                     ,List<UserList> list
                                     , string[] returnFields)
        {

            

            orgListPart.CurrentChanged += delegate(object sender, object data)
            {
                Dictionary<string, object> initValues = new Dictionary<string, object>();
                List<UserList> userLists = new List<UserList>();

                OrganizationList org = data as OrganizationList;
                if (org == null || Utility.GuidIsNullOrEmpty(org.ParentID))
                {
                    userLists = list;
                }
                else
                {
                    userLists = UserService.GetUserListBySearch(org.ID, string.Empty, string.Empty, true, null, 0);
                }
                initValues.Add("InitSource", userLists);
                _listPart.Init(initValues);
                _listPart.DataSource = userLists;

            };

            listPart.Selected += delegate(object sender, object data)
            {
                UserList userData = data as UserList;
                if (userData == null) return;
                if (DataChoosed != null)
                {
                    DataChoosed(sender, new DataFindEventArgs(Utility.GetSingleSearchResult<UserList>(userData, returnFields)));
                }
            };
        }

    }

}
