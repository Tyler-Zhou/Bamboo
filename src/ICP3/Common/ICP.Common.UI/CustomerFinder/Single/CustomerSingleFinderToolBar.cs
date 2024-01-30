using System;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using DevExpress.XtraBars;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Common.UI.CustomerFinder
{
    [ToolboxItem(false)]
    public partial class CustomerSingleFinderToolBar : BaseEditPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region init

        public CustomerSingleFinderToolBar()
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

            if (LocalCommonServices.PermissionService.HaveFunctionPermission("COMMON_CUSTOMERLIST"))
            {
                barAdd.Enabled = true;
                barEdit.Enabled = true;
            }
            else
            {
                barAdd.Enabled = false;
                barEdit.Enabled = false;
            }
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
            Workitem.Commands[CustomerCommonConstants.Common_FinderConfirm].Execute();
            var findForm = FindForm();
            if (findForm != null) findForm.Close();
        }

        private void barSearch_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[CustomerCommonConstants.Command_ShowSearch].Execute();
        }

        private void barEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[CustomerCommonConstants.Common_FinderEdit].Execute();    
        }

        private void barAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[CustomerCommonConstants.Common_FinderAdd].Execute();
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
                CustomerInfo data = value as CustomerInfo;
                barConfirm.Enabled = data != null;
            }
        }

        #endregion
    }
}
