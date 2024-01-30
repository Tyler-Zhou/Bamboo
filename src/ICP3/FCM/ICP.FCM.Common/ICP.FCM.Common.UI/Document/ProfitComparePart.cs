using System;
using System.Collections.Generic;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Common;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.ReportCenter.UI;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;


namespace ICP.FCM.Common.UI.Document
{

    /// <summary>
    /// 签收前后利润比较控件
    /// </summary>
 
    public partial class ProfitComparePart : BaseEditPart
    { 
        /// <summary>
        /// 工作服务
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;
        }

        public ICP.FCM.Common.ServiceInterface.IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<ICP.FCM.Common.ServiceInterface.IFCMCommonService>();
            }
        }

        public ICP.Common.ServiceInterface.IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<ICP.Common.ServiceInterface.IConfigureService>();
            }
        }
        public RateHelper RateHelper
        {
            get
            {
                return ClientHelper.Get<RateHelper, RateHelper>();
            }
        }

        public IOceanImportService OceanImportService
        {
            get
            {
                return ServiceClient.GetService<IOceanImportService>();
            }
        }
        public IOperationAgentService OperationAgentService
        {
            get
            {
                return ServiceClient.GetService<IOperationAgentService>();
            }
        }

        /// <summary>
        /// 海出操作ID
        /// </summary>
        public Guid OceanBookingID { get; set; }

        /// <summary>
        /// 海进操作ID
        /// </summary>
        public Guid OIBookingID { get; set; }
        public OperationType OperationType { get; set; }
        List<SolutionCurrencyList> _CurrencyList = null;
        List<SolutionExchangeRateList> ratList = null;

        /// <summary>
        ///旧利润USD值
        /// </summary>
        ProfitCompare usdProfit ;


        /// <summary>
        ///旧利润其他币种值
        /// </summary>
        ProfitCompare otherProfit;

        Guid standardCurrencyID = Guid.Empty;
        public ProfitComparePart()
        {
            InitializeComponent();
            if (!LocalData.IsDesignMode)
            {
             
                this.Disposed += new EventHandler(ProfitComparePart_Disposed);
            }
        }

        void ProfitComparePart_Disposed(object sender, EventArgs e)
        {
        }

        private void ProfitComparePart_Load(object sender, EventArgs e)
        {
            if (LocalData.IsEnglish)
            {
                lblNewProfitCaption.Text = "New Profit:";
                lblOldProfitCaption.Text = "Old Profit:";
            }
        }


        public void Init(Guid oceanBookingID, Guid oiBookingID, OperationType operationType)
        {
            this.OceanBookingID = oceanBookingID;
            this.OIBookingID = oiBookingID;
            this.OperationType = operationType;
            if (!LocalData.IsDesignMode)
            {
             
                Guid OperationID = Guid.Empty;
                if (OperationType == OperationType.OceanExport || operationType == OperationType.AirExport)
                {
                    OperationID = OceanBookingID;
                }
                else if (OperationType == OperationType.OceanImport || operationType == OperationType.AirImport)
                {
                    OperationID = OIBookingID;
                }

                ICP.FCM.Common.ServiceInterface.DataObjects.OperationCommonInfo operationCommonInfo = FCMCommonService.GetOperationCommonInfo(OperationID, OperationType);

                ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(operationCommonInfo.CompanyID);
                ratList = ConfigureService.GetCompanyExchangeRateList(operationCommonInfo.CompanyID, true);
                _CurrencyList = ConfigureService.GetSolutionCurrencyList(configureInfo.SolutionID, true);
                foreach (var item in _CurrencyList)
                {
                    cmbProfitCurrency.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.CurrencyName, item.CurrencyID));
                    if (item.CurrencyName == "USD")
                    {
                        standardCurrencyID = item.CurrencyID;
                    }
                }


                
                try
                {
                    usdProfit = OperationAgentService.GetProfitCompare(OceanBookingID, OIBookingID, OperationType);
                    lblNewProfitValue.Text = usdProfit.NewProfit.ToString("N");
                    lblOldProfitValue.Text = usdProfit.OldProfit.ToString("N");

                    cmbProfitCurrency.SelectedText = "USD";
                }
                catch (System.Exception ex)
                {
                    lblNewProfitValue.Text ="刷新利息失败";
                    lblOldProfitValue.Text = "刷新利息失败";
                	 
                }

               

                
                this.cmbProfitCurrency.SelectedIndexChanged += new System.EventHandler(this.cmbProfitCurrency_SelectedIndexChanged);

            }
        }


        private void cmbProfitCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Guid profitCurrencyID = (Guid)cmbProfitCurrency.Properties.Items[cmbProfitCurrency.SelectedIndex].Value;

                otherProfit = new ProfitCompare();
                otherProfit.OldProfit = RateHelper.GetAmountByRate(usdProfit.OldProfit, standardCurrencyID, profitCurrencyID, ratList);
                otherProfit.NewProfit = RateHelper.GetAmountByRate(usdProfit.NewProfit, standardCurrencyID, profitCurrencyID, ratList);
                lblOldProfitValue.Text = otherProfit.OldProfit.ToString("N");
                lblNewProfitValue.Text = otherProfit.NewProfit.ToString("N");
            }
            catch (System.Exception ex)
            {
                lblNewProfitValue.Text = "刷新利息失败";
                lblOldProfitValue.Text = "刷新利息失败";

            }
        }




    }
}
