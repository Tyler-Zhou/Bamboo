using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;

namespace ICP.Business.Common.UI.QuotedPrice
{
    /// <summary>
    /// 选择报价
    /// </summary>
    public partial class SelectQuotedPrice : XtraUserControl
    {
        #region Services & Fields & Property & Delegate
        #region Services
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem RootWorkItem
        {
            get;
            set;
        }

        /// <summary>
        /// 报价服务
        /// </summary>
        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }

        #endregion

        #region Property

        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid CustomerID { get; set; }
        /// <summary>
        /// 报价人
        /// </summary>
        public Guid QuoteBy { get; set; }

        private QuotedPricePartInfo _SelectedPrice;
        /// <summary>
        /// 选择报价
        /// </summary>
        public QuotedPricePartInfo SelectedPrice
        {
            get { return _SelectedPrice; }
            set { _SelectedPrice = value; }
        }
        #endregion
        #endregion

        #region Init
        /// <summary>
        /// 选择报价
        /// </summary>
        public SelectQuotedPrice()
        {
            InitializeComponent();
            RegisterEvent();
            InitControls();
            Disposed += (sender, arg) =>
            {
                if (RootWorkItem != null)
                {
                    RootWorkItem.Items.Remove(this);
                    RootWorkItem = null;
                }
                UnRegisterEvent();
            };
        } 
        #endregion

        #region Controls Event
        /// <summary>
        /// 查询报价单
        /// </summary>
        void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime? from = null, to = null;
                from = new DateTime(dteStartDate.DateTime.Year, dteStartDate.DateTime.Month, dteStartDate.DateTime.Day, 0, 0, 0);
                to = new DateTime(dtpEndDate.DateTime.Year, dtpEndDate.DateTime.Month, dtpEndDate.DateTime.Day, 23, 59, 59);
                List<QuotedPricePartInfo> list = FCMCommonService.GetRecentlyQuotedPriceList(null,stxtNo.Text.Trim()
                        , CustomerID
                        , QuoteBy
                        , from
                        , to
                        , Int32.MaxValue);

                bsList.DataSource = list;
                bsList.ResetBindings(false);
            }
            catch (Exception ex)
            {
                MessageBoxService.ShowError(ex.Message);
            }
        }

        void btnOK_Click(object sender, EventArgs e)
        {
            SelectAndCloseForm();
        }
        
        /// <summary>
        /// 取消
        /// </summary>
        void btnCancle_Click(object sender, EventArgs e)
        {
            CloseForm(DialogResult.Cancel);
        }
        /// <summary>
        /// 双击选择
        /// </summary>
        void gvMain_DoubleClick(object sender, EventArgs e)
        {
            SelectAndCloseForm();
        }
        #endregion

        #region Custom Method
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitControls()
        {
            dteStartDate.EditValue = DateTime.Now;
            dtpEndDate.EditValue = DateTime.Now.AddDays(14);
            #region Set Language

            if (!LocalData.IsEnglish)
            {
                btnSearch.Text = "查询";
                btnOK.Text = "确定";
                btnCancel.Text = "取消";
                labNo.Text = "报价单号";
                labReceipt.Text = "装货地";
                labPOL.Text = "装货港";
                labPOD.Text = "卸货港";
                labDelivery.Text = "交货地";
                labFrom.Text = "从";
                labTo.Text = "到";

                colNo.Caption = "报价单号";
                colTransportClauseName.Caption = "运输条款";
                colPlaceOfReceiptName.Caption = "装货地";
                colPOLName.Caption = "装货港";
                colPODName.Caption = "卸货港";
                colPlaceOfDeliveryName.Caption = "交货地";
                colCommodity.Caption = "品名";
            }
            #endregion
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvent()
        {
            btnSearch.Click += btnSearch_Click;
            btnOK.Click += btnOK_Click;
            btnCancel.Click += btnCancle_Click;
            gvMain.DoubleClick += gvMain_DoubleClick;
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        private void UnRegisterEvent()
        {
            btnSearch.Click -= btnSearch_Click;
            btnOK.Click -= btnOK_Click;
            btnCancel.Click -= btnCancle_Click;
            gvMain.DoubleClick -= gvMain_DoubleClick;
        }

        /// <summary>
        /// 选择数据并关闭窗体
        /// </summary>
        private void SelectAndCloseForm()
        {
            if (bsList.Current == null) return;

            QuotedPricePartInfo current = bsList.Current as QuotedPricePartInfo;

            _SelectedPrice = current;
            var findForm = FindForm();
            CloseForm(DialogResult.OK);
        }

        private void CloseForm(DialogResult dialogResult)
        {
            var findForm = FindForm();
            if (findForm != null)
            {
                findForm.DialogResult = dialogResult;
                findForm.Close();
            }
        }
        #endregion
    }
}
