using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraBars;

namespace ICP.FCM.DomesticTrade.UI.Booking
{
    [ToolboxItem(false)]
    public partial class BookingToolBar : ICP.Framework.ClientComponents.UIFramework.BaseToolBar
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();

        public BookingToolBar()
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


            barAdd.Caption="新增(&A)";
            barCopy.Caption="复制(&O)";
            barCancel.Caption = "取消(&D)";
            barEdit.Caption="编辑(&E)";
            this.barSubPrint.Caption = "打印(&P)";
            barTruck.Caption="派车(&T)";
            barReplyAgent.Caption="申请代理(&G)";
            barE_Booking.Caption="电子订舱(&B)";
            barBL.Caption= "提单(&L)";
            barBill.Caption="帐单(&B)";
            barClose.Caption = "关闭(&C)";
            barLoadContainer.Caption = "装箱";
            barRefresh.Caption = "刷新(&R)";
            barShowSearch.Caption = "查询(&H)";
            barRefresh.Hint = "刷新(R)";
            barShowSearch.Hint = "查询(H)";

            barPrintOrder .Caption = "业务联单";
            barPrintBookingConfirm.Caption = "订舱确认书";
            barPrintInWarehouse.Caption = "入仓单";
        }

        private void BulidCommond()
        {
            barAdd.ItemClick += delegate { Workitem.Commands[DTBookingCommandConstants.Command_AddData].Execute(); };
            barCopy.ItemClick += delegate { Workitem.Commands[DTBookingCommandConstants.Command_CopyData].Execute(); };
            barEdit.ItemClick += delegate { Workitem.Commands[DTBookingCommandConstants.Command_EditData].Execute(); };
            barCancel.ItemClick += delegate { Workitem.Commands[DTBookingCommandConstants.Command_CancelData].Execute(); };
            barTruck.ItemClick += delegate { Workitem.Commands[DTBookingCommandConstants.Command_Truck].Execute(); };
            barReplyAgent.ItemClick += delegate { Workitem.Commands[DTBookingCommandConstants.Command_ReplyAgent].Execute(); };
            barE_Booking.ItemClick += delegate { Workitem.Commands[DTBookingCommandConstants.Command_E_Booking].Execute(); };
            barBL.ItemClick += delegate { Workitem.Commands[DTBookingCommandConstants.Command_BL].Execute(); };
            barBill.ItemClick += delegate { Workitem.Commands[DTBookingCommandConstants.Command_Bill].Execute(); };
            barShowSearch.ItemClick += delegate { Workitem.Commands[DTBookingCommandConstants.Command_ShowSearch].Execute(); };
            barLoadContainer.ItemClick += delegate { Workitem.Commands[DTBookingCommandConstants.Command_LoadContainer].Execute(); };
            this.barRefresh.ItemClick += new ItemClickEventHandler(barRefresh_ItemClick);
            barPrintOrder.ItemClick += delegate { Workitem.Commands[DTBookingCommandConstants.Command_PrintOrder].Execute(); };
            barPrintBookingConfirm.ItemClick += delegate { Workitem.Commands[DTBookingCommandConstants.Command_PrintBookingConfirm].Execute(); };
            barPrintInWarehouse.ItemClick += delegate { Workitem.Commands[DTBookingCommandConstants.Command_PrintInWarehouse].Execute(); };

            barClose.ItemClick += delegate { this.FindForm().Close(); };
        }

        void barRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Workitem.Commands[DTBookingCommandConstants.Command_RefreshData].Execute();
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
