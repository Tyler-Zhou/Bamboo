using System.Collections.Generic;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraBars;

namespace ICP.FAM.UI.GLCode.Finder
{
    [ToolboxItem(false)]
    public partial class MultiFinderSelectedToolBar : BaseToolBar
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region init

        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();

        public MultiFinderSelectedToolBar()
        {
            InitializeComponent();
            Disposed += delegate {
                _barItemDic.Clear();
                _barItemDic = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            if (LocalData.IsEnglish == false) SetCnText();

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
            barRemove.Caption = "移除(&D)";
            barRemoveAll.Caption = "移除全部(&A)";
            barConfirm.Caption = "确定(&O)";
        }

        #endregion

        #region barItem

        private void barConfirm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[GLCodeCommandConstants.Command_FinderConfirm].Execute();
            FindForm().Close();
        }

        private void barRemove_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[GLCodeCommandConstants.Command_FinderRemove].Execute();
        }

        private void barRemoveAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[GLCodeCommandConstants.Command_FinderRemoveAll].Execute();
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
