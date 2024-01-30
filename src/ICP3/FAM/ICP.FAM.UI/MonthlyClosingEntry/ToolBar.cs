using System.Collections.Generic;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraBars;

namespace ICP.FAM.UI.MonthlyClosingEntry
{
    public partial class ToolBar : BaseToolBar
    {

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public ToolBar()
        {
            InitializeComponent();
            BulidBarItemDictionary();
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

        private void bbiNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[EntryCommondConstants.Commond_Add].Execute();
        }

        private void bbiEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[EntryCommondConstants.Commond_Edit].Execute();
        }

        private void bbiDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[EntryCommondConstants.Commond_Delete].Execute();
        }

        private void bbiClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().Close();
        }

        private void bbiRecycle_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[EntryCommondConstants.Commond_Delete].Execute();
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

        private void barShowSearch_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[EntryCommondConstants.Command_ShowSearch].Execute();
        }
    }
}
