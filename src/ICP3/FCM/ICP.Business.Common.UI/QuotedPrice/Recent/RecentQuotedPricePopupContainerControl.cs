using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using System;
using Container = System.ComponentModel.Container;

namespace ICP.Business.Common.UI
{
    /// <summary>
    /// 最近报价单选择
    /// </summary>
    public class RecentQuotedPricePopupContainerControl : PopupContainerControl
    {
        #region Services & Fields & Property & Delegate
        #region Services
        /// <summary>
        /// 最近报价
        /// </summary>
        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        #endregion

        #region Fields
        private GridView gvMain;
        private GridColumn colNo;
        private GridColumn colPOLName;
        private GridColumn colPODName;
        private BindingSource bindingSource;
        private IContainer components;
        private GridColumn colPlaceOfDeliveryName;
        private LWGridControl lwGridControl; 
        #endregion

        #region Property
        #region 揽货人ID
        private Guid _salesID;
        /// <summary>
        /// 揽货人ID
        /// </summary>
        public Guid SalesID
        {
            get
            {
                return _salesID;
            }
            set
            {
                _salesID = value;
                GetQuotedPriceRecentRecords();
            }
        }
        #endregion

        #region 报价单ID
        private Guid _QuotedPriceID;
        /// <summary>
        /// 报价单ID
        /// </summary>
        public Guid QuotedPriceID
        {
            get
            {
                return _QuotedPriceID;
            }
            set
            {
                _QuotedPriceID = value;
                GetQuotedPriceRecentRecords();
            }
        } 
        #endregion
        
        #region 客户ID
        private Guid _CustomerID;
        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid CustomerID
        {
            get
            {
                return _CustomerID;
            }
            set
            {
                _CustomerID = value;
                GetQuotedPriceRecentRecords();
            }
        }
        #endregion

        /// <summary>
        /// 网格数据源
        /// </summary>
        public object DataSource
        {
            get
            {
                return bindingSource.DataSource;
            }
            set
            {
                bindingSource.DataSource = value;
                bindingSource.ResetBindings(false);
            }
        }

        /// <summary>
        /// 当前报价
        /// </summary>
        public QuotedPricePartInfo CurrentQuotedPrice
        {
            get
            {
                if (bindingSource.List == null || bindingSource.Current == null)
                {
                    return null;
                }
                return bindingSource.Current as QuotedPricePartInfo;
            }
        }
        #endregion

        #region Delegate
        /// <summary>
        /// 双击列表数据行改变事件
        /// </summary>
        public event EventHandler<CommonEventArgs<QuotedPricePartInfo>> SelectChanged; 
        #endregion
        
        #endregion

        #region Init
        /// <summary>
        /// 最近报价单选择
        /// </summary>
        public RecentQuotedPricePopupContainerControl()
        {
            InitializeComponent();
        }
        
        #endregion

