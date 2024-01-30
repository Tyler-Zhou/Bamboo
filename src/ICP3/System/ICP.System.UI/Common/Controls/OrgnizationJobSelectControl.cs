using System;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.Sys.UI.Common
{
    public partial class OrgnizationJobSelectControl : BaseEditPart
    {
        public OrgnizationJobSelectControl()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.treeMain.DataSource = null;
                this.treeMain.DoubleClick -= this.treeMain_DoubleClick;
                this.treeMain.GetStateImage -= this.treeMain_GetStateImage;
                this.bsList.DataSource = null;
                this.bsList.Dispose();
                this.Saved = null;
            
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            treeMain.ExpandAll();
        }

        private void treeMain_DoubleClick(object sender, EventArgs e)
        {
            if (CurrentRow == null || CurrentRow.Type == OrganizationJobType.Organization) return;

            if (Saved != null) Saved(CurrentRow);
        }

        private Organization2JobList CurrentRow
        {
            get { return bsList.Current as Organization2JobList; }
        }

        public override object DataSource
        {
            get { return bsList.DataSource; }
            set { bsList.DataSource =value; }
        }
        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        private void treeMain_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            Organization2JobList data = treeMain.GetDataRecordByNode(e.Node) as Organization2JobList;
            if (data == null) return;
            e.Node.StateImageIndex = (short)data.Type;
        }
    }
}
