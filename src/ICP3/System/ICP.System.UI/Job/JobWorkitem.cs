using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.WinForms;
using ICP.Sys.ServiceInterface.DataObjects;
using System.ComponentModel;
using ICP.Sys.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Sys.UI.Job
{
    public class JobWorkitem: WorkItem
    {
        public IJobService JobService
        {
            get
            {
                return ServiceClient.GetService<IJobService>();
            }
        }

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            JobMainWorkspace jobMainSpce = this.SmartParts.Get<JobMainWorkspace>("JobMainWorkspace");
            if (jobMainSpce == null)
            {
                jobMainSpce = this.SmartParts.AddNew<JobMainWorkspace>("JobMainWorkspace");

                JobMainListPart jobMainListPart = this.SmartParts.AddNew<JobMainListPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[JobWorkSpaceConstants.ListWorkspace];
                if (listWorkspace == null) listWorkspace = this.Workspaces.AddNew<DeckWorkspace>(JobWorkSpaceConstants.ListWorkspace);
                listWorkspace.Show(jobMainListPart);

                JobEditPart jobEditPart = this.SmartParts.AddNew<JobEditPart>();
                IWorkspace editWorkspace = (IWorkspace)this.Workspaces[JobWorkSpaceConstants.EditWorkspace];
                if (editWorkspace == null) editWorkspace = this.Workspaces.AddNew<DeckWorkspace>(JobWorkSpaceConstants.EditWorkspace);
                editWorkspace.Show(jobEditPart);

                JobOrganizationPart jobOrganizationPart = this.SmartParts.AddNew<JobOrganizationPart>();
                IWorkspace organizationWorkspace = (IWorkspace)this.Workspaces[JobWorkSpaceConstants.OrganizationWorkspace];
                if (editWorkspace == null) organizationWorkspace = this.Workspaces.AddNew<DeckWorkspace>(JobWorkSpaceConstants.OrganizationWorkspace);
                organizationWorkspace.Show(jobOrganizationPart);

                #region Connection
                #region CurrentChanging
                jobMainListPart.CurrentChanging += delegate(object sender, CancelEventArgs e)
                {
                   
                    UIConnectionHelper.ParentChangingForEditPart(jobMainListPart
                                                                , jobEditPart.SaveData
                                                                , (jobEditPart.DataSource as JobInfo)
                                                                , e
                                                                , LocalData.IsEnglish ? "Job Edit" : "编辑职位");

                   
                };
                #endregion
                #region jobMainListPart.CurrentChanged
                jobMainListPart.CurrentChanged += delegate(object sender, object data)
                {
                    JobList listData = data as JobList;
                    Dictionary<string, object> keyValue = new Dictionary<string, object>();
                    keyValue.Add("ParentList", data);
                    jobOrganizationPart.Init(keyValue);

                    #region EditPart
                    JobInfo info = null;

                    if (listData != null)
                    {
                        if (listData.IsNew)
                        {
                            info = new JobInfo();
                            Utility.CopyToValue(listData, info, typeof(JobInfo));
                        }
                        else
                        {
                            info = JobService.GetJobInfo(((JobList)data).ID);
                        }
                    }
                    jobEditPart.DataSource = info;
                    #endregion
                };

                #endregion
                #region jobEditPart.Saved
                jobEditPart.Saved += delegate(object[] prams)
                {
                    if (jobMainListPart.Current == null || prams[0] == null) return;

                    ManyHierarchyResultData result = prams[1] as ManyHierarchyResultData;

                    if (result == null) return;

                    JobList joblist = prams[0] as JobList;
                    JobList currentRow = jobMainListPart.Current as JobList;


                    Utility.CopyToValue(joblist, currentRow, typeof(JobList));
                    if (currentRow.IsNew==false)
                    {
                        List<JobList> listSource = jobMainListPart.DataSource as List<JobList>;
                        if (listSource == null) return;
                        if (result == null) return;

                        foreach (SingleHierarchyResultData sr in result.ChildResults)
                        {
                            JobList value = listSource.Find(delegate(JobList item) { return item.ID == sr.ID; });
                            if (value != null)
                            {
                                value.UpdateDate = sr.UpdateDate;
                                value.HierarchyCode = sr.HierarchyCode;
                                value.EndEdit();
                            }
                        }
                    }
                    jobMainListPart.Refresh(currentRow);
                };
                #endregion

                #endregion

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title =  LocalData.IsEnglish?"Job Manage":"职位管理";
                mainWorkspace.Show(jobMainSpce, smartPartInfo);
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(jobMainSpce);
            }
        }
    }

    public class JobWorkSpaceConstants
    {
        public const string OrganizationWorkspace = "OrganizationWorkspace";
        public const string EditWorkspace = "EditWorkspace";
        public const string ListWorkspace = "ListWorkspace";
    }
}
