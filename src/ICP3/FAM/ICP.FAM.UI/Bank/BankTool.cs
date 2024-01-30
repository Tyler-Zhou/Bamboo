using System.Collections.Generic;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using DevExpress.XtraBars;
using Microsoft.Practices.CompositeUI;

namespace ICP.FAM.UI
{
    [ToolboxItem(false)]
    public partial class BankTool : BaseToolBar
    {
        public BankTool()
        {
            InitializeComponent();

            BulidBarItemDictionary();

            BulidCommond();
            Disposed += delegate
            {
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
        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        /// <summary>
        /// 注册命令
        /// </summary>
        private void BulidCommond()
        {
            
            barNew.ItemClick += delegate { Workitem.Commands[BankCommandConstants.Command_BankAdd].Execute(); };
            barEdit.ItemClick += delegate { Workitem.Commands[BankCommandConstants.Command_BankEdit].Execute(); };

            barSearch.ItemClick += delegate { Workitem.Commands[BankCommandConstants.Command_BankShowSearch].Execute(); };
         
            barClose.ItemClick += delegate { FindForm().Close(); };
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

        private void barCancel_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BankCommandConstants.Command_BankCancel].Execute();
        }
    }
}
