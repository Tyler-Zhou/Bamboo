using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraBars;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.Business.Common.UI.CSPBooking
{
    /// <summary>
    /// 工具栏面板
    /// </summary>
    [ToolboxItem(false)]
    public partial class PartToolBar : BaseToolBar
    {
        #region Service
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;
        }
        #endregion

        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();

        public PartToolBar()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this._barItemDic.Clear();
                this._barItemDic = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    this.Workitem = null;
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

            barRefresh.ItemClick += delegate { Workitem.Commands[CSPBKConstants.COMMAND_REFRESH].Execute(); };
            barDownload.ItemClick += delegate { Workitem.Commands[CSPBKConstants.COMMAND_DOWNLOAD].Execute(); };

            barClose.ItemClick += delegate { this.FindForm().Close(); };

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


    }
}
