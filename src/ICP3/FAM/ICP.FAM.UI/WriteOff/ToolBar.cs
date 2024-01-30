using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;

using ICP.Framework.ClientComponents.UIFramework;
using DevExpress.XtraBars;

namespace ICP.FAM.UI.WriteOff
{
    [ToolboxItem(false)]
    public partial class ToolBar : BaseToolBar
    {

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();


        public ToolBar()
        {
            InitializeComponent();
            BulidBarItemDictionary();
            barAllCheck.Visibility = BarItemVisibility.Never;
            Disposed += delegate {
                _barItemDic.Clear();
                _barItemDic = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        #region ITools
        private void BulidBarItemDictionary()
        {
            foreach (BarItem item in barManager1.Items)
            {
                _barItemDic.Add(item.Name, item);
            }
        }

        public override void SetEnable(string name, bool enable)
        {
            if (_barItemDic.ContainsKey(name) && _barItemDic[name] != null)
                _barItemDic[name].Enabled = enable;
        }

        public override void SetVisible(string name, bool visible)
        {
            if (_barItemDic.ContainsKey(name) && _barItemDic[name] != null)
                _barItemDic[name].Visibility = visible ? BarItemVisibility.Always : BarItemVisibility.Never;
        }

        public override void SetText(string name, string text)
        {
            if (_barItemDic.ContainsKey(name) && _barItemDic[name] != null)
                _barItemDic[name].Caption = text;
        }


        #endregion

        private void bbiAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[WriteOffCommands.Command_WriteOff_AddData_CR].Execute();
        }

        private void bbiAddDR_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[WriteOffCommands.Command_WriteOff_AddData_DR].Execute();
        }

        private void bbiNewInvoice_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[WriteOffCommands.Command_AddInvoice].Execute();
        }

        private void bbiListCredentials_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[WriteOffCommands.Command_ListCredentials].Execute();
        }

        private void bbiBullion_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[WriteOffCommands.Command_Bullion].Execute();
        }

        private void bbiCancelBullion_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[WriteOffCommands.Command_CancelBullion].Execute();
        }

        private void bbiAllowMultiSelect_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[WriteOffCommands.Command_AllowMultiSelection].Execute();
        }

        public void ForMultiView(bool forMultiView)
        {
            bbiBullion.Visibility = forMultiView ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiCancelBullion.Visibility = bbiBullion.Visibility;
            bbiAudit.Visibility = bbiBullion.Visibility;
            bbiCancelAudit.Visibility = bbiBullion.Visibility;

            barAllCheck.Visibility = forMultiView ? BarItemVisibility.Always : BarItemVisibility.Never;
        }

        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().Close();
        }

        private void barShoeSearch_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[WriteOffCommands.Command_ShowSearch].Execute();
        }

        private void bbiAudit_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[WriteOffCommands.Command_Auditor].Execute();
        }

        private void bbiCancelAudit_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[WriteOffCommands.Command_UnAuditor].Execute();
        }

        private void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[WriteOffCommands.Command_DeleteData].Execute();
        }

        private void barEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[WriteOffCommands.Command_EditData].Execute();
        }

        private void barPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[WriteOffCommands.Command_PrintCheck].Execute();
        }

        private void barAllCheck_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[WriteOffCommands.Command_AllCheck].Execute();
        }

        private void barVoid_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[WriteOffCommands.Command_VoidData].Execute();
        }

        private void btnCredentialsPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[WriteOffCommands.Command_CredentialsPrint].Execute();
        }

        private void barUntieLock_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[WriteOffCommands.Command_UntieLock].Execute();
        }
        private void barDirectBank_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[WriteOffCommands.Command_DirectBank].Execute();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                //解锁权限
                if (LocalCommonServices.PermissionService.HaveActionPermission(ActionsConstants.FAM_CHECK_UntieLock))
                {
                    barUntieLock.Visibility = BarItemVisibility.Always;
                }
                else
                {
                    barUntieLock.Visibility = BarItemVisibility.Never;
                }
                //直连支付权限
                if (LocalCommonServices.PermissionService.HaveActionPermission(ActionsConstants.FAM_DirectBankPayment))
                {
                    barDirectBank.Visibility = BarItemVisibility.Always;
                }
                else
                {
                    barDirectBank.Visibility = BarItemVisibility.Never;
                }
            }
        }

        
    }
}
