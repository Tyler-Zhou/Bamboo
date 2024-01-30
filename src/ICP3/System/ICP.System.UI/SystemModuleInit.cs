using System;
using ICP.Sys.UI.SystemHelp.SystemErrorMemo;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Sys.ServiceInterface;
using ICP.Framework.ClientComponents.Controls;

namespace ICP.Sys.UI
{
    public class SystemModuleInit : Microsoft.Practices.CompositeUI.ModuleInit
    {

        Microsoft.Practices.CompositeUI.WorkItem _rootWorkItem;
        IDataFinderFactory _datafinderFactory;

        public SystemModuleInit([ServiceDependency]WorkItem rootWorkItem
            , [ServiceDependency]IDataFinderFactory datafinderFactory
            )
        {
            _rootWorkItem = rootWorkItem;
            _datafinderFactory = datafinderFactory;
        }


        public override void AddServices()
        {
            base.AddServices();

            _rootWorkItem.Services.AddNew<ICP.Sys.UI.Common.UserClientService, IUserClientService>();

            // role
            _datafinderFactory.RegisterMini<ICP.Sys.UI.Role.Finder.RoleMiniFinder>(SystemFinderConstants.RoleFinder);

            // job
            _datafinderFactory.RegisterMini<ICP.Sys.UI.Job.Finder.OrganizationJobMiniFinder>(SystemFinderConstants.OrganizationJobFinder);

            // job
            _datafinderFactory.RegisterMini<ICP.Sys.UI.Job.Finder.OrganizationAndJobMiniFinder>(SystemFinderConstants.OrganizationAndJobFinder);

            // job
            _datafinderFactory.RegisterMini<ICP.Sys.UI.Job.Finder.JobMiniFinder>(SystemFinderConstants.JobFinder);

            //Organization
            _datafinderFactory.RegisterMini<ICP.Sys.UI.Organization.Finder.OrgMiniFinder>(SystemFinderConstants.OrganizationFinder);

            //User
            _datafinderFactory.Register<ICP.Sys.UI.UserManage.Finder.UserFinder>(SystemFinderConstants.UserFinder);

            _datafinderFactory.RegisterMini<ICP.Sys.UI.UserManage.MiniFinder.UserMiniFinder>(SystemFinderConstants.UserFinder);
        }

        #region Command

        #region 角色
        //打开角色管理
        [CommandHandler(FunctionConstants.SYSTEM_ROLELIST)]
        public void Open_SYSTEM_ROLELIST(object sender, EventArgs e)
        {
            using (new CursorHelper())
            {
                Role.RoleWorkitem roleWorkitem = _rootWorkItem.WorkItems.AddNew<Role.RoleWorkitem>();
                roleWorkitem.Run();
            }
        }
        #endregion

        #region 职位
        //打开职位管理
        [CommandHandler(FunctionConstants.SYSTEM_JOBLIST)]
        public void Open_SYSTEM_JOBLIST(object sender, EventArgs e)
        {
            using (new CursorHelper())
            {
                Job.JobWorkitem jobWorkitem = _rootWorkItem.WorkItems.AddNew<Job.JobWorkitem>();
                jobWorkitem.Run();
            }
        }
        #endregion

        #region 用户
        //打开用户管理
        [CommandHandler(FunctionConstants.SYSTEM_USERLIST)]
        public void Open_SYSTEM_USERLIST(object sender, EventArgs e)
        {
            using (new CursorHelper())
            {
                UserManage.UserWorkitem userWorkitem = _rootWorkItem.WorkItems.AddNew<UserManage.UserWorkitem>();
                userWorkitem.Run();
            }
        }
        #endregion

        #region 组织结构
        //打开组织结构管理
        [CommandHandler(FunctionConstants.SYSTEM_ORGANIZATIONLIST)]
        public void Open_SYSTEM_ORGANIZATIONLIST(object sender, EventArgs e)
        {
            using (new CursorHelper())
            {
                Organization.OrganizationWorkitem organizationWorkitem = _rootWorkItem.WorkItems.AddNew<Organization.OrganizationWorkitem>();
                organizationWorkitem.Run();
            }
        }
        #endregion

        #region 权限管理
        //打开功能表权限管理 
        [CommandHandler(FunctionConstants.SYSTEM_PERMISSIONLIST)]
        public void Open_SYSTEM_PERMISSIONLIST(object sender, EventArgs e)
        {
            using (new CursorHelper())
            {
                PermissionManage.Function.FunctionWorkitem functionWorkitem = _rootWorkItem.WorkItems.AddNew<PermissionManage.Function.FunctionWorkitem>();
                functionWorkitem.Run();
            }
        }
        #endregion

        #region 菜单配置

