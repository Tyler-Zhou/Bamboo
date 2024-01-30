using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraBars;

namespace ICP.Sys.UI.UserManage
{
    public partial class UserToolBar : ICP.Framework.ClientComponents.UIFramework.BaseToolBar
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();

        public UserToolBar()
        {
            InitializeComponent();
            this.Disposed += delegate {
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
            barAdd.Caption = "新增(&A)";
            barDisuse.Caption = "作废(&D)";
            barClose.Caption = "关闭(&C)";
            barSearch.Caption = "查询(&H)";
        }

        #region barItem

        private void barDisuse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Workitem.Commands[UserCommonConstants.Common_DisuseData].Execute();
        }

        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Workitem.Commands[UserCommonConstants.Common_AddData].Execute();
        }

        private void barSearch_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[UserCommonConstants.Command_ShowSearch].Execute();
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
