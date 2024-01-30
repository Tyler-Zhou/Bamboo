using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraBars;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.Common.ServiceInterface.Common;

namespace ICP.FCM.OceanExport.UI.Order
{   
    /// <summary>
    /// 订单工具栏
    /// </summary>
    [ToolboxItem(false)]
    public partial class OEOrderToolBar : ICP.Framework.ClientComponents.UIFramework.BaseToolBar
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();

        public OEOrderToolBar()
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
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false)
            {
                //SetCnText();
            }
            BulidBarItemDictionary();
            BulidCommond();
            SetPermissions();
        }
        /// <summary>
        /// 设置权限
        /// </summary>
        private void SetPermissions()
        {
            if (!ICP.Framework.CommonLibrary.Client.LocalCommonServices.PermissionService.HaveActionPermission(FCMPermissionsConstants.FCM_EditOrder))
            {
                this.barAdd.Visibility = BarItemVisibility.Never;
                this.barCopy.Visibility = BarItemVisibility.Never;
                this.barCancel.Visibility = BarItemVisibility.Never;
            }
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
            //barAdd.se
            barAdd.Caption = "新增(&A)";
            barCancel.Caption = "取消(&D)";
            barCopy.Caption = "复制(&O)";
            barPrint.Caption = "打印(&P)";
            barSearch.Caption = "查询(&H)";
            barEdit.Caption = "编辑(&E)";

            barSearch.Hint = "查询(H)";
            barRefresh.Hint = "刷新(R)";
            //barViewReason.Hint = "查看打回原因(&V)";

            barClose.Caption = "关闭(&C)";

        }

        private void BulidCommond()
        {
            barAdd.ItemClick += delegate { Workitem.Commands[OEOrderCommandConstants.Command_AddData].Execute(); };
            barCopy.ItemClick += delegate { Workitem.Commands[OEOrderCommandConstants.Command_CopyData].Execute(); };
            barEdit.ItemClick += delegate { Workitem.Commands[OEOrderCommandConstants.Command_EditData].Execute(); };
            barCancel.ItemClick += delegate { Workitem.Commands[OEOrderCommandConstants.Command_CancelData].Execute(); };
            barPrint.ItemClick += delegate { Workitem.Commands[OEOrderCommandConstants.Command_Print].Execute(); };
            barSearch.ItemClick += delegate { Workitem.Commands[OEOrderCommandConstants.Command_ShowSearch].Execute(); };
            //barViewReason.ItemClick += delegate { Workitem.Commands[OEOrderCommandConstants.Command_ViewReason].Execute(); };
            this.barRefresh.ItemClick += new ItemClickEventHandler(barRefresh_ItemClick);
            //barDocument.ItemClick += delegate { Workitem.Commands[OEOrderCommandConstants.Command_Document].Execute(); };
            //barFaxEmail.ItemClick += delegate { Workitem.Commands[OEOrderCommandConstants.Command_FaxEmail].Execute(); };
            //barMemo.ItemClick += delegate { Workitem.Commands[OEOrderCommandConstants.Command_Memo].Execute(); };
            barAgentFilerList.ItemClick += new ItemClickEventHandler(barAgentFilerList_ItemClick);

            barClose.ItemClick += delegate { this.FindForm().Close(); };
        }

        void barRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Workitem.Commands[OEOrderCommandConstants.Command_RefreshData].Execute();
        }
        void barAgentFilerList_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Workitem.Commands[OEOrderCommandConstants.Command_AgentFilerSearch].Execute();
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
