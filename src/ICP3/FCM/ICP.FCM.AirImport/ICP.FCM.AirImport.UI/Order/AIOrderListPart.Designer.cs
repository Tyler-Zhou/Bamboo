namespace ICP.FCM.AirImport.UI
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
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShipperName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPlaceOfReceiptName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeparture = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDetination = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConsigneeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSalesName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colClosingDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSODate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerContactName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colArriveDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAirCompany = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.bsList.DataSource = typeof(ICP.FCM.AirImport.ServiceInterface.AirOrderList);
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
            this.gridColumn1,
            this.colCustomerName,
            this.colAirCompany,
            this.colUpdateByName,
            this.colUpdateDate,
            this.colShipperName,
            this.colPlaceOfReceiptName,
            this.colDeparture,
            this.colDetination,
            this.colConsigneeName,
            this.colSalesName,
            this.colClosingDate,
            this.colSODate,
            this.colETD,
            this.gridColumn2,
            this.colCustomerContactName,
            this.colArriveDate});
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
            // gridColumn1
            // 
            this.gridColumn1.Caption = "代理";
            this.gridColumn1.FieldName = "AgentName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 9;
            // 
            // colCustomerName
            // 
            this.colCustomerName.Caption = "客户";
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 2;
            this.colCustomerName.Width = 120;
            // 
            // colShipperName
            // 
            this.colShipperName.Caption = "发货人";
            this.colShipperName.FieldName = "ShipperName";
            this.colShipperName.Name = "colShipperName";
            this.colShipperName.Visible = true;
            this.colShipperName.VisibleIndex = 4;
            this.colShipperName.Width = 120;
            // 
            // colPlaceOfReceiptName
            // 
            this.colPlaceOfReceiptName.Caption = "收货人";
            this.colPlaceOfReceiptName.FieldName = "ConsigneeName";
            this.colPlaceOfReceiptName.Name = "colPlaceOfReceiptName";
            this.colPlaceOfReceiptName.Visible = true;
            this.colPlaceOfReceiptName.VisibleIndex = 5;
            this.colPlaceOfReceiptName.Width = 120;
            // 
            // colDeparture
            // 
            this.colDeparture.Caption = "起运港";
            this.colDeparture.FieldName = "POLName";
            this.colDeparture.Name = "colDeparture";
            this.colDeparture.Visible = true;
            this.colDeparture.VisibleIndex = 6;
            this.colDeparture.Width = 120;
            // 
            // colDetination
            // 
            this.colDetination.Caption = "目的港";
            this.colDetination.FieldName = "PODName";
            this.colDetination.Name = "colDetination";
            this.colDetination.Visible = true;
            this.colDetination.VisibleIndex = 7;
            // 
            // colConsigneeName
            // 
            this.colConsigneeName.Caption = "交货地";
            this.colConsigneeName.FieldName = "PlaceOfDeliveryName";
            this.colConsigneeName.Name = "colConsigneeName";
            this.colConsigneeName.Visible = true;
            this.colConsigneeName.VisibleIndex = 8;
            this.colConsigneeName.Width = 120;
            // 
            // colSalesName
            // 
            this.colSalesName.Caption = "揽货人";
            this.colSalesName.FieldName = "SalesName";
            this.colSalesName.Name = "colSalesName";
            this.colSalesName.Visible = true;
            this.colSalesName.VisibleIndex = 10;
            this.colSalesName.Width = 80;
            // 
            // colClosingDate
            // 
            this.colClosingDate.Caption = "起航日";
            this.colClosingDate.FieldName = "ETD";
            this.colClosingDate.Name = "colClosingDate";
            this.colClosingDate.Visible = true;
            this.colClosingDate.VisibleIndex = 13;
            // 
            // colSODate
            // 
            this.colSODate.Caption = "创建日";
            this.colSODate.FieldName = "CreateDate";
            this.colSODate.Name = "colSODate";
            this.colSODate.Visible = true;
            this.colSODate.VisibleIndex = 12;
            // 
            // colETD
            // 
            this.colETD.Caption = "放货日";
            this.colETD.FieldName = "DeliveryOfGoodsDate";
            this.colETD.Name = "colETD";
            this.colETD.Visible = true;
            this.colETD.VisibleIndex = 16;

            //
            // colUpdateByName
            //
            this.colUpdateByName.Caption = "更新人";
            this.colUpdateByName.FieldName = "UpdateByName";
            this.colUpdateByName.Name = "colUpdateByName";
            this.colUpdateByName.VisibleIndex = 17;
            this.colUpdateByName.Visible = true;

            //
            // colUpdateDate
            //
            this.colUpdateDate.Caption = "更新时间";
            this.colUpdateDate.FieldName = "UpdateDate";
            this.colUpdateDate.Name = "colUpdateDate";
            this.colUpdateDate.VisibleIndex = 18;
            this.colUpdateDate.Visible = true;

            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "到达日";
            this.gridColumn2.FieldName = "ETA";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 14;
            // 
            // colCustomerContactName
            // 
            this.colCustomerContactName.Caption = "客服";
            this.colCustomerContactName.FieldName = "CustomerContactName";
            this.colCustomerContactName.Name = "colCustomerContactName";
            this.colCustomerContactName.Visible = true;
            this.colCustomerContactName.VisibleIndex = 11;
            // 
            // colArriveDate
            // 
            this.colArriveDate.Caption = "到交货地日";
            this.colArriveDate.FieldName = "ArriveDate";
            this.colArriveDate.Name = "colArriveDate";
            this.colArriveDate.Visible = true;
            this.colArriveDate.VisibleIndex = 15;
            // 
            // colAirCompany
            // 
            this.colAirCompany.Caption = "航空公司";
            this.colAirCompany.FieldName = "AirCompanyName";
            this.colAirCompany.Name = "colAirCompany";
            this.colAirCompany.Visible = true;
            this.colAirCompany.VisibleIndex = 3;

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
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colShipperName;
        private DevExpress.XtraGrid.Columns.GridColumn colConsigneeName;
        private DevExpress.XtraGrid.Columns.GridColumn colPlaceOfReceiptName;
        private DevExpress.XtraGrid.Columns.GridColumn colDeparture;
        private DevExpress.XtraGrid.Columns.GridColumn colSalesName;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbState;
        private DevExpress.XtraGrid.Columns.GridColumn colDetination;
        private DevExpress.XtraGrid.Columns.GridColumn colETD;
        private DevExpress.XtraGrid.Columns.GridColumn colSODate;
        private DevExpress.XtraGrid.Columns.GridColumn colClosingDate;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerContactName;
        private DevExpress.XtraGrid.Columns.GridColumn colArriveDate;
        private DevExpress.XtraGrid.Columns.GridColumn colAirCompany;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateByName;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateDate;
    }
}
