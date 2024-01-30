using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.OA.UI.UserInformation
{
    class UserInfoWorkItem : WorkItem
    {
        #region services
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion
        #region 方法
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Workitem = null;
            }
            base.Dispose(disposing);
        }
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        public partial class UserInfoWorkSpaceConstants
        {
            public const string SearchWorkspace = "SearchWorkspace";
            public const string ReportWorkspace = "ReportWorkspace";
            public const string MainViewWorkspce = "MainViewWorkspce";
        }
        public void Show()
        {
            UserInfoMainWorkSpace UserMainWorkSpace = this.SmartParts.Get<UserInfoMainWorkSpace>("UserInfoMainWorkSpace");
            if (UserMainWorkSpace == null)
            {
                UserMainWorkSpace = this.SmartParts.AddNew<UserInfoMainWorkSpace>("UserInfoMainWorkSpace");

                //ContactSearchPart contactSearchPart = this.SmartParts.AddNew<ContactSearchPart>();
                //IWorkspace searchViewWorkspce = (IWorkspace)this.Workspaces[UserInfoWorkSpaceConstants.SearchWorkspace];
                //searchViewWorkspce.Show(contactSearchPart);

                //contactSearchPart.OnSearched += delegate(object sender, object results)
                //{
                //    //OnSearchPartSearched(reportViewBase, results);
                //};

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "User Info" : "员工信息";
                mainWorkspace.Show(UserMainWorkSpace, smartPartInfo);
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].
                    Activate(UserMainWorkSpace);
            }
        }
        #endregion
    }
}
