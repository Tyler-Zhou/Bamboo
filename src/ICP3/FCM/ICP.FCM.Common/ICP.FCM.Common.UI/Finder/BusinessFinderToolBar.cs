using System.Collections.Generic;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using DevExpress.XtraBars;
using Microsoft.Practices.CompositeUI;

namespace ICP.FCM.Common.UI.Finder
{
    [ToolboxItem(false)]
    public partial class BusinessFinderToolBar :  BaseToolBar
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        #endregion

        #region 初始化

        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();

        public BusinessFinderToolBar()
        {
            InitializeComponent();

            this.Disposed += delegate {
                this._barItemDic = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();

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
            barConfirm.ItemClick += delegate { Workitem.Commands[BusinessFinderConstants.Command_FinderConfirm].Execute(); };
           
            barClose.ItemClick += delegate { this.FindForm().Close(); };
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
    }
}
