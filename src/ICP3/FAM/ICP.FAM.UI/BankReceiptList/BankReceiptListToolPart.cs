using System.Collections.Generic;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using DevExpress.XtraBars;
using Microsoft.Practices.CompositeUI;

namespace ICP.FAM.UI.BankReceiptList
{
    [ToolboxItem(false)]
    public partial class BankReceiptListToolPart : BaseToolBar
    {
        [ServiceDependency]
        public WorkItem workItem { get; set; }

        public BankReceiptListToolPart()
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
            barAdd.ItemClick += delegate { workItem.Commands[BankReceiptListCommandConstants.Command_Add].Execute(); };
            barEdit.ItemClick += delegate { workItem.Commands[BankReceiptListCommandConstants.Command_Edit].Execute(); };
            barDelete.ItemClick += delegate { workItem.Commands[BankReceiptListCommandConstants.Command_Cancel].Execute(); };
            barSearch.ItemClick += delegate { workItem.Commands[BankReceiptListCommandConstants.CommandShowSearch].Execute(); };
            bbiAudit.ItemClick += delegate { workItem.Commands[BankReceiptListCommandConstants.Command_Auditor].Execute(); };
            bbiCancelAudit.ItemClick += delegate { workItem.Commands[BankReceiptListCommandConstants.Command_UnAuditor].Execute(); };
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
