using System.Collections.Generic;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraBars;

namespace ICP.FAM.UI
{
    public partial class GLCodeToolPart : BaseToolBar
    {
        public GLCodeToolPart()
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

            BulidBarItemDictionary();
            BulidCommond();
        }
        #region 服务
        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();

        private void BulidBarItemDictionary()
        {
            foreach (BarItem item in barManager1.Items)
            {
                _barItemDic.Add(item.Name, item);
            }
        }
        #endregion

        #region  注册命令
        /// <summary>
        /// 注册命令
        /// </summary>
        private void BulidCommond()
        {
            barAdd.ItemClick += delegate { Workitem.Commands[GLCodeCommandConstants.Command_GLCodeAdd].Execute(); };//新增
            barEdit.ItemClick += delegate { Workitem.Commands[GLCodeCommandConstants.Command_GLCodeEdit].Execute(); };//编辑
            barInvalid.ItemClick += delegate { Workitem.Commands[GLCodeCommandConstants.Command_GLCodeCancel].Execute(); };//作废
            barSearch.ItemClick += delegate { Workitem.Commands[GLCodeCommandConstants.Command_GLCodeShowSearch].Execute(); };//查询
            barTo.ItemClick += delegate { Workitem.Commands[GLCodeCommandConstants.Command_GLCodeTo].Execute(); };//导出
            barCompany.ItemClick += delegate { Workitem.Commands[GLCodeCommandConstants.Command_GLCompany].Execute(); };//所属公司
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

        #region 关闭
        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().Close();
        }
        #endregion

    }
}
