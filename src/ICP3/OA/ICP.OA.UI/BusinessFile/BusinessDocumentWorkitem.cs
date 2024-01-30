using System.Collections.Generic;
using System.ComponentModel;
using ICP.Framework.CommonLibrary.Client;
using ICP.OA.ServiceInterface.Client;
using ICP.OA.ServiceInterface.DataObjects;
using ICP.OA.UI.Document;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace ICP.OA.UI
{

    public class BusinessDocumentWorkitem : WorkItem
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        public ICP.OA.ServiceInterface.Client.IDocumentClientService DocumentClientService
        {
            get 
            {
                return ServiceClient.GetClientService<IDocumentClientService>();
            }
        }

        #endregion

        /// <summary>
        /// 文件夹类型
        /// </summary>
        public FolderType Foldertype
        {
            get;
            set;
        }

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            DocMainWorkspce mainSpce = this.SmartParts.Get<DocMainWorkspce>("BDocMainWorkspce");
            if (mainSpce == null)
            {

                mainSpce = this.SmartParts.AddNew<DocMainWorkspce>("BDocMainWorkspce");

                BDocMainViewPart docMainViewPart = this.SmartParts.AddNew<BDocMainViewPart>();
                docMainViewPart.Foldertype = this.Foldertype;
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
                    keyValue.Add("BusinessList", data);
                    docUserPart.Init(keyValue);
                    docjobPart.Init(keyValue);

                    DocumentFolderList listData = data as DocumentFolderList;

                    #region user

                    List<DocumentUserPermissionList> userDocumentPermissionList = null;
                    if (listData!=null&&!listData.IsNew)
                    {
                        userDocumentPermissionList = DocumentClientService.GetDocumentUserPermissionList(listData.ID); 
                    }
                    
                    docUserPart.DataSource = userDocumentPermissionList;

                    #endregion

                    #region job

                    List<DocumentOrganizationJobPermissionList> jobDocumentPermissionList = null;

                    if (listData != null && !listData.IsNew)
                    {
                        jobDocumentPermissionList = DocumentClientService.GetDocumentOrganizationJobPermissionList(listData.ID); 
                    }
                   
                    docjobPart.DataSource = jobDocumentPermissionList;

                    #endregion
                };

                #endregion

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo info = new SmartPartInfo();

                if (Foldertype == FolderType.Booking)
                {
                    info.Title = LocalData.IsEnglish ? "Booking Statistics" : "订舱统计";
                }
                else if (Foldertype == FolderType.Business)
                {
                    info.Title = LocalData.IsEnglish ? "Business Info" : "商务信息";
                }
                else
                {
                    info.Title = LocalData.IsEnglish ? "Document Manage" : "文档管理";
                }
                mainWorkspace.Show(mainSpce, info);
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }
    }



}
