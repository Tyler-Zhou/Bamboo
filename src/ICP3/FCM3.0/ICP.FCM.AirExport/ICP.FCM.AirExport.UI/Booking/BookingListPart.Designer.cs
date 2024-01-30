namespace ICP.FCM.AirExport.UI.Booking
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
            this.colBookingCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFilightNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMBLNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHBLNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeparture = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDetination = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShipperName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConsigneeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colClosingDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSODate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAgentOfCarrierName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAirCompany = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSalesName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBookingerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFiler = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAgent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPlaceOfDelivery = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.bsList.DataSource = typeof(ICP.FCM.AirExport.ServiceInterface.DataObjects.AirBookingList);
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
            this.colBookingCustomerName,
            this.colFilightNo,
            this.colMBLNo,
            this.colHBLNo,
            this.colDeparture,
            this.colDetination,
            this.colETD,
            this.colETA,
            this.colShipperName,
            this.colConsigneeName,
            this.colClosingDate,
            this.colSODate,
            this.colCreateDate,
            this.colAgentOfCarrierName,
            this.colAirCompany,
            this.colSalesName,
            this.colBookingerName,
            this.colFiler,
            this.colAgent,
            this.colPlaceOfDelivery,
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
            // colBookingCustomerName
            // 
            this.colBookingCustomerName.FieldName = "BookingCustomerName";
            this.colBookingCustomerName.Name = "colBookingCustomerName";
            this.colBookingCustomerName.Visible = true;
            this.colBookingCustomerName.VisibleIndex = 7;
            // 
            // colFilightNo
            // 
            this.colFilightNo.FieldName = "FilightNo";
            this.colFilightNo.Name = "colFilightNo";
            this.colFilightNo.Visible = true;
            this.colFilightNo.VisibleIndex = 6;
            this.colFilightNo.Width = 120;
            // 
            // colMBLNo
            // 
            this.colMBLNo.FieldName = "MBLNo";
            this.colMBLNo.Name = "colMBLNo";
            this.colMBLNo.OptionsColumn.FixedWidth = true;
            this.colMBLNo.Visible = true;
            this.colMBLNo.VisibleIndex = 4;
            this.colMBLNo.Width = 120;
            // 
            // colHBLNo
            // 
            this.colHBLNo.FieldName = "HBLNo";
            this.colHBLNo.Name = "colHBLNo";
            this.colHBLNo.OptionsColumn.FixedWidth = true;
            this.colHBLNo.Visible = true;
            this.colHBLNo.VisibleIndex = 5;
            this.colHBLNo.Width = 120;
            // 
            // colDeparture
            // 
            this.colDeparture.Caption = "Departure";
            this.colDeparture.FieldName = "DepartureName";
            this.colDeparture.Name = "colDeparture";
            this.colDeparture.Visible = true;
            this.colDeparture.VisibleIndex = 8;
            this.colDeparture.Width = 120;
            // 
            // colDetination
            // 
            this.colDetination.Caption = "Detination";
            this.colDetination.FieldName = "DetinationName";
            this.colDetination.Name = "colDetination";
            this.colDetination.Visible = true;
            this.colDetination.VisibleIndex = 9;
            this.colDetination.Width = 120;
            // 
            // colETD
            // 
            this.colETD.FieldName = "ETD";
            this.colETD.Name = "colETD";
            this.colETD.Visible = true;
            this.colETD.VisibleIndex = 11;
            this.colETD.Width = 80;
            // 
            // colETA
            // 
            this.colETA.Caption = "ETA";
            this.colETA.FieldName = "ETA";
            this.colETA.Name = "colETA";
            this.colETA.Visible = true;
            this.colETA.VisibleIndex = 12;
            // 
            // colShipperName
            // 
            this.colShipperName.FieldName = "ShipperName";
            this.colShipperName.Name = "colShipperName";
            this.colShipperName.Visible = true;
            this.colShipperName.VisibleIndex = 13;
            // 
            // colConsigneeName
            // 
            this.colConsigneeName.FieldName = "ConsigneeName";
            this.colConsigneeName.Name = "colConsigneeName";
            this.colConsigneeName.Visible = true;
            this.colConsigneeName.VisibleIndex = 14;
            // 
            // colClosingDate
            // 
            this.colClosingDate.FieldName = "ClosingDate";
            this.colClosingDate.Name = "colClosingDate";
            this.colClosingDate.Visible = true;
            this.colClosingDate.VisibleIndex = 17;
            // 
            // colSODate
            // 
            this.colSODate.FieldName = "SODate";
            this.colSODate.Name = "colSODate";
            this.colSODate.Visible = true;
            this.colSODate.VisibleIndex = 18;
            // 
            // colCreateDate
            // 
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 22;
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
            // colAirCompany
            // 
            this.colAirCompany.Caption = "AirCompany";
            this.colAirCompany.FieldName = "AirCompanyName";
            this.colAirCompany.Name = "colAirCompany";
            this.colAirCompany.Visible = true;
            this.colAirCompany.VisibleIndex = 16;
            this.colAirCompany.Width = 120;
            // 
            // colSalesName
            // 
            this.colSalesName.Caption = "Sales";
            this.colSalesName.FieldName = "SalesName";
            this.colSalesName.Name = "colSalesName";
            this.colSalesName.Visible = true;
            this.colSalesName.VisibleIndex = 19;
            this.colSalesName.Width = 80;
            // 
            // colBookingerName
            // 
            this.colBookingerName.Caption = "Booking";
            this.colBookingerName.FieldName = "BookingerName";
            this.colBookingerName.Name = "colBookingerName";
            this.colBookingerName.Visible = true;
            this.colBookingerName.VisibleIndex = 20;
            // 
            // colFiler
            // 
            this.colFiler.Caption = "Filer";
            this.colFiler.FieldName = "FilerName";
            this.colFiler.Name = "colFiler";
            this.colFiler.Visible = true;
            this.colFiler.VisibleIndex = 21;
            // 
            // colAgent
            // 
            this.colAgent.Caption = "Agent";
            this.colAgent.FieldName = "AgentName";
            this.colAgent.Name = "colAgent";
            this.colAgent.Visible = true;
            this.colAgent.VisibleIndex = 3;
            // 
            // colPlaceOfDelivery
            // 
            this.colPlaceOfDelivery.Caption = "PlaceOfDelivery";
            this.colPlaceOfDelivery.FieldName = "PlaceOfDeliveryName";
            this.colPlaceOfDelivery.Name = "colPlaceOfDelivery";
            this.colPlaceOfDelivery.Visible = true;
            this.colPlaceOfDelivery.VisibleIndex = 10;
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
            this.colUpdateByName.VisibleIndex = 23;
            // 
            // colUpdateDate
            // 
            this.colUpdateDate.FieldName = "UpdateDate";
            this.colUpdateDate.Name = "colUpdateDate";
            this.colUpdateDate.Visible = true;
            this.colUpdateDate.VisibleIndex = 24;
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
        private DevExpress.XtraGrid.Columns.GridColumn colMBLNo;
        private DevExpress.XtraGrid.Columns.GridColumn colHBLNo;
        private DevExpress.XtraGrid.Columns.GridColumn colFilightNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colAirCompany;
        private DevExpress.XtraGrid.Columns.GridColumn colAgentOfCarrierName;
        private DevExpress.XtraGrid.Columns.GridColumn colDeparture;
        private DevExpress.XtraGrid.Columns.GridColumn colDetination;
        private DevExpress.XtraGrid.Columns.GridColumn colETD;
        private DevExpress.XtraGrid.Columns.GridColumn colSalesName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        private DevExpress.XtraGrid.Columns.GridColumn colETA;
        private DevExpress.XtraGrid.Columns.GridColumn colBookingerName;
        private DevExpress.XtraGrid.Columns.GridColumn colFiler;
        private DevExpress.XtraGrid.Columns.GridColumn colShipperName;
        private DevExpress.XtraGrid.Columns.GridColumn colConsigneeName;
        private DevExpress.XtraGrid.Columns.GridColumn colClosingDate;
        private DevExpress.XtraGrid.Columns.GridColumn colSODate;
        private DevExpress.XtraEditors.PopupContainerControl pccHyperLinks;
        private DevExpress.XtraEditors.HyperLinkEdit hyperLinkEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colAgent;
        private DevExpress.XtraGrid.Columns.GridColumn colPlaceOfDelivery;
        private DevExpress.XtraGrid.Columns.GridColumn colBookingCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateByName;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateDate;
    }
}
