using System;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI.Commands;
using DevExpress.XtraBars.Docking;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;

namespace ICP.Common.UI
{
    [ToolboxItem(false)]
    public partial class MovieProjectMainWorkSpace : DevExpress.XtraEditors.XtraUserControl
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        public MovieProjectMainWorkSpace()
        {
            InitializeComponent();
           dpSearch.Text= LocalData.IsEnglish?"Search":"查询";
           this.Disposed += delegate {

               if (Workitem != null)
               {
                   Workitem.Workspaces.Remove(this.ToolbarWorkspace);

                   Workitem.Workspaces.Remove(this.SearchWorkspace);
                   Workitem.Workspaces.Remove(this.EditWorkspace);
                   Workitem.Workspaces.Remove(this.ListWorkspace);
                   Workitem.Items.Remove(this);

                   this.SearchWorkspace.PerformLayout();
                   this.ToolbarWorkspace.PerformLayout();
                   this.ListWorkspace.PerformLayout();
                   this.EditWorkspace.PerformLayout();
                   this.PerformLayout();
                   Workitem.Dispose();
                   Workitem = null;
               }
           
           };
        }

        [CommandHandler(MovieProjectCommandConstants.Command_MovieProjectShowSearch)]
        public void Command_ShowSearch(object o, EventArgs e)
        {
            if (dpSearch.Visibility == DockVisibility.Hidden)
            {
                dpSearch.Visibility = DockVisibility.Visible;
            }
            else
            {
                dpSearch.Visibility = DockVisibility.Hidden;
            }
            ToolbarWorkspace.SendToBack();
            this.Refresh();
        }

    }
}
