using System;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraBars;

namespace ICP.Common.UI.Geography.Location
{
    [ToolboxItem(false)]
    public partial class LocationMultiFinderToolBar :  ICP.Framework.ClientComponents.UIFramework.BaseEditPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region init

        public LocationMultiFinderToolBar()
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
        }

        #endregion

        #region barItem

        private void barSelect_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[LocationCommonConstants.Common_FindeSelect].Execute();
        }

        private void barSearch_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[LocationCommonConstants.Command_ShowSearch].Execute();
        }

        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
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
                //LocationList data = value as LocationList;
                //if (data == null) barClose .Enabled =barRemoveAll.Enabled = false;
                //else barRemove.Enabled =barRemoveAll.Enabled = true;
            }
        }

        #endregion
    }
}
