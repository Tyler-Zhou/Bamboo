using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI.WinForms;

namespace ICP.Sys.UI.Job.Finder
{
    public class OrganizationJobMiniFinder : BaseMiniDataFinder
    {
        #region 服务注入

        public IJobService JobService
        {
            get
            {
                return ServiceClient.GetService<IJobService>();
            }
        }

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion
        public override void Dispose()
        {
            if (this._Workitem != null)
            {
                this._Workitem.DataChoosed -= this.OnDataChoosed;
                this._Workitem = null;
            }
            this.Workitem = null;
            this.DataChoosed = null;
            base.Dispose();
        }
        private const string JobMiniFinderWorkspace = "OrganizationJobMiniFinderWorkspace";

        OrganizationJobMiniFinderWorkitem _Workitem = null;

        #region IDataFinder 成员

        public override event EventHandler<DataFindEventArgs> DataChoosed;

        public override void PickOne(string searchValue, string property, string[] returnFields, System.Windows.Forms.Control container)
        {
            DeckWorkspace workspce = this.Workitem.Workspaces.Get<DeckWorkspace>(JobMiniFinderWorkspace);
            if (workspce == null || workspce.IsDisposed)
            {
                if (this.Workitem.Workspaces.Contains(JobMiniFinderWorkspace))
                {
                    this.Workitem.Workspaces.Remove(workspce);
                }
                workspce = this.Workitem.Workspaces.AddNew<DeckWorkspace>(JobMiniFinderWorkspace);
                workspce.Dock = System.Windows.Forms.DockStyle.Fill;
                workspce.BringToFront();
            }
            container.Controls.Clear();
            container.Controls.Add(workspce);
            PickOne(searchValue, property, returnFields, JobMiniFinderWorkspace);
        }

        public void PickOne(string searchValue, string property, string[] returnFields, string workspaceName)
        {
            if (string.IsNullOrEmpty(workspaceName))
                workspaceName = ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace;

            IWorkspace workspace = this.Workitem.Workspaces[workspaceName];
            List<Organization2JobList> list = JobService.GetOrganization2JobList(null, true);

            _Workitem = Workitem.WorkItems.Get<OrganizationJobMiniFinderWorkitem>(this.GetHashCode().ToString() + "OrganizationJobMiniFinderWorkitem");
            if (_Workitem == null)
            {
                _Workitem = Workitem.WorkItems.AddNew<OrganizationJobMiniFinderWorkitem>(this.GetHashCode().ToString() + "OrganizationJobMiniFinderWorkitem");
                _Workitem.DataChoosed += this.OnDataChoosed;
            }
            _Workitem.Show(workspace, list, returnFields);

        }
        private void OnDataChoosed(object sender, DataFindEventArgs e)
        {
            if (this.DataChoosed != null) DataChoosed(sender, e);
        }

        public override void ResetCondition(IDictionary<string, object> values)
        {
            if (_Workitem != null){_Workitem.ResetCondition(values);}
        }

        #endregion
    }

    public class OrganizationAndJobMiniFinder : BaseMiniDataFinder
    {
        #region 服务注入
        public IJobService JobService
        {
            get
            {
                return ServiceClient.GetService<IJobService>();
            }
        }

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        private const string JobMiniFinderWorkspace = "OrganizationAndJobMiniFinderWorkspace";

        OrganizationAndJobMiniFinderWorkitem _Workitem = null;

        #region IDataFinder 成员
        public override void Dispose()
        {
            if (this._Workitem != null)
            {
                this._Workitem.DataChoosed -= this.OnDataChoosed;
                this._Workitem = null;
            }
            this.DataChoosed = null;
            this.Workitem = null;
            base.Dispose();
        }
        public override event EventHandler<DataFindEventArgs> DataChoosed;

        public override void PickOne(string searchValue, string property, string[] returnFields, System.Windows.Forms.Control container)
        {
            DeckWorkspace workspce = this.Workitem.Workspaces.Get<DeckWorkspace>(JobMiniFinderWorkspace);
            if (workspce == null || workspce.IsDisposed)
            {
                if (this.Workitem.Workspaces.Contains(JobMiniFinderWorkspace))
                {
                    this.Workitem.Workspaces.Remove(workspce);
                }
                workspce = this.Workitem.Workspaces.AddNew<DeckWorkspace>(JobMiniFinderWorkspace);
                workspce.Dock = System.Windows.Forms.DockStyle.Fill;
                workspce.BringToFront();
            }
            container.Controls.Clear();
            container.Controls.Add(workspce);
            PickOne(searchValue, property, returnFields, JobMiniFinderWorkspace);
        }

        public void PickOne(string searchValue, string property, string[] returnFields, string workspaceName)
        {
            if (string.IsNullOrEmpty(workspaceName))
                workspaceName = ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace;

            IWorkspace workspace = this.Workitem.Workspaces[workspaceName];
            List<Organization2JobList> list = JobService.GetOrganization2JobList(null, true);

            _Workitem = Workitem.WorkItems.Get<OrganizationAndJobMiniFinderWorkitem>(this.GetHashCode().ToString() + "OrganizationAndJobMiniFinderWorkitem");
            if (_Workitem == null)
            {
                _Workitem = Workitem.WorkItems.AddNew<OrganizationAndJobMiniFinderWorkitem>(this.GetHashCode().ToString() + "OrganizationAndJobMiniFinderWorkitem");
                _Workitem.DataChoosed += this.OnDataChoosed;
            }
            _Workitem.Show(workspace, list, returnFields);

        }
        private void OnDataChoosed(object sender, DataFindEventArgs e)
        {
            if (this.DataChoosed != null) DataChoosed(sender, e);
        }

        public override void ResetCondition(IDictionary<string, object> values)
        {
            if (_Workitem != null) { _Workitem.ResetCondition(values); }
        }

        #endregion
    }

}
