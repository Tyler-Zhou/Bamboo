namespace ICP.FRM.UI.ProfitRatios
{
    partial class PRListPart
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
            this.colComapnyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContractNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCarrierName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVesselName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoyageName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContainerDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBookingNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperationNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperationDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOLName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPODName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPlaceOfDeliveryName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAdjustmentAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUndoable = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colComapnyID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContractID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContractBaseItemID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVesselID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsNew = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsDirty = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FRM.ServiceInterface.DataObjects.BusinessStatisticsList);
            this.bsList.PositionChanged += new System.EventHandler(this.bsMainList_PositionChanged);
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(714, 248);
            this.gcMain.TabIndex = 0;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colComapnyName,
            this.colContractNo,
            this.colCarrierName,
            this.colVesselName,
            this.colVoyageName,
            this.colContainerDescription,
            this.colBookingNo,
            this.colOperationNo,
            this.colOperationDate,
            this.colPOLName,
            this.colPODName,
            this.colPlaceOfDeliveryName,
            this.colAdjustmentAmount,
            this.colUndoable,
            this.colComapnyID,
            this.colContractID,
            this.colContractBaseItemID,
            this.colVesselID,
            this.colIsNew,
            this.colIsDirty});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.IndicatorWidth = 35;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.ShowDetailButtons = false;
            this.gvMain.OptionsView.ShowFooter = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvMain_CustomDrawRowIndicator);
            this.gvMain.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(this.gvMain_BeforeLeaveRow);
            this.gvMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvMain_KeyDown);
            // 
            // colComapnyName
            // 
            this.colComapnyName.Caption = "Comapny Name";
            this.colComapnyName.FieldName = "CompanyName";
            this.colComapnyName.Name = "colComapnyName";
            this.colComapnyName.Visible = true;
            this.colComapnyName.VisibleIndex = 0;
            this.colComapnyName.Width = 150;
            // 
            // colContractNo
            // 
            this.colContractNo.Caption = "Contract No";
            this.colContractNo.FieldName = "ContractNo";
            this.colContractNo.Name = "colContractNo";
            this.colContractNo.Visible = true;
            this.colContractNo.VisibleIndex = 1;
            this.colContractNo.Width = 120;
            // 
            // colCarrierName
            // 
            this.colCarrierName.FieldName = "CarrierName";
            this.colCarrierName.Name = "colCarrierName";
            this.colCarrierName.Visible = true;
            this.colCarrierName.VisibleIndex = 2;
            this.colCarrierName.Width = 100;
            // 
            // colVesselName
            // 
            this.colVesselName.Caption = "Vessel Name";
            this.colVesselName.FieldName = "VesselName";
            this.colVesselName.Name = "colVesselName";
            this.colVesselName.Visible = true;
            this.colVesselName.VisibleIndex = 3;
            this.colVesselName.Width = 220;
            // 
            // colVoyageName
            // 
            this.colVoyageName.FieldName = "VoyageName";
            this.colVoyageName.Name = "colVoyageName";
            this.colVoyageName.Visible = true;
            this.colVoyageName.VisibleIndex = 4;
            this.colVoyageName.Width = 80;
            // 
            // colContainerDescription
            // 
            this.colContainerDescription.Caption = "Container Description";
            this.colContainerDescription.FieldName = "ContainerDescription";
            this.colContainerDescription.Name = "colContainerDescription";
            this.colContainerDescription.Visible = true;
            this.colContainerDescription.VisibleIndex = 5;
            this.colContainerDescription.Width = 300;
            // 
            // colBookingNo
            // 
            this.colBookingNo.FieldName = "BookingNo";
            this.colBookingNo.Name = "colBookingNo";
            this.colBookingNo.Visible = true;
            this.colBookingNo.VisibleIndex = 6;
            this.colBookingNo.Width = 90;
            // 
            // colOperationNo
            // 
            this.colOperationNo.FieldName = "OperationNo";
            this.colOperationNo.Name = "colOperationNo";
            this.colOperationNo.Visible = true;
            this.colOperationNo.VisibleIndex = 7;
            this.colOperationNo.Width = 90;
            // 
            // colOperationDate
            // 
            this.colOperationDate.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.colOperationDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colOperationDate.FieldName = "OperationDate";
            this.colOperationDate.Name = "colOperationDate";
            this.colOperationDate.Visible = true;
            this.colOperationDate.VisibleIndex = 8;
            this.colOperationDate.Width = 90;
            // 
            // colPOLName
            // 
            this.colPOLName.FieldName = "POLName";
            this.colPOLName.Name = "colPOLName";
            this.colPOLName.Visible = true;
            this.colPOLName.VisibleIndex = 9;
            this.colPOLName.Width = 100;
            // 
            // colPODName
            // 
            this.colPODName.FieldName = "PODName";
            this.colPODName.Name = "colPODName";
            this.colPODName.Visible = true;
            this.colPODName.VisibleIndex = 10;
            this.colPODName.Width = 100;
            // 
            // colPlaceOfDeliveryName
            // 
            this.colPlaceOfDeliveryName.FieldName = "PlaceOfDeliveryName";
            this.colPlaceOfDeliveryName.Name = "colPlaceOfDeliveryName";
            this.colPlaceOfDeliveryName.Visible = true;
            this.colPlaceOfDeliveryName.VisibleIndex = 11;
            this.colPlaceOfDeliveryName.Width = 100;
            // 
            // colAdjustmentAmount
            // 
            this.colAdjustmentAmount.Caption = "Adjustment Amount(USD)";
            this.colAdjustmentAmount.FieldName = "AdjustmentAmount";
            this.colAdjustmentAmount.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            this.colAdjustmentAmount.Name = "colAdjustmentAmount";
            this.colAdjustmentAmount.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colAdjustmentAmount.Visible = true;
            this.colAdjustmentAmount.VisibleIndex = 12;
            this.colAdjustmentAmount.Width = 90;
            // 
            // colUndoable
            // 
            this.colUndoable.FieldName = "Undoable";
            this.colUndoable.Name = "colUndoable";
            // 
            // colComapnyID
            // 
            this.colComapnyID.Caption = "Comapny ID";
            this.colComapnyID.FieldName = "CompanyID";
            this.colComapnyID.Name = "colComapnyID";
            // 
            // colContractID
            // 
            this.colContractID.Caption = "Contract ID";
            this.colContractID.FieldName = "ContractID";
            this.colContractID.Name = "colContractID";
            // 
            // colContractBaseItemID
            // 
            this.colContractBaseItemID.FieldName = "ContractBaseItemID";
            this.colContractBaseItemID.Name = "colContractBaseItemID";
            // 
            // colVesselID
            // 
            this.colVesselID.Caption = "Vessel ID";
            this.colVesselID.FieldName = "VesselID";
            this.colVesselID.Name = "colVesselID";
            // 
            // colIsNew
            // 
            this.colIsNew.Caption = "Is New";
            this.colIsNew.FieldName = "IsNew";
            this.colIsNew.Name = "colIsNew";
            this.colIsNew.OptionsColumn.ReadOnly = true;
            // 
            // colIsDirty
            // 
            this.colIsDirty.Caption = "Is Dirty";
            this.colIsDirty.FieldName = "IsDirty";
            this.colIsDirty.Name = "colIsDirty";
            // 
            // PRListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Name = "PRListPart";
            this.Size = new System.Drawing.Size(714, 248);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsList;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn colContractNo;
        private DevExpress.XtraGrid.Columns.GridColumn colVesselName;
        private DevExpress.XtraGrid.Columns.GridColumn colContainerDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colUndoable;
        private DevExpress.XtraGrid.Columns.GridColumn colContractID;
        private DevExpress.XtraGrid.Columns.GridColumn colVesselID;
        private DevExpress.XtraGrid.Columns.GridColumn colIsNew;
        private DevExpress.XtraGrid.Columns.GridColumn colIsDirty;
        private DevExpress.XtraGrid.Columns.GridColumn colComapnyName;
        private DevExpress.XtraGrid.Columns.GridColumn colComapnyID;
        private DevExpress.XtraGrid.Columns.GridColumn colPOLName;
        private DevExpress.XtraGrid.Columns.GridColumn colPODName;
        private DevExpress.XtraGrid.Columns.GridColumn colPlaceOfDeliveryName;
        private DevExpress.XtraGrid.Columns.GridColumn colContractBaseItemID;
        private DevExpress.XtraGrid.Columns.GridColumn colCarrierName;
        private DevExpress.XtraGrid.Columns.GridColumn colVoyageName;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationDate;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationNo;
        private DevExpress.XtraGrid.Columns.GridColumn colBookingNo;
        private DevExpress.XtraGrid.Columns.GridColumn colAdjustmentAmount;
    }
}
