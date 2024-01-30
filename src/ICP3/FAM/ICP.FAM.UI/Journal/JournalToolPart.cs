using System.Collections.Generic;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using DevExpress.XtraBars;
using Microsoft.Practices.CompositeUI;

namespace ICP.FAM.UI
{
    [ToolboxItem(false)]
    public partial class JournalToolPart : BaseToolBar
    {   
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public JournalToolPart()
        {
            InitializeComponent();
            BulidBarItemDictionary();
            BulidCommond();
            Disposed += delegate
            {
                _barItemDic.Clear();
                _barItemDic = null;
                if (WorkItem != null)
                {
                    WorkItem.Items.Remove(this);
                    WorkItem = null;
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
        /// 服务
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        /// <summary>
        /// 注册命令
        /// </summary>
        private void BulidCommond()
        {
            barAdd1.ItemClick += delegate { Workitem.Commands[JournalCommandConstants.Command_JournalAdd].Execute(); };//新增
            barEdit.ItemClick += delegate { Workitem.Commands[JournalCommandConstants.Command_JournalEdit].Execute(); };//编辑
            barDelete.ItemClick += delegate { Workitem.Commands[JournalCommandConstants.Command_JournalCancel].Execute(); };//作废
            barSearch.ItemClick += delegate { Workitem.Commands[JournalCommandConstants.Command_JournalShowSearch].Execute(); };//查询
            barClose.ItemClick += delegate { FindForm().Close(); };//关闭
        }
        #region IToolBar成员

        public override void SetEnable(string name, bool enable)
        {
            if (_barItemDic.ContainsKey(name) && _barItemDic[name] != null)
                _barItemDic[name].Enabled =enable;
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

    }
}
