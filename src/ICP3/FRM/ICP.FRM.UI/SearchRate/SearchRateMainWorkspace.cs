using System.ComponentModel;
using DevExpress.XtraTab;
using ICP.Framework.ClientComponents.UIManagement;
using Microsoft.Practices.CompositeUI;

namespace ICP.FRM.UI.SearchRate
{
    [ToolboxItem(false)]
    public partial class SearchRateMainWorkspace : BasePart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        public SearchRateMainWorkspace()
        {
            InitializeComponent();
            Disposed += delegate
            {
                if (Workitem != null)
                {

                    Workitem.Workspaces.Remove(OceanMainWorkspace);

                    Workitem.Workspaces.Remove(TruckMainWorkspace);
                    Workitem.Workspaces.Remove(AirMainWorkspace);
              
                    Workitem.Items.Remove(this);

                    OceanMainWorkspace.PerformLayout();
                    TruckMainWorkspace.PerformLayout();
                    AirMainWorkspace.PerformLayout();
                    PerformLayout();
                    Workitem.Dispose();
                    Workitem = null;

                }
            };
        }

        bool _isLoadAirWorkitem = false;
        bool _isLoadTruckWorkitem = false;

        private void xtraTabWorkspace1_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (xtraTabWorkspace1.SelectedTabPageIndex == 0)//Ocean
            {
            }
            else if (xtraTabWorkspace1.SelectedTabPageIndex == 1 && _isLoadAirWorkitem==false)//Air
            {
                SearchAirWorkitem airWorkitem = Workitem.WorkItems.AddNew<SearchAirWorkitem>();
                airWorkitem.Run();
                _isLoadAirWorkitem = true;
            }
            else if (xtraTabWorkspace1.SelectedTabPageIndex == 2 && _isLoadTruckWorkitem == false)//Truck
            {
                SearchTruckWorkitem truckWorkitem = Workitem.WorkItems.AddNew<SearchTruckWorkitem>();
                truckWorkitem.Run();
                _isLoadTruckWorkitem = true;
            }
        }

    }
}
