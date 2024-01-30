using System;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FRM.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface;

namespace ICP.FCM.OceanExport.UI.Common.Parts
{
    /// <summary>
    /// 业务界面询价面板
    /// </summary>
    public partial class UCInquirePrice : BaseEditPart
    {

        #region Service

        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;
        }

        public IClientInquireRateService ClientInquireRateService
        {
            get { return ServiceClient.GetClientService<IClientInquireRateService>(); }
        }

        public IClientOceanExportService ClientOceanExportService
        {
            get
            {
                return ServiceClient.GetClientService<IClientOceanExportService>();
            }
        }
        #endregion

        #region 事件

        public event EventHandler ClickNew;

        #endregion

        #region 变量

        /// <summary>
        /// 当前询价面板绑定值
        /// </summary>
        public InquirePricePartInfo CurrentInquirePrice = new InquirePricePartInfo();

        public OceanBookingInfo OceanBookingInfo
        {
            get;
            set;
        }

        #endregion

        #region 属性
        /// <summary>
        /// 设置需要确认按钮是否禁用
        /// </summary>
        public bool SetConfirmationEnabled
        {
            set
            {
                chkNeedConfirmation.Enabled = value;
                if (value == false)
                {
                    chkNeedConfirmation.Checked = false;
                }
            }
        }

        #endregion

        public UCInquirePrice()
        {
            InitializeComponent();
            if (!DesignMode && LocalData.IsEnglish)
            {
                SetEnText();
            }
        }

        private void SetEnText()
        {
            grbInquirePrice.Text = "Inquire Price";
            lblNo.Text = "Inquire Price NO";
            lblConfirmed.Text = "Confirmed By";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }

        private void InitControls()
        {


            btnInquirePrice.Click += new System.EventHandler(this.txtInquirePrice_Click);
        }

        /// <summary>
        /// 数据源
        /// </summary>
        public override object DataSource
        {
            get { return bsInquirePrice.DataSource as InquirePricePartInfo; }
            set { BindingData(value); }
        }

        private void BindingData(object value)
        {
            if (value == null)
            {
                return;
            }
            bsInquirePrice.DataSource = value;
            bsInquirePrice.ResetBindings(false);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (ClickNew != null)
            {
                ClickNew(sender, e);
            }
        }

        private void txtInquirePrice_Click(object sender, EventArgs e)
        {
            if (this.OceanBookingInfo == null)
            {
                return;
            }
            ClientOceanExportService.SelectContract(this.OceanBookingInfo, SelectType.InquirePrice, AfterSelectInquirePrice);
        }

        private void AfterSelectInquirePrice(object[] parameters)
        {
            FreightList selectedInquirePrice = parameters[0] as FreightList;
            if (selectedInquirePrice != null)
            {
                if (DataSource == null)
                {
                    DataSource = new InquirePricePartInfo();
                }
                CurrentInquirePrice = DataSource as InquirePricePartInfo;
                CurrentInquirePrice.InquirePriceID = selectedInquirePrice.ID;
                CurrentInquirePrice.InquirePriceNO = btnInquirePrice.Text = selectedInquirePrice.FreightNo;

                this.DataSource = CurrentInquirePrice;
            }
        }
    }
}