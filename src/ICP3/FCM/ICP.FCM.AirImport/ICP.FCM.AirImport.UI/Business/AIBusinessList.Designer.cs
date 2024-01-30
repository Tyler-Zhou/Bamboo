namespace ICP.FCM.AirImport.UI
{
    partial class OIBusinessList
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
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colRefNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMBLNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPaid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReleased = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAgent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOLFilerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSales = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFlightNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coShipper = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConsignee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeparture = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDetination = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPlaceOfDeliveryName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDETA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colITNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQTY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWeight = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMeasurement = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFilerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReleaseDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOBLRcved = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateByName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcmbState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).BeginInit();
            this.SuspendLayout();
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
            this.cmbState});
            this.gcMain.Size = new System.Drawing.Size(670, 489);
            this.gcMain.TabIndex = 1;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FCM.AirImport.ServiceInterface.AirBusinessList);
            this.bsList.PositionChanged += new System.EventHandler(this.bsList_PositionChanged);
            // 
            // gvMain
            // 
            this.gvMain.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colState,
            this.colRefNo,
            this.colMBLNo,
            this.colSubNo,
            this.colPaid,
            this.colReleased,
            this.colAgent,
            this.colPOLFilerName,
            this.colCustomer,
            this.colCustomerNo,
            this.colSales,
            this.colFlightNo,
            this.coShipper,
            this.colConsignee,
            this.colDeparture,
            this.colETD,
            this.colDetination,
            this.colETA,
            this.colPlaceOfDeliveryName,
            this.colDETA,
            this.colITNO,
            this.colQTY,
            this.colWeight,
            this.colMeasurement,
            this.colFilerName,
            this.colCreateDate,
            this.colReleaseDate,
            this.colOBLRcved,
            this.colUpdateByName,
            this.colUpdateDate
            });
            this.gvMain.GridControl = this.gcMain;
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
            this.gvMain.DoubleClick += new System.EventHandler(this.gvMain_DoubleClick);
            this.gvMain.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvMain_RowCellClick);
            this.gvMain.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvMain_RowStyle);
            // 
            // colState
            // 
            this.colState.Caption = "状态";
            this.colState.ColumnEdit = this.cmbState;
            this.colState.FieldName = "State";
            this.colState.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colState.Name = "colState";
            this.colState.OptionsColumn.AllowMove = false;
            this.colState.Visible = true;
            this.colState.VisibleIndex = 0;
            this.colState.Width = 51;
            // 
            // cmbState
            // 
            this.cmbState.AutoHeight = false;
            this.cmbState.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbState.Name = "cmbState";
            // 
            // colRefNo
            // 
            this.colRefNo.Caption = "业务号";
            this.colRefNo.FieldName = "No";
            this.colRefNo.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colRefNo.Name = "colRefNo";
            this.colRefNo.OptionsColumn.AllowMove = false;
            this.colRefNo.Visible = true;
            this.colRefNo.VisibleIndex = 1;
            this.colRefNo.Width = 120;
            // 
            // colMBLNo
            // 
            this.colMBLNo.Caption = "主提单号";
            this.colMBLNo.FieldName = "MBLNo";
            this.colMBLNo.Name = "colMBLNo";
            this.colMBLNo.Visible = true;
            this.colMBLNo.VisibleIndex = 2;
            this.colMBLNo.Width = 120;
            // 
            // colSubNo
            // 
            this.colSubNo.Caption = "分提单号";
            this.colSubNo.FieldName = "SubNo";
            this.colSubNo.Name = "colSubNo";
            this.colSubNo.Visible = true;
            this.colSubNo.VisibleIndex = 3;
            this.colSubNo.Width = 120;
            // 
            // colPaid
            // 
            this.colPaid.Caption = "付款";
            this.colPaid.FieldName = "Paid";
            this.colPaid.Name = "colPaid";
            this.colPaid.Visible = true;
            this.colPaid.VisibleIndex = 4;
            // 
            // colReleased
            // 
            this.colReleased.Caption = "电放";
            this.colReleased.FieldName = "IsTelex";
            this.colReleased.Name = "colReleased";
            this.colReleased.Visible = true;
            this.colReleased.VisibleIndex = 5;
            // 
            // colAgent
            // 
            this.colAgent.Caption = "代理";
            this.colAgent.FieldName = "AgentName";
            this.colAgent.Name = "colAgent";
            this.colAgent.Visible = true;
            this.colAgent.VisibleIndex = 7;
            this.colAgent.Width = 120;
            // 
            // colPOLFilerName
            // 
            this.colPOLFilerName.Caption = "港前客服";
            this.colPOLFilerName.FieldName = "POLFilerName";
            this.colPOLFilerName.Name = "colPOLFilerName";
            this.colPOLFilerName.Visible = true;
            this.colPOLFilerName.VisibleIndex = 8;
            // 
            // colCustomer
            // 
            this.colCustomer.Caption = "客户";
            this.colCustomer.FieldName = "CustomerName";
            this.colCustomer.Name = "colCustomer";
            this.colCustomer.Visible = true;
            this.colCustomer.VisibleIndex = 9;
            this.colCustomer.Width = 120;
            // 
            // colCustomerNo
            // 
            this.colCustomerNo.Caption = "客户参考号";
            this.colCustomerNo.FieldName = "CustomerNo";
            this.colCustomerNo.Name = "colCustomerNo";
            this.colCustomerNo.Visible = true;
            this.colCustomerNo.VisibleIndex = 10;
            // 
            // colSales
            // 
            this.colSales.Caption = "揽货人";
            this.colSales.FieldName = "SalesName";
            this.colSales.Name = "colSales";
            this.colSales.Visible = true;
            this.colSales.VisibleIndex = 11;
            // 
            // colFlightNo
            // 
            this.colFlightNo.Caption = "航班号";
            this.colFlightNo.FieldName = "FlightNo";
            this.colFlightNo.Name = "colFlightNo";
            this.colFlightNo.Visible = true;
            this.colFlightNo.VisibleIndex = 12;
            this.colFlightNo.Width = 100;
            // 
            // coShipper
            // 
            this.coShipper.Caption = "发货人";
            this.coShipper.FieldName = "ShipperName";
            this.coShipper.Name = "coShipper";
            this.coShipper.Visible = true;
            this.coShipper.VisibleIndex = 13;
            // 
            // colConsignee
            // 
            this.colConsignee.Caption = "收货人";
            this.colConsignee.FieldName = "ConsigneeName";
            this.colConsignee.Name = "colConsignee";
            this.colConsignee.Visible = true;
            this.colConsignee.VisibleIndex = 14;
            // 
            // colDeparture
            // 
            this.colDeparture.Caption = "起运港";
            this.colDeparture.FieldName = "POLName";
            this.colDeparture.Name = "colDeparture";
            this.colDeparture.Visible = true;
            this.colDeparture.VisibleIndex = 15;
            // 
            // colETD
            // 
            this.colETD.Caption = "起航日";
            this.colETD.FieldName = "ETD";
            this.colETD.Name = "colETD";
            this.colETD.Visible = true;
            this.colETD.VisibleIndex = 17;
            // 
            // colDetination
            // 
            this.colDetination.Caption = "目的港";
            this.colDetination.FieldName = "PODName";
            this.colDetination.Name = "colDetination";
            this.colDetination.Visible = true;
            this.colDetination.VisibleIndex = 16;
            // 
            // colETA
            // 
            this.colETA.Caption = "到达日 ";
            this.colETA.FieldName = "ETA";
            this.colETA.Name = "colETA";
            this.colETA.Visible = true;
            this.colETA.VisibleIndex = 18;
            // 
            // colPlaceOfDeliveryName
            // 
            this.colPlaceOfDeliveryName.Caption = "交货地";
            this.colPlaceOfDeliveryName.FieldName = "PlaceOfDeliveryName";
            this.colPlaceOfDeliveryName.Name = "colPlaceOfDeliveryName";
            this.colPlaceOfDeliveryName.Visible = true;
            this.colPlaceOfDeliveryName.VisibleIndex = 19;
            // 
            // colDETA
            // 
            this.colDETA.Caption = "到交货地日";
            this.colDETA.FieldName = "DETA";
            this.colDETA.Name = "colDETA";
            this.colDETA.Visible = true;
            this.colDETA.VisibleIndex = 20;
            // 
            // colITNO
            // 
            this.colITNO.Caption = "I.T.NO";
            this.colITNO.FieldName = "ITNO";
            this.colITNO.Name = "colITNO";
            this.colITNO.Visible = true;
            this.colITNO.VisibleIndex = 21;
            // 
            // colQTY
            // 
            this.colQTY.Caption = "数量";
            this.colQTY.FieldName = "QuantityUnit";
            this.colQTY.Name = "colQTY";
            this.colQTY.Visible = true;
            this.colQTY.VisibleIndex = 22;
            // 
            // colWeight
            // 
            this.colWeight.Caption = "重量";
            this.colWeight.FieldName = "WeightUnit";
            this.colWeight.Name = "colWeight";
            this.colWeight.Visible = true;
            this.colWeight.VisibleIndex = 23;
            // 
            // colMeasurement
            // 
            this.colMeasurement.Caption = "体积";
            this.colMeasurement.FieldName = "MeasurementUnit";
            this.colMeasurement.Name = "colMeasurement";
            this.colMeasurement.Visible = true;
            this.colMeasurement.VisibleIndex = 24;
            this.colMeasurement.Width = 80;
            // 
            // colFilerName
            // 
            this.colFilerName.Caption = "客服";
            this.colFilerName.FieldName = "CustomerServiceName";
            this.colFilerName.Name = "colFilerName";
            this.colFilerName.Visible = true;
            this.colFilerName.VisibleIndex = 25;
            // 
            // colCreateDate
            // 
            this.colCreateDate.Caption = "创建日期";
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 26;
            // 
            // colReleaseDate
            // 
            this.colReleaseDate.Caption = "放货日期";
            this.colReleaseDate.FieldName = "ReleaseDate";
            this.colReleaseDate.Name = "colReleaseDate";
            this.colReleaseDate.Visible = true;
            this.colReleaseDate.VisibleIndex = 27;

            //
            //colUpdateByName
            //
            this.colUpdateByName.Caption = "更新人";
            this.colUpdateByName.FieldName = "UpdateByName";
            this.colUpdateByName.Name = "UpdateByName";
            this.colUpdateByName.Visible = true;
            this.colUpdateByName.VisibleIndex = 28;
            //
            //colUpdateDate
            //
            this.colUpdateDate.Caption = "更新时间";
            this.colUpdateDate.FieldName = "UpdateDate";
            this.colUpdateDate.Name = "UpdateDate";
            this.colUpdateDate.Visible = true;
            this.colUpdateDate.VisibleIndex = 29;

            // 
            // colOBLRcved
            // 
            this.colOBLRcved.Caption = "收到正本";
            this.colOBLRcved.FieldName = "OBLRcved";
            this.colOBLRcved.Name = "colOBLRcved";
            this.colOBLRcved.Visible = true;
            this.colOBLRcved.VisibleIndex = 6;
            // 
            // rcmbState
            // 
            this.rcmbState.AutoHeight = false;
            this.rcmbState.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbState.Name = "rcmbState";
            // 
            // OIBusinessList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Name = "OIBusinessList";
            this.Size = new System.Drawing.Size(670, 489);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        protected DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbState;
        private DevExpress.XtraGrid.Columns.GridColumn colRefNo;
        private DevExpress.XtraGrid.Columns.GridColumn colMBLNo;
        private DevExpress.XtraGrid.Columns.GridColumn colSubNo;
        private DevExpress.XtraGrid.Columns.GridColumn colAgent;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomer;
        private DevExpress.XtraGrid.Columns.GridColumn colPaid;
        private DevExpress.XtraGrid.Columns.GridColumn colReleased;
        private DevExpress.XtraGrid.Columns.GridColumn colFlightNo;
        private DevExpress.XtraGrid.Columns.GridColumn colDetination;
        private DevExpress.XtraGrid.Columns.GridColumn colITNO;
        private DevExpress.XtraGrid.Columns.GridColumn coShipper;
        private DevExpress.XtraGrid.Columns.GridColumn colConsignee;
        private DevExpress.XtraGrid.Columns.GridColumn colQTY;
        private DevExpress.XtraGrid.Columns.GridColumn colWeight;
        private DevExpress.XtraGrid.Columns.GridColumn colMeasurement;
        private DevExpress.XtraGrid.Columns.GridColumn colFilerName;
        private DevExpress.XtraGrid.Columns.GridColumn colSales;
        private DevExpress.XtraGrid.Columns.GridColumn colETA;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colETD;
        private DevExpress.XtraGrid.Columns.GridColumn colReleaseDate;
        private DevExpress.XtraGrid.Columns.GridColumn colFETA;
        private DevExpress.XtraGrid.Columns.GridColumn colDeparture;
        private DevExpress.XtraGrid.Columns.GridColumn colPlaceOfDeliveryName;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerNo;
        private DevExpress.XtraGrid.Columns.GridColumn colPOLFilerName;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbState;
        private DevExpress.XtraGrid.Columns.GridColumn colDETA;
        private DevExpress.XtraGrid.Columns.GridColumn colOBLRcved;

        private DevExpress.XtraGrid.Columns.GridColumn colUpdateByName;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateDate;

    }
}
