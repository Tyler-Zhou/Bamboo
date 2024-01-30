using System.Collections.Generic;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraBars;

namespace ICP.TMS.UI
{

    [ToolboxItem(false)]
    public partial class DriverToolBar : BaseToolBar
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region 初始
        public DriverToolBar()
        {
            InitializeComponent();
            BulidBarItemDictionary();
            this.Disposed += delegate {
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

        #region 按钮
        private void bbiNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Workitem.Commands[DriverCommondConstants.Commond_Add].Execute();
        }

        private void bbiRecycle_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Workitem.Commands[DriverCommondConstants.Commond_Delete].Execute();

        }

        private void bbiClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.FindForm().Close();
        }

        private void barShowSearch_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Workitem.Commands[DriverCommondConstants.Command_ShowSearch].Execute();
        }
        #endregion





    }
}
