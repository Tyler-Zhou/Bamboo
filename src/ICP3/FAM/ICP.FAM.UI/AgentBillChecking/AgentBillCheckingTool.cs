using System.Collections.Generic;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraBars;

namespace ICP.FAM.UI
{
    public partial class AgentBillCheckingTool : BaseToolBar
    {
        public AgentBillCheckingTool()
        {
            InitializeComponent();
            BulidBarItemDictionary();
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

        #region  IToolBar
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

        #region 服务
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        #endregion

        #region 事件
        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().Close();
        }

        private void barAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[ABCCommandConstants.Command_ABCAdd].Execute();
        }

        private void barOpen_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[ABCCommandConstants.Command_ABCOpen].Execute();
        }

        private void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[ABCCommandConstants.Command_ABCDelete].Execute();
        }

        private void barSearch_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[ABCCommandConstants.Command_ABCShowSearch].Execute();
        }
        #endregion


    }
}
