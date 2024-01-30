using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraBars;

namespace ICP.Sys.UI.PermissionManage.Function
{
    public partial class FunctionToolBar : ICP.Framework.ClientComponents.UIFramework.BaseToolBar
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion
        #region init
        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();

        public FunctionToolBar()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.barClose.ItemClick -= this.barClose_ItemClick;
                this.barRefresh.ItemClick -= this.barRefresh_ItemClick;
                this._barItemDic.Clear();
                this._barItemDic = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();

            BulidBarItemDictionary();
            
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
            barRefresh.Caption = "刷新(&R)";
            barClose.Caption = "关闭(&C)";
        }

        #endregion

        #region barItem

        private void barRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[FunctionCommonConstants.Command_Refresh].Execute();
        }

        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.FindForm().Close();
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
