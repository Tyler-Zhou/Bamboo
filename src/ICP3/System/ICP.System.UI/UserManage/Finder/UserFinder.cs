using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.WinForms;
using System.Collections;
using System.Text.RegularExpressions;

namespace ICP.Sys.UI.UserManage.Finder
{
    public class UserFinder : IDataFinder, IDisposable
    {
        /// <summary>
        /// IDataFinder 成员
        /// </summary>
        public bool IsBusy { get; set; }

        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        #endregion

        private const string UserFinderWorkspace = "UserFinderWorkspace";



        #region ChildWorkitem

        UserSingleFinderWorkitem singleFinderWorkitem = null;
        UserMultiFinderWorkitem multiFinderWorkitem = null;

        #endregion

        #region IDataFinder 成员

        public event EventHandler<DataFindEventArgs> DataChoosed;

        #region One

        public void PickOne(string searchValue, string property, SearchConditionCollection conditions, string[] returnFields, FinderTriggerType triggerType, System.Windows.Forms.Control container)
        {
            DeckWorkspace workspce = this.Workitem.Workspaces.Get<DeckWorkspace>(UserFinderWorkspace);
            if (workspce == null || workspce.IsDisposed)
            {
                if (this.Workitem.Workspaces.Contains(UserFinderWorkspace))
                {
                    this.Workitem.Workspaces.Remove(workspce);
                }

                workspce = this.Workitem.Workspaces.AddNew<DeckWorkspace>(UserFinderWorkspace);
                workspce.Dock = System.Windows.Forms.DockStyle.Fill;
                workspce.BringToFront();
            }
            container.Controls.Clear();
            container.Controls.Add(workspce);
            PickOne(searchValue, property, conditions, returnFields, triggerType, UserFinderWorkspace);
        }
        /// <summary>
        /// 判断是否为中文
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool IsValidChinese(string str)
        {
            return Regex.IsMatch(str, "^[\u4e00-\u9fbb]");
        }

        public void PickOne(string searchValue, string property, SearchConditionCollection conditions, string[] returnFields, FinderTriggerType triggerType, string workspaceName)
        {
            if (string.IsNullOrEmpty(workspaceName))
                workspaceName = ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace;

            IWorkspace workspace = this.Workitem.Workspaces[workspaceName];

            #region

            string name, code;
            name = code = string.Empty;
            if (triggerType == FinderTriggerType.KeyEnter)
            {
                if (IsValidChinese(searchValue))
                {
                    name = searchValue;
                }
                else
                {
                    code = searchValue;
                }
                //if (property.Contains(SearchFieldConstants.Code))
                //    code = searchValue;
                //else
                //    name = searchValue;
            }

            List<UserList> list = UserService.GetUserListByList(code, name, null, null, null, null, true, true, 0);

            if (list != null && list.Count == 1)
            {
                if (DataChoosed != null)
                    DataChoosed(this, new DataFindEventArgs(Utility.GetSingleSearchResult<UserList>(list[0], returnFields)));

                return;
            }

            #endregion

            singleFinderWorkitem = Workitem.WorkItems.Get<UserSingleFinderWorkitem>(this.GetHashCode().ToString() + "UserSingleFinderWorkitem");
            if (singleFinderWorkitem == null)
            {
                singleFinderWorkitem = Workitem.WorkItems.AddNew<UserSingleFinderWorkitem>(this.GetHashCode().ToString() + "UserSingleFinderWorkitem");
                singleFinderWorkitem.DataChoosed += this.OnDataChoosed;
            }


            Dictionary<string, object> initValues = new Dictionary<string, object>();
            initValues.Add("Name", name);
            initValues.Add("Code", code);

            if (conditions != null && conditions.Contain("FormTitle"))
            {
                initValues.Add("FormTitle", conditions.GetValue("FormTitle"));
            }

            singleFinderWorkitem.Show(workspace, list, returnFields, initValues);
        }
        private void OnDataChoosed(object sender, DataFindEventArgs e)
        {
            if (this.DataChoosed != null) DataChoosed(sender, e);
        }
        #endregion

