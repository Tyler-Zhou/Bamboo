﻿namespace ICP.FCM.OceanImport.UI
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
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsList = new System.Windows.Forms.BindingSource();
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colRefNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMBLNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPaid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContainerNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOLFilerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAgent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSales = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVesselVoyage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coShipper = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConsignee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPlaceOfDeliveryName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDETA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFinalDestination = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colITNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQTY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWeight = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMeasurement = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFilerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOverSeasFilerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsWirteOff = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsPaid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReleased = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOBLRcved = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsRelease = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsReceiveNotice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsApplyRC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsNoticeRelease = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsNoticePay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsAgreeRC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsReleaseCargo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsSentAN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTelexNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReleaseBLDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReleaseDate = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.bsList.DataSource = typeof(ICP.FCM.OceanImport.ServiceInterface.OceanBusinessList);
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
            this.colCustomer,
            this.colETA,
            this.colContainerNo,
            this.colPOLFilerName,
            this.colAgent,
            this.colCustomerNo,
            this.colSales,
            this.colVesselVoyage,
            this.coShipper,
            this.colConsignee,
            this.colPOL,
            this.colETD,
            this.colPOD,
            this.colPlaceOfDeliveryName,
            this.colDETA,
            this.colFinalDestination,
            this.colITNO,
            this.colQTY,
            this.colWeight,
            this.colMeasurement,
            this.colFilerName,
            this.colOverSeasFilerName,
            this.colCreateDate,
            this.colIsWirteOff,
            this.colIsPaid,
            this.colReleased,
            this.colOBLRcved,
            this.colIsRelease,
            this.colIsReceiveNotice,
            this.colIsApplyRC,
            this.colIsNoticeRelease,
            this.colIsNoticePay,
            this.colIsAgreeRC,
            this.colIsReleaseCargo,
            this.colIsSentAN,
            this.colTelexNo,
            this.colReleaseBLDate,
            this.colReleaseDate,
            this.colUpdateByName,
            this.colUpdateDate});
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
            this.gvMain.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvMain_RowCellClick);
            this.gvMain.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvMain_CustomDrawRowIndicator);
            this.gvMain.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvMain_RowStyle);
            this.gvMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvMain_KeyDown);
            this.gvMain.DoubleClick += new System.EventHandler(this.gvMain_DoubleClick);
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
            this.colPaid.FieldName = "IsPaid";
            this.colPaid.Name = "colPaid";
            this.colPaid.Visible = true;
            this.colPaid.VisibleIndex = 5;
            // 
            // colCustomer
            // 
            this.colCustomer.Caption = "客户";
            this.colCustomer.FieldName = "CustomerName";
            this.colCustomer.Name = "colCustomer";
            this.colCustomer.Visible = true;
            this.colCustomer.VisibleIndex = 6;
            this.colCustomer.Width = 120;
            // 
            // colETA
            // 
            this.colETA.Caption = "到港日";
            this.colETA.FieldName = "ETA";
            this.colETA.Name = "colETA";
            this.colETA.Visible = true;
            this.colETA.VisibleIndex = 7;
            // 
            // colContainerNo
            // 
            this.colContainerNo.Caption = "箱号";
            this.colContainerNo.FieldName = "ContainerNo";
            this.colContainerNo.Name = "colContainerNo";
            this.colContainerNo.OptionsColumn.FixedWidth = true;
            this.colContainerNo.Visible = true;
            this.colContainerNo.VisibleIndex = 8;
            this.colContainerNo.Width = 100;
            // 
            // colPOLFilerName
            // 
            this.colPOLFilerName.Caption = "港前客服";
            this.colPOLFilerName.FieldName = "POLFilerName";
            this.colPOLFilerName.Name = "colPOLFilerName";
            this.colPOLFilerName.Visible = true;
            this.colPOLFilerName.VisibleIndex = 9;
            // 
            // colAgent
            // 
            this.colAgent.Caption = "代理";
            this.colAgent.FieldName = "AgentName";
            this.colAgent.Name = "colAgent";
            this.colAgent.Visible = true;
            this.colAgent.VisibleIndex = 10;
            this.colAgent.Width = 120;
            // 
            // colCustomerNo
            // 
            this.colCustomerNo.Caption = "客户参考号";
            this.colCustomerNo.FieldName = "CustomerNo";
            this.colCustomerNo.Name = "colCustomerNo";
            this.colCustomerNo.Visible = true;
            this.colCustomerNo.VisibleIndex = 11;
            // 
            // colSales
            // 
            this.colSales.Caption = "揽货人";
            this.colSales.FieldName = "SalesName";
            this.colSales.Name = "colSales";
            this.colSales.Visible = true;
            this.colSales.VisibleIndex = 12;
            // 
            // colVesselVoyage
            // 
            this.colVesselVoyage.Caption = "船名航次";
            this.colVesselVoyage.FieldName = "VesselVoyage";
            this.colVesselVoyage.Name = "colVesselVoyage";
            this.colVesselVoyage.Visible = true;
            this.colVesselVoyage.VisibleIndex = 13;
            this.colVesselVoyage.Width = 100;
            // 
            // coShipper
            // 
            this.coShipper.Caption = "发货人";
            this.coShipper.FieldName = "ShipperName";
            this.coShipper.Name = "coShipper";
            this.coShipper.Visible = true;
            this.coShipper.VisibleIndex = 14;
            // 
            // colConsignee
            // 
            this.colConsignee.Caption = "收货人";
            this.colConsignee.FieldName = "ConsigneeName";
            this.colConsignee.Name = "colConsignee";
            this.colConsignee.Visible = true;
            this.colConsignee.VisibleIndex = 15;
            // 
            // colPOL
            // 
            this.colPOL.Caption = "装货港";
            this.colPOL.FieldName = "POLName";
            this.colPOL.Name = "colPOL";
            this.colPOL.Visible = true;
            this.colPOL.VisibleIndex = 16;
            // 
            // colETD
            // 
            this.colETD.Caption = "离港日";
            this.colETD.FieldName = "ETD";
            this.colETD.Name = "colETD";
            this.colETD.Visible = true;
            this.colETD.VisibleIndex = 17;
            // 
            // colPOD
            // 
            this.colPOD.Caption = "卸货港";
            this.colPOD.FieldName = "PODName";
            this.colPOD.Name = "colPOD";
            this.colPOD.Visible = true;
            this.colPOD.VisibleIndex = 18;
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
            // colFinalDestination
            // 
            this.colFinalDestination.Caption = "最终目的地";
            this.colFinalDestination.FieldName = "FinalDestinationName";
            this.colFinalDestination.Name = "colFinalDestination";
            this.colFinalDestination.Visible = true;
            this.colFinalDestination.VisibleIndex = 21;
            // 
            // colITNO
            // 
            this.colITNO.Caption = "I.T.NO";
            this.colITNO.FieldName = "ITNO";
            this.colITNO.Name = "colITNO";
            this.colITNO.Visible = true;
            this.colITNO.VisibleIndex = 22;
            // 
            // colQTY
            // 
            this.colQTY.Caption = "数量";
            this.colQTY.FieldName = "QuantityUnit";
            this.colQTY.Name = "colQTY";
            this.colQTY.Visible = true;
            this.colQTY.VisibleIndex = 23;
            // 
            // colWeight
            // 
            this.colWeight.Caption = "重量";
            this.colWeight.FieldName = "WeightUnit";
            this.colWeight.Name = "colWeight";
            this.colWeight.Visible = true;
            this.colWeight.VisibleIndex = 24;
            // 
            // colMeasurement
            // 
            this.colMeasurement.Caption = "体积";
            this.colMeasurement.FieldName = "MeasurementUnit";
            this.colMeasurement.Name = "colMeasurement";
            this.colMeasurement.Visible = true;
            this.colMeasurement.VisibleIndex = 25;
            this.colMeasurement.Width = 80;
            // 
            // colFilerName
            // 
            this.colFilerName.Caption = "客服";
            this.colFilerName.FieldName = "CustomerServiceName";
            this.colFilerName.Name = "colFilerName";
            this.colFilerName.Visible = true;
            this.colFilerName.VisibleIndex = 26;
            // 
            // colOverSeasFilerName
            // 
            this.colOverSeasFilerName.Caption = "海外部客服";
            this.colOverSeasFilerName.FieldName = "OverSeasFilerName";
            this.colOverSeasFilerName.Name = "colOverSeasFilerName";
            this.colOverSeasFilerName.Visible = true;
            this.colOverSeasFilerName.VisibleIndex = 27;
            // 
            // colCreateDate
            // 
            this.colCreateDate.Caption = "创建日期";
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 28;
            // 
            // colIsWirteOff
            // 
            this.colIsWirteOff.Caption = "IsWirteOff";
            this.colIsWirteOff.FieldName = "IsWriteOff";
            this.colIsWirteOff.Name = "colIsWirteOff";
            this.colIsWirteOff.Visible = true;
            this.colIsWirteOff.VisibleIndex = 30;
            // 
            // colIsPaid
            // 
            this.colIsPaid.Caption = "IsPaid";
            this.colIsPaid.FieldName = "IsPaid";
            this.colIsPaid.Name = "colIsPaid";
            this.colIsPaid.Visible = true;
            this.colIsPaid.VisibleIndex = 29;
            // 
            // colReleased
            // 
            this.colReleased.Caption = "电放";
            this.colReleased.FieldName = "ReleaseType";
            this.colReleased.Name = "colReleased";
            this.colReleased.Visible = true;
            this.colReleased.VisibleIndex = 31;
            // 
            // colOBLRcved
            // 
            this.colOBLRcved.Caption = "收到正本";
            this.colOBLRcved.FieldName = "OBLRcved";
            this.colOBLRcved.Name = "colOBLRcved";
            this.colOBLRcved.Visible = true;
            this.colOBLRcved.VisibleIndex = 32;
            this.colOBLRcved.Width = 100;
            // 
            // colIsRelease
            // 
            this.colIsRelease.Caption = "IsRelease";
            this.colIsRelease.FieldName = "IsRelease";
            this.colIsRelease.Name = "colIsRelease";
            this.colIsRelease.Visible = true;
            this.colIsRelease.VisibleIndex = 34;
            // 
            // colIsReceiveNotice
            // 
            this.colIsReceiveNotice.Caption = "IsReceiveNotice";
            this.colIsReceiveNotice.FieldName = "IsReceiveNotice";
            this.colIsReceiveNotice.Name = "colIsReceiveNotice";
            this.colIsReceiveNotice.Visible = true;
            this.colIsReceiveNotice.VisibleIndex = 35;
            // 
            // colIsApplyRC
            // 
            this.colIsApplyRC.Caption = "IsApplyRC";
            this.colIsApplyRC.FieldName = "IsApplyRC";
            this.colIsApplyRC.Name = "colIsApplyRC";
            this.colIsApplyRC.Visible = true;
            this.colIsApplyRC.VisibleIndex = 37;
            // 
            // colIsNoticeRelease
            // 
            this.colIsNoticeRelease.Caption = "IsNoticeRelease";
            this.colIsNoticeRelease.FieldName = "IsNoticeRelease";
            this.colIsNoticeRelease.Name = "colIsNoticeRelease";
            this.colIsNoticeRelease.Visible = true;
            this.colIsNoticeRelease.VisibleIndex = 33;
            // 
            // colIsNoticePay
            // 
            this.colIsNoticePay.Caption = "IsNoticePay";
            this.colIsNoticePay.FieldName = "IsNoticePay";
            this.colIsNoticePay.Name = "colIsNoticePay";
            this.colIsNoticePay.Visible = true;
            this.colIsNoticePay.VisibleIndex = 36;
            // 
            // colIsAgreeRC
            // 
            this.colIsAgreeRC.Caption = "AgreeRC";
            this.colIsAgreeRC.FieldName = "IsAgreeRC";
            this.colIsAgreeRC.Name = "colIsAgreeRC";
            this.colIsAgreeRC.Visible = true;
            this.colIsAgreeRC.VisibleIndex = 38;
            // 
            // colIsReleaseCargo
            // 
            this.colIsReleaseCargo.Caption = "IsReleaseCargo";
            this.colIsReleaseCargo.FieldName = "IsReleaseCargo";
            this.colIsReleaseCargo.Name = "colIsReleaseCargo";
            this.colIsReleaseCargo.Visible = true;
            this.colIsReleaseCargo.VisibleIndex = 39;
            // 
            // colIsSentAN
            // 
            this.colIsSentAN.Caption = "SentAN";
            this.colIsSentAN.FieldName = "IsSentAN";
            this.colIsSentAN.Name = "colIsSentAN";
            this.colIsSentAN.Visible = true;
            this.colIsSentAN.VisibleIndex = 4;
            // 
            // colTelexNo
            // 
            this.colTelexNo.Caption = "TelexNo";
            this.colTelexNo.FieldName = "TelexNo";
            this.colTelexNo.Name = "colTelexNo";
            this.colTelexNo.Visible = true;
            this.colTelexNo.VisibleIndex = 40;
            // 
            // colReleaseBLDate
            // 
            this.colReleaseBLDate.Caption = "放单日期";
            this.colReleaseBLDate.FieldName = "ReleaseBLDate";
            this.colReleaseBLDate.Name = "colReleaseBLDate";
            this.colReleaseBLDate.Visible = true;
            this.colReleaseBLDate.VisibleIndex = 41;
            // 
            // colReleaseDate
            // 
            this.colReleaseDate.Caption = "放货日期";
            this.colReleaseDate.FieldName = "ReleaseDate";
            this.colReleaseDate.Name = "colReleaseDate";
            this.colReleaseDate.Visible = true;
            this.colReleaseDate.VisibleIndex = 42;
            // 
            // colUpdateByName
            // 
            this.colUpdateByName.Caption = "更新人";
            this.colUpdateByName.FieldName = "UpdateByName";
            this.colUpdateByName.Name = "colUpdateByName";
            this.colUpdateByName.Visible = true;
            this.colUpdateByName.VisibleIndex = 43;
            // 
            // colUpdateDate
            // 
            this.colUpdateDate.Caption = "更新时间";
            this.colUpdateDate.FieldName = "UpdateDate";
            this.colUpdateDate.Name = "colUpdateDate";
            this.colUpdateDate.Visible = true;
            this.colUpdateDate.VisibleIndex = 44;
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
        private DevExpress.XtraGrid.Columns.GridColumn colVesselVoyage;
        private DevExpress.XtraGrid.Columns.GridColumn colContainerNo;
        private DevExpress.XtraGrid.Columns.GridColumn colPOD;
        private DevExpress.XtraGrid.Columns.GridColumn colITNO;
        private DevExpress.XtraGrid.Columns.GridColumn colFinalDestination;
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
        private DevExpress.XtraGrid.Columns.GridColumn colOBLRcved;
        private DevExpress.XtraGrid.Columns.GridColumn colFETA;
        private DevExpress.XtraGrid.Columns.GridColumn colPOL;
        private DevExpress.XtraGrid.Columns.GridColumn colPlaceOfDeliveryName;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerNo;
        private DevExpress.XtraGrid.Columns.GridColumn colPOLFilerName;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbState;
        private DevExpress.XtraGrid.Columns.GridColumn colOverSeasFilerName;
        private DevExpress.XtraGrid.Columns.GridColumn colDETA;
        private DevExpress.XtraGrid.Columns.GridColumn colIsSentAN;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateByName;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colIsRelease;
        private DevExpress.XtraGrid.Columns.GridColumn colIsReceiveNotice;
        private DevExpress.XtraGrid.Columns.GridColumn colIsReleaseCargo;
        private DevExpress.XtraGrid.Columns.GridColumn colIsApplyRC;
        private DevExpress.XtraGrid.Columns.GridColumn colIsNoticePay;
        private DevExpress.XtraGrid.Columns.GridColumn colIsNoticeRelease;
        private DevExpress.XtraGrid.Columns.GridColumn colIsPaid;
        private DevExpress.XtraGrid.Columns.GridColumn colIsWirteOff;
        private DevExpress.XtraGrid.Columns.GridColumn colTelexNo;
        private DevExpress.XtraGrid.Columns.GridColumn colReleaseBLDate;
        private DevExpress.XtraGrid.Columns.GridColumn colIsAgreeRC;
    }
}
