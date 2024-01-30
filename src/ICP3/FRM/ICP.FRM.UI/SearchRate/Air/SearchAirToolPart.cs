using System;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;

namespace ICP.FRM.UI.SearchRate
{
    [ToolboxItem(false)]
    public partial class SearchAirToolPart : BaseToolBar
    {
        public SearchAirToolPart()
        {
            InitializeComponent();
            Disposed += delegate {
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            barSearch.ItemClick += delegate { Workitem.Commands[SearchAirCommandConstants.Command_ShowSearch].Execute(); };
            barRefresh.ItemClick += delegate { Workitem.Commands[SearchAirCommandConstants.Command_RefreshData].Execute(); };
            barClose.ItemClick += delegate { FindForm().Close(); };
        }
    }
}
