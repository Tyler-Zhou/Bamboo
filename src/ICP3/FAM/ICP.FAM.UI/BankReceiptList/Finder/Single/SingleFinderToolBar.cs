using DevExpress.XtraBars;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using System;
using System.ComponentModel;

namespace ICP.FAM.UI.BankReceiptList.Finder
{
    [ToolboxItem(false)]
    public partial class SingleFinderToolBar : BaseEditPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region init

        public SingleFinderToolBar()
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        #endregion

        #region barItem

        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            var findForm = FindForm();
            if (findForm != null) findForm.Close();
        }

        private void barConfirm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BankReceiptFinderConstants.COMMANDSINGLEFINDERCONFIRM].Execute();
            var findForm = FindForm();
            if (findForm != null) findForm.Close();
        }

        private void barSearch_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BankReceiptFinderConstants.COMMANDSINGLEFINDERSHOWSEARCH].Execute();
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
                BankReceiptInfo data = value as BankReceiptInfo;
                barConfirm.Enabled = data != null;
            }
        }

        #endregion
    }
}
