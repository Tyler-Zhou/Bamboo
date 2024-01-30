using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraBars;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FRM.UI.ProfitRatios
{   
    /// <summary>
    /// 工具栏
    /// </summary>
    [ToolboxItem(false)]
    public partial class PRToolBar : BaseToolBar
    {
        #region Service
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();
        /// <summary>
        /// 
        /// </summary>
        public PRToolBar()
        {
            InitializeComponent();
            this.Disposed += delegate { 
                this._barItemDic.Clear();
                this._barItemDic=null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
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
            if (LocalCommonServices.PermissionService.HaveActionPermission(ProfitRatiosCommandConstants.Command_BusinessClosing))
            {
                barBusinessClosing.Visibility = BarItemVisibility.Always;
            }
            else
            {
                barBusinessClosing.Visibility = BarItemVisibility.Never;
            }
            barExport.ItemClick += delegate { Workitem.Commands[ProfitRatiosCommandConstants.Command_Export].Execute(); };
            barPrint.ItemClick += delegate { Workitem.Commands[ProfitRatiosCommandConstants.Command_Print].Execute(); };
            barSearch.ItemClick += delegate { Workitem.Commands[ProfitRatiosCommandConstants.Command_ShowSearch].Execute(); };
            barRefresh.ItemClick += delegate { Workitem.Commands[ProfitRatiosCommandConstants.Command_Refresh].Execute(); };
            barBusinessClosing.ItemClick += delegate { Workitem.Commands[ProfitRatiosCommandConstants.Command_BusinessClosing].Execute(); };
            barClose.ItemClick += delegate { this.FindForm().Close(); };
        }

        #region IToolBar成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="enable"></param>
        public override void SetEnable(string name, bool enable)
        {
            if (_barItemDic.ContainsKey(name) && _barItemDic[name] != null)
                _barItemDic[name].Enabled = enable;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="visible"></param>
        public override void SetVisible(string name, bool visible)
        {
            if (_barItemDic.ContainsKey(name) && _barItemDic[name] != null)
                _barItemDic[name].Visibility = visible ? BarItemVisibility.Always : BarItemVisibility.Never;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        public override void SetText(string name, string text)
        {
            if (_barItemDic.ContainsKey(name) && _barItemDic[name] != null)
                _barItemDic[name].Caption = text;
        }
        #endregion
    }
}
