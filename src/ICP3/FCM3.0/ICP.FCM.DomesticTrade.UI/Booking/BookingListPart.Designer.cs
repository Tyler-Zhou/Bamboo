namespace ICP.FCM.DomesticTrade.UI.Booking
{
    partial class BookingListPart
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
            this.colState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcmbState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShippingOrderNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPlaceOfReceiptName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOLName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPODName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPlaceOfDeliveryName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVesselVoyage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShipperName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConsigneeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSODate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAgentOfCarrierName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCarrierName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSalesName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBookingerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFiler = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPODContact = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pccHyperLinks = new DevExpress.XtraEditors.PopupContainerControl();
            this.hyperLinkEdit1 = new DevExpress.XtraEditors.HyperLinkEdit();
            this.colUpdateByName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pccHyperLinks)).BeginInit();
            this.pccHyperLinks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.DTBookingList);
            this.bsList.PositionChanged += new System.EventHandler(this.bsMainList_PositionChanged);
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcmbState});
            this.gcMain.Size = new System.Drawing.Size(707, 376);
            this.gcMain.TabIndex = 0;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colState,
            this.colNo,
            this.colCustomerName,
            this.colShippingOrderNo,
            this.colPlaceOfReceiptName,
            this.colPOLName,
            this.colPODName,
            this.colPlaceOfDeliveryName,
            this.colVesselVoyage,
            this.colETD,
            this.colETA,
            this.colShipperName,
            this.colConsigneeName,
            this.colSODate,
            this.colCreateDate,
            this.colAgentOfCarrierName,
            this.colCarrierName,
            this.colType,
            this.colSalesName,
            this.colBookingerName,
            this.colFiler,
            this.colPODContact,
            this.colUpdateByName,
            this.colUpdateDate});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.IndicatorWidth = 35;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowDetailButtons = false;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvMain_CustomDrawRowIndicator);
            this.gvMain.MouseEnter += new System.EventHandler(this.gvMain_MouseEnter);
            this.gvMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvMain_KeyDown);
            this.gvMain.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(this.gvMain_BeforeLeaveRow);
            this.gvMain.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvMain_RowCellClick);
            this.gvMain.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvMain_RowStyle);
            // 
            // colState
            // 
            this.colState.ColumnEdit = this.rcmbState;
            this.colState.FieldName = "State";
            this.colState.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colState.Name = "colState";
            this.colState.OptionsColumn.AllowMove = false;
            this.colState.Visible = true;
            this.colState.VisibleIndex = 0;
            this.colState.Width = 60;
            // 
            // rcmbState
            // 
            this.rcmbState.AutoHeight = false;
            this.rcmbState.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbState.Name = "rcmbState";
            // 
            // colNo
            // 
            this.colNo.FieldName = "No";
            this.colNo.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colNo.Name = "colNo";
            this.colNo.OptionsColumn.AllowMove = false;
            this.colNo.Visible = true;
            this.colNo.VisibleIndex = 1;
            this.colNo.Width = 120;
            // 
            // colCustomerName
            // 
            this.colCustomerName.Caption = "Customer";
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 2;
            this.colCustomerName.Width = 120;
            // 
            // colShippingOrderNo
            // 
            this.colShippingOrderNo.FieldName = "OceanShippingOrderNo";
            this.colShippingOrderNo.Name = "colShippingOrderNo";
            this.colShippingOrderNo.Visible = true;
            this.colShippingOrderNo.VisibleIndex = 3;
            this.colShippingOrderNo.Width = 120;
            // 
            // colPlaceOfReceiptName
            // 
            this.colPlaceOfReceiptName.FieldName = "PlaceOfReceiptName";
            this.colPlaceOfReceiptName.Name = "colPlaceOfReceiptName";
            this.colPlaceOfReceiptName.Visible = true;
            this.colPlaceOfReceiptName.VisibleIndex = 4;
            // 
            // colPOLName
            // 
            this.colPOLName.Caption = "POL";
            this.colPOLName.FieldName = "POLName";
            this.colPOLName.Name = "colPOLName";
            this.colPOLName.Visible = true;
            this.colPOLName.VisibleIndex = 5;
            this.colPOLName.Width = 120;
            // 
            // colPODName
            // 
            this.colPODName.Caption = "POD";
            this.colPODName.FieldName = "PODName";
            this.colPODName.Name = "colPODName";
            this.colPODName.Visible = true;
            this.colPODName.VisibleIndex = 6;
            this.colPODName.Width = 120;
            // 
            // colPlaceOfDeliveryName
            // 
            this.colPlaceOfDeliveryName.Caption = "PlaceOfDelivery";
            this.colPlaceOfDeliveryName.FieldName = "PlaceOfDeliveryName";
            this.colPlaceOfDeliveryName.Name = "colPlaceOfDeliveryName";
            this.colPlaceOfDeliveryName.Visible = true;
            this.colPlaceOfDeliveryName.VisibleIndex = 7;
            this.colPlaceOfDeliveryName.Width = 120;
            // 
            // colVesselVoyage
            // 
            this.colVesselVoyage.FieldName = "VesselVoyage";
            this.colVesselVoyage.Name = "colVesselVoyage";
            this.colVesselVoyage.Visible = true;
            this.colVesselVoyage.VisibleIndex = 8;
            this.colVesselVoyage.Width = 120;
            // 
            // colETD
            // 
            this.colETD.FieldName = "ETD";
            this.colETD.Name = "colETD";
            this.colETD.Visible = true;
            this.colETD.VisibleIndex = 9;
            this.colETD.Width = 80;
            // 
            // colETA
            // 
            this.colETA.Caption = "ETA";
            this.colETA.FieldName = "ETA";
            this.colETA.Name = "colETA";
            this.colETA.Visible = true;
            this.colETA.VisibleIndex = 10;
            // 
            // colShipperName
            // 
            this.colShipperName.FieldName = "ShipperName";
            this.colShipperName.Name = "colShipperName";
            this.colShipperName.Visible = true;
            this.colShipperName.VisibleIndex = 11;
            // 
            // colConsigneeName
            // 
            this.colConsigneeName.FieldName = "ConsigneeName";
            this.colConsigneeName.Name = "colConsigneeName";
            this.colConsigneeName.Visible = true;
            this.colConsigneeName.VisibleIndex = 12;
            // 
            // colSODate
            // 
            this.colSODate.FieldName = "SODate";
            this.colSODate.Name = "colSODate";
            this.colSODate.Visible = true;
            this.colSODate.VisibleIndex = 13;
            // 
            // colCreateDate
            // 
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 14;
            this.colCreateDate.Width = 80;
            // 
            // colAgentOfCarrierName
            // 
            this.colAgentOfCarrierName.Caption = "AgentOfCarrier";
            this.colAgentOfCarrierName.FieldName = "AgentOfCarrierName";
            this.colAgentOfCarrierName.Name = "colAgentOfCarrierName";
            this.colAgentOfCarrierName.Visible = true;
            this.colAgentOfCarrierName.VisibleIndex = 15;
            this.colAgentOfCarrierName.Width = 120;
            // 
            // colCarrierName
            // 
            this.colCarrierName.Caption = "Carrier";
            this.colCarrierName.FieldName = "CarrierName";
            this.colCarrierName.Name = "colCarrierName";
            this.colCarrierName.Visible = true;
            this.colCarrierName.VisibleIndex = 16;
            this.colCarrierName.Width = 120;
            // 
            // colType
            // 
            this.colType.FieldName = "OEOperationTypeDescription";
            this.colType.Name = "colType";
            this.colType.Visible = true;
            this.colType.VisibleIndex = 17;
            this.colType.Width = 80;
            // 
            // colSalesName
            // 
            this.colSalesName.Caption = "Sales";
            this.colSalesName.FieldName = "SalesName";
            this.colSalesName.Name = "colSalesName";
            this.colSalesName.Visible = true;
            this.colSalesName.VisibleIndex = 18;
            this.colSalesName.Width = 80;
            // 
            // colBookingerName
            // 
            this.colBookingerName.Caption = "Booking";
            this.colBookingerName.FieldName = "BookingerName";
            this.colBookingerName.Name = "colBookingerName";
            this.colBookingerName.Visible = true;
            this.colBookingerName.VisibleIndex = 19;
            // 
            // colFiler
            // 
            this.colFiler.Caption = "Filer";
            this.colFiler.FieldName = "FilerName";
            this.colFiler.Name = "colFiler";
            this.colFiler.Visible = true;
            this.colFiler.VisibleIndex = 20;
            // 
            // colPODContact
            // 
            this.colPODContact.Caption = "PODContact";
            this.colPODContact.FieldName = "PODContact";
            this.colPODContact.Name = "colPODContact";
            this.colPODContact.Visible = true;
            this.colPODContact.VisibleIndex = 21;
            // 
            // pccHyperLinks
            // 
            this.pccHyperLinks.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.pccHyperLinks.Controls.Add(this.hyperLinkEdit1);
            this.pccHyperLinks.Location = new System.Drawing.Point(198, 92);
            this.pccHyperLinks.Name = "pccHyperLinks";
            this.pccHyperLinks.Size = new System.Drawing.Size(200, 100);
            this.pccHyperLinks.TabIndex = 1;
            // 
            // hyperLinkEdit1
            // 
            this.hyperLinkEdit1.Location = new System.Drawing.Point(22, 21);
            this.hyperLinkEdit1.Name = "hyperLinkEdit1";
            this.hyperLinkEdit1.Size = new System.Drawing.Size(100, 21);
            this.hyperLinkEdit1.TabIndex = 0;
            // 
            // colUpdateByName
            // 
            this.colUpdateByName.Caption = "UpdateByName";
            this.colUpdateByName.FieldName = "UpdateByName";
            this.colUpdateByName.Name = "colUpdateByName";
            this.colUpdateByName.Visible = true;
            this.colUpdateByName.VisibleIndex = 22;
            // 
            // colUpdateDate
            // 
            this.colUpdateDate.FieldName = "UpdateDate";
            this.colUpdateDate.Name = "colUpdateDate";
            this.colUpdateDate.Visible = true;
            this.colUpdateDate.VisibleIndex = 23;
            // 
            // BookingListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pccHyperLinks);
            this.Controls.Add(this.gcMain);
            this.Name = "BookingListPart";
            this.Size = new System.Drawing.Size(707, 376);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pccHyperLinks)).EndInit();
            this.pccHyperLinks.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsList;
        protected ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        protected DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbState;
        private DevExpress.XtraGrid.Columns.GridColumn colNo;
        private DevExpress.XtraGrid.Columns.GridColumn colShippingOrderNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colCarrierName;
        private DevExpress.XtraGrid.Columns.GridColumn colAgentOfCarrierName;
        private DevExpress.XtraGrid.Columns.GridColumn colVesselVoyage;
        private DevExpress.XtraGrid.Columns.GridColumn colPOLName;
        private DevExpress.XtraGrid.Columns.GridColumn colPODName;
        private DevExpress.XtraGrid.Columns.GridColumn colPlaceOfDeliveryName;
        private DevExpress.XtraGrid.Columns.GridColumn colETD;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colSalesName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        private DevExpress.XtraGrid.Columns.GridColumn colETA;
        private DevExpress.XtraGrid.Columns.GridColumn colBookingerName;
        private DevExpress.XtraGrid.Columns.GridColumn colFiler;
        private DevExpress.XtraGrid.Columns.GridColumn colPODContact;
        private DevExpress.XtraGrid.Columns.GridColumn colPlaceOfReceiptName;
        private DevExpress.XtraGrid.Columns.GridColumn colShipperName;
        private DevExpress.XtraGrid.Columns.GridColumn colConsigneeName;
        private DevExpress.XtraGrid.Columns.GridColumn colSODate;
        private DevExpress.XtraEditors.PopupContainerControl pccHyperLinks;
        private DevExpress.XtraEditors.HyperLinkEdit hyperLinkEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateByName;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateDate;
    }
}
