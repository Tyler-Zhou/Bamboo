﻿using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using DevExpress.XtraBars;

namespace ICP.Common.UI.Configure.ChargingCode
{
    [ToolboxItem(false)]
    public partial class ChargingCodeFinderToolBar : BaseEditPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region init

        public ChargingCodeFinderToolBar()
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
            Workitem.Commands[ChargingCodeInfoCommonConstants.Common_FinderConfirm].Execute();
            CloseForm();
        }

        private void barSearch_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[ChargingCodeInfoCommonConstants.Command_ShowSearch].Execute();
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
                ChargingCodeList data = value as ChargingCodeList;
                if (data == null) barConfirm.Enabled = false;
                else barConfirm.Enabled = true;
            }
        }

        #endregion
    }
}