using System;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;

namespace ICP.Common.UI.CC
{
    [ToolboxItem(false)]
    public partial class CCToolBar : BaseEditPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        public CCToolBar()
        {
            InitializeComponent();
            this.Disposed += delegate {
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            BulidCommand();
        }

        private void BulidCommand()
        {
            barShowSearch.ItemClick += delegate { Workitem.Commands[CCCommonConstants.Command_ShowSearch].Execute(); };
            barRefresh.ItemClick += delegate { Workitem.Commands[CCCommonConstants.Command_RefreshData].Execute(); };
            barClose.ItemClick += delegate
            {
                var findForm = this.FindForm();
                if (findForm != null) findForm.Close();
            };
        }
    }
}
