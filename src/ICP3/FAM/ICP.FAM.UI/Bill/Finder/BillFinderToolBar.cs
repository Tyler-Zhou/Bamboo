using System.Collections.Generic;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using DevExpress.XtraBars;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;

namespace ICP.FAM.UI.Bill.Finder
{
    [ToolboxItem(false)]
    public partial class BillFinderToolBar :  BaseToolBar
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        #endregion

        #region 初始化

        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();

        public BillFinderToolBar()
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
            if (LocalData.IsEnglish == false) SetCnText();

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

        private void SetCnText()
        {
           
        }

        private void BulidCommond()
        {
            barConfirm.ItemClick += delegate { Workitem.Commands[BillCommandConstants.Command_FinderConfirm].Execute(); };
            barSelectAll.ItemClick += delegate { Workitem.Commands[BillCommandConstants.Command_FinderSelectAll].Execute(); };
            barClearAll.ItemClick += delegate { Workitem.Commands[BillCommandConstants.Command_FinderClearAll].Execute(); };
            barClose.ItemClick += delegate { FindForm().Close(); };
        }

        #endregion

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

        private void barConfirm_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
