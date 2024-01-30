using System;
using System.ComponentModel;
using DevExpress.XtraBars;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.FAM.ServiceInterface;

namespace ICP.FCM.AirExport.UI
{
    [ToolboxItem(false)]
    [SmartPart]
    public partial class OEBusinessInfoEditPart : BaseEditPart
    {
        #region 服务注入

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IFinanceClientService FinanceClientService { get; set; }

        #endregion

        public OEBusinessInfoEditPart()
        {
            InitializeComponent();
            Disposed += delegate { if(Workitem !=null)Workitem.Items.Remove(this); };

            if (LocalData.IsEnglish == false) SetCnText();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            bsMBL.DataSource = UIModelHelper.GetNormalObject<AirBookingInfo>();
            bsHBL1.DataSource = UIModelHelper.GetNormalObject<AirHBLInfo>();

            bsMBL.ResetBindings(false);
            bsHBL1.ResetBindings(false);

            barClose.ItemClick += delegate { FindForm().Close(); };
        }


        #region 设置中文字符

        private void SetCnText()
        {
           
            labBookingDate.Text = "委托日期";
            labBookingMode.Text = "委托方式";

            labContractNo.Text = "合约号";
         
            labCompany.Text = "操作口岸";
            labConsignee.Text = "收货人";
            labCustomer.Text = "客户";
         

      
            labNo.Text = "业务号";
            labPaymentTerm.Text = "付款方式";
            labPlaceOfDelivery.Text = "交货地";
            labPOD.Text = "卸货港";
         
            labSales.Text = "揽货人";
            labSalesDepartment.Text = "揽货部门";
            labSalesType.Text = "揽货类型";
            labShipper.Text = "发货人";
            labTradeTerm.Text = "贸易条款";
            labTransportClause.Text = "运输条款";
       
            labType.Text = "业务类型";
       

            labAgent.Text = "代理";
           
            labOverseasFiler.Text = "海外部客服";
            
            labFiler.Text = "文件";
            labState.Text = "状态";
        
            labVoyage.Text = "大船";
            labPreVoyage.Text = "驳船";
            labPlaceOfReceipt.Text = "收货地";
            labFinalDestination.Text = "最终目的地";

            labETD.Text = "离港日";
            labETA.Text = "到港日";

            navBarBase.Caption = "基本信息";
            navBarBLInfo.Caption = "委托信息";
      

            labShippingLine.Text = "航线";
         
            labPOL2.Text = "装货港";
            tabMBL.Text = "基础";

            barRefresh.Caption = "刷新(&R)";
        
           
            barPrintOrder.Caption = "业务联单";
            barPrintBookingConfirm.Caption = "订舱确认书";
            barPrintInWarehouse.Caption = "进仓通知书";
            barClose.Caption = "关闭(&C)";

         
        }

        #endregion

        private void barRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barBill_ItemClick(object sender, ItemClickEventArgs e)
        {
            FinanceClientService.ShowBillList(new OperationCommonInfo 
            {
                OperationID = Guid.NewGuid(),
                CompanyID = Guid.Empty
            },
            ClientConstants.MainWorkspace);
        }

    }
}