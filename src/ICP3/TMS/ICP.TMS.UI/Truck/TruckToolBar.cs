using System.Collections.Generic;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraBars;

namespace ICP.TMS.UI
{
    /// <summary>
    /// 拖车资料工具栏
    /// </summary>
    [ToolboxItem(false)]
    public partial class TruckToolBar : BaseToolBar
    {

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public TruckToolBar()
        {
            InitializeComponent();
            BulidBarItemDictionary();
            this.Disposed += delegate
            {
                this._barItemDic.Clear();
                this._barItemDic = null;
                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
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

        private void bbiClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.FindForm().Close();
        }

        private void barShowSearch_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Workitem.Commands[TruckCommondConstants.Command_ShowSearch].Execute();
        }

        private void bbiNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Workitem.Commands[TruckCommondConstants.Commond_Add].Execute();
        }

        private void bbiRecycle_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Workitem.Commands[TruckCommondConstants.Commond_Delete].Execute();
        }

      
    }
}