        //打开菜单配置管理
        [CommandHandler(FunctionConstants.SYSTEM_UICONFIGURELIST)]
        public void Open_SYSTEM_UICONFIGURELIST(object sender, EventArgs e)
        {
            using (new CursorHelper())
            {
                PermissionManage.Permission.PermissionWorkitem permissionWorkitem = _rootWorkItem.WorkItems.AddNew<PermissionManage.Permission.PermissionWorkitem>();
                permissionWorkitem.Run();
            }
        }
        #endregion

        #region 用户资料
        /// <summary>
        /// 框架中主窗口右下角的用户资料按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Framework_ModifyUserInfo")]
        public void Framework_ModifyUserInfo(object sender, EventArgs e)
        {
            using (new CursorHelper())
            {
                ICP.Sys.UI.UserManage.UserInfoEditPart.UserInfoCustom.UserID = LocalData.UserInfo.LoginID;
                ICP.Sys.UI.UserManage.UserInfoEditPart.UserInfoCustom.PoweModifyPassword = false;
                ICP.Sys.UI.UserManage.UserInfoEditPart ue = _rootWorkItem.Items.AddNew<ICP.Sys.UI.UserManage.UserInfoEditPart>();
                PartLoader.ShowDialog(ue, LocalData.IsEnglish ? "User Info" : "个人资料", System.Windows.Forms.FormBorderStyle.Sizable);
            }

        }
        #endregion

        #region 新增反馈

        //新增反馈
        [CommandHandler(FunctionConstants.SYSTEM_NEWFEEDBACK)]
        public void Open_SYSTEM_NEWFEEDBACK(object sender, EventArgs e)
        {
            using (new CursorHelper())
            {

                SystemHelp.NewFeedback.NewFeedbackWorkItem newFeedbackWorkItem = _rootWorkItem.WorkItems.AddNew<SystemHelp.NewFeedback.NewFeedbackWorkItem>();
                newFeedbackWorkItem.Run();


            }

        }
        #endregion

        #region 我的反馈

        //我的反馈
        [CommandHandler(FunctionConstants.SYSTEM_MYFEEDBACKS)]
        public void Open_SYSTEM_MYFEEDBACKS(object sender, EventArgs e)
        {
            using (new CursorHelper())
            {
                SystemHelp.MyFeedback.MyFeedbackWorkItem myFeedbackWorkItem = _rootWorkItem.WorkItems.AddNew<SystemHelp.MyFeedback.MyFeedbackWorkItem>();
                myFeedbackWorkItem.Run();

            }
        }
        #endregion

        #region 反馈示例

        //反馈示例
        [CommandHandler(FunctionConstants.SYSTEM_SAMPLENEWFEEDBACK)]
        public void Open_SYSTEM_SAMPLENEWFEEDBACK(object sender, EventArgs e)
        {
            using (new CursorHelper())
            {
                SystemHelp.FeedbackSample.FeedbackSampleWorkItem feedbackSampleWorkItem = _rootWorkItem.WorkItems.AddNew<SystemHelp.FeedbackSample.FeedbackSampleWorkItem>();
                feedbackSampleWorkItem.Run();
            }
        }
        #endregion

        #region 系统错误日志

        [CommandHandler(FunctionConstants.SYSTEM_SYSTEMERRORLOG)]
        public void SYSTEM_SYSTEMERRORLOG(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm();
            try
            {
                var systemErrorMemoWorkItem = _rootWorkItem.WorkItems.AddNew<SystemErrorMemoWorkItem>();
                systemErrorMemoWorkItem.Run();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
            }
        }

        #endregion

        #region 帮助文档
        [CommandHandler(FunctionConstants.SYSTEM_HELPDOCUMENT)]
        public void Open_SYSTEM_HELPDOCUMENT(object sender, EventArgs e)
        {
            using (new CursorHelper())
            {
                SystemHelp.HelpDocument.HelpDocumentWorkItem helpDocumentWorkItem = _rootWorkItem.WorkItems.AddNew<SystemHelp.HelpDocument.HelpDocumentWorkItem>();
                helpDocumentWorkItem.Run();
            }
        }

        #endregion

        #region 任务中心权限配置
        [CommandHandler(FunctionConstants.SYSTEM_WORKSPACEVIEW)]
        public void SYSTEM_WORKSPACEVIEW(object sender, EventArgs e)
        {
            using (new CursorHelper())
            {
                ICP.Sys.UI.WorkSpaceView.WorkSpaceViewWorkItem workSpaceWorkItem = _rootWorkItem.WorkItems.AddNew<ICP.Sys.UI.WorkSpaceView.WorkSpaceViewWorkItem>();
                workSpaceWorkItem.Run();
            }
        }
        #endregion

        #endregion

    }
}
