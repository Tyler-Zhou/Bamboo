namespace ICP.Business.Common.UI.CSPBooking
{
    partial class PartList
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
            this.bsList = new System.Windows.Forms.BindingSource();
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBookingNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBookingName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFreightMethodTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTradeTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShipmentTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTransportClauseName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBookingDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContainers = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShipperName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConsigneeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOLName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOLAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETDForPOL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPODName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPODAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETAForPOD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantityUnitName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWeight = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWeightUnitName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMeasurement = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMeasurementUnitName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIncoTermName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsDeclaration = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsInsurance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsTruck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMarks = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcmbState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.cmbOEOperationType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colFBAFreightMethodTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOEOperationType)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FCM.Common.ServiceInterface.DataObjects.BookingDelegateList);
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
            this.rcmbState,
            this.repositoryItemCheckEdit1,
            this.cmbOEOperationType});
            this.gcMain.Size = new System.Drawing.Size(707, 376);
            this.gcMain.TabIndex = 0;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBookingNo,
            this.colBookingName,
            this.colTradeTypeName,
            this.colFreightMethodTypeName,
            this.colFBAFreightMethodTypeName,
            this.colShipmentTypeName,
            this.colTransportClauseName,
            this.colBookingDate,
            this.colContainers,
            this.colCustomerName,
            this.colShipperName,
            this.colConsigneeName,
            this.colPOLName,
            this.colPOLAddress,
            this.colETDForPOL,
            this.colPODName,
            this.colPODAddress,
            this.colETAForPOD,
            this.colQuantity,
            this.colQuantityUnitName,
            this.colWeight,
            this.colWeightUnitName,
            this.colMeasurement,
            this.colMeasurementUnitName,
            this.colIncoTermName,
            this.colIsDeclaration,
            this.colIsInsurance,
            this.colIsTruck,
            this.colMarks});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.IndicatorWidth = 35;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsDetail.AllowZoomDetail = false;
            this.gvMain.OptionsDetail.EnableMasterViewMode = false;
            this.gvMain.OptionsDetail.ShowDetailTabs = false;
            this.gvMain.OptionsDetail.SmartDetailExpand = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowDetailButtons = false;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvMain_RowCellClick);
            this.gvMain.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvMain_RowStyle);
            this.gvMain.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(this.gvMain_BeforeLeaveRow);
            this.gvMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvMain_KeyDown);
            // 
            // colBookingNo
            // 
            this.colBookingNo.FieldName = "BookingNo";
            this.colBookingNo.Name = "colBookingNo";
            this.colBookingNo.Visible = true;
            this.colBookingNo.VisibleIndex = 0;
            // 
            // colBookingName
            // 
            this.colBookingName.FieldName = "BookingName";
            this.colBookingName.Name = "colBookingName";
            this.colBookingName.Visible = true;
            this.colBookingName.VisibleIndex = 1;
            // 
            // colFreightMethodTypeName
            // 
            this.colFreightMethodTypeName.Caption = "Freight Method";
            this.colFreightMethodTypeName.FieldName = "FreightMethodTypeName";
            this.colFreightMethodTypeName.Name = "colFreightMethodTypeName";
            this.colFreightMethodTypeName.Visible = true;
            this.colFreightMethodTypeName.VisibleIndex = 3;
            // 
            // colTradeTypeName
            // 
            this.colTradeTypeName.Caption = "Trade Type";
            this.colTradeTypeName.FieldName = "TradeTypeName";
            this.colTradeTypeName.Name = "colTradeTypeName";
            this.colTradeTypeName.Visible = true;
            this.colTradeTypeName.VisibleIndex = 2;
            // 
            // colShipmentTypeName
            // 
            this.colShipmentTypeName.Caption = "Shipment Type";
            this.colShipmentTypeName.FieldName = "ShipmentTypeName";
            this.colShipmentTypeName.Name = "colShipmentTypeName";
            this.colShipmentTypeName.Visible = true;
            this.colShipmentTypeName.VisibleIndex = 5;
            // 
            // colTransportClauseName
            // 
            this.colTransportClauseName.Caption = "Transport Clause";
            this.colTransportClauseName.FieldName = "TransportClauseName";
            this.colTransportClauseName.Name = "colTransportClauseName";
            this.colTransportClauseName.Visible = true;
            this.colTransportClauseName.VisibleIndex = 6;
            // 
            // colBookingDate
            // 
            this.colBookingDate.FieldName = "BookingDate";
            this.colBookingDate.Name = "colBookingDate";
            this.colBookingDate.Visible = true;
            this.colBookingDate.VisibleIndex = 7;
            // 
            // colContainers
            // 
            this.colContainers.FieldName = "Containers";
            this.colContainers.Name = "colContainers";
            this.colContainers.Visible = true;
            this.colContainers.VisibleIndex = 8;
            // 
            // colCustomerName
            // 
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 9;
            // 
            // colShipperName
            // 
            this.colShipperName.FieldName = "ShipperName";
            this.colShipperName.Name = "colShipperName";
            this.colShipperName.Visible = true;
            this.colShipperName.VisibleIndex = 10;
            // 
            // colConsigneeName
            // 
            this.colConsigneeName.FieldName = "ConsigneeName";
            this.colConsigneeName.Name = "colConsigneeName";
            this.colConsigneeName.Visible = true;
            this.colConsigneeName.VisibleIndex = 11;
            // 
            // colPOLName
            // 
            this.colPOLName.FieldName = "POLName";
            this.colPOLName.Name = "colPOLName";
            this.colPOLName.Visible = true;
            this.colPOLName.VisibleIndex = 12;
            // 
            // colPOLAddress
            // 
            this.colPOLAddress.FieldName = "POLAddress";
            this.colPOLAddress.Name = "colPOLAddress";
            this.colPOLAddress.Visible = true;
            this.colPOLAddress.VisibleIndex = 13;
            // 
            // colETDForPOL
            // 
            this.colETDForPOL.Caption = "ETD";
            this.colETDForPOL.FieldName = "ETDForPOL";
            this.colETDForPOL.Name = "colETDForPOL";
            this.colETDForPOL.Visible = true;
            this.colETDForPOL.VisibleIndex = 14;
            // 
            // colPODName
            // 
            this.colPODName.FieldName = "PODName";
            this.colPODName.Name = "colPODName";
            this.colPODName.Visible = true;
            this.colPODName.VisibleIndex = 15;
            // 
            // colPODAddress
            // 
            this.colPODAddress.FieldName = "PODAddress";
            this.colPODAddress.Name = "colPODAddress";
            this.colPODAddress.Visible = true;
            this.colPODAddress.VisibleIndex = 16;
            // 
            // colETAForPOD
            // 
            this.colETAForPOD.Caption = "ETA";
            this.colETAForPOD.FieldName = "ETAForPOD";
            this.colETAForPOD.Name = "colETAForPOD";
            this.colETAForPOD.Visible = true;
            this.colETAForPOD.VisibleIndex = 17;
            // 
            // colQuantity
            // 
            this.colQuantity.FieldName = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.Visible = true;
            this.colQuantity.VisibleIndex = 18;
            // 
            // colQuantityUnitName
            // 
            this.colQuantityUnitName.Caption = "Quantity Unit";
            this.colQuantityUnitName.FieldName = "QuantityUnitName";
            this.colQuantityUnitName.Name = "colQuantityUnitName";
            this.colQuantityUnitName.Visible = true;
            this.colQuantityUnitName.VisibleIndex = 19;
            // 
            // colWeight
            // 
            this.colWeight.FieldName = "Weight";
            this.colWeight.Name = "colWeight";
            this.colWeight.Visible = true;
            this.colWeight.VisibleIndex = 20;
            // 
            // colWeightUnitName
            // 
            this.colWeightUnitName.Caption = "Weight Unit";
            this.colWeightUnitName.FieldName = "WeightUnitName";
            this.colWeightUnitName.Name = "colWeightUnitName";
            this.colWeightUnitName.Visible = true;
            this.colWeightUnitName.VisibleIndex = 21;
            // 
            // colMeasurement
            // 
            this.colMeasurement.FieldName = "Measurement";
            this.colMeasurement.Name = "colMeasurement";
            this.colMeasurement.Visible = true;
            this.colMeasurement.VisibleIndex = 22;
            // 
            // colMeasurementUnitName
            // 
            this.colMeasurementUnitName.Caption = "Measurement Unit";
            this.colMeasurementUnitName.FieldName = "MeasurementUnitName";
            this.colMeasurementUnitName.Name = "colMeasurementUnitName";
            this.colMeasurementUnitName.Visible = true;
            this.colMeasurementUnitName.VisibleIndex = 23;
            // 
            // colIncoTermName
            // 
            this.colIncoTermName.FieldName = "IncoTermName";
            this.colIncoTermName.Name = "colIncoTermName";
            this.colIncoTermName.Visible = true;
            this.colIncoTermName.VisibleIndex = 24;
            // 
            // colIsDeclaration
            // 
            this.colIsDeclaration.FieldName = "IsDeclaration";
            this.colIsDeclaration.Name = "colIsDeclaration";
            this.colIsDeclaration.Visible = true;
            this.colIsDeclaration.VisibleIndex = 25;
            // 
            // colIsInsurance
            // 
            this.colIsInsurance.FieldName = "IsInsurance";
            this.colIsInsurance.Name = "colIsInsurance";
            this.colIsInsurance.Visible = true;
            this.colIsInsurance.VisibleIndex = 26;
            // 
            // colIsTruck
            // 
            this.colIsTruck.FieldName = "IsTruck";
            this.colIsTruck.Name = "colIsTruck";
            this.colIsTruck.Visible = true;
            this.colIsTruck.VisibleIndex = 27;
            // 
            // colMarks
            // 
            this.colMarks.FieldName = "Marks";
            this.colMarks.Name = "colMarks";
            this.colMarks.Visible = true;
            this.colMarks.VisibleIndex = 28;
            // 
            // rcmbState
            // 
            this.rcmbState.AutoHeight = false;
            this.rcmbState.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbState.Name = "rcmbState";
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // cmbOEOperationType
            // 
            this.cmbOEOperationType.AutoHeight = false;
            this.cmbOEOperationType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbOEOperationType.Name = "cmbOEOperationType";
            // 
            // colFBAFreightMethodTypeName
            // 
            this.colFBAFreightMethodTypeName.Caption = "FBA Freight Method";
            this.colFBAFreightMethodTypeName.FieldName = "FBAFreightMethodTypeName";
            this.colFBAFreightMethodTypeName.Name = "colFBAFreightMethodTypeName";
            this.colFBAFreightMethodTypeName.Visible = true;
            this.colFBAFreightMethodTypeName.VisibleIndex = 4;
            // 
            // PartList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Name = "PartList";
            this.Size = new System.Drawing.Size(707, 376);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOEOperationType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsList;
        protected ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        protected DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbState;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbOEOperationType;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colBookingName;
        private DevExpress.XtraGrid.Columns.GridColumn colBookingDate;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colShipperName;
        private DevExpress.XtraGrid.Columns.GridColumn colConsigneeName;
        private DevExpress.XtraGrid.Columns.GridColumn colPOLName;
        private DevExpress.XtraGrid.Columns.GridColumn colPOLAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colPODName;
        private DevExpress.XtraGrid.Columns.GridColumn colPODAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colWeight;
        private DevExpress.XtraGrid.Columns.GridColumn colMeasurement;
        private DevExpress.XtraGrid.Columns.GridColumn colETDForPOL;
        private DevExpress.XtraGrid.Columns.GridColumn colETAForPOD;
        private DevExpress.XtraGrid.Columns.GridColumn colContainers;
        private DevExpress.XtraGrid.Columns.GridColumn colBookingNo;
        private DevExpress.XtraGrid.Columns.GridColumn colIncoTermName;
        private DevExpress.XtraGrid.Columns.GridColumn colIsDeclaration;
        private DevExpress.XtraGrid.Columns.GridColumn colIsInsurance;
        private DevExpress.XtraGrid.Columns.GridColumn colIsTruck;
        private DevExpress.XtraGrid.Columns.GridColumn colMarks;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantityUnitName;
        private DevExpress.XtraGrid.Columns.GridColumn colWeightUnitName;
        private DevExpress.XtraGrid.Columns.GridColumn colMeasurementUnitName;
        private DevExpress.XtraGrid.Columns.GridColumn colFreightMethodTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colTradeTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colShipmentTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colTransportClauseName;
        private DevExpress.XtraGrid.Columns.GridColumn colFBAFreightMethodTypeName;
    }
}
