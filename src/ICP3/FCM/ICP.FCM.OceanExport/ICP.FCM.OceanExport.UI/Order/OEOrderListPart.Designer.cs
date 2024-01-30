namespace ICP.FCM.OceanExport.UI.Order
{
    partial class OEOrderListPart
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
            this.colRefNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShipperName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPlaceOfReceiptName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOLName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPODName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPlaceOfDeliveryName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConsigneeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBookingTypeDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSalesName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colClosingDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSODate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperator = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFiler = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPODContact = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateByName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FCM.OceanExport.ServiceInterface.DataObjects.OceanOrderList);
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
            this.gcMain.Size = new System.Drawing.Size(714, 248);
            this.gcMain.TabIndex = 0;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colState,
            this.colRefNo,
            this.colCustomerName,
            this.colShipperName,
            this.colPlaceOfReceiptName,
            this.colPOLName,
            this.colPODName,
            this.colPlaceOfDeliveryName,
            this.colConsigneeName,
            this.colBookingTypeDescription,
            this.colSalesName,
            this.colClosingDate,
            this.colSODate,
            this.colETD,
            this.colETA,
            this.colOperator,
            this.colFiler,
            this.colPODContact,
            this.colCreateDate,
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
            this.gvMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvMain_KeyDown);
            this.gvMain.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(this.gvMain_BeforeLeaveRow);
            this.gvMain.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvMain_RowCellClick);
            this.gvMain.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvMain_RowStyle);
            // 
            // colState
            // 
            this.colState.Caption = "State";
            this.colState.ColumnEdit = this.rcmbState;
            this.colState.FieldName = "State";
            this.colState.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colState.Name = "colState";
            this.colState.OptionsColumn.AllowMove = false;
            this.colState.Visible = true;
            this.colState.VisibleIndex = 0;
            this.colState.Width = 51;
            // 
            // rcmbState
            // 
            this.rcmbState.AutoHeight = false;
            this.rcmbState.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbState.Name = "rcmbState";
            // 
            // colRefNo
            // 
            this.colRefNo.Caption = "No";
            this.colRefNo.FieldName = "RefNo";
            this.colRefNo.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colRefNo.Name = "colRefNo";
            this.colRefNo.OptionsColumn.AllowMove = false;
            this.colRefNo.Visible = true;
            this.colRefNo.VisibleIndex = 1;
            this.colRefNo.Width = 120;
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
            // colShipperName
            // 
            this.colShipperName.Caption = "Shipper";
            this.colShipperName.FieldName = "ShipperName";
            this.colShipperName.Name = "colShipperName";
            this.colShipperName.Visible = true;
            this.colShipperName.VisibleIndex = 3;
            this.colShipperName.Width = 120;
            // 
            // colPlaceOfReceiptName
            // 
            this.colPlaceOfReceiptName.Caption = "PlaceOfReceipt";
            this.colPlaceOfReceiptName.FieldName = "PlaceOfReceiptName";
            this.colPlaceOfReceiptName.Name = "colPlaceOfReceiptName";
            this.colPlaceOfReceiptName.Visible = true;
            this.colPlaceOfReceiptName.VisibleIndex = 4;
            this.colPlaceOfReceiptName.Width = 120;
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
            // 
            // colConsigneeName
            // 
            this.colConsigneeName.Caption = "Consignee";
            this.colConsigneeName.FieldName = "ConsigneeName";
            this.colConsigneeName.Name = "colConsigneeName";
            this.colConsigneeName.Visible = true;
            this.colConsigneeName.VisibleIndex = 8;
            this.colConsigneeName.Width = 120;
            // 
            // colBookingTypeDescription
            // 
            this.colBookingTypeDescription.Caption = "OEOperationType";
            this.colBookingTypeDescription.FieldName = "BookingTypeDescription";
            this.colBookingTypeDescription.Name = "colBookingTypeDescription";
            this.colBookingTypeDescription.Visible = true;
            this.colBookingTypeDescription.VisibleIndex = 9;
            this.colBookingTypeDescription.Width = 100;
            // 
            // colSalesName
            // 
            this.colSalesName.Caption = "Sales";
            this.colSalesName.FieldName = "SalesName";
            this.colSalesName.Name = "colSalesName";
            this.colSalesName.Visible = true;
            this.colSalesName.VisibleIndex = 10;
            this.colSalesName.Width = 80;
            // 
            // colClosingDate
            // 
            this.colClosingDate.Caption = "gridColumn1";
            this.colClosingDate.FieldName = "ClosingDate";
            this.colClosingDate.Name = "colClosingDate";
            this.colClosingDate.Visible = true;
            this.colClosingDate.VisibleIndex = 11;
            // 
            // colSODate
            // 
            this.colSODate.Caption = "gridColumn1";
            this.colSODate.FieldName = "SODate";
            this.colSODate.Name = "colSODate";
            this.colSODate.Visible = true;
            this.colSODate.VisibleIndex = 12;
            // 
            // colETD
            // 
            this.colETD.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.colETD.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colETD.FieldName = "ETD";
            this.colETD.Name = "colETD";
            this.colETD.Visible = true;
            this.colETD.VisibleIndex = 13;
            // 
            // colETA
            // 
            this.colETA.FieldName = "ETA";
            this.colETA.Name = "colETA";
            this.colETA.Visible = true;
            this.colETA.VisibleIndex = 14;
            // 
            // colOperator
            // 
            this.colOperator.Caption = "Operator";
            this.colOperator.FieldName = "BookingerName";
            this.colOperator.Name = "colOperator";
            this.colOperator.Visible = true;
            this.colOperator.VisibleIndex = 15;
            // 
            // colFiler
            // 
            this.colFiler.Caption = "Filer";
            this.colFiler.FieldName = "Filer";
            this.colFiler.Name = "colFiler";
            this.colFiler.Visible = true;
            this.colFiler.VisibleIndex = 16;
            // 
            // colPODContact
            // 
            this.colPODContact.Caption = "PODContact";
            this.colPODContact.Name = "colPODContact";
            this.colPODContact.Visible = true;
            this.colPODContact.VisibleIndex = 17;
            // 
            // colCreateDate
            // 
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 18;
            this.colCreateDate.Width = 80;
            // 
            // colUpdateByName
            // 
            this.colUpdateByName.Caption = "UpdateByName";
            this.colUpdateByName.FieldName = "UpdateByName";
            this.colUpdateByName.Name = "colUpdateByName";
            this.colUpdateByName.Visible = true;
            this.colUpdateByName.VisibleIndex = 19;
            // 
            // colUpdateDate
            // 
            this.colUpdateDate.Caption = "UpdateDate";
            this.colUpdateDate.FieldName = "UpdateDate";
            this.colUpdateDate.Name = "colUpdateDate";
            this.colUpdateDate.Visible = true;
            this.colUpdateDate.VisibleIndex = 20;
            // 
            // OEOrderListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Name = "OEOrderListPart";
            this.Size = new System.Drawing.Size(714, 248);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsList;
        protected ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        protected DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        private DevExpress.XtraGrid.Columns.GridColumn colRefNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colShipperName;
        private DevExpress.XtraGrid.Columns.GridColumn colConsigneeName;
        private DevExpress.XtraGrid.Columns.GridColumn colPlaceOfReceiptName;
        private DevExpress.XtraGrid.Columns.GridColumn colPOLName;
        private DevExpress.XtraGrid.Columns.GridColumn colPODName;
        private DevExpress.XtraGrid.Columns.GridColumn colBookingTypeDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colSalesName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbState;
        private DevExpress.XtraGrid.Columns.GridColumn colPlaceOfDeliveryName;
        private DevExpress.XtraGrid.Columns.GridColumn colETD;
        private DevExpress.XtraGrid.Columns.GridColumn colETA;
        private DevExpress.XtraGrid.Columns.GridColumn colOperator;
        private DevExpress.XtraGrid.Columns.GridColumn colFiler;
        private DevExpress.XtraGrid.Columns.GridColumn colPODContact;
        private DevExpress.XtraGrid.Columns.GridColumn colSODate;
        private DevExpress.XtraGrid.Columns.GridColumn colClosingDate;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateByName;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateDate;
    }
}
