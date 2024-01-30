using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraBars;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.AirImport.UI
{
    [ToolboxItem(false)]
    public partial class OIBusinessTool : ICP.Framework.ClientComponents.UIFramework.BaseToolBar
    {
        public OIBusinessTool()
        {
            InitializeComponent();

            BulidBarItemDictionary();

            BulidCommond();
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
            barSearch.ItemClick += delegate { Workitem.Commands[AIBusinessCommandConstants.Command_ShowSearch].Execute(); };

            barAdd.ItemClick += delegate { Workitem.Commands[AIBusinessCommandConstants.Command_AddData].Execute(); };
            barCopy.ItemClick += delegate { Workitem.Commands[AIBusinessCommandConstants.Command_CopyData].Execute(); };
            barEdit.ItemClick += delegate { Workitem.Commands[AIBusinessCommandConstants.Command_EditData].Execute(); };
            barCancel.ItemClick += delegate { Workitem.Commands[AIBusinessCommandConstants.Command_CancelData].Execute(); };
            barRefresh.ItemClick += delegate { Workitem.Commands[AIBusinessCommandConstants.Command_RefreshData].Execute(); };          
            barDownload.ItemClick += delegate { Workitem.Commands[AIBusinessCommandConstants.Command_DownLoad].Execute(); };
            barBusinessTransfer.ItemClick += delegate { Workitem.Commands[AIBusinessCommandConstants.Command_BusinessTransfer].Execute(); };
            barCargoBook.ItemClick += delegate { Workitem.Commands[AIBusinessCommandConstants.Command_CargoBook].Execute(); };
            barBoxTracking.ItemClick += delegate { Workitem.Commands[AIBusinessCommandConstants.Command_BoxTracking].Execute(); };

            barDelivery.ItemClick += delegate { Workitem.Commands[AIBusinessCommandConstants.Command_Delivery].Execute(); };
            barBill.ItemClick += delegate { Workitem.Commands[AIBusinessCommandConstants.Command_Bill].Execute(); };             
            barPrintArrivalNotice.ItemClick += delegate { Workitem.Commands[AIBusinessCommandConstants.Command_PrintArrivalNotice].Execute(); };
            barReleaseOrder.ItemClick += delegate { Workitem.Commands[AIBusinessCommandConstants.Command_PrintReleaseOrder].Execute(); };
            barPrintProfit.ItemClick += delegate { Workitem.Commands[AIBusinessCommandConstants.Command_PrintProfit].Execute(); };
            barPrintAuthority.ItemClick += delegate { Workitem.Commands[AIBusinessCommandConstants.Command_PrintAuthority].Execute(); };
            
            barClose.ItemClick += delegate { this.FindForm().Close(); };
        }


        /// <summary>
        /// 设置取消放货
        /// </summary>
        public void SetCancelDelivery(bool isCancel)
        {
            if (isCancel)
            {
                this.barDelivery.Caption = LocalData.IsEnglish ? "Cancel Release" : "取消放货";
                this.barDelivery.Glyph = global::ICP.FCM.AirImport.UI.Properties.Resources.Return_16;
            }
            else
            {
                this.barDelivery.Caption = LocalData.IsEnglish ? "Release" : "放货";
                this.barDelivery.Glyph = global::ICP.FCM.AirImport.UI.Properties.Resources.Check_16;
            }
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
