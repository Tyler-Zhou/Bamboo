using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.WinForms;
using System;
using System.Collections.Generic;

namespace ICP.Sys.UI.UserManage.MiniFinder
{
    /// <summary>
    /// 
    /// </summary>
    public class UserMiniFinder : BaseMiniDataFinder
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        private const string UserFinderWorkspace = "UserMiniFinderWorkspace";

        UMFS_Workitem _UMFS_Workitem = null;

        #region IDataFinder 成员
        public override void Dispose()
        {
            Workitem = null;
            this.DataChoosed = null;
            if (this._UMFS_Workitem != null)
            {
                _UMFS_Workitem.DataChoosed -= this.OnDataChoosed;
                this._UMFS_Workitem = null;
            }
            base.Dispose();
        }
        /// <summary>
        /// 
        /// </summary>
        public override event EventHandler<DataFindEventArgs> DataChoosed;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="property"></param>
        /// <param name="returnFields"></param>
        /// <param name="container"></param>
        public override void PickOne(string searchValue, string property, string[] returnFields, System.Windows.Forms.Control container)
        {
            DeckWorkspace workspce = this.Workitem.Workspaces.Get<DeckWorkspace>(UserFinderWorkspace);
            if (workspce == null || workspce.IsDisposed)
            {
                if (this.Workitem.Workspaces.Contains(UserFinderWorkspace))
                {
                    this.Workitem.Workspaces.Remove(workspce);
                }

                workspce = this.Workitem.Workspaces.AddNew<DeckWorkspace>(UserFinderWorkspace);
                workspce.Dock = System.Windows.Forms.DockStyle.Fill;
                workspce.BringToFront();
            }
            container.Controls.Clear();
            container.Controls.Add(workspce);

            PickOne(searchValue, property, returnFields, UserFinderWorkspace);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="property"></param>
        /// <param name="returnFields"></param>
        /// <param name="workspaceName"></param>
        public void PickOne(string searchValue, string property, string[] returnFields, string workspaceName)
        {
            if (string.IsNullOrEmpty(workspaceName))
                workspaceName = ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace;

            IWorkspace workspace = this.Workitem.Workspaces[workspaceName];
            _UMFS_Workitem = Workitem.WorkItems.Get<UMFS_Workitem>(this.GetHashCode().ToString() + "UMFS_Workitem");
            if (_UMFS_Workitem == null)
            {
                _UMFS_Workitem = Workitem.WorkItems.AddNew<UMFS_Workitem>(this.GetHashCode().ToString() + "UMFS_Workitem");
                _UMFS_Workitem.DataChoosed += this.OnDataChoosed;
            }
            _UMFS_Workitem.Show(workspace, returnFields);

        }
        private void OnDataChoosed(object sender, DataFindEventArgs e)
        {
            if (this.DataChoosed != null) DataChoosed(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        public override void ResetCondition(IDictionary<string, object> values)
        {
            if (_UMFS_Workitem != null) _UMFS_Workitem.ResetCondition(values);
        }

        #endregion
    }
}
