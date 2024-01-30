using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraBars;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;

namespace ICP.Business.Common.UI.QuotedPrice
{
    /// <summary>
    /// 报价工具栏
    /// </summary>
    [ToolboxItem(false)]
    public partial class QuotedPriceToolBar : BaseToolBar
    {
        #region Service & Property
        /// <summary>
        /// 按钮目录
        /// </summary>
        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        #endregion

        #region Init
        /// <summary>
        /// 报价工具栏
        /// </summary>
        public QuotedPriceToolBar()
        {
            InitializeComponent();
            BulidBarItemDictionary();
            BulidCommand();
            Disposed += (sender, arg) =>
            {
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
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

        #region Custom Method
        private void BulidBarItemDictionary()
        {
            foreach (BarItem item in barManager1.Items)
            {
                _barItemDic.Add(item.Name, item);
            }
        }
        /// <summary>
        /// 注册按钮事件
        /// </summary>
        private void BulidCommand()
        {
            barAdd.ItemClick += delegate {Workitem.Commands[QPCommandConstants.Command_AddData].Execute();};
            barEdit.ItemClick += delegate { Workitem.Commands[QPCommandConstants.Command_EditData].Execute(); };
            barDelete.ItemClick += delegate { Workitem.Commands[QPCommandConstants.Command_DeleteData].Execute(); };
            barCopy.ItemClick += delegate { Workitem.Commands[QPCommandConstants.Command_CopyData].Execute(); };
            barPrint.ItemClick += delegate { Workitem.Commands[QPCommandConstants.Command_PrintPrice].Execute(); };
            barRefresh.ItemClick += delegate { Workitem.Commands[QPCommandConstants.Command_RefreshData].Execute(); };
            barClose.ItemClick += delegate { var findForm = FindForm(); if (findForm != null) findForm.Close(); };
        }
        #endregion
    }
}
