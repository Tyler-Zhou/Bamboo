using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.Sys.UI.WorkSpaceView
{
    public class WorkSpaceViewWorkItem : WorkItem
    {
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            WorkSpaceViewMainWorkSpace mainSpce = this.SmartParts.Get<WorkSpaceViewMainWorkSpace>("WorkSpaceViewMainWorkSpace");
            if (mainSpce == null)
            {
                mainSpce = this.SmartParts.AddNew<WorkSpaceViewMainWorkSpace>("WorkSpaceViewMainWorkSpace");

                #region AddPart

                WorkSpaceViewToolPart toolBar = this.SmartParts.AddNew<WorkSpaceViewToolPart>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[WorkSpaceViewConstants.ToolWorkSpace];
                toolBarWorkspace.Show(toolBar);

                WorkSpaceViewListPart listPart = this.SmartParts.AddNew<WorkSpaceViewListPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[WorkSpaceViewConstants.ListWorkSpace];
                listWorkspace.Show(listPart);

                WorkSpaceViewOpViewPart opListPart = this.SmartParts.AddNew<WorkSpaceViewOpViewPart>();
                IWorkspace opListWorkspace = (IWorkspace)this.Workspaces[WorkSpaceViewConstants.OPListWorkSpace];
                opListWorkspace.Show(opListPart);

                WorkSpaceViewUserPart userListPart = this.SmartParts.AddNew<WorkSpaceViewUserPart>();
                IWorkspace userListWorkspace = (IWorkspace)this.Workspaces[WorkSpaceViewConstants.UserListWorkSpace];
                userListWorkspace.Show(userListPart);

                WorkSpaceViewRolePart roleListPart = this.SmartParts.AddNew<WorkSpaceViewRolePart>();
                IWorkspace roleListWorkspce = (IWorkspace)this.Workspaces[WorkSpaceViewConstants.RoleListWorkSpace];
                roleListWorkspce.Show(roleListPart);

                #endregion

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "任务中心配置" : "任务中心配置";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                WorkSpaceUIAdapter workSpaceAdapter = new WorkSpaceUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(opListPart.GetType().Name, opListPart);
                dic.Add(userListPart.GetType().Name, userListPart);
                dic.Add(roleListPart.GetType().Name, roleListPart);

                workSpaceAdapter.Init(dic);
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }


    }
    public class WorkSpaceViewConstants
    {
        public const string WorkSpaceView_Command_Refresh = "WorkSpaceView_Command_Refresh";

        public const string ToolWorkSpace = "ToolWorkSpace";
        public const string ListWorkSpace = "ListWorkSpace";
        public const string OPListWorkSpace = "OPListWorkSpace";
        public const string UserListWorkSpace = "UserListWorkSpace";
        public const string RoleListWorkSpace = "RoleListWorkSpace";
    }

    public class WorkSpaceUIAdapter
    {
        WorkSpaceViewToolPart toolBar = null;
        WorkSpaceViewListPart listPart = null;
        WorkSpaceViewOpViewPart opListPart = null;
        WorkSpaceViewUserPart userListPart = null;
        WorkSpaceViewRolePart roleListPart = null;

        public void Init(Dictionary<string, object> controls)
        {
            toolBar = (WorkSpaceViewToolPart)controls[typeof(WorkSpaceViewToolPart).Name];
            listPart = (WorkSpaceViewListPart)controls[typeof(WorkSpaceViewListPart).Name];
            opListPart = (WorkSpaceViewOpViewPart)controls[typeof(WorkSpaceViewOpViewPart).Name];
            userListPart = (WorkSpaceViewUserPart)controls[typeof(WorkSpaceViewUserPart).Name];
            roleListPart = (WorkSpaceViewRolePart)controls[typeof(WorkSpaceViewRolePart).Name];

            #region 主表当前行发生改变时
            listPart.CurrentChanged += listPart_CurrentChanged;
            #endregion

            opListPart.Saved += opListPart_Saved;


            listPart.BindData();
        }

        void opListPart_Saved(params object[] prams)
        {
            listPart.EditPartSaved(prams);
        }

        void listPart_CurrentChanged(object sender, object data)
        {
            if (listPart.CurrentRow == null)
            {
                return;
            }
            //填充编辑界面&OperationViews
            opListPart.MainList = listPart.CurrentRow;
            opListPart.BindData();

            //填充RoleList
            roleListPart.WorkSpaceID = listPart.CurrentRow.ID;
            roleListPart.BindData();

            //填充UserList
            userListPart.WorkSpaceID = listPart.CurrentRow.ID;
            userListPart.BindData();
        }


    }

}
