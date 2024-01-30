namespace ICP.Business.Common.UI.QuotedPrice
{
    partial class QuotedPriceListPart
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTargetTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCommodity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTransportClauseName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuoteByName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFromDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colToDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConfirmedName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateByName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPaymentType = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FCM.Common.ServiceInterface.DataObjects.QuotedPriceOrderList);
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(852, 552);
            this.gcMain.TabIndex = 1;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colNo,
            this.colCustomerName,
            this.colTargetTypeName,
            this.colCommodity,
            this.colTransportClauseName,
            this.colQuoteByName,
            this.colPaymentType,
            this.colFromDate,
            this.colToDate,
            this.colConfirmedName,
            this.colCreateByName,
            this.colCreateDate});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.IndicatorWidth = 27;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsDetail.AllowZoomDetail = false;
            this.gvMain.OptionsDetail.EnableMasterViewMode = false;
            this.gvMain.OptionsDetail.ShowDetailTabs = false;
            this.gvMain.OptionsDetail.SmartDetailExpand = false;
            this.gvMain.OptionsDetail.SmartDetailExpandButtonMode = DevExpress.XtraGrid.Views.Grid.DetailExpandButtonMode.AlwaysEnabled;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsSelection.UseIndicatorForSelection = false;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowDetailButtons = false;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colNo
            // 
            this.colNo.FieldName = "No";
            this.colNo.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colNo.Name = "colNo";
            this.colNo.Visible = true;
            this.colNo.VisibleIndex = 0;
            this.colNo.Width = 120;
            // 
            // colCustomerName
            // 
            this.colCustomerName.Caption = "CustomerName";
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 1;
            this.colCustomerName.Width = 120;
            // 
            // colTargetTypeName
            // 
            this.colTargetTypeName.Caption = "Type";
            this.colTargetTypeName.FieldName = "TargetTypeName";
            this.colTargetTypeName.Name = "colTargetTypeName";
            this.colTargetTypeName.Visible = true;
            this.colTargetTypeName.VisibleIndex = 2;
            this.colTargetTypeName.Width = 80;
            // 
            // colCommodity
            // 
            this.colCommodity.Caption = "Commodity";
            this.colCommodity.FieldName = "Commodity";
            this.colCommodity.Name = "colCommodity";
            this.colCommodity.Visible = true;
            this.colCommodity.VisibleIndex = 3;
            this.colCommodity.Width = 120;
            // 
            // colTransportClauseName
            // 
            this.colTransportClauseName.Caption = "Mode";
            this.colTransportClauseName.FieldName = "TransportClauseName";
            this.colTransportClauseName.Name = "colTransportClauseName";
            this.colTransportClauseName.Visible = true;
            this.colTransportClauseName.VisibleIndex = 4;
            this.colTransportClauseName.Width = 100;
            // 
            // colQuoteByName
            // 
            this.colQuoteByName.Caption = "Quote By Name";
            this.colQuoteByName.FieldName = "QuoteByName";
            this.colQuoteByName.Name = "colQuoteByName";
            this.colQuoteByName.Visible = true;
            this.colQuoteByName.VisibleIndex = 5;
            // 
            // colFromDate
            // 
            this.colFromDate.Caption = "From Date";
            this.colFromDate.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.colFromDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colFromDate.FieldName = "FromDate";
            this.colFromDate.Name = "colFromDate";
            this.colFromDate.Visible = true;
            this.colFromDate.VisibleIndex = 6;
            this.colFromDate.Width = 80;
            // 
            // colToDate
            // 
            this.colToDate.Caption = "To Date";
            this.colToDate.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.colToDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colToDate.FieldName = "ToDate";
            this.colToDate.Name = "colToDate";
            this.colToDate.Visible = true;
            this.colToDate.VisibleIndex = 8;
            this.colToDate.Width = 80;
            // 
            // colConfirmedName
            // 
            this.colConfirmedName.Caption = "Confirmed By";
            this.colConfirmedName.FieldName = "ConfirmedName";
            this.colConfirmedName.Name = "colConfirmedName";
            this.colConfirmedName.Visible = true;
            this.colConfirmedName.VisibleIndex = 9;
            this.colConfirmedName.Width = 80;
            // 
            // colCreateByName
            // 
            this.colCreateByName.FieldName = "CreateByName";
            this.colCreateByName.Name = "colCreateByName";
            this.colCreateByName.Visible = true;
            this.colCreateByName.VisibleIndex = 10;
            this.colCreateByName.Width = 120;
            // 
            // colCreateDate
            // 
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 11;
            this.colCreateDate.Width = 120;
            // 
            // colPaymentType
            // 
            this.colPaymentType.Caption = "Payment Type";
            this.colPaymentType.FieldName = "PaymentTypeName";
            this.colPaymentType.Name = "colPaymentType";
            this.colPaymentType.Visible = true;
            this.colPaymentType.VisibleIndex = 7;
            // 
            // QuotedPriceListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Name = "QuotedPriceListPart";
            this.Size = new System.Drawing.Size(852, 552);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsList;
        protected Framework.ClientComponents.Controls.LWGridControl gcMain;
        protected DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn colNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateByName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colCommodity;
        private DevExpress.XtraGrid.Columns.GridColumn colTransportClauseName;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colFromDate;
        private DevExpress.XtraGrid.Columns.GridColumn colToDate;
        private DevExpress.XtraGrid.Columns.GridColumn colConfirmedName;
        private DevExpress.XtraGrid.Columns.GridColumn colQuoteByName;
        private DevExpress.XtraGrid.Columns.GridColumn colTargetTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colPaymentType;
    }
}
