using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.WinForms;

namespace ICP.Sys.UI.Organization.Finder
{
    public class OrgMiniFinder : BaseMiniDataFinder
    {
        #region 服务注入

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        private const string OrgMiniFinderWorkspace = "OrgMiniFinderWorkspace";

        OrgMiniFinderWorkitem _OrgMiniFinderWorkitem = null;

        #region IDataFinder 成员
        public override void Dispose()
        {
            if (this._OrgMiniFinderWorkitem != null)
            {
                _OrgMiniFinderWorkitem.DataChoosed -= this.OnDataChoosed;
                this._OrgMiniFinderWorkitem = null;
            }
            this.DataChoosed = null;
            this.Workitem = null;
            base.Dispose();
        }
        public override event EventHandler<DataFindEventArgs> DataChoosed;

        public override void PickOne(string searchValue, string property, string[] returnFields, System.Windows.Forms.Control container)
        {
            DeckWorkspace workspce = this.Workitem.Workspaces.Get<DeckWorkspace>(OrgMiniFinderWorkspace);

            if (workspce == null||workspce.IsDisposed)
            {
                if (this.Workitem.Workspaces.Contains(OrgMiniFinderWorkspace))
                {
                    this.Workitem.Workspaces.Remove(workspce);
                }
                workspce = this.Workitem.Workspaces.AddNew<DeckWorkspace>(OrgMiniFinderWorkspace);
                workspce.Dock = System.Windows.Forms.DockStyle.Fill;
                workspce.BringToFront();
            }
            container.Controls.Clear();
            container.Controls.Add(workspce);
            PickOne(searchValue, property, returnFields, OrgMiniFinderWorkspace);
        }

        public void PickOne(string searchValue, string property, string[] returnFields, string workspaceName)
        {
            if (string.IsNullOrEmpty(workspaceName))
                workspaceName = ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace;

            IWorkspace workspace = this.Workitem.Workspaces[workspaceName];

            _OrgMiniFinderWorkitem = Workitem.WorkItems.Get<OrgMiniFinderWorkitem>(this.GetHashCode().ToString() + "OrgMiniFinderWorkitem");
            if (_OrgMiniFinderWorkitem == null)
            {
                _OrgMiniFinderWorkitem = Workitem.WorkItems.AddNew<OrgMiniFinderWorkitem>(this.GetHashCode().ToString() + "OrgMiniFinderWorkitem");
                _OrgMiniFinderWorkitem.DataChoosed += this.OnDataChoosed;
            }
            _OrgMiniFinderWorkitem.Show(workspace, returnFields);

        }
        private void OnDataChoosed(object sender, DataFindEventArgs e)
        {
            if (this.DataChoosed != null) DataChoosed(sender, e);
        }

        public override void ResetCondition(IDictionary<string, object> values)
        {
            if (_OrgMiniFinderWorkitem != null) _OrgMiniFinderWorkitem.ResetCondition(values);
        }

        #endregion
    }
}
