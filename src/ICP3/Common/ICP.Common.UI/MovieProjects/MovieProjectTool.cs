using System.Collections.Generic;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using DevExpress.XtraBars;
using Microsoft.Practices.CompositeUI;

namespace ICP.Common.UI
{
    [ToolboxItem(false)]
    public partial class MovieProjectTool : BaseToolBar
    {
        public MovieProjectTool()
        {
            InitializeComponent();

            BulidBarItemDictionary();

            BulidCommond();
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
        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        /// <summary>
        /// 注册命令
        /// </summary>
        private void BulidCommond()
        {

            barNew.ItemClick += delegate { Workitem.Commands[MovieProjectCommandConstants.Command_MovieProjectAdd].Execute(); };
            barSearch.ItemClick += delegate { Workitem.Commands[MovieProjectCommandConstants.Command_MovieProjectShowSearch].Execute(); };
         
            barClose.ItemClick += delegate
            {
                var findForm = this.FindForm();
                if (findForm != null) findForm.Close();
            };
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

        private void barCancel_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Workitem.Commands[MovieProjectCommandConstants.Command_MovieProjectCancel].Execute();
        }
    }
}
