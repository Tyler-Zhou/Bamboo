using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.WinForms;

namespace ICP.Sys.UI.Job.Finder
{
    public class JobMiniFinder : BaseMiniDataFinder
    {
        #region 服务注入


        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        private const string JobMiniFinderWorkspace = "JobMiniFinderWorkspace";

        JobMiniFinderWorkitem _JobMiniFinderWorkitem = null;

        #region IDataFinder 成员
        public override void Dispose()
        {
            if (_JobMiniFinderWorkitem != null)
            {
                this._JobMiniFinderWorkitem.DataChoosed -= this.OnDataChoosed;
                this._JobMiniFinderWorkitem = null;
            }
            this.Workitem = null;
            this.DataChoosed = null;
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

            _JobMiniFinderWorkitem = Workitem.WorkItems.Get<JobMiniFinderWorkitem>(this.GetHashCode().ToString() + "JobMiniFinderWorkitem");
            if (_JobMiniFinderWorkitem == null)
            {
                _JobMiniFinderWorkitem = Workitem.WorkItems.AddNew<JobMiniFinderWorkitem>(this.GetHashCode().ToString() + "JobMiniFinderWorkitem");
                _JobMiniFinderWorkitem.DataChoosed += this.OnDataChoosed;

            }
            _JobMiniFinderWorkitem.Show(workspace, returnFields);

        }
        private void OnDataChoosed(object sender, DataFindEventArgs e)
        {
            if (this.DataChoosed != null) DataChoosed(sender, e);
        }

        public override void ResetCondition(IDictionary<string, object> values)
        {
            if (_JobMiniFinderWorkitem != null) _JobMiniFinderWorkitem.ResetCondition(values);
        }

        #endregion
    }
}
