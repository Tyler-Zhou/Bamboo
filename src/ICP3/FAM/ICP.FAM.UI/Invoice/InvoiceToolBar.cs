using System.Collections.Generic;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using DevExpress.XtraBars;
using Microsoft.Practices.CompositeUI;

namespace ICP.FAM.UI
{
    [ToolboxItem(false)]
    public partial class InvoiceToolBar : BaseToolBar
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        public InvoiceToolBar()
        {
            InitializeComponent();
            BulidBarItemDictionary();
            BulidCommond();
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
        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();

        private void BulidBarItemDictionary()
        {
            foreach (BarItem item in barManager1.Items)
            {
                _barItemDic.Add(item.Name, item);
            }
        }
        /// <summary>
        /// 注册命令
        /// </summary>
        private void BulidCommond()
        {
            barAdd.ItemClick += delegate { Workitem.Commands[InvoiceCommandConstants.Command_InvoiceAdd].Execute(); };
            barEdit.ItemClick+=delegate{Workitem.Commands[InvoiceCommandConstants.Command_InvoiceEdit].Execute();};
            barCancel.ItemClick += delegate { Workitem.Commands[InvoiceCommandConstants.Command_InvoiceCancel].Execute(); };
            barShowSearch.ItemClick += delegate { Workitem.Commands[InvoiceCommandConstants.Command_InvoiceShowSearch].Execute(); };
            barRefresh.ItemClick += delegate { Workitem.Commands[InvoiceCommandConstants.Command_InvoiceRefreshData].Execute(); };
            barExpress.ItemClick += delegate { Workitem.Commands[InvoiceCommandConstants.Command_Express].Execute(); };
            barPrintInvoice.ItemClick += delegate { Workitem.Commands[InvoiceCommandConstants.Command_PrintInvoice].Execute(); };
            barPrintVientam.ItemClick += delegate { Workitem.Commands[InvoiceCommandConstants.Command_PrintVientam].Execute(); };
            barPreview1.ItemClick += delegate { Workitem.Commands[InvoiceCommandConstants.Command_PreviewInvoice].Execute(); };
            barInvoiceCount.ItemClick += delegate { Workitem.Commands[InvoiceCommandConstants.Command_InvoiceCount].Execute(); };
            barGetInvoiceNo.ItemClick += delegate { Workitem.Commands[InvoiceCommandConstants.Command_GetInvoiceNo].Execute(); };
            barDutyFreeDetail.ItemClick += delegate { Workitem.Commands[InvoiceCommandConstants.Command_DutyFreeDetail].Execute(); };
            barOperationInvoice.ItemClick += delegate { Workitem.Commands[InvoiceCommandConstants.Command_OperationInvoice].Execute(); };
           
            
            barClose.ItemClick += delegate { FindForm().Close(); };
        }

        #region IEditPart成员

        //public override object DataSource
        //{
        //    get
        //    {
        //        return null;
        //    }
        //    set
        //    {
        //        this.BindingSource(value);
        //    }
        //}
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
        //private void BindingSource(object value)
        //{
        //    InvoiceList listData = value as InvoiceList;
        //    if (listData == null || listData.IsNew)
        //    {
        //        barEdit.Enabled = true;
        //        barCancel.Enabled = true;
        //        barExpress.Enabled =true;
        //        barPrint.Enabled = true;
        //    }
        //    else
        //    {
        //        barEdit.Enabled = true;
        //        barCancel.Enabled = true;
        //        barExpress.Enabled = true;
        //        barPrint.Enabled = true;

        //    }
        //}

        #endregion

    }
}