        #region Many

        public void PickMany(string searchValue, string property, SearchConditionCollection conditions, string[] returnFields, FinderTriggerType triggerType, GetExistValueHandler getExistValueHandler, System.Windows.Forms.Control container)
        {
            DeckWorkspace workspce = this.Workitem.Workspaces.Get<DeckWorkspace>(UserFinderWorkspace);
            if (workspce == null || workspce.IsDisposed)
            {
                workspce = this.Workitem.Workspaces.AddNew<DeckWorkspace>(UserFinderWorkspace);
                container.Controls.Add(workspce);
                workspce.Dock = System.Windows.Forms.DockStyle.Fill;
                workspce.BringToFront();
            }
            this.PickMany(searchValue, property, conditions, returnFields, triggerType, getExistValueHandler, UserFinderWorkspace);
        }

        public void PickMany(string searchValue, string property, SearchConditionCollection conditions, string[] returnFields, FinderTriggerType triggerType, GetExistValueHandler getExistValueHandler, string workspaceName)
        {
            if (string.IsNullOrEmpty(workspaceName))
                workspaceName = ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace;

            IWorkspace workspace = this.Workitem.Workspaces[workspaceName];

            #region
            string name, code;
            name = code = string.Empty;
            if (triggerType == FinderTriggerType.KeyEnter)
            {
                if (IsValidChinese(searchValue))
                {
                    name = searchValue;
                }
                else
                {
                    code = searchValue;
                }
            }

            List<UserList> list = UserService.GetUserListByList(code, name, null, null, null, null, true, true, 0);
            if (list != null && list.Count == 1)
            {
                if (DataChoosed != null)
                    DataChoosed(this, new DataFindEventArgs(Utility.GetMultiSearchResult<UserList>(list, returnFields)));

                return;
            }

            List<Guid> existValues = new List<Guid>();
            IList exists = null;
            if (getExistValueHandler != null)
            {
                exists = getExistValueHandler();
            }
            if (exists != null && exists.Count > 0)
            {
                foreach (var item in exists)
                {
                    existValues.Add((Guid)item);
                }
            }
            List<UserList> existList = new List<UserList>();
            foreach (var item in list)
            {
                if (existValues.Contains(item.ID))
                    existList.Add(item);
            }

            #endregion

            multiFinderWorkitem = Workitem.WorkItems.Get<UserMultiFinderWorkitem>(this.GetHashCode().ToString() + "UserMultiFinderWorkitem");
            if (multiFinderWorkitem == null)
            {
                multiFinderWorkitem = Workitem.WorkItems.AddNew<UserMultiFinderWorkitem>(this.GetHashCode().ToString() + "UserMultiFinderWorkitem");
                multiFinderWorkitem.DataChoosed += OnMultiFinderDataChoosed;
            }


            Dictionary<string, object> initValues = new Dictionary<string, object>();
            initValues.Add("Name", name);
            initValues.Add("Code", code);

            if (conditions != null && conditions.Contain("FormTitle"))
            {
                initValues.Add("FormTitle", conditions.GetValue("FormTitle").Value);
            }

            multiFinderWorkitem.Show(workspace, list, existList, returnFields, initValues);
        }

        #endregion
        private void OnMultiFinderDataChoosed(object sender, DataFindEventArgs e)
        {
            if (this.DataChoosed != null) DataChoosed(sender, e);
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            this.DataChoosed = null;
            if (Workitem != null)
            {
                if (singleFinderWorkitem != null)
                {
                    singleFinderWorkitem.DataChoosed -= this.OnDataChoosed;
                    singleFinderWorkitem = null;
                }
                if (multiFinderWorkitem != null)
                {
                    multiFinderWorkitem.DataChoosed -= OnMultiFinderDataChoosed;
                    multiFinderWorkitem = null;
                }
                Workitem.Items.Remove(this);
                Workitem = null;
            }
        }

        #endregion
    }

}
