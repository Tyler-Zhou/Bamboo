using System;
using DevExpress.XtraBars;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.Service;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.WF.ServiceInterface;

namespace ICP.FAM.UI.CustomerBill
{
    public partial class PaymentFreightPart : BaseEditPart
    {
        public PaymentFreightPart()
        {
            InitializeComponent();
            Disposed += delegate {
                bsList.DataSource = null;
                bsList.Dispose();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            
            };
        }

        #region 初始化

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IWorkflowClientService WorkflowClientService
        {
            get
            {
                return ServiceClient.GetClientService<IWorkflowClientService>();
            }
        }

        #endregion

        #region 初始化
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitMessage();
            }
        }
        /// <summary>
        /// 初始化消息
        /// </summary>
        private void InitMessage()
        {
            RegisterMessage("11082400001", LocalData.IsEnglish ? "Input the process name" : "请输入流程名称");
        }
        #endregion

        #region 属性及变量
        public PaymentFreightItem CurrentData
        {
            set
            {
                bsList.DataSource = value;
            }
            get
            {
                return bsList.DataSource as PaymentFreightItem;
            }
        }


        private Guid _currtentbilltoId = Guid.Empty;

        public void SetDataSource(PaymentFreightItem payment, Guid currentbillToid)
        {
            CurrentData = payment;
            _currtentbilltoId = currentbillToid;

            if (!string.IsNullOrEmpty(CurrentData.CurrencyName))
            {
                labAmount.Text = CurrentData.CurrencyName;
            }
        }

        #endregion

        #region 关闭
        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().Close();
        }
        #endregion

        #region 发起流程
        private void barStartWork_ItemClick(object sender, ItemClickEventArgs e)
        {
            bsList.EndEdit();

            if (string.IsNullOrEmpty(txtWorkName.Text))
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(
                    FindForm(),
                    NativeLanguageService.GetText(this, "11082400001"));

                return;
            }

            try
            {
                ShowFayFreight();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);

            }


        }

        private void ShowFayFreight()
        {
            string workName = CurrentData.WorkFlowName.Trim();

            if (string.IsNullOrEmpty(workName))
            {
                workName = cmbShipperName.Text + (LocalData.IsEnglish ? "Freight Prepaid" : "运费付讫");
               
            }
            string formTitle = LocalData.IsEnglish ? "Applications for payment" : "付款申请";

            WorkflowClientService.StartNoticeOfPaymentWorkFlow(
                LocalData.UserInfo.LoginID,
                LocalData.UserInfo.DefaultDepartmentID,
                workName,
                formTitle,
                string.Empty,
                DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified),
                CurrentData.CompanyID,
                CurrentData.BLNo,
                CurrentData.BankName,
                CurrentData.Amount.ToString(),
                CurrentData.CurrencyName,
                CurrentData.OriginalityAmount,
                CurrentData.CustomerName,
                CurrentData.BillToID,
                CurrentData.BillNo,
                CurrentData.BillToName,
                CurrentData.BankNo,
                CurrentData.GoodsNmae,
                CurrentData.Remark,
                LocalData.UserInfo.UserName);
        }

        #endregion
    }
}
