using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraBars;

namespace ICP.FCM.OceanExport.UI.Booking
{
    /// <summary>
    /// 订舱单工具栏
    /// </summary>
    [ToolboxItem(false)]
    public partial class BookingToolBar : ICP.Framework.ClientComponents.UIFramework.BaseToolBar
    {
        #region Service

      
        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;
        }
        #endregion

        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();

        public BookingToolBar()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this._barItemDic.Clear();
                this._barItemDic = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
            };
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();

            BulidBarItemDictionary();
            BulidCommond();

            //if (ICP.Framework.CommonLibrary.Client.LocalData.UserInfo.DefaultCompanyID == new System.Guid("a62a9f8e-e69c-4e6e-ad85-e75aed3c6cf9"))
            //    barE_Booking.Visibility = BarItemVisibility.Never;
            //else
            //{
            //    barEdiNB.Visibility = BarItemVisibility.Never;
            //    barE_Booking.Visibility = BarItemVisibility.Always;
            //}  
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
            barCopy.Caption = "复制(&O)";
            barCancel.Caption = "取消(&D)";
            barEdit.Caption = "编辑(&E)";
            this.barSubPrint.Caption = "打印(&P)";
            barTruck.Caption = "派车(&T)";
            barReplyAgent.Caption = "申请代理(&G)";
            barE_Booking.Caption = "电子订舱(&B)";
            barBL.Caption = "提单(&L)";
            barBill.Caption = "帐单(&B)";
            barClose.Caption = "关闭(&C)";
            barLoadContainer.Caption = "装箱";
            barRefresh.Caption = "刷新(&R)";
            barShowSearch.Caption = "查询(&H)";
            barRefresh.Hint = "刷新(R)";
            barShowSearch.Hint = "查询(H)";

            barPrintOrder.Caption = "业务联单";
            barPrintBookingConfirm.Caption = "订舱确认书";
            barPrintInWarehouse.Caption = "入仓单";
            barCustoms.Caption = "报关委托";
            barEdiNB.Caption = "电子订舱(&B)";
            barBookingCon.Caption = "预配";
            barBooking.Caption = "订舱";
        }

        private void BulidCommond()
        {
            barAdd.ItemClick += delegate { Workitem.Commands[OEBookingCommandConstants.Command_AddData].Execute(); };
            barCopy.ItemClick += delegate { Workitem.Commands[OEBookingCommandConstants.Command_CopyData].Execute(); };
            barCopyOrder.ItemClick += delegate { Workitem.Commands[OEBookingCommandConstants.Command_CopyOrderData].Execute(); };
            barEdit.ItemClick += delegate { Workitem.Commands[OEBookingCommandConstants.Command_EditData].Execute(); };
            barCancel.ItemClick += delegate { Workitem.Commands[OEBookingCommandConstants.Command_CancelData].Execute(); };
            barTruck.ItemClick += delegate { Workitem.Commands[OEBookingCommandConstants.Command_Truck].Execute(); };
            barReplyAgent.ItemClick += delegate { Workitem.Commands[OEBookingCommandConstants.Command_ReplyAgent].Execute(); };
            barE_Booking.ItemClick += delegate { Workitem.Commands[OEBookingCommandConstants.Command_E_Booking].Execute(); };
            barCustoms.ItemClick += delegate { Workitem.Commands[OEBookingCommandConstants.Command_Customs].Execute(); };
            barBL.ItemClick += delegate { Workitem.Commands[OEBookingCommandConstants.Command_BL].Execute(); };
            barBill.ItemClick += delegate { Workitem.Commands[OEBookingCommandConstants.Command_Bill].Execute(); };
            barShowSearch.ItemClick += delegate { Workitem.Commands[OEBookingCommandConstants.Command_ShowSearch].Execute(); };
            barLoadContainer.ItemClick += delegate { Workitem.Commands[OEBookingCommandConstants.Command_LoadContainer].Execute(); };
            this.barRefresh.ItemClick += new ItemClickEventHandler(barRefresh_ItemClick);
            barPrintOrder.ItemClick += delegate { Workitem.Commands[OEBookingCommandConstants.Command_PrintOrder].Execute(); };
            barPrintBookingConfirm.ItemClick += delegate { Workitem.Commands[OEBookingCommandConstants.Command_PrintBookingConfirm].Execute(); };

            barPrintProfit.ItemClick += delegate { Workitem.Commands[OEBookingCommandConstants.Command_PrintProfit].Execute(); };
            barPrintInWarehouse.ItemClick += delegate { Workitem.Commands[OEBookingCommandConstants.Command_PrintInWarehouse].Execute(); };
            barCCustomerBookingSuccess.ItemClick += delegate { Workitem.Commands[OEBookingCommandConstants.Command_MailADJSOCopyToCustomerCH].Execute(); };
            barECustomerBookingSuccess.ItemClick += delegate { Workitem.Commands[OEBookingCommandConstants.Command_MailADJSOCopyToCustomerEN].Execute(); };

            barCCustomerBookingConfirmation.ItemClick += delegate { Workitem.Commands[OEBookingCommandConstants.Command_OEMailSoConfirmationToCustomerCH].Execute(); };
            barECustomerBookingConfirmation.ItemClick += delegate { Workitem.Commands[OEBookingCommandConstants.Command_OEMailSoConfirmationToCustomerEH].Execute(); };

            barEAgentBookingConfirmation.ItemClick += delegate { Workitem.Commands[OEBookingCommandConstants.Command_MailSoConfirmationToAgentEN].Execute(); };
            barBooking.ItemClick += delegate { Workitem.Commands[OEBookingCommandConstants.Command_OEBE_BookingNB].Execute(); };
            barBookingCon.ItemClick += delegate { Workitem.Commands[OEBookingCommandConstants.Command_OEBE_BookingConNB].Execute(); };

            barClose.ItemClick += delegate { this.FindForm().Close(); };

        }

        void barRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Workitem.Commands[OEBookingCommandConstants.Command_RefreshData].Execute();
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
