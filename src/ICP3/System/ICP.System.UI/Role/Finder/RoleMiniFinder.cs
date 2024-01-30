using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI.WinForms;

namespace ICP.Sys.UI.Role.Finder
{
    public class RoleMiniFinder : BaseMiniDataFinder
    {
        #region 服务注入

        public IRoleService RoleService
        {
            get
            {
                return ServiceClient.GetService<IRoleService>();
            }
        }

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        private const string RoleMiniFinderWorkspace = "RoleMiniFinderWorkspace";

        RoleMiniFinderWorkitem _RoleMiniFinderWorkitem = null;

        #region IDataFinder 成员
        public override void Dispose()
        {
            this.DataChoosed = null;
            if (this._RoleMiniFinderWorkitem != null)
            {
                _RoleMiniFinderWorkitem.DataChoosed -= this.OnDataChoosed;
                this._RoleMiniFinderWorkitem = null;
            }
            if (this.Workitem != null)
            {
                this.Workitem.Items.Remove(this);
                this.Workitem = null;
            }
            base.Dispose();
        }
        public override event EventHandler<DataFindEventArgs> DataChoosed;

        public override void PickOne(string searchValue, string property, string[] returnFields, System.Windows.Forms.Control container)
        {
            DeckWorkspace workspce = this.Workitem.Workspaces.Get<DeckWorkspace>(RoleMiniFinderWorkspace);
            if (workspce == null || workspce.IsDisposed)
            {
                if (this.Workitem.Workspaces.Contains(RoleMiniFinderWorkspace))
                {
                    this.Workitem.Workspaces.Remove(workspce);
                }
                workspce = this.Workitem.Workspaces.AddNew<DeckWorkspace>(RoleMiniFinderWorkspace);
                workspce.Dock = System.Windows.Forms.DockStyle.Fill;
                workspce.BringToFront();
            }
            container.Controls.Clear();
            container.Controls.Add(workspce);
            PickOne(searchValue, property, returnFields, RoleMiniFinderWorkspace);
        }

        public void PickOne(string searchValue, string property, string[] returnFields, string workspaceName)
        {
            if (string.IsNullOrEmpty(workspaceName))
                workspaceName = ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace;

            IWorkspace workspace = this.Workitem.Workspaces[workspaceName];
            List<RoleList> list = RoleService.GetRoleList(string.Empty, true, 0);

            _RoleMiniFinderWorkitem = Workitem.WorkItems.Get<RoleMiniFinderWorkitem>(this.GetHashCode().ToString() + "RoleMiniFinderWorkitem");
            if (_RoleMiniFinderWorkitem == null)
            {
                _RoleMiniFinderWorkitem = Workitem.WorkItems.AddNew<RoleMiniFinderWorkitem>(this.GetHashCode().ToString() + "RoleMiniFinderWorkitem");
                _RoleMiniFinderWorkitem.DataChoosed += this.OnDataChoosed;
               
            }
            _RoleMiniFinderWorkitem.Show(workspace, list, returnFields);

        }
        private void OnDataChoosed(object sender, DataFindEventArgs e)
        {
            if (this.DataChoosed != null) DataChoosed(sender, e);
        }
        public override void ResetCondition(IDictionary<string, object> values)
        {
            _RoleMiniFinderWorkitem.ResetCondition(values);
        }

        #endregion
    }
}
