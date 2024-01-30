using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraBars;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.Common.ServiceInterface.Common;

namespace ICP.FCM.OceanImport.UI
{
    [ToolboxItem(false)]
    public partial class OIOrderToolBar : ICP.Framework.ClientComponents.UIFramework.BaseToolBar
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();

        public OIOrderToolBar()
        {
            InitializeComponent();
            this.Disposed += delegate 
            {
                _barItemDic.Clear();
                _barItemDic = null;
                if (Workitem!=null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
          

            
            };
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();

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
                this.barConfirmBooking.Visibility = BarItemVisibility.Never;
                this.barConfirmBookingShip.Visibility = BarItemVisibility.Never;
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

        }
        /// <summary>
        /// 注册命令
        /// </summary>
        private void BulidCommond()
        {
            barAdd.ItemClick += delegate { Workitem.Commands[OIOrderCommandConstants.Command_AddData].Execute(); };
            barCopy.ItemClick += delegate { Workitem.Commands[OIOrderCommandConstants.Command_CopyData].Execute(); };
            barEdit.ItemClick += delegate { Workitem.Commands[OIOrderCommandConstants.Command_EditData].Execute(); };
            barCancel.ItemClick += delegate { Workitem.Commands[OIOrderCommandConstants.Command_CancelData].Execute(); };
            //barPrint.ItemClick += delegate { Workitem.Commands[OIOrderCommandConstants.Command_Print].Execute(); };
            barPritnOrder.ItemClick += delegate { Workitem.Commands[OIOrderCommandConstants.Command_Print].Execute(); };         
            barSearch.ItemClick += delegate { Workitem.Commands[OIOrderCommandConstants.Command_ShowSearch].Execute(); };
            barConfirmBooking.ItemClick += delegate { Workitem.Commands[OIOrderCommandConstants.Command_ConfirmBooking].Execute(); };
            barConfirmBookingShip.ItemClick += delegate { Workitem.Commands[OIOrderCommandConstants.Command_ConfirmBookingShip].Execute(); };        
            barClose.ItemClick += delegate { this.FindForm().Close(); };
            barRefresh.ItemClick += delegate { this.Workitem.Commands[OIOrderCommandConstants.Command_RefreshData].Execute(); };
            //barFaxEmail.ItemClick += delegate { Workitem.Commands[OEOrderCommandConstants.Command_FaxEmail].Execute(); };
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
