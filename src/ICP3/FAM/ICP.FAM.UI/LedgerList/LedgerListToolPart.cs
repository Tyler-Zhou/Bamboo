using System.Collections.Generic;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using DevExpress.XtraBars;
using Microsoft.Practices.CompositeUI;

namespace ICP.FAM.UI
{
    [ToolboxItem(false)]
    public partial class LedgerListToolPart : BaseToolBar
    {
        [ServiceDependency]
        public WorkItem workItem { get; set; }

        public LedgerListToolPart()
        {
            InitializeComponent();
            BulidBarItemDictionary();
            BulidCommond();
            Disposed += delegate {
                barItemDic.Clear();
                barItemDic = null;
                if (workItem != null)
                {
                    workItem.Items.Remove(this);
                    workItem = null;
                }
            
            };
        }

        Dictionary<string, BarItem> barItemDic = new Dictionary<string, BarItem>();

        private void BulidBarItemDictionary()
        {
            foreach (BarItem item in barManager1.Items)
            {
                barItemDic.Add(item.Name, item);
            }
        }

        /// <summary>
        /// 注册命令
        /// </summary>
        private void BulidCommond()
        {
            barAdd.ItemClick += delegate { workItem.Commands[LedgerListCommandConstants.Command_Add].Execute(); };
            barEdit.ItemClick += delegate { workItem.Commands[LedgerListCommandConstants.Command_Edit].Execute(); };
            barDelete.ItemClick += delegate { workItem.Commands[LedgerListCommandConstants.Command_Cancel].Execute(); };
            btnCashierChecked.ItemClick += delegate { workItem.Commands[LedgerListCommandConstants.Command_Cashier].Execute(); };
            btnCashierUnChecked.ItemClick += delegate { workItem.Commands[LedgerListCommandConstants.Command_Cashier].Execute(); };
            btnFMChecked.ItemClick += delegate { workItem.Commands[LedgerListCommandConstants.Command_FM].Execute(); };
            btnFMUnChecked.ItemClick += delegate { workItem.Commands[LedgerListCommandConstants.Command_FM].Execute(); };
            btnAduited.ItemClick += delegate { workItem.Commands[LedgerListCommandConstants.Command_Aduitor].Execute(); };
            btnUnAduited.ItemClick += delegate { workItem.Commands[LedgerListCommandConstants.Command_Aduitor].Execute(); };
            btnKeepAccounts.ItemClick += delegate { workItem.Commands[LedgerListCommandConstants.Command_KeepAccounts].Execute(); };
            btnCancelAccounts.ItemClick += delegate { workItem.Commands[LedgerListCommandConstants.Command_KeepAccounts].Execute(); };
            barSearch.ItemClick += delegate { workItem.Commands[LedgerListCommandConstants.CommandShowSearch].Execute(); };
            barPrint.ItemClick += delegate { workItem.Commands[LedgerListCommandConstants.Command_Print].Execute(); };
            barBulkPrint.ItemClick += delegate { workItem.Commands[LedgerListCommandConstants.Command_BulkPrint].Execute(); };
            barBillVoucher.ItemClick += delegate { workItem.Commands[LedgerListCommandConstants.Command_BillVoucher].Execute(); };
            barReorganize.ItemClick += delegate { workItem.Commands[LedgerListCommandConstants.Command_Reorganize].Execute(); };
            barAdjustRate.ItemClick += delegate { workItem.Commands[LedgerListCommandConstants.Command_AdjustRate].Execute(); };

            barClose.ItemClick += delegate { FindForm().Close(); };

        }

        #region IToolBar成员

        public override void SetEnable(string name, bool enable)
        {
            if (barItemDic.ContainsKey(name) && barItemDic[name] != null)
                barItemDic[name].Enabled = enable;
        }

        public override void SetVisible(string name, bool visible)
        {
            if (barItemDic.ContainsKey(name) && barItemDic[name] != null)
                barItemDic[name].Visibility = visible ? BarItemVisibility.Always : BarItemVisibility.Never;
        }

        public override void SetText(string name, string text)
        {
            if (barItemDic.ContainsKey(name) && barItemDic[name] != null)
                barItemDic[name].Caption = text;
        }

        #endregion


    }
}