        #region Controls Event
        /// <summary>
        /// 网格双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvOrders_DoubleClick(object sender, EventArgs e)
        {
            if (SelectChanged != null)
            {
                SelectChanged(sender, new CommonEventArgs<QuotedPricePartInfo>(CurrentQuotedPrice));
            }
        }
        /// <summary>
        /// 释放this
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                gvMain.DoubleClick -= gvOrders_DoubleClick;
                lwGridControl.DataSource = null;
                bindingSource.DataSource = null;
                bindingSource.Dispose();

            }
            base.Dispose(disposing);
        } 
        #endregion

        #region Custom Method
        /// <summary>
        /// 设置语言
        /// </summary>
        /// <param name="isEnglish"></param>
        public void SetLanguage(bool isEnglish)
        {
            if (!isEnglish)
            {
                colNo.Caption = "报价单号";
                colPOLName.Caption = "装货港";
                colPODName.Caption = "卸货港";
            }
        }
        /// <summary>
        /// 获取最近业务报价记录
        /// </summary>
        public void GetQuotedPriceRecentRecords()
        {
            if (CustomerID == Guid.Empty || SalesID == Guid.Empty)
            {
                bindingSource.Clear();
                bindingSource.DataSource = new List<QuotedPriceOrderInfo>();
                bindingSource.ResetBindings(false);
                return;
            }
            List<QuotedPricePartInfo> orderList = FCMCommonService.GetRecentlyQuotedPriceList(null,"", CustomerID, SalesID, null, null, 10);
            bindingSource.DataSource = orderList;
            bindingSource.ResetBindings(false);
        }

        /// <summary>
        /// 初始化组件
        /// </summary>
        private void InitializeComponent()
        {
            components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(RecentQuotedPricePopupContainerControl));
            lwGridControl = new LWGridControl();
            bindingSource = new BindingSource(components);
            gvMain = new GridView();
            colNo = new GridColumn();
            colPOLName = new GridColumn();
            colPODName = new GridColumn();
            colPlaceOfDeliveryName = new GridColumn();
            ((ISupportInitialize)(lwGridControl)).BeginInit();
            ((ISupportInitialize)(bindingSource)).BeginInit();
            ((ISupportInitialize)(gvMain)).BeginInit();
            ((ISupportInitialize)(this)).BeginInit();
            SuspendLayout();
            // 
            // lwGridControl
            // 
            lwGridControl.DataSource = bindingSource;
            lwGridControl.Dock = DockStyle.Fill;
            lwGridControl.Location = new Point(0, 0);
            lwGridControl.MainView = gvMain;
            lwGridControl.Name = "lwGridControl";
            lwGridControl.Size = new Size(658, 173);
            lwGridControl.TabIndex = 0;
            lwGridControl.ViewCollection.AddRange(new BaseView[] {
            gvMain});
            // 
            // bindingSource
            // 
            bindingSource.DataSource = ((object)(resources.GetObject("bindingSource.DataSource")));
            // 
            // gvMain
            // 
            gvMain.Columns.AddRange(new GridColumn[] {
            colNo,
            colPOLName,
            colPODName,
            colPlaceOfDeliveryName});
            gvMain.GridControl = lwGridControl;
            gvMain.Name = "gvMain";
            gvMain.OptionsBehavior.Editable = false;
            gvMain.OptionsBehavior.ReadOnly = true;
            gvMain.OptionsView.ShowGroupPanel = false;
            gvMain.DoubleClick += new EventHandler(gvOrders_DoubleClick);
            // 
            // colNo
            // 
            colNo.FieldName = "QuotedPriceNo";
            colNo.Name = "colNo";
            colNo.OptionsColumn.AllowEdit = false;
            colNo.Visible = true;
            colNo.VisibleIndex = 0;
            colNo.Width = 130;
            // 
            // colPOLName
            // 
            colPOLName.Caption = "POL";
            colPOLName.FieldName = "POLName";
            colPOLName.Name = "colPOLName";
            colPOLName.OptionsColumn.AllowEdit = false;
            colPOLName.Visible = true;
            colPOLName.VisibleIndex = 1;
            colPOLName.Width = 100;
            // 
            // colPODName
            // 
            colPODName.Caption = "POD";
            colPODName.FieldName = "PODName";
            colPODName.Name = "colPODName";
            colPODName.OptionsColumn.AllowEdit = false;
            colPODName.Visible = true;
            colPODName.VisibleIndex = 2;
            colPODName.Width = 100;
            // 
            // colPlaceOfDeliveryName
            // 
            colPlaceOfDeliveryName.Caption = "Place Of Delivery";
            colPlaceOfDeliveryName.FieldName = "PlaceOfDeliveryName";
            colPlaceOfDeliveryName.Name = "colPlaceOfDeliveryName";
            colPlaceOfDeliveryName.Visible = true;
            colPlaceOfDeliveryName.VisibleIndex = 3;
            // 
            // RecentQuotedPricePopupContainerControl
            // 
            Controls.Add(lwGridControl);
            Size = new Size(658, 173);
            ((ISupportInitialize)(lwGridControl)).EndInit();
            ((ISupportInitialize)(bindingSource)).EndInit();
            ((ISupportInitialize)(gvMain)).EndInit();
            ((ISupportInitialize)(this)).EndInit();
            ResumeLayout(false);

        } 
        #endregion
    }
}
