using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using DevExpress.XtraBars;

namespace ICP.Common.UI.Geography.Location
{
    [ToolboxItem(false)]
    public partial class LocationSingleFinderToolBar : BaseEditPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region init

        public LocationSingleFinderToolBar()
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

        #endregion

        #region barItem

        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            CloseForm();
        }

        private void barConfirm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[LocationCommonConstants.Common_FinderConfirm].Execute();
            CloseForm();
        }

        private void barSearch_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[LocationCommonConstants.Command_ShowSearch].Execute();
        }

        private void CloseForm()
        {
            var findForm = this.FindForm();
            if (findForm != null) findForm.Close();
        }

        #endregion

        #region BaseEdit成员

        public override object DataSource
        {
            get
            {
                return null;
            }
            set
            {
                LocationList data = value as LocationList;
                if (data == null) barConfirm.Enabled = false;
                else barConfirm.Enabled = true;
            }
        }

        #endregion
    }
}
