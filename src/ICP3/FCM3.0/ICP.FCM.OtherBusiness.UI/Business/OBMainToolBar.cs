using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI.Utility;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.Controls;
using ICP.FCM.Common.ServiceInterface.Common;
namespace ICP.FCM.OtherBusiness.UI.Business
{
    [ToolboxItem(false)]
    public partial class OBMainToolBar : BaseToolBar
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion



        public AddType addType
        {
            get;
            set;
        }

        protected virtual bool IsHidebarVerifiSheet { get { return false; } }

        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();
        [Microsoft.Practices.CompositeUI.EventBroker.EventPublication("AddData")]
        public event EventHandler<DataEventArgs<AddType>> AddEvent;
        public OBMainToolBar()
        {
            InitializeComponent();
            this.Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();

            BulidBarItemDictionary();
            BulidCommond();

            if (IsHidebarVerifiSheet)
            {
                barVerifiSheet.Visibility = BarItemVisibility.Never;
                barPickUp.Visibility = BarItemVisibility.Never;
            }
            else
            {
                barVerifiSheet.Visibility = BarItemVisibility.Always;
                barPickUp.Visibility = BarItemVisibility.Always;
            }

            if (addType == AddType.OtherBusinessOrder)
            {
                SetPermissions();
            }
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
                this.barBill.Visibility = BarItemVisibility.Never;
                this.barVerifiSheet.Visibility = BarItemVisibility.Never;
                this.barPickUp.Visibility = BarItemVisibility.Never;
                this.barRemark.Visibility = BarItemVisibility.Never;
            }
        }
        private void SetCnText()
        {
            barAdd.Caption = "新增";
            barEdit.Caption = "编辑";
            barDownload.Caption = "下载";
            barClose.Caption = "关闭";
            barRefresh.Caption = "刷新";
            barFaxLog.Caption = "传真日志";
            barPrint.Caption = "打印";
            barRemark.Caption = "备注";
            barBill.Caption = "账单";
            barCancel.Caption = "作废";
            barSearch.Caption = "查询";
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
            barAdd.ItemClick += delegate
            {
                AddType type = AddType.OtherBusinessOrder;
                //其他业务订单新增
                if (!IsHidebarVerifiSheet)
                {
                    type = AddType.OtherBusiness;

                }
                Workitem.RootWorkItem.State["AddData"] = type;
                Workitem.Commands[OrderCommandConstants.Command_AddOtherData].Execute();
            };
            barRefresh.ItemClick += delegate { Workitem.Commands[OrderCommandConstants.Command_RefreshData].Execute(); };
            barEdit.ItemClick += delegate { Workitem.Commands[OrderCommandConstants.Command_EditData].Execute(); };
            barCopy.ItemClick += delegate { Workitem.Commands[OrderCommandConstants.Command_CopyData].Execute(); };
            barCancel.ItemClick += delegate { Workitem.Commands[OrderCommandConstants.Command_CancelData].Execute(); };
            barOpContact.ItemClick += delegate { Workitem.Commands[OrderCommandConstants.Command_OpContact].Execute(); };
            barProfit.ItemClick += delegate { Workitem.Commands[OrderCommandConstants.Command_Profit].Execute(); };
            barSearch.ItemClick += delegate { Workitem.Commands[OrderCommandConstants.Command_ShowSearch].Execute(); };
            barBill.ItemClick += delegate { Workitem.Commands[OrderCommandConstants.Command_Bill].Execute(); };
            if (!IsHidebarVerifiSheet)
            {
                barVerifiSheet.ItemClick += delegate { Workitem.Commands[OrderCommandConstants.Command_VerifiSheet].Execute(); };
            }

            barPickUp.ItemClick += delegate { Workitem.Commands[OrderCommandConstants.Command_PickUp].Execute(); };  
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

    public class OBOrderMainToolBar : OBMainToolBar
    {
        protected override bool IsHidebarVerifiSheet { get { return true; } }
    }
}
