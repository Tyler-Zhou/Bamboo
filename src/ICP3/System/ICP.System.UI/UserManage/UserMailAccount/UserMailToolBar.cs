using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraBars;

namespace ICP.Sys.UI.UserManage.UserMailAccount
{
    [ToolboxItem(false)]
    public partial class UserMailToolBar : ICP.Framework.ClientComponents.UIFramework.BaseToolBar
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region init

        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();

        public UserMailToolBar()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.barDefault.ItemClick -= this.barDefault_ItemClick;
                this.barNew.ItemClick -= this.barNew_ItemClick;
                this.barRemove.ItemClick -= this.barRemove_ItemClick;
                this.barSave.ItemClick -= this.barSave_ItemClick;
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
            barNew.Caption = "新增(&N)";
            barSave.Caption = "保存(&S)";
            barRemove.Caption = "删除(&R)";
            barDefault.Caption = "设为默认(&F)";
        }

        #endregion

        #region barItem

        private void barNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Workitem.Commands[UserCommonConstants.Common_MailAddData].Execute();
        }

        private void barRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Workitem.Commands[UserCommonConstants.Common_MailDeleteData].Execute();
        }

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Workitem.Commands[UserCommonConstants.Command_MailSaveData].Execute();
        }

        private void barDefault_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Workitem.Commands[UserCommonConstants.Command_MailSetDefault].Execute();
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
