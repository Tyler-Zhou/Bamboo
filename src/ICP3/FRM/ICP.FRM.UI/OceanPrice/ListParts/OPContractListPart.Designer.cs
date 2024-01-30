namespace ICP.FRM.UI.OceanPrice
{
    partial class OPContractListPart
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colContractName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShippingLineName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContractNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCarrierName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConsigneeNames = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNotifyNames = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPaymentTermName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContractType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFromDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colToDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPublisherName = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FRM.ServiceInterface.DataObjects.OceanList);
            this.bsList.PositionChanged += new System.EventHandler(this.bsMainList_PositionChanged);
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(614, 248);
            this.gcMain.TabIndex = 0;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colContractName,
            this.colShippingLineName,
            this.colContractNo,
            this.colCarrierName,
            this.colConsigneeNames,
            this.colNotifyNames,
            this.colPaymentTermName,
            this.colCurrencyName,
            this.colContractType,
            this.colFromDate,
            this.colToDate,
            this.colState,
            this.colPublisherName});
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
            this.gvMain.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(this.gvMain_BeforeLeaveRow);
            this.gvMain.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvMain_RowStyle);
            // 
            // colContractName
            // 
            this.colContractName.Caption = "Name";
            this.colContractName.FieldName = "ContractName";
            this.colContractName.Name = "colContractName";
            this.colContractName.Visible = true;
            this.colContractName.VisibleIndex = 0;
            this.colContractName.Width = 120;
            // 
            // colShippingLineName
            // 
            this.colShippingLineName.Caption = "ShippingLine";
            this.colShippingLineName.FieldName = "ShippingLineName";
            this.colShippingLineName.Name = "colShippingLineName";
            this.colShippingLineName.Visible = true;
            this.colShippingLineName.VisibleIndex = 1;
            this.colShippingLineName.Width = 120;
            // 
            // colContractNo
            // 
            this.colContractNo.Caption = "ContractNo";
            this.colContractNo.FieldName = "ContractNo";
            this.colContractNo.Name = "colContractNo";
            this.colContractNo.Visible = true;
            this.colContractNo.VisibleIndex = 2;
            this.colContractNo.Width = 120;
            // 
            // colCarrierName
            // 
            this.colCarrierName.Caption = "Carrier";
            this.colCarrierName.FieldName = "CarrierName";
            this.colCarrierName.Name = "colCarrierName";
            this.colCarrierName.Visible = true;
            this.colCarrierName.VisibleIndex = 3;
            this.colCarrierName.Width = 120;
            // 
            // colConsigneeNames
            // 
            this.colConsigneeNames.Caption = "Consignee";
            this.colConsigneeNames.FieldName = "ConsigneeNames";
            this.colConsigneeNames.Name = "colConsigneeNames";
            this.colConsigneeNames.Visible = true;
            this.colConsigneeNames.VisibleIndex = 4;
            this.colConsigneeNames.Width = 120;
            // 
            // colNotifyNames
            // 
            this.colNotifyNames.Caption = "Notify";
            this.colNotifyNames.FieldName = "NotifyPartyNames";
            this.colNotifyNames.Name = "colNotifyNames";
            this.colNotifyNames.Visible = true;
            this.colNotifyNames.VisibleIndex = 5;
            this.colNotifyNames.Width = 120;
            // 
            // colPaymentTermName
            // 
            this.colPaymentTermName.Caption = "Payment";
            this.colPaymentTermName.FieldName = "PaymentTermName";
            this.colPaymentTermName.Name = "colPaymentTermName";
            this.colPaymentTermName.Visible = true;
            this.colPaymentTermName.VisibleIndex = 6;
            this.colPaymentTermName.Width = 100;
            // 
            // colCurrencyName
            // 
            this.colCurrencyName.Caption = "Currency";
            this.colCurrencyName.FieldName = "CurrencyName";
            this.colCurrencyName.Name = "colCurrencyName";
            this.colCurrencyName.Visible = true;
            this.colCurrencyName.VisibleIndex = 7;
            this.colCurrencyName.Width = 80;
            // 
            // colContractType
            // 
            this.colContractType.Caption = "Type";
            this.colContractType.FieldName = "ContractType";
            this.colContractType.Name = "colContractType";
            this.colContractType.Visible = true;
            this.colContractType.VisibleIndex = 8;
            this.colContractType.Width = 80;
            // 
            // colFromDate
            // 
            this.colFromDate.Caption = "(Duration)From";
            this.colFromDate.FieldName = "FromDate";
            this.colFromDate.Name = "colFromDate";
            this.colFromDate.Visible = true;
            this.colFromDate.VisibleIndex = 9;
            this.colFromDate.Width = 103;
            // 
            // colToDate
            // 
            this.colToDate.Caption = "(Duration)To";
            this.colToDate.FieldName = "ToDate";
            this.colToDate.Name = "colToDate";
            this.colToDate.Visible = true;
            this.colToDate.VisibleIndex = 10;
            this.colToDate.Width = 93;
            // 
            // colState
            // 
            this.colState.Caption = "State";
            this.colState.FieldName = "State";
            this.colState.Name = "colState";
            this.colState.Visible = true;
            this.colState.VisibleIndex = 11;
            this.colState.Width = 80;
            // 
            // colPublisherName
            // 
            this.colPublisherName.Caption = "Publisher";
            this.colPublisherName.FieldName = "PublisherName";
            this.colPublisherName.Name = "colPublisherName";
            this.colPublisherName.Visible = true;
            this.colPublisherName.VisibleIndex = 12;
            this.colPublisherName.Width = 80;
            // 
            // OPContractListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Name = "OPContractListPart";
            this.IsMultiLanguage = false;
            this.Size = new System.Drawing.Size(614, 248);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsList;
        protected ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        protected DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn colContractName;
        private DevExpress.XtraGrid.Columns.GridColumn colShippingLineName;
        private DevExpress.XtraGrid.Columns.GridColumn colContractNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCarrierName;
        private DevExpress.XtraGrid.Columns.GridColumn colConsigneeNames;
        private DevExpress.XtraGrid.Columns.GridColumn colNotifyNames;
        private DevExpress.XtraGrid.Columns.GridColumn colPaymentTermName;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyName;
        private DevExpress.XtraGrid.Columns.GridColumn colContractType;
        private DevExpress.XtraGrid.Columns.GridColumn colFromDate;
        private DevExpress.XtraGrid.Columns.GridColumn colToDate;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        private DevExpress.XtraGrid.Columns.GridColumn colPublisherName;
    }
}
