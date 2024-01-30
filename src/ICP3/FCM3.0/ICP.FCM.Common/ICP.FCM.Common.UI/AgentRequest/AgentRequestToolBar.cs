using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraBars;
using Microsoft.Practices.CompositeUI;

namespace ICP.FCM.Common.UI.AgentRequest
{
    [ToolboxItem(false)]
    public partial class AgentRequestToolBar : ICP.Framework.ClientComponents.UIFramework.BaseToolBar
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();

        public AgentRequestToolBar()
        {
            InitializeComponent();
            this.Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };
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
            barClose.Caption = "关闭(&C)";
            barSearch.Caption = "查询(&H)";
            barReject.Caption = "打回(&R)";
            barEdit.Caption = "指定代理(&A)";
        }

        private void BulidCommond()
        {
            barEdit.ItemClick += delegate { Workitem.Commands[OEAgentRequesCommandConstants.Command_Assign].Execute(); };
            barSearch.ItemClick += delegate { Workitem.Commands[OEAgentRequesCommandConstants.Command_ShowSearch].Execute(); };
            barReject.ItemClick += delegate { Workitem.Commands[OEAgentRequesCommandConstants.Command_Reject].Execute(); };
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
