using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.ClientComponents.UIFramework;
using DevExpress.XtraBars;

namespace ICP.FAM.UI.Bill
{
    [ToolboxItem(false)]
    public partial class BillOperationToolBar : BaseToolBar
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        #endregion

        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();

        public BillOperationToolBar()
        {
            InitializeComponent();
            Disposed += delegate {
                _barItemDic.Clear();
                _barItemDic = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };

            BulidBarItemDictionary();
            BulidCommond();
        }

        private void BulidBarItemDictionary()
        {
            foreach (BarItem item in barManager1.Items)
            {
                _barItemDic.Add(item.Name, item);
            }
        }

        private void BulidCommond()
        {
            barWriteOff.ItemClick += delegate { Workitem.Commands[BillCommandConstants.Command_WriteOff].Execute(); };
            barMultiCurrencyWriteOff.ItemClick += delegate { Workitem.Commands[BillCommandConstants.Command_MultiCurrencyWriteOff].Execute(); };

            barAuditor.ItemClick += delegate { Workitem.Commands[BillCommandConstants.Command_Auditor].Execute(); };
            barUnAuditor.ItemClick += delegate { Workitem.Commands[BillCommandConstants.Command_UnAuditor].Execute(); };


            barPaymentRequest.ItemClick += delegate { Workitem.Commands[BillCommandConstants.Command_PaymentRequest].Execute(); };

            barInvoice.ItemClick += delegate { Workitem.Commands[BillCommandConstants.Command_AddInvoice].Execute(); };
            barInvoiceContract.ItemClick += delegate { Workitem.Commands[BillCommandConstants.Command_InvoiceContract].Execute(); };

            barRemove.ItemClick += delegate { Workitem.Commands[BillCommandConstants.Command_Remove].Execute(); };
            barClear.ItemClick += delegate { Workitem.Commands[BillCommandConstants.Command_Clear].Execute(); };
        }

        #region IToolBar成员

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


        private void barBusinessCost_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.State["BusinessCostRate"] = ((decimal)numoperateRate.EditValue) / 100;
            Workitem.Commands[BillCommandConstants.Command_BusinessCost].Execute();
        }
    }
}
