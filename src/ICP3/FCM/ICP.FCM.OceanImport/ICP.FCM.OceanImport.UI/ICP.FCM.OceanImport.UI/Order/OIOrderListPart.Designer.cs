namespace ICP.FCM.OceanImport.UI
{
    partial class OIOrderListPart
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
            this.colCustomer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShipperName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConsigneeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPlaceOfReceiptName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPlaceOfDeliveryName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAgentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSalesName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOverSeasFilerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerContactName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBookType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colArriveDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReleaseDate = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.bsList.DataSource = typeof(ICP.FCM.OceanImport.ServiceInterface.OceanOrderList);
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
            this.colCustomer,
            this.colShipperName,
            this.colConsigneeName,
            this.colPlaceOfReceiptName,
            this.colPOL,
            this.colPOD,
            this.colPlaceOfDeliveryName,
            this.colAgentName,
            this.colSalesName,
            this.colOverSeasFilerName,
            this.colCustomerContactName,
            this.colBookType,
            this.colETA,
            this.colETD,
            this.colArriveDate,
            this.colReleaseDate,
            this.colUpdateByName,
            this.colUpdateDate});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.IndicatorWidth = 50;
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
            this.colState.Caption = "状态";
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
            this.colRefNo.Caption = "业务号";
            this.colRefNo.FieldName = "RefNo";
            this.colRefNo.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colRefNo.Name = "colRefNo";
            this.colRefNo.OptionsColumn.AllowMove = false;
            this.colRefNo.Visible = true;
            this.colRefNo.VisibleIndex = 1;
            this.colRefNo.Width = 120;
            // 
            // colCustomer
            // 
            this.colCustomer.Caption = "客户";
            this.colCustomer.FieldName = "CustomerName";
            this.colCustomer.Name = "colCustomer";
            this.colCustomer.Visible = true;
            this.colCustomer.VisibleIndex = 2;
            // 
            // colShipperName
            // 
            this.colShipperName.Caption = "发货人";
            this.colShipperName.FieldName = "ShipperName";
            this.colShipperName.Name = "colShipperName";
            this.colShipperName.Visible = true;
            this.colShipperName.VisibleIndex = 3;
            // 
            // colConsigneeName
            // 
            this.colConsigneeName.Caption = "收货人";
            this.colConsigneeName.FieldName = "ConsigneeName";
            this.colConsigneeName.Name = "colConsigneeName";
            this.colConsigneeName.Visible = true;
            this.colConsigneeName.VisibleIndex = 4;
            // 
            // colPlaceOfReceiptName
            // 
            this.colPlaceOfReceiptName.Caption = "收货地";
            this.colPlaceOfReceiptName.FieldName = "PlaceOfReceiptName";
            this.colPlaceOfReceiptName.Name = "colPlaceOfReceiptName";
            this.colPlaceOfReceiptName.Visible = true;
            this.colPlaceOfReceiptName.VisibleIndex = 5;
            // 
            // colPOL
            // 
            this.colPOL.Caption = "装货港";
            this.colPOL.FieldName = "POLName";
            this.colPOL.Name = "colPOL";
            this.colPOL.Visible = true;
            this.colPOL.VisibleIndex = 6;
            // 
            // colPOD
            // 
            this.colPOD.Caption = "卸货港";
            this.colPOD.FieldName = "PODName";
            this.colPOD.Name = "colPOD";
            this.colPOD.Visible = true;
            this.colPOD.VisibleIndex = 7;
            // 
            // colPlaceOfDeliveryName
            // 
            this.colPlaceOfDeliveryName.Caption = "交货地";
            this.colPlaceOfDeliveryName.FieldName = "PlaceOfDeliveryName";
            this.colPlaceOfDeliveryName.Name = "colPlaceOfDeliveryName";
            this.colPlaceOfDeliveryName.Visible = true;
            this.colPlaceOfDeliveryName.VisibleIndex = 8;
            // 
            // colAgentName
            // 
            this.colAgentName.Caption = "代理";
            this.colAgentName.FieldName = "AgentName";
            this.colAgentName.Name = "colAgentName";
            this.colAgentName.Visible = true;
            this.colAgentName.VisibleIndex = 9;
            // 
            // colSalesName
            // 
            this.colSalesName.Caption = "揽货人";
            this.colSalesName.FieldName = "SalesName";
            this.colSalesName.Name = "colSalesName";
            this.colSalesName.Visible = true;
            this.colSalesName.VisibleIndex = 10;
            // 
            // colOverSeasFilerName
            // 
            this.colOverSeasFilerName.Caption = "海外部客服";
            this.colOverSeasFilerName.FieldName = "OverSeasFilerName";
            this.colOverSeasFilerName.Name = "colOverSeasFilerName";
            this.colOverSeasFilerName.Visible = true;
            this.colOverSeasFilerName.VisibleIndex = 11;
            // 
            // colCustomerContactName
            // 
            this.colCustomerContactName.Caption = "客服";
            this.colCustomerContactName.FieldName = "CustomerContactName";
            this.colCustomerContactName.Name = "colCustomerContactName";
            this.colCustomerContactName.Visible = true;
            this.colCustomerContactName.VisibleIndex = 12;
            // 
            // colBookType
            // 
            this.colBookType.Caption = "业务类型";
            this.colBookType.FieldName = "BookingTypeDescription";
            this.colBookType.Name = "colBookType";
            this.colBookType.Visible = true;
            this.colBookType.VisibleIndex = 13;
            // 
            // colETA
            // 
            this.colETA.Caption = "ETA";
            this.colETA.FieldName = "ETA";
            this.colETA.Name = "colETA";
            this.colETA.Visible = true;
            this.colETA.VisibleIndex = 14;
            // 
            // colETD
            // 
            this.colETD.Caption = "ETD";
            this.colETD.FieldName = "ETD";
            this.colETD.Name = "colETD";
            this.colETD.Visible = true;
            this.colETD.VisibleIndex = 15;
            // 
            // colArriveDate
            // 
            this.colArriveDate.Caption = "到交货地日";
            this.colArriveDate.FieldName = "ArriveDate";
            this.colArriveDate.Name = "colArriveDate";
            this.colArriveDate.Visible = true;
            this.colArriveDate.VisibleIndex = 16;
            // 
            // colReleaseDate
            // 
            this.colReleaseDate.Caption = "放货日";
            this.colReleaseDate.FieldName = "DeliveryOfGoodsDate";
            this.colReleaseDate.Name = "colReleaseDate";
            this.colReleaseDate.Visible = true;
            this.colReleaseDate.VisibleIndex = 17;
            // 
            // colUpdateByName
            // 
            this.colUpdateByName.Caption = "UpdateByName";
            this.colUpdateByName.FieldName = "UpdateByName";
            this.colUpdateByName.Name = "colUpdateByName";
            this.colUpdateByName.Visible = true;
            this.colUpdateByName.VisibleIndex = 18;
            // 
            // colUpdateDate
            // 
            this.colUpdateDate.FieldName = "UpdateDate";
            this.colUpdateDate.Name = "colUpdateDate";
            this.colUpdateDate.Visible = true;
            this.colUpdateDate.VisibleIndex = 19;
            // 
            // OIOrderListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Name = "OIOrderListPart";
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
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbState;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomer;
        private DevExpress.XtraGrid.Columns.GridColumn colShipperName;
        private DevExpress.XtraGrid.Columns.GridColumn colConsigneeName;
        private DevExpress.XtraGrid.Columns.GridColumn colPlaceOfReceiptName;
        private DevExpress.XtraGrid.Columns.GridColumn colPOL;
        private DevExpress.XtraGrid.Columns.GridColumn colPOD;
        private DevExpress.XtraGrid.Columns.GridColumn colPlaceOfDeliveryName;
        private DevExpress.XtraGrid.Columns.GridColumn colAgentName;
        private DevExpress.XtraGrid.Columns.GridColumn colSalesName;
        private DevExpress.XtraGrid.Columns.GridColumn colOverSeasFilerName;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerContactName;
        private DevExpress.XtraGrid.Columns.GridColumn colBookType;
        private DevExpress.XtraGrid.Columns.GridColumn colETA;
        private DevExpress.XtraGrid.Columns.GridColumn colETD;
        private DevExpress.XtraGrid.Columns.GridColumn colArriveDate;
        private DevExpress.XtraGrid.Columns.GridColumn colReleaseDate;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateByName;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateDate;
    }
}
