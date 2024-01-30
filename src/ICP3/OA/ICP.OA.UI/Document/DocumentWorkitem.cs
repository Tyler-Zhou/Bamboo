using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System.ComponentModel;
using ICP.OA.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.OA.UI.Document
{

    public class DocumentWorkitem:WorkItem
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public ICP.OA.ServiceInterface.Client.IDocumentClientService DocumentClientService
        {
            get
            {
                return ServiceClient.GetClientService<ICP.OA.ServiceInterface.Client.IDocumentClientService>();
            }
        }

        #endregion
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Workitem = null;
            }
            base.Dispose(disposing);
        }
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            DocMainWorkspce mainSpce = this.SmartParts.Get<DocMainWorkspce>("DocMainWorkspce");
            if (mainSpce == null)
            {

                mainSpce = this.SmartParts.AddNew<DocMainWorkspce>("DocMainWorkspce");

                DocMainViewPart docMainViewPart = this.SmartParts.AddNew<DocMainViewPart>();
                IWorkspace mainViewWorkspce = (IWorkspace)this.Workspaces[DocWorkSpaceConstants.MainViewWorkspce];
                mainViewWorkspce.Show(docMainViewPart);

                DocOrgJobListPart docjobPart = this.SmartParts.AddNew<DocOrgJobListPart>();
                IWorkspace jobWorkspace = (IWorkspace)this.Workspaces[DocWorkSpaceConstants.JobWorkspace];
                jobWorkspace.Show(docjobPart);

                DocUserListPart docUserPart = this.SmartParts.AddNew<DocUserListPart>();
                IWorkspace userWorkspace = (IWorkspace)this.Workspaces[DocWorkSpaceConstants.UserWorkspace];
                userWorkspace.Show(docUserPart);

                #region Connection

                docMainViewPart.CurrentChanging += delegate(object sender, CancelEventArgs e)
                {
                    UIConnectionHelper.ParentChangingForListPart<DocumentOrganizationJobPermissionList>(docjobPart.SaveData
                                                                                    , (docjobPart.DataSource as List<DocumentOrganizationJobPermissionList>)
                                                                                    , e
                                                                                    , LocalData.IsEnglish ? "Job" : "职位 ");

                    UIConnectionHelper.ParentChangingForListPart<DocumentUserPermissionList>(docUserPart.SaveData
                                                                                   , (docUserPart.DataSource as List<DocumentUserPermissionList>)
                                                                                   , e
                                                                                   , LocalData.IsEnglish ? "User" : "用户 ");
                };
               
                docMainViewPart.CurrentChanged += delegate(object sender, object data)
                {
                    Dictionary<string, object> keyValue = new Dictionary<string, object>();
                    keyValue.Add("ParentList", data);
                    docUserPart.Init(keyValue);
                    docjobPart.Init(keyValue);

                    DocumentFolderFileList listData = data as DocumentFolderFileList;

                    #region user

                    List<DocumentUserPermissionList> userDocumentPermissionList = null;
                    if (listData.IsNew == false)
                    {
                        userDocumentPermissionList = DocumentClientService.GetDocumentUserPermissionList(listData.ID); 
                    }
                    
                    docUserPart.DataSource = userDocumentPermissionList;

                    #endregion

                    #region job

                    List<DocumentOrganizationJobPermissionList> jobDocumentPermissionList = null;
                    if (((DocumentFolderFileList)data).IsNew==false)
                    {
                        jobDocumentPermissionList = DocumentClientService.GetDocumentOrganizationJobPermissionList(listData.ID); 
                    }
                   
                    docjobPart.DataSource = jobDocumentPermissionList;

                    #endregion
                };

                #endregion

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo info = new SmartPartInfo();
                info.Title =LocalData.IsEnglish ?   "Document Manage":"文档管理";
                mainWorkspace.Show(mainSpce, info);
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }
    }


    public class DocWorkSpaceConstants
    {
        public const string UserWorkspace = "UserWorkspace";
        public const string JobWorkspace = "JobWorkspace";
        public const string MainViewWorkspce = "MainViewWorkspce";
    }

    public class DocStateConstants
    {
        public const string User2OrganizationJob = "User2OrganizationJob";
    }
}
